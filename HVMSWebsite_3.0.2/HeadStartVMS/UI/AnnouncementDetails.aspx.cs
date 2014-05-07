using System;
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
using System.Collections.Generic;
using METAOPTION;
using METAOPTION.BAL;
namespace HeadStartVMS.UI
{
    public partial class AnnouncementDetails : System.Web.UI.Page
    {
        public const string GROSS_COMMISSION_FORMULAE =
              "Result=Sold Price - Car cost(Expense Amount) If Result > {0} then BuyerCommission = {1} if Result < {2} then BuyerCommission = {3} if between {2} & {0} then Buyer Commission= {6}";

        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["MasterPage"])))
                    this.MasterPageFile = Convert.ToString(Session["MasterPage"]);
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
            #region [ Page Load ]
            if (!IsPostBack)
            {
                long AnnouncementId = Convert.ToInt64(Request.QueryString["AnnouncementId"]);
                Util.Validate_QueryString_Value("AnnouncementDetails", AnnouncementId.ToString(), Constant.OrgID);

                BindAnnouncement(AnnouncementId);
                BindAnnouncementDetails(AnnouncementId);
                BindCommissionsCalculatedDetails(AnnouncementId);
                BindChromeUpdate(AnnouncementId);
            }
            #endregion
        }

        #region [ Bind Announcement ]
        void BindAnnouncement(Int64 AnnouncementId)
        {
            ViewAnnouncementBAL objAnn = new ViewAnnouncementBAL();
            List<string> lstAnnouncement = null;
            lstAnnouncement = objAnn.GetAnnouncement(AnnouncementId);
            if (lstAnnouncement.Count > 0)
            {
                lblTitle.Text = lstAnnouncement[0].ToString();
                lblDescription.Text = lstAnnouncement[1].ToString();
                lblType.Text = lstAnnouncement[2].ToString();
                lblDateAdded.Text = lstAnnouncement[3].ToString();
                lblAddedby.Text = lstAnnouncement[4].ToString();
            }
        }
        #endregion

        #region [ Bind Announcement Details ]
        void BindAnnouncementDetails(Int64 AnnouncementId)
        {
            ViewAnnouncementDetailsBAL objAnDetails = new ViewAnnouncementDetailsBAL();

            if (Convert.ToInt32(Session["LoginEntityTypeID"]) == 2)
            {
                gvLaneAssignments.DataSource = objAnDetails.SelectAnnouncementDetails_Ver211(AnnouncementId, Constant.UserId);
                gvLaneAssignments.DataBind();
            }
            else
            {
                gvLaneAssignments.DataSource = objAnDetails.SelectAnnouncementDetails(AnnouncementId);
                gvLaneAssignments.DataBind();
            }
            if (gvLaneAssignments.Rows.Count > 0)
                tblLane.Visible = true;
            else
                tblLane.Visible = false;

        }

        protected void gvLaneAssignments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToInt32(Session["LoginEntityTypeID"]) == 2)
                {
                    HyperLink hplViewCarPage = (HyperLink)e.Row.FindControl("hplViewCarPage");
                    hplViewCarPage.Enabled = false;
                }

            }
        }
        #endregion

        #region [ Bind Commissions Calculated Details ]
        void BindCommissionsCalculatedDetails(Int64 AnnouncementId)
        {
            ViewAnnouncementDetailsBAL objAnDetails = new ViewAnnouncementDetailsBAL();

            if (Convert.ToInt32(Session["LoginEntityTypeID"]) == 2)
            {
                gvCommission.DataSource = objAnDetails.GetCommissionsCalculated_Ver211(AnnouncementId, Constant.UserId);
                gvCommission.DataBind();
            }
            else
            {
                gvCommission.DataSource = objAnDetails.GetCommissionsCalculated(AnnouncementId);
                gvCommission.DataBind();
            }
            if (gvCommission.Rows.Count > 0)
                tblCommissions.Visible = true;
            else
                tblCommissions.Visible = false;

        }



        protected void frmBCommissionCalculationDetails_DataBound(object sender, EventArgs e)
        {
            FormView frmBCommissionCalculationDetails = (sender) as FormView;
            FormViewRow frmRow = frmBCommissionCalculationDetails.Row;

            //Return if No Row Found
            if (frmRow == null)
                return;

            string test = frmBCommissionCalculationDetails.DataKey["BuyerCommissionId"].ToString();

            ////Get Commission TypeId,based on it Hide/Unhide rows for various formulae desscription
            Label lblCommissionTypeId = frmRow.FindControl("lblCommissionTypeId") as Label;
            Label lblCommissionRuleDesc = frmRow.FindControl("lblCommissionRuleDesc") as Label;

            lblCommissionTypeId.Visible = false;

            HtmlTableRow tr5050 = frmRow.FindControl("tr5050") as HtmlTableRow;
            HtmlTableRow trGrossProfit = frmRow.FindControl("trGrossProfit") as HtmlTableRow;
            HtmlTableRow trFixedCommission = frmRow.FindControl("trFixedCommission") as HtmlTableRow;
            HtmlTableRow tr5050IIndLevelComm = tr5050.FindControl("tr5050IIndLevelComm") as HtmlTableRow;
            HtmlTableRow trReconFee = tr5050.FindControl("trReconFee") as HtmlTableRow;
            Label lblExpensesText = tr5050.FindControl("lblExpensesText") as Label;
            //HtmlTableRow trIIndLevelBuyer = tr5050.FindControl("trIIndLevelBuyer") as HtmlTableRow;

            //Commission Type: Fixed
            if (lblCommissionTypeId.Text == Constant.COMMISSIONTYPE_FIXED)
            {
                tr5050.Visible = false;
                trGrossProfit.Visible = false;

            }
            //Commission Type 50:50 /50:50/2 /50:50 IInd Level
            else if (lblCommissionTypeId.Text == Constant.COMMISSIONTYPE_5050 || lblCommissionTypeId.Text == Constant.COMMISSIONTYPE_5050SPLIT || lblCommissionTypeId.Text == Constant.COMMISSIONTYPE_IINDLEVEL5050)
            {

                //Display other buyer information only if IInd Level 5050
                //if (lblCommissionTypeId.Text == Constant.COMMISSIONTYPE_IINDLEVEL5050 )
                //    tr5050IIndLevelComm.Visible = true;

                //else
                //    tr5050IIndLevelComm.Visible = false;


                //if (tr5050IIndLevelComm.Visible || lblCommissionTypeId.Text == Constant.COMMISSIONTYPE_5050)
                //    trIIndLevelBuyer.Visible = false;
                //else
                //    trIIndLevelBuyer.Visible = true;
                long buyerCommissionId = Convert.ToInt64(frmBCommissionCalculationDetails.DataKey["BuyerCommissionId"]);
                GetBuyerCommSettings_FromTranTableResult objResult = BuyerBAL.GetBuyerCommission_TransactionSetting(buyerCommissionId);

                //If recon fee already posted it will be a part of expenses
                if (objResult.Recon_fee_Expense > 0)
                {
                    lblExpensesText.Text = "Expenses including (Recon Fee : " + String.Format("{0:C}", objResult.Recon_fee_Expense) + ")";
                    trReconFee.Visible = false;
                }
                else
                {
                    trReconFee.Visible = true;
                }

                trFixedCommission.Visible = false;
                trGrossProfit.Visible = false;
            }
            else if (lblCommissionTypeId.Text == Constant.COMMISSIONTYPE_GROSS)
            {
                trFixedCommission.Visible = false;
                tr5050.Visible = false;

                //Prepare Gross Profit Rule from Transaction Table,It could be different from Current Buyer Settings
                string strGrossFormule = GROSS_COMMISSION_FORMULAE;
                GetBuyerCommSettings_FromTranTableResult objResult = BuyerBAL.GetBuyerCommission_TransactionSetting(Convert.ToInt64(frmBCommissionCalculationDetails.DataKey["BuyerCommissionId"]));
                if (objResult == null)
                    return;

                //Code Changes for Drew Commission
                string exactMessage = "Exact Value";
                if (objResult.Exact_Value.HasValue)
                {
                    if (objResult.Exact_Value.Value > 0)
                        exactMessage = Convert.ToString(objResult.Exact_Value);
                    else
                        exactMessage = "Exact Value";
                }
                else
                    exactMessage = "Exact Value";

                lblCommissionRuleDesc.Text = string.Format(strGrossFormule, objResult.Max_Gross, objResult.MaxValue_Gross,
                                      objResult.Min_Gross, objResult.MinValue_Gross, objResult.Min_Gross, objResult.Max_Gross, exactMessage);

            }
        }


        #endregion

        #region [ Bind Chrome Update ]
        void BindChromeUpdate(Int64 AnnouncementId)
        {

            ViewAnnouncementDetailsBAL objAnDetails = new ViewAnnouncementDetailsBAL();
            if (Convert.ToInt32(Session["LoginEntityTypeID"]) == 2)
            {
                gvChromeUpdate.DataSource = objAnDetails.GetChromeUpdates_Ver211(AnnouncementId, Constant.UserId);
                gvChromeUpdate.DataBind();
            }
            else
            {
                gvChromeUpdate.DataSource = objAnDetails.GetChromeUpdates(AnnouncementId);
                gvChromeUpdate.DataBind();
            }
            if (gvChromeUpdate.Rows.Count > 0)
                tblChrome.Visible = true;
            else
                tblChrome.Visible = false;

        }
        #endregion

        protected void gvCommission_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.EmptyDataRow)
            {
                //Set Inventory Header
                Panel pnlShowCommissionDetails = e.Row.FindControl("pnlShowCommissionDetails") as Panel;
                if (pnlShowCommissionDetails == null) return;
                HiddenField hdInventoryId = e.Row.FindControl("hdInventoryId") as HiddenField;
                Label lblHeaderInventoryInfo = pnlShowCommissionDetails.FindControl("lblHeaderInventoryInfo") as Label;

                //If Header Info Not prepared,prepared header    
                // if (string.IsNullOrEmpty(lblHeaderInventoryInfo.Text))
                lblHeaderInventoryInfo.Text = "Buyer Commission Calculation For " + ' ' + InventoryBAL.GetCurrentInventoryHeader(Convert.ToInt64(hdInventoryId.Value)) + " ( Code:" + hdInventoryId.Value + " )";

                if (Convert.ToInt32(Session["LoginEntityTypeID"]) == 2)
                {
                    HyperLink hplViewCarPage = (HyperLink)e.Row.FindControl("hplViewCarPage");
                    HyperLink hylnkCheckNo = (HyperLink)e.Row.FindControl("hylnkCheckNo");
                    hplViewCarPage.Enabled = false;
                    hylnkCheckNo.Enabled = false;
                }
            }
        }
        /// <summary>
        /// Handle Page Index Changing Event of Gridview control for providing pagination in gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvCommission_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCommission.PageIndex = e.NewPageIndex;
            long AnnouncementId = Convert.ToInt64(Request.QueryString["AnnouncementId"]);
            BindCommissionsCalculatedDetails(AnnouncementId);
        }
    }
}
