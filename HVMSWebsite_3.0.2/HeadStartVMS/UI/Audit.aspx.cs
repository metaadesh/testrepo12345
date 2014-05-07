using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using METAOPTION.BAL;
using System.Data;

namespace METAOPTION.UI
{
    public partial class Audit : System.Web.UI.Page
    {
        CommonBAL bal = new CommonBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindAuditBy();
                BindTable();
                txtAddedOnFrom.Text = System.DateTime.Today.AddDays(-7).ToShortDateString();
                txtAddedOnTo.Text = System.DateTime.Today.ToShortDateString();
                BindAuditGrid();
            }
        }

        #region[Bind Modified By]
        private void BindAuditBy()
        {
            ddlModifiedBy.DataSource = bal.GetAuditBy(Constant.OrgID);
            ddlModifiedBy.DataTextField = "DisplayName";
            ddlModifiedBy.DataValueField = "SecurityUserID";
            ddlModifiedBy.DataBind();
            ddlModifiedBy.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        #endregion

        #region[Bind Table]
        protected void BindTable()
        {
            ddlTable.DataSource = bal.GetAuditTables();
            ddlTable.DataTextField = "TableName";
            ddlTable.DataValueField = "TableID";
            ddlTable.DataBind();
            ddlTable.Items.Insert(0, new ListItem("ALL", "-1"));
        }

        protected void ddlTable_SelectedIndexChanged(object sender,EventArgs e)
        {
            BindColumns();
        }
        #endregion

        #region[Bind Columns]
        protected void BindColumns()
        {
            ddlColumn.DataSource = bal.GetAuditColumns(Convert.ToInt32(ddlTable.SelectedValue));
            ddlColumn.DataTextField = "ColumnName";
            ddlColumn.DataValueField = "ColumnID";
            ddlColumn.DataBind();
            ddlColumn.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        #endregion

        #region[Bind Audit Grid]
        protected void BindAuditGrid()
        {
            DateTime DateFrom = System.DateTime.Today.AddDays(-7);
            DateTime DateTo = System.DateTime.Today;
            if (!String.IsNullOrEmpty(txtAddedOnFrom.Text))
                DateFrom = Convert.ToDateTime(txtAddedOnFrom.Text);
            if (!String.IsNullOrEmpty(txtAddedOnTo.Text))
                DateTo = Convert.ToDateTime(txtAddedOnTo.Text);

            string strSort = "ModifiedDate DESC";
            if (ViewState["sortDirection"] != null)
            {
                if (Convert.ToString(ViewState["sortDirection"]).ToLower() == "ascending")
                    strSort = ViewState["SortItem"].ToString() + " asc";
                else
                    strSort = ViewState["SortItem"].ToString() + " desc";
            }

            String Column = ddlColumn.SelectedValue;
            if (String.IsNullOrEmpty(ddlColumn.SelectedValue))
                Column = "-1";

            String rowID = "-1";
            if (!String.IsNullOrEmpty(txtRowID.Text.Trim()))
                rowID = txtRowID.Text.Trim();

            ObjectDataSource odsHistory = new ObjectDataSource();
            odsHistory.TypeName = "METAOPTION.BAL.CommonBAL";
            odsHistory.SelectMethod = "SearchAudit";
            odsHistory.SelectCountMethod = "SearchAuditCount";
            odsHistory.EnablePaging = true;
            odsHistory.SelectParameters.Add("TableID", ddlTable.SelectedValue);
            odsHistory.SelectParameters.Add("ColumnID", Column);
            odsHistory.SelectParameters.Add("RowID", rowID);
            odsHistory.SelectParameters.Add("DateFrom", Convert.ToString(DateFrom));
            odsHistory.SelectParameters.Add("DateTo", Convert.ToString(DateTo));
            odsHistory.SelectParameters.Add("ModifiedBy", ddlModifiedBy.SelectedValue);
            odsHistory.SelectParameters.Add("Source", txtSource.Text);
            odsHistory.SelectParameters.Add("UpdatedFrom", txtUpdatedFrom.Text);
            odsHistory.SelectParameters.Add("StartRowIndex", DbType.Int32, gvAudit.PageIndex.ToString());
            odsHistory.SelectParameters.Add("MaximumRows", DbType.Int32, gvAudit.PageSize.ToString());
            odsHistory.SelectParameters.Add("OrderBy", strSort);
            odsHistory.SelectParameters.Add("OrgID", Constant.OrgID.ToString());
            gvAudit.DataSource = odsHistory;
            gvAudit.DataBind();
        }
        #endregion

        #region[Paging]
        protected void gvAudit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAudit.PageIndex = e.NewPageIndex;
            BindAuditGrid();
        }
        #endregion

        #region[Search]
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvAudit.PageIndex = 0;
            BindAuditGrid();
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

        protected void gvAudit_Sorting(object sender, GridViewSortEventArgs e)
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
            BindAuditGrid();
        }
        #endregion
    }
}