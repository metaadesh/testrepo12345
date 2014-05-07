using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace METAOPTION
{
    public class WorkOrderBAL
    {
        WorkOrderDAL WOdal = new WorkOrderDAL();
        #region[Seach work order]
        public DataTable SearchWorkOrder(String VIN, long AddedBy, long Vendor, String WONumber, Int32 Status, DateTime SyncFrom, DateTime SyncTo, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, Int16 OrgID)
        {
            return WOdal.SearchWorkOrder(VIN, AddedBy, Vendor, WONumber, Status, SyncFrom, SyncTo, StartRowIndex, MaximumRows, SortExpression, OrgID);
        }
        #endregion

        #region[Search work order - count]
        public Int32 SearchWorkOrderCount(String VIN, long AddedBy, long Vendor, String WONumber, Int32 Status, DateTime SyncFrom, DateTime SyncTo, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, Int16 OrgID)
        {
            return WOdal.SearchWorkOrderCount(VIN, AddedBy, Vendor, WONumber, Status, SyncFrom, SyncTo, StartRowIndex, MaximumRows, SortExpression, OrgID);
        }
        #endregion

        #region[Work order added by users]
        public List<Mobile_WorkOrder_AddedByResult> GetWorkOrderUsers(Int16 OrgID)
        {
            return WOdal.GetWorkOrderUsers(OrgID);
        }
        #endregion

        #region[Work order added by vendors]
        public List<Vendor> GetWorkOrderVendors(Int16 OrgID)
        {
            return WOdal.GetWorkOrderVendors(OrgID);
        }
        #endregion

        #region[Work order sort options]
        public DataTable WorkOrder_SortOptions(String strQuery, String Sort1, String Sort2)
        {
            return WOdal.WorkOrder_SortOptions(strQuery, Sort1, Sort2);
        }
        #endregion

        #region[Work order expenses by WO ID]
        public IQueryable Expense_ByWOId(long WOId)
        {
            return WOdal.Expense_ByWOId(WOId).AsQueryable();
        }
        #endregion

        #region[Delete work order]
        public Int32 DeleteWorkOrder(long WOId, long UserID, String Reason)
        {
            return WOdal.DeleteWorkOrder(WOId, UserID, Reason);
        }
        #endregion

        #region[Work order expenses by WO ID]
        public IQueryable Entity_ByWOId(long WOId)
        {
            return WOdal.Entity_ByWOId(WOId);
        }

        public List<Mobile_FillWOExpensesResult> FillWorkOrderExpenses(long WOID)
        {
            return WOdal.FillWorkOrderExpenses(WOID);
        }


        #endregion

        #region[WO Images]
        public System.Collections.ArrayList WorkOrderImages(long WorkOrderID)
        {
            return WOdal.WorkOrderImages(WorkOrderID);
        }
        #endregion

        #region[Update WorkOrder]
        public Int32 UpdateWorkOrder(long WOId, Int32 Status, long ModifiedBy, DateTime ModifiedDate)
        {
            return WOdal.UpdateWorkOrder(WOId, Status, ModifiedBy, ModifiedDate);
        }
        #endregion

        #region[Get Email Details]
        public List<Mobile_GetEmailDetailResult> GetEmailDetail(Int32 Type, long WODetailID)
        {
            return WOdal.GetEmailDetail(Type, WODetailID);
        }
        #endregion
    }
}
