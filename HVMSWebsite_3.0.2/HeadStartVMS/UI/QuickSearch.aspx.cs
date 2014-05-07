using System;
using System.Collections.Generic;
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
    public partial class QuickSearch : System.Web.UI.Page
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
            if (Request.QueryString["param1"] == null && Request.QueryString["param2"] == null)
                return;

            int VINMatch = Convert.ToInt32(Request.QueryString["param1"]);
            string VINNo = Request.QueryString["param2"];

            if (!IsPostBack)
            {
                BindData(VINMatch, VINNo, Constant.OrgID,Constant.UserId);
            }
        }

        


        /// <summary>
        /// Bind Data In GridView Based on VINNO matched
        /// </summary>
        protected void BindData(int VINMATCH, string VINNo, Int16 OrgID, long userid)
        {
            gvInventoryList.DataSource = InventoryBAL.QuickSearchScanInventories(VINMATCH, VINNo, OrgID,userid);
            gvInventoryList.DataBind();
        }
    }
}