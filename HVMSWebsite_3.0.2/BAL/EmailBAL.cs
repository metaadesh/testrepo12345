using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using METAOPTION.DAL;

namespace METAOPTION
{
    public class EmailBAL
    {
        METAOPTION.EmailDAL objdal = new METAOPTION.EmailDAL();

        public DataTable GetEmailDetail(Int32 MasterLookUpID, long RefKey)
        {
            return objdal.GetEmailDetail(MasterLookUpID,RefKey);
        }

        public DataTable GetSMSDetail(Int32 MasterLookUpID, long RefKey)
        {
            return objdal.GetSMSDetail(MasterLookUpID,RefKey);
        }
    }
}
