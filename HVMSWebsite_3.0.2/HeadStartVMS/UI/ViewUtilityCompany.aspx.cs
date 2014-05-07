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
using System.Web.Services;

namespace METAOPTION.UI
{
    public partial class ViewUtilityCompany : System.Web.UI.Page, IPagePermission
    {

        #region[Constaints & Public Variables]
        public string EntityId = string.Empty;
        public string type = string.Empty;
        //Page Level Rights
        public const string PAGE = "UTILITYCOMPANY";
        public const string PAGERIGHT_DELETE = "UTILITYCOMPANY.DELETE";
        public const string PAGERIGHT_EDIT = "UTILITYCOMPANY.EDIT";
        public const string PAGERIGHT_VIEW = "UTILITYCOMPANY.VIEW";
        //Page Section Level Rights
        public const string UTILITYCOMPANY_COMPANYDETAILS_EDIT = "COMPANYDETAILS.EDIT";
        public const string UTILITYCOMPANY_COMPANYDETAILS_VIEW = "COMPANYDETAILS.VIEW";
        public const string UTILITYCOMPANY_COMPANYCONTACT_EDIT = "COMPANYCONTACT.EDIT";
        public const string UTILITYCOMPANY_COMPANYCONTACT_VIEW = "COMPANYCONTACT.VIEW";
        public const string UTILITYCOMPANY_COMPANYPAYMENT_EDIT = "COMPANYPAYMENT.EDIT";
        public const string UTILITYCOMPANY_COMPANYPAYMENT_VIEW = "COMPANYPAYMENT.VIEW";
        #endregion

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
            if (!IsPostBack)
            {
                if (Request.QueryString["EntityId"] != null)
                {
                    //4 is Entity Type For Utility Company
                    Util.Validate_QueryString_Value(4, Request.QueryString["EntityId"], Constant.OrgID);
                }
            }

            CheckUserPagePermissions(); // Check User Page Permission
            if (Request["EntityId"] != null && Request["type"] != null)
            {
                EntityId = Request["EntityId"];
                hfUtilityCompanyId.Value = EntityId;
                type = Request["type"];
            }
            if (!IsPostBack)
                BindContactDetails();
        }

        #region[Method that Check User Page Permission]
        public void CheckUserPagePermissions()
        {
            List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, PAGE);
            bool bTrue = true;
            if (!dict.Contains(PAGERIGHT_VIEW))// Check Page Permission for Logged User
                Response.Redirect("Permission.aspx?MSG=UTILITYCOMPANY.EDIT OR UTILITYCOMPANY.VIEW");
            // Check Permission For View of Each Scetion
            if (!dict.Contains(UTILITYCOMPANY_COMPANYDETAILS_VIEW))//Check Permission For View Details Section
            {
                // this.upComDetails.Visible = false;
                bTrue = false;
            }
            if (!dict.Contains(UTILITYCOMPANY_COMPANYCONTACT_VIEW))//Check Permission For View Contact Section
            {
                // this.Contact1.Visible = false;
                bTrue = false;
            }
            if (!dict.Contains(UTILITYCOMPANY_COMPANYPAYMENT_VIEW))//Check Permission For View Payment Section
            {
                this.grdCommision.Visible = false;
                bTrue = false;
            }
            if (!dict.Contains(PAGERIGHT_EDIT))// Check User has Edit Permission
            {
                this.imgbtnEdit.Visible = false;
                //GridView grdContactDetails = (GridView)Contact1.FindControl("grdContactDetails");
                //grdContactDetails.Columns[7].Visible = false;
                return;
            }
            // Check Permission For Edit of Each Scetion
            if (!dict.Contains(UTILITYCOMPANY_COMPANYDETAILS_EDIT))//Check Permission For Edit Details Section
            {
                this.imgbtnEdit.Visible = false;
                bTrue = false;
            }
            if (!dict.Contains(UTILITYCOMPANY_COMPANYCONTACT_EDIT))//Check Permission For Edit Contact Section
            {
                //GridView grdContactDetails = (GridView)Contact1.FindControl("grdContactDetails");
                //grdContactDetails.Columns[7].Visible = false;
                bTrue = false;
            }
            if (!dict.Contains(UTILITYCOMPANY_COMPANYPAYMENT_EDIT))//Check Permission For EDIT Payment Section
            {
                this.trAddNewPayment.Visible = false;
                bTrue = false;
            }

            //Check Delete Right
            if (dict.Contains(PAGERIGHT_DELETE))
            {
                if (Request["EntityId"] == null || Request["EntityId"] == "")
                    return;
                int entityCount = BAL.Common.CanEntityBeDeleted(Convert.ToInt64(Request["EntityId"]), 4);
                if (entityCount == 0)
                    btnDelete.Visible = true;
                else
                    btnDelete.Visible = false;
            }

            if (!(dict.Contains(PAGERIGHT_VIEW) || bTrue))
                Response.Redirect("Permission.aspx?MSG=UTILITYCOMPANY.EDIT OR UTILITYCOMPANY.VIEW");

        }
        #endregion



        protected void grdCommision_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCommision.PageIndex = e.NewPageIndex;
        }

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


        protected void fvEditCompanyDetail_DataBound(object sender, EventArgs e)
        {
            if (fvEditCompanyDetail.DataItemCount > 0)
            {
                // Find HiddenField for Country & State Value
                // Find HiddenField for Country & State Value

                HiddenField hfieldCountry = fvEditCompanyDetail.Row.FindControl("hfieldCountry") as HiddenField;
                HiddenField hfieldState = fvEditCompanyDetail.Row.FindControl("hfieldState") as HiddenField;

                DropDownList ddlCountry = fvEditCompanyDetail.Row.FindControl("ddlCountry") as DropDownList;
                DropDownList ddlState = fvEditCompanyDetail.Row.FindControl("ddlState") as DropDownList;

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
            }
        }

        #region[Handle click event to Update Comp details]
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // findout button which was clicked
            Button btnUpdate = (Button)sender;
            //find out all controls
            Panel pnlEditCompanyDetail = (Panel)btnUpdate.Parent;
            FormView fvEditCompanyDetail = (FormView)pnlEditCompanyDetail.FindControl("fvEditCompanyDetail");
            FormViewRow frmrow = (FormViewRow)fvEditCompanyDetail.Row;

            TextBox txtCompanyName = frmrow.FindControl("txtCompanyName") as TextBox;
            TextBox txtTaxIdNo = frmrow.FindControl("txtTaxIdNo") as TextBox;
            DropDownList ddlCompanyCategory = frmrow.FindControl("ddlCompanyCategory") as DropDownList;

            DropDownList ddlPaymentFreq = frmrow.FindControl("ddlPaymentFreq") as DropDownList;

            TextBox txtAccountNo = frmrow.FindControl("txtAccountNo") as TextBox;
            //TextBox txtAccountingCode = frmrow.FindControl("txtAccountingCode") as TextBox;
            TextBox txtComments = frmrow.FindControl("txtComments") as TextBox;

            TextBox txtStreet = frmrow.FindControl("txtStreet") as TextBox;
            TextBox txtSuite = frmrow.FindControl("txtSuite") as TextBox;

            TextBox txtCity = frmrow.FindControl("txtCity") as TextBox;

            DropDownList ddlState = frmrow.FindControl("ddlState") as DropDownList;
            DropDownList ddlCountry = frmrow.FindControl("ddlCountry") as DropDownList;

            TextBox txtZip = frmrow.FindControl("txtZip") as TextBox;
            TextBox txtFax = frmrow.FindControl("txtFax") as TextBox;
            TextBox txtPhone = frmrow.FindControl("txtPhone") as TextBox;
            TextBox txtOther = frmrow.FindControl("txtOther") as TextBox;
            TextBox txtEmail = frmrow.FindControl("txtEmail") as TextBox;
            HiddenField hdnAddressId = frmrow.FindControl("hdnAddressId") as HiddenField;

            // Create object of UtilityCompany Class
            UtilityCompany objUtiComp = new UtilityCompany();
            objUtiComp.UtilityCompanyId = Convert.ToInt64(Request["EntityId"]);
            // Assign company details to UtilityCompany class properties
            objUtiComp.CompanyName = txtCompanyName.Text.Trim();
            objUtiComp.TaxIdNumber = txtTaxIdNo.Text.Trim();

            if (!string.IsNullOrEmpty(ddlCompanyCategory.SelectedValue))
                objUtiComp.CompanyCategoryId = Convert.ToInt32(ddlCompanyCategory.SelectedValue);

            if (!string.IsNullOrEmpty(ddlPaymentFreq.SelectedValue))
                objUtiComp.PayementFrequencyId = Convert.ToInt32(ddlPaymentFreq.SelectedValue);

            objUtiComp.AccountNumber = txtAccountNo.Text.Trim();
            objUtiComp.Comments = txtComments.Text.Trim();
            objUtiComp.ModifiedBy = Constant.UserId;
            objUtiComp.DateModified = DateTime.Now;

            // Create object of Addredd class
            Address objAddress = new Address();

            // Assign address value to address class properties
            objAddress.AddressId = Convert.ToInt32(hdnAddressId.Value);
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
            objAddress.Phone2 = txtOther.Text.Trim();
            objAddress.Email1 = txtEmail.Text.Trim();

            // Call method for update details
            BAL.UtilityCompanyBAL.UpdateUtilityCompanyDetails(objUtiComp, objAddress);
            DataBind(); BindContactDetails();

        }
        #endregion


        /// <summary>
        /// Handle Selected Index Changed Event of Country DropDownlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlCountry = fvEditCompanyDetail.FindControl("ddlCountry") as DropDownList;
            DropDownList ddlState = fvEditCompanyDetail.FindControl("ddlState") as DropDownList;
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
                DropDownList ddlState = fvEditCompanyDetail.FindControl("ddlState") as DropDownList;
                ddlState.DataSource = METAOPTION.BAL.Common.GetStateList(CountryId);
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("", "0"));
            }

        }
        /// <summary>
        /// Soft Delete Utility Company(Is Active=0)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            BAL.Common.DeleteEntity(Convert.ToInt64(Request["EntityId"]), 4, Constant.UserId, DateTime.Now);
            //After Deleting,redirect user to listing page
            Response.Redirect("UtilityCompanyList.aspx");
        }

        #region [Added by Rupendra 26 Nov 12 for Check Company Utility Exixts or Not]
        protected void frmViewCompDetails_DataBound(object sender, EventArgs e)
        {
            FormView fv = sender as FormView;

            HiddenField hfIsComUtilityExists = (HiddenField)fv.FindControl("hfIsComUtilityExists");
            HiddenField hfIsActive = (HiddenField)fv.FindControl("hfIsActive");

            if (Convert.ToBoolean(hfIsComUtilityExists.Value) == true)
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

        }


        [WebMethod(true)]
        public static void DeleteArchiveUtilityCompany(int Status, Int64 UtilityCompanyId, Int64 UserID)
        {
            BAL.UtilityCompanyBAL.DeleteArchiveUtilityCompany((Status == -1) ? 1 : Status, UtilityCompanyId, UserID);
        }
        #endregion
    }
}