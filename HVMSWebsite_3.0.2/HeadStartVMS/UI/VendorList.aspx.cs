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
    public partial class VendorList : System.Web.UI.Page
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
            #endregion

            if (!(Page.IsPostBack || IsCallback))
            {
                FillControls();
                btnSearch_Click(sender, e);
            }
        }

        #region[Check Permission method]
        /// <summary>
        /// Method that check permision
        /// of User.
        /// </summary>
        private void CheckPermission()
        {
            List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, "VENDOR");
            bool bTrue = true;
            if (!dict.Contains("VENDOR.VIEW"))
            {

                bTrue = false;
            }
            //Redirect User to Common Page for InSufficient Rights
            if (!(dict.Contains("VENDOR.VIEW") || bTrue))
                Response.Redirect("Permission.aspx?MSG=VENDOR.VIEW");


        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //BAL.ViewCustomerBAL dealer = new METAOPTION.BAL.ViewCustomerBAL();
            gvViewVendor.DataSource = BAL.VendorBAL.GetVendorList(
               this.txtName.Text.Trim()
               , Convert.ToInt32(this.ddlCategory.SelectedValue)
               , Convert.ToInt32(ddlType.SelectedValue)
               , this.txtCity.Text.Trim()
               , Convert.ToInt32(this.ddlCountry.SelectedValue)
               , Convert.ToInt32(this.ddlState.SelectedValue)
               , this.txtZip.Text.Trim()
               , Convert.ToInt32(this.ddlStatus.SelectedValue)
               , Constant.OrgID);
            gvViewVendor.DataBind();
        }

        protected void FillControls()
        {
            //Fill Country Drop Down
            BAL.Common bal = new BAL.Common();
            ddlCountry.DataSource = bal.GetCountryList();
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryId";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("", "-1"));

            //Fill Vendor Type Drop Down
            MasterBAL objMasterBAL = new MasterBAL();
            ddlType.DataSource = objMasterBAL.GetVendorType();
            ddlType.DataTextField = "VendorType1";
            ddlType.DataValueField = "VendorTypeId";
            ddlType.DataBind();
            ddlType.Items.Insert(0, new ListItem("", "-1"));

            // Fill State Drop Down   
            BindState();

            // Fill Vendor Category Drop Down           
            ddlCategory.DataSource = objMasterBAL.GetVendorCategory();
            ddlCategory.DataTextField = "VendorCategory1";
            ddlCategory.DataValueField = "VendorCategoryId";
            ddlCategory.DataBind();

            ddlCategory.Items.Insert(0, new ListItem("", "-1"));
        }

        protected void BindState()
        {
            // Fill State Drop Down           
            ddlState.DataSource = BAL.Common.GetStateList(Convert.ToInt32(ddlCountry.SelectedValue));
            ddlState.DataTextField = "State";
            ddlState.DataValueField = "StateId";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("", "-1"));
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindState();
        }

        protected void gvViewVendor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvViewVendor.PageIndex = e.NewPageIndex;
            btnSearch_Click(sender, new EventArgs());
        }
    }
}