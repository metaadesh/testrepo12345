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
    public partial class Admin_ManageUserSetting : System.Web.UI.Page
    {
        #region Page Variables
        Admin_UserSettingBAL objUserSettingBAL = new Admin_UserSettingBAL();
        String DefaultSorting = "UserName";
        #endregion

        #region Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindOrganizations();
                BindEntityTypes();
                SetDefaultControlValues();
                GridViewSortString = DefaultSorting;
                BindGrid();
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
            ddlOrganization.Items.Insert(0, new ListItem("All", "-1"));
        }
        #endregion

        #region Set Default Drop down value like Org,Entity Type, Status
        private void SetDefaultControlValues()
        {
            setValue(ddlOrganization, "org");
            setValue(ddlEntityType, "type");
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
        #endregion

        //#region [Bind All User]
        //protected void BindGrid()
        //{
        //    Int16 OrgID = Int16.Parse(ddlOrganization.SelectedValue);
        //    String Sort = "UserName ASC";
        //    GrdUser.DataSource = objUserSettingBAL.GetAllUserInfo(txtUserName.Text, txtDisplayName.Text, Convert.ToInt32(ddlEntityType.SelectedValue), Sort, Convert.ToInt32(ddlActiveStatus.SelectedValue), OrgID);
        //    GrdUser.DataBind();
        //}
        //#endregion

        #region Add New user button Click Event
        protected void btnAddNewUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin_User.aspx?Mode=Ins&ReturnUrl=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
        }
        #endregion

        #region Bind Entity Type Drop down
        protected void BindEntityTypes()
        {
            Admin_OrganizationBAL obj = new Admin_OrganizationBAL();
            ddlEntityType.DataSource = obj.GetRealEntityType_List();
            ddlEntityType.DataTextField = "EntityType1";
            ddlEntityType.DataValueField = "EntityTypeID";
            ddlEntityType.DataBind();
            ddlEntityType.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        #endregion

        #region Entity Type Selected Index Change Event
        //protected void ddlEntityType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindGrid();
        //}
        #endregion

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

        public String GridViewSortString
        {
            get
            {
                if (ViewState["SortString"] != null)
                    return ViewState["SortString"].ToString();
                else
                    return DefaultSorting;
            }
            set { ViewState["SortString"] = value; }

        }

        protected void GrdUser_OnSorting(object sender, GridViewSortEventArgs e)
        {
            if (GridViewSortString == e.SortExpression)
            {
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    GridViewSortDirection = SortDirection.Descending;
                }
                else
                {
                    GridViewSortDirection = SortDirection.Ascending;
                }
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
            }

            GridViewSortString = e.SortExpression;
            BindGrid();
        }
        #endregion

        #region Bind Grid View Data
        private void BindGrid()
        {
            Int16 OrgID = Int16.Parse(ddlOrganization.SelectedValue);
            String sort = "";
            String sortDirection = String.Empty;
            //sortExpression = GridViewSortString;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                sortDirection = ASCENDING;
            }
            else
            {
                sortDirection = DESCENDING;
            }

            //if (sortExpression != null)
            //    sort = sortExpression + direction;
            //else
            //    sort = DefaultSorting + ASCENDING;

            sort = GridViewSortString + sortDirection;

            GrdUser.DataSource = objUserSettingBAL.GetAllUserInfo(txtUserName.Text, txtDisplayName.Text, Convert.ToInt32(ddlEntityType.SelectedValue), sort, Convert.ToInt32(ddlActiveStatus.SelectedValue), OrgID);
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
                    ibtnUserStatus.ImageUrl = "~/Images/Inactive_grey.png";
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

        #region[ Select an employee to create the ID/Password ]
        protected void ibtnUserStatus_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtnSelect = (ImageButton)sender;
            GridViewRow grdrow = (GridViewRow)imgbtnSelect.NamingContainer;
            HiddenField hfUserStatus = (HiddenField)grdrow.FindControl("hfUserStatus");
            long UserID = Convert.ToInt64(GrdUser.DataKeys[grdrow.RowIndex].Value.ToString());

            if (hfUserStatus.Value == "1")
                objUserSettingBAL.SecurityUser_UpdateActiveStatus(UserID, 0);
            else if (hfUserStatus.Value == "0")
                objUserSettingBAL.SecurityUser_UpdateActiveStatus(UserID, 1);

            BindGrid();
        }
        #endregion

        #region Send Email Button Click Event
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
                    dtEmeil = objUserSettingBAL.UserEmail(dt);
                if (dtEmeil.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEmeil.Rows.Count; i++)
                    {
                        StringBuilder strBody = new StringBuilder();
                        string strpwd = string.Empty;
                        if (!string.IsNullOrEmpty(Convert.ToString(dtEmeil.Rows[i]["UserPassword"])))
                            strpwd = METAOPTION.Common.EncryptMD5.Decrypt(Convert.ToString(dtEmeil.Rows[i]["UserPassword"]));
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
                        objUserSettingBAL.LogMailContent("Password Notification", strBody.ToString(), 2030, Convert.ToString(dtEmeil.Rows[i]["Email"]), ConfigurationManager.AppSettings["mailFrom"].ToString(), ConfigurationManager.AppSettings["PasswordCCed"].ToString(), ConfigurationManager.AppSettings["EmailFromCCed"].ToString(), dtRef);

                    }
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Email Logged Successfully.');", true);
            }
            catch (Exception ex)
            { }
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridViewSortDirection = SortDirection.Ascending;
            GridViewSortString = DefaultSorting;
            GrdUser.PageIndex = 0;
            BindGrid();
        }

        protected void GrdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdUser.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}
