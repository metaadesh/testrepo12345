using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Collections;
using System.Data.SqlClient;

namespace METAOPTION.DAL
{
    public class EmployeeListDAL
    {
        DALDataContext objDAL = new DALDataContext();

        #region [Get All Employee/User List]
        public IEnumerable GetAllEmployee()
        {
            return objDAL.GetEmployeeList().AsEnumerable();

        }
        #endregion
        #region [Get All Employee/User List]
        public IEnumerable GetAllEmployee(String initial, String city, String state, String zip, Int32 entityType, Int16 OrgID)
        {
            return objDAL.spEntity5in1(initial, city, state, zip, entityType, OrgID).AsEnumerable();

        }
        #endregion

        #region [Get All User List]
        public IQueryable GetAllUser()
        {
            IQueryable result = (from p in objDAL.SecurityUsers
                                 select new { EmployeeId = p.SecurityUserID, p.UserName, p.DisplayName, p.UserNote, p.IsActive, p.UserPassword }) as IQueryable;
            return result;
        }
        #endregion

        #region[Get Employee fullName]
        public string GetEmployeeFullName(long employeeId)
        {
            var result = objDAL.GetEmployeeFullName(employeeId).AsQueryable();
            string strFullName = string.Empty; ;
            foreach (GetEmployeeFullNameResult Item in result)
            {
                strFullName = Item.FullName;
            }
            return strFullName;
        }
        #endregion

        #region [Get All Employee/User Info]
        public IEnumerable GetAllUserInfo(String userName, string displayName, Int32 EntityTypeID, String SortExpression, Int32 IsActive, Int16 OrgID)
        {
            return objDAL.GetAllUserInfo(userName, displayName, EntityTypeID, SortExpression, IsActive, OrgID).AsEnumerable();
        }
        #endregion

        #region
        public IEnumerable GetAllUserInfo_Ver211(String userName, string displayName, Int32 EntityTypeID, Int32 SecurityUserId, String SortExpression, Int32 IsActive)
        {
            return objDAL.GetAllUserInfo_Ver211(userName, displayName, EntityTypeID,SecurityUserId, SortExpression, IsActive).AsEnumerable();
        }
        #endregion

        #region[Get All Entity Type]
        public List<EntityType> GetAllEntityType()
        {
            DALDataContext objDal = new DALDataContext();
            List<EntityType> result = (from p in objDal.EntityTypes
                                       where p.IsRealEntity  == true && p.IsActive==1
                                       select p).OrderBy(p => p.EntityType1).ToList<EntityType>();
            return result;
        }
        #endregion


        public DataTable UserEmailList(DataTable dtEntityId)
        {
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ContactUserEmails"))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    SqlParameter cmdEntityIde = new SqlParameter("@dtUserId", dtEntityId);
                    cmdEntityIde.SqlDbType = SqlDbType.Structured;
                    cmd.Parameters.Add(cmdEntityIde);
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dTab.Load(reader);
                    reader.Close();
                    objDal.Dispose();
                    return dTab;
                }

            }

        }
        public void LogMailContent(String Subject, String Body, int EmailType, String MailTo, String MailFrom, String MailCCed, String MailBCCed, DataTable dtRefID)
        {
            //String _ConnectionString = ConfigurationManager.ConnectionStrings["HVMSConnectionString"].ConnectionString;
            DALDataContext DC = new DALDataContext();

            using (SqlConnection conn = new SqlConnection(DC.Connection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Mobile_LogEmailContent"))
                {
                    try
                    {
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        cmd.Parameters.Add(new SqlParameter("@Subject", Subject));
                        cmd.Parameters.Add(new SqlParameter("@Body", Body));
                        cmd.Parameters.Add(new SqlParameter("@EmailType", EmailType));
                        cmd.Parameters.Add(new SqlParameter("@MailTo", MailTo));
                        cmd.Parameters.Add(new SqlParameter("@MailFrom", MailFrom));
                        cmd.Parameters.Add(new SqlParameter("@MailCCed", MailCCed));
                        cmd.Parameters.Add(new SqlParameter("@MailBCCed", MailBCCed));


                        SqlParameter cmdFieldCustomValue = new SqlParameter("@RefIDs", dtRefID);
                        cmdFieldCustomValue.SqlDbType = SqlDbType.Structured;
                        cmd.Parameters.Add(cmdFieldCustomValue);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    { }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }
    }
}