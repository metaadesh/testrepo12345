using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION.DAL;
using System.Collections;
using System.Data;

namespace METAOPTION.BAL
{
    public class Admin_ManageEntitiesBAL
    {
        Admin_ManageEntitiesDAL obj = new Admin_ManageEntitiesDAL();

        #region [Add vender details]
        /// <summary>
        /// region for add vender details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long AddVenderDetails(Vendor objVendor, Address objAddress)
        {
            METAOPTION.DAL.Admin_ManageEntitiesDAL objvendorDAL = new Admin_ManageEntitiesDAL();
            return objvendorDAL.AddVenderDetails(objVendor, objAddress);
        }
        #endregion

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
            METAOPTION.DAL.Admin_ManageEntitiesDAL objUtilityCompDAL = new Admin_ManageEntitiesDAL();
            return objUtilityCompDAL.AddUlilityCompany(objUtilityCompany, objAddress);
        }
        #endregion

        public static long AddEmployee(Employee objEmployee, Address objAddress)
        {
            METAOPTION.DAL.Admin_ManageEntitiesDAL objDAL = new Admin_ManageEntitiesDAL();
            return objDAL.AddEmployee(objEmployee, objAddress);
        }

        #region[Get Vendor List along with Search]
        public static IEnumerable GetVendorList(string vendorName, Int32 categoryId, Int32 typeId, string city, Int32 countryId, Int32 stateId, string zip, Int32 Status, Int16 OrgID)
        {
            METAOPTION.DAL.Admin_ManageEntitiesDAL objvendorDAL = new Admin_ManageEntitiesDAL();
            return objvendorDAL.GetVendorList(vendorName, categoryId, typeId, city, countryId, stateId, zip, Status, OrgID);
        }
        #endregion

        public DataTable EntitySearch(Int16 OrgID, Int32 EntityTypeID, String EntityName, String City, Int32 CountryID, Int32 StateID, String ZipCode, Int32 ActiveStatus)
        {
            return obj.EntitySearch(OrgID, EntityTypeID, EntityName, City, CountryID, StateID, ZipCode, ActiveStatus);
        }

        public Int32 UpdateEntityStatus(Int16 OrgID, Int32 EntityTypeID, Int64 EntityID, Int32 Status)
        {
            return obj.UpdateEntityStatus(OrgID, EntityTypeID, EntityID, Status);
        }

    }
}
