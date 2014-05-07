using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace METAOPTION
{
    /* 
        <author>Prem Shanker Verma</author>
        <date>11-Sep-2013</date>
        <summary>This class provide the data for "InventorySearchPage"</summary>
    */

    public class InventoryDepositSearchDAL
    {
        #region Populate Deposit Search Filters

        public DataSet GetSearchFilters(Int16 OrgID)
        {
            DataSet ds = new DataSet();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("Deposit_GetDepositFilterData", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(ds);
                objDal.Dispose();
            }
            return ds;
        }

        #endregion

        #region Deposit Details and Counts

        public DataTable GetDepositsDetails(String VIN, Int32 Year, Int32 MakeID, Int32 ModelID, Int32 BodyID, Double AmountFrom, Double AmountTo, String AddedBy, String DateFrom, String DateTo, String BuyerIDs, String Comment, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, Int16 OrgID)
        {
            DataTable dt = new DataTable();

            try
            {
                DALDataContext objDal = new DALDataContext();
                using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();
                    SqlCommand Cmd = new SqlCommand("Deposit_ViewAllDeposits", Conn);
                    Cmd.Parameters.AddWithValue("@VIN", VIN);
                    Cmd.Parameters.AddWithValue("@Year", Year);
                    Cmd.Parameters.AddWithValue("@MakeId", MakeID);
                    Cmd.Parameters.AddWithValue("@ModelId", ModelID);
                    Cmd.Parameters.AddWithValue("@BodyId", BodyID);
                    Cmd.Parameters.AddWithValue("@AmountFrom", AmountFrom);
                    Cmd.Parameters.AddWithValue("@AmountTo", AmountTo);
                    Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                    Cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                    Cmd.Parameters.AddWithValue("@DateTo", DateTo);
                    Cmd.Parameters.AddWithValue("@BuyerIDs", BuyerIDs);
                    Cmd.Parameters.AddWithValue("@Comment", Comment);
                    Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                    Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                    Cmd.Parameters.AddWithValue("@sortExpression", SortExpression);
                    Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(reader);
                    reader.Close();
                    objDal.Dispose();
                }
            }
            catch { }
            return dt;
        }

        public Int32 GetDepositsDetailsCount(String VIN, Int32 Year, Int32 MakeID, Int32 ModelID, Int32 BodyID, Double AmountFrom, Double AmountTo, String AddedBy, String DateFrom, String DateTo, String BuyerIDs, String Comment, Int16 OrgID)//, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            Int32 Result = 0;
            try
            {
                DALDataContext objDal = new DALDataContext();
                using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();

                    SqlCommand Cmd = new SqlCommand("Deposit_ViewAllDepositsCount", Conn);
                    Cmd.Parameters.AddWithValue("@VIN", VIN);
                    Cmd.Parameters.AddWithValue("@Year", Year);
                    Cmd.Parameters.AddWithValue("@MakeId", MakeID);
                    Cmd.Parameters.AddWithValue("@ModelId", ModelID);
                    Cmd.Parameters.AddWithValue("@BodyId", BodyID);
                    Cmd.Parameters.AddWithValue("@AmountFrom", AmountFrom);
                    Cmd.Parameters.AddWithValue("@AmountTo", AmountTo);
                    Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                    Cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                    Cmd.Parameters.AddWithValue("@DateTo", DateTo);
                    Cmd.Parameters.AddWithValue("@BuyerIDs", BuyerIDs);
                    Cmd.Parameters.AddWithValue("@Comment", Comment);
                    Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Result = (Int32)Cmd.ExecuteScalar();
                    objDal.Dispose();
                }
            }
            catch { }
            return Result;
        }


        #endregion

        #region Data Table based on Queries

        public DataTable GetDataTable(String strQuery)
        {
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
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

        #region Data Table for Deposit History

        public DataTable GetDepositHistory(Int64 InventoryID, String ColumnName)
        {
            DataTable dt = new DataTable();
            try
            {
                DALDataContext objDal = new DALDataContext();
                using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();
                    SqlCommand Cmd = new SqlCommand("Deposit_DepositDetail", Conn);
                    Cmd.Parameters.AddWithValue("@InventoryID", InventoryID);
                    Cmd.Parameters.AddWithValue("@ColumnName", ColumnName);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(reader);
                    reader.Close();
                    objDal.Dispose();
                }
            }
            catch { }
            return dt;
        }

        #endregion

        #region Inventory Detail

        public DataTable GetInventoryDetail(Int64 InventoryID)
        {
            DataTable dt = new DataTable();
            try
            {
                DALDataContext objDal = new DALDataContext();
                using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();
                    SqlCommand Cmd = new SqlCommand("Deposit_InventoryDetail", Conn);
                    Cmd.Parameters.AddWithValue("@InventoryID", InventoryID);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(reader);
                    reader.Close();
                    objDal.Dispose();
                }
            }
            catch { }
            return dt;
        }

        #endregion


        #region Data Table for SortDDL

        public DataTable GetSortData(Int32 TableID, String ExcludeValues)
        {
            DataTable dt = new DataTable();
            //try
            //{
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("Deposit_FillSortDDL", Conn);
                Cmd.Parameters.AddWithValue("@TableID", TableID);
                Cmd.Parameters.AddWithValue("@ExcludeValues", ExcludeValues);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            //}
            //catch { }
            return dt;
        }

        #endregion

        #region Daily Deposit Summary And Count

        public DataTable GetDepositSummary_Daily(String DateFrom, String DateTo, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, String SortDirection, Int16 OrgID)
        {
            DataTable dt = new DataTable();

            //try
            //{
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("Deposit_DepositsSummary_Daily", Conn);
                Cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                Cmd.Parameters.AddWithValue("@DateTo", DateTo);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@sortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@SortDirection", SortDirection);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            //}
            //catch { }
            return dt;
        }

        public Int32 GetDepositSummary_DailyCount(String DateFrom, String DateTo, Int16 OrgID)//, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            Int32 Result = 0;
            //try
            //{
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("Deposit_DepositsSummary_Dailycount", Conn);
                Cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                Cmd.Parameters.AddWithValue("@DateTo", DateTo);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            //}
            //catch { }
            return Result;
        }

        #endregion

        #region Daily Deposit Summary Detail

        public DataTable GetDepositSummaryDetail_Daily(String DepositDate, String SortExpression, String SortDirection, Int16 OrgID)
        {
            DataTable dt = new DataTable();

            //try
            //{
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("Deposit_SummaryDetail_Daily", Conn);
                Cmd.Parameters.AddWithValue("@DepositDate", DepositDate);
                Cmd.Parameters.AddWithValue("@sortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@SortDirection", SortDirection);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            //}
            //catch { }
            return dt;
        }
        #endregion

        #region Monthly Deposit Summary And its Count

        public DataTable GetDepositSummary_Monthly(String DateFrom, String DateTo, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, String SortDirection, Int16 OrgID)
        {
            DataTable dt = new DataTable();

            //try
            //{
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("Deposit_DepositsSummary_Monthly", Conn);
                Cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                Cmd.Parameters.AddWithValue("@DateTo", DateTo);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@sortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@SortDirection", SortDirection);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            //}
            //catch { }
            return dt;
        }

        public Int32 GetDepositSummary_MonthlyCount(String DateFrom, String DateTo, Int16 OrgID)//, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            Int32 Result = 0;
            //try
            //{
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("Deposit_DepositsSummary_MonthlyCount", Conn);
                Cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                Cmd.Parameters.AddWithValue("@DateTo", DateTo);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            //}
            //catch { }
            return Result;
        }


        #endregion

        #region Monthly Deposit Summary Detail

        public DataTable GetDepositSummaryDetail_Monthly(String DepositDateFrom, String DepositDateTo, String SortExpression, String SortDirection, Int16 OrgID)
        {
            DataTable dt = new DataTable();

            //try
            //{
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("Deposit_SummaryDetail_Monthly", Conn);
                Cmd.Parameters.AddWithValue("@DepositDateFrom", DepositDateFrom);
                Cmd.Parameters.AddWithValue("@DepositDateTo", DepositDateTo);
                Cmd.Parameters.AddWithValue("@sortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@SortDirection", SortDirection);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            //}
            //catch { }
            return dt;
        }
        #endregion


    }
}
