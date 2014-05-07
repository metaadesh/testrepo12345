using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using METAOPTION.BAL;

namespace METAOPTION.UI
{
    public partial class Admin_ViewAllGroups : System.Web.UI.Page
    {
        Admin_GroupBAL objBAL = new Admin_GroupBAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindOrganizationDDL();
                BindGroupList();
            }
        }

        #region [Bind Group List]
        protected void BindGroupList()
        {
            GrdGroup.DataSource = objBAL.GetAllGroups(Convert.ToInt16(ddlOrganization.SelectedValue));
            GrdGroup.DataBind();
        }
        #endregion

        #region[Submit button click event]
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin_AddNewGroup.aspx?mode=Ins&ReturnUrl=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
        }
        #endregion

        #region[Default setting button click event]
        protected void btnDefaultSetting_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
            if (ddlOrganization.SelectedValue == "-1")
            {
                lblMsg.Visible = true;
                mpeDefaultSetting.Hide();
            }
            else
            {
                mpeDefaultSetting.Show();
            }
        }
        #endregion

        #region[Delete Organization Associated with a group]
        protected void GrdGroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string grpId = GrdGroup.DataKeys[e.RowIndex].Value.ToString();
            objBAL.DeleteGroupFromOrg(Convert.ToInt64(grpId), Convert.ToInt16(ddlOrganization.SelectedValue));
            BindGroupList();
        }
        #endregion

        #region[Paging]
        protected void GrdGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdGroup.PageIndex = e.NewPageIndex;
            BindGroupList();
        }
        #endregion

        #region[Organization DDL]
        protected void BindOrganizationDDL()
        {
            Admin_OrganizationBAL objBal = new Admin_OrganizationBAL();
            ddlOrganization.DataSource = objBal.GetOrganizationsList();
            ddlOrganization.DataTextField = "OrgName";
            ddlOrganization.DataValueField = "OrgID";
            ddlOrganization.DataBind();
            ddlOrganization.Items.Insert(0, new ListItem("All", "-1"));
        }

        protected void ddlOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGroupList();
        }
        #endregion

        #region[Search Groups]
        protected void btnSearchGroup_Click(object sender, EventArgs e)
        {
            SearchGroups();
            this.mpeDefaultSetting.Show();
        }

        private void SearchGroups()
        {
            this.gvGroups.DataSource = objBAL.OrgGroups_NotAssociated(txtGroupNamegrp.Text.Trim(), txtGroupDescgrp.Text.Trim(), Convert.ToInt16(ddlOrganization.SelectedValue));
            this.gvGroups.DataBind();
        }

        protected void gvGroups_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGroups.PageIndex = e.NewPageIndex;
            SearchGroups();
            mpeDefaultSetting.Show();
        }
        #endregion

        #region[Associated Group to an Organization]
        protected void ibtnAddGroup_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtnSelect = (ImageButton)sender;
            GridViewRow grdrow = (GridViewRow)imgbtnSelect.NamingContainer;
            long GroupId = Convert.ToInt64(this.gvGroups.DataKeys[grdrow.RowIndex].Value.ToString());

            Int32 result = objBAL.AddGroupToOrg(GroupId, Convert.ToInt16(ddlOrganization.SelectedValue));
            if (result > 0)
            {
                BindGroupList();
                mpeDefaultSetting.Hide();
            }
            else
            {
                //TODO: show error in label
                mpeDefaultSetting.Show();
            }
        }
        #endregion

    }
}