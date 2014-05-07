using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using METAOPTION.DAL;

namespace METAOPTION.DAL
{
   public class Admin_LoginDAL
    {
        Admin_DALDataContext objDAL = new Admin_DALDataContext();

        #region[Get Login]
        public Dictionary<string, string> GetLogin(string userName, string userPassword, string OrgCode)
        {

            var result = objDAL.SuperAdminLogin(userName, userPassword, OrgCode).ToList();
            Dictionary<string, string> objDictionary = new Dictionary<string, string>();
            foreach (SuperAdminLoginResult item in result)
            {
                objDictionary.Add("SecurityUserId", item.SecurityUserId.ToString());
                objDictionary.Add("EntityID", item.EntityID.ToString());
                objDictionary.Add("DisplayName", item.DisplayName);
                objDictionary.Add("EntityTypeID", item.EntityTypeID.ToString());
                objDictionary.Add("OrgID", item.OrgID.ToString());
                objDictionary.Add("OrgCode", item.OrgCode);
                objDictionary.Add("Organisation", item.Organisation);
            }
            return objDictionary;

        }

        public Dictionary<string, string> GetLoginWithIP(string userName, string userPassword, string IPAddress, string OrgCode)
        {

            var result = objDAL.SuperAdminLoginWithIP(userName, userPassword, IPAddress, OrgCode).ToList();
            Dictionary<string, string> objDictionary = new Dictionary<string, string>();
            foreach (SuperAdminLoginWithIPResult item in result)
            {
                objDictionary.Add("SecurityUserId", item.SecurityUserId.ToString());
                objDictionary.Add("EntityID", item.EntityID.ToString());
                objDictionary.Add("DisplayName", item.DisplayName);
                objDictionary.Add("EntityTypeID", item.EntityTypeID.ToString());
                objDictionary.Add("OrgID", item.OrgID.ToString());
                objDictionary.Add("OrgCode", item.OrgCode);
                objDictionary.Add("Organisation", item.Organisation);
            }
            return objDictionary;

        }
        #endregion

        #region [Get the Buyer Info against buyer ID]
        public static DataTable GetBuyer_IsDirect(Int32 BuyerID)
        {
            DataTable dTab = new DataTable();

            Admin_DALDataContext objDal = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Buyer_GetIsBuyerType", Conn);
                Cmd.Parameters.AddWithValue("@EntityID", BuyerID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region[Check What type of Permission User Have]
        public String GetUserPermission(long UserID)
        {
            Admin_DALDataContext objDal = new Admin_DALDataContext();
            var result = objDal.Permission_ByUserID(UserID).AsQueryable();
            string strPermission = string.Empty; ;
            foreach (Permission_ByUserIDResult Item in result)
            {
                strPermission = Item.Permission;
            }
            return strPermission;
        }
        #endregion

        #region[Check User IPRestriction]
        public bool CheckIPRestriction(string userName, string password)
        {
            bool Result;
            Admin_DALDataContext objDal = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("User_GetIPRestriction", Conn);
                Cmd.Parameters.AddWithValue("@UserName", userName);
                Cmd.Parameters.AddWithValue("@Password", password);

                Cmd.CommandType = CommandType.StoredProcedure;
                var res = Cmd.ExecuteScalar();
                if (res != null)
                    Result = Convert.ToBoolean(res);
                else
                    Result = false;
                objDal.Dispose();
            }
            return Result;
        }
        #endregion
      
        #region[ Get Email and Password At Admin_ForgetPassword.aspx page ]
        /// <summary>
        ///  Get Email and Password
        /// </summary>
        /// <param name="UserName">Login User Name</param>
        /// <param name="Email">Email to send the password</param>
        /// <returns></returns>
        public DataTable Email_Password(String UserName, String Email)
        {
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            DataTable dTab = new DataTable("EmailPassord");
            using (SqlConnection Conn = new SqlConnection(objDAL.Connection.ConnectionString))
            {
                try
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();

                    SqlCommand Cmd = new SqlCommand("Email_Password", Conn);
                    Cmd.Parameters.AddWithValue("@UserName", UserName);
                    Cmd.Parameters.AddWithValue("@Email", Email);

                    Cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dReader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dTab.Load(dReader);
                    dReader.Close();
                }
                catch
                {
                    dTab = null;
                }
            }
            return dTab;

        }
        #endregion

        #region[ Insert employeeId, Current datetime and Other Details when employee get Login]
        public Int64 InsertLoginDetails(long UserId, string IPAddress, bool IsSuccess, string LogInId, string Password, bool IsActive)
        {
            Admin_DALDataContext objDal = new Admin_DALDataContext();
            LoginHistory objLoginHistory = new LoginHistory();
            objLoginHistory.UserId = UserId;
            objLoginHistory.IPAddress = IPAddress;
            objLoginHistory.IsSuccess = IsSuccess;
            objLoginHistory.LogInId = LogInId;
            objLoginHistory.Password = Password;
            objLoginHistory.LastLogin = DateTime.Now;
            objLoginHistory.IsActive = IsActive == true ? 1 : 0;
            objDal.LoginHistories.InsertOnSubmit(objLoginHistory);
            objDal.SubmitChanges();
            return objLoginHistory.LoginHistoryId;
        }
        #endregion

        #region [Get Last LoginDetails]
        public DateTime GetLastLogin(long employeeId)
        {
            var result = (from p in Admin_DALDataContext.Factory.DB.LoginHistories
                          where p.UserId == employeeId
                          orderby p.LoginHistoryId descending
                          select p.LastLogin).Take(2).Min();


            return result.Value;
        }
        #endregion

        #region[Insert User Logout details in table LoginHistory, When click on Logout]
        public static void Logout_Session(Int64 LoginHistoryID)
        {
            METAOPTION.Admin_DALDataContext.Factory.DB.Logout(LoginHistoryID);
        }
        #endregion


    }
}
