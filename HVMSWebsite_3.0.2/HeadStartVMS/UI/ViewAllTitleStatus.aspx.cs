using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using METAOPTION.BAL;
using System.Collections;
using System.Web.Security;

namespace METAOPTION.UI
{
    public partial class ViewAllTitleStatus : System.Web.UI.Page
    {

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
            //Set OrgID context key of AutoCompleteExtender from Session
            txtDealer_AutoCompleteExtender.ContextKey = Convert.ToString(Constant.OrgID);

            if (!Page.IsPostBack)
            {
                FillCountry();
                BindYear();
                BindMake();
                BindSort1DDL();
                if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
                    ddlCarStatus.SelectedValue = "-1";
                BindGrid(); 
                dvSearch.Style.Add("display", "block");
              
            }
        }

        protected void BindGrid()
        {
            gvViewAllTitleStatus.PageSize = Convert.ToInt32(ddlPageSize1.SelectedValue);

            String DateFrom, DateTo;

            if (String.IsNullOrEmpty(txtDateFrom.Text))
                DateFrom = DateTime.Today.AddYears(-100).ToShortDateString();
            else
                DateFrom = txtDateFrom.Text;

            if (String.IsNullOrEmpty(txtDateTo.Text))
                DateTo = DateTime.Today.AddDays(1).ToShortDateString();
            else
                DateTo = Convert.ToDateTime(txtDateTo.Text).AddDays(1).ToShortDateString();

            String sort = String.Empty;
            if (ddlSort1.SelectedValue != "-1")
                sort = String.Format("{0} {1}", ddlSort1.SelectedValue, rbtnSort1Direction.SelectedValue);

            if (ddlSort2.SelectedValue != "-1")
                sort += String.Format("{0} {1}"
                    , sort == "" ? ddlSort2.SelectedValue : ", " + ddlSort2.SelectedValue
                    , rbtnSort2Direction.SelectedValue);

            if (ddlSort3.SelectedValue != "-1")
                sort += String.Format("{0} {1}"
                    , sort == "" ? ddlSort3.SelectedValue : ", " + ddlSort3.SelectedValue
                    , rbtnSort3Direction.SelectedValue);

            if (String.IsNullOrEmpty(sort))
                sort = "BT.DaysDelayed DESC";

            ObjectDataSource odsViewAllTitleStatus = new ObjectDataSource();
            odsViewAllTitleStatus.Selected += new ObjectDataSourceStatusEventHandler(odsViewAllTitleStatus_Selected);
            odsViewAllTitleStatus.TypeName = "METAOPTION.BAL.InventoryBAL";
            odsViewAllTitleStatus.SelectMethod = "GetViewAllTitleStatus";
            odsViewAllTitleStatus.SelectCountMethod = "GetViewAllTitleStatusCount";
            odsViewAllTitleStatus.EnablePaging = true;

            odsViewAllTitleStatus.SelectParameters.Add("VIN", String.IsNullOrEmpty(txtVINNumber.Text.Trim().ToString()) ? "" : txtVINNumber.Text.Trim());
            odsViewAllTitleStatus.SelectParameters.Add("Year", DbType.Int32, String.IsNullOrEmpty(ddlYear.SelectedValue) ? "-1" : ddlYear.SelectedValue);
            odsViewAllTitleStatus.SelectParameters.Add("MakeId", DbType.Int32, String.IsNullOrEmpty(ddlMake.SelectedValue) ? "-1" : ddlMake.SelectedValue);
            odsViewAllTitleStatus.SelectParameters.Add("ModelId", DbType.Int32, String.IsNullOrEmpty(ddlModel.SelectedValue) ? "-1" : ddlModel.SelectedValue);
            odsViewAllTitleStatus.SelectParameters.Add("DealerID", DbType.Int32, String.IsNullOrEmpty(hfDealerId.Value) ? "-1" : hfDealerId.Value);
            odsViewAllTitleStatus.SelectParameters.Add("TitlePresent", DbType.Int32, String.IsNullOrEmpty(ddlTitlePresent.SelectedValue) ? "-1" : ddlTitlePresent.SelectedValue);
            odsViewAllTitleStatus.SelectParameters.Add("DateFrom", DateFrom);
            odsViewAllTitleStatus.SelectParameters.Add("DateTo", DateTo);
            odsViewAllTitleStatus.SelectParameters.Add("LateFee", String.IsNullOrEmpty(ddlLateFee.SelectedValue) ? "-1" : ddlLateFee.SelectedValue);
            odsViewAllTitleStatus.SelectParameters.Add("CarStatus", DbType.Int32, String.IsNullOrEmpty(ddlCarStatus.SelectedValue) ? "-1" : ddlCarStatus.SelectedValue);

            odsViewAllTitleStatus.SelectParameters.Add("LoginEntityTypeID", DbType.Int32, Session["LoginEntityTypeID"].ToString());
            odsViewAllTitleStatus.SelectParameters.Add("UserEntityID", DbType.Int32, Session["UserEntityID"].ToString());
            odsViewAllTitleStatus.SelectParameters.Add("BuyerParentID", DbType.Int32, String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString());
            odsViewAllTitleStatus.SelectParameters.Add("BuyerIsDirect", DbType.String, String.IsNullOrEmpty(Convert.ToString(Session["BuyerIsDirect"])) ? "-1" : Session["BuyerIsDirect"].ToString());
            odsViewAllTitleStatus.SelectParameters.Add("BuyerAccessLevel", DbType.String, String.IsNullOrEmpty(Convert.ToString(Session["BuyerAccessLevel"])) ? "-1" : Session["BuyerAccessLevel"].ToString());

            odsViewAllTitleStatus.SelectParameters.Add("StartRowIndex", DbType.Int32, gvViewAllTitleStatus.PageIndex.ToString());
            odsViewAllTitleStatus.SelectParameters.Add("MaximumRows", DbType.Int32, ddlPageSize1.SelectedValue);
            odsViewAllTitleStatus.SelectParameters.Add("SystemID", DbType.Int32, Convert.ToString(Session["SystemId"]));
            odsViewAllTitleStatus.SelectParameters.Add("SortExpression", sort);
            odsViewAllTitleStatus.SelectParameters.Add("OrgID", DbType.Int16, Convert.ToString(Constant.OrgID));

            gvViewAllTitleStatus.DataSource = odsViewAllTitleStatus;
            gvViewAllTitleStatus.DataBind();
            //hfDealerId.Value = "";
        }

        #region [Bind Year, Make]
        private void BindYear()
        {
            ddlYear.DataSource = BAL.Common.GetYearList();
            ddlYear.DataTextField = "Year";
            ddlYear.DataValueField = "Year";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, new ListItem("ALL", "-1"));
        }

        private void BindMake()
        {
            ddlModel.Items.Clear();
            ddlMake.DataSource = BAL.Common.GetAllMakes();
            ddlMake.DataValueField = "MakeId";
            ddlMake.DataTextField = "Make";
            ddlMake.DataBind();
            ddlMake.Items.Insert(0, new ListItem("ALL", "-1"));
        }

        protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMake.Items.Count > 0)
                if (Convert.ToInt32(ddlMake.SelectedValue) == -1)
                {
                    ddlModel.Items.Clear();
                    ddlModel.Items.Insert(0, new ListItem("ALL", "-1"));
                }
                else
                {
                    BindModel(Convert.ToInt32(ddlMake.SelectedValue));
                }
        }

        private void BindModel(long makeId)
        {
            ddlModel.Items.Clear();
            ddlModel.DataSource = BAL.Common.GetModel(makeId);
            ddlModel.DataValueField = "ModelId";
            ddlModel.DataTextField = "Model";
            ddlModel.DataBind();
            ddlModel.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        #endregion

        #region[Drop down paging]
        protected void odsViewAllTitleStatus_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue == null) return;
            if (e.ReturnValue.GetType().Name == "Int32")
            {

                Int32 count = Convert.ToInt32(e.ReturnValue);
                Int32 pagesize = gvViewAllTitleStatus.PageSize;
                Int32 pagecount = count / pagesize;
                if (count < pagesize)
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} of {0} records", count);
                }
                else
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} through {1} of {2} records"
                         , String.Format("{0:#,###}", pagesize * gvViewAllTitleStatus.PageIndex + 1)
                         , String.Format("{0:#,###}", pagesize * (gvViewAllTitleStatus.PageIndex + 1) > count ? count : pagesize * (gvViewAllTitleStatus.PageIndex + 1))
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

                    ddlPaging.SelectedValue = String.Format("{0}", gvViewAllTitleStatus.PageIndex + 1);
                    ddlPaging1.SelectedValue = String.Format("{0}", gvViewAllTitleStatus.PageIndex + 1);
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
            gvViewAllTitleStatus.PageIndex = 0;
            BindGrid();
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedIndex > 0)
                gvViewAllTitleStatus.PageIndex--;
            BindGrid();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedValue != ddlPaging.Items.Count.ToString())
                gvViewAllTitleStatus.PageIndex++;
            BindGrid();
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            gvViewAllTitleStatus.PageIndex = ddlPaging.Items.Count - 1;
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

            gvViewAllTitleStatus.PageIndex = 0;
            BindGrid();
        }
        #endregion

        #region[Drop down page selection change]
        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                gvViewAllTitleStatus.PageIndex = Convert.ToInt32(((DropDownList)sender).SelectedValue) - 1;
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
                    return SortDirection.Ascending;
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected void gvViewAllTitleStatus_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, ASCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, DESCENDING);
            }

        }

        private void SortGridView(string sortExpression, string direction)
        {
            if (ddlSort1.Items.FindByValue(sortExpression) != null)
            {
                ddlSort1.SelectedValue = sortExpression;
                if (direction == " ASC") rbtnSort1Direction.SelectedIndex = 0;
                else rbtnSort1Direction.SelectedIndex = 1;
            }

            if (ddlSort2.Items.FindByValue(sortExpression) != null)
            {
                ddlSort2.SelectedValue = "-1";
                rbtnSort2Direction.SelectedIndex = 0;
            }

            if (ddlSort3.Items.FindByValue(sortExpression) != null)
            {
                ddlSort3.SelectedValue = "-1";
                rbtnSort3Direction.SelectedIndex = 0;
            }

            BindGrid();
        }

        #endregion

        #region[Sort dropdown]
        private void BindSort1DDL()
        {
            ddlSort1.DataSource = InventoryBAL.InventoryList_SortOptions("SELECT * FROM LookUp_Sort WHERE TableId = 12 order by Sequence asc, SortText asc", "", "");
            ddlSort1.DataTextField = "SortText";
            ddlSort1.DataValueField = "SortValue";
            ddlSort1.DataBind();

            ddlSort1.Items.Insert(0, new ListItem("", "-1"));
            if (ddlSort1.Items.FindByValue("I.DateAdded") != null)
                ddlSort1.SelectedValue = "I.DateAdded";

            BindSort2DDL();
            BindSort3DDL();
        }

        private void BindSort2DDL()
        {
            ddlSort2.DataSource = InventoryBAL.InventoryList_SortOptions(String.Format("SELECT * FROM LookUp_Sort WHERE TableId = 12 AND SortValue NOT IN ('{0}') order by Sequence asc, SortText asc", ddlSort1.SelectedValue), "", "");
            ddlSort2.DataTextField = "SortText";
            ddlSort2.DataValueField = "SortValue";
            ddlSort2.DataBind();
            ddlSort2.Items.Insert(0, new ListItem("", "-1"));

            BindSort3DDL();
        }

        private void BindSort3DDL()
        {
            ddlSort3.DataSource = InventoryBAL.InventoryList_SortOptions(String.Format("SELECT * FROM LookUp_Sort WHERE TableId =12 AND SortValue NOT IN ('{0}', '{1}') order by Sequence asc, SortText asc", ddlSort1.SelectedValue, ddlSort2.SelectedValue), "", "");
            ddlSort3.DataTextField = "SortText";
            ddlSort3.DataValueField = "SortValue";
            ddlSort3.DataBind();
            ddlSort3.Items.Insert(0, new ListItem("", "-1"));
        }
        #endregion

        #region[Sort dropdown selected index changed handler]
        protected void ddlSort1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvSearch.Style.Add("display", "block");
            BindSort2DDL();
            ddlSort2.SelectedValue = "-1";
            ddlSort3.SelectedValue = "-1";
        }

        protected void ddlSort2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvSearch.Style.Add("display", "block");
            BindSort3DDL();
            ddlSort3.SelectedValue = "-1";
        }

        protected void ddlSort3_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvSearch.Style.Add("display", "block");
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvViewAllTitleStatus.PageIndex = 0;
            BindGrid();
        }

        #region[ Select Dealer Popup ]
        private void FillCountry()
        {
            this.ddlCountry.DataSource = BAL.Common.CountryList();
            this.ddlCountry.DataTextField = "CountryName";
            this.ddlCountry.DataValueField = "CountryId";
            this.ddlCountry.DataBind();
            this.ddlCountry.Items.Insert(0, new ListItem("", "-1"));
            this.ddlDealerState.Items.Insert(0, new ListItem("", "-1"));
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlDealerState.Items.Clear();
            this.ddlDealerState.DataSource = BAL.Common.GetStateList(Convert.ToInt16(this.ddlCountry.SelectedValue));
            this.ddlDealerState.DataTextField = "State";
            this.ddlDealerState.DataValueField = "StateId";
            this.ddlDealerState.DataBind();
            this.ddlDealerState.Items.Insert(0, new ListItem("", "-1"));

            this.mpeOpenDealerSelector.Show();
        }

        protected void btnSearchDealers_Click(object sender, EventArgs e)
        {
            this.mpeOpenDealerSelector.Show();

            // Change the popup titile
            if (hfDealerCustomerType.Value == "1")
                this.lblDealerCustomerHeading.Text = "Select Customer";
            else
                this.lblDealerCustomerHeading.Text = "Select Dealer";
        }

        protected void imgbtnSelect_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton img = (ImageButton)sender;
            GridViewRow gvRow = (GridViewRow)img.NamingContainer;

            hfDealerId.Value = Convert.ToString(this.gvDealerDetails.DataKeys[gvRow.RowIndex].Value);
            //this.txtDealer.Text
             string a   = gvRow.Cells[2].Text + "(" +
                     gvRow.Cells[3].Text + ", " +
                     gvRow.Cells[4].Text + ", " +
                     gvRow.Cells[6].Text + "-" +
                     gvRow.Cells[5].Text + ")";
            //this.txtDealerName.Text = string.Empty;
             string aformate = a.Replace("&nbsp;"," ");
             string bformate = aformate.Replace("&amp;","&");
             this.txtDealer.Text = bformate;
            this.gvDealerDetails.DataBind();
         }

        protected void ibtnClose_click(object sender, EventArgs e)
        {
           this.gvDealerDetails.DataBind();
        }

        protected void gvDealerDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvSearch.Style.Add("display", "block");
            this.mpeOpenDealerSelector.Show();
        }

        #endregion
    }
}