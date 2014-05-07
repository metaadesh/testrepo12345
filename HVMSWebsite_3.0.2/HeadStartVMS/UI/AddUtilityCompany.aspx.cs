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
    public partial class AddUtilityCompany : System.Web.UI.Page
    {

        // #region Public Variables
        long utilityId = 0;


        Int32 totalRows = 0;
        Contact objContact = new Contact();
        CommonBAL objCommonBAL = new CommonBAL();

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

            //Load Country dropdownlist
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
            List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, "UTILITYCOMPANY");
            bool bTrue = true;
            if (!dict.Contains("UTILITYCOMPANY.ADD"))
            {
                bTrue = false;
            }
            //Redirect User to Common Page for InSufficient Rights
            if (!(dict.Contains("UTILITYCOMPANY.ADD") || bTrue))
                Response.Redirect("Permission.aspx?MSG=UTILITYCOMPANY.ADD");


        }
        #endregion

        /// <summary>
        /// Add new row to contact detail section.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewRow_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["ContDetail"] != null)
                AddNewRow();
        }

        #region[ Contact Detail ]
        #region[ Create Table Structure ]
        /// <summary>
        /// Create Table Structure
        /// </summary>
        private void CreateTable()
        {
            DataTable dTab = new DataTable("ContactDetail");
            if (Session["ContDetail"] != null)
            {
                dTab = (DataTable)Session["ContDetail"];
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
            Session["ContDetail"] = dTab;
        }
        #endregion
        #region[ Add new row to Contact Detail table ]
        protected void AddNewRow()
        {
            DataTable dTab = new DataTable();
            if (Session["ContDetail"] != null)
                dTab = (DataTable)Session["ContDetail"];

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
            dtContact = (DataTable)Session["ContDetail"];
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
            Session["ContDetail"] = dtContact;
        }
        #endregion

        protected void REMOVE(object sender, CommandEventArgs e)
        {
            String rowId = e.CommandArgument.ToString();
            if (Session["ContDetail"] != null)
            {
                DataTable dtContact = new DataTable();
                dtContact = (DataTable)Session["ContDetail"];

                // if only one row, not allowed
                if (dtContact.Rows.Count == 1)
                    return;

                for (int Index = 0; Index < dtContact.Rows.Count; Index++)
                    if (dtContact.Rows[Index]["SNo"].ToString() == rowId)
                        dtContact.Rows[Index].Delete();
                Session["ContDetail"] = dtContact;
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

                    dtContact = (DataTable)Session["ContDetail"];
                    if (dtContact != null)
                    {
                        ddlJobTitle.SelectedValue = dtContact.Rows[e.Row.RowIndex]["JobTitleId"].ToString();
                        ddlContactType.SelectedValue = dtContact.Rows[e.Row.RowIndex]["ContactTypeId"].ToString();
                    }
                }

            }
        }


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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            utilityId = AddUtilityCompanyDetails();

            if (utilityId != 0)
                Response.Redirect("ViewUtilityCompany.aspx?EntityId=" + utilityId + "&type=4");
        }

        #region[Method to add Uti.Comp Details]
        protected long AddUtilityCompanyDetails()
        {
            try
            {
                // Create object of UtilityCompany Class
                UtilityCompany objUtiComp = new UtilityCompany();
                // Assign company details to UtilityCompany class properties
                objUtiComp.CompanyName = txtCompanyName.Text.Trim();
                objUtiComp.TaxIdNumber = txtTaxIdNo.Text.Trim();

                if (!string.IsNullOrEmpty(ddlCompanyCategory.SelectedValue))
                    objUtiComp.CompanyCategoryId = Convert.ToInt32(ddlCompanyCategory.SelectedValue);

                if (!string.IsNullOrEmpty(ddlPaymentFreq.SelectedValue))
                    objUtiComp.PayementFrequencyId = Convert.ToInt32(ddlPaymentFreq.SelectedValue);

                objUtiComp.AccountNumber = txtAccountNo.Text.Trim();
                objUtiComp.Comments = txtComments.Text.Trim();
                objUtiComp.AddedBy = Constant.UserId;
                objUtiComp.DateAdded = DateTime.Now;
                objUtiComp.OrgID = Constant.OrgID;

                // Create object of Addredd class
                Address objAddress = new Address();
                // Assign address value to address class properties
                objAddress.Street = txtStreet.Text.Trim();
                objAddress.Suite = txtSuite.Text.Trim();
                objAddress.City = txtCity.Text.Trim();

                if (!string.IsNullOrEmpty(ddlState.SelectedValue) && ddlState.SelectedValue != "0")
                    objAddress.StateId = Convert.ToInt32(ddlState.SelectedValue);

                if (!string.IsNullOrEmpty(ddlCountry.SelectedValue) && ddlCountry.SelectedValue != "0")
                    objAddress.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);

                objAddress.Zip = txtZip.Text.Trim();
                objAddress.Phone1 = txtPhone.Text.Trim();
                objAddress.Fax = txtFax.Text.Trim();
                objAddress.Phone2 = txtOther.Text.Trim();
                objAddress.Email1 = txtEmail.Text.Trim();


                // Pass the both object into Method
                utilityId = BAL.UtilityCompanyBAL.AddUlilityCompany(objUtiComp, objAddress);
                // Call method to add contact details of Comapny
                AddContactDetails(utilityId);

            }
            catch (Exception ex)
            {
            }
            return utilityId;
        }
        #endregion

        #region[Add Contact Details of Company]
        public void AddContactDetails(long utilityId)
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
                objContact.EntityId = utilityId;
                objContact.EntityTypeId = 4;
                objContact.ContactTypeId = Convert.ToInt32(ddlContactType.SelectedValue);
                objContact.JobTitleId = Convert.ToInt32(ddlJobTitle.SelectedValue);
                objContact.FirstName = txtFirstName.Text.Trim();
                objContact.MiddleName = txtMiddleName.Text.Trim();
                objContact.LastName = txtLastName.Text.Trim();
                objContact.OfficePhone = txtOfficeNo.Text.Trim();
                objContact.CellPhone = txtCellNo.Text.Trim();
                objContact.Email = txtEmail.Text.Trim();
                objContact.AddedBy = Constant.UserId;


                //save the Contact details of a row.

                objCommonBAL.AddContactDetails(objContact);


            }
        }
        #endregion

        /// <summary>
        /// Handle Selected Index changed event of Country DropDownList
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