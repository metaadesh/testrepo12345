using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION.DAL;
namespace METAOPTION.BAL
{
    public class GetMakeModelByInventoryIdBAL
    {
        public List<string> GetMake(Int64 inventoryId)
        {
            var result = GetMakeModelByInventoryIdDAL.GetMakeModelByInventory(inventoryId).AsQueryable();
            List<string> lstMake = new List<string>();
            foreach (GetMakeModelByInventoryIdResult objResult in result)
            {
                //Year at Index 0
                lstMake.Add(objResult.Year.ToString());
                //model at Index 1
                lstMake.Add(objResult.model.ToString());
                //Make at Index 2
                lstMake.Add(objResult.Make.ToString());
            }
            return lstMake;
        }
    }
}

