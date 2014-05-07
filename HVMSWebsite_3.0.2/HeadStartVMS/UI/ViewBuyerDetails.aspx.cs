using System;
using System.Collections;
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
using Microsoft.Reporting;
using Microsoft.Reporting.WebForms;
using METAOPTION.Reports;
using System.Web.Services;

namespace METAOPTION.UI
{
    public partial class ViewBuyer : System.Web.UI.Page
    {
        public string EntityId = string.Empty;
        public const string PAGERIGHT_DELETE = "BUYER.DELETE";
        public const string PAGE = "BUYER";
        String ParentBuyerID = "-1";
        public const string GROSS_COMMISSION_FORMULAE =
            "Result=Sold Price - Car cost(Expense Amount) If Result > {0} then BuyerCommission = {1} if Result < {2} then BuyerCommission = {3} if between {2} & {0} then Buyer Commission= {6}";

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
                if (Request.QueryString["BuyerId"] != null && Request.QueryString["BuyerId"].ToString() != "")
                {
                    Util.Validate_QueryString_Value(2, Request.QueryString["BuyerId"].ToString(), Constant.OrgID); // EntityTypeID=2 is for Buyer
                }
            }

            EntityId = Request["BuyerId"];
            hfBuyerid.Value = EntityId;
            if (!IsPostBack)
            {
                if (Convert.ToString(Session["LoginEntityTypeID"]) == "2" && (Convert.ToString(Session["UserEntityID"]) != Convert.ToString(Request["BuyerId"])))
                    Response.Redirect("~/UI/ViewBuyerDetails.aspx?Mode=View&BuyerID=" + Convert.ToString(Session["UserEntityID"]) + "&type=2");

                if (Convert.ToString(Session["BuyerAccessLevel"]) != "1")                    
                    ParentBuyerID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString();
                ParentID.Value = ParentBuyerID;
                EntityTYpeID.Value = Convert.ToString(Session["LoginEntityTypeID"]);
               // Mode=View&BuyerId=60&type=2
                //BindPurchasedCars();
                //BindCommisionPaid();
                //Bind_BuyerCommCal_Details();    

                //Set total outstanding amount for this buyer in header label of outstanding grid
                lblTotalOutstanding.Text = BuyerBAL.GetBuyerOutstandingAmount(Convert.ToInt64(EntityId)).ToString();

                //Format the amount
                lblTotalOutstanding.Text = "Outstanding (" + String.Format("{0:C}", Convert.ToDecimal(lblTotalOutstanding.Text)) + ")";

                // Check rights to add/edit buyer commission setting
                System.Collections.Generic.List<String> rights = CommonBAL.GetPagePermission(Constant.UserId, "WEBAPP.BUYER.ADDEDITCOMMISSIONRULE");
                if (rights.Count < 1)
                {
                    ibtnEditCommissionSetting.Visible = false;
                    ibtnAudit.Visible = false;
                    lbtnSetupCommission.Visible = false;
                }
                else
                {
                    //If setting exist: show edit/history icon, hide create link
                    BuyerBAL bal = new BuyerBAL();
                    if (bal.IsSettingExists(Convert.ToInt64(hfBuyerid.Value)))
                    {
                        ibtnEditCommissionSetting.Visible = true;
                        ibtnAudit.Visible = true;
                        lbtnSetupCommission.Visible = false;
                    }
                    //If setting doesn't exist: hide edit/history icon, show create link
                    else
                    {
                        ibtnEditCommissionSetting.Visible = false;
                        ibtnAudit.Visible = false;
                        lbtnSetupCommission.Visible = true;
                    }
                }
            }

            if (Request["BuyerId"] != null)
            {
                // type = Request["type"];
                CheckUserPagePermissions();
            }
            //For Delete Buyer Button
            if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
            {
                btnDelete.Visible = false;
                imgDelete.Visible = false;
                imgUnDelete.Visible = false;
                imgArchive.Visible = false;
                imgUnArchive.Visible = false;
                imgbtnEdit.Visible = false;
            }

        }

        ///// <summary>
        ///// Display Buyer Commission Calculation Details in Modal Popup(When user click on  the image icon in grid)
        ///// </summary>
        //protected void Bind_BuyerCommCal_Details()
        //{
        //    Panel pnlShowCommissionDetails = gvExpense.FindControl("pnlShowCommissionDetails") as Panel;
        //    FormView frmBCommissionCalculationDetails = pnlShowCommissionDetails.FindControl("frmBCommissionCalculationDetails") as FormView;
        //    frmBCommissionCalculationDetails.DataSource = BuyerBAL.GetBuyerComm_CalculationInfo(Convert.ToInt64(EntityId), Convert.ToInt64(gvExpense.DataKeys[0].Value));
        //    frmBCommissionCalculationDetails.DataBind();
        //}

        #region IPagePermission Members
        /// <summary>
        /// Check Logged-In User Page Level Permission
        /// </summary>
        public void CheckUserPagePermissions()
        {
            System.Collections.Generic.List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, PAGE);

            //STEP1: Check If any permission found for this page 
            if (dict == null || dict.Count < 1)
                return;

            //Check Delete Right
            if (dict.Contains(PAGERIGHT_DELETE))
            {
                int entityCount = BAL.Common.CanEntityBeDeleted(Convert.ToInt64(Request["BuyerId"]), 2);
                if (entityCount == 0)
                    btnDelete.Visible = true;
                else
                    btnDelete.Visible = false;
            }

        }
        #endregion

        //protected void BindPurchasedCars()
        //{
        //    grdViewCars.DataSource = BAL.BuyerBAL.GetPurchasedCarsByBuyer(Convert.ToInt64(Request["BuyerId"]));
        //    grdViewCars.DataBind();
        //}

        //protected void BindCommisionPaid()
        //{
        //    CommonBAL objCommonBAL = new CommonBAL();
        //    grdCommision.DataSource = objCommonBAL.GetPayments(Convert.ToInt64(Request["BuyerId"]), Convert.ToInt32(Constant.EntityType.Buyer));
        //    grdCommision.DataBind();
        //}

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (CheckBuyerCodeAvailability())
            {
                Button btnUpdate = (Button)sender;
                Panel pnlEditBuyer = (Panel)btnUpdate.Parent;
                FormView frmviewEditBuyer = (FormView)pnlEditBuyer.FindControl("frmviewEditBuyer");
                FormViewRow frmrow = (FormViewRow)frmviewEditBuyer.Row;
                //DropDownList ddlTitle = frmrow.FindControl("ddlTitle") as DropDownList;
                TextBox txtFirstName = frmrow.FindControl("txtFirstName") as TextBox;
                TextBox txtMiddleName = frmrow.FindControl("txtMiddleName") as TextBox;
                TextBox txtLastName = frmrow.FindControl("txtLastName") as TextBox;
                TextBox txtBuyerCode = frmrow.FindControl("txtBuyerCode") as TextBox;
                TextBox txtIdNumber = frmrow.FindControl("txtIdNumber") as TextBox;
                DropDownList ddlPaymentTerms = frmrow.FindControl("ddlPaymentTerms") as DropDownList;

                // TextBox txtAccountingCode = frmrow.FindControl("txtAccountingCode") as TextBox;
                TextBox txtCommisionValue = frmrow.FindControl("txtCommisionValue") as TextBox;
                DropDownList ddlCommisionType = frmrow.FindControl("ddlCommisionType") as DropDownList;
                TextBox txtSSLNumber = frmrow.FindControl("txtSSLNumber") as TextBox;
                TextBox txtLPnumber = frmrow.FindControl("txtLPnumber") as TextBox;
                TextBox txtStreet = frmrow.FindControl("txtStreet") as TextBox;
                TextBox txtSuite = frmrow.FindControl("txtSuite") as TextBox;
                TextBox txtCity = frmrow.FindControl("txtCity") as TextBox;
                DropDownList ddlState = (DropDownList)frmrow.FindControl("ddlState");
                DropDownList ddlCountry = (DropDownList)frmrow.FindControl("ddlCountry");
                TextBox txtZip = frmrow.FindControl("txtZip") as TextBox;
                TextBox txtPhone = frmrow.FindControl("txtPhone") as TextBox;
                TextBox txtCellPhone = frmrow.FindControl("txtCellPhone") as TextBox;

                TextBox txtFax = frmrow.FindControl("txtFax") as TextBox;
                TextBox txtOtherNumber = frmrow.FindControl("txtOtherNumber") as TextBox;
                TextBox txtEmail = frmrow.FindControl("txtEmail") as TextBox;
                DropDownList ddlCGetPaid = frmrow.FindControl("ddlCGetPaid") as DropDownList;
                //TextBox txtAverageCost = frmrow.FindControl("txtAverageCost") as TextBox;
                //TextBox txtReconCost = frmrow.FindControl("txtReconCost") as TextBox;
                TextBox txtComment = frmrow.FindControl("txtComment") as TextBox;
                HiddenField hdnfieldAddressId = frmrow.FindControl("HiddenField1") as HiddenField;

                DropDownList ddlDirectBuyer = (DropDownList)frmrow.FindControl("ddlDirectBuyer");
                DropDownList ddlAccessLevel = (DropDownList)frmrow.FindControl("ddlAccessLevel");
                DropDownList ddlParentBuyer = (DropDownList)frmrow.FindControl("ddlParentBuyer");

                Buyer objBuyer = new Buyer();

                //objBuyer.TitleId = Convert.ToInt32(ddlTitle.SelectedValue);
                objBuyer.FirstName = txtFirstName.Text.Trim();
                objBuyer.MiddleName = txtMiddleName.Text.Trim();
                objBuyer.LastName = txtLastName.Text.Trim();
                objBuyer.Buyer_Code = txtBuyerCode.Text.Trim();
                objBuyer.TaxIdNumber = txtIdNumber.Text.Trim();
                objBuyer.PaymentTermId = Convert.ToInt32(ddlPaymentTerms.SelectedValue);
                // objBuyer.AccountingCode = txtAccountingCode.Text.Trim();
                objBuyer.CommissionTypeId = Convert.ToInt32(ddlCommisionType.SelectedValue);
                if (txtCommisionValue.Text != "")
                    objBuyer.CommissionValue = Convert.ToDecimal(txtCommisionValue.Text.Trim());
                objBuyer.StateSalesmanLicenseNumber = txtSSLNumber.Text.Trim();
                objBuyer.LicensePlateNumber = txtLPnumber.Text.Trim();
                objBuyer.CellPhone = txtCellPhone.Text.Trim();
                objBuyer.ModifiedBy = Constant.UserId;
                objBuyer.CommissionTermId = Convert.ToInt32(ddlCGetPaid.SelectedValue);
                objBuyer.Comments = txtComment.Text.Trim();
                #region[Added By: Ashar on 16 Jan' 2013(Ref: Jan 15 email by Naushad)]
                objBuyer.IsDirectBuyer = Convert.ToBoolean(Convert.ToInt32(ddlDirectBuyer.SelectedValue));
                if (ddlParentBuyer.SelectedValue != "-1")
                    objBuyer.ParentBuyer = Convert.ToInt64(ddlParentBuyer.SelectedValue);
                if (ddlAccessLevel.SelectedValue != "")
                    objBuyer.AccessLevel = Convert.ToInt16(ddlAccessLevel.SelectedValue);
                #endregion
                // create object to address master
                Address objAddress = new Address();
                //assign values to address properties
                objAddress.AddressId = Convert.ToInt64(hdnfieldAddressId.Value);
                objAddress.Street = txtStreet.Text.Trim();
                objAddress.Suite = txtSuite.Text.Trim();
                objAddress.City = txtCity.Text.Trim();
                //Save selected country & address details
                if (!string.IsNullOrEmpty(ddlState.SelectedValue) && ddlState.SelectedValue != "0")
                    objAddress.StateId = Convert.ToInt32(ddlState.SelectedValue);

                if (!string.IsNullOrEmpty(ddlCountry.SelectedValue) && ddlCountry.SelectedValue != "0")
                    objAddress.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
                objAddress.Zip = txtZip.Text.Trim();
                objAddress.Email1 = txtEmail.Text.Trim();
                objAddress.Phone1 = txtPhone.Text.Trim();
                objAddress.Fax = txtFax.Text.Trim();
                objAddress.Phone2 = txtOtherNumber.Text.Trim();

                // objAddress.ModifiedBy = Constant.UserId;

                long BuyerId = Convert.ToInt64(Request["BuyerId"]);
                objBuyer.BuyerId = BuyerId;
                BAL.BuyerBAL.UpdateBuyerDetails(objBuyer, objAddress);
                DataBind();
                //BindPurchsedCars();
                //BindCommisionPaid();
                lblError.Text = "";
            }
            else
            {
                modPopUp.Show();
                lblError.Text = "Buyer code already exist";
            }
        }

        protected void grdViewCars_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdViewCars.PageIndex = e.NewPageIndex;
            //BindPurchasedCars();
        }

        protected void frmviewEditBuyer_DataBound(object sender, EventArgs e)
        {
            if (frmviewEditBuyer.DataItemCount > 0)
            {
                // Find HiddenField for Country & State Value

                // Find HiddenField for Country & State Value

                HiddenField hfieldCountry = frmviewEditBuyer.Row.FindControl("hfieldCountry") as HiddenField;
                HiddenField hfieldState = frmviewEditBuyer.Row.FindControl("hfieldState") as HiddenField;
                DropDownList ddlState = frmviewEditBuyer.Row.FindControl("ddlState") as DropDownList;

                DropDownList ddlCountry = frmviewEditBuyer.Row.FindControl("ddlCountry") as DropDownList;

                //Fill Country Drop Down
                BAL.Common bal = new BAL.Common();
                ddlCountry.DataSource = bal.GetCountryList();
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("", "0"));

                if (ddlCountry.Items.Count < 1)
                    return;

                //Make U.S Country Selection
                if (!string.IsNullOrEmpty(hfieldCountry.Value))
                {
                    if (ddlCountry.Items.FindByValue(hfieldCountry.Value) != null && hfieldCountry.Value != "0")
                        ddlCountry.SelectedValue = hfieldCountry.Value;

                    else
                        ddlCountry.SelectedValue = Constant.US_COUNTRYID;
                }
                else
                    ddlCountry.SelectedValue = Constant.US_COUNTRYID;


                // IF CountryId is not null
                if (!string.IsNullOrEmpty(hfieldCountry.Value) && hfieldCountry.Value != "0")
                {
                    //Fill State DropDown According that countryID
                    ddlState.DataSource = BAL.Common.GetStateList(Convert.ToInt32(hfieldCountry.Value));
                }
                else
                {
                    //Fill State DropDown for U.S.A Default
                    ddlState.DataSource = BAL.Common.GetStateList(233);
                }
                ddlState.DataBind();

                //Insert blank item
                ddlState.Items.Insert(0, new ListItem("", "0"));

                // IF stateId is not null
                if (hfieldState.Value != null && ddlState.Items.FindByValue(hfieldState.Value) != null)
                    ddlState.SelectedValue = hfieldState.Value;

                ////Again Open Popup as its getting closed.
                //modPopUp.Show();
                #region[Added By: Ashar on 16 Jan' 2013(Ref: Jan 15 email by Naushad)]
                HiddenField hfDirectBuyer = (HiddenField)frmviewEditBuyer.Row.FindControl("hfDirectBuyer");
                HiddenField hfAccessLevel = (HiddenField)frmviewEditBuyer.Row.FindControl("hfAccessLevel");
                HiddenField hfParentBuyer = (HiddenField)frmviewEditBuyer.Row.FindControl("hfParentBuyer");
                DropDownList ddlDirectBuyer = (DropDownList)frmviewEditBuyer.Row.FindControl("ddlDirectBuyer");
                DropDownList ddlAccessLevel = (DropDownList)frmviewEditBuyer.Row.FindControl("ddlAccessLevel");
                DropDownList ddlParentBuyer = (DropDownList)frmviewEditBuyer.Row.FindControl("ddlParentBuyer");

                if (Convert.ToBoolean(hfDirectBuyer.Value) == true)
                    ddlDirectBuyer.SelectedValue = "1";
                else if (Convert.ToBoolean(hfDirectBuyer.Value) == false)
                    ddlDirectBuyer.SelectedValue = "0";
                ddlAccessLevel.SelectedValue=hfAccessLevel.Value;

                BuyerBAL objBbal = new BuyerBAL();
                ddlParentBuyer.DataSource = objBbal.GetAllDirectBuyers(Constant.OrgID);
                ddlParentBuyer.DataTextField = "BuyerName";
                ddlParentBuyer.DataValueField = "BuyerId";
                ddlParentBuyer.DataBind();
                ddlParentBuyer.Items.Insert(0, new ListItem("None", "-1"));

                ddlParentBuyer.SelectedValue = hfParentBuyer.Value;
                #endregion
            }
        }

        protected void grdCommision_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCommision.PageIndex = e.NewPageIndex;
            //BindCommisionPaid();
        }

        protected void lnkViewAllPurchasedCars_Click(object sender, EventArgs e)
        {
            ReportParameter[] parameters = new ReportParameter[8];
            parameters[0] = new ReportParameter("startdate", "1/1/1900");
            parameters[1] = new ReportParameter("enddate", DateTime.Now.ToString());
            parameters[2] = new ReportParameter("year", "-1");
            parameters[3] = new ReportParameter("make", "-1");
            parameters[4] = new ReportParameter("model", "-1");
            parameters[5] = new ReportParameter("buyer", Request["BuyerId"].ToString());
            parameters[6] = new ReportParameter("userId", Constant.UserId.ToString());
            //parameters[7] = new ReportParameter("systemId", Session["SystemID"].ToString());
            parameters[7] = new ReportParameter("OrgID", Constant.OrgID.ToString());

            ReportParameters.Parameters = parameters;
            ReportParameters.ReportName = "/Hollenshead/PurchasedCarsByBuyers";

            Response.Redirect("~/VMSReports/Reports.aspx?ReturnURL=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
        }
        /// <summary>
        /// Handle Selected Index changed event for Country Dropdownlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlCountry = frmviewEditBuyer.FindControl("ddlCountry") as DropDownList;
            DropDownList ddlState = frmviewEditBuyer.FindControl("ddlState") as DropDownList;
            if (!string.IsNullOrEmpty(ddlCountry.SelectedValue) && ddlCountry.SelectedValue != "0")
                BindStates(Convert.ToInt32(ddlCountry.SelectedValue), false);
            else
                ddlState.Items.Clear();

        }


        /// <summary>
        /// Bind All States for Provided Country,bydefault it is 1 for U.S
        /// </summary>
        private void BindStates(int CountryId, bool isFromDb)
        {
            if (!isFromDb)
            {
                //Find dropdownlist
                DropDownList ddlState = frmviewEditBuyer.FindControl("ddlState") as DropDownList;
                ddlState.DataSource = METAOPTION.BAL.Common.GetStateList(CountryId);
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("", "0"));
            }

        }
        /// <summary>
        /// Soft Delete Buyer 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            BAL.Common.DeleteEntity(Convert.ToInt64(Request["BuyerId"]), 2, Constant.UserId, DateTime.Now);

            //After Deleting,redirect user to listing page
            Response.Redirect("BuyerList.aspx");
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

                //Code Changes for Drew Commission
                string exactMessage = "Exact Value";
                if (objResult.Exact_Value.HasValue)
                {
                    if (objResult.Exact_Value.Value > 0)
                        exactMessage = Convert.ToString(objResult.Exact_Value);
                    else
                        exactMessage = "Exact Value";
                }
                else
                    exactMessage = "Exact Value";

                lblCommissionRuleDesc.Text = string.Format(strGrossFormule, objResult.Max_Gross, objResult.MaxValue_Gross,
                                      objResult.Min_Gross, objResult.MinValue_Gross, objResult.Min_Gross, objResult.Max_Gross, exactMessage);


            }
        }

        protected void frmviewBuyer_DataBound(object sender, EventArgs e)
        {
            FormViewRow row = frmviewBuyer.Row;

            FormView fv = sender as FormView;
            #region [Added by Rupendra 22 Nov 12 for Check Buyer Exixts or Not]
            HiddenField hfIsBuyerExists = (HiddenField)fv.FindControl("hfIsBuyerExists");
            HiddenField hfIsActive = (HiddenField)fv.FindControl("hfIsActive");

            if (Convert.ToBoolean(hfIsBuyerExists.Value) == true)
            {
                imgDelete.Attributes.Add("style", "display:none;float:right;padding-right:5px;");
                imgUnDelete.Attributes.Add("style", "display:none;float:right;padding-right:5px;");
            }
            else
            {
                if (hfIsActive.Value == "1" || hfIsActive.Value == "2")
                {
                    imgDelete.Attributes.Add("style", "display:block;float:right;padding-right:5px;");
                    imgUnDelete.Attributes.Add("style", "display:none;float:right;padding-right:5px;");
                }
                else if (hfIsActive.Value == "0")
                {
                    imgDelete.Attributes.Add("style", "display:none;float:right;padding-right:5px;");
                    imgUnDelete.Attributes.Add("style", "display:block;float:right;padding-right:5px;");
                }
            }

            if (hfIsActive.Value == "0")
            {
                imgArchive.Attributes.Add("style", "display:block;float:right;padding-right:5px;");
                imgUnArchive.Attributes.Add("style", "display:none;float:right;padding-right:5px;");
            }
            else if (hfIsActive.Value == "1")
            {
                imgArchive.Attributes.Add("style", "display:block;float:right;padding-right:5px;");
                imgUnArchive.Attributes.Add("style", "display:none;float:right;padding-right:5px;");
            }
            else if (hfIsActive.Value == "2")
            {
                imgArchive.Attributes.Add("style", "display:none;float:right;padding-right:5px;");
                imgUnArchive.Attributes.Add("style", "display:block;float:right;padding-right:5px;");
            }
            #endregion


            if (row == null)
                return;
            // Store buyer code in hidden field
            HiddenField hfBCode = row.FindControl("hfBCode") as HiddenField;
            hfBuyerCode.Value = hfBCode.Value;

            HiddenField hdnCommTypeId = row.FindControl("hdnCommTypeId") as HiddenField;
            Label lblRule = row.FindControl("lblCommissionRule") as Label;
            string strGrossFormule = GROSS_COMMISSION_FORMULAE;
            GetBuyerCommissionSettingsResult objResult = BuyerBAL.GetBuyerSpecificCommissionSetting(Convert.ToInt64(EntityId));
            if (objResult == null)
                return;
            //Gross Profit Rule,Prepare formulae based on real variable values from commissionsetting table
            if (hdnCommTypeId.Value == Constant.COMMISSIONTYPE_FIXED)
                lblRule.Text = "Fixed Commission (" + objResult.FixedCommission + ")";

            //Prepare Gross Formulae as per Buyer Current Settings not from transaction table
            else if (hdnCommTypeId.Value == Constant.COMMISSIONTYPE_GROSS)
                lblRule.Text = string.Format(strGrossFormule, objResult.Max_Gross, objResult.MaxValue_Gross,
                                      objResult.Min_Gross, objResult.MinValue_Gross, objResult.Min_Gross, objResult.Max_Gross, "Exact Value");




        }

        protected void gvExpense_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.EmptyDataRow)
            {
                //Set Inventory Header
                Panel pnlShowCommissionDetails = e.Row.FindControl("pnlShowCommissionDetails") as Panel;
                if (pnlShowCommissionDetails == null) return;
                HiddenField hdInventoryId = e.Row.FindControl("hdInventoryId") as HiddenField;
                Label lblHeaderInventoryInfo = pnlShowCommissionDetails.FindControl("lblHeaderInventoryInfo") as Label;

                //If Header Info Not prepared,prepared header    
                // if (string.IsNullOrEmpty(lblHeaderInventoryInfo.Text))
                lblHeaderInventoryInfo.Text = "Buyer Commission Calculation For " + ' ' + InventoryBAL.GetCurrentInventoryHeader(Convert.ToInt64(hdInventoryId.Value)) + " ( Code:" + hdInventoryId.Value + " )";
            }
        }
        /// <summary>
        /// Handle page index changing event for pagination in gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvExpense_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvExpense.PageIndex = e.NewPageIndex;

        }

        private bool CheckBuyerCodeAvailability()
        {
            TextBox buyerCode = (TextBox)(frmviewEditBuyer.FindControl("txtBuyerCode"));
            if (!string.IsNullOrEmpty(buyerCode.Text))
            {
                if (hfBuyerCode.Value == buyerCode.Text.Trim())
                    return true;
                else
                {
                    if (BuyerBAL.BuyerCodeAvailability(buyerCode.Text.Trim()) > 0)// Check if BuyerCode already exists
                        return false;
                    else
                        return true;
                }
            }
            else
                return false;
        }

        #region[Update commission settings]
        protected void btnUpdateCommissionSetting_Click(object sender, EventArgs e)
        {
            Button btnUpdate = (Button)sender;
            Panel pnlEditBuyer = (Panel)btnUpdate.Parent;
            FormView fvEditCommissionSetting = (FormView)pnlEditBuyer.FindControl("fvEditCommissionSetting");
            FormViewRow frmrow = (FormViewRow)fvEditCommissionSetting.Row;
            TextBox txtMinGrossEdit = (TextBox)frmrow.FindControl("txtMinGrossEdit");
            TextBox txtMinValueGrossEdit = (TextBox)frmrow.FindControl("txtMinValueGrossEdit");
            TextBox txtMaxGrossEdit = (TextBox)frmrow.FindControl("txtMaxGrossEdit");
            TextBox txtMaxValueGrossEdit = (TextBox)frmrow.FindControl("txtMaxValueGrossEdit");
            TextBox txtExactValueEdit = (TextBox)frmrow.FindControl("txtExactValueEdit");
            TextBox txtTitlefee5050Edit = (TextBox)frmrow.FindControl("txtTitlefee5050Edit");
            TextBox txtReconfee5050Edit = (TextBox)frmrow.FindControl("txtReconfee5050Edit");
            TextBox txtInventoryIdEdit = (TextBox)frmrow.FindControl("txtInventoryIdEdit");
            TextBox txtFixedCommissionEdit = (TextBox)frmrow.FindControl("txtFixedCommissionEdit");
            TextBox txtSecondBuyerCommission5050SplitEdit = (TextBox)frmrow.FindControl("txtSecondBuyerCommission5050SplitEdit");
            long buyerID = Convert.ToInt64(hfBuyerid.Value);

            BuyerBAL bal = new BuyerBAL();
            /* All parameters are made nullable because user may leave it blank */ 
            bal.UpdateBuyerCommissionSettings(buyerID
                , String.IsNullOrEmpty(txtMinGrossEdit.Text) ? (Int32?)null : Convert.ToInt32(txtMinGrossEdit.Text)
                , String.IsNullOrEmpty(txtMinValueGrossEdit.Text) ? (Int32?)null : Convert.ToInt32(txtMinValueGrossEdit.Text)
                , String.IsNullOrEmpty(txtMaxGrossEdit.Text) ? (Int32?)null : Convert.ToInt32(txtMaxGrossEdit.Text)
                , String.IsNullOrEmpty(txtMaxValueGrossEdit.Text) ? (Int32?)null : Convert.ToInt32(txtMaxValueGrossEdit.Text)
                , String.IsNullOrEmpty(txtExactValueEdit.Text) ? (Int32?)null : Convert.ToInt32(txtExactValueEdit.Text)
                , String.IsNullOrEmpty(txtTitlefee5050Edit.Text) ? (decimal?)null : Convert.ToDecimal(txtTitlefee5050Edit.Text)
                , String.IsNullOrEmpty(txtReconfee5050Edit.Text) ? (decimal?)null : Convert.ToDecimal(txtReconfee5050Edit.Text)
                , String.IsNullOrEmpty(txtInventoryIdEdit.Text) ? (Int32?)null : Convert.ToInt32(txtInventoryIdEdit.Text)
                , String.IsNullOrEmpty(txtFixedCommissionEdit.Text) ? (decimal?)null : Convert.ToDecimal(txtFixedCommissionEdit.Text)
                , String.IsNullOrEmpty(txtSecondBuyerCommission5050SplitEdit.Text) ? (decimal?)null : Convert.ToDecimal(txtSecondBuyerCommission5050SplitEdit.Text)
                , Constant.UserId);
            
            // Bind the Commission grid
            //gvCommissionSettings.DataBind();
            // Reload the page so that Commission Rule reflects in the BuyerDetail section
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
        #endregion

        #region[Audit]
        protected void ibtnAudit_Click(object sender, ImageClickEventArgs e)
        {
            BindAuditGrid();
            mpeAudit.Show();
        }

        private void BindAuditGrid()
        {
            BuyerBAL bal = new BuyerBAL();
            gvAudit.DataSource = bal.BuyerCommissionHistory(Convert.ToInt64(hfBuyerid.Value));
            gvAudit.DataBind();
        }

        protected void gvAudit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAudit.PageIndex = e.NewPageIndex;
            BindAuditGrid();
            mpeAudit.Show();
        }
        #endregion

        #region[Setup commission setting]
        protected void btnSaveCommissionSetting_Click(object sender, EventArgs e)
        {
            BuyerCommissionSetting objCommission = new BuyerCommissionSetting();
            objCommission.BuyerId = Convert.ToInt64(hfBuyerid.Value);
            objCommission.Min_Gross = String.IsNullOrEmpty(txtMinGross.Text) ? (Int32?)null : Convert.ToInt32(txtMinGross.Text);
            objCommission.MinValue_Gross = String.IsNullOrEmpty(txtMinValueGross.Text) ? (Int32?)null : Convert.ToInt32(txtMinValueGross.Text);
            objCommission.Max_Gross = String.IsNullOrEmpty(txtMaxGross.Text) ? (Int32?)null : Convert.ToInt32(txtMaxGross.Text);
            objCommission.MaxValue_Gross = String.IsNullOrEmpty(txtMaxValueGross.Text) ? (Int32?)null : Convert.ToInt32(txtMaxValueGross.Text);
            objCommission.Exact_Value = String.IsNullOrEmpty(txtExactValue.Text) ? (Int32?)null : Convert.ToInt32(txtExactValue.Text);
            objCommission.Title_fee_5050 = String.IsNullOrEmpty(txtTitlefee5050.Text) ? (decimal?)null : Convert.ToDecimal(txtTitlefee5050.Text);
            objCommission.Recon_fee_5050 = String.IsNullOrEmpty(txtReconfee5050.Text) ? (decimal?)null : Convert.ToDecimal(txtReconfee5050.Text);
            objCommission.InventoryId = String.IsNullOrEmpty(txtInventoryId.Text) ? (Int32?)null : Convert.ToInt32(txtInventoryId.Text);
            objCommission.FixedCommission = String.IsNullOrEmpty(txtFixedCommission.Text) ? (decimal?)null : Convert.ToDecimal(txtFixedCommission.Text);
            objCommission.SecondBuyerCommission_5050Split = String.IsNullOrEmpty(txtSecondBuyerCommission5050Split.Text) ? (decimal?)null : Convert.ToDecimal(txtSecondBuyerCommission5050Split.Text);

            //Insert record in database
            BuyerBAL bal = new BuyerBAL();
            bal.AddBuyerCommissionSetting(objCommission);

            //Bind commission grid
            //gvCommissionSettings.DataBind();
            //Bind commission in edit mode
            //fvEditCommissionSetting.DataBind();
            //show edit/history icon, hide create link
            //ibtnEditCommissionSetting.Visible = true;
            //ibtnAudit.Visible = true;
            //lbtnSetupCommission.Visible = false;

            // Reload the page so that Commission Rule reflects in the BuyerDetail section
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
        #endregion        

        [WebMethod(true)]
        public static void DeleteArchiveBuyer(int Status, Int64 BuyerID, Int64 UserID)
        {
            BAL.BuyerBAL.DeleteArchiveBuyer((Status == -1) ? 1 : Status, BuyerID, UserID);
        }
    }
}