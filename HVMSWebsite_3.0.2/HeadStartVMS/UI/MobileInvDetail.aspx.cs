using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using METAOPTION.BAL;

namespace METAOPTION.UI
{
    public partial class MobileInvDetail : System.Web.UI.Page
    {
        long _InvID = 0;
        String _VIN;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.HasKeys())
            {
                _InvID = Convert.ToInt64(Request.QueryString["ID"]);
                _VIN = Request.QueryString["VIN"];
            }

            if (!Page.IsPostBack)
                BindCommonInvDetail();
        }

        protected void ibtnEditBasicCarInfo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("https://www.google.co.in/");
        }

        protected void BindCommonInvDetail()
        {
            InventoryBAL bal = new InventoryBAL();
            fvCommonInvDetails.DataSource = bal.GetCommonInvDetail(_InvID, _VIN);
            fvCommonInvDetails.DataBind();
        }

    }
}