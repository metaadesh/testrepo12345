using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using METAOPTION;
using METAOPTION.BAL;

namespace METAOPTION.UI
{
    public partial class Admin_Master : System.Web.UI.MasterPage
    {
        Admin_LoginBAL objBAL = new Admin_LoginBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["empId"] == null)
                Response.Redirect("~/Admin_Login.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
            else
            {
                lbl_Welcome_Disname.Text = "Welcome " + Constant.UserDisplayName;
                lbl_LastLoginTime.Text = "Last Login was on: " + objBAL.GetLastLogin(Constant.UserId);
            }
        }

        protected void link_logout_click(object sender, EventArgs e)
        {
            try
            {
                Int64 LoginHistoryId = Convert.ToInt64(System.Web.HttpContext.Current.Session["LoginHistoryId"].ToString());
                Admin_LoginBAL.Logout_Session(LoginHistoryId);
            }
            catch { }
                System.Web.HttpContext.Current.Session.Abandon();
                System.Web.Security.FormsAuthentication.SignOut();
                //System.Web.HttpContext.Current.Response.Redirect("~/Admin_Login.aspx", true);
                System.Web.HttpContext.Current.Response.Redirect("~/Admin_Login.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));

        }
    }
}