using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using METAOPTION.Reports;
using METAOPTION.BAL;
using System.Web.Security;

namespace METAOPTION.VMSReports
{
    public partial class AvgCommissionReport : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["ReportMaster"])))
                    this.MasterPageFile = Convert.ToString(Session["ReportMaster"]);
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
            if (!IsPostBack)
            {
               // CheckPermission();
                BindBuyers();
                this.txtFromDate.Text = DateTime.Now.AddMonths(-3).ToShortDateString();
                this.txtToDate.Text = DateTime.Today.ToShortDateString();
            }
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {



            ReportParameter[] parameters = new ReportParameter[5];
            parameters[0] = new ReportParameter("startdate", txtFromDate.Text.Trim());
            parameters[1] = new ReportParameter("enddate", txtToDate.Text.Trim());
            parameters[2] = new ReportParameter("UserId", Constant.UserId.ToString());
            parameters[3] = new ReportParameter("BUYERID", ddlBuyers.SelectedValue);
            parameters[4] = new ReportParameter("OrgID", Constant.OrgID.ToString());

            ReportParameters.Parameters = parameters;
            ReportParameters.ReportName = "/Hollenshead/AverageCommissionReport";
            //ReportParameters.ReportName = "/HeadStartVMS/AverageCommissionReport";
            Response.Redirect("Reports.aspx?ReturnURL=AvgCommissionReport.aspx");
        }
        #region [ Bind Buyer ]
        protected void BindBuyers()
        {
            //ddlBuyers.DataSource = Common.GetBuyerList();
            //ddlBuyers.DataValueField = "BuyerId";
            //ddlBuyers.DataTextField = "BuyerName";
            //ddlBuyers.DataBind();
            //ddlBuyers.Items.Insert(0, new ListItem("ALL", "-1"));
            //ddlBuyers.SelectedValue = "-1";

            Int32 BuyerParentID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? -1 : Convert.ToInt32(Session["BuyerParent"]);
            if (Convert.ToString(Session["BuyerAccessLevel"]) == "1")
                ddlBuyers.DataSource = BAL.Common.GetMobileBuyerList_ver211(Convert.ToInt32(Session["empId"]), -1, Convert.ToInt32(Session["LoginEntityTypeID"]), Constant.OrgID);
            else
                ddlBuyers.DataSource = BAL.Common.GetMobileBuyerList_ver211(Convert.ToInt32(Session["empId"]), BuyerParentID, Convert.ToInt32(Session["LoginEntityTypeID"]), Constant.OrgID);
            ddlBuyers.DataValueField = "BuyerId";
            ddlBuyers.DataTextField = "BuyerName";
            ddlBuyers.DataBind();

            // If buyer logs in, show the logged in buyer as selected in ddl
            if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
                if (ddlBuyers.Items.FindByValue(Convert.ToString(Session["UserEntityID"])) != null)
                    ddlBuyers.SelectedValue = Convert.ToString(Session["UserEntityID"]);

            // Disable ddl if it has only one item
            if (ddlBuyers.Items.Count == 1)
                ddlBuyers.Enabled = false;
            //else insert ALL at -1 position
            else if (ddlBuyers.Items.Count > 1)
                ddlBuyers.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        #endregion
        //private void CheckPermission()
        //{
        //    List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, "REPORTS");
        //    if (!(dict.Contains("ACCOUNTINGREPORT.VIEW")))
        //        Response.Redirect("../UI/Permission.aspx?MSG=REPORTS.ACCOUNTINGREPORT.VIEW");


        //}
    }
}
