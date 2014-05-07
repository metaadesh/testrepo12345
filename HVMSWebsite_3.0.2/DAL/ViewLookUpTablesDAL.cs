using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Data.Linq;
using System.Xml.Linq;
using METAOPTION;
namespace METAOPTION.DAL
{
    public class ViewLookUpTablesDAL
    {
        DALDataContext objDAL = new DALDataContext();
        #region[View Look Up Tables]
       /// <summary>
       /// Select All Lookup Tables
       /// </summary>
       /// <returns></returns>
        public IQueryable SelectLookUpTables(bool IsReadWrite)
        {
            return objDAL.SelectAllLookupTables(IsReadWrite).AsQueryable();       
        }
        #endregion
    }
    
   
}
