using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using METAOPTION;
using METAOPTION.DAL;
using System.Web;
using System.Collections.Generic;

namespace METAOPTION.BAL
{
    public class Admin_MakeUserBAL
    {       
        #region[Assign LoginID and Password To Employee]
        public Int32 AssignLoginPassword(SecurityUser objSecurityUser)
        {
           Admin_MakeUserDAL objUser = new Admin_MakeUserDAL();
           return objUser.AssignLoginPassword(objSecurityUser);
        }
        #endregion

        #region [Get Associated Groups with a User]
        public IQueryable GetAssociatedGroups(long EmpId)
        {
           Admin_MakeUserDAL objUser = new Admin_MakeUserDAL();
           return objUser.GetAssociatedGroups(EmpId);
        }
        #endregion

        #region[ Get User Info By User Id ]
       /// <summary>
        /// Get User Info By User Id
       /// </summary>
       /// <param name="userId">User Id</param>
       /// <returns></returns>
        public static DataTable GetUserInfo(long userId)
        {
           Admin_MakeUserDAL objUser = new Admin_MakeUserDAL();
           return objUser.GetUserInfo(userId);
        }
        #endregion

        #region[ Get ALL Active Groups ]
        /// <summary>
        /// Get ALL Active Groups 
        /// </summary>
        /// <returns></returns>
        public static DataTable ActiveGroups(String GroupName, String GroupDescription, Int16 OrgID)
        {
            Admin_MakeUserDAL objUser = new Admin_MakeUserDAL();
            return objUser.ActiveGroups(GroupName, GroupDescription, OrgID);
        }
        #endregion
        #region[ Add Group to database ]
        /// <summary>
        /// Add Group to database 
        /// </summary>
        /// <returns></returns>
        public static Int32 AddUserGroup(long userId, long securityGroupId, long addedBy)
        {
           Admin_MakeUserDAL objUser = new Admin_MakeUserDAL();
           HttpContext.Current.Cache.Remove(Admin_CacheEnum.PagePermissions);
           return objUser.AddUserGroup(userId, securityGroupId, addedBy);
        }
        #endregion

        #region[ Activate/Deactivate Associated Group ]
        /// <summary>
        /// Activate/Deactivate Associated Group
        /// </summary>
        /// <param name="SecurityUserGroupId">SecurityUserGroup Id</param>
        /// <param name="IsActive">Is Active</param>
        /// <param name="deletedBy">Activated By User</param>
        /// <returns></returns>
        public static bool Activate_Deactivate_User_Associated_Group(long SecurityUserGroupId, int IsActive, long deletedBy)
        {
           Admin_MakeUserDAL objUser = new Admin_MakeUserDAL();
           HttpContext.Current.Cache.Remove(Admin_CacheEnum.PagePermissions);
           return objUser.Activate_Deactivate_User_Associated_Group(SecurityUserGroupId, IsActive, deletedBy);   
        }
        #endregion        

        #region[ Get Email and Password ]
        /// <summary>
        ///  Get Email and Password
        /// </summary>
        /// <param name="UserName">Login User Name</param>
        /// <param name="Email">Email to send the password</param>
        /// <returns></returns>
        public static DataTable Email_Password(String UserName, String Email)
        {
           Admin_MakeUserDAL objUser = new Admin_MakeUserDAL();
           return objUser.Email_Password(UserName, Email);
        }
        #endregion  
      
        #region[ Update Security User Information ]
        /// <summary>
        ///  Update Security User Information
        /// </summary>
        /// <param name="DisplayName">Display Name</param>
        /// <param name="UserNote">Note</param>
        /// <param name="ModifiedBy">Modified By</param>
        /// <param name="IsActive">Is Active?</param>
        /// <param name="SecurityUserId">User Id</param>
        /// <returns></returns>
        public static bool SecurityUser_Update(String DisplayName, String UserNote, Int64 ModifiedBy, Int32 IsActive, Int64 SecurityUserId)
        {
           Admin_MakeUserDAL objDAL = new Admin_MakeUserDAL();
           return objDAL.SecurityUser_Update(DisplayName, UserNote, ModifiedBy, IsActive, SecurityUserId);
            
        }
        #endregion
        //public static bool ChangedPassword(Int64 SecurityUserId, String NewPassword)
        //{
        //    Admin_MakeUserDAL objDAL = new Admin_MakeUserDAL();
        //    return objDAL.ChangedPassword(SecurityUserId, NewPassword);

        //}
        #region[ Check User Old Password ]
        /// <summary>
        /// Check User Old Password
        /// </summary>
        /// <param name="userPassword">userPassword</param>
        /// <returns>true or false</returns>
        public static bool CheckUserOldPassword(string userPassword, Int64 SecurityUserID)
        {
            Admin_MakeUserDAL objDAL = new Admin_MakeUserDAL();
            return objDAL.CheckUserOldPassword(userPassword, SecurityUserID);
        }
         #endregion

        #region[Update Security User Active Status]
        public static void SecurityUser_UpdateActiveStatus(long UserID, Int32 IsActive)
        {
            Admin_MakeUserDAL.SecurityUser_UpdateActiveStatus(UserID, IsActive);
        }
        #endregion

        #region[Fetch User Settings]
        public DataTable UserSettings(long UserID)
        {
            Admin_MakeUserDAL dal = new Admin_MakeUserDAL();
            return dal.UserSettings(UserID);
        }
        #endregion

        #region[Update User Settings]
        public void UpdateUserSettings(long UserID, String SettingKeyValue)
        {
            Admin_MakeUserDAL objDAL = new Admin_MakeUserDAL();
            objDAL.UpdateUserSettings(UserID, SettingKeyValue);
        }
        #endregion

        #region [Bind IPPersmission for Users]
        public  List<IPUser_PermissionSelectResult> BindIPPermission(Int32 UserID)
        {
            Admin_MakeUserDAL objDAL = new Admin_MakeUserDAL();
            return objDAL.BindIPPermission(UserID);

        }
        #endregion


        public Int32 AutoIPRestriction(Int32 IPUserId, Int32 ModifiedBY, Int32 IPType, int Flag)
        {
            Admin_MakeUserDAL objDAL = new Admin_MakeUserDAL();
            return objDAL.AutoIPRestriction(IPUserId, ModifiedBY, IPType, Flag);
        }
    }
}
