using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using METAOPTION;
using System.Dynamic;
using System.Collections;

namespace METAOPTION.DAL
{
    public class PreExpenseDAL
    {
        DALDataContext objDAL = new DALDataContext();

        public DataTable SearchPreExpense(String VIN, long AddedBy, long Vendor, Int32 Status, DateTime SyncFrom, DateTime SyncTo, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            DataTable dTab = new DataTable("Expenses");

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_PreExpenseSearch", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@Vendor", Vendor);
                Cmd.Parameters.AddWithValue("@Status", Status);
                Cmd.Parameters.AddWithValue("@SyncDateFrom", SyncFrom);
                Cmd.Parameters.AddWithValue("@SyncDateTo", SyncTo);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@sortExpression", SortExpression);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public Int32 SearchPreExpenseCount(String VIN, long AddedBy, long Vendor, Int32 Status, DateTime SyncFrom, DateTime SyncTo, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_PreExpenseSearchCount", Conn);

                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@Vendor", Vendor);
                Cmd.Parameters.AddWithValue("@Status", Status);
                Cmd.Parameters.AddWithValue("@SyncDateFrom", SyncFrom);
                Cmd.Parameters.AddWithValue("@SyncDateTo", SyncTo);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@sortExpression", SortExpression);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }

        public List<ExpenseType> GetAllExpenseType()
        {
            DALDataContext objDal = new DALDataContext();
            //List<ExpenseType> result = (from p in METAOPTION.DALDataContext.Factory.DB.ExpenseTypes
            //                                  where p.IsActive == 1
            //                                  orderby p.ExpenseType1 ascending
            //                                  select p) as List<ExpenseType>;

            List<ExpenseType> result = (from p in objDal.ExpenseTypes
                                        where p.IsActive == 1
                                        orderby p.ExpenseType1 ascending
                                        select p).ToList<ExpenseType>();

            return result;
        }

        public List<Contact> GetContactDetail(long ContactID)
        {
            DALDataContext objDal = new DALDataContext();
            return (from c in objDal.Contacts
                    where c.ContactId == ContactID
                    select c).ToList<Contact>();
        }

        public List<Mobile_GetAllExpensesResult> GetAllExpenseTypes(Int32 EntityTypeID, Int64 EntityID)
        {
            DALDataContext objDal = new DALDataContext();
            return objDal.Mobile_GetAllExpenses(EntityTypeID, EntityID).ToList<Mobile_GetAllExpensesResult>();
        }

        public void Insert_EntityExpenses(Int64 AddedBy, DataTable dtExpenses)
        {
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Mobile_InsertEntityExpenses"))
                {
                    try
                    {
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        cmd.Parameters.Add(new SqlParameter("@AddedBy", AddedBy));


                        SqlParameter cmdFieldCustomValue = new SqlParameter("@ExpenseValues", dtExpenses);
                        cmdFieldCustomValue.SqlDbType = SqlDbType.Structured;
                        cmd.Parameters.Add(cmdFieldCustomValue);
                        cmd.ExecuteNonQuery();
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
        }

        public void ApproveExpense(Int64 AddedBy, Int32 Status, DataTable dtExpenses, String ApprovalNote)
        {
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Mobile_PreExpenseInsert"))
                {
                    try
                    {
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        cmd.Parameters.Add(new SqlParameter("@AddedBy", AddedBy));
                        cmd.Parameters.Add(new SqlParameter("@Status", Status));
                        cmd.Parameters.Add(new SqlParameter("@ApprovalNote", ApprovalNote));

                        SqlParameter cmdFieldCustomValue = new SqlParameter("@PreExpenseValues", dtExpenses);
                        cmdFieldCustomValue.SqlDbType = SqlDbType.Structured;
                        cmd.Parameters.Add(cmdFieldCustomValue);
                        cmd.ExecuteNonQuery();
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
        }

        public void RejectPreExpense(Int64 PreExpID, Int64 ModifiedBy, Int32 Status, String Reason)
        {
            int i = METAOPTION.DALDataContext.Factory.DB.Mobile_RejectPreExpense(PreExpID, ModifiedBy, Status, Reason);
        }

        public void DeletePreExpense(Int64 PreExpenseID, Int64 UserID, Int32 Status, String Reason)
        {
            int i = METAOPTION.DALDataContext.Factory.DB.Mobile_DeletePreExpense(PreExpenseID, UserID, Status, Reason);
        }

        public void PreExpense_MakePending(Int64 PreExpenseID, Int64 UserID)
        {
            int i = METAOPTION.DALDataContext.Factory.DB.Mobile_PreExpense_MarkPending(PreExpenseID, UserID);
        }

        public DataTable SearchEntityExpenses(Int64 EntityId, Int32 EntityTypeID, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            DataTable dTab = new DataTable("Expenses");

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_EntityExpensesSearch", Conn);
                Cmd.Parameters.AddWithValue("@EntityId", EntityId);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@sortExpression", SortExpression);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public Int32 SearchEntityExpensesCount(Int64 EntityId, Int32 EntityTypeID, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            Int32 Result = 0;

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_EntityExpensesSearchCount", Conn);
                Cmd.Parameters.AddWithValue("@EntityId", EntityId);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@sortExpression", SortExpression);
                Cmd.CommandType = CommandType.StoredProcedure;
                Result = (Int32)Cmd.ExecuteScalar();

                objDal.Dispose();
            }
            return Result;
        }

        #region [get Expense with filter criteria and get Count]
        public DataTable SearchEntityExpenses_ByFilter(Int64 EntityId, Int32 EntityTypeID, Int32 ExpenseType, String Fromdate, String ToDate, Int32 Price, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            DataTable dTab = new DataTable();

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_EntityExpensesSearch_ver211", Conn);
                Cmd.Parameters.AddWithValue("@SecurityUserID", EntityId);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                Cmd.Parameters.AddWithValue("@ExpenseType", ExpenseType);
                Cmd.Parameters.AddWithValue("@Fromdate", Fromdate);
                Cmd.Parameters.AddWithValue("@ToDate", ToDate);
                Cmd.Parameters.AddWithValue("@Price", Price);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@sortExpression", SortExpression);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public Int32 SearchEntityExpensesCount_ByFilter(Int64 EntityId, Int32 EntityTypeID, Int32 ExpenseType, String Fromdate, String ToDate, Int32 Price, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            Int32 Result = 0;

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_EntityExpensesSearchCount_ver211", Conn);
                Cmd.Parameters.AddWithValue("@SecurityUserID", EntityId);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                Cmd.Parameters.AddWithValue("@ExpenseType", ExpenseType);
                Cmd.Parameters.AddWithValue("@Fromdate", Fromdate);
                Cmd.Parameters.AddWithValue("@ToDate", ToDate);
                Cmd.Parameters.AddWithValue("@Price", Price);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@sortExpression", SortExpression);
                Cmd.CommandType = CommandType.StoredProcedure;
                Result = (Int32)Cmd.ExecuteScalar();

                objDal.Dispose();
            }
            return Result;
        }
        #endregion

        #region [Get Expense Type]
        public List<ExpenseType_ver211Result> GetExpenseTyepes()
        {
            DALDataContext objDal = new DALDataContext();
            return objDal.ExpenseType_ver211().ToList<ExpenseType_ver211Result>();
        }
        #endregion

        public List<Mobile_ExpensesOfEntityResult> GetEntityExpenses(Int32 EntityTypeID, long EntityID)
        {
            DALDataContext objDal = new DALDataContext();
            return objDal.Mobile_ExpensesOfEntity(EntityTypeID, EntityID).ToList<Mobile_ExpensesOfEntityResult>();
        }

        public List<Mobile_GetEntityExpensesByIDResult> GetEntityExpenseByID(Int64 EntityExpenseID)
        {
            DALDataContext objDal = new DALDataContext();
            return objDal.Mobile_GetEntityExpensesByID(EntityExpenseID).ToList<Mobile_GetEntityExpensesByIDResult>();
        }

        public Int32 UpdateEntityExpense(Int64 EntityExpenseID, Int32 ExpenseTypeId, Int32 MinCount, Int32 MaxCount, Decimal DefaultpPrice, Int64 ModifiedBy)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_UpdateEntityExpenses", Conn);
                Cmd.Parameters.AddWithValue("@EntityExpenseID", EntityExpenseID);
                Cmd.Parameters.AddWithValue("@ExpenseTypeId", ExpenseTypeId);
                Cmd.Parameters.AddWithValue("@MinCount", MinCount);
                Cmd.Parameters.AddWithValue("@MaxCount", MaxCount);
                Cmd.Parameters.AddWithValue("@DefaultPrice", DefaultpPrice);
                Cmd.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);

                SqlParameter ouparm = Cmd.Parameters.Add("@Status", SqlDbType.Int);
                ouparm.Direction = ParameterDirection.Output;

                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.ExecuteNonQuery();
                Result = Convert.ToInt32(Cmd.Parameters["@Status"].Value);
                objDal.Dispose();
            }
            return Result;
        }

        public void DeleteEntityExpense(Int64 EntityExpenseID, Int64 DeletedBy)
        {
            String strqry = String.Format("Update Mobile_EntityExpenses Set DateDeleted='{0}',DeletedBy={1},IsActive=0 where EntityExpenseID={2}", DateTime.Now, DeletedBy, EntityExpenseID);
            DALDataContext.Factory.DB.ExecuteCommand(strqry);
            DALDataContext.Factory.DB.SubmitChanges();
        }

        #region[Images]
        public System.Collections.ArrayList PreExpenseImages(string PreExpenseID)
        {
            DataTable dTab = new DataTable();
            System.Collections.ArrayList array = new System.Collections.ArrayList();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_PreExpenseImagesPath", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@PreExpenseId", PreExpenseID);

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
        /// <summary>
        /// //Modified by Rupendra 6 Sep 12
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public System.Collections.ArrayList PreExpenseImagesByInvId(string VIN)
        {
            DataTable dTab = new DataTable();
            System.Collections.ArrayList array = new System.Collections.ArrayList();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_PreExpenseImagesPathByInventoryId", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@VIN", VIN);

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

        public List<Mobile_GetInvforExpensesResult> GetInvForExpenses(String VIN)
        {
            DALDataContext objDal = new DALDataContext();
            return objDal.Mobile_GetInvforExpenses(VIN).ToList<Mobile_GetInvforExpensesResult>();
        }

        #region [Get PreExpense Detail by PreExpenseId]
        public IEnumerable PreExpenseDetail_ByPreExpenseId(long PreExpenseId)
        {
            DALDataContext objDAL = new DALDataContext();
            return objDAL.Mobile_PreExpense_ByID(PreExpenseId).AsEnumerable();
        }
        #endregion

        #region[Duplicate expense]
        public DataTable GetDuplicateExpense(String VIN, Int32 ExpenseTypeID, Decimal Amount, Int32 EntityID, Int32 EntityTypeID, Int32 Period)
        {
            DataTable dTab = new DataTable("Expense");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_DuplicateExpense", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@ExpenseTypeID", ExpenseTypeID);
                Cmd.Parameters.AddWithValue("@Amount", Amount);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@EntityTypeId", EntityTypeID);
                Cmd.Parameters.AddWithValue("@Period", Period);

                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region[Duplicate pre-expense]
        public DataTable GetDuplicatePreExpense(String VIN, Int32 ExpenseTypeID, Decimal Amount, Int32 EntityID, Int32 EntityTypeID, Int32 Period)
        {
            DataTable dTab = new DataTable("Expense");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_DuplicatePreExpense", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@ExpenseTypeID", ExpenseTypeID);
                Cmd.Parameters.AddWithValue("@Amount", Amount);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@EntityTypeId", EntityTypeID);
                Cmd.Parameters.AddWithValue("@Period", Period);

                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        #region[Pre-Expense added by users]
        public List<Mobile_PreExp_AddedByResult> GetPreExpUsers()
        {
            DALDataContext objDal = new DALDataContext();
            List<Mobile_PreExp_AddedByResult> result = objDal.Mobile_PreExp_AddedBy().ToList<Mobile_PreExp_AddedByResult>();
            return result;
        }
        #endregion
        #region[Pre-Expense added by users]
        public List<SecurityUser> GetPreExpUsersByAddedBy(Int32 EntityID, Int32 ParentEntityID, Int32 EntityTypeID, Int16 OrgID)
        {
            List<SecurityUser> lst = new List<SecurityUser>();
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_PreExp_AddedBy_ver2111", Conn);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            foreach (DataRow dr in dTab.Rows)
            {
                SecurityUser SU = new SecurityUser();
                SU.DisplayName = dr["DisplayName"].ToString();
                SU.SecurityUserID = Convert.ToInt32(dr["SecurityUserID"]);
                lst.Add(SU);
            }
            return lst;
        }
        #endregion

        #region[Pre-Expense added by vendors]
        public List<Vendor> GetPreExpVendors(Int16 OrgID)
        {
            DALDataContext objDal = new DALDataContext();
            List<Vendor> result = (from p in objDal.Vendors
                                   from PreExp in objDal.Mobile_PreExpenses
                                   where p.VendorId == PreExp.EntityId && p.IsActive == 1
                                   && p.OrgID==PreExp.OrgID
                                   && p.OrgID==OrgID
                                   select p).Distinct().OrderBy(p => p.VendorName).ToList<Vendor>();
            return result;
        }
        #endregion

        #region[Pre-Expense added by vendors For only vendor]
        public List<Vendor> GetPreExpVendors_AddedVendor(Int32 SecurityUserID, Int16 OrgID)
        {
            DALDataContext objDal = new DALDataContext();
            List<Vendor> result = (from p in objDal.Vendors
                                   from PreExp in objDal.Mobile_PreExpenses
                                   from SU in objDal.SecurityUsers
                                   where p.VendorId == PreExp.EntityId
                                   && p.VendorId == SU.EntityID && p.OrgID == SU.OrgID
                                   && SU.SecurityUserID == SecurityUserID 
                                   && p.IsActive == 1
                                   && p.OrgID == OrgID
                                   select p).Distinct().OrderBy(p => p.VendorName).ToList<Vendor>();
            return result;
        }
        #endregion
        #region[Pre-Expense added by vendors For only Buyer]
        public List<Vendor> GetPreExpVendors_AddedVendor_ByBuyer(Int32 SecurityUserID)
        {
            DALDataContext objDal = new DALDataContext();
            List<Vendor> result = (from p in objDal.Vendors
                                   from PreExp in objDal.Mobile_PreExpenses
                                   from SU in objDal.SecurityUsers
                                   where p.VendorId == PreExp.EntityId && p.VendorId == SU.EntityID && PreExp.AddedBy == SecurityUserID && p.IsActive == 1
                                   select p).Distinct().OrderBy(p => p.VendorName).ToList<Vendor>();

            //List<Vendor> result = (from p in objDal.Vendors
            //                       from PreExp in objDal.Mobile_PreExpenses
            //                       from SU in objDal.SecurityUsers
            //                       where p.VendorId == PreExp.EntityId
            //                       && p.VendorId == SU.EntityID && p.OrgID==SU.OrgID
            //                       && PreExp.AddedBy == SecurityUserID && p.OrgID==PreExp.OrgID
            //                       && p.IsActive == 1
            //                       && p.OrgID==OrgID
            //                       select p).Distinct().OrderBy(p => p.VendorName).ToList<Vendor>();
            return result;
        }
        #endregion

        #region[Pre-Expense added by vendors For only Child Buyer]
        public List<Vendor> GetPreExpVendors_AddedVendor_ByBuyer(Int32 EntityID, Int32 ParentBuyerID, Int32 EntityTypeID, Int16 OrgID)
        {
            List<Vendor> lst = new List<Vendor>();
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_PreExp_AddedByVendor_ForChildBuyer", Conn);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentBuyerID);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            foreach (DataRow dr in dTab.Rows)
            {
                Vendor SU = new Vendor();
                SU.VendorName = dr["VendorName"].ToString();
                SU.VendorId = Convert.ToInt32(dr["VendorId"]);
                lst.Add(SU);
            }
            return lst;

        }
        #endregion

        #region[PreExpense ID By Expense ID]
        public long PreExp_ByExpID(long ExpID)
        {
            DALDataContext db = new DALDataContext();
            var result = db.Mobile_PreExpenses.FirstOrDefault(x => x.ExpenseId == ExpID);
            if (result != null)
                return result.PreExpenseID;
            else
                return 0;
        }
        #endregion

        #region[Expense list sort options]
        public DataTable PreExpense_SortOptions(String strQuery, String Sort1, String Sort2)
        {
            DataTable dTab = new DataTable("SortPreExp");
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

        public bool IsPreExpImageExist(long PreExpID)
        {
            int count = METAOPTION.DALDataContext.Factory.DB.PreExp_MobileImagesCount(PreExpID).Single().Total.GetValueOrDefault();
            if (count > 0)
                return true;
            else return false;

        }

        public long PreExp_ByInvID(long InvID)
        {
            DALDataContext db = new DALDataContext();
            var result = db.Mobile_PreExpenses.FirstOrDefault(x => x.InventoryId == InvID);
            if (result != null)
                return result.PreExpenseID;
            else
                return 0;
        }

        #region [Get PreExpImages By InventoryID - Count]
        public int GetPreExpImagesByInventoryIDCount(long invID)
        {
            List<Mobile_PreExpImagesByInvIDCountResult> Result = METAOPTION.DALDataContext.Factory.DB.Mobile_PreExpImagesByInvIDCount(invID).ToList();
            if (Result.Count > 0)
                return Result.First().Total.Value;
            else return 0;
        }
        #endregion

        public IQueryable GetPreExpenseImagesByInventoryID(long invID, Int32 StartRow, Int32 MaximumRows)
        {
            DALDataContext objDAL = new DALDataContext();
            return objDAL.Mobile_PreExpenseImagesByInvID(invID, StartRow, MaximumRows).AsQueryable();
        }

        public List<Mobile_GetExpenseMinMaxCountResult> GetExpenseMinMaxCount(long PreExpenseID)
        {
            DALDataContext objDAL = new DALDataContext();
            return objDAL.Mobile_GetExpenseMinMaxCount(PreExpenseID).ToList<Mobile_GetExpenseMinMaxCountResult>();
        }
        #region [Added by Rupendra 08 Aug 2012]
        public List<Mobile_GetEmailDetailResult> GetEmailDetail(Int32 Type, long PreExpenseID)
        {
            DALDataContext objDAL = new DALDataContext();
            return objDAL.Mobile_GetEmailDetail(Type, PreExpenseID).ToList<Mobile_GetEmailDetailResult>();
        }
        public List<Mobile_PreExpense_ByIDResult> PreExpenseDetailByPreExpenseId(long PreExpenseID)
        {
            DALDataContext objDAL = new DALDataContext();
            return objDAL.Mobile_PreExpense_ByID(PreExpenseID).ToList<Mobile_PreExpense_ByIDResult>();
        }
        public List<Mobile_GetSMSDetailResult> GetSentSMSDetails(Int32 Type, long PreExpenseID)
        {
            DALDataContext objDAL = new DALDataContext();
            return objDAL.Mobile_GetSMSDetail(Type, PreExpenseID).ToList<Mobile_GetSMSDetailResult>();
        }
        #endregion
        public void EditPreExpense(Int64 PreExpenseID, Int32 Count, Decimal DefaultPrice, Decimal TotalPrice, Int64 ModifiedBy)
        {
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Mobile_EditPreExpense"))
                {
                    try
                    {
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        cmd.Parameters.Add(new SqlParameter("@PreExpenseID", PreExpenseID));
                        cmd.Parameters.Add(new SqlParameter("@Count", Count));
                        cmd.Parameters.Add(new SqlParameter("@DefaultPrice", DefaultPrice));
                        cmd.Parameters.Add(new SqlParameter("@TotalPrice", TotalPrice));
                        cmd.Parameters.Add(new SqlParameter("@ModifiedBy", ModifiedBy));

                        cmd.ExecuteNonQuery();
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
        }


        #region [Added by Rupendra on 24 Dec 12 for Vendor, Dealer and Buyer login details]
        public DataTable SearchPreExpense_Ver211(String VIN, long AddedBy, long Vendor, Int32 Status, DateTime SyncFrom, DateTime SyncTo, Int32 EntityTypeID, Int32 EntityID, Int32 ParentEntityID, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, Int16 OrgID)
        {
            DataTable dTab = new DataTable("Expenses");

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_PreExpenseSearch_Ver2111", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@Vendor", Vendor);
                Cmd.Parameters.AddWithValue("@Status", Status);
                Cmd.Parameters.AddWithValue("@SyncDateFrom", SyncFrom);
                Cmd.Parameters.AddWithValue("@SyncDateTo", SyncTo);
                Cmd.Parameters.AddWithValue("@EntityTypeId", EntityTypeID);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@sortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public Int32 SearchPreExpenseCount_Ver211(String VIN, long AddedBy, long Vendor, Int32 Status, DateTime SyncFrom, DateTime SyncTo, Int32 EntityTypeID, Int32 EntityID, Int32 ParentEntityID, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, Int16 OrgID)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_PreExpenseSearchCount_Ver2111", Conn);

                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@Vendor", Vendor);
                Cmd.Parameters.AddWithValue("@Status", Status);
                Cmd.Parameters.AddWithValue("@SyncDateFrom", SyncFrom);
                Cmd.Parameters.AddWithValue("@SyncDateTo", SyncTo);
                Cmd.Parameters.AddWithValue("@EntityTypeId", EntityTypeID);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@sortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }
        #endregion
    }
}
