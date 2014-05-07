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
using METAOPTION.BAL;
namespace METAOPTION.UI
{
    public partial class AddInventory : System.Web.UI.Page, IPagePermission
    {
        public const string PAGE = "INVENTORY";
        public const string PAGERIGHT = "INVENTORY.ADD";
        public const string PAGE_ERROR = "Record Couldn't be Saved Successfully,Some error occurred";
        //Check whether LaneAutomation is enabled or disabled
        //String EnableLaneAutomation = ConfigurationManager.AppSettings["EnableLaneAutomation"];

        #region Public Variables
        string _Year = string.Empty;
        string _Model = string.Empty;
        string _Make = string.Empty;
        string _Body = string.Empty;
        long _PreInvID = 0;
        long _ExtColorID = 0;
        long _IntColorID = 0;

        PreInventoryBAL PreInvBAL = new PreInventoryBAL();
        #endregion

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
            //Set OrgID context key of AutoCompleteExtender from Session
            txtTest_AutoCompleteExtender.ContextKey = Constant.OrgID.ToString();
            //Set Default Button for this page
            Page.Form.DefaultButton = btnAdd.UniqueID;

            //Check User Permissions for "INVENTORY.ADD" Right
            CheckUserPagePermissions();

            if (!IsPostBack)
            {
                // Get the URL from where the request is coming
                String ReferrerURL = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : "/Default.aspx";
                hfReferrerURL.Value = ReferrerURL;

                //Set default value of Purchase Date to Current Date
                txtPurchaseDate.Text = DateTime.Today.ToShortDateString();

                if (!string.IsNullOrEmpty(Request.QueryString["PreInvID"]))
                {
                    _PreInvID = Convert.ToInt64(Request.QueryString["PreInvID"]);
                    // Validate this pre inventory id then fill the controls 
                    Util.Validate_QueryString_Value("AddPreInventory", _PreInvID.ToString(), Constant.OrgID);
                    FillControlsByPreInvID(_PreInvID);
                }

                else
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["VIN"]))
                        txtVinNo.Text = Request.QueryString["VIN"];

                    AddGenericColors();

                    #region Set Default Values for Year,Make,Model,Body as fetched from User from ValidateVin screen

                    //SET YEAR
                    if (!string.IsNullOrEmpty(Request.QueryString["Year"]))
                        _Year = Request.QueryString["Year"].ToString();

                    //SET MAKE
                    if (!string.IsNullOrEmpty(Request.QueryString["MakeId"]))
                        _Make = Request.QueryString["MakeId"];


                    //SET MODEL
                    if (!string.IsNullOrEmpty(Request.QueryString["ModelId"]))
                        _Model = Request.QueryString["ModelId"];


                    //SET BODY
                    if (!string.IsNullOrEmpty(Request.QueryString["BodyId"]))
                        _Body = Request.QueryString["BodyId"];

                    #endregion

                    //Make it true for Setting Default Values from Previous selected values on ValidateVin Screen
                    LoadMasters(true);

                    //Bind grades drop down list
                    BindGrades();
                }

            }

        }

        #region ApplyPagePermission Members
        /// <summary>
        /// Check User Permissions for "INVENTORY.ADD" Right
        /// </summary>
        public void CheckUserPagePermissions()
        {
            System.Collections.Generic.List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, PAGE);
            bool bTrue = true;
            if (!dict.Contains(PAGERIGHT))
            {
                bTrue = false;
            }

            //Redirect User to Common Page for InSufficient Rights
            if (!(dict.Contains(PAGERIGHT) || bTrue))
                Response.Redirect("Permission.aspx?MSG=INVENTORY.ADD");
        }

        #endregion

        ///// <summary>
        ///// This function will mask lane number and remove any _ appended and replace it with 0
        ///// </summary>
        ///// <param name="LaneNumber"></param>
        //private string  maskLaneNumber(string strLaneNumber)
        //{
        //   string strTemp = strLaneNumber.Replace('_', '0');
        //   return strTemp;
        //}

        #region Bind Make
        /// <summary>
        /// Bind Dropdownlist with make details based on year selected
        /// </summary>
        private void BindMake(int year, bool isDefault)
        {
            /*Bind Make DropDownlist*/
            ddlMake.DataSource = Common.GetMakes(year);
            ddlMake.DataTextField = "Make";
            ddlMake.DataValueField = "MakeId";
            ddlMake.DataBind();
            ddlMake.Items.Insert(0, new ListItem("", ""));

            if (isDefault)
                if (ddlMake.Items.FindByValue(_Make) != null)
                    ddlMake.SelectedValue = _Make;

            //Bind Model this selected Make
            if (!string.IsNullOrEmpty(ddlMake.SelectedValue))
            {
                BindModel(Convert.ToInt32(ddlMake.SelectedValue), isDefault);
            }



        }

        #endregion

        #region Bind Model
        /// <summary>
        /// Bind dropdownlist with Model details based on make selected
        /// </summary>
        private void BindModel(int makeId, bool isDefault)
        {
            /*Bind Model DropDownlist*/
            ddlModel.DataSource = Common.GetModel(makeId);
            ddlModel.DataTextField = "Model";
            ddlModel.DataValueField = "ModelId";
            ddlModel.DataBind();
            ddlModel.Items.Insert(0, new ListItem("", ""));

            if (isDefault)
                if (ddlModel.Items.FindByValue(_Model) != null)
                    ddlModel.SelectedValue = _Model;

            //Bind Body
            if (!string.IsNullOrEmpty(ddlModel.SelectedValue))
                BindBody(Convert.ToInt32(ddlModel.SelectedValue), isDefault);

            //Bind Colors for Selected Year,Make,Model,Body
            // BindColors();
        }
        #endregion

        #region Bind Body
        /// <summary>
        /// Bind dropdownlist with Body details based on make selected
        /// </summary>
        private void BindBody(int modelId, bool isDefault)
        {
            Common objCommon = new Common();
            /*Bind Body DropDownlist*/
            ddlBody.DataSource = objCommon.GetBodies(modelId);
            ddlBody.DataTextField = "Body";
            ddlBody.DataValueField = "BodyId";
            ddlBody.DataBind();
            ddlBody.Items.Insert(0, new ListItem("", ""));
            //Set Default Value
            if (isDefault)
                if (ddlBody.Items.FindByValue(_Body) != null)
                    ddlBody.SelectedValue = _Body;

            //Bind Colors for Selected Year,Make,Model,Body
            BindColors();
        }
        #endregion

        #region Bind Designations
        /// <summary>
        /// Bind dropdownlist with Designations
        /// </summary>
        private void BindDesignations()
        {
            Common objCommon = new Common();
            /*Bind Designation List*/
            ddlDesignation.DataSource = objCommon.GetDesignationList();
            ddlDesignation.DataTextField = "Designation";
            ddlDesignation.DataValueField = "DesignationId";
            ddlDesignation.DataBind();
        }
        #endregion

        #region Bind Buyers
        //<summary>
        //Bind dropdownlist with Buyers details
        //</summary>
        private void BindBuyers()
        {
            /*Bind BuyerAgent List*/
            BuyerBAL bal = new BuyerBAL();
            ddlBuyer.DataSource = bal.GetAllDirectBuyers(Constant.OrgID);// Common.GetBuyerList();
            ddlBuyer.DataTextField = "BuyerName";
            ddlBuyer.DataValueField = "BuyerId";
            ddlBuyer.DataBind();
            //Add One Blank Item
            ddlBuyer.Items.Insert(0, "");
        }
        #endregion

        #region BIND All Masters

        /// <summary>
        /// This method Get masters information by calling BAL Layer Methods
        /// </summary>
        private void LoadMasters(bool isDefault)
        {
            /*Bind Years DropDownlist*/
            BindYears(isDefault);
            BindBuyers();
            BindDesignations();
            BindEngines();
            BindTrans();
            BindWheelDrives();
        }
        #endregion

        #region Bind Engines
        /// <summary>
        /// Bind List of engines in dropdownlist
        /// </summary>
        private void BindEngines()
        {
            Common objCommon = new Common();
            ddlEngine.DataSource = objCommon.GetEngineList();
            ddlEngine.DataTextField = "EngineType";
            ddlEngine.DataValueField = "EngineId";
            ddlEngine.DataBind();

            //Proving one blank value for Making this dropdownlist "OPTIONAL"
            ddlEngine.Items.Insert(0, "");
        }
        #endregion

        #region Bind Trans
        /// <summary>
        /// Bind list of Trans in dropdownlist
        /// </summary>
        private void BindTrans()
        {
            ddlTrans.DataSource = Common.GetTransList();
            ddlTrans.DataTextField = "TransType";
            ddlTrans.DataValueField = "TransId";
            ddlTrans.DataBind();

            //Proving one value item for Making this dropdownlist "OPTIONAL"
            ddlTrans.Items.Insert(0, "");
        }
        #endregion

        #region Bind Wheel Drives
        /// <summary>
        /// Bind list of WheelDrives in dropdownlist
        /// </summary>
        private void BindWheelDrives()
        {
            Common objCommon = new Common();
            ddlWheelDrive.DataSource = objCommon.GetWheelDriveList();
            ddlWheelDrive.DataTextField = "WheelDrive";
            ddlWheelDrive.DataValueField = "WheelDriveId";
            ddlWheelDrive.DataBind();

            //Proving one blank Value for Making this dropdownlist "OPTIONAL"
            ddlWheelDrive.Items.Insert(0, "");
        }
        #endregion

        //#region Bind Car Location
        //private void BindCarLocation()
        //{
        //    ddlCarLocation.DataSource = Common.GetCarLocations();
        //    ddlCarLocation.DataTextField = "CarLocation";
        //    ddlCarLocation.DataValueField = "CarLocationId";
        //    ddlCarLocation.DataBind();
        //}
        //#endregion

        #region Bind Years
        /// <summary>
        /// Bind Years in dropdownlist
        /// </summary>
        private void BindYears(bool isDefault)
        {

            /*Load Years*/
            ddlYear.DataSource = Common.GetYearList();
            ddlYear.DataValueField = "Year";
            ddlYear.DataTextField = "Year";
            ddlYear.DataBind();
            ddlYear.SelectedIndex = 0;

            if (isDefault)
                if (ddlYear.Items.FindByValue(_Year) != null)
                    ddlYear.SelectedValue = _Year;

            //Bind Make Details for Selected Year
            if (!string.IsNullOrEmpty(ddlYear.SelectedValue))
                BindMake(Convert.ToInt32(ddlYear.SelectedValue), isDefault);


        }
        #endregion

        #region ADD Inventory
        /// <summary>
        /// This method call BAL LAYER ADDINVENTORY METHOD FOR INSERTION OF NEW INVENTORY
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private long AddInvent()
        {
            //#region Passing Parameters to BAL LAYER for Add New Inventory
            long InventoryId = 0;

            Inventory obj = new Inventory();

            obj.VIN = txtVinNo.Text;

            if (!string.IsNullOrEmpty(txtArrivalDate.Text.Trim()))
                obj.ArrivalDate = Convert.ToDateTime(txtArrivalDate.Text.Trim());

            if (!string.IsNullOrEmpty(txtCost.Text))
                obj.CarCost = Convert.ToDecimal(txtCost.Text.Trim());

            // Added by Ashar on 01 June'2012 as per Naushad's request
            if (!string.IsNullOrEmpty(txtMarketPrice.Text))
                obj.MarketPrice = Convert.ToDecimal(txtMarketPrice.Text.Trim());

            if (!string.IsNullOrEmpty(ddlDesignation.SelectedValue))
                obj.DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue);

            if (!string.IsNullOrEmpty(ddlMake.SelectedValue))
                obj.MakeId = Convert.ToInt64(ddlMake.SelectedValue);

            if (!string.IsNullOrEmpty(ddlModel.SelectedValue))
                obj.ModelId = Convert.ToInt32(ddlModel.SelectedValue);

            if (!string.IsNullOrEmpty(ddlBody.SelectedValue))
                obj.BodyId = Convert.ToInt32(ddlBody.SelectedValue);

            //Setting Interior and exterior color selected
            if (!string.IsNullOrEmpty(ddlExtCol.SelectedValue))
                obj.ExtColorId = Convert.ToInt32(ddlExtCol.SelectedValue);

            // Set garde
            if (ddlGrade.SelectedValue != "-1")
                obj.Grade = Convert.ToInt32(ddlGrade.SelectedValue);
            else
                obj.Grade = null;

            if (!string.IsNullOrEmpty(ddlIntCol.SelectedValue))
                obj.IntColorId = Convert.ToInt32(ddlIntCol.SelectedValue);

            if (!string.IsNullOrEmpty(ddlVehiclePresent.SelectedValue))
                obj.VehiclePresent = Convert.ToBoolean(ddlVehiclePresent.SelectedValue);

            if (!string.IsNullOrEmpty(txtMileageIn.Text))
                obj.MileageIn = Convert.ToInt64(txtMileageIn.Text.Trim());

            obj.CarNote = txtNote.Text;

            if (!string.IsNullOrEmpty(txtPurchaseDate.Text.Trim()))
                obj.PurchaseDate = Convert.ToDateTime(txtPurchaseDate.Text.Trim());

            obj.RegularLane = txtRLaneNo.Text;
            obj.VirtualLane = txtVLaneNo.Text;
            obj.ExoticLane = txtELaneNo.Text;
            obj.OnlineLane = txtOLaneNo.Text;

            obj.TitlePresentNotes = txtTitlePresentNote.Text.Trim();
            obj.TitlePresent = chkTitlePresent.Checked;

            obj.CheckNumber = txtChequeNo.Text.Trim();
            obj.DesignatedEquipment = txtDEquipment.Text.Trim();

            //Set selected dealer Id from Hidden Field
            if (!string.IsNullOrEmpty(hdDealerId.Value))
                obj.DealerId = Convert.ToInt64(hdDealerId.Value);

            if (!string.IsNullOrEmpty(txtCarLocation.Text))
                obj.CarLocation = txtCarLocation.Text.Trim();

            if (ddlYear.SelectedItem != null)
                obj.Year = Convert.ToInt32(ddlYear.SelectedValue);

            if (!string.IsNullOrEmpty(ddlBuyer.SelectedValue))
                obj.BuyerId = Convert.ToInt32(ddlBuyer.SelectedValue);

            //Re-Order/Removed Items as Change request made by Claudia
            //Setting car properties
            obj.AC = chkCarProp1.Items[0].Selected;
            obj.PowerLocks = chkCarProp1.Items[1].Selected;
            obj.PowerWindows = chkCarProp1.Items[2].Selected;

            obj.SunMoon = chkCarProp2.Items[0].Selected;
            obj.Leather = chkCarProp2.Items[1].Selected;
            obj.Navigation = chkCarProp2.Items[2].Selected;

            //Note required
            obj.AlloyWheels = false;  //chkCarProp2.Items[1].Selected;
            obj.PowerSeat = false;   //chkCarProp2.Items[3].Selected;

            //Set Engine,WheelDrive,Trans
            if (!string.IsNullOrEmpty(ddlEngine.SelectedValue))
                obj.EngineId = Convert.ToInt32(ddlEngine.SelectedValue);

            if (!string.IsNullOrEmpty(ddlWheelDrive.SelectedValue))
                obj.WheelDriveId = Convert.ToInt32(ddlWheelDrive.SelectedValue);

            if (!string.IsNullOrEmpty(ddlTrans.SelectedValue))
                obj.TransId = Convert.ToInt32(ddlTrans.SelectedValue);

            //Set Default CarStatus(Inventory)
            obj.CarStatus = 1;

            // Set other defaults where db does not allow NULL
            obj.IsExotic = false;
            obj.TitleShipped = false;
            obj.SoldStatus = 0;
            obj.ComeBackStatus = false;
            obj.SystemID = Convert.ToInt32(Session["SystemID"]);//Added by Shiv
            //Read UserId from session
            long UserId = Constant.UserId;
            //UserId = 1;
            obj.AddedBy = UserId;

            if (ddlBarCarFax.SelectedValue != "-1")
                obj.BadCarFax = Convert.ToBoolean(Convert.ToInt32(ddlBarCarFax.SelectedValue));

            if (ddlBadAutoCheck.SelectedValue != "-1")
                obj.BadAutoCheck = Convert.ToBoolean(Convert.ToInt32(ddlBadAutoCheck.SelectedValue));

            //Associate ComeBack Entry with old inv id
            if (!string.IsNullOrEmpty(hdOldInventoryId.Value))
                obj.OldInventoryId = Convert.ToInt64(hdOldInventoryId.Value);

            //Add organisation id from session
            obj.OrgID = Constant.OrgID;

            /*CALL BAL LAYER ADDINVENTORY METHOD*/
            InventoryId = InventoryBAL.AddInventory(obj);

            #region [Update Title Tracking Note]

            if (!string.IsNullOrEmpty(txtTitleTrackingNote.Text.Trim()))
            {
                if (InventoryId > 0)
                {
                    long NoteId = 0;
                    Note objNote = new Note();
                    objNote.EntityId = InventoryId;
                    objNote.EntityTypeId = (int)EntityTypes.Inventory;
                    objNote.Notes = txtTitleTrackingNote.Text.Trim();
                    objNote.SecurityUserId = Constant.UserId;
                    objNote.DateAdded = DateTime.Now;
                    objNote.AddedBy = Constant.UserId;
                    objNote.NoteTypeID = (int)NoteType.TITAL_TRACKING_NOTE;
                    objNote.IsActive = 1;
                    NoteId = InventoryBAL.UpdateNote(objNote, (int)NoteSave_Mode.Add);
                }

            }
            #endregion

            //return InventoryId;
            #region[Assign Lane Number by calling Lane Automation API]
            LNA_LaneAssignmentBAL objLA = new LNA_LaneAssignmentBAL();

            //Check for Automatic Lane Numbering true/false from database
            String LaneAutomationStatus = objLA.GetLaneAutomationStatus();
            //If Lane Automation is enabled
            if (LaneAutomationStatus == "1")
            {
                //Check for day/time settings
                //LaneSettingRuleType 4 : New Inventory Lane Numbering
                if (objLA.IsSettingExist(4, Constant.OrgID))
                {
                    DataTable dt = new DataTable();
                    // Get LaneNumber for the inventory according to the rule
                    dt = objLA.GetInventoryLaneRun(InventoryId);
                    Int32 LaneTypeID = 0;
                    String LaneRunNo = "";
                    long LaneRuleID = 0;
                    if (dt.Rows.Count > 0)
                    {
                        LaneTypeID = Convert.ToInt32(dt.Rows[0]["LaneTypeID"]);
                        LaneRunNo = Convert.ToString(dt.Rows[0]["LaneRunNo"]);
                        LaneRuleID = Convert.ToInt64(dt.Rows[0]["LaneRuleID"]);
                        // Assign lane number to the inventory
                        objLA.Inventory_AssignLane(InventoryId, LaneTypeID, LaneRunNo, LaneRuleID, Constant.UserId);
                        // Insert record in LogLaneHistory table
                        objLA.LogLaneHistory(InventoryId, 11, obj.RegularLane, LaneRunNo, 0, Constant.UserId, "Lane Automation API", "API", -10, true);
                    }
                }
            }
            #endregion
            return InventoryId;
        }
        #endregion

        #region U.I Events
        #region ADD BUTTON EVENT
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //if (Request.QueryString.HasKeys())
            //{
            //    string returnurl = Request["ReturnUrl"];
            //    Response.Redirect(returnurl, true);

            //}

            long inventoryNo;
            if (IsValid)
            {
                inventoryNo = AddInvent();
                if (Request.QueryString.HasKeys())
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["PreInvID"]))
                    {
                        _PreInvID = Convert.ToInt64(Request.QueryString["PreInvID"]);
                        PreInvBAL.Update_Inv_PreInv(inventoryNo, _PreInvID, Convert.ToInt64(Session["empId"]));
                    }
                }

                //inventoryNo = 27;
                if (inventoryNo > 0)
                    Response.Redirect("InventoryDetail.aspx?Code=" + inventoryNo);

                else
                    Master.PageMessage = PAGE_ERROR;

            }
        }
        #endregion
        /// <summary>
        /// Reset Control Values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Redirect to same page to Reset fields
            //Response.Redirect(Request.Url.AbsoluteUri);  
            Response.Redirect(hfReferrerURL.Value);
        }
        #endregion

        /// <summary>
        /// Handle Selected Index Changed Event of Year DropDownlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clear all dropdownlists depend directly/indirectly on this
            ddlMake.Items.Clear();
            ddlModel.Items.Clear();
            ddlBody.Items.Clear();
            AddGenericColors();


            //Fetch Makes based on selected Year
            if (!string.IsNullOrEmpty(ddlYear.SelectedValue))
                BindMake(Convert.ToInt32(ddlYear.SelectedValue), false);
        }

        /// <summary>
        /// Handle Selected Indexed Event of ddlMake
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clear Dropdownlists depend on this
            ddlModel.Items.Clear();
            ddlBody.Items.Clear();
            AddGenericColors();

            //Fetch Body and Model for selected Make
            if (!string.IsNullOrEmpty(ddlMake.SelectedValue))
            {
                //Fetch Model's based on Selected Make
                BindModel(Convert.ToInt32(ddlMake.SelectedValue), false);
            }

        }

        /// <summary>
        /// Handle Selected Indexed Changed event of Dropdownlist ddlBody to Fetch Colors for Selected Year,Make,Model,Body
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void ddlBody_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Add Generic Colors
            AddGenericColors();

            //Fetch Colors for Selected Year,Make,Model,Body
            BindColors();
        }

        /// <summary>
        /// Bind Interior and Exterior Colors for Selected Year,Make,Model,Body
        /// </summary>

        private void BindColors()
        {
            //Common objCommon = new Common();
            ListItem lstItem = null;
            //Bind Interior Colors
            if (!string.IsNullOrEmpty(ddlBody.SelectedValue) && !string.IsNullOrEmpty(ddlMake.SelectedValue))
            {
                //Bind Interior Colors For Selected Year,Make,Model,Body
                List<GetInteriorColorsForMMBYResult> objInteriorColors = Common.GetInteriorColorForMMBY(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMake.SelectedValue), Convert.ToInt32(ddlModel.SelectedValue), Convert.ToInt32(ddlBody.SelectedValue));


                //Fill Chrome Interior Colors
                foreach (GetInteriorColorsForMMBYResult Item in objInteriorColors)
                {
                    lstItem = new ListItem(Item.IntDesc, Item.IntColorId.ToString());
                    ddlIntCol.Items.Add(lstItem);
                }

            }



            //Bind Exterior Colors
            if (!string.IsNullOrEmpty(ddlBody.SelectedValue) && !string.IsNullOrEmpty(ddlMake.SelectedValue))
            {
                //Bind Exterior Colors For Selected Year,Make,Model,Body
                List<GetExteriorColorsForMMBYResult> objExteriorColors = Common.GetExteriorColorForMMBY(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMake.SelectedValue), Convert.ToInt32(ddlModel.SelectedValue), Convert.ToInt32(ddlBody.SelectedValue));

                //Fill Chrome Exterior Colors
                foreach (GetExteriorColorsForMMBYResult Item in objExteriorColors)
                {
                    lstItem = new ListItem(Item.Ext1Desc, Item.ExtColorId.ToString());
                    ddlExtCol.Items.Add(lstItem);
                }


            }

        }

        /// <summary>
        /// Add Generic Colors in DropDownLists
        /// </summary>

        private void AddGenericColors()
        {
            ddlExtCol.Items.Clear();
            ddlIntCol.Items.Clear();

            //Append Generic Interior Colors in the Interior Color DropDownlist
            List<GetCommonIntColorsResult> objCommonIntColors = Common.CommonInteriorColors();
            ddlIntCol.DataSource = objCommonIntColors;
            ddlIntCol.DataTextField = "IntDesc";
            ddlIntCol.DataValueField = "IntColorId";
            ddlIntCol.DataBind();

            //Insert One Blank Item
            ddlIntCol.Items.Insert(0, new ListItem("", ""));

            //Append Generic Exterior Colors in the Exterior Color DropDownlist
            List<GetCommonExtColorsResult> objCommonExtColors = Common.CommonExteriorColors();

            ddlExtCol.DataSource = objCommonExtColors;
            ddlExtCol.DataTextField = "Ext1Desc";
            ddlExtCol.DataValueField = "ExtColorId";
            ddlExtCol.DataBind();

            //Insert Blank Item
            ddlExtCol.Items.Insert(0, new ListItem("", ""));
        }

        ///// <summary>
        ///// Bind Interior Colors either based on selected YMMB and append Generic Colors at last
        ///// </summary>
        ///// <param name="q"></param>
        //private void BindInteriorColors(IQueryable source)
        //{
        //    ddlIntCol.DataSource = source;
        //    ddlIntCol.DataTextField = "IntDesc";
        //    ddlIntCol.DataValueField = "IntColorId";
        //    ddlIntCol.DataBind();
        //}

        ///// <summary>
        ///// Bind Exterior Colors either based on selected YMMB and append Generic Colors at last
        ///// </summary>
        ///// <param name="q"></param>
        //private void BindExteriorColors(IQueryable source)
        //{
        //    ddlExtCol.DataSource = source;
        //    ddlExtCol.DataTextField = "Ext1Desc";
        //    ddlExtCol.DataValueField = "ExtColorId";
        //    ddlExtCol.DataBind();

        //}

        /// <summary>
        /// Handle Selected Indexed Changed event of Dropdownlist ddlModel to Fetch Colors for Selected Year,Make,Model,Body
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clear all items
            ddlBody.Items.Clear();
            AddGenericColors();

            //Bind Body details for selected model
            if (!string.IsNullOrEmpty(ddlModel.SelectedValue))
                BindBody(Convert.ToInt32(ddlModel.SelectedValue), false);


        }

        ///// <summary>
        ///// Bind gridview Control with dealer details fetched based on parameters passed
        ///// </summary>
        //private void BindDealers(string strDealerName, string strDealerCity, string strDealerZip, int dealerStateId)
        //{
        //    gvDealerDetails.DataSource = InventoryBAL.SearchDealer(strDealerName, strDealerCity,
        //                                                          dealerStateId, strDealerZip);
        //    gvDealerDetails.DataBind();

        //}



        ///// <summary>
        ///// Handle Row Click Event for toggling selected GridRow by changing background color
        ///// </summary>
        ///// <param name="sender"></param> 
        ///// <param name="e"></param>
        //protected void gvDealerDetails_RowCreated(object sender, GridViewRowEventArgs e)
        //{

        //}

        //protected void gvDealerDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        //{

        //}

        //protected void gvDealerDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        //{

        //}

        ///// <summary>
        ///// Handling Page Index Changing event of Gridview control
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void gvDealerDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvDealerDetails.PageIndex = e.NewPageIndex;
        //    int dealerStateId = Convert.ToInt32(ddlDealerState.SelectedValue);
        //    BindDealers(txtDealerToSearch.Text.Trim(), txtDealerCity.Text.Trim(), txtDealerZip.Text.Trim(),
        //       dealerStateId);

        //}
        //#endregion


        ///// <summary>
        ///// Override Render event for Invalid Page Postback Event Valdidation error and set unique id
        ///// </summary>
        ///// <param name="writer"></param>
        //protected override void Render(HtmlTextWriter writer)
        //{
        //    foreach (GridViewRow r in gvDealerDetails.Rows)
        //    {
        //        if (r.RowType == DataControlRowType.DataRow)
        //        {
        //            Page.ClientScript.RegisterForEventValidation
        //                    (r.UniqueID + "$ctl00");

        //        }
        //    }

        //    base.Render(writer);
        //}



        ///// <summary>
        ///// Handle Gridview ImageButton Click event for handling selected dealer id and text
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void imgbtnSelect_Click(object sender, ImageClickEventArgs e)
        //{
        //    ImageButton imgbtnSelect = (ImageButton)sender;
        //    GridViewRow row = (GridViewRow)imgbtnSelect.NamingContainer;
        //    //Set Selected DealerId in Hidden Field for Insertion in db table
        //    hdDealerId.Value = imgbtnSelect.CommandArgument.ToString(); ;

        //    //Set Name of Selected Dealer in textbox field
        //    txtDealerShip.Text = row.Cells[2].Text;


        //    //Hide Model PopUp
        //    MPEDealerSearch.Hide();
        //}

        protected void lnkSelectCar_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handle Image button click event of Linked Car Gridview for getting selected car inventoryid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnSelLinkedCar_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtnSelect = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgbtnSelect.NamingContainer;

            //Set Name of Selected Inventory Id in Hidden Field
            //This selected inventoryid will be updated as oldinventoryid against current inventory record
            hdOldInventoryId.Value = imgbtnSelect.CommandArgument.ToString();

            //Set other Linked car values from db
            lblCarCost.Text = row.Cells[11].Text;
            lblDesignation.Text = row.Cells[12].Text;
            lblCommission.Text = row.Cells[13].Text;
            lblSoldPrice.Text = row.Cells[14].Text;
            //lblProfit.Text = row.Cells[15].Text;
            //lblExpense.Text = row.Cells[16].Text;
            lblComebackFrom.Text = row.Cells[15].Text;
            lblSoldTo.Text = row.Cells[16].Text;
            lblPurchaseFrom.Text = row.Cells[17].Text;
            lblBuyerName.Text = row.Cells[18].Text;

            //Hide Model PopUp
            MPELinkedCars.Hide();
        }

        protected void gvSelLinkedCars_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Hide few columns, there values displayed on page once popup hided
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                //FOR ROW
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;
                e.Row.Cells[17].Visible = false;
                e.Row.Cells[18].Visible = false;

            }

        }
        ///// <summary>
        ///// Handle Click event of Search Dealer button
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnSearchDealers_Click(object sender, EventArgs e)
        //{
        //    string strDealerName = null;
        //    string strDealerCity = null;
        //    string strDealerZip = null;
        //    int dealerStateId = -1;

        //    //Set Variables to be passed to BAL LAYER
        //    if (!string.IsNullOrEmpty(txtDealerToSearch.Text.Trim()))
        //        strDealerName = txtDealerToSearch.Text.Trim();

        //    if (!string.IsNullOrEmpty(txtDealerCity.Text.Trim()))
        //        strDealerCity = txtDealerCity.Text.Trim();

        //    if (!string.IsNullOrEmpty(txtDealerZip.Text.Trim()))
        //        strDealerZip = txtDealerZip.Text.Trim();

        //    if (!string.IsNullOrEmpty(ddlDealerState.SelectedValue))
        //        dealerStateId = Convert.ToInt32(ddlDealerState.SelectedValue);

        //    //Get Searched results for dealers,if any
        //    //Bind gridViewControl
        //    BindDealers(strDealerName, strDealerCity, strDealerZip, dealerStateId);
        //    MPEDealerSearch.Show();
        //}

        #region [Bind grades dropdownlist]
        private void BindGrades()
        {
            DataTable dtGrades = InventoryBAL.FetchGrades();
            if (dtGrades.Rows.Count > 0)
            {
                ddlGrade.DataSource = dtGrades;
                ddlGrade.DataTextField = "Id";
                ddlGrade.DataValueField = "Id";
                ddlGrade.DataBind();
                ListItem li = new ListItem("", "-1");
                ddlGrade.Items.Insert(0, li);
            }
        }
        #endregion

        #region[FillControlsByPreInvID]
        public void FillControlsByPreInvID(long PreInvID)
        {
            Mobile_PreInventory objInv = new Mobile_PreInventory();
            objInv = PreInvBAL.GetPreInventoryByID(PreInvID);

            if (objInv != null)
            {
                //These colors are independent of Year,Make,Model,Body Selected
                AddGenericColors();
                txtVinNo.Text = objInv.VIN;

                _Year = Convert.ToString(objInv.Year);
                _Make = Convert.ToString(objInv.MakeId);
                _Model = Convert.ToString(objInv.ModelId);
                _Body = Convert.ToString(objInv.BodyId);

                _ExtColorID = Convert.ToInt64(objInv.ExtColorId);
                _IntColorID = Convert.ToInt64(objInv.IntColorId);

                txtMileageIn.Text = Convert.ToString(objInv.MileageIn);
                txtNote.Text = objInv.Comments;
                txtCost.Text = Convert.ToString(objInv.Price);
                txtDealerShip.Text = PreInvBAL.GetDealerName(Convert.ToInt64(objInv.DealerId));
                hdDealerId.Value = Convert.ToString(objInv.DealerId);
                if (objInv.BuyerId != 0)
                    ddlBuyer.SelectedValue = objInv.BuyerId.ToString();

                chkCarProp2.Items[0].Selected = Convert.ToBoolean(objInv.Roof);
                chkCarProp2.Items[1].Selected = Convert.ToBoolean(objInv.Leather);
                chkCarProp2.Items[2].Selected = Convert.ToBoolean(objInv.Navigation);

                //Make it true for Setting Default Values from Previous selected values on ValidateVin Screen
                LoadMasters(true);

                BindColors();
                if (ddlExtCol.Items.FindByValue(Convert.ToString(_ExtColorID)) != null)
                    ddlExtCol.SelectedValue = Convert.ToString(_ExtColorID);


                if (ddlIntCol.Items.FindByValue(Convert.ToString(_IntColorID)) != null)
                    ddlIntCol.SelectedValue = Convert.ToString(_IntColorID);

                txtMarketPrice.Text = Convert.ToString(objInv.MarketPrice);
                if (objInv.BadCarFax != null)
                    ddlBarCarFax.SelectedValue = Convert.ToBoolean(objInv.BadCarFax) ? "1" : "0";
                else
                    ddlBarCarFax.SelectedValue = "-1";

                if (objInv.BadAutoCheck != null)
                    ddlBadAutoCheck.SelectedValue = Convert.ToBoolean(objInv.BadAutoCheck) ? "1" : "0";
                else
                    ddlBadAutoCheck.SelectedValue = "-1";

                if (objInv.Grade != null)
                    ddlGrade.SelectedValue = Convert.ToString(objInv.Grade);

                if (objInv.EngineId != null)
                    ddlEngine.SelectedValue = Convert.ToString(objInv.EngineId);

                if (objInv.TransId != null)
                    ddlTrans.SelectedValue = Convert.ToString(objInv.TransId);

                if (objInv.WheelDriveId != null)
                    ddlWheelDrive.SelectedValue = Convert.ToString(objInv.WheelDriveId);

                chkCarProp1.Items[0].Selected = Convert.ToBoolean(objInv.AC);
                chkCarProp1.Items[1].Selected = Convert.ToBoolean(objInv.PowerLocks);
                chkCarProp1.Items[2].Selected = Convert.ToBoolean(objInv.PowerWindows);

                //Bind grades drop down list
                BindGrades();
            }
        }
        #endregion

    }
}