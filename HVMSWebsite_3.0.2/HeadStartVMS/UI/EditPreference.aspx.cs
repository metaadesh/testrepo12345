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

namespace METAOPTION.UI
{
    public partial class EditPreference : System.Web.UI.Page
    {
        #region[Create Objects]
        Int32 totalPreRows = 0;
        Int32 totalMPreRow = 0;
        Int32 totalEPreRow = 0;
        CommonBAL objCommonBAL = new CommonBAL();
        Common objCommon = new Common();
        MasterBAL objMasterBAL = new MasterBAL();
       // DealerCustomerBAL objDealerCustomerBAL = new DealerCustomerBAL();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            #region[Load Event]
            SaveControlState();
            CreatePreferenceTable();
            CreateMPTable();
            CreateEPTable();
            if (!IsPostBack)
            {
                fvPreference.DataSource = objPrefSetting;
                fvPreference.DataBind();
            }
            #endregion
        }
        #region[Save Control State]
        protected override object SaveControlState()
        {
            SavePrefControlDetails();
            SaveMPrefControlState();
            SaveEPrefControlState();
            return base.SaveControlState();
        }

        protected void SavePrefControlDetails()
        {
            DataTable dtPreference = (DataTable)Session["EditPreference"];
            for (int index = 0; index < grdPreference.Rows.Count; index++)
            { 
                DropDownList ddlMinYear = (DropDownList)grdPreference.Rows[index].FindControl("ddlMinYear");
                DropDownList ddlMaxYear = (DropDownList)grdPreference.Rows[index].FindControl("ddlMaxYear");
                DropDownList ddlMake = (DropDownList)grdPreference.Rows[index].FindControl("ddlMake");
                DropDownList ddlModel = (DropDownList)grdPreference.Rows[index].FindControl("ddlModel");
                TextBox txtMinMileage = (TextBox)grdPreference.Rows[index].FindControl("txtMinMileage");
                TextBox txtMaxMileage = (TextBox)grdPreference.Rows[index].FindControl("txtMaxMileage");
                TextBox txtMinPrice = (TextBox)grdPreference.Rows[index].FindControl("txtMinPrice");
                TextBox txtMaxPrice = (TextBox)grdPreference.Rows[index].FindControl("txtMaxPrice");

                dtPreference.Rows[index]["IsEnabled"] = ((CheckBox)grdPreference.Rows[index].FindControl("chkEnable")).Checked;
                if (!string.IsNullOrEmpty(ddlMinYear.SelectedValue))
                dtPreference.Rows[index]["yearsFrom"] = ddlMinYear.SelectedValue;

                if (!string.IsNullOrEmpty(ddlMaxYear.SelectedValue))
                dtPreference.Rows[index]["yearsTo"] = ddlMaxYear.SelectedValue;

                if (!string.IsNullOrEmpty(ddlMake.SelectedValue))
                    dtPreference.Rows[index]["MakeId"] = Convert.ToInt32(ddlMake.SelectedValue);

                if (!string.IsNullOrEmpty(ddlModel.SelectedValue))
                dtPreference.Rows[index]["ModelId"] = Convert.ToInt32(ddlModel.SelectedValue);

                if (!string.IsNullOrEmpty(txtMinMileage.Text))
                    dtPreference.Rows[index]["MileageMin"] = Convert.ToInt32(txtMinMileage.Text.Trim());

                if (!string.IsNullOrEmpty(txtMaxMileage.Text))
                dtPreference.Rows[index]["MileageMax"] = Convert.ToInt32(txtMaxMileage.Text.Trim());
                if (!string.IsNullOrEmpty(txtMinPrice.Text))
                    dtPreference.Rows[index]["PriceMin"] = Convert.ToDecimal(txtMinPrice.Text.Trim());
                if (!string.IsNullOrEmpty(txtMaxPrice.Text))
                    dtPreference.Rows[index]["PriceMax"] = Convert.ToDecimal(txtMaxPrice.Text.Trim());
            }
            Session["EditPreference"] = dtPreference;
        }

        protected void SaveMPrefControlState()
        {
            DataTable dtMPref = (DataTable)Session["MPreference"];
            for (int index = 0; index < grdViewMobile.Rows.Count; index++)
            {
                dtMPref.Rows[index]["IsEnable"] = ((CheckBox)grdViewMobile.Rows[index].FindControl("chkEnable")).Checked;
                dtMPref.Rows[index]["MobileNo"] = ((TextBox)grdViewMobile.Rows[index].FindControl("txtMobile")).Text.Trim();
            }
            Session["MPreference"] = dtMPref;
        }

        protected void SaveEPrefControlState()
        {
            DataTable dtEPref = (DataTable)Session["EPreference"];
            for (int index = 0; index < grdViewEmail.Rows.Count; index++)
            {
                dtEPref.Rows[index]["IsEnable"] = ((CheckBox)grdViewEmail.Rows[index].FindControl("chkEnable")).Checked;
                dtEPref.Rows[index]["Email"] = ((TextBox)grdViewEmail.Rows[index].FindControl("txtEmail")).Text.Trim();
            }
            Session["EPreference"] = dtEPref;
        }
        #endregion

        #region [Create DataTable for Preference Section]
        private void CreatePreferenceTable()
        {
           
            DataTable dtPref = new DataTable("Preference");
            if (Session["EditPreference"] != null)
            {
                dtPref = (DataTable)Session["EditPreference"];
            }
            else
            {
                // Add columns
                dtPref.Columns.Add("SNo", System.Type.GetType("System.Int32"));
                dtPref.Columns.Add("IsEnabled", System.Type.GetType("System.Boolean"));
                dtPref.Columns.Add("yearsFrom", System.Type.GetType("System.String"));
                dtPref.Columns.Add("yearsTo", System.Type.GetType("System.String"));
                dtPref.Columns.Add("MakeId", System.Type.GetType("System.Int32"));
                dtPref.Columns.Add("ModelId", System.Type.GetType("System.Int32"));
                dtPref.Columns.Add("MileageMin", System.Type.GetType("System.Int32"));
                dtPref.Columns.Add("MileageMax", System.Type.GetType("System.Int32"));
                dtPref.Columns.Add("PriceMin", System.Type.GetType("System.Decimal"));
                dtPref.Columns.Add("PriceMax", System.Type.GetType("System.Decimal"));

                //Check if dealer has preference, Retreive dealer preference from database
                DataTable dtFromDB = BAL.DealerCustomerBAL.GetDealerPreference(Convert.ToInt64(Request["EntityId"]));

                if (dtFromDB.Rows.Count != 0)
                {
                    for (int index = 0; index < dtFromDB.Rows.Count; index++)
                    {
                        //Add rows in table
                        DataRow dtrow = dtPref.NewRow();
                        dtrow["SNo"] = index + 1;
                        dtrow["IsEnabled"] = dtFromDB.Rows[index]["IsEnabled"];
                        dtrow["yearsFrom"] = dtFromDB.Rows[index]["yearsFrom"];
                        dtrow["yearsTo"] = dtFromDB.Rows[index]["yearsTo"];
                        dtrow["MakeId"] = dtFromDB.Rows[index]["MakeId"];
                        dtrow["ModelId"] = dtFromDB.Rows[index]["ModelId"];
                        dtrow["MileageMin"] = dtFromDB.Rows[index]["MileageMin"];
                        dtrow["MileageMax"] = dtFromDB.Rows[index]["MileageMax"];
                        dtrow["PriceMin"] = dtFromDB.Rows[index]["PriceMin"];
                        dtrow["PriceMax"] = dtFromDB.Rows[index]["PriceMax"];
                        // add this row in table
                        dtPref.Rows.Add(dtrow);
                    }
                }
                else
                {
                    // Add Default Row
                    DataRow newPreference = dtPref.NewRow();
                    newPreference["SNo"] = 1;
                    dtPref.Rows.Add(newPreference);
                }
            }

           

            // Total Rows into table
            totalPreRows = dtPref.Rows.Count;

             // Store current table to session
            Session["EditPreference"] = dtPref;
            // Bind to grid
            grdPreference.DataSource = dtPref;

            grdPreference.DataBind();

           
        }
        #endregion

        #region [Create DataTable for Mobile Preference Section]
        private void CreateMPTable()
        {
            DataTable dtMPref = new DataTable("MPreference");
            if (Session["MPreference"] != null)
            {
                dtMPref = (DataTable)Session["MPreference"];
            }
            else
            {
                // Retreive dealer Mobile preference from database
               DataTable  dtFromDB = BAL.DealerCustomerBAL.GetDealerMobilePreference(Convert.ToInt64(Request["EntityId"]));
               if (dtFromDB.Rows.Count != 0)
                {
                    // Add column
                    dtMPref.Columns.Add("SNo", System.Type.GetType("System.Int32"));
                    dtMPref.Columns.Add("IsEnable", System.Type.GetType("System.Boolean"));
                    dtMPref.Columns.Add("MobileNo", System.Type.GetType("System.String"));
                    for (int index = 0; index < dtFromDB.Rows.Count; index++)
                    {
                        // Add Row
                        DataRow dtrow = dtMPref.NewRow();
                        dtrow["SNo"] = index + 1;
                        dtrow["IsEnable"] = dtFromDB.Rows[index]["IsEnable"];
                        dtrow["MobileNo"] = dtFromDB.Rows[index]["MobileNo"];
                        dtMPref.Rows.Add(dtrow);
                    }
                }
                else
                {
                    // Add columns
                    dtMPref.Columns.Add("SNo", System.Type.GetType("System.Int32"));
                    dtMPref.Columns.Add("IsEnable", System.Type.GetType("System.Boolean"));
                    dtMPref.Columns.Add("MobileNo", System.Type.GetType("System.String"));
                    
                    // Add Default Row
                    DataRow newPreference = dtMPref.NewRow();
                    newPreference["SNo"] = 1;
                    dtMPref.Rows.Add(newPreference);
                }
            }

            // Total Rows into table
            totalMPreRow = dtMPref.Rows.Count;

            // Store current table to session
            Session["MPreference"] = dtMPref;
            // Bind to grid
            grdViewMobile.DataSource = dtMPref;

            grdViewMobile.DataBind();

           
        }
        #endregion

        #region [Create DataTable for Email Preference Section]
        private void CreateEPTable()
        {
            DataTable dtEPref = new DataTable("EPreference");
            if (Session["EPreference"] != null)
            {
                dtEPref = (DataTable)Session["EPreference"];
            }
            else
            {
                // Retreive dealer Email preference from database
                DataTable dtFromDB = BAL.DealerCustomerBAL.GetDealerEmailPreference(Convert.ToInt64(Request["EntityId"]));
                if (dtFromDB.Rows.Count != 0)
                {
                    // Add column
                    dtEPref.Columns.Add("SNo", System.Type.GetType("System.Int32"));
                    dtEPref.Columns.Add("IsEnable", System.Type.GetType("System.Boolean"));
                    dtEPref.Columns.Add("Email", System.Type.GetType("System.String"));
                    for (int index = 0; index < dtFromDB.Rows.Count; index++)
                    {
                        // Add Row
                        DataRow dtrow = dtEPref.NewRow();
                        dtrow["SNo"] = index + 1;
                        dtrow["IsEnable"] = dtFromDB.Rows[index]["IsEnable"];
                        dtrow["Email"] = dtFromDB.Rows[index]["Email"];
                        dtEPref.Rows.Add(dtrow);
                    }
                }
                else
                {
                    // Add columns
                    dtEPref.Columns.Add("SNo", System.Type.GetType("System.Int32"));
                    dtEPref.Columns.Add("IsEnable", System.Type.GetType("System.Boolean"));
                    dtEPref.Columns.Add("Email", System.Type.GetType("System.String"));

                    // Add Default Row
                    DataRow newPreference = dtEPref.NewRow();
                    newPreference["SNo"] = 1;
                    dtEPref.Rows.Add(newPreference);
                }
            }

            // Total Rows into table
            totalEPreRow = dtEPref.Rows.Count;
            // Store current table to session
            Session["EPreference"] = dtEPref;
            // Bind to grid
            grdViewEmail.DataSource = dtEPref;

            grdViewEmail.DataBind();

            
        }
        #endregion

        #region[Remove row from preference grid]
        protected void RemoveRow(object sender, CommandEventArgs e)
        {
            string RowId = e.CommandArgument.ToString();
            // Check from which grid button click
            if (e.CommandName == "Pref")
            {
                if (Session["EditPreference"] != null)
                {
                    DataTable dtPreference = new DataTable();
                    dtPreference = (DataTable)Session["EditPreference"];
                    // If only one row, not allowed
                    if (dtPreference.Rows.Count == 1)
                        return;
                    for (int index = 0; index < dtPreference.Rows.Count; index++)
                        if (dtPreference.Rows[index]["SNo"].ToString() == RowId)
                            dtPreference.Rows[index].Delete();
                    Session["EditPreference"] = dtPreference;
                    CreatePreferenceTable();

                }
            }
            if (e.CommandName == "MPref")
            {
                if (Session["MPreference"] != null)
                {
                    DataTable dtMPref = new DataTable();
                    dtMPref = (DataTable)Session["MPreference"];
                    // If only one row, not allowed
                    if (dtMPref.Rows.Count == 1)
                        return;
                    for (int index = 0; index < dtMPref.Rows.Count; index++)
                        if (dtMPref.Rows[index]["SNo"].ToString() == RowId)
                            dtMPref.Rows[index].Delete();
                    Session["MPreference"] = dtMPref;
                    CreateMPTable();

                }
            }
            if (e.CommandName == "EPref")
            {
                if (Session["EPreference"] != null)
                {
                    DataTable dtEPref = new DataTable();
                    dtEPref = (DataTable)Session["EPreference"];
                    // If only one row, not allowed
                    if (dtEPref.Rows.Count == 1)
                        return;
                    for (int index = 0; index < dtEPref.Rows.Count; index++)
                        if (dtEPref.Rows[index]["SNo"].ToString() == RowId)
                            dtEPref.Rows[index].Delete();
                    Session["EPreference"] = dtEPref;
                    CreateEPTable();

                }
            }

        }
        #endregion

        #region[Selected Index of Drop Down]
        /// <summary>
        /// handle selected index of 
        /// year and make drop down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlMake = (DropDownList)sender;
            GridViewRow grdrow = (GridViewRow)ddlMake.Parent.Parent;
            DropDownList ddlModel = (DropDownList)grdrow.FindControl("ddlModel");
            ddlModel.DataSource = BAL.Common.GetModel(Convert.ToInt32(ddlMake.SelectedValue));
            ddlModel.DataBind();
        }

        protected void ddlMaxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlMaxYear = (DropDownList)sender;
            GridViewRow grdrow = (GridViewRow)ddlMaxYear.Parent.Parent;
            DropDownList ddlMinYear = (DropDownList)grdrow.FindControl("ddlMinYear");
            DropDownList ddlMake = (DropDownList)grdrow.FindControl("ddlMake");
            Int32 YearFrom = Convert.ToInt32(ddlMinYear.SelectedValue);
            Int32 YearTo = Convert.ToInt32(ddlMaxYear.SelectedValue);
            ddlMake.DataSource = objMasterBAL.GetMakeList(YearFrom, YearTo);
            ddlMake.DataBind();
        }
        #endregion

        #region[Add Row in preference Grid]
        protected void ImgbtnAddRow_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            string grdNo = btn.CommandArgument;
            AddNewPreRow(grdNo);
        }
        protected void AddNewPreRow(string grdNo)
        {
            DataTable dTab = new DataTable();
            if (grdNo == "1")
            {
                if (Session["EditPreference"] != null)
                {
                    dTab = (DataTable)Session["EditPreference"];
                    Int32 newId = Convert.ToInt32(dTab.Rows[dTab.Rows.Count - 1]["SNo"]) + 1; ;
                    // Add new row to Preference Table
                    DataRow newPreference = dTab.NewRow();
                    newPreference["SNo"] = newId;
                    dTab.Rows.Add(newPreference);

                    // Total Rows into table
                    totalPreRows = dTab.Rows.Count;

                    // Bind to grid
                    grdPreference.DataSource = dTab;
                    grdPreference.DataBind();
                }
                else
                    return;
            }
            if (grdNo == "2")
            {
                if (Session["MPreference"] != null)
                {
                    dTab = (DataTable)Session["MPreference"];
                    Int32 newId = Convert.ToInt32(dTab.Rows[dTab.Rows.Count - 1]["SNo"]) + 1; ;
                    // Add new row to Mobile Preference Table
                    DataRow newPreference = dTab.NewRow();
                    newPreference["SNo"] = newId;
                    dTab.Rows.Add(newPreference);

                    // Total Rows into table
                    totalMPreRow = dTab.Rows.Count;

                    // Bind to Mobile grid
                    grdViewMobile.DataSource = dTab;
                    grdViewMobile.DataBind();
                }
                else
                    return;
            }
            if (grdNo == "3")
            {
                if (Session["EPreference"] != null)
                {
                    dTab = (DataTable)Session["EPreference"];
                    Int32 newId = Convert.ToInt32(dTab.Rows[dTab.Rows.Count - 1]["SNo"]) + 1; ;
                    // Add new row to Email Preference Table
                    DataRow newPreference = dTab.NewRow();
                    newPreference["SNo"] = newId;
                    dTab.Rows.Add(newPreference);

                    // Total Rows into Email Preference table
                    totalEPreRow = dTab.Rows.Count;

                    // Bind to Mobile grid
                    grdViewEmail.DataSource = dTab;
                    grdViewEmail.DataBind();
                }
                else
                    return;
            }

          
        }
        #endregion

        #region[Row DataBound Event of Grids]
        # region[Row data bound event of preference grid]
        /// <summary>
        /// handel data baound event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdPreference_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (totalPreRows > 0)
            {
                ImageButton ibtn; DataTable dtPref = new DataTable();
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (totalPreRows - 1 > e.Row.RowIndex)
                    {
                        ibtn = (ImageButton)e.Row.FindControl("ImgbtnAddRow");
                        ibtn.Visible = false;
                    }

                    if (totalPreRows > e.Row.RowIndex)
                    {
                        if (totalPreRows == 1)
                        {
                            ibtn = (ImageButton)e.Row.FindControl("ImgbtnRemove");
                            ibtn.Visible = false;
                        }
                    }
                    // Findout Drop Down controls in preferenceGrid
                    DropDownList ddlMinYear = e.Row.FindControl("ddlMinYear") as DropDownList;
                    DropDownList ddlMaxYear = e.Row.FindControl("ddlMaxYear") as DropDownList;
                    DropDownList ddlMake = e.Row.FindControl("ddlMake") as DropDownList;
                    DropDownList ddlModel = e.Row.FindControl("ddlModel") as DropDownList;
                    CheckBox ChkEnable = e.Row.FindControl("ChkEnable") as CheckBox;

                    //Fill Year Range Drop Down From DataSource
                    ddlMinYear.DataSource = objYear;
                    ddlMinYear.DataBind();
                    ddlMaxYear.DataSource = objYear;
                    ddlMaxYear.DataBind();

                   

                    dtPref = (DataTable)Session["EditPreference"];
                    if (dtPref != null)
                    {
                        ddlMinYear.SelectedValue = dtPref.Rows[e.Row.RowIndex]["yearsFrom"].ToString();
                        ddlMaxYear.SelectedValue = dtPref.Rows[e.Row.RowIndex]["yearsTo"].ToString();
                       
                      
                        //Fill Make Drop Down
                        Int32 YearFrom = Convert.ToInt32(ddlMinYear.SelectedValue);
                        Int32 YearTo = Convert.ToInt32(ddlMaxYear.SelectedValue);
                        ddlMake.DataSource = objMasterBAL.GetMakeList(YearFrom, YearTo);
                        ddlMake.DataBind();


                        if (!string.IsNullOrEmpty(dtPref.Rows[e.Row.RowIndex]["MakeId"].ToString()))
                        ddlMake.SelectedValue = dtPref.Rows[e.Row.RowIndex]["MakeId"].ToString();

                       
                        if (dtPref.Rows[e.Row.RowIndex]["IsEnabled"].ToString() != "")
                            ChkEnable.Checked = Convert.ToBoolean(dtPref.Rows[e.Row.RowIndex]["IsEnabled"]);


                        // Fill Model Drop Down
                        if (!string.IsNullOrEmpty(ddlMake.SelectedValue))
                        {
                            ddlModel.DataSource = BAL.Common.GetModel(Convert.ToInt32(ddlMake.SelectedValue));
                            ddlModel.DataBind();
                            if (!string.IsNullOrEmpty(dtPref.Rows[e.Row.RowIndex]["ModelId"].ToString()))
                            ddlModel.SelectedValue = dtPref.Rows[e.Row.RowIndex]["ModelId"].ToString();
                        }


                    }


                }

            }
        }
       #endregion

        protected void grdViewMobile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (totalMPreRow > 0)
            {
                ImageButton ibtn; DataTable dtMPref = new DataTable();
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (totalMPreRow - 1 > e.Row.RowIndex)
                    {
                        ibtn = (ImageButton)e.Row.FindControl("ImgbtnAddRow");
                        ibtn.Visible = false;
                    }

                    if (totalMPreRow > e.Row.RowIndex)
                    {
                        if (totalMPreRow == 1)
                        {
                            ibtn = (ImageButton)e.Row.FindControl("ImgbtnRemove");
                            ibtn.Visible = false;
                        }
                    }
                    // Findout Check Box controls in Mobile preferenceGrid

                    CheckBox ChkEnable = e.Row.FindControl("chkEnable") as CheckBox;



                    dtMPref = (DataTable)Session["MPreference"];
                    if (dtMPref != null)
                    {
                        if (dtMPref.Rows[e.Row.RowIndex]["IsEnable"].ToString() != "")
                            ChkEnable.Checked = Convert.ToBoolean(dtMPref.Rows[e.Row.RowIndex]["IsEnable"]);
                    }


                }
            }
        }

        protected void grdViewEmail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (totalEPreRow > 0)
            {
                ImageButton ibtn; DataTable dtEPref = new DataTable();
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (totalEPreRow - 1 > e.Row.RowIndex)
                    {
                        ibtn = (ImageButton)e.Row.FindControl("ImgbtnAddRow");
                        ibtn.Visible = false;
                    }

                    if (totalEPreRow > e.Row.RowIndex)
                    {
                        if (totalEPreRow == 1)
                        {
                            ibtn = (ImageButton)e.Row.FindControl("ImgbtnRemove");
                            ibtn.Visible = false;
                        }
                    }
                    // Findout Check Box control in Email preferenceGrid

                    CheckBox ChkEnable = e.Row.FindControl("chkEnable") as CheckBox;



                    dtEPref = (DataTable)Session["EPreference"];
                    if (dtEPref != null)
                    {
                        if (dtEPref.Rows[e.Row.RowIndex]["IsEnable"].ToString() != "")
                            ChkEnable.Checked = Convert.ToBoolean(dtEPref.Rows[e.Row.RowIndex]["IsEnable"]);
                    }


                }
            }
        }
        #endregion

        #region[Click event to Save Record]
        protected void btnSave_Click(object sender, EventArgs e)
        {
            long DealerId = Convert.ToInt64(Request["EntityId"]);
            if (fvPreference.DataItemCount > 0)
            {
               RadioButtonList RblPrefSetting = (RadioButtonList)fvPreference.Row.FindControl("RblPrefSetting");
               RadioButtonList rblSmsSetting = (RadioButtonList)fvPreference.Row.FindControl("rblSmsSetting");
               RadioButtonList rblEmailSetting = (RadioButtonList)fvPreference.Row.FindControl("rblEmailSetting");
                // Create object of Dealer class
               Dealer objDealer = new Dealer();
                //Assign values to class properties
               objDealer.PreferenceSettings = RblPrefSetting.Items[0].Selected;
               objDealer.ReceiveSms = rblSmsSetting.Items[0].Selected;
               objDealer.ReceiveEmail = rblEmailSetting.Items[0].Selected;
               objDealer.DealerId = DealerId;
                // Update Dealer Preference Setting Details
               BAL.DealerCustomerBAL.UpdateDealerPreferenceSetting(objDealer);

                if (RblPrefSetting.Items[0].Selected == true)
                //    // Insert dealer preference details
                    AddDealerPreferenceDetails(DealerId);
               if (rblSmsSetting.Items[0].Selected == true)
                //    // Insert dealer mobile preference
                    AddDealerMobilePreference(DealerId);
               if (rblEmailSetting.Items[0].Selected == true)
                  // Insert dealer Email preference
                   AddDealerEmailPreference(DealerId);

            }
          this.Page.ClientScript.RegisterStartupScript(typeof(EditPreference), "closeThickBox", "self.parent.updated();", true);
           //Response.Redirect("testPreference.aspx");
        }

        #region [Add Dealer/Customer Preference details]
        protected void AddDealerPreferenceDetails(long DealerId)
        {
            for (int index = 0; index < grdPreference.Rows.Count; index++)
            {
                CheckBox chkEnable = (CheckBox)grdPreference.Rows[index].FindControl("chkEnable");
                DropDownList ddlMinYear = (DropDownList)grdPreference.Rows[index].FindControl("ddlMinYear");
                DropDownList ddlMaxYear = (DropDownList)grdPreference.Rows[index].FindControl("ddlMaxYear");
                DropDownList ddlMake = (DropDownList)grdPreference.Rows[index].FindControl("ddlMake");
                DropDownList ddlModel = (DropDownList)grdPreference.Rows[index].FindControl("ddlModel");
                TextBox txtMinMileage = (TextBox)grdPreference.Rows[index].FindControl("txtMinMileage");
                TextBox txtMaxMileage = (TextBox)grdPreference.Rows[index].FindControl("txtMaxMileage");
                TextBox txtMinPrice = (TextBox)grdPreference.Rows[index].FindControl("txtMinPrice");
                TextBox txtMaxPrice = (TextBox)grdPreference.Rows[index].FindControl("txtMaxPrice");

                DealerPreference objDealerPreferebce = new DealerPreference();
                objDealerPreferebce.DealerId = DealerId;
                if (!string.IsNullOrEmpty(ddlMinYear.SelectedValue))
                    objDealerPreferebce.yearsFrom = Convert.ToInt32(ddlMinYear.SelectedValue);
                if (!string.IsNullOrEmpty(ddlMaxYear.SelectedValue))
                    objDealerPreferebce.yearsTo = Convert.ToInt32(ddlMaxYear.SelectedValue);
                if (!string.IsNullOrEmpty(ddlMake.SelectedValue))
                    objDealerPreferebce.MakeId = Convert.ToInt64(ddlMake.SelectedValue);
                if (!string.IsNullOrEmpty(ddlModel.SelectedValue))
                    objDealerPreferebce.ModelId = Convert.ToInt64(ddlModel.SelectedValue);
                if (!string.IsNullOrEmpty(txtMinMileage.Text))
                    objDealerPreferebce.MileageMin = Convert.ToInt64(txtMinMileage.Text.Trim());
                if (!string.IsNullOrEmpty(txtMaxMileage.Text))
                    objDealerPreferebce.MileageMax = Convert.ToInt64(txtMaxMileage.Text.Trim());
                if (!string.IsNullOrEmpty(txtMinPrice.Text))
                    objDealerPreferebce.PriceMin = Convert.ToDecimal(txtMinPrice.Text.Trim());
                if (!string.IsNullOrEmpty(txtMaxPrice.Text))
                    objDealerPreferebce.PriceMax = Convert.ToDecimal(txtMaxPrice.Text.Trim());
                objDealerPreferebce.IsEnabled = chkEnable.Checked;
                objDealerPreferebce.AddedBy = Constant.UserId;

                
                if(index == 0)
                // Delete All records of this dealer
                BAL.DealerCustomerBAL.AddDealerPreference(objDealerPreferebce, 2);
                // Insert New records
                BAL.DealerCustomerBAL.AddDealerPreference(objDealerPreferebce, 1);
            }
            Session["EditPreference"] = null;
            //CreatePreferenceTable();

        }
        #endregion

        #region [Add Dealer/Customer SMS Preference Details]
        /// <summary>
        /// add dealer/customer Mobile Preference details
        /// </summary>
        /// <param name="DealerId"></param>
        protected void AddDealerMobilePreference(long DealerId)
        {

            for (int index = 0; index < grdViewMobile.Rows.Count; index++)
            {
                CheckBox chkEnable = (CheckBox)grdViewMobile.Rows[index].FindControl("chkEnable");
                TextBox txtMobile = (TextBox)grdViewMobile.Rows[index].FindControl("txtMobile");

                DealerMobile objDealerMobile = new DealerMobile();
                //For the first Row of Mobile Preference
                objDealerMobile.DealerId = DealerId;
                objDealerMobile.MobileNo = txtMobile.Text.Trim();
                objDealerMobile.IsEnable = chkEnable.Checked;
                objDealerMobile.AddedBy = Constant.UserId;
                if(index==0)
                    // Delete old Mobile preference Details
                BAL.DealerCustomerBAL.AddDealerMobilePreference(objDealerMobile, 2);
                BAL.DealerCustomerBAL.AddDealerMobilePreference(objDealerMobile, 1);
            }
            Session["MPreference"] = null;
           // CreateMPTable();

          

        }
        #endregion

        #region [Add Dealer/Customer Email Preference Details]
        /// <summary>
        /// add dealer/customer email preference details
        /// </summary>
        /// <param name="DealerId"></param>
        protected void AddDealerEmailPreference(long DealerId)
        {
            for (int index = 0; index < grdViewEmail.Rows.Count; index++)
            {
                CheckBox chkEnable = (CheckBox)grdViewEmail.Rows[index].FindControl("chkEnable");
                TextBox txtEmail = (TextBox)grdViewEmail.Rows[index].FindControl("txtEmail");

                DealerEmail objDealerEmail = new DealerEmail();
                //For First Row of Email Preference
                objDealerEmail.DealerId = DealerId;
                objDealerEmail.Email = txtEmail.Text.Trim();
                objDealerEmail.IsEnable = chkEnable.Checked;
                objDealerEmail.AddedBy = Constant.UserId;
                if(index == 0)
                    // Delete old Email preference
                BAL.DealerCustomerBAL.AddDealerEmailPreference(objDealerEmail, 2);
                BAL.DealerCustomerBAL.AddDealerEmailPreference(objDealerEmail, 1);
            }
            Session["EPreference"] = null;
            //CreateEPTable();
        }
        #endregion

        protected void fvPreference_DataBound(object sender, EventArgs e)
        {
            // Check If formview has row or not
            if (fvPreference.DataItemCount > 0)
            {
                // Findout controls in formview
                HiddenField hfieldPreSetting = fvPreference.FindControl("hfieldPreSetting") as HiddenField;
                HiddenField hfieldMobile = fvPreference.FindControl("hfieldMobile") as HiddenField;
                HiddenField hfieldEmail = fvPreference.FindControl("hfieldEmail") as HiddenField;

                RadioButtonList RblPrefSetting = fvPreference.FindControl("RblPrefSetting") as RadioButtonList;
                RadioButtonList rblSmsSetting = fvPreference.FindControl("rblSmsSetting") as RadioButtonList;
                RadioButtonList rblEmailSetting = fvPreference.FindControl("rblEmailSetting") as RadioButtonList;
                // Set preference setting
                if (hfieldPreSetting.Value == "True")
                    RblPrefSetting.Items[0].Selected = true;
                else
                    RblPrefSetting.Items[1].Selected = true;
                // Set Sms Setting
                if (hfieldMobile.Value == "True")
                    rblSmsSetting.Items[0].Selected = true;
                else
                    rblSmsSetting.Items[1].Selected = true;
                // Set Email Setting
                if (hfieldEmail.Value == "True")
                    rblEmailSetting.Items[0].Selected = true;
                else
                    rblEmailSetting.Items[1].Selected = true;
            }
        }

        #endregion

       

    }
}
