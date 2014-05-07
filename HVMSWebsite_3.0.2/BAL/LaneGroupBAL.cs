using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION.DAL;
using System.Data;

using System.Data.SqlClient;
namespace METAOPTION.BAL
{
    public class LaneGroupBAL
    {
        LaneGroupDAL objD = new LaneGroupDAL();
        /// <summary>
        /// Ge tLane Groups
        /// </summary>
        /// <returns></returns>
        public DataTable GetLaneGroups()
        {
           DataTable dt= objD.GetLaneGroups();
           return dt;
        }
    }
}
