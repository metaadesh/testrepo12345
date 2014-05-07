using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace METAOPTION.DAL
{
    public class DocumentDAL
    {
        String ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HeadStartVMS_SystemConnectionString3"].ConnectionString;
        #region[Get All Document Type]
        public List<DocumentType> GetAllDocumentType()
        {
            DALDataContext objDal = new DALDataContext();
            List<DocumentType> result = (from d in objDal.DocumentTypes
                                         select d).OrderBy(d => d.DocumentType1).ToList<DocumentType>();
            return result;
        }
        #endregion

        #region[ Add document to database ]
        /// <summary>
        /// Add document to database 
        /// </summary>
        /// <param name="doc"> Document </param>
        /// <returns>Integer </returns>
        public Int64 Document_Add(Document doc)
        {

            long? docId = null;
            return METAOPTION.DALDataContext.Factory.DB.Document_Insert(ref docId
               , doc.EntityId
               , doc.EntityTypeId
               , doc.DocumentTypeId
               , doc.DocumentTitle
               , doc.DocumentName
               , doc.Description
               , doc.DocumentBinary
               , doc.FileType
               , doc.AddedBy);
        }

        public Int64 Document_Add_New(Document doc)
        {
            //DALDataContext objDal = new DALDataContext();
            Int64 Result = 0;

            using (SqlConnection Conn = new SqlConnection(ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Document_Insert", Conn);
                Cmd.Parameters.AddWithValue("@EntityId", doc.EntityId);
                Cmd.Parameters.AddWithValue("@EntityTypeId", doc.EntityTypeId);
                Cmd.Parameters.AddWithValue("@DocumentTypeId", doc.DocumentTypeId);
                Cmd.Parameters.AddWithValue("@DocumentTitle", doc.DocumentTitle);
                Cmd.Parameters.AddWithValue("@DocumentName", doc.DocumentName);
                Cmd.Parameters.AddWithValue("@Description", doc.Description);
                Cmd.Parameters.AddWithValue("@DocumentBinary", doc.DocumentBinary);
                Cmd.Parameters.AddWithValue("@FileType", doc.FileType);
                Cmd.Parameters.AddWithValue("@AddedBy", doc.AddedBy);

                SqlParameter ouparm = Cmd.Parameters.Add("@DocumentId", SqlDbType.BigInt);
                ouparm.Direction = ParameterDirection.Output;

                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.ExecuteNonQuery();
                Result = Convert.ToInt32(Cmd.Parameters["@DocumentId"].Value);

            }
            return Result;
        }
        #endregion

        #region[Added By Rupendra 26 Oct 12]
        public DataTable GetDocumentDetails(String VIN, String EntityId, String EntityTypeId, String DocumentTypeId, String AddedBy, String FileType, String DocName, String DocDescription, DateTime DateFrom, DateTime DateTo, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, short OrgID)
        {
            DataTable dTab = new DataTable();

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Documnet_SelectDocument", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@EntityId", EntityId);
                Cmd.Parameters.AddWithValue("@EntiTypeId", EntityTypeId);
                Cmd.Parameters.AddWithValue("@DocumentTypeId", DocumentTypeId);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@FileType", FileType);
                Cmd.Parameters.AddWithValue("@DocName", DocName);
                Cmd.Parameters.AddWithValue("@DocDescription", DocDescription);
                Cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                Cmd.Parameters.AddWithValue("@DateTo", DateTo);
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

        public Int32 GetDocumentDetailsCount(String VIN, String EntityId, String EntityTypeId, String DocumentTypeId, String AddedBy, String FileType, String DocName, String DocDescription, DateTime DateFrom, DateTime DateTo, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, short OrgID)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Documnet_SelectDocumentCount", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@EntityId", EntityId);
                Cmd.Parameters.AddWithValue("@EntiTypeId", EntityTypeId);
                Cmd.Parameters.AddWithValue("@DocumentTypeId", DocumentTypeId);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@FileType", FileType);
                Cmd.Parameters.AddWithValue("@DocName", DocName);
                Cmd.Parameters.AddWithValue("@DocDescription", DocDescription);
                Cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                Cmd.Parameters.AddWithValue("@DateTo", DateTo);
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

        public DataSet GetDocumentFilters(Int16 OrgID)
        {
            DataSet ds = new DataSet();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("Documnet_GetDoumnetFilterData", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(ds);
                objDal.Dispose();
            }
            return ds;
        }

        public DataTable GetDocumentBinarybyDocId(Int64 DocumentID)
        {
            DataTable dTab = new DataTable();

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Document_GetDocumentDatabyDocumentId", Conn);
                Cmd.Parameters.AddWithValue("@DocumentId", DocumentID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public DataTable DocumentListing_SortOptions(String strQuery, String Sort1, String Sort2)
        {
            DataTable dTab = new DataTable("SortUCRListing");
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


        public DataTable BindTopFiveDocument(Int32 InventoryId, Int16 OrgID)
        {
            DataTable dTab = new DataTable();

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Document_GetTopFiveDocument", Conn);
                Cmd.Parameters.AddWithValue("@EntityId", InventoryId);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public Int32 DeleteDocument(Int32 DocumentId)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("Document_DeleteByDocumentId", Conn);
                Cmd.Parameters.AddWithValue("@DocumentId", DocumentId);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteNonQuery();
                objDal.Dispose();
            }
            return Result;
        }

        public Inventory GetVINByInventoryId(Int32 InventoryId, Int16 OrgID)
        {
            DALDataContext objDal = new DALDataContext();
            Inventory result = (from p in objDal.Inventories
                                where p.InventoryId == InventoryId && p.OrgID == OrgID
                                select p).FirstOrDefault();
            return result;
        }


        public DataTable GetYearMakeByInventoryId(Int32 InventoryId)
        {
            DataTable dTab = new DataTable();

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("GetYMByInventoryID", Conn);
                Cmd.Parameters.AddWithValue("@InventoryId", InventoryId);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }



        public DataTable CheckFileTitleName(int type, String DocumentTitle, Int32 EntityId,Int16 OrgID)
        {
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("Document_CheckFileTitleName", Conn);
                Cmd.Parameters.AddWithValue("@Type", type);
                Cmd.Parameters.AddWithValue("@DocumentTitle", DocumentTitle);
                Cmd.Parameters.AddWithValue("@EntityId", EntityId);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }



        public DataTable GetYMMBbyInventoryId(Int32 InventoryId)
        {
            DataTable dTab = new DataTable();

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("Document_GetYMMBbyInventoryId", Conn);
                Cmd.Parameters.AddWithValue("@InventoryId", InventoryId);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }
        #endregion

        public DataTable GetYearMakeByVIN(String VIN, Int32 VINMatch, short OrgId)
        {
            DataTable dTab = new DataTable();

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("GetYMByVIN", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@VINMatch", VINMatch);
                Cmd.Parameters.AddWithValue("@OrgID", OrgId);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

    }
}
