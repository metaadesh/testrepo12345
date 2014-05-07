using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using METAOPTION;
using METAOPTION.DAL;
using System.Web;
namespace METAOPTION.BAL
{
    public class MasterBAL
    {
        static Double ShortCacheExpiry = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["ShortCacheExpiry"]);
        static Double LongCacheExpiry = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["LongCacheExpiry"]);

        MasterDAL objMasterDAL = new MasterDAL();
        #region [Get Year List]
        /// <summary>
        /// region to get year list
        /// </summary>
        /// <returns></returns>
        public List<int> GetYearList()
        {
            return objMasterDAL.GetYearList();
        }
        #endregion

        //#region [Get Make]
        ///// <summary>
        ///// region to get Emake
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<EMake> GetMake()
        //{
        //    return objMasterDAL.GetMake();
        //}
        //#endregion

        //#region [Get Model]
        ///// <summary>
        ///// region to get Model
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<EModel> GetModel()
        //{
        //    return objMasterDAL.GetModel();
        //}
        //#endregion

        #region [Get Engine List]
        /// <summary>
        /// region to get engine list
        /// </summary>
        /// <returns></returns>
        public IQueryable<Engine> GetEngineList()
        {
            return objMasterDAL.GetEngineList();
        }
        #endregion

        #region [Get Trans List]
        /// <summary>
        /// region to get Trans List
        /// </summary>
        /// <returns></returns>
        public IQueryable<Tran> GetTransList()
        {
            return objMasterDAL.GetTransList();
        }
        #endregion

        #region [Get WheelDrive]
        /// <summary>
        /// region to get wheel drive
        /// </summary>
        /// <returns></returns>
        public IQueryable<WheelDrive> GetWheelDriveList()
        {
            return objMasterDAL.GetWheelDriveList();
        }
        #endregion

        //#region [Get Designation]
        ///// <summary>
        ///// region to get designation
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<Designation> GetDesignationList()
        //{
        //    return objMasterDAL.GetDesignationList();
        //}
        //#endregion

        #region [Get Buyer List]
        /// <summary>
        /// region to get buyer list
        /// </summary>
        /// <returns></returns>
        public IQueryable<Buyer> GetBuyerList()
        {
            return objMasterDAL.GetBuyerList();
        }
        #endregion

        //#region [Get EBody Type]
        ///// <summary>
        ///// region to get EBody type
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<EBody> GetEBodyList()
        //{
        //    return objMasterDAL.GetEBodyList();
        //}
        //#endregion

        #region [Get Company Category]
        /// <summary>
        /// region to get company category
        /// or company type
        /// </summary>
        /// <returns></returns>
        public IQueryable<CompanyCategory> GetCompanyCategory()
        {

            return objMasterDAL.GetCompanyCategory();
        }
        #endregion

        #region[Get Payment Frequency]
        /// <summary>
        /// region to get payment frequency
        /// </summary>
        /// <returns></returns>
        public IQueryable<PayementFrequency> GetPaymentFrequency()
        {
            return objMasterDAL.GetPaymentFrequency();
        }
        #endregion

        #region [Get Employee Type/Security Group]
        /// <summary>
        /// region to get employee type
        /// or security group
        /// </summary>
        /// <returns></returns>
        public IQueryable<SecurityGroup> GetEmployeeType()
        {

            return objMasterDAL.GetEmployeeType();
        }
        #endregion

        #region[Get US State List]
        /// <summary>
        /// get us-state list
        /// </summary>
        /// <returns></returns>
        public  IQueryable<State> GetUSStates()
        {
            return objMasterDAL.GetUSStates();
        }
        #endregion

        #region [Get Employee Document Type]
        /// <summary>
        /// region to get employee document type
        /// </summary>
        /// <returns></returns>
        public IQueryable<DocumentType> GetEmployeeDocument()
        {
            return objMasterDAL.GetEmployeeDocumentType();
        }
        #endregion

        #region [Get Patment Terms]
        public IQueryable<PaymentTerm> GetPaymentTerms()
        {
            return objMasterDAL.GetPaymentTerms();
        }
        #endregion

        #region [Get Contact Type]
        /// <summary>
        /// region to get contact type
        /// </summary>
        /// <returns></returns>
        public IQueryable<ContactType> GetContactType()
        {
            return objMasterDAL.GetContactType();
        }
        #endregion

        #region [Get Commision Type]
        /// <summary>
        /// region to get commision type
        /// </summary>
        /// <returns></returns>
        //public IQueryable<CommissionType> GetCommisionType()
        //{
        //    return objMasterDAL.GetCommisionType();
        //}

        public List<CommissionType> GetCommisionType()
        {
            //return objMasterDAL.GetCommisionType();
            if (HttpContext.Current.Cache[CacheEnum.CommissionType] != null)
                return (List<CommissionType>)HttpContext.Current.Cache[CacheEnum.CommissionType];
            else
            {
                METAOPTION.DAL.Common objCommon = new METAOPTION.DAL.Common();
                List<CommissionType> des = objMasterDAL.GetCommisionType();
                HttpContext.Current.Cache[CacheEnum.CommissionType] = des;
                HttpContext.Current.Cache.Insert(CacheEnum.CommissionType, des, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(LongCacheExpiry));

                return des;
            }
        }
        #endregion

        #region [Get Commision Paid List]
        /// <summary>
        /// region to get commision paid type
        /// </summary>
        /// <returns></returns>
        public IQueryable<CommissionTerm> GetCommisionPaid()
        {
            return objMasterDAL.GetCommisionPaid();
        }
        #endregion

        //#region Get Exterior Color
        //public IQueryable<EColor> GetExteriorColor()
        //{
        //    return objMasterDAL.GetExteriorColor();
        //}
        //#endregion

        //#region Get Interior Color
        //public IQueryable<EColor> GetInteriorColor()
        //{
        //    return objMasterDAL.GetInteriorColor();
        //}
        //#endregion

        #region Get U.S State List
        public IQueryable<State> GetUSStateList()
        {
            return objMasterDAL.GetUSStateList();
        }
        #endregion

        #region [Get vendor Category]
        /// <summary>
        /// region to get Vendor Category
        /// </summary>
        /// <returns></returns>
        public IQueryable<VendorCategory> GetVendorCategory()
        {
            return objMasterDAL.GetVendorCategory();
        }
        #endregion

        #region [Get vendor Type]
        /// <summary>
        /// region to get vendor type
        /// </summary>
        /// <returns></returns>
        public IQueryable<VendorType> GetVendorType()
        {
            return objMasterDAL.GetVendorType();
        }
        #endregion

        #region [Get TitleType List]
        public IQueryable<Title> GetTitleList()
        {
            return objMasterDAL.GetTitleList();
        }
        #endregion

        #region [Get JobTitle List]
        public IQueryable<JobTitle> GetJobTitle()
        {
            return objMasterDAL.GetJobTitle();
        }
        #endregion

        #region [Get Dealer Type]
        public IQueryable<DealerType> GetDealerType()
        {
            return objMasterDAL.GetDealerType();
        }
        #endregion

        #region [Get Dealer Category]
        /// <summary>
        /// this is the region to get dealer category
        /// </summary>
        /// <returns></returns>
        public IQueryable<DealerCategory> GetDealerCategory()
        {
            return objMasterDAL.GetDealerCategory();
        }
        #endregion

        #region [Get Dealer Source List]
        /// <summary>
        /// this is the region to get source list
        /// </summary>
        /// <returns></returns>
        public IQueryable<DealerSource> GetDealerSource()
        {
            return objMasterDAL.GetDealerSource();
        }
        #endregion

        #region [Get All Make List]
        public IQueryable GetMakeList()
        {
            return objMasterDAL.GetMakeList();
        }
        #endregion

        #region[Get Make List in year Range]
        public IQueryable GetMakeList(Int32 YearFrom, Int32 YearTo)
        {
            return objMasterDAL.GetMakeList(YearFrom, YearTo);
        }
        #endregion

        #region [Get distinct InventoryId]
        public IQueryable GetInventoryId()
        {
            return objMasterDAL.GetInventoryId(); 
        }
        #endregion

        #region [Get distinct EntityType]
        public IQueryable GetEntityType()
        {
            return objMasterDAL.GetEntityType();  
        }
        #endregion 

        #region [Get CheckPaidStatus]
        public IQueryable GetCheckPaidStatus()
        {
            return objMasterDAL.GetCheckPaidStatus(); 
        }
        #endregion 

        #region[Get Document Type]
        public IQueryable DocumentType(long EntityTypeId , Int16 OrgID)
        {

            return objMasterDAL.DocumentType(EntityTypeId, OrgID);
        }
        #endregion
    }
}
