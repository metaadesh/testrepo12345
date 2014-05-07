using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using METAOPTION.DAL;

namespace METAOPTION.BAL
{
    public class Admin_ViewGroupListBAL
    {
        Admin_ViewGroupListDAL objViewGroupListDAL = new Admin_ViewGroupListDAL();

        #region [Get All Group List]
        public IQueryable<SecurityGroup> GetAllGroups()
        {
            return objViewGroupListDAL.GetAllGroups();
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
            return objViewGroupListDAL.GetGroupDetails_ByGroupId(GroupId);

        }
        #endregion

        #region [Added by Rupendra 21 Nov 12 For ManagePermission page]
        public DataTable GetAllManagePermission(Int16 OrgID)
        {
            return objViewGroupListDAL.GetAllManagePermission(OrgID);
        }
        public DataTable GetManagePermissionByID(Int64 IPPermissionID, String SUID)
        {
            return objViewGroupListDAL.GetManagePermissionByID(IPPermissionID, SUID);
        }
        public Int32 SaveIPPermission(String IPAddress, int IPType, string Description, Int64 AddedBy, Int16 OrgID)
        {
            return objViewGroupListDAL.SaveIPPermission(IPAddress, IPType, Description, AddedBy, OrgID);
        }
        public Int32 UpdateIPPermission(Int64 IPPermissionID, String IPAddress, int IPType, string Description, Int64 ModifiedBy)
        {
            return objViewGroupListDAL.UpdateIPPermission(IPPermissionID, IPAddress, IPType, Description, ModifiedBy);
        }
        public Int32 DeleteIPPermission(Int64 IPPermissionID, Int32 IPType, Int64 DeletedBy, String UsersIDs)
          {
            return objViewGroupListDAL.DeleteIPPermission(IPPermissionID, IPType, DeletedBy, UsersIDs);
        }
        public Int32 DeleteIPRestriction(Int32 IPPermissionID, Int32 UserId, Int32 IPType, Int64 DeletedBy)
        {
            return objViewGroupListDAL.DeleteIPRestriction(IPPermissionID, UserId, IPType, DeletedBy);
        }
        public DataTable GetBindSearchGrid(string IPAddress, string IPType, string Description, string EntityId, Int32 EntityTypeId, string strSort,Int16 OrgID)
        {
            return objViewGroupListDAL.GetBindSearchGrid(IPAddress, IPType, Description, EntityId, EntityTypeId, strSort, OrgID);
        }

        public DataTable CheckDuplicateIPAddress(string ipAddress)
        {
            return objViewGroupListDAL.CheckDuplicateIPAddress(ipAddress);
        }
        public DataTable GetEntity(Int32 EntityTypeId,Int16 OrgID)
        {
            return objViewGroupListDAL.GetEntity(EntityTypeId, OrgID);
        }

        public Int32 AddUserPermissionIP(String IPAddress, int IPType, string Description, Int32 EntityTypeId, DataTable dtEntityId, Int64 AddedBy, Int16 OrgID)
        {
            return objViewGroupListDAL.AddUserPermissionIP(IPAddress, IPType, Description, EntityTypeId, dtEntityId, AddedBy,OrgID);
        }
        public Int32 UpdateUserPermissionIP(Int64 IPPermissionID, String IPAddress, int IPType, string Description, DataTable dt, Int64 AddedBy)
        {
            return objViewGroupListDAL.UpdateUserPermissionIP(IPPermissionID, IPAddress, IPType, Description, dt, AddedBy);
        }

        public DataTable IPPermissionEntityName(Int32 EntityTypeId, string IPAddress, Int32 IPPermissionID)
        {
            return objViewGroupListDAL.IPPermissionEntityName(EntityTypeId, IPAddress, IPPermissionID);
        }


        public Int32 AutoIPRestriction(Int32 IPUserId, Int32 ModifiedBY, Int32 IPType, int Flag)
        {
            return objViewGroupListDAL.AutoIPRestriction(IPUserId, ModifiedBY, IPType, Flag);
        }

        public Int32 AutoIPRestriction(Int32 IPUserId,  Int32 IPType, int Flag)
        {
            return objViewGroupListDAL.AutoIPRestriction(IPUserId,IPType, Flag);
        }

        public DataTable GetEntityIdbyIPAddredd(string IPAddress, Int32 IPType, short OrgID)
        {
            return objViewGroupListDAL.GetEntityIdbyIPAddredd(IPAddress, IPType, OrgID);
        }
        #endregion

        #region [Added by Rupendra 12 - 12 - 12 For Add and Display Setting Keys]
        public DataTable GetSettingKeysValue(String ProjectName)
        {
            return objViewGroupListDAL.GetSettingKeysValue(ProjectName);
        }

        public Int32 SettingsKeysInsert(String SettingKey, String SettingkeyValue, String Project, String Description)
        {
            return objViewGroupListDAL.SettingsKeysInsert(SettingKey, SettingkeyValue, Project, Description);
        }
        #endregion
    }
}
