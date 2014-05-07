using System;
using System.Collections;
using System.Linq;
using System.Web.UI.WebControls;

//Extended
using System.Collections.Generic;
using METAOPTION.BAL;
using MetaOption.Web.UI.WebControls;
using System.Web.Security;

namespace METAOPTION.UI
{
    public partial class MakeANewBuyerPayment : System.Web.UI.Page
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
            PageLoad();
        }

        private void CheckPermission()
        {             
            List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, "PAYMENT");

            if (!dict.Contains("MAKEANEWPAYMENT.ADD"))
                Response.Redirect("Permission.aspx?MSG=MAKEANEWPAYMENT.ADD",true);
        }

        private void PageLoad()
        {
            txtCheckDate.Attributes.Add("readonly", "true");
            Int32 recipientId = 0;
            Int32 receipentTypeId = 0;

            if (Request.QueryString["EntityId"] != null && Request.QueryString["EntityId"].ToString() != String.Empty)
            {
                recipientId = Int32.Parse(Request.QueryString["EntityId"].ToString());
                hdnSelectedRecipientID.Value = recipientId.ToString();
            }
            if (Request.QueryString["type"] != null && Request.QueryString["type"].ToString() != String.Empty)
            { 
                receipentTypeId = Int32.Parse(Request.QueryString["type"].ToString());

                 // Added by Ali to redirect if there is any return url
                if (Request["ReturnURL"] != null)
                   Response.Redirect(Convert.ToString(Request["ReturnURL"]));

                if (receipentTypeId == 4 || recipientId == 0)
                    Response.Redirect("Payments.aspx", true);

                List<GetRecipientsResult> receipents = paymentBLL.GetRecipients(receipentTypeId, Constant.OrgID);

                var result = (from p in receipents
                              where p.recipientid == recipientId 
                              select p);

                GetRecipientsResult  receipent = result.Single();  
                ddlRecipientType.SelectedValue = receipentTypeId.ToString();
                hdnAccountingCode.Value = receipent.accountingcode;
                lblSelectedRecipientName.Text = receipent.recipientname;

                ddlRecipientType.SelectedValue = receipentTypeId.ToString();
                ddlRecipientType.Enabled = false;
                lnkSelect.Visible = false;

                if (Request.QueryString["ExpenseId"] != null && Request.QueryString["ExpenseId"].ToString() != String.Empty)
                {
                    Int32 expenseId = Convert.ToInt32(Request.QueryString["ExpenseId"].ToString()); 
                    PopulateExpenseAmount(receipentTypeId, recipientId, expenseId);
                }
            }
                     
                         
            lblError.Visible = false;
            if (Session["NewPaymentValidationErr"] != null && Session["NewPaymentValidationErr"].ToString() != String.Empty)
            {
                lblError.Text = Session["NewPaymentValidationErr"].ToString()+ " Please fill the values properly for add to payment.";
                lblError.Visible = true;
                Session.Remove("NewPaymentValidationErr");
            }
                          
            if (!IsPostBack)
            {
                Session["ApplyPaymentStep"] = null;

                //Below line Commented by Nitin Paliwal,Nov 25 2009
                //Set Buyer as default value in recipient type dropdownlist
                //Util.setValue(ddlRecipientType, paymentBLL.GetRecipientType(), "EntityType1", "EntityTypeId");
                ListItem lstItem = new ListItem();
                lstItem.Text = "Buyer";
                lstItem.Value = "2";
                ddlRecipientType.Items.Add(lstItem);
                ddlRecipientType.SelectedIndex = 0;

                Util.setValue(this.ddlAccountNumber, paymentBLL.GetActiveAccounts(), "AccountNoWithBankInfo", "AccNoWithBankAccID");

                try
                {
                    Int64 checckNo = (Int64)paymentBLL.GetNextCheckNo().Single().NextCheckNo;
                    txtCheckNumber.Text = checckNo.ToString();
                }
                catch { }

                if (recipientId == 0 || receipentTypeId == 0)
                    BindFilterData(); 
            } 
        }

        private void PopulateExpenseAmount(int entityTypeId, int entityId, int expenseId )
        {
            List<GetOpenExpenseListResult> expenses = paymentBLL.GetOpenExpenseList(entityId,entityTypeId);

            var result = (from p in expenses
                          where p.ObjectId == expenseId
                          select p);

            GetOpenExpenseListResult expense = result.Single();
            Util.setValue(txtAmount, expense.ExpenseAmount);
        }

        private List<GetRecipientsResult> GetFilteredRecipients()
        {
            String searchField = Util.getValue(ddlSearchField);
            String searchOperator = Util.getValue(ddlSearchOperator);
            String searchString = Util.getValue(txtSearchString).ToLower();

            int receipentTypeId = int.Parse(Util.getValue(ddlRecipientType));
            if (receipentTypeId == 0)
                return null;

            List<GetRecipientsResult> receipents = paymentBLL.GetRecipients(receipentTypeId, Constant.OrgID);

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

        private void BindFilterData()
        {

            List<GetRecipientsResult> receipents = GetFilteredRecipients();

            // Fill Recipients in Grid View
            Util.setValue(grvRecipient, receipents);
        }
         
        protected void grvRecipient_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvRecipient.PageIndex = e.NewPageIndex;
            //BindData();
            BindFilterData();

            mpeSelectRecipient.Show();
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
             
        protected void lnkSelect_Click(object sender, EventArgs e)
        {

        }
         
        protected void ddlRecipientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Initialize Controls
            txtSearchString.Text = "";
            hdnSelectedRecipientID.Value = "";
            lblSelectedRecipientName.Text = "";
            hdnAccountingCode.Value = "";

            String selectedRecipientType = Util.getValue(ddlRecipientType, "SelectedText");
            lblRecipientType.Text = (selectedRecipientType != String.Empty) ? "Select " + selectedRecipientType : "Recipient(s)";

            BindFilterData();
          
            /*
            if (ddlRecipientType.SelectedValue == "4")
                Response.Redirect("Payments.aspx");
            else
            {
                BindFilterData(); 
            }
            */ 
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
                    hdnAccountingCode.Value = ((HiddenField) grvRecipient.Rows[row.RowIndex].FindControl("hdnAccCode")).Value.Trim();
                }
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            BindFilterData();
            mpeSelectRecipient.Show();
        }
       
    }
}
