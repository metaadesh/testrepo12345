using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION.DAL;
namespace METAOPTION.BAL
{
    public class ViewCountRecoredSinceLastLoginBAL
    {
        ViewCountRecoredSinceLastLoginDAL objDAL = new ViewCountRecoredSinceLastLoginDAL();
        public List<string> GetCountsLastLogin(Int64 UserId)
        {
            var result = objDAL.GetCountsLastLogin(UserId).AsQueryable();
            List<string> lstCount = new List<string>();
            int iCount = 0;
            foreach (Get10RecordsForAnnouncement_EmailResult objResult in result)
            {
                iCount += 1;
            }
            lstCount.Add(iCount.ToString());
            return lstCount;
        }
        public int Ge10RecordsCount(Int64 addedBy)
        {
            return objDAL.Get10Counts(addedBy);
        }

    }
}
