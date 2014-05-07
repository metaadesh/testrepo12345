using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace METAOPTION
{
    public class CacheEnum
    {
        static string pagePermissions = "PagePermissions";
        static string yMM = "YMM";
        static string country = "Country";
        static string trans = "Trans";
        static string engines = "Engines";
        static string designation = "Designation";
        static string buyers = "Buyers";
        static string dealer = "Dealer";
        static string vendor = "Vendor";
        static string lastLogin = "LastLogin";
        static string loginHistory = "LoginHistory";
        static string announcement = "Announcement";
        static string commissionType = "CommissionType";
        static string buyerAnnouncement = "BuyerAnnouncement";

        //Modified By Prem(8800128549), Add the logic for caching organization wise, Create the read only Properties for all the fields

        public static string PagePermissions
        {
            get
            {
                return pagePermissions + "_" + HttpContext.Current.Session["OrgID"].ToString();
            }
        }

        public static string YMM
        {
            get
            {
                return yMM + "_" + HttpContext.Current.Session["OrgID"].ToString();
            }
        }

        public static string Country
        {
            get
            {
                return country + "_" + HttpContext.Current.Session["OrgID"].ToString();
            }
        }

        public static string Trans
        {
            get
            {
                return trans + "_" + HttpContext.Current.Session["OrgID"].ToString();
            }
        }

        public static string Engines
        {
            get
            {
                return engines + "_" + HttpContext.Current.Session["OrgID"].ToString();
            }
        }

        public static string Designation
        {
            get
            {
                return designation + "_" + HttpContext.Current.Session["OrgID"].ToString();
            }
        }

        public static string Buyers
        {
            get
            {
                return buyers + "_" + HttpContext.Current.Session["OrgID"].ToString();
            }
        }

        public static string Dealer
        {
            get
            {
                return dealer + "_" + HttpContext.Current.Session["OrgID"].ToString();
            }
        }

        public static string Vendor
        {
            get
            {
                return vendor + "_" + HttpContext.Current.Session["OrgID"].ToString();
            }
        }

        public static string LastLogin
        {
            get
            {
                return lastLogin + "_" + HttpContext.Current.Session["OrgID"].ToString();
            }
        }

        public static string LoginHistory
        {
            get
            {
                return loginHistory + "_" + HttpContext.Current.Session["OrgID"].ToString();
            }
        }

        public static string Announcement
        {
            get
            {
                return announcement + "_" + HttpContext.Current.Session["OrgID"].ToString();
            }
        }

        public static string CommissionType
        {
            get
            {
                return commissionType + "_" + HttpContext.Current.Session["OrgID"].ToString();
            }
        }

        public static string BuyerAnnouncement
        {
            get
            {
                return buyerAnnouncement + "_" + HttpContext.Current.Session["OrgID"].ToString();
            }
        }

    }
}
