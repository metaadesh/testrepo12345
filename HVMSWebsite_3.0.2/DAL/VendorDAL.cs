using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using METAOPTION;
using METAOPTION.DAL;
namespace METAOPTION.DAL
{
    public class VendorDAL
    {
        #region[For Add Screen]
        #region [Add vender Details]
        /// <summary>
        /// this is the region for
        /// add vender details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public long AddVenderDetails(Vendor objVendor, Address objAddress)
        {
            Nullable<long> VenderId = null;
            METAOPTION.DALDataContext.Factory.DB.VendorDetailsInsert(ref VenderId, objVendor.VendorName, objVendor.VendorDIN, objVendor.VendorCategoryId, objVendor.TaxIdNumber,
                objVendor.VendorTypeId, objVendor.PaymentTermId,
                //objVendor.AccountingCode,
                objVendor.Comments, objVendor.AddedBy,objVendor.TransExpenseCalculationMethod,objVendor.OrgID,
                objAddress.Street, objAddress.Suite, objAddress.City, objAddress.StateId, objAddress.CountryId, objAddress.Zip, objAddress.Phone1,
                objAddress.Phone1Ext, objAddress.Phone2, objAddress.Phone2Ext, objAddress.Fax, objAddress.Email1,
                objAddress.Email2);
            return VenderId.Value;
        }
        #endregion
        #endregion

        #region[For View Screen]
        #region[Get Vendor Details]
        public IQueryable GetVendorDetails(long VendorId)
        {
            IQueryable result = METAOPTION.DALDataContext.Factory.DB.GetVendorDetailsByVendorId(VendorId).AsQueryable();
            return result;
        }
        #endregion

        #region[Get Vendor List along with Search]
        public IEnumerable GetVendorList(string vendorName, Int32 categoryId, Int32 typeId, string city, Int32 countryId, Int32 stateId, string zip,Int32 Status,Int16 OrgID)
        {
            return METAOPTION.DALDataContext.Factory.DB.VendorSearchList(vendorName, categoryId, typeId, city, countryId, stateId, zip, Status, OrgID).AsEnumerable();
        }
        #endregion

        #region [Get Vendors List]
        //public IEnumerable GetVendorList()
        //{
        //    return METAOPTION.DALDataContext.Factory.DB.GetVendorList().AsEnumerable();

        //}

        public List<GetVendorListResult> GetVendorList(short OrgID)
        {
            List<GetVendorListResult> result = METAOPTION.DALDataContext.Factory.DB.GetVendorList(OrgID).ToList<GetVendorListResult>();
            return result;
        }
        #endregion

        #region[Update Vendor Details]
        public int UpdateVendorDetails(Vendor objVendor, Address objAddress)
        {
            int result = METAOPTION.DALDataContext.Factory.DB.VendorDetailsUpdate(objVendor.VendorId, objVendor.VendorName, objVendor.VendorDIN, objVendor.VendorCategoryId,
                objVendor.TaxIdNumber, objVendor.VendorTypeId, objVendor.PaymentTermId,
                //objVendor.AccountingCode,
                objVendor.Comments, objVendor.ModifiedBy, objVendor.TransExpenseCalculationMethod,
                objAddress.AddressId, objAddress.Street, objAddress.Suite, objAddress.City,
                objAddress.StateId, objAddress.CountryId, objAddress.Zip, objAddress.Phone1, objAddress.Phone1Ext, objAddress.Phone2,
                objAddress.Phone2Ext, objAddress.Fax, objAddress.Email1, objAddress.Email2);

            return result;
        }
        #endregion

        #region[Update ExpenseAutoApproval Flag]
        public void UpdateExpenseAutoApprovalFlag(Int64 VendorID, bool Flag)
        {
            int i = METAOPTION.DALDataContext.Factory.DB.Mobile_UpdateExpenseAutoApproval(VendorID, Flag);
        }
        #endregion

        #region[Delete & Archive Vendor]
        public void DeleteArchiveVendor(int Status, Int64 VendorID, Int64 UserID)
        {
            int i = METAOPTION.DALDataContext.Factory.DB.DeleteArchiveVendor(Status, VendorID, UserID);
        }
        #endregion

        #endregion

        #region [Get the Vendor list for Transport Zone Price]
        public List<TransportationPriceLookUp_VendorResult> GetAllVendor(Int32 EntityID, Int32 EntityTypeID, short OrgId)
        {
            return METAOPTION.DALDataContext.Factory.DB.TransportationPriceLookUp_Vendor(EntityID, EntityTypeID, OrgId).ToList<TransportationPriceLookUp_VendorResult>();
        }
        #endregion

        #region [Get the Zone list for Transport Zone Price]
        public List<TransportationPriceLookUp_ZoneResult> GetAllZone(Int32 EntityID, Int32 EntityTypeID, short OrgId)
        {
            return METAOPTION.DALDataContext.Factory.DB.TransportationPriceLookUp_Zone(EntityID, EntityTypeID, OrgId).ToList<TransportationPriceLookUp_ZoneResult>();
        }
        #endregion

        #region [Get the  Transport Zone Price list]
        public List<TransportationPriceLookUp_SelectResult> GetAllZonePrice(Int32 EntityID, Int32 EntityTypeID, String Zone, String Mileage, short OrgID)
        {
            return METAOPTION.DALDataContext.Factory.DB.TransportationPriceLookUp_Select(EntityID, EntityTypeID, Zone, Mileage, OrgID).ToList<TransportationPriceLookUp_SelectResult>();
        }
       
        #endregion
    }
}
