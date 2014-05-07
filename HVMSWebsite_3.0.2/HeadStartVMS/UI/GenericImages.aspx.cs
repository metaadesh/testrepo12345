using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using METAOPTION.BAL;
using System.Configuration;
using System.Web.Security;
using System.Data;

namespace METAOPTION.UI
{
    public partial class GenericImages : System.Web.UI.Page
    {
        public int ImagesPeriod = 0;
        CommonBAL bal = new CommonBAL();
        String ParentBuyerID = string.Empty;
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
            ImagesPeriod = Convert.ToInt32(ConfigurationManager.AppSettings["ImagesPeriod"]);
            if (!Page.IsPostBack)
            {
                if (Convert.ToString(Session["BuyerAccessLevel"]) == "1")
                    ParentBuyerID = "-1";
                else
                    ParentBuyerID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? "-1" : Session["BuyerParent"].ToString();           
                BindAddedByddl();
                BindGrid();
            }
        }

        #region[Bind users]
        protected void BindAddedByddl()
        {
            Int32 ParentID = -1;
            if (Convert.ToString(Session["BuyerAccessLevel"]) != "1")               
                ParentID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? -1 : Convert.ToInt32(Session["BuyerParent"]);

            ddlAddedBy.DataSource = bal.GetGenericImagesUsers_AddedBy(Convert.ToInt32(Session["empId"]),Convert.ToInt32(ParentID), Convert.ToInt32(Session["LoginEntityTypeID"]),Constant.OrgID);         
            ddlAddedBy.DataTextField = "DisplayName";
            ddlAddedBy.DataValueField = "SecurityUserID";
            ddlAddedBy.DataBind();
            ddlAddedBy.Items.Insert(0, new ListItem("ALL", "-1"));
            if (Convert.ToString(Session["LoginEntityTypeID"]) != "5")
            {                
                if (ddlAddedBy.Items.FindByValue(Convert.ToString(Session["empid"])) != null && ParentID == -1 && ddlAddedBy.Items.Count == 2)
                {
                    ddlAddedBy.SelectedValue = Convert.ToString(Session["empid"]);
                    ddlAddedBy.Enabled = false;
                }
                else if(ddlAddedBy.Items.FindByValue(Convert.ToString(Session["empid"])) != null)
                    ddlAddedBy.SelectedValue = Convert.ToString(Session["empid"]);
                else if (ddlAddedBy.Items.Count == 1)
                    ddlAddedBy.Enabled = false;
            }            
        }
        #endregion

        #region[Bind Grid]
        protected void BindGrid()
        {
            Int32 ParentID = -1;
            if (Convert.ToString(Session["BuyerAccessLevel"]) != "1")
                ParentID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? -1 : Convert.ToInt32(Session["BuyerParent"]);
            
            //gvGenericImages.DataSource = bal.GetAllGenericImages_Ver211(txtVIN.Text.Trim(), Convert.ToInt64(ddlAddedBy.SelectedValue), Convert.ToInt32(Session["LoginEntityTypeID"]), Convert.ToInt32(Session["UserEntityID"]), ParentID);
            //gvGenericImages.DataBind();

            String VIN = String.IsNullOrEmpty(txtVIN.Text.Trim()) ? String.Empty : txtVIN.Text.Trim();
            ObjectDataSource odsImages = new ObjectDataSource();
            odsImages.TypeName = "METAOPTION.BAL.CommonBAL";
            odsImages.EnablePaging = true;
            odsImages.SelectMethod = "GetAllGenericImages_Ver211";
            odsImages.SelectCountMethod = "GetAllGenericImagesCount_Ver211";
            odsImages.SelectParameters.Add("VIN", VIN);
            odsImages.SelectParameters.Add("AddedBy", ddlAddedBy.SelectedValue);
            odsImages.SelectParameters.Add("EntityTypeID", DbType.Int32, Session["LoginEntityTypeID"].ToString());
            odsImages.SelectParameters.Add("EntityID", DbType.Int32, Session["UserEntityID"].ToString());
            odsImages.SelectParameters.Add("ParentEntityID", Convert.ToString(ParentID));
            odsImages.SelectParameters.Add("StartRowIndex", DbType.Int32, gvGenericImages.PageIndex.ToString());
            odsImages.SelectParameters.Add("MaximumRows", DbType.Int32, gvGenericImages.PageSize.ToString());
            odsImages.SelectParameters.Add("OrgID", DbType.Int16, Constant.OrgID.ToString());
            gvGenericImages.DataSource = odsImages;
            gvGenericImages.DataBind();

        }
        #endregion

        #region[Paging]
        protected void gvGenericImages_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGenericImages.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        #endregion

        #region[Search button event handler]
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region[View Images]
        // PreInventory Images
        protected void ibtncars_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibtn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)ibtn.NamingContainer;
            HiddenField hfVIN = (HiddenField)row.FindControl("hfVIN");

            ifrmImages.Attributes.Add("src", String.Format("GenericImageGallery.aspx?i={0}&v={1}&t={2}&p={3}", -1, hfVIN.Value, 1, ImagesPeriod));
            mpeImages.Show();
        }
        // PreExpense Images
        protected void ibtncars1_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibtn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)ibtn.NamingContainer;
            HiddenField hfVIN = (HiddenField)row.FindControl("hfVIN");

            ifrmImages.Attributes.Add("src", String.Format("GenericImageGallery.aspx?i={0}&v={1}&t={2}&p={3}", -1, hfVIN.Value, 2, ImagesPeriod));
            mpeImages.Show();
        }
        // All Generic Images
        protected void ibtncars2_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibtn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)ibtn.NamingContainer;
            HiddenField hfVIN = (HiddenField)row.FindControl("hfVIN");

            ifrmImages.Attributes.Add("src", String.Format("GenericImageGallery.aspx?i={0}&v={1}&t={2}&p={3}", -1, hfVIN.Value, 3, ImagesPeriod));
            mpeImages.Show();
        }
        // Generic Images
        protected void ibtncars3_Click(object sender, ImageClickEventArgs e)
        {
            long ImageID = Convert.ToInt64(gvGenericImages.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
            ImageButton ibtn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)ibtn.NamingContainer;
            HiddenField hfVIN = (HiddenField)row.FindControl("hfVIN");

            ifrmImages.Attributes.Add("src", String.Format("GenericImageGallery.aspx?i={0}&v={1}&t={2}&p={3}", ImageID, "", 4, ImagesPeriod));
            mpeImages.Show();
        }
        #endregion

        #region[Format device ID]
        public string FormatDeviceID(object oDeviceID)
        {
            String DeviceID = Convert.ToString(oDeviceID);
            string strDeviceID = String.Empty;
            if (!string.IsNullOrEmpty(DeviceID))
            {
                if (DeviceID != "5" && DeviceID.Length > 5)
                    strDeviceID = DeviceID.Substring(0, 5);
                else
                    strDeviceID = String.Empty;
            }
            return strDeviceID;
        }
        #endregion

        #region[RowDataBound event]
        protected void gvGenericImages_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfVIN = (HiddenField)e.Row.FindControl("hfVIN");
                ImageButton ibtncars = (ImageButton)e.Row.FindControl("ibtncars");
                ImageButton ibtncars1 = (ImageButton)e.Row.FindControl("ibtncars1");
                Int32 PreInvImageCount = CommonBAL.GetPreInv_PreExpCount(hfVIN.Value, 1, ImagesPeriod);
                Int32 PreExpImageCount = CommonBAL.GetPreInv_PreExpCount(hfVIN.Value, 2, ImagesPeriod);

                if (PreInvImageCount == 0)
                {
                    ibtncars.ImageUrl = "~/Images/car-disabled.png";
                    ibtncars.Enabled = false;
                    ibtncars.ToolTip = "No PreInv Images";
                }

                if (PreExpImageCount == 0)
                {
                    ibtncars1.ImageUrl = "~/Images/car-disabled.png";
                    ibtncars1.Enabled = false;
                    ibtncars1.ToolTip = "No PreExp Images";
                }

            }
        }
        #endregion
    }
}