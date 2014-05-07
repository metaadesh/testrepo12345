using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Data;
using System.Text;
using METAOPTION;
using METAOPTION.DAL;
using System.Web;

namespace METAOPTION.BAL
{
    public class ViewAnnouncementBAL
    {
        static Double ShortCacheExpiry = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["ShortCacheExpiry"]);
        static Double LongCacheExpiry = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["LongCacheExpiry"]);

        #region [Get Announcement]
        public List<string> GetAnnouncement(Int64 AnnouncementID)
        {
            ViewAnnouncementDAL objDAL = new ViewAnnouncementDAL();
            var result = objDAL.GetAnnouncement(AnnouncementID).AsQueryable();
            List<string> lstLaneAnnouncement = new List<string>();
            foreach (GetAnnouncementResult objResult in result)
            {
                //AnnouncementTitle at Index 0
                if (objResult.AnnouncementTitle != null)
                {
                    lstLaneAnnouncement.Add(objResult.AnnouncementTitle.ToString());
                }
                else
                    lstLaneAnnouncement.Add("N/A");
                //Description at Index 1
                if (objResult.Description != null)
                {
                    lstLaneAnnouncement.Add(objResult.Description.ToString());
                }
                else
                    lstLaneAnnouncement.Add("N/A");
                //AnnouncementType at Index 2

                if (objResult.AnnouncementType != null)
                {
                    lstLaneAnnouncement.Add(objResult.AnnouncementType.ToString());
                }
                else
                    lstLaneAnnouncement.Add("N/A");

                //DateAdded at Index 3
                if(objResult.DateAdded != null)
                {
                lstLaneAnnouncement.Add(objResult.DateAdded.ToString());
                }
                else
                    lstLaneAnnouncement.Add("N/A");
                //AddedByName at Index 4
                if (objResult.AddedByName != null)
                {
                    lstLaneAnnouncement.Add(objResult.AddedByName.ToString());
                }
                else
                    lstLaneAnnouncement.Add("N/A");

            }
            return lstLaneAnnouncement;
        }
        #endregion

        #region [Get Announcement List details]
        public DataTable GetAnnouncementList(Int16 OrgID)
        {
            #region[OldCode]
            //return ViewAnnouncementDAL.GetAnnouncementList();
            #endregion

            if (HttpContext.Current.Cache[CacheEnum.Announcement] == null)
            {
                DataTable dt = new DataTable();
                dt = ViewAnnouncementDAL.GetAnnouncementList(OrgID);
                HttpContext.Current.Cache.Insert(CacheEnum.Announcement, dt, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(ShortCacheExpiry));
                return dt;
            }
            else
                return (DataTable)HttpContext.Current.Cache[CacheEnum.Announcement];
        }
        #endregion

        #region[Added by Rupendra 18 Jan 13 for fetch Buyer Announcement] 
        public DataTable GetAnnouncementList_Buyer_Ver211(Int32 SecurityUserId)
        {           
            if (HttpContext.Current.Cache[CacheEnum.BuyerAnnouncement] == null)
            {
                DataTable dt = new DataTable();
                dt = ViewAnnouncementDAL.GetAnnouncementList_Buyer_Ver211(SecurityUserId);
                HttpContext.Current.Cache.Insert(CacheEnum.BuyerAnnouncement, dt, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(ShortCacheExpiry));
                return dt;
            }
            else
                return (DataTable)HttpContext.Current.Cache[CacheEnum.BuyerAnnouncement];
        }
        #endregion
    }
}
