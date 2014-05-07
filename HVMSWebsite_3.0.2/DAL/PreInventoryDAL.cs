using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using METAOPTION;


namespace METAOPTION.DAL
{
    public class PreInventoryDAL
    {
        DALDataContext objDAL = new DALDataContext();


        #region[Get Inventory Images]
        public DataTable InventoryImages_ByInventoryId(long PreInventoryID)
        {
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_PreInventoryImagesPath", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@PreInventoryId", PreInventoryID);

                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;

        }

        public System.Collections.ArrayList PreInventoryImages(long PreInventoryID)
        {
            DataTable dTab = new DataTable();
            System.Collections.ArrayList array = new System.Collections.ArrayList();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_PreInventoryImagesPath", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@PreInventoryId", PreInventoryID);

                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                array.Add(dTab);

                dTab = new DataTable();
                dTab.Load(reader);
                array.Add(dTab);

                reader.Close();
                objDal.Dispose();
            }
            return array;

        }

        public bool isInventoryExists(long PreInventoryID)
        {
            DALDataContext objDal = new DALDataContext();
            string vin = string.Empty;
            var result = from v in objDal.Mobile_PreInventories
                         where v.PreInventoryId == PreInventoryID
                         select new { v.VIN };

            if (result != null)
            {
                vin = result.FirstOrDefault().VIN;
            }
            if (objDal.Inventories.Where(i => i.VIN == vin).Any())
                return true;
            else
                return false;
        }

        public Mobile_PreInventory GetPreInventoryByID(long PreInventoryID)
        {
            DALDataContext objDal = new DALDataContext();
            Mobile_PreInventory preinv = (from p in objDal.Mobile_PreInventories
                                          where p.PreInventoryId == PreInventoryID
                                          select p).SingleOrDefault();
            return preinv;

        }

        public String GetDealerName(long DealerID)
        {
            DALDataContext objDal = new DALDataContext();
            String DealerName = string.Empty;
            var result = (from d in objDal.Dealers
                          where d.DealerId == DealerID
                          select new { d.DealerName }).ToList();
            if (result != null && result.Count > 0)
                DealerName = result.FirstOrDefault().DealerName;
            return DealerName;
        }

        //public void UpdatePreInvbyInvID(long PreInvID,long InvID)
        //{
        //    DALDataContext objDal = new DALDataContext();
        //    string qry = String.Format("Update Mobile_PreInventory set InventoryId={1},IsPending=0 where PreInventoryId={0}",PreInvID,InvID);
        //    objDal.ExecuteQuery<String>(qry);
        //}
        #endregion

        public DataTable SearchPreInventory(String VIN, Int32 Year, long MakeID, long ModelID, long BodyID, String Dealer, long BuyerID, Int32 Status, long AddedBy, Int32 CRStatus, Int32 Period, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            DataTable dTab = new DataTable("Inventories");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_PreInventorySearch", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@Year", Year);
                Cmd.Parameters.AddWithValue("@MakeID", MakeID);
                Cmd.Parameters.AddWithValue("@ModelID", ModelID);
                Cmd.Parameters.AddWithValue("@BodyID", BodyID);
                Cmd.Parameters.AddWithValue("@Dealer", Dealer);
                Cmd.Parameters.AddWithValue("@BuyerID", BuyerID);
                Cmd.Parameters.AddWithValue("@Status", Status);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@CRStatus", CRStatus);
                Cmd.Parameters.AddWithValue("@Period", Period);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@sortExpression", SortExpression);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public Int32 SearchPreInventoryCount(String VIN, Int32 Year, long MakeID, long ModelID, long BodyID, String Dealer, long BuyerID, Int32 Status, long AddedBy, Int32 CRStatus, Int32 Period, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            Int32 Result = 0;

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_PreInventorySearchCount", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@Year", Year);
                Cmd.Parameters.AddWithValue("@MakeID", MakeID);
                Cmd.Parameters.AddWithValue("@ModelID", ModelID);
                Cmd.Parameters.AddWithValue("@BodyID", BodyID);
                Cmd.Parameters.AddWithValue("@Dealer", Dealer);
                Cmd.Parameters.AddWithValue("@BuyerID", BuyerID);
                Cmd.Parameters.AddWithValue("@Status", Status);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@CRStatus", CRStatus);
                Cmd.Parameters.AddWithValue("@Period", Period);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@sortExpression", SortExpression);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }

        #region [Added by Rupendra 28 Dec 12 for fetch data on EntityType bassed]
        public DataTable SearchPreInventory_Ver211(String VIN, Int32 Year, long MakeID, long ModelID, long BodyID, String Dealer, long BuyerID, Int32 Status, long AddedBy, Int32 CRStatus, Int32 Period, Int32 LoginEntityTypeID, Int32 EntityID, Int32 ParentEntityID, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, Int16 OrgID)
        {
            DataTable dTab = new DataTable("Inventories");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_PreInventorySearch_Ver211", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@Year", Year);
                Cmd.Parameters.AddWithValue("@MakeID", MakeID);
                Cmd.Parameters.AddWithValue("@ModelID", ModelID);
                Cmd.Parameters.AddWithValue("@BodyID", BodyID);
                Cmd.Parameters.AddWithValue("@Dealer", Dealer);
                Cmd.Parameters.AddWithValue("@BuyerID", BuyerID);
                Cmd.Parameters.AddWithValue("@Status", Status);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@CRStatus", CRStatus);
                Cmd.Parameters.AddWithValue("@Period", Period);
                Cmd.Parameters.AddWithValue("@EntityTypeId", LoginEntityTypeID);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@sortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public Int32 SearchPreInventoryCount_Ver211(String VIN, Int32 Year, long MakeID, long ModelID, long BodyID, String Dealer, long BuyerID, Int32 Status, long AddedBy, Int32 CRStatus, Int32 Period, Int32 LoginEntityTypeID, Int32 EntityID, Int32 ParentEntityID, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, Int16 OrgID)
        {
            Int32 Result = 0;

            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_PreInventorySearchCount_Ver211", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@Year", Year);
                Cmd.Parameters.AddWithValue("@MakeID", MakeID);
                Cmd.Parameters.AddWithValue("@ModelID", ModelID);
                Cmd.Parameters.AddWithValue("@BodyID", BodyID);
                Cmd.Parameters.AddWithValue("@Dealer", Dealer);
                Cmd.Parameters.AddWithValue("@BuyerID", BuyerID);
                Cmd.Parameters.AddWithValue("@Status", Status);
                Cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                Cmd.Parameters.AddWithValue("@CRStatus", CRStatus);
                Cmd.Parameters.AddWithValue("@Period", Period);
                Cmd.Parameters.AddWithValue("@EntityTypeId", LoginEntityTypeID);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@ParentEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@StartRowIndex", StartRowIndex);
                Cmd.Parameters.AddWithValue("@MaximumRows", MaximumRows);
                Cmd.Parameters.AddWithValue("@sortExpression", SortExpression);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;

                Result = (Int32)Cmd.ExecuteScalar();
                objDal.Dispose();
            }
            return Result;
        }

        #endregion


        #region[Inventory list sort options]
        /// <summary>
        /// Display sort options for inventory list
        /// </summary>
        /// <param name="strQuery"></param>
        /// <returns></returns>
        public DataTable InventoryList_SortOptions(String strQuery, String Sort1, String Sort2)
        {
            DataTable dTab = new DataTable("InvSort");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = Conn.CreateCommand();
                Cmd.CommandText = strQuery;
                Cmd.CommandType = CommandType.Text;
                SqlDataReader dReader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);

                dTab.Load(dReader);
            }
            return dTab;
        }
        #endregion

        public DataTable GetPreInventoryDetailByID(Int64 PreInventoryID)
        {
            DataTable dTab = new DataTable("Inventories");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_GetPreInventoryDetail", Conn);
                Cmd.Parameters.AddWithValue("@PreInventoryID", PreInventoryID);

                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public DataTable GetPreInvLinked(Int64 PreInventoryID, String VIN, Int32 Period, Int16 OrgID)
        {
            DataTable dTab = new DataTable("Inventories");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_GetPreInventoryLinked", Conn);
                Cmd.Parameters.AddWithValue("@PreInventoryID", PreInventoryID);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@Period", Period);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public DataTable GetInvLinked(Int64 PreInventoryID, String VIN, Int32 Period, Int16 OrgID)
        {
            DataTable dTab = new DataTable("Inventories");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_GetInventoryLinked", Conn);
                Cmd.Parameters.AddWithValue("@PreInventoryID", PreInventoryID);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@Period", Period);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);

                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public void DeletePreinventory(Int64 PreInventoryID, Int64 UserID)
        {
            int i = METAOPTION.DALDataContext.Factory.DB.Mobile_DeletePreInventory(PreInventoryID, UserID);
        }

        public IQueryable<Mobile_LocationType> GetLocationType()
        {
            IQueryable<Mobile_LocationType> result = (from p in METAOPTION.DALDataContext.Factory.DB.Mobile_LocationTypes
                                                      where p.IsActive == 1
                                                      orderby p.LocationName ascending
                                                      select p) as IQueryable<Mobile_LocationType>;

            return result;
        }

        public void Mobile_EntityLocationInsert(Mobile_EntityLocation MEL)
        {
            objDAL.Mobile_EntityLocations.InsertOnSubmit(MEL);
            objDAL.SubmitChanges();

        }

        public DataTable GetVINLocationHistory(String VIN,Int16 OrgID)
        {
            DataTable dTab = new DataTable("Inventories");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_GetVINLocationHistory", Conn);
                Cmd.Parameters.AddWithValue("@VIN", VIN);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);

                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public String GetVINFromPreInventory(Int64 PreInvID, Int16 OrgID)
        {
            String VIN = String.Empty;
            var result = (from v in METAOPTION.DALDataContext.Factory.DB.Mobile_PreInventories
                          where v.PreInventoryId == PreInvID && v.OrgID == @OrgID
                          select new { v.VIN });
            if (result != null)
                VIN = result.FirstOrDefault().VIN;

            return VIN;
        }

        public void MarkPreInvAsPending(Int64 PreInvID, Int64 ModifiedBy)
        {
            String strqry = String.Format("Update Mobile_PreInventory Set IsPending=1,IsRejected=0,DateModified='{1}',ModifiedBy={2} Where PreInventoryId={0}", PreInvID, System.DateTime.Now, ModifiedBy);
            DALDataContext.Factory.DB.ExecuteCommand(strqry);
            DALDataContext.Factory.DB.SubmitChanges();
        }



        public void RejectPreInventory(Int64 PreInvID, Int64 ModifiedBy, String Reason)
        {
            String strqry = String.Format("Update Mobile_PreInventory Set IsPending=1,IsRejected=1,DateModified='{1}',ModifiedBy={2},DeleteReason='{3}' Where PreInventoryId={0}", PreInvID, System.DateTime.Now, ModifiedBy, Reason);
            DALDataContext.Factory.DB.ExecuteCommand(strqry);
            DALDataContext.Factory.DB.SubmitChanges();
        }

        public DataTable GetDealerInfoByID(Int64 DealerID)
        {
            DataTable dTab = new DataTable("Inventories");
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_GetDealerInfo", Conn);
                Cmd.Parameters.AddWithValue("@DealerID", DealerID);

                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            return dTab;
        }

        public void LinkCR(Int64 PreInvID, Int64 InvID, Int64 ModifiedBy, Int32 CRID, String CRURL, Int32 CRStatus)
        {
            int i = METAOPTION.DALDataContext.Factory.DB.LinkCR(PreInvID, InvID, CRID, CRURL, CRStatus, ModifiedBy);
        }

        public void RemoveCR(Int64 PreInvID, Int64 InvID, Int64 ModifiedBy)
        {
            int i = METAOPTION.DALDataContext.Factory.DB.RemoveCR(PreInvID, InvID, ModifiedBy);
        }

        public void Update_Inv_PreInv(long InvID, long PreInvID, long AddedBy)
        {
            int i = METAOPTION.DALDataContext.Factory.DB.Mobile_UpdatePreInventory(InvID, PreInvID, AddedBy);
        }

        public long PreInv_ByInvID(long InvID)
        {
            DALDataContext db = new DALDataContext();
            var result = db.Mobile_PreInventories.FirstOrDefault(x => x.InventoryId == InvID);
            if (result != null)
                return result.PreInventoryId;
            else
                return 0;
        }

        //Adesh , 23 Jan 2013
        public long PreInv_ByInvID(DataTable dt)
        {
            if (dt.Rows.Count > 0)
                return Convert.ToInt64(dt.Rows[0]["PreInvID"]);
            else
                return 0;
        }

        public DataTable GetAllPreInvImages(long InvID)
        {
            DataTable dtab = new DataTable();
            dtab.Columns.Add("ImageID");
            dtab.Columns.Add("ThumbPath");
            dtab.Columns.Add("MainPath");

            DALDataContext db = new DALDataContext();
            var result = db.Mobile_Images.Select(x => new { x.ImageID, x.ThumbNailPath, x.ServerPath }).ToList();
            foreach (var r in result)
            {
                DataRow row = dtab.NewRow();
                row["ImageID"] = r.ImageID.ToString();
                row["ThumbPath"] = r.ThumbNailPath;
                row["MainPath"] = r.ServerPath;
                dtab.Rows.Add(row);
            }
            return dtab;
        }

        public IQueryable GetPreInvImagesByInventoryID(long invID, Int32 StartRow, Int32 MaximumRows)
        {
            DALDataContext objDAL = new DALDataContext();
            return objDAL.Mobile_PreInventoryImagesByID(invID, StartRow, MaximumRows).AsQueryable();
        }

        #region [Get PreInvImages By InventoryID - Count]
        public int GetPreInvImagesByInventoryIDCount(long invID)
        {
            List<Mobile_PreInventoryImagesByIDCountResult> Result = METAOPTION.DALDataContext.Factory.DB.Mobile_PreInventoryImagesByIDCount(invID).ToList();
            if (Result.Count > 0)
                return Result.First().Total.Value;
            else return 0;
        }
        #endregion

        public String CRID_ByInvID(long InvID)
        {
            DALDataContext db = new DALDataContext();
            var result = db.Inventories.FirstOrDefault(x => x.InventoryId == InvID);
            //if (result != null)
            return result.CR_ID;
            //else
            //    return 0;
        }

        //Adesh 23 Jan,2013
        public String CRID_ByInvID(DataTable dt)
        {
            if (dt.Rows.Count > 0)
                return Convert.ToString(dt.Rows[0][0]);
            else
                return "0";
        }

        public Int32? CRID_ByPreInvID(long PreInvID)
        {
            DALDataContext db = new DALDataContext();
            var result = db.Mobile_PreInventories.FirstOrDefault(x => x.PreInventoryId == PreInvID);
            if (result != null)
                return result.CR_ID;
            else
                return 0;
        }

        #region[PreInventory added by users]
        public List<Mobile_PreInv_AddedByResult> GetAllPreInvUsers()
        {
            DALDataContext objDal = new DALDataContext();
            List<Mobile_PreInv_AddedByResult> result = objDal.Mobile_PreInv_AddedBy().ToList<Mobile_PreInv_AddedByResult>();
            return result;
        }
        #endregion

        #region[PreInventory added by users against buyer]
        public List<SecurityUser> GetAllPreInvUsers_ver211(Int32 EntityID, Int32 ParentEntityID, Int32 EntityTypeID, Int16 OrgID)
        {

            List<SecurityUser> lst = new List<SecurityUser>();
            DataTable dTab = new DataTable();
            DALDataContext objDal = new DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Mobile_PreInv_AddedBy_ver211", Conn);
                Cmd.Parameters.AddWithValue("@EntityID", EntityID);
                Cmd.Parameters.AddWithValue("@BuyerEntityID", ParentEntityID);
                Cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDal.Dispose();
            }
            foreach (DataRow dr in dTab.Rows)
            {
                SecurityUser SU = new SecurityUser();
                SU.DisplayName = dr["DisplayName"].ToString();
                SU.SecurityUserID = Convert.ToInt32(dr["SecurityUserID"]);
                lst.Add(SU);
            }
            return lst;
        }
        #endregion

        #region[Entity type by Location type]
        public Int32? EntityTypeByLocationType(long LocationTypeID)
        {
            DALDataContext db = new DALDataContext();
            var result = db.Mobile_LocationTypes.FirstOrDefault(x => x.LocationTypeID == LocationTypeID);
            if (result != null)
                return result.EntityTypeID;
            else
                return 0;
        }
        #endregion

        #region[Added by Rupendra 22 Aug 12 for get generic Image]
        public int GetGenImagesByGenericIDCount(string VIN)
        {
            List<Mobile_GenericImagesByGenIDCountResult> Result = METAOPTION.DALDataContext.Factory.DB.Mobile_GenericImagesByGenIDCount(VIN).ToList();
            if (Result.Count > 0)
                return Result.First().Total.Value;
            else return 0;
        }
        public IQueryable GetGenericImagesByGenericID(string VIN, Int32 StartRow, Int32 MaximumRows)
        {
            DALDataContext objDAL = new DALDataContext();
            return objDAL.Mobile_GenericImagesByID(VIN, StartRow, MaximumRows).AsQueryable();
        }
        #endregion
    }
}
