using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using System.Web.Security;

namespace METAOPTION.UI
{
    public partial class UCRUpdateLog : System.Web.UI.Page
    {
        UCRBAL objUCRBAL = new UCRBAL();
        DataSet ds = new DataSet();
        DataTable dtYear = new DataTable();
        DataTable dtMake = new DataTable();
        DataTable dtModel = new DataTable();
        DataTable dtBody = new DataTable();

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
            {
                BindYearMakeModelBody();
                BindSort1DDL();
                BindGrid();
            }
        }

        #region[Bind grid]
        protected void BindGrid()
        {
            try
            {
                string strCRID, Reference;
                String SyncDateFrom, SyncDateTo, strYear, strMake, strModel, strBody, strVIN;

                if (String.IsNullOrEmpty(txtSyncDateFrom.Text))
                    SyncDateFrom = DateTime.Today.AddYears(-100).ToShortDateString();
                else
                    SyncDateFrom = txtSyncDateFrom.Text;

                if (String.IsNullOrEmpty(txtSyncDateTo.Text))
                    SyncDateTo = DateTime.Today.AddDays(1).ToShortDateString();
                else
                    SyncDateTo = Convert.ToDateTime(txtSyncDateTo.Text).AddDays(1).ToShortDateString();

                gvUpdateLog.PageSize = Convert.ToInt32(ddlPageSize1.SelectedValue);

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
                    sort = "UCRUpdateId desc";


                if (!string.IsNullOrEmpty(txtCRID.Text.Trim()))
                    strCRID = txtCRID.Text;
                else if (Request.QueryString["CRId"] != null)
                    strCRID = Request.QueryString["CRId"];
                else
                    strCRID = "-1";


                if (!string.IsNullOrEmpty(txtRefrence.Text.Trim()))
                    Reference = txtRefrence.Text;
                else
                    Reference = "";

                if (!string.IsNullOrEmpty(txtVin.Text.Trim()))
                    strVIN = txtVin.Text;
                else
                    strVIN = "";


                if (ddlYear.SelectedValue == "-1")
                    strYear = "-1";
                else
                    strYear = ddlYear.SelectedItem.Text;
                if (ddlMake.SelectedValue == "-1")
                    strMake = "-1";
                else
                    strMake = ddlMake.SelectedValue;
                if (ddlModel.SelectedValue == "-1")
                    strModel = "-1";
                else
                    strModel = ddlModel.SelectedValue;
                if (ddlBody.SelectedValue == "-1")
                    strBody = "-1";
                else
                    strBody = ddlBody.SelectedValue;



                ObjectDataSource odsUCRUpdate = new ObjectDataSource();
                odsUCRUpdate.Selected += new ObjectDataSourceStatusEventHandler(odsUCRUpdate_Selected);
                odsUCRUpdate.TypeName = "METAOPTION.UCRBAL";
                odsUCRUpdate.SelectMethod = "GetUCRUpdateLog";
                odsUCRUpdate.SelectCountMethod = "GetUCRUpdateCount";
                odsUCRUpdate.EnablePaging = true;


                odsUCRUpdate.SelectParameters.Add("CRID", strCRID);
                odsUCRUpdate.SelectParameters.Add("VIN", strVIN);
                odsUCRUpdate.SelectParameters.Add("Year", DbType.Int32, strYear);
                odsUCRUpdate.SelectParameters.Add("Make", strMake);
                odsUCRUpdate.SelectParameters.Add("Model", strModel);
                odsUCRUpdate.SelectParameters.Add("Body", strBody);
                odsUCRUpdate.SelectParameters.Add("DateFrom", SyncDateFrom);
                odsUCRUpdate.SelectParameters.Add("DateTo", SyncDateTo);
                odsUCRUpdate.SelectParameters.Add("TranStatus", DbType.Int32, ddlTranStatus.SelectedValue);
                odsUCRUpdate.SelectParameters.Add("DataStatus", DbType.Int32, ddlDataStatus.SelectedValue);
                odsUCRUpdate.SelectParameters.Add("Reference", Reference);
                odsUCRUpdate.SelectParameters.Add("StartRowIndex", DbType.Int32, gvUpdateLog.PageIndex.ToString());
                odsUCRUpdate.SelectParameters.Add("MaximumRows", DbType.Int32, ddlPageSize1.SelectedValue);
                odsUCRUpdate.SelectParameters.Add("SortExpression", sort);
                gvUpdateLog.DataSource = odsUCRUpdate;
                gvUpdateLog.DataBind();
            }
            catch (Exception ex) { }
        }
        #endregion

        #region [Bind Year, Make, Model and Body]
        public void BindYearMakeModelBody()
        {
            try
            {
                ds = objUCRBAL.GetUCRUpdateLogYMMB();
                ds.Tables[0].TableName = "dtYear";
                ds.Tables[1].TableName = "dtMake";
                ds.Tables[2].TableName = "dtModel";
                ds.Tables[3].TableName = "dtBody";


                dtYear = ds.Tables["dtYear"];
                dtMake = ds.Tables["dtMake"];
                dtModel = ds.Tables["dtModel"];
                dtBody = ds.Tables["dtBody"];


                ddlYear.DataSource = dtYear;
                ddlYear.DataTextField = "Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("--Select--", "-1"));

                ddlMake.DataSource = dtMake;
                ddlMake.DataTextField = "Make";
                ddlMake.DataValueField = "MakeId";
                ddlMake.DataBind();
                ddlMake.Items.Insert(0, new ListItem("--Select--", "-1"));

                ddlModel.DataSource = dtModel;
                ddlModel.DataTextField = "Model";
                ddlModel.DataValueField = "ModelId";
                ddlModel.DataBind();
                ddlModel.Items.Insert(0, new ListItem("--Select--", "-1"));

                ddlBody.DataSource = dtBody;
                ddlBody.DataTextField = "Body";
                ddlBody.DataValueField = "BodyId";
                ddlBody.DataBind();
                ddlBody.Items.Insert(0, new ListItem("--Select--", "-1"));
            }
            catch (Exception ex) { }
        }
        #endregion

        #region [RowDataBound Event]
        protected void gvUpdateLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ImageButton btnUCRRequest = (ImageButton)e.Row.FindControl("btnUCRRequest");
                    ImageButton btnUCRResponse = (ImageButton)e.Row.FindControl("btnUCRResponse");
                    HiddenField hdnRequestCount = (HiddenField)e.Row.FindControl("hdnRequestCount");
                    HiddenField hdnResponseCount = (HiddenField)e.Row.FindControl("hdnResponseCount");
                    if ((!string.IsNullOrEmpty(hdnRequestCount.Value)) && (hdnRequestCount.Value != "0"))
                        btnUCRRequest.Visible = true;
                    else
                        btnUCRRequest.Visible = false;
                    if ((!string.IsNullOrEmpty(hdnResponseCount.Value)) && (hdnResponseCount.Value != "0"))
                        btnUCRResponse.Visible = true;
                    else
                        btnUCRResponse.Visible = false;
                }
            }
            catch (Exception ex) { }
        }
        #endregion

        #region[Gridview Selected Event]
        protected void odsUCRUpdate_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue == null) return;
            if (e.ReturnValue.GetType().Name == "Int32")
            {

                Int32 count = Convert.ToInt32(e.ReturnValue);
                Int32 pagesize = gvUpdateLog.PageSize;
                Int32 pagecount = count / pagesize;
                if (count < pagesize)
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} of {0} records", count);
                }
                else
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} through {1} of {2} records"
                         , String.Format("{0:#,###}", pagesize * gvUpdateLog.PageIndex + 1)
                         , String.Format("{0:#,###}", pagesize * (gvUpdateLog.PageIndex + 1) > count ? count : pagesize * (gvUpdateLog.PageIndex + 1))
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

                    ddlPaging.SelectedValue = String.Format("{0}", gvUpdateLog.PageIndex + 1);
                    ddlPaging1.SelectedValue = String.Format("{0}", gvUpdateLog.PageIndex + 1);
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

        protected void gvUpdateLog_Sorting(object sender, GridViewSortEventArgs e)
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

        #region[ Bind sort dropdown]
        private void BindSort1DDL()
        {
            try
            {
                ddlSort1.DataSource = objUCRBAL.UCRListing_SortOptions("Select SortText,SortValue from Lookup_Sort where tableid=9 order by Sequence asc", "", "");
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
                ddlSort2.DataSource = objUCRBAL.UCRListing_SortOptions(String.Format("Select SortText,SortValue from Lookup_Sort where tableid=9 AND SortValue NOT IN ('{0}') order by Sequence asc", ddlSort1.SelectedValue), "", "");
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
                ddlSort3.DataSource = objUCRBAL.UCRListing_SortOptions(String.Format("Select SortText,SortValue from Lookup_Sort where tableid=9 AND SortValue NOT IN ('{0}', '{1}') order by Sequence asc", ddlSort1.SelectedValue, ddlSort2.SelectedValue), "", "");
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
            gvUpdateLog.PageIndex = 0;
            BindGrid();
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedIndex > 0)
                gvUpdateLog.PageIndex--;
            BindGrid();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedValue != ddlPaging.Items.Count.ToString())
                gvUpdateLog.PageIndex++;
            BindGrid();
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            gvUpdateLog.PageIndex = ddlPaging.Items.Count - 1;
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

            gvUpdateLog.PageIndex = 0;
            BindGrid();
        }
        #endregion

        #region [Search Button]
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region[Drop down page selection change]
        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                gvUpdateLog.PageIndex = Convert.ToInt32(((DropDownList)sender).SelectedValue) - 1;
            BindGrid();
            EnablePaging();
        }
        #endregion

        #region[UCR Request,Response XML Details]
        protected void btnUCRRequest_Click(object sender, EventArgs e)
        {
            try
            {
                string str = string.Empty;
                string strUCRUpdateID = ((ImageButton)sender).CommandArgument;
                if (!string.IsNullOrEmpty(strUCRUpdateID))
                {
                    DataTable dtUCRResponseDetail = new DataTable();
                    dtUCRResponseDetail = objUCRBAL.GetUCRRequestByUCRUpdateID(Convert.ToInt64(strUCRUpdateID), 1);

                    if (dtUCRResponseDetail.Rows.Count > 0)
                    {
                        str = Convert.ToString(dtUCRResponseDetail.Rows[0]["UpdateXML"]);
                        StringBuilder strxml = new StringBuilder();
                        strxml.AppendLine("<?xml version=\"1.0\" encoding=\"utf-16\" ?> ");
                        //strxml.AppendLine("<Response>");
                        strxml.AppendLine(str);
                        //strxml.AppendLine("</Response>");

                        if (!String.IsNullOrEmpty(strxml.ToString()))
                        {
                            string path = Server.MapPath("UCRUpdateLodRequestData.xml");
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

        protected void btnUCRResponse_Click(object sender, EventArgs e)
        {
            try
            {
                string str = string.Empty;
                string strUCRUpdateID = ((ImageButton)sender).CommandArgument;
                if (!string.IsNullOrEmpty(strUCRUpdateID))
                {
                    DataTable dtUCRResponseDetail = new DataTable();
                    dtUCRResponseDetail = objUCRBAL.GetUCRRequestByUCRUpdateID(Convert.ToInt64(strUCRUpdateID), 2);

                    if (dtUCRResponseDetail.Rows.Count > 0)
                    {
                        str = Convert.ToString(dtUCRResponseDetail.Rows[0]["ResponseXML"]);
                        StringBuilder strxml = new StringBuilder();
                        strxml.AppendLine("<?xml version=\"1.0\" encoding=\"utf-16\" ?> ");
                        //strxml.AppendLine("<Response>");
                        strxml.AppendLine(str);
                       // strxml.AppendLine("</Response>");

                        if (!String.IsNullOrEmpty(strxml.ToString()))
                        {
                            string path = Server.MapPath("UCRUpdateLodResponseData.xml");
                            if (File.Exists(path))
                                File.Delete(path);

                            byte[] binLogString = Encoding.Default.GetBytes(strxml.ToString());


                            System.IO.FileStream loFile = new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write, System.IO.FileShare.Write);
                            loFile.Seek(0, System.IO.SeekOrigin.End);
                            loFile.Write(binLogString, 0, binLogString.Length);
                            loFile.Close();

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Response", "ShowResponseXML();", true);
                        }
                    }
                }
            }
            catch (Exception ex) { }

        }
        #endregion
    }
}