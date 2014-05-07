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
    public partial class InventoryDepositSearch : System.Web.UI.Page
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
                BindDepositFilters();
                BindSort1DDL();
                BindGrid();
            }
        }

        private void CheckPermission()
        {
            List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, "INVENTORY");

            if (!dict.Contains("DEPOSIT.VIEW"))
                Response.Redirect("Permission.aspx?MSG=DEPOSIT.VIEW", true);
        }

        #region[ Make ]
        private void BindMake()
        {
            this.ddlModel.Items.Clear();
            this.ddlMake.DataSource = BAL.Common.GetAllMakes();
            this.ddlMake.DataValueField = "MakeId";
            this.ddlMake.DataTextField = "Make";
            this.ddlMake.DataBind();
            this.ddlMake.Items.Insert(0, new ListItem("ALL", "-1"));
        }

        protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvSearch.Style.Add("display", "block");
            if (this.ddlMake.Items.Count > 0)
                if (Convert.ToInt32(this.ddlMake.SelectedValue) == -1)
                {
                    this.ddlModel.Items.Clear();
                    this.ddlModel.Items.Insert(0, new ListItem("ALL", "-1"));
                    this.ddlBody.Items.Clear();
                    this.ddlBody.Items.Insert(0, new ListItem("ALL", "-1"));
                }
                else
                {
                    BindModel(Convert.ToInt32(this.ddlMake.SelectedValue));
                }
        }
        #endregion

        #region[ Model ]
        private void BindModel(long makeId)
        {
            this.ddlModel.Items.Clear();
            this.ddlModel.DataSource = BAL.Common.GetModel(makeId);
            this.ddlModel.DataValueField = "ModelId";
            this.ddlModel.DataTextField = "Model";
            this.ddlModel.DataBind();
            this.ddlModel.Items.Insert(0, new ListItem("ALL", "-1"));
            this.ddlBody.Items.Clear();
            this.ddlBody.Items.Insert(0, new ListItem("ALL", "-1"));
        }

        protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvSearch.Style.Add("display", "block");
            if (this.ddlModel.Items.Count > 0)
                if (Convert.ToInt32(this.ddlModel.SelectedValue) == -1)
                {
                    this.ddlBody.Items.Clear();
                    this.ddlBody.Items.Insert(0, new ListItem("ALL", "-1"));
                }
                else
                {
                    BindBody(Convert.ToInt32(this.ddlModel.SelectedValue));
                }
        }

        #endregion

        #region[ Body ]
        private void BindBody(Int32 modelId)
        {
            Common objCommon = new Common();
            this.ddlBody.Items.Clear();
            this.ddlBody.DataSource = objCommon.GetBodies(modelId);
            this.ddlBody.DataValueField = "BodyId";
            this.ddlBody.DataTextField = "Body";
            this.ddlBody.DataBind();
            this.ddlBody.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        #endregion

        #region[Bind grid]
        protected void BindGrid()
        {
            // return;//prem remove it
            try
            {
                String DateFrom, DateTo;

                string strAddedBy = string.Empty;
                string strBuyer = string.Empty;
                string strComment;

                if (String.IsNullOrEmpty(txtDateFrom.Text))
                    DateFrom = DateTime.Today.AddYears(-100).ToShortDateString();
                else
                    DateFrom = txtDateFrom.Text.Replace("'", "");

                if (String.IsNullOrEmpty(txtDateTo.Text))
                    DateTo = DateTime.Today.AddDays(1).ToShortDateString();
                else
                    DateTo = Convert.ToDateTime(txtDateTo.Text.Replace("'", "")).ToShortDateString();

                if (!string.IsNullOrEmpty(hAddedBy.Value))
                    strAddedBy = hAddedBy.Value;
                else
                    strAddedBy = "-1";

                if (!string.IsNullOrEmpty(hBuyer.Value))
                    strBuyer = hBuyer.Value;
                else
                    strBuyer = "-1";


                if (!string.IsNullOrEmpty(txtComment.Text))
                    strComment = txtComment.Text.Replace("'", "");
                else
                    strComment = "";

                gvDeposits.PageSize = Convert.ToInt32(ddlPageSize1.SelectedValue);

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
                    sort = "Inventory.DateAdded DESC";

                sort += ", Inventory.InventoryID Asc";

                Double AmountFrom = 0.0, AmountTo = 9999999999;
                if (Double.TryParse(txtAmountFrom.Text.Trim().Replace("'", ""), out AmountFrom) == false)
                {
                    AmountFrom = 0.0;
                }
                if (Double.TryParse(txtAmountTo.Text.Trim().Replace("'", ""), out AmountTo) == false)
                {
                    AmountTo = 9999999999;
                }

                try
                {
                    int noOfRows = objDeposit.GetDepositsDetailsCount(txtVINNumber.Text.Trim().Replace("'", ""), int.Parse(ddlYear.SelectedValue), int.Parse(ddlMake.SelectedValue), int.Parse(ddlModel.SelectedValue), int.Parse(ddlBody.SelectedValue), AmountFrom, AmountTo, strAddedBy, DateFrom, DateTo, strBuyer, strComment, gvDeposits.PageIndex, int.Parse(ddlPageSize1.SelectedValue), sort,Constant.OrgID);
                    Int32 pagesize = gvDeposits.PageSize;
                    Int32 pagecount = noOfRows / pagesize;
                    if ((noOfRows % pagesize) > 0) pagecount++;
                    if (pagecount < gvDeposits.PageIndex + 1)
                    {
                        gvDeposits.PageIndex = 0;
                    }
                }
                catch { }

                ObjectDataSource odsDeposits = new ObjectDataSource();
                odsDeposits.Selected += new ObjectDataSourceStatusEventHandler(odsDeposits_Selected);
                odsDeposits.TypeName = "METAOPTION.InventoryDepositSearchBAL";
                odsDeposits.SelectMethod = "GetDepositsDetails";
                odsDeposits.SelectCountMethod = "GetDepositsDetailsCount";
                odsDeposits.EnablePaging = true;
                odsDeposits.SelectParameters.Add("VIN", txtVINNumber.Text.Trim().Replace("'", ""));
                odsDeposits.SelectParameters.Add("Year", ddlYear.SelectedValue);
                odsDeposits.SelectParameters.Add("MakeID", ddlMake.SelectedValue);
                odsDeposits.SelectParameters.Add("ModelID", ddlModel.SelectedValue);
                odsDeposits.SelectParameters.Add("BodyID", ddlBody.SelectedValue);
                odsDeposits.SelectParameters.Add("AmountFrom", AmountFrom.ToString());
                odsDeposits.SelectParameters.Add("AmountTo", AmountTo.ToString());
                odsDeposits.SelectParameters.Add("AddedBy", strAddedBy);
                odsDeposits.SelectParameters.Add("DateFrom", DateFrom);
                odsDeposits.SelectParameters.Add("DateTo", DateTo);
                odsDeposits.SelectParameters.Add("BuyerIDs", strBuyer);
                odsDeposits.SelectParameters.Add("Comment", strComment);
                odsDeposits.SelectParameters.Add("StartRowIndex", DbType.Int32, gvDeposits.PageIndex.ToString());
                odsDeposits.SelectParameters.Add("MaximumRows", DbType.Int32, ddlPageSize1.SelectedValue);
                odsDeposits.SelectParameters.Add("SortExpression", sort);
                odsDeposits.SelectParameters.Add("OrgID", Constant.OrgID.ToString());
                gvDeposits.DataSource = odsDeposits;
                gvDeposits.DataBind();

            }
            catch (Exception ex) { }
            //finally
            //{
            //    //hAddedBy.Value = "";
            //}
        }

        #endregion

        #region [Bind Filter Dropdown]
        public void BindDepositFilters()
        {
            System.Collections.ArrayList arraylist = new ArrayList();
            String ParentEntityID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString();
            if (Convert.ToString(Session["BuyerAccessLevel"]) == "1")
                arraylist = BAL.Common.GetDataForInventorySearchDDLs(Convert.ToInt32(Session["UserEntityID"]), -1, Convert.ToInt32(Session["LoginEntityTypeID"]), Constant.OrgID);
            else
                arraylist = BAL.Common.GetDataForInventorySearchDDLs(Convert.ToInt32(Session["UserEntityID"]), Convert.ToInt32(ParentEntityID), Convert.ToInt32(Session["LoginEntityTypeID"]),Constant.OrgID);

            // Fill Buyer List
            this.ddlBuyer.DataSource = arraylist[0];
            this.ddlBuyer.DataValueField = "BuyerId";
            this.ddlBuyer.DataTextField = "BuyerName";
            this.ddlBuyer.DataBind();
            //this.ddlBuyer.Items.Insert(0, new ListItem("All", "-1"));
            String ParentID = "-1";
            if (Convert.ToString(Session["BuyerAccessLevel"]) != "1")
                ParentID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString();

            //do review prem 3-oct-2013
            //if (Convert.ToString(Session["LoginEntityTypeID"]) == "2" && ParentID == "-1" && ddlBuyer.Items.Count == 2)
            //{
            //    this.ddlBuyer.SelectedValue = Convert.ToString(Session["UserEntityID"]);
            //    this.ddlBuyer.Enabled = false;
            //}
            //else if (Convert.ToString(Session["LoginEntityTypeID"]) == "2" && ddlBuyer.Items.FindByValue(Convert.ToString(Session["UserEntityID"])) != null)
            //    this.ddlBuyer.SelectedValue = Convert.ToString(Session["UserEntityID"]);

            //Fill From Year
            this.ddlYear.DataSource = arraylist[2];
            this.ddlYear.DataTextField = "Year";
            this.ddlYear.DataValueField = "Year";
            this.ddlYear.DataBind();
            this.ddlYear.Items.Insert(0, new ListItem("ALL", "-1"));

            //Fill All Makes
            this.ddlModel.Items.Clear();
            this.ddlMake.DataSource = arraylist[3];
            this.ddlMake.DataValueField = "MakeId";
            this.ddlMake.DataTextField = "Make";
            this.ddlMake.DataBind();
            this.ddlMake.Items.Insert(0, new ListItem("ALL", "-1"));

            this.ddlModel.Items.Insert(0, new ListItem("ALL", "-1"));
            this.ddlBody.Items.Insert(0, new ListItem("ALL", "-1"));

            ds = objDeposit.GetSearchFilters(Constant.OrgID);

            ddlAddedBy.DataSource = ds.Tables["dtAddedBy"];
            ddlAddedBy.DataTextField = "UserName";
            ddlAddedBy.DataValueField = "UserID";
            ddlAddedBy.DataBind();

            if ((Request.QueryString["From"] != null) && (Request.QueryString["To"] != null))
            {
                DateTime dFrom;
                DateTime dTo;
                if (DateTime.TryParse(Request.QueryString["From"].ToString(), out dFrom))
                {
                    txtDateFrom.Text = dFrom.ToString("MM/dd/yyyy");
                }
                else
                {
                    txtDateFrom.Text = DateTime.Today.AddDays(-28).ToString("MM/dd/yyyy");
                }

                if (DateTime.TryParse(Request.QueryString["To"].ToString(), out dTo))
                {
                    txtDateTo.Text = dTo.ToString("MM/dd/yyyy");
                }
                else
                {
                    txtDateTo.Text = DateTime.Today.ToString("MM/dd/yyyy");
                }
            }
            else if (Request.QueryString["To"] != null)
            {
                DateTime dTo;
                if (DateTime.TryParse(Request.QueryString["To"].ToString(), out dTo))
                {
                    txtDateFrom.Text = dTo.ToString("MM/dd/yyyy");
                    txtDateTo.Text = dTo.ToString("MM/dd/yyyy");
                }
                else
                {
                    txtDateFrom.Text = DateTime.Today.ToString("MM/dd/yyyy");
                    txtDateTo.Text = DateTime.Today.ToString("MM/dd/yyyy");
                }
            }
            else
            {
                txtDateFrom.Text = DateTime.Today.AddDays(-28).ToString("MM/dd/yyyy");
                txtDateTo.Text = DateTime.Today.ToString("MM/dd/yyyy");
            }
        }
        #endregion

        #region[Gridview Selected Event]
        protected void odsDeposits_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue == null) return;
            if (e.ReturnValue.GetType().Name == "Int32")
            {
                Int32 count = Convert.ToInt32(e.ReturnValue);
                Int32 pagesize = gvDeposits.PageSize;
                Int32 pagecount = count / pagesize;
                if (count < pagesize)
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} of {0} records", count);
                }
                else
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} through {1} of {2} records"
                         , String.Format("{0:#,###}", pagesize * gvDeposits.PageIndex + 1)
                         , String.Format("{0:#,###}", pagesize * (gvDeposits.PageIndex + 1) > count ? count : pagesize * (gvDeposits.PageIndex + 1))
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

                    //Added by prem because if the current page index of grid is more than pagecount then it will give error
                    //if (pagecount >= gvDeposits.PageIndex + 1)
                    //{
                    ddlPaging.SelectedValue = String.Format("{0}", gvDeposits.PageIndex + 1);
                    ddlPaging1.SelectedValue = String.Format("{0}", gvDeposits.PageIndex + 1);
                    //}
                    //else
                    //{
                    //    ddlPaging.SelectedValue = String.Format("{0}", 1);
                    //    ddlPaging1.SelectedValue = String.Format("{0}", 1);

                    //    gvDeposits.DataSource = null;
                    //    gvDeposits.DataBind();
                    //    gvDeposits.PageIndex = 0;
                    //    string js = " <script type='text/javascript'>AutoFiredEvent();</script>";
                    //    this.RegisterStartupScript("refresh", js);

                    //}
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
            gvDeposits.PageIndex = 0;
            BindGrid();
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedIndex > 0)
                gvDeposits.PageIndex--;
            BindGrid();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedValue != ddlPaging.Items.Count.ToString())
                gvDeposits.PageIndex++;
            BindGrid();
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            gvDeposits.PageIndex = ddlPaging.Items.Count - 1;
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

            gvDeposits.PageIndex = 0;
            BindGrid();
        }
        #endregion

        #region[Drop down page selection change]
        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                gvDeposits.PageIndex = Convert.ToInt32(((DropDownList)sender).SelectedValue) - 1;
            BindGrid();
            EnablePaging();
        }
        #endregion

        #region[ Bind sort dropdown]
        private void BindSort1DDL()
        {
            try
            {
                ddlSort1.DataSource = objDeposit.GetSortData(14, "''"); //objDeposit.GetDataTable("Select SortText,SortValue from Lookup_Sort where tableid=14 order by Sequence asc");
                ddlSort1.DataTextField = "SortText";
                ddlSort1.DataValueField = "SortValue";
                ddlSort1.DataBind();
                ddlSort1.Items.Insert(0, new ListItem("", "-1"));
                ddlSort1.SelectedValue = "Inventory.DateAdded";
                BindSort2DDL();
                BindSort3DDL();
            }
            catch (Exception) { }
        }

        private void BindSort2DDL()
        {
            try
            {
                String excludeValues = "'" + ddlSort1.SelectedValue + "'";
                ddlSort2.DataSource = objDeposit.GetSortData(14, excludeValues); //objDeposit.GetDataTable(String.Format("Select SortText,SortValue from Lookup_Sort where tableid=14 AND SortValue NOT IN ('{0}') order by Sequence asc", ddlSort1.SelectedValue));
                ddlSort2.DataTextField = "SortText";
                ddlSort2.DataValueField = "SortValue";
                ddlSort2.DataBind();
                ddlSort2.Items.Insert(0, new ListItem("", "-1"));

                BindSort3DDL();
            }
            catch (Exception ex) { }
        }

        private void BindSort3DDL()
        {
            try
            {
                String excludeValues = "'" + ddlSort1.SelectedValue + "','" + ddlSort2.SelectedValue + "'";
                ddlSort3.DataSource = objDeposit.GetSortData(14, excludeValues);//objDeposit.GetDataTable(String.Format("Select SortText,SortValue from Lookup_Sort where tableid=14 AND SortValue NOT IN ('{0}', '{1}') order by Sequence asc", ddlSort1.SelectedValue, ddlSort2.SelectedValue));
                ddlSort3.DataTextField = "SortText";
                ddlSort3.DataValueField = "SortValue";
                ddlSort3.DataBind();
                ddlSort3.Items.Insert(0, new ListItem("", "-1"));
            }
            catch (Exception ex) { }

        }
        #endregion

        #region[Sort dropdown selected index changed handler]
        protected void ddlSort1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSort2DDL();
            ddlSort2.SelectedValue = "-1";
            ddlSort3.SelectedValue = "-1";
        }

        protected void ddlSort2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSort3DDL();
            ddlSort3.SelectedValue = "-1";
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

        protected void gvDeposits_Sorting(object sender, GridViewSortEventArgs e)
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
                if (direction == " ASC")
                    rbtnSort1Direction.SelectedIndex = 0;
                else
                    rbtnSort1Direction.SelectedIndex = 1;
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

        #region[Search button]
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvDeposits.PageIndex = 0;
            BindGrid();

        }
        #endregion

        protected void grdDepositHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Int64 InventoryID = Int64.Parse(hInventoryID.Value);
            MPEDepositHistory.Show();
            grdDepositHistory.PageIndex = e.NewPageIndex;
            BindGrid_DepositHistory(InventoryID, ViewState["ColumnName"].ToString());
        }

        protected void imgbtnDepositHistory_Click(object sender, ImageClickEventArgs e)
        {
            Int64 InventoryID;
            GridViewRow row = (GridViewRow)(sender as ImageButton).NamingContainer;
            InventoryID = (Int64)gvDeposits.DataKeys[row.RowIndex].Value;

            DataTable dt = objDeposit.GetInventoryDetail(InventoryID);

            lblVin.Text = dt.Rows[0]["VIN"].ToString();
            lblYear.Text = dt.Rows[0]["Year"].ToString();
            lblMake.Text = dt.Rows[0]["Make"].ToString();
            lblModel.Text = dt.Rows[0]["Model"].ToString();
            lblBody.Text = dt.Rows[0]["Body"].ToString();
            lblAddedBy.Text = dt.Rows[0]["AddedBy"].ToString();

            double depositAmount = 0.0, soldprice = 0.0;
            if (Double.TryParse(dt.Rows[0]["DepositAmount"].ToString(), out depositAmount) == false)
            {
                depositAmount = 0.0;
            }

            if (Double.TryParse(dt.Rows[0]["SoldPrice"].ToString(), out soldprice) == false)
            {
                soldprice = 0.0;
            }

            lblDepositAmount.Text = "$" + depositAmount.ToString("N2", System.Globalization.CultureInfo.InvariantCulture);
            lblSoldPrice.Text = "$" + soldprice.ToString("N2", System.Globalization.CultureInfo.InvariantCulture);


            lblSoldDate.Text = dt.Rows[0]["SoldDate"].ToString();
            lblSoldStatus.Text = dt.Rows[0]["SoldStatus"].ToString();
            lblSoldComment.Text = dt.Rows[0]["SoldComment"].ToString();
            lblBuyer.Text = dt.Rows[0]["BuyerName"].ToString();

            MPEDepositHistory.Show();
            hInventoryID.Value = InventoryID.ToString();
            ViewState["ColumnName"] = "All";
            grdDepositHistory.PageIndex = 0;
            BindGrid_DepositHistory(InventoryID, ViewState["ColumnName"].ToString());
        }

        private void BindGrid_DepositHistory(Int64 InventoryID, String ColumnName)
        {
            DataTable dt = objDeposit.GetDepositHistory(InventoryID, ColumnName);
            grdDepositHistory.DataSource = dt;
            grdDepositHistory.DataBind();
        }


        protected void ddlColumnName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            ViewState["ColumnName"] = ddl.SelectedValue;
            Int64 InventoryID = Int64.Parse(hInventoryID.Value);
            grdDepositHistory.PageIndex = 0;
            BindGrid_DepositHistory(InventoryID, ViewState["ColumnName"].ToString());
        }

        protected void grdDepositHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                DropDownList ddl = e.Row.FindControl("ddlColumnName") as DropDownList;
                ddl.SelectedValue = ViewState["ColumnName"].ToString();
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                String status = (e.Row.FindControl("lblColumnName") as Label).Text;
                if (status == "Sold Status")
                {
                    e.Row.Cells[1].Text = ChangeStatus(e.Row.Cells[1].Text);
                    e.Row.Cells[2].Text = ChangeStatus(e.Row.Cells[2].Text);
                }
            }
        }

        private string ChangeStatus(string numStatus)
        {
            string result = "";
            switch (numStatus)
            {
                case "0":
                    result = "No";
                    break;
                case "1":
                    result = "Yes";
                    break;
                case "2":
                    result = "Sold Not Paid";
                    break;
            }
            return result;
        }



    }
}