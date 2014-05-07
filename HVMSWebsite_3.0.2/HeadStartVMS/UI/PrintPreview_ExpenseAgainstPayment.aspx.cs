using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

//Extended
using System.Collections.Generic;
using METAOPTION.BAL;
using METAOPTION.PeachtreeService;
using MetaOption.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using METAOPTION.Reports;
using System.Configuration;
using System.IO;


namespace METAOPTION.UI
{
    public partial class PrintPreview_ExpenseAgainstPayment : System.Web.UI.Page
    {
        PaymentBLL paymentBLL = new PaymentBLL();
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        private Decimal totalExpenseAmount = 0;

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

        public Int32 PaymentID
        {
            get
            {
                string str = Request.QueryString["PaymentId"].ToString();
                if (ViewState["PaymentId"] == null)
                {
                    if (Request.QueryString["paymentid"] != null && Request.QueryString["PaymentId"].ToString() != "")
                        ViewState["paymentid"] = Int32.Parse(Request.QueryString["PaymentId"].ToString());
                    else
                        ViewState["paymentid"] = 0;
                }

                return (Int32)ViewState["paymentid"];
            }
            set { ViewState["paymentid"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            //lblError.Visible = false;

           // CheckPermission();

            //Display Server DateTime information in Header
            lblServerDate.Text = DateTime.Now.ToString();

            if (!IsPostBack)
            {
               
                BindData();

                //If Buyer,We check logic inside whether to display outstanding table or not
                if (hdnEntityTypeId.Value == "2")
                    DisplayBuyerOutstanding();
                else tblOutstandingAmt.Visible = false;
            }

        }

        /// <summary>
        /// Display Buyer Outstanding Amount if any
        /// </summary>
        protected void DisplayBuyerOutstanding()
        {
            decimal outstanding_Amt = BuyerBAL.GetBuyerOutstandingAmount(Convert.ToInt64(hdnEntityId.Value));

            if (outstanding_Amt != 0)
            {
                lblPaymentMade.Text = lblAmount.Text;
                lblOutstandingAmount.Text = (BuyerBAL.GetBuyerOutstandingAmount(Convert.ToInt64(hdnEntityId.Value))).ToString();

                //Display Amount as Outstanding Amount(Due) + Payment made
                decimal totalAmount = Convert.ToDecimal(lblAmount.Text) + Convert.ToDecimal(lblOutstandingAmount.Text);

                //Apply Decimal Format for all Numeric Values
                lblPaymentMade.Text = String.Format("{0:C}", Convert.ToDecimal(lblPaymentMade.Text));
                lblAmount.Text = String.Format("{0:C}", totalAmount);
                lblExpAmount.Text = String.Format("{0:C}", totalExpenseAmount);
                lblOutstandingAmount.Text = String.Format("{0:C}", Convert.ToDecimal(lblOutstandingAmount.Text));
                lblTotalAmount.Text = String.Format("{0:C}", totalAmount);
            }
            else //Hide table if No Outstanding
            {
                tblOutstandingAmt.Visible = false;
            }
        }

        private void CheckPermission()
        {
            List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, "PAYMENT");

            if (!dict.Contains("MAKEANEWPAYMENT.ADD"))
                Response.Redirect("Permission.aspx?MSG=MAKEANEWPAYMENT.ADD", true);
        }

        private void BindData()
        {
            if (PaymentID > 0)
            {
                List<GetPaymentByIDResult> results = paymentBLL.GetPaymentByID(PaymentID,Constant.OrgID);
                GetPaymentByIDResult pmt = results.First();

                Util.setValue(lblAmount, pmt.Amount);
                Util.setValue(lblCheckNumber, pmt.CheckNumber);
                Util.setValue(lblCheckDate, pmt.CheckDate);
                Util.setValue(lblInvoicenumber, pmt.InvoiceNumber);
                Util.setValue(lblComments, pmt.Comments);
                Util.setValue(lblRecipientType, pmt.recipienttype);
                Util.setValue(lblRecipientName, pmt.recipientname);
                Util.setValue(lblBankName, pmt.BankName);
                //Util.setValue(lblAccountNumber, pmt.AccountNumber);
                Util.setValue(hdnEntityId, pmt.EntityId);
                Util.setValue(hdnEntityTypeId, pmt.EntityTypeId);
                Util.setValue(lblAddedBy, pmt.DisplayName);
                Util.setValue(lblDateAdded, pmt.DateAdded);
                Util.setValue(lblPeachtreeRefNo, pmt.PeechtreeRefNumber);
                Util.setValue(lblPeachtreeRefDate, pmt.PeechtreeRefDate);
                Util.setValue(lblAccountingCode, pmt.AccountingCode);


                //// Print Section                
                //Boolean isPrinted = (pmt.IsPrinted == null) ? false : (Boolean)pmt.IsPrinted;
                //if (!IsPostBack)
                //    Util.setValue(chkIsPrinted, isPrinted);

                String printDate = (String.IsNullOrEmpty(pmt.PrintDateTime)) ? "" : " on " + pmt.PrintDateTime.ToString();
                String notPrintMsg = (pmt.IsVoided != 1) ? " - Check it and click on update after you print this check." : "";
                // String printStatus = (isPrinted) ? "Printed" + printDate : "Not Printed" + notPrintMsg;
                //chkIsPrinted.ToolTip = printStatus;
                //chkIsPrinted.Text = (isPrinted) ? " ( Printed" + printDate + ")" : " ( Not Printed )";

                // lnkVoidPayment.Visible = true;
                // lnkSelectAccountingCode.Visible = false;
                // lnkUpdatePeachtree.Visible = false;
                grvSelectedExpense.Visible = false;
                grvVoidedExpense.Visible = false;
                // chkIsPrinted.Enabled = false;
                // lnkUpdatePrintStatus.Visible = false;
                //lnkPrintCheck.Visible = false;
                //lnkPrintCheckVersion1.Visible = false;

                // Void Section
                if (pmt.IsVoided != 1)
                {
                    grvSelectedExpense.Visible = true;
                    Util.setValue(grvSelectedExpense, paymentBLL.GetExpenseByPaymentID(PaymentID));
                    //Util.setValue(lblVoidStatus, "Normal");

                    //if (!isPrinted)
                    //{
                    //    chkIsPrinted.Enabled = true;
                    //    lnkUpdatePrintStatus.Visible = true;
                    //    lnkPrintCheck.Visible = true;
                    //    lnkPrintCheckVersion1.Visible = true;
                    //}

                    //lnkSelectAccountingCode.Visible = true;

                    //if (String.IsNullOrEmpty(pmt.PeechtreeRefNumber))
                    //    lnkUpdatePeachtree.Visible = true;
                }
                else
                {
                    List<GetVoidHistoryByPaymentIDResult> voidHistoryResults = paymentBLL.GetVoidHistoryByPaymentID(PaymentID);
                    GetVoidHistoryByPaymentIDResult voidHistory = voidHistoryResults.First();

                    // lnkVoidPayment.Visible = false;
                    //  lblVoidStatus.Visible = true;
                    // Util.setValue(lblVoidStatus, String.Format("VOIDED BY {0} ON {1}", voidHistory.VoidedByName, voidHistory.DateVoided));
                    // lblVoidStatus.ForeColor = System.Drawing.Color.Red;
                    grvVoidedExpense.Visible = true;

                    // Get data from CheckVoidHistory & CheckVoidHistoryDetails 
                    Util.setValue(grvVoidedExpense, paymentBLL.GetVoidExpenseByPaymentID(PaymentID));
                }
            }
        }

        //private void BindVendorsList()
        //{
        //    IEnumerable<PeachtreeVendorsList> peachtreeVendorsList = GetPeachtreeVendorsList().Sort<PeachtreeVendorsList>(" Name ASC, ID ASC");
        //    //List<PeachtreeVendorsList> peachtreeVendorsList = GetPeachtreeVendorsList(); 
        //    Util.setValue(grvPeachtreeVendors, peachtreeVendorsList);
        //}

        //private List<PeachtreeVendorsList> GetPeachtreeVendorsList()
        //{
        //    String searchField = Util.getValue(ddlSearchField);
        //    String searchOperator = Util.getValue(ddlSearchOperator);
        //    String searchString = Util.getValue(txtSearchString);

        //    List<PeachtreeVendorsList> peachtreeVendorsList = paymentBLL.GetPeachtreeVendorsList().ToList();

        //    var query = from p in peachtreeVendorsList
        //                select p;

        //    switch (searchField.ToLower())
        //    {
        //        case "name":
        //            if (searchOperator == "begins with")
        //                query = query.Where(c => c.Name.StartsWith(searchString));
        //            if (searchOperator == "ends with")
        //                query = query.Where(c => c.Name.EndsWith(searchString));
        //            if (searchOperator == "contains")
        //                query = query.Where(c => c.Name.Contains(searchString));
        //            break;

        //        case "id":
        //            if (searchOperator == "begins with")
        //                query = query.Where(c => c.ID.StartsWith(searchString));
        //            if (searchOperator == "ends with")
        //                query = query.Where(c => c.ID.EndsWith(searchString));
        //            if (searchOperator == "contains")
        //                query = query.Where(c => c.ID.Contains(searchString));
        //            break;

        //        case "line1":
        //            if (searchOperator == "begins with")
        //                query = query.Where(c => c.addressLine1.StartsWith(searchString));
        //            if (searchOperator == "ends with")
        //                query = query.Where(c => c.addressLine1.EndsWith(searchString));
        //            if (searchOperator == "contains")
        //                query = query.Where(c => c.addressLine1.Contains(searchString));
        //            break;

        //        case "city":
        //            if (searchOperator == "begins with")
        //                query = query.Where(c => c.City.StartsWith(searchString));
        //            if (searchOperator == "ends with")
        //                query = query.Where(c => c.City.EndsWith(searchString));
        //            if (searchOperator == "contains")
        //                query = query.Where(c => c.City.Contains(searchString));
        //            break;

        //        case "state":
        //            if (searchOperator == "begins with")
        //                query = query.Where(c => c.State.StartsWith(searchString));
        //            if (searchOperator == "ends with")
        //                query = query.Where(c => c.State.EndsWith(searchString));
        //            if (searchOperator == "contains")
        //                query = query.Where(c => c.State.Contains(searchString));
        //            break;

        //        case "country":
        //            if (searchOperator == "begins with")
        //                query = query.Where(c => c.Country.StartsWith(searchString));
        //            if (searchOperator == "ends with")
        //                query = query.Where(c => c.Country.EndsWith(searchString));
        //            if (searchOperator == "contains")
        //                query = query.Where(c => c.Country.Contains(searchString));
        //            break;
        //    }

        //    query = query.OrderBy(c => c.Name);
        //    return query.ToList();
        //}

        //private void RefreshVendorsFromPeachtree()
        //{

        //    HeadStartVMSPtree ptService = new HeadStartVMSPtree();
        //    String vendorListXML = String.Empty;
        //    String errMsg = String.Empty;

        //    String securityCode = ConfigurationSettings.AppSettings["PeachtreeSecurityCode"].ToString();
        //    Boolean boolTestMode = Convert.ToBoolean(ConfigurationSettings.AppSettings["PeachtreeTestMode"].ToString());
        //    Boolean retval = ptService.GetVendorList(securityCode, boolTestMode, out vendorListXML, out errMsg);

        //    // Used for testing
        //    //StreamReader sr = new StreamReader(@"D:\Shared\HeadStartVMS\tempVendorList_04e0d18d-54b0-460c-b376-e725492f9615.xml"); 
        //    //vendorListXML = sr.ReadToEnd();

        //    DataSet dsXML = Util.GetDataSet(vendorListXML, false);

        //    if (Util.IsValidDataSet(dsXML))
        //    {
        //        DataTable dtPAWVendor = dsXML.Tables["PAW_Vendor"];

        //        IEnumerable<PeachtreeVendorsList> pvlResult = from p in dsXML.Tables["PAW_Vendor"].AsEnumerable()
        //                                                      join q in dsXML.Tables["RemitToAddress"].AsEnumerable() on p.Field<int>("PAW_Vendor_Id") equals q.Field<int>("PAW_Vendor_Id") into UP
        //                                                      from q in UP.DefaultIfEmpty()

        //                                                      join r in dsXML.Tables["ID"].AsEnumerable() on p.Field<int>("PAW_Vendor_Id") equals r.Field<int>("PAW_Vendor_Id") into UR
        //                                                      from r in UR.DefaultIfEmpty()

        //                                                      join s in dsXML.Tables["PhoneNumbers"].AsEnumerable() on p.Field<int>("PAW_Vendor_Id") equals s.Field<int>("PAW_Vendor_Id") into US
        //                                                      from s in US.DefaultIfEmpty()

        //                                                      select new PeachtreeVendorsList
        //                                                      {
        //                                                          isInactive = Convert.ToBoolean(p.Field<string>("isInactive")),
        //                                                          Name = GetString(p.Field<string>("Name")),
        //                                                          ID = GetString(r.Field<string>("ID_Text")),
        //                                                          Fax = GetString(p.Field<string>("FaxNumber")),
        //                                                          Email_Address = GetString(p.Field<string>("EMail_Address")),
        //                                                          Web_Address = GetString(p.Field<string>("Web_Address")),
        //                                                          GUID = GetString(p.Field<string>("GUID")).Replace("{", "").Replace("}", ""),
        //                                                          AccountNumber = GetString(p.Field<string>("AccountNumber")),
        //                                                          addressLine1 = GetString(q.Field<string>("Line1")),
        //                                                          addressLine2 = GetString(q.Field<string>("Line2")),
        //                                                          City = GetString(q.Field<string>("City")),
        //                                                          State = GetString(q.Field<string>("State")),
        //                                                          Zip = GetString(q.Field<string>("Zip")),
        //                                                          Country = GetString(q.Field<string>("Country")),
        //                                                          //PhoneNumbers_Id = s.Field<int>("PhoneNumbers_Id"),
        //                                                          DateAdded = Util.GetServerDate(),
        //                                                          AddedBy = Constant.UserId,
        //                                                          IsActive = 1,
        //                                                          Phone1 = GetPhoneNo(dsXML.Tables["PhoneNumber"], s.Field<int>("PhoneNumbers_Id"), 1),
        //                                                          Phone2 = GetPhoneNo(dsXML.Tables["PhoneNumber"], s.Field<int>("PhoneNumbers_Id"), 2)
        //                                                      };

        //        // Delete all Data from PeachtreeVendorsList table
        //        paymentBLL.DeleteAllPeachtreeVendorsList();

        //        // Save new result
        //        if (pvlResult.Count() > 0)
        //            paymentBLL.Save(pvlResult);
        //    }
        //}

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

        //private void UpdatePaymentInPeachtree()
        //{
        //    try
        //    {
        //        List<GetExpenseByPaymentIDResult> expenseList = paymentBLL.GetExpenseByPaymentID(PaymentID);
        //        DataTable dtExpenses = Util.ConvertToTable(expenseList);

        //        List<GetPaymentByIDResult> payment = paymentBLL.GetPaymentByID(PaymentID);
        //        DataTable dtPayment = Util.ConvertToTable(payment);

        //        HeadStartVMSPtree ptService = new HeadStartVMSPtree();
        //        String paymentGuid = String.Empty;
        //        String errMsg = String.Empty;

        //        String securityCode = ConfigurationSettings.AppSettings["PeachtreeSecurityCode"].ToString();
        //        Boolean boolTestMode = Convert.ToBoolean(ConfigurationSettings.AppSettings["PeachtreeTestMode"].ToString());

        //        dtPayment.TableName = "dtPayment";
        //        dtExpenses.TableName = "dtExpenses";

        //        //dtPayment.WriteXml(@"E:\XML\dtPayment.xml");
        //        //dtExpenses.WriteXml(@"E:\XML\dtExpenses.xml");

        //        Boolean retval = ptService.ImportPaymentDataTables(securityCode, boolTestMode, dtPayment, dtExpenses, out paymentGuid, out errMsg);

        //        if (Util.IsGuid(paymentGuid))
        //        {
        //            // Update Payment for Peachtree 
        //            Payment pmt = new Payment();
        //            pmt.PaymentId = PaymentID;
        //            pmt.PeechtreeRefNumber = paymentGuid;
        //            pmt.PeechtreeRefDate = Util.GetServerDate();
        //            retval = paymentBLL.UpdatePaymentPeachtree(pmt);

        //            if (retval)
        //                lblPeachtreeMsg.Text = "Peachtree data updated successfully";
        //            else
        //                lblPeachtreeMsg.Text = "Peachtree data updated successfully but database returned error {Guid: " + paymentGuid.ToString() + "}";

        //            lblPeachtreeMsg.Visible = true;
        //        }
        //        else
        //        {
        //            lblPeachtreeMsg.Text = "Error returned from Peachtree {" + errMsg.ToString() + "}";
        //            lblPeachtreeMsg.Visible = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblPeachtreeMsg.Text = "Error returned from Service {" + ex.Message.ToString() + "}";
        //        lblPeachtreeMsg.Visible = true;
        //    }
        //}

        protected void lnkRecipientName_Click(object sender, EventArgs e)
        {
            String entityId = Util.getValue(hdnEntityId);
            String entityTypeId = Util.getValue(hdnEntityTypeId);

            String url = "";
            switch (entityTypeId)
            {
                case "1":
                    url = "ViewDealer.aspx?Mode=View&EntityId=" + entityId + "&type=" + entityTypeId;
                    break;

                case "2":
                    url = "ViewBuyerDetails.aspx?Mode=View&BuyerId=" + entityId + "&type=" + entityTypeId;
                    break;

                case "3":
                    url = "ViewVendor.aspx?Mode=View&EntityId=" + entityId + "&type=" + entityTypeId;
                    break;

                case "4":
                    url = "ViewUtilityCompany.aspx?Mode=View&EntityId=" + entityId + "&type=" + entityTypeId;
                    break;
            }

            Response.Redirect(url);
        }

        //protected void btnCommitVoidPayment_Click(object sender, EventArgs e)
        //{
        //    String voidComment = Util.getValue(txtVoidComments);
        //    Int32 voidReasonID = Int32.Parse(Util.getValue(ddlVoidReason));

        //    if (PaymentID > 0)
        //    {
        //        bool retVal = paymentBLL.VoidCheck(PaymentID, voidReasonID, voidComment, Constant.UserId);
        //        BindData();
        //    }
        //}

        //protected void lnkPrintCheck_Click(object sender, EventArgs e)
        //{
        //    ReportParameter[] parameters = new ReportParameter[2];
        //    parameters[0] = new ReportParameter("PaymentID", PaymentID.ToString());
        //    ReportParameters.Parameters = parameters;
        //    parameters[1] = new ReportParameter("UserId", Constant.UserId.ToString());
        //    ReportParameters.Parameters = parameters;

        //    ReportParameters.ReportName = "/Hollenshead/CheckPrintingReport";
        //    Response.Redirect("~/VMSReports/Reports.aspx?ReturnURL=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
        //}

        //protected void lnkPrintCheckVersion1_Click(object sender, EventArgs e)
        //{
        //    Microsoft.Reporting.WebForms.ReportViewer rview = new Microsoft.Reporting.WebForms.ReportViewer();

        //    rview.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"]);
        //    System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter> paramList = new System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter>();

        //    paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("PaymentID", PaymentID.ToString()));
        //    paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("UserId", Constant.UserId.ToString()));

        //    rview.ServerReport.ReportPath = "/Hollenshead/CheckPrintingReport_V1";
        //    rview.ServerReport.SetParameters(paramList);


        //    string mimeType, encoding, extension, deviceInfo;
        //    string[] streamids;
        //    Microsoft.Reporting.WebForms.Warning[] warnings;
        //    string format = "PDF"; //Desired format goes here (PDF, Excel, or Image)
        //    deviceInfo = "<DeviceInfo>" + "<SimplePageHeaders>True</SimplePageHeaders>" + "</DeviceInfo>";

        //    //deviceInfo =
        //    //            "<DeviceInfo>" +
        //    //            "  <OutputFormat>PDF</OutputFormat>" +
        //    //            "  <PageWidth>8.5in</PageWidth>" +
        //    //            "  <PageHeight>11in</PageHeight>" +
        //    //            "  <MarginTop>0.5in</MarginTop>" +
        //    //            "  <MarginLeft>1in</MarginLeft>" +
        //    //            "  <MarginRight>1in</MarginRight>" +
        //    //            "  <MarginBottom>0.5in</MarginBottom>" +
        //    //            "</DeviceInfo>";

        //    byte[] bytes = rview.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
        //    Response.Clear();

        //    Response.ContentType = mimeType;
        //    Response.AddHeader("content-disposition", String.Format("attachment; filename=HeadStartVMS_CheckPrint_{0}.{1}", PaymentID.ToString(), extension));
        //    Response.BinaryWrite(bytes);
        //    Response.End();
        //}

        //protected void lnkUpdatePrintStatus_Click(object sender, EventArgs e)
        //{
        //    Payment pmt = new Payment();
        //    pmt.PaymentId = Int64.Parse(PaymentID.ToString());
        //    pmt.IsPrinted = chkIsPrinted.Checked;
        //    pmt.PrintDateTime = Util.GetServerDate().ToString();
        //    paymentBLL.UpdatePrintCheckStatus(pmt);

        //    BindData();
        //}

        //protected void grvPeachtreeVendors_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    grvPeachtreeVendors.PageIndex = e.NewPageIndex;
        //    BindVendorsList();
        //    mpeAccountingCode.Show();
        //}

        //protected void grvPeachtreeVendors_OnSorting(object sender, GridViewSortEventArgs e)
        //{
        //    string sortExpression = e.SortExpression;

        //    if (GridViewSortDirection == SortDirection.Ascending)
        //    {
        //        GridViewSortDirection = SortDirection.Descending;
        //        SortGridView(sortExpression, DESCENDING);
        //    }
        //    else
        //    {
        //        GridViewSortDirection = SortDirection.Ascending;
        //        SortGridView(sortExpression, ASCENDING);
        //    }
        //}

        //private void SortGridView(string sortExpression, string direction)
        //{
        //    IEnumerable<PeachtreeVendorsList> results = GetPeachtreeVendorsList().Sort<PeachtreeVendorsList>(sortExpression + direction);

        //    Util.setValue(grvPeachtreeVendors, results.ToList());
        //    mpeAccountingCode.Show();
        //}

        //protected void btnFilter_Click(object sender, EventArgs e)
        //{
        //    BindVendorsList();
        //    mpeAccountingCode.Show();
        //}

        //protected void btnRefreshVendorsFromPeachtree_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        lblError.Text = "";
        //        RefreshVendorsFromPeachtree();
        //        BindVendorsList();
        //        //BindData(); 
        //    }
        //    catch (Exception ex)
        //    {
        //        lblError.Text = "Error in refreshing data." + ex.Message.ToString();
        //        lblError.Visible = true;
        //    }
        //    mpeAccountingCode.Show();
        //}

        //protected void btnSelectAccCode_Click(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow row in grvPeachtreeVendors.Rows)
        //    {
        //        GroupRadioButton selectedRadioButton = row.FindControl("selectedRadioButton") as GroupRadioButton;

        //        if (selectedRadioButton != null && selectedRadioButton.Checked)
        //        {
        //            lblAccountingCode.Text = grvPeachtreeVendors.DataKeys[row.RowIndex].Value.ToString();
        //            hdnGUID.Value = ((HiddenField)grvPeachtreeVendors.Rows[row.RowIndex].FindControl("hdnGUID")).Value.Trim();
        //        }
        //    }

        //    // Update Accounting Code
        //    try
        //    {
        //        AccountingCode ac = new AccountingCode();
        //        ac.AccountingCode1 = Util.getValue(lblAccountingCode);
        //        ac.EntityID = Int64.Parse(Util.getValue(hdnEntityId));
        //        ac.EntityTypeID = Int32.Parse(Util.getValue(hdnEntityTypeId));
        //        ac.PeachtreeVendorGUID = Util.getValue(hdnGUID);
        //        ac.AddedBy = Constant.UserId;
        //        ac.DateAdded = Util.GetServerDate();
        //        ac.ModifiedBy = Constant.UserId;
        //        ac.DateModified = Util.GetServerDate();
        //        ac.IsActive = 1;

        //        Int64? accountingCodeID = 0;
        //        int? returnCode = 0;
        //        paymentBLL.SaveAccountingCode(ac, ref accountingCodeID, ref returnCode);
        //    }
        //    catch
        //    {

        //    }

        //    BindData();
        //}

        //protected void lnkUpdatePeachtree_Click(object sender, EventArgs e)
        //{
        //    UpdatePaymentInPeachtree();
        //    BindData();
        //}

        //protected void lnkVoidPayment_Click(object sender, EventArgs e)
        //{
        //    mpeVoidPayment.Show();
        //}

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    mpeVoidPayment.Hide();
        //}

        //protected void lnkVIN_Click(object sender, EventArgs e)
        //{
        //    LinkButton btn = (LinkButton)sender;
        //    String inventoryID = btn.CommandArgument;
        //    Response.Redirect("ManageInventory.aspx?Code=" + inventoryID, true);
        //}

        protected void grvSelectedExpense_RowDataBound(object sender, GridViewRowEventArgs args)
        {
            GridView gridView = (GridView)sender;

            if (args.Row.RowType == DataControlRowType.DataRow)
            {
                String expAmount = ((HiddenField)args.Row.Cells[3].FindControl("hdnExpenseAmount")).Value.Trim();

                if (expAmount != String.Empty)
                    totalExpenseAmount += Convert.ToDecimal(expAmount);

                Label lblInvoiceNumber = (Label)args.Row.FindControl("lblInvoiceNumber");
                HiddenField hdnComments = (HiddenField)args.Row.FindControl("hdnComment");
                if (!string.IsNullOrEmpty(hdnComments.Value))
                    lblInvoiceNumber.Text = hdnComments.Value + "</br><i>" + lblInvoiceNumber.Text + "</i>";
                else
                    lblInvoiceNumber.Text = "<i>" + lblInvoiceNumber.Text + "</i>";

            }
            else if (args.Row.RowType == DataControlRowType.Footer)
            {
                if (gridView.Rows.Count <= 1)
                    args.Row.Visible = false;

                //String footerHtml = String.Format("<span class='rowTotal'>Total Expense: {0}</span> ", totalExpenseAmount.ToString());
                args.Row.Cells[1].Text = "<span style='font-weight:bold; text-align:right'> Total Expense:</span>";
                args.Row.Cells[2].Text = String.Format("<span style='font-weight:bold; text-align:right'>{0}</span> ", String.Format("{0:C}", totalExpenseAmount));
                args.Row.Cells[3].Text = "<span style='font-weight:bold; text-align:right'> Record Count:</span>";
                args.Row.Cells[4].Text = String.Format("<span style='font-weight:bold; text-align:right'>{0}</span> ", gridView.Rows.Count);

            }
        }
    }
}
