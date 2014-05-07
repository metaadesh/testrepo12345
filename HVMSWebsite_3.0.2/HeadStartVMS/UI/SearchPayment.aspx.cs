using System;
using System.Collections;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

//Extended
using System.Collections.Generic;
using METAOPTION.BAL;
using MetaOption.Web.UI.WebControls;
using System.Web.Security;
using System.Data;

namespace METAOPTION.UI
{
    public partial class SearchPayment : System.Web.UI.Page
    { 
        PaymentBLL paymentBLL = new PaymentBLL();
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["MasterPage"])))
                    this.MasterPageFile = Convert.ToString(Session["MasterPage"]);
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
            CheckPermission();

            if (!IsPostBack)
            {
                Util.setValue(ddlRecipientType, paymentBLL.GetRecipientType(), "EntityType1", "EntityTypeId");
                Util.setValue(this.ddlAccountNumber, paymentBLL.GetActiveAccounts(), "AccountNoWithBankInfo", "AccountNumber");
                if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
               {
                   if (ddlRecipientType.Items.FindByValue(Convert.ToString(Session["LoginEntityTypeID"])) != null)
                {
                    ddlRecipientType.SelectedValue = Convert.ToString(Session["LoginEntityTypeID"]);
                    ddlRecipientType.Enabled = false;
                }
                   BindData();
               }

            }
        }

        private void CheckPermission()
        {
            List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, "PAYMENT");

            if (!dict.Contains("ALLPAYMENT.VIEW"))
                Response.Redirect("Permission.aspx?MSG=ALLPAYMENT.VIEW", true);
        }

        private void BindData()
        {
            List<GetRecipientsResult> receipents = GetFilteredRecipients();

            // Fill Recipients in Grid View
            Util.setValue(grvRecipient, receipents);
        }

        private List<GetRecipientsResult> GetFilteredRecipients()  
        {
            String BuyerParentId = "-1";
            if (Convert.ToString(Session["BuyerAccessLevel"]) != "1")
                BuyerParentId = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString();           
          
            String searchField = Util.getValue(ddlSearchField);
            String searchOperator = Util.getValue(ddlSearchOperator);
            String searchString = Util.getValue(txtSearchString).ToLower();

            int receipentTypeId = int.Parse(Util.getValue(ddlRecipientType));
            if (receipentTypeId == 0)
                return null;
            List<GetRecipientsResult> receipents =null;
            if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
            {
                receipents = paymentBLL.GetRecipients_ByEntityTypeID(receipentTypeId, Convert.ToInt64(Session["UserEntityID"]), Convert.ToInt64(BuyerParentId));
            }
            else
                receipents = paymentBLL.GetRecipients(receipentTypeId,Constant.OrgID);
            var query = from p in receipents
                        select p;

            switch (searchField.ToLower())
            {
                case "accountingcode":
                    if (searchOperator == "begins with")
                        query = query.Where(c => c.accountingcode.ToLower().StartsWith(searchString));
                    if (searchOperator == "ends with")
                        query = query.Where(c => c.accountingcode.ToLower().EndsWith(searchString));
                    if (searchOperator == "contains")
                        query = query.Where(c => c.accountingcode.ToLower().Contains(searchString));
                    break;

                case "recipientname":
                    if (searchOperator == "begins with")
                        query = query.Where(c => c.recipientname.ToLower().StartsWith(searchString));
                    if (searchOperator == "ends with")
                        query = query.Where(c => c.recipientname.ToLower().EndsWith(searchString));
                    if (searchOperator == "contains")
                        query = query.Where(c => c.recipientname.ToLower().Contains(searchString));
                    break;

                case "street":
                    if (searchOperator == "begins with")
                        query = query.Where(c => c.Street.ToLower().StartsWith(searchString));
                    if (searchOperator == "ends with")
                        query = query.Where(c => c.Street.ToLower().EndsWith(searchString));
                    if (searchOperator == "contains")
                        query = query.Where(c => c.Street.ToLower().Contains(searchString));
                    break;

                case "city":
                    if (searchOperator == "begins with")
                        query = query.Where(c => c.City.ToLower().StartsWith(searchString));
                    if (searchOperator == "ends with")
                        query = query.Where(c => c.City.ToLower().EndsWith(searchString));
                    if (searchOperator == "contains")
                        query = query.Where(c => c.City.ToLower().Contains(searchString));
                    break;
            }

            return query.ToList();
        }

        protected void grvRecipient_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvRecipient.PageIndex = e.NewPageIndex;
            BindData();

            mpeSelectRecipient.Show();
        }

        protected void lnkSelect_Click(object sender, EventArgs e)
        {

        }

        protected void grvRecipient_OnSorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, ASCENDING);
            }
        }

        private void SortGridView(string sortExpression, string direction)
        {
            IEnumerable<GetRecipientsResult> results = GetFilteredRecipients().Sort<GetRecipientsResult>(sortExpression + direction);

            // Fill Recipients in Grid View
            Util.setValue(grvRecipient, results.ToList());
            mpeSelectRecipient.Show();
        }
             

        protected void ddlRecipientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Initialize Controls
            txtSearchString.Text = "";
            hdnSelectedRecipientID.Value = "0";
            lblSelectedRecipientName.Text = "";
            hdnAccountingCode.Value = "";

            String selectedRecipientType = Util.getValue(ddlRecipientType, "SelectedText");
            lblRecipientType.Text = (selectedRecipientType != String.Empty) ? "Select " + selectedRecipientType : "Recipient(s)";
            
            BindData();
        }

        protected void btnSelectRecipient_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvRecipient.Rows)
            {
                GroupRadioButton selectedRadioButton = row.FindControl("selectedRadioButton") as GroupRadioButton;

                if (selectedRadioButton != null && selectedRadioButton.Checked)
                {
                    hdnSelectedRecipientID.Value = grvRecipient.DataKeys[row.RowIndex].Value.ToString();
                    String recipientName = grvRecipient.Rows[row.RowIndex].Cells[3].Text.Trim();

                    lblSelectedRecipientName.Text = recipientName;
                    hdnAccountingCode.Value = ((HiddenField)grvRecipient.Rows[row.RowIndex].FindControl("hdnAccCode")).Value.Trim();
                }
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            BindData();
            mpeSelectRecipient.Show();
        }

        protected void btnSearchPayments_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnClosePopup_Click(object sender, ImageClickEventArgs e)
        {

        }
    }
}
