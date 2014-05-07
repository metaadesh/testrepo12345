using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION;
using METAOPTION.DAL;
using System.Data;
using System.Collections;
using System.IO;
using System.Xml;

namespace METAOPTION
{
    public class UCRBAL
    {
        METAOPTION.UCRDAL objUCRDAL = new METAOPTION.UCRDAL();

        #region[UCRLog region]
        public DataTable GetUCRDetails(Int32 CRID, DateTime DateFrom, DateTime DateTo, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            return objUCRDAL.GetUCRDetails(CRID, DateFrom, DateTo, StartRowIndex, MaximumRows, SortExpression);
        }

        public Int32 GetUCRDetailsCount(Int32 CRID, DateTime DateFrom, DateTime DateTo, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            return objUCRDAL.GetUCRDetailsCount(CRID, DateFrom, DateTo, StartRowIndex, MaximumRows, SortExpression);
        }

        #endregion

        #region[UCRListing region]
        public DataTable GetUCRListing(Int32 CRID, Int32 UCRLogId, string VIN, string LaneNo, string RunNo, Int32 LegacyInventoryId, Int32 InventoryId, Int32 Year, string Make, string Model, string Body, DateTime DateFrom, DateTime DateTo, Int32 CRStatus, string AddedBy, string LinkedUCR, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            return objUCRDAL.GetUCRListing(CRID, UCRLogId, VIN, LaneNo, RunNo, LegacyInventoryId, InventoryId, Year, Make, Model, Body, DateFrom, DateTo, CRStatus, AddedBy, LinkedUCR, StartRowIndex, MaximumRows, SortExpression);
        }

        public Int32 GetUCRListingCount(Int32 CRID, Int32 UCRLogId, string VIN, string LaneNo, string RunNo, Int32 LegacyInventoryId, Int32 InventoryId, Int32 Year, string Make, string Model, string Body, DateTime DateFrom, DateTime DateTo, Int32 CRStatus, string AddedBy, string LinkedUCR, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            return objUCRDAL.GetUCRListingCount(CRID, UCRLogId, VIN, LaneNo, RunNo, LegacyInventoryId, InventoryId, Year, Make, Model, Body, DateFrom, DateTo, CRStatus, AddedBy, LinkedUCR, StartRowIndex, MaximumRows, SortExpression);
        }
        #endregion

        public DataSet GetYearMakeModelBody()
        {
            DataSet ds = new DataSet();
            ds = objUCRDAL.GetYearMakeModelBody();
            return ds;
        }

        #region[Expense list sort options]
        public DataTable UCRListing_SortOptions(String strQuery, String Sort1, String Sort2)
        {
            return objUCRDAL.UCRListing_SortOptions(strQuery, Sort1, Sort2);
        }
        #endregion

        //public DataTable GetUCRImagesByUCRID(long UCRID)
        //{
        //    return objUCRDAL.GetUCRImageDetails(UCRID);
        //}

        public List<UCRAudioVideoByUCRIdResult> GetAudioVideoByUCRID(long UCRID)
        {
            return objUCRDAL.GetAudioVideoByUCRID(UCRID);
        }

        public List<UCRImageDisplaybyUCRIdResult> GetUCRImagesByUCRID(long UCRID)
        {
            return objUCRDAL.GetUCRImagesByUCRID(UCRID);
        }

        public Int32 GetUCRImagesCount(Int32 UCRId)
        {
            return objUCRDAL.GetUCRImagesCount(UCRId);
        }
        public Int32 GetUCRAudioVideoCount(Int32 UCRId)
        {
            return objUCRDAL.GetUCRAudioVideoCount(UCRId);
        }

        #region [UCRImages, Added by Rupendra 14 Sep 12]
        public System.Collections.ArrayList UCRImagesByInvId(long InventoryId)
        {
            return objUCRDAL.UCRImagesByInvId(InventoryId);
        }
        public System.Collections.ArrayList UCRAudioVideoByInvId(long InventoryId)
        {
            return objUCRDAL.UCRAudioVideoByInvId(InventoryId);
        }
        #endregion

        public DataTable GetYMMBByVIN(string VIN)
        {
            return objUCRDAL.GetYMMBByVIN(VIN);
        }

        public DataTable GetUCRUpdateXML(int CRID)//, int InventoryId
        {
            return objUCRDAL.GetUCRUpdateXML(CRID);
        }

        public DataTable GetUCRLogPageDetails(Int64 UCRLogID)
        {
            return objUCRDAL.GetUCRLogPageDetails(UCRLogID);
        }
        public DataTable GetUCRLogPageDetailsbyUCRPageId(Int64 UCRPageID)
        {
            return objUCRDAL.GetUCRLogPageDetailsbyUCRPageId(UCRPageID);
        }
        public DataTable GetUCRResponseDetailsbyUCRId(Int64 UCRID)
        {
            return objUCRDAL.GetUCRResponseDetailsbyUCRId(UCRID);
        }

        public DataTable GetTestAudioVideoURL(Int64 UCRID)
        {
            return objUCRDAL.GetTestAudioVideoURL(UCRID);
        }

        #region[UCRUpdateLog 12 Oct 12]
        public DataTable GetUCRUpdateLog(Int32 CRID, String VIN, Int32 Year, string Make, string Model, String Body, DateTime DateFrom, DateTime DateTo, Int32 TranStatus, Int32 DataStatus, String Reference, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            return objUCRDAL.GetUCRUpdateLog(CRID, VIN, Year, Make, Model, Body, DateFrom, DateTo, TranStatus, DataStatus, Reference, StartRowIndex, MaximumRows, SortExpression);
        }

        public Int32 GetUCRUpdateCount(Int32 CRID, String VIN, Int32 Year, string Make, string Model, String Body, DateTime DateFrom, DateTime DateTo, Int32 TranStatus, Int32 DataStatus, String Reference, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            return objUCRDAL.GetUCRUpdateCount(CRID, VIN, Year, Make, Model, Body, DateFrom, DateTo, TranStatus, DataStatus, Reference, StartRowIndex, MaximumRows, SortExpression);
        }

        public DataTable GetUCRRequestByUCRUpdateID(Int64 UCRUpdateID, Int32 Type)
        {
            return objUCRDAL.GetUCRRequestByUCRUpdateID(UCRUpdateID, Type);
        }

        public DataSet GetUCRUpdateLogYMMB()
        {
            DataSet ds = new DataSet();
            ds = objUCRDAL.GetUCRUpdateLogYMMB();
            return ds;
        }
        #endregion

        #region[Added by Rupendra 17 OCT 12 for get UCR Image]
        public static int GetUCRImagesCountByInvId(long InventoryId)
        {
            UCRDAL objUCRDAL = new UCRDAL();
            return objUCRDAL.GetUCRImagesCountByInvId(InventoryId);
        }

        public IQueryable GetUCRImagesByInvID(long InventoryId, Int32 StartRow, Int32 MaximumRows)
        {
            return objUCRDAL.GetUCRImagesByInvID(InventoryId, StartRow, MaximumRows).AsQueryable();
        }
        #endregion
    }
}
