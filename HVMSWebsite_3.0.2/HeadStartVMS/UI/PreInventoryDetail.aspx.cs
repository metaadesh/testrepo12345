using System;
using System.Collections;
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
using METAOPTION;
using METAOPTION.BAL;
using System.Text;

namespace METAOPTION.UI
{
    public partial class PreInventoryDetail : System.Web.UI.Page
    {
        PreInventoryBAL PreInvBAL = new PreInventoryBAL();
        Int64 _Code = 0;
        String VIN = String.Empty;
        PreInventoryMaster pmaster;
        public string UCRlinkUrl = String.Empty;
        public string CreateUCRUrl = String.Empty;

        public string crVin = string.Empty;
        public string crPreInvID = string.Empty;
        public string crYear = string.Empty;
        public string crMake = string.Empty;
        public string crModel = string.Empty;
        public string crBody = string.Empty;
        public string crPrice = string.Empty;
        public string crMileage = string.Empty;
        public string crExtCol = string.Empty;
        public string crIntCol = string.Empty;
        public int DuplicatePeriod = 0;


        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["PreInventoryMaster"])))
                    this.MasterPageFile = Convert.ToString(Session["PreInventoryMaster"]);
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
            DuplicatePeriod = Convert.ToInt32(ConfigurationManager.AppSettings["DuplicateInDays"]);

            if (!IsPostBack)
            {
                if (Session["PreInvID"] != null)
                {
                    BindPreInvGrid();
                    _Code = Convert.ToInt64(Session["PreInvID"]);
                    Bind_PreInvLinkedCars(Convert.ToInt64(Session["PreInvID"]));
                    Bind_InvLinkedCars(Convert.ToInt64(Session["PreInvID"]));
                    frame1.Attributes.Add("src", String.Format("ImageGallery.aspx?id={0}", _Code));
                    Int32? CRId = PreInvBAL.CRID_ByPreInvID(_Code);
                    hfCRId.Value = Convert.ToString(CRId);
                }
            }

            if (Request.QueryString.HasKeys())
            {
                _Code = Convert.ToInt64(Request.QueryString["Code"]);
                Session["PreInvID"] = _Code;
                BindPreInvGrid();
                Bind_PreInvLinkedCars(Convert.ToInt64(Session["PreInvID"]));
                Bind_InvLinkedCars(Convert.ToInt64(Session["PreInvID"]));
            }

            OkButton.OnClientClick = String.Format("fnClickUpdate('{0}','{1}')", OkButton.UniqueID, "");
            if (Convert.ToString(Session["LoginEntityTypeID"]) == "2" || Convert.ToString(Session["LoginEntityTypeID"]) == "1")
            {
                btnCreateUcr.Visible = false;
                btnAdd.Visible =false;
                btnDeleteInventory.Visible =false;
            }
            
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            pmaster = Page.Master as PreInventoryMaster;
            if (pmaster != null)
            {
                if (pmaster.SyncImagesCount > 0)
                    panOpen.Visible = true;
                else
                    panOpen.Visible = false;
            }

        }

        public void BindPreInvGrid()
        {
            String DeleteTooltip = String.Empty;
            DataTable dt = new DataTable();
            DataTable dtdealer = new DataTable();
            StringBuilder DealerInfo=new StringBuilder();
            bool isPending = false, isRejected = false;
            Int32 CRStatus = 0;
            dt = PreInvBAL.GetPreInventoryDetailByID(Convert.ToInt64(Session["PreInvID"]));
            rptdetail.DataSource = dt;
            rptdetail.DataBind();
            if (dt.Rows.Count > 0)
            {
                ExtractCrInfo(dt);

                VIN = Convert.ToString(dt.Rows[0]["VIN"]);
                dtdealer = PreInvBAL.GetDealerInfoByID(Convert.ToInt64(dt.Rows[0]["DealerID"]));
                isPending = Convert.ToBoolean(dt.Rows[0]["IsPending"]);
                isRejected = Convert.ToBoolean(dt.Rows[0]["IsRejected"]);
                CRStatus = Convert.ToInt32((String.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["CR_Status"]))) ? "0" : dt.Rows[0]["CR_Status"]);
                lblucrheader.Text = String.Format("[{0} {1} {2} {3}]", VIN, dt.Rows[0]["Year"], dt.Rows[0]["Make"], dt.Rows[0]["Model"]);
                lblucrheader2.Text = String.Format("[{0} {1} {2} {3}]", VIN, dt.Rows[0]["Year"], dt.Rows[0]["Make"], dt.Rows[0]["Model"]);
                
                if (Convert.ToInt64(dt.Rows[0]["InvID"]) > 0)
                {
                    btnAdd.Visible = false;
                    btnDeleteInventory.Visible = false;
                    lblCarStatus.Text = "Car Details [Status: Added To Inventory]";
                    btnPending.Visible = false;
                }
                else if (isPending == true && isRejected==false)
                {
                    hfpending.Value = "true";
                    lblCarStatus.Text = "Car Details [Status: Pending]";
                    btnPending.Visible = false;
                    btnAdd.Visible = true;
                    btnDeleteInventory.Visible = true;
                }
                else if(isPending==true && isRejected==true) //Rejected
                {
                    btnPending.Text = "Make Pending";
                    hfpending.Value = "false";
                    btnAdd.Visible = false;
                    btnDeleteInventory.Visible = false;
                    btnPending.Visible = true;
                    lblCarStatus.Text = "Car Details [Status: Rejected]";
                }

                if (CRStatus == 0)
                {
                    lblCRStatus.Text = "[UCR Status: Not Ready]";
                    btnCreateUcr.Visible = true;
                    btnChangeUcr.Visible = false;
                    btnViewUCR.Visible = false;
                }
                else if (CRStatus == 10 || CRStatus == 20)
                {
                    lblCRStatus.Text = "[UCR Status: Initiated]";
                    btnCreateUcr.Visible = false;
                    btnChangeUcr.Visible = true;
                    btnViewUCR.Visible = false;
                }
                // Changed by Ashar on 12 Oct, 2012 as per MOSS task #65
                else if (CRStatus == 30)
                {
                    lblCRStatus.Text = "[UCR Status: Available]";
                    btnCreateUcr.Visible = false;
                    btnChangeUcr.Visible = true;
                    btnViewUCR.Visible = false;
                }

                DealerInfo.AppendFormat("Dealer: {0}<br/>",dt.Rows[0]["Dealer"]);
                
            }

            if (dtdealer.Rows.Count > 0)
            {
                DealerInfo.AppendLine(String.Format("Street: {0}<br/>", dtdealer.Rows[0]["Street"]));
                DealerInfo.AppendLine(String.Format("City: {0}<br/>", dtdealer.Rows[0]["City"]));
                DealerInfo.AppendLine(String.Format("Zip: {0}<br/>", dtdealer.Rows[0]["Zip"]));
                DealerInfo.AppendLine(String.Format("State: {0}<br/>", dtdealer.Rows[0]["StateCode"]));
                DealerInfo.AppendLine(String.Format("Phone1: {0}<br/>", dtdealer.Rows[0]["Phone1"]));
                DealerInfo.AppendLine(String.Format("Phone2: {0}<br/>", dtdealer.Rows[0]["Phone2"]));
                DealerInfo.AppendLine(String.Format("Fax: {0}<br/>", dtdealer.Rows[0]["Fax"]));
                DealerInfo.AppendLine(String.Format("Email1: {0}<br/>", dtdealer.Rows[0]["Email1"]));
                DealerInfo.AppendLine(String.Format("Email2: {0}", dtdealer.Rows[0]["Email2"]));

                long DealerID = Convert.ToInt64(dt.Rows[0]["DealerID"]);
                HyperLink hlinkDealer = (HyperLink)rptdetail.Items[0].FindControl("hlinkDealerInfo");
                hlinkDealer.ToolTip = DealerInfo.ToString();
                hlinkDealer.Text = Convert.ToString(dtdealer.Rows[0]["DealerName"]);
                hlinkDealer.NavigateUrl = String.Format("ViewDealer.aspx?Mode=View&EntityId={0}&type=1", DealerID);
            }
            hfvin.Value = VIN;
            if (isRejected == true)
            {
                btnDiscardedInfo.Visible = true;
                DeleteTooltip = String.Format("Reason: {0}<br/>Deleted By: {1}<br/>Dated: {2}", dt.Rows[0]["DeleteReason"], dt.Rows[0]["ModifiedBy"], dt.Rows[0]["DateModified"]);
                //btnDiscardedInfo.Title = "This is discarded by Development Team<br> Date :3-March-2012<br>Reason:Duplicate Record";
                btnDiscardedInfo.Title = DeleteTooltip;
                btnCreateUcr.Visible = false;
            }
            else
            {
                btnDiscardedInfo.Visible = false;
            }
        }

        public void ExtractCrInfo(DataTable dtCR)
        {
            crPreInvID = Convert.ToString(Session["PreInvID"]);
            crVin = Convert.ToString(dtCR.Rows[0]["VIN"]);
            crYear = Convert.ToString(dtCR.Rows[0]["Year"]);
            crMake = Convert.ToString(dtCR.Rows[0]["Make"]);
            crModel = Convert.ToString(dtCR.Rows[0]["Model"]);
            crBody = Convert.ToString(dtCR.Rows[0]["Body"]);
            crPrice = Convert.ToString(dtCR.Rows[0]["Price"]);
            crMileage = Convert.ToString(dtCR.Rows[0]["Mileage"]);
            crExtCol = Convert.ToString(dtCR.Rows[0]["ExtColor"]);
            crIntCol = Convert.ToString(dtCR.Rows[0]["IntColor"]);
        }

        protected void rptdetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hfCatID =(HiddenField)e.Item.FindControl("hfcategoryid");
                HiddenField hfBuyerNote = (HiddenField)e.Item.FindControl("hfBuyerNote");;
                HiddenField hfIsEmailSent = (HiddenField)e.Item.FindControl("hfIsEmailSent");
                HiddenField hfCRStatus = (HiddenField)e.Item.FindControl("hfCRStatus");
                HiddenField hfVIN2 = (HiddenField)e.Item.FindControl("hfVIN2");
                HiddenField hfyear = (HiddenField)e.Item.FindControl("hfyear");
                HiddenField hfmake = (HiddenField)e.Item.FindControl("hfmake");
                HiddenField hfmodel = (HiddenField)e.Item.FindControl("hfmodel");
                HiddenField hfbody = (HiddenField)e.Item.FindControl("hfbody");
                HiddenField hfprice = (HiddenField)e.Item.FindControl("hfprice");
                HiddenField hfmileage = (HiddenField)e.Item.FindControl("hfmileage");
                HiddenField hfextcol = (HiddenField)e.Item.FindControl("hfextcol");
                HiddenField hfintcol = (HiddenField)e.Item.FindControl("hfintcol");
                HiddenField hfInventID = (HiddenField)e.Item.FindControl("hfInventID");

                ImageButton ImgFlag = (ImageButton)e.Item.FindControl("imgCatflag");
                Label lblvin = (Label)e.Item.FindControl("lblvin");
                ImageButton ibtnEmailStatus = (ImageButton)e.Item.FindControl("ibtnEmailStatus");
                HyperLink hlnkVIN = (HyperLink)e.Item.FindControl("hlnkVIN");

                
                if (Convert.ToInt64(String.IsNullOrEmpty(hfCatID.Value)?"0":hfCatID.Value) > 0)
                    ImgFlag.Visible = true;
                else
                    ImgFlag.Visible = false;

                if(!String.IsNullOrEmpty(hfBuyerNote.Value))
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
                
                hfvin.Value = hfVIN2.Value;
                hfcryear.Value = hfyear.Value;
                hfcrmake.Value = hfmake.Value;
                hfcrmodel.Value = hfmodel.Value;
                hfcrbody.Value = hfbody.Value;
                hfcrprice.Value = hfprice.Value;
                hfcrmileage.Value = hfmileage.Value;
                hfcrextcol.Value = hfextcol.Value;
                hfcrintcol.Value = hfintcol.Value;

                if (Convert.ToInt64(hfInventID.Value) > 0)
                {
                    lblvin.Visible = false;
                    hlnkVIN.Visible = true;
                }
            }
        }

        protected void gvLinkedFilter_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvLinkedFilter.PageIndex = e.NewPageIndex;
            Bind_PreInvLinkedCars(Convert.ToInt64(Session["PreInvID"]));

        }

        protected void grdlinkedcarInv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grdlinkedcarInv.PageIndex = e.NewPageIndex;
            Bind_InvLinkedCars(Convert.ToInt64(Session["PreInvID"]));

        }

        private void Bind_PreInvLinkedCars(long PreInventoryId)
        {
            //Check if VinNo provided, Get Linked Cars for provided InventoryId,VIN
            if (!string.IsNullOrEmpty(hfvin.Value))
            {
                gvLinkedFilter.DataSource = PreInvBAL.GetPreInvLinked(PreInventoryId, hfvin.Value, DuplicatePeriod, Constant.OrgID);
                gvLinkedFilter.DataBind();
            }
        }

        private void Bind_InvLinkedCars(long PreInventoryId)
        {
            //Check if VinNo provided, Get Linked Cars for provided InventoryId,VIN
            if (!string.IsNullOrEmpty(hfvin.Value))
            {
                grdlinkedcarInv.DataSource = PreInvBAL.GetInvLinked(PreInventoryId, hfvin.Value, DuplicatePeriod, Constant.OrgID);
                grdlinkedcarInv.DataBind();
            }
        }

        protected void gvLinkedFilter_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfCatID = (HiddenField)e.Row.FindControl("hfcategoryid");
                HiddenField hfInvID = (HiddenField)e.Row.FindControl("hfInvID");
                ImageButton ImgFlag = (ImageButton)e.Row.FindControl("imgCatflag");
                if (Convert.ToInt64(String.IsNullOrEmpty(hfCatID.Value) ? "0" : hfCatID.Value) > 0)
                    ImgFlag.Visible = true;
                else
                    ImgFlag.Visible = false;
                if (!String.IsNullOrEmpty(hfInvID.Value))
                { 
                    if(Convert.ToInt64(hfInvID.Value)>0)
                        e.Row.BackColor = System.Drawing.Color.LightCyan;
                }
            }
        }

        protected void grdlinkedcarInv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfCatID = (HiddenField)e.Row.FindControl("hfcategoryid");
                ImageButton ImgFlag = (ImageButton)e.Row.FindControl("imgCatflag");
                if (Convert.ToInt64(String.IsNullOrEmpty(hfCatID.Value) ? "0" : hfCatID.Value) > 0)
                    ImgFlag.Visible = true;
                else
                    ImgFlag.Visible = false;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("AddInventory.aspx?PreInvID={0}", Session["PreInvID"]));
        }

        protected void OkButton_Click(object sender, EventArgs e)
        {
            //PreInvBAL.DeletePreinventory(Convert.ToInt64(Session["PreInvID"]), Convert.ToInt64(Session["empId"]),txtreason.Text);

            if (rdlist.SelectedItem.Value == "D")
            {
                PreInvBAL.DeletePreinventory(Convert.ToInt64(Session["PreInvID"]), Convert.ToInt64(Session["empId"]));
                Response.Redirect("PreInventory.aspx");
            }
            else if (rdlist.SelectedItem.Value == "R")
            {
                PreInvBAL.RejectPreInventory(Convert.ToInt64(Session["PreInvID"]), Convert.ToInt64(Session["empId"]), txtreason.Text);
                if (Session["PreInvID"] != null)
                {
                    BindPreInvGrid();
                    _Code = Convert.ToInt64(Session["PreInvID"]);
                    Bind_PreInvLinkedCars(Convert.ToInt64(Session["PreInvID"]));
                    Bind_InvLinkedCars(Convert.ToInt64(Session["PreInvID"]));
                    txtreason.Text = "";
                }
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            txtreason.Text = "";
        }

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

        protected void btnDeleteInventory_Click(object sender, EventArgs e)
        {
            txtreason.Text = string.Empty;
            ModelPopupExtender1.Show();
            
            //PreInvBAL.DeletePreinventory(Convert.ToInt64(Session["PreInvID"]), Convert.ToInt64(Session["empId"]));
            //Response.Redirect("PreInventory.aspx");
        }

        protected void btnPending_Click(object sender, EventArgs e)
        {
                PreInvBAL.MarkPreInvAsPending(Convert.ToInt64(Session["PreInvID"]), Convert.ToInt64(Session["empId"]));
                BindPreInvGrid();
                Bind_PreInvLinkedCars(Convert.ToInt64(Session["PreInvID"]));
                Bind_InvLinkedCars(Convert.ToInt64(Session["PreInvID"]));
        }

        public string FormatBuyer(object oBuyer)
        {
            string Buyer = Convert.ToString(oBuyer);
            string str = Buyer.Replace("()", "");

            return str;
        }

        protected void rdlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModelPopupExtender1.Show();
            if (rdlist.SelectedItem.Value == "R")
                trReason.Visible = false;
            else
                trReason.Visible = true;
        }

        protected void btnCreateUcr_Click(object sender, EventArgs e)
        {
            mpeCR.Show();
        }

        protected void btnChangeUcr_Click(object sender, EventArgs e)
        {
            mpeChangeCR.Show();
            hlnkViewReport.NavigateUrl = String.Format("{0}{1}", UCRlinkUrl, hfCRId.Value);
        }

        protected void btnViewUCR_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("{0}{1}", UCRlinkUrl, hfCRId.Value));
            //Response.Redirect(String.Format("http://web.metaoptionllc.com:82/Report/{0}", Session["PreInvID"]));
        }

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
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=1&value={0}&invtype=P&id={1}", txtCRIdUrl.Text, Session["PreInvID"]);
            mpeCR.Hide();
            mpucr.Show();

        }

        protected void btncridvalidate2_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=1&value={0}&invtype=P&id={1}", txtCRIdUrl2.Text, Session["PreInvID"]);
            mpeCR.Hide();
            mpucr.Show();

        }
        #endregion

        #region[Link CR by CRURL]
        protected void btncrurlvalidate_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=2&value={0}&invtype=P&id={1}", txtCRIdUrl.Text, Session["PreInvID"]);
            mpeCR.Hide();
            mpucr.Show();
        }
        protected void btncrurlvalidate2_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=2&value={0}&invtype=P&id={1}", txtCRIdUrl2.Text, Session["PreInvID"]);
            mpeCR.Hide();
            mpucr.Show();

        }
        #endregion

        #region[Link CR by VIN]
        protected void btncrsearch_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=3&value={0}&invtype=P&id={1}", txtCRIdUrl.Text, Session["PreInvID"]);
            mpeCR.Hide();
            mpucr.Show();

        }
        protected void btncrsearch2_Click(object sender, EventArgs e)
        {
            iframeucr.Attributes["src"] = String.Format("ucrdetail.aspx?type=3&value={0}&invtype=P&id={1}", txtCRIdUrl2.Text, Session["PreInvID"]);
            mpeCR.Hide();
            mpucr.Show();

        }
        #endregion

        protected void btnucrcancel_Click(object sender, EventArgs e)
        {
            mpucr.Hide();
        }

        protected void btnCRok2_Click(object sender, EventArgs e)
        {
            PreInvBAL.RemoveCR(Convert.ToInt64(Session["PreInvID"]), 0, Convert.ToInt64(Session["empId"]));
            mpeChangeCR.Hide();
            Response.Redirect("PreInventoryDetail.aspx");
        }

        #region[Email detail popup]
        protected void ibtnEmailStatus_Click(object sender, ImageClickEventArgs e)
        {
            long PreInvID = Convert.ToInt64(Session["PreInvID"]);
            IfrmEmailDetail.Attributes.Add("src", String.Format("EmailDetails.aspx?type=2000&id={0}", PreInvID));
            mpeEmailDetail.Show();
        }
        #endregion
    }
}