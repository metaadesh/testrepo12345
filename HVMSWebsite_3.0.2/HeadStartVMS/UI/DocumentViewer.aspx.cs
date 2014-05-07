using System;
using System.IO;
using System.Web;
using METAOPTION;
using System.Data;


namespace WebAppTWAIN
{
    public partial class documentviewer : System.Web.UI.Page
    {
        DocumentBAL objDocument = new DocumentBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] filePaths = Directory.GetFiles(Server.MapPath("../Temp"));
            foreach (string filePath in filePaths)
                File.Delete(filePath);

            if (Request.QueryString["Id"] != null)
            {
                ShowFile(Convert.ToInt32(Request.QueryString["Id"]));
            }
            else
            {
                Response.Redirect("~/Home.aspx");
            }
        }
        private void ShowFile(int DocumentIdID)
        {
            DataTable dtFile = new DataTable();
            dtFile = objDocument.GetDocumentBinarybyDocId(DocumentIdID);
            if (dtFile.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(dtFile.Rows[0]["DocumentBinary"])))
                {
                    byte[] buffer = (byte[])dtFile.Rows[0]["DocumentBinary"];
                    this.Title = this.Title + " :: Showing file - " + Convert.ToString(dtFile.Rows[0]["DocumentName"]);
                    string sTempFilePath = Path.Combine(Server.MapPath("../Temp"), Convert.ToString(dtFile.Rows[0]["DocumentName"]));
                    File.WriteAllBytes(sTempFilePath, buffer);
                    viewerFrame.Attributes.Add("src", HttpUtility.UrlPathEncode("../Temp/" + Convert.ToString(dtFile.Rows[0]["DocumentName"])));
                }
            }
        }
    }
}