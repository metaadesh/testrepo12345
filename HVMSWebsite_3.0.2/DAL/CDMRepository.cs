using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;
using CDMService = METAOPTION.CDMService;
using METAOPTION.CDMService;
using System.Net;
using System.Configuration;
using System.Web;

namespace METAOPTION.DAL
{
    public class CDMRepository
    {
        public class Header
        {
            public string MethodName { get; set; }
            public string ReferenceID { get; set; }
            public string TransactionStatus { get; set; }
            public string TransactionTime { get; set; }
            public string TransactionType { get; set; }
            public string Reference { get; set; }
            public class CDMResponse
            {
                public int ErrorCode { get; set; }
                public string ErrorMessage { get; set; }
            }
        }

        public string  DecodeVin(string vin)
        {
            String responseData = String.Empty, responseStatus = String.Empty;
            DALDataContext db = new DALDataContext();
            try
            {
                String SystemCode = ConfigurationManager.AppSettings["CDMSystemCode"].ToString();
                String DealerCode = ConfigurationManager.AppSettings["CDMDealerCode"].ToString();
                String SecurityCode = ConfigurationManager.AppSettings["CDMSecurityCode"].ToString();

                String Requestparameters = string.Empty;
                String Servicename = "DecodeVIN";
                MyJsonDictionary<string, string> result = new MyJsonDictionary<string, string>();
                StringBuilder InventoryRecord = new StringBuilder();
                result["vin"] = String.Format("{0}", vin);// InventoryRecord.ToString();
                result["RH"] = "{\"DeviceID\":\"082932A2-EB84-5C8A-928B-834AD28D6650\",\"ApplicationID\":\"20\",\"DeviceTypeID\":\"40\",\"SystemCode\":\""+SystemCode+"\",\"DealerCode\":\""+DealerCode+"\",\"SecurityCode\":\""+SecurityCode+"\",\"DeviceName\":\"Browser\",\"DeviceOSVersion\":\"1.0\",\"AppVersion\":\"1.0.0\",\"MobileCarrier\":\"@txt.att.net\",\"Latitude\":\"28.571434\", \"Longitude\":\"77.269784\"}";
                Requestparameters = Serialize(result);
                
                responseData = SendRequest(Servicename, Requestparameters);
                ServiceResponseOfResponseTableDataUs8T9G22 chromeTablesData = JsonDeserialize<ServiceResponseOfResponseTableDataUs8T9G22>(responseData);

                responseStatus = chromeTablesData.Header.Response.ErrMessage.ToString();

                if (chromeTablesData.Header.Response.ErrMessage == "Success" && chromeTablesData.Data != null)
                {
                    db.CDM_HVMS_TABLES_UPDATE_For_VIN(vin);
                    //CDM_UpdateTable(chromeTablesData);
                }
            }
            catch (Exception ex)
            {
                SaveError(new ErrorLog
                {
                    ErrorMessage = String.Format("{0}{1}{2}", ex.Message, Environment.NewLine, ex.InnerException),
                    MethodName = "DecodeVin",
                    LoggedTime = DateTime.Now
                });
                throw new Exception(String.Format("DecodeVIN: {0}", ex.Message));
            }
            return responseStatus;
        }

        public string SendRequest(string ServiceName, string RequestParameters)
        {
            CDMRepository cdmRepor = new CDMRepository();
            HttpWebRequest req = null;
            HttpWebResponse res = null;
            string urlparam = string.Empty;
            String serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"].ToString();
            string url = serviceUrl + "/" + ServiceName;
            req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json; charset=utf-8";
            req.Timeout = 300000;

            Stream reqStream = req.GetRequestStream();
            string fileContents = RequestParameters;

            byte[] fileToSend = Encoding.UTF8.GetBytes(fileContents);
            reqStream.Write(fileToSend, 0, fileToSend.Length);
            reqStream.Close();

            //req.ProtocolVersion = HttpVersion.Version10;
            //req.ServicePoint.Expect100Continue = false;

            res = (HttpWebResponse)req.GetResponse();
            Stream responseStream = res.GetResponseStream();
            var streamReader = new StreamReader(responseStream);

            var str = streamReader.ReadToEnd();
            streamReader.Close();
            streamReader.Dispose();

            return str.ToString();
        }

          public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }

         public static String Serialize(Object data)
        {
            var serializer = new DataContractJsonSerializer(data.GetType());
            var ms = new MemoryStream();
            serializer.WriteObject(ms, data);

            return Encoding.UTF8.GetString(ms.ToArray());
        }
    
    [Serializable]
    public class MyJsonDictionary<K, V> : ISerializable
    {
        Dictionary<K, V> dict = new Dictionary<K, V>();

        public MyJsonDictionary() { }

        protected MyJsonDictionary(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            foreach (K key in dict.Keys)
            {
                info.AddValue(key.ToString(), dict[key]);
            }
        }

        public void Add(K key, V value)
        {
            dict.Add(key, value);
        }

        public V this[K index]
        {
            set { dict[index] = value; }
            get { return dict[index]; }
        }
    }

       

         public  void SaveError(ErrorLog log)
        {
           DALDataContext db = new DALDataContext();
           log.LoggedTime = DateTime.Now;
           db.ErrorLogs.InsertOnSubmit(log);
           db.SubmitChanges();

         }
        
    }
}