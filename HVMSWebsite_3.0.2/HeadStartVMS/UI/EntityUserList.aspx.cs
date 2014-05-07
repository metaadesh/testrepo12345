using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using METAOPTION.BAL;
using System.Configuration;
using System.Web.Security;

namespace METAOPTION.UI
{
    public partial class EntityUserList : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                BindUser();
            }

        }

        #region [Bind All User]
        protected void BindUser()
        {
            String Sort = "UserName ASC";
            GrdUser.DataSource = objEmployeeListBAL.GetAllUserInfo_Ver211(txtUserName.Text, txtDisplayName.Text, Convert.ToInt32(Session["LoginEntityTypeID"]), Convert.ToInt32(Session["empId"]), Sort, Convert.ToInt32(ddlActiveStatus.SelectedValue));
            GrdUser.DataBind();
        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect("User.aspx?Mode=Ins&ReturnUrl=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
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

            GrdUser.DataSource = objEmployeeListBAL.GetAllUserInfo_Ver211(txtUserName.Text, txtDisplayName.Text, Convert.ToInt32(Session["LoginEntityTypeID"]), Convert.ToInt32(Session["empId"]), sort, Convert.ToInt32(ddlActiveStatus.SelectedValue));
            GrdUser.DataBind();
        }

        #endregion

        #region[Grid row data bound event]
        protected void GrdUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    HiddenField hfUserStatus = (HiddenField)e.Row.FindControl("hfUserStatus");
            //    ImageButton ibtnUserStatus = (ImageButton)e.Row.FindControl("ibtnUserStatus");
            //    ibtnUserStatus.Enabled = false;
            //    if (hfUserStatus.Value == "0")
            //    {
            //        ibtnUserStatus.ImageUrl = "~/Images/DeleteButton.png";
            //        ibtnUserStatus.ToolTip = "Click to activate";
            //    }
            //    else
            //    {
            //        ibtnUserStatus.ImageUrl = "~/Images/H_active.png";
            //        ibtnUserStatus.ToolTip = "Click to deactivate";
            //    }


            //}
        }

        protected void GrdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            String Sort = "UserName ASC";
            this.GrdUser.PageIndex = e.NewPageIndex;
            GrdUser.DataSource = objEmployeeListBAL.GetAllUserInfo_Ver211(txtUserName.Text, txtDisplayName.Text, Convert.ToInt32(Session["LoginEntityTypeID"]), Convert.ToInt32(Session["empId"]), Sort, Convert.ToInt32(ddlActiveStatus.SelectedValue));
            GrdUser.DataBind();
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



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindUser();
        }


    }
}
