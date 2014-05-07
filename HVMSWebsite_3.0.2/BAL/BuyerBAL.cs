using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using METAOPTION;
using METAOPTION.DAL;
using System.Web;
namespace METAOPTION.BAL
{
    public class BuyerBAL
    {
        #region[For Add Screen]
        #region [Add Buyer Details]
        /// <summary>
        /// add buyer full details
        /// </summary>
        /// <param name="objBuyer"></param>
        /// <param name="objAddress"></param>
        /// <returns></returns>
        public static long AddBuyerDetails(Buyer objBuyer, Address objAddress)
        {
            METAOPTION.DAL.BuyerDAL objBuyerDAL = new METAOPTION.DAL.BuyerDAL();
            HttpContext.Current.Cache.Remove(CacheEnum.Buyers);
            return objBuyerDAL.AddBuyerDetails(objBuyer, objAddress);
        }
        #endregion

        #endregion

        #region[View Buyer Screen]
        #region [Get All Buyer List]
        public static IQueryable GetBuyerList()
        {
            METAOPTION.DAL.BuyerDAL objBuyerDAL = new METAOPTION.DAL.BuyerDAL();
            return objBuyerDAL.GetBuyerList();
        }
        #endregion

        #region[Get Buyer List along with Search]
        public static IEnumerable GetBuyerList(string buyerName, Int32 commissionId, Int32 paymentId, string city, Int32 countryId, Int32 stateId, string zip, Int32 Status,Int16 OrgID)
        {
            METAOPTION.DAL.BuyerDAL objBuyerDAL = new METAOPTION.DAL.BuyerDAL();
            return objBuyerDAL.GetBuyerList(buyerName, commissionId, paymentId, city, countryId, stateId, zip, Status, OrgID);
        }
        #endregion

        #region[Get Buyer Details]
        public static IQueryable GetBuyerDetails(long BuyerId,Int16 OrgID)
        {
            METAOPTION.DAL.BuyerDAL objBuyerDAL = new METAOPTION.DAL.BuyerDAL();
            return objBuyerDAL.GetBuyerDetails(BuyerId, OrgID);
        }
        #endregion

        #region[Get Purchased Cars by a buyer]
        public IEnumerable GetPurchasedCarsByBuyer(long BuyerId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            METAOPTION.DAL.BuyerDAL objBuyerDAL = new METAOPTION.DAL.BuyerDAL();
            return objBuyerDAL.GetPurchasedCarsByBuyer(BuyerId, StartRowIndex, MaximumRows);
        }
        #endregion

        #region[Count Purchased cars by Buyer]
        public Int32? GetPurchasedCarsByBuyerCount(long BuyerId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            METAOPTION.DAL.BuyerDAL objBuyerDAL = new METAOPTION.DAL.BuyerDAL();
            return objBuyerDAL.GetPurchasedCarsByBuyerCount(BuyerId, StartRowIndex, MaximumRows);
        }
        #endregion


        #region[Update Buyer Details]
        public static int UpdateBuyerDetails(Buyer objBuyer, Address objAddress)
        {
            METAOPTION.DAL.BuyerDAL objBuyerDAL = new METAOPTION.DAL.BuyerDAL();
            HttpContext.Current.Cache.Remove(CacheEnum.Buyers);
            return objBuyerDAL.UpdateBuyerDetails(objBuyer, objAddress);
        }
        #endregion

        #region [get BuyerCommission Calculation Details]
        public static GetBuyerCommCalculationInfoResult GetBuyerComm_CalculationInfo(long buyerId, long expenseId)
        {
            return BuyerDAL.GetBuyerComm_CalculationInfo(buyerId, expenseId);
        }
        public static DataTable GetBuyerComm_CalculationInformation(long buyerId, Int32 ParentBuyerID, Int32 EntityTypeID, long expenseId)
        {
            return BuyerDAL.GetBuyerComm_CalculationInformation(buyerId, ParentBuyerID, EntityTypeID, expenseId);
        }
        #endregion


        #region [get Buyer Total outstanding amount]
        /// <summary>
        /// Get outstanding amount due on buyer(if any)
        /// </summary>
        /// <param name="buyerId"></param>
        /// <returns></returns>
        public static decimal GetBuyerOutstandingAmount(long buyerId)
        {
            return BuyerDAL.GetBuyerOutstandingAmount(buyerId);
        }
        #endregion



        #region [get Buyer all outstanding transaction details]
        /// <summary>
        /// Get All payment transactions(including current outstanding, previous outstanding, Total outstanding amount)
        /// </summary>
        /// <param name="buyerId"></param>
        /// <returns></returns>
        public static GetBuyerOutstandingsDetailsResult[] GetBuyerOutstandingDetails(long buyerId)
        {
            return BuyerDAL.GetBuyerOutstandingDetails(buyerId);
        }
        #endregion

        #region [get Buyer specific commission settings]
        /// <summary>
        /// get Buyer specific commission settings
        /// </summary>
        /// <param name="buyerId"></param>
        /// <returns></returns>
        public static GetBuyerCommissionSettingsResult GetBuyerSpecificCommissionSetting(long buyerId)
        {
            return BuyerDAL.GetBuyerSpecificCommissionSetting(buyerId);
        }
        #endregion

        #region [get Buyer Commission Settings From Transaction Table]
        /// <summary>
        /// get Buyer commission settings from transaction table
        /// </summary>
        /// <param name="buyerId"></param>
        /// <returns></returns>
        public static GetBuyerCommSettings_FromTranTableResult GetBuyerCommission_TransactionSetting(long buyerCommissionId)
        {
            return BuyerDAL.GetBuyerCommission_TransactionSetting(buyerCommissionId);
        }
        #endregion


        #endregion

        public static Int32 BuyerCodeAvailability(String BuyerCode)
        {
            return BuyerDAL.BuyerCodeAvailability(BuyerCode);
        }

        #region[Delete & Archive Vendor]
        public static void DeleteArchiveBuyer(int Status, Int64 BuyerID, Int64 UserID)
        {
            BuyerDAL objBuyerDAL = new BuyerDAL();
            METAOPTION.DAL.VendorDAL objvendorDAL = new VendorDAL();
            objBuyerDAL.DeleteArchiveBuyer(Status, BuyerID, UserID);
        }
        #endregion

        #region[Get direct Buyers List]
        public IQueryable GetAllDirectBuyers(short orgID)
        {
            BuyerDAL dal = new BuyerDAL();
            return dal.GetAllDirectBuyers(orgID);
        }

     
        #endregion        

        #region[Update buyer commission setting]
        public Int32 UpdateBuyerCommissionSettings(long buyerID, Int32? minGross, Int32? minValGross, Int32? maxGross, Int32? maxValGross, Int32? exactVal, decimal? titleFee, decimal? reconFee, long? inventoryID, decimal? fixedCommission, decimal? secondBuyerCommission, long ModifiedBy)
        {
            BuyerDAL dal = new BuyerDAL();
            return dal.UpdateBuyerCommissionSettings(buyerID, minGross, minValGross, maxGross, maxValGross, exactVal, titleFee, reconFee, inventoryID, fixedCommission, secondBuyerCommission, ModifiedBy);
        }
        #endregion

        #region[Buyer Commission History]
        public DataTable BuyerCommissionHistory(long BuyerID)
        {
            BuyerDAL dal = new BuyerDAL();
            return dal.BuyerCommissionHistory(BuyerID);
        }
        #endregion

        #region[Add Buyer Commission Setting]
        public void AddBuyerCommissionSetting(BuyerCommissionSetting obj)
        {
            BuyerDAL dal = new BuyerDAL();
            dal.AddBuyerCommissionSetting(obj);
        }
        #endregion

        #region[Check whether commission setting exists for a buyer]
        public bool IsSettingExists(long buyerID)
        {
            BuyerDAL dal = new BuyerDAL();
            return dal.IsSettingExists(buyerID);
        }
        #endregion

        #region[Fetch buyer commission settings]
        public static BuyerCommissionSettings_FetchResult GetCommissionSetting(long buyerId)
        {
            return BuyerDAL.GetCommissionSetting(buyerId);
        }
        #endregion
    }
}
