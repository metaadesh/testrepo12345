using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using METAOPTION;

namespace METAOPTION.DAL
{
    public class DealerCustomerDAL
    {

        #region[For add Screen]


        #region [Add Dealer/Customer Details]
        /// <summary>
        /// this is the region to add Dealer/Customer
        /// details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public long AddDealerDetails(Dealer objDealer, Address objAddress, String WelcomeTaskIDs)
        {
            Nullable<long> DealerId = null;
            METAOPTION.DALDataContext.Factory.DB.CustomerDealerInsert(ref DealerId, objDealer.DealerName, objDealer.DealerDIN
            , objDealer.DealerTypeId
            , objDealer.DealerCategoryId
            , objDealer.DealerSourceId
                //, objDealer.AccountingCode
            , objDealer.Comment
            , objDealer.PreferenceSettings
            , objDealer.ReceiveSms
            , objDealer.ReceiveEmail
            , objDealer.AuctionAccessNo
            , objDealer.AddedBy
            , objDealer.OrgID
            , WelcomeTaskIDs

            , objAddress.Street
            , objAddress.Suite
            , objAddress.City
            , objAddress.StateId
            , objAddress.CountryId
            , objAddress.Zip
            , objAddress.Phone1
            , objAddress.Phone1Ext
            , objAddress.Phone2
            , objAddress.Phone2Ext
            , objAddress.Fax
            , objAddress.Email1
            , objAddress.Email2);
            return DealerId.Value;
        }
        #endregion

        #region [Add FranchiseId against dealerId/CustomerId]
        public void AddFranchise(int FranchiseId, long DealerId)
        {
            METAOPTION.DALDataContext.Factory.DB.DealerFranchiseInsert(FranchiseId, DealerId);
        }
        #endregion

        #region[Add Dealer Preference details]
        /// <summary>
        /// add dealer/customer preference details
        /// </summary>
        /// <param name="objDealerPreference"></param>
        public void AddDealerPreference(DealerPreference objDealerPreference, Int32 Mode)
        {
            METAOPTION.DALDataContext.Factory.DB.DealerPreferenceInsert(objDealerPreference.DealerId, objDealerPreference.yearsFrom, objDealerPreference.yearsTo,
                objDealerPreference.MakeId, objDealerPreference.ModelId, objDealerPreference.MileageMin, objDealerPreference.MileageMax,
                objDealerPreference.PriceMin, objDealerPreference.PriceMax, objDealerPreference.IsEnabled, objDealerPreference.AddedBy, Mode);
        }
        #endregion

        #region[Add Dealer/Customer Mobile Preference Details]
        public void AddDealerMobilePreference(DealerMobile objDealerMobile, Int32 Mode)
        {
            METAOPTION.DALDataContext.Factory.DB.DealerMobilePreferenceInsert(objDealerMobile.DealerId, objDealerMobile.MobileNo, objDealerMobile.IsEnable, objDealerMobile.AddedBy, Mode);
        }
        #endregion

        #region[Add Dealer/Customer Email Preference Details]
        public void AddDealerEmailPreference(DealerEmail objDealerEmail, Int32 Mode)
        {
            METAOPTION.DALDataContext.Factory.DB.DealerEmailPreferenceInsert(objDealerEmail.DealerId, objDealerEmail.Email, objDealerEmail.IsEnable, objDealerEmail.AddedBy, Mode);
        }
        #endregion

        #endregion

        #region[For View Screen]

        #region [Get Dealer/Customer Details]
        public IQueryable GetDealerDetails(long DealerId, Int16 OrgID)
        {
            //return objDAL.GetDealerDetailsByDealerId(DealerId).AsQueryable();
            return METAOPTION.DALDataContext.Factory.DB.GetDealerDetailsByDealerId(DealerId, OrgID).AsQueryable();
        }
        #endregion

        #region[Get Dealer/Customer Franchise List]
        public List<long> GetDealerFranchise(long DealerId)
        {
            List<long> lstDealer = new List<long>();
            var result = METAOPTION.DALDataContext.Factory.DB.GetDealerFranchise(DealerId);

            foreach (GetDealerFranchiseResult item in result)
            {
                lstDealer.Add(item.FranchiseId.Value);
            }
            return lstDealer;

        }
        #endregion

        #region[Get Multiple Franchise Name]
        public string GetFranchiseName(long DealerId)
        {
            string strMakeName = string.Empty;
            METAOPTION.DALDataContext.Factory.DB.GetMakeName(DealerId, ref strMakeName);
            return strMakeName;
        }
        #endregion

        #region[Get Dealer Preference Setting]
        public IQueryable GetDealerPreferenceSetting(long DealerId)
        {
            return METAOPTION.DALDataContext.Factory.DB.GetDealerPreferenceSetting(DealerId).AsQueryable();
        }
        #endregion

        #region[Get Dealer Preference Details]
        public DataTable GetDealerPreference(long DealerId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection Conn = new SqlConnection(ConfigurationSettings.AppSettings["DBMetaoptionConnection"].ToString()))
            {
                SqlCommand Cmd = new SqlCommand("GetDealerPreferenceByDealerId");
                Cmd.Parameters.AddWithValue("@DealerId", DealerId);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Connection = Conn;
                if (!Conn.State.Equals(ConnectionState.Open))
                    Conn.Open();
                SqlDataReader dReader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(dReader);
                dReader.Close();



            }
            return dt;
        }
        #endregion

        #region[Get Dealer Mobile Preference]
        public DataTable GetDealerMobilePreference(long DealerId)
        {
            // return METAOPTION.DALDataContext.Factory.DB.GetDealerMobilePreference(DealerId).AsQueryable();

            DataTable dt = new DataTable();
            using (SqlConnection Conn = new SqlConnection(ConfigurationSettings.AppSettings["DBMetaoptionConnection"].ToString()))
            {
                SqlCommand Cmd = new SqlCommand("GetDealerMobilePreference");
                Cmd.Parameters.AddWithValue("@DealerId", DealerId);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Connection = Conn;
                if (!Conn.State.Equals(ConnectionState.Open))
                    Conn.Open();
                SqlDataReader dReader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(dReader);
                dReader.Close();
            }
            return dt;
        }
        #endregion

        #region[Get Dealer Emails Preference]
        public DataTable GetDealerEmailPreference(long DealerId)
        {
            // return METAOPTION.DALDataContext.Factory.DB.GetDealerEmailPreference(DealerId).AsQueryable();

            DataTable dt = new DataTable();
            using (SqlConnection Conn = new SqlConnection(ConfigurationSettings.AppSettings["DBMetaoptionConnection"].ToString()))
            {
                SqlCommand Cmd = new SqlCommand("GetDealerEmailPreference");
                Cmd.Parameters.AddWithValue("@DealerId", DealerId);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Connection = Conn;
                if (!Conn.State.Equals(ConnectionState.Open))
                    Conn.Open();
                SqlDataReader dReader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(dReader);
                dReader.Close();
            }
            return dt;
        }
        #endregion

        #region [Get Purchased Cars by Dealer/Customer]
        public IEnumerable GetPurchasedCarsByDealer(long DealerId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            return METAOPTION.DALDataContext.Factory.DB.GetPurchasedCarsByDealer(DealerId, StartRowIndex, MaximumRows).AsEnumerable();
        }
        #endregion

        #region[Retunrs Purchased cars count of Dealer]
        public Int32? GetPurchasedCarsByDealerCount(long DealerId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            GetPurchasedCarsByDealerCountResult result = METAOPTION.DALDataContext.Factory.DB.GetPurchasedCarsByDealerCount(DealerId, StartRowIndex, MaximumRows).Single();
            return result.TotalRecord;
        }
        #endregion

        #region [Get Sold Cars to Dealer/Customer]
        public IEnumerable GetSoldCarsToDealer(long DealerId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            return METAOPTION.DALDataContext.Factory.DB.GetSoldCarsToDealer(DealerId, StartRowIndex, MaximumRows).AsEnumerable();
        }
        #endregion

        #region[Retunrs Sold cars count of Dealer]
        public Int32? GetSoldCarsToDealerCount(long DealerId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            GetSoldCarsToDealerCountResult result = METAOPTION.DALDataContext.Factory.DB.GetSoldCarsToDealerCount(DealerId, StartRowIndex, MaximumRows).Single();
            return result.TotalRecord;
        }
        #endregion

        #region[Get Business Summary of a Dealer/Customer]
        public IQueryable DealerBusinessSummary(long DealerId)
        {
            return METAOPTION.DALDataContext.Factory.DB.GetDealerBusinessSummary(DealerId).AsQueryable();
        }
        #endregion

        #region[Delete Dealer/Customer franchise]
        public void DealerFranchiseDelete(long DealerId)
        {
            METAOPTION.DALDataContext.Factory.DB.DealerFranchiseDelete(DealerId);
        }
        #endregion

        #region[Update Dealer/Customer Details]
        public int UpdateDealerDetails(Dealer objDealer, Address objAddress, String WelcomeTaskIDs)
        {
            return METAOPTION.DALDataContext.Factory.DB.CustomerDealerUpdate(objDealer.DealerId, objDealer.DealerName, objDealer.DealerDIN
            , objDealer.DealerTypeId
            , objDealer.DealerCategoryId
            , objDealer.DealerSourceId
            , objDealer.Comment
            , objDealer.PreferenceSettings
            , objDealer.ReceiveSms
            , objDealer.ReceiveEmail
            , objDealer.AuctionAccessNo
            , objDealer.ModifiedBy
            , objDealer.OrgID
            , WelcomeTaskIDs

            , objAddress.AddressId
            , objAddress.Street
            , objAddress.Suite
            , objAddress.City
            , objAddress.StateId
            , objAddress.CountryId
            , objAddress.Zip
            , objAddress.Phone1
            , objAddress.Phone1Ext
            , objAddress.Phone2
            , objAddress.Phone2Ext
            , objAddress.Fax
            , objAddress.Email1
            , objAddress.Email2);
        }
        #endregion

        #region[Update Dealer/Customer Preference Setting]
        public int UpdateDealerPreferenceSetting(Dealer objDealer)
        {
            return METAOPTION.DALDataContext.Factory.DB.DealerPreferenceSettingUpdate(objDealer.PreferenceSettings
                 , objDealer.ReceiveSms
                 , objDealer.ReceiveEmail
                 , objDealer.DealerId);
        }
        #endregion

        #region[Update Dealer/Customer Preference]
        public int UpdateDealerPreference(DealerPreference objDealerPreference)
        {
            return METAOPTION.DALDataContext.Factory.DB.DealerPreferenceUpdate(objDealerPreference.DealerPreferenceId
                , objDealerPreference.yearsFrom
                , objDealerPreference.yearsTo
                , objDealerPreference.MakeId
                , objDealerPreference.ModelId
                , objDealerPreference.MileageMin
                , objDealerPreference.MileageMax
                , objDealerPreference.PriceMin
                , objDealerPreference.PriceMax
                , objDealerPreference.IsEnabled
                , objDealerPreference.ModifiedBy);
        }
        #endregion

        #region[Update Dealer/Customer Mobile Preference]
        public int UpdateDealerMobilePreference(DealerMobile objDealerMobile)
        {
            return METAOPTION.DALDataContext.Factory.DB.DealerMobilePreferenceUpdate(objDealerMobile.DealerMobileId
                , objDealerMobile.MobileNo
                , objDealerMobile.IsEnable
                , objDealerMobile.ModifiedBy);
        }
        #endregion

        #region[Update Dealer/Customer Email Preference]
        public int UpdateDealerEmailPreference(DealerEmail objDealerEmail)
        {
            return METAOPTION.DALDataContext.Factory.DB.DealerEmailPreferenceUpdate(objDealerEmail.DealerEmailId
                , objDealerEmail.Email
                , objDealerEmail.IsEnable
                , objDealerEmail.ModifiedBy);
        }
        #endregion

        #region [Get Dealer/Customer List]
        public DataTable DealerSearchList(String dealerName, Int32 categoryId, Int32 typeId, String city, Int32 countryId, Int32 stateId, String zip, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, Int32 Status, Int32 OrgID, Int32 DaysLastTransaction, String DateSince, String WelcomeTasks)
        {
            DataTable dt = new DataTable("Dealers");
            using (SqlConnection Conn = new SqlConnection(METAOPTION.DALDataContext.Factory.DB.Connection.ConnectionString))
            {
                SqlCommand Cmd = Conn.CreateCommand();
                Cmd.CommandText = "DealerSearchList";
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@DealerName", dealerName);
                Cmd.Parameters.AddWithValue("@Category", categoryId);
                Cmd.Parameters.AddWithValue("@Type", typeId);
                Cmd.Parameters.AddWithValue("@City", city);
                Cmd.Parameters.AddWithValue("@Country", countryId);
                Cmd.Parameters.AddWithValue("@State", stateId);
                Cmd.Parameters.AddWithValue("@ZipCode", zip);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@SortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@Status", Status);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.Parameters.AddWithValue("@DaysLastTransaction", DaysLastTransaction);
                Cmd.Parameters.AddWithValue("@DateSince", DateSince);
                Cmd.Parameters.AddWithValue("@WelcomeTasks", WelcomeTasks);

                if (!Conn.State.Equals(ConnectionState.Open))
                    Conn.Open();
                SqlDataReader dReader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(dReader);
                dReader.Close();
            }
            return dt;
        }
        #endregion

        #region [Get Dealer/Customer List Count]
        public Int32? DealerSearchListCount(String dealerName, Int32 categoryId, Int32 typeId, String city, Int32 countryId, Int32 stateId, String zip, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, Int32 Status, Int32 OrgID, Int32 DaysLastTransaction, String DateSince, String WelcomeTasks)
        {
            using (SqlConnection Conn = new SqlConnection(METAOPTION.DALDataContext.Factory.DB.Connection.ConnectionString))
            {
                SqlCommand Cmd = Conn.CreateCommand();
                Cmd.CommandText = "DealerSearchListCount";
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@DealerName", dealerName);
                Cmd.Parameters.AddWithValue("@Category", categoryId);
                Cmd.Parameters.AddWithValue("@Type", typeId);
                Cmd.Parameters.AddWithValue("@City", city);
                Cmd.Parameters.AddWithValue("@Country", countryId);
                Cmd.Parameters.AddWithValue("@State", stateId);
                Cmd.Parameters.AddWithValue("@ZipCode", zip);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@SortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@Status", Status);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.Parameters.AddWithValue("@DaysLastTransaction", DaysLastTransaction);
                Cmd.Parameters.AddWithValue("@DateSince", DateSince);
                Cmd.Parameters.AddWithValue("@WelcomeTasks", WelcomeTasks);

                if (!Conn.State.Equals(ConnectionState.Open))
                    Conn.Open();
                object count = Cmd.ExecuteScalar();
                if (count != null)
                    return Convert.ToInt32(count);
                else
                    return 0;
            }
        }
        #endregion

        #region[Get Dealer/Customer Name]
        public string GetDealerName(long DealerId)
        {
            string DealerName = string.Empty;
            var result = METAOPTION.DALDataContext.Factory.DB.GetDealerNameById(DealerId).AsQueryable();
            foreach (GetDealerNameByIdResult item in result)
            {
                DealerName = item.DealerName;
            }
            return DealerName;
        }
        #endregion
        #endregion

        #region[Dealer list sort options]
        /// <summary>
        /// Display sort options for dealer list
        /// </summary>
        /// <param name="strQuery"></param>
        /// <returns></returns>
        public DataTable DealerList_SortOptions(String strQuery, String Sort1, String Sort2)
        {
            DataTable dTab = new DataTable("DealerCustomerSort");
            using (SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["DBMetaoptionConnection"].ToString()))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = Conn.CreateCommand();
                Cmd.CommandText = strQuery;
                Cmd.CommandType = CommandType.Text;
                SqlDataReader dReader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);

                dTab.Load(dReader);
            }
            return dTab;
        }
        #endregion

        #region[Inventory Dealer By Buyer ID]
        /// <summary>
        /// Buyer ID
        /// </summary>
        /// <param name="strQuery"></param>
        /// <param name="Sort1"></param>
        /// <param name="Sort2"></param>
        /// <returns></returns>
        public List<Inventory_Dealer_ByBuyerIDResult> Inventory_Dealer_ByBuyerID(long buyerID)
        {
            return METAOPTION.DALDataContext.Factory.DB.Inventory_Dealer_ByBuyerID(buyerID).ToList<Inventory_Dealer_ByBuyerIDResult>();
        }
        #endregion


        #region[Delete & Archive Buyer]
        public void DeleteArchiveDealer(int Status, Int64 DealerID, Int64 UserID)
        {
            int i = METAOPTION.DALDataContext.Factory.DB.DeleteArchiveDealer(Status, DealerID, UserID);
        }
        #endregion

        #region[Inventory Customer By Buyer ID(s)]
        public List<Inventory_Customer_ByBuyerIDResult> Inventory_Customer_ByBuyerID(String BuyerID, DateTime SoldDateFrom, DateTime SoldDateTo)
        {
            return METAOPTION.DALDataContext.Factory.DB.Inventory_Customer_ByBuyerID(BuyerID, SoldDateFrom, SoldDateTo).ToList<Inventory_Customer_ByBuyerIDResult>();
        }
        #endregion

        #region[Inventory Dealer By Buyer ID(s)]
        public List<InventoryDealer_ByBuyerIDResult> InventoryDealer_ByBuyerID(String BuyerID, DateTime SoldDateFrom, DateTime SoldDateTo)
        {
            return METAOPTION.DALDataContext.Factory.DB.InventoryDealer_ByBuyerID(BuyerID, SoldDateFrom, SoldDateTo).ToList<InventoryDealer_ByBuyerIDResult>();
        }
        #endregion

        #region Get Welcome Task Data
        public DataTable GetWelcomeTasks()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(METAOPTION.DALDataContext.Factory.DB.Connection.ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "GetWelcomeTasks";
                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataReader dReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(dReader);
                dReader.Close();
            }
            return dt;
        }

        #endregion
    }
}
