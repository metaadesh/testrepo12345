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
    public partial class BuyerLaneAssignmentsMaster : System.Web.UI.MasterPage
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
                    lblOrgName.Text = Constant.OrganisationName;
                }
                catch { }
            }
            if ((Session["LoginEntityTypeID"] != null) && (Convert.ToString(Session["LoginEntityTypeID"]) == "2"))
            {
                GetEntityIdbySecurityId();
            }
        }
        #endregion

        public void CheckRequestPage()
        {
            string IsDirectBuyer = String.IsNullOrEmpty(Convert.ToString(Session["BuyerIsDirect"])) ? "-1" : Convert.ToString(Session["BuyerIsDirect"]);
            Int32 ParentBuyerID = String.IsNullOrEmpty(Session["BuyerParent"].ToString()) ? -1 : Convert.ToInt32(Session["BuyerParent"].ToString());
            Int32 BuyerAccessLevel = String.IsNullOrEmpty(Session["BuyerAccessLevel"].ToString()) ? -1 : Convert.ToInt32(Session["BuyerAccessLevel"].ToString());
            Int32 EntityId = String.IsNullOrEmpty(Session["UserEntityID"].ToString()) ? -1 : Convert.ToInt32(Session["UserEntityID"].ToString());

            string RequestedPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            List<string> ChildBuyerPages = new List<string>();
            ChildBuyerPages.Add("BuyerDefault.aspx");
            ChildBuyerPages.Add("ViewBuyerDetails.aspx");
            ChildBuyerPages.Add("InventorySearch.aspx");
            ChildBuyerPages.Add("PreInventory.aspx");

            ChildBuyerPages.Add("InventoryDetail.aspx");
            ChildBuyerPages.Add("InventoryExpense.aspx");
            ChildBuyerPages.Add("InventoryNotes.aspx");
            ChildBuyerPages.Add("InventoryDocuments.aspx");
            ChildBuyerPages.Add("InventoryDriver.aspx");
            ChildBuyerPages.Add("InventoryExpense.aspx");
            ChildBuyerPages.Add("InventoryLifeCycle.aspx");
            ChildBuyerPages.Add("ExpenseAgainstPayment.aspx");
            ChildBuyerPages.Add("BuyerSystemStats.aspx");
            ChildBuyerPages.Add("QuickSearch.aspx");
            ChildBuyerPages.Add("PreInventoryDetail.aspx");
            //ChildBuyerPages.Add("ViewAnnouncementList.aspx");
            //ChildBuyerPages.Add("AddNewAnnouncement.aspx");
            ChildBuyerPages.Add("AnnouncementDetails.aspx");

            List<string> ParentBuyerPages = new List<string>();
            ParentBuyerPages.Add("BuyerDefault.aspx");
            ParentBuyerPages.Add("ViewBuyerDetails.aspx");
            ParentBuyerPages.Add("Payments.aspx");
            ParentBuyerPages.Add("ViewAllExpenses.aspx");
            ParentBuyerPages.Add("InventorySearch.aspx");
            ParentBuyerPages.Add("ViewAllCommission.aspx");
            ParentBuyerPages.Add("ViewAllTitleStatus.aspx");
            ParentBuyerPages.Add("PreInventory.aspx");
            ParentBuyerPages.Add("PreExpense.aspx");
            ParentBuyerPages.Add("ViewLocation.aspx");
            ParentBuyerPages.Add("QuickSearch.aspx");
            ParentBuyerPages.Add("VinScanLog.aspx");
            ParentBuyerPages.Add("GenericImages.aspx");
            ParentBuyerPages.Add("InventoryDetail.aspx");
            ParentBuyerPages.Add("InventoryExpense.aspx");
            ParentBuyerPages.Add("InventoryNotes.aspx");
            ParentBuyerPages.Add("InventoryDocuments.aspx");
            ParentBuyerPages.Add("InventoryDriver.aspx");
            ParentBuyerPages.Add("InventoryExpense.aspx");
            ParentBuyerPages.Add("InventoryLifeCycle.aspx");
            ParentBuyerPages.Add("ExpenseAgainstPayment.aspx");
            ParentBuyerPages.Add("BuyerSystemStats.aspx");
            ParentBuyerPages.Add("PreInventoryDetail.aspx");
            ParentBuyerPages.Add("SearchPayment.aspx");

            //ParentBuyerPages.Add("ViewAnnouncementList.aspx");
            //ParentBuyerPages.Add("AddNewAnnouncement.aspx");
            ParentBuyerPages.Add("AnnouncementDetails.aspx");


            if ((Convert.ToString(Session["LoginEntityTypeID"]) == "2") && (IsDirectBuyer.ToLower() == "false"))
            {
                if (BuyerAccessLevel == 1)
                {
                    if (!ChildBuyerPages.Contains(RequestedPage))
                        Response.Redirect("~/UI/BuyerDefault.aspx?Query=refresh");
                }
                else if (BuyerAccessLevel == 2)
                {
                    if (!ParentBuyerPages.Contains(RequestedPage))
                        Response.Redirect("~/UI/BuyerDefault.aspx?Query=refresh");
                }
            }
            else if ((Convert.ToString(Session["LoginEntityTypeID"]) == "2") && (IsDirectBuyer.ToLower() == "true"))
            {
                if (!ParentBuyerPages.Contains(RequestedPage))
                    Response.Redirect("~/UI/BuyerDefault.aspx?Query=refresh");
            }
        }

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
            //Cache.Remove(CacheEnum.BuyerAnnouncement);
            #endregion

        }

        #region [Added by Vipin 14 jan 2013 for get EntityId using User Id]
        protected void GetEntityIdbySecurityId()
        {
            DataTable dtEntityId = new DataTable();
            dtEntityId = objLoginBLL.EntityIdByUserId(Constant.UserId);
            if (dtEntityId.Rows.Count > 0)
            {
                hdnURL.Value = "../UI/ViewBuyerDetails.aspx?Mode=View&BuyerId=" + Convert.ToString(dtEntityId.Rows[0]["EntityID"]) + "&type=" + Convert.ToString(Session["LoginEntityTypeID"]);
            }
        }
        #endregion
    }
}