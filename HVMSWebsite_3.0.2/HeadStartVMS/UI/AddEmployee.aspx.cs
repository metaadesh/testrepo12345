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
    public partial class AddEmployee : System.Web.UI.Page
    {
        public const string PAGE = "EMPLOYEE";
        public const string PAGERIGHT = "EMPLOYEE.ADD";

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
            //Check Whether user has sufficient prievelege to Add New Employee in the System
            CheckPermission();

            if (!IsPostBack)
                LoadMasters();
        }
        #region[Check Permission method]
        /// <summary>
        /// This method check permission whether user has prievelge to add employee in the system
        /// </summary>
        private void CheckPermission()
        {
            System.Collections.Generic.List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, PAGE);
            bool bTrue = true;
            if (!dict.Contains(PAGERIGHT))
            {
                bTrue = false;
            }

            //Redirect User to Common Page for InSufficient Rights
            if (!(dict.Contains(PAGERIGHT) || bTrue))
                Response.Redirect("Permission.aspx?MSG=EMPLOYEE.ADD");


        }
        #endregion

        /// <summary>
        /// This method load various masters
        /// </summary>
        private void LoadMasters()
        {
            BindCountry();
            BindDriverState();
            BindTitles();
            BindEmployeeTypes();
        }

        #region [Bind Drivers State]
        /// <summary>
        /// Bind U.S States in Drivers State Dropdownlist
        /// </summary>
        protected void BindDriverState()
        {
            ddlDriverLicState.DataSource = BAL.Common.GetStateList(233);
            ddlDriverLicState.DataBind();
            ddlDriverLicState.Items.Insert(0, new ListItem("", "0"));
        }

        #endregion

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

        ///// <summary>
        ///// Bind states based on selected Country
        ///// </summary>
        //protected void BindState()
        //{
        //    // Fill State Drop Down           
        //    ddlState.DataSource = BAL.Common.GetStateList(Convert.ToInt32(ddlCountry.SelectedValue));
        //    ddlState.DataTextField = "State";
        //    ddlState.DataValueField = "StateId";
        //    ddlState.DataBind();
        //    ddlState.Items.Insert(0, new ListItem("", "0"));
        //}

        #region Bind Titles
        /// <summary>
        /// Bind list of titles
        /// </summary>
        private void BindTitles()
        {
            Common objCommon = new Common();
            ddlTitle.DataSource = objCommon.GetTitles();
            ddlTitle.DataTextField = "Title";
            ddlTitle.DataValueField = "TitleId";
            ddlTitle.DataBind();
        }

        #endregion

        /// <summary>
        /// Bind Employee Types in Dropdownlist
        /// </summary>
        private void BindEmployeeTypes()
        {
            Common objCommon = new Common();
            ddlEmployeeType.DataSource = objCommon.GetEmployeeType();
            ddlEmployeeType.DataValueField = "EmployeeTypeId";
            ddlEmployeeType.DataTextField = "EmployeeType";
            ddlEmployeeType.DataBind();
        }

        /// <summary>
        /// Bind All States for Provided Country,bydefault it is 1 for U.S
        /// </summary>
        private void BindStates(int CountryId)
        {
            ddlState.DataSource = Common.GetStateList(CountryId);
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("", "0"));
        }

        /// <summary>
        /// This method call method of BAL Class to Add New Employee
        /// </summary>
        /// <returns></returns>
        private long AddEmp()
        {
            long empID;

            Employee objEmployee = new Employee();
            Address objEmpAddress = new Address();
            // AddEmployeeBAL objAddEmployeeBAL = new AddEmployeeBAL();

            // objEmployee.AccountingCode = txtAccountingCode.Text;
            objEmployee.CellPhone = txtCellPhone.Text;
            objEmployee.DriverLicenseNumber = txtDriverLicNo.Text.Trim();
            // objEmployee..Notes  = txtComment.Text;
            if (!string.IsNullOrEmpty(txtLicExpDate.Text))
                objEmployee.DriverLicensExpDate = Convert.ToDateTime(txtLicExpDate.Text);

            //Get Driver Licence StaeId
            if (!string.IsNullOrEmpty(ddlDriverLicState.SelectedValue) && ddlDriverLicState.SelectedValue != "0")
                objEmployee.DriverLicenseStateId = Convert.ToInt32(ddlDriverLicState.SelectedValue);

            objEmpAddress.Email1 = txtEmail.Text;
            objEmployee.EmployeeCode = txtEmployeeCode.Text;

            if (!string.IsNullOrEmpty(ddlEmployeeType.SelectedValue))
                objEmployee.EmployeeTypeId = Convert.ToInt32(ddlEmployeeType.SelectedValue);

            objEmpAddress.Street = txtStreet.Text.Trim();
            objEmpAddress.Suite = txtSuite.Text.Trim();
            objEmpAddress.City = txtCity.Text;

            if (!string.IsNullOrEmpty(ddlState.SelectedValue) && ddlState.SelectedValue != "0")
                objEmpAddress.StateId = Convert.ToInt32(ddlState.SelectedValue);

            if (!string.IsNullOrEmpty(ddlCountry.SelectedValue) && ddlCountry.SelectedValue != "0")
                objEmpAddress.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);

            objEmpAddress.Zip = txtZip.Text.Trim();

            //objEmployee.Ext = txtExt.Text;
            objEmployee.FirstName = txtFirstName.Text;
            objEmployee.LastName = txtLastName.Text;
            objEmployee.MiddleName = txtMiddleName.Text;
            objEmpAddress.Phone1 = txtPhone.Text;
            objEmpAddress.Phone1Ext = txtExt.Text.Trim();

            objEmployee.AddedBy = Constant.UserId;
            objEmployee.DateAdded = DateTime.Now;
            objEmployee.IsActive = 1;
            objEmployee.OrgID = Constant.OrgID;
            // objEmployee.Comments = txtComment.Text.Trim();
            objEmployee.SpecialPayrollConditions = txtSpcPayCondt.Text;

            if (!string.IsNullOrEmpty(ddlTitle.SelectedValue))
                objEmployee.TitleId = Convert.ToInt32(ddlTitle.SelectedValue);
            //Call BAL Layer method to insert employee record along with address details
            empID = BAL.EmployeeBAL.AddEmployee(objEmployee, objEmpAddress);
            return empID;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                //Add Employee Detail
                long EmployeeId = AddEmp();
                Response.Redirect("ViewEmployee.aspx?EmployeeId=" + EmployeeId);
            }
        }
        /// <summary>
        /// Reset all fields on click of cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Redirect to same page for reset all fields
            Response.Redirect("AddEmployee.aspx");
        }

        /// <summary>
        /// Handled Selected Index change event of dropdownlist
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

        #region [Commented code]
        ///// <summary>
        ///// This function Reset all controls values
        ///// </summary>
        //private void Reset()
        //{
        //    txtZip.Text = string.Empty;
        //    txtStreet.Text = string.Empty;
        //    ddlState.SelectedIndex = -1;
        //    txtSpcPayCondt.Text = string.Empty;
        //    txtPhone.Text = string.Empty;
        //    txtMiddleName.Text = string.Empty;
        //    txtLicExpDate.Text = string.Empty;
        //    txtLastName.Text = string.Empty;
        //    txtFirstName.Text = string.Empty;
        //    txtExt.Text = string.Empty;
        //    txtEmployeeCode.Text = string.Empty;
        //    txtEmail.Text = string.Empty;
        //    txtDriverLicNo.Text = string.Empty;
        //   // txtComment.Text = string.Empty;
        //    txtCity.Text = string.Empty;
        //    txtCellPhone.Text = string.Empty;
        //   // txtAccountingCode.Text = string.Empty;
        //}

        #endregion
    }
}
