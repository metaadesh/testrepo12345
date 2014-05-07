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
using METAOPTION.Common;

namespace METAOPTION
{
    public partial class Admin_ForgetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.divMessage.Visible = false;
            this.lblError.Visible = false;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                this.txtUserName.Focus();
                this.txtUserName.BackColor = System.Drawing.Color.LightGray;
                return;
            }
            else if (String.IsNullOrEmpty(this.txtEmail.Text.Trim()) || (!System.Text.RegularExpressions.Regex.IsMatch(this.txtEmail.Text.Trim(), @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")))
            {
                this.txtEmail.Focus();
                this.txtEmail.BackColor = System.Drawing.Color.LightGray;
                return;
            }

            String strEmail = string.Empty;
            String strPassword = string.Empty;
            DataTable dTable = Admin_LoginBAL.Email_Password(this.txtUserName.Text.Trim(), this.txtEmail.Text.Trim());
            if (dTable != null && dTable.Rows.Count > 0)
            {
                strEmail = Convert.ToString(dTable.Rows[0]["Email"]);
                strPassword = Convert.ToString(dTable.Rows[0]["Password"]);
                strPassword = EncryptMD5.Decrypt(strPassword);

                SendEmailMessage.sendEmail("Password recovery", "Password recovery", strEmail);
                this.divMessage.Visible = true;
                this.tblForget.Visible = false;
            }
            else
            {
                this.lblError.Visible = true;
                //this.lblError.Text = "User does not exist!";
                this.lblError.Text = "Incorrect user name or email!";
            }


        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

        }
    }
}