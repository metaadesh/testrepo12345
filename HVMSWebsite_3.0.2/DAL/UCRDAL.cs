using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using METAOPTION;

namespace METAOPTION
{
    public class UCRDAL
    {

        #region[UCRLog region]
        public DataTable GetUCRDetails(Int32 CRID, DateTime DateFrom, DateTime DateTo, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            DataTable dTab = new DataTable();

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("GetUCRLogDetails", Conn);
                Cmd.Parameters.AddWithValue("@CRID", CRID);
                Cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                Cmd.Parameters.AddWithValue("@DateTo", DateTo);
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

        public Int32 GetUCRDetailsCount(Int32 CRID, DateTime DateFrom, DateTime DateTo, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("GetUCRLogDetailsCount", Conn);
                Cmd.Parameters.AddWithValue("@CRID", CRID);
                Cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                Cmd.Parameters.AddWithValue("@DateTo", DateTo);
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

        #region[UCRListing region]
        public DataTable GetUCRListing(Int32 CRID, Int32 UCRLogId, string VIN, string LaneNo, string RunNo, Int32 LegacyInventoryId, Int32 InventoryId, Int32 Year, string Make, string Model, string Body, DateTime DateFrom, DateTime DateTo, Int32 CRStatus, string  AddedBy, string LinkedUCR, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            DataTable dTab = new DataTable();

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("GetUCRLogListing", Conn);
                Cmd.Parameters.AddWithValue("@CRID", CRID);
                Cmd.Parameters.AddWithValue("@UCRLogId", UCRLogId);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@Year", Year);
                Cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                Cmd.Parameters.AddWithValue("@DateTo", DateTo);
                Cmd.Parameters.AddWithValue("@Make", Make);
                Cmd.Parameters.AddWithValue("@Model", Model);
                Cmd.Parameters.AddWithValue("@Body", Body);
                Cmd.Parameters.AddWithValue("@CRStatus", CRStatus);
                Cmd.Parameters.AddWithValue("@LaneNo", LaneNo);
                Cmd.Parameters.AddWithValue("@RunNO", RunNo);
                Cmd.Parameters.AddWithValue("@LegacyInventoryId", LegacyInventoryId);
                Cmd.Parameters.AddWithValue("@InventoryId", InventoryId);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@UCRLinked", LinkedUCR);
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

        public Int32 GetUCRListingCount(Int32 CRID, Int32 UCRLogId, string VIN, string LaneNo, string RunNo, Int32 LegacyInventoryId, Int32 InventoryId, Int32 Year, string Make, string Model, string Body, DateTime DateFrom, DateTime DateTo, Int32 CRStatus, string AddedBy, string LinkedUCR, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("GetUCRLogListingCount", Conn);
                Cmd.Parameters.AddWithValue("@CRID", CRID);
                Cmd.Parameters.AddWithValue("@UCRLogId", UCRLogId);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@Year", Year);
                Cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                Cmd.Parameters.AddWithValue("@DateTo", DateTo);
                Cmd.Parameters.AddWithValue("@Make", Make);
                Cmd.Parameters.AddWithValue("@Model", Model);
                Cmd.Parameters.AddWithValue("@Body", Body);
                Cmd.Parameters.AddWithValue("@CRStatus", CRStatus);
                Cmd.Parameters.AddWithValue("@LaneNo", LaneNo);
                Cmd.Parameters.AddWithValue("@RunNO", RunNo);
                Cmd.Parameters.AddWithValue("@LegacyInventoryId", LegacyInventoryId);
                Cmd.Parameters.AddWithValue("@InventoryId", InventoryId);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@UCRLinked", LinkedUCR);
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

        public DataSet GetYearMakeModelBody()
        {
            DataSet ds = new DataSet("UCRListing");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("GetUCRYearMakeModelBody", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(ds);
                objDal.Dispose();
            }
            return ds;
        }

        #region[Expense list sort options]
        public DataTable UCRListing_SortOptions(String strQuery, String Sort1, String Sort2)
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
        #endregion

        public DataTable GetUCRImageDetails(long UCRID)
        {
            DataTable dTab = new DataTable();

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("UCRImageDisplaybyUCRId", Conn);
                Cmd.Parameters.AddWithValue("@UCRID", UCRID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public List<UCRAudioVideoByUCRIdResult> GetAudioVideoByUCRID(long UCRID)
        {
            DALDataContext objDal = new DALDataContext();
            return objDal.UCRAudioVideoByUCRId(UCRID).ToList<UCRAudioVideoByUCRIdResult>();
        }

        public List<UCRImageDisplaybyUCRIdResult> GetUCRImagesByUCRID(long UCRID)
        {
            DALDataContext objDal = new DALDataContext();
            return objDal.UCRImageDisplaybyUCRId(UCRID).ToList<UCRImageDisplaybyUCRIdResult>();
        }

        public Int32 GetUCRImagesCount(Int32 UCRID)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("UCRImageDisplaybyUCRIdCount", Conn);
                Cmd.Parameters.AddWithValue("@UCRID", UCRID);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }

        public Int32 GetUCRAudioVideoCount(Int32 UCRID)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("UCRAudioVideoByUCRIdCount", Conn);
                Cmd.Parameters.AddWithValue("@UCRID", UCRID);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }

        #region [UCR Images]
        public System.Collections.ArrayList UCRImagesByInvId(long InventoryId)
        {
            DataTable dTab = new DataTable();
            System.Collections.ArrayList array = new System.Collections.ArrayList();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("UCRImagesPathByInventoryId", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@InventoryId", InventoryId);

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

        public System.Collections.ArrayList UCRAudioVideoByInvId(long InventoryId)
        {
            DataTable dTab = new DataTable();
            System.Collections.ArrayList array = new System.Collections.ArrayList();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("GetAudioVideoByInventoryId", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@InventoryId", InventoryId);

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

        public DataTable GetYMMBByVIN(String VIN)
        {
            DataTable dTab = new DataTable();

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("GetYMMBByVIN", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public DataTable GetUCRUpdateXML(int CRID)//, int InventoryId
        {
            DataTable dTab = new DataTable();

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("GetUCRUpdateXML", Conn);
                Cmd.Parameters.AddWithValue("@CRID", CRID);
                //Cmd.Parameters.AddWithValue("@InventoryId", InventoryId);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }


        public DataTable GetUCRLogPageDetails(Int64 UCRLogID)
        {
            DataTable dTab = new DataTable();

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("GetUCRLogPageDetails", Conn);
                Cmd.Parameters.AddWithValue("@UCRLogId", UCRLogID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public DataTable GetUCRLogPageDetailsbyUCRPageId(Int64 UCRPageID)
        {
            DataTable dTab = new DataTable();

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("GetUCRLogPageDetailsByUCRPageId", Conn);
                Cmd.Parameters.AddWithValue("@UCRPageID", UCRPageID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }


        public DataTable GetUCRResponseDetailsbyUCRId(Int64 UCRID)
        {
            DataTable dTab = new DataTable();

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("GetUCRResponseDetailsByUCRId", Conn);
                Cmd.Parameters.AddWithValue("@UCRId", UCRID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }


        public DataTable GetTestAudioVideoURL(Int64 UCRID)
        {
            DataTable dTab = new DataTable();

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("UCRAudioVideoByUCRId", Conn);
                Cmd.Parameters.AddWithValue("@UCRId", UCRID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        #region[UCRUpdateLog 12 Oct 12]

        public DataTable GetUCRUpdateLog(Int32 CRID, String VIN, Int32 Year, string Make, string Model, String Body, DateTime DateFrom, DateTime DateTo, Int32 TranStatus, Int32 DataStatus, String Reference, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            DataTable dTab = new DataTable();

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("GetUCRUpdateLog", Conn);
                Cmd.Parameters.AddWithValue("@CRID", CRID);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@Year", Year);
                Cmd.Parameters.AddWithValue("@MakeId", Make);
                Cmd.Parameters.AddWithValue("@ModelId", Model);
                Cmd.Parameters.AddWithValue("@BodyId", Body);
                Cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                Cmd.Parameters.AddWithValue("@DateTo", DateTo);
                Cmd.Parameters.AddWithValue("@TransactionStatus", TranStatus);
                Cmd.Parameters.AddWithValue("@DataStatus", DataStatus);
                Cmd.Parameters.AddWithValue("@Reference", Reference);
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

        public Int32 GetUCRUpdateCount(Int32 CRID, String VIN, Int32 Year, string Make, string Model, String Body, DateTime DateFrom, DateTime DateTo, Int32 TranStatus, Int32 DataStatus, String Reference, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            Int32 Result = 0;
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("GetUCRUpdateLogCount", Conn);
                Cmd.Parameters.AddWithValue("@CRID", CRID);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@Year", Year);
                Cmd.Parameters.AddWithValue("@MakeId", Make);
                Cmd.Parameters.AddWithValue("@ModelId", Model);
                Cmd.Parameters.AddWithValue("@BodyId", Body);
                Cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
                Cmd.Parameters.AddWithValue("@DateTo", DateTo);
                Cmd.Parameters.AddWithValue("@TransactionStatus", TranStatus);
                Cmd.Parameters.AddWithValue("@DataStatus", DataStatus);
                Cmd.Parameters.AddWithValue("@Reference", Reference);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@sortExpression", SortExpression);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }

        public DataTable GetUCRRequestByUCRUpdateID(Int64 UCRUpdateID, Int32 Type)
        {
            DataTable dTab = new DataTable();

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("GetUCRRequestDetailsByUCRUpdateID", Conn);
                Cmd.Parameters.AddWithValue("@UCRUpdateID", UCRUpdateID);
                Cmd.Parameters.AddWithValue("@Type", Type);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public DataSet GetUCRUpdateLogYMMB()
        {
            DataSet ds = new DataSet("UCRUpdateLog");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                SqlCommand Cmd = new SqlCommand("GetUCRUpdateLogYMMB", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(ds);
                objDal.Dispose();
            }
            return ds;
        }
        #endregion

        #region[Added by Rupendra 17 OCT 12 for get UCR Image Show on Inventory Details Page]
        public int GetUCRImagesCountByInvId(Int64 InventoryId)
        {
            List<UCRImagesCountByInvIdResult> Result = METAOPTION.DALDataContext.Factory.DB.UCRImagesCountByInvId(InventoryId).ToList();
            if (Result.Count > 0)
                return Result.First().Total.Value;
            else return 0;
        }
        public IQueryable GetUCRImagesByInvID(Int64 InventoryId, Int32 StartRow, Int32 MaximumRows)
        {
            DALDataContext objDAL = new DALDataContext();
            return objDAL.UCRImagesByInvID(InventoryId, StartRow, MaximumRows).AsQueryable();
        }
        #endregion
    }
}
