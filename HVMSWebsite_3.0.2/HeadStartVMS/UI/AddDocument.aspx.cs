using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using METAOPTION.BAL;

namespace METAOPTION.UI
{
    
    public partial class AddDocument : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region[Page Load Event]
            if (!IsPostBack)
            {
                Int16 OrganisationID = Constant.OrgID;
                if (Request["mode"] == "edit")
                    GetDocumentDetails();
                else
                    btnUpdate.Visible = false;

            }
            #endregion
        }
        #region[ Get Details of Doc & Update details]
        protected void GetDocumentDetails()
        {
            btnUpload.Visible = false; // Upload Button Visibility set false
            DataTable dtdoc = InventoryBAL.DocumentByID(Convert.ToInt32(Request["ID"]));
            if (dtdoc.Rows.Count > 0)
            {
                ddlDocumentType.SelectedValue = dtdoc.Rows[0]["DocumentTypeID"].ToString();
                txtTitle.Text = dtdoc.Rows[0]["DocumentTitle"].ToString();
                txtDescription.Text = dtdoc.Rows[0]["Description"].ToString();
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Document doc = new Document();
            doc.DocumentId = Convert.ToInt64(Request["ID"]);
            doc.ModifiedBy = Constant.UserId;
            doc.DateModified = DateTime.Now;
            doc.DocumentTitle = txtTitle.Text.Trim();
            doc.Description = txtDescription.Text;
            if (!string.IsNullOrEmpty(ddlDocumentType.SelectedValue))
                doc.DocumentTypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);

            //CAL BAL method to Update modifed details
            InventoryBAL.Document_Update(doc);

            //Refresh Page
            Page.ClientScript.RegisterStartupScript(typeof(AddDocument), "closeThickBox", "self.parent.updated();", true);
        }
        #endregion

        #region[Upload New Document]
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //Return if EntityId not provided
            if (Request["EntityId"] == null)
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
                    doc.EntityTypeId = Convert.ToInt32(Request["type"]);
                    doc.EntityId = Convert.ToInt64(Request["EntityId"]);
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
                    long result = METAOPTION.BAL.InventoryBAL.AddInventoryDoc(doc);

                }
                else
                {
                    //this.lblErr.Text = "File size must be less than 2MB!";
                    //this.lblErr.Visible = true;
                }
               Page.ClientScript.RegisterStartupScript(typeof(AddDocument), "closeThickBox", "self.parent.updated();", true);
                //Response.Redirect("ViewEmployee.aspx?EmployeeId=" + Request["EmployeeId"]);
            }
        }
        #endregion
    }
}
