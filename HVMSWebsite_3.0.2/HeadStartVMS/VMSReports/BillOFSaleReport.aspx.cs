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
    public partial class BillOFSaleReport : System.Web.UI.Page
    {
        
        MasterBAL objMaster = new MasterBAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               BindInventory();
            }
        }

        #region Bind Inventory
        private void BindInventory()
        {
            ddlInventory.DataSource = objMaster.GetInventoryId();  
            ddlInventory.DataValueField = "InventoryId";
            ddlInventory.DataTextField = "InventoryId";
            ddlInventory.DataBind();
            ddlInventory.Items.Insert(0, new ListItem("ALL", "-1"));
            ddlInventory.SelectedIndex = -1;

        }


        #endregion
        
        /// <summary>
        /// Render report according to the report name and parameters
        /// </summary>
        /// <param name="prams"></param>
        /// <param name="reportName"></param>
        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            ReportParameter[] parameters = new ReportParameter[3];

            parameters[0] = new ReportParameter("InventoryId", ddlInventory.SelectedValue);
            parameters[1] = new ReportParameter("UserId",Constant.UserId.ToString());
            parameters[2] = new ReportParameter("systemId", Session["SystemID"].ToString());

            ReportParameters.Parameters = parameters;
            ReportParameters.ReportName = "/Hollenshead/BillOFSaleReport";
            Response.Redirect("Reports.aspx?ReturnURL=BillOFSaleReport.aspx");
        }
    }
}

