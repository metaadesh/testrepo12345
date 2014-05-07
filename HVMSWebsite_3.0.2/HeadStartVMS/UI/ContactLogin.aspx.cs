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
    public partial class ContactLogin : System.Web.UI.Page
    {
        public Int64 ContactID,EntityID;
        public Int32 EntityTypeID;
        PreExpenseBAL PreExpBAL = new PreExpenseBAL();
        MakeUserBAL objUserBAL = new MakeUserBAL();
        String FullName = String.Empty;

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
                if (Request.QueryString.HasKeys())
                {
                    ContactID = Convert.ToInt64(Request.QueryString["ID"]);
                    List<Contact> contact = new List<Contact>();
                    contact = PreExpBAL.GetContactDetail(ContactID);
                    if (contact.Count > 0)
                    {
                        EntityID = Convert.ToInt64(contact[0].EntityId);
                        EntityTypeID = Convert.ToInt32(contact[0].EntityTypeId);
                        FullName = contact[0].FirstName + " " + contact[0].MiddleName + " " + contact[0].LastName;
                        txtFullName.Text = FullName;
                        txtUserName.Text = String.Empty;
                        txtPassword.Text = String.Empty;
                        ViewState["EntityID"] = EntityID;
                        ViewState["EntityTypeID"] = EntityTypeID;
                    }
                }
            }
        }

        #region[ Submit to Create User ]
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
                SecurityUser objSecurityUser = new SecurityUser();
                objSecurityUser.EntityID = Convert.ToInt64(ViewState["EntityID"]);
                objSecurityUser.EntityTypeID = Convert.ToInt32(ViewState["EntityTypeID"]);
                objSecurityUser.ContactId = Convert.ToInt64(Request.QueryString["ID"]);
                string PassEncripted = EncryptMD5.Encrypt(txtPassword.Text.Trim());
                objSecurityUser.UserName = txtUserName.Text.Trim();
                objSecurityUser.UserPassword = PassEncripted;
                objSecurityUser.DisplayName = txtDisplayName.Text.Trim();
                objSecurityUser.UserNote = String.Empty;
                objSecurityUser.AddedBy = Constant.UserId;
                objSecurityUser.IsActive = 1;
                Int32 result = objUserBAL.AssignLoginPassword(objSecurityUser);
                if (result == -1)
                {
                    this.lblError.Text = "User Already Exist";
                }
                else
                {
                    //if (Request["ReturnUrl"] != null)
                    //    Response.Redirect(String.Format("ContactLogin.aspx?ID={0}&Mode=EDIT&ReturnUrl={1}", Convert.ToString(result), Request["ReturnUrl"]));
                    //else
                    //    Response.Redirect(String.Format("ContactLogin.aspx?ID={0}&Mode=EDIT", Convert.ToString(result)));
                    Response.Redirect(Request["ReturnUrl"].ToString());
                }
        }
        #endregion
    }
}