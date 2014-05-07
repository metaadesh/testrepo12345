using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using System.Data;
using METAOPTION.DAL;

namespace METAOPTION
{
    public class InventoryDepositSearchBAL
    {
        InventoryDepositSearchDAL objDAL = new InventoryDepositSearchDAL();
        public DataSet GetSearchFilters(Int16 OrgID)
        {
            DataSet ds = new DataSet();
            ds = objDAL.GetSearchFilters(OrgID);
            ds.Tables[0].TableName = "dtMake";
            ds.Tables[1].TableName = "dtAddedBy";

            return ds;
        }

        public DataTable GetDepositsDetails(String VIN, Int32 Year, Int32 MakeID, Int32 ModelID, Int32 BodyID, Double AmountFrom, Double AmountTo, String AddedBy, String DateFrom, String DateTo, String BuyerIDs, String Comment, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, Int16 OrgID)
        {
            return objDAL.GetDepositsDetails(VIN, Year, MakeID, ModelID, BodyID, AmountFrom, AmountTo, AddedBy, DateFrom, DateTo, BuyerIDs, Comment, StartRowIndex, MaximumRows, SortExpression, OrgID);
        }

        public Int32 GetDepositsDetailsCount(String VIN, Int32 Year, Int32 MakeID, Int32 ModelID, Int32 BodyID, Double AmountFrom, Double AmountTo, String AddedBy, String DateFrom, String DateTo, String BuyerIDs, String Comment, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, Int16 OrgID)
        {
            return objDAL.GetDepositsDetailsCount(VIN, Year, MakeID, ModelID, BodyID, AmountFrom, AmountTo, AddedBy, DateFrom, DateTo, BuyerIDs, Comment, OrgID);//, StartRowIndex, MaximumRows, SortExpression);
        }

        public DataTable GetDataTable(string strQuery)
        {
            return objDAL.GetDataTable(strQuery);
        }

        public DataTable GetDepositHistory(Int64 InventoryID, String ColumnName)
        {
            return objDAL.GetDepositHistory(InventoryID, ColumnName);
        }

        public DataTable GetInventoryDetail(Int64 InventoryID)
        {
            return objDAL.GetInventoryDetail(InventoryID);
        }

        public DataTable GetSortData(Int32 TableID, String ExcludeValues)
        {
            return objDAL.GetSortData(TableID, ExcludeValues);
        }

        public DataTable GetDepositSummary_Daily(String DateFrom, String DateTo, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, String SortDirection, Int16 OrgID)
        {
            return objDAL.GetDepositSummary_Daily(DateFrom, DateTo, StartRowIndex, MaximumRows, SortExpression, SortDirection, OrgID);
        }

        public Int32 GetDepositSummary_DailyCount(String DateFrom, String DateTo, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, String SortDirection, Int16 OrgID)
        {
            return objDAL.GetDepositSummary_DailyCount(DateFrom, DateTo, OrgID);//, StartRowIndex, MaximumRows, SortExpression);
        }

        public DataTable GetDepositSummaryDetail_Daily(String DepositDate, String SortExpression, String SortDirection, Int16 OrgID)
        {
            return objDAL.GetDepositSummaryDetail_Daily(DepositDate, SortExpression, SortDirection, OrgID);
        }

        public DataTable GetDepositSummary_Monthly(String DateFrom, String DateTo, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, String SortDirection, Int16 OrgID)
        {
            return objDAL.GetDepositSummary_Monthly(DateFrom, DateTo, StartRowIndex, MaximumRows, SortExpression, SortDirection, OrgID);
        }

        public Int32 GetDepositSummary_MonthlyCount(String DateFrom, String DateTo, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, String SortDirection, Int16 OrgID)
        {
            return objDAL.GetDepositSummary_MonthlyCount(DateFrom, DateTo, OrgID);//, StartRowIndex, MaximumRows, SortExpression);
        }

        public DataTable GetDepositSummaryDetail_Monthly(String DepositDateFrom, String DepositDateTo, String SortExpression, String SortDirection, Int16 OrgID)
        {
            return objDAL.GetDepositSummaryDetail_Monthly(DepositDateFrom, DepositDateTo, SortExpression, SortDirection, OrgID);
        }

    }
}
