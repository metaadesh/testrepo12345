using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web;
using System.Xml.Linq;
using METAOPTION;
using METAOPTION.DAL;
using System.Dynamic;
using System.Collections;

namespace METAOPTION.BAL
{
    public class PreExpenseBAL
    {
        METAOPTION.DAL.PreExpenseDAL objdal = new METAOPTION.DAL.PreExpenseDAL();

        public List<ExpenseType> GetAllExpenseType()
        {
            return objdal.GetAllExpenseType();
        }

        //public void Mobile_EntityExpenseInsert(Mobile_EntityExpense MEE)
        //{
        //    objdal.Mobile_EntityExpenseInsert(MEE);
        //}

        public List<Mobile_GetAllExpensesResult> GetAllExpenseTypes(Int32 EntityTypeID, Int64 EntityID)
        {
            return objdal.GetAllExpenseTypes(EntityTypeID, EntityID);
        }

        public void Insert_EntityExpenses(Int64 AddedBy, DataTable dtExpenses)
        {
            objdal.Insert_EntityExpenses(AddedBy, dtExpenses);
        }

        public DataTable SearchPreExpense(String VIN, long AddedBy, long Vendor, Int32 Status, DateTime SyncFrom, DateTime SyncTo, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            return objdal.SearchPreExpense(VIN, AddedBy, Vendor, Status, SyncFrom, SyncTo, StartRowIndex, MaximumRows, SortExpression);
        }

        public Int32 SearchPreExpenseCount(String VIN, long AddedBy, long Vendor, Int32 Status, DateTime SyncFrom, DateTime SyncTo, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            return objdal.SearchPreExpenseCount(VIN, AddedBy, Vendor, Status, SyncFrom, SyncTo, StartRowIndex, MaximumRows, SortExpression);
        }

        #region [Added by Rupendra on 24 Dec 12 for Vendor, Dealer and Buyer login details]
        public DataTable SearchPreExpense_Ver211(String VIN, long AddedBy, long Vendor, Int32 Status, DateTime SyncFrom, DateTime SyncTo, Int32 EntityTypeID, Int32 EntityID, Int32 ParentEntityID, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, Int16 OrgID)
        {
            return objdal.SearchPreExpense_Ver211(VIN, AddedBy, Vendor, Status, SyncFrom, SyncTo, EntityTypeID, EntityID, ParentEntityID, StartRowIndex, MaximumRows, SortExpression, OrgID);
        }

        public Int32 SearchPreExpenseCount_Ver211(String VIN, long AddedBy, long Vendor, Int32 Status, DateTime SyncFrom, DateTime SyncTo, Int32 EntityTypeID, Int32 EntityID, Int32 ParentEntityID, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, Int16 OrgID)
        {
            return objdal.SearchPreExpenseCount_Ver211(VIN, AddedBy, Vendor, Status, SyncFrom, SyncTo, EntityTypeID, EntityID, ParentEntityID, StartRowIndex, MaximumRows, SortExpression, OrgID);
        }

        #endregion
        public void ApproveExpense(Int64 AddedBy, Int32 Status, DataTable dtExpenses, String ApprovalNote)
        {
            objdal.ApproveExpense(AddedBy, Status, dtExpenses, ApprovalNote);
        }

        public void RejectPreExpense(Int64 PreExpID, Int64 ModifiedBy, Int32 Status, String Reason)
        {
            objdal.RejectPreExpense(PreExpID, ModifiedBy, Status, Reason);
        }

        public void DeletePreExpense(Int64 PreExpenseID, Int64 UserID, Int32 Status, String Reason)
        {
            objdal.DeletePreExpense(PreExpenseID, UserID, Status, Reason);
        }

        public void PreExpense_MakePending(Int64 PreExpenseID, Int64 UserID)
        {
            objdal.PreExpense_MakePending(PreExpenseID, UserID);
        }

        public DataTable SearchEntityExpenses(Int64 EntityId, Int32 EntityTypeID, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            return objdal.SearchEntityExpenses(EntityId, EntityTypeID, StartRowIndex, MaximumRows, SortExpression);
        }

        public Int32 SearchEntityExpensesCount(Int64 EntityId, Int32 EntityTypeID, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            return objdal.SearchEntityExpensesCount(EntityId, EntityTypeID, StartRowIndex, MaximumRows, SortExpression);
        }

        public List<Mobile_ExpensesOfEntityResult> GetEntityExpenses(Int32 EntityTypeID, long EntityID)
        {
            return objdal.GetEntityExpenses(EntityTypeID, EntityID);
        }

        public List<Mobile_GetEntityExpensesByIDResult> GetEntityExpenseByID(Int64 EntityExpenseID)
        {
            return objdal.GetEntityExpenseByID(EntityExpenseID);
        }

        public Int32 UpdateEntityExpense(Int64 EntityExpenseID, Int32 ExpenseTypeId, Int32 MinCount, Int32 MaxCount, Decimal DefaultpPrice, Int64 ModifiedBy)
        {
            return objdal.UpdateEntityExpense(EntityExpenseID, ExpenseTypeId, MinCount, MaxCount, DefaultpPrice, ModifiedBy);
        }

        public void DeleteEntityExpense(Int64 EntityExpenseID, Int64 DeletedBy)
        {
            objdal.DeleteEntityExpense(EntityExpenseID, DeletedBy);
        }

        public List<Contact> GetContactDetail(long ContactID)
        {
            return objdal.GetContactDetail(ContactID);
        }

        #region[Images]
        public System.Collections.ArrayList PreExpenseImages(string PreExpenseID)
        {
            PreExpenseDAL objPreExpenseDAL = new PreExpenseDAL();
            return objPreExpenseDAL.PreExpenseImages(PreExpenseID);
        }


        /// <summary>
        /// //Modified by Rupendra 6 Sep 12
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public System.Collections.ArrayList PreExpenseImagesByInvId(string VIN)
        {
            PreExpenseDAL objPreExpenseDAL = new PreExpenseDAL();
            return objPreExpenseDAL.PreExpenseImagesByInvId(VIN);
        }
        #endregion

        public List<Mobile_GetInvforExpensesResult> GetInvForExpenses(String VIN)
        {
            return objdal.GetInvForExpenses(VIN);
        }

        #region [Get PreExpense Detail by PreExpenseId]
        public IEnumerable PreExpenseDetail_ByPreExpenseId(long PreExpenseId)
        {
            return objdal.PreExpenseDetail_ByPreExpenseId(PreExpenseId);
        }
        #endregion

        #region[Duplicate expense]
        public DataTable GetDuplicateExpense(String VIN, Int32 ExpenseTypeID, Decimal Amount, Int32 EntityID, Int32 EntityTypeID, Int32 Period)
        {
            return objdal.GetDuplicateExpense(VIN, ExpenseTypeID, Amount, EntityID, EntityTypeID, Period);
        }
        #endregion

        #region[Duplicate pre-expense]
        public DataTable GetDuplicatePreExpense(String VIN, Int32 ExpenseTypeID, Decimal Amount, Int32 EntityID, Int32 EntityTypeID, Int32 Period)
        {
            return objdal.GetDuplicatePreExpense(VIN, ExpenseTypeID, Amount, EntityID, EntityTypeID, Period);
        }
        #endregion

        #region[Pre-Expense added by users]
        public List<Mobile_PreExp_AddedByResult> GetPreExpUsers()
        {
            return objdal.GetPreExpUsers();
        }
        #endregion
        #region[Pre-Expense added by users]
        public List<SecurityUser> GetPreExpUsers_AddedBy(Int32 EntityID, Int32 ParentEntityID, Int32 EntityTypeID, Int16 OrgID)
        {
            return objdal.GetPreExpUsersByAddedBy(EntityID, ParentEntityID, EntityTypeID,OrgID);
        }
        #endregion

        #region[Pre-Expense added by vendors]
        public List<Vendor> GetPreExpVendors(Int16 OrgID)
        {
            return objdal.GetPreExpVendors(OrgID);
        }
        #endregion

        #region[Pre-Expense added by vendors For Vendor List]
        public List<Vendor> GetPreExpVendors_AddedVendor(Int32 AddedBy, Int16 OrgID)
        {
            return objdal.GetPreExpVendors_AddedVendor(AddedBy, OrgID);
        }
        #endregion


        #region[Pre-Expense added by vendors For Vendor List for Buyer]
        public List<Vendor> GetPreExpVendors_AddedVendor_ByBuyer(Int32 AddedBy)
        {
            return objdal.GetPreExpVendors_AddedVendor_ByBuyer(AddedBy);
        }
        #endregion

        #region[Pre-Expense added by vendors For Vendor List for Child Buyer]
        public List<Vendor> GetPreExpVendors_AddedVendor_ByBuyer(Int32 EntityID, Int32 ParentEntityID, Int32 EntityTypeID,Int16 OrgID)
        {
            return objdal.GetPreExpVendors_AddedVendor_ByBuyer(EntityID, ParentEntityID, EntityTypeID, OrgID);
        }
        #endregion

        #region[PreExpense ID By Expense ID]
        public long PreExp_ByExpID(long ExpID)
        {
            return objdal.PreExp_ByExpID(ExpID);
        }
        #endregion

        #region[Expense list sort options]
        public DataTable PreExpense_SortOptions(String strQuery, String Sort1, String Sort2)
        {
            return objdal.PreExpense_SortOptions(strQuery, Sort1, Sort2);
        }
        #endregion

        public bool IsPreExpImageExist(long PreExpID)
        {
            return objdal.IsPreExpImageExist(PreExpID);

        }

        public long PreExp_ByInvID(long InvID)
        {
            return objdal.PreExp_ByInvID(InvID);
        }

        #region [Get PreExpImages By InventoryID - Count]
        public int GetPreExpImagesByInventoryIDCount(long invID)
        {
            return objdal.GetPreExpImagesByInventoryIDCount(invID);
        }
        #endregion

        public IQueryable GetPreExpenseImagesByInventoryID(long invID, Int32 StartRow, Int32 MaximumRows)
        {
            return objdal.GetPreExpenseImagesByInventoryID(invID, StartRow, MaximumRows).AsQueryable();
        }

        public List<Mobile_GetExpenseMinMaxCountResult> GetExpenseMinMaxCount(long PreExpenseID)
        {
            return objdal.GetExpenseMinMaxCount(PreExpenseID);
        }
        #region [Added by Rupendra 08 Aug 2012]
        public List<Mobile_GetEmailDetailResult> GetEmailDetails(Int32 Type, long PreExpenseID)
        {
            return objdal.GetEmailDetail(Type, PreExpenseID);
        }

        public List<Mobile_PreExpense_ByIDResult> PreExpenseDetailBy_PreExpenseId(long PreExpenseID)
        {
            return objdal.PreExpenseDetailByPreExpenseId(PreExpenseID);
        }

        public List<Mobile_GetSMSDetailResult> GetSMSDetails(Int32 Type, long PreExpenseID)
        {
            return objdal.GetSentSMSDetails(Type, PreExpenseID);
        }
        #endregion
        public void EditPreExpense(Int64 PreExpenseID, Int32 Count, Decimal DefaultPrice, Decimal TotalPrice, Int64 ModifiedBy)
        {
            objdal.EditPreExpense(PreExpenseID, Count, DefaultPrice, TotalPrice, ModifiedBy);
        }


        #region [get Expense with filter criteria and get Count]
        public  DataTable SearchEntityExpenses_ByFilter(Int64 EntityId, Int32 EntityTypeID, Int32 ExpenseType, String Fromdate, String ToDate, Int32 Price, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            return objdal.SearchEntityExpenses_ByFilter(EntityId, EntityTypeID,ExpenseType, Fromdate, ToDate,Price,StartRowIndex, MaximumRows, SortExpression);
        }

        public  Int32 SearchEntityExpensesCount_ByFilter(Int64 EntityId, Int32 EntityTypeID, Int32 ExpenseType, String Fromdate, String ToDate, Int32 Price, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            return objdal.SearchEntityExpensesCount_ByFilter(EntityId, EntityTypeID, ExpenseType, Fromdate, ToDate, Price, StartRowIndex, MaximumRows, SortExpression);

        }
        #endregion
       

        #region [Get Expense Type]
        public List<ExpenseType_ver211Result> GetExpenseTyepes()
        {
            return objdal.GetExpenseTyepes();
        }
        #endregion
    }
}
