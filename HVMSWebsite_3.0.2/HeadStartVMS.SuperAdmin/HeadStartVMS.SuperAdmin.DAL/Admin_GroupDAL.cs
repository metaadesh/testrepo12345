using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace METAOPTION.DAL
{
    public class Admin_GroupDAL
    {
        Admin_DALDataContext objDAL = new Admin_DALDataContext();
        #region [Get All Group List]
        public DataTable GetAllGroups(short OrgID)
        {
            DataTable dTab = new DataTable("Groups");

            Admin_DALDataContext objDal = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("SecurityGroup_Search", Conn);
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

        #region[Delete Group(Mark IsActive = 0)]
        public int Group_Delete(long groupId, long userId)
        {
            return objDAL.Group_Delete(groupId, userId);
        }
        #endregion

        #region [Add Group Details]
        public long AddGroupDetails(SecurityGroup objGrp, short OrgID)
        {
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            Nullable<long> SecGroupId = null;
            objDAL.SecurityGroupInsert(ref SecGroupId, objGrp.GroupName, objGrp.GroupDesc, objGrp.AddedBy, objGrp.IsSystemDefault, OrgID);
            return SecGroupId.Value;
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

            Dictionary<string, string> grp = new Dictionary<string, string>();
            foreach (SecurityGroup item in query)
            {
                grp.Add("GName", item.GroupName);
                grp.Add("GDesc", item.GroupDesc);
            }
            return grp;
        }
        #endregion

        #region [Update Group Details]
        /// <summary>
        /// update security group by securityGroupId
        /// </summary>
        /// <param name="objSecurityGroup"></param>
        /// <returns></returns>
        public int UpdateGroup(SecurityGroup objSecurityGroup)
        {
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            int result = objDAL.SecurityGroupUpdate(objSecurityGroup.SecurityGroupId, objSecurityGroup.GroupName, objSecurityGroup.GroupDesc, objSecurityGroup.ModifiedBy);
            return result;

        }
        #endregion

        #region [Get Associated Rights with a group]
        /// <summary>
        /// GET ALL RIGHTS ASSOCIATED WITH A GROUP
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        public IEnumerable GetGroupWithRights(long GroupId)
        {
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            return objDAL.GetAssociatedRightsWithGroup(GroupId).AsEnumerable();
        }
        #endregion

        #region[Delete Right From A Group]
        public int DeleteRightFromGroup(SecurityGroupRight objSecGrpRight)
        {
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            int result = objDAL.DeleteUserOrRightFromGroup(2, objSecGrpRight.SecurityGroupRightId);
            return result;
        }
        #endregion

        #region [Add Right In Group]
        /// <summary>
        /// add a right in a particular group
        /// </summary>
        /// <param name="objSecGrpRight"></param>
        /// <returns></returns>
        public long AddRightInGroup(long groupId, long rightId)
        {
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDAL.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Insert_GroupRight", Conn);
                Cmd.Parameters.AddWithValue("@GroupId", groupId);
                Cmd.Parameters.AddWithValue("@RightId", rightId);
                Cmd.CommandType = CommandType.StoredProcedure;

                return (long)Cmd.ExecuteScalar();
            }
        }
        #endregion

        #region[Search Rights]
        public IEnumerable GetRightsName(string prefix)
        {
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            var result = (from p in objDAL.SecurityRights
                          orderby p.RightName
                          where p.RightName.StartsWith(prefix)
                          select p).AsEnumerable();
            return result;
        }
        #endregion

        #region[Get Active Groups not associated with an organisation]
        public DataTable OrgGroups_NotAssociated(String grpName, String grpDesc, short orgID)
        {
            DataTable dTab = new DataTable("OrgGrps");

            Admin_DALDataContext objDal = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("OrganisationGroups_NotAssociated", Conn);
                Cmd.Parameters.AddWithValue("@GroupName", grpName);
                Cmd.Parameters.AddWithValue("@GroupDesc", grpDesc);
                Cmd.Parameters.AddWithValue("@OrgID", orgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region[Add Group to Organisation]
        public Int32 AddGroupToOrg(long securityGroupId, short orgID)
        {
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            Int32 result = -1;
            using (SqlConnection Conn = new SqlConnection(objDAL.Connection.ConnectionString))
            {
                try
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();

                    SqlCommand Cmd = new SqlCommand("SecurityGroupOrganisation_Insert", Conn);
                    Cmd.Parameters.AddWithValue("@SecurityGroupID", securityGroupId);
                    Cmd.Parameters.AddWithValue("@OrganisationID", orgID);
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

        #region[Delete Group(IsActive = 0)]
        public void DeleteGroupFromOrg(long groupId, short orgId)
        {
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDAL.Connection.ConnectionString))
            {
                try
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();

                    SqlCommand Cmd = new SqlCommand("SecurityGroupOrganisation_Delete", Conn);
                    Cmd.Parameters.AddWithValue("@GroupId", groupId);
                    Cmd.Parameters.AddWithValue("@OrgId", orgId);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Object value = Cmd.ExecuteScalar();
                }
                catch(Exception e)
                {
                    
                }
            }
        }
        #endregion
    }
}
