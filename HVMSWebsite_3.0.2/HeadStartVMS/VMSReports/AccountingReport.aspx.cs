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
    public partial class AccountingReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckPermission();
                this.txtFromDate.Text = DateTime.Now.AddYears(-1).ToShortDateString();
                this.txtToDate.Text = DateTime.Today.ToShortDateString();
            }
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            TimeSpan diff = Convert.ToDateTime(txtToDate.Text) - Convert.ToDateTime(txtFromDate.Text);
            double var = diff.TotalDays;
            if (var > 365.0 * 2)
            {
                ltErr.Text = "* Date difference cannot be more than 2 years";
                return;
            }
            else if (var < 0.0)
            {
                ltErr.Text = "* Invalid date range";
                return;
            }
            else
            {


                ReportParameter[] parameters = new ReportParameter[5];
                parameters[0] = new ReportParameter("startdate", txtFromDate.Text.Trim());
                parameters[1] = new ReportParameter("enddate", txtToDate.Text.Trim());
                parameters[2] = new ReportParameter("UserId", Constant.UserId.ToString());
                parameters[3] = new ReportParameter("systemId", Session["SystemID"].ToString());
                parameters[4] = new ReportParameter("OrgID", Constant.OrgID.ToString());

                ReportParameters.Parameters = parameters;
                ReportParameters.ReportName = "/Hollenshead/AccountingReport";
                //ReportParameters.ReportName = "/HeadStartVMS/AccountingReport";
                Response.Redirect("Reports.aspx?ReturnURL=AccountingReport.aspx");
            }
        }

        #region[Check Permission method]
        /// <summary>
        /// Method that check permision
        /// of User.
        /// </summary>
        private void CheckPermission()
        {
            List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, "REPORTS");
            if (!(dict.Contains("ACCOUNTINGREPORT.VIEW")))
                Response.Redirect("../UI/Permission.aspx?MSG=REPORTS.ACCOUNTINGREPORT.VIEW");


        }
        #endregion

       
    }
}
