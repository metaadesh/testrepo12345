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
using System.Text;

namespace METAOPTION.UI
{
    public partial class DealerListGoogleMap : System.Web.UI.Page
    {
        //Common objCommon = new Common();
        //MasterBAL objMasterBAL = new MasterBAL();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["MasterPageFullScreen"])))
                    this.MasterPageFile = Convert.ToString(Session["MasterPageFullScreen"]);
                else
                    Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(Request.Url.ToString()));
            }
            catch (Exception ex)
            {
                Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(Request.Url.ToString()));
            }

        }

        protected String GoogleMapKey;
        protected String DealerList;

        protected void Page_Load(object sender, EventArgs e)
        {
            GoogleMapKey = System.Configuration.ConfigurationManager.AppSettings["GoogleMapKey"].ToString();

            DataTable dtDealer = new DataTable();
            dtDealer = GetDealersDataTable();

            StringBuilder sb = new StringBuilder();
            String comma = ",";

            sb.Append("new Array(");
            //sb.Append("new Array('Arun gupta', 'New Delhi India', 11, 14, ' 03 - 12 - 2013')" + comma);
            //sb.Append("new Array('Tarun talyan', 'Mumbai India', 19, 14, '23 - 02 - 2014')" + comma);
            //sb.Append("new Array('Abhinav Mishra', 'Noida India', 26, 2, '06 - 04 - 2013')");

            Int32 count = 0;
            Int32 totalRows = dtDealer.Rows.Count;
            String DealerAddress = "";
            foreach (DataRow row in dtDealer.Rows)
            {
                count++;

                DealerAddress = row["Street"].ToString().Replace("'", "") + " " + row["City"].ToString().Replace("'", "") + " " + row["StateFullName"].ToString().Replace("'", "") + " " + row["CountryFullName"].ToString().Replace("'", "");

                sb.Append("new Array('" + row["DealerName"].ToString() + "', '" + DealerAddress + "', " + row["TotalSold"].ToString() + ", " + row["TotalPurchased"].ToString() + ", ' 03 - 12 - 2013')");

                if (count != totalRows)
                {
                    sb.Append(comma);
                }

            }


            sb.Append(");");

            DealerList = sb.ToString();

            // DealerList = new Array(
            // new Array('Manly Seman', 'NewYork USA', 90, 44, '08 - 09 - 2013'),
            // new Array('Maroubra Drien', 'Ottawa Canada', 24, 32, '25 - 01 - 2014'),
            // new Array('Alix Rz', 'Stockholm Sweden', 46, 32, '12 - 05 - 2010'),
            // new Array('Marian Seman', 'Atlanta Georgia USA', 90, 44, '01 - 09 - 2013'),
            // new Array('Stone Gold', 'Les Vegaus Nevada USA', 74, 32, '15 - 01 - 2014'),
            // new Array('Justin Black', 'Birminghum United Kingdom', 103, 32, '19 - 01 - 2010')
            //);

        }

        private DataTable GetDealersDataTable()
        {
            METAOPTION.BAL.DealerCustomerBAL bal = new METAOPTION.BAL.DealerCustomerBAL();
            DataTable dtDealer = new DataTable();
            dtDealer = bal.DealerSearchList(GetValue("dealerName", ""), GetValue("categoryId", 1), GetValue("typeId", 1), GetValue("city", ""), GetValue("countryId", 1), GetValue("stateId", 1),
                                            GetValue("zip", ""), GetValue("StartRowIndex", 1), GetValue("MaximumRows", 1), GetValue("SortExpression", ""), GetValue("Status", 1),
                                            GetValue("OrgID", 1), GetValue("DaysLastTransaction", 1), GetValue("DateSince", ""), GetValue("WelcomeTasks", ""));

            return dtDealer;
            //ObjectDataSource odsDealerSelector = new ObjectDataSource();
            //odsDealerSelector.Selected += new ObjectDataSourceStatusEventHandler(odsDealerSelector_Selected);
            //odsDealerSelector.TypeName = "METAOPTION.BAL.DealerCustomerBAL";
            //odsDealerSelector.SelectMethod = "DealerSearchList";
            //odsDealerSelector.SelectCountMethod = "DealerSearchListCount";
            //odsDealerSelector.EnablePaging = true;
            //odsDealerSelector.SelectParameters.Add("dealerName", txtName.Text.Trim());
            //odsDealerSelector.SelectParameters.Add("categoryId", ddlCategory.SelectedValue);
            //odsDealerSelector.SelectParameters.Add("typeId", ddlType.SelectedValue);
            //odsDealerSelector.SelectParameters.Add("city", txtCity.Text.Trim());
            //odsDealerSelector.SelectParameters.Add("countryId", ddlCountry.SelectedValue);
            //odsDealerSelector.SelectParameters.Add("stateId", ddlState.SelectedValue);
            //odsDealerSelector.SelectParameters.Add("zip", txtZip.Text.Trim());
            //odsDealerSelector.SelectParameters.Add("StartRowIndex", DbType.Int32, gvViewDealer.PageIndex.ToString());
            //odsDealerSelector.SelectParameters.Add("MaximumRows", DbType.Int32, ddlPageSize1.SelectedValue);
            //odsDealerSelector.SelectParameters.Add("SortExpression", sort);
            //odsDealerSelector.SelectParameters.Add("Status", DbType.Int32, ddlStatus.SelectedValue);
            //odsDealerSelector.SelectParameters.Add("OrgID", DbType.Int32, Constant.OrgID.ToString());

            //odsDealerSelector.SelectParameters.Add("DaysLastTransaction", DbType.Int32, DaysLastTransaction.ToString());
            //odsDealerSelector.SelectParameters.Add("DateSince", DbType.String, DateSince);

            //odsDealerSelector.SelectParameters.Add("WelcomeTasks", DbType.String, strWelcomeTasks);
            //gvViewDealer.DataSource = odsDealerSelector;
            //gvViewDealer.DataBind();

        }

        private String GetValue(String QueryString, String str)
        {
            String value = "";
            if (Request.QueryString[QueryString] != null)
            {
                value = Request.QueryString[QueryString].ToString();
            }
            return value;
        }

        private Int32 GetValue(String QueryString, Int32 inte)
        {
            Int32 value = -1;
            if (Request.QueryString[QueryString] != null)
            {
                if (Int32.TryParse(Request.QueryString[QueryString].ToString(), out value) == false)
                {
                    value = -1;
                }

            }
            return value;
        }
    }
}
