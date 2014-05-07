using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using METAOPTION.DAL;
using METAOPTION;
namespace METAOPTION.BAL
{
    public class ViewLaneAssignmentsGroupsBAL
    {
        ViewLaneAssignmentsGroupsDAL objDAL = new ViewLaneAssignmentsGroupsDAL();
        #region "[Select LaneAssignments Groups]"
        public IQueryable SelectLaneAssignmentsGroups()
        {
            return objDAL.SelectLaneAssignmentsGroups();
        }
        #endregion
    }
}
