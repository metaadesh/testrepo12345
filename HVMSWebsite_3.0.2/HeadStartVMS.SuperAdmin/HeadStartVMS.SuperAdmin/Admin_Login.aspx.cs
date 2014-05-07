using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using METAOPTION.Common;
using METAOPTION;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using METAOPTION.BAL;

namespace METAOPTION
{
    public partial class Admin_Login : System.Web.UI.Page
    {

        Admin_LoginBAL objLoginBLL = new Admin_LoginBAL();
        String IPSecurityKey = ConfigurationManager.AppSettings["EnableIPSecurity"];

        #region[ Page Load Event ]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Page.IsPostBack || Page.IsCallback))
            {
                if (Request.Cookies["RememberMe"] != null)
                {
                    HttpCookie cook = (HttpCookie)Request.Cookies["RememberMe"];
                    this.txtLogin.Text = Convert.ToString(cook.Values["UID"]);
                    this.txtPass.Attributes.Add("value", Convert.ToString(cook.Values["PWD"]));
                }
            }
        }
        #endregion


        #region[ Validate user and redirect to home page ]

        protected void btnLogin_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string userName = txtLogin.Text.Trim();
                string userPassword = EncryptMD5.Encrypt(this.txtPass.Text.Trim());
                string IPAddress = Request.UserHostAddress;
                string OrgCode = "";//txtOrgCode.Text.ToUpper().Trim();


                Dictionary<string, string> objDictionary;
                if (IPSecurityKey.ToUpper() == "TRUE")
                {
                    if (objLoginBLL.CheckIPRestriction(userName, userPassword))
                        objDictionary = objLoginBLL.GetLoginWithIP(userName, userPassword, IPAddress, OrgCode);
                    else
                        objDictionary = objLoginBLL.GetLogin(userName, userPassword, OrgCode);
                }
                else
                    objDictionary = objLoginBLL.GetLogin(userName, userPassword, OrgCode);

                if (objDictionary.Count > 0)
                {
                    //Check if user has only "MobileUser" permission
                    //Redirect it to Permission page
                    String Permission = objLoginBLL.GetUserPermission(Convert.ToInt64(objDictionary["SecurityUserId"]));
                    if (!String.IsNullOrEmpty(Permission) && Permission.ToLower() == "mobileuser")
                        Response.Redirect("~/UI/Admin_Permission.aspx?MSG=AccessDenied");

                    //else allow him to login
                    else
                    {
                        Session["empId"] = objDictionary["SecurityUserId"];
                        Session["UserEntityID"] = objDictionary["EntityID"];
                        Session["disName"] = objDictionary["DisplayName"];
                        Session["LoginEntityTypeID"] = objDictionary["EntityTypeID"];
                        Session["UserName"] = userName.Trim();
                        //Session["OrgID"] = objDictionary["OrgID"];
                        //Session["OrgCode"] = objDictionary["OrgCode"];
                        //Session["Organization"] = objDictionary["Organisation"];

                        //LogHistory(const.User,true,userName,userPassword);
                        LogHistory(Convert.ToInt64(Session["empId"]), true, userName, userPassword);

                        // Save Id and Password into cookie
                        if (this.chkRememberMe.Checked)
                            RememberMe();

                        // Added to use for CR
                        FormsAuthenticationTicket ticket = new
                                FormsAuthenticationTicket(1, //version
                                userName, // user name
                                DateTime.Now,             //creation
                                DateTime.Now.AddMinutes(30), //Expiration (you can set it to 1 month
                                true,   //Persistent
                                String.Format("{0} {1} {2} VMS", objDictionary["SecurityUserId"], userName, userPassword));
                        // additional informations: 0:UserId, 1: UserName, 2: Encrypted Password, 3: Display Name, VMS to identify the user system.

                        string hashedTicket = FormsAuthentication.Encrypt(ticket);

                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashedTicket);
                        HttpContext.Current.Response.Cookies.Add(cookie);
                        if (Request.QueryString["ReturnUrl"] != null)
                            Response.Redirect(Request.QueryString["ReturnUrl"].ToString());
                        else
                            Response.Redirect("~/UI/Admin_Home.aspx");
                        
                    }
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Login Id or Password does not match";
                }

            }
            catch (Exception ex)
            {
                this.lblMessage.Text = ex.Message;
            }

        }

        #endregion

        #region[Store User All Login Details in LoginHistory Table]
        private void LogHistory(long UserId, bool IsSuccessful, string UserName, String UserPassword)
        {
            string LoginIPAddress = Request.ServerVariables["remote_addr"];
            Session["LoginHistoryId"]=objLoginBLL.InsertLoginDetails(UserId,LoginIPAddress,IsSuccessful,UserName,UserPassword,true);
        }
        #endregion


        #region[ Remember me on this machine ]
        /// Remember me on this machine.
        private void RememberMe()
        {
            HttpCookie cook = new HttpCookie("RememberMe");
            cook.Values.Add("UID", this.txtLogin.Text.Trim());
            cook.Values.Add("PWD", this.txtPass.Text.Trim());
            cook.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Add(cook);
        }
        #endregion

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }
       
    }
}