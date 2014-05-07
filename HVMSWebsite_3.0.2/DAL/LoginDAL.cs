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
    public class LoginDAL
    {

        #region [Check User Name]
        /// <summary>
        /// region to check UserName exist or not
        /// </summary>
        /// <returns></returns>
        public bool CheckUserName(string userName)
        {

            bool bTrue = false;
            using (SqlConnection Conn = new SqlConnection(DALDataContext.Factory.DB.Connection.ConnectionString))
            {
                try
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();

                    string sqlStatment = "Select EntityId AS [EntityId]  From SecurityUser Where UserName = @UserName";
                    SqlCommand CMD = new SqlCommand(sqlStatment, Conn);
                    CMD.CommandType = CommandType.Text;

                    CMD.Parameters.AddWithValue("@UserName", userName);
                    Object result = CMD.ExecuteScalar();
                    if (result != null)
                        bTrue = Convert.ToInt32(result) > 0;
                }
                catch (Exception ex)
                {
                    bTrue = false;
                }
            }
            return bTrue;
        }
        #endregion

        #region[ Get Single Value ]
        public bool GetSingleValue(Int64 UserId)
        {
            bool bTrue = false;
            using (SqlConnection Conn = new SqlConnection(DALDataContext.Factory.DB.Connection.ConnectionString))
            {
                try
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();
                    string sqlStatement = @"Select * from SecurityUserGroup WHERE SecurityGroupId = 1 AND UserId = @UserId";
                    SqlCommand CMD = new SqlCommand(sqlStatement, Conn);
                    CMD.Parameters.AddWithValue("@UserId", UserId);
                    CMD.CommandType = CommandType.Text;
                    Object result = CMD.ExecuteScalar();
                    if (result != null)
                        bTrue = Convert.ToInt64(result) > 0;
                }
                catch (Exception ex)
                {
                    bTrue = false;
                }
            }
            return bTrue;
        }
        #endregion

        #region [Check UserName and UserPassword]
        /// <summary>
        /// check username and password 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="UserPassword"></param>
        /// <returns></returns>
        public bool CheckUserPassword(string userName, string userPassword)
        {

            var result = (from p in DALDataContext.Factory.DB.SecurityUsers
                          where p.UserName == userName
                          where p.UserPassword == userPassword
                          select p.SecurityUserID).AsQueryable();

            return result.Count() > 0;

        }
        #endregion

        #region[Get Login]
        /// <summary>
        /// if login name & password match, get login.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetLogin(string userName, string userPassword, string OrgCode)
        {

            var result = DALDataContext.Factory.DB.GetLogin(userName, userPassword, OrgCode).ToList();
            Dictionary<string, string> objDictionary = new Dictionary<string, string>();
            foreach (GetLoginResult item in result)
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

            var result = DALDataContext.Factory.DB.GetLoginWithIP(userName, userPassword, IPAddress, OrgCode).ToList();
            Dictionary<string, string> objDictionary = new Dictionary<string, string>();
            foreach (GetLoginWithIPResult item in result)
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

        #region[Keep login history]
        /// <summary>
        /// Insert employeeId and Current datetime when employee get Login
        /// </summary>
        /// <param name="employeeId"></param>
        public Int64 InsertLoginDetails(long UserId, string IPAddress, bool IsSuccess, string LogInId, string Password, bool IsActive)
        {
            DALDataContext objDal = new DALDataContext();
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
            var result = (from p in DALDataContext.Factory.DB.LoginHistories
                          where p.UserId == employeeId
                          orderby p.LoginHistoryId descending
                          select p.LastLogin).Take(2).Min();


            return result.Value;
        }
        #endregion

        /// <summary>
        /// Display login History both for Web & Access
        /// </summary>
        /// <returns></returns>
        public static GetLoginHistoryResult[] GetLoginHistory(string loginFrom, Int16 OrgID)
        {
            GetLoginHistoryResult[] result = DALDataContext.Factory.DB.GetLoginHistory(loginFrom, OrgID).ToArray();
            return result;
        }

        #region [Added By Vipin 17 Jan 2013 Get the Buyer Info against buyer ID]
        public static DataTable GetBuyer_IsDirect(Int32 BuyerID)
        {
            DataTable dTab = new DataTable();

            DALDataContext objDal = new DALDataContext();
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

        //public void UpdateLogOutTime(Int64 LoginHistoryID)
        //{
        //    DALDataContext objDal = new DALDataContext();
        //    string qry = String.Format("Update LoginHistory set LogOut='{1}' where LoginHistoryId={0}", LoginHistoryID, System.DateTime.Now);
        //    objDal.ExecuteQuery<String>(qry);
        //}

        public String GetUserPermission(long UserID)
        {
            DALDataContext objDal = new DALDataContext();
            var result = objDal.Permission_ByUserID(UserID).AsQueryable();
            string strPermission = string.Empty; ;
            foreach (Permission_ByUserIDResult Item in result)
            {
                strPermission = Item.Permission;
            }
            return strPermission;
        }
        #region[Added by Rupendra 11 Dec 12 for Manage IP Permission]

        public int CheckIPAddess(string IPAddess)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("IPPermissions_CheckIPAddess", Conn);
                Cmd.Parameters.AddWithValue("@IPAddess", IPAddess);
                Cmd.CommandType = CommandType.StoredProcedure;
                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }

        public DataTable EntityIdByUserId(Int64 UserID)
        {
            DataTable dTab = new DataTable();

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("EntityIdByUserId", Conn);
                Cmd.Parameters.AddWithValue("@SecurityUserId", UserID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region [Added by Rupendra 1 Jan 13 for Vendor,Dealer and Buyer login]
        public static GetLoginHistory_Ver211Result[] GetLoginHistory_ver211(string loginFrom, Int32 EntityTypeId)
        {
            GetLoginHistory_Ver211Result[] result = DALDataContext.Factory.DB.GetLoginHistory_Ver211(loginFrom, EntityTypeId).ToArray();
            return result;
        }
        #endregion

        #region [Find all Child buyer if Login buyer is Direct buyer]
        public static DataTable GetChildBuyer(Int32 BuyerID)
        {
            DataTable dTab = new DataTable();

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Buyer_GetChildBuyer", Conn);
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

        #region[Check User IPRestriction]
        public bool CheckIPRestriction(string userName, string password)
        {
            bool Result;
            DALDataContext objDal = new DALDataContext();
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

        #region[Get Default System]
        public int GetDefaultSystem(String OrgCode)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("GetDefaultSystem", Conn);
                Cmd.Parameters.AddWithValue("@OrgCode", OrgCode);
                Cmd.CommandType = CommandType.StoredProcedure;
                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }
        #endregion
    }
}
