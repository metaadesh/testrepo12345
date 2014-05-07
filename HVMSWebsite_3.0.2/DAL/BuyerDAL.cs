using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using METAOPTION;
using METAOPTION.DAL;
using System.Data.SqlClient;

namespace METAOPTION.DAL
{
    public class BuyerDAL
    {
        #region[For Add Screen]
        #region [Add Buyer Details]
        /// <summary>
        /// this is region to add
        /// buyer details
        /// </summary>
        /// <returns></returns>
        public long AddBuyerDetails(Buyer objBuyer, Address objAddress)
        {
            // DALDataContext objDAL = new DALDataContext();
            Nullable<long> BuyerId = null;
            METAOPTION.DALDataContext.Factory.DB.BuyerDetailsInsert(ref BuyerId, objBuyer.TitleId, objBuyer.FirstName, objBuyer.MiddleName,
                objBuyer.LastName, objBuyer.Buyer_Code, objBuyer.TaxIdNumber, objBuyer.PaymentTermId,
                //objBuyer.AccountingCode,
                objBuyer.CommissionTypeId, objBuyer.CommissionValue, objBuyer.StateSalesmanLicenseNumber,
                objBuyer.LicensePlateNumber, objBuyer.CellPhone, objBuyer.CommissionTermId, objBuyer.Comments,
                objBuyer.AddedBy, objBuyer.OrgID, objBuyer.IsDirectBuyer, objBuyer.ParentBuyer, objBuyer.AccessLevel, objAddress.Street, objAddress.Suite, objAddress.City, objAddress.StateId, objAddress.CountryId,
                objAddress.Zip, objAddress.Phone1, objAddress.Phone1Ext, objAddress.Phone2, objAddress.Phone2Ext,
                objAddress.Fax, objAddress.Email1, objAddress.Email2);

            return BuyerId.Value;
        }
        #endregion
        #endregion

        #region[For View Buyer Screen]
        #region [Get All Buyer List]
        public IQueryable GetBuyerList()
        {
            IQueryable result = METAOPTION.DALDataContext.Factory.DB.GetBuyers().AsQueryable();
            return result;
        }
        #endregion

        #region[Get Buyer List along with Search]
        public IEnumerable GetBuyerList(string buyerName, Int32 commissionId, Int32 paymentId, string city, Int32 countryId, Int32 stateId, string zip, Int32 Status, Int16 OrgID)
        {
            return METAOPTION.DALDataContext.Factory.DB.BuyerListSearch(buyerName, commissionId, paymentId, city, countryId, stateId, zip, Status, OrgID).AsEnumerable();

        }
        #endregion

        #region[Get Buyer Details]
        public IQueryable GetBuyerDetails(long BuyerId, Int16 OrgID)
        {
            IQueryable result = METAOPTION.DALDataContext.Factory.DB.GetBuyerDetailsByBuyerId(BuyerId, OrgID).AsQueryable();
            return result;
        }
        #endregion

        #region[Get Purchsed Cars by a Buyer]
        public IEnumerable GetPurchasedCarsByBuyer(long BuyerId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            return METAOPTION.DALDataContext.Factory.DB.GetPurchasedCarsByBuyer(BuyerId, StartRowIndex, MaximumRows).AsEnumerable();
        }
        #endregion

        #region [Get Purchased Cars Count of a Buyer]
        public Int32? GetPurchasedCarsByBuyerCount(long BuyerId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            GetPurchasedCarsByBuyerCountResult result = METAOPTION.DALDataContext.Factory.DB.GetPurchasedCarsByBuyerCount(BuyerId, StartRowIndex, MaximumRows).Single();
            return result.TotalRecord;
        }
        #endregion

        #region[Update Buyer Details]
        public int UpdateBuyerDetails(Buyer objBuyer, Address objAddress)
        {
            int result = METAOPTION.DALDataContext.Factory.DB.BuyerDetailsUpdate(objBuyer.BuyerId, objBuyer.TitleId, objBuyer.FirstName,
                objBuyer.MiddleName, objBuyer.LastName, objBuyer.Buyer_Code, objBuyer.TaxIdNumber, objBuyer.PaymentTermId,
                //objBuyer.AccountingCode,
                objBuyer.CommissionTypeId, objBuyer.CommissionValue, objBuyer.StateSalesmanLicenseNumber,
                objBuyer.LicensePlateNumber, objBuyer.CellPhone, objBuyer.CommissionTermId, objBuyer.Comments, objBuyer.ModifiedBy,
                objBuyer.IsDirectBuyer, objBuyer.ParentBuyer, objBuyer.AccessLevel,
                objAddress.AddressId, objAddress.Street, objAddress.Suite, objAddress.City, objAddress.StateId, objAddress.CountryId,
                objAddress.Zip, objAddress.Phone1, objAddress.Phone1Ext, objAddress.Phone2, objAddress.Phone2Ext,
                objAddress.Fax, objAddress.Email1, objAddress.Email2);
            return result;
        }
        #endregion

        #region [get BuyerCommission Calculation Details]
        /// <summary>
        /// Get Buyer Commission Calculation details with all parameters values and formula applied
        /// </summary>
        /// <param name="buyerId"></param>
        /// <param name="expenseId"></param>
        /// <returns></returns>
        public static GetBuyerCommCalculationInfoResult GetBuyerComm_CalculationInfo(long buyerId, long expenseId)
        {
            GetBuyerCommCalculationInfoResult result;
            return result = METAOPTION.DALDataContext.Factory.DB.GetBuyerCommCalculationInfo(buyerId, expenseId).SingleOrDefault();
        }

        public static DataTable GetBuyerComm_CalculationInformation(long buyerId, Int32 ParentBuyerID, Int32 EntityTypeID, long expenseId)
        {
            DataTable Dt = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("GetBuyerCommCalculationInfo_ver211", Conn);
                Cmd.Parameters.AddWithValue("@buyerid", buyerId);
                Cmd.Parameters.AddWithValue("@ParentBuyerID", ParentBuyerID);
                Cmd.Parameters.AddWithValue("@ExpenseId", expenseId);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                Dt.Load(reader);
                reader.Close();
                objDal.Dispose();

            }
            return Dt;
        }
        #endregion

        #region [get Buyer Total Outstanding Amount]
        /// <summary>
        /// Get Outstanding amount due on buyer(if any)
        /// </summary>
        /// <param name="buyerId"></param>
        /// <returns></returns>
        public static decimal GetBuyerOutstandingAmount(long buyerId)
        {
            int recordCount = METAOPTION.DALDataContext.Factory.DB.GetBuyerOutstandings(buyerId).Count();
            if (recordCount > 0)
                return METAOPTION.DALDataContext.Factory.DB.GetBuyerOutstandings(buyerId).SingleOrDefault().TotalOutstandingAmount.Value;
            else
                return 0;
        }
        #endregion

        #region [get Buyer outstanding transaction details]
        /// <summary>
        /// Get All payment transactions(including current outstanding, previous outstanding, Total outstanding amount)
        /// </summary>
        /// <param name="buyerId"></param>
        /// <returns></returns>
        public static GetBuyerOutstandingsDetailsResult[] GetBuyerOutstandingDetails(long buyerId)
        {
            GetBuyerOutstandingsDetailsResult[] result = METAOPTION.DALDataContext.Factory.DB.GetBuyerOutstandingsDetails(buyerId).ToArray();
            return result;
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
            GetBuyerCommissionSettingsResult result = METAOPTION.DALDataContext.Factory.DB.GetBuyerCommissionSettings(buyerId).SingleOrDefault();
            return result;
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
            GetBuyerCommSettings_FromTranTableResult result = METAOPTION.DALDataContext.Factory.DB.GetBuyerCommSettings_FromTranTable(buyerCommissionId).SingleOrDefault();
            return result;
        }
        #endregion

        #endregion

        public static Int32 BuyerCodeAvailability(String BuyerCode)
        {
            int recordCount = METAOPTION.DALDataContext.Factory.DB.BuyerCode_CheckAvailability(BuyerCode).SingleOrDefault().Total.Value;
            if (recordCount > 0)
                return 1;
            else
                return 0;
        }

        #region[Delete & Archive Buyer]
        public void DeleteArchiveBuyer(int Status, Int64 BuyerID, Int64 UserID)
        {
            int i = METAOPTION.DALDataContext.Factory.DB.DeleteArchiveBuyer(Status, BuyerID, UserID);
        }
        #endregion

        #region[Get direct Buyers List]
        public IQueryable GetAllDirectBuyers(short orgID)
        {
            IQueryable result = METAOPTION.DALDataContext.Factory.DB.Buyer_DirectBuyerList(orgID).AsQueryable();
            return result;
        }
        #endregion

        #region[Update buyer commission setting]
        public Int32 UpdateBuyerCommissionSettings(long buyerID, Int32? minGross, Int32? minValGross, Int32? maxGross, Int32? maxValGross, Int32? exactVal, decimal? titleFee, decimal? reconFee, long? inventoryID, decimal? fixedCommission, decimal? secondBuyerCommission, long ModifiedBy)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("BuyerCommissionSettings_Update", Conn);

                Cmd.Parameters.AddWithValue("@BuyerId", buyerID);
                Cmd.Parameters.AddWithValue("@Min_Gross", minGross);
                Cmd.Parameters.AddWithValue("@MinValue_Gross", minValGross);
                Cmd.Parameters.AddWithValue("@Max_Gross", maxGross);
                Cmd.Parameters.AddWithValue("@MaxValue_Gross", maxValGross);
                Cmd.Parameters.AddWithValue("@Exact_Value", exactVal);
                Cmd.Parameters.AddWithValue("@Title_fee_5050", titleFee);
                Cmd.Parameters.AddWithValue("@Recon_fee_5050", reconFee);
                Cmd.Parameters.AddWithValue("@InventoryId", inventoryID);
                Cmd.Parameters.AddWithValue("@FixedCommission", fixedCommission);
                Cmd.Parameters.AddWithValue("@SecondBuyerCommission_5050Split", secondBuyerCommission);
                Cmd.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);
                Cmd.CommandType = CommandType.StoredProcedure;
                Result = (Int32)Cmd.ExecuteNonQuery();
                objDal.Dispose();
            }
            return Result;

        }
        #endregion

        #region[Buyer Commission History]
        public DataTable BuyerCommissionHistory(long BuyerID)
        {
            DataTable dTab = new DataTable("CommissionHistory");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("BuyerCommissionHistory", Conn);
                Cmd.Parameters.AddWithValue("@BuyerID", BuyerID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region[Add Buyer Commission Setting]
        public void AddBuyerCommissionSetting(BuyerCommissionSetting obj)
        {
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("BuyerCommissionSettings_Insert", Conn);

                Cmd.Parameters.AddWithValue("@BuyerId", obj.BuyerId);
                Cmd.Parameters.AddWithValue("@Min_Gross", obj.Min_Gross);
                Cmd.Parameters.AddWithValue("@MinValue_Gross", obj.MinValue_Gross);
                Cmd.Parameters.AddWithValue("@Max_Gross", obj.Max_Gross);
                Cmd.Parameters.AddWithValue("@MaxValue_Gross", obj.MaxValue_Gross);
                Cmd.Parameters.AddWithValue("@Exact_Value", obj.Exact_Value);
                Cmd.Parameters.AddWithValue("@Title_fee_5050", obj.Title_fee_5050);
                Cmd.Parameters.AddWithValue("@Recon_fee_5050", obj.Recon_fee_5050);
                Cmd.Parameters.AddWithValue("@InventoryId", obj.InventoryId);
                Cmd.Parameters.AddWithValue("@FixedCommission", obj.FixedCommission);
                Cmd.Parameters.AddWithValue("@SecondBuyerCommission_5050Split", obj.SecondBuyerCommission_5050Split);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.ExecuteNonQuery();
                objDal.Dispose();
            }
        }
        #endregion

        #region[Check whether commission setting exists for a buyer]
        public bool IsSettingExists(long buyerID)
        {
            DALDataContext dal = new DALDataContext();
            Int32 res = (from p in dal.BuyerCommissionSettings
                         where p.BuyerId == buyerID
                         select p).Count();
            if (res > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region[Fetch buyer commission settings]
        public static BuyerCommissionSettings_FetchResult GetCommissionSetting(long buyerId)
        {
            BuyerCommissionSettings_FetchResult result = METAOPTION.DALDataContext.Factory.DB.BuyerCommissionSettings_Fetch(buyerId).SingleOrDefault();
            return result;
        }
        #endregion
    }
}