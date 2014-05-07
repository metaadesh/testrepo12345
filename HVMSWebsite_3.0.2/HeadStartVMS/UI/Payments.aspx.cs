using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

//Extended
using System.Collections.Generic;
using METAOPTION.BAL;
using System.Text;
using System.Web;
using System.Web.Security;


namespace METAOPTION.UI
{
    public partial class Payments : System.Web.UI.Page
    {
        PaymentBLL paymentBLL = new PaymentBLL();
        private int rowCount = 0;
        private int totalRowCount = 0;

        public Hashtable SearchParam
        {
            get { return (Hashtable)Cache["htSearchParam"]; }
            set { Cache["htSearchParam"] = value; }
        }

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

            lblResultTitle.Text = "View All Payments";

            if (!IsPostBack)
            {
                if (!Request.RawUrl.Contains("SearchPayment.aspx") && (SearchParam != null))
                    Cache.Remove("htSearchParam");

                BindData();

                if ((Convert.ToString(Session["LoginEntityTypeID"]) == "1") || (Convert.ToString(Session["LoginEntityTypeID"]) == "2") || (Convert.ToString(Session["LoginEntityTypeID"]) == "3"))
                    lnkViewAllPayment.Visible = false;
            }
        }

        private void CheckPermission()
        {
            List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, "PAYMENT");

            if (!dict.Contains("ALLPAYMENT.VIEW"))
                Response.Redirect("Permission.aspx?MSG=ALLPAYMENT.VIEW", true);
        }

        private void BindData()
        {
            ReadSearchParam();
            Util.setValue(grvAllPayments, GetPayments());
        }

        private DataSet GetPayments()
        {
            String orderBy = grvAllPayments.OrderBy;
            if (String.IsNullOrEmpty(orderBy))
                orderBy = " dateadded desc, checkdate desc";

            return GetPayments(grvAllPayments.PageIndex, orderBy);
        }

        private DataSet GetPayments(int pageIndex, String sortExp)
        {
            StringBuilder sb = new StringBuilder();
            if (SearchParam != null)
            {
                lblResultTitle.Text = "View Filtered Payments";

                Int32 entityId = Int32.Parse(SearchParam["entityId"].ToString());
                Int32 entityTypeId = Int32.Parse(SearchParam["entityTypeId"].ToString());
                String recipientType = SearchParam["recipientType"].ToString();
                String recipientName = SearchParam["recipientName"].ToString();
                String checkNumber = SearchParam["checkNumber"].ToString();
                String minCheckDate = SearchParam["minCheckDate"].ToString();
                String maxCheckDate = SearchParam["maxCheckDate"].ToString();
                Decimal minAmount = Decimal.Parse(SearchParam["minAmount"].ToString());
                Decimal maxAmount = Decimal.Parse(SearchParam["maxAmount"].ToString());
                String invoiceNumber = SearchParam["invoiceNumber"].ToString();
                String accountNumber = SearchParam["accountNumber"].ToString();

                sb.Append((entityId > 0) ? String.Format(" and EntityId = {0}", entityId) : "");
                sb.Append((entityTypeId > 0) ? String.Format(" and entityTypeId = {0}", entityTypeId) : "");
                sb.Append((checkNumber != String.Empty) ? String.Format(" and checknumber like '%{0}%'", checkNumber) : "");
                sb.Append((minAmount > 0) ? String.Format(" and Amount >={0} ", minAmount) : "");
                sb.Append((maxAmount > 0) ? String.Format(" and Amount <={0} ", maxAmount) : "");
                sb.Append((minCheckDate != String.Empty) ? String.Format(" and CheckDate >= '{0}'", minCheckDate) : "");
                sb.Append((maxCheckDate != String.Empty) ? String.Format(" and CheckDate <= '{0}'", maxCheckDate) : "");
                sb.Append((invoiceNumber != String.Empty) ? String.Format(" and invoicenumber like '%{0}%'", invoiceNumber) : "");
                sb.Append((accountNumber != "0") ? String.Format(" and AccountNumber like '%{0}%'", accountNumber) : "");
            }

            //List<GetAllPaymentsResult> filteredPayments = paymentBLL.GetFilterPayments(pageIndex, grvAllPayments.PageSize, sortExp, sb.ToString(), ref rowCount, ref totalRowCount);
            DataSet filteredPayments = new DataSet();
            //if ((Session["LoginEntityTypeID"] != null) && (Convert.ToString(Session["LoginEntityTypeID"]) == "3"))
            //{
            Int32 BuyerParentId = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? -1 : Convert.ToInt32(Session["BuyerParent"].ToString());

            filteredPayments = paymentBLL.GetFilterPayments_Ver211(pageIndex, grvAllPayments.PageSize, sortExp, sb.ToString(), ref rowCount, ref totalRowCount, Convert.ToInt32(Session["SystemID"]), Convert.ToInt32(Session["LoginEntityTypeID"]), Convert.ToInt32(Session["UserEntityID"]), BuyerParentId, Constant.OrgID);
            grvAllPayments.VirtualItemCount = totalRowCount;
            //}
            //else
            //{
            //    filteredPayments = paymentBLL.GetFilterPayments(pageIndex, grvAllPayments.PageSize, sortExp, sb.ToString(), ref rowCount, ref totalRowCount, Convert.ToInt32(Session["SystemID"]));
            //    grvAllPayments.VirtualItemCount = totalRowCount;
            //}

            return filteredPayments;
        }

        private void ReadSearchParam()
        {
            // If from Seach Page then filter the data else 
            if (Page.PreviousPage != null)
            {
                ContentPlaceHolder cphPayment = (ContentPlaceHolder)Page.PreviousPage.Master.FindControl("ContentPlaceHolder1");
                if (cphPayment != null)
                {
                    String recipientType = Util.getValue((DropDownList)cphPayment.FindControl("ddlRecipientType"), "Text");
                    String recipientName = Util.getValue((Label)cphPayment.FindControl("lblSelectedRecipientName"));
                    String checkNumber = Util.getValue((TextBox)cphPayment.FindControl("txtCheckNumber"));
                    String minCheckDate = Util.getValue((TextBox)cphPayment.FindControl("txtMinCheckDate"));
                    String maxCheckDate = Util.getValue((TextBox)cphPayment.FindControl("txtMaxCheckDate"));
                    String minimumAmount = Util.getValue((TextBox)cphPayment.FindControl("txtMinAmount"));
                    String maximumAmount = Util.getValue((TextBox)cphPayment.FindControl("txtMaxAmount"));
                    Decimal minAmount = Decimal.Parse((minimumAmount != "") ? minimumAmount : "0");
                    Decimal maxAmount = Decimal.Parse((maximumAmount != "") ? maximumAmount : "0");
                    String invoiceNumber = Util.getValue((TextBox)cphPayment.FindControl("txtInvoiceNumber"));
                    String accountNumber = Util.getValue((DropDownList)cphPayment.FindControl("ddlAccountNumber"));
                    Int32 entityId = Int32.Parse(Util.getValue((HiddenField)cphPayment.FindControl("hdnSelectedRecipientID")));
                    Int32 entityTypeId = Int32.Parse(Util.getValue((DropDownList)cphPayment.FindControl("ddlRecipientType")));

                    Hashtable htSearchParam = new Hashtable();
                    htSearchParam.Add("recipientType", recipientType);
                    htSearchParam.Add("recipientName", recipientName);
                    htSearchParam.Add("checkNumber", checkNumber);
                    htSearchParam.Add("minCheckDate", minCheckDate);
                    htSearchParam.Add("maxCheckDate", maxCheckDate);
                    htSearchParam.Add("minAmount", minAmount);
                    htSearchParam.Add("maxAmount", maxAmount);
                    htSearchParam.Add("invoiceNumber", invoiceNumber);
                    htSearchParam.Add("accountNumber", accountNumber);
                    htSearchParam.Add("entityId", entityId);
                    htSearchParam.Add("entityTypeId", entityTypeId);

                    SearchParam = htSearchParam;
                }
            }
        }

        protected void grvAllPayments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvAllPayments.PageIndex = e.NewPageIndex;

            BindData();
        }

        protected void grvAllPayments_OnSorting(object sender, GridViewSortEventArgs e)
        {
            BindData();
        }

        protected void lnkViewAllPayments_Click(object sender, EventArgs e)
        {
            if (SearchParam != null)
                Cache.Remove("htSearchParam");

            BindData();
        }

        protected void lnkCheckNumber_OnClick(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            Int32 paymentId = Int32.Parse(btn.CommandArgument);
            Response.Redirect("ExpenseAgainstPayment.aspx?PaymentId=" + paymentId);
        }
        /// <summary>
        /// This link button open View All Payment Report Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkViewAllPayment_Click(object sender, EventArgs e)
        {
            Response.Redirect("../VMSReports/ViewAllPayments.aspx?ReturnURL=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
        }

        #region Change Request:BUGID: 725: Highlight voided row and display "Voided" and hide image

        /// <summary>
        /// Handle GridView RowDatabound event to highlight row,if check is voided and hide print image and show "voided"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grvAllPayments_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow row = e.Row;
                HiddenField hdIsCheckVoided = row.Cells[0].FindControl("hdIsCheckVoided") as HiddenField;
                ImageButton imgNoprint = row.Cells[0].Controls[0].FindControl("imgNoprint") as ImageButton;
                Label lblVoidText = row.Cells[0].FindControl("lblVoidText") as Label;


                LinkButton lnkCheckNumber = (LinkButton)e.Row.FindControl("lnkCheckNumber");
                //if Check Voided then Highlight the row and make Print button False.
                if (hdIsCheckVoided.Value == "1")
                {
                    row.BackColor = System.Drawing.Color.Aqua;
                    lblVoidText.Text = "Voided";

                    //Make Print button visibility false
                    if (imgNoprint != null)
                        imgNoprint.Visible = false;
                }

                //if ((Convert.ToString(Session["LoginEntityTypeID"]) == "2") || (Convert.ToString(Session["LoginEntityTypeID"]) == "2"))
                //    lnkCheckNumber.Enabled = false;
            }
        }

        #endregion
    }
}
