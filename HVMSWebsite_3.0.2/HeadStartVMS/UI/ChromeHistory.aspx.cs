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
using METAOPTION.BAL;

namespace METAOPTION.UI
{
    public partial class ChromeHistory : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                //Load Chrome History Data
                LoadChromeHistory();
            }
        }
        /// <summary>
        /// This method LoadChromeHistory in GridView Control
        /// </summary>
        private void LoadChromeHistory()
        {
          gvChromeHistory.DataSource =  Common.ShowChromeHistory();
          gvChromeHistory.DataBind();
        }
        /// <summary>
        /// Handle PageIndex Change event of Gridview control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvChromeHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvChromeHistory.PageIndex = e.NewPageIndex;
            LoadChromeHistory();
        }
    }
}
