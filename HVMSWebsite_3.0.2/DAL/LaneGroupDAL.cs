using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using METAOPTION;
namespace METAOPTION.DAL
{
    public class LaneGroupDAL
    {
        DALDataContext objDAL = new DALDataContext();
        #region"[Select LaneAssignments Groups]"
        public DataTable GetLaneGroups()
        {
            DataTable dt = new DataTable();
            IQueryable<vwLaneAssignmentsGroup> result = (from p in objDAL.vwLaneAssignmentsGroups
                                                         orderby p.GroupName 
                                                         select p) as IQueryable<vwLaneAssignmentsGroup>;
            dt = ToDataTable(objDAL, result);
            
            return dt;
        }
        
        public DataTable ToDataTable(System.Data.Linq.DataContext ctx, object query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            IDbCommand cmd = ctx.GetCommand(query as IQueryable);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = (SqlCommand)cmd;
            DataTable dt = new DataTable("sd");

            try
            {
                cmd.Connection.Open();
                adapter.FillSchema(dt, SchemaType.Source);
                adapter.Fill(dt);
            }
            finally
            {
                cmd.Connection.Close();
            }
            return dt;
        }




        #endregion
    }
}
