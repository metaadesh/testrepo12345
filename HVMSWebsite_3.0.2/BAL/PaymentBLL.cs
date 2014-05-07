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

namespace METAOPTION.BAL
{
    public class PaymentBLL
    {
        METAOPTION.DAL.PaymentDAL objPaymentDAL = new METAOPTION.DAL.PaymentDAL();

        public DateTime GetServerDate()
        {
            return objPaymentDAL.GetServerDate();
        }

        public IQueryable<EntityType> GetRecipientType()
        {
            var result = (from p in objPaymentDAL.GetRecipientType()
                          where p.EntityTypeId < 5
                          select p);

            return result;
        }

        public List<GetRecipientsResult> GetRecipients(int receipentTypeId, Int16 OrgID)
        {
            return objPaymentDAL.GetRecipients(receipentTypeId, OrgID);
        }

        public List<GetRecipientsResult> GetRecipients_ByEntityTypeID(int receipentTypeId, Int64 EntityID, Int64 ParentEntityID)
        {
            return objPaymentDAL.GetRecipients_ByEntityTypeID(receipentTypeId, EntityID, ParentEntityID);
        }

        public List<GetActiveAccountsResult> GetActiveAccounts()
        {
            return objPaymentDAL.GetActiveAccounts();
        }

        public List<GetOpenExpenseListResult> GetOpenExpenseList(int entityId, int entityTypeId)
        {
            return objPaymentDAL.GetOpenExpenseList(entityId, entityTypeId).ToList();
        }

        public List<GetAllPaymentsResult> GetAllPayments()
        {
            return objPaymentDAL.GetAllPayments();
        }

        public List<GetExpenseByPaymentIDResult> GetExpenseByPaymentID(int paymentId)
        {
            return objPaymentDAL.GetExpenseByPaymentID(paymentId);
        }

        public List<GetPaymentByIDResult> GetPaymentByID(Int64 paymentId, Int16 OrgID)
        {
            return objPaymentDAL.GetPaymentByID(paymentId, OrgID).ToList();
        }

        //public List<GetAllPaymentsResult> GetFilterPayments(int pageIndex, int pageSize, string sortExpression, String filterClause, ref int rowCount, ref int totalRowCount)
        //{
        //    return objPaymentDAL.GetFilterPayments( pageIndex, pageSize, sortExpression, filterClause, ref rowCount, ref totalRowCount).ToList();
        //}

        public DataSet GetFilterPayments(int pageIndex, int pageSize, string sortExpression, String filterClause, ref int rowCount, ref int totalRowCount, Int32 SystemID)
        {
            return objPaymentDAL.GetFilterPayments(pageIndex, pageSize, sortExpression, filterClause, ref rowCount, ref totalRowCount, SystemID);
        }


        #region [Added by Rupendra 3 Jan 2013 for display Vendor Payment data]
        public DataSet GetFilterPayments_Ver211(int pageIndex, int pageSize, string sortExpression, String filterClause, ref int rowCount, ref int totalRowCount, Int32 SystemID, Int32 EntityTypeId, Int32 EntityId, Int32 BuyerParentId, short OrgID)
        {
            return objPaymentDAL.GetFilterPayments_Ver211(pageIndex, pageSize, sortExpression, filterClause, ref rowCount, ref totalRowCount, SystemID, EntityTypeId, EntityId, BuyerParentId, OrgID);
        }
        #endregion

        public IEnumerable<GetNextCheckNoResult> GetNextCheckNo()
        {
            return objPaymentDAL.GetNextCheckNo();
        }

        public IEnumerable<GetVoidReasonResult> GetVoidReason()
        {
            return objPaymentDAL.GetVoidReason();
        }

        public IEnumerable<GetVoidExpenseByPaymentIDResult> GetVoidExpenseByPaymentID(Int64 paymentID)
        {
            return objPaymentDAL.GetVoidExpenseByPaymentID(paymentID);
        }

        public Boolean VoidCheck(Int64 paymentID, Int32 voidReasonID, String voidComment, Int64 modifiedBy)
        {
            return objPaymentDAL.VoidCheck(paymentID, voidReasonID, voidComment, modifiedBy);
        }

        public List<GetVoidHistoryByPaymentIDResult> GetVoidHistoryByPaymentID(int paymentId)
        {
            return objPaymentDAL.GetVoidHistoryByPaymentID(paymentId).ToList();
        }

        public Boolean Save(Payment pmt, DataTable dtSelectedExpense, ref Int64 paymentID)
        {
            return objPaymentDAL.Save(pmt, dtSelectedExpense, ref  paymentID);
        }


        public Boolean Save(PeachtreeVendorsList pvl)
        {
            return objPaymentDAL.Save(pvl);
        }

        public Boolean Save(IEnumerable<PeachtreeVendorsList> listObj)
        {
            return objPaymentDAL.Save(listObj);
        }

        public IQueryable<PeachtreeVendorsList> GetPeachtreeVendorsList()
        {
            return objPaymentDAL.GetPeachtreeVendorsList();
        }

        public List<GetPeachTreevendorByCodeResult> GetPeachtreeVendorByAccountingCode(string AccountingCode)
        {
            return objPaymentDAL.GetPeachtreeVendorByAccountingCode(AccountingCode).ToList();
        }

        public Boolean DeleteAllPeachtreeVendorsList()
        {
            return objPaymentDAL.DeleteAllPeachtreeVendorsList();
        }

        public Boolean SaveAccountingCode(AccountingCode ac, ref Int64? accountingCodeID, ref int? returnCode)
        {
            return objPaymentDAL.SaveAccountingCode(ac, ref accountingCodeID, ref returnCode);
        }

        public Boolean UpdatePrintCheckStatus(Payment pmt)
        {
            return objPaymentDAL.UpdatePrintCheckStatus(pmt);
        }

        public Boolean UpdatePaymentPeachtree(Payment pmt)
        {
            return objPaymentDAL.UpdatePaymentPeachtree(pmt);
        }

        public List<GetUnpaidCarCostResult> GetUnpaidCarCost(Int32 SystemID, short OrgID)
        {
            return objPaymentDAL.GetUnpaidCarCost(SystemID, OrgID).ToList();
        }

        public IQueryable<Expense> GetExpenseByID(Int64 expenseID)
        {
            return objPaymentDAL.GetExpenseByID(expenseID);
        }

        public Boolean SaveBuyerPayment(Payment pmt, DataTable dtSelectedExpense, decimal ActualPaymentMade
                                         , decimal SelectedExpenseTotal, decimal CurrentOutstandingAmount
                                        , decimal TotalOutstandingAmount, ref Int64 pmtID)
        {
            return objPaymentDAL.Save_BuyerPayment(pmt, dtSelectedExpense, ActualPaymentMade, SelectedExpenseTotal
            , CurrentOutstandingAmount, TotalOutstandingAmount, ref pmtID);
        }

        public IQueryable<Payment> GetPaymentToUpdatePeachTree(DateTime CheckDate)
        {
            return objPaymentDAL.GetPaymentToUpdatePeachTree(CheckDate);
        }

        public void SavePeachTreeLog(long paymentId, String peachTreeRef, String Comment)
        {
            objPaymentDAL.SavePeachTreeLog(paymentId, peachTreeRef, Comment);
        }

        #region[Get Duplicate payment details]
        public List<Payment_CheckDuplicateCheckNoResult> GetDuplicatePaymentDetails(String CheckNo, Int32 Period)
        {
            return objPaymentDAL.GetDuplicatePaymentDetails(CheckNo, Period);
        }
        #endregion

        #region[Open expense list]
        public DataTable GetOpenExpenseList(Int32 EntityID, Int32 EntityTypeID, DataTable dtOpenExpenses)
        {
            return objPaymentDAL.GetOpenExpenseList(EntityID, EntityTypeID, dtOpenExpenses);
        }
        #endregion

        #region [Get Payment for BuyerID and PaymentID]
        public Int32 PaymentCount_ByPaymentID(Int32 PaymentID, Int32 EntityID, Int32 ParentEntityID, Int32 EntityTypeID)
        {
            return objPaymentDAL.PaymentCount_ByPaymentID(PaymentID, EntityID, ParentEntityID, EntityTypeID);
        }
        public Int32 PaymentCount_ByPaymentIDVendorID(long PaymentID, Int32 EntityID, Int32 ParentEntityID, Int32 EntityTypeID)
        {
            return objPaymentDAL.PaymentCount_ByPaymentIDVendorID(PaymentID, EntityID, ParentEntityID, EntityTypeID);
        }
        #endregion
    }
}
