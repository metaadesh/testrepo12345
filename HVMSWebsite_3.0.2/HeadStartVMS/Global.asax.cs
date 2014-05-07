using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;
using System.Text;
using METAOPTION.BAL;
using System.IO.Compression;
using System.IO;
using System.Web.UI;


namespace METAOPTION
{
    public class Global : System.Web.HttpApplication
    {
        #region [Exception Handling Block]
        /// <summary>
        /// This method Log any unhandled Error in the Application
        /// </summary>
        private void LogError()
        {
            // Note:- All Unhandled Exceptions will be catched here and Common Error Page Shown To User
            // There is no need to put try..catch block anywhere, if try..catch block putted any where
            // write throw ex; statement to throw the exception.

            Exception ex = Server.GetLastError();

            if (ex != null)
            {
                if (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                //Create Format to Store Error
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendLine("EXCEPTION - BEGIN<br/><br/>");
                sb.AppendFormat("URL: {0}<br/>", Request.Url);
                sb.AppendFormat("Time: {0}<br/><br/>", DateTime.Now);
                sb.AppendLine("EXCEPTION DETAILS:<br/><br/>");
                sb.Append(ex);
                sb.Append("<br/><br/>EXCEPTION - END");
                sb.Replace("\n", "<br/>");

                string strError = sb.ToString();
           
                //Log Unhandled Exception In Db
                METAOPTION.BAL.Common.LogError(strError);
            }
        }

        #endregion

        #region [Handle Application Error Events]

        /// <summary>
        /// Handle Application Error Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {

                LogError(Server.GetLastError(), HttpContext.Current.Request.Path); 
                LogError();
            }
            catch {}
        }

        

        #endregion

        protected void Session_Start(object sender, EventArgs e)
        {
          // Constant.LiveUsers += 1;
        }
        protected void Session_End(object sender, EventArgs e)
        {
            if (Session["empId"] != null)
            { 
                Constant.LiveUsers -= 1; 
            }
            if (Session["LoginHistoryID"]!=null)
            {

                METAOPTION.DALDataContext.Factory.DB.Logout(Convert.ToInt64(Session["LoginHistoryID"]));
                //(new LoginBLL()).UpdateLogOutTime(Convert.ToInt64(Session["LoginHistoryID"]));
            }
        }

        private void LogError(Exception ex, String source)
        {
            try
            {
                String LogFile = HttpContext.Current.Request.MapPath("~/Errorlog.txt");
                if (LogFile != "")
                {
                    String Message = String.Format("{0}{0}============={1}============={0}{2}{0}{3}{0}{4}"
                        , Environment.NewLine
                        , DateTime.Now
                        , ex.Message
                        , source
                        , ex.InnerException);
                    byte[] binLogString = Encoding.Default.GetBytes(Message);


                    System.IO.FileStream loFile = new System.IO.FileStream(LogFile, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write, System.IO.FileShare.Write);
                    loFile.Seek(0, System.IO.SeekOrigin.End);
                    loFile.Write(binLogString, 0, binLogString.Length);
                    loFile.Close();
                }
            }
            catch { ; }
        }

        public Global()
        {
            //InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.PostReleaseRequestState +=
                new EventHandler(Global_PostReleaseRequestState);
        }

        private void Global_PostReleaseRequestState(object sender, EventArgs e)
        {
            string contentType = Response.ContentType;

            if (contentType == "text/html" ||
                contentType == "text/css")
            {
                Response.Cache.VaryByHeaders["Accept-Encoding"] = true;

                string acceptEncoding =
                    Request.Headers["Accept-Encoding"];

                if (acceptEncoding != null)
                {
                    if (acceptEncoding.Contains("gzip"))
                    {
                        Response.Filter = new GZipStream(
                            Response.Filter, CompressionMode.Compress);
                        Response.AppendHeader(
                            "Content-Encoding", "gzip");
                    }
                    else if (acceptEncoding.Contains("deflate"))
                    {
                        Response.Filter = new DeflateStream(
                            Response.Filter, CompressionMode.Compress);
                        Response.AppendHeader(
                            "Content-Encoding", "deflate");
                    }
                }
            }
        } 
       
    }
}