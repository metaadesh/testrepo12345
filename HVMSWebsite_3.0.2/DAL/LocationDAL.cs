using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace METAOPTION
{
    public class LocationDAL
    {
        DALDataContext objDAL = new DALDataContext();

        //#region[Fetch VIN location by VIN, LocationID and AddedBy]
        //public DataTable FetchVINLocation(String VIN, long LocationID, long AddedBy)
        //{
        //    DataTable dTab = new DataTable("Mobile_VinLocation");
        //    DALDataContext objDal = new DALDataContext();
        //    using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
        //    {
        //        if (Conn.State == ConnectionState.Closed)
        //            Conn.Open();

        //        SqlCommand Cmd = new SqlCommand("Mobile_GetVINLocation", Conn);
        //        Cmd.Parameters.AddWithValue("@VIN", VIN);
        //        Cmd.Parameters.AddWithValue("@LocationID", LocationID);
        //        Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
        //        Cmd.CommandType = CommandType.StoredProcedure;
        //        SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        dTab.Load(reader);
        //        reader.Close();
        //        objDal.Dispose();
        //    }
        //    return dTab;
        //}
        //#endregion

        #region [Get location to bind dropdown]
        public IQueryable GetAllLocations()
        {
            return METAOPTION.DALDataContext.Factory.DB.Mobile_GetLocation().AsQueryable();
        }
        #endregion

        #region [Get location to bind dropdown for vendor]
        public IQueryable GetAllLocations(Int32 AddedBy)
        {
            return METAOPTION.DALDataContext.Factory.DB.Mobile_GetLocation_ver211(AddedBy).AsQueryable();
        }
        #endregion

        #region [Get location to bind dropdown for child buyer]
        public IQueryable GetAllLocations(Int32 AddedBy, Int32 ParentBuyerID)
        {
            return METAOPTION.DALDataContext.Factory.DB.Mobile_GetLocationForChildBuyer_ver211(AddedBy, ParentBuyerID).AsQueryable();
        }

        public List<Mobile_EntityLocation> GetAllLocations(Int32 SecurityUserID, Int32 ParentEntityID, Int32 EntityTypeID, Int16 OrgID)
        {
            List<Mobile_EntityLocation> lst = new List<Mobile_EntityLocation>();
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_GetLocation_ver2111", Conn);
                Cmd.Parameters.AddWithValue("@SecurityUserID", SecurityUserID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            foreach (DataRow dr in dTab.Rows)
            {
                Mobile_EntityLocation SU = new Mobile_EntityLocation();
                SU.LocationCode = dr["LocationCode"].ToString();
                SU.LocationID = Convert.ToInt32(dr["LocationID"]);
                lst.Add(SU);
            }
            return lst;
        }
        #endregion

        #region [Get location inventory stats]
        public IQueryable LocationInventoryStats(long locationStatus, Int16 OrgID)
        {
            return METAOPTION.DALDataContext.Factory.DB.Mobile_Location_InventoryStats(locationStatus, OrgID).AsQueryable();
        }
        #endregion

        //#region [Get User name and id to bind dropdown on view location screen]
        //public IQueryable GetAllLocationUsers()
        //{
        //    return METAOPTION.DALDataContext.Factory.DB.Mobile_GetLocationUsers().AsQueryable();
        //}
        //#endregion

        #region[View Location added by users]
        public List<SecurityUser> GetAllLocationUsers()
        {
            DALDataContext objDal = new DALDataContext();
            List<SecurityUser> result = (from p in objDal.SecurityUsers
                                         from I in objDal.Mobile_VinLocations
                                         where p.SecurityUserID == I.AddedBy
                                         select p).Distinct().OrderBy(p => p.DisplayName).ToList<SecurityUser>();
            return result;
        }
        #endregion

        #region[View Location added by users]
        public List<SecurityUser> GetAllLocationUsers_AddedBy(Int32 AddedBy)
        {
            DALDataContext objDal = new DALDataContext();
            List<SecurityUser> result = (from p in objDal.SecurityUsers
                                         from I in objDal.Mobile_VinLocations
                                         where p.SecurityUserID == I.AddedBy && I.AddedBy == AddedBy
                                         select p).Distinct().OrderBy(p => p.DisplayName).ToList<SecurityUser>();
            return result;
        }
        #endregion

        #region[ Added by  Vipin, View Location added by users for ChildBuyer]
        public List<Mobile_VinLocation_AddedByUsersResult> GetAllLocationUsers_AddedBy(Int32 AddedBy, Int32 ParentBuyerID)
        {
            DALDataContext objDal = new DALDataContext();
            return objDal.Mobile_VinLocation_AddedByUsers(AddedBy, ParentBuyerID).ToList<Mobile_VinLocation_AddedByUsersResult>();



        }

        public List<SecurityUser> GetAllLocationUsers_AddedBy(Int32 SecurityUserID, Int32 ParentEntityID, Int32 EntityTypeID, Int16 OrgID)
        {
            List<SecurityUser> lst = new List<SecurityUser>();
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_VinLocation_AddedByUsers_ver211", Conn);
                Cmd.Parameters.AddWithValue("@SecurityUserID", SecurityUserID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            foreach (DataRow dr in dTab.Rows)
            {
                SecurityUser SU = new SecurityUser();
                SU.DisplayName = dr["DisplayName"].ToString();
                SU.SecurityUserID = Convert.ToInt32(dr["SecurityUserID"]);
                lst.Add(SU);
            }
            return lst;

        }

        #endregion

        #region[VIN location]
        public List<Mobile_GetVINLocationResult> FetchVINLocation(String VIN, long LocationID, long AddedBy, Int32 StartRowIndex, Int32 MaximumRows,Int16 OrgID)
        {
            DALDataContext objDal = new DALDataContext();
            return objDal.Mobile_GetVINLocation(VIN, LocationID, AddedBy, StartRowIndex, MaximumRows,OrgID).ToList<Mobile_GetVINLocationResult>();
        }

        public List<Mobile_GetVINLocation_Ver211Result> FetchVINLocationVer211(String VIN, long LocationID, long AddedBy, Int32 EntityTypeId, Int32 SecurityUserId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            DALDataContext objDal = new DALDataContext();
            return objDal.Mobile_GetVINLocation_Ver211(VIN, LocationID, AddedBy, EntityTypeId, SecurityUserId, StartRowIndex, MaximumRows).ToList<Mobile_GetVINLocation_Ver211Result>();
        }


        public DataTable FetchVINLocationVer211(String VIN, long LocationID, long AddedBy, Int32 EntityTypeID, Int32 EntityID, Int32 ParentEntityID, Int32 StartRowIndex, Int32 MaximumRows, Int16 OrgID)
        {
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_GetVINLocation_Ver2111", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@LocationID", LocationID);
                Cmd.Parameters.AddWithValue("@EntityTypeId", EntityTypeID);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
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

        #region[VIN location count]
        public Int32? FetchVINLocationCount(String VIN, long LocationID, long AddedBy, Int32 StartRowIndex, Int32 MaximumRows, Int16 OrgID)
        {
            Mobile_GetVINLocationCountResult result = METAOPTION.DALDataContext.Factory.DB.Mobile_GetVINLocationCount(VIN, LocationID, AddedBy, StartRowIndex, MaximumRows,OrgID).Single();
            return result.Total;
        }
        public Int32? FetchVINLocationCountVer211(String VIN, long LocationID, long AddedBy, Int32 EntityTypeId, Int32 SecurityUserId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            Mobile_GetVINLocationCount_Ver211Result result = METAOPTION.DALDataContext.Factory.DB.Mobile_GetVINLocationCount_Ver211(VIN, LocationID, AddedBy, EntityTypeId, SecurityUserId, StartRowIndex, MaximumRows).Single();
            return result.Total;
        }

        public Int32? FetchVINLocationCountVer211(String VIN, long LocationID, long AddedBy, Int32 EntityTypeID, Int32 EntityID, Int32 ParentEntityID, Int32 StartRowIndex, Int32 MaximumRows, Int16 OrgID)
        {
            Int32 Result = 0;

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_GetVINLocationCount_Ver2111", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@LocationID", LocationID);
                Cmd.Parameters.AddWithValue("@EntityTypeId", EntityTypeID);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }


        #endregion
    }
}
