using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace METAOPTION.DAL
{
    public class Admin_ManageEntitiesDAL
    {

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
            METAOPTION.Admin_DALDataContext.Factory.DB.VendorDetailsInsert(ref VenderId, objVendor.VendorName, objVendor.VendorDIN, objVendor.VendorCategoryId, objVendor.TaxIdNumber,
                objVendor.VendorTypeId, objVendor.PaymentTermId,
                //objVendor.AccountingCode,
                objVendor.Comments, objVendor.AddedBy, objVendor.TransExpenseCalculationMethod, objVendor.OrgID,
                objAddress.Street, objAddress.Suite, objAddress.City, objAddress.StateId, objAddress.CountryId, objAddress.Zip, objAddress.Phone1,
                objAddress.Phone1Ext, objAddress.Phone2, objAddress.Phone2Ext, objAddress.Fax, objAddress.Email1,
                objAddress.Email2);
            return VenderId.Value;
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
        public long AddUlilityCompany(UtilityCompany objUtilityCompany, Address objAddress)
        {
            Nullable<long> UtilityCompId = null;
            METAOPTION.Admin_DALDataContext.Factory.DB.UtilityCompanyInsert(ref UtilityCompId
                , objUtilityCompany.CompanyName
                , objUtilityCompany.TaxIdNumber
                , objUtilityCompany.CompanyCategoryId
                , objUtilityCompany.PayementFrequencyId
                , objUtilityCompany.AccountNumber
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

        public long AddEmployee(Employee objEmployee, Address objAddress)
        {
            Nullable<long> empId = null;
            long employeeId = 0;
            METAOPTION.Admin_DALDataContext.Factory.DB.EmployeeInsert
            (
             ref empId
            , objEmployee.TitleId
            , objEmployee.FirstName
            , objEmployee.MiddleName
            , objEmployee.LastName
            , objEmployee.EmployeeCode
            , objEmployee.EmployeeTypeId
            , objEmployee.CellPhone
            , objEmployee.DriverLicenseStateId
            , objEmployee.DriverLicenseNumber
            , objEmployee.DriverLicensExpDate
            , objEmployee.SpecialPayrollConditions
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
            , objAddress.Email2
            , objEmployee.DateAdded
            , objEmployee.AddedBy
            , objEmployee.IsActive
            , objEmployee.OrgID
            );
            if (empId.HasValue)
                employeeId = empId.Value;
            return employeeId;
        }

        #region[Get Vendor List along with Search]
        public IEnumerable GetVendorList(string vendorName, Int32 categoryId, Int32 typeId, string city, Int32 countryId, Int32 stateId, string zip, Int32 Status, Int16 OrgID)
        {
            return METAOPTION.Admin_DALDataContext.Factory.DB.VendorSearchList(vendorName, categoryId, typeId, city, countryId, stateId, zip, Status, OrgID).AsEnumerable();
        }
        #endregion


        public DataTable EntitySearch(Int16 OrgID, Int32 EntityTypeID, String EntityName, String City, Int32 CountryID, Int32 StateID, String ZipCode, Int32 ActiveStatus)
        {
            DataTable dt = new DataTable();
            Admin_DALDataContext context = new Admin_DALDataContext();
            using (SqlConnection con = new SqlConnection(context.Connection.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("Admin_EntitySearch", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EntityName", EntityName);
                cmd.Parameters.AddWithValue("@City", City);
                cmd.Parameters.AddWithValue("@CountryID", CountryID);
                cmd.Parameters.AddWithValue("@StateID", StateID);
                cmd.Parameters.AddWithValue("@ZipCode", ZipCode);
                cmd.Parameters.AddWithValue("@ActiveStatus", ActiveStatus);
                cmd.Parameters.AddWithValue("@OrgID", OrgID);
                cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                context.Dispose();
            }

            return dt;
        }

        public Int32 UpdateEntityStatus(Int16 OrgID, Int32 EntityTypeID, Int64 EntityID, Int32 Status)
        {
            Int32 count = 0;
            count = METAOPTION.Admin_DALDataContext.Factory.DB.Admin_UpdateEntityStatus(OrgID, EntityTypeID, EntityID, Status);
            return count;
        }
    }
}
