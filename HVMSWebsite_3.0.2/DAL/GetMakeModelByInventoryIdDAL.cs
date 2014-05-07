using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION;
namespace METAOPTION.DAL
{
    public class GetMakeModelByInventoryIdDAL
    {
        static DALDataContext objDAL = new DALDataContext();
        public static IQueryable GetMakeModelByInventory(Int64 inventoryId)
        {
            IQueryable result = objDAL.GetMakeModelByInventoryId(inventoryId).AsQueryable();
            return result;
        }
    }
}
