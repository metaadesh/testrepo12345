using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using METAOPTION.BAL;

namespace METAOPTION.UI
{
    public partial class ManagePermissionAdd : System.Web.UI.Page
    {
        ViewGroupListBAL objViewGroupListBAL = new ViewGroupListBAL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (rdbIPType.SelectedValue == "2")
            {
                AddPermissionIP();
                txtIP.Text = "";
                txtDescription.Text = "";
            }
            else
            {
                AddUserPermissionIP();
                txtIP.Text = "";
                txtDescription.Text = "";
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "parent.HideAddModelpopup();", true);
        }

        public void AddPermissionIP()
        {
            int ret = 0;
            DataTable dtSecurityID = new DataTable();
            dtSecurityID = objViewGroupListBAL.GetEntityIdbyIPAddredd(txtIP.Text, 2, Constant.OrgID);
            if (dtSecurityID.Rows.Count > 0)
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('This IP Address already exists!');", true);
            else
                ret = objViewGroupListBAL.SaveIPPermission(txtIP.Text, Convert.ToInt32(rdbIPType.SelectedValue), txtDescription.Text, Constant.UserId, Constant.OrgID);

        }

        public void AddUserPermissionIP()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EntityId");

            if (!string.IsNullOrEmpty(hdnEntity.Value))
            {
                string[] strEntityId = hdnEntitiesId.Value.Split(',');
                for (int i = 0; i < strEntityId.Length; i++)
                {
                    DataRow row = dt.NewRow();
                    row["EntityId"] = strEntityId[i];
                    dt.Rows.Add(row);
                    dt.AcceptChanges();
                }
            }

            DataTable dtSecurityID = new DataTable();
            dtSecurityID = objViewGroupListBAL.GetEntityIdbyIPAddredd(txtIP.Text, 1, Constant.OrgID);
            if (dtSecurityID.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dtSecurityID.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            DataRow row = dt.Rows[j];
                            if ((Convert.ToString(dtSecurityID.Rows[i]["SecurityUserID"])) == (Convert.ToString(row["EntityId"])))
                            {
                                row.Delete();
                                dt.AcceptChanges();
                            }
                        }

                    }
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dtSecurityID.Rows[0]["Description"])))
                {
                    if (!string.IsNullOrEmpty(txtDescription.Text))
                        txtDescription.Text = txtDescription.Text + ", " + Convert.ToString(dtSecurityID.Rows[0]["Description"]);
                    else
                        txtDescription.Text = Convert.ToString(dtSecurityID.Rows[0]["Description"]);
                }
            }
            if (dt.Rows.Count > 0)
            {
                int ret = objViewGroupListBAL.AddUserPermissionIP(txtIP.Text, Convert.ToInt32(rdbIPType.SelectedValue), txtDescription.Text, Convert.ToInt32(ddlEntityType.SelectedValue), dt, Constant.UserId, Constant.OrgID);
                dlEntity.Items.Clear();
                dlEntity.Text = string.Empty;
                ddlEntityType.SelectedValue = "-1";
            }
        }

        protected void ddlEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillEntityName();
        }

        public void FillEntityName()
        {
            dlEntity.Items.Clear();
            dlEntity.Text = string.Empty;
            string EntityTypeId = ddlEntityType.SelectedValue;
            if (!string.IsNullOrEmpty(EntityTypeId))
            {
                DataTable dtEntity = new DataTable();
                dtEntity = objViewGroupListBAL.GetEntity(Convert.ToInt32(EntityTypeId), Constant.OrgID);
                dlEntity.DataSource = dtEntity;
                dlEntity.DataTextField = "EntityName";
                dlEntity.DataValueField = "SecurityUserID";
                dlEntity.DataBind();
            }
            else
            {
                dlEntity.Items.Clear();
                dlEntity.Text = string.Empty;
            }
        }

        protected void rdbIPType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbIPType.SelectedValue == "1")
            {
                dlEntity.Enabled = true;
                ddlEntityType.Enabled = true;
                rfv1.Enabled = true;
                RequiredFieldValidator2.Enabled = true;

            }
            else
            {
                dlEntity.Items.Clear();
                dlEntity.Text = string.Empty;
                dlEntity.Enabled = false;
                ddlEntityType.SelectedValue = "-1";
                ddlEntityType.Enabled = false;
                rfv1.Enabled = false;
                RequiredFieldValidator2.Enabled = false;

            }

        }
    }
}