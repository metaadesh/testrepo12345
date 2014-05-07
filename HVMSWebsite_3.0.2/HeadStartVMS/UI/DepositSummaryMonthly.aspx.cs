using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.IO;
using System.Text;
using System.Drawing.Imaging;
using System.Web.UI.HtmlControls;
using System.Net;
using System.ComponentModel;
using System.Web.Security;
using METAOPTION.BAL;
using System.Collections;

namespace METAOPTION.UI
{
    public partial class DepositSummaryMonthly : System.Web.UI.Page
    {
        InventoryDepositSearchBAL objDeposit = new InventoryDepositSearchBAL();
        DataSet ds = new DataSet();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["MasterPageNoLeftPanel"])))
                    this.MasterPageFile = Convert.ToString(Session["MasterPageNoLeftPanel"]);
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
            CheckPermission();

            if (!Page.IsPostBack)
            {
                FillDefaultValues();
                BindGrid();
            }
        }

        private void CheckPermission()
        {
            List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, "INVENTORY");

            if (!dict.Contains("DEPOSIT.VIEW"))
                Response.Redirect("Permission.aspx?MSG=DEPOSIT.VIEW", true);
        }

        private void FillDefaultValues()
        {
            fillMonth(ddlMonthFrom);
            fillYear(ddlYearFrom);

            fillMonth(ddlMonthTo);
            fillYear(ddlYearTo);

            //taking last 6 months data

            ddlMonthFrom.SelectedValue = DateTime.Now.AddMonths(-6).Month.ToString();
            ddlYearFrom.SelectedValue = DateTime.Now.AddMonths(-6).Year.ToString();

            ddlMonthTo.SelectedValue = DateTime.Now.Month.ToString();
            ddlYearTo.SelectedValue = DateTime.Now.Year.ToString();

            //txtDateTo.Text = DateTime.Today.ToString("MM/dd/yyyy");
            //txtDateFrom.Text = DateTime.Today.AddDays(-28).ToString("MM/dd/yyyy");

        }

        private void fillMonth(DropDownList ddl)
        {
            System.Collections.Generic.List<string> monthNames = System.Globalization.DateTimeFormatInfo.CurrentInfo.MonthNames.Take(12).ToList();
            var monthSelectList = monthNames.Select(m => new { ID = monthNames.IndexOf(m) + 1, Name = m });
            foreach (var month in monthSelectList)
            {
                ddl.Items.Add(new ListItem(month.Name, month.ID.ToString()));
            }
        }

        private void fillYear(DropDownList ddl)
        {
            int CurrentYear = DateTime.Today.Year;
            int StartingYear = 1950;
            for (int Year = CurrentYear; Year >= StartingYear; Year--)
            {
                ddl.Items.Add(new ListItem(Year.ToString(), Year.ToString()));
            }
        }


        #region[Bind grid]
        protected void BindGrid()
        {

            // return;//prem remove it
            try
            {
                String DateFrom, DateTo;
                DateFrom = ddlMonthFrom.SelectedValue + "/01/" + ddlYearFrom.SelectedValue;

                int NoOfDays = DateTime.DaysInMonth(int.Parse(ddlYearTo.SelectedValue), int.Parse(ddlMonthTo.SelectedValue));
                DateTo = ddlMonthTo.SelectedValue + "/" + NoOfDays.ToString() + "/" + ddlYearTo.SelectedValue;

                //if (String.IsNullOrEmpty(txtDateFrom.Text))
                //DateFrom = DateTime.Today.AddYears(-100).ToShortDateString();
                //else
                //    DateFrom = txtDateFrom.Text.Replace("'", "");

                //if (String.IsNullOrEmpty(txtDateTo.Text))
                //    DateTo = DateTime.Today.AddDays(1).ToShortDateString();
                //else
                //    DateTo = Convert.ToDateTime(txtDateTo.Text.Replace("'", "")).ToShortDateString();

                grvDepositSummary.PageSize = Convert.ToInt32(ddlPageSize1.SelectedValue);


                String sort = "DepositDate";
                String Direction = "DESC";
                if (SortExpression != "")
                {
                    sort = SortExpression;
                }
                else
                {
                    SortExpression = sort;
                }

                if (GridViewSortDirection.ToString() == "Ascending")
                {
                    Direction = "ASC";
                }

                //try
                //{
                //    int noOfRows = objDeposit.GetDepositsDetailsCount(txtVINNumber.Text.Trim().Replace("'", ""), int.Parse(ddlYear.SelectedValue), int.Parse(ddlMake.SelectedValue), int.Parse(ddlModel.SelectedValue), int.Parse(ddlBody.SelectedValue), AmountFrom, AmountTo, strAddedBy, DateFrom, DateTo, strBuyer, strComment, grvDepositSummary.PageIndex, int.Parse(ddlPageSize1.SelectedValue), sort);
                //    Int32 pagesize = grvDepositSummary.PageSize;
                //    Int32 pagecount = noOfRows / pagesize;
                //    if ((noOfRows % pagesize) > 0) pagecount++;
                //    if (pagecount < grvDepositSummary.PageIndex + 1)
                //    {
                //        grvDepositSummary.PageIndex = 0;
                //    }
                //}
                //catch { }

                ObjectDataSource odsDeposits = new ObjectDataSource();
                odsDeposits.Selected += new ObjectDataSourceStatusEventHandler(odsDeposits_Selected);
                odsDeposits.TypeName = "METAOPTION.InventoryDepositSearchBAL";
                odsDeposits.SelectMethod = "GetDepositSummary_Monthly";
                odsDeposits.SelectCountMethod = "GetDepositSummary_MonthlyCount";
                odsDeposits.EnablePaging = true;
                odsDeposits.SelectParameters.Add("DateFrom", DateFrom);
                odsDeposits.SelectParameters.Add("DateTo", DateTo);
                odsDeposits.SelectParameters.Add("StartRowIndex", DbType.Int32, grvDepositSummary.PageIndex.ToString());
                odsDeposits.SelectParameters.Add("MaximumRows", DbType.Int32, ddlPageSize1.SelectedValue);
                odsDeposits.SelectParameters.Add("SortExpression", sort);
                odsDeposits.SelectParameters.Add("SortDirection", Direction);
                odsDeposits.SelectParameters.Add("OrgID", Constant.OrgID.ToString());
                grvDepositSummary.DataSource = odsDeposits;
                grvDepositSummary.DataBind();

            }
            catch { }
            //finally
            //{
            //    //hAddedBy.Value = "";
            //}

        }

        #endregion

        #region[Gridview Selected Event]
        protected void odsDeposits_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue == null) return;
            if (e.ReturnValue.GetType().Name == "Int32")
            {
                Int32 count = Convert.ToInt32(e.ReturnValue);
                Int32 pagesize = grvDepositSummary.PageSize;
                Int32 pagecount = count / pagesize;
                if (count < pagesize)
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} of {0} records", count);
                }
                else
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} through {1} of {2} records"
                         , String.Format("{0:#,###}", pagesize * grvDepositSummary.PageIndex + 1)
                         , String.Format("{0:#,###}", pagesize * (grvDepositSummary.PageIndex + 1) > count ? count : pagesize * (grvDepositSummary.PageIndex + 1))
                         , String.Format("{0:#,###}", count));
                }

                if ((count % pagesize) > 0) pagecount++;

                ddlPaging.Items.Clear();
                ddlPaging1.Items.Clear();

                if (pagecount != 0)
                {
                    for (int i = 0; i < pagecount; i++)
                    {
                        ddlPaging.Items.Add(new ListItem((i + 1).ToString(), (i + 1).ToString()));
                        ddlPaging1.Items.Add(new ListItem((i + 1).ToString(), (i + 1).ToString()));
                    }
                    ddlPaging.SelectedValue = String.Format("{0}", grvDepositSummary.PageIndex + 1);
                    ddlPaging1.SelectedValue = String.Format("{0}", grvDepositSummary.PageIndex + 1);
                    EnablePaging();
                }
                else
                {
                    ddlPaging.Items.Add(new ListItem("0", "0"));
                    ddlPaging.SelectedValue = "0";
                    ddlPaging1.Items.Add(new ListItem("0", "0"));
                    ddlPaging1.SelectedValue = "0";
                    EnablePaging();
                }
            }
        }
        #endregion

        #region[Enable/Disable paging]
        private void EnablePaging()
        {
            btnPrev1.Enabled = btnPrev.Enabled = true;
            btnFirst1.Enabled = btnFirst.Enabled = true;
            btnNext1.Enabled = btnNext.Enabled = true;
            btnLast1.Enabled = btnLast.Enabled = true;

            if ((ddlPaging.SelectedValue == "0") || (ddlPaging1.SelectedValue == "0"))
            {
                btnPrev1.Enabled = btnPrev.Enabled = false;
                btnFirst1.Enabled = btnFirst.Enabled = false;
                btnNext1.Enabled = btnNext.Enabled = false;
                btnLast1.Enabled = btnLast.Enabled = false;
            }
            else if (ddlPaging.SelectedValue == "1")
            {
                if ((ddlPaging.SelectedValue == ddlPaging.Items.Count.ToString()) || (ddlPaging1.SelectedValue == ddlPaging1.Items.Count.ToString()))
                {
                    btnPrev1.Enabled = btnPrev.Enabled = false;
                    btnFirst1.Enabled = btnFirst.Enabled = false;
                    btnNext1.Enabled = btnNext.Enabled = false;
                    btnLast1.Enabled = btnLast.Enabled = false;
                }
                else
                {
                    btnPrev1.Enabled = btnPrev.Enabled = false;
                    btnFirst1.Enabled = btnFirst.Enabled = false;
                }
            }
            else if ((ddlPaging.SelectedValue == ddlPaging.Items.Count.ToString()) || (ddlPaging1.SelectedValue == ddlPaging1.Items.Count.ToString()))
            {
                btnNext1.Enabled = btnNext.Enabled = false;
                btnLast1.Enabled = btnLast.Enabled = false;

            }
        }
        #endregion

        #region[Paging Click events]
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            grvDepositSummary.PageIndex = 0;
            BindGrid();
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedIndex > 0)
                grvDepositSummary.PageIndex--;
            BindGrid();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedValue != ddlPaging.Items.Count.ToString())
                grvDepositSummary.PageIndex++;
            BindGrid();
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            grvDepositSummary.PageIndex = ddlPaging.Items.Count - 1;
            BindGrid();
        }
        #endregion

        #region[Page size selection change]
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            if (ddl.ID == "ddlPageSize1")
                ddlPageSize2.SelectedValue = ddlPageSize1.SelectedValue;
            else
                ddlPageSize1.SelectedValue = ddlPageSize2.SelectedValue;

            grvDepositSummary.PageIndex = 0;
            BindGrid();
        }
        #endregion

        #region[Drop down page selection change]
        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                grvDepositSummary.PageIndex = Convert.ToInt32(((DropDownList)sender).SelectedValue) - 1;
            BindGrid();
            EnablePaging();
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
                    return SortDirection.Descending;
            }
            set
            {
                ViewState["sortDirection"] = value;
            }
        }

        public String SortExpression
        {
            get
            {
                if (ViewState["SortExpression"] != null)
                    return ViewState["SortExpression"].ToString();
                else
                    return "";
            }
            set
            {
                ViewState["SortExpression"] = value;
            }
        }

        protected void grvDepositSummary_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (SortExpression == sortExpression)
            {
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    GridViewSortDirection = SortDirection.Descending;
                    // SortGridView(sortExpression, ASCENDING);
                }
                else
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    // SortGridView(sortExpression, DESCENDING);
                }
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
            }
            SortExpression = sortExpression;
            BindGrid();

        }

        //private void SortGridView(string sortExpression, string direction)
        //{
        //    BindGrid();
        //}

        #endregion

        #region[Search button]
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SortExpression = "";
            GridViewSortDirection = SortDirection.Descending;
            BindGrid();
        }
        #endregion

        protected void ibtnExpand_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as ImageButton).NamingContainer;
            String Year = grvDepositSummary.DataKeys[row.RowIndex].Values["Year"].ToString();
            String Month = grvDepositSummary.DataKeys[row.RowIndex].Values["Month"].ToString();
            // String Year =grvDepositSummary.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0].ToString();
            //ImageButton ibtn = (ImageButton)sender;
            //GridViewRow row = (GridViewRow)ibtn.NamingContainer;

            GridView grdSummaryDetail;
            ImageButton ibtnExpand = (ImageButton)row.FindControl("ibtnExpand");
            grdSummaryDetail = (GridView)row.FindControl("grdSummaryDetail");
            // gvExpenseDetail.RowDataBound += new GridViewRowEventHandler(gvExpenseDetail_RowDataBound);
            string imgUrl = ibtnExpand.ImageUrl;

            String SortExpression = "Bank";
            String SortDirection = "ASC";

            String DepositDateFrom, DepositDateTo;
            DepositDateFrom = Month + "/01/" + Year;

            int NoOfDays = DateTime.DaysInMonth(int.Parse(Year), int.Parse(Month));
            DepositDateTo = Month + "/" + NoOfDays.ToString() + "/" + Year;

            if (imgUrl == "~/Images/expand.png")
            {
                grdSummaryDetail.DataSource = objDeposit.GetDepositSummaryDetail_Monthly(DepositDateFrom, DepositDateTo, SortExpression, SortDirection, Constant.OrgID);
                grdSummaryDetail.DataBind();
                ibtnExpand.ToolTip = "Collapse";
                ibtnExpand.ImageUrl = "~/Images/collapse.png";
            }
            else
            {
                ibtnExpand.ToolTip = "Expand";
                ibtnExpand.ImageUrl = "~/Images/expand.png";
            }

        }

        protected void grvDepositSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                String Year = grvDepositSummary.DataKeys[e.Row.RowIndex].Values["Year"].ToString();
                String Month = grvDepositSummary.DataKeys[e.Row.RowIndex].Values["Month"].ToString();
                String DepositDateFrom, DepositDateTo;
                DepositDateFrom = Month + "/01/" + Year;
                int NoOfDays = DateTime.DaysInMonth(int.Parse(Year), int.Parse(Month));
                DepositDateTo = Month + "/" + NoOfDays.ToString() + "/" + Year;
                HyperLink hyLink = e.Row.FindControl("hylnkDeposits") as HyperLink;
                hyLink.NavigateUrl = "InventoryDepositSearch.aspx?From=" + DepositDateFrom + "&To=" + DepositDateTo;
            }
        }

    }
}