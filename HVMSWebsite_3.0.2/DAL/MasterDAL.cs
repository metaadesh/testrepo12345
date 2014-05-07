using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;

using System.Xml.Linq;
using METAOPTION;
using METAOPTION.DAL;

namespace METAOPTION.DAL
{
    public class MasterDAL
    {
        DALDataContext objDAL = new DALDataContext();

        #region [Get Year List]
        /// <summary>
        /// This function return list of Years
        /// </summary>
        /// <returns></returns>
        public List<int> GetYearList()
        {
           
            List<int> lstYear = new List<int>();
            for(int year=1979;year <=2099;year++)
            {
                lstYear.Add(year);
               
            }

            return lstYear;
        }
        #endregion

        //#region [Get Make Type]
        ///// <summary>
        ///// Return Make List
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<make GetMake()
        //{
        //    IQueryable<EMake> result = (from p in objDAL.EMakes
        //                                select p) as IQueryable<EMake>;
        //    return result;
        //}
        //#endregion

        //#region [Get Model Type]
        ///// <summary>
        ///// Return Model List
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<EModel> GetModel()
        //{
        //    IQueryable<EModel> result = (from p in objDAL.EModels
        //                                 select p) as IQueryable<EModel>;
        //    return result;
        //}
        //#endregion

        #region [Get Engine List]
        /// <summary>
        /// Return Engine List
        /// </summary>
        /// <returns></returns>
        public IQueryable<Engine> GetEngineList()
        {
            IQueryable<Engine> result = (from p in objDAL.Engines
                                         select p) as IQueryable<Engine>;
            return result;
        }
        #endregion

        #region [Get Trans List]
        /// <summary>
        /// Return Trans List
        /// </summary>
        /// <returns></returns>
        public IQueryable<Tran> GetTransList()
        {
            IQueryable<Tran > result = (from p in objDAL.Trans
                                         select p) as IQueryable<Tran >;
            return result;
        }
        #endregion

        #region [Get WheelDrive List]
        /// <summary>
        /// Return WheelDrive List
        /// </summary>
        /// <returns></returns>
        public IQueryable<WheelDrive> GetWheelDriveList()
        {
            IQueryable<WheelDrive> result = (from p in objDAL.WheelDrives
                                       select p) as IQueryable<WheelDrive>;
            return result;
        }
        #endregion

        //#region [Get Designation List]
        ///// <summary>
        ///// Return Desingation List
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<Designation> GetDesignationList()
        //{
        //    IQueryable<Designation> result = (from p in objDAL.Designations
        //                               select p) as IQueryable<Designation>;
        //    return result;
        //}
        //#endregion

        #region [Get Buyers List]
        /// <summary>
        /// Return  Buyers List
        /// </summary>
        /// <returns></returns>
        public IQueryable<Buyer> GetBuyerList()
        {
            IQueryable<Buyer> result = (from p in objDAL.Buyers
                                              select p) as IQueryable<Buyer>;
            return result;
        }
        #endregion

        //#region [Get Body List]
        ///// <summary>
        ///// Return Body List
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<Body> GetEBodyList()
        //{
        //    IQueryable<EBody> result = (from p in objDAL.EBodies
        //                                select p) as IQueryable<EBody>;
        //    return result;
        //}
        //#endregion

        #region [Get Payment Frequency Details]
        /// <summary>
        /// Return Payment Frequency Details
        /// </summary>
        /// <returns></returns>
        public IQueryable<PayementFrequency> GetPaymentFrequency()
        {
            IQueryable<PayementFrequency> result = (from p in objDAL.PayementFrequencies
                                        select p) as IQueryable<PayementFrequency>;
            return result;
        }
        #endregion

        #region [Get Company Category Details]
        /// <summary>
        /// Return Company Category Details
        /// </summary>
        /// <returns></returns>
        public IQueryable<CompanyCategory> GetCompanyCategory()
        {
            IQueryable<CompanyCategory> result = (from p in objDAL.CompanyCategories
                                                    select p) as IQueryable<CompanyCategory>;
            return result;
        }
        #endregion
        
        #region [Get Employee Type Details]
        /// <summary>
        /// Return Employee Type Details
        /// </summary>
        /// <returns></returns>
        public IQueryable<SecurityGroup> GetEmployeeType()
        {
            IQueryable<SecurityGroup> result = (from p in objDAL.SecurityGroups
                                                  select p) as IQueryable<SecurityGroup>;
            return result;
        }
        #endregion

        #region [Get Employee Document Type]
        /// <summary>
        /// region to get employee document type
        /// </summary>
        /// <returns></returns>
        public IQueryable<DocumentType> GetEmployeeDocumentType()
        {
            IQueryable<DocumentType> result = (from p in objDAL.DocumentTypes
                                               select p) as IQueryable<DocumentType>;
            return result;
        }
        #endregion

        #region [Get US-State List]
        /// <summary>
        /// This static method return list of All US States
        /// </summary>
        /// <returns></returns>
        public  IQueryable<State>  GetUSStates()
        {

            IQueryable<State> result = (from p in objDAL.States
                                                  select p) as IQueryable<State>;
            return result;
            
        }
        #endregion


        #region [Get US-State List]
        /// <summary>
        /// This static method return list of All US States
        /// </summary>
        /// <returns></returns>
        public IQueryable<State> GetUSStateList()
        {

            IQueryable<State> result = (from p in objDAL.States
                                        select p) as IQueryable<State>;
            return result;

        }
        #endregion

        #region [Get PaymentTerms]
        /// <summary>
        /// region to get payment terms
        /// </summary>
        /// <returns></returns>
        public IQueryable<PaymentTerm> GetPaymentTerms()
        {
            IQueryable<PaymentTerm> result = (from p in objDAL.PaymentTerms
                                              select p) as IQueryable<PaymentTerm>;
            return result;
        }
        #endregion

        #region [Get Contact Type]
        /// <summary>
        /// region to get contact type
        /// </summary>
        /// <returns></returns>
        public IQueryable<ContactType> GetContactType()
        {
            IQueryable<ContactType> result = (from p in objDAL.ContactTypes
                                              select p) as IQueryable<ContactType>;
            return result;
        }
        #endregion

        #region [Get Commision Type]
        /// <summary>
        /// region to get commision type
        /// </summary>
        /// <returns></returns>
        //public IQueryable<CommissionType> GetCommisionType()
        //{
        //    IQueryable<CommissionType> result = (from p in objDAL.CommissionTypes
        //                                         select p) as IQueryable<CommissionType>;
        //    return result;
        //}

        public List<CommissionType> GetCommisionType()
        {
            List<CommissionType> result = (from p in objDAL.CommissionTypes
                                           select p).ToList<CommissionType>();
            return result;
        }
          #endregion

        #region [Get Commision Paid List]
        /// <summary>
        /// region to get commision paid type
        /// </summary>
        /// <returns></returns>
        public IQueryable<CommissionTerm> GetCommisionPaid()
        {
            IQueryable<CommissionTerm> result = (from p in objDAL.CommissionTerms
                                                 select p) as IQueryable<CommissionTerm>;
            return result;
        }
        #endregion


        //#region [Get Interior Color List]
        ///// <summary>
        ///// region to get Interior Color
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<EColor> GetInteriorColor()
        //{
        //    /*Note: 0 For Interior Colors*/
        //    IQueryable<EColor> result = (from p in objDAL.EColors
        //                                 where p.IsIntOrExterior==0        
        //                                 select p
        //                                ) as IQueryable<EColor>;
        //    return result;
        //}
        //#endregion


        //#region [Get Exterior Color List]
        ///// <summary>
        ///// region to get Interior Color
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<EColor> GetExteriorColor()
        //{
        //    /*Note:1 For Exterior Colors*/
        //    IQueryable<EColor> result = (from p in objDAL.EColors
        //                                 where p.IsIntOrExterior == 1
        //                                 select p
        //                                ) as IQueryable<EColor>;
        //    return result;
        //}
        //#endregion

        #region [Get vendor Category]
        /// <summary>
        /// region to get Vendor Category
        /// </summary>
        /// <returns></returns>
        public IQueryable<VendorCategory> GetVendorCategory()
        {
            IQueryable<VendorCategory> result = (from p in objDAL.VendorCategories
                                                 select p) as IQueryable<VendorCategory>;
            return result;
        }
        #endregion

        #region [Get vendor Type]
        /// <summary>
        /// region to get vendor type
        /// </summary>
        /// <returns></returns>
        public IQueryable<VendorType> GetVendorType()
        {
            IQueryable<VendorType> result = (from p in objDAL.VendorTypes
                                             select p) as IQueryable<VendorType>;
            return result;
        }
        #endregion

        #region [Get TitleType List]
        public IQueryable<Title> GetTitleList()
        {
            IQueryable<Title> result = (from p in objDAL.Titles
                                        select p) as IQueryable<Title>;
            return result;
        }
        #endregion

        #region [Get JobTitle List]
        public IQueryable<JobTitle> GetJobTitle()
        {
            IQueryable<JobTitle> result = (from p in objDAL.JobTitles
                                           select p) as IQueryable<JobTitle>;
            return result;
        }
        #endregion

        #region [Get Dealer Type]
        public IQueryable<DealerType> GetDealerType()
        {
            IQueryable<DealerType> result = (from p in objDAL.DealerTypes
                                             select p) as IQueryable<DealerType>;
            return result;
        }
        #endregion

        #region [Get Dealer Category]
        /// <summary>
        /// this is the region to get dealer category
        /// </summary>
        /// <returns></returns>
        public IQueryable<DealerCategory> GetDealerCategory()
        {
            IQueryable<DealerCategory> result = (from p in objDAL.DealerCategories
                                                 select p) as IQueryable<DealerCategory>;
            return result;
        }
        #endregion

        #region [Get Dealer Source List]
        /// <summary>
        /// this is the region to get source list
        /// </summary>
        /// <returns></returns>
        public IQueryable<DealerSource> GetDealerSource()
        {
            IQueryable<DealerSource> result = (from p in objDAL.DealerSources
                                               select p) as IQueryable<DealerSource>;
            return result;
        }
        #endregion

        #region [Get All Make List]
        public IQueryable GetMakeList()
        {
            return objDAL.GetMakeList().AsQueryable();
        }
        #endregion

        #region[Get Make List in year Range]
        public IQueryable GetMakeList(Int32 YearFrom,Int32 YearTo)
        {
           return  objDAL.GetMakeInYearRange(YearFrom, YearTo).AsQueryable();
        }
        #endregion

         #region [Get distinct InventoryId]
        public IQueryable GetInventoryId()
        {
            return objDAL.GetInventoryId().AsQueryable(); 
        }
        #endregion

        #region [Get distinct EntityType]
        public IQueryable GetEntityType()
        {
            return objDAL.GetEntityType().AsQueryable();    
        }
        #endregion 

        #region [Get CheckPaidStatus]
        public IQueryable GetCheckPaidStatus()
        {
            return objDAL.GetCheckPaidStatus().AsQueryable();     
        }
        #endregion 

        #region[Get Document Type]
        public IQueryable DocumentType(long EntityTypeId, Int16 OrgID)
        {
            var result = (from doc in objDAL.DocumentTypes
                          where doc.EntityTypeId == EntityTypeId && doc.OrgID==OrgID  //&& doc.IsActive==1
                          select doc).Distinct();
            return result.AsQueryable();
        }
        #endregion
      
    }

}
