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
    public partial class BuyerList : System.Web.UI.Page
    {
        
        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["MasterPage"] )))
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
            List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, "BUYER");
            bool bTrue = true;
            if (!dict.Contains("BUYER.VIEW"))
            {

                bTrue = false;
            }
            if (!(dict.Contains("BUYER.VIEW") || bTrue))
                Response.Redirect("Permission.aspx?MSG=BUYER.VIEW");


        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            gvViewBuyer.DataSource = BAL.BuyerBAL.GetBuyerList(
               this.txtName.Text.Trim()
               , Convert.ToInt32(this.ddlCommision.SelectedValue)
               , Convert.ToInt32(ddlPaymentTerm.SelectedValue)
               , this.txtCity.Text.Trim()
               , Convert.ToInt32(this.ddlCountry.SelectedValue)
               , Convert.ToInt32(this.ddlState.SelectedValue)
               , this.txtZip.Text.Trim()
               , Convert.ToInt32(this.ddlStatus.SelectedValue)
               , Constant.OrgID);
            gvViewBuyer.DataBind();
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

            //Fill Buyer Commision Drop Down
            MasterBAL objMasterBAL = new MasterBAL();
            ddlCommision.DataSource = objMasterBAL.GetCommisionType();
            ddlCommision.DataTextField = "CommissionType1";
            ddlCommision.DataValueField = "CommissionTypeId";
            ddlCommision.DataBind();
            ddlCommision.Items.Insert(0, new ListItem("", "-1"));

            // Fill State Drop Down   
            BindState();

            // Fill Vendor Category Drop Down           
            ddlPaymentTerm.DataSource = objMasterBAL.GetPaymentTerms();
            ddlPaymentTerm.DataTextField = "PaymentTerm1";
            ddlPaymentTerm.DataValueField = "PaymentTermId";
            ddlPaymentTerm.DataBind();

            ddlPaymentTerm.Items.Insert(0, new ListItem("", "-1"));
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

        protected void gvViewBuyer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvViewBuyer.PageIndex = e.NewPageIndex;
            btnSearch_Click(sender, e);
        }


    }
}