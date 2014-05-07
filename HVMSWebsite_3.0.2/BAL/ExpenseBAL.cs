using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION;
using METAOPTION.DAL;
using System.Data;
using System.Collections;

namespace METAOPTION
{
    public class ExpenseBAL
    {

        METAOPTION.DAL.ExpensesDAL objdal = new METAOPTION.DAL.ExpensesDAL();

        public System.Collections.ArrayList ExpenseImages(long ExpenseID)
        {
            ExpensesDAL dal = new ExpensesDAL();
            return dal.ExpenseImages(ExpenseID);
        }
        #region[Pre-Expense added by users]
        public List<Mobile_PreExp_AddedByResult> GetPreExpUsers()
        {
            return objdal.GetPreExpUsers();
        }
        #endregion
        #region[Expense list sort options]
        public DataTable Expense_SortOptions(String strQuery, String Sort1, String Sort2)
        {
            return objdal.Expense_SortOptions(strQuery, Sort1, Sort2);
        }
        #endregion

        public DataTable SearchExpense(String VIN, string CheckNo, string CheckPaid, long AddedBy, long EntityId, long EntityTypeId, long ExpenseTypeId, DateTime SyncFrom, DateTime SyncTo, string InvoiceNumber, string SourceFilter, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            return objdal.SearchExpense(VIN, CheckNo, CheckPaid, AddedBy, EntityId, EntityTypeId, ExpenseTypeId, SyncFrom, SyncTo, InvoiceNumber, SourceFilter, StartRowIndex, MaximumRows, SortExpression);//,CheckNumber,  CheckPaid
        }

        public Int32 SearchExpenseCount(String VIN, string CheckNo, string CheckPaid, long AddedBy, long EntityId, long EntityTypeId, long ExpenseTypeId, DateTime SyncFrom, DateTime SyncTo, string InvoiceNumber, string SourceFilter, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)//string CheckNumber, int CheckPaid, 
        {
            return objdal.SearchExpenseCount(VIN, CheckNo, CheckPaid, AddedBy, EntityId, EntityTypeId, ExpenseTypeId, SyncFrom, SyncTo, InvoiceNumber, SourceFilter, StartRowIndex, MaximumRows, SortExpression);
        }


        #region [Added by Rupendra 26 Dec 12, Vendor login details]
        public DataTable SearchExpense_Ver211(String VIN, string CheckNo, string CheckPaid, long AddedBy, long EntityId, long EntityTypeId, long ExpenseTypeId, DateTime SyncFrom, DateTime SyncTo, string InvoiceNumber, string SourceFilter, Int32 LoginEntityTypeID, Int32 UserEntityID, Int32 BuyerParentID, string BuyerIsDirect, Int32 BuyerAccessLevel, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, short orgID)
        {
            return objdal.SearchExpense_Ver211(VIN, CheckNo, CheckPaid, AddedBy, EntityId, EntityTypeId, ExpenseTypeId, SyncFrom, SyncTo, InvoiceNumber, SourceFilter, LoginEntityTypeID, UserEntityID, BuyerParentID, BuyerIsDirect, BuyerAccessLevel, StartRowIndex, MaximumRows, SortExpression, orgID);//,CheckNumber,  CheckPaid
        }

        public Int32 SearchExpenseCount_Ver211(String VIN, string CheckNo, string CheckPaid, long AddedBy, long EntityId, long EntityTypeId, long ExpenseTypeId, DateTime SyncFrom, DateTime SyncTo, string InvoiceNumber, string SourceFilter, Int32 LoginEntityTypeID, Int32 UserEntityID, Int32 BuyerParentID, string BuyerIsDirect, Int32 BuyerAccessLevel, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, short orgID)//string CheckNumber, int CheckPaid, 
        {
            return objdal.SearchExpenseCount_Ver211(VIN, CheckNo, CheckPaid, AddedBy, EntityId, EntityTypeId, ExpenseTypeId, SyncFrom, SyncTo, InvoiceNumber, SourceFilter, LoginEntityTypeID, UserEntityID, BuyerParentID, BuyerIsDirect, BuyerAccessLevel, StartRowIndex, MaximumRows, SortExpression, orgID);
        }
        #endregion

        #region[PreExpense ID By Expense ID]
        public long PreExp_ByExpID(long ExpID)
        {
            return objdal.PreExp_ByExpID(ExpID);
        }
        #endregion

        #region [Get PreExpense Detail by PreExpenseId]
        public IEnumerable PreExpenseDetail_ByPreExpenseId(long PreExpenseId)
        {
            return objdal.PreExpenseDetail_ByPreExpenseId(PreExpenseId);
        }
        #endregion

        #region [ Add by Vipin 17 jan 2013 Get Expense type agianst SecurityUserID]
        public List<ExpenseType> GetExpenseType(Int32 EntityID,Int32 ParentEntityID, Int32 EntityTypeId)
        {

            return objdal.GetExpenseType(EntityID, ParentEntityID, EntityTypeId);
        }

        #endregion

        #region [Added by Vipin 17 jan 2013 Get Expense type agianst SecurityUserID]
        public List<SecurityUser> GetAddedByUser(Int32 EntityID, Int32 ParentEntityID, Int32 EntityTypeID)
        {
            return objdal.GetAddedByUser(EntityID, ParentEntityID, EntityTypeID);
        }
        #endregion

        public Int32 UpdateInvoiceNumber(int ExpenseId, string InvoiceNumber)
        {
            return objdal.UpdateInvoiceNumber(ExpenseId, InvoiceNumber);
        }
        public Int32 DeleteExpense(int ExpenseId, int UserId)
        {
            return objdal.DeleteExpense(ExpenseId, UserId);
        }

        public DataTable GetVIN(string VIN, Int16 OrgID)
        {

            DataTable dt = new DataTable();
            dt = objdal.GetVIN(VIN, OrgID);
            return dt;
        }

        public DataTable GetEntities(int EntityTypeId, short orgID)
        {
            DataTable dt = new DataTable();
            dt = objdal.GetEntities(EntityTypeId, orgID);
            return dt;
        }

        public DataSet GetEntityNameExpenseAddedBy(Int16 OrgID)
        {
            DataSet ds = new DataSet();
            ds = objdal.GetEntityNameExpenseAddedBy(OrgID);
            return ds;
        }

        public DataSet GetEntityNameExpenseAddedBy_Ver211(Int32 EntityTypeId, Int32 SecurityUserId)
        {
            DataSet ds = new DataSet();
            ds = objdal.GetEntityNameExpenseAddedBy_Ver211(EntityTypeId, SecurityUserId);
            return ds;
        }

        public DataTable GetEntities_Ver211(Int32 EntityTypeId, Int32 EntityUserId,Int32 BuyerParentId)
        {
            DataTable dt = new DataTable();
            dt = objdal.GetEntities_Ver211(EntityTypeId, EntityUserId,BuyerParentId);
            return dt;
        }

        public DataTable GetDirectBuyerEntities_Ver211(Int32 EntityUserId)
        {
            DataTable dt = new DataTable();
            dt = objdal.GetDirectBuyerEntities_Ver211(EntityUserId);
            return dt;
        }

        public DataTable ChildBuyerEntities_Ver211(Int32 ChildUserId, Int32 ParenBuyerId)
        {
            DataTable dt = new DataTable();
            dt = objdal.ChildBuyerEntities_Ver211(ChildUserId,ParenBuyerId);
            return dt;
        }

        public DataTable GetMobileExpenseDetails(int ExpenseID)
        {
            DataTable dt = new DataTable();
            dt = objdal.GetMobileExpenseDetailsbyExpenseId(ExpenseID);
            return dt;
        }

        public int UpdateHistoryTable(int ExpenseID, int UserId, decimal ExpenseAmount, int ExpenseType, string ExpenseDate, string Commnets, decimal ExpenseAmountNew, int ExpenseTypeNew, string ExpenseDateNew, string CommnetsNew, string invoiceNo, string invoiceNoNew, string Source)
        {
            int ret;
            ret = objdal.UpdateHistoryTable(ExpenseID, UserId, ExpenseAmount, ExpenseType, ExpenseDate, Commnets, ExpenseAmountNew, ExpenseTypeNew, ExpenseDateNew, CommnetsNew, invoiceNo, invoiceNoNew, Source);
            return ret;
        }

        public int UpdateInventoryCost(int InventoryID, decimal? Carcost)
        {
            int ret;
            ret = objdal.UpdateInventoryCost(InventoryID, Carcost);
            return ret;
        }

        public List<GetMobExpDetailsbyExpenseIdResult> PreExpenseDetailBy_PreExpenseId(long PreExpenseID)
        {
            return objdal.PreExpenseDetailByPreExpenseId(PreExpenseID);
        }
    }
}
