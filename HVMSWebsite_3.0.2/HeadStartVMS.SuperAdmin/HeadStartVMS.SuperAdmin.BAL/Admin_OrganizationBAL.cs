using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using METAOPTION.DAL;
using System.Collections;

namespace METAOPTION.BAL
{
    public class Admin_OrganizationBAL
    {
        Admin_OrganizationDAL objDAL = new Admin_OrganizationDAL();

        public IQueryable<Object> GetOrganizationsList()
        {
            METAOPTION.DAL.Admin_Common obj = new METAOPTION.DAL.Admin_Common();
            return objDAL.GetOrganizationsList();
        }

        #region[Show Organization]
        public DataTable ShowOrganization(string OrgName, string Code, String Phone, String Website, String Email)
        {
            return objDAL.ShowOrganization(OrgName, Code, Phone, Website, Email);
        }
        #endregion

        #region[Fill the Entity Type]

        public List<EntityType> GetRealEntityType_List()
        {
            return objDAL.GetRealEntityType_List();
        }
        public DataTable GetEntityType()
        {
            return objDAL.GetEntityType();
        }
        #endregion

        #region[Populate Organization DropDownList]
        public DataTable ddl_GetOrganization()
        {
            return objDAL.ddl_GetOrganization();
        }
        #endregion

        #region[Bind Users]
        public DataTable GetEmployee(int EntityTypeID, Int16 OrgID, Int64 SUID, Int32 IsActive)
        {
            return objDAL.GetEmployee(EntityTypeID, OrgID, SUID, IsActive);
        }
        public DataTable GetEmployee(int EntityTypeID, Int16 OrgID, Int32 IsActive)
        {
            return objDAL.GetEmployee(EntityTypeID, OrgID, IsActive);
        }
        #endregion

        #region[Update Password]
        public int UpdatePasswd(Int64 securityuserid, string password, Int64 modifiedby)
        {
            return objDAL.UpdatePasswd(securityuserid, password, modifiedby);
        }
        #endregion

        #region[Change and Update Image at SearchOrganization page]
        public void Manage_Image(Int16 OrgID, Int16 status)
        {
            objDAL.Manage_Image(OrgID, status);
        }
        #endregion

        #region[Delete Organization  at SearchOrganization page]
        public void Delete_Organization(Int16 OrgID, Int16 status, Int64 loginID)
        {
            objDAL.Delete_Organization(OrgID, status, loginID);
        }
        #endregion

        #region Get Master Page Left Panel Values
        public Hashtable Admin_GetLeftPanelValue()
        {
            return objDAL.Admin_GetLeftPanelValue();
        }
        #endregion

        #region[Add new Organization]
        public Int16 AddNewOrganization(string Organization1, string Orgcode, string Website, string Address, string Phone, string Fax, string Mail, Int64 Addedby, Int16 Isactive, String status, Boolean AllowLaneAutomation, Boolean AllowMAA, String Password)
        {
            return objDAL.AddNewOrganization(Organization1, Orgcode, Website, Address, Phone, Fax, Mail, Addedby, Isactive, status, AllowLaneAutomation, AllowMAA, Password);
        }
        #endregion

        #region[Get OrganizationName and OrganizationCode with respect to OrgID]

        public DataTable getorginfo(Int16 OrganizationID)
        {
            return objDAL.getorginfo(OrganizationID);
        }
        #endregion

        #region[Get Organization Summary DataTable]
        public DataTable OrganizationSummary(Int16 OrgID)
        {
            return objDAL.OrganizationSummary(OrgID);
        }
        #endregion

        #region Allow/Deny Lane Automation & MAA Status
        public Int32 AllowDeny_Lane_MAA(Int16 OrgID, Boolean NewStatus, String Type)
        {
            return objDAL.AllowDeny_Lane_MAA(OrgID, NewStatus, Type);
        }
        #endregion

        #region[Get the Organization information w r to Entity ID]

        public Int16? GetEntiyInformation(Int64 entityid)
        {
            return objDAL.GetEntiyInformation(entityid);
        }

        #endregion

    }
}
