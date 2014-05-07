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
using System.Text;

namespace METAOPTION.UI
{
    public partial class Admin_ManageSystem : System.Web.UI.Page
    {
        Admin_UserSettingBAL objUserSettingBAL = new Admin_UserSettingBAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindOrganizations();
                SetDefaultControlValues();
                BindSystem();
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

        #region [Bind All System]
        protected void BindSystem()
        {
            Int16 OrgID = Int16.Parse(ddlOrganization.SelectedValue);
            Admin_SystemBAL objBAL = new Admin_SystemBAL();
            GrdSystem.DataSource = objBAL.SystemSearch(OrgID, txtSystemName.Text.Trim(), Int32.Parse(ddlActiveStatus.SelectedValue));
            GrdSystem.DataBind();
        }
        #endregion


        private void SetDefaultControlValues()
        {
            setValue(ddlOrganization, "org");
            setValue(ddlActiveStatus, "status");
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

        protected void btnAddNewSystem_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin_AddSystem.aspx?Mode=Ins&ReturnUrl=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
        }

        #region[Grid row data bound event]
        protected void GrdSystem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hSystemStatus = (HiddenField)e.Row.FindControl("hSystemActiveStatus");
                ImageButton ibtnSystemStatus = (ImageButton)e.Row.FindControl("ibtnSystemStatus");
                if (hSystemStatus.Value == "0")
                {
                    ibtnSystemStatus.ImageUrl = "~/Images/Inactive_grey.png";
                    ibtnSystemStatus.ToolTip = "Click to activate";
                    e.Row.Cells[2].Text = "False";
                }
                else
                {
                    ibtnSystemStatus.ImageUrl = "~/Images/H_active.png";
                    ibtnSystemStatus.ToolTip = "Click to deactivate";
                    e.Row.Cells[2].Text = "True";
                }
            }
        }
        #endregion

        #region[Change System Active Status]
        protected void ibtnSystemStatus_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtnSelect = (ImageButton)sender;
            GridViewRow grdrow = (GridViewRow)imgbtnSelect.NamingContainer;
            HiddenField hSystemStatus = (HiddenField)grdrow.FindControl("hSystemActiveStatus");
            Int32 SystemID = Convert.ToInt32(GrdSystem.DataKeys[grdrow.RowIndex].Value.ToString());

            Admin_SystemBAL sysBAL = new Admin_SystemBAL();

            if (hSystemStatus.Value == "1")
                sysBAL.UpdateSystemActiveStatus(SystemID, false);
            else if (hSystemStatus.Value == "0")
                sysBAL.UpdateSystemActiveStatus(SystemID, true);

            BindSystem();
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindSystem();
        }
    }
}
