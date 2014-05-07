using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using METAOPTION;
namespace METAOPTION.DAL
{
    public class ViewAnnouncementDetailsDAL
    {
        DALDataContext objDAL = new DALDataContext();
        //public IEnumerable GetAnnouncementDetails(Int64 AnnouncementId)
        //{
        //    return objDAL.GetAnnouncementDetails(AnnouncementId);
        //}
        #region [ Get Announcement details]
        /// <summary>
        /// Get Announcement Details
        /// </summary>
        /// <param name="AnnouncementId">AnnouncementId</param>
        /// <returns>Announcement Details</returns>
        public IEnumerable GetAnnouncementDetails(Int64 AnnouncementId)
        {
            return objDAL.GetAnnouncementDetails(AnnouncementId).AsEnumerable();
        }
        /// <summary>
        /// Select Announcement Details
        /// </summary>
        /// <param name="AnnouncementId">AnnouncementId</param>
        /// <returns></returns>
        public IEnumerable SelectAnnouncementDetails(Int64 AnnouncementId)
        {
            return objDAL.GetAnnouncementDetails(AnnouncementId).AsEnumerable();

        }
        #endregion



        #region [ Commissions Calculated ]
        /// <summary>
        /// Commissions Calculated
        /// </summary>
        /// <param name="AnnouncementId">AnnouncementId</param>
        /// <returns></returns>
        public IEnumerable GetCommissionsCalculated(Int64 AnnouncementId)
        {
            return objDAL.GetCommissionsCalculated(AnnouncementId).AsEnumerable();

        }
        #endregion


        #region [ Get Chrome Updates]
        /// <summary>
        /// Get Chrome Updates details
        /// </summary>
        /// <param name="AnnouncementId">AnnouncementId</param>
        /// <returns></returns>
        public IEnumerable GetChromeUpdates(Int64 AnnouncementId)
        {
            return objDAL.GetChromeUpdates(AnnouncementId).AsEnumerable();

        }
        #endregion


        #region [Added by Rupendra 18 Jan 2013]
        public IEnumerable SelectAnnouncementDetails_Ver211(Int64 AnnouncementId, Int64 SecurityUserId)
        {
            return objDAL.GetAnnouncementDetails_Ver211(AnnouncementId, SecurityUserId).AsEnumerable();

        }
        public IEnumerable GetCommissionsCalculated_Ver211(Int64 AnnouncementId, Int64 SecurityUserId)
        {
            return objDAL.GetCommissionsCalculated_Ver211(AnnouncementId, SecurityUserId).AsEnumerable();

        }

        public IEnumerable GetChromeUpdates_Ver211(Int64 AnnouncementId, Int64 SecurityUserId)
        {
            return objDAL.GetChromeUpdates_Ver211(AnnouncementId, SecurityUserId).AsEnumerable();

        }
        #endregion

    }
}
