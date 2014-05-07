using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using METAOPTION;
using METAOPTION.DAL;
using System.Web;
namespace METAOPTION.BAL
{
    public class DealerCustomerBAL
    {
        #region[For Add Screen]
        #region [Add Dealer/Customer Details]
        /// <summary>
        /// this is the region to insert
        /// Dealer/Customer Details
        /// </summary>
        /// <returns></returns>
        /// 
        public static long AddDealerDetails(Dealer objDealer, Address objAddress, String WelcomeTaskIDs)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            HttpContext.Current.Cache.Remove(CacheEnum.Dealer);
            return objDealerCustomerDAL.AddDealerDetails(objDealer, objAddress, WelcomeTaskIDs);
        }
        #endregion

        #region [Add FranchiseId against dealerId/CustomerId]
        public static void AddFranchise(int FranchiseId, long DealerId)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            objDealerCustomerDAL.AddFranchise(FranchiseId, DealerId);
        }
        #endregion

        #region[Add Dealer Preference details]
        /// <summary>
        /// add dealer/customer preference details
        /// </summary>
        /// <param name="objDealerPreference"></param>
        public static void AddDealerPreference(DealerPreference objDealerPreference, Int32 Mode)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            objDealerCustomerDAL.AddDealerPreference(objDealerPreference, Mode);
        }
        #endregion

        #region[Add Dealer/Customer Mobile Preference Details]
        public static void AddDealerMobilePreference(DealerMobile objDealerMobile, Int32 Mode)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            objDealerCustomerDAL.AddDealerMobilePreference(objDealerMobile, Mode);
        }
        #endregion

        #region[Add Dealer/Customer Email Preference Details]
        public static void AddDealerEmailPreference(DealerEmail objDealerEmail, Int32 Mode)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            objDealerCustomerDAL.AddDealerEmailPreference(objDealerEmail, Mode);
        }
        #endregion
        #endregion

        #region[For View Screen]

        #region [Get Dealer/Customer Details]
        public static IQueryable GetDealerDetails(long DealerId, Int16 OrgID)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            return objDealerCustomerDAL.GetDealerDetails(DealerId, OrgID);
        }
        #endregion

        #region[Get Dealer/Customer Franchise]
        public static List<long> GetDealerFranchise(long DealerId)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            return objDealerCustomerDAL.GetDealerFranchise(DealerId);

        }
        #endregion


        #region[Get Multiple Franchise Name]
        public static string GetFranchiseName(long DealerId)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            return objDealerCustomerDAL.GetFranchiseName(DealerId);
        }
        #endregion

        #region[Get Dealer Preference Setting]
        public static IQueryable GetDealerPreferenceSetting(long DealerId)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            return objDealerCustomerDAL.GetDealerPreferenceSetting(DealerId);
        }
        #endregion

        #region[Get Dealer Preference Details]
        public static DataTable GetDealerPreference(long DealerId)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            return objDealerCustomerDAL.GetDealerPreference(DealerId);
        }
        #endregion

        #region[Get Dealer Mobile Preference]
        public static DataTable GetDealerMobilePreference(long DealerId)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            return objDealerCustomerDAL.GetDealerMobilePreference(DealerId);
        }
        #endregion

        #region[Get Dealer Emails Preference]
        public static DataTable GetDealerEmailPreference(long DealerId)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            return objDealerCustomerDAL.GetDealerEmailPreference(DealerId);
        }
        #endregion

        #region [Get Purchased Cars by Dealer/Customer]
        public IEnumerable GetPurchasedCarsByDealer(long DealerId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            return objDealerCustomerDAL.GetPurchasedCarsByDealer(DealerId, StartRowIndex, MaximumRows);
        }
        #endregion

        #region[Count Purchased cars by Dealer]
        public Int32? GetPurchasedCarsByDealerCount(long DealerId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            return objDealerCustomerDAL.GetPurchasedCarsByDealerCount(DealerId, StartRowIndex, MaximumRows);
        }
        #endregion

        #region [Get Sold Cars to Dealer/Customer]
        public IEnumerable GetSoldCarsToDealer(long DealerId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            return objDealerCustomerDAL.GetSoldCarsToDealer(DealerId, StartRowIndex, MaximumRows);
        }
        #endregion

        #region[Count Sold cars by Dealer]
        public Int32? GetSoldCarsToDealerCount(long DealerId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            return objDealerCustomerDAL.GetSoldCarsToDealerCount(DealerId, StartRowIndex, MaximumRows);
        }
        #endregion


        #region[Get Business Summary of a Dealer/Customer]
        public static IQueryable DealerBusinessSummary(long DealerId)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            return objDealerCustomerDAL.DealerBusinessSummary(DealerId);
        }
        #endregion

        #region[Delete Dealer/Customer franchise]
        public static void DealerFranchiseDelete(long DealerId)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            objDealerCustomerDAL.DealerFranchiseDelete(DealerId);
        }
        #endregion

        #region[Update Dealer/Customer Details]
        public static int UpdateDealerDetails(Dealer objDealer, Address objAddress, String WelcomeTaskIDs)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            HttpContext.Current.Cache.Remove(CacheEnum.Dealer);
            return objDealerCustomerDAL.UpdateDealerDetails(objDealer, objAddress, WelcomeTaskIDs);
        }
        #endregion

        #region[Update Dealer/Customer Preference Setting]
        public static int UpdateDealerPreferenceSetting(Dealer objDealer)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            return objDealerCustomerDAL.UpdateDealerPreferenceSetting(objDealer);
        }
        #endregion

        #region[Update Dealer/Customer Preference]
        public static int UpdateDealerPreference(DealerPreference objDealerPreference)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            return objDealerCustomerDAL.UpdateDealerPreference(objDealerPreference);
        }
        #endregion

        #region[Update Dealer/Customer Mobile Preference]
        public static int UpdateDealerMobilePreference(DealerMobile objDealerMobile)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            return objDealerCustomerDAL.UpdateDealerMobilePreference(objDealerMobile);
        }
        #endregion

        #region[Update Dealer/Customer Email Preference]
        public static int UpdateDealerEmailPreference(DealerEmail objDealerEmail)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            return objDealerCustomerDAL.UpdateDealerEmailPreference(objDealerEmail);
        }
        #endregion

        #region [Get Dealer/Customer List]
        public DataTable DealerSearchList(String dealerName, Int32 categoryId, Int32 typeId, String city, Int32 countryId, Int32 stateId, String zip, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, Int32 Status, Int32 OrgID, Int32 DaysLastTransaction, String DateSince, String WelcomeTasks)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            return objDealerCustomerDAL.DealerSearchList(
                  dealerName == null ? "" : dealerName
                , categoryId
                , typeId
                , city == null ? "" : city
                , countryId
                , stateId
                , zip == null ? "" : zip
                , StartRowIndex
                , MaximumRows
                , SortExpression == null ? "" : SortExpression
                , Status
                , OrgID
                , DaysLastTransaction
                , DateSince
                , WelcomeTasks);
        }
        #endregion
        #region [Get Dealer/Customer List]
        public Int32? DealerSearchListCount(String dealerName, Int32 categoryId, Int32 typeId, String city, Int32 countryId, Int32 stateId, String zip, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, Int32 Status, Int32 OrgID, Int32 DaysLastTransaction, String DateSince, String WelcomeTasks)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            return objDealerCustomerDAL.DealerSearchListCount(
                dealerName == null ? "" : dealerName
                 , categoryId
                 , typeId
                 , city == null ? "" : city
                 , countryId
                 , stateId
                 , zip == null ? "" : zip
                 , StartRowIndex
                 , MaximumRows
                 , SortExpression == null ? "" : SortExpression
                 , Status
                 , OrgID
                , DaysLastTransaction
                , DateSince
                , WelcomeTasks);
        }
        #endregion

        #region[Get Dealer/Customer Name]
        public static string GetDealerName(long DealerId)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            return objDealerCustomerDAL.GetDealerName(DealerId);

        }
        #endregion

        #endregion

        #region[Dealer/Customer list sort options]
        public static DataTable DealerList_SortOptions(String strQuery, String Sort1, String Sort2)
        {
            DealerCustomerDAL dcDAL = new DealerCustomerDAL();
            return dcDAL.DealerList_SortOptions(strQuery, Sort1, Sort2);
        }
        #endregion

        #region[Dealer/Customer list sort options]
        public static List<Inventory_Dealer_ByBuyerIDResult> Inventory_Dealer_ByBuyerID(long BuyerID)
        {
            METAOPTION.DAL.DealerCustomerDAL objDealerCustomerDAL = new DealerCustomerDAL();
            return objDealerCustomerDAL.Inventory_Dealer_ByBuyerID(BuyerID);
        }
        #endregion


        #region[Delete & Archive Dealer]
        public static void DeleteArchiveDealer(int Status, Int64 DealerID, Int64 UserID)
        {
            DealerCustomerDAL objDealerDAL = new DealerCustomerDAL();
            METAOPTION.DAL.VendorDAL objvendorDAL = new VendorDAL();
            objDealerDAL.DeleteArchiveDealer(Status, DealerID, UserID);
        }
        #endregion

        #region[Inventory Customer By Buyer ID(s)]
        public static List<Inventory_Customer_ByBuyerIDResult> Inventory_Customer_ByBuyerID(String BuyerID, DateTime SoldDateFrom, DateTime SoldDateTo)
        {
            DealerCustomerDAL objDAL = new DealerCustomerDAL();
            return objDAL.Inventory_Customer_ByBuyerID(BuyerID, SoldDateFrom, SoldDateTo);
        }
        #endregion

        #region[Inventory Dealer By Buyer ID(s)]
        public static List<InventoryDealer_ByBuyerIDResult> InventoryDealer_ByBuyerID(String BuyerID, DateTime SoldDateFrom, DateTime SoldDateTo)
        {
            DealerCustomerDAL objDAL = new DealerCustomerDAL();
            return objDAL.InventoryDealer_ByBuyerID(BuyerID, SoldDateFrom, SoldDateTo);
        }
        #endregion

        #region Get Welcome Task Data
        public DataTable GetWelcomeTasks()
        {
            METAOPTION.DAL.DealerCustomerDAL dal = new DealerCustomerDAL();
            return dal.GetWelcomeTasks();
        }
        #endregion

    }
}
