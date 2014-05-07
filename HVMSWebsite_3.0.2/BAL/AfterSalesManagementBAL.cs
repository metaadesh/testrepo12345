using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION.DAL;
using System.Collections;
using System.Data;

namespace METAOPTION.BAL
{
 public class AfterSalesManagementBAL
    {
     /// <summary>
      /// Get After Sales Data
      /// </summary>
      /// <param name="inventoryId"></param>
      /// <returns></returns>
     public static DataTable GetAfterSalesData(string regLaneNumber,string exoticLaneNumber, int soldStatus, int startRowIndex, int maximumRows, string sortExpression, string sortDirection,int SystemID, Int16 OrgID)
     {
         return AfterSalesManagementDAL.GetAfterSalesData(regLaneNumber, exoticLaneNumber, soldStatus, startRowIndex, maximumRows, sortExpression, sortDirection, SystemID, OrgID);
     }

      /// <summary>
      /// Get After Sales Data Record Count
      /// </summary>
      /// <param name="inventoryId"></param>
      /// <returns>After Sales Data Record Count</returns>
     public Int32? GetAfterSalesDataRecordCount(string regLaneNumber, string exoticLaneNumber, int soldStatus, int startRowIndex, int maximumRows, string sortExpression, string sortDirection, int SystemID, Int16 OrgID)
     {
         AfterSalesManagementDAL objDAL = new AfterSalesManagementDAL();
         return objDAL.GetAfterSalesDataRecordCount(regLaneNumber, exoticLaneNumber, soldStatus, startRowIndex, maximumRows, sortExpression, sortDirection, SystemID, OrgID);
     
     }

     /// <summary>
     /// Update After Sales Data
     /// </summary>
     /// <returns>0 if Success,999 for failure</returns>
     public static int UpdateAfterSalesData(long inventoryId, long customerId, DateTime? soldDate, decimal SoldPrice,
            long mileageOut, DateTime? depositDate, decimal depositAmount, int bankAccountId, string depositComment, int soldStatus, string soldComment,long modifiedby,DateTime modifiedDate)
     {

         return AfterSalesManagementDAL.UpdateAfterSalesData(inventoryId, customerId, soldDate, SoldPrice, mileageOut, depositDate, depositAmount,
              bankAccountId, depositComment, soldStatus, soldComment,modifiedby,modifiedDate);
     }
     /// <summary>
     /// Get after sales history details for inventoryid passed
     /// </summary>
     /// <param name="inventoryId"></param>
     /// <returns>after sales history details</returns>
     public static IEnumerable GetAfterSalesHistory(long inventoryId)
     {
         return AfterSalesManagementDAL.GetAfterSalesHistory(inventoryId);
     }
     
 }
}
