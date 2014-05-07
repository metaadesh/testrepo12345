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
using METAOPTION;

namespace HeadStartVMS.UI
{
    public partial class ViewNewAnnouncement : System.Web.UI.Page
    {
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

        #region [ PAge Load ]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["CheckPermission"] = string.Empty;
                CheckPermission();
                BindGrid();

                if (Convert.ToInt32(Session["LoginEntityTypeID"]) == 2)
                {
                    gvNewAnnouncementList.Columns[6].Visible = false;
                    gvNewAnnouncementList.Columns[7].Visible = false;
                }
                //if ((Session["LoginEntityTypeID"] != null) && (Convert.ToString(Session["LoginEntityTypeID"]) == "2"))
                //    hplAddNewAnnouncement.Visible = false;
                //else
                //    hplAddNewAnnouncement.Visible = true;
            }
        }
        #endregion

        #region [ Check Permission ]
        private void CheckPermission()
        {
            //do not remove this try block
            try
            {
                bool bSecurity = false;
                List<String> Permissions = CommonBAL.GetPagePermission(Constant.UserId, "ANNOUNCEMENT");
                ViewState["CheckPermission"] = Permissions;
                //Count checking for those user who does not have any permission yet but their user & password is active 
                if (Permissions.Count > 0)
                {
                    if ((Permissions.Contains("ANNOUNCEMENT.ADD")))
                    {
                        bSecurity = true;
                        hplAddNewAnnouncement.Enabled = true;

                    }
                    else
                    {
                        hplAddNewAnnouncement.Enabled = false;
                        hplAddNewAnnouncement.ToolTip = "Permission Protected";
                    }
                    if ((Permissions.Contains("ANNOUNCEMENT.VIEW")))
                    {
                        if (bSecurity != true)
                        {
                            hplAddNewAnnouncement.Enabled = false;
                            hplAddNewAnnouncement.ToolTip = "Permission Protected";
                        }
                        bSecurity = true;

                    }
                    if ((Permissions.Contains("ANNOUNCEMENT.EDIT")))
                    {
                        bSecurity = true;
                    }
                    if ((Permissions.Contains("ANNOUNCEMENT.DELETE")))
                    {
                        bSecurity = true;
                    }
                    if (bSecurity == false)
                    {
                        Response.Redirect("Permission.aspx?msg=ANNOUNCEMENT.VIEW");
                    }
                }
                else
                {
                    Response.Redirect("Permission.aspx?msg=ANNOUNCEMENT.VIEW");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("Permission.aspx?msg=ANNOUNCEMENT.VIEW");
            }

        }
        #endregion

        #region [ ReffreshGrid ]
        void ReffreshGrid()
        {
            string ViewOption = Request.QueryString["Type"].ToString();
            if (ViewOption.ToLower() == "viewall")
            {
                BindGrid();

            }
            else
            {
                CurrentAnnouncementBindGrid(ViewOption);
            }
        }
        #endregion

        #region [ AnnouncementBindGrid ]
        void CurrentAnnouncementBindGrid(string AnnouncementId)
        {
            long id = Convert.ToInt64(AnnouncementId);
            gvNewAnnouncementList.DataSource = LaneAssignmentBAL.GetCurrentAnnouncementList(id);
            gvNewAnnouncementList.DataBind();

        }
        #endregion

        #region [ Get Announcement Type ]
        public int AnnouncementType(string type)
        {
            Int32 Type = 0;
            switch (type.ToLower())
            {
                case "general":
                    Type = 1;
                    break;
                case "lane":
                    Type = 2;
                    break;

                case "commission":
                    Type = 3;
                    break;

                case "chrome":
                    Type = 4;
                    break;
            }
            return Type;
        }

        #endregion

        #region [ Hide Grid Header ]
        void HideGridHeader()
        {
            gvNewAnnouncementList.HeaderRow.Cells[6].Visible = false;
            gvNewAnnouncementList.HeaderRow.Cells[7].Visible = false;
        }
        #endregion

        #region [ BindGrid ]
        void BindGrid()
        {
            LaneAssignmentBAL objAnnouncement = new LaneAssignmentBAL();
            if (Convert.ToInt32(Session["LoginEntityTypeID"]) == 2)
            {
                gvNewAnnouncementList.DataSource = objAnnouncement.GetAnnouncementSelect_Ver211(Constant.UserId, Constant.OrgID);
                gvNewAnnouncementList.DataBind();
            }
            else
            {
                gvNewAnnouncementList.DataSource = objAnnouncement.GetAnnouncementSelect(Constant.OrgID);
                gvNewAnnouncementList.DataBind();
            }

        }

        #endregion

        #region [ gvnewAnnouncementList_PageIndexChanging ]
        protected void gvnewAnnouncementList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvNewAnnouncementList.PageIndex = e.NewPageIndex; BindGrid();
        }
        #endregion

        #region [ gvnewAnnouncementList_RowCancelingEdit ]
        protected void gvnewAnnouncementList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvNewAnnouncementList.EditIndex = -1;
            BindGrid();
        }
        #endregion

        #region [ gvnewAnnouncementList_RowDeleting ]
        protected void gvnewAnnouncementList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            LaneAssignmentBAL objAnnouncement = new LaneAssignmentBAL();
            long AnnouncementTypId = Convert.ToInt64(gvNewAnnouncementList.DataKeys[e.RowIndex].Value);
            objAnnouncement.DeleteAnnouncement(AnnouncementTypId);
            BindGrid();
        }
        #endregion

        #region [ gvnewAnnouncementList_RowEditing ]
        protected void gvnewAnnouncementList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvNewAnnouncementList.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        #endregion

        #region [ gvnewAnnouncementList_RowUpdating ]
        protected void gvnewAnnouncementList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            LaneAssignmentBAL objAnnouncement = new LaneAssignmentBAL();
            GridViewRow grdrow = (GridViewRow)gvNewAnnouncementList.Rows[e.RowIndex];
            long AnnouncementTypId = Convert.ToInt64(gvNewAnnouncementList.DataKeys[e.RowIndex].Value);
            TextBox txtTitle = grdrow.FindControl("txtTitle") as TextBox;
            TextBox txtDescription = grdrow.FindControl("txtDescription") as TextBox;
            DropDownList ddlAnnouncementType = grdrow.FindControl("ddlAnnouncementType") as DropDownList;
            Announcement ObjAnn = new Announcement();
            ObjAnn.AnnouncementId = AnnouncementTypId;
            ObjAnn.AnnouncementTitle = txtTitle.Text;
            ObjAnn.Description = txtDescription.Text;
            ObjAnn.ModifiedBy = Constant.UserId;
            ObjAnn.AnnouncementTypeID = Convert.ToInt32(ddlAnnouncementType.SelectedValue);
            long iUPdateStatus = objAnnouncement.UpdateAnnouncement(ObjAnn);
            gvNewAnnouncementList.EditIndex = -1;
            BindGrid();
        }
        #endregion

        #region [ Page Render ]

        ///// <summary>
        ///// Override Render event for Invalid Page Postback Event Valdidation error and set unique id
        ///// </summary>
        ///// <param name="writer">HTML Write</param>
        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow r in gvNewAnnouncementList.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    Page.ClientScript.RegisterForEventValidation
                            (r.UniqueID + "$ctl00");
                }
            }

            base.Render(writer);
        }
        #endregion

        #region [ gvnewAnnouncementList_SelectedIndexChanged ]
        protected void gvnewAnnouncementList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LaneAssignmentBAL objAnnouncement = new LaneAssignmentBAL();
            GridView gvPolicies = (GridView)sender;
            DataKey key = gvNewAnnouncementList.SelectedDataKey;

            this.dvAnnouncementEdit.Visible = true;
            //  force databinding
            this.dvAnnouncementEdit.DataSource = objAnnouncement.GetAnnouncementById(Convert.ToInt64(key.Value));
            this.dvAnnouncementEdit.DataBind();
            //  update the contents in the detail panel
            this.updPnlCustomerDetail.Update();
            //  show the modal popup
            this.mdlPopup.Show();
        }
        #endregion

        #region [ btnUpdate_Click ]
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                //  hide the detail view control

                this.dvAnnouncementEdit.Visible = false;

                //  hide the modal popup
                this.mdlPopup.Hide();

                this.upAnnouncementList.Update();
                foreach (DetailsViewRow item in dvAnnouncementEdit.Rows)
                {
                    LaneAssignmentBAL objAnnouncement = new LaneAssignmentBAL();
                    TextBox txtAnnouncementTitle = (TextBox)item.Controls[0].FindControl("txtAnnouncementTitle");
                    TextBox txtDescription = (TextBox)item.Controls[0].FindControl("txtDescription");
                    DropDownList ddlType = (DropDownList)item.Controls[0].FindControl("ddlAnnouncement1Type");
                    TextBox id = (TextBox)item.Controls[0].FindControl("txtAnnouncementId");
                    Announcement ObjAnn = new Announcement();
                    ObjAnn.AnnouncementId = Convert.ToInt64(id.Text);
                    ObjAnn.AnnouncementTitle = txtAnnouncementTitle.Text;
                    ObjAnn.Description = txtDescription.Text;
                    ObjAnn.ModifiedBy = Constant.UserId;
                    ObjAnn.AnnouncementTypeID = Convert.ToInt32(ddlType.SelectedValue);
                    long iUPdateStatus = objAnnouncement.UpdateAnnouncement(ObjAnn);
                    break;

                }

                //  refresh the grid so we can see our changed
                BindGrid();
            }
        }
        #endregion

        #region [ dvAnnouncementEdit_ItemUpdating ]
        protected void dvAnnouncementEdit_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {

        }
        #endregion

        #region [ gvNewAnnouncementList_RowDataBound ]
        protected void gvNewAnnouncementList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // loop all data rows
                List<String> Permissions = (List<String>)ViewState["CheckPermission"];
                ImageButton btnViewDetails = (ImageButton)e.Row.FindControl("btnViewDetails");
                if (!(Permissions.Contains("ANNOUNCEMENT.EDIT")))
                {
                    gvNewAnnouncementList.Columns[6].Visible = false;
                }
                if (!(Permissions.Contains("ANNOUNCEMENT.DELETE")))
                {
                    gvNewAnnouncementList.Columns[7].Visible = false;
                }

                foreach (DataControlFieldCell cell in e.Row.Cells)
                {
                    // check all cells in one row
                    foreach (Control control in cell.Controls)
                    {
                        ImageButton button = control as ImageButton;
                        if (button != null && button.CommandName == "Delete")
                            // Add delete confirmation

                            button.OnClientClick = "if (!confirm('Are you sure " +
                                   "you want to delete this record?')) return;";


                    }
                }




            }

        }
        #endregion

        #region [ gvNewAnnouncementList_Sorting ]
        protected void gvNewAnnouncementList_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, " DESC");
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, " ASC");
            }
        }
        #endregion

        #region [ GridViewSortDirection ]
        protected SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                {
                    ViewState["sortDirection"] = SortDirection.Ascending;
                }

                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        #endregion

        #region [ SortGridView ]
        protected void SortGridView(string sortExpression, string direction)
        {

            LaneAssignmentBAL objAnnouncement = new LaneAssignmentBAL();
            DataTable dt = (DataTable)objAnnouncement.GetAnnouncementSelect(Constant.OrgID);
            DataView dv = new DataView(dt);
            dv.Sort = sortExpression + direction;
            gvNewAnnouncementList.DataSource = dv;
            gvNewAnnouncementList.DataBind();

        }
        #endregion
    }
}
