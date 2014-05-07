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
    public partial class ManageIP : System.Web.UI.Page
    {
        Admin_ViewGroupListBAL objViewGroupListBAL = new Admin_ViewGroupListBAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckPermission();
            if (!IsPostBack)
            {
                BindOrganizations();
                FillEntityTypes();
                BindSearchGrid();
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

        private void FillEntityTypes()
        {
            BAL.Admin_OrganizationBAL objBal = new BAL.Admin_OrganizationBAL();
            ddlEntityType.DataSource = objBal.GetRealEntityType_List();
            ddlEntityType.DataTextField = "EntityType1";
            ddlEntityType.DataValueField = "EntityTypeID";
            ddlEntityType.DataBind();
            ddlEntityType.Items.Insert(0, new ListItem("All", "-1"));
        }

        #region [Bind Manage Permission List]
        //protected void BindGrid()
        //{
        //    Int16 OrgID = Int16.Parse(ddlOrganization.SelectedValue);
        //    gvIPPermission.DataSource = objViewGroupListBAL.GetAllManagePermission(OrgID);
        //    gvIPPermission.DataBind();
        //    hdnSearch.Value = "0";
        //}

        protected void gvIPPermission_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblEntityType = (Label)e.Row.FindControl("lblEntityType");
                HiddenField hdIPAddress = (HiddenField)e.Row.FindControl("hdIPAddress");
                HiddenField hdnEntityType = (HiddenField)e.Row.FindControl("hdnEntityType");
                Label lblSUID = (Label)e.Row.FindControl("lblSUID");
                Int32 PermnID = Convert.ToInt32(gvIPPermission.DataKeys[e.Row.RowIndex].Value);
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
            IframeAdd.Attributes.Add("src", String.Format("Admin_AddIP.aspx"));
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
                ifrmEditIP.Attributes.Add("src", String.Format("Admin_EditIP.aspx?IPPermissionID={0}&Type={1}", IPPermissionID, hdnIPType.Value));
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
                BindSearchGrid();

            }
            catch (Exception ex) { }
        }

        public void BindSearchGrid()
        {
            string EntityId = string.Empty;
            string EntityType = string.Empty;
            string IPType = string.Empty;
            string entityName = string.Empty;
            Int16 OrgID = Int16.Parse(ddlOrganization.SelectedValue);

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

            gvIPPermission.DataSource = objViewGroupListBAL.GetBindSearchGrid(txtIP.Text, IPType, txtDescription.Text, EntityId, Convert.ToInt32(EntityType), strSort, OrgID);
            gvIPPermission.DataBind();


        }

        protected void ddlOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillEntityName();
        }

        protected void ddlEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillEntityName();
        }

        public void FillEntityName()
        {
            Int16 OrgID = Int16.Parse(ddlOrganization.SelectedValue);
            dlEntity.Items.Clear();
            dlEntity.Text = string.Empty;
            string EntityTypeId = ddlEntityType.SelectedValue;
            if (!string.IsNullOrEmpty(EntityTypeId))
            {
                DataTable dtEntity = new DataTable();
                dtEntity = objViewGroupListBAL.GetEntity(Convert.ToInt32(EntityTypeId), OrgID);
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
            //List<String> rights = CommonBAL.GetPagePermission(Constant.UserId, "WEBAPP");
            //if (rights.Count < 1)
            //    Response.Redirect("Permission.aspx?MSG=WEBAPP.IPPERMISSIONS");

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
            BindSearchGrid();

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