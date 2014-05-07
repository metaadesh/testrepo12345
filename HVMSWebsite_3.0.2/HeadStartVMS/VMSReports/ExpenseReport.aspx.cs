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
    public partial class ExpenseReport : System.Web.UI.Page
    {
      

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.txtStartDate.Text = DateTime.Today.AddDays(-7).ToShortDateString();
                this.txtEndDate.Text = DateTime.Today.ToShortDateString();
                BindEntityType();
                BindExpenseType(); 
                
            }
        }
        
        #region Bind EntityType
        protected void BindEntityType()
        {
            MasterBAL objMaster = new MasterBAL(); 
            ddlEntityType.DataSource = objMaster.GetEntityType();
            ddlEntityType.DataTextField = "EntityType";
            ddlEntityType.DataValueField = "EntityTypeId";
            ddlEntityType.DataBind();
            ddlEntityType.Items.Insert(0, new ListItem("ALL", "-1"));
            ddlEntityType.SelectedValue = "-1";

            //Bind entity name for selected type
            if (!string.IsNullOrEmpty(ddlEntityType.SelectedValue))
                BindEntitiy();

        }
        #endregion

        #region Bind ExpenseType
        protected void BindExpenseType()
        {
            InventoryBAL objExpense = new InventoryBAL();
            MasterBAL objMaster = new MasterBAL(); 
            ddlExpenseType.DataSource=objExpense.GetExpenses();
            ddlExpenseType.DataTextField="ExpenseType";
            ddlExpenseType.DataValueField="ExpenseTypeId";
            ddlExpenseType.DataBind();
            ddlExpenseType.Items.Insert(0,new ListItem ("ALL","-1"));
            ddlExpenseType.SelectedValue ="-1";

        }
        #endregion 

         /// <summary>
        /// Render report according to the report name and parameters
        /// </summary>
        /// <param name="prams"></param>
        /// <param name="reportName"></param>
        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            ReportParameter[] parameters = new ReportParameter[9];
            parameters[0] = new ReportParameter("startdate", txtStartDate.Text.Trim());
            parameters[1] = new ReportParameter("enddate", txtEndDate.Text.Trim());
            parameters[2] = new ReportParameter("entitytypeid", ddlEntityType.SelectedValue);
            parameters[3] = new ReportParameter("expensetype", ddlExpenseType.SelectedValue);
            parameters[4] = new ReportParameter("paidstatus", ddlChkPaidStatus.SelectedValue);
            parameters[5] = new ReportParameter("UserID", Constant.UserId.ToString());
            parameters[6] = new ReportParameter("systemId", Session["SystemID"].ToString());
            parameters[7] = new ReportParameter("entityid", ddlEntityName.SelectedValue);
            parameters[8] = new ReportParameter("OrgID", Constant.OrgID.ToString());

            ReportParameters.Parameters = parameters;
            ReportParameters.ReportName = "/Hollenshead/ExpenseReport";

            Response.Redirect("Reports.aspx?ReturnURL=ExpenseReport.aspx");

        }

        #region[EntityType SelectedIndexChanged event]
        protected void ddlEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ddlEntityType.SelectedValue))
                BindEntitiy();
        }
        #endregion

        #region[Bind entity dropdown]
        protected void BindEntitiy()
        {
            NotificationBAL objBal = new NotificationBAL();
            ddlEntityName.DataSource = objBal.GetEntitiesByEntityType(Convert.ToInt32(ddlEntityType.SelectedValue), Constant.OrgID);
            ddlEntityName.DataTextField = "DisplayName";
            ddlEntityName.DataValueField = "EntityID";
            ddlEntityName.DataBind();
            ddlEntityName.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        #endregion
    }
}

