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

namespace METAOPTION.UI
{
    public partial class InvDrivers : System.Web.UI.Page, IPagePermission
    {
        #region Public Variables
        public const string PAGE = "INVENTORYDRIVER";
        public const string INVENTORYDRIVER_INVENTORYDRIVER_ADD = "INVENTORYDRIVER.ADD";
        public const string INVENTORYDRIVER_INVENTORYDRIVER_VIEW = "INVENTORYDRIVER.VIEW";
        public const string INVENTORYDRIVER_INVENTORYDRIVER_EDIT = "INVENTORYDRIVER.EDIT";
        public const string INVENTORYDRIVER_INVENTORYDRIVER_DELETE = "INVENTORYDRIVER.DELETE";
        #endregion
        long Code = -1;

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
            
            //Check User Page Level Permissions
            CheckUserPagePermissions();

            try
            {
                //Get Inventory Id
                if (!string.IsNullOrEmpty(Request.QueryString["Code"]))
                    Code = Convert.ToInt64(Request.QueryString["Code"]);
            }
            catch { }

            //Do nothing if -1
            if (Code == -1)
                return;

            if (!IsPostBack)
            {
                //Show Inventory Header Information which tell about teh current expense being edited for which inventory
                lblInventoryHeader.Text = "Inventory Drivers For " + InventoryBAL.GetCurrentInventoryHeader(Code);

                LoadData(Code);
            }
        }

        #region IPagePermission Members
        /// <summary>
        /// Check User Page Level Permissions
        /// </summary>
        public void CheckUserPagePermissions()
        {
            System.Collections.Generic.List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, PAGE);

            //Check If any permission found for this page 
            if (dict == null || dict.Count < 1)
                Response.Redirect("Permission.aspx?MSG=INVENTORYDRIVER:ADD/EDIT/VIEW/DELETE");

            //Disable Add Link, If No Rights
            if (!dict.Contains(INVENTORYDRIVER_INVENTORYDRIVER_ADD))
            {
                lnkAddNewDriver.Visible = false;
            }

            else if (!dict.Contains(INVENTORYDRIVER_INVENTORYDRIVER_EDIT))
            {
                //EnableDisableEditButtons(false);
                Button btnEdit = gvDriver.FindControl("ibtnDriverEdit") as Button;
                btnEdit.Visible = false;
            }
            else if (!dict.Contains(INVENTORYDRIVER_INVENTORYDRIVER_DELETE))
            {
                Button btnDelete = gvDriver.FindControl("ibtnDriverDelete") as Button;
                btnDelete.Visible = false;
            }
        }

        #endregion

        /// <summary>
        /// Load Driver lists based on InventoryId
        /// </summary>
        /// <param name="inventoryId"></param>
        private void LoadData(long inventoryId)
        {
            gvDriver.DataSource = InventoryBAL.SearchInventoryDriverByInventoryId(inventoryId, Constant.OrgID);
            gvDriver.DataBind();
            if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
            {
                lnkAddNewDriver.Visible = false;
                if (gvDriver.Rows.Count > 0)
                    gvDriver.Columns[5].Visible = false;
            }
        }

        /// <summary>
        /// Handle Paging event of grid for providing paging in gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvDriver_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDriver.PageIndex = e.NewPageIndex;
            gvDriver.DataSource = InventoryBAL.SearchInventoryDriverByInventoryId(Code, Constant.OrgID);
            gvDriver.DataBind();
        }

        /// <summary>
        /// Add/Edit Driver Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveDriver_Click(object sender, EventArgs e)
        {
            if (Code == -1)
                return;

            InventoryDriver objInvDriver = new InventoryDriver();
            //These details,outside loop will be same for both add/edit
            objInvDriver.StartLocation = txtStartLocation.Text.Trim();
            objInvDriver.EndLocation = txtEndLocation.Text.Trim();
            if (!string.IsNullOrEmpty(txtStartDate.Text))
                objInvDriver.StartLocationDate = Convert.ToDateTime(txtStartDate.Text.Trim());

            if (!string.IsNullOrEmpty(txtEndDate.Text))
                objInvDriver.EndLocationDate = Convert.ToDateTime(txtEndDate.Text.Trim());

            objInvDriver.InventoryId = Code;

            //Add New Driver Details in InventoryDriver Table
            if (hdUpdateDriverId.Value == "-1")
            {
                objInvDriver.DriverId = Convert.ToInt64(ddlDrivers.SelectedValue);

                objInvDriver.AddedBy = Constant.UserId;
                objInvDriver.DateAdded = DateTime.Now;
                objInvDriver.EntityTypeID = 5;
                //CALL BAL  Method to Add New Inventory Driver
                InventoryBAL.AddInventoryDriver(objInvDriver);

            }

            //Update Driver Details in InventoryDriver Details Table
            else
            {
                //No Need to Nullify AddedBy & AddedDate as they are passed null from DAL
                objInvDriver.DriverId = null;
                objInvDriver.ModifiedBy = Constant.UserId;
                objInvDriver.DateModified = DateTime.Now;
                InventoryBAL.UpdateInventoryDriver(objInvDriver, Convert.ToInt64(hdUpdateDriverId.Value));
            }

            //Refresh data in GridView for Added/Updated Record
            LoadData(Code);
        }

        /// <summary>
        /// Handle LinkButton Click event to open a Popup for AddDriver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkAddNewDriver_Click(object sender, EventArgs e)
        {
            //return if inventoryid not provided
            if (Code == -1)
                return;

            //Set HiddeField for DriverId=-1,Its got value only in Update Mode
            hdUpdateDriverId.Value = "-1";

            //Change Popup Heading
            lblHeading.Text = "Add Driver";

            //Show HTML Table row containing dropdownlist for associating new driver with Inventory
            trDrivers.Visible = true;

            //Disable HTML Table row for display driver name
            trDriverName.Visible = false;

            //Reset Fields
            ResetFields();
            //Open Driver
            MPEAddDriver.Show();
        }
        /// <summary>
        /// Reset Driver Popup Fields
        /// </summary>
        private void ResetFields()
        {
            txtEndDate.Text = string.Empty;
            txtStartDate.Text = string.Empty;
            txtStartLocation.Text = string.Empty;
            txtEndLocation.Text = string.Empty;
            ddlDrivers.SelectedIndex = -1;
        }

        /// <summary>
        /// Handle Edit Image Button Click to open Driver Popup in edit Mode,with current record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ibtnDriverEdit_Click(object sender, ImageClickEventArgs e)
        {
            Expense objExp = new Expense();
            ImageButton imgBtnDriverEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgBtnDriverEdit.NamingContainer;

            //Set InventoryDriverId for Which Pop needs to be open in edit mode
            long InventoryDriverId = Convert.ToInt64(gvDriver.DataKeys[row.RowIndex].Value);

            //Set This inventoryId in Hidden Field to update record
            hdUpdateDriverId.Value = InventoryDriverId.ToString();

            //Hide HTML Table row containing dropdownlist for drivers.
            trDrivers.Visible = false;

            //Enable HTML Table row for display driver name whose record is currently being edited
            trDriverName.Visible = true;

            //Set Driver Name whose record is currently being edited
            if (!string.IsNullOrEmpty(row.Cells[0].Text.Trim()))
                lblInvDriverName.Text = row.Cells[0].Text.Trim();
            else
                lblInvDriverName.Text = null;
            //Change Popup Heading
            lblHeading.Text = "Edit Inventory Driver Details";

            //Fill All Control values for the driver row data clicked by user
            if (row.Cells[1].Text != "&nbsp;")
                txtStartLocation.Text = row.Cells[1].Text.Trim();
            if (row.Cells[2].Text != "&nbsp;")
                txtEndLocation.Text = row.Cells[2].Text.Trim();
            if (row.Cells[3].Text != "&nbsp;")
                txtStartDate.Text = row.Cells[3].Text.Trim();
            if (row.Cells[4].Text != "&nbsp;")
                txtEndDate.Text = row.Cells[4].Text.Trim();

            //Open Popup In Edit Mode
            MPEAddDriver.Show();

        }

        /// <summary>
        /// Handle Image button event to delete Inventory Driver(Mark IsActive=0)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ibtnDriverDelete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btnDeleteDriver = (ImageButton)sender;
            //lblHeading.Text = "Edit Expense Details";
            GridViewRow row = (GridViewRow)btnDeleteDriver.NamingContainer;
            long InvDriverIdToDelete = Convert.ToInt64(gvDriver.DataKeys[row.RowIndex].Value);

            //Set arributes deletedby/datedeleted as going to delete expense(IsActive=0)
            InventoryDriver objDriver = new InventoryDriver();
            objDriver.IsActive = 0;
            objDriver.DeletedBy = Constant.UserId;
            objDriver.DateDeleted = DateTime.Now;

            //Call BAL Method to delete expense (Marke IsActive=0 in InventoryDriver table
            InventoryBAL.DeleteInventoryDriver(objDriver, InvDriverIdToDelete);

            //Refresh Page
            LoadData(Code);
        }
        /// <summary>
        /// Handle Button Click event for move back to parent screen i.e ManageInventory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("InventoryDetail.aspx?Code=" + Code);
        }
    }
}
