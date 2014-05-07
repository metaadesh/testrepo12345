using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using METAOPTION.BAL;
using System.Web.Security;

namespace METAOPTION.UI
{
    public partial class CompanyContacts : System.Web.UI.Page
    {
        public string EntityId = string.Empty;
        public string type = string.Empty;
        public const string PAGERIGHT_DELETE = "VENDOR.DELETE";
        public const string PAGE = "VENDOR";

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

        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            {
                if (Convert.ToString(Session["LoginEntityTypeID"]) == "1")

                if (Session["empId"] != null && Session["LoginEntityTypeID"] != null)
                {
                    EntityId =Convert.ToString(Session["empId"]);
                    type = Convert.ToString(Session["LoginEntityTypeID"]);
                }
                       
                BindContactType();
                BindJobTitle();
                BindGrid(0);
               

            }

        }
        protected void BindContactType()
        {
            List<ContactType_ver211Result> list = METAOPTION.BAL.CommonBAL.GetContactType();
            ddlContactType.DataSource = list;
            ddlContactType.DataValueField = "ContactTypeId";
            ddlContactType.DataTextField = "ContactType";
            ddlContactType.DataBind();
            ddlContactType.Items.Insert(0, new ListItem("All", "0"));
        }
        protected void BindJobTitle()
        {
            List<JobTile_ver211Result> list = METAOPTION.BAL.CommonBAL.GetJobTitle();
            ddlJobTitle.DataSource = list;
            ddlJobTitle.DataValueField = "JobTitleId";
            ddlJobTitle.DataTextField = "JobTitle";
            ddlJobTitle.DataBind();
            ddlJobTitle.Items.Insert(0, new ListItem("All", "0"));
        }
        protected void BindGrid(Int32 pageIndex)
        {
            String CellNo = String.IsNullOrEmpty(txtCellPhone.Text) ? "-1" : txtCellPhone.Text;
            Int32 Jtiltle = ddlJobTitle.SelectedValue == "0" ? -1 : Convert.ToInt32(ddlJobTitle.SelectedValue);
            String Usrname = String.IsNullOrEmpty(txtUserName.Text) ? "-1" : txtUserName.Text;
            List<GetEntityContactDetails_ver211Result> LstContactDetails = METAOPTION.BAL.CommonBAL.GetContactDetail_BySecurityUserID(Convert.ToInt32(Session["empId"]), Convert.ToInt32(Session["LoginEntityTypeID"]), Jtiltle, Usrname, Convert.ToInt32(ddlContactType.SelectedValue), CellNo);
            grdContactDetails.DataSource = LstContactDetails;
            grdContactDetails.DataBind();

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid(0);
           
        }

       

        protected void grdContactDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkPwd = (HyperLink)e.Row.FindControl("hlkcontact");
                HiddenField hfSecurityUserID = (HiddenField)e.Row.FindControl("hfSecurityUserID");
                HiddenField hfUserName = (HiddenField)e.Row.FindControl("hfUserName");
                HiddenField hfContactID = (HiddenField)e.Row.FindControl("hfContactID");
                ImageButton ImgBtn = (ImageButton)e.Row.FindControl("imgbtnDelete");
               if (!String.IsNullOrEmpty(hfSecurityUserID.Value))  //Change Pwd
                {
                    hlnkPwd.NavigateUrl = String.Format("AdminChangePassword.aspx?Code={0}+&UserName={1}&ReturnUrl={2}", hfSecurityUserID.Value, hfUserName.Value, HttpUtility.UrlEncode(Request.Url.AbsoluteUri).ToString());
                }
                else  //Add Pwd
                {
                    hlnkPwd.NavigateUrl = String.Format("ContactLogin.aspx?ID={0}&Mode=Ins&ReturnUrl={1}", hfContactID.Value, HttpUtility.UrlEncode(Request.Url.AbsoluteUri).ToString());
                }
               if (Convert.ToString(Session["LoginEntityTypeID"]) == "3" || Convert.ToString(Session["LoginEntityTypeID"]) == "1")
                {
                    ImgBtn.Enabled = false;
                    hlnkPwd.Enabled = false;
                }

            }
        }
        protected void grdContactDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            CommonBAL objCommonBAL = new CommonBAL();
            long ContactId = Convert.ToInt64(grdContactDetails.DataKeys[e.RowIndex].Value);
            objCommonBAL.DeleteDealerContact(ContactId);
            BindGrid(0);
        }



    }
}