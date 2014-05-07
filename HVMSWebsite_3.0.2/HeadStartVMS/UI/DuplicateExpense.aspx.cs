using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using METAOPTION.BAL;
using System.Web.Security;

namespace METAOPTION.UI
{
    public partial class DuplicateExpense : System.Web.UI.Page
    {
        InventoryBAL bal = new InventoryBAL();
        long _EntityID = 0;
        int _EntityTypeID = 0;
        int _ExpenseTypeID = 0;
        String _Amount = "";
        int _Period = 0;
        long _InventoryID = 0;

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
            if (Request.QueryString.HasKeys())
            {
                _EntityID = Convert.ToInt64(Request.QueryString["C1"]);
                _EntityTypeID = Convert.ToInt32(Request.QueryString["C2"]);
                _ExpenseTypeID = Convert.ToInt32(Request.QueryString["C3"]);
                _Amount = Request.QueryString["C4"];
                _InventoryID = Convert.ToInt32(Request.QueryString["C5"]);
                _Period = Convert.ToInt32(Request.QueryString["C6"]);
            }
            if (!Page.IsPostBack)
                BindDuplicateExpenseGrid();
        }

        #region[Bind Duplicate Expense Grid]
        protected void BindDuplicateExpenseGrid()
        {
            gvDuplicateExpense.DataSource = bal.GetDuplicateExpenseDetails(_EntityID, _EntityTypeID, _ExpenseTypeID, _Amount, _InventoryID, _Period);
            gvDuplicateExpense.DataBind();
        }
        #endregion

        #region[Paging]
        protected void gvDuplicateExpense_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDuplicateExpense.PageIndex = e.NewPageIndex;
            BindDuplicateExpenseGrid();
        }
        #endregion
    }
}