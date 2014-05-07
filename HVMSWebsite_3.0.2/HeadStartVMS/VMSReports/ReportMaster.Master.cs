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
using METAOPTION.BAL;
using METAOPTION;
using dotless;
using EcmaScript;
using Yahoo;
using SquishIt;
using SquishIt.Framework;
using SquishIt.Framework.Css.Compressors;
using SquishIt.Framework.JavaScript.Minifiers;
using System.Collections.Generic;

namespace HeadStartVMS.Reports
{
    public partial class ReportMaster : System.Web.UI.MasterPage
    {
        DataTable dtSystems;
        LoginBLL objLoginBAL = new LoginBLL();
        private string _Error = string.Empty;
        //Set Messages from U.I
        public string PageMessage
        {
            set
            {
                _Error = value;
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckPermission();
            CheckRequestPage();

            lblCopyright.Text = ConfigurationManager.AppSettings["Copyright"];
            if (!IsPostBack)
            {
                try
                {
                    //Get default system
                    LoginBLL objLoginBLL = new LoginBLL();
                    Int32 sysID = objLoginBLL.GetDefaultSystem(Convert.ToString(Session["OrgCode"]));
                    if (Session["dtSystems"] != null && Session["SystemID"] != null)
                    {

                        dtSystems = (DataTable)Session["dtSystems"];
                        if (Session["SystemID"].ToString() == "-1")
                            imgLogo.Src = dtSystems.Select(String.Format("SystemID={0}", sysID))[0].ItemArray[2].ToString();
                        else
                            imgLogo.Src = dtSystems.Select("SystemID=" + Session["SystemID"].ToString())[0].ItemArray[2].ToString();
                    }
                    lblOrgName.Text = Constant.OrganisationName;
                }
                catch { }
            }
            if (Session["empId"] == null)
                Response.Redirect("~/Login.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
            else
            {
                lblWelcome.Text = "Welcome " + Session["disName"].ToString();
                long empId = Convert.ToInt64(Session["empId"].ToString());
                lblLastLogin.Text = "Last Login was on: " + objLoginBAL.GetLastLogin(empId).ToString();
            }
        }

        public void CheckRequestPage()
        {
            string RequestedPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);

            if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
                Response.Redirect("../UI/BuyerDefault.aspx?Query=refresh");
            else if (Convert.ToString(Session["LoginEntityTypeID"]) == "3")
                Response.Redirect("../UI/VendorDefault.aspx?Query=refresh");
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            //Session.Abandon();
            //Response.Redirect("~/UI/Login.aspx");
        }
        protected void lnkClearCache_Click(object sender, EventArgs e)
        {
            //Centralize Cache function By Prem(8800128549) on 2-Dec-2013
            METAOPTION.BAL.Common.ClearCache();

            #region OldCode
            //Cache.Remove(CacheEnum.Announcement);
            //Cache.Remove(CacheEnum.Buyers);
            //Cache.Remove(CacheEnum.Country);
            //Cache.Remove(CacheEnum.Dealer);
            //Cache.Remove(CacheEnum.Designation);
            //Cache.Remove(CacheEnum.Engines);
            //Cache.Remove(CacheEnum.PagePermissions);
            //Cache.Remove(CacheEnum.Trans);
            //Cache.Remove(CacheEnum.Vendor);
            //Cache.Remove(CacheEnum.YMM);
            #endregion

        }


        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            // Bundle.Css()
            //     .Add("~/CSS/AjaxRelated.css")
            //     .Add("~/CSS/MainStyle.css")
            //     .Add("~/CSS/ModalPopUp.css")
            //     .Add("~/CSS/ControlStyle.css")
            //     .Add("~/CSS/thickbox.css")
            //     .Add("~/CSS/tipTip.css")
            //     .ForceRelease().WithCompressor(CssCompressors.YuiCompressor)
            //     .Render("~/CSS/min.css");

            // Bundle.JavaScript()
            //.Add("~/CSS/Menu.js").WithMinifier(JavaScriptMinifiers.NullMinifier)
            //.Add("~/CSS/jquery-1.2.6.min.js").WithMinifier(JavaScriptMinifiers.NullMinifier)
            //.Add("~/CSS/jquery-1.7.2.min.js").WithMinifier(JavaScriptMinifiers.NullMinifier)
            //.Add("~/CSS/jquery.browser.min.js").WithMinifier(JavaScriptMinifiers.NullMinifier)
            //.Add("~/CSS/PageScript.js").WithMinifier(JavaScriptMinifiers.NullMinifier)
            //.Add("~/CSS/thickbox-compressed.js").WithMinifier(JavaScriptMinifiers.NullMinifier)
            //.Add("~/CSS/jquery.tipTip.minified.js").WithMinifier(JavaScriptMinifiers.NullMinifier)
            //.Add("~/CSS/jquery.tipTip.js").WithMinifier(JavaScriptMinifiers.NullMinifier)
            //.ForceRelease().WithMinifier(JavaScriptMinifiers.Ms)
            //.Render("~/CSS/min.js");

            lblUIMessage.Text = _Error;

        }

        #region[Check Permission]
        private void CheckPermission()
        {
            // Check if the user has right to access Lane Automation Setting
            List<String> Permissions = CommonBAL.GetPagePermission(Constant.UserId, "WEBAPP.LANEAUTOMATION");
            if (!(Permissions.Contains("LANEAUTOMATION.SETTING")))
                liLaneAutomationSetting.Visible = false;

            // Check if the user has right to access Apply Lane Automation
            if (!(Permissions.Contains("LANEAUTOMATION.APPLYRULES")))
                liApplyLaneAutomation.Visible = false;
        }
        #endregion
    }
}
