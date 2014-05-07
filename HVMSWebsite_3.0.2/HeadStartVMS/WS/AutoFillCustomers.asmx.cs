using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Services;
using METAOPTION;
using AjaxControlToolkit;
namespace METAOPTION.WS
{
    public class CheckInfo
    {
        public string CheckNo { get; set; }
        public String CheckDate { get; set; }
        public String Amount { get; set; }
        public String DateAdded { get; set; }
        public String AddedBy { get; set; }
        public String RecipientName { get; set; }
        public String RecipientType { get; set; }
    }

    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    //[System.Web.Script.Services.ScriptService]
    public class AutoFillNames : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod]
        public string[] AutoFillCustomers(string prefixText, int count, string contextKey)
        {
            //contextKey = OrgID : Applied by Ashar on 10/24/2013
            METAOPTION.DALDataContext objDataContext = new METAOPTION.DALDataContext();

            List<string> dealernames = new List<string>();
            GetCustomerNamesResult[] res = objDataContext.GetCustomerNames(prefixText, count, Convert.ToInt16(contextKey)).ToArray();
            foreach (GetCustomerNamesResult item in res)
            {
                dealernames.Add(item.DealerName);
            }
            return dealernames.ToArray();
        }

        //////////////////////////////////////////Added by Rupendra 16 Aug 2012 for return paid customer Details/////////
        [WebMethod]
        [ScriptMethod]
        public string[] AutoFillCustomersDetails(string prefixText, int count)
        {
            METAOPTION.DALDataContext objDataContext = new METAOPTION.DALDataContext();

            List<string> dealers = new List<string>();
            GetCustomerNamesDetailsResult[] res = objDataContext.GetCustomerNamesDetails(prefixText, count).ToArray();
            foreach (GetCustomerNamesDetailsResult item in res)
            {
                dealers.Add(item.DealerName);
            }
            return dealers.ToArray();
        }


        //////////////////////////////////////////End////////////////////////////////////////////

        [WebMethod]
        [ScriptMethod]
        public String UserDetail(Int32 UserID)
        {
            METAOPTION.DALDataContext objDataContext = new METAOPTION.DALDataContext();
            DataTable dtUser = METAOPTION.BAL.MakeUserBAL.GetUserInfo(UserID);
            //GetUserInfo
            // UserID|SystemID|UserName|ScreenName
            String user = String.Empty;
            if (dtUser != null && dtUser.Rows.Count > 0)
                user = String.Format("{0}|1|{1}|{2}"
                 , dtUser.Rows[0]["SecurityUserID"]
                 , dtUser.Rows[0]["UserName"]
                 , dtUser.Rows[0]["DisplayName"]);

            return user;

        }

        [WebMethod]
        [ScriptMethod]
        public bool BuyerCodeAvailability(String BuyerCode)
        {
            if (!string.IsNullOrEmpty(BuyerCode))
            {
                if (METAOPTION.BAL.BuyerBAL.BuyerCodeAvailability(BuyerCode.Trim()) > 0)// Check if BuyerCode already exists
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public CascadingDropDownNameValue[] BindModelDropdown(long MakeID)
        {
            List<GetModelResult> ListOfModels = new List<GetModelResult>();
            ListOfModels = BAL.Common.GetListOfModels(MakeID);

            List<CascadingDropDownNameValue> models = new List<CascadingDropDownNameValue>();
            foreach (var model in ListOfModels)
            {
                string ModelId = model.ModelID.ToString();
                string Model = model.Model.ToString();
                models.Add(new CascadingDropDownNameValue(Model, ModelId));
            }
            return models.ToArray();
        }

        [WebMethod]
        [ScriptMethod]
        public List<CheckInfo> DuplicateCheckPayment(String CheckNo)
        {
            BAL.PaymentBLL bal = new BAL.PaymentBLL();
            Int32 Period = 30;
            if (System.Configuration.ConfigurationManager.AppSettings["DuplicateCheckPeriod"] != null)
                Period = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings.Get("DuplicateCheckPeriod"));
            
            List<Payment_CheckDuplicateCheckNoResult> records = bal.GetDuplicatePaymentDetails(CheckNo, Period);
            return ConvertToCheckInfo(records);

        }

        private List<CheckInfo> ConvertToCheckInfo(List<Payment_CheckDuplicateCheckNoResult> records)
        {
            List<CheckInfo> info = new List<CheckInfo>();
            
            foreach (Payment_CheckDuplicateCheckNoResult rec in records)
            {
                info.Add(new CheckInfo()
                {
                      CheckNo = rec.CheckNumber
                    , CheckDate = String.Format("{0:dd MMM yyyy}", rec.CheckDate)
                    , Amount = String.Format("{0:#,###}", rec.Amount)
                    , DateAdded=String.Format("{0:dd MMM yyyy hh:mm:ss tt}", rec.DateUpdated)
                    , AddedBy= Convert.ToString(rec.AddedBy)
                    , RecipientName=rec.RecipientName
                    , RecipientType=rec.RecepientType
                });           
            }
            return info;
        }
    }
}
