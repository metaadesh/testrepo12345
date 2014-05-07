using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace METAOPTION.DAL
{
  public  class AfterSalesManagementDAL
    {
      /// <summary>
      /// Get After Sales Data
      /// </summary>
      /// <param name="inventoryId"></param>
      /// <returns></returns>
      public static DataTable GetAfterSalesData(string regLaneNumber, string exoticLaneNumber, int soldStatus, int startRowIndex, int maximumRows, string sortExpression, string sortDirection, int SystemID, Int16 OrgID)
      {
           DataTable dTab= new DataTable();
          //HttpContext context = HttpContext.Current;
          //if (context.Cache["spInventorySearchCount"] == null)
          //{

          DALDataContext objDal = new DALDataContext();
          using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
          {
              if (Conn.State == ConnectionState.Closed)
                  Conn.Open();

              SqlCommand Cmd = new SqlCommand("GetAfterSalesInventories", Conn);
              Cmd.Parameters.AddWithValue("@RegularLane", regLaneNumber);
              Cmd.Parameters.AddWithValue("@ExoticLane", exoticLaneNumber);
              Cmd.Parameters.AddWithValue("@SoldStatus", soldStatus);
              Cmd.Parameters.AddWithValue("@StartRowIndex", startRowIndex);
              Cmd.Parameters.AddWithValue("@MaximumRows", maximumRows);
              Cmd.Parameters.AddWithValue("@sortExpression", sortExpression);
              Cmd.Parameters.AddWithValue("@sortDirection", sortDirection);
              Cmd.Parameters.AddWithValue("@SystemID", SystemID);
              Cmd.Parameters.AddWithValue("@OrgID", OrgID);
              Cmd.CommandType = CommandType.StoredProcedure;
              SqlDataReader dr = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
              dTab.Load(dr);
              objDal.Dispose();           
          }
          return dTab;


          //return METAOPTION.DALDataContext.Factory.DB.GetAfterSalesInventories(regLaneNumber, soldStatus, startRowIndex, maximumRows, sortExpression, sortDirection).AsEnumerable();
     
      }

      /// <summary>
      /// Get After Sales Data Record Count
      /// </summary>
      /// <param name="inventoryId"></param>
      /// <returns>After Sales Data Record Count</returns>
      public Int32? GetAfterSalesDataRecordCount(string regLaneNumber, string exoticLaneNumber, int soldStatus, int startRowIndex, int maximumRows, string sortExpression, string sortDirection, int SystemID, Int16 OrgID)
      {
          GetAfterSalesInventoriesRecordsCountResult result = METAOPTION.DALDataContext.Factory.DB.GetAfterSalesInventoriesRecordsCount(regLaneNumber, exoticLaneNumber, soldStatus, startRowIndex, maximumRows, sortExpression, sortDirection, SystemID, OrgID).Single();
         return result.AfterSalesRecordsCount;
      }


      /// <summary>
      /// Update After Sales Data
      /// </summary>
      /// <returns>0 if Success,999 for failure</returns>
      public static int UpdateAfterSalesData(long inventoryId, long customerId,DateTime? soldDate,decimal SoldPrice,
             long  mileageOut, DateTime? depositDate, decimal depositAmount,int bankAccountId, string depositComment,int soldStatus, string soldComment,long modifiedby,DateTime modifiedDate)
      {
          return METAOPTION.DALDataContext.Factory.DB.UpdateAfterSalesInventories(inventoryId, customerId, soldDate, SoldPrice,
              mileageOut, depositDate, depositAmount, bankAccountId, depositComment, soldStatus, soldComment, modifiedby, modifiedDate);
      }


      /// <summary>
      /// Get After Sales History Details based on inventorid provided
      /// </summary>
      /// <returns>return after sales history</returns>
      public static IEnumerable GetAfterSalesHistory(long inventoryId)
      {
          return METAOPTION.DALDataContext.Factory.DB.GetAfterSalesUIHistory(inventoryId).AsEnumerable();
      }
    }
}
