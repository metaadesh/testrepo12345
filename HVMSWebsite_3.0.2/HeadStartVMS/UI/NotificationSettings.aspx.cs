using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using METAOPTION.BAL;
using System.Data;
using System.Web.Services;
using System.Web.Security;

namespace METAOPTION.UI
{
    public partial class NotificationSettings : System.Web.UI.Page
    {
        NotificationBAL BAL = new NotificationBAL();

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
            if (!Page.IsPostBack)
            {
                CheckPermission();
                BindTableSearchDDL();
                BindNotificationTypeSearchDDL();
                BindEntityTypeSearchDDL();
                BindAddedBySearchDDL();
                BindSort1DDL();
                BindNotificationGrid();
            }
        }

        #region[Main screen code]

        #region[Bind Notification Grid]
        protected void BindNotificationGrid()
        {
            gvNotifications.PageSize = Convert.ToInt32(ddlPageSize1.SelectedValue);

            String sort = String.Empty;
            if (ddlSort1.SelectedValue != "-1")
                sort = String.Format("{0} {1}", ddlSort1.SelectedValue, rbtnSort1Direction.SelectedValue);

            if (ddlSort2.SelectedValue != "-1")
                sort += String.Format("{0} {1}"
                    , sort == "" ? ddlSort2.SelectedValue : ", " + ddlSort2.SelectedValue
                    , rbtnSort2Direction.SelectedValue);

            if (ddlSort3.SelectedValue != "-1")
                sort += String.Format("{0} {1}"
                    , sort == "" ? ddlSort3.SelectedValue : ", " + ddlSort3.SelectedValue
                    , rbtnSort3Direction.SelectedValue);

            if (String.IsNullOrEmpty(sort))
                sort = "NP.NotificationPreferenceID DESC";

            String EntityID = ddlEntitySearch.SelectedValue;
            if (String.IsNullOrEmpty(EntityID))
                EntityID = "-1";

            ObjectDataSource odsNotification = new ObjectDataSource();
            odsNotification.Selected += new ObjectDataSourceStatusEventHandler(odsNotification_Selected);
            odsNotification.TypeName = "METAOPTION.NotificationBAL";
            odsNotification.SelectMethod = "SearchNotification";
            odsNotification.SelectCountMethod = "SearchNotificationCount";
            odsNotification.EnablePaging = true;
            odsNotification.SelectParameters.Add("TableID", ddlTableSearch.SelectedValue);
            odsNotification.SelectParameters.Add("NotificationTypeID", ddlNotifTypeSearch.SelectedValue);
            odsNotification.SelectParameters.Add("EntityTypeID", ddlEntityTypeSearch.SelectedValue);
            odsNotification.SelectParameters.Add("EntityID", EntityID);
            odsNotification.SelectParameters.Add("MailTo", txtMailToSearch.Text.Trim());
            odsNotification.SelectParameters.Add("AddedBy", ddlAddedBySearch.SelectedValue);
            odsNotification.SelectParameters.Add("StartRowIndex", DbType.Int32, gvNotifications.PageIndex.ToString());
            odsNotification.SelectParameters.Add("MaximumRows", DbType.Int32, ddlPageSize1.SelectedValue);
            odsNotification.SelectParameters.Add("SortExpression", sort);
            odsNotification.SelectParameters.Add("OrgID", Constant.OrgID.ToString());
            gvNotifications.DataSource = odsNotification;
            gvNotifications.DataBind();
        }
        #endregion

        #region[Notification grid- RowdataBound]
        protected void gvNotifications_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton ibtnEmail = (ImageButton)e.Row.FindControl("ibtnEmail");
                HiddenField hfEmail = (HiddenField)e.Row.FindControl("hfEmail");
                ImageButton ibtnSMS = (ImageButton)e.Row.FindControl("ibtnSMS");
                HiddenField hfSMS = (HiddenField)e.Row.FindControl("hfSMS");

                if (hfEmail.Value == "True")
                {
                    ibtnEmail.ImageUrl = "../Images/H_active.png";
                    ibtnEmail.ToolTip = "Click to change";
                }
                else
                {
                    ibtnEmail.ImageUrl = "../Images/H_delete.png";
                    ibtnEmail.ToolTip = "Click to change";
                }

                if (hfSMS.Value == "True")
                {
                    ibtnSMS.ImageUrl = "../Images/H_active.png";
                    ibtnSMS.ToolTip = "Click to change";
                }
                else
                {
                    ibtnSMS.ImageUrl = "../Images/H_delete.png";
                    ibtnSMS.ToolTip = "Click to change";
                }
            }
        }
        #endregion

        #region[Change notification preferences]
        protected void ibtnEmail_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibtn = sender as ImageButton;
            GridViewRow row = (GridViewRow)ibtn.NamingContainer;
            Int32 Id = Convert.ToInt32(gvNotifications.DataKeys[row.RowIndex].Value);
            HiddenField hfEmail = (HiddenField)row.FindControl("hfEmail");

            Boolean Status = Convert.ToBoolean(hfEmail.Value == "True" ? "False" : "True");
            BAL.UpdateNotifyViaEmail(Id, Status, Constant.UserId);
            BindNotificationGrid();
        }

        protected void ibtnSMS_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibtn = sender as ImageButton;
            GridViewRow row = (GridViewRow)ibtn.NamingContainer;
            Int32 Id = Convert.ToInt32(gvNotifications.DataKeys[row.RowIndex].Value);
            HiddenField hfSMS = (HiddenField)row.FindControl("hfSMS");

            Boolean Status = Convert.ToBoolean(hfSMS.Value == "True" ? "False" : "True");
            BAL.UpdateNotifyViaSMS(Id, Status, Constant.UserId);
            BindNotificationGrid();
        }
        #endregion

        #region[Delete notification preferences]
        protected void ibtnDeleteNotPref_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibtn = sender as ImageButton;
            GridViewRow row = (GridViewRow)ibtn.NamingContainer;
            Int32 Id = Convert.ToInt32(gvNotifications.DataKeys[row.RowIndex].Value);
            BAL.DeleteNotificationPreference(Id, 1);
            BindNotificationGrid();
        }
        #endregion

        #region[Manage notification button event handler]
        protected void btnManageNotification_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageNotifications.aspx");
        }
        #endregion

        #region[Drop down paging]
        protected void odsNotification_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue.GetType().Name == "Int32")
            {

                Int32 count = Convert.ToInt32(e.ReturnValue);
                Int32 pagesize = gvNotifications.PageSize;
                Int32 pagecount = count / pagesize;
                if (count < pagesize)
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} of {0} records", count);
                }
                else
                {
                    lblCount1.Text = lblCount.Text = String.Format("Showing {0} through {1} of {2} records"
                         , String.Format("{0:#,###}", pagesize * gvNotifications.PageIndex + 1)
                         , String.Format("{0:#,###}", pagesize * (gvNotifications.PageIndex + 1) > count ? count : pagesize * (gvNotifications.PageIndex + 1))
                         , String.Format("{0:#,###}", count));
                }

                if ((count % pagesize) > 0) pagecount++;

                ddlPaging.Items.Clear();
                ddlPaging1.Items.Clear();

                if (pagecount != 0)
                {
                    for (int i = 0; i < pagecount; i++)
                    {
                        ddlPaging.Items.Add(new ListItem((i + 1).ToString(), (i + 1).ToString()));
                        ddlPaging1.Items.Add(new ListItem((i + 1).ToString(), (i + 1).ToString()));
                    }

                    ddlPaging.SelectedValue = String.Format("{0}", gvNotifications.PageIndex + 1);
                    ddlPaging1.SelectedValue = String.Format("{0}", gvNotifications.PageIndex + 1);
                    EnablePaging();
                }
                else
                {
                    ddlPaging.Items.Add(new ListItem("0", "0"));
                    ddlPaging.SelectedValue = "0";
                    ddlPaging1.Items.Add(new ListItem("0", "0"));
                    ddlPaging1.SelectedValue = "0";
                    EnablePaging();
                }
            }
        }
        #endregion

        #region[Drop down page selection change]
        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvNotifications.PageIndex = Convert.ToInt32(((DropDownList)sender).SelectedValue) - 1;
            BindNotificationGrid();
            EnablePaging();
        }
        #endregion

        #region[Enable/Disable paging]
        private void EnablePaging()
        {
            btnPrev1.Enabled = btnPrev.Enabled = true;
            btnFirst1.Enabled = btnFirst.Enabled = true;
            btnNext1.Enabled = btnNext.Enabled = true;
            btnLast1.Enabled = btnLast.Enabled = true;

            if ((ddlPaging.SelectedValue == "0") || (ddlPaging1.SelectedValue == "0"))
            {
                btnPrev1.Enabled = btnPrev.Enabled = false;
                btnFirst1.Enabled = btnFirst.Enabled = false;
                btnNext1.Enabled = btnNext.Enabled = false;
                btnLast1.Enabled = btnLast.Enabled = false;
            }
            else if (ddlPaging.SelectedValue == "1")
            {
                if ((ddlPaging.SelectedValue == ddlPaging.Items.Count.ToString()) || (ddlPaging1.SelectedValue == ddlPaging1.Items.Count.ToString()))
                {
                    btnPrev1.Enabled = btnPrev.Enabled = false;
                    btnFirst1.Enabled = btnFirst.Enabled = false;
                    btnNext1.Enabled = btnNext.Enabled = false;
                    btnLast1.Enabled = btnLast.Enabled = false;
                }
                else
                {
                    btnPrev1.Enabled = btnPrev.Enabled = false;
                    btnFirst1.Enabled = btnFirst.Enabled = false;
                }
            }
            else if ((ddlPaging.SelectedValue == ddlPaging.Items.Count.ToString()) || (ddlPaging1.SelectedValue == ddlPaging1.Items.Count.ToString()))
            {
                btnNext1.Enabled = btnNext.Enabled = false;
                btnLast1.Enabled = btnLast.Enabled = false;

            }
        }
        #endregion

        #region[Paging Click events]
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            gvNotifications.PageIndex = 0;
            BindNotificationGrid();
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedIndex > 0)
                gvNotifications.PageIndex--;

            BindNotificationGrid();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (ddlPaging.SelectedValue != ddlPaging.Items.Count.ToString())
                gvNotifications.PageIndex++;

            BindNotificationGrid();
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            gvNotifications.PageIndex = ddlPaging.Items.Count - 1;
            BindNotificationGrid();
        }
        #endregion

        #region[Page size selection change]
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            if (ddl.ID == "ddlPageSize1")
                ddlPageSize2.SelectedValue = ddlPageSize1.SelectedValue;
            else
                ddlPageSize1.SelectedValue = ddlPageSize2.SelectedValue;

            gvNotifications.PageIndex = 0;

            BindNotificationGrid();
        }
        #endregion

        #region[Sort dropdown selected index changed handler]
        protected void ddlSort1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSort2DDL();
            ddlSort2.SelectedValue = "-1";
            ddlSort3.SelectedValue = "-1";
        }

        protected void ddlSort2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSort3DDL();
            ddlSort3.SelectedValue = "-1";
        }
        #endregion

        #region[ Bind sort dropdown]
        private void BindSort1DDL()
        {
            ddlSort1.DataSource = BAL.NotificationList_SortOptions("SELECT SortText, SortValue FROM Lookup_Sort WHERE TableId = 11 ORDER BY Sequence ASC", "", "");
            ddlSort1.DataTextField = "SortText";
            ddlSort1.DataValueField = "SortValue";
            ddlSort1.DataBind();
            ddlSort1.Items.Insert(0, new ListItem("", "-1"));

            BindSort2DDL();
            BindSort3DDL();
        }

        private void BindSort2DDL()
        {
            ddlSort2.DataSource = BAL.NotificationList_SortOptions(String.Format("SELECT SortText, SortValue FROM Lookup_Sort WHERE TableId = 11 AND SortValue NOT IN ('{0}') ORDER BY Sequence ASC", ddlSort1.SelectedValue), "", "");
            ddlSort2.DataTextField = "SortText";
            ddlSort2.DataValueField = "SortValue";
            ddlSort2.DataBind();
            ddlSort2.Items.Insert(0, new ListItem("", "-1"));

            BindSort3DDL();
        }

        private void BindSort3DDL()
        {
            ddlSort3.DataSource = BAL.NotificationList_SortOptions(String.Format("SELECT SortText, SortValue FROM Lookup_Sort WHERE TableId = 11 AND SortValue NOT IN ('{0}', '{1}') ORDER BY Sequence ASC", ddlSort1.SelectedValue, ddlSort2.SelectedValue), "", "");
            ddlSort3.DataTextField = "SortText";
            ddlSort3.DataValueField = "SortValue";
            ddlSort3.DataBind();
            ddlSort3.Items.Insert(0, new ListItem("", "-1"));

        }
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

        protected void gvNotifications_Sorting(object sender, GridViewSortEventArgs e)
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
            if (ddlSort1.Items.FindByValue(sortExpression) != null)
            {
                ddlSort1.SelectedValue = sortExpression;
                if (direction == " ASC") rbtnSort1Direction.SelectedIndex = 0;
                else rbtnSort1Direction.SelectedIndex = 1;
            }

            if (ddlSort2.Items.FindByValue(sortExpression) != null)
            {
                ddlSort2.SelectedValue = "-1";
                rbtnSort2Direction.SelectedIndex = 0;
            }

            if (ddlSort3.Items.FindByValue(sortExpression) != null)
            {
                ddlSort3.SelectedValue = "-1";
                rbtnSort3Direction.SelectedIndex = 0;
            }

            BindNotificationGrid();
        }

        #endregion

        #region[Bind Search Controls]
        protected void BindTableSearchDDL()
        {
            ddlTableSearch.DataSource = BAL.GetAllTables_Notification();
            ddlTableSearch.DataTextField = "TableName";
            ddlTableSearch.DataValueField = "TableID";
            ddlTableSearch.DataBind();
            ddlTableSearch.Items.Insert(0, new ListItem("ALL", "-1"));
        }

        protected void ddlTableSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTableSearch.SelectedValue == "-1")
                BindNotificationTypeSearchDDL();
            else
                BindNotificationTypeSearchDDLByTableID();
        }

        protected void BindNotificationTypeSearchDDLByTableID()
        {
            ddlNotifTypeSearch.DataSource = BAL.GetNotificationTypeByTableID(Convert.ToInt32(ddlTableSearch.SelectedValue));
            ddlNotifTypeSearch.DataTextField = "NotificationType1";
            ddlNotifTypeSearch.DataValueField = "NotificationTypeID";
            ddlNotifTypeSearch.DataBind();
            ddlNotifTypeSearch.Items.Insert(0, new ListItem("ALL", "-1"));
        }

        protected void BindNotificationTypeSearchDDL()
        {
            ddlNotifTypeSearch.DataSource = BAL.GetAllNotificationType();
            ddlNotifTypeSearch.DataTextField = "NotificationType1";
            ddlNotifTypeSearch.DataValueField = "NotificationTypeID";
            ddlNotifTypeSearch.DataBind();
            ddlNotifTypeSearch.Items.Insert(0, new ListItem("ALL", "-1"));
        }

        protected void BindEntityTypeSearchDDL()
        {
            ddlEntityTypeSearch.DataSource = BAL.GetAllEntityType_Notification();
            ddlEntityTypeSearch.DataTextField = "EntityType1";
            ddlEntityTypeSearch.DataValueField = "EntityTypeID";
            ddlEntityTypeSearch.DataBind();
            ddlEntityTypeSearch.Items.Insert(0, new ListItem("ALL", "-1"));
        }

        protected void ddlEntityTypeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindEntitySearchDDL();
        }

        protected void BindEntitySearchDDL()
        {
            ddlEntitySearch.DataSource = BAL.NotificationEntitiesByEntityType(Convert.ToInt32(ddlEntityTypeSearch.SelectedValue), Constant.OrgID);
            ddlEntitySearch.DataTextField = "DisplayName";
            ddlEntitySearch.DataValueField = "EntityId";
            ddlEntitySearch.DataBind();
            ddlEntitySearch.Items.Insert(0, new ListItem("ALL", "-1"));
        }

        protected void BindAddedBySearchDDL()
        {
            ddlAddedBySearch.DataSource = BAL.GetNotfPrefAddedBy(Constant.OrgID);
            ddlAddedBySearch.DataTextField = "DisplayName";
            ddlAddedBySearch.DataValueField = "SecurityUserID";
            ddlAddedBySearch.DataBind();
            ddlAddedBySearch.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        #endregion

        protected void btnSearchNotification_Click(object sender, EventArgs e)
        {
            BindNotificationGrid();
        }

        #endregion

        #region[PopUp Code]

        #region[Bind Entity Type Dropdown]
        protected void BindEntityTypeddl()
        {
            ddlEntityTypes.DataSource = BAL.GetAllEntityType_Notification();
            ddlEntityTypes.DataTextField = "EntityType1";
            ddlEntityTypes.DataValueField = "EntityTypeID";
            ddlEntityTypes.DataBind();
            ddlEntityTypes.Items.Insert(0, new ListItem("SELECT", "-1"));
        }
        #endregion

        #region[EntityType - SelectedIndexChanged]
        protected void ddlEntityTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpeAddNewPreference.Show();
            hfEntityTypeID.Value = ddlEntityTypes.SelectedValue;
            BindEntitiesddl();
            ddlTableType.Items.Clear();
            ddlNotificationType.Items.Clear();
            lblError.Text = "";
            chkEmail.Checked = chkSMS.Checked = false;
            trColumns.Visible = false;
        }
        #endregion

        #region[Bind Entities]
        protected void BindEntitiesddl()
        {
            ddlEntities.DataSource = BAL.GetEntitiesByEntityType(Convert.ToInt32(hfEntityTypeID.Value), Constant.OrgID);
            ddlEntities.DataTextField = "DisplayName";
            ddlEntities.DataValueField = "EntityId";
            ddlEntities.DataBind();
            ddlEntities.Items.Insert(0, new ListItem("SELECT", "-1"));
        }
        #endregion

        #region[Entities - SelectedIndexChanged]
        protected void ddlEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpeAddNewPreference.Show();
            hfEntityID.Value = ddlEntities.SelectedValue;
            BindTablesddl();
            BindUserNotificationGrid();
            ddlNotificationType.Items.Clear();
            lblError.Text = "";
            chkEmail.Checked = chkSMS.Checked = false;
            trColumns.Visible = false;
        }
        #endregion

        #region[Bind Table Dropdown]
        protected void BindTablesddl()
        {
            ddlTableType.DataSource = BAL.GetAllTables_Notification();
            ddlTableType.DataTextField = "TableName";
            ddlTableType.DataValueField = "TableID";
            ddlTableType.DataBind();
            ddlTableType.Items.Insert(0, new ListItem("SELECT", "-1"));
        }
        #endregion

        #region[TableType - SelectedIndexChanged]
        protected void ddlTableType_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpeAddNewPreference.Show();
            BindNotificationTypeddl();
            lblError.Text = "";
            chkEmail.Checked = chkSMS.Checked = false;
            trColumns.Visible = false;
        }
        #endregion

        #region[Bind Notification Type DDL]
        protected void BindNotificationTypeddl()
        {
            Int32 TableID = Convert.ToInt32(ddlTableType.SelectedValue);
            ddlNotificationType.DataSource = BAL.GetNotificationTypeByTableID(TableID);
            ddlNotificationType.DataTextField = "NotificationType1";
            ddlNotificationType.DataValueField = "NotificationTypeID";
            ddlNotificationType.DataBind();
            ddlNotificationType.Items.Insert(0, new ListItem("SELECT", "-1"));
        }
        #endregion

        #region[NotificationType - SelectedIndexChanged]
        protected void ddlNotificationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpeAddNewPreference.Show();
            Int32 EntityTypeID = Convert.ToInt32(ddlEntityTypes.SelectedValue);
            long EntityID = Convert.ToInt64(ddlEntities.SelectedValue);
            Int32 NotificationTypeID = Convert.ToInt32(ddlNotificationType.SelectedValue);

            if (BAL.UserNotificationAlreadySet(EntityTypeID, EntityID, NotificationTypeID))
                BindColumnsChkBoxList();
            else
            {
                if (NotificationTypeID == 4)
                {
                    BindColumnsChkBoxList();
                    lblError.Text = "";
                }
                else
                {
                    //Response.Write("<script>alert('Hello');</script>");
                    lblError.Text = "Notification already exist for selected user";
                    ddlNotificationType.SelectedValue = "-1";
                }
            }
        }
        #endregion

        #region[Bind columns checkbox list]
        protected void BindColumnsChkBoxList()
        {
            Int32 TableID = Convert.ToInt32(ddlTableType.SelectedValue);
            Int32 ETID = Convert.ToInt32(hfEntityTypeID.Value);
            Int64 EID = Convert.ToInt64(hfEntityID.Value);
            Int32 NotfTypeID = Convert.ToInt32(ddlNotificationType.SelectedValue);

            if (TableID == 2 && NotfTypeID == 4)
            {
                cblColumns.DataSource = BAL.GetColumnsToNotify(TableID);
                cblColumns.DataTextField = "ColumnDescription";
                cblColumns.DataValueField = "ColumnID";
                cblColumns.DataBind();
                if (cblColumns.Items.Count > 0)
                {
                    trColumns.Visible = true;
                    List<TableColumnMaster> a = BAL.GetAlreadyNotifiedColumns(TableID, ETID, EID);
                    foreach (ListItem item in cblColumns.Items)
                    {
                        foreach (TableColumnMaster i in a)
                        {
                            if (item.Value == Convert.ToString(i.ColumnID))
                            {
                                item.Selected = true;
                                item.Enabled = false;
                            }
                        }
                    }
                }
                else
                    trColumns.Visible = false;
            }
            else
                trColumns.Visible = false;
        }
        #endregion

        #region[Set Preference]
        private void AddNotificationPreference()
        {
            Int32 NotificationPreferenceId = 0;
            NotificationPreference objNP = new NotificationPreference();

            Int32 TableID = Convert.ToInt32(ddlTableType.SelectedValue);
            Int32 NotfTypeID = Convert.ToInt32(ddlNotificationType.SelectedValue);
            if (TableID == 2 && NotfTypeID == 4)
            {
                if (cblColumns.Items.Count > 0)
                {
                    foreach (ListItem item in cblColumns.Items)
                        if (item.Selected)
                        {
                            objNP.NotificationTypeID = Convert.ToInt32(ddlNotificationType.SelectedValue);
                            objNP.EntityTypeID = Convert.ToInt32(ddlEntityTypes.SelectedValue);
                            objNP.EntityID = Convert.ToInt32(ddlEntities.SelectedValue);
                            objNP.ColumnID = Convert.ToInt64(item.Value);
                            objNP.NotifyViaEmail = chkEmail.Checked;
                            objNP.NotifyViaSMS = chkSMS.Checked;
                            objNP.AddedBy = Constant.UserId;
                            objNP.IsActive = true;
                            NotificationPreferenceId = BAL.NotificationPreference_Insert(objNP);
                        }
                }
            }
            else
            {
                objNP.NotificationTypeID = Convert.ToInt32(ddlNotificationType.SelectedValue);
                objNP.EntityTypeID = Convert.ToInt32(ddlEntityTypes.SelectedValue);
                objNP.EntityID = Convert.ToInt32(ddlEntities.SelectedValue);
                objNP.ColumnID = 0;
                objNP.NotifyViaEmail = chkEmail.Checked;
                objNP.NotifyViaSMS = chkSMS.Checked;
                objNP.AddedBy = Constant.UserId;
                objNP.IsActive = true;
                NotificationPreferenceId = BAL.NotificationPreference_Insert(objNP);
            }

        }
        #endregion

        #region[Add button event handler]
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //mpeAddNewPreference.Show();
            AddNotificationPreference();
            //Reset controls
            BindTablesddl();
            ddlNotificationType.Items.Clear();
            chkEmail.Checked = false;
            chkSMS.Checked = false;

            //Bind grid
            //BindUserNotificationGrid();
            BindNotificationGrid();
        }
        #endregion

        #region[Bind Notification Grid]
        protected void BindUserNotificationGrid()
        {
            Int32 EntTypeID = Convert.ToInt32(hfEntityTypeID.Value);
            long EntID = Convert.ToInt32(hfEntityID.Value);
            gvUserNotifications.DataSource = BAL.FetchUserNotification(EntTypeID, EntID);
            gvUserNotifications.DataBind();
        }
        #endregion
        /*
        #region[Notification grid- RowdataBound]
        protected void gvUserNotifications_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton ibtnNotifyViaEmail = (ImageButton)e.Row.FindControl("ibtnNotifyViaEmail");
                HiddenField hfNotifyViaEmail = (HiddenField)e.Row.FindControl("hfNotifyViaEmail");
                ImageButton ibtnNotifyViaSMS = (ImageButton)e.Row.FindControl("ibtnNotifyViaSMS");
                HiddenField hfNotifyViaSMS = (HiddenField)e.Row.FindControl("hfNotifyViaSMS");

                if (hfNotifyViaEmail.Value == "True")
                {
                    ibtnNotifyViaEmail.ImageUrl = "../Images/H_active.png";
                    ibtnNotifyViaEmail.ToolTip = "Click to change";
                }
                else
                {
                    ibtnNotifyViaEmail.ImageUrl = "../Images/H_delete.png";
                    ibtnNotifyViaEmail.ToolTip = "Click to change";
                }

                if (hfNotifyViaSMS.Value == "True")
                {
                    ibtnNotifyViaSMS.ImageUrl = "../Images/H_active.png";
                    ibtnNotifyViaSMS.ToolTip = "Click to change";
                }
                else
                {
                    ibtnNotifyViaSMS.ImageUrl = "../Images/H_delete.png";
                    ibtnNotifyViaSMS.ToolTip = "Click to change";
                }
            }
        }
        #endregion

        #region[Change notification preferences]
        protected void ibtnNotifyViaEmail_Click(object sender, ImageClickEventArgs e)
        {
            mpeAddNewPreference.Show();
            ImageButton ibtn = sender as ImageButton;
            GridViewRow row = (GridViewRow)ibtn.NamingContainer;
            Int32 Id = Convert.ToInt32(gvUserNotifications.DataKeys[row.RowIndex].Value);
            HiddenField hfNotifyViaEmail = (HiddenField)row.FindControl("hfNotifyViaEmail");

            Boolean Status = Convert.ToBoolean(hfNotifyViaEmail.Value == "True" ? "False" : "True");
            BAL.UpdateNotifyViaEmail(Id, Status, Constant.UserId);
            BindUserNotificationGrid();
        }

        protected void ibtnNotifyViaSMS_Click(object sender, ImageClickEventArgs e)
        {
            mpeAddNewPreference.Show();
            ImageButton ibtn = sender as ImageButton;
            GridViewRow row = (GridViewRow)ibtn.NamingContainer;
            Int32 Id = Convert.ToInt32(gvUserNotifications.DataKeys[row.RowIndex].Value);
            HiddenField hfNotifyViaSMS = (HiddenField)row.FindControl("hfNotifyViaSMS");

            Boolean Status = Convert.ToBoolean(hfNotifyViaSMS.Value == "True" ? "False" : "True");
            BAL.UpdateNotifyViaSMS(Id, Status, Constant.UserId);
            BindUserNotificationGrid();
        }
        #endregion

        #region[Delete notification preferences]
        protected void ibtnDeleteUserNotPref_Click(object sender, ImageClickEventArgs e)
        {
            mpeAddNewPreference.Show();
            ImageButton ibtn = sender as ImageButton;
            GridViewRow row = (GridViewRow)ibtn.NamingContainer;
            Int32 Id = Convert.ToInt32(gvUserNotifications.DataKeys[row.RowIndex].Value);
            BAL.DeleteNotificationPreference(Id, 1);
            BindUserNotificationGrid();
        }
        #endregion        
        */
        #region[Reset all controls]
        private void ResetControls()
        {
            ddlEntityTypes.Items.Clear();
            ddlEntities.Items.Clear();
            ddlTableType.Items.Clear();
            ddlNotificationType.Items.Clear();
            trColumns.Visible = false;
            chkEmail.Checked = false;
            chkSMS.Checked = false;
        }
        #endregion

        protected void btnAddNotificationPreference_Click(object sender, EventArgs e)
        {
            mpeAddNewPreference.Show();
            ResetControls();
            BindEntityTypeddl();
        }

        #endregion

        [WebMethod]
        public static Int32 IsNotificationAlreadySet(Int32 EntityTypeID, long EntityID, Int32 NotificationTypeID)
        {
            NotificationBAL bal = new NotificationBAL();
            if (bal.UserNotificationAlreadySet(EntityTypeID, EntityID, NotificationTypeID))
                return 1;
            else
                return 0;
        }


        #region[Added by Rupendra 17 Dec 12 for set Page Permission]
        private void CheckPermission()
        {
            List<String> rights = CommonBAL.GetPagePermission(Constant.UserId, "WEBAPP");
            if (rights.Count < 1)
                Response.Redirect("Permission.aspx?MSG=WEBAPP.NOTIFICATIONSETTINGS");

        }
        #endregion
    }
}