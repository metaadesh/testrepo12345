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
    public partial class ImageGallery : System.Web.UI.Page
    {
        String ServerUrl = System.Configuration.ConfigurationManager.AppSettings["MobileImagePath"];

        PreInventoryBAL PreInvBAL = new PreInventoryBAL();
        System.Collections.ArrayList arraylist = new ArrayList();
        String Year, Make, Model, Body, Price, Mileage, UserName, DeviceID, DeviceName, VIN, SyncDate, Lat, Long;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.HasKeys())
                {
                       arraylist = PreInvBAL.PreInventoryImages(Convert.ToInt64(Request.QueryString["id"]));

                        dlthumb.DataSource = arraylist[0];
                        dlthumb.DataBind();
                }
            }
        }

        public String GetImagePath(object Path)
        {
            return String.Format("{0}{1}",ServerUrl,Path);
        }

        public String GetDesc(object ImageID)
        {
            DataTable dtImg = new DataTable();
            dtImg = (DataTable)arraylist[0];

            DataTable dtInv = new DataTable();
            dtInv = (DataTable)arraylist[1];

            DataRow[] Imgrow = dtImg.Select(String.Format("ImageID={0}",ImageID));
            SyncDate = Convert.ToString(Imgrow[0]["DateAdded"]);
            Lat = Convert.ToString(Imgrow[0]["Latitude"]);
            Long = Convert.ToString(Imgrow[0]["Longitude"]);

            DataRow[] Invrow = dtInv.Select();
            DeviceName = Convert.ToString(Invrow[0]["DeviceName"]);
            DeviceID = Convert.ToString(Invrow[0]["DeviceID"]);
            UserName = Convert.ToString(Invrow[0]["UserName"]);
            Year = Convert.ToString(Invrow[0]["Year"]);
            Make = Convert.ToString(Invrow[0]["Make"]);
            Model = Convert.ToString(Invrow[0]["Model"]);
            Body = Convert.ToString(Invrow[0]["Body"]);
            Price = String.Format("${0:#,###}", Convert.ToDecimal(Invrow[0]["Price"]));
            Mileage = String.Format("{0:#,###}", Convert.ToDecimal(Invrow[0]["MileageIn"]));
            VIN = Convert.ToString(Invrow[0]["VIN"]);
            spHeader0.InnerText = VIN;
            spHeader1.InnerText = String.Format("{0} {1} {2}",Year, Make, Model);
            spHeader2.InnerText = String.Format("{0}  {1}  {2}",Body,Price,Mileage);
            return String.Format("Device: {0} | DeviceID: {1}<br/>Synced By: {2} | Synced Date: {3} | Latitude: {4} | Longitude: {5}", DeviceName, DeviceID, UserName, SyncDate, Lat, Long);
        }
    }
}