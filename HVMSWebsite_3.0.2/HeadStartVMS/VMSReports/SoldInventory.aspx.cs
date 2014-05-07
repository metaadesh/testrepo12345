using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using METAOPTION.BAL;
using Microsoft.Reporting;
using Microsoft.Reporting.WebForms;
using METAOPTION.Reports;
using Telerik.Web.UI;
using System.Web.Security;

namespace METAOPTION.VMSReports
{
    public partial class SoldInventory : System.Web.UI.Page
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

        #region[Bind Years dropdownlist]
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

        #region[Year selected index change]
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Fetch Makes based on selected Year
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

        #region[Make selected index change]
        protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Fetch Body and Model for selected Make
            if (!string.IsNullOrEmpty(ddlMake.SelectedValue))
                BindModel(Convert.ToInt32(ddlMake.SelectedValue));
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
        }
        #endregion

        #region[Bind Buyer]
        protected void BindBuyers()
        {
            Int32 BuyerParentID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? -1 : Convert.ToInt32(Session["BuyerParent"]);
            if (Convert.ToString(Session["BuyerAccessLevel"]) == "1")
                ddlBuyers.DataSource = BAL.Common.GetMobileBuyerList_ver211(Convert.ToInt32(Session["empId"]), -1, Convert.ToInt32(Session["LoginEntityTypeID"]), Constant.OrgID);
            else
                ddlBuyers.DataSource = BAL.Common.GetMobileBuyerList_ver211(Convert.ToInt32(Session["empId"]), BuyerParentID, Convert.ToInt32(Session["LoginEntityTypeID"]), Constant.OrgID);
            ddlBuyers.DataValueField = "BuyerId";
            ddlBuyers.DataTextField = "BuyerName";
            ddlBuyers.DataBind();

            if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
            {
                if (ddlBuyers.Items.FindItemByValue(Convert.ToString(Session["UserEntityID"])) != null)
                {
                    ddlBuyers.SelectedValue = Convert.ToString(Session["UserEntityID"]);
                    ddlBuyers.Items.FindItemByValue(Convert.ToString(Session["UserEntityID"])).Checked = true;

                    DateTime DateFrom = System.DateTime.Today.AddDays(-7);
                    DateTime DateTo = System.DateTime.Today;
                    if (!String.IsNullOrEmpty(txtStartDate.Text))
                        DateFrom = Convert.ToDateTime(txtStartDate.Text);
                    if (!String.IsNullOrEmpty(txtEndDate.Text))
                        DateTo = Convert.ToDateTime(txtEndDate.Text);

                    // Bind dealer(s) affiliated with the selected buyer(s)
                    BindDealers(ddlBuyers.SelectedValue, DateFrom, DateTo);
                    // Bind customer(s) affiliated with the selected buyer(s)
                    BindCustomers(ddlBuyers.SelectedValue, DateFrom, DateTo);
                }
            }

            if (ddlBuyers.Items.Count == 1)
                ddlBuyers.Enabled = false;
        }
        #endregion

        #region[Buyer dropdown on item checked]
        protected void ddlBuyers_ItemChecked(object sender, RadComboBoxItemEventArgs e)
        {            
            String buyer = "";
            for (int i = 0; i < ddlBuyers.CheckedItems.Count(); i++)
                buyer += "," + ddlBuyers.CheckedItems[i].Value.ToString();


            DateTime DateFrom = System.DateTime.Today.AddDays(-7);
            DateTime DateTo = System.DateTime.Today;
            if (!String.IsNullOrEmpty(txtStartDate.Text))
                DateFrom = Convert.ToDateTime(txtStartDate.Text);
            if (!String.IsNullOrEmpty(txtEndDate.Text))
                DateTo = Convert.ToDateTime(txtEndDate.Text);

            // Bind dealer(s) affiliated with the selected buyer(s)
            BindDealers(buyer, DateFrom, DateTo);
            // Bind customer(s) affiliated with the selected buyer(s)
            BindCustomers(buyer, DateFrom, DateTo);
        }
        #endregion

        #region[View Report button event handler]
        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            String SystemID = Convert.ToString(Constant.SystemID);
            if (Session["SystemID"] != null)
                SystemID = Convert.ToString(Session["SystemID"]);

            //Purchased From(Dealer)
            ReportParameter paramDealer = new ReportParameter("PurchasedFrom");
            paramDealer.Values.AddRange(DealerIDs());
            //Sold To(Customer)
            ReportParameter paramCustomer = new ReportParameter("SoldTo");
            paramCustomer.Values.AddRange(CustomerIDs());

            ReportParameter[] parameters = new ReportParameter[14];
            parameters[0] = new ReportParameter("startdate", txtStartDate.Text.Trim());
            parameters[1] = new ReportParameter("enddate", txtEndDate.Text.Trim());
            parameters[2] = new ReportParameter("year", ddlYear.SelectedValue);
            parameters[3] = new ReportParameter("make", ddlMake.SelectedValue);
            parameters[4] = new ReportParameter("model", ddlModel.SelectedValue);
            parameters[5] = new ReportParameter("buyer", ddlBuyers.SelectedValue == "" ? "-1" : ddlBuyers.SelectedValue);
            parameters[6] = paramCustomer;
            parameters[7] = paramDealer;
            parameters[8] = new ReportParameter("soldstatus", ddlSoldStatus.SelectedValue);
            parameters[9] = new ReportParameter("groupby", ddlGroupBy.SelectedValue);
            parameters[10] = new ReportParameter("showcarnote", Convert.ToString(Convert.ToInt32(cbCarNote.Checked)));
            parameters[11] = new ReportParameter("UserId", Constant.UserId.ToString());
            parameters[12] = new ReportParameter("systemId", SystemID);
            parameters[13] = new ReportParameter("OrgID", Constant.OrgID.ToString());

            ReportParameters.Parameters = parameters;
            ReportParameters.ReportName = "/Hollenshead/SoldInventoryReport";

            Response.Redirect("Reports.aspx?ReturnURL=SoldInventory.aspx");
        }
        #endregion       

        #region[Bind dealers by buyer]
        private void BindDealers(String BuyerID,DateTime StartDate, DateTime EndDate)
        {
            List<InventoryDealer_ByBuyerIDResult> list = DealerCustomerBAL.InventoryDealer_ByBuyerID(BuyerID, StartDate, EndDate);
            chklstDealer.DataSource = list;
            chklstDealer.DataValueField = "DealerId";
            chklstDealer.DataTextField = "DealerName";
            chklstDealer.DataBind();
        }
        #endregion

        #region[Bind customers by buyer]
        private void BindCustomers(String BuyerID, DateTime StartDate, DateTime EndDate)
        {
            List<Inventory_Customer_ByBuyerIDResult> list = DealerCustomerBAL.Inventory_Customer_ByBuyerID(BuyerID, StartDate, EndDate);
            cblCustomer.DataSource = list;
            cblCustomer.DataValueField = "CustomerID";
            cblCustomer.DataTextField = "CustomerName";
            cblCustomer.DataBind();
        }
        #endregion

        #region[Purchased From]
        protected String[] DealerIDs()
        {
            String IDs = "";
            foreach (ListItem item in chklstDealer.Items)
            {
                if (item.Selected)
                    IDs += item.Value + ",";
            }

            if (String.IsNullOrEmpty(IDs))
                IDs = "-1,";

            IDs = IDs.TrimEnd(',');
            return IDs.Split(',');
        }
        #endregion

        #region[Sold To]
        protected String[] CustomerIDs()
        {
            String IDs = "";
            foreach (ListItem item in cblCustomer.Items)
            {
                if (item.Selected)
                    IDs += item.Value + ",";
            }

            if (String.IsNullOrEmpty(IDs))
                IDs = "-1,";

            IDs = IDs.TrimEnd(',');
            return IDs.Split(',');
        }
        #endregion
    }
}