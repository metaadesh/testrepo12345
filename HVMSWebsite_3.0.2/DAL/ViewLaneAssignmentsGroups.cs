using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using METAOPTION;
namespace METAOPTION.DAL
{
    public class ViewLaneAssignmentsGroupsDAL
    {
        DALDataContext objDAL = new DALDataContext();
        #region"[Select LaneAssignments Groups]"
        /// <summary>
        /// Select Lane Assignments Groups lists
        /// </summary>
        /// <returns></returns>
        public IQueryable SelectLaneAssignmentsGroups()
        {
            IQueryable result = objDAL.GetLaneAssignmentsGroups().AsQueryable();
            

            return result;
        }
        #endregion
    }
}
