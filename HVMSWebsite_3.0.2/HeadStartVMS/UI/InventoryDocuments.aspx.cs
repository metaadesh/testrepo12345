using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using METAOPTION.BAL;
using System.IO;

namespace METAOPTION.UI
{
    public partial class InventoryDocuments : System.Web.UI.Page, IPagePermission
    {
        #region [Public Variables and Constants]
        long _inventoryId = -1;
        long _userId = -1;
        public const string PAGE = "INVENTORYDOCUMENT";
        public const string INVENTORYDOCUMENT_INVENTORYDOCUMENT_ADD = "INVENTORYDOCUMENT.ADD";
        public const string INVENTORYDOCUMENT_INVENTORYDOCUMENT_VIEW = "INVENTORYDOCUMENT.VIEW";
        public const string INVENTORYDOCUMENT_INVENTORYDOCUMENT_EDIT = "INVENTORYDOCUMENT.EDIT";
        public const string INVENTORYDOCUMENT_INVENTORYDOCUMENT_DELETE = "INVENTORYDOCUMENT.DELETE";
        #endregion

        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["MasterPageNoLeftPanel"])))
                    this.MasterPageFile = Convert.ToString(Session["MasterPageNoLeftPanel"]);
                else
                    Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(Request.Url.ToString()));
            }
            catch (Exception ex)
            {
                Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(Request.Url.ToString()));
            }
        }

        #region[ Page Load ]
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check User Level Page Permission
            if (!Page.IsPostBack)
            {
                try
                {
                    if (Request.QueryString["Code"] != null && Request.QueryString["Code"].ToString() != "")
                    {
                        Util.Validate_QueryString_Value(6, Request.QueryString["Code"].ToString(), Constant.OrgID);
                    }
                }
                catch { }
            }

            CheckUserPagePermissions();

            try
            {
                if (Request["Code"] != null)
                    this._inventoryId = Convert.ToInt32(Request["Code"]);

                if (Session["empId"] != null)
                    this._userId = Constant.UserId;
            }
            catch { }

            //Do nothing if -1
            if (this._inventoryId == -1)
                return;

            this.lblErr.Visible = false;

            if (!Page.IsPostBack)
            {
                //Show Inventory Header Information which tell about teh current expense being edited for which inventory
                lblInventoryHeader.Text = "Inventory Documents For " + InventoryBAL.GetCurrentInventoryHeader(_inventoryId);
                DocumentBAL obj = new DocumentBAL();
                DataTable dtYMMB = obj.GetYMMBbyInventoryId(Convert.ToInt32(Request["Code"]));
                lblScan.Text = "Scan Document " + Convert.ToString(dtYMMB.Rows[0]["YMMB"]);
                BindControls();
            }


            //Load Inventory Documents for this Inventory Id
            LoadData(_inventoryId);
        }
        #endregion

        #region IPagePermission Members

        public void CheckUserPagePermissions()
        {
            //Get Permissions for User on this page
            System.Collections.Generic.List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, PAGE);

            //Check If any permission found for this page 
            if (dict == null || dict.Count < 1)
                Response.Redirect("Permission.aspx?MSG=INVENTORYDOCUMENT:ADD/EDIT/VIEW/DELETE");

            //Disable Add Link, If No Rights
            if (!dict.Contains(INVENTORYDOCUMENT_INVENTORYDOCUMENT_ADD))
            {
                lnkAddnewDocument.Visible = false;
            }

            else if (!dict.Contains(INVENTORYDOCUMENT_INVENTORYDOCUMENT_EDIT))
            {
                //EnableDisableEditButtons(false);
                Button btnEdit = gvDocuments.FindControl("imgBtnEditDoc") as Button;
                btnEdit.Visible = false;
            }
            else if (!dict.Contains(INVENTORYDOCUMENT_INVENTORYDOCUMENT_DELETE))
            {
                Button btnDelete = gvDocuments.FindControl("imgBtnDelDoc") as Button;
                btnDelete.Visible = false;
            }
        }

        #endregion

        #region[ Grid View page index ]
        /// <summary>
        /// Handle Page Index change event of Gridview control displaying document details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvDocuments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDocuments.PageIndex = e.NewPageIndex;
            LoadData(_inventoryId);
        }
        #endregion

        #region[ Load All Inventory Documents]
        private void LoadData(long inventoryId)
        {

            if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
            {
                btnScan.Visible = false;
                lnkAddnewDocument.Visible = false;
            }
            // String ParentEntityID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString();
            // gvDocuments.DataSource = InventoryBAL.Document_ByEntityId(inventoryId, Convert.ToInt32(Session["empId"]), Convert.ToInt32(ParentEntityID), Convert.ToInt32(Session["LoginEntityTypeID"]));
            // gvDocuments.DataBind();
            //   }
            //  else
            //  {
            gvDocuments.DataSource = InventoryBAL.Document_ByEntityId(6, inventoryId);
            gvDocuments.DataBind();
            //   }




        }
        #endregion

        #region[ Upload Button Click ]
        /// <summary>
        /// Upload File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //Return if InventoryId not provided
            if (_inventoryId == -1)
                return;

            if (this.fuUpload.HasFile)
            {
                if (fuUpload.PostedFile.ContentLength < 1097152)   //2 MB
                {
                    HttpPostedFile fileToUpload = fuUpload.PostedFile;
                    Int32 fLen = fileToUpload.ContentLength;

                    byte[] fileBytes = new byte[fLen];
                    fileToUpload.InputStream.Read(fileBytes, 0, fLen);

                    System.Data.Linq.Binary binaryData = new System.Data.Linq.Binary(fileBytes);
                    String fName = fileToUpload.FileName;
                    String shortName = Path.GetFileName(fileToUpload.FileName);
                    String fType = fileToUpload.ContentType;
                    Document doc = new Document();
                    doc.EntityTypeId = 6;
                    doc.EntityId = _inventoryId;
                    doc.FileType = fType;
                    doc.DocumentTypeId = Convert.ToInt32(this.ddlDocumentType.SelectedValue);
                    doc.DocumentTitle = this.txtTitle.Text.Trim();
                    doc.Description = this.txtDescription.Text.Trim();
                    doc.DocumentBinary = fileBytes;
                    doc.DocumentName = shortName;

                    //doc.DocumentType = fType;
                    doc.AddedBy = Constant.UserId;

                    doc.IsActive = 1;
                    doc.DateAdded = DateTime.Now;

                    //Call BAL Method to Add Inventory Document
                    long result = InventoryBAL.AddInventoryDoc(doc);

                    //Close Popup
                    MPEDocs.Hide();


                }
                else
                {
                    this.lblErr.Text = "File size must be less than 2MB!";
                    this.lblErr.Visible = true;
                }
            }
            LoadData(_inventoryId);
        }
        #endregion

        #region[ Bind Controls ]
        private void BindControls()
        {
            int IsActive = 1;
            ddlDocumentType.DataSource = METAOPTION.BAL.Common.DocumentTypeList(Convert.ToInt16(Constant.OrgID), IsActive);
            ddlDocumentType.DataTextField = "DocumentType1";
            ddlDocumentType.DataValueField = "DocumentTypeId";
            ddlDocumentType.DataBind();
        }
        #endregion

        #region[ Add new document click ]
        /// <summary>
        /// Handle Click event of Link Button to Open Document Popup control for Uploading New Document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkAddnewDocument_Click(object sender, EventArgs e)
        {
            //return if inventoryid not provided
            if (this._inventoryId == -1)
                return;

            // Visible row for uploading file
            trUploadRow.Visible = true;
            hdUpdateDocId.Value = "-1";
            //Disable Save button
            this.btnUpdateDoc.Visible = false;
            this.btnUpload.Visible = true;
            //Set Popup Heading
            lblHeading.Text = "Add Inventory Document";



            //Reset Fields before opening popup to Add New Document
            ResetFields();

            //Open Popup
            MPEDocs.Show();
        }
        #endregion

        #region[ Reset Fields ]
        /// <summary>
        /// Reset All Fields 
        /// </summary>
        private void ResetFields()
        {
            txtDescription.Text = string.Empty;
            txtTitle.Text = string.Empty;
            ddlDocumentType.SelectedIndex = -1;
        }
        #endregion

        #region [ gvDocuments RowCommand ]
        protected void gvDocuments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            OpenAttachment(Convert.ToInt32(e.CommandArgument));
        }
        #endregion

        #region [ Open attached file from the database ]
        /// <summary>
        /// Open attached file from the database 
        /// </summary>
        /// <param name="attachmentid">documentId</param>
        private void OpenAttachment(Int32 documentId)
        {
            BAL.AttachedDocs doc = InventoryBAL.DocumentOpenById(documentId);
            //ProjectAttachment projectAttach = new ProjectAttachment();
            //PMS.BLL.PMS pms = new PMS.BLL.PMS();
            //projectAttach = pms.GetProjectAttachmentById(attachmentid);

            if (doc.FileLength > 0)
            {
                Response.ContentType = Convert.ToString(doc.FileType);
                Response.AppendHeader("Content-Disposition",
                "attachment; filename=\"" + doc.FileName + "\"");
                Response.OutputStream.Write(doc.FileBytes, 0, doc.FileLength);
            }
            else
            {
                Response.Write("Problem in getting file data");
            }
        }
        #endregion

        #region[ Update Document into Database ]
        protected void btnUpdateDoc_Click(object sender, EventArgs e)
        {
            Document doc = new Document();
            doc.DocumentId = Convert.ToInt64(hdUpdateDocId.Value);
            doc.ModifiedBy = this._userId;
            doc.DateModified = DateTime.Now;
            doc.DocumentTitle = txtTitle.Text.Trim();
            doc.Description = txtDescription.Text;
            if (!string.IsNullOrEmpty(ddlDocumentType.SelectedValue))
                doc.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

            //CAL BAL method to Update modifed details
            InventoryBAL.Document_Update(doc);

            //Refresh GridView
            LoadData(_inventoryId);
        }
        #endregion

        #region[ Update Document - Open Popup to Edit ]
        /// <summary>
        /// Update Document - Open Popup to Edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnEditDoc_Click(object sender, ImageClickEventArgs e)
        {
            //Enabled Save button
            this.btnUpdateDoc.Visible = true;
            this.btnUpload.Visible = false;
            trUploadRow.Visible = false;

            ImageButton imgBtnEdit = (ImageButton)sender;
            //Set Popup Heading
            lblHeading.Text = "Edit Inventory Document";

            GridViewRow row = (GridViewRow)imgBtnEdit.NamingContainer;

            //Set DocumentID for which records need to be updated in db into HIDDEN Field
            hdUpdateDocId.Value = Convert.ToString(gvDocuments.DataKeys[row.RowIndex].Value);

            //Set Control Values with gridview control clicked record for editing
            txtTitle.Text = row.Cells[1].Text;
            txtDescription.Text = row.Cells[2].Text;

            String value = ((HiddenField)row.Cells[0].FindControl("hfDocumentTypeId")).Value;
            if (ddlDocumentType.Items.FindByValue(value) != null)
                ddlDocumentType.SelectedValue = value;

            //Open Popup to Edit Records
            MPEDocs.Show();
        }
        #endregion

        #region[ Delete document mark IsActive = 0 ]
        /// <summary>
        /// Delete document mark IsActive = 0 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnDelDoc_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgBtnDeleteDoc = (ImageButton)sender;
            //lblHeading.Text = "Edit Expense Details";
            GridViewRow row = (GridViewRow)imgBtnDeleteDoc.NamingContainer;
            long DocId = Convert.ToInt64(gvDocuments.DataKeys[row.RowIndex].Value);

            ////Call BAL Method to delete expense
            InventoryBAL.Document_Delete(DocId, this._userId);

            //Refresh Gridview Control
            LoadData(_inventoryId);
        }
        #endregion

        protected void lnkbtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("InventoryDetail.aspx?Code=" + this._inventoryId.ToString());
        }

        #region[Added by Rupendra 2 Nov 12 for Scan Document]
        protected void btnScan_Click(object sender, EventArgs e)
        {
            DataTable result = new DataTable();
            DocumentBAL obj = new DocumentBAL();
            mpeScan.Show();
            hfInventoryID.Value = Request.QueryString["Code"];
            if (!string.IsNullOrEmpty(hfInventoryID.Value))
                result = obj.GetYearMakeByInventoryId(Convert.ToInt32(hfInventoryID.Value));
            frmScanner.Attributes.Add("src", String.Format("Scanner.aspx?etid={0}&eid={1}&VIN={2}&Year={3}&Make={4}", 6, hfInventoryID.Value, result.Rows[0]["VIN"], result.Rows[0]["Year"], result.Rows[0]["Make"]));
        }


        protected void btnImgDocument_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton ibtncars = (ImageButton)sender;
                GridViewRow row = (GridViewRow)ibtncars.NamingContainer;
                long DocumentID = Convert.ToInt64(gvDocuments.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
                ifrmSlideShow.Attributes.Add("src", String.Format("DocumentViewer.aspx?Id={0}", DocumentID));
                ModalPopupExtender1.Show();
            }
            catch (Exception ex) { }
        }

        protected void imgDownload_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DocumentBAL objDocument = new DocumentBAL();
                string ContentType = string.Empty;
                string ContentTypeValue = string.Empty;
                ImageButton ibtncars = (ImageButton)sender;
                GridViewRow row = (GridViewRow)ibtncars.NamingContainer;
                long DocumentID = Convert.ToInt64(gvDocuments.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);

                DataTable dtFile = new DataTable();
                dtFile = objDocument.GetDocumentBinarybyDocId(DocumentID);
                if (dtFile.Rows.Count > 0)
                {
                    byte[] buffer = (byte[])dtFile.Rows[0]["DocumentBinary"];
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.ClearContent();
                    if (!string.IsNullOrEmpty(Convert.ToString(dtFile.Rows[0]["DocumentName"])))
                    {
                        ContentType = Convert.ToString(dtFile.Rows[0]["DocumentName"]);
                        String extn = ContentType.Substring(ContentType.LastIndexOf("."));
                        switch (extn.ToLower())
                        {
                            case ".jpg":
                                ContentTypeValue = "image/jpeg";
                                break;
                            case ".jpeg":
                                ContentTypeValue = "image/jpeg";
                                break;
                            case ".png":
                                ContentTypeValue = "image/png";
                                break;
                            case ".pdf":
                                ContentTypeValue = "application/pdf";
                                break;
                            default:
                                ContentTypeValue = "image/jpeg";
                                break;
                        }
                    }
                    Response.AddHeader("Content-type", ContentTypeValue);
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Convert.ToString(dtFile.Rows[0]["DocumentName"]).Replace(" ", "_") + "");
                    Response.AddHeader("Content-Length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);
                    Response.Flush();
                    Response.End();
                }

            }
            catch (Exception ex) { }
        }

        protected void gvDocuments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
            //    {
            //        e.Row.Cells[5].Visible = false;
            //    }

            //}
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnImgEdit = (ImageButton)e.Row.FindControl("imgBtnEditDoc");
                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgBtnDelDoc");
                //HiddenField hdnFileFormat = (HiddenField)e.Row.FindControl("hdnFileFormat");

                //if (!string.IsNullOrEmpty(hdnFileFormat.Value))
                //{
                //    if ((hdnFileFormat.Value == "image/png") || (hdnFileFormat.Value == ".png") || (hdnFileFormat.Value == "image/jpeg") || (hdnFileFormat.Value == "image/jpg") || ((hdnFileFormat.Value == ".jpg")))
                //    {
                //        btnImgDocument.Visible = true;
                //        imgDownload.Visible = false;
                //    }
                //    else
                //    {
                //        btnImgDocument.Visible = false;
                //        imgDownload.Visible = true;
                //    }
                //}

                if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
                {
                    btnImgEdit.Visible = false;
                    imgDelete.Visible = false;
                }
                ImageButton lb = e.Row.FindControl("imgDownload") as ImageButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lb);

            }
        }
        #endregion



    }
}