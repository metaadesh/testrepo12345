using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace METAOPTION
{
    public class LocationBAL
    {
        METAOPTION.LocationDAL objLocationDAL = new METAOPTION.LocationDAL();

        //#region[Fetch VIN location by VIN, LocationID and AddedBy]
        //public DataTable FetchVINLocation(String VIN, long LocationID, long AddedBy)
        //{
        //    return objLocationDAL.FetchVINLocation(VIN, LocationID, AddedBy);
        //}
        //#endregion

        #region [Get location to bind dropdown]
        public IQueryable GetAllLocations()
        {
            return objLocationDAL.GetAllLocations();
        }
        #endregion

        #region [Get location to bind dropdown for vendor]
        public IQueryable GetAllLocations(Int32 AddedBy)
        {
            return objLocationDAL.GetAllLocations(AddedBy);
        }
        #endregion

        #region [Get location to bind dropdown for child buyer]
        public IQueryable GetAllLocations(Int32 AddedBy, Int32 ParentBuyerID)
        {
            return objLocationDAL.GetAllLocations(AddedBy, ParentBuyerID).AsQueryable();
        }

        public List<Mobile_EntityLocation> GetAllLocations(Int32 SecurityUserID, Int32 ParentEntityID, Int32 EntityTypeID, Int16 OrgID)
        {
            return objLocationDAL.GetAllLocations(SecurityUserID, ParentEntityID, EntityTypeID, OrgID);
        }
        #endregion

        #region[ Added by  Vipin, View Location added by users for ChildBuyer]
        public List<Mobile_VinLocation_AddedByUsersResult> GetAllLocationUsers_AddedBy(Int32 AddedBy, Int32 ParentBuyerID)
        {

            return objLocationDAL.GetAllLocationUsers_AddedBy(AddedBy, ParentBuyerID);



        }

        public List<SecurityUser> GetAllLocationUsers_AddedBy(Int32 SecurityUserID, Int32 ParentEntityID, Int32 EntityTypeID, Int16 OrgID)
        {

            return objLocationDAL.GetAllLocationUsers_AddedBy(SecurityUserID, ParentEntityID, EntityTypeID, OrgID);



        }

        #endregion

        #region [Get location inventory stats]
        public IQueryable LocationInventoryStats(long locationStatus, Int16 OrgID)
        {
            return objLocationDAL.LocationInventoryStats(locationStatus, OrgID);
        }
        #endregion

        //#region[Get User name and id to bind dropdown on view location screen]
        //public IQueryable GetAllLocationUsers()
        //{
        //    return objLocationDAL.GetAllLocationUsers();
        //}

        //#endregion

        #region[Generic Images added by users]
        public List<SecurityUser> GetAllLocationUsers()
        {
            return objLocationDAL.GetAllLocationUsers();
        }
        #endregion


        #region[Generic Images added by users]
        public List<SecurityUser> GetAllLocationUsers_AddedBy(Int32 AddedBy)
        {
            return objLocationDAL.GetAllLocationUsers_AddedBy(AddedBy);
        }
        #endregion


        #region[VIN location]
        public List<Mobile_GetVINLocationResult> FetchVINLocation(String VIN, long LocationID, long AddedBy, Int32 StartRowIndex, Int32 MaximumRows,Int16 OrgID)
        {
            return objLocationDAL.FetchVINLocation(VIN ?? "", LocationID, AddedBy, StartRowIndex, MaximumRows,OrgID);
        }

        public List<Mobile_GetVINLocation_Ver211Result> FetchVINLocationVer211(String VIN, long LocationID, long AddedBy, Int32 EntityTypeID, Int32 SecurityUserID, Int32 StartRowIndex, Int32 MaximumRows)
        {
            return objLocationDAL.FetchVINLocationVer211(VIN ?? "", LocationID, AddedBy, EntityTypeID, SecurityUserID, StartRowIndex, MaximumRows);
        }

        public DataTable FetchVINLocationVer211(String VIN, long LocationID, long AddedBy, Int32 EntityTypeID, Int32 EntityID, Int32 ParentEntityID, Int32 StartRowIndex, Int32 MaximumRows, Int16 OrgID)
        {
            return objLocationDAL.FetchVINLocationVer211(VIN ?? "", LocationID, AddedBy, EntityTypeID, EntityID, ParentEntityID, StartRowIndex, MaximumRows, OrgID);
        }
        #endregion

        #region[VIN location count]
        public Int32? FetchVINLocationCount(String VIN, long LocationID, long AddedBy, Int32 StartRowIndex, Int32 MaximumRows,Int16 OrgID)
        {
            return objLocationDAL.FetchVINLocationCount(VIN ?? "", LocationID, AddedBy, StartRowIndex, MaximumRows,OrgID);
        }

        public Int32? FetchVINLocationCountVer211(String VIN, long LocationID, long AddedBy, Int32 EntityTypeID, Int32 SecurityUserID, Int32 StartRowIndex, Int32 MaximumRows)
        {
            return objLocationDAL.FetchVINLocationCountVer211(VIN ?? "", LocationID, AddedBy, EntityTypeID, SecurityUserID, StartRowIndex, MaximumRows);
        }

        public Int32? FetchVINLocationCountVer211(String VIN, long LocationID, long AddedBy, Int32 EntityTypeID, Int32 EntityID, Int32 ParentEntityID, Int32 StartRowIndex, Int32 MaximumRows, Int16 OrgID)
        {
            return objLocationDAL.FetchVINLocationCountVer211(VIN ?? "", LocationID, AddedBy, EntityTypeID, EntityID, ParentEntityID, StartRowIndex, MaximumRows, OrgID);
        }
        #endregion
    }
}
