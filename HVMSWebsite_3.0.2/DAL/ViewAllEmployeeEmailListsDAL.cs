using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using METAOPTION;
namespace METAOPTION.DAL
{
    public class ViewAllEmployeeEmailListsDAL
    {
        DALDataContext objDal = new DALDataContext();
        #region " [ Get All Employee Email Lists] "
        //public IQueryable selectAllEmployeeEmailLists()
        //{
        //    IQueryable resut = objDal.GetAllEmployeeEmailLists().AsQueryable();
        //    return resut;
        //}
        /// <summary>
        /// select All Employee Email Lists
        /// </summary>
        /// <returns></returns>
        public IEnumerable selectAllEmployeeEmailLists(Int16 OrgID)
        {
            return objDal.GetAllEmployeeEmailLists(OrgID).AsEnumerable();
        }
        #endregion
    }
}
