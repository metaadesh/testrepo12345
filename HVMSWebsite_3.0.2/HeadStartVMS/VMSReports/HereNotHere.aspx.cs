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
using System.Xml.Linq;
using Microsoft.Reporting;
using Microsoft.Reporting.WebForms;
using METAOPTION.UI;
using METAOPTION.BAL;

namespace METAOPTION.Reports
{
    public partial class HereNotHere : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            ReportParameter[] parameters = new ReportParameter[7];
            parameters[0] = new ReportParameter("startdate", txtStartDate.Text.Trim());
            parameters[1] = new ReportParameter("enddate",  txtEndDate.Text.Trim());
            parameters[2] = new ReportParameter("designationID", ddlNotHereStatus.SelectedValue);
            parameters[3] = new ReportParameter("price",ddlPrice.SelectedValue);
            parameters[4] = new ReportParameter("UserId", Constant.UserId.ToString());
            parameters[5] = new ReportParameter("systemId", Convert.ToString(Session["SystemId"]));
            parameters[6] = new ReportParameter("OrgID", Constant.OrgID.ToString());

            ReportParameters.Parameters = parameters;
            ReportParameters.ReportName = "/Hollenshead/HereNotHereReport";
            Response.Redirect("Reports.aspx?ReturnURL=HereNotHere.aspx");

        }
    }
}
