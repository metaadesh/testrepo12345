using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Configuration;
using System.IO;

//Extended
using System.Collections.Generic;
using METAOPTION.BAL;
using METAOPTION.PeachtreeService;
using MetaOption.Web.UI.WebControls;
using System.Web.Security;

namespace METAOPTION.UI
{
    public partial class ApplyBuyerPayment : System.Web.UI.Page
    {
        PaymentBLL paymentBLL = new PaymentBLL();
        private double _totalSelectedExpense = 0.00;
        CultureInfo ci = new CultureInfo("en-US");
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

        public DataTable SelectedExpenseTable
        {
            get
            {
                if (ViewState["dtSelectedExpense" + Constant.UserId.ToString()] == null)
                    ViewState["dtSelectedExpense" + Constant.UserId.ToString()] = CreateSelectedExpenseTable();

                return (DataTable)ViewState["dtSelectedExpense" + Constant.UserId.ToString()];
            }
            set { ViewState["dtSelectedExpense" + Constant.UserId.ToString()] = value; }
        }

        private Boolean ShowExpenseList
        {
            get;
            set;
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
            lblError.Visible = false;
            
            BindDataFromPrevPage();

          
            if(string.IsNullOrEmpty(lblTotalOutstanding.Text))
                lblTotalOutstanding.Text="0.00";
            
            if (string.IsNullOrEmpty(lblTotalSelExpAmount.Text))
                lblTotalSelExpAmount.Text = "0.00";

            if(string.IsNullOrEmpty(lblCurrentOutstanding.Text))
                lblCurrentOutstanding.Text = String.Format("{0:C}", 0);

            //lblTotalOutstanding.Text = ( Convert.ToDecimal(lblTotalOutstanding.Text) + BuyerBAL.GetBuyerOutstandingAmount(Convert.ToInt64(hdnEntityId.Value)) ).ToString();
            
            //Get Buyer Outstanding amount(if any)

            if (Session["ApplyPaymentStep"] != null && Session["ApplyPaymentStep"].ToString() == "PaymentStatus")
            {
                btnApplySelectedExpenses.Visible = false;
                Session["ApplyPaymentStep"] = null;
                Response.Redirect("MakeANewPayment.aspx", true);
            }


            ShowExpenseList = true;
            Int32 entityTypeId = Int32.Parse(hdnEntityTypeId.Value);

            //For Utility Company
            if (entityTypeId == 4)
            {
                ShowExpenseList = false;
                selectedExpensesTable.Visible = false;
                openExpensesTable.Visible = false;
                btnApplySelectedExpenses.Visible = true;
            }

            if (!IsPostBack)
            {  
                //Added by Nitin Paliwal,Nov 25 2009
                txtPaymentAmount.Text = hdnAmount.Value.Replace('$', ' ').Trim();
                //Get Total Outstanding(previous) if any
                //both are same,but for calculation we keep updating TotalOutstanding
                PreviousOutstanding = BuyerBAL.GetBuyerOutstandingAmount(Convert.ToInt64(hdnEntityId.Value));
                lblPrevOutstandingAmount.Text = String.Format("{0:C}",PreviousOutstanding);

                TotalOutstanding = BuyerBAL.GetBuyerOutstandingAmount(Convert.ToInt64(hdnEntityId.Value));
                lblTotalOutstanding.Text = String.Format("{0:C}", TotalOutstanding);


                if (ShowExpenseList)
                    BindOpenExpenseList();

                BindVendorsList();
            }

            if (ShowExpenseList)
                BindSelectedExpense();
        }



        private void BindDataFromPrevPage()
        {
            if (Page.PreviousPage != null)
            {
                ContentPlaceHolder cphPayment = (ContentPlaceHolder)Page.PreviousPage.Master.FindControl("ContentPlaceHolder1");
                if (cphPayment != null)
                {
                    try
                    {
                        lblRecipientType.Text = Util.getValue((DropDownList)cphPayment.FindControl("ddlRecipientType"), "Text");
                        lblCheckNumber.Text = Util.getValue((TextBox)cphPayment.FindControl("txtCheckNumber"));
                        //lblAmount.Text = Double.Parse(Util.getValue((TextBox)cphPayment.FindControl("txtAmount"))).ToString("C", ci);
                        hdnAmount.Value = Util.getValue((TextBox)cphPayment.FindControl("txtAmount"));

                        lblRecipientName.Text = Util.getValue((Label)cphPayment.FindControl("lblSelectedRecipientName"));
                        TextBox date = (TextBox)cphPayment.FindControl("txtCheckDate");

                        lblCheckDate.Text = Util.getValue((TextBox)cphPayment.FindControl("txtCheckDate"));
                        lblInvoicenumber.Text = Util.getValue((TextBox)cphPayment.FindControl("txtInvoiceNumber"));

                        String accNoWithBankAccID = Util.getValue((DropDownList)cphPayment.FindControl("ddlAccountNumber"));
                        String[] accInfo = accNoWithBankAccID.Split('~');
                        hdnBankAccountID.Value = accInfo[0];
                        lblAccountNumber.Text = accInfo[1];

                        lblAccountingCode.Text = Util.getValue((HiddenField)cphPayment.FindControl("hdnAccountingCode"));
                        lblComments.Text = Util.getValue((TextBox)cphPayment.FindControl("txtComments"));

                        String accountInfo = Util.getValue((DropDownList)cphPayment.FindControl("ddlAccountNumber"), "Text");
                        String[] arrAccountInfo = accountInfo.Split('-');
                        if (arrAccountInfo.Length > 1)
                            this.lblBankName.Text = arrAccountInfo[1].Trim();

                        String selectedRecipientID = Util.getValue((HiddenField)cphPayment.FindControl("hdnSelectedRecipientID"));
                        hdnEntityId.Value = (String.IsNullOrEmpty(selectedRecipientID)) ? "0" : selectedRecipientID;
                        hdnEntityTypeId.Value = Util.getValue((DropDownList)cphPayment.FindControl("ddlRecipientType"));

                    }
                    catch (Exception ex)
                    {
                        Session["NewPaymentValidationErr"] = ex.Message;
                        Response.Redirect("MakeANewPayment.aspx", true);
                    }
                }
            }
        }

        private void BindOpenExpenseList()
        {
            Int32 entityId = Int32.Parse(Util.getValue(hdnEntityId));
            Int32 entityTypeId = Int32.Parse(hdnEntityTypeId.Value);

            // Fill OpenExpenseList in Grid View
            Util.setValue(grvOpenExpenses, paymentBLL.GetOpenExpenseList(entityId, entityTypeId));
        }

        private void BindSelectedExpense()
        {
            _totalSelectedExpense = 0.0;
            grvSelectedExpense.DataSource = SelectedExpenseTable;
            grvSelectedExpense.DataBind();
        }

        protected void grvOpenExpenses_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvOpenExpenses.PageIndex = e.NewPageIndex;
            BindOpenExpenseList();
        }

        protected void grvOpenExpenses_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnSelectAll_Click(object sender, EventArgs e)
        {
            String selectStatus = btnSelectAll.Text.ToLower();
            btnSelectAll.Text = (selectStatus == "select all") ? "Unselect All" : "Select All";

            foreach (GridViewRow gvrOpenExpensesRow in grvOpenExpenses.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvrOpenExpensesRow.Cells[0].FindControl("chkSelect");
                chkSelect.Checked = (selectStatus == "select all") ? true : false;
            }
        }

        protected void btnAddToSelectedList_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvrOpenExpensesRow in grvOpenExpenses.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvrOpenExpensesRow.Cells[0].FindControl("chkSelect");

                if (chkSelect.Checked)
                {
                    Int32 selectedExpenseId = Int32.Parse(((HiddenField)gvrOpenExpensesRow.Cells[0].FindControl("hdnExpenseId")).Value);
                    AddNewRow(selectedExpenseId);
                }
                //Finally Update Payment fields based on new expense selected(if any) or deselected from list(if any)
                UpdatePaymentFields();
            }
            
        }

        protected void chkSelect_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelect = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chkSelect.NamingContainer;

            Int32 selectedExpenseId = 0;

            if (chkSelect.Checked)
            {
                selectedExpenseId = Int32.Parse(((HiddenField)row.Cells[0].FindControl("hdnExpenseId")).Value);
                AddNewRow(selectedExpenseId);
            }

            //Finally Update Payment fields based on new expense selected(if any) or deselected from list(if any)
            UpdatePaymentFields();
        }

        protected void grvSelectedExpense_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView gridView = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Double expenseAmount = Double.Parse(((HiddenField)e.Row.FindControl("hdnExpenseAmount")).Value);

                _totalSelectedExpense += expenseAmount;
            }
           
            lblExpenseTotal.Text = _totalSelectedExpense.ToString("C", ci);
            
            //Added by nitin paliwal,25 nov 2009
            TotalSelExpAmount = Convert.ToDecimal(_totalSelectedExpense);
            lblTotalSelExpAmount.Text = String.Format("{0:C}", TotalSelExpAmount);

            //hdnExpenseTotal.Value = _totalSelectedExpense.ToString();

            //btnApplySelectedExpenses.Visible = false;
            //if ((Double.Parse(hdnAmount.Value.Trim()) == Double.Parse(hdnExpenseTotal.Value.Trim())) && SelectedExpenseTable.Rows.Count>0)
            //    btnApplySelectedExpenses.Visible = true;

            hdnExpenseTotal.Value = Math.Round(_totalSelectedExpense, 2).ToString();

            Double expenseSelectedForPaid = Math.Round(_totalSelectedExpense, 2);
            Double amountInputForPaid = Math.Round(Double.Parse(hdnAmount.Value.Trim()), 2);

            btnApplySelectedExpenses.Visible = false;
            
            // if (expenseSelectedForPaid == amountInputForPaid && SelectedExpenseTable.Rows.Count > 0)
            if (SelectedExpenseTable.Rows.Count > 0)
              btnApplySelectedExpenses.Visible = true;
        }

        protected void grvSelectedExpense_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvSelectedExpense.PageIndex = e.NewPageIndex;
            grvSelectedExpense.DataSource = SelectedExpenseTable;
            grvSelectedExpense.DataBind();
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            ImageButton ibtn = (ImageButton)sender;
            Int32 expenseId = Int32.Parse(ibtn.CommandArgument);


            DataTable dtSelectedExpense = SelectedExpenseTable;
            EnumerableRowCollection<DataRow> row = (from r in dtSelectedExpense.AsEnumerable()
                                                    where r.Field<Int32>("ObjectId") == expenseId
                                                    select r);

            if (row.Count() == 1)
            {
                dtSelectedExpense.Rows.Remove(row.Single());

                // Save in ViewState
                this.SelectedExpenseTable = dtSelectedExpense;
            }

            BindSelectedExpense();

            //Finally Update Payment fields based on new expense selected(if any) or deselected from list(if any)
            UpdatePaymentFields();
        }

        private void AddNewRow(Int32 selectedExpenseId)
        {
            try
            {
                List<GetOpenExpenseListResult> openExpense = paymentBLL.GetOpenExpenseList(Int32.Parse(hdnEntityId.Value), Int32.Parse(hdnEntityTypeId.Value));

                var selectedExpense = (from p in openExpense
                                       where p.ObjectId == selectedExpenseId
                                       select p);

                GetOpenExpenseListResult result = selectedExpense.Single();


                DataTable dtSelectedExpense = this.SelectedExpenseTable;

                EnumerableRowCollection<DataRow> row = (from r in dtSelectedExpense.AsEnumerable()
                                                        where r.Field<Int32>("ObjectId") == result.ObjectId
                                                        select r);

                //If the seleted row is not added in table then add this
                if (row.Count() == 0)
                    AddRowInDataTable(ref dtSelectedExpense, result);

                // Save in ViewState
                this.SelectedExpenseTable = dtSelectedExpense;

                // Bind in GeidView
                BindSelectedExpense();
            }
            catch { }
        }

        private DataTable CreateSelectedExpenseTable()
        {
            DataTable dtSelExpence = new DataTable();
            dtSelExpence.Columns.Add("ObjectId", typeof(Int32));
            dtSelExpence.Columns.Add("ExpenseDate", typeof(DateTime));
            dtSelExpence.Columns.Add("ExpenseType");
            dtSelExpence.Columns.Add("ExpenseAmount", typeof(Decimal));
            dtSelExpence.Columns.Add("VIN");
            dtSelExpence.Columns.Add("Comments"); ;

            return dtSelExpence;
        }

        private void AddRowInDataTable(ref DataTable dt, GetOpenExpenseListResult result)
        {
            DataRow dr = dt.NewRow();
            dr["ObjectId"] = result.ObjectId;
            dr["ExpenseDate"] = (result.ExpenseDate == null) ? DateTime.MinValue : result.ExpenseDate;
            dr["ExpenseType"] = result.ExpenseType;
            dr["ExpenseAmount"] = result.ExpenseAmount;
            dr["VIN"] = result.VIN;
            dr["Comments"] = result.Comments;

            dt.Rows.Add(dr);
        }

        protected void btnApplySelectedExpenses_Click(object sender, EventArgs e)
        {
            if (Session["ApplyPaymentStep"] != null && Session["ApplyPaymentStep"].ToString() == "PaymentStatus")
            {
                Session["ApplyPaymentStep"] = null;
                Response.Redirect("MakeANewPayment.aspx", true);
            }

            if (String.IsNullOrEmpty(Util.getValue(lblAccountingCode)))
            {
                lblError.Text = "Accounting Code is required to make a payment. Please set Accounting Code first.";
                lblError.Visible = true;
                return;
            }

            if (IsValid)
            {
                Int32 entityID = Int32.Parse(Util.getValue(hdnEntityId));

                if (entityID <= 0)
                    return;

                // Check if the Selected Expense is already updated py payment ID by any other user
                DataTable dtSelectedExpense = SelectedExpenseTable;
                Decimal? totalExpenseToPay = 0;
                Int32 entityTypeID = Int32.Parse(Util.getValue(hdnEntityTypeId));

                if (entityTypeID != 4) //4-UtilityCompany
                {
                    foreach (DataRow dr in dtSelectedExpense.Rows)
                    {
                        Int64 expenseId = Int64.Parse(dr["ObjectId"].ToString());

                        if (expenseId > 0)
                        {
                            IQueryable<Expense> result = paymentBLL.GetExpenseByID(expenseId);
                            Expense exp = result.First();

                            totalExpenseToPay += exp.ExpenseAmount;

                            if (exp.CheckPaid == true)
                            {
                                lblError.Text = "This expense is already paid. Please retry for another expense.";
                                lblError.Visible = true;
                                return;
                            }
                        }
                    }

                    //Below line of code commented by Nitin Paliwal,25 Nov 2009

                    //// Cross check the total expense to pay 
                    //if (totalExpenseToPay.Value != Convert.ToDecimal(Util.getValue(hdnAmount)))
                    //{
                    //    lblError.Text = "Total selected expense should not be different than the amount to be paid.";
                    //    lblError.Visible = true;
                    //    return;
                    //}
                }


                Payment pmt = new Payment();
                pmt.PaymentId = 0;
                pmt.EntityId = entityID;
                pmt.EntityTypeId = Int32.Parse(Util.getValue(hdnEntityTypeId));
                pmt.BankAccountId = Int32.Parse(Util.getValue(hdnBankAccountID));
                pmt.CheckNumber = Util.getValue(lblCheckNumber);
                pmt.CheckDate = DateTime.Parse(Util.getValue(lblCheckDate));
                //Did Changes By Nitin Paliwal, 03 Dec 2009, to correct payment amount as
                pmt.Amount = Convert.ToDecimal(Util.getValue(txtPaymentAmount));  //Convert.ToDecimal(Util.getValue(hdnExpenseTotal));
                pmt.InvoiceNumber = Util.getValue(lblInvoicenumber);
                pmt.AddedBy = Constant.UserId;
                pmt.DateAdded = Util.GetServerDate();
                pmt.Comments = Util.getValue(lblComments);
                pmt.IsActive = 1;

                // Insert Payment & Update Expense for selected Expense
                Int64 paymentID = 0;
                //DataTable dtSelectedExpense = SelectedExpenseTable;
                Boolean isSaved = paymentBLL.SaveBuyerPayment(pmt, dtSelectedExpense,Convert.ToDecimal(txtPaymentAmount.Text)
                                   ,TotalSelExpAmount,Convert.ToDecimal(ViewState["CurrentOutstanding"])
                                   ,Convert.ToDecimal(ViewState["TotalOutstanding"]), ref paymentID);

                // Update Peachtree
                UpdatePaymentInPeachtree(dtSelectedExpense, paymentID);


                Session["PaymentsStatus"] = isSaved;
                Response.Redirect("PaymentStatus.aspx?pid=" + paymentID);
            }
        }

        private void UpdatePaymentInPeachtree(DataTable dtExpenses, Int64 paymentID)
        {
            try
            {
                Session["IsPeachtreeError"] = null;
                Session["PeachtreeError"] = null;

                List<GetPaymentByIDResult> results = paymentBLL.GetPaymentByID(paymentID, Constant.OrgID);
                DataTable dtPayment = Util.ConvertToTable(results);

                HeadStartVMSPtree ptService = new HeadStartVMSPtree();
                String paymentGuid = String.Empty;
                String errMsg = String.Empty;

                String securityCode = ConfigurationSettings.AppSettings["PeachtreeSecurityCode"].ToString();
                Boolean boolTestMode = Convert.ToBoolean(ConfigurationSettings.AppSettings["PeachtreeTestMode"].ToString());

                // Set DataTabe names, it is required for serialization
                dtExpenses.TableName = "EXPENSES";
                dtPayment.TableName = "PAYMENT";

                Boolean retval = ptService.ImportPaymentDataTables(securityCode, boolTestMode, dtPayment, dtExpenses, out paymentGuid, out errMsg);

                if (Util.IsGuid(paymentGuid))
                {
                    // Update Payment for Peachtree 
                    Payment pmt = new Payment();
                    pmt.PaymentId = paymentID;
                    pmt.PeechtreeRefNumber = paymentGuid;
                    pmt.PeechtreeRefDate = Util.GetServerDate();
                    retval = paymentBLL.UpdatePaymentPeachtree(pmt);

                    if (retval)
                    {
                        Session["IsPeachtreeError"] = false;
                        Session["PeachtreeError"] = "";
                    }
                    else
                    {
                        Session["IsPeachtreeError"] = true;
                        Session["PeachtreeError"] = "Peachtree Error: Peachtree updated successfully but there was an error updating database";
                    }
                }
                else
                {
                    // if paymentGuid is blank then it means there is an error in Peachtree update
                    Session["IsPeachtreeError"] = true;
                    Session["PeachtreeError"] = "Peachtree Error:" + errMsg;
                }

            }
            catch (Exception ex)
            {
                Session["IsPeachtreeError"] = true; ;
                Session["PeachtreeError"] = "Peachtree Error:" + ex.Message;
            }
        }

        // PeachTree Part goes Here
        private void BindVendorsList()
        {
            List<PeachtreeVendorsList> peachtreeVendorsList = GetPeachtreeVendorsList();
            Util.setValue(grvPeachtreeVendors, peachtreeVendorsList);
        }

        private List<PeachtreeVendorsList> GetPeachtreeVendorsList()
        {
            String searchField = Util.getValue(ddlSearchField);
            String searchOperator = Util.getValue(ddlSearchOperator);
            String searchString = Util.getValue(txtSearchString).ToLower();

            List<PeachtreeVendorsList> peachtreeVendorsList = paymentBLL.GetPeachtreeVendorsList().ToList();

            var query = from p in peachtreeVendorsList
                        select p;

            switch (searchField.ToLower())
            {
                case "name":
                    if (searchOperator == "begins with")
                        query = query.Where(c => c.Name.ToLower().StartsWith(searchString));
                    if (searchOperator == "ends with")
                        query = query.Where(c => c.Name.ToLower().EndsWith(searchString));
                    if (searchOperator == "contains")
                        query = query.Where(c => c.Name.ToLower().Contains(searchString));
                    break;

                case "id":
                    if (searchOperator == "begins with")
                        query = query.Where(c => c.ID.ToLower().StartsWith(searchString));
                    if (searchOperator == "ends with")
                        query = query.Where(c => c.ID.ToLower().EndsWith(searchString));
                    if (searchOperator == "contains")
                        query = query.Where(c => c.ID.ToLower().Contains(searchString));
                    break;

                case "line1":
                    if (searchOperator == "begins with")
                        query = query.Where(c => c.addressLine1.ToLower().StartsWith(searchString));
                    if (searchOperator == "ends with")
                        query = query.Where(c => c.addressLine1.ToLower().EndsWith(searchString));
                    if (searchOperator == "contains")
                        query = query.Where(c => c.addressLine1.ToLower().Contains(searchString));
                    break;

                case "city":
                    if (searchOperator == "begins with")
                        query = query.Where(c => c.City.ToLower().StartsWith(searchString));
                    if (searchOperator == "ends with")
                        query = query.Where(c => c.City.ToLower().EndsWith(searchString));
                    if (searchOperator == "contains")
                        query = query.Where(c => c.City.ToLower().Contains(searchString));
                    break;

                case "state":
                    if (searchOperator == "begins with")
                        query = query.Where(c => c.State.ToLower().StartsWith(searchString));
                    if (searchOperator == "ends with")
                        query = query.Where(c => c.State.ToLower().EndsWith(searchString));
                    if (searchOperator == "contains")
                        query = query.Where(c => c.State.ToLower().Contains(searchString));
                    break;

                case "country":
                    if (searchOperator == "begins with")
                        query = query.Where(c => c.Country.ToLower().StartsWith(searchString));
                    if (searchOperator == "ends with")
                        query = query.Where(c => c.Country.ToLower().EndsWith(searchString));
                    if (searchOperator == "contains")
                        query = query.Where(c => c.Country.ToLower().Contains(searchString));
                    break;
            }

            query = query.OrderBy(c => c.Name);
            return query.ToList();
        }

        private void RefreshVendorsFromPeachtree()
        {
            HeadStartVMSPtree ptService = new HeadStartVMSPtree();
            String vendorListXML = String.Empty;
            String errMsg = String.Empty;
            String securityCode = ConfigurationSettings.AppSettings["PeachtreeSecurityCode"].ToString();
            Boolean boolTestMode = Convert.ToBoolean(ConfigurationSettings.AppSettings["PeachtreeTestMode"].ToString());

            Boolean retval = ptService.GetVendorList(securityCode, boolTestMode, out vendorListXML, out errMsg);

            if (!retval)
            {
                lblError.Text = "Error Returned from Peachtree: " + errMsg;
                lblError.Visible = true;
                return;
            }

            DataSet dsXML = Util.GetDataSet(vendorListXML, false);

            if (Util.IsValidDataSet(dsXML))
            {
                DataTable dtPAWVendor = dsXML.Tables["PAW_Vendor"];

                IEnumerable<PeachtreeVendorsList> pvlResult = from p in dsXML.Tables["PAW_Vendor"].AsEnumerable()
                                                              join q in dsXML.Tables["RemitToAddress"].AsEnumerable() on p.Field<int>("PAW_Vendor_Id") equals q.Field<int>("PAW_Vendor_Id") into UP
                                                              from q in UP.DefaultIfEmpty()

                                                              join r in dsXML.Tables["ID"].AsEnumerable() on p.Field<int>("PAW_Vendor_Id") equals r.Field<int>("PAW_Vendor_Id") into UR
                                                              from r in UR.DefaultIfEmpty()

                                                              join s in dsXML.Tables["PhoneNumbers"].AsEnumerable() on p.Field<int>("PAW_Vendor_Id") equals s.Field<int>("PAW_Vendor_Id") into US
                                                              from s in US.DefaultIfEmpty()

                                                              select new PeachtreeVendorsList
                                                              {
                                                                  isInactive = Convert.ToBoolean(p.Field<string>("isInactive")),
                                                                  Name = GetString(p.Field<string>("Name")),
                                                                  ID = GetString(r.Field<string>("ID_Text")),
                                                                  Fax = GetString(p.Field<string>("FaxNumber")),
                                                                  Email_Address = GetString(p.Field<string>("EMail_Address")),
                                                                  Web_Address = GetString(p.Field<string>("Web_Address")),
                                                                  GUID = GetString(p.Field<string>("GUID")).Replace("{", "").Replace("}", ""),
                                                                  AccountNumber = GetString(p.Field<string>("AccountNumber")),
                                                                  addressLine1 = GetString(q.Field<string>("Line1")),
                                                                  addressLine2 = GetString(q.Field<string>("Line2")),
                                                                  City = GetString(q.Field<string>("City")),
                                                                  State = GetString(q.Field<string>("State")),
                                                                  Zip = GetString(q.Field<string>("Zip")),
                                                                  Country = GetString(q.Field<string>("Country")),
                                                                  DateAdded = Util.GetServerDate(),
                                                                  AddedBy = Constant.UserId,
                                                                  IsActive = 1,
                                                                  Phone1 = GetPhoneNo(dsXML.Tables["PhoneNumber"], s.Field<int>("PhoneNumbers_Id"), 1),
                                                                  Phone2 = GetPhoneNo(dsXML.Tables["PhoneNumber"], s.Field<int>("PhoneNumbers_Id"), 2)
                                                              };

                // Delete all Data from PeachtreeVendorsList table
                paymentBLL.DeleteAllPeachtreeVendorsList();

                // Save new result
                if (pvlResult.Count() > 0)
                    paymentBLL.Save(pvlResult);
            }
        }

        private String GetString(String str)
        {
            return String.IsNullOrEmpty(str) ? "" : str;
        }

        private String GetPhoneNo(DataTable dtPhoneNumber, int PhoneNumbers_Id, int type)
        {
            DataRow[] drRows = dtPhoneNumber.Select("PhoneNumbers_Id=" + PhoneNumbers_Id.ToString());
            if (drRows.LongLength > 0 && type == 1)
                return drRows[0]["PhoneNumber_Text"].ToString(); // Phone1
            else if (drRows.LongLength > 1 && type == 2)
                return drRows[1]["PhoneNumber_Text"].ToString(); // Phone 2
            else
                return "";
        }

        protected void grvPeachtreeVendors_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvPeachtreeVendors.PageIndex = e.NewPageIndex;
            BindVendorsList();
            mpeAccountingCode.Show();
        }

        protected void grvPeachtreeVendors_OnSorting(object sender, GridViewSortEventArgs e)
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
            IEnumerable<PeachtreeVendorsList> results = GetPeachtreeVendorsList().Sort<PeachtreeVendorsList>(sortExpression + direction);

            Util.setValue(grvPeachtreeVendors, results.ToList());
            mpeAccountingCode.Show();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            BindVendorsList();
            mpeAccountingCode.Show();
        }

        protected void btnRefreshVendorsFromPeachtree_Click(object sender, EventArgs e)
        {
            // NEEDS WORK for Wait MSG and Error Handeling
            try
            {
                RefreshVendorsFromPeachtree();
            }
            catch { }

            mpeAccountingCode.Show();
        }

        protected void btnSelectAccCode_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvPeachtreeVendors.Rows)
            {
                GroupRadioButton selectedRadioButton = row.FindControl("selectedRadioButton") as GroupRadioButton;

                if (selectedRadioButton != null && selectedRadioButton.Checked)
                {
                    lblAccountingCode.Text = grvPeachtreeVendors.DataKeys[row.RowIndex].Value.ToString();
                    hdnGUID.Value = ((HiddenField)grvPeachtreeVendors.Rows[row.RowIndex].FindControl("hdnGUID")).Value.Trim();
                }
            }

            // Update Accounting Code
            try
            {
                Int64 entityID = Int64.Parse(Util.getValue(hdnEntityId));
                if (entityID <= 0)
                    return;

                AccountingCode ac = new AccountingCode();
                ac.AccountingCode1 = Util.getValue(lblAccountingCode);
                ac.EntityID = Int64.Parse(Util.getValue(hdnEntityId));
                ac.EntityTypeID = Int32.Parse(Util.getValue(hdnEntityTypeId));
                ac.PeachtreeVendorGUID = Util.getValue(hdnGUID);
                ac.AddedBy = Constant.UserId;
                ac.DateAdded = Util.GetServerDate();
                ac.ModifiedBy = Constant.UserId;
                ac.DateModified = Util.GetServerDate();
                ac.IsActive = 1;

                Int64? accountingCodeID = 0;
                int? returnCode = 0;
                paymentBLL.SaveAccountingCode(ac, ref accountingCodeID, ref returnCode);
            }
            catch
            {
            }
        }
        /// <summary>
        /// Handle textbox textchanged event to adjust Current Outstanding and Total Outstanding based on payment amount entered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtPaymentAmount_TextChanged(object sender, EventArgs e)
        {
            UpdatePaymentFields();   
        }

        public decimal TotalSelExpAmount
        {
            set;
            get;
        }

        public decimal TotalOutstanding
        {
            set;
            get;
        }

        public decimal PreviousOutstanding
        {
            set;
            get;
        }


        public decimal CurrentOutstanding
        {
            set;
            get;
        }


        /// <summary>
        /// This method will update payment outstanding,total outstanding amount based on changed payment amount
        /// </summary>
        protected void UpdatePaymentFields()
        {
            //decimal TotalSelExp_Amt;

            //TotalSelExp_Amt = Convert.ToDecimal(lblTotalSelExpAmount.Text);

            CurrentOutstanding = Convert.ToDecimal(txtPaymentAmount.Text) - TotalSelExpAmount;
            ViewState["CurrentOutstanding"] = CurrentOutstanding;

            //Format the Current Outstanding Amount
            lblCurrentOutstanding.Text = String.Format("{0:C}", CurrentOutstanding);

            TotalOutstanding =
               BuyerBAL.GetBuyerOutstandingAmount(Convert.ToInt64(hdnEntityId.Value)) + CurrentOutstanding;
            ViewState["TotalOutstanding"] = TotalOutstanding;

            //Format the total oustanding Amount
            lblTotalOutstanding.Text = String.Format("{0:C}", TotalOutstanding);
            
        }
    }
}
