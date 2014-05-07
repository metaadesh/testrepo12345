using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION;
namespace METAOPTION.DAL
{
    public class UpdateOnlineLaneAssignmentDAL
    {
        DALDataContext objDAL = new DALDataContext();

        public long UpdateOnlineLane(Inventory obj)
        {
           
            objDAL.UpdateOnlineLane
                (Convert.ToInt32( obj.InventoryId), obj.OnlineLane);
            return obj.InventoryId;
        }
    }
    
}
