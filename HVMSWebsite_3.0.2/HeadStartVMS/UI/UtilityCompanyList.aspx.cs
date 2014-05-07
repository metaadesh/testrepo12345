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
    public partial class UtilityCompanyList : System.Web.UI.Page
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
            List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, "UTILITYCOMPANY");
            bool bTrue = true;
            if (!dict.Contains("UTILITYCOMPANY.VIEW"))
            {

                bTrue = false;
            }
            //Redirect User to Common Page for InSufficient Rights
            if (!(dict.Contains("UTILITYCOMPANY.VIEW") || bTrue))
                Response.Redirect("Permission.aspx?MSG=UTILITYCOMPANY.VIEW");


        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            gvViewCompany.DataSource = BAL.UtilityCompanyBAL.GetUtilityCompanyList(
               this.txtName.Text.Trim()
               , Convert.ToInt32(this.ddlCategory.SelectedValue)
               , Convert.ToInt32(ddlPaymentFreq.SelectedValue)
               , this.txtCity.Text.Trim()
               , Convert.ToInt32(this.ddlCountry.SelectedValue)
               , Convert.ToInt32(this.ddlState.SelectedValue)
               , this.txtZip.Text.Trim()
               , Convert.ToInt32(this.ddlStatus.SelectedValue)
               ,Constant.OrgID
               );
            gvViewCompany.DataBind();
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

            //Fill Company Category Drop Down
            MasterBAL objMasterBAL = new MasterBAL();
            ddlCategory.DataSource = objMasterBAL.GetCompanyCategory();
            ddlCategory.DataTextField = "CompanyCategory1";
            ddlCategory.DataValueField = "CompanyCategoryId";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("", "-1"));

            // Fill State Drop Down   
            BindState();

            // Fill Company Payment Frequency Drop Down           
            ddlPaymentFreq.DataSource = objMasterBAL.GetPaymentFrequency();
            ddlPaymentFreq.DataTextField = "PayementFrequency1";
            ddlPaymentFreq.DataValueField = "PayementFrequencyId";
            ddlPaymentFreq.DataBind();

            ddlPaymentFreq.Items.Insert(0, new ListItem("", "-1"));
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

        protected void gvViewCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvViewCompany.PageIndex = e.NewPageIndex;
            btnSearch_Click(sender, e);
        }


    }
}