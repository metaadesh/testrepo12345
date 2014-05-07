using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION.DAL;
using System.Collections;
using System.Data;

namespace METAOPTION.BAL
{
    public class MAAAfterSalesBAL
    {
        /// <summary>
        /// Get MAA After Sales Data
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public static DataTable GetMAAAfterSalesData(string regLaneNumber, string exoticLaneNumber, int soldStatus, int startRowIndex, int maximumRows, string sortExpression, string sortDirection, bool isMAASearch,int SystemID,short OrgID)
        {
            return MAAAfterSalesDAL.Get_MAAAfterSalesData(regLaneNumber, exoticLaneNumber, soldStatus, startRowIndex, maximumRows, sortExpression, sortDirection, isMAASearch, SystemID, OrgID);
        }

        /// <summary>
        /// Get MAA After Sales Data Record Count
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns>After Sales Data Record Count</returns>
        public Int32? GetMAAAfterSalesDataRecordCount(string regLaneNumber, string exoticLaneNumber, int soldStatus, int startRowIndex, int maximumRows, string sortExpression, string sortDirection, bool isMAASearch, int SystemID, Int16 OrgID)
        {
            MAAAfterSalesDAL objDAL = new MAAAfterSalesDAL();
            return objDAL.Get_MAAAfterSalesDataRecordCount(regLaneNumber, exoticLaneNumber, soldStatus, startRowIndex, maximumRows, sortExpression, sortDirection, isMAASearch, SystemID, OrgID);
        }

        /// <summary>
        /// Update After Sales Data
        /// </summary>
        /// <returns>0 if Success,999 for failure</returns>
        public static int UpdateMAAAfterSalesData(long inventoryId, long customerId, DateTime? soldDate, decimal SoldPrice,
               long mileageOut, DateTime? depositDate, decimal depositAmount, int bankAccountId, string depositComment, int soldStatus, string soldComment, long modifiedby, DateTime modifiedDate)
        {

            return MAAAfterSalesDAL.UpdateMAAAfterSalesData(inventoryId, customerId, soldDate, SoldPrice, mileageOut, depositDate, depositAmount,
                 bankAccountId, depositComment, soldStatus, soldComment, modifiedby, modifiedDate);
        }
        /// <summary>
        /// Get MAA After sales history details for inventoryid passed
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns>after sales history details</returns>
        public static IEnumerable GetMAAAfterSalesHistory(long inventoryId)
        {
            return MAAAfterSalesDAL.GetMAAAfterSalesHistory(inventoryId);
        }

        #region[Check if MAA is allowed for the organization]
        public bool IsMAAAllowed(short OrgID)
        {
            MAAAfterSalesDAL objDal = new MAAAfterSalesDAL();
            return objDal.IsMAAAllowed(OrgID);
        }
        #endregion

    }
}
