using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using METAOPTION.BAL;
using Microsoft.Reporting.WebForms;
using System.Web.Security;

namespace METAOPTION.Reports
{
    public partial class BuyerInventory : System.Web.UI.Page
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
                BindYears();
                BindBuyers();
            }

        }
        #region[Button view report click event]
        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            String startDate = txtFromDate.Text.Trim();
            String endDate = txtToDate.Text.Trim();
            if (!String.IsNullOrEmpty(startDate) && String.IsNullOrEmpty(endDate)) endDate = startDate;
            if (!String.IsNullOrEmpty(endDate) && String.IsNullOrEmpty(startDate)) startDate = endDate;
            if (String.IsNullOrEmpty(startDate)) startDate = "01/01/1900";
            if (String.IsNullOrEmpty(endDate)) endDate = DateTime.Today.Date.ToString();

            ReportParameter[] parameters = new ReportParameter[12];
            parameters[0] = new ReportParameter("startdate", startDate);
            parameters[1] = new ReportParameter("enddate", endDate);
            parameters[2] = new ReportParameter("year", ddlYear.SelectedValue);
            parameters[3] = new ReportParameter("make", ddlMake.SelectedValue);
            parameters[4] = new ReportParameter("model", ddlModel.SelectedValue);
            parameters[5] = new ReportParameter("buyer", ddlBuyer.SelectedValue);
            parameters[6] = new ReportParameter("soldstatus", ddlSoldStatus.SelectedValue);
            parameters[7] = new ReportParameter("carstatus", ddlCarStatus.SelectedValue);
            parameters[8] = new ReportParameter("title", ddlTitlePresent.SelectedValue);
            parameters[9] = new ReportParameter("UserId", Constant.UserId.ToString());
            parameters[10] = new ReportParameter("systemId", Session["SystemID"].ToString());
            parameters[11] = new ReportParameter("OrgID", Constant.OrgID.ToString());

            ReportParameters.Parameters = parameters;
            ReportParameters.ReportName = "/Hollenshead/BuyerInventoryReport";
            //ReportParameters.ReportName = "/HeadStartVMS/BuyerInventoryReport";

            Response.Redirect("Reports.aspx?ReturnURL=BuyerInventory.aspx");
        }
        #endregion
        #region[Year selected index changed]
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlYear.SelectedValue))
                BindMake(Convert.ToInt32(ddlYear.SelectedValue));
        }
        #endregion
        #region[Make selected index changed]
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
        #region[Bind Years]
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
        #region[Bind Make]
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
        #region[Bind Model]
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

            // Bind body upon selecting model
            // if (!string.IsNullOrEmpty(ddlModel.SelectedValue))
            //{
            //    BindBody(Convert.ToInt32(ddlModel.SelectedValue));
            //}
        }
        #endregion
        #region[Bind Buyers ]
        protected void BindBuyers()
        {
            //ddlBuyer.DataSource = Common.GetBuyerList();
            //ddlBuyer.DataValueField = "BuyerId";
            //ddlBuyer.DataTextField = "BuyerName";
            //ddlBuyer.DataBind();

            //ddlBuyer.Items.Insert(0, new ListItem("ALL", "-1"));
            //ddlBuyer.SelectedValue = "-1";

            Int32 BuyerParentID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? -1 : Convert.ToInt32(Session["BuyerParent"]);
            if (Convert.ToString(Session["BuyerAccessLevel"]) == "1")
                ddlBuyer.DataSource = BAL.Common.GetMobileBuyerList_ver211(Convert.ToInt32(Session["empId"]), -1, Convert.ToInt32(Session["LoginEntityTypeID"]), Constant.OrgID);
            else
                ddlBuyer.DataSource = BAL.Common.GetMobileBuyerList_ver211(Convert.ToInt32(Session["empId"]), BuyerParentID, Convert.ToInt32(Session["LoginEntityTypeID"]), Constant.OrgID);
            ddlBuyer.DataValueField = "BuyerId";
            ddlBuyer.DataTextField = "BuyerName";
            ddlBuyer.DataBind();

            // If buyer logs in, show the logged in buyer as selected in ddl
            if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
                if (ddlBuyer.Items.FindByValue(Convert.ToString(Session["UserEntityID"])) != null)
                    ddlBuyer.SelectedValue = Convert.ToString(Session["UserEntityID"]);

            // Disable ddl if it has only one item
            if (ddlBuyer.Items.Count == 1)
                ddlBuyer.Enabled = false;
            //else insert ALL at -1 position
            else if (ddlBuyer.Items.Count > 1)
                ddlBuyer.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        #endregion

    }
}
