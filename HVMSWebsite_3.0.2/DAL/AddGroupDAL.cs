using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace METAOPTION.DAL
{
   public class AddGroupDAL
   {      
      #region [Add Group Details]
      public long AddGroupDetails(SecurityGroup objSecurityGroup, short OrgID)
      {
         DALDataContext objDAL = new DALDataContext();
         Nullable<long> SecGroupId = null;
         objDAL.SecurityGroupInsert(ref SecGroupId, objSecurityGroup.GroupName, objSecurityGroup.GroupDesc, objSecurityGroup.AddedBy, objSecurityGroup.IsSystemDefault, OrgID);
         return SecGroupId.Value;
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
         DALDataContext objDAL = new DALDataContext();
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
         DALDataContext objDAL = new DALDataContext();
         return objDAL.GetAssociatedRightsWithGroup(GroupId).AsEnumerable();
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
         DALDataContext objDAL = new DALDataContext();
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

      #region[Delete Right From A Group]
      public int DeleteRightFromGroup(SecurityGroupRight objSecGrpRight)
      {
         DALDataContext objDAL = new DALDataContext();
         int result = objDAL.DeleteUserOrRightFromGroup(2, objSecGrpRight.SecurityGroupRightId);
         return result;
      }
      #endregion

      #region[Search Rights]
      public IEnumerable GetRightsName(string prefix)
      {
         DALDataContext objDAL = new DALDataContext();
         var result = (from p in objDAL.SecurityRights
                       orderby p.RightName
                       where p.RightName.StartsWith(prefix)
                       select p).AsEnumerable();
         return result;
      }
      #endregion

      #region[ Delete Group - (Mark IsActive = 0 ) ]
      public int Group_Delete(long groupId, long userId)
      {
         DALDataContext objDAL = new DALDataContext();
         return objDAL.Group_Delete(groupId, userId);
      }
      #endregion
      #region [ Search Group ]
      /// <summary>
      ///  Search Group By Name & Description
      /// </summary>
      /// <param name="GroupName">Group Name</param>
      /// <param name="Description">Description</param>
      /// <returns></returns>
      public IEnumerable SearchGroups(String GroupName, String Description)
      {
         DALDataContext objDal= new DALDataContext();
         var result = (from grp in objDal.SecurityGroups
                       where grp.IsActive == 1
                       && grp.GroupName.StartsWith(GroupName)
                       && grp.GroupDesc.StartsWith(Description)
                       select grp).Distinct().AsEnumerable();
         return result;
      }
      #endregion
   }
}
