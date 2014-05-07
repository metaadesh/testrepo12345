using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using METAOPTION.BAL;
using METAOPTION.DAL;
using System.Web.Services;
namespace METAOPTION.UI
{

    public partial class ViewEmployee : System.Web.UI.Page, IPagePermission
    {
        #region[Constaints & Public Variables]
        public string EntityId = string.Empty;
        public const string PAGE = "EMPLOYEE";
        //Page Level Rights
        public const string PAGERIGHT_EDIT = "EMPLOYEE.EDIT";
        public const string PAGERIGHT_VIEW = "EMPLOYEE.VIEW";
        //Page Section Level Rights
        public const string EMPLOYEE_EMPLOYEEDETAILS_EDIT = "EMPLOYEEDETAILS.EDIT";
        public const string EMPLOYEE_EMPLOYEEDETAILS_VIEW = "EMPLOYEEDETAILS.VIEW";
        public const string EMPLOYEE_EMPLOYEECOMMENT_EDIT = "EMPLOYEECOMMENT.EDIT";
        public const string EMPLOYEE_EMPLOYEECOMMENT_VIEW = "EMPLOYEECOMMENT.VIEW";
        public const string EMPLOYEE_EMPLOYEEDOCUMENT_EDIT = "EMPLOYEEDOCUMENT.EDIT";
        public const string EMPLOYEE_EMPLOYEEDOCUMENT_VIEW = "EMPLOYEEDOCUMENT.VIEW";
        #endregion

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
            if (!IsPostBack)
            {
                if (Request.QueryString["EmployeeId"] != null)
                {
                    //5 is Entity Type For Employee
                    Util.Validate_QueryString_Value(5, Request.QueryString["EmployeeId"], Constant.OrgID);
                }
            }

            CheckUserPagePermissions(); // Check User Page Permission

            if (!IsPostBack)
            {
                // BindCountry(true);
                BindComments();
                BindDocuments();
            }
            if (Request["EmployeeId"] != null)
                EntityId = Request["EmployeeId"];
            hfEmployeeId.Value = EntityId;
        }


        /// <summary>
        /// Bind All States for Provided Country,bydefault it is 1 for U.S
        /// </summary>
        private void BindStates(int CountryId, bool isFromDb)
        {
            if (!isFromDb)
            {
                //Find dropdownlist
                DropDownList ddlState = frmviewEditEmployee.FindControl("ddlState") as DropDownList;
                ddlState.DataSource = METAOPTION.BAL.Common.GetStateList(CountryId);
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("", "0"));
            }

        }




        #region[Method that Check User Page Permission]
        public void CheckUserPagePermissions()
        {
            List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, PAGE);
            bool bTrue = true;
            if (!dict.Contains(PAGERIGHT_VIEW))// Check Page Permission for Logged User
                Response.Redirect("Permission.aspx?MSG=EMPLOYEE.EDIT OR EMPLOYEE.VIEW");
            // Check Permission For View of Each Section
            if (!dict.Contains(EMPLOYEE_EMPLOYEEDETAILS_VIEW))//Check Permission For View Details Section
            {
                // this.upEmployeeDetails.Visible = false;
                bTrue = false;
            }
            if (!dict.Contains(EMPLOYEE_EMPLOYEECOMMENT_VIEW))//Check Permission For View Comment Section
            {
                this.tblComment.Visible = false;
                bTrue = false;
            }
            if (!dict.Contains(EMPLOYEE_EMPLOYEEDOCUMENT_VIEW))//Check Permission For View Document Section
            {
                this.tblDocument.Visible = false;
                bTrue = false;
            }
            // Check Permission For Edit of Each Section
            if (!dict.Contains(PAGERIGHT_EDIT))// Check User has Edit Permission
            {
                this.imgbtnEdit.Visible = false;
                this.grdComments.Columns[3].Visible = false;
                this.trAddNewComment.Visible = false;
                this.grdDocument.Columns[5].Visible = false;
                this.trAddNewDoc.Visible = false;
                return;
            }
            if (!dict.Contains(EMPLOYEE_EMPLOYEEDETAILS_EDIT))//Check Permission For Edit Details Section
            {
                this.imgbtnEdit.Visible = false;
                bTrue = false;
            }
            if (!dict.Contains(EMPLOYEE_EMPLOYEECOMMENT_EDIT))//Check Permission For Edit Comment Section
            {
                //this.grdComments.Columns[2].Visible = false;
                this.trAddNewComment.Visible = false;
                bTrue = false;
            }
            if (!dict.Contains(EMPLOYEE_EMPLOYEEDOCUMENT_EDIT))//Check Permission For Edit Document Section
            {
                this.tblDocument.Visible = false;
                bTrue = false;
            }
            if (!(dict.Contains(PAGERIGHT_VIEW) || bTrue))
                Response.Redirect("Permission.aspx?MSG=EMPLOYEE.EDIT OR EMPLOYEE.VIEW");

        }
        #endregion
        protected void BindComments()
        {
            ////Bind Grid For Employee Comments
            grdComments.DataSource = METAOPTION.BAL.InventoryBAL.Notes_ByInventoryId(Convert.ToInt32(Request["EmployeeId"]), 5);
            grdComments.DataBind();
        }

        protected void BindDocuments()
        {
            grdDocument.DataSource = METAOPTION.BAL.InventoryBAL.Document_ByEntityId(5, Convert.ToInt32(Request["EmployeeId"]));
            grdDocument.DataBind();
        }

        #region [Handle click event to update employee Details]
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Find out the row of formview
            FormViewRow frmViewRow = (FormViewRow)frmviewEditEmployee.Row;
            //Findout controls in formviewrow
            DropDownList ddlTitle = (DropDownList)frmViewRow.FindControl("ddlTitle");
            TextBox txtFirstName = (TextBox)frmViewRow.FindControl("txtFirstName");
            TextBox txtMiddleName = (TextBox)frmViewRow.FindControl("txtMiddleName");
            TextBox txtLastName = (TextBox)frmViewRow.FindControl("txtLastName");
            TextBox txtEmployeeCode = (TextBox)frmViewRow.FindControl("txtEmployeeCode");
            DropDownList ddlEmployeeType = (DropDownList)frmViewRow.FindControl("ddlEmployeeType");
            // TextBox txtAccountingCode = (TextBox)frmViewRow.FindControl("txtAccountingCode");
            DropDownList ddlDLState = (DropDownList)frmViewRow.FindControl("ddlDLState");
            TextBox txtDLNumber = (TextBox)frmViewRow.FindControl("txtDLNumber");
            TextBox txtDLExpDate = (TextBox)frmViewRow.FindControl("txtDLExpDate");
            TextBox txtStreet = (TextBox)frmViewRow.FindControl("txtStreet");
            TextBox txtCity = (TextBox)frmViewRow.FindControl("txtCity");
            TextBox txtSuite = (TextBox)frmViewRow.FindControl("txtSuite");
            DropDownList ddlState = (DropDownList)frmViewRow.FindControl("ddlState");
            DropDownList ddlCountry = (DropDownList)frmViewRow.FindControl("ddlCountry");
            TextBox txtZip = (TextBox)frmViewRow.FindControl("txtZip");
            TextBox txtPhone = (TextBox)frmViewRow.FindControl("txtPhone");
            TextBox txtExt = (TextBox)frmViewRow.FindControl("txtExt");
            TextBox txtCellPhone = (TextBox)frmViewRow.FindControl("txtCellPhone");
            TextBox txtEmail = (TextBox)frmViewRow.FindControl("txtEmail");
            TextBox txtSPCondition = (TextBox)frmViewRow.FindControl("txtSPCondition");
            HiddenField hdfAddressId = (HiddenField)frmViewRow.FindControl("hdfAddressId");

            //Create object of Employee table
            Employee objEmployee = new Employee();
            //Assign control's values to employee table properties
            objEmployee.EmployeeId = Convert.ToInt64(Request["EmployeeId"]);
            objEmployee.TitleId = Convert.ToInt32(ddlTitle.SelectedValue);
            objEmployee.FirstName = txtFirstName.Text.Trim();
            objEmployee.MiddleName = txtMiddleName.Text;
            objEmployee.LastName = txtLastName.Text.Trim();
            objEmployee.EmployeeCode = txtEmployeeCode.Text.Trim();
            objEmployee.EmployeeTypeId = Convert.ToInt32(ddlEmployeeType.SelectedValue);
            // objEmployee.AccountingCode = txtAccountingCode.Text.Trim();
            objEmployee.DriverLicenseStateId = Convert.ToInt32(ddlDLState.SelectedValue);
            objEmployee.DriverLicenseNumber = txtDLNumber.Text.Trim();

            if (!string.IsNullOrEmpty(txtDLExpDate.Text))
                objEmployee.DriverLicensExpDate = Convert.ToDateTime(txtDLExpDate.Text.Trim());

            objEmployee.CellPhone = txtCellPhone.Text.Trim();
            objEmployee.SpecialPayrollConditions = txtSPCondition.Text.Trim();
            objEmployee.ModifiedBy = Constant.UserId;

            //Create object of address table
            Address objAddress = new Address();
            // Assign values to address table properties
            objAddress.EntityId = Convert.ToInt64(Request["EmployeeId"]);
            objAddress.Street = txtStreet.Text.Trim();
            objAddress.City = txtCity.Text.Trim();

            //Save selected country & address details
            if (!string.IsNullOrEmpty(ddlState.SelectedValue) && ddlState.SelectedValue != "0")
                objAddress.StateId = Convert.ToInt32(ddlState.SelectedValue);

            if (!string.IsNullOrEmpty(ddlCountry.SelectedValue) && ddlCountry.SelectedValue != "0")
                objAddress.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);


            objAddress.Zip = txtZip.Text.Trim();
            objAddress.Phone1 = txtPhone.Text.Trim();
            objAddress.Phone1Ext = txtExt.Text.Trim();
            objAddress.Email1 = txtEmail.Text.Trim();
            objAddress.AddressId = Convert.ToInt64(hdfAddressId.Value);
            objAddress.ModifiedBy = Constant.UserId;
            objAddress.Suite = txtSuite.Text.Trim();
            //Call method for update employee details along address details
            BAL.EmployeeBAL.UpdateEmployeeDetails(objEmployee, objAddress);
            //Again bind details
            DataBind(); BindComments();
            BindDocuments();
        }
        #endregion

        protected void frmviewEditEmployee_DataBound(object sender, EventArgs e)
        {
            if (frmviewEditEmployee.DataItemCount > 0)
            {
                // Find HiddenField for Country & State Value

                HiddenField hfieldCountry = frmviewEditEmployee.Row.FindControl("hfieldCountry") as HiddenField;
                HiddenField hfieldState = frmviewEditEmployee.Row.FindControl("hfieldState") as HiddenField;
                HiddenField hfieldDLState = frmviewEditEmployee.Row.FindControl("hfieldDLState") as HiddenField;
                DropDownList ddlState = frmviewEditEmployee.Row.FindControl("ddlState") as DropDownList;
                DropDownList ddlDLState = frmviewEditEmployee.Row.FindControl("ddlDLState") as DropDownList;
                DropDownList ddlCountry = frmviewEditEmployee.Row.FindControl("ddlCountry") as DropDownList;

                //Fill Country Drop Down
                BAL.Common bal = new BAL.Common();
                ddlCountry.DataSource = bal.GetCountryList();
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("", "0"));

                if (ddlCountry.Items.Count < 1)
                    return;

                //Make U.S Country Selection
                if (!string.IsNullOrEmpty(hfieldCountry.Value))
                {
                    if (ddlCountry.Items.FindByValue(hfieldCountry.Value) != null && hfieldCountry.Value != "0")
                        ddlCountry.SelectedValue = hfieldCountry.Value;

                    else
                        ddlCountry.SelectedValue = Constant.US_COUNTRYID;
                }
                else
                    ddlCountry.SelectedValue = Constant.US_COUNTRYID;


                // IF CountryId is not null
                if (!string.IsNullOrEmpty(hfieldCountry.Value) && hfieldCountry.Value != "0")
                {
                    //Fill State DropDown According that countryID
                    ddlState.DataSource = BAL.Common.GetStateList(Convert.ToInt32(hfieldCountry.Value));
                    ddlDLState.DataSource = BAL.Common.GetStateList(Convert.ToInt32(hfieldCountry.Value));
                }
                else
                {
                    //Fill State DropDown for U.S.A Default
                    ddlState.DataSource = BAL.Common.GetStateList(233);
                    ddlDLState.DataSource = BAL.Common.GetStateList(233);
                }
                ddlState.DataBind();
                ddlDLState.DataBind();

                //Insert blank item
                ddlState.Items.Insert(0, new ListItem("", "0"));

                // IF stateId is not null
                if (hfieldState.Value != null && ddlState.Items.FindByValue(hfieldState.Value) != null)
                    ddlState.SelectedValue = hfieldState.Value;
                if (hfieldDLState.Value != null)
                    ddlDLState.SelectedValue = hfieldDLState.Value;

                hfieldState.Value = null;
                hfieldCountry.Value = null;
            }
        }

        protected void grdComments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdComments.PageIndex = e.NewPageIndex;
            grdComments.DataBind();
        }

        protected void grdComments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Find Deleted Row;
            long RowId = Convert.ToInt64(grdComments.DataKeys[e.RowIndex].Value);
            // Create object of notes Class
            Note objNote = new Note();
            objNote.DeletedBy = Constant.UserId;
            objNote.DateDeleted = DateTime.Now;
            objNote.IsActive = 1;
            //CALL BAL Method to Delete Notes,(Mark IsActive=0)
            BAL.InventoryBAL.DeleteNotes(objNote, RowId);
            BindComments();
        }

        #region [ Open attached file from the database ]
        /// <summary>
        /// Open attached file from the database 
        /// </summary>
        /// <param name="attachmentid">documentId</param>
        private void OpenAttachment(Int32 documentId)
        {
            BAL.AttachedDocs doc = BAL.InventoryBAL.DocumentOpenById(documentId);
            //ProjectAttachment projectAttach = new ProjectAttachment();
            //PMS.BLL.PMS pms = new PMS.BLL.PMS();
            //projectAttach = pms.GetProjectAttachmentById(attachmentid);

            if (doc.FileLength > 0)
            {
                Response.ContentType = Convert.ToString(doc.FileType);
                Response.AppendHeader("Content-Disposition",
                "attachment; filename=\"" + doc.FileName + "\"");
                Response.OutputStream.Write(doc.FileBytes, 0, doc.FileLength);
            }
            else
            {
                Response.Write("Problem in getting file data");
            }
        }
        #endregion

        protected void grdDocument_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "OpenDoc")
                OpenAttachment(Convert.ToInt32(e.CommandArgument));
        }

        protected void grdDocument_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            long DocId = Convert.ToInt64(grdDocument.DataKeys[e.RowIndex].Value);

            //Call BAL Method to delete expense
            METAOPTION.BAL.InventoryBAL.Document_Delete(DocId, Constant.UserId);

            //Refresh Gridview Control
            BindDocuments();
        }
        /// <summary>
        /// Handle selected index changed event of Country dropdownlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlCountry = frmviewEditEmployee.FindControl("ddlCountry") as DropDownList;
            DropDownList ddlState = frmviewEditEmployee.FindControl("ddlState") as DropDownList;
            if (!string.IsNullOrEmpty(ddlCountry.SelectedValue) && ddlCountry.SelectedValue != "0")
                BindStates(Convert.ToInt32(ddlCountry.SelectedValue), false);
            else
                ddlState.Items.Clear();
        }

        #region [Added by Rupendra 22 Nov 12 for Check Buyer Exixts or Not]

        protected void frmviewEmployee_DataBound(object sender, EventArgs e)
        {
            FormView fv = sender as FormView;
            HiddenField hfIsEmployeeExists = (HiddenField)fv.FindControl("hfIsEmployeeExists");
            HiddenField hfIsActive = (HiddenField)fv.FindControl("hfIsActive");

            if (!string.IsNullOrEmpty(hfIsEmployeeExists.Value))
            {
                if (Convert.ToBoolean(hfIsEmployeeExists.Value) == true)
                {
                    imgDelete.Attributes.Add("style", "display:none;float:right;padding-right:5px;");
                    imgUnDelete.Attributes.Add("style", "display:none;float:right;padding-right:5px;");
                }
                else
                {
                    if (hfIsActive.Value == "1" || hfIsActive.Value == "2")
                    {
                        imgDelete.Attributes.Add("style", "display:block;float:right;padding-right:5px;");
                        imgUnDelete.Attributes.Add("style", "display:none;float:right;padding-right:5px;");
                    }
                    else if (hfIsActive.Value == "0")
                    {
                        imgDelete.Attributes.Add("style", "display:none;float:right;padding-right:5px;");
                        imgUnDelete.Attributes.Add("style", "display:block;float:right;padding-right:5px;");
                    }
                }
            }

            if (hfIsActive.Value == "0")
            {
                imgArchive.Attributes.Add("style", "display:block;float:right;padding-right:5px;");
                imgUnArchive.Attributes.Add("style", "display:none;float:right;padding-right:5px;");
            }
            else if (hfIsActive.Value == "1")
            {
                imgArchive.Attributes.Add("style", "display:block;float:right;padding-right:5px;");
                imgUnArchive.Attributes.Add("style", "display:none;float:right;padding-right:5px;");
            }
            else if (hfIsActive.Value == "2")
            {
                imgArchive.Attributes.Add("style", "display:none;float:right;padding-right:5px;");
                imgUnArchive.Attributes.Add("style", "display:block;float:right;padding-right:5px;");
            }
        }

        [WebMethod(true)]
        public static void DeleteArchiveEmployee(int Status, Int64 EmployeeID, Int64 UserID)
        {
            BAL.EmployeeBAL.DeleteArchiveEmployee((Status == -1) ? 1 : Status, EmployeeID, UserID);
        }
        #endregion
    }
}