using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using METAOPTION.DAL;

namespace METAOPTION.BAL
{
    public class ActivityBAL
    {
        ActivityDAL objDAL = new ActivityDAL();
        #region[Fetch Activity Stats]
        public DataTable FetchActivityStats(DateTime startDate, DateTime endDate)
        {
            return objDAL.FetchActivityStats(startDate, endDate);
        }
        #endregion

        #region[Insert record into Activity Stats table]
        public void InsertActivityStats(ActivityStat obj)
        {
            objDAL.InsertActivityStats(obj);
        }
        #endregion

        #region[Fetch Activity Stats - Location/Unresolved]
        public DataTable FetchLocationActivityStats()
        {
            return objDAL.FetchLocationActivityStats();
        }
        #endregion

        #region[Fetch Activity Stats Detail]
        public DataTable FetchActivityStatsDetail(DateTime startDate, DateTime endDate, String Code)
        {
            return objDAL.FetchActivityStatsDetail(startDate, endDate, Code);
        }
        #endregion
        #region[Fetch Activity Stats UnResolvedCar Detail]
        public DataTable GetActivityStatsUnResolvedCarDetail(String Code, Int32 StartRowIndex, Int32 MaximumRows)
        {
            return objDAL.GetUnResolvedCarDetail(Code, StartRowIndex, MaximumRows);
        
        }
        #endregion

        #region[Fetch Activity Stats Count]
        public Int32 GetActivityStatsDetailsCount(String Code, Int32 StartRowIndex, Int32 MaximumRows)
        {
            return objDAL.GetUnResolvedCarDetailCount(Code, StartRowIndex, MaximumRows);

        }
        #endregion
        #region[Fetch Activity Stats Location Detail]
        public DataTable GetActivityStatsLocationDetail(String Code, Int32 StartRowIndex, Int32 MaximumRows)
        {
            return objDAL.GetLocationStatsDetail(Code, StartRowIndex, MaximumRows);

        }
        #endregion

        #region[Fetch Activity Stats Location Count]
        public Int32 GetActivityStatsLocationDetailsCount(String Code, Int32 StartRowIndex, Int32 MaximumRows)
        {
            return objDAL.GetLocationStatsDetailCount(Code, StartRowIndex, MaximumRows);

        }
        #endregion





    }
}
