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
    public partial class UnPaidReport : System.Web.UI.Page
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
            if (!Page.IsPostBack)
            {
                this.txtStartDate.Text = DateTime.Today.AddDays(-7).ToShortDateString();
                this.txtEndDate.Text = DateTime.Today.ToShortDateString();
                BindYears();
                BindBuyers();
            }
        }
        #region Bind Years
        /// <summary>
        /// Bind Years in dropdownlist
        /// </summary>
        private void BindYears()
        {
            /*Load Years*/
            ddlYear.DataSource = Common.GetYearList();
            ddlYear.DataValueField = "Year";
            ddlYear.DataTextField = "Year";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, new ListItem("ALL", "-1"));
            ddlYear.SelectedIndex = 0;

            //Bind Make Details for Selected Year
            if (!string.IsNullOrEmpty(ddlYear.SelectedValue))
                BindMake(Convert.ToInt32(ddlYear.SelectedValue));


        }
        #endregion

        #region Bind Make
        /// <summary>
        /// Bind Dropdownlist with make details based on year selected
        /// </summary>
        private void BindMake(int year)
        {
            /*Bind Make DropDownlist*/
            ddlMake.DataSource = Common.GetMakes(year);
            ddlMake.DataTextField = "Make";
            ddlMake.DataValueField = "MakeId";
            ddlMake.DataBind();
            ddlMake.Items.Insert(0, new ListItem("ALL", "-1"));
            //Bind Model this selected Make
            if (!string.IsNullOrEmpty(ddlMake.SelectedValue))
            {
                BindModel(Convert.ToInt32(ddlMake.SelectedValue));
            }
        }

        #endregion

        #region Bind Model
        /// <summary>
        /// Bind dropdownlist with Model details based on make selected
        /// </summary>
        private void BindModel(int makeId)
        {
            /*Bind Model DropDownlist*/
            ddlModel.DataSource = BAL.Common.GetModel(makeId);
            ddlModel.DataTextField = "Model";
            ddlModel.DataValueField = "ModelId";
            ddlModel.DataBind();
            ddlModel.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        #endregion

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


        #region Year selected index change
        /// <summary>
        /// Year selected index change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Fetch Makes based on selected Year
            if (!string.IsNullOrEmpty(ddlYear.SelectedValue))
                BindMake(Convert.ToInt32(ddlYear.SelectedValue));
        }
        #endregion

        #region Make selected index change
        /// <summary>
        /// Make selected index change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Fetch Body and Model for selected Make
            if (!string.IsNullOrEmpty(ddlMake.SelectedValue))
                BindModel(Convert.ToInt32(ddlMake.SelectedValue));
        }
        #endregion

        /// <summary>
        /// Render report according to the report name and parameters
        /// </summary>
        /// <param name="prams"></param>
        /// <param name="reportName"></param>
        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            ReportParameter[] parameters = new ReportParameter[9];
            parameters[0] = new ReportParameter("startdate", txtStartDate.Text.Trim());
            parameters[1] = new ReportParameter("enddate", txtEndDate.Text.Trim());
            parameters[2] = new ReportParameter("year", ddlYear.SelectedValue);
            parameters[3] = new ReportParameter("make", ddlMake.SelectedValue);
            parameters[4] = new ReportParameter("model", ddlModel.SelectedValue);
            parameters[5] = new ReportParameter("UserId", Constant.UserId.ToString());
            parameters[6] = new ReportParameter("carstatus", ddlCarStatus.SelectedValue);
            parameters[7] = new ReportParameter("buyer", ddlBuyers.SelectedValue);
            parameters[8] = new ReportParameter("systemId", Session["SystemID"].ToString());

            ReportParameters.Parameters = parameters;
            ReportParameters.ReportName = "/Hollenshead/NoPaidReport";

            Response.Redirect("Reports.aspx?ReturnURL=UnPaidReport.aspx");

        }
    }
}
