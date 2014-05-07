using System;
using System.Collections;
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
using System.Collections.Generic;
using System.Text;
using System.Web.Services;
using METAOPTION.UserControls;
using METAOPTION.UCR;


namespace METAOPTION.UI
{
    public partial class PreInventory : System.Web.UI.Page
    {
        PreInventoryBAL PreInvBAL = new PreInventoryBAL();
        private int rowCount = 0;
        private int totalRowCount = 0;
        public string UCRlinkUrl = String.Empty;
        public string CreateUCRUrl = String.Empty;
        public int DuplicatePeriod = 0;

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
            UCRlinkUrl = ConfigurationManager.AppSettings["UCRlinkUrl"];
            CreateUCRUrl = ConfigurationManager.AppSettings["CreateUCRUrl"];
            DuplicatePeriod = Convert.ToInt32(ConfigurationManager.AppSettings["DuplicateInDays"]);

            if (!IsPostBack)
            {
                BindUsers();
                BindBuyers();
                BindYear();
                BindMake();
                this.ddlModel.Items.Insert(0, new ListItem("ALL", "-1"));
                this.ddlBody.Items.Insert(0, new ListItem("ALL", "-1"));
                BindSort1DDL();
                BindGrid();


            }

            OkButton.OnClientClick = String.Format("fnClickUpdate('{0}','{1}')", OkButton.UniqueID, "");
        }

        private void CheckPermission()
        {
            List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, "PAYMENT");

            if (!dict.Contains("ALLPAYMENT.VIEW"))
                Response.Redirect("Permission.aspx?MSG=ALLPAYMENT.VIEW", true);
        }


        protected void grvPreInv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvPreInv.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        //protected void grvPreInv_OnSorting(object sender, GridViewSortEventArgs e)
        //{
        //    BindGrid();  
        //}


        #region Change Request:BUGID: 725: Highlight voided row and display "Voided" and hide image

        /// <summary>
        /// Handle GridView RowDatabound event to highlight row,if check is voided and hide print image and show "voided"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grvPreInv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblImageCount = (Label)e.Row.FindControl("lblImageCount");
                ImageButton imgCar = (ImageButton)e.Row.FindControl("ibtncars");
                ImageButton imgAdd = (ImageButton)e.Row.FindControl("ibtnaddinv");
                ImageButton imgPending = (ImageButton)e.Row.FindControl("ibtnPending");
                ImageButton imgDel = (ImageButton)e.Row.FindControl("ibtnDelete");
                ImageButton ibtnCR = (ImageButton)e.Row.FindControl("ibtnCR");
                ImageButton ibtnDuplicate = (ImageButton)e.Row.FindControl("ibtnDuplicate");
                ImageButton ibtnView = (ImageButton)e.Row.FindControl("ibtnView");
                Image imggCR = (Image)e.Row.FindControl("imggCR");
                Label lblvin = (Label)e.Row.FindControl("lblvin");

                HiddenField hfPending = (HiddenField)e.Row.FindControl("hfpending");
                HiddenField hfIsActive = (HiddenField)e.Row.FindControl("hfIsActive");
                HiddenField hfRejected = (HiddenField)e.Row.FindControl("hfRejected");
                HiddenField hfCRStatus = (HiddenField)e.Row.FindControl("hfCRStatus");
                HiddenField hfDuplicateInv = (HiddenField)e.Row.FindControl("hfDuplicateInv");
                HiddenField hfDuplicatePreInv = (HiddenField)e.Row.FindControl("hfDuplicatePreInv");
                HyperLink hlnkVIN = (HyperLink)e.Row.FindControl("hlnkVIN");
                HyperLink ancrCR = (HyperLink)e.Row.FindControl("ancrCR");

                ImageButton ibtnEmailStatus = (ImageButton)e.Row.FindControl("ibtnEmailStatus");
                HiddenField hfIsEmailSent = (HiddenField)e.Row.FindControl("hfIsEmailSent");
                HiddenField hfBuyerNote = (HiddenField)e.Row.FindControl("hfBuyerNote");
                HyperLink hylnkView = (HyperLink)e.Row.FindControl("hylnkView");

                HiddenField hfyear = (HiddenField)e.Row.FindControl("hfyear");
                HiddenField hfmake = (HiddenField)e.Row.FindControl("hfmake");
                HiddenField hfmodel = (HiddenField)e.Row.FindControl("hfmodel");
                HiddenField hfbody = (HiddenField)e.Row.FindControl("hfbody");
                HiddenField hfprice = (HiddenField)e.Row.FindControl("hfprice");
                HiddenField hfmileage = (HiddenField)e.Row.FindControl("hfmileage");
                HiddenField hfextcol = (HiddenField)e.Row.FindControl("hfextcol");
                HiddenField hfintcol = (HiddenField)e.Row.FindControl("hfintcol");
                HiddenField hfPreInv_CRId = (HiddenField)e.Row.FindControl("hfPreInv_CRId");
                HtmlTableRow trnew = (HtmlTableRow)e.Row.FindControl("trnew");
                HtmlTableRow trnew1 = (HtmlTableRow)e.Row.FindControl("trnew1");
                String PreInvID = grvPreInv.DataKeys[e.Row.RowIndex].Values[0].ToString();

                if (Convert.ToString(Session["LoginEntityTypeID"]) != "2" || Convert.ToString(Session["LoginEntityTypeID"]) != "1")
                {
                    ibtnCR.OnClientClick = String.Format("ShowUCRPopup('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}');return false;",
                                                         lblvin.Text, hfyear.Value, hfmake.Value, hfmodel.Value, hfbody.Value, hfprice.Value, hfmileage.Value,
                                                         hfextcol.Value, hfintcol.Value, hfCRStatus.Value, hfPreInv_CRId.Value, PreInvID);
                }
                // Add VIN in hidden field
                hfvin.Value = lblvin.Text;

                Label lblInvID = (Label)e.Row.FindControl("lblInvID");
                if (Convert.ToInt32(lblInvID.Text) > 0)
                {
                    imgAdd.ImageUrl = "~/Images/H_active.png";
                    imgAdd.Enabled = false;
                    e.Row.BackColor = System.Drawing.Color.LightCyan;
                    //e.Row.ToolTip = "Added to Inventory";
                    imgPending.Visible = false;
                    imgDel.Visible = false;
                    lblvin.ToolTip = "Status: Added To Inventory";
                    lblvin.Visible = false;
                    hlnkVIN.Visible = true;
                }
                else
                    imgPending.Visible = false;

                if (Convert.ToBoolean(hfPending.Value) == true && Convert.ToBoolean(hfRejected.Value) == false)
                {
                    imgAdd.Visible = true;
                    imgDel.Visible = true;
                    imgPending.Visible = false;
                    lblvin.ToolTip = "Status: Pending";
                }

                if (Convert.ToBoolean(hfPending.Value) == true && Convert.ToBoolean(hfRejected.Value) == true) //Rejectd
                {
                    imgAdd.Visible = false;
                    imgPending.Visible = true;
                    imgDel.Visible = false;
                    lblvin.ToolTip = "Status: Rejected";
                    ibtnCR.Visible = false;
                }


                if (Convert.ToInt32(lblImageCount.Text) > 0)
                {
                    imgCar.Visible = true;
                    imgCar.ToolTip = String.Format("View {0} {1}", lblImageCount.Text, Convert.ToInt32(lblImageCount.Text) > 1 ? "Images" : "Image");
                }
                else
                    imgCar.Visible = false;

                if (String.IsNullOrEmpty(hfCRStatus.Value) || hfCRStatus.Value == "0")
                {

                    ibtnCR.ImageUrl = "~/Images/ucr-btn1.png";
                    ibtnCR.ToolTip = "Not Ready";
                    imggCR.ImageUrl = "~/Images/ucr-btn1.png";
                    imggCR.ToolTip = "Not Ready";
                }
                else if (hfCRStatus.Value == "10" || hfCRStatus.Value == "20")
                {
                    ibtnCR.ImageUrl = "~/Images/ucr-btn2.png";
                    ibtnCR.ToolTip = "Initiated";
                    imggCR.ImageUrl = "~/Images/ucr-btn2.png";
                    imggCR.ToolTip = "Initiated";
                }
                else if (hfCRStatus.Value == "30")
                {
                    ibtnCR.ImageUrl = "~/Images/ucr-btn.png";
                    ibtnCR.ToolTip = "Available";
                    imggCR.ImageUrl = "~/Images/ucr-btn.png";
                    imggCR.ToolTip = "Available";
                }

                if (hfDuplicateInv.Value == "0" && hfDuplicatePreInv.Value == "0")
                    ibtnDuplicate.Visible = false;
                else
                    ibtnDuplicate.Visible = true;

                if (!String.IsNullOrEmpty(hfBuyerNote.Value))
                {
                    ibtnEmailStatus.Visible = true;
                    if (Convert.ToBoolean(hfIsEmailSent.Value) == true)
                    {
                        ibtnEmailStatus.ImageUrl = "../Images/Email_Sent.png";
                        ibtnEmailStatus.ToolTip = "Buyer Email Sent";
                        //ibtnEmailStatus.Attributes.Add("title", "Buyer Email Sent");
                    }
                    else if (Convert.ToBoolean(hfIsEmailSent.Value) == false)
                    {
                        ibtnEmailStatus.ImageUrl = "../Images/Email_NotSent.png";
                        ibtnEmailStatus.ToolTip = "Buyer Email Not Sent";
                        //ibtnEmailStatus.Attributes.Add("title", "Buyer Email Not Sent");
                    }
                }
                else
                    ibtnEmailStatus.Visible = false;

                //if (e.Row.RowIndex % 2 == 0)
                //    trnew.BgColor = "#F9F9F9";
                //else
                //    trnew.BgColor = "#FFF";

                bool isAlternating = e.Row.RowState == DataControlRowState.Alternate;
                if (isAlternating)
                {
                    trnew.Attributes.Add("class", grvPreInv.AlternatingRowStyle.CssClass);
                    trnew1.Attributes.Add("class", grvPreInv.AlternatingRowStyle.CssClass);
                }
                else
                {
                    trnew.Attributes.Add("class", grvPreInv.RowStyle.CssClass);
                    trnew1.Attributes.Add("class", grvPreInv.RowStyle.CssClass);
                }

                if (Convert.ToInt32(lblInvID.Text) > 0)
                {
                    trnew.Attributes.Add("style", "background-color:LightCyan;");
                    trnew1.Attributes.Add("style", "background-color:LightCyan;");
                }
                trnew.Style.Add("display", "none");
                trnew1.Style.Add("display", "none");
                if (Convert.ToString(Session["LoginEntityTypeID"]) == "2" || Convert.ToString(Session["LoginEntityTypeID"]) == "1")
                {
                    imgDel.Visible = false;
                    // grvPreInv.Columns[0].Visible = false;                  
                    hylnkView.Enabled = false;
                    if (!imgAdd.ImageUrl.Contains("~/Images/H_active.png"))
                        imgAdd.Visible = false;
                    //imgAdd.Visible = false;
                    // ibtnView.Visible = false;
                    if (String.IsNullOrEmpty(hfCRStatus.Value))
                    {
                        ibtnCR.Visible = false;
                        ancrCR.Visible = true;
                        ancrCR.Enabled = false;
                    }
                    else if (hfCRStatus.Value == "10" || hfCRStatus.Value == "20" || hfCRStatus.Value == "30")
                    {
                        ibtnCR.Visible = false;
                        ancrCR.Visible = true;
                        ancrCR.NavigateUrl = "http://www.universalconditionreport.com/Report/" + hfPreInv_CRId.Value;
                    }


                }
            }
        }

        public String Duplicate(object pre, object inv)
        {
            if (Convert.ToInt32(pre) == 0 && Convert.ToInt32(inv) == 0)
                return String.Empty;
            else
                return String.Format("<p style='margin-top:10px'>Dup- PreInv: {0} Inv: {1}</p>", pre, inv);
        }

        protected void ibtncars_Click(object sender, ImageClickEventArgs e)
        {
            long PreInvID = Convert.ToInt64(grvPreInv.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            //CarSlideShow.SlideShowExtender1.ContextKey = PreInvID.ToString();
            frame1.Attributes.Add("src", String.Format("ImageGallery.aspx?id={0}", PreInvID));
            //Session["PreInvID"] = PreInvID;
            //mpoImageSlidshow.Show();
            ModalPopupExtender1.Show();
        }

        #endregion

        #region[Add to Inventory]
        protected void ibtnaddinv_Click(object sender, ImageClickEventArgs e)
        {
            long preInvID = Convert.ToInt64(grvPreInv.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);

            Response.Redirect(String.Format("AddInventory.aspx?PreInvID={0}&ReturnUrl=PreInventory.aspx", preInvID));

        }
        #endregion

        #region[Inventory CR]
        public string UCRLink(object inventoryID)
        {
            //long preInvID = Convert.ToInt64(grvPreInv.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);

            return String.Format("http://web.metaoptionllc.com:82/CarDeatil/NewCR?token={0}&sysid=2&code={1}", Session.SessionID, inventoryID);

        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grvPreInv.PageIndex = 0;
            BindGrid();
        }

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
            ddlSort1.DataSource = PreInvBAL.InventoryList_SortOptions("Select SortText,SortValue from Lookup_Sort where tableid=4 order by Sequence asc", "", "");
            ddlSort1.DataTextField = "SortText";
            ddlSort1.DataValueField = "SortValue";
            ddlSort1.DataBind();

            ddlSort1.Items.Insert(0, new ListItem("", "-1"));
            //if (ddlSort1.Items.FindByValue("I.[DateAdded]") != null)
            //    ddlSort1.SelectedValue = "I.[DateAdded]";

            BindSort2DDL();
            BindSort3DDL();
        }

        private void BindSort2DDL()
        {
            ddlSort2.DataSource = PreInvBAL.InventoryList_SortOptions(String.Format("Select SortText,SortValue from Lookup_Sort where tableid=4 AND SortValue NOT IN ('{0}') order by Sequence asc", ddlSort1.SelectedValue), "", "");
            ddlSort2.DataTextField = "SortText";
            ddlSort2.DataValueField = "SortValue";
            ddlSort2.DataBind();

            ddlSort2.Items.Insert(0, new ListItem("", "-1"));


            BindSort3DDL();
        }

        private void BindSort3DDL()
        {
            ddlSort3.DataSource = PreInvBAL.InventoryList_SortOptions(String.Format("Select SortText,SortValue from Lookup_Sort where tableid=4 AND SortValue NOT IN ('{0}', '{1}') order by Sequence asc", ddlSort1.SelectedValue, ddlSort2.SelectedValue), "", "");
            ddlSort3.DataTextField = "SortText";
            ddlSort3.DataValueField = "SortValue";
            ddlSort3.DataBind();

            ddlSort3.Items.Insert(0, new ListItem("", "-1"));

        }
        #endregion

        #region[Year]
        private void BindYear()
        {
            this.ddlYear.DataSource = BAL.Common.GetYearList();
            this.ddlYear.DataTextField = "Year";
            this.ddlYear.DataValueField = "Year";
            this.ddlYear.DataBind();
            this.ddlYear.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        #endregion

        #region[Drop down page selection change]
        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            grvPreInv.PageIndex = Convert.ToInt32(((DropDownList)sender).SelectedValue) - 1;
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
            grvPreInv.PageIndex = 0;
            BindGrid();
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedIndex > 0)
                grvPreInv.PageIndex--;

            BindGrid();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedValue != ddlPaging.Items.Count.ToString())
                grvPreInv.PageIndex++;

            BindGrid();
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            grvPreInv.PageIndex = ddlPaging.Items.Count - 1;
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

            grvPreInv.PageIndex = 0;

            BindGrid();
        }
        #endregion

        #region[Bind grid]
        protected void BindGrid()
        {
            if (Convert.ToString(Session["LoginEntityTypeID"]) == "1")
            {
                txtDealer.Text = Convert.ToString(Session["UserName"]);
                txtDealer.Enabled = false;
            }

            String ParentBuyerID = "-1";
            if (Convert.ToString(Session["BuyerAccessLevel"]) != "1")
                ParentBuyerID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString();
            // ddlPageSize1.SelectedValue = ddlPageSize2.SelectedValue;
            grvPreInv.PageSize = Convert.ToInt32(ddlPageSize1.SelectedValue);

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
                sort = "PreInventoryId desc";
            ObjectDataSource odsInventory = new ObjectDataSource();
            odsInventory.Selected += new ObjectDataSourceStatusEventHandler(odsInventory_Selected);
            odsInventory.TypeName = "METAOPTION.BAL.PreInventoryBAL";
            odsInventory.SelectMethod = "SearchPreInventory_Ver211";
            odsInventory.SelectCountMethod = "SearchPreInventoryCount_Ver211";
            odsInventory.EnablePaging = true;
            odsInventory.SelectParameters.Add("VIN", String.IsNullOrEmpty(txtVINNumber.Text.Trim()) ? String.Empty : txtVINNumber.Text.Trim());
            odsInventory.SelectParameters.Add("Year", ddlYear.SelectedValue);
            odsInventory.SelectParameters.Add("MakeID", ddlMake.SelectedValue);
            odsInventory.SelectParameters.Add("ModelID", ddlModel.SelectedValue);
            odsInventory.SelectParameters.Add("BodyID", ddlBody.SelectedValue);
            odsInventory.SelectParameters.Add("Dealer", String.IsNullOrEmpty(txtDealer.Text.Trim()) ? String.Empty : txtDealer.Text.Trim());
            odsInventory.SelectParameters.Add("BuyerID", ddlBuyer.SelectedValue);
            odsInventory.SelectParameters.Add("Status", ddlStatus.SelectedValue);
            odsInventory.SelectParameters.Add("AddedBy", ddlUsers.SelectedValue);
            odsInventory.SelectParameters.Add("CRStatus", ddlUCR.SelectedValue);
            odsInventory.SelectParameters.Add("Period", Convert.ToString(DuplicatePeriod));
            odsInventory.SelectParameters.Add("LoginEntityTypeID", Convert.ToString(Session["LoginEntityTypeID"]));
            odsInventory.SelectParameters.Add("EntityID", Convert.ToString(Session["UserEntityID"]));
            odsInventory.SelectParameters.Add("ParentEntityID", ParentBuyerID);
            odsInventory.SelectParameters.Add("StartRowIndex", DbType.Int32, grvPreInv.PageIndex.ToString());
            odsInventory.SelectParameters.Add("MaximumRows", DbType.Int32, ddlPageSize1.SelectedValue);
            odsInventory.SelectParameters.Add("SortExpression", sort);
            odsInventory.SelectParameters.Add("OrgID", Constant.OrgID.ToString());
            grvPreInv.DataSource = odsInventory;
            grvPreInv.DataBind();

        }
        #endregion

        #region[Drop down paging]
        protected void odsInventory_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue.GetType().Name == "Int32")
            {

                Int32 count = Convert.ToInt32(e.ReturnValue);
                Int32 pagesize = grvPreInv.PageSize;
                Int32 pagecount = count / pagesize;
                if (count < pagesize)
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} of {0} records", count);
                }
                else
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} through {1} of {2} records"
                         , String.Format("{0:#,###}", pagesize * grvPreInv.PageIndex + 1)
                         , String.Format("{0:#,###}", pagesize * (grvPreInv.PageIndex + 1) > count ? count : pagesize * (grvPreInv.PageIndex + 1))
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

                    ddlPaging.SelectedValue = String.Format("{0}", grvPreInv.PageIndex + 1);
                    ddlPaging1.SelectedValue = String.Format("{0}", grvPreInv.PageIndex + 1);
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

        #region[View PreInventoryDetail]
        protected void ibtnView_Click(object sender, ImageClickEventArgs e)
        {
            long preInvID = Convert.ToInt64(grvPreInv.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            Session["PreInvID"] = preInvID;
            Response.Redirect("PreInventoryDetail.aspx");

        }
        #endregion

        #region[Delete PreInventory]
        protected void ibtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            long preInvID = Convert.ToInt64(grvPreInv.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            hfDeletePrvID.Value = preInvID.ToString();
            txtreason.Text = string.Empty;
            ModelPopupExtender2.Show();


            //PreInvBAL.DeletePreinventory(preInvID, Convert.ToInt64(Session["empId"]),"");
            //BindGrid();
        }
        #endregion

        #region[PreInventory Pending]
        protected void ibtnPending_Click(object sender, ImageClickEventArgs e)
        {
            long preInvID = Convert.ToInt64(grvPreInv.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);

            ImageButton imgPending = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgPending.NamingContainer;
            HiddenField hfPending = (HiddenField)row.FindControl("hfpending");
            PreInvBAL.MarkPreInvAsPending(preInvID, Convert.ToInt64(Session["empId"]));

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

        //public string FormatGridPrice(object oPrice)
        //{
        //    string Price = Convert.ToString(oPrice);
        //    string str = string.Empty;
        //    if (Price.Length == 1 && Price == "$")
        //        str = string.Empty;
        //    else
        //        str = String.Format("${0:#,###}", Price); 
        //    return str;
        //}

        public string FormatBuyer(object oBuyer)
        {
            string Buyer = Convert.ToString(oBuyer);
            string str = Buyer.Replace("()", "");

            return str;
        }

        protected void OkButton_Click(object sender, EventArgs e)
        {
            if (rdlist.SelectedItem.Value == "D")
            {
                PreInvBAL.DeletePreinventory(Convert.ToInt64(hfDeletePrvID.Value), Convert.ToInt64(Session["empId"]));
            }
            else if (rdlist.SelectedItem.Value == "R")
            {
                PreInvBAL.RejectPreInventory(Convert.ToInt64(hfDeletePrvID.Value), Convert.ToInt64(Session["empId"]), txtreason.Text);
            }
            txtreason.Text = "";
            BindGrid();
        }

        protected void rdlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModelPopupExtender2.Show();
            if (rdlist.SelectedItem.Value == "R")
                trReason.Visible = false;
            else
                trReason.Visible = true;
        }

        #region[ Make ]
        private void BindMake()
        {
            this.ddlModel.Items.Clear();
            this.ddlMake.DataSource = BAL.Common.GetAllMake_Mobile();
            this.ddlMake.DataValueField = "MakeId";
            this.ddlMake.DataTextField = "VINDivisionName";
            this.ddlMake.DataBind();
            this.ddlMake.Items.Insert(0, new ListItem("ALL", "-1"));
        }

        protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dvSearch.Style.Add("display", "block");
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
            this.ddlModel.DataSource = BAL.Common.GetModels_Mobile(makeId);
            this.ddlModel.DataValueField = "ModelId";
            this.ddlModel.DataTextField = "VINModelName";
            this.ddlModel.DataBind();
            this.ddlModel.Items.Insert(0, new ListItem("ALL", "-1"));
            this.ddlBody.Items.Clear();
            this.ddlBody.Items.Insert(0, new ListItem("ALL", "-1"));
        }

        protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dvSearch.Style.Add("display", "block");
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
            this.ddlBody.DataSource = objCommon.GetBodies_Mobile(modelId);
            this.ddlBody.DataValueField = "BodyId";
            this.ddlBody.DataTextField = "VINStyleName";
            this.ddlBody.DataBind();
            this.ddlBody.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        #endregion

        #region[Users]
        private void BindUsers()
        {
            Int32 BuyerParentID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? -1 : Convert.ToInt32(Session["BuyerParent"]);
            PreInventoryBAL bal = new PreInventoryBAL();
            if (Convert.ToString(Session["BuyerAccessLevel"]) == "1")
                this.ddlUsers.DataSource = bal.GetAllPreInvUsers_ver211(Convert.ToInt32(Session["UserEntityID"]), -1, Convert.ToInt32(Session["LoginEntityTypeID"]), Constant.OrgID);
            else
                this.ddlUsers.DataSource = bal.GetAllPreInvUsers_ver211(Convert.ToInt32(Session["UserEntityID"]), BuyerParentID, Convert.ToInt32(Session["LoginEntityTypeID"]), Constant.OrgID);
            this.ddlUsers.DataTextField = "DisplayName";
            this.ddlUsers.DataValueField = "SecurityUserID";
            this.ddlUsers.DataBind();
            this.ddlUsers.Items.Insert(0, new ListItem("ALL", "-1"));
            if (Convert.ToString(Session["LoginEntityTypeID"]) == "1")
            {
                tdtitle.InnerText = "VIEW ALL Mobile-INVENTORIES";
                if (ddlUsers.Items.FindByValue(Convert.ToString(Session["empId"])) != null && BuyerParentID == -1 && ddlUsers.Items.Count == 2)
                {
                    ddlUsers.SelectedValue = Convert.ToString(Session["empId"]);
                    ddlUsers.Enabled = false;
                }

            }

        }
        #endregion

        #region[Add/Link UCR]
        protected void ibtnCR_Click(object sender, ImageClickEventArgs e)
        {
            long preInvID = Convert.ToInt64(grvPreInv.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            //hfDeletePrvID.Value = preInvID.ToString();
            txtCRIdUrl.Text = string.Empty;
            ImageButton ibtn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)ibtn.NamingContainer;
            Label lblvin = (Label)row.FindControl("lblvin");
            HiddenField hfyear = (HiddenField)row.FindControl("hfyear");
            HiddenField hfmake = (HiddenField)row.FindControl("hfmake");
            HiddenField hfmodel = (HiddenField)row.FindControl("hfmodel");
            HiddenField hfbody = (HiddenField)row.FindControl("hfbody");
            HiddenField hfprice = (HiddenField)row.FindControl("hfprice");
            HiddenField hfmileage = (HiddenField)row.FindControl("hfmileage");
            HiddenField hfextcol = (HiddenField)row.FindControl("hfextcol");
            HiddenField hfintcol = (HiddenField)row.FindControl("hfintcol");
            HiddenField hfCRStatus = (HiddenField)row.FindControl("hfCRStatus");
            HiddenField hfPreInv_CRId = (HiddenField)row.FindControl("hfPreInv_CRId");

            hfPreInvID.Value = preInvID.ToString();

            hfvin.Value = lblvin.Text;
            hfcryear.Value = hfyear.Value;
            hfcrmake.Value = hfmake.Value;
            hfcrmodel.Value = hfmodel.Value;
            hfcrbody.Value = hfbody.Value;
            hfcrprice.Value = hfprice.Value;
            hfcrmileage.Value = hfmileage.Value;
            hfcrextcol.Value = hfextcol.Value;
            hfcrintcol.Value = hfintcol.Value;

            lblucrheader.Text = String.Format("[{0} {1} {2} {3}]", lblvin.Text, hfyear.Value, hfmake.Value, hfmodel.Value);
            lblucrheader2.Text = String.Format("[{0} {1} {2} {3}]", lblvin.Text, hfyear.Value, hfmake.Value, hfmodel.Value);
            rbtnListCR.SelectedIndex = 0;
            rbtnCRIdUrl.SelectedIndex = 0;
            //mpeCR.Show();

            if (String.IsNullOrEmpty(hfCRStatus.Value) || hfCRStatus.Value == "0")
            {
                mpeCR.Show();
            }
            else if (hfCRStatus.Value == "10" || hfCRStatus.Value == "20")
            {
                mpeChangeCR.Show();
                hlnkViewReport.NavigateUrl = String.Format("{0}{1}", UCRlinkUrl, hfPreInv_CRId.Value);
            }
            else if (hfCRStatus.Value == "30")
            {
                Response.Redirect(String.Format("{0}{1}", UCRlinkUrl, hfPreInv_CRId.Value));
                //Response.Redirect(String.Format("http://cr.rhollensheadautosales.com/Report/{0}", hfPreInv_CRId.Value));
            }
            lblError1.Visible = false;
            lblError2.Visible = false;

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

        #region[Buyers]
        private void BindBuyers()
        {
            Int32 BuyerParentID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? -1 : Convert.ToInt32(Session["BuyerParent"]);
            if (Convert.ToString(Session["BuyerAccessLevel"]) == "1")
                ddlBuyer.DataSource = BAL.Common.GetMobileBuyerList_ver211(Convert.ToInt32(Session["empId"]) , -1, Convert.ToInt32(Session["LoginEntityTypeID"]), Constant.OrgID);
            else
                ddlBuyer.DataSource = BAL.Common.GetMobileBuyerList_ver211(Convert.ToInt32(Session["empId"]), BuyerParentID, Convert.ToInt32(Session["LoginEntityTypeID"]), Constant.OrgID);
            ddlBuyer.DataValueField = "BuyerId";
            ddlBuyer.DataTextField = "BuyerName";
            ddlBuyer.DataBind();
            ddlBuyer.Items.Insert(0, new ListItem("ALL", "-1"));
            if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
            {
                tdtitle.InnerText = "VIEW ALL Mobile-INVENTORIES";
                if (ddlBuyer.Items.FindByValue(Convert.ToString(Session["UserEntityID"])) != null && BuyerParentID == -1 && ddlBuyer.Items.Count == 2)
                {
                    ddlBuyer.SelectedValue = Convert.ToString(Session["UserEntityID"]);
                    ddlBuyer.Enabled = false;
                }
                else if (ddlBuyer.Items.FindByValue(Convert.ToString(Session["UserEntityID"])) != null)
                    ddlBuyer.SelectedValue = Convert.ToString(Session["UserEntityID"]);
            }


        }
        #endregion

        #region[Close CR popup]
        protected void btnCRcancel_Click(object sender, EventArgs e)
        {
            mpeCR.Hide();
        }
        #endregion

        #region[Duplicate VIN - Inventory and PreInventory]
        protected void ibtnDuplicate_Click(object sender, ImageClickEventArgs e)
        {
            mpeDuplicateVIN.Show();
            long PreInvID = Convert.ToInt64(grvPreInv.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            String VIN = PreInvBAL.GetVINFromPreInventory(PreInvID, Constant.OrgID);
            hdnVIN.Value = VIN;
            BindDuplicatePreInventory();
            dvDuplicatePreInv.Visible = true;
            dvDuplicateInv.Visible = false;
            rbtnlstDuplicateVIN.SelectedValue = "P";
        }
        #endregion

        #region[Duplicate Inventory]
        private void BindDuplicateInventory()
        {
            grdlinkedcarInv.DataSource = PreInvBAL.GetInvLinked(Convert.ToInt64(Session["PreInvID"]), hdnVIN.Value, DuplicatePeriod, Constant.OrgID);
            grdlinkedcarInv.DataBind();
            grdlinkedcarInv.PageIndex = 0;
        }

        protected void grdlinkedcarInv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            mpeDuplicateVIN.Show();
            this.grdlinkedcarInv.PageIndex = e.NewPageIndex;
            BindDuplicateInventory();
        }
        #endregion

        #region[Duplicate pre-inventory]
        private void BindDuplicatePreInventory()
        {
            gvLinkedFilter.DataSource = PreInvBAL.GetPreInvLinked(Convert.ToInt64(Session["PreInvID"]), hdnVIN.Value, DuplicatePeriod, Constant.OrgID);
            gvLinkedFilter.DataBind();
            gvLinkedFilter.PageIndex = 0;
        }

        protected void gvLinkedFilter_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            mpeDuplicateVIN.Show();
            this.gvLinkedFilter.PageIndex = e.NewPageIndex;
            BindDuplicatePreInventory();

        }
        #endregion

        #region[Radio to show duplicate inventory and preinventory]
        protected void rbtnlstDuplicateVIN_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpeDuplicateVIN.Show();
            if (rbtnlstDuplicateVIN.SelectedValue == "I")
            {
                dvDuplicatePreInv.Visible = false;
                dvDuplicateInv.Visible = true;
                BindDuplicateInventory();
            }
            else if (rbtnlstDuplicateVIN.SelectedValue == "P")
            {
                dvDuplicatePreInv.Visible = true;
                dvDuplicateInv.Visible = false;
                BindDuplicatePreInventory();
            }
        }
        #endregion

        #region[Link CR by CRID]
        protected void btncridvalidate_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCRIdUrl.Text))
            {
                iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=1&value={0}&invtype=P&id={1}", txtCRIdUrl.Text, hfPreInvID.Value);
                mpeCR.Hide();
                mpucr.Show();
            }
            else
            {
                lblError1.Visible = true;
                lblError1.Text = "Please provide the CR ID";
            }
        }

        protected void btncridvalidate2_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCRIdUrl2.Text))
            {
                iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=1&value={0}&invtype=P&id={1}", txtCRIdUrl2.Text, hfPreInvID.Value);
                mpeCR.Hide();
                mpucr.Show();
            }
            else
            {
                lblError2.Visible = true;
                lblError2.Text = "Please provide the CR ID";
            }
        }
        #endregion

        #region[Link CR by CRURL]
        protected void btncrurlvalidate_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCRIdUrl.Text))
            {
                iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=2&value={0}&invtype=P&id={1}", txtCRIdUrl.Text, hfPreInvID.Value);
                mpeCR.Hide();
                mpucr.Show();
            }
            else
            {
                lblError1.Visible = true;
                lblError1.Text = "Please provide the CR URL";
            }
        }

        protected void btncrurlvalidate2_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCRIdUrl2.Text))
            {
                iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=2&value={0}&invtype=P&id={1}", txtCRIdUrl2.Text, hfPreInvID.Value);
                mpeCR.Hide();
                mpucr.Show();
            }
            else
            {
                lblError2.Visible = true;
                lblError2.Text = "Please provide the CR URL";
            }
        }
        #endregion

        #region[Link CR by VIN]
        protected void btncrsearch_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=3&value={0}&invtype=P&id={1}", txtCRIdUrl.Text, hfPreInvID.Value);
            mpeCR.Hide();
            mpucr.Show();

        }

        protected void btncrsearch2_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=3&value={0}&invtype=P&id={1}", txtCRIdUrl2.Text, hfPreInvID.Value);
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
            PreInvID = Convert.ToInt64(hfPreInvID.Value);
            //Mobile_PreInventory objInv = new Mobile_PreInventory();
            //objInv = PreInvBAL.GetPreInventoryByID(PreInvID);
            //InventoryID = Convert.ToInt64(objInv.InventoryId);
            PreInvBAL.RemoveCR(PreInvID, InventoryID, Convert.ToInt64(Session["empId"]));
            mpeChangeCR.Hide();
            BindGrid();
        }

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

        protected void grvPreInv_OnSorting(object sender, GridViewSortEventArgs e)
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

        public string FormatEmptyString(object oValue)
        {
            string returnvalue = string.Empty;
            if (!String.IsNullOrEmpty(Convert.ToString(oValue)))
                returnvalue = Convert.ToString(oValue);
            else
                returnvalue = "N/A";
            return returnvalue;
        }

        #region[Email detail popup]
        protected void ibtnEmailStatus_Click(object sender, ImageClickEventArgs e)
        {
            long PreInvID = Convert.ToInt64(grvPreInv.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            //IfrmEmailDetail.Attributes.Add("src", String.Format("EmailDetails.aspx?type=2000&id={0}", PreInvID));
            IfrmEmailDetail.Attributes.Add("src", String.Format("EmailDetails.aspx?type=5001&id={0}", PreInvID));
            mpeEmailDetail.Show();
        }
        #endregion

        public static String InsertNewRow()
        {
            return "</td></tr><tr id=\"trnew\" runat=\"server\"><td colspan=\"13\" style=\"padding-left:30px;\" class=\"GridContent\">";

        }
    }
}
