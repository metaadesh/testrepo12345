using System;
using System.Collections;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using METAOPTION.DAL;
using System.Data.Common;
using System.ComponentModel;
using System.Dynamic;
namespace METAOPTION.DAL
{
    public class InventoryDAL
    {

        /// <summary>
        /// Get Car Details-Section Information for particular inventory
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public IQueryable GetCarDetails(long? inventoryId)
        {
            return METAOPTION.DALDataContext.Factory.DB.CarDetailsSelect(inventoryId).AsQueryable();
        }

        /// <summary>
        /// Get Car Details-Section Information for particular inventory
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public static CarDetailsSelectResult GetCarDetail(long? inventoryId)
        {
            return METAOPTION.DALDataContext.Factory.DB.CarDetailsSelect(inventoryId).SingleOrDefault();

        }


        /// <summary>
        /// This method return Car Properties-Selected by user True/False
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public Dictionary<string, bool> GetCarPropertiesBool(long? inventoryId)
        {
            Dictionary<string, bool> objDictionaryCarProp = new Dictionary<string, bool>();

            var result = METAOPTION.DALDataContext.Factory.DB.CarPropertySelectBool(inventoryId).ToList();
            foreach (CarPropertySelectBoolResult item in result)
            {
                objDictionaryCarProp.Add("item.Leather", item.Leather);
                objDictionaryCarProp.Add("item.Navigation", item.Navigation);
                objDictionaryCarProp.Add("item.PowerLocks", item.PowerLocks);
                objDictionaryCarProp.Add("item.PowerSeat", item.PowerSeat);
                objDictionaryCarProp.Add("item.PowerWindows", item.PowerWindows);
                objDictionaryCarProp.Add("item.SunMoon", item.SunMoon);
                objDictionaryCarProp.Add("item.AC", item.AC);
                objDictionaryCarProp.Add("item.AlloyWheels", item.AlloyWheels);

            }

            return objDictionaryCarProp;
        }

        //Adesh 22 Jan,2013
        public Dictionary<string, bool> GetCarPropertiesBool(DataTable dt)
        {
            Dictionary<string, bool> objDictionaryCarProp = new Dictionary<string, bool>();

            foreach (DataRow dr in dt.Rows)
            {
                objDictionaryCarProp.Add("item.Leather", Convert.ToBoolean(dr["Leather"]));
                objDictionaryCarProp.Add("item.Navigation", Convert.ToBoolean(dr["Navigation"]));
                objDictionaryCarProp.Add("item.PowerLocks", Convert.ToBoolean(dr["PowerLocks"]));
                objDictionaryCarProp.Add("item.PowerSeat", Convert.ToBoolean(dr["PowerSeat"]));
                objDictionaryCarProp.Add("item.PowerWindows", Convert.ToBoolean(dr["PowerWindows"]));
                objDictionaryCarProp.Add("item.SunMoon", Convert.ToBoolean(dr["SunMoon"]));
                objDictionaryCarProp.Add("item.AC", Convert.ToBoolean(dr["AC"]));
                objDictionaryCarProp.Add("item.AlloyWheels", Convert.ToBoolean(dr["AlloyWheels"]));

            }

            return objDictionaryCarProp;
        }

        /// <summary>
        /// Update Car Properties using edit popup of manage inventory screen
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <param name="AC"></param>
        /// <param name="PowerWindows"></param>
        /// <param name="PowerLocks"></param>
        /// <param name="AllowWheels"></param>
        /// <param name="navigation"></param>
        /// <param name="sunMoon"></param>
        /// <param name="leather"></param>
        /// <param name="powerSeat"></param>
        /// <returns></returns>
        public int CarPropertyUpdate(long? inventoryId, bool AC, bool PowerWindows, bool PowerLocks, bool AllowWheels,
          bool navigation, bool sunMoon, bool leather, bool powerSeat, long updatedBy, DateTime updatedDate)
        {
            return METAOPTION.DALDataContext.Factory.DB.CarPropertyUpdate(inventoryId, AC, PowerWindows, PowerLocks, AllowWheels, navigation, sunMoon,
                leather, powerSeat, updatedBy, updatedDate);
        }

        /// <summary>
        /// Update dealer details using edit popup of manage inventory screen
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <param name="mileagOut"></param>
        /// <param name="SoldDate"></param>
        /// <param name="marketPrice"></param>
        /// <param name="actualCost"></param>
        /// <param name="priceSold"></param>
        /// <param name="depositDate"></param>
        /// <param name="depositAmount"></param>
        /// <param name="bankId"></param>
        /// <param name="strDepositComment"></param>
        /// <param name="soldStatus"></param>
        /// <param name="comeBackStatus"></param>
        /// <param name="strComeBackReason"></param>
        /// <param name="soldTo"></param>
        /// <param name="strSoldComment"></param>
        /// <returns></returns>
        public int SoldToUpdate(long? inventoryId, long mileagOut, DateTime? SoldDate, decimal marketPrice,
           decimal actualCost, decimal priceSold, DateTime? depositDate, decimal depositAmount, int bankAccountId,
            string strDepositComment, int soldStatus, long soldTo
            , string strSoldComment, long updatedBy, DateTime updatedDate)
        {
            return METAOPTION.DALDataContext.Factory.DB.SoldToUpdate(inventoryId, mileagOut, SoldDate, marketPrice, actualCost, priceSold, depositDate, depositAmount,
                bankAccountId, strDepositComment, soldStatus, soldTo, strSoldComment, updatedBy, updatedDate);


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <param name="soldStatus"></param>
        /// <param name="strSoldComment"></param>
        /// <param name="mileagOut"></param>
        /// <param name="SoldDate"></param>
        /// <param name="marketPrice"></param>
        /// <param name="priceSold"></param>
        /// <param name="depositDate"></param>
        /// <param name="depositAmount"></param>
        /// <param name="bankAccountId"></param>
        /// <param name="strDepositComment"></param>
        /// <param name="soldTo"></param>
        /// <param name="updatedBy"></param>
        /// <param name="updatedDate"></param>  
        /// <returns></returns>
        public int SoldToUpdate_ver211(long? inventoryId, long mileagOut, DateTime? SoldDate, decimal marketPrice,
           decimal actualCost, decimal priceSold, DateTime? depositDate, decimal depositAmount, int bankAccountId,
            string strDepositComment, int soldStatus, long soldTo
            , string strSoldComment, long updatedBy, DateTime updatedDate)
        {
            METAOPTION.DALDataContext.Factory.DB.SoldToUpdate_ver211(inventoryId, mileagOut, SoldDate, marketPrice, actualCost, priceSold, depositDate
            , depositAmount, bankAccountId, strDepositComment, soldStatus, soldTo, strSoldComment, updatedBy, 1);

            return 1;
        }

        /// <summary>
        /// Update Dealer Details using edit popup of manage inventory screen
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <param name="cost"></param>
        /// <param name="purchDate"></param>
        /// <param name="titlePresent"></param>
        /// <param name="titleShipped"></param>
        /// <param name="strTitlePresentNotes"></param>
        /// <param name="strTitleShippedNotes"></param>
        /// <param name="designationId"></param>
        /// <param name="PurchaseFrom"></param>
        /// <param name="buyerId"></param>
        /// <param name="strChequeNo"></param>
        /// <returns></returns>
        public int DealerDetailUpdate(long? inventoryId, decimal cost, DateTime? purchDate, bool titlePresent, int titleCopyReceived, bool titleShipped, string strTitlePresentNotes,
              string strTitleShippedNotes, int designationId, long PurchaseFrom, long buyerId, string strChequeNo
            , long updatedBy, DateTime updatedDate, int vehicleHistoryReportId, int? DupTitle, string DupTitleNote)
        {

            return METAOPTION.DALDataContext.Factory.DB.DealerDetailsUpdate(inventoryId, cost, purchDate, titlePresent, titleCopyReceived, titleShipped, strTitlePresentNotes,
                strTitleShippedNotes, designationId, PurchaseFrom, buyerId, strChequeNo, updatedBy, updatedDate, vehicleHistoryReportId, DupTitle, DupTitleNote);

        }


        /// <summary>
        /// This method return Car Properties-Section Information for particular inventory
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public IQueryable GetCarProperties(long? inventoryId)
        {
            return METAOPTION.DALDataContext.Factory.DB.CarPropertySelect(inventoryId).AsQueryable();
        }



        /// <summary>
        /// This method return string containing section update information in the format [Updated by "UserName" on "Date"]
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <param name="strSectionName"></param>
        /// <param name="strResult"></param>
        /// <returns></returns>
        public string GetSectionHistory(long? inventoryId, int SectionId, string strResult)
        {
            METAOPTION.DALDataContext.Factory.DB.HistorySelect(inventoryId, SectionId, ref strResult);
            return strResult;

        }

        /// <summary>
        /// This method return SoldTo Details
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public IQueryable GetSoldToDetails(long? inventoryId)
        {
            return METAOPTION.DALDataContext.Factory.DB.SoldToSelect(inventoryId).AsQueryable();
        }

        /// <summary>
        /// This method return Dealer details for Inventory
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public IQueryable GetDealerForInventory(long? inventoryId)
        {
            return METAOPTION.DALDataContext.Factory.DB.DealerDetailsSelect(inventoryId).AsQueryable();
        }

        /// <summary>
        /// Select Inventory SoldStatus,Mode=1 using StoredProcedure Call
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns>List containing Sold Status & Sold Comments</returns>
        public List<string> InventorySoldStatus(long? inventoryId)
        {
            List<string> lstSoldStatus = new List<string>();
            var result = METAOPTION.DALDataContext.Factory.DB.SoldStatusSelectUpdate(inventoryId, null, null, null, null, 1);
            foreach (SoldStatusSelectUpdateResult item in result)
            {
                lstSoldStatus.Add(item.SoldStatus.ToString());
                lstSoldStatus.Add(item.SoldComment);
                lstSoldStatus.Add(item.UpdatedHistory);
            }
            return lstSoldStatus;
        }

        //Added by Adesh On 21 Jan,2013
        public List<string> InventorySoldStatus(DataTable dt)
        {
            List<string> lstSoldStatus = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                lstSoldStatus.Add(Convert.ToString(dr["SoldStatus"]));
                lstSoldStatus.Add(dr["SoldComment"] == DBNull.Value ? null : Convert.ToString(dr["SoldComment"]));
                lstSoldStatus.Add(dr["UpdatedHistory"] == DBNull.Value ? null : Convert.ToString(dr["UpdatedHistory"]));
            }
            return lstSoldStatus;
        }

        /// <summary>
        /// Update Inventory Sold Status and Comments,Mode=2 For Update using StoredProcedure Call
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <param name="SoldStatus"></param>
        /// <param name="SoldComments"></param>
        /// <returns>0/1</returns>
        public int InventorySoldStatusUpdate(long? inventoryId, int SoldStatus, string SoldComments,
            long UpdatedBy, DateTime UpdatedDate)
        {
            METAOPTION.DALDataContext.Factory.DB.SoldStatusSelectUpdate(inventoryId, SoldStatus, SoldComments, UpdatedBy, UpdatedDate, 2);
            return 0;
        }


        /// <summary>
        /// Update Car Details based on particular InventoryId
        /// </summary>
        /// <param name="objInventory"></param>
        /// <returns>0 if operation Successfull</returns>
        public int UpdateCarDetails(Inventory objInventory)
        {
            return METAOPTION.DALDataContext.Factory.DB.CarDetailsUpdate
                (
                objInventory.InventoryId,
                objInventory.VIN,
                objInventory.Year,
                objInventory.MileageIn,
                objInventory.ArrivalDate,
                objInventory.MakeId,
                objInventory.ModelId,
                objInventory.BodyId,
                objInventory.DesignatedEquipment,
                objInventory.CarNote,
                objInventory.ExtColorId,
                objInventory.IntColorId,
                objInventory.VehiclePresent,
                objInventory.EngineId,
                objInventory.TransId,
                objInventory.WheelDriveId,
                objInventory.RegularLane,
                objInventory.ExoticLane,
                objInventory.VirtualLane,
                objInventory.OnlineLane,
                objInventory.TitlePresent,
                objInventory.TitlePresentNotes,
                objInventory.TitleShipped,
                objInventory.TitleShippedNotes,
                objInventory.CarLocation,
                objInventory.ModifiedBy,
                objInventory.DateModified,
                objInventory.Grade,
                objInventory.BadCarFax);

        }


        #region[ Search Inventory ]
        /// <summary>
        /// Search Inventory
        /// </summary>
        /// <param name="VINNo">VIN #</param>
        /// <param name="year">Year</param>
        /// <param name="model">Model </param>
        /// <param name="make">Make</param>
        /// <param name="dealer">Year</param>
        /// <returns>List of Inventory</returns>
        public DataTable SearchInventory(String VIN
            , Int32 Year
            , String Model
            , String Make
            , String Dealer
            , Int32 CarStatus
            , Int32 CRStatus
            , Int32 startRowIndex
            , Int32 maximumRows
            , Int32 SystemID
            , String SortExpression
            , short OrgID)
        {
            // return METAOPTION.DALDataContext.Factory.DB.spInventorySearch(VINNo, year, model, make, dealer, StartRowIndex, MaximumRows).AsEnumerable();
            DataTable dTab = new DataTable("Inventories");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("spInventorySearch", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN == null ? "" : VIN);
                Cmd.Parameters.AddWithValue("@Year", Year == 0 ? -1 : Year);
                Cmd.Parameters.AddWithValue("@Model", Model == null ? "" : Model);
                Cmd.Parameters.AddWithValue("@Make", Make == null ? "" : Make);
                Cmd.Parameters.AddWithValue("@Dealer", Dealer == null ? "" : Dealer);
                Cmd.Parameters.AddWithValue("@CarStatus", CarStatus);
                Cmd.Parameters.AddWithValue("@CRStatus", CRStatus);
                Cmd.Parameters.AddWithValue("@StartRowIndex", startRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", maximumRows);
                Cmd.Parameters.AddWithValue("@SystemID", SystemID);
                Cmd.Parameters.AddWithValue("@SortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region[ Search Inventory Count ]
        /// <summary>
        /// Search Inventory Count
        /// </summary>
        /// <param name="VINNo">VIN #</param>
        /// <param name="year">Year</param>
        /// <param name="model">Model </param>
        /// <param name="make">Make</param>
        /// <param name="dealer">Year</param>
        /// <returns>List of Inventory</returns>
        public Int32 SearchInventoryCount(String VIN
            , Int32 Year
            , String Model
            , String Make
            , String Dealer
            , Int32 CarStatus
            , Int32 CRStatus
            , Int32 startRowIndex
            , Int32 maximumRows
            , Int32 SystemID
            , String SortExpression
            , short OrgID)
        {
            Int32 Result = 0;
            //HttpContext context = HttpContext.Current;
            //if (context.Cache["spInventorySearchCount"] == null)
            //{

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("spInventorySearchCount", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN == null ? "" : VIN);
                Cmd.Parameters.AddWithValue("@Year", Year == 0 ? -1 : Year);
                Cmd.Parameters.AddWithValue("@Model", Model == null ? "" : Model);
                Cmd.Parameters.AddWithValue("@Make", Make == null ? "" : Make);
                Cmd.Parameters.AddWithValue("@Dealer", Dealer == null ? "" : Dealer);
                Cmd.Parameters.AddWithValue("@CarStatus", CarStatus);
                Cmd.Parameters.AddWithValue("@CRStatus", CRStatus);
                Cmd.Parameters.AddWithValue("@StartRowIndex", startRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", maximumRows);
                Cmd.Parameters.AddWithValue("@SystemID", SystemID);
                Cmd.Parameters.AddWithValue("@SortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }
        #endregion

        #region[Search Main Inventory  ]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="VInMatch">VIN Match Criteria</param>
        /// <param name="VINNo">VIN No</param>
        /// <param name="checkNo">Check No</param>
        /// <param name="year">Make Year</param>
        /// <param name="makeId">Make Id</param>
        /// <param name="modelId">Model Id</param>
        /// <param name="dealerId">Dealer Id</param>
        /// <param name="customerId">Customer Id</param>
        /// <param name="buyerId">Buyer Id</param>
        /// <param name="designationId">Designation Id</param>
        /// <param name="comeBack">Come Back</param>
        /// <param name="sold">Sold</param>
        /// <returns>List of Inventories</returns>
        public DataTable SearchInventory(Int32 VInMatch, String VINNo, String checkNo, Int32 FromYear, Int32 ToYear,
                                        long makeId, long modelId, long BodyId, long dealerId, long customerId,
                                        long buyerId, long designationId, int comeBack, int sold, Int32 CarStatus, Int32 CRStatus,
                                        Int32 StartRowIndex, Int32 MaximumRows, Int32 SystemID, String SortExpression, Int32 EntityTypeID,
                                        Int32 EntityID, Int32 ParentEntityID, Int16 OrgID, String SoldDateFrom, String SoldDateTo)
        {

            DataTable Result = new DataTable("Inventories");

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("spInventoryMainSearch_ver211", Conn);
                Cmd.Parameters.AddWithValue("@VINMatch", VInMatch);
                Cmd.Parameters.AddWithValue("@VINNO", VINNo);
                Cmd.Parameters.AddWithValue("@CheckNo", checkNo);
                Cmd.Parameters.AddWithValue("@FromYear", FromYear);
                Cmd.Parameters.AddWithValue("@ToYear", ToYear);
                Cmd.Parameters.AddWithValue("@MakeId", makeId);
                Cmd.Parameters.AddWithValue("@ModelId", modelId);
                Cmd.Parameters.AddWithValue("@BodyId", BodyId);
                Cmd.Parameters.AddWithValue("@DealerId", dealerId);
                Cmd.Parameters.AddWithValue("@CustomerId", customerId);
                Cmd.Parameters.AddWithValue("@BuyerId", buyerId);
                Cmd.Parameters.AddWithValue("@DesignationId", designationId);
                Cmd.Parameters.AddWithValue("@ComeBack", comeBack);
                Cmd.Parameters.AddWithValue("@Sold", sold);
                Cmd.Parameters.AddWithValue("@CarStatus", CarStatus);
                Cmd.Parameters.AddWithValue("@CRStatus", CRStatus);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@SystemID", SystemID);
                Cmd.Parameters.AddWithValue("@SortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.Parameters.AddWithValue("@SoldDateFrom", SoldDateFrom);
                Cmd.Parameters.AddWithValue("@SoldDateTo", SoldDateTo);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                Result.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return Result;
        }
        #endregion

        #region[ Inventory Search Main Count ]
        /// <summary>
        /// Inventory Search Main Count 
        /// </summary>
        /// <param name="VInMatch">VIN Match Criteria</param>
        /// <param name="VINNo">VIN No</param>
        /// <param name="checkNo">Check No</param>
        /// <param name="year">Make Year</param>
        /// <param name="makeId">Make Id</param>
        /// <param name="modelId">Model Id</param>
        /// <param name="dealerId">Dealer Id</param>
        /// <param name="customerId">Customer Id</param>
        /// <param name="buyerId">Buyer Id</param>
        /// <param name="designationId">Designation Id</param>
        /// <param name="comeBack">Come Back</param>
        /// <param name="sold">Sold</param>
        /// <returns>List of Inventories</returns>
        public Int32 SearchInventoryCount(Int32 VInMatch, String VINNo, String checkNo, Int32 FromYear, Int32 ToYear,
                                            long makeId, long modelId, long BodyId, long dealerId, long customerId,
                                            long buyerId, long designationId, int comeBack, int sold, Int32 CarStatus,
                                            Int32 CRStatus, Int32 StartRowIndex, Int32 MaximumRows, Int32 SystemID,
                                            String SortExpression, Int32 EntityTypeID, Int32 EntityID, Int32 ParentEntityID,
                                            short OrgID, String SoldDateFrom, String SoldDateTo)
        {
            Int32 Result = 0;

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("spInventoryMainSearchCount_ver211", Conn);
                Cmd.Parameters.AddWithValue("@VINMatch", VInMatch);
                Cmd.Parameters.AddWithValue("@VINNO", VINNo);
                Cmd.Parameters.AddWithValue("@CheckNo", checkNo);
                Cmd.Parameters.AddWithValue("@FromYear", FromYear);
                Cmd.Parameters.AddWithValue("@ToYear", ToYear);
                Cmd.Parameters.AddWithValue("@MakeId", makeId);
                Cmd.Parameters.AddWithValue("@ModelId", modelId);
                Cmd.Parameters.AddWithValue("@BodyId", BodyId);
                Cmd.Parameters.AddWithValue("@DealerId", dealerId);
                Cmd.Parameters.AddWithValue("@CustomerId", customerId);
                Cmd.Parameters.AddWithValue("@BuyerId", buyerId);
                Cmd.Parameters.AddWithValue("@DesignationId", designationId);
                Cmd.Parameters.AddWithValue("@ComeBack", comeBack);
                Cmd.Parameters.AddWithValue("@Sold", sold);
                Cmd.Parameters.AddWithValue("@CarStatus", CarStatus);
                Cmd.Parameters.AddWithValue("@CRStatus", CRStatus);
                Cmd.Parameters.AddWithValue("@SystemID", SystemID);
                Cmd.Parameters.AddWithValue("@SortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.Parameters.AddWithValue("@SoldDateFrom", SoldDateFrom);
                Cmd.Parameters.AddWithValue("@SoldDateTo", SoldDateTo);
                Cmd.CommandType = CommandType.StoredProcedure;
                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }
        #endregion

        #region [Fetch Systems]
        public DataTable FetchSystems(short OrgID)
        {
            DataTable dTab = new DataTable("Systems");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("FetchSystems", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region [Fetch Grades]
        public DataTable FetchGrades()
        {
            DataTable dTab = new DataTable("Grades");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Grade_Select", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region [Fetch notes by Inventory ID]
        public DataTable FetchNotesByInventoryID(Int64 EntityID, Int32 EntityTypeID, Int32 NoteTypeID)
        {
            DataTable dTab = new DataTable("Notes");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("FetchNotesByInventoryID", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                Cmd.Parameters.AddWithValue("@NoteTypeID", NoteTypeID);
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region [Update Inventory System]
        public void UpdateInventorySystem(Int64 InventoryId, Int32 SystemID, Int64? UpdatedBy, DateTime UpdatedDate)
        {
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("UpdateSystem", Conn);
                Cmd.Parameters.AddWithValue("@InventoryId", InventoryId);
                Cmd.Parameters.AddWithValue("@SystemID", SystemID);
                Cmd.Parameters.AddWithValue("@UpdatedBy", UpdatedBy);
                Cmd.Parameters.AddWithValue("@UpdatedDate", UpdatedDate);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.ExecuteNonQuery();
                objDal.Dispose();
            }

        }
        #endregion

        #region [Check Payment]
        public int CheckPayemnt(Int64 InventoryId)
        {
            object count;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("CheckPayment", Conn);
                Cmd.Parameters.AddWithValue("@InventoryId", InventoryId);
                Cmd.CommandType = CommandType.StoredProcedure;
                count = Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return (int)count;

        }
        #endregion

        #region[ Search Inventory Drivers by Inventory Id ]
        /// <summary>
        /// Search Inventory Drivers by Inventory Id
        /// </summary> /// </summary>
        /// <param name="invnetoryIdr">InventoryId</param>
        /// <returns>List of Drivers</returns>
        public IEnumerable SearchInventoryDriverByInventoryId(Int32 invnetoryId, Int16 OrgID)
        {
            return METAOPTION.DALDataContext.Factory.DB.spInventoryDriverByInventoryId(invnetoryId, OrgID).AsEnumerable();
        }
        public DataTable SearchInventoryDriverByInventoryId(Int32 invnetoryId, Int32 SecurityUserID, Int32 ParentEntityID, Int32 EntityTypeID)
        {
            DataTable dTab = new DataTable("Notes");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("spInventoryDriverByInventoryId_ver211", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@InventoryID", invnetoryId);
                Cmd.Parameters.AddWithValue("@SecurityUserID", SecurityUserID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region[ Search Linked Inventory ]
        /// <summary>
        /// Search Inventory
        /// </summary>
        /// <param name="invnetoryIdr">InventoryId</param>
        /// <returns>List of Inventory</returns>
        public IEnumerable SearchInventoryLinked(long inventoryId, string VIN, Int16 OrgID)
        {
            return METAOPTION.DALDataContext.Factory.DB.spInventoryLinked(inventoryId, VIN, OrgID).AsEnumerable();
        }

        public DataTable SearchInventoryLinked(long inventoryId, string VIN, Int32 SecurityUserID, Int32 ParentEntityID, Int32 EntityTypeID)
        {
            DataTable dTab = new DataTable("Notes");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("spInventoryLinked_ver211", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@InventoryID", inventoryId);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@SecurityUserID", SecurityUserID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }


        #endregion


        #region Get List of Comeback Reasons
        /// <summary>
        /// This method return list of Comeback Reasons
        /// </summary>
        /// <returns></returns>
        public IQueryable GetComeBackReason()
        {
            return METAOPTION.DALDataContext.Factory.DB.ComeBackReasonSelect().AsQueryable();

        }

        //Adesh 23 Jan,2013
        public IQueryable GetComeBackReason(DataTable dt)
        {
            //return METAOPTION.DALDataContext.Factory.DB.ComeBackReasonSelect().AsQueryable();
            IQueryable<ComeBackReasonSelectResult> result = Common.ToCollection<ComeBackReasonSelectResult>(dt).AsQueryable();
            return result;

        }
        #endregion


        #region Update Comeback Reason Details
        /// <summary>
        /// Update Comeback Details
        /// </summary>
        /// <param name="comebackStatus"></param>
        /// <param name="comebackReasonId"></param>
        /// <param name="comebackDate"></param>
        /// <param name="ComebackMileageIn"></param>
        /// <param name="ComebackDealerId"></param>
        /// <param name="chargeBackCommission"></param>
        /// <param name="comebackComments"></param>
        /// <param name="inventoryId"></param>
        /// <returns>0/1</returns>
        public int UpdateComeBackDetails(bool comebackStatus, int comebackReasonId, DateTime? comebackDate,
                long ComebackMileageIn, long ComebackDealerId, decimal chargeBackCommission, string comebackComments, long inventoryId, long addedBy, DateTime addedDate, long? modifiedBy, DateTime? modifiedDate, long? deletedBy, DateTime? dateDeleted, bool IsActive, ref string strOutErrorMessage)
        {

            return METAOPTION.DALDataContext.Factory.DB.ComeBackStatusUpdate(comebackStatus, comebackReasonId, comebackDate,
                   ComebackMileageIn, ComebackDealerId, chargeBackCommission, comebackComments, inventoryId, addedBy, addedDate, modifiedBy, modifiedDate, deletedBy, dateDeleted, IsActive, ref strOutErrorMessage);

        }
        #endregion


        #region [Get Inventory Drivers Count]
        public int GetDriversCount(long InventoryId)
        {
            List<DriversCountByInventoryIdResult> Result = METAOPTION.DALDataContext.Factory.DB.DriversCountByInventoryId(InventoryId).ToList();
            if (Result.Count > 0)
                return Result.First().TotalDrivers.Value;
            else return 0;
        }
        #endregion

        #region [Get Inventory Expenses Count]
        public int GetExpensesCount(long InventoryId)
        {
            List<Expense_CountByInventoryIdResult> Result = METAOPTION.DALDataContext.Factory.DB.Expense_CountByInventoryId(InventoryId).ToList();

            if (Result.Count > 0)
                return Result.First().TotalExpense.Value;
            else
                return 0;
        }

        #endregion

        /// <summary>
        /// Select Comeback Details
        /// </summary>
        /// <param name="InventoryId"></param>
        /// <returns>List Containing Comeback Details</returns>
        public List<string> ComebackDetailSelect(long InventoryId)
        {

            var result = METAOPTION.DALDataContext.Factory.DB.ComebackDetailsSelect(InventoryId).AsQueryable();
            List<string> lstComeBackData = new List<string>();
            //To refer particular element take index
            foreach (ComebackDetailsSelectResult Item in result)
            {

                //Charge Back Commision [0]
                lstComeBackData.Add(Item.ChargebackCommision.ToString());

                //Comeback Comments [1]
                lstComeBackData.Add(Item.ComeBackComments);

                //ComebackDate [2]
                lstComeBackData.Add(Item.ComeBackDate.ToString());

                //Comeback DealerId [3]
                lstComeBackData.Add(Item.ComeBackDealerId.ToString());

                //Comeback MileageIn [4] 
                lstComeBackData.Add(Item.ComeBackMileageIn.ToString());

                //Comeback Reason ID [5]
                lstComeBackData.Add(Item.ComeBackReasonID.ToString());

                //Comeback Status [6]
                lstComeBackData.Add(Item.ComeBackStatus.ToString());

                //Last Updated Comeback History [7]
                lstComeBackData.Add(Item.UpdatedHistory);

                //Comeback Dealer Name [8]
                lstComeBackData.Add(Item.ComeBackDealerName);


            }
            return lstComeBackData;
        }

        //Adesh ,21 Jan,2013
        public List<string> ComebackDetailSelect(DataTable dt)
        {

            List<string> lstComeBackData = new List<string>();
            //To refer particular element take index
            foreach (DataRow dr in dt.Rows)
            {

                //Charge Back Commision [0]
                lstComeBackData.Add(Convert.ToString(dr["ChargebackCommision"]));

                //Comeback Comments [1]
                lstComeBackData.Add(dr["ComeBackComments"] == DBNull.Value ? null : Convert.ToString(dr["ComeBackComments"]));

                //ComebackDate [2]
                lstComeBackData.Add(Convert.ToString(dr["ComeBackDate"]));

                //Comeback DealerId [3]
                lstComeBackData.Add(Convert.ToString(dr["ComeBackDealerId"]));

                //Comeback MileageIn [4] 
                lstComeBackData.Add(Convert.ToString(dr["ComeBackMileageIn"]));

                //Comeback Reason ID [5]
                lstComeBackData.Add(Convert.ToString(dr["ComeBackReasonID"]));

                //Comeback Status [6]
                lstComeBackData.Add(Convert.ToString(dr["ComeBackStatus"]));

                //Last Updated Comeback History [7]
                lstComeBackData.Add(dr["UpdatedHistory"] == DBNull.Value ? null : Convert.ToString(dr["UpdatedHistory"]));

                //Comeback Dealer Name [8]
                lstComeBackData.Add(dr["ComeBackDealerName"] == DBNull.Value ? null : Convert.ToString(dr["ComeBackDealerName"]));


            }
            return lstComeBackData;
        }

        #region [Get Inventory Notes Count]
        public int GetInventoryNotes(long InventoryId)
        {
            List<Notes_CountByInventoryIdResult> result = METAOPTION.DALDataContext.Factory.DB.Notes_CountByInventoryId(InventoryId, 6).ToList();
            if (result.Count > 0)
                return result.First().TotalNote.Value;
            else
                return 0;
        }
        #endregion

        #region [Check Whether check paid against inventory by/for particular entity]
        /// <summary>
        /// Return 0 if check not paid else return 1
        /// </summary>
        /// <param name="InventoryId"></param>
        /// <returns>0/1</returns>
        public int IsCheckPaid(long InventoryId, long entityId, int entityTypeId, bool isCheckForAllEntity)
        {
            List<IsCheckPaidResult> result = METAOPTION.DALDataContext.Factory.DB.IsCheckPaid(InventoryId, entityId, entityTypeId, isCheckForAllEntity).ToList();
            if (result.Count > 0)
                return result.First().CheckPaid.Value;
            else
                return 0;
        }
        #endregion

        #region [Get Inventory Documents Count]
        public int GetInventoryDocs(long InventoryId)
        {
            List<Document_CountByEntityIdResult> result = METAOPTION.DALDataContext.Factory.DB.Document_CountByEntityId(6, InventoryId).ToList();
            if (result.Count > 0)
                return result.First().TotalDocument.Value;
            else
                return 0;
        }
        #endregion


        #region [Check Whether Inventory can be Move to Archieve]
        public bool CanMoveToArchieve(long InventoryId)
        {
            Nullable<bool> isSuccess = null;
            METAOPTION.DALDataContext.Factory.DB.CanInvMoveToArchieve(InventoryId, ref isSuccess);
            return isSuccess.Value;

        }

        //Adesh, 22 Jan 2013
        public bool CanMoveToArchieve(DataTable dt)
        {
            Nullable<bool> isSuccess = null;
            if (dt.Rows.Count > 0)
                isSuccess = Convert.ToBoolean(dt.Rows[0]["IsSuccess"]);
            return isSuccess.Value;

        }
        #endregion

        #region [Check Whether Inventory can be Move to Inventory]
        public bool CanMoveToInventory(long InventoryId)
        {
            Nullable<bool> isSuccess = null;
            METAOPTION.DALDataContext.Factory.DB.CanInvMoveToInventory(InventoryId, ref isSuccess);
            return isSuccess.Value;
        }

        //Adesh 22 Jan,2013
        public bool CanMoveToInventory(DataTable dt)
        {
            Nullable<bool> isSuccess = null;
            if (dt.Rows.Count > 0)
                isSuccess = Convert.ToBoolean(dt.Rows[0]["IsSuccess"]);
            return isSuccess.Value;
        }
        #endregion


        #region [Update CarStatus]
        /// <summary>
        /// Update Car Status (Mode=2) against InventoryId
        /// </summary>
        /// <param name="InventoryId"></param>
        /// <param name="carStatus"></param>
        /// <returns></returns>
        public int CarStatusUpdate(long InventoryId, int carStatus, long updatedBy, DateTime updatedDate)
        {
            METAOPTION.DALDataContext.Factory.DB.CarStatusSelectUpdate(InventoryId, carStatus, updatedBy, updatedDate, 2);
            return 1;
        }
        #endregion


        #region [Select CarStatus]
        /// <summary>
        /// Select Car Status (Mode=1) against InventoryId from InventoryTable
        /// </summary>
        /// <param name="InventoryId"></param>
        /// <returns></returns>
        public int CarStatusSelect(long InventoryId)
        {

            List<CarStatusSelectUpdateResult> carStatus = METAOPTION.DALDataContext.Factory.DB.CarStatusSelectUpdate(InventoryId, null, null, null, 1).ToList();
            if ((carStatus).Count > 0)
                return carStatus.First().CarStatus.Value;
            else
                return 0;

        }

        //Adesh 22 Jan,2013
        public int CarStatusSelect(DataTable dt)
        {
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["CarStatus"]);
            else
                return 0;
        }
        #endregion

        #region [Inventory Expenses]
        /// <summary>
        /// Add New Expense
        /// </summary>
        /// <param name="objExpense"></param>
        /// <returns>ExpenseId</returns>
        public long AddNewExpense(Expense objExpense)
        {
            Nullable<long> ExpenseId = null;
            METAOPTION.DALDataContext.Factory.DB.ExpenseInsertUpdate(ref ExpenseId, null, objExpense.InventoryId, objExpense.ExpenseDate, objExpense.EntityId,
                                         objExpense.EntityTypeId, objExpense.ExpenseTypeId, objExpense.ExpenseAmount,
                                         objExpense.Comments, objExpense.CheckPaid, null,
                                         objExpense.DateAdded, objExpense.AddedBy, null,
                                         null, null, null,
                                         1, 1, objExpense.InvoiceNo);
            return ExpenseId.Value;
        }

        /// <summary>
        /// Search Expenses by InventoryId
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public IEnumerable Expense_SearchByInventoryId(long inventoryId, Int16 OrgID)
        {
            return METAOPTION.DALDataContext.Factory.DB.Expense_SearchByInventoryId(inventoryId, OrgID).AsEnumerable();
        }

        public DataTable Expense_SearchByInventoryId(long inventoryId, Int32 EntityID, Int32 ParentEntityID, Int32 EntityTypeID)
        {
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Expense_SearchByInventoryId_ver211", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@InventoryId", inventoryId);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);

                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        /// <summary>
        /// Update Expense
        /// </summary>
        /// <param name="objExpense"></param>
        /// <returns>0/1</returns>
        public int UpdateExpense(Expense objExpense, long ExpenseId)
        {
            Nullable<long> tempField = null;
            return METAOPTION.DALDataContext.Factory.DB.ExpenseInsertUpdate(ref tempField, ExpenseId, objExpense.InventoryId, objExpense.ExpenseDate, null,
                                        null, objExpense.ExpenseTypeId, objExpense.ExpenseAmount,
                                        objExpense.Comments, null, null,
                                        null, null, objExpense.DateModified,
                                        objExpense.ModifiedBy, null, null,
                                        1, 2, objExpense.InvoiceNo);
        }

        /// <summary>
        /// Delete Expense
        /// </summary>
        /// <param name="objExpense"></param>
        /// <returns>0/1</returns>
        public int DeleteExpense(Expense objExpense, long ExpenseId)
        {
            Nullable<long> tempField = null;
            return METAOPTION.DALDataContext.Factory.DB.ExpenseInsertUpdate(ref tempField, ExpenseId, null, null, null,
                                        null, null, null,
                                        null, null, null,
                                        null, null, null,
                                        null, objExpense.DateDeleted, objExpense.DeletedBy,
                                        0, 3, objExpense.InvoiceNo);
        }

        /// <summary>
        /// List of Vendors 
        /// </summary>
        /// <returns>List of Vendors</returns>
        public IEnumerable GetVendorList(short OrgID)
        {
            return METAOPTION.DALDataContext.Factory.DB.GetVendorList(OrgID).AsEnumerable();
        }




        /// <summary>
        /// Get List of Expenses
        /// </summary>
        /// <returns></returns>
        public IQueryable GetExpenses()
        {
            return METAOPTION.DALDataContext.Factory.DB.GetExpenses().AsQueryable();

        }
        #endregion

        #region [Add New Inventory]
        //DALDataContext METAOPTION.DALDataContext.Factory.DB. = new DALDataContext();
        /// <summary>
        /// This method Add new entry in inventory table and return Inventory Id Generated
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Return System Generated Inventory Id</returns>
        public long AddInventory(Inventory obj)
        {
            Nullable<long> invt = null;
            long inventoryId = 0;
            METAOPTION.DALDataContext.Factory.DB.InventoryInsert(
                   ref invt
                  , obj.OldInventoryId
                  , obj.VIN
                  , obj.AverageMMR
                  , obj.Year
                  , obj.MileageIn
                  , obj.ArrivalDate
                  , obj.MakeId
                  , obj.ModelId
                  , obj.BodyId
                  , obj.DesignatedEquipment
                  , obj.CarNote
                  , obj.ExtColorId
                  , obj.IntColorId
                  , obj.VehiclePresent
                  , obj.EngineId
                  , obj.TransId
                  , obj.WheelDriveId
                  , obj.Leather
                  , obj.SunMoon
                  , obj.Navigation
                  , obj.AlloyWheels
                  , obj.PowerLocks
                  , obj.PowerWindows
                  , obj.AC
                  , obj.PowerSeat
                  , obj.RegularLane
                  , obj.ExoticLane
                  , obj.VirtualLane
                  , obj.OnlineLane
                  , obj.IsExotic
                  , obj.DealerId
                  , obj.DesignationId
                  , obj.BuyerId
                  , obj.CarCost
                  , obj.PurchaseDate
                  , obj.TitlePresent
                  , obj.TitlePresentNotes
                  , obj.TitleShipped
                  , obj.TitleShippedNotes
                  , obj.CustomerID
                  , obj.SoldDate
                  , obj.MarketPrice
                  , obj.SoldPrice
                  , obj.MileageOut
                  , obj.CheckNumber
                  , obj.DepositDate
                  , obj.DepositAmount
                  , obj.BankAccountId
                  , obj.DepositComment
                  , obj.SoldStatus
                  , obj.SoldComment
                  , obj.ComeBackStatus
                  , obj.ComeBackReasonID
                  , obj.ComeBackDate
                  , obj.ComeBackMileageIn
                  , obj.ComeBackDealerId
                  , obj.ChargebackCommision
                  , obj.ComeBackComments
                  , obj.CarStatus
                  , obj.CarLocation
                  , DateTime.Now
                  , obj.AddedBy
                  , 1
                  , obj.SystemID
                  , obj.Grade
                  , obj.BadCarFax
                  , obj.BadAutoCheck
                  , obj.OrgID);

            if (invt.HasValue)
                inventoryId = invt.Value;

            return inventoryId;
        }
        #endregion

        #region[ SearchDealer - Overloaded method ]
        /// <summary>
        /// Search dealer based on Dealer Name,Address,City,State,Zip Provided
        /// </summary>
        /// <param name="strDealerName"></param>
        /// <param name="strCity"></param>
        /// <param name="dealerStateId"></param>
        /// <param name="zip"></param>
        /// <returns>List of Searched Dealers</returns>
        public IEnumerable SearchDealer(string strDealerName, string strCity, int dealerStateId, string zip, short orgID)
        {
            return METAOPTION.DALDataContext.Factory.DB.DealerSearch(strDealerName, strCity, dealerStateId, zip, -1, orgID).AsEnumerable();
        }
        #endregion
        #region[ SearchDealer - Overloaded method ]
        public IEnumerable SearchDealer(string strDealerName, string strCity, int dealerStateId, string zip, int CountryId, short orgID)
        {
            return METAOPTION.DALDataContext.Factory.DB.DealerSearch(strDealerName, strCity, dealerStateId, zip, CountryId, orgID).AsEnumerable();
        }
        #endregion
        #region[ SearchDealer  - With Custom Paging ]
        /// <summary>
        /// SearchDealer  - With Custom Paging
        /// </summary>
        /// <param name="strDealerName"> Dealer Name</param>
        /// <param name="strCity">City</param>
        /// <param name="dealerStateId"> Dealer State Id</param>
        /// <param name="zip">Zip</param>
        /// <param name="CountryId">Country Id</param>
        /// <param name="StartRowIndex">Start Row Number</param>
        /// <param name="MaximumRows"> Maximum Row Number</param>
        /// <returns>List of Dealers</returns>
        public IEnumerable SearchDealerPaged(string strDealerName
                    , string strCity
                    , int dealerStateId
                    , string zip
                    , int CountryId
                    , Int32 StartRowIndex
                    , Int32 MaximumRows
            , short OrgID)
        {
            return METAOPTION.DALDataContext.Factory.DB.DealerSearchPaged(strDealerName, strCity, dealerStateId, zip, CountryId, StartRowIndex, MaximumRows, OrgID).AsEnumerable();
        }

        #region[ SearchDealer Count - Custom Paging Count ]
        /// <summary>
        /// SearchDealer  - With Custom Paging
        /// </summary>
        /// <param name="strDealerName"> Dealer Name</param>
        /// <param name="strCity">City</param>
        /// <param name="dealerStateId"> Dealer State Id</param>
        /// <param name="zip">Zip</param>
        /// <param name="CountryId">Country Id</param>
        /// <param name="StartRowIndex">Start Row Number</param>
        /// <param name="MaximumRows"> Maximum Row Number</param>
        /// <returns>Total Count</returns>
        public Int32? SearchDealerPagedCount(string strDealerName
                     , string strCity
                    , int dealerStateId
                    , string zip
                    , int CountryId
                    , Int32 StartRowIndex
                    , Int32 MaximumRows
            , short OrgID)
        {
            DealerSearchPagedCountResult result = METAOPTION.DALDataContext.Factory.DB.DealerSearchPagedCount(strDealerName, strCity, dealerStateId, zip, CountryId, StartRowIndex, MaximumRows, OrgID).Single();

            return result.TotalCount;
        }
        #endregion
        #endregion


        #region[ Get Linked Cars ]
        /// <summary>
        /// This function retreives list of Linked cars(Select inventories where comeback status=true
        /// </summary>
        /// <param name="?"></param>
        /// <returns>List of inventories where comeback status =true</returns>
        public IEnumerable GetLinkedCars(string strVIN)
        {
            return METAOPTION.DALDataContext.Factory.DB.GetLinkedCars(strVIN);
        }
        #endregion
        #region [Inventory Drivers]
        #region[ Search Inventory Drivers by Inventory Id ]
        /// <summary>
        /// Search Inventory Drivers by Inventory Id
        /// </summary> /// </summary>
        /// <param name="invnetoryIdr">InventoryId</param>
        /// <returns>List of Drivers</returns>
        public IEnumerable SearchInventoryDriverByInventoryId(long inventoryId, Int16 OrgID)
        {
            return METAOPTION.DALDataContext.Factory.DB.spInventoryDriverByInventoryId(inventoryId, OrgID).AsEnumerable();
        }
        #endregion

        #region[ Get List of Drivers ]
        /// <summary>
        /// Search Inventory Drivers by Inventory Id
        /// </summary> /// </summary>
        /// <param name="invnetoryIdr">InventoryId</param>
        /// <returns>List of Drivers</returns>
        public IQueryable GetDriverList(Int16 OrgID)
        {
            return METAOPTION.DALDataContext.Factory.DB.GetDriverList(OrgID).AsQueryable();
        }
        #endregion


        #region[ Add InventoryDriver ]
        /// <summary>
        /// Search Inventory Drivers by Inventory Id
        /// </summary> /// </summary>
        /// <param name="invnetoryIdr">InventoryId</param>
        /// <returns>InventoryDriverId generated</returns>
        public long AddInventoryDriver(InventoryDriver objDriver)
        {
            //This is the InventoryDriverId generated and return by sql 
            Nullable<long> InventoryDriverId = null;

            //Call DAL method to Insert Driver Record in db,Pass mode=1 last parameter for Insert
            METAOPTION.DALDataContext.Factory.DB.InventDriverInsertUpdateDelete(ref InventoryDriverId, null, objDriver.InventoryId,
                  objDriver.DriverId, objDriver.StartLocation, objDriver.EndLocation, objDriver.StartLocationDate,
                  objDriver.EndLocationDate, objDriver.Comments, objDriver.DateAdded, objDriver.AddedBy, null,
                  null, null, null, 1, 1, objDriver.EntityTypeID, objDriver.ContactID);

            if (InventoryDriverId.HasValue)
                return InventoryDriverId.Value;
            else return 0;

        }
        #endregion

        #region Update Inventory Driver Details (InventoryDriver table)
        /// <summary>
        /// Update Inventory Driver Details
        /// </summary>
        /// <returns>0/1</returns>
        public int UpdateInventoryDriver(InventoryDriver objDriver, long InvUD)
        {
            Nullable<long> TempDriverId = null;

            //Call DAL method to Insert Driver Record in db; Pass Mode=2 last parameter for Update
            return METAOPTION.DALDataContext.Factory.DB.InventDriverInsertUpdateDelete(ref TempDriverId, InvUD, objDriver.InventoryId,
                  null, objDriver.StartLocation, objDriver.EndLocation, objDriver.StartLocationDate,
                  objDriver.EndLocationDate, objDriver.Comments, null, null,
                  objDriver.DateModified, objDriver.ModifiedBy, null, null, 1, 2, objDriver.EntityTypeID, objDriver.ContactID);

        }
        #endregion

        #region Delete Inventory Driver(Mark IsActive=0 against particular inventorydriver record in InventoryDriver Table)
        /// <summary>
        /// Delete Inventory Driver (Mark IsActive flag to 0)
        /// </summary>
        /// <returns>0/1</returns>
        public int DeleteInventoryDriver(InventoryDriver objDriver, long InvUD)
        {
            Nullable<long> TempDriverId = null;
            //Call DAL method to Insert Driver Record in db; Pass Mode=3 last parameter for Delete(IsActive=0)
            return METAOPTION.DALDataContext.Factory.DB.InventDriverInsertUpdateDelete(ref TempDriverId, InvUD, null,
                  null, null, null, null,
                  null, null, null, null,
                  null, null, objDriver.DateDeleted, objDriver.DeletedBy, 0, 3, objDriver.EntityTypeID, objDriver.ContactID);

        }
        #endregion

        #region [Inventory Documents]
        #region[ Get all documents by Inventory Id ]
        /// <summary>
        /// Search Documents by passing EntityTypeId and EntityId
        /// </summary>
        /// <param name="EntityTypeId">Entity TypeI d</param>
        /// <param name="EntityId">Entity Id</param>
        /// <returns>List of documents in IQueryable format</returns>
        public IEnumerable Search_DocumentsByEntityId(long entityTypeId, long entityId)
        {

            return METAOPTION.DALDataContext.Factory.DB.Document_SearchByEntityId(entityTypeId, entityId).AsEnumerable();
        }

        public DataTable Search_DocumentsByEntityId(long InventoryID, Int32 SecurityUserID, Int32 ParentEntityID, Int32 EntityTypeID)
        {
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Document_SearchByEntityId_ver211", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@InventoryID", InventoryID);
                Cmd.Parameters.AddWithValue("@SecurityUserID", SecurityUserID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;

        }
        #endregion

        #region[ Add document to database ]
        /// <summary>
        /// Add document to database 
        /// </summary>
        /// <param name="doc"> Document </param>
        /// <returns>Integer </returns>
        public Int64 Document_Add(Document doc)
        {

            long? docId = null;
            return METAOPTION.DALDataContext.Factory.DB.Document_Insert(ref docId
               , doc.EntityId
               , doc.EntityTypeId
               , doc.DocumentTypeId
               , doc.DocumentTitle
               , doc.DocumentName
               , doc.Description
               , doc.DocumentBinary
               , doc.FileType
               , doc.AddedBy);

        }

        //public Int64 Document_Add(Document doc)
        //{
        //    DALDataContext objDal = new DALDataContext();
        //    Int64 Result = 0;

        //    using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
        //    {
        //        if (Conn.State == ConnectionState.Closed)
        //            Conn.Open();

        //        SqlCommand Cmd = new SqlCommand("Document_Insert", Conn);
        //        Cmd.Parameters.AddWithValue("@EntityId", doc.EntityId);
        //        Cmd.Parameters.AddWithValue("@EntityTypeId", doc.EntityTypeId);
        //        Cmd.Parameters.AddWithValue("@DocumentTypeId", doc.DocumentTypeId);
        //        Cmd.Parameters.AddWithValue("@DocumentTitle", doc.DocumentTitle);
        //        Cmd.Parameters.AddWithValue("@DocumentName", doc.DocumentName);
        //        Cmd.Parameters.AddWithValue("@Description", doc.Description);
        //        Cmd.Parameters.AddWithValue("@DocumentBinary", doc.DocumentBinary);
        //        Cmd.Parameters.AddWithValue("@FileType", doc.FileType);
        //        Cmd.Parameters.AddWithValue("@AddedBy", doc.AddedBy);

        //        SqlParameter ouparm = Cmd.Parameters.Add("@DocumentId", SqlDbType.BigInt);
        //        ouparm.Direction = ParameterDirection.Output;

        //        Cmd.CommandType = CommandType.StoredProcedure;
        //        Cmd.ExecuteNonQuery();
        //        Result = Convert.ToInt32(Cmd.Parameters["@DocumentId"].Value);

        //        objDal.Dispose();
        //    }
        //    return Result;
        //}
        #endregion

        #region[ Update document to database ]
        /// <summary>
        /// Update document to database 
        /// </summary>
        /// <param name="doc"> Document </param>
        /// <returns>Integer</returns>
        public int Document_Update(Document doc)
        {

            return METAOPTION.DALDataContext.Factory.DB.Document_Update(
                doc.DocumentId
              , doc.DocumentTypeId
              , doc.DocumentTitle
              , doc.Description
              , doc.ModifiedBy);

        }
        #endregion

        #region[ Open document by id from database ]
        /// <summary>
        /// Open document by id from database 
        /// </summary>
        /// <param name="documentId">document Id</param>
        public DataTable DocumentOpenById(Int32 documentId)
        {

            DataTable dTab = new DataTable("Documents");
            using (SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["DBMetaoptionConnection"].ToString()))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("DocumentOpenById", Conn);
                Cmd.Parameters.AddWithValue("@DocumentId", documentId);
                Cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dReader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(dReader);
                dReader.Close();
            }
            return dTab;
        }
        #endregion

        #region[ Delete Inventory Document(Mark IsActive=0 ]
        /// <summary>
        /// Delete Inventory Document(Mark IsActive=0
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="InvUD"></param>
        /// <returns></returns>
        public long DeleteInventoryDoc(long DocumentId, long DeletedBy)
        {

            return METAOPTION.DALDataContext.Factory.DB.Document_Delete(DocumentId, DeletedBy);
        }
        #endregion

        #endregion
        #endregion

        #region [Inventory Notes]
        public IEnumerable Notes_SearchByInventoryId(Int32 entityId, Int32 entityTypeId)
        {
            return METAOPTION.DALDataContext.Factory.DB.Notes_SearchByInventoryId(entityId, entityTypeId).AsEnumerable();
        }
        /// <summary>
        /// Add Inventory Notes against EntityId
        /// </summary>
        /// <param name="objNote"></param>
        /// <returns>NoteID generated from notes table</returns>
        public long AddNotes(Note objNote)
        {
            Nullable<long> noteId = null;
            METAOPTION.DALDataContext.Factory.DB.NoteInsertUpdateDelete(ref noteId, null, objNote.EntityId, objNote.EntityTypeId,
                objNote.Notes, objNote.SecurityUserId, objNote.DateAdded, objNote.AddedBy, null, null, null, null, 1, 1, objNote.NoteTypeID);
            if (noteId.HasValue)
                return noteId.Value;
            else return 0;
        }

        /// <summary>
        /// return specialcase note id, if exists against inventoryid
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns>special case note id</returns>
        public long IsSpecialCaseNoteExists(long inventoryId)
        {
            return METAOPTION.DALDataContext.Factory.DB.SpecialNotesExist(inventoryId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objNote"></param>
        /// <param name="updateNoteId"></param>
        /// <returns></returns>
        public int UpdateNote(Note objNote, Int32 Mode)
        {
            Nullable<long> tempNoteId = null;
            return METAOPTION.DALDataContext.Factory.DB.NoteInsertUpdateDelete(ref tempNoteId,
                  objNote.NoteId
                , objNote.EntityId
                , objNote.EntityTypeId
                , objNote.Notes
                , objNote.SecurityUserId
                , objNote.DateAdded
                , objNote.AddedBy
                , objNote.ModifiedBy
                , objNote.DateModified
                , objNote.DeletedBy
                , objNote.DateDeleted
                , objNote.IsActive
                , Mode
                , objNote.NoteTypeID);

        }
        /// <summary>
        /// Add Inventory Notes against EntityId
        /// </summary>
        /// <param name="objNote"></param>
        /// <returns>NoteID generated from notes table</returns>
        public int UpdateNotes(Note objNote, long updateNoteId)
        {
            Nullable<long> tempNoteId = null;
            return METAOPTION.DALDataContext.Factory.DB.NoteInsertUpdateDelete(ref tempNoteId, updateNoteId, objNote.EntityId, objNote.EntityTypeId,
                objNote.Notes, objNote.SecurityUserId, null, null, objNote.ModifiedBy, objNote.DateModified, null, null, 1, 2, objNote.NoteTypeID);

        }

        /// <summary>
        /// Add Inventory Notes against EntityId
        /// </summary>
        /// <param name="objNote"></param>
        /// <returns>NoteID generated from notes table</returns>
        public int DeleteNotes(Note objNote, long deleteNoteId)
        {
            Nullable<long> tempNoteId = null;
            return METAOPTION.DALDataContext.Factory.DB.NoteInsertUpdateDelete(ref tempNoteId, deleteNoteId, objNote.EntityId, objNote.EntityTypeId,
                objNote.Notes, objNote.SecurityUserId, null, null, null, null, objNote.DeletedBy, objNote.DateDeleted, 0, 3, objNote.NoteTypeID);

        }

        #region [Get Note by Note ID]
        public string GetNoteByID(long NoteId)
        {
            string result = (from p in METAOPTION.DALDataContext.Factory.DB.Notes
                             where p.NoteId == NoteId
                             select p.Notes).Single();
            return result;


        }
        #endregion


        #region [Validate VIN]
        /// <summary>
        /// Return list of matched VinNo
        /// </summary>
        /// <param name="strVinNo"></param>
        /// <returns>Return list of matched VinNo</returns>
        public IQueryable GetMatchedVinNumbers(string strVinNo)
        {
            IQueryable result =
            METAOPTION.DALDataContext.Factory.DB.GetMatchedVinNumbers(strVinNo).AsQueryable();
            return result;
        }
        #endregion

        #endregion

        #region [Get Dealer Details Resultset w.r.t InventoryId]
        /// <summary>
        /// Return dealer details
        /// </summary>
        /// <param name="Dealerid"></param>
        /// <returns></returns>
        public static DealerDetailsSelectResult GetDealerForInv(long invId)
        {
            DealerDetailsSelectResult result = METAOPTION.DALDataContext.Factory.DB.DealerDetailsSelect(invId).SingleOrDefault();
            return result;
        }

        //Adesh 22 Jan,2013
        public static DealerDetailsSelectResult GetDealerForInv(DataTable dt)
        {
            List<DealerDetailsSelectResult> lst = Common.ToCollection<DealerDetailsSelectResult>(dt);
            DealerDetailsSelectResult result = lst.SingleOrDefault();
            return result;
        }
        #endregion

        #region [Inventory Header Details for Inventory Documents/Expense/Drivers/Notes Screen]
        /// <summary>
        /// This method will provide inventory information like year, make, model with VIN# for
        /// Making Header on Inventory Documents/Expense/Drivers/Notes Screen
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public static CarDetailsSelectResult GetCurrentInventoryHeader(long inventoryId)
        {

            CarDetailsSelectResult objResult = METAOPTION.DALDataContext.Factory.DB.CarDetailsSelect(inventoryId).SingleOrDefault();

            return objResult;

        }
        #endregion


        #region [Delete Inventory]
        /// <summary>
        /// Delete Inventory
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <param name="deletedBy"></param>
        /// <param name="deletedDate"></param>
        /// <returns></returns>
        public int DeleteInventory(long inventoryId, long deletedBy, DateTime deletedDate)
        {
            return METAOPTION.DALDataContext.Factory.DB.InventoryDelete(inventoryId, deletedBy, deletedDate);
        }
        #endregion

        #region [Inventory Quick Search]
        /// <summary>
        /// This method  Quick Search Inventory results
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public static QuickInventorySearchResult[] QuickSearchInventories(int vinMatch, string VINNO, long UserID, Int16 OrgID)
        {
            QuickInventorySearchResult[] objResult = METAOPTION.DALDataContext.Factory.DB.QuickInventorySearch(vinMatch, VINNO, UserID, OrgID).ToArray();
            return objResult;
        }
        #endregion



        #region [Get list of Vehicle history report options available]
        /// <summary>
        /// Get list of Vehicle history report options available
        /// </summary>
        /// <returns></returns>
        public static VehicleHistoryReportSelectResult[] GetVehicleHistoryReports()
        {

            return METAOPTION.DALDataContext.Factory.DB.VehicleHistoryReportSelect().ToArray();
        }
        #endregion

        #region[Inventory list sort options]
        /// <summary>
        /// Display sort options for inventory list
        /// </summary>
        /// <param name="strQuery"></param>
        /// <returns></returns>
        public DataTable InventoryList_SortOptions(String strQuery, String Sort1, String Sort2)
        {
            DataTable dTab = new DataTable("InvSort");
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

        #region[Search Inventory sort options]
        /// <summary>
        /// Display sort options for Search list
        /// </summary>
        /// <param name="strQuery"></param>
        /// <returns></returns>
        public DataTable SearchInventory_SortOptions(String strQuery, String Sort1, String Sort2)
        {
            DataTable dTab = new DataTable("SearchInvSort");
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

        public ExpandoObject CRInfo_ByInvID(long InvID)
        {
            DALDataContext db = new DALDataContext();
            var result = (from p in db.Inventories
                          where p.InventoryId == InvID
                          select new { p.CR_ID, p.CR_Status, p.CR_URL }).FirstOrDefault();
            if (result != null)
            {
                dynamic invCR = new ExpandoObject();
                invCR.CR_ID = result.CR_ID;
                invCR.CR_Status = result.CR_Status;
                invCR.CR_URL = result.CR_URL;
                return invCR;
            }
            else
                return null;
        }

        #region [Get Inventory Details by InventoryId]
        public static InventoryDetails_ByInventoryIDResult GetInventoryDetailsByInventoryID(long InventoryId)
        {
            InventoryDetails_ByInventoryIDResult result = METAOPTION.DALDataContext.Factory.DB.InventoryDetails_ByInventoryID(InventoryId).SingleOrDefault();
            return result;
        }
        #endregion

        #region [Get Expense by InventoryId]
        public IEnumerable GetExpense_ByInventoryID(long InventoryId)
        {
            DALDataContext objDAL = new DALDataContext();

            return objDAL.Expense_ByInventoryID(InventoryId).AsEnumerable();
        }
        public IEnumerable GetExpense_ByInventoryID(long InventoryId, String EntityTypeID)
        {
            DALDataContext objDAL = new DALDataContext();
            if (EntityTypeID == "1")
                return objDAL.Expense_Dealer_ByInventoryID(InventoryId).AsEnumerable();
            else
                return objDAL.Expense_ByInventoryID(InventoryId).AsEnumerable();
        }

        #endregion

        #region [Get Location by InventoryId]
        public IEnumerable GetLocation_ByInventoryID(long InventoryId)
        {
            DALDataContext objDAL = new DALDataContext();
            return objDAL.Location_ByInventoryID(InventoryId).AsEnumerable();
        }
        #endregion

        #region[Get VIN from Inventory ID]
        public String GetVINFromInventory(Int64 InvID)
        {
            String VIN = String.Empty;
            var result = (from v in METAOPTION.DALDataContext.Factory.DB.Inventories
                          where v.InventoryId == InvID
                          select new { v.VIN });
            if (result != null)
                VIN = result.FirstOrDefault().VIN;

            return VIN;
        }
        #endregion

        #region[Complete inventory info to show on Inventory Detail screen]
        public static System.Collections.ArrayList CompleteInventoryInfoByInventoryID(long InventoryID)
        {
            DataTable dTab = new DataTable();
            System.Collections.ArrayList array = new System.Collections.ArrayList();
            DALDataContext objDal = new DALDataContext();
            SqlDataReader reader = null;
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                try
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();

                    SqlCommand Cmd = new SqlCommand("Inventory_ViewInfo_ByInventoryID", Conn);
                    Cmd.Parameters.AddWithValue("@InventoryId", InventoryID);
                    Cmd.CommandType = CommandType.StoredProcedure;


                    reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    //CarDetails
                    dTab.Load(reader);
                    array.Add(dTab);

                    //CarProperty
                    dTab = new DataTable();
                    dTab.Load(reader);
                    array.Add(dTab);

                    //DealerDetails
                    dTab = new DataTable();
                    dTab.Load(reader);
                    array.Add(dTab);

                    //SoldTo
                    dTab = new DataTable();
                    dTab.Load(reader);
                    array.Add(dTab);

                }
                catch (Exception ex)
                { }
                finally
                {
                    reader.Close();
                    objDal.Dispose();
                    Conn.Close();
                }
            }
            return array;
        }
        #endregion

        #region [Table History Record Select against inventory id]
        public List<SoldToHistory_SelectResult> TableHistorySelect(long InventoryID)
        {
            DALDataContext objDal = new DALDataContext();
            return objDal.SoldToHistory_Select(InventoryID).ToList<SoldToHistory_SelectResult>();
        }
        #endregion

        #region[Count : Inventory, Expense, Document, Driver - By InvnetoryID]
        public List<Count_ExpInvDocDriverResult> Count_ExpInvDocDriver(Int64 InventoryID)
        {
            return METAOPTION.DALDataContext.Factory.DB.Count_ExpInvDocDriver(InventoryID).ToList<Count_ExpInvDocDriverResult>();
        }

        public DataTable Count_ExpInvDocDriver(Int64 InventoryID, Int32 SecurityUserID, Int32 ParentEntityID, Int32 EntityTypeID)
        {
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Count_ExpInvDocDriver_ver211", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@InventoryID", InventoryID);
                Cmd.Parameters.AddWithValue("@SecrityUserID", SecurityUserID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        #endregion

        public DataTable Exp_PreExp_ByInventoryID(long InventoryId)
        {
            DataTable dTab = new DataTable("ExpPreExpDetails");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("ExpDetails_ByInventoryID", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@InventoryID", InventoryId);
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        #region[Get Duplicate expense details]
        public DataTable GetDuplicateExpenseDetails(long EntityID, Int32 EntityTypeID, Int32 ExpenseTypeID, String Amount, long InventoryID, Int32 Period)
        {
            DataTable dTab = new DataTable("DupExpense");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Expense_CheckDuplicate", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                Cmd.Parameters.AddWithValue("@ExpenseTypeID", ExpenseTypeID);
                Cmd.Parameters.AddWithValue("@ExpenseAmount", Amount);
                Cmd.Parameters.AddWithValue("@InventoryID", InventoryID);
                Cmd.Parameters.AddWithValue("@Period", Period);
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region[Get common inventory details to show on mobile browser]
        public DataTable GetCommonInvDetail(long InventoryId, String VIN)
        {
            DataTable dTab = new DataTable("CommonInvDetails");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Inv_CommonDetail", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@InventoryID", InventoryId);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region [Get Location by VIN]
        public IEnumerable GetLocation_ByVIN(String VIN, Int16 OrgID)
        {
            DALDataContext objDAL = new DALDataContext();
            return objDAL.Location_ByVIN(VIN, OrgID).AsEnumerable();
        }
        #endregion


        #region [Added by Rupendra 15 Nov 12 for Export data]
        public DataTable SearchInventoryDataExport(Int32 VInMatch, String VINNo, String checkNo, Int32 FromYear, Int32 ToYear,
                                                    long makeId, long modelId, long BodyId, long dealerId, long customerId,
                                                    long buyerId, long designationId, int comeBack, int sold, Int32 CarStatus,
                                                    Int32 CRStatus, Int32 SystemID, String SortExpression, Int32 EntityTypeID,
                                                    Int32 EntityID, Int32 ParentEntityID, short OrgID, String SoldDateFrom,
                                                    String SoldDateTo
                                                   )
        {
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("InventorySearchExport_ver211", Conn);
                Cmd.Parameters.AddWithValue("@VINMatch", VInMatch);
                Cmd.Parameters.AddWithValue("@VINNO", VINNo);
                Cmd.Parameters.AddWithValue("@CheckNo", checkNo);
                Cmd.Parameters.AddWithValue("@FromYear", FromYear);
                Cmd.Parameters.AddWithValue("@ToYear", ToYear);
                Cmd.Parameters.AddWithValue("@MakeId", makeId);
                Cmd.Parameters.AddWithValue("@ModelId", modelId);
                Cmd.Parameters.AddWithValue("@BodyId", BodyId);
                Cmd.Parameters.AddWithValue("@DealerId", dealerId);
                Cmd.Parameters.AddWithValue("@CustomerId", customerId);
                Cmd.Parameters.AddWithValue("@BuyerId", buyerId);
                Cmd.Parameters.AddWithValue("@DesignationId", designationId);
                Cmd.Parameters.AddWithValue("@ComeBack", comeBack);
                Cmd.Parameters.AddWithValue("@Sold", sold);
                Cmd.Parameters.AddWithValue("@CarStatus", CarStatus);
                Cmd.Parameters.AddWithValue("@CRStatus", CRStatus);
                Cmd.Parameters.AddWithValue("@SystemID", SystemID);
                Cmd.Parameters.AddWithValue("@SortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.Parameters.AddWithValue("@SoldDateFrom", SoldDateFrom);
                Cmd.Parameters.AddWithValue("@SoldDateTo", SoldDateTo);


                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }


        public Int32 SearchInventoryDataExportCount(Int32 VInMatch, String VINNo, String checkNo, Int32 FromYear, Int32 ToYear,
                                                    long makeId, long modelId, long BodyId, long dealerId, long customerId,
                                                    long buyerId, long designationId, int comeBack, int sold, Int32 CarStatus,
                                                    Int32 CRStatus, Int32 SystemID, String SortExpression, Int32 EntityTypeID,
                                                    Int32 EntityID, Int32 ParentEntityID, short OrgID, String SoldDateFrom,
                                                    String SoldDateTo
                                                   )
        {
            Int32 Result = 0;

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("InventorySearchExportCount_Ver211", Conn);
                Cmd.Parameters.AddWithValue("@VINMatch", VInMatch);
                Cmd.Parameters.AddWithValue("@VINNO", VINNo);
                Cmd.Parameters.AddWithValue("@CheckNo", checkNo);
                Cmd.Parameters.AddWithValue("@FromYear", FromYear);
                Cmd.Parameters.AddWithValue("@ToYear", ToYear);
                Cmd.Parameters.AddWithValue("@MakeId", makeId);
                Cmd.Parameters.AddWithValue("@ModelId", modelId);
                Cmd.Parameters.AddWithValue("@BodyId", BodyId);
                Cmd.Parameters.AddWithValue("@DealerId", dealerId);
                Cmd.Parameters.AddWithValue("@CustomerId", customerId);
                Cmd.Parameters.AddWithValue("@BuyerId", buyerId);
                Cmd.Parameters.AddWithValue("@DesignationId", designationId);
                Cmd.Parameters.AddWithValue("@ComeBack", comeBack);
                Cmd.Parameters.AddWithValue("@Sold", sold);
                Cmd.Parameters.AddWithValue("@CarStatus", CarStatus);
                Cmd.Parameters.AddWithValue("@CRStatus", CRStatus);
                Cmd.Parameters.AddWithValue("@SystemID", SystemID);
                Cmd.Parameters.AddWithValue("@SortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.Parameters.AddWithValue("@SoldDateFrom", SoldDateFrom);
                Cmd.Parameters.AddWithValue("@SoldDateTo", SoldDateTo);
                Cmd.CommandType = CommandType.StoredProcedure;
                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }

        public Int32 SaveExportHistory(String attachment, Int32 rowCount, String DataContent, Int64 UserId)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Export_SaveHistory", Conn);

                Cmd.Parameters.AddWithValue("@FileName", attachment);
                Cmd.Parameters.AddWithValue("@RecordCount", rowCount);
                Cmd.Parameters.AddWithValue("@FileContent", DataContent);
                Cmd.Parameters.AddWithValue("@AddedBy", UserId);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteNonQuery();
                objDal.Dispose();
            }
            return Result;
        }

        public DataTable InventoryListDataExport(String VIN, Int32 Year, String Model, String Make, String Dealer, Int32 CarStatus, Int32 CRStatus, Int32 SystemID, String SortExpression, Int16 OrgID)
        {
            DataTable dTab = new DataTable("Inventories");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Export_InventoryList", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN == null ? "" : VIN);
                Cmd.Parameters.AddWithValue("@Year", Year == 0 ? -1 : Year);
                Cmd.Parameters.AddWithValue("@Model", Model == null ? "" : Model);
                Cmd.Parameters.AddWithValue("@Make", Make == null ? "" : Make);
                Cmd.Parameters.AddWithValue("@Dealer", Dealer == null ? "" : Dealer);
                Cmd.Parameters.AddWithValue("@CarStatus", CarStatus);
                Cmd.Parameters.AddWithValue("@CRStatus", CRStatus);
                Cmd.Parameters.AddWithValue("@SystemID", SystemID);
                Cmd.Parameters.AddWithValue("@SortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public Int32 InventoryListDataExportCount(String VIN, Int32 Year, String Model, String Make, String Dealer, Int32 CarStatus, Int32 CRStatus, Int32 SystemID, String SortExpression, Int16 OrgID)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Export_InventoryListCount", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN == null ? "" : VIN);
                Cmd.Parameters.AddWithValue("@Year", Year == 0 ? -1 : Year);
                Cmd.Parameters.AddWithValue("@Model", Model == null ? "" : Model);
                Cmd.Parameters.AddWithValue("@Make", Make == null ? "" : Make);
                Cmd.Parameters.AddWithValue("@Dealer", Dealer == null ? "" : Dealer);
                Cmd.Parameters.AddWithValue("@CarStatus", CarStatus);
                Cmd.Parameters.AddWithValue("@CRStatus", CRStatus);
                Cmd.Parameters.AddWithValue("@SystemID", SystemID);
                Cmd.Parameters.AddWithValue("@SortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }
        #endregion

        #region [Save Inventory Table Edit History]
        public int SaveInventoryHistory(Int64 InventoryID, String CarNoteOld, String CarNoteNew, String CarFaxOld, String CarFaxNew, String SoldStatusOld, String SoldStatusNew, String ComebackOld, String ComebackNew, Int64 UserId, String Source)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("InventoryEditHistory", Conn);

                Cmd.Parameters.AddWithValue("@InventoryId", InventoryID);
                Cmd.Parameters.AddWithValue("@CarNoteOld", CarNoteOld);
                Cmd.Parameters.AddWithValue("@CarNoteNew", CarNoteNew);
                Cmd.Parameters.AddWithValue("@CarFaxOld", CarFaxOld);
                Cmd.Parameters.AddWithValue("@CarFaxNew", CarFaxNew);
                Cmd.Parameters.AddWithValue("@SoldStatusOld", SoldStatusOld);
                Cmd.Parameters.AddWithValue("@SoldStatusNew", SoldStatusNew);
                Cmd.Parameters.AddWithValue("@ComebackOld", ComebackOld);
                Cmd.Parameters.AddWithValue("@ComebackNew", ComebackNew);
                Cmd.Parameters.AddWithValue("@UserId", UserId);
                Cmd.Parameters.AddWithValue("@Source", Source);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteNonQuery();
                objDal.Dispose();
            }
            return Result;
        }
        #endregion

        #region [Save Inventory Table Edit History by Vipin]
        public int SaveInventoryHistory(Int64 InventoryID, String CarNoteOld, String CarNoteNew, String CarFaxOld, String CarFaxNew, String SoldStatusOld, String SoldStatusNew, String ComebackOld, String ComebackNew, String CustomerIDNew, string CustomerIDOld, String DateSoldNew, string DateSoldOld,
            string MarketPriceNew, string MarkerPriceOld, string PriceSoldNew, string PriceSoldOld, string MileageOutNew, string MileageOutOld,
            String DepositeDateNew, string DepositeDateOld, string DepositeAmmountNew, string DepositeAmmountOld, string BankIDNew, String BankIDOld,
            string DepositeCommentNew, string DepositeCommentOld, string SoldCommentNew, string SoldCommentOld,
            Int64 UserId, String Source)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("InventoryEditHistory_ver211", Conn);

                Cmd.Parameters.AddWithValue("@InventoryId", InventoryID);
                Cmd.Parameters.AddWithValue("@CarNoteOld", CarNoteOld);
                Cmd.Parameters.AddWithValue("@CarNoteNew", CarNoteNew);
                Cmd.Parameters.AddWithValue("@CarFaxOld", CarFaxOld);
                Cmd.Parameters.AddWithValue("@CarFaxNew", CarFaxNew);
                Cmd.Parameters.AddWithValue("@SoldStatusOld", SoldStatusOld);
                Cmd.Parameters.AddWithValue("@SoldStatusNew", SoldStatusNew);
                Cmd.Parameters.AddWithValue("@ComebackOld", ComebackOld);
                Cmd.Parameters.AddWithValue("@ComebackNew", ComebackNew);
                Cmd.Parameters.AddWithValue("@CustIDNew", CustomerIDNew);
                Cmd.Parameters.AddWithValue("@CustIDOld", CustomerIDOld);
                Cmd.Parameters.AddWithValue("@DateSoldNew", DateSoldNew);
                Cmd.Parameters.AddWithValue("@DateSoldOld", DateSoldOld);
                Cmd.Parameters.AddWithValue("@MarketPriceNew", MarketPriceNew);
                Cmd.Parameters.AddWithValue("@MarketPriceOld", MarkerPriceOld);
                Cmd.Parameters.AddWithValue("@PriceSoldNew", PriceSoldNew);
                Cmd.Parameters.AddWithValue("@PriceSoldOld", PriceSoldOld);
                Cmd.Parameters.AddWithValue("@MileageOutNew", MileageOutNew);
                Cmd.Parameters.AddWithValue("@MileageOutOld", MileageOutOld);
                Cmd.Parameters.AddWithValue("@DepositeDateNew", DepositeDateNew);
                Cmd.Parameters.AddWithValue("@DepositeDateOld", DepositeDateOld);
                Cmd.Parameters.AddWithValue("@DepositeAmmountNew", DepositeAmmountNew);
                Cmd.Parameters.AddWithValue("@DepositeAmmountOld", DepositeAmmountOld);
                Cmd.Parameters.AddWithValue("@BankIDNew", BankIDNew);
                Cmd.Parameters.AddWithValue("@BankIDOld", BankIDOld);
                Cmd.Parameters.AddWithValue("@DepositCommentNew", DepositeCommentNew);
                Cmd.Parameters.AddWithValue("@DepositCommentOld", DepositeCommentOld);
                Cmd.Parameters.AddWithValue("@SoldCommentNew", SoldCommentNew);
                Cmd.Parameters.AddWithValue("@SoldCommentOld", SoldCommentOld);
                Cmd.Parameters.AddWithValue("@UserId", UserId);
                Cmd.Parameters.AddWithValue("@Source", Source);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteNonQuery();
                objDal.Dispose();
            }
            return Result;
        }
        #endregion

        #region[Get Complete Inventory Details-Added By Adesh 16 Jan,2013]
        public DataSet GetCompleteInventoryDetail(Int64 EmployeeID, String Page, Int64 InventoryID, String VIN)
        {
            DataTable dTab;
            System.Collections.ArrayList array = new System.Collections.ArrayList();
            DALDataContext objDal = new DALDataContext();
            SqlDataReader reader = null;
            DataSet ds = new DataSet();

            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                try
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();

                    SqlCommand Cmd = new SqlCommand("GetInventoryDetails", Conn);
                    Cmd.Parameters.AddWithValue("@EmployeeId", EmployeeID);
                    Cmd.Parameters.AddWithValue("@Page", Page);
                    Cmd.Parameters.AddWithValue("@InventoryId", InventoryID);
                    Cmd.Parameters.AddWithValue("@VIN", VIN);
                    Cmd.CommandType = CommandType.StoredProcedure;

                    reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    //Page Permissions- SP (GetPagePermission)
                    dTab = new DataTable("PagePermissions");
                    dTab.Load(reader);
                    ds.Tables.Add(dTab);

                    //CarDetailsSelect- SP (Inventory_ViewInfo_ByInventoryID)
                    dTab = new DataTable("CarDetails");
                    dTab.Load(reader);
                    ds.Tables.Add(dTab);

                    //CarPropertySelect- SP (Inventory_ViewInfo_ByInventoryID)
                    dTab = new DataTable("CarProperties");
                    dTab.Load(reader);
                    ds.Tables.Add(dTab);

                    //DealerDetailsSelect- SP (Inventory_ViewInfo_ByInventoryID)
                    dTab = new DataTable("DealerDetails");
                    dTab.Load(reader);
                    ds.Tables.Add(dTab);

                    //SoldToSelect- SP (Inventory_ViewInfo_ByInventoryID)
                    dTab = new DataTable("SoldTo");
                    dTab.Load(reader);
                    ds.Tables.Add(dTab);

                    //Linked Cars- SP (spInventoryLinked)
                    //dTab = new DataTable("LinkedCars");
                    //dTab.Load(reader);
                    //ds.Tables.Add(dTab);

                    //Moved To "Inventory" - SP (CanInvMoveToInventory_v211)
                    dTab = new DataTable("MoveTo");
                    dTab.Load(reader);
                    ds.Tables.Add(dTab);

                    //Moving Inventory from On-Hand,Archive etc.- SP (CarStatusSelectUpdate)
                    dTab = new DataTable("CarStatus");
                    dTab.Load(reader);
                    ds.Tables.Add(dTab);

                    //DealerDetails-SP (DealerDetailsSelect) 
                    dTab = new DataTable("DealerDetailsSelect");
                    dTab.Load(reader);
                    ds.Tables.Add(dTab);

                    //Car Properties- SP (CarPropertySelectBool)
                    dTab = new DataTable("CarPropertiesBool");
                    dTab.Load(reader);
                    ds.Tables.Add(dTab);

                    //Comeback Details- SP (ComebackDetailsSelect)
                    dTab = new DataTable("ComeBackDetailsSelect");
                    dTab.Load(reader);
                    ds.Tables.Add(dTab);

                    //Comeback Reasons- SP (ComeBackReasonSelect)
                    dTab = new DataTable("ComeBackReasonSelect");
                    dTab.Load(reader);
                    ds.Tables.Add(dTab);

                    //Get count of Total expense, note, documents, drivers by InvnetoryID- SP (Count_ExpInvDocDriver)
                    dTab = new DataTable("InventoryStats");
                    dTab.Load(reader);
                    ds.Tables.Add(dTab);

                    //Select & Update Sold Status Details(For SoldStatus Popup)-SP (SoldStatusSelectUpdate)
                    dTab = new DataTable("SoldStatus");
                    dTab.Load(reader);
                    ds.Tables.Add(dTab);

                    //Inventory can be move to archiev- SP (CanInvMoveToArchieve_v211)
                    dTab = new DataTable("MoveToArchive");
                    dTab.Load(reader);
                    ds.Tables.Add(dTab);

                    //Get PreInvID by InvID- SP (GetPreInvIDByInvID)
                    dTab = new DataTable("PreInvIDByInvID");
                    dTab.Load(reader);
                    ds.Tables.Add(dTab);

                    //Get CRID by InvID- SP (GetCRIDByInvID)
                    dTab = new DataTable("CRIDByInvID");
                    dTab.Load(reader);
                    ds.Tables.Add(dTab);
                }
                catch (Exception ex)
                { }
                finally
                {
                    reader.Close();
                    objDal.Dispose();
                    Conn.Close();
                }
            }
            return ds;
        }
        #endregion

        #region [View All Title Status - added Bu Rupendra 24 Jan 2013]
        public DataTable GetViewAllTitleStatus(String VIN
           , Int32 Year
           , Int32 MakeId
           , Int32 ModelId
           , Int32 DealerID
           , Int32 TitlePresent
           , DateTime DateFrom
           , DateTime DateTo
           , Int32 LateFee
           , Int32 CarStatus
           , Int32 LoginEntityTypeID
           , Int32 UserEntityID
           , Int32 BuyerParentID
           , String BuyerIsDirect
           , String BuyerAccessLevel
           , Int32 StartRowIndex
           , Int32 MaximumRows
           , Int32 SystemID
           , String SortExpression
            , short OrgID)
        {
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("ViewAllTitle_Select", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@Year", Year);
                Cmd.Parameters.AddWithValue("@Make", MakeId);
                Cmd.Parameters.AddWithValue("@Model", ModelId);
                Cmd.Parameters.AddWithValue("@DealerID", DealerID);
                Cmd.Parameters.AddWithValue("@CarStatus", CarStatus);
                Cmd.Parameters.AddWithValue("@TitlePresent", TitlePresent);
                Cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                Cmd.Parameters.AddWithValue("@DateTo", DateTo);
                Cmd.Parameters.AddWithValue("@LateFee", LateFee);

                Cmd.Parameters.AddWithValue("@LoginEntityTypeID", LoginEntityTypeID);
                Cmd.Parameters.AddWithValue("@UserEntityID", UserEntityID);
                Cmd.Parameters.AddWithValue("@BuyerParentID", BuyerParentID);
                Cmd.Parameters.AddWithValue("@BuyerIsDirect", BuyerIsDirect);
                Cmd.Parameters.AddWithValue("@BuyerAccessLevel", BuyerAccessLevel);

                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@SystemID", SystemID);
                Cmd.Parameters.AddWithValue("@SortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public Int32 GetViewAllTitleStatusCount(String VIN
          , Int32 Year
          , Int32 MakeId
          , Int32 ModelId
          , Int32 DealerID
          , Int32 TitlePresent
          , DateTime DateFrom
          , DateTime DateTo
          , Int32 LateFee
          , Int32 CarStatus
          , Int32 LoginEntityTypeID
          , Int32 UserEntityID
          , Int32 BuyerParentID
          , String BuyerIsDirect
          , String BuyerAccessLevel
          , Int32 StartRowIndex
          , Int32 MaximumRows
          , Int32 SystemID
          , String SortExpression
            , short OrgID)
        {
            DALDataContext objDal = new DALDataContext();
            Int32 Result = 0;
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("ViewAllTitleCount_Select", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@Year", Year);
                Cmd.Parameters.AddWithValue("@Make", MakeId);
                Cmd.Parameters.AddWithValue("@Model", ModelId);
                Cmd.Parameters.AddWithValue("@DealerID", DealerID);
                Cmd.Parameters.AddWithValue("@CarStatus", CarStatus);
                Cmd.Parameters.AddWithValue("@TitlePresent", TitlePresent);
                Cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                Cmd.Parameters.AddWithValue("@DateTo", DateTo);
                Cmd.Parameters.AddWithValue("@LateFee", LateFee);

                Cmd.Parameters.AddWithValue("@LoginEntityTypeID", LoginEntityTypeID);
                Cmd.Parameters.AddWithValue("@UserEntityID", UserEntityID);
                Cmd.Parameters.AddWithValue("@BuyerParentID", BuyerParentID);
                Cmd.Parameters.AddWithValue("@BuyerIsDirect", BuyerIsDirect);
                Cmd.Parameters.AddWithValue("@BuyerAccessLevel", BuyerAccessLevel);

                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@SystemID", SystemID);
                Cmd.Parameters.AddWithValue("@SortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                Result = (Int32)Cmd.ExecuteScalar();

            }
            return Result;
        }
        #endregion

        #region [Update Title Present Note, Added by Rupendra 1 Feb 2012]

        public Int32 UpdateTitlePresentTrack(Int64 InventoryId, Int32 TitleValue, Int64 ModifiedBy)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Inventory_TitlePresentTrack", Conn);

                Cmd.Parameters.AddWithValue("@InventoryId", InventoryId);
                Cmd.Parameters.AddWithValue("@TitleValue", TitleValue);
                Cmd.Parameters.AddWithValue("@Modifiedby", ModifiedBy);
                Cmd.CommandType = CommandType.StoredProcedure;
                Result = (Int32)Cmd.ExecuteNonQuery();
                objDal.Dispose();
            }
            return Result;

        }
        #endregion

        #region[Audit by InventoryID]
        public DataTable InventoryAudit(long InventoryID)
        {
            DataTable dTab = new DataTable("InvHistory");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("TableHistory_ByInvID", Conn);
                Cmd.Parameters.AddWithValue("@InvID", InventoryID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region[Lane History by InventoryID]
        public DataTable InventoryLaneHistory(long InventoryID)
        {
            DataTable dTab = new DataTable("InvLaneHistory");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("LaneHistory_ByInvID", Conn);
                Cmd.Parameters.AddWithValue("@InventoryID", InventoryID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region [Inventory Quick Search TO Scan]
        public static QuickInventorySearch_ScanResult[] QuickSearchScanInventories(int vinMatch, string VINNO, Int16 OrgID, long UserID)
        {
            QuickInventorySearch_ScanResult[] objResult = METAOPTION.DALDataContext.Factory.DB.QuickInventorySearch_Scan(vinMatch, VINNO, OrgID, UserID).ToArray();
            return objResult;
        }
        #endregion
    }
}
