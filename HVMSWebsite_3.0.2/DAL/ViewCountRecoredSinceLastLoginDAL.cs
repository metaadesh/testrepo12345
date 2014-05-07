using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION;
namespace METAOPTION.BAL
{
    public class ViewCountRecoredSinceLastLoginDAL
    {
        DALDataContext objDAL = new DALDataContext();

        #region "Get count for announcement & email are pending since last login"
        /// <summary>
        /// Get count for announcement & email are pending since last login
        /// </summary>
        /// <param name="UserId"> pass user id </param>
        /// <returns> returns pending count</returns>
        public IQueryable GetCountsLastLogin(Int64 UserId)
        {
            return objDAL.Get10RecordsForAnnouncement_Email(UserId).AsQueryable();
        }
        #endregion
        public int Get10Counts(Int64 Userid)
        {
            var result = (from p in objDAL.vwGet10RecordSinceLastLogins
                          where p.ModifiedBy == Userid
                          select p).AsQueryable().Take(10);
            return result.Count();

        }
    }
}
