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
using Microsoft.Reporting;
using Microsoft.Reporting.WebForms;
using System.Xml.Linq;
using METAOPTION;
using METAOPTION.BAL;
using METAOPTION.Reports;
using System.Web.Services;
using Telerik.Web.UI;

namespace METAOPTION.UI
{
    public partial class ViewDealer : System.Web.UI.Page
    {
        public string EntityId = string.Empty;
        public string type = string.Empty;
        public const string PAGERIGHT_DELETE = "CUSTOMERDEALER.DELETE";
        public const string PAGE = "CUSTOMERDEALER";

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
            if (Convert.ToString(Session["LoginEntityTypeID"]) == "1" && (Convert.ToString(Session["UserEntityID"]) != Convert.ToString(Request["EntityId"])))
                Response.Redirect("~/UI/ViewDealer.aspx?Mode=View&EntityId=" + Convert.ToString(Session["UserEntityID"]) + "&type=1");

            #region[Check Permission]
            if (Convert.ToString(Session["LoginEntityTypeID"]) != "1")
                CheckPermission();
            #endregion

            if (Request["EntityId"] != null && Request["type"] != null)
            {
                EntityId = Request["EntityId"];
                hfDealerid.Value = EntityId;
                type = Request["type"];

                //1 is Entity Type is For Dealer
                Util.Validate_QueryString_Value(1, EntityId, Constant.OrgID);
                BindContactDetails();
            }

            #region[Page Load Event]
            // Events that fire when page pade first time
            if (!IsPostBack)
            {
                String ReferrerURL = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : "/Default.aspx";
                hfReferrerURL.Value = ReferrerURL;

                BindDealerDetails();
                string temp = Request["EntityId"].ToString();

                if (Request["EntityId"] != null && Request["EntityId"] != "")
                {
                    //lblCustomerName.Text = BAL.DealerCustomerBAL.GetDealerName(Convert.ToInt64(Request["EntityId"])).ToString();
                    lblCustomerName.Text = BAL.DealerCustomerBAL.GetDealerName(Convert.ToInt64(Request["EntityId"]));
                    //lblDealerName.Text = BAL.DealerCustomerBAL.GetDealerName(Convert.ToInt64(Request["EntityId"])).ToString();
                    lblDealerName.Text = BAL.DealerCustomerBAL.GetDealerName(Convert.ToInt64(Request["EntityId"]));

                    EntityId = Request["EntityId"];
                    type = Request["type"];
                }
                if (Convert.ToString(Session["LoginEntityTypeID"]) == "1")
                {
                    imgbtnEdit.Visible = false;
                    imgArchive.Visible = false;
                    AddNContct.Visible = false;
                    Newparmnt.Visible = false;
                }
                //BindWelcomeTasks();
            }
            // Find Grid of Contact Section
            //grdContactDetails = (GridView)Contact1.FindControl("grdContactDetails");

            ////ImageButton imgbtnDelete = (ImageButton)grdContactDetails.FindControl("imgbtnDelete");
            //// Find Data Source of Contact Details
            //ObjectDataSource objContactDetails = (ObjectDataSource)Contact1.FindControl("objContactDetails");

            //// Assign Data source & Rebind the Grid
            //grdContactDetails.DataSource = objContactDetails;
            //grdContactDetails.DataBind();

            #endregion
        }

        public void BindContactDetails()
        {
            grdContactDetails.DataSource = objContactDetails;
            grdContactDetails.DataBind();
            if (grdContactDetails.Rows.Count > 0)
                grdContactDetails.Columns[8].Visible = false;
        }

        private void BindWelcomeTasks()
        {
            ContentPlaceHolder cph = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
            Panel pnlEditDealerDetails = (Panel)cph.FindControl("pnlEditDealerDetails");// btnUpdate.Parent;
            FormView frmviewEditDealer = (FormView)pnlEditDealerDetails.FindControl("frmviewEditDealer");
            FormViewRow frmrow = (FormViewRow)frmviewEditDealer.Row;
            RadComboBox ddlWelcomeTasks = (RadComboBox)frmviewEditDealer.FindControl("ddlWelcomeTasks");

            DealerCustomerBAL bal = new DealerCustomerBAL();
            ddlWelcomeTasks.DataSource = bal.GetWelcomeTasks();
            ddlWelcomeTasks.DataTextField = "Task";
            ddlWelcomeTasks.DataValueField = "WelcomeTaskID";
            ddlWelcomeTasks.DataBind();
        }

        protected void ibtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            CommonBAL objCommonBAL = new CommonBAL();
            long ContactId = Convert.ToInt64(grdContactDetails.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            objCommonBAL.DeleteDealerContact(ContactId);
            BindContactDetails();
        }

        protected void grdContactDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkPwd = (HyperLink)e.Row.FindControl("hlkcontact");
                HiddenField hfSecurityUserID = (HiddenField)e.Row.FindControl("hfSecurityUserID");
                HiddenField hfUserName = (HiddenField)e.Row.FindControl("hfUserName");
                HiddenField hfContactID = (HiddenField)e.Row.FindControl("hfContactID");
                if (!String.IsNullOrEmpty(hfSecurityUserID.Value))  //Change Pwd
                {
                    hlnkPwd.NavigateUrl = String.Format("AdminChangePassword.aspx?Code={0}+&UserName={1}&ReturnUrl={2}", hfSecurityUserID.Value, hfUserName.Value, HttpUtility.UrlEncode(Request.Url.AbsoluteUri).ToString());
                }
                else  //Add Pwd
                {
                    hlnkPwd.NavigateUrl = String.Format("ContactLogin.aspx?ID={0}&Mode=Ins&ReturnUrl={1}", hfContactID.Value, HttpUtility.UrlEncode(Request.Url.AbsoluteUri).ToString());
                }

            }
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

            if (!dict.Contains("DETAILS.VIEW"))
            {
                // this.upDealerDetails.Visible = false;
                bTrue = false;
            }
            if (!dict.Contains("DETAILS.EDIT"))
            {
                this.imgbtnEdit.Visible = false;
                bTrue = false;
            }
            if (!dict.Contains("CONTACT.VIEW"))
            {
                this.grdContactDetails.Visible = false;
                bTrue = false;
            }
            if (!dict.Contains("CONTACT.EDIT"))
            {
                grdContactDetails.Columns[7].Visible = false;
                bTrue = false;
            }
            if (!dict.Contains("PREFERENCE.VIEW"))
            {
                this.EditPreference1.Visible = false;
                bTrue = false;
            }
            if (!dict.Contains("PREFERENCE.EDIT"))
            {
                HtmlImage ImgEditPref = (HtmlImage)EditPreference1.FindControl("ImgEditPref");
                ImgEditPref.Visible = false;
                bTrue = false;
            }

            //Check Delete Right
            if (dict.Contains(PAGERIGHT_DELETE))
            {
                if (Request["EntityId"] == null || Request["EntityId"] == "")
                    return;
                int entityCount = BAL.Common.CanEntityBeDeleted(Convert.ToInt64(Request["EntityId"]), 1);
                if (entityCount == 0)
                    btnDelete.Visible = true;
                else
                    btnDelete.Visible = false;
            }


            if (!(dict.Contains("VIEWDEALER.VIEW") || bTrue))
                Response.Redirect("Permission.aspx?MSG=CUSTOMERDEALER.EDIT OR CUSTOMERDEALER.VIEW");


        }
        #endregion

        /// <summary>
        /// Bind Purchsad cars
        /// </summary>
        /// 
        protected void BindDealerDetails()
        {
            frmviewDealer.DataSource = objDealerDetails;
            frmviewDealer.DataBind();

            frmviewEditDealer.DataSource = objDealerDetails;
            frmviewEditDealer.DataBind();
        }
        protected void frmviewEditDealer_DataBound(object sender, EventArgs e)
        {
            if (frmviewEditDealer.DataItemCount > 0)
            {
                ListBox lstMake = (ListBox)frmviewEditDealer.FindControl("lstMake");
                lstMake.DataSource = objMake;
                lstMake.DataBind();
                //list lstMake = objDealerCustomerBAL.
                List<long> lstFranchiseList = BAL.DealerCustomerBAL.GetDealerFranchise(Convert.ToInt64(Request["EntityId"]));
                for (int index = 0; index < lstFranchiseList.Count; index++)
                {
                    for (int j = 0; j < lstMake.Items.Count; j++)
                    {
                        if (lstFranchiseList[index] == Convert.ToInt64(lstMake.Items[j].Value))
                            lstMake.Items[j].Selected = true;
                    }
                }
                // Find HiddenField for Country & State Value

                HiddenField hfieldCountry = frmviewEditDealer.Row.FindControl("hfieldCountry") as HiddenField;
                HiddenField hfieldState = frmviewEditDealer.Row.FindControl("hfieldState") as HiddenField;

                DropDownList ddlCountry = frmviewEditDealer.Row.FindControl("ddlCountry") as DropDownList;
                DropDownList ddlState = frmviewEditDealer.Row.FindControl("ddlState") as DropDownList;

                BAL.Common bal = new BAL.Common();
                ddlCountry.DataSource = bal.GetCountryList();
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("", "0"));

                //Make U.S Country Selection
                if (!string.IsNullOrEmpty(hfieldCountry.Value))
                    if (ddlCountry.Items.FindByValue(hfieldCountry.Value) != null && hfieldCountry.Value != "0")
                        ddlCountry.SelectedValue = hfieldCountry.Value;
                    else
                        ddlCountry.SelectedValue = Constant.US_COUNTRYID;
                else
                    ddlCountry.SelectedValue = Constant.US_COUNTRYID;

                // IF CountryId is not null
                if (!string.IsNullOrEmpty(hfieldCountry.Value) && hfieldCountry.Value != "0")
                    //Fill State DropDown According that countryID
                    ddlState.DataSource = BAL.Common.GetStateList(Convert.ToInt32(hfieldCountry.Value));
                else
                    //Fill State DropDown for U.S.A Default
                    ddlState.DataSource = BAL.Common.GetStateList(233);

                ddlState.DataBind();

                //Insert blank item
                ddlState.Items.Insert(0, new ListItem("", "0"));

                // IF stateId is not null
                if (hfieldState.Value != null && ddlState.Items.FindByValue(hfieldState.Value) != null)
                    ddlState.SelectedValue = hfieldState.Value;

                BindWelcomeTasks();

                HiddenField HFWelcomeTaskIDs = frmviewEditDealer.Row.FindControl("HFWelcomeTaskIDs") as HiddenField;
                HiddenField HFWelcomeTaskTexts = frmviewEditDealer.Row.FindControl("HFWelcomeTaskTexts") as HiddenField;
                RadComboBox ddlWelcomeTasks = (RadComboBox)frmviewEditDealer.Row.FindControl("ddlWelcomeTasks");
                
                foreach (RadComboBoxItem item in ddlWelcomeTasks.Items)
                {
                    CheckBox chk1 = (CheckBox)item.FindControl("chk1");
                    chk1.Checked = false;
                }

                if (!String.IsNullOrEmpty(HFWelcomeTaskIDs.Value))
                {
                    String[] arrIDs = HFWelcomeTaskIDs.Value.Split(',');
                    foreach (String str in arrIDs)
                    {
                        foreach (RadComboBoxItem item in ddlWelcomeTasks.Items)
                        {
                            CheckBox chk1 = (CheckBox)item.FindControl("chk1");
                            HiddenField hdnID = (HiddenField)item.FindControl("HFWelcomeTaskID");
                            if (str == hdnID.Value)
                            {
                                chk1.Checked = true;
                            }
                        }
                    }
                }
                ddlWelcomeTasks.Text = HFWelcomeTaskTexts.Value;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // findout button which was clicked
            Button btnUpdate = (Button)sender;
            //find out all controls
            Panel pnlEditDealerDetails = (Panel)btnUpdate.Parent;
            FormView frmviewEditDealer = (FormView)pnlEditDealerDetails.FindControl("frmviewEditDealer");
            FormViewRow frmrow = (FormViewRow)frmviewEditDealer.Row;

            TextBox txtDealerName = (TextBox)frmrow.FindControl("txtDealerName");
            TextBox txtDealerDIN = (TextBox)frmrow.FindControl("txtDealerDIN");
            DropDownList ddlType = (DropDownList)frmrow.FindControl("ddlType");
            DropDownList ddlCategory = (DropDownList)frmrow.FindControl("ddlCategory");
            // TextBox txtAccountingCode = (TextBox)frmrow.FindControl("txtAccountingCode");
            DropDownList ddlSource = (DropDownList)frmrow.FindControl("ddlSource");
            TextBox txtComment = (TextBox)frmrow.FindControl("txtComment");
            ListBox lstMake = (ListBox)frmrow.FindControl("lstMake");
            TextBox txtStreet = (TextBox)frmrow.FindControl("txtStreet");
            TextBox txtSuite = (TextBox)frmrow.FindControl("txtSuite");
            TextBox txtCity = (TextBox)frmrow.FindControl("txtCity");
            DropDownList ddlCountry = (DropDownList)frmrow.FindControl("ddlCountry");
            DropDownList ddlState = (DropDownList)frmrow.FindControl("ddlState");
            TextBox txtZip = (TextBox)frmrow.FindControl("txtZip");
            TextBox txtPhone = (TextBox)frmrow.FindControl("txtPhone");
            TextBox txtFax = (TextBox)frmrow.FindControl("txtFax");
            TextBox txtOtherNumber = (TextBox)frmrow.FindControl("txtOtherNumber");
            TextBox txtEmail = (TextBox)frmrow.FindControl("txtEmail");

            //Change Request, June 16 2010(TASK:New Fields in Dealer and Inventory)
            TextBox txtAuctionAccessNo = (TextBox)frmrow.FindControl("txtAuctionAccessNo");
            HiddenField hdnAddress = (HiddenField)frmrow.FindControl("HiddenField1");

            Dealer objDealer = new Dealer();
            objDealer.DealerId = Convert.ToInt64(Request["EntityId"]);
            objDealer.DealerName = txtDealerName.Text.Trim();
            objDealer.DealerDIN = txtDealerDIN.Text.Trim();
            objDealer.DealerTypeId = Convert.ToInt32(ddlType.SelectedValue);
            objDealer.DealerCategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
            objDealer.DealerSourceId = Convert.ToInt32(ddlSource.SelectedValue);
            // objDealer.AccountingCode = txtAccountingCode.Text.Trim();
            objDealer.AuctionAccessNo = txtAuctionAccessNo.Text.Trim();
            objDealer.Comment = txtComment.Text.Trim();

            objDealer.ModifiedBy = Constant.UserId;
            objDealer.OrgID = Constant.OrgID;

            //DropDownList ddlWelcomeTasks = (DropDownList)frmrow.FindControl("ddlWelcomeTasks");
            //if (ddlWelcomeTasks.SelectedValue == "")
            //{
            //    objDealer.WelcomeTaskID = null;
            //}
            //else
            //{
            //    objDealer.WelcomeTaskID = Byte.Parse(ddlWelcomeTasks.SelectedValue);
            //}

            String strWelcomeTasks = "";
            if (!string.IsNullOrEmpty(hWelcomeTasks.Value))
                strWelcomeTasks = hWelcomeTasks.Value;
            else
                strWelcomeTasks = "";

            // Create object of Address master
            Address objAddress = new Address();
            // assign address value to address master properties
            objAddress.AddressId = Convert.ToInt32(hdnAddress.Value);
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

            BAL.DealerCustomerBAL.UpdateDealerDetails(objDealer, objAddress, strWelcomeTasks);
            BAL.DealerCustomerBAL.DealerFranchiseDelete(Convert.ToInt64(Request["EntityId"]));

            for (int index = 0; index < lstMake.Items.Count; index++)
            {
                if (lstMake.Items[index].Selected)
                    BAL.DealerCustomerBAL.AddFranchise(Convert.ToInt32(lstMake.Items[index].Value), Convert.ToInt64(Request["EntityId"]));
            }
            // DataBind();
            BindDealerDetails();
        }

        protected void frmviewDealer_DataBound(object sender, EventArgs e)
        {
            FormView fv = sender as FormView;
            #region [Added by Rupendra 22 Nov 12 for Check Buyer Exixts or Not]
            HiddenField hfIsDealerExists = (HiddenField)fv.FindControl("hfIsDealerExists");
            HiddenField hfIsActive = (HiddenField)fv.FindControl("hfIsActive");

            if (Convert.ToBoolean(hfIsDealerExists.Value) == true)
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
            if (frmviewDealer.DataItemCount > 0)
            {
                // Find out label control 
                //Label  lblFranchise = (Label)frmviewDealer.Row.FindControl("lblFranchise");
                //lblFranchise.Text = BAL.DealerCustomerBAL.GetFranchiseName(Convert.ToInt64(Request["EntityId"]));
            }
        }

        //protected void grdviewPurchsedCars_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    grdviewPurchsedCars.PageIndex = e.NewPageIndex;
        //    BindPurchasedCars();
        //}

        protected void grdViewSoldCars_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdViewSoldCars.PageIndex = e.NewPageIndex;
            //BindSoldCars();
        }

        protected void grdCommision_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCommision.PageIndex = e.NewPageIndex;
            //BindCommission();
        }

        /// <summary>
        /// Handle Selected Index changed event of Country DropDownlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlCountry = frmviewEditDealer.FindControl("ddlCountry") as DropDownList;
            DropDownList ddlState = frmviewEditDealer.FindControl("ddlState") as DropDownList;
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
                DropDownList ddlState = frmviewEditDealer.FindControl("ddlState") as DropDownList;
                ddlState.DataSource = METAOPTION.BAL.Common.GetStateList(CountryId);
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("", "0"));
            }

        }
        protected void lnkViewAllPurchasedCars_Click(object sender, EventArgs e)
        {
            // get parameter for report
            GetParameter();
            // Report Name without extension
            ReportParameters.ReportName = "/Hollenshead/PurchasedCarsByDealers";
            // change only ReturnURL to your page name so on clicking back button it will back to your page
            Response.Redirect("~/VMSReports/Reports.aspx?ReturnURL=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));

        }

        protected void lnkViewAllSoldCars_Click(object sender, EventArgs e)
        {
            // get parameter for report
            GetParameter();
            // Report Name without extension
            ReportParameters.ReportName = "/Hollenshead/SoldCarsToDealers";
            // change only ReturnURL to your page name so on clicking back button it will back to your page
            Response.Redirect("~/VMSReports/Reports.aspx?ReturnURL=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
        }

        #region[Get Parameter]
        protected void GetParameter()
        {
            ReportParameter[] parameters = new ReportParameter[7];
            parameters[0] = new ReportParameter("startdate", "1/1/1900");
            parameters[1] = new ReportParameter("enddate", DateTime.Now.ToString());
            parameters[2] = new ReportParameter("year", "-1");
            parameters[3] = new ReportParameter("make", "-1");
            parameters[4] = new ReportParameter("model", "-1");
            parameters[5] = new ReportParameter("dealer", Request["EntityId"].ToString());
            parameters[6] = new ReportParameter("userId", Session["EmpId"].ToString());


            ReportParameters.Parameters = parameters;
        }
        #endregion


        /// <summary>
        /// Soft Delete Dealer/Customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            BAL.Common.DeleteEntity(Convert.ToInt64(Request["EntityId"]), 1, Constant.UserId, DateTime.Now);
            //After Deleting,redirect user to listing page
            Response.Redirect("DealerList.aspx");
        }

        #region[Back to previous page]
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(hfReferrerURL.Value);
        }
        #endregion


        [WebMethod(true)]
        public static void DeleteArchiveDealer(int Status, Int64 DealerID, Int64 UserID)
        {
            BAL.DealerCustomerBAL.DeleteArchiveDealer((Status == -1) ? 1 : Status, DealerID, UserID);
        }

        protected void imgbtnEdit_Click(object sender, EventArgs e)
        {
            //FormView frmviewEditDealer = (FormView)pnlEditDealerDetails.FindControl("frmviewEditDealer");
            //FormViewRow frmrow = (FormViewRow)frmviewEditDealer.Row;
            //DropDownList ddlWelcomeTasks = (DropDownList)frmrow.FindControl("ddlWelcomeTasks");

            //DealerCustomerBAL bal = new DealerCustomerBAL();
            //ddlWelcomeTasks.DataSource = bal.GetWelcomeTasks();
            //ddlWelcomeTasks.DataTextField = "Task";
            //ddlWelcomeTasks.DataValueField = "WelcomeTaskID";
            //ddlWelcomeTasks.DataBind();
        }
    }
}