using System;
using System.Data.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION.DAL;
using System.Collections;
using System.Data;

namespace METAOPTION.BAL
{
    public class EmployeeListBAL
    {
        EmployeeListDAL objEmployeeListDAL = new EmployeeListDAL();


        #region [Get All Employee/User List]
        public IEnumerable GetAllEmployee()
        {
            return objEmployeeListDAL.GetAllEmployee();
        }
        #endregion
        #region [Get All Employee/User List]
        public IEnumerable GetAllEmployee(String initial, String city, String state, String zip, Int32 entityType, Int16 OrgID)
        {
            return objEmployeeListDAL.GetAllEmployee(initial, city, state, zip, entityType, OrgID);

        }
        #endregion

        #region [Get All User List]
        public IQueryable GetAllUser()
        {
            return objEmployeeListDAL.GetAllUser();
        }
        #endregion

        #region[Get Employee fullName]
        public string GetEmployeeFullName(long employeeId)
        {
            return objEmployeeListDAL.GetEmployeeFullName(employeeId);
        }
        #endregion

        #region [Get All Employee/User Info]
        public IEnumerable GetAllUserInfo(String userName, string displayName, Int32 EntityTypeID, String SortExpression, Int32 IsActive, Int16 OrgID)
        {
            return objEmployeeListDAL.GetAllUserInfo(userName, displayName, EntityTypeID, SortExpression, IsActive, OrgID);
        }
        #endregion

        #region [Added by Rupendra 2 Jan 2013 for fetch record on EntityTypeId]
        public IEnumerable GetAllUserInfo_Ver211(String userName, string displayName, Int32 EntityTypeID, Int32 SecurityUserId, String SortExpression, Int32 IsActive)
        {
            return objEmployeeListDAL.GetAllUserInfo_Ver211(userName, displayName, EntityTypeID, SecurityUserId,SortExpression, IsActive);
        }
        #endregion

        #region[Get All Entity Type]
        public List<EntityType> GetAllEntityType()
        {
            return objEmployeeListDAL.GetAllEntityType();
        }
        #endregion


        public DataTable UserEmail(DataTable dtEntityId)
        {
            DataTable dt = objEmployeeListDAL.UserEmailList(dtEntityId);
            return dt;
        }
        public void LogMailContent(String Subject, String Body, Int16 EmailType, String MailTo, String MailFrom, String MailCCed, String MailBCCed, DataTable dtRefID)
        {
            objEmployeeListDAL.LogMailContent(Subject, Body, EmailType, MailTo, MailFrom, MailCCed, MailBCCed, dtRefID);
        }
    }
}
