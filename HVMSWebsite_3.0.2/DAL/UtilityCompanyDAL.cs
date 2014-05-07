using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace METAOPTION.DAL
{
    public class UtilityCompanyDAL
    {
        #region[Add Utility Company along with address Details]
        /// <summary>
        /// add utility company details
        /// along with address details
        /// </summary>
        /// <param name="objUtilityCompany"></param>
        /// <param name="objAddress"></param>
        /// <returns></returns>
        public long AddUlilityCompany(UtilityCompany objUtilityCompany, Address objAddress)
        {
            Nullable<long> UtilityCompId = null;
            METAOPTION.DALDataContext.Factory.DB.UtilityCompanyInsert(ref UtilityCompId
                , objUtilityCompany.CompanyName
                , objUtilityCompany.TaxIdNumber
                , objUtilityCompany.CompanyCategoryId
                , objUtilityCompany.PayementFrequencyId
                , objUtilityCompany.AccountNumber
                //, objUtilityCompany.AccountingCode
                , objUtilityCompany.Comments
                , objUtilityCompany.AddedBy
                , objUtilityCompany.OrgID
                , objAddress.Street
                , objAddress.Suite
                , objAddress.City
                , objAddress.StateId
                , objAddress.CountryId
                , objAddress.Zip
                , objAddress.Phone1
                , objAddress.Phone1Ext
                , objAddress.Phone2
                , objAddress.Phone2Ext
                , objAddress.Fax
                , objAddress.Email1
                , objAddress.Email2);
            return UtilityCompId.Value;
        }
        #endregion

        #region[Get Utility Company Details]
        /// <summary>
        /// get details of utility company
        /// by utility Company Id.
        /// </summary>
        /// <param name="utiCompanyId"></param>
        /// <returns></returns>
        public IQueryable GetCompanyDetailsByCompanyId(long utiCompanyId,Int16 OrgID)
        {
            return METAOPTION.DALDataContext.Factory.DB.GetCompanyDetailsById(utiCompanyId, OrgID).AsQueryable();
        }
        #endregion

        #region[Get Company List]
        /// <summary>
        /// get utility company list
        /// along with search condition
        /// </summary>
        /// <param name="comName"></param>
        /// <param name="category"></param>
        /// <param name="payFrequency"></param>
        /// <param name="city"></param>
        /// <param name="countryId"></param>
        /// <param name="stateId"></param>
        /// <param name="zip"></param>
        /// <returns></returns>
        public IEnumerable GetUtilityCompanyList(string comName, Int32 category, Int32 payFrequency, string city, Int32 countryId, Int32 stateId, string zip, Int32 Status, Int16 OrgID)
        {
            return METAOPTION.DALDataContext.Factory.DB.CompanySearchList(comName, category, payFrequency, city, countryId, stateId, zip, Status,OrgID).AsEnumerable();
        }
        #endregion

        #region[Update Utility Company Details]
        public int UpdateUtilityCompanyDetails(UtilityCompany objUtilityCompany, Address objAddress)
        {
            return METAOPTION.DALDataContext.Factory.DB.UtilityCompanyUpdate(objUtilityCompany.UtilityCompanyId
                 , objUtilityCompany.CompanyName
                 , objUtilityCompany.TaxIdNumber
                 , objUtilityCompany.CompanyCategoryId
                 , objUtilityCompany.PayementFrequencyId
                 , objUtilityCompany.AccountNumber
                //, objUtilityCompany.AccountingCode
                 , objUtilityCompany.Comments
                 , objUtilityCompany.ModifiedBy
                 , objAddress.AddressId
                 , objAddress.Street
                 , objAddress.Suite
                 , objAddress.City
                 , objAddress.StateId
                 , objAddress.CountryId
                 , objAddress.Zip
                 , objAddress.Phone1
                 , objAddress.Phone1Ext
                 , objAddress.Phone2
                 , objAddress.Phone2Ext
                 , objAddress.Fax
                 , objAddress.Email1
                 , objAddress.Email2);
        }
        #endregion


        #region[Delete & Archive Vendor]
        public void DeleteArchiveUtilityCompany(int Status, Int64 UtilityCompanyId, Int64 UserID)
        {
            int i = METAOPTION.DALDataContext.Factory.DB.DeleteArchiveUtilityCompany(Status, UtilityCompanyId, UserID);
        }
        #endregion
    }
}
