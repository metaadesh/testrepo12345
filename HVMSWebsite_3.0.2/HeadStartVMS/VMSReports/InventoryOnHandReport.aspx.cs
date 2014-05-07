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
using Microsoft.Reporting;
using Microsoft.Reporting.WebForms;
using METAOPTION.UI;
using METAOPTION.BAL;



namespace METAOPTION.Reports
{
    public partial class InventoryOnHandReport : System.Web.UI.Page
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindYears();
               
                //BindDealers(); Naushad sir requested, need to be implemented as a popup later on.
            }
        }
        #region Bind Years
        /// <summary>
        /// Bind Years in dropdownlist
        /// </summary>
        private void BindYears()
        {
            /*Load Years*/
            ddlYear.DataSource = Common.GetYearList();
            ddlYear.DataValueField = "Year";
            ddlYear.DataTextField = "Year";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, new ListItem("ALL", "-1"));
            ddlYear.SelectedIndex = 0;

            //Bind Make Details for Selected Year
            if (!string.IsNullOrEmpty(ddlYear.SelectedValue))
                BindMake(Convert.ToInt32(ddlYear.SelectedValue));


        }
        #endregion

        #region Bind Make
        /// <summary>
        /// Bind Dropdownlist with make details based on year selected
        /// </summary>
        private void BindMake(int year)
        {
            /*Bind Make DropDownlist*/
            ddlMake.DataSource = Common.GetMakes(year);
            ddlMake.DataTextField = "Make";
            ddlMake.DataValueField = "MakeId";
            ddlMake.DataBind();
            ddlMake.Items.Insert(0, new ListItem("ALL", "-1"));
            //Bind Model this selected Make
            if (!string.IsNullOrEmpty(ddlMake.SelectedValue))
            {
                BindModel(Convert.ToInt32(ddlMake.SelectedValue));
            }
        }

        #endregion

        #region Bind Model
        /// <summary>
        /// Bind dropdownlist with Model details based on make selected
        /// </summary>
        private void BindModel(int makeId)
        {
            /*Bind Model DropDownlist*/
            ddlModel.DataSource = BAL.Common.GetModel(makeId);
            ddlModel.DataTextField = "Model";
            ddlModel.DataValueField = "ModelId";
            ddlModel.DataBind();
            ddlModel.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        #endregion

   
        #region Year selected index change
        /// <summary>
        /// Year selected index change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Fetch Makes based on selected Year
            if (!string.IsNullOrEmpty(ddlYear.SelectedValue))
                BindMake(Convert.ToInt32(ddlYear.SelectedValue));
        }
        #endregion

        #region Make selected index change
        /// <summary>
        /// Make selected index change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Fetch Body and Model for selected Make
            if (!string.IsNullOrEmpty(ddlMake.SelectedValue))
                BindModel(Convert.ToInt32(ddlMake.SelectedValue));
        }
        #endregion

        /// <summary>
        /// Render report according to the report name and parameters
        /// </summary>
        /// <param name="prams"></param>
        /// <param name="reportName"></param>
        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            ReportParameter[] parameters = new ReportParameter[7];
            parameters[0] = new ReportParameter("year", ddlYear.SelectedValue);
            parameters[1] = new ReportParameter("make", ddlMake.SelectedValue);
            parameters[2] = new ReportParameter("model", ddlModel.SelectedValue);
            parameters[3] = new ReportParameter("UserId", Constant.UserId.ToString());
            parameters[4] = new ReportParameter("carstatus", ddlCarStatus.SelectedValue);
            parameters[5] = new ReportParameter("systemId", Session["SystemID"].ToString());
            parameters[6] = new ReportParameter("OrgID", Constant.OrgID.ToString());

            ReportParameters.Parameters = parameters;
            ReportParameters.ReportName = "/Hollenshead/InventoryOnHand";

            Response.Redirect("Reports.aspx?ReturnURL=InventoryOnHandReport.aspx");

        }
    }
}

