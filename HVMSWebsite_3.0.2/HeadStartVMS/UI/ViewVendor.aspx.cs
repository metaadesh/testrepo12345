using System;
using System.Collections;
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
using System.Web.Services;

namespace METAOPTION.UI
{
    public partial class ViewVendor : System.Web.UI.Page
    {
        public string EntityId = string.Empty;
        public string type = string.Empty;
        public const string PAGERIGHT_DELETE = "VENDOR.DELETE";
        public const string PAGE = "VENDOR";

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
            #region[Page Load Events]
            if (Request["EntityId"] != null && Request["type"] != null)
            {
                if (Convert.ToString(Session["LoginEntityTypeID"]) == "3" && (Convert.ToString(Session["UserEntityID"]) != Convert.ToString(Request["EntityId"])))
                    Response.Redirect("~/UI/ViewVendor.aspx?Mode=View&EntityId=" + Convert.ToString(Session["UserEntityID"]) + "&type=3");
              
                EntityId = Request["EntityId"];
                type = Request["type"];
                ucexpensedetails.EntityID = Convert.ToInt64(EntityId);
                ucexpensedetails.EntityTypeID = Convert.ToInt32(type);
                ancAddExpense.HRef = String.Format("ManageEntityExpenses.aspx?EntityId={0}&type=3&ReturnUrl={1}", EntityId, HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
                CheckUserPagePermissions();
                hfvendorid.Value = EntityId;

                //3 is Entity Type For Vendor
                Util.Validate_QueryString_Value(3, EntityId, Constant.OrgID);
                BindContactDetails();
            }
            if (!IsPostBack)
            {
                String ReferrerURL = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : "/Default.aspx";
                hfReferrerURL.Value = ReferrerURL;
                BindContactDetails();
                EntityLoginDetails();
                if ((Session["LoginEntityTypeID"] != null) && (Convert.ToString(Session["LoginEntityTypeID"]) == "3"))
                {
                    btnDelete.Visible = false;
                    imgbtnEdit.Visible = false;
                    imgDelete.Visible = false;
                    imgUnDelete.Visible = false;
                    imgArchive.Visible = false;
                    imgUnArchive.Visible = false;
                    ancAddExpense.Style.Add("display", "none");
                    grdContactDetails.Columns[8].Visible = false;
                }
            }

            #endregion

        }

        #region[Bind Payment Grid]
        /// <summary>
        /// Bind Payment Grid of Vendor
        /// </summary>
        //protected void BindCommisionPaid()
        //{
        //    CommonBAL objCommonBAL = new CommonBAL();
        //    grdCommision.DataSource = objCommonBAL.GetPayments(Convert.ToInt64(Request["EntityId"]), Convert.ToInt32(Constant.EntityType.Vendor));
        //    grdCommision.DataBind();
        //}
        /// <summary>
        /// Handle page index change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdCommision_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCommision.PageIndex = e.NewPageIndex;
            //BindCommisionPaid();
            EntityLoginDetails();
        }
        #endregion

        #region[Update Vendor Details]
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // findout button which was clicked
            Button btnUpdate = (Button)sender;
            //find out all controls
            Panel pnlEditVendor = (Panel)btnUpdate.Parent;
            FormView frmviewEditVendor = (FormView)pnlEditVendor.FindControl("frmviewEditVendor");
            FormViewRow frmrow = (FormViewRow)frmviewEditVendor.Row;

            TextBox txtName = frmrow.FindControl("txtName") as TextBox;
            TextBox txtDIN = frmrow.FindControl("txtDIN") as TextBox;
            DropDownList ddlCategory = frmrow.FindControl("ddlCategory") as DropDownList;
            TextBox txtIdNumber = frmrow.FindControl("txtIdNumber") as TextBox;
            DropDownList ddlVendorType = frmrow.FindControl("ddlVenderType") as DropDownList;
            DropDownList ddlPaymentTerms = frmrow.FindControl("ddlPaymentTerms") as DropDownList;
            DropDownList ddlState = frmrow.FindControl("ddlState") as DropDownList;
            DropDownList ddlCountry = frmrow.FindControl("ddlCountry") as DropDownList;
            //TextBox txtAccountingCode = frmrow.FindControl("txtAccountingCode") as TextBox;
            TextBox txtStreet = frmrow.FindControl("txtStreet") as TextBox;
            TextBox txtSuite = frmrow.FindControl("txtSuite") as TextBox;

            TextBox txtCity = frmrow.FindControl("txtCity") as TextBox;


            TextBox txtZip = frmrow.FindControl("txtZip") as TextBox;
            TextBox txtFax = frmrow.FindControl("txtFax") as TextBox;
            TextBox txtPhone = frmrow.FindControl("txtPhone") as TextBox;
            TextBox txtOtherNumber = frmrow.FindControl("txtOtherNumber") as TextBox;
            TextBox txtEmail = frmrow.FindControl("txtEmail") as TextBox;
            TextBox txtComment = frmrow.FindControl("txtComment") as TextBox;
            DropDownList ddlExpenseCalcMethod = frmrow.FindControl("ddlExpenseCalcMethod") as DropDownList;

            HiddenField hdnAddressId = frmrow.FindControl("HiddenField1") as HiddenField;

            Vendor objVendor = new Vendor();
            objVendor.VendorId = Convert.ToInt64(Request["EntityId"]);
            objVendor.VendorName = txtName.Text.Trim();
            objVendor.VendorDIN = txtDIN.Text.Trim();
            objVendor.VendorCategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
            objVendor.VendorTypeId = Convert.ToInt32(ddlVendorType.SelectedValue);
            objVendor.TaxIdNumber = txtIdNumber.Text.Trim();
            objVendor.PaymentTermId = Convert.ToInt32(ddlPaymentTerms.SelectedValue);
            // objVendor.AccountingCode = txtAccountingCode.Text.Trim();
            objVendor.Comments = txtComment.Text.Trim();
            objVendor.ModifiedBy = Constant.UserId;
            objVendor.TransExpenseCalculationMethod = Convert.ToInt16(ddlExpenseCalcMethod.SelectedValue);

            Address objAddress = new Address();
            objAddress.AddressId = Convert.ToInt64(hdnAddressId.Value);
            objAddress.Street = txtStreet.Text.Trim();
            objAddress.Suite = txtSuite.Text.Trim();
            objAddress.City = txtCity.Text.Trim();
            objAddress.Zip = txtZip.Text.Trim();
            objAddress.Fax = txtFax.Text.Trim();
            objAddress.Email1 = txtEmail.Text.Trim();
            objAddress.Phone1 = txtPhone.Text.Trim();
            objAddress.Phone2 = txtOtherNumber.Text.Trim();

            //Save selected country & address details
            if (!string.IsNullOrEmpty(ddlState.SelectedValue) && ddlState.SelectedValue != "0")
                objAddress.StateId = Convert.ToInt32(ddlState.SelectedValue);

            if (!string.IsNullOrEmpty(ddlCountry.SelectedValue) && ddlCountry.SelectedValue != "0")
                objAddress.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);

            BAL.VendorBAL.UpdateVendorDetails(objVendor, objAddress);
            DataBind(); BindContactDetails();
        }
        #endregion

        #region[Edit vendor - RowDataBound event]
        protected void frmviewEditVendor_DataBound(object sender, EventArgs e)
        {
            if (frmviewEditVendor.DataItemCount > 0)
            {
                // Find HiddenField for Country & State Value

                HiddenField hfieldCountry = frmviewEditVendor.Row.FindControl("hfieldCountry") as HiddenField;
                HiddenField hfieldState = frmviewEditVendor.Row.FindControl("hfieldState") as HiddenField;
                HiddenField hfExpenseCalcMethod = (HiddenField)frmviewEditVendor.Row.FindControl("hfExpenseCalcMethod");

                DropDownList ddlCountry = frmviewEditVendor.Row.FindControl("ddlCountry") as DropDownList;
                DropDownList ddlState = frmviewEditVendor.Row.FindControl("ddlState") as DropDownList;
                DropDownList ddlExpenseCalcMethod = frmviewEditVendor.Row.FindControl("ddlExpenseCalcMethod") as DropDownList;

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

                ddlExpenseCalcMethod.SelectedValue = hfExpenseCalcMethod.Value;
            }
        }
        #endregion


        protected void BindContactDetails()
        {
            grdContactDetails.DataSource = objContactDetails;
            grdContactDetails.DataBind();
        }
        protected void grdContactDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            CommonBAL objCommonBAL = new CommonBAL();
            long ContactId = Convert.ToInt64(grdContactDetails.DataKeys[e.RowIndex].Value);
            objCommonBAL.DeleteDealerContact(ContactId);
            BindContactDetails();
        }

        /// <summary>
        /// Bind All States for Provided Country,bydefault it is 1 for U.S
        /// </summary>
        private void BindStates(int CountryId, bool isFromDb)
        {
            if (!isFromDb)
            {
                //Find dropdownlist
                DropDownList ddlState = frmviewEditVendor.FindControl("ddlState") as DropDownList;
                ddlState.DataSource = METAOPTION.BAL.Common.GetStateList(CountryId);
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("", "0"));
            }

        }

        /// <summary>
        /// Handled Selected Index changed event of Country DropDownlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlCountry = frmviewEditVendor.FindControl("ddlCountry") as DropDownList;
            DropDownList ddlState = frmviewEditVendor.FindControl("ddlState") as DropDownList;
            if (!string.IsNullOrEmpty(ddlCountry.SelectedValue) && ddlCountry.SelectedValue != "0")
                BindStates(Convert.ToInt32(ddlCountry.SelectedValue), false);
            else
                ddlState.Items.Clear();

            EntityLoginDetails();

        }

        /// <summary>
        /// Soft Delete Vendor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            BAL.Common.DeleteEntity(Convert.ToInt64(Request["EntityId"]), 3, Constant.UserId, DateTime.Now);

            //After Deleting,redirect user to listing page
            Response.Redirect("VendorList.aspx");

        }
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
                int entityCount = BAL.Common.CanEntityBeDeleted(Convert.ToInt64(Request["EntityId"]), 3);
                if (entityCount == 0)
                    btnDelete.Visible = true;
                else
                    btnDelete.Visible = false;
            }
        }
        #endregion

        protected void grdContactDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkPwd = (HyperLink)e.Row.FindControl("hlkcontact");
                HiddenField hfSecurityUserID = (HiddenField)e.Row.FindControl("hfSecurityUserID");
                HiddenField hfUserName = (HiddenField)e.Row.FindControl("hfUserName");
                HiddenField hfContactID = (HiddenField)e.Row.FindControl("hfContactID");
                ImageButton imgbtnDelete = (ImageButton)e.Row.FindControl("imgbtnDelete");

                if (!String.IsNullOrEmpty(hfSecurityUserID.Value))  //Change Pwd
                {
                    hlnkPwd.NavigateUrl = String.Format("AdminChangePassword.aspx?Code={0}+&UserName={1}&ReturnUrl={2}", hfSecurityUserID.Value, hfUserName.Value, HttpUtility.UrlEncode(Request.Url.AbsoluteUri).ToString());
                }
                else  //Add Pwd
                {
                    hlnkPwd.NavigateUrl = String.Format("ContactLogin.aspx?ID={0}&Mode=Ins&ReturnUrl={1}", hfContactID.Value, HttpUtility.UrlEncode(Request.Url.AbsoluteUri).ToString());
                }

                if ((Session["LoginEntityTypeID"] != null) && (Convert.ToString(Session["LoginEntityTypeID"]) == "3"))
                {
                    hlnkPwd.Visible = false;
                    imgbtnDelete.Visible = false;
                }

            }
        }

        #region[Back to previous page]
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(hfReferrerURL.Value);
        }
        #endregion

        protected void frmviewVendorDetails_DataBound(object sender, EventArgs e)
        {
            FormView fv = sender as FormView;


            HtmlImage imgOn = (HtmlImage)fv.FindControl("imgOn");
            HtmlImage imgOff = (HtmlImage)fv.FindControl("imgOff");
            HiddenField hfExpenseAutoApproval = (HiddenField)fv.FindControl("hfExpenseAutoApproval");
            HiddenField hfIsVendorExists = (HiddenField)fv.FindControl("hfIsVendorExists");
            HiddenField hfIsActive = (HiddenField)fv.FindControl("hfIsActive");

            if (Convert.ToBoolean(hfExpenseAutoApproval.Value) == false)
            {
                imgOff.Attributes.Add("style", "display:block");
                imgOn.Attributes.Add("style", "display:none");
            }
            else
            {
                imgOff.Attributes.Add("style", "display:none");
                imgOn.Attributes.Add("style", "display:block");
            }

            if (Convert.ToBoolean(hfIsVendorExists.Value) == true)
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

            HtmlImage imgOffDummy = (HtmlImage)fv.FindControl("imgOffDummy");
            HtmlImage imgonDummy = (HtmlImage)fv.FindControl("imgonDummy");
            if ((Session["LoginEntityTypeID"] != null) && (Convert.ToString(Session["LoginEntityTypeID"]) == "3"))
            {
                imgOn.Visible = false;
                imgOff.Visible = false;
                if (Convert.ToBoolean(hfExpenseAutoApproval.Value) == false)
                {
                    imgonDummy.Style.Add("display", "none");
                    imgOffDummy.Style.Add("display", "");
                }
                else
                {
                    imgonDummy.Style.Add("display", "");
                    imgOffDummy.Style.Add("display", "none");
                }
            }


        }

        #region[Added By Rupendra 13 Sep 12 for show Comment with Invoice number]
        protected void gvExpense_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string EntityTypeID = string.Empty;
                    HyperLink lnkVIN = (HyperLink)e.Row.FindControl("lnkVIN");
                    Label lblInvoiceNumber = (Label)e.Row.FindControl("lblInvoiceNumber");
                    HiddenField hdnComments = (HiddenField)e.Row.FindControl("hdnComment");
                    if (!string.IsNullOrEmpty(hdnComments.Value))
                        lblInvoiceNumber.Text = hdnComments.Value + "</br><i>" + lblInvoiceNumber.Text + "</i>";
                    else
                        lblInvoiceNumber.Text = "<i>" + lblInvoiceNumber.Text + "</i>";
                    if ((Session["LoginEntityTypeID"] != null) && (Convert.ToString(Session["LoginEntityTypeID"]) == "3"))
                        lnkVIN.Enabled = false;
                }

            }
            catch (Exception ex) { }
        }
        #endregion

        [WebMethod]
        public static void UpdateAutoApproval(int Flag, Int64 VendorID)
        {
            BAL.VendorBAL.UpdateExpenseAutoApprovalFlag(VendorID, (Flag == 1) ? true : false);
        }

        [WebMethod(true)]
        public static void DeleteArchiveVendor(int Status, Int64 VendorID, Int64 UserID)
        {
            BAL.VendorBAL.DeleteArchiveVendor((Status == -1) ? 1 : Status, VendorID, UserID);
        }


        #region [Added by Rupendra 21 Dec 12 Vendor, Dealer and Buyer login details]
        public void EntityLoginDetails()
        {
            if ((Session["LoginEntityTypeID"] != null) && (Convert.ToString(Session["LoginEntityTypeID"]) == "3"))
            {
                foreach (GridViewRow gvr in grdCommision.Rows)
                {
                    if (gvr.RowType == DataControlRowType.DataRow)
                    {
                        HyperLink hylnkCheckNo = (HyperLink)gvr.FindControl("hylnkCheckNo");
                        hylnkCheckNo.Enabled = false;
                    }
                }
            }
        }
        #endregion
    }
}
