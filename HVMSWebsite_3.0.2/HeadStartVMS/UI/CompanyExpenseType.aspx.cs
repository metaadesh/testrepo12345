using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;

namespace METAOPTION.UI
{
    public partial class CompanyExpenseType : System.Web.UI.Page
    {
        public string EntityId = string.Empty;
        public string type = string.Empty;
        METAOPTION.BAL.PreExpenseBAL preExpenseBAL = new BAL.PreExpenseBAL();
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
            if (!IsPostBack)
            {
                if (Session["empId"] != null && Session["LoginEntityTypeID"] != null)
                {
                    EntityId = Convert.ToString(Session["empId"]);
                    type = Convert.ToString(Session["LoginEntityTypeID"]);
                }
                BindExpenseType();                
                BindGrid(0);


            }

        }

        protected void BindExpenseType()
        {
           
            List<ExpenseType_ver211Result> list = preExpenseBAL.GetExpenseTyepes();
            ddlExpenseType.DataSource = list;
            ddlExpenseType.DataValueField = "ExpenseTypeId";
            ddlExpenseType.DataTextField = "ExpenseType";
            ddlExpenseType.DataBind();
            ddlExpenseType.Items.Insert(0, new ListItem("All", "0"));
        }
        #region[Bind grid]
        protected void BindGrid(Int32 pageIndex)
        {

            String SyncDateFrom, SyncDateTo;
            if (String.IsNullOrEmpty(txtSyncDateFrom.Text))
                SyncDateFrom = DateTime.Today.AddYears(-100).ToShortDateString();
            else
                SyncDateFrom = txtSyncDateFrom.Text;

            if (String.IsNullOrEmpty(txtSyncDateTo.Text))
                SyncDateTo = DateTime.Today.AddDays(1).ToShortDateString();
            else
                SyncDateTo = Convert.ToDateTime(txtSyncDateTo.Text).AddDays(1).ToShortDateString();

            String sort = String.Empty;

            if (String.IsNullOrEmpty(sort))
                sort = "ExpenseType Asc";
            String ExType = ddlExpenseType.SelectedValue == "0" ? "-1" : ddlExpenseType.SelectedValue;
            String Price = String.IsNullOrEmpty(txtPrice.Text) ? "-1" : txtPrice.Text;
            ObjectDataSource odsExpense = new ObjectDataSource();
            odsExpense.Selected += new ObjectDataSourceStatusEventHandler(odsExpense_Selected);
            odsExpense.TypeName = "METAOPTION.BAL.PreExpenseBAL";
            odsExpense.SelectMethod = "SearchEntityExpenses_ByFilter";
            odsExpense.SelectCountMethod = "SearchEntityExpensesCount_ByFilter";
            odsExpense.EnablePaging = true;
            odsExpense.SelectParameters.Add("EntityId", Convert.ToString(Session["empId"]));
            odsExpense.SelectParameters.Add("EntityTypeID", Convert.ToString(Session["LoginEntityTypeID"]));
            odsExpense.SelectParameters.Add("ExpenseType" ,DbType.Int32, ExType);
            odsExpense.SelectParameters.Add("Fromdate", SyncDateFrom);
            odsExpense.SelectParameters.Add("ToDate", SyncDateTo);
            odsExpense.SelectParameters.Add("Price", DbType.Int32, Price);
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid(0);

        }
        protected void grvexpensetypes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvexpensetypes.PageIndex = e.NewPageIndex;
            BindGrid(grvexpensetypes.PageIndex);
        }
        protected void ibtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            long EntityExpenseID = Convert.ToInt64(grvexpensetypes.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            preExpenseBAL.DeleteEntityExpense(EntityExpenseID, Convert.ToInt64(Session["empId"]));
            BindGrid(0);
        }

        protected void grvexpensetypes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton ImgBtn = (ImageButton)e.Row.FindControl("imgbtnDelete");
                if (Convert.ToString(Session["LoginEntityTypeID"]) == "3")
                {
                    ImgBtn.Enabled = false;
                }    
            }
        }
    }
}