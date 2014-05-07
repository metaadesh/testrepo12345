using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using METAOPTION.UserControls;

namespace METAOPTION.UI
{
    public partial class Scanner : System.Web.UI.Page
    {
        Int32 _EntityTypeID;
        long _EntityID;

        protected void Page_Load(object sender, EventArgs e)
        {
            //UserControl p = this.Parent as UserControl;
            //HiddenField hd = (HiddenField)p.FindControl("hfInventoryID");
            //_EntityID = Convert.ToInt64(hd.Value);
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["etid"] != null && Request.QueryString["eid"] != null)
                {
                    _EntityTypeID = Convert.ToInt32(Request.QueryString["etid"]);
                    _EntityID = Convert.ToInt64(Request.QueryString["eid"]);

                    Session["ETID"] = _EntityTypeID;
                    Session["EID"] = _EntityID;
                }
            }

        }


        protected void webScanner_ScannedFilesUploaded(object sender, ScannedFileEventArgs e)
        {
            UploadDocs(e.FileTitle, e.FileDescription, e.ScannedFile, e.FileName, e.EntityId, e.EntityTypeId, e.DocumentTypeId, e.FileType, e.AddedBy);
        }

        #region[Upload New Document]
        private void UploadDocs(String DocTitle, String DocDesc, HttpPostedFile file, String DocName, String EntityId, String EntityTypeId, String DocumentTypeId, String FileType, String AddedBy)
        {
            try
            {

                HttpPostedFile uploadfile = file;
                var memoryStream = new System.IO.MemoryStream();
                uploadfile.InputStream.CopyTo(memoryStream);
                memoryStream.ToArray();

                Byte[] inputBuffer = memoryStream.ToArray();

                Document doc = new Document();
                doc.EntityTypeId = Convert.ToInt32(EntityTypeId);// Convert.ToInt32(Session["ETID"]);    //6
                doc.EntityId = Convert.ToInt64(EntityId); // Convert.ToInt64(Session["EID"]); //178537

                if (DocumentTypeId.ToLower() != "nodata")
                {
                    doc.DocumentTypeId = Convert.ToInt32(DocumentTypeId);
                }
                else
                {
                    doc.DocumentTypeId = null;
                }
                // doc.DocumentTypeId = Convert.ToInt32(strDocTypeId);//20;// Convert.ToInt32(this.ddlDocumentType.SelectedValue);

                if (DocTitle.ToLower() != "nodata")
                    doc.DocumentTitle = DocTitle;
                else
                    doc.DocumentTitle = "";
                doc.DocumentName = DocName;
                if (DocDesc.ToLower() != "nodata")
                    doc.Description = DocDesc;
                else
                    doc.Description = "";
                doc.DocumentBinary = inputBuffer;
                doc.FileType = FileType;
                if (!string.IsNullOrEmpty(AddedBy))
                    doc.AddedBy = Convert.ToInt64(AddedBy);// Constant.UserId;
                doc.DateAdded = DateTime.Now;
                //Call BAL Method to Add Inventory Document
                DocumentBAL docBAL = new DocumentBAL();
                Int64 result = docBAL.Document_Add(doc);
            }
            catch (Exception ex) { }
        }
        #endregion
    }

}