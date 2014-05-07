using System;
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
   public partial class AddGroup : System.Web.UI.Page
   {

      AddGroupBAL objAddGroupBAL = new AddGroupBAL();
      ViewGroupListBAL objViewGroupListBAL = new ViewGroupListBAL();
      long _groupId = -1;
      String Mode = string.Empty;

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

      #region [ PageLoad ]
      protected void Page_Load(object sender, EventArgs e)
      {
         // Keep this on top to first get the Group Id
         try
         {
            if (Request["Mode"] != null)
               this.Mode = Convert.ToString(Request["Mode"]).ToUpper();

            if (Request["Code"] != null)
               this._groupId = Convert.ToInt64(Request["Code"]);
         }
         catch { }

         if (!IsPostBack)
            CheckOperation();


      }
      #endregion

      #region [Check operation for this page]
      /// <summary>
      /// check which operation is going on to operate
      /// </summary>
      protected void CheckOperation()
      {
         if (this.Mode == "VIEW")
         {
            BindGroupDetails(); BindGroupRights();
            txtGName.Enabled = false; txtGDesc.Enabled = false;
            this.btnAddRight.Visible = false;
            this.GridView1.Columns[0].Visible = false;
            btnSubmit.Visible = false; btnUpdate.Visible = false; btnCancel.Text = "   Back   ";
         }
         if (this.Mode == "EDIT")
         {
            BindGroupDetails(); BindGroupRights();
            btnSubmit.Visible = false; btnCancel.Text = "  Cancel  ";
            this.btnAddRight.Visible = true;
         }
         if (this.Mode == "INS")
         {
            btnUpdate.Visible = false; btnCancel.Text = "  Cancel  ";
            fsetRights.Visible = false;
         }
      }
      #endregion

      #region [ Submit Click ]
      protected void btnSubmit_Click(object sender, EventArgs e)
      {
         if (Page.IsValid)
         {
            SecurityGroup objSecGroup = new SecurityGroup();
            objSecGroup.GroupName = txtGName.Text.Trim();
            objSecGroup.GroupDesc = txtGDesc.Text.Trim();
            objSecGroup.AddedBy = Constant.UserId;

            //Mark whether the group is general or for a specific organization
            //IsSystemDefault: true-General group/false-organization specific
            objSecGroup.IsSystemDefault = false;
            long result = objAddGroupBAL.AddGroupDetails(objSecGroup, Constant.OrgID);
            if (result > 0)
            {
               if (Request["ReturnUrl"] != null)
                  Response.Redirect(string.Format("AddGroup.aspx?Code={0}&Mode=Edit&ReturnUrl={1}", Convert.ToString(result), Request["ReturnUrl"]));
               else
                  Response.Redirect(string.Format("AddGroup.aspx?Code={0}&Mode=Edit", Convert.ToString(result)));
            }
         }
      }
      #endregion

      #region [ Update Click ]
      protected void btnUpdate_Click(object sender, EventArgs e)
      {
         UpdateGroup_ByGroupId();
         if (Request["ReturnUrl"] != null)
            Response.Redirect(Request["ReturnUrl"]);
      }
      #endregion



      #region [ Add Rights ]
      protected void btnAddRight_Click(object sender, EventArgs e)
      {
         SearchRights();
         this.mpeOpenRights.Show();
      }
      #endregion

      #region [Bind Group Details]
      /// <summary>
      /// region bind group details
      /// by groupId
      /// </summary>
      protected void BindGroupDetails()
      {
         if (this._groupId > 0)
         {
            Dictionary<string, string> objDic = objViewGroupListBAL.GetGroupDetails_ByGroupId(this._groupId);
            txtGName.Text = objDic["GName"];
            txtGDesc.Text = objDic["GDesc"];
         }
      }
      #endregion

      #region [Update Group]
      /// <summary>
      /// update group by the group ID
      /// </summary>
      protected void UpdateGroup_ByGroupId()
      {
         if (Page.IsValid)
         {
            if (this._groupId > 0)
            {
               SecurityGroup objSecGroup = new SecurityGroup();
               objSecGroup.SecurityGroupId = this._groupId;
               objSecGroup.GroupName = txtGName.Text.Trim();
               objSecGroup.GroupDesc = txtGDesc.Text.Trim();
               if (Session["EmpId"] != null)
                  objSecGroup.ModifiedBy = Constant.UserId;

               int result = objAddGroupBAL.UpdateGroup(objSecGroup);

               if (result == 0)
               {
                  if (Request["ReturnUrl"] != null)
                     Response.Redirect(string.Format("AddGroup.aspx?Code={0}&Mode=view&ReturnUrl={1}", Convert.ToString(this._groupId), Request["ReturnUrl"]));
                  else
                     Response.Redirect(string.Format("AddGroup.aspx?Code={0}&Mode=view", Convert.ToString(this._groupId)));
               }
            }
         }
      }
      #endregion

      #region [Bind Group Rights]
      protected void BindGroupRights()
      {
         GridView1.DataSource = objAddGroupBAL.GetGroupWithRights(this._groupId);
         GridView1.DataBind();
      }
      #endregion

      #region[ Delete Group ]
      protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
      {
         if (e.CommandName == "DeleteRight")
         {
            SecurityGroupRight objSecGrpRight = new SecurityGroupRight();
            objSecGrpRight.SecurityGroupRightId = Convert.ToInt64(e.CommandArgument);
            int result = objAddGroupBAL.DeleteRightFromGroup(objSecGrpRight);
            BindGroupRights();
         }
      }
      #endregion

      #region[ Add right by clicking OK Button ]
      protected void ibtnOk_Click(object sender, ImageClickEventArgs e)
      {
         ImageButton img = (ImageButton)sender;
         GridViewRow gvRow = (GridViewRow)img.NamingContainer;
         long rightId = Convert.ToInt64(gvRights.DataKeys[gvRow.RowIndex].Value);

         long result = AddGroupBAL.AddRightInGroup(this._groupId, rightId);
         if (result > 0)
         {
            BindGroupRights();
            this.mpeOpenRights.Hide();
         }

      }
      #endregion

      #region[ Search Rights ]
      protected void SearchRights()
      {
         this.gvRights.DataSource = BAL.AddGroupBAL.GetRightsName(this.txtRightName.Text.Trim());
         this.gvRights.DataBind();
         this.mpeOpenRights.Show();
      }
      #endregion

      #region[ Paging of Grids ]
      protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
      {
         this.GridView1.PageIndex = e.NewPageIndex;
         BindGroupRights();
      }

      protected void gvRights_PageIndexChanging(object sender, GridViewPageEventArgs e)
      {
         this.gvRights.PageIndex = e.NewPageIndex;
         SearchRights();
         this.mpeOpenRights.Show();
      }
      #endregion
      #region [ Cancel Click ]
      protected void btnCancel_Click(object sender, EventArgs e)
      {
         if (Request["ReturnUrl"] != null)
            Response.Redirect(Request["ReturnUrl"]);
      }
      #endregion

      protected void btnSearchRight_Click(object sender, EventArgs e)
      {
         SearchRights();
      }

   }
}