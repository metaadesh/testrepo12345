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
using SSOLib.Service;
using dotless;
using EcmaScript;
using Yahoo;
using SquishIt;
using SquishIt.Framework;
using SquishIt.Framework.Css.Compressors;
using SquishIt.Framework.JavaScript.Minifiers;

namespace METAOPTION.VMSReports
{
    public partial class BuyerReportMaster : System.Web.UI.MasterPage
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
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckRequestPage();
            lblCopyright.Text = ConfigurationManager.AppSettings["Copyright"];
            if (!IsPostBack)
            {
                //Get default system
                try
                {
                    Int32 sysID = objLoginBLL.GetDefaultSystem(Convert.ToString(Session["OrgCode"]));
                    if (Session["dtSystems"] != null && Session["SystemID"] != null)
                    {

                        dtSystems = (DataTable)Session["dtSystems"];
                        if (Session["SystemID"].ToString() == "-1")
                            imgLogo.Src = dtSystems.Select(String.Format("SystemID={0}", sysID))[0].ItemArray[2].ToString();
                        else
                            imgLogo.Src = dtSystems.Select("SystemID=" + Session["SystemID"].ToString())[0].ItemArray[2].ToString();
                    }
                    if ((Session["LoginEntityTypeID"] != null) && (Convert.ToString(Session["LoginEntityTypeID"]) == "2"))
                    {
                        GetEntityIdbySecurityId();
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
                lblLastLogin.Text = "Last Login was on: " + objLoginBLL.GetLastLogin(empId).ToString();
            }

        }

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

            ChildBuyerPages.Add("ViewAnnouncementList.aspx");
            ChildBuyerPages.Add("AddNewAnnouncement.aspx");
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


            ParentBuyerPages.Add("ViewAnnouncementList.aspx");
            ParentBuyerPages.Add("AddNewAnnouncement.aspx");
            ParentBuyerPages.Add("AnnouncementDetails.aspx");
            //Reports
            ParentBuyerPages.Add("ATCAuctionsOpenDealList.aspx");
            ParentBuyerPages.Add("AvgCommissionReport.aspx");
            ParentBuyerPages.Add("BlockRunList.aspx");
            ParentBuyerPages.Add("BlockRunListEnhanced.aspx");
            ParentBuyerPages.Add("BlockRunListExcludingCost.aspx");
            ParentBuyerPages.Add("BuyersReport.aspx");
            ParentBuyerPages.Add("BuyersReportV2.aspx");
            ParentBuyerPages.Add("BuyerInventory.aspx");
            ParentBuyerPages.Add("ChargeBackReport.aspx");
            ParentBuyerPages.Add("CommissionReport.aspx");
            ParentBuyerPages.Add("NetProfitAndLoss.aspx");
            ParentBuyerPages.Add("NetProfitAndLossSpecialCaseNotes.aspx");
            ParentBuyerPages.Add("NoTitlesReport.aspx");
            ParentBuyerPages.Add("PurchasedCarsByBuyers.aspx");
            ParentBuyerPages.Add("SalesBreakDownReport.aspx");
            ParentBuyerPages.Add("UnpaidReport.aspx");
            ParentBuyerPages.Add("UnpaidReportSpecificDate.aspx");
            ParentBuyerPages.Add("SoldInventory.aspx");

            if ((Convert.ToString(Session["LoginEntityTypeID"]) == "2") && (IsDirectBuyer.ToLower() == "false"))
            {
                if (BuyerAccessLevel == 1)
                {
                    if (!ChildBuyerPages.Contains(RequestedPage))
                        Response.Redirect("../UI/BuyerDefault.aspx?Query=refresh");
                }
                else if (BuyerAccessLevel == 2)
                {
                    if (!ParentBuyerPages.Contains(RequestedPage))
                        Response.Redirect("../UI/BuyerDefault.aspx?Query=refresh");
                }
            }
            else if ((Convert.ToString(Session["LoginEntityTypeID"]) == "2") && (IsDirectBuyer.ToLower() == "true"))
            {
                if (!ParentBuyerPages.Contains(RequestedPage))
                    Response.Redirect("../UI/BuyerDefault.aspx?Query=refresh");
            }
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Util.Logout_Session();
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