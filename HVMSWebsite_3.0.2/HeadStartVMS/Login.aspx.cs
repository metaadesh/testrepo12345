using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using METAOPTION;
using METAOPTION.AutoFillCustomersService;
using SSOLib.Service;
using METAOPTION.BAL;
using dotless;
using EcmaScript;
using Yahoo;
using SquishIt;
using SquishIt.Framework;
using SquishIt.Framework.Css.Compressors;
using SquishIt.Framework.JavaScript.Minifiers;
using System.Net;
using System.Net.Sockets;

namespace METAOPTION.UI
{

    public partial class Login : System.Web.UI.Page
    {
        LoginBLL objLoginBLL = new LoginBLL();
        String IPSecurityKey = ConfigurationManager.AppSettings["EnableIPSecurity"];

        #region[ Page Load Event ]
        /// <summary>
        /// Page Load Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Page.IsPostBack || Page.IsCallback))
            {
                if (Request.Cookies["RememberMe"] != null)
                {
                    HttpCookie cook = (HttpCookie)Request.Cookies["RememberMe"];
                    this.txtLogin.Text = Convert.ToString(cook.Values["UID"]);
                    this.txtPass.Attributes.Add("value", Convert.ToString(cook.Values["PWD"]));
                    
                    //Read OrgCode cookie
                    if (!String.IsNullOrEmpty(txtLogin.Text))
                    {
                        string cookieName = String.Format("OrgCode_{0}", this.txtLogin.Text.ToLower());
                        if (Request.Cookies[cookieName] != null)
                            this.txtOrgCode.Text = (Request.Cookies[cookieName].Value);
                    }
                }
            }
        }
        #endregion
        #region[ Validate user and redirect to home page ]
        /// <summary>
        /// Validate user and redirect to home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        #region[Old Code "btnLogin_Click"]
        /* 
      protected void btnLogin_Click(object sender, ImageClickEventArgs e)
      {
          try
          {
              string userName = txtLogin.Text.Trim();
              string userPassword = EncryptMD5.Encrypt(this.txtPass.Text.Trim());

              // Check if username is valid
              bool result = objLoginBLL.CheckUserName(userName);
              if (result == true)
              {

                  // Check if password is valid
                  bool res = objLoginBLL.CheckUserPassword(userName, userPassword);
                  if (res == true)
                  {
                      Dictionary<string, string> objDictionary = objLoginBLL.GetLogin(userName, userPassword);
                      Session["empId"] = objDictionary["SecurityUserId"];
                      Session["disName"] = objDictionary["DisplayName"];
                      Constant.LiveUsers += 1;
                      LogHistory(Constant.UserId, true, userName, userPassword);

                      //List<String> rights = CommonBAL.GetPagePermission(Constant.UserId, "WEBAPP");
                      //if (rights.Count < 1)
                      //{
                      //    Session.Clear();
                      //    Response.Redirect("~/UI/Permission.aspx?MSG=WEBAPP.LOGIN"); 
                      //}
                      //else
                      //{
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


                          if (Request["ReturnUrl"] != null)
                              Response.Redirect(Request["ReturnUrl"].ToString());
                          else
                              Response.Redirect("UI/default.aspx");
                      //}

                  }
                  else
                  {
                      LogHistory(0, false, userName, userPassword);
                      lblMessage.Visible = true;
                      lblMessage.Text = "Password does not match";
                  }
              }
              else
              {

                  LogHistory(0, false, userName, userPassword);
                  lblMessage.Visible = true;
                  lblMessage.Text = "User does not exist";
              }
          }
          catch (Exception ex)
          {
              this.lblMessage.Text = ex.Message;
          }
          Session["SystemId"] = 10;
      }
        */
        #endregion

        #region[Optimized Code "btnLogin_Click"]
        protected void btnLogin_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string userName = txtLogin.Text.Trim();
                string userPassword = EncryptMD5.Encrypt(this.txtPass.Text);
                string IPAddress = Request.UserHostAddress;
                string OrgCode = txtOrgCode.Text.ToUpper().Trim();
                DataTable dtBuyer = new DataTable();
                //LoggerService.LogFile(Request.UserHostAddress);

                //Fetch default system for current Organization
                Int32 sysID = objLoginBLL.GetDefaultSystem(txtOrgCode.Text.Trim());
                Session["SystemId"] = sysID;

                #region[OldCode]
                //string ipAddedss = getclientIP();
                //int ret = 0;
                //Check whether IPSecurity is Enabled or Disabled
                //if (IPSecurityKey.ToUpper() == "TRUE")
                //{
                //    #region[Added by Rupendra 11 Dec 12 for Manage IP Permission]

                //    //string strHostName = System.Net.Dns.GetHostName();
                //    //string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(1).ToString();

                //    if (!string.IsNullOrEmpty(IPAddress))
                //        ret = objLoginBLL.CheckIPAddess(IPAddress);

                //    #endregion
                //}
                //else
                //    ret = 1;

                //if (ret == 1)
                //{
                #endregion

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
                        Response.Redirect("UI/Permission.aspx?MSG=AccessDenied");
                    //else allow him to login
                    else
                    {
                        Session["empId"] = objDictionary["SecurityUserId"];
                        Session["UserEntityID"] = objDictionary["EntityID"];
                        Session["disName"] = objDictionary["DisplayName"];
                        Session["LoginEntityTypeID"] = objDictionary["EntityTypeID"];
                        Session["UserName"] = userName.Trim();
                        Session["OrgID"] = objDictionary["OrgID"];
                        Session["OrgCode"] = objDictionary["OrgCode"];
                        Session["Organisation"] = objDictionary["Organisation"];

                        if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
                        {
                            dtBuyer = objLoginBLL.GetBuyer_IsDirect(Convert.ToInt32(Session["UserEntityID"]));
                            if (dtBuyer.Rows.Count > 0)
                            {
                                Session["BuyerIsDirect"] = Convert.ToString(dtBuyer.Rows[0]["IsDirectBuyer"]);
                                Session["BuyerParent"] = Convert.ToString(dtBuyer.Rows[0]["ParentBuyer"]);
                                Session["BuyerAccessLevel"] = Convert.ToString(dtBuyer.Rows[0]["AccessLevel"]);
                            }
                        }
                        SetMasterPage();
                        Constant.LiveUsers += 1;
                        LogHistory(Constant.UserId, true, userName, userPassword);

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

                        if (Request["ReturnUrl"] != null)
                            Response.Redirect(Request["ReturnUrl"].ToString());
                        else if (Convert.ToString(Session["LoginEntityTypeID"]) == "3")
                            Response.Redirect("UI/VendorDefault.aspx");
                        else if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
                            Response.Redirect("UI/BuyerDefault.aspx");
                        else if (Convert.ToString(Session["LoginEntityTypeID"]) == "1")
                            Response.Redirect("UI/DealerDefault.aspx");
                        else
                            Response.Redirect("UI/default.aspx");

                    }


                }
                else
                {
                    LogHistory(0, false, userName, userPassword);
                    lblMessage.Visible = true;
                    lblMessage.Text = "UserID/Password/Org Code does not match ";
                }

                #region[OldCode]
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You are not a authorized user!');", true);
                //}
                #endregion

            }
            catch (Exception ex)
            {
                // this.lblMessage.Text = ex.Message;            Hide Exception Message for Security Purpose
                this.lblMessage.Text = "UserID/Password/Org Code does not match ";
            }
            //Session["SystemId"] = 10;
        }
        #endregion

        public void SetMasterPage()
        {
            if (Session["LoginEntityTypeID"] != null)
            {
                //if (Convert.ToInt32(Session["LoginEntityTypeID"].ToString()) == 1) //If Dealer
                //{
                //    Session["MasterPage"] = "~/UI/DealerMasterPage.Master";
                //    Session["MasterPageNoLeftPanel"] = "~/UI/DealerMasterPageNoLeftPanel.Master";
                //    Session["LaneAssignmentsMaster"] = "~/UI/DealerLaneAssignmentsMaster.Master";
                //    Session["PreInventoryMaster"] = "~/UI/DealerPreInventoryMaster.Master";
                //    Session["ReportMaster"] = "~/VMSReports/ReportMaster.Master";
                //}


                if (Convert.ToInt32(Session["LoginEntityTypeID"].ToString()) == 2) // If Buyer
                {
                    Session["MasterPage"] = "~/UI/BuyerMasterPage.Master";
                    Session["MasterPageNoLeftPanel"] = "~/UI/BuyerMasterPageNoLeftPanel.Master";
                    Session["LaneAssignmentsMaster"] = "~/UI/BuyerLaneAssignmentsMaster.Master";
                    Session["PreInventoryMaster"] = "~/UI/BuyerPreInventoryMaster.Master";
                    Session["ReportMaster"] = "~/VMSReports/BuyerReportMaster.Master";

                    Session["MasterPageFullScreen"] = "~/UI/BuyerMasterPageFullScreen.Master";

                }
                else if (Convert.ToInt32(Session["LoginEntityTypeID"].ToString()) == 3) // If Vendor
                {
                    Session["MasterPage"] = "~/UI/VendorMasterPage.Master";         //VendorMasterPage.Master
                    Session["MasterPageNoLeftPanel"] = "~/UI/VendorMasterPageNoLeftPanel.Master";
                    Session["LaneAssignmentsMaster"] = "~/UI/VendorLaneAssignmentsMaster.Master";
                    Session["PreInventoryMaster"] = "~/UI/VendorPreInventoryMaster.Master";
                    Session["ReportMaster"] = "~/VMSReports/ReportMaster.Master";

                    Session["MasterPageFullScreen"] = "~/UI/VendorMasterPageFullScreen.Master";

                }
                else if (Convert.ToInt32(Session["LoginEntityTypeID"].ToString()) == 1) // If Vendor
                {
                    Session["MasterPage"] = "~/UI/DealerMasterPage.Master";         //VendorMasterPage.Master
                    Session["MasterPageNoLeftPanel"] = "~/UI/DealerMasterPageNoLeftPanel.Master";
                    Session["LaneAssignmentsMaster"] = "~/UI/DealerLaneAssignmentsMaster.Master";
                    Session["PreInventoryMaster"] = "~/UI/DealerPreInventoryMaster.Master";
                    Session["ReportMaster"] = "~/VMSReports/ReportMaster.Master";

                    Session["MasterPageFullScreen"] = "~/UI/DealerMasterPageFullScreen.Master";

                }
                else if (Convert.ToInt32(Session["LoginEntityTypeID"].ToString()) == 5) //If Employee
                {
                    Session["MasterPage"] = "~/UI/MasterPage.Master";
                    Session["MasterPageNoLeftPanel"] = "~/UI/MasterPageNoLeftPanel.Master";
                    Session["LaneAssignmentsMaster"] = "~/UI/LaneAssignmentsMaster.Master";
                    Session["PreInventoryMaster"] = "~/UI/PreInventoryMaster.Master";
                    Session["ReportMaster"] = "~/VMSReports/ReportMaster.Master";

                    Session["MasterPageFullScreen"] = "~/UI/MasterPageFullScreen.Master";
                }
            }
        }

        #endregion
        private void LogHistory(long UserId, bool isSuccessful, string userName, String Password)
        {
            // Log user history to database.
            String cIPAddress = Request.ServerVariables["remote_addr"];
            Session["LoginHistoryId"] = objLoginBLL.InsertLoginDetails(UserId, cIPAddress, isSuccessful, userName, Password, true);
        }

        #region[ Remember me on this machine ]
        /// <summary>
        /// Remember me on this machine.
        /// </summary>
        private void RememberMe()
        {
            HttpCookie cook = new HttpCookie("RememberMe");
            cook.Values.Add("UID", this.txtLogin.Text.Trim());
            cook.Values.Add("PWD", this.txtPass.Text.Trim());
            //cook.Values.Add("ORG", this.txtOrgCode.Text.Trim());
            cook.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Add(cook);

            //set username as key
            string key = this.txtLogin.Text.Trim().ToLower();
            string cookName = String.Format("OrgCode_{0}", key);
            if (Response.Cookies[cookName].Value == null)
            {
                Response.Cookies[cookName].Value = this.txtOrgCode.Text;
                Response.Cookies[cookName].Expires = DateTime.Now.AddDays(30);
            }
        }
        #endregion

        #region[Insert UserDetails to UserMaster]
        private void Inser_UserDetails(string strUser)
        {
            string website = string.Empty;
            int websiteid;
            long websiteuserid;
            string username = string.Empty;
            string screenname = string.Empty;

            String _ConnectionString = ConfigurationManager.ConnectionStrings["HeadStartScraping_connectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            try
            {

                string[] str = strUser.Split('|');

                websiteuserid = Convert.ToInt64(str[0]);
                websiteid = Convert.ToInt32(str[1]);
                username = str[2];
                screenname = str[3];
                website = "HeadStartVMS";

                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "dbo.UserMaster_Insert";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter("@Website", website));
                cmd.Parameters.Add(new SqlParameter("@WebsiteId", websiteid));
                cmd.Parameters.Add(new SqlParameter("@WebsiteUserId", websiteuserid));
                cmd.Parameters.Add(new SqlParameter("@UserName", username));
                cmd.Parameters.Add(new SqlParameter("@ScreenName", screenname));

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { this.lblMessage.Text = ex.Message; }
            finally
            {
                conn.Close();
            }


        }
        #endregion

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // Bundle.Css()
            //.Add("~/CSS/AjaxRelated.css")
            //.Add("~/CSS/MainStyle.css")
            //.Add("~/CSS/ModalPopUp.css")
            //.Add("~/CSS/ControlStyle.css")
            //.Add("~/CSS/thickbox.css")
            //.Add("~/CSS/tipTip.css")
            //.ForceRelease().WithCompressor(CssCompressors.YuiCompressor)
            //.Render("~/CSS/min.css");

            // Bundle.JavaScript()
            //.Add("~/CSS/Menu.js").WithMinifier(JavaScriptMinifiers.NullMinifier)
            //.Add("~/CSS/jquery-1.2.6.min.js").WithMinifier(JavaScriptMinifiers.NullMinifier)
            //.Add("~/CSS/jquery-1.7.2.min.js").WithMinifier(JavaScriptMinifiers.NullMinifier)
            //.Add("~/CSS/jquery.browser.min.js").WithMinifier(JavaScriptMinifiers.NullMinifier)
            //.Add("~/CSS/PageScript.js").WithMinifier(JavaScriptMinifiers.NullMinifier)
            //.Add("~/CSS/thickbox-compressed.js").WithMinifier(JavaScriptMinifiers.NullMinifier)
            //.Add("~/CSS/jquery.tipTip.minified.js").WithMinifier(JavaScriptMinifiers.NullMinifier)
            //.Add("~/CSS/jquery.tipTip.js").WithMinifier(JavaScriptMinifiers.NullMinifier)
            //.ForceRelease().WithMinifier(JavaScriptMinifiers.Ms)
            //.Render("~/CSS/min.js");

        }
        public static string getclientIP()
        {
            IPHostEntry host;
            string localIP = "?";
            try
            {
                host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        localIP = ip.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogServiceError(ex, HttpContext.Current.Request.Url.ToString(), String.Empty, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return localIP;
        }
    }
}