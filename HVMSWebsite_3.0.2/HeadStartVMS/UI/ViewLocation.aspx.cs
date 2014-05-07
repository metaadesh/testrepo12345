using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;


namespace METAOPTION.UI
{
    public partial class ViewLocation : System.Web.UI.Page
    {
        METAOPTION.LocationBAL bal = new METAOPTION.LocationBAL();
      
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
            {
               
                BindLocations();
                BindUsers();
                BindGrid();

                
            }
        }

        #region[Bind Grid]
        protected void BindGrid()
        {
            Int32 ParentBuyerID = -1;

            if (Convert.ToString(Session["BuyerAccessLevel"]) == "1")
                ParentBuyerID = -1;
            else
                ParentBuyerID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? -1 : Convert.ToInt32(Session["BuyerParent"]);

            String VIN = String.IsNullOrEmpty(txtVIN.Text.Trim()) ? String.Empty : txtVIN.Text.Trim();
                ObjectDataSource odsLocation = new ObjectDataSource();
                odsLocation.TypeName = "Metaoption.LocationBAL";
                odsLocation.EnablePaging = true;
                odsLocation.SelectMethod = "FetchVINLocationVer211";
                odsLocation.SelectCountMethod = "FetchVINLocationCountVer211";
                odsLocation.SelectParameters.Add("VIN", VIN);
                odsLocation.SelectParameters.Add("LocationID", ddlLocation.SelectedValue);
                odsLocation.SelectParameters.Add("AddedBy", ddlUsers.SelectedValue);
                odsLocation.SelectParameters.Add("EntityTypeID", DbType.Int32, Session["LoginEntityTypeID"].ToString());
                odsLocation.SelectParameters.Add("EntityID", DbType.Int32, Session["UserEntityID"].ToString());
                odsLocation.SelectParameters.Add("ParentEntityID", DbType.Int32, ParentBuyerID.ToString());
                odsLocation.SelectParameters.Add("StartRowIndex", DbType.Int32, gvLocation.PageIndex.ToString());
                odsLocation.SelectParameters.Add("MaximumRows", DbType.Int32, gvLocation.PageSize.ToString());
                odsLocation.SelectParameters.Add("OrgID", DbType.Int16, Constant.OrgID.ToString());
               gvLocation.DataSource = odsLocation;
               gvLocation.DataBind();
        }
        #endregion

        #region[Users]
        private void BindUsers()
        {
            Int32 ParentBuyerID = -1;
            if (Convert.ToString(Session["BuyerAccessLevel"]) != "1")                
                ParentBuyerID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? -1 : Convert.ToInt32(Session["BuyerParent"]);
            this.ddlUsers.DataSource = bal.GetAllLocationUsers_AddedBy(Convert.ToInt32(Session["empId"]), ParentBuyerID, Convert.ToInt32(Session["LoginEntityTypeID"]),Constant.OrgID);
            this.ddlUsers.DataTextField = "DisplayName";
            this.ddlUsers.DataValueField = "SecurityUserID";
            this.ddlUsers.DataBind();
            this.ddlUsers.Items.Insert(0, new ListItem("ALL", "-1"));
            if (Convert.ToString(Session["LoginEntityTypeID"]) != "5")
            {
                if (ddlUsers.Items.FindByValue(Convert.ToString(Session["empid"])) != null && ParentBuyerID == -1 && ddlUsers.Items.Count == 2)
                {
                    ddlUsers.SelectedValue = Convert.ToString(Session["empid"]);
                    ddlUsers.Enabled = false;
                }
                else if (ddlUsers.Items.FindByValue(Convert.ToString(Session["empid"])) != null)
                    ddlUsers.SelectedValue = Convert.ToString(Session["empid"]);
                else if (ddlUsers.Items.Count == 1)
                    ddlUsers.Enabled = false;
            }
           
        }
        #endregion

        #region[Locations]
        private void BindLocations()
        {
            Int32 ParentBuyerID = -1;
            if (Convert.ToString(Session["BuyerAccessLevel"]) != "1")              
                ParentBuyerID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? -1 : Convert.ToInt32(Session["BuyerParent"]);

            this.ddlLocation.DataSource = bal.GetAllLocations(Convert.ToInt32(Session["empId"]), ParentBuyerID, Convert.ToInt32(Session["LoginEntityTypeID"]),Constant.OrgID);         
            this.ddlLocation.DataTextField = "LocationCode";
            this.ddlLocation.DataValueField = "LocationID";
            this.ddlLocation.DataBind();
            this.ddlLocation.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        #endregion

        #region[Search button click event handler]
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvLocation.PageIndex = 0;
            BindGrid();
        }
        #endregion

        #region[Paging]
        protected void gvLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLocation.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        #endregion

    }
}