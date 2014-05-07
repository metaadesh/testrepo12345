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
using METAOPTION.BAL;
namespace METAOPTION.UI
{
    public partial class AddBuyer : System.Web.UI.Page
    {
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

            //Load Countries
            if (!IsPostBack)
            {
                BindCountry();
                BindParentBuyers();
            }
            #endregion
        }
        /// <summary>
        /// Bind All States for Provided Country,bydefault it is 1 for U.S
        /// </summary>
        private void BindStates(int CountryId)
        {
            ddlState.DataSource = Common.GetStateList(CountryId);
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("", "0"));
        }


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
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("", "0"));

            //Make U.S Country Selected ByDefault
            if (ddlCountry.Items.FindByValue(Constant.US_COUNTRYID) != null)
            {
                ddlCountry.SelectedValue = Constant.US_COUNTRYID;
                //By Default bind states of U.S
                BindStates(233);
            }
        }
        #endregion

        #region[Check Permission method]
        /// <summary>
        /// Method that check permision
        /// of User.
        /// </summary>
        private void CheckPermission()
        {
            List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, "BUYER");
            bool bTrue = true;
            if (!dict.Contains("BUYER.ADD"))
            {

                bTrue = false;
            }
            if (!(dict.Contains("BUYER.VIEW") || bTrue))
                Response.Redirect("Permission.aspx?MSG=BUYER.ADD");


        }
        #endregion

        #region [Call the method to add buyer]
        /// <summary>
        /// this is the region to call method
        /// </summary>
        protected long AddBuyerDetails()
        {
            //Check if page is valid
            long BuyerId = 0;
            if (Page.IsValid)
            {
                //create object of Buyer master
                Buyer objBuyer = new Buyer();
                // assign values to Buyer properties
                //if (!string.IsNullOrEmpty(ddlTitle.SelectedValue))
                //    objBuyer.TitleId = Convert.ToInt32(ddlTitle.SelectedValue);
                objBuyer.Buyer_Code = txtBuyerCode.Text.Trim();
                objBuyer.FirstName = txtFirstName.Text.Trim();
                objBuyer.MiddleName = txtMiddleName.Text.Trim();
                objBuyer.LastName = txtLastName.Text.Trim();
                objBuyer.TaxIdNumber = txtIdNumber.Text.Trim();
                if (!string.IsNullOrEmpty(ddlPaymentTerms.SelectedValue))
                    objBuyer.PaymentTermId = Convert.ToInt32(ddlPaymentTerms.SelectedValue);
                // objBuyer.AccountingCode = txtAccountingCode.Text.Trim();
                if (!string.IsNullOrEmpty(ddlCommisionType.SelectedValue))
                    objBuyer.CommissionTypeId = Convert.ToInt32(ddlCommisionType.SelectedValue);
                if (!string.IsNullOrEmpty(txtCommisionValue.Text))
                    objBuyer.CommissionValue = Convert.ToDecimal(txtCommisionValue.Text.Trim());
                objBuyer.StateSalesmanLicenseNumber = txtSSLNumber.Text.Trim();
                objBuyer.LicensePlateNumber = txtLPnumber.Text.Trim();
                objBuyer.CellPhone = txtCellPhone.Text.Trim();
                if (!string.IsNullOrEmpty(Session["EmpId"].ToString()))
                    objBuyer.AddedBy = Constant.UserId;
                if (!string.IsNullOrEmpty(ddlCGetPaid.SelectedValue))
                    objBuyer.CommissionTermId = Convert.ToInt32(ddlCGetPaid.SelectedValue);
                objBuyer.Comments = txtComment.Text.Trim();
                objBuyer.OrgID = Constant.OrgID;

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
                objAddress.Street = txtStreet.Text.Trim();
                objAddress.Suite = txtSuite.Text.Trim();
                objAddress.City = txtCity.Text.Trim();

                if (!string.IsNullOrEmpty(ddlState.SelectedValue) && ddlState.SelectedValue != "0")
                    objAddress.StateId = Convert.ToInt32(ddlState.SelectedValue);

                if (!string.IsNullOrEmpty(ddlCountry.SelectedValue) && ddlCountry.SelectedValue != "0")
                    objAddress.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);

                objAddress.Zip = txtZip.Text.Trim();
                objAddress.Fax = txtFax.Text.Trim();
                objAddress.Email1 = txtEmail.Text.Trim();
                objAddress.Phone1 = txtPhone.Text.Trim();
                objAddress.Phone2 = txtOtherNumber.Text.Trim();
                //objBuyer.AvgCostCar = Convert.ToDecimal(txtAverageCost.Text.Trim());
                //objBuyer.AvgRecondCostCar = Convert.ToDecimal(txtReconCost.Text.Trim());
                //Call the methods to add buyer details
                BuyerId = BAL.BuyerBAL.AddBuyerDetails(objBuyer, objAddress);


            }
            return BuyerId;


        }
        #endregion

        #region [Click event to add buyer details]
        /// <summary>
        /// this is the region
        /// to add buyer details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (CheckBuyerCodeAvailability())
            {
                long BuyerId = AddBuyerDetails();
                if (BuyerId > 0)
                    Response.Redirect("ViewBuyerDetails.aspx?BuyerId=" + BuyerId);
            }
            else
                lblError.Text = "Buyer code already exist";
        }
        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Redirect to same screen for Reset Fields
            Response.Redirect("AddBuyer.aspx");
        }
        /// <summary>
        /// Handle Index Change Event of Country DropDownlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlCountry.SelectedValue) && ddlCountry.SelectedValue != "0")
                BindStates(Convert.ToInt32(ddlCountry.SelectedValue));
            else
                ddlState.Items.Clear();

        }

        private bool CheckBuyerCodeAvailability()
        {
            if (!string.IsNullOrEmpty(txtBuyerCode.Text))
            {
                if (BuyerBAL.BuyerCodeAvailability(txtBuyerCode.Text.Trim()) > 0)// Check if BuyerCode already exists
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        #region[Bind parent buyer dropdown]
        protected void BindParentBuyers()
        {
            BuyerBAL bal = new BuyerBAL();
            ddlParentBuyer.DataSource = bal.GetAllDirectBuyers(Constant.OrgID);
            ddlParentBuyer.DataTextField = "BuyerName";
            ddlParentBuyer.DataValueField = "BuyerId";
            ddlParentBuyer.DataBind();
            ddlParentBuyer.Items.Insert(0, new ListItem("Select", "-1"));
        }
        #endregion
    }
}