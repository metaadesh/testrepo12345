using System;
using System.Collections;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

//Extended
using System.Collections.Generic;
using METAOPTION.BAL;
using MetaOption.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using METAOPTION.Reports;
using System.Web;
using System.Web.Security;

namespace METAOPTION.Reports
{
    public partial class ViewAllPayments : System.Web.UI.Page
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
            
           CheckPermission();

  
            if (!IsPostBack)
            {
                txtStartDate.Text = DateTime.Today.AddDays(-7).ToShortDateString();
                txtEndDate.Text = DateTime.Today.ToShortDateString();

                txtCheckStartDate.Text = DateTime.Today.AddDays(-7).ToShortDateString();
                txtCheckEndDate.Text = DateTime.Today.ToShortDateString();

                Util.setValue(ddlRecipientType, paymentBLL.GetRecipientType(), "EntityType1", "EntityTypeId");
                Util.setValue(this.ddlAccountNumber, paymentBLL.GetActiveAccounts(), "AccountNoWithBankInfo", "AccountNumber");
            }
        }
        /// <summary>
        /// Check Permission
        /// </summary>
        private void CheckPermission()
        {
            List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, "PAYMENT");

            if (!dict.Contains("ALLPAYMENT.VIEW"))
                Response.Redirect("Permission.aspx?MSG=ALLPAYMENT.VIEW", true);
        }
        /// <summary>
        /// Bind Data
        /// </summary>
        private void BindData()
        {
            List<GetRecipientsResult> receipents = GetFilteredRecipients();

            // Fill Recipients in Grid View
            Util.setValue(grvRecipient, receipents);
        }
       ///// <summary>
       ///// Get list of Filtered Receipients
       ///// </summary>
       ///// <returns></returns>
       // private List<GetRecipientsResult> GetFilteredRecipients()
       // {
       //     String searchField = Util.getValue(ddlSearchField);
       //     String searchOperator = Util.getValue(ddlSearchOperator);
       //     String searchString = Util.getValue(txtSearchString).ToLower();

       //     int receipentTypeId = int.Parse(Util.getValue(ddlRecipientType));
       //     if (receipentTypeId == 0)
       //         return null;

       //     List<GetRecipientsResult> receipents = paymentBLL.GetRecipients(receipentTypeId);

       //     var query = from p in receipents
       //                 select p;

       //     switch (searchField.ToLower())
       //     {
       //         case "accountingcode":
       //             if (searchOperator == "begins with")
       //                 query = query.Where(c => c.accountingcode.ToLower().StartsWith(searchString));
       //             if (searchOperator == "ends with")
       //                 query = query.Where(c => c.accountingcode.ToLower().EndsWith(searchString));
       //             if (searchOperator == "contains")
       //                 query = query.Where(c => c.accountingcode.ToLower().Contains(searchString));
       //             break;

       //         case "recipientname":
       //             if (searchOperator == "begins with")
       //                 query = query.Where(c => c.recipientname.ToLower().StartsWith(searchString));
       //             if (searchOperator == "ends with")
       //                 query = query.Where(c => c.recipientname.ToLower().EndsWith(searchString));
       //             if (searchOperator == "contains")
       //                 query = query.Where(c => c.recipientname.ToLower().Contains(searchString));
       //             break;

       //         case "street":
       //             if (searchOperator == "begins with")
       //                 query = query.Where(c => c.Street.ToLower().StartsWith(searchString));
       //             if (searchOperator == "ends with")
       //                 query = query.Where(c => c.Street.ToLower().EndsWith(searchString));
       //             if (searchOperator == "contains")
       //                 query = query.Where(c => c.Street.ToLower().Contains(searchString));
       //             break;

       //         case "city":
       //             if (searchOperator == "begins with")
       //                 query = query.Where(c => c.City.ToLower().StartsWith(searchString));
       //             if (searchOperator == "ends with")
       //                 query = query.Where(c => c.City.ToLower().EndsWith(searchString));
       //             if (searchOperator == "contains")
       //                 query = query.Where(c => c.City.ToLower().Contains(searchString));
       //             break;
       //     }

       //     return query.ToList();
       // }

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

            switch (searchOperator.ToLower())
            {
                case "accountingcode":
                    if (searchField == "begins with")
                        query = query.Where(c => c.accountingcode.ToLower().StartsWith(searchString));
                    if (searchField == "ends with")
                        query = query.Where(c => c.accountingcode.ToLower().EndsWith(searchString));
                    if (searchField == "contains")
                        query = query.Where(c => c.accountingcode.ToLower().Contains(searchString));
                    break;

                case "recipientname":
                    if (searchField == "begins with")
                        query = query.Where(c => c.recipientname.ToLower().StartsWith(searchString));
                    if (searchField == "ends with")
                        query = query.Where(c => c.recipientname.ToLower().EndsWith(searchString));
                    if (searchField == "contains")
                        query = query.Where(c => c.recipientname.ToLower().Contains(searchString));
                    break;

                case "street":
                    if (searchField == "begins with")
                        query = query.Where(c => c.Street.ToLower().StartsWith(searchString));
                    if (searchField == "ends with")
                        query = query.Where(c => c.Street.ToLower().EndsWith(searchString));
                    if (searchField == "contains")
                        query = query.Where(c => c.Street.ToLower().Contains(searchString));
                    break;

                case "city":
                    if (searchField == "begins with")
                        query = query.Where(c => c.City.ToLower().StartsWith(searchString));
                    if (searchField == "ends with")
                        query = query.Where(c => c.City.ToLower().EndsWith(searchString));
                    if (searchField == "contains")
                        query = query.Where(c => c.City.ToLower().Contains(searchString));
                    break;
            }

            return query.ToList();
        }
        
        
        /// <summary>
        /// Handle gridview paging event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grvRecipient_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvRecipient.PageIndex = e.NewPageIndex;
            BindData();

            mpeSelectRecipient.Show();
        }

        protected void lnkSelect_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Handle sorting event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
       /// <summary>
       /// Sort gridview
       /// </summary>
       /// <param name="sortExpression"></param>
       /// <param name="direction"></param>
        private void SortGridView(string sortExpression, string direction)
        {
            IEnumerable<GetRecipientsResult> results = GetFilteredRecipients().Sort<GetRecipientsResult>(sortExpression + direction);

            // Fill Recipients in Grid View
            Util.setValue(grvRecipient, results.ToList());
            mpeSelectRecipient.Show();
        }
             
       /// <summary>
       /// Handle click event of Dropdownlist for receipient type change
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
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
        /// <summary>
        /// Select Receipent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Handle click event of Search button(for selected filters)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            BindData();
            mpeSelectRecipient.Show();
        }

       
       
        /// <summary>
        /// Handle click event of ViewAllPayment button,Open Report Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnViewAllPayments_Click(object sender, EventArgs e)
        {
            ReportParameter[] parameters = new ReportParameter[13];
            parameters[0] = new ReportParameter("startdate", txtStartDate.Text.Trim());
            parameters[1] = new ReportParameter("enddate", txtEndDate.Text.Trim());
            parameters[2] = new ReportParameter("entitytypeid", ddlRecipientType.SelectedValue);
            parameters[3] = new ReportParameter("entityid", hdnSelectedRecipientID.Value);
            parameters[4] = new ReportParameter("checknumber", txtCheckNumber.Text.Trim() == "" ? "-1" : txtCheckNumber.Text);
            parameters[5] = new ReportParameter("checkdatemin", txtCheckStartDate.Text.Trim());
            parameters[6] = new ReportParameter("checkdatemax", txtCheckEndDate.Text.Trim());
            parameters[7] = new ReportParameter("amountmin", txtMinAmount.Text.Trim() == "" ? "-1" : txtMinAmount.Text);
            parameters[8] = new ReportParameter("amountmax", txtMaxAmount.Text.Trim() == "" ? "-1" : txtMaxAmount.Text);
            parameters[9] = new ReportParameter("invoicenumber", txtInvoiceNumber.Text.Trim() == "" ? "-1" : txtInvoiceNumber.Text);
            parameters[10] = new ReportParameter("accountnumber", string.IsNullOrEmpty(ddlAccountNumber.SelectedValue) ? "-1" : ddlAccountNumber.SelectedValue);
            parameters[11] = new ReportParameter("UserId", Constant.UserId.ToString());
            parameters[12] = new ReportParameter("systemId", Session["SystemID"].ToString());

            
            //Set Report Parameters
            ReportParameters.Parameters = parameters;

            //Specify Report Name
            ReportParameters.ReportName = "/Hollenshead/ViewALLPaymentsMade";
            Response.Redirect("Reports.aspx?ReturnURL=ViewAllPayments.aspx");

     

        }
    }
}
