using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using METAOPTION;
using METAOPTION.BAL;

namespace METAOPTION.UI
{
    public partial class AddCustomer : System.Web.UI.Page
    {
        Int32 totalRows = 0;
        Int32 totalPreRows = 0;
        Contact objContact = new Contact();
        MasterBAL objMasterBAL = new MasterBAL();

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
            #region[Check Permission]
            CheckPermission();
            #endregion

            #region[Page Load Events]

            SaveControlState();
            CreatePreferenceTable();
            CreateTable();
            if (!IsPostBack)
            {
                BindCountry();
                BindState();
                BindWelcomeTasks();
            }
            #endregion
        }

        #region[Check Permission method]
        /// <summary>
        /// Method that check permision
        /// of User.
        /// </summary>
        private void CheckPermission()
        {
            List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, "CUSTOMERDEALER");
            bool bTrue = true;
            if (!dict.Contains("CUSTOMERDEALER.ADD"))
            {
                // this.gvInventoryList.Columns[1].Visible = false;
                bTrue = false;
            }
            if (!dict.Contains("CUSTOMERDEALER.EDIT"))
            {
                // this.hlnkAddNew.Visible = false;
                bTrue = false;
            }
            if (!(dict.Contains("CUSTOMERDEALER.VIEW") || bTrue))
                Response.Redirect("Permission.aspx?MSG=CUSTOMERDEALER.ADD");


        }
        #endregion

        #region[Bind Country Drop Down and State Drop Down]
        /// <summary>
        /// Bind Country Drop Down and State Drop Down
        /// on the based of Country DropDown
        /// </summary>
        protected void BindCountry()
        {
            //Fill Country Drop Down
            BAL.Common bal = new BAL.Common();
            ddlCountry.DataSource = bal.GetCountryList();
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryId";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("", "0"));
            //Make U.S Country Selected By Default
            if (ddlCountry.Items.FindByValue(Constant.US_COUNTRYID) != null)
                ddlCountry.SelectedValue = Constant.US_COUNTRYID;
        }

        protected void BindState()
        {
            // Fill State Drop Down           
            ddlState.DataSource = BAL.Common.GetStateList(Convert.ToInt32(ddlCountry.SelectedValue));
            ddlState.DataTextField = "State";
            ddlState.DataValueField = "StateId";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("", "0"));
        }

        private void BindWelcomeTasks()
        {
            DealerCustomerBAL bal = new DealerCustomerBAL();
            ddlWelcomeTasks.DataSource = bal.GetWelcomeTasks();
            ddlWelcomeTasks.DataTextField = "Task";
            ddlWelcomeTasks.DataValueField = "WelcomeTaskID";
            ddlWelcomeTasks.DataBind();
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindState();
        }
        #endregion

        #region [Create DataTable for Contact Section]
        private void CreateTable()
        {
            DataTable dTab = new DataTable("DContDetail");
            if (Session["DContDetail"] != null)
            {
                dTab = (DataTable)Session["DContDetail"];
            }
            else
            {
                // Add columns
                dTab.Columns.Add("SNo", System.Type.GetType("System.Int32"));
                dTab.Columns.Add("JobTitleId", System.Type.GetType("System.String"));
                dTab.Columns.Add("FirstName", System.Type.GetType("System.String"));
                dTab.Columns.Add("MiddleName", System.Type.GetType("System.String"));
                dTab.Columns.Add("LastName", System.Type.GetType("System.String"));
                dTab.Columns.Add("ContactTypeId", System.Type.GetType("System.Int64"));
                dTab.Columns.Add("OfficeNo", System.Type.GetType("System.String"));
                dTab.Columns.Add("CellNo", System.Type.GetType("System.String"));
                dTab.Columns.Add("Email", System.Type.GetType("System.String"));

                // Add Default Row
                DataRow newContact = dTab.NewRow();
                newContact["SNo"] = 1;
                dTab.Rows.Add(newContact);
            }

            // Total Rows into table
            totalRows = dTab.Rows.Count;

            // Bind to grid
            gdvContactDetail.DataSource = dTab;

            gdvContactDetail.DataBind();

            // Store current table to session
            Session["DContDetail"] = dTab;
        }
        #endregion

        #region [Create DataTable for Preference Section]
        private void CreatePreferenceTable()
        {
            DataTable dtPref = new DataTable("Preference");
            if (Session["Preference"] != null)
            {
                dtPref = (DataTable)Session["Preference"];
            }
            else
            {
                // Add columns
                dtPref.Columns.Add("SNo", System.Type.GetType("System.Int32"));
                dtPref.Columns.Add("ChkEnable", System.Type.GetType("System.Boolean"));
                dtPref.Columns.Add("MinYear", System.Type.GetType("System.String"));
                dtPref.Columns.Add("MaxYear", System.Type.GetType("System.String"));
                dtPref.Columns.Add("Make", System.Type.GetType("System.Int32"));
                dtPref.Columns.Add("Model", System.Type.GetType("System.Int32"));
                dtPref.Columns.Add("MinMileage", System.Type.GetType("System.String"));
                dtPref.Columns.Add("MaxMileage", System.Type.GetType("System.String"));
                dtPref.Columns.Add("MinPrice", System.Type.GetType("System.String"));
                dtPref.Columns.Add("MaxPrice", System.Type.GetType("System.String"));

                // Add Default Row
                DataRow newPreference = dtPref.NewRow();
                newPreference["SNo"] = 1;
                dtPref.Rows.Add(newPreference);
            }

            // Total Rows into table
            totalPreRows = dtPref.Rows.Count;

            // Bind to grid
            grdPreference.DataSource = dtPref;

            grdPreference.DataBind();

            // Store current table to session
            Session["Preference"] = dtPref;
        }
        #endregion

        #region[Save Control state of Contact & Preference Section]
        protected override object SaveControlState()
        {
            SaveContactDetail();
            SavePrefControlDetails();
            return base.SaveControlState();

        }
        protected void SaveContactDetail()
        {
            DataTable dtContact = new DataTable();
            dtContact = (DataTable)Session["DContDetail"];
            for (int index = 0; index < gdvContactDetail.Rows.Count; index++)
            {
                // Set Job Title Value
                dtContact.Rows[index]["JobTitleId"] = (((DropDownList)gdvContactDetail.Rows[index].FindControl("ddlJobTitle"))).SelectedValue;
                // Set First Name Value
                dtContact.Rows[index]["FirstName"] = (((TextBox)gdvContactDetail.Rows[index].FindControl("txtFirstName"))).Text;
                // Set Middle Name Value
                dtContact.Rows[index]["MiddleName"] = (((TextBox)gdvContactDetail.Rows[index].FindControl("txtMiddleName"))).Text;
                // Set Last Name Value
                dtContact.Rows[index]["LastName"] = (((TextBox)gdvContactDetail.Rows[index].FindControl("txtLastName"))).Text;
                // Set Contact Tyep  Value
                dtContact.Rows[index]["ContactTypeId"] = (((DropDownList)gdvContactDetail.Rows[index].FindControl("ddlContactType"))).SelectedValue;
                // Set Cell Phone Value
                dtContact.Rows[index]["OfficeNo"] = (((TextBox)gdvContactDetail.Rows[index].FindControl("txtOfficeNo"))).Text;
                // Set Office Phone Value
                dtContact.Rows[index]["CellNo"] = (((TextBox)gdvContactDetail.Rows[index].FindControl("txtCellNo"))).Text;
                // Set Email Value
                dtContact.Rows[index]["Email"] = (((TextBox)gdvContactDetail.Rows[index].FindControl("txtEmail"))).Text;
            }
            Session["DContDetail"] = dtContact;
        }
        protected void SavePrefControlDetails()
        {
            DataTable dtPreference = (DataTable)Session["Preference"];
            for (int index = 0; index < grdPreference.Rows.Count; index++)
            {
                DropDownList ddlMake = (DropDownList)grdPreference.Rows[index].FindControl("ddlMake");
                DropDownList ddlModel = (DropDownList)grdPreference.Rows[index].FindControl("ddlModel");

                dtPreference.Rows[index]["ChkEnable"] = ((CheckBox)grdPreference.Rows[index].FindControl("chkEnable")).Checked;
                dtPreference.Rows[index]["MinYear"] = (((DropDownList)grdPreference.Rows[index].FindControl("ddlMinYear"))).SelectedValue;
                dtPreference.Rows[index]["MaxYear"] = (((DropDownList)grdPreference.Rows[index].FindControl("ddlMaxYear"))).SelectedValue;
                if (!string.IsNullOrEmpty(ddlMake.SelectedValue))
                    dtPreference.Rows[index]["Make"] = Convert.ToInt32(ddlMake.SelectedValue);
                if (!string.IsNullOrEmpty(ddlModel.SelectedValue))
                    dtPreference.Rows[index]["Model"] = Convert.ToInt32(ddlModel.SelectedValue);
                dtPreference.Rows[index]["MinMileage"] = (((TextBox)grdPreference.Rows[index].FindControl("txtMinMileage"))).Text;
                dtPreference.Rows[index]["MaxMileage"] = (((TextBox)grdPreference.Rows[index].FindControl("txtMaxMileage"))).Text;
                dtPreference.Rows[index]["MinPrice"] = (((TextBox)grdPreference.Rows[index].FindControl("txtMinPrice"))).Text;
                dtPreference.Rows[index]["MaxPrice"] = (((TextBox)grdPreference.Rows[index].FindControl("txtMaxPrice"))).Text;
            }
            Session["Preference"] = dtPreference;
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
                objDealer.DealerTypeId = Convert.ToInt32(ddlType.SelectedValue);
                objDealer.DealerCategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
                objDealer.DealerSourceId = Convert.ToInt32(ddlSource.SelectedValue);
                //objDealer.AccountingCode = txtAccountingCode.Text.Trim();
                objDealer.Comment = txtComment.Text.Trim();
                objDealer.PreferenceSettings = Convert.ToBoolean(RblPrefSetting.Items[0].Selected);
                objDealer.ReceiveSms = Convert.ToBoolean(rblSmsSetting.Items[0].Selected);
                objDealer.ReceiveEmail = Convert.ToBoolean(rblEmailSetting.Items[0].Selected);

                //Change Request: June 16 2010(TASK:New Fields in Dealer and Inventory)
                objDealer.AuctionAccessNo = txtAuctionAccessNumber.Text.Trim();

                objDealer.AddedBy = Constant.UserId;
                objDealer.OrgID = Constant.OrgID;

                String strWelcomeTasks = "";
                if (!string.IsNullOrEmpty(hWelcomeTasks.Value))
                    strWelcomeTasks = hWelcomeTasks.Value;
                else
                    strWelcomeTasks = "";
               
                //if (ddlWelcomeTasks.SelectedValue == "")
                //{
                //    objDealer.WelcomeTaskID = null;
                //}
                //else
                //{
                //    objDealer.WelcomeTaskID = Byte.Parse(ddlWelcomeTasks.SelectedValue);
                //}

                // Create object of Address master
                Address objAddress = new Address();
                // assign address value to address master properties
                objAddress.Street = txtStreet.Text.Trim();
                objAddress.Suite = txtSuite.Text.Trim();
                objAddress.City = txtCity.Text.Trim();

                //Save selected country & address details
                if (!string.IsNullOrEmpty(ddlState.SelectedValue) && ddlState.SelectedValue != "0")
                    objAddress.StateId = Convert.ToInt32(ddlState.SelectedValue);

                if (!string.IsNullOrEmpty(ddlCountry.SelectedValue) && ddlCountry.SelectedValue != "0")
                    objAddress.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
                objAddress.Zip = txtZip.Text.Trim();
                objAddress.Phone1 = txtPhone.Text.Trim();
                objAddress.Fax = txtFax.Text.Trim();
                objAddress.Phone2 = txtOtherNumber.Text.Trim();
                objAddress.Email1 = txtEmail.Text.Trim();

                DealerId = BAL.DealerCustomerBAL.AddDealerDetails(objDealer, objAddress, strWelcomeTasks);

                // for multiple selection of Franchise

                for (int index = 0; index < lstMake.Items.Count; index++)
                {
                    if (lstMake.Items[index].Selected)
                        BAL.DealerCustomerBAL.AddFranchise(Convert.ToInt32(lstMake.Items[index].Value), DealerId);
                }
                // call method for insert dealer/customer contact details
                AddDealerContactDetails(DealerId);
                if (RblPrefSetting.Items[0].Selected == true)
                    // Insert dealer preference details
                    AddDealerPreferenceDetails(DealerId);
                if (rblSmsSetting.Items[0].Selected == true)
                    // Insert dealer mobile preference
                    AddDealerMobilePreference(DealerId);
                if (rblEmailSetting.Items[0].Selected == true)
                    // Insert dealer Email preference
                    AddDealerEmailPreference(DealerId);
                // remove the session of contact and preference details
                Session["DContDetail"] = null;
                Session["Preference"] = null;

            }
            return DealerId;
        }
        #endregion

        #region [Add Dealer/Customer Details]
        /// <summary>
        /// this is the region to add
        /// dealer/customer details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            long DealerId = AddDealer();
            Response.Redirect("ViewDealer.aspx?EntityId=" + DealerId + "&type=1");
        }
        #endregion

        #region[Add Contact Details of Vender]
        /// <summary>
        /// add contact details of Dealer/Customer
        /// </summary>
        /// <param name="DealerId"></param>
        public void AddDealerContactDetails(long DealerId)
        {
            for (int index = 0; index < gdvContactDetail.Rows.Count; index++)
            {
                //findout control from gridview of contact section
                DropDownList ddlJobTitle = gdvContactDetail.Rows[index].FindControl("ddlJobTitle") as DropDownList;
                TextBox txtFirstName = gdvContactDetail.Rows[index].FindControl("txtFirstName") as TextBox;
                TextBox txtMiddleName = gdvContactDetail.Rows[index].FindControl("txtMiddleName") as TextBox;
                TextBox txtLastName = gdvContactDetail.Rows[index].FindControl("txtLastName") as TextBox;
                DropDownList ddlContactType = gdvContactDetail.Rows[index].FindControl("ddlContactType") as DropDownList;
                TextBox txtOfficeNo = gdvContactDetail.Rows[index].FindControl("txtOfficeNo") as TextBox;
                TextBox txtCellNo = gdvContactDetail.Rows[index].FindControl("txtCellNo") as TextBox;
                TextBox txtEmail = gdvContactDetail.Rows[index].FindControl("txtEmail") as TextBox;

                // assign contact table properties according to values of controls
                objContact.EntityId = DealerId;
                objContact.EntityTypeId = 1;
                objContact.ContactTypeId = Convert.ToInt32(ddlContactType.SelectedValue);
                objContact.JobTitleId = Convert.ToInt32(ddlJobTitle.SelectedValue);
                objContact.FirstName = txtFirstName.Text.Trim();
                objContact.MiddleName = txtMiddleName.Text.Trim();
                objContact.LastName = txtLastName.Text.Trim();
                objContact.OfficePhone = txtOfficeNo.Text.Trim();
                objContact.CellPhone = txtCellNo.Text.Trim();
                objContact.Email = txtEmail.Text.Trim();
                objContact.AddedBy = Constant.UserId;


                //save the Contact details of a row.
                CommonBAL objCommonBAL = new CommonBAL();
                objCommonBAL.AddContactDetails(objContact);


            }
        }
        #endregion

        #region [Add Dealer/Customer Preference details]
        protected void AddDealerPreferenceDetails(long DealerId)
        {
            for (int index = 0; index < grdPreference.Rows.Count; index++)
            {
                CheckBox chkEnable = (CheckBox)grdPreference.Rows[index].FindControl("chkEnable");
                DropDownList ddlMinYear = (DropDownList)grdPreference.Rows[index].FindControl("ddlMinYear");
                DropDownList ddlMaxYear = (DropDownList)grdPreference.Rows[index].FindControl("ddlMaxYear");
                DropDownList ddlMake = (DropDownList)grdPreference.Rows[index].FindControl("ddlMake");
                DropDownList ddlModel = (DropDownList)grdPreference.Rows[index].FindControl("ddlModel");
                TextBox txtMinMileage = (TextBox)grdPreference.Rows[index].FindControl("txtMinMileage");
                TextBox txtMaxMileage = (TextBox)grdPreference.Rows[index].FindControl("txtMaxMileage");
                TextBox txtMinPrice = (TextBox)grdPreference.Rows[index].FindControl("txtMinPrice");
                TextBox txtMaxPrice = (TextBox)grdPreference.Rows[index].FindControl("txtMaxPrice");

                DealerPreference objDealerPreferebce = new DealerPreference();
                objDealerPreferebce.DealerId = DealerId;
                if (!string.IsNullOrEmpty(ddlMinYear.SelectedValue))
                    objDealerPreferebce.yearsFrom = Convert.ToInt32(ddlMinYear.SelectedValue);
                if (!string.IsNullOrEmpty(ddlMaxYear.SelectedValue))
                    objDealerPreferebce.yearsTo = Convert.ToInt32(ddlMaxYear.SelectedValue);
                if (!string.IsNullOrEmpty(ddlMake.SelectedValue))
                    objDealerPreferebce.MakeId = Convert.ToInt64(ddlMake.SelectedValue);
                if (!string.IsNullOrEmpty(ddlModel.SelectedValue))
                    objDealerPreferebce.ModelId = Convert.ToInt64(ddlModel.SelectedValue);
                if (!string.IsNullOrEmpty(txtMinMileage.Text))
                    objDealerPreferebce.MileageMin = Convert.ToInt64(txtMinMileage.Text.Trim());
                if (!string.IsNullOrEmpty(txtMaxMileage.Text))
                    objDealerPreferebce.MileageMax = Convert.ToInt64(txtMaxMileage.Text.Trim());
                if (!string.IsNullOrEmpty(txtMinPrice.Text))
                    objDealerPreferebce.PriceMin = Convert.ToDecimal(txtMinPrice.Text.Trim());
                if (!string.IsNullOrEmpty(txtMaxPrice.Text))
                    objDealerPreferebce.PriceMax = Convert.ToDecimal(txtMaxPrice.Text.Trim());
                objDealerPreferebce.IsEnabled = chkEnable.Checked;
                objDealerPreferebce.AddedBy = Constant.UserId;

                BAL.DealerCustomerBAL.AddDealerPreference(objDealerPreferebce, 1);


            }
        }
        #endregion

        #region [Add Dealer/Customer SMS Preference Details]
        /// <summary>
        /// add dealer/customer Mobile Preference details
        /// </summary>
        /// <param name="DealerId"></param>
        protected void AddDealerMobilePreference(long DealerId)
        {

            DealerMobile objDealerMobile = new DealerMobile();
            //For the first Row of Mobile Preference
            objDealerMobile.DealerId = DealerId;
            objDealerMobile.MobileNo = txtMobile1.Text.Trim();
            objDealerMobile.IsEnable = ChkSmsEnable1.Checked;
            objDealerMobile.AddedBy = Constant.UserId;
            BAL.DealerCustomerBAL.AddDealerMobilePreference(objDealerMobile, 1);

            //For the Second Row of Mobile Preference
            objDealerMobile.DealerId = DealerId;
            objDealerMobile.MobileNo = txtMobile2.Text.Trim();
            objDealerMobile.IsEnable = ChkSmsEnable2.Checked;
            objDealerMobile.AddedBy = Constant.UserId;
            BAL.DealerCustomerBAL.AddDealerMobilePreference(objDealerMobile, 1);
            //For the Third Row of Mobile Preference
            objDealerMobile.DealerId = DealerId;
            objDealerMobile.MobileNo = txtMobile3.Text.Trim();
            objDealerMobile.IsEnable = ChkSmsEnable3.Checked;
            objDealerMobile.AddedBy = Constant.UserId;
            BAL.DealerCustomerBAL.AddDealerMobilePreference(objDealerMobile, 1);

        }
        #endregion

        #region [Add Dealer/Customer Email Preference Details]
        /// <summary>
        /// add dealer/customer email preference details
        /// </summary>
        /// <param name="DealerId"></param>
        protected void AddDealerEmailPreference(long DealerId)
        {

            DealerEmail objDealerEmail = new DealerEmail();
            //For First Row of Email Preference
            objDealerEmail.DealerId = DealerId;
            objDealerEmail.Email = txtEmail1.Text.Trim();
            objDealerEmail.IsEnable = ChkEmailEnable1.Checked;
            objDealerEmail.AddedBy = Constant.UserId;
            BAL.DealerCustomerBAL.AddDealerEmailPreference(objDealerEmail, 1);
            //For Second Row of Email Preference
            objDealerEmail.DealerId = DealerId;
            objDealerEmail.Email = txtEmail2.Text.Trim();
            objDealerEmail.IsEnable = ChkEmailEnable2.Checked;
            objDealerEmail.AddedBy = Constant.UserId;
            BAL.DealerCustomerBAL.AddDealerEmailPreference(objDealerEmail, 1);
            //For Third Row of Email Preference
            objDealerEmail.DealerId = DealerId;
            objDealerEmail.Email = txtEmail3.Text.Trim();
            objDealerEmail.IsEnable = ChkEmailEnable3.Checked;
            objDealerEmail.AddedBy = Constant.UserId;
            BAL.DealerCustomerBAL.AddDealerEmailPreference(objDealerEmail, 1);

        }
        #endregion

        #region[Add & Remove Row from Contact & Preference section]
        protected void btnNewRow_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["DContDetail"] != null)
                AddNewRow();


        }
        protected void AddNewRow()
        {
            DataTable dTab = new DataTable();
            if (Session["DContDetail"] != null)
                dTab = (DataTable)Session["DContDetail"];

            Int32 newId = Convert.ToInt32(dTab.Rows[dTab.Rows.Count - 1]["SNo"]) + 1; ;
            // Add new row to Contact Detail Table
            DataRow newContact = dTab.NewRow();
            newContact["SNo"] = newId;
            dTab.Rows.Add(newContact);

            // Total Rows into table
            totalRows = dTab.Rows.Count;

            // Bind to grid
            gdvContactDetail.DataSource = dTab;
            gdvContactDetail.DataBind();
        }
        protected void REMOVE(object sender, CommandEventArgs e)
        {
            String rowId = e.CommandArgument.ToString();
            if (Session["DContDetail"] != null)
            {
                DataTable dtContact = new DataTable();
                dtContact = (DataTable)Session["DContDetail"];

                // if only one row, not allowed
                if (dtContact.Rows.Count == 1)
                    return;

                for (int Index = 0; Index < dtContact.Rows.Count; Index++)
                    if (dtContact.Rows[Index]["SNo"].ToString() == rowId)
                        dtContact.Rows[Index].Delete();
                Session["DContDetail"] = dtContact;
                CreateTable();
            }
        }
        protected void RemoveRow(object sender, CommandEventArgs e)
        {
            string RowId = e.CommandArgument.ToString();
            if (Session["Preference"] != null)
            {
                DataTable dtPreference = new DataTable();
                dtPreference = (DataTable)Session["Preference"];
                // If only one row, not allowed
                if (dtPreference.Rows.Count == 1)
                    return;
                for (int index = 0; index < dtPreference.Rows.Count; index++)
                    if (dtPreference.Rows[index]["SNo"].ToString() == RowId)
                        dtPreference.Rows[index].Delete();
                Session["Preference"] = dtPreference;
                CreatePreferenceTable();

            }
        }
        protected void ImgbtnAddRow_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["Preference"] != null)
                AddNewPreRow();
        }
        protected void AddNewPreRow()
        {
            DataTable dTab = new DataTable();
            if (Session["Preference"] != null)
                dTab = (DataTable)Session["Preference"];

            Int32 newId = Convert.ToInt32(dTab.Rows[dTab.Rows.Count - 1]["SNo"]) + 1; ;
            // Add new row to Preference Table
            DataRow newPreference = dTab.NewRow();
            newPreference["SNo"] = newId;
            dTab.Rows.Add(newPreference);

            // Total Rows into table
            totalPreRows = dTab.Rows.Count;

            // Bind to grid
            grdPreference.DataSource = dTab;
            grdPreference.DataBind();
        }
        #endregion

        #region[Row Bound Event of Contact & Preference Grid]
        protected void gdvContactDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (totalRows > 0)
            {
                ImageButton ibtn; DataTable dtContact = new DataTable();
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (totalRows - 1 > e.Row.RowIndex)
                    {
                        ibtn = (ImageButton)e.Row.Cells[9].FindControl("btnNewRow");
                        ibtn.Visible = false;
                    }

                    if (totalRows > e.Row.RowIndex)
                    {
                        if (totalRows == 1)
                        {
                            ibtn = (ImageButton)e.Row.Cells[0].FindControl("btnRemove");
                            ibtn.Visible = false;
                        }
                    }

                    DropDownList ddlJobTitle = e.Row.FindControl("ddlJobTitle") as DropDownList;
                    DropDownList ddlContactType = e.Row.FindControl("ddlContactType") as DropDownList;
                    ddlJobTitle.DataSource = objJobTitle;
                    ddlJobTitle.DataBind();
                    ddlContactType.DataSource = objContactType;
                    ddlContactType.DataBind();

                    dtContact = (DataTable)Session["DContDetail"];
                    if (dtContact != null)
                    {
                        ddlJobTitle.SelectedValue = dtContact.Rows[e.Row.RowIndex]["JobTitleId"].ToString();
                        ddlContactType.SelectedValue = dtContact.Rows[e.Row.RowIndex]["ContactTypeId"].ToString();
                    }
                }

            }
        }
        protected void grdPreference_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (totalPreRows > 0)
            {
                ImageButton ibtn; DataTable dtPref = new DataTable();
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (totalPreRows - 1 > e.Row.RowIndex)
                    {
                        ibtn = (ImageButton)e.Row.FindControl("ImgbtnAddRow");
                        ibtn.Visible = false;
                    }

                    if (totalPreRows > e.Row.RowIndex)
                    {
                        if (totalPreRows == 1)
                        {
                            ibtn = (ImageButton)e.Row.FindControl("ImgbtnRemove");
                            ibtn.Visible = false;
                        }
                    }
                    // Findout Drop Down controls in preferenceGrid
                    DropDownList ddlMinYear = e.Row.FindControl("ddlMinYear") as DropDownList;
                    DropDownList ddlMaxYear = e.Row.FindControl("ddlMaxYear") as DropDownList;
                    DropDownList ddlMake = e.Row.FindControl("ddlMake") as DropDownList;
                    DropDownList ddlModel = e.Row.FindControl("ddlModel") as DropDownList;
                    CheckBox ChkEnable = e.Row.FindControl("ChkEnable") as CheckBox;

                    //Fill Year Range Drop Down From DataSource
                    ddlMinYear.DataSource = objYear;
                    ddlMinYear.DataBind();
                    ddlMaxYear.DataSource = objYear;
                    ddlMaxYear.DataBind();



                    dtPref = (DataTable)Session["Preference"];
                    if (dtPref != null)
                    {
                        ddlMinYear.SelectedValue = dtPref.Rows[e.Row.RowIndex]["MinYear"].ToString();
                        ddlMaxYear.SelectedValue = dtPref.Rows[e.Row.RowIndex]["MaxYear"].ToString();

                        //Fill Make Drop Down
                        Int32 YearFrom = Convert.ToInt32(ddlMinYear.SelectedValue);
                        Int32 YearTo = Convert.ToInt32(ddlMaxYear.SelectedValue);
                        ddlMake.DataSource = objMasterBAL.GetMakeList(YearFrom, YearTo);
                        ddlMake.DataBind();

                        if (!string.IsNullOrEmpty(dtPref.Rows[e.Row.RowIndex]["Make"].ToString()))
                            ddlMake.SelectedValue = dtPref.Rows[e.Row.RowIndex]["Make"].ToString();

                        if (dtPref.Rows[e.Row.RowIndex]["ChkEnable"].ToString() != "")
                            ChkEnable.Checked = Convert.ToBoolean(dtPref.Rows[e.Row.RowIndex]["ChkEnable"]);



                        // Fill Model Drop Down
                        if (!string.IsNullOrEmpty(ddlMake.SelectedValue))
                        {
                            ddlModel.DataSource = BAL.Common.GetModel(Convert.ToInt32(ddlMake.SelectedValue));
                            ddlModel.DataBind();
                            if (!string.IsNullOrEmpty(dtPref.Rows[e.Row.RowIndex]["Model"].ToString()))
                                ddlModel.SelectedValue = dtPref.Rows[e.Row.RowIndex]["Model"].ToString();
                        }

                    }


                }

            }
        }
        #endregion
        ///// <summary>
        ///// Override Render event for Invalid Page Postback Event Valdidation error and set unique id
        ///// </summary>
        ///// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow r in gdvContactDetail.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    Page.ClientScript.RegisterForEventValidation
                            (r.UniqueID + "$ctl00");

                    //Page.ClientScript.RegisterForEventValidation
                    //        (r.UniqueID + "$ctl01");
                }
            }

            base.Render(writer);
        }

        #region[Index Changed event of Make & Year DropDown]
        protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlMake = (DropDownList)sender;
            GridViewRow grdrow = (GridViewRow)ddlMake.Parent.Parent;
            DropDownList ddlModel = (DropDownList)grdrow.FindControl("ddlModel");
            ddlModel.DataSource = BAL.Common.GetModel(Convert.ToInt32(ddlMake.SelectedValue));
            ddlModel.DataBind();
        }
        protected void ddlMaxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CreatePreferenceTable();
            DropDownList ddlMaxYear = (DropDownList)sender;
            GridViewRow grdrow = (GridViewRow)ddlMaxYear.Parent.Parent;
            DropDownList ddlMinYear = (DropDownList)grdrow.FindControl("ddlMinYear");
            DropDownList ddlMake = (DropDownList)grdrow.FindControl("ddlMake");
            Int32 YearFrom = Convert.ToInt32(ddlMinYear.SelectedValue);
            Int32 YearTo = Convert.ToInt32(ddlMaxYear.SelectedValue);
            ddlMake.DataSource = objMasterBAL.GetMakeList(YearFrom, YearTo);
            ddlMake.DataBind();
        }
        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Redirect to same screen for Reset all fields
            Response.Redirect("AddCustomer.aspx");
        }
    }
}
