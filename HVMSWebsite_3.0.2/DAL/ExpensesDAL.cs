using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using METAOPTION;

namespace METAOPTION.DAL
{
    public class ExpensesDAL
    {
        // static DALDataContext objDAL = new DALDataContext();

        #region[Images]
        public System.Collections.ArrayList ExpenseImages(long ExpenseID)
        {
            DataTable dTab = new DataTable();
            System.Collections.ArrayList array = new System.Collections.ArrayList();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_ExpenseImages", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@ExpenseID", ExpenseID);

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
        #region[Pre-Expense added by users]
        public List<Mobile_PreExp_AddedByResult> GetPreExpUsers()
        {
            DALDataContext objDal = new DALDataContext();
            List<Mobile_PreExp_AddedByResult> result = objDal.Mobile_PreExp_AddedBy().ToList<Mobile_PreExp_AddedByResult>();
            return result;
        }
        #endregion
        #region[Expense list sort options]
        public DataTable Expense_SortOptions(String strQuery, String Sort1, String Sort2)
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

        public DataTable SearchExpense(String VIN, string CheckNumber, string CheckPaid, long AddedBy, long EntityId, long EntityTypeId, long ExpenseTypeId, DateTime SyncFrom, DateTime SyncTo, string InvoiceNumber, string SourceFilter, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            DataTable dTab = new DataTable("Expenses");

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("ExpenseSearch", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@Entity", EntityId);
                Cmd.Parameters.AddWithValue("@Entitytpeid", EntityTypeId);
                Cmd.Parameters.AddWithValue("@ExpenseTypeId", ExpenseTypeId);
                Cmd.Parameters.AddWithValue("@SyncDateFrom", SyncFrom);
                Cmd.Parameters.AddWithValue("@SyncDateTo", SyncTo);
                Cmd.Parameters.AddWithValue("@CheckNumber", CheckNumber);
                Cmd.Parameters.AddWithValue("@CheckPaid", CheckPaid);
                Cmd.Parameters.AddWithValue("@IsInvoice", InvoiceNumber);
                Cmd.Parameters.AddWithValue("@IsMobile", SourceFilter);
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

        public Int32 SearchExpenseCount(String VIN, string CheckNumber, string CheckPaid, long AddedBy, long EntityId, long EntityTypeId, long ExpenseTypeId, DateTime SyncFrom, DateTime SyncTo, string InvoiceNumber, string SourceFilter, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)//, , int CheckPaid
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("ExpenseSearchCount", Conn);

                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@Entity", EntityId);
                Cmd.Parameters.AddWithValue("@Entitytpeid", EntityTypeId);
                Cmd.Parameters.AddWithValue("@ExpenseTypeId", ExpenseTypeId);
                Cmd.Parameters.AddWithValue("@SyncDateFrom", SyncFrom);
                Cmd.Parameters.AddWithValue("@SyncDateTo", SyncTo);
                Cmd.Parameters.AddWithValue("@CheckNumber", CheckNumber);
                Cmd.Parameters.AddWithValue("@CheckPaid", CheckPaid);
                Cmd.Parameters.AddWithValue("@IsInvoice", InvoiceNumber);
                Cmd.Parameters.AddWithValue("@IsMobile", SourceFilter);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@sortExpression", SortExpression);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }


        #region[Added by Rupendra 26 Dec 12, Vendor login details]
        public DataTable SearchExpense_Ver211(String VIN, string CheckNumber, string CheckPaid, long AddedBy, long EntityId, long EntityTypeId, long ExpenseTypeId, DateTime SyncFrom, DateTime SyncTo, string InvoiceNumber, string SourceFilter, Int32 LoginEntityTypeID,Int32 UserEntityID, Int32 BuyerParentID, string BuyerIsDirect,Int32 BuyerAccessLevel, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, short orgID)
        {
            DataTable dTab = new DataTable("Expenses");

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("ExpenseSearch_Ver211", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@Entity", EntityId);
                Cmd.Parameters.AddWithValue("@Entitytpeid", EntityTypeId);
                Cmd.Parameters.AddWithValue("@ExpenseTypeId", ExpenseTypeId);
                Cmd.Parameters.AddWithValue("@SyncDateFrom", SyncFrom);
                Cmd.Parameters.AddWithValue("@SyncDateTo", SyncTo);
                Cmd.Parameters.AddWithValue("@CheckNumber", CheckNumber);
                Cmd.Parameters.AddWithValue("@CheckPaid", CheckPaid);
                Cmd.Parameters.AddWithValue("@IsInvoice", InvoiceNumber);
                Cmd.Parameters.AddWithValue("@IsMobile", SourceFilter);
                Cmd.Parameters.AddWithValue("@LoginEntityTypeID", LoginEntityTypeID);
                Cmd.Parameters.AddWithValue("@UserEntityID", UserEntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", BuyerParentID);
                Cmd.Parameters.AddWithValue("@BuyerIsDirect", BuyerIsDirect);
                Cmd.Parameters.AddWithValue("@BuyerAccessLevel", BuyerAccessLevel);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@sortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@OrgID", orgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public Int32 SearchExpenseCount_Ver211(String VIN, string CheckNumber, string CheckPaid, long AddedBy, long EntityId, long EntityTypeId, long ExpenseTypeId, DateTime SyncFrom, DateTime SyncTo, string InvoiceNumber, string SourceFilter, Int32 LoginEntityTypeID,Int32 UserEntityID, Int32 BuyerParentID, string BuyerIsDirect,Int32 BuyerAccessLevel, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression,short orgID)//, , int CheckPaid
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("ExpenseSearchCount_Ver211", Conn);

                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@Entity", EntityId);
                Cmd.Parameters.AddWithValue("@Entitytpeid", EntityTypeId);
                Cmd.Parameters.AddWithValue("@ExpenseTypeId", ExpenseTypeId);
                Cmd.Parameters.AddWithValue("@SyncDateFrom", SyncFrom);
                Cmd.Parameters.AddWithValue("@SyncDateTo", SyncTo);
                Cmd.Parameters.AddWithValue("@CheckNumber", CheckNumber);
                Cmd.Parameters.AddWithValue("@CheckPaid", CheckPaid);
                Cmd.Parameters.AddWithValue("@IsInvoice", InvoiceNumber);
                Cmd.Parameters.AddWithValue("@IsMobile", SourceFilter);
                Cmd.Parameters.AddWithValue("@LoginEntityTypeID", LoginEntityTypeID);
                Cmd.Parameters.AddWithValue("@UserEntityID", UserEntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", BuyerParentID);
                Cmd.Parameters.AddWithValue("@BuyerIsDirect", BuyerIsDirect);
                Cmd.Parameters.AddWithValue("@BuyerAccessLevel", BuyerAccessLevel);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@sortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@OrgID", orgID);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
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

        #region [Get PreExpense Detail by PreExpenseId]
        public IEnumerable PreExpenseDetail_ByPreExpenseId(long PreExpenseId)
        {
            DALDataContext objDAL = new DALDataContext();
            return objDAL.Mobile_PreExpense_ByID(PreExpenseId).AsEnumerable();
        }
        #endregion


        public Int32 UpdateInvoiceNumber(int ExpenseId, string InvoiceNumber)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("UpdateInvoiceNumber", Conn);

                Cmd.Parameters.AddWithValue("@ExpenseId", ExpenseId);
                Cmd.Parameters.AddWithValue("@InvoiceNumber", InvoiceNumber);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteNonQuery();
                objDal.Dispose();
            }
            return Result;
        }

        public Int32 UpdateInventoryCost(int InventoryID, decimal? Carcost)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("UpdateInventoryCarCost", Conn);

                Cmd.Parameters.AddWithValue("@InventoryId", InventoryID);
                Cmd.Parameters.AddWithValue("@CarCost", Carcost);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteNonQuery();
                objDal.Dispose();
            }
            return Result;
        }

        public Int32 DeleteExpense(int ExpenseId, int UserId)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("DealerExpenses", Conn);

                Cmd.Parameters.AddWithValue("@ExpenseId", ExpenseId);
                Cmd.Parameters.AddWithValue("@UserId", UserId);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteNonQuery();
                objDal.Dispose();
            }
            return Result;
        }

        public DataTable GetVIN(string VIN, Int16 OrgID)
        {
            DataTable dTab = new DataTable("Expenses");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("GetInventoryIdforExpense", Conn);

                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public DataTable GetMobileExpenseDetailsbyExpenseId(int ExpenseID)
        {
            DataTable dTab = new DataTable("Expenses");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("GetExpDetailsbyExpenseId", Conn);

                Cmd.Parameters.AddWithValue("@ExpenseId", ExpenseID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }


        public DataTable GetEntities(int EntityTypeId, short OrgID)
        {
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("GetEntitiesbyEntityId", Conn);

                Cmd.Parameters.AddWithValue("@EntityTypeId", EntityTypeId);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        #region [Get Added by user agianst SecurityUserID]
        public List<SecurityUser> GetAddedByUser(Int32 EntityID, Int32 ParentEntityID, Int32 EntityTypeID )
        {
            List<SecurityUser> lst = new List<SecurityUser>();
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Expense_GetAddedByUser_ver211", Conn);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
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
                SU.SecurityUserID = Convert.ToInt32(dr["AddedBy"]);
                lst.Add(SU);
            }
            return lst;
        }

        #endregion

        #region [Get Added By User agianst SecurityUserID]
        public List<ExpenseType> GetExpenseType(Int32 EntityID, Int32 ParentEntityID, Int32 EntityTypeID)
        {
            List<ExpenseType> Lst = new List<ExpenseType>();
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("ExpenseType_Get", Conn);

                Cmd.Parameters.AddWithValue("@EntityTypeId", EntityTypeID);
                Cmd.Parameters.AddWithValue("@EntityId", EntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityId", ParentEntityID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
                foreach (DataRow dr in dTab.Rows)
                {
                    ExpenseType ext = new ExpenseType();
                    ext.ExpenseTypeId = Convert.ToInt32(dr["ExpenseTypeId"]);
                    ext.ExpenseType1 = dr["ExpenseType"].ToString();
                    Lst.Add(ext);
                }

            }
            return Lst;
        }

        #endregion

        public DataSet GetEntityNameExpenseAddedBy(Int16 OrgID)
        {
            DataSet ds = new DataSet();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("GetExpeseTypeAddedByEntityType", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(ds);
                objDal.Dispose();
            }
            return ds;
        }



        public DataSet GetEntityNameExpenseAddedBy_Ver211(Int32 EntityTypeId, Int32 SecurityUserId)
        {
            DataSet ds = new DataSet();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("GetExpeseTypeAddedByEntityType_Ver211", Conn);
                Cmd.Parameters.AddWithValue("@EntityTypeId", EntityTypeId);
                Cmd.Parameters.AddWithValue("@SecurityUserId", SecurityUserId);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(ds);
                objDal.Dispose();
            }
            return ds;
        }

        public DataTable GetEntities_Ver211(Int32 EntityTypeId, Int32 EntityUserId,Int32 BuyerUserId)
        {
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("GetEntitiesbyEntityId_Ver211", Conn);

                Cmd.Parameters.AddWithValue("@EntityTypeId", EntityTypeId);
                Cmd.Parameters.AddWithValue("@EntityId", EntityUserId);
                Cmd.Parameters.AddWithValue("@BuyerEntityId", BuyerUserId);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }


        public DataTable GetDirectBuyerEntities_Ver211(Int32 EntityUserId)
        {
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("GetEntitiesbyEntityId_Ver211_New", Conn);
                Cmd.Parameters.AddWithValue("@EntityUserId", EntityUserId);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }


        public DataTable ChildBuyerEntities_Ver211(Int32 ChildUserId,Int32 ParenBuyerId)
        {
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("GetChildBuyerEntityId_Ver211_New", Conn);
                Cmd.Parameters.AddWithValue("@UserEntityId", ChildUserId);
                Cmd.Parameters.AddWithValue("@ParentBuyerId", ParenBuyerId);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public Int32 UpdateHistoryTable(int ExpenseID, int UserId, decimal ExpenseAmount, int ExpenseType, string ExpenseDate, string Commnets, decimal ExpenseAmountNew, int ExpenseTypeNew, string ExpenseDateNew, string CommnetsNew, string invoiceNo, string invoiceNoNew, string Source)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("InsertViewAllExpEditHistory", Conn);

                Cmd.Parameters.AddWithValue("@ExpenseId", ExpenseID);
                Cmd.Parameters.AddWithValue("@UserId", UserId);
                Cmd.Parameters.AddWithValue("@Cost", ExpenseAmount);
                Cmd.Parameters.AddWithValue("@CostNew", ExpenseAmountNew);
                Cmd.Parameters.AddWithValue("@ExpenseType", ExpenseType);
                Cmd.Parameters.AddWithValue("@ExpenseTypeNew", ExpenseTypeNew);
                Cmd.Parameters.AddWithValue("@Expensedate", ExpenseDate);
                Cmd.Parameters.AddWithValue("@ExpensedateNew", ExpenseDateNew);
                Cmd.Parameters.AddWithValue("@Comments", Commnets);
                Cmd.Parameters.AddWithValue("@CommentsNew", CommnetsNew);
                Cmd.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
                Cmd.Parameters.AddWithValue("@InvoiceNoNew", invoiceNoNew);
                Cmd.Parameters.AddWithValue("@Source", Source);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteNonQuery();
                objDal.Dispose();
            }
            return Result;
        }

        public List<GetMobExpDetailsbyExpenseIdResult> PreExpenseDetailByPreExpenseId(long PreExpenseID)
        {
            DALDataContext objDal = new DALDataContext();
            return objDal.GetMobExpDetailsbyExpenseId(PreExpenseID).ToList<GetMobExpDetailsbyExpenseIdResult>();
        }
    }
}
