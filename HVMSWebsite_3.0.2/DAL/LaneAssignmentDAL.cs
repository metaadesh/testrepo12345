using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace METAOPTION.DAL
{
    public class LaneAssignmentDAL
    {


        #region [ Get Lane Assignment List]
        /// <summary>
        /// Get Lane Assignment List
        /// </summary>
        /// <param name="records">records</param>
        /// <returns>Lane Assignment List</returns>
        public IEnumerable LaneAssignmentList(Int32? records)
        {
            DALDataContext objLan = new DALDataContext();
            return objLan.InventoryLaneAssignmentList1(records).AsEnumerable();
        }

        #endregion

        #region[ Inventory Lane Ediable Fields ]
        /// <summary>
        /// Inventory Lane Ediable Fields
        /// </summary>
        /// <param name="inventoryId">Inventory Id</param>
        /// <returns></returns>
        public DataTable InventoryLaneEdiableFields(long inventoryId)
        {
            DALDataContext objLan = new DALDataContext();
            DataTable dt = new DataTable("InventoryEdit");
            using (SqlConnection Conn = new SqlConnection(objLan.Connection.ConnectionString))
            {
                try
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();

                    SqlCommand CMD = new SqlCommand("InventoryLaneEditableFields", Conn);
                    CMD.CommandType = CommandType.StoredProcedure;

                    CMD.Parameters.AddWithValue("@InventoryId", inventoryId);
                    SqlDataReader dReader = CMD.ExecuteReader(CommandBehavior.CloseConnection);

                    dt.Load(dReader);
                    dReader.Close();
                }
                catch (Exception ex)
                {
                    //TO DO
                }
            }
            return dt;
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
            DALDataContext objLan = new DALDataContext();
            Int32 result = objLan.InventoryLaneEdit(IsExotic, RegularLane, ExoticLane, VirtualLane, OnlineLane, MarketPrice, InventoryId);
            return result;
        }
        #endregion

        #region[ Select Inventory Records ]
        public DataTable GetSortedInventoryRecords(String Filter, String OrderBy)
        {
            DALDataContext objLan = new DALDataContext();

            DataTable dTab = new DataTable("InventoryEdit");
            using (SqlConnection Conn = new SqlConnection(objLan.Connection.ConnectionString))
            {

                String sqlStatement = "SELECT DISTINCT vwLaneAssignments.* FROM vwLaneAssignments ";
                if (Filter != string.Empty)
                {
                    sqlStatement += Filter;
                }

                if (OrderBy != string.Empty)
                    sqlStatement += OrderBy;


                try
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();

                    SqlCommand CMD = new SqlCommand(sqlStatement, Conn);
                    CMD.CommandType = CommandType.Text;

                    //CMD.Parameters.AddWithValue("@Year", year );
                    //CMD.Parameters.AddWithValue("@Make", make);
                    //CMD.Parameters.AddWithValue("@Model", model);
                    //CMD.Parameters.AddWithValue("@LaneType", laneType);
                    //CMD.Parameters.AddWithValue("@LaneNo", laneNo);

                    SqlDataReader dReader = CMD.ExecuteReader(CommandBehavior.CloseConnection);

                    dTab.Load(dReader);
                    dReader.Close();
                }
                catch (Exception ex)
                {
                    //TO DO
                }
            }
            return dTab;
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
            DALDataContext objLan = new DALDataContext();
            return objLan.spUpdateRegularLane(inventoryId, RegularLane, userId);
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
            DALDataContext objLan = new DALDataContext();
            return objLan.spUpdateExoticLane(inventoryId, ExoticLane, modifiedBy);
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
            DALDataContext objLan = new DALDataContext();
            return objLan.spUpdateOnlineLane(inventoryId, OnlineLane, modifiedBy);
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
            DALDataContext objLan = new DALDataContext();
            return objLan.spUpdateVirtualLane(inventoryId, VirtualLane, modifiedBy);
        }
        #endregion

        #region[ Update Market Price]
        /// <summary>
        /// Update Market Price
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="MarketPrice">MarketPrice</param>
        /// <param name="modifiedBy">modifiedBy</param>
        /// <returns></returns>
        public Int32 UpdateMarketPrice(long inventoryId, decimal MarketPrice, long modifiedBy)
        {
            DALDataContext objLan = new DALDataContext();
            return objLan.spUpdateMarketPrice(inventoryId, MarketPrice, modifiedBy);
        }
        #endregion

        #region[ Update Mileage]
        /// <summary>
        /// Update Mileage
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="Mileage">Mileage</param>
        /// <param name="modifiedBy">modifiedBy</param>
        /// <returns></returns>
        public Int32 UpdateMileage(long inventoryId, long Mileage, long modifiedBy)
        {
            DALDataContext objLan = new DALDataContext();
            return objLan.spUpdateMileage(inventoryId, Mileage, modifiedBy);
        }
        #endregion

        #region[ Update VIN # ]
        /// <summary>
        /// Update VIN #
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="VIN">VIN #</param>
        /// <param name="modifiedBy">modifiedBy</param>
        /// <returns></returns>
        public Int32 UpdateVIN(long inventoryId, String VIN, long modifiedBy)
        {
            DALDataContext objLan = new DALDataContext();
            return objLan.spUpdateVIN(inventoryId, VIN, modifiedBy);
        }
        #endregion

        #region[ Update Lane Notes]
        /// <summary>
        /// Update Lane Notes
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="Notes">Notes</param>
        /// <param name="modifiedBy">modifiedBy</param>
        /// <returns></returns>
        public Int32 UpdateNotes(long inventoryId, String Notes, long modifiedBy)
        {
            DALDataContext objLan = new DALDataContext();
            return objLan.UpdateLaneNote(inventoryId, Notes, modifiedBy);
        }
        #endregion

        #region[ Update Lane IsExotic]
        /// <summary>
        /// Update Lane IsExotic
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <param name="IsExotic">IsExotic</param>
        /// <returns></returns>
        public Int32 UpdateIsExotic(long inventoryId, bool IsExotic)
        {
            DALDataContext objLan = new DALDataContext();
            return objLan.spUpdateIsExotic(inventoryId, IsExotic);
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
            DALDataContext objLan = new DALDataContext();
            return objLan.spUpdateLaneHistory(inventoryId);
        }
        #endregion


        #region[ Get User Session Count]
        //public Int32 GetSessionCount(long ModifiedBy)
        //{
        //    DALDataContext objLane = new DALDataContext();
        //    String sqlStatment = "";
        //    Int32 iCount = 0;
        //    sqlStatment = "Select distinct COUNT(1) from vwLaneAssignments WHERE  vwLaneAssignments.InventoryId IN (Select InventoryId from LaneHistory where AddedBy=@AddedBy AND CONVERT(varchar, LaneHistory.DateAdded, 101) = CONVERT(varchar, GetDate(), 101))";
        //    using (SqlConnection Conn = new SqlConnection(objLane.Connection.ConnectionString))
        //    {
        //        try
        //        {
        //            if (Conn.State == ConnectionState.Closed)
        //                Conn.Open();
        //            SqlCommand cmd = new SqlCommand(sqlStatment, Conn);
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Parameters.AddWithValue("@AddedBy", ModifiedBy);
        //            Object result = cmd.ExecuteScalar();
        //            if (result != null)
        //                iCount = Convert.ToInt32(result);
        //        }
        //        catch (Exception ex)
        //        {
        //            iCount = 0;
        //        }
        //    }
        //    return iCount;
        //}
        #endregion

        #region[ Lane Count Upto 10 recorsd Pendinding For Announcement]
        //public Int32 GetLaneCountUpto10recorsdPendindingForAnnouncement(long ModifiedBy)
        //{
        //    DALDataContext objLane = new DALDataContext();
        //    String sqlStatment = "";
        //    Int32 iCount = 0;
        //    sqlStatment = "Select Count(1) from vwGet10RecordSinceLastLogin Where modifiedby=@modifiedby";
        //    using (SqlConnection Conn = new SqlConnection(objLane.Connection.ConnectionString))
        //    {
        //        try
        //        {
        //            if (Conn.State == ConnectionState.Closed)
        //                Conn.Open();
        //            SqlCommand cmd = new SqlCommand(sqlStatment, Conn);
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Parameters.AddWithValue("@modifiedby", ModifiedBy);
        //            Object result = cmd.ExecuteScalar();
        //            if (result != null)
        //                iCount = Convert.ToInt32(result);
        //        }
        //        catch (Exception ex)
        //        {
        //            iCount = 0;
        //        }
        //    }
        //    return iCount;
        //}
        #endregion

        #region[ Lane Count Upto 100 recorsd Pendinding For Announcement]
        //public Int32 GetLaneCountUpto100recorsdPendindingForAnnouncement(long ModifiedBy)
        //{
        //    DALDataContext objLane = new DALDataContext();
        //    String sqlStatment = "";
        //    Int32 iCount = 0;
        //    sqlStatment = "Select Count(1) from vw100RecordsAvailableForAnnouncementAndEmailCount Where modifiedby=@modifiedby";
        //    using (SqlConnection Conn = new SqlConnection(objLane.Connection.ConnectionString))
        //    {
        //        try
        //        {
        //            if (Conn.State == ConnectionState.Closed)
        //                Conn.Open();
        //            SqlCommand cmd = new SqlCommand(sqlStatment, Conn);
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Parameters.AddWithValue("@modifiedby", ModifiedBy);
        //            Object result = cmd.ExecuteScalar();
        //            if (result != null)
        //                iCount = Convert.ToInt32(result);
        //        }
        //        catch (Exception ex)
        //        {
        //            iCount = 0;
        //        }
        //    }
        //    return iCount;
        //}
        #endregion

        #region [ To Select Only Exotic Pattern ]
        /// <summary>
        /// To Select Only Exotic Pattern
        /// </summary>
        /// <param name="ExoticPattern"></param>
        public IQueryable SelectExoticPattern()
        {
            DALDataContext objLan = new DALDataContext();
            IQueryable query = objLan.SelectExoticLanePattern().AsQueryable();
            return query;
        }
        #endregion

        #region [ To Update Only Exotic Pattern ]
        /// <summary>
        /// To Update Only Exotic Pattern
        /// </summary>
        /// <param name="ExoticPattern"></param>
        public void UpdateLanePattern(String ExoticPattern)
        {
            DALDataContext objLan = new DALDataContext();
            objLan.UpdateExoticLanePattern(ExoticPattern);
        }
        #endregion

        #region [ To Get All Lane Assignment list details ]
        /// <summary>
        /// To Get All Lane Assignment list details
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetInventoryFilterList()
        {
            DALDataContext objLan = new DALDataContext();
            String SQL = "";

            SQL = "SELECT distinct  * from vwLaneAssignments ";
            DataTable dTab = new DataTable("vwLaneAssignment");
            using (SqlConnection con = new SqlConnection())
            {
                SqlCommand cmd = new SqlCommand();
                con.ConnectionString = objLan.Connection.ConnectionString;

                if (con.State == ConnectionState.Closed)
                    con.Open();

                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.CommandText = SQL;
                SqlDataReader dr = cmd.ExecuteReader();

                dTab.Load(dr);
                dr.Close();
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return dTab;
        }
        #endregion

        #region [Get User logged Session wise Updated recods counts ]
        /// <summary>
        /// Get User logged Session wise Updated recods counts
        /// </summary>
        /// <param name="Userid"></param>
        /// <returns>Current updated records counts</returns>
        public int Get20Counts(Int64 Userid)
        {
            DALDataContext objLan = new DALDataContext();
            var result = (from p in objLan.vwGet20ModifiedRecordSsessionWises
                          where p.ModifiedBy == Userid
                          select p).AsQueryable().Take(20);
            return result.Count();

        }
        #endregion

        #region[Get 100 records available for announcement and email]
        /// <summary>
        /// Get 100 records available for announcement and email
        /// </summary>
        /// <param name="addedby">addedby</param>
        /// <returns></returns>
        public DataTable SelectLaneAnnouncementAndEmail(Int64 addedby)
        {
            DALDataContext objLane = new DALDataContext();
            DataTable dTab = new DataTable("Inventory100");
            using (SqlConnection Conn = new SqlConnection(objLane.Connection.ConnectionString))
            {

                String sqlStatement = "SELECT DISTINCT vw100RecordsAvailableForAnnouncementAndEmailCount.* FROM vw100RecordsAvailableForAnnouncementAndEmailCount WHERE ModifiedBy=" + addedby;

                try
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();

                    SqlCommand CMD = new SqlCommand(sqlStatement, Conn);
                    CMD.CommandType = CommandType.Text;
                    SqlDataReader dReader = CMD.ExecuteReader(CommandBehavior.CloseConnection);

                    dTab.Load(dReader);
                    dReader.Close();
                }
                catch (Exception ex)
                {
                    //TO DO
                }
            }
            return dTab;
        }

        #endregion

        #region " [ Get All Employee Email Lists] "
        /// <summary>
        /// Get All Employee Email Lists
        /// </summary>
        /// <returns>Employee Email list</returns>
        public IEnumerable selectAllEmployeeEmailLists(Int16 OrgID)
        {
            DALDataContext objLan = new DALDataContext();
            return objLan.GetAllEmployeeEmailLists(OrgID).AsEnumerable();
        }
        #endregion

        #region "Get count for announcement & email are pending since last login"
        /// <summary>
        /// Get count for announcement & email are pending since last login
        /// </summary>
        /// <param name="Userid">Userid</param>
        /// <returns>Modified records Count</returns>
        public int Get10Counts(Int64 Userid)
        {
            DALDataContext objLan = new DALDataContext();
            var result = (from p in objLan.vwGet10RecordSinceLastLogins
                          where p.ModifiedBy == Userid
                          select p).AsQueryable().Take(10);
            return result.Count();

        }


        #endregion

        #region[Select InnerHTML Of Inventory List ]
        /// <summary>
        /// Select InnerHTML Of Inventory List
        /// </summary>
        /// <returns></returns>
        public IQueryable SelectInnerHTML()
        {
            DALDataContext objLan = new DALDataContext();
            IQueryable query = objLan.SelectInnerHTML().AsQueryable();
            return query;
        }
        #endregion

        #region[Insert InnerHTML Of Inventory List for Email purpose ]
        /// <summary>
        /// Insert InnerHTML Of Inventory List for Email purpose
        /// </summary>
        /// <param name="html">HTML code</param>
        /// <returns></returns>
        public int InsertInnerHTML(string html)
        {
            DALDataContext objLan = new DALDataContext();
            return objLan.InsertInnerHTML(html);
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
            DALDataContext objLan = new DALDataContext();
            DataTable dTab = new DataTable("InventoryEdit");
            using (SqlConnection Conn = new SqlConnection(objLan.Connection.ConnectionString))
            {

                String sqlStatement = "SELECT DISTINCT vwGet10RecordSinceLastLogin.* FROM vwGet10RecordSinceLastLogin WHERE ModifiedBy=" + addedby;


                try
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();

                    SqlCommand CMD = new SqlCommand(sqlStatement, Conn);
                    CMD.CommandType = CommandType.Text;
                    SqlDataReader dReader = CMD.ExecuteReader(CommandBehavior.CloseConnection);

                    dTab.Load(dReader);
                    dReader.Close();
                }
                catch (Exception ex)
                {
                    //TO DO
                }
            }
            return dTab;
        }
        #endregion

        #region [ Select current user Session updated records]
        /// <summary>
        /// Select current user Session updated records
        /// </summary>
        /// <param name="addedby">addedby</param>
        /// <returns></returns>
        public DataTable Select20Records(Int64 addedby)
        {
            DALDataContext objLan = new DALDataContext();
            DataTable dTab = new DataTable("InventorySession");
            using (SqlConnection Conn = new SqlConnection(objLan.Connection.ConnectionString))
            {

                String sqlStatement = "SELECT DISTINCT top(20) vwGet20ModifiedRecordSsessionWise.* FROM vwGet20ModifiedRecordSsessionWise WHERE ModifiedBy=" + addedby;


                try
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();

                    SqlCommand CMD = new SqlCommand(sqlStatement, Conn);
                    CMD.CommandType = CommandType.Text;
                    SqlDataReader dReader = CMD.ExecuteReader(CommandBehavior.CloseConnection);

                    dTab.Load(dReader);
                    dReader.Close();
                }
                catch (Exception ex)
                {
                    //TO DO
                }
            }
            return dTab;
        }
        #endregion

        #region [ To Add Announcement ]
        /// <summary>
        /// To Add Announcement
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public long AddAnnouncement(Announcement obj)
        {
            DALDataContext objLan = new DALDataContext();
            Nullable<long> Id = null;
            objLan.SaveAnnouncement
                (ref Id, obj.AnnouncementTitle, obj.Description, obj.AddedBy, obj.AnnouncementTypeID, obj.OrgID);
            return Id.Value;
        }


        /// <summary>
        /// To Add Announcement Relationship
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public long AddAnnouncementRelationship(AnnouncementRelationship obj)
        {
            DALDataContext objLan = new DALDataContext();
            Nullable<long> AnnouncementRelationshipId = null;
            objLan.SaveAnnouncementRelationship
                (ref AnnouncementRelationshipId, obj.AnnouncementId, obj.EntityId, obj.AddedBy);
            return AnnouncementRelationshipId.Value;
        }
        #endregion


        #region[Get All Lane Assignment History]
        /// <summary>
        /// Get All Lane Assignment History
        /// </summary>
        /// <param name="Inventoryid">Inventoryid</param>
        /// <returns></returns>
        public IEnumerable SelectLaneAssignmentHistory(Int64 Inventoryid)
        {
            DALDataContext objLan = new DALDataContext();
            return objLan.GetLaneAssignmentHistory(Inventoryid).AsEnumerable();
        }
        #endregion

        #region [ Get Make & Model By Inventory ]
        /// <summary>
        /// Get Make & Model By Inventory
        /// </summary>
        /// <param name="inventoryId">inventoryId</param>
        /// <returns></returns>
        public IQueryable GetMakeModelByInventory(Int64 inventoryId)
        {
            DALDataContext objLan = new DALDataContext();
            IQueryable result = objLan.GetMakeModelByInventoryId(inventoryId).AsQueryable();
            return result;
        }
        #endregion

        #region [ Select Announcement List]
        /// <summary>
        /// To view All  Announcement
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetAnnouncementSelect(Int16 OrgID)
        {
            DALDataContext objLan = new DALDataContext();
            return objLan.NewAnnouncementSelect(OrgID).AsEnumerable();
        }

        /// <summary>
        /// Get Announcement count
        /// </summary>
        /// <returns></returns>
        public int GetAnnouncementCount(Int16 OrgID)
        {
            DALDataContext objLan = new DALDataContext();
            return objLan.NewAnnouncementSelect(OrgID).Count();
        }
        /// <summary>
        /// To Delete Announcement(Status Changed)
        /// </summary>
        /// <param name="AnnouncementId">AnnouncementId</param>
        /// <returns></returns>
        public long DeleteAnnouncement(Int64 AnnouncementId)
        {
            DALDataContext objLan = new DALDataContext();
            return objLan.DeleteAnnouncement(AnnouncementId);
        }
        /// <summary>
        /// To Update Announcement
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public long UpdateAnnouncement(Announcement obj)
        {
            DALDataContext objLan = new DALDataContext();
            return objLan.UpdateAnnouncement(obj.AnnouncementId, obj.AnnouncementTypeID, obj.AnnouncementTitle, obj.Description, obj.ModifiedBy);
        }
        /// <summary>
        /// To Get Announcement by id for edit
        /// </summary>
        /// <param name="AnnouncementId"></param>
        /// <returns></returns>
        public IEnumerable GetAnnouncementById(Int64 AnnouncementId)
        {
            DALDataContext objLan = new DALDataContext();
            return objLan.GetAnnouncementById(AnnouncementId).AsEnumerable();
        }
        #endregion

        #region[ Current Announcement List ]
        /// <summary>
        /// Current Announcement List
        /// </summary>
        /// <param name="AnnouncementId">AnnouncementId</param>
        /// <returns></returns>
        public static DataTable GetCurrentAnnouncementList(Int64 AnnouncementId)
        {
            DALDataContext objDAL = new DALDataContext();
            DataTable dTab = new DataTable("CurrentAnnouncementList");
            using (SqlConnection Conn = new SqlConnection(objDAL.Connection.ConnectionString))
            {

                String sqlStatement = "SELECT distinct  vwCurrentAnnouncementList.*  FROM vwCurrentAnnouncementList Where AnnouncementId=" + AnnouncementId + " ORDER BY DateAdded DESC ";

                try
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();

                    SqlCommand CMD = new SqlCommand(sqlStatement, Conn);
                    CMD.CommandType = CommandType.Text;
                    SqlDataReader dReader = CMD.ExecuteReader(CommandBehavior.CloseConnection);

                    dTab.Load(dReader);
                    dReader.Close();
                }
                catch (Exception ex)
                {

                }
            }
            return dTab;
        }

        /// <summary>
        /// To Get All Announcement type
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetAnnouncementType()
        {
            DALDataContext objLan = new DALDataContext();
            return objLan.GetAnnouncementType().AsEnumerable();
        }
        #endregion

        #region[Search Lane History]
        public DataTable SearchLaneHistory(long InvID, Int32 FieldID, DateTime DateFrom, DateTime DateTo, long AddedBy, String Source, String UpdateFrom, Int32 StartRowIndex, Int32 MaximumRows, String OrderBy, Int16 OrgID)
        {
            DataTable dTab = new DataTable("LaneHistory");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("LaneHistory_Search", Conn);
                Cmd.Parameters.AddWithValue("@InventoryID", InvID);
                Cmd.Parameters.AddWithValue("@FieldID", FieldID);
                Cmd.Parameters.AddWithValue("@DateAddedFrom", DateFrom);
                Cmd.Parameters.AddWithValue("@DateAddedTo", DateTo);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@Source", Source == null ? "" : Source);
                Cmd.Parameters.AddWithValue("@UpdatedFrom", UpdateFrom == null ? "" : UpdateFrom);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@OrderBy", OrderBy);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public Int32 SearchLaneHistoryCount(long InvID, Int32 FieldID, DateTime DateFrom, DateTime DateTo, long AddedBy, String Source, String UpdateFrom, Int32 StartRowIndex, Int32 MaximumRows, String OrderBy, Int16 OrgID)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("LaneHistory_SearchCount", Conn);
                Cmd.Parameters.AddWithValue("@InventoryID", InvID);
                Cmd.Parameters.AddWithValue("@FieldID", FieldID);
                Cmd.Parameters.AddWithValue("@DateAddedFrom", DateFrom);
                Cmd.Parameters.AddWithValue("@DateAddedTo", DateTo);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@Source", Source == null ? "" : Source);
                Cmd.Parameters.AddWithValue("@UpdatedFrom", UpdateFrom == null ? "" : UpdateFrom);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@OrderBy", OrderBy);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }
        #endregion

        #region[Table Fields]
        public List<TableField> GetTableFields()
        {
            DALDataContext dal = new DALDataContext();
            List<TableField> fields = (from f in dal.TableFields
                                       where f.IsActive == 1
                                       select f).OrderBy(f => f.FieldName).ToList<TableField>();
            return fields;
        }
        #endregion

        #region[LaneHistory Added By]
        public IEnumerable GetLaneHistoryAddedBy(Int16 OrgID)
        {
            DALDataContext dal = new DALDataContext();
            IEnumerable users = (from u in dal.SecurityUsers
                                 from l in dal.LaneHistories
                                 where u.SecurityUserID == l.AddedBy
                                 && u.OrgID==OrgID
                                 select new { u.SecurityUserID, u.DisplayName }).Distinct().OrderBy(u => u.DisplayName);
            return users;
        }
        #endregion

        #region [Added by Rupendra 18 Jan 2013 for fetch Buyer Announcement]
        public IEnumerable GetAnnouncementSelect_Ver211(Int64 SecurityUserId, short OrgID)
        {
            DALDataContext objLan = new DALDataContext();
            return objLan.NewAnnouncementSelect_Ver211(SecurityUserId, OrgID).AsEnumerable();
        }
        #endregion
    }
}

