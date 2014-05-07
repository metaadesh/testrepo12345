using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using METAOPTION;
using METAOPTION.DAL;

namespace METAOPTION.BAL
{
    public class AppEnum
    {
        //ExpenseLog Enum
        public enum ExpenseLog
        {
            ExpenseApproved = 1,
            ExpenseDeleted = 2,
            ExpenseRejected = 3
        }
    }
}
