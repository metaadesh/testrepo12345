using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace METAOPTION.UI
{
    public partial class ManageLocation : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["MasterPageNoLeftPanel"])))
                    this.MasterPageFile = Convert.ToString(Session["MasterPageNoLeftPanel"]);
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
                BindGrid();
        }

        #region[Bind Grid]
        protected void BindGrid()
        {
            METAOPTION.LocationBAL bal = new METAOPTION.LocationBAL();
            gvLocation.DataSource = bal.LocationInventoryStats(Convert.ToInt64(ddlLocationStatus.SelectedValue), Constant.OrgID);
            gvLocation.DataBind();
        }
        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageEntityLocation.aspx");
        }

        //#region[Paging]
        //protected void gvLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvLocation.PageIndex = e.NewPageIndex;
        //    BindGrid();
        //}
        //#endregion

        protected void ddlLocationStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();

        }
    }
}