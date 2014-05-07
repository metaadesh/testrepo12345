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
using System.Text;
using METAOPTION.ManheimAutoAuction_OnlineRegistration;
using System.Net;
namespace METAOPTION
{
    public partial class TestManheim_Page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //   txtRequestForReg.Text = "";
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            file objFile = GenerateRequestFile();
            UploadFileResponse resp = RequestIntiate_For_Registration(objFile);
            vehicleResult [] vehicleProcessed = resp.vehicles;
            
            bool isSuccess  = resp.success;
            string strErrorMessage = resp.error;

            lblErrorMessage.Text = "Is Succes " + isSuccess.ToString() + strErrorMessage;
            gvVehiclesAccepted.DataSource = vehicleProcessed;
            gvVehiclesAccepted.DataBind();
        }
        /// <summary>
        /// Generate Request File
        /// </summary>
        /// <returns></returns>
        public file GenerateRequestFile()
        {
            //Prepare
            file objRequestFile = new METAOPTION.ManheimAutoAuction_OnlineRegistration.file();
            txtRequestForReg.Text = "";

            System.IO.StreamReader sr = new System.IO.StreamReader("c:\\HEADSTARTVMS_10292009_171623.txt");
            string line;

            while (sr.Peek() != -1)
            {
                line = sr.ReadLine();
                //Response.Write(Server.HtmlEncode(line) + "<br/>");
                txtRequestForReg.Text = txtRequestForReg.Text + line;
            }

            //Base 64 Encoded string
            objRequestFile.filestring = "IkludmVudG9yeV9JRCIsIlJlZ19UeXBlIiwiU2FsZV9EYXRlIiwiTGFuZV9SdW4iLCJWaW4iLCJZ" +
"ZWFyIiwiTWFrZSIsIk1vZGVsIiwiQm9keSIsIkNvbG9yIiwiTWlsZWFnZSIsIkVuZ2luZSIsIlJh" +
"ZGlvIiwiVHJhbnMiLCJQUyIsIlBCIiwiQUMiLCJFVyIsIkVTIiwiVE9QIiwiQ0MiLCJEUkwiLCJU" +
"aXRsZSINCiIxMTUyMDMiLCJGUklEQVkiLCIxMC8zMC8yMDA5IiwiMTItMDAyMCIsIjJCNEdQNDRH" +
"MVhSMjcxOTMyIiwiMTk5OSIsIkRvZGdlIiwiQ2FyYXZhbiIsIiIsIiIsIjE3MTQxMyIsIiIsIiIs" +
"IkFUIiwiIiwiIiwiWSIsIlkiLCJOIiwiIiwiIiwiWSIsIlkiDQoiMTE1NjE0IiwiRlJJREFZIiwi" +
"MTAvMzAvMjAwOSIsIjEyLTAwNDAiLCJZVjFUUzU5MjQ1MTM5NTk5NyIsIjIwMDUiLCJWb2x2byIs" +
"IlM4MCIsIiIsIiIsIjExMDM3MCIsIiIsIiIsIkFUIiwiIiwiIiwiWSIsIlkiLCJOIiwiIiwiIiwi" +
"WSIsIlki";
    
    //base64Encode(txtRequestForReg.Text);
            
            //This indicates,this is test request
            objRequestFile.test = true;
            return objRequestFile;
        
        }

        /// <summary>
        /// Encode string to base64
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string base64Encode(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Encode" + e.Message);
            }
        }
        /// <summary>
        /// Decode string to base64
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string base64Decode(string data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Decode" + e.Message);
            }
        }

        /// <summary>
        /// Initiate request for file registration and return response xml
        /// </summary>
        /// <param name="objFile"></param>
        /// <returns></returns>
        public UploadFileResponse RequestIntiate_For_Registration(file objFile)
        {

            // Create the Web Request to Manheim Web Service
            HttpWebRequest request
                = WebRequest.Create("http://www.manheimautoauctiononline3.com/hhead/RegisterVehicles.php") as HttpWebRequest;

            // Add authentication to request   
            request.Credentials = new NetworkCredential("hhead_webservice", "service$user");

            //Call Web Service method to initiate Registration Request
            RegisterVehicles objRegisterVehicles = new METAOPTION.ManheimAutoAuction_OnlineRegistration.RegisterVehicles();
            
            //Provide Credentials to process Current Web Request 
            objRegisterVehicles.Credentials = request.Credentials;
         
            //Call web service method Upload File, through proxy class method
            return objRegisterVehicles.UploadFile(objFile);
        }
    }
}
