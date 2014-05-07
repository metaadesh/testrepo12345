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

namespace METAOPTION.UI
{
    public partial class AdminPermission : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["msg"] != null)
                this.lblPermission.Text = Convert.ToString(Request["msg"]);

            if (Request["msg"].ToString().ToLower().Contains("accessdenied"))
            {
                string test = Convert.ToString(Request.UrlReferrer);
                paralogin.Visible = true;
                paradefault.Visible = false;
                tdPermission.Visible = false;
                lblSorry.Text = "Sorry, You don't have sufficient permission to see the requested page!<br />Access Denied";
            }
            else
            {
                hlnkDefault.HRef = "Admin_Login.aspx";
                paradefault.Visible = true;
                paralogin.Visible = false;
            }
        }
    }
}