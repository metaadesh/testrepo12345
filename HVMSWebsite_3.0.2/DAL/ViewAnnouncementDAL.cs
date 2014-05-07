using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using METAOPTION;
namespace METAOPTION.DAL
{
    public class ViewAnnouncementDAL
    {

        #region [To Get Announce ment by id]
        /// <summary>
        /// To Get Announce ment by id
        /// </summary>
        /// <param name="AnnouncementId"></param>
        /// <returns></returns>
        public IQueryable GetAnnouncement(Int64 AnnouncementId)
        {
            DALDataContext objDAL = new DALDataContext();
            IQueryable query = objDAL.GetAnnouncement(AnnouncementId).AsQueryable();
            return query;
        }
        #endregion

        #region[ Select Announcement List ]
        /// <summary>
        /// Select Announcement List 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAnnouncementList(Int16 OrgID)
        {
            DALDataContext objDAL = new DALDataContext();
            DataTable dTab = new DataTable("AnnouncementList");
            using (SqlConnection Conn = new SqlConnection(objDAL.Connection.ConnectionString))
            {
                //Modified By Prem(8800128549) on 27-Nov-2013, Change the Query into SP and Add the logic for multi organization
                //String sqlStatement = "SELECT distinct top(5)  vwCurrentAnnouncementList.*  FROM vwCurrentAnnouncementList  ORDER BY DateAdded DESC ";

                try
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();
                    SqlCommand CMD = new SqlCommand("Announcement_Select", Conn);
                    CMD.CommandType = CommandType.StoredProcedure;
                    CMD.Parameters.AddWithValue("@OrgID", OrgID);
                    SqlDataReader dReader = CMD.ExecuteReader(CommandBehavior.CloseConnection);
                    dTab.Load(dReader);
                    dReader.Close();
                }
                catch (Exception ex)
                {

                }
            }
            return dTab;
        }
        #endregion

        #region[Added by Rupendra 18 Jan 13 for fetch Buyer Announcement]
        public static DataTable GetAnnouncementList_Buyer_Ver211(Int32 SecurityUserId)
        {
            DALDataContext objDAL = new DALDataContext();
            DataTable dTab = new DataTable("AnnouncementList");
            using (SqlConnection Conn = new SqlConnection(objDAL.Connection.ConnectionString))
            {

                String sqlStatement = "SELECT distinct top(5)  vwCurrentAnnouncementList.*  FROM vwCurrentAnnouncementList where SecurityUserID = " + SecurityUserId + "  ORDER BY DateAdded DESC ";

                try
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();

                    SqlCommand CMD = new SqlCommand(sqlStatement, Conn);
                    CMD.CommandType = CommandType.Text;
                    SqlDataReader dReader = CMD.ExecuteReader(CommandBehavior.CloseConnection);

                    dTab.Load(dReader);
                    dReader.Close();
                }
                catch (Exception ex)
                {

                }
            }
            return dTab;
        }
        #endregion
    }
}
