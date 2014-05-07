using System;
using System.Collections;
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
using METAOPTION;
using METAOPTION.BAL;

namespace METAOPTION.UI
{
    public partial class LiveUsers : System.Web.UI.Page
    {
        //EmployeeListBAL objEmployeeListBAL = new EmployeeListBAL();

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
            //CheckPermission();
            if (!IsPostBack)
            {
                if ((Session["LoginEntityTypeID"] != null) && ((Convert.ToString(Session["LoginEntityTypeID"]) == "3") || (Convert.ToString(Session["LoginEntityTypeID"]) == "1")))
                    BindAll_Ver211();
                else
                    BindAll();
            }
        }
        /// <summary>
        /// Bind all grids
        /// </summary>
        protected void BindAll()
        {
            //Load Live Users in GridControl
            LoadUsers();
            //later on need collapsable for weblogin history
            BindLoginHistory("Web");
            BindLoginHistory("Access");
        }

        #region [Added by Rupendra 1 Jan 2013 for Vendor, Dealer and Buyer login]
        /// <summary>
        /// Bind all grids
        /// </summary>
        protected void BindAll_Ver211()
        {
            //Load Live Users in GridControl
            LoadUsers_Ver211();
            //later on need collapsable for weblogin history
            BindLoginHistory_Ver211("Web");
            BindLoginHistory("Access");
        }

        protected void LoadUsers_Ver211()
        {
            if (Constant.LiveUsers >= 1)
            {
                gvLiveUsers.DataSource = METAOPTION.BAL.Common.ShowLiveUsers(Convert.ToInt32(Constant.LiveUsers), Constant.OrgID);
                gvLiveUsers.DataBind();
            }
        }

        protected void BindLoginHistory_Ver211(string from)
        {
            if (from == "Access")
            {
                gvAccessLoginHistory.DataSource = LoginBLL.GetLoginHistory("Access", Constant.OrgID);
                gvAccessLoginHistory.DataBind();
            }
            else
            {
                gvWebLoginHistory.DataSource = LoginBLL.GetLoginHistory_ver211("Web", Convert.ToInt32(Session["LoginEntityTypeID"]));
                gvWebLoginHistory.DataBind();
            }
        }
        #endregion

        #region [Bind All User]
        /// <summary>
        /// Bind List of Live Users in Grid and Show
        /// </summary>
        protected void LoadUsers()
        {
            if (Constant.LiveUsers >= 1)
            {
                gvLiveUsers.DataSource = METAOPTION.BAL.Common.ShowLiveUsers(Convert.ToInt32(Constant.LiveUsers), Constant.OrgID);
                gvLiveUsers.DataBind();
            }
        }
        #endregion

        /// <summary>
        /// Handle Page Index change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLiveUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvLiveUsers.PageIndex = e.NewPageIndex;
            LoadUsers();
        }

        /// <summary>
        /// Handle Page Index change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvAccessLoginHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAccessLoginHistory.PageIndex = e.NewPageIndex;
            BindLoginHistory("Access");
        }

        /// <summary>
        /// Handle Page Index change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvWebLoginHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvWebLoginHistory.PageIndex = e.NewPageIndex;
            if ((Session["LoginEntityTypeID"] != null) && ((Convert.ToString(Session["LoginEntityTypeID"]) == "3") || (Convert.ToString(Session["LoginEntityTypeID"]) == "1")))
                BindLoginHistory_Ver211("Web");
            else
                BindLoginHistory("Web");

            Page.ClientScript.RegisterStartupScript(typeof(LiveUsers), "showHideHistory", "showHideContent();", true);
        }


        /// <summary>
        /// Fetch Login History details in grid
        /// </summary>
        /// <param name="from"></param>
        protected void BindLoginHistory(string from)
        {
            if (from == "Access")
            {
                gvAccessLoginHistory.DataSource = LoginBLL.GetLoginHistory("Access", Constant.OrgID);
                gvAccessLoginHistory.DataBind();
            }
            else
            {
                gvWebLoginHistory.DataSource = LoginBLL.GetLoginHistory("Web", Constant.OrgID);
                gvWebLoginHistory.DataBind();
            }
        }

        protected void timerLiverUser_Tick(object sender, EventArgs e)
        {
            BindAll();
        }

        protected String GetEditURL(Object EntityTypeID)
        {
            String strEditURL = String.Empty;
            try
            {
                if (!String.IsNullOrEmpty(EntityTypeID.ToString()))
                {
                    Int32 ID = Int32.Parse(EntityTypeID.ToString());
                    switch (ID)
                    {
                        case 1:
                            strEditURL = "ViewDealer.aspx?Mode=View&EntityId=" + Eval("EntityId") + "&type=1";
                            break;
                        case 2:
                            //
                            strEditURL = "ViewBuyerDetails.aspx?Mode=View&BuyerId=" + Eval("EntityId") + "&type=2";
                            break;
                        case 3:
                            strEditURL = "ViewVendor.aspx?Mode=View&EntityId=" + Eval("EntityId") + "&type=3";
                            break;
                        case 4:
                            strEditURL = "ViewUtilityCompany.aspx?Mode=View&EntityId=" + Eval("EntityId") + "&type=4";
                            break;
                        case 5:
                            strEditURL = "ViewEmployee.aspx?Mode=View&EmployeeId=" + Eval("EntityId") + "&type=5";
                            break;
                    }

                    if (strEditURL != String.Empty)
                    {
                        strEditURL += "&ReturnUrl=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri).ToString();
                    }
                }
            }
            catch
            {
                strEditURL = "#";
            }
            return strEditURL;
        }
    }
}
