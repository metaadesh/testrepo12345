using System;
using System.Collections;
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

namespace METAOPTION.UserControls
{
    public partial class Contact : System.Web.UI.UserControl
    {
        public string EntityId = string.Empty;
        public string type = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["EntityId"] != null && Request["type"] != null)
            {
                EntityId = Request["EntityId"];
                type = Request["type"];
            }
            if (!IsPostBack)
                BindContactDetails();
        }

        protected void BindContactDetails()
        {
            grdContactDetails.DataSource = objContactDetails;
            grdContactDetails.DataBind();
        }
        protected void grdContactDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            CommonBAL objCommonBAL = new CommonBAL();
            long ContactId = Convert.ToInt64(grdContactDetails.DataKeys[e.RowIndex].Value);
            objCommonBAL.DeleteDealerContact(ContactId);
            BindContactDetails();
        }

        protected void grdContactDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkPwd = (HyperLink)e.Row.FindControl("hlkcontact");
                HiddenField hfSecurityUserID = (HiddenField)e.Row.FindControl("hfSecurityUserID");
                HiddenField hfUserName = (HiddenField)e.Row.FindControl("hfUserName");
                HiddenField hfContactID = (HiddenField)e.Row.FindControl("hfContactID");
                if (!String.IsNullOrEmpty(hfSecurityUserID.Value))  //Change Pwd
                {
                    hlnkPwd.NavigateUrl = String.Format("~/UI/AdminChangePassword.aspx?Code={0}+&UserName={1}&ReturnUrl={2}", hfSecurityUserID.Value, hfUserName.Value, HttpUtility.UrlEncode(Request.Url.AbsoluteUri).ToString());
                }
                else  //Add Pwd
                {
                    hlnkPwd.NavigateUrl = String.Format("~/UI/ContactLogin.aspx?ID={0}&Mode=Ins&ReturnUrl={1}", hfContactID.Value, HttpUtility.UrlEncode(Request.Url.AbsoluteUri).ToString());
                }

            }
        }
    }
}