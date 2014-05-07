using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using METAOPTION.BAL;

namespace METAOPTION.UI
{
    public partial class InventoryExpense : System.Web.UI.Page, IPagePermission
    {
        #region [Public Variables and Constants]
        public const string GROSS_COMMISSION_FORMULAE =
              "Result=Sold Price - Car cost(Expense Amount) If Result > {0} then BuyerCommission = {1} if Result < {2} then BuyerCommission = {3} if between {2} & {0} then Buyer Commission= Exact Result";

        const string EXPENSETYPEID_CARCOST = "50";
        const string EXPENSETYPEID_COMMISSION = "12";
        const string EXPENSETYPEID_PAYOFF = "52";
        const string ENTITY_TYPEID_VENDOR = "3";
        const string ENTITY_TYPEID_BUYER = "2";
        const string ENTITY_TYPEID_DEALER = "1";
        const string blankSpace = "&nbsp;";
        public const string PAGE = "INVENTORYEXPENSE";
        public const string INVENTORYEXPENSE_INVENTORYEXPENSE_ADD = "INVENTORYEXPENSE.ADD";
        public const string INVENTORYEXPENSE_INVENTORYEXPENSE_VIEW = "INVENTORYEXPENSE.VIEW";
        public const string INVENTORYEXPENSE_INVENTORYEXPENSE_EDIT = "INVENTORYEXPENSE.EDIT";
        public const string INVENTORYEXPENSE_INVENTORYEXPENSE_DELETE = "INVENTORYEXPENSE.DELETE";
        long Code = -1;
        long PreExpID = -1;
        long ExpID = -1;
        public long DuplicateExpensePeriod = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DuplicateExpensePeriod"]);
        //public String DuplicateExpenseMessage = System.Configuration.ConfigurationManager.AppSettings["DuplicateExpenseMessage"];
        #endregion
        ExpenseBAL ExpBAL = new ExpenseBAL();

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

            if (!Page.IsPostBack)
            {
                try
                {
                    if (Request.QueryString["Code"] != null && Request.QueryString["Code"].ToString() != "")
                    {
                        Util.Validate_QueryString_Value(6, Request.QueryString["Code"].ToString(), Constant.OrgID);
                    }
                }
                catch { }
            }
            //Check Logged-In User Page Level Permission
            CheckUserPagePermissions();

            //Get InventoryId for Which Expense belongs
            if (!string.IsNullOrEmpty(Request.QueryString["Code"]))
                try
                {
                    Code = Convert.ToInt64(Request.QueryString["Code"]);

                    //Get PreExpenseID
                    InventoryBAL bal = new InventoryBAL();
                    DataTable dt = (DataTable)bal.Exp_PreExp_ByInventoryID(Code);
                    if (dt.Rows.Count > 0)
                    {
                        PreExpID = Convert.ToInt64(dt.Rows[0]["PreExpenseID"]);
                        ExpID = Convert.ToInt64(dt.Rows[0]["ExpenseID"]);
                    }
                }
                catch { }

            // Pre-Expense Image count
            PreExpenseBAL expBal = new PreExpenseBAL();
            Int64 TotalExpImages = expBal.GetPreExpImagesByInventoryIDCount(Code);

            if (TotalExpImages == 0)
                ibtncars1.Visible = false;
            else
            {
                ibtncars1.Visible = true;
                ibtncars1.ToolTip = TotalExpImages + " Expense Image(s)";
            }

            //Do nothing if code=-1
            if (Code == -1)
                return;
            if (!IsPostBack)
            {
                if (Request.UrlReferrer != null)
                    hlnkBack.NavigateUrl = Request.UrlReferrer.ToString();

                //Show Inventory Header Information which tell about teh current expense being edited for which inventory
                lblInventoryHeader.Text = "Inventory Expenses For " + InventoryBAL.GetCurrentInventoryHeader(Code) + " (Code:" + Code + ")";

                BindExpenseType();

                //By Default load Vendors
                BindEntities();

                //Show Expenses for Current Inventory
                FillExpenseForInventoryId(Code);

                //Bind Linked Cars
                BindLinkedCars(Code);
            }
        }

        /// <summary>
        /// Bind Linked Cars Details
        /// </summary>
        /// <param name="inventoryId"></param>
        private void BindLinkedCars(long inventoryId)
        {
            CarDetailsSelectResult objCarDetails = InventoryBAL.GetCarDetail(inventoryId);
            if (objCarDetails != null && !string.IsNullOrEmpty(objCarDetails.VIN))
            {
                //if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
                //{

                //    String ParentEntityID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString();
                //    gvLinkedFilter.DataSource = InventoryBAL.SearchInventoryLinked(inventoryId, objCarDetails.VIN, Convert.ToInt32(Session["empId"]), Convert.ToInt32(ParentEntityID), Convert.ToInt32(Session["LoginEntityTypeID"]));
                //    gvLinkedFilter.DataBind();
                //}
                //else
                //{
                gvLinkedFilter.DataSource = InventoryBAL.SearchInventoryLinked(inventoryId, objCarDetails.VIN, Constant.OrgID);
                gvLinkedFilter.DataBind();
                //  }
                //Make Linked Car Heading Visible, if there was any linked car entries
                if (gvLinkedFilter.Rows.Count > 0)
                    dvLinkedCars.Visible = true;
            }
        }


        /// <summary>
        /// Bind all expense types
        /// </summary>
        protected void BindExpenseType()
        {
            //Bind Expense Type Items in dropdownlist
            ddlExpenseType.DataSource = objExpenseTypes;
            ddlExpenseType.DataBind();
        }

        #region IPagePermission Members
        /// <summary>
        /// This method checks Page Level Permission for logged-in user
        /// </summary>
        public void CheckUserPagePermissions()
        {
            //Get Permissions for User on this page
            System.Collections.Generic.List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, PAGE);

            //Check If any permission found for this page 
            if (dict == null || dict.Count < 1)
                Response.Redirect("Permission.aspx?MSG=INVENTORYEXPENSE:ADD/EDIT/VIEW/DELETE");

            //Disable Add Link, If No Rights
            if (!dict.Contains(INVENTORYEXPENSE_INVENTORYEXPENSE_ADD))
            {
                lnkAddNewExpense.Visible = false;
            }

            else if (!dict.Contains(INVENTORYEXPENSE_INVENTORYEXPENSE_EDIT))
            {
                //EnableDisableEditButtons(false);
                Button btnEdit = gvExpenses.FindControl("imbEditExp") as Button;
                btnEdit.Visible = false;
            }
            else if (!dict.Contains(INVENTORYEXPENSE_INVENTORYEXPENSE_DELETE))
            {
                Button btnDelete = gvExpenses.FindControl("imgDelete") as Button;
                btnDelete.Visible = false;
            }
        }

        #endregion

        /// <summary>
        /// By default always bind vendors
        /// </summary>
        protected void Default_Binding()
        {
            ddlEntity.Items.Clear();
            InventoryBAL objBAL = new InventoryBAL();
            ddlEntity.DataSource = objBAL.GetVendorList(Constant.OrgID);
            this.ddlEntity.DataValueField = "VendorId";
            this.ddlEntity.DataTextField = "VendorName";
            ddlEntity.DataBind();

            //Add Default Value(Blank Option)
            ListItem lstDefault = new ListItem();
            lstDefault.Text = "";
            lstDefault.Value = "-1";
            ddlEntity.Items.Insert(0, lstDefault);

            ddlExpenseType.Items.Clear();
            //Bind default Expense types
            BindExpenseType();

        }

        /// <summary>
        /// Bind Entity either Buyer/Vendor/Dealer based on EntityType Selected in first dropdownlist
        /// </summary>
        private void BindEntities()
        {
            InventoryBAL objBAL = new InventoryBAL();

            //Clear Expense items
            ddlExpenseType.Items.Clear();

            //Bind Expense type for vendor/Buyers
            BindExpenseType();

            //Clear Entity DropDownlist before loading buyers or vendors
            ddlEntity.Items.Clear();

            //Load Buyers in dropdownlist
            if (ddlEntityType.SelectedValue == ENTITY_TYPEID_BUYER)
            {
                ddlEntity.DataSource = Common.GetBuyerList(Constant.OrgID);
                this.ddlEntity.DataValueField = "BuyerId";
                this.ddlEntity.DataTextField = "BuyerName";
                ddlEntity.DataBind();
            }
            //Load Vendors in dropdownlist
            else if (ddlEntityType.SelectedValue == ENTITY_TYPEID_VENDOR)
            {
                ddlEntity.DataSource = objBAL.GetVendorList(Constant.OrgID);
                this.ddlEntity.DataValueField = "VendorId";
                this.ddlEntity.DataTextField = "VendorName";
                ddlEntity.DataBind();
            }

            else if (ddlEntityType.SelectedValue == ENTITY_TYPEID_DEALER)
            {

                //Bind Dealer associated with inventory in Entity dropdownlist
                ListItem lstInvDealerId = new ListItem();
                DealerDetailsSelectResult objDealerInfo = InventoryBAL.GetDealerForInv(Code);
                if (objDealerInfo != null && objDealerInfo.DealerId != 0)
                {
                    lstInvDealerId.Text = objDealerInfo.DealerName;
                    lstInvDealerId.Value = objDealerInfo.DealerId.ToString();
                    ddlEntity.Items.Insert(0, lstInvDealerId);
                    //Apply Dealer Name as selected value
                    if (ddlEntity.Items.Count == 1)
                    {
                        ddlEntity.SelectedIndex = 0;
                        //Set Expense Type Options to "Pay-OFF"
                        ddlExpenseType.Items.Clear();
                        ListItem lstPayOffOption = new ListItem();
                        lstPayOffOption.Text = "PAY OFF";
                        lstPayOffOption.Value = "52";
                        ddlExpenseType.Items.Insert(0, lstPayOffOption);
                    }

                }
            }
            //Add Default Value(Blank Option)
            ListItem lstDefault = new ListItem();
            lstDefault.Text = "";
            lstDefault.Value = "-1";
            ddlEntity.Items.Insert(0, lstDefault);

        }

        /// <summary>
        /// Handle paging in gridview control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvExpenses_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvExpenses.PageIndex = e.NewPageIndex;
            FillExpenseForInventoryId(Code);
        }


        /// <summary>
        /// Bind Expense Grid for particular Inventory
        /// </summary>
        /// <param name="inventoryId"></param>
        private void FillExpenseForInventoryId(long inventoryId)
        {
            //inventoryId = 9580;

            if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
            {
                lnkAddNewExpense.Visible = false;
                btnAddInvoice.Visible = false;
            }
            //    String ParentEntityID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString();

            //    DataTable dt = InventoryBAL.Expenses_ByInventoryId(inventoryId, Convert.ToInt32(Session["UserEntityID"]), Convert.ToInt32(ParentEntityID), Convert.ToInt32(Session["LoginEntityTypeID"]));

            //    gvExpenses.DataSource = dt;
            //    gvExpenses.DataBind();
            //}
            //else
            //{
            gvExpenses.DataSource = InventoryBAL.Expenses_ByInventoryId(inventoryId, Constant.OrgID);
            gvExpenses.DataBind();
            //}

        }

        protected void btnAddExpense_Click(object sender, EventArgs e)
        {
            //if InventoryId not provided return
            if (Code == -1)
                return;

            InventoryBAL objBAL = new InventoryBAL();
            Expense objExp = new Expense();

            //Check duplicate expense
            long EntityID = Convert.ToInt64(ddlEntity.SelectedValue);
            Int32 EntityTypeID = Convert.ToInt32(ddlEntityType.SelectedValue);
            Int32 ExpenseID = Convert.ToInt32(ddlExpenseType.SelectedValue);
            DataTable dtDupExpense = objBAL.GetDuplicateExpenseDetails(EntityID, EntityTypeID, ExpenseID, txtExpenseAmount.Text, Code, 30);

            if (dtDupExpense.Rows.Count > 0)
            {
                MPEAddExpense.Show();
                divMsg.InnerHtml = String.Format("<a href='DuplicateExpense.aspx?C1={0}&C2={1}&C3={2}&C4={3}&C5={4}&C6={5}' style='color:red;' target='_blank'>Duplicate expense, click me to view detail</a>", EntityID, EntityTypeID, ExpenseID, txtExpenseAmount.Text, Code, DuplicateExpensePeriod);
                btnAddExpense.Enabled = false;
                btnContinue.Visible = true;
            }
            else
            {
                //ADD EXPENSE
                if (hdExpenseUpId.Value == "-1")
                {
                    if (!string.IsNullOrEmpty(txtExpenseAmount.Text.Trim()))
                        objExp.ExpenseAmount = Convert.ToDecimal(txtExpenseAmount.Text);
                    else
                        objExp.ExpenseAmount = decimal.Zero;

                    if (!string.IsNullOrEmpty(ddlExpenseType.SelectedValue))
                        objExp.ExpenseTypeId = Convert.ToInt32(ddlExpenseType.SelectedValue);



                    if (!string.IsNullOrEmpty(txtExpenseDate.Text))
                        objExp.ExpenseDate = Convert.ToDateTime(txtExpenseDate.Text);

                    objExp.Comments = txtComments.Text;
                    objExp.InventoryId = Code;

                    objExp.AddedBy = Constant.UserId;
                    objExp.DateAdded = DateTime.Now;

                    //EntityType Id for Dealer=1, Buyer=2 and Vendor=3
                    if (ddlEntityType.SelectedValue == ENTITY_TYPEID_BUYER)
                        objExp.EntityTypeId = Convert.ToInt32(ENTITY_TYPEID_BUYER);

                    else if (ddlEntityType.SelectedValue == ENTITY_TYPEID_VENDOR)
                        objExp.EntityTypeId = Convert.ToInt32(ENTITY_TYPEID_VENDOR);

                    else if (ddlEntityType.SelectedValue == ENTITY_TYPEID_DEALER)
                        objExp.EntityTypeId = Convert.ToInt32(ENTITY_TYPEID_DEALER);

                    if (!string.IsNullOrEmpty(ddlEntity.SelectedValue))
                        objExp.EntityId = Convert.ToInt64(ddlEntity.SelectedValue);

                    if (!string.IsNullOrEmpty(txtInvoice.Text))
                        objExp.InvoiceNo = txtInvoice.Text;

                    objExp.CheckPaid = false; // added by Naushad on 09/23/09 to avoid crash, default to 0

                    objBAL.AddNewExpense(objExp);


                }
                //EDIT EXPENSE
                else
                {


                    #region[Insert TableHistory to maintain Update History]
                    decimal ExpenseAmount;
                    int ExpenseType;
                    string ExpenseDate;
                    string Commnets = string.Empty;
                    decimal ExpenseAmountNew;
                    int ExpenseTypeNew;
                    string ExpenseDateNew;
                    string CommnetsNew = string.Empty;
                    string invoiceNo = string.Empty;
                    string invoiceNoNew = string.Empty;
                    if (ViewState["ExpenseId"] != null)
                    {
                        DataTable dtOldValue = ExpBAL.GetMobileExpenseDetails(Convert.ToInt32(ViewState["ExpenseId"]));

                        int ret;
                        if (dtOldValue.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtOldValue.Rows[0]["ExpenseAmount"]) != txtExpenseAmount.Text)
                            {
                                ExpenseAmountNew = Convert.ToDecimal(txtExpenseAmount.Text);
                                if (!string.IsNullOrEmpty(Convert.ToString(dtOldValue.Rows[0]["ExpenseAmount"])))
                                    ExpenseAmount = Convert.ToDecimal(dtOldValue.Rows[0]["ExpenseAmount"]);
                                else
                                    ExpenseAmount = 0;
                            }
                            else
                            {
                                ExpenseAmountNew = 0;
                                ExpenseAmount = 0;
                            }
                            if (Convert.ToString(dtOldValue.Rows[0]["ExpenseTypeId"]) != ddlExpenseType.SelectedValue)
                            {
                                ExpenseTypeNew = Convert.ToInt32(ddlExpenseType.SelectedValue);
                                ExpenseType = Convert.ToInt32(dtOldValue.Rows[0]["ExpenseTypeId"]);
                            }
                            else
                            {
                                ExpenseTypeNew = -1;
                                ExpenseType = -1;
                            }
                            if (!string.IsNullOrEmpty(txtExpenseDate.Text) && (Convert.ToDateTime(dtOldValue.Rows[0]["ExpenseDate"]).ToString("dd/MM/yy")) != Convert.ToDateTime(txtExpenseDate.Text).ToString("dd/MM/yy"))
                            {
                                ExpenseDateNew = txtExpenseDate.Text;
                                ExpenseDate = Convert.ToString(dtOldValue.Rows[0]["ExpenseDate"]);
                            }
                            else
                            {
                                ExpenseDateNew = "";
                                ExpenseDate = "";
                            }
                            if ((!string.IsNullOrEmpty(txtComments.Text)) && (Convert.ToString(dtOldValue.Rows[0]["Comments"]) != txtComments.Text))
                            {
                                CommnetsNew = txtComments.Text;
                                Commnets = Convert.ToString(dtOldValue.Rows[0]["Comments"]);
                            }
                            else if ((string.IsNullOrEmpty(txtComments.Text)) && (Convert.ToString(dtOldValue.Rows[0]["Comments"]) != txtComments.Text))
                            {
                                CommnetsNew = txtComments.Text;
                                Commnets = Convert.ToString(dtOldValue.Rows[0]["Comments"]);
                            }

                            else
                            {
                                CommnetsNew = "";
                                Commnets = "";
                            }

                            if ((!string.IsNullOrEmpty(txtInvoice.Text)) && (txtInvoice.Text != Convert.ToString(dtOldValue.Rows[0]["InvoiceNo"])))
                            {
                                invoiceNoNew = txtInvoice.Text.Trim();
                                invoiceNo = Convert.ToString(dtOldValue.Rows[0]["InvoiceNo"]).Trim();
                            }
                            else if ((string.IsNullOrEmpty(txtInvoice.Text)) && (txtInvoice.Text != Convert.ToString(dtOldValue.Rows[0]["InvoiceNo"])))
                            {
                                invoiceNoNew = txtInvoice.Text.Trim();
                                invoiceNo = Convert.ToString(dtOldValue.Rows[0]["InvoiceNo"]).Trim();
                            }
                            else
                            {
                                invoiceNoNew = "";
                                invoiceNo = "";
                            }


                            ret = ExpBAL.UpdateHistoryTable(Convert.ToInt32(ViewState["ExpenseId"]), Convert.ToInt32(Constant.UserId), ExpenseAmount, ExpenseType, ExpenseDate, Commnets, ExpenseAmountNew, ExpenseTypeNew, ExpenseDateNew, CommnetsNew, invoiceNo, invoiceNoNew, "InventoryExpense");
                        }
                    }
                    #endregion[End]

                    //Create new instance of class
                    objExp = new Expense();
                    if (!string.IsNullOrEmpty(txtExpenseAmount.Text.Trim()))
                        objExp.ExpenseAmount = Convert.ToDecimal(txtExpenseAmount.Text);
                    else
                        objExp.ExpenseAmount = decimal.Zero;

                    if (!string.IsNullOrEmpty(ddlExpenseType.SelectedValue))
                        objExp.ExpenseTypeId = Convert.ToInt32(ddlExpenseType.SelectedValue);

                    objExp.Comments = txtComments.Text;
                    objExp.ModifiedBy = Constant.UserId;
                    objExp.DateModified = DateTime.Now;

                    if (!string.IsNullOrEmpty(txtExpenseDate.Text))
                        objExp.ExpenseDate = Convert.ToDateTime(txtExpenseDate.Text);


                    if (!string.IsNullOrEmpty(txtInvoice.Text))
                        objExp.InvoiceNo = txtInvoice.Text;
                    //Update Expense
                    objBAL.UpdateExpense(objExp, Convert.ToInt64(hdExpenseUpId.Value));
                }
                FillExpenseForInventoryId(Code);
            }
        }

        protected void ibtnExpenseDelete_Click(object sender, ImageClickEventArgs e)
        {

        }


        /// <summary>
        /// Edit button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imbEditExp_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                divMsg.InnerHtml = "";
                btnContinue.Visible = false;
                btnAddExpense.Enabled = true;
                txtInvoice.Text = "";
                txtComments.Text = "";
                txtExpenseDate.Text = "";

                ImageButton ibtnExpenseEdit = (ImageButton)sender;
                lblHeading.Text = "Edit Expense Details";
                GridViewRow row = (GridViewRow)ibtnExpenseEdit.NamingContainer;
                long ExpenseId = Convert.ToInt64(gvExpenses.DataKeys[row.RowIndex].Value);
                ddlEntity.Enabled = false;

                //Show Data For Editing.
                this.trType.Visible = false;
                this.trVendor.Visible = false;

                hdExpenseUpId.Value = ExpenseId.ToString();
                ViewState["ExpenseId"] = hdExpenseUpId.Value;
                //Read Expense amount from Hidden Field(UnFormatted Text)
                HiddenField hdExpenseAmount = row.FindControl("hdExpenseAmount") as HiddenField;
                HiddenField hdnComments = row.Cells[8].FindControl("hdnComment") as HiddenField;
                if (hdExpenseAmount != null && !string.IsNullOrEmpty(hdExpenseAmount.Value))
                    txtExpenseAmount.Text = hdExpenseAmount.Value;

                else
                {
                    txtExpenseAmount.Text = decimal.Zero.ToString();
                    hdExpenseAmount.Value = decimal.Zero.ToString();
                }

                if (row.Cells[1].Text != blankSpace)
                    txtExpenseDate.Text = row.Cells[1].Text;
                HiddenField hdnExpTypeId = (HiddenField)gvExpenses.Rows[row.RowIndex].FindControl("hdnExpenseTypeId");
                if (!string.IsNullOrEmpty(hdnExpTypeId.Value))
                    ddlExpenseType.SelectedValue = hdnExpTypeId.Value;
                //if (row.Cells[5].Text != blankSpace)
                //    ddlExpenseType.Items.FindByValue(row.Cells[5].Text);

                if (!string.IsNullOrEmpty(hdnComments.Value))
                    txtComments.Text = hdnComments.Value;

                //Show Table Row for Entity Name
                trEntityName.Visible = true;

                // [Change Request 725:Did Change for open Entity Page based on EntityType]
                HyperLink hpylnkOpenEntityPage = row.Cells[2].Controls[0].FindControl("hpylnkOpenEntityPage") as HyperLink;


                //Show Entity Name while editing expense
                if (row.Cells[2].Text != blankSpace)
                    lblEntityName.Text = hpylnkOpenEntityPage.Text;

                HiddenField hdnEntityTypeId = row.FindControl("hdEntityTypeId") as HiddenField;
                if ((!string.IsNullOrEmpty(hdnEntityTypeId.Value)) && (hdnEntityTypeId.Value == "1"))
                {
                    ddlExpenseType.Enabled = false;
                    txtExpenseDate.Enabled = false;
                }
                else
                {
                    ddlExpenseType.Enabled = true;
                    txtExpenseDate.Enabled = true;
                }

                //Disble ExpenseType Dropdownlist for expense of type CARCOST & Commission
                if ((hdnExpTypeId.Value == "50") || (hdnEntityTypeId.Value == "1"))
                    ddlExpenseType.Enabled = false;
                else
                    ddlExpenseType.Enabled = true;

                //if (!string.IsNullOrEmpty(ddlExpenseType.SelectedValue))
                //{
                //    if (ddlExpenseType.SelectedValue == EXPENSETYPEID_CARCOST || ddlExpenseType.SelectedValue == EXPENSETYPEID_COMMISSION)
                //        ddlExpenseType.Enabled = false;
                //    else
                //        ddlExpenseType.Enabled = true;
                //}


                HiddenField hdnInvoiceNumber = row.FindControl("hdnInvoiceNumber") as HiddenField;
                if (!string.IsNullOrEmpty(hdnInvoiceNumber.Value))
                {
                    string[] strvalue = hdnInvoiceNumber.Value.Split('#');
                    if (strvalue.Length > 0)
                        txtInvoice.Text = strvalue[1].Trim();
                    else
                        txtInvoice.Text = "";
                }


                MPEAddExpense.Show();
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// Handle Row DataBound Event of Expense Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvExpenses_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Hide ID columns
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                #region [Change Request 725:Did Change for open Entity Page based on EntityType]
                //Add Navigate Url for Opening Particular Entity Page
                HyperLink hpylnkOpenEntityPage = e.Row.Cells[2].Controls[0].FindControl("hpylnkOpenEntityPage") as HyperLink;
                long entityId = Convert.ToInt64(hpylnkOpenEntityPage.ToolTip);
                //GridViewRow row = (GridViewRow)hpylnkOpenEntityPage.NamingContainer;
                HiddenField hdEntityTypeId = e.Row.Cells[2].Controls[1].FindControl("hdEntityTypeId") as HiddenField;

                //Check EntityId & EntityTypeId and Open relevant Url
                if (entityId > 0 && hdEntityTypeId.Value != null)
                {
                    if (hdEntityTypeId.Value == "1")
                        hpylnkOpenEntityPage.NavigateUrl = "ViewDealer.aspx?Mode=View&EntityId=" + entityId + "&type=1";
                    else if (hdEntityTypeId.Value == "2")
                        hpylnkOpenEntityPage.NavigateUrl = "ViewBuyerDetails.aspx?Mode=View&BuyerId=" + entityId + "&type=2";
                    else if (hdEntityTypeId.Value == "3")
                        hpylnkOpenEntityPage.NavigateUrl = "ViewVendor.aspx?Mode=View&EntityId=" + entityId + "&type=3";
                    else if (hdEntityTypeId.Value == "4")
                        hpylnkOpenEntityPage.NavigateUrl = "ViewUtilityCompany.aspx?Mode=View&EntityId=" + entityId + "&type=4";
                    else if (hdEntityTypeId.Value == "5")
                        hpylnkOpenEntityPage.NavigateUrl = "InventoryDetail.aspx?Code=" + entityId;
                }
                #endregion

                ImageButton imgEdit = e.Row.Cells[8].FindControl("imbEditExp") as ImageButton;
                ImageButton imgDelete = e.Row.Cells[8].FindControl("imgDelete") as ImageButton;
                LinkButton lnkShowBuyerCalculation = e.Row.Cells[5].FindControl("lnkShowBuyerCalculation") as LinkButton;
                Label lblExpenseTyp = e.Row.Cells[5].FindControl("lblExpenseTyp") as Label;
                HiddenField hfPreExpID = (HiddenField)e.Row.FindControl("hfPreExpID");
                ImageButton imgPreExpDetail = (ImageButton)e.Row.FindControl("ibtnPreExpDetail");
                //Display tooltip when user mouseover on row.
                e.Row.ToolTip = imgEdit.ToolTip;

                if (!String.IsNullOrEmpty(hfPreExpID.Value))
                {
                    if (Convert.ToInt64(hfPreExpID.Value) > 0)
                        imgPreExpDetail.Visible = true;
                    else
                        imgPreExpDetail.Visible = false;
                }
                else
                    imgPreExpDetail.Visible = false;

                //If Check is Paid Won't allow user to edit/delete any expense,Make them Visible False
                if (!string.IsNullOrEmpty(e.Row.Cells[7].Text) && e.Row.Cells[7].Text == "Yes")
                {

                    imgEdit.Visible = false;
                    imgDelete.Visible = false;
                }
                //Show Popup only for buyer commission as commissiontype id=12 is also applicable for other entities
                if (e.Row.Cells[5].Text != EXPENSETYPEID_COMMISSION)
                    lnkShowBuyerCalculation.Visible = false;

                else
                    lblExpenseTyp.Visible = false;

                //If Expense is of type commission buyer entitity is other than buyer then dont display popup.
                if (hdEntityTypeId.Value != "2" && e.Row.Cells[5].Text == EXPENSETYPEID_COMMISSION)
                {
                    lnkShowBuyerCalculation.Visible = false;
                    lblExpenseTyp.Visible = true;
                }
                //Disble delete button for expense of type CARCOST & Commission
                if (e.Row.Cells[5].Text == EXPENSETYPEID_CARCOST || e.Row.Cells[5].Text == EXPENSETYPEID_COMMISSION)
                {
                    imgDelete.Visible = false;
                }
                //Change Request 725:Did Change for open Entity Page based on EntityType
                string entityName = hpylnkOpenEntityPage.Text;

                int entityTypeId = 0;
                if (!string.IsNullOrEmpty(e.Row.Cells[9].Text) && e.Row.Cells[9].Text != blankSpace)
                    entityTypeId = Convert.ToInt32(e.Row.Cells[9].Text);
                //Based on EntityType prefix entityname
                switch (entityTypeId)
                {
                    case 1:
                        hpylnkOpenEntityPage.Text = entityName + " " + "(D)";
                        break;
                    case 2:
                        hpylnkOpenEntityPage.Text = entityName + " " + "(B)";
                        break;
                    case 3:
                        hpylnkOpenEntityPage.Text = entityName + " " + "(V)";
                        break;
                    case 4:
                        hpylnkOpenEntityPage.Text = entityName + " " + "(U)";
                        break;
                    case 5:
                        hpylnkOpenEntityPage.Text = entityName + " " + "(E)";
                        break;
                    case 6:
                        hpylnkOpenEntityPage.Text = entityName + " " + "(I)";
                        break;
                }//end switch statement

                // Show/hide car image button
                long ExpenseId = Convert.ToInt64(gvExpenses.DataKeys[e.Row.RowIndex].Value);
                ImageButton ibtncars = e.Row.Cells[8].FindControl("ibtncars") as ImageButton;
                if (ExpID == ExpenseId)
                    ibtncars.Visible = true;
                else
                    ibtncars.Visible = false;

                //////////Added by Rupendra 13 Sep 12 to show Invoive number and comment//////////////
                Label lblInvoiceNumber = (Label)e.Row.FindControl("lblInvoiceNumber");
                HiddenField hdnComments = (HiddenField)e.Row.FindControl("hdnComment");
                if (!string.IsNullOrEmpty(hdnComments.Value))
                    lblInvoiceNumber.Text = hdnComments.Value + "</br><i>" + lblInvoiceNumber.Text + "</i>";
                else
                    lblInvoiceNumber.Text = "<i>" + lblInvoiceNumber.Text + "</i>";
                ///////////////////////End////////////////////////////////////////////////////////////

            }
            //SetExpenseTypeId & EntityTypeId Fields to False
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                //SetExpenseTypeId & EntityTypeId Fields to False
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[9].Visible = false;
                if (Convert.ToString(Session["LoginEntityTypeID"]) == "2" || Convert.ToString(Session["LoginEntityTypeID"]) == "3")
                {
                    e.Row.Cells[0].Visible = false;
                    e.Row.Cells[10].Visible = false;
                }

            }
            //Commission Calculation Display Code
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.EmptyDataRow)
            {
                //Set Inventory Header
                Panel pnlShowCommissionDetails = e.Row.FindControl("pnlShowCommissionDetails") as Panel;
                HyperLink hlnkbuyerName = (HyperLink)e.Row.FindControl("hpylnkOpenEntityPage");
                //  HyperLink hlnkCheckNo = (HyperLink)e.Row.FindControl("hylnk");
                if (pnlShowCommissionDetails == null) return;
                //HiddenField hdInventoryId = e.Row.FindControl("hdInventoryId") as HiddenField;
                Label lblHeaderInventoryInfo = pnlShowCommissionDetails.FindControl("lblHeaderInventoryInfo") as Label;

                //If Header Info Not prepared,prepared header    
                // if (string.IsNullOrEmpty(lblHeaderInventoryInfo.Text))
                lblHeaderInventoryInfo.Text = "Buyer Commission Calculation For " + ' ' + InventoryBAL.GetCurrentInventoryHeader(Code) + " ( Code:" + Code + " )";

                if (Convert.ToString(Session["LoginEntityTypeID"]) == "2" || Convert.ToString(Session["LoginEntityTypeID"]) == "3")
                {
                    // e.Row.Cells[10].Visible = false;
                    hlnkbuyerName.Enabled = false;
                    // hlnkCheckNo.Enabled = false;
                }
            }

        }

        protected void gvLinkedFilter_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.EmptyDataRow)
            {
                HyperLink hlnkInvID = (HyperLink)e.Row.FindControl("hylnk");
                HyperLink hlnkVIN = (HyperLink)e.Row.FindControl("hylnkVIN");
                // HyperLink hlnkCheckNo = (HyperLink)e.Row.FindControl("hylnkCHKNO");
                if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
                {
                    hlnkInvID.Enabled = false;
                    //hlnkVIN.Enabled = false;                   
                }
                if (Convert.ToString(Session["LoginEntityTypeID"]) == "3")
                {
                    hlnkVIN.Enabled = false;
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkAddNewExpense_Click(object sender, EventArgs e)
        {
            divMsg.InnerHtml = "";
            btnContinue.Visible = false;
            btnAddExpense.Enabled = true;
            txtInvoice.Text = "";
            txtComments.Text = "";
            txtExpenseDate.Text = "";
            txtExpenseDate.Text = DateTime.Now.ToString("MM/dd/yy");
            //if InventoryId not provided return
            if (Code == -1)
                return;

            //Show the Popup
            MPEAddExpense.Show();

            Expense objExp = new Expense();

            //Show Data For Editing.
            this.trType.Visible = true;
            this.trVendor.Visible = true;

            //For Vendor Selection
            ddlEntityType.SelectedValue = "3";
            Default_Binding();

            lblHeading.Text = "Add Expense";
            hdExpenseUpId.Value = "-1";
            txtExpenseAmount.Text = string.Empty;
            //ddlExpenseType.SelectedIndex = -1;
            ddlExpenseType.Enabled = true;
            txtComments.Text = string.Empty;
            ddlEntity.Enabled = true;



            //Hide Table Row containing Entity Name
            trEntityName.Visible = false;


        }

        /// <summary>
        /// Mark Flag IsActive=1 for particular ExpenseId
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgDelete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibtnExpenseEdit = (ImageButton)sender;
            //lblHeading.Text = " Edit Expense Details";
            GridViewRow row = (GridViewRow)ibtnExpenseEdit.NamingContainer;
            long ExpenseId = Convert.ToInt64(gvExpenses.DataKeys[row.RowIndex].Value);

            //Get ExpenseId to be deleted(Mark IsActive=0) In db
            // hdDeleteExpenseId.Value = ExpenseId.ToString();

            //Set arributes deletedby/datedeleted as going to delete expense(IsActive=0)
            Expense obj = new Expense();
            obj.IsActive = 0;
            obj.DeletedBy = Constant.UserId;
            obj.DateDeleted = DateTime.Now;

            //Call BAL Method to delete expense
            InventoryBAL objBAL = new InventoryBAL();
            objBAL.DeleteExpense(obj, Convert.ToInt64(ExpenseId));

            //Refresh Page
            FillExpenseForInventoryId(Code);
        }
        /// <summary>
        /// Handle click event of button for moving back to parent screen i.e ManageInventory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void lnkbtnBack_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("InventoryDetail.aspx?Code=" + Code);
        //}

        /// <summary>
        /// Handle Selected Index Changed Event of EntityType DropDownList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindEntities();
        }


        /// <summary>
        /// Handle paging in gridview control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLinkedFilter_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvLinkedFilter.PageIndex = e.NewPageIndex;
            BindLinkedCars(Code);

        }
        /// <summary>
        /// Open Entity Page based on EntityType
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkOpenEntityPage_Click(object sender, EventArgs e)
        {
        }

        protected void frmBCommissionCalculationDetails_DataBound(object sender, EventArgs e)
        {
            FormView frmBCommissionCalculationDetails = (sender) as FormView;
            FormViewRow frmRow = frmBCommissionCalculationDetails.Row;

            //Return if No Row Found
            if (frmRow == null)
                return;

            string test = frmBCommissionCalculationDetails.DataKey["BuyerCommissionId"].ToString();

            ////Get Commission TypeId,based on it Hide/Unhide rows for various formulae desscription
            Label lblCommissionTypeId = frmRow.FindControl("lblCommissionTypeId") as Label;
            Label lblCommissionRuleDesc = frmRow.FindControl("lblCommissionRuleDesc") as Label;

            lblCommissionTypeId.Visible = false;

            HtmlTableRow tr5050 = frmRow.FindControl("tr5050") as HtmlTableRow;
            HtmlTableRow trGrossProfit = frmRow.FindControl("trGrossProfit") as HtmlTableRow;
            HtmlTableRow trFixedCommission = frmRow.FindControl("trFixedCommission") as HtmlTableRow;
            HtmlTableRow tr5050IIndLevelComm = tr5050.FindControl("tr5050IIndLevelComm") as HtmlTableRow;
            HtmlTableRow trReconFee = tr5050.FindControl("trReconFee") as HtmlTableRow;
            Label lblExpensesText = tr5050.FindControl("lblExpensesText") as Label;
            //HtmlTableRow trIIndLevelBuyer = tr5050.FindControl("trIIndLevelBuyer") as HtmlTableRow;

            //Commission Type: Fixed
            if (lblCommissionTypeId.Text == Constant.COMMISSIONTYPE_FIXED)
            {
                tr5050.Visible = false;
                trGrossProfit.Visible = false;

            }
            //Commission Type 50:50 /50:50/2 /50:50 IInd Level
            else if (lblCommissionTypeId.Text == Constant.COMMISSIONTYPE_5050 || lblCommissionTypeId.Text == Constant.COMMISSIONTYPE_5050SPLIT || lblCommissionTypeId.Text == Constant.COMMISSIONTYPE_IINDLEVEL5050)
            {

                //Display other buyer information only if IInd Level 5050
                //if (lblCommissionTypeId.Text == Constant.COMMISSIONTYPE_IINDLEVEL5050 )
                //    tr5050IIndLevelComm.Visible = true;

                //else
                //    tr5050IIndLevelComm.Visible = false;


                //if (tr5050IIndLevelComm.Visible || lblCommissionTypeId.Text == Constant.COMMISSIONTYPE_5050)
                //    trIIndLevelBuyer.Visible = false;
                //else
                //    trIIndLevelBuyer.Visible = true;
                long buyerCommissionId = Convert.ToInt64(frmBCommissionCalculationDetails.DataKey["BuyerCommissionId"]);
                GetBuyerCommSettings_FromTranTableResult objResult = BuyerBAL.GetBuyerCommission_TransactionSetting(buyerCommissionId);

                //If recon fee already posted it will be a part of expenses
                if (objResult.Recon_fee_Expense > 0)
                {
                    lblExpensesText.Text = "Expenses including (Recon Fee : " + String.Format("{0:C}", objResult.Recon_fee_Expense) + ")";
                    trReconFee.Visible = false;
                }
                else
                {
                    trReconFee.Visible = true;
                }

                trFixedCommission.Visible = false;
                trGrossProfit.Visible = false;
            }
            else if (lblCommissionTypeId.Text == Constant.COMMISSIONTYPE_GROSS)
            {
                trFixedCommission.Visible = false;
                tr5050.Visible = false;

                //Prepare Gross Profit Rule from Transaction Table,It could be different from Current Buyer Settings
                string strGrossFormule = GROSS_COMMISSION_FORMULAE;
                GetBuyerCommSettings_FromTranTableResult objResult = BuyerBAL.GetBuyerCommission_TransactionSetting(Convert.ToInt64(frmBCommissionCalculationDetails.DataKey["BuyerCommissionId"]));
                if (objResult == null)
                    return;

                lblCommissionRuleDesc.Text = string.Format(strGrossFormule, objResult.Max_Gross, objResult.MaxValue_Gross,
                                      objResult.Min_Gross, objResult.MinValue_Gross, objResult.Min_Gross, objResult.Max_Gross);

            }
        }


        protected void ibtnPreExpDetail_Click(object sender, ImageClickEventArgs e)
        {
            long ExpID = Convert.ToInt64(gvExpenses.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            BindPreExpenseDetail(ExpID);
            mpePreExpDetail.Show();
        }

        protected void BindPreExpenseDetail(long ExpID)
        {
            PreExpenseBAL bal = new PreExpenseBAL();
            long PreExpID = bal.PreExp_ByExpID(ExpID);
            gvPreExpDetail.DataSource = bal.PreExpenseDetail_ByPreExpenseId(PreExpID);
            gvPreExpDetail.DataBind();
        }

        protected void ibtncars1_Click(object sender, ImageClickEventArgs e)
        {
            frmImage.Attributes.Add("src", String.Format("ExpenseImageGallery.aspx?id={0}", PreExpID));
            mpeImages.Show();
        }

        protected void ibtncars_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibtn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)ibtn.NamingContainer;
            long ExpenseId = Convert.ToInt64(gvExpenses.DataKeys[row.RowIndex].Value);

            frmImage.Attributes.Add("src", String.Format("ExpenseImages.aspx?id={0}", ExpenseId));
            mpeImages.Show();
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            InventoryBAL objBAL = new InventoryBAL();
            Expense objExp = new Expense();

            //ADD EXPENSE
            if (hdExpenseUpId.Value == "-1")
            {
                if (!string.IsNullOrEmpty(txtExpenseAmount.Text.Trim()))
                    objExp.ExpenseAmount = Convert.ToDecimal(txtExpenseAmount.Text);
                else
                    objExp.ExpenseAmount = decimal.Zero;

                if (!string.IsNullOrEmpty(ddlExpenseType.SelectedValue))
                    objExp.ExpenseTypeId = Convert.ToInt32(ddlExpenseType.SelectedValue);



                if (!string.IsNullOrEmpty(txtExpenseDate.Text))
                    objExp.ExpenseDate = Convert.ToDateTime(txtExpenseDate.Text);

                objExp.Comments = txtComments.Text;
                objExp.InventoryId = Code;

                objExp.AddedBy = Constant.UserId;
                objExp.DateAdded = DateTime.Now;

                //EntityType Id for Dealer=1, Buyer=2 and Vendor=3
                if (ddlEntityType.SelectedValue == ENTITY_TYPEID_BUYER)
                    objExp.EntityTypeId = Convert.ToInt32(ENTITY_TYPEID_BUYER);

                else if (ddlEntityType.SelectedValue == ENTITY_TYPEID_VENDOR)
                    objExp.EntityTypeId = Convert.ToInt32(ENTITY_TYPEID_VENDOR);

                else if (ddlEntityType.SelectedValue == ENTITY_TYPEID_DEALER)
                    objExp.EntityTypeId = Convert.ToInt32(ENTITY_TYPEID_DEALER);

                if (!string.IsNullOrEmpty(ddlEntity.SelectedValue))
                    objExp.EntityId = Convert.ToInt64(ddlEntity.SelectedValue);

                if (!string.IsNullOrEmpty(txtInvoice.Text))
                    objExp.InvoiceNo = txtInvoice.Text;


                objExp.CheckPaid = false; // added by Naushad on 09/23/09 to avoid crash, default to 0

                objBAL.AddNewExpense(objExp);






            }
            //EDIT EXPENSE
            else
            {


                #region[Insert TableHistory to maintain Update History]
                decimal ExpenseAmount;
                int ExpenseType;
                string ExpenseDate;
                string Commnets = string.Empty;
                decimal ExpenseAmountNew;
                int ExpenseTypeNew;
                string ExpenseDateNew;
                string CommnetsNew = string.Empty;
                string invoiceNo = string.Empty;
                string invoiceNoNew = string.Empty;
                if (ViewState["ExpenseId"] != null)
                {
                    DataTable dtOldValue = ExpBAL.GetMobileExpenseDetails(Convert.ToInt32(ViewState["ExpenseId"]));

                    int ret;
                    if (dtOldValue.Rows.Count > 0)
                    {
                        if (Convert.ToString(dtOldValue.Rows[0]["ExpenseAmount"]) != txtExpenseAmount.Text)
                        {
                            ExpenseAmountNew = Convert.ToDecimal(txtExpenseAmount.Text);
                            if (!string.IsNullOrEmpty(Convert.ToString(dtOldValue.Rows[0]["ExpenseAmount"])))
                                ExpenseAmount = Convert.ToDecimal(dtOldValue.Rows[0]["ExpenseAmount"]);
                            else
                                ExpenseAmount = 0;
                        }
                        else
                        {
                            ExpenseAmountNew = 0;
                            ExpenseAmount = 0;
                        }
                        if (Convert.ToString(dtOldValue.Rows[0]["ExpenseTypeId"]) != ddlExpenseType.SelectedValue)
                        {
                            ExpenseTypeNew = Convert.ToInt32(ddlExpenseType.SelectedValue);
                            ExpenseType = Convert.ToInt32(dtOldValue.Rows[0]["ExpenseTypeId"]);
                        }
                        else
                        {
                            ExpenseTypeNew = -1;
                            ExpenseType = -1;
                        }
                        if (!string.IsNullOrEmpty(txtExpenseDate.Text) && (Convert.ToDateTime(dtOldValue.Rows[0]["ExpenseDate"]).ToString("dd/MM/yy")) != Convert.ToDateTime(txtExpenseDate.Text).ToString("dd/MM/yy"))
                        {
                            ExpenseDateNew = txtExpenseDate.Text;
                            ExpenseDate = Convert.ToString(dtOldValue.Rows[0]["ExpenseDate"]);
                        }
                        else
                        {
                            ExpenseDateNew = "";
                            ExpenseDate = "";
                        }
                        if ((!string.IsNullOrEmpty(txtComments.Text)) && (Convert.ToString(dtOldValue.Rows[0]["Comments"]) != txtComments.Text))
                        {
                            CommnetsNew = txtComments.Text;
                            Commnets = Convert.ToString(dtOldValue.Rows[0]["Comments"]);
                        }
                        else if ((string.IsNullOrEmpty(txtComments.Text)) && (Convert.ToString(dtOldValue.Rows[0]["Comments"]) != txtComments.Text))
                        {
                            CommnetsNew = txtComments.Text;
                            Commnets = Convert.ToString(dtOldValue.Rows[0]["Comments"]);
                        }

                        else
                        {
                            CommnetsNew = "";
                            Commnets = "";
                        }

                        if ((!string.IsNullOrEmpty(txtInvoice.Text)) && (txtInvoice.Text != Convert.ToString(dtOldValue.Rows[0]["InvoiceNo"])))
                        {
                            invoiceNoNew = txtInvoice.Text.Trim();
                            invoiceNo = Convert.ToString(dtOldValue.Rows[0]["InvoiceNo"]).Trim();
                        }
                        else if ((string.IsNullOrEmpty(txtInvoice.Text)) && (txtInvoice.Text != Convert.ToString(dtOldValue.Rows[0]["InvoiceNo"])))
                        {
                            invoiceNoNew = txtInvoice.Text.Trim();
                            invoiceNo = Convert.ToString(dtOldValue.Rows[0]["InvoiceNo"]).Trim();
                        }
                        else
                        {
                            invoiceNoNew = "";
                            invoiceNo = "";
                        }


                        ret = ExpBAL.UpdateHistoryTable(Convert.ToInt32(ViewState["ExpenseId"]), Convert.ToInt32(Constant.UserId), ExpenseAmount, ExpenseType, ExpenseDate, Commnets, ExpenseAmountNew, ExpenseTypeNew, ExpenseDateNew, CommnetsNew, invoiceNo, invoiceNoNew, "InventoryExpense");
                    }
                }
                #endregion[End]


                //Create new instance of class
                objExp = new Expense();
                if (!string.IsNullOrEmpty(txtExpenseAmount.Text.Trim()))
                    objExp.ExpenseAmount = Convert.ToDecimal(txtExpenseAmount.Text);
                else
                    objExp.ExpenseAmount = decimal.Zero;

                if (!string.IsNullOrEmpty(ddlExpenseType.SelectedValue))
                    objExp.ExpenseTypeId = Convert.ToInt32(ddlExpenseType.SelectedValue);

                objExp.Comments = txtComments.Text;
                objExp.ModifiedBy = Constant.UserId;
                objExp.DateModified = DateTime.Now;

                if (!string.IsNullOrEmpty(txtExpenseDate.Text))
                    objExp.ExpenseDate = Convert.ToDateTime(txtExpenseDate.Text);

                if (!string.IsNullOrEmpty(txtInvoice.Text))
                    objExp.InvoiceNo = txtInvoice.Text;

                //Update Expense
                objBAL.UpdateExpense(objExp, Convert.ToInt64(hdExpenseUpId.Value));
            }
            FillExpenseForInventoryId(Code);
        }

        protected void lblInventoryHeader_Click(object sender, EventArgs e)
        {
            Response.Redirect("InventoryDetail.aspx?Mode=View&Code=" + Convert.ToInt64(Request.QueryString["Code"]));
        }

        #region [Added by Rupendra 13 Sep 12 to add Invoice no]
        protected void btnInvoiceNumberSave_Click(object sender, EventArgs e)
        {

            try
            {
                if (gvExpenses.Rows.Count > 0)
                {
                    int ret = 0;
                    for (int i = 0; i < gvExpenses.Rows.Count; i++)
                    {
                        CheckBox chk = (CheckBox)gvExpenses.Rows[i].FindControl("chkSelect");
                        HiddenField hdnExpenseId = (HiddenField)gvExpenses.Rows[i].FindControl("hdnExpenseId");
                        if ((chk.Checked == true) && (!string.IsNullOrEmpty(hdnExpenseId.Value)))
                            ret = ExpBAL.UpdateInvoiceNumber(Convert.ToInt32(hdnExpenseId.Value), txtInvoiceNumber.Text);
                    }
                    mpePreExpDetail.Hide();
                    txtInvoiceNumber.Text = "";
                    FillExpenseForInventoryId(Code);
                }
            }
            catch (Exception ex) { }

        }
        #endregion
    }
}
