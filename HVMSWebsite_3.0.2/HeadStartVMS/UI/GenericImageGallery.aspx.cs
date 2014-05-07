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
using System.IO;
using System.Net;

namespace METAOPTION
{
    public partial class GenericImageGallery : System.Web.UI.Page
    {
        CommonBAL bal = new CommonBAL();
        System.Collections.ArrayList arraylist = new ArrayList();
        String UserName, DeviceID, DeviceName, VIN, SyncDate,Lat, Long;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.HasKeys())
                {
                    long ImageID = Convert.ToInt64(Request.QueryString["i"]);
                    String VIN = Request.QueryString["v"];
                    Int32 Type = Convert.ToInt32(Request.QueryString["t"]);
                    Int32 Period = Convert.ToInt32(Request.QueryString["p"]);
                    hfType.Value = Convert.ToString(Type);
                    arraylist = bal.AllImages(ImageID, VIN, Type, Period);

                    
                    dlthumb.DataSource = arraylist[0];
                    dlthumb.DataBind();
                }
            }
        }

        public String GetImagePath(object Path)
        {
            String ServerUrl = String.Empty;
            string path;
            if (hfType.Value == "1")
                ServerUrl = System.Configuration.ConfigurationManager.AppSettings["MobileImagePath"];
            else if (hfType.Value == "2")
                ServerUrl = System.Configuration.ConfigurationManager.AppSettings["ExpenseImagePath"];
            else if (hfType.Value == "3" || hfType.Value == "4")
                ServerUrl = System.Configuration.ConfigurationManager.AppSettings["GenericImagePath"];
            path= String.Format("{0}{1}", ServerUrl, Path);

            //bool exist = false;
            //try
            //{
            //    HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(path);
            //    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            //    {
            //        exist = response.StatusCode == HttpStatusCode.OK;
            //    }
            //}
            //catch
            //{
            //    exist = false;
            //}
            //if (exist)
            //    return path;
            //else
            //    return "../Images/nocar.png";
            return path;

        }

        public String GetMainImagePath(object Path)
        {
            String ServerUrl = String.Empty;
            string path;
            if (hfType.Value == "1")
                ServerUrl = System.Configuration.ConfigurationManager.AppSettings["MobileImagePath"];
            else if (hfType.Value == "2")
                ServerUrl = System.Configuration.ConfigurationManager.AppSettings["ExpenseImagePath"];
            else if (hfType.Value == "3" || hfType.Value == "4")
                ServerUrl = System.Configuration.ConfigurationManager.AppSettings["GenericImagePath"];
            path = String.Format("{0}{1}", ServerUrl, Path);

            bool exist = false;
            try
            {
                HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(path);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    exist = response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch
            {
                exist = false;
            }
            if (exist)
                return path;
            else
                return "../Images/nocar.png";
           

        }

        public String GetDesc(object ImageID)
        {
            DataTable dtImg = new DataTable();
            dtImg = (DataTable)arraylist[0];

          
            DataTable dtInv = new DataTable();
            dtInv = (DataTable)arraylist[1];

            DataRow[] Imgrow = dtImg.Select(String.Format("ImageID={0}", ImageID));
            SyncDate = Convert.ToString(Imgrow[0]["SyncDate"]);
            if (Imgrow != null && Imgrow.Count() > 0)
            {
                // DataRow[] Invrow = dtInv.Select(String.Format("ImageId = {0}", ImageID));
                VIN = Convert.ToString(Imgrow[0]["VIN"]);
                UserName = Convert.ToString(Imgrow[0]["DisplayName"]);
                DeviceID = Convert.ToString(Imgrow[0]["DeviceID"]);
                DeviceName = Convert.ToString(Imgrow[0]["DeviceName"]);
                spHeader0.InnerText = VIN;
                Lat = Convert.ToString(Imgrow[0]["Latitude"]);
                Long = Convert.ToString(Imgrow[0]["Longitude"]);
               
                return String.Format("Device: {0} | DeviceID: {1}<br/>Synced By: {2} | Synced Date: {3} | Latitude: {4} | Longitude: {5}", DeviceName, DeviceID, UserName, SyncDate, Lat, Long);
            }
            else
                return "Missing";
            
            
        }
    }
}