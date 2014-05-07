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

namespace METAOPTION.UI
{
    public partial class GroupList : System.Web.UI.Page
    {
        
        ViewGroupListBAL objViewGroupListBAL = new ViewGroupListBAL();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            try{
            if (!String.IsNullOrEmpty(Convert.ToString(Session["MasterPage"])))
                this.MasterPageFile = Convert.ToString(Session["MasterPage"]);
            else
                Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(Request.Url.ToString()));
        } catch (Exception ex)
            {
                Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(Request.Url.ToString()));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

           CheckPermission();

            if (!IsPostBack)
            {
                BindGroupList();
            }
        }
        #region [Bind Group List]
        protected void BindGroupList()
        {
          GrdGroup.DataSource = objViewGroupListBAL.GetAllGroups(Constant.OrgID);
          GrdGroup.DataBind();
        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddGroup.aspx?mode=Ins&ReturnUrl=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
        }

        protected void GrdGroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string grpId = GrdGroup.DataKeys[e.RowIndex].Value.ToString();
            
            if (Constant.UserId != -1)
            {
               BAL.AddGroupBAL.Group_Delete(Convert.ToInt64(grpId), Constant.UserId);
               BindGroupList();
            }
        }
        #region[ Page Permission ]
        private void CheckPermission()
        {
           List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, "SECURITY");
           if (!dict.Contains("GROUP.ADD"))
              this.btnSubmit.Visible = false;

           if (!dict.Contains("GROUP.EDIT"))
              this.GrdGroup.Columns[0].Visible = false;

           if (!dict.Contains("GROUP.DELETE"))
              this.GrdGroup.Columns[2].Visible = false;

           if (!(dict.Contains("GROUP.ADD") || dict.Contains("GROUP.EDIT") || dict.Contains("GROUP.DELETE")))
              Response.Redirect("Permission.aspx?MSG=SECURITY.GROUP.ADD OR SECURITY.GROUP.EDIT OR SECURITY.GROUP.VIEW");
        }
        #endregion

        #region[Paging]
        protected void GrdGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdGroup.PageIndex = e.NewPageIndex;
            BindGroupList();
        }
        #endregion
    }
}
