using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace METAOPTION
{
    public class EmailDAL
    {
        public DataTable GetEmailDetail(Int32 MasterLookUpID, long RefKey)
        {
            DataTable dTab = new DataTable("Emails");

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_GetEmailDetail", Conn);
                Cmd.Parameters.AddWithValue("@MasterLookUpID", MasterLookUpID);
                Cmd.Parameters.AddWithValue("@RefKey", RefKey);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public DataTable GetSMSDetail(Int32 MasterLookUpID, long RefKey)
        {
            DataTable dTab = new DataTable("SMS");

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_GetSMSDetail", Conn);
                Cmd.Parameters.AddWithValue("@MasterLookUpID", MasterLookUpID);
                Cmd.Parameters.AddWithValue("@RefKey", RefKey);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
    }
}
