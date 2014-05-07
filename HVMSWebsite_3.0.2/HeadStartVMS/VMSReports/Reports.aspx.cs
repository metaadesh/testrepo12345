using System;
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
using Microsoft.Reporting;
using Microsoft.Reporting.WebForms; 
using System.Xml.Linq;
using System.Net;
using System.Security.Principal;

namespace METAOPTION.Reports
{

    public partial class Reports : System.Web.UI.Page
    {
        String ReturnURL = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["ReturnURL"] != null)
                ReturnURL = Convert.ToString(Request["ReturnURL"]);

            if (!Page.IsPostBack)
                // check for report parameters and report Name
                if (ReportParameters.Parameters != null && ReportParameters.ReportName != String.Empty)
                    RenderReport(ReportParameters.Parameters, ReportParameters.ReportName);
        }

        /// <summary>
        /// Render report according to the report name and parameters
        /// </summary>
        /// <param name="prams"></param>
        /// <param name="reportName"></param>
        public void RenderReport(ReportParameter[] prams, String reportName)
        {
            String reportServerUrl = System.Configuration.ConfigurationManager.AppSettings["ReportServer"].ToString();
            rptView.ServerReport.ReportServerUrl = new System.Uri(reportServerUrl);
            rptView.ServerReport.ReportPath = reportName;
            rptView.ProcessingMode = ProcessingMode.Remote;

            string rsUserName = System.Configuration.ConfigurationManager.AppSettings["rsUserName"].ToString();
            string rsPassword = System.Configuration.ConfigurationManager.AppSettings["rsPassword"].ToString();
            string rsDomain = System.Configuration.ConfigurationManager.AppSettings["rsDomain"].ToString();

            rptView.ServerReport.ReportServerCredentials = new CustomReportCredentials(rsUserName, rsPassword, rsDomain);
            //System.Net.NetworkCredential nc = new NetworkCredential(rsUserName, rsPassword, rsDomain);
            //rptView.ServerReport.ReportServerCredentials = nc;
            rptView.ServerReport.SetParameters(prams);
            rptView.ShowCredentialPrompts = false;
            rptView.ServerReport.Refresh();

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(ReturnURL);
        }

    }

    #region[Report Viewer Credentials]
    public class CustomReportCredentials : Microsoft.Reporting.WebForms.IReportServerCredentials
    {

        // local variable for network credential.
        private string _UserName;
        private string _PassWord;
        private string _DomainName;
        public CustomReportCredentials(string UserName, string PassWord, string DomainName)
        {
            _UserName = UserName;
            _PassWord = PassWord;
            _DomainName = DomainName;
        }
        public WindowsIdentity ImpersonationUser
        {
            get
            {
                return null;  // not use ImpersonationUser
            }
        }
        public ICredentials NetworkCredentials
        {
            get
            {

                // use NetworkCredentials
                return new NetworkCredential(_UserName, _PassWord, _DomainName);
            }
        }
        public bool GetFormsCredentials(out Cookie authCookie, out string user, out string password, out string authority)
        {

            // not use FormsCredentials unless you have implements a custom autentication.
            authCookie = null;
            user = password = authority = null;
            return false;
        }

    }

    #endregion
}
