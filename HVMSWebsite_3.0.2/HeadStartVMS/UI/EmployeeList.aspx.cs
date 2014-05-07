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
    public partial class UserList : System.Web.UI.Page
    {

        EmployeeListBAL objEmployeeListBAL = new EmployeeListBAL();

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
            CheckPermission();
            if (!IsPostBack)
            {
                BindEntityTypes();
                //BindEmployee();
                BindUser();
            }

        }

        #region [Bind All User]
        protected void BindUser()
        {
            String Sort = "UserName ASC";
            //GrdUser.DataSource = objEmployeeListBAL.GetAllUser();
            GrdUser.DataSource = objEmployeeListBAL.GetAllUserInfo(txtUserName.Text, txtDisplayName.Text, Convert.ToInt32(ddlEntityType.SelectedValue), Sort, Convert.ToInt32(ddlActiveStatus.SelectedValue), Constant.OrgID);
            GrdUser.DataBind();
        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect("User.aspx?Mode=Ins&ReturnUrl=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
        }

        private void CheckPermission()
        {
            List<String> rights = CommonBAL.GetPagePermission(Constant.UserId, "SECURITY");
            if (rights.Count < 1)
                Response.Redirect("Permission.aspx?MSG=SECURITY.USER.ADD OR SECURITY.USER.EDIT OR SECURITY.USER.VIEW");

            if (!rights.Contains("USER.ADD"))
                this.btnSubmit.Visible = false;

            if (!rights.Contains("USER.EDIT"))
                this.GrdUser.Columns[0].Visible = false;

        }

        protected void BindEntityTypes()
        {
            ddlEntityType.DataSource = objEmployeeListBAL.GetAllEntityType();
            ddlEntityType.DataTextField = "EntityType1";
            ddlEntityType.DataValueField = "EntityTypeID";
            ddlEntityType.DataBind();
            ddlEntityType.Items.Insert(0, new ListItem("ALL", "-1"));
        }

        protected void ddlEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindUser();
        }

        #region[Sorting]
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] != null)
                    return (SortDirection)ViewState["sortDirection"];
                else
                    return SortDirection.Ascending;
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected void GrdUser_OnSorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, ASCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, DESCENDING);
            }

        }

        private void SortGridView(string sortExpression, string direction)
        {
            String sort = "";
            if (sortExpression != null)
                sort = sortExpression + direction;

            GrdUser.DataSource = objEmployeeListBAL.GetAllUserInfo(txtUserName.Text, txtDisplayName.Text, Convert.ToInt32(ddlEntityType.SelectedValue), sort, Convert.ToInt32(ddlActiveStatus.SelectedValue), Constant.OrgID);
            GrdUser.DataBind();
        }

        #endregion

        #region[Grid row data bound event]
        protected void GrdUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfUserStatus = (HiddenField)e.Row.FindControl("hfUserStatus");
                ImageButton ibtnUserStatus = (ImageButton)e.Row.FindControl("ibtnUserStatus");
                if (hfUserStatus.Value == "0")
                {
                    ibtnUserStatus.ImageUrl = "~/Images/DeleteButton.png";
                    ibtnUserStatus.ToolTip = "Click to activate";
                }
                else
                {
                    ibtnUserStatus.ImageUrl = "~/Images/H_active.png";
                    ibtnUserStatus.ToolTip = "Click to deactivate";
                }
            }
        }
        #endregion

        protected void ddlActiveStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindUser();
        }

        #region[ Select an employee to create the ID/Password ]
        protected void ibtnUserStatus_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtnSelect = (ImageButton)sender;
            GridViewRow grdrow = (GridViewRow)imgbtnSelect.NamingContainer;
            HiddenField hfUserStatus = (HiddenField)grdrow.FindControl("hfUserStatus");
            long UserID = Convert.ToInt64(GrdUser.DataKeys[grdrow.RowIndex].Value.ToString());

            if (hfUserStatus.Value == "1")
                MakeUserBAL.SecurityUser_UpdateActiveStatus(UserID, 0);
            else if (hfUserStatus.Value == "0")
                MakeUserBAL.SecurityUser_UpdateActiveStatus(UserID, 1);

            BindUser();
        }
        #endregion

        protected void btnSendPass_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtEmeil = new DataTable();
                DataTable dt = new DataTable();
                dt.Columns.Add("EntityID");
                dt.Columns.Add("EntityTypeId");
                dt.Columns.Add("ContactId");

                foreach (GridViewRow grvrow in GrdUser.Rows)
                {
                    string typeId = string.Empty;
                    CheckBox chkBx = (CheckBox)grvrow.FindControl("chkSelect");
                    HiddenField EntityTypeID = (HiddenField)grvrow.FindControl("EntityTypeID");
                    HiddenField hdnContactId = (HiddenField)grvrow.FindControl("hdnContactId");
                    HiddenField hdnEntityId = (HiddenField)grvrow.FindControl("hdnEntityId");
                    if (chkBx != null && chkBx.Checked)
                    {
                        String EntityID = Convert.ToString(GrdUser.DataKeys[grvrow.RowIndex].Value);
                        DataRow row = dt.NewRow();
                        row["EntityID"] = hdnEntityId.Value;
                        row["EntityTypeId"] = EntityTypeID.Value;
                        row["ContactId"] = hdnContactId.Value;
                        dt.Rows.Add(row);
                    }
                }
                if (dt.Rows.Count > 0)
                    dtEmeil = objEmployeeListBAL.UserEmail(dt);
                if (dtEmeil.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEmeil.Rows.Count; i++)
                    {
                        StringBuilder strBody = new StringBuilder();
                        string strpwd = string.Empty;
                        if (!string.IsNullOrEmpty(Convert.ToString(dtEmeil.Rows[i]["UserPassword"])))
                            strpwd = EncryptMD5.Decrypt(Convert.ToString(dtEmeil.Rows[i]["UserPassword"]));
                        strBody.Append("<div style='height: 300px; width: 700px;'>");
                        strBody.Append("<div style='width: 97%; float: left; margin-top: 20px; padding: 10px'>");
                        strBody.Append("<div style='width: 100%; float: left; margin-top: 0px'>");
                        strBody.Append("Dear " + Convert.ToString(dtEmeil.Rows[i]["DisplayName"]) + ", </div>");
                        strBody.Append("<div style='width: 100%; float: left; margin-top: 20px'>");
                        strBody.Append("Your current HVMS Username is: " + Convert.ToString(dtEmeil.Rows[i]["UserName"]) + " </div>");
                        strBody.Append("<div style='width: 100%; float: left; margin-top: 20px'>");
                        strBody.Append("Your current HVMS Password is: " + strpwd + " </div>");
                        strBody.Append("<div style='width: 100%; float: left; margin-top: 20px'>");
                        strBody.Append("Thanks, </div>");
                        strBody.Append("<div style='width: 100%; float: left; margin-top: 20px'>");
                        strBody.Append("Admin </div>");
                        strBody.Append("<div style='width: 100%; float: left; margin-top: 5px'>");
                        strBody.Append("HeadStartVMS.com");
                        strBody.Append("</div>");
                        strBody.Append("</div>");
                        strBody.Append("</div>");
                        strBody.Append("</div>");

                        DataTable dtRef = new DataTable();
                        dtRef.Columns.Add("SecurityId");
                        DataRow row = dtRef.NewRow();
                        row["SecurityId"] = dtEmeil.Rows[i]["SecurityUserId"];
                        dtRef.Rows.Add(row);
                        objEmployeeListBAL.LogMailContent("Password Notification", strBody.ToString(), 2030, Convert.ToString(dtEmeil.Rows[i]["Email"]), ConfigurationManager.AppSettings["mailFrom"].ToString(), ConfigurationManager.AppSettings["PasswordCCed"].ToString(), ConfigurationManager.AppSettings["EmailFromCCed"].ToString(), dtRef);

                    }
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Email Logged Successfully.');", true);
            }
            catch (Exception ex)
            { }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindUser();
        }


    }
}
