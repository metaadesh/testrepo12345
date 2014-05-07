using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using METAOPTION;
using System.Data.SqlClient;

namespace METAOPTION.DAL
{
    public class Admin_ViewGroupListDAL
    {
        Admin_DALDataContext objDAL = new Admin_DALDataContext();
        
        #region [Get All Group List]
        public IQueryable<SecurityGroup> GetAllGroups()
        {
            IQueryable<SecurityGroup> result = (from p in objDAL.SecurityGroups
                                                where p.IsActive == 1
                                                select p) as IQueryable<SecurityGroup>;
            return result;
        }
        #endregion

        #region [Get Group Details]
        /// <summary>
        /// region to get group details
        /// by groupId
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetGroupDetails_ByGroupId(long GroupId)
        {
            var query = (from p in objDAL.SecurityGroups
                         where p.SecurityGroupId == GroupId
                         select p) as IQueryable<SecurityGroup>;
            // System.Collections.Hashtable ht = new System.Collections.Hashtable();

            Dictionary<string, string> grp = new Dictionary<string, string>();

            foreach (SecurityGroup item in query)
            {
                grp.Add("GName", item.GroupName);
                grp.Add("GDesc", item.GroupDesc);


            }

            return grp;
        }
        #endregion
        
        #region [Added by Rupendra 21 Nov 12 For ManagePermission page]
        public DataTable GetAllManagePermission(Int16 OrgID)
        {
            DataTable dTab = new DataTable();

            Admin_DALDataContext objDal = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Admin_IPSearch", Conn);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);

                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public DataTable GetManagePermissionByID(Int64 IPPermissionID, String SUID)
        {
            DataTable dTab = new DataTable();

            Admin_DALDataContext objDal = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("IPPermissions_GetManagePermissionByID", Conn);
                Cmd.Parameters.AddWithValue("@IPPermissionID", IPPermissionID);
                Cmd.Parameters.AddWithValue("@SecurityUserID", SUID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }


        public Int32 SaveIPPermission(String IPAddress, int IPType, string Description, Int64 AddedBy, Int16 OrgID)
        {
            Int32 Result = 0;
            Admin_DALDataContext objDal = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("IPPermissions_Insert", Conn);
                Cmd.Parameters.AddWithValue("@IPAddress", IPAddress);
                Cmd.Parameters.AddWithValue("@IPType", IPType);
                Cmd.Parameters.AddWithValue("@Description", Description);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteNonQuery();
                objDal.Dispose();
            }
            return Result;
        }

        public Int32 UpdateIPPermission(Int64 IPPermissionID, String IPAddress, int IPType, string Description, Int64 ModifiedBy)
        {
            Int32 Result = 0;
            Admin_DALDataContext objDal = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("IPPermissions_Update", Conn);
                Cmd.Parameters.AddWithValue("@IPPermissionID", IPPermissionID);
                Cmd.Parameters.AddWithValue("@IPAddress", IPAddress);
                Cmd.Parameters.AddWithValue("@IPType", IPType);
                Cmd.Parameters.AddWithValue("@Description", Description);
                Cmd.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteNonQuery();
                objDal.Dispose();
            }
            return Result;
        }

        public Int32 DeleteIPPermission(Int64 IPPermissionID, Int32 IPType, Int64 DeletedBy, String UsersIDs)
        {
            Int32 Result = 0;
            Admin_DALDataContext objDal = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("IPPermissions_DeletebyIPPermissionID", Conn);

                Cmd.Parameters.AddWithValue("@IPPermissionID", IPPermissionID);
                Cmd.Parameters.AddWithValue("@IPType", IPType);
                Cmd.Parameters.AddWithValue("@SecurityUserID", UsersIDs);
                Cmd.Parameters.AddWithValue("@DeletedBy", DeletedBy);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteNonQuery();
                objDal.Dispose();
            }
            return Result;
        }


        public DataTable GetBindSearchGrid(string IPAddress, string IPType, string Description, string EntityId, Int32 EntityTypeId, string strSort,Int16 OrgID)
        {
            DataTable dTab = new DataTable();

            Admin_DALDataContext objDal = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Admin_IPSearch", Conn);
                Cmd.Parameters.AddWithValue("@IPAddress", IPAddress);
                Cmd.Parameters.AddWithValue("@IPType", IPType);
                Cmd.Parameters.AddWithValue("@Description", Description);
                Cmd.Parameters.AddWithValue("@EntityId", EntityId);
                Cmd.Parameters.AddWithValue("@EntityType", EntityTypeId);
                Cmd.Parameters.AddWithValue("@SortBy", strSort);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }


        public DataTable CheckDuplicateIPAddress(string IPAddress)
        {
            DataTable dTab = new DataTable();
            Admin_DALDataContext objDal = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("IPPermissions_CheckDuplicateIP", Conn);
                Cmd.Parameters.AddWithValue("@IPAddress", IPAddress);
                Cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public DataTable GetEntity(Int32 EntityTypeId,Int16 OrgID)
        {
            DataTable dTab = new DataTable();
            Admin_DALDataContext objDal = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("IPPermission_SelectEntity", Conn);
                Cmd.Parameters.AddWithValue("@EntityType", EntityTypeId);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public Int32 AddUserPermissionIP(String IPAddress, int IPType, string Description, Int32 EntityTypeId, DataTable dtEntityId, Int64 AddedBy, Int16 OrgID)
        {
            Int32 Result = 0;
            Admin_DALDataContext objDal = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("IPPermissions_InsertUserIP", Conn);
                Cmd.Parameters.AddWithValue("@IPAddress", IPAddress);
                Cmd.Parameters.AddWithValue("@IPType", IPType);
                Cmd.Parameters.AddWithValue("@Description", Description);
                Cmd.Parameters.AddWithValue("@EntityTypeId", EntityTypeId);
                Cmd.Parameters.AddWithValue("@EntityId", dtEntityId);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter ouparm = Cmd.Parameters.Add("@IPPermissionID", SqlDbType.BigInt);
                ouparm.Direction = ParameterDirection.Output;
                Result = (Int32)Cmd.ExecuteNonQuery();
                objDal.Dispose();
            }
            return Result;
        }

        public Int32 UpdateUserPermissionIP(Int64 IPPermissionID, String IPAddress, int IPType, string Description, DataTable dt, Int64 AddedBy)
        {
            Int32 Result = 0;
            Admin_DALDataContext objDal = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("IPPermissions_UpdateUserIP", Conn);
                Cmd.Parameters.AddWithValue("@IPPermissionID", IPPermissionID);
                Cmd.Parameters.AddWithValue("@IPAddress", IPAddress);
                Cmd.Parameters.AddWithValue("@IPType", IPType);
                Cmd.Parameters.AddWithValue("@Description", Description);
                //Cmd.Parameters.AddWithValue("@EntityTypeId", EntityTypeId);
                Cmd.Parameters.AddWithValue("@EntityId", dt);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteNonQuery();
                objDal.Dispose();
            }
            return Result;
        }


        public DataTable IPPermissionEntityName(Int32 EntityTyeId, string IPAddress, Int32 IPPerMissionID)
        {
            DataTable dTab = new DataTable();
            Admin_DALDataContext objDal = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("IPPermission_GetEntityName", Conn);
                Cmd.Parameters.AddWithValue("@IPAddress", IPAddress);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTyeId);
                Cmd.Parameters.AddWithValue("@IPPermissionID", IPPerMissionID);
                Cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }


        public Int32 AutoIPRestriction(Int32 IPUserId, Int32 ModifiedBy, Int32 IPType, int Flag)
        {
            Int32 Result = 0;
            Admin_DALDataContext objDal = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("IPPermissions_IPRestriction_ver211", Conn);
                Cmd.Parameters.AddWithValue("@IPTypeValue", Flag);
                Cmd.Parameters.AddWithValue("@IPUserId", IPUserId);
                Cmd.Parameters.AddWithValue("@ModifyBy", ModifiedBy);             
                Cmd.CommandType = CommandType.StoredProcedure;
                Result = (Int32)Cmd.ExecuteNonQuery();
                objDal.Dispose();
            }
            return Result;
        }

        public Int32 AutoIPRestriction(Int32 IPUserId,  Int32 IPType, int Flag)
        {
            Int32 Result = 0;
            Admin_DALDataContext objDal = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("IPPermissions_IPRestriction_ver211", Conn);
                Cmd.Parameters.AddWithValue("@IPTypeValue", Flag);
                Cmd.Parameters.AddWithValue("@IPUserId", IPUserId);               
                Cmd.Parameters.AddWithValue("@IPType", IPType);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteNonQuery();
                objDal.Dispose();
            }
            return Result;
        }

        public Int32 DeleteIPRestriction(Int32 IPPermissionID, Int32 UserId, Int32 IPType, Int64 DeletedBy)
        {
            Int32 Result = 0;
            Admin_DALDataContext objDal = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("IPPermissions_DeleteIPRestriction", Conn);

                Cmd.Parameters.AddWithValue("@IPPermissionID", IPPermissionID);
                Cmd.Parameters.AddWithValue("@IPUserID", UserId);
                Cmd.Parameters.AddWithValue("@IPType", IPType);
                Cmd.Parameters.AddWithValue("@DeletedBy", DeletedBy);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteNonQuery();
                objDal.Dispose();
            }
            return Result;
        }



        public DataTable GetEntityIdbyIPAddredd(string IPAddress, Int32 IPType, short OrgID)
        {
            DataTable dTab = new DataTable();
            Admin_DALDataContext objDal = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("IPPermission_GetEntityIdbyIPAddress", Conn);
                Cmd.Parameters.AddWithValue("@IPAddress", IPAddress);
                Cmd.Parameters.AddWithValue("@IPType", IPType);
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

        #region [Added by Rupendra 12 - 12 - 12 for ]
        public DataTable GetSettingKeysValue(String ProjectName)
        {
            DataTable dTab = new DataTable();

            Admin_DALDataContext objDal = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Settings_Select", Conn);
                Cmd.Parameters.AddWithValue("@Project", ProjectName);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public Int32 SettingsKeysInsert(String SettingKey, String SettingkeyValue, String Project, String Description)
        {
            Int32 Result = 0;
            Admin_DALDataContext objDal = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("SettingsKeys_Insert", Conn);

                Cmd.Parameters.AddWithValue("@SettingKey", SettingKey);
                Cmd.Parameters.AddWithValue("@SettingkeyValue", SettingkeyValue);
                Cmd.Parameters.AddWithValue("@Project", Project);
                Cmd.Parameters.AddWithValue("@Description", Description);
                SqlParameter ouparm = Cmd.Parameters.Add("@SettingkeyID", SqlDbType.Int);
                ouparm.Direction = ParameterDirection.Output;

                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteNonQuery();
                objDal.Dispose();
            }
            return Result;
        }

        #endregion
    }
}
