using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using METAOPTION.BAL;

namespace METAOPTION.UserControls
{
    public partial class ExpenseDetails : System.Web.UI.UserControl
    {

        public Int32 EntityTypeID { get; set; }
        public Int64 EntityID { get; set; }

        PreExpenseBAL PreExpBAL = new PreExpenseBAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
            EntityLoginDetails();
            if ((Session["LoginEntityTypeID"] != null) && (Convert.ToString(Session["LoginEntityTypeID"]) == "3"))
            {
                grvexpensetypes.Columns[5].Visible = false;
            }
        }

        #region[Bind grid]
        protected void BindGrid()
        {


            String sort = String.Empty;

            if (String.IsNullOrEmpty(sort))
                sort = "ExpenseType Asc";

            ObjectDataSource odsExpense = new ObjectDataSource();
            odsExpense.Selected += new ObjectDataSourceStatusEventHandler(odsExpense_Selected);
            odsExpense.TypeName = "METAOPTION.BAL.PreExpenseBAL";
            odsExpense.SelectMethod = "SearchEntityExpenses";
            odsExpense.SelectCountMethod = "SearchEntityExpensesCount";
            odsExpense.EnablePaging = true;
            odsExpense.SelectParameters.Add("EntityId", Convert.ToString(EntityID));
            odsExpense.SelectParameters.Add("EntityTypeID", Convert.ToString(EntityTypeID));
            odsExpense.SelectParameters.Add("StartRowIndex", DbType.Int32, grvexpensetypes.PageIndex.ToString());
            odsExpense.SelectParameters.Add("MaximumRows", DbType.Int32, grvexpensetypes.PageCount.ToString());
            odsExpense.SelectParameters.Add("SortExpression", sort);
            grvexpensetypes.DataSource = odsExpense;
            grvexpensetypes.DataBind();
        }
        #endregion

        protected void odsExpense_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue == null) return;
            if (e.ReturnValue.GetType().Name == "Int32")
            {
                Int32 count = Convert.ToInt32(e.ReturnValue);
                Int32 pagesize = grvexpensetypes.PageSize;
                Int32 pagecount = count / pagesize;
                if ((count % pagesize) > 0) pagecount++;
            }
        }



        protected void ibtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            long EntityExpenseID = Convert.ToInt64(grvexpensetypes.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            PreExpBAL.DeleteEntityExpense(EntityExpenseID, Convert.ToInt64(Session["empId"]));
            BindGrid();
        }

        #region [Added by Rupendra 21 Dec 12 Vendor, Dealer and Buyer login details]
        public void EntityLoginDetails()
        {
            string EntityTypeID = string.Empty;
            if (Session["LoginEntityTypeID"] != null)
                EntityTypeID = Convert.ToString(Session["LoginEntityTypeID"]);
            if (EntityTypeID == "3")
            {
                foreach (GridViewRow gvr in grvexpensetypes.Rows)
                {
                    if (gvr.RowType == DataControlRowType.DataRow)
                    {
                        ImageButton imgbtnDelete = (ImageButton)gvr.FindControl("imgbtnDelete");
                        imgbtnDelete.Visible = false;
                    }
                }
            }
        }
        #endregion
    }
}