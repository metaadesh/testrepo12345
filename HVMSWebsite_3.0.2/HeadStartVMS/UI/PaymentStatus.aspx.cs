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
using System.Globalization;

//Extended
 
namespace METAOPTION.UI
{
    public partial class PaymentStatus : System.Web.UI.Page
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
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now);

            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
            Response.Expires = -1500;
            Response.CacheControl = "no-cache";

            Session["ApplyPaymentStep"] = "PaymentStatus";
            
            if (Session["PaymentsStatus"] != null && Session["PaymentsStatus"].ToString() != "")
            {
                Boolean isSuccess = Convert.ToBoolean(Session["PaymentsStatus"].ToString());

                if (isSuccess)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Payment is made successfully."; 
                }
                else
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Error in applying payment.";
                } 
                Session.Remove("PaymentsStatus");


                lblPeachtreeUpdateStatus.ForeColor = System.Drawing.Color.Green;
                lblPeachtreeUpdateStatus.Text = "Peachtree update is working in backgound, to check the status please try after some time.";
                /* //**********Commentd By ALI and added above two lines ********
                if (Session["IsPeachtreeError"] != null && Session["IsPeachtreeError"].ToString() != "")
                {

                    Boolean isPeachtreeError = Convert.ToBoolean(Session["IsPeachtreeError"].ToString());
                    if (!isPeachtreeError)
                    {
                        lblPeachtreeUpdateStatus.ForeColor = System.Drawing.Color.Green;
                        lblPeachtreeUpdateStatus.Text = "Peachtree updated successfully.";
                    }
                    else
                    {
                        lblPeachtreeUpdateStatus.ForeColor = System.Drawing.Color.Red;
                        lblPeachtreeUpdateStatus.Text = (Session["PeachtreeError"] != null) ? Session["PeachtreeError"].ToString() : "Error in updating Peachtree."; 
                    }

                    Session.Remove("IsPeachtreeError");
                    Session.Remove("PeachtreeError");  
                }   
                  */
            }
            //else
            //{
            //    Response.Redirect("MakeANewPayment.aspx");
            //}    
        }

        protected void btnAddNewPayment_Click(object sender, EventArgs e)
        {
            Session["ApplyPaymentStep"] = null;
            Response.Redirect("MakeANewPayment.aspx",true);
        }

        protected void btnExpenseAgainstPayment_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["pid"] != null && Request.QueryString["pid"].ToString() != string.Empty)
            {
                String paymentID = Request.QueryString["pid"].ToString();
                Response.Redirect("ExpenseAgainstPayment.aspx?PaymentId=" + paymentID, true); 
            }
            else
                Response.Redirect("Payments.aspx", true);
        }

    }
}
