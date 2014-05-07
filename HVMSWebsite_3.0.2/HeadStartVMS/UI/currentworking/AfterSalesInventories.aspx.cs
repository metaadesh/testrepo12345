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

namespace METAOPTION.UI
{
   
    public partial class AfterSalesInventories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                gvAfterSales.DataSource = objAfterSalesData;
                gvAfterSales.DataBind();
            }
            catch (Exception ex)
            {
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
            HiddenField hdCarCost = gvAfterSales.Rows[e.RowIndex].FindControl("hdCarCost") as HiddenField;
            //Label lblErrMsg = gvAfterSales.Rows[e.RowIndex].FindControl("lblErrMsg") as Label;

            //lblErrMsg.Text = "";

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

            decimal carCost=0;
            if(hdCarCost!=null)
             carCost = Convert.ToDecimal(hdCarCost.Value);
            decimal diff = carCost - soldPrice;

            //if (diff > 3000 || diff < -3000)
            //{
            //    ScriptManager.RegisterOnSubmitStatement(gvAfterSales, this.GetType(), "CheckAmountDifference", "CheckAmountDifference");
            //}
            //{
            //    lblErrMsg.Text = "The difference amount of car cost - sold price should not be > 3000 or < -3000. <br/>";
            //    ViewState["3000Check"] = "The difference amount of car cost - sold price should not be > 3000 or < -3000. <br/>";
            //}
            //else
            //    lblErrMsg.Text = "";

            //// (REFER: BUGID:732 Claudia Change Request)
            //if (!string.IsNullOrEmpty(txtDepositDate.Text))
            //    depositDate = Convert.ToDateTime(txtDepositDate.Text);

            //if (!string.IsNullOrEmpty(txtDepositAmount.Text))
            //    depositAmount = Convert.ToDecimal(txtDepositAmount.Text);

            //if (!string.IsNullOrEmpty(ddlBankAccountId.SelectedValue))
            //    bankAccountId = Convert.ToInt32(ddlBankAccountId.SelectedValue);
           

               int flagSuccess;

              // if (ViewState["3000Check"].ToString() == "")
              // {
                   //Update current inventory details in database
                   flagSuccess = DAL.AfterSalesManagementDAL.UpdateAfterSalesData(inventoryId, customerId, soldDate, soldPrice, mileageOut, depositDate, depositAmount,
                       bankAccountId, null, Convert.ToBoolean(ddlSoldStatusTable.SelectedValue), txtSoldComment.Text, Constant.UserId, DateTime.Now);

                   //Updated Successfully
                   if (flagSuccess == 0)
                   {
                       
                   }
                   else //Error Updating
                   {
                   }
               //}
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
                    ScriptManager.GetCurrent(this).SetFocus(txtCustomerName);

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
        
    }
}
