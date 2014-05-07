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
    public partial class MasterPageNoLeftPanel : System.Web.UI.MasterPage
    {
        DataTable dtSystems;
        LoginBLL objLoginBLL = new LoginBLL();
        string strContentPage;
        private string _Error = string.Empty;

        public string PageMessage
        {
            get { return _Error; }
            set { _Error = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckPermission();
            lblCopyright.Text = ConfigurationManager.AppSettings["Copyright"];
            btnChange.Visible = false;
            DDLSystem.Enabled = false;

            if (!IsPostBack)
            {

                if (Session["dtSystems"] != null && Session["SystemID"] != null)
                {
                    dtSystems = (DataTable)Session["dtSystems"];
                    DDLSystem.DataSource = dtSystems;
                    DDLSystem.DataTextField = "Description";
                    DDLSystem.DataValueField = "SystemId";
                    DDLSystem.DataBind();
                    ListItem item = new ListItem("All", "-1");
                    DDLSystem.Items.Insert(0, item);
                    DDLSystem.SelectedValue = Session["SystemID"].ToString();

                    //if (Session["SystemID"].ToString() != "-1")
                    //    imgLogo.Src = dtSystems.Select("SystemID=" + Session["SystemID"].ToString())[0].ItemArray[2].ToString();
                    //else
                    //    imgLogo.Src = dtSystems.Select("SystemID=10")[0].ItemArray[2].ToString();
                    try
                    {
                        DataRow[] drow = dtSystems.Select(String.Format("systemID={0}", DDLSystem.SelectedValue));


                        if (DDLSystem.SelectedValue == "-1")
                            imgLogo.Src = "../images/Logo.gif";
                        else
                            imgLogo.Src = Convert.ToString(drow[0][2]);
                        lblOrgName.Text = Constant.OrganisationName;
                    }
                    catch { }
                }
            }
            //CheckPermission();            
            lblUIMessage.Visible = false;

            if (Session["empId"] == null)
                Response.Redirect("~/Login.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
            else
            {
                lblWelcome.Text = "Welcome " + Session["disName"].ToString();
                long empId = Convert.ToInt64(Session["empId"].ToString());
                lblLastLogin.Text = "Last Login was on: " + objLoginBLL.GetLastLogin(empId).ToString();

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
        #region Old code
        //private void CheckPermission()
        //{
        //    List<String> Permissions = CommonBAL.GetPagePermission(Constant.UserId, "DEFAULT");
        //    /// Add new inventory Right
        //    if (!(Permissions.Contains("INVENTORY.ADDINVENTORY")))
        //    {
        //        this.hlnkAddInventoryMenu.Enabled = false;
        //        this.hlnkAddInventoryMenu.ToolTip = "Permission Protected";
        //    }

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
        //}
        #endregion


        #region [Inventory Search]
        /// <summary>
        /// Search Inventory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        #endregion

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
            try
            {
                if (DDLSystem.SelectedValue == "10")
                    imgLogo.Src = dtSystems.Select("SystemID=10")[0].ItemArray[2].ToString();
                else if (DDLSystem.SelectedValue == "11")
                    imgLogo.Src = dtSystems.Select("SystemID=11")[0].ItemArray[2].ToString();
                else if (DDLSystem.SelectedValue == "12")
                    imgLogo.Src = dtSystems.Select("SystemID=12")[0].ItemArray[2].ToString();
                else
                    imgLogo.Src = "../images/Logo.gif";
            }
            catch { }
            strContentPage = Page.AppRelativeVirtualPath;
            Response.Redirect(strContentPage);

        }

        #region [Fill System's Dropdown List ]
        private void FillSystemDDL()
        {
            dtSystems = new DataTable();
            if (Session["dtSystems"] == null)
            {
                dtSystems = InventoryBAL.FetchSystems(Constant.OrgID);
                Session["dtSystems"] = dtSystems;
            }
            else
                dtSystems = (DataTable)Session["dtSystems"];
            DDLSystem.DataSource = dtSystems;
            DDLSystem.DataTextField = "Description";
            DDLSystem.DataValueField = "SystemId";
            DDLSystem.DataBind();
            ListItem item = new ListItem("All", "-1");
            DDLSystem.Items.Insert(0, item);

        }
        #endregion

        #region[Scan Related]
        protected void btnScan_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtScanVIN.Text))
            {
                QuickInventorySearch_ScanResult[] result = InventoryBAL.QuickSearchScanInventories(Convert.ToInt32(ddlVINPartern.SelectedValue), txtScanVIN.Text.Trim(), Constant.OrgID,Constant.UserId);
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
            //    Response.Write("<script>alert('Please enter the VIN');</script>");
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

                    row["InventoryID"] = ((Label)(grvrow.FindControl("lblInventoryID"))).Text;
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
