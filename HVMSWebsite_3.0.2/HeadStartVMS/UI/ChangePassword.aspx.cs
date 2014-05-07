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
using METAOPTION;
using METAOPTION.BAL;
namespace HeadStartVMS.UI
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["MasterPage"] )))
                    this.MasterPageFile = Convert.ToString(Session["MasterPage"]);
                else
                    Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(Request.Url.ToString()));
            }
            catch (Exception ex)
            {
                Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(Request.Url.ToString()));
            }
           
        }

        #region [ Page Load ]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblStatus.Text = "";
            }

        }
        #endregion

        #region [ Update Password ]
        protected void btnSave_Click(object sender, EventArgs e)
        {
            
                bool bValidatePassword = false;
                string userNewPassword = EncryptMD5.Encrypt(this.txtNewPassword.Text.Trim());
                string userOldPassword = EncryptMD5.Encrypt(this.txtOldPassword.Text.Trim());
                bValidatePassword = METAOPTION.BAL.MakeUserBAL.CheckUserOldPassword(userOldPassword, Convert.ToInt64(Session["empId"]));
                if (bValidatePassword)
                {
                    bool Status = METAOPTION.BAL.MakeUserBAL.ChangedPassword(Convert.ToInt64(Session["empId"]), userNewPassword);
                    if (!Status)
                    {
                        lblStatus.Text = "Password successfully changed";
                    }
                }
                else
                {
                    lblStatus.Text = "Old password does not matched!";
                }
            }

       
            
        }
        #endregion
    }

