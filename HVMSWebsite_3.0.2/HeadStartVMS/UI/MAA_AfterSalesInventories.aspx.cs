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
   
    public partial class MAA_AfterSalesInventories : System.Web.UI.Page
    {
        public const string PAGE = "MAA_AFTERSALES";
        public const string MAA_AFTERSALES_MAA_AFTERSALES_VIEW = "MAA_AFTERSALES.VIEW";
        public const string MAA_AFTERSALES_MAA_AFTERSALES_EDIT = "MAA_AFTERSALES.EDIT";

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


        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserPagePermissions();
            //Set Gridview pagesize
            gvAfterSales.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
           
            if (!IsPostBack)
            {
                currentRowIndex = -1;
                BindData();
                ViewState["sortDirection"] = "ASC";
            }
            //Set this property to maintain scrollback position on postback
            Page.MaintainScrollPositionOnPostBack = true;

        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            GridViewRow pagerRow = gvAfterSales.BottomPagerRow;
            if (pagerRow != null)
            {
                pagerRow.Cells[0].Attributes.Add("align", "right");
                pagerRow.Cells[0].Attributes.Add("colspan", "16");
            }
        }
        #region CheckUserPagePermissions 
        /// <summary>
        /// This method checks Page Level Permission for logged-in user
        /// </summary>
        public void CheckUserPagePermissions()
        {
            MAAAfterSalesBAL objBal = new MAAAfterSalesBAL();
             //Check if lane automation is allowed for this organization
            if (objBal.IsMAAAllowed(Constant.OrgID))
            {
                //Get Permissions for User on this page
                System.Collections.Generic.List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, PAGE);

                //Check If any permission found for this page 
                if (dict == null || dict.Count < 1)
                    Response.Redirect("Permission.aspx?MSG=MAA_AfterSalesInventories:EDIT/VIEW");
            }
            else
                Response.Redirect("default.aspx");
        }

        #endregion

        /// <summary>
        /// handle click event of search button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvAfterSales.PageIndex = 0;
            BindData();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "FooterAdjust", "GetFooter()", true);
        }
        /// <summary>
        /// Bind After Sales Data in Gridview Control
        /// </summary>
        protected void BindData()
        {
            try
            {
                //Bind GridView Control
                gvAfterSales.DataSource = objMAAAfterSalesData;
                gvAfterSales.DataBind();
            }
            catch (Exception ex)
            {
                string exx = ex.Message + ex.InnerException.Message;
                throw ex;
            }
        }
       
       /// <summary>
       /// Handle gridview row updating event,this event fires when user Click on Update Button
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        protected void gvAfterSales_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Write Code Logic to update the current row values in db
            gvAfterSales.EditIndex = -1;

            long inventoryId = Convert.ToInt64(gvAfterSales.DataKeys[e.RowIndex].Value);

            TextBox txtSoldDate = gvAfterSales.Rows[e.RowIndex].FindControl("txtSoldDate") as TextBox;
            TextBox txtSoldPrice = gvAfterSales.Rows[e.RowIndex].FindControl("txtSoldPrice") as TextBox;
            TextBox txtMileageOut = gvAfterSales.Rows[e.RowIndex].FindControl("txtMieageOut") as TextBox;
            //BugId:732 Deposit Date and Amount are not updated from this screen from nowonwards as per claudia request
            //TextBox txtDepositDate =  gvAfterSales.Rows[e.RowIndex].FindControl("txtDepositDate") as TextBox;
            //TextBox txtDepositAmount = gvAfterSales.Rows[e.RowIndex].FindControl("txtDepositAmount") as TextBox;
            //DropDownList ddlBankAccountId = gvAfterSales.Rows[e.RowIndex].FindControl("ddlBankName") as DropDownList;
            //TextBox txtDepositComment = gvAfterSales.Rows[e.RowIndex].FindControl("txtDepositComment") as TextBox;
            DropDownList ddlSoldStatusTable = gvAfterSales.Rows[e.RowIndex].FindControl("ddlSoldStatusTable") as DropDownList;
            TextBox txtSoldComment = gvAfterSales.Rows[e.RowIndex].FindControl("txtSoldComment") as TextBox;
            HiddenField hdCustomerId = gvAfterSales.Rows[e.RowIndex].FindControl("hdCustomerId") as HiddenField;

            long customerId = 0;
            DateTime? soldDate = null;
            Decimal soldPrice = 0;
            long mileageOut = 0;
            DateTime? depositDate = null;
            Decimal depositAmount = 0;
            int bankAccountId = 0;

            if (hdCustomerId != null && hdCustomerId.Value != "")
                customerId = Convert.ToInt64(hdCustomerId.Value);

            if (!string.IsNullOrEmpty(txtSoldDate.Text))
                soldDate = Convert.ToDateTime(txtSoldDate.Text);

            if (!string.IsNullOrEmpty(txtSoldPrice.Text))
                soldPrice = Convert.ToDecimal(txtSoldPrice.Text);


            if (!string.IsNullOrEmpty(txtMileageOut.Text))
                mileageOut = Convert.ToInt64(txtMileageOut.Text);

            //// (REFER: BUGID:732 Claudia Change Request)
            //if (!string.IsNullOrEmpty(txtDepositDate.Text))
            //    depositDate = Convert.ToDateTime(txtDepositDate.Text);

            //if (!string.IsNullOrEmpty(txtDepositAmount.Text))
            //    depositAmount = Convert.ToDecimal(txtDepositAmount.Text);

            //if (!string.IsNullOrEmpty(ddlBankAccountId.SelectedValue))
            //    bankAccountId = Convert.ToInt32(ddlBankAccountId.SelectedValue);


            int flagSuccess;



            //Update current inventory details in database
            flagSuccess = DAL.AfterSalesManagementDAL.UpdateAfterSalesData(inventoryId, customerId, soldDate, soldPrice, mileageOut, depositDate, depositAmount,
                bankAccountId, null, Convert.ToInt32(ddlSoldStatusTable.SelectedValue), txtSoldComment.Text, Constant.UserId, DateTime.Now);

            //Updated Successfully
            if (flagSuccess == 0)
            {

            }
            else //Error Updating
            {
            }

            //Bind Data
            BindData();
            currentRowIndex = e.RowIndex;

            //Set Focus on Current Row
            ScriptManager.GetCurrent(this).SetFocus(gvAfterSales.Rows[e.RowIndex]);
            if (hdCustomerId != null)
                hdCustomerId.Value = "0";

            //Change Row Color, After Updating row data
            ScriptManager.RegisterClientScriptBlock(gvAfterSales, this.GetType(), "ChangeRowColor", "ChangeRowColor(2+(2 * " + Convert.ToString(e.RowIndex) + "))", true);
       
        }



        /// <summary>
        /// Tell current row index
        /// </summary>
        public int currentRowIndex
        {
            set
            {
                ViewState["CurrentRowIndex"] = value;
            }
            get
            {
                return Convert.ToInt32(ViewState["CurrentRowIndex"]);
            }
        }
        

        /// <summary>
        /// Handle Gridview row editing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvAfterSales_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAfterSales.EditIndex = e.NewEditIndex;
            BindData();

            //Set Current Row Highlighted and set focus on CustomerName textbox
            TextBox txtCustomerName = gvAfterSales.Rows[e.NewEditIndex].FindControl("txtCustomerName") as TextBox;
            if (txtCustomerName != null)
            {
                txtCustomerName.Text = txtCustomerName.Text.Trim();
                ScriptManager.GetCurrent(this).SetFocus(txtCustomerName);

            }

            //Set OrgID = context key of AutoCompleteExtender from Session
            AjaxControlToolkit.AutoCompleteExtender ace = gvAfterSales.Rows[e.NewEditIndex].FindControl("txtTest_AutoCompleteExtender") as AjaxControlToolkit.AutoCompleteExtender;
            ace.ContextKey = Constant.OrgID.ToString();

            currentRowIndex = gvAfterSales.EditIndex;

        }

        /// <summary>
        /// Handle Gridview RowCancelingEdit Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvAfterSales_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAfterSales.EditIndex = -1;
            BindData();
           
        }


        
        /// <summary>
        /// Handle Page Index changing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvAfterSales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAfterSales.PageIndex = e.NewPageIndex;
            BindData();
        }
        /// <summary>
        /// Handle Gridview Sorting Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvAfterSales_Sorting(object sender, GridViewSortEventArgs e)
        {
            //Set Sort Expression for Dealer and Customer with Aliases
            if (e.SortExpression == "CustomerName")
                e.SortExpression = "D2.DealerName";

            if (e.SortExpression == "DealerName")
                e.SortExpression = "D1.DealerName";

            //Save Column Name to Sort On
            ViewState["sortExpression"] = e.SortExpression;

            //Save Sort Direction
            if( Convert.ToString(ViewState["sortDirection"]) == "ASC")
               ViewState["sortDirection"] = "DESC";
             else
                ViewState["sortDirection"] = "ASC";

           //Call Bind method 
           BindData();
        }


        /// <summary>
        /// Handle object datasource selecting event for setting sortexpression and sortdirection params values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void objAfterSalesData_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
         
            //Set Sort Expression and Sort Direction in method
            if (ViewState["sortExpression"] != null)
                e.InputParameters["sortExpression"] = Convert.ToString(ViewState["sortExpression"]);
           
            if(ViewState["sortDirection"]!=null)
              e.InputParameters["sortDirection"] = Convert.ToString(ViewState["sortDirection"]);

            //Set Default to RegularLane with Ascending Order
            if (ViewState["sortExpression"] == null)
            {
                e.InputParameters["sortExpression"] = "RegularLane";
                e.InputParameters["sortDirection"] = "ASC";
            }

         
        }

        protected void gvAfterSales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           GridViewRow row=  e.Row;

           HtmlTableRow trMAA = row.FindControl("trMAA") as HtmlTableRow;
           if (trMAA == null)
               return;
          //Hide MAA Row for normal inventory data
           if (!chkMAA_Records.Checked)
               trMAA.Visible = false;
           else
               trMAA.Visible = true;
                      
        }
        /// <summary>
        /// Populate MAA Data into main fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMAAUpdate_Click(object sender, EventArgs e)
        {
            Button btnMAAUpdate = (sender) as Button;
            GridViewRow selRow = (GridViewRow)btnMAAUpdate.NamingContainer;
            ////Search MAA Controls, Swap values with orignal textboxes for updating in db, if any values
            Label lblMAASoldDate = selRow.FindControl("lblMAASoldDate") as Label;
            Label lblMAASoldPrice = selRow.FindControl("lblMAASoldPrice") as Label;
            Label lblMileageIn = selRow.FindControl("lblMileageIn") as Label;
            TextBox txtSoldDate = selRow.FindControl("txtSoldDate") as TextBox;
            TextBox txtSoldPrice = selRow.FindControl("txtSoldPrice") as TextBox;
            TextBox txtMileageOut = selRow.FindControl("txtMieageOut") as TextBox;
            HiddenField hdCustomerId = selRow.FindControl("hdCustomerId") as HiddenField;

           
            //if SoldPrice,SoldDate,CustomerId is blank then only Populate MAA Data
            if ((txtSoldPrice.Text.Trim() == "" || txtSoldPrice.Text == "0" || txtSoldPrice.Text == "0.00") &&
                txtSoldDate.Text.Trim() == ""  &&
                (hdCustomerId.Value.Trim() == "" || hdCustomerId.Value == "0") &&
                (txtMileageOut.Text.Trim() == "" || txtMileageOut.Text == "0" || txtMileageOut.Text == "0.00"))
            {
                txtSoldPrice.Text  = lblMAASoldPrice.Text;
                txtSoldDate.Text   = lblMAASoldDate.Text;
                txtMileageOut.Text = lblMileageIn.Text;
            }
            

        }

        //protected void chkMAA_Records_CheckedChanged(object sender, EventArgs e)
        //{
        //    BindData();
        //}
        
    }
}
