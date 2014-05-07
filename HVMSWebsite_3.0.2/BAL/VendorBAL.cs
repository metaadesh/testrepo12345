using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using METAOPTION;
using METAOPTION.DAL;
using System.Web;
using System.Collections.Generic;

namespace METAOPTION.BAL
{
    public class VendorBAL
    {
        static Double ShortCacheExpiry = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["ShortCacheExpiry"]);
        static Double LongCacheExpiry = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["LongCacheExpiry"]);
     
        #region [Add vender details]
        /// <summary>
        /// region for add vender details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long AddVenderDetails(Vendor objVendor, Address objAddress)
        {
            METAOPTION.DAL.VendorDAL objvendorDAL = new VendorDAL();
            HttpContext.Current.Cache.Remove(CacheEnum.Vendor);
            return objvendorDAL.AddVenderDetails(objVendor, objAddress);
        }
        #endregion

        #region[Get Vendor Details]
        public static IQueryable GetVendorDetails(long VendorId)
        {
            METAOPTION.DAL.VendorDAL objvendorDAL = new VendorDAL();
            return objvendorDAL.GetVendorDetails(VendorId);
        }
        #endregion

        #region[Get Vendor List along with Search]
        public static IEnumerable GetVendorList(string vendorName, Int32 categoryId, Int32 typeId, string city, Int32 countryId, Int32 stateId, string zip, Int32 Status,Int16 OrgID)
        {
            METAOPTION.DAL.VendorDAL objvendorDAL = new VendorDAL();
            return objvendorDAL.GetVendorList(vendorName, categoryId, typeId, city, countryId, stateId, zip, Status, OrgID);
        }
        #endregion

        #region [Get Vendors List]
        public static List<GetVendorListResult> GetVendorList(short OrgID)
        {
            //METAOPTION.DAL.VendorDAL objvendorDAL = new VendorDAL();
            //return objvendorDAL.GetVendorList();
            if (HttpContext.Current.Cache[CacheEnum.Vendor] != null)
                return (List<GetVendorListResult>)HttpContext.Current.Cache[CacheEnum.Vendor];
            else
            {
                METAOPTION.DAL.VendorDAL objVendor = new METAOPTION.DAL.VendorDAL();
                List<GetVendorListResult> vendor = objVendor.GetVendorList(OrgID);
                HttpContext.Current.Cache[CacheEnum.Vendor] = vendor;
                HttpContext.Current.Cache.Insert(CacheEnum.Vendor, vendor, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(ShortCacheExpiry));

                return vendor;
            }
        }
        #endregion

        #region[Update Vendor Details]
        public static int UpdateVendorDetails(Vendor objVendor, Address objAddress)
        {
            METAOPTION.DAL.VendorDAL objvendorDAL = new VendorDAL();
            HttpContext.Current.Cache.Remove(CacheEnum.Vendor);
            return objvendorDAL.UpdateVendorDetails(objVendor, objAddress);
        }
        #endregion

        #region[Update ExpenseAutoApproval Flag]
        public static void UpdateExpenseAutoApprovalFlag(Int64 VendorID, bool Flag)
        {
            METAOPTION.DAL.VendorDAL objvendorDAL = new VendorDAL();
            objvendorDAL.UpdateExpenseAutoApprovalFlag(VendorID, Flag);
        }
        #endregion

        #region[Delete & Archive Vendor]
        public static void DeleteArchiveVendor(int Status, Int64 VendorID, Int64 UserID)
        {
            METAOPTION.DAL.VendorDAL objvendorDAL = new VendorDAL();
            objvendorDAL.DeleteArchiveVendor(Status, VendorID, UserID);
        }
        #endregion

        #region [Get the Vendor list for Transport Zone Price]
        public List<TransportationPriceLookUp_VendorResult> GetAllVendor(Int32 EntityID, Int32 EntityTypeID, short OrgId)
        {
            METAOPTION.DAL.VendorDAL objvendorDAL = new VendorDAL();
            return objvendorDAL.GetAllVendor(EntityID, EntityTypeID, OrgId);
        }
        #endregion

        #region [Get the Zone list for Transport Zone Price]
        public List<TransportationPriceLookUp_ZoneResult> GetAllZone(Int32 EntityID, Int32 EntityTypeID, short OrgId)
        {
            METAOPTION.DAL.VendorDAL objvendorDAL = new VendorDAL();
            return objvendorDAL.GetAllZone(EntityID, EntityTypeID, OrgId);
        }
        #endregion

        #region [Get the  Transport Zone Price list]
        public List<TransportationPriceLookUp_SelectResult> GetAllZonePrice(Int32 EntityID, Int32 EntityTypeID, String Zone, String Mileage, short OrgID)
        {
            METAOPTION.DAL.VendorDAL objvendorDAL = new VendorDAL();
            return objvendorDAL.GetAllZonePrice(EntityID, EntityTypeID, Zone, Mileage, OrgID);
        }
        
        #endregion
    }
}
