using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using METAOPTION.BAL;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Security;
using System.Globalization;


namespace METAOPTION.UI
{
    public partial class ViewAllExpenses : System.Web.UI.Page
    {
        DataSet ds = new DataSet();
        const string EXPENSETYPEID_CARCOST = "50";
        const string EXPENSETYPEID_COMMISSION = "12";
        const string ENTITY_TYPEID_VENDOR = "3";
        const string ENTITY_TYPEID_BUYER = "2";
        const string ENTITY_TYPEID_DEALER = "1";
        long Code = -1;
        const string blankSpace = "&nbsp;";
        ExpenseBAL ExpBAL = new ExpenseBAL();

        DataTable dtExpenseType = new DataTable();
        DataTable dtDisplayName = new DataTable();
        DataTable dtEntityName = new DataTable();

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
            txtTest_AutoCompleteExtender.ContextKey = Convert.ToString(Constant.OrgID);

            if (!Page.IsPostBack)
            {
                BindExpenseEntityName();
                BindSort1DDL();
                SetFromDate();
                BindGrid();
                #region [Added by Rupendra 21 Dec 12 Vendor, Dealer and Buyer login details]
                if ((Convert.ToString(Session["LoginEntityTypeID"]) == "1") || (Convert.ToString(Session["LoginEntityTypeID"]) == "2") || (Convert.ToString(Session["LoginEntityTypeID"]) == "3"))
                {
                    lnkAddNewExpense.Visible = false;
                    btnAddInvoice.Visible = false;
                    gvAllExpenseList.Columns[0].Visible = false;
                    gvAllExpenseList.Columns[11].HeaderStyle.Width = Unit.Pixel(20);
                    gvAllExpenseList.Columns[11].ItemStyle.Width = Unit.Pixel(20);
                }
                #endregion
            }
        }

        public void SetFromDate()
        {
            string fromdate = System.Configuration.ConfigurationManager.AppSettings["NumberOfDays"];
            if (!string.IsNullOrEmpty(fromdate))
            {
                txtSyncDateFrom.Text = DateTime.Now.AddDays(-Convert.ToInt32(fromdate)).ToString("MM/dd/yyyy");
                txtSyncDateTo.Text = DateTime.Now.ToString("MM/dd/yyyy");
            }
            else
            { txtSyncDateFrom.Text = ""; }
        }

        #region[Bind Expense Type, Entity Type, Added By dropdown]
        public void BindExpenseEntityName()
        {
            try
            {
                DataTable dtEntyties = new DataTable();
                if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
                {
                    dlEntityType.Enabled = false;
                    ds = ExpBAL.GetEntityNameExpenseAddedBy_Ver211(Convert.ToInt32(Session["LoginEntityTypeID"]), Convert.ToInt32(Constant.UserId));


                    if ((!string.IsNullOrEmpty(Convert.ToString(Session["BuyerIsDirect"]))) && (Convert.ToString(Session["BuyerIsDirect"]).ToLower() == "true"))
                    {
                        dtEntyties = ExpBAL.GetDirectBuyerEntities_Ver211(Convert.ToInt32(Session["UserEntityID"].ToString()));
                        dlEntities.Enabled = true;
                    }
                    else if ((!string.IsNullOrEmpty(Session["BuyerAccessLevel"].ToString())) && (Convert.ToString(Session["BuyerAccessLevel"]) == "2"))
                    {
                        Int32 BuyerParentID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? -1 : Convert.ToInt32(Session["BuyerParent"]);
                        dtEntyties = ExpBAL.ChildBuyerEntities_Ver211(Convert.ToInt32(Session["UserEntityID"]), BuyerParentID);
                        dlEntities.Enabled = true;
                    }
                    else
                    {
                        Int32 BuyerParentID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? -1 : Convert.ToInt32(Session["BuyerParent"]);
                        dtEntyties = ExpBAL.GetEntities_Ver211(Convert.ToInt32(Session["LoginEntityTypeID"]), Convert.ToInt32(Session["UserEntityID"]), BuyerParentID);

                    }
                    dlEntities.DataSource = dtEntyties;
                    dlEntities.DataTextField = "DisplayName";
                    dlEntities.DataValueField = "EntityId";
                    dlEntities.DataBind();
                    if (dtEntyties.Rows.Count == 1)
                    {
                        dlEntities.Items.FindByValue(Convert.ToString(dtEntyties.Rows[0]["EntityId"])).Selected = true;
                        dlEntities.Enabled = false;
                    }

                    else
                        dlEntities.Items.FindByValue(Convert.ToString(Session["UserEntityID"])).Selected = true;

                    dlEntities.Items.Insert(0, new ListItem("ALL", "-1"));

                }
                else if ((Convert.ToString(Session["LoginEntityTypeID"]) == "1") || (Convert.ToString(Session["LoginEntityTypeID"]) == "3"))
                {

                    dlEntityType.Enabled = false;
                    dlEntities.Enabled = false;
                    ds = ExpBAL.GetEntityNameExpenseAddedBy_Ver211(Convert.ToInt32(Session["LoginEntityTypeID"]), Convert.ToInt32(Constant.UserId));

                    dtEntyties = ExpBAL.GetEntities_Ver211(Convert.ToInt32(Session["LoginEntityTypeID"]), Convert.ToInt32(Session["UserEntityID"]), -1);
                    dlEntities.DataSource = dtEntyties;
                    dlEntities.DataTextField = "DisplayName";
                    dlEntities.DataValueField = "EntityId";
                    dlEntities.DataBind();
                    // dlEntities.Items.Insert(0, new ListItem("--Select--", "-1"));
                }
                else
                    ds = ExpBAL.GetEntityNameExpenseAddedBy(Constant.OrgID);

                ds.Tables[0].TableName = "ExpenseType";
                ds.Tables[1].TableName = "DisplayName";
                ds.Tables[2].TableName = "EntityType";

                dtExpenseType = ds.Tables["ExpenseType"];
                dtDisplayName = ds.Tables["DisplayName"];
                dtEntityName = ds.Tables["EntityType"];

                dlExpenseType.DataSource = dtExpenseType;
                dlExpenseType.DataTextField = "ExpenseType";
                dlExpenseType.DataValueField = "ExpenseTypeId";
                dlExpenseType.DataBind();
                dlExpenseType.Items.Insert(0, new ListItem("--Select--", "-1"));

                dlEntityType.DataSource = dtEntityName;
                dlEntityType.DataTextField = "EntityType";
                dlEntityType.DataValueField = "EntityTypeId";
                dlEntityType.DataBind();
                dlEntityType.Items.Insert(0, new ListItem("--Select--", "-1"));

                ddlAddedBy.DataSource = dtDisplayName;
                ddlAddedBy.DataTextField = "DisplayName";
                ddlAddedBy.DataValueField = "AddedBy";
                ddlAddedBy.DataBind();
                ddlAddedBy.Items.Insert(0, new ListItem("ALL", "-1"));

                if (Convert.ToString(Session["LoginEntityTypeID"]) == "1")
                {
                    dlEntityType.Items.FindByValue(Convert.ToString(Session["LoginEntityTypeID"])).Selected = true;
                    dlEntities.Visible = false;
                    txtDealerShip.Visible = true;
                    txtDealerShip.Enabled = false;
                    txtDealerShip.Text = Convert.ToString(Session["disName"]);
                }
                if ((Convert.ToString(Session["LoginEntityTypeID"]) == "2") || (Convert.ToString(Session["LoginEntityTypeID"]) == "3"))
                {
                    dlEntityType.Items.FindByValue(Convert.ToString(Session["LoginEntityTypeID"])).Selected = true;
                }
            }
            catch (Exception ex) { }
        }
        #endregion

        #region[Bind grid]
        protected void BindGrid()
        {
            //try
            //{
                String SyncDateFrom, SyncDateTo;
                if (String.IsNullOrEmpty(txtSyncDateFrom.Text))
                    SyncDateFrom = DateTime.Today.AddYears(-100).ToShortDateString();
                else
                    SyncDateFrom = txtSyncDateFrom.Text;

                try
                {
                    if (String.IsNullOrEmpty(txtSyncDateTo.Text))
                        SyncDateTo = DateTime.Today.AddDays(1).ToShortDateString();
                    else
                        SyncDateTo = Convert.ToDateTime(txtSyncDateTo.Text).AddDays(1).ToShortDateString();
                }
                catch
                {
                    SyncDateTo = DateTime.ParseExact(txtSyncDateTo.Text, "mm-dd-yyyy", CultureInfo.InvariantCulture).AddDays(1).ToShortDateString();
                }
                // ddlPageSize1.SelectedValue = ddlPageSize2.SelectedValue;
                gvAllExpenseList.PageSize = Convert.ToInt32(ddlPageSize1.SelectedValue);

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
                    sort = "E.ExpenseID desc";

                string strPaid = string.Empty;

                if (ddlPaid.SelectedValue == "1")
                    strPaid = "1";
                else if (ddlPaid.SelectedValue == "0")
                    strPaid = "0";
                else
                    strPaid = "";

                string invoiceNo = string.Empty;

                if (ddlInvoiceNumber.SelectedValue == "1")
                    invoiceNo = "1";
                else if (ddlInvoiceNumber.SelectedValue == "0")
                    invoiceNo = "0";
                else
                    invoiceNo = "";

                string sourceFilter = string.Empty;

                if (ddlSourceFilter.SelectedValue == "1")
                    sourceFilter = "1";
                else if (ddlSourceFilter.SelectedValue == "0")
                    sourceFilter = "0";
                else
                    sourceFilter = "";
                string strEntityId = string.Empty;
                if ((txtDealerShip.Visible == true) && (!string.IsNullOrEmpty(txtDealerShip.Text)))
                {
                    strEntityId = hdDealerId.Value;
                }
                else
                {
                    if (dlEntities.SelectedValue == "")
                        strEntityId = "-1";
                    else
                        strEntityId = dlEntities.SelectedValue;
                }

                if (Convert.ToString(Session["LoginEntityTypeID"]) == "1")
                {
                    strEntityId = Convert.ToString(Session["UserEntityID"]);
                }
                ObjectDataSource odsExpense = new ObjectDataSource();
                odsExpense.Selected += new ObjectDataSourceStatusEventHandler(odsExpense_Selected);
                //#region [Modified by Rupendra 26 Dec 12, Vendor login details]
                //if ((Convert.ToString(Session["LoginEntityTypeID"]) == "2") || (Convert.ToString(Session["LoginEntityTypeID"]) == "3"))
                //{
                odsExpense.TypeName = "METAOPTION.ExpenseBAL";
                odsExpense.SelectMethod = "SearchExpense_Ver211";
                odsExpense.SelectCountMethod = "SearchExpenseCount_Ver211";
                odsExpense.EnablePaging = true;
                odsExpense.SelectParameters.Add("VIN", String.IsNullOrEmpty(txtVINNumber.Text.Trim()) ? String.Empty : txtVINNumber.Text.Trim());
                odsExpense.SelectParameters.Add("CheckNo", String.IsNullOrEmpty(txtCheck.Text.Trim()) ? String.Empty : txtCheck.Text.Trim());
                odsExpense.SelectParameters.Add("CheckPaid", strPaid.Trim());
                odsExpense.SelectParameters.Add("AddedBy", DbType.Int32, ddlAddedBy.SelectedValue);
                odsExpense.SelectParameters.Add("EntityId", DbType.Int32, strEntityId);
                odsExpense.SelectParameters.Add("EntityTypeId", DbType.Int32, dlEntityType.SelectedValue);
                odsExpense.SelectParameters.Add("ExpenseTypeId", DbType.Int32, dlExpenseType.SelectedValue);
                odsExpense.SelectParameters.Add("SyncFrom", SyncDateFrom);
                odsExpense.SelectParameters.Add("SyncTo", SyncDateTo);
                odsExpense.SelectParameters.Add("InvoiceNumber", DbType.String, invoiceNo);
                odsExpense.SelectParameters.Add("SourceFilter", DbType.String, sourceFilter);
                odsExpense.SelectParameters.Add("LoginEntityTypeID", DbType.Int32, Session["LoginEntityTypeID"].ToString());
                odsExpense.SelectParameters.Add("UserEntityID", DbType.Int32, Session["UserEntityID"].ToString());
                odsExpense.SelectParameters.Add("BuyerParentID", DbType.Int32, String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString());
                odsExpense.SelectParameters.Add("BuyerIsDirect", DbType.String, String.IsNullOrEmpty(Convert.ToString(Session["BuyerIsDirect"])) ? "-1" : Session["BuyerIsDirect"].ToString());
                odsExpense.SelectParameters.Add("BuyerAccessLevel", DbType.String, String.IsNullOrEmpty(Convert.ToString(Session["BuyerAccessLevel"])) ? "-1" : Session["BuyerAccessLevel"].ToString());
                odsExpense.SelectParameters.Add("StartRowIndex", DbType.Int32, gvAllExpenseList.PageIndex.ToString());
                odsExpense.SelectParameters.Add("MaximumRows", DbType.Int32, ddlPageSize1.SelectedValue);
                odsExpense.SelectParameters.Add("SortExpression", sort);
                odsExpense.SelectParameters.Add("orgID", DbType.Int16, Constant.OrgID.ToString());
                gvAllExpenseList.DataSource = odsExpense;
                gvAllExpenseList.DataBind();
                //}
                //#endregion
                //else
                //{
                //    odsExpense.TypeName = "METAOPTION.ExpenseBAL";
                //    odsExpense.SelectMethod = "SearchExpense";
                //    odsExpense.SelectCountMethod = "SearchExpenseCount";
                //    odsExpense.EnablePaging = true;
                //    odsExpense.SelectParameters.Add("VIN", String.IsNullOrEmpty(txtVINNumber.Text.Trim()) ? String.Empty : txtVINNumber.Text.Trim());
                //    odsExpense.SelectParameters.Add("CheckNo", String.IsNullOrEmpty(txtCheck.Text.Trim()) ? String.Empty : txtCheck.Text.Trim());
                //    odsExpense.SelectParameters.Add("CheckPaid", strPaid.Trim());
                //    odsExpense.SelectParameters.Add("AddedBy", DbType.Int32, ddlAddedBy.SelectedValue);
                //    odsExpense.SelectParameters.Add("EntityId", DbType.Int32, strEntityId);
                //    odsExpense.SelectParameters.Add("EntityTypeId", DbType.Int32, dlEntityType.SelectedValue);
                //    odsExpense.SelectParameters.Add("ExpenseTypeId", DbType.Int32, dlExpenseType.SelectedValue);
                //    odsExpense.SelectParameters.Add("SyncFrom", SyncDateFrom);
                //    odsExpense.SelectParameters.Add("SyncTo", SyncDateTo);
                //    odsExpense.SelectParameters.Add("InvoiceNumber", DbType.String, invoiceNo);
                //    odsExpense.SelectParameters.Add("SourceFilter", DbType.String, sourceFilter);
                //    odsExpense.SelectParameters.Add("StartRowIndex", DbType.Int32, gvAllExpenseList.PageIndex.ToString());
                //    odsExpense.SelectParameters.Add("MaximumRows", DbType.Int32, ddlPageSize1.SelectedValue);
                //    odsExpense.SelectParameters.Add("SortExpression", sort);
                //    gvAllExpenseList.DataSource = odsExpense;
                //    gvAllExpenseList.DataBind();
                //}
            //}
            //catch (Exception ex) { }
        }
        #endregion

        #region[Expense grid - RowDataBound]

        decimal totalPrice = 0M;
        int totalItems = 0;
        protected void gvAllExpenseList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if ((Convert.ToString(Session["LoginEntityTypeID"]) == "1") || (Convert.ToString(Session["LoginEntityTypeID"]) == "2") || (Convert.ToString(Session["LoginEntityTypeID"]) == "3"))
                    {
                        CheckBox chkall = (CheckBox)e.Row.FindControl("chkall");
                        chkall.Visible = false;
                    }
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    totalItems += 1;
                    Label lblpaid = (Label)e.Row.FindControl("lblpaid");
                    if (lblpaid.Text == "True")
                        lblpaid.Text = "Yes";
                    else
                        lblpaid.Text = "No";
                    ImageButton ibtnPreExpDetail = (ImageButton)e.Row.FindControl("ibtnPreExpDetail");
                    HiddenField hdnPreExpID = (HiddenField)e.Row.FindControl("hdnPreExpID");
                    Label lblTotal = (Label)e.Row.FindControl("lblAmount");
                    if (!string.IsNullOrEmpty(lblTotal.Text))
                        lblTotal.Text = lblTotal.Text;
                    else
                        lblTotal.Text = "0";
                    decimal Total = Decimal.Parse(lblTotal.Text);
                    totalPrice += Total;


                    if (!string.IsNullOrEmpty(hdnPreExpID.Value))
                        ibtnPreExpDetail.Visible = true;
                    else
                        ibtnPreExpDetail.Visible = false;

                    Label lblInvoiceNumber = (Label)e.Row.FindControl("lblInvoiceNumber");
                    //if (!string.IsNullOrEmpty(lblInvoiceNumber.Text))
                    //{
                    //    lblInvoiceNumber.Visible = true;
                    //}
                    //else
                    //    lblInvoiceNumber.Visible = false;

                    HiddenField hdnComments = (HiddenField)e.Row.FindControl("hdnComments");
                    if (!string.IsNullOrEmpty(hdnComments.Value))
                        lblInvoiceNumber.Text = hdnComments.Value + "</br><i>" + lblInvoiceNumber.Text + "</i>";
                    else
                        lblInvoiceNumber.Text = "<i>" + lblInvoiceNumber.Text + "</i>";

                    ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                    HiddenField hdnExpenseTypeId = (HiddenField)e.Row.FindControl("hdnExpenseTypeId");
                    ImageButton imbEditExp = (ImageButton)e.Row.FindControl("imbEditExp");

                    if ((!string.IsNullOrEmpty(hdnExpenseTypeId.Value)) && (hdnExpenseTypeId.Value == "50"))
                        imgDelete.Visible = false;
                    else
                        imgDelete.Visible = true;

                    if ((lblpaid.Text == "Yes") && (hdnExpenseTypeId.Value == "50"))
                        imbEditExp.Visible = false;
                    else
                        imbEditExp.Visible = true;

                    HiddenField hdnDealerEntityName = (HiddenField)e.Row.FindControl("hdnDealerEntityName");
                    LinkButton lblDealer = (LinkButton)e.Row.FindControl("lblDealer");
                    lblDealer.ToolTip = hdnDealerEntityName.Value;

                    HiddenField hdnCheckDetails = (HiddenField)e.Row.FindControl("hdnCheckDetails");
                    HyperLink hylnk = (HyperLink)e.Row.FindControl("hylnk");
                    hylnk.ToolTip = hdnCheckDetails.Value;

                    HiddenField hdnVINDetails = (HiddenField)e.Row.FindControl("hdnVINDetails");
                    HyperLink hlnkVIN = (HyperLink)e.Row.FindControl("hlnkVIN");
                    string str = System.Text.RegularExpressions.Regex.Replace(hdnVINDetails.Value, @"\s+", " ").Trim();
                    hlnkVIN.ToolTip = str;


                    #region [Added by Rupendra 21 Dec 12 Vendor, Dealer and Buyer login details]
                    if ((Convert.ToString(Session["LoginEntityTypeID"]) == "1") || (Convert.ToString(Session["LoginEntityTypeID"]) == "2") || (Convert.ToString(Session["LoginEntityTypeID"]) == "3"))
                    {
                        //ImageButton imbEditExp = (ImageButton)e.Row.FindControl("imbEditExp");
                        // ImageButton imgDelete = (ImageButton)e.Row..FindControl("imgDelete");
                        LinkButton lnkExpenseDate = (LinkButton)e.Row.FindControl("lnkExpenseDate");
                        // LinkButton lblDealer = (LinkButton)e.Row.FindControl("lblDealer");
                        // HyperLink hlnkVIN = (HyperLink)e.Row.FindControl("hlnkVIN");
                        CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
                        imbEditExp.Visible = false;
                        imgDelete.Visible = false;
                        lnkExpenseDate.Enabled = false;
                        lblDealer.Enabled = false;
                        chkSelect.Visible = false;
                        hylnk.Enabled = false;
                        hlnkVIN.Enabled = false;
                    }

                    if ((Convert.ToString(Session["LoginEntityTypeID"]) == "1") || (Convert.ToString(Session["LoginEntityTypeID"]) == "2"))
                    {
                        hlnkVIN.Enabled = true;
                        hylnk.Enabled = true;
                    }

                    #endregion

                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {

                    e.Row.Cells[2].ColumnSpan = 2;
                    e.Row.Cells.RemoveAt(11);
                    Label lblTotalAmount = (Label)e.Row.FindControl("lblTotalAmount");
                    Label lblTotalCount = (Label)e.Row.FindControl("lblTotalCount");
                    lblTotalAmount.Text = "$ " + totalPrice.ToString(("#,##0"));
                    //if (!string.IsNullOrEmpty(hdnTotalrows.Value) && (totalItems == Convert.ToInt32(hdnTotalrows.Value)))
                    //    lblTotalCount.Text = "Total Count = " + hdnTotalrows.Value; //totalItems.ToString();
                    //else
                    lblTotalCount.Text = "Count = " + totalItems.ToString();

                }
            }
            catch (Exception ex) { }
        }
        #endregion

        #region [Row Command Event]
        protected void gvAllExpenseList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string EntityId = string.Empty;
                string EntityType = string.Empty;
                string[] str;
                str = Convert.ToString(e.CommandArgument.ToString()).Split(',');
                if (str.Length > 0)
                {
                    EntityType = str[0];
                    EntityId = str[1];
                    switch (EntityType)
                    {
                        case "1":
                            Response.Redirect("ViewDealer.aspx?Mode=View&EntityId=" + EntityId + "&type=1");
                            break;
                        case "2":
                            Response.Redirect("ViewBuyerDetails.aspx?Mode=View&BuyerId=" + EntityId + "&type=2");
                            break;
                        case "3":
                            Response.Redirect("ViewVendor.aspx?Mode=View&EntityId=" + EntityId + "&type=3");
                            break;
                        default:
                            break;
                    }
                }

            }
            if (e.CommandName == "SelectExpenseDate")
            {
                string strInventory = Convert.ToString(e.CommandArgument);
                Response.Redirect("InventoryExpense.aspx?Mode=View&Code=" + strInventory);
            }
        }
        #endregion

        #region [Object Datasource Selected Event]
        protected void odsExpense_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            try
            {
                if (e.ReturnValue == null) return;
                if (e.ReturnValue.GetType().Name == "Int32")
                {
                    Int32 count = Convert.ToInt32(e.ReturnValue);
                    Int32 pagesize = gvAllExpenseList.PageSize;
                    Int32 pagecount = count / pagesize;

                    if (count < pagesize)
                    {
                        hdnTotalrows.Value = Convert.ToString(count);
                        lblCount1.Text = lblCount.Text = String.Format("Showing {0} of {0} records", count);
                    }
                    else
                    {
                        hdnTotalrows.Value = Convert.ToString(pagesize);
                        lblCount1.Text = lblCount.Text = String.Format("Showing {0} through {1} of {2} records"
                             , String.Format("{0:#,###}", pagesize * gvAllExpenseList.PageIndex + 1)
                             , String.Format("{0:#,###}", pagesize * (gvAllExpenseList.PageIndex + 1) > count ? count : pagesize * (gvAllExpenseList.PageIndex + 1))
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

                        ddlPaging.SelectedValue = String.Format("{0}", gvAllExpenseList.PageIndex + 1);
                        ddlPaging1.SelectedValue = String.Format("{0}", gvAllExpenseList.PageIndex + 1);
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
            catch (Exception ex) { }
        }
        #endregion

        #region[Enable/Disable paging]
        private void EnablePaging()
        {
            try
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
            catch (Exception ex) { }
        }
        #endregion

        #region[Bind Entity Type Dropdown]
        protected void BindEntityTypeddl()
        {
            try
            {
                ddlAddedBy.DataSource = ExpBAL.GetPreExpUsers();
                ddlAddedBy.DataTextField = "DisplayName";
                ddlAddedBy.DataValueField = "SecurityUserID";
                ddlAddedBy.DataBind();
                ddlAddedBy.Items.Insert(0, new ListItem("ALL", "-1"));
            }
            catch (Exception ex) { }
        }
        #endregion

        #region[ Bind sort dropdown]
        private void BindSort1DDL()
        {
            try
            {
                ddlSort1.DataSource = ExpBAL.Expense_SortOptions("Select SortText,SortValue from Lookup_Sort where tableid=7 order by Sequence asc", "", "");
                ddlSort1.DataTextField = "SortText";
                ddlSort1.DataValueField = "SortValue";
                ddlSort1.DataBind();

                ddlSort1.Items.Insert(0, new ListItem("", "-1"));

                BindSort2DDL();
                BindSort3DDL();
            }
            catch (Exception ex) { }
        }

        private void BindSort2DDL()
        {
            try
            {
                ddlSort2.DataSource = ExpBAL.Expense_SortOptions(String.Format("Select SortText,SortValue from Lookup_Sort where tableid=7 AND SortValue NOT IN ('{0}') order by Sequence asc", ddlSort1.SelectedValue), "", "");
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
                ddlSort3.DataSource = ExpBAL.Expense_SortOptions(String.Format("Select SortText,SortValue from Lookup_Sort where tableid=7 AND SortValue NOT IN ('{0}', '{1}') order by Sequence asc", ddlSort1.SelectedValue, ddlSort2.SelectedValue), "", "");
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

        #region[Paging Click events]
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            gvAllExpenseList.PageIndex = 0;
            BindGrid();
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedIndex > 0)
                gvAllExpenseList.PageIndex--;
            BindGrid();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedValue != ddlPaging.Items.Count.ToString())
                gvAllExpenseList.PageIndex++;
            BindGrid();
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            gvAllExpenseList.PageIndex = ddlPaging.Items.Count - 1;
            BindGrid();
        }
        #endregion

        #region[Page size selection change]
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = sender as DropDownList;
                if (ddl.ID == "ddlPageSize1")
                    ddlPageSize2.SelectedValue = ddlPageSize1.SelectedValue;
                else
                    ddlPageSize1.SelectedValue = ddlPageSize2.SelectedValue;

                gvAllExpenseList.PageIndex = 0;
                BindGrid();
            }
            catch (Exception ex) { }
        }
        #endregion

        #region[Drop down page selection change]
        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (((DropDownList)sender).SelectedValue != "0")
                    gvAllExpenseList.PageIndex = Convert.ToInt32(((DropDownList)sender).SelectedValue) - 1;
                BindGrid();

                EnablePaging();
            }
            catch (Exception ex) { }
        }
        #endregion

        #region[Pre-Expense Details]
        protected void ibtnExpenseDetail_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton imgExp = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgExp.NamingContainer;
                HiddenField hfExpID = (HiddenField)row.FindControl("hdnPreExpID");
                if (!string.IsNullOrEmpty(hfExpID.Value))
                {
                    BindPreExpenseDetail(Convert.ToInt64(hfExpID.Value));
                    mpePreExpDetail.Show();
                }
            }
            catch (Exception ex) { }
        }

        protected void BindPreExpenseDetail(long ExpID)
        {
            try
            {
                //gvPreExpDetail.DataSource = ExpBAL.PreExpenseDetail_ByPreExpenseId(ExpID);
                // gvPreExpDetail.DataBind();
            }
            catch (Exception ex) { }
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

        protected void gvAllExpenseList_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
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
            catch (Exception ex) { }

        }

        private void SortGridView(string sortExpression, string direction)
        {
            try
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
            catch (Exception ex) { }
        }

        #endregion

        protected void dlEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string EntityTypeId = dlEntityType.SelectedValue;
                if (EntityTypeId == "1")
                {
                    dlEntities.Visible = false;
                    txtDealerShip.Visible = true;
                }
                else
                {
                    dlEntities.Visible = true;
                    txtDealerShip.Visible = false;
                    if (!string.IsNullOrEmpty(EntityTypeId))
                    {
                        DataTable dtEntyties = new DataTable();
                        dtEntyties = ExpBAL.GetEntities(Convert.ToInt32(EntityTypeId), Constant.OrgID);
                        dlEntities.DataSource = dtEntyties;
                        dlEntities.DataTextField = "DisplayName";
                        dlEntities.DataValueField = "EntityId";
                        dlEntities.DataBind();
                        dlEntities.Items.Insert(0, new ListItem("--Select--", "-1"));
                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            BindGrid();
        }

        #region[Add new Expenses here]
        protected void lnkAddNewExpense_Click(object sender, EventArgs e)
        {
            try
            {
                txtInvoice.Text = "";
                txtComments.Text = "";
                txtExpenseAmount.Enabled = true;
                txtExpenseDate.Enabled = true;
                trEntityName.Visible = true;
                btnAddExpense.Enabled = true;
                lblInventoryId.Text = "";
                txtInventoryId.Text = "";
                txtExpenseDate.Text = DateTime.Now.ToString("MM/dd/yy");
                MPEAddExpense.Show();

                Expense objExp = new Expense();
                lnkGridshow.Visible = true;
                //Show Data For Editing.
                this.trType.Visible = true;
                this.trVendor.Visible = true;
                this.trEntityShowname.Visible = false;
                //For Vendor Selection
                ddlEntityType.SelectedValue = "-1";
                Default_Binding();

                lblHeading.Text = "Add Expense";
                hdExpenseUpId.Value = "-1";
                txtExpenseAmount.Text = string.Empty;
                //ddlExpenseType.SelectedIndex = -1;
                ddlExpenseType.Enabled = true;
                txtComments.Text = string.Empty;
                ddlEntity.Enabled = true;


                //MPEAddExpense.Show();
                //Default_Binding();
            }
            catch (Exception ex) { }
        }
        #endregion

        protected void btnInvoiceNumberSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvAllExpenseList.Rows.Count > 0)
                {
                    int ret = 0;
                    for (int i = 0; i < gvAllExpenseList.Rows.Count; i++)
                    {
                        CheckBox chk = (CheckBox)gvAllExpenseList.Rows[i].FindControl("chkSelect");
                        HiddenField hdnExpenseId = (HiddenField)gvAllExpenseList.Rows[i].FindControl("hdnExpenseId");
                        if ((chk.Checked == true) && (!string.IsNullOrEmpty(hdnExpenseId.Value)))
                            ret = ExpBAL.UpdateInvoiceNumber(Convert.ToInt32(hdnExpenseId.Value), txtInvoiceNumber.Text);
                    }
                    mpePreExpDetail.Hide();
                    txtInvoiceNumber.Text = "";
                    BindGrid();
                }
            }
            catch (Exception ex) { }

        }

        protected void btnVINSearch_Click(object sender, EventArgs e)
        {
            DataTable dtVIN = new DataTable();
            if (!string.IsNullOrEmpty(txtVIN.Text))
                dtVIN = ExpBAL.GetVIN(txtVIN.Text, Constant.OrgID);
            grdInventoryShow.DataSource = dtVIN;
            grdInventoryShow.DataBind();
            ModalPopupExtender2.Show();
        }

        protected void lnkGridshow_Click(object sender, EventArgs e)
        {
            txtVIN.Text = "";
            grdInventoryShow.DataSource = null;
            grdInventoryShow.DataBind();
            MPEAddExpense.Hide();
            ModalPopupExtender2.Show();
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                MPEAddExpense.Show();
                lblInventoryId.Visible = true;
                foreach (GridViewRow row in grdInventoryShow.Rows)
                {
                    RadioButton rb = (RadioButton)row.FindControl("chkSelect");
                    if (rb.Checked)
                    {
                        HiddenField hdnID = (HiddenField)grdInventoryShow.Rows[row.RowIndex].FindControl("hdnInventoryId");
                        Label lblMake = (Label)grdInventoryShow.Rows[row.RowIndex].FindControl("lblMake");
                        Label lblModel = (Label)grdInventoryShow.Rows[row.RowIndex].FindControl("lblModel");
                        Label lblYear = (Label)grdInventoryShow.Rows[row.RowIndex].FindControl("lblYear");
                        Label lblMilege = (Label)grdInventoryShow.Rows[row.RowIndex].FindControl("lblMilege");
                        Label lblDateAdded = (Label)grdInventoryShow.Rows[row.RowIndex].FindControl("lblDateAdded");
                        Label lblBody = (Label)grdInventoryShow.Rows[row.RowIndex].FindControl("lblBody");
                        lblInventoryId.Text = lblYear.Text + " " + lblMake.Text + " " + lblModel.Text + " " + lblBody.Text + " " + lblMilege.Text + " " + lblDateAdded.Text + " " + "(Code: " + hdnID.Value + ")";
                        txtInventoryId.Text = lblYear.Text + " " + lblMake.Text + " " + lblModel.Text + " " + lblBody.Text + " " + lblMilege.Text + " " + lblDateAdded.Text + " " + "(Code: " + hdnID.Value + ")";
                        hdnInventory.Value = hdnID.Value;
                    }
                }
                ModalPopupExtender2.Hide();
                MPEAddExpense.Show();
            }
            catch (Exception ex) { }
        }

        protected void Default_Binding()
        {
            try
            {
                ddlEntity.Items.Clear();
                InventoryBAL objBAL = new InventoryBAL();
                ddlEntity.DataSource = objBAL.GetVendorList(Constant.OrgID);
                this.ddlEntity.DataValueField = "VendorId";
                this.ddlEntity.DataTextField = "VendorName";
                ddlEntity.DataBind();

                //Add Default Value(Blank Option)
                ListItem lstDefault = new ListItem();
                lstDefault.Text = "";
                lstDefault.Value = "-1";
                ddlEntity.Items.Insert(0, lstDefault);

                ddlExpenseType.Items.Clear();
                //Bind default Expense types
                BindExpenseType();
            }
            catch (Exception ex) { }

        }

        protected void BindExpenseType()
        {
            //Bind Expense Type Items in dropdownlist
            ddlExpenseType.DataSource = objExpenseTypes;
            ddlExpenseType.DataBind();
            if (ddlExpenseType.Items[0].Text == "--Select--")
            {
                ddlExpenseType.Items.RemoveAt(0);
            }
            ddlExpenseType.Items.Insert(0, "--Select--");
        }

        protected void ddlEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindEntities();
            MPEAddExpense.Show();
        }

        private void BindEntities()
        {
            try
            {
                InventoryBAL objBAL = new InventoryBAL();

                //Clear Expense items
                ddlExpenseType.Items.Clear();

                //Bind Expense type for vendor/Buyers
                BindExpenseType();

                //Clear Entity DropDownlist before loading buyers or vendors
                ddlEntity.Items.Clear();

                //Load Buyers in dropdownlist
                if (ddlEntityType.SelectedValue == ENTITY_TYPEID_BUYER)
                {
                    ddlEntity.DataSource = Common.GetBuyerList(Constant.OrgID);
                    this.ddlEntity.DataValueField = "BuyerId";
                    this.ddlEntity.DataTextField = "BuyerName";
                    ddlEntity.DataBind();
                }
                //Load Vendors in dropdownlist
                else if (ddlEntityType.SelectedValue == ENTITY_TYPEID_VENDOR)
                {
                    ddlEntity.DataSource = objBAL.GetVendorList(Constant.OrgID);
                    this.ddlEntity.DataValueField = "VendorId";
                    this.ddlEntity.DataTextField = "VendorName";
                    ddlEntity.DataBind();
                }

                else if (ddlEntityType.SelectedValue == ENTITY_TYPEID_DEALER)
                {

                    //Bind Dealer associated with inventory in Entity dropdownlist
                    ListItem lstInvDealerId = new ListItem();
                    if (!string.IsNullOrEmpty(hdnInventory.Value))
                    {
                        DealerDetailsSelectResult objDealerInfo = InventoryBAL.GetDealerForInv(Convert.ToInt64(hdnInventory.Value));

                        if (objDealerInfo != null && objDealerInfo.DealerId != 0)
                        {
                            lstInvDealerId.Text = objDealerInfo.DealerName;
                            lstInvDealerId.Value = objDealerInfo.DealerId.ToString();
                            ddlEntity.Items.Insert(0, lstInvDealerId);
                            //Apply Dealer Name as selected value
                            if (ddlEntity.Items.Count == 1)
                            {
                                ddlEntity.SelectedIndex = 0;
                                //Set Expense Type Options to "Pay-OFF"
                                ddlExpenseType.Items.Clear();
                                ListItem lstPayOffOption = new ListItem();
                                lstPayOffOption.Text = "PAY OFF";
                                lstPayOffOption.Value = "52";
                                ddlExpenseType.Items.Insert(0, lstPayOffOption);
                            }

                        }
                    }
                    //Add Default Value(Blank Option)
                    ListItem lstDefault = new ListItem();
                    lstDefault.Text = "";
                    lstDefault.Value = "-1";
                    ddlEntity.Items.Insert(0, lstDefault);
                }
            }
            catch (Exception ex) { }

        }

        protected void imbEditExp_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtInvoice.Text = "";
                lblHeading.Text = "";
                txtExpenseAmount.Text = "";
                txtExpenseDate.Text = "";
                txtComments.Text = "";
                lblEntityName.Text = "";

                trEntityName.Visible = false;
                btnAddExpense.Enabled = true;
                lblInventoryId.Text = "";
                txtInventoryId.Text = "";
                lnkGridshow.Visible = false;
                ImageButton ibtnExpenseEdit = (ImageButton)sender;

                GridViewRow row = (GridViewRow)ibtnExpenseEdit.NamingContainer;
                Label lblvin = row.FindControl("lblvin") as Label;
                HiddenField hdnEntityTypeId = row.FindControl("hdnEntityTypeId") as HiddenField;

                LinkButton lnkExpenseDate = row.FindControl("lnkExpenseDate") as LinkButton;
                hdnExpenseId = row.FindControl("hdnExpenseID") as HiddenField;
                ViewState["ExpenseId"] = hdnExpenseId.Value;
                HiddenField hdnMake = row.FindControl("hdnMake") as HiddenField;
                HiddenField hdnModel = row.FindControl("hdnModel") as HiddenField;
                HiddenField hdnYear = row.FindControl("hdnYear") as HiddenField;
                HiddenField InventoryNumber = row.FindControl("hdnInventoryNo") as HiddenField;
                lblHeading.Text = hdnYear.Value + " " + hdnMake.Value + " " + hdnModel.Value + "</br> VIN# " + lblvin.Text + " (Code: " + InventoryNumber.Value + ")";
                long ExpenseId = Convert.ToInt64(gvAllExpenseList.DataKeys[row.RowIndex].Value);
                ddlEntity.Enabled = false;

                //Show Data For Editing.
                this.trType.Visible = false;
                this.trVendor.Visible = false;
                this.trEntityShowname.Visible = true;
                hdExpenseUpId.Value = ExpenseId.ToString();
                BindExpenseType();
                //Read Expense amount from Hidden Field(UnFormatted Text)
                HiddenField hdExpenseAmount = row.Cells[10].FindControl("hdExpenseAmount") as HiddenField;
                HiddenField hdnExpenseTypeId = row.Cells[10].FindControl("hdnExpenseTypeId") as HiddenField;
                HiddenField hdnComments = row.Cells[10].FindControl("hdnComments") as HiddenField;
                HiddenField hdnInventoryNo = row.Cells[10].FindControl("hdnInventoryNo") as HiddenField;
                if (hdExpenseAmount != null && !string.IsNullOrEmpty(hdExpenseAmount.Value))
                    txtExpenseAmount.Text = hdExpenseAmount.Value;

                else
                {
                    txtExpenseAmount.Text = decimal.Zero.ToString();
                    hdExpenseAmount.Value = decimal.Zero.ToString();
                }

                //if (row.Cells[1].Text != blankSpace)
                //{
                //    string[] str = row.Cells[1].Text.Split(' ');
                txtExpenseDate.Text = lnkExpenseDate.Text;
                //}

                if (!string.IsNullOrEmpty(hdnExpenseTypeId.Value))
                    ddlExpenseType.SelectedValue = hdnExpenseTypeId.Value;

                if (!string.IsNullOrEmpty(hdnComments.Value))
                    txtComments.Text = hdnComments.Value;

                LinkButton lblDealer = gvAllExpenseList.Rows[row.RowIndex].FindControl("lblDealer") as LinkButton;
                if (!string.IsNullOrEmpty(lblDealer.Text))
                    lblEntityName.Text = lblDealer.Text;

                if (!string.IsNullOrEmpty(hdnInventoryNo.Value))
                {
                    lblInventoryId.Visible = true;
                    lblInventoryId.Text = hdnInventoryNo.Value;
                }

                HiddenField hdnExpTypeId = (HiddenField)gvAllExpenseList.Rows[row.RowIndex].FindControl("hdnExpenseTypeId");
                if ((!string.IsNullOrEmpty(hdnExpTypeId.Value)) && (hdnExpTypeId.Value == "50"))
                    ddlExpenseType.Enabled = false;

                else
                    ddlExpenseType.Enabled = true;
                //Show Table Row for Entity Name
                // trEntityName.Visible = true;

                if ((!string.IsNullOrEmpty(hdnEntityTypeId.Value)) && (hdnEntityTypeId.Value == "1"))
                {
                    ddlExpenseType.Enabled = false;
                    txtExpenseDate.Enabled = false;
                }
                else
                {
                    ddlExpenseType.Enabled = true;
                    txtExpenseDate.Enabled = true;
                }

                if ((hdnExpTypeId.Value == "50") || (hdnEntityTypeId.Value == "1"))
                    ddlExpenseType.Enabled = false;
                else
                    ddlExpenseType.Enabled = true;

                HiddenField hdnInvoiceNumber = row.FindControl("hdnInvoiceNumber") as HiddenField;
                if (!string.IsNullOrEmpty(hdnInvoiceNumber.Value))
                {
                    string[] strvalue = hdnInvoiceNumber.Value.Split('#');
                    if (strvalue.Length > 0)
                        txtInvoice.Text = strvalue[1].Trim();
                    else
                        txtInvoice.Text = "";
                }

                MPEAddExpense.Show();
            }
            catch (Exception ex) { }
        }

        protected void btnAddExpense_Click(object sender, EventArgs e)
        {
            try
            {
                InventoryBAL objBAL = new InventoryBAL();
                Expense objExp = new Expense();

                //Check duplicate expense
                long EntityID = Convert.ToInt64(ddlEntity.SelectedValue);
                Int32 EntityTypeID = Convert.ToInt32(ddlEntityType.SelectedValue);
                Int32 ExpenseID = Convert.ToInt32(ddlExpenseType.SelectedValue);
                DataTable dtDupExpense = objBAL.GetDuplicateExpenseDetails(EntityID, EntityTypeID, ExpenseID, txtExpenseAmount.Text, Code, 30);

                if (dtDupExpense.Rows.Count > 0)
                {
                    MPEAddExpense.Show();
                    btnAddExpense.Enabled = false;
                }
                else
                {
                    //ADD EXPENSE
                    if (hdExpenseUpId.Value == "-1")
                    {
                        if (!string.IsNullOrEmpty(txtExpenseAmount.Text.Trim()))
                            objExp.ExpenseAmount = Convert.ToDecimal(txtExpenseAmount.Text);
                        else
                            objExp.ExpenseAmount = decimal.Zero;

                        if (!string.IsNullOrEmpty(ddlExpenseType.SelectedValue))
                            objExp.ExpenseTypeId = Convert.ToInt32(ddlExpenseType.SelectedValue);



                        if (!string.IsNullOrEmpty(txtExpenseDate.Text))
                            objExp.ExpenseDate = Convert.ToDateTime(txtExpenseDate.Text);

                        objExp.Comments = txtComments.Text;
                        if (!string.IsNullOrEmpty(hdnInventory.Value))
                            objExp.InventoryId = Convert.ToInt64(hdnInventory.Value);

                        objExp.AddedBy = Constant.UserId;
                        objExp.DateAdded = DateTime.Now;

                        //EntityType Id for Dealer=1, Buyer=2 and Vendor=3
                        if (ddlEntityType.SelectedValue == ENTITY_TYPEID_BUYER)
                            objExp.EntityTypeId = Convert.ToInt32(ENTITY_TYPEID_BUYER);

                        else if (ddlEntityType.SelectedValue == ENTITY_TYPEID_VENDOR)
                            objExp.EntityTypeId = Convert.ToInt32(ENTITY_TYPEID_VENDOR);

                        else if (ddlEntityType.SelectedValue == ENTITY_TYPEID_DEALER)
                            objExp.EntityTypeId = Convert.ToInt32(ENTITY_TYPEID_DEALER);

                        if (!string.IsNullOrEmpty(ddlEntity.SelectedValue))
                            objExp.EntityId = Convert.ToInt64(ddlEntity.SelectedValue);

                        if (!string.IsNullOrEmpty(txtInvoice.Text))
                            objExp.InvoiceNo = txtInvoice.Text;


                        objExp.CheckPaid = false;

                        objBAL.AddNewExpense(objExp);


                    }
                    //EDIT EXPENSE
                    else
                    {
                        //////////////Insert TableHistory to maintain Update History//
                        decimal ExpenseAmount;
                        int ExpenseType;
                        string ExpenseDate;
                        string Commnets = string.Empty;
                        decimal ExpenseAmountNew;
                        int ExpenseTypeNew;
                        string ExpenseDateNew;
                        string CommnetsNew = string.Empty;
                        string invoiceNo = string.Empty;
                        string invoiceNoNew = string.Empty;
                        if (ViewState["ExpenseId"] != null)
                        {
                            DataTable dtOldValue = ExpBAL.GetMobileExpenseDetails(Convert.ToInt32(ViewState["ExpenseId"]));

                            int ret;
                            if (dtOldValue.Rows.Count > 0)
                            {
                                if (Convert.ToString(dtOldValue.Rows[0]["ExpenseAmount"]) != txtExpenseAmount.Text)
                                {
                                    ExpenseAmountNew = Convert.ToDecimal(txtExpenseAmount.Text);
                                    if (!string.IsNullOrEmpty(Convert.ToString(dtOldValue.Rows[0]["ExpenseAmount"])))
                                        ExpenseAmount = Convert.ToDecimal(dtOldValue.Rows[0]["ExpenseAmount"]);
                                    else
                                        ExpenseAmount = 0;
                                }
                                else
                                {
                                    ExpenseAmountNew = 0;
                                    ExpenseAmount = 0;
                                }
                                if (Convert.ToString(dtOldValue.Rows[0]["ExpenseTypeId"]) != ddlExpenseType.SelectedValue)
                                {
                                    ExpenseTypeNew = Convert.ToInt32(ddlExpenseType.SelectedValue);
                                    ExpenseType = Convert.ToInt32(dtOldValue.Rows[0]["ExpenseTypeId"]);
                                }
                                else
                                {
                                    ExpenseTypeNew = -1;
                                    ExpenseType = -1;
                                }
                                if (!string.IsNullOrEmpty(txtExpenseDate.Text) && (Convert.ToDateTime(dtOldValue.Rows[0]["ExpenseDate"]).ToString("dd/MM/yy")) != Convert.ToDateTime(txtExpenseDate.Text).ToString("dd/MM/yy"))
                                {
                                    ExpenseDateNew = txtExpenseDate.Text;
                                    ExpenseDate = Convert.ToString(dtOldValue.Rows[0]["ExpenseDate"]);
                                }
                                else
                                {
                                    ExpenseDateNew = "";
                                    ExpenseDate = "";
                                }
                                if ((!string.IsNullOrEmpty(txtComments.Text)) && (Convert.ToString(dtOldValue.Rows[0]["Comments"]) != txtComments.Text))
                                {
                                    CommnetsNew = txtComments.Text;
                                    Commnets = Convert.ToString(dtOldValue.Rows[0]["Comments"]);
                                }
                                else if ((string.IsNullOrEmpty(txtComments.Text)) && (Convert.ToString(dtOldValue.Rows[0]["Comments"]) != txtComments.Text))
                                {
                                    CommnetsNew = txtComments.Text;
                                    Commnets = Convert.ToString(dtOldValue.Rows[0]["Comments"]);
                                }
                                else
                                {
                                    CommnetsNew = "";
                                    Commnets = "";
                                }

                                if ((!string.IsNullOrEmpty(txtInvoice.Text)) && (txtInvoice.Text != Convert.ToString(dtOldValue.Rows[0]["InvoiceNo"])))
                                {
                                    invoiceNoNew = txtInvoice.Text.Trim();
                                    invoiceNo = Convert.ToString(dtOldValue.Rows[0]["InvoiceNo"]).Trim();
                                }
                                else if ((string.IsNullOrEmpty(txtInvoice.Text)) && (txtInvoice.Text != Convert.ToString(dtOldValue.Rows[0]["InvoiceNo"])))
                                {
                                    invoiceNoNew = txtInvoice.Text.Trim();
                                    invoiceNo = Convert.ToString(dtOldValue.Rows[0]["InvoiceNo"]).Trim();
                                }
                                else
                                {
                                    invoiceNoNew = "";
                                    invoiceNo = "";
                                }


                                ret = ExpBAL.UpdateHistoryTable(Convert.ToInt32(ViewState["ExpenseId"]), Convert.ToInt32(Constant.UserId), ExpenseAmount, ExpenseType, ExpenseDate, Commnets, ExpenseAmountNew, ExpenseTypeNew, ExpenseDateNew, CommnetsNew, invoiceNo, invoiceNoNew, "ViewAllExpense");
                            }
                        }
                        //////////////////End/////////////////////////////////////////

                        //Create new instance of class
                        objExp = new Expense();
                        if (!string.IsNullOrEmpty(txtExpenseAmount.Text.Trim()))
                            objExp.ExpenseAmount = Convert.ToDecimal(txtExpenseAmount.Text);
                        else
                            objExp.ExpenseAmount = decimal.Zero;

                        if (!string.IsNullOrEmpty(ddlExpenseType.SelectedValue))
                            objExp.ExpenseTypeId = Convert.ToInt32(ddlExpenseType.SelectedValue);

                        objExp.Comments = txtComments.Text;
                        objExp.ModifiedBy = Constant.UserId;
                        objExp.DateModified = DateTime.Now;

                        if (!string.IsNullOrEmpty(txtExpenseDate.Text))
                            objExp.ExpenseDate = Convert.ToDateTime(txtExpenseDate.Text);

                        //Update Expense

                        if (!string.IsNullOrEmpty(txtInvoice.Text))
                            objExp.InvoiceNo = txtInvoice.Text;

                        objBAL.UpdateExpense(objExp, Convert.ToInt64(hdExpenseUpId.Value));

                        if ((objExp.ExpenseTypeId != null) && (objExp.ExpenseTypeId == 50))
                        {
                            int ret = ExpBAL.UpdateInventoryCost(Convert.ToInt32(lblInventoryId.Text), objExp.ExpenseAmount);
                        }
                    }
                }
                txtInvoice.Text = "";
                BindGrid();
            }
            catch (Exception ex) { }
        }

        protected void imgDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton ibtnExpenseEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)ibtnExpenseEdit.NamingContainer;
                long ExpenseId = Convert.ToInt64(gvAllExpenseList.DataKeys[row.RowIndex].Value);
                //Expense obj = new Expense();
                //obj.IsActive = 0;
                //obj.DeletedBy = Constant.UserId;
                //obj.DateDeleted = DateTime.Now;
                //InventoryBAL objBAL = new InventoryBAL();
                ExpBAL.DeleteExpense(Convert.ToInt32(ExpenseId), Convert.ToInt32(Constant.UserId));
                BindGrid();
            }
            catch (Exception ex) { }
        }

        protected void btnAddInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean flag = false;
                foreach (GridViewRow row in gvAllExpenseList.Rows)
                {
                    CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                    if (chkSelect.Checked)
                        flag = true;
                }
                if (flag == true)
                    mpePreExpDetail.Show();
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please check at least one Expense.');", true);
                }
            }
            catch (Exception ex) { }
        }

        [WebMethod]
        public static PreExpDetailsPopup[] ViewPreExpenseDetails(long PreExpenseID)
        {
            ExpenseBAL bal = new ExpenseBAL();

            List<GetMobExpDetailsbyExpenseIdResult> result = new List<GetMobExpDetailsbyExpenseIdResult>();
            List<PreExpDetailsPopup> Count = new List<PreExpDetailsPopup>();
            result = bal.PreExpenseDetailBy_PreExpenseId(PreExpenseID);
            PreExpDetailsPopup preExpense = new PreExpDetailsPopup();

            foreach (var row in result)
            {
                preExpense.PreExpVIN = row.VIN;
                preExpense.PreExpenseDate = row.ExpenseDate.ToString();
                preExpense.PreCount = row.Count.ToString();
                preExpense.PreSyncDate = row.SyncDate.ToString();
                preExpense.PreDefaultPrice = row.DefaultPrice.ToString();
                preExpense.PreApprovedBy = row.ApprovedBy;
                preExpense.PreTotalPrice = row.TotalPrice.ToString();
                preExpense.PreAddedBy = row.DisplayName;
                preExpense.PreDescription = row.Description;
                preExpense.PreApprovalNote = row.ApprovalNote;
                preExpense.PreDeviceName = row.DeviceName;
                Count.Add(preExpense);
            }

            return Count.ToArray();

        }

    }

    public class PreExpDetailsPopup
    {
        public string PreExpVIN { get; set; }
        public string PreExpenseDate { get; set; }
        public string PreCount { get; set; }
        public string PreSyncDate { get; set; }
        public string PreDefaultPrice { get; set; }
        public string PreApprovedBy { get; set; }
        public string PreTotalPrice { get; set; }
        public string PreAddedBy { get; set; }
        public string PreDescription { get; set; }
        public string PreDeviceName { get; set; }
        public string PreApprovalNote { get; set; }
    }

}