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
using METAOPTION.UserControls;
using System.Web.Services;

namespace METAOPTION.UI
{
    public partial class PreExpense : System.Web.UI.Page
    {
        public int DuplicatePeriod = 0;

        PreExpenseBAL PreExpBAL = new PreExpenseBAL();

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
            DuplicatePeriod = Convert.ToInt32(ConfigurationManager.AppSettings["DuplicateInDays"]);
            if (!IsPostBack)
            {
                BindAddedByddl();
                BindVendors();
                BindSort1DDL();
                BindGrid();
                if (Convert.ToString(Session["LoginEntityTypeID"]) == "3")
                {
                    if (ddlVendor.Items.FindByValue(Convert.ToString(Session["UserEntityID"])) != null)
                    {
                        ddlVendor.SelectedValue = Convert.ToString(Session["UserEntityID"]);
                        ddlVendor.Enabled = false;
                    }
                    btnapprove.Visible = false;
                    mainlbl.InnerHtml = "VIEW ALL MOBILE EXPENSES";
                }
                if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
                {
                    btnapprove.Visible = false;
                    mainlbl.InnerHtml = "VIEW ALL MOBILE EXPENSES";
                }
            }

        }


        #region[Bind grid]
        protected void BindGrid()
        {
            String BuyerParentId = "-1";
            if (Convert.ToString(Session["BuyerAccessLevel"]) != "1")
                BuyerParentId = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString();

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
            gvExpenseList.PageSize = Convert.ToInt32(ddlPageSize1.SelectedValue);

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
                sort = "PreExpenseID desc";

            ObjectDataSource odsExpense = new ObjectDataSource();
            odsExpense.Selected += new ObjectDataSourceStatusEventHandler(odsExpense_Selected);

            //if ((Convert.ToString(Session["LoginEntityTypeID"]) == "2") || (Convert.ToString(Session["LoginEntityTypeID"]) == "3"))
            //{
            odsExpense.TypeName = "METAOPTION.BAL.PreExpenseBAL";
            odsExpense.SelectMethod = "SearchPreExpense_Ver211";
            odsExpense.SelectCountMethod = "SearchPreExpenseCount_Ver211";
            odsExpense.EnablePaging = true;
            odsExpense.SelectParameters.Add("VIN", txtVINNumber.Text.Trim());
            odsExpense.SelectParameters.Add("AddedBy", DbType.Int32, ddlAddedBy.SelectedValue);
            odsExpense.SelectParameters.Add("Vendor", DbType.Int32, ddlVendor.SelectedValue);
            odsExpense.SelectParameters.Add("Status", DbType.Int32, ddlStatus.SelectedValue);
            odsExpense.SelectParameters.Add("SyncFrom", SyncDateFrom);
            odsExpense.SelectParameters.Add("SyncTo", SyncDateTo);
            odsExpense.SelectParameters.Add("EntityTypeID", DbType.Int32, Session["LoginEntityTypeID"].ToString());
            odsExpense.SelectParameters.Add("EntityID", Convert.ToString(Session["UserEntityID"]));
            odsExpense.SelectParameters.Add("ParentEntityID", BuyerParentId);
            odsExpense.SelectParameters.Add("StartRowIndex", DbType.Int32, gvExpenseList.PageIndex.ToString());
            odsExpense.SelectParameters.Add("MaximumRows", DbType.Int32, ddlPageSize1.SelectedValue);
            odsExpense.SelectParameters.Add("SortExpression", sort);
            odsExpense.SelectParameters.Add("OrgID", Constant.OrgID.ToString());
            gvExpenseList.DataSource = odsExpense;
            gvExpenseList.DataBind();
            //}
            //else
            //{
            //    odsExpense.TypeName = "METAOPTION.BAL.PreExpenseBAL";
            //    odsExpense.SelectMethod = "SearchPreExpense";
            //    odsExpense.SelectCountMethod = "SearchPreExpenseCount";
            //    odsExpense.EnablePaging = true;
            //    odsExpense.SelectParameters.Add("VIN", txtVINNumber.Text.Trim());
            //    odsExpense.SelectParameters.Add("AddedBy", DbType.Int32, ddlAddedBy.SelectedValue);
            //    odsExpense.SelectParameters.Add("Vendor", DbType.Int32, ddlVendor.SelectedValue);
            //    odsExpense.SelectParameters.Add("Status", DbType.Int32, ddlStatus.SelectedValue);
            //    odsExpense.SelectParameters.Add("SyncFrom", SyncDateFrom);
            //    odsExpense.SelectParameters.Add("SyncTo", SyncDateTo);

            //    odsExpense.SelectParameters.Add("StartRowIndex", DbType.Int32, gvExpenseList.PageIndex.ToString());
            //    odsExpense.SelectParameters.Add("MaximumRows", DbType.Int32, ddlPageSize1.SelectedValue);
            //    odsExpense.SelectParameters.Add("SortExpression", sort);
            //    gvExpenseList.DataSource = odsExpense;
            //    gvExpenseList.DataBind();
            //}
        }
        #endregion

        protected void odsExpense_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue == null) return;
            if (e.ReturnValue.GetType().Name == "Int32")
            {

                Int32 count = Convert.ToInt32(e.ReturnValue);
                Int32 pagesize = gvExpenseList.PageSize;
                Int32 pagecount = count / pagesize;
                if (count < pagesize)
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} of {0} records", count);
                }
                else
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} through {1} of {2} records"
                         , String.Format("{0:#,###}", pagesize * gvExpenseList.PageIndex + 1)
                         , String.Format("{0:#,###}", pagesize * (gvExpenseList.PageIndex + 1) > count ? count : pagesize * (gvExpenseList.PageIndex + 1))
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

                    ddlPaging.SelectedValue = String.Format("{0}", gvExpenseList.PageIndex + 1);
                    ddlPaging1.SelectedValue = String.Format("{0}", gvExpenseList.PageIndex + 1);
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
            gvExpenseList.PageIndex = 0;
            BindGrid();

        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedIndex > 0)
                gvExpenseList.PageIndex--;
            BindGrid();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedValue != ddlPaging.Items.Count.ToString())
                gvExpenseList.PageIndex++;
            BindGrid();


        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            gvExpenseList.PageIndex = ddlPaging.Items.Count - 1;
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

            gvExpenseList.PageIndex = 0;
            BindGrid();

        }
        #endregion

        #region[Drop down page selection change]
        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                gvExpenseList.PageIndex = Convert.ToInt32(((DropDownList)sender).SelectedValue) - 1;
            BindGrid();
            EnablePaging();

        }
        #endregion

        #region[Delete PreExpense]
        protected void ibtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            long PreExpID = Convert.ToInt64(gvExpenseList.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            hfDeletePreExpID.Value = PreExpID.ToString();
            txtreason.Text = string.Empty;
            //mpDelete.Show();
        }

        protected void DeleteOk_Click(object sender, EventArgs e)
        {
            if (rdlist.SelectedItem.Value == "D")
            {
                PreExpBAL.DeletePreExpense(Convert.ToInt64(hfDeletePreExpID.Value), Convert.ToInt64(Session["empId"]), Convert.ToInt32(AppEnum.ExpenseLog.ExpenseDeleted), txtreason.Text);
            }
            else if (rdlist.SelectedItem.Value == "R")
            {
                PreExpBAL.RejectPreExpense(Convert.ToInt64(hfDeletePreExpID.Value), Convert.ToInt64(Session["empId"]), Convert.ToInt32(AppEnum.ExpenseLog.ExpenseRejected), txtreason.Text);
            }
            txtreason.Text = "";
            BindGrid();

        }
        #endregion

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

        #region[Approve pre-expense]
        protected void btnapprove_Click(object sender, EventArgs e)
        {
            txtapprovalnate.Text = String.Empty;
            mpApprove.Show();
        }

        protected void btnApproveOk_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PreExpenseID");
            dt.Columns.Add("VIN");
            dt.Columns.Add("InventoryID");

            foreach (GridViewRow grvrow in gvExpenseList.Rows)
            {
                CheckBox chkBx = (CheckBox)grvrow.FindControl("chkappr");
                if (chkBx != null && chkBx.Checked)
                {
                    String PreExpID = Convert.ToString(gvExpenseList.DataKeys[grvrow.RowIndex].Value);
                    HiddenField hfInvID = (HiddenField)grvrow.FindControl("hfInvID");
                    DataRow row = dt.NewRow();
                    row["PreExpenseID"] = PreExpID;
                    Label lblVIN = (Label)grvrow.Cells[1].FindControl("lblVIN");
                    row["VIN"] = lblVIN.Text;
                    row["InventoryID"] = hfInvID.Value;
                    dt.Rows.Add(row);
                }
            }

            if (dt.Rows.Count > 0)
                PreExpBAL.ApproveExpense(Convert.ToInt64(Session["empId"]), Convert.ToInt32(AppEnum.ExpenseLog.ExpenseApproved), dt, txtapprovalnate.Text);
            BindGrid();
        }
        #endregion

        #region[Expense grid - RowDataBound]
        protected void gvExpenseList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                CheckBox chkall = (CheckBox)e.Row.FindControl("chkallappr");
                if ((Convert.ToString(Session["LoginEntityTypeID"]) == "2") || (Convert.ToString(Session["LoginEntityTypeID"]) == "3"))
                {
                    // gvExpenseList.Columns[0].Visible = false;
                    chkall.Visible = false;
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                CheckBox chk = (CheckBox)e.Row.FindControl("chkappr");
                Label lblImageCount = (Label)e.Row.FindControl("lblImageCount");
                Label lblvin = (Label)e.Row.FindControl("lblvin");
                HyperLink hlnkVIN = (HyperLink)e.Row.FindControl("hlnkVIN");
                HyperLink hlnkVendor = (HyperLink)e.Row.FindControl("hlnkVendor");
                //HtmlAnchor ancdetail = (HtmlAnchor)e.Row.FindControl("ancdetail");

                ImageButton imgDel = (ImageButton)e.Row.FindControl("ibtnDelete");
                ImageButton imgCar = (ImageButton)e.Row.FindControl("ibtncars");
                ImageButton imgInv = (ImageButton)e.Row.FindControl("ibtnInv");
                ImageButton ibtnDuplicate = (ImageButton)e.Row.FindControl("ibtnDuplicate");
                ImageButton imgappr = (ImageButton)e.Row.FindControl("ibtnappr");
                ImageButton imgPending = (ImageButton)e.Row.FindControl("ibtnPending");
                ImageButton imgInfo = (ImageButton)e.Row.FindControl("ibtnInfo");

                HiddenField hfExpID = (HiddenField)e.Row.FindControl("hfExpID");
                HiddenField hfInvCount = (HiddenField)e.Row.FindControl("hfInvCount");
                HiddenField hfInvID = (HiddenField)e.Row.FindControl("hfInvID");
                HiddenField hfPending = (HiddenField)e.Row.FindControl("hfpending");
                HiddenField hfRejected = (HiddenField)e.Row.FindControl("hfRejected");
                HiddenField hfDuplicateExp = (HiddenField)e.Row.FindControl("hfDuplicateExp");
                HiddenField hfNoInventoryFound = (HiddenField)e.Row.FindControl("hfNoInventoryID");
                HiddenField hfIsEmailSent = (HiddenField)e.Row.FindControl("hfIsEmailSent");
                ImageButton imgEmailStatus = (ImageButton)e.Row.FindControl("imgEmailStatus");
                HiddenField hfIsDriverEmailSent = (HiddenField)e.Row.FindControl("hfIsDriverEmailSent");
                ImageButton imgDriverEmailStatus = (ImageButton)e.Row.FindControl("imgDriverEmailStatus");
                HiddenField hfIsDriverSMSSent = (HiddenField)e.Row.FindControl("hfIsDriverSMSSent");
                ImageButton imgDriverSMSStatus = (ImageButton)e.Row.FindControl("imgDriverSMSStatus");
                ImageButton ibtnExpenseDetail = (ImageButton)e.Row.FindControl("ibtnExpenseDetail");
                Label lblCode = (Label)e.Row.FindControl("lblCode");
                HyperLink hlnkCode = (HyperLink)e.Row.FindControl("hlnkCode");
                ImageButton ibtnEdit = (ImageButton)e.Row.FindControl("ibtnEdit");

                //ibtnEdit.OnClientClick = String.Format("ShowEditPreExpensePopup('{0}');return false;",e.Row.RowIndex);

                //lblCode.Text = "Code: " + hfInvID.Value;
                hlnkCode.Text = hfInvID.Value;
                hlnkCode.NavigateUrl = String.Format("InventoryDetail.aspx?Mode=View&Code={0}", hfInvID.Value);
                if (Convert.ToInt64(hfExpID.Value) > 0) //IF Expense is Aprroved
                {
                    chk.Visible = false;
                    imgDel.Visible = false;
                    e.Row.BackColor = System.Drawing.Color.LightCyan;
                    imgappr.Visible = true;
                    imgappr.Enabled = false;
                    imgInv.Visible = false;
                    imgPending.Visible = false;
                    lblvin.Visible = false;
                    hlnkVIN.Visible = true;
                    //ancdetail.Visible = true;
                    ibtnExpenseDetail.Visible = true;
                    lblCode.Visible = true;
                    hlnkCode.Visible = true;

                }
                else if (Convert.ToInt64(hfExpID.Value) == 0) // IF Expense is not Approved
                {
                    if (Convert.ToInt64(hfInvID.Value) > 0) // IF InventoryID is there
                    {
                        chk.Visible = true;
                        imgInv.Visible = false;
                        lblvin.Visible = false;
                        hlnkVIN.Visible = true;
                        lblCode.Visible = true;
                        hlnkCode.Visible = true;
                    }
                    else if (Convert.ToInt64(hfInvID.Value) == 0)
                    {
                        if (Convert.ToInt32(hfInvCount.Value) > 1)
                        { imgInv.Visible = true; chk.Visible = false; }
                        else if (Convert.ToInt32(hfInvCount.Value) == 0)
                        { chk.Visible = false; imgInv.Visible = false; }
                        else if (Convert.ToInt32(hfInvCount.Value) == 1)
                        { chk.Visible = true; imgInv.Visible = false; }
                    }
                    ibtnEdit.Visible = true;
                }

                if (Convert.ToInt32(lblImageCount.Text) > 0)
                {
                    imgCar.Visible = true;
                    imgCar.ToolTip = String.Format("View {0} {1}", lblImageCount.Text, Convert.ToInt32(lblImageCount.Text) > 1 ? "Images" : "Image");
                }
                else
                    imgCar.Visible = false;

                if (hfDuplicateExp.Value == "0" || String.IsNullOrEmpty(hfDuplicateExp.Value))
                    ibtnDuplicate.Visible = false;
                else
                {
                    ibtnDuplicate.Visible = true;
                    chk.Visible = false;
                }

                if (Convert.ToBoolean(hfPending.Value) == true && Convert.ToBoolean(hfRejected.Value) == false)
                {
                    imgDel.Visible = true;
                    imgPending.Visible = false;
                }

                if (Convert.ToBoolean(hfPending.Value) == true && Convert.ToBoolean(hfRejected.Value) == true) //Rejectd
                {
                    imgPending.Visible = true;
                    imgDel.Visible = false;
                    imgInv.Visible = false;
                }

                // Show buyer email icon
                //if ((!String.IsNullOrEmpty(hfInvID.Value) && hfInvID.Value != "0") && (!String.IsNullOrEmpty(hfExpID.Value) && hfExpID.Value != "0"))
                //{
                imgEmailStatus.Visible = true;

                if (Convert.ToBoolean(hfIsEmailSent.Value) == true)
                {
                    imgEmailStatus.ImageUrl = "~/Images/Email_Sent.png";
                    imgEmailStatus.ToolTip = "Buyer Email Sent";
                }

                else if (Convert.ToBoolean(hfIsEmailSent.Value) == false)
                {
                    imgEmailStatus.ImageUrl = "~/Images/Email_NotSent.png";
                    imgEmailStatus.ToolTip = "Buyer Email Not Sent";

                }
                //}
                //else
                //imgEmailStatus.Visible = false;

                // Show driver email icon
                if (Convert.ToBoolean(hfIsDriverEmailSent.Value) == true)
                {
                    imgDriverEmailStatus.ImageUrl = "~/Images/driveremailsent.png";
                    imgDriverEmailStatus.ToolTip = "Driver Email Sent";
                }

                else if (Convert.ToBoolean(hfIsDriverEmailSent.Value) == false)
                {
                    imgDriverEmailStatus.ImageUrl = "~/Images/driveremailnotsent.png";
                    imgDriverEmailStatus.Enabled = false;
                    imgDriverEmailStatus.ToolTip = "Driver Email Not Sent";
                }

                if (Convert.ToBoolean(hfIsDriverSMSSent.Value) == true)
                {
                    imgDriverSMSStatus.ImageUrl = "~/Images/sms-sent.png";
                    imgDriverSMSStatus.ToolTip = "Driver SMS Status";
                }
                else if (Convert.ToBoolean(hfIsDriverSMSSent.Value) == false)
                {
                    imgDriverSMSStatus.ImageUrl = "~/Images/sms_not_sent.png";
                    imgDriverSMSStatus.ToolTip = "Driver SMS Not Sent";
                    imgDriverSMSStatus.Enabled = false;
                }

                ImageButton ibtnWO = (ImageButton)e.Row.FindControl("ibtnWO");
                HiddenField hfWOId = (HiddenField)e.Row.FindControl("hfWOId");

                if (!String.IsNullOrEmpty(hfWOId.Value))
                    ibtnWO.Visible = true;

                if (Convert.ToString(Session["LoginEntityTypeID"]) == "3")
                {
                    ibtnEdit.Visible = false;
                    imgDel.Visible = false;
                    hlnkVIN.Enabled = false;
                    chk.Visible = false;
                    imgappr.Enabled = false;
                    hlnkCode.Enabled = false;
                    imgInv.Visible = false;
                }
                else if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
                {
                    hlnkVendor.Enabled = false;
                    ibtnEdit.Visible = false;
                    imgDel.Visible = false;
                    hlnkVIN.Enabled = false;
                    chk.Visible = false;
                    imgappr.Enabled = false;
                    hlnkCode.Enabled = false;
                    imgInv.Visible = false;
                }

                #region[Old Code]
                /*
                if (!String.IsNullOrEmpty(hfExpID.Value))
                {
                    if (Convert.ToInt64(hfExpID.Value) > 0)
                    {
                        chk.Visible = false;
                        imgDel.Visible = false;
                        e.Row.BackColor = System.Drawing.Color.LightCyan;
                        imgappr.Visible = true;
                        imgappr.Enabled = false;
                        imgInv.Visible = false;
                        imgPending.Visible = false;
                        lblvin.Visible = false;
                        hlnkVIN.Visible = true;
                    }
                    else
                        imgPending.Visible = false;
                }
                else
                {
                    if (!String.IsNullOrEmpty(hfInvCount.Value))
                    {
                        if (Convert.ToInt32(hfInvCount.Value) > 1)
                        { imgInv.Visible = true; chk.Visible = false; }
                        else if (Convert.ToInt32(hfInvCount.Value) == 0)
                        { chk.Visible = false; imgInv.Visible = false; }
                        else if (Convert.ToInt32(hfInvCount.Value) == 1)
                        { chk.Visible = true; imgInv.Visible = false; }
                            
                    }
                }

                if (Convert.ToInt32(lblImageCount.Text) > 0)
                {
                    imgCar.Visible = true;
                    imgCar.ToolTip = String.Format("View {0} {1}", lblImageCount.Text, Convert.ToInt32(lblImageCount.Text) > 1 ? "Images" : "Image");
                }
                else
                    imgCar.Visible = false;

                if (!String.IsNullOrEmpty(hfInvID.Value))
                {
                    if (Convert.ToInt64(hfInvID.Value) > 0)
                        ancdetail.Visible = true;
                    else
                        ancdetail.Visible = false;
                }

                if (hfDuplicateExp.Value == "0" || String.IsNullOrEmpty(hfDuplicateExp.Value))
                    ibtnDuplicate.Visible = false;
                else
                {
                    ibtnDuplicate.Visible = true;
                    chk.Visible = false;
                }

                if (Convert.ToBoolean(hfPending.Value) == true && Convert.ToBoolean(hfRejected.Value) == false)
                {
                    imgDel.Visible = true;
                    imgPending.Visible = false;
                }

                if (Convert.ToBoolean(hfPending.Value) == true && Convert.ToBoolean(hfRejected.Value) == true) //Rejectd
                {
                    imgPending.Visible = true;
                    imgDel.Visible = false;
                    imgInv.Visible = false;
                }

                if (Convert.ToBoolean(hfIsEmailSent.Value) == true)
                {
                    imgEmailStatus.Src = "../Images/Email_Sent.png";
                    imgEmailStatus.Attributes.Add("title", "Email Sent");
                }

                else if (Convert.ToBoolean(hfIsEmailSent.Value) == false)
                {
                    imgEmailStatus.Src = "../Images/Email_NotSent.png";
                    imgEmailStatus.Attributes.Add("title", "Email Not Sent");
                }
                */
                #endregion

            }
        }
        #endregion

        #region[Images]
        protected void ibtncars_Click(object sender, ImageClickEventArgs e)
        {
            long PreExpID = Convert.ToInt64(gvExpenseList.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            frmImage.Attributes.Add("src", String.Format("ExpenseImageGallery.aspx?id={0}", PreExpID));
            mpeImages.Show();
        }
        #endregion

        #region[Show Inventory]
        protected void ibtnInv_Click(object sender, ImageClickEventArgs e)
        {
            long PreExpID = Convert.ToInt64(gvExpenseList.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            ImageButton imginv = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imginv.NamingContainer;
            HiddenField hfvin = (HiddenField)row.FindControl("hfvin");
            hfVINForSave.Value = hfvin.Value;
            hfExpIDForSave.Value = PreExpID.ToString();

            #region[Old Code]
            //ucExpInv.VIN = hfvin.Value;

            // ctrlInventores;
            //ctrlInventores.LoadControl("~/UserControls/InventorySelectionForExpense.ascx");

            //InventorySelectionCtrl UC1 = (InventorySelectionCtrl)LoadControl("~/UserControls/InventorySelectionForExpense.ascx");
            //UC1.VIN = hfvin.Value;
            //phcExpInv.Controls.Add(UC1);
            #endregion

            grvinventory.DataSource = PreExpBAL.GetInvForExpenses(hfvin.Value);
            grvinventory.DataBind();
            lblVINInvLinking.Text = "[" + hfvin.Value + "]";
            mpexpinv.Show();

        }
        #endregion

        #region[Inventory linking - Approve]
        protected void btnSubmitInv_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PreExpenseID");
            dt.Columns.Add("VIN");
            dt.Columns.Add("InventoryID");

            foreach (GridViewRow grvrow in grvinventory.Rows)
            {
                RadioButton rdbtn = (RadioButton)grvrow.FindControl("rdselect");
                if (rdbtn != null && rdbtn.Checked)
                {
                    String InvID = Convert.ToString(grvinventory.DataKeys[grvrow.RowIndex].Value);

                    DataRow row = dt.NewRow();
                    row["PreExpenseID"] = hfExpIDForSave.Value;
                    row["VIN"] = hfVINForSave.Value;
                    row["InventoryID"] = InvID;
                    dt.Rows.Add(row);
                    break;
                }
            }

            if (dt.Rows.Count > 0)
                PreExpBAL.ApproveExpense(Convert.ToInt64(Session["empId"]), Convert.ToInt32(AppEnum.ExpenseLog.ExpenseApproved), dt, txtapprnote2.Text);
            txtapprnote2.Text = String.Empty;
            BindGrid();

        }
        #endregion

        #region[Duplicate Expense]
        protected void ibtnDuplicate_Click(object sender, ImageClickEventArgs e)
        {
            mpeDuplicateExp.Show();
            PreExpenseBAL bal = new PreExpenseBAL();
            long PreExpID = Convert.ToInt64(gvExpenseList.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            ImageButton ibtn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)ibtn.NamingContainer;
            HiddenField hfvin = (HiddenField)row.FindControl("hfvin");
            HiddenField hfExpenseTypeID = (HiddenField)row.FindControl("hfExpenseTypeID");
            HiddenField hfTotalPrice = (HiddenField)row.FindControl("hfTotalPrice");
            HiddenField hfEntityId = (HiddenField)row.FindControl("hfEntityId");
            HiddenField hfEntityTypeId = (HiddenField)row.FindControl("hfEntityTypeId");
            hdnVIN.Value = hfvin.Value;
            hdnExpTypeID.Value = hfExpenseTypeID.Value;
            hdnExpAmount.Value = hfTotalPrice.Value;
            hdnEntityID.Value = hfEntityId.Value;
            hdnEntityTypeID.Value = hfEntityTypeId.Value;
            BindDuplicatePreExpenses();
        }

        private void BindDuplicateExpenses()
        {
            gvDuplicateExpenses.DataSource = PreExpBAL.GetDuplicateExpense(hdnVIN.Value, Convert.ToInt32(hdnExpTypeID.Value), Convert.ToDecimal(hdnExpAmount.Value), Convert.ToInt32(hdnEntityID.Value), Convert.ToInt32(hdnEntityTypeID.Value), DuplicatePeriod);
            gvDuplicateExpenses.DataBind();
        }

        protected void gvDuplicateExpenses_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            mpeDuplicateExp.Show();
            this.gvDuplicateExpenses.PageIndex = e.NewPageIndex;
            BindDuplicateExpenses();
        }

        private void BindDuplicatePreExpenses()
        {
            gvDuplicatePreExpenses.DataSource = PreExpBAL.GetDuplicatePreExpense(hdnVIN.Value, Convert.ToInt32(hdnExpTypeID.Value), Convert.ToDecimal(hdnExpAmount.Value), Convert.ToInt32(hdnEntityID.Value), Convert.ToInt32(hdnEntityTypeID.Value), DuplicatePeriod);
            gvDuplicatePreExpenses.DataBind();
        }

        protected void gvDuplicatePreExpenses_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            mpeDuplicateExp.Show();
            this.gvDuplicatePreExpenses.PageIndex = e.NewPageIndex;
            BindDuplicatePreExpenses();
        }

        #region[Radio to show duplicate inventory and preinventory]
        protected void rbtnlstDuplicateVIN_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpeDuplicateExp.Show();
            if (rbtnlstDuplicateVIN.SelectedValue == "E")
            {
                divDuplicatePreExpenses.Visible = false;
                divDuplicateExpenses.Visible = true;
                BindDuplicateExpenses();
            }
            else if (rbtnlstDuplicateVIN.SelectedValue == "P")
            {
                divDuplicatePreExpenses.Visible = true;
                divDuplicateExpenses.Visible = false;
                BindDuplicatePreExpenses();
            }
        }
        #endregion
        #endregion

        #region[Inventory linking - paging]
        protected void grvinventory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            mpexpinv.Show();
            grvinventory.PageIndex = e.NewPageIndex;
            grvinventory.DataSource = PreExpBAL.GetInvForExpenses(hfVINForSave.Value);
            grvinventory.DataBind();
        }
        #endregion

        #region[Search button event handler]
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvExpenseList.PageIndex = 0;
            BindGrid();

        }
        #endregion

        #region[Bind Added By Dropdown]
        protected void BindAddedByddl()
        {
            String ParentBuyerID = "-1";
            if (Convert.ToString(Session["BuyerAccessLevel"]) != "1")
                ParentBuyerID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString();

            ddlAddedBy.DataSource = PreExpBAL.GetPreExpUsers_AddedBy(Convert.ToInt32(Session["UserEntityID"]), Convert.ToInt32(ParentBuyerID), Convert.ToInt32(Session["LoginEntityTypeID"]), Constant.OrgID);

            ddlAddedBy.DataTextField = "DisplayName";
            ddlAddedBy.DataValueField = "SecurityUserID";
            ddlAddedBy.DataBind();
            ddlAddedBy.Items.Insert(0, new ListItem("ALL", "-1"));

            if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
            {
                if (ddlAddedBy.Items.FindByValue(Convert.ToString(Session["empId"])) != null && ParentBuyerID == "-1" && ddlAddedBy.Items.Count == 2)
                {
                    ddlAddedBy.SelectedValue = Convert.ToString(Session["empId"]);
                    ddlAddedBy.Enabled = false;
                }
            }
        }
        #endregion

        #region[Bind Vendor Dropdown]
        protected void BindVendors()
        {
            String ParentBuyerID = "-1";
            if (Convert.ToString(Session["BuyerAccessLevel"]) != "1")
                ParentBuyerID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString();

            if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
                ddlVendor.DataSource = PreExpBAL.GetPreExpVendors_AddedVendor_ByBuyer(Convert.ToInt32(Session["UserEntityID"]), Convert.ToInt32(ParentBuyerID), Convert.ToInt32(Session["LoginEntityTypeID"]),Constant.OrgID);
            else if (Convert.ToString(Session["LoginEntityTypeID"]) == "3")
                ddlVendor.DataSource = PreExpBAL.GetPreExpVendors_AddedVendor(Convert.ToInt32(Session["empId"]), Constant.OrgID);
            else
                ddlVendor.DataSource = PreExpBAL.GetPreExpVendors(Constant.OrgID);
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
            ddlSort1.DataSource = PreExpBAL.PreExpense_SortOptions("Select SortText,SortValue from Lookup_Sort where tableid=5 order by Sequence asc", "", "");
            ddlSort1.DataTextField = "SortText";
            ddlSort1.DataValueField = "SortValue";
            ddlSort1.DataBind();

            ddlSort1.Items.Insert(0, new ListItem("", "-1"));

            BindSort2DDL();
            BindSort3DDL();
        }

        private void BindSort2DDL()
        {
            ddlSort2.DataSource = PreExpBAL.PreExpense_SortOptions(String.Format("Select SortText,SortValue from Lookup_Sort where tableid=5 AND SortValue NOT IN ('{0}') order by Sequence asc", ddlSort1.SelectedValue), "", "");
            ddlSort2.DataTextField = "SortText";
            ddlSort2.DataValueField = "SortValue";
            ddlSort2.DataBind();

            ddlSort2.Items.Insert(0, new ListItem("", "-1"));

            BindSort3DDL();
        }

        private void BindSort3DDL()
        {
            ddlSort3.DataSource = PreExpBAL.PreExpense_SortOptions(String.Format("Select SortText,SortValue from Lookup_Sort where tableid=5 AND SortValue NOT IN ('{0}', '{1}') order by Sequence asc", ddlSort1.SelectedValue, ddlSort2.SelectedValue), "", "");
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

        protected void gvExpenseList_Sorting(object sender, GridViewSortEventArgs e)
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

        #region[PreInventory Pending]
        protected void ibtnPending_Click(object sender, ImageClickEventArgs e)
        {
            long preExpID = Convert.ToInt64(gvExpenseList.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            ImageButton imgPending = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgPending.NamingContainer;
            HiddenField hfPending = (HiddenField)row.FindControl("hfpending");
            PreExpBAL.PreExpense_MakePending(preExpID, Convert.ToInt64(Session["empId"]));
            BindGrid();
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

        #region[Format description]
        public string FormatDescription(object oDescription)
        {
            String Description = Convert.ToString(oDescription);
            string strDescription = String.Empty;
            if (!string.IsNullOrEmpty(Description))
            {
                if (Description.Length > 25)
                    strDescription = Description.Substring(0, 24) + "...";
                else
                    strDescription = Description;
            }
            return strDescription;
        }
        #endregion

        #region[Email & SMS Popup]
        protected void imgEmailStatus_Click(object sender, ImageClickEventArgs e)
        {
            long PreExpID = Convert.ToInt64(gvExpenseList.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            //ifrmemail.Attributes.Add("src", String.Format("EmailDetails.aspx?type={0}&id={1}", 2010,PreExpID));
            // mpeEmail.Show();
        }

        protected void imgDriverEmailStatus_Click(object sender, ImageClickEventArgs e)
        {
            long PreExpID = Convert.ToInt64(gvExpenseList.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            //ifrmemail.Attributes.Add("src", String.Format("EmailDetails.aspx?type={0}&id={1}", 2020, PreExpID));
            // mpeEmail.Show();
        }

        //protected void imgDriverSMSStatus_Click(object sender, ImageClickEventArgs e)
        //{
        //    long PreExpID = Convert.ToInt64(gvExpenseList.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
        //     ifrmsms.Attributes.Add("src", String.Format("SMSDetail.aspx?type={0}&id={1}", 2021, PreExpID));
        //     mpesms.Show();
        //}
        #endregion

        #region[Pre-Expense Details]
        protected void ibtnExpenseDetail_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgExp = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgExp.NamingContainer;
            HiddenField hfExpID = (HiddenField)row.FindControl("hfExpID");
            BindPreExpenseDetail(Convert.ToInt64(hfExpID.Value));
            // mpePreExpDetail.Show();
        }
        #endregion

        protected void BindPreExpenseDetail(long ExpID)
        {
            PreExpenseBAL bal = new PreExpenseBAL();
            long PreExpID = bal.PreExp_ByExpID(ExpID);
            // gvPreExpDetail.DataSource = bal.PreExpenseDetail_ByPreExpenseId(PreExpID);
            // gvPreExpDetail.DataBind();
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            PreExpenseBAL bal = new PreExpenseBAL();
            if (!String.IsNullOrEmpty(hfEditPreExpID.Value))
            {
                Int64 PreExpenseID = Convert.ToInt64(hfEditPreExpID.Value);
                Int32 Count = Convert.ToInt32(hfddlCount.Value);
                Decimal DefaultPrice = String.IsNullOrEmpty(txtdefaultprice.Text) ? 0 : Convert.ToDecimal(txtdefaultprice.Text);
                Decimal TotalPrice = String.IsNullOrEmpty(txttotalprice.Text) ? 0 : Convert.ToDecimal(txttotalprice.Text);

                bal.EditPreExpense(PreExpenseID, Count, DefaultPrice, TotalPrice, Convert.ToInt64(Session["empId"]));
                BindGrid();
            }
        }

        #region[WorkOrder details]
        protected void ibtnWO_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgWO = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgWO.NamingContainer;
            HiddenField hfWorkOrderID = (HiddenField)row.FindControl("hfWOId");
            long WorkOrderID = Convert.ToInt64(hfWorkOrderID.Value);
            WorkOrderBAL woBAL = new WorkOrderBAL();
            gvExpenseDetail.DataSource = MakeExpenseGroup(woBAL.FillWorkOrderExpenses(WorkOrderID));
            gvExpenseDetail.DataBind();
            mpeWODetails.Show();
        }

        protected void gvExpenseDetail_RowDataBound(object sender, GridViewRowEventArgs e)
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

        [WebMethod]
        public static PreExpenseMinMax[] GetPreExpenseMinMaxCount(long PreExpenseID)
        {
            PreExpenseBAL bal = new PreExpenseBAL();
            List<Mobile_GetExpenseMinMaxCountResult> result = new List<Mobile_GetExpenseMinMaxCountResult>();
            List<PreExpenseMinMax> Count = new List<PreExpenseMinMax>();

            result = bal.GetExpenseMinMaxCount(PreExpenseID);
            foreach (var r in result)
            {
                PreExpenseMinMax p = new PreExpenseMinMax();
                p.MinCount = Convert.ToInt32(r.MinCount);
                p.MaxCount = Convert.ToInt32(r.MaxCount);
                Count.Add(p);
            }

            return Count.ToArray();
        }


        /// Added By Rupendra 7 Aug 2012 to show email popups using Jquery.//////

        [WebMethod]
        public static EmailDetailPopup[] EmailDetails(long PreExpenseID)
        {
            PreExpenseBAL bal = new PreExpenseBAL();

            List<Mobile_GetEmailDetailResult> result = new List<Mobile_GetEmailDetailResult>();
            List<EmailDetailPopup> Count = new List<EmailDetailPopup>();
            //result = bal.GetEmailDetails(2010, PreExpenseID);
            result = bal.GetEmailDetails(5005, PreExpenseID);
            EmailDetailPopup p = new EmailDetailPopup();

            foreach (var r in result)
            {
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

        [WebMethod]
        public static DriverEmailPopup[] DriverEmailStatus(long PreExpenseID)
        {
            PreExpenseBAL bal = new PreExpenseBAL();
            List<Mobile_GetEmailDetailResult> result = new List<Mobile_GetEmailDetailResult>();
            List<DriverEmailPopup> Count = new List<DriverEmailPopup>();
            result = bal.GetEmailDetails(2020, PreExpenseID);
            DriverEmailPopup p = new DriverEmailPopup();
            foreach (var r in result)
            {
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
                preExpense.ApprovedBy = row.AddedBy;
                preExpense.TotalPrice = row.TotalPrice.ToString();
                preExpense.AddedBy = row.EntityName;
                preExpense.Description = row.Description;
                preExpense.ApprovalNote = row.ApprovalNote;
                preExpense.DeviceName = row.DeviceName;
                Count.Add(preExpense);
            }

            return Count.ToArray();

        }

        [WebMethod]
        public static DriverSMSPopup[] DriverSMSDetail(long PreExpenseID)
        {
            PreExpenseBAL bal = new PreExpenseBAL();
            List<Mobile_GetEmailDetailResult> result = new List<Mobile_GetEmailDetailResult>();
            List<DriverSMSPopup> Count = new List<DriverSMSPopup>();
            result = bal.GetEmailDetails(2021, PreExpenseID);
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

        #region[Format Latitude & Longitude Tooltip]
        public string FormatLatLongToolTip(object Latitude, object Longitude)
        {
            return String.Format("Latitude: {0}\nLongitude: {1}", Latitude, Longitude);
        }
        #endregion

        #region[Format Latitude & Longitude Address]
        public string FormatLatLongAddress(object Address, object Latitude, object Longitude)
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Address)))
                return Convert.ToString(Address);
            else
                return String.Format("{0}<br/>{1}", Latitude, Longitude);
        }
        #endregion

        #region[Format Expense]
        public string FormatExpense(object ExpenseType, object Zone, object Distance)
        {
            if (Convert.ToString(ExpenseType).ToLower() == "Transportation".ToLower())
                return String.Format("{0}\n({1} M / {2})", ExpenseType, Distance, Zone);
            else
                return Convert.ToString(ExpenseType);
        }
        #endregion




    }

    public class PreExpenseMinMax
    {
        public int MinCount { get; set; }
        public int MaxCount { get; set; }
    }


    public class EmailDetailPopup
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

    public class PreExpenseDetailsPopup
    {
        public string VIN { get; set; }
        public string ExpenseDate { get; set; }
        public string Count { get; set; }
        public string SyncDate { get; set; }
        public string DefaultPrice { get; set; }
        public string ApprovedBy { get; set; }
        public string TotalPrice { get; set; }
        public string AddedBy { get; set; }
        public string Description { get; set; }
        public string DeviceName { get; set; }
        public string ApprovalNote { get; set; }
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
}
