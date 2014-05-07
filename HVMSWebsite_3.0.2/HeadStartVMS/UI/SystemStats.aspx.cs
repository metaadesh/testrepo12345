using System;
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
using Microsoft.Reporting.WebForms;
using METAOPTION.Reports;
using METAOPTION.BAL;

namespace METAOPTION.UI
{
   public partial class SystemStats : System.Web.UI.Page
   {

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
         LoadData();
      }
      public void LoadData()
      {
          spSystemStatsResult stats = BAL.Common.SystemStats(Convert.ToInt32(Session["SystemID"]),Constant.OrgID);
         
          //CHANGE REQUEST:728
         //Unpaid Inventory
         if (stats.Unpaid_Total != null)
         {
             lblUnpaidTotal1.Text = "Total Receivable";
             lblUnpaidTotal11.Text = String.Format("{0:C}", stats.Unpaid_Total); 
         }

         //Unpaid Inventory Count
         if (stats.Unpaid_Count !=null && stats.Unpaid_Count > 1 )
         {
             lblUnpaidCount1.Text = "Number of Receivable Inventory";
             lblUnpaidCount11.Text = stats.Unpaid_Count.ToString();
         }
         else
         {
             lblUnpaidCount1.Text = "Number of Inventory Paid ";
             lblUnpaidCount11.Text = stats.Unpaid_Count.ToString();
         }
          
         //this.lblVehicleCountRegularLane2212.Text = Convert.ToString(stats.Total_Vehicles_In_Reg_Lane);
         ////Bug Request 749:Add Exotic Count along with regular lane count
         //this.lblVehicleCountRegularLane2212.Text += ", " + Convert.ToString(stats.Total_Vehicles_In_Exotic_Lane);


         //this.lblVehicleCountRegularLane2212.Text += ", " + Convert.ToString(stats.Total_Vehicles_In_Vir_Lane);
         //this.lblVehicleCountRegularLane2212.Text += ", " + Convert.ToString(stats.Total_Vehicles_In_Online_Lane);
         dListRegular.DataSource = Common.LaneStatic('R',Constant.OrgID);
         dListRegular.DataBind();

         dListExotic.DataSource = Common.LaneStatic('E', Constant.OrgID);
         dListExotic.DataBind();

         this.lblTotalInventoryDollars.Text = String.Format("{0:C}", stats.Total_Inventory_Dollars);
         this.lblTotalVehiclesintheSystem.Text = Convert.ToString(stats.Number_Vehicles);
         this.lblAvgPerVehicle.Text = String.Format("{0:C}", stats.Average_per_Vehicle);
         // Inventory Vehicles
         this.lblNumberInventoryVehicles.Text = Convert.ToString(stats.Number_Inventory_Vehicles);
         this.lblAveragePerInventoryVehicle.Text = String.Format("{0:C}", stats.Average_Inventory_Vehicles);
         // On-Hand Inventory Vehicles  
         this.lblNumber_OnHand_Vehicles.Text = Convert.ToString(stats.Number_OnHand_Vehicles);
         this.lblAverage_OnHand_Vehicles.Text = String.Format("{0:C}", stats.Average_OnHand_Vehicles);
         // On-Hand Inventory Vehicles  
         this.lblNumber_Archived_Vehicles.Text = Convert.ToString(stats.Number_Archived_Vehicles);
         this.lblAverage_Archived_Vehicles.Text = String.Format("{0:C}", stats.Average_Archived_Vehicles);
         // Vehicles WTD
         this.lblVehicle_Added_WTD.Text = Convert.ToString(stats.Vehicle_Added_WTD);
         // Vehicles MTD
         this.lblVehicles_Added_MTD.Text = Convert.ToString(stats.Vehicles_Added_MTD);
         // Vehicles YTD
         this.lblVehicles_Added_YTD.Text = Convert.ToString(stats.Vehicles_Added_YTD);
         // Not Paid Yet -> Cars / Amount 
         this.lblCarsNotPaidYet.Text = Convert.ToString(stats.CarsNotPaidYet);
         this.lblAmountNotPaidYet.Text = String.Format("{0:C}", stats.AmountNotPaidYet);
         // Open Expenses 
         this.lblNumber_OpenExpenses.Text = Convert.ToString(stats.Number_OpenExpenses);
         this.lblAmout_OpenExpenses.Text = String.Format("{0:C}", stats.Amount_OpenExpenses);
         // Not Paid Yet -> Cars / Amount 
         this.lblNumber_OpenCommissions.Text = Convert.ToString(stats.Number_OpenCommissions);
         this.lblAmount_OpenCommissions.Text = String.Format("{0:C}", stats.Amount_OpenCommissions);
        

         // Check permission and do the the Show/Hide accordingly
         List<String> rights = BAL.CommonBAL.GetPagePermission(Constant.UserId, "SYSTEM");
         if (rights.Count < 1)
            Response.Redirect("Permission.aspx?MSG=SYSTEM.STATS.XXXXXX");

         if (!rights.Contains("STATS.TOTALINVENTORYDOLLERS"))
            this.trInventoryDollers.Visible = false;

         if (!rights.Contains("STATS.NUMBEROFVEHICLEINSYSTEM"))
            this.trTotalVehiclesintheSystem.Visible = false;

         if (!rights.Contains("STATS.AVG$PERVEHICLE"))
            this.trAvgPerVehicle.Visible = false;

         // Number of Inventory Vehicles in the System
         if (!rights.Contains("STATS.NUMBER_INVENTORY_VEHICLES"))
            this.trNumberInventoryVehicles.Visible = false;

         // Average $ per Inventory Vehicle on system stats page
         if (!rights.Contains("STATS.AVG_$PER_INVENTORY_VEHICLE"))
            this.trAveragePerInventoryVehicle.Visible = false;

         // Number of On-Hand Vehicles in the System
         if (!rights.Contains("STATS.NUMBER_ONHAND_VEHICLE"))
            this.trNumber_OnHand_Vehicles.Visible = false;

         // Average $ per On-Hand Vehicle 
         if (!rights.Contains("STATS.AVG_$PER_ONHAND_VEHICLE"))
            this.trAverage_OnHand_Vehicles.Visible = false;


         // Number of Archived Vehicles in the System 
         if (!rights.Contains("STATS.NUMBER_ARCHIVED_VEHICLE"))
            this.trNumber_Archived_Vehicles.Visible = false;

         // Average $ per Archived Vehicle 
         if (!rights.Contains("STATS.AVG_$PER_ARCHIVED_VEHICLE"))
            this.trAverage_Archived_Vehicles.Visible = false;

         // Vehicles Added this week  
         if (!rights.Contains("STATS.VEHICLE_ADDED_THISWEEK"))
            this.trVehicle_Added_WTD.Visible = false;

         // Vehicles Added MTD (Month to date) 
         if (!rights.Contains("STATS.VEHICLE_ADDED_MTD"))
            this.trVehicles_Added_MTD.Visible = false;

         // Vehicles Added YTD (Year to Date) ) 
         if (!rights.Contains("STATS.VEHICLE_ADDED_YTD"))
            this.trVehicles_Added_YTD.Visible = false;

         // Number of Cars not paid yet 
         if (!rights.Contains("STATS.NUMBER_CARS_NOTPAIDYET"))
            this.trCarsNotPaidYet.Visible = false;

         // Amount of Cars not paid yet
         if (!rights.Contains("STATS.AMOUNT_CARS_NOTPAIDYET"))
            this.trAmountNotPaidYet.Visible = false;

         // Number of Open Expenses 
         if (!rights.Contains("STATS.NUMBER_OPEN_EXPENSES"))
            this.trNumber_OpenExpenses.Visible = false;

         // Amount of Open Expenses 
         if (!rights.Contains("STATS.AMOUNT_OPEN_EXPENSES"))
            this.trAmount_OpenExpenses.Visible = false;

         // Number of Open Commissions 
         if (!rights.Contains("STATS.NUMBER_OPEN_COMMISSIONS"))
            this.trNumber_OpenCommissions.Visible = false;

         // Amount of open commissions  
         if (!rights.Contains("STATS.AMOUNT_OPEN_COMMISSIONS"))
            this.trAmount_OpenCommissions.Visible = false;

      }
       /// <summary>
       /// Call Unpaid report(.rdl)0
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
      protected void lnkUnpaidReport_Click(object sender, EventArgs e)
      {
         // string StartDate = DateTime.Today.AddDays(-7).ToShortDateString();
          string EndDate = DateTime.Today.ToShortDateString();

          ReportParameter[] parameters = new ReportParameter[9];
          parameters[0] = new ReportParameter("startdate", "01/01/1900");
          parameters[1] = new ReportParameter("enddate", EndDate);
          parameters[2] = new ReportParameter("year", "-1");
          parameters[3] = new ReportParameter("make", "-1");
          parameters[4] = new ReportParameter("model", "-1");
          parameters[5] = new ReportParameter("UserId", Constant.UserId.ToString());
          parameters[6] = new ReportParameter("carstatus", "-1");
          parameters[7] = new ReportParameter("buyer", "-1");
          parameters[8] = new ReportParameter("systemId", Session["SystemID"].ToString());
                    
          ReportParameters.Parameters = parameters;

          ReportParameters.ReportName = "/Hollenshead/NoPaidReport";
          Response.Redirect("~/VMSReports/Reports.aspx?ReturnURL=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));

      }
   }
}
