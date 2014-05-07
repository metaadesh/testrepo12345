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
using METAOPTION.DAL;
namespace METAOPTION.UI
{
    public partial class AddVendor : System.Web.UI.Page
    {
        Int32 totalRows = 0;

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
            #region[Check Permission]
            CheckPermission();
            #endregion

            //Load Country DropDownList
            if (!IsPostBack)
                BindCountry();

            SaveControlState();
            CreateTable();

        }

        #region[Check Permission method]
        /// <summary>
        /// Method that check permision
        /// of User.
        /// </summary>
        private void CheckPermission()
        {
            List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, "VENDOR");
            bool bTrue = true;
            if (!dict.Contains("VENDOR.ADD"))
            {
                bTrue = false;
            }
            //Redirect User to Common Page for InSufficient Rights
            if (!(dict.Contains("VENDOR.ADD") || bTrue))
                Response.Redirect("Permission.aspx?MSG=VENDOR.ADD");


        }
        #endregion

        /// <summary>
        /// Bind All States for Provided Country,bydefault it is 1 for U.S
        /// </summary>
        private void BindStates(int CountryId)
        {
            ddlState.DataSource = METAOPTION.BAL.Common.GetStateList(CountryId);
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("", "0"));
        }


        #region[Bind Country Drop Down and State Drop Down]
        /// <summary>
        /// Bind Country Drop Down and State Drop Down
        /// on the based of Country DropDown
        /// </summary>
        protected void BindCountry()
        {
            //Fill Country Drop Down
            BAL.Common bal = new BAL.Common();
            ddlCountry.DataSource = bal.GetCountryList();
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("", "0"));

            //Make U.S Country Selected ByDefault
            if (ddlCountry.Items.FindByValue(Constant.US_COUNTRYID) != null)
            {
                ddlCountry.SelectedValue = Constant.US_COUNTRYID;
                //By Default bind states of U.S
                BindStates(233);
            }
        }
        #endregion

        #region [Call method to add Vender Details]
        protected long AddVenderDetails()
        {
            long VendorId = 0;
            Vendor objVender = new Vendor();
            Address objAddress = new Address();
            if (Page.IsValid)
            {
                objVender.VendorName = txtName.Text.Trim();
                objVender.VendorDIN = txtDIN.Text.Trim();
                if (!string.IsNullOrEmpty(ddlCategory.SelectedValue))
                    objVender.VendorCategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
                objVender.TaxIdNumber = txtIdNumber.Text.Trim();
                if (!string.IsNullOrEmpty(ddlVenderType.SelectedValue))
                    objVender.VendorTypeId = Convert.ToInt32(ddlVenderType.SelectedValue);
                if (!string.IsNullOrEmpty(ddlPaymentTerms.SelectedValue))
                    objVender.PaymentTermId = Convert.ToInt32(ddlPaymentTerms.SelectedValue);
                //objVender.AccountingCode = txtAccountingCode.Text.Trim();
                objVender.Comments = txtComment.Text.Trim();
                if (!string.IsNullOrEmpty(Session["EmpId"].ToString()))
                    objVender.AddedBy = Constant.UserId;
                objVender.OrgID = Constant.OrgID; //Added by prem on 25-oct-2013

                // assign values for address properties
                objAddress.Street = txtStreet.Text.Trim();
                objAddress.Suite = txtSuite.Text.Trim();
                objAddress.City = txtCity.Text.Trim();

                if (!string.IsNullOrEmpty(ddlState.SelectedValue) && ddlState.SelectedValue != "0")
                    objAddress.StateId = Convert.ToInt32(ddlState.SelectedValue);

                if (!string.IsNullOrEmpty(ddlCountry.SelectedValue) && ddlCountry.SelectedValue != "0")
                    objAddress.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);

                objAddress.Zip = txtZip.Text.Trim();
                objAddress.Phone1 = txtPhone.Text.Trim();
                objAddress.Phone2 = txtOtherNumber.Text.Trim();
                objAddress.Fax = txtFax.Text.Trim();
                objAddress.Email1 = txtEmail.Text.Trim();
                //Added by Ashar on 26 Feb'2013
                objVender.TransExpenseCalculationMethod = Convert.ToInt16(ddlExpenseCalcMethod.SelectedValue);
                VendorId = BAL.VendorBAL.AddVenderDetails(objVender, objAddress);

                AddVendorContactDetails(VendorId);


            }
            return VendorId;

        }
        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            long VendorId = AddVenderDetails();
            Response.Redirect("viewVendor.aspx?EntityId=" + VendorId + "&type=3");
        }

        /// <summary>
        /// Add new row to contact detail section.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewRow_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["ContactDetail"] != null)
                AddNewRow();


        }
        #region[ Contat Detail ]
        #region[ Create Table Structure ]
        /// <summary>
        /// Create Table Structure
        /// </summary>
        private void CreateTable()
        {
            DataTable dTab = new DataTable("ContactDetail");
            if (Session["ContactDetail"] != null)
            {
                dTab = (DataTable)Session["ContactDetail"];
            }
            else
            {
                // Add columns
                dTab.Columns.Add("SNo", System.Type.GetType("System.Int32"));
                dTab.Columns.Add("JobTitleId", System.Type.GetType("System.String"));
                dTab.Columns.Add("FirstName", System.Type.GetType("System.String"));
                dTab.Columns.Add("MiddleName", System.Type.GetType("System.String"));
                dTab.Columns.Add("LastName", System.Type.GetType("System.String"));
                dTab.Columns.Add("ContactTypeId", System.Type.GetType("System.String"));
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
            Session["ContactDetail"] = dTab;
        }
        #endregion
        #region[ Add new row to Contact Detail table ]
        protected void AddNewRow()
        {
            DataTable dTab = new DataTable();
            if (Session["ContactDetail"] != null)
                dTab = (DataTable)Session["ContactDetail"];

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
        #region[ Save Control's values ]
        protected void SaveContactDetail()
        {
            DataTable dtContact = new DataTable();
            dtContact = (DataTable)Session["ContactDetail"];
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
            Session["ContactDetail"] = dtContact;
        }
        #endregion

        protected void REMOVE(object sender, CommandEventArgs e)
        {
            String rowId = e.CommandArgument.ToString();
            if (Session["ContactDetail"] != null)
            {
                DataTable dtContact = new DataTable();
                dtContact = (DataTable)Session["ContactDetail"];

                // if only one row, not allowed
                if (dtContact.Rows.Count == 1)
                    return;

                for (int Index = 0; Index < dtContact.Rows.Count; Index++)
                    if (dtContact.Rows[Index]["SNo"].ToString() == rowId)
                        dtContact.Rows[Index].Delete();
                Session["ContactDetail"] = dtContact;
                CreateTable();
            }
        }
        #endregion

        protected override object SaveControlState()
        {
            SaveContactDetail();
            return base.SaveControlState();

        }

        protected override void LoadViewState(object savedState)
        {
            SaveContactDetail();
            base.LoadViewState(savedState);

        }

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

                    dtContact = (DataTable)Session["ContactDetail"];
                    if (dtContact != null)
                    {
                        ddlJobTitle.SelectedValue = dtContact.Rows[e.Row.RowIndex]["JobTitleId"].ToString();
                        ddlContactType.SelectedValue = dtContact.Rows[e.Row.RowIndex]["ContactTypeId"].ToString();
                    }
                }

            }
        }

        #region[Add Contact Details of Vender]
        public void AddVendorContactDetails(long VenderId)
        {
            CommonBAL objCommonBAL = new CommonBAL();
            Contact objContact = new Contact();
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
                objContact.EntityId = VenderId;
                objContact.EntityTypeId = 3;
                if (!string.IsNullOrEmpty(ddlContactType.SelectedValue))
                    objContact.ContactTypeId = Convert.ToInt32(ddlContactType.SelectedValue);
                if (!string.IsNullOrEmpty(ddlJobTitle.SelectedValue))
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
        /// <summary>
        /// Reset all fields on click of Cancel Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Redirect to Same Page for Reset all fields
            Response.Redirect("AddVendor.aspx");
        }
        /// <summary>
        /// Handle selected index change event of dropdownlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlCountry.SelectedValue) && ddlCountry.SelectedValue != "0")
                BindStates(Convert.ToInt32(ddlCountry.SelectedValue));
            else
                ddlState.Items.Clear();

        }


    }


}
