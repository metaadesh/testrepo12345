using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION.DAL;
using System.Data;

namespace METAOPTION
{
    public class DocumentBAL
    {
        DocumentDAL docDAL = new DocumentDAL();

        #region[Get All Document Type]
        public List<DocumentType> GetAllDocumentType()
        {
            return docDAL.GetAllDocumentType();
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
            return docDAL.Document_Add(doc);
        }
        #endregion

        #region [Added by Rupendra 26 Oct 12]
        public DataTable GetDocumentDetails(String VIN, String EntityId, String EntityTypeId, String DocumentTypeId, String AddedBy, String FileType, String DocName, String DocDescription, DateTime DateFrom, DateTime DateTo, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, short OrgID)
        {
            return docDAL.GetDocumentDetails(VIN, EntityId, EntityTypeId, DocumentTypeId, AddedBy, FileType, DocName, DocDescription, DateFrom, DateTo, StartRowIndex, MaximumRows, SortExpression, OrgID);
        }

        public Int32 GetDocumentDetailsCount(String VIN, String EntityId, String EntityTypeId, String DocumentTypeId, String AddedBy, String FileType, String DocName, String DocDescription, DateTime DateFrom, DateTime DateTo, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, short OrgID)
        {
            return docDAL.GetDocumentDetailsCount(VIN, EntityId, EntityTypeId, DocumentTypeId, AddedBy, FileType, DocName, DocDescription, DateFrom, DateTo, StartRowIndex, MaximumRows, SortExpression, OrgID);
        }

        public DataSet GetDocumentFilters(Int16 OrgID)
        {
            DataSet ds = new DataSet();
            ds = docDAL.GetDocumentFilters(OrgID);
            return ds;
        }

        public DataTable GetDocumentBinarybyDocId(Int64 DocumentID)
        {
            return docDAL.GetDocumentBinarybyDocId(DocumentID);
        }

        #region[Expense list sort options]
        public DataTable DocumentListing_SortOptions(String strQuery, String Sort1, String Sort2)
        {
            return docDAL.DocumentListing_SortOptions(strQuery, Sort1, Sort2);
        }
        #endregion


        public DataTable BindTopFiveDocument(Int32 InventoryId, Int16 OrgID)
        {
            return docDAL.BindTopFiveDocument(InventoryId, OrgID);
        }

        public Int32 DeleteDocument(Int32 DocumentId)
        {
            return docDAL.DeleteDocument(DocumentId);
        }

        public Inventory GetVINByInventoryId(Int32 InventoryId, Int16 OrgID)
        {
            return docDAL.GetVINByInventoryId(InventoryId, OrgID);
        }

        public DataTable GetYearMakeByInventoryId(Int32 InventoryId)
        {
            return docDAL.GetYearMakeByInventoryId(InventoryId);
        }


        public DataTable CheckFileTitleName(int type, String DocumentTitle, Int32 EntityId, Int16 OrgID)
        {
            return docDAL.CheckFileTitleName(type, DocumentTitle, EntityId, OrgID);
        }

        public DataTable GetYMMBbyInventoryId(Int32 InventoryId)
        {
            return docDAL.GetYMMBbyInventoryId(InventoryId);
        }
        #endregion

        public DataTable GetYearMakeByVIN(String VIN, Int32 VINMatch, short OrgId)
        {
            return docDAL.GetYearMakeByVIN(VIN, VINMatch, OrgId);
        }
    }
}
