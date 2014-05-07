using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using METAOPTION;

namespace METAOPTION.DAL
{
    public class EmployeeDAL
    {
        /// <summary>
        /// This method Add new record in Employee table and also add employee address details in address table and return employee id generated
        /// </summary>
        /// <param name="objEmployee"></param>
        /// <returns>System generated Employee Id </returns>
        public long AddEmployee(Employee objEmployee, Address objAddress)
        {
            Nullable<long> empId = null;
            long employeeId = 0;
            METAOPTION.DALDataContext.Factory.DB.EmployeeInsert
            (
             ref empId
            , objEmployee.TitleId
            , objEmployee.FirstName
            , objEmployee.MiddleName
            , objEmployee.LastName
            , objEmployee.EmployeeCode
            , objEmployee.EmployeeTypeId
                // , objEmployee.AccountingCode
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
            //Check if any value returned
            if (empId.HasValue)
                employeeId = empId.Value;

            return employeeId;

        }

        #region[Get Details of an employee]
        public IQueryable GetEmployeeDetails(long EmployeeId,Int16 OrgID)
        {
            return METAOPTION.DALDataContext.Factory.DB.GetEmployeeDetails(EmployeeId, OrgID).AsQueryable();
        }
        #endregion


        #region[Get Employee List along with Search]
        /// <summary>
        /// get list of employee alon with search 
        /// condition
        /// </summary>
        /// <param name="empName"></param>
        /// <param name="typeId"></param>
        /// <param name="city"></param>
        /// <param name="countryId"></param>
        /// <param name="stateId"></param>
        /// <param name="zip"></param>
        /// <returns></returns>
        public IEnumerable GetEmployeeList(string empName, Int32 typeId, string city, Int32 countryId, Int32 stateId, string zip, Int32 Status, Int16 OrgID)
        {
            return METAOPTION.DALDataContext.Factory.DB.EmployeeListSearch(empName, typeId, city, countryId, stateId, zip, Status,OrgID).AsEnumerable();
        }
        #endregion

        #region[Update Employee Details along address details]
        public int UpdateEmployeeDetails(Employee objEmployee, Address objAddress)
        {
            return METAOPTION.DALDataContext.Factory.DB.EmployeeUpdate(objEmployee.EmployeeId
                 , objEmployee.TitleId
                 , objEmployee.FirstName
                 , objEmployee.MiddleName
                 , objEmployee.LastName
                 , objEmployee.EmployeeCode
                 , objEmployee.EmployeeTypeId
                // , objEmployee.AccountingCode
                 , objEmployee.CellPhone
                 , objEmployee.DriverLicenseStateId
                 , objEmployee.DriverLicenseNumber
                 , objEmployee.DriverLicensExpDate
                 , objEmployee.SpecialPayrollConditions
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
                 , objAddress.Email2
                 , objAddress.ModifiedBy);
        }
        #endregion

        #region[Delete & Archive Employee]
        public void DeleteArchiveEmployee(int Status, Int64 EmployeeID, Int64 UserID)
        {
            int i = METAOPTION.DALDataContext.Factory.DB.DeleteArchiveEmployee(Status, EmployeeID, UserID);
        }
        #endregion
    }
}
