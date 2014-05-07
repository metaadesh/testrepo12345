using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION.DAL;
using System.Collections;
using System.Data;

namespace METAOPTION.BAL
{
    public class Admin_UserSettingBAL
    {
        Admin_UserSettingDAL objDAL = new Admin_UserSettingDAL();


        //#region [Get All Employee/User List]
        //public IEnumerable GetAllEmployee()
        //{
        //    return objDAL.GetAllEmployee();
        //}
        //#endregion
        #region [Get All Employee/User List]
        public IEnumerable GetAllEmployee(String initial, String city, String state, String zip, Int32 entityType, Int16 OrgID)
        {
            return objDAL.GetAllEmployee(initial, city, state, zip, entityType, OrgID);

        }
        #endregion

        //#region [Get All User List]
        //public IQueryable GetAllUser()
        //{
        //    return objDAL.GetAllUser();
        //}
        //#endregion

        //#region[Get Employee fullName]
        //public string GetEmployeeFullName(long employeeId)
        //{
        //    return objDAL.GetEmployeeFullName(employeeId);
        //}
        //#endregion

        #region [Get All Employee/User Info]
        public IEnumerable GetAllUserInfo(String userName, string displayName, Int32 EntityTypeID, String SortExpression, Int32 IsActive, Int16 OrgID)
        {
            return objDAL.GetAllUserInfo(userName, displayName, EntityTypeID, SortExpression, IsActive, OrgID);
        }
        #endregion

        //#region [Added by Rupendra 2 Jan 2013 for fetch record on EntityTypeId]
        //public IEnumerable GetAllUserInfo_Ver211(String userName, string displayName, Int32 EntityTypeID, Int32 SecurityUserId, String SortExpression, Int32 IsActive)
        //{
        //    return objDAL.GetAllUserInfo_Ver211(userName, displayName, EntityTypeID, SecurityUserId,SortExpression, IsActive);
        //}
        //#endregion

        #region[Get All Entity Type]
        public List<EntityType> GetAllEntityType()
        {
            return objDAL.GetAllEntityType();
        }
        #endregion


        public DataTable UserEmail(DataTable dtEntityId)
        {
            DataTable dt = objDAL.UserEmailList(dtEntityId);
            return dt;
        }
        public void LogMailContent(String Subject, String Body, Int16 EmailType, String MailTo, String MailFrom, String MailCCed, String MailBCCed, DataTable dtRefID)
        {
            objDAL.LogMailContent(Subject, Body, EmailType, MailTo, MailFrom, MailCCed, MailBCCed, dtRefID);
        }

        #region[Update Security User Active Status]
        public void SecurityUser_UpdateActiveStatus(long UserID, Int32 IsActive)
        {
            objDAL.SecurityUser_UpdateActiveStatus(UserID, IsActive);
        }
        #endregion

        #region [Check whether user name exist or not]
        public bool CheckUserNameExistance(String UserName)
        {
            return objDAL.CheckUserNameExistance(UserName);
        }

        #endregion
    }
}
