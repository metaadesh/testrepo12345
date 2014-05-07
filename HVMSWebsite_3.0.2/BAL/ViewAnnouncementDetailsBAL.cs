using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using METAOPTION;
using METAOPTION.DAL;
namespace METAOPTION.BAL
{
    public class ViewAnnouncementDetailsBAL
    {
        ViewAnnouncementDetailsDAL objDAL = new ViewAnnouncementDetailsDAL();
        //public IEnumerable GetAnnouncementDetails(Int64 AnnouncementId)
        //{
        //    return objDAL.GetAnnouncementDetails(AnnouncementId);
        //}
        #region [ Get Announcement Details]
        public IEnumerable GetAnnouncementDetails(Int64 AnnouncementId)
        {
            return objDAL.GetAnnouncementDetails(AnnouncementId);

        }
        public IEnumerable SelectAnnouncementDetails(Int64 AnnouncementId)
        {
            return objDAL.SelectAnnouncementDetails(AnnouncementId);

        }
        #endregion

       

        #region [ Commissions Calculated ]
        public IEnumerable GetCommissionsCalculated(Int64 AnnouncementId)
        {
            return objDAL.GetCommissionsCalculated(AnnouncementId);

        }
        #endregion

        #region [ Get Chrome Updates]
        public IEnumerable GetChromeUpdates(Int64 AnnouncementId)
        {
            return objDAL.GetChromeUpdates(AnnouncementId);

        }
        #endregion


        #region[Added by Rupendra 18 Jan 2013]
        public IEnumerable SelectAnnouncementDetails_Ver211(Int64 AnnouncementId, Int64 SecurityUserId)
        {
            return objDAL.SelectAnnouncementDetails_Ver211(AnnouncementId, SecurityUserId);

        }
        public IEnumerable GetCommissionsCalculated_Ver211(Int64 AnnouncementId, Int64 SecurityUserId)
        {
            return objDAL.GetCommissionsCalculated_Ver211(AnnouncementId, SecurityUserId);

        }

        public IEnumerable GetChromeUpdates_Ver211(Int64 AnnouncementId, Int64 SecurityUserId)
        {
            return objDAL.GetChromeUpdates_Ver211(AnnouncementId, SecurityUserId);

        }
        #endregion

    }
}
