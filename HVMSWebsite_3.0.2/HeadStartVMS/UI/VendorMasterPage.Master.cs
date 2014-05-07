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

namespace METAOPTION.UI
{
    public partial class VendorMasterPage : System.Web.UI.MasterPage
    {
        DataTable dtSystems;
        LoginBLL objLoginBLL = new LoginBLL();
        string strContentPage;

        private string _Error = string.Empty;
        //Set Messages from U.I
        public string PageMessage
        {
            get { return _Error; }
            set
            {
                _Error = value;
            }

        }

        // LoginBAL objLoginBAL = new LoginBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckRequestPage();
            lblCopyright.Text = ConfigurationManager.AppSettings["Copyright"];
            btnChange.Visible = false;
            DDLSystem.Enabled = false;

            //List<String> rights = CommonBAL.GetPagePermission(Constant.UserId, "WEBAPP");
            //if (rights.Count < 1)
            //    Response.Redirect("~/UI/Permission.aspx?MSG=WEBAPP.LOGIN");
            //else
            //{

            if (Page.AppRelativeVirtualPath.ToLower() == "~/ui/vendordefault.aspx")
            {
                btnChange.Visible = true;
                DDLSystem.Enabled = true;
            }


            if (!IsPostBack)
            {

                FillSystemDDL();
                //Get default system

                try
                {
                    Int32 sysID = objLoginBLL.GetDefaultSystem(Convert.ToString(Session["OrgCode"]));
                    if (DDLSystem.Items.Count > 0)
                    {
                        if (Session["SystemId"] == null)
                        {
                            DDLSystem.SelectedIndex = 1;
                            Session["SystemId"] = DDLSystem.SelectedValue;//Default value for session variable
                        }
                        else
                        {
                            DDLSystem.SelectedValue = Session["SystemId"].ToString();
                            if (DDLSystem.SelectedValue != "-1")
                                imgLogo.Src = dtSystems.Select("SystemID=" + DDLSystem.SelectedValue)[0].ItemArray[2].ToString();
                            else
                                imgLogo.Src = dtSystems.Select(String.Format("SystemID={0}", sysID))[0].ItemArray[2].ToString();


                        }
                    }
                    else
                        Session["SystemId"] = Constant.SystemID;

                    if (dtSystems != null && dtSystems.Rows.Count > 0) //Set Peachtree value in session for current System
                    {
                        if (DDLSystem.SelectedValue == "-1")
                            Session["PeachTreeValue"] = dtSystems.Select(String.Format("SystemID={0}", sysID))[0].ItemArray[3].ToString();
                        else
                            Session["PeachTreeValue"] = dtSystems.Select("SystemID=" + DDLSystem.SelectedValue)[0].ItemArray[3].ToString();
                    }
                    if ((Session["LoginEntityTypeID"] != null) && (Convert.ToString(Session["LoginEntityTypeID"]) == "3"))
                    {
                        GetEntityIdbySecurityId();
                    }
                    lblOrgName.Text = Constant.OrganisationName;
                }
                catch { }

            }

            lblUIMessage.Visible = false;

            if (Session["empId"] == null)
                Response.Redirect("~/Login.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
            else
            {
                lblWelcome.Text = "Welcome " + Session["disName"].ToString();
                long empId = Convert.ToInt64(Session["empId"].ToString());
                lblLastLogin.Text = "Last Login was on: " + objLoginBLL.GetLastLogin(empId).ToString();

                // CheckPermission();

                UpdateSystemStatus();

            }
            // }

        }

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
            //ParentBuyerPages.Add("ViewAllCommission.aspx");
            //ParentBuyerPages.Add("ViewAllTitleStatus.aspx");
            ParentBuyerPages.Add("ViewAllExpenses.aspx");
            ParentBuyerPages.Add("PreExpense.aspx");
            ParentBuyerPages.Add("ViewLocation.aspx");
            ParentBuyerPages.Add("VinScanLog.aspx");
            ParentBuyerPages.Add("GenericImages.aspx");
            ParentBuyerPages.Add("Payments.aspx");
            ParentBuyerPages.Add("ExpenseAgainstPayment.aspx");

            ParentBuyerPages.Add("InventoryDetail.aspx");

            ParentBuyerPages.Add("PrintPreview_ExpenseAgainstPayment.aspx");
            ParentBuyerPages.Add("VendorSystemStats.aspx");


            if ((Convert.ToString(Session["LoginEntityTypeID"]) == "3"))
            {
                if (!ParentBuyerPages.Contains(RequestedPage))
                    Response.Redirect("~/UI/VendorDefault.aspx?Query=refresh");
            }
        }
        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Util.Logout_Session();
            //Session.Abandon();
            //Response.Redirect("~/Login.aspx");
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

            lblUIMessage.Text = _Error;
            if (this._Error.Length > 0)
                lblUIMessage.Visible = true;

        }

        #region old code
        //private void CheckPermission()
        //{
        //    #region INVENTORY
        //    List<String> Permissions = CommonBAL.GetPagePermission(Constant.UserId, "DEFAULT");
        //    /// Add new inventory Right
        //    if (!(Permissions.Contains("INVENTORY.ADDINVENTORY")))
        //    {
        //        this.hlnkAddInventoryMenu.Enabled = false;
        //        this.hlnkAddInventoryMenu.ToolTip = "Permission Protected";
        //    }
        //    #endregion

        //    #region [ SECURITY FOR ANNOUNCEMENT ADD/VIEW ]
        //    Permissions = CommonBAL.GetPagePermission(Constant.UserId, "ANNOUNCEMENT");
        //    //Count checking for those user who does not have any permission yet but their user & password is active.
        //    bool bAnnouncement = false;
        //    if (Permissions.Count > 0)
        //    {
        //        if (!(Permissions.Contains("ANNOUNCEMENT.VIEW")))
        //        {
        //            this.hplViewAnnouncement.ToolTip = "Permission Protected";
        //            this.hplViewAnnouncement.Enabled = false;
        //            this.hplAddAnnouncement.ToolTip = "Permission Protected";
        //            this.hplAddAnnouncement.Enabled = false;
        //        }
        //        else
        //        {
        //            bAnnouncement = true;
        //        }
        //        if (!(Permissions.Contains("ANNOUNCEMENT.DELETE")))
        //        {
        //            if (bAnnouncement == false)
        //            {
        //                this.hplViewAnnouncement.ToolTip = "Permission Protected";
        //                this.hplViewAnnouncement.Enabled = false;
        //                this.hplAddAnnouncement.ToolTip = "Permission Protected";
        //                this.hplAddAnnouncement.Enabled = false;
        //            }
        //        }
        //        else
        //        {
        //            bAnnouncement = true;
        //            this.hplViewAnnouncement.Enabled = true;
        //        }
        //        if (!(Permissions.Contains("ANNOUNCEMENT.EDIT")))
        //        {
        //            if (bAnnouncement == false)
        //            {
        //                this.hplViewAnnouncement.ToolTip = "Permission Protected";
        //                this.hplViewAnnouncement.Enabled = false;
        //                this.hplAddAnnouncement.ToolTip = "Permission Protected";
        //                this.hplAddAnnouncement.Enabled = false;
        //            }
        //        }
        //        else
        //        {
        //            bAnnouncement = true;
        //            this.hplViewAnnouncement.Enabled = true;
        //        }
        //        if (!(Permissions.Contains("ANNOUNCEMENT.ADD")))
        //        {
        //            this.hplAddAnnouncement.ToolTip = "Permission Protected";
        //            this.hplAddAnnouncement.Enabled = false;
        //        }
        //        else
        //        {
        //            this.hplAddAnnouncement.Enabled = true;
        //        }

        //    }
        //    else
        //    {
        //        this.hplAddAnnouncement.ToolTip = "Permission Protected";
        //        this.hplAddAnnouncement.Enabled = false;
        //        this.hplViewAnnouncement.Enabled = false;
        //    }

        //    #endregion

        //    #region [ LANE SECURITY ASSIGNMENT ]
        //    Permissions = CommonBAL.GetPagePermission(Constant.UserId, "LANEASSIGNMENT");
        //    bool bLaneSecurity = false;
        //    //Count checking for those user who does not have any permission yet but their user & password is active.
        //    if (Permissions.Count > 0)
        //    {
        //        if ((Permissions.Contains("LANEASSIGNMENT.EDIT")))
        //        {
        //            bLaneSecurity = true;
        //        }
        //        else if ((Permissions.Contains("LANEASSIGNMENT.VIEW")))
        //        {
        //            bLaneSecurity = true;
        //        }
        //        if (bLaneSecurity == false)
        //        {
        //            this.hplLaneAssignment.Attributes.Add("onclick", "return false;");
        //            this.hplLaneAssignment.Enabled = false;
        //        }
        //    }
        //    else
        //    {
        //        this.hplLaneAssignment.Attributes.Add("onclick", "return false;");
        //        this.hplLaneAssignment.Enabled = false;
        //    }
        //    #endregion

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

        //    #region [ Security for Payments ]
        //    Permissions = CommonBAL.GetPagePermission(Constant.UserId, "PAYMENT");

        //    if (!Permissions.Contains("MAKEANEWPAYMENT.ADD"))
        //        lnkMakeANewPayment.Enabled = false;

        //    if (!Permissions.Contains("ACCOUNTSPAYABLE.VIEW"))
        //        lnkAccountsPayable.Enabled = false;

        //    if (!Permissions.Contains("ALLPAYMENT.VIEW"))
        //    {
        //        lnkPayments.Enabled = false;
        //        lnkSearchPayment.Enabled = false;
        //    }
        //    #endregion [ Security for Payments ]

        #endregion

        //}
        /// <summary>
        /// Update System Status
        /// </summary>

        private void UpdateSystemStatus()
        {
            // #region [ System Stats ]

            //Nullify Session object if request from homepage i.e query="refresh" and VINNO empty
            if (!string.IsNullOrEmpty(Request.QueryString["Query"]))
                // if (Request.QueryString["Query"] == "refresh" && string.IsNullOrEmpty(txtVINNo.Text))
                //     Session["SystemStatusValues"] = null;

                // this.lblLiveUsers.Text = Convert.ToString(Constant.LiveUsers);

                if (Session["SystemStatusValues"] == null)
                {
                    spSystemStatsResult stats = BAL.Common.SystemStats(Convert.ToInt32(Session["SystemID"]), Constant.OrgID);
                    if (stats == null) return;
                    Session["SystemStatusValues"] = stats;
                }

            spSystemStatsResult sysStatus = Session["SystemStatusValues"] as spSystemStatsResult;

            //if (sysStatus.Vehicles_Added_Today <= 1)
            //    lblVehiclesAddedToday.Text = sysStatus.Vehicles_Added_Today.ToString() + " Vehicle";
            //else
            //    lblVehiclesAddedToday.Text = sysStatus.Vehicles_Added_Today.ToString() + " Vehicles";

            #region[Optimized By Adesh]

            //dlistRegular.DataSource = BAL.Common.LaneStatic('R');
            //dlistRegular.DataBind();

            //dListExotic.DataSource = BAL.Common.LaneStatic('E');
            //dListExotic.DataBind();


            //System.Collections.ArrayList arraylist = new ArrayList();
            //arraylist = BAL.Common.LaneStatic_New();

            //if (arraylist.Count > 0)
            //{
            //    dlistRegular.DataSource = arraylist[0];
            //    dlistRegular.DataBind();

            //    dListExotic.DataSource = arraylist[1];
            //    dListExotic.DataBind();
            //}
            #endregion

            // lblTotalInventoryVehicles.Text = string.Format("{0:0,0} Vehicles", sysStatus.Number_Inventory_Vehicles);

            // lblAvgInvCostPerVehicle.Text = "$" + string.Format("{0:N} Per Vehicle", sysStatus.Average_Inventory_Vehicles);


            ///////////////Added by Rupendra 21 Aug 12 for Display Current PreExpense, PreInventory, Image, Location Added////
            GetMobileActivityCurrentDetails_Ver211Result objDetails = BAL.Common.GetMobileActivityDetails_Ver211(Convert.ToInt32(Session["LoginEntityTypeID"].ToString()), Convert.ToInt32(Constant.UserId));
            //lblInventoryTodayAdd.Text = objDetails.NewPreInventory + " Vehicles Added, " + objDetails.PreInvPending + " Pending Approval";
            lblPreExpenseToday.Text = objDetails.NewPreExpense + " Expenses Added, " + objDetails.PendingPreExpense + " Pending Approval";
            lblVINScanToday.Text = objDetails.LocationCapture + " Location Capture, " + objDetails.VINLocation + " VIN Scan";
            lblMObileActivityImage.Text = "Pictures: " + objDetails.PreExpenseImage + " PreExpenses, " + objDetails.GenericImage + " Generic";//objDetails.PreInventoryImage + " PreInventory, " +
            ////////////////////////////////////////End///////////////////////////////////////////////////////////////////////

        }

        #region Old Code
        //if (rights.Contains("STATS.TOTALINVENTORYDOLLERS"))
        //{
        //    SetStats(count, "Average Inventory $ per Vehicle", String.Format("{0:N} $ Per Vehicle", stats.Average_Inventory_Vehicles));
        //    count++;
        //} if (count == 3) return;



        //List<String> rights = null;

        ////Put System Status Rights in Session refer BugId: 757:Check Permissions on Master Page 
        //// Check permission and do the the Show/Hide accordingly
        //if (Session["SystemStatusRights"] == null)
        //{
        //    rights = BAL.CommonBAL.GetPagePermission(Constant.UserId, "SYSTEM");
        //    Session["SystemStatusRights"] = rights;
        //}

        //rights = Session["SystemStatusRights"] as List<string>;

        //if (rights.Count == 0) return;
        //int count = 0;


        //if (stats.Vehicles_Added_Today <= 1)
        //    SetStats(count, "Vehicles Added Today", stats.Vehicles_Added_Today.ToString() + " Vehicle");
        //else
        //    SetStats(count, "Vehicles Added Today", stats.Vehicles_Added_Today.ToString() + " Vehicles");

        ////Bug Request 749:Add Exotic Count along with regular lane count
        ////Display Number of Vehicles in Regular Lane 22
        //if (stats.Total_Vehicles_In_Reg_Lane <= 1)
        //    SetStats(3, "Vehicles in Lane 22,12", stats.Total_Vehicles_In_Reg_Lane.ToString() + " Vehicle" + ", " + stats.Total_Vehicles_In_Exotic_Lane.ToString() + " Vehicle");
        //else
        //    SetStats(3, "Vehicles in Lane 22,12", stats.Total_Vehicles_In_Reg_Lane.ToString() + " Vehicles" + ", " + stats.Total_Vehicles_In_Exotic_Lane.ToString() + " Vehicles");

        //count++;

        //if (rights.Contains("STATS.NUMBEROFVEHICLEINSYSTEM"))
        //{
        //    SetStats(count, "Number of Inventory Vehicles", String.Format("{0:0,0} Vehicles", stats.Number_Inventory_Vehicles));
        //    count++;
        //}

        //if (rights.Contains("STATS.TOTALINVENTORYDOLLERS"))
        //{
        //    SetStats(count, "Average Inventory $ per Vehicle", String.Format("{0:N} $ Per Vehicle", stats.Average_Inventory_Vehicles));
        //    count++;
        //} if (count == 3) return;



        //#region[ On Hand ]
        //if (rights.Contains("STATS.NUMBER_ONHAND_VEHICLE"))
        //{
        //    SetStats(count, "Number of On-Hand Vehicles in the System", String.Format("{0:0,0} ", stats.Number_OnHand_Vehicles));
        //    count++;
        //} if (count == 3) return;

        //if (rights.Contains("STATS.AVG_$PER_ONHAND_VEHICLEE"))
        //{
        //    SetStats(count, "Average $ per On-Hand Vehicle", String.Format("{0:0,0} $ Per Vehicle", stats.Average_OnHand_Vehicles));
        //    count++;
        //} if (count == 3) return;
        //#endregion
        //#region[ Archived ]
        //if (rights.Contains("STATS.NUMBER_ARCHIVED_VEHICLE"))
        //{
        //    SetStats(count, "Number of Archived Vehicles in the System", String.Format("{0:0,0} ", stats.Number_Archived_Vehicles));
        //    count++;
        //} if (count == 3) return;

        //if (rights.Contains("STATS.AVG_$PER_ARCHIVED_VEHICLE"))
        //{
        //    SetStats(count, "Average $ per Archived Vehicle", String.Format("{0:0,0} $ Per Vehicle", stats.Average_Archived_Vehicles));
        //    count++;
        //} if (count == 3) return;

        //#endregion
        //#region[ Vehicles Added this week  ]
        //if (rights.Contains("STATS.VEHICLE_ADDED_THISWEEK"))
        //{
        //    SetStats(count, "Vehicles Added this week", String.Format("{0:0,0} ", stats.Vehicle_Added_WTD));
        //    count++;
        //} if (count == 3) return;

        //if (rights.Contains("STATS.VEHICLE_ADDED_MTD"))
        //{
        //    SetStats(count, "Vehicles Added MTD", String.Format("{0:N} ", stats.Vehicles_Added_MTD));
        //    count++;
        //} if (count == 3) return;

        //if (rights.Contains("STATS.VEHICLE_ADDED_YTD"))
        //{
        //    SetStats(count, "Vehicles Added YTD", String.Format("{0:N} ", stats.Vehicles_Added_YTD));
        //    count++;
        //} if (count == 3) return;
        //#endregion
        //#region[ Number of Cars not paid yet  ]
        //if (rights.Contains("STATS.NUMBER_CARS_NOTPAIDYET"))
        //{
        //    SetStats(count, "Number of Cars not paid yet", String.Format("{0:0,0} ", stats.CarsNotPaidYet));
        //    count++;
        //} if (count == 3) return;

        //if (rights.Contains("STATS.AMOUNT_CARS_NOTPAIDYET"))
        //{
        //    SetStats(count, "Amount of Cars not paid yet", String.Format("{0:0,0} $ Per Vehicle", stats.AmountNotPaidYet));
        //    count++;
        //} if (count == 3) return;

        //#endregion
        //#region[ Amount of Open Expenses  ]
        //if (rights.Contains("STATS.NUMBER_OPEN_EXPENSES"))
        //{
        //    SetStats(count, "Number of Open Expenses", String.Format("{0:0,0} ", stats.Number_OpenExpenses));
        //    count++;
        //} if (count == 3) return;

        //if (rights.Contains("STATS.AMOUNT_OPEN_EXPENSES"))
        //{
        //    SetStats(count, "Amount of Open Expenses", String.Format("{0:0,0} $ Per Vehicle", stats.Amount_OpenExpenses));
        //    count++;
        //} if (count == 3) return;

        //#endregion
        //#region[ Number of Open Commissions   ]
        //if (rights.Contains("STATS.NUMBER_OPEN_COMMISSIONS"))
        //{
        //    SetStats(count, "Number of Open Commissions", String.Format("{0:0,0} ", stats.Number_OpenCommissions));
        //    count++;
        //} if (count == 3) return;

        //if (rights.Contains("STATS.AMOUNT_OPEN_COMMISSIONS"))
        //{
        //    SetStats(count, "Amount of open commissions", String.Format("{0:0,0} $ Per Vehicle", stats.Amount_OpenCommissions));
        //    count++;
        //}

        //#endregion


        // #endregion
        // }

        //private void SetStats(int Count, String Text, String Value)
        //{
        //   if (Count == 4) return;

        //   switch (Count)
        //   {
        //      case 0:
        //         this.trStats1.Visible = true;
        //         this.trStats11.Visible = true;
        //         lblStat1.Text = Text;
        //         lblStat11.Text = Value;
        //         break;
        //      case 1:
        //         this.trStats2.Visible = true;
        //         this.trStats22.Visible = true;
        //         lblStat2.Text = Text;
        //         lblStat22.Text = Value;
        //         break;
        //      case 2:
        //         this.trStats3.Visible = true;
        //         this.trStats33.Visible = true;
        //         lblStat3.Text = Text;
        //         lblStat33.Text = Value;
        //         break;

        //      case 3:
        //         this.trStats4.Visible = true;
        //         this.trStats44.Visible = true;
        //         lblStat4.Text = Text;
        //         lblStat44.Text = Value;
        //         break;
        //      default: break;
        //   }
        //}

        #endregion

        /// <summary>
        /// Search Inventory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearchInv_Click(object sender, EventArgs e)
        {

            //if (string.IsNullOrEmpty(txtVINNo.Text))
            //    return;

            // QuickInventorySearchResult[] result = InventoryBAL.QuickSearchInventories(Convert.ToInt32(ddlVINPartern.SelectedValue), txtVINNo.Text.Trim());
            //if one inventory record found,open manageinventory page
            //   if (result.Count() == 1)
            //   {
            //        Response.Redirect("InventoryDetail.aspx?Code=" + result[0].InventoryId + "");
            //      }
            //      else
            //        Response.Redirect("QuickSearch.aspx?param1=" + ddlVINPartern.SelectedValue + "&param2=" + txtVINNo.Text.Trim() + "");

        }

        protected void lnkRefSystemStats_Click(object sender, EventArgs e)
        {
            Session["SystemStatusValues"] = null;
            UpdateSystemStatus();
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            Session["SystemId"] = DDLSystem.SelectedValue;
            if (Session["dtSystems"] == null)
            {
                dtSystems = InventoryBAL.FetchSystems(Constant.OrgID);
                Session["dtSystems"] = dtSystems;
            }
            else
                dtSystems = (DataTable)Session["dtSystems"];

            DataRow[] drow = dtSystems.Select(String.Format("systemID={0}", DDLSystem.SelectedValue));


            if (DDLSystem.SelectedValue == "-1")
            {
                //Get default system
                Int32 sysID = objLoginBLL.GetDefaultSystem(Convert.ToString(Session["OrgCode"]));
                imgLogo.Src = "../images/Logo.gif";
                Session["PeachTreeValue"] = dtSystems.Select(String.Format("SystemID={0}", sysID))[0].ItemArray[3].ToString();
            }
            else
            {
                imgLogo.Src = Convert.ToString(drow[0][2]);
                Session["PeachTreeValue"] = Convert.ToString(drow[0][3]);

            }


            Session.Remove("SystemStatusValues");//Remove session status variable from session

            strContentPage = Page.AppRelativeVirtualPath;
            Response.Redirect(strContentPage);

        }

        #region [Fill System's Dropdown List ]

        private void FillSystemDDL()
        {
            dtSystems = new DataTable();
            // Fetch System's info
            if (Session["dtSystems"] == null)
            {
                dtSystems = InventoryBAL.FetchSystems(Constant.OrgID);
                Session["dtSystems"] = dtSystems;
            }
            else
                dtSystems = (DataTable)Session["dtSystems"];

            // Bind system's DDL to its data
            DDLSystem.DataSource = dtSystems;
            DDLSystem.DataTextField = "Description";
            DDLSystem.DataValueField = "SystemId";
            DDLSystem.DataBind();
            ListItem item = new ListItem("All", "-1");// 
            DDLSystem.Items.Insert(0, item);

        }
        #endregion

        protected void Timer1_Tick(object sender, EventArgs e)
        {

        }

        #region[Scan Related]
        protected void btnScan_Click(object sender, EventArgs e)
        {

            //if (!string.IsNullOrEmpty(txtScanVIN.Text))
            //{
            //    QuickInventorySearchResult[] result = InventoryBAL.QuickSearchInventories(Convert.ToInt32(ddlVINPartern.SelectedValue), txtScanVIN.Text.Trim());
            //    //if one inventory record found,open scanner popup
            //    if (result.Count() == 1)
            //    {
            //        DataTable dt = new DataTable();
            //        DocumentBAL docBAL = new DocumentBAL();
            //        dt = docBAL.GetYearMakeByVIN(txtScanVIN.Text, Convert.ToInt32(ddlVINPartern.SelectedValue));
            //        if (dt.Rows.Count > 0)
            //        {
            //            mpeScan.Show();
            //            DataTable dtYMMB = docBAL.GetYMMBbyInventoryId(Convert.ToInt32(dt.Rows[0]["InventoryID"]));
            //            lblScan.Text = "Scan Document " + Convert.ToString(dtYMMB.Rows[0]["YMMB"]);
            //            frmScanVINSelection.Attributes.Add("src", String.Format("Scanner.aspx?etid={0}&eid={1}&VIN={2}&Year={3}&Make={4}", 6, dt.Rows[0]["InventoryId"], txtScanVIN.Text, dt.Rows[0]["Year"], dt.Rows[0]["Make"]));
            //        }
            //    }
            //    else if (result.Count() > 1)
            //    {
            //        mpeScan.Hide();
            //        BindInvGrid();
            //        mpeSelInv.Show();
            //        //frmAshar.Attributes.Add("src", "Scanner.aspx");
            //        //frmAshar.Attributes.Add("src", String.Format("Scanner.aspx?etid={0}&eid={1}&VIN={2}&Year={3}&Make={4}", 6, hfInventoryID.Value, result.Rows[0]["VIN"], result.Rows[0]["Year"], result.Rows[0]["Make"]));
            //    }
            //}
            //else
            //    return;
        }

        //protected void BindInvGrid()
        //{
        //    DocumentBAL docBAL = new DocumentBAL();
        //    DataTable dt = docBAL.GetYearMakeByVIN(txtScanVIN.Text, Convert.ToInt32(ddlVINPartern.SelectedValue));
        //    gvSelInv.DataSource = dt;
        //    gvSelInv.DataBind();
        //}

        protected void btnOK_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("InventoryID");
            dt.Columns.Add("VIN");
            dt.Columns.Add("Year");
            dt.Columns.Add("Make");

            foreach (GridViewRow grvrow in gvSelInv.Rows)
            {
                RadioButton rdbtn = (RadioButton)grvrow.FindControl("rdselect");
                if (rdbtn != null && rdbtn.Checked)
                {
                    DataRow row = dt.NewRow();
                    /* Ashar Code */
                    /*
                    row["InventoryID"] = grvrow.FindControl("lblInventoryID"); 
                    row["VIN"] = grvrow.FindControl("lblVIN");
                    row["Year"] = grvrow.FindControl("lblYear");
                    row["Make"] = grvrow.FindControl("lblMake");
                    */

                    row["InventoryID"] = ((System.Web.UI.WebControls.Label)(grvrow.FindControl("lblInventoryID"))).Text;
                    row["VIN"] = ((System.Web.UI.WebControls.Label)(grvrow.FindControl("lblVIN"))).Text;
                    row["Year"] = ((System.Web.UI.WebControls.Label)(grvrow.FindControl("lblYear"))).Text;
                    row["Make"] = ((System.Web.UI.WebControls.Label)(grvrow.FindControl("lblMake"))).Text;

                    dt.Rows.Add(row);
                    break;
                }
            }

            //if (dt.Rows.Count > 0)
            //{
            //    DataTable dtbl = new DataTable();
            //    DocumentBAL docBAL = new DocumentBAL();
            //    //dtbl = docBAL.GetYearMakeByVIN(txtScanVIN.Text, Convert.ToInt32(ddlVINPartern.SelectedValue));
            //    dtbl = docBAL.GetYearMakeByInventoryId(Convert.ToInt32(dt.Rows[0]["InventoryID"].ToString()));
            //    if (dtbl.Rows.Count > 0)
            //    {
            //        DataTable dtYMMB = docBAL.GetYMMBbyInventoryId(Convert.ToInt32(dt.Rows[0]["InventoryID"]));
            //        if (dtYMMB.Rows.Count > 0)
            //        {
            //            lblScan.Text = "Scan Document " + Convert.ToString(dtYMMB.Rows[0]["YMMB"]);
            //            frmScanVINSelection.Attributes.Add("src", String.Format("Scanner.aspx?etid={0}&eid={1}&VIN={2}&Year={3}&Make={4}", 6, dtbl.Rows[0]["InventoryId"], txtScanVIN.Text, dtbl.Rows[0]["Year"], dtbl.Rows[0]["Make"]));
            //            mpeScan.Show();
            //        }
            //    }
            //}
        }
        #endregion

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