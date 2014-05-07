using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Reflection;

namespace METAOPTION.DAL
{
    public class Common
    {
        //DALDataContext objDAL = new DALDataContext();
        // static DALDataContext objDALStatic = new DALDataContext();

        #region [Get List of Years]
        /// <summary>
        /// Get list of all years
        /// </summary>
        /// <returns>List of Years</returns>
        public List<GetYearResult> GetYearList()
        {
            return METAOPTION.DALDataContext.Factory.DB.GetYear().ToList<GetYearResult>();
        }

        #endregion

        #region [Get Make for particular Year]
        /// <summary>
        /// Get list of all make for particular year
        /// </summary>
        /// <param name="year"></param>
        /// <returns>List of makes for particular year provided</returns>
        public IQueryable GetMake(int year)
        {
            return METAOPTION.DALDataContext.Factory.DB.GetMake(year).AsQueryable();
        }
        public static List<YMM_CommonRecordsResult> YMM_CommonRecords()
        {
            return METAOPTION.DALDataContext.Factory.DB.YMM_CommonRecords().ToList<YMM_CommonRecordsResult>();
        }
        #endregion

        #region [Get All Models for a particular Make]
        /// <summary>
        /// Get all Models
        /// </summary>
        /// <returns>List of All Models for a particular Make provided</returns>
        public IQueryable GetModels(long makeId)
        {
            return METAOPTION.DALDataContext.Factory.DB.GetModel(makeId).AsQueryable();
        }
        #endregion

        #region [Get All Bodies For a Make]
        /// <summary>
        /// Get list of all Bodies
        /// </summary>
        /// <returns>List of all bodies For a Make</returns>
        public IQueryable GetBodies(long modelId)
        {
            return METAOPTION.DALDataContext.Factory.DB.GetBody(modelId).AsQueryable();
        }
        #endregion

        #region [Get List of Interior Color for provided Make, Model, Body & Year]
        /// <summary>
        /// Get List of Interior Color for provided Make, Model, Body & Year
        /// </summary>
        /// <param name="year"></param>
        /// <param name="makeId"></param>
        /// <param name="modelId"></param>
        /// <param name="bodyId"></param>
        /// <returns>List of Interior Color for provided Make, Model, Body & Year</returns>
        public static List<GetInteriorColorsForMMBYResult> GetInteriorColorForMMBY(int year, long makeId, long modelId, long bodyId)
        {
            DALDataContext objDAL = new DALDataContext();
            return objDAL.GetInteriorColorsForMMBY(year, makeId, modelId, bodyId).ToList();
        }
        #endregion

        #region [Get List of Dealer Category]
        /// <summary>
        /// This method return list of States for CountryId Provided,Default=1 for U.S
        /// </summary>
        /// <returns></returns>
        public static IQueryable GetDealerCategory(Int32 isActive)
        {
            DALDataContext objDAL = new DALDataContext();
            IQueryable result = (from dc in objDAL.DealerCategories
                                 where dc.IsActive == isActive
                                 select dc).Distinct().AsQueryable();
            return result;
        }
        #endregion

        #region [Get List of Exterior Color for provided Make, Model, Body & Year]
        /// <summary>
        /// Get List of Exterior Color for provided Make, Model, Body & Year
        /// </summary>
        /// <param name="year"></param>
        /// <param name="makeId"></param>
        /// <param name="modelId"></param>
        /// <param name="bodyId"></param>
        /// <returns>List of Exterior Color for provided Make, Model, Body & Year</returns>
        public static List<GetExteriorColorsForMMBYResult> GetExteriorColorForMMBY(int year, long makeId, long modelId, long bodyId)
        {
            return METAOPTION.DALDataContext.Factory.DB.GetExteriorColorsForMMBY(year, makeId, modelId, bodyId).ToList();
        }
        #endregion

        #region [Get Engine List]
        /// <summary>
        /// Return Engine List
        /// </summary>
        /// <returns></returns>
        public List<GetEnginesResult> GetEngineList()
        {
            List<GetEnginesResult> result = METAOPTION.DALDataContext.Factory.DB.GetEngines().ToList<GetEnginesResult>();
            return result;
        }
        #endregion

        #region [Get Trans List]
        /// <summary>
        /// Return Trans List
        /// </summary>
        /// <returns></returns>
        public List<GetTransResult> GetTransList()
        {
            List<GetTransResult> result = METAOPTION.DALDataContext.Factory.DB.GetTrans().ToList<GetTransResult>();
            return result;
        }
        #endregion

        #region [Get WheelDrive List]
        /// <summary>
        /// Return WheelDrive List
        /// </summary>
        /// <returns></returns>
        public IQueryable GetWheelDriveList()
        {
            return METAOPTION.DALDataContext.Factory.DB.GetWheelDrives().AsQueryable();
        }
        #endregion

        #region [Get Designation List]
        /// <summary>
        /// Return Desingation List
        /// </summary>
        /// <returns></returns>
        public List<GetDesignationResult> GetDesignationList()
        {
            List<GetDesignationResult> result = METAOPTION.DALDataContext.Factory.DB.GetDesignation().ToList<GetDesignationResult>();
            return result;
        }
        #endregion

        #region [Get Buyers List]
        /// <summary>
        /// Return  Buyers List in the order First Name & Middle Name
        /// </summary>
        /// <returns></returns>
        public List<GetBuyerListResult> GetBuyerList(short OrgID)
        {
            List<GetBuyerListResult> result = METAOPTION.DALDataContext.Factory.DB.GetBuyerList(OrgID).ToList<GetBuyerListResult>();
            return result;
        }
        #endregion

        #region [Get Buyers List along with BuyerCode]
        /// <summary>
        /// Return  Buyers List in the order First Name & Middle Name along with BuyerCode
        /// </summary>
        /// <returns></returns>
        public GetBuyerListNewResult[] GetBuyerListNew()
        {
            GetBuyerListNewResult[] result = METAOPTION.DALDataContext.Factory.DB.GetBuyerListNew().ToArray();
            return result;
        }
        #endregion

        #region [Get Payment Frequency Details]
        /// <summary>
        /// Return Payment Frequency Details
        /// </summary>
        /// <returns></returns>
        public IQueryable<PayementFrequency> GetPaymentFrequency()
        {
            DALDataContext objDAL = new DALDataContext();
            IQueryable<PayementFrequency> result = (from p in objDAL.PayementFrequencies
                                                    select p) as IQueryable<PayementFrequency>;
            return result;
        }
        #endregion

        #region [ Get EntityType List ]
        /// <summary>
        /// Get EntityType List
        /// </summary>
        /// <param name="isActive">IsActive - 0 or 1</param>
        /// <returns>List of entity types</returns>
        public static IQueryable EntityTypeList(Int32 isActive)
        {
            DALDataContext objDAL = new DALDataContext();
            var result = from ett in objDAL.EntityTypes
                         where ett.IsActive == isActive && ett.IsRealEntity == true
                         select ett;
            return result.AsQueryable();
        }
        #endregion

        #region [ Get Document Type List ]
        /// <summary>
        /// Get Document Type List
        /// </summary>
        /// <param name="isActive">IsActive - 0 or 1</param>
        /// <returns>List of doucument types</returns>
        public static IQueryable DocumentTypeList(Int16 OrgID, Int32 isActive)
        {
            DALDataContext objDAL = new DALDataContext();
            var result = (from doc in objDAL.DocumentTypes
                          where doc.IsActive == isActive && doc.OrgID==OrgID
                          select doc).Distinct();
            return result.AsQueryable();
        }
        #endregion

        #region [Get List of Common Exterior Colors]
        /// <summary>
        /// Get List of Common Exterior Colors
        /// </summary>
        /// <param name="year"></param>
        /// <param name="makeId"></param>
        /// <param name="modelId"></param>
        /// <param name="bodyId"></param>
        /// <returns>Get List of Common Exterior Colors</returns>
        public static List<GetCommonExtColorsResult> CommonExteriorColors()
        {
            return METAOPTION.DALDataContext.Factory.DB.GetCommonExtColors().ToList();
        }
        #endregion

        #region [Get List of Common Interior Colors]
        /// <summary>
        /// Get List of Common Interior Colors
        /// </summary>
        /// <param name="year"></param>
        /// <param name="makeId"></param>
        /// <param name="modelId"></param>
        /// <param name="bodyId"></param>
        /// <returns>Get List of Common Interior Colors</returns>
        public static List<GetCommonIntColorsResult> CommonInteriorColors()
        {
            return METAOPTION.DALDataContext.Factory.DB.GetCommonIntColors().ToList();
        }
        #endregion

        //#region [Get Company Category Details]
        ///// <summary>
        ///// Return Company Category Details
        ///// </summary>
        ///// <returns></returns>

        public IQueryable<CompanyCategory> GetCompanyCategory()
        {
            DALDataContext objDAL = new DALDataContext();
            IQueryable<CompanyCategory> result = (from p in objDAL.CompanyCategories
                                                  select p) as IQueryable<CompanyCategory>;
            return result;
        }

        //#endregion

        //#region [Get Employee Type Details]
        ///// <summary>
        ///// Return Employee Type Details
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<SecurityGroup> GetEmployeeType()
        //{
        //    IQueryable<SecurityGroup> result = (from p in objDAL.SecurityGroups
        //                                          select p) as IQueryable<SecurityGroup>;
        //    return result;
        //}
        //#endregion

        //#region [Get Employee Document Type]
        ///// <summary>
        ///// region to get employee document type
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<EmpDocumentType> GetEmployeeDocumentType()
        //{
        //    IQueryable<EmpDocumentType> result = (from p in objDAL.EmpDocumentTypes
        //                                          select p) as IQueryable<EmpDocumentType>;
        //    return result;
        //}
        //#endregion

        //#region [Get US-State List]
        ///// <summary>
        ///// This static method return list of All US States
        ///// </summary>
        ///// <returns></returns>
        //public  IQueryable<State>  GetUSStates()
        //{

        //    IQueryable<State> result = (from p in objDAL.States
        //                                          select p) as IQueryable<State>;
        //    return result;

        //}
        //#endregion

        #region Bind Titles
        /// <summary>
        /// Bind list of titles
        /// </summary>
        public IQueryable GetTitles()
        {
            IQueryable result = METAOPTION.DALDataContext.Factory.DB.GetTitles().AsQueryable();
            return result;
        }

        #endregion

        #region [Get Employee Type Details]
        /// <summary>
        /// Return Employee Type Details
        /// </summary>
        /// <returns>EmployeeType List</returns>
        public IQueryable GetEmployeeType()
        {
            IQueryable result = METAOPTION.DALDataContext.Factory.DB.GetEmployeeType().AsQueryable();
            return result;
        }
        #endregion

        #region[Get List of Country]
        public List<GetCountryListResult> GetCountryList()
        {
            return METAOPTION.DALDataContext.Factory.DB.GetCountryList().ToList<GetCountryListResult>();
        }
        #endregion

        #region [Get List of States]
        /// <summary>
        /// This method return list of States for CountryId Provided,Default=1 for U.S
        /// </summary>
        /// <returns></returns>
        public IQueryable GetStates(int CountryId)
        {
            IQueryable result = METAOPTION.DALDataContext.Factory.DB.GetStates(CountryId).AsQueryable();
            return result;
        }
        #endregion

        #region [Get List of States, Static function]
        /// <summary>
        /// This method return list of States for CountryId Provided,Default=1 for U.S
        /// </summary>
        /// <returns></returns>
        public static IQueryable GetStateList(int CountryId)
        {
            IQueryable result = METAOPTION.DALDataContext.Factory.DB.GetStates(CountryId).AsQueryable();
            return result;
        }
        #endregion

        #region [Get PaymentTerms]
        /// <summary>
        /// region to get payment terms
        /// </summary>
        /// <returns></returns>
        public IQueryable GetPaymentTerms()
        {
            return METAOPTION.DALDataContext.Factory.DB.GetPaymentFrequency().AsQueryable();
        }
        #endregion

        //#region [Get Contact Type]
        ///// <summary>
        ///// region to get contact type
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<ContactType> GetContactType()
        //{
        //    IQueryable<ContactType> result = (from p in objDAL.ContactTypes
        //                                      select p) as IQueryable<ContactType>;
        //    return result;
        //}
        //#endregion

        //#region [Get Commision Type]
        ///// <summary>
        ///// region to get commision type
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<CommissionType> GetCommisionType()
        //{
        //    IQueryable<CommissionType> result = (from p in objDAL.CommissionTypes
        //                                         select p) as IQueryable<CommissionType>;
        //    return result;
        //}
        //  #endregion

        //#region [Get Commision Paid List]
        ///// <summary>
        ///// region to get commision paid type
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<CommissionPaid> GetCommisionPaid()
        //{
        //    IQueryable<CommissionPaid> result = (from p in objDAL.CommissionPaids
        //                                         select p) as IQueryable<CommissionPaid>;
        //    return result;
        //}
        //#endregion

        //#region [Get vendor Category]
        ///// <summary>
        ///// region to get Vendor Category
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<VendorCategory> GetVendorCategory()
        //{
        //    IQueryable<VendorCategory> result = (from p in objDAL.VendorCategories
        //                                         select p) as IQueryable<VendorCategory>;
        //    return result;
        //}
        //#endregion

        //#region [Get vendor Type]
        ///// <summary>
        ///// region to get vendor type
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<VendorType> GetVendorType()
        //{
        //    IQueryable<VendorType> result = (from p in objDAL.VendorTypes
        //                                     select p) as IQueryable<VendorType>;
        //    return result;
        //}

        //#endregion


        //#region [Get Bank list]
        ///// <summary>
        ///// region to get list of Banks
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<Bank> GetBankList()
        //{
        //    IQueryable<Bank> result = (from p in objDAL.Banks
        //                                     select p) as IQueryable<Bank>;
        //    return result;

        //}

        //#endregion       

        #region [ Get all dealer's Id & Name ]
        public List<GetDealerListResult> GetAllDealer()
        {
            List<GetDealerListResult> result = METAOPTION.DALDataContext.Factory.DB.GetDealerList().ToList<GetDealerListResult>();
            return result;
        }
        #endregion

        #region [ Get look up Tables list ]
        public static System.Data.DataTable LookupTableRecords(String sqlStatement)
        {
            DALDataContext objDAL = new DALDataContext();
            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection())
            {
                con.ConnectionString = objDAL.Connection.ConnectionString;
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = con;

                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();

                cmd.CommandText = sqlStatement;
                cmd.CommandType = System.Data.CommandType.Text;
                System.Data.SqlClient.SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                System.Data.DataTable dtab = new System.Data.DataTable("lookup");
                dtab.Load(dr);
                dr.Close();
                return dtab;
            }
        }

        #endregion

        #region [ Get Selected look up table details ]
        public static System.Data.DataTable SelectLookupTableRecords(String sqlStatement)
        {
            DALDataContext objDAL = new DALDataContext();
            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection())
            {
                con.ConnectionString = objDAL.Connection.ConnectionString;
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = con;

                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();

                cmd.CommandText = sqlStatement;
                cmd.CommandType = System.Data.CommandType.Text;
                System.Data.SqlClient.SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                System.Data.DataTable dtab = new System.Data.DataTable("Selectlookup");
                dtab.Load(dr);
                dr.Close();
                return dtab;
            }
        }
        #endregion

        #region [ Update lookup table ]
        public static int UpdateLookupTableRecords(String sqlStatement)
        {
            DALDataContext objDAL = new DALDataContext();
            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection())
            {
                con.ConnectionString = objDAL.Connection.ConnectionString;
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = con;

                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();

                cmd.CommandText = sqlStatement;
                cmd.CommandType = System.Data.CommandType.Text;
                return (int)cmd.ExecuteNonQuery();

            }
        }
        #endregion
        #region[Edit bank Account]
        //public int EditBankAccount(Int32 BankAccountID,string AccNumber,Int16 OrgID,Int64 ModifiedBy,string MODE)
        //{
        // }
        #endregion
        #region [ Delete lookup table ]
        public static int DeleteLookupTableRecords(String sqlStatement)
        {
            DALDataContext objDAL = new DALDataContext();
            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection())
            {
                con.ConnectionString = objDAL.Connection.ConnectionString;
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = con;

                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();

                cmd.CommandText = sqlStatement;
                cmd.CommandType = System.Data.CommandType.Text;
                return (int)cmd.ExecuteNonQuery();

            }
        }

        public int newAddBankAccount(int BankID,string AccNumber,Int16 OrgID, Int64 AddedBy, int isActive,String MODE)
        {
            DALDataContext objdal = new DALDataContext();
            Nullable<Int32> Result = null;
            Int32 ReturnValue = 0;
            objdal.AddNewBankAccount(
                ref Result,
                BankID,
                AccNumber,
                OrgID,
                AddedBy,
                isActive,
                MODE);

            if (Result.HasValue)
            {
                ReturnValue = Result.Value;     
            }

            return ReturnValue;
        }
        #endregion

        #region [ Bank list]
        public IQueryable GetAllBankList()
        {
            DALDataContext objDAL = new DALDataContext();
            IQueryable bank = (from bnk in objDAL.Banks
                               where bnk.IsActive == 1
                               select new { bnk.BankId, bnk.BankName }).AsQueryable();

            return bank;
        }
        #endregion

        #region [ Country List - Active only ]
        /// <summary>
        /// Country List - Active only
        /// </summary>
        /// <returns></returns>
        public IEnumerable CountryList()
        {
            DALDataContext objDAL = new DALDataContext();
            var result = (from p in objDAL.Countries
                          where p.IsActive == 1
                          orderby p.CountryName
                          select p).Distinct().AsEnumerable();
            return result;

        }
        #endregion

        //#region [Select All Entity Type]
        //public IQueryable GetAllEntityType()
        //{
        //    IQueryable EntityTypes = (from et in objDAL.EntityTypes
        //                       select new { et.EntityTypeId, et.EntityTypes }).AsQueryable();

        #region [Select All Entity Type]
        public IQueryable GetAllEntityType()
        {
            DALDataContext objDAL = new DALDataContext();
            IQueryable EntityTypes = (from et in objDAL.EntityTypes
                                      where et.IsRealEntity == true
                                      select new { et.EntityTypeId, et.EntityType1 }).AsQueryable();


            return EntityTypes;
        }
        #endregion

        #region [Select All Country List]
        public IQueryable GetAllCountryList()
        {
            DALDataContext objDAL = new DALDataContext();
            IQueryable Countries = (from et in objDAL.Countries
                                    select new { et.CountryId, et.CountryName }).AsQueryable();

            return Countries;
        }
        #endregion

        #region [Select All Groups Status]
        public IQueryable GetAllGroupsStatusList()
        {
            DALDataContext objDAL = new DALDataContext();
            IQueryable GroupStatus = (from et in objDAL.GroupStatus
                                      select new { et.GroupStatusId, et.GroupStatus1 }).AsQueryable();

            return GroupStatus;
        }
        #endregion

        /// <summary>
        /// Retreive List of Bank Accounts
        /// </summary>
        /// <returns>Retreive List of Bank Accounts</returns>
        public IQueryable BankAccounts()
        {
            DALDataContext objDAL = new DALDataContext();
            return objDAL.GetActiveAccounts().AsQueryable();
        }

        #region[ Sytem Statistics ]
        /// <summary>
        /// Sytem Statistics
        /// </summary>
        /// <returns></returns>
        public static spSystemStatsResult SystemStats(int SystemID, Int16 OrgID)
        {
            spSystemStatsResult result = DALDataContext.Factory.DB.spSystemStats(SystemID, OrgID).FirstOrDefault();

            return result; //There is always exactly one row containing computed data
            //foreach (spSystemStatsResult res in result)
            //   return res;

            //return null;
        }
        #endregion

        #region [Added by Rupendra 21 Aug 12 for get deatils of new added Preinventory and preexpense.]
        public static GetMobileActivityCurrentDetailsResult GetMobileActivityDetails(Int16 OrgID)
        {
            GetMobileActivityCurrentDetailsResult result = DALDataContext.Factory.DB.GetMobileActivityCurrentDetails(OrgID).FirstOrDefault();
            return result;
        }

        public static GetMobileActivityCurrentDetails_Ver211Result GetMobileActivityDetails_Ver211(Int32 EntityTypeID, Int32 SecurityUserID)
        {
            GetMobileActivityCurrentDetails_Ver211Result result = DALDataContext.Factory.DB.GetMobileActivityCurrentDetails_Ver211(EntityTypeID, SecurityUserID).FirstOrDefault();
            return result;
        }

        public static GetMobileActivityCurrentDetails_Ver211_NewResult GetMobileActivityDetails_Ver211_New(Int32 EntityTypeID, String IsDirectBuyer, Int32 ParentBuyerId, Int32 BuyerAccessLevel, Int32 EntityId)
        {
            GetMobileActivityCurrentDetails_Ver211_NewResult result = DALDataContext.Factory.DB.GetMobileActivityCurrentDetails_Ver211_New(EntityTypeID, IsDirectBuyer, ParentBuyerId, BuyerAccessLevel, EntityId).FirstOrDefault();
            return result;
        }
        #endregion

        #region[ Lane Static ]
        /// <summary>
        /// Sytem Statistics
        /// </summary>
        /// <returns></returns>
        public static List<Lane_StaticResult> LaneStatic(Char LaneType, Int16 OrgID)
        {
            return DALDataContext.Factory.DB.Lane_Static(LaneType, OrgID).ToList<Lane_StaticResult>();
        }
        #endregion

        #region [Chrome History]
        /// <summary>
        /// Return Chrome History Data
        /// </summary>
        /// <returns>Return Chrome History Data</returns>
        public IEnumerable ShowChromeHistory()
        {
            return METAOPTION.DALDataContext.Factory.DB.ChromeUpdatesSelect().AsEnumerable();
        }
        #endregion

        #region [Exception Handling(Log unhandled exceptions in db)]
        /// <summary>
        /// Log any unhandled exception in the system and common error message page will be displayed
        /// </summary>
        /// <param name="strErrorMessage"></param>
        /// <returns></returns>
        public int LogError(string strErrorMessage)
        {
            return METAOPTION.DALDataContext.Factory.DB.LogErrors(strErrorMessage);
        }
        #endregion

        #region [Display List of Live Users]
        /// <summary>
        /// Fetch List of Live Users
        /// </summary>
        /// <param name="LiveUserCount"></param>
        /// <returns>Fetch List of Live Users</returns>
        public IEnumerable ShowLiveUsers(int topCount, Int16 OrgID)
        {
            return METAOPTION.DALDataContext.Factory.DB.GetLiveUsers(topCount, OrgID);
        }
        #endregion

        #region [Get Inventory LifeCycle]
        /// <summary>
        /// GetInventoryLifeCycleEvents
        /// </summary>
        /// <param name="MasterId"></param>
        /// <param name="SectionId"></param>
        /// <returns></returns>
        public IEnumerable GetInventoryLifeCycleEvents(long MasterId)
        {
            return METAOPTION.DALDataContext.Factory.DB.GetInventoryLifeCycleInfo(MasterId);
        }

        public DataTable GetInventoryLifeCycleEvents(long MasterId, Int32 EntityID, Int32 ParentEntityID, Int32 EntityTypeID)
        {
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("GetInventoryLifeCycleInfo_ver211", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@InventoryID", MasterId);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        #endregion

        public static int CanEntityBeDeleted(long entityId, int entityTypeId)
        {
            int? entityCount = 0;
            METAOPTION.DALDataContext.Factory.DB.CanEntityBeDeleted(entityId, entityTypeId, ref entityCount);
            return entityCount.Value;
        }
        /// <summary>
        /// Soft Delete Entity(IsActive=0 based on EntityId & EntityTypeId provided
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="entityTypeId"></param>
        /// <param name="deletedby"></param>
        /// <param name="deleteddate"></param>
        /// <returns></returns>
        public static int DeleteEntity(long entityId, int entityTypeId, long deletedby, DateTime deleteddate)
        {
            return METAOPTION.DALDataContext.Factory.DB.Delete_Entity(entityId, entityTypeId, deletedby, deleteddate);
        }

        #region Custom Make, Model,Body Insert,Update

        #region Add Custom Make,Model,Body
        /// <summary>
        /// Add Custom Make baed on Year Selected
        /// </summary>
        /// <param name="make"></param>
        /// <param name="year"></param>
        /// <param name="addedBy"></param>
        /// <param name="addedDate"></param>
        /// <returns></returns>
        public long AddCustomMake(string make, int year, long addedBy, DateTime addedDate)
        {
            long? makeID = 0;
            METAOPTION.DALDataContext.Factory.DB.CHROME_YMMB_MAKE_LOG_Insert(make, ref makeID, year, addedBy, addedDate);
            return makeID.GetValueOrDefault();
        }
        public long AddCustomModel(string model, long makeId, int year, long addedBy, DateTime addedDate)
        {
            long? modelId = 0;
            METAOPTION.DALDataContext.Factory.DB.CHROME_YMMB_MODEL_LOG_Insert(model, makeId, ref modelId, year, addedBy, addedDate);
            return modelId.GetValueOrDefault();
        }
        public long AddCustomBody(string body, long modelId, int year, long addedBy, DateTime addedDate)
        {
            long? bodyId = 0;
            METAOPTION.DALDataContext.Factory.DB.CHROME_YMMB_BODY_LOG_Insert(body, modelId, ref bodyId, year, addedBy, addedDate);
            return bodyId.GetValueOrDefault();
        }
        #endregion

        #region Get Custom Make,Model,Body Along with YMMB Data

        public GETCUSTOMMAKEResult[] GetCustomMake(int year)
        {
            return METAOPTION.DALDataContext.Factory.DB.GETCUSTOMMAKE(year).ToArray();
        }
        public GETCUSTOMMODELResult[] GetCustomModel(long makeId)
        {
            return METAOPTION.DALDataContext.Factory.DB.GETCUSTOMMODEL(makeId).ToArray();

        }
        public GETCUSTOMBODYResult[] GetCustomBody(long modelId)
        {
            return METAOPTION.DALDataContext.Factory.DB.GETCUSTOMBODY(modelId).ToArray();
        }
        #endregion

        #region Update Custom Make,Model,Body
        public int UpdateCustomMake(long makeId, int year, string make, long ModifiedBy, int isActive)
        {
            return METAOPTION.DALDataContext.Factory.DB.CHROME_YMMB_MAKE_LOG_Update(year, makeId, make, ModifiedBy, isActive);
        }

        public int UpdateCustomModel(long modelId, long makeId, string model, long ModifiedBy, int isActive)
        {
            return METAOPTION.DALDataContext.Factory.DB.CHROME_YMMB_MODEL_LOG_Update(makeId, modelId, model, ModifiedBy, isActive);
        }

        public int UpdateCustomBody(long bodyId, long modelId, string body, long ModifiedBy, int isActive)
        {
            return METAOPTION.DALDataContext.Factory.DB.CHROME_YMMB_BODY_LOG_Update(modelId, bodyId, body, ModifiedBy, isActive);
        }

        #endregion

        #region Sink YMMB table with Manually Entered Custom Data
        public void AddCustomRecordsToYMMB(long scheduledBy)
        {
            int TransactionId = METAOPTION.DALDataContext.Factory.DB.CHROME_YMMB_SYNCHRONIZATION(scheduledBy);

        }
        #endregion

        #region IsCustom Year,Make,Model,Body
        public bool IsCustomMake(long MakeId)
        {
            int count = METAOPTION.DALDataContext.Factory.DB.IsCustomMake(MakeId).Single().Count.GetValueOrDefault();
            if (count > 0)
                return true;
            else return false;

        }

        public bool IsCustomModel(long ModelId)
        {
            int count = METAOPTION.DALDataContext.Factory.DB.IsCustomModel(ModelId).Single().Count.GetValueOrDefault();
            if (count > 0)
                return true;
            else return false;
        }

        public bool IsCustomBody(long BodyId)
        {
            int count = METAOPTION.DALDataContext.Factory.DB.IsCustomBody(BodyId).Single().Count.GetValueOrDefault();
            if (count > 0)
                return true;
            else return false;
        }
        #endregion

        #endregion

        #region Get Last 4 Years for Custom YMMB Screen
        /// <summary>
        /// Get list of all years
        /// </summary>
        /// <returns>List of Years</returns>
        public GetYearResult[] GetYearList(int count)
        {
            return METAOPTION.DALDataContext.Factory.DB.GetYear().Take(count).ToArray();
        }

        #endregion

        #region Delete(IsActive=0) Custom Make,Model,Body
        //Note:- return -999 means error AND -100 means record exists in inventory table.
        public int DeleteCustomMake(long makeId, long deletedBy)
        {
            return METAOPTION.DALDataContext.Factory.DB.CHROME_YMMB_MAKE_LOG_Delete(makeId, deletedBy);
        }
        public int DeleteCustomModel(long modelId, long deletedBy)
        {
            return METAOPTION.DALDataContext.Factory.DB.CHROME_YMMB_MODEL_LOG_Delete(modelId, deletedBy);
        }
        public int DeleteCustomBody(long bodyId, long deletedBy)
        {
            return METAOPTION.DALDataContext.Factory.DB.CHROME_YMMB_BODY_LOG_Delete(bodyId, deletedBy);
        }

        #endregion

        #region [Get Make for particular Year]
        /// <summary>
        /// Get list of all makes
        /// </summary>
        /// <returns>List of makes</returns>
        public IQueryable GetAllMake()
        {
            return METAOPTION.DALDataContext.Factory.DB.CHROME_YMMB_GetMakes().AsQueryable();
        }

        #endregion

        #region[Get all makes - Mobile]
        public IQueryable GetAllMake_Mobile()
        {
            return METAOPTION.DALDataContext.Factory.DB.Mobile_Chrome_YMMB_GetMakes().AsQueryable();
        }
        #endregion

        #region [Get All Models for a particular Make - Mobile]
        public IQueryable GetModels_Mobile(long makeId)
        {
            return METAOPTION.DALDataContext.Factory.DB.Mobile_Chrome_YMMB_GetModels(makeId).AsQueryable();
        }
        #endregion

        #region [Get All Bodies For a Make - Mobile]
        public IQueryable GetBodies_Mobile(long modelId)
        {
            return METAOPTION.DALDataContext.Factory.DB.Mobile_Chrome_YMMB_GetBody(modelId).AsQueryable();
        }
        #endregion

        #region [Get User name and id to bind dropdown]
        public IQueryable GetAllUsers()
        {
            return METAOPTION.DALDataContext.Factory.DB.Mobile_GetUsers().AsQueryable();
        }
        #endregion

        #region [Get mobile Buyers List]
        public IQueryable GetMobileBuyerList()
        {
            IQueryable result = METAOPTION.DALDataContext.Factory.DB.Mobile_GetBuyerList().AsQueryable();
            return result;
        }
        #endregion

        #region [Get mobile Buyers List against Buyer ID]
        public List<BuyerDetail> GetMobileBuyerList_ver211(Int32 SecurityUserID, Int32 ParentEntityID, Int32 EntityTypeID, Int16 OrgID)
        {
            List<BuyerDetail> lst = new List<BuyerDetail>();
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd;
                if (EntityTypeID == 1)
                    Cmd = new SqlCommand("Mobile_GetBuyerList_PreInv_ver211", Conn);
                else
                    Cmd = new SqlCommand("Mobile_GetBuyerList_ver211", Conn);
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
                BuyerDetail SU = new BuyerDetail();
                SU.BuyerName = dr["BuyerName"].ToString();
                SU.BuyerID = Convert.ToInt32(dr["BuyerId"]);
                lst.Add(SU);
            }
            return lst;


        }
        #endregion

        #region[Code By Adesh]
        public static System.Collections.ArrayList LaneStatic_New(Int16 OrgID)
        {
            DataTable dTab = new DataTable();
            System.Collections.ArrayList array = new System.Collections.ArrayList();
            DALDataContext objDal = new DALDataContext();
            SqlDataReader reader = null;
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                try
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();

                    SqlCommand Cmd = new SqlCommand("Lane_StaticForR_E", Conn);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@OrgID", OrgID);

                    reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dTab.Load(reader);
                    array.Add(dTab);

                    dTab = new DataTable();
                    dTab.Load(reader);
                    array.Add(dTab);

                }
                catch (Exception ex)
                { }
                finally
                {
                    reader.Close();
                    objDal.Dispose();
                    Conn.Close();

                }
            }
            return array;

        }

        public static System.Collections.ArrayList GetDataForInventorySearchDDLs(Int32 EntityID, Int32 ParentEntityID, Int32 EntityTypeID, Int16 OrgID)
        {
            DataTable dTab = new DataTable();
            System.Collections.ArrayList array = new System.Collections.ArrayList();
            DALDataContext objDal = new DALDataContext();
            SqlDataReader reader = null;
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                try
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();

                    SqlCommand Cmd = new SqlCommand("GetDataForInventorySearchDDLs_ver211", Conn);
                    Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                    Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                    Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                    Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                    Cmd.CommandType = CommandType.StoredProcedure;

                    reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    //Get BuyerList
                    dTab.Load(reader);
                    array.Add(dTab);

                    //Get Designation
                    dTab = new DataTable();
                    dTab.Load(reader);
                    array.Add(dTab);

                    //Get Year
                    dTab = new DataTable();
                    dTab.Load(reader);
                    array.Add(dTab);

                    //Get Make
                    dTab = new DataTable();
                    dTab.Load(reader);
                    array.Add(dTab);

                    //Get CountryList
                    dTab = new DataTable();
                    dTab.Load(reader);
                    array.Add(dTab);

                }
                catch (Exception ex)
                { }
                finally
                {
                    reader.Close();
                    objDal.Dispose();
                    Conn.Close();

                }
            }
            return array;

        }

        public static List<GetModelResult> GetListOfModels(long makeId)
        {
            return METAOPTION.DALDataContext.Factory.DB.GetModel(makeId).ToList<GetModelResult>();
        }

        #endregion

        public static List<T> ToCollection<T>(DataTable dt)
        {
            List<T> lst = new System.Collections.Generic.List<T>();
            Type tClass = typeof(T);
            PropertyInfo[] pClass = tClass.GetProperties();
            List<DataColumn> dc = dt.Columns.Cast<DataColumn>().ToList();
            T cn;
            foreach (DataRow item in dt.Rows)
            {
                cn = (T)Activator.CreateInstance(tClass);
                foreach (PropertyInfo pc in pClass)
                {
                    // Can comment try catch block. 
                    try
                    {
                        DataColumn d = dc.Find(c => c.ColumnName == pc.Name);
                        if (d != null)
                            pc.SetValue(cn, item[pc.Name], null);
                    }
                    catch
                    {
                    }
                }
                lst.Add(cn);
            }
            return lst;
        }

        #region [Added by Rupendra 17 Jan 2013, Fetch Regular and Exotic lane of a Buyer]
        public static System.Collections.ArrayList Lane_StaticForR_E_Ver211(Int32 EntityTypeId, Int32 SecurityId)
        {
            DataTable dTab = new DataTable();
            System.Collections.ArrayList array = new System.Collections.ArrayList();
            DALDataContext objDal = new DALDataContext();
            SqlDataReader reader = null;
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                try
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();
                    SqlCommand Cmd;
                    if (EntityTypeId == 1)
                        Cmd = new SqlCommand("Lane_StaticForR_EForDealer_Ver211", Conn);
                    else
                        Cmd = new SqlCommand("Lane_StaticForR_E_Ver211", Conn);
                    Cmd.Parameters.AddWithValue("@EntityTypeId", EntityTypeId);
                    Cmd.Parameters.AddWithValue("@SecurityUserID", SecurityId);
                    Cmd.CommandType = CommandType.StoredProcedure;

                    reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dTab.Load(reader);
                    array.Add(dTab);

                    dTab = new DataTable();
                    dTab.Load(reader);
                    array.Add(dTab);

                }
                catch (Exception ex)
                { }
                finally
                {
                    reader.Close();
                    objDal.Dispose();
                    Conn.Close();

                }
            }
            return array;

        }

        public static spSystemStats_Ver211Result SystemStats_Ver211(int SystemID, Int32 EntityiTypeId, Int32 SecurityId)
        {
            spSystemStats_Ver211Result result = DALDataContext.Factory.DB.spSystemStats_Ver211(SystemID, EntityiTypeId, SecurityId).FirstOrDefault();
            return result;
        }
        public static spSystemStats_Dealer_Ver211Result spSystemStats_Dealer_Ver211(int SystemID, Int32 EntityiTypeId, Int32 SecurityId)
        {
            spSystemStats_Dealer_Ver211Result result = DALDataContext.Factory.DB.spSystemStats_Dealer_Ver211(SystemID, EntityiTypeId, SecurityId).FirstOrDefault();
            return result;
        }
        #endregion

        #region User Logout, By prem(8800128549)
        public static void Logout_Session(Int64 LoginHistoryID)
        {
            METAOPTION.DALDataContext.Factory.DB.Logout(LoginHistoryID);
        }
        #endregion

        #region Validate Query String Values, Coded by Prem
        public static bool Validate_QueryString_Value(Int32 EntityTypeID, Int64 EntityID, Int16 OrgID)
        {
            bool isValid = false;
            using (SqlConnection con = new SqlConnection(DALDataContext.Factory.DB.Connection.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SP_Validate_QueryString_Value", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                cmd.Parameters.AddWithValue("@EntityID", EntityID);
                cmd.Parameters.AddWithValue("@OrgID", OrgID);
                isValid = (bool)cmd.ExecuteScalar();
            }
            return isValid;
        }

        public static bool Validate_QueryString_Value(String PageName, String Code, Int16 OrgID)
        {
            bool isValid = false;
            using (SqlConnection con = new SqlConnection(DALDataContext.Factory.DB.Connection.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SP_Validate_QueryString_Value_2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PageName", PageName);
                cmd.Parameters.AddWithValue("@Code", Code);
                cmd.Parameters.AddWithValue("@OrgID", OrgID);
                isValid = (bool)cmd.ExecuteScalar();
            }
            return isValid;
        }

        #endregion
    }

    public class BuyerDetail
    {
        public string BuyerName { get; set; }
        public Int32 BuyerID { get; set; }
    }

}