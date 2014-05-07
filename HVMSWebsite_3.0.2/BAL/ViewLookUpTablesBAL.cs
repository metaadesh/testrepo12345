using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using METAOPTION.DAL;
namespace METAOPTION.BAL
{
    public class ViewLookUpTablesBAL
    {
        ViewLookUpTablesDAL objDAL = new ViewLookUpTablesDAL();
        #region "Select All lookup tables"
        public IQueryable SelectLookUpTbales(bool IsReadWrite)
        {
            return objDAL.SelectLookUpTables(IsReadWrite);
        }
        #endregion

    }
}
