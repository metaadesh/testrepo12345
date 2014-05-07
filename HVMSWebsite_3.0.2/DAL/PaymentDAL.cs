using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using METAOPTION;
using System.Configuration;


namespace METAOPTION.DAL
{
    public class PaymentDAL
    {
        DALDataContext objDAL = new DALDataContext();

        /// <summary>
        /// Get current datetime of server.
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime GetServerDate()
        {
            DALDataContext objDAL = new DALDataContext();
            List<GetServerTimestampResult> serverTimestamp = objDAL.GetServerTimestamp().ToList();
            return serverTimestamp[0].ServerDateTime;
        }

        /// <summary>
        /// Get all Recipient Type avilable       
        /// </summary>
        /// <returns>EntityType</returns>
        public IQueryable<EntityType> GetRecipientType()
        {
            IQueryable<EntityType> result = (from p in objDAL.EntityTypes
                                             where p.IsRealEntity == true
                                             select p) as IQueryable<EntityType>;

            return result;
        }

        /// <summary>
        /// Get a list of recipients for the type specified. 
        /// </summary>
        /// <param name="receipentTypeId"></param>
        /// <returns>Recipients List</returns>
        public List<GetRecipientsResult> GetRecipients(int receipentTypeId, Int16 OrgID)
        {

            return objDAL.GetRecipients(receipentTypeId, OrgID).ToList();
        }
        public List<GetRecipientsResult> GetRecipients_ByEntityTypeID(int receipentTypeId, Int64 EntityID, Int64 ParentEntityID)
        {

            DataTable dTab = new DataTable("Recipients");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("GetRecipientsAgainstEntityTypeID", Conn);
                Cmd.Parameters.AddWithValue("@EntityTypeId", receipentTypeId);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            List<GetRecipientsResult> lst = new List<GetRecipientsResult>();

            foreach (DataRow dr in dTab.Rows)
            {
                GetRecipientsResult gr = new GetRecipientsResult();
                gr.recipientid = Convert.ToInt32(dr["recipientid"]);
                gr.recipienttype = Convert.ToString(dr["recipienttype"]);
                gr.recipientname = Convert.ToString(dr["recipientname"]);
                gr.accountingcode = Convert.ToString(dr["accountingcode"]);
                gr.Street = Convert.ToString(dr["Street"]);
                gr.Suite = Convert.ToString(dr["Suite"]);
                gr.City = Convert.ToString(dr["City"]);
                gr.State = Convert.ToString(dr["State"]);
                gr.StateCode = Convert.ToString(dr["StateCode"]);
                gr.CountryName = Convert.ToString(dr["CountryName"]);
                gr.CountryCode = Convert.ToString(dr["CountryCode"]);
                gr.peachtreevendorguid = Convert.ToString(dr["peachtreevendorguid"]);
                lst.Add(gr);

            }
            return lst;
        }


        /// <summary>
        /// Get Active Accounts
        /// </summary>
        /// <returns>Active accounts list</returns>
        public List<GetActiveAccountsResult> GetActiveAccounts()
        {
            return objDAL.GetActiveAccounts().ToList();
        }

        /// <summary>
        /// Get all active expenses against a entityid and entittype    
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="entityTypeId"></param>
        /// <returns>List of Expenses</returns>
        public List<GetOpenExpenseListResult> GetOpenExpenseList(int entityId, int entityTypeId)
        {
            return objDAL.GetOpenExpenseList(entityId, entityTypeId).ToList();
        }

        /// <summary>
        /// Gat all active Payments
        /// </summary>
        /// <returns>A list of Payments</returns>
        public List<GetAllPaymentsResult> GetAllPayments()
        {
            return objDAL.GetAllPayments().ToList();
        }

        /// <summary>
        /// Get Expense list for a perticular PaymentID
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns>Expense list</returns>
        public List<GetExpenseByPaymentIDResult> GetExpenseByPaymentID(int paymentId)
        {
            return objDAL.GetExpenseByPaymentID(paymentId).ToList();
        }

        /// <summary>
        /// Get Payment detail by a PaymentID
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns>Payment as list</returns>
        public List<GetPaymentByIDResult> GetPaymentByID(Int64 paymentId,Int16 OrgID)
        {
            return objDAL.GetPaymentByID(paymentId, OrgID).ToList();
        }

        /// <summary>
        /// Get all Payment object
        /// </summary>
        /// <returns>Payment object</returns>
        public IQueryable<Payment> GetPayment()
        {
            IQueryable<Payment> result = (from p in objDAL.Payments
                                          select p) as IQueryable<Payment>;

            return result;
        }

        //public List<GetAllPaymentsResult> GetFilterPayments(int pageIndex, int pageSize, string sortExpression, String filterClause, ref int rowCount, ref int totalRowCount)
        //{
        //    int? count1 = 0;
        //    int? count2 = 0;

        //    List<GetAllPaymentsResult> result =  objDAL.GetFilteredPayments(pageIndex, pageSize, sortExpression, filterClause, ref count1, ref count2).ToList();

        //    rowCount = (int) count1;
        //    totalRowCount = (int) count2;

        //    return result; 
        //}

        /// <summary>
        /// Get Payments records as per the filter result for a perticular page
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="filterClause"></param>
        /// <param name="rowCount"></param>
        /// <param name="totalRowCount"></param>
        /// <returns>DataSet contains the payments result</returns>
        public DataSet GetFilterPayments(int pageIndex, int pageSize, string sortExpression, String filterClause, ref int rowCount, ref int totalRowCount, Int32 SystemID)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@pageIndex", SqlDbType.Int);
            param[0].Value = pageIndex;
            param[1] = new SqlParameter("@pageSize", SqlDbType.Int);
            param[1].Value = pageSize;
            param[2] = new SqlParameter("@sortExpression", SqlDbType.NVarChar, 100);
            param[2].Value = sortExpression;
            param[3] = new SqlParameter("@filterClause", SqlDbType.NVarChar, 4000);
            param[3].Value = filterClause;
            param[4] = new SqlParameter("@rowCount", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Default, rowCount);
            param[5] = new SqlParameter("@totalRowCount", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Default, totalRowCount);
            param[6] = new SqlParameter("@SystemID", SystemID);

            DataSet ds = SqlPipe.ExecuteDataset(objDAL.Connection.ConnectionString, "GetFilteredPayments", param);
            rowCount = (Int32)param[4].Value;
            totalRowCount = (Int32)param[5].Value;

            return ds;
        }
        #region [Added by Rupendra 3 Jan 2013 for display Vendor Payment data]
        public DataSet GetFilterPayments_Ver211(int pageIndex, int pageSize, string sortExpression, String filterClause, ref int rowCount, ref int totalRowCount, Int32 SystemID, Int32 EntityTypeId, Int32 EntityId, Int32 BuyerParentId, short OrgID)
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@pageIndex", SqlDbType.Int);
            param[0].Value = pageIndex;
            param[1] = new SqlParameter("@pageSize", SqlDbType.Int);
            param[1].Value = pageSize;
            param[2] = new SqlParameter("@sortExpression", SqlDbType.NVarChar, 100);
            param[2].Value = sortExpression;
            param[3] = new SqlParameter("@filterClause", SqlDbType.NVarChar, 4000);
            param[3].Value = filterClause;
            param[4] = new SqlParameter("@rowCount", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Default, rowCount);
            param[5] = new SqlParameter("@totalRowCount", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Default, totalRowCount);
            param[6] = new SqlParameter("@SystemID", SystemID);
            param[7] = new SqlParameter("@EntityTypeID", EntityTypeId);
            param[8] = new SqlParameter("@UserEntityID", EntityId);
            param[9] = new SqlParameter("@ParentEntityID", BuyerParentId);
            param[10] = new SqlParameter("@OrgID", OrgID);
      
            DataSet ds = SqlPipe.ExecuteDataset(objDAL.Connection.ConnectionString, "GetFilteredPayments_Ver211", param);
            rowCount = (Int32)param[4].Value;
            totalRowCount = (Int32)param[5].Value;

            return ds;
        }
        #endregion
        /// <summary>
        /// Get the Next Check No
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GetNextCheckNoResult> GetNextCheckNo()
        {
            return objDAL.GetNextCheckNo();
        }

        /// <summary>
        /// Get Void Reason
        /// </summary>
        /// <returns>List of void reason</returns>
        public IEnumerable<GetVoidReasonResult> GetVoidReason()
        {
            return objDAL.GetVoidReason();
        }

        /// <summary>
        ///  Get expenses that was voided for a perticular payment ID
        /// </summary>
        /// <param name="paymentID"></param>
        /// <returns>Void Expenses</returns>
        public IEnumerable<GetVoidExpenseByPaymentIDResult> GetVoidExpenseByPaymentID(Int64 paymentID)
        {
            return objDAL.GetVoidExpenseByPaymentID(paymentID);
        }

        /// <summary>
        /// Void a Payment and maintains the history for it.
        /// </summary>
        /// <param name="paymentID"></param>
        /// <param name="voidReasonID"></param>
        /// <param name="voidComment"></param>
        /// <param name="modifiedBy"></param>
        /// <returns>Boolean</returns>
        public Boolean VoidCheck(Int64 paymentID, Int32 voidReasonID, String voidComment, Int64 modifiedBy)
        {
            Boolean retVal = false;
            int? errorCode = 0;

            objDAL.VoidCheck(paymentID, voidReasonID, voidComment, modifiedBy, ref errorCode);

            if (errorCode == 0)
                retVal = true;

            return retVal;
        }

        /// <summary>
        ///  Get history result for a void payment       
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        public List<GetVoidHistoryByPaymentIDResult> GetVoidHistoryByPaymentID(int paymentId)
        {
            return objDAL.GetVoidHistoryByPaymentID(paymentId).ToList();
        }

        /// <summary>
        /// Save the Payment and selected expense for that Payment
        /// </summary>
        /// <param name="pmt"></param>
        /// <param name="dtSelectedExpense"></param>
        /// <param name="pmtID"></param>
        /// <returns>True if saved successfully. Returns new paymentID as ref.</returns>
        public Boolean Save(Payment pmt, DataTable dtSelectedExpense, ref Int64 pmtID)
        {
            Boolean retVal = false;
            DALDataContext objDAL = new DALDataContext();
            System.Data.Common.DbTransaction tran = null;
            Int64? paymentID = 0;
            int? errorCode = 0;
            Int64? userID = pmt.AddedBy;

            try
            {
                objDAL.Connection.Open();

                //BeginTran
                tran = objDAL.Connection.BeginTransaction();
                objDAL.Transaction = tran;

                // Inser data in Payment
                objDAL.Payment_Insert(ref paymentID, pmt.EntityTypeId, pmt.EntityId, pmt.CheckNumber,
                                        pmt.Amount, pmt.CheckDate, pmt.InvoiceNumber, pmt.BankAccountId,
                                        pmt.Comments, pmt.IsPrinted, pmt.PrintDateTime,
                                        pmt.PeechtreeRefNumber, pmt.PeechtreeRefDate, pmt.DateAdded,
                                        pmt.AddedBy, pmt.DateModified, pmt.ModifiedBy, pmt.DateDeleted,
                                        pmt.DeletedBy, pmt.IsActive, ref errorCode);

                pmtID = (Int64)paymentID;



                if (pmt.EntityTypeId != 4) // Do not Update in Expense if Utility Company
                {
                    // Update Data in Expense
                    foreach (DataRow dr in dtSelectedExpense.Rows)
                    {
                        Int64 objectId = Int64.Parse(dr["ObjectId"].ToString());

                        if (objectId > 0)
                            objDAL.UpdateExpensePaidStatus(objectId, paymentID, true, userID, ref errorCode);
                    }
                }

                // Commit transaction
                tran.Commit();
                retVal = true;
            }
            catch (Exception ex)
            {
                // Rollback transaction
                if (tran != null)
                    tran.Rollback();
            }
            finally
            {
                // Close the connection
                if (objDAL.Connection.State == ConnectionState.Open)
                    objDAL.Connection.Close();
            }

            return retVal;
        }






        /// <summary>
        /// Save the PeachtreeVendorsList
        /// </summary>
        /// <param name="pvl"></param>
        /// <returns>Boolean</returns>
        public Boolean Save(PeachtreeVendorsList pvl)
        {
            Boolean retVal = false;
            try
            {
                objDAL.PeachtreeVendorsLists.InsertOnSubmit(pvl);
                objDAL.SubmitChanges();

                retVal = true;
            }
            catch { }

            return retVal;
        }

        public Boolean Save(IEnumerable<PeachtreeVendorsList> listObj)
        {
            Boolean retVal = false;
            try
            {
                objDAL.PeachtreeVendorsLists.InsertAllOnSubmit(listObj);
                objDAL.SubmitChanges();

                retVal = true;
            }
            catch { }

            return retVal;
        }

        /// <summary>
        /// Get all Peachtree Vendor List
        /// </summary>
        /// <returns>PeachtreeVendorsList</returns>
        public IQueryable<PeachtreeVendorsList> GetPeachtreeVendorsList()
        {
            IQueryable<PeachtreeVendorsList> result = (from p in objDAL.PeachtreeVendorsLists
                                                       select p) as IQueryable<PeachtreeVendorsList>;

            return result;
        }

        /// <summary>
        /// Get all Peachtree Vendor List
        /// </summary>
        /// <returns>PeachtreeVendorsList</returns>
        public List<GetPeachTreevendorByCodeResult> GetPeachtreeVendorByAccountingCode(string AccountingCode)
        {
            return objDAL.GetPeachTreeVendorByCode(AccountingCode).ToList();
        }


        /// <summary>
        /// Delete all data from PeachtreeVendorsList
        /// </summary>
        /// <returns>Boolean</returns>
        public Boolean DeleteAllPeachtreeVendorsList()
        {
            DALDataContext objDAL = new DALDataContext();
            Int32 returnCode = 0;
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@returnCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Default, returnCode);

            Int32 val = SqlPipe.ExecuteNonQuery(objDAL.Connection.ConnectionString, "PeachtreeVendorsListDelete", param);
            returnCode = (Int32)param[0].Value;

            return (returnCode == 0);
        }

        /// <summary>
        /// Saves the accounting code
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="accountingCodeID"></param>
        /// <param name="returnCode"></param>
        /// <returns>Boolean</returns>
        public Boolean SaveAccountingCode(AccountingCode ac, ref Int64? accountingCodeID, ref int? returnCode)
        {
            Boolean retVal = false;

            try
            {
                objDAL.AccountingCodeSave(ref accountingCodeID, ac.EntityID, ac.EntityTypeID,
                            ac.AccountingCode1, ac.PeachtreeVendorGUID, ac.DateAdded, ac.AddedBy,
                            ac.DateModified, ac.ModifiedBy, ac.DateDeleted, ac.DeletedBy,
                            ac.IsActive, ref returnCode);


                retVal = true;
            }
            catch { }

            return retVal;
        }

        /// <summary>
        /// Update Print Check Status in Payment Table
        /// </summary>
        /// <param name="p"></param>
        /// <returns>Boolean</returns>
        public Boolean UpdatePrintCheckStatus(Payment p)
        {
            Boolean retVal = false;
            int? returnCode = 0;

            try
            {
                objDAL.PrintCheckStatusUpdate(p.PaymentId, p.IsPrinted, p.PrintDateTime, ref returnCode);

                if (returnCode == 0)
                    retVal = true;
            }
            catch { }

            return retVal;
        }

        /// <summary>
        /// Update PeechtreeRefNumber in Payment Table
        /// </summary>
        /// <param name="p"></param>
        /// <returns>Boolean</returns>
        public Boolean UpdatePaymentPeachtree(Payment p)
        {
            Boolean retVal = false;
            int? returnCode = 0;

            try
            {
                objDAL.PaymentPeachtreeUpdate(p.PaymentId, p.PeechtreeRefNumber, p.PeechtreeRefDate, ref returnCode);

                if (returnCode == 0)
                    retVal = true;
            }
            catch { }

            return retVal;
        }

        /// <summary>
        /// GetUppaid CarCost
        /// </summary>
        /// <returns></returns>
        public List<GetUnpaidCarCostResult> GetUnpaidCarCost(Int32 SystemID, short OrgID)
        {
            return objDAL.GetUnpaidCarCost(SystemID, OrgID).ToList();
        }


        /// <summary>
        /// Get Expense detail by expenseID
        /// </summary>
        /// <param name="expenseID"></param>
        /// <returns></returns>
        public IQueryable<Expense> GetExpenseByID(Int64 expenseID)
        {
            IQueryable<Expense> result = (from e in objDAL.Expenses
                                          where e.ExpenseId == expenseID
                                          select e) as IQueryable<Expense>;

            return result;
        }


        /// <summary>
        /// Save the Buyer Payment and selected expense for that Payment
        /// </summary>
        /// <param name="pmt"></param>
        /// <param name="dtSelectedExpense"></param>
        /// <param name="pmtID"></param>
        /// <returns>True if saved successfully. Returns new paymentID as ref.</returns>
        public Boolean Save_BuyerPayment(Payment pmt, DataTable dtSelectedExpense, decimal ActualPaymentMade
                                         , decimal SelectedExpenseTotal, decimal CurrentOutstandingAmount
                                        , decimal TotalOutstandingAmount, ref Int64 pmtID)
        {
            Boolean retVal = false;
            DALDataContext objDAL = new DALDataContext();
            System.Data.Common.DbTransaction tran = null;
            Int64? paymentID = 0;
            int? errorCode = 0;
            Int64? userID = pmt.AddedBy;

            try
            {
                objDAL.Connection.Open();

                //BeginTran
                tran = objDAL.Connection.BeginTransaction();
                objDAL.Transaction = tran;

                // Inser data in Payment
                objDAL.Payment_Insert(ref paymentID, pmt.EntityTypeId, pmt.EntityId, pmt.CheckNumber,
                                        pmt.Amount, pmt.CheckDate, pmt.InvoiceNumber, pmt.BankAccountId,
                                        pmt.Comments, pmt.IsPrinted, pmt.PrintDateTime,
                                        pmt.PeechtreeRefNumber, pmt.PeechtreeRefDate, pmt.DateAdded,
                                        pmt.AddedBy, pmt.DateModified, pmt.ModifiedBy, pmt.DateDeleted,
                                        pmt.DeletedBy, pmt.IsActive, ref errorCode);

                pmtID = (Int64)paymentID;

                //Insert data in Buyer_PaymentTransactions Table
                objDAL.Buyer_PaymentTransactionInsert(pmt.EntityId, pmtID, ActualPaymentMade, SelectedExpenseTotal, CurrentOutstandingAmount,
                    TotalOutstandingAmount, pmt.AddedBy);

                if (pmt.EntityTypeId != 4) // Do not Update in Expense if Utility Company
                {
                    // Update Data in Expense
                    foreach (DataRow dr in dtSelectedExpense.Rows)
                    {
                        Int64 objectId = Int64.Parse(dr["ObjectId"].ToString());

                        if (objectId > 0)
                            objDAL.UpdateExpensePaidStatus(objectId, paymentID, true, userID, ref errorCode);
                    }
                }

                // Commit transaction
                tran.Commit();
                retVal = true;
            }
            catch (Exception ex)
            {
                // Rollback transaction
                if (tran != null)
                    tran.Rollback();
            }
            finally
            {
                // Close the connection
                if (objDAL.Connection.State == ConnectionState.Open)
                    objDAL.Connection.Close();
            }

            return retVal;
        }

        /// <summary>
        /// Get Payments To Update Peach Tree
        /// </summary>
        /// <param name="CheckDate"></param>
        /// <returns></returns>
        public IQueryable<Payment> GetPaymentToUpdatePeachTree(DateTime CheckDate)
        {
            String varNull = null;
            IQueryable<Payment> result = (from e in objDAL.Payments
                                          where e.PeechtreeRefNumber == varNull
                                            && e.CheckDate > CheckDate
                                          select e) as IQueryable<Payment>;

            return result;
        }

        public void SavePeachTreeLog(long paymentId, String peachTreeRef, String Comment)
        {
            PeachTreeLog ptLog = new PeachTreeLog();
            ptLog.PaymentID = paymentId;
            ptLog.PeachTreeRef = peachTreeRef;
            ptLog.DateUpdated = DateTime.Now;
            ptLog.Comment = Comment;
            DALDataContext dc = new DALDataContext();
            METAOPTION.DALDataContext.Factory.DB.SubmitChanges();
        }

        #region[Get Duplicate payment details]
        public List<Payment_CheckDuplicateCheckNoResult> GetDuplicatePaymentDetails(String CheckNo, Int32 Period)
        {
            DALDataContext db = new DALDataContext();
            return db.Payment_CheckDuplicateCheckNo(CheckNo, Period).ToList<Payment_CheckDuplicateCheckNoResult>();
        }
        #endregion

        #region[Open expense list]
        public DataTable GetOpenExpenseList(Int32 EntityID, Int32 EntityTypeID, DataTable dtOpenExpenses)
        {
            DALDataContext objDal = new DALDataContext();
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("OpenExpenseList"))
                {
                    try
                    {
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        cmd.Parameters.Add(new SqlParameter("@EntityId", EntityID));
                        cmd.Parameters.Add(new SqlParameter("@EntityTypeId", EntityTypeID));

                        SqlParameter cmdFieldCustomValue = new SqlParameter("@SelectedExpense", dtOpenExpenses);
                        cmdFieldCustomValue.SqlDbType = SqlDbType.Structured;
                        cmd.Parameters.Add(cmdFieldCustomValue);

                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dt = new DataTable();
                        dt.Load(dr);

                    }
                    catch (Exception ex)
                    { }
                    finally
                    {
                        conn.Close();
                        objDal.Dispose();
                    }
                }
            }
            return dt;
        }
        #endregion

        #region [Get Payment for BuyerID and PaymentID]
        public Int32 PaymentCount_ByPaymentID(Int32 PaymentID, Int32 EntityID, Int32 ParentEntityID, Int32 EntityTypeID)
        {
            Int32 Result = 0;

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Payment_AgainstBuyerID", Conn);
                Cmd.Parameters.AddWithValue("@PaymentID", PaymentID);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }

        public Int32 PaymentCount_ByPaymentIDVendorID(long PaymentID, Int32 EntityID, Int32 ParentEntityID, Int32 EntityTypeID)
        {
            Int32 Result = 0;

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Payment_AgainstVendorID", Conn);
                Cmd.Parameters.AddWithValue("@PaymentID", PaymentID);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }

        #endregion
    }
}
