using System;
using System.Data;
using System.Data.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION;
using METAOPTION.DAL;

namespace METAOPTION.BAL
{
  public class AddGroupBAL
    {
        AddGroupDAL objAddGroupDAL = new AddGroupDAL();

        #region [Add Group Details]
        public long AddGroupDetails(SecurityGroup objSecurityGroup, short OrgID)
        {
            return objAddGroupDAL.AddGroupDetails(objSecurityGroup, OrgID);
        }
        #endregion

        #region [Update Group Details]
        public int UpdateGroup(SecurityGroup objSecurityGroup)
        {
            return objAddGroupDAL.UpdateGroup(objSecurityGroup);
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
           return objAddGroupDAL.GetGroupWithRights(GroupId);
        }
        #endregion

        #region[Delete Right From A Group]
        public int DeleteRightFromGroup(SecurityGroupRight objSecGrpRight)
        {
            return objAddGroupDAL.DeleteRightFromGroup(objSecGrpRight);
        }
        #endregion

        #region[Get Rights Name & RightId according to prefix Character]
        public static IEnumerable GetRightsName(string prefix)
        {
           AddGroupDAL objAddGrp = new AddGroupDAL();
           return objAddGrp.GetRightsName(prefix);
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
           AddGroupDAL objDAL = new AddGroupDAL();
           return objDAL.AddRightInGroup(groupId, rightId);
        }
        #endregion

        #region[ Delete Group - (Mark IsActive = 0 ) ]
        /// <summary>
        /// Delete Group - (Mark IsActive = 0 )
        /// </summary>
        /// <param name="groupId">Group Id</param>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        public static int Group_Delete(long groupId, long userId)
        {
           AddGroupDAL objDAL = new AddGroupDAL();
           return objDAL.Group_Delete(groupId, userId);
        }
        #endregion

        
    }
}
