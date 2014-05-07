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
using System.Collections.Generic;
using System.Text;
using System.Web.Services;
using METAOPTION.UserControls;

namespace METAOPTION.UI
{
    public partial class ExpenseImageGallery : System.Web.UI.Page
    {
        String ServerUrl = System.Configuration.ConfigurationManager.AppSettings["ExpenseImagePath"];

        PreExpenseBAL PreExpBAL = new PreExpenseBAL();
        System.Collections.ArrayList arraylist = new ArrayList();
        String UserName, DeviceID, DeviceName, VIN, SyncDate, Count, DefaultPrice, TotalPrice, ExpenseType, Lat, Long;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.HasKeys())
                {
                    arraylist = PreExpBAL.PreExpenseImages(Convert.ToString(Request.QueryString["id"]));
                    dlthumb.DataSource = arraylist[0];
                    dlthumb.DataBind();
                }
            }
        }

        public String GetImagePath(object Path)
        {
            return String.Format("{0}{1}", ServerUrl, Path);
        }

        public String GetDesc(object ImageID)
        {
            DataTable dtImg = new DataTable();
            dtImg = (DataTable)arraylist[0];

            DataTable dtInv = new DataTable();
            dtInv = (DataTable)arraylist[1];

            DataRow[] Imgrow = dtImg.Select(String.Format("ImageID={0}", ImageID));
            SyncDate = Convert.ToString(Imgrow[0]["DateAdded"]);
            Lat = Convert.ToString(Imgrow[0]["Latitude"]);
            Long = Convert.ToString(Imgrow[0]["Longitude"]);

            DataRow[] Invrow = dtInv.Select();
            DeviceName = Convert.ToString(Invrow[0]["DeviceName"]);
            DeviceID = Convert.ToString(Invrow[0]["DeviceID"]);
            UserName = Convert.ToString(Invrow[0]["UserName"]);
            Count = Convert.ToString(Invrow[0]["Count"]);
            ExpenseType = Convert.ToString(Invrow[0]["ExpenseType"]);
            DefaultPrice = String.Format("${0:#,###}", Convert.ToDecimal(Invrow[0]["DefaultPrice"]));
            TotalPrice = String.Format("${0:#,###}", Convert.ToDecimal(Invrow[0]["TotalPrice"]));
            VIN = Convert.ToString(Invrow[0]["VIN"]);
            spHeader0.InnerText = VIN + " ";
            spHeader1.InnerText = String.Format("Expense Type: {0} ", ExpenseType);
            spHeader2.InnerText = String.Format("Default Price: {0}  Total Price: {1}  Count: {2} ", DefaultPrice, TotalPrice, Count);
            return String.Format("Device: {0} | DeviceID: {1}<br/>Synced By: {2} | Synced Date: {3} | Latitude: {4} | Longitude: {5}", DeviceName, DeviceID, UserName, SyncDate, Lat, Long);
        }
    }
}