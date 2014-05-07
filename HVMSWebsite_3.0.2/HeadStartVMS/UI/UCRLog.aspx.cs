using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Web.Security;

namespace METAOPTION.UI
{
    public partial class UCRLog : System.Web.UI.Page
    {
        UCRBAL objUCRBAL = new UCRBAL();
        String sort = String.Empty;

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
            if (!Page.IsPostBack)
                BindGrid();
        }

        #region[Bind grid]
        protected void BindGrid()
        {
            try
            {
                String SyncDateFrom, SyncDateTo;
                string strCRID;
                if (String.IsNullOrEmpty(txtDateFrom.Text))
                    SyncDateFrom = DateTime.Today.AddYears(-100).ToShortDateString();
                else
                    SyncDateFrom = txtDateFrom.Text;

                if (String.IsNullOrEmpty(txtDateTo.Text))
                    SyncDateTo = DateTime.Today.AddDays(1).ToShortDateString();
                else
                    SyncDateTo = Convert.ToDateTime(txtDateTo.Text).AddDays(1).ToShortDateString();

                // ddlPageSize1.SelectedValue = ddlPageSize2.SelectedValue;
                gvUCRDetailsList.PageSize = Convert.ToInt32(ddlPageSize1.SelectedValue);


                //if (ddlSort1.SelectedValue != "-1")
                //    sort = String.Format("{0} {1}", ddlSort1.SelectedValue, rbtnSort1Direction.SelectedValue);

                //if (ddlSort2.SelectedValue != "-1")
                //    sort += String.Format("{0} {1}"
                //        , sort == "" ? ddlSort2.SelectedValue : ", " + ddlSort2.SelectedValue
                //        , rbtnSort2Direction.SelectedValue);

                //if (ddlSort3.SelectedValue != "-1")
                //    sort += String.Format("{0} {1}"
                //        , sort == "" ? ddlSort3.SelectedValue : ", " + ddlSort3.SelectedValue
                //        , rbtnSort3Direction.SelectedValue);

                if (String.IsNullOrEmpty(sort))
                    sort = "UCRLogId desc";

                if (!string.IsNullOrEmpty(txtCRID.Text.Trim()))
                    strCRID = txtCRID.Text;
                else
                    strCRID = "-1";

                ObjectDataSource odsUCRLog = new ObjectDataSource();
                odsUCRLog.Selected += new ObjectDataSourceStatusEventHandler(odsExpense_Selected);
                odsUCRLog.TypeName = "METAOPTION.UCRBAL";
                odsUCRLog.SelectMethod = "GetUCRDetails";
                odsUCRLog.SelectCountMethod = "GetUCRDetailsCount";
                odsUCRLog.EnablePaging = true;
                odsUCRLog.SelectParameters.Add("CRID", strCRID);
                odsUCRLog.SelectParameters.Add("DateFrom", SyncDateFrom);
                odsUCRLog.SelectParameters.Add("DateTo", SyncDateTo);
                odsUCRLog.SelectParameters.Add("StartRowIndex", DbType.Int32, gvUCRDetailsList.PageIndex.ToString());
                odsUCRLog.SelectParameters.Add("MaximumRows", DbType.Int32, ddlPageSize1.SelectedValue);
                odsUCRLog.SelectParameters.Add("SortExpression", sort);
                gvUCRDetailsList.DataSource = odsUCRLog;
                gvUCRDetailsList.DataBind();
            }
            catch (Exception ex) { }
        }
        #endregion

        #region [RowDataBound Event]
        protected void gvUCRDetailsList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ImageButton btnUCRPages = (ImageButton)e.Row.FindControl("btnUCRPages");
                    // HiddenField hdnUCRDetails = (HiddenField)e.Row.FindControl("hdnUCRDetails");
                    // btnUCRDetail.ToolTip = hdnUCRDetails.Value;

                    Int64 objUCRLOgID = (Int64)gvUCRDetailsList.DataKeys[e.Row.RowIndex].Value;
                    if (objUCRLOgID > 0)
                    {
                        DataTable dtUCRPageCount = new DataTable();
                        dtUCRPageCount = objUCRBAL.GetUCRLogPageDetails(objUCRLOgID);
                        if (dtUCRPageCount.Rows.Count > 0)
                            btnUCRPages.Visible = true;
                        else
                            btnUCRPages.Visible = false;
                    }
                    ImageButton imgbtnSelect = (ImageButton)e.Row.FindControl("imgbtnSelect");
                    HiddenField hdnRecord = (HiddenField)e.Row.FindControl("hdnRecord");
                    if (!string.IsNullOrEmpty(hdnRecord.Value) && (hdnRecord.Value != "0"))
                        imgbtnSelect.Visible = true;
                    else
                        imgbtnSelect.Visible = false;

                    HiddenField hdnErrorCode = (HiddenField)e.Row.FindControl("hdnErrorCode");
                    HiddenField hdnErrormsg = (HiddenField)e.Row.FindControl("hdnErrormsg");
                    Label lblTransactionStatus = (Label)e.Row.FindControl("lblTransactionStatus");
                    if (!string.IsNullOrEmpty(hdnErrorCode.Value))
                        lblTransactionStatus.ToolTip = "Err Code : " + hdnErrorCode.Value + Environment.NewLine;
                    if (!string.IsNullOrEmpty(hdnErrormsg.Value))
                        lblTransactionStatus.ToolTip += "Err Message : " + hdnErrormsg.Value;


                }
            }
            catch (Exception ex) { }
        }
        #endregion


        protected void odsExpense_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue == null) return;
            if (e.ReturnValue.GetType().Name == "Int32")
            {

                Int32 count = Convert.ToInt32(e.ReturnValue);
                Int32 pagesize = gvUCRDetailsList.PageSize;
                Int32 pagecount = count / pagesize;
                if (count < pagesize)
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} of {0} records", count);
                }
                else
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} through {1} of {2} records"
                         , String.Format("{0:#,###}", pagesize * gvUCRDetailsList.PageIndex + 1)
                         , String.Format("{0:#,###}", pagesize * (gvUCRDetailsList.PageIndex + 1) > count ? count : pagesize * (gvUCRDetailsList.PageIndex + 1))
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

                    ddlPaging.SelectedValue = String.Format("{0}", gvUCRDetailsList.PageIndex + 1);
                    ddlPaging1.SelectedValue = String.Format("{0}", gvUCRDetailsList.PageIndex + 1);
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
            gvUCRDetailsList.PageIndex = 0;
            BindGrid();
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedIndex > 0)
                gvUCRDetailsList.PageIndex--;
            BindGrid();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedValue != ddlPaging.Items.Count.ToString())
                gvUCRDetailsList.PageIndex++;
            BindGrid();
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            gvUCRDetailsList.PageIndex = ddlPaging.Items.Count - 1;
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

            gvUCRDetailsList.PageIndex = 0;
            BindGrid();
        }
        #endregion

        #region[Drop down page selection change]
        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                gvUCRDetailsList.PageIndex = Convert.ToInt32(((DropDownList)sender).SelectedValue) - 1;
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

        protected void gvUCRDetailsList_Sorting(object sender, GridViewSortEventArgs e)
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
            //if (ddlSort1.Items.FindByValue(sortExpression) != null)
            //{
            //    ddlSort1.SelectedValue = sortExpression;
            //    if (direction == " ASC")
            //        rbtnSort1Direction.SelectedIndex = 0;
            //    else
            //        rbtnSort1Direction.SelectedIndex = 1;
            //}

            //if (ddlSort2.Items.FindByValue(sortExpression) != null)
            //{
            //    ddlSort2.SelectedValue = "-1";
            //    rbtnSort2Direction.SelectedIndex = 0;
            //}

            //if (ddlSort3.Items.FindByValue(sortExpression) != null)
            //{
            //    ddlSort3.SelectedValue = "-1";
            //    rbtnSort3Direction.SelectedIndex = 0;
            //}

            BindGrid();
        }

        #endregion

        #region[Show Details using UCRLogId]
        protected void imgbtnSelect_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton img = (ImageButton)sender;
            GridViewRow gvRow = (GridViewRow)img.NamingContainer;
            int UCRLogId = Convert.ToInt32(this.gvUCRDetailsList.DataKeys[gvRow.RowIndex].Value);
            if (UCRLogId > 0)
                Response.Redirect("~/UI/UCRLogListing.aspx?UCRLogId=" + UCRLogId);
        }
        #endregion

        #region[UCR Pages Records]
        protected void btnUCRPages_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtUCRPage = new DataTable();
            ImageButton ibtnExpenseEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)ibtnExpenseEdit.NamingContainer;
            GridView gvUCRLogPages = row.FindControl("gvUCRLogPages") as GridView;
            HtmlTableRow tr = row.FindControl("trnew") as HtmlTableRow;

            long UCRID = Convert.ToInt64(gvUCRDetailsList.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            dtUCRPage = objUCRBAL.GetUCRLogPageDetails(UCRID);
            gvUCRLogPages.DataSource = dtUCRPage;
            gvUCRLogPages.DataBind();
            if (ibtnExpenseEdit.ImageUrl == "~/Images/expand.png")
            {
                ibtnExpenseEdit.ImageUrl = "~/Images/collapse.png";
                tr.Visible = true;
            }
            else
            {
                ibtnExpenseEdit.ImageUrl = "~/Images/expand.png";
                tr.Visible = false;
            }
        }
        #endregion

        #region[Show XML Details]
        protected void btnUCRPageDetails_Click(object sender, EventArgs e)
        {
            try
            {
                string str = string.Empty;
                string strUCRPageId = ((ImageButton)sender).CommandArgument;
                if (!string.IsNullOrEmpty(strUCRPageId))
                {
                    DataTable dtUCRPageDetaisl = new DataTable();
                    dtUCRPageDetaisl = objUCRBAL.GetUCRLogPageDetailsbyUCRPageId(Convert.ToInt64(strUCRPageId));
                    if (dtUCRPageDetaisl.Rows.Count > 0)
                    {
                        str = Convert.ToString(dtUCRPageDetaisl.Rows[0]["UCRPageResponse"]);
                        StringBuilder strxml = new StringBuilder();
                        strxml.AppendLine("<?xml version=\"1.0\" encoding=\"utf-16\" ?> ");
                        strxml.AppendLine("<ResponseCollection>");
                        strxml.AppendLine(str);
                        strxml.AppendLine("</ResponseCollection>");

                        if (!String.IsNullOrEmpty(strxml.ToString()))
                        {
                            string path = Server.MapPath("responsedata.xml");
                            if (File.Exists(path))
                                File.Delete(path);

                            byte[] binLogString = Encoding.Default.GetBytes(strxml.ToString());


                            System.IO.FileStream loFile = new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write, System.IO.FileShare.Write);
                            loFile.Seek(0, System.IO.SeekOrigin.End);
                            loFile.Write(binLogString, 0, binLogString.Length);
                            loFile.Close();

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Response", "ShowRequestXML();", true);

                        }
                    }
                }
            }
            catch (Exception ex) { }

        }
        #endregion

        #region[Seach Button]
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion
    }
}