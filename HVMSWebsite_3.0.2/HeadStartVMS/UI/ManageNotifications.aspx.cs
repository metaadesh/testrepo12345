using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.Security;

namespace METAOPTION.UI
{
    public partial class ManageNotifications : System.Web.UI.Page
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
                BindNotificationTypeddl();
                BindNotificationCcBcc();
                BindEmpTypeddl();
            }
        }        

        #region[Bind NotificationType DDL]
        private void BindNotificationTypeddl()
        {
            ddlNotificationTypes.DataSource = BAL.GetAllNotificationType();
            ddlNotificationTypes.DataTextField = "NotificationType1";
            ddlNotificationTypes.DataValueField = "NotificationTypeID";
            ddlNotificationTypes.DataBind();
            ddlNotificationTypes.SelectedValue = "1";
        }
        #endregion

        #region[EntityType - SelectedIndexChanged]
        protected void ddlNotificationTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindNotificationCcBcc();
        }
        #endregion

        #region[Bind Notification grid for which CC/BCC already set]
        private void BindNotificationCcBcc()
        {
            gvNotificationCcBcc.DataSource = BAL.GetCcBcc_NotificationType(Convert.ToInt32(ddlNotificationTypes.SelectedValue),Constant.OrgID);
            gvNotificationCcBcc.DataBind();
        }
        #endregion

        #region[Add button click event handler]
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            mpeAddCC.Show();
            BindEmployeeDetailsGrid();
            ClearFields();
        }
        #endregion

        #region[Clear Fields]
        private void ClearFields()
        {
            txtEmail.Text = "";
            txtCellPhone.Text = "";
            txtFName.Text = "";
            txtLName.Text = "";
            txtEmail.Text = "";
            //gvEmployeeDetails.Visible = false;
        }
        #endregion

        #region[CC, BCC, SMS popup paging]
        protected void gvEmployeeDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            mpeAddCC.Show();
            gvEmployeeDetails.PageIndex = e.NewPageIndex;
            BindEmployeeDetailsGrid();
        }
        #endregion

        #region[Bind EmployeeType info]
        private void BindEmployeeDetailsGrid()
        {            
            Int32 EmpTypeID = Convert.ToInt32(ddlEmployeeType.SelectedValue);
            Int32 NotfTypeID = Convert.ToInt32(ddlNotificationTypes.SelectedValue);
            gvEmployeeDetails.DataSource = BAL.SearchEmployee(EmpTypeID, txtEmail.Text, txtCellPhone.Text, txtFName.Text, txtLName.Text, NotfTypeID,Constant.OrgID);
            gvEmployeeDetails.DataBind();
        }
        #endregion

        #region[Search employee type]
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            mpeAddCC.Show();
            gvEmployeeDetails.Visible = true;
            gvEmployeeDetails.PageIndex = 0;
            BindEmployeeDetailsGrid();
        }
        #endregion

        #region[EmployeeType grid- RowdataBound]
        protected void gvEmployeeDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
            }
        }
        #endregion

        #region[Save CC,BCC,SMS preferences to database]
        protected void btnSaveCcBccSmsTo_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow grvrow in gvEmployeeDetails.Rows)
            {
                Label lblEmail = (Label)grvrow.FindControl("lblEmail");
                Label lblCellPhone = (Label)grvrow.FindControl("lblCellPhone");
                HiddenField hfEmpTypeID = (HiddenField)grvrow.FindControl("hfEmpTypeID");
                CheckBox chkCCto = (CheckBox)grvrow.FindControl("chkSelectCCto");
                CheckBox chkBCCTo = (CheckBox)grvrow.FindControl("chkSelectBCCto");
                CheckBox chkSMSTo = (CheckBox)grvrow.FindControl("chkSelectSMSto");
                Int32 EmpID = Convert.ToInt32(gvEmployeeDetails.DataKeys[grvrow.RowIndex].Value);
                if (chkCCto.Checked || chkBCCTo.Checked || chkSMSTo.Checked)
                {
                    Int32 NotfRecptID = 0;
                    NotificationRecipientCcBcc objNP = new NotificationRecipientCcBcc();
                    objNP.NotificationTypeID = Convert.ToInt32(ddlNotificationTypes.SelectedValue);
                    //objNP.EntityTypeID = 0;
                    //objNP.EntityID = 0;
                    objNP.EmployeeTypeID = Convert.ToInt32(hfEmpTypeID.Value);
                    objNP.EmployeeID = EmpID;
                    objNP.EmailCC = chkCCto.Checked;
                    objNP.EmailBCC = chkBCCTo.Checked;
                    objNP.SMS = chkSMSTo.Checked;
                    objNP.IsActive = true;
                    objNP.OrgID = Constant.OrgID;
                    NotfRecptID = BAL.NotfRecp_Insert(objNP);
                }
                else
                {
                    //Code for delete
                }
            }

            BindNotificationCcBcc();
        }
        #endregion

        #region[Bind EmployeeType ddl]
        private void BindEmpTypeddl()
        {
            ddlEmployeeType.DataSource = BAL.GetAllEmployeeType();
            ddlEmployeeType.DataTextField = "EmployeeType1";
            ddlEmployeeType.DataValueField = "EmployeeTypeID";
            ddlEmployeeType.DataBind();
            ddlEmployeeType.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        #endregion

        #region[CcBcc grid- RowdataBound]
        protected void gvNotificationCcBcc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton ibtnEmailCC = (ImageButton)e.Row.FindControl("ibtnEmailCC");
                HiddenField hfEmailCC = (HiddenField)e.Row.FindControl("hfEmailCC");
                ImageButton ibtnEmailBCC = (ImageButton)e.Row.FindControl("ibtnEmailBCC");
                HiddenField hfEmailBCC = (HiddenField)e.Row.FindControl("hfEmailBCC");
                ImageButton ibtnSMS = (ImageButton)e.Row.FindControl("ibtnSMS");
                HiddenField hfSMS = (HiddenField)e.Row.FindControl("hfSMS");

                if (hfEmailCC.Value == "True")
                {
                    ibtnEmailCC.ImageUrl = "../Images/H_active.png";
                    ibtnEmailCC.ToolTip = "Click to change";
                }
                else
                {
                    ibtnEmailCC.ImageUrl = "../Images/H_delete.png";
                    ibtnEmailCC.ToolTip = "Click to change";
                }

                if (hfEmailBCC.Value == "True")
                {
                    ibtnEmailBCC.ImageUrl = "../Images/H_active.png";
                    ibtnEmailBCC.ToolTip = "Click to change";
                }
                else
                {
                    ibtnEmailBCC.ImageUrl = "../Images/H_delete.png";
                    ibtnEmailBCC.ToolTip = "Click to change";
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
        protected void ibtnEmailCC_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibtn = sender as ImageButton;
            GridViewRow row = (GridViewRow)ibtn.NamingContainer;
            Int32 Id = Convert.ToInt32(gvNotificationCcBcc.DataKeys[row.RowIndex].Value);
            HiddenField hfEmailCC = (HiddenField)row.FindControl("hfEmailCC");
            Boolean IsCC = Convert.ToBoolean(hfEmailCC.Value == "True" ? "False" : "True");
            BAL.UpdateNotificationRecipient_CC(Id, IsCC);
            BindNotificationCcBcc();
        }

        protected void ibtnEmailBCC_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibtn = sender as ImageButton;
            GridViewRow row = (GridViewRow)ibtn.NamingContainer;
            Int32 Id = Convert.ToInt32(gvNotificationCcBcc.DataKeys[row.RowIndex].Value);
            HiddenField hfEmailBCC = (HiddenField)row.FindControl("hfEmailBCC");
            Boolean IsBCC = Convert.ToBoolean(hfEmailBCC.Value == "True" ? "False" : "True");
            BAL.UpdateNotificationRecipient_BCC(Id, IsBCC);
            BindNotificationCcBcc();
        }

        protected void ibtnSMS_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibtn = sender as ImageButton;
            GridViewRow row = (GridViewRow)ibtn.NamingContainer;
            Int32 Id = Convert.ToInt32(gvNotificationCcBcc.DataKeys[row.RowIndex].Value);
            HiddenField hfSMS = (HiddenField)row.FindControl("hfSMS");
            Boolean IsSMS = Convert.ToBoolean(hfSMS.Value == "True" ? "False" : "True");
            BAL.UpdateNotificationRecipient_SMS(Id, IsSMS);
            BindNotificationCcBcc();
        }
        #endregion

        #region[Delete notification preferences]
        protected void ibtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibtn = sender as ImageButton;
            GridViewRow row = (GridViewRow)ibtn.NamingContainer;
            Int32 Id = Convert.ToInt32(gvNotificationCcBcc.DataKeys[row.RowIndex].Value);
            BAL.DeleteCcBccPref(Id);
            BindNotificationCcBcc();
        }
        #endregion

    }
}