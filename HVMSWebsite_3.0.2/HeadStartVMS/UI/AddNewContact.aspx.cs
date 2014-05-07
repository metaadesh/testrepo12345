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
    public partial class AddNewContact : System.Web.UI.Page
    {
        Int32 totalRows = 0;
        Contact objContact = new Contact();
        CommonBAL objCommonBAL = new CommonBAL();
        Common objCommon = new Common();
        MasterBAL objMasterBAL = new MasterBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            SaveControlState();
            CreateTable();
        }

        #region [Create DataTable for Contact Section]
        private void CreateTable()
        {
            DataTable dTab = new DataTable("DContDetail");
            if (Session["DContDetail"] != null)
            {
                dTab = (DataTable)Session["DContDetail"];
            }
            else
            {
                // Add columns
                dTab.Columns.Add("SNo", System.Type.GetType("System.Int32"));
                dTab.Columns.Add("JobTitleId", System.Type.GetType("System.String"));
                dTab.Columns.Add("FirstName", System.Type.GetType("System.String"));
                dTab.Columns.Add("MiddleName", System.Type.GetType("System.String"));
                dTab.Columns.Add("LastName", System.Type.GetType("System.String"));
                dTab.Columns.Add("ContactTypeId", System.Type.GetType("System.Int64"));
                dTab.Columns.Add("OfficeNo", System.Type.GetType("System.String"));
                dTab.Columns.Add("CellNo", System.Type.GetType("System.String"));
                dTab.Columns.Add("Email", System.Type.GetType("System.String"));

                // Add Default Row
                DataRow newContact = dTab.NewRow();
                newContact["SNo"] = 1;
                dTab.Rows.Add(newContact);
            }

            // Total Rows into table
            totalRows = dTab.Rows.Count;

            // Bind to grid
            gdvContactDetail.DataSource = dTab;

            gdvContactDetail.DataBind();

            // Store current table to session
            Session["DContDetail"] = dTab;
        }
        #endregion

        #region[Add New Row]
        protected void AddNewRow()
        {
            DataTable dTab = new DataTable();
            if (Session["DContDetail"] != null)
                dTab = (DataTable)Session["DContDetail"];

            Int32 newId = Convert.ToInt32(dTab.Rows[dTab.Rows.Count - 1]["SNo"]) + 1; ;
            // Add new row to Contact Detail Table
            DataRow newContact = dTab.NewRow();
            newContact["SNo"] = newId;
            dTab.Rows.Add(newContact);

            // Total Rows into table
            totalRows = dTab.Rows.Count;

            // Bind to grid
            gdvContactDetail.DataSource = dTab;
            gdvContactDetail.DataBind();
        }
        #endregion

        #region[Save ContactDetails]
        protected void SaveContactDetail()
        {
            DataTable dtContact = new DataTable();
            dtContact = (DataTable)Session["DContDetail"];
            for (int index = 0; index < gdvContactDetail.Rows.Count; index++)
            {
                // Set Job Title Value
                dtContact.Rows[index]["JobTitleId"] = (((DropDownList)gdvContactDetail.Rows[index].FindControl("ddlJobTitle"))).SelectedValue;
                // Set First Name Value
                dtContact.Rows[index]["FirstName"] = (((TextBox)gdvContactDetail.Rows[index].FindControl("txtFirstName"))).Text;
                // Set Middle Name Value
                dtContact.Rows[index]["MiddleName"] = (((TextBox)gdvContactDetail.Rows[index].FindControl("txtMiddleName"))).Text;
                // Set Last Name Value
                dtContact.Rows[index]["LastName"] = (((TextBox)gdvContactDetail.Rows[index].FindControl("txtLastName"))).Text;
                // Set Contact Tyep  Value
                dtContact.Rows[index]["ContactTypeId"] = (((DropDownList)gdvContactDetail.Rows[index].FindControl("ddlContactType"))).SelectedValue;
                // Set Cell Phone Value
                dtContact.Rows[index]["OfficeNo"] = (((TextBox)gdvContactDetail.Rows[index].FindControl("txtOfficeNo"))).Text;
                // Set Office Phone Value
                dtContact.Rows[index]["CellNo"] = (((TextBox)gdvContactDetail.Rows[index].FindControl("txtCellNo"))).Text;
                // Set Email Value
                dtContact.Rows[index]["Email"] = (((TextBox)gdvContactDetail.Rows[index].FindControl("txtEmail"))).Text;
            }
            Session["DContDetail"] = dtContact;
        }
        #endregion

        protected void gdvContactDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (totalRows > 0)
            {
                ImageButton ibtn; DataTable dtContact = new DataTable();
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (totalRows - 1 > e.Row.RowIndex)
                    {
                        ibtn = (ImageButton)e.Row.Cells[9].FindControl("btnNewRow");
                        ibtn.Visible = false;
                    }

                    if (totalRows > e.Row.RowIndex)
                    {
                        if (totalRows == 1)
                        {
                            ibtn = (ImageButton)e.Row.Cells[0].FindControl("btnRemove");
                            ibtn.Visible = false;
                        }
                    }

                    DropDownList ddlJobTitle = e.Row.FindControl("ddlJobTitle") as DropDownList;
                    DropDownList ddlContactType = e.Row.FindControl("ddlContactType") as DropDownList;
                    ddlJobTitle.DataSource = objJobTitle;
                    ddlJobTitle.DataBind();
                    ddlContactType.DataSource = objContactType;
                    ddlContactType.DataBind();

                    dtContact = (DataTable)Session["DContDetail"];
                    if (dtContact != null)
                    {
                        ddlJobTitle.SelectedValue = dtContact.Rows[e.Row.RowIndex]["JobTitleId"].ToString();
                        ddlContactType.SelectedValue = dtContact.Rows[e.Row.RowIndex]["ContactTypeId"].ToString();
                    }
                }

            }
        }

        protected void btnNewRow_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["DContDetail"] != null)
                AddNewRow();


        }

        protected override object SaveControlState()
        {
            SaveContactDetail();
            return base.SaveControlState();

        }

        protected void REMOVE(object sender, CommandEventArgs e)
        {
            String rowId = e.CommandArgument.ToString();
            if (Session["DContDetail"] != null)
            {
                DataTable dtContact = new DataTable();
                dtContact = (DataTable)Session["DContDetail"];

                // if only one row, not allowed
                if (dtContact.Rows.Count == 1)
                    return;

                for (int Index = 0; Index < dtContact.Rows.Count; Index++)
                    if (dtContact.Rows[Index]["SNo"].ToString() == rowId)
                        dtContact.Rows[Index].Delete();
                Session["DContDetail"] = dtContact;
                CreateTable();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["DContDetail"] = null;
            this.Page.ClientScript.RegisterStartupScript(typeof(AddNewContact), "closeThickBox", "self.parent.updated();", true);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddDealerContactDetails(Convert.ToInt64(Request["EntityId"]),Convert.ToInt32(Request["type"]));
            Session["DContDetail"] = null;
            this.Page.ClientScript.RegisterStartupScript(typeof(AddNewContact), "closeThickBox", "self.parent.updated();", true);
        }

        #region[Add Contact Details of Vender]
        /// <summary>
        /// add contact details of Dealer/Customer
        /// </summary>
        /// <param name="DealerId"></param>
        public void AddDealerContactDetails(long DealerId,Int32 EntityTypeId)
        {
            for (int index = 0; index < gdvContactDetail.Rows.Count; index++)
            {
                //findout control from gridview of contact section
                DropDownList ddlJobTitle = gdvContactDetail.Rows[index].FindControl("ddlJobTitle") as DropDownList;
                TextBox txtFirstName = gdvContactDetail.Rows[index].FindControl("txtFirstName") as TextBox;
                TextBox txtMiddleName = gdvContactDetail.Rows[index].FindControl("txtMiddleName") as TextBox;
                TextBox txtLastName = gdvContactDetail.Rows[index].FindControl("txtLastName") as TextBox;
                DropDownList ddlContactType = gdvContactDetail.Rows[index].FindControl("ddlContactType") as DropDownList;
                TextBox txtOfficeNo = gdvContactDetail.Rows[index].FindControl("txtOfficeNo") as TextBox;
                TextBox txtCellNo = gdvContactDetail.Rows[index].FindControl("txtCellNo") as TextBox;
                TextBox txtEmail = gdvContactDetail.Rows[index].FindControl("txtEmail") as TextBox;

                // assign contact table properties according to values of controls
                objContact.EntityId = DealerId;
                objContact.EntityTypeId = EntityTypeId;
                if (!string.IsNullOrEmpty(ddlContactType.SelectedValue))
                objContact.ContactTypeId = Convert.ToInt32(ddlContactType.SelectedValue);
                objContact.JobTitleId = Convert.ToInt32(ddlJobTitle.SelectedValue);
                objContact.FirstName = txtFirstName.Text.Trim();
                objContact.MiddleName = txtMiddleName.Text.Trim();
                objContact.LastName = txtLastName.Text.Trim();
                objContact.OfficePhone = txtOfficeNo.Text.Trim();
                objContact.CellPhone = txtCellNo.Text.Trim();
                objContact.Email = txtEmail.Text.Trim();
                if (!string.IsNullOrEmpty(Session["EmpId"].ToString()))
                objContact.AddedBy = Constant.UserId;


                //save the Contact details of a row.

                objCommonBAL.AddContactDetails(objContact);
               
               
                
            }
        }
        #endregion

      

    }
}
