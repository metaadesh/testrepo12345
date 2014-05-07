using System;
using System.Collections;
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

namespace METAOPTION
{
    public partial class AddCustomMMB : System.Web.UI.Page
    {
        public const string PAGE = "ADDCUSTOMMMB";
        public const string PAGERIGHT = "ADDCUSTOMMMB.VIEW";
        #region Public Variables
        string _Year = string.Empty;
        string _Model = string.Empty;
        string _Make = string.Empty;
        string _Body = string.Empty;
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
            CheckUserPagePermissions();
            if (!IsPostBack)
                BindYears(false);
        }
        #region Bind Years
        /// <summary>
        /// Bind Years in dropdownlist
        /// </summary>
        private void BindYears(bool isDefault)
        {
           
            /*Load Years*/
            //Bind Last 4 Years
            ddlYear.DataSource = Common.GetYearList();
            ddlYear.DataValueField = "Year";
            ddlYear.DataTextField = "Year";
            ddlYear.DataBind();
            ddlYear.SelectedIndex = 0;


            //Bind Make Details for Selected Year
            if (!string.IsNullOrEmpty(ddlYear.SelectedValue))
                BindMake(Convert.ToInt32(ddlYear.SelectedValue), isDefault);


        }
        #endregion

        #region ApplyPagePermission Members
        /// <summary>
        /// Check User Permissions for "INVENTORY.ADD" Right
        /// </summary>
        public void CheckUserPagePermissions()
        {
            System.Collections.Generic.List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, PAGE);
            bool bTrue = true;
            if (!dict.Contains(PAGERIGHT))
            {
                bTrue = false;
            }

            //Redirect User to Common Page for InSufficient Rights
            if (!(dict.Contains(PAGERIGHT) || bTrue))
                Response.Redirect("Permission.aspx?MSG=ADDCUSTOMMMB.VIEW");
        }

        #endregion

        #region Bind Make
        /// <summary>
        /// Bind Dropdownlist with make details based on year selected
        /// </summary>
        private void BindMake(int year, bool isDefault)
        {

            Common objCommon = new Common();
            /*Bind Make DropDownlist*/
            ddlMake.DataSource = objCommon.GetCustomMake(year);
            ddlMake.DataTextField = "Make";
            ddlMake.DataValueField = "MakeId";
            ddlMake.DataBind();
            ddlMake.Items.Insert(0, new ListItem("", ""));

            //Set Default Value
            if (isDefault)
                if (ddlMake.Items.FindByValue(_Make) != null)
                    ddlMake.SelectedValue = _Make;

            //Bind Model this selected Make
            if (!string.IsNullOrEmpty(ddlMake.SelectedValue))
            { 
                DisableMakeUpdate(Convert.ToInt64(ddlMake.SelectedValue));
                BindModel(Convert.ToInt64(ddlMake.SelectedValue), isDefault);
            }

        }
          private void DisableMakeUpdate(long MakeId)
            {
                Common objCommon = new Common();
                if (objCommon.IsCustomMake(Convert.ToInt64(ddlMake.SelectedValue)))
                {
                    btnUpdateMake.Enabled = true;
                    btnDeleteMake.Enabled = true;
                }
                else
                {
                    btnUpdateMake.Enabled = false;
                    btnDeleteMake.Enabled = false;
                }
            }

          private void DisableModelUpdate(long ModelId)
          {
              Common objCommon = new Common();
              if (objCommon.IsCustomModel(Convert.ToInt64(ddlModel.SelectedValue)))
              {
                  btnUpdateModel.Enabled = true;
                  btnDeleteModel.Enabled = true;
              }
              else
              {
                  btnUpdateModel.Enabled = false;
                  btnDeleteModel.Enabled = false;
              }
          }

          private void DisableBodyUpdate(long BodyId)
          {
              Common objCommon = new Common();
              if (objCommon.IsCustomBody(Convert.ToInt64(ddlBody.SelectedValue)))
              {
                  btnUpdateBody.Enabled = true;
                  btnDeleteBody.Enabled = true;
              }
              else
              {
                  btnUpdateBody.Enabled = false;
                  btnDeleteBody.Enabled = false;
              }
          }

        #endregion

        #region Bind Model
        /// <summary>
        /// Bind dropdownlist with Model details based on make selected
        /// </summary>
        private void BindModel(long makeId, bool isDefault)
        {
            Common objCommon = new Common();
            /*Bind Model DropDownlist*/
            ddlModel.DataSource = objCommon.GetCustomModel(makeId);
            ddlModel.DataTextField = "Model";
            ddlModel.DataValueField = "ModelId";
            ddlModel.DataBind();
            ddlModel.Items.Insert(0, new ListItem("", ""));

            //Set Default Value
            if (isDefault)
                if (ddlModel.Items.FindByValue(_Model) != null)
                    ddlModel.SelectedValue = _Model;
          
            //Bind Body
            if (!string.IsNullOrEmpty(ddlModel.SelectedValue))
            {
                DisableModelUpdate(Convert.ToInt64(ddlModel.SelectedValue));
                BindBody(Convert.ToInt64(ddlModel.SelectedValue), isDefault);
            }
            //Bind Colors for Selected Year,Make,Model,Body
            // BindColors();
        }
        #endregion

        #region Bind Body
        /// <summary>
        /// Bind dropdownlist with Body details based on make selected
        /// </summary>
        private void BindBody(long modelId, bool isDefault)
        {
            Common objCommon = new Common();
            /*Bind Body DropDownlist*/
            ddlBody.DataSource = objCommon.GetCustomBody(modelId);
            ddlBody.DataTextField = "Body";
            ddlBody.DataValueField = "BodyId";
            ddlBody.DataBind();
            ddlBody.Items.Insert(0, new ListItem("", ""));

            //Set Default Value
            if (isDefault)
                if (ddlBody.Items.FindByValue(_Body) != null)
                    ddlBody.SelectedValue = _Body;

            if (!string.IsNullOrEmpty(ddlBody.SelectedValue))
            {
                DisableBodyUpdate(Convert.ToInt64(ddlBody.SelectedValue));
            }
        }
        #endregion

        /// <summary>
        /// Handle Selected Indexed Changed event of Dropdownlist ddlModel to Fetch Colors for Selected Year,Make,Model,Body
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clear all items
            ddlBody.Items.Clear();
            txtModel.Text = "";
           
            //Bind Body details for selected model
            if (!string.IsNullOrEmpty(ddlModel.SelectedValue))
            {
                txtModel.Text = ddlModel.SelectedItem.Text;
                DisableModelUpdate(Convert.ToInt32(ddlModel.SelectedValue));  

                BindBody(Convert.ToInt32(ddlModel.SelectedValue), false);
            }
            else
            {
                txtModel.Text = "";
            }

        }

        /// <summary>
        /// Handle Selected Indexed Event of ddlMake
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clear Dropdownlists depend on this
            ddlModel.Items.Clear();
            ddlBody.Items.Clear();
            txtMake.Text = "";
           

            //Fetch Body and Model for selected Make
            if (!string.IsNullOrEmpty(ddlMake.SelectedValue))
            {
                DisableMakeUpdate(Convert.ToInt64(ddlMake.SelectedValue));
                //Fetch Model's based on Selected Make
                BindModel(Convert.ToInt64(ddlMake.SelectedValue), false);
                txtMake.Text = ddlMake.SelectedItem.Text;
            }

            else
            {
                txtMake.Text = "";
            }

        }



        /// <summary>
        /// Handle Selected Index Changed Event of Year DropDownlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clear all dropdownlists depend directly/indirectly on this
            ddlMake.Items.Clear();
            ddlModel.Items.Clear();
            ddlBody.Items.Clear();
            ResetFields();

            //Fetch Makes based on selected Year
            if (!string.IsNullOrEmpty(ddlYear.SelectedValue))
                BindMake(Convert.ToInt32(ddlYear.SelectedValue), false);
        }

        protected void btnSaveMake_Click(object sender, EventArgs e)
        {  
            lblMessage.Text="";
            Common objCommon=new Common();
            long makeId=0;

            if (!string.IsNullOrEmpty(ddlYear.SelectedValue) && txtMake.Text.Trim() != "")
                     makeId = objCommon.AddCustomMake(txtMake.Text.Trim(), Convert.ToInt32(ddlYear.SelectedValue), Constant.UserId, DateTime.Now);
            else
                {
                    lblMessage.Text = "Enter Make!";
                    return;
                }
            if (makeId > 0)
            {
                _Make = Convert.ToString(makeId);
                lblMessage.Text = "Make " + txtMake.Text.Trim() + " (" + makeId + ")" + " Added Successfully";
                ScriptManager.GetCurrent(this).SetFocus(txtModel);
            }
            else if (makeId == -100)//Code = -100 For Already Exists
                lblMessage.Text = "Make " + txtMake.Text.Trim() + " already exists against year: " + ddlYear.SelectedValue;
            else
               lblMessage.Text = "Some db error occurred, While inserting the record";
            
            if(!string.IsNullOrEmpty(ddlYear.SelectedValue))
             BindMake(Convert.ToInt32(ddlYear.SelectedValue), true);

            
            ResetFields();
        }

        protected void btnSaveModel_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            txtMake.Text = "";
            Common objCommon = new Common();
            long modelId = 0;

            if (!string.IsNullOrEmpty(ddlMake.SelectedValue) && txtModel.Text.Trim() != "")
                    modelId = objCommon.AddCustomModel(txtModel.Text.Trim(), Convert.ToInt64(ddlMake.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Constant.UserId, DateTime.Now);

                else
                {
                    lblMessage.Text = "Enter Model!";
                    return;
                }
            
            if (modelId > 0)
            {
                _Model = Convert.ToString(modelId);
                lblMessage.Text = "Model " +txtModel.Text.Trim() +" (" + modelId +")"+ " Added Successfully";
                ScriptManager.GetCurrent(this).SetFocus(txtBody);
            }
            else if (modelId == -100)//Code = -100 For Already Exists
                lblMessage.Text = "Model " + txtModel.Text.Trim() + " already exists against Make: " + ddlMake.SelectedItem.Text;
            else
                lblMessage.Text = "Some db error occurred, While inserting the record";
           
            if(!string.IsNullOrEmpty(ddlMake.SelectedValue)) 
             BindModel(Convert.ToInt64(ddlMake.SelectedValue), true);

            ResetFields();
        }

        protected void btnSaveBody_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            txtMake.Text = "";
            txtModel.Text = "";
            Common objCommon = new Common();
            long bodyId = 0;
           
            if (!string.IsNullOrEmpty(ddlModel.SelectedValue) && txtBody.Text.Trim() != "")
                   bodyId = objCommon.AddCustomBody(txtBody.Text.Trim(), Convert.ToInt64(ddlModel.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Constant.UserId, DateTime.Now);
             else
                {
                    lblMessage.Text = "Enter Body!";
                    return;
                }

            if (bodyId > 0)
            {
                _Body = Convert.ToString(bodyId);
                lblMessage.Text = "Body " + txtBody.Text.Trim()+ " ("+ bodyId +")" + " Added Successfully";
                ScriptManager.GetCurrent(this).SetFocus(txtMake);
            }
            else if (bodyId == -100)//Code = -100 For Already Exists
                lblMessage.Text = "Body " + txtBody.Text.Trim() + " already exists against Model: " + ddlModel.SelectedItem.Text;
            else
              lblMessage.Text = "Some db error occurred, While inserting the record";
            
            if(!string.IsNullOrEmpty(ddlModel.SelectedValue))
                BindBody(Convert.ToInt64(ddlModel.SelectedValue), true);

            ResetFields();
        }

        protected void ddlBody_SelectedIndexChanged(object sender, EventArgs e)
        {
           txtBody.Text = "";

            if (!string.IsNullOrEmpty(ddlBody.SelectedValue))
            {
                DisableBodyUpdate(Convert.ToInt64(ddlBody.SelectedValue));
                txtBody.Text = ddlBody.SelectedItem.Text;
            }
            else
            {
                 txtBody.Text = "";
            }
        }

        protected void btnSaveToMainDB_Click(object sender, EventArgs e)
        {
            try
            {
                Common objCommon = new Common();
                objCommon.AddCustomRecordsToYMMB(Constant.UserId);
                lblMessage.Text = "System is upgrading Chrome tables for Custom Chrome Data and this process may last for 10 Minutes.";
                ResetFields();
            }

            catch (Exception ex)
            {
                lblMessage.Text = "Custom Chrome Data not processed Successfully, Please try again After Some Time";
                lblMessage.ToolTip = ex.InnerException.Message;
            }
        }

        protected void btnUpdateMake_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            Common objCommon = new Common();
            int Code = 0;

            if (!string.IsNullOrEmpty(txtMake.Text) && !string.IsNullOrEmpty(ddlMake.SelectedValue))
            {
                _Make = Convert.ToString(ddlMake.SelectedValue);
                Code = objCommon.UpdateCustomMake(Convert.ToInt64(ddlMake.SelectedValue),Convert.ToInt32(ddlYear.SelectedValue)
                                          , txtMake.Text.Trim(), Constant.UserId, 1);
            }
            else
            {
                lblMessage.Text = "Enter Make to Update!";
                return;
            }

           if (Code == -999)//Code = -999 For Db Error
               lblMessage.Text = "Some Db Error Occurred while updating the record";
           else if (Code == -100)//Code = -100 For Already Exists
               lblMessage.Text = "Make " + txtMake.Text.Trim() + " already exists against year: " + ddlYear.SelectedValue;
          
           else
               lblMessage.Text = txtMake.Text.Trim() + " Updated Successfully";

           ResetFields();
           
           if(!string.IsNullOrEmpty(ddlYear.SelectedValue))
             BindMake(Convert.ToInt32(ddlYear.SelectedValue), true);   
        }

        private void ResetFields()
        {
            txtMake.Text = "";
            txtModel.Text = "";
            txtBody.Text = "";
        }
        protected void btnUpdateModel_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            Common objCommon = new Common();
            int Code = 0;

            if (!string.IsNullOrEmpty(txtModel.Text) && !string.IsNullOrEmpty(ddlModel.SelectedValue))
            {
                _Model = Convert.ToString(ddlModel.SelectedValue);
                Code = objCommon.UpdateCustomModel(Convert.ToInt64(ddlModel.SelectedValue), Convert.ToInt64(ddlMake.SelectedValue)
                                          , txtModel.Text.Trim(), Constant.UserId, 1);
            }
            else
            {
                lblMessage.Text = "Enter Model to Update!";
                return;
            }

            if (Code == -999)//Code = -999 For Db Error
                lblMessage.Text = "Some Db Error Occurred while updating the record";

            else if (Code == -100)//Code = -100 For Already Exists
                lblMessage.Text = "Model " + txtModel.Text.Trim() + " already exists against Make: " + ddlMake.SelectedItem.Text;
          
            else
                lblMessage.Text = txtModel.Text.Trim() + " Updated Successfully";

            ResetFields();

            if(!string.IsNullOrEmpty(ddlMake.SelectedValue))
                BindModel(Convert.ToInt64(ddlMake.SelectedValue), true);
        }

        protected void btnUpdateBody_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            Common objCommon = new Common();
            int Code = 0;

            if (!string.IsNullOrEmpty(txtBody.Text) && !string.IsNullOrEmpty(ddlBody.SelectedValue))
            {
                _Body = Convert.ToString(ddlBody.SelectedValue);
                Code = objCommon.UpdateCustomBody(Convert.ToInt64(ddlBody.SelectedValue),Convert.ToInt64(ddlModel.SelectedValue)
                                          , txtBody.Text.Trim(), Constant.UserId, 1);
            }
            else
            {
                lblMessage.Text = "Enter Body to Update!";
                return;
            }

            if (Code == -999)//Code = -999 For Db Error
                lblMessage.Text = "Some Db Error Occurred while updating the record";

            else if (Code == -100)//Code = -100 For Already Exists
                lblMessage.Text = "Body " + txtBody.Text.Trim() + " already exists against Model: " + ddlModel.SelectedItem.Text;
           
            else
                lblMessage.Text = txtBody.Text.Trim() + " Updated Successfully";

            ResetFields();
           if(!string.IsNullOrEmpty(ddlModel.SelectedValue))
               BindBody(Convert.ToInt64(ddlModel.SelectedValue), true);

        }

        protected void btnDeleteMake_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            Common objCommon=new Common();
            int code = 0;
            if (!string.IsNullOrEmpty(ddlMake.SelectedValue))
            {
              code= objCommon.DeleteCustomMake(Convert.ToInt64(ddlMake.SelectedValue),Constant.UserId);

              if (code == -100)
                  lblMessage.Text = "Delete operation couldn't be performed against make " + ddlMake.SelectedItem.Text + " as this make already exists against some inventory.";
              else if (code == -999)
                  lblMessage.Text = "Some db error has been Occurred.";
              else
                  lblMessage.Text = "Make " + ddlMake.SelectedItem.Text + " has been successfully deleted and all model,body underlying this make also deleted";

              ddlModel.Items.Clear();
              ddlBody.Items.Clear();

              if (!string.IsNullOrEmpty(ddlYear.SelectedValue))
                  BindMake(Convert.ToInt32(ddlYear.SelectedValue), false);
              
              ResetFields();
            }
        }

        protected void btnDeleteModel_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            Common objCommon = new Common();
            int code = 0;
            if (!string.IsNullOrEmpty(ddlModel.SelectedValue))
            {
                code = objCommon.DeleteCustomModel(Convert.ToInt64(ddlModel.SelectedValue),Constant.UserId);

                if (code == -100)
                    lblMessage.Text = "Delete operation couldn't be performed against model " + ddlModel.SelectedItem.Text + " as this model already exists against some inventory.";
                else if (code ==-999)
                    lblMessage.Text = "Some db error has been Occurred.";
                else
                    lblMessage.Text = "Model " + ddlModel.SelectedItem.Text + " has been successfully deleted and all body underlying this model also deleted";

                ResetFields();

                ddlBody.Items.Clear();

                if (!string.IsNullOrEmpty(ddlMake.SelectedValue))
                    BindModel(Convert.ToInt64(ddlMake.SelectedValue), false);
            }
        }

        protected void btnDeleteBody_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            Common objCommon = new Common();
            int code = 0;
            if (!string.IsNullOrEmpty(ddlBody.SelectedValue))
            {
                code = objCommon.DeleteCustomBody(Convert.ToInt64(ddlBody.SelectedValue),Constant.UserId);

                if (code == -100)
                    lblMessage.Text = "Delete operation couldn't be performed against body " + ddlBody.SelectedItem.Text + " as this body already exists against some inventory.";
                else if (code == -999)
                    lblMessage.Text = "Some db error has been Occurred.";
                else
                    lblMessage.Text = "Body " + ddlBody.SelectedItem.Text + " has been successfully deleted.";

              
                if (!string.IsNullOrEmpty(ddlModel.SelectedValue))
                    BindBody(Convert.ToInt64(ddlModel.SelectedValue), false);

                ResetFields();
            }

        }
    }
}
