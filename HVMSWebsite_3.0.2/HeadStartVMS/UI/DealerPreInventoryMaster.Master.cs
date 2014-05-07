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
    public partial class DealerPreInventoryMaster : System.Web.UI.MasterPage
    {
        DataTable dtSystems;
        LoginBLL objLoginBLL = new LoginBLL();
        PreInventoryBAL PreInvBAL = new PreInventoryBAL();
        string strContentPage;

        private string _Error = string.Empty;
        //Set Messages from U.I
        public string PageMessage
        {
            set
            {
                _Error = value;
            }

        }

        private Int32 _ScanImagesCount = 0;

        public Int32 ScanImagesCount
        {
            get { return _ScanImagesCount; }
            set { _ScanImagesCount = value; }
        }

        private Int32 _SyncImagesCount = 0;
        public Int32 SyncImagesCount
        {
            get { return _SyncImagesCount; }
            set { _SyncImagesCount = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckRequestPage();
            lblCopyright.Text = ConfigurationManager.AppSettings["Copyright"];
            btnChange.Visible = false;
            DDLSystem.Enabled = false;

            if (Page.AppRelativeVirtualPath.ToLower() == "~/ui/default.aspx")
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

                //CheckPermission();

                UpdateStatus();
                VINLocationHistory();
            }
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

        public void CheckRequestPage()
        {

            string RequestedPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);


            List<string> ParentBuyerPages = new List<string>();
            ParentBuyerPages.Add("DealerDefault.aspx");
            ParentBuyerPages.Add("ViewDealer.aspx");
            ParentBuyerPages.Add("Payments.aspx");
            ParentBuyerPages.Add("ViewAllExpenses.aspx");
            ParentBuyerPages.Add("InventorySearch.aspx");
            ParentBuyerPages.Add("CompanyContacts.aspx");
            ParentBuyerPages.Add("PrintPreview_ExpenseAgainstPayment.aspx");
            ParentBuyerPages.Add("PreInventory.aspx");
            ParentBuyerPages.Add("EntityUserList.aspx");
            ParentBuyerPages.Add("ViewLocation.aspx");
            ParentBuyerPages.Add("VinScanLog.aspx");
            ParentBuyerPages.Add("GenericImages.aspx");
            ParentBuyerPages.Add("InventoryDetail.aspx");
            ParentBuyerPages.Add("QuickSearch.aspx");
            ParentBuyerPages.Add("ExpenseAgainstPayment.aspx");
            ParentBuyerPages.Add("DealerSystemStates.aspx");
            ParentBuyerPages.Add("PreInventoryDetail.aspx");

            ParentBuyerPages.Add("ViewAnnouncementList.aspx");
            //   ParentBuyerPages.Add("AddNewAnnouncement.aspx");
            ParentBuyerPages.Add("AnnouncementDetails.aspx");


            if (Convert.ToString(Session["LoginEntityTypeID"]) == "1")
            {
                if (!ParentBuyerPages.Contains(RequestedPage))
                    Response.Redirect("~/UI/DealerDefault.aspx?Query=refresh");
            }
        }

        protected void btnSearchInv_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtVINNo.Text))
                return;

            QuickInventorySearchResult[] result = InventoryBAL.QuickSearchInventories(Convert.ToInt32(ddlVINPartern.SelectedValue), txtVINNo.Text.Trim(), Constant.UserId, Constant.OrgID);
            //if one inventory record found,open manageinventory page
            if (result.Count() == 1)
            {
                Response.Redirect("InventoryDetail.aspx?Code=" + result[0].InventoryId + "");
            }
            else
                Response.Redirect("QuickSearch.aspx?param1=" + ddlVINPartern.SelectedValue + "&param2=" + txtVINNo.Text.Trim() + "");

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
            if (this._Error.Length > 0)
                lblUIMessage.Visible = true;

        }

        protected void lnkRefSystemStats_Click(object sender, EventArgs e)
        {
            Session["SystemStatusValues"] = null;
            UpdateStatus();
        }

        public void UpdateStatus()
        {
            if (Session["PreInvID"] != null)
            {
                DataTable dt = new DataTable();
                dt = PreInvBAL.GetPreInventoryDetailByID(Convert.ToInt64(Session["PreInvID"]));
                if (dt.Rows.Count > 0)
                {
                    lblDeviceName.Text = Convert.ToString(dt.Rows[0]["DeviceName"]);
                    if (Convert.ToString(dt.Rows[0]["DeviceID"]) == "5")
                    {
                        lblDeviceID.Visible = false;
                        //spDeviceID.Visible = true;
                    }
                    else
                    {
                        lblDeviceID.Visible = true;
                        lblDeviceID.Text = Convert.ToString(dt.Rows[0]["DeviceID"]);
                        //spDeviceID.Visible = true;
                        //spDeviceID.InnerText = Convert.ToString(dt.Rows[0]["DeviceID"]);
                    }

                    lblScanDate.Text = String.Format("Scanned Date: {0}", dt.Rows[0]["ScanDate"]);
                    lblScanBy.Text = String.Format("Scanned By: {0}", dt.Rows[0]["ScanBy"]);
                    lblSyncDate.Text = String.Format("Synced Date: {0}", dt.Rows[0]["SyncDate"]);
                    lblScanImageInfo.Text = String.Format("Total Images Scanned: {0}", dt.Rows[0]["ScanImageCount"]);
                    lblSyncImageInfo.Text = String.Format("Total Images Synced: {0}", dt.Rows[0]["SyncImageCount"]);
                    lblInventoryInfo.Text = String.Format("Added To Inventory: {0}", Convert.ToInt64(dt.Rows[0]["InventoryId"]) > 0 ? "YES" : "NO");

                    this.ScanImagesCount = String.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ScanImageCount"])) ? 0 : Convert.ToInt32(dt.Rows[0]["ScanImageCount"]);
                    this.SyncImagesCount = String.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["SyncImageCount"])) ? 0 : Convert.ToInt32(dt.Rows[0]["SyncImageCount"]);
                }
            }
        }

        public void VINLocationHistory()
        {
            DataTable dt = new DataTable();
            if (Session["PreInvID"] != null)
            {
                String VIN = PreInvBAL.GetVINFromPreInventory(Convert.ToInt64(Session["PreInvID"]), Constant.OrgID);
                dt = PreInvBAL.GetVINLocationHistory(VIN, Constant.OrgID);
                dlhistory.DataSource = dt;
                dlhistory.DataBind();
            }
        }

        protected void BindAllLocation()
        {
            if (Session["PreInvID"] != null)
            {
                String VIN = PreInvBAL.GetVINFromPreInventory(Convert.ToInt64(Session["PreInvID"]), Constant.OrgID);
                //LocationBAL bal = new LocationBAL();
                //gvAllLocation.DataSource = bal.FetchVINLocation(VIN, -1, -1);
                //gvAllLocation.DataBind();
                ObjectDataSource odsLocation = new ObjectDataSource();
                odsLocation.TypeName = "Metaoption.LocationBAL";
                odsLocation.SelectMethod = "FetchVINLocation";
                odsLocation.SelectCountMethod = "FetchVINLocationCount";
                odsLocation.EnablePaging = true;
                odsLocation.SelectParameters.Add("VIN", VIN);
                odsLocation.SelectParameters.Add("LocationID", "-1");
                odsLocation.SelectParameters.Add("AddedBy", "-1");
                odsLocation.SelectParameters.Add("StartRowIndex", DbType.Int32, gvAllLocation.PageIndex.ToString());
                odsLocation.SelectParameters.Add("MaximumRows", DbType.Int32, gvAllLocation.PageSize.ToString());
                odsLocation.SelectParameters.Add("OrgID", DbType.Int16, Constant.OrgID.ToString());
                gvAllLocation.DataSource = odsLocation;
                gvAllLocation.DataBind();
            }
        }

        protected void btnViewMore_Click(object sender, EventArgs e)
        {
            mpeLocation.Show();
            BindAllLocation();
        }

        protected void gvAllLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            mpeLocation.Show();
            gvAllLocation.PageIndex = e.NewPageIndex;
            BindAllLocation();
        }

        #region[Scan Related]
        protected void btnScan_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtScanVIN.Text))
            {
                QuickInventorySearch_ScanResult[] result = InventoryBAL.QuickSearchScanInventories(Convert.ToInt32(ddlVINPartern.SelectedValue), txtScanVIN.Text.Trim(), Constant.OrgID, Constant.UserId);
                //if one inventory record found,open scanner popup
                if (result.Count() == 1)
                {
                    DataTable dt = new DataTable();
                    DocumentBAL docBAL = new DocumentBAL();
                    dt = docBAL.GetYearMakeByVIN(txtScanVIN.Text, Convert.ToInt32(ddlVINPartern.SelectedValue), Constant.OrgID);
                    if (dt.Rows.Count > 0)
                    {
                        mpeScan.Show();
                        DataTable dtYMMB = docBAL.GetYMMBbyInventoryId(Convert.ToInt32(dt.Rows[0]["InventoryID"]));
                        lblScan.Text = "Scan Document " + Convert.ToString(dtYMMB.Rows[0]["YMMB"]);
                        frmScanner.Attributes.Add("src", String.Format("Scanner.aspx?etid={0}&eid={1}&VIN={2}&Year={3}&Make={4}", 6, dt.Rows[0]["InventoryId"], txtScanVIN.Text, dt.Rows[0]["Year"], dt.Rows[0]["Make"]));
                    }
                }
                else if (result.Count() > 1)
                {
                    mpeScan.Hide();
                    BindInvGrid();
                    mpeSelInv.Show();
                }
            }
            //else
            //    return;
        }

        protected void BindInvGrid()
        {
            DocumentBAL docBAL = new DocumentBAL();
            DataTable dt = docBAL.GetYearMakeByVIN(txtScanVIN.Text, Convert.ToInt32(ddlVINPartern.SelectedValue), Constant.OrgID);
            gvSelInv.DataSource = dt;
            gvSelInv.DataBind();
        }

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

                    row["InventoryID"] = ((Label)grvrow.FindControl("lblInventoryID")).Text;
                    row["VIN"] = ((Label)(grvrow.FindControl("lblVIN"))).Text;
                    row["Year"] = ((Label)(grvrow.FindControl("lblYear"))).Text;
                    row["Make"] = ((Label)(grvrow.FindControl("lblMake"))).Text;

                    dt.Rows.Add(row);
                    break;
                }
            }

            if (dt.Rows.Count > 0)
            {
                DataTable dtbl = new DataTable();
                DocumentBAL docBAL = new DocumentBAL();
                //dtbl = docBAL.GetYearMakeByVIN(txtScanVIN.Text, Convert.ToInt32(ddlVINPartern.SelectedValue));
                dtbl = docBAL.GetYearMakeByInventoryId(Convert.ToInt32(dt.Rows[0]["InventoryID"].ToString()));
                if (dtbl.Rows.Count > 0)
                {
                    DataTable dtYMMB = docBAL.GetYMMBbyInventoryId(Convert.ToInt32(dt.Rows[0]["InventoryID"]));
                    if (dtYMMB.Rows.Count > 0)
                    {
                        lblScan.Text = "Scan Document " + Convert.ToString(dtYMMB.Rows[0]["YMMB"]);
                        frmScanner.Attributes.Add("src", String.Format("Scanner.aspx?etid={0}&eid={1}&VIN={2}&Year={3}&Make={4}", 6, dtbl.Rows[0]["InventoryId"], txtScanVIN.Text, dtbl.Rows[0]["Year"], dtbl.Rows[0]["Make"]));
                        mpeScan.Show();
                    }
                }
            }
        }
        #endregion

        #region [Added by Rupendra 28 Dec 12 for get EntityId using User Id]

        protected void lnkViewVendorMenu_Click(object sender, EventArgs e)
        {
            DataTable dtEntityId = new DataTable();
            dtEntityId = objLoginBLL.EntityIdByUserId(Constant.UserId);
            if (dtEntityId.Rows.Count > 0)
            {
                Response.Redirect("~/UI/ViewDealer.aspx?Mode=View&EntityId=" + Convert.ToString(dtEntityId.Rows[0]["EntityID"]) + "&type=" + Convert.ToString(Session["LoginEntityTypeID"]));
            }
        }

        #endregion
    }
}