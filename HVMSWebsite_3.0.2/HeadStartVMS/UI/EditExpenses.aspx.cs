using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using METAOPTION.BAL;


namespace METAOPTION.UI
{
    public partial class EditExpenses : System.Web.UI.Page
    {
        PreExpenseBAL PreExpBal = new PreExpenseBAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            long EntityExpenseID = Convert.ToInt64(Request["ID"]);
            Int32 Status=-1;
            if (grdexpensesDetails.Rows.Count > 0)
            {

                GridViewRow grdrow = (GridViewRow)grdexpensesDetails.Rows[0];
                DropDownList ddlExpenseType = grdrow.FindControl("ddlExpenseType") as DropDownList;
                TextBox txtmincount = grdrow.FindControl("txtMinCount") as TextBox;
                TextBox txtmaxcount = grdrow.FindControl("txtmaxcount") as TextBox;
                TextBox txtdefaultprice = grdrow.FindControl("txtdefaultprice") as TextBox;
                Status = PreExpBal.UpdateEntityExpense(EntityExpenseID,
                    Convert.ToInt32(ddlExpenseType.SelectedItem.Value), 
                    String.IsNullOrEmpty(txtmincount.Text)?0:Convert.ToInt32(txtmincount.Text), 
                    String.IsNullOrEmpty(txtmaxcount.Text)?0:Convert.ToInt32(txtmaxcount.Text), 
                    String.IsNullOrEmpty(txtdefaultprice.Text)?0:Convert.ToDecimal(txtdefaultprice.Text), 
                    Convert.ToInt64(Session["empId"]));
            }
            if(Status==1)
                this.Page.ClientScript.RegisterStartupScript(typeof(EditExpenses), "closeThickBox", "self.parent.updated();", true);
            else if(Status==0)
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Expense Type Already Exists.Please Select Another Expense Type.');", true);
            
        }
    }
}