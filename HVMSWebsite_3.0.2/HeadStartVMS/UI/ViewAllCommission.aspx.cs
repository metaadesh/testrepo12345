using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using METAOPTION.BAL;
using System.Web.UI.HtmlControls;
using System.Data;

namespace METAOPTION.UI
{
    public partial class ViewAllCommission : System.Web.UI.Page
    {
        ExpenseBAL objExp = new ExpenseBAL();
       
        public const string GROSS_COMMISSION_FORMULAE =
          "Result=Sold Price - Car cost(Expense Amount) If Result > {0} then BuyerCommission = {1} if Result < {2} then BuyerCommission = {3} if between {2} & {0} then Buyer Commission= {6}";
       
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["MasterPageNoLeftPanel"] != null)
                this.MasterPageFile = Convert.ToString(Session["MasterPageNoLeftPanel"]);
            else
                Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(Request.Url.ToString()));


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                   
                BindExpenseType();
                BindAddedByUser();
                BindGrid();
            }
        }

        protected void BindExpenseType()
        {
            String ParentBuyerID = "-1";
            if (Convert.ToString(Session["BuyerAccessLevel"]) != "1")              
                ParentBuyerID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString();
            ParentID.Value = ParentBuyerID;
            EntityTYpeID.Value = Convert.ToString(Session["LoginEntityTypeID"]);
            List<ExpenseType> Lst = objExp.GetExpenseType(Convert.ToInt32(Session["UserEntityID"]), Convert.ToInt32(ParentBuyerID), Convert.ToInt32(Session["LoginEntityTypeID"]));
            ddlExpenseType.DataSource = Lst;
            ddlExpenseType.DataTextField = "ExpenseType1";
            ddlExpenseType.DataValueField = "ExpenseTypeId";
            ddlExpenseType.DataBind();
            ddlExpenseType.Items.Insert(0, new ListItem("All", "-1"));
            if (ddlExpenseType.Items.FindByText("Commission") != null)
                  ddlExpenseType.SelectedValue = "12";

        }


        protected void BindAddedByUser()
        {
            String ParentBuyerID = "-1";
            if (Convert.ToString(Session["BuyerAccessLevel"]) != "1")                
                ParentBuyerID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString();
             List<SecurityUser> Lsst = objExp.GetAddedByUser(Convert.ToInt32(Session["UserEntityID"]), Convert.ToInt32(ParentBuyerID), Convert.ToInt32(Session["LoginEntityTypeID"]));
            ddlAddedBy.DataSource = Lsst;
            ddlAddedBy.DataTextField = "DisplayName";
            ddlAddedBy.DataValueField = "SecurityUserID";
            ddlAddedBy.DataBind();
            ddlAddedBy.Items.Insert(0, new ListItem("ALL", "-1"));
        }

        protected void BindGrid()
        {
            String ParentBuyerID = "-1";            
                ParentBuyerID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString();
                       
            String ExpType =  ddlExpenseType.SelectedValue == "-1" ? "-1" : ddlExpenseType.SelectedValue;
            String SyncDateFrom, SyncDateTo;
            if (String.IsNullOrEmpty(txtSyncDateFrom.Text))
                SyncDateFrom = DateTime.Today.AddYears(-100).ToShortDateString();
            else
                SyncDateFrom = txtSyncDateFrom.Text;

            if (String.IsNullOrEmpty(txtSyncDateTo.Text))
                SyncDateTo = DateTime.Today.AddDays(1).ToShortDateString();
            else
                SyncDateTo = Convert.ToDateTime(txtSyncDateTo.Text).AddDays(1).ToShortDateString();
            gvExpense.PageSize = Convert.ToInt32(ddlPageSize1.SelectedValue);
            ObjectDataSource odsExpense = new ObjectDataSource();
            odsExpense.Selected += new ObjectDataSourceStatusEventHandler(odsExpense_Selected);
            odsExpense.TypeName = "METAOPTION.BAL.CommonBAL";
            odsExpense.SelectMethod = "GetExpenseList_ByBuyer"; 
            odsExpense.SelectCountMethod = "GetExpenseList_ByBuyerCount";
            odsExpense.EnablePaging = true;
            odsExpense.SelectParameters.Add("EntityID", DbType.Int64,  Session["UserEntityID"].ToString());
            odsExpense.SelectParameters.Add("ParentEntityID", DbType.Int32, ParentBuyerID.ToString());
            odsExpense.SelectParameters.Add("EntityTypeId", DbType.Int32, Convert.ToString(Session["LoginEntityTypeID"]));
            odsExpense.SelectParameters.Add("ExpenseTypeID", DbType.Int32, ExpType);
            odsExpense.SelectParameters.Add("VIN", String.IsNullOrEmpty(txtVIN.Text) ? "-1" : txtVIN.Text.Trim());
            odsExpense.SelectParameters.Add("FromDate", SyncDateFrom);
            odsExpense.SelectParameters.Add("ToDate", SyncDateTo);
            odsExpense.SelectParameters.Add("AddedBy", DbType.Int32, ddlAddedBy.SelectedValue);
            odsExpense.SelectParameters.Add("CheckNo", String.IsNullOrEmpty(txtCheck.Text) ? "-1" : txtCheck.Text);
            odsExpense.SelectParameters.Add("StartRowIndex", DbType.Int32, gvExpense.PageIndex.ToString());
            odsExpense.SelectParameters.Add("MaximumRows", DbType.Int32, gvExpense.PageSize.ToString());           
            gvExpense.DataSource = odsExpense;
            gvExpense.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
            BindGrid();

        }
        protected void odsExpense_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            try
            {
                if (e.ReturnValue == null) return;
                if (e.ReturnValue.GetType().Name == "Int32")
                {
                    Int32 count = Convert.ToInt32(e.ReturnValue);
                    Int32 pagesize = gvExpense.PageSize;
                    Int32 pagecount = count / pagesize;

                    if (count < pagesize)
                    {
                        hdnTotalrows.Value = Convert.ToString(count);
                        lblCount1.Text = lblCount.Text = String.Format("Showing {0} of {0} records", count);
                    }
                    else
                    {
                        hdnTotalrows.Value = Convert.ToString(pagesize);
                        lblCount1.Text = lblCount.Text = String.Format("Showing {0} through {1} of {2} records"
                             , String.Format("{0:#,###}", pagesize * gvExpense.PageIndex + 1)
                             , String.Format("{0:#,###}", pagesize * (gvExpense.PageIndex + 1) > count ? count : pagesize * (gvExpense.PageIndex + 1))
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

                        ddlPaging.SelectedValue = String.Format("{0}", gvExpense.PageIndex + 1);
                        ddlPaging1.SelectedValue = String.Format("{0}", gvExpense.PageIndex + 1);
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
            catch (Exception ex) { }
        }

        #region[Enable/Disable paging]
        private void EnablePaging()
        {
            try
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
            catch (Exception ex) { }
        }
        #endregion

        #region[Paging Click events]
        protected void btnFirst_Click(object sender, EventArgs e)
        {

            gvExpense.PageIndex = 0;
            BindGrid();
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedIndex > 0)
                gvExpense.PageIndex--;
            BindGrid();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedValue != ddlPaging.Items.Count.ToString())
                gvExpense.PageIndex++;
            BindGrid();
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            gvExpense.PageIndex = ddlPaging.Items.Count - 1;
            BindGrid();
        }
        #endregion

        #region[Page size selection change]
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = sender as DropDownList;
                if (ddl.ID == "ddlPageSize1")
                    ddlPageSize2.SelectedValue = ddlPageSize1.SelectedValue;
                else
                    ddlPageSize1.SelectedValue = ddlPageSize2.SelectedValue;

                gvExpense.PageIndex = 0;
                BindGrid();
            }
            catch (Exception ex) { }
        }
        #endregion

        #region[Drop down page selection change]
        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (((DropDownList)sender).SelectedValue != "0")
                    gvExpense.PageIndex = Convert.ToInt32(((DropDownList)sender).SelectedValue) - 1;
                BindGrid();

                EnablePaging();
            }
            catch (Exception ex) { }
        }
        #endregion


        protected void gvExpense_RowDataBound(object sender, GridViewRowEventArgs e)
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
            }
        }
        /// <summary>
        /// Handle page index changing event for pagination in gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvExpense_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvExpense.PageIndex = e.NewPageIndex;

            BindGrid();
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


    }
}