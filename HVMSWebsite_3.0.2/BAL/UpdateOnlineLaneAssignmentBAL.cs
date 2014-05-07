using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION.DAL;
namespace METAOPTION.BAL
{
    public class UpdateOnlineLaneAssignmentBAL
    {
        UpdateOnlineLaneAssignmentDAL objOnline = new UpdateOnlineLaneAssignmentDAL();
        public long UpdateOnlineLane(Inventory obj)
        {
            return objOnline.UpdateOnlineLane(obj);
        }

    }
}
