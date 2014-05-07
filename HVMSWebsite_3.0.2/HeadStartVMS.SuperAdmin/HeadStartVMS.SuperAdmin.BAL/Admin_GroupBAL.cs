using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION.DAL;
using System.Collections;
using System.Data;

namespace METAOPTION.BAL
{
    public class Admin_GroupBAL
    {
        Admin_GroupDAL objDAL = new Admin_GroupDAL();

        #region [Get All Group List]
        public DataTable GetAllGroups(short OrgID)
        {
            return objDAL.GetAllGroups(OrgID);
        }
        #endregion

        #region[Delete Group (Mark IsActive = 0)]
        public static int Group_Delete(long groupId, long userId)
        {
            Admin_GroupDAL objDal = new Admin_GroupDAL();
            return objDal.Group_Delete(groupId, userId);
        }
        #endregion

        #region [Add Group Details]
        public long AddGroupDetails(SecurityGroup objGrp, short OrgID)
        {
            return objDAL.AddGroupDetails(objGrp, OrgID);
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
            return objDAL.GetGroupDetails_ByGroupId(GroupId);

        }
        #endregion

        #region [Update Group Details]
        public int UpdateGroup(SecurityGroup objSecurityGroup)
        {
            return objDAL.UpdateGroup(objSecurityGroup);
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
            return objDAL.GetGroupWithRights(GroupId);
        }
        #endregion

        #region[Delete Right From A Group]
        public int DeleteRightFromGroup(SecurityGroupRight objSecGrpRight)
        {
            return objDAL.DeleteRightFromGroup(objSecGrpRight);
        }
        #endregion

        #region [Add Right In Group]
        /// <summary>
        /// add a right in a particular group
        /// </summary>
        /// <param name="objSecGrpRight"></param>
        /// <returns></returns>
        public static long AddRightInGroup(long groupId, long rightId)
        {
            Admin_GroupDAL objDAL = new Admin_GroupDAL();
            return objDAL.AddRightInGroup(groupId, rightId);
        }
        #endregion

        #region[Get Rights Name & RightId according to prefix Character]
        public static IEnumerable GetRightsName(string prefix)
        {
            Admin_GroupDAL obj = new Admin_GroupDAL();
            return obj.GetRightsName(prefix);
        }
        #endregion

        #region[Get Active Groups not associated with an Organization]
        public DataTable OrgGroups_NotAssociated(String grpName, String grpDesc, short orgID)
        {
            return objDAL.OrgGroups_NotAssociated(grpName, grpDesc, orgID);
        }
        #endregion

        #region[Add Group to Organization]
        public Int32 AddGroupToOrg(long securityGroupId, short orgID)
        {
            return objDAL.AddGroupToOrg(securityGroupId, orgID);
        }
        #endregion

        #region[Delete Group(IsActive = 0)]
        public void DeleteGroupFromOrg(long groupId, short orgId)
        {
            objDAL.DeleteGroupFromOrg(groupId, orgId);
        }
        #endregion
    }
}
