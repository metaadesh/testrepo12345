using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;  
using System.Data;
using METAOPTION.BAL;
using System.Web.UI.HtmlControls;
using System.Web.Services;

namespace METAOPTION.UI
{
    public partial class Admin_EditIP : System.Web.UI.Page
    {
        Admin_ViewGroupListBAL objViewGroupListBAL = new Admin_ViewGroupListBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
            }
        }
        public void BindGrid()
        {
            if (Request.QueryString["IPPermissionID"] != null)
            {
                hdnIPPermissionId.Value = Convert.ToString(Request.QueryString["IPPermissionID"]);
                DataTable dtBind = new DataTable();
                String SecurityUID = String.IsNullOrEmpty(Convert.ToString(Session["SUID"])) ? "-1" : Convert.ToString(Session["SUID"]);
                dtBind = objViewGroupListBAL.GetManagePermissionByID(Convert.ToInt64(Request.QueryString["IPPermissionID"]), SecurityUID);
                if (dtBind.Rows.Count > 0)
                {
                    gvEditIPPermission.DataSource = dtBind;
                    gvEditIPPermission.DataBind();
                    txtIP.Text = Convert.ToString(dtBind.Rows[0]["IPAddress"]);
                    txtDescription.Text = Convert.ToString(dtBind.Rows[0]["Description"]);

                }
                else
                {
                    gvEditIPPermission.DataSource = dtBind;
                    gvEditIPPermission.DataBind();
                }
            }
        }

        protected void gvEditIPPermission_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField SystemIPRestriction = (HiddenField)e.Row.FindControl("hdSystemIPRestriction");
                HiddenField IPType = (HiddenField)e.Row.FindControl("hdnIPType");
                HtmlImage imgOn = (HtmlImage)e.Row.FindControl("imgOn");
                HtmlImage imgOff = (HtmlImage)e.Row.FindControl("imgOff");
                if (IPType.Value == "1")
                {

                    if (Convert.ToString(SystemIPRestriction.Value.ToLower()) == "0")
                    {
                        imgOff.Attributes.Add("style", "display:block");
                        imgOn.Attributes.Add("style", "display:none");
                    }
                    else
                    {
                        imgOff.Attributes.Add("style", "display:none");
                        imgOn.Attributes.Add("style", "display:block");
                    }
                }
                else
                {
                    if (Convert.ToString(SystemIPRestriction.Value.ToLower()) == "0")
                    {
                        imgOff.Attributes.Add("style", "display:block");
                        imgOn.Attributes.Add("style", "display:none");
                    }
                    else
                    {
                        imgOff.Attributes.Add("style", "display:none");
                        imgOn.Attributes.Add("style", "display:block");
                    }
                }
            }
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int ret = 0;
            if (Request.QueryString["Type"] != null)
            {
                ret = objViewGroupListBAL.UpdateIPPermission(Convert.ToInt64(Request.QueryString["IPPermissionID"]), txtIP.Text, Convert.ToInt32(Request.QueryString["Type"]), txtDescription.Text, Constant.UserId);

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "parent.HideModelpopup();", true);

        }

        protected void lnkDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Int32 IPUserID = 0;
                ImageButton lnkDelete = (ImageButton)sender;
                GridViewRow row = (GridViewRow)lnkDelete.NamingContainer;
                //Int32 IPUserID = Convert.ToInt32(gvEditIPPermission.DataKeys[((GridViewRow)(sender as ImageButton).NamingContainer).RowIndex].Values[0]);
                HiddenField hdnIPType = row.FindControl("hdnIPType") as HiddenField;
                HiddenField hdIPUserId = row.FindControl("hdIPUserId") as HiddenField;
                if (!string.IsNullOrEmpty(hdIPUserId.Value))
                    IPUserID = Convert.ToInt32(hdIPUserId.Value);

                int ret = objViewGroupListBAL.DeleteIPRestriction(Convert.ToInt32(Request.QueryString["IPPermissionID"]), IPUserID, Convert.ToInt32(hdnIPType.Value), Constant.UserId);
                BindGrid();

                if (gvEditIPPermission.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "parent.HideModelpopup();", true);
                }
            }
            catch (Exception ex) { }
        }


        [WebMethod]
        public static void UpdateAutoIPApproval(int Flag, Int32 IPUserID, Int32 IPPermissionId, Int32 IPType)
        {
            Admin_ViewGroupListBAL objViewGroupListBAL = new Admin_ViewGroupListBAL();

            objViewGroupListBAL.AutoIPRestriction(IPUserID, IPPermissionId, IPType, (Flag == 1) ? 1 : 0);

        }
    }
}