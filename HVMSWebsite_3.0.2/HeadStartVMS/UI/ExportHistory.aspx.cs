using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using METAOPTION.BAL;

namespace METAOPTION.UI
{
    public partial class ExportHistory : System.Web.UI.Page
    {
        CommonBAL bal = new CommonBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindExportedBy();
                txtAddedOnFrom.Text = System.DateTime.Today.AddDays(-7).ToShortDateString();
                txtAddedOnTo.Text = System.DateTime.Today.ToShortDateString();
                BindHistoryGrid();
            }
        }

        #region[Bind Exported By]
        private void BindExportedBy()
        {
            ddlExportedBy.DataSource = bal.GetExportedBy(Constant.OrgID);
            ddlExportedBy.DataTextField = "DisplayName";
            ddlExportedBy.DataValueField = "SecurityUserID";
            ddlExportedBy.DataBind();
            ddlExportedBy.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        #endregion

        #region[Bind Export History Grid]
        protected void BindHistoryGrid()
        {
            DateTime DateFrom = System.DateTime.Today.AddDays(-7);
            DateTime DateTo = System.DateTime.Today;
            if (!String.IsNullOrEmpty(txtAddedOnFrom.Text))
                DateFrom = Convert.ToDateTime(txtAddedOnFrom.Text);
            if (!String.IsNullOrEmpty(txtAddedOnTo.Text))
                DateTo = Convert.ToDateTime(txtAddedOnTo.Text);

            string strSort = "DateAdded DESC";
            if (ViewState["sortDirection"] != null)
            {
                if (Convert.ToString(ViewState["sortDirection"]).ToLower() == "ascending")
                    strSort = ViewState["SortItem"].ToString() + " asc";
                else
                    strSort = ViewState["SortItem"].ToString() + " desc";
            }

            String FileName = txtFileName.Text;
            long ExportedBy = Convert.ToInt64(ddlExportedBy.SelectedValue);
            gvExportHistory.DataSource = bal.SearchExportHistory(FileName, DateFrom, DateTo, ExportedBy, strSort, Constant.OrgID);
            gvExportHistory.DataBind();
        }
        #endregion

        #region[Paging]
        protected void gvExportHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvExportHistory.PageIndex = e.NewPageIndex;
            BindHistoryGrid();
        }
        #endregion

        #region[Search]
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvExportHistory.PageIndex = 0;
            BindHistoryGrid();
        }
        #endregion

        #region[Sorting]
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] != null)
                    return (SortDirection)ViewState["sortDirection"];
                else
                    return SortDirection.Ascending;
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected void gvExportHistory_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                string sortExpression = e.SortExpression;
                ViewState["SortItem"] = e.SortExpression;
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    GridViewSortDirection = SortDirection.Descending;
                }
                else
                {
                    GridViewSortDirection = SortDirection.Ascending;
                }
            }
            catch (Exception ex) { }
            BindHistoryGrid();
        }
        #endregion
    }
}