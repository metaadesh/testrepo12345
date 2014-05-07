using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION;
namespace METAOPTION.DAL
{
    public class UpdateLaneAssignmentsDAL
    {
        DALDataContext objDAL = new DALDataContext();

        public long UpdateLaneAssignments(Inventory obj)
        {

            objDAL.UpdateLaneAssignments
                (Convert.ToInt32(obj.InventoryId),obj.RegularLane,obj.ExoticLane,obj.VirtualLane,obj.OnlineLane,obj.MarketPrice,obj.IsExotic,obj.AddedBy);
            return obj.InventoryId;
        }
    }
}
