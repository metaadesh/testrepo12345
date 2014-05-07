using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using METAOPTION.BAL;
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;
using METAOPTION.Reports;

namespace METAOPTION.UI
{
    // public enum Status {OnHand,Archieve,Inventory }
    //public enum NoteSave_Mode
    //{
    //    Add = 1,
    //    Update = 2,
    //    Delete = 3
    //}

    public partial class InventoryDetail : System.Web.UI.Page, IPagePermission
    {

        #region Constants and Public variables
        public const string INVENTORY_ONHAND = "ONHAND";
        public const string INVENTORY_ARCHIEVE = "ARCHIEVE";

        //Page Level Rights
        public const string PAGE = "INVENTORY";
        public const string PAGERIGHT_EDIT = "INVENTORY.EDIT";
        public const string PAGERIGHT_VIEW = "INVENTORY.VIEW";
        public const string PAGERIGHT_DELETE = "INVENTORY.DELETE";
        //Page Section Level Rights
        public const string INVENTORY_CARDETAILS_VIEW = "INVENTORY.CARDETAILS.VIEW";
        public const string INVENTORY_CARDETAILS_EDIT = "INVENTORY.CARDETAILS.EDIT";
        public const string INVENTORY_CARPROPERTY_VIEW = "INVENTORY.CARPROPERTY.VIEW";
        public const string INVENTORY_CARPROPERTY_EDIT = "INVENTORY.CARPROPERTY.EDIT";
        public const string INVENTORY_DEALERDETAILS_VIEW = "INVENTORY.DEALERDETAILS.VIEW";
        public const string INVENTORY_DEALERDETAILS_EDIT = "INVENTORY.DEALERDETAILS.EDIT";
        public const string INVENTORY_SOLDTO_VIEW = "INVENTORY.SOLDTO.VIEW";
        public const string INVENTORY_SOLDTO_EDIT = "INVENTORY.SOLDTO.EDIT";
        public const string INVENTORY_LINKEDCARS_VIEW = "INVENTORY.LINKEDCARS.VIEW";


        //Page Messages For Move to Inventory,Archieve,Onhand
        public const string SUCCESSFULLY_MOVE_TO_ARCHIEVE = "Car Successfully moved to Archive";
        public const string SUCCESSFULLY_MOVE_TO_ONHAND = "Car Successfully moved to OnHand";
        public const string SUCCESSFULLY_MOVE_TO_INVENTORY = "Car Successfully moved to Inventory";


        //BUTTON Enabled CSS
        public const string BUTTON_DISABLE_CSS = "Btn_Disabled";
        public const string BUTTON_ENABLE_CSS = "Btn_Form";
        //BUTTON Disabled CSS

        Int32 _code = -1;
        List<string> lstChromeData = null;
        string dealerOrSoldToSection = string.Empty;

        //CR related
        public string UCRlinkUrl = String.Empty;
        public string CreateUCRUrl = String.Empty;
        public string crVin = string.Empty;
        public string crInvID = string.Empty;
        public string crYear = string.Empty;
        public string crMake = string.Empty;
        public string crModel = string.Empty;
        public string crBody = string.Empty;
        public string crPrice = string.Empty;
        public string crMileage = string.Empty;
        public string crExtCol = string.Empty;
        public string crIntCol = string.Empty;
        DataTable dtOldValue = new DataTable();
        #endregion

        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["MasterPageNoLeftPanel"])))
                    this.MasterPageFile = Convert.ToString(Session["MasterPageNoLeftPanel"]);
                else
                    Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(Request.Url.ToString()));
            }
            catch (Exception ex)
            {
                Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(Request.Url.ToString()));
            }
        }


        #region [Page Load Event]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    if (Request.QueryString["Code"] != null && Request.QueryString["Code"].ToString() != "")
                    {
                        Util.Validate_QueryString_Value(6, Request.QueryString["Code"].ToString(), Constant.OrgID); // EntityTypeID=6 is for Inventory
                    }
                }
                catch { }
            }

            //Set OrgID context key of AutoCompleteExtender from Session
            txtComeBackDealer_AutoCompleteExtender.ContextKey = Convert.ToString(Constant.OrgID);

            UCRlinkUrl = ConfigurationManager.AppSettings["UCRlinkUrl"];
            CreateUCRUrl = ConfigurationManager.AppSettings["CreateUCRUrl"];

            //Check Logged-In User Page Level Permission
            CheckUserPagePermissions();
            //if (hdnSetViewState.Value == "0")
            //{
            //    hdSelDealerId.Value = "-1";
            //}
            //else
            //{

            //    hdSelDealerId.Value = Convert.ToString(ViewState["VSCustomerID"]);
            //}

            #region [Set InventoryId by reading from QueryString]
            //Set InventoryId by reading from QueryString
            if (Request["Code"] != null)
            {
                try
                {
                    this._code = Convert.ToInt32(Request["Code"]);
                    //Insert records in Activity Stats table
                    ActivityStat objActStats = new ActivityStat();
                    objActStats.ActivityLookUpID = Convert.ToInt32(Constant.ActivityLookup.InventoryAccessed);
                    objActStats.RefKey = Request["Code"];
                    objActStats.TableID = 2;//TODO:Remove hard coaded value
                    objActStats.ColumnID = 1;//TODO:Remove hard coaded value
                    objActStats.UserID = Constant.UserId;
                    ActivityBAL actBal = new ActivityBAL();
                    actBal.InsertActivityStats(objActStats);
                }
                catch { }
            }
            #endregion

            #region Handle Page IsPost Back Event
            if (!IsPostBack)
            {
                //Do Nothing if Invalid Inventory Code
                if (_code == -1)
                    return;
                if (Request.UrlReferrer != null)
                    HyperLink1.NavigateUrl = HyperLink2.NavigateUrl = Request.UrlReferrer.ToString();

                //Change LaneAssignment Button text if it is from AfterSales Screen
                if (!string.IsNullOrEmpty(Request.QueryString["ReturnURL"]))
                {
                    if (Request.QueryString["ReturnURL"] == "AfterSalesInventories.aspx")
                        btnBackLaneAssignment.Text = "After Sales";
                }

                //Make Redirect To LaneAssignment Page Button Visibility to True if ReturnUrl is passed 
                if (!string.IsNullOrEmpty(Request.QueryString["ReturnURL"]))
                    btnBackLaneAssignment.Visible = true;
                else
                    btnBackLaneAssignment.Visible = false;


                //By Default Disable All Inventory Buttons
                MoveInventoryButtons(false, BUTTON_DISABLE_CSS);

                //Disable All Edit Buttons,By Default, if InventoryStatus=Archive
                if (InventoryBAL.CarStatusSelect(_code) == 3)
                    EnableDisableEditButtons(false);

                else
                    EnableDisableEditButtons(true);

                ///*Get Inventory No, for which records has to be displayed in different tables*/
                if (!string.IsNullOrEmpty(Request.QueryString["Code"]) & _code != -1)
                {
                    //Get all sections data to be displayed in read only mode
                    BindAllSections(_code);

                    //Bind all sections data to be displayed in editable mode
                    BindAllSectionsInEditMode(_code);

                    //Load Comeback Details
                    LoadComeBackDetails();

                }

                //Check if Logged in User belongs to Admin group
                LoginBLL objLogin = new LoginBLL();
                if (objLogin.GetSingleValue(Constant.UserId))
                {
                    imgSystem.Visible = true;
                }
                else
                    imgSystem.Visible = false;

                // Image slide show
                PreInventoryBAL PreInvBAL = new PreInventoryBAL();
                long PreInvID = PreInvBAL.PreInv_ByInvID(_code);
                //ifImgSlideShow.Attributes.Add("src", String.Format("ImageGallery.aspx?id={0}", PreInvID));
                String CRId = PreInvBAL.CRID_ByInvID(_code);
                hfCRID.Value = Convert.ToString(CRId);

            }
            #endregion

            //Check is Buyer associated against current inventory
            DropDownList ddlBuyer = frmViewEditDealerDetails.FindControl("ddlBuyer") as DropDownList;
            if (!string.IsNullOrEmpty(ddlBuyer.SelectedValue) && ddlBuyer.SelectedValue != "0")
                isBuyerAssociated = true;
            else
                isBuyerAssociated = false;

            //Disable buyer commission textbox, if buyer not enabled
            if (!isBuyerAssociated)
                txtChargeBackComm.Enabled = false;


            //I moved this line from outside ispost back, to referesh each time
            //Show Inventory Drivers,Documents,Notes,Expenses Counts For this inventory

            DisplayCounts();

            //If inventory rulings fulfilled change inventory status
            ChangeInventoryStatus();
            if (Convert.ToString(Session["LoginEntityTypeID"]) == "2" || Convert.ToString(Session["LoginEntityTypeID"]) == "1")
            {
                btnCreateUcr.Visible = false;
                btnEditCarDetails.Visible = false;
                btnEditDealerDetails.Visible = false;
                imgbtnEditSoldToDetails.Visible = false;
                imgbtnEditCarProp.Visible = false;
                btnMoveToInv.Enabled = false;
                btnMoveToArch.Enabled = false;
                btnMoveToOnHand.Enabled = false;
                btnBillofSale.Enabled = false;
                btnBillofSale_PDF.Enabled = false;
                btnChangeUcr.Visible = false;

            }
            if (Convert.ToString(Session["LoginEntityTypeID"]) == "1")
            {
                frmSoldTo.Visible = false;
                dvSoldTo.Visible = false;
                divSystem.Visible = false;
                SystemDiv.Visible = false;
                btnMoveToInv.Visible = false;
                btnMoveToArch.Visible = false;
                btnMoveToOnHand.Visible = false;
                btnBillofSale.Visible = false;
                btnBillofSale_PDF.Visible = false;
                btnChangeUcr.Visible = false;
                ibtnLaneHistory.Visible = false;
                ibtnAudit.Visible = false;
            }
        }
        #endregion

        #region [Show Inventory Drivers,Documents,Notes,Expenses Counts For this inventory]
        private void DisplayCounts()
        {
            if (Convert.ToString(Session["LoginEntityTypeID"]) == "1")
            {
                lnkExpense.Visible = false;
                lnkNotes.Visible = false;
                lnkDocuments.Visible = false;
                lnkDrivers.Visible = false;
                lnkInvEvents.Visible = false;

            }
            // Show Count for Documents,Notes,Drivers,Expenses by Calling BAL Method
            if ((_code != 0 && _code != -1) && Convert.ToString(Session["LoginEntityTypeID"]) != "1")
            {

                String ParentEntityID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString();
                InventoryBAL bal = new InventoryBAL();

                //if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
                //{
                //    DataTable Count = bal.Count_ExpInvDocDriver(_code, Convert.ToInt32(Session["empId"]), Convert.ToInt32(ParentEntityID), Convert.ToInt32(Session["LoginEntityTypeID"]));
                //    lnkExpense.Text = "Inventory Expenses ( " + Count.Rows[0]["TotalExpense"].ToString() + " ) ";
                //    lnkNotes.Text = "Inventory Notes ( " + Count.Rows[0]["TotalNote"].ToString() + " ) ";
                //    lnkDocuments.Text = "Inventory Documents ( " + Count.Rows[0]["TotalDocument"].ToString() + " ) ";
                //    lnkDrivers.Text = "Inventory Drivers ( " + Count.Rows[0]["TotalDrivers"].ToString() + " ) ";
                //}
                //else
                //{
                List<Count_ExpInvDocDriverResult> Count = bal.Count_ExpInvDocDriver(_code);
                lnkExpense.Text = "Inventory Expenses ( " + Count[0].TotalExpense + " ) ";
                lnkNotes.Text = "Inventory Notes ( " + Count[0].TotalNote + " ) ";
                lnkDocuments.Text = "Inventory Documents ( " + Count[0].TotalDocument + " ) ";
                lnkDrivers.Text = "Inventory Drivers ( " + Count[0].TotalDrivers + " ) ";

                // }
            }
        }
        #endregion

        #region[PagePermission]
        public void CheckUserPagePermissions()
        {
            System.Collections.Generic.List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, PAGE);

            //STEP1: Check If any permission found for this page 
            if (dict == null || dict.Count < 1)
                Response.Redirect("Permission.aspx?MSG=INVENTORY.EDIT & INVENTORY.VIEW");

            //Check Delete Right
            if (!dict.Contains(PAGERIGHT_DELETE))
                btnDeleteInventory.Visible = false;
            else
                btnDeleteInventory.Visible = true;

            //STEP2: Disable All Edit buttons, if User Has not INVENTORY.EDIT Right (Only View Right)
            if (!dict.Contains(PAGERIGHT_EDIT))
            {
                EnableDisableEditButtons(false);
            }
            else
            {
                return;
            }



            //STEP3: Check For Car Details Section: EDIT & VIEW RIGHT  & "VIEW RIGHT"

            if (!dict.Contains(INVENTORY_CARDETAILS_VIEW) && !dict.Contains(INVENTORY_CARDETAILS_EDIT))
            {
                //Hide Section
                dvCarDetails.Visible = false;
                frmCarDetails.Visible = false;
            }
            else if (dict.Contains(INVENTORY_CARDETAILS_VIEW))
            {
                //Disable Edit Button
                btnEditCarDetails.Enabled = false;
            }

            //STEP4: Check For Car Properties Section: EDIT & VIEW RIGHT  & "VIEW RIGHT"
            if (!dict.Contains(INVENTORY_CARPROPERTY_VIEW) && !dict.Contains(INVENTORY_CARPROPERTY_EDIT))
            {
                //Hide Section
                dvCarProp.Visible = false;
                frmCarProp.Visible = false;
            }
            else if (dict.Contains(INVENTORY_CARPROPERTY_VIEW))
            {
                //Disable Edit Buttons
                imgbtnEditCarProp.Visible = false;
            }

            //STEP5: Check For DEALER DETAILS Section: EDIT & VIEW RIGHT  & "VIEW RIGHT"
            if (!dict.Contains(INVENTORY_DEALERDETAILS_VIEW) && !dict.Contains(INVENTORY_DEALERDETAILS_EDIT))
            {
                //Hide Section
                dvDealerDet.Visible = false;
                frmDealerDetails.Visible = false;
            }
            else if (dict.Contains(INVENTORY_DEALERDETAILS_VIEW))
            {
                //Disable Edit Buttons
                btnEditDealerDetails.Visible = false;
            }

            //STEP5: Check For SOLDTO Section: EDIT & VIEW RIGHT  & "VIEW RIGHT"
            if (!dict.Contains(INVENTORY_SOLDTO_VIEW) && !dict.Contains(INVENTORY_SOLDTO_EDIT))
            {
                //Hide Section
                dvSoldTo.Visible = false;
                frmSoldTo.Visible = false;
            }
            else if (dict.Contains(INVENTORY_SOLDTO_VIEW))
            {
                //Disable Edit Buttons
                imgbtnEditSoldToDetails.Visible = false;
            }

            //STEP6: Check For LINKED CAR Section: "VIEW RIGHT"
            if (!dict.Contains(INVENTORY_LINKEDCARS_VIEW))
            {
                //Hide Section
                dvLinkedCars.Visible = false;
                gvLinkedFilter.Visible = false;
            }

        }
        #endregion

        #region [Load Come backDetails, If any and make all fields read only, if comeback details entered]
        private void LoadComeBackDetails()
        {
            List<string> lstComebackData = InventoryBAL.ComebackDetailSelect(_code);
            if (lstComebackData.Count < 1)
                return;

            if (ddlComebackStatus.Items.FindByValue(lstComebackData[6]) != null)
                ddlComebackStatus.SelectedValue = lstComebackData[6];

            //Bind ComeBack Reason DropDownList
            InventoryBAL objBAL = new InventoryBAL();
            ddlComeBackReason.DataSource = objBAL.GetComeBackReason();
            ddlComeBackReason.DataBind();

            if (ddlComeBackReason.Items.FindByValue(lstComebackData[5]) != null)
                ddlComeBackReason.SelectedValue = lstComebackData[5];

            txtComeBackDate.Text = lstComebackData[2];
            txtMileageIn.Text = lstComebackData[4];
            txtComeBackDealer.Text = lstComebackData[8];
            txtChargeBackComm.Text = lstComebackData[0];
            txtComeBackComments.Text = lstComebackData[1];


        }
        #endregion

        #region[Set Inventory Status ONHAND/ARCHIEVE/INVENTORY]
        public string InventoryStatus { get; set; }
        #endregion

        #region [Change Inventory Status]
        // Display Inventory Popups if Conditions evaluated to True
        private void ChangeInventoryStatus()
        {

            //Check Whether CAR Can be "MOVE TO INVENTORY" & Not already Status "INVENTORY":1
            if (InventoryBAL.CanMoveToInventory(_code) && InventoryBAL.CarStatusSelect(_code) != 1)
            {
                //Enable Move to Inventory Button
                btnMoveToInv.Enabled = true;
                btnMoveToInv.CssClass = BUTTON_ENABLE_CSS;
            }
            else
            {
                btnMoveToInv.Enabled = false;
                btnMoveToInv.CssClass = BUTTON_DISABLE_CSS;
            }

            //Check Whether CAR Can be "MOVE TO ONHAND" & Not already Status "ONHAND":2
            List<string> lstSoldStatus = InventoryBAL.InventorySoldStatus(_code);
            if (lstSoldStatus.Count < 1)
                return;

            if (lstSoldStatus[0].ToUpper() != "0" && InventoryBAL.CarStatusSelect(_code) != 2)
            {
                InventoryStatus = INVENTORY_ONHAND;
                btnMoveToOnHand.Enabled = true;
                btnMoveToOnHand.CssClass = BUTTON_ENABLE_CSS;
            }
            else
            {
                btnMoveToOnHand.Enabled = false;
                btnMoveToOnHand.CssClass = BUTTON_DISABLE_CSS;
            }


            //Check Whether CAR Can be "MOVE TO ARCHIEVE" & Not already Status "ARCHIEVE":3
            if (InventoryBAL.CanMoveToArchieve(_code) && InventoryBAL.CarStatusSelect(_code) != 3)
            {
                InventoryStatus = INVENTORY_ARCHIEVE;
                btnMoveToArch.Enabled = true;
                btnMoveToArch.CssClass = BUTTON_ENABLE_CSS;
            }
            else
            {
                btnMoveToArch.Enabled = false;
                btnMoveToArch.CssClass = BUTTON_DISABLE_CSS;

            }
            //Sold Status 2 is for Sold Not Paid,Added By Nitin Paliwal,Feb 03 2010.
            //if Sold Status "Yes" or "Sold Not Paid", Display Bill of Sale Report by enabling this button.
            if (lstSoldStatus[0] == "1" || lstSoldStatus[0] == "2")
            {
                btnBillofSale.Enabled = btnBillofSale_PDF.Enabled = true;
                btnBillofSale.CssClass = BUTTON_ENABLE_CSS;
            }
            else
            {
                btnBillofSale.Enabled = btnBillofSale_PDF.Enabled = false;
                btnBillofSale.CssClass = BUTTON_DISABLE_CSS;
            }
        }
        #endregion

        #region[Update Comeback Detail for the Inventory]
        protected void btnComeBackSave_Click(object sender, EventArgs e)
        {
            //Get Control Values for Adding Comeback Details in Inventory table
            int comeBackReasonId = 0;
            long mileageIn = 0;
            DateTime? comeBackDate = null;
            decimal chargebackCommission = 0;
            bool comebackStatus = false;
            //Fetch values of Controls for update in db

            if (!string.IsNullOrEmpty(ddlComebackStatus.SelectedValue))
                comebackStatus = Convert.ToBoolean(ddlComebackStatus.SelectedValue);

            if (!string.IsNullOrEmpty(ddlComeBackReason.SelectedValue))
                comeBackReasonId = Convert.ToInt32(ddlComeBackReason.SelectedValue);

            if (!string.IsNullOrEmpty(txtComeBackDate.Text))
                comeBackDate = Convert.ToDateTime(txtComeBackDate.Text);

            if (!string.IsNullOrEmpty(txtMileageIn.Text))
                mileageIn = Convert.ToInt64(txtMileageIn.Text);

            //if (!string.IsNullOrEmpty(txtChargeBackComm.Text) && txtChargeBackComm.Enabled)
            //    chargebackCommission = Convert.ToDecimal(txtChargeBackComm.Text);

            //else
            //Nitin Paliwal/Oct 27 2009
            //Change Request:711 Hide chargeback commission and make comebackpopup free for editing.
            chargebackCommission = -1;
            //tell sp that No entry required in expense table
            //as user might wants to update comeback comments only(as this field is always enabled)                                


            #region [Rupendra 28 Nov 12 Insert in Table History]

            System.Collections.ArrayList arraylist = new ArrayList();
            if (Request.QueryString["Code"] != null)
            {
                Int64 InventoryId = Convert.ToInt64(Request.QueryString["Code"]);
                arraylist = BAL.InventoryBAL.CompleteInventoryInfoByInventoryID(InventoryId);
                if (arraylist.Count > 0)
                {
                    dtOldValue = arraylist[3] as DataTable;
                    if (dtOldValue.Rows.Count > 0)
                    {
                        int ret;
                        string ComebackOld = string.Empty;
                        string ComebackNew = string.Empty;
                        if (dtOldValue.Rows.Count > 0)
                        {

                            ComebackOld = Convert.ToString(dtOldValue.Rows[0]["ComeBackStatus"]);

                            if (ComebackOld.ToLower() == "yes")
                                ComebackOld = "1";
                            else
                                ComebackOld = "0";

                            string ddlValue;
                            if (ddlComebackStatus.SelectedValue.ToLower() == "true")
                                ddlValue = "1";
                            else
                                ddlValue = "0";

                            if (ddlValue != ComebackOld)
                            {
                                ComebackNew = ddlValue;
                            }
                            else
                            {
                                ComebackOld = "";
                                ComebackNew = "";
                            }

                            InventoryBAL objInventoryBAL = new InventoryBAL();
                            ret = objInventoryBAL.SaveInventoryHistory(InventoryId, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ComebackOld, ComebackNew, Constant.UserId, "InventoryDetails");
                        }
                    }
                }

            }

            #endregion
            long ComeBackDealerId = 0;

            //Get Comeback Dealer from Hidden Field
            if (!string.IsNullOrEmpty(hdComeBackDealer.Value))
                ComeBackDealerId = Convert.ToInt64(hdComeBackDealer.Value);


            long UserId = Constant.UserId;
            string strOutErrorMessage = string.Empty;
            InventoryBAL.UpdateComeBackDetails(comebackStatus, comeBackReasonId, comeBackDate,
                mileageIn, ComeBackDealerId, chargebackCommission, txtComeBackComments.Text.Trim(), _code
                , UserId, DateTime.Now, null, null, null, null, true, ref strOutErrorMessage);

            //Set Error Message, if buyer id is not associated
            if (!string.IsNullOrEmpty(strOutErrorMessage))
            {
                Master.PageMessage = strOutErrorMessage;
                return;
            }

            //Bind All Sections  
            BindAllSections(_code);
            BindAllSectionsInEditMode(_code);

        }
        #endregion

        #region[Edit car details - Databound]
        // Handle DataBound event of CarDetails Formview control for selection of dropdownlists with user selected values from db
        protected void frmCarDetailsUpdate_DataBound(object sender, EventArgs e)
        {
            FormViewRow row = frmCarDetailsUpdate.Row;
            if (row == null)
                return;
            if (row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlYear = frmCarDetailsUpdate.Row.FindControl("ddlYear") as DropDownList;
                DropDownList ddlMake = frmCarDetailsUpdate.Row.FindControl("ddlMake") as DropDownList;
                DropDownList ddlGrade = frmCarDetailsUpdate.Row.FindControl("ddlGrade") as DropDownList;
                DropDownList ddlModel = frmCarDetailsUpdate.Row.FindControl("ddlModel") as DropDownList;
                DropDownList ddlBody = frmCarDetailsUpdate.Row.FindControl("ddlBody") as DropDownList;
                DropDownList ddlEngine = frmCarDetailsUpdate.Row.FindControl("ddlEngine") as DropDownList;
                DropDownList ddlWheelDrive = frmCarDetailsUpdate.Row.FindControl("ddlWheelDrive") as DropDownList;
                DropDownList ddlTrans = frmCarDetailsUpdate.Row.FindControl("ddlTrans") as DropDownList;
                DropDownList ddlExtCol = frmCarDetailsUpdate.Row.FindControl("ddlExtCol") as DropDownList;
                DropDownList ddlIntCol = frmCarDetailsUpdate.Row.FindControl("ddlIntCol") as DropDownList;
                DropDownList ddlBadCarFax = frmCarDetailsUpdate.Row.FindControl("ddlBadCarFax") as DropDownList;
                DropDownList ddlVehiclePresent = frmCarDetailsUpdate.Row.FindControl("ddlVehiclePresent") as DropDownList;
                //InventoryBAL obj = new InventoryBAL();
                Common objCommon = new Common();
                CarDetailsSelectResult result = InventoryBAL.GetCarDetail(_code);

                if (result == null)
                    return;

                //Making below dropdownlists Optional By Providing one blank value in each of them
                ddlTrans.Items.Insert(0, "");
                ddlWheelDrive.Items.Insert(0, "");
                ddlEngine.Items.Insert(0, "");


                //Bind Year
                ddlYear.DataSource = Common.GetYearList();
                ddlYear.DataBind();

                //Find Year and make selected value if found
                if (ddlYear.Items.FindByValue(Convert.ToString(result.Year)) != null)
                    ddlYear.SelectedValue = Convert.ToString(result.Year);

                //Fill grade DDL and make selected value if found
                DataTable dtGrades = InventoryBAL.FetchGrades();
                if (dtGrades.Rows.Count > 0)
                {
                    ddlGrade.Items.Clear();
                    ddlGrade.DataSource = dtGrades;
                    ddlGrade.DataTextField = "Id";
                    ddlGrade.DataValueField = "Id";
                    ddlGrade.DataBind();
                    ListItem li = new ListItem("", "-1");
                    ddlGrade.Items.Insert(0, li);
                }

                if (ddlGrade.Items.FindByValue(Convert.ToString(result.Grade)) != null)
                    ddlGrade.SelectedValue = Convert.ToString(result.Grade);

                ddlBadCarFax.SelectedValue = result.BadCarFax == null ? "-1" : (result.BadCarFax == true ? "1" : "0");
                //Add Generic Colors
                AddGenericColors();

                //Fill Make
                if (!string.IsNullOrEmpty(ddlYear.SelectedValue))
                    BindMake(Convert.ToInt32(ddlYear.SelectedValue), true);



                //Find Generic Exterior Color and Set Value from db(if generic color it will be set)
                if (ddlExtCol.Items.FindByValue(Convert.ToString(result.ExtColorId)) != null)
                    ddlExtCol.SelectedValue = Convert.ToString(result.ExtColorId);

                //Find Generic Interior Color and Set Value from db(if generic color it will be set)
                if (ddlIntCol.Items.FindByValue(Convert.ToString(result.IntColorId)) != null)
                    ddlIntCol.SelectedValue = Convert.ToString(result.IntColorId);


                //Find Trans
                if (ddlTrans.Items.FindByValue(Convert.ToString(result.TransId)) != null)
                    ddlTrans.SelectedValue = Convert.ToString(result.TransId);

                //Find Engine
                if (ddlEngine.Items.FindByValue(Convert.ToString(result.EngineId)) != null)
                    ddlEngine.SelectedValue = Convert.ToString(result.EngineId);


                //Find Wheel Drive and set value from db
                if (ddlWheelDrive.Items.FindByValue(Convert.ToString(result.WheelDriveId)) != null)
                    ddlWheelDrive.SelectedValue = Convert.ToString(result.WheelDriveId);

                //Find Vehicle Present and set value from db
                if (ddlVehiclePresent.Items.FindByValue(Convert.ToString(result.VehiclePresent)) != null)
                    ddlVehiclePresent.SelectedValue = Convert.ToString(result.VehiclePresent);


                //Set Arrival Date to Current date as a default date if Vehicle Present="True"
                TextBox txtArrivalDate = frmCarDetailsUpdate.FindControl("txtArrivalDate") as TextBox;

                //Set Arrival Date if Vehicle Is Present and arrival date not empty or null
                if (ddlVehiclePresent.SelectedValue == "True" && string.IsNullOrEmpty(txtArrivalDate.Text))
                    txtArrivalDate.Text = DateTime.Today.ToShortDateString();

            }
        }
        #endregion

        #region[Save Updated Car Details]
        protected void btnEditPopCarDet_Click(object sender, EventArgs e)
        {



            Inventory objInventory = new Inventory();
            // InventoryBAL InventoryBAL = new InventoryBAL();
            Button obj = (Button)sender;

            //Find Controls & Fetch Values provided by user to make updates in db
            FormViewRow frmrow = frmCarDetailsUpdate.Row;
            TextBox txtVinNo = frmrow.FindControl("txtVinNo") as TextBox;
            DropDownList ddlYear = frmrow.FindControl("ddlYear") as DropDownList;
            TextBox txtMileageIn = frmrow.FindControl("txtMileageIn") as TextBox;
            DropDownList ddlModel = frmrow.FindControl("ddlModel") as DropDownList;
            DropDownList ddlGrade = frmrow.FindControl("ddlGrade") as DropDownList;

            DropDownList ddlMake = frmrow.FindControl("ddlMake") as DropDownList;
            TextBox txtNote = frmrow.FindControl("txtNote") as TextBox;
            DropDownList ddlBody = frmrow.FindControl("ddlBody") as DropDownList;
            DropDownList ddlEngine = frmrow.FindControl("ddlEngine") as DropDownList;
            DropDownList ddlVehiclePresent = frmrow.FindControl("ddlVehiclePresent") as DropDownList;
            DropDownList ddlWheelDrive = frmrow.FindControl("ddlWheelDrive") as DropDownList;
            DropDownList ddlTrans = frmrow.FindControl("ddlTrans") as DropDownList;
            DropDownList ddlIntCol = frmrow.FindControl("ddlIntCol") as DropDownList;
            DropDownList ddlExtCol = frmrow.FindControl("ddlExtCol") as DropDownList;
            DropDownList ddlBadCarFax = frmrow.FindControl("ddlBadCarFax") as DropDownList;
            TextBox ddlCarLocation = frmrow.FindControl("ddlCarLocation") as TextBox;
            TextBox txtArrivalDate = frmrow.FindControl("txtArrivalDate") as TextBox;
            TextBox txtExoticLaneNo = frmrow.FindControl("txtExoLaneNumPopUp") as TextBox;
            TextBox txtRegLaneNo = frmrow.FindControl("txtRegLaneNoPopUp") as TextBox;
            TextBox txtOnlineLaneNo = frmrow.FindControl("txtOnlineLanPopUp") as TextBox;
            TextBox txtVirLaneNoPopUp = frmrow.FindControl("txtVirLaneNoPopUp") as TextBox;
            TextBox txtDEquipment = frmrow.FindControl("txtDEquipment") as TextBox;



            //Put all Values in Inventory table Collection object
            objInventory.InventoryId = _code;
            objInventory.VIN = txtVinNo.Text;

            if (!string.IsNullOrEmpty(txtMileageIn.Text))
                objInventory.MileageIn = Convert.ToInt64(txtMileageIn.Text);

            if (!string.IsNullOrEmpty(txtArrivalDate.Text.Trim()))
                objInventory.ArrivalDate = Convert.ToDateTime(txtArrivalDate.Text);

            objInventory.CarNote = txtNote.Text;

            //Check all dropdown list, if not Null/Empty Set Values in Collection
            if (!string.IsNullOrEmpty(ddlYear.SelectedValue))
                objInventory.Year = Convert.ToInt32(ddlYear.SelectedValue);

            if (!string.IsNullOrEmpty(ddlMake.SelectedValue))
                objInventory.MakeId = Convert.ToInt64(ddlMake.SelectedValue);

            if (!string.IsNullOrEmpty(ddlModel.SelectedValue))
                objInventory.ModelId = Convert.ToInt32(ddlModel.SelectedValue);

            if (!string.IsNullOrEmpty(ddlBody.SelectedValue))
                objInventory.BodyId = Convert.ToInt64(ddlBody.SelectedValue);

            if (!string.IsNullOrEmpty(ddlExtCol.SelectedValue))
                objInventory.ExtColorId = Convert.ToInt32(ddlExtCol.SelectedValue);

            if (!string.IsNullOrEmpty(ddlIntCol.SelectedValue))
                objInventory.IntColorId = Convert.ToInt32(ddlIntCol.SelectedValue);

            if (!string.IsNullOrEmpty(ddlEngine.SelectedValue))
                objInventory.EngineId = Convert.ToInt32(ddlEngine.SelectedValue);

            if (!string.IsNullOrEmpty(ddlTrans.SelectedValue))
                objInventory.TransId = Convert.ToInt32(ddlTrans.SelectedValue);

            if (!string.IsNullOrEmpty(ddlWheelDrive.SelectedValue))
                objInventory.WheelDriveId = Convert.ToInt32(ddlWheelDrive.SelectedValue);

            // if (!string.IsNullOrEmpty(ddlCarLocation.SelectedValue))
            objInventory.CarLocation = ddlCarLocation.Text;


            if (!string.IsNullOrEmpty(ddlVehiclePresent.SelectedValue))
                objInventory.VehiclePresent = Convert.ToBoolean(ddlVehiclePresent.SelectedValue);

            if (ddlBadCarFax.SelectedValue != "-1")
                objInventory.BadCarFax = Convert.ToBoolean(Convert.ToInt32(ddlBadCarFax.SelectedValue));
            //Set Lanes Values

            objInventory.RegularLane = txtRegLaneNo.Text;
            objInventory.ExoticLane = txtExoticLaneNo.Text;
            objInventory.VirtualLane = txtVirLaneNoPopUp.Text;
            objInventory.OnlineLane = txtOnlineLaneNo.Text;

            objInventory.DesignatedEquipment = txtDEquipment.Text.Trim();

            //Set Section UpdatedBy & Updated Date for History Update
            objInventory.DateModified = DateTime.Now;
            objInventory.ModifiedBy = Constant.UserId;

            if (ddlGrade.SelectedValue != "-1")
                objInventory.Grade = Convert.ToInt32(ddlGrade.SelectedValue);
            else
                objInventory.Grade = null;




            #region [Rupendra 28 Nov 12 Insert in Table History]

            System.Collections.ArrayList arraylist = new ArrayList();
            if (Request.QueryString["Code"] != null)
            {
                Int64 InventoryId = Convert.ToInt64(Request.QueryString["Code"]);
                arraylist = BAL.InventoryBAL.CompleteInventoryInfoByInventoryID(InventoryId);
                if (arraylist.Count > 0)
                {
                    dtOldValue = arraylist[0] as DataTable;
                    if (dtOldValue.Rows.Count > 0)
                    {
                        int ret;
                        string CarNoteOld = string.Empty;
                        string CarNoteNew = string.Empty;
                        string AutocheckOld = string.Empty;
                        string AutocheckNew = string.Empty;
                        string CarFaxOld = string.Empty;
                        string CarFaxNew = string.Empty;
                        if (dtOldValue.Rows.Count > 0)
                        {
                            if ((!string.IsNullOrEmpty(txtNote.Text)) && (Convert.ToString(dtOldValue.Rows[0]["CarNote"]) != txtNote.Text))
                            {
                                CarNoteNew = txtNote.Text;
                                CarNoteOld = Convert.ToString(dtOldValue.Rows[0]["CarNote"]);
                            }
                            else if ((string.IsNullOrEmpty(txtNote.Text)) && (Convert.ToString(dtOldValue.Rows[0]["CarNote"]) != txtNote.Text))
                            {
                                CarNoteNew = txtNote.Text;
                                CarNoteOld = Convert.ToString(dtOldValue.Rows[0]["CarNote"]);
                            }
                            else
                            {
                                CarNoteNew = "";
                                CarNoteOld = "";
                            }

                            CarFaxOld = Convert.ToString(dtOldValue.Rows[0]["BadCarFax"]);
                            AutocheckOld = Convert.ToString(dtOldValue.Rows[0]["BadAutoCheck"]);

                            if (string.IsNullOrEmpty(Convert.ToString(dtOldValue.Rows[0]["BadCarFax"])))
                                CarFaxOld = "-1";
                            else if (CarFaxOld.ToLower() == "true")
                                CarFaxOld = "1";
                            else
                                CarFaxOld = "0";
                            if (AutocheckOld.ToLower() == "true")
                                AutocheckOld = "1";
                            else
                                AutocheckOld = "0";


                            if (ddlBadCarFax.SelectedValue != CarFaxOld)
                            {
                                CarFaxNew = ddlBadCarFax.SelectedValue;
                            }
                            else
                            {
                                CarFaxOld = "";
                                CarFaxNew = "";
                            }

                            InventoryBAL objInventoryBAL = new InventoryBAL();
                            ret = objInventoryBAL.SaveInventoryHistory(InventoryId, CarNoteOld, CarNoteNew, CarFaxOld, CarFaxNew, string.Empty, string.Empty, string.Empty, string.Empty, Constant.UserId, "InventoryDetails");
                        }
                    }
                }

            }

            #endregion

            //CALL BAL METHOD TO UPDATE CAR DETAILS
            InventoryBAL.UpdateCarDetails(objInventory);



            #region Change Request:- Please add "Special Case Comments" by Todd Burt,16/07/2010
            //Add Special Case Notes
            TextBox txtSpecialCaseNote = frmrow.FindControl("txtSpecialCaseNote") as TextBox;

            //Check if notes already exist against this inventory
            long specialCasenoteId = InventoryBAL.IsSpecialCaseNoteExists(_code);
            //Update
            if (specialCasenoteId > 0)
            {
                Note objNote = new Note();
                objNote.EntityId = _code;
                objNote.EntityTypeId = (int)EntityTypes.Inventory;
                objNote.Notes = txtSpecialCaseNote.Text.Trim();
                objNote.SecurityUserId = Constant.UserId;
                objNote.ModifiedBy = Constant.UserId;
                objNote.DateModified = DateTime.Now;
                objNote.NoteTypeID = (int)NoteType.SPECIAL_NOTE;
                long NoteId = InventoryBAL.UpdateNotes(objNote, specialCasenoteId);
            }
            //Insert
            else
            {
                if (!string.IsNullOrEmpty(txtSpecialCaseNote.Text.Trim()))
                {
                    Note objNote = new Note();
                    objNote.EntityId = _code;
                    objNote.EntityTypeId = (int)EntityTypes.Inventory;
                    objNote.Notes = txtSpecialCaseNote.Text.Trim();
                    objNote.SecurityUserId = Constant.UserId;
                    objNote.AddedBy = Constant.UserId;
                    objNote.DateAdded = DateTime.Now;
                    objNote.NoteTypeID = (int)NoteType.SPECIAL_NOTE;
                    objNote.IsActive = 1;
                    long NoteId = InventoryBAL.AddNotes(objNote);
                }
            }


            #endregion

            //Bind All Sections
            BindAllSections(_code);
            BindAllSectionsInEditMode(_code);

        }
        #endregion

        #region[Update System]
        protected void btnSystemUpdate_Click(object sender, EventArgs e)
        {
            InventoryBAL obj = new InventoryBAL();

            if (obj.CheckPayemnt(_code) > 0)
            {
                string Script = "alert('You can not change system because other vehicles are associated with it');";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Warning!", Script, true);
            }
            else
            {
                InventoryBAL.UpdateInventorySystem(_code, Convert.ToInt32(ddlSystem.SelectedValue), Constant.UserId, DateTime.Now);
                //Bind All Sections
                BindAllSections(_code);
                BindAllSectionsInEditMode(_code);
            }
        }
        #endregion

        #region[Render]
        /// <summary>
        /// Override Render event for Invalid Page Postback Event Valdidation error and set unique id
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            //CarDetails Gridview
            FormViewRow row = null;
            //Dealer Details
            row = frmViewEditDealerDetails.Row;
            if (row == null)
                return;

            if (row.RowType == DataControlRowType.DataRow)
            {
                Page.ClientScript.RegisterForEventValidation
                        (row.UniqueID + "$ctl00");

            }

            //Sold To
            row = frmEditSoldTo.Row;
            if (row == null)
                return;

            if (row.RowType == DataControlRowType.DataRow)
            {
                Page.ClientScript.RegisterForEventValidation
                        (row.UniqueID + "$ctl00");
            }


            base.Render(writer);
        }
        #endregion

        #region[Bind all section - View Mode]
        private void BindAllSections(long InventoryID)
        {
            System.Collections.ArrayList arraylist = new ArrayList();
            arraylist = BAL.InventoryBAL.CompleteInventoryInfoByInventoryID(_code);
            if (arraylist.Count > 0)
            {
                // Bind car details section
                if (arraylist[0] != null)
                {
                    frmCarDetails.DataSource = arraylist[0];
                    frmCarDetails.DataBind();
                }

                // Bind car property section
                if (arraylist[1] != null)
                {
                    frmCarProp.DataSource = arraylist[1];
                    frmCarProp.DataBind();
                }

                // Bind purchased from section
                if (arraylist[2] != null)
                {
                    frmDealerDetails.DataSource = arraylist[2];
                    frmDealerDetails.DataBind();
                }
                // Bind sold to section
                if (arraylist[3] != null)
                {

                    frmSoldTo.DataSource = arraylist[3];
                    frmSoldTo.DataBind();
                }

                HiddenField hfDupTitleNote = frmDealerDetails.FindControl("hfDupTitleNote") as HiddenField;
                Label lblDupTitleNote = frmDealerDetails.FindControl("lblDupTitleNote") as Label;
                if (hfDupTitleNote != null)
                {
                    if (String.IsNullOrEmpty(hfDupTitleNote.Value))
                        lblDupTitleNote.Visible = false;
                    else
                    {
                        lblDupTitleNote.Visible = true;
                        lblDupTitleNote.Text = "(" + hfDupTitleNote.Value + ")";
                    }

                }
                //Show Last Updated SoldHistory In SoldTo Details Section after SoldStatus ikon
                List<string> lstSoldInfo = InventoryBAL.InventorySoldStatus(_code);
                if (lstSoldInfo.Count < 1)
                    return;
                List<string> lstComebackData = InventoryBAL.ComebackDetailSelect(_code);
                if (lstComebackData.Count < 1)
                    return;

                //Display SoldTo Section History
                Label lblSoldHistory = frmSoldTo.FindControl("lblSoldHistoryLastUpdated") as Label;
                lblSoldHistory.Text = lstSoldInfo[2];

                //Display Comeback Section History
                Label lblComebackLastUpdatedHistory = frmSoldTo.FindControl("lblComebackLastUpdatedHistory") as Label;
                lblComebackLastUpdatedHistory.Text = lstComebackData[7];

                //Fetch and save CustomerID in hidden field
                HiddenField hfCustomerID = frmSoldTo.FindControl("hfCustomerID") as HiddenField;
                hfCustID.Value = hfCustomerID.Value;
            }
            BindLinkedCars(_code);
            ChangeInventoryStatus();
        }
        #endregion

        #region[Linked Cars - Bind grid]
        private void BindLinkedCars(long inventoryId)
        {
            //Check if VinNo provided, Get Linked Cars for provided InventoryId,VIN
            if (!string.IsNullOrEmpty(VIN))
            {
                gvLinkedFilter.DataSource = InventoryBAL.SearchInventoryLinked(inventoryId, VIN, Constant.OrgID);
                gvLinkedFilter.DataBind();
            }
        }
        #endregion

        #region Bind Popup Sections data on Page load
        /// <summary>
        /// Bind Car Details Section Details in Editable Mode
        /// </summary>
        private void BindCarDetailsInEditMode(long inventoryId)
        {
            //Bind formview control with object datsource control
            frmCarDetailsUpdate.DataSource = objGetCarDetails;
            frmCarDetailsUpdate.DataBind();
        }
        /// <summary>
        /// Bind Car Property Section details in Editable mode
        /// </summary>
        private void BindCarPropertyInEditMode(long inventoryId)
        {
            //Fill User Selectd property in pnlPopEditCarProp Panel
            FillSelCarProperty();
        }

        /// <summary>
        /// This method fill selected Car Property when user click open EDIT PopUp Window for Car Properties
        /// </summary>
        private void FillSelCarProperty()
        {
            //Get Selected Values for Car Properties from dictionary collection
            System.Collections.Generic.Dictionary<string, bool> objCarProp =
               InventoryBAL.GetCarPropertiesBool(_code);

            //Check for counts
            if (objCarProp.Count == 0)
                return;

            //Re-Order/Removed Items as Change request made by Claudia
            //Make checkboxes selected for the values from db
            CheckBoxList chkCarProp1 = pnlPopEditCarProp.FindControl("chkCarProp1") as CheckBoxList;
            chkCarProp1.Items[0].Selected = objCarProp["item.AC"];
            chkCarProp1.Items[1].Selected = objCarProp["item.PowerLocks"];
            chkCarProp1.Items[2].Selected = objCarProp["item.PowerWindows"];
            //chkCarProp1.Items[3].Selected = objCarProp["item.AlloyWheels"];

            CheckBoxList chkCarProp2 = pnlPopEditCarProp.FindControl("chkCarProp2") as CheckBoxList;
            //chkCarProp2.Items[0].Selected = objCarProp["item.PowerWindows"];
            chkCarProp2.Items[0].Selected = objCarProp["item.SunMoon"];
            chkCarProp2.Items[1].Selected = objCarProp["item.Leather"];
            chkCarProp2.Items[2].Selected = objCarProp["item.Navigation"];
        }



        /// <summary>
        /// Bind Dealer Details in Editable mode
        /// </summary>
        private void BindDealersInEditMode(long inventoryId)
        {

            frmViewEditDealerDetails.DataSource = InventoryBAL.GetDealerForInventory(inventoryId);
            frmViewEditDealerDetails.DataBind();

            //Select Title Present /Title Shipped
            DealerDetailsSelectResult result = InventoryBAL.GetDealerForInv(inventoryId);

            if (result != null)
            {
                //Set Title Present
                DropDownList ddlTitlePresent = frmViewEditDealerDetails.FindControl("ddlTitlePresent") as DropDownList;

                if (result.TitleP)
                    ddlTitlePresent.SelectedValue = "True";
                else
                    ddlTitlePresent.SelectedValue = "False";



                //Set Title Shpped
                DropDownList ddlTitleShipped = frmViewEditDealerDetails.FindControl("ddlTitleShipped") as DropDownList;

                if (result.TitleS)
                    ddlTitleShipped.SelectedValue = "True";
                else
                    ddlTitleShipped.SelectedValue = "False";


                //Set Dup title
                DropDownList ddlDupTitle = frmViewEditDealerDetails.FindControl("ddlDupTitle") as DropDownList;

                if (result.DupTitle == "Yes")
                    ddlDupTitle.SelectedValue = "1";
                else
                    ddlDupTitle.SelectedValue = "0";

            }
            //Find Controls
            TextBox txtDealerShip = frmViewEditDealerDetails.FindControl("txtDealerShip") as TextBox;
            TextBox txtCost = frmViewEditDealerDetails.FindControl("txtCost") as TextBox;
            TextBox txtCheckNumber = frmViewEditDealerDetails.FindControl("txtCheckNumber") as TextBox;
            LinkButton lnkSelectDealer = frmViewEditDealerDetails.FindControl("lnkSelectDealer") as LinkButton;
            HiddenField hdDealerIdSel = frmViewEditDealerDetails.FindControl("hdDealerIdSel") as HiddenField;
            DropDownList ddlBuyer = frmViewEditDealerDetails.FindControl("ddlBuyer") as DropDownList;


            int checkPaidCount = 0;

            //Disable Delete button, if car is archieved
            if (InventoryBAL.CarStatusSelect(_code) == 3)
                btnDeleteInventory.Visible = false;
            //Enable Delete button, if car is not archived
            // commented following two lines by Naushad on 3/19/2012; This logic is incorrect.
            //      Delete button will only be available if there is permission which was checked earlier.
            //else
            //    btnDeleteInventory.Visible = true;


            //if check is paid then disable Select DealerLink and textbox for dealer 
            if (hdDealerIdSel != null && !string.IsNullOrEmpty(hdDealerIdSel.Value.Trim()))
            {
                long dealerId = Convert.ToInt64(hdDealerIdSel.Value);
                //Check Whether CheckPaid(In expense table)for Entity:dealer, 
                checkPaidCount = InventoryBAL.IsCheckPaid(inventoryId, dealerId, 1, false);

                // If Check Paid Count=1, Disable Selected Dealer Link and textbox & delete Inventory button
                if (checkPaidCount >= 1)
                {
                    //Enable False as check is paid
                    txtDealerShip.Enabled = false;
                    lnkSelectDealer.Enabled = false;
                    //Added Style Attribute as its still appearing on firefox,Nitin Paliwal,Feb 04 2010
                    lnkSelectDealer.Style.Add("display", "none;");
                    //Disable CheckNumber,if check Paid
                    txtCheckNumber.Enabled = false;
                    txtCost.Enabled = false;

                }
                else
                {
                    //Enable Dealer textbox and select dealer link 
                    txtDealerShip.Enabled = true;
                    lnkSelectDealer.Enabled = true;
                    //Added Style Attribute as its still appearing on firefox,Nitin Paliwal,Feb 04 2010
                    lnkSelectDealer.Style.Remove("display");
                    //Enable CheckNumber,if checknot paid against this dealer
                    txtCheckNumber.Enabled = true;
                    txtCost.Enabled = true;
                }
            }

            //Disable Buyer DropDownlist, if Check isPaid
            if (ddlBuyer.SelectedValue != "0")
                checkPaidCount = InventoryBAL.IsCheckPaid(inventoryId, Convert.ToInt64(ddlBuyer.SelectedValue), 2, false);

            //Disable buyer dropdownlist and DeleteInventory Button, if check is paid//
            if (checkPaidCount >= 1)
                ddlBuyer.Enabled = false;
            else
                ddlBuyer.Enabled = true;

            //Disable Delete Inventory Button,if checkpaid = 1 For All Entities,
            //Pass "True" in IsCheckPaid Function for checking all entitytypes
            if (ddlBuyer.Items.Count > 0)
                checkPaidCount = InventoryBAL.IsCheckPaid(inventoryId, Convert.ToInt64(ddlBuyer.SelectedValue), 2, true);
            if (checkPaidCount >= 1)
                btnDeleteInventory.Visible = false;
            // commented following two lines by Naushad on 3/19/2012; This logic is incorrect.
            //      Delete button will only be available if there is permission which was checked earlier.
            //else
            //   btnDeleteInventory.Visible = true;

        }
        /// <summary>
        /// Bind SoldTo Section details in Editable mode
        /// </summary>
        private void BindSoldToInEditMode(long inventoryId)
        {
            frmEditSoldTo.DataSource = InventoryBAL.GetSoldToDetails(inventoryId);
            frmEditSoldTo.DataBind();

            //Get Sold Status for the current inventory being edited
            List<string> lstSoldInfo = InventoryBAL.InventorySoldStatus(_code);

            if (lstSoldInfo.Count > 0)
            {
                //Find Dropdownlist and set Sold Status For Sold Status DropDownList
                DropDownList ddlSoldStatus = frmEditSoldTo.FindControl("ddlSoldStatus") as DropDownList;
                ddlSoldStatus.SelectedValue = lstSoldInfo[0].ToString();
                //if (lstSoldInfo[0].ToUpper() == "TRUE")
                //    ddlSoldStatus.SelectedValue = "True";
                //else
                //    ddlSoldStatus.SelectedValue = "False";
            }

        }

        #endregion

        #region[Is Buyer associated]
        public bool isBuyerAssociated { get; set; }
        #endregion

        #region BindTrans
        /// <summary>
        /// Bind dropdownlist with Trans details
        /// </summary>
        private void BindTrans()
        {
            ///*Bind Trans DropDownlist*/
            //objMasterBAL.GetTransList();
            //ddlTrans.DataSource = Common.GetTransList();
            //ddlTrans.DataTextField = "TransId";
            //ddlTrans.DataValueField = "TransType";
            //ddlTrans.DataBind();
        }
        #endregion

        #region Bind Car Location
        private void BindCarLocation()
        {
            //ddlCarLocation.DataSource = Common.GetCarLocations();
            //ddlCarLocation.DataTextField = "CarLocation";
            //ddlCarLocation.DataValueField = "CarLocationId";
            //ddlCarLocation.DataBind();
        }
        #endregion

        #region Bind Model
        /// <summary>
        /// Bind dropdownlist with Model details based on make selected
        /// </summary>
        private void BindModel(int makeId, bool isDefault)
        {
            ///*Bind Model DropDownlist*/
            //ddlModel.DataSource = Common.GetModel(makeId);
            //ddlModel.DataTextField = "Model";
            //ddlModel.DataValueField = "ModelId";
            //ddlModel.DataBind();


            //if (isDefault)
            //    if (ddlModel.Items.FindByValue(_Model) != null)
            //        ddlModel.SelectedValue = _Model;

            ////Bind Body
            //if (!string.IsNullOrEmpty(ddlModel.SelectedValue))
            //    BindBody(Convert.ToInt32(ddlModel.SelectedValue), isDefault);

            ////Bind Colors for Selected Year,Make,Model,Body
            //BindColors();
        }
        #endregion

        #region Save updated Car Properties details
        protected void carPopPropertyEdit_Click(object sender, EventArgs e)
        {
            //Did Claudia requested changes i.e Re-order/Remove items
            //Get Updated Car Properties values from checkboxes for Saving in db
            CheckBoxList chkCarProp1 = pnlPopEditCarProp.FindControl("chkCarProp1") as CheckBoxList;
            bool AC = chkCarProp1.Items[0].Selected;
            bool PowerLocks = chkCarProp1.Items[1].Selected;
            bool PowerWindows = chkCarProp1.Items[2].Selected;

            //AlloyWheels no more required
            bool AlloyWheels = false;//chkCarProp1.Items[3].Selected;

            CheckBoxList chkCarProp2 = pnlPopEditCarProp.FindControl("chkCarProp2") as CheckBoxList;
            // bool PowerWindows =      chkCarProp2.Items[0].Selected;
            bool SunMoon = chkCarProp2.Items[0].Selected;
            bool Leather = chkCarProp2.Items[1].Selected;
            bool Navigation = chkCarProp2.Items[2].Selected;

            //PowerSeats no more required
            bool PowerSeats = false;// chkCarProp2.Items[3].Selected;

            //Set Section UpdatedBy & Updated Date for History Update
            DateTime sectionUpdatedDate = DateTime.Now;
            long sectionUpdatedBy = Constant.UserId;

            //Call Update method of DAL layer to update Car Property modified by user(if any)
            InventoryBAL.CarPropertyUpdate(_code, AC, PowerWindows, PowerLocks, AlloyWheels,
                Navigation, SunMoon, Leather, PowerSeats, sectionUpdatedBy, sectionUpdatedDate);

            //ReBind to Reflect changes in View Section
            //Get all sections data to be displayed in read only mode
            //BindAllSections(_code);
            BindAllSections(_code);
            BindAllSectionsInEditMode(_code);

        }
        #endregion
        /// <summary>
        /// Handle Click event of Save button of Dealer Details Edit Popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveDealerDetails(object sender, EventArgs e)
        {
            #region Save Updated Dealer Details
            bool TitleShipped = false;
            bool TitlePresent = false;
            int TitleCopyReceived = 0;
            long buyerId = 0;
            int Desig = 0;
            long dealerId = 0;
            int vehicleHistoryReportId = 0;
            DateTime? purchDate = null;
            decimal Cost = decimal.Zero;
            int DupTitle = 0;

            //Find Controls and fetch selected values by a user
            TextBox txtDealerShip = frmViewEditDealerDetails.FindControl("txtDealerShip") as TextBox;
            Label lblDealerId = frmViewEditDealerDetails.FindControl("lblDealerId") as Label;
            TextBox txtPurchaseDate = frmViewEditDealerDetails.FindControl("txtPurchaseDate") as TextBox;
            TextBox txtCost = frmViewEditDealerDetails.FindControl("txtCost") as TextBox;
            TextBox txtCheckNumber = frmViewEditDealerDetails.FindControl("txtCheckNumber") as TextBox;
            TextBox txtTitlePresentNotes = frmViewEditDealerDetails.FindControl("txtTitlePresentNotes") as TextBox;
            TextBox txtTitleShippedNotes = frmViewEditDealerDetails.FindControl("txtTitleShippedNotes") as TextBox;
            DropDownList ddlBuyer = frmViewEditDealerDetails.FindControl("ddlBuyer") as DropDownList;
            DropDownList ddlDesig = frmViewEditDealerDetails.FindControl("ddlDesig") as DropDownList;
            DropDownList ddlTitlePresent = frmViewEditDealerDetails.FindControl("ddlTitlePresent") as DropDownList;
            DropDownList ddlTitleShipped = frmViewEditDealerDetails.FindControl("ddlTitleShipped") as DropDownList;

            //Change Request: June 16 2010,Add Inventory & Dealer fields
            DropDownList ddlVehicleHistoryReport = frmViewEditDealerDetails.FindControl("ddlVehicleHistoryReport") as DropDownList;

            //Change Request: June 17 2010,reported By Todd Burt for "Open deal list report"
            DropDownList ddlTitleCopyReceived = frmViewEditDealerDetails.FindControl("ddlTitleCopyReceived") as DropDownList;

            //Change Request : June 11 2012, Add dup title and dup title note
            DropDownList ddlDupTitle = frmViewEditDealerDetails.FindControl("ddlDupTitle") as DropDownList;
            TextBox txtDupTitleNote = frmViewEditDealerDetails.FindControl("txtDupTitleNote") as TextBox;

            //Find Controls and fetch selected values by a user

            if (!string.IsNullOrEmpty(ddlVehicleHistoryReport.SelectedValue))
                vehicleHistoryReportId = Convert.ToInt32(ddlVehicleHistoryReport.SelectedValue);

            //if (!string.IsNullOrEmpty(ddlTitleCopyReceived.SelectedValue))
            //    TitleCopyReceived = Convert.ToInt32(ddlTitleCopyReceived.SelectedValue);

            if (!string.IsNullOrEmpty(txtCost.Text))
                Cost = Convert.ToDecimal(txtCost.Text.Trim());

            if (!string.IsNullOrEmpty(txtPurchaseDate.Text))
                purchDate = Convert.ToDateTime(txtPurchaseDate.Text);

            if (!string.IsNullOrEmpty(hdSelDealerId.Value))
                dealerId = Convert.ToInt64(hdSelDealerId.Value);


            if (!string.IsNullOrEmpty(ddlBuyer.SelectedValue) && ddlBuyer.Items.Count > 0)
                buyerId = Convert.ToInt64(ddlBuyer.SelectedValue);

            if (!string.IsNullOrEmpty(ddlDesig.SelectedValue))
                Desig = Convert.ToInt32(ddlDesig.SelectedValue);

            if (!string.IsNullOrEmpty(ddlTitleShipped.SelectedValue))
                TitleShipped = Convert.ToBoolean(ddlTitleShipped.SelectedValue);


            if (!string.IsNullOrEmpty(ddlTitlePresent.SelectedValue))
                TitlePresent = Convert.ToBoolean(ddlTitlePresent.SelectedValue);


            if (!string.IsNullOrEmpty(ddlDupTitle.SelectedValue))
                DupTitle = Convert.ToInt32(ddlDupTitle.SelectedValue);

            ////Nullify Check Number, if empty
            //if(string.IsNullOrEmpty(txtCheckNumber.Text.Trim()))
            //    txtCheckNumber.Text=null;

            //Set Section UpdatedBy & Updated Date for History Update
            DateTime sectionUpdatedDate = DateTime.Now;
            long sectionUpdatedBy = Constant.UserId;

            #region[Titlepresent tracking added by Rupendra 1 Feb 2013]
            int ret = 0;
            Int32 TitlePresentValue = 0;
            InventoryBAL objInventoryBAL = new InventoryBAL();
            if ((!string.IsNullOrEmpty(ddlTitlePresent.SelectedValue)) && (ddlTitlePresent.SelectedValue.ToLower() == "true"))
                TitlePresentValue = 1;
            else
                TitlePresentValue = 0;
            ret = objInventoryBAL.UpdateTitlePresentTrack(_code, TitlePresentValue, Constant.UserId);
            #endregion

            //CALL DAL CLASS Method to Update Dealer Details
            InventoryBAL.DealerDetailUpdate(_code, Cost, purchDate,
                 TitlePresent, TitleCopyReceived, TitleShipped, txtTitlePresentNotes.Text.Trim(), txtTitleShippedNotes.Text.Trim(),
                 Desig, dealerId, buyerId, txtCheckNumber.Text.Trim(), sectionUpdatedBy, sectionUpdatedDate, vehicleHistoryReportId, DupTitle, txtDupTitleNote.Text.Trim());


            #endregion

            #region [Update Title Tracking Note]
            TextBox TitleTrackingNote = frmViewEditDealerDetails.FindControl("txtTitleTrackingNote") as TextBox;
            HiddenField hdnNoteID = frmViewEditDealerDetails.FindControl("hdnNoteID") as HiddenField;

            long NoteId = 0;
            Note objNote = new Note();
            objNote.NoteId = Convert.ToInt64(hdnNoteID.Value);
            objNote.EntityId = _code;
            objNote.EntityTypeId = (int)EntityTypes.Inventory;
            objNote.Notes = TitleTrackingNote.Text.Trim();
            objNote.SecurityUserId = Constant.UserId;
            objNote.DateAdded = DateTime.Now;
            objNote.AddedBy = Constant.UserId;
            objNote.ModifiedBy = Constant.UserId;
            objNote.DateModified = DateTime.Now;
            objNote.NoteTypeID = (int)NoteType.TITAL_TRACKING_NOTE;
            objNote.IsActive = 1;
            if (Convert.ToInt64(hdnNoteID.Value) > 0)
                NoteId = InventoryBAL.UpdateNote(objNote, (int)NoteSave_Mode.Update);
            else if (!string.IsNullOrEmpty(TitleTrackingNote.Text))
                NoteId = InventoryBAL.UpdateNote(objNote, (int)NoteSave_Mode.Add);
            #endregion
            //ReBind to Reflect changes in View Section
            //Get all sections data to be displayed in read only mode
            //BindAllSections(_code);
            //BindDealerDetailSection(_code);
            BindAllSections(_code);
            BindAllSectionsInEditMode(_code);

            //Nullify Hidden Field Value
            hdSelDealerId.Value = null;

        }



        /// <summary>
        /// Handle click event of Edit button of SoldTo EDIT POPUP SECTION
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEditPopUpSoldToDetails_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(hdnSetViewState.Value))
            {
                if (hdnSetViewState.Value == "0")
                    hdSelDealerId.Value = "-1";
                else
                    hdSelDealerId.Value = Convert.ToString(ViewState["VSCustomerID"]);
            }
            else if (!String.IsNullOrEmpty(hfCustID.Value))
                hdSelDealerId.Value = hfCustID.Value;
            else
                hdSelDealerId.Value = Convert.ToString(ViewState["VSCustomerID"]);

            #region SAVE SOLD TO SECTION DETAILS
            DateTime? SoldDate = null;
            DateTime? DepositDate = null;

            long MileageOut = 0;
            decimal MarketPrice = decimal.Zero;
            decimal ActualCost = decimal.Zero;
            decimal PriceSold = decimal.Zero;
            decimal depositAmount = decimal.Zero;
            int bankId = 0;
            long CustId = 0;
            //bool comebackStatus = false;
            int SoldStatus = 0;
            TextBox txtcustomerID = frmEditSoldTo.FindControl("txtCustomerName") as TextBox;

            //Set CustomerId From Hidden Field
            if (!String.IsNullOrEmpty(hdSelDealerId.Value))  //(!String.IsNullOrEmpty(hdDealerName.Value) )
                CustId = Convert.ToInt64(hdSelDealerId.Value);
            if (hdSelDealerId.Value == "-1")
                CustId = -1;

            //FIND TEMPLATE CONTROLS to Save Updated SoldTo Section Values
            TextBox txtSoldDate = frmEditSoldTo.FindControl("txtSoldDate") as TextBox;
            TextBox txtMarketPrice = frmEditSoldTo.FindControl("txtMarketPrice") as TextBox;
            // TextBox txtActualCost = frmEditSoldTo.FindControl("txtActualCost") as TextBox;
            TextBox txtPriceSold = frmEditSoldTo.FindControl("txtPriceSold") as TextBox;
            TextBox txtMileageOut = frmEditSoldTo.FindControl("txtMileageOut") as TextBox;
            TextBox txtDepositDate = frmEditSoldTo.FindControl("txtDepositDate") as TextBox;
            TextBox txtDepositAmount = frmEditSoldTo.FindControl("txtDepositAmount") as TextBox;
            TextBox txtDepositComment = frmEditSoldTo.FindControl("txtDepositComment") as TextBox;
            TextBox txtSoldComment = frmEditSoldTo.FindControl("txtSoldComment") as TextBox;

            DropDownList ddlBankName = frmEditSoldTo.FindControl("ddlBankName") as DropDownList;
            DropDownList ddlSoldStatus = frmEditSoldTo.FindControl("ddlSoldStatus") as DropDownList;

            if (!string.IsNullOrEmpty(ddlSoldStatus.SelectedValue))
                SoldStatus = Convert.ToInt32(ddlSoldStatus.SelectedValue);

            if (!string.IsNullOrEmpty(txtSoldDate.Text))
                SoldDate = Convert.ToDateTime(txtSoldDate.Text);

            if (!string.IsNullOrEmpty(txtMarketPrice.Text))
                MarketPrice = Convert.ToDecimal(txtMarketPrice.Text.Trim());


            //if (!string.IsNullOrEmpty(txtActualCost.Text))
            //    ActualCost = Convert.ToDecimal(txtActualCost.Text.Trim());

            if (!string.IsNullOrEmpty(ddlBankName.SelectedValue))
                bankId = Convert.ToInt32(ddlBankName.SelectedValue);

            if (!string.IsNullOrEmpty(txtDepositAmount.Text))
                depositAmount = Convert.ToDecimal(txtDepositAmount.Text.Trim());


            if (!string.IsNullOrEmpty(txtMileageOut.Text.Trim()))
                MileageOut = Convert.ToInt64(txtMileageOut.Text);

            if (!string.IsNullOrEmpty(txtPriceSold.Text.Trim()))
                PriceSold = Convert.ToDecimal(txtPriceSold.Text);

            if (!string.IsNullOrEmpty(txtDepositDate.Text.Trim()))
                DepositDate = Convert.ToDateTime(txtDepositDate.Text);

            //Set Section UpdatedBy & Updated Date for History Update
            DateTime sectionUpdatedDate = DateTime.Now;
            long sectionUpdatedBy = Constant.UserId;




            #region [Rupendra 28 Nov 12 Insert in Table History]

            System.Collections.ArrayList arraylist = new ArrayList();
            if (Request.QueryString["Code"] != null)
            {
                Int64 InventoryId = Convert.ToInt64(Request.QueryString["Code"]);
                arraylist = BAL.InventoryBAL.CompleteInventoryInfoByInventoryID(InventoryId);
                if (arraylist.Count > 0)
                {
                    dtOldValue = arraylist[3] as DataTable;
                    if (dtOldValue.Rows.Count > 0)
                    {
                        int ret;
                        string SoldStatusOld = string.Empty;
                        string SoldStatusNew = string.Empty;


                        if (Convert.ToString(dtOldValue.Rows[0]["SoldStatus"]).ToLower() == "yes")
                            SoldStatusOld = "1";
                        else if (Convert.ToString(dtOldValue.Rows[0]["SoldStatus"]).ToLower() == "no")
                            SoldStatusOld = "0";
                        else
                            SoldStatusOld = "2";


                        if (dtOldValue.Rows.Count > 0)
                        {

                            if (ddlSoldStatus.SelectedValue != SoldStatusOld)
                            {
                                SoldStatusNew = ddlSoldStatus.SelectedValue;
                            }
                            else
                            {
                                SoldStatusOld = "";
                                SoldStatusNew = "";
                            }

                            // InventoryBAL objInventoryBAL = new InventoryBAL();
                            // ret = objInventoryBAL.SaveInventoryHistory(InventoryId, string.Empty, string.Empty, string.Empty, string.Empty, SoldStatusOld, SoldStatusNew, string.Empty, string.Empty, Constant.UserId, "InventoryDetails");
                        }
                    }
                }

            }

            #endregion


            //CALL DAL METHOD TO UPDATE SOLD TO SECTION DETAILS
            InventoryBAL.SoldToUpdate_ver211(_code, MileageOut, SoldDate, MarketPrice, ActualCost,
                 PriceSold, DepositDate, depositAmount, bankId, txtDepositComment.Text.Trim(), SoldStatus, CustId, txtSoldComment.Text.Trim(), sectionUpdatedBy, sectionUpdatedDate);

            //ReBind to Reflect changes in View Section
            BindAllSections(_code);
            BindAllSectionsInEditMode(_code);


            //Reset Hidden Field
            hdSelDealerId.Value = null;
            hdnSetViewState.Value = null;
            #endregion
        }

        /// <summary>
        /// Handle Click event of Image button for fetching dealer name from selected row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnSelect_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtnSelect = (ImageButton)sender;

            GridViewRow row = (GridViewRow)imgbtnSelect.NamingContainer;
            //Set Selected DealerId/CustomerId in Hidden Field for Insertion in db table
            hdSelDealerId.Value = imgbtnSelect.CommandArgument.ToString();
            ViewState["VSCustomerID"] = hdSelDealerId.Value;
            //Set Name of Selected Dealer/Customer in Hidden Field
            hdDealerName.Value = Server.HtmlDecode(row.Cells[2].Text);

            //Find textbox control in EditPopup for Dealer Details and assign name of selected dealer
            TextBox txtDealerShip = frmViewEditDealerDetails.Row.FindControl("txtDealerShip") as TextBox;
            txtDealerShip.Text = hdDealerName.Value;

            //Search Ajax Model Popup Control & Hide Model PopUp
            AjaxControlToolkit.ModalPopupExtender MPESelectDealer = frmViewEditDealerDetails.Row.FindControl("MPESelectDealer") as AjaxControlToolkit.ModalPopupExtender;
            MPESelectDealer.Hide();
            //Open Parent Model Popup for Dealer
            MPEDealerDetails.Show();

        }

        /// <summary>
        /// Handle Click event of Search button for Searching dealers based on user selected crieteria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearchDealers_Click(object sender, EventArgs e)
        {
            string strDealerName = null;
            string strDealerCity = null;
            string strDealerZip = null;
            int dealerStateId = -1;

            //Set Variables to be passed to BAL LAYER
            if (!string.IsNullOrEmpty(txtDealerToSearch.Text.Trim()))
                strDealerName = txtDealerToSearch.Text.Trim();

            if (!string.IsNullOrEmpty(txtDealerCity.Text.Trim()))
                strDealerCity = txtDealerCity.Text.Trim();

            if (!string.IsNullOrEmpty(txtDealerZip.Text.Trim()))
                strDealerZip = txtDealerZip.Text.Trim();

            if (!string.IsNullOrEmpty(ddlDealerState.SelectedValue))
                dealerStateId = Convert.ToInt32(ddlDealerState.SelectedValue);

            //Bind Dealers/Customers detail in gridview control if any
            BindDealers(strDealerName, strDealerCity, strDealerZip, dealerStateId);

            //Search Child Ajax Model Popup Control & Show in their respective FormView Controls
            AjaxControlToolkit.ModalPopupExtender MPESelectDealer = frmViewEditDealerDetails.Row.FindControl("MPESelectDealer") as AjaxControlToolkit.ModalPopupExtender;
            MPESelectDealer.Show();

        }

        /// <summary>
        /// Bind gridview Control with dealer details fetched based on parameters passed
        /// </summary>
        private void BindDealers(string strDealerName, string strDealerCity, string strDealerZip, int dealerStateId)
        {
            //AddInventoryBAL objAddInventoryBAL = new AddInventoryBAL();
            gvDealerDetails.DataSource = InventoryBAL.SearchDealer(strDealerName, strDealerCity, dealerStateId, strDealerZip, Constant.OrgID);
            gvDealerDetails.DataBind();


        }

        /// <summary>
        /// Bind gridview Control with customer details fetched based on parameters passed
        /// </summary>
        private void BindCustomers(string strDealerName, string strDealerCity, string strDealerZip, int dealerStateId)
        {
            //Bind Grid Control
            gvCustomerSearch.DataSource = InventoryBAL.SearchDealer(strDealerName, strDealerCity, dealerStateId, strDealerZip, Constant.OrgID);
            gvCustomerSearch.DataBind();


        }
        /// <summary>
        /// Handle click event of customer search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEditSearchCust_Click(object sender, EventArgs e)
        {
            string strDealerName = null;
            string strDealerCity = null;
            string strDealerZip = null;
            int dealerStateId = -1;

            //Set Variables to be passed to BAL LAYER
            if (!string.IsNullOrEmpty(txtCustName.Text.Trim()))
                strDealerName = txtCustName.Text.Trim();

            if (!string.IsNullOrEmpty(txtCustCity.Text.Trim()))
                strDealerCity = txtCustCity.Text.Trim();

            if (!string.IsNullOrEmpty(txtCustZip.Text.Trim()))
                strDealerZip = txtCustZip.Text.Trim();

            if (!string.IsNullOrEmpty(ddlCustState.SelectedValue))
                dealerStateId = Convert.ToInt32(ddlCustState.SelectedValue);

            //Bind Dealers/Customers detail in gridview control if any
            BindCustomers(strDealerName, strDealerCity, strDealerZip, dealerStateId);

            //Search Child Ajax Model Popup Control & Show in their respective FormView Controls
            AjaxControlToolkit.ModalPopupExtender MPESelectCustomer = frmEditSoldTo.Row.FindControl("MPESelectCustomer") as AjaxControlToolkit.ModalPopupExtender;
            MPESelectCustomer.Show();
        }

        /// <summary>
        /// Handle Image button click for select customer 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnSelect_Clck(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtnSelect = (ImageButton)sender;

            GridViewRow row = (GridViewRow)imgbtnSelect.NamingContainer;

            //Set Selected DealerId/CustomerId in Hidden Field for Insertion in db table
            hdSelDealerId.Value = imgbtnSelect.CommandArgument.ToString(); ;
            ViewState["VSCustomerID"] = hdSelDealerId.Value;
            //Set Name of Selected Dealer/Customer in Hidden Field
            hdDealerName.Value = Server.HtmlDecode(row.Cells[2].Text);



            //Find Textbox control in EditPopup for SoldTo Details and assign name of selected dealer
            TextBox txtCustName = frmEditSoldTo.Row.FindControl("txtCustomerName") as TextBox;
            txtCustName.Text = hdDealerName.Value;


            //Search Ajax Model Popup Control & Hide Model PopUp
            AjaxControlToolkit.ModalPopupExtender MPESelectCustomer = frmEditSoldTo.Row.FindControl("MPESelectCustomer") as AjaxControlToolkit.ModalPopupExtender;
            MPESelectCustomer.Hide();

            //Open Parent Model Popup for Dealer
            MPESoldTo.Show();
        }

        /// <summary>
        /// Handle Paging in gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvDealerDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerDetails.PageIndex = e.NewPageIndex;
            gvDealerDetails.DataBind();
        }

        /// <summary>
        /// Handle Selected Index Changed Event of Year DropDownList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            MPECarDetailsUpdate.Show();
            //Find Controls in Formview Control
            DropDownList ddlYear = (DropDownList)sender;
            DropDownList ddlMake = frmCarDetailsUpdate.Row.FindControl("ddlMake") as DropDownList;
            DropDownList ddlModel = frmCarDetailsUpdate.Row.FindControl("ddlModel") as DropDownList;
            DropDownList ddlBody = frmCarDetailsUpdate.Row.FindControl("ddlBody") as DropDownList;
            DropDownList ddlExtCol = frmCarDetailsUpdate.Row.FindControl("ddlExtCol") as DropDownList;
            DropDownList ddlIntCol = frmCarDetailsUpdate.Row.FindControl("ddlIntCol") as DropDownList;

            //Clear all dropdownlists for Make,Model,Body,ExtCol,IntCol before Filling new makes for Selected Year
            ddlMake.Items.Clear();
            ddlModel.Items.Clear();
            ddlBody.Items.Clear();
            AddGenericColors();

            //Fetch Makes based on selected Year
            if (!string.IsNullOrEmpty(ddlYear.SelectedValue))
                BindMake(Convert.ToInt32(ddlYear.SelectedValue), false);
        }
        /// <summary>
        /// Bind Make
        /// </summary>
        /// <param name="year"></param>
        private void BindMake(int year, bool isFromDb)
        {
            DropDownList ddlMake = frmCarDetailsUpdate.Row.FindControl("ddlMake") as DropDownList;

            //Bind Make based on selected Year
            ddlMake.DataSource = Common.GetMakes(year);
            ddlMake.DataBind();

            //Provide One Empty Element for Making It Optional
            ddlMake.Items.Insert(0, new ListItem("", ""));

            //Fetch Crhome Data from db  
            CarDetailsSelectResult result = InventoryBAL.GetCarDetail(_code);

            //Check is Value need to be set from db
            if (isFromDb)
            {
                //Find Make AND Set value from db
                if (ddlMake.Items.FindByValue(Convert.ToString(result.MakeId)) != null)
                    ddlMake.SelectedValue = Convert.ToString(result.MakeId);
            }

            if (!string.IsNullOrEmpty(ddlMake.SelectedValue))
                BindModel(Convert.ToInt64(ddlMake.SelectedValue), isFromDb);

        }
        /// <summary>
        /// Bind Model
        /// </summary>
        /// <param name="makeId"></param>
        private void BindModel(long makeId, bool isFromDb)
        {
            DropDownList ddlModel = frmCarDetailsUpdate.Row.FindControl("ddlModel") as DropDownList;
            //Bind Model based on selected model
            ddlModel.DataSource = Common.GetModel(makeId);
            ddlModel.DataBind();

            //Provide One Empty Element for Making It Optional
            ddlModel.Items.Insert(0, new ListItem("", ""));

            //Fetch Crhome Data from db
            CarDetailsSelectResult result = InventoryBAL.GetCarDetail(_code);

            //Check is Value need to be set from db
            if (isFromDb)
            {
                //Find Model and Set Value from db
                if (ddlModel.Items.FindByValue(Convert.ToString(result.ModelId)) != null)
                    ddlModel.SelectedValue = Convert.ToString(result.ModelId);

                //Bind Body
            }
            if (!string.IsNullOrEmpty(ddlModel.SelectedValue))
                BindBody(Convert.ToInt64(ddlModel.SelectedValue), isFromDb);


        }
        /// <summary>
        /// Bind Body
        /// </summary>
        /// <param name="bodyId"></param>
        private void BindBody(long modelId, bool isFromDB)
        {
            DropDownList ddlBody = frmCarDetailsUpdate.Row.FindControl("ddlBody") as DropDownList;
            Common objCommon = new Common();
            //Bind Body based on selected Model
            ddlBody.DataSource = objCommon.GetBodies(modelId);
            ddlBody.DataBind();

            //Provide One Empty Element for Making It Optional
            ddlBody.Items.Insert(0, new ListItem("", ""));


            //Get make,model,year,body,ext color,int color ID from db
            CarDetailsSelectResult result = InventoryBAL.GetCarDetail(_code);

            //Check is Value need to be set from db
            if (isFromDB)
            {
                //Find Body and Set Value from db
                if (ddlBody.Items.FindByValue(Convert.ToString(result.BodyId)) != null)
                    ddlBody.SelectedValue = Convert.ToString(result.BodyId);
            }
            //Call Colors()

            BindColors(isFromDB);
        }

        /// <summary>
        /// Bind Interior Colors either based on selected YMMB and append Generic Colors at last
        /// </summary>
        /// <param name="q"></param>
        private void BindInteriorColors(IQueryable source)
        {
            //Find Control in Formview Control
            DropDownList ddlIntCol = frmCarDetailsUpdate.Row.FindControl("ddlIntCol") as DropDownList;

            //Bind Dropdownlist
            ddlIntCol.DataSource = source;
            ddlIntCol.DataTextField = "IntDesc";
            ddlIntCol.DataValueField = "IntColorId";
            ddlIntCol.DataBind();
        }

        /// <summary>
        /// Bind Exterior Colors either based on selected YMMB and append Generic Colors at last
        /// </summary>
        /// <param name="q"></param>
        private void BindExteriorColors(IQueryable source)
        {
            //Find Control in Formview Control
            DropDownList ddlExtCol = frmCarDetailsUpdate.Row.FindControl("ddlExtCol") as DropDownList;
            //Bind Dropdownlist
            ddlExtCol.DataSource = source;
            ddlExtCol.DataTextField = "Ext1Desc";
            ddlExtCol.DataValueField = "ExtColorId";
            ddlExtCol.DataBind();

        }


        /// <summary>
        /// Handle Selected Index Changed Event of Make Dropdownlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlMake = (DropDownList)sender;
            //Find Control in Formview Control
            DropDownList ddlModel = frmCarDetailsUpdate.Row.FindControl("ddlModel") as DropDownList;
            DropDownList ddlBody = frmCarDetailsUpdate.Row.FindControl("ddlBody") as DropDownList;
            //DropDownList ddlExtCol = frmCarDetailsUpdate.Row.FindControl("ddlExtCol") as DropDownList;
            //DropDownList ddlIntCol = frmCarDetailsUpdate.Row.FindControl("ddlIntCol") as DropDownList;

            //Clear all Model,Body,Ext Color,Int Color Before Filling new Model based on selected Make
            ddlModel.Items.Clear();
            ddlBody.Items.Clear();
            AddGenericColors();

            //Fetch Body and Model for selected Make
            if (!string.IsNullOrEmpty(ddlMake.SelectedValue))
            {
                //Fetch Model's based on Selected Make
                BindModel(Convert.ToInt64(ddlMake.SelectedValue), false);
            }

        }

        /// <summary>
        /// Handle selected index changed event of Model Dropdownlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Find Control in Formview Control
            DropDownList ddlExtCol = frmCarDetailsUpdate.Row.FindControl("ddlExtCol") as DropDownList;
            DropDownList ddlIntCol = frmCarDetailsUpdate.Row.FindControl("ddlIntCol") as DropDownList;
            DropDownList ddlBody = frmCarDetailsUpdate.Row.FindControl("ddlBody") as DropDownList;

            //Clear Previous Colors & Body before Filling New Body For Selected Model
            ddlBody.Items.Clear();
            AddGenericColors();

            DropDownList ddlModel = (DropDownList)sender;
            //Bind Body details for selected model
            if (!string.IsNullOrEmpty(ddlModel.SelectedValue))
                BindBody(Convert.ToInt64(ddlModel.SelectedValue), false);
        }

        /// <summary>
        /// Handle selected index changed event of Body Dropdownlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlBody_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Add Generic Colors
            AddGenericColors();

            //Fetch Colors for Selected Year,Make,Model,Body
            BindColors(false);
        }

        /// <summary>
        /// Add Generic Colors in DropDownLists
        /// </summary>
        private void AddGenericColors()
        {
            DropDownList ddlExtCol = frmCarDetailsUpdate.Row.FindControl("ddlExtCol") as DropDownList;
            DropDownList ddlIntCol = frmCarDetailsUpdate.Row.FindControl("ddlIntCol") as DropDownList;

            //Clear Colors
            ddlExtCol.Items.Clear();
            ddlIntCol.Items.Clear();


            //Append Generic Interior Colors in the Interior Color DropDownlist
            List<GetCommonIntColorsResult> objCommonIntColors = Common.CommonInteriorColors();
            ddlIntCol.DataSource = objCommonIntColors;
            ddlIntCol.DataTextField = "IntDesc";
            ddlIntCol.DataValueField = "IntColorId";
            ddlIntCol.DataBind();

            //Insert Blank Item
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


        /// <summary>
        /// Bind Interior and Exterior Colors for Selected Year,Make,Model,Body
        /// </summary>
        private void BindColors(bool isFromDB)
        {
            //Find Control in Formview Control
            DropDownList ddlYear = frmCarDetailsUpdate.Row.FindControl("ddlYear") as DropDownList;
            DropDownList ddlMake = frmCarDetailsUpdate.Row.FindControl("ddlMake") as DropDownList;
            DropDownList ddlModel = frmCarDetailsUpdate.Row.FindControl("ddlModel") as DropDownList;
            DropDownList ddlBody = frmCarDetailsUpdate.Row.FindControl("ddlBody") as DropDownList;
            DropDownList ddlExtCol = frmCarDetailsUpdate.Row.FindControl("ddlExtCol") as DropDownList;
            DropDownList ddlIntCol = frmCarDetailsUpdate.Row.FindControl("ddlIntCol") as DropDownList;
            ListItem lstItem = null;



            //Fetch Interior Colors
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
            //else
            //{
            //    ddlIntCol.Items.Clear();
            //}



            //Fetch Exterior Colors
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

            //else
            //{
            //    ddlExtCol.Items.Clear();
            //}



            //Check if from db value
            if (isFromDB)
            {
                CarDetailsSelectResult result = InventoryBAL.GetCarDetail(_code);

                //Find Exterior Color and Set Value from db
                if (ddlExtCol.Items.FindByValue(Convert.ToString(result.ExtColorId)) != null)
                    ddlExtCol.SelectedValue = Convert.ToString(result.ExtColorId);

                //Find Interior Color and Set Value from db
                if (ddlIntCol.Items.FindByValue(Convert.ToString(result.IntColorId)) != null)
                    ddlIntCol.SelectedValue = Convert.ToString(result.IntColorId);
            }
        }


        /// <summary>
        /// Handle Click event of Selected ComeBack Dealer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnComeBackDealerSelect_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtnSelect = (ImageButton)sender;

            GridViewRow row = (GridViewRow)imgbtnSelect.NamingContainer;
            //Set Selected DealerId/CustomerId in Hidden Field for Insertion in db table
            hdComeBackDealer.Value = imgbtnSelect.CommandArgument.ToString(); ;

            //Set Name of Selected Dealer/Customer in Hidden Field
            hdDealerName.Value = row.Cells[2].Text;

            //Display dealer name by reading it from hidden field
            txtComeBackDealer.Text = hdDealerName.Value;

            //Set ComeBack DealerName in Panel pnlComeBackDetails
            txtComeBackDealer.Text = hdDealerName.Value;

            //Search Ajax Model Popup Control & Hide Model PopUp
            AjaxControlToolkit.ModalPopupExtender MPEComeBackStatus = frmSoldTo.Row.FindControl("MPEComeBackStatus") as AjaxControlToolkit.ModalPopupExtender;

            //Hide dealerpopup as dealer is selected and dealername is assigned in parentpopup textbox
            //MPEComeBackDealer.Hide();

            //Show Parent popup window wiht searched dealername assigned
            MPEComeBackStatus.Show();
        }

        #region [Commented Code for BugId:750: Customer Name Auto Select Feature remove popup]
        ///// <summary>
        ///// Handle Click event of ComeBackDealer Search button
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnComeBackDealerSearch_Click(object sender, EventArgs e)
        //{
        //    string strDealerName = null;
        //    string strDealerCity = null;
        //    string strDealerZip = null;
        //    int dealerStateId = -1;

        //    //Set Variables to be passed to BAL LAYER
        //    if (!string.IsNullOrEmpty(txtComeBackName.Text.Trim()))
        //        strDealerName = txtComeBackName.Text.Trim();

        //    if (!string.IsNullOrEmpty(txtComeBackCity.Text.Trim()))
        //        strDealerCity = txtComeBackCity.Text.Trim();

        //    if (!string.IsNullOrEmpty(txtComeBackZip.Text.Trim()))
        //        strDealerZip = txtComeBackZip.Text.Trim();

        //    if (!string.IsNullOrEmpty(ddlComeBackState.SelectedValue))
        //        dealerStateId = Convert.ToInt32(ddlComeBackState.SelectedValue);

        //    //Bind ComeBack Dealer detail in gridview control if any
        //    BindComeBackDealer(strDealerName, strDealerCity, strDealerZip, dealerStateId);

        //    //Show Popup
        //    MPEComeBackDealer.Show();

        //}

        ///// <summary>
        ///// Bind gridview Control with dealer details fetched based on parameters passed
        ///// </summary>
        //private void BindComeBackDealer(string strDealerName, string strDealerCity, string strDealerZip, int dealerStateId)
        //{
        //    gvComeBackDealer.DataSource = InventoryBAL.SearchDealer(strDealerName, strDealerCity,
        //                                                          dealerStateId, strDealerZip);
        //    gvComeBackDealer.DataBind();

        //}

        #endregion

        /// <summary>
        /// Get/Set VIN Property to be accessible throughout the page from any of the sections
        /// </summary>
        public string VIN
        {
            get;
            set;
        }

        /// <summary>
        /// Handle DataBound Event of formview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void frmCarDetails_DataBound(object sender, EventArgs e)
        {

            FormViewRow row = frmCarDetails.Row;
            //Return if Row Null
            if (row == null)
                return;

            //Set VIN Property for accessible from any of the page sections
            Label lblVIN = row.FindControl("lblVIN") as Label;
            VIN = lblVIN.Text;

            //Check For OLDMMBC FIELD VALUE
            Label lblOldMMBC = row.FindControl("lbltemp") as Label;
            //Check if OldMMBC is 0 then make this row visible false
            if (!string.IsNullOrEmpty(lblOldMMBC.Text))
            {
                if (lblOldMMBC.Text == "0")
                    ((HtmlTableRow)row.FindControl("trOldMMBC")).Visible = false;
            }

            //Display Inventory Status in Div Header
            Label lblInvStatus = row.FindControl("lblCarStat") as Label;
            lblCarStatus.Text = "CAR DETAILS (Status: " + lblInvStatus.Text + ")";

            //Code added by Shiv related to System
            HiddenField hdnText = row.FindControl("hdnSystemText") as HiddenField;
            HiddenField hdnValue = row.FindControl("hdnSystemValue") as HiddenField;
            HiddenField hdnSystemHistory = row.FindControl("hdnUpdatedSystemHistory") as HiddenField;
            HiddenField hdnAddBy = row.FindControl("hdnAddedBy") as HiddenField;

            lblSystem.Text = lblSystem2.Text = hdnText.Value;
            lblSystemHistory.Text = hdnSystemHistory.Value;
            lblSystemDateAdded.Text = hdnAddBy.Value;
            FillSystemDDL();
            ddlSystem.SelectedValue = hdnValue.Value;

            Label lblCRStatus = row.FindControl("lblCRStatus") as Label;
            if (String.IsNullOrEmpty(lblCRStatus.Text) || lblCRStatus.Text == "0")
            {
                lblUCRStatus.Text = "[UCR Status: Not Ready]";
                btnCreateUcr.Visible = true;
                btnChangeUcr.Visible = false;
                btnViewUCR.Visible = false;
            }
            else if (lblCRStatus.Text == "10" || lblCRStatus.Text == "20")
            {
                lblUCRStatus.Text = "[UCR Status: Initiated]";
                btnCreateUcr.Visible = false;
                btnChangeUcr.Visible = true;
                btnViewUCR.Visible = false;
            }
            else if (lblCRStatus.Text == "30")
            {
                lblUCRStatus.Text = "[UCR Status: Available]";
                btnCreateUcr.Visible = false;
                btnChangeUcr.Visible = false;
                btnViewUCR.Visible = true;
            }

            if (Convert.ToString(Session["LoginEntityTypeID"]) == "2" || Convert.ToString(Session["LoginEntityTypeID"]) == "1")
            {
                btnCreateUcr.Visible = false;
                btnEditCarDetails.Visible = false;
                btnEditDealerDetails.Visible = false;
                imgbtnEditSoldToDetails.Visible = false;
                imgbtnEditCarProp.Visible = false;
                btnMoveToInv.Enabled = false;
                btnMoveToArch.Enabled = false;
                btnMoveToOnHand.Enabled = false;
                btnBillofSale.Enabled = false;
                btnBillofSale_PDF.Enabled = false;
                btnChangeUcr.Visible = false;


            }
            Label lblImageCount = row.FindControl("lblImageCount") as Label;
            HiddenField hfCRStatus = (HiddenField)row.FindControl("hfCRStatus");
            HiddenField hfVIN2 = (HiddenField)row.FindControl("hfVIN2");

            HiddenField hfyear = (HiddenField)row.FindControl("hfyear");
            HiddenField hfmake = (HiddenField)row.FindControl("hfmake");
            HiddenField hfmodel = (HiddenField)row.FindControl("hfmodel");
            HiddenField hfbody = (HiddenField)row.FindControl("hfbody");
            HiddenField hfprice = (HiddenField)row.FindControl("hfprice");
            HiddenField hfmileage = (HiddenField)row.FindControl("hfmileage");
            HiddenField hfextcol = (HiddenField)row.FindControl("hfextcol");
            HiddenField hfintcol = (HiddenField)row.FindControl("hfintcol");

            hfvin.Value = hfVIN2.Value;
            hfcryear.Value = hfyear.Value;
            hfcrmake.Value = hfmake.Value;
            hfcrmodel.Value = hfmodel.Value;
            hfcrbody.Value = hfbody.Value;
            hfcrprice.Value = hfprice.Value;
            hfcrmileage.Value = hfmileage.Value;
            hfcrextcol.Value = hfextcol.Value;
            hfcrintcol.Value = hfintcol.Value;
        }

        #region [Fill System's Dropdown List ]
        private void FillSystemDDL()
        {
            DataTable dtSystems = new DataTable();
            if (Session["dtSystems"] == null)
            {
                dtSystems = InventoryBAL.FetchSystems(Constant.OrgID);
                Session["dtSystems"] = dtSystems;
            }
            else
                dtSystems = (DataTable)Session["dtSystems"];

            ddlSystem.DataSource = dtSystems;
            ddlSystem.DataTextField = "Description";
            ddlSystem.DataValueField = "SystemId";
            ddlSystem.DataBind();
            //ListItem item = new ListItem("All", "-1");
            //ddlSystem.Items.Insert(0, item);

        }
        #endregion

        /// <summary>
        /// Handle Click event of linkbutton to open Expense page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkExpense_Click(object sender, EventArgs e)
        {
            Response.Redirect("InventoryExpense.aspx?Code=" + _code);
        }

        /// <summary>
        /// Handle Click event of linkbutton to open Inventory Life Cycle Events Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkInventoryEvents_Click(object sender, EventArgs e)
        {
            Response.Redirect("InventoryLifeCycle.aspx?Code=" + _code);
        }

        /// <summary>
        /// Handle click event of link button to open Notes Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkNotes_Click(object sender, EventArgs e)
        {
            Response.Redirect("InventoryNotes.aspx?Code=" + _code);
        }
        /// <summary>
        /// Handle Click event of link button to open documents page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkDocuments_Click(object sender, EventArgs e)
        {
            Response.Redirect("InventoryDocuments.aspx?Code=" + _code);
        }
        /// <summary>
        /// Handle Click event of Link Button to open Drivers page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkDrivers_Click(object sender, EventArgs e)
        {
            Response.Redirect("InventoryDriver.aspx?Code=" + _code);
        }
        /// <summary>
        /// Handle paging in gridview control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLinkedFilter_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvLinkedFilter.PageIndex = e.NewPageIndex;
            BindLinkedCars(_code);

        }
        /// <summary>
        /// Handle Click event of image button for SoldStatus/SoldComments Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgOpenSoldStatusPopUp_Click(object sender, ImageClickEventArgs e)
        {
            //Fetch Sold Status & Sold Comments information If any from db & Display in Edit SoldStatus Popup fields
            List<string> lstSoldInfo = InventoryBAL.InventorySoldStatus(_code);
            //By Default Show Sold Status=No
            if (ddlPopSoldStatus.Items.Count > 0)
                ddlPopSoldStatus.SelectedIndex = 1;

            if (lstSoldInfo != null && lstSoldInfo.Count > 0)
            {
                ddlPopSoldStatus.SelectedValue = lstSoldInfo[0];
                txtPopSoldComment.Text = lstSoldInfo[1];
            }
            //Open Popup For SoldTo Update
            MPESoldUpdate.Show();
        }
        /// <summary>
        /// Handle Click event of SoldStatus Update Popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int soldStatus = 0;
            DateTime updatedDate = DateTime.Now;
            long updatedBy = Constant.UserId;
            if (!string.IsNullOrEmpty(ddlPopSoldStatus.SelectedValue))
                soldStatus = Convert.ToInt32(ddlPopSoldStatus.SelectedValue);

            //Update SoldDetails

            #region [Rupendra 28 Nov 12 Insert in Table History]

            System.Collections.ArrayList arraylist = new ArrayList();
            if (Request.QueryString["Code"] != null)
            {
                Int64 InventoryId = Convert.ToInt64(Request.QueryString["Code"]);
                arraylist = BAL.InventoryBAL.CompleteInventoryInfoByInventoryID(InventoryId);
                if (arraylist.Count > 0)
                {
                    dtOldValue = arraylist[3] as DataTable;
                    if (dtOldValue.Rows.Count > 0)
                    {
                        int ret;
                        string SoldStatusOld = string.Empty;
                        string SoldStatusNew = string.Empty;


                        if (Convert.ToString(dtOldValue.Rows[0]["SoldStatus"]).ToLower() == "yes")
                            SoldStatusOld = "1";
                        else if (Convert.ToString(dtOldValue.Rows[0]["SoldStatus"]).ToLower() == "no")
                            SoldStatusOld = "0";
                        else
                            SoldStatusOld = "2";


                        if (dtOldValue.Rows.Count > 0)
                        {

                            if (ddlPopSoldStatus.SelectedValue != SoldStatusOld)
                            {
                                SoldStatusNew = ddlPopSoldStatus.SelectedValue;
                            }
                            else
                            {
                                SoldStatusOld = "";
                                SoldStatusNew = "";
                            }

                            InventoryBAL objInventoryBAL = new InventoryBAL();
                            ret = objInventoryBAL.SaveInventoryHistory(InventoryId, string.Empty, string.Empty, string.Empty, string.Empty, SoldStatusOld, SoldStatusNew, string.Empty, string.Empty, Constant.UserId, "InventoryDetails");
                        }
                    }
                }

            }

            #endregion

            InventoryBAL.InventorySoldStatusUpdate(_code, soldStatus, txtPopSoldComment.Text.Trim(), updatedBy, updatedDate);

            //Bind All Data on U.I
            BindAllSections(_code);
            BindAllSectionsInEditMode(_code);

        }

        /// <summary>
        /// Handle Click event to Move Car to Inventory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMoveToInv_Click(object sender, EventArgs e)
        {
            MoveToInventory();

            //Again Check Conditions
            ChangeInventoryStatus();

            UpdateCarStatus();
        }

        /// <summary>
        /// Handle Click event to Move Car to Archieve
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMoveToArch_Click(object sender, EventArgs e)
        {
            //if (InventoryBAL.CarStatusSelect(_code) != "3")
            MoveToArchieve();

            //Again Check Conditions
            ChangeInventoryStatus();

            UpdateCarStatus();
        }

        /// <summary>
        /// Handle Click event to Move Car to OnHand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMoveToOnHand_Click(object sender, EventArgs e)
        {
            //if(InventoryBAL.CarStatusSelect(_code)!= "2")
            MovetoOnHand();

            //Again Check Conditions
            ChangeInventoryStatus();

            UpdateCarStatus();
        }

        /// <summary>
        /// Update CarStatus in CarDetails Header, if Car Status changed
        /// </summary>
        private void UpdateCarStatus()
        {
            string strCarStatus = string.Empty;
            int carStatusCode = InventoryBAL.CarStatusSelect(_code);
            if (carStatusCode == 1)
                strCarStatus = "Inventory";
            else if (carStatusCode == 2)
                strCarStatus = "OnHand";
            else if (carStatusCode == 3)
                strCarStatus = "Archived";

            //Update  CarDetails Section Header
            lblCarStatus.Text = "CAR DETAILS (Status: " + strCarStatus + ")";
        }

        /// <summary>
        /// Move Car To OnHand,Mode=2 For OnHand
        /// </summary>
        private void MovetoOnHand()
        {
            InventoryBAL.CarStatusUpdate(_code, 2, Constant.UserId, DateTime.Now);
            Master.PageMessage = SUCCESSFULLY_MOVE_TO_ONHAND;
            //Disabled button once udpated
            btnMoveToOnHand.Enabled = false;
            btnMoveToOnHand.CssClass = BUTTON_DISABLE_CSS;
            //Visible All Edit Buttons, if Found Visible False
            EnableDisableEditButtons(true);
        }

        /// <summary>
        /// Move Car To Archieve,Mode=3 For Archieve
        /// </summary>
        private void MoveToArchieve()
        {
            InventoryBAL.CarStatusUpdate(_code, 3, Constant.UserId, DateTime.Now);
            Master.PageMessage = SUCCESSFULLY_MOVE_TO_ARCHIEVE;
            //Make  All Edit Buttons Visible False, if Inventory move to Archieve
            EnableDisableEditButtons(false);

            //Disabled button once udpated
            btnMoveToArch.Enabled = false;
            btnMoveToArch.CssClass = BUTTON_DISABLE_CSS;
        }
        /// <summary>
        /// Move Car to Inventory,Mode=1 For Inventory
        /// </summary>
        private void MoveToInventory()
        {
            InventoryBAL.CarStatusUpdate(_code, 1, Constant.UserId, DateTime.Now);
            Master.PageMessage = SUCCESSFULLY_MOVE_TO_INVENTORY;
            btnMoveToInv.Enabled = false;
            btnMoveToInv.CssClass = BUTTON_DISABLE_CSS;
            //Enable All Edit Buttons, if Found Disable
            EnableDisableEditButtons(true);
        }

        #region [Commented Code For MovetoInventory,Archieve using Popup]
        ///// <summary>
        ///// Handle Click Event of Inventory Change Status Popup Button
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnInvYes_Click(object sender, EventArgs e)
        //{  
        //    //Retreive value of Inventory status from Session
        //    if (Session["InventoryStatus"] != null)
        //        InventoryStatus = Session["InventoryStatus"].ToString();


        //    //Update Car Status to OnHand,For ONHAND CARSTATUS=2
        //    if (InventoryStatus == INVENTORY_ONHAND)
        //        MovetoOnHand();

        //    //Update Car Status to Archieve,For Archieve CARSTATUS=3
        //    if (InventoryStatus == INVENTORY_ARCHIEVE)
        //        MoveToArchieve();
        //}
        ///// <summary>
        ///// Handle Click Event of Inventory Change Status Popup Button
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnInvNo_Click(object sender, EventArgs e)
        //{
        //    //Retreive value of Inventory status from Session
        //    if (Session["InventoryStatus"] != null)
        //        InventoryStatus =  Session["InventoryStatus"].ToString();

        //    //Enabled MOVE TO ONHAND INVENTORY BUTTON
        //    if (InventoryStatus == INVENTORY_ONHAND)
        //        btnMoveToOnHand.Enabled = true;

        //    //Enabled MOVE TO ARCHIEVE INVENTORY BUTTON
        //    if (InventoryStatus == INVENTORY_ARCHIEVE)
        //        btnMoveToArch.Enabled = true;
        //}
        #endregion


        /// <summary>
        /// Enable/Disable Inventory Move Buttons
        /// </summary>
        /// <param name="val"></param>
        private void MoveInventoryButtons(bool val, string cssClass)
        {
            btnMoveToInv.Enabled = val;
            btnMoveToInv.CssClass = cssClass;

            btnMoveToArch.Enabled = val;
            btnMoveToArch.CssClass = cssClass;

            btnMoveToOnHand.Enabled = val;
            btnMoveToOnHand.CssClass = cssClass;
        }

        /// <summary>
        /// Enable/Disable Edit Buttons
        /// </summary>
        /// <param name="val"></param>
        private void EnableDisableEditButtons(bool val)
        {
            btnEditCarDetails.Visible = val;
            imgbtnEditCarProp.Visible = val;

            //Fixed Manage Inventory Edit  Dealer Popup Issue
            if (!val)
                btnEditDealerDetails.Style.Add("display", "none;");
            else
                btnEditDealerDetails.Style.Remove("display");

            imgbtnEditSoldToDetails.Visible = val;

        }


        #region [Open Bill of Sale Report]
        protected void btnBillofSale_Click(object sender, EventArgs e)
        {
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("InventoryId", _code.ToString());
            parameters[1] = new ReportParameter("UserId", Constant.UserId.ToString());
            ReportParameters.Parameters = parameters;
            ReportParameters.ReportName = "/Hollenshead/BillOFSaleReport";

            Response.Redirect("~/VMSReports/Reports.aspx?ReturnURL=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
        }
        protected void btnBillofSale_PDF_Click(object sender, EventArgs e)
        {
            //Response.Write(String.Format("<script>window.open('BillOfSale.aspx?Code={0}', '_blank');</script>", _code));
            //  Response.Redirect(String.Format("BillOfSale.aspx?Code={0}", _code));
            String Url = String.Format("openBOFwindow('BillOfSale.aspx?Code={0}');", _code);
            ScriptManager.RegisterClientScriptBlock(updMain, this.GetType(), "alert", Url, true);
        }
        #endregion

        ///// <summary>
        ///// Handle SoldStatus dropdownlist selected index changed event(SoldStatus Edit Popup)
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void ddlSoldStatus_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    TextBox txtSoldComment = frmEditSoldTo.FindControl("txtSoldComment") as TextBox;
        //    DropDownList ddlSoldStatus = frmEditSoldTo.FindControl("ddlSoldStatus") as DropDownList;

        //    if (ddlSoldStatus != null && txtSoldComment != null)
        //    {
        //        if (ddlSoldStatus.SelectedValue.ToUpper() == "TRUE")
        //            txtSoldComment.Text += " " + DateTime.Now.ToString() + " ";
        //    }
        //}

        /// <summary>
        /// Handle TitlePresent dropdownlist selected index changed event(Dealer Detail Edit Popup)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlTitlePresent_SelectedIndexChanged(object sender, EventArgs e)
        {
            MPEDealerDetails.Show();
            DropDownList ddlTitlePresent = sender as DropDownList;
            TextBox txtTitlePresentNotes = frmViewEditDealerDetails.FindControl("txtTitlePresentNotes") as TextBox;
            //DropDownList ddlTitlePresent = frmViewEditDealerDetails.FindControl("ddlTitlePresent") as DropDownList;
            //Append date & time in notes/comment textbox if Status changed from NO TO YES
            if (ddlTitlePresent != null && txtTitlePresentNotes != null)
            {
                if (ddlTitlePresent.SelectedValue.ToUpper() == "TRUE")
                    txtTitlePresentNotes.Text += " " + DateTime.Now.ToString() + " ";
            }

        }

        /// <summary>
        /// Handle TitleShipped dropdownlist selected index changed event(Dealer Detail Edit Popup)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlTitleShipped_SelectedIndexChanged(object sender, EventArgs e)
        {
            MPEDealerDetails.Show();
            DropDownList ddlTitleShipped = sender as DropDownList;
            TextBox txtTitleShippedNotes = frmViewEditDealerDetails.FindControl("txtTitleShippedNotes") as TextBox;
            //DropDownList ddlTitleShipped = frmViewEditDealerDetails.FindControl("ddlTitleShipped") as DropDownList;
            //Append date & time in notes/comment textbox if Status changed from NO TO YES
            if (ddlTitleShipped != null && txtTitleShippedNotes != null)
            {
                if (ddlTitleShipped.SelectedValue.ToUpper() == "TRUE")
                    txtTitleShippedNotes.Text += " " + DateTime.Now.ToString() + " ";
            }
        }
        /// <summary>
        /// Handle dropdownlist "Vehicle Present Selected Index changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlVehiclePresent_SelectedIndexChanged(object sender, EventArgs e)
        {
            MPECarDetailsUpdate.Show();
            DropDownList ddlVehiclePresent = sender as DropDownList;

            TextBox txtArrivalDate = frmCarDetailsUpdate.FindControl("txtArrivalDate") as TextBox;

            //Set Arrival Date if Vehicle Is Present and arrival date not empty or null
            if (ddlVehiclePresent.SelectedValue == "True" && string.IsNullOrEmpty(txtArrivalDate.Text))
                txtArrivalDate.Text = DateTime.Today.ToShortDateString();

        }
        /// <summary>
        /// Delete Inventory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteInventory_Click(object sender, EventArgs e)
        {
            InventoryBAL obj = new InventoryBAL();
            obj.DeleteInventory(_code, Constant.UserId, DateTime.Now);
            Response.Redirect("InventorySearch.aspx");
        }
        /// <summary>
        /// Re-direct user to laneassignment page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBackLaneAssignment_Click(object sender, EventArgs e)
        {
            //Already check at page load for QueryString Parameter ReturnUrl
            //Note: This button visible only, if this parameter is passed
            Response.Redirect(Request.QueryString["ReturnURL"]);
        }

        //// [Change Request BugId: 725:In Linked Cars Grid there is a column Check No. make it hyperlink 
        ////and move user to ExpenseAgainstPayment.aspx Page
        // #region [Open Expense Against Payment Screen for particular check number clicked]
        // /// <summary>
        // /// Open ExpenseAgainstPayment.aspx page on click of checknumber field inside linked Filter grid
        // /// </summary>
        // /// <param name="sender"></param>
        // /// <param name="e"></param>
        // protected void lnkCheckNumber_OnClick(object sender, EventArgs e)
        // {
        //     LinkButton btn = (LinkButton)sender;
        //     Int32 paymentId = Int32.Parse(btn.CommandArgument);
        //     if(paymentId!=0)
        //       Response.Redirect("ExpenseAgainstPayment.aspx?PaymentId=" + paymentId);
        // }
        // #endregion

        public String CarStatus(object carstatus, object isActive, object dateDeleted, object deletedBy)
        {
            bool IsActive = true;
            String CarStatus = String.Empty;
            DateTime DateDeleted = DateTime.MinValue;
            String DeletedBy = String.Empty;
            try
            {
                DeletedBy = Convert.ToString(deletedBy);
                CarStatus = Convert.ToString(carstatus);

                if (isActive != null)
                    IsActive = Convert.ToBoolean(isActive);

                if (dateDeleted != null)
                    DateDeleted = Convert.ToDateTime(dateDeleted);

            }
            catch {/* Just to bypass in case of any error */}
            if (!IsActive)
            {
                return String.Format("{0} [<i style='text-transform:none'>Deleted On:{1}    Deleted By: {2}</i>]", CarStatus, DateDeleted, DeletedBy);
            }
            else
            {
                return CarStatus;
            }
        }

        #region[Create/Edit/Delete UCR]
        protected void btnCreateUcr_Click(object sender, EventArgs e)
        {
            mpeCR.Show();
        }

        protected void btnChangeUcr_Click(object sender, EventArgs e)
        {
            mpeChangeCR.Show();
            hlnkViewReport.NavigateUrl = String.Format("{0}{1}", UCRlinkUrl, hfCRID.Value);
        }

        protected void btnViewUCR_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("{0}{1}", UCRlinkUrl, hfCRID.Value));
            //Response.Redirect(String.Format("http://web.metaoptionllc.com:82/Report/{0}", hfCRID.Value));
        }
        #endregion

        #region[Add/Link CR - show hide link option]
        protected void rbtnListCR_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpeCR.Show();
        }
        #endregion

        #region[Add/Link CR - ID/URL]
        protected void rbtnCRIdUrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpeCR.Show();
        }
        #endregion

        #region[Close CR popup]
        protected void btnCRcancel_Click(object sender, EventArgs e)
        {
            mpeCR.Hide();
        }
        #endregion

        #region[Link CR by CRID]
        protected void btncridvalidate_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=1&value={0}&invtype=I&id={1}", txtCRIdUrl.Text, _code);
            mpeCR.Hide();
            mpucr.Show();

        }

        protected void btncridvalidate2_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=1&value={0}&invtype=I&id={1}", txtCRIdUrl2.Text, _code);
            mpeCR.Hide();
            mpucr.Show();

        }
        #endregion

        #region[Link CR by CRURL]
        protected void btncrurlvalidate_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=2&value={0}&invtype=I&id={1}", txtCRIdUrl.Text, _code);
            mpeCR.Hide();
            mpucr.Show();

        }

        protected void btncrurlvalidate2_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=2&value={0}&invtype=I&id={1}", txtCRIdUrl2.Text, _code);
            mpeCR.Hide();
            mpucr.Show();

        }
        #endregion

        #region[Link CR by VIN]
        protected void btncrsearch_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=3&value={0}&invtype=I&id={1}", txtCRIdUrl.Text, _code);
            mpeCR.Hide();
            mpucr.Show();

        }

        protected void btncrsearch2_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=3&value={0}&invtype=I&id={1}", txtCRIdUrl2.Text, _code);
            mpeCR.Hide();
            mpucr.Show();

        }
        #endregion

        protected void btnucrcancel_Click(object sender, EventArgs e)
        {
            mpucr.Hide();
        }

        protected void btnCRok_Click(object sender, EventArgs e)
        {

        }

        protected void btnCRok2_Click(object sender, EventArgs e)
        {
            long PreInvID = 0, InventoryID = 0;
            InventoryID = _code;
            PreInventoryBAL PreInvBAL = new PreInventoryBAL();
            PreInvBAL.RemoveCR(PreInvID, InventoryID, Convert.ToInt64(Session["empId"]));
            mpeChangeCR.Hide();
            Response.Redirect(String.Format("InventoryDetail.aspx?Mode=View&Code={0}", InventoryID));
        }

        public void ExtractCrInfo(CarDetailsSelectResult res)
        {
            crInvID = Convert.ToString(_code);
            crVin = Convert.ToString(res.VIN);
            crYear = Convert.ToString(res.Year);
            crMake = Convert.ToString(res.Make);
            crModel = Convert.ToString(res.Model);
            crBody = Convert.ToString(res.Body);
            crPrice = Convert.ToString(res.CarCost);
            crMileage = Convert.ToString(res.MileageIn);
            crExtCol = Convert.ToString(res.Ext1Desc);
            crIntCol = Convert.ToString(res.INTDesc);
        }

        #region[Duplicate title ddl Selected Index Changed]
        protected void ddlDupTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            MPEDealerDetails.Show();
            DropDownList ddlDupTitle = sender as DropDownList;
            TextBox txtDupTitleNote = frmViewEditDealerDetails.FindControl("txtDupTitleNote") as TextBox;
            if (ddlDupTitle != null && txtDupTitleNote != null)
            {
                //if (ddlDupTitle.SelectedValue == "1")
                txtDupTitleNote.Text += " " + DateTime.Now.ToString() + " ";
            }
        }
        #endregion

        private void BindAllSectionsInEditMode(long inventoryId)
        {
            System.Collections.ArrayList arraylist = new ArrayList();
            arraylist = BAL.InventoryBAL.CompleteInventoryInfoByInventoryID(_code);
            if (arraylist.Count > 0)
            {
                //BindCarDetailsInEditMode
                if (arraylist[0] != null)
                {
                    frmCarDetailsUpdate.DataSource = arraylist[0];
                    frmCarDetailsUpdate.DataBind();
                }

                #region[BindDealersInEditMode]
                if (arraylist[2] != null)
                {

                    frmViewEditDealerDetails.DataSource = arraylist[2];
                    frmViewEditDealerDetails.DataBind();
                }
                //Select Title Present /Title Shipped
                DealerDetailsSelectResult result = InventoryBAL.GetDealerForInv(inventoryId);

                if (result != null)
                {
                    //Set Title Present
                    DropDownList ddlTitlePresent = frmViewEditDealerDetails.FindControl("ddlTitlePresent") as DropDownList;

                    if (result.TitleP)
                        ddlTitlePresent.SelectedValue = "True";
                    else
                        ddlTitlePresent.SelectedValue = "False";



                    //Set Title Shpped
                    DropDownList ddlTitleShipped = frmViewEditDealerDetails.FindControl("ddlTitleShipped") as DropDownList;

                    if (result.TitleS)
                        ddlTitleShipped.SelectedValue = "True";
                    else
                        ddlTitleShipped.SelectedValue = "False";


                    //Set Dup title
                    DropDownList ddlDupTitle = frmViewEditDealerDetails.FindControl("ddlDupTitle") as DropDownList;

                    if (result.DupTitle == "Yes")
                        ddlDupTitle.SelectedValue = "1";
                    else
                        ddlDupTitle.SelectedValue = "0";

                }
                //Find Controls
                TextBox txtDealerShip = frmViewEditDealerDetails.FindControl("txtDealerShip") as TextBox;
                TextBox txtCost = frmViewEditDealerDetails.FindControl("txtCost") as TextBox;
                TextBox txtCheckNumber = frmViewEditDealerDetails.FindControl("txtCheckNumber") as TextBox;
                LinkButton lnkSelectDealer = frmViewEditDealerDetails.FindControl("lnkSelectDealer") as LinkButton;
                HiddenField hdDealerIdSel = frmViewEditDealerDetails.FindControl("hdDealerIdSel") as HiddenField;
                DropDownList ddlBuyer = frmViewEditDealerDetails.FindControl("ddlBuyer") as DropDownList;


                int checkPaidCount = 0;

                //Disable Delete button, if car is archieved
                if (InventoryBAL.CarStatusSelect(_code) == 3)
                    btnDeleteInventory.Visible = false;
                //Enable Delete button, if car is not archived
                // commented following two lines by Naushad on 3/19/2012; This logic is incorrect.
                //      Delete button will only be available if there is permission which was checked earlier.
                //else
                //    btnDeleteInventory.Visible = true;


                //if check is paid then disable Select DealerLink and textbox for dealer 
                if (hdDealerIdSel != null && !string.IsNullOrEmpty(hdDealerIdSel.Value.Trim()))
                {
                    long dealerId = Convert.ToInt64(hdDealerIdSel.Value);
                    //Check Whether CheckPaid(In expense table)for Entity:dealer, 
                    checkPaidCount = InventoryBAL.IsCheckPaid(inventoryId, dealerId, 1, false);

                    // If Check Paid Count=1, Disable Selected Dealer Link and textbox & delete Inventory button
                    if (checkPaidCount >= 1)
                    {
                        //Enable False as check is paid
                        txtDealerShip.Enabled = false;
                        lnkSelectDealer.Enabled = false;
                        //Added Style Attribute as its still appearing on firefox,Nitin Paliwal,Feb 04 2010
                        lnkSelectDealer.Style.Add("display", "none;");
                        //Disable CheckNumber,if check Paid
                        txtCheckNumber.Enabled = false;
                        txtCost.Enabled = false;

                    }
                    else
                    {
                        //Enable Dealer textbox and select dealer link 
                        txtDealerShip.Enabled = true;
                        lnkSelectDealer.Enabled = true;
                        //Added Style Attribute as its still appearing on firefox,Nitin Paliwal,Feb 04 2010
                        lnkSelectDealer.Style.Remove("display");
                        //Enable CheckNumber,if checknot paid against this dealer
                        txtCheckNumber.Enabled = true;
                        txtCost.Enabled = true;
                    }
                }

                //Disable Buyer DropDownlist, if Check isPaid

                if (ddlBuyer.SelectedValue != "0")
                    checkPaidCount = InventoryBAL.IsCheckPaid(inventoryId, Convert.ToInt64(ddlBuyer.SelectedValue), 2, false);

                //Disable buyer dropdownlist and DeleteInventory Button, if check is paid//
                if (checkPaidCount >= 1)
                    ddlBuyer.Enabled = false;
                else
                    ddlBuyer.Enabled = true;

                //Disable Delete Inventory Button,if checkpaid = 1 For All Entities,
                //Pass "True" in IsCheckPaid Function for checking all entitytypes
                checkPaidCount = InventoryBAL.IsCheckPaid(inventoryId, Convert.ToInt64(ddlBuyer.SelectedValue), 2, true);
                if (checkPaidCount >= 1)
                    btnDeleteInventory.Visible = false;
                // commented following two lines by Naushad on 3/19/2012; This logic is incorrect.
                //      Delete button will only be available if there is permission which was checked earlier.
                //else
                //   btnDeleteInventory.Visible = true;
                #endregion

                #region[BindSoldToInEditMode]

                frmEditSoldTo.DataSource = arraylist[3];
                frmEditSoldTo.DataBind();



                //Get Sold Status for the current inventory being edited
                List<string> lstSoldInfo = InventoryBAL.InventorySoldStatus(_code);

                if (lstSoldInfo.Count > 0)
                {
                    //Find Dropdownlist and set Sold Status For Sold Status DropDownList
                    DropDownList ddlSoldStatus = frmEditSoldTo.FindControl("ddlSoldStatus") as DropDownList;
                    ddlSoldStatus.SelectedValue = lstSoldInfo[0].ToString();
                    //if (lstSoldInfo[0].ToUpper() == "TRUE")
                    //    ddlSoldStatus.SelectedValue = "True";
                    //else
                    //    ddlSoldStatus.SelectedValue = "False";
                }
                #endregion
            }
            BindCarPropertyInEditMode(inventoryId);
        }

        public String FormatBadAutoCheck(object BadCarAutoCheck)
        {
            String str = string.Empty;
            if (BadCarAutoCheck == DBNull.Value)
                str = "Unknown";
            else
            {
                if (Convert.ToBoolean(BadCarAutoCheck) == true)
                    str = "Yes";
                else if (Convert.ToBoolean(BadCarAutoCheck) == false)
                    str = "No";
            }
            return str;

        }

        public String FormatBadCarFax(object BadCarFax)
        {
            String str = string.Empty;
            if (BadCarFax == DBNull.Value)
                str = "Unknown";
            else
            {
                if (Convert.ToBoolean(BadCarFax) == true)
                    str = "Yes";
                else if (Convert.ToBoolean(BadCarFax) == false)
                    str = "No";
            }
            return str;

        }

        #region[Audit]
        protected void ibtnAudit_Click(object sender, ImageClickEventArgs e)
        {
            BindAuditGrid();
            mpeAudit.Show();
        }

        private void BindAuditGrid()
        {
            InventoryBAL bal = new InventoryBAL();
            gvAudit.DataSource = bal.InventoryAudit(_code);
            gvAudit.DataBind();
        }

        protected void gvAudit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAudit.PageIndex = e.NewPageIndex;
            BindAuditGrid();
            mpeAudit.Show();
        }
        #endregion

        #region[Lane History & Sold History]
        protected void ibtnLaneHistory_Click(object sender, ImageClickEventArgs e)
        {
            BindLaneHistoryGrid();
            mpeLaneHistory.Show();
        }

        protected void ibtnSoldHistory_Click(object sender, ImageClickEventArgs e)
        {
            BindSoldHistoryGrid();
            mpeSoldHistory.Show();
        }

        private void BindLaneHistoryGrid()
        {
            InventoryBAL bal = new InventoryBAL();
            gvLaneLog.DataSource = bal.InventoryLaneHistory(_code);
            gvLaneLog.DataBind();
        }

        private void BindSoldHistoryGrid()
        {
            InventoryBAL bal = new InventoryBAL();
            gvSoldHistory.DataSource = bal.TableHistorySelect(_code);
            gvSoldHistory.DataBind();
        }

        protected void gvLaneLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLaneLog.PageIndex = e.NewPageIndex;
            BindLaneHistoryGrid();
            mpeLaneHistory.Show();
        }

        protected void gvSoldHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSoldHistory.PageIndex = e.NewPageIndex;
            BindSoldHistoryGrid();
            mpeSoldHistory.Show();
        }

        #endregion

    }

}