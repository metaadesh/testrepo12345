using System;
using System.Collections.Generic;
using System.Collections;
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
using System.IO;

namespace METAOPTION.UI
{
    public partial class InventoryList : System.Web.UI.Page
    {
        public string UCRlinkUrl = String.Empty;
        public string CreateUCRUrl = String.Empty;

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
            UCRlinkUrl = ConfigurationManager.AppSettings["UCRlinkUrl"];
            CreateUCRUrl = ConfigurationManager.AppSettings["CreateUCRUrl"];

            if (!(Page.IsPostBack || Page.IsCallback))
            {
                CheckPagePermission();
                BindYear();
                BindSort1DDL();
                if (Request["CarStatus"] != null)
                    if (ddlCarStatus.Items.FindByValue((String)Request["CarStatus"]) != null)
                        ddlCarStatus.SelectedValue = (String)Request["CarStatus"];
                BindGrid();
            }
        }

        private void CheckPagePermission()
        {
            List<String> Rights = CommonBAL.GetPagePermission(Constant.UserId, "INVENTORY");
            if (!(Rights.Contains("INVENTORY.VIEW") || Rights.Contains("INVENTORY.EDIT") || Rights.Contains("INVENTORY.ADD")))
                Response.Redirect("Permission.aspx?MSG=INVENTORY.VIEW or INVENTORY.EDIT or INVENTORY.ADD");
        }

        #region [ Bind Controls ]
        private void BindYear()
        {
            this.ddlYear.DataSource = BAL.Common.GetYearList();
            this.ddlYear.DataTextField = "Year";
            this.ddlYear.DataValueField = "Year";
            this.ddlYear.DataBind();
            this.ddlYear.Items.Insert(0, new ListItem("", "-1"));
        }
        #endregion

        #region[Drop down page selection change]
        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvInventoryList.PageIndex = Convert.ToInt32(((DropDownList)sender).SelectedValue) - 1;
            BindGrid();
            EnablePaging();
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

        #region[Sort dropdown]
        private void BindSort1DDL()
        {
            ddlSort1.DataSource = InventoryBAL.InventoryList_SortOptions("SELECT * FROM LookUp_Sort WHERE TableId = 1", "", "");
            ddlSort1.DataTextField = "SortText";
            ddlSort1.DataValueField = "SortValue";
            ddlSort1.DataBind();

            ddlSort1.Items.Insert(0, new ListItem("", "-1"));
            if (ddlSort1.Items.FindByValue("I.[DateAdded]") != null)
                ddlSort1.SelectedValue = "I.[DateAdded]";

            BindSort2DDL();
            BindSort3DDL();
        }

        private void BindSort2DDL()
        {
            ddlSort2.DataSource = InventoryBAL.InventoryList_SortOptions(String.Format("SELECT * FROM LookUp_Sort WHERE TableId = 1 AND SortValue NOT IN ('{0}')", ddlSort1.SelectedValue), "", "");
            ddlSort2.DataTextField = "SortText";
            ddlSort2.DataValueField = "SortValue";
            ddlSort2.DataBind();

            ddlSort2.Items.Insert(0, new ListItem("", "-1"));
            //if (ddlSort2.Items.FindByValue("YMMB.VINDivisionName") != null)
            //    ddlSort2.SelectedValue = "YMMB.VINDivisionName";

            BindSort3DDL();
        }

        private void BindSort3DDL()
        {
            ddlSort3.DataSource = InventoryBAL.InventoryList_SortOptions(String.Format("SELECT * FROM LookUp_Sort WHERE TableId =1 AND SortValue NOT IN ('{0}', '{1}')", ddlSort1.SelectedValue, ddlSort2.SelectedValue), "", "");
            ddlSort3.DataTextField = "SortText";
            ddlSort3.DataValueField = "SortValue";
            ddlSort3.DataBind();

            ddlSort3.Items.Insert(0, new ListItem("", "-1"));
            //if (ddlSort3.Items.FindByValue("YMMB.VINModelName") != null)
            //    ddlSort3.SelectedValue = "YMMB.VINModelName";
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

        #region[Bind grid]
        protected void BindGrid()
        {
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
                sort = "I.[DateAdded] DESC";

            // Added to stop data inconsistency
            sort += ", InventoryId DESC";

            ObjectDataSource odsInventory = new ObjectDataSource();
            odsInventory.Selected += new ObjectDataSourceStatusEventHandler(odsInventory_Selected);
            odsInventory.TypeName = "METAOPTION.BAL.InventoryBAL";
            odsInventory.SelectMethod = "SearchInventory";
            odsInventory.SelectCountMethod = "SearchInventoryCount";
            odsInventory.EnablePaging = true;
            odsInventory.SelectParameters.Add("VIN", txtVINNumber.Text.Trim());
            odsInventory.SelectParameters.Add("Year", ddlYear.SelectedValue);
            odsInventory.SelectParameters.Add("Model", txtModel.Text.Trim());
            odsInventory.SelectParameters.Add("Make", txtMake.Text.Trim());
            odsInventory.SelectParameters.Add("Dealer", txtDealer.Text.Trim());
            odsInventory.SelectParameters.Add("CarStatus", ddlCarStatus.SelectedValue);
            odsInventory.SelectParameters.Add("CRStatus", ddlUCR.SelectedValue);
            odsInventory.SelectParameters.Add("startRowIndex", DbType.Int32, gvInventoryList.PageIndex.ToString());
            odsInventory.SelectParameters.Add("maximumRows", DbType.Int32, ddlPageSize1.SelectedValue);
            odsInventory.SelectParameters.Add("SystemID", DbType.Int32, Convert.ToString(Session["SystemId"]));
            odsInventory.SelectParameters.Add("SortExpression", sort);
            odsInventory.SelectParameters.Add("OrgID",DbType.Int16,Constant.OrgID.ToString());
            gvInventoryList.DataSource = odsInventory;
            gvInventoryList.DataBind();
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

        #region[Search button event handler]
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvInventoryList.PageIndex = 0;
            BindGrid();
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

        #region[Inventory grid RowDataBound event]
        protected void gvInventoryList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton ibtnCR = (ImageButton)e.Row.FindControl("ibtnCR");
                HiddenField hfCRStatus = (HiddenField)e.Row.FindControl("hfCRStatus");
                ImageButton imgCar = (ImageButton)e.Row.FindControl("ibtncars");
                Label lblImageCount = (Label)e.Row.FindControl("lblImageCount");

                HtmlAnchor ibtnCRAvailable = (HtmlAnchor)e.Row.FindControl("ibtnCRAvailable");

                if (String.IsNullOrEmpty(hfCRStatus.Value) || hfCRStatus.Value == "0")
                {
                    ibtnCR.ImageUrl = "~/Images/ucr-btn1.png";
                    ibtnCR.ToolTip = "Not Ready";
                }
                else if (hfCRStatus.Value == "10" || hfCRStatus.Value == "20")
                {
                    ibtnCR.ImageUrl = "~/Images/ucr-btn2.png";
                    ibtnCR.ToolTip = "Initiated";
                }

                // Changed by Ashar on 11 Oct, 2012 as per MOSS task #65
                else if (hfCRStatus.Value == "30")
                {
                    ibtnCR.ImageUrl = "~/Images/ucr-btn.png";
                    ibtnCR.ToolTip = "Available";
                }

                //else if (hfCRStatus.Value == "30")
                //{
                //    ibtnCRAvailable.Visible = true;
                //    ibtnCR.Visible = false;
                //    ibtnCRAvailable.Title = "Available";
                //    #region [Added by Rupendra 14 Sep 12 for open UCR in a new tab]
                //    HiddenField hdnInventoryId = (HiddenField)e.Row.FindControl("hdnInventoryId");
                //    HiddenField hdnCarcost = (HiddenField)e.Row.FindControl("hdnCarcost");

                //    HiddenField hfCRID = (HiddenField)e.Row.FindControl("hfCRId");
                //    HiddenField hfVIN2 = (HiddenField)e.Row.FindControl("hfVIN2");

                //    HiddenField hfyear = (HiddenField)e.Row.FindControl("hfyear");
                //    HiddenField hfmake = (HiddenField)e.Row.FindControl("hfmake");
                //    HiddenField hfmodel = (HiddenField)e.Row.FindControl("hfmodel");
                //    HiddenField hfbody = (HiddenField)e.Row.FindControl("hfbody");
                //    HiddenField hfprice = (HiddenField)e.Row.FindControl("hfprice");
                //    HiddenField hfmileage = (HiddenField)e.Row.FindControl("hfmileage");
                //    HiddenField hfextcol = (HiddenField)e.Row.FindControl("hfextcol");
                //    HiddenField hfintcol = (HiddenField)e.Row.FindControl("hfintcol");


                //    StringBuilder querystring = new StringBuilder();
                //    querystring.Append("SystemID=2&VIN=" + hfVIN2.Value);
                //    querystring.Append(String.Format("&SourceInventoryID={0}", hdnInventoryId.Value));
                //    querystring.Append(String.Format("&Year={0}", hfyear.Value));
                //    querystring.Append("&Make=" + hfmake.Value);
                //    querystring.Append("&Model=" + hfmodel.Value);
                //    querystring.Append("&Body=" + hfbody.Value);
                //    querystring.Append(String.Format("&Mileage={0}", hfmileage.Value));
                //    querystring.Append(String.Format("&MPrice={0}", hdnCarcost.Value));
                //    querystring.Append("&ExtColor=" + hfextcol.Value);
                //    querystring.Append("&IntColor=" + hfintcol.Value);
                //    ibtnCRAvailable.HRef = (String.Format("{0}{1}", UCRlinkUrl, hfCRID.Value));
                //    #endregion

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

        #region[Add/View CR image button]
        protected void ibtnCR_Click(object sender, ImageClickEventArgs e)
        {
            long InvID = Convert.ToInt64(gvInventoryList.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            ImageButton ibtn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)ibtn.NamingContainer;
            HiddenField hfCRStatus = (HiddenField)row.FindControl("hfCRStatus");
            HiddenField hfCRID = (HiddenField)row.FindControl("hfCRId");
            HiddenField hfVIN2 = (HiddenField)row.FindControl("hfVIN2");

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
            // Changed by Ashar on 11 Oct, 2012 as per MOSS task #65
            else if (hfCRStatus.Value == "10" || hfCRStatus.Value == "20" || hfCRStatus.Value == "30")
            {
                mpeChangeCR.Show();
                hlnkViewReport.NavigateUrl = String.Format("{0}{1}", UCRlinkUrl, hfCRID.Value);
            }
            //else if (hfCRStatus.Value == "30")
            //{
            // Response.Redirect(String.Format("{0}{1}", UCRlinkUrl, hfCRID.Value));
            //Response.Redirect(String.Format("http://web.metaoptionllc.com:82/Report/{0}", hfInventoryID.Value));
            //}

            //CarDetailsSelectResult res = METAOPTION.DAL.InventoryDAL.GetCarDetail(InvID);
            //if (res != null)
            //    ExtractCrInfo(res);
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
            long InvID = Convert.ToInt64(gvInventoryList.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            PreInventoryBAL PreInvBAL = new PreInventoryBAL();
            long PreInvID = PreInvBAL.PreInv_ByInvID(InvID);

            ImageButton ibtncars = (ImageButton)sender;
            GridViewRow row = (GridViewRow)ibtncars.NamingContainer;
            HiddenField hdnVIN = row.FindControl("hfVIN2") as HiddenField;

            ifrmSlideShow.Attributes.Add("src", String.Format("ViewAllImages.aspx?id={0}&v={1}&preid={2}", InvID, hdnVIN.Value, PreInvID));
            //ifrmSlideShow.Attributes.Add("src", String.Format("ImageGallery.aspx?id={0}", PreInvID));
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
            for (int col = 0; col < 20; col++)
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

                else if (col == 14)
                {
                    cellExport.Text = "Price Sold";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }

                else if (col == 15)
                {
                    cellExport.Text = "Date Sold";
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0198E1");
                }

                else if (col == 16)
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

                cellExport.BorderWidth = 1;
                //cellExport.Style.Add("font-weight", "bold");
                cellExport.Style.Add("color", "White");
                cellExport.Style.Add("margin-bottom", "2px");
                cellExport.Style.Add("font-size", "11pt");
                cellExport.Style.Add("text-align", "Center");
                cellExport.Style.Add("border-color", "Maroon");
                //cellExport.BorderWidth = Unit.Pixel(2);
                // cellExport.ColumnSpan = 4;

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
                for (Int16 cl = 0; cl < dtExport.Columns.Count-1; cl++)
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
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["ComeBack"]);
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
                    else if (cl == 14)
                    {
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["SoldPrice"]);
                        cellExport.Style.Add("text-align", "right");
                    }

                    else if (cl == 15)
                    {
                        cellExport.Text = Convert.ToString(dtExport.Rows[cr]["SoldDate"]);
                        cellExport.Style.Add("text-align", "left");
                    }

                    else if (cl == 16)
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

                    cellExport.BorderWidth = 1;
                    cellExport.Style.Add("font-size", "10pt");
                    //cellExport.Style.Add("font-weight", "bold");
                    // cellExport.Style.Add("font-weight", "bold");
                    // cellExport.Style.Add("text-align", "left");
                    cellExport.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0FFFF");
                    cellExport.Style.Add("vertical-align", "top");
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
            InventoryBAL objInventoryBAL = new InventoryBAL();
            DataTable dtExport = new DataTable();
            dtExport = objInventoryBAL.InventoryListDataExport(txtVINNumber.Text.Trim(), Convert.ToInt32(ddlYear.SelectedValue),
                txtModel.Text.Trim(), txtMake.Text.Trim(), txtDealer.Text.Trim(), Convert.ToInt32(ddlCarStatus.SelectedValue)
                , Convert.ToInt32(ddlUCR.SelectedValue), Convert.ToInt32(Session["SystemId"]), "Mobile_Chrome_Make.VINDivisionName DESC, I.DATEADDED DESC",Constant.OrgID);
            return dtExport;
        }

        public Int32 ExportDataCount()
        {
            InventoryBAL objInventoryBAL = new InventoryBAL();
            Int32 rowCount = 0;
            rowCount = objInventoryBAL.InventoryListDataExportCount(txtVINNumber.Text.Trim(), Convert.ToInt32(ddlYear.SelectedValue),
               txtModel.Text.Trim(), txtMake.Text.Trim(), txtDealer.Text.Trim(), Convert.ToInt32(ddlCarStatus.SelectedValue),
               Convert.ToInt32(ddlUCR.SelectedValue), Convert.ToInt32(Session["SystemId"]), "Mobile_Chrome_Make.VINDivisionName DESC, I.DATEADDED DESC", Constant.OrgID);
            return rowCount;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            DataTable dtExportData = new DataTable();
            dtExportData = ExportData();
            ExportToExcel(dtExportData, 2);
        }
        #endregion
    }
}
