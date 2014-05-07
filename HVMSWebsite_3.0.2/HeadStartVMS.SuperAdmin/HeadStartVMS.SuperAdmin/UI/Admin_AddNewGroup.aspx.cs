using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using METAOPTION.BAL;

namespace METAOPTION.UI
{
    public partial class Admin_AddNewGroup : System.Web.UI.Page
    {

        Admin_GroupBAL objBAL = new Admin_GroupBAL();
        long _groupId = -1;
        String Mode = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Mode"] != null)
                this.Mode = Convert.ToString(Request["Mode"]).ToUpper();
            else
                this.Mode = "VIEW";

            if (Request["Code"] != null)
                this._groupId = Convert.ToInt64(Request["Code"]);

            if (!IsPostBack)
            {
                BindOrganizationDDL();
                CheckOperation();
            }
        }

        #region [Check operation for this page]
        /// <summary>
        /// check which operation is going on to operate
        /// </summary>
        protected void CheckOperation()
        {
            if (this.Mode == "VIEW")
            {
                BindGroupDetails(); 
                BindGroupRights();
                txtGName.Enabled = false; 
                txtGDesc.Enabled = false;
                this.btnAddRight.Visible = false;
                this.gvAssociatedRights.Columns[0].Visible = false;
                btnSubmit.Visible = false; 
                btnUpdate.Visible = false; 
                //btnCancel.Text = "Back";
                ddlOrganization.Enabled = false;
            }
            if (this.Mode == "EDIT")
            {
                BindGroupDetails(); 
                BindGroupRights();
                btnSubmit.Visible = false; 
                //btnCancel.Text = "Cancel";
                this.btnAddRight.Visible = true;
                ddlOrganization.Enabled = false;
            }
            if (this.Mode == "INS")
            {
                btnUpdate.Visible = false; 
                //btnCancel.Text = "Cancel";
                fsetRights.Visible = false;
            }
        }
        #endregion

        #region [Add New Group]
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SecurityGroup objSecGroup = new SecurityGroup();
                objSecGroup.GroupName = txtGName.Text.Trim();
                objSecGroup.GroupDesc = txtGDesc.Text.Trim();
                objSecGroup.AddedBy = 1;// Constant.UserId;
                //Mark whether the group is general or for a specific organization
                //IsSystemDefault: true-General group/false-organization specific
                if (ddlOrganization.SelectedValue == "-1")
                    objSecGroup.IsSystemDefault = true;
                else
                    objSecGroup.IsSystemDefault = false;
                long result = objBAL.AddGroupDetails(objSecGroup, Convert.ToInt16(ddlOrganization.SelectedValue));
                if (result > 0)
                {
                    if (Request["ReturnUrl"] != null)
                        Response.Redirect(string.Format("Admin_AddNewGroup.aspx?Code={0}&Mode=Edit&ReturnUrl={1}", Convert.ToString(result), Request["ReturnUrl"]));
                    else
                        Response.Redirect(string.Format("Admin_AddNewGroup.aspx?Code={0}&Mode=Edit", Convert.ToString(result)));
                }
            }
        }
        #endregion

        #region [ Update Click ]
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateGroup_ByGroupId();
            if (Request["ReturnUrl"] != null)
                Response.Redirect(Request["ReturnUrl"]);
        }
        #endregion

        #region [ Add Rights ]
        protected void btnAddRight_Click(object sender, EventArgs e)
        {
            SearchRights();
            this.mpeOpenRights.Show();
        }
        #endregion

        #region [Bind Group Details]
        /// <summary>
        /// region bind group details
        /// by groupId
        /// </summary>
        protected void BindGroupDetails()
        {
            if (this._groupId > 0)
            {
                Dictionary<string, string> objDic = objBAL.GetGroupDetails_ByGroupId(this._groupId);
                txtGName.Text = objDic["GName"];
                txtGDesc.Text = objDic["GDesc"];
            }
        }
        #endregion

        #region [Update Group]
        /// <summary>
        /// update group by the group ID
        /// </summary>
        protected void UpdateGroup_ByGroupId()
        {
            if (Page.IsValid)
            {
                if (this._groupId > 0)
                {
                    SecurityGroup objSecGroup = new SecurityGroup();
                    objSecGroup.SecurityGroupId = this._groupId;
                    objSecGroup.GroupName = txtGName.Text.Trim();
                    objSecGroup.GroupDesc = txtGDesc.Text.Trim();
                    if (Session["EmpId"] != null)
                        objSecGroup.ModifiedBy = Constant.UserId;

                    int result = objBAL.UpdateGroup(objSecGroup);

                    if (result == 0)
                    {
                        if (Request["ReturnUrl"] != null)
                            Response.Redirect(string.Format("AddGroup.aspx?Code={0}&Mode=view&ReturnUrl={1}", Convert.ToString(this._groupId), Request["ReturnUrl"]));
                        else
                            Response.Redirect(string.Format("AddGroup.aspx?Code={0}&Mode=view", Convert.ToString(this._groupId)));
                    }
                }
            }
        }
        #endregion

        #region [Bind Group Rights]
        protected void BindGroupRights()
        {
            gvAssociatedRights.DataSource = objBAL.GetGroupWithRights(this._groupId);
            gvAssociatedRights.DataBind();
        }
        #endregion

        #region[ Delete Group ]
        protected void gvAssociatedRights_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRight")
            {
                SecurityGroupRight objSecGrpRight = new SecurityGroupRight();
                objSecGrpRight.SecurityGroupRightId = Convert.ToInt64(e.CommandArgument);
                int result = objBAL.DeleteRightFromGroup(objSecGrpRight);
                BindGroupRights();
            }
        }
        #endregion

        #region[ Add right by clicking OK Button ]
        protected void ibtnOk_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton img = (ImageButton)sender;
            GridViewRow gvRow = (GridViewRow)img.NamingContainer;
            long rightId = Convert.ToInt64(gvRights.DataKeys[gvRow.RowIndex].Value);

            long result = Admin_GroupBAL.AddRightInGroup(this._groupId, rightId);
            if (result > 0)
            {
                BindGroupRights();
                this.mpeOpenRights.Hide();
            }

        }
        #endregion

        #region[ Search Rights ]
        protected void SearchRights()
        {
            this.gvRights.DataSource = Admin_GroupBAL.GetRightsName(this.txtRightName.Text.Trim());
            this.gvRights.DataBind();
            this.mpeOpenRights.Show();
        }
        #endregion

        #region[ Paging of Grids ]
        protected void gvAssociatedRights_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAssociatedRights.PageIndex = e.NewPageIndex;
            BindGroupRights();
        }

        protected void gvRights_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvRights.PageIndex = e.NewPageIndex;
            SearchRights();
            this.mpeOpenRights.Show();
        }
        #endregion

        #region [ Cancel Click ]
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //if (Request["ReturnUrl"] != null)
            //    Response.Redirect(Request["ReturnUrl"]);
            if (Request["ReturnUrl"] != null)
            {
                System.Web.HttpContext.Current.Response.Redirect(Request["ReturnUrl"]);
            }
            else
            {
                System.Web.HttpContext.Current.Response.Redirect("Admin_Home.aspx");
            }
        }
        #endregion

        protected void btnSearchRight_Click(object sender, EventArgs e)
        {
            SearchRights();
        }

        #region[Organization DDL]
        protected void BindOrganizationDDL()
        {
            Admin_OrganizationBAL objBal = new Admin_OrganizationBAL();
            ddlOrganization.DataSource = objBal.GetOrganizationsList();
            ddlOrganization.DataTextField = "OrgName";
            ddlOrganization.DataValueField = "OrgID";
            ddlOrganization.DataBind();
            ddlOrganization.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        #endregion

    }
}