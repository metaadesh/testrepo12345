using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace METAOPTION.DAL
{
    /*
         Author:        Prem Shanker Verma
         Date:          22-Jan-2014
         Description:   Contains the functions of system mudule like Insert, Seach, update etc
    */
    public class Admin_SystemDAL
    {

        public Int32 AddNewSystem(Int16 OrgID, String Description, String ImagePath, Boolean IsActive, Boolean PeachTree)
        {
            Int32 SystemID = 0;
            Admin_DALDataContext context = new Admin_DALDataContext();
            using (SqlConnection con = new SqlConnection(context.Connection.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("Admin_AddNewSystem", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrgID", OrgID);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
                cmd.Parameters.AddWithValue("@IsActive", IsActive);
                cmd.Parameters.AddWithValue("@PeachTree", PeachTree);
                SystemID = (Int32)cmd.ExecuteScalar();
                context.Dispose();
            }
            return SystemID;
        }

        public DataTable SystemSearch(Int16 OrgID, String SystemName, Int32 ActiveStatus)
        {
            DataTable dtSystem = new DataTable();
            Admin_DALDataContext context = new Admin_DALDataContext();
            using (SqlConnection con = new SqlConnection(context.Connection.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("Admin_SystemSearch", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrgID", OrgID);
                cmd.Parameters.AddWithValue("@SystemName", SystemName);
                cmd.Parameters.AddWithValue("@ActiveStatus", ActiveStatus);
                SqlDataReader sdr = cmd.ExecuteReader();
                dtSystem.Load(sdr);
                context.Dispose();
            }
            return dtSystem;
        }

        public DataTable GetSystemDetails(Int32 SystemID)
        {
            DataTable dtSystem = new DataTable();
            Admin_DALDataContext context = new Admin_DALDataContext();
            using (SqlConnection con = new SqlConnection(context.Connection.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("Admin_GetSystemDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SystemID", SystemID);
                SqlDataReader sdr = cmd.ExecuteReader();
                dtSystem.Load(sdr);
                context.Dispose();
            }
            return dtSystem;
        }

        public Int32 UpdateSystem(String SystemName, String ImagePath, Boolean IsActive, Boolean PeachTree, Int32 SystemID)
        {
            Int32 rowEffected = 0;
            Admin_DALDataContext context = new Admin_DALDataContext();
            using (SqlConnection con = new SqlConnection(context.Connection.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("Admin_UpdateSystemDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SystemName", SystemName);
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
                cmd.Parameters.AddWithValue("@IsActive", IsActive);
                cmd.Parameters.AddWithValue("@PeachTree", PeachTree);
                cmd.Parameters.AddWithValue("@SystemID", SystemID);
                rowEffected = cmd.ExecuteNonQuery();
                context.Dispose();
            }
            return rowEffected;
        }

        public Int32 UpdateSystemImagePath(Int32 SystemID, String ImagePath)
        {
            Int32 rowEffected = 0;
            Admin_DALDataContext context = new Admin_DALDataContext();
            using (SqlConnection con = new SqlConnection(context.Connection.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("Admin_UpdateSystemLogoPath", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SystemID", SystemID);
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
                rowEffected = cmd.ExecuteNonQuery();
                context.Dispose();
            }
            return rowEffected;
        }

        public Int32 UpdateSystemActiveStatus(Int32 SystemID, Boolean IsActive)
        {
            Int32 rowEffected = 0;
            Admin_DALDataContext context = new Admin_DALDataContext();
            using (SqlConnection con = new SqlConnection(context.Connection.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("Admin_ChangeSystemActiveStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SystemID", SystemID);
                cmd.Parameters.AddWithValue("@IsActive", IsActive);
                rowEffected = cmd.ExecuteNonQuery();
                context.Dispose();
            }
            return rowEffected;
        }

    }
}
