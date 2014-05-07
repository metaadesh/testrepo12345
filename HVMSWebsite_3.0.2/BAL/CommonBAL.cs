using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Collections;
using METAOPTION.DAL;
using System.Web;
using System.Reflection;
namespace METAOPTION.BAL
{
    public class CommonBAL
    {
        static Double ShortCacheExpiry = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["ShortCacheExpiry"]);
        static Double LongCacheExpiry = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["LongCacheExpiry"]);

        #region[ Get Page Permission by User Id & Page Name ]
        /// <summary>
        /// This method return Page permission against employeeid and page name provided
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static List<String> GetPagePermission(long? employeeId, string page)
        {
            #region[OldCode]

            //CommonDAL objCommonDAL = new CommonDAL();
            //List<String> temp = new List<String>();
            //temp= objCommonDAL.GetPagePermission(employeeId, page);

            #endregion

            List<String> result = new List<String>();
            var permissions = (from p in GetAllPagePermissions()
                               where p.SecurityUserID == employeeId && p.RightName.StartsWith(page)
                               select p);
            foreach (GetAllPagePermissionResult p in permissions)
            {
                result.Add(p.RightName.Remove(0, p.RightName.IndexOf('.') + 1));
            }
            return result;
        }

        // Added by Adesh On 16 Jan,2013
        public static List<String> GetPagePermission(DataTable dtPermissions)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetPagePermission(dtPermissions);
        }

        #endregion

        private static List<GetAllPagePermissionResult> GetAllPagePermissions()
        {
            CommonDAL objCommonDAL = new CommonDAL();

            if (HttpContext.Current.Cache[CacheEnum.PagePermissions] == null)
            {
                List<GetAllPagePermissionResult> result = objCommonDAL.GetAllPagePermission().ToList<GetAllPagePermissionResult>();
                HttpContext.Current.Cache.Insert(CacheEnum.PagePermissions, result, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(LongCacheExpiry));
                return result;
            }
            else
                return (List<GetAllPagePermissionResult>)HttpContext.Current.Cache[CacheEnum.PagePermissions];

        }

        #region [Added By Vipin 17 Jan 2013 Get Expense List with many Filter Criteria]
        public DataTable GetExpenseList_ByBuyer(Int64 EntityID, Int32 ParentEntityID, Int32 EntityTypeId, Int32 ExpenseTypeID, String VIN, DateTime FromDate,
            DateTime ToDate, Int32 AddedBy, String CheckNo, Int32 StartRowIndex, Int32 MaximumRows)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetExpenseList_ByBuyer(EntityID, ParentEntityID, EntityTypeId, ExpenseTypeID, VIN, FromDate, ToDate, AddedBy, CheckNo, StartRowIndex, MaximumRows);

        }

        public Int32 GetExpenseList_ByBuyerCount(Int64 EntityID, Int32 ParentEntityID, Int32 EntityTypeId, Int32 ExpenseTypeID, String VIN, DateTime FromDate,
            DateTime ToDate, Int32 AddedBy, String CheckNo, Int32 StartRowIndex, Int32 MaximumRows)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return Convert.ToInt32(objCommonDAL.GetExpenseList_ByBuyerCount(EntityID, ParentEntityID, EntityTypeId, ExpenseTypeID, VIN, FromDate, ToDate, AddedBy, CheckNo, StartRowIndex, MaximumRows));

        }
        #endregion

        #region[VIN scan log added by users for Child Buyer]
        public List<Mobile_VinScanLog_user_Ver211Result> GetVINScanLogUsers_AddedByForChildBuyer(Int32 EntityID, Int32 ParentBuyerID)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetVINScanLogUsers_AddedByForChildBuyer(EntityID, ParentBuyerID);
        }

        public List<SecurityUser> GetVINScanLogUsers_AddedByForChildBuyer(Int32 SecurityUserID, Int32 ParentEntityID, Int32 EntityTypeID, Int16 OrgID)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetVINScanLogUsers_AddedByForChildBuyer(SecurityUserID, ParentEntityID, EntityTypeID, OrgID);
        }
        #endregion

        #region [Add Contact Details of Entity(Dealer/Customer,Vendor,Buyer etc)]
        /// <summary>
        /// add contact details of entity(Dealer/Customer,Buyer,Vendor etc)
        /// </summary>
        /// <param name="objContact"></param>
        /// <returns></returns>
        public void AddContactDetails(Contact objContact)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            objCommonDAL.AddContactDetails(objContact);
        }
        #endregion

        #region[Get contact details of Entity]
        public IQueryable GetEntityContactDetails(long EntityId, long EntityTypeId)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetEntityContactDetails(EntityId, EntityTypeId);
        }
        #endregion

        #region[Get contact Type]
        public static List<ContactType_ver211Result> GetContactType()
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetContactType();
        }
        #endregion

        #region[Get Job Title]
        public static List<JobTile_ver211Result> GetJobTitle()
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetJobTitle();
        }
        #endregion

        #region[Get Contact detail by SecurityUserID and EntityTypeID]
        public static List<GetEntityContactDetails_ver211Result> GetContactDetail_BySecurityUserID(Int32 SecurityUserID, Int32 EntityTypeID, Int32 JobTitle, String UserName, Int32 ContactType, String CellPhone)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetContactDetail_BySecurityUserID(SecurityUserID, EntityTypeID, JobTitle, UserName, ContactType, CellPhone);

        }
        #endregion

        #region[Get Contact Details By ContactId]
        public IQueryable GetContactDetailsByContactId(long ContactId)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetContactDetailsByContactId(ContactId);
        }
        #endregion

        #region[Update Contact Details of Entity]
        public int UpdateContactDetails(Contact objContact)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.UpdateContactDetails(objContact);
        }
        #endregion

        #region [Delete Contact Details]
        public void DeleteDealerContact(long ContactId)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            objCommonDAL.DeleteDealerContact(ContactId);
        }
        #endregion

        #region[Get Commission Paid of entity]
        //public IEnumerable _DEL_GetCommisionPaid(long entityId)
        //{
        //   CommonDAL objCommonDAL = new CommonDAL();
        //   //return objCommonDAL.GetCommisionPaid(entityId);
        //}
        #endregion

        #region[Get Payments for an entity]
        // Created by Naushad on August 20, 2009
        public IEnumerable GetPayments(long entityId, int entityTypeId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetPayments(entityId, entityTypeId, StartRowIndex, MaximumRows);
        }
        #endregion

        #region[Count Payments by entity]
        // Created by Naushad on August 21, 2009
        public Int32? GetPaymentsCount(long entityId, int entityTypeId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetPaymentsCount(entityId, entityTypeId, StartRowIndex, MaximumRows);
        }
        #endregion

        #region[Get Expenses for an entity]
        // Created by Naushad on August 22, 2009
        public IEnumerable GetExpenseList(long entityId, int entityTypeId, int expenseTypeId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetExpenseList(entityId, entityTypeId, expenseTypeId, StartRowIndex, MaximumRows);
        }
        #endregion

        #region[Count Expenses for an entity]
        // Created by Naushad on August 22, 2009
        public Int32? GetExpenseListCount(long entityId, int entityTypeId, int expenseTypeId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetExpenseListCount(entityId, entityTypeId, expenseTypeId, StartRowIndex, MaximumRows);
        }
        #endregion

        #region[VIN scan log]
        public List<Mobile_VINScanLog_FetchResult> GetVINScanLog(String VIN, long AddedBy, Int32 StartRowIndex, Int32 MaximumRows)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetVINScanLog(VIN, AddedBy, StartRowIndex, MaximumRows);
        }
        public List<Mobile_VINScanLog_Fetch_Ver211Result> GetVINScanLogVer211(String VIN, long AddedBy, Int32 EntityTypeID, Int32 SecurityUserID, Int32 StartRowIndex, Int32 MaximumRows)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetVINScanLogVer211(VIN, AddedBy, EntityTypeID, SecurityUserID, StartRowIndex, MaximumRows);
        }
        #endregion
        #region[VIN scan log for child buyer]
        public DataTable GetVINScanLogForChildBuyerVer211(String VIN, long AddedBy, Int32 EntityTypeID, Int32 EntityID, Int32 ParentEntityID, Int32 StartRowIndex, Int32 MaximumRows, Int16 OrgID)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetVINScanLogForChildBuyerVer211(VIN, AddedBy, EntityTypeID, EntityID, ParentEntityID, StartRowIndex, MaximumRows, OrgID);
        }

        public Int32 GetVINScanLogVer211CountForChildBuyer(String VIN, long AddedBy, Int32 EntityTypeID, Int32 EntityID, Int32 ParentEntityID, Int32 StartRowIndex, Int32 MaximumRows, Int16 OrgID)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetVINScanLogVer211CountForChildBuyer(VIN, AddedBy, EntityTypeID, EntityID, ParentEntityID, StartRowIndex, MaximumRows, OrgID);
        }
        #endregion


        #region[VIN scan log count]
        public Int32? GetVINScanLogCount(String VIN, long AddedBy, Int32 StartRowIndex, Int32 MaximumRows)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetVINScanLogCount(VIN, AddedBy, StartRowIndex, MaximumRows);
        }

        public Int32? GetVINScanLogVer211Count(String VIN, long AddedBy, Int32 EntityTypeID, Int32 SecurityUserID, Int32 StartRowIndex, Int32 MaximumRows)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetVINScanLogVer211Count(VIN, AddedBy, EntityTypeID, SecurityUserID, StartRowIndex, MaximumRows);
        }
        #endregion

        #region[VIN scan log added by users]
        public List<SecurityUser> GetVINScanLogUsers()
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetVINScanLogUsers();
        }
        #endregion

        #region[VIN scan log added by users]
        public List<SecurityUser> GetVINScanLogUsers_AddedBy(Int32 AddedBy)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetVINScanLogUsers_AddedBy(AddedBy);
        }
        #endregion

        #region[Generic Images]
        public System.Collections.ArrayList GenericImages(long GenericImageID)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GenericImages(GenericImageID);
        }
        #endregion

        #region[Generic Images ]
        public List<Mobile_GenericImages_FetchResult> GetAllGenericImages(String VIN, long AddedBy)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetAllGenericImages(VIN, AddedBy);
        }
        public DataTable GetAllGenericImages_Ver211(String VIN, long AddedBy, Int32 EntityTypeID, Int32 EntityID, Int32 ParentEntityID, Int32 StartRowIndex, Int32 MaximumRows, Int16 OrgID)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetAllGenericImages_Ver211(VIN, AddedBy, EntityTypeID, EntityID, ParentEntityID, StartRowIndex, MaximumRows, OrgID);
        }

        public Int32 GetAllGenericImagesCount_Ver211(String VIN, long AddedBy, Int32 EntityTypeID, Int32 EntityID, Int32 ParentEntityID, Int32 StartRowIndex, Int32 MaximumRows, Int16 OrgID)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetAllGenericImagesCount_Ver211(VIN, AddedBy, EntityTypeID, EntityID, ParentEntityID, StartRowIndex, MaximumRows, OrgID);
        }
        #endregion

        #region[Generic Images added by users]
        public List<SecurityUser> GetGenericImagesUsers()
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetGenericImagesUsers();
        }
        #endregion

        #region[Generic Images added by users for Vendor]
        public List<SecurityUser> GetGenericImagesUsers_AddedBy(Int32 AddedBy)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetGenericImagesUsers_AddedBy(AddedBy);
        }
        #endregion

        #region[Generic Images added by users ]
        public List<SecurityUser> GetGenericImagesUsers_AddedBy(Int32 SecurityUserID, Int32 ParentEntityID, Int32 EntityTypeID,Int16 OrgID)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetGenericImagesUsers_AddedBy(SecurityUserID, ParentEntityID, EntityTypeID, OrgID);
        }
        #endregion

        #region[All Images]
        public System.Collections.ArrayList AllImages(long ImageID, String VIN, Int32 Type, Int32 Period)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.AllImages(ImageID, VIN, Type, Period);
        }
        public DataTable GenericImageDetails(String VIN)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GenericImageDetails(VIN);
        }
        #endregion

        #region [Get PreInv & PreExp Image Count]
        public static int GetPreInv_PreExpCount(String VIN, Int32 Type, Int32 Period)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.GetPreInv_PreExpCount(VIN, Type, Period);
        }
        #endregion

        #region [Insert Settings in database]
        public long Settings_Insert(Setting objSetting)
        {
            CommonDAL objCommonDAL = new CommonDAL();
            return objCommonDAL.Settings_Insert(objSetting);
        }
        #endregion

        #region[Search Export History]
        public DataTable SearchExportHistory(String FileName, DateTime DateFrom, DateTime DateTo, long AddedBy, String OrderBy, Int16 OrgID)
        {
            CommonDAL dal = new CommonDAL();
            return dal.SearchExportHistory(FileName, DateFrom, DateTo, AddedBy, OrderBy, OrgID);
        }
        #endregion

        #region[ExportHistory Added By]
        public IEnumerable GetExportedBy(Int16 OrgID)
        {
            CommonDAL dal = new CommonDAL();
            return dal.GetExportedBy(OrgID);
        }
        #endregion

        #region[Search Audit]
        public DataTable SearchAudit(Int32 TableID, long ColumnID, long RowID, DateTime DateFrom, DateTime DateTo, long ModifiedBy, String Source, String UpdatedFrom, Int32 StartRowIndex, Int32 MaximumRows, String OrderBy, String OrgID)
        {
            CommonDAL dal = new CommonDAL();
            return dal.SearchAudit(TableID, ColumnID, RowID, DateFrom, DateTo, ModifiedBy, Source, UpdatedFrom, StartRowIndex, MaximumRows, OrderBy, Convert.ToInt16(OrgID));
        }

        public Int32 SearchAuditCount(Int32 TableID, long ColumnID, long RowID, DateTime DateFrom, DateTime DateTo, long ModifiedBy, String Source, String UpdatedFrom, Int32 StartRowIndex, Int32 MaximumRows, String OrderBy, String OrgID)
        {
            CommonDAL dal = new CommonDAL();
            return dal.SearchAuditCount(TableID, ColumnID, RowID, DateFrom, DateTo, ModifiedBy, Source, UpdatedFrom, StartRowIndex, MaximumRows, OrderBy, Convert.ToInt16(OrgID));
        }
        #endregion

        #region[Audit Added By]
        public IEnumerable GetAuditBy(Int16 OrgID)
        {
            CommonDAL dal = new CommonDAL();
            return dal.GetAuditBy(OrgID);
        }
        #endregion

        #region[Audit Tables]
        public IEnumerable GetAuditTables()
        {
            CommonDAL dal = new CommonDAL();
            return dal.GetAuditTables();
        }
        #endregion

        #region[Audit Columns]
        public IEnumerable GetAuditColumns(Int32 TableID)
        {
            CommonDAL dal = new CommonDAL();
            return dal.GetAuditColumns(TableID);
        }
        #endregion
    }
}
