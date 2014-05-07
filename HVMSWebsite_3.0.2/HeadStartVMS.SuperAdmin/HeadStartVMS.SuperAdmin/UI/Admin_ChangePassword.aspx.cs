using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using METAOPTION.BAL;
using METAOPTION.Common;

namespace METAOPTION.UI
{
    public partial class Admin_ChangePassword : System.Web.UI.Page
    {
        public string max;
        Admin_OrganizationBAL ObjBAL = new Admin_OrganizationBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
                if (!String.IsNullOrEmpty(Request.QueryString["Code"]))// && !string.IsNullOrEmpty(Request.QueryString["UserName"])
                {

                    Int64 code = Convert.ToInt64(Request.QueryString["Code"]);
                    //string username = Request.QueryString["UserName"].ToString();
                    maker(code);
                    

                }
                else
                {

                    BindOrganization();
                    BindRole();
                }
                Session["SuperAdminID"] = Constant.UserId;
                txt_newpasswd.Attributes.Add("autocomplete", "off");
            }
        }
        private void maker(Int64 userId)//,string username
        {
            DataTable dTab = BAL.Admin_MakeUserBAL.GetUserInfo(userId);
            if (dTab != null && dTab.Rows.Count > 0)
            {
                ddl_role.DataSource = dTab;
                ddl_Organization.DataSource = dTab;
                ddl_Organization.DataTextField = "OrgDisplayName";
                ddl_Organization.DataValueField = "OrgID";
                ddl_role.DataValueField = "EntityTypeID";
                ddl_role.DataTextField = "ENTITY_TYPE";// dTab.Rows[0]["ENTITY_TYPE"].ToString();
                ddl_Organization.DataBind();
                ddl_role.DataBind();

                BindUser(Convert.ToInt32(ddl_role.SelectedValue), Convert.ToInt16(ddl_Organization.SelectedValue), userId,Convert.ToInt32(dTab.Rows[0]["IsActive"])); //, Convert.ToInt64(dTab.Rows[0]["SecurityUserID"])
               
                ddl_username.SelectedValue = Convert.ToString(dTab.Rows[0]["EntityID"]);
                ddl_username.Enabled = false;
                ddl_Organization.Enabled = false;
                ddl_role.Enabled = false;
            }
        }



        #region[Bind Organization and Role DropDownList]
        private void BindOrganization()
        {
            this.ddl_role.Items.Clear();
            this.ddl_username.Items.Clear();
            DataTable dt = new DataTable();
            dt = ObjBAL.ddl_GetOrganization();

            //DataView dv = new DataView(dt);
            //dv.Sort = "OrgName";
            //dt.Clear();
            //dt = dv.ToTable();

            this.ddl_Organization.DataSource = dt;
            this.ddl_Organization.DataValueField = dt.Columns[0].ToString();
            this.ddl_Organization.DataTextField = dt.Columns[1].ToString();
            this.ddl_Organization.DataBind();

            string a = Constant.UserId.ToString();
            string b = Constant.UserDisplayName;

            Int16? OrgID = ObjBAL.GetEntiyInformation(Convert.ToInt64(Constant.UserId));
            if (OrgID != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i][0].ToString()) == OrgID)//Convert.ToInt16(Session["OrgID"].ToString())
                    {
                        max = dt.Rows[i][1].ToString();
                        break;
                    }
                }

                this.ddl_Organization.SelectedValue = ddl_Organization.Items.FindByText(max).Value;
            }
            else
            {
                this.ddl_role.Items.Clear();
                this.ddl_username.Items.Clear();
            }

            //this.ddl_Organization.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        private void BindRole()
        {
            this.ddl_role.Items.Clear();
            DataTable EntityTable = new DataTable();
            EntityTable =  ObjBAL.GetEntityType();

            //DataView dv = new DataView(EntityTable);
            //EntityTable.Clear();
            //dv.Sort = "EntityType";
            //EntityTable = dv.ToTable();


            this.ddl_role.DataSource = EntityTable; //ObjBAL.GetRealEntityType_List();
            this.ddl_role.DataTextField = "EntityType";         //"EntityType1";
            this.ddl_role.DataValueField = "EntityTypeId";         //"EntityTypeId";
            this.ddl_role.DataBind();
            //   this.ddl_role.Items.Insert(0, new ListItem("ALL", "-1"));
            this.ddl_username.Items.Clear();
            //   this.ddl_username.Items.Insert(0, new ListItem("ALL", "-1"));
            BindUser(Convert.ToInt32(this.ddl_role.SelectedValue), Convert.ToInt16(ddl_Organization.SelectedValue), Convert.ToInt64(0), Convert.ToInt32(EntityTable.Rows[0]["IsActive"].ToString()));//New Modified Code

        }
        private void BindUser(int ROLEID, Int16 ORGID, Int32 IsActive)
        {
            DataTable dt = new DataTable();
            dt = ObjBAL.GetEmployee(ROLEID, ORGID, IsActive);
            DataView dv = new DataView(dt);
            dv.Sort = "UserNameDisplay ASC";

            this.ddl_username.DataSource = dv;
            this.ddl_username.DataValueField = dt.Columns["EmployeeID"].ToString();
            this.ddl_username.DataTextField = dt.Columns["UserNameDisplay"].ToString();
            this.ddl_username.DataBind();
            // this.ddl_username.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        private void BindUser(Int32 ROLEID, Int16 ORGID, Int64 SUID, Int32 IsActive)
        {
            DataTable dt = new DataTable();
            dt = ObjBAL.GetEmployee(ROLEID, ORGID, SUID, IsActive);
            DataView dv = new DataView(dt);
            dv.Sort = "UserNameDisplay ASC";

            this.ddl_username.DataSource = dv;
            this.ddl_username.DataValueField = dt.Columns["EmployeeID"].ToString();
            this.ddl_username.DataTextField = dt.Columns["UserNameDisplay"].ToString();
            this.ddl_username.DataBind();
            // this.ddl_username.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        #endregion


        #region[Organization,Role and UserName SelectIndexChange]
        protected void ddl_Organization_selectedindexchange(object sender, EventArgs e)
        {
            if (this.ddl_Organization.Items.Count > 0)
                if (Convert.ToInt32(this.ddl_Organization.SelectedValue) == -1)
                {
                    this.ddl_role.Items.Clear();
                    //  this.ddl_role.Items.Insert(0, new ListItem("ALL", "-1"));
                    this.ddl_username.Items.Clear();
                    //  this.ddl_username.Items.Insert(0, new ListItem("ALL", "-1"));
                }
                else
                {
                    BindRole();
                }
        }
        protected void ddl_role_selectedindexchange(object sender, EventArgs e)
        {
            if (this.ddl_role.Items.Count > 0)
                if (Convert.ToInt32(this.ddl_Organization.SelectedValue) == -1)
                {
                    this.ddl_username.Items.Clear();
                    // this.ddl_username.Items.Insert(0, new ListItem("ALL", "-1"));
                }
                else
                {
                    BindUser(Convert.ToInt32(this.ddl_role.SelectedValue), Convert.ToInt16(ddl_Organization.SelectedValue),Convert.ToInt32(1));
                }
        }
        protected void ddl_username_selectedindexchange(object sender, EventArgs e)
        {
        }
        #endregion


        #region[Update and Cancel Button]
        #region[Update Password]
        protected void btn_update_click(object sender, EventArgs e)
        {

            Int16 OrgID = Convert.ToInt16(ddl_Organization.SelectedValue);

            int EntityTYPE = 0;
            Int64 EmployeeID = 0;
            if ((!string.IsNullOrEmpty(Convert.ToString(ddl_role.SelectedValue))) && (!string.IsNullOrEmpty(Convert.ToString(ddl_username.SelectedValue))))
            {
                EntityTYPE = Convert.ToInt32(ddl_role.SelectedValue);
                EmployeeID = Convert.ToInt64(ddl_username.SelectedValue);

                Int64 modifiedby = Convert.ToInt64(Session["SuperAdminID"]);
                if (!string.IsNullOrEmpty(txt_newpasswd.Text.Trim()))
                {

                    if (txt_newpasswd.Text == txt_confirmpasswd.Text)
                    {
                        string NewPasswd = EncryptMD5.Encrypt(txt_newpasswd.Text.Trim());
                        int CheckValue = ObjBAL.UpdatePasswd(EmployeeID, NewPasswd, modifiedby);

                        if (CheckValue == EntityTYPE)
                        {
                            ShowMessage.Text = "<b>Password updated successfully</b>";
                            ShowMessage.ForeColor = System.Drawing.Color.Green;
                            txt_newpasswd.Text = string.Empty;
                            txt_confirmpasswd.Text = string.Empty;
                            lbl_confirmpassword.Text = "";
                            lbl_newpassword.Text = "";
                        }
                        else
                        {
                            ShowMessage.Text = "<b>Error..TRY AGAIN!!</b>";
                            ShowMessage.ForeColor = System.Drawing.Color.Red;
                            lbl_confirmpassword.Text = "";
                            lbl_newpassword.Text = "";
                            lbl_usererrormsg.Text = "";
                        }

                    }
                    else
                    {
                        lbl_confirmpassword.Text = "Password does not match";
                        lbl_newpassword.Text = "";
                        ShowMessage.Text = "";
                        lbl_usererrormsg.Text = "";
                    }
                }
                else
                {
                    lbl_newpassword.Text = "Enter new password";
                    ShowMessage.Text = "";
                    lbl_confirmpassword.Text = "";
                    lbl_usererrormsg.Text = "";
                }

            }
            else
            {
                lbl_usererrormsg.Text = "<b> *</b>";
                lbl_usererrormsg.ForeColor = System.Drawing.Color.Red;
                lbl_confirmpassword.Text = "";
                lbl_newpassword.Text = "";
            }


        }
        #endregion

        #region[Cancel Password]
        protected void btn_cancel_click(object sender, EventArgs e)
        {
            if (Request["ReturnUrl"] != null)
            {
                Response.Redirect(Request["ReturnUrl"]);
            }
            else
            {
                System.Web.HttpContext.Current.Response.Redirect("../UI/Admin_Home.aspx");
            }
            
        }
        #endregion
        #endregion
    }
}