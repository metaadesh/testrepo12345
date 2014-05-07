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
using METAOPTION;
using METAOPTION.BAL;
using System.Text;
using System.IO;

namespace METAOPTION.UI
{
    public partial class Admin_AddSystem : System.Web.UI.Page
    {
        Admin_SystemBAL objBAL = new Admin_SystemBAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindOrganizations();
                try
                {
                    if (Request.QueryString["Mode"] != null && Request.QueryString["Code"] != null && Request.QueryString["Mode"].ToString() == "edit")
                    {
                        ChangeEditMode(Request.QueryString["Code"]);
                    }
                }
                catch { }
            }
        }

        private void ChangeEditMode(String ID)
        {
            Int32 SystemID = Int32.Parse(ID);
            DataTable dt = objBAL.GetSystemDetails(SystemID);
            if (dt.Rows.Count > 0)
            {
                ddlOrganization.SelectedValue = dt.Rows[0]["OrgID"].ToString();
                txtSystemName.Text = dt.Rows[0]["SystemName"].ToString();
                chkSystemActiveStatus.Checked = (dt.Rows[0]["IsActive"].ToString() == "1" ? true : false);
                chkIsPeachTree.Checked = Boolean.Parse(dt.Rows[0]["PeachTree"].ToString());

                ddlOrganization.Enabled = false;
                btnAdd.Text = "Update";
                ViewState["mode"] = "edit";
                ViewState["SystemID"] = SystemID;

                tblSystemLogo.Visible = true;
                if (!DBNull.Value.Equals(dt.Rows[0]["ImagePath"]))
                {
                    SetLogo(dt.Rows[0]["ImagePath"].ToString().Trim());
                }
                else
                {
                    SetLogo("");
                }
            }
        }

        private void SetLogo(String ImageUrl)
        {
            try
            {
                lnkRemoveLogo.Visible = true;
                if (ImageUrl != "")
                {
                    String physicalPath = Server.MapPath("/Images/Logos");
                    if (File.Exists(physicalPath + "\\" + ImageUrl))
                    {
                        imgSystemLogo.ImageUrl = "~/Images/Logos/" + ImageUrl + "?" + DateTime.Now.Ticks;
                    }
                    else
                    {
                        imgSystemLogo.ImageUrl = "~/Images/Logos/NOLOGO.png" + "?" + DateTime.Now.Ticks;
                        lnkRemoveLogo.Visible = false;
                    }
                }
                else
                {
                    imgSystemLogo.ImageUrl = "~/Images/Logos/NOLOGO.png" + "?" + DateTime.Now.Ticks;
                    lnkRemoveLogo.Visible = false;
                }
            }
            catch { }
        }

        private void BindOrganizations()
        {
            BAL.Admin_OrganizationBAL obj = new BAL.Admin_OrganizationBAL();
            ddlOrganization.DataSource = obj.GetOrganizationsList();
            ddlOrganization.DataTextField = "OrgName";
            ddlOrganization.DataValueField = "OrgID";
            ddlOrganization.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            if (Request.QueryString["ReturnUrl"] != null)
            {
                Response.Redirect(Request["ReturnUrl"].ToString());
            }
            else
            {
                //if (Request["ReturnUrl"] != null)
                //{
                //    System.Web.HttpContext.Current.Response.Redirect(Request["ReturnUrl"]);
                //}
                //else
                //{
                System.Web.HttpContext.Current.Response.Redirect("Admin_Home.aspx");
               // }
            }
        }

        private void ClearControls()
        {
            txtSystemName.Text = "";
            chkSystemActiveStatus.Checked = false;
            chkIsPeachTree.Checked = false;
            
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Int16 OrgID = Int16.Parse(ddlOrganization.SelectedValue);
            String ImageName = "";

            try
            {
                if (fupSystemLogo.HasFile)
                {
                    ImageName = fupSystemLogo.PostedFile.FileName;
                    String physicalPath = Server.MapPath("/Images/Logos");
                retry:
                    if (File.Exists(physicalPath + "\\" + ImageName) == false)
                    {
                        if (fupSystemLogo.PostedFile.ContentLength < 512000)
                        {
                            fupSystemLogo.PostedFile.SaveAs(physicalPath + "\\" + ImageName);
                        }
                        else
                        {
                            lblMessage.Text = "image size can't be more than 500KB !";
                            ImageName = "";
                            return;
                        }
                    }
                    else
                    {
                        ImageName = DateTime.Now.Ticks.ToString() + "_" + ImageName;
                        goto retry;
                    }
                }
            }
            catch
            {
                ImageName = "";
            }

            Int32 SystemID = 0;
            if (ViewState["mode"] != null && ViewState["mode"].ToString() == "edit")
            {
                SystemID = Int32.Parse(ViewState["SystemID"].ToString());
                Int32 rowEffected = 0;
                rowEffected = objBAL.UpdateSystem(txtSystemName.Text.Trim(), ImageName, chkSystemActiveStatus.Checked, chkIsPeachTree.Checked, SystemID);
                if (SystemID != 0)
                {
                    lblMessage.Text = "System details updated successfully";
                    if (ImageName != "")
                    {
                        SetLogo(ImageName);
                    }
                }
            }
            else
            {
                SystemID = objBAL.AddNewSystem(OrgID, txtSystemName.Text.Trim(), ImageName, chkSystemActiveStatus.Checked, chkIsPeachTree.Checked);
                if (SystemID != 0)
                {
                    lblMessage.Text = "System added successfully";
                    ClearControls();
                }
            }
        }

        protected void lnkRemoveLogo_Click(object sender, EventArgs e)
        {
            if (ViewState["mode"] != null)
            {
                if (ViewState["mode"].ToString() == "edit")
                {
                    Int32 SystemID = Int32.Parse(ViewState["SystemID"].ToString());
                    objBAL.UpdateSystemImagePath(SystemID, null);
                    SetLogo("");
                }
            }
        }
    }
}
