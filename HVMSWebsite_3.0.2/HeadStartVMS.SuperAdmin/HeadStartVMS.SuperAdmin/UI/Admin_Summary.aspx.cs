using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using METAOPTION.BAL;
using System.Data;

namespace METAOPTION.UI
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (this.Context.Items["SummaryOrgID"] != null)
                {
                    Int16 OrgID = -1;
                    String strOrgID = "";
                    strOrgID = this.Context.Items["SummaryOrgID"].ToString();
                    OrgID = Int16.Parse(strOrgID);

                    if (this.Context.Items["IsNewOrganization"] != null)
                    {
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        lblMessage.Visible = false;
                    }
                    ShowSummary(OrgID);
                }
                else
                {
                    Response.Redirect("Admin_Home.aspx");
                }
            }
        }

        private void ShowSummary(Int16 OrgID)
        {
            Admin_OrganizationBAL objBAL = new Admin_OrganizationBAL();
            DataTable dt = objBAL.OrganizationSummary(OrgID);
            if (dt.Rows.Count > 0)
            {
                tblNoSummary.Visible = false;
                tblSummary.Visible = true;
                DataRow row;
                row = dt.Rows[0];
                lblOrgName.Text = row["OrgName"].ToString();
                lblOrgCode.Text = row["OrgCode"].ToString();
                lblOrgWebsite.Text = row["OrgWebsite"].ToString();
                lblOrgAddress.Text = row["OrgAddress"].ToString();
                lblOrgPhone.Text = row["OrgPhone"].ToString();
                lblOrgFax.Text = row["OrgFax"].ToString();
                lblOrgEmail.Text = row["OrgEmail"].ToString();
                lblSystemName.Text = row["SystemName"].ToString();
                lblAdminName.Text = row["AdminName"].ToString();
                lblLoginUserName.Text = row["UserName"].ToString();
                if (!String.IsNullOrEmpty(row["UserPassword"].ToString()))
                {
                    lblLoginPassword.Text = METAOPTION.Common.EncryptMD5.Decrypt(row["UserPassword"].ToString());
                }
                else
                {
                    lblLoginPassword.Text = "";
                }
                lblUserDefaultGroups.Text = row["GroupNames"].ToString();
            }
            else
            {
                tblNoSummary.Visible = true;
                tblSummary.Visible = false;
            }

        }
    }
}