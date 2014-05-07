using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace METAOPTION
{
    public class WorkOrderDAL
    {
        #region[Seach work order]
        public DataTable SearchWorkOrder(String VIN, long AddedBy, long Vendor, String WONumber, Int32 Status, DateTime SyncFrom, DateTime SyncTo, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, Int16 OrgID)
        {
            DataTable dTab = new DataTable("WorkOrder");

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_WorkOrder_Search", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@Vendor", Vendor);
                Cmd.Parameters.AddWithValue("@WONumber", WONumber);
                Cmd.Parameters.AddWithValue("@Status", Status);
                Cmd.Parameters.AddWithValue("@SyncDateFrom", SyncFrom);
                Cmd.Parameters.AddWithValue("@SyncDateTo", SyncTo);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@SortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region[Search work order - count]
        public Int32 SearchWorkOrderCount(String VIN, long AddedBy, long Vendor, String WONumber, Int32 Status, DateTime SyncFrom, DateTime SyncTo, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, Int16 OrgID)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("Mobile_WorkOrder_SearchCount", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@Vendor", Vendor);
                Cmd.Parameters.AddWithValue("@WONumber", WONumber);
                Cmd.Parameters.AddWithValue("@Status", Status);
                Cmd.Parameters.AddWithValue("@SyncDateFrom", SyncFrom);
                Cmd.Parameters.AddWithValue("@SyncDateTo", SyncTo);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@SortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }
        #endregion

        #region[Work order added by users]
        public List<Mobile_WorkOrder_AddedByResult> GetWorkOrderUsers(Int16 OrgID)
        {
            DALDataContext objDal = new DALDataContext();
            List<Mobile_WorkOrder_AddedByResult> result = objDal.Mobile_WorkOrder_AddedBy(OrgID).ToList<Mobile_WorkOrder_AddedByResult>();
            return result;
        }
        #endregion

        #region[Work order added by vendors]
        public List<Vendor> GetWorkOrderVendors(Int16 OrgID)
        {
            DALDataContext objDal = new DALDataContext();
            List<Vendor> result = (from p in objDal.Vendors
                                   from work in objDal.Mobile_WorkOrders
                                   where p.VendorId == work.EntityId && p.IsActive == 1 && p.OrgID==OrgID
                                   select p).Distinct().OrderBy(p => p.VendorName).ToList<Vendor>();
            return result;
        }
        #endregion

        #region[Work order sort options]
        public DataTable WorkOrder_SortOptions(String strQuery, String Sort1, String Sort2)
        {
            DataTable dTab = new DataTable("SortWorkOrder");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = Conn.CreateCommand();
                Cmd.CommandText = strQuery;
                Cmd.CommandType = CommandType.Text;
                SqlDataReader dReader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);

                dTab.Load(dReader);
            }
            return dTab;
        }
        #endregion

        #region[Work order expenses by WO ID]
        public IQueryable Expense_ByWOId(long WOId)
        {
            return METAOPTION.DALDataContext.Factory.DB.Mobile_WorkOrderExpensesDetails(WOId).AsQueryable();
        }
        #endregion

        #region[Delete work order]
        public Int32 DeleteWorkOrder(long WOId, long UserID, String Reason)
        {
            return METAOPTION.DALDataContext.Factory.DB.Mobile_WorkOrder_Delete(WOId, UserID, Reason);
            //int i = METAOPTION.DALDataContext.Factory.DB.Mobile_WorkOrder_Delete(PreInventoryID, UserID);
        }
        #endregion

        #region[Work order expenses by WO ID]
        public IQueryable Entity_ByWOId(long WOId)
        {
            return METAOPTION.DALDataContext.Factory.DB.Mobile_WorkOrder_SelectEntity(WOId).AsQueryable();
        }

        public List<Mobile_FillWOExpensesResult> FillWorkOrderExpenses(long WOID)
        {
            return METAOPTION.DALDataContext.Factory.DB.Mobile_FillWOExpenses(WOID).ToList<Mobile_FillWOExpensesResult>();
        }
        #endregion

        #region[WO Images]
        public System.Collections.ArrayList WorkOrderImages(long WorkOrderID)
        {
            DataTable dTab = new DataTable();
            System.Collections.ArrayList array = new System.Collections.ArrayList();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_WOImagesPath", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@WOId", WorkOrderID);

                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                array.Add(dTab);

                dTab = new DataTable();
                dTab.Load(reader);
                array.Add(dTab);

                reader.Close();
                objDal.Dispose();
            }
            return array;
        }
        #endregion

        #region[Update WorkOrder]
        public Int32 UpdateWorkOrder(long WOId, Int32 Status, long ModifiedBy, DateTime ModifiedDate)
        {
            return METAOPTION.DALDataContext.Factory.DB.Mobile_UpdateWOStatusFromWeb(WOId, Status, ModifiedDate, ModifiedBy);
        }
        #endregion

        #region[Get Email Details]
        public List<Mobile_GetEmailDetailResult> GetEmailDetail(Int32 Type, long WODetailID)
        {
            DALDataContext objDAL = new DALDataContext();
            return objDAL.Mobile_GetEmailDetail(Type, WODetailID).ToList<Mobile_GetEmailDetailResult>();
        }
        #endregion
    }
}
