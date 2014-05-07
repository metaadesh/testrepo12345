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
    public partial class ExpenseVsDepositReport : System.Web.UI.Page
    {
        decimal ExpenseTotal=0;
        decimal DepositTotal=0;

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
                //txtStartDate.Text = DateTime.Today.AddMonths(-3).ToShortDateString();
                //txtEndDate.Text = DateTime.Today.ToShortDateString();

                txtStartDate.Text = "03/03/1930";
                txtEndDate.Text = "01/01/1998";
                BindGrid();
            }
        }

        #region[BindGrid]

        public void BindGrid()
        {
            ObjectDataSource ObjectDataSource1 = new ObjectDataSource();
            ObjectDataSource1.SelectParameters.Add("FromDate", DbType.DateTime, txtStartDate.Text);
            ObjectDataSource1.SelectParameters.Add("ToDate", DbType.DateTime, txtEndDate.Text);
            ObjectDataSource1.SelectParameters.Add("Filter", DbType.String, ddlfilter.SelectedItem.Value);
            ObjectDataSource1.SelectParameters.Add("startRowIndex", DbType.Int32, gvExpensebyMonthly_Daily.PageIndex.ToString());
            ObjectDataSource1.SelectParameters.Add("maximumRows", DbType.Int32, gvExpensebyMonthly_Daily.PageSize.ToString());
            ObjectDataSource1.TypeName = "METAOPTION.BAL.Accounting_ExpenseDepositBAL";
            ObjectDataSource1.SelectMethod = "GetFinanceReportData";
            ObjectDataSource1.SelectCountMethod = "GetFinanceReportDataCount";
            ObjectDataSource1.Selected += new ObjectDataSourceStatusEventHandler(ObjectDataSource1_Selected);
            ObjectDataSource1.EnablePaging = true;
            gvExpensebyMonthly_Daily.DataSource = ObjectDataSource1;
            gvExpensebyMonthly_Daily.DataBind();

            
            
        }

        void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue.GetType().Name == "Int32")
            {
                Int32 count = Convert.ToInt32(e.ReturnValue);

                Int32 pagesize = gvExpensebyMonthly_Daily.PageSize;
                Int32 pagecount = count / pagesize;

               
                // one more page if there is any remender
                if ((count % pagesize) > 0) pagecount++;
             }
        }


        #endregion

        protected void btnView_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void gvExpensebyMonthly_Daily_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal row_exptotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ExpenseAmount"));
                decimal row_deptotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DepositAmount"));
                ExpenseTotal += row_exptotal;
                DepositTotal += row_deptotal;
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_exptotal =(Label)e.Row.FindControl("lblexptotal");
                Label lbl_deptotal = (Label)e.Row.FindControl("lbldeposittotal");

                lbl_exptotal.Text = String.Format("{0:C}",ExpenseTotal);
                lbl_deptotal.Text = String.Format("{0:C}",DepositTotal); 
            }
        }

        
        protected void gvExpensebyMonthly_Daily_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvExpensebyMonthly_Daily.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }

    
}
