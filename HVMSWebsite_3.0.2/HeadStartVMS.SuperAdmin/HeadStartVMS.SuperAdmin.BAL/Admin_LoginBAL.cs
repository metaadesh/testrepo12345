using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using METAOPTION.DAL;

namespace METAOPTION.BAL
{
    public class Admin_LoginBAL
    {
     
        Admin_LoginDAL  objLoginDAL = new Admin_LoginDAL();

        #region[Get Login]

        public Dictionary<string, string> GetLogin(string userName, string userPassword, string OrgCode)
        {
            return objLoginDAL.GetLogin(userName, userPassword, OrgCode);
        }

        public Dictionary<string, string> GetLoginWithIP(string userName, string userPassword, string IPAddress, string OrgCode)
        {
            return objLoginDAL.GetLoginWithIP(userName, userPassword, IPAddress, OrgCode);
        }

        #endregion

        #region [Get the Buyer Info against buyer ID]
        public DataTable GetBuyer_IsDirect(Int32 BuyerID)
        {
            return Admin_LoginDAL.GetBuyer_IsDirect(BuyerID);
                   
         }
        #endregion

        #region[Get User Permision]
        public String GetUserPermission(long UserID)
        {
            return objLoginDAL.GetUserPermission(UserID);
        }
        #endregion

        #region[Check User IPRestriction]
        public bool CheckIPRestriction(string userName, string password)
        {
            Admin_LoginDAL objDal = new Admin_LoginDAL();
            return objDal.CheckIPRestriction(userName, password);
        }
        #endregion
      
        #region[ Get Email and Password For Forget password Page ]
        /// <summary>
        ///  Get Email and Password
        /// </summary>
        /// <param name="UserName">Login User Name</param>
        /// <param name="Email">Email to send the password</param>
        /// <returns></returns>
        public static DataTable Email_Password(String UserName, String Email)
        {
            Admin_LoginDAL objUser = new Admin_LoginDAL();
            return objUser.Email_Password(UserName, Email);
        }
        #endregion 

        #region[ Insert employeeId, Current datetime and Other Details when employee get Login]
        public Int64 InsertLoginDetails(long UserId, string IPAddress, bool IsSuccess, string LogInId, string Password, bool IsActive)
        {
            return objLoginDAL.InsertLoginDetails(UserId,IPAddress,IsSuccess,LogInId,Password,IsActive);
        }

        #endregion

        #region [Get Last LoginDetails]
        public DateTime GetLastLogin(long employeeId)
        {
           return objLoginDAL.GetLastLogin(employeeId);
        }
        #endregion

        #region[Insert User Logout details in table LoginHistory, When click on Logout]
        public static void Logout_Session(Int64 LoginHistoryID)
        {
            Admin_LoginDAL.Logout_Session(LoginHistoryID);
        }
        #endregion


    }
}
