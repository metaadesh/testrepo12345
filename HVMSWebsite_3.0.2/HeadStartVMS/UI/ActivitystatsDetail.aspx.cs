using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using METAOPTION.BAL;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;


namespace METAOPTION.UI
{
    public partial class ActivitystatsDetail : System.Web.UI.Page
    {
        ActivityBAL objBAL = new ActivityBAL();
        ObjectDataSource odsActivity = new ObjectDataSource();
        String Code = String.Empty;
        String filterBy = String.Empty;
        Decimal Total = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Code = Request.QueryString["Code"];
            filterBy = Convert.ToString(Session["SortPreference"]);
            //Default filter by WEEK
            if (String.IsNullOrEmpty(filterBy))
                filterBy = "2";

            if (!Page.IsPostBack)
            {
                BindData();
                
            }
        }

        #region[Bind Data]
        protected void BindData()
        {

            #region[SortPreference]
            DateTime DateFrom = System.DateTime.Today.AddDays(-7);
            DateTime DateTo = System.DateTime.Today;
            #region[1:Today]
            if (filterBy == "1")
            {
                DateFrom = System.DateTime.Today;
            }
            #endregion
            #region[2:This Week]
            if (filterBy == "2")
            {
                //Find first date of current week(monday)
                DateTime mondayDate = DateTime.Today.AddDays(((int)(DateTime.Today.DayOfWeek) * -1) + 1);
                DateFrom = mondayDate;
            }
            #endregion
            #region[3:Last One Week]
            if (filterBy == "3")
            {
                DateFrom = System.DateTime.Today.AddDays(-7);
            }
            #endregion
            #region[4:This Month]
            if (filterBy == "4")
            {
                // Find first date of current month
                DateTime startOfMonth = new DateTime(System.DateTime.Today.Year, System.DateTime.Today.Month, 1);
                DateFrom = startOfMonth;
            }
            #endregion
            #region[5:Last One Month]
            if (filterBy == "5")
            {
                DateFrom = System.DateTime.Today.AddMonths(-1);
            }
            #endregion
            #region[6:This Year]
            if (filterBy == "6")
            {
                //Find first date of current year
                DateTime startOfYear = new DateTime(System.DateTime.Today.Year, 1, 1);
                DateFrom = startOfYear;
            }
            #endregion
            #region[7:Last One Year]
            if (filterBy == "7")
            {
                DateFrom = System.DateTime.Today.AddYears(-1);
            }
            #endregion
            #region[8:Date Range]
            if (filterBy == "8")
            {
                DateFrom = Convert.ToDateTime(Session["StartDate"]);
                DateTo = Convert.ToDateTime(Session["EndDate"]);
            }
            #endregion
            #endregion

            if (Code == "3YearsOld" || Code == "UnresolvedCars" || Code == "30MonthsOld" || Code == "2YearsOld" || Code == "1YearOld" || Code == "6MonthsOld" || Code == "3MonthsOld")
            {
                gvAllActivity.PageSize = Convert.ToInt32(ddlPageSize1.SelectedValue);
                odsActivity.Selected += new ObjectDataSourceStatusEventHandler(odsActivity_Selected);
                odsActivity.TypeName = "METAOPTION.BAL.ActivityBAL";
                odsActivity.SelectMethod = "GetActivityStatsUnResolvedCarDetail";
                odsActivity.SelectCountMethod = "GetActivityStatsDetailsCount";
                odsActivity.EnablePaging = true;
                odsActivity.SelectParameters.Add("Code", Code);
                odsActivity.SelectParameters.Add("StartRowIndex", DbType.Int32, gvAllActivity.PageIndex.ToString());
                odsActivity.SelectParameters.Add("MaximumRows", DbType.Int32, ddlPageSize1.SelectedValue);

                gvAllActivity.DataSource = odsActivity;
                gvAllActivity.DataBind();
                gvAllActivity.Visible = true;
                table1.Visible = true;
                table2.Visible = true;
                grddetail.Visible = false;

            }


            else if (Code == "Today_ACIT" || Code == "Today_CO1133" || Code == "Today_CO1373" || Code == "Today_CMAA" || Code == "Week_ACIT" || Code == "Week_CO1133" || Code == "Week_CO1373" || Code == "Week_CMAA" || Code == "All_ACIT" || Code == "All_CO1133" || Code == "All_CO1373" || Code == "All_CMAA")
            {
                gvAllActivity.PageSize = Convert.ToInt32(ddlPageSize1.SelectedValue);
                odsActivity.Selected += new ObjectDataSourceStatusEventHandler(odsActivity_Selected);
                odsActivity.TypeName = "METAOPTION.BAL.ActivityBAL";
                odsActivity.SelectMethod = "GetActivityStatsLocationDetail";
                odsActivity.SelectCountMethod = "GetActivityStatsLocationDetailsCount";
                odsActivity.EnablePaging = true;
                odsActivity.SelectParameters.Add("Code", Code);
                odsActivity.SelectParameters.Add("StartRowIndex", DbType.Int32, gvAllActivity.PageIndex.ToString());
                odsActivity.SelectParameters.Add("MaximumRows", DbType.Int32, ddlPageSize1.SelectedValue);

                gvAllActivity.DataSource = odsActivity;
                gvAllActivity.DataBind();
                gvAllActivity.Visible = true;
                table1.Visible = true;
                table2.Visible = true;
                grddetail.Visible = false;
            }

            else
            {
                DataTable dt = objBAL.FetchActivityStatsDetail(DateFrom, DateTo, Code);
                if (Code == "TCC" || Code == "DCC" || Code == "VEA" || Code == "VED" || Code == "TDA" || Code == "TANSC" || Code == "TASC" || Code == "TComA" || Code == "LTF" || Code == "TAC")
                {
                    grddetail.ShowFooter = true;
                    dvTotal.Visible = true;
                }
                grddetail.AllowPaging = true;
                grddetail.PageSize = 50;
                grddetail.DataSource = dt;
                grddetail.DataBind();
                decimal totalAmount = 0;
                if (Code == "TCC" || Code == "DCC" || Code == "TASC" || Code == "TANSC")
                {
                    foreach (DataRow row in dt.Rows)
                        totalAmount += Convert.ToDecimal(row["CarCost"]);
                    lblTotal.Text = Convert.ToString(totalAmount);
                }
                if (Code == "VEA" || Code == "VED")
                {
                    foreach (DataRow row in dt.Rows)
                        totalAmount += Convert.ToDecimal(row["Amount"]);
                    lblTotal.Text = Convert.ToString(totalAmount);
                }
                if (Code == "TDA")
                {
                    foreach (DataRow row in dt.Rows)
                        totalAmount += Convert.ToDecimal(row["DepositAmount"]);
                    lblTotal.Text = Convert.ToString(totalAmount);
                }
                if (Code == "TComA")
                {
                    foreach (DataRow row in dt.Rows)
                        totalAmount += Convert.ToDecimal(row["Amount"]);
                    lblTotal.Text = Convert.ToString(totalAmount);
                }
                if (Code == "LTF")
                {
                    foreach (DataRow row in dt.Rows)
                        totalAmount += Convert.ToDecimal(row["LateFee"]);
                    lblTotal.Text = Convert.ToString(totalAmount);
                }
                if (Code == "TAC")
                {
                    foreach (DataRow row in dt.Rows)
                        totalAmount += Convert.ToDecimal(row["Amount"]);
                    lblTotal.Text = Convert.ToString(totalAmount);
                }
                gvAllActivity.Visible = false;
                table1.Visible = false;
                table2.Visible = false;

            }

        }
        #endregion

        #region[Gridview(grddetail) RowDataBound]
        protected void grddetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (TableCell tc in e.Row.Cells)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    tc.CssClass = "GridHeader";
                }

                else if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Footer)
                {
                    tc.CssClass = "GridContent";
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Code == "TCC" || Code == "DCC")
                {
                    String str = e.Row.Cells[10].Text.Replace(",", "").Replace("&nbsp;", "");
                    Decimal CarCost = String.IsNullOrEmpty(str) ? 0 : Convert.ToDecimal(str);
                    Total += CarCost;
                    
                   
                }
                else if (Code == "VEA" || Code == "VED")
                {
                    String str = e.Row.Cells[2].Text.Replace(",", "").Replace("&nbsp;", "");
                    Decimal Expense = String.IsNullOrEmpty(str) ? 0 : Convert.ToDecimal(str);
                    Total += Expense;
                }
                else if (Code == "TDA")
                {
                    String str = e.Row.Cells[5].Text.Replace(",", "").Replace("&nbsp;", "");
                    Decimal Deposit = String.IsNullOrEmpty(str) ? 0 : Convert.ToDecimal(str);
                    Total += Deposit;
                }
                else if (Code == "TANSC" || Code == "TASC")
                {
                    String str = e.Row.Cells[5].Text.Replace(",", "").Replace("&nbsp;", "");
                    Decimal SoldCar = String.IsNullOrEmpty(str) ? 0 : Convert.ToDecimal(str);
                    Total += SoldCar;
                }
                else if (Code == "TComA")
                {
                    String str = e.Row.Cells[5].Text.Replace(",", "").Replace("&nbsp;", "");
                    Decimal Commission = String.IsNullOrEmpty(str) ? 0 : Convert.ToDecimal(str);
                    Total += Commission;
                }
                else if (Code == "LTF")
                {
                    String str = e.Row.Cells[12].Text.Replace(",", "").Replace("&nbsp;", "");
                    Decimal TitleFee = String.IsNullOrEmpty(str) ? 0 : Convert.ToDecimal(str);
                    Total += TitleFee;
                }
                else if (Code == "TAC")
                {
                    String str = e.Row.Cells[3].Text.Replace(",", "").Replace("&nbsp;", "");
                    Decimal CheckAmount = String.IsNullOrEmpty(str) ? 0 : Convert.ToDecimal(str);
                    Total += CheckAmount;
                }
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl = new Label();
                lbl.Text = Total.ToString("#,#");
                e.Row.Attributes.Add("style", "font-weight:bold");

                if (Code == "TCC" || Code == "DCC")
                {
                    //lbl.Text = Total.ToString("#,#");
                    e.Row.Cells[9].Text = "TOTAL";
                    e.Row.Cells[10].Controls.Add(lbl);
                }
                else if (Code == "VEA" || Code == "VED")
                {
                    //lbl.Text = Total.ToString("#,#");
                    e.Row.Cells[1].Text = "TOTAL";
                    e.Row.Cells[2].Controls.Add(lbl);
                }
                else if (Code == "TDA")
                {
                   // lbl.Text = Total.ToString("#,#");
                    e.Row.Cells[4].Text = "TOTAL";
                    e.Row.Cells[5].Controls.Add(lbl);
                }
                else if (Code == "TANSC" || Code == "TASC")
                {
                    //lbl.Text = Total.ToString("#,#");
                    e.Row.Cells[4].Text = "TOTAL";
                    e.Row.Cells[5].Controls.Add(lbl);
                }
                else if (Code == "TComA")
                {
                    //lbl.Text = Total.ToString("#,#");
                    e.Row.Cells[4].Text = "TOTAL";
                    e.Row.Cells[5].Controls.Add(lbl);
                }
                else if (Code == "LTF")
                {
                    //lbl.Text = Total.ToString("#,#");
                    e.Row.Cells[11].Text = "TOTAL";
                    e.Row.Cells[12].Controls.Add(lbl);
                }
                else if (Code == "TAC")
                {
                    //lbl.Text = Total.ToString("#,#");
                    e.Row.Cells[2].Text = "TOTAL";
                    e.Row.Cells[3].Controls.Add(lbl);
                }
            }
        }
        #endregion

        #region[Gridview(gvAllActivity) RowDataBound]
        protected void gvAllActivity_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (TableCell tc in e.Row.Cells)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    tc.CssClass = "GridHeader";
                }

                else
                {
                    tc.CssClass = "GridContent";
                }
            }
        }
        #endregion

        #region[Gridview Selected Event]
        protected void odsActivity_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue == null) return;
            if (e.ReturnValue.GetType().Name == "Int32")
            {

                Int32 count = Convert.ToInt32(e.ReturnValue);
                Int32 pagesize = gvAllActivity.PageSize;
                Int32 pagecount = count / pagesize;
                if (count < pagesize)
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} of {0} records", count);
                }
                else
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} through {1} of {2} records"
                         , String.Format("{0:#,###}", pagesize * gvAllActivity.PageIndex + 1)
                         , String.Format("{0:#,###}", pagesize * (gvAllActivity.PageIndex + 1) > count ? count : pagesize * (gvAllActivity.PageIndex + 1))
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

                    ddlPaging.SelectedValue = String.Format("{0}", gvAllActivity.PageIndex + 1);
                    ddlPaging1.SelectedValue = String.Format("{0}", gvAllActivity.PageIndex + 1);
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
            gvAllActivity.PageIndex = 0;
            BindData();
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedIndex > 0)
                gvAllActivity.PageIndex--;
            BindData();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedValue != ddlPaging.Items.Count.ToString())
                gvAllActivity.PageIndex++;
            BindData();
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            gvAllActivity.PageIndex = ddlPaging.Items.Count - 1;
            BindData();
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

            gvAllActivity.PageIndex = 0;
            BindData();
        }
        #endregion

        #region[Drop down page selection change]
        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                gvAllActivity.PageIndex = Convert.ToInt32(((DropDownList)sender).SelectedValue) - 1;
            BindData();
            EnablePaging();
        }
        #endregion

        #region[Paging]
        protected void gvAllActivity_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAllActivity.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void grddetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grddetail.PageIndex = e.NewPageIndex;
            BindData();
        }
        #endregion

    }
}