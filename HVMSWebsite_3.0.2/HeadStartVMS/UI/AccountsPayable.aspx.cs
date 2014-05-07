using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using METAOPTION.BAL;
using System.Data;
using System.Web.Security;


namespace METAOPTION.UI
{
    public partial class AccountsPayable : System.Web.UI.Page
    {      
        PaymentBLL paymentBLL = new PaymentBLL();

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

            if (!IsPostBack) 
                BindData(); 
        }

        private void CheckPermission()
        {
            List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, "PAYMENT");

            if (!dict.Contains("ACCOUNTSPAYABLE.VIEW"))
                Response.Redirect("Permission.aspx?MSG=ACCOUNTSPAYABLE.VIEW", true);
        }
        
        private void BindData()
        {
            String orderBy = grvUnpaidCarExpense.OrderBy;
            if (String.IsNullOrEmpty(orderBy))
                orderBy = " DealerName desc";

            IEnumerable<GetUnpaidCarCostResult> fiterResult = paymentBLL.GetUnpaidCarCost(Convert.ToInt32(Session["SystemID"]), Constant.OrgID).Sort<GetUnpaidCarCostResult>(orderBy);
            Util.setValue(grvUnpaidCarExpense, fiterResult.ToList());
        }

        protected void grvUnpaidCarExpense_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvUnpaidCarExpense.PageIndex = e.NewPageIndex; 
            BindData();
        }

        protected void grvUnpaidCarExpense_OnSorting(object sender, GridViewSortEventArgs e)
        {
            BindData();
        }

        protected void lnkPay_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender; 
            Int32 expenseId = Int32.Parse(btn.CommandArgument);
            GridViewRow row = (GridViewRow)btn.Parent.Parent;
            HiddenField hdnEntityTypeId = (HiddenField)row.FindControl("hdnEntityTypeId");
            HiddenField hdnEntityId = (HiddenField)row.FindControl("hdnEntityId");
            
            if (Session["SystemId"].ToString() == "-1")
            {
                DataTable dtSystems = new DataTable();
                if (Session["dtSystems"] == null)
                {
                    dtSystems = InventoryBAL.FetchSystems(Constant.OrgID);
                    Session["dtSystems"] = dtSystems;
                }
                else
                    dtSystems = (DataTable)Session["dtSystems"];

                HiddenField hdnSystemID = (HiddenField)row.FindControl("hdnSysID");
                Session["PeachTreeValue2"] = dtSystems.Select("SystemID=" + hdnSystemID.Value)[0].ItemArray[3].ToString();
            }

            Response.Redirect("MakeANewPayment.aspx?EntityId=" + hdnEntityId.Value + "&type=" + hdnEntityTypeId.Value + "&ExpenseId=" + expenseId.ToString(), true);
        }

        protected void lnkVIN_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender; 
            GridViewRow row = (GridViewRow)btn.Parent.Parent;
            HiddenField hdnInventoryId = (HiddenField)row.FindControl("hdnInventoryId"); 
            Response.Redirect("InventoryDetail.aspx?Code=" + hdnInventoryId.Value, true); 
        }   
    }
}
