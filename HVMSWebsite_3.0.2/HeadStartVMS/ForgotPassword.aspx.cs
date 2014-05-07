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
using dotless;
using EcmaScript;
using Yahoo;
using SquishIt;
using SquishIt.Framework;
using SquishIt.Framework.Css.Compressors;
using SquishIt.Framework.JavaScript.Minifiers;


namespace METAOPTION
{
   public partial class ForgetPassword : System.Web.UI.Page
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         this.divMessage.Visible = false;
         this.lblError.Visible = false;
      }

      protected void btnSubmit_Click(object sender, EventArgs e)
      {
         
         if(String.IsNullOrEmpty(txtUserName.Text.Trim()))
         {
            this.txtUserName.Focus();
            this.txtUserName.BackColor = System.Drawing.Color.LightGray;
            return;
         }
         else if(String.IsNullOrEmpty(this.txtEmail.Text.Trim()) ||(!System.Text.RegularExpressions.Regex.IsMatch(this.txtEmail.Text.Trim(), @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")) )
         {
            this.txtEmail.Focus();
            this.txtEmail.BackColor = System.Drawing.Color.LightGray;          
            return;
         }

         String strEmail = string.Empty;
         String strPassword = string.Empty;
         DataTable dTable = MakeUserBAL.Email_Password(this.txtUserName.Text.Trim(), this.txtEmail.Text.Trim());
         if (dTable != null && dTable.Rows.Count > 0)
         {
            strEmail = Convert.ToString(dTable.Rows[0]["Email"]);
            strPassword = Convert.ToString(dTable.Rows[0]["Password"]);
            strPassword = EncryptMD5.Decrypt(strPassword);

            EmailMessage.sendEmail("Password recovery", "Password recovery", strEmail);
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

     
   }
}
