using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace METAOPTION.UI
{
    public partial class VendorSystemStats : System.Web.UI.Page
    {
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
            GetMobileActivityCurrentDetails_Ver211Result objDetails = BAL.Common.GetMobileActivityDetails_Ver211(Convert.ToInt32(Session["LoginEntityTypeID"].ToString()), Convert.ToInt32(Constant.UserId));
            lblTotalExpAdded.Text = objDetails.NewPreExpense.ToString();
            lblPendingApproval.Text = objDetails.PendingPreExpense.ToString();
            lblLocationCapture.Text = objDetails.LocationCapture.ToString();
            lblVinScanLog.Text = objDetails.VINLocation.ToString();
            lblPreExpImg.Text = objDetails.PreExpenseImage.ToString();
            lblGenericImg.Text = objDetails.GenericImage.ToString();

        }
    }
}