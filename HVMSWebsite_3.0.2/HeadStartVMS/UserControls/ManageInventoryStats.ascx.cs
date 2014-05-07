using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using METAOPTION.BAL;
using System.Configuration;
using System.Data;
//using DYNAMICWEBTWAINCTRLLib;

namespace METAOPTION.UserControls
{
    public partial class ManageInventoryStats : System.Web.UI.UserControl
    {

        public string UCRlinkUrl = String.Empty;
        public string CreateUCRUrl = String.Empty;
        String InventoryImageUrl = System.Configuration.ConfigurationManager.AppSettings["MobileImagePath"];
        String ExpenseImageUrl = System.Configuration.ConfigurationManager.AppSettings["ExpenseImagePath"];
        String GenericImageUrl = System.Configuration.ConfigurationManager.AppSettings["GenericImagePath"];
        String VIN = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            UCRlinkUrl = ConfigurationManager.AppSettings["UCRlinkUrl"];
            CreateUCRUrl = ConfigurationManager.AppSettings["CreateUCRUrl"];

            if (!IsPostBack)
            {
                if (Request.QueryString["Code"] != null)
                {
                    hfInventoryID.Value = Request.QueryString["Code"];
                    InventoryBAL bal = new InventoryBAL();
                    VIN = bal.GetVINFromInventory(Convert.ToInt64(hfInventoryID.Value));
                    ViewState["VIN"] = VIN;
                }
                CurrentPage = 1;
                CurrentExpPage = 1;
                CurrentGenericPage = 1;
                CurrentUCRPage = 1;
                // Total inv images
                Int64 Total = PreInventoryBAL.GetPreInvImagesByInventoryIDCount(Convert.ToInt64(hfInventoryID.Value));
                // Total exp images
                PreExpenseBAL expBal = new PreExpenseBAL();
                Int64 TotalExpImages = expBal.GetPreExpImagesByInventoryIDCount(Convert.ToInt64(hfInventoryID.Value));

                ViewState["TotalImages"] = Total.ToString();
                if (Total != 0)
                {
                    CollapsiblePanelExtender1.Collapsed = false;
                }
                else
                {
                    CollapsiblePanelExtender1.Collapsed = true;
                    divInventory.Visible = false;
                }
                //dvImagePaging.Visible = false;

                ViewState["TotalExpenseImages"] = TotalExpImages.ToString();
                if (TotalExpImages != 0)
                {
                    CollapsiblePanelExtender2.Collapsed = false;
                }
                else
                {
                    CollapsiblePanelExtender2.Collapsed = true;
                    divExpense.Visible = false;
                }
                //dvExpImagePaging.Visible = false;
                #region[Added by Rupendra 22 Aug 12 for show Generic Images ]
                if (!string.IsNullOrEmpty(VIN))
                {
                    divGeneric.Visible = true;
                    Int64 GenericTotal = PreInventoryBAL.GetGenImagesByGenericIDCount(VIN);
                    if (GenericTotal > 0)
                        ViewState["TotalGenericImages"] = GenericTotal.ToString();
                    else
                        ViewState["TotalGenericImages"] = 0;
                    if (GenericTotal != 0)
                    {
                        CollapsiblePanelExtender3.Collapsed = false;
                        divGeneric.Visible = true;
                    }
                    else
                    {
                        CollapsiblePanelExtender3.Collapsed = true;
                        divGeneric.Visible = false;
                    }
                    if ((CollapsiblePanelExtender1.Collapsed == false) && (CollapsiblePanelExtender2.Collapsed == false))
                        CollapsiblePanelExtender3.Collapsed = true;
                    else
                        CollapsiblePanelExtender3.Collapsed = false;

                    BindGenImages(false);
                }
                else
                {
                    divGeneric.Visible = false;
                }
                #endregion

                #region[Added by Rupendra 17 OCT 12 for show UCR Images ]

                UCRBAL objUCRBAL = new UCRBAL();
                if (!string.IsNullOrEmpty(hfInventoryID.Value))
                {
                    divUCR.Visible = true;
                    // hfInventoryID.Value = "172248";
                    Int64 UCRTotal = UCRBAL.GetUCRImagesCountByInvId(Convert.ToInt64(hfInventoryID.Value));
                    if (UCRTotal > 0)
                        ViewState["TotalUCRImages"] = UCRTotal.ToString();
                    else
                        ViewState["TotalUCRImages"] = 0;
                    if (UCRTotal != 0)
                    {
                        CollapsiblePanelExtender4.Collapsed = false;
                        divUCR.Visible = true;
                    }
                    else
                    {
                        CollapsiblePanelExtender4.Collapsed = true;
                        divUCR.Visible = false;
                    }
                    if ((CollapsiblePanelExtender1.Collapsed == false) && (CollapsiblePanelExtender2.Collapsed == false) && (CollapsiblePanelExtender3.Collapsed == false))
                        CollapsiblePanelExtender4.Collapsed = true;
                    else if ((CollapsiblePanelExtender1.Collapsed == false) && (CollapsiblePanelExtender2.Collapsed == false))
                        CollapsiblePanelExtender4.Collapsed = true;
                    else if ((CollapsiblePanelExtender2.Collapsed == false) && (CollapsiblePanelExtender3.Collapsed == false))
                        CollapsiblePanelExtender4.Collapsed = true;
                    else if ((CollapsiblePanelExtender3.Collapsed == false) && (CollapsiblePanelExtender1.Collapsed == false))
                        CollapsiblePanelExtender4.Collapsed = true;
                    else
                        CollapsiblePanelExtender4.Collapsed = false;

                    BindUCRImages(false);
                }
                else
                {
                    divUCR.Visible = false;
                }
                #endregion

                BindImages(false);
                BindExpImages(false);
                BindExpense();
                CRInfo();
                BindLocation();
                BindTopFiveDocument();
                DisplayCounts();

                DocumentBAL obj = new DocumentBAL();
                DataTable dtYMMB = obj.GetYMMBbyInventoryId(Convert.ToInt32(Request["Code"]));
                lblScan.Text = "Scan Document " + Convert.ToString(dtYMMB.Rows[0]["YMMB"]);
                if (Convert.ToString(Session["LoginEntityTypeID"]) == "1")
                    tblDocument.Visible = false;
            }
        }

        #region[Inventory Images]
        #region[Image box Paging]
        private int CurrentPage
        {
            get
            {
                object objPage = ViewState["_CurrentPage"];
                int _CurrentPage = 0;
                if (objPage == null)
                    _CurrentPage = 0;
                else
                    _CurrentPage = (int)objPage;
                return _CurrentPage;
            }
            set
            { ViewState["_CurrentPage"] = value; }
        }

        private int firstIndex
        {
            get
            {

                int _FirstIndex = 0;
                if (ViewState["_FirstIndex"] == null)
                    _FirstIndex = 0;
                else
                    _FirstIndex = Convert.ToInt32(ViewState["_FirstIndex"]);
                return _FirstIndex;
            }
            set { ViewState["_FirstIndex"] = value; }
        }

        private int lastIndex
        {
            get
            {
                int _LastIndex = 0;
                if (ViewState["_LastIndex"] == null)
                    _LastIndex = 0;
                else
                    _LastIndex = Convert.ToInt32(ViewState["_LastIndex"]);
                return _LastIndex;
            }
            set { ViewState["_LastIndex"] = value; }
        }

        protected void BindImages(bool bNew)
        {
            PagedDataSource pds = new PagedDataSource();
            this.lblCount.Text = "Total Images : " + ViewState["TotalImages"].ToString();
            pds.AllowPaging = true;

            Int32 Count = Convert.ToInt32(ViewState["TotalImages"]);
            Int32 PageSize = pds.PageSize = 4;
            Int32 Total = Count / PageSize;
            if ((Count % PageSize) > 0) Total++;

            if (bNew)
                pds.CurrentPageIndex = 0;
            else
                pds.CurrentPageIndex = CurrentPage;
            if (Total == 0)
                this.lblPageInfo.Text = "Page 0 of 0 |";
            else
                this.lblPageInfo.Text = "Page " + (CurrentPage) + " of " + Total.ToString() + " |";
            this.lnkPrev.Enabled = CurrentPage == 1 ? false : true;
            this.lnkFirst.Enabled = CurrentPage == 1 ? false : true;
            this.lnkNext.Enabled = CurrentPage == Total ? false : true;
            this.lnkLast.Enabled = CurrentPage == Total ? false : true;

            PreInventoryBAL bal = new PreInventoryBAL();
            rptImages.DataSource = bal.GetPreInvImagesByInventoryID(Convert.ToInt64(hfInventoryID.Value), CurrentPage - 1, PageSize);
            rptImages.DataBind();
        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            BindImages(false);
        }

        protected void lnkLast_Click(object sender, EventArgs e)
        {
            CurrentPage = Convert.ToInt32(ViewState["TotalImages"]) / 4;
            if ((Convert.ToInt32(ViewState["TotalImages"]) % 4) > 0) CurrentPage++;
            BindImages(false);
        }

        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 1;
            BindImages(false);
        }

        protected void lnkPrev_Click(object sender, EventArgs e)
        {
            CurrentPage = CurrentPage == 1 ? CurrentPage : CurrentPage -= 1;
            BindImages(false);
        }

        #endregion
        #region[Zoom image]
        protected void ibtnThumbImages_Click(object sender, ImageClickEventArgs e)
        {
            //mpeLargeImage.Show();
            PreInventoryBAL PreInvBAL = new PreInventoryBAL();
            long PreInvID = PreInvBAL.PreInv_ByInvID(Convert.ToInt64(hfInventoryID.Value));
            //frame1.Attributes.Add("src", String.Format("ImageGallery.aspx?id={0}", PreInvID));
            ifrmSlideShow.Attributes.Add("src", String.Format("ViewAllImages.aspx?id={0}&v={1}&preid={2}&comeFrom={3}", hfInventoryID.Value, ViewState["VIN"], PreInvID, "Inventory"));  //ImageGallery.aspx
            //Session["PreInvID"] = PreInvID;
            //mpoImageSlidshow.Show();
            ModalPopupExtender1.Show();
            //ScriptManager.RegisterStartupScript(UpdatePanel5, UpdatePanel5.GetType(), "Response", "openCenteredWindow('" + hfInventoryID.Value + "','" + ViewState["VIN"] + "','" + PreInvID + "','Inventory');", true);
        }
        #endregion
        public String GetImagePath(object Path)
        {
            return String.Format("{0}{1}", InventoryImageUrl, Path);
        }
        #endregion

        #region[Expense Images]
        protected void ibtnExpThumbImages_Click(object sender, ImageClickEventArgs e)
        {
            //mpeLargeExpImage.Show();
            ////Get PreExpenseID
            //long PreExpID = -1;
            //string strPreExpID = string.Empty;
            //InventoryBAL bal = new InventoryBAL();
            //DataTable dt = (DataTable)bal.Exp_PreExp_ByInventoryID(Convert.ToInt64(hfInventoryID.Value));
            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //        strPreExpID += Convert.ToInt64(dt.Rows[i]["PreExpenseID"]) + ",";

            //    if (!string.IsNullOrEmpty(strPreExpID))
            //    {
            //        strPreExpID = strPreExpID.Substring(0, strPreExpID.Length - 1);
            //        // PreExpID = Convert.ToInt64(strPreExpID);
            //    }
            //}
            // IframeExp.Attributes.Add("src", String.Format("ExpenseImageGallery.aspx?id={0}", strPreExpID));
            PreInventoryBAL PreInvBAL = new PreInventoryBAL();
            long PreInvID = PreInvBAL.PreInv_ByInvID(Convert.ToInt64(hfInventoryID.Value));
            //  frame1.Attributes.Add("src", String.Format("ImageGallery.aspx?id={0}", PreInvID));
            ifrmSlideShow.Attributes.Add("src", String.Format("ViewAllImages.aspx?id={0}&v={1}&preid={2}&comeFrom={3}", hfInventoryID.Value, ViewState["VIN"], PreInvID, "Expense"));  //ImageGallery.aspx
            //Session["PreInvID"] = PreInvID;
            //mpoImageSlidshow.Show();
            ModalPopupExtender1.Show();
        }

        public String GetExpImagePath(object Path)
        {
            return String.Format("{0}{1}", ExpenseImageUrl, Path);
        }

        #region[Image box Paging]
        private int CurrentExpPage
        {
            get
            {
                object objPage = ViewState["_CurrentExpPage"];
                int _CurrentExpPage = 0;
                if (objPage == null)
                    _CurrentExpPage = 0;
                else
                    _CurrentExpPage = (int)objPage;
                return _CurrentExpPage;
            }
            set
            { ViewState["_CurrentExpPage"] = value; }
        }

        private int ExpfirstIndex
        {
            get
            {

                int _ExpFirstIndex = 0;
                if (ViewState["_ExpFirstIndex"] == null)
                    _ExpFirstIndex = 0;
                else
                    _ExpFirstIndex = Convert.ToInt32(ViewState["_ExpFirstIndex"]);
                return _ExpFirstIndex;
            }
            set { ViewState["_ExpFirstIndex"] = value; }
        }

        private int ExplastIndex
        {
            get
            {
                int _ExpLastIndex = 0;
                if (ViewState["_ExpLastIndex"] == null)
                    _ExpLastIndex = 0;
                else
                    _ExpLastIndex = Convert.ToInt32(ViewState["_ExpLastIndex"]);
                return _ExpLastIndex;
            }
            set { ViewState["_ExpLastIndex"] = value; }
        }

        protected void BindExpImages(bool bNew)
        {
            PagedDataSource pds = new PagedDataSource();
            this.lblExpImgCount.Text = "Total Images : " + ViewState["TotalExpenseImages"].ToString();
            pds.AllowPaging = true;

            Int32 Count = Convert.ToInt32(ViewState["TotalExpenseImages"]);
            Int32 PageSize = pds.PageSize = 4;
            Int32 Total = Count / PageSize;
            if ((Count % PageSize) > 0) Total++;

            if (bNew)
                pds.CurrentPageIndex = 0;
            else
                pds.CurrentPageIndex = CurrentExpPage;
            if (Total == 0)
                this.lblExpPageInfo.Text = "Page 0 of 0 |";
            else
                this.lblExpPageInfo.Text = "Page " + (CurrentExpPage) + " of " + Total.ToString() + " |";
            this.lnkExpPrev.Enabled = CurrentExpPage == 1 ? false : true;
            this.lnkExpFirst.Enabled = CurrentExpPage == 1 ? false : true;
            this.lnkExpNext.Enabled = CurrentExpPage == Total ? false : true;
            this.lnkExpLast.Enabled = CurrentExpPage == Total ? false : true;

            PreExpenseBAL bal = new PreExpenseBAL();
            dlExpenseImages.DataSource = bal.GetPreExpenseImagesByInventoryID(Convert.ToInt64(hfInventoryID.Value), CurrentExpPage - 1, PageSize);
            dlExpenseImages.DataBind();
        }

        protected void lnkExpNext_Click(object sender, EventArgs e)
        {
            CurrentExpPage += 1;
            BindExpImages(false);
        }

        protected void lnkExpLast_Click(object sender, EventArgs e)
        {
            CurrentExpPage = Convert.ToInt32(ViewState["TotalExpenseImages"]) / 4;
            if ((Convert.ToInt32(ViewState["TotalExpenseImages"]) % 4) > 0) CurrentExpPage++;
            BindExpImages(false);
        }

        protected void lnkExpFirst_Click(object sender, EventArgs e)
        {
            CurrentExpPage = 1;
            BindExpImages(false);
        }

        protected void lnkExpPrev_Click(object sender, EventArgs e)
        {
            CurrentExpPage = CurrentExpPage == 1 ? CurrentExpPage : CurrentExpPage -= 1;
            BindExpImages(false);
        }

        #endregion
        #endregion

        #region[UCR]
        #region[CR Info]
        protected void CRInfo()
        {
            InventoryBAL bal = new InventoryBAL();
            InventoryDetails_ByInventoryIDResult objInvDetails = InventoryBAL.GetInventoryDetailsByInventoryID(Convert.ToInt64(hfInventoryID.Value));

            if (objInvDetails != null)
            {
                Int32 CRStatus = Convert.ToInt32(objInvDetails.CR_Status);
                if (CRStatus == 0)
                {
                    lblCRId.Text = "Not Available";
                    imggCR.ToolTip = "Not Ready";
                    ibtnCR.ToolTip = "Not Ready";
                    ibtnCR.ImageUrl = "~/Images/ucr-btn1.png";
                    imggCR.ImageUrl = "~/Images/ucr-btn1.png";
                    ancUCRUrl.Visible = false;
                    lblUCRUrl.Visible = true;
                }
                else if (CRStatus == 10 || CRStatus == 20)
                {
                    lblCRId.Text = Convert.ToString(objInvDetails.CR_ID);
                    ibtnCR.ToolTip = "Initiated";
                    imggCR.ToolTip = "Initiated";
                    ibtnCR.ImageUrl = "~/Images/ucr-btn2.png";
                    imggCR.ImageUrl = "~/Images/ucr-btn2.png";
                    ancUCRUrl.Visible = true;
                    lblUCRUrl.Visible = false;
                }
                else if (CRStatus == 30)
                {
                    lblCRId.Text = Convert.ToString(objInvDetails.CR_ID);
                    ibtnCR.ToolTip = "Available";
                    ibtnCR.ImageUrl = "~/Images/ucr-btn.png";
                    imggCR.ToolTip = "Available";
                    imggCR.ImageUrl = "~/Images/ucr-btn.png";
                    ancUCRUrl.Visible = true;
                    lblUCRUrl.Visible = false;
                }

                hfInventoryID.Value = Convert.ToString(objInvDetails.InventoryId);
                hfvin.Value = objInvDetails.VIN;
                hfcryear.Value = Convert.ToString(objInvDetails.Year);
                hfcrmake.Value = objInvDetails.MakeName;
                hfcrmodel.Value = objInvDetails.ModelName;
                hfcrbody.Value = objInvDetails.Body;
                hfcrprice.Value = Convert.ToString(objInvDetails.CarCost);
                hfcrmileage.Value = Convert.ToString(objInvDetails.MileageIn);
                hfcrextcol.Value = objInvDetails.ExtColor;
                hfcrintcol.Value = objInvDetails.IntColor;

                String URL = String.Format("{0}{1}", UCRlinkUrl, objInvDetails.CR_ID);
                //String URL = String.Format("http://web.metaoptionllc.com:82/Report/{0}", objInvDetails.CR_ID);
                ancUCRUrl.HRef = URL;
                if (Convert.ToString(Session["LoginEntityTypeID"]) == "2" || Convert.ToString(Session["LoginEntityTypeID"]) == "1")
                {

                    if (CRStatus == 0)
                    {
                        ibtnCR.Visible = false;
                        ancrCR.Visible = true;
                        ancrCR.Enabled = false;
                    }
                    else if (CRStatus == 10 || CRStatus == 20 || CRStatus == 30)
                    {
                        ibtnCR.Visible = false;
                        ancrCR.Visible = true;
                        ancrCR.NavigateUrl = "http://www.universalconditionreport.com/Report/" + objInvDetails.CR_ID;
                    }


                }
            }
        }
        #endregion
        #region[CR button click event]
        protected void ibtnCR_Click(object sender, ImageClickEventArgs e)
        {
            InventoryBAL bal = new InventoryBAL();
            InventoryDetails_ByInventoryIDResult objInvDetails = InventoryBAL.GetInventoryDetailsByInventoryID(Convert.ToInt64(hfInventoryID.Value));

            hfInventoryID.Value = Convert.ToString(objInvDetails.InventoryId);
            hfvin.Value = objInvDetails.VIN;
            hfcryear.Value = Convert.ToString(objInvDetails.Year);
            hfcrmake.Value = objInvDetails.MakeName;
            hfcrmodel.Value = objInvDetails.ModelName;
            hfcrbody.Value = objInvDetails.Body;
            hfcrprice.Value = Convert.ToString(objInvDetails.CarCost);
            hfcrmileage.Value = Convert.ToString(objInvDetails.MileageIn);
            hfcrextcol.Value = objInvDetails.ExtColor;
            hfcrintcol.Value = objInvDetails.IntColor;

            Int32 CRStatus = Convert.ToInt32(objInvDetails.CR_Status);
            if (CRStatus == 0)
                mpeAddCR.Show();
            else if (CRStatus == 10 || CRStatus == 20 || CRStatus == 30)
            {
                mpeEditCR.Show();
                hylnkViewReport.NavigateUrl = String.Format("{0}{1}", UCRlinkUrl, objInvDetails.CR_ID);
            }
            //else if (CRStatus == 30)
            //    Response.Redirect(String.Format("{0}{1}", UCRlinkUrl, objInvDetails.CR_ID));
            //Response.Redirect(String.Format("http://web.metaoptionllc.com:82/Report/{0}", objInvDetails.CR_ID));
        }
        #endregion
        #region[Add/Link CR - show hide link option]
        protected void rbtnListAddUCR_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpeAddCR.Show();
        }
        #endregion
        #region[Add/Link CR - ID/URL]
        protected void rbtnListAddUCRIdUrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpeAddCR.Show();
        }
        #endregion
        #region[Close CR popup]
        protected void btnAddUCRCancel_Click(object sender, EventArgs e)
        {
            mpeAddCR.Hide();
        }
        #endregion
        #region[Link CR by CRID]
        protected void btnAddUCRIdValidate_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=1&value={0}&invtype=I&id={1}", txtAddUCRIdUrl.Text, hfInventoryID.Value);
            mpeAddCR.Hide();
            mpucr.Show();

        }

        protected void btnEditUCRIdValidate_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=1&value={0}&invtype=I&id={1}", txtEditUCRIdUrl.Text, hfInventoryID.Value);
            mpeAddCR.Hide();
            mpucr.Show();

        }
        #endregion
        #region[Link CR by CRURL]
        protected void btnAddUCRUrlValidate_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=2&value={0}&invtype=I&id={1}", txtAddUCRIdUrl.Text, hfInventoryID.Value);
            mpeAddCR.Hide();
            mpucr.Show();

        }

        protected void btnEditUCRUrlValidate_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=2&value={0}&invtype=I&id={1}", txtEditUCRIdUrl.Text, hfInventoryID.Value);
            mpeAddCR.Hide();
            mpucr.Show();

        }
        #endregion
        #region[Link CR by VIN]
        protected void btnAddUCRSearch_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=3&value={0}&invtype=I&id={1}", txtAddUCRIdUrl.Text, hfInventoryID.Value);
            mpeAddCR.Hide();
            mpucr.Show();

        }

        protected void btnEditUCRSearch_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=3&value={0}&invtype=I&id={1}", txtEditUCRIdUrl.Text, hfInventoryID.Value);
            mpeAddCR.Hide();
            mpucr.Show();

        }
        #endregion

        protected void btnucrcancel_Click(object sender, EventArgs e)
        {
            mpucr.Hide();
        }

        protected void btnAddUCROk_Click(object sender, EventArgs e)
        {

        }

        protected void btnEditUCROk_Click(object sender, EventArgs e)
        {
            long PreInvID = 0, InventoryID = 0;

            InventoryID = Convert.ToInt64(hfInventoryID.Value);
            PreInventoryBAL PreInvBAL = new PreInventoryBAL();
            PreInvBAL.RemoveCR(PreInvID, InventoryID, Convert.ToInt64(Session["empId"]));
            mpeEditCR.Hide();
            //BindGrid();
            CRInfo();
        }
        #endregion

        #region[Expense]
        protected void BindExpense()
        {
            InventoryBAL bal = new InventoryBAL();
            gvExpense.DataSource = bal.GetExpense_ByInventoryID(Convert.ToInt64(hfInventoryID.Value), Convert.ToString(Session["LoginEntityTypeID"]));
            gvExpense.DataBind();

            hplViewExpenseDetails.NavigateUrl = String.Format("../UI/InventoryExpense.aspx?Code={0}", hfInventoryID.Value);
        }
        #endregion

        #region[Location]
        #region[Bind Location]
        protected void BindLocation()
        {
            InventoryBAL bal = new InventoryBAL();
            gvLocation.DataSource = bal.GetLocation_ByVIN(VIN, Constant.OrgID);
            gvLocation.DataBind();

            if (gvLocation.Rows.Count == 0)
                trLocationDetailsFooter.Visible = false;
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
        #region[Location Detail]
        protected void BindAllLocation()
        {
            InventoryBAL Invbal = new InventoryBAL();
            String VINNo = Invbal.GetVINFromInventory(Convert.ToInt64(hfInventoryID.Value));
            ObjectDataSource odsLocation = new ObjectDataSource();
            odsLocation.TypeName = "Metaoption.LocationBAL";
            odsLocation.SelectMethod = "FetchVINLocation";
            odsLocation.SelectCountMethod = "FetchVINLocationCount";
            odsLocation.EnablePaging = true;
            odsLocation.SelectParameters.Add("VIN", VINNo);
            odsLocation.SelectParameters.Add("LocationID", "-1");
            odsLocation.SelectParameters.Add("AddedBy", "-1");
            odsLocation.SelectParameters.Add("StartRowIndex", DbType.Int32, gvAllLocation.PageIndex.ToString());
            odsLocation.SelectParameters.Add("MaximumRows", DbType.Int32, gvAllLocation.PageSize.ToString());
            odsLocation.SelectParameters.Add("OrgID", DbType.Int16, Constant.OrgID.ToString());
            gvAllLocation.DataSource = odsLocation;
            gvAllLocation.DataBind();
        }

        protected void btnLocationDetails_Click(object sender, EventArgs e)
        {
            mpeLocation.Show();
            BindAllLocation();
        }

        protected void gvAllLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            mpeLocation.Show();
            gvAllLocation.PageIndex = e.NewPageIndex;
            BindAllLocation();
        }
        #endregion
        #endregion

        #region[Generic Images]
        protected void ibtnGenericThumbImages_Click(object sender, ImageClickEventArgs e)
        {
            PreInventoryBAL PreInvBAL = new PreInventoryBAL();
            long PreInvID = PreInvBAL.PreInv_ByInvID(Convert.ToInt64(hfInventoryID.Value));
            //  frame1.Attributes.Add("src", String.Format("ImageGallery.aspx?id={0}", PreInvID));
            ifrmSlideShow.Attributes.Add("src", String.Format("ViewAllImages.aspx?id={0}&v={1}&preid={2}&comeFrom={3}", hfInventoryID.Value, ViewState["VIN"], PreInvID, "Generic"));  //ImageGallery.aspx
            //Session["PreInvID"] = PreInvID;
            //mpoImageSlidshow.Show();
            ModalPopupExtender1.Show();

            //ScriptManager.RegisterStartupScript(UpdatePanel5, UpdatePanel5.GetType(), "Response", "centerPopup();", true);
            // MPEGenericImage.Show();
            //IframeGeneric.Attributes.Add("src", String.Format("GenericImageGallery.aspx?i=-1&t=3&p=30&v=" + ViewState["VIN"]));
        }

        public String GetGenericImagePath(object Path)
        {
            return String.Format("{0}{1}", GenericImageUrl, Path);
        }

        #region[Image box Paging]
        private int CurrentGenericPage
        {
            get
            {
                object objGenPage = this.ViewState["_CurrentGenericPage"];
                int _CurrentGenericPage = 0;
                if (objGenPage == null)
                    _CurrentGenericPage = 0;
                else
                    _CurrentGenericPage = (int)objGenPage;
                return _CurrentGenericPage;
            }
            set
            { this.ViewState["_CurrentGenericPage"] = value; }
        }

        private int GenericfirstIndex
        {
            get
            {

                int _GenericFirstIndex = 0;
                if (ViewState["_GenericFirstIndex"] == null)
                    _GenericFirstIndex = 0;
                else
                    _GenericFirstIndex = Convert.ToInt32(ViewState["_GenericFirstIndex"]);
                return _GenericFirstIndex;
            }
            set { ViewState["_GenericFirstIndex"] = value; }
        }

        private int GenericlastIndex
        {
            get
            {
                int _GenericLastIndex = 0;
                if (ViewState["_GenericLastIndex"] == null)
                    _GenericLastIndex = 0;
                else
                    _GenericLastIndex = Convert.ToInt32(ViewState["_GenericLastIndex"]);
                return _GenericLastIndex;
            }
            set { ViewState["_GenericLastIndex"] = value; }
        }

        protected void BindGenImages(bool bNew)
        {
            PreInventoryBAL bal = new PreInventoryBAL();
            PagedDataSource objGenericImage = new PagedDataSource();
            this.lblGenImgCount.Text = "Total Images : " + ViewState["TotalGenericImages"].ToString();
            objGenericImage.AllowPaging = true;

            Int32 Count = Convert.ToInt32(ViewState["TotalGenericImages"]);
            Int32 PageSize = objGenericImage.PageSize = 4;
            Int32 Total = Count / PageSize;
            if ((Count % PageSize) > 0) Total++;

            if (bNew)
                objGenericImage.CurrentPageIndex = 0;
            else
                objGenericImage.CurrentPageIndex = CurrentGenericPage;
            if (Total == 0)
                this.lblGenPageInfo.Text = "Page 0 of 0 |";
            else
                this.lblGenPageInfo.Text = "Page " + (CurrentGenericPage) + " of " + Total.ToString() + " |";
            this.lnkGenPrev.Enabled = CurrentGenericPage == 1 ? false : true;
            this.lnkGenFirst.Enabled = CurrentGenericPage == 1 ? false : true;
            this.lnkGenNext.Enabled = CurrentGenericPage == Total ? false : true;
            this.lnkGenLast.Enabled = CurrentGenericPage == Total ? false : true;


            dlGenericImages.DataSource = bal.GetGenericImagesByGenericID(ViewState["VIN"].ToString(), CurrentGenericPage - 1, PageSize);
            dlGenericImages.DataBind();
        }

        protected void dlGenericImages_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdnDescription = (HiddenField)e.Item.FindControl("hdnDescription");
                if (!string.IsNullOrEmpty(hdnDescription.Value))
                {
                    lblDescription.Text = "Note : " + hdnDescription.Value;
                }
                else
                    lblDescription.Visible = false;

            }
        }

        protected void lnkGenericNext_Click(object sender, EventArgs e)
        {
            CurrentGenericPage += 1;
            BindGenImages(false);
        }

        protected void lnkGenericLast_Click(object sender, EventArgs e)
        {
            CurrentGenericPage = Convert.ToInt32(ViewState["TotalGenericImages"]) / 4;
            if ((Convert.ToInt32(ViewState["TotalGenericImages"]) % 4) > 0) CurrentGenericPage++;
            BindGenImages(false);
        }

        protected void lnkGenericFirst_Click(object sender, EventArgs e)
        {
            CurrentGenericPage = 1;
            BindGenImages(false);
        }

        protected void lnkGenericPrev_Click(object sender, EventArgs e)
        {
            CurrentGenericPage = CurrentGenericPage == 1 ? CurrentGenericPage : CurrentGenericPage -= 1;
            BindGenImages(false);
        }

        #endregion


        #endregion

        #region[UCR Images Added by Rupendra 17 Oct 12]
        protected void ibtnUCRThumbImages_Click(object sender, ImageClickEventArgs e)
        {
            PreInventoryBAL PreInvBAL = new PreInventoryBAL();
            long PreInvID = PreInvBAL.PreInv_ByInvID(Convert.ToInt64(hfInventoryID.Value));
            //  frame1.Attributes.Add("src", String.Format("ImageGallery.aspx?id={0}", PreInvID));
            ifrmSlideShow.Attributes.Add("src", String.Format("ViewAllImages.aspx?id={0}&v={1}&preid={2}&comeFrom={3}", hfInventoryID.Value, ViewState["VIN"], PreInvID, "UCR"));  //ImageGallery.aspx
            //Session["PreInvID"] = PreInvID;
            //mpoImageSlidshow.Show();
            ModalPopupExtender1.Show();
            //MPEGenericImage.Show();
            // IframeGeneric.Attributes.Add("src", String.Format("GenericImageGallery.aspx?i=-1&t=3&p=30&v=" + ViewState["VIN"]));
        }

        public String GetUCRImagePath(object Path)
        {
            String str = String.Empty;
            try
            {
                str = METAOPTION.WS.DecodeUCRVirtualPah.GetUCRPhysicalPath(Path.ToString());
                if (String.IsNullOrEmpty(str))
                    str = "../Images/nocar.PNG";

            }
            catch (Exception ex)
            {

            }
            return str;

        }

        #region[Image box Paging]
        private int CurrentUCRPage
        {
            get
            {
                object objUCRPage = this.ViewState["_CurrentUCRPage"];
                int _CurrentUCRPage = 0;
                if (objUCRPage == null)
                    _CurrentUCRPage = 0;
                else
                    _CurrentUCRPage = (int)objUCRPage;
                return _CurrentUCRPage;
            }
            set
            { this.ViewState["_CurrentUCRPage"] = value; }
        }

        private int UCRfirstIndex
        {
            get
            {

                int _UCRFirstIndex = 0;
                if (ViewState["_UCRFirstIndex"] == null)
                    _UCRFirstIndex = 0;
                else
                    _UCRFirstIndex = Convert.ToInt32(ViewState["_UCRFirstIndex"]);
                return _UCRFirstIndex;
            }
            set { ViewState["_UCRFirstIndex"] = value; }
        }

        private int UCRlastIndex
        {
            get
            {
                int _UCRLastIndex = 0;
                if (ViewState["_UCRLastIndex"] == null)
                    _UCRLastIndex = 0;
                else
                    _UCRLastIndex = Convert.ToInt32(ViewState["_UCRLastIndex"]);
                return _UCRLastIndex;
            }
            set { ViewState["_UCRLastIndex"] = value; }
        }

        protected void BindUCRImages(bool bNew)
        {
            UCRBAL objUCRBAL = new UCRBAL();
            PagedDataSource objUCRImage = new PagedDataSource();
            this.lblUCRCount.Text = "Total Images : " + ViewState["TotalUCRImages"].ToString();
            objUCRImage.AllowPaging = true;

            Int32 Count = Convert.ToInt32(ViewState["TotalUCRImages"]);
            Int32 PageSize = objUCRImage.PageSize = 4;
            Int32 Total = Count / PageSize;
            if ((Count % PageSize) > 0) Total++;

            if (bNew)
                objUCRImage.CurrentPageIndex = 0;
            else
                objUCRImage.CurrentPageIndex = CurrentUCRPage;
            if (Total == 0)
                this.lblUCRInfo.Text = "Page 0 of 0 |";
            else
                this.lblUCRInfo.Text = "Page " + (CurrentUCRPage) + " of " + Total.ToString() + " |";
            this.lnkUCRPre.Enabled = CurrentUCRPage == 1 ? false : true;
            this.lnkUCRFirst.Enabled = CurrentUCRPage == 1 ? false : true;
            this.lnkUCRNext.Enabled = CurrentUCRPage == Total ? false : true;
            this.lnkUCRLast.Enabled = CurrentUCRPage == Total ? false : true;

            // hfInventoryID.Value = "172248";
            dlUCRImages.DataSource = objUCRBAL.GetUCRImagesByInvID(Convert.ToInt64(hfInventoryID.Value), CurrentGenericPage - 1, PageSize);
            dlUCRImages.DataBind();
        }

        protected void dlUCRImages_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdnDescription = (HiddenField)e.Item.FindControl("hdnUCRDescription");
                if (!string.IsNullOrEmpty(hdnDescription.Value))
                {
                    lblDescription.Text = "Note : " + hdnDescription.Value;
                }
                else
                    lblDescription.Visible = false;

            }
        }

        protected void lnkUCRNext_Click(object sender, EventArgs e)
        {
            CurrentUCRPage += 1;
            BindUCRImages(false);
        }

        protected void lnkUCRLast_Click(object sender, EventArgs e)
        {
            CurrentUCRPage = Convert.ToInt32(ViewState["TotalUCRImages"]) / 4;
            if ((Convert.ToInt32(ViewState["TotalUCRImages"]) % 4) > 0) CurrentUCRPage++;
            BindUCRImages(false);
        }

        protected void lnkUCRFirst_Click(object sender, EventArgs e)
        {
            CurrentUCRPage = 1;
            BindUCRImages(false);
        }

        protected void lnkUCRPrev_Click(object sender, EventArgs e)
        {
            CurrentUCRPage = CurrentGenericPage == 1 ? CurrentUCRPage : CurrentUCRPage -= 1;
            BindUCRImages(false);
        }

        #endregion
        #endregion

        #region[Document]
        protected void btnScan_Click(object sender, EventArgs e)
        {
            mpeScan.Show();
            // Open Scanner.aspx screen in iframe
            // Send EntityTypeID and EntityID in querystring
            // entityTypeID = 6 (Inventory)  
            DataTable result = new DataTable();
            DocumentBAL obj = new DocumentBAL();
            if (!string.IsNullOrEmpty(hfInventoryID.Value))
                result = obj.GetYearMakeByInventoryId(Convert.ToInt32(hfInventoryID.Value));
            frmScanner.Attributes.Add("src", String.Format("Scanner.aspx?etid={0}&eid={1}&VIN={2}&Year={3}&Make={4}", 6, hfInventoryID.Value, result.Rows[0]["VIN"], result.Rows[0]["Year"], result.Rows[0]["Make"]));
            //Response.Redirect(String.Format("Scanner.aspx?etid={0}&eid={1}&VIN={2}&Year={3}&Make={4}", 6, hfInventoryID.Value, result.Rows[0]["VIN"], result.Rows[0]["Year"], result.Rows[0]["Make"]));

        }

        ////Added by Rupendra 1 Nov 2012 to get top 5 Document/////////////
        public void BindTopFiveDocument()
        {
            DataTable dtDocument = new DataTable();
            if (Request.QueryString["Code"] != null)
            {
                DocumentBAL objBAL = new DocumentBAL();
                dtDocument = objBAL.BindTopFiveDocument(Convert.ToInt32(Request.QueryString["Code"]), Constant.OrgID);
                gvDocument.DataSource = dtDocument;
                gvDocument.DataBind();
            }
            if (Convert.ToString(Session["LoginEntityTypeID"]) == "2" || Convert.ToString(Session["LoginEntityTypeID"]) == "1")
            {
                hplViewExpenseDetails.Visible = false;
                btnScan.Visible = false;
                lnkDocuments.Visible = false;
                if (dtDocument.Rows.Count == 0)
                    tblDocument.Visible = false;
            }

            //if (dtDocument.Rows.Count > 0)
            //    hlnkViewAllScan.Text = "View All " + "(" + dtDocument.Rows[0]["TotalCount"] + ")";
            //else
            //    hlnkViewAllScan.Text = "View All (0)";
        }


        protected void lnkDocument_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkDocument = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnkDocument.NamingContainer;
                long DocumentID = Convert.ToInt64(gvDocument.DataKeys[((GridViewRow)(sender as LinkButton).NamingContainer).RowIndex].Values[0]);
                Iframe1.Attributes.Add("src", String.Format("DocumentViewer.aspx?Id={0}", DocumentID));
                ModalPopupExtender2.Show();
            }
            catch (Exception ex) { }
        }

        protected void lnkDocuments_Click(object sender, EventArgs e)
        {
            Response.Redirect("InventoryDocuments.aspx?Code=" + Request.QueryString["Code"]);
        }


        private void DisplayCounts()
        {
            int Code = 0;
            if (Request.QueryString["Code"] != null)
                Code = Convert.ToInt32(Request.QueryString["Code"]);
            if (Code != 0 && Code != -1)
            {
                InventoryBAL bal = new InventoryBAL();
                List<Count_ExpInvDocDriverResult> Count = bal.Count_ExpInvDocDriver(Code);
                lnkDocuments.Text = "View All (" + Count[0].TotalDocument + ")  ";

            }
        }


        protected void btnImgDocument_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton ibtncars = (ImageButton)sender;
                GridViewRow row = (GridViewRow)ibtncars.NamingContainer;
                long DocumentID = Convert.ToInt64(gvDocument.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
                Iframe1.Attributes.Add("src", String.Format("DocumentViewer.aspx?Id={0}", DocumentID));
                ModalPopupExtender2.Show();
            }
            catch (Exception ex) { }
        }

        protected void imgDownload_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DocumentBAL objDocument = new DocumentBAL();
                string ContentType = string.Empty;
                string ContentTypeValue = string.Empty;
                ImageButton ibtncars = (ImageButton)sender;
                GridViewRow row = (GridViewRow)ibtncars.NamingContainer;
                long DocumentID = Convert.ToInt64(gvDocument.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);

                DataTable dtFile = new DataTable();
                dtFile = objDocument.GetDocumentBinarybyDocId(DocumentID);
                if (dtFile.Rows.Count > 0)
                {
                    byte[] buffer = (byte[])dtFile.Rows[0]["DocumentBinary"];
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.ClearContent();
                    if (!string.IsNullOrEmpty(Convert.ToString(dtFile.Rows[0]["DocumentName"])))
                    {
                        ContentType = Convert.ToString(dtFile.Rows[0]["DocumentName"]);
                        String extn = ContentType.Substring(ContentType.LastIndexOf("."));
                        switch (extn.ToLower())
                        {
                            case ".jpg":
                                ContentTypeValue = "image/jpeg";
                                break;
                            case ".jpeg":
                                ContentTypeValue = "image/jpeg";
                                break;
                            case ".png":
                                ContentTypeValue = "image/png";
                                break;
                            case ".pdf":
                                ContentTypeValue = "application/pdf";
                                break;
                            default:
                                ContentTypeValue = "image/jpeg";
                                break;
                        }
                    }
                    Response.AddHeader("Content-type", ContentTypeValue);
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Convert.ToString(dtFile.Rows[0]["DocumentName"]).Replace(" ", "_") + "");
                    Response.AddHeader("Content-Length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);
                    Response.Flush();
                    Response.End();
                }

            }
            catch (Exception ex) { }
        }

        protected void gvDocument_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //ImageButton btnImgDocument = (ImageButton)e.Row.FindControl("btnImgDocument");
                Label lblDocumentName = (Label)e.Row.FindControl("lblDocumentName");
                HiddenField hdnFileName = (HiddenField)e.Row.FindControl("hdnFileName");
                lblDocumentName.ToolTip = hdnFileName.Value;
                //if (!string.IsNullOrEmpty(hdnFileFormat.Value))
                //{
                //    if ((hdnFileFormat.Value == "image/png") || (hdnFileFormat.Value == ".png") || (hdnFileFormat.Value == "image/jpeg") || (hdnFileFormat.Value == "image/jpg") || (hdnFileFormat.Value == ".jpg"))
                //    {
                //        btnImgDocument.Visible = true;
                //        imgDownload.Visible = false;
                //    }
                //    else
                //    {
                //        btnImgDocument.Visible = false;
                //        imgDownload.Visible = true;
                //    }
                //}
                //  ImageButton lb = e.Row.FindControl("imgDownload") as ImageButton;
                //ScriptManager.GetCurrent(this).RegisterPostBackControl(lb);
            }
        }
        ////////////////////////End///////////////////////////////////////
        #endregion
    }
}