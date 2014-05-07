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
    public partial class User : System.Web.UI.Page
    {
        EmployeeListBAL objEmployeeListBAL = new EmployeeListBAL();
        MakeUserBAL objUserBAL = new MakeUserBAL();
        long Code = -1;
        String Mode = String.Empty;
        Int32 UserID = 0;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["MasterPage"])))
                    this.MasterPageFile = Convert.ToString(Session["MasterPage"]);
                else
                    Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(Request.Url.ToString()));
            }
            catch (Exception ex)
            {
                Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(Request.Url.ToString()));
            }
        }

        #region[ Page Load Event ]
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblGroupAssociation.Visible = false;
            CheckPermission();

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
            this.ddlEntityTypesh.DataSource = BAL.Common.GetEntityList(1);
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

        #region[Bind All Employee]
        void BindEmployee()
        {
            GrdEmployee.DataSource = objEmployeeListBAL.GetAllEmployee(
               this.txtInitialsh.Text.Trim()
               , this.txtCitysh.Text.Trim()
               , this.txtStatesh.Text.Trim()
               , this.txtZipsh.Text.Trim()
               , Convert.ToInt16(this.ddlEntityTypesh.SelectedValue)
               , Constant.OrgID);
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
                BindAssociatedGroups(this.Code); txtDisplayName.Enabled = false; txtUserName.Enabled = false;
                txtNotes.Enabled = false; ChkActive.Enabled = false;
                btnSubmit.Visible = false; btnUpdate.Visible = false; btnCancel.Text = "   Back  ";
                btnDelete.Visible = false;

                // Hide, Enable some controls
                this.lnkPopUp.Visible = false;
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
                this.lnkPopUp.Visible = false;
                this.txtFullName.Enabled = false;
                this.txtUserName.Enabled = false;
                this.txtPassword.Enabled = false;
                this.txtConpassword.Enabled = false;
                GetExistingUser(Convert.ToInt32(this.Code));
                this.pnlEmployeeList.Visible = false;
                btnSubmit.Visible = false; btnCancel.Text = "  Cancel   ";

            }
            else if (this.Mode == "INS")
            {
                btnUpdate.Visible = false; btnCancel.Text = "  Cancel   ";
                fsetAssGroup.Visible = false;
                btnDelete.Visible = false;
            }
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
                SecurityUser objSecurityUser = new SecurityUser();
                objSecurityUser.EntityID = Convert.ToInt64(this.hfEntityId.Value);
                objSecurityUser.EntityTypeID = Convert.ToInt32(this.hfEntityType.Value);
                string PassEncripted = EncryptMD5.Encrypt(txtPassword.Text.Trim());
                objSecurityUser.UserName = txtUserName.Text.Trim();
                objSecurityUser.UserPassword = PassEncripted;
                objSecurityUser.DisplayName = txtDisplayName.Text.Trim();
                objSecurityUser.UserNote = txtNotes.Text.Trim();
                objSecurityUser.AddedBy = Constant.UserId;
                objSecurityUser.IsActive = (this.ChkActive.Checked ? 1 : 0);
                objSecurityUser.OrgID = Constant.OrgID;
                Int32 result = objUserBAL.AssignLoginPassword(objSecurityUser);
                if (result == -1)
                {
                    this.lblError.Text = "User Already Exist";
                }
                else
                {
                    if (Request["ReturnUrl"] != null)
                        Response.Redirect(String.Format("User.aspx?Code={0}&Mode=EDIT&ReturnUrl={1}", Convert.ToString(result), Request["ReturnUrl"]));
                    else
                        Response.Redirect(String.Format("User.aspx?Code={0}&Mode=EDIT", Convert.ToString(result)));
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
            bool result = MakeUserBAL.SecurityUser_Update(this.txtDisplayName.Text.Trim()
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
            //    txtFullName.Text = objEmployeeListBAL.GetEmployeeFullName(EmployeeId);

            //}
        }
        #endregion

        #region[ Select an employee to create the ID/Password ]
        protected void imgbtnSelect_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtnSelect = (ImageButton)sender;
            GridViewRow grdrow = (GridViewRow)imgbtnSelect.NamingContainer;
            long EmployeeId = Convert.ToInt64(GrdEmployee.DataKeys[grdrow.RowIndex].Value.ToString());
            txtFullName.Text = (grdrow.Cells[1].Text.Trim()
                                 + " (" + grdrow.Cells[2].Text.Trim()
                                  + ", " + grdrow.Cells[3].Text.Trim()
                                  + ", " + grdrow.Cells[4].Text.Trim() + ")").Replace("&nbsp;", "");
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
            this.gvGroups.DataSource = MakeUserBAL.ActiveGroups(this.txtGroupNamegrp.Text.Trim(), this.txtGroupDescgrp.Text.Trim(), Constant.OrgID);
            this.gvGroups.DataBind();
        }
        #endregion

        #region[ Get Existing User Info. ]
        protected void GetExistingUser(Int32 userId)
        {
            DataTable dTab = BAL.MakeUserBAL.GetUserInfo(userId);
            if (dTab != null && dTab.Rows.Count > 0)
            {
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

        protected void gvGroups_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGroups.PageIndex = e.NewPageIndex;
            SearchGroups();
            mpeModelGroup.Show();
        }

        protected void gvGroups_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnSearchGroup_Click(object sender, EventArgs e)
        {
            SearchGroups();
            this.mpeModelGroup.Show();
        }

        protected void ibtnAddGroup_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtnSelect = (ImageButton)sender;
            GridViewRow grdrow = (GridViewRow)imgbtnSelect.NamingContainer;
            long GroupId = Convert.ToInt64(this.gvGroups.DataKeys[grdrow.RowIndex].Value.ToString());

            try
            {
                Int32 result = BAL.MakeUserBAL.AddUserGroup(this.Code, GroupId, Constant.UserId);
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
        #region[ Delete Associated group (mark IsActive - 0) ]
        protected void ImgbtnDelRight_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtnSelect = (ImageButton)sender;
            GridViewRow grdrow = (GridViewRow)imgbtnSelect.NamingContainer;
            long GroupId = Convert.ToInt64(this.GrdGroup.DataKeys[grdrow.RowIndex].Value.ToString());
            bool IsDeleted = MakeUserBAL.Activate_Deactivate_User_Associated_Group(GroupId, 0, this.Code);
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

        private void CheckPermission()
        {
            List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, "SECURITY");
            if (!dict.Contains("USER.ADD"))
                btnSubmit.Visible = false;

            if (!dict.Contains("USER.EDIT"))
                this.btnUpdate.Visible = false;

            if (!dict.Contains("USER.DELETE"))
                this.ChkActive.Enabled = false;

            if (!dict.Contains("GROUP.ADD"))
                this.btnAssociateGroup.Visible = false;

            if (!dict.Contains("GROUP.DELETE"))
            {
                this.GrdGroup.Columns[0].Visible = false;
                this.pnlGroups.Visible = false;
            }

            if (dict.Count == 0)
                Response.Redirect("Permission.aspx?MSG=SECURITY.USER.ADD OR SECURITY.USER.EDIT OR SECURITY.USER.VIEW");
        }

        protected void btnEditCancel_Click(object sender, EventArgs e)
        {
            modPopUp.Hide();
        }

        protected void btnCanelGroup_Click(object sender, EventArgs e)
        {
            mpeModelGroup.Hide();
        }

        protected void ddlEntityTypesh_SelectedIndexChanged(object sender, EventArgs e)
        {
            modPopUp.Show();
            BindEmployee();
        }

        #region[Delete user]
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            MakeUserBAL.SecurityUser_UpdateActiveStatus(this.Code, 2);
            lblError.Text = "User deleted successfully";
            btnCancel.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnAssociateGroup.Enabled = false;
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
            if (lst!= null)
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
            }
        }

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
               // Text=<%# String.Format("({0})",Eval("IPAddress").ToString().Remove((Eval("IPAddress").ToString()).LastIndexOf(","),1))
               if(!String.IsNullOrEmpty(HDIPA.Value))
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

        [WebMethod]
        public static void UpdateUserSetting(long UserID, String SettingKeyValue)
        {
            MakeUserBAL bal = new MakeUserBAL();
            bal.UpdateUserSettings(UserID, (SettingKeyValue == "1") ? "True" : "False");
        }
        [WebMethod]
        public static void UpdateAutoIPApproval(int Flag, Int32 IPUserID, Int32 ModifiedBy, Int32 IPType)
        {
            ViewGroupListBAL objViewGroupListBAL = new ViewGroupListBAL();
            objViewGroupListBAL.AutoIPRestriction(IPUserID, ModifiedBy, IPType, (Flag == 1) ? 1 : 0);

        }
    }
}