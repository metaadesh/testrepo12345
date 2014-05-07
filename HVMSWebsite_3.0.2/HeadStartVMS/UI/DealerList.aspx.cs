using System;
using System.Collections;
using System.Collections.Generic;
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
using METAOPTION.BAL;

namespace METAOPTION.UI
{
    public partial class DealerList : System.Web.UI.Page
    {
        Common objCommon = new Common();
        MasterBAL objMasterBAL = new MasterBAL();


        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["MasterPageFullScreen"])))
                    this.MasterPageFile = Convert.ToString(Session["MasterPageFullScreen"]);
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

            if (hWelcomeTasks.Value != null)
            {

              //  ddlWelcomeTasks.SelectedIndex = hWelcomeTasks.Value;
            }

            if (!(Page.IsPostBack || IsCallback))
            {
                dvSearch.Style.Add("display", "block");
                FillControls();
                BindSort1DDL();

                if (Request.Cookies["DealerListHistory"] != null)
                    FillFromCookies();

                SearchDealers();
                if (Request.Cookies["DealerListHistory"] != null)
                {
                    HttpCookie cookie = (HttpCookie)Request.Cookies["DealerListHistory"];
                    if (Convert.ToString(cookie.Values["DPage"]) != "-1")
                        if (ddlPaging1.Items.FindByValue(Convert.ToString(cookie.Values["DPage"])) != null)
                            ddlPaging1.SelectedValue = Convert.ToString(cookie.Values["DPage"]);

                    if (Convert.ToString(cookie.Values["DSize"]) != "-1")
                        if (ddlPageSize1.Items.FindByValue(Convert.ToString(cookie.Values["DSize"])) != null)
                            ddlPageSize1.SelectedValue = Convert.ToString(cookie.Values["DSize"]);

                    ddlPaging_SelectedIndexChanged(ddlPaging1, e);
                }
            }
            // btnSearch_Click(sender, e);
        }

        #region[Check Permission method]
        /// <summary>
        /// Method that check permision
        /// of User.
        /// </summary>
        private void CheckPermission()
        {
            List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, "CUSTOMERDEALER");
            bool bTrue = true;

            if (!dict.Contains("CUSTOMERDEALER.EDIT"))
            {
                this.gvViewDealer.Columns[1].Visible = false;
                bTrue = false;
            }
            if (!(dict.Contains("CUSTOMERDEALER.VIEW") || bTrue))
                Response.Redirect("Permission.aspx?MSG=CUSTOMERDEALER.EDIT OR CUSTOMERDEALER.VIEW");


        }
        #endregion

        #region[Bind controls]
        protected void FillControls()
        {
            //Fill Country Drop Down
            ddlCountry.DataSource = objCommon.GetCountryList();
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryId";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("", "-1"));

            //if (this.ddlCountry.Items.Count > 0)
            //{
            //    if (Convert.ToInt32(ddlCountry.SelectedValue) == -1)
            //    {
            //        this.ddlState.Items.Clear();
            //        this.ddlState.Items.Insert(0, new ListItem("ALL", "-1"));
            //    }
            //    else
            //    {

            //    }
            //}

            //Fill Dealer Type Drop Down
            ddlType.DataSource = objMasterBAL.GetDealerType();
            ddlType.DataTextField = "DealerType1";
            ddlType.DataValueField = "DealerTypeId";
            ddlType.DataBind();
            ddlType.Items.Insert(0, new ListItem("", "-1"));

            // Fill State Drop Down   
            BindState();
            BindWelcomeTasks();

            // Fill Dealer Category Drop Down           
            ddlCategory.DataSource = BAL.Common.GetDealerCategory(1);
            ddlCategory.DataTextField = "Category";
            ddlCategory.DataValueField = "DealerCategoryId";
            ddlCategory.DataBind();

            ddlCategory.Items.Insert(0, new ListItem("", "-1"));
        }

        protected void BindState()
        {
            // Fill State Drop Down    
            ddlState.DataSource = BAL.Common.GetStateList(Convert.ToInt32(ddlCountry.SelectedValue));
            ddlState.DataTextField = "State";
            ddlState.DataValueField = "StateId";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("", "-1"));
        }

        private void BindWelcomeTasks()
        {
            DealerCustomerBAL bal = new DealerCustomerBAL();
            ddlWelcomeTasks.DataSource = bal.GetWelcomeTasks();
            ddlWelcomeTasks.DataTextField = "Task";
            ddlWelcomeTasks.DataValueField = "WelcomeTaskID";
            ddlWelcomeTasks.DataBind();
            if (hWelcomeTasks != null)
            {
              

            }
            //ddlWelcomeTasks.Items.Insert(0, new ListItem("", ""));
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvSearch.Style.Add("display", "block");
            BindState();
        }
        #endregion

        #region[Search Dealers]
        public void SearchDealers()
        {
            string strWelcomeTasks = string.Empty;
            gvViewDealer.PageSize = Convert.ToInt32(ddlPageSize1.SelectedValue);

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
                sort = "DealerName ASC";

            // Added to stop data inconsistency
            sort += ", Dealer.DealerID DESC";

            // remove this hardcode value, bring this from text box. if text box is empty or any unwanted data is there then it will be -1
            Int32 DaysLastTransaction = -1;
            if (Int32.TryParse(txtDaySinceTransaction.Text.Trim(), out DaysLastTransaction) == false)
            {
                DaysLastTransaction = -1;
            }

            String DateSince = String.Empty;

            if (String.IsNullOrEmpty(txtNewCustomerSince.Text))
                DateSince = "1/1/1950";
            else
            {
                DateTime cusSince = new DateTime();
                if (DateTime.TryParse(txtNewCustomerSince.Text.Replace("'", "").Trim(), out cusSince) == true)
                {
                    DateSince = cusSince.ToShortDateString();
                }
                else
                {
                    DateSince = "1/1/1950";
                    txtNewCustomerSince.Text = "";
                }
            }

            if (!string.IsNullOrEmpty(hWelcomeTasks.Value))
                strWelcomeTasks = hWelcomeTasks.Value;
            else
                strWelcomeTasks = "-1";

            ObjectDataSource odsDealerSelector = new ObjectDataSource();
            odsDealerSelector.Selected += new ObjectDataSourceStatusEventHandler(odsDealerSelector_Selected);
            odsDealerSelector.TypeName = "METAOPTION.BAL.DealerCustomerBAL";
            odsDealerSelector.SelectMethod = "DealerSearchList";
            odsDealerSelector.SelectCountMethod = "DealerSearchListCount";
            odsDealerSelector.EnablePaging = true;
            odsDealerSelector.SelectParameters.Add("dealerName", txtName.Text.Trim());
            odsDealerSelector.SelectParameters.Add("categoryId", ddlCategory.SelectedValue);
            odsDealerSelector.SelectParameters.Add("typeId", ddlType.SelectedValue);
            odsDealerSelector.SelectParameters.Add("city", txtCity.Text.Trim());
            odsDealerSelector.SelectParameters.Add("countryId", ddlCountry.SelectedValue);
            odsDealerSelector.SelectParameters.Add("stateId", ddlState.SelectedValue);
            odsDealerSelector.SelectParameters.Add("zip", txtZip.Text.Trim());
            odsDealerSelector.SelectParameters.Add("StartRowIndex", DbType.Int32, gvViewDealer.PageIndex.ToString());
            odsDealerSelector.SelectParameters.Add("MaximumRows", DbType.Int32, ddlPageSize1.SelectedValue);
            odsDealerSelector.SelectParameters.Add("SortExpression", sort);
            odsDealerSelector.SelectParameters.Add("Status", DbType.Int32, ddlStatus.SelectedValue);
            odsDealerSelector.SelectParameters.Add("OrgID", DbType.Int32, Constant.OrgID.ToString());

            odsDealerSelector.SelectParameters.Add("DaysLastTransaction", DbType.Int32, DaysLastTransaction.ToString());
            odsDealerSelector.SelectParameters.Add("DateSince", DbType.String, DateSince);

            odsDealerSelector.SelectParameters.Add("WelcomeTasks", DbType.String, strWelcomeTasks);
            gvViewDealer.DataSource = odsDealerSelector;
            gvViewDealer.DataBind();

            WriteCookies();

        }
        #endregion

        #region[Drop down paging]
        protected void odsDealerSelector_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue == null)
                return;
            if (e.ReturnValue.GetType().Name == "Int32")
            {

                Int32 count = Convert.ToInt32(e.ReturnValue);
                Int32 pagesize = gvViewDealer.PageSize;
                Int32 pagecount = count / pagesize;
                if (count < pagesize)
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} of {0} records", count);
                }
                else
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} through {1} of {2} records"
                         , String.Format("{0:#,###}", pagesize * gvViewDealer.PageIndex + 1)
                         , String.Format("{0:#,###}", pagesize * (gvViewDealer.PageIndex + 1) > count ? count : pagesize * (gvViewDealer.PageIndex + 1))
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

                    ddlPaging.SelectedValue = String.Format("{0}", gvViewDealer.PageIndex + 1);
                    ddlPaging1.SelectedValue = String.Format("{0}", gvViewDealer.PageIndex + 1);
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
            gvViewDealer.PageIndex = 0;
            SearchDealers();
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedIndex > 0)
                gvViewDealer.PageIndex--;
            SearchDealers();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedValue != ddlPaging.Items.Count.ToString())
                gvViewDealer.PageIndex++;
            SearchDealers();
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            gvViewDealer.PageIndex = ddlPaging.Items.Count - 1;
            SearchDealers();
        }
        #endregion

        #region[Drop down page selection change]
        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                gvViewDealer.PageIndex = Convert.ToInt32(((DropDownList)sender).SelectedValue) - 1;

            SearchDealers();
            EnablePaging();
            WriteCookies();
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

            gvViewDealer.PageIndex = 0;
            SearchDealers();
        }
        #endregion

        #region[Search button handler]
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvViewDealer.PageIndex = 0;
            SearchDealers();
            //dvCollapse.Style.Add("display", "none");
            //dvExpand.Style.Add("display", "block");
            //dvSearch.Style.Add("display", "none");
            //WriteCookies();
        }
        #endregion

        #region[Sort dropdown]
        private void BindSort1DDL()
        {
            ddlSort1.DataSource = DealerCustomerBAL.DealerList_SortOptions("SELECT * FROM LookUp_Sort WHERE TableId = 2", "", "");
            ddlSort1.DataTextField = "SortText";
            ddlSort1.DataValueField = "SortValue";
            ddlSort1.DataBind();

            ddlSort1.Items.Insert(0, new ListItem("", "-1"));
            BindSort2DDL();
        }

        private void BindSort2DDL()
        {
            ddlSort2.Items.Clear();
            ddlSort2.DataSource = DealerCustomerBAL.DealerList_SortOptions(String.Format("SELECT * FROM LookUp_Sort WHERE TableId = 2 AND SortValue NOT IN ('{0}')", ddlSort1.SelectedValue), "", "");
            ddlSort2.DataTextField = "SortText";
            ddlSort2.DataValueField = "SortValue";
            ddlSort2.DataBind();

            ddlSort2.Items.Insert(0, new ListItem("", "-1"));
            BindSort3DDL();
        }

        private void BindSort3DDL()
        {
            ddlSort3.Items.Clear();
            ddlSort3.DataSource = DealerCustomerBAL.DealerList_SortOptions(String.Format("SELECT * FROM LookUp_Sort WHERE TableId =2 AND SortValue NOT IN ('{0}', '{1}')", ddlSort1.SelectedValue, ddlSort2.SelectedValue), "", "");
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
        #endregion

        #region[Save searches in cookie]
        private void WriteCookies()
        {
            HttpCookie cookie = new HttpCookie("DealerListHistory");
            cookie.Values["DName"] = txtName.Text.Trim();
            cookie.Values["DCategory"] = ddlCategory.SelectedValue;
            cookie.Values["DType"] = ddlType.SelectedValue;
            cookie.Values["DCity"] = txtCity.Text.Trim();
            cookie.Values["DCountry"] = ddlCountry.SelectedValue;
            cookie.Values["DState"] = ddlState.SelectedValue;
            cookie.Values["DStatus"] = ddlStatus.SelectedValue;
            cookie.Values["DZip"] = txtZip.Text.Trim();
            cookie.Values["DDaySinceLastTrans"] = txtDaySinceTransaction.Text.Trim();
            cookie.Values["DNewCustomerSince"] = txtNewCustomerSince.Text.Trim();
            cookie.Values["DWelcomeKit"] = hWelcomeTasks.Value;
            cookie.Values["DS1"] = ddlSort1.SelectedValue;
            cookie.Values["DS2"] = ddlSort2.SelectedValue;
            cookie.Values["DS3"] = ddlSort3.SelectedValue;
            cookie.Values["DRadioS1"] = rbtnSort1Direction.SelectedValue;
            cookie.Values["DRadioS2"] = rbtnSort2Direction.SelectedValue;
            cookie.Values["DRadioS3"] = rbtnSort3Direction.SelectedValue;
            cookie.Values["DPage"] = ddlPaging1.SelectedValue;
            cookie.Values["DSize"] = ddlPageSize1.SelectedValue = ddlPageSize2.SelectedValue;
            cookie.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Add(cookie);
        }
        #endregion

        #region[Read and fill search from cookie]
        private void FillFromCookies()
        {
            if (Request.Cookies["DealerListHistory"] != null)
            {
                HttpCookie cookie = (HttpCookie)Request.Cookies["DealerListHistory"];
                txtName.Text = Convert.ToString(cookie.Values["DName"]);

                if (Convert.ToString(cookie.Values["DCategory"]) != "-1")
                    if (ddlCategory.Items.FindByValue(Convert.ToString(cookie.Values["DCategory"])) != null)
                        ddlCategory.SelectedValue = Convert.ToString(cookie.Values["DCategory"]);

                if (Convert.ToString(cookie.Values["DType"]) != "-1")
                    if (ddlType.Items.FindByValue(Convert.ToString(cookie.Values["DType"])) != null)
                        ddlType.SelectedValue = Convert.ToString(cookie.Values["DType"]);

                txtCity.Text = Convert.ToString(cookie.Values["DCity"]);

                if (Convert.ToString(cookie.Values["DCountry"]) != "-1")
                    if (ddlCountry.Items.FindByValue(Convert.ToString(cookie.Values["DCountry"])) != null)
                        ddlCountry.SelectedValue = Convert.ToString(cookie.Values["DCountry"]);


                if (Convert.ToString(cookie.Values["DState"]) != "-1")
                    if (ddlCountry.SelectedValue != "-1")
                        BindState();
                if (ddlState.Items.FindByValue(Convert.ToString(cookie.Values["DState"])) != null)
                    ddlState.SelectedValue = Convert.ToString(cookie.Values["DState"]);

                if (Convert.ToString(cookie.Values["DStatus"]) != "-1")
                    if (ddlStatus.Items.FindByValue(Convert.ToString(cookie.Values["DStatus"])) != null)
                        ddlStatus.SelectedValue = Convert.ToString(cookie.Values["DStatus"]);

                txtZip.Text = Convert.ToString(cookie.Values["DZip"]);
                txtDaySinceTransaction.Text = Convert.ToString(cookie.Values["DDaySinceLastTrans"]);
                txtNewCustomerSince.Text=Convert.ToString(cookie.Values["DNewCustomerSince"]);

                hWelcomeTasks.Value = Convert.ToString(cookie.Values["DWelcomeKit"]);

                if (Convert.ToString(cookie.Values["DS1"]) != "-1")
                    if (ddlSort1.Items.FindByValue(Convert.ToString(cookie.Values["DS1"])) != null)
                    {
                        ddlSort1.SelectedValue = Convert.ToString(cookie.Values["DS1"]);
                        BindSort2DDL();
                    }
                if (Convert.ToString(cookie.Values["DS2"]) != "-1")
                    if (ddlSort2.Items.FindByValue(Convert.ToString(cookie.Values["DS2"])) != null)
                    {
                        ddlSort2.SelectedValue = Convert.ToString(cookie.Values["DS2"]);

                        BindSort3DDL();
                    }

                if (Convert.ToString(cookie.Values["DS3"]) != "-1")
                    if (ddlSort3.Items.FindByValue(Convert.ToString(cookie.Values["DS3"])) != null)
                        ddlSort3.SelectedValue = Convert.ToString(cookie.Values["DS3"]);

                if (ddlSort1.SelectedValue.ToString() != "-1")
                    if (Convert.ToString(cookie.Values["DRadioS1"]) != null)
                        rbtnSort1Direction.SelectedValue = cookie.Values["DRadioS1"];

                if (ddlSort2.SelectedValue.ToString() != "-1")
                    if (Convert.ToString(cookie.Values["DRadioS2"]) != null)
                        rbtnSort2Direction.SelectedValue = cookie.Values["DRadioS2"];

                if (ddlSort3.SelectedValue.ToString() != "-1")
                    if (Convert.ToString(cookie.Values["DRadioS3"]) != null)
                        rbtnSort3Direction.SelectedValue = cookie.Values["DRadioS3"];

                if (Convert.ToString(cookie.Values["DPage"]) != "-1")
                    if (ddlPaging1.Items.FindByValue(Convert.ToString(cookie.Values["DPage"])) != null)
                        ddlPaging1.SelectedValue = Convert.ToString(cookie.Values["DPage"]);

                if (Convert.ToString(cookie.Values["DSize"]) != "-1")
                    if (ddlPageSize1.Items.FindByValue(Convert.ToString(cookie.Values["DSize"])) != null)
                        ddlPageSize1.SelectedValue = ddlPageSize2.SelectedValue = Convert.ToString(cookie.Values["DSize"]);

                //cookie.Expires = DateTime.Now.AddMonths(1);
                //Response.Cookies.Add(cookie);
            }
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

        protected void gvViewDealer_Sorting(object sender, GridViewSortEventArgs e)
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

            SearchDealers();
        }

        #endregion

        #region[Reset button to clear all filed]
        public void btnReset_Click(object sender, EventArgs e)
        {
            Response.Cookies["DealerListHistory"].Expires = DateTime.Now.AddDays(-1d);
            Response.Redirect("../UI/DealerList.aspx");
        }
        #endregion

        protected void btnViewMap_Click(object sender, EventArgs e)
        {
            string strWelcomeTasks = string.Empty;
            gvViewDealer.PageSize = Convert.ToInt32(ddlPageSize1.SelectedValue);

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
                sort = "DealerName ASC";
            sort += ", Dealer.DealerID DESC";
            Int32 DaysLastTransaction = -1;
            if (Int32.TryParse(txtDaySinceTransaction.Text.Trim(), out DaysLastTransaction) == false)
            {
                DaysLastTransaction = -1;
            }
            String DateSince = String.Empty;
            if (String.IsNullOrEmpty(txtNewCustomerSince.Text))
                DateSince = "1/1/1950";
            else
            {
                DateTime cusSince = new DateTime();
                if (DateTime.TryParse(txtNewCustomerSince.Text.Replace("'", "").Trim(), out cusSince) == true)
                {
                    DateSince = cusSince.ToShortDateString();
                }
                else
                {
                    DateSince = "1/1/1950";
                    txtNewCustomerSince.Text = "";
                }
            }

            if (!string.IsNullOrEmpty(hWelcomeTasks.Value))
                strWelcomeTasks = hWelcomeTasks.Value;
            else
                strWelcomeTasks = "-1";


            String QueryString = "";
            QueryString += "dealerName=" + Server.UrlEncode(txtName.Text.Trim());
            QueryString += "&categoryId=" + Server.UrlEncode(ddlCategory.SelectedValue);
            QueryString += "&typeId=" + Server.UrlEncode(ddlType.SelectedValue);
            QueryString += "&city=" + Server.UrlEncode(txtCity.Text.Trim());
            QueryString += "&countryId=" + Server.UrlEncode(ddlCountry.SelectedValue);
            QueryString += "&stateId=" + Server.UrlEncode(ddlState.SelectedValue);
            QueryString += "&zip=" + Server.UrlEncode(txtZip.Text.Trim());
            QueryString += "&StartRowIndex=" + Server.UrlEncode(gvViewDealer.PageIndex.ToString());
            QueryString += "&MaximumRows=" + Server.UrlEncode(ddlPageSize1.SelectedValue);
            QueryString += "&SortExpression=" + Server.UrlEncode(sort);
            QueryString += "&Status=" + Server.UrlEncode(ddlStatus.SelectedValue);
            QueryString += "&OrgID=" + Server.UrlEncode(Constant.OrgID.ToString());
            QueryString += "&DaysLastTransaction=" + Server.UrlEncode(DaysLastTransaction.ToString());
            QueryString += "&DateSince=" + Server.UrlEncode(DateSince);
            QueryString += "&WelcomeTasks=" + Server.UrlEncode(strWelcomeTasks);


            //odsDealerSelector.SelectParameters.Add("dealerName", );
            //odsDealerSelector.SelectParameters.Add("categoryId", ddlCategory.SelectedValue);
            //odsDealerSelector.SelectParameters.Add("typeId", ddlType.SelectedValue);//
            //odsDealerSelector.SelectParameters.Add("city", txtCity.Text.Trim());//
            //odsDealerSelector.SelectParameters.Add("countryId", ddlCountry.SelectedValue);//
            //odsDealerSelector.SelectParameters.Add("stateId", ddlState.SelectedValue);
            //odsDealerSelector.SelectParameters.Add("zip", txtZip.Text.Trim());//
            //odsDealerSelector.SelectParameters.Add("StartRowIndex", DbType.Int32, gvViewDealer.PageIndex.ToString());//
            //odsDealerSelector.SelectParameters.Add("MaximumRows", DbType.Int32, ddlPageSize1.SelectedValue);//
            //odsDealerSelector.SelectParameters.Add("SortExpression", sort);//

            //odsDealerSelector.SelectParameters.Add("Status", DbType.Int32, ddlStatus.SelectedValue);//
            //odsDealerSelector.SelectParameters.Add("OrgID", DbType.Int32, Constant.OrgID.ToString());//
            //odsDealerSelector.SelectParameters.Add("DaysLastTransaction", DbType.Int32, DaysLastTransaction.ToString());//
            //odsDealerSelector.SelectParameters.Add("DateSince", DbType.String, DateSince);
            //odsDealerSelector.SelectParameters.Add("WelcomeTasks", DbType.String, strWelcomeTasks);


            Response.Redirect("DealerListGoogleMap.aspx?" + QueryString);

        }
    }
}
