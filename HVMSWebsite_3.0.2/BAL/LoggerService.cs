using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Configuration;

namespace METAOPTION
{
    public class LoggerService
    {

        public static void LogFile(string Message)
        {
            try
            {
                String path = HttpContext.Current.Server.MapPath("~");
                String FormatMsg = String.Format(@"{0}======================={1}==============={0}Message: {2}"
                    , Environment.NewLine
                    , DateTime.Now
                    , Message);
                byte[] binLogString = Encoding.Default.GetBytes(FormatMsg);

                String LogFile = string.Format("{0}/Log.txt", path);
                System.IO.FileStream loFile = new System.IO.FileStream(LogFile, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write, System.IO.FileShare.Write);
                loFile.Seek(0, System.IO.SeekOrigin.End);
                loFile.Write(binLogString, 0, binLogString.Length);
                loFile.Close();
            }
            catch (Exception ex)
            {
                //LoggerService.LogEventsToFile(ex, "", this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        public static void LogServiceError(Exception Ex, string URL, string ClassFileName, string MethodName)
        {

            try
            {
                //String path = Application.StartupPath;
                //String path = HttpContext.Current.Server.MapPath("~");
                String path = HttpContext.Current.Server.MapPath("~");

                //String LogFile = string.Format("{0}/ServiceLog_" + System.DateTime.Now.Day.ToString() + "_" + System.DateTime.Now.Month.ToString() + "_" + System.DateTime.Now.Year.ToString() + ".txt", path);
                String LogFile = string.Format("{0}/Errorlog.txt", path);
                if (LogFile != "")
                {
                    String Message = String.Format(
                        @"{0}====================== {1} ====================={0}{3}{0}Message: {2}{0}{4}{0}ClassFileName: {5}{0}MethodName: {6}{0}"
                        , Environment.NewLine
                        , DateTime.Now
                        , Ex.InnerException
                        , URL
                        , Ex.Message
                        , ClassFileName
                        , MethodName);
                    byte[] binLogString = Encoding.Default.GetBytes(Message);


                    System.IO.FileStream loFile = new System.IO.FileStream(LogFile, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write, System.IO.FileShare.Write);
                    loFile.Seek(0, System.IO.SeekOrigin.End);
                    loFile.Write(binLogString, 0, binLogString.Length);
                    loFile.Close();
                }

            }
            catch { ; }

        }
    }
}
