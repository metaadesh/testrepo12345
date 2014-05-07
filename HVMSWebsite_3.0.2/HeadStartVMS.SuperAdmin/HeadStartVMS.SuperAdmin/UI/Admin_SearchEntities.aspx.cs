using System;
using System.Collections;
using System.Collections.Generic;
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
    public partial class Admin_SearchEntities : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Page.IsPostBack || IsCallback))
            {
                BindOrganizations();
                BindEntityTypes();
                FillControls();
                SetDefaultControlValues();

                BindGrid();

            }
        }

        private void BindOrganizations()
        {
            BAL.Admin_OrganizationBAL obj = new BAL.Admin_OrganizationBAL();
            ddlOrganization.DataSource = obj.GetOrganizationsList();
            ddlOrganization.DataTextField = "OrgName";
            ddlOrganization.DataValueField = "OrgID";
            ddlOrganization.DataBind();
            ddlOrganization.Items.Insert(0, new ListItem("All", "-1"));
        }

        private void BindEntityTypes()
        {
            BAL.Admin_OrganizationBAL obj = new BAL.Admin_OrganizationBAL();
            ddlEntityType.DataSource = obj.GetRealEntityType_List();
            ddlEntityType.DataTextField = "EntityType1";
            ddlEntityType.DataValueField = "EntityTypeID";
            ddlEntityType.DataBind();
            ddlEntityType.Items.Insert(0, new ListItem("All", "-1"));
        }

        private void SetDefaultControlValues()
        {
            setValue(ddlOrganization, "org");
            setValue(ddlEntityType, "type");
            setValue(ddlStatus, "status");
        }

        private void setValue(DropDownList ddl, String QueryStringKey)
        {
            if (Request.QueryString[QueryStringKey] == null && string.IsNullOrEmpty(Request.QueryString[QueryStringKey]) == true)
            {
                ddl.SelectedValue = "-1";
            }
            else
            {
                if (ddl.Items.FindByValue(Request.QueryString[QueryStringKey]) != null)
                {
                    ddl.SelectedValue = Request.QueryString[QueryStringKey];
                }
                else
                {
                    ddl.SelectedValue = "-1";
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            Int16 OrgID = Int16.Parse(ddlOrganization.SelectedValue);
            Int32 EntityTypeID = Int32.Parse(ddlEntityType.SelectedValue);

            Admin_ManageEntitiesBAL objBAL = new Admin_ManageEntitiesBAL();
            DataTable dt = new DataTable();
            dt = objBAL.EntitySearch(OrgID, EntityTypeID, txtName.Text.Trim(), txtCity.Text.Trim(), Int32.Parse(ddlCountry.SelectedValue), Int32.Parse(ddlState.SelectedValue), txtZip.Text.Trim(), Int32.Parse(ddlStatus.SelectedValue));

            gvEntities.DataSource = dt;
            gvEntities.DataBind();

            //gvEntities.DataSource = BAL.Admin_BuyerBAL.GetBuyerList(
            //   this.txtName.Text.Trim()
            //   , Convert.ToInt32(-1)
            //   , Convert.ToInt32(-1)
            //   , this.txtCity.Text.Trim()
            //   , Convert.ToInt32(this.ddlCountry.SelectedValue)
            //   , Convert.ToInt32(this.ddlState.SelectedValue)
            //   , this.txtZip.Text.Trim()
            //   , Convert.ToInt32(this.ddlStatus.SelectedValue)
            //   , OrgID);
            //gvEntities.DataBind();
            //          var dt = ConvertToDataTable(
            //  BuildList(100)
            //);

            //gvViewVendor.DataSource = BAL.Admin_ManageEntitiesBAL.GetVendorList(
            // this.txtName.Text.Trim()
            // , Convert.ToInt32(this.ddlCategory.SelectedValue)
            // , Convert.ToInt32(ddlType.SelectedValue)
            // , this.txtCity.Text.Trim()
            // , Convert.ToInt32(this.ddlCountry.SelectedValue)
            // , Convert.ToInt32(this.ddlState.SelectedValue)
            // , this.txtZip.Text.Trim()
            // , Convert.ToInt32(this.ddlStatus.SelectedValue)
            // , Constant.OrgID);

        }

        DataTable ConvertToDataTable<TSource>(IEnumerable<TSource> source)
        {
            var props = typeof(TSource).GetProperties();

            var dt = new DataTable();
            dt.Columns.AddRange(
              props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray()
            );

            source.ToList().ForEach(
              i => dt.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray())
            );

            return dt;
        }

        protected void FillControls()
        {
            //Fill Country Drop Down
            BAL.Admin_Common bal = new BAL.Admin_Common();
            ddlCountry.DataSource = bal.GetCountryList();
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryId";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("All", "-1"));

            // Fill State Drop Down   
            BindState();

        }

        protected void BindState()
        {
            // Fill State Drop Down           
            ddlState.DataSource = BAL.Admin_Common.GetStateList(Convert.ToInt32(ddlCountry.SelectedValue));
            ddlState.DataTextField = "State";
            ddlState.DataValueField = "StateId";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("All", "-1"));
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindState();
        }

        protected void gvViewBuyer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEntities.PageIndex = e.NewPageIndex;
            btnSearch_Click(sender, e);
        }

        #region[Change Entity Active Status]
        protected void ibtnEntityStatus_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtnSelect = (ImageButton)sender;
            GridViewRow grdrow = (GridViewRow)imgbtnSelect.NamingContainer;
            HiddenField hOrgID = (HiddenField)grdrow.FindControl("hOrgID");
            HiddenField hEntityTypeID = (HiddenField)grdrow.FindControl("hEntityTypeID");
            HiddenField hEntityStatus = (HiddenField)grdrow.FindControl("hEntitiesActiveStatus");
            Int64 EntityID = Convert.ToInt64(gvEntities.DataKeys[grdrow.RowIndex].Value.ToString());

            Admin_ManageEntitiesBAL BAL = new Admin_ManageEntitiesBAL();

            if (hEntityStatus.Value == "1" || hEntityStatus.Value == "2")
            {
                BAL.UpdateEntityStatus(Int16.Parse(hOrgID.Value), Int32.Parse(hEntityTypeID.Value), EntityID, 0);
            }
            else if (hEntityStatus.Value == "0")
            {
                BAL.UpdateEntityStatus(Int16.Parse(hOrgID.Value), Int32.Parse(hEntityTypeID.Value), EntityID, 1);
            }
            BindGrid();
        }
        #endregion

        protected void gvEntities_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hEntityStatus = (HiddenField)e.Row.FindControl("hEntitiesActiveStatus");
                ImageButton ibtnEntityStatus = (ImageButton)e.Row.FindControl("ibtnEntitiesStatus");
                if (hEntityStatus.Value == "0")
                {
                    ibtnEntityStatus.ImageUrl = "~/Images/DeleteButton.png";
                    ibtnEntityStatus.ToolTip = "Click to activate";
                }
                else
                {
                    ibtnEntityStatus.ImageUrl = "~/Images/H_active.png";
                    ibtnEntityStatus.ToolTip = "Click to deactivate";
                }
            }
        }

        protected void lnk_newentity_click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Response.Redirect("Admin_ManageEntities.aspx?ReturnUrl="+HttpUtility.UrlEncode(Request.Url.AbsoluteUri));

        }
    }
}