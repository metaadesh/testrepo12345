using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using METAOPTION.BAL;


namespace METAOPTION.UI
{
    public partial class Admin_AddOrganization : System.Web.UI.Page
    {
        Admin_OrganizationBAL ObjBAL = new Admin_OrganizationBAL();
        Int16 OrganizationID;
        string MODE = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request["org"] != null)
                this.OrganizationID = Convert.ToInt16(Request["org"]);
            if (Request["mode"] != null)
                this.MODE = Convert.ToString(Request["mode"]);

            if (!string.IsNullOrEmpty(MODE))
            {
                Button_OrgSubmit.Text = "Update";
                btn_back.Visible = true;
            }
            else
            {
                MODE = "NEW";
            }
            if (!IsPostBack)
            {
                if (OrganizationID != 0)
                {
                    DataTable dt = ObjBAL.getorginfo(OrganizationID);
                    txt_orgname.Text = dt.Rows[0]["orgName"].ToString();
                    txt_orgcode.Text = dt.Rows[0]["orgCode"].ToString();
                    txt_website.Text = dt.Rows[0]["orgWebsite"].ToString();
                    txt_address.Text = dt.Rows[0]["orgAddress"].ToString();
                    txt_phone.Text = dt.Rows[0]["orgPhone"].ToString();
                    txt_fax.Text = dt.Rows[0]["orgFax"].ToString();
                    txt_mail.Text = dt.Rows[0]["orgMail"].ToString();
                    if (!String.IsNullOrEmpty(dt.Rows[0]["orgLaneAutomation"].ToString()))
                    {
                        chkAllowLaneAutomation.Checked = Boolean.Parse(dt.Rows[0]["orgLaneAutomation"].ToString());
                    }
                    else
                    {
                        chkAllowLaneAutomation.Checked = false;
                    }

                    if (!String.IsNullOrEmpty(dt.Rows[0]["orgMAA"].ToString()))
                    {
                        chkAllowMAA.Checked = Boolean.Parse(dt.Rows[0]["orgMAA"].ToString());
                    }
                    else
                    {
                        chkAllowMAA.Checked = false;
                    }
                    txt_orgcode.Enabled = false;
                }
            }
        }

        private Int16 AddOrganization()
        {
            Int16 result = 0;
            string OrgName = "",
                OrgCode = "",
                Website = "",
                Address = "",
                Phone = "",
                Fax = "",
                Email = "";
            Int64 AddedBy = Constant.UserId;
            Int16 IsActive = 1;      // set Active Status of Any New Organization With help of sp 
            //named AddOrganization in table Organization

            if (!string.IsNullOrEmpty(txt_orgname.Text.Trim()))
                OrgName = Convert.ToString(txt_orgname.Text.Trim());

            if (!string.IsNullOrEmpty(txt_orgcode.Text.Trim()))
                OrgCode = (Convert.ToString(txt_orgcode.Text.Trim())).ToUpper();

            if (!string.IsNullOrEmpty(txt_website.Text.Trim()))
            {
                Website = Convert.ToString(txt_website.Text.Trim());

                if (!(Website.IndexOf("http://", StringComparison.CurrentCultureIgnoreCase) >= 0) && !(Website.IndexOf("https://", StringComparison.CurrentCultureIgnoreCase) >= 0))
                {
                    if (!(Website.IndexOf("www.", StringComparison.CurrentCultureIgnoreCase) >= 0))
                    {
                        Website = "www." + Website;
                    }
                    Website = "http://" + Website;

                }
                //else
                //{
                //    if (!(Website.IndexOf("www.", StringComparison.CurrentCultureIgnoreCase) >= 0))//Website.Contains("www.")
                //    {
                //         string mark="http://";
                //        int length = mark.Length;
                //        string flag = Website.Substring(0, length-1) + "www." + Website.Substring(length,Website.Length);
                //        Website = flag;
                //    }
                //}
            }
            if (!string.IsNullOrEmpty(txt_address.Text.Trim()))
                Address = Convert.ToString(txt_address.Text.Trim());

            if (!string.IsNullOrEmpty(txt_phone.Text.Trim()))
                Phone = Convert.ToString(txt_phone.Text.Trim());

            if (!string.IsNullOrEmpty(txt_fax.Text.Trim()))
                Fax = Convert.ToString(txt_fax.Text.ToString());

            if (!string.IsNullOrEmpty(txt_mail.Text.Trim()))
                Email = Convert.ToString(txt_mail.Text.Trim());

            String UserPassword = OrgCode + Constant.GetRandomNumber4Digit().ToString();
            UserPassword = METAOPTION.Common.EncryptMD5.Encrypt(UserPassword.Trim().ToLower());

            return result = ObjBAL.AddNewOrganization(OrgName, OrgCode, Website, Address, Phone, Fax, Email, AddedBy, IsActive, MODE, chkAllowLaneAutomation.Checked, chkAllowMAA.Checked, UserPassword);

        }

        protected void Btn_AddOrganization(object sender, EventArgs e)
        {
            Int16 OrgID = 0;
            OrgID = AddOrganization();
            if (OrgID == -1)
            {
                ShowMessage.Text = "<b>Organization code already exist</b>";
                ShowMessage.ForeColor = System.Drawing.Color.Red;
            }
            else if (OrgID > 0)
            {
                if (MODE.Equals("NEW"))
                {
                    ShowMessage.Text = "<b>New Organization created successfully</b>";
                    ShowMessage.ForeColor = System.Drawing.Color.Green;
                    txt_orgname.Text = string.Empty;
                    txt_orgcode.Text = string.Empty;
                    txt_phone.Text = string.Empty;
                    txt_website.Text = string.Empty;
                    txt_mail.Text = string.Empty;
                    txt_fax.Text = string.Empty;
                    txt_address.Text = string.Empty;

                    this.Context.Items["SummaryOrgID"] = OrgID.ToString();
                    this.Context.Items["IsNewOrganization"] = 1;
                    Server.Transfer("Admin_Summary.aspx");
                }
                else if (MODE.Equals("edit"))
                {
                    ShowMessage.Text = "<b>Organization updated successfully</b>";
                    ShowMessage.ForeColor = System.Drawing.Color.Green;
                }
            }
            else
            {
                ShowMessage.Text = "<b>Organization Not Created...ERROR</b>";
                ShowMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void btn_back_redirect(object sender, EventArgs e)
        {
            Response.Redirect("../UI/Admin_SearchOrganization.aspx");
        }
    }
}