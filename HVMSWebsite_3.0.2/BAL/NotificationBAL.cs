using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace METAOPTION
{
    public class NotificationBAL
    {
        NotificationDAL dal = new NotificationDAL();

        #region[Get all entity type for Notification]
        public List<EntityType> GetAllEntityType_Notification()
        {
            return dal.GetAllEntityType_Notification();
        }
        #endregion

        #region[Entities by Entity Type Id]
        public DataTable GetEntitiesByEntityType(int EntityTypeId, short OrgID)
        {
            return dal.GetEntitiesByEntityType(EntityTypeId, OrgID);
        }
        #endregion

        #region[Get all Table Notification]
        public List<TableMaster> GetAllTables_Notification()
        {
            return dal.GetAllTables_Notification();
        }
        #endregion

        #region[Get NotificationType by TableID]
        public List<NotificationType> GetNotificationTypeByTableID(Int32 TableID)
        {
            return dal.GetNotificationTypeByTableID(TableID);
        }

        //public DataTable GetNotificationTypeForUser(Int32 TableID, Int32 EntityTypeID, long EntityID)
        //{
        //    return dal.GetNotificationTypeForUser(TableID, EntityTypeID, EntityID);
        //}
        #endregion

        #region [Add Notification Preference]
        public Int32 NotificationPreference_Insert(NotificationPreference objNotPref)
        {
            return dal.NotificationPreference_Insert(objNotPref);
        }
        #endregion

        #region[Get Columns To Notify]
        public List<TableColumnMaster> GetColumnsToNotify(Int32 TableID)
        {
            return dal.GetColumnsToNotify(TableID);
        }
        #endregion

        #region[Get columns already notified to user]
        public List<TableColumnMaster> GetAlreadyNotifiedColumns(Int32 TableID, Int32 EntityTypeID, long EntityID)
        {
            return dal.GetAlreadyNotifiedColumns(TableID, EntityTypeID, EntityID);
        }
        #endregion

        #region[Fetch User's Notifications]
        public DataTable FetchUserNotification(Int32 EntityTypeID, long EntityID)
        {
            return dal.FetchUserNotification(EntityTypeID, EntityID);
        }
        #endregion

        #region[Update notify via Email and SMS]
        public int UpdateNotifyViaEmail(Int32 PreferenceID, Boolean NotifyViaEmail, long ModifiedBy)
        {
            return dal.UpdateNotifyViaEmail(PreferenceID, NotifyViaEmail, ModifiedBy);
        }

        public int UpdateNotifyViaSMS(Int32 PreferenceID, Boolean NotifyViaSMS, long ModifiedBy)
        {
            return dal.UpdateNotifyViaSMS(PreferenceID, NotifyViaSMS, ModifiedBy);
        }
        #endregion

        #region[Delete notification preference]
        public int DeleteNotificationPreference(Int32 PreferenceID, long DeletedBy)
        {
            return dal.DeleteNotificationPreference(PreferenceID, DeletedBy);
        }
        #endregion

        #region[Get All Notification Type]
        public List<NotificationType> GetAllNotificationType()
        {
            return dal.GetAllNotificationType();
        }
        #endregion

        #region[Get All Employee Type]
        public List<EmployeeType> GetAllEmployeeType()
        {
            return dal.GetAllEmployeeType();
        }
        #endregion

        #region[Search Employee]
        public DataTable SearchEmployee(Int32 EmployeeTypeId, String Email, String CellPhone, String FirstName, String LastName, Int32 NotificationTypeID, Int16 OrgID)
        {
            return dal.SearchEmployee(EmployeeTypeId, Email, CellPhone, FirstName, LastName, NotificationTypeID, OrgID);
        }
        #endregion

        #region[Fetch CC/BCC preference already set for a NotificationType]
        public DataTable GetCcBcc_NotificationType(Int32 NotificationTypeID, Int16 OrgID)
        {
            return dal.GetCcBcc_NotificationType(NotificationTypeID, OrgID);
        }
        #endregion

        #region [Add Notification Recipient CcBcc]
        public Int32 NotfRecp_Insert(NotificationRecipientCcBcc objNR)
        {
            return dal.NotfRecp_Insert(objNR);
        }
        #endregion

        #region[Update CC,BCC, SMS in NotificationRecipientCcBcc]
        public int UpdateNotificationRecipient_CC(Int32 NotfRecptID, Boolean IsCC)
        {
            return dal.UpdateNotificationRecipient_CC(NotfRecptID, IsCC);
        }

        public int UpdateNotificationRecipient_BCC(Int32 NotfRecptID, Boolean IsBCC)
        {
            return dal.UpdateNotificationRecipient_BCC(NotfRecptID, IsBCC);
        }

        public int UpdateNotificationRecipient_SMS(Int32 NotfRecptID, Boolean IsSMS)
        {
            return dal.UpdateNotificationRecipient_SMS(NotfRecptID, IsSMS);
        }
        #endregion

        #region[Employee by EmployeeTypeId]
        public DataTable GetEmployeeByEmployeeType(Int32 EmployeeType)
        {
            return dal.GetEmployeeByEmployeeType(EmployeeType);
        }
        #endregion

        #region[Delete notification preference CC/BCC/SMS]
        public int DeleteCcBccPref(Int32 CcBccPrefID)
        {
            return dal.DeleteCcBccPref(CcBccPrefID);
        }
        #endregion

        #region[Search Notification]
        public DataTable SearchNotification(Int32 TableID, Int32 NotificationTypeID, Int32 EntityTypeID, long EntityID, String MailTo, long AddedBy, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, Int16 OrgID)
        {
            return dal.SearchNotification(TableID, NotificationTypeID, EntityTypeID, EntityID, MailTo, AddedBy, StartRowIndex, MaximumRows, SortExpression, OrgID);
        }
        #endregion

        #region[Search Notification Count]
        public Int32 SearchNotificationCount(Int32 TableID, Int32 NotificationTypeID, Int32 EntityTypeID, long EntityID, String MailTo, long AddedBy, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, Int16 OrgID)
        {
            return dal.SearchNotificationCount(TableID, NotificationTypeID, EntityTypeID, EntityID, MailTo, AddedBy, StartRowIndex, MaximumRows, SortExpression, OrgID);
        }
        #endregion

        #region[Notification Preference AddedBy]
        public List<NotificationPreference_AddedByResult> GetNotfPrefAddedBy(Int16 OrgID)
        {
            return dal.GetNotfPrefAddedBy(OrgID);
        }
        #endregion

        #region[Notification list sort options]
        public DataTable NotificationList_SortOptions(String strQuery, String Sort1, String Sort2)
        {
            return dal.NotificationList_SortOptions(strQuery, Sort1, Sort2);
        }
        #endregion

        #region[Notification Entity by Entity Type Id]
        public DataTable NotificationEntitiesByEntityType(Int32 EntityTypeId, Int16 OrgID)
        {
            return dal.NotificationEntitiesByEntityType(EntityTypeId, OrgID);
        }
        #endregion

        #region[Is Notification already set]
        public bool UserNotificationAlreadySet(Int32 EntityTypeID, long EntityID, Int32 NotificationTypeID)
        {
            return dal.UserNotificationAlreadySet(EntityTypeID, EntityID, NotificationTypeID);
        }
        #endregion
    }
}
