using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using METAOPTION.BAL;

namespace METAOPTION.Reports
{
    public partial class NormanReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //CheckPermission();
                this.txtDate.Text = DateTime.Today.ToShortDateString();
            }
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("SaleDate", txtDate.Text.Trim());
            parameters[1] = new ReportParameter("OrgID", Constant.OrgID.ToString());

            ReportParameters.Parameters = parameters;
            ReportParameters.ReportName = "/Hollenshead/NormanReport";
            //ReportParameters.ReportName = "/HeadStartVMS/NormanReport";
            Response.Redirect("Reports.aspx?ReturnURL=NormanReport.aspx");
        }

        #region[Check Permission method]
        /// <summary>
        /// Method that check permision
        /// of User.
        /// </summary>
        private void CheckPermission()
        {
            List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, "REPORTS");
            if (!(dict.Contains("NORMANREPORT.VIEW")))
                Response.Redirect("../UI/Permission.aspx?MSG=REPORTS.NORMANREPORT.VIEW");


        }
        #endregion
    }
}
