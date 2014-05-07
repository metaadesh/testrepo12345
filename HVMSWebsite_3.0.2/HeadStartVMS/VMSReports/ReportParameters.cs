using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.Reporting;
using Microsoft.Reporting.WebForms;
using System.Xml.Linq;

namespace METAOPTION.Reports
{
   public static class ReportParameters
   {
      private static ReportParameter[] _parameters= null;
      public static ReportParameter[] Parameters
      {
         get { return _parameters; }
         set { _parameters = value; }
      }
      private static String _ReportName = String.Empty;
      public static String ReportName
      {
         get { return _ReportName; }
         set { _ReportName = value; }
      }
   }
}
