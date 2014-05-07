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
using METAOPTION.BAL;

namespace METAOPTION.UI
{
    public partial class AdminChangePassword : System.Web.UI.Page,IPagePermission
    {
       public const string PASSWORD_MISMATCH_ERROR = "New Password & Confirm Password should match!";
       public const string PASSWORD_CHANGED_SUCCESSFULLY = "Password has been changed successfully";
       public const string PAGE     = "ADMIN";
       public const string PAGERIGHT = "ADMIN.CHANGEPASSWORD";
       long empId = 0;
       string username = string.Empty;

           #region [Page Load Event]
       protected void Page_Load(object sender, EventArgs e)
       {

           #region [Fetch UserId & Check Permission]
           ////Check Admin Preivelges
           CheckUserPagePermissions();
           
           txtNewPassword.Focus();
           if (!IsPostBack)
           {
               if (!string.IsNullOrEmpty(Request.QueryString["UserName"]))
                   txtLogin.Text = Request.QueryString["UserName"];
           }
           #endregion
       }

       #endregion
          
           #region IPagePermission Members

       public void CheckUserPagePermissions()
       {
           System.Collections.Generic.List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, PAGE);
           bool bTrue = true;
           if (!dict.Contains(PAGERIGHT))
           {
               bTrue = false;
           }

           //Redirect User to Common Page for InSufficient Rights
           if (!(dict.Contains(PAGERIGHT) || bTrue))
               Response.Redirect("Permission.aspx?MSG=ADMIN.CHANGEPASSWORD");
           
       }

       #endregion

           #region [Click event of Button for Change User Password]
        /// <summary>
        /// Change User Password,This page lets Admin user to change selected user password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnChangeUserPassword_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Code"]))
                empId = Convert.ToInt64(Request.QueryString["Code"]);


            string strNewPwd = EncryptMD5.Encrypt(this.txtNewPassword.Text.Trim());
            string strConfirmPwd = EncryptMD5.Encrypt(this.txtConfirmNewPassword.Text.Trim());
            
            if (strNewPwd.CompareTo(strConfirmPwd) == 0)
                ChangeUserPassword(strNewPwd,empId);
            else
                lblErrorMessage.Text = PASSWORD_MISMATCH_ERROR;

        }

       #endregion

           #region [Change User Password]
        /// <summary>
        /// This method allow Admin person to change password of other users.
        /// </summary>
        protected void ChangeUserPassword(string strNewPwd,long empId)
        {
            bool bStatus = METAOPTION.BAL.MakeUserBAL.ChangedPassword(empId, strNewPwd);
            if (!bStatus)
            {
                lblErrorMessage.Text = PASSWORD_CHANGED_SUCCESSFULLY;
                 btnChangeUserPassword.Enabled = false;

            }
        }
        #endregion

           #region [handle click event of back button to move back to parent screen]
        /// <summary>
        /// Handle Click event of Back Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request["ReturnUrl"]);
        }



#endregion
    }
}
