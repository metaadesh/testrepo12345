using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using METAOPTION;
using METAOPTION.DAL;
using System.Data.SqlClient;


namespace METAOPTION.DAL
{
    public class Admin_MakeUserDAL
    {

        #region[Assign LoginID and Password To Employee]
        public Int32 AssignLoginPassword(SecurityUser objSecurityUser)
        {
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            Int32 Result = -1;

            using (SqlConnection Conn = new SqlConnection(objDAL.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("AssignLoginPassword", Conn);
                Cmd.Parameters.AddWithValue("@EntityTypeID", objSecurityUser.EntityTypeID);
                Cmd.Parameters.AddWithValue("@EntityId", objSecurityUser.EntityID);
                Cmd.Parameters.AddWithValue("@UserName", objSecurityUser.UserName);
                Cmd.Parameters.AddWithValue("@UserPassword", objSecurityUser.UserPassword);
                Cmd.Parameters.AddWithValue("@DisplayName", objSecurityUser.DisplayName);
                Cmd.Parameters.AddWithValue("@UserNote", objSecurityUser.UserNote);
                Cmd.Parameters.AddWithValue("@AddedBy", objSecurityUser.AddedBy);
                Cmd.Parameters.AddWithValue("@IsActive", objSecurityUser.IsActive);
                Cmd.Parameters.AddWithValue("@ContactID", objSecurityUser.ContactId);
                Cmd.Parameters.AddWithValue("@OrgID", objSecurityUser.OrgID);

                Cmd.CommandType = CommandType.StoredProcedure;
                Object objResult = Cmd.ExecuteScalar();
                if (objResult != null)
                    Result = Convert.ToInt32(objResult);
            }
            return Result;
        }
        #endregion

        #region [Get Associated Groups with a User]
        public IQueryable GetAssociatedGroups(long EmpId)
        {
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            IQueryable result = objDAL.GetAssociatedGroupsWithUser(EmpId).AsQueryable();
            return result;
        }
        #endregion

        #region[ Get User Info By User Id ]
        /// <summary>
        /// Get User Info By User Id
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        public DataTable GetUserInfo(long userId)
        {
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            DataTable dTab = new DataTable("UserInfo");
            using (SqlConnection Conn = new SqlConnection(objDAL.Connection.ConnectionString))
            {

              //  String sqlStatement = "Select SecurityUser.* FROM SecurityUser Where SecurityUserId = @SecurityUserId";
               
                SqlCommand Cmd = new SqlCommand("Admin_GetUserInfo", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;

                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                Cmd.Parameters.AddWithValue("@SecurityUserId", userId);
             
                SqlDataReader dReader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(dReader);
                dReader.Close();
            }
            return dTab;
        }
        #endregion

        #region[ Get ALL Active Groups ]
        /// <summary>
        /// Get ALL Active Groups 
        /// </summary>
        /// <returns></returns>
        public DataTable ActiveGroups(String grpName, String grpDescription, Int16 OrgID)
        {
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            DataTable dTab = new DataTable("ActiveGroups");
            using (SqlConnection Conn = new SqlConnection(objDAL.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                //String sqlStatement = "SELECT SecurityGroup.* From SecurityGroup Where IsActive = 1 AND OrgID=@OrgID AND GroupName Like '%' + @GroupName + '%' AND GroupDesc Like '%' + @Description +'%'";
                SqlCommand Cmd = new SqlCommand("ActiveOrganizationGroups", Conn);
                Cmd.Parameters.AddWithValue("@GroupName", grpName);
                Cmd.Parameters.AddWithValue("@Description", grpDescription);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dReader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(dReader);
                dReader.Close();
            }
            return dTab;
        }
        #endregion

        #region[ Add Group to database ]
        /// <summary>
        /// Add Group to database 
        /// </summary>
        /// <returns></returns>
        public Int32 AddUserGroup(long userId, long securityGroupId, long addedBy)
        {
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            Int32 result = -1;
            using (SqlConnection Conn = new SqlConnection(objDAL.Connection.ConnectionString))
            {
                try
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();

                    SqlCommand Cmd = new SqlCommand("Insert_UserGroup", Conn);
                    Cmd.Parameters.AddWithValue("@UserId", userId);
                    Cmd.Parameters.AddWithValue("@SecurityGroupId", securityGroupId);
                    Cmd.Parameters.AddWithValue("@AddedBy", addedBy);
                    Cmd.CommandType = CommandType.StoredProcedure;

                    Object value = Cmd.ExecuteScalar();
                    if (value != null)
                        result = Convert.ToInt32(value);
                }
                catch
                {
                    result = 0;
                }
            }
            return result;

        }
        #endregion

        #region[ Activate/Deactivate Associated Group ]
        /// <summary>
        /// Activate/Deactivate Associated Group
        /// </summary>
        /// <param name="SecurityUserGroupId">SecurityUserGroup Id</param>
        /// <param name="IsActive">Is Active</param>
        /// <param name="deletedBy">Activated By User</param>
        /// <returns></returns>
        public bool Activate_Deactivate_User_Associated_Group(long SecurityUserGroupId, int IsActive, long deletedBy)
        {
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            Int32 result = -1;
            using (SqlConnection Conn = new SqlConnection(objDAL.Connection.ConnectionString))
            {
                try
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();

                    String sqlStatement = "Update SecurityUserGroup Set IsActive = @IsActive, DeletedBy = @deletedBy, DateDeleted=GetDate() Where SecurityUserGroupId = @SecurityUserGroupId ";
                    SqlCommand Cmd = new SqlCommand(sqlStatement, Conn);
                    Cmd.Parameters.AddWithValue("@IsActive", IsActive);
                    Cmd.Parameters.AddWithValue("@deletedBy", deletedBy);
                    Cmd.Parameters.AddWithValue("@SecurityUserGroupId", SecurityUserGroupId);
                    Cmd.CommandType = CommandType.Text;

                    Object value = Cmd.ExecuteNonQuery();
                    if (value != null)
                        result = Convert.ToInt32(value);
                }
                catch
                {
                    result = 0;
                }
            }
            return result > 0;

        }
        #endregion

        #region[ Get Email and Password ]
        /// <summary>
        ///  Get Email and Password
        /// </summary>
        /// <param name="UserName">Login User Name</param>
        /// <param name="Email">Email to send the password</param>
        /// <returns></returns>
        public DataTable Email_Password(String UserName, String Email)
        {
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            DataTable dTab = new DataTable("EmailPassord");
            using (SqlConnection Conn = new SqlConnection(objDAL.Connection.ConnectionString))
            {
                try
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();

                    SqlCommand Cmd = new SqlCommand("Email_Password", Conn);
                    Cmd.Parameters.AddWithValue("@UserName", UserName);
                    Cmd.Parameters.AddWithValue("@Email", Email);

                    Cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dReader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dTab.Load(dReader);
                    dReader.Close();
                }
                catch
                {
                    dTab = null;
                }
            }
            return dTab;

        }
        #endregion

        #region[ Update Security User Information ]
        /// <summary>
        ///  Update Security User Information
        /// </summary>
        /// <param name="DisplayName">Display Name</param>
        /// <param name="UserNote">Note</param>
        /// <param name="ModifiedBy">Modified By</param>
        /// <param name="IsActive">Is Active?</param>
        /// <param name="SecurityUserId">User Id</param>
        /// <returns></returns>
        public bool SecurityUser_Update(String DisplayName, String UserNote, Int64 ModifiedBy, Int32 IsActive, Int64 SecurityUserId)
        {
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            Int32 result = (Int32)objDAL.SecurityUser_Update(DisplayName, UserNote, ModifiedBy, IsActive, SecurityUserId);
            return result > 0;
        }
        #endregion

        //#region[ To Update security user password ]

        //public bool ChangedPassword(Int64 SecurityUserId, String NewPassword)
        //{
        //    Admin_DALDataContext objDAL = new Admin_DALDataContext();
        //    Int32 UpdateResult = (Int32)objDAL.spUpdateUserPassword(SecurityUserId, NewPassword);
        //    return UpdateResult > 0;
        //}
        //#endregion

        #region [ Check User Old Password ]
        /// <summary>
        /// Check User Old Password
        /// </summary>
        /// <param name="UserPassword">UserPassword</param>
        /// <returns>Password found ? True or False</returns>
        public bool CheckUserOldPassword(string UserPassword, Int64 SecurityUserID)
        {
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            bool bOldPasswordTrue = false;
            using (SqlConnection Conn = new SqlConnection(objDAL.Connection.ConnectionString))
            {
                try
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();

                    string sqlStatment = "Select EntityId AS [EntityId]  From SecurityUser Where UserPassword = @UserPassword AND SecurityUserID=@SecurityUserID";
                    SqlCommand CMD = new SqlCommand(sqlStatment, Conn);
                    CMD.CommandType = CommandType.Text;

                    CMD.Parameters.AddWithValue("@UserPassword", UserPassword);
                    CMD.Parameters.AddWithValue("@SecurityUserID", SecurityUserID);
                    Object result = CMD.ExecuteScalar();
                    if (result != null)
                        bOldPasswordTrue = Convert.ToInt32(result) > 0;
                }
                catch (Exception ex)
                {
                    bOldPasswordTrue = false;
                }
            }
            return bOldPasswordTrue;
        }
        #endregion

        #region[Update Security User Active Status]
        public static void SecurityUser_UpdateActiveStatus(long UserID, Int32 IsActive)
        {
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            objDAL.SecurityUser_UpdateActiveStatus(UserID, IsActive);
        }
        #endregion

        #region[Fetch User Settings]
        public DataTable UserSettings(long UserID)
        {
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            DataTable dTab = new DataTable("Settings");
            using (SqlConnection Conn = new SqlConnection(objDAL.Connection.ConnectionString))
            {
                try
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();

                    SqlCommand Cmd = new SqlCommand("UserSettings", Conn);
                    Cmd.Parameters.AddWithValue("@UserID", UserID);

                    Cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dReader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dTab.Load(dReader);
                    dReader.Close();
                }
                catch
                {
                    dTab = null;
                }
            }
            return dTab;

        }
        #endregion

        #region[Update User Settings]
        public void UpdateUserSettings(long UserID, String SettingKeyValue)
        {
            UpdateUserSettingsResult UII = METAOPTION.Admin_DALDataContext.Factory.DB.UpdateUserSettings(UserID, SettingKeyValue).FirstOrDefault();

        }
        #endregion

        #region [Bind IPPersmission for Users]
        public List<IPUser_PermissionSelectResult> BindIPPermission(Int32 UserID)
        {
            List<IPUser_PermissionSelectResult> Lst = METAOPTION.Admin_DALDataContext.Factory.DB.IPUser_PermissionSelect(UserID).ToList<IPUser_PermissionSelectResult>();
            if (Lst.Count > 0)
                return Lst;
            else
                return null;
        }
        #endregion



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

    }
}
