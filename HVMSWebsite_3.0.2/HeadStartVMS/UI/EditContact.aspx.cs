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

namespace METAOPTION.UI
{
    public partial class EditContact : System.Web.UI.Page
    {
       
        
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            long ContactId = Convert.ToInt64(Request["ID"]);
            if (grdContactDetails.Rows.Count > 0)
            {
                GridViewRow grdrow = (GridViewRow)grdContactDetails.Rows[0];
                TextBox txtFirstName = grdrow.FindControl("txtFirstName") as TextBox;
                TextBox txtMiddleName = grdrow.FindControl("txtMiddleName") as TextBox;
                TextBox txtLastName = grdrow.FindControl("txtLastName") as TextBox;
                TextBox txtOfficePhone = grdrow.FindControl("txtOfficePhone") as TextBox;
                TextBox txtCellPhone = grdrow.FindControl("txtCellPhone") as TextBox;
                TextBox txtEmail = grdrow.FindControl("txtEmail") as TextBox;
                DropDownList ddlContactType = grdrow.FindControl("ddlContactType") as DropDownList;
                DropDownList ddlJobTitle = grdrow.FindControl("ddlJobTitle") as DropDownList;
                // Create object of Contact Class
                Contact objContact = new Contact();
                objContact.ContactId = ContactId;
                if (!string.IsNullOrEmpty(ddlJobTitle.SelectedValue))
                objContact.JobTitleId = Convert.ToInt32(ddlJobTitle.SelectedValue);
                objContact.FirstName = txtFirstName.Text.Trim();
                objContact.MiddleName = txtMiddleName.Text.Trim();
                objContact.LastName = txtLastName.Text.Trim();
                if (!string.IsNullOrEmpty(ddlContactType.SelectedValue))
                objContact.ContactTypeId = Convert.ToInt32(ddlContactType.SelectedValue);
                objContact.OfficePhone = txtOfficePhone.Text.Trim();
                objContact.CellPhone = txtCellPhone.Text.Trim();
                objContact.Email = txtEmail.Text.Trim();
                objContact.ModifiedBy = Constant.UserId;
                CommonBAL objCommonBAL = new CommonBAL();// Create new object 
                objCommonBAL.UpdateContactDetails(objContact);
            }
            this.Page.ClientScript.RegisterStartupScript(typeof(EditContact), "closeThickBox", "self.parent.updated();", true);
        }
    }
    
}

