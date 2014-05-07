using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace METAOPTION.DAL
{
    public class ActivityDAL
    {
        #region[Fetch Activity Stats]
        public DataTable FetchActivityStats(DateTime startDate, DateTime endDate)
        {
            DataTable dTab = new DataTable("ActivityStats");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("ActivityStats_Fetch", Conn);
                //Cmd.Parameters.AddWithValue("@SortPreference", SortPreference);
                Cmd.Parameters.AddWithValue("@StartDate", startDate);
                Cmd.Parameters.AddWithValue("@EndDate", endDate);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region[Insert record into Activity Stats table]
        public void InsertActivityStats(ActivityStat obj)
        {
            DALDataContext objDal = new DALDataContext();
            objDal.ActivityStats_Insert(obj.ActivityLookUpID, obj.RefKey, obj.TableID, obj.ColumnID, obj.UserID);
        }
        #endregion

        #region[Fetch Activity Stats - Location/Unresolved]
        public DataTable FetchLocationActivityStats()
        {
            DataTable dTab = new DataTable("Location");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Activity_UnresolvedCars", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region[Fetch Activity Stats]
        public DataTable FetchActivityStatsDetail(DateTime startDate, DateTime endDate,String Code)
        {
            DataTable dTab = new DataTable("ActivityStats");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("ActivityStatsDetails_Fetch", Conn);
                //Cmd.Parameters.AddWithValue("@SortPreference", SortPreference);
                Cmd.Parameters.AddWithValue("@StartDate", startDate);
                Cmd.Parameters.AddWithValue("@EndDate", endDate);
                Cmd.Parameters.AddWithValue("@Code", Code);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region[Fetch Activity UnResolvedCar Detail]
        public DataTable GetUnResolvedCarDetail(String Code, Int32 StartRowIndex, Int32 MaximumRows)
        {
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Activity_UnResolvedCarStats", Conn);
                Cmd.Parameters.AddWithValue("@Code", Code);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region[Fetch Activity LocationStats Detail]
        public DataTable GetLocationStatsDetail(String Code, Int32 StartRowIndex, Int32 MaximumRows)
        {
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Activity_LocationStats", Conn);
                Cmd.Parameters.AddWithValue("@Code", Code);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        public Int32 GetUnResolvedCarDetailCount(String Code, Int32 StartRowIndex, Int32 MaximumRows)
        {
            Int32 Result = 0;

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Activity_UnResolvedCarStatsCount", Conn);
                Cmd.Parameters.AddWithValue("@Code", Code);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.CommandType = CommandType.StoredProcedure;
               // Result = (Int32)Cmd.ExecuteScalar();
                var result = Cmd.ExecuteScalar();
                if (result != null)
                    Result = Convert.ToInt32(result);

                objDal.Dispose();
            }
            return Result;
        }

        public Int32 GetLocationStatsDetailCount(String Code, Int32 StartRowIndex, Int32 MaximumRows)
        {
            Int32 Result = 0;

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Activity_LocationStatsCount", Conn);
                Cmd.Parameters.AddWithValue("@Code", Code);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.CommandType = CommandType.StoredProcedure;
                var result = Cmd.ExecuteScalar();
                if (result != null)
                    Result = Convert.ToInt32(result);

                objDal.Dispose();
            }
            return Result;
        }

    }
}
