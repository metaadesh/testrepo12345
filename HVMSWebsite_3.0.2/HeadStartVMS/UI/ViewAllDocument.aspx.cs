using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.IO;
using System.Text;
using System.Drawing.Imaging;
using System.Web.UI.HtmlControls;
using System.Net;
using System.ComponentModel;
using System.Web.Security;
using METAOPTION.BAL;

namespace METAOPTION.UI
{
    public partial class ViewAllDocument : System.Web.UI.Page
    {
        DocumentBAL objDocument = new DocumentBAL();
        DataSet ds = new DataSet();
        DataTable dtAddedBy = new DataTable();
        DataTable dtDesciption = new DataTable();
        DataTable dtDocumentType = new DataTable();
        DataTable dtDocumentTitle = new DataTable();
        DataTable dtFileType = new DataTable();
        DataTable dtEntityType = new DataTable();

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDocumentFilters();
                BindSort1DDL();
                txtDateFrom.Text = System.DateTime.Today.AddDays(-7).ToShortDateString();
                txtDateTo.Text = System.DateTime.Today.ToShortDateString();
                BindGrid();
            }
        }

        #region[Bind grid]
        protected void BindGrid()
        {
            try
            {
                String DateFrom, DateTo;
                string VIN = "";
                string EntityId = "-1";
                string EntityTypeId = "-1";
                string DocumentTypeId, strAddedby = "-1";
                string FileType, DocName, DocDescription = "-1";


               // gvAllDocument.PageSize = Convert.ToInt32(ddlPageSize1.SelectedValue);


                if (String.IsNullOrEmpty(txtDateFrom.Text))
                    DateFrom = DateTime.Today.AddYears(-100).ToShortDateString();
                else
                    DateFrom = txtDateFrom.Text;

                if (String.IsNullOrEmpty(txtDateTo.Text))
                    DateTo = DateTime.Today.AddDays(1).ToShortDateString();
                else
                    DateTo = Convert.ToDateTime(txtDateTo.Text).AddDays(1).ToShortDateString();

                gvAllDocument.PageSize = Convert.ToInt32(ddlPageSize1.SelectedValue);

                String sort = String.Empty;
                if (ddlSort1.SelectedValue != "-1")
                    sort = String.Format("{0} {1}", ddlSort1.SelectedValue, rbtnSort1Direction.SelectedValue);

                if (ddlSort2.SelectedValue != "-1")
                    sort += String.Format("{0} {1}"
                        , sort == "" ? ddlSort2.SelectedValue : ", " + ddlSort2.SelectedValue
                        , rbtnSort2Direction.SelectedValue);

                if (ddlSort3.SelectedValue != "-1")
                    sort += String.Format("{0} {1}"
                        , sort == "" ? ddlSort3.SelectedValue : ", " + ddlSort3.SelectedValue
                        , rbtnSort3Direction.SelectedValue);

                if (String.IsNullOrEmpty(sort))
                    sort = "DocumentId desc";

                ObjectDataSource odsDocumnet = new ObjectDataSource();
                odsDocumnet.Selected += new ObjectDataSourceStatusEventHandler(odsDocument_Selected);
                odsDocumnet.TypeName = "METAOPTION.DocumentBAL";
                odsDocumnet.SelectMethod = "GetDocumentDetails";
                odsDocumnet.SelectCountMethod = "GetDocumentDetailsCount";
                odsDocumnet.EnablePaging = true;



                if (!string.IsNullOrEmpty(Hidden.Value))
                    strAddedby = Hidden.Value;
                else
                    Hidden.Value = "-1";

                if (!string.IsNullOrEmpty(hdnDocType.Value))
                    DocumentTypeId = hdnDocType.Value;
                else
                    DocumentTypeId = "-1";

                if (!string.IsNullOrEmpty(hdnEntityType.Value))
                    EntityTypeId = hdnEntityType.Value;
                else
                    EntityTypeId = "-1";

                if (!string.IsNullOrEmpty(hdnFileType.Value))
                    FileType = hdnFileType.Value;
                else
                    FileType = "-1";

                if (!string.IsNullOrEmpty(txtDocName.Text))
                    DocName = txtDocName.Text;
                else
                    DocName = "";

                if (!string.IsNullOrEmpty(txtDescription.Text))
                    DocDescription = txtDescription.Text;
                else
                    DocDescription = "";


                //if (ddlPaging.SelectedValue == null && ddlPaging.SelectedValue == null)
                //{
                //    ddlPaging.SelectedValue = ddlPaging.Items.FindByValue("1").Value;
                //    ddlPaging1.SelectedValue = ddlPaging1.Items.FindByValue("1").Value;
                //}

                odsDocumnet.SelectParameters.Add("VIN", txtVINNumber.Text);
                odsDocumnet.SelectParameters.Add("EntityId", EntityId);
                odsDocumnet.SelectParameters.Add("EntityTypeId", EntityTypeId);
                odsDocumnet.SelectParameters.Add("DocumentTypeId", DocumentTypeId);
                odsDocumnet.SelectParameters.Add("AddedBy", strAddedby);
                odsDocumnet.SelectParameters.Add("FileType", FileType);
                odsDocumnet.SelectParameters.Add("DocName", DocName);
                odsDocumnet.SelectParameters.Add("DocDescription", DocDescription);
                odsDocumnet.SelectParameters.Add("DateFrom", DateFrom);
                odsDocumnet.SelectParameters.Add("DateTo", DateTo);
                odsDocumnet.SelectParameters.Add("StartRowIndex", DbType.Int32, gvAllDocument.PageIndex.ToString());
                odsDocumnet.SelectParameters.Add("MaximumRows", DbType.Int32, ddlPageSize1.SelectedValue);
                odsDocumnet.SelectParameters.Add("SortExpression", sort);
                odsDocumnet.SelectParameters.Add("OrgID", DbType.Int16, Constant.OrgID.ToString());
                gvAllDocument.DataSource = odsDocumnet;
                gvAllDocument.DataBind();

            } 
            catch (Exception ex) { }
            //finally
            //{
            //    Hidden.Value = "";
            //    hdnEntityType.Value = "";
            //    hdnDocType.Value = "";
            //}
        }


        //protected void gvAllDocument_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        ImageButton btnImgDocument = (ImageButton)e.Row.FindControl("btnImgDocument");
        //        ImageButton imgDownload = (ImageButton)e.Row.FindControl("imgDownload");
        //        HiddenField hdnFileFormat = (HiddenField)e.Row.FindControl("hdnFileFormat");

        //        if (!string.IsNullOrEmpty(hdnFileFormat.Value))
        //        {
        //            if ((hdnFileFormat.Value == "image/png") || (hdnFileFormat.Value == ".png") || (hdnFileFormat.Value == "image/jpeg") || (hdnFileFormat.Value == "image/jpg") || (hdnFileFormat.Value == ".jpg"))
        //            {
        //                btnImgDocument.Visible = true;
        //                imgDownload.Visible = false;
        //            }
        //            else
        //            {
        //                btnImgDocument.Visible = false;
        //                imgDownload.Visible = true;
        //            }
        //        }
        //    }
        //}
        #endregion

        #region [Bind Filter Dropdown]
        public void BindDocumentFilters()
        {
            try
            {
                ds = objDocument.GetDocumentFilters(Constant.OrgID);
                ds.Tables[0].TableName = "dtFileType";
                ds.Tables[1].TableName = "dtDocumentType";
                ds.Tables[2].TableName = "dtDocumentTitle";
                ds.Tables[3].TableName = "dtDesciption";
                ds.Tables[4].TableName = "dtAddedBy";
                ds.Tables[5].TableName = "dtEntityType";

                dtAddedBy = ds.Tables["dtAddedBy"];
                dtDesciption = ds.Tables["dtDesciption"];
                dtDocumentType = ds.Tables["dtDocumentType"];
                dtDocumentTitle = ds.Tables["dtDocumentTitle"];
                dtFileType = ds.Tables["dtFileType"];
                dtEntityType = ds.Tables["dtEntityType"];

                dlAddedBy.DataSource = dtAddedBy;
                dlAddedBy.DataTextField = "DisplayName";
                dlAddedBy.DataValueField = "SecurityUserID";
                dlAddedBy.DataBind();

                //dlDescription.DataSource = dtDesciption;
                //dlDescription.DataTextField = "Description";
                //dlDescription.DataValueField = "Description";
                //dlDescription.DataBind();

                dlDocumentType.DataSource = dtDocumentType;
                dlDocumentType.DataTextField = "DocumentType";
                dlDocumentType.DataValueField = "DocumentTypeId";
                dlDocumentType.DataBind();

                //dlDocumentName.DataSource = dtDocumentTitle;
                //dlDocumentName.DataTextField = "DocumentTitle";
                //dlDocumentName.DataValueField = "DocumentTitle";
                //dlDocumentName.DataBind();

                dlEntityType.DataSource = dtEntityType;
                dlEntityType.DataTextField = "EntityType";
                dlEntityType.DataValueField = "EntityTypeId";
                dlEntityType.DataBind();

                dlFileType.DataSource = dtFileType;
                dlFileType.DataTextField = "FileType";
                dlFileType.DataValueField = "FileType";
                dlFileType.DataBind();
            }
            catch (Exception ex) { }
        }
        #endregion

        #region[Gridview Selected Event]
        protected void odsDocument_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue == null) return;
            if (e.ReturnValue.GetType().Name == "Int32")
            {

                Int32 count = Convert.ToInt32(e.ReturnValue);
                Int32 pagesize = gvAllDocument.PageSize;
                Int32 pagecount = count / pagesize;
                if (count < pagesize)
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} of {0} records", count);
                }
                else
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} through {1} of {2} records"
                         , String.Format("{0:#,###}", pagesize * gvAllDocument.PageIndex + 1)
                         , String.Format("{0:#,###}", pagesize * (gvAllDocument.PageIndex + 1) > count ? count : pagesize * (gvAllDocument.PageIndex + 1))
                         , String.Format("{0:#,###}", count));
                }

                if ((count % pagesize) > 0) pagecount++;

                ddlPaging.Items.Clear();
                ddlPaging1.Items.Clear();

                if (pagecount != 0)
                {
                    for (int i = 0; i < pagecount; i++)
                    {
                        ddlPaging.Items.Add(new ListItem((i + 1).ToString(), (i + 1).ToString()));
                        ddlPaging1.Items.Add(new ListItem((i + 1).ToString(), (i + 1).ToString()));
                    }


                    if (count < pagesize)
                    {
                       // ddlPaging.Items.Add(new ListItem("1", "1"));
                        ddlPaging.SelectedValue = "1";
                       // ddlPaging1.Items.Add(new ListItem("1", "1"));
                        ddlPaging1.SelectedValue = "1";
                    }
                    else
                    {
                        ddlPaging.SelectedValue = String.Format("{0}", gvAllDocument.PageIndex + 1);
                        ddlPaging1.SelectedValue = String.Format("{0}", gvAllDocument.PageIndex + 1);
                    }
                    EnablePaging();

                }
                else
                {
                    ddlPaging.Items.Add(new ListItem("0", "0"));
                    ddlPaging.SelectedValue = "0";
                    ddlPaging1.Items.Add(new ListItem("0", "0"));
                    ddlPaging1.SelectedValue = "0";
                    EnablePaging();
                }
            }
        }
        #endregion

        #region[Enable/Disable paging]
        private void EnablePaging()
        {
            btnPrev1.Enabled = btnPrev.Enabled = true;
            btnFirst1.Enabled = btnFirst.Enabled = true;
            btnNext1.Enabled = btnNext.Enabled = true;
            btnLast1.Enabled = btnLast.Enabled = true;

            if ((ddlPaging.SelectedValue == "0") || (ddlPaging1.SelectedValue == "0"))
            {
                btnPrev1.Enabled = btnPrev.Enabled = false;
                btnFirst1.Enabled = btnFirst.Enabled = false;
                btnNext1.Enabled = btnNext.Enabled = false;
                btnLast1.Enabled = btnLast.Enabled = false;
            }
            else if (ddlPaging.SelectedValue == "1")
            {
                if ((ddlPaging.SelectedValue == ddlPaging.Items.Count.ToString()) || (ddlPaging1.SelectedValue == ddlPaging1.Items.Count.ToString()))
                {
                    btnPrev1.Enabled = btnPrev.Enabled = false;
                    btnFirst1.Enabled = btnFirst.Enabled = false;
                    btnNext1.Enabled = btnNext.Enabled = false;
                    btnLast1.Enabled = btnLast.Enabled = false;
                }
                else
                {
                    btnPrev1.Enabled = btnPrev.Enabled = false;
                    btnFirst1.Enabled = btnFirst.Enabled = false;
                }
            }
            else if ((ddlPaging.SelectedValue == ddlPaging.Items.Count.ToString()) || (ddlPaging1.SelectedValue == ddlPaging1.Items.Count.ToString()))
            {
                btnNext1.Enabled = btnNext.Enabled = false;
                btnLast1.Enabled = btnLast.Enabled = false;

            }
        }
        #endregion

        #region[Paging Click events]
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            gvAllDocument.PageIndex = 0;
            BindGrid();
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedIndex > 0)
                gvAllDocument.PageIndex--;
            BindGrid();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedValue != ddlPaging.Items.Count.ToString())
                gvAllDocument.PageIndex++;
            BindGrid();
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            gvAllDocument.PageIndex = ddlPaging.Items.Count - 1;
            BindGrid();
        }
        #endregion

        #region[Page size selection change]
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            if (ddl.ID == "ddlPageSize1")
                ddlPageSize2.SelectedValue = ddlPageSize1.SelectedValue;
            else
                ddlPageSize1.SelectedValue = ddlPageSize2.SelectedValue;

            gvAllDocument.PageIndex = 0;
            BindGrid();
        }
        #endregion

        #region[Drop down page selection change]
        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                gvAllDocument.PageIndex = Convert.ToInt32(((DropDownList)sender).SelectedValue) - 1;
            BindGrid();
            EnablePaging();
        }
        #endregion

        #region[ Bind sort dropdown]
        private void BindSort1DDL()
        {
            try
            {
                ddlSort1.DataSource = objDocument.DocumentListing_SortOptions("Select SortText,SortValue from Lookup_Sort where tableid=10 order by Sequence asc", "", "");
                ddlSort1.DataTextField = "SortText";
                ddlSort1.DataValueField = "SortValue";
                ddlSort1.DataBind();

                ListItem lst = ddlSort1.Items.FindByText("EntityName");
                ddlSort1.Items.Remove(lst);

                ddlSort1.Items.Insert(0, new ListItem("", "-1"));

                BindSort2DDL();
                BindSort3DDL();
            }
            catch (Exception ex) { }
        }

        private void BindSort2DDL()
        {
            try
            {
                ddlSort2.DataSource = objDocument.DocumentListing_SortOptions(String.Format("Select SortText,SortValue from Lookup_Sort where tableid=10 AND SortValue NOT IN ('{0}') order by Sequence asc", ddlSort1.SelectedValue), "", "");
                ddlSort2.DataTextField = "SortText";
                ddlSort2.DataValueField = "SortValue";
                ddlSort2.DataBind();

                ListItem lst = ddlSort2.Items.FindByText("EntityName");
                ddlSort2.Items.Remove(lst);
                ddlSort2.Items.Insert(0, new ListItem("", "-1"));

                BindSort3DDL();
            }
            catch (Exception ex) { }
        }

        private void BindSort3DDL()
        {
            try
            {
                ddlSort3.DataSource = objDocument.DocumentListing_SortOptions(String.Format("Select SortText,SortValue from Lookup_Sort where tableid=10 AND SortValue NOT IN ('{0}', '{1}') order by Sequence asc", ddlSort1.SelectedValue, ddlSort2.SelectedValue), "", "");
                ddlSort3.DataTextField = "SortText";
                ddlSort3.DataValueField = "SortValue";
                ddlSort3.DataBind();

                ListItem lst = ddlSort3.Items.FindByText("EntityName");
                ddlSort3.Items.Remove(lst);

                ddlSort3.Items.Insert(0, new ListItem("", "-1"));
            }
            catch (Exception ex) { }

        }
        #endregion

        #region[Sort dropdown selected index changed handler]
        protected void ddlSort1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSort2DDL();
            ddlSort2.SelectedValue = "-1";
            ddlSort3.SelectedValue = "-1";
        }

        protected void ddlSort2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSort3DDL();
            ddlSort3.SelectedValue = "-1";
        }
        #endregion

        #region[Sorting]
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] != null)
                    return (SortDirection)ViewState["sortDirection"];
                else
                    return SortDirection.Ascending;
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected void gvAllDocument_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, ASCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, DESCENDING);
            }

        }

        private void SortGridView(string sortExpression, string direction)
        {
            if (ddlSort1.Items.FindByValue(sortExpression) != null)
            {
                ddlSort1.SelectedValue = sortExpression;
                if (direction == " ASC")
                    rbtnSort1Direction.SelectedIndex = 0;
                else
                    rbtnSort1Direction.SelectedIndex = 1;
            }

            if (ddlSort2.Items.FindByValue(sortExpression) != null)
            {
                ddlSort2.SelectedValue = "-1";
                rbtnSort2Direction.SelectedIndex = 0;
            }

            if (ddlSort3.Items.FindByValue(sortExpression) != null)
            {
                ddlSort3.SelectedValue = "-1";
                rbtnSort3Direction.SelectedIndex = 0;
            }



            BindGrid();
        }

        #endregion

        #region[Search button]
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvAllDocument.PageIndex = 0;
            BindGrid();
        }
        #endregion

        protected void btnImgDocument_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton ibtncars = (ImageButton)sender;
                GridViewRow row = (GridViewRow)ibtncars.NamingContainer;
                long DocumentID = Convert.ToInt64(gvAllDocument.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
                ifrmSlideShow.Attributes.Add("src", String.Format("DocumentViewer.aspx?Id={0}", DocumentID));
                ModalPopupExtender1.Show();
            }
            catch (Exception ex) { }
        }

        protected void imgDownload_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                string ContentType = string.Empty;
                string ContentTypeValue = string.Empty;
                ImageButton ibtncars = (ImageButton)sender;
                GridViewRow row = (GridViewRow)ibtncars.NamingContainer;
                long DocumentID = Convert.ToInt64(gvAllDocument.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);

                DataTable dtFile = new DataTable();
                dtFile = objDocument.GetDocumentBinarybyDocId(DocumentID);
                if (dtFile.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dtFile.Rows[0]["DocumentBinary"])))
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dtFile.Rows[0]["DocumentName"])))
                        {
                            byte[] buffer = (byte[])dtFile.Rows[0]["DocumentBinary"];

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

                            Response.AddHeader("Content-type", ContentTypeValue);
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + Convert.ToString(dtFile.Rows[0]["DocumentName"]).Replace(" ", "_") + "");
                            Response.AddHeader("Content-Length", buffer.Length.ToString());
                            Response.BinaryWrite(buffer);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }

            }
            catch (Exception ex) { }
        }

        #region [Delete Document]
        protected void imgDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton ibtnExpenseEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)ibtnExpenseEdit.NamingContainer;
                long DocumentID = Convert.ToInt64(gvAllDocument.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
                //Commented by Ashar on 4th Sep'13, deleted by info was not saving
                //objDocument.DeleteDocument(Convert.ToInt32(DocumentID));                
                InventoryBAL.Document_Delete(DocumentID, Constant.UserId);
                BindGrid();
            }
            catch (Exception ex) { }
        }

        protected void gvAllDocument_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string VIN = Convert.ToString(e.CommandArgument);
                if (!string.IsNullOrEmpty(VIN))
                {
                    Response.Redirect("InventoryDocuments.aspx?Code=" + VIN);
                }
            }
        }


        protected void gvAllDocument_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //ImageButton imgDownload = e.Row.FindControl("imgDownload") as ImageButton;
                //ScriptManager.GetCurrent(this).RegisterPostBackControl(imgDownload);
                //ImageButton btnImgDocument = e.Row.FindControl("btnImgDocument") as ImageButton;
                //ScriptManager.GetCurrent(this).RegisterPostBackControl(btnImgDocument);
            }
        }

        protected void btnClose_Click(object sender, ImageClickEventArgs e)
        {
            ModalPopupExtender1.Hide();
            BindGrid();
        }
        #endregion


    }
}