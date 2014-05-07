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
using METAOPTION;
using METAOPTION.BAL;
using METAOPTION.DAL;
namespace HeadStartVMS.UI
{

    public partial class ValidateVin : System.Web.UI.Page, IPagePermission
    {
        public const string PAGE = "INVENTORY";
        public const string PAGERIGHT = "INVENTORY.VIEW";
        String MailFrom = ConfigurationManager.AppSettings["MailFrom"];

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
            //Check User Level Page Permission for INVENTORY.VIEW Right i.e Search Inventories based on VIN
            CheckUserPagePermissions();
        }

        #region ApplyPagePermission Members
        /// <summary>
        ///  Check User Level Page Permission for INVENTORY.VIEW Right i.e Search Inventories based on VIN
        /// </summary>
       public  void CheckUserPagePermissions()
        {
            //Note:-There might be a user, Who can quick Search Inventories based on VIN but don't 
            //have prievelege to ADD New Inventory 

            System.Collections.Generic.List<string> dict = CommonBAL.GetPagePermission(Constant.UserId, PAGE);
            bool bTrue = true;
            if (!dict.Contains(PAGERIGHT))
            {
                bTrue = false;
            }

            //Redirect User to Common Page for InSufficient Rights
            if (!(dict.Contains(PAGERIGHT) || bTrue))
                Response.Redirect("Permission.aspx?MSG=INVENTORY.VIEW");
        }

        #endregion
        
        /// <summary>
        /// Click event of Next Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (txtVinNo.Text.Trim().Length < 17)
            {
                Master.PageMessage = "Vin No should be of 17 Characters";

                gvVinMatchData.DataSource = null;
                gvVinMatchData.DataBind();

            }
            else
            {
                BindVINData();
            }
        }
        /// <summary>
        /// Bind VIN DATA
        /// </summary>
        private void BindVINData()
        {
            #region[Old Code]
            /*
            //Replace 9 the character of VIN# With _ for matching any character
            string strVinNo = Common.GetVinNo(txtVinNo.Text);

            //Bind Gridview with matched VIN Numbers
            gvVinMatchData.DataSource = InventoryBAL.GetMatchedVinNumbers(strVinNo);
            gvVinMatchData.DataBind();

            //Check Gridview row collection, If No row created display user no Vin Number found again avaialble VIN patterns.
            if (gvVinMatchData.Rows.Count < 1)
            {
                Master.PageMessage = "VIN# provided by you is Not Valid, Please either provide Valid VIN# or Click on “I don’t have VIN# button";
            }
            */
            #endregion

            #region[New Code - CDM Integration]
            //Replace 9 the character of VIN# With _ for matching any character
            string strVinNo = METAOPTION.BAL.Common.GetVinNo(txtVinNo.Text);

            //Bind Gridview with matched VIN Numbers
            gvVinMatchData.DataSource = InventoryBAL.GetMatchedVinNumbers(strVinNo);
            gvVinMatchData.DataBind();


            //Check Gridview row collection, If No row created display user no Vin Number found again avaialble VIN patterns.
            if (gvVinMatchData.Rows.Count < 1 && ViewState["SrvCallCounter"] == null)
            {
                mpeConfirmVin.Show();
                //   Master.PageMessage = "VIN# provided by you is Not Valid, Please either provide Valid VIN# or Click on “I don’t have VIN# button";
            }
            else if (gvVinMatchData.Rows.Count < 1 && ViewState["SrvCallCounter"].ToString() == "1")
            {
                Master.PageMessage = "Details for the Entered VIN is not found.";
            }
            ViewState["SrvCallCounter"] = null;
            #endregion
        }

        /// <summary>
        /// User proceed with without VIN#
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnWithoutVin_Click(object sender, EventArgs e)
        {

            //Redirect user to AddInventory Page with selected Year,Make,Mode,Body
            Response.Redirect("AddInventory.aspx");
 
        }

        /// <summary>
        /// Handle Row Created evetn for changing css on mouse over and out for row selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvVinMatchData_RowCreated(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    // when mouse is over the row, save original color to new attribute, and change it to highlight yellow color

            //    e.Row.Attributes.Add("onmouseover",

            //  "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#E3F2F9';this.style.cursor='hand'");


            //    // when mouse leaves the row, change the bg color to its original value    

            //    e.Row.Attributes.Add("onmouseout",

            //    "this.style.backgroundColor=this.originalstyle;");

            //    //    //e.Row.Attributes.Add("onclick",

            //    //    e.Row.Attributes["OnClick"] = ClientScript.GetPostBackEventReference(this, "Select$" + e.Row.RowIndex); 

            //    //}


            //}


        }
        /// <summary>
        /// Override Render event for Invalid Page Postback Event Valdidation error and set unique id
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow r in gvVinMatchData.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    Page.ClientScript.RegisterForEventValidation
                            (r.UniqueID + "$ctl00");
                 
                    //Page.ClientScript.RegisterForEventValidation
                    //        (r.UniqueID + "$ctl01");
                }
            }

            base.Render(writer);
        }

        /// <summary>
        /// Handle Row Command Event to redirect user with selected row to Add InventoryScreen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvVinMatchData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        /// <summary>
        /// Row data bound event of Gridview to regiester javascipt on click of link button(asp:ButtonField)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvVinMatchData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Hide ID columns
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                //FOR ROW
               // e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
            }

        }

        protected void imgbtnSelect_Click(object sender, ImageClickEventArgs e)
        {

                ImageButton imgbtnSelect = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgbtnSelect.NamingContainer;
           

                //string _commandName = e.CommandName;
                int Year = 0;
                int MakeId = 0;
                int ModelId = 0;
                int BodyId = 0;
                

                //Fetch Selected row values to be passed in querystring;
                if (!string.IsNullOrEmpty(row.Cells[1].Text))
                    Year = Convert.ToInt32(row.Cells[1].Text);
                if (!string.IsNullOrEmpty(row.Cells[2].Text))
                    MakeId = Convert.ToInt32(row.Cells[2].Text);

                if (!string.IsNullOrEmpty(row.Cells[3].Text))
                    ModelId = Convert.ToInt32(row.Cells[3].Text);

                if (!string.IsNullOrEmpty(row.Cells[4].Text))
                    BodyId = Convert.ToInt32(row.Cells[4].Text);


                //Redirect user to AddInventory Page with selected Year,Make,Mode,Body
                Response.Redirect("AddInventory.aspx?VIN=" + txtVinNo.Text.Trim() + "&Year=" + Year + "&MakeId=" + MakeId + "&ModelId=" + ModelId + "&BodyId=" + BodyId + " ");
        }

        #region[btnConfirmYes_Click (CDM Related)]
        protected void btnConfirmYes_Click(object sender, EventArgs e)
        {
            try
            {
                CDMRepository CDM = new CDMRepository();
                string response = CDM.DecodeVin(txtVinNo.Text.Trim());
                if (response == "Success")
                {
                    ViewState["SrvCallCounter"] = "1";
                    BindVINData();
                }
                else
                {
                    ViewState["SrvCallCounter"] = null;
                    Master.PageMessage = response.ToString();
                    //   Master.PageMessage = "VIN# provided by you is Not Valid, Please either provide Valid VIN# or Click on “I don’t have VIN# button";
                }

            }
            catch (Exception ex)
            {
                LoggerService.LogServiceError(ex, String.Empty, this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);

                #region[Log email notification]
                String error = String.Format(
                        @"{0}<br /><b>Message:</b> {1}<br /><b>Time:</b> {2}<br /><b>ClassFileName:</b> {3}<br /><b>MethodName:</b> {4}<br /><b>VIN:</b> {5}"
                        , ex.InnerException
                        , ex.Message
                        , System.DateTime.Now
                        , this.GetType().Name.ToString()
                        , System.Reflection.MethodBase.GetCurrentMethod().Name
                        , txtVinNo.Text);

                String MailBody = "<div style='padding:5px;font-family:Verdana;font-size:12px'><b>Error Details:</b> " + error + "</div>";
                EmployeeListBAL objBal = new EmployeeListBAL();
                objBal.LogMailContent("HVMS-CDM service faces an issue", MailBody, 6001, "fakhruddin.ali@metaoption.com", MailFrom, "adesh.sharma@metaoption.com", "ashar.ilyas@metaoption.com", null);
                #endregion
            }
        }
        #endregion

        #region[btnConfirmNo_Click (CDM Related)]
        protected void btnConfirmNo_Click(object sender, EventArgs e)
        {
            mpeConfirmVin.Hide();
        }
        #endregion
       
    }
}
