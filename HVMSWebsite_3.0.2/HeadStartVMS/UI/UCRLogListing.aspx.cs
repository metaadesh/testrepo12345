using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Configuration;
using METAOPTION.BAL;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using System.Text;
using System.Web.Security;

namespace METAOPTION.UI
{
    public partial class UCRLogListing : System.Web.UI.Page
    {
        UCRBAL objUCRBAL = new UCRBAL();
        DataSet ds = new DataSet();
        DataTable dtYear = new DataTable();
        DataTable dtMake = new DataTable();
        DataTable dtModel = new DataTable();
        DataTable dtBody = new DataTable();
        DataTable dtDisplayName = new DataTable();

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
                string strCRID, strLegInventoryId, strInventoryId, strYear, strMake, strModel, strBody;
                String SyncDateFrom, SyncDateTo, UCRLogId = "-1";

                if (String.IsNullOrEmpty(txtSyncDateFrom.Text))
                    SyncDateFrom = DateTime.Today.AddYears(-100).ToShortDateString();
                else
                    SyncDateFrom = txtSyncDateFrom.Text;

                if (String.IsNullOrEmpty(txtSyncDateTo.Text))
                    SyncDateTo = DateTime.Today.AddDays(1).ToShortDateString();
                else
                    SyncDateTo = Convert.ToDateTime(txtSyncDateTo.Text).AddDays(1).ToShortDateString();

                gvUCRListing.PageSize = Convert.ToInt32(ddlPageSize1.SelectedValue);

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
                    sort = "CRDateAdded desc,CRModifiedDate desc";

                ObjectDataSource odsUCRListing = new ObjectDataSource();
                odsUCRListing.Selected += new ObjectDataSourceStatusEventHandler(odsUCRListing_Selected);
                odsUCRListing.TypeName = "METAOPTION.UCRBAL";
                odsUCRListing.SelectMethod = "GetUCRListing";
                odsUCRListing.SelectCountMethod = "GetUCRListingCount";
                odsUCRListing.EnablePaging = true;

                if (!string.IsNullOrEmpty(txtCRID.Text.Trim()))
                    strCRID = txtCRID.Text;
                else
                    strCRID = "-1";
                if (!string.IsNullOrEmpty(txtLegInventoryId.Text.Trim()))
                    strLegInventoryId = txtLegInventoryId.Text;
                else
                    strLegInventoryId = "-1";
                if (!string.IsNullOrEmpty(txtInventoryId.Text.Trim()))
                    strInventoryId = txtInventoryId.Text;
                else
                    strInventoryId = "-1";
                if (ddlYear.SelectedValue == "-1")
                    strYear = "-1";
                else
                    strYear = ddlYear.SelectedItem.Text;
                if (ddlMake.SelectedValue == "-1")
                    strMake = "";
                else
                    strMake = ddlMake.SelectedItem.Text;
                if (ddlModel.SelectedValue == "-1")
                    strModel = "";
                else
                    strModel = ddlModel.SelectedItem.Text;
                if (ddlBody.SelectedValue == "-1")
                    strBody = "";
                else
                    strBody = ddlBody.SelectedItem.Text;

                if (Request.QueryString["UCRLogId"] != null)
                    UCRLogId = Convert.ToString(Request.QueryString["UCRLogId"]);

                string LinkedUCR = string.Empty;

                if (ddlUCRLinked.SelectedValue == "1")
                    LinkedUCR = "1";
                else if (ddlUCRLinked.SelectedValue == "0")
                    LinkedUCR = "0";
                else
                    LinkedUCR = "";

                string strAddedby = string.Empty;
                if (ddlAddedBy.SelectedItem.Text.ToLower() == "all")
                    strAddedby = "";
                else
                    strAddedby = ddlAddedBy.SelectedItem.Text;


                odsUCRListing.SelectParameters.Add("CRID", strCRID);
                odsUCRListing.SelectParameters.Add("UCRLogId", DbType.Int32, UCRLogId);
                odsUCRListing.SelectParameters.Add("VIN", String.IsNullOrEmpty(txtVINNumber.Text.Trim()) ? String.Empty : txtVINNumber.Text.Trim());
                odsUCRListing.SelectParameters.Add("LaneNo", String.IsNullOrEmpty(txtLaneNo.Text.Trim()) ? String.Empty : txtLaneNo.Text.Trim());
                odsUCRListing.SelectParameters.Add("RunNo", String.IsNullOrEmpty(txtRunNo.Text.Trim()) ? String.Empty : txtRunNo.Text.Trim());
                odsUCRListing.SelectParameters.Add("LegacyInventoryId", DbType.Int32, strLegInventoryId);
                odsUCRListing.SelectParameters.Add("InventoryId", DbType.Int32, strInventoryId);
                odsUCRListing.SelectParameters.Add("Year", DbType.Int32, strYear);
                odsUCRListing.SelectParameters.Add("Make", strMake);
                odsUCRListing.SelectParameters.Add("Model", strModel);
                odsUCRListing.SelectParameters.Add("Body", strBody);
                odsUCRListing.SelectParameters.Add("DateFrom", SyncDateFrom);
                odsUCRListing.SelectParameters.Add("DateTo", SyncDateTo);
                odsUCRListing.SelectParameters.Add("CRStatus", ddlUCR.SelectedValue);
                odsUCRListing.SelectParameters.Add("AddedBy", strAddedby);
                odsUCRListing.SelectParameters.Add("LinkedUCR", DbType.String, LinkedUCR);
                odsUCRListing.SelectParameters.Add("StartRowIndex", DbType.Int32, gvUCRListing.PageIndex.ToString());
                odsUCRListing.SelectParameters.Add("MaximumRows", DbType.Int32, ddlPageSize1.SelectedValue);
                odsUCRListing.SelectParameters.Add("SortExpression", sort);
                gvUCRListing.DataSource = odsUCRListing;
                gvUCRListing.DataBind();
            }
            catch (Exception ex) { }
        }
        #endregion

        #region [Bind Year, Make, Model and Body]
        public void BindYearMakeModelBody()
        {
            try
            {
                ds = objUCRBAL.GetYearMakeModelBody();
                ds.Tables[0].TableName = "dtYear";
                ds.Tables[1].TableName = "dtMake";
                ds.Tables[2].TableName = "dtModel";
                ds.Tables[3].TableName = "dtBody";
                ds.Tables[4].TableName = "dtDisplayName";

                dtYear = ds.Tables["dtYear"];
                dtMake = ds.Tables["dtMake"];
                dtModel = ds.Tables["dtModel"];
                dtBody = ds.Tables["dtBody"];
                dtDisplayName = ds.Tables["dtDisplayName"];

                ddlYear.DataSource = dtYear;
                ddlYear.DataTextField = "Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("--Select--", "-1"));

                ddlMake.DataSource = dtMake;
                ddlMake.DataTextField = "Make";
                ddlMake.DataValueField = "Make";
                ddlMake.DataBind();
                ddlMake.Items.Insert(0, new ListItem("--Select--", "-1"));

                ddlModel.DataSource = dtModel;
                ddlModel.DataTextField = "Model";
                ddlModel.DataValueField = "Model";
                ddlModel.DataBind();
                ddlModel.Items.Insert(0, new ListItem("--Select--", "-1"));

                ddlBody.DataSource = dtBody;
                ddlBody.DataTextField = "Body";
                ddlBody.DataValueField = "Body";
                ddlBody.DataBind();
                ddlBody.Items.Insert(0, new ListItem("--Select--", "-1"));

                ddlAddedBy.DataSource = dtDisplayName;
                ddlAddedBy.DataTextField = "DisplayName";
                ddlAddedBy.DataValueField = "DisplayName";
                ddlAddedBy.DataBind();
                ddlAddedBy.Items.Insert(0, new ListItem("ALL", "-1"));
            }
            catch (Exception ex) { }
        }
        #endregion

        #region [RowDataBound Event]
        protected void gvUCRListing_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ImageButton btnAudioVideo = (ImageButton)e.Row.FindControl("btnAudioVideo");
                    ImageButton btnUpdateHis = (ImageButton)e.Row.FindControl("btnUpdateHis");
                    HtmlAnchor btnUpdateHistory = (HtmlAnchor)e.Row.FindControl("btnUpdateHistory");
                    HiddenField hdnCRID = (HiddenField)e.Row.FindControl("hdnCRID");
                    HiddenField hdnImageCount = (HiddenField)e.Row.FindControl("hdnImageCount");
                    HiddenField hdnURL = (HiddenField)e.Row.FindControl("hdnURL");
                    HiddenField hfInvID = (HiddenField)e.Row.FindControl("hfInvID");
                    HiddenField hdnUCRUpdateCount = (HiddenField)e.Row.FindControl("hdnUCRUpdateCount");
                    Label lblCode = (Label)e.Row.FindControl("lblCode");
                    HyperLink hlnkCode = (HyperLink)e.Row.FindControl("hlnkCode");
                    HtmlAnchor ibtnCRAvailable = (HtmlAnchor)e.Row.FindControl("ibtnCRAvailable");
                    HtmlImage UCRImage = (HtmlImage)ibtnCRAvailable.FindControl("imgAvailabel");

                    hlnkCode.Text = hfInvID.Value;
                    hlnkCode.NavigateUrl = String.Format("InventoryDetail.aspx?Mode=View&Code={0}", hfInvID.Value);

                    if (!string.IsNullOrEmpty(hdnImageCount.Value))
                        if (Convert.ToInt32(hdnImageCount.Value) > 0)
                            btnAudioVideo.Visible = true;
                        else
                            btnAudioVideo.Visible = false;
                    else
                        btnAudioVideo.Visible = false;

                    if (Convert.ToInt64(hfInvID.Value) > 0)
                    {
                        lblCode.Visible = true;
                        hlnkCode.Visible = true;
                    }
                    else
                    {
                        lblCode.Visible = false;
                        hlnkCode.Visible = false;
                    }

                    // ImageButton ibtnCR = (ImageButton)e.Row.FindControl("btnUCRDetail");
                    HiddenField hdnUCRDetails = (HiddenField)e.Row.FindControl("hdnUCRDetails");

                    if (String.IsNullOrEmpty(hdnUCRDetails.Value) || hdnUCRDetails.Value == "0")
                    {

                        ibtnCRAvailable.Visible = true;
                        ibtnCRAvailable.Title = "Initiated";
                        ibtnCRAvailable.HRef = hdnURL.Value;
                        ibtnCRAvailable.Title = "Not Ready";
                        UCRImage.Src = "~/Images/ucr-btn1.png";

                        //ibtnCR.ImageUrl = "~/Images/ucr-btn1.png";
                        // ibtnCR.ToolTip = "Not Ready";
                    }
                    else if (hdnUCRDetails.Value == "10" || hdnUCRDetails.Value == "20")
                    {
                        ibtnCRAvailable.Visible = true;
                        ibtnCRAvailable.Title = "Initiated";
                        ibtnCRAvailable.HRef = hdnURL.Value;
                        ibtnCRAvailable.Title = "Initiated";
                        UCRImage.Src = "~/Images/ucr-btn2.png";
                        //ibtnCR.ImageUrl = "~/Images/ucr-btn2.png";
                        // ibtnCR.ToolTip = "Initiated";

                    }
                    else if (hdnUCRDetails.Value == "30")
                    {
                        ibtnCRAvailable.Visible = true;
                        ibtnCRAvailable.Title = "Available";
                        ibtnCRAvailable.HRef = hdnURL.Value;
                    }

                    if ((!string.IsNullOrEmpty(hdnUCRUpdateCount.Value)) && (hdnUCRUpdateCount.Value != "0"))
                    {
                        btnUpdateHistory.HRef = "~/UI/UCRUpdateLog.aspx?CRId=" + hdnCRID.Value;
                        btnUpdateHistory.Visible = true;
                        btnUpdateHis.Visible = true;
                    }
                    else
                    {
                        btnUpdateHistory.Visible = false;
                        btnUpdateHis.Visible = false;
                    }

                }
            }
            catch (Exception ex) { }
        }
        #endregion

        #region[Gridview Selected Event]
        protected void odsUCRListing_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue == null) return;
            if (e.ReturnValue.GetType().Name == "Int32")
            {

                Int32 count = Convert.ToInt32(e.ReturnValue);
                Int32 pagesize = gvUCRListing.PageSize;
                Int32 pagecount = count / pagesize;
                if (count < pagesize)
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} of {0} records", count);
                }
                else
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} through {1} of {2} records"
                         , String.Format("{0:#,###}", pagesize * gvUCRListing.PageIndex + 1)
                         , String.Format("{0:#,###}", pagesize * (gvUCRListing.PageIndex + 1) > count ? count : pagesize * (gvUCRListing.PageIndex + 1))
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

                    ddlPaging.SelectedValue = String.Format("{0}", gvUCRListing.PageIndex + 1);
                    ddlPaging1.SelectedValue = String.Format("{0}", gvUCRListing.PageIndex + 1);
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
            gvUCRListing.PageIndex = 0;
            BindGrid();
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedIndex > 0)
                gvUCRListing.PageIndex--;
            BindGrid();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedValue != ddlPaging.Items.Count.ToString())
                gvUCRListing.PageIndex++;
            BindGrid();
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            gvUCRListing.PageIndex = ddlPaging.Items.Count - 1;
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

            gvUCRListing.PageIndex = 0;
            BindGrid();
        }
        #endregion

        #region[ Bind sort dropdown]
        private void BindSort1DDL()
        {
            try
            {
                ddlSort1.DataSource = objUCRBAL.UCRListing_SortOptions("Select SortText,SortValue from Lookup_Sort where tableid=8 order by Sequence asc", "", "");
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
                ddlSort2.DataSource = objUCRBAL.UCRListing_SortOptions(String.Format("Select SortText,SortValue from Lookup_Sort where tableid=8 AND SortValue NOT IN ('{0}') order by Sequence asc", ddlSort1.SelectedValue), "", "");
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
                ddlSort3.DataSource = objUCRBAL.UCRListing_SortOptions(String.Format("Select SortText,SortValue from Lookup_Sort where tableid=8 AND SortValue NOT IN ('{0}', '{1}') order by Sequence asc", ddlSort1.SelectedValue, ddlSort2.SelectedValue), "", "");
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

        #region[Drop down page selection change]
        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "0")
                gvUCRListing.PageIndex = Convert.ToInt32(((DropDownList)sender).SelectedValue) - 1;
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

        protected void gvUCRListing_Sorting(object sender, GridViewSortEventArgs e)
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

        #region [Search Button]
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        protected void btnUCRDetail_Click(object sender, EventArgs e)
        {
            ImageButton ibtn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)ibtn.NamingContainer;
            HiddenField hdnURL = (HiddenField)row.FindControl("hdnURL");
        }

        protected void ibtncars_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton ibtncars = (ImageButton)sender;
                GridViewRow row = (GridViewRow)ibtncars.NamingContainer;
                long UCRID = Convert.ToInt64(gvUCRListing.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);

                ifrmSlideShow.Attributes.Add("src", String.Format("SectionImages.aspx?UCRID={0}", UCRID));
                ModalPopupExtender1.Show();
            }
            catch (Exception ex) { }
        }

        #region[BindUpdateHistory]
        protected void btnUpdateHis_Click(object sender, ImageClickEventArgs e)
        {

            DataTable dtUpdateXML = new DataTable();
            DataTable dtUCR = new DataTable();
            ImageButton ibtnExpenseEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)ibtnExpenseEdit.NamingContainer;
            HiddenField hdnCRId = row.FindControl("hdnCRId") as HiddenField;
            HiddenField hdnInventoryId = row.FindControl("hfInvID") as HiddenField;
            GridView gvUpdateHistory = row.FindControl("gvUpdateHistory") as GridView;
            HtmlTableRow tr = row.FindControl("trnew") as HtmlTableRow;

            if (!string.IsNullOrEmpty(hdnCRId.Value) && (!string.IsNullOrEmpty(hdnInventoryId.Value) && (Convert.ToString(hdnInventoryId.Value) != "0")))
            {
                dtUpdateXML = objUCRBAL.GetUCRUpdateXML(Convert.ToInt32(hdnCRId.Value));//, Convert.ToInt32(hdnInventoryId.Value)
                if (dtUpdateXML.Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    DataSet ds = ConvertXMLToDataSet(Convert.ToString(dtUpdateXML.Rows[0]["UpdateXML"]));
                    dtUCR = ds.Tables["UCR"];
                    dt = dtUCR.Copy();

                    if (dt.Columns.Contains("Command_Id"))
                    {
                        dt.Columns.Remove("Command_Id");
                        gvUpdateHistory.DataSource = dt;
                        gvUpdateHistory.DataBind();
                    }

                    else
                    {
                        gvUpdateHistory.DataSource = dtUCR;
                        gvUpdateHistory.DataBind();
                    }

                }
            }
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
    
        public DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }
        #endregion

        #region[UCR Response Details]
        protected void btnUCRResponseDetails_Click(object sender, EventArgs e)
        {
            try
            {
                string str = string.Empty;
                string strUCRId = ((ImageButton)sender).CommandArgument;
                if (!string.IsNullOrEmpty(strUCRId))
                {
                    DataTable dtUCRResponseDetail = new DataTable();
                    dtUCRResponseDetail = objUCRBAL.GetUCRResponseDetailsbyUCRId(Convert.ToInt64(strUCRId));

                    if (dtUCRResponseDetail.Rows.Count > 0)
                    {
                        str = Convert.ToString(dtUCRResponseDetail.Rows[0]["UCRResponse"]);
                        StringBuilder strxml = new StringBuilder();
                        strxml.AppendLine("<?xml version=\"1.0\" encoding=\"utf-16\" ?> ");
                        strxml.AppendLine("<ResponseCollection>");
                        strxml.AppendLine(str);
                        strxml.AppendLine("</ResponseCollection>");

                        if (!String.IsNullOrEmpty(strxml.ToString()))
                        {
                            string path = Server.MapPath("UCRResponseData.xml");
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