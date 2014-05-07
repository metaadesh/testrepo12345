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
using METAOPTION.ReportingService;

namespace METAOPTION.UI
{
    public partial class BillOfSale : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Code"] != null)
            {
                // Byte[] result;

                // string encoding;
                // string mimetype;
                // ParameterValue[] parametersUsed = new ParameterValue[2];
                // parametersUsed[0] = new ParameterValue();
                // parametersUsed[0].Name = "InventoryId";
                // parametersUsed[0].Value = Convert.ToString(Request["Code"]);
                // parametersUsed[1] = new ParameterValue();
                // parametersUsed[1].Name = "UserId";
                // parametersUsed[1].Value = Constant.UserId.ToString();
                // METAOPTION.ReportingService.Warning[] warnings;
                // string[] streamids;

                // METAOPTION.ReportingService.ReportingService rs = new METAOPTION.ReportingService.ReportingService();
                //// rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
                // rs.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;



                // result = rs.Render("/Hollenshead/BillOFSaleReport", "PDF", null, null, parametersUsed, null, null, out encoding, out mimetype, out parametersUsed, out warnings, out streamids);

                // Response.ClearContent();
                // Response.AppendHeader("content-length", result.Length.ToString());
                // Response.ContentType = "application/pdf";
                // Response.BinaryWrite(result);
                // //Response.Flush();
                // //Response.Close();

                Microsoft.Reporting.WebForms.ReportViewer rview = new Microsoft.Reporting.WebForms.ReportViewer();

                rview.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"]);

                System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter> paramList = new System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter>();
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("InventoryId", Convert.ToString(Request["Code"])));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("UserId", Constant.UserId.ToString()));

                string rsUserName = System.Configuration.ConfigurationManager.AppSettings["rsUserName"].ToString();
                string rsPassword = System.Configuration.ConfigurationManager.AppSettings["rsPassword"].ToString();
                string rsDomain = System.Configuration.ConfigurationManager.AppSettings["rsDomain"].ToString();

                rview.ServerReport.ReportServerCredentials = new METAOPTION.Reports.CustomReportCredentials(rsUserName, rsPassword, rsDomain);

                rview.ServerReport.ReportPath = "/Hollenshead/BillOFSaleReport";
                rview.ServerReport.SetParameters(paramList);


                string mimeType, encoding, extension, deviceInfo;
                string[] streamids;
                Microsoft.Reporting.WebForms.Warning[] warnings;
                string format = "PDF"; //Desired format goes here (PDF, Excel, or Image)
                deviceInfo = "<DeviceInfo>" + "<SimplePageHeaders>True</SimplePageHeaders>" + "</DeviceInfo>";

                //deviceInfo =
                //            "<DeviceInfo>" +
                //            "  <OutputFormat>PDF</OutputFormat>" +
                //            "  <PageWidth>8.5in</PageWidth>" +
                //            "  <PageHeight>11in</PageHeight>" +
                //            "  <MarginTop>0.5in</MarginTop>" +
                //            "  <MarginLeft>1in</MarginLeft>" +
                //            "  <MarginRight>1in</MarginRight>" +
                //            "  <MarginBottom>0.5in</MarginBottom>" +
                //            "</DeviceInfo>";

                Byte[] result = rview.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
                Response.ClearContent();
                Response.AppendHeader("content-length", result.Length.ToString());
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(result);
                Response.End();

                //Response.Clear();

                //Response.ContentType = mimeType;
                //Response.AddHeader("content-disposition", String.Format("attachment; filename=HeadStartVMS_CheckPrint_{0}.{1}", PaymentID.ToString(), extension));
                //Response.BinaryWrite(bytes);
                //Response.End(); 
            }
        }
    }
}
