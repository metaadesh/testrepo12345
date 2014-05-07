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
    public class CommonDAL
    {
        #region[ Get Page Permisson by page name and user Id ]
        /// <summary>
        ///  Get Page Permisson by page name and user Id 
        /// </summary>
        /// <param name="employeeId">logged in user Id </param>
        /// <param name="page"> page name </param>
        /// <returns></returns>
        public List<String> GetPagePermission(long? employeeId, string page)
        {
            DALDataContext objDAL = new DALDataContext();
            List<String> result = new List<String>();
            var list = objDAL.GetPagePermission(employeeId, page).ToList();

            foreach (GetPagePermissionResult item in list)
            {
                result.Add(item.PageSection + "." + item.Right);
            }
            return result;
        }
        // Added by Adesh On 16 Jan,2013
        public List<String> GetPagePermission(DataTable dtPermissions)
        {
            List<String> result = new List<String>();
            foreach (DataRow dr in dtPermissions.Rows)
            {
                result.Add(Convert.ToString(dr["PageSection"]) + "." + Convert.ToString(dr["Right"]));
            }
            return result;
        }

        public List<GetAllPagePermissionResult> GetAllPagePermission()
        {
            DALDataContext objDAL = new DALDataContext();

            return objDAL.GetAllPagePermission().ToList<GetAllPagePermissionResult>();

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
            DALDataContext objDAL = new DALDataContext();
            objDAL.ContactDetailsInsert(objContact.EntityId, objContact.EntityTypeId, objContact.ContactTypeId,
                objContact.JobTitleId, objContact.FirstName, objContact.MiddleName, objContact.LastName,
                objContact.OfficePhone, objContact.CellPhone, objContact.Email, objContact.AddedBy);

        }
        #endregion

        #region[Get contact details of Entity]
        public IQueryable GetEntityContactDetails(long EntityId, long EntityTypeId)
        {
            DALDataContext objDAL = new DALDataContext();
            IQueryable result = objDAL.GetEntityContactDetails(EntityId, EntityTypeId).AsQueryable();
            return result;
        }
        #endregion

        #region[Get contact type]
        public List<ContactType_ver211Result> GetContactType()
        {
            DALDataContext objDAL = new DALDataContext();
            List<ContactType_ver211Result> result = objDAL.ContactType_ver211().ToList<ContactType_ver211Result>();
            return result;
        }
        #endregion

        #region[Get Job Title]
        public List<JobTile_ver211Result> GetJobTitle()
        {
            DALDataContext objDAL = new DALDataContext();
            List<JobTile_ver211Result> result = objDAL.JobTile_ver211().ToList<JobTile_ver211Result>();
            return result;
        }
        #endregion

        #region[Get Contact detail by SecurityUserID and EntityTypeID]
        public List<GetEntityContactDetails_ver211Result> GetContactDetail_BySecurityUserID(Int32 SecurityUserID, Int32 EntityTypeID, Int32 JobTitle, String UserName, Int32 ContactType, String CellPhone)
        {
            DALDataContext objDAL = new DALDataContext();
            List<GetEntityContactDetails_ver211Result> result = objDAL.GetEntityContactDetails_ver211(SecurityUserID, EntityTypeID, JobTitle, UserName, ContactType, CellPhone).ToList<GetEntityContactDetails_ver211Result>();
            return result;
        }
        #endregion

        #region[Get Contact Details By ContactId]
        /// <summary>
        /// Get Contact Details By ContactId
        /// </summary>
        /// <param name="ContactId">Contact Id</param>
        /// <returns></returns>
        public IQueryable GetContactDetailsByContactId(long ContactId)
        {
            DALDataContext objDAL = new DALDataContext();
            return objDAL.GetContactByContactId(ContactId).AsQueryable();
        }
        #endregion

        #region[Update Contact Details of Entity (Dealer,vendor etc)]
        /// <summary>
        /// Update Contact Details of Entity (Dealer,vendor etc)
        /// </summary>
        /// <param name="objContact">Contact</param>
        /// <returns></returns>
        public int UpdateContactDetails(Contact objContact)
        {
            DALDataContext objDAL = new DALDataContext();
            int result = objDAL.ContactDetailsUpdate(objContact.JobTitleId
                , objContact.ContactTypeId
                , objContact.FirstName
                , objContact.MiddleName
                , objContact.LastName
                , objContact.OfficePhone
                , objContact.CellPhone
                , objContact.Email
                , objContact.ContactId
                , objContact.ModifiedBy);

            return result;
        }
        #endregion

        #region [Delete Contact Details]
        /// <summary>
        /// Delete Contact Details
        /// </summary>
        /// <param name="ContactId">Contact Id</param>
        public void DeleteDealerContact(long ContactId)
        {
            DALDataContext objDAL = new DALDataContext();
            var result = (from p in objDAL.Contacts
                          where p.ContactId == ContactId
                          select p).AsQueryable();
            objDAL.Contacts.DeleteOnSubmit(result.Single());
            objDAL.SubmitChanges();
        }
        #endregion

        #region [Get Payments Made for an Entity]
        /// <summary>
        /// Get Payments for an entity
        /// Created by Naushad on August 20, 2009
        /// </summary>
        /// <param name="entityId">Entity Id</param>
        /// <returns></returns>
        public IEnumerable GetPayments(long entityId, int entityTypeId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            DALDataContext objDAL = new DALDataContext();
            return objDAL.GetPayments(entityId, entityTypeId, StartRowIndex, MaximumRows).AsEnumerable();
        }
        #endregion

        #region[Retunrs Payments Count for an Entity]
        public Int32? GetPaymentsCount(long entityId, int entityTypeId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            GetPaymentsCountResult result = METAOPTION.DALDataContext.Factory.DB.GetPaymentsCount(entityId, entityTypeId, StartRowIndex, MaximumRows).Single();
            return result.TotalRecord;
        }
        #endregion


        #region [Get Expenses for an Entity]
        /// <summary>
        /// Get Expenses for an entity
        /// Created by Naushad on August 22, 2009
        /// </summary>
        /// <param name="entityId">EntityID</param>
        /// <param name="entityTypeId">EntityTypeID</param>
        /// <param name="MaximumRows">MaximumRows to be returned</param>
        /// <param name="StartRowIndex">StartRowIndex</param>
        /// <returns></returns>
        public IEnumerable GetExpenseList(long entityId, int entityTypeId, int expenseTypeId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            DALDataContext objDAL = new DALDataContext();
            return objDAL.GetExpenseList(entityId, entityTypeId, expenseTypeId, StartRowIndex, MaximumRows).AsEnumerable();
        }
        #endregion

        #region [Added By Vipin 17 Jan 2013 Get Expense List with many Filter Criteria]
        public DataTable GetExpenseList_ByBuyer(Int64 EntityID, Int32 ParentEntityID, Int32 EntityTypeId, Int32 ExpenseTypeID, String VIN, DateTime FromDate,
            DateTime ToDate, Int32 AddedBy, String CheckNo, Int32 StartRowIndex, Int32 MaximumRows)
        {
            DataTable Dt = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("GetExpenseList_ver211", Conn);
                Cmd.Parameters.AddWithValue("@EntityId", EntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@EntityTypeId", EntityTypeId);
                Cmd.Parameters.AddWithValue("@ExpenseTypeID", ExpenseTypeID);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@FromDate", FromDate);
                Cmd.Parameters.AddWithValue("@ToDate", ToDate);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@CheckNo", CheckNo);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                Dt.Load(reader);
                reader.Close();
                objDal.Dispose();

            }
            return Dt;
        }

        public Int32 GetExpenseList_ByBuyerCount(Int64 EntityID, Int32 ParentEntityID, Int32 EntityTypeId, Int32 ExpenseTypeID, String VIN, DateTime FromDate,
            DateTime ToDate, Int32 AddedBy, String CheckNo, Int32 StartRowIndex, Int32 MaximumRows)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("GetExpenseListCount_ver211", Conn);
                Cmd.Parameters.AddWithValue("@EntityId", EntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@EntityTypeId", EntityTypeId);
                Cmd.Parameters.AddWithValue("@ExpenseTypeID", ExpenseTypeID);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@FromDate", FromDate);
                Cmd.Parameters.AddWithValue("@ToDate", ToDate);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@CheckNo", CheckNo);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }
        #endregion

        #region[Retunrs Expense Count for an Entity]
        /// <summary>
        /// Get Expenses Count for an entity
        /// Created by Naushad on August 22, 2009
        /// </summary>
        /// <param name="entityId">EntityID</param>
        /// <param name="entityTypeId">EntityTypeID</param>
        /// <param name="MaximumRows">MaximumRows to be returned</param>
        /// <param name="StartRowIndex">StartRowIndex</param>
        /// <returns></returns>
        public Int32? GetExpenseListCount(long entityId, int entityTypeId, int expenseTypeId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            GetExpenseListCountResult result = METAOPTION.DALDataContext.Factory.DB.GetExpenseListCount(entityId, entityTypeId, expenseTypeId, StartRowIndex, MaximumRows).Single();
            return result.TotalRecord;
        }
        #endregion

        #region[VIN scan log]
        public List<Mobile_VINScanLog_FetchResult> GetVINScanLog(String VIN, long AddedBy, Int32 StartRowIndex, Int32 MaximumRows)
        {
            DALDataContext objDal = new DALDataContext();
            return objDal.Mobile_VINScanLog_Fetch(VIN, AddedBy, StartRowIndex, MaximumRows).ToList<Mobile_VINScanLog_FetchResult>();
        }
        public List<Mobile_VINScanLog_Fetch_Ver211Result> GetVINScanLogVer211(String VIN, long AddedBy, Int32 EntityTypeID, Int32 SecurityUserID, Int32 StartRowIndex, Int32 MaximumRows)
        {
            DALDataContext objDal = new DALDataContext();
            return objDal.Mobile_VINScanLog_Fetch_Ver211(VIN, AddedBy, EntityTypeID, SecurityUserID, StartRowIndex, MaximumRows).ToList<Mobile_VINScanLog_Fetch_Ver211Result>();
        }
        #endregion

        #region[VIN scan log for Child Buyer]
        public DataTable GetVINScanLogForChildBuyerVer211(String VIN, long AddedBy, Int32 EntityTypeID, Int32 EntityID, Int32 ParentEntityID, Int32 StartRowIndex, Int32 MaximumRows, Int16 OrgID)
        {
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_VINScanLog_FetchByChildBuyer_Ver2111", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@EntityTypeId", EntityTypeID);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public Int32 GetVINScanLogVer211CountForChildBuyer(String VIN, long AddedBy, Int32 EntityTypeID, Int32 EntityID, Int32 ParentEntityID, Int32 StartRowIndex, Int32 MaximumRows, Int16 OrgID)
        {
            Int32 Result = 0;

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_VINScanLog_FetchbyChildBuyer_Count_Ver2111", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@EntityTypeId", EntityTypeID);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }
        #endregion

        #region[VIN scan log count]
        public Int32? GetVINScanLogCount(String VIN, long AddedBy, Int32 StartRowIndex, Int32 MaximumRows)
        {
            Mobile_VINScanLog_Fetch_CountResult result = METAOPTION.DALDataContext.Factory.DB.Mobile_VINScanLog_Fetch_Count(VIN, AddedBy, StartRowIndex, MaximumRows).Single();
            return result.Total;
        }

        public Int32? GetVINScanLogVer211Count(String VIN, long AddedBy, Int32 EntityTypeID, Int32 SecurityUserID, Int32 StartRowIndex, Int32 MaximumRows)
        {
            Mobile_VINScanLog_Fetch_Count_Ver211Result result = METAOPTION.DALDataContext.Factory.DB.Mobile_VINScanLog_Fetch_Count_Ver211(VIN, AddedBy, EntityTypeID, SecurityUserID, StartRowIndex, MaximumRows).Single();
            return result.Total;
        }
        #endregion

        #region[VIN scan log added by users]
        public List<SecurityUser> GetVINScanLogUsers()
        {
            DALDataContext objDal = new DALDataContext();
            List<SecurityUser> result = (from p in objDal.SecurityUsers
                                         from V in objDal.Mobile_VinScanLogs
                                         where p.SecurityUserID == V.AddedBy
                                         select p).Distinct().OrderBy(p => p.DisplayName).ToList<SecurityUser>();
            return result;
        }
        #endregion

        #region[VIN scan log added by users for vendor]
        public List<SecurityUser> GetVINScanLogUsers_AddedBy(Int32 AddedBy)
        {
            DALDataContext objDal = new DALDataContext();
            List<SecurityUser> result = (from p in objDal.SecurityUsers
                                         from V in objDal.Mobile_VinScanLogs
                                         where p.SecurityUserID == V.AddedBy && V.AddedBy == AddedBy
                                         select p).Distinct().OrderBy(p => p.DisplayName).ToList<SecurityUser>();
            return result;
        }
        #endregion

        #region[VIN scan log added by users for Child Buyer]
        public List<Mobile_VinScanLog_user_Ver211Result> GetVINScanLogUsers_AddedByForChildBuyer(Int32 EntityID, Int32 ParentBuyerID)
        {
            DALDataContext objDal = new DALDataContext();
            return objDal.Mobile_VinScanLog_user_Ver211(EntityID, ParentBuyerID).ToList<Mobile_VinScanLog_user_Ver211Result>();
        }

        public List<SecurityUser> GetVINScanLogUsers_AddedByForChildBuyer(Int32 SecurityUserID, Int32 ParentEntityID, Int32 EntityTypeID, Int16 OrgID)
        {
            List<SecurityUser> lst = new List<SecurityUser>();
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_VinScanLog_user_Ver2111", Conn);
                Cmd.Parameters.AddWithValue("@SecurityUserID", SecurityUserID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            foreach (DataRow dr in dTab.Rows)
            {
                SecurityUser SU = new SecurityUser();
                SU.DisplayName = dr["DisplayName"].ToString();
                SU.SecurityUserID = Convert.ToInt32(dr["SecurityUserID"]);
                lst.Add(SU);
            }
            return lst;
        }

        #endregion

        #region[Generic Images Path]
        public System.Collections.ArrayList GenericImages(long GenericImageID)
        {
            DataTable dTab = new DataTable();
            System.Collections.ArrayList array = new System.Collections.ArrayList();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_GenericImagesPath", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@GenericImageID", GenericImageID);

                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                array.Add(dTab);

                dTab = new DataTable();
                dTab.Load(reader);
                array.Add(dTab);

                reader.Close();
                objDal.Dispose();
            }
            return array;

        }
        #endregion

        #region[Generic Images]
        public List<Mobile_GenericImages_FetchResult> GetAllGenericImages(String VIN, long AddedBy)
        {
            DALDataContext objDal = new DALDataContext();
            return objDal.Mobile_GenericImages_Fetch(VIN, AddedBy).ToList<Mobile_GenericImages_FetchResult>();
        }

        public DataTable GetAllGenericImages_Ver211(String VIN, long AddedBy, Int32 EntityTypeID, Int32 EntityID, Int32 ParentEntityID, Int32 StartRowIndex, Int32 MaximumRows, Int16 OrgID)
        {
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_GenericImages_Fetch_Ver2111", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@EntityTypeId", EntityTypeID);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public Int32 GetAllGenericImagesCount_Ver211(String VIN, long AddedBy, Int32 EntityTypeID, Int32 EntityID, Int32 ParentEntityID, Int32 StartRowIndex, Int32 MaximumRows, Int16 OrgID)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_GenericImages_FetchCount_Ver2111", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@EntityTypeId", EntityTypeID);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }
        #endregion

        #region[Generic Images added by users]
        public List<SecurityUser> GetGenericImagesUsers()
        {
            DALDataContext objDal = new DALDataContext();
            List<SecurityUser> result = (from p in objDal.SecurityUsers
                                         from I in objDal.Mobile_GenericImages
                                         where p.SecurityUserID == I.AddedBy
                                         select p).Distinct().OrderBy(p => p.DisplayName).ToList<SecurityUser>();
            return result;
        }
        #endregion

        #region[Generic Images added by users for vendor]
        public List<SecurityUser> GetGenericImagesUsers_AddedBy(Int32 AddedBy)
        {
            DALDataContext objDal = new DALDataContext();
            List<SecurityUser> result = (from p in objDal.SecurityUsers
                                         from I in objDal.Mobile_GenericImages
                                         where p.SecurityUserID == I.AddedBy && I.AddedBy == AddedBy
                                         select p).Distinct().OrderBy(p => p.DisplayName).ToList<SecurityUser>();
            return result;
        }


        #endregion

        #region[Generic Images added by users ]
        public List<SecurityUser> GetGenericImagesUsers_AddedBy(Int32 SecurityUserID, Int32 ParentEntityID, Int32 EntityTypeID,Int16 OrgID)
        {
            List<SecurityUser> lst = new List<SecurityUser>();
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_GenericImages_addedByUsers_ver211", Conn);
                Cmd.Parameters.AddWithValue("@SecurityUserID", SecurityUserID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            foreach (DataRow dr in dTab.Rows)
            {
                SecurityUser SU = new SecurityUser();
                SU.DisplayName = dr["DisplayName"].ToString();
                SU.SecurityUserID = Convert.ToInt32(dr["SecurityUserID"]);
                lst.Add(SU);
            }
            return lst;
        }
        #endregion

        #region[All Images Path]
        public System.Collections.ArrayList AllImages(long ImageID, String VIN, Int32 Type, Int32 Period)
        {
            DataTable dTab = new DataTable();
            System.Collections.ArrayList array = new System.Collections.ArrayList();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_AllImages", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ImageID", ImageID);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@Type", Type);
                Cmd.Parameters.AddWithValue("@Period", Period);

                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                array.Add(dTab);

                dTab = new DataTable();
                dTab.Load(reader);
                array.Add(dTab);

                reader.Close();
                objDal.Dispose();
            }
            return array;

        }
        public DataTable GenericImageDetails(String VIN)
        {
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("GetGenericImageDetails", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@VIN", VIN);

                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;

        }
        #endregion

        #region [Get PreInv & PreExp Image Count]
        public int GetPreInv_PreExpCount(String VIN, Int32 Type, Int32 Period)
        {
            List<Mobile_PreInv_PreExp_ImageCountResult> Result = METAOPTION.DALDataContext.Factory.DB.Mobile_PreInv_PreExp_ImageCount(VIN, Type, Period).ToList();
            if (Result.Count > 0)
                return Result.First().Total.Value;
            else return 0;
        }
        #endregion

        #region [Insert Settings in database]
        public long Settings_Insert(Setting objSetting)
        {
            Nullable<long> setting = null;
            long SettingID = 0;
            METAOPTION.DALDataContext.Factory.DB.Settings_Insert(
                objSetting.SettingID
                , objSetting.SettingkeyID
                , objSetting.SettingkeyValue
                , objSetting.EntityID
                , objSetting.EntityTypeID
                , objSetting.SecurityUserID
                , objSetting.IsActive);

            if (setting.HasValue)
                SettingID = setting.Value;
            return SettingID;
        }
        #endregion

        #region[Search Export History]
        public DataTable SearchExportHistory(String FileName, DateTime DateFrom, DateTime DateTo, long AddedBy, String OrderBy, Int16 OrgID)
        {
            DataTable dTab = new DataTable("ExportHistory");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("ExportHistory_Search", Conn);
                Cmd.Parameters.AddWithValue("@FileName", FileName);
                Cmd.Parameters.AddWithValue("@DateAddedFrom", DateFrom);
                Cmd.Parameters.AddWithValue("@DateAddedTo", DateTo);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@OrderBy", OrderBy);
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

        #region[ExportHistory Added By]
        public IEnumerable GetExportedBy(Int16 OrgID)
        {
            DALDataContext dal = new DALDataContext();
            IEnumerable users = (from u in dal.SecurityUsers
                                 from e in dal.ExportHistories
                                 where u.SecurityUserID == e.AddedBy && u.OrgID == OrgID
                                 select new { u.SecurityUserID, u.DisplayName }).Distinct().OrderBy(u => u.DisplayName);
            return users;
        }
        #endregion

        #region[Search Audit]
        public DataTable SearchAudit(Int32 TableID, long ColumnID, long RowID, DateTime DateFrom, DateTime DateTo, long ModifiedBy, String Source, String UpdatedFrom, Int32 StartRowIndex, Int32 MaximumRows, String OrderBy, Int16 OrgID)
        {
            DataTable dTab = new DataTable("TableHistory");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("TableHistory_Search", Conn);
                Cmd.Parameters.AddWithValue("@TableID", TableID);
                Cmd.Parameters.AddWithValue("@ColumnID", ColumnID);
                Cmd.Parameters.AddWithValue("@RowID", RowID);
                Cmd.Parameters.AddWithValue("@DateModifiedFrom", DateFrom);
                Cmd.Parameters.AddWithValue("@DateModifiedTo", DateTo);
                Cmd.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);
                Cmd.Parameters.AddWithValue("@Source", Source);
                Cmd.Parameters.AddWithValue("@UpdatedFrom", UpdatedFrom);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@OrderBy", OrderBy);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public Int32 SearchAuditCount(Int32 TableID, long ColumnID, long RowID, DateTime DateFrom, DateTime DateTo, long ModifiedBy, String Source, String UpdatedFrom, Int32 StartRowIndex, Int32 MaximumRows, String OrderBy, Int16 OrgID)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("TableHistory_SearchCount", Conn);
                Cmd.Parameters.AddWithValue("@TableID", TableID);
                Cmd.Parameters.AddWithValue("@ColumnID", ColumnID);
                Cmd.Parameters.AddWithValue("@RowID", RowID);
                Cmd.Parameters.AddWithValue("@DateModifiedFrom", DateFrom);
                Cmd.Parameters.AddWithValue("@DateModifiedTo", DateTo);
                Cmd.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);
                Cmd.Parameters.AddWithValue("@Source", Source);
                Cmd.Parameters.AddWithValue("@UpdatedFrom", UpdatedFrom);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@OrderBy", OrderBy);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }
        #endregion

        #region[Audit Added By]
        public IEnumerable GetAuditBy(Int16 OrgID)
        {
            DALDataContext dal = new DALDataContext();
            IEnumerable users = (from u in dal.SecurityUsers
                                 from h in dal.TableHistories
                                 where u.SecurityUserID == h.ModifiedBy
                                 && u.OrgID == OrgID
                                 select new { u.SecurityUserID, u.DisplayName }).Distinct().OrderBy(u => u.DisplayName);
            return users;
        }
        #endregion

        #region[Audit Tables]
        public IEnumerable GetAuditTables()
        {
            DALDataContext dal = new DALDataContext();
            IEnumerable columns = (from c in dal.TableColumnMasters
                                   where c.IsActive == true
                                   select new { c.TableID, c.TableName }).Distinct().OrderBy(c => c.TableName);
            return columns;
        }
        #endregion

        #region[Audit Columns]
        public IEnumerable GetAuditColumns(Int32 TableID)
        {
            DALDataContext dal = new DALDataContext();
            IEnumerable columns = (from c in dal.TableColumnMasters
                                   where c.TableID == TableID && c.IsActive == true
                                   select new { c.ColumnID, c.ColumnName }).Distinct().OrderBy(c => c.ColumnName);
            return columns;
        }
        #endregion



    }

}
