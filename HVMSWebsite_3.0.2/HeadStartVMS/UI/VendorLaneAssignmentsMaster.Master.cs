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
using dotless;
using EcmaScript;
using Yahoo;
using SquishIt;
using SquishIt.Framework;
using SquishIt.Framework.Css.Compressors;
using SquishIt.Framework.JavaScript.Minifiers;

namespace METAOPTION.UI
{
    public partial class VendorLaneAssignmentsMaster : System.Web.UI.MasterPage
    {
        DataTable dtSystems;
        LoginBLL objLoginBLL = new LoginBLL();

        private string _Error = string.Empty;
        //Set Messages from U.I
        public string PageMessage
        {
            set
            {
                _Error = value;
            }

        }

        #region [ Page Load ]
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckRequestPage();
            lblCopyright.Text = ConfigurationManager.AppSettings["Copyright"];

            if (!IsPostBack)
            {
                try
                {
                    if (Session["dtSystems"] != null && Session["SystemID"] != null)
                    {
                        dtSystems = (DataTable)Session["dtSystems"];
                        imgLogo.Src = dtSystems.Select("SystemID=" + Session["SystemID"].ToString())[0].ItemArray[2].ToString();
                    }

                    if (Session["empId"] == null)
                        Response.Redirect("~/Login.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
                    else
                    {
                        lblWelcome.Text = "Welcome " + Session["disName"].ToString();
                        long empId = Convert.ToInt64(Session["empId"].ToString());
                        lblLastLogin.Text = "Last Login was on: " + objLoginBLL.GetLastLogin(empId).ToString();
                        //CheckPermission();
                    }

                    if ((Session["LoginEntityTypeID"] != null) && (Convert.ToString(Session["LoginEntityTypeID"]) == "3"))
                    {
                        GetEntityIdbySecurityId();
                    }
                    lblOrgName.Text = Constant.OrganisationName;
                }
                catch { }
            }
        }
        #endregion
        #region [Log Out ]
        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Util.Logout_Session();
            //Session.Abandon();
            //Response.Redirect("~/Login.aspx");
        }
        #endregion
        #region [ OnPreRender ]
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            //Bundle.Css()
            //.Add("~/CSS/AjaxRelated.css")
            //.Add("~/CSS/MainStyle.css")
            //.Add("~/CSS/ModalPopUp.css")
            //.Add("~/CSS/ControlStyle.css")
            //.Add("~/CSS/thickbox.css")
            //.Add("~/CSS/tipTip.css")
            //.ForceRelease().WithCompressor(CssCompressors.YuiCompressor)
            //.Render("~/CSS/min.css");

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
        #endregion
        #region [ Old Code]
        //private void CheckPermission()
        //{

        //    List<String> Permissions = CommonBAL.GetPagePermission(Constant.UserId, "LANEASSIGNMENT");

        //    bool bLaneSecurity = false;
        //    if ((Permissions.Contains("LANEASSIGNMENT.EDIT")))
        //    {
        //        bLaneSecurity = true;
        //    }
        //    else if ((Permissions.Contains("LANEASSIGNMENT.VIEW")))
        //    {
        //        bLaneSecurity = true;
        //    }
        //    if (bLaneSecurity == false)
        //    {
        //        this.hplLaneAssignment.Attributes.Add("onclick", "return false;");
        //        this.hplLaneAssignment.Enabled = false;
        //        this.hplLaneAssignment.ToolTip = "Permission Protected";
        //    }

        //    #region [ Security for Look up tables ]
        //    Permissions = CommonBAL.GetPagePermission(Constant.UserId, "LOOKUPTABLES");
        //    bool bLookUpSecurity = false;
        //    if (Permissions.Count > 0)
        //    {
        //        if ((Permissions.Contains("LOOKUPTABLES.ADD")))
        //        {
        //            bLookUpSecurity = true;
        //        }
        //        if ((Permissions.Contains("LOOKUPTABLES.DELETE")))
        //        {
        //            bLookUpSecurity = true;
        //        }
        //        if ((Permissions.Contains("LOOKUPTABLES.VIEW")))
        //        {
        //            bLookUpSecurity = true;
        //        }
        //        if ((Permissions.Contains("LOOKUPTABLES.REACTIVATE")))
        //        {
        //            bLookUpSecurity = true;
        //        }
        //        if (bLookUpSecurity == false)
        //        {
        //            this.hplLookUpTables.Attributes.Add("onclick", "return false;");
        //            this.hplLookUpTables.Enabled = false;
        //        }
        //    }
        //    else
        //    {
        //        this.hplLookUpTables.Attributes.Add("onclick", "return false;");
        //        this.hplLookUpTables.Enabled = false;
        //    }
        //    #endregion

        //}
        #endregion


        public void CheckRequestPage()
        {

            string RequestedPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);

            List<string> ParentBuyerPages = new List<string>();
            ParentBuyerPages.Add("VendorDefault.aspx");
            ParentBuyerPages.Add("ViewVendor.aspx");
            ParentBuyerPages.Add("CompanyContacts.aspx");
            ParentBuyerPages.Add("EntityUserList.aspx");
            ParentBuyerPages.Add("CompanyExpenseType.aspx");
            ParentBuyerPages.Add("TransportationPriceLookUp.aspx");
            //ParentBuyerPages.Add("ViewAllTitleStatus.aspx");
            //ParentBuyerPages.Add("PreInventory.aspx");
            ParentBuyerPages.Add("PreExpense.aspx");
            ParentBuyerPages.Add("ViewLocation.aspx");
            ParentBuyerPages.Add("VinScanLog.aspx");
            ParentBuyerPages.Add("GenericImages.aspx");
            ParentBuyerPages.Add("Payments.aspx");
            ParentBuyerPages.Add("ExpenseAgainstPayment.aspx");
            ParentBuyerPages.Add("ViewAllExpenses.aspx");
            ParentBuyerPages.Add("InventoryDetail.aspx");

            ParentBuyerPages.Add("PrintPreview_ExpenseAgainstPayment.aspx");
            ParentBuyerPages.Add("VendorSystemStats.aspx");


            if ((Convert.ToString(Session["LoginEntityTypeID"]) == "3"))
            {
                if (!ParentBuyerPages.Contains(RequestedPage))
                    Response.Redirect("~/UI/VendorDefault.aspx?Query=refresh");
            }
        }
        protected void lnkClearCache_Click(object sender, EventArgs e)
        {
            //Centralize Cache function By Prem(8800128549) on 2-Dec-2013
            BAL.Common.ClearCache();
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


        #region [Added by Rupendra 28 Dec 12 for get EntityId using User Id]
        protected void GetEntityIdbySecurityId()
        {
            DataTable dtEntityId = new DataTable();
            dtEntityId = objLoginBLL.EntityIdByUserId(Constant.UserId);
            if (dtEntityId.Rows.Count > 0)
            {
                hdnURL.Value = "../UI/ViewVendor.aspx?Mode=View&EntityId=" + Convert.ToString(dtEntityId.Rows[0]["EntityID"]) + "&type=" + Convert.ToString(Session["LoginEntityTypeID"]);
            }
        }
        #endregion
    }
}