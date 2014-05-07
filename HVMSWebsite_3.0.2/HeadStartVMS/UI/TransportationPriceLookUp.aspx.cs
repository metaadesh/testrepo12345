using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using METAOPTION.BAL;

namespace METAOPTION.UI
{
    public partial class TransportationPriceLookUp : System.Web.UI.Page
    {
        VendorBAL ObjTransLookUp = new VendorBAL();
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
            if (!IsPostBack)
            {
                BindDealer();
                BindZone();
                BindGrid();
            }
        }
        public void BindDealer()
        {
            if (Convert.ToInt32(Session["LoginEntityTypeID"]) == 3)
                ddlVendor.DataSource = ObjTransLookUp.GetAllVendor(Convert.ToInt32(Session["UserEntityID"]), Convert.ToInt32(Session["LoginEntityTypeID"]), Constant.OrgID);
            else
                ddlVendor.DataSource = ObjTransLookUp.GetAllVendor(-1, -1, Constant.OrgID);
            ddlVendor.DataValueField = "VendorID";
            ddlVendor.DataTextField = "VendorName";
            ddlVendor.DataBind();
            ddlVendor.Items.Insert(0, new ListItem("All", "-1"));
            if (Convert.ToInt32(Session["LoginEntityTypeID"]) == 3 && ddlVendor.Items.FindByValue(Convert.ToString(Session["UserEntityID"])) != null)
            {
                ddlVendor.SelectedValue = Convert.ToString(Session["UserEntityID"]);
                ddlVendor.Enabled = false;
            }

        }
        public void BindZone()
        {

            if (Convert.ToInt32(Session["LoginEntityTypeID"]) == 3)
                ddlZone.DataSource = ObjTransLookUp.GetAllZone(Convert.ToInt32(Session["UserEntityID"]), Convert.ToInt32(Session["LoginEntityTypeID"]), Constant.OrgID);
            else
                ddlZone.DataSource = ObjTransLookUp.GetAllZone(-1, -1, Constant.OrgID);
            ddlZone.DataValueField = "Zone";
            ddlZone.DataTextField = "Zone";
            ddlZone.DataBind();
            ddlZone.Items.Insert(0, new ListItem("All", "-1"));
        }
        public void BindGrid()
        {
            String Mileage = String.IsNullOrEmpty(txtMileage.Text) ? "-1" : txtMileage.Text.Trim();
            Int32 DealerID = ddlVendor.SelectedValue == "-1" ? -1 : Convert.ToInt32(ddlVendor.SelectedValue);
            if (Convert.ToInt32(Session["LoginEntityTypeID"]) == 3)
                grdPriceZoneDetails.DataSource = ObjTransLookUp.GetAllZonePrice(Convert.ToInt32(Session["UserEntityID"]), Convert.ToInt32(Session["LoginEntityTypeID"]), ddlZone.SelectedValue, Mileage, Constant.OrgID);
            else
                grdPriceZoneDetails.DataSource = ObjTransLookUp.GetAllZonePrice(DealerID, -1, ddlZone.SelectedValue, Mileage, Constant.OrgID);
                grdPriceZoneDetails.DataBind();


        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();

        }

        protected void grdPriceZoneDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPriceZoneDetails.PageIndex = e.NewPageIndex;
            BindGrid();
        }

    }
}