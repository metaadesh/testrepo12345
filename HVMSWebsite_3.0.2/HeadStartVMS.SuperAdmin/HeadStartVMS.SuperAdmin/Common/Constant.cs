using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


namespace METAOPTION
{
    /// <summary>
    /// This interface applies to all screens for implmenting security rights
    /// </summary>
    public interface IPagePermission
    {
        /// <summary>
        /// Check User Level Page Permissions and Apply Permissions
        /// </summary>
        void CheckUserPagePermissions();
    }

    public partial class Constant
    {
        private static long liveusersHVMS = 0;
        private static long liveusersRAPID = 0;
        private static long liveusersDEMO = 0;

        public enum EntityType
        {
            Dealer_customer = 1,
            Buyer = 2,
            Vendor = 3,
            UtilityCompany_SubContractor = 4,
            Employee = 5,
            Inventory = 6
        };

        public enum AnnouncementType
        {
            Generic = 1,
            Lane = 2,
            Commision = 3,
            Chrome = 4,
        };

        public enum CommissionType
        {
            Fixed = 1,
            Percentage = 2
        };

        public enum ContactType
        {
            Regular = 1,
            Principal = 2,
            Other = 3,
            Manager = 4
        };

        public enum DealerType
        {
            Commercial = 1,
            Individual = 2,
            Other = 3
        };

        public enum DocumentType
        {
            Title_Shipped = 1,
            Titte_Present = 2,
            Purchased_Bill_Sale = 3,
            Shipped_Bill_Sale = 4,
            Other = 5,
            License = 20,
            Offer_Letter = 21,
            Leave_Application = 22
        };

        public enum EmployeeType
        {
            Driver = 1,
            Officer = 2,
            Accountant = 3,
            Clerk = 4,
            Other = 5
        };

        public enum VendorType
        {
            Cleaning_Chemicals = 1,
            Auto_Parts = 2,
            Mechanical_Repair_Service = 3,
            Body_Shop_Repair = 4,
            Others = 5
        };

        public static long UserId
        {
            get
            {
                long userId = -1;
                try
                {
                    if (HttpContext.Current.Session["empId"] == null)
                        HttpContext.Current.Response.Redirect("../Admin_Login.aspx");

                    userId = Convert.ToInt64(HttpContext.Current.Session["empId"]);
                }
                catch (Exception ex)
                {
                    userId = -1;
                }
                return userId;
            }
        }

        public static String UserDisplayName
        {
            get
            {
                String DisplayName = String.Empty; ;
                try
                {
                    DisplayName = HttpContext.Current.Session["disName"].ToString();
                }
                catch
                {
                    DisplayName = String.Empty;
                }
                return DisplayName;
            }
        }

        //public static int SystemID
        //{
        //    get
        //    {
        //        int sysId = -1;
        //        try
        //        {
        //            if (HttpContext.Current.Session["SystemId"] == null)
        //                HttpContext.Current.Response.Redirect("../Admin_Login.aspx");

        //            sysId = Convert.ToInt32(HttpContext.Current.Session["SystemId"]);
        //        }
        //        catch (Exception ex)
        //        {
        //            sysId = -1;
        //        }
        //        return sysId;
        //    }
        //}

        //#region Added by Prem(8800128549) 20-Nov-2013, Add the logic for multi organization
        //public static long LiveUsers
        //{
        //    get
        //    {
        //        long liveusers = 0;
        //        if (Constant.OrgID == 1)//R Hollenshead
        //        {
        //            liveusers = liveusersHVMS;
        //        }
        //        else if (Constant.OrgID == 2)//Rapid Remarketing
        //        {
        //            liveusers = liveusersRAPID;
        //        }
        //        else if (Constant.OrgID == 3)//Demo
        //        {
        //            liveusers = liveusersDEMO;
        //        }
        //        return liveusers;
        //    }
        //    set
        //    {
        //        if (Constant.OrgID == 1)//R Hollenshead
        //        {
        //            liveusersHVMS = value;
        //        }
        //        else if (Constant.OrgID == 2)//Rapid Remarketing
        //        {
        //            liveusersRAPID = value;
        //        }
        //        else if (Constant.OrgID == 3)//Demo
        //        {
        //            liveusersDEMO = value;
        //        }
        //    }
        //}
        //#endregion
        //US Country ID

        public const string US_COUNTRYID = "233";

        #region [Buyer Commision Types/Rules ID From CommissionType Table and read here from WebConfig]
        public static string COMMISSIONTYPE_FIXED = ConfigurationSettings.AppSettings["COMMISSIONTYPE_FIXED"];
        public static string COMMISSIONTYPE_5050 = ConfigurationSettings.AppSettings["COMMISSIONTYPE_5050"];
        public static string COMMISSIONTYPE_5050SPLIT = ConfigurationSettings.AppSettings["COMMISSIONTYPE_5050SPLIT"];
        public static string COMMISSIONTYPE_IINDLEVEL5050 = ConfigurationSettings.AppSettings["COMMISSIONTYPE_IINDLEVEL5050"];
        public static string COMMISSIONTYPE_GROSS = ConfigurationSettings.AppSettings["COMMISSIONTYPE_GROSS"];
        public static string COMMISSIONTYPE_NOCOMMISSION = ConfigurationSettings.AppSettings["COMMISSIONTYPE_NOCOMMISSION"];
        #endregion

        public enum ActivityLookup
        {
            UCRAdded = 1,
            UCRModified = 2,
            RegularLaneModified = 3,
            ExoticLaneModified = 4,
            InventoryAccessed = 5,
            //No_of_times_Title_Present_chan = 6,
            //No_of_times_Title_Present_chan = 7,
            //No_of_times_Dup_Titles_changed = 8,
            //No_of_times_Dup_Titles_changed = 9,
            //No_of_times_Title_shipped_chan = 10,
            //No_of_times_Title_shipped_chan = 11,
            Document_Added = 12,
            Document_Deleted = 13,
            Commission_Added = 14,
            Total_Commission_Amt = 15,
            No_of_Late_Title_applied = 16,
            Total_Late_Title_Fee = 17,
            No_of_Check_written = 18,
            Total_Amt_of_checks = 19,
            No_of_VIN_search_per_user = 20,
            No_of_Unique_VIN_search = 21,
            No_of_car_added_in_Inventory = 22,
            No_of_car_deleted_in_the_Inven = 23,
            Values_of_car_added_in_the_Inv = 24,
            Values_of_car_deleted_in_the_I = 25,
            No_of_times_changed_to_Here = 26,
            No_of_times_changed_to_Not_Her = 27,
            No_of_Good_Car_Fax = 28,
            No_of_Bad_Car_Fax = 29,
            No_of_Unknown_Car_Fax = 30,
            No_of_Good_Auto_Check = 31,
            No_of_Bad_Auto_Check = 32,
            No_of_Unknown_Auto_Check = 33,
            Expense_Added = 34,
            Expense_Deleted = 35,
            Value_of_Expense_Added = 36,
            Value_of_Expense_Deleted = 37,
            No_of_cars_came_back_to_Invent = 38,
            No_of_cars_came_back_and_delet = 39,
            Total_Value_of_comeback_cars = 40,
            Total_Value_deleted_of_comebac = 41,
            Deposit_Added = 42,
            Deposit_Deleted = 43,
            Total_Deposit_Amount = 44,
            Total_Deposit_Change = 45,
            No_of_cars_sold = 46,
            No_of_cars_not_sold = 47,
            Total_amount_of_sold_cars = 48,
            Total_amount_of_not_sold_cars = 49,
            Cars_registered_with_MAA = 50,
            Total_value_of_registered_cars = 51,
            No_of_cars_acknowledged = 52,
            No_of_cars_confirmed = 53,
            No_of_cars_Failed = 54,
            No_of_cars_cancelled = 55,
            //No_of_cars_sold = 56,
            No_of_cars_unresolved = 57,
            No_of_cars_3_years_old = 58,
            //No_of_cars_2.5_years_old = 59,
            No_of_cars_2_years_old = 60,
            No_of_cars_1_year_old = 61,
            No_of_cars_6_months_old = 62,
            No_of_cars_3_months_old = 63
        };


        public static Int32 GetRandomNumber4Digit()
        {
            Random rnd = new Random();
            return rnd.Next(1000, 9999);
        }

    }
}
