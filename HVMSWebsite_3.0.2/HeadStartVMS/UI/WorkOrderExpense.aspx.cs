using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using METAOPTION.BAL;
using System.Web.Services;
using System.Web.Security;

namespace METAOPTION.UI
{
    public partial class WorkOrderExpense : System.Web.UI.Page
    {
        WorkOrderBAL woBAL = new WorkOrderBAL();

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
            if (!IsPostBack)
            {
                BindAddedByddl();
                BindVendors();
                BindSort1DDL();
                BindGrid();
            }
        }

        #region[Bind grid]
        protected void BindGrid()
        {
            String SyncDateFrom, SyncDateTo;
            if (String.IsNullOrEmpty(txtSyncDateFrom.Text))
                SyncDateFrom = DateTime.Today.AddYears(-100).ToShortDateString();
            else
                SyncDateFrom = txtSyncDateFrom.Text;

            if (String.IsNullOrEmpty(txtSyncDateTo.Text))
                SyncDateTo = DateTime.Today.AddDays(1).ToShortDateString();
            else
                SyncDateTo = Convert.ToDateTime(txtSyncDateTo.Text).AddDays(1).ToShortDateString();

            // ddlPageSize1.SelectedValue = ddlPageSize2.SelectedValue;
            gvWOExpenses.PageSize = Convert.ToInt32(ddlPageSize1.SelectedValue);

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
                sort = "WO.WorkOrderID DESC";

            ObjectDataSource odsExpense = new ObjectDataSource();
            odsExpense.Selected += new ObjectDataSourceStatusEventHandler(odsWOExpense_Selected);
            odsExpense.TypeName = "METAOPTION.WorkOrderBAL";
            odsExpense.SelectMethod = "SearchWorkOrder";
            odsExpense.SelectCountMethod = "SearchWorkOrderCount";
            odsExpense.EnablePaging = true;
            odsExpense.SelectParameters.Add("VIN", txtVINNumber.Text.Trim());
            odsExpense.SelectParameters.Add("AddedBy", DbType.Int32, ddlAddedBy.SelectedValue);
            odsExpense.SelectParameters.Add("Vendor", DbType.Int32, ddlVendor.SelectedValue);
            odsExpense.SelectParameters.Add("WONumber", txtWONumber.Text.Trim());
            odsExpense.SelectParameters.Add("Status", DbType.Int32, ddlStatus.SelectedValue);
            odsExpense.SelectParameters.Add("SyncFrom", SyncDateFrom);
            odsExpense.SelectParameters.Add("SyncTo", SyncDateTo);
            odsExpense.SelectParameters.Add("StartRowIndex", DbType.Int32, gvWOExpenses.PageIndex.ToString());
            odsExpense.SelectParameters.Add("MaximumRows", DbType.Int32, ddlPageSize1.SelectedValue);
            odsExpense.SelectParameters.Add("SortExpression", sort);
            odsExpense.SelectParameters.Add("OrgID", Constant.OrgID.ToString());
            gvWOExpenses.DataSource = odsExpense;
            gvWOExpenses.DataBind();
        }
        #endregion

        #region[Paging and record count info]
        protected void odsWOExpense_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue == null) return;
            if (e.ReturnValue.GetType().Name == "Int32")
            {

                Int32 count = Convert.ToInt32(e.ReturnValue);
                Int32 pagesize = gvWOExpenses.PageSize;
                Int32 pagecount = count / pagesize;
                if (count < pagesize)
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} of {0} records", count);
                }
                else
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} through {1} of {2} records"
                         , String.Format("{0:#,###}", pagesize * gvWOExpenses.PageIndex + 1)
                         , String.Format("{0:#,###}", pagesize * (gvWOExpenses.PageIndex + 1) > count ? count : pagesize * (gvWOExpenses.PageIndex + 1))
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

                    ddlPaging.SelectedValue = String.Format("{0}", gvWOExpenses.PageIndex + 1);
                    ddlPaging1.SelectedValue = String.Format("{0}", gvWOExpenses.PageIndex + 1);
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
            gvWOExpenses.PageIndex = 0;
            BindGrid();
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedIndex > 0)
                gvWOExpenses.PageIndex--;
            BindGrid();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedValue != ddlPaging.Items.Count.ToString())
                gvWOExpenses.PageIndex++;
            BindGrid();
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            gvWOExpenses.PageIndex = ddlPaging.Items.Count - 1;
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

            gvWOExpenses.PageIndex = 0;
            BindGrid();
        }
        #endregion

        #region[Drop down page selection change]
        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                gvWOExpenses.PageIndex = Convert.ToInt32(((DropDownList)sender).SelectedValue) - 1;
            BindGrid();
            EnablePaging();
        }
        #endregion

        #region[Format device id]
        public string FormatDeviceID(object oDeviceID)
        {
            String DeviceID = Convert.ToString(oDeviceID);
            string strDeviceID = String.Empty;
            if (!string.IsNullOrEmpty(DeviceID))
            {
                if (DeviceID != "5" && DeviceID.Length > 5)
                    strDeviceID = DeviceID.Substring(0, 5);
                else
                    strDeviceID = String.Empty;
            }
            return strDeviceID;
        }
        #endregion

        #region[Format device Name]
        public string FormatDeviceName(object oDeviceName)
        {
            String DeviceName = Convert.ToString(oDeviceName);
            string strDeviceName = String.Empty;
            if (!string.IsNullOrEmpty(DeviceName))
            {
                if (DeviceName == "iPhone OS")
                    strDeviceName = DeviceName.Substring(0, 6);
                else
                    strDeviceName = DeviceName;
            }
            return strDeviceName;
        }
        #endregion

        #region[Search button event handler]
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region[Bind Added By Dropdown]
        protected void BindAddedByddl()
        {
            ddlAddedBy.DataSource = woBAL.GetWorkOrderUsers(Constant.OrgID);
            ddlAddedBy.DataTextField = "DisplayName";
            ddlAddedBy.DataValueField = "SecurityUserID";
            ddlAddedBy.DataBind();
            ddlAddedBy.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        #endregion

        #region[Bind Vendor Dropdown]
        protected void BindVendors()
        {
            ddlVendor.DataSource = woBAL.GetWorkOrderVendors(Constant.OrgID);
            ddlVendor.DataTextField = "VendorName";
            ddlVendor.DataValueField = "VendorID";
            ddlVendor.DataBind();
            ddlVendor.Items.Insert(0, new ListItem("ALL", "-1"));
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

        #region[ Bind sort dropdown]
        private void BindSort1DDL()
        {
            ddlSort1.DataSource = woBAL.WorkOrder_SortOptions("Select SortText,SortValue from Lookup_Sort where tableid=6 order by Sequence asc", "", "");
            ddlSort1.DataTextField = "SortText";
            ddlSort1.DataValueField = "SortValue";
            ddlSort1.DataBind();

            ddlSort1.Items.Insert(0, new ListItem("", "-1"));

            BindSort2DDL();
            BindSort3DDL();
        }

        private void BindSort2DDL()
        {
            ddlSort2.DataSource = woBAL.WorkOrder_SortOptions(String.Format("Select SortText,SortValue from Lookup_Sort where tableid=6 AND SortValue NOT IN ('{0}') order by Sequence asc", ddlSort1.SelectedValue), "", "");
            ddlSort2.DataTextField = "SortText";
            ddlSort2.DataValueField = "SortValue";
            ddlSort2.DataBind();

            ddlSort2.Items.Insert(0, new ListItem("", "-1"));

            BindSort3DDL();
        }

        private void BindSort3DDL()
        {
            ddlSort3.DataSource = woBAL.WorkOrder_SortOptions(String.Format("Select SortText,SortValue from Lookup_Sort where tableid=6 AND SortValue NOT IN ('{0}', '{1}') order by Sequence asc", ddlSort1.SelectedValue, ddlSort2.SelectedValue), "", "");
            ddlSort3.DataTextField = "SortText";
            ddlSort3.DataValueField = "SortValue";
            ddlSort3.DataBind();

            ddlSort3.Items.Insert(0, new ListItem("", "-1"));

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

        protected void gvWOExpenses_Sorting(object sender, GridViewSortEventArgs e)
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

        #region[View work order expense details]
        protected void ibtnExpand_Click(object sender, ImageClickEventArgs e)
        {
            long WOId = Convert.ToInt64(gvWOExpenses.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            ImageButton ibtn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)ibtn.NamingContainer;

            GridView gvExpenseDetail;
            ImageButton ibtnExpand = (ImageButton)row.FindControl("ibtnExpand");
            gvExpenseDetail = (GridView)row.FindControl("gvExpenseDetail");
            gvExpenseDetail.RowDataBound += new GridViewRowEventHandler(gvExpenseDetail_RowDataBound);
            string imgUrl = ibtnExpand.ImageUrl;
            if (imgUrl == "~/Images/expand.png")
            {
                gvExpenseDetail.DataSource = MakeExpenseGroup(woBAL.FillWorkOrderExpenses(WOId));
                gvExpenseDetail.DataBind();
                ibtnExpand.ToolTip = "Collapse";
                ibtnExpand.ImageUrl = "~/Images/collapse.png";

            }
            else
            {
                ibtnExpand.ToolTip = "Expand";
                ibtnExpand.ImageUrl = "~/Images/expand.png";
            }

        }
        #endregion

        #region[Work order expense grid - RowDataBound]
        void gvExpenseDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfRowType = (HiddenField)e.Row.FindControl("hfRowType");
                if (hfRowType.Value.ToUpper() == "EMPTYROW")
                {
                    e.Row.Cells[0].ColumnSpan = 13;

                    foreach (TableCell cell in e.Row.Cells)
                    {
                        int CellIndex = e.Row.Cells.GetCellIndex(cell);
                        if (CellIndex != 0)
                            cell.Visible = false;
                    }

                }

                #region[Notification Status]
                HiddenField hfStatusCode = (HiddenField)e.Row.FindControl("hfStatusCode");
                ImageButton imgVEmailStatus = (ImageButton)e.Row.FindControl("imgVendorEmailStatus");
                ImageButton imgVSMSStatus = (ImageButton)e.Row.FindControl("imgVendorSMSStatus");
                ImageButton imgDEmailStatus = (ImageButton)e.Row.FindControl("imgDriverEmailStatus");
                ImageButton imgDSMSStatus = (ImageButton)e.Row.FindControl("imgDriverSMSStatus");

                HiddenField hfIsVendorEmailSent = (HiddenField)e.Row.FindControl("hfIsVendorEmailSent");
                HiddenField hfIsVendorSMSSent = (HiddenField)e.Row.FindControl("hfIsVendorSMSSent");
                HiddenField hfIsDriverEmailSent = (HiddenField)e.Row.FindControl("hfIsDriverEmailSent");
                HiddenField hfIsDriverSMSSent = (HiddenField)e.Row.FindControl("hfIsDriverSMSSent");

                //Vendor Email icon
                if (hfIsVendorEmailSent.Value == "True")
                {
                    imgVEmailStatus.ImageUrl = "~/Images/Email_Sent.png";
                    imgVEmailStatus.ToolTip = "Vendor email sent";
                }
                else
                {
                    imgVEmailStatus.ImageUrl = "~/Images/Email_NotSent.png";
                    imgVEmailStatus.Enabled = false;
                    imgVEmailStatus.ToolTip = "Vendor email not sent";
                }

                //Vendor SMS icon
                if (hfIsVendorSMSSent.Value == "True")
                {
                    imgVSMSStatus.ImageUrl = "~/Images/sms-sent.png";
                    imgVSMSStatus.ToolTip = "Vendor SMS sent";
                }
                else
                {
                    imgVSMSStatus.ImageUrl = "~/Images/sms_not_sent.png";
                    imgVSMSStatus.ToolTip = "Vendor SMS not sent";
                    imgVSMSStatus.Enabled = false;
                }

                //Driver Email icon
                if (hfStatusCode.Value == "0" || hfStatusCode.Value == "3")
                {
                    imgDEmailStatus.Visible = true;
                    imgDSMSStatus.Visible = true;
                    if (hfIsDriverEmailSent.Value == "True")
                    {
                        imgDEmailStatus.ImageUrl = "~/Images/Driver_EmailSent.png";
                        imgDEmailStatus.ToolTip = "Driver email sent";
                    }
                    else
                    {
                        imgDEmailStatus.ImageUrl = "~/Images/Driver_EmailNotSent.png";
                        imgDEmailStatus.ToolTip = "Driver email not sent";
                        imgDEmailStatus.Enabled = false;
                    }

                    //Driver SMS icon
                    if (hfIsDriverSMSSent.Value == "True")
                    {
                        imgDSMSStatus.ImageUrl = "~/Images/Driver_SMSSent.png";
                        imgDSMSStatus.ToolTip = "Driver SMS sent";
                    }
                    else
                    {
                        imgDSMSStatus.ImageUrl = "~/Images/Driver_SMSNotSent.png";
                        imgDSMSStatus.ToolTip = "Driver SMS not sent";
                        imgDSMSStatus.Enabled = false;
                    }
                }
                #endregion

            }
        }
        #endregion

        #region[Make WO expense group]
        public DataTable MakeExpenseGroup(List<Mobile_FillWOExpensesResult> listOfExpenses)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("GroupName");
            dt.Columns.Add("EntityName");
            dt.Columns.Add("EntityType");
            dt.Columns.Add("WOExpenseID");
            dt.Columns.Add("ParentWOExpenseID");
            dt.Columns.Add("ExpenseType");
            dt.Columns.Add("Count");
            dt.Columns.Add("DefaultPrice");
            dt.Columns.Add("TotalPrice");
            dt.Columns.Add("Description");
            dt.Columns.Add("DeviceID");
            dt.Columns.Add("DeviceTypeID");
            dt.Columns.Add("DeviceName");
            dt.Columns.Add("Latitude");
            dt.Columns.Add("Longitude");
            dt.Columns.Add("SyncDate");
            dt.Columns.Add("DateAdded");
            dt.Columns.Add("DateModified");
            dt.Columns.Add("Status");
            dt.Columns.Add("StatusCode");
            dt.Columns.Add("IsVendorEmailSent");
            dt.Columns.Add("IsVendorSMSSent");
            dt.Columns.Add("IsShopManagerEmailSent");
            dt.Columns.Add("IsShopManagerSMSSent");
            dt.Columns.Add("IsDriverEmailSent");
            dt.Columns.Add("IsDriverSMSSent");
            dt.Columns.Add("WorkOrderDetailID");
            dt.Columns.Add("RowType");

            int GroupCount = 1;
            String EntityName = String.Empty;
            String EntityType = String.Empty;
            String Status = String.Empty;
            int? StatusCode = 0;

            EntityName = listOfExpenses[0].EntityName;
            EntityType = listOfExpenses[0].EntityType;
            Status = listOfExpenses[0].Status;
            StatusCode = listOfExpenses[0].StatusCode;

            DataRow EmptyRow = GetEmptyRow(dt, EntityName, EntityType, Status);
            dt.Rows.Add(EmptyRow);

            foreach (var exp in listOfExpenses)
            {
                DataRow row = dt.NewRow();

                if (exp.EntityName != EntityName && exp.StatusCode != StatusCode)
                {
                    EntityName = exp.EntityName;
                    EntityType = exp.EntityType;
                    Status = exp.Status;
                    StatusCode = exp.StatusCode;
                    GroupCount += 1;
                    EmptyRow = GetEmptyRow(dt, EntityName, EntityType, Status);
                    dt.Rows.Add(EmptyRow);
                }
                else if (exp.EntityName == EntityName && exp.StatusCode != StatusCode)
                {
                    EntityName = exp.EntityName;
                    EntityType = exp.EntityType;
                    Status = exp.Status;
                    StatusCode = exp.StatusCode;
                    GroupCount += 1;
                    EmptyRow = GetEmptyRow(dt, EntityName, EntityType, Status);
                    dt.Rows.Add(EmptyRow);
                }
                else if (exp.EntityName != EntityName && exp.StatusCode == StatusCode)
                {
                    EntityName = exp.EntityName;
                    EntityType = exp.EntityType;
                    Status = exp.Status;
                    StatusCode = exp.StatusCode;
                    GroupCount += 1;
                    EmptyRow = GetEmptyRow(dt, EntityName, EntityType, Status);
                    dt.Rows.Add(EmptyRow);
                }

                row["GroupName"] = String.Empty;
                row["WOExpenseID"] = Convert.ToString(exp.WOExpenseID);
                row["EntityName"] = exp.EntityName;
                row["ParentWOExpenseID"] = Convert.ToString(exp.ParentWOExpenseID);
                row["ExpenseType"] = exp.ExpenseType;
                row["Count"] = Convert.ToString(exp.Count);
                row["DefaultPrice"] = Convert.ToString(exp.DefaultPrice);
                row["TotalPrice"] = Convert.ToString(exp.TotalPrice);
                row["Description"] = exp.Description;
                row["Latitude"] = exp.Latitude;
                row["Longitude"] = exp.Longitude;
                row["SyncDate"] = Convert.ToString(exp.SyncDate);
                row["DateAdded"] = Convert.ToString(exp.DateAdded);
                row["DateModified"] = Convert.ToString(exp.DateModified);
                row["DeviceID"] = Convert.ToString(exp.DeviceID);
                row["DeviceTypeID"] = Convert.ToString(exp.DeviceTypeID);
                row["DeviceName"] = Convert.ToString(exp.DeviceName);
                row["StatusCode"] = Convert.ToString(exp.StatusCode);
                row["IsVendorEmailSent"] = Convert.ToString(exp.IsVendorEmailSent);
                row["IsVendorSMSSent"] = Convert.ToString(exp.IsVendorSMSSent);
                row["IsShopManagerEmailSent"] = Convert.ToString(exp.IsShopManagerEmailSent);
                row["IsShopManagerSMSSent"] = Convert.ToString(exp.IsShopManagerSMSSent);
                row["IsDriverEmailSent"] = Convert.ToString(exp.IsDriverEmailSent);
                row["IsDriverSMSSent"] = Convert.ToString(exp.IsDriverSMSSent);
                row["WorkOrderDetailID"] = Convert.ToString(exp.WorkOrderDetailID);
                row["RowType"] = "DataRow";
                dt.Rows.Add(row);
            }
            return dt;
        }

        public DataRow GetEmptyRow(DataTable dt, String EntityName, String EntityType, String Status)
        {
            DataRow EmptyRow = dt.NewRow();
            EmptyRow["GroupName"] = String.Format("{0} ({1}) | {2}", EntityName, EntityType, Status);
            EmptyRow["WOExpenseID"] = String.Empty;
            EmptyRow["EntityName"] = String.Empty;
            EmptyRow["ParentWOExpenseID"] = String.Empty;
            EmptyRow["ExpenseType"] = String.Empty;
            EmptyRow["Count"] = String.Empty;
            EmptyRow["DefaultPrice"] = String.Empty;
            EmptyRow["TotalPrice"] = String.Empty;
            EmptyRow["Description"] = String.Empty;
            EmptyRow["Latitude"] = String.Empty;
            EmptyRow["Longitude"] = String.Empty;
            EmptyRow["SyncDate"] = String.Empty;
            EmptyRow["DateAdded"] = String.Empty;
            EmptyRow["DateModified"] = String.Empty;
            EmptyRow["StatusCode"] = String.Empty;
            EmptyRow["IsVendorEmailSent"] = String.Empty;
            EmptyRow["IsVendorSMSSent"] = String.Empty;
            EmptyRow["IsShopManagerEmailSent"] = String.Empty;
            EmptyRow["IsShopManagerSMSSent"] = String.Empty;
            EmptyRow["IsDriverEmailSent"] = String.Empty;
            EmptyRow["IsDriverSMSSent"] = String.Empty;
            EmptyRow["WorkOrderDetailID"] = String.Empty;
            EmptyRow["RowType"] = "EmptyRow";
            return EmptyRow;
        }


        #endregion

        #region[Delete work order]
        protected void ibtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            long WOId = Convert.ToInt64(gvWOExpenses.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            hfDeleteWOID.Value = WOId.ToString();
            txtreason.Text = string.Empty;
            mpeDeleteWO.Show();
        }

        protected void btnDelOK_Click(object sender, EventArgs e)
        {
            woBAL.DeleteWorkOrder(Convert.ToInt64(hfDeleteWOID.Value), Convert.ToInt64(Session["empId"]), txtreason.Text);
            txtreason.Text = "";
            BindGrid();
        }

        #endregion

        #region[Work order grid - RowDataBound]
        protected void gvWOExpenses_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfStatus = (HiddenField)e.Row.FindControl("hfStatus");
                ImageButton ibtnDelete = (ImageButton)e.Row.FindControl("ibtnDelete");
                ImageButton ibtnApprove = (ImageButton)e.Row.FindControl("ibtnApprove");
                ImageButton ibtnUpdateStatus = (ImageButton)e.Row.FindControl("ibtnUpdateStatus");
                Label lblvinNo = (Label)e.Row.FindControl("lblvinNo");
                HyperLink hlnkVIN = (HyperLink)e.Row.FindControl("hlnkVIN");
                Label lblCode = (Label)e.Row.FindControl("lblCode");
                HyperLink hlnkCode = (HyperLink)e.Row.FindControl("hlnkCode");
                HiddenField hfInventoryID = (HiddenField)e.Row.FindControl("hfInventoryID");

                if (hfStatus.Value == "4") //Approved
                {
                    ibtnDelete.Visible = false;
                    ibtnApprove.Visible = true;
                    e.Row.BackColor = System.Drawing.Color.LightCyan;
                    ibtnUpdateStatus.Visible = true;
                    ibtnUpdateStatus.ImageUrl = "../Images/H_active.png";
                    ibtnUpdateStatus.Enabled = false;
                    if (!String.IsNullOrEmpty(hfInventoryID.Value) && Convert.ToInt64(hfInventoryID.Value) > 0)
                    {
                        lblvinNo.Visible = false;
                        hlnkVIN.Visible = true;
                        lblCode.Visible = true;
                        hlnkCode.Visible = true;
                    }


                }
                else if (hfStatus.Value == "3") //Completed
                {
                    ibtnUpdateStatus.Visible = true;
                    ibtnUpdateStatus.ImageUrl = "../Images/approve.png";
                    ibtnApprove.Visible = false;
                }
                else if (hfStatus.Value == "2") //Accepted
                {
                    ibtnUpdateStatus.Visible = true;
                    ibtnUpdateStatus.ImageUrl = "../Images/complete.png";
                    ibtnApprove.Visible = false;
                }
                else if (hfStatus.Value == "1") //Modified
                {
                    ibtnUpdateStatus.Visible = true;
                    ibtnUpdateStatus.ImageUrl = "../Images/accept.png";
                    ibtnApprove.Visible = false;
                }
                else
                {
                    ibtnDelete.Visible = true;
                    ibtnApprove.Visible = false;
                    ibtnUpdateStatus.Visible = false;
                }

                HiddenField hfImageCount = (HiddenField)e.Row.FindControl("hfImageCount");
                ImageButton ibtnWOImages = (ImageButton)e.Row.FindControl("ibtnWOImages");
                if (Convert.ToInt32(hfImageCount.Value) > 0)
                {
                    ibtnWOImages.Visible = true;
                    ibtnWOImages.ToolTip = String.Format("View {0} {1}", hfImageCount.Value, Convert.ToInt32(hfImageCount.Value) > 1 ? "Images" : "Image");
                }

            }
        }
        #endregion

        #region[Update status]
        protected void ibtnUpdateStatus_Click(object sender, ImageClickEventArgs e)
        {
            long WorkOrderID = Convert.ToInt64(gvWOExpenses.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);

            ImageButton imgUpdateStat = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgUpdateStat.NamingContainer;
            HiddenField hfStatus = (HiddenField)row.FindControl("hfStatus");

            Int32 WorkOrderStatus = Convert.ToInt32(hfStatus.Value) + 1;
            woBAL.UpdateWorkOrder(WorkOrderID, WorkOrderStatus, Convert.ToInt64(Session["empId"]), System.DateTime.Now);
            BindGrid();
        }
        #endregion

        #region[Notification status button click events]
        #region[Vendor Email PopUp]
        [WebMethod]
        public static VendorEmailPopup[] VendorEmailDetail(long WorkOrderDetailID)
        {
            WorkOrderBAL bal = new WorkOrderBAL();
            List<Mobile_GetEmailDetailResult> result = new List<Mobile_GetEmailDetailResult>();
            List<VendorEmailPopup> Count = new List<VendorEmailPopup>();
            result = bal.GetEmailDetail(5009, WorkOrderDetailID);
            VendorEmailPopup p;
            foreach (var r in result)
            {
                p = new VendorEmailPopup();
                p.Mailto = r.MailTo;
                p.MailSubject = r.Subject;
                p.MailFrom = r.MailFrom;
                p.MailCCED = r.MailCCed;
                p.MailBCED = r.MailBCCed;
                p.MailBody = r.Body;
                p.Attempt = r.AttemptCount.ToString();
                p.LoggedOn = r.LogTime.ToString();
                p.SentOn = r.SentTime.ToString();
                Count.Add(p);
            }
            return Count.ToArray();
        }

        public class VendorEmailPopup
        {
            public string Mailto { get; set; }
            public string MailSubject { get; set; }
            public string MailFrom { get; set; }
            public string MailCCED { get; set; }
            public string MailBCED { get; set; }
            public string MailBody { get; set; }
            public string Attempt { get; set; }
            public string LoggedOn { get; set; }
            public string SentOn { get; set; }
        }
        #endregion
        #region[Driver Email PopUp]
        [WebMethod]
        public static DriverEmailPopup[] DriverEmailDetail(long WorkOrderDetailID)
        {
            WorkOrderBAL bal = new WorkOrderBAL();
            List<Mobile_GetEmailDetailResult> result = new List<Mobile_GetEmailDetailResult>();
            List<DriverEmailPopup> Count = new List<DriverEmailPopup>();
            result = bal.GetEmailDetail(4001, WorkOrderDetailID);
            DriverEmailPopup p;
            foreach (var r in result)
            {
                p = new DriverEmailPopup();
                p.Mailto = r.MailTo;
                p.MailSubject = r.Subject;
                p.MailFrom = r.MailFrom;
                p.MailCCED = r.MailCCed;
                p.MailBCED = r.MailBCCed;
                p.MailBody = r.Body;
                p.Attempt = r.AttemptCount.ToString();
                p.LoggedOn = r.LogTime.ToString();
                p.SentOn = r.SentTime.ToString();
                Count.Add(p);
            }
            return Count.ToArray();
        }

        public class DriverEmailPopup
        {
            public string Mailto { get; set; }
            public string MailSubject { get; set; }
            public string MailFrom { get; set; }
            public string MailCCED { get; set; }
            public string MailBCED { get; set; }
            public string MailBody { get; set; }
            public string Attempt { get; set; }
            public string LoggedOn { get; set; }
            public string SentOn { get; set; }
        }
        #endregion
        #region[Vendor SMS PopUp]
        [WebMethod]
        public static VendorSMSPopup[] VendorSMSDetail(long WorkOrderDetailID)
        {
            WorkOrderBAL bal = new WorkOrderBAL();
            List<Mobile_GetEmailDetailResult> result = new List<Mobile_GetEmailDetailResult>();
            List<VendorSMSPopup> Count = new List<VendorSMSPopup>();
            result = bal.GetEmailDetail(4010, WorkOrderDetailID);
            VendorSMSPopup p;
            foreach (var r in result)
            {
                p = new VendorSMSPopup();
                p.SMSTo = r.MailTo;
                p.SMSFrom = r.MailFrom;
                p.SMSBody = r.Body;
                p.SMSAttempt = r.AttemptCount.ToString();
                p.SMSLoggedOn = r.LogTime.ToString();
                p.SMSSentOn = r.SentTime.ToString();
                Count.Add(p);
            }
            return Count.ToArray();
        }

        public class VendorSMSPopup
        {
            public string SMSTo { get; set; }
            public string SMSFrom { get; set; }
            public string SMSBody { get; set; }
            public string SMSAttempt { get; set; }
            public string SMSLoggedOn { get; set; }
            public string SMSSentOn { get; set; }
        }
        #endregion
        #region[Driver SMS PopUp]
        [WebMethod]
        public static DriverSMSPopup[] DriverSMSDetail(long WorkOrderDetailID)
        {
            WorkOrderBAL bal = new WorkOrderBAL();
            List<Mobile_GetEmailDetailResult> result = new List<Mobile_GetEmailDetailResult>();
            List<DriverSMSPopup> Count = new List<DriverSMSPopup>();
            result = bal.GetEmailDetail(4011, WorkOrderDetailID);
            DriverSMSPopup p;
            foreach (var r in result)
            {
                p = new DriverSMSPopup();
                p.SMSTo = r.MailTo;
                p.SMSFrom = r.MailFrom;
                p.SMSBody = r.Body;
                p.SMSAttempt = r.AttemptCount.ToString();
                p.SMSLoggedOn = r.LogTime.ToString();
                p.SMSSentOn = r.SentTime.ToString();
                Count.Add(p);
            }
            return Count.ToArray();
        }

        public class DriverSMSPopup
        {
            public string SMSTo { get; set; }
            public string SMSFrom { get; set; }
            public string SMSBody { get; set; }
            public string SMSAttempt { get; set; }
            public string SMSLoggedOn { get; set; }
            public string SMSSentOn { get; set; }
        }
        #endregion
        #endregion

        #region[WorkOrder images]
        protected void ibtnWOImages_Click(object sender, ImageClickEventArgs e)
        {
            long WorkOrderID = Convert.ToInt64(gvWOExpenses.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            frmWOImages.Attributes.Add("src", String.Format("WOImageGallery.aspx?id={0}", WorkOrderID));
            mpeWOImages.Show();
        }
        #endregion

        #region[PreExp Detail]
        [WebMethod]
        public static PreExpenseDetailsPopup[] PreExpenseDetails(long PreExpenseID)
        {
            PreExpenseBAL bal = new PreExpenseBAL();

            List<Mobile_PreExpense_ByIDResult> result = new List<Mobile_PreExpense_ByIDResult>();
            List<PreExpenseDetailsPopup> Count = new List<PreExpenseDetailsPopup>();
            result = bal.PreExpenseDetailBy_PreExpenseId(PreExpenseID);
            PreExpenseDetailsPopup preExpense = new PreExpenseDetailsPopup();

            foreach (var row in result)
            {
                preExpense.VIN = row.VIN;
                preExpense.ExpenseDate = row.ExpenseDate.ToString();
                preExpense.Count = row.Count.ToString();
                preExpense.SyncDate = row.SyncDate.ToString();
                preExpense.DefaultPrice = row.DefaultPrice.ToString();
                //preExpense.ApprovedBy = row.AddedBy;
                preExpense.TotalPrice = row.TotalPrice.ToString();
                preExpense.AddedBy = row.EntityName;
                preExpense.Description = row.Description;
                //preExpense.ApprovalNote = row.ApprovalNote;
                preExpense.DeviceName = row.DeviceName;
                Count.Add(preExpense);
            }

            return Count.ToArray();

        }

        public class PreExpenseDetailsPopup
        {
            public string VIN { get; set; }
            public string ExpenseDate { get; set; }
            public string Count { get; set; }
            public string SyncDate { get; set; }
            public string DefaultPrice { get; set; }
            //public string ApprovedBy { get; set; }
            public string TotalPrice { get; set; }
            public string AddedBy { get; set; }
            public string Description { get; set; }
            public string DeviceName { get; set; }
            //public string ApprovalNote { get; set; }
        }
        #endregion
    }
}