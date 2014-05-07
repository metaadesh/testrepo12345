#region "Name space section"
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using METAOPTION.BAL;
using System.Web.Services;
using System.Collections.Generic;
using METAOPTION;
using Microsoft.Reporting.WebForms;
using METAOPTION.Reports;

#endregion

namespace METAOPTION.UI
{
    public partial class LaneAssignment : System.Web.UI.Page
    {
        #region [Page Global object declariatioin]
        bool bSecurity = false;
        string GridviewInnerHTML = string.Empty;
        DataTable dt;
        #endregion

        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["LaneAssignmentsMaster"])))
                    this.MasterPageFile = Convert.ToString(Session["LaneAssignmentsMaster"]);
                else
                    Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(Request.Url.ToString()));
            }
            catch (Exception ex)
            {
                Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(Request.Url.ToString()));
            }
            
        }


        #region [ Page Load ]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Page.IsPostBack || Page.IsCallback))
            {
                // Save user id in hidden filed to used from JavaScript
                ViewState["PageSecurity"] = string.Empty;
                ViewState["DataTable"] = null;
                CheckPermission();
               
                if (Session["empId"] != null)
                    this.userId.Value = Convert.ToString(Session["empId"]);

                BindYear();
                BindMake(0);

                //RegisterScriptGrid(gvInventoryList);
                LoadExoticPattern();

                // hide exotic, online and virtual on page load
                chkShowExotic.Checked = false;
                chkShowOnline.Checked = false;

                // Show 50 records in grid if user is not Bob (BobSecurityUserID=13)
                if (!ConfigurationSettings.AppSettings["BobSecurityUserID"].ToString().Equals(this.userId.Value.ToString()))
                {
                  gvInventoryList.PageSize = Convert.ToInt32(ConfigurationSettings.AppSettings["LaneGridPageSize"].ToString());
                  ddlViewRecords.SelectedValue = ConfigurationSettings.AppSettings["LaneGridPageSize"].ToString();
                }

                //Load Lane Settings from User Previous P
                Load_Lane_Settings();

              
                //Bind GridView
                btnApplySorting_Click(sender, e);
                
                #region [For Count records]
                //Get Count of modified recored in current session
                ModifiedRecordCount(Constant.UserId);
                //Get Count upto 100 records for announcement and email
                Get100_Announcement_EmailRecordsCount(Constant.UserId);
                BindAllEmployeeNameAndEmail();
                ///Get count upto 10 recods since last login
                lnk10RecordsforEmailAnn.Text = "(0) Records for announcement and email last login";
                Get10_Announcement_EmailRecordsCountSinceLastLogin(Constant.UserId);
                #endregion

                

            }
        }
        #endregion

        private void CheckPermission()
        {
            try
            {
                List<String> Permissions = CommonBAL.GetPagePermission(Constant.UserId, "LANEASSIGNMENT");
                ViewState["PageSecurity"] = Permissions;
                /// Add new lane assignment Right
                //Count checking for those user who does not have any permission yet but their user & password is active 
                if (Permissions.Count > 0)
                {
                    if ((Permissions.Contains("LANEASSIGNMENT.EDIT")))
                    {
                        bSecurity = true;
                        lnk10RecordsforEmailAnn.Enabled = true;
                        lnk100RecordsForAnnounce_Email.Enabled = true;
                        lnk20RecordsModified.Enabled = true;
                        btnUpdate.Enabled = true;
                    }
                    else if ((Permissions.Contains("LANEASSIGNMENT.VIEW")))
                    {
                        bSecurity = true;
                        lnk10RecordsforEmailAnn.Enabled = false;
                        lnk100RecordsForAnnounce_Email.Enabled = false;
                        lnk20RecordsModified.Enabled = false;
                        btnUpdate.Enabled = false;
                    }
                    if (bSecurity == false)
                    {
                        Response.Redirect("Permission.aspx?msg=LANEASSIGNMENT.VIEW");
                    }
                }
                else
                {
                    Response.Redirect("Permission.aspx?msg=LANEASSIGNMENT.VIEW");
                }
            }
            catch(Exception ex)
            {
                Response.Redirect("Permission.aspx?msg=LANEASSIGNMENT.VIEW");
            }
            
        }

        #region[ Data Bind ]

        #region [ Bind Years ]
        /// <summary>
        /// Bind Years in dropdownlist
        /// </summary>
        private void BindYear()
        {
            this.ddlYear.DataSource = BAL.Common.GetYearList();
            this.ddlYear.DataTextField = "Year";
            this.ddlYear.DataValueField = "Year";
            this.ddlYear.DataBind();
            this.ddlYear.Items.Insert(0, new ListItem("", "-1"));
            this.ddlMake.Items.Insert(0, new ListItem("", "-1"));
            //this.ddlModel.Items.Insert(0, new ListItem("", "-1"));

           

            //RegisterScript(ddlYear);
            //RegisterScript(ddlMake);
            //RegisterScript(ddlModel);
            //RegisterScript(ddlViewRecords);
        }
        #endregion

        #region[ Bind Make ]
        /// <summary>
        /// Bind Dropdownlist with make details based on year selected
        /// </summary>
        private void BindMake(int year)
        {
          MasterBAL objMaster = new MasterBAL();

            //this.ddlModel.Items.Clear();

            this.ddlMake.DataSource = objMaster.GetMakeList();
            this.ddlMake.DataValueField = "MakeId";
            this.ddlMake.DataTextField = "VINDivisionName";
            this.ddlMake.DataBind();
            this.ddlMake.Items.Insert(0, new ListItem("", "-1"));

            //if (Session["UserLaneSettings"] == null)
            //    return;
            //LaneSettings objLaneSettings = (BAL.LaneSettings)Session["UserLaneSettings"];
            //if (objLaneSettings == null)
            //    return;

           
        }
        #endregion

        #region[ Bind Model ]
        /// Bind Dropdownlist with model details based on make id selected
        private void BindModel(long makeId)
        {
          /* Naushad - 10/01
            this.ddlModel.Items.Clear();
            this.ddlModel.DataSource = BAL.Common.GetModel(makeId);
            this.ddlModel.DataValueField = "ModelId";
            this.ddlModel.DataTextField = "Model";
            this.ddlModel.DataBind();
            this.ddlModel.Items.Insert(0, new ListItem("", "-1"));
          */
            //if (Session["UserLaneSettings"] == null)
            //    return;
            //LaneSettings objLaneSettings = (BAL.LaneSettings)Session["UserLaneSettings"];
            //if (objLaneSettings == null)
            //    return;

            ////if (ddlMake.Items.FindByValue(objLaneSettings.Make) != null)
            ////    ddlMake.SelectedValue = objLaneSettings.Make;

            //if (ddlModel.Items.FindByValue(objLaneSettings.Model) != null)
            //    ddlModel.SelectedValue = objLaneSettings.Model;

        }
        #endregion

        #region[ Bind Main Grid ]
        private void BindMainGrid(String Filter, String orderBy)
        {
            LaneAssignmentBAL oLane = new LaneAssignmentBAL();
            gvInventoryList.PageSize = Convert.ToInt32(this.ddlViewRecords.SelectedValue);
             dt = oLane.GetSortedInventoryRecords(Filter, orderBy);
             ViewState["DataTable"] = dt;
            this.gvInventoryList.DataSource = dt;
            this.gvInventoryList.DataBind();
        }
        #endregion

        #endregion

        #region [Selected Index Change ]

        #region[ Year Selected Index Change ]
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlYear.Items.Count > 0)
            {
                if (Convert.ToInt32(this.ddlYear.SelectedValue) == -1)
                {
                    this.ddlMake.Items.Clear();
                    this.ddlMake.Items.Insert(0, new ListItem("", "-1"));
                }
                else
                {
                    BindMake(Convert.ToInt32(this.ddlYear.SelectedValue));
                }
                // Clear Model ddl
                //this.ddlModel.Items.Clear();
                //this.ddlModel.Items.Insert(0, new ListItem("", "-1"));
            }
        }
        #endregion

        #region [Make Selected Index Change ]
        protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
        {
          // Naushad, 10/01/2009, Making Make independent of Year and hiding Model, user will just select Year/Make and can filter
          /*
            if (this.ddlMake.Items.Count > 0)
                if (Convert.ToInt32(this.ddlMake.SelectedValue) == -1)
                {
                    this.ddlModel.Items.Clear();
                    this.ddlModel.Items.Insert(0, new ListItem("", "-1"));
                }
                else
                {
                    BindModel(Convert.ToInt32(this.ddlMake.SelectedValue));
                }
           * */
        }
        #endregion

        #endregion

        #region [Filter]
        protected void btnFilter_Click(object sender, EventArgs e)
        {

            btnApplySorting_Click(sender, e);
        }
        #endregion [Filter]

        #region [History GridBind]
        void BindGrid(Int64 invetoryid)
        {
            
            LaneAssignmentBAL ObjLane = new LaneAssignmentBAL();
            List<string> lstMake = null;
            lstMake = ObjLane.GetMake(invetoryid);
            if (lstMake.Count > 0)
            {
                if (lstMake[0].ToString() == "0")
                {
                    lblYear.Text = "N/A";
                }
                else
                    lblYear.Text = lstMake[0].ToString();
                lblMake.Text = lstMake[1].ToString();
                lblModel.Text = lstMake[2].ToString();

            }
            gvLaneHistory.DataSource = ObjLane.SelectLaneAssignmentHistory(invetoryid);
            gvLaneHistory.DataBind();
            if (gvLaneHistory.Rows.Count == 0)
            {
                //  update the contents in the detail panel
                this.upShowHistory.Update();
                //  show the modal popup
                this.mdpShowHistory.Hide();
            }
        }
        #endregion [GridBind]

        #region [ gvInventoryList Page Index Changing GridBind]
        protected void gvInventoryList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInventoryList.PageIndex = e.NewPageIndex;
            btnApplySorting_Click(sender, new EventArgs());
        }
        #endregion [GridBind]

        #region [ gvInventoryList Row Editing]
        protected void gvInventoryList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvInventoryList.EditIndex = e.NewEditIndex;
            btnApplySorting_Click(sender, new EventArgs());
        }
        #endregion [ gvInventoryList Row Editing]

        #region [ For Edit & Update]
        protected void btnEdit_Click(object sender, ImageClickEventArgs e)
        {
            // Show the popup
            LaneAssignmentBAL oLane = new LaneAssignmentBAL();
            this.mpeEditLanes.Show();

            ImageButton img = (ImageButton)sender;
            GridViewRow gdrow = (GridViewRow)img.Parent.Parent;
            long inventoryId = Convert.ToInt64(gvInventoryList.DataKeys[gdrow.RowIndex].Value);
            this.divEditSection.Visible = true;

            DataTable dtInv = oLane.InventoryLaneEdiableFields(inventoryId);

            if (dtInv != null)
                if (dtInv.Rows.Count > 0)
                {
                    this.txtRegularNo.Text = Convert.ToString(dtInv.Rows[0]["RegularLane"]);
                    this.txtExoticNo.Text = Convert.ToString(dtInv.Rows[0]["ExoticLane"]);
                    this.txtVirtualNo.Text = Convert.ToString(dtInv.Rows[0]["VirtualLane"]);
                    this.txtOnlineNo.Text = Convert.ToString(dtInv.Rows[0]["OnLineLane"]);
                    this.txtMarketPrice.Text = Convert.ToDecimal(dtInv.Rows[0]["MarketPrice"]).ToString();
                    this.chkIsExotic.Checked = Convert.ToBoolean(dtInv.Rows[0]["IsExotic"]);
                    this.txtNotes.Text = Convert.ToString(dtInv.Rows[0]["Notes"]);
                }

            // Saving it to make possible to update the grid row without re - load
            Session["RowId"] = gdrow;

        }

        protected void btnEditUpdate_Click(object sender, EventArgs e)
        {
            LaneAssignmentBAL oLane = new LaneAssignmentBAL();
            GridViewRow gvRow = null;
            if (Session["RowId"] != null)
                gvRow = (GridViewRow)Session["RowId"];

            if (gvRow != null)
            {
                Int32 Result = -1;
                long inventoryId = Convert.ToInt64(gvInventoryList.DataKeys[gvRow.RowIndex].Value);

                Decimal marketPrice = 0;
                if (txtMarketPrice.Text.Trim() != string.Empty)
                    marketPrice = Convert.ToDecimal(this.txtMarketPrice.Text.Trim());

                Result = oLane.EditInventoryLanes(
                     this.chkIsExotic.Checked ? 1 : 0
                   , this.txtRegularNo.Text.Trim()
                   , this.txtExoticNo.Text.Trim()
                   , this.txtVirtualNo.Text.Trim()
                   , this.txtOnlineNo.Text.Trim()
                   , marketPrice
                   , inventoryId);

                if (Result == 0)
                    try
                    {

                        ((Label)gvInventoryList.Rows[gvRow.RowIndex].Cells[2].Controls[1]).Text = this.txtRegularNo.Text.Trim();
                        ((Label)gvInventoryList.Rows[gvRow.RowIndex].Cells[3].Controls[1]).Text = this.txtExoticNo.Text.Trim();
                        ((Label)gvInventoryList.Rows[gvRow.RowIndex].Cells[4].Controls[1]).Text = this.txtOnlineNo.Text.Trim();
                        ((Label)gvInventoryList.Rows[gvRow.RowIndex].Cells[5].Controls[1]).Text = this.txtVirtualNo.Text.Trim();
                        ((Label)gvInventoryList.Rows[gvRow.RowIndex].Cells[11].Controls[1]).Text = this.txtMarketPrice.Text.Trim();


                    }
                    catch (Exception ex) { }
            }
        }
        #endregion

        #region [ Apply Sorting ]
        protected void btnApplySorting_Click(object sender, EventArgs e)
        {

            //chkShowExotic.Checked = false;
            //chkShowOnline.Checked = false;
            
            String OrderBy = "";// Order By vwLaneAssignments.DateAdded " + this.ddlSort11.SelectedValue;
            String Sep = " , ";
            if (ddlSort1.SelectedItem.Text != "Choose")
            {
                OrderBy = "Order By " + ddlSort1.SelectedValue + " " + this.ddlSort11.SelectedValue;
            }
            
            if (ddlSort2.SelectedItem.Text != "Choose")
            {
                if (OrderBy.IndexOf(ddlSort2.SelectedValue.ToString()) == -1)
                {
                    if (OrderBy != "")
                    {
                        OrderBy = OrderBy + Sep;
                    }
                    else
                        OrderBy = "Order By ";
                    OrderBy = OrderBy + ddlSort2.SelectedValue + " " + this.ddlSort22.SelectedValue;
                }
            }

            // Shot - 3
            if (ddlSort3.SelectedItem.Text != "Choose")
            {
                if (OrderBy.IndexOf(ddlSort3.SelectedValue.ToString()) == -1)
                {
                    if (OrderBy != "")
                    {
                        OrderBy = OrderBy + Sep;
                    }
                    else
                        OrderBy = "Order By ";
                    OrderBy = OrderBy + ddlSort3.SelectedValue + " " + this.ddlSort33.SelectedValue + Environment.NewLine;
                }
            }

            // Short - 4
            if (ddlSort4.SelectedItem.Text != "Choose")
            {
                if (OrderBy.IndexOf(ddlSort4.SelectedValue.ToString()) == -1)
                {
                    if (OrderBy != "")
                    {
                        OrderBy = OrderBy + Sep;
                    }
                    else
                        OrderBy = "Order By ";
                    OrderBy = OrderBy + ddlSort4.SelectedValue + " " + this.ddlSort44.SelectedValue + Environment.NewLine;
                }
            }

            // Short - 5
            if (ddlSort5.SelectedItem.Text != "Choose")
            {
                if (OrderBy.IndexOf(ddlSort5.SelectedValue.ToString()) == -1)
                {
                    if (OrderBy != "")
                    {
                        OrderBy = OrderBy + Sep;
                    }
                    else
                        OrderBy = "Order By ";
                    OrderBy = OrderBy + ddlSort5.SelectedValue + " " + this.ddlSort55.SelectedValue + Environment.NewLine;
                }
            }


            // Sort - 6
            if (ddlSort6.SelectedItem.Text != "Choose")
            {
                if (OrderBy.IndexOf(ddlSort6.SelectedValue.ToString()) == -1)
                {
                    if (OrderBy != "")
                    {
                        OrderBy = OrderBy + Sep;
                    }
                    else
                        OrderBy = "Order By ";
                    OrderBy = OrderBy + ddlSort6.SelectedValue + " " + this.ddlSort66.SelectedValue + Environment.NewLine;
                }
            }
            
            //Save Sort Criteria for Next Visit

            string filter = Where();
            Session["Filter"] = filter;
            Session["OrderBy"] = OrderBy;
            Save_Lane_Settings();

            // Naushad, 9/29/09, if Exotic checkbox is checked then display in grid
            if (chkShowExotic.Checked)
            {
              gvInventoryList.Columns[3].ItemStyle.CssClass = "";
              gvInventoryList.Columns[3].HeaderStyle.CssClass = "";
            }
            else
            {
              gvInventoryList.Columns[3].ItemStyle.CssClass = "hideCol";
              gvInventoryList.Columns[3].HeaderStyle.CssClass = "hideCol";
            }

            if (chkShowOnline.Checked)
            {
              gvInventoryList.Columns[4].ItemStyle.CssClass = "";
              gvInventoryList.Columns[5].ItemStyle.CssClass = "";

              gvInventoryList.Columns[4].HeaderStyle.CssClass = "";
              gvInventoryList.Columns[5].HeaderStyle.CssClass = "";
            }
            else
            {
              gvInventoryList.Columns[4].ItemStyle.CssClass = "hideCol";
              gvInventoryList.Columns[5].ItemStyle.CssClass = "hideCol";

              gvInventoryList.Columns[4].HeaderStyle.CssClass = "hideCol";
              gvInventoryList.Columns[5].HeaderStyle.CssClass = "hideCol";
            }

            BindMainGrid(filter, OrderBy);

        }
        #endregion


        #region [Load & Save Lane Settings]
        /// <summary>
        /// Save Lane Settings and re-load page with same applied settings(if any)
        /// </summary>
        protected void Save_Lane_Settings()
        {
            BAL.LaneSettings objLaneSettings = new LaneSettings();

            objLaneSettings.ExoticPattern = txtExoticPattern.Text;

            if (chkShowExotic.Checked)
                objLaneSettings.IsShowExotic = true;

            if(chkShowOnline.Checked)
               objLaneSettings.IsShowOnlineandVirtual = true;
            
            objLaneSettings.ViewCount=Convert.ToInt32(ddlViewRecords.SelectedValue);

            objLaneSettings.SortKey1 = ddlSort1.SelectedValue;
            
            objLaneSettings.SortKey1Order = ddlSort11.SelectedValue;

            objLaneSettings.SortKey2 = ddlSort2.SelectedValue;
            
            objLaneSettings.SortKey2Order = ddlSort22.SelectedValue;

            objLaneSettings.SortKey3 = ddlSort3.SelectedValue;
            
            objLaneSettings.SortKey3Order = ddlSort33.SelectedValue;

            objLaneSettings.SortKey4 = ddlSort4.SelectedValue;
            
            objLaneSettings.SortKey4Order = ddlSort44.SelectedValue;

            objLaneSettings.SortKey5 = ddlSort5.SelectedValue;
            
            objLaneSettings.SortKey5Order = ddlSort55.SelectedValue;

            objLaneSettings.SortKey6 = ddlSort6.SelectedValue;
            
            objLaneSettings.SortKey6Order = ddlSort66.SelectedValue;

            objLaneSettings.Year = ddlYear.SelectedValue;

            objLaneSettings.Make = ddlMake.SelectedValue;

            //objLaneSettings.Model = ddlModel.SelectedValue;

            objLaneSettings.Lane = txtLaneNo.Text;

            objLaneSettings.LaneType = ddlLaneType.SelectedValue;

            objLaneSettings.gridPagePosition = gvInventoryList.PageIndex;

            //Save this object in Session to retreive on page load, if object not null
            Session["UserLaneSettings"] = objLaneSettings;
        }


        /// <summary>
        /// Load Lane Settings(if any)
        /// </summary>
        protected void Load_Lane_Settings()
        {
            if (Session["UserLaneSettings"] == null)
                return;
            LaneSettings objLaneSettings = (BAL.LaneSettings) Session["UserLaneSettings"];
            if (objLaneSettings == null)
                return;
            
            //Set Lane Control Filter/Sorting Criteria  Values from Session Object

            txtExoticPattern.Text = objLaneSettings.ExoticPattern;

            if(objLaneSettings.IsShowExotic)
              chkShowExotic.Checked = true;

            if (objLaneSettings.IsShowOnlineandVirtual)
                chkShowOnline.Checked = true; 

            ddlViewRecords.SelectedValue = Convert.ToString(objLaneSettings.ViewCount);

        
            ddlSort1.SelectedValue = objLaneSettings.SortKey1;

            ddlSort11.SelectedValue = objLaneSettings.SortKey1Order;

            ddlSort2.SelectedValue = objLaneSettings.SortKey2;

            ddlSort22.SelectedValue = objLaneSettings.SortKey2Order;

            ddlSort3.SelectedValue = objLaneSettings.SortKey3;

            ddlSort33.SelectedValue = objLaneSettings.SortKey3Order;

            ddlSort4.SelectedValue = objLaneSettings.SortKey4 ;

            ddlSort44.SelectedValue = objLaneSettings.SortKey4Order;

            ddlSort5.SelectedValue = objLaneSettings.SortKey5;

            ddlSort55.SelectedValue = objLaneSettings.SortKey5Order;

            ddlSort6.SelectedValue = objLaneSettings.SortKey6;

            ddlSort66.SelectedValue = objLaneSettings.SortKey6Order;

           
            if (ddlYear.Items.FindByValue(objLaneSettings.Year) != null)
              ddlYear.SelectedValue = objLaneSettings.Year;

            if (ddlYear.SelectedIndex != -1)
                BindMake(Convert.ToInt32(ddlYear.SelectedValue));

           
            if (ddlMake.Items.FindByValue(objLaneSettings.Make) != null)
                ddlMake.SelectedValue = objLaneSettings.Make;


            if (ddlMake.SelectedIndex != -1)
                //BindModel(Convert.ToInt64(ddlMake.SelectedValue));

            //if (ddlModel.Items.FindByValue(objLaneSettings.Model) != null)
                //ddlModel.SelectedValue = objLaneSettings.Model;

            txtLaneNo.Text= objLaneSettings.Lane;

            ddlLaneType.SelectedValue = objLaneSettings.LaneType;

            gvInventoryList.PageIndex = objLaneSettings.gridPagePosition;

        }

        #endregion

        #region "Making dynamically where clause"
        /// <summary>
        /// Creating Daynamic where clause
        /// </summary>
        /// <returns></returns>
        public String Where()
        {
            String Where = string.Empty;


            if (ddlYear.SelectedItem.Text != "")
            {
                Where = "  vwLaneAssignments.[Year] = " + Convert.ToInt32(ddlYear.SelectedItem.Text) + " ";
            }
            if (ddlMake.SelectedItem.Text != "")
            {
                if (Where != string.Empty)
                {
                    Where = Where + " AND vwLaneAssignments.MakeID = " + Convert.ToInt32(ddlMake.SelectedValue) + " ";
                }
                else
                {
                    Where = " vwLaneAssignments.MakeID = " + Convert.ToInt32(ddlMake.SelectedValue) + " ";

                }
            }
          /*
            if (ddlModel.SelectedItem.Text != "")
            {
                if (Where != string.Empty)
                {
                    Where = Where + " AND vwLaneAssignments.ModelID = " + Convert.ToInt32(ddlModel.SelectedValue) + " ";
                }
                else
                {
                    Where = "  vwLaneAssignments.ModelID = " + Convert.ToInt32(ddlModel.SelectedValue) + " ";
                }
            }
          */
            if (ddlLaneType.SelectedItem.Text != "")
            {
                switch (ddlLaneType.SelectedItem.Text)
                {
                    case "Regular":
                        if (Where != string.Empty)
                        {
                            if (txtLaneNo.Text.Trim().Length > 0)
                            {

                                Where = Where + "  AND   vwLaneAssignments.RegularLane LIKE '%" + txtLaneNo.Text.ToString().Trim() + "%' ";

                            }
                        }
                        else
                        {
                            if (txtLaneNo.Text.Trim().Length > 0)
                            {

                                Where = "  vwLaneAssignments.RegularLane LIKE '%" + txtLaneNo.Text.ToString().Trim() + "%' ";
                            }
                        }
                        break;
                    case "Exotic":
                        if (Where != string.Empty)
                        {
                            if (txtLaneNo.Text.Trim().Length > 0)
                            {
                                Where = Where + "  AND   vwLaneAssignments.ExoticLane LIKE '%" + txtLaneNo.Text.ToString().Trim() + "%' ";
                            }
                        }
                        else
                        {
                            if (txtLaneNo.Text.Trim().Length > 0)
                            {
                                Where = "    vwLaneAssignments.ExoticLane LIKE '%" + txtLaneNo.Text.ToString().Trim() + "%' ";
                            }
                        }
                        break;
                    case "Virtual":
                        if (Where != string.Empty)
                        {
                            if (txtLaneNo.Text.Trim().Length > 0)
                            {
                                Where = Where + "  AND   vwLaneAssignments.VirtualLane LIKE '%" + txtLaneNo.Text.ToString().Trim() + "%' ";
                            }
                        }
                        else
                        {
                            if (txtLaneNo.Text.Trim().Length > 0)
                            {
                                Where = "     vwLaneAssignments.VirtualLane LIKE '%" + txtLaneNo.Text.ToString().Trim() + "%' ";
                            }
                        }
                        break;
                    case "Online":
                        if (Where != string.Empty)
                        {
                            if (txtLaneNo.Text.Trim().Length > 0)
                            {
                                Where = Where + "  AND   vwLaneAssignments.OnlineLane LIKE '%" + txtLaneNo.Text.ToString().Trim() + "%' ";
                            }
                        }
                        else
                        {
                            if (txtLaneNo.Text.Trim().Length > 0)
                            {
                                Where = "     vwLaneAssignments.OnlineLane LIKE '%" + txtLaneNo.Text.ToString().Trim() + "%' ";
                            }
                        }
                        break;
                }
            }
            if (Where != "")
            {
                Where = "Where  " + Where;
            }
            return Where;

        }

        #endregion

        #region [ Update Exotic Pattern ]
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                LaneAssignmentBAL ObjLane = new LaneAssignmentBAL();
                ObjLane.UpdateExoticLanePattern(Convert.ToString(txtExoticPattern.Text));
                #region [ Modal Pouup Commented HTML code, Its taking much time to display]
                //pnlExoticPatternUpdated.Visible = true;
                //upExoticPatternUpdated.Update();
                //mdpupExoticPatternUpdated.Show();
                #endregion 
                //Threading also not giving exact out put
                //txtExoticPattern.BackColor = System.Drawing.Color.Yellow;
                //thread.Thread.Sleep(10000);
                //txtExoticPattern.BackColor = System.Drawing.Color.White;
            }
            catch (Exception ex)
            {
                //TO DO
            }
        }
        #endregion

        #region [ To load Exotic Pattern ]

        /// <summary>
        /// To load Exotic Pattern
        /// </summary>
        void LoadExoticPattern()
        {
            LaneAssignmentBAL ObjLane = new LaneAssignmentBAL();
            List<string> lstExoticLanePattern = null;
            lstExoticLanePattern = ObjLane.SelectExoticLanePattern();
            if (lstExoticLanePattern.Count > 0)
            {
                txtExoticPattern.Text = lstExoticLanePattern[0].ToString();

            }
        }
        #endregion

        #region [gvInventoryList SelectedIndexChanging]
        protected void gvInventoryList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int rowIndex = e.NewSelectedIndex;//gvInventoryList.SelectedIndex;

            string InventoryId = gvInventoryList.DataKeys[rowIndex].Value.ToString();
            ViewState["invetoryid"] = InventoryId;
            //  update the contents in the detail panel
            this.upShowHistory.Update();
            //  show the modal popup

            this.mdpShowHistory.Show();
            BindGrid(Convert.ToInt64(InventoryId));
        }
        #endregion

        #region [gvLaneHistory PageIndexChanging]
        protected void gvLaneHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gvLaneHistory.PageIndex = e.NewPageIndex;
            BindGrid(Convert.ToInt64(ViewState["invetoryid"]));
        }
        #endregion

        #region [ gvInventoryList RowDataBound ]
        protected void gvInventoryList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                #region [ Get the Object for Security applying]
                HyperLink hplViewCarPage = (HyperLink)e.Row.FindControl("hplViewCarPage");
                ImageButton btnHistory = (ImageButton)e.Row.FindControl("btnHistory");
                //Label gvlblIsExotic = (Label)e.Row.FindControl("gvlblIsExotic");
                Label gvlblRegularLane = (Label)e.Row.FindControl("gvlblRegularLane");
                Label gvlblExoticLane = (Label)e.Row.FindControl("gvlblExoticLane");
                Label gvlblOnlineLane = (Label)e.Row.FindControl("gvlblOnlineLane");
                Label gvlblVirtualLane = (Label)e.Row.FindControl("gvlblVirtualLane");
                Label gvlblMarketPrice = (Label)e.Row.FindControl("gvlblMarketPrice");
                Label gvlblVIN = (Label)e.Row.FindControl("gvlblVIN");
                Label gvlblNotes = (Label)e.Row.FindControl("gvlblNotes");
                Label gvlblGRP = (Label)e.Row.FindControl("gvlblGRP");
                #endregion

                
                int pID = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "inventoryid"));
                /*
                  string SQLStatement = "Select Inventory.InventoryId from Inventory INNER JOIN LaneHistory ON LaneHistory.InventoryId = Inventory.InventoryId Where Inventory.InventoryId=" + pID;
                  DataTable dtCount = METAOPTION.BAL.Common.LookupTableRecords(SQLStatement);
                  if (dtCount.Rows.Count ==0)
                   {
                       e.Row.Cells[2].ForeColor = System.Drawing.Color.DarkBlue;
                   }
              
                 */

                Color col = Color.FromName(((System.Data.DataRowView)(e.Row.DataItem)).Row["RegularNumberForeColor"].ToString());
                e.Row.Cells[2].ForeColor = col;
                
                Color colMake = Color.FromName(((System.Data.DataRowView)(e.Row.DataItem)).Row["MakeColor"].ToString());
                e.Row.Cells[7].ForeColor = colMake;

                ImageButton imgbtn = (ImageButton)e.Row.FindControl("btnHistory");
                //string jScript = string.Format("popUp('{0}')", Convert.ToInt64());
                imgbtn.Attributes.Add("onClick", "popUp('LaneHistory.aspx?inventoryid=" + pID.ToString() + "');return false;");
                ///////////
                ////Updated By Anwarul; Date: 10-13-2009
                //try//plz Do not remove 
                //{
                //    Color CurrentRowUpdated = Color.FromName(((System.Data.DataRowView)(e.Row.DataItem)).Row["CurrentRowUpdatedForeColor"].ToString());
                //    e.Row.BackColor = CurrentRowUpdated;
                //}
                //catch (Exception ex) { }

                // For Groups header anme
                Label lblGname1 = (Label)e.Row.FindControl("gvlblGRP1");
                Label lblGname2 = (Label)e.Row.FindControl("gvlblGRP2");
                Label lblGname3 = (Label)e.Row.FindControl("gvlblGRP3");
                Label lblGname4 = (Label)e.Row.FindControl("gvlblGRP4");
                Label lblGname5 = (Label)e.Row.FindControl("gvlblGRP5");

                DataTable dt = new DataTable();
                dt = ToBindGroups();
                Label GRPName = (Label)e.Row.FindControl("gvlblGRP");
                CheckBox chkGp1 = (CheckBox)e.Row.FindControl("gvchkGRP1");
                CheckBox chkGp2 = (CheckBox)e.Row.FindControl("gvchkGRP2");
                CheckBox chkGp3 = (CheckBox)e.Row.FindControl("gvchkGRP3");
                CheckBox chkGp4 = (CheckBox)e.Row.FindControl("gvchkGRP4");
                CheckBox chkGp5 = (CheckBox)e.Row.FindControl("gvchkGRP5");
                String gpAbb = String.Empty;
                //gvchkIsExotic,gvlblIsExotic

                //CheckBox gvchkIsExotic = (CheckBox)e.Row.FindControl("gvchkIsExotic");

                HtmlInputCheckBox gvchkIsExotic = (HtmlInputCheckBox)e.Row.FindControl("gvchkIsExotic");

                Label gvlblIsExotic = (Label)e.Row.FindControl("gvlblIsExotic");
                if (gvlblIsExotic != null)
                {
                    if (gvlblIsExotic.Text == "Y")
                    {
                        gvchkIsExotic.Checked = true;
                    }
                    else
                    {
                        gvchkIsExotic.Checked = false;
                    }
                }
                if (GRPName != null)
                {
                    gpAbb = GRPName.Text;
                    string[] split = gpAbb.Split(',');
                    int gpIndex = 0;
                    try
                    {
                        foreach (string item in split)
                        {
                            gpIndex += 1;
                            int i = 0;
                            if (dt.Rows.Count > 0)
                            {
                                for (i = 0; i < dt.Rows.Count; i++)
                                {
                                    string gp = dt.Rows[i]["GroupAbbreviation"].ToString();
                                    if (gp.Trim().ToLower() == item.Trim().ToLower())
                                    {
                                        switch (i + 1)
                                        {
                                            case 1:
                                                chkGp1.Checked = true;
                                                break;
                                            case 2:
                                                chkGp2.Checked = true;
                                                break;
                                            case 3:
                                                chkGp3.Checked = true;
                                                break;
                                            case 4:
                                                chkGp4.Checked = true;
                                                break;
                                            case 5:
                                                chkGp5.Checked = true;
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                        #region [ Managing security on grid columns]
                        List<String> Permissions = (List<String>)ViewState["PageSecurity"];
                        if (!(Permissions.Contains("LANEASSIGNMENT.EDIT")))
                        {

                            lnk10RecordsforEmailAnn.Enabled = false;
                            lnk10RecordsforEmailAnn.ToolTip = "Permission Protected";
                            lnk100RecordsForAnnounce_Email.Enabled = false;
                            lnk100RecordsForAnnounce_Email.ToolTip = "Permission Protected";
                            lnk20RecordsModified.Enabled = false;
                            lnk20RecordsModified.ToolTip = "Permission Protected";
                            btnUpdate.Enabled = false;
                            btnUpdate.ToolTip = "Permission Protected";
                            gvlblIsExotic.Enabled = false;
                            gvlblIsExotic.ToolTip = "Permission Protected";
                            gvlblIsExotic.Style.Add("text-decoration", "none");
                            gvlblRegularLane.Enabled = false;
                            gvlblRegularLane.ToolTip = "Permission Protected";
                            gvlblRegularLane.Style.Add("text-decoration", "none");
                            gvlblExoticLane.Enabled = false;
                            gvlblExoticLane.ToolTip = "Permission Protected";
                            gvlblExoticLane.Style.Add("text-decoration", "none");
                            gvlblOnlineLane.Enabled = false;
                            gvlblOnlineLane.ToolTip = "Permission Protected";
                            gvlblOnlineLane.Style.Add("text-decoration", "none");
                            gvlblVirtualLane.Enabled = false;
                            gvlblVirtualLane.ToolTip = "Permission Protected";
                            gvlblVirtualLane.Style.Add("text-decoration", "none");

                            gvlblMarketPrice.Enabled = false;
                            gvlblMarketPrice.ToolTip = "Permission Protected";
                            gvlblMarketPrice.Style.Add("text-decoration", "none");

                            //gvlblMileage.Enabled = false;
                            //gvlblMileage.ToolTip = "Permission Protected";
                            //gvlblMileage.Style.Add("text-decoration", "none");

                            gvlblVIN.Enabled = false;
                            gvlblVIN.ToolTip = "Permission Protected";
                            gvlblVIN.Style.Add("text-decoration", "none");
                            gvlblNotes.Enabled = false;
                            gvlblNotes.Style.Add("text-decoration", "none");
                            gvlblGRP.Attributes.Add("onclick", "return false;");
                            gvlblGRP.Enabled = false;
                            gvlblGRP.ToolTip = "Permission Protected";
                            gvlblGRP.Style.Add("text-decoration", "none");
                            btnHistory.Attributes.Add("onclick", "return false;");
                            btnHistory.ToolTip = "Permission Protected";
                        }
                        
                        #endregion


                    }
                    catch
                    {
                    }
                }



                #region "To Binding Lane Groups for Edit section"

                if (dt.Rows.Count > 0)
                {
                    if (lblGname1 != null)
                    {
                        switch (dt.Rows.Count)
                        {
                            case 1:
                                lblGname1.Text = dt.Rows[0]["GroupName"].ToString() + " (" + dt.Rows[0]["GroupAbbreviation"].ToString() + ")";
                                break;
                            case 2:
                                lblGname1.Text = dt.Rows[0]["GroupName"].ToString() + " (" + dt.Rows[0]["GroupAbbreviation"].ToString() + ")";
                                lblGname2.Text = dt.Rows[1]["GroupName"].ToString() + " (" + dt.Rows[1]["GroupAbbreviation"].ToString() + ")";
                                break;
                            case 3:
                                lblGname1.Text = dt.Rows[0]["GroupName"].ToString() + " (" + dt.Rows[0]["GroupAbbreviation"].ToString() + ")";
                                lblGname2.Text = dt.Rows[1]["GroupName"].ToString() + " (" + dt.Rows[1]["GroupAbbreviation"].ToString() + ")";
                                lblGname3.Text = dt.Rows[2]["GroupName"].ToString() + " (" + dt.Rows[2]["GroupAbbreviation"].ToString() + ")";
                                break;
                            case 4:
                                lblGname1.Text = dt.Rows[0]["GroupName"].ToString() + " (" + dt.Rows[0]["GroupAbbreviation"].ToString() + ")";
                                lblGname2.Text = dt.Rows[1]["GroupName"].ToString() + " (" + dt.Rows[1]["GroupAbbreviation"].ToString() + ")";
                                lblGname3.Text = dt.Rows[2]["GroupName"].ToString() + " (" + dt.Rows[2]["GroupAbbreviation"].ToString() + ")";
                                lblGname4.Text = dt.Rows[3]["GroupName"].ToString() + " (" + dt.Rows[3]["GroupAbbreviation"].ToString() + ")";
                                break;
                            case 5:
                                lblGname1.Text = dt.Rows[0]["GroupName"].ToString() + " (" + dt.Rows[0]["GroupAbbreviation"].ToString() + ")";
                                lblGname2.Text = dt.Rows[1]["GroupName"].ToString() + " (" + dt.Rows[1]["GroupAbbreviation"].ToString() + ")";
                                lblGname3.Text = dt.Rows[2]["GroupName"].ToString() + " (" + dt.Rows[2]["GroupAbbreviation"].ToString() + ")";
                                lblGname4.Text = dt.Rows[3]["GroupName"].ToString() + " (" + dt.Rows[3]["GroupAbbreviation"].ToString() + ")";
                                lblGname5.Text = dt.Rows[4]["GroupName"].ToString() + " (" + dt.Rows[4]["GroupAbbreviation"].ToString() + ")";

                                break;
                        }
                    }

                }
                #endregion
            }
        }
        #endregion

        #region [ Page render ]
        protected override void Render(HtmlTextWriter writer)
        {
            
                foreach (GridViewRow r in gvInventoryList.Rows)
                {
                    
                        if (r.RowType == DataControlRowType.DataRow)
                        {
                            Page.ClientScript.RegisterForEventValidation
                                    (r.UniqueID + "$ctl00");
                        }
                    
                }
                base.Render(writer);
                AjaxControlToolkit.ToolkitScriptManager ajax = (AjaxControlToolkit.ToolkitScriptManager)this.Page.Master.FindControl("ToolkitScriptManager1");

                if (ajax != null)

                    ajax.AsyncPostBackTimeout = 600;


            
        }
        #endregion

        #region [Bind Groups]
        public DataTable ToBindGroups()
        {
            LaneGroupBAL objLG = new LaneGroupBAL();
            DataTable dt = objLG.GetLaneGroups();
            return dt;
        }
        #endregion

        #region "To Get 20 modified recored by this sessioin"
        /// <summary>
        /// to get 20 records modified by last sessioni
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnk20RecordsModified_Click(object sender, EventArgs e)
        {
            
                LaneAssignmentBAL ObjLane = new LaneAssignmentBAL();
                gvInventoryList.DataSource = ObjLane.Select20Records(Constant.UserId);
                gvInventoryList.DataBind();
                lnk20RecordsModified.Text = "(" + gvInventoryList.Rows.Count.ToString() + ") Records modified in this session";
                ModifiedRecordCount(Constant.UserId);
                Get100_Announcement_EmailRecordsCount(Constant.UserId);
                Get10_Announcement_EmailRecordsCountSinceLastLogin(Constant.UserId);
               
            
        }

        #endregion

        #region "To get 10 records for email & announcement"

        protected void lnk10RecordsforEmailAnn_Click(object sender, EventArgs e)
        {
            LaneAssignmentBAL ObjLane = new LaneAssignmentBAL();
            ddlYear.SelectedIndex = 0;
            ddlMake.SelectedIndex = 0;
            //ddlModel.SelectedIndex = 0;
            ddlLaneType.SelectedIndex = 0;
            txtLaneNo.Text = string.Empty;
            gvInventoryList.DataSource = ObjLane.Select10RecordSinceLastLogin(Constant.UserId);
            gvInventoryList.DataBind();


            lnk10RecordsforEmailAnn.Text = "(" + gvInventoryList.Rows.Count + ") Records for announcement and email last login";
            ModifiedRecordCount(Constant.UserId);
            Get100_Announcement_EmailRecordsCount(Constant.UserId);
            Get10_Announcement_EmailRecordsCountSinceLastLogin(Constant.UserId);
            
        }
        #endregion

        #region " To Get upto 100 records for announcement & email since last login"



        protected void lnk100RecordsForAnnounce_Email_Click(object sender, EventArgs e)
        {
            
                LaneAssignmentBAL ObjLane = new LaneAssignmentBAL();
                ddlYear.SelectedIndex = 0;
                ddlMake.SelectedIndex = 0;
                //ddlModel.SelectedIndex = 0;
                ddlLaneType.SelectedIndex = 0;
                txtLaneNo.Text = string.Empty;
                //Zero parameter me return all rows
                gvInventoryList.DataSource = ObjLane.GetLaneAnnouncementAndEmail(Constant.UserId);
                gvInventoryList.DataBind();
                lnk100RecordsForAnnounce_Email.Text = "(" + gvInventoryList.Rows.Count.ToString() + ") Records for announcement and email";
                ModifiedRecordCount(Constant.UserId);
                Get100_Announcement_EmailRecordsCount(Constant.UserId);
                Get10_Announcement_EmailRecordsCountSinceLastLogin(Constant.UserId);
                
           


        }

        public void Get100_Announcement_EmailRecordsCount(Int64 userId)
        {
            LaneAssignmentBAL ObjLane = new LaneAssignmentBAL(); 
            int iCount = 0;
            iCount = ObjLane.SelectLaneAnnouncementAndEmail(userId);
            lnk100RecordsForAnnounce_Email.Text = "(" + iCount.ToString() + ") Records for announcement and email";
            
        }
        public void Get10_Announcement_EmailRecordsCountSinceLastLogin(Int64 userId)
        {
            LaneAssignmentBAL ObjLane = new LaneAssignmentBAL();
            int iCount = 0;
            iCount = ObjLane.Ge10RecordsCount(userId);
            lnk10RecordsforEmailAnn.Text = "(" + iCount.ToString() + ") Records for announcement and email last login";
            
        }
        #endregion

        #region "View All Inventory"
        protected void lnkViewAllInventory_Click(object sender, EventArgs e)
        {
            
                LaneAssignmentBAL ObjLane = new LaneAssignmentBAL();
                ddlYear.SelectedIndex = 0;
                ddlMake.SelectedIndex = 0;
                //ddlModel.SelectedIndex = 0;
                ddlLaneType.SelectedIndex = 0;
                txtLaneNo.Text = string.Empty;
                chkShowOnline.Checked = false;
                chkShowExotic.Checked = false;
                gvInventoryList.DataSource = ObjLane.GetInventoryFilterList();
                gvInventoryList.DataBind();
                
            
        }
        #endregion

        #region "[ Current session counting for modified records ]"
        void ModifiedRecordCount(Int64 UserID)
        {
            LaneAssignmentBAL ObjLane = new LaneAssignmentBAL();
            int iCount = 0;
            iCount = ObjLane.Ge20RecordsCount(UserID);
            lnk20RecordsModified.Text = "(" + iCount.ToString() + ") Records modified in this session";

        }
        #endregion

        #region "To bind all employee name and email"
        /// <summary>
        /// Bind All Employee Name And Email
        /// </summary>
        void BindAllEmployeeNameAndEmail()
        {
            LaneAssignmentBAL ObjLane = new LaneAssignmentBAL();
            lstEmployeeList.Items.Clear();
            lstEmployeeList.DataValueField = "Email1";
            lstEmployeeList.DataTextField = "FullName";
            lstEmployeeList.DataSource = ObjLane.SelectAllEmployeeEmailLists(Constant.OrgID);
            lstEmployeeList.DataBind();
        }
        #endregion

        #region "Add new Announcement"
        /// <summary>
        /// Create new announcement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateAnnouncement_Click(object sender, EventArgs e)
        {
            ///Object instantiation part
            ///
            
            LaneAssignmentBAL ObjLane = new LaneAssignmentBAL();
            Announcement objAnnounce = new Announcement();
            AnnouncementRelationship objAnnounceRelship = new AnnouncementRelationship();
            //ADDAnnouncementRelationshipInsertBAL objAddRelShip = new ADDAnnouncementRelationshipInsertBAL();
            LaneAssignmentBAL objLaneHistory = new LaneAssignmentBAL();
            //Local Variable declarition
            long AnnouncementId = 0;
            long InventoryId = 0;
            int iCount = 0;
            int iFirstCount = 0;
            ///Page validating
            if (Page.IsValid)
            {
                //Assgning new value for new Announcement creation
                objAnnounce.AnnouncementTitle = txtTitle.Text;
                objAnnounce.Description = txtDescription.Text;
                objAnnounce.AddedBy = Constant.UserId;
                objAnnounce.AnnouncementTypeID = 2;
                AnnouncementId = ObjLane.AddAnnouncement(objAnnounce);
                //Iteration for counting announcement & email pending records
                //Description : To get each Inventoryid for ADDING Announcement Relationship for ANNOUNCEMENT
                for (int i = 0; i < gvInventoryList.Rows.Count; i++)
                {
                    iFirstCount += 1;
                    iCount = 0;
                    foreach (GridViewRow rowItem in gvInventoryList.Rows)
                    {
                        iCount += 1;
                        //Geting Inventory id
                        InventoryId = Convert.ToInt64(gvInventoryList.DataKeys[rowItem.RowIndex].Value);
                        if (iCount == iFirstCount)
                            break;
                    }
                    ///Here goes adding feature for Announcement Relationship
                    ///
                    //Update lane hstory
                    objLaneHistory.UpdateIsAnnouncementCreated(InventoryId);
                    objAnnounceRelship.AnnouncementId = AnnouncementId;
                    objAnnounceRelship.EntityId = InventoryId;
                    objAnnounceRelship.AddedBy = Constant.UserId;//User id need to put here
                    long ID = ObjLane.AddAnnouncementRelationship(objAnnounceRelship);
                }
            }

            txtTitle.Text = string.Empty;
            txtDescription.Text = string.Empty;
            chkSendEmail.Checked = false;


            Response.Redirect("AnnouncementDetails.aspx?AnnouncementId=" + AnnouncementId);
        }
        #endregion

        #region "Send Email to employee"

        protected void chkSendEmail_CheckedChanged(object sender, EventArgs e)
        {
            LaneAssignmentBAL ObjLane = new LaneAssignmentBAL(); 
            if (chkSendEmail.Checked == true)
            {
                List<string> lstInnerHTMLCode = null;
                lstInnerHTMLCode = ObjLane.SelectInnerHTML();
                if (lstInnerHTMLCode.Count > 0)
                {
                    GridviewInnerHTML = lstInnerHTMLCode[0].ToString();
                }
            }
            //Put here from Email Address
            string EmailBody = txtDescription.Text + "<br><hr><br>" + GridviewInnerHTML;
            string subject = txtTitle.Text;
            if (chkSendEmail.Checked == true)
            {
                //Checking here to collect all employee emails or only selected employee emails
                if (ddlSendEmailOption.SelectedItem.Text.Trim().ToLower() == "send to all")
                {
                    foreach (ListItem item in lstEmployeeList.Items)
                    {
                        if (item.Value.ToString() != "")
                            if (System.Text.RegularExpressions.Regex.IsMatch(item.Value.ToString(), @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
                            {
                                try
                                {
                                    METAOPTION.EmailMessage.sendEmail(EmailBody, subject, item.Value.ToString());
                                }
                                catch (Exception ex)
                                {
                                    //TO DO
                                }
                            }
                    }
                }
                else
                {
                    //Get email only selected employee
                    foreach (ListItem item in lstEmployeeList.Items)
                    {
                        if (item.Selected == true)
                        {
                            if (item.Value.ToString() != "")
                                if (System.Text.RegularExpressions.Regex.IsMatch(item.Value.ToString(), @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
                                {
                                    try
                                    {
                                        METAOPTION.EmailMessage.sendEmail(EmailBody, subject, item.Value.ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        //TO DO
                                    }
                                }

                        }
                    }
                }
            }
        }

        #endregion

       
   
        /// <summary>
        /// Handle Click button of Link button to open View Lane Report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkLaneReport_Click(object sender, EventArgs e)
        {
            //Specify Report Parameters required to run Lane Report(bydefault pass all -1("ALL" Match)
            ReportParameter[] parameters = new ReportParameter[9];
            parameters[0] = new ReportParameter("year", "-1");
            parameters[1] = new ReportParameter("make", "-1");
            parameters[2] = new ReportParameter("model", "-1");
            parameters[3] = new ReportParameter("body", "-1");
            parameters[4] = new ReportParameter("lanetype", "-1");
            parameters[5] = new ReportParameter("entitytypeid", "-1");
            parameters[6] = new ReportParameter("comebackstatus", "-1");
            parameters[7] = new ReportParameter("UserId", Constant.UserId.ToString());
            parameters[8] = new ReportParameter("systemId", Session["SystemID"].ToString());

            //Set Report Parameters & ReportName
            ReportParameters.Parameters = parameters;
            ReportParameters.ReportName = "/Hollenshead/LaneReport";
            Response.Redirect("~/VMSReports/Reports.aspx?ReturnURL=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
      
        }

        protected void gvInventoryList_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt=null; int itemp = 0;
                if (ViewState["DataTable"] != null || ViewState["DataTable"].ToString() != string.Empty)
                {
                    dt = (DataTable)ViewState["DataTable"];
                }
                if (dt != null)
                {
                     itemp = e.Row.RowIndex;
                    int PageSize = Convert.ToInt32(ddlViewRecords.SelectedValue);
                    if (PageSize > dt.Rows.Count)
                    {
                        gvInventoryList.PageSize = dt.Rows.Count;
                        itemp = itemp + 1;
                    }
                    else
                        itemp = itemp + 2;
                }
                e.Row.Attributes.Add("onclick", "onGridViewRowSelected('" + itemp.ToString() + "')");
            }
           
        }
    }

}
















