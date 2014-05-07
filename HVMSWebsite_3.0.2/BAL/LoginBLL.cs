using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION.DAL;
using System.Web;
using System.Data;

namespace METAOPTION
{

    public class LoginBLL
    {
        LoginDAL objLoginDAL = new LoginDAL();

        #region [Check User Name]
        /// <summary>
        /// region to check UserName exist or not
        /// </summary>
        /// <returns></returns>
        public bool CheckUserName(string userName)
        {
            return objLoginDAL.CheckUserName(userName);
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
            return objLoginDAL.CheckUserPassword(userName, userPassword);
        }
        #endregion

        #region[ Get Single Value ]
        public bool GetSingleValue(Int64 UserId)
        {
            return objLoginDAL.GetSingleValue(UserId);
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
            return objLoginDAL.GetLogin(userName, userPassword, OrgCode);
        }

        public Dictionary<string, string> GetLoginWithIP(string userName, string userPassword, string IPAddress, string OrgCode)
        {
            return objLoginDAL.GetLoginWithIP(userName, userPassword, IPAddress, OrgCode);
        }
        #endregion

        #region[Keep login history]
        /// <summary>
        /// Insert employeeId and Current datetime when employee get Login
        /// </summary>
        /// <param name="employeeId"></param>
        public Int64 InsertLoginDetails(long UserId, string IPAddress, bool IsSuccess, string LogInId, string Password, bool IsActive)
        {
            return objLoginDAL.InsertLoginDetails(UserId, IPAddress, IsSuccess, LogInId, Password, IsActive);
        }
        #endregion

        #region [Get Last LoginDetails]
        public DateTime GetLastLogin(long employeeId)
        {
            #region[OldCode]
            //return objLoginDAL.GetLastLogin(employeeId);
            #endregion

            if (HttpContext.Current.Session[CacheEnum.LastLogin] == null)
            {
                DateTime lastlogin = objLoginDAL.GetLastLogin(employeeId);
                HttpContext.Current.Session.Add(CacheEnum.LastLogin, lastlogin);
                return lastlogin;
            }
            else
                return (DateTime)HttpContext.Current.Session[CacheEnum.LastLogin];
        }
        #endregion

        /// <summary>
        /// Display login History both for Web & Access
        /// </summary>
        /// <returns></returns>
        public static GetLoginHistoryResult[] GetLoginHistory(string loginFrom,Int16 OrgID)
        {
            return LoginDAL.GetLoginHistory(loginFrom, OrgID);
        }


        #region [Added By Vipin 17 Jan 2013 Get the Buyer Info against buyer ID]
        public DataTable GetBuyer_IsDirect(Int32 BuyerID)
        {
            return LoginDAL.GetBuyer_IsDirect(BuyerID);
        }
        #endregion
        //public void UpdateLogOutTime(Int64 LoginHistoryID)
        //{
        //    objLoginDAL.UpdateLogOutTime(LoginHistoryID);
        //}

        public String GetUserPermission(long UserID)
        {
            return objLoginDAL.GetUserPermission(UserID);
        }

        #region[Added by Rupendra 11 Dec 12 for Manage IP Permission]
        public int CheckIPAddess(string IPAddess)
        {
            return objLoginDAL.CheckIPAddess(IPAddess);
        }

        public DataTable EntityIdByUserId(Int64 UserID)
        {
            return objLoginDAL.EntityIdByUserId(UserID);
        }
        #endregion

        #region [Added by Rupendra 1 Jan 13 for Vendor, Dealer and Buyer Logins]
        public static GetLoginHistory_Ver211Result[] GetLoginHistory_ver211(string loginFrom, Int32 EntityTypeId)
        {
            return LoginDAL.GetLoginHistory_ver211(loginFrom, EntityTypeId);
        }
        #endregion

        #region [Find all Child buyer if Login buyer is Direct buyer]
        public DataTable GetChildBuyer(Int32 BuyerID)
        {
            return LoginDAL.GetChildBuyer(BuyerID);
        }
        #endregion

        #region[Check User IPRestriction]
        public bool CheckIPRestriction(string userName, string password)
        {
            LoginDAL objDal = new LoginDAL();
            return objDal.CheckIPRestriction(userName, password);
        }
        #endregion

        #region[Get Default System]
        public int GetDefaultSystem(String OrgCode)
        {
            return objLoginDAL.GetDefaultSystem(OrgCode);
        }
        #endregion
    }
}
