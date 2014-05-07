using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using METAOPTION.BAL;
using System.Data;
using System.Web.Services;
using Telerik.Web.UI;
using System.Web.Security;

namespace METAOPTION.UI
{
    public partial class ManagePermissions : System.Web.UI.Page
    {
        ViewGroupListBAL objViewGroupListBAL = new ViewGroupListBAL();

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
                BindGrid();
            }
        }

        #region [Bind Manage Permission List]
        protected void BindGrid()
        {
            gvIPPermission.DataSource = objViewGroupListBAL.GetAllManagePermission(Constant.OrgID);
            gvIPPermission.DataBind();
            hdnSearch.Value = "0";
        }

        protected void gvIPPermission_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblEntityType = (Label)e.Row.FindControl("lblEntityType");
                HiddenField hdIPAddress = (HiddenField)e.Row.FindControl("hdIPAddress");
                HiddenField hdnEntityType = (HiddenField)e.Row.FindControl("hdnEntityType");
                Label lblSUID = (Label)e.Row.FindControl("lblSUID");
                Int32 PermnID =Convert.ToInt32(gvIPPermission.DataKeys[e.Row.RowIndex].Value);
                //long IPPermissionID = Convert.ToInt64(gvIPPermission.DataKeys[e.Row.RowIndex].Value);
                if ((!string.IsNullOrEmpty(hdnEntityType.Value)) && (!string.IsNullOrEmpty(hdIPAddress.Value)))
                {
                    DataTable dtEntity = new DataTable();
                    dtEntity = objViewGroupListBAL.IPPermissionEntityName(Convert.ToInt32(hdnEntityType.Value), hdIPAddress.Value, PermnID);
                    if (dtEntity.Rows.Count > 0)
                    {
                        lblEntityType.Text = Convert.ToString(dtEntity.Rows[0]["EntityName"]);
                        lblSUID.Text = Convert.ToString(dtEntity.Rows[0]["SecurityUserID"]);
                    }

                }
            }
        }


        protected void gvIPPermission_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvIPPermission.PageIndex = e.NewPageIndex;
            //if (rdbIPType.SelectedValue == "-1")
            //    BindGrid();
            //else
            BindSearchGrid();
        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            IframeAdd.Attributes.Add("src", String.Format("ManagePermissionAdd.aspx"));
            MPEAdd.Show();
        }

        protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton lnkDelete = (ImageButton)sender;
                GridViewRow row = (GridViewRow)lnkDelete.NamingContainer;
                long IPPermissionID = Convert.ToInt64(gvIPPermission.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
                HiddenField hdnIPType = row.FindControl("hdnIPType") as HiddenField;
                Label lblSecurityUID = row.FindControl("lblSUID") as Label;
                Session["SUID"] = lblSecurityUID.Text;
                    ifrmEditIP.Attributes.Add("src", String.Format("ManagePermissionEdit.aspx?IPPermissionID={0}&Type={1}", IPPermissionID, hdnIPType.Value));
                ModalPopupExtender1.Show();

            }
            catch (Exception ex) { }
        }

        protected void lnkDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int IPType = 0;
                ImageButton lnkDelete = (ImageButton)sender;
                GridViewRow row = (GridViewRow)lnkDelete.NamingContainer;
                long IPPermissionID = Convert.ToInt64(gvIPPermission.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
                Label lblSecurityUID = row.FindControl("lblSUID") as Label;
                HiddenField hdnIPType = row.FindControl("hdnIPType") as HiddenField;
                if (!string.IsNullOrEmpty(hdnIPType.Value))
                {
                    if (hdnIPType.Value == "1")
                        IPType = 1;
                    else
                        IPType = 2;
                }
                 String SecurityUID = String.IsNullOrEmpty(lblSecurityUID.Text) ? "-1" : lblSecurityUID.Text;
                int ret = objViewGroupListBAL.DeleteIPPermission(IPPermissionID, IPType, Constant.UserId, SecurityUID);
                BindGrid();

            }
            catch (Exception ex) { }
        }

        public void BindSearchGrid()
        {
            string EntityId = string.Empty;
            string EntityType = string.Empty;
            string IPType = string.Empty;
            string entityName = string.Empty;

            for (int j = 0; j < dlEntity.Items.Count; j++)
            {
                CheckBox checkbox = (CheckBox)dlEntity.Items[j].FindControl("chkEntity");
                HiddenField hdn = (HiddenField)dlEntity.Items[j].FindControl("hdnEntityId");
                if (checkbox.Checked == true)
                    EntityId += hdn.Value + ",";
            }

            if (!string.IsNullOrEmpty(EntityId))
                EntityId = EntityId.Substring(0, EntityId.Length - 1);
            else
                EntityId = "-1";
            if (!string.IsNullOrEmpty(ddlEntityType.SelectedValue))
                EntityType = ddlEntityType.SelectedValue;
            else
                EntityType = "-1";
            if (rdbIPType.SelectedValue != "-1")
                IPType = rdbIPType.SelectedValue;
            else
                IPType = "";

            string strSort = string.Empty;
            if (ViewState["sortDirection"] != null)
            {
                if (Convert.ToString(ViewState["sortDirection"]).ToLower() == "ascending")
                    strSort = ViewState["SortItem"].ToString() + " asc";
                else
                    strSort = ViewState["SortItem"].ToString() + " desc";
            }
            else
                strSort = "IP.DateAdded Desc";

            gvIPPermission.DataSource = objViewGroupListBAL.GetBindSearchGrid(txtIP.Text, IPType, txtDescription.Text, EntityId, Convert.ToInt32(EntityType), strSort, Constant.OrgID);
            gvIPPermission.DataBind();


        }

        protected void ddlEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillEntityName();
        }

        public void FillEntityName()
        {
            dlEntity.Items.Clear();
            dlEntity.Text = string.Empty;
            string EntityTypeId = ddlEntityType.SelectedValue;
            if (!string.IsNullOrEmpty(EntityTypeId))
            {
                DataTable dtEntity = new DataTable();
                dtEntity = objViewGroupListBAL.GetEntity(Convert.ToInt32(EntityTypeId), Constant.OrgID);
                dlEntity.DataSource = dtEntity;
                dlEntity.DataTextField = "EntityName";
                dlEntity.DataValueField = "SecurityUserID";
                dlEntity.DataBind();
            }
            else
            {
                dlEntity.Items.Clear();
                dlEntity.Text = string.Empty;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindSearchGrid();
        }

        private void CheckPermission()
        {
            List<String> rights = CommonBAL.GetPagePermission(Constant.UserId, "WEBAPP");
            if (rights.Count < 1)
                Response.Redirect("Permission.aspx?MSG=WEBAPP.IPPERMISSIONS");

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlEntityType.Enabled = true;
            ddlEntityType.SelectedValue = "-1";
            dlEntity.Enabled = true;
            dlEntity.Items.Clear();
            dlEntity.Text = "";
            txtIP.Text = "";
            txtDescription.Text = "";
            rdbIPType.SelectedValue = "-1";
            this.gvIPPermission.PageIndex = 0;
            BindGrid();

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

        protected void gvIPPermission_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                string sortExpression = e.SortExpression;
                ViewState["SortItem"] = e.SortExpression;
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    GridViewSortDirection = SortDirection.Descending;
                }
                else
                {
                    GridViewSortDirection = SortDirection.Ascending;
                }
            }
            catch (Exception ex) { }

            BindSearchGrid();
        }

        #endregion
    }
}