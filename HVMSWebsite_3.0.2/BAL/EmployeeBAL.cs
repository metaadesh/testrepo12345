using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION.DAL;
namespace METAOPTION.BAL
{
    public class EmployeeBAL
    {
        //<summary>
        //This method call method of DAL to ADD NEW Employee entry in db along with address details
        //</summary>
        //<param name="objEmployee"></param>
        //<returns>Employee Id generated</returns>
        public static long AddEmployee(Employee objEmployee, Address objAddress)
        {
            METAOPTION.DAL.EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.AddEmployee(objEmployee, objAddress);
        }

        #region[Get Details of an employee]
        public static IQueryable GetEmployeeDetails(long EmployeeId,Int16 OrgID)
        {
            METAOPTION.DAL.EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.GetEmployeeDetails(EmployeeId,OrgID);
        }
        #endregion

        //#region[Get Comments for Etitty]
        //public IEnumerable GetCommentsForEntity(long entityId, Int32 entityTypeId)
        //{
        //    return objDAL.GetCommentsForEntity(entityId, entityTypeId);
        //}
        //#endregion

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
        public static IEnumerable GetEmployeeList(string empName, Int32 typeId, string city, Int32 countryId, Int32 stateId, string zip, Int32 Status, Int16 OrgID)
        {
            METAOPTION.DAL.EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.GetEmployeeList(empName, typeId, city, countryId, stateId, zip, Status, OrgID);
        }
        #endregion

        #region[Update Employee Details along address details]
        public static int UpdateEmployeeDetails(Employee objEmployee, Address objAddress)
        {
            METAOPTION.DAL.EmployeeDAL objDAL = new EmployeeDAL();
            return objDAL.UpdateEmployeeDetails(objEmployee, objAddress);
        }
        #endregion


        #region[Delete & Archive Vendor]
        public static void DeleteArchiveEmployee(int Status, Int64 EmployeeID, Int64 UserID)
        {
            METAOPTION.DAL.EmployeeDAL objEmployeeDAL = new EmployeeDAL();
            objEmployeeDAL.DeleteArchiveEmployee(Status, EmployeeID, UserID);
        }
        #endregion
    }
}
