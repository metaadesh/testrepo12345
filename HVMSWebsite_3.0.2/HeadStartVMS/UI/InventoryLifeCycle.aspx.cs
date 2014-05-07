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
    public partial class InventoryLifeCycle : System.Web.UI.Page
    {
        long _code = -1;

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
            if (!Page.IsPostBack)
            {
                try
                {
                    if (Request.QueryString["Code"] != null && Request.QueryString["Code"].ToString() != "")
                    {
                        Util.Validate_QueryString_Value(6, Request.QueryString["Code"].ToString(), Constant.OrgID);
                    }
                }
                catch { }
            }

            #region [Set InventoryId by reading from QueryString]
            //Set InventoryId by reading from QueryString
            if (Request["Code"] != null)
                try
                {
                    this._code = Convert.ToInt64(Request["Code"]);
                }

                catch
                {

                }
            #endregion

            if (!IsPostBack)
            {
                //Load Chrome History Data
                LoadInventoryEvents();
            }
        }
        /// <summary>
        /// This method LoadChromeHistory in GridView Control
        /// </summary>
        private void LoadInventoryEvents()
        {
            //if (Convert.ToString(Session["LoginEntityTypeID"]) == "2")
            //{
            //    String ParentEntityID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString();

            //    gvInventoryCycle.DataSource = Common.GetInventoryLifeCycleEvents(_code, Convert.ToInt32(Session["UserEntityID"]), Convert.ToInt32(ParentEntityID), Convert.ToInt32(Session["LoginEntityTypeID"]));
            //    gvInventoryCycle.DataBind();
            //}
            //else{
                gvInventoryCycle.DataSource = Common.GetInventoryLifeCycleEvents(_code);
                gvInventoryCycle.DataBind();
           // }
        }
      
       
       /// <summary>
        ///  Handle PageIndex Change event of Gridview control
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       protected void gvInventoryCycle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvInventoryCycle.PageIndex = e.NewPageIndex;
            LoadInventoryEvents();
        }
        /// <summary>
        /// Go back to parent page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       protected void lnkbtnBack_Click(object sender, EventArgs e)
       {
           Response.Redirect("InventoryDetail.aspx?Code=" + _code);
       }
    }
}
