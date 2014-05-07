using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION.DAL;

namespace METAOPTION.BAL
{
    public class UtilityCompanyBAL
    {
        #region[Add Utility Company along with address Details]
        /// <summary>
        /// add utility company details
        /// along with address details
        /// </summary>
        /// <param name="objUtilityCompany"></param>
        /// <param name="objAddress"></param>
        /// <returns></returns>
        public static long AddUlilityCompany(UtilityCompany objUtilityCompany, Address objAddress)
        {
            METAOPTION.DAL.UtilityCompanyDAL objUtilityCompDAL = new UtilityCompanyDAL();
            return objUtilityCompDAL.AddUlilityCompany(objUtilityCompany, objAddress);
        }
        #endregion

        #region[Get Utility Company Details]
        /// <summary>
        /// get details of utility company
        /// by utility Company Id.
        /// </summary>
        /// <param name="utiCompanyId"></param>
        /// <returns></returns>
        public static IQueryable GetCompanyDetailsByCompanyId(long utiCompanyId,Int16 OrgID)
        {
            METAOPTION.DAL.UtilityCompanyDAL objUtilityCompDAL = new UtilityCompanyDAL();
            return objUtilityCompDAL.GetCompanyDetailsByCompanyId(utiCompanyId, OrgID);
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
        public static IEnumerable GetUtilityCompanyList(string comName, Int32 category, Int32 payFrequency, string city, Int32 countryId, Int32 stateId, string zip, Int32 Status,Int16 OrgID)
        {
            METAOPTION.DAL.UtilityCompanyDAL objUtilityCompDAL = new UtilityCompanyDAL();
            return objUtilityCompDAL.GetUtilityCompanyList(comName, category, payFrequency, city, countryId, stateId, zip, Status, OrgID);
        }
        #endregion

        #region[Update Utility Company Details]
        public static int UpdateUtilityCompanyDetails(UtilityCompany objUtilityCompany, Address objAddress)
        {
            METAOPTION.DAL.UtilityCompanyDAL objUtilityCompDAL = new UtilityCompanyDAL();
            return objUtilityCompDAL.UpdateUtilityCompanyDetails(objUtilityCompany, objAddress);
        }
        #endregion

        #region[Delete & Archive Vendor]
        public static void DeleteArchiveUtilityCompany(int Status, Int64 UtilityCompanyId, Int64 UserID)
        {
            METAOPTION.DAL.UtilityCompanyDAL objUtilityCompDAL = new UtilityCompanyDAL();
            objUtilityCompDAL.DeleteArchiveUtilityCompany(Status, UtilityCompanyId, UserID);
        }
        #endregion
    }
}
