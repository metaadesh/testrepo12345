using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace METAOPTION.DAL
{
    public class Accounting_ExpenseDepositDAL
    {
        public static DataTable GetFinanceReportData(DateTime FromDate,DateTime ToDate,String Filter,int startRowIndex, int maximumRows)
        {
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("FinanceReport_Getlist", Conn);
                Cmd.Parameters.AddWithValue("@FromDate", FromDate);
                Cmd.Parameters.AddWithValue("@ToDate", ToDate);
                Cmd.Parameters.AddWithValue("@StartRowIndex", startRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", maximumRows);
                Cmd.Parameters.AddWithValue("@filter", Filter);
                
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(dr);
                objDal.Dispose();
            }
            return dTab;
        }

        public static Int32 GetFinanceReportDataCount(DateTime FromDate, DateTime ToDate, String Filter, int startRowIndex, int maximumRows)
        {
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("FinanceReport_GetlistCount", Conn);
                Cmd.Parameters.AddWithValue("@FromDate", FromDate);
                Cmd.Parameters.AddWithValue("@ToDate", ToDate);
                Cmd.Parameters.AddWithValue("@StartRowIndex", startRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", maximumRows);
                Cmd.Parameters.AddWithValue("@filter", Filter);

                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(dr);
                objDal.Dispose();
            }
            return Convert.ToInt32(dTab.Rows[0][0].ToString());
        }
    }
}
