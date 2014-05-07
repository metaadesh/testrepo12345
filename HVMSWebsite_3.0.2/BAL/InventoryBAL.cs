using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using METAOPTION.DAL;
using System.Dynamic;
namespace METAOPTION.BAL
{

    /// <summary>
    /// This buisness class contain methods for manage inventory page
    /// </summary>
    public class InventoryBAL
    {


        /// <summary>
        /// Get Car Details
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>

        public static IQueryable GetCarDetails(long? inventoryId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.GetCarDetails(inventoryId);
        }

        #region [Check Payment]
        public int CheckPayemnt(Int64 InventoryId)
        {
            InventoryDAL obj = new InventoryDAL();
            return obj.CheckPayemnt(InventoryId);
        }
        #endregion

        /// <summary>
        /// Select Comeback Details
        /// </summary>
        /// <param name="InventoryId"></param>
        /// <returns>List Containing Comeback Details</returns>
        public static List<string> ComebackDetailSelect(long InventoryId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.ComebackDetailSelect(InventoryId);

        }

        public static List<string> ComebackDetailSelect(DataTable dt)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.ComebackDetailSelect(dt);

        }
        /// <summary>
        /// Get Chrome details for filling dropdownlist SelectedValue Property in Edit Popup
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>

        public static List<string> GetChromeDetails(long? inventoryId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            var result = objDAL.GetCarDetails(inventoryId).AsQueryable();
            List<string> lstChromeData = new List<string>();
            foreach (CarDetailsSelectResult objResult in result)
            {
                //Year at Index 0
                lstChromeData.Add(objResult.Year.ToString());

                //MakeId at Index 1
                lstChromeData.Add(objResult.MakeId.ToString());

                //ModelId at Index 2
                lstChromeData.Add(objResult.ModelId.ToString());

                //BodyId at Index3
                lstChromeData.Add(objResult.BodyId.ToString());

                //TransId at Index4
                lstChromeData.Add(objResult.TransId.ToString());

                //EngineId at Index 5
                lstChromeData.Add(objResult.EngineId.ToString());

                //ExtColorId at Index 6
                lstChromeData.Add(objResult.ExtColorId.ToString());

                //Int ColorId at Index 7
                lstChromeData.Add(objResult.IntColorId.ToString());

                //WheelDrive Id at Index 8
                lstChromeData.Add(objResult.WheelDriveId.ToString());

                //WheelPresent at Index 9
                lstChromeData.Add(objResult.VehiclePresent.ToString());

                //Grade at Index 10
                lstChromeData.Add(objResult.Grade.ToString());

            }
            return lstChromeData;
        }

        #region [Return Inventory Header Details for Inventory Documents/Expense/Drivers/Notes Screen]
        /// <summary>
        /// This method will provide inventory information like year, make, model with VIN# for
        /// Making Header on Inventory Documents/Expense/Drivers/Notes Screen
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns>Return Inventory Header Details for Inventory Documents/Expense/Drivers/Notes Screen</returns>
        public static string GetCurrentInventoryHeader(long inventoryId)
        {
            CarDetailsSelectResult result = InventoryDAL.GetCurrentInventoryHeader(inventoryId);
            System.Text.StringBuilder strInventoryHeader = new System.Text.StringBuilder();
            if (result != null)
            {
                strInventoryHeader.Append(result.Year);
                strInventoryHeader.Append(", ");
                strInventoryHeader.Append(result.Make);
                strInventoryHeader.Append(", ");
                strInventoryHeader.Append(result.Model);
                strInventoryHeader.Append(" with VIN# ");
                strInventoryHeader.Append(result.VIN);

                return strInventoryHeader.ToString();
            }
            else
                return string.Empty;

        }
        #endregion

        /// <summary>
        /// Get Car Properties
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public static IQueryable GetCarProperties(long? inventoryId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.GetCarProperties(inventoryId);
        }

        /// <summary>
        /// Get Car Properties
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public static Dictionary<string, bool> GetCarPropertiesBool(long? inventoryId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.GetCarPropertiesBool(inventoryId);
        }

        public static Dictionary<string, bool> GetCarPropertiesBool(DataTable dt)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.GetCarPropertiesBool(dt);
        }

        #region Get List of Comeback Reasons
        /// <summary>
        /// This method returns list of Comeback Reasons
        /// </summary>
        /// <returns></returns>
        public IQueryable GetComeBackReason()
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.GetComeBackReason();

        }

        public IQueryable GetComeBackReason(DataTable dt)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.GetComeBackReason(dt);

        }
        #endregion




        #region Update Comeback Details
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
        public static int UpdateComeBackDetails(bool comebackStatus, int comebackReasonId, DateTime? comebackDate,
                long ComebackMileageIn, long ComebackDealerId, decimal chargeBackCommission, string comebackComments, long inventoryId, long addedBy, DateTime addedDate, long? modifiedBy, DateTime? modifiedDate, long? deletedBy, DateTime? dateDeleted, bool IsActive, ref string strOutErrorMessage)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.UpdateComeBackDetails(comebackStatus, comebackReasonId, comebackDate,
                    ComebackMileageIn, ComebackDealerId, chargeBackCommission, comebackComments, inventoryId, addedBy, addedDate, modifiedBy, modifiedDate, deletedBy, dateDeleted, IsActive, ref strOutErrorMessage);
        }

        #endregion
        /// <summary>
        /// Update Dealer Details using Edit Popup section of ManageInventory screen
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
        public static int DealerDetailUpdate(long? inventoryId, decimal cost, DateTime? purchDate, bool titlePresent, int titleCopyReceived, bool titleShipped, string strTitlePresentNotes,
                         string strTitleShippedNotes, int designationId, long PurchaseFrom, long buyerId, string strChequeNo, long updatedBy, DateTime updatedDate, int vehicleHistoryReportId, int? DupTitle, string DupTitleNote)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.DealerDetailUpdate(inventoryId, cost, purchDate, titlePresent, titleCopyReceived, titleShipped, strTitlePresentNotes,
                 strTitleShippedNotes, designationId, PurchaseFrom, buyerId, strChequeNo, updatedBy, updatedDate, vehicleHistoryReportId, DupTitle, DupTitleNote);

        }


        /// <summary>
        /// Update SoldToDetails using edit popup of ManageInventory Screen
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
        public static int SoldToUpdate(long? inventoryId, long mileagOut, DateTime? SoldDate, decimal marketPrice,
            decimal actualCost, decimal priceSold, DateTime? depositDate, decimal depositAmount, int bankAccountId,
             string strDepositComment, int soldStatus, long soldTo, string strSoldComment, long updatedBy, DateTime updatedDate)
        {
            InventoryDAL objDAL = new InventoryDAL();

            return objDAL.SoldToUpdate(inventoryId, mileagOut, SoldDate, marketPrice, actualCost, priceSold, depositDate, depositAmount,
             bankAccountId, strDepositComment, soldStatus, soldTo, strSoldComment, updatedBy, updatedDate);

        }


        public static int SoldToUpdate_ver211(long? inventoryId, long mileagOut, DateTime? SoldDate, decimal marketPrice,
         decimal actualCost, decimal priceSold, DateTime? depositDate, decimal depositAmount, int bankAccountId,
          string strDepositComment, int soldStatus, long soldTo
          , string strSoldComment, long updatedBy, DateTime updatedDate)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.SoldToUpdate_ver211(inventoryId, mileagOut, SoldDate, marketPrice,
            actualCost, priceSold, depositDate, depositAmount, bankAccountId,
            strDepositComment, soldStatus, soldTo
            , strSoldComment, updatedBy, updatedDate);

        }

        /// <summary>
        /// Update Car Properties detail using edit popup of ManageInventory screen
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
        public static int CarPropertyUpdate(long? inventoryId, bool AC, bool PowerWindows, bool PowerLocks, bool AllowWheels,
                bool navigation, bool sunMoon, bool leather, bool powerSeat, long updatedBy, DateTime updatedDate)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.CarPropertyUpdate(inventoryId, AC, PowerWindows, PowerLocks, AllowWheels, navigation, sunMoon,
                   leather, powerSeat, updatedBy, updatedDate);
        }

        /// <summary>
        /// Get Section History
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <param name="strSectionName"></param>
        /// <param name="strResult"></param>
        /// <returns></returns>
        public static string GetSectionHistory(long? inventoryId, int SectionId, string strResult)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.GetSectionHistory(inventoryId, SectionId, strResult);
        }
        /// <summary>
        /// Get SoldTo Details
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public static IQueryable GetSoldToDetails(long? inventoryId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.GetSoldToDetails(inventoryId);
        }

        /// <summary>
        /// Get Dealer DETAILS for Inventory
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public static IQueryable GetDealerForInventory(long? inventoryId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.GetDealerForInventory(inventoryId);
        }
        /// <summary>
        /// Update Car Details in db
        /// </summary>
        /// <param name="objInventory"></param>
        /// <returns></returns>
        public static int UpdateCarDetails(Inventory objInventory)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.UpdateCarDetails(objInventory);

        }

        #region[ Search Inventory ]
        /// <summary>
        /// Search Inventory
        /// </summary>
        /// <param name="VINNo">VIN #</param>
        /// <param name="year">Make Year</param>
        /// <param name="Model">Model Name</param>
        /// <param name="make"> Make Name</param>
        /// <param name="dealer">Dealer Name</param>
        /// <returns>List of inventories</returns>
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
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.SearchInventory(VIN, Year, Model, Make, Dealer, CarStatus, CRStatus, startRowIndex
                , maximumRows, SystemID, SortExpression, OrgID);
        }
        #endregion
        #region[ Search Inventory Count]
        /// <summary>
        /// Search Inventory
        /// </summary>
        /// <param name="VINNo">VIN #</param>
        /// <param name="year">Make Year</param>
        /// <param name="Model">Model Name</param>
        /// <param name="make"> Make Name</param>
        /// <param name="dealer">Dealer Name</param>
        /// <returns>List of inventories</returns>
        public static Int32 SearchInventoryCount(String VIN
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
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.SearchInventoryCount(VIN, Year, Model, Make, Dealer, CarStatus, CRStatus, startRowIndex
                , maximumRows, SystemID, SortExpression, OrgID);
        }
        #endregion

        #region[ Search Main Inventory  ]
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
        public static DataTable SearchInventory(Int32 VInMatch, String VINNo, String checkNo, Int32 FromYear,
                                                Int32 ToYear, long makeId, long modelId, long BodyId, long dealerId,
                                                long customerId, long buyerId, long designationId, int comeBack, int sold,
                                                Int32 CarStatus, Int32 CRStatus, Int32 StartRowIndex, Int32 MaximumRows,
                                                Int32 SystemID, String SortExpression, Int32 EntityTypeID, Int32 EntityID,
                                                Int32 ParentEntityID, short OrgID, String SoldDateFrom, String SoldDateTo)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.SearchInventory(VInMatch
                        , VINNo
                        , checkNo
                        , FromYear
                        , ToYear
                        , makeId
                        , modelId
                        , BodyId
                        , dealerId
                        , customerId
                        , buyerId
                        , designationId
                        , comeBack
                        , sold
                        , CarStatus
                        , CRStatus
                        , StartRowIndex
                        , MaximumRows
                        , SystemID
                        , SortExpression
                        , EntityTypeID
                        , EntityID
                        , ParentEntityID
                        , OrgID
                        , SoldDateFrom
                        , SoldDateTo);
        }


        #endregion
        #region[ Search Main Inventory Count]
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
        public static Int32 SearchInventoryCount(Int32 VInMatch, String VINNo, String checkNo, Int32 FromYear,
                                                Int32 ToYear, long makeId, long modelId, long BodyId, long dealerId,
                                                long customerId, long buyerId, long designationId, int comeBack, int sold,
                                                Int32 CarStatus, Int32 CRStatus, Int32 StartRowIndex, Int32 MaximumRows,
                                                Int32 SystemID, String SortExpression, Int32 EntityTypeID, Int32 EntityID,
                                                Int32 ParentEntityID, short OrgID, String SoldDateFrom, String SoldDateTo)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.SearchInventoryCount(VInMatch
                        , VINNo
                        , checkNo
                        , FromYear
                        , ToYear
                        , makeId
                        , modelId
                        , BodyId
                        , dealerId
                        , customerId
                        , buyerId
                        , designationId
                        , comeBack
                        , sold
                        , CarStatus
                        , CRStatus
                        , StartRowIndex
                        , MaximumRows
                        , SystemID
                        , SortExpression
                        , EntityTypeID
                        , EntityID
                        , ParentEntityID
                        , OrgID
                        , SoldDateFrom
                        , SoldDateTo);
        }
        #endregion

        #region [Fetch Systems]
        public static DataTable FetchSystems(short OrgID)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.FetchSystems(OrgID);
        }
        #endregion

        #region [Fetch Grades]
        public static DataTable FetchGrades()
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.FetchGrades();
        }
        #endregion

        #region [Fetch notes by Inventory ID]
        public static DataTable FetchNotesByInventoryID(Int64 EntityID, Int32 EntityTypeID, Int32 NoteTypeID)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.FetchNotesByInventoryID(EntityID, EntityTypeID, NoteTypeID);
        }
        #endregion

        #region [Update Inventory System]
        public static void UpdateInventorySystem(Int64 InventoryId, Int32 SystemID, Int64 UpdatedBy, DateTime UpdatedDate)
        {
            InventoryDAL objDAL = new InventoryDAL();
            objDAL.UpdateInventorySystem(InventoryId, SystemID, UpdatedBy, UpdatedDate);
        }
        #endregion

        #region[ Search Linked Inventories ]
        /// <summary>
        /// Search Linked Inventories
        /// </summary>
        /// <param name="inventoryId">InventoryId</param>
        /// <returns>List of linked inventories</returns>
        public static IEnumerable SearchInventoryLinked(long inventoryId, string VIN, Int16 OrgID)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.SearchInventoryLinked(inventoryId, VIN, OrgID);
        }

        public static DataTable SearchInventoryLinked(long inventoryId, string VIN, Int32 SecurityUserID, Int32 ParentEntityID, Int32 EntityTypeID)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.SearchInventoryLinked(inventoryId, VIN, SecurityUserID, ParentEntityID, EntityTypeID);
        }
        #endregion


        /// <summary>
        /// Update Inventory Sold Status and Comments
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <param name="SoldStatus"></param>
        /// <param name="SoldComments"></param>
        /// <returns>0/1</returns>
        public static int InventorySoldStatusUpdate(long? inventoryId, int SoldStatus, string SoldComments, long UpdatedBy, DateTime UpdatedDate)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.InventorySoldStatusUpdate(inventoryId, SoldStatus, SoldComments, UpdatedBy, UpdatedDate);
        }
        /// <summary>
        /// Select Inventory SoldStatus
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns>List containing Sold Status & Sold Comments</returns>
        public static List<string> InventorySoldStatus(long? inventoryId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.InventorySoldStatus(inventoryId);
        }
        #region [Table History Record Select against inventory id]
        public List<SoldToHistory_SelectResult> TableHistorySelect(long InventoryID)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.TableHistorySelect(InventoryID);
        }
        #endregion

        public static List<string> InventorySoldStatus(DataTable dt)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.InventorySoldStatus(dt);
        }

        public static IEnumerable SearchInventoryDriverByInventoryId(Int32 inventoryId, Int16 OrgID)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.SearchInventoryDriverByInventoryId(inventoryId, OrgID);
        }

        #region [Check Whether Inventory can be Move to Inventory]
        /// <summary>
        /// Check Whether inventory can be move to inventory
        /// </summary>
        /// <param name="InventoryId"></param>
        /// <returns></returns>
        public static bool CanMoveToInventory(long InventoryId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.CanMoveToInventory(InventoryId);
        }

        public static bool CanMoveToInventory(DataTable dt)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.CanMoveToInventory(dt);
        }
        #endregion

        #region [Check Whether Inventory can be Move to Archieve]
        /// <summary>
        /// Check Whether Inventory can be move to archieve
        /// </summary>
        /// <param name="InventoryId"></param>
        /// <returns></returns>
        public static bool CanMoveToArchieve(long InventoryId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.CanMoveToArchieve(InventoryId);
        }

        public static bool CanMoveToArchieve(DataTable dt)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.CanMoveToArchieve(dt);
        }
        #endregion

        #region [Get Inventory Drivers Count]
        public static int GetDriversCount(long InventoryId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.GetDriversCount(InventoryId);
        }
        #endregion

        #region [Get Inventory Expenses Count]
        public static int GetExpensesCount(long InventoryId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.GetExpensesCount(InventoryId);
        }

        #endregion

        #region [Get Inventory Notes Count]
        public static int GetInventoryNotes(long InventoryId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.GetInventoryNotes(InventoryId);
        }
        #endregion

        #region [Get Inventory Documents Count]
        public static int GetInventoryDocs(long InventoryId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.GetInventoryDocs(InventoryId);
        }
        #endregion

        #region [Select CarStatus]
        /// <summary>
        /// Select Car Status (Mode=1) against InventoryId from InventoryTable
        /// </summary>
        /// <param name="InventoryId"></param>
        /// <returns></returns>
        public static int CarStatusSelect(long InventoryId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.CarStatusSelect(InventoryId);
        }

        public static int CarStatusSelect(DataTable dt)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.CarStatusSelect(dt);
        }
        #endregion

        #region [Update CarStatus]
        /// <summary>
        /// Update Car Status (Mode=2) against InventoryId
        /// </summary>
        /// <param name="InventoryId"></param>
        /// <param name="carStatus"></param>
        /// <returns></returns>
        public static int CarStatusUpdate(long InventoryId, int carStatus, long updatedBy, DateTime updatedDate)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.CarStatusUpdate(InventoryId, carStatus, updatedBy, updatedDate);
        }
        #endregion

        #region [Check Whether check paid against inventory by/for particular entity]
        /// <summary>
        /// Return 0 if check not paid else return 1
        /// </summary>
        /// <param name="InventoryId"></param>
        /// <returns>0/1</returns>
        public static int IsCheckPaid(long InventoryId, long entityId, int entityTypeId, bool isCheckForAllEntity)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.IsCheckPaid(InventoryId, entityId, entityTypeId, isCheckForAllEntity);
        }
        #endregion

        #region [Validate VIN]
        /// <summary>
        /// Return list of matched VIN Numbers to U.I For the provided pattern
        /// </summary>
        /// <param name="strVinNo"></param>
        /// <returns></returns>
        public static IQueryable GetMatchedVinNumbers(string strVinNo)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.GetMatchedVinNumbers(strVinNo);
        }
        #endregion


        #region [Inventory Documents]
        #region[ Document by Entity Type & Entity Id ]
        /// <summary>
        /// Document by Entity Type & Entity Id
        /// </summary>
        /// <param name="entityTypeId">EntitytypeId</param>
        /// <param name="entityId">EntityId</param>
        /// <returns> List of Documents</returns>
        public static IEnumerable Document_ByEntityId(long entityTypeId, long entityId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.Search_DocumentsByEntityId(entityTypeId, entityId);
        }

        public static DataTable Document_ByEntityId(long InventoryID, Int32 SecurityUserID, Int32 ParentEntityID, Int32 EntityTypeID)
        {

            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.Search_DocumentsByEntityId(InventoryID, SecurityUserID, ParentEntityID, EntityTypeID);
        }
        #endregion

        //#region[ Add Inventory Document ]
        ///// <summary>
        ///// Add Inventory Document
        ///// </summary>
        ///// <param name="doc">Document</param>
        ///// <returns>Document ID generated</returns>
        //public static long AddInventoryDoc(Document doc)
        //{
        //    InventoryDAL objDAL = new InventoryDAL();
        //    return objDal.Document_Add(doc);
        //}
        //#endregion

        #region[ Open document by id from database ]
        /// <summary>
        /// Open document by id from database 
        /// </summary>
        /// <param name="documentId">document Id</param>
        public static AttachedDocs DocumentOpenById(Int32 documentId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            AttachedDocs doc = new AttachedDocs();
            DataTable dTab = objDAL.DocumentOpenById(documentId);
            if (dTab != null && dTab.Rows.Count > 0)
            {
                doc.FileName = Convert.ToString(dTab.Rows[0]["DocumentName"]);
                doc.FileType = Convert.ToString(dTab.Rows[0]["FileType"]);
                doc.FileBytes = (Byte[])dTab.Rows[0]["DocumentBinary"];
                doc.FileLength = doc.FileBytes.Length;

            }
            return doc;
        }
        public static DataTable DocumentByID(Int32 documentId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.DocumentOpenById(documentId);
        }
        #endregion

        #region[ Update document to database ]
        /// <summary>
        /// Update document to database 
        /// </summary>
        /// <param name="doc"> Document </param>
        /// <returns>Integer</returns>
        public static int Document_Update(Document doc)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.Document_Update(doc);
        }
        #endregion


        //#region[ Delete Inventory Document(Mark IsActive=0) ]
        ///// <summary>
        ///// Delete Inventory Document(Mark IsActive=0)
        ///// </summary>
        ///// <param name="DocumentId">Document Id</param>
        ///// <param name="DeletedBy">Deleted By</param>
        ///// <returns></returns>
        //public static long Document_Delete(long DocumentId, long DeletedBy)
        //{
        //    InventoryDAL objDAL = new InventoryDAL();
        //    return objDAL.Document_Delete(DocumentId, DeletedBy);
        //}
        //#endregion

        #region [Inventory Expenses]
        /// <summary>
        /// Search Expenses by InventoryId
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns>List of Inventories</returns>
        public static IEnumerable Expenses_ByInventoryId(long inventoryId, Int16 OrgID)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.Expense_SearchByInventoryId(inventoryId, OrgID);
        }

        public static DataTable Expenses_ByInventoryId(long inventoryId, Int32 EntityID, Int32 ParentEntityID, Int32 EntityTypeID)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.Expense_SearchByInventoryId(inventoryId, EntityID, ParentEntityID, EntityTypeID);
        }
        /// <summary>
        /// Add New Expense
        /// </summary>
        /// <param name="objExpense"></param>
        /// <returns>ExpenseId</returns>
        public long AddNewExpense(Expense objExpense)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.AddNewExpense(objExpense);

        }


        /// <summary>
        /// Get List of Expenses
        /// </summary>
        /// <returns></returns>
        public IQueryable GetExpenses()
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.GetExpenses();

        }

        /// <summary>
        /// Update Expense
        /// </summary>
        /// <param name="objExpense"></param>
        /// <returns>0/1</returns>
        public int UpdateExpense(Expense objExpense, long ExpenseId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.UpdateExpense(objExpense, ExpenseId);
        }

        /// <summary>
        /// Delete Expense
        /// </summary>
        /// <param name="objExpense"></param>
        /// <returns>0/1</returns>
        public int DeleteExpense(Expense objExpense, long ExpenseId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.DeleteExpense(objExpense, ExpenseId);
        }

        /// <summary>
        /// List of Vendors 
        /// </summary>
        /// <returns>List of Vendors</returns>
        public IEnumerable GetVendorList(short OrgID)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.GetVendorList(OrgID);

        }
        #endregion

        #region [Inventory Drivers]
        #region Search Inventory Driver by InventoryID
        /// <summary>
        /// Search Drivers by InventoryId
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns>List of Inventory Driver with Details</returns>
        public static IEnumerable SearchInventoryDriverByInventoryId(long inventoryId, Int16 OrgID)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.SearchInventoryDriverByInventoryId(inventoryId, OrgID);
        }

        public static DataTable SearchInventoryDriverByInventoryId(Int32 inventoryId, Int32 SecurityUserID, Int32 ParentEntityID, Int32 EntityTypeID)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.SearchInventoryDriverByInventoryId(inventoryId, SecurityUserID, ParentEntityID, EntityTypeID);
        }
        #endregion

        #region[ Get List of Drivers ]
        /// <summary>
        /// Search Inventory Drivers by Inventory Id
        /// </summary> /// </summary>
        /// <param name="invnetoryIdr">InventoryId</param>
        /// <returns>List of Drivers e.g Name & DriverId</returns>
        public static IQueryable GetDriverList(Int16 OrgID)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.GetDriverList(OrgID).AsQueryable();
        }
        #endregion


        #region[ Add InventoryDriver ]
        /// <summary>
        /// Search Inventory Drivers by Inventory Id
        /// </summary> /// </summary>
        /// <param name="invnetoryIdr">InventoryId</param>
        /// <returns>InventoryDriverId generated</returns>
        public static long AddInventoryDriver(InventoryDriver objDriver)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.AddInventoryDriver(objDriver);

        }
        #endregion


        #region Update Inventory Driver Details (InventoryDriver table)
        /// <summary>
        /// Update Inventory Driver Details
        /// </summary>
        /// <returns>0/1</returns>
        public static int UpdateInventoryDriver(InventoryDriver objDriver, long InvUD)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.UpdateInventoryDriver(objDriver, InvUD);
        }
        #endregion

        #region Delete Inventory Driver(Mark IsActive=0 against particular inventorydriver record in InventoryDriver Table)
        /// <summary>
        /// Delete Inventory Driver (Mark IsActive flag to 0)
        /// </summary>
        /// <returns>0/1</returns>
        public static int DeleteInventoryDriver(InventoryDriver objDriver, long InvUD)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.DeleteInventoryDriver(objDriver, InvUD);
        }
        #endregion

        #endregion


        #region [Inventory Notes]
        public static IEnumerable Notes_ByInventoryId(Int32 entityId, Int32 entityTypeId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.Notes_SearchByInventoryId(entityId, entityTypeId);
        }
        /// <summary>
        /// AddNotes
        /// </summary>
        /// <param name="objNote"></param>
        /// <returns>NoteId</returns>
        public static long AddNotes(Note objNote)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.AddNotes(objNote);

        }

        /// <summary>
        /// return special case note id, if exists against inventoryid
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns>special case note id</returns>
        public static long IsSpecialCaseNoteExists(long inventoryId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.IsSpecialCaseNoteExists(inventoryId);
        }


        public static int UpdateNotes(Note objNote, long updateNoteId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.UpdateNotes(objNote, updateNoteId);
        }
        public static int DeleteNotes(Note objNote, long deleteNoteId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.DeleteNotes(objNote, deleteNoteId);
        }

        #region[Update Note]
        /// <summary>
        /// Update Note 
        /// </summary>
        /// <param name="objNote">Note Entity</param>
        /// <param name="Mode">Mode 1: to Insert, 2: to Update, 3 : To Delete</param>
        /// <returns></returns>
        public static int UpdateNote(Note objNote, Int32 Mode)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.UpdateNote(objNote, Mode);
        }
        #endregion
        #region [Get Note by Note ID]
        public static string GetNoteByID(long NoteId)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.GetNoteByID(NoteId);
        }
        #endregion
        #endregion

        #region [Add Inventory]
        /// <summary>
        /// This method Add New Inventory record in inventory table and return inventoryid generated
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long AddInventory(Inventory obj)
        {
            InventoryDAL objDAL = new InventoryDAL();
            long inventoryId;
            inventoryId = objDAL.AddInventory(obj);

            return inventoryId;
        }

        #region[ Search Dealer without Country  ]
        // <summary>
        ///  [ Search Dealer without Country criteria  ]
        /// </summary>
        /// <param name="strDealerName"></param>
        /// <param name="strCity"></param>
        /// <param name="dealerStateId"></param>
        /// <param name="zip"></param>
        /// <returns>List of Searched Dealers</returns>
        public static IEnumerable SearchDealer(string strDealerName, string strCity, int dealerStateId, string zip, short orgID)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.SearchDealer(strDealerName, strCity, dealerStateId, zip, orgID);
        }
        #endregion
        #region[ Search Dealer without Country  ]
        // <summary>
        ///  [ Search Dealer with Country criteria  ]
        /// </summary>
        /// <param name="strDealerName"></param>
        /// <param name="strCity"></param>
        /// <param name="dealerStateId"></param>
        /// <param name="zip"></param>
        /// <param name="countryId>Country Id </param>
        /// <returns>List of Searched Dealers</returns>
        public static IEnumerable SearchDealer(string strDealerName, string strCity, int dealerStateId, string zip, int country, short orgID)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.SearchDealer(strDealerName, strCity, dealerStateId, zip, country, orgID);

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
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.SearchDealerPaged(strDealerName, strCity, dealerStateId, zip, CountryId, StartRowIndex, MaximumRows, OrgID);
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
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.SearchDealerPagedCount(strDealerName, strCity, dealerStateId, zip, CountryId, StartRowIndex, MaximumRows, OrgID);
        }
        #endregion
        #endregion

        /// <summary>
        /// This function retreives list of Linked cars(Select inventories where comeback status=true
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public IEnumerable GetLinkedCars(string strVIN)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.GetLinkedCars(strVIN);
        }
        #endregion

        #region [Get Dealer Details Resultset w.r.t InventoryId]
        /// <summary>
        /// Return dealer details
        /// </summary>
        /// <param name="Dealerid"></param>
        /// <returns></returns>
        public static DealerDetailsSelectResult GetDealerForInv(long invId)
        {
            return InventoryDAL.GetDealerForInv(invId);

        }

        public static DealerDetailsSelectResult GetDealerForInv(DataTable dt)
        {
            return InventoryDAL.GetDealerForInv(dt);

        }
        #endregion


        #region Inventory Documents


        #region[ Add Inventory Document ]
        /// <summary>
        /// Add Inventory Document
        /// </summary>
        /// <param name="doc">Document</param>
        /// <returns>Document ID generated</returns>
        public static long AddInventoryDoc(Document doc)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.Document_Add(doc);
        }
        #endregion


        #region[ Delete Inventory Document(Mark IsActive=0) ]
        /// <summary>
        /// Delete Inventory Document(Mark IsActive=0)
        /// </summary>
        /// <param name="DocumentId">Document Id</param>
        /// <param name="DeletedBy">Deleted By</param>
        /// <returns></returns>
        public static long Document_Delete(long DocumentId, long DeletedBy)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.DeleteInventoryDoc(DocumentId, DeletedBy);
        }
        #endregion

        #endregion
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
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.DeleteInventory(inventoryId, deletedBy, deletedDate);
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
            return InventoryDAL.QuickSearchInventories(vinMatch, VINNO, UserID, OrgID);

        }
        #endregion


        /// <summary>
        /// Get Car Details-Section Information for particular inventory
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public static CarDetailsSelectResult GetCarDetail(long? inventoryId)
        {
            return InventoryDAL.GetCarDetail(inventoryId);
        }


        #region [Get list of Vehicle history report options available]
        /// <summary>
        /// Get list of Vehicle history report options available
        /// </summary>
        /// <returns></returns>
        public static VehicleHistoryReportSelectResult[] GetVehicleHistoryReports()
        {

            return InventoryDAL.GetVehicleHistoryReports();
        }
        #endregion

        #region[Inventory list sort options]
        public static DataTable InventoryList_SortOptions(String strQuery, String Sort1, String Sort2)
        {
            InventoryDAL invDAL = new InventoryDAL();
            return invDAL.InventoryList_SortOptions(strQuery, Sort1, Sort2);
        }
        #endregion

        #region[Search Inventory sort options]
        public static DataTable SearchInventory_SortOptions(String strQuery, String Sort1, String Sort2)
        {
            InventoryDAL invDAL = new InventoryDAL();
            return invDAL.SearchInventory_SortOptions(strQuery, Sort1, Sort2);
        }
        #endregion

        public ExpandoObject CRInfo_ByInvID(long InvID)
        {
            InventoryDAL dal = new InventoryDAL();
            return dal.CRInfo_ByInvID(InvID);
        }

        #region[Get Inventory Details by InventoryId]
        public static InventoryDetails_ByInventoryIDResult GetInventoryDetailsByInventoryID(long InventoryId)
        {
            return InventoryDAL.GetInventoryDetailsByInventoryID(InventoryId);
        }
        #endregion

        #region[Get Expense by InventoryId]
        public IEnumerable GetExpense_ByInventoryID(long InventoryId)
        {
            InventoryDAL objInventoryDAL = new InventoryDAL();
            return objInventoryDAL.GetExpense_ByInventoryID(InventoryId);
        }
        public IEnumerable GetExpense_ByInventoryID(long InventoryId, String EntityTypeID)
        {
            InventoryDAL objInventoryDAL = new InventoryDAL();
            return objInventoryDAL.GetExpense_ByInventoryID(InventoryId, EntityTypeID);
        }
        #endregion

        #region[Get Location by InventoryId]
        public IEnumerable GetLocation_ByInventoryID(long InventoryId)
        {
            InventoryDAL objInventoryDAL = new InventoryDAL();
            return objInventoryDAL.GetLocation_ByInventoryID(InventoryId);
        }
        #endregion

        #region[Get VIN from Inventory ID]
        public String GetVINFromInventory(Int64 InvID)
        {
            InventoryDAL objInventoryDAL = new InventoryDAL();
            return objInventoryDAL.GetVINFromInventory(InvID);
        }
        #endregion

        #region[Complete inventory info to show on Inventory Detail screen]
        public static System.Collections.ArrayList CompleteInventoryInfoByInventoryID(long InventoryID)
        {
            return InventoryDAL.CompleteInventoryInfoByInventoryID(InventoryID);
        }
        #endregion

        #region[Count : Inventory, Expense, Document, Driver - By InvnetoryID]
        public List<Count_ExpInvDocDriverResult> Count_ExpInvDocDriver(Int64 InventoryID)
        {
            InventoryDAL dal = new InventoryDAL();
            return dal.Count_ExpInvDocDriver(InventoryID);
        }

        public DataTable Count_ExpInvDocDriver(Int64 InventoryID, Int32 SecurityUserID, Int32 ParentEntityID, Int32 EntityTypeID)
        {
            InventoryDAL dal = new InventoryDAL();
            return dal.Count_ExpInvDocDriver(InventoryID, SecurityUserID, ParentEntityID, EntityTypeID);
        }
        #endregion

        public DataTable Exp_PreExp_ByInventoryID(long InventoryId)
        {
            InventoryDAL dal = new InventoryDAL();
            return dal.Exp_PreExp_ByInventoryID(InventoryId);
        }

        #region[Get Duplicate expense details]
        public DataTable GetDuplicateExpenseDetails(long EntityID, Int32 EntityTypeID, Int32 ExpenseTypeID, String Amount, long InventoryID, Int32 Period)
        {
            InventoryDAL dal = new InventoryDAL();
            return dal.GetDuplicateExpenseDetails(EntityID, EntityTypeID, ExpenseTypeID, Amount, InventoryID, Period);
        }
        #endregion

        #region[Get common inventory details to show on mobile browser]
        public DataTable GetCommonInvDetail(long InventoryId, String VIN)
        {
            InventoryDAL dal = new InventoryDAL();
            return dal.GetCommonInvDetail(InventoryId, VIN);
        }
        #endregion

        #region [Get Location by VIN]
        public IEnumerable GetLocation_ByVIN(String VIN, Int16 OrgID)
        {
            InventoryDAL dal = new InventoryDAL();
            return dal.GetLocation_ByVIN(VIN, OrgID);
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
            InventoryDAL dal = new InventoryDAL();
            return dal.SearchInventoryDataExport(VInMatch
                        , VINNo
                        , checkNo
                        , FromYear
                        , ToYear
                        , makeId
                        , modelId
                        , BodyId
                        , dealerId
                        , customerId
                        , buyerId
                        , designationId
                        , comeBack
                        , sold
                        , CarStatus
                        , CRStatus
                        , SystemID
                        , SortExpression
                        , EntityTypeID
                        , EntityID
                        , ParentEntityID
                        , OrgID
                        , SoldDateFrom
                        , SoldDateTo);
        }

        public Int32 SearchInventoryDataExportCount(Int32 VInMatch, String VINNo, String checkNo, Int32 FromYear, Int32 ToYear,
                                                    long makeId, long modelId, long BodyId, long dealerId, long customerId,
                                                    long buyerId, long designationId, int comeBack, int sold, Int32 CarStatus,
                                                    Int32 CRStatus, Int32 SystemID, String SortExpression, Int32 EntityTypeID,
                                                    Int32 EntityID, Int32 ParentEntityID, short OrgID, String SoldDateFrom,
                                                    String SoldDateTo
                                                   )
        {
            InventoryDAL dal = new InventoryDAL();

            return dal.SearchInventoryDataExportCount(VInMatch, VINNo, checkNo, FromYear, ToYear, makeId, modelId, BodyId, dealerId, customerId, buyerId, designationId, comeBack, sold,
                                                      CarStatus, CRStatus, SystemID, SortExpression, EntityTypeID, EntityID, ParentEntityID, OrgID, SoldDateFrom, SoldDateTo);
        }


        public int SaveExportHistory(String attachment, Int32 rowCount, String DataContent, Int64 UserId)
        {
            InventoryDAL dal = new InventoryDAL();
            int ret;
            ret = dal.SaveExportHistory(attachment, rowCount, DataContent, UserId);
            return ret;
        }

        public DataTable InventoryListDataExport(String VIN, Int32 Year, String Model, String Make, String Dealer, Int32 CarStatus, Int32 CRStatus, Int32 SystemID, String SortExpression, Int16 OrgID)
        {
            InventoryDAL dal = new InventoryDAL();
            return dal.InventoryListDataExport(VIN, Year, Model, Make, Dealer, CarStatus, CRStatus, SystemID, SortExpression, OrgID);
        }

        public Int32 InventoryListDataExportCount(String VIN, Int32 Year, String Model, String Make, String Dealer, Int32 CarStatus, Int32 CRStatus, Int32 SystemID, String SortExpression, Int16 OrgID)
        {
            InventoryDAL dal = new InventoryDAL();
            return dal.InventoryListDataExportCount(VIN, Year, Model, Make, Dealer, CarStatus, CRStatus, SystemID, SortExpression, OrgID);
        }
        #endregion

        #region [Save Inventory Table Edit History]

        public int SaveInventoryHistory(Int64 InventoryID, String CarNoteOld, String CarNoteNew, String CarFaxOld, String CarFaxNew, String SoldStatusOld, String SoldStatusNew, String ComebackOld, String ComebackNew, Int64 UserId, String Source)
        {
            InventoryDAL dal = new InventoryDAL();
            return dal.SaveInventoryHistory(InventoryID, CarNoteOld, CarNoteNew, CarFaxOld, CarFaxNew, SoldStatusOld, SoldStatusNew, ComebackOld, ComebackNew, UserId, Source);
        }

        public int SaveInventoryHistory(Int64 InventoryID, String CarNoteOld, String CarNoteNew, String CarFaxOld, String CarFaxNew, String SoldStatusOld, String SoldStatusNew, String ComebackOld, String ComebackNew, String CustomerIDNew, string CustomerIDOld, String DateSoldNew, string DateSoldOld,
            string MarketPriceNew, string MarkerPriceOld, string PriceSoldNew, string PriceSoldOld, string MileageOutNew, string MileageOutOld,
            String DepositeDateNew, string DepositeDateOld, string DepositeAmmountNew, string DepositeAmmountOld, string BankIDNew, String BankIDOld,
            string DepositeCommentNew, string DepositeCommentOld, string SoldCommentNew, string SoldCommentOld,
            Int64 UserId, String Source)
        {
            InventoryDAL dal = new InventoryDAL();
            return dal.SaveInventoryHistory(InventoryID, CarNoteOld, CarNoteNew, CarFaxOld, CarFaxNew, SoldStatusOld, SoldStatusNew, ComebackOld, ComebackNew, CustomerIDNew, CustomerIDOld, DateSoldNew, DateSoldOld,
             MarketPriceNew, MarkerPriceOld, PriceSoldNew, PriceSoldOld, MileageOutNew, MileageOutOld,
             DepositeDateNew, DepositeDateOld, DepositeAmmountNew, DepositeAmmountOld, BankIDNew, BankIDOld,
             DepositeCommentNew, DepositeCommentOld, SoldCommentNew, SoldCommentOld,
            UserId, Source);
        }
        #endregion

        #region[Get Complete Inventory Details-Added By Adesh 16 Jan,2013]
        public DataSet GetCompleteInventoryDetail(Int64 EmployeeID, String Page, Int64 InventoryID, String VIN)
        {
            InventoryDAL dal = new InventoryDAL();
            return dal.GetCompleteInventoryDetail(EmployeeID, Page, InventoryID, VIN);
        }
        #endregion

        #region [View All Title Status - Added by Rupendra 24 Jan 2013]
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
           , String SortExpression, short OrgID)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.GetViewAllTitleStatus(
             VIN
           , Year
           , MakeId
           , ModelId
           , DealerID
           , TitlePresent
           , DateFrom
           , DateTo
           , LateFee
           , CarStatus
           , LoginEntityTypeID
           , UserEntityID
           , BuyerParentID
           , BuyerIsDirect
           , BuyerAccessLevel
           , StartRowIndex
           , MaximumRows
           , SystemID
           , SortExpression
           , OrgID);
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
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.GetViewAllTitleStatusCount(
             VIN
           , Year
           , MakeId
           , ModelId
           , DealerID
           , TitlePresent
           , DateFrom
           , DateTo
           , LateFee
           , CarStatus
           , LoginEntityTypeID
           , UserEntityID
           , BuyerParentID
           , BuyerIsDirect
           , BuyerAccessLevel
           , StartRowIndex
           , MaximumRows
           , SystemID
           , SortExpression
           , OrgID);
        }

        #endregion

        #region [Update Title Present Note, Added by Rupendra 1 Feb 2012]
        public Int32 UpdateTitlePresentTrack(Int64 InventoryId, Int32 TitleValue, Int64 ModifiedBy)
        {
            InventoryDAL obj = new InventoryDAL();
            return obj.UpdateTitlePresentTrack(InventoryId, TitleValue, ModifiedBy);
        }
        #endregion

        #region[Audit by InventoryID]
        public DataTable InventoryAudit(long InventoryID)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.InventoryAudit(InventoryID);
        }
        #endregion

        #region[Lane History by InventoryID]
        public DataTable InventoryLaneHistory(long InventoryID)
        {
            InventoryDAL objDAL = new InventoryDAL();
            return objDAL.InventoryLaneHistory(InventoryID);
        }
        #endregion

        #region [Inventory Quick Search TO Scan]
        public static QuickInventorySearch_ScanResult[] QuickSearchScanInventories(int vinMatch, string VINNO, Int16 OrgID, long UserID)
        {
            return InventoryDAL.QuickSearchScanInventories(vinMatch, VINNO, OrgID, UserID);
        }
        #endregion
    }
}
