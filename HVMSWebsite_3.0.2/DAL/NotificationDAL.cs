using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace METAOPTION
{
    public class NotificationDAL
    {
        #region[Get all entity type for Notification]
        public List<EntityType> GetAllEntityType_Notification()
        {
            DALDataContext objDal = new DALDataContext();
            List<EntityType> result = (from p in objDal.EntityTypes
                                       where p.IsActive == 1 && p.IsRealEntity == true && p.EntityTypeId != 1
                                       select p).OrderBy(p => p.EntityType1).ToList<EntityType>();
            return result;
        }
        #endregion

        #region[Entities by Entity Type Id]
        public DataTable GetEntitiesByEntityType(int EntityTypeId, short OrgID)
        {
            DataTable dTab = new DataTable("Entity");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("EntitiesByEntityType", Conn);

                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeId);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region[Get all Table Notification]
        public List<TableMaster> GetAllTables_Notification()
        {
            DALDataContext objDal = new DALDataContext();
            List<TableMaster> result = (from p in objDal.TableMasters
                                        where p.IsActive == true
                                        select p).OrderBy(p => p.TableName).ToList<TableMaster>();
            return result;
        }
        #endregion

        #region[Get NotificationType by TableID, EntityTypeID & EntityID]
        public List<NotificationType> GetNotificationTypeByTableID(Int32 TableID)
        {
            DALDataContext dal = new DALDataContext();
            List<NotificationType> result = (from p in dal.NotificationTypes
                                             where p.TableID == TableID && p.IsActive == true
                                             select p).ToList<NotificationType>();
            return result;
        }

        //public DataTable GetNotificationTypeForUser(Int32 TableID, Int32 EntityTypeID, long EntityID)
        //{
        //    DataTable dTab = new DataTable("UserNotification");

        //    DALDataContext objDal = new DALDataContext();
        //    using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
        //    {
        //        if (Conn.State == ConnectionState.Closed)
        //            Conn.Open();

        //        SqlCommand Cmd = new SqlCommand("NotificationType_Users", Conn);
        //        Cmd.Parameters.AddWithValue("@TableID", TableID);
        //        Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
        //        Cmd.Parameters.AddWithValue("@EntityID", EntityID);
        //        Cmd.CommandType = CommandType.StoredProcedure;
        //        SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        dTab.Load(reader);
        //        reader.Close();
        //        objDal.Dispose();
        //    }
        //    return dTab;
        //}
        #endregion

        #region [Add Notification Preference]
        public Int32 NotificationPreference_Insert(NotificationPreference objNotPref)
        {
            Nullable<Int32> np = null;
            Int32 NotPreferenceID = 0;
            METAOPTION.DALDataContext.Factory.DB.NotificationPreference_Insert(
                    objNotPref.NotificationPreferenceID
                  , objNotPref.NotificationTypeID
                  , objNotPref.EntityTypeID
                  , objNotPref.EntityID
                  , objNotPref.ColumnID
                  , objNotPref.NotifyViaEmail
                  , objNotPref.NotifyViaSMS
                  , objNotPref.AddedBy
                  , objNotPref.IsActive
                  , objNotPref.IsRealEntity);

            if (np.HasValue)
                NotPreferenceID = np.Value;
            return NotPreferenceID;
        }
        #endregion

        #region[Get Columns To Notify]
        public List<TableColumnMaster> GetColumnsToNotify(Int32 TableID)
        {
            DALDataContext dal = new DALDataContext();
            List<TableColumnMaster> result = (from p in dal.TableColumnMasters
                                              where p.TableID == TableID && p.IsActive == true && p.Notify == true
                                              select p).ToList<TableColumnMaster>();
            return result;
        }
        #endregion

        #region[Get columns already notified to user]
        public List<TableColumnMaster> GetAlreadyNotifiedColumns(Int32 TableID, Int32 EntityTypeID, long EntityID)
        {
            DALDataContext dal = new DALDataContext();
            List<TableColumnMaster> result = (from t in dal.TableColumnMasters
                                              join p in dal.NotificationPreferences on t.ColumnID equals p.ColumnID
                                              where t.TableID == TableID && p.IsActive == true && t.Notify == true
                                                && p.EntityTypeID == EntityTypeID && p.EntityID == EntityID
                                              select t).ToList<TableColumnMaster>();
            return result;
        }
        #endregion

        #region[Fetch User's Notifications]
        public DataTable FetchUserNotification(Int32 EntityTypeID, long EntityID)
        {
            DataTable dTab = new DataTable("Notification");

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("UserNotificationStatus", Conn);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region[Update notify via Email and SMS]
        public int UpdateNotifyViaEmail(Int32 PreferenceID, Boolean NotifyViaEmail, long ModifiedBy)
        {
            return METAOPTION.DALDataContext.Factory.DB.NotificationStatus_Email(PreferenceID, NotifyViaEmail, ModifiedBy);
        }

        public int UpdateNotifyViaSMS(Int32 PreferenceID, Boolean NotifyViaSMS, long ModifiedBy)
        {
            return METAOPTION.DALDataContext.Factory.DB.NotificationStatus_SMS(PreferenceID, NotifyViaSMS, ModifiedBy);
        }
        #endregion

        #region[Delete notification preference]
        public int DeleteNotificationPreference(Int32 PreferenceID, long DeletedBy)
        {
            return METAOPTION.DALDataContext.Factory.DB.NotificationPreference_Delete(PreferenceID, DeletedBy);
        }
        #endregion

        #region[Get All Notification Type]
        public List<NotificationType> GetAllNotificationType()
        {
            DALDataContext dal = new DALDataContext();
            List<NotificationType> result = (from n in dal.NotificationTypes
                                             where n.IsActive == true
                                             select n).ToList<NotificationType>();
            return result;
        }
        #endregion

        #region[Get All Employee Type]
        public List<EmployeeType> GetAllEmployeeType()
        {
            DALDataContext dal = new DALDataContext();
            List<EmployeeType> result = (from n in dal.EmployeeTypes
                                         where n.IsActive == 1
                                         select n).ToList<EmployeeType>();
            return result;
        }
        #endregion

        #region[Search Employee]
        public DataTable SearchEmployee(Int32 EmployeeTypeId, String Email, String CellPhone, String FirstName, String LastName, Int32 NotificationTypeID, Int16 OrgID)
        {
            DataTable dTab = new DataTable("Employee");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("Employee_Search", Conn);

                Cmd.Parameters.AddWithValue("@EmployeeTypeId", EmployeeTypeId);
                Cmd.Parameters.AddWithValue("@Email", Email);
                Cmd.Parameters.AddWithValue("@CellPhone", CellPhone);
                Cmd.Parameters.AddWithValue("@FName", FirstName);
                Cmd.Parameters.AddWithValue("@LName", LastName);
                Cmd.Parameters.AddWithValue("@NotificationTypeID", NotificationTypeID);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region[Fetch CC/BCC preference already set for a NotificationType]
        public DataTable GetCcBcc_NotificationType(Int32 NotificationTypeID, Int16 OrgID)
        {
            DataTable dTab = new DataTable("GetCcBcc");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("NotificationType_GetCC_BCC", Conn);

                Cmd.Parameters.AddWithValue("@NotificationTypeID", NotificationTypeID);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region [Add Notification Recipient CcBcc]
        public Int32 NotfRecp_Insert(NotificationRecipientCcBcc objNR)
        {
            Nullable<Int32> np = null;
            Int32 NotificationRecipientCcBccID = 0;
            METAOPTION.DALDataContext.Factory.DB.NotificationRecipientCcBcc_Insert(
                    objNR.NotificationRecipientCcBccID
                  , objNR.NotificationTypeID
                //, objNR.EntityTypeID
                //, objNR.EntityID
                  , objNR.EmployeeTypeID
                  , objNR.EmployeeID
                  , objNR.EmailCC
                  , objNR.EmailBCC
                  , objNR.SMS
                  , objNR.IsActive
                  , objNR.OrgID);

            if (np.HasValue)
                NotificationRecipientCcBccID = np.Value;
            return NotificationRecipientCcBccID;
        }
        #endregion

        #region[Update CC,BCC, SMS in NotificationRecipientCcBcc]
        public int UpdateNotificationRecipient_CC(Int32 NotfRecptID, Boolean IsCC)
        {
            return METAOPTION.DALDataContext.Factory.DB.NotfRecpt_SetCC(NotfRecptID, IsCC);
        }

        public int UpdateNotificationRecipient_BCC(Int32 NotfRecptID, Boolean IsBCC)
        {
            return METAOPTION.DALDataContext.Factory.DB.NotfRecpt_SetBCC(NotfRecptID, IsBCC);
        }

        public int UpdateNotificationRecipient_SMS(Int32 NotfRecptID, Boolean IsSMS)
        {
            return METAOPTION.DALDataContext.Factory.DB.NotfRecpt_SetSMS(NotfRecptID, IsSMS);
        }
        #endregion

        #region[Employee by EmployeeTypeId]
        public DataTable GetEmployeeByEmployeeType(Int32 EmployeeType)
        {
            DataTable dTab = new DataTable("Employee");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("Employee_ByEmployeeType", Conn);

                Cmd.Parameters.AddWithValue("@EmployeeTypeId", EmployeeType);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region[Delete notification preference CC/BCC/SMS]
        public int DeleteCcBccPref(Int32 CcBccPrefID)
        {
            return METAOPTION.DALDataContext.Factory.DB.NotificationRecipientCcBcc_Delete(CcBccPrefID);
        }
        #endregion

        #region[Search Notification]
        public DataTable SearchNotification(Int32 TableID, Int32 NotificationTypeID, Int32 EntityTypeID, long EntityID, String MailTo, long AddedBy, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, Int16 OrgID)
        {
            DataTable Result = new DataTable("Notifications");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("AllNotifications", Conn);
                Cmd.Parameters.AddWithValue("@TableID", TableID);
                Cmd.Parameters.AddWithValue("@NotificationTypeID", NotificationTypeID);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@MailTo", MailTo);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@SortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                Result.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return Result;
        }
        #endregion

        #region[Search Notification Count]
        public Int32 SearchNotificationCount(Int32 TableID, Int32 NotificationTypeID, Int32 EntityTypeID, long EntityID, String MailTo, long AddedBy, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, Int16 OrgID)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("AllNotifications_Count", Conn);
                Cmd.Parameters.AddWithValue("@TableID", TableID);
                Cmd.Parameters.AddWithValue("@NotificationTypeID", NotificationTypeID);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@MailTo", MailTo);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@SortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }
        #endregion

        #region[Notification Preference AddedBy]
        public List<NotificationPreference_AddedByResult> GetNotfPrefAddedBy(Int16 OrgID)
        {
            DALDataContext objDal = new DALDataContext();
            List<NotificationPreference_AddedByResult> result = objDal.NotificationPreference_AddedBy(OrgID).ToList<NotificationPreference_AddedByResult>();
            return result;
        }
        #endregion

        #region[Notification list sort options]
        public DataTable NotificationList_SortOptions(String strQuery, String Sort1, String Sort2)
        {
            DataTable dTab = new DataTable("NotfSort");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = Conn.CreateCommand();
                Cmd.CommandText = strQuery;
                Cmd.CommandType = CommandType.Text;
                SqlDataReader dReader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);

                dTab.Load(dReader);
            }
            return dTab;
        }
        #endregion

        #region[Notification Entity by Entity Type Id]
        public DataTable NotificationEntitiesByEntityType(Int32 EntityTypeId,Int16 OrgID)
        {
            DataTable dTab = new DataTable("NotificationEntity");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("EntitiesByEntityType_Notification", Conn);

                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeId);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region[Is Notification already set]
        public bool UserNotificationAlreadySet(Int32 EntityTypeID, long EntityID, Int32 NotificationTypeID)
        {
            int? count = METAOPTION.DALDataContext.Factory.DB.UserNotification_AlreadySet(EntityTypeID, EntityID, NotificationTypeID).Single().Total;
            if (count > 0)
                return false;
            else
                return true;

        }
        #endregion
    }
}
