using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Collections;

namespace METAOPTION.DAL
{
  public class ValidateVinDAL
    {
      DALDataContext objDAL = new DALDataContext();
      /// <summary>
      /// Return list of matched VinNo
      /// </summary>
      /// <param name="strVinNo"></param>
      /// <returns>Return list of matched VinNo</returns>
      public IQueryable GetMatchedVinNumbers (string strVinNo)
      { 
   
          IQueryable result=
         objDAL.GetMatchedVinNumbers(strVinNo).AsQueryable();
         return result;
      }
    }
}
