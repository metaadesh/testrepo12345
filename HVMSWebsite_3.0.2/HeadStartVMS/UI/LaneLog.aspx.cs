using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace METAOPTION.UI
{
    public partial class LaneLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindAddedBy();
                BindTableFields();
                txtAddedOnFrom.Text = System.DateTime.Today.AddDays(-7).ToShortDateString();
                txtAddedOnTo.Text = System.DateTime.Today.ToShortDateString();
                BindLaneLogGrid();
            }
        }

        #region[Bind Table Fields]
        protected void BindTableFields()
        {
            METAOPTION.BAL.LaneAssignmentBAL bal = new BAL.LaneAssignmentBAL();
            ddlField.DataSource = bal.GetTableFields();
            ddlField.DataTextField = "FieldName";
            ddlField.DataValueField = "FieldID";
            ddlField.DataBind();
            ddlField.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        #endregion

        #region[Bind Added By]
        private void BindAddedBy()
        {
            BAL.LaneAssignmentBAL bal = new BAL.LaneAssignmentBAL();
            ddlAddedBy.DataSource = bal.GetLaneHistoryAddedBy(Constant.OrgID);
            ddlAddedBy.DataTextField = "DisplayName";
            ddlAddedBy.DataValueField = "SecurityUserID";
            ddlAddedBy.DataBind();
            ddlAddedBy.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        #endregion

        #region[Bind Lane Log Grid]
        private void BindLaneLogGrid()
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

            gvLaneLog.PageSize = Convert.ToInt32(ddlPageSize1.SelectedValue);

            ObjectDataSource odsHistory = new ObjectDataSource();
            odsHistory.TypeName = "METAOPTION.BAL.LaneAssignmentBAL";
            odsHistory.SelectMethod = "SearchLaneHistory";
            odsHistory.SelectCountMethod = "SearchLaneHistoryCount";
            odsHistory.EnablePaging = true;
            odsHistory.SelectParameters.Add("InvID", txtInventoryID.Text == "" ? "-1" : txtInventoryID.Text);
            odsHistory.SelectParameters.Add("FieldID", ddlField.SelectedValue);
            odsHistory.SelectParameters.Add("DateFrom", Convert.ToString(DateFrom));
            odsHistory.SelectParameters.Add("DateTo", Convert.ToString(DateTo));
            odsHistory.SelectParameters.Add("AddedBy", ddlAddedBy.SelectedValue);
            odsHistory.SelectParameters.Add("Source", txtSource.Text);
            odsHistory.SelectParameters.Add("UpdateFrom", txtUpdatedFrom.Text);
            odsHistory.SelectParameters.Add("StartRowIndex", DbType.Int32, gvLaneLog.PageIndex.ToString());
            odsHistory.SelectParameters.Add("MaximumRows", DbType.Int32, gvLaneLog.PageSize.ToString());
            odsHistory.SelectParameters.Add("OrderBy", strSort);
            odsHistory.SelectParameters.Add("OrgID", Constant.OrgID.ToString());
            gvLaneLog.DataSource = odsHistory;
            gvLaneLog.DataBind();
        }
        #endregion

        #region[Paging]
        protected void gvLaneLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLaneLog.PageIndex = e.NewPageIndex;
            BindLaneLogGrid();
        }
        #endregion

        #region[Search]
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvLaneLog.PageIndex = 0;
            BindLaneLogGrid();
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

        protected void gvLaneLog_Sorting(object sender, GridViewSortEventArgs e)
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
            BindLaneLogGrid();
        }
        #endregion
      
        #region[Page size selection change]
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvLaneLog.PageIndex = 0;
            BindLaneLogGrid();
        }
        #endregion


    }
}