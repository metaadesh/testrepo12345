using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web;
using System.Xml.Linq;
using METAOPTION;
using METAOPTION.DAL;

namespace METAOPTION.BAL
{
    public class PreInventoryBAL
    {
        METAOPTION.DAL.PreInventoryDAL objPreInventoryDAL = new METAOPTION.DAL.PreInventoryDAL();


        public DataTable PreInventoryImagesPath(long PreInventoryID)
        {
            return objPreInventoryDAL.InventoryImages_ByInventoryId(PreInventoryID);
        }

        public System.Collections.ArrayList PreInventoryImages(long PreInventoryID)
        {
            return objPreInventoryDAL.PreInventoryImages(PreInventoryID);
        }

        public bool isInventoryExists(long PreInventoryID)
        {
            return objPreInventoryDAL.isInventoryExists(PreInventoryID);
        }

        public Mobile_PreInventory GetPreInventoryByID(long PreInventoryID)
        {
            return objPreInventoryDAL.GetPreInventoryByID(PreInventoryID);
        }

        public String GetDealerName(long DealerID)
        {
            return objPreInventoryDAL.GetDealerName(DealerID);
        }

        //public void UpdatePreInvbyInvID(long PreInvID, long InvID)
        //{
        //    objPreInventoryDAL.UpdatePreInvbyInvID(PreInvID, InvID);
        //}

        public DataTable SearchPreInventory(String VIN, Int32 Year, long MakeID, long ModelID, long BodyID, String Dealer, long BuyerID, Int32 Status, long AddedBy, Int32 CRStatus, Int32 Period, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            return objPreInventoryDAL.SearchPreInventory(VIN, Year, MakeID, ModelID, BodyID, Dealer, BuyerID, Status, AddedBy, CRStatus, Period, StartRowIndex, MaximumRows, SortExpression);
        }

        public Int32 SearchPreInventoryCount(String VIN, Int32 Year, long MakeID, long ModelID, long BodyID, String Dealer, long BuyerID, Int32 Status, long AddedBy, Int32 CRStatus, Int32 Period, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression)
        {
            return objPreInventoryDAL.SearchPreInventoryCount(VIN, Year, MakeID, ModelID, BodyID, Dealer, BuyerID, Status, AddedBy, CRStatus, Period, StartRowIndex, MaximumRows, SortExpression);
        }

        #region [Added by Rupendra 28 Dec 12 for fetch data on EntityType bassed]
        public DataTable SearchPreInventory_Ver211(String VIN, Int32 Year, long MakeID, long ModelID, long BodyID, String Dealer, long BuyerID, Int32 Status, long AddedBy, Int32 CRStatus, Int32 Period, Int32 LoginEntityTypeID, Int32 EntityID, Int32 ParentEntityID, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, Int16 OrgID)
        {
            return objPreInventoryDAL.SearchPreInventory_Ver211(VIN, Year, MakeID, ModelID, BodyID, Dealer, BuyerID, Status, AddedBy, CRStatus, Period, LoginEntityTypeID, EntityID, ParentEntityID, StartRowIndex, MaximumRows, SortExpression, OrgID);
        }

        public Int32 SearchPreInventoryCount_Ver211(String VIN, Int32 Year, long MakeID, long ModelID, long BodyID, String Dealer, long BuyerID, Int32 Status, long AddedBy, Int32 CRStatus, Int32 Period, Int32 LoginEntityTypeID, Int32 EntityID, Int32 ParentEntityID, Int32 StartRowIndex, Int32 MaximumRows, String SortExpression, Int16 OrgID)
        {
            return objPreInventoryDAL.SearchPreInventoryCount_Ver211(VIN, Year, MakeID, ModelID, BodyID, Dealer, BuyerID, Status, AddedBy, CRStatus, Period, LoginEntityTypeID, EntityID, ParentEntityID, StartRowIndex, MaximumRows, SortExpression, OrgID);
        }
        #endregion

        #region[Inventory list sort options]
        public DataTable InventoryList_SortOptions(String strQuery, String Sort1, String Sort2)
        {
            return objPreInventoryDAL.InventoryList_SortOptions(strQuery, Sort1, Sort2);
        }
        #endregion

        public DataTable GetPreInventoryDetailByID(Int64 PreInventoryID)
        {
            return objPreInventoryDAL.GetPreInventoryDetailByID(PreInventoryID);
        }

        public DataTable GetPreInvLinked(Int64 PreInventoryID, String VIN, Int32 Period, Int16 OrgID)
        {
            return objPreInventoryDAL.GetPreInvLinked(PreInventoryID, VIN, Period, OrgID);
        }

        public DataTable GetInvLinked(Int64 PreInventoryID, String VIN, Int32 Period, Int16 OrgID)
        {
            return objPreInventoryDAL.GetInvLinked(PreInventoryID, VIN, Period, OrgID);
        }

        public void DeletePreinventory(Int64 PreInventoryID, Int64 UserID)
        {
            objPreInventoryDAL.DeletePreinventory(PreInventoryID, UserID);
        }

        public IQueryable<Mobile_LocationType> GetLocationType()
        {
            var result = (from p in objPreInventoryDAL.GetLocationType()
                          select p);

            return result;
        }

        public void Mobile_EntityLocationInsert(Mobile_EntityLocation MEL)
        {
            objPreInventoryDAL.Mobile_EntityLocationInsert(MEL);
        }

        public String GetVINFromPreInventory(Int64 PreInvID, Int16 OrgID)
        {
            return objPreInventoryDAL.GetVINFromPreInventory(PreInvID, OrgID);
        }

        public DataTable GetVINLocationHistory(String VIN, Int16 OrgID)
        {
            return objPreInventoryDAL.GetVINLocationHistory(VIN, OrgID);
        }

        public void MarkPreInvAsPending(Int64 PreInvID, Int64 ModifiedBy)
        {
            objPreInventoryDAL.MarkPreInvAsPending(PreInvID, ModifiedBy);
        }



        public DataTable GetDealerInfoByID(Int64 DealerID)
        {
            return objPreInventoryDAL.GetDealerInfoByID(DealerID);
        }

        public void RejectPreInventory(Int64 PreInvID, Int64 ModifiedBy, String Reason)
        {
            objPreInventoryDAL.RejectPreInventory(PreInvID, ModifiedBy, Reason);
        }

        public void LinkCR(Int64 PreInvID, Int64 InvID, Int64 ModifiedBy, Int32 CRID, String CRURL, Int32 CRStatus)
        {
            objPreInventoryDAL.LinkCR(PreInvID, InvID, ModifiedBy, CRID, CRURL, CRStatus);
        }


        public void RemoveCR(Int64 PreInvID, Int64 InvID, Int64 ModifiedBy)
        {
            objPreInventoryDAL.RemoveCR(PreInvID, InvID, ModifiedBy);
        }

        public void Update_Inv_PreInv(long InvID, long PreInvID, long AddedBy)
        {
            objPreInventoryDAL.Update_Inv_PreInv(InvID, PreInvID, AddedBy);
        }

        public long PreInv_ByInvID(long InvID)
        {
            return objPreInventoryDAL.PreInv_ByInvID(InvID);
        }

        public long PreInv_ByInvID(DataTable dt)
        {
            return objPreInventoryDAL.PreInv_ByInvID(dt);
        }

        public IQueryable GetPreInvImagesByInventoryID(long invID, Int32 StartRow, Int32 MaximumRows)
        {
            return objPreInventoryDAL.GetPreInvImagesByInventoryID(invID, StartRow, MaximumRows);
        }

        #region [Get PreInvImages By InventoryID - Count]
        public static int GetPreInvImagesByInventoryIDCount(long InvId)
        {
            PreInventoryDAL objPreInventoryDAL = new PreInventoryDAL();
            return objPreInventoryDAL.GetPreInvImagesByInventoryIDCount(InvId);
        }
        #endregion

        public String CRID_ByInvID(long InvID)
        {
            return objPreInventoryDAL.CRID_ByInvID(InvID);
        }

        public String CRID_ByInvID(DataTable dt)
        {
            return objPreInventoryDAL.CRID_ByInvID(dt);
        }

        public Int32? CRID_ByPreInvID(long PreInvID)
        {
            return objPreInventoryDAL.CRID_ByPreInvID(PreInvID);
        }

        #region[PreInventory added by users]
        public List<Mobile_PreInv_AddedByResult> GetAllPreInvUsers()
        {
            return objPreInventoryDAL.GetAllPreInvUsers();
        }
        #endregion

        #region[PreInventory added by users]
        public List<SecurityUser> GetAllPreInvUsers_ver211(Int32 EntityID, Int32 ParentEntityID, Int32 EntityTypeID, Int16 OrgID)
        {
            return objPreInventoryDAL.GetAllPreInvUsers_ver211(EntityID, ParentEntityID, EntityTypeID, OrgID);
        }
        #endregion

        #region[Entity type by Location type]
        public Int32? EntityTypeByLocationType(long LocationTypeID)
        {
            return objPreInventoryDAL.EntityTypeByLocationType(LocationTypeID);
        }
        #endregion

        #region[Added by Rupendra 22 Aug 12 for get generic Image]
        public static int GetGenImagesByGenericIDCount(string VIN)
        {
            PreInventoryDAL objPreInventoryDAL = new PreInventoryDAL();
            return objPreInventoryDAL.GetGenImagesByGenericIDCount(VIN);
        }

        public IQueryable GetGenericImagesByGenericID(string VIN, Int32 StartRow, Int32 MaximumRows)
        {
            PreInventoryDAL objPreInventoryDAL = new PreInventoryDAL();
            return objPreInventoryDAL.GetGenericImagesByGenericID(VIN, StartRow, MaximumRows).AsQueryable();
        }
        #endregion
    }
}
