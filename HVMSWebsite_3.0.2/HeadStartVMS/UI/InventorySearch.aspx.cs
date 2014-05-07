using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using METAOPTION.BAL;
using System.Text;
using System.Collections;
using System.Web.Services;
using System.IO;

namespace METAOPTION.UI
{
    public partial class InventorySearch : System.Web.UI.Page
    {
        public string UCRlinkUrl = String.Empty;
        public string CreateUCRUrl = String.Empty;

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
            //Set OrgID context key of AutoCompleteExtender from Session
            txtCustomer_AutoCompleteExtender.ContextKey = txtDealer_AutoCompleteExtender.ContextKey = Convert.ToString(Constant.OrgID);

            UCRlinkUrl = ConfigurationManager.AppSettings["UCRlinkUrl"];
            CreateUCRUrl = ConfigurationManager.AppSettings["CreateUCRUrl"];

            if (!(Page.IsPostBack || Page.IsCallback))
            {
                if (Convert.ToString(Session["LoginEntityTypeID"]) == "1")
                {
                    Table4.Visible = false;

                }
                CheckPagePermission();
                dvSearch.Style.Add("display", "block");
                //FillControls();
                FillControls_Optimized();
                BindSort1DDL();
                
                // The default sold date servch will be of 4 weeks
                txtSoldDateFrom.Text = DateTime.Today.AddDays(-28).ToString("MM/dd/yyyy");
                txtSoldDateTo.Text = DateTime.Today.ToString("MM/dd/yyyy");

                if (Request.Cookies["InvSearchHistory"] != null)
                    FillFromCookies();

                BindGrid();


                if (Request.Cookies["InvSearchHistory"] != null)
                {
                    HttpCookie cookie = (HttpCookie)Request.Cookies["InvSearchHistory"];
                    if (Convert.ToString(cookie.Values["PNo"]) != "-1")
                        if (ddlPaging1.Items.FindByValue(Convert.ToString(cookie.Values["PNo"])) != null)
                            ddlPaging1.SelectedValue = Convert.ToString(cookie.Values["PNo"]);

                    if (Convert.ToString(cookie.Values["PSz"]) != "-1")
                        if (ddlPageSize1.Items.FindByValue(Convert.ToString(cookie.Values["PSz"])) != null)
                            ddlPageSize1.SelectedValue = Convert.ToString(cookie.Values["PSz"]);

                    ddlPaging_SelectedIndexChanged(ddlPaging1, e);
                }
                /* */

                //This functionality depriciated here and move to Quick Search Page
                //if (Request.QueryString["param1"] != null && Request.QueryString["param2"] != null)
                //{
                //    string VINMatch = Request.QueryString["param1"];
                //    string VIN_ToSearch = Request.QueryString["param2"];
                //    txtVINNo.Text = VIN_ToSearch;
                //    if (ddlVINPartern.Items.FindByValue(VINMatch) != null)
                //        ddlVINPartern.SelectedValue = VINMatch;

                //    //If only one row,redirect user to ManageInventory.aspx page
                //    if (gvInventoryList.Rows.Count == 1)
                //        Response.Redirect("ManageInventory.aspx?Code=" + gvInventoryList.DataKeys[0].Value + "");

                //}
            }
            //Set Focus on VINNO textbox using current scriptmanager associated with this page
            //ScriptManager.GetCurrent(this).SetFocus(txtVINNo);
        }

        #region [ Year ]
        private void BindYear()
        {
            this.ddlFromYear.DataSource = BAL.Common.GetYearList();
            this.ddlFromYear.DataTextField = "Year";
            this.ddlFromYear.DataValueField = "Year";
            this.ddlFromYear.DataBind();
            this.ddlFromYear.Items.Insert(0, new ListItem("ALL", "-1"));
        }


        private void BindToYear()
        {
            this.ddlToYear.DataSource = BAL.Common.GetYearList();
            this.ddlToYear.DataTextField = "Year";
            this.ddlToYear.DataValueField = "Year";
            this.ddlToYear.DataBind();
            this.ddlToYear.Items.Insert(0, new ListItem("ALL", "-1"));
        }

        protected void ddlFromYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvSearch.Style.Add("display", "block");
            ddlToYear.SelectedValue = ddlFromYear.SelectedValue;
        }
        #endregion

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

        #region[ Fill controls-Old Code ]
        /*
        private void FillControls()
        {
            // Fill Buyer List
            this.ddlBuyer.DataSource = BAL.Common.GetBuyerList();
            this.ddlBuyer.DataValueField = "BuyerId";
            this.ddlBuyer.DataTextField = "BuyerName";
            this.ddlBuyer.DataBind();
            this.ddlBuyer.Items.Insert(0, new ListItem("", "-1"));

            // Fill Designations
            this.ddlDesignation.DataSource = BAL.Common.GetDesignations();
            this.ddlDesignation.DataTextField = "Designation";
            this.ddlDesignation.DataValueField = "DesignationId";
            this.ddlDesignation.DataBind();
            this.ddlDesignation.Items.Insert(0, new ListItem("", "-1"));

            // Fill Years
            BindYear();    // Years From
            BindToYear();  // Years To
            BindMake();    //Fill all makes
            FillCountry();
            this.ddlModel.Items.Insert(0, new ListItem("ALL", "-1"));
            this.ddlBody.Items.Insert(0, new ListItem("ALL", "-1"));
        }
         */
        #endregion

        #region[ Fill controls-Optimized]
        private void FillControls_Optimized()
        {
            System.Collections.ArrayList arraylist = new ArrayList();
            String ParentEntityID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString();
            if (Convert.ToString(Session["BuyerAccessLevel"]) == "1")
                arraylist = BAL.Common.GetDataForInventorySearchDDLs(Convert.ToInt32(Session["UserEntityID"]), -1, Convert.ToInt32(Session["LoginEntityTypeID"]), Constant.OrgID);
            else
                arraylist = BAL.Common.GetDataForInventorySearchDDLs(Convert.ToInt32(Session["UserEntityID"]), Convert.ToInt32(ParentEntityID), Convert.ToInt32(Session["LoginEntityTypeID"]), Constant.OrgID);

            // Fill Buyer List
            this.ddlBuyer.DataSource = arraylist[0];
            this.ddlBuyer.DataValueField = "BuyerId";
            this.ddlBuyer.DataTextField = "BuyerName";
            this.ddlBuyer.DataBind();
            this.ddlBuyer.Items.Insert(0, new ListItem("", "-1"));
            String ParentID = "-1";
            if (Convert.ToString(Session["BuyerAccessLevel"]) != "1")
                ParentID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString();

            if (Convert.ToString(Session["LoginEntityTypeID"]) == "2" && ParentID == "-1" && ddlBuyer.Items.Count == 2)
            {
                this.ddlBuyer.SelectedValue = Convert.ToString(Session["UserEntityID"]);
                this.ddlBuyer.Enabled = false;
            }
            else if (Convert.ToString(Session["LoginEntityTypeID"]) == "2" && ddlBuyer.Items.FindByValue(Convert.ToString(Session["UserEntityID"])) != null)
                this.ddlBuyer.SelectedValue = Convert.ToString(Session["UserEntityID"]);


            // Fill Designations
            this.ddlDesignation.DataSource = arraylist[1];
            this.ddlDesignation.DataTextField = "Designation";
            this.ddlDesignation.DataValueField = "DesignationId";
            this.ddlDesignation.DataBind();
            this.ddlDesignation.Items.Insert(0, new ListItem("", "-1"));

            //Fill From Year
            this.ddlFromYear.DataSource = arraylist[2];
            this.ddlFromYear.DataTextField = "Year";
            this.ddlFromYear.DataValueField = "Year";
            this.ddlFromYear.DataBind();
            this.ddlFromYear.Items.Insert(0, new ListItem("ALL", "-1"));

            //Fill To Year
            this.ddlToYear.DataSource = arraylist[2];
            this.ddlToYear.DataTextField = "Year";
            this.ddlToYear.DataValueField = "Year";
            this.ddlToYear.DataBind();
            this.ddlToYear.Items.Insert(0, new ListItem("ALL", "-1"));

            //Fill All Makes
            this.ddlModel.Items.Clear();
            this.ddlMake.DataSource = arraylist[3];
            this.ddlMake.DataValueField = "MakeId";
            this.ddlMake.DataTextField = "Make";
            this.ddlMake.DataBind();
            this.ddlMake.Items.Insert(0, new ListItem("ALL", "-1"));

            //Fill Country
            this.ddlCountry.DataSource = arraylist[4];
            this.ddlCountry.DataTextField = "CountryName";
            this.ddlCountry.DataValueField = "CountryId";
            this.ddlCountry.DataBind();
            this.ddlCountry.Items.Insert(0, new ListItem("", "-1"));
            this.ddlDealerState.Items.Insert(0, new ListItem("", "-1"));

            this.ddlModel.Items.Insert(0, new ListItem("ALL", "-1"));
            this.ddlBody.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        #endregion

        #region[Bind grid]
        protected void BindGrid()
        {
            String SoldDateFrom, SoldDateTo;

            String ParentBuyerID = "-1";

            if (Convert.ToString(Session["BuyerAccessLevel"]) != "1")
                ParentBuyerID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString();
            // ddlPageSize1.SelectedValue = ddlPageSize2.SelectedValue;
            gvInventoryList.PageSize = Convert.ToInt32(ddlPageSize1.SelectedValue);

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

            // Added to stop data inconsistency
            sort += ", Inventory.InventoryId DESC";


            if (String.IsNullOrEmpty(txtSoldDateFrom.Text))
                SoldDateFrom = DateTime.Today.AddYears(-100).ToShortDateString();
            else
                SoldDateFrom = txtSoldDateFrom.Text.Replace("'", "");

            if (String.IsNullOrEmpty(txtSoldDateTo.Text))
                SoldDateTo = DateTime.Today.AddDays(1).ToShortDateString();
            else
                SoldDateTo = Convert.ToDateTime(txtSoldDateTo.Text.Replace("'", "")).ToShortDateString();


            ObjectDataSource odsInventory = new ObjectDataSource();
            odsInventory.Selected += new ObjectDataSourceStatusEventHandler(odsInventory_Selected);
            odsInventory.TypeName = "METAOPTION.BAL.InventoryBAL";
            odsInventory.SelectMethod = "SearchInventory";
            odsInventory.SelectCountMethod = "SearchInventoryCount";
            odsInventory.EnablePaging = true;
            odsInventory.SelectParameters.Add("VInMatch", ddlVINPattern.SelectedValue);
            odsInventory.SelectParameters.Add("VINNo", txtVINNo.Text.Trim());
            odsInventory.SelectParameters.Add("checkNo", txtCheckNo.Text.Trim());

            if (ddlFromYear.SelectedValue == "-1")
                odsInventory.SelectParameters.Add("FromYear", "1950");
            else
                odsInventory.SelectParameters.Add("FromYear", ddlFromYear.SelectedValue);

            if (ddlToYear.SelectedValue == "-1")
                odsInventory.SelectParameters.Add("ToYear", DateTime.Now.AddYears(1).Year.ToString());
            else
                odsInventory.SelectParameters.Add("ToYear", ddlToYear.SelectedValue);

            odsInventory.SelectParameters.Add("makeId", ddlMake.SelectedValue);
            odsInventory.SelectParameters.Add("modelId", ddlModel.SelectedValue);
            odsInventory.SelectParameters.Add("BodyId", ddlBody.SelectedValue);
            odsInventory.SelectParameters.Add("dealerId", hfDealerId.Value);
            odsInventory.SelectParameters.Add("customerId", hfCustomerId.Value);
            odsInventory.SelectParameters.Add("buyerId", ddlBuyer.SelectedValue);
            odsInventory.SelectParameters.Add("designationId", ddlDesignation.SelectedValue);
            odsInventory.SelectParameters.Add("comeBack", ddlComeBack.SelectedValue);
            odsInventory.SelectParameters.Add("sold", ddlSold.SelectedValue);
            odsInventory.SelectParameters.Add("CarStatus", ddlCarStatus.SelectedValue);
            odsInventory.SelectParameters.Add("CRStatus", ddlUCR.SelectedValue);
            odsInventory.SelectParameters.Add("StartRowIndex", DbType.Int32, gvInventoryList.PageIndex.ToString());
            odsInventory.SelectParameters.Add("MaximumRows", DbType.Int32, ddlPageSize1.SelectedValue);
            odsInventory.SelectParameters.Add("SystemID", DbType.Int32, Convert.ToString(Session["SystemId"]));
            odsInventory.SelectParameters.Add("SortExpression", sort);
            odsInventory.SelectParameters.Add("EntityTypeID", DbType.Int32, Convert.ToString(Session["LoginEntityTypeID"]));
            odsInventory.SelectParameters.Add("EntityID", DbType.Int32, Convert.ToString(Session["UserEntityID"]));
            odsInventory.SelectParameters.Add("ParentEntityID", ParentBuyerID);
            odsInventory.SelectParameters.Add("OrgID", DbType.Int16, Constant.OrgID.ToString());
            odsInventory.SelectParameters.Add("SoldDateFrom", SoldDateFrom);
            odsInventory.SelectParameters.Add("SoldDateTo", SoldDateTo);

            gvInventoryList.DataSource = odsInventory;
            gvInventoryList.DataBind();

            WriteCookies();
        }
        #endregion

        #region[ Select Dealer Popup ]
        #region [ Fill Country & State ]
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
        #endregion

        #region[ Search Dealer ]
        ///// <summary>
        ///// Search Dealer
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void SearchDealer()
        //{
        //   AddInventoryBAL objAddInventoryBAL = new AddInventoryBAL();
        //   gvDealerDetails.DataSource = InventoryBAL.SearchDealer(
        //      this.txtDealerName.Text.Trim()
        //      , this.txtCity.Text.Trim()
        //      , Convert.ToInt16(this.ddlDealerState.SelectedValue)
        //      , this.txtZip.Text.Trim()
        //      , Convert.ToInt16(this.ddlCountry.SelectedValue));
        //   gvDealerDetails.DataBind();

        //}
        #endregion

        #region[ Dealer Search button clicked ]
        protected void btnSearchDealers_Click(object sender, EventArgs e)
        {
            this.mpeOpenDealerSelector.Show();

            // Change the popup titile
            if (hfDealerCustomerType.Value == "1")
                this.lblDealerCustomerHeading.Text = "Select Customer";
            else
                this.lblDealerCustomerHeading.Text = "Select Dealer";
        }
        #endregion

        protected void imgbtnSelect_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton img = (ImageButton)sender;
            GridViewRow gvRow = (GridViewRow)img.NamingContainer;
            if (hfDealerCustomerType.Value == "1")
            {
                hfCustomerId.Value = Convert.ToString(this.gvDealerDetails.DataKeys[gvRow.RowIndex].Value);
                string temp = gvRow.Cells[2].Text + "(" +
                         gvRow.Cells[3].Text + ", " +
                         gvRow.Cells[4].Text + ", " +
                         gvRow.Cells[6].Text + "-" +
                         gvRow.Cells[5].Text + ")";
                string temp1 = temp.Replace("&nbsp;", " ");
                string temp2 = temp1.Replace("&amp;", "&");
                this.txtCustomer.Text = temp2;
            }
            else
            {
                hfDealerId.Value = Convert.ToString(this.gvDealerDetails.DataKeys[gvRow.RowIndex].Value);
                string temp = gvRow.Cells[2].Text + "(" +
                         gvRow.Cells[3].Text + ", " +
                         gvRow.Cells[4].Text + ", " +
                         gvRow.Cells[6].Text + "-" +
                         gvRow.Cells[5].Text + ")";

                string temp1 = temp.Replace("&nbsp;", " ");
                string temp2 = temp1.Replace("&amp;", "&");
                this.txtDealer.Text = temp2;
            }


            this.gvDealerDetails.DataBind();
        }
        #endregion

        #region[Check permission]
        private void CheckPagePermission()
        {
            List<String> Rights = CommonBAL.GetPagePermission(Constant.UserId, "INVENTORY");

            if (!(Rights.Contains("INVENTORY.VIEW") || Rights.Contains("INVENTORY.EDIT") || Rights.Contains("INVENTORY.ADD")))
                Response.Redirect("Permission.aspx?MSG=INVENTORY.VIEW or INVENTORY.EDIT or INVENTORY.ADD");
        }
        #endregion

        #region[Sort dropdown]
        private void BindSort1DDL()
        {
            ddlSort1.DataSource = InventoryBAL.InventoryList_SortOptions("SELECT * FROM LookUp_Sort WHERE TableId = 3 order by Sequence asc, SortText asc", "", "");
            ddlSort1.DataTextField = "SortText";
            ddlSort1.DataValueField = "SortValue";
            ddlSort1.DataBind();

            ddlSort1.Items.Insert(0, new ListItem("", "-1"));
            if (ddlSort1.Items.FindByValue("Inventory.DateAdded") != null)
                ddlSort1.SelectedValue = "Inventory.DateAdded";
            if (Convert.ToString(Session["LoginEntityTypeID"]) != "1")
                ddlSort1.Items.Remove(ddlSort1.Items.FindByText("CheckNumber"));
            BindSort2DDL();
            BindSort3DDL();
        }

        private void BindSort2DDL()
        {
            ddlSort2.DataSource = InventoryBAL.InventoryList_SortOptions(String.Format("SELECT * FROM LookUp_Sort WHERE TableId = 3 AND SortValue NOT IN ('{0}') order by Sequence asc, SortText asc", ddlSort1.SelectedValue), "", "");
            ddlSort2.DataTextField = "SortText";
            ddlSort2.DataValueField = "SortValue";
            ddlSort2.DataBind();
            ddlSort2.Items.Insert(0, new ListItem("", "-1"));
            if (Convert.ToString(Session["LoginEntityTypeID"]) != "1")
                ddlSort2.Items.Remove(ddlSort2.Items.FindByText("CheckNumber"));
            BindSort3DDL();
        }

        private void BindSort3DDL()
        {
            ddlSort3.DataSource = InventoryBAL.InventoryList_SortOptions(String.Format("SELECT * FROM LookUp_Sort WHERE TableId =3 AND SortValue NOT IN ('{0}', '{1}') order by Sequence asc, SortText asc", ddlSort1.SelectedValue, ddlSort2.SelectedValue), "", "");
            ddlSort3.DataTextField = "SortText";
            ddlSort3.DataValueField = "SortValue";
            ddlSort3.DataBind();
            ddlSort3.Items.Insert(0, new ListItem("", "-1"));
            if (Convert.ToString(Session["LoginEntityTypeID"]) != "1")
                ddlSort3.Items.Remove(ddlSort3.Items.FindByText("CheckNumber"));
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

        #region[Drop down paging]
        protected void odsInventory_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue == null) return;
            if (e.ReturnValue.GetType().Name == "Int32")
            {

                Int32 count = Convert.ToInt32(e.ReturnValue);
                Int32 pagesize = gvInventoryList.PageSize;
                Int32 pagecount = count / pagesize;
                if (count < pagesize)
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} of {0} records", count);
                }
                else
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} through {1} of {2} records"
                         , String.Format("{0:#,###}", pagesize * gvInventoryList.PageIndex + 1)
                         , String.Format("{0:#,###}", pagesize * (gvInventoryList.PageIndex + 1) > count ? count : pagesize * (gvInventoryList.PageIndex + 1))
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

                    ddlPaging.SelectedValue = String.Format("{0}", gvInventoryList.PageIndex + 1);
                    ddlPaging1.SelectedValue = String.Format("{0}", gvInventoryList.PageIndex + 1);
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
            gvInventoryList.PageIndex = 0;
            BindGrid();
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedIndex > 0)
                gvInventoryList.PageIndex--;
            BindGrid();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedValue != ddlPaging.Items.Count.ToString())
                gvInventoryList.PageIndex++;
            BindGrid();
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            gvInventoryList.PageIndex = ddlPaging.Items.Count - 1;
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

            gvInventoryList.PageIndex = 0;
            BindGrid();
        }
        #endregion

        #region[Drop down page selection change]
        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                gvInventoryList.PageIndex = Convert.ToInt32(((DropDownList)sender).SelectedValue) - 1;
            BindGrid();
            EnablePaging();
            WriteCookies();
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

        protected void gvInventoryList_Sorting(object sender, GridViewSortEventArgs e)
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

        #region[Search button event handler]
        protected void btnSearchInventory_Click(object sender, EventArgs e)
        {
            gvInventoryList.PageIndex = 0;
            BindGrid();
            //dvCollapse.Style.Add("display", "none");
            //dvExpand.Style.Add("display", "block");
            //dvSearch.Style.Add("display", "none");
        }
        #endregion

        #region[Dealer/Customer popup paging]
        protected void gvDealerDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvSearch.Style.Add("display", "block");
            this.mpeOpenDealerSelector.Show();
        }
        #endregion

        #region[Save searches in cookie]
        private void WriteCookies()
        {
            HttpCookie cookie = new HttpCookie("InvSearchHistory");
            cookie.Values["Make"] = ddlMake.SelectedValue;
            cookie.Values["Model"] = ddlModel.SelectedValue;
            cookie.Values["Body"] = ddlBody.SelectedValue;
            cookie.Values["FYear"] = ddlFromYear.SelectedValue;
            cookie.Values["TYear"] = ddlToYear.SelectedValue;
            cookie.Values["VINP"] = ddlVINPattern.SelectedValue;
            cookie.Values["VIN"] = txtVINNo.Text.Trim();
            cookie.Values["Chk"] = txtCheckNo.Text.Trim();
            cookie.Values["Cust"] = txtCustomer.Text.Trim();
            cookie.Values["CustID"] = hfCustomerId.Value;
            cookie.Values["Dlr"] = txtDealer.Text.Trim();
            cookie.Values["DlrID"] = hfDealerId.Value;
            cookie.Values["UCRID"] = ddlUCR.SelectedValue;
            cookie.Values["Soldfrom"] = txtSoldDateFrom.Text;
            cookie.Values["Soldto"] = txtSoldDateTo.Text;
            cookie.Values["Dsg"] = ddlDesignation.SelectedValue;
            cookie.Values["Byr"] = ddlBuyer.SelectedValue;
            cookie.Values["CmBk"] = ddlComeBack.SelectedValue;
            cookie.Values["Sold"] = ddlSold.SelectedValue;
            cookie.Values["CSts"] = ddlCarStatus.SelectedValue;
            cookie.Values["S1"] = ddlSort1.SelectedValue;
            cookie.Values["S2"] = ddlSort2.SelectedValue;
            cookie.Values["S3"] = ddlSort3.SelectedValue;
            cookie.Values["radioS1"] = rbtnSort1Direction.SelectedValue;
            cookie.Values["radioS2"] = rbtnSort2Direction.SelectedValue;
            cookie.Values["radioS3"] = rbtnSort3Direction.SelectedValue;
            cookie.Values["PNo"] = ddlPaging1.SelectedValue;
            cookie.Values["PSz"] = ddlPageSize1.SelectedValue = ddlPageSize2.SelectedValue;
            cookie.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Add(cookie);
        }
        #endregion

        #region[Read and fill search from cookie]
        private void FillFromCookies()
        {
            if (Request.Cookies["InvSearchHistory"] != null)
            {
                HttpCookie cookie = (HttpCookie)Request.Cookies["InvSearchHistory"];

                if (Convert.ToString(cookie.Values["Make"]) != "-1")
                    if (ddlMake.Items.FindByValue(Convert.ToString(cookie.Values["Make"])) != null)
                        ddlMake.SelectedValue = Convert.ToString(cookie.Values["Make"]);

                if (Convert.ToString(cookie.Values["Model"]) != "-1")
                {
                    if (ddlMake.SelectedValue != "-1")
                        BindModel(Convert.ToInt64(ddlMake.SelectedValue));
                    if (ddlModel.Items.FindByValue(Convert.ToString(cookie.Values["Model"])) != null)
                        ddlModel.SelectedValue = Convert.ToString(cookie.Values["Model"]);
                }
                if (Convert.ToString(cookie.Values["Body"]) != "-1")
                {
                    if (ddlModel.SelectedValue != "-1")
                        BindBody(Convert.ToInt32(ddlModel.SelectedValue));
                    if (ddlBody.Items.FindByValue(Convert.ToString(cookie.Values["Body"])) != null)
                        ddlBody.SelectedValue = Convert.ToString(cookie.Values["Body"]);
                }

                if (Convert.ToString(cookie.Values["FYear"]) != "-1")
                    if (ddlFromYear.Items.FindByValue(Convert.ToString(cookie.Values["FYear"])) != null)
                        ddlFromYear.SelectedValue = Convert.ToString(cookie.Values["FYear"]);

                if (Convert.ToString(cookie.Values["TYear"]) != "-1")
                    if (ddlToYear.Items.FindByValue(Convert.ToString(cookie.Values["TYear"])) != null)
                        ddlToYear.SelectedValue = Convert.ToString(cookie.Values["TYear"]);


                if (Convert.ToString(cookie.Values["VINP"]) != "0")
                    if (ddlVINPattern.Items.FindByValue(Convert.ToString(cookie.Values["VINP"])) != null)
                        ddlVINPattern.SelectedValue = Convert.ToString(cookie.Values["VINP"]);

                txtVINNo.Text = Convert.ToString(cookie.Values["VIN"]);
                txtCheckNo.Text = Convert.ToString(cookie.Values["Chk"]);
                txtCustomer.Text = Convert.ToString(cookie.Values["Cust"]);
                txtDealer.Text = Convert.ToString(cookie.Values["Dlr"]);

                //if (txtCustomer.Text.ToString() != null)                             
                //{
                hfCustomerId.Value = Convert.ToString(cookie.Values["CustID"]);
                // }

                //if (txtDealer.Text.ToString() != null)
                //{
                hfDealerId.Value = Convert.ToString(cookie.Values["DlrID"]);
                // }

                if (Convert.ToString(cookie.Values["UCRID"]) != "-1")
                    if (ddlUCR.Items.FindByValue(Convert.ToString(cookie.Values["UCRID"])) != null)
                        ddlUCR.SelectedValue = Convert.ToString(cookie.Values["UCRID"]);

                txtSoldDateFrom.Text = Convert.ToString(cookie.Values["Soldfrom"]);
                txtSoldDateTo.Text = Convert.ToString(cookie.Values["Soldto"]);


                if (Convert.ToString(cookie.Values["Dsg"]) != "-1")
                    if (ddlDesignation.Items.FindByValue(Convert.ToString(cookie.Values["Dsg"])) != null)
                        ddlDesignation.SelectedValue = Convert.ToString(cookie.Values["Dsg"]);

                if (Convert.ToString(cookie.Values["Byr"]) != "-1")
                    if (ddlBuyer.Items.FindByValue(Convert.ToString(cookie.Values["Byr"])) != null)
                        ddlBuyer.SelectedValue = Convert.ToString(cookie.Values["Byr"]);

                if (Convert.ToString(cookie.Values["CmBk"]) != "-1")
                    if (ddlComeBack.Items.FindByValue(Convert.ToString(cookie.Values["CmBk"])) != null)
                        ddlComeBack.SelectedValue = Convert.ToString(cookie.Values["CmBk"]);

                if (Convert.ToString(cookie.Values["Sold"]) != "-1")
                    if (ddlSold.Items.FindByValue(Convert.ToString(cookie.Values["Sold"])) != null)
                        ddlSold.SelectedValue = Convert.ToString(cookie.Values["Sold"]);

                if (Convert.ToString(cookie.Values["CSts"]) != "-1")
                    if (ddlCarStatus.Items.FindByValue(Convert.ToString(cookie.Values["CSts"])) != null)
                        ddlCarStatus.SelectedValue = Convert.ToString(cookie.Values["CSts"]);

                if (Convert.ToString(cookie.Values["S1"]) != "-1")
                    if (ddlSort1.Items.FindByValue(Convert.ToString(cookie.Values["S1"])) != null)
                    {
                        ddlSort1.SelectedValue = Convert.ToString(cookie.Values["S1"]);
                        BindSort2DDL();
                    }
                if (Convert.ToString(cookie.Values["S2"]) != "-1")
                    if (ddlSort2.Items.FindByValue(Convert.ToString(cookie.Values["S2"])) != null)
                    {
                        ddlSort2.SelectedValue = Convert.ToString(cookie.Values["S2"]);

                        BindSort3DDL();
                    }

                if (Convert.ToString(cookie.Values["S3"]) != "-1")
                    if (ddlSort3.Items.FindByValue(Convert.ToString(cookie.Values["S3"])) != null)
                        ddlSort3.SelectedValue = Convert.ToString(cookie.Values["S3"]);

                if (ddlSort1.SelectedValue.ToString() != "-1")
                    if (Convert.ToString(cookie.Values["radioS1"]) != null)
                        rbtnSort1Direction.SelectedValue = cookie.Values["radioS1"];

                if (ddlSort2.SelectedValue.ToString() != "-1")
                    if (Convert.ToString(cookie.Values["radioS2"]) != null)
                        rbtnSort2Direction.SelectedValue = cookie.Values["radioS2"];

                if (ddlSort3.SelectedValue.ToString() != "-1")
                    if (Convert.ToString(cookie.Values["radioS3"]) != null)
                        rbtnSort3Direction.SelectedValue = cookie.Values["radioS3"];

                if (Convert.ToString(cookie.Values["PNo"]) != "-1")
                    if (ddlPaging1.Items.FindByValue(Convert.ToString(cookie.Values["PNo"])) != null)
                        ddlPaging1.SelectedValue = Convert.ToString(cookie.Values["PNo"]);

                if (Convert.ToString(cookie.Values["PSz"]) != "-1")
                    if (ddlPageSize1.Items.FindByValue(Convert.ToString(cookie.Values["PSz"])) != null)
                        ddlPageSize1.SelectedValue = ddlPageSize2.SelectedValue = Convert.ToString(cookie.Values["PSz"]);

            }
        }
        #endregion

        #region[Inventory grid RowDataBound event]
        protected void gvInventoryList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (Convert.ToString(Session["LoginEntityTypeID"]) == "1")
                {
                    gvInventoryList.Columns[15].Visible = false;
                    gvInventoryList.Columns[16].Visible = true;
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton ibtnCR = (ImageButton)e.Row.FindControl("ibtnCR");
                Image imggCR = (Image)e.Row.FindControl("imggCR");
                //HiddenField hfCRStatus = (HiddenField)e.Row.FindControl("hfCRStatus");
                ImageButton imgCar = (ImageButton)e.Row.FindControl("ibtncars");
                Label lblImageCount = (Label)e.Row.FindControl("lblImageCount");
                Label lblCRStatus = (Label)e.Row.FindControl("lblCRStatus");//
                HyperLink ancrCR = (HyperLink)e.Row.FindControl("ancrCR");
                HiddenField hdnInvID = (HiddenField)e.Row.FindControl("hdnInventoryId");


                //HtmlAnchor ibtnCRAvailable = (HtmlAnchor)e.Row.FindControl("ibtnCRAvailable");
                //HiddenField hdnInventoryId = (HiddenField)e.Row.FindControl("hdnInventoryId");

                if (String.IsNullOrEmpty(lblCRStatus.Text) || lblCRStatus.Text == "0")
                {
                    ibtnCR.ImageUrl = "~/Images/ucr-btn1.png";
                    ibtnCR.ToolTip = "Not Ready";
                    if (Convert.ToString(Session["LoginEntityTypeID"]) == "2" || Convert.ToString(Session["LoginEntityTypeID"]) == "1")
                        ibtnCR.Enabled = false;

                }
                else if (lblCRStatus.Text == "10" || lblCRStatus.Text == "20")
                {
                    ibtnCR.ImageUrl = "~/Images/ucr-btn2.png";
                    ibtnCR.ToolTip = "Initiated";
                    imggCR.ImageUrl = "~/Images/ucr-btn2.png";
                    imggCR.ToolTip = "Initiated";
                }

                // Changed by Ashar on 11 Oct, 2012 as per MOSS task #65
                else if (lblCRStatus.Text == "30")
                {
                    ibtnCR.ImageUrl = "~/Images/ucr-btn.png";
                    ibtnCR.ToolTip = "Available";
                    imggCR.ImageUrl = "~/Images/ucr-btn.png";
                    imggCR.ToolTip = "Available";
                }
                if (Convert.ToString(Session["LoginEntityTypeID"]) == "2" || Convert.ToString(Session["LoginEntityTypeID"]) == "1")
                {

                    if (lblCRStatus.Text == "10" || lblCRStatus.Text == "20" || lblCRStatus.Text == "30")
                    {
                        ibtnCR.Visible = false;
                        long InvID = Convert.ToInt32(hdnInvID.Value); //Convert.ToInt64(gvInventoryList.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
                        InventoryDetails_ByInventoryIDResult objInvDetails = InventoryBAL.GetInventoryDetailsByInventoryID(InvID);
                        ancrCR.Visible = true;
                        ancrCR.NavigateUrl = String.Format("{0}{1}", UCRlinkUrl, objInvDetails.CR_ID);
                    }
                }

                //{
                //    if (lblCRStatus.Text == "30")
                //    {
                //        ibtnCRAvailable.Visible = true;
                //        ibtnCR.Visible = false;
                //        ibtnCRAvailable.Title = "Available";
                //        if (!string.IsNullOrEmpty(hdnInventoryId.Value))
                //        {
                //            InventoryDetails_ByInventoryIDResult objInvDetails = InventoryBAL.GetInventoryDetailsByInventoryID(Convert.ToInt64(hdnInventoryId.Value));

                //            StringBuilder querystring = new StringBuilder();
                //            querystring.Append("SystemID=2&VIN=" + objInvDetails.VIN);
                //            querystring.Append(String.Format("&SourceInventoryID={0}", objInvDetails.InventoryId));
                //            querystring.Append(String.Format("&Year={0}", objInvDetails.Year));
                //            querystring.Append("&Make=" + objInvDetails.MakeName);
                //            querystring.Append("&Model=" + objInvDetails.ModelName);
                //            querystring.Append("&Body=" + objInvDetails.Body);
                //            querystring.Append(String.Format("&Mileage={0}", objInvDetails.MileageIn));
                //            querystring.Append(String.Format("&MPrice={0}", objInvDetails.CarCost));
                //            querystring.Append("&ExtColor=" + objInvDetails.ExtColor);
                //            querystring.Append("&IntColor=" + objInvDetails.IntColor);
                //            ibtnCRAvailable.HRef = (String.Format("{0}{1}", UCRlinkUrl, objInvDetails.CR_ID));
                //        }
                //    }
                //    //else
                //    //{
                //    //    ibtnCR.Visible = true;
                //    //    //ibtnCRAvailable.Visible = false;
                //    //    ibtnCR.ImageUrl = "~/Images/ucr-btn.png";
                //    //    ibtnCR.ToolTip = "Available";
                //    //}
                //}

                if (Convert.ToInt32(lblImageCount.Text) > 0)
                {
                    imgCar.Visible = true;
                    imgCar.ToolTip = String.Format("View {0} {1}", lblImageCount.Text, Convert.ToInt32(lblImageCount.Text) > 1 ? "Images" : "Image");
                }
                else
                    imgCar.Visible = false;



            }
        }
        #endregion

        #region[Add/View CR image button-Old Code]
        /*
        protected void ibtnCR_Click_Old(object sender, ImageClickEventArgs e)
        {
            long InvID = Convert.ToInt64(gvInventoryList.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            ImageButton ibtn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)ibtn.NamingContainer;
            HiddenField hfCRStatus = (HiddenField)row.FindControl("hfCRStatus");
            HiddenField hfVIN2 = (HiddenField)row.FindControl("hfVIN2");
            HiddenField hfCRId = (HiddenField)row.FindControl("hfCRId");

            HiddenField hfyear = (HiddenField)row.FindControl("hfyear");
            HiddenField hfmake = (HiddenField)row.FindControl("hfmake");
            HiddenField hfmodel = (HiddenField)row.FindControl("hfmodel");
            HiddenField hfbody = (HiddenField)row.FindControl("hfbody");
            HiddenField hfprice = (HiddenField)row.FindControl("hfprice");
            HiddenField hfmileage = (HiddenField)row.FindControl("hfmileage");
            HiddenField hfextcol = (HiddenField)row.FindControl("hfextcol");
            HiddenField hfintcol = (HiddenField)row.FindControl("hfintcol");
            hfvin.Value = hfVIN2.Value;
            hfcryear.Value = hfyear.Value;
            hfcrmake.Value = hfmake.Value;
            hfcrmodel.Value = hfmodel.Value;
            hfcrbody.Value = hfbody.Value;
            hfcrprice.Value = hfprice.Value;
            hfcrmileage.Value = hfmileage.Value;
            hfcrextcol.Value = hfextcol.Value;
            hfcrintcol.Value = hfintcol.Value;

            hfInventoryID.Value = Convert.ToString(InvID);
            lblucrheader.Text = String.Format("[{0} {1} {2} {3}]", hfVIN2.Value, hfyear.Value, hfmake.Value, hfmodel.Value);
            lblucrheader2.Text = String.Format("[{0} {1} {2} {3}]", hfVIN2.Value, hfyear.Value, hfmake.Value, hfmodel.Value);

            if (String.IsNullOrEmpty(hfCRStatus.Value) || hfCRStatus.Value == "0")
            {
                mpeCR.Show();
            }
            else if (hfCRStatus.Value == "10" || hfCRStatus.Value == "20")
            {
                mpeChangeCR.Show();
                hlnkViewReport.NavigateUrl = String.Format("{0}{1}", UCRlinkUrl, hfCRId.Value);
            }
            else if (hfCRStatus.Value == "30")
            {
                Response.Redirect(String.Format("{0}{1}", UCRlinkUrl, hfCRId.Value));
                //Response.Redirect(String.Format("http://web.metaoptionllc.com:82/Report/{0}", hfCRId.Value));
            }

            //CarDetailsSelectResult res = METAOPTION.DAL.InventoryDAL.GetCarDetail(InvID);
            //if (res != null)
            //    ExtractCrInfo(res);
        }
        */
        #endregion

        #region[Add/View CR image button-Old Code]
        protected void ibtnCR_Click(object sender, ImageClickEventArgs e)
        {
            long InvID = Convert.ToInt64(gvInventoryList.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            ImageButton ibtn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)ibtn.NamingContainer;
            InventoryDetails_ByInventoryIDResult objInvDetails = InventoryBAL.GetInventoryDetailsByInventoryID(InvID);

            StringBuilder querystring = new StringBuilder();
            querystring.Append("SystemID=2&VIN=" + objInvDetails.VIN);
            querystring.Append(String.Format("&SourceInventoryID={0}", objInvDetails.InventoryId));
            querystring.Append(String.Format("&Year={0}", objInvDetails.Year));
            querystring.Append("&Make=" + objInvDetails.MakeName);
            querystring.Append("&Model=" + objInvDetails.ModelName);
            querystring.Append("&Body=" + objInvDetails.Body);
            querystring.Append(String.Format("&Mileage={0}", objInvDetails.MileageIn));
            querystring.Append(String.Format("&MPrice={0}", objInvDetails.CarCost));
            querystring.Append("&ExtColor=" + objInvDetails.ExtColor);
            querystring.Append("&IntColor=" + objInvDetails.IntColor);

            hfcrquerystring.Value = querystring.ToString();

            hfvin.Value = objInvDetails.VIN;

            hfInventoryID.Value = Convert.ToString(InvID);
            lblucrheader.Text = String.Format("[{0} {1} {2} {3}]", objInvDetails.VIN, objInvDetails.Year, objInvDetails.MakeName, objInvDetails.ModelName);
            lblucrheader2.Text = String.Format("[{0} {1} {2} {3}]", objInvDetails.VIN, objInvDetails.Year, objInvDetails.MakeName, objInvDetails.ModelName);

            if (String.IsNullOrEmpty(objInvDetails.CR_Status.ToString()) || objInvDetails.CR_Status == 0)
            {
                mpeCR.Show();
            }

            // Changed by Ashar on 11 Oct, 2012 as per MOSS task #65
            else if (objInvDetails.CR_Status == 10 || objInvDetails.CR_Status == 20 || objInvDetails.CR_Status.Value == 30)
            {
                mpeChangeCR.Show();
                hlnkViewReport.NavigateUrl = String.Format("{0}{1}", UCRlinkUrl, objInvDetails.CR_ID);
            }
            //else if (objInvDetails.CR_Status.Value == 30)
            //{
            //    //ibtn.OnClientClick = "aspnetForm.target ='_blank';";
            //    //string str = String.Format("{0}{1}", UCRlinkUrl, objInvDetails.CR_ID);
            //    //Response.Write("<script  type='text/javascript'>");
            //    //Response.Write("window.open(Test.aspx?id=174563 ,_blank)");
            //    //Response.Write("</script>");
            //    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open(" + UCRlinkUrl + "," + objInvDetails.CR_ID + ");", true);
            //    //Response.Redirect(String.Format("{0}{1}", UCRlinkUrl, objInvDetails.CR_ID));
            //    //Response.Redirect(String.Format("http://web.metaoptionllc.com:82/Report/{0}", hfCRId.Value));
            //}
        }
        #endregion

        #region[Add/Link CR - show hide link option]
        protected void rbtnListCR_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpeCR.Show();
        }
        #endregion

        #region[Add/Link CR - ID/URL]
        protected void rbtnCRIdUrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpeCR.Show();
        }
        #endregion

        #region[Close CR popup]
        protected void btnCRcancel_Click(object sender, EventArgs e)
        {
            mpeCR.Hide();
        }
        #endregion

        #region[Link CR by CRID]
        protected void btncridvalidate_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=1&value={0}&invtype=I&id={1}", txtCRIdUrl.Text, hfInventoryID.Value);
            mpeCR.Hide();
            mpucr.Show();

        }

        protected void btncridvalidate2_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=1&value={0}&invtype=I&id={1}", txtCRIdUrl2.Text, hfInventoryID.Value);
            mpeCR.Hide();
            mpucr.Show();

        }
        #endregion

        #region[Link CR by CRURL]
        protected void btncrurlvalidate_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=2&value={0}&invtype=I&id={1}", txtCRIdUrl.Text, hfInventoryID.Value);
            mpeCR.Hide();
            mpucr.Show();

        }

        protected void btncrurlvalidate2_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=2&value={0}&invtype=I&id={1}", txtCRIdUrl2.Text, hfInventoryID.Value);
            mpeCR.Hide();
            mpucr.Show();

        }
        #endregion

        #region[Link CR by VIN]
        protected void btncrsearch_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=3&value={0}&invtype=I&id={1}", txtCRIdUrl.Text, hfInventoryID.Value);
            mpeCR.Hide();
            mpucr.Show();

        }

        protected void btncrsearch2_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=3&value={0}&invtype=I&id={1}", txtCRIdUrl2.Text, hfInventoryID.Value);
            mpeCR.Hide();
            mpucr.Show();

        }
        #endregion

        protected void btnucrcancel_Click(object sender, EventArgs e)
        {
            mpucr.Hide();
        }

        protected void btnCRok_Click(object sender, EventArgs e)
        {

        }

        protected void btnCRok2_Click(object sender, EventArgs e)
        {
            long PreInvID = 0, InventoryID = 0;

            InventoryID = Convert.ToInt64(hfInventoryID.Value);
            PreInventoryBAL PreInvBAL = new PreInventoryBAL();
            PreInvBAL.RemoveCR(PreInvID, InventoryID, Convert.ToInt64(Session["empId"]));
            mpeChangeCR.Hide();
            BindGrid();
        }

        protected void ibtncars_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibtncars = (ImageButton)sender;
            GridViewRow row = (GridViewRow)ibtncars.NamingContainer;

            HiddenField hdnVIN = row.FindControl("hdnVIN") as HiddenField;

            long InvID = Convert.ToInt64(gvInventoryList.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            PreInventoryBAL PreInvBAL = new PreInventoryBAL();
            long PreInvID = PreInvBAL.PreInv_ByInvID(InvID);
            ifrmSlideShow.Attributes.Add("src", String.Format("ViewAllImages.aspx?id={0}&v={1}&preid={2}", InvID, hdnVIN.Value, PreInvID));  //ImageGallery.aspx
            //Session["PreInvID"] = PreInvID;
            //mpoImageSlidshow.Show();
            ModalPopupExtender1.Show();
        }

        #region [Added by Rupendra 15 Nov 2012 for Export data in Excel]
        protected void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dtExportData = new DataTable();
            string ExportCount = System.Configuration.ConfigurationManager.AppSettings["ExportDataCount"];
            Int32 rowCount = ExportDataCount();
            if (rowCount > Convert.ToInt32(ExportCount))
            {
                string fileName = string.Empty;
                fileName = "HVMS_InventoryReport_" + DateTime.Now.ToString("MMddyyyy_HHmmss");
                lblHeader.Text = fileName + ".xls";
                ModalPopupExtenderExport.Show();
            }
            else
            {
                dtExportData = ExportData();
                ExportToExcel(dtExportData, 1);
            }
        }


        public void ExportToExcel(DataTable dtExport, int type)
        {
            string attachment = string.Empty;
            string fileName = string.Empty;
            fileName = "HVMS_InventoryReport_" + DateTime.Now.ToString("MMddyyyy_HHmmss");
            attachment = "attachment; filename=" + fileName + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";

            Table mainTable = new Table();
            TableRow trExport;
            TableCell cellExport;

            #region "Header Region"
            trExport = new TableRow();
            ///////////////////////////Header Design///////////////////////////////
            for (int col = 0; col < 22; col++)
            {
                cellExport = new TableCell();
                if (col == 0)
                {
                    cellExport.Text = "Purchased Date";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }
                if (col == 1)
                {
                    cellExport.Text = "Year";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }
                else if (col == 2)
                {
                    cellExport.Text = "Make";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }
                else if (col == 3)
                {
                    cellExport.Text = "Model";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }
                else if (col == 4)
                {
                    cellExport.Text = "Body";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }
                else if (col == 5)
                {
                    cellExport.Text = "V";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }
                else if (col == 6)
                {
                    cellExport.Text = "Arrival Date";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }

                else if (col == 7)
                {
                    cellExport.Text = "T";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }

                else if (col == 8)
                {
                    cellExport.Text = "Car Cost($)";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }

                else if (col == 9)
                {
                    cellExport.Text = "Mileage In";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }

                else if (col == 10)
                {
                    cellExport.Text = "CB";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }

                else if (col == 11)
                {
                    cellExport.Text = "Ext/Int Color";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }

                else if (col == 12)
                {
                    cellExport.Text = "Buyer";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }

                else if (col == 13)
                {
                    cellExport.Text = "Purchased From";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }

                else if (col == 14 && Convert.ToString(Convert.ToString(Session["LoginEntityTypeID"])) != "1")
                {
                    cellExport.Text = "Price Sold";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }

                else if (col == 15 && Convert.ToString(Convert.ToString(Session["LoginEntityTypeID"])) != "1")
                {
                    cellExport.Text = "Date Sold";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }

                else if (col == 16 && Convert.ToString(Convert.ToString(Session["LoginEntityTypeID"])) != "1")
                {
                    cellExport.Text = "Sold";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }

                else if (col == 17)
                {
                    cellExport.Text = "Notes";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }

                else if (col == 18)
                {
                    cellExport.Text = "Special Case Notes";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }

                else if (col == 19)
                {
                    cellExport.Text = "Title Tracking Notes";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }

                else if (col == 20)
                {
                    cellExport.Text = "Sold To";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }

                else if (col == 21)
                {
                    cellExport.Text = "State";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }

                cellExport.BorderWidth = 1;
                //cellExport.Style.Add("font-weight", "bold");
                cellExport.Style.Add("color", "White");
                cellExport.Style.Add("margin-bottom", "2px");
                cellExport.Style.Add("font-size", "11pt");
                cellExport.Style.Add("text-align", "Center");
                cellExport.Style.Add("border-color", "Maroon");
                //cellExport.BorderWidth = Unit.Pixel(2);
                // cellExport.ColumnSpan = 4;
                if ((col == 15 || col == 14 || col == 16) && Convert.ToString(Convert.ToString(Session["LoginEntityTypeID"])) == "1")
                {
                }
                else
                    trExport.Cells.Add(cellExport);
                mainTable.Rows.Add(trExport);

            }
            //***************************Header End********************************************
            #endregion

            #region"Data Region"

            Int32 dtCount = 0;
            if (type == 2)
                dtCount = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ExportDataCount"]);
            else
                dtCount = dtExport.Rows.Count;
            //***************************Rows********************************************
            for (int cr = 0; cr < dtCount; cr++)
            {
                trExport = new TableRow();
                for (Int16 cl = 0; cl < dtExport.Columns.Count - 1; cl++)
                {
                    cellExport = new TableCell();
                    if (cl == 0)
                    {
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["PurchaseOn"]);
                        cellExport.Style.Add("text-align", "left");
                    }
                    else if (cl == 1)
                    {
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["Year"]);
                    }
                    else if (cl == 2)
                    {
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["MakeName"]);
                        cellExport.Style.Add("text-align", "left");
                    }
                    else if (cl == 3)
                    {
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["ModelName"]);
                        cellExport.Style.Add("text-align", "left");
                    }
                    else if (cl == 4)
                    {
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["Body"]);
                        cellExport.Style.Add("text-align", "left");
                    }
                    else if (cl == 5)
                    {
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["VehiclePresent"]);
                        cellExport.Style.Add("text-align", "left");
                    }
                    else if (cl == 6)
                    {
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["Arrival"]);
                        cellExport.Style.Add("text-align", "left");
                    }
                    else if (cl == 7)
                    {
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["TitlePresent"]);
                        cellExport.Style.Add("text-align", "left");
                    }
                    else if (cl == 8)
                    {
                        cellExport.Style.Add("text-align", "right");
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["CarCost"]);
                    }
                    else if (cl == 9)
                    {
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["MileageIn"]);
                        cellExport.Style.Add("text-align", "right");
                    }
                    else if (cl == 10)
                    {
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["ComeBackYes"]);
                        cellExport.Style.Add("text-align", "left");
                    }
                    else if (cl == 11)
                    {
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["ExtColor"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(dtExport.Rows[cr]["IntColor"])))
                            cellExport.Text += "/ " + Convert.ToString(dtExport.Rows[cr]["IntColor"]);
                        cellExport.Style.Add("text-align", "left");
                    }
                    else if (cl == 12)
                    {
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["Buyer"]);
                        cellExport.Style.Add("text-align", "left");
                    }
                    else if (cl == 13)
                    {
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["DealerName"]);
                        cellExport.Style.Add("text-align", "left");
                    }
                    else if (cl == 14 && Convert.ToString(Convert.ToString(Session["LoginEntityTypeID"])) != "1")
                    {
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["SoldPrice"]);
                        cellExport.Style.Add("text-align", "right");
                    }

                    else if (cl == 15 && Convert.ToString(Convert.ToString(Session["LoginEntityTypeID"])) != "1")
                    {
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["SoldDate"]);
                        cellExport.Style.Add("text-align", "left");
                    }

                    else if (cl == 16 && Convert.ToString(Convert.ToString(Session["LoginEntityTypeID"])) != "1")
                    {
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["SoldStatus"]);
                        cellExport.Style.Add("text-align", "left");
                    }

                    else if (cl == 17)
                    {
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["Notes"]);
                        cellExport.Style.Add("text-align", "left");
                    }

                    else if (cl == 18)
                    {
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["SpecialCaseNotes"]);
                        cellExport.Style.Add("text-align", "left");
                    }

                    else if (cl == 19)
                    {
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["TitleTrackingNotes"]);
                        cellExport.Style.Add("text-align", "left");
                    }

                    else if (cl == 20)
                    {
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["SoldToName"]);
                        cellExport.Style.Add("text-align", "left");
                    }

                    else if (cl == 21)
                    {
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["SoldToState"]);
                        cellExport.Style.Add("text-align", "left");
                    }

                    cellExport.BorderWidth = 1;
                    cellExport.Style.Add("font-size", "10pt");
                    //cellExport.Style.Add("font-weight", "bold");
                    // cellExport.Style.Add("font-weight", "bold");
                    // cellExport.Style.Add("text-align", "left");
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0FFFF");
                    cellExport.Style.Add("vertical-align", "top");
                    if ((cl == 15 || cl == 14 || cl == 16) && Convert.ToString(Convert.ToString(Session["LoginEntityTypeID"])) == "1")
                    {
                    }
                    else
                        trExport.Cells.Add(cellExport);

                }
                mainTable.Rows.Add(trExport);


            }
            #endregion

            //trExport = new TableRow();
            //cellExport = new TableCell();
            //cellExport.Text = "";
            //cellExport.BorderWidth = 1;
            //cellExport.ColumnSpan = 4;
            //cellExport.Style.Add("text-align", "left");
            //cellExport.Style.Add("vertical-align", "top");
            //cellExport.BackColor = System.Drawing.Color.White;
            //trExport.Cells.Add(cellExport);
            //mainTable.Rows.Add(trExport);

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            mainTable.RenderControl(hw);

            #region [Save Export History]
            InventoryBAL objInventoryBAL = new InventoryBAL();
            int ret = objInventoryBAL.SaveExportHistory(fileName + ".xls", dtCount, sw.ToString(), Constant.UserId);
            #endregion
            Response.Write(sw.ToString());
            Response.End();

        }

        public DataTable ExportData()
        {
            String SoldDateFrom, SoldDateTo;

            String ParentBuyerID = "-1";
            if (Convert.ToString(Session["BuyerAccessLevel"]) != "1")
                ParentBuyerID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString();

            InventoryBAL objInventoryBAL = new InventoryBAL();
            DataTable dtExport = new DataTable();
            string strFromYear = string.Empty;
            string strToYear = string.Empty;
            if (ddlFromYear.SelectedValue == "-1")
                strFromYear = "1950";
            else
                strFromYear = ddlFromYear.SelectedValue;

            if (ddlToYear.SelectedValue == "-1")
                strToYear = DateTime.Now.AddYears(1).Year.ToString();
            else
                strToYear = ddlToYear.SelectedValue;

            if (String.IsNullOrEmpty(txtSoldDateFrom.Text))
                SoldDateFrom = DateTime.Today.AddYears(-100).ToShortDateString();
            else
                SoldDateFrom = txtSoldDateFrom.Text.Replace("'", "");

            if (String.IsNullOrEmpty(txtSoldDateTo.Text))
                SoldDateTo = DateTime.Today.AddDays(1).ToShortDateString();
            else
                SoldDateTo = Convert.ToDateTime(txtSoldDateTo.Text.Replace("'", "")).ToShortDateString();


            dtExport = objInventoryBAL.SearchInventoryDataExport(Convert.ToInt32(ddlVINPattern.SelectedValue),
                                                                txtVINNo.Text.Trim(),
                                                                txtCheckNo.Text.Trim(),
                                                                Convert.ToInt32(strFromYear),
                                                                Convert.ToInt32(strToYear),
                                                                Convert.ToInt32(ddlMake.SelectedValue),
                                                                Convert.ToInt32(ddlModel.SelectedValue),
                                                                Convert.ToInt32(ddlBody.SelectedValue),
                                                                Convert.ToInt32(hfDealerId.Value),
                                                                Convert.ToInt32(hfCustomerId.Value),
                                                                Convert.ToInt32(ddlBuyer.SelectedValue),
                                                                Convert.ToInt32(ddlDesignation.SelectedValue),
                                                                Convert.ToInt32(ddlComeBack.SelectedValue),
                                                                Convert.ToInt32(ddlSold.SelectedValue),
                                                                Convert.ToInt32(ddlCarStatus.SelectedValue),
                                                                Convert.ToInt32(ddlUCR.SelectedValue),
                                                                Convert.ToInt32(Session["SystemId"]),
                                                                "Inventory.DateAdded DESC",
                                                                Convert.ToInt32(Session["LoginEntityTypeID"]),
                                                                Convert.ToInt32(Session["UserEntityID"]),
                                                                Convert.ToInt32(ParentBuyerID),
                                                                Constant.OrgID,
                                                                SoldDateFrom,
                                                                SoldDateTo);
            return dtExport;
        }

        public Int32 ExportDataCount()
        {
            String SoldDateFrom, SoldDateTo;
            String ParentBuyerID = "-1";
            if (Convert.ToString(Session["BuyerAccessLevel"]) != "1")
                ParentBuyerID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString();

            InventoryBAL objInventoryBAL = new InventoryBAL();
            Int32 rowCount = 0;
            string strFromYear = string.Empty;
            string strToYear = string.Empty;
            if (ddlFromYear.SelectedValue == "-1")
                strFromYear = "1950";
            else
                strFromYear = ddlFromYear.SelectedValue;

            if (ddlToYear.SelectedValue == "-1")
                strToYear = DateTime.Now.AddYears(1).Year.ToString();
            else
                strToYear = ddlToYear.SelectedValue;

            if (String.IsNullOrEmpty(txtSoldDateFrom.Text))
                SoldDateFrom = DateTime.Today.AddYears(-100).ToShortDateString();
            else
                SoldDateFrom = txtSoldDateFrom.Text.Replace("'", "");

            if (String.IsNullOrEmpty(txtSoldDateTo.Text))
                SoldDateTo = DateTime.Today.AddDays(1).ToShortDateString();
            else
                SoldDateTo = Convert.ToDateTime(txtSoldDateTo.Text.Replace("'", "")).ToShortDateString();

            rowCount = objInventoryBAL.SearchInventoryDataExportCount(Convert.ToInt32(ddlVINPattern.SelectedValue),
                                                                        txtVINNo.Text.Trim(),
                                                                        txtCheckNo.Text.Trim(),
                                                                        Convert.ToInt32(strFromYear),
                                                                        Convert.ToInt32(strToYear),
                                                                        Convert.ToInt32(ddlMake.SelectedValue),
                                                                        Convert.ToInt32(ddlModel.SelectedValue),
                                                                        Convert.ToInt32(ddlBody.SelectedValue),
                                                                        Convert.ToInt32(hfDealerId.Value),
                                                                        Convert.ToInt32(hfCustomerId.Value),
                                                                        Convert.ToInt32(ddlBuyer.SelectedValue),
                                                                        Convert.ToInt32(ddlDesignation.SelectedValue),
                                                                        Convert.ToInt32(ddlComeBack.SelectedValue),
                                                                        Convert.ToInt32(ddlSold.SelectedValue),
                                                                        Convert.ToInt32(ddlCarStatus.SelectedValue),
                                                                        Convert.ToInt32(ddlUCR.SelectedValue),
                                                                        Convert.ToInt32(Session["SystemId"]),
                                                                        "Inventory.DateAdded DESC",
                                                                        Convert.ToInt32(Session["LoginEntityTypeID"]),
                                                                        Convert.ToInt32(Session["UserEntityID"]),
                                                                        Convert.ToInt32(ParentBuyerID),
                                                                        Constant.OrgID,
                                                                        SoldDateFrom,
                                                                        SoldDateTo);
            return rowCount;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            DataTable dtExportData = new DataTable();
            dtExportData = ExportData();
            ExportToExcel(dtExportData, 2);
        }
        #endregion


        protected void ibtnClose_click(object sender, EventArgs e)
        {
            this.gvDealerDetails.DataBind();
        }

        #region[Reset Button to clear all search fields and search results]
        public void btn_SearchReset_Click(object sender, EventArgs e)
        {
            Response.Cookies["InvSearchHistory"].Expires = DateTime.Now.AddDays(-1d);
            Response.Redirect("../UI/InventorySearch.aspx");

        }
        #endregion

    }
}
