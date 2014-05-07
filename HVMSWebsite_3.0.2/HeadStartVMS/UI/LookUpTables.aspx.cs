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
using METAOPTION.DAL;
using METAOPTION.BAL;
using System.Text;

namespace METAOPTION.UI
{
    public partial class LookUpTables : System.Web.UI.Page
    {
        #region [ Declariation part  ]
        ViewLookUpTablesBAL objBAL = new ViewLookUpTablesBAL();
        METAOPTION.BAL.Common OBJNEW = new BAL.Common();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        int IsActive = 0;
        String Where = string.Empty;
        String SQLForUpdateValues = string.Empty;
        //For look up Insert type are two (1) Table are not dependent for insert new vales(2) Table are dependent by foreign key for Insert new vales
        //bInsertType=false mean no dependent , bInsertType=true mean dependent
        bool bInsertType = false;

        #endregion

        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["MasterPageNoLeftPanel"])))
                    this.MasterPageFile = Convert.ToString(Session["MasterPageNoLeftPanel"]);
                else
                    Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(Request.Url.ToString()));
            }
            catch (Exception ex)
            {
                Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(Request.Url.ToString()));
            }
            
        }


        #region [ Page Load Event  ]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckPermission();
                ViewState["Title"] = "";
                BindDropDown(false);
                lnkAdd.Text = "Add " + GetTitle(ddlLookUpTables.SelectedItem.Text);
                ViewState["SortBothActive"] = "";
                ViewState["SortBothInActive"] = "";


            }

        }
        #endregion

        #region [ Security for Look up tables ]
        private void CheckPermission()
        {
            // Do not remove this try..catch...block
            try
            {
                List<String> Permissions = CommonBAL.GetPagePermission(Constant.UserId, "LOOKUPTABLES");
                ViewState["CheckPermission"] = Permissions;

                bool bLookUpSecurity = false;
                //Count checking for those user who does not have any permission yet but their user & password is active.
                if (Permissions.Count > 0)
                {
                    if (!(Permissions.Contains("LOOKUPTABLES.ADD")))
                    {
                        lnkAdd.Enabled = false;
                        lnkAdd.ToolTip = "Permission Protected";
                    }
                    if ((Permissions.Contains("LOOKUPTABLES.DELETE")))
                    {
                        bLookUpSecurity = true;
                    }
                    if ((Permissions.Contains("LOOKUPTABLES.EDIT")))
                    {
                        bLookUpSecurity = true;
                    }
                    if ((Permissions.Contains("LOOKUPTABLES.REACTIVATE")))
                    {
                        bLookUpSecurity = true;
                    }
                    if ((Permissions.Contains("LOOKUPTABLES.VIEW")))
                    {
                        bLookUpSecurity = true;
                    }
                    if (bLookUpSecurity == false)
                    {
                        Response.Redirect("Permission.aspx?msg=LOOKUPTABLES.VIEW");
                    }
                }
                else
                {
                    Response.Redirect("Permission.aspx?msg=LOOKUPTABLES.VIEW");
                }


            }
            catch (Exception ex)
            {
                Response.Redirect("Permission.aspx?msg=LOOKUPTABLES.VIEW");
            }


        }
        #endregion

        #region [ Set Dynamic Table title ]
        public String GetTitle(String tblName)
        {
            string temptitle = "";
            switch (tblName)
            {
                case "BankAccount":
                    temptitle = "Bank Account";
                    break;
                case "ComeBackReason":
                    temptitle = "Come Back Reason";
                    break;
                case "CompanyCategory":
                    temptitle = "Company Category";
                    break;
                case "ContactType":
                    temptitle = "Contact Type";
                    break;
                case "DealerCategory":
                    temptitle = "Dealer Category";
                    break;
                case "DealerDesignation":
                    temptitle = "Dealer Designation";
                    break;
                case "DealerSource":
                    temptitle = "Dealer Source";
                    break;
                case "DealerType":
                    temptitle = "Dealer Type";
                    break;
                case "DocumentType":
                    temptitle = "Document Type";
                    break;
                case "EmployeeType":
                    temptitle = "Employee Type";
                    break;
                case "ExpenseType":
                    temptitle = "Expense Type";
                    break;
                case "JobTitle":
                    temptitle = "Job Title";
                    break;
                case "VendorCategory":
                    temptitle = "Vendor Category";
                    break;
                case "VendorType":
                    temptitle = "Vendor Type";
                    break;
                case "WheelDrive":
                    temptitle = "Wheel Drive";
                    break;
                case "CheckVoidReason":
                    temptitle = "Check Void Reason";
                    break;
                default:
                    temptitle = tblName;
                    break;

            }
            return temptitle;

        }
        #endregion

        #region [ Drop down list Selected Index changed Event  ]
        protected void ddlLookUpTables_SelectedIndexChanged(object sender, EventArgs e)
        {

            lnkAdd.Text = "Add " + GetTitle(ddlLookUpTables.SelectedItem.Text);
            ViewState["Title"] = GetTitle(ddlLookUpTables.SelectedItem.Text.Trim());
            if (ddlLookUpTables.Items.Count != 0)
            {
                if (rdActive.Checked == true)
                {
                    IsActive = 1;
                }
                else
                    IsActive = 0;
                Where = Active(ddlLookUpTables.SelectedItem.Text, IsActive);
                if(ddlLookUpTables.SelectedValue.ToLower().Contains("orgid"))
                {
                    Where = Where + "AND OrgID="+Constant.OrgID;
                }
                if (rdRO.Checked == true)
                {
                    dvAdd.Visible = false;
                    String Statement = "Select " + ddlLookUpTables.SelectedValue + " From " + ddlLookUpTables.SelectedItem.Text + Where;
                    gvViewOnly.DataSource = METAOPTION.BAL.Common.LookupTableRecords(Statement);
                    gvViewOnly.DataBind();
                    rdInActive.Enabled = true;
                }
                else
                {
                    dvAdd.Visible = true;
                    if (IsActive == 1)
                    {

                        gvLookUpTables.Controls.Clear();
                        string PkID = GetId(ddlLookUpTables.SelectedValue);
                        gvLookUpTables.DataKeyNames = new string[] { PkID };
                        String Statement = "Select " + ddlLookUpTables.SelectedValue + " From " + ddlLookUpTables.SelectedItem.Text + Where;
                        gvLookUpTables.DataSource = METAOPTION.BAL.Common.LookupTableRecords(Statement);
                        gvLookUpTables.DataBind();
                        rdInActive.Enabled = true;
                    }
                    else
                    {
                        pnlEdit.Visible = false;
                        gvInActive.Controls.Clear();
                        string PkID = GetId(ddlLookUpTables.SelectedValue);
                        gvInActive.DataKeyNames = new string[] { PkID };
                        String Statement = "Select " + ddlLookUpTables.SelectedValue + " From " + ddlLookUpTables.SelectedItem.Text + Where;
                        gvInActive.DataSource = METAOPTION.BAL.Common.LookupTableRecords(Statement);
                        gvInActive.DataBind();
                        rdInActive.Enabled = true;
                        gvInActive.Visible = true;
                        pnlReActive.Visible = true;
                    }
                }
            }
        }
        #endregion

        #region [ Bind Drowp down list  ]
        void BindDropDown(bool type)
        {
            //this.pnlPopup.Visible = false;


            ddlLookUpTables.DataTextField = "TableName";
            ddlLookUpTables.DataValueField = "ShowColumns";
            ddlLookUpTables.DataSource = objBAL.SelectLookUpTbales(type);
            ddlLookUpTables.DataBind();
            if (rdActive.Checked == true)
            {
                IsActive = 1;
            }
            else
                IsActive = 0;
            Where = Active(ddlLookUpTables.SelectedItem.Text, IsActive);
            if (ddlLookUpTables.SelectedValue.ToLower().Contains("orgid"))
            {
                Where = Where + "AND OrgID=" + Constant.OrgID;
            }
            if (rdRO.Checked == true)
            {
                dvAdd.Visible = false;
                String Statement = "Select " + ddlLookUpTables.SelectedValue + " From " + ddlLookUpTables.SelectedItem.Text + Where;
                ViewState["ViewOnly"] = METAOPTION.BAL.Common.LookupTableRecords(Statement);
                gvViewOnly.DataSource = METAOPTION.BAL.Common.LookupTableRecords(Statement);
                gvViewOnly.DataBind();
                pnlView.Visible = true;
                pnlEdit.Visible = false;
                rdInActive.Enabled = true;
                pnlReActive.Visible = false;
            }
            else
            {
                dvAdd.Visible = true;
                ViewState["Title"] = GetTitle(ddlLookUpTables.SelectedItem.Text.Trim());
                if (IsActive == 1)
                {
                    gvLookUpTables.Controls.Clear();
                    string PkID = GetId(ddlLookUpTables.SelectedValue);
                    gvLookUpTables.DataKeyNames = new string[] { PkID };
                    String Statement = "Select " + ddlLookUpTables.SelectedValue + " From " + ddlLookUpTables.SelectedItem.Text + Where;
                    dt = (DataTable)METAOPTION.BAL.Common.LookupTableRecords(Statement);
                    dv = new DataView(dt);
                    if (ViewState["SortBothActive"].ToString().Length > 0)
                    {
                        dv.Sort = Convert.ToString(ViewState["SortBothActive"]);
                        gvLookUpTables.DataSource = dv;
                    }
                    else
                    {
                        gvLookUpTables.DataSource = dt;
                    }
                    gvLookUpTables.DataBind();
                    rdInActive.Enabled = true;
                    pnlView.Visible = false;
                    pnlEdit.Visible = true;
                }
                else
                {
                    ViewState["Title"] = GetTitle(ddlLookUpTables.SelectedItem.Text.Trim());
                    pnlEdit.Visible = false;
                    gvInActive.Controls.Clear();
                    string PkID = GetId(ddlLookUpTables.SelectedValue);
                    gvInActive.DataKeyNames = new string[] { PkID };
                    String Statement = "Select " + ddlLookUpTables.SelectedValue + " From " + ddlLookUpTables.SelectedItem.Text + Where;
                    gvInActive.DataSource = METAOPTION.BAL.Common.LookupTableRecords(Statement);
                    gvInActive.DataBind();
                    rdInActive.Enabled = true;
                    gvInActive.Visible = true;
                    pnlReActive.Visible = true;
                    pnlView.Visible = false;

                }
            }

        }


        void Bind(bool type)
        {
            //this.pnlPopup.Visible = false;


            if (type == true)
            {
                IsActive = 1;
            }
            else
                IsActive = 0;
            Where = Active(ddlLookUpTables.SelectedItem.Text, IsActive);
            if (ddlLookUpTables.SelectedValue.ToLower().Contains("orgid"))
            {
                Where = Where + "AND OrgID=" + Constant.OrgID;
            }
            if (rdRO.Checked == true)
            {
                String Statement = "Select " + ddlLookUpTables.SelectedValue + " From " + ddlLookUpTables.SelectedItem.Text + Where;
                gvViewOnly.DataSource = METAOPTION.BAL.Common.LookupTableRecords(Statement);
                gvViewOnly.DataBind();
                pnlView.Visible = true;
                pnlEdit.Visible = false;
                rdInActive.Enabled = true;
                pnlReActive.Visible = false;
                dvAdd.Visible = false;
            }
            else
            {
                if (IsActive == 1)
                {
                    gvLookUpTables.Controls.Clear();
                    string PkID = GetId(ddlLookUpTables.SelectedValue);
                    gvLookUpTables.DataKeyNames = new string[] { PkID };
                    String Statement = "Select " + ddlLookUpTables.SelectedValue + " From " + ddlLookUpTables.SelectedItem.Text + Where;
                    gvLookUpTables.DataSource = METAOPTION.BAL.Common.LookupTableRecords(Statement);
                    gvLookUpTables.DataBind();
                    rdInActive.Enabled = true;
                    pnlView.Visible = false;
                    pnlEdit.Visible = true;
                    pnlReActive.Visible = false;
                }
                else
                {
                    pnlEdit.Visible = false;
                    gvInActive.Controls.Clear();
                    string PkID = GetId(ddlLookUpTables.SelectedValue);
                    gvInActive.DataKeyNames = new string[] { PkID };
                    String Statement = "Select " + ddlLookUpTables.SelectedValue + " From " + ddlLookUpTables.SelectedItem.Text + Where;
                    gvInActive.DataSource = METAOPTION.BAL.Common.LookupTableRecords(Statement);
                    gvInActive.DataBind();
                    rdInActive.Enabled = true;
                    gvInActive.Visible = true;
                    pnlReActive.Visible = true;
                    pnlView.Visible = false;

                }
                dvAdd.Visible = true;
            }

        }
        #endregion

        #region [ To make Where Clause dynamic ]
        public String Active(string tblName, int iVal)
        {
            string IsActiveVal = " Where IsActive= ";
            switch (tblName)
            {
                case "CHROME_COLOR_EXTERIOR":
                    IsActiveVal = "";
                    rdInActive.Enabled = false;
                    rdInActive.Checked = false;
                    rdActive.Checked = true;
                    break;
                case "CHROME_COLOR_INTERIOR":
                    IsActiveVal = "";
                    rdInActive.Enabled = false;
                    rdInActive.Checked = false;
                    rdActive.Checked = true;
                    break;
                case "CHROME_YMMBCV":
                    IsActiveVal = "";
                    rdInActive.Enabled = false;
                    rdInActive.Checked = false;
                    rdActive.Checked = true;
                    break;
                case "COLOR_CATEGORIES":
                    IsActiveVal = "";
                    rdInActive.Enabled = false;
                    rdInActive.Checked = false;
                    rdActive.Checked = true;

                    break;
                default:
                    IsActiveVal = IsActiveVal + iVal;
                    break;
            }

            return IsActiveVal;
        }
        #endregion

        #region [ Grid View look up Wread & write page index ]
        protected void gvLookUpTables_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (rdActive.Checked == true)
            {
                IsActive = 1;
            }
            else
                IsActive = 0;
            Where = Active(ddlLookUpTables.SelectedItem.Text, IsActive);
            if (ddlLookUpTables.SelectedValue.ToLower().Contains("orgid"))
            {
                Where = Where + "AND OrgID=" + Constant.OrgID;
            }
            gvLookUpTables.PageIndex = e.NewPageIndex;
            if (ddlLookUpTables.Items.Count != 0)
            {
                String Statement = "Select " + ddlLookUpTables.SelectedValue + " From " + ddlLookUpTables.SelectedItem.Text + Where;
                gvLookUpTables.DataSource = METAOPTION.BAL.Common.LookupTableRecords(Statement);
                gvLookUpTables.DataBind();
            }

        }
        #endregion

        #region [ To get PK field name from Array string]
        public string GetId(string str)
        {
            string id = "";
            string[] split = str.Split(',');

            foreach (string item in split)
            {
                id = item;
                ViewState["PkId"] = id;
                break;
            }
            return id;
        }
        #endregion

        #region [ to make dynamic sql statement without PK id  ]
        public string RemoveId(string SQL)
        {

            string sql = "";
            string[] split = SQL.Split(',');
            int i = 0;
            string sep = " , ";
            foreach (string item in split)
            {
                i += 1;
                if (sql.Trim().Length > 0)
                {
                    sql = sql + sep;
                }
                if (i > 1)
                {
                    sql = sql + item;
                }



            }
            return sql;
        }
        #endregion

        #region [ GridView Selected Index Changed envent ]
        protected void gvLookUpTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                GridView grid = (GridView)sender;
                DataKey key = gvLookUpTables.SelectedDataKey;
                int rowIndex = gvLookUpTables.SelectedIndex;

                string PKID = gvLookUpTables.DataKeys[rowIndex].Value.ToString();
                ViewState["PKID"] = PKID.ToString(); ;
                //Removing PK ID from SQL statement 
                string SQL = RemoveId(ddlLookUpTables.SelectedValue);


                //display the selected value in a pop up
                this.dvLookupTables.Visible = true;
                //  force databinding
                String Statement = "Select " + SQL + " From " + ddlLookUpTables.SelectedItem.Text + "  Where  " + Convert.ToString(ViewState["PkId"]) + " = " + PKID;
                dvLookupTables.DataSource = METAOPTION.BAL.Common.LookupTableRecords(Statement);
                dvLookupTables.DataBind();
               
            
                if (ddlLookUpTables.SelectedValue.ToLower().Contains("orgid"))
                {
                    int CountNoRow = 0;
                    CountNoRow = dvLookupTables.Rows.Count;
                    dvLookupTables.Rows[CountNoRow - 1].Visible = false;
                }

                //  update the contents in the detail panel
                this.updPnlCustomerDetail.Update();
                //  show the modal popup
                this.mdlPopup.Show();
                lbl_errormsg_Edit.Visible = false;


            
        }
        #endregion

        #region [ radion buttopn View Only  changed event  ]
        protected void rdRO_CheckedChanged(object sender, EventArgs e)
        {

            BindDropDown(false);
            lnkAdd.Text = "Add " + GetTitle(ddlLookUpTables.SelectedItem.Text);
            pnlView.Visible = true;
            pnlEdit.Visible = false;
            ddlLookUpTables.AutoPostBack = true;

        }
        #endregion

        #region [ radion buttopn Read & Write changed event ]
        protected void rdRW_CheckedChanged(object sender, EventArgs e)
        {

            BindDropDown(true);
            lnkAdd.Text = "Add " + GetTitle(ddlLookUpTables.SelectedItem.Text);
            dvAdd.Visible = true;
            ViewState["Title"] = GetTitle(ddlLookUpTables.SelectedItem.Text.Trim());

        }
        #endregion

        #region [ Look up tables Row Data Bound ]
        protected void gvLookUpTables_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // loop all data rows
                List<String> Permissions = CommonBAL.GetPagePermission(Constant.UserId, "LOOKUPTABLES");
                if (Permissions.Count > 0)
                {
                    if (!(Permissions.Contains("LOOKUPTABLES.DELETE")))
                    {
                        gvLookUpTables.Columns[1].Visible = false;
                    }
                    if (!(Permissions.Contains("LOOKUPTABLES.EDIT")))
                    {
                        gvLookUpTables.Columns[0].Visible = false;
                    }
                }


                foreach (DataControlFieldCell cell in e.Row.Cells)
                {
                    //Tool tips set for Update button
                    e.Row.Cells[0].ToolTip = "Update";
                    // check all cells in one row
                    foreach (Control control in cell.Controls)
                    {
                        ImageButton button = control as ImageButton;
                        if (button != null && button.CommandName == "Delete")
                            // Add delete confirmation
                            button.OnClientClick = "if (!confirm('Are you sure " +
                                   "you want to delete this record?')) return;";
                    }

                    if (ddlLookUpTables.SelectedValue.ToLower().Contains("orgid"))
                    {
                        int i = e.Row.Cells.Count;
                        e.Row.Cells[i - 1].Visible = false;
                        gvLookUpTables.HeaderRow.Cells[i - 1].Visible = false;
                    }
                }
              }

        }
        #endregion

        #region [ Page render ]
        protected override void Render(HtmlTextWriter writer)
        {

            foreach (GridViewRow r in gvLookUpTables.Rows)
            {

                if (r.RowType == DataControlRowType.DataRow)
                {
                    Page.ClientScript.RegisterForEventValidation
                            (r.UniqueID + "$ctl00");
                }

            }
            foreach (GridViewRow r in gvViewOnly.Rows)
            {

                if (r.RowType == DataControlRowType.DataRow)
                {
                    Page.ClientScript.RegisterForEventValidation
                            (r.UniqueID + "$ctl00");
                }

            }

            Page.ClientScript.RegisterForEventValidation(pnlDocType.ClientID);


            base.Render(writer);

        }
        #endregion

        #region [ Paging for lookup table View Only]
        protected void gvViewOnly_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //this.pnlPopup.Visible = false;
            if (rdActive.Checked == true)
            {
                IsActive = 1;
            }
            else
                IsActive = 0;
            gvViewOnly.PageIndex = e.NewPageIndex;
            Where = Active(ddlLookUpTables.SelectedItem.Text, IsActive);
            if (ddlLookUpTables.SelectedValue.ToLower().Contains("orgid"))
            {
                Where = Where + "AND OrgID=" + Constant.OrgID;
            }
            if (ddlLookUpTables.Items.Count != 0)
            {
                String Statement = "Select " + ddlLookUpTables.SelectedValue + " From " + ddlLookUpTables.SelectedItem.Text + Where;
                gvViewOnly.DataSource = METAOPTION.BAL.Common.LookupTableRecords(Statement);
                gvViewOnly.DataBind();
            }
        }
        #endregion

        #region [ Update Look up tables ]
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                //  hide the detail view control

                this.dvLookupTables.Visible = false;

                //  hide the modal popup
                this.mdlPopup.Hide();
                string strColumns = RemoveId(ddlLookUpTables.SelectedValue);
                /// ArrayList col = String.Join(',', (string[])strColumns.ToCharArray(Type.GetType("System.Char")));
                //ArrayList a = ArrayListToString(col);
                string[] splitCol = strColumns.Split(',');
                string AccountNo = string.Empty;
                Label msg = new Label();
                String Query = " ";
                TextBox myTextBox = new TextBox();
                this.updPnlCustomerDetail.Update();
                int Rowindex = 0;
                int ind = 0;
                String sep = " , ";
                foreach (DetailsViewRow item in dvLookupTables.Rows)
                {
                    DetailsViewRow row = dvLookupTables.Rows[Rowindex];
                    myTextBox = myTextBox = (TextBox)row.Cells[1].Controls[0];
                    // msg=(Label)dvLookupTables.Rows[Rowindex].Cells[1].Controls[0];
                    // msg = (Label)dvLookupTables.Rows[Rowindex].FindControl("lbl_errormsg_Edit");
                    if (ddlLookUpTables.SelectedItem.Text.Trim().Equals("BankAccount"))
                    {
                        AccountNo = myTextBox.Text;
                    }
                    else
                    {

                        if (SQLForUpdateValues.Trim().Length > 0)
                        {
                            SQLForUpdateValues = SQLForUpdateValues + sep;
                        }
                        if (myTextBox != null)
                        {
                            SQLForUpdateValues = SQLForUpdateValues + myTextBox.Text;
                        }
                        Rowindex += 1;
                        ind += 1;
                    }
                }

                if (!ddlLookUpTables.SelectedItem.Text.Trim().Equals("BankAccount"))
                {
                    string[] splitCData = SQLForUpdateValues.Split(',');
                    ArrayList arrListColls = new ArrayList();
                    ArrayList arrListData = new ArrayList();

                    foreach (string item in splitCol)
                    {
                        //add each item to the new ArrayList
                        arrListColls.Add(item.ToString());

                    }
                    foreach (string item in splitCData)
                    {
                        //add each item to the new ArrayList
                        arrListData.Add(item.ToString());

                    }


                    for (int i = 0; i < arrListColls.Count; i++)
                    {
                        if (Query.Trim().Length > 0)
                        {
                            Query = Query + sep;
                        }
                        string Col, Data = string.Empty;
                        Col = arrListColls[i].ToString();
                        Data = "'" + arrListData[i].ToString().Trim() + "'";
                        Query = Query + Col + " = " + Data;
                    }
                }
                switch (ddlLookUpTables.SelectedItem.Text.Trim())
                {
                    case "Bank":
                        Query = Query + ",DateModified=GetDate(),ModifiedBy=" + Constant.UserId;
                        break;
                    //case "BankAccount":
                    //    Query = Query + ",DateModified=GetDate(),ModifiedBy=" + Constant.UserId;
                    //    break;
                    case "Groups":
                        Query = Query + ",DateModified=GetDate(),ModifiedBy=" + Constant.UserId;
                        break;
                }

                if (ddlLookUpTables.SelectedItem.Text.Trim().Equals("BankAccount"))
                {
                    if (!string.IsNullOrEmpty(AccountNo))
                    {
                        int InsertStatus = OBJNEW.newAddBankAccount(Convert.ToInt32(ViewState["PKID"]), AccountNo, Constant.OrgID, Constant.UserId, 1, "UPDATE");
                        if (InsertStatus == -1)
                        {
                            lbl_errormsg_Edit.Text = "<B>Account Number already in use!!</B>";
                            lbl_errormsg_Edit.Visible = true;
                            this.updPnlCustomerDetail.Update();
                            this.dvLookupTables.Visible = true;
                            //  hide the modal popup
                            this.mdlPopup.Show();
                        }
                    }
                    else
                    {
                        lbl_errormsg_Edit.Text = "<B>Account Number can not be empty</B>";
                        lbl_errormsg_Edit.Visible = true;
                        this.updPnlCustomerDetail.Update();
                        this.dvLookupTables.Visible = true;
                        //  hide the modal popup
                        this.mdlPopup.Show();
                    }
                }
                else
                {
                    string SQLUpdateStatement = " Update " + ddlLookUpTables.SelectedItem.Text + " SET " + Query + " Where " + Where + GetId(ddlLookUpTables.SelectedValue) + " = " + Convert.ToUInt64(ViewState["PKID"]);
                    int a = METAOPTION.BAL.Common.UpdateLookupTableRecords(SQLUpdateStatement);
                }
                //  refresh the grid so we can see our changed
                #region "[Load Grid]"
                Bind(true);
                #endregion

            }
        }
        #endregion

        #region [ Look up tables deleting ]
        protected void gvLookUpTables_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            long LookupId = Convert.ToInt64(gvLookUpTables.DataKeys[e.RowIndex].Value);

            String SQL = "Update " + ddlLookUpTables.SelectedItem.Text + " Set  IsActive =0 Where " + GetId(ddlLookUpTables.SelectedValue) + " = " + LookupId;
            int delStatus = METAOPTION.BAL.Common.DeleteLookupTableRecords(SQL);
            Bind(true);

        }
        #endregion

        #region [ View Only Active Looh up tables ]
        protected void rdActive_CheckedChanged(object sender, EventArgs e)
        {
            Bind(true);

        }
        #endregion

        #region [ View Only In Active Look up tables ]
        protected void rdInActive_CheckedChanged(object sender, EventArgs e)
        {
            Bind(false);
        }
        #endregion

        #region [ Re-Activate Look up tables ]
        protected void gvInActive_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            long LookupId = Convert.ToInt64(gvInActive.DataKeys[e.RowIndex].Value);
            String SQL = "Update " + ddlLookUpTables.SelectedItem.Text + " Set  IsActive = 1 Where " + GetId(ddlLookUpTables.SelectedValue) + " = " + LookupId;
            int delStatus = METAOPTION.BAL.Common.DeleteLookupTableRecords(SQL);
            Bind(false);

        }
        #endregion

        #region [ Add New Look up Entry ]
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            //Get Insert Type
            bInsertType = GetInsertType(ddlLookUpTables.SelectedItem.Text.Trim());
            String Statement = "";
            string SQL = "";
            //Table are not dependendent for insert new Values
            if (bInsertType == false)
            {
                if (rdRW.Checked == true)
                {
                    SQL = RemoveId(ddlLookUpTables.SelectedValue);
                    //display the selected value in a pop up
                    this.dvAddIndependent.Visible = true;
                    //  force databinding
                    Statement = "Select " + SQL + " From " + ddlLookUpTables.SelectedItem.Text;
                    dvAddIndependent.DataSource = METAOPTION.BAL.Common.LookupTableRecords(Statement);
                    dvAddIndependent.DataBind();
                    //  update the contents in the detail panel
                    this.uppAddIndependentTable.Update();
                    //  show the modal popup
                    this.mpeShowIndependent.Show();
                }
            }
            else if (bInsertType == true)//Table are dependendent for insert new Values
            {
                if (rdRW.Checked == true)
                {
                    switch (ddlLookUpTables.SelectedItem.Text.Trim())
                    {
                        case "BankAccount":
                            SQL = RemoveId(ddlLookUpTables.SelectedValue);
                            SQL = "BankID," + SQL;
                            //display the selected value in a pop up
                            this.dvAddBankAccount.Visible = true;
                            //  force databinding
                            Statement = "Select " + SQL + " From " + ddlLookUpTables.SelectedItem.Text;
                            dvAddBankAccount.DataSource = METAOPTION.BAL.Common.LookupTableRecords(Statement);
                            dvAddBankAccount.DataBind();
                            //  update the contents in the detail panel
                            this.upBankAccount.Update();
                            //  show the modal popup
                            this.mdpBankAccount.Show();
                            break;
                        case "DocumentType":
                            SQL = RemoveId(ddlLookUpTables.SelectedValue);
                            SQL = "EntityTypeId," + SQL;
                            //display the selected value in a pop up
                            this.dvAddDocumentType.Visible = true;
                            //  force databinding
                            Statement = "Select " + SQL + " From " + ddlLookUpTables.SelectedItem.Text;
                            dvAddDocumentType.DataSource = METAOPTION.BAL.Common.LookupTableRecords(Statement);
                            dvAddDocumentType.DataBind();
                            //  update the contents in the detail panel
                            this.upDocumentType.Update();
                            //  show the modal popup
                            this.mdpDocumentType.Show();
                            break;
                        case "Groups":
                            SQL = RemoveId(ddlLookUpTables.SelectedValue);
                            //display the selected value in a pop up
                            this.dvAddDocumentType.Visible = true;
                            //  force databinding
                            Statement = "Select " + SQL + " From " + ddlLookUpTables.SelectedItem.Text;
                            dvGroups.DataSource = METAOPTION.BAL.Common.LookupTableRecords(Statement);
                            dvGroups.DataBind();
                            //  update the contents in the detail panel
                            this.upGroups.Update();
                            //  show the modal popup
                            this.mdpGroups.Show();
                            break;
                        case "State":
                            SQL = RemoveId(ddlLookUpTables.SelectedValue);
                            //display the selected value in a pop up
                            this.dvState.Visible = true;
                            //  force databinding
                            Statement = "Select " + SQL + " From " + ddlLookUpTables.SelectedItem.Text;
                            dvGroups.DataSource = METAOPTION.BAL.Common.LookupTableRecords(Statement);
                            dvGroups.DataBind();
                            //  update the contents in the detail panel
                            this.upGroups.Update();
                            //  show the modal popup
                            this.mdpState.Show();
                            break;
                    }
                }
            }


        }
        #endregion

        #region [ Make sense type of table to insert new records]
        public bool GetInsertType(string tblName)
        {
            if (("BankAccount" == tblName.Trim()) || ("DocumentType" == tblName.Trim()) || ("Groups" == tblName.Trim()) || ("State" == tblName.Trim()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region [ Add data into Independent(without foreign key entry) Loouk Up lookup tables ]
        protected void btnAddIndependentLooukUp_Click(object sender, EventArgs e)
        {
            TextBox myTextBox = new TextBox();
            DropDownList myDropDown = new DropDownList();
            String SQL = "";
            string strColumns = RemoveId(ddlLookUpTables.SelectedValue);
            int Rowindex = 0;
            int ind = 0;
            String sep = " , ";
            foreach (DetailsViewRow item in dvAddIndependent.Rows)
            {
                DetailsViewRow row = dvAddIndependent.Rows[Rowindex];
                myTextBox = (TextBox)row.Cells[1].Controls[0];
                if (SQLForUpdateValues.Trim().Length > 0)
                {
                    SQLForUpdateValues = SQLForUpdateValues + sep;
                }
                if (myTextBox != null)
                {
                    SQLForUpdateValues = SQLForUpdateValues + "'" + myTextBox.Text + "'";
                }

                Rowindex += 1;
                ind += 1;


            }

            switch (ddlLookUpTables.SelectedItem.Text.Trim())
            {
                case "Bank":
                    SQLForUpdateValues = "Values (" + SQLForUpdateValues + ",1,GetDate()," + Constant.UserId + ")";
                    SQL = "INSERT INTO " + ddlLookUpTables.SelectedItem.Text + "( " + strColumns + ",IsActive,DateAdded,AddedBy)" + SQLForUpdateValues;
                    break;
                default:
                    SQLForUpdateValues = "Values (" + SQLForUpdateValues + ",1)";
                    SQL = "INSERT INTO " + ddlLookUpTables.SelectedItem.Text + "( " + strColumns + ",IsActive)" + SQLForUpdateValues;
                    break;
            }
            int InsertStatus = METAOPTION.BAL.Common.DeleteLookupTableRecords(SQL);

            Bind(true);
        }
        #endregion

        #region [Insert Bank Account]
        protected void btnBankAccountAdd_Click(object sender, EventArgs e)
        {
            TextBox myTextBox = new TextBox();
            DropDownList myDropDown = new DropDownList();
            Label msg=new Label();
          //  String SQL = "";
          //  string strColumns = RemoveId(ddlLookUpTables.SelectedValue);
           // strColumns = "BankID," + strColumns;
          //  String sep = " , ";
            int BankID = 0;
            string AccNumber = string.Empty;
            foreach (DetailsViewRow item in dvAddBankAccount.Rows)
            {
                myDropDown = (DropDownList)item.Controls[0].FindControl("ddlBankId");
                myTextBox = (TextBox)item.Controls[0].FindControl("txtAccountNumber");
                msg=(Label)item.Controls[0].FindControl("lbl_errormsg");

                  BankID = Convert.ToInt32(myDropDown.SelectedValue);
                  AccNumber= myTextBox.Text.ToString();

                //if (myDropDown != null)
                //{
                //    SQLForUpdateValues = SQLForUpdateValues + Convert.ToInt32(myDropDown.SelectedValue);
                //}

                //if (SQLForUpdateValues.Trim().Length > 0)
                //{
                //    SQLForUpdateValues = SQLForUpdateValues + sep;
                //}
                //if (myTextBox != null)
                //{
                //    SQLForUpdateValues = SQLForUpdateValues + "'" + myTextBox.Text + "'";
                //}
                //if (SQLForUpdateValues.Trim().Length > 0)
                //{
                //    SQLForUpdateValues = SQLForUpdateValues + sep;
                //}//newAddBankAccount(int BankID, string AccNumber, Int16 OrgID, Int64 AddedBy)

              
                break;
            }
            //SQLForUpdateValues = "Values (" + SQLForUpdateValues + Constant.OrgID +",1,GetDate()," + Constant.UserId + ")";
            //SQL = "INSERT INTO " + ddlLookUpTables.SelectedItem.Text + "( " + strColumns + ",IsActive,DateAdded,AddedBy)" + SQLForUpdateValues;
            //int InsertStatus = METAOPTION.BAL.Common.DeleteLookupTableRecords(SQL);

            if (!string.IsNullOrEmpty(AccNumber))
            { 
            int InsertStatus = OBJNEW.newAddBankAccount(BankID, AccNumber, Constant.OrgID, Constant.UserId, 1,"NEW");
            if (InsertStatus == -1)
            {
                msg.Text = "<B>Account Number already in use!!</B>";
                msg.Visible = true;
                this.upBankAccount.Update();
                //  show the modal popup
                this.mdpBankAccount.Show();

              }
              else
                { Bind(true); }
            }
            else
            {
                msg.Text = "<B>Please enter an account number</B>";
                msg.Visible = true;
                this.upBankAccount.Update();
                //  show the modal popup
                this.mdpBankAccount.Show();
            }

            
        }
        #endregion

        #region [ Add doculemt type]
        protected void btnAddDocumentType_Click(object sender, EventArgs e)
        {
            TextBox myTextBox = new TextBox();
            DropDownList myDropDown = new DropDownList();
            String SQL = "";
            string strColumns = RemoveId(ddlLookUpTables.SelectedValue);
            strColumns = "EntityTypeId," + strColumns;
            String sep = " , ";
            foreach (DetailsViewRow item in dvAddDocumentType.Rows)
            {
                myDropDown = (DropDownList)item.Controls[0].FindControl("ddlDocumentType");
                myTextBox = (TextBox)item.Controls[0].FindControl("txtDocumentType");

                if (myDropDown != null)
                {
                    SQLForUpdateValues = SQLForUpdateValues + Convert.ToInt32(myDropDown.SelectedValue);
                }

                if (SQLForUpdateValues.Trim().Length > 0)
                {
                    SQLForUpdateValues = SQLForUpdateValues + sep;
                }
                if (myTextBox != null)
                {
                    SQLForUpdateValues = SQLForUpdateValues + "'" + myTextBox.Text + "'";
                }
                if (SQLForUpdateValues.Trim().Length > 0)
                {
                    SQLForUpdateValues = SQLForUpdateValues + sep;
                }
                break;
            }
            SQLForUpdateValues = "Values (" + SQLForUpdateValues +Constant.OrgID+",1)";
            SQL = "INSERT INTO " + ddlLookUpTables.SelectedItem.Text + "( " + strColumns + ",IsActive)" + SQLForUpdateValues;
            int InsertStatus = METAOPTION.BAL.Common.DeleteLookupTableRecords(SQL);

            Bind(true);
        }
        #endregion

        #region [ Add Group]
        protected void btnAddGroup_Click(object sender, EventArgs e)
        {
            TextBox txtAbb = new TextBox();
            TextBox txtGroup = new TextBox();
            DropDownList ddlStatus = new DropDownList();
            String SQL = "";
            string strColumns = RemoveId(ddlLookUpTables.SelectedValue);
            strColumns = "GroupStatus," + strColumns;
            String sep = " , ";
            foreach (DetailsViewRow item in dvGroups.Rows)
            {
                ddlStatus = (DropDownList)item.Controls[0].FindControl("ddlGroupStatus");
                txtAbb = (TextBox)item.Controls[0].FindControl("txtGroupAbbreviation");
                txtGroup = (TextBox)item.Controls[0].FindControl("txtGroupName");
                if (ddlStatus != null)
                {
                    SQLForUpdateValues = SQLForUpdateValues + Convert.ToInt32(ddlStatus.SelectedValue);
                }

                if (SQLForUpdateValues.Trim().Length > 0)
                {
                    SQLForUpdateValues = SQLForUpdateValues + sep;
                }
                if (txtAbb != null)
                {
                    SQLForUpdateValues = SQLForUpdateValues + "'" + txtAbb.Text + "'";
                }
                if (SQLForUpdateValues.Trim().Length > 0)
                {
                    SQLForUpdateValues = SQLForUpdateValues + sep;
                }
                if (txtGroup != null)
                {
                    SQLForUpdateValues = SQLForUpdateValues + "'" + txtGroup.Text + "'";
                }
                if (SQLForUpdateValues.Trim().Length > 0)
                {
                    SQLForUpdateValues = SQLForUpdateValues + sep;
                }
                break;
            }
            SQLForUpdateValues = "Values (" + SQLForUpdateValues +Constant.OrgID+",1,GetDate()," + Constant.UserId + ")";
            SQL = "INSERT INTO " + ddlLookUpTables.SelectedItem.Text + "( " + strColumns + ",IsActive,DateAdded,AddedBy)" + SQLForUpdateValues;
            int InsertStatus = METAOPTION.BAL.Common.DeleteLookupTableRecords(SQL);
            Bind(true);
        }
        #endregion

        #region [ Add State]
        protected void btnAddState_Click(object sender, EventArgs e)
        {
            TextBox txtStateCode = new TextBox();
            TextBox txtState = new TextBox();
            DropDownList ddlCountry = new DropDownList();
            String SQL = "";
            string strColumns = RemoveId(ddlLookUpTables.SelectedValue);
            strColumns = "CountryId," + strColumns;
            String sep = " , ";
            foreach (DetailsViewRow item in dvState.Rows)
            {
                ddlCountry = (DropDownList)item.Controls[0].FindControl("ddlCountry");
                txtStateCode = (TextBox)item.Controls[0].FindControl("txtStateCode");
                txtState = (TextBox)item.Controls[0].FindControl("txtState");
                if (ddlCountry != null)
                {
                    SQLForUpdateValues = SQLForUpdateValues + Convert.ToInt32(ddlCountry.SelectedValue);
                }

                if (SQLForUpdateValues.Trim().Length > 0)
                {
                    SQLForUpdateValues = SQLForUpdateValues + sep;
                }
                if (txtStateCode != null)
                {
                    SQLForUpdateValues = SQLForUpdateValues + "'" + txtStateCode.Text + "'";
                }
                if (SQLForUpdateValues.Trim().Length > 0)
                {
                    SQLForUpdateValues = SQLForUpdateValues + sep;
                }
                if (txtState != null)
                {
                    SQLForUpdateValues = SQLForUpdateValues + "'" + txtState.Text + "'";
                }
                break;
            }
            SQLForUpdateValues = "Values (" + SQLForUpdateValues + ",1)";
            SQL = "INSERT INTO " + ddlLookUpTables.SelectedItem.Text + "( " + strColumns + ",IsActive)" + SQLForUpdateValues;
            int InsertStatus = METAOPTION.BAL.Common.DeleteLookupTableRecords(SQL);
            Bind(true);
        }
        #endregion

        #region [ Apply sorting on Grid ]
        protected void gvViewOnly_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridViewOnly(sortExpression, " DESC");
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridViewOnly(sortExpression, " ASC");
            }


        }
        protected SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                {
                    ViewState["sortDirection"] = SortDirection.Ascending;
                }

                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }
        protected void SortGridViewOnly(string sortExpression, string direction)
        {

            if (rdActive.Checked == true)
            {
                IsActive = 1;
            }
            else
                IsActive = 0;
            Where = Active(ddlLookUpTables.SelectedItem.Text, IsActive);
            if (ddlLookUpTables.SelectedValue.ToLower().Contains("orgid"))
            {
                Where = Where + "AND OrgID=" + Constant.OrgID;
            }
            String Statement = "Select " + ddlLookUpTables.SelectedValue + " From " + ddlLookUpTables.SelectedItem.Text + Where;

            DataTable dt = (DataTable)METAOPTION.BAL.Common.LookupTableRecords(Statement);
            dv = new DataView(dt);
            dv.Sort = sortExpression + direction;
            gvViewOnly.DataSource = dv;
            gvViewOnly.DataBind();

        }
        protected void SortGridViewActive(string sortExpression, string direction)
        {
            if (rdActive.Checked == true)
            {
                IsActive = 1;
            }
            else
                IsActive = 0;
            Where = Active(ddlLookUpTables.SelectedItem.Text, IsActive);
            if (ddlLookUpTables.SelectedValue.ToLower().Contains("orgid"))
            {
                Where = Where + "AND OrgID=" + Constant.OrgID;
            }
            String Statement = "Select " + ddlLookUpTables.SelectedValue + " From " + ddlLookUpTables.SelectedItem.Text + Where;
            dt = (DataTable)METAOPTION.BAL.Common.LookupTableRecords(Statement);
            dv = new DataView(dt);
            dv.Sort = sortExpression + direction; ;
            gvLookUpTables.DataSource = dv;
            gvLookUpTables.DataBind();

        }

        protected void SortGridViewReActive(string sortExpression, string direction)
        {

            if (rdActive.Checked == true)
            {
                IsActive = 1;
            }
            else
                IsActive = 0;
            Where = Active(ddlLookUpTables.SelectedItem.Text, IsActive);
            if (ddlLookUpTables.SelectedValue.ToLower().Contains("orgid"))
            {
                Where = Where + "AND OrgID=" + Constant.OrgID;
            }
            String Statement = "Select " + ddlLookUpTables.SelectedValue + " From " + ddlLookUpTables.SelectedItem.Text + Where;
            dt = (DataTable)METAOPTION.BAL.Common.LookupTableRecords(Statement);
            dv = new DataView(dt);
            dv.Sort = sortExpression + direction; ;
            gvInActive.DataSource = dv;
            gvInActive.DataBind();

        }

        protected void gvLookUpTables_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridViewActive(sortExpression, " DESC");
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridViewActive(sortExpression, " ASC");
            }
        }

        protected void gvInActive_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridViewReActive(sortExpression, " DESC");
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridViewReActive(sortExpression, " ASC");
            }
        }

        #endregion

        #region [ gvInActive RowDataBound ]
        protected void gvInActive_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // loop all data rows
                foreach (DataControlFieldCell cell in e.Row.Cells)
                {
                    List<String> Permissions = CommonBAL.GetPagePermission(Constant.UserId, "LOOKUPTABLES");
                    if (Permissions.Count > 0)
                    {
                        if (!(Permissions.Contains("LOOKUPTABLES.REACTIVATE")))
                        {
                            gvInActive.Columns[0].Visible = false;
                        }

                    }
                    // check all cells in one row
                    foreach (Control control in cell.Controls)
                    {
                        ImageButton button = control as ImageButton;
                        if (button != null && button.CommandName == "Delete")
                            // Add delete confirmation
                            button.OnClientClick = "if (!confirm('Are you sure " +
                                   "you want to re-activate this record?')) return;";
                    }

                    if(ddlLookUpTables.SelectedValue.ToLower().Contains("orgid"))
                    {
                        int cellcount=e.Row.Cells.Count;
                        e.Row.Cells[cellcount-1].Visible=false;
                        gvInActive.HeaderRow.Cells[cellcount-1].Visible=false;

                    }
                }
            }
        }
        #endregion
    }
}
