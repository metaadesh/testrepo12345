using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using METAOPTION.DAL;
using System.Web;

namespace METAOPTION.BAL
{
    public class LaneAssignmentBAL
    {
       
        #region [Lane Assignment List]
        /// <summary>
        /// Lane Assignment List
        /// </summary>
        /// <param name="Records">Records</param>
        /// <returns></returns>
        public IEnumerable LaneAssignmentList(Int32? Records)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            if (Records == null) Records = 500;
            return objLane.LaneAssignmentList(Records);
        }
        #endregion

        #region [Inventory Lane Editable Fields]
        /// <summary>
        /// Inventory Lane Editable Fields
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <returns></returns>
        public DataTable InventoryLaneEdiableFields(long inventoryId)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.InventoryLaneEdiableFields(inventoryId);
        }
        #endregion

        #region[ Edit Inventory Lanes ]
        /// <summary>
        /// Edit Inventory Lanes
        /// </summary>
        /// <param name="IsExotic">IsExotic</param>
        /// <param name="RegularLane">RegularLane</param>
        /// <param name="ExoticLane">ExoticLane</param>
        /// <param name="VirtualLane">VirtualLane</param>
        /// <param name="OnlineLane">OnlineLane</param>
        /// <param name="MarketPrice">MarketPrice</param>
        /// <param name="InventoryId">InventoryId</param>
        /// <returns></returns>
        public Int32 EditInventoryLanes(Int32 IsExotic, String RegularLane, String ExoticLane
                 , String VirtualLane, String OnlineLane, Decimal MarketPrice, long InventoryId)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            Int32 result = objLane.EditInventoryLanes(IsExotic, RegularLane, ExoticLane, VirtualLane, OnlineLane, MarketPrice, InventoryId);
            return result;
        }
        #endregion

        #region [Get Sorted Inventory Records]
        /// <summary>
        /// Get Sorted Inventory Records
        /// </summary>
        /// <param name="Filter">Filter</param>
        /// <param name="OrderBy">OrderBy</param>
        /// <returns></returns>
        public DataTable GetSortedInventoryRecords(String Filter, String OrderBy)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.GetSortedInventoryRecords(Filter, OrderBy);
        }
        #endregion

        #region[ Update Regular Lane ]
        /// <summary>
        /// Update Regular Lane
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="RegularLane">RegularLane</param>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        public Int32 UpdateRegularLane(long inventoryId, String RegularLane, long userId)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.UpdateRegularLane(inventoryId, RegularLane, userId);
        }
        #endregion

        #region[ Update Exotic Lane ]
        /// <summary>
        /// Update Exotic Lane
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="ExoticLane">ExoticLane</param>
        /// <param name="modifiedBy">modifiedBy</param>
        /// <returns></returns>
        public Int32 UpdateExoticLane(long inventoryId, String ExoticLane, long modifiedBy)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.UpdateExoticLane(inventoryId, ExoticLane, modifiedBy);
        }
        #endregion

        #region[ Update Online Lane ]
        /// <summary>
        /// Update Online Lane
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="OnlineLane">OnlineLane</param>
        /// <param name="modifiedBy">modifiedBy</param>
        /// <returns></returns>
        public Int32 UpdateOnlineLane(long inventoryId, String OnlineLane, long modifiedBy)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.UpdateOnlineLane(inventoryId, OnlineLane, modifiedBy);
        }
        #endregion

        #region[ Update Virtual Lane ]
        /// <summary>
        /// Update Virtual Lane
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="VirtualLane">VirtualLane</param>
        /// <param name="modifiedBy">modifiedBy</param>
        /// <returns></returns>
        public Int32 UpdateVirtualLane(long inventoryId, String VirtualLane, long modifiedBy)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.UpdateVirtualLane(inventoryId, VirtualLane, modifiedBy);
        }
        #endregion

        #region[ Update Market Price Lane ]
        /// <summary>
        /// Update Market Price Lan
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="MarketPrice">MarketPrice</param>
        /// <param name="modifiedBy">modifiedBy</param>
        /// <returns></returns>
        public Int32 UpdateMarketPrice(long inventoryId, Decimal MarketPrice, long modifiedBy)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.UpdateMarketPrice(inventoryId, MarketPrice, modifiedBy);
        }
        #endregion


        #region[ Update Mileage ]
        /// <summary>
        /// Update Mileage
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="Mileage">Mileage</param>
        /// <param name="modifiedBy">modifiedBy</param>
        /// <returns></returns>
        public Int32 UpdateMileage(long inventoryId, long Mileage, long modifiedBy)
        {
          LaneAssignmentDAL objLane = new LaneAssignmentDAL();
          return objLane.UpdateMileage(inventoryId, Mileage, modifiedBy);
        }
        #endregion


        #region[ Update VIN # ]
        /// <summary>
        /// Update VIN # 
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="VIN">VIN  #</param>
        /// <param name="modifiedBy">modifiedBy</param>
        /// <returns></returns>
        public Int32 UpdateVIN(long inventoryId, String VIN, long modifiedBy)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.UpdateVIN(inventoryId, VIN, modifiedBy);
        }
        #endregion

        #region[ Update Lane Notes ]
        /// <summary>
        /// Update Lane Notes
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="Notes">Notes</param>
        /// <param name="modifiedBy">modifiedBy</param>
        /// <returns></returns>
        public Int32 UpdateLaneNotes(long inventoryId, String Notes, long modifiedBy)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.UpdateNotes(inventoryId, Notes, modifiedBy);
        }
        #endregion

        #region[ Update Lane IsExotic ]
        /// <summary>
        /// Update Lane IsExotic
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="IsExotic">IsExotic</param>
        /// <returns></returns>
        public Int32 UpdateIsExotic(long inventoryId, bool IsExotic)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.UpdateIsExotic(inventoryId, IsExotic);
        }
        #endregion
        #region[ Update Lane History]
        /// <summary>
        /// Update Lane History
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <returns></returns>
        public Int32 UpdateIsAnnouncementCreated(long inventoryId)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.UpdateIsAnnouncementCreated(inventoryId);
        }
        #endregion

        #region[ Update Session Count]
        //public Int32 GetLaneSessionCount(long ModifiedBy)
        //{ 
            //LaneAssignmentDAL objLane = new LaneAssignmentDAL();
        //    return objLane.GetSessionCount(ModifiedBy); 
        //}
        #endregion

        #region[ Lane Count Upto 10 recorsd Pendinding For Announcement]
        //public Int32 GetLaneCountUpto10recorsdPendindingForAnnouncement(long ModifiedBy)
        //{
        //    return objLane.GetLaneCountUpto10recorsdPendindingForAnnouncement(ModifiedBy);
        //}
        #endregion

        #region[ Lane Count Upto 100 recorsd Pendinding For Announcement]
        //public Int32 GetLaneCountUpto100recorsdPendindingForAnnouncement(long ModifiedBy)
        //{
        //    return objLane.GetLaneCountUpto100recorsdPendindingForAnnouncement(ModifiedBy);
        //}
        #endregion

        #region [To select exotic lane pattern]
        /// <summary>
        /// To select exotic lane pattern
        /// </summary>
        /// <returns></returns>
        public List<string> SelectExoticLanePattern()
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            var result = objLane.SelectExoticPattern().AsQueryable();
            List<string> lstLanePattern = new List<string>();
            foreach (SelectExoticLanePatternResult objResult in result)
            {
                //LanePattern at Index 0
                lstLanePattern.Add(objResult.LANEPATTERN.ToString());
            }
            return lstLanePattern;
        }

        #endregion

        #region [To Update Exotic lane Pattern]
        /// <summary>
        /// To Update Exotic lane Pattern
        /// </summary>
        /// <param name="ExoticPattern"></param>
        public void UpdateExoticLanePattern(String ExoticPattern)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            objLane.UpdateLanePattern(ExoticPattern);
        }
        #endregion

        #region [ To Get All Lane Assignment list details ]
        /// <summary>
        /// To Get All Lane Assignment list details
        /// </summary>
        /// <returns></returns>
        public DataTable GetInventoryFilterList()
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.GetInventoryFilterList() as DataTable;
        }

        #endregion

        #region [Get Current User updated records count]
        /// <summary>
        /// Get Current User updated records count
        /// </summary>
        /// <param name="addedBy"></param>
        /// <returns></returns>
        public int Ge20RecordsCount(Int64 addedBy)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.Get20Counts(addedBy);
        }

        #endregion 

        #region[Select 100 records Lane Announcement And Email]
        /// <summary>
        /// Select 100 records Lane Announcement And Email
        /// </summary>
        /// <param name="UserId">To pass logged user id</param>
        /// <returns>inventory details</returns>
        public int SelectLaneAnnouncementAndEmail(Int64 UserId)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            DataTable dt = objLane.SelectLaneAnnouncementAndEmail(UserId);

            return dt.Rows.Count;
        }
        /// <summary>
        /// For Select
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public DataTable GetLaneAnnouncementAndEmail(Int64 UserId)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.SelectLaneAnnouncementAndEmail(UserId);

        }
        #endregion

        #region [ Select all employee email lists ] 
        /// <summary>
        /// Select all employee email lists
        /// </summary>
        /// <returns></returns>
        public IEnumerable SelectAllEmployeeEmailLists(Int16 OrgID)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.selectAllEmployeeEmailLists(OrgID);

        }
        #endregion

        #region "Get count for announcement & email are pending since last login"
        /// <summary>
        /// Get count for announcement & email are pending since last login
        /// </summary>
        /// <param name="addedBy">addedBy</param>
        /// <returns></returns>
        public int Ge10RecordsCount(Int64 addedBy)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.Get10Counts(addedBy);
        }
        #endregion

        #region[ Select InnerHTML Of Inventory List ]
        /// <summary>
        /// Select InnerHTML Of Inventory List
        /// </summary>
        /// <returns></returns>
        public List<string> SelectInnerHTML()
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            var result = objLane.SelectInnerHTML().AsQueryable();
            List<string> lstInnerHTML = new List<string>();
            foreach (SelectInnerHTMLResult objResult in result)
            {
                //InnerHTML at Index 0
                lstInnerHTML.Add(objResult.InnerHTML.ToString());


            }
            return lstInnerHTML;
        }
        #endregion

        #region[Insert InnerHTML Of Inventory List for Email purpose ]
        /// <summary>
        /// Insert InnerHTML Of Inventory List for Email purpose
        /// </summary>
        /// <param name="html">html code</param>
        /// <returns></returns>
        public int InsertInnerHTML(string html)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.InsertInnerHTML(html);
        }
        #endregion

        #region [ Get 10 records of Lane Assg. details since last login ]
        /// <summary>
        /// Get 10 records of Lane Assg. details since last login
        /// </summary>
        /// <param name="addedby">addedby</param>
        /// <returns></returns>
        public DataTable Select10RecordSinceLastLogin(Int64 addedby)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.Select10RecordSinceLastLogin(addedby);
        }
        #endregion

        #region [ Select current user Session updated records ]
        /// <summary>
        /// Select current user Session updated records
        /// </summary>
        /// <param name="addedby">addedby</param>
        /// <returns></returns>
        public DataTable Select20Records(Int64 addedby)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.Select20Records(addedby);
        }
        #endregion

        #region [ To Add Announcement ]
        /// <summary>
        /// To Add/Update Announcement
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public long AddAnnouncement(Announcement obj)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            HttpContext.Current.Cache.Remove(CacheEnum.Announcement);
            return objLane.AddAnnouncement(obj);
        }
        /// <summary>
        /// To Insert Announcement Relationship  
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public long AddAnnouncementRelationship(AnnouncementRelationship obj)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.AddAnnouncementRelationship(obj);
        }
        #endregion

        #region[Get All Lane Assignment History ]
        /// <summary>
        /// Get All Lane Assignment History
        /// </summary>
        /// <param name="Inventoryid">Inventoryid</param>
        /// <returns></returns>
        public IEnumerable SelectLaneAssignmentHistory(Int64 Inventoryid)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.SelectLaneAssignmentHistory(Inventoryid);
        }
        #endregion

        #region [Get Year,Make & Model for History list]
        /// <summary>
        /// Get Year,Make & Model for History list
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <returns></returns>
        public List<string> GetMake(Int64 inventoryId)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            var result = objLane.GetMakeModelByInventory(inventoryId).AsQueryable();
            List<string> lstMake = new List<string>();
            foreach (GetMakeModelByInventoryIdResult objResult in result)
            {
                //Year at Index 0
                lstMake.Add(objResult.Year.ToString());
                //model at Index 1
                lstMake.Add(objResult.model.ToString());
                //Make at Index 2
                lstMake.Add(objResult.Make.ToString());
            }
            return lstMake;
        }

        #endregion

        #region [ Select Announcement List]
        /// <summary>
        /// To  Get All new announcement list
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetAnnouncementSelect(Int16 OrgID)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.GetAnnouncementSelect(OrgID);
        }
        /// <summary>
        /// Get Row count
        /// </summary>
        /// <returns></returns>
        public int GetAnnouncementCount(Int16 OrgID)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.GetAnnouncementCount(OrgID);
        }
        /// <summary>
        /// To Deleted Announcement(Status changed)
        /// </summary>
        /// <param name="AnnouncementId">AnnouncementId</param>
        /// <returns>Return Updated status</returns>
        public long DeleteAnnouncement(Int64 AnnouncementId)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.DeleteAnnouncement(AnnouncementId);
        }
        /// <summary>
        /// Update Announcement
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public long UpdateAnnouncement(Announcement obj)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.UpdateAnnouncement(obj);
        }
        /// <summary>
        /// To Get Announcement by id for edit
        /// </summary>
        /// <param name="AnnouncementId"></param>
        /// <returns></returns>
        public IEnumerable GetAnnouncementById(Int64 AnnouncementId)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.GetAnnouncementById(AnnouncementId);
        }
        /// <summary>
        /// To Get All announcement type list
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetAnnouncementType()
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.GetAnnouncementType();
        }
        #endregion

        #region [Get Current Announcement List]
        /// <summary>
        /// Get Current Announcement List
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static DataTable GetCurrentAnnouncementList(Int64 AnnouncementId)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return LaneAssignmentDAL.GetCurrentAnnouncementList(AnnouncementId);
        }

        #endregion
        /// <summary>
        /// Select All Employee EmailLists 
        /// </summary>
        /// <returns></returns>
        public IEnumerable SelectAllEmployeeEmailListsDAL(Int16 OrgID)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
           return objLane.selectAllEmployeeEmailLists(OrgID);

        }        

        #region[Search Lane History]
        public DataTable SearchLaneHistory(long InvID, Int32 FieldID, DateTime DateFrom, DateTime DateTo, long AddedBy, String Source, String UpdateFrom, Int32 StartRowIndex, Int32 MaximumRows, String OrderBy, String OrgID)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.SearchLaneHistory(InvID, FieldID, DateFrom, DateTo, AddedBy, Source, UpdateFrom, StartRowIndex, MaximumRows, OrderBy, Convert.ToInt16(OrgID));
        }

        public Int32 SearchLaneHistoryCount(long InvID, Int32 FieldID, DateTime DateFrom, DateTime DateTo, long AddedBy, String Source, String UpdateFrom, Int32 StartRowIndex, Int32 MaximumRows, String OrderBy, String OrgID)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.SearchLaneHistoryCount(InvID, FieldID, DateFrom, DateTo, AddedBy, Source, UpdateFrom, StartRowIndex, MaximumRows, OrderBy, Convert.ToInt16(OrgID));
        }
        #endregion

        #region[Table Fields]
        public List<TableField> GetTableFields()
        {
            LaneAssignmentDAL dal = new LaneAssignmentDAL();
            return dal.GetTableFields();
        }
        #endregion

        #region[LaneHistory Added By]
        public IEnumerable GetLaneHistoryAddedBy(Int16 OrgID)
        {
            LaneAssignmentDAL dal = new LaneAssignmentDAL();
            return dal.GetLaneHistoryAddedBy(OrgID);
        }
        #endregion

        #region [Added by Rupendra 18 Jan 2013 for fetch Buyer Announcement]
        public IEnumerable GetAnnouncementSelect_Ver211(Int64 SecurityUserId, short OrgID)
        {
            LaneAssignmentDAL objLane = new LaneAssignmentDAL();
            return objLane.GetAnnouncementSelect_Ver211(SecurityUserId, OrgID);
        }
        #endregion
    }
}
