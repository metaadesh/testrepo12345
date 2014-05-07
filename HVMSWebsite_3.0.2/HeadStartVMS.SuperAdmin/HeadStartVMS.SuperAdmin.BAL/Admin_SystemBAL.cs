using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION.DAL;
using System.Data;

namespace METAOPTION.BAL
{
    public class Admin_SystemBAL
    {
        Admin_SystemDAL objDAL = new Admin_SystemDAL();

        public Int32 AddNewSystem(Int16 OrgID, String Description, String ImagePath, Boolean IsActive, Boolean PeachTree)
        {
            return objDAL.AddNewSystem(OrgID, Description, ImagePath, IsActive, PeachTree);
        }

        public DataTable SystemSearch(Int16 OrgID, String SystemName, Int32 ActiveStatus)
        {
            return objDAL.SystemSearch(OrgID, SystemName, ActiveStatus);
        }

        public DataTable GetSystemDetails(Int32 SystemID)
        {
            return objDAL.GetSystemDetails(SystemID);
        }

        public Int32 UpdateSystem(String SystemName, String ImagePath, Boolean IsActive, Boolean PeachTree, Int32 SystemID)
        {
            return objDAL.UpdateSystem(SystemName, ImagePath, IsActive, PeachTree, SystemID);
        }

        public Int32 UpdateSystemImagePath(Int32 SystemID, String ImagePath)
        {
            return objDAL.UpdateSystemImagePath(SystemID, ImagePath);
        }

        public Int32 UpdateSystemActiveStatus(Int32 SystemID, Boolean IsActive)
        {
            return objDAL.UpdateSystemActiveStatus(SystemID, IsActive);
        }


    }
}
