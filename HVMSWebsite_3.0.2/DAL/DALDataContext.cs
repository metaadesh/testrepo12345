using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace METAOPTION
{
   partial class DALDataContext
   {
      partial void OnCreated()
      {
         this.Connection.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBMetaoptionConnection"].ToString();
         this.CommandTimeout = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
      }

      /// <summary>
      /// This Class returns DataContext object for performing db Operations
      /// </summary>
      public class Factory
      {
          public static DALDataContext DB
          {
              get
              {
                 DALDataContext  db = null;
                  if (db == null)
                      db = new DALDataContext();

                  return db;
              }
          }
      }

   }
}
