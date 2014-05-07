using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION.DAL;
namespace METAOPTION.BAL
{
    public class UpdateLaneAssignmentsBAL
    {

        UpdateLaneAssignmentsDAL objLA = new UpdateLaneAssignmentsDAL();
        /// <summary>
        /// Update Lane Assignments
        /// </summary>
        /// <param name="obj">obj</param>
        /// <returns></returns>
        public long UpdateLaneAssignments(Inventory obj)
        {
            return objLA.UpdateLaneAssignments(obj);
        }
    }
}
