using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using METAOPTION.BAL;
using METAOPTION.WS;

namespace METAOPTION.UI
{
    /*
        Author: Prem Shanker Verma (8800128549)
        Create On: 07-Jan-2013
    */
    public partial class Admin_ManageEntities : System.Web.UI.Page
    {
        BAL.Admin_OrganizationBAL objBal = new BAL.Admin_OrganizationBAL();

        #region Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                BindOrganizations();
                FillEntityTypes();
                BindCountry();
                BindParentBuyers();//for Buyer
                BindEmployeeTypes();
                BindTitles();
                ShowMessage();
                //string str = Request.QueryString["ReturnUrl"].ToString();
                if (Request.QueryString["ReturnUrl"] != null)
                {
                    btnBack.Visible = true;
                }
                else
                {
                    btnBack.Visible = false;
                }
            }
        }
        #endregion

        #region Bind Entity Type Dropdown
        private void FillEntityTypes()
        {
            ddlEntityType.DataSource = objBal.GetRealEntityType_List();
            ddlEntityType.DataTextField = "EntityType1";
            ddlEntityType.DataValueField = "EntityTypeID";
            ddlEntityType.DataBind();
            //ddlEntityType.Items.Insert(0, new ListItem("", ""));
        }
        #endregion

        #region Bind Organization drop down
        private void BindOrganizations()
        {
            BAL.Admin_OrganizationBAL obj = new BAL.Admin_OrganizationBAL();
            ddlOrganization.DataSource = obj.GetOrganizationsList();
            ddlOrganization.DataTextField = "OrgName";
            ddlOrganization.DataValueField = "OrgID";
            ddlOrganization.DataBind();
        }
        #endregion

        #region Bind Country Drop down
        protected void BindCountry()
        {
            BAL.Admin_Common bal = new BAL.Admin_Common();
            List<GetCountryListResult> countryList = bal.GetCountryList(); // it will bring one time country from data base

            ddlBuyerCountry.DataSource = countryList;
            ddlBuyerCountry.DataBind();
            ddlBuyerCountry.Items.Insert(0, new ListItem("", "0"));
            if (ddlBuyerCountry.Items.FindByValue(Constant.US_COUNTRYID) != null)
            {
                ddlBuyerCountry.SelectedValue = Constant.US_COUNTRYID;
                BindStates(Int32.Parse(Constant.US_COUNTRYID));//By Default bind states of U.S
            }

            ddlDealerCountry.DataSource = countryList;
            ddlDealerCountry.DataBind();
            ddlDealerCountry.Items.Insert(0, new ListItem("", "0"));
            if (ddlDealerCountry.Items.FindByValue(Constant.US_COUNTRYID) != null)
            {
                ddlDealerCountry.SelectedValue = Constant.US_COUNTRYID;
            }

            ddlVendorCountry.DataSource = countryList;
            ddlVendorCountry.DataBind();
            ddlVendorCountry.Items.Insert(0, new ListItem("", "0"));
            if (ddlVendorCountry.Items.FindByValue(Constant.US_COUNTRYID) != null)
            {
                ddlVendorCountry.SelectedValue = Constant.US_COUNTRYID;
            }

            ddlCompanyCountry.DataSource = countryList;
            ddlCompanyCountry.DataBind();
            ddlCompanyCountry.Items.Insert(0, new ListItem("", "0"));
            if (ddlCompanyCountry.Items.FindByValue(Constant.US_COUNTRYID) != null)
            {
                ddlCompanyCountry.SelectedValue = Constant.US_COUNTRYID;
            }

            ddlEmployeeCountry.DataSource = countryList;
            ddlEmployeeCountry.DataBind();
            ddlEmployeeCountry.Items.Insert(0, new ListItem("", "0"));
            if (ddlEmployeeCountry.Items.FindByValue(Constant.US_COUNTRYID) != null)
            {
                ddlEmployeeCountry.SelectedValue = Constant.US_COUNTRYID;
            }

            //ddlEmployeeCountry.DataSource = countryList;
            //ddlEmployeeCountry.DataBind();
            //ddlEmployeeCountry.Items.Insert(0, new ListItem("", "0"));
            //if (ddlEmployeeCountry.Items.FindByValue(Constant.US_COUNTRYID) != null)
            //{
            //    ddlEmployeeCountry.SelectedValue = Constant.US_COUNTRYID;
            //}

            //Make U.S Country Selected ByDefault
            //if (ddl.Items.FindByValue(Constant.US_COUNTRYID) != null)
            //{
            //    ddl.SelectedValue = Constant.US_COUNTRYID;
            //    //By Default bind states of U.S
            //    BindStates(233);
            //}
        }
        #endregion

        #region Bind States Drop down
        private void BindStates(int CountryId)
        {
            DropDownList ddl = new DropDownList();
            switch (ddlEntityType.SelectedValue)
            {
                case "1":  //1	Dealer/Customer
                    ddl = ddlDealerState;
                    break;
                case "2":  //2	Buyer	
                    ddl = ddlBuyerState;
                    break;
                case "3": //3	Vendor	
                    ddl = ddlVendorState;
                    break;
                case "4": //4	Utility Company	
                    ddl = ddlCompanyState;
                    break;
                case "5": //5	Employee
                    ddl = ddlEmployeeState;
                    ddlEmployeeDriverLicState.DataSource = BAL.Admin_Common.GetStateList(CountryId);
                    ddlEmployeeDriverLicState.DataBind();
                    ddlEmployeeDriverLicState.Items.Insert(0, new ListItem("", "0"));
                    break;
            }

            ddl.DataSource = BAL.Admin_Common.GetStateList(CountryId);
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("", "0"));
        }
        #endregion

        #region Bind Employee Type Drop Down
        private void BindEmployeeTypes()
        {
            BAL.Admin_Common objCommon = new BAL.Admin_Common();
            ddlEmployeeType.DataSource = objCommon.GetEmployeeType();
            ddlEmployeeType.DataValueField = "EmployeeTypeId";
            ddlEmployeeType.DataTextField = "EmployeeType1";
            ddlEmployeeType.DataBind();
        }
        #endregion

        #region Bind Titles
        /// <summary>
        /// Bind list of titles
        /// </summary>
        private void BindTitles()
        {
            BAL.Admin_Common objCommon = new BAL.Admin_Common();
            ddlEmployeeTitle.DataSource = objCommon.GetTitles();
            ddlEmployeeTitle.DataTextField = "Title";
            ddlEmployeeTitle.DataValueField = "TitleId";
            ddlEmployeeTitle.DataBind();
        }

        #endregion

        #region Entity Type Selected Index Changed Event
        protected void ddlEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender != null)
            {
                lblMessage.Text = "";
            }
            tblDealer.Visible = false;
            tblBuyer.Visible = false;
            tblVendor.Visible = false;
            tblUtilityCompany.Visible = false;
            tblEmployee.Visible = false;

            switch (ddlEntityType.SelectedValue)
            {
                case "1":  //1	Dealer/Customer
                    tblDealer.Visible = true;
                    btnAdd.ValidationGroup = "Dealer";
                    //rfvEntityType.ValidationGroup = "Dealer";
                    break;
                case "2":  //2	Buyer	
                    tblBuyer.Visible = true;
                    btnAdd.ValidationGroup = "Buyer";
                    //rfvEntityType.ValidationGroup = "Buyer";
                    break;
                case "3": //3	Vendor	
                    tblVendor.Visible = true;
                    btnAdd.ValidationGroup = "Vendor";
                    //rfvEntityType.ValidationGroup = "Vendor";
                    break;
                case "4": //4	Utility Company	
                    tblUtilityCompany.Visible = true;
                    btnAdd.ValidationGroup = "Company";
                    //rfvEntityType.ValidationGroup = "Company";
                    break;
                case "5": //5	Employee
                    tblEmployee.Visible = true;
                    btnAdd.ValidationGroup = "Employee";
                    //rfvEntityType.ValidationGroup = "Employee";
                    break;
            }

            BindStates(Int32.Parse(Constant.US_COUNTRYID));//By Default bind states of U.S
        }
        #endregion

        #region Add button Click Event
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            Int64 NewEntityID = 0;

            switch (ddlEntityType.SelectedValue)
            {
                case "1":  //1	Dealer/Customer
                    {
                        NewEntityID = AddDealer();
                        if (NewEntityID > 0)
                        {
                            Response.Redirect("Admin_ManageEntities.aspx?type=1&msg=succ" + "&ReturnUrl=" + HttpUtility.UrlEncode(Request["ReturnUrl"]));
                        }
                    }
                    break;
                case "2":  //2	Buyer	
                    {
                        if (CheckBuyerCodeAvailability())
                        {
                            NewEntityID = AddBuyer();
                            if (NewEntityID > 0)
                            {
                                Response.Redirect("Admin_ManageEntities.aspx?type=2&msg=succ" + "&ReturnUrl=" + HttpUtility.UrlEncode(Request["ReturnUrl"]));
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Buyer code already exist ";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    break;
                case "3": //3	Vendor	
                    {
                        NewEntityID = AddVender();
                        if (NewEntityID > 0)
                        {
                            Response.Redirect("Admin_ManageEntities.aspx?type=3&msg=succ" + "&ReturnUrl=" + HttpUtility.UrlEncode(Request["ReturnUrl"]));
                        }
                    }
                    break;
                case "4": //4	Utility Company	
                    {
                        NewEntityID = AddUtilityCompany();
                        if (NewEntityID > 0)
                        {
                            Response.Redirect("Admin_ManageEntities.aspx?type=4&msg=succ" + "&ReturnUrl=" + HttpUtility.UrlEncode(Request["ReturnUrl"]));
                        }
                    }
                    break;
                case "5": //5	Employee
                    {
                        NewEntityID = AddEmployee();
                        if (NewEntityID > 0)
                        {
                            Response.Redirect("Admin_ManageEntities.aspx?type=5&msg=succ" + "&ReturnUrl=" + HttpUtility.UrlEncode(Request["ReturnUrl"]));
                        }
                    }
                    break;
            }
        }
        #endregion

        #region Reset Button click Event
        protected void btnReset_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            //Redirect to same screen to Reset Fields
            if (Request["ReturnUrl"] != null)
            {
                Response.Redirect("Admin_ManageEntities.aspx?type=" + ddlEntityType.SelectedValue + "&ReturnUrl=" + HttpUtility.UrlEncode(Request["ReturnUrl"]));
            }
            else
            {
                Response.Redirect("Admin_ManageEntities.aspx?type=" + ddlEntityType.SelectedValue);
            }
        }
        #endregion

        #region Back Button Click Event
        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (Request["ReturnUrl"] != null)
            {
                Response.Redirect(Request["ReturnUrl"]);
            }
        }
        #endregion

        #region Show message after adding Entity
        private void ShowMessage()
        {
            if (Request.QueryString["msg"] != null)
            {
                lblMessage.Text = "Added Successfully ";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                hScrollPosition.Value = "200";
                //lblMessage.Focus();
            }
            else
            {
                lblMessage.Text = "";
            }
            if (Request.QueryString["type"] != null)
            {
                Int16 typeID = 0;
                if (Int16.TryParse(Request.QueryString["type"].ToString(), out typeID) == false)
                {
                    typeID = 0;
                }

                if (ddlEntityType.Items.FindByValue(typeID.ToString()) != null)
                {
                    ddlEntityType.SelectedValue = typeID.ToString();
                    ddlEntityType_SelectedIndexChanged(null, null);
                }
            }
        }
        #endregion

        #region Country drop down selected index changed Event
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = new DropDownList();
            DropDownList ddlState = new DropDownList();
            switch (ddlEntityType.SelectedValue)
            {
                case "1":  //1	Dealer/Customer
                    ddl = ddlDealerCountry;
                    ddlState = ddlDealerState;
                    break;
                case "2":  //2	Buyer	
                    ddl = ddlBuyerCountry;
                    ddlState = ddlBuyerState;
                    break;
                case "3": //3	Vendor	
                    ddl = ddlVendorCountry;
                    ddlState = ddlVendorState;
                    break;
                case "4": //4	Utility Company	
                    ddl = ddlCompanyCountry;
                    ddlState = ddlCompanyState;
                    break;
                case "5": //5	Employee
                    ddl = ddlEmployeeCountry;
                    ddlState = ddlEmployeeState;
                    break;
            }

            if (!string.IsNullOrEmpty(ddl.SelectedValue) && ddl.SelectedValue != "0")
            {
                BindStates(Convert.ToInt32(ddl.SelectedValue));
            }
            else
            {
                ddlState.Items.Clear();
                if (ddlEntityType.SelectedValue == "5")
                {
                    ddlEmployeeDriverLicState.Items.Clear();
                }
            }
        }
        #endregion

        #region Function to check buyer code existence
        private bool CheckBuyerCodeAvailability()
        {
            if (!string.IsNullOrEmpty(txtBuyerCode.Text))
            {
                if (Admin_BuyerBAL.BuyerCodeAvailability(txtBuyerCode.Text.Trim()) > 0)// Check if BuyerCode already exists
                    return false;
                else
                    return true;
            }
            else
                return false;
        }
        #endregion

        #region[Bind parent buyer dropdown]
        protected void BindParentBuyers()
        {
            Admin_BuyerBAL bal = new Admin_BuyerBAL();
            ddlBuyerParentBuyer.DataSource = bal.GetAllDirectBuyers(Int16.Parse(ddlOrganization.SelectedValue));
            ddlBuyerParentBuyer.DataTextField = "BuyerName";
            ddlBuyerParentBuyer.DataValueField = "BuyerId";
            ddlBuyerParentBuyer.DataBind();
            ddlBuyerParentBuyer.Items.Insert(0, new ListItem("Select", "-1"));
        }
        #endregion

        #region [Call the method to add Buyer]
        /// <summary>
        /// this is the region to call method
        /// </summary>
        protected long AddBuyer()
        {
            //Check if page is valid
            long BuyerId = 0;
            if (Page.IsValid)
            {
                Buyer objBuyer = new Buyer();

                objBuyer.Buyer_Code = txtBuyerCode.Text.Trim();
                objBuyer.FirstName = txtBuyerFirstName.Text.Trim();
                objBuyer.MiddleName = txtBuyerMiddleName.Text.Trim();
                objBuyer.LastName = txtBuyerLastName.Text.Trim();
                objBuyer.TaxIdNumber = txtBuyerIdNumber.Text.Trim();
                if (!string.IsNullOrEmpty(ddlBuyerPaymentTerms.SelectedValue))
                    objBuyer.PaymentTermId = Convert.ToInt32(ddlBuyerPaymentTerms.SelectedValue);
                if (!string.IsNullOrEmpty(ddlBuyerCommisionType.SelectedValue))
                    objBuyer.CommissionTypeId = Convert.ToInt32(ddlBuyerCommisionType.SelectedValue);
                if (!string.IsNullOrEmpty(txtBuyerCommisionValue.Text))
                    objBuyer.CommissionValue = Convert.ToDecimal(txtBuyerCommisionValue.Text.Trim());
                objBuyer.StateSalesmanLicenseNumber = txtBuyerSSLNumber.Text.Trim();
                objBuyer.LicensePlateNumber = txtBuyerLPnumber.Text.Trim();
                objBuyer.CellPhone = txtBuyerCellPhone.Text.Trim();
                if (!string.IsNullOrEmpty(Session["EmpId"].ToString()))
                    objBuyer.AddedBy = Constant.UserId;
                if (!string.IsNullOrEmpty(ddlBuyerCGetPaid.SelectedValue))
                    objBuyer.CommissionTermId = Convert.ToInt32(ddlBuyerCGetPaid.SelectedValue);
                objBuyer.Comments = txtBuyerComment.Text.Trim();
                objBuyer.OrgID = Int16.Parse(ddlOrganization.SelectedValue);

                #region[Added By: Ashar on 16 Jan' 2013(Ref: Jan 15 email by Naushad)]
                objBuyer.IsDirectBuyer = Convert.ToBoolean(Convert.ToInt32(ddlBuyerDirectBuyer.SelectedValue));
                if (ddlBuyerParentBuyer.SelectedValue != "-1")
                    objBuyer.ParentBuyer = Convert.ToInt64(ddlBuyerParentBuyer.SelectedValue);
                if (ddlBuyerAccessLevel.SelectedValue != "")
                    objBuyer.AccessLevel = Convert.ToInt16(ddlBuyerAccessLevel.SelectedValue);
                #endregion
                // create object to address master
                Address objAddress = new Address();
                //assign values to address properties
                objAddress.Street = txtBuyerStreet.Text.Trim();
                objAddress.Suite = txtBuyerSuite.Text.Trim();
                objAddress.City = txtBuyerCity.Text.Trim();

                if (!string.IsNullOrEmpty(ddlBuyerState.SelectedValue) && ddlBuyerState.SelectedValue != "0")
                    objAddress.StateId = Convert.ToInt32(ddlBuyerState.SelectedValue);

                if (!string.IsNullOrEmpty(ddlBuyerCountry.SelectedValue) && ddlBuyerCountry.SelectedValue != "0")
                    objAddress.CountryId = Convert.ToInt32(ddlBuyerCountry.SelectedValue);

                objAddress.Zip = txtBuyerZip.Text.Trim();
                objAddress.Fax = txtBuyerFax.Text.Trim();
                objAddress.Email1 = txtBuyerEmail.Text.Trim();
                objAddress.Phone1 = txtBuyerPhone.Text.Trim();
                objAddress.Phone2 = txtBuyerOtherNumber.Text.Trim();
                //Call the methods to add buyer details
                BuyerId = BAL.Admin_BuyerBAL.AddBuyerDetails(objBuyer, objAddress);
            }
            return BuyerId;
        }
        #endregion

        #region [Method call to add Dealer/Customer Details ]
        /// <summary>
        /// this is the region for insert
        /// Dealer/Customer details
        /// </summary>
        protected long AddDealer()
        {
            long DealerId = 0;
            // Check if the page is valid
            if (Page.IsValid)
            {
                Dealer objDealer = new Dealer();
                objDealer.DealerName = txtDealerName.Text.Trim();
                objDealer.DealerDIN = txtDealerDIN.Text.Trim();
                objDealer.DealerTypeId = Convert.ToInt32(ddlDealerType.SelectedValue);
                objDealer.DealerCategoryId = Convert.ToInt32(ddlDealerCategory.SelectedValue);
                objDealer.DealerSourceId = Convert.ToInt32(ddlDealerSource.SelectedValue);
                objDealer.Comment = txtDealerComment.Text.Trim();
                objDealer.PreferenceSettings = Convert.ToBoolean(1);
                objDealer.ReceiveSms = Convert.ToBoolean(1);
                objDealer.ReceiveEmail = Convert.ToBoolean(1);
                objDealer.AuctionAccessNo = txtDealerAuctionAccessNumber.Text.Trim();
                objDealer.AddedBy = Constant.UserId;
                objDealer.OrgID = Int16.Parse(ddlOrganization.SelectedValue);
                // Create object of Address master
                Address objAddress = new Address();
                objAddress.Street = txtDealerStreet.Text.Trim();
                objAddress.Suite = txtDealerSuite.Text.Trim();
                objAddress.City = txtDealerCity.Text.Trim();

                //Save selected country & address details
                if (!string.IsNullOrEmpty(ddlDealerState.SelectedValue) && ddlDealerState.SelectedValue != "0")
                    objAddress.StateId = Convert.ToInt32(ddlDealerState.SelectedValue);

                if (!string.IsNullOrEmpty(ddlDealerCountry.SelectedValue) && ddlDealerCountry.SelectedValue != "0")
                    objAddress.CountryId = Convert.ToInt32(ddlDealerCountry.SelectedValue);
                objAddress.Zip = txtDealerZip.Text.Trim();
                objAddress.Phone1 = txtDealerPhone.Text.Trim();
                objAddress.Fax = txtDealerFax.Text.Trim();
                objAddress.Phone2 = txtDealerOtherNumber.Text.Trim();
                objAddress.Email1 = txtDealerEmail.Text.Trim();

                DealerId = BAL.Admin_DealerCustomerBAL.AddDealerDetails(objDealer, objAddress);
                // for multiple selection of Franchise
                for (int index = 0; index < lstDealerMake.Items.Count; index++)
                {
                    if (lstDealerMake.Items[index].Selected)
                        BAL.Admin_DealerCustomerBAL.AddFranchise(Convert.ToInt32(lstDealerMake.Items[index].Value), DealerId);
                }
            }
            return DealerId;
        }
        #endregion

        #region [Call method to add Vender Details]
        protected long AddVender()
        {
            long VendorId = 0;
            Vendor objVender = new Vendor();
            Address objAddress = new Address();
            if (Page.IsValid)
            {
                objVender.VendorName = txtVendorName.Text.Trim();
                objVender.VendorDIN = txtVendorDIN.Text.Trim();
                if (!string.IsNullOrEmpty(ddlVendorCategory.SelectedValue))
                    objVender.VendorCategoryId = Convert.ToInt32(ddlVendorCategory.SelectedValue);
                objVender.TaxIdNumber = txtVendorIdNumber.Text.Trim();
                if (!string.IsNullOrEmpty(ddlVenderType.SelectedValue))
                    objVender.VendorTypeId = Convert.ToInt32(ddlVenderType.SelectedValue);
                if (!string.IsNullOrEmpty(ddlVendorPaymentTerms.SelectedValue))
                    objVender.PaymentTermId = Convert.ToInt32(ddlVendorPaymentTerms.SelectedValue);
                objVender.Comments = txtVendorComment.Text.Trim();
                if (!string.IsNullOrEmpty(Session["EmpId"].ToString()))
                    objVender.AddedBy = Constant.UserId;
                objVender.OrgID = Int16.Parse(ddlOrganization.SelectedValue);

                // assign values for address properties
                objAddress.Street = txtVendorStreet.Text.Trim();
                objAddress.Suite = txtVendorSuite.Text.Trim();
                objAddress.City = txtVendorCity.Text.Trim();

                if (!string.IsNullOrEmpty(ddlVendorState.SelectedValue) && ddlVendorState.SelectedValue != "0")
                    objAddress.StateId = Convert.ToInt32(ddlVendorState.SelectedValue);

                if (!string.IsNullOrEmpty(ddlVendorCountry.SelectedValue) && ddlVendorCountry.SelectedValue != "0")
                    objAddress.CountryId = Convert.ToInt32(ddlVendorCountry.SelectedValue);

                objAddress.Zip = txtVendorZip.Text.Trim();
                objAddress.Phone1 = txtVendorPhoneNumber.Text.Trim();
                objAddress.Phone2 = txtVendorOtherNumber.Text.Trim();
                objAddress.Fax = txtVendorFax.Text.Trim();
                objAddress.Email1 = txtVendorEmail.Text.Trim();
                //Added by Ashar on 26 Feb'2013
                objVender.TransExpenseCalculationMethod = Convert.ToInt16(ddlVendorExpenseCalcMethod.SelectedValue);
                VendorId = BAL.Admin_ManageEntitiesBAL.AddVenderDetails(objVender, objAddress);
            }
            return VendorId;

        }
        #endregion

        #region[Method to add Utility Company Details]
        protected long AddUtilityCompany()
        {
            long utilityId = 0;
            try
            {
                // Create object of UtilityCompany Class
                UtilityCompany objUtiComp = new UtilityCompany();
                // Assign company details to UtilityCompany class properties
                objUtiComp.CompanyName = txtCompanyName.Text.Trim();
                objUtiComp.TaxIdNumber = txtCompanyTaxIdNo.Text.Trim();

                if (!string.IsNullOrEmpty(ddlCompanyCategory.SelectedValue))
                    objUtiComp.CompanyCategoryId = Convert.ToInt32(ddlCompanyCategory.SelectedValue);

                if (!string.IsNullOrEmpty(ddlCompanyPaymentFreq.SelectedValue))
                    objUtiComp.PayementFrequencyId = Convert.ToInt32(ddlCompanyPaymentFreq.SelectedValue);

                objUtiComp.AccountNumber = txtCompanyAccountNo.Text.Trim();
                objUtiComp.Comments = txtCompanyComments.Text.Trim();
                objUtiComp.AddedBy = Constant.UserId;
                objUtiComp.DateAdded = DateTime.Now;
                objUtiComp.OrgID = Int16.Parse(ddlOrganization.SelectedValue);

                // Create object of Addredd class
                Address objAddress = new Address();
                // Assign address value to address class properties
                objAddress.Street = txtCompanyStreet.Text.Trim();
                objAddress.Suite = txtCompanySuite.Text.Trim();
                objAddress.City = txtCompanyCity.Text.Trim();

                if (!string.IsNullOrEmpty(ddlCompanyState.SelectedValue) && ddlCompanyState.SelectedValue != "0")
                    objAddress.StateId = Convert.ToInt32(ddlCompanyState.SelectedValue);

                if (!string.IsNullOrEmpty(ddlCompanyCountry.SelectedValue) && ddlCompanyCountry.SelectedValue != "0")
                    objAddress.CountryId = Convert.ToInt32(ddlCompanyCountry.SelectedValue);

                objAddress.Zip = txtCompanyZip.Text.Trim();
                objAddress.Phone1 = txtCompanyPhone.Text.Trim();
                objAddress.Fax = txtCompanyFax.Text.Trim();
                objAddress.Phone2 = txtCompanyOther.Text.Trim();
                objAddress.Email1 = txtCompanyEmail.Text.Trim();

                // Pass the both object into Method
                utilityId = BAL.Admin_ManageEntitiesBAL.AddUlilityCompany(objUtiComp, objAddress);
            }
            catch (Exception ex)
            {
            }
            return utilityId;
        }
        #endregion

        #region[Method to add Employee Details]

        /// <summary>
        /// This method call method of BAL Class to Add New Employee
        /// </summary>
        /// <returns></returns>
        private long AddEmployee()
        {
            long empID;

            Employee objEmployee = new Employee();
            Address objEmpAddress = new Address();
            objEmployee.CellPhone = txtEmployeeCellPhone.Text;
            objEmployee.DriverLicenseNumber = txtEmployeeDriverLicNo.Text.Trim();
            if (!string.IsNullOrEmpty(txtEmployeeLicExpDate.Text))
                objEmployee.DriverLicensExpDate = Convert.ToDateTime(txtEmployeeLicExpDate.Text);

            //Get Driver Licence StaeId
            if (!string.IsNullOrEmpty(ddlEmployeeDriverLicState.SelectedValue) && ddlEmployeeDriverLicState.SelectedValue != "0")
                objEmployee.DriverLicenseStateId = Convert.ToInt32(ddlEmployeeDriverLicState.SelectedValue);

            objEmpAddress.Email1 = txtEmployeeEmail.Text;
            objEmployee.EmployeeCode = txtEmployeeCode.Text;

            if (!string.IsNullOrEmpty(ddlEmployeeType.SelectedValue))
                objEmployee.EmployeeTypeId = Convert.ToInt32(ddlEmployeeType.SelectedValue);

            objEmpAddress.Street = txtEmployeeStreet.Text.Trim();
            objEmpAddress.Suite = txtEmployeeSuite.Text.Trim();
            objEmpAddress.City = txtEmployeeCity.Text;

            if (!string.IsNullOrEmpty(ddlEmployeeState.SelectedValue) && ddlEmployeeState.SelectedValue != "0")
                objEmpAddress.StateId = Convert.ToInt32(ddlEmployeeState.SelectedValue);

            if (!string.IsNullOrEmpty(ddlEmployeeCountry.SelectedValue) && ddlEmployeeCountry.SelectedValue != "0")
                objEmpAddress.CountryId = Convert.ToInt32(ddlEmployeeCountry.SelectedValue);

            objEmpAddress.Zip = txtEmployeeZip.Text.Trim();
            objEmployee.FirstName = txtEmployeeFirstName.Text;
            objEmployee.LastName = txtEmployeeLastName.Text;
            objEmployee.MiddleName = txtEmployeeMiddleName.Text;
            objEmpAddress.Phone1 = txtEmployeePhone.Text;
            objEmpAddress.Phone1Ext = txtEmployeeExt.Text.Trim();
            objEmployee.AddedBy = Constant.UserId;
            objEmployee.DateAdded = DateTime.Now;
            objEmployee.IsActive = 1;
            objEmployee.OrgID = Int16.Parse(ddlOrganization.SelectedValue);
            objEmployee.SpecialPayrollConditions = txtEmployeeSpcPayCondt.Text;
            if (!string.IsNullOrEmpty(ddlEmployeeTitle.SelectedValue))
                objEmployee.TitleId = Convert.ToInt32(ddlEmployeeTitle.SelectedValue);
            //Call BAL Layer method to insert employee record along with address details
            empID = BAL.Admin_ManageEntitiesBAL.AddEmployee(objEmployee, objEmpAddress);
            return empID;
        }
        #endregion

    }
}