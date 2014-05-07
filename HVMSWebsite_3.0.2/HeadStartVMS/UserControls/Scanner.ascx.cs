using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
//using DYNAMICWEBTWAINCTRLLib;

//http://www.dynamsoft.com/help/TWAIN/WebTwain/index.htm

namespace METAOPTION.UserControls
{
    public partial class Scanner : System.Web.UI.UserControl
    {
        public string Scanner_ShowUI = ConfigurationManager.AppSettings["ShowScannerUI"];
        public string Scanner_AutoFeedEnabled = ConfigurationManager.AppSettings["AutoFeedEnabled"];
        public string Scanner_DuplexEnabled = ConfigurationManager.AppSettings["DuplexEnabled"];
        public string Scanner_PixelType = ConfigurationManager.AppSettings["PixelType"];
        public string Scanner_Resolution = ConfigurationManager.AppSettings["Resolution"];

        protected void Page_Load(object sender, EventArgs e)
        {

            if (this.ScannedFileCollection.Count > 0)
            {
                // hdnDocumentTypeId.Value = ddlDocumentType.SelectedValue;
                if (this.ScannedFilesUploaded != null)
                    this.ScannedFilesUploaded(this, new ScannedFileEventArgs());
            }
            string strApplicationPath = HttpContext.Current.Request.ApplicationPath;
            if (strApplicationPath == null || strApplicationPath == "/")
                strApplicationPath = "";

            this.hrDWTEXE.HRef = strApplicationPath + "/DynamicWebTWAIN/DynamicWebTWAINPlugIn.msi";
            this.hrDWTMac.HRef = strApplicationPath + "/DynamicWebTWAIN/DynamicWebTWAINMacEditionTrial.pkg";
            if (!Page.IsPostBack)
                BindDocTypeddl();
            hdnEntityId.Value = Convert.ToString(Request.QueryString["eid"]);
            hdnEntityTypeId.Value = Convert.ToString(Request.QueryString["etid"]);
            hdnAddedBy.Value = Convert.ToString(Constant.UserId);
            // txt_fileName.Value = Convert.ToString(Request.QueryString["VIN"]) + "-" + DateTime.Now;
            string VIN = GetLast(Convert.ToString(Request.QueryString["VIN"]), 6);
            hdnFileName.Value = VIN + "_" + Convert.ToString(Request.QueryString["Year"]) + "_" + Convert.ToString(Request.QueryString["Make"]) + "_" + DateTime.Now.ToString("MMddyyyy_HHmmss"); ;
        }
        public static string GetLast(string s, int tail_length)
        {
            if (tail_length >= s.Length)
                return s;
            return s.Substring(s.Length - tail_length);
        }

        public HttpFileCollection ScannedFileCollection
        {
            get { return HttpContext.Current.Request.Files; }
        }
        public event EventHandler<ScannedFileEventArgs> ScannedFilesUploaded;

        #region[Bind DocumnetType DDL]
        protected void BindDocTypeddl()
        {
            DocumentBAL bal = new DocumentBAL();
            ddlDocumentType.DataSource = bal.GetAllDocumentType();
            ddlDocumentType.DataTextField = "DocumentType1";
            ddlDocumentType.DataValueField = "DocumentTypeId";
            ddlDocumentType.DataBind();
            ddlDocumentType.Items.Insert(0, new ListItem("--Select--", "-1"));
        }

        public DropDownList CountryDropDown { get; protected set; }
        #endregion

    }

    #region[Class - ScannedFileEventArgs]
    public class ScannedFileEventArgs : EventArgs
    {
        public HttpPostedFile ScannedFile
        {
            get { if (HttpContext.Current.Request.Files.Count == 0) return null; return HttpContext.Current.Request.Files[0]; }
        }
        public string EventKey
        {
            get
            {
                if (HttpContext.Current.Request.QueryString["EventKey"] == null)
                    return "";
                else
                    return HttpContext.Current.Request.QueryString["EventKey"].ToString();
            }
        }


        public string FileName
        {
            get
            {
                try { return this.ScannedFile.FileName.Split(new string[] { "#$#" }, StringSplitOptions.RemoveEmptyEntries)[0]; }
                catch (Exception) { return ""; }
            }
        }
        public string FileTitle
        {
            get
            {
                try
                {
                    return this.ScannedFile.FileName.Split(new string[] { "#$#" }, StringSplitOptions.RemoveEmptyEntries)[1];
                }
                catch (Exception)
                {

                    return "";
                }
            }
        }
        public string FileDescription
        {
            get
            {
                try
                {
                    return this.ScannedFile.FileName.Split(new string[] { "#$#" }, StringSplitOptions.RemoveEmptyEntries)[2];
                }
                catch (Exception)
                {

                    return "";
                }
            }
        }

        public string EntityId
        {
            get
            {
                try
                {
                    return this.ScannedFile.FileName.Split(new string[] { "#$#" }, StringSplitOptions.RemoveEmptyEntries)[3];
                }
                catch (Exception)
                {

                    return "";
                }
            }
        }
        public string EntityTypeId
        {
            get
            {
                try
                {
                    return this.ScannedFile.FileName.Split(new string[] { "#$#" }, StringSplitOptions.RemoveEmptyEntries)[4];
                }
                catch (Exception)
                {

                    return "";
                }
            }
        }
        public string DocumentTypeId
        {
            get
            {
                try
                {
                    return this.ScannedFile.FileName.Split(new string[] { "#$#" }, StringSplitOptions.RemoveEmptyEntries)[5];
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }
        public string FileType
        {
            get
            {
                try
                {
                    return this.ScannedFile.FileName.Split(new string[] { "#$#" }, StringSplitOptions.RemoveEmptyEntries)[6];
                }
                catch (Exception)
                {

                    return "";
                }
            }
        }
        public string AddedBy
        {
            get
            {
                try
                {
                    return this.ScannedFile.FileName.Split(new string[] { "#$#" }, StringSplitOptions.RemoveEmptyEntries)[7];
                }
                catch (Exception)
                {

                    return "";
                }
            }
        }
    }
    #endregion
}