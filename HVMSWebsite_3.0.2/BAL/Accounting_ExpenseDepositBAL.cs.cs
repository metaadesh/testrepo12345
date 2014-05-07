using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using METAOPTION.DAL;
using System.Collections;
using System.Data;

namespace METAOPTION.BAL
{
    public class Accounting_ExpenseDepositBAL
    {
        public static DataTable GetFinanceReportData(DateTime FromDate, DateTime ToDate, String Filter, int startRowIndex, int maximumRows)
        {
           return Accounting_ExpenseDepositDAL.GetFinanceReportData(FromDate, ToDate, Filter,startRowIndex,maximumRows);
        }

        public static Int32 GetFinanceReportDataCount(DateTime FromDate, DateTime ToDate, String Filter, int startRowIndex, int maximumRows)
        {
            return Accounting_ExpenseDepositDAL.GetFinanceReportDataCount(FromDate, ToDate, Filter, startRowIndex, maximumRows);
        }

    }
}
