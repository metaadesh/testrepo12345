using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using METAOPTION.BAL;
using AjaxControlToolkit;

namespace METAOPTION.WS
{
   /// <summary>
   /// Summary description for AutoFill
   /// </summary>
   [WebService(Namespace = "http://tempuri.org/")]
   [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
   [ToolboxItem(false)]
   [System.Web.Script.Services.ScriptService] 
   public class AutoFill : System.Web.Services.WebService
   {
      //[System.Web.Services.WebMethod]
      //[System.Web.Script.Services.ScriptMethod]
      //public string[] GetRightList(string prefixText, int count)
      //{
      //   //Set default count = 20
      //   if (count == 0) count = 20;

      //   //return result list as an array
      //   return AddGroupBAL.GetRightsName(prefixText, count);
      //}

       
      
   }
}
