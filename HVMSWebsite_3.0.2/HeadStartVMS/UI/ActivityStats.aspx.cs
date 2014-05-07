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
    public partial class ActivityStats : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Remove("SortPreference");
            if (!Page.IsPostBack)
                BindData();
        }

        #region[Bind Data]
        protected void BindData()
        {
            DateTime DateFrom = System.DateTime.Today.AddDays(-7);
            DateTime DateTo = System.DateTime.Today;
            #region[1:Today]
            if (ddlSortFilter.SelectedValue == "1")
            {
                DateFrom = System.DateTime.Today;
            }
            #endregion
            #region[2:This Week]
            if (ddlSortFilter.SelectedValue == "2")
            {
                //Find first date of current week(monday)
                DateTime mondayDate = DateTime.Today.AddDays(((int)(DateTime.Today.DayOfWeek) * -1) + 1);
                DateFrom = mondayDate;
            }
            #endregion
            #region[3:Last One Week]
            if (ddlSortFilter.SelectedValue == "3")
            {
                DateFrom = System.DateTime.Today.AddDays(-7);
            }
            #endregion
            #region[4:This Month]
            if (ddlSortFilter.SelectedValue == "4")
            {
                // Find first date of current month
                DateTime startOfMonth = new DateTime(System.DateTime.Today.Year, System.DateTime.Today.Month, 1);
                DateFrom = startOfMonth;
            }
            #endregion
            #region[5:Last One Month]
            if (ddlSortFilter.SelectedValue == "5")
            {
                DateFrom = System.DateTime.Today.AddMonths(-1);
            }
            #endregion
            #region[6:This Year]
            if (ddlSortFilter.SelectedValue == "6")
            {
                //Find first date of current year
                DateTime startOfYear = new DateTime(System.DateTime.Today.Year, 1, 1);
                DateFrom = startOfYear;
            }
            #endregion
            #region[7:Last One Year]
            if (ddlSortFilter.SelectedValue == "7")
            {
                DateFrom = System.DateTime.Today.AddYears(-1);
            }
            #endregion
            #region[8:Date Range]
            if (ddlSortFilter.SelectedValue == "8")
            {
                if (String.IsNullOrEmpty(txtDateFrom.Text))
                    txtDateFrom.Text = Convert.ToString(DateFrom);
                if (String.IsNullOrEmpty(txtDateTo.Text))
                    txtDateTo.Text = Convert.ToString(DateTo);

                DateFrom = Convert.ToDateTime(txtDateFrom.Text.Trim());
                DateTo = Convert.ToDateTime(txtDateTo.Text.Trim());
            }
            #endregion

            ActivityBAL objBAL = new ActivityBAL();
            DataTable dt = objBAL.FetchActivityStats(DateFrom, DateTo);
            DataTable dtbl = objBAL.FetchLocationActivityStats();

            rptActivityStats.DataSource = dt;
            rptActivityStats.DataBind();

            rptLocationStats.DataSource = dtbl;
            rptLocationStats.DataBind();
        }
        #endregion

        #region[Sort button click event]
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            Session["SortPreference"] = ddlSortFilter.SelectedValue;
            if (ddlSortFilter.SelectedValue == "8")
            {
                Session["StartDate"] = txtDateFrom.Text;
                Session["EndDate"] = txtDateTo.Text;
            }
            else
            {
                Session.Remove("StartDate");
                Session.Remove("EndDate");
            }
            BindData();
        }
        #endregion        

        #region[Enable/disable link]
        public Boolean EnabledDisabledLink(object Count)
        {
            if (Count is DBNull)
                return false;
            else if (Convert.ToInt64(Count) > 0 && Count!=null)
                return true;
            else
                return false;
        }
        #endregion
        
    }
}