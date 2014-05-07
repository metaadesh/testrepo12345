using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using METAOPTION;
namespace METAOPTION.DAL
{
    public class ViewInventoryNotesDAL
    {
        DALDataContext objDAL = new DALDataContext();
        DataTable dt = new DataTable();
        #region[Get Inventory Notes List]
        /// <summary>
        /// Get Inventory Notes List
        /// </summary>
        /// <param name="Inventoryid">to pass Inventoryid</param>
        /// <returns>Data Table</returns>
        //public IQueryable SelectInventoryNotes(Int64 Inventoryid)
        //{
        //    IQueryable query = objDAL.GetInventoryNotes(Inventoryid).AsQueryable();
        //    //dt = ToDataTable(objDAL, query);
        //    return query;
        //}
        #endregion
        /// <summary>
        /// Created custom DataTable
        /// </summary>
        /// <param name="ctx">DataContext object</param>
        /// <param name="query"> to pass SQL query</param>
        /// <returns>DataTable</returns>
        public DataTable ToDataTable(System.Data.Linq.DataContext ctx, object query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            IDbCommand cmd = ctx.GetCommand(query as IQueryable);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = (SqlCommand)cmd;
            DataTable Localdt = new DataTable("sd");

            try
            {
               if( cmd.Connection.State.ToString()!="Open")
                cmd.Connection.Open();
                adapter.FillSchema(Localdt, SchemaType.Source);
                adapter.Fill(Localdt);
            }
            finally
            {
                cmd.Connection.Close();
            }
            return Localdt;
        }



        public Dictionary<string, bool> Select(long? inventoryId)
        {
            Dictionary<string, bool> objDictionaryCarProp = new Dictionary<string, bool>();

            //var result= objDAL.CarPropertySelectBool(inventoryId).ToList();
            //foreach(CarPropertySelectBoolResult item in result)
            //{
            //    objDictionaryCarProp.Add("item.Leather", item.Leather.Value);
            //    objDictionaryCarProp.Add("item.Navigation", item.Navigation.Value);
            //    objDictionaryCarProp.Add("item.PowerLocks", item.PowerLocks.Value);
            //    objDictionaryCarProp.Add("item.PowerSeat", item.PowerSeat.Value);
            //    objDictionaryCarProp.Add("item.PowerWindows", item.PowerWindows.Value);
            //    objDictionaryCarProp.Add("item.SunMoon", item.SunMoon.Value);
            //    objDictionaryCarProp.Add("item.AC", item.AC.Value);
            //    objDictionaryCarProp.Add("item.AlloyWheels", item.AlloyWheels.Value);

            //}

            return objDictionaryCarProp;
        }
    }
}
