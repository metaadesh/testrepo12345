using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using METAOPTION.DAL;
using System.Reflection;


namespace METAOPTION.BAL
{
    public enum NoteType
    {
        SPECIAL_NOTE = 1,
        TITAL_TRACKING_NOTE = 2,
        OTHER_NOTE = 10
    }

    public enum EntityTypes
    {
        Dealer_Customer = 1,
        Buyer = 2,
        Vendor = 3,
        Utility_Company = 4,
        Employee = 5,
        Inventory = 6
    }

    public enum SystemType
    {
        HEADSTART = 10,
        MAFS_AUTO_TRADER = 11,
        METAOPTION_TEST = 12
    }

    /// <summary>
    /// Common class contain methods used from most of the U.I's
    /// </summary>

    public class Admin_Common
    {
        static Double ShortCacheExpiry = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["ShortCacheExpiry"]);
        static Double LongCacheExpiry = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["LongCacheExpiry"]);

        #region Bind Titles
        /// <summary>
        /// Bind list of titles
        /// </summary>
        public IQueryable GetTitles()
        {
            METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
            return objCommon.GetTitles();
        }

        #endregion

        ///// <summary>
        ///// Decode 9th character with _ in  Vin Number provided
        ///// </summary>
        ///// <param name="strVinNo"></param>
        ///// <returns>Decode 9th character with _ in  Vin Number provided</returns>

        //public static string GetVinNo(string strVinNo)
        //{
        //    strVinNo = strVinNo.Substring(0, 10);
        //    string strTemp = string.Empty;
        //    if (!string.IsNullOrEmpty(strVinNo))
        //    {
        //        if (strVinNo.Length >= 9)
        //        {
        //            strTemp = strVinNo.Trim();
        //            char[] chrVinNo = strTemp.ToCharArray();
        //            chrVinNo[8] = '_';
        //            strTemp = new string(chrVinNo);
        //            strTemp += "%";
        //        }
        //    }
        //    return strTemp;
        //}

        //#region List of Exterior Color for Year,Make,Model,Body Provided
        ///// <summary>
        ///// List of exterior color for Year,Make,model,body provided
        ///// </summary>
        ///// <param name="year"></param>
        ///// <param name="makeId"></param>
        ///// <param name="modelId"></param>
        ///// <param name="bodyId"></param>
        ///// <returns>List of exterior color for Year,Make,model,body provided</returns>
        //public static List<GetExteriorColorsForMMBYResult> GetExteriorColorForMMBY(int year, long makeId, long modelId, long bodyId)
        //{

        //    return DAL.Admin_Common.GetExteriorColorForMMBY(year, makeId, modelId, bodyId);

        //}
        //#endregion

        //#region List of Interior Color for Year,Make,Model,Body Provided
        ///// <summary>
        ///// List of Interior color for Year,Make,model,body provided
        ///// </summary>
        ///// <param name="year"></param>
        ///// <param name="makeId"></param>
        ///// <param name="modelId"></param>
        ///// <param name="bodyId"></param>
        ///// <returns>List of Interior color for Year,Make,model,body provided</returns>
        //public static List<GetInteriorColorsForMMBYResult> GetInteriorColorForMMBY(int year, long makeId, long modelId, long bodyId)
        //{
        //    return DAL.Admin_Common.GetInteriorColorForMMBY(year, makeId, modelId, bodyId);

        //}
        //#endregion

        //#region [Get List of Common Exterior Colors]
        ///// <summary>
        ///// Get List of Common Interior Colors
        ///// </summary>
        ///// <returns>Get List of Common Interior Colors</returns>
        //public static List<GetCommonIntColorsResult> CommonInteriorColors()
        //{
        //    //METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return DAL.Admin_Common.CommonInteriorColors();
        //}
        //#endregion

        //#region [Get List of Common Exterior Colors]
        ///// <summary>
        ///// Get List of Common Exterior Colors
        ///// </summary>
        ///// <returns>Get List of Common Interior Colors</returns>
        //public static List<GetCommonExtColorsResult> CommonExteriorColors()
        //{
        //    return DAL.Admin_Common.CommonExteriorColors();
        //}
        //#endregion

        //#region [Get List of Years]
        ///// <summary>
        ///// Get list of all years
        ///// </summary>
        ///// <returns>List of Years</returns>
        //public static IQueryable GetYearList()
        //{
        //    return (from p in YMM_CommonRecords()
        //            select new { Year = p.YEAR }).Distinct()
        //            .OrderByDescending(x => x.Year)
        //            .AsQueryable();
        //}

        //#endregion

       

        //#region [Get Make for particular Year - Static]
        /// <summary>
        /// Get list of all make for particular year - Static
        /// </summary>
        /// <param name="year"></param>
        /// <returns>List of makes for particular year provided</returns>
        //public static IQueryable GetMakes(int year)
        //{
        //    //METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    //return objCommon.GetMake(year);

        //    return (from p in YMM_CommonRecords()
        //            where p.YEAR == year
        //            select new { p.MakeID, p.Make })
        //           .Distinct()
        //           .OrderBy(x => x.Make)
        //           .AsQueryable();
        //}

        //private static List<YMM_CommonRecordsResult> YMM_CommonRecords()
        //{
        //    if (HttpContext.Current.Cache[Admin_CacheEnum.YMM] == null)
        //    {
        //        List<YMM_CommonRecordsResult> result = DAL.Admin_Common.YMM_CommonRecords().ToList<YMM_CommonRecordsResult>();
        //        HttpContext.Current.Cache.Insert(Admin_CacheEnum.YMM, result, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(LongCacheExpiry));
        //        return result;
        //    }
        //    else
        //        return (List<YMM_CommonRecordsResult>)HttpContext.Current.Cache[Admin_CacheEnum.YMM];

        //}
        //#endregion

        //#region [Get Car Locations]
        ///// <summary>
        ///// This method return list of Car Locations
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable GetCarLocations()
        //{
        //    return objCommon.GetCarLocations();
        //}
        //#endregion

        //#region [Get PaymentTerms]
        ///// <summary>
        ///// region to get payment terms
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable GetPaymentTerms()
        //{
        //    METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.GetPaymentFrequency();
        //}
        //#endregion

        //#region [Get Employee Type Details]
        ///// <summary>
        ///// Return Employee Type Details
        ///// </summary>
        ///// <returns>EmployeeType List</returns>
        //public IQueryable GetEmployeeType()
        //{
        //    METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.GetEmployeeType();
        //}
        //#endregion


      
        #region[Get List of Country]
        public List<GetCountryListResult> GetCountryList()
        {
            //METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
            //return objCommon.GetCountryList();
            if (HttpContext.Current.Cache[Admin_CacheEnum.Country] != null)
                return (List<GetCountryListResult>)HttpContext.Current.Cache[Admin_CacheEnum.Country];
            else
            {
                METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
                List<GetCountryListResult> country = objCommon.GetCountryList();
                HttpContext.Current.Cache[Admin_CacheEnum.Country] = country;
                HttpContext.Current.Cache.Insert(Admin_CacheEnum.Country, country, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(LongCacheExpiry));

                return country;
            }
        }
        #endregion

        #region [Get List of States]
        /// <summary>
        /// This method return list of States for CountryId Provided,Default=1 for U.S
        /// </summary>
        /// <returns></returns>
        public IQueryable GetStates(int CountryId)
        {
            METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
            return objCommon.GetStates(1);
        }
        #endregion

        //#region [Get All Models for a particular Make - Static]
        ///// <summary>
        ///// Get all Models - Static
        ///// </summary>
        ///// <returns>List of All Models for a particular Make provided</returns>
        //public static IQueryable GetModel(long makeId)
        //{
        //    return (from p in YMM_CommonRecords()
        //            where p.MakeID == makeId
        //            select new { p.ModelID, p.Model })
        //           .Distinct()
        //           .OrderBy(x => x.Model)
        //           .AsQueryable();
        //}
        //#endregion

        //#region [Get All Bodies For a Make]
        ///// <summary>
        ///// Get list of all Bodies
        ///// </summary>
        ///// <returns>List of all bodies For a Make</returns>
        //public IQueryable GetBodies(long modelId)
        //{
        //    METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.GetBodies(modelId); ;
        //}
        //#endregion

        //#region Trans List
        ///// <summary>
        ///// Get trans list
        ///// </summary>
        ///// <returns>Trans List</returns>
        //public static List<GetTransResult> GetTransList()
        //{
        //    if (HttpContext.Current.Cache[Admin_CacheEnum.Trans] != null)
        //        return (List<GetTransResult>)HttpContext.Current.Cache[Admin_CacheEnum.Trans];
        //    else
        //    {
        //        METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //        List<GetTransResult> tran = objCommon.GetTransList();
        //        HttpContext.Current.Cache[Admin_CacheEnum.Trans] = tran;
        //        HttpContext.Current.Cache.Insert(Admin_CacheEnum.Trans, tran, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(LongCacheExpiry));

        //        return tran;
        //    }
        //}
        //#endregion

        //#region Engine List
        ///// <summary>
        ///// Get Engine List
        ///// </summary>
        ///// <returns>Engine List</returns>
        //public List<GetEnginesResult> GetEngineList()
        //{
        //    if (HttpContext.Current.Cache[Admin_CacheEnum.Engines] != null)
        //        return (List<GetEnginesResult>)HttpContext.Current.Cache[Admin_CacheEnum.Engines];
        //    else
        //    {
        //        METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //        List<GetEnginesResult> engine = objCommon.GetEngineList();
        //        HttpContext.Current.Cache[Admin_CacheEnum.Engines] = engine;
        //        HttpContext.Current.Cache.Insert(Admin_CacheEnum.Engines, engine, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(LongCacheExpiry));

        //        return engine;
        //    }
        //}
        //#endregion

        //#region [Get WheelDrive]
        ///// <summary>
        ///// region to get wheel drive
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable GetWheelDriveList()
        //{
        //    METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.GetWheelDriveList();
        //}
        //#endregion

        //#region [Get Designation]
        ///// <summary>
        ///// region to get designation
        ///// </summary>
        ///// <returns></returns>
        //public List<GetDesignationResult> GetDesignationList()
        //{
        //    if (HttpContext.Current.Cache[Admin_CacheEnum.Designation] != null)
        //        return (List<GetDesignationResult>)HttpContext.Current.Cache[Admin_CacheEnum.Designation];
        //    else
        //    {
        //        METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //        List<GetDesignationResult> des = objCommon.GetDesignationList();
        //        HttpContext.Current.Cache[Admin_CacheEnum.Designation] = des;
        //        HttpContext.Current.Cache.Insert(Admin_CacheEnum.Designation, des, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(LongCacheExpiry));

        //        return des;
        //    }
        //}
        //#endregion

        //#region [Get Buyer List]
        ///// <summary>
        ///// region to get buyer list
        ///// </summary>
        ///// <returns></returns>
        //public static List<GetBuyerListResult> GetBuyerList(short OrgID)
        //{
        //    if (HttpContext.Current.Cache[Admin_CacheEnum.Buyers] != null)
        //        return (List<GetBuyerListResult>)HttpContext.Current.Cache[Admin_CacheEnum.Buyers];
        //    else
        //    {
        //        METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //        List<GetBuyerListResult> buyers = objCommon.GetBuyerList(OrgID);
        //        HttpContext.Current.Cache[Admin_CacheEnum.Buyers] = buyers;
        //        HttpContext.Current.Cache.Insert(Admin_CacheEnum.Buyers, buyers, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(LongCacheExpiry));

        //        return buyers;
        //    }
        //}
        //#endregion

        //#region [Get Buyer List - static]
        ///// <summary>
        ///// region to get buyer list - static
        ///// </summary>
        ///// <returns></returns>
        //public static GetBuyerListNewResult[] GetBuyersWithCode()
        //{
        //    METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.GetBuyerListNew();
        //}
        //#endregion

        //#region[ Get Entity List ]
        ///// <summary>
        ///// Get Entity List
        ///// </summary>
        ///// <returns></returns>
        //public static IQueryable GetEntityList(Int32 isActive)
        //{
        //    return DAL.Admin_Common.EntityTypeList(isActive);
        //}
        //#endregion

        //#region[ Get docuement Type List ]
        ///// <summary>
        ///// Get document type List
        ///// </summary>
        ///// <returns></returns>
        //public static IQueryable DocumentTypeList(Int32 isActive)
        //{
        //    return DAL.Admin_Common.DocumentTypeList(isActive);
        //}
        //#endregion

        ////#region[Get Payment Frequency]
        /////// <summary>
        /////// region to get payment frequency
        /////// </summary>
        /////// <returns></returns>
        ////public IQueryable<PayementFrequency> GetPaymentFrequency()
        ////{
        ////    return objMasterDAL.GetPaymentFrequency();
        ////}
        ////#endregion

        #region [Get Employee Type/Security Group]
        /// <summary>
        /// region to get employee type
        /// or security group
        /// </summary>
        /// <returns></returns>
        public IQueryable<EmployeeType> GetEmployeeType()
        {
            METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
            return objCommon.GetEmployeeType();
        }
        #endregion

        ////#region[Get US State List]
        /////// <summary>
        /////// get us-state list
        /////// </summary>
        /////// <returns></returns>
        ////public  IQueryable<State> GetUSStates()
        ////{
        ////    return objMasterDAL.GetUSStates();
        ////}
        ////#endregion

        ////#region [Get Employee Document Type]
        /////// <summary>
        /////// region to get employee document type
        /////// </summary>
        /////// <returns></returns>
        ////public IQueryable<EmpDocumentType> GetEmployeeDocument()
        ////{
        ////    return objMasterDAL.GetEmployeeDocumentType();
        ////}
        ////#endregion

        ////#region [Get Patment Terms]
        ////public IQueryable<PaymentTerm> GetPaymentTerms()
        ////{
        ////    return objMasterDAL.GetPaymentTerms();
        ////}
        ////#endregion

        ////#region [Get Contact Type]
        /////// <summary>
        /////// region to get contact type
        /////// </summary>
        /////// <returns></returns>
        ////public IQueryable<ContactType> GetContactType()
        ////{
        ////    return objMasterDAL.GetContactType();
        ////}
        ////#endregion

        ////#region [Get Commision Type]
        /////// <summary>
        /////// region to get commision type
        /////// </summary>
        /////// <returns></returns>
        ////public IQueryable<CommissionType> GetCommisionType()
        ////{
        ////    return objMasterDAL.GetCommisionType();
        ////}
        ////#endregion

        ////#region [Get Commision Paid List]
        /////// <summary>
        /////// region to get commision paid type
        /////// </summary>
        /////// <returns></returns>
        ////public IQueryable<CommissionPaid> GetCommisionPaid()
        ////{
        ////    return objMasterDAL.GetCommisionPaid();
        ////}
        ////#endregion

        ////#region Get Exterior Color
        ////public IQueryable<EColor> GetExteriorColor()
        ////{
        ////    return objMasterDAL.GetExteriorColor();
        ////}
        ////#endregion

        ////#region Get Interior Color
        ////public IQueryable<EColor> GetInteriorColor()
        ////{
        ////    return objMasterDAL.GetInteriorColor();
        ////}
        ////#endregion

        ////#region Get U.S State List
        ////public IQueryable<State> GetUSStateList()
        ////{
        ////    return objMasterDAL.GetUSStateList();
        ////}
        ////#endregion

        ////#region [Get vendor Category]
        /////// <summary>
        /////// region to get Vendor Category
        /////// </summary>
        /////// <returns></returns>
        ////public IQueryable<VendorCategory> GetVendorCategory()
        ////{
        ////    return objMasterDAL.GetVendorCategory();
        ////}
        ////#endregion

        ////#region [Get vendor Type]
        /////// <summary>
        /////// region to get vendor type
        /////// </summary>
        /////// <returns></returns>
        ////public IQueryable<VendorType> GetVendorType()
        ////{
        ////    return objMasterDAL.GetVendorType();
        ////}
        ////#endregion

        ////#region List of Banks
        /////// <summary>
        /////// region to get list of banks for binding in dropdownlists
        /////// </summary>
        /////// <returns></returns>
        ////public IQueryable<Bank> GetBankList()
        ////{
        ////    return objMasterDAL.GetBankList();
        ////}

        ////#endregion

        //#region [ Get all distinct dealer's Name & Id]
        //public static List<GetDealerListResult> GetAllDealer()
        //{
        //    //DAL.Admin_Common objCommon = new DAL.Admin_Common();
        //    //return objCommon.GetAllDealer();
        //    if (HttpContext.Current.Cache[Admin_CacheEnum.Dealer] != null)
        //        return (List<GetDealerListResult>)HttpContext.Current.Cache[Admin_CacheEnum.Dealer];
        //    else
        //    {
        //        METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //        List<GetDealerListResult> dealer = objCommon.GetAllDealer();
        //        HttpContext.Current.Cache[Admin_CacheEnum.Dealer] = dealer;
        //        HttpContext.Current.Cache.Insert(Admin_CacheEnum.Dealer, dealer, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(LongCacheExpiry));

        //        return dealer;
        //    }

        //}
        //#endregion

        //public static IQueryable GetDealerCategory(Int32 isActive)
        //{
        //    return DAL.Admin_Common.GetDealerCategory(isActive);
        //}

        #region [Get List of States, Static function]
        /// <summary>
        /// This method return list of States for CountryId Provided,Default=1 for U.S
        /// </summary>
        /// <returns></returns>
        public static IQueryable GetStateList(int CountryId)
        {
            METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
            IQueryable result = objCommon.GetStates(CountryId).AsQueryable();
            return result;
        }
        #endregion


        #region [Select look up  tables list]
        public static System.Data.DataTable LookupTableRecords(String sqlStatement)
        {
            return DAL.Admin_Common.LookupTableRecords(sqlStatement);
        }
        #endregion

        //#region [Get selected look up table details]
        //public static System.Data.DataTable SelectLookupTableRecords(String sqlStatement)
        //{
        //    return DAL.Admin_Common.SelectLookupTableRecords(sqlStatement);
        //}
        //#endregion

        #region [Update selected records of look up tables]
        public static int UpdateLookupTableRecords(String sqlStatement)
        {
            return (int)DAL.Admin_Common.UpdateLookupTableRecords(sqlStatement);
        }
        #endregion

        #region [Delete selected records of look up tables]
        public static int DeleteLookupTableRecords(String sqlStatement)
        {
            return (int)DAL.Admin_Common.DeleteLookupTableRecords(sqlStatement);
        }
        #endregion


        #region [Select All lookup tables]
        public IQueryable SelectLookUpTbales(bool IsReadWrite)
        {
            METAOPTION.DAL.Admin_Common objdal = new DAL.Admin_Common();
            return objdal.SelectLookUpTables(IsReadWrite);
        }
        #endregion

        #region[Add/Update bank account]
        public int newAddBankAccount(int BankID, string AccNumber, Int16 OrgID, Int64 AddedBy, int isActive, String MODE)
        {
            METAOPTION.DAL.Admin_Common OBJ = new DAL.Admin_Common();

            return OBJ.newAddBankAccount(BankID, AccNumber, OrgID, AddedBy, isActive, MODE);
        }
        #endregion
        #region[Get all Organization list]
        public IQueryable OrganizationList()
        {
            METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
            IQueryable result = objCommon.OrganizationList().AsQueryable();
            return result;

        }
        #endregion
        #region[Get all Organization datatable ]
        public DataTable GetOrganizations(string MODE)
        {
            METAOPTION.DAL.Admin_Common objorg = new METAOPTION.DAL.Admin_Common();
            return objorg.GetOrganizations(MODE) ;

        }
        #endregion

        #region [ Bank List]
        public static IQueryable GetAllBankList()
        {
            METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
            IQueryable result = objCommon.GetAllBankList().AsQueryable();
            return result;
        }
        #endregion

        #region [Select All Entity Type]
        public static IQueryable GetAllEntityType()
        {
            METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
            IQueryable result = objCommon.GetAllEntityType().AsQueryable();
            return result;
        }
        #endregion

        #region[count all entitytype in repeter control at Master left pannel]
        public DataTable GetEmpCount(Int16 ORGID)
        {
            METAOPTION.DAL.Admin_Common obj = new DAL.Admin_Common();
            return obj.GetEmpCount(ORGID);
        }
        #endregion

        //#region [Select All Country List]
        ////public static IQueryable GetAllCountryList()
        ////{
        ////    METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        ////    IQueryable result = objCommon.GetAllCountryList().AsQueryable();
        ////    return result;
        ////}
        //#endregion

        #region [Select All Groups Status]
        public static IQueryable GetAllGroupsStatusList()
        {
            METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
            IQueryable result = objCommon.GetAllGroupsStatusList().AsQueryable();
            return result;
        }
        #endregion

        //#region [ Country List - Active only ]
        ///// <summary>
        ///// Country List - Active only
        ///// </summary>
        ///// <returns></returns>
        //public static IEnumerable CountryList()
        //{
        //    DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.CountryList();

        //}
        //#endregion

        //#region [Get List of Banks]
        ///// <summary>
        ///// This method return list of Banks(BankAccount+BankName)
        ///// </summary>
        ///// <returns>List of Bank Accounts</returns>
        //public IQueryable GetBankAccounts()
        //{
        //    DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.BankAccounts();
        //}
        //#endregion

        ////public static spSystemStatsResult SystemStats(int SystemID, Int16 OrgID)
        ////{
        ////    return DAL.Admin_Common.SystemStats(SystemID, OrgID);
        ////}

        ///////////Added by Rupendra 21 Aug 12//////////////////////////
        ////public static GetMobileActivityCurrentDetailsResult GetMobileActivityDetails(Int16 OrgID)
        ////{
        ////    return DAL.Admin_Common.GetMobileActivityDetails(OrgID);
        ////}

        ////public static GetMobileActivityCurrentDetails_Ver211Result GetMobileActivityDetails_Ver211(Int32 EntityTypeID, Int32 SecurityUserID)
        ////{
        ////    return DAL.Admin_Common.GetMobileActivityDetails_Ver211(EntityTypeID, SecurityUserID);
        ////}

        ////public static GetMobileActivityCurrentDetails_Ver211_NewResult GetMobileActivityDetails_Ver211_New(Int32 EntityTypeID, String IsDirectBuyer, Int32 ParentBuyerId, Int32 BuyerAccessLevel, Int32 EntityId)
        ////{
        ////    return DAL.Admin_Common.GetMobileActivityDetails_Ver211_New(EntityTypeID, IsDirectBuyer, ParentBuyerId, BuyerAccessLevel, EntityId);
        ////}
        //////////////////////////End////////////////////////////////////

        ////public static List<Lane_StaticResult> LaneStatic(Char LaneType, Int16 OrgID)
        ////{
        ////    return DAL.Admin_Common.LaneStatic(LaneType, OrgID);
        ////}

        //#region [Chrome History]
        ///// <summary>
        ///// Return Chrome History Data
        ///// </summary>
        ///// <returns>Return Chrome History Data</returns>
        //public static IEnumerable ShowChromeHistory()
        //{
        //    DAL.Admin_Common common = new METAOPTION.DAL.Admin_Common();
        //    return common.ShowChromeHistory();
        //}
        //#endregion

        //#region [Exception Handling(Log unhandled exceptions in db)]
        ///// <summary>
        ///// Log any unhandled exception in the system and common error message page will be displayed
        ///// </summary>
        ///// <param name="strErrorMessage"></param>
        ///// <returns></returns>
        //public static int LogError(string strErrorMessage)
        //{
        //    DAL.Admin_Common common = new METAOPTION.DAL.Admin_Common();
        //    return common.LogError(strErrorMessage);
        //}
        //#endregion

        //#region [Display List of Live Users]
        ///// <summary>
        ///// Fetch List of Live Users
        ///// </summary>
        ///// <param name="LiveUserCount"></param>
        ///// <returns>Fetch List of Live Users</returns>
        //public static IEnumerable ShowLiveUsers(int topCount, Int16 OrgID)
        //{
        //    DAL.Admin_Common common = new METAOPTION.DAL.Admin_Common();
        //    return common.ShowLiveUsers(topCount, OrgID);
        //}
        //#endregion

        //#region [Get Inventory LifeCycle]
        ///// <summary>
        ///// GetInventoryLifeCycleEvents
        ///// </summary>
        ///// <param name="MasterId"></param>
        ///// <param name="SectionId"></param>
        ///// <returns></returns>
        //public static IEnumerable GetInventoryLifeCycleEvents(long MasterId)
        //{
        //    DAL.Admin_Common common = new METAOPTION.DAL.Admin_Common();
        //    return common.GetInventoryLifeCycleEvents(MasterId);
        //}

        //public static DataTable GetInventoryLifeCycleEvents(long MasterId, Int32 EntityID, Int32 ParentEntityID, Int32 EntityTypeID)
        //{
        //    DAL.Admin_Common common = new METAOPTION.DAL.Admin_Common();
        //    return common.GetInventoryLifeCycleEvents(MasterId, EntityID, ParentEntityID, EntityTypeID);
        //}
        //#endregion

        //public static int CanEntityBeDeleted(long entityId, int entityTypeId)
        //{
        //    return DAL.Admin_Common.CanEntityBeDeleted(entityId, entityTypeId);
        //}

        ///// <summary>
        ///// Soft Delete Entity(IsActive=0 based on EntityId & EntityTypeId provided
        ///// </summary>
        ///// <param name="entityId"></param>
        ///// <param name="entityTypeId"></param>
        ///// <param name="deletedby"></param>
        ///// <param name="deleteddate"></param>
        ///// <returns></returns>
        ///// 

        //public static int DeleteEntity(long entityId, int entityTypeId, long deletedby, DateTime deleteddate)
        //{
        //    return DAL.Admin_Common.DeleteEntity(entityId, entityTypeId, deletedby, deleteddate);
        //}

        //#region Custom Make, Model,Body Insert,Update

        //#region Add Custom Make,Model,Body
        ///// <summary>
        ///// Add Custom Make baed on Year Selected
        ///// </summary>
        ///// <param name="make"></param>
        ///// <param name="year"></param>
        ///// <param name="addedBy"></param>
        ///// <param name="addedDate"></param>
        ///// <returns></returns>
        //public long AddCustomMake(string make, int year, long addedBy, DateTime addedDate)
        //{
        //    DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.AddCustomMake(make, year, addedBy, addedDate);
        //}
        //public long AddCustomModel(string model, long makeId, int year, long addedBy, DateTime addedDate)
        //{
        //    DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.AddCustomModel(model, makeId, year, addedBy, addedDate);
        //}
        //public long AddCustomBody(string body, long modelId, int year, long addedBy, DateTime addedDate)
        //{
        //    DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.AddCustomBody(body, modelId, year, addedBy, addedDate);
        //}
        //#endregion

        //#region Get Custom Make,Model,Body Along with YMMB Data

        //public GETCUSTOMMAKEResult[] GetCustomMake(int year)
        //{
        //    DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.GetCustomMake(year);
        //}
        //public GETCUSTOMMODELResult[] GetCustomModel(long makeId)
        //{
        //    DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.GetCustomModel(makeId);

        //}
        //public GETCUSTOMBODYResult[] GetCustomBody(long modelId)
        //{
        //    DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.GetCustomBody(modelId);
        //}
        //#endregion

        //#region Update Custom Make,Model,Body
        //public int UpdateCustomMake(long makeId, int year, string make, long ModifiedBy, int isActive)
        //{
        //    DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.UpdateCustomMake(makeId, year, make, ModifiedBy, isActive);
        //}

        //public int UpdateCustomModel(long modelId, long makeId, string model, long ModifiedBy, int isActive)
        //{
        //    DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.UpdateCustomModel(modelId, makeId, model, ModifiedBy, isActive);
        //}

        //public int UpdateCustomBody(long bodyId, long modelId, string body, long ModifiedBy, int isActive)
        //{
        //    DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.UpdateCustomBody(bodyId, modelId, body, ModifiedBy, isActive);
        //}

        //#endregion

        //#region Sink YMMB table with Manually Entered Custom Data
        //public void AddCustomRecordsToYMMB(long scheduledBy)
        //{
        //    DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    objCommon.AddCustomRecordsToYMMB(scheduledBy);
        //    HttpContext.Current.Cache.Remove(Admin_CacheEnum.YMM);
        //}
        //#endregion

        //#region IsCustom Year,Make,Model,Body
        //public bool IsCustomMake(long MakeId)
        //{
        //    DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.IsCustomMake(MakeId);
        //}

        //public bool IsCustomModel(long ModelId)
        //{
        //    DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.IsCustomModel(ModelId);
        //}

        //public bool IsCustomBody(long BodyId)
        //{
        //    DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.IsCustomBody(BodyId);
        //}
        //#endregion

        //#endregion

        //#region Get Last 4 Years for Custom YMMB Screen
        ///// <summary>
        ///// Get list of all years
        ///// </summary>
        ///// <returns>List of Years</returns>
        //public GetYearResult[] GetYearList(int count)
        //{
        //    DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.GetYearList(count);
        //}

        //#endregion

        //#region Delete(IsActive=0) Custom Make,Model,Body
        ////Note:- return -999 means error AND -100 means record exists in inventory table.
        //public int DeleteCustomMake(long makeId, long deletedBy)
        //{
        //    DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.DeleteCustomMake(makeId, deletedBy);
        //}
        //public int DeleteCustomModel(long modelId, long deletedBy)
        //{
        //    DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.DeleteCustomModel(modelId, deletedBy);
        //}
        //public int DeleteCustomBody(long bodyId, long deletedBy)
        //{
        //    DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.DeleteCustomBody(bodyId, deletedBy);
        //}

        //#endregion

        //#region [Get All Makes]
        ///// <summary>
        ///// Get list of all makes
        ///// </summary>
        ///// <returns>List of makes</returns>
        //public static IQueryable GetAllMakes()
        //{
        //    METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.GetAllMake();
        //}
        //#endregion

        //#region [Get All Makes - Mobile]
        //public static IQueryable GetAllMake_Mobile()
        //{
        //    METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.GetAllMake_Mobile();
        //}
        //#endregion

        //#region [Get All Models for a particular Make - Mobile]
        //public static IQueryable GetModels_Mobile(long makeId)
        //{
        //    METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.GetModels_Mobile(makeId);
        //}
        //#endregion

        //#region [Get All Bodies For a Make - Mobile]
        //public IQueryable GetBodies_Mobile(long modelId)
        //{
        //    METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.GetBodies_Mobile(modelId); ;
        //}
        //#endregion

        //#region [Get User name and id to bind dropdown]
        //public static IQueryable GetAllUsers()
        //{
        //    METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.GetAllUsers();
        //}

        //#endregion

        //#region [Get mobile Buyers List]
        //public static IQueryable GetMobileBuyerList()
        //{
        //    METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.GetMobileBuyerList();
        //}
        //#endregion

        //#region [Get mobile Buyers List against BuyerID]
        //public static List<BuyerDetail> GetMobileBuyerList_ver211(Int32 SecurityUserID, Int32 ParentEntityID, Int32 EntityTypeID, Int16 OrgID)
        //{
        //    METAOPTION.DAL.Admin_Common objCommon = new METAOPTION.DAL.Admin_Common();
        //    return objCommon.GetMobileBuyerList_ver211(SecurityUserID, ParentEntityID, EntityTypeID, OrgID);
        //}
        //#endregion

        //#region[Code By Adesh]
        //public static System.Collections.ArrayList LaneStatic_New(Int16 OrgID)
        //{
        //    return DAL.Admin_Common.LaneStatic_New(OrgID);
        //}

        //public static System.Collections.ArrayList GetDataForInventorySearchDDLs(Int32 EntityID, Int32 ParentEntityID, Int32 EntityTypeID, Int16 OrgID)
        //{
        //    return DAL.Admin_Common.GetDataForInventorySearchDDLs(EntityID, ParentEntityID, EntityTypeID, OrgID);
        //}

        //public static List<GetModelResult> GetListOfModels(long makeId)
        //{
        //    return DAL.Admin_Common.GetListOfModels(makeId);
        //}

        //#endregion

        //public static List<T> ToCollection<T>(DataTable dt)
        //{
        //    List<T> lst = new System.Collections.Generic.List<T>();
        //    Type tClass = typeof(T);
        //    PropertyInfo[] pClass = tClass.GetProperties();
        //    List<DataColumn> dc = dt.Columns.Cast<DataColumn>().ToList();
        //    T cn;
        //    foreach (DataRow item in dt.Rows)
        //    {
        //        cn = (T)Activator.CreateInstance(tClass);
        //        foreach (PropertyInfo pc in pClass)
        //        {
        //            // Can comment try catch block. 
        //            try
        //            {
        //                DataColumn d = dc.Find(c => c.ColumnName == pc.Name);
        //                if (d != null)
        //                    pc.SetValue(cn, item[pc.Name], null);
        //            }
        //            catch
        //            {
        //            }
        //        }
        //        lst.Add(cn);
        //    }
        //    return lst;
        //}

        //#region [Added by Rupendra 17 Jan 2013, Fetch Regular and Exotic lane of a Buyer]
        //public static System.Collections.ArrayList Lane_StaticForR_E_Ver211(Int32 EntityTypeId, Int32 SecurityId)
        //{
        //    return DAL.Admin_Common.Lane_StaticForR_E_Ver211(EntityTypeId, SecurityId);
        //}

        //public static spSystemStats_Ver211Result SystemStats_Ver211(int SystemID, Int32 EntityiTypeId, Int32 SecurityId)
        //{
        //    return DAL.Admin_Common.SystemStats_Ver211(SystemID, EntityiTypeId, SecurityId);
        //}
        //public static spSystemStats_Dealer_Ver211Result spSystemStats_Dealer_Ver211(int SystemID, Int32 EntityiTypeId, Int32 SecurityId)
        //{
        //    return DAL.Admin_Common.spSystemStats_Dealer_Ver211(SystemID, EntityiTypeId, SecurityId);
        //}
        //#endregion

        //#region User Logout, By prem(8800128549)
        //public static void Logout_Session(Int64 LoginHistoryID)
        //{
        //    DAL.Admin_Common.Logout_Session(LoginHistoryID);
        //}
        //#endregion

        //#region Clear the Cache, Organization wise By Prem(8800128549)
        //public static void ClearCache()
        //{
        //    System.Web.HttpContext.Current.Cache.Remove(Admin_CacheEnum.Announcement);
        //    System.Web.HttpContext.Current.Cache.Remove(Admin_CacheEnum.Buyers);
        //    System.Web.HttpContext.Current.Cache.Remove(Admin_CacheEnum.Country);
        //    System.Web.HttpContext.Current.Cache.Remove(Admin_CacheEnum.Dealer);
        //    System.Web.HttpContext.Current.Cache.Remove(Admin_CacheEnum.Designation);
        //    System.Web.HttpContext.Current.Cache.Remove(Admin_CacheEnum.Engines);
        //    System.Web.HttpContext.Current.Cache.Remove(Admin_CacheEnum.PagePermissions);
        //    System.Web.HttpContext.Current.Cache.Remove(Admin_CacheEnum.Trans);
        //    System.Web.HttpContext.Current.Cache.Remove(Admin_CacheEnum.Vendor);
        //    System.Web.HttpContext.Current.Cache.Remove(Admin_CacheEnum.YMM);
        //}
        //#endregion

        //#region Validate Query String Values, Coded by Prem

        //public static bool Validate_QueryString_Value(Int32 EntityTypeID, Int64 EntityID, Int16 OrgID)
        //{
        //    return DAL.Admin_Common.Validate_QueryString_Value(EntityTypeID, EntityID, OrgID);
        //}

        //public static bool Validate_QueryString_Value(String Page, String Code, Int16 OrgID)
        //{
        //    return DAL.Admin_Common.Validate_QueryString_Value(Page, Code, OrgID);
        //}

        //#endregion

    }
}