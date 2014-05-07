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
using METAOPTION.BAL;
using System.Web.Services;


namespace METAOPTION.UI
{
    public partial class Admin_User : System.Web.UI.Page
    {
        #region Page Variables
        Admin_UserSettingBAL objUserSettingBAL = new Admin_UserSettingBAL();
        Admin_MakeUserBAL objUserBAL = new Admin_MakeUserBAL();
        long Code = -1;
        String Mode = String.Empty;
        Int32 UserID = 0;
        Boolean IsUserDeleted = false;
        #endregion

        #region[ Page Load Event ]
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblGroupAssociation.Visible = false;
            try
            {
                UserID = Convert.ToInt32(Session["empId"]);
                if (Request["Code"] != null)
                {
                    this.Code = Convert.ToInt64(Request["Code"]);
                    hfSecurityUserID.Value = Request["Code"];
                }

                if (Request["Mode"] != null)
                    this.Mode = Convert.ToString(Request["Mode"]).ToUpper();
            }
            catch { this.Code = -1; }

            if (!IsPostBack)
            {
                BindOrganizations();
                BindEntityType();
                CheckOperation();
                BindEmployee();
                BindUserSettings();
                BindIPPermission();
            }

        }
        #endregion

        #region[ Bind Entity Type ]

        protected void BindEntityType()
        {
            Admin_OrganizationBAL obj = new Admin_OrganizationBAL();
            this.ddlEntityTypesh.DataSource = obj.GetRealEntityType_List();
            this.ddlEntityTypesh.DataValueField = "EntityTypeId";
            this.ddlEntityTypesh.DataTextField = "EntityType1";
            this.ddlEntityTypesh.DataBind();
            this.ddlEntityTypesh.SelectedValue = "5";
            this.hfEntityType.Value = ddlEntityTypesh.SelectedValue;

            foreach (ListItem item in ddlEntityTypesh.Items)
            {
                // 1-Dealer/Customer, 2-Buyer, 5-Employee, 3-Vendor
                if (item.Value == "1" || item.Value == "2" || item.Value == "5" || item.Value == "3")
                    item.Enabled = true;
                else
                    item.Enabled = false;
            }
        }

        #endregion

        #region Bind Organization Drop down
        private void BindOrganizations()
        {
            BAL.Admin_OrganizationBAL obj = new BAL.Admin_OrganizationBAL();
            ddlOrganization.DataSource = obj.GetOrganizationsList();
            ddlOrganization.DataTextField = "OrgName";
            ddlOrganization.DataValueField = "OrgID";
            ddlOrganization.DataBind();
        }
        #endregion

        #region Organization Drop down Selected Index Changed Event
        protected void ddlOrganization_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindEmployee();
        }
        #endregion

        #region[Bind All Employee]
        void BindEmployee()
        {
            Int16 OrgID = Int16.Parse(ddlOrganization.SelectedValue);
            GrdEmployee.DataSource = objUserSettingBAL.GetAllEmployee(
               this.txtInitialsh.Text.Trim()
               , this.txtCitysh.Text.Trim()
               , this.txtStatesh.Text.Trim()
               , this.txtZipsh.Text.Trim()
               , Convert.ToInt16(this.ddlEntityTypesh.SelectedValue)
               , OrgID);
            GrdEmployee.DataBind();
        }
        #endregion

        #region [Check operation for this page]
        /// <summary>
        /// check which operation is going on to operate
        /// </summary>
        protected void CheckOperation()
        {
            if (this.Mode == "VIEW")
            {
                BindAssociatedGroups(this.Code);
                txtDisplayName.Enabled = false;
                txtUserName.Enabled = false;
                txtNotes.Enabled = false;
                ChkActive.Enabled = false;
                btnSubmit.Visible = false;
                btnUpdate.Visible = false;
                btnCancel.Text = "   Back  ";
                btnDelete.Visible = false;

                // Hide, Enable some controls
                this.ddlOrganization.Enabled = false;
                this.lnkPopUp.Visible = false;
                this.lnkAddNewUser.Visible = false;
                this.txtFullName.Enabled = false;
                this.txtUserName.Enabled = false;
                this.txtPassword.Enabled = false;
                this.txtConpassword.Enabled = false;
                this.txtDisplayName.Enabled = false;
                this.txtNotes.Enabled = false;
                this.ChkActive.Enabled = false;
                btnAssociateGroup.Visible = false;
                GetExistingUser(Convert.ToInt32(this.Code));
                this.pnlEmployeeList.Visible = false;
            }
            else if (this.Mode == "EDIT")
            {
                BindAssociatedGroups(this.Code);
                this.lnkAddNewUser.Visible = false;
                this.ddlOrganization.Enabled = false;
                this.lnkPopUp.Visible = false;
                this.txtFullName.Enabled = false;
                this.txtUserName.Enabled = false;
                this.txtPassword.Enabled = false;
                this.txtConpassword.Enabled = false;
                GetExistingUser(Convert.ToInt32(this.Code));
                this.pnlEmployeeList.Visible = false;
                btnSubmit.Visible = false;
                btnCancel.Text = "  Cancel   ";

            }
            else if (this.Mode == "INS")
            {
                btnUpdate.Visible = false;
                btnCancel.Text = "  Cancel   ";
                fsetAssGroup.Visible = false;
                btnDelete.Visible = false;
            }
        }
        #endregion

        #region popup button click event
        protected void lnkPopUp_Click(object sender, EventArgs e)
        {
            ddlEntityTypesh_SelectedIndexChanged(null, null);
        }
        #endregion

        #region [Bind Associated Groups]
        protected void BindAssociatedGroups(long userId)
        {
            GrdGroup.DataSource = objUserBAL.GetAssociatedGroups(userId);
            GrdGroup.DataBind();
        }
        #endregion

        #region[ Submit to Create User ]
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //Validate the User Name first befor creating new app user regardless of organization, prem
                Admin_UserSettingBAL objBAL = new Admin_UserSettingBAL();
                if (objBAL.CheckUserNameExistance(txtUserName.Text.Trim()))
                {
                    this.lblError.Text = " User Name Already Exist";
                    this.txtUserName.Focus();
                    return;
                }

                SecurityUser objSecurityUser = new SecurityUser();
                objSecurityUser.EntityID = Convert.ToInt64(this.hfEntityId.Value);
                objSecurityUser.EntityTypeID = Convert.ToInt32(this.hfEntityType.Value);

                string PassEncripted = METAOPTION.Common.EncryptMD5.Encrypt(txtPassword.Text.Trim());

                objSecurityUser.UserName = txtUserName.Text.Trim();

                objSecurityUser.UserPassword = PassEncripted;
                objSecurityUser.DisplayName = txtDisplayName.Text.Trim();
                objSecurityUser.UserNote = txtNotes.Text.Trim();
                objSecurityUser.AddedBy = Constant.UserId;
                objSecurityUser.IsActive = (this.ChkActive.Checked ? 1 : 0);
                objSecurityUser.OrgID = Int16.Parse(ddlOrganization.SelectedValue);
                Int32 result = objUserBAL.AssignLoginPassword(objSecurityUser);
                if (result == -1)
                {
                    this.lblError.Text = "User Already Exist";
                }
                else
                {
                    if (Request["ReturnUrl"] != null)
                        Response.Redirect(String.Format("Admin_User.aspx?Code={0}&Mode=EDIT&ReturnUrl={1}", Convert.ToString(result), Request["ReturnUrl"]));
                    else
                        Response.Redirect(String.Format("Admin_User.aspx?Code={0}&Mode=EDIT", Convert.ToString(result)));
                }
            }
            catch (Exception ex)
            {
                this.lblError.Text = ex.Message;
            }
        }
        #endregion

        #region[ Update Button Clicked ]
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            bool result = Admin_MakeUserBAL.SecurityUser_Update(this.txtDisplayName.Text.Trim()
                , this.txtNotes.Text.Trim()
                , Constant.UserId
                , this.ChkActive.Checked ? 1 : 0
                , this.Code);
            if (!result)
                this.lblError.Text = "Updated Successfully";

        }
        #endregion

        #region[ Cancel Button Clicked ]
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Request["ReturnUrl"] != null)
                Response.Redirect(Request["ReturnUrl"]);
        }
        #endregion

        #region[ Grid Events ]
        protected void GrdEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdEmployee.PageIndex = e.NewPageIndex;
            BindEmployee();
            modPopUp.Show();
        }

        protected void GrdEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "SelectEmp")
            //{
            //    long EmployeeId = Convert.ToInt64(e.CommandArgument);
            //    txtFullName.Text = objUserSettingBAL.GetEmployeeFullName(EmployeeId);

            //}
        }
        #endregion

        #region[ Select an employee to create the ID/Password ]
        protected void imgbtnSelect_Click(object sender, ImageClickEventArgs e)
        {
            //prem(27-Jan-2014)
            ImageButton imgbtnSelect = (ImageButton)sender;
            GridViewRow grdrow = (GridViewRow)imgbtnSelect.NamingContainer;
            long EmployeeId = Convert.ToInt64(GrdEmployee.DataKeys[grdrow.RowIndex].Value.ToString());
            String fullName = "(" + grdrow.Cells[2].Text.Trim().Replace("&nbsp;", "");
            fullName += grdrow.Cells[3].Text.Trim().Replace("&nbsp;", "") != "" ? ", " + grdrow.Cells[3].Text.Trim() : "";
            fullName += grdrow.Cells[4].Text.Trim().Replace("&nbsp;", "") != "" ? ", " + grdrow.Cells[4].Text.Trim() : "";
            fullName += ")";
            fullName = fullName.Replace("()", "");
            //txtFullName.Text = (grdrow.Cells[1].Text.Trim()
            //                     + " (" + grdrow.Cells[2].Text.Trim()
            //                      + ", " + grdrow.Cells[3].Text.Trim()
            //                      + ", " + grdrow.Cells[4].Text.Trim() + ")").Replace("&nbsp;", "");
            txtFullName.Text = grdrow.Cells[1].Text.Trim().Replace("&nbsp;", "") + " " + fullName;
            hfEntityType.Value = ddlEntityTypesh.SelectedValue;
            this.hfEntityId.Value = EmployeeId.ToString();
            modPopUp.Hide();
        }
        #endregion

        #region [  Search Section Entity ]
        protected void btnSearchsh_Click(object sender, EventArgs e)
        {
            BindEmployee();

            // Save value of Entity Type Id so save latter.
            this.hfEntityType.Value = ddlEntityTypesh.SelectedValue;

            this.modPopUp.Show();
        }
        #endregion

        #region[ Search Groups ]
        private void SearchGroups()
        {
            Int16 OrgID = Int16.Parse(ddlOrganization.SelectedValue);
            this.gvGroups.DataSource = Admin_MakeUserBAL.ActiveGroups(this.txtGroupNamegrp.Text.Trim(), this.txtGroupDescgrp.Text.Trim(), OrgID);
            this.gvGroups.DataBind();
        }
        #endregion

        #region[ Get Existing User Info. ]
        protected void GetExistingUser(Int32 userId)
        {
            DataTable dTab = BAL.Admin_MakeUserBAL.GetUserInfo(userId);
            if (dTab != null && dTab.Rows.Count > 0)
            {
                this.ddlOrganization.SelectedValue = dTab.Rows[0]["OrgID"].ToString();
                this.txtUserName.Text = Convert.ToString(dTab.Rows[0]["UserName"]);
                this.txtDisplayName.Text = Convert.ToString(dTab.Rows[0]["DisplayName"]);
                this.txtNotes.Text = Convert.ToString(dTab.Rows[0]["UserNote"]);
                this.ChkActive.Checked = Convert.ToBoolean(dTab.Rows[0]["IsActive"]);
                trFullName.Visible = false;
                trPassword.Visible = false;
                trCofirmPassword.Visible = false;
            }
        }
        #endregion

        #region Grid view page Index Changing + Row command Events
        protected void gvGroups_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGroups.PageIndex = e.NewPageIndex;
            SearchGroups();
            this.mpeModelGroup.Show();
        }

        protected void gvGroups_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        #endregion

        #region Search Button Click Event
        protected void btnSearchGroup_Click(object sender, EventArgs e)
        {
            SearchGroups();
            this.mpeModelGroup.Show();
        }
        #endregion

        #region Add Button click Event
        protected void ibtnAddGroup_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtnSelect = (ImageButton)sender;
            GridViewRow grdrow = (GridViewRow)imgbtnSelect.NamingContainer;
            long GroupId = Convert.ToInt64(this.gvGroups.DataKeys[grdrow.RowIndex].Value.ToString());

            try
            {
                Int32 result = BAL.Admin_MakeUserBAL.AddUserGroup(this.Code, GroupId, Constant.UserId);
                if (result == -1)
                {
                    this.lblGroupAssociation.Text = "All ready associated with this user!";
                    this.lblGroupAssociation.Visible = true;
                }
                else if (result == 0)
                {
                    this.lblGroupAssociation.Text = "Some SQL Server Error!";
                    this.lblGroupAssociation.Visible = true;
                }
                else if (result > 0)
                {
                    this.lblGroupAssociation.Text = "Successfully associated with ID: " + result.ToString();
                    this.lblGroupAssociation.Visible = true;
                    this.mpeModelGroup.Hide();

                    // Rebind the grid to refress association
                    BindAssociatedGroups(this.Code);
                }
            }
            catch { this.mpeModelGroup.Hide(); }
        }
        #endregion

        #region[ Delete Associated group (mark IsActive - 0) ]
        protected void ImgbtnDelRight_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtnSelect = (ImageButton)sender;
            GridViewRow grdrow = (GridViewRow)imgbtnSelect.NamingContainer;
            long GroupId = Convert.ToInt64(this.GrdGroup.DataKeys[grdrow.RowIndex].Value.ToString());
            bool IsDeleted = Admin_MakeUserBAL.Activate_Deactivate_User_Associated_Group(GroupId, 0, this.Code);
            if (IsDeleted)
                // Rebind the grid to refress association
                BindAssociatedGroups(this.Code);
            else
            {
                this.lblGroupAssociation.Text = "Some SQL Server Error!";
                this.lblGroupAssociation.Visible = true;
            }
        }
        #endregion

        #region Cancel Button Click Event
        protected void btnEditCancel_Click(object sender, EventArgs e)
        {
            modPopUp.Hide();
        }
        #endregion

        #region Pop up window cross button click event
        protected void btnCanelGroup_Click(object sender, EventArgs e)
        {
            mpeModelGroup.Hide();
        }
        #endregion

        #region Pop up window Entity type Selected Index Changed Event
        protected void ddlEntityTypesh_SelectedIndexChanged(object sender, EventArgs e)
        {
            modPopUp.Show();
            BindEmployee();
        }
        #endregion

        #region Add New user Button click Event
        protected void lnkAddNewUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin_ManageEntities.aspx" + "?ReturnUrl=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
        }
        #endregion

        #region[Delete user]
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Admin_MakeUserBAL.SecurityUser_UpdateActiveStatus(this.Code, 2);
            lblError.Text = "User deleted successfully";
            btnCancel.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnAssociateGroup.Enabled = false;
            txtDisplayName.Enabled = false;
            txtNotes.Enabled = false;
            ChkActive.Enabled = false;
            IsUserDeleted = true;

            BindAssociatedGroups(this.Code);
            BindUserSettings();
        }
        #endregion

        #region[Bind User Settings Grid]
        protected void BindUserSettings()
        {
            DataTable dt = objUserBAL.UserSettings(Code);
            if (dt.Rows.Count > 0)
            {
                gvUserSettings.DataSource = dt;
                gvUserSettings.DataBind();
            }
            else
            {
                DataTable dtbl = new DataTable();
                dtbl.Columns.Add("SettingID");
                dtbl.Columns.Add("SettingkeyValue");
                dtbl.Columns.Add("SettingKey");


                DataRow dr = dtbl.NewRow();
                dr["SettingID"] = "1";
                dr["SettingkeyValue"] = "True";
                dr["SettingKey"] = "MobileExpenseManualSync";

                dtbl.Rows.Add(dr);

                gvUserSettings.DataSource = dtbl;
                gvUserSettings.DataBind();
            }
        }
        #endregion

        #region [Bind IPPermission]
        protected void BindIPPermission()
        {
            List<IPUser_PermissionSelectResult> lst = objUserBAL.BindIPPermission(Convert.ToInt32(Code));
            if (lst != null)
            {
                gvIpPermission.DataSource = lst;
                gvIpPermission.DataBind();
            }
            else
            {
                DataTable dtbl = new DataTable();
                dtbl.Columns.Add("IPAddress");
                dtbl.Columns.Add("IPType");
                dtbl.Columns.Add("IPRestriction");


                DataRow dr = dtbl.NewRow();
                dr["IPAddress"] = "";
                dr["IPType"] = "1";
                dr["IPRestriction"] = "1";

                dtbl.Rows.Add(dr);
                gvIpPermission.DataSource = dtbl;
                gvIpPermission.DataBind();

            }

        }
        #endregion

        #region User Setting Grid View Row Data bound Event
        protected void gvUserSettings_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlImage imgOn = (HtmlImage)e.Row.FindControl("imgOn");
                HtmlImage imgOff = (HtmlImage)e.Row.FindControl("imgOff");
                HiddenField hfSettingKeyValue = (HiddenField)e.Row.FindControl("hfSettingKeyValue");

                if (hfSettingKeyValue.Value == "True")
                {
                    imgOff.Attributes.Add("style", "display:none");
                    imgOn.Attributes.Add("style", "display:block");
                }
                else
                {
                    imgOff.Attributes.Add("style", "display:block");
                    imgOn.Attributes.Add("style", "display:none");
                }
                if (IsUserDeleted)
                {
                    imgOff.Disabled = true;
                    imgOn.Disabled = true;
                }
            }
        }
        #endregion

        #region IP Permission Grid view Row Data Bound Event
        protected void gvIpPermission_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField IPRestriction = (HiddenField)e.Row.FindControl("hdIPRestriction");
                HiddenField IPType = (HiddenField)e.Row.FindControl("hdnIPType");
                HiddenField HDIPA = (HiddenField)e.Row.FindControl("hdnIPAddress");
                HtmlImage imgOn = (HtmlImage)e.Row.FindControl("img1");
                HtmlImage imgOff = (HtmlImage)e.Row.FindControl("img2");
                Label lblIPAdd = (Label)e.Row.FindControl("lblIPAddress");
                if (!String.IsNullOrEmpty(HDIPA.Value))
                    lblIPAdd.Text = "(" + HDIPA.Value.Remove(HDIPA.Value.LastIndexOf(","), 1) + ")";
                if (IPType.Value == "1")
                {

                    if (Convert.ToString(IPRestriction.Value.ToLower()) == "0")
                    {
                        imgOff.Attributes.Add("style", "display:block");
                        imgOn.Attributes.Add("style", "display:none");
                    }
                    else
                    {
                        imgOff.Attributes.Add("style", "display:none");
                        imgOn.Attributes.Add("style", "display:block");
                    }
                }
                else
                {
                    if (Convert.ToString(IPRestriction.Value.ToLower()) == "0")
                    {
                        imgOff.Attributes.Add("style", "display:block");
                        imgOn.Attributes.Add("style", "display:none");
                    }
                    else
                    {
                        imgOff.Attributes.Add("style", "display:none");
                        imgOn.Attributes.Add("style", "display:block");
                    }
                }
            }
        }
        #endregion

        #region Web Method to Update User Setting
        [WebMethod]
        public static void UpdateUserSetting(long UserID, String SettingKeyValue)
        {
            Admin_MakeUserBAL bal = new Admin_MakeUserBAL();
            bal.UpdateUserSettings(UserID, (SettingKeyValue == "1") ? "True" : "False");
        }
        #endregion

        #region Web Method to Apply auto IP Restriction
        [WebMethod]
        public static void UpdateAutoIPApproval(int Flag, Int32 IPUserID, Int32 ModifiedBy, Int32 IPType)
        {
            Admin_MakeUserBAL bal = new Admin_MakeUserBAL();
            bal.AutoIPRestriction(IPUserID, ModifiedBy, IPType, (Flag == 1) ? 1 : 0);
        }
        #endregion

        #region Group Grid View Row Data Bound Event
        protected void GrdGroup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (IsUserDeleted)
                {
                    GridViewRow row = e.Row;
                    ImageButton img = (ImageButton)row.FindControl("ImgbtnDelRight");
                    if (img != null)
                    {
                        img.Enabled = false;
                    }

                }
            }
        }
        #endregion
    }
}