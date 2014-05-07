using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Data.Linq;
using System.Collections;

namespace METAOPTION.DAL
{
    public class Admin_OrganizationDAL
    {
        Admin_DALDataContext objDAL = new Admin_DALDataContext();

        #region Organizations list
        public IQueryable<Object> GetOrganizationsList()
        {
            Admin_DALDataContext obj = new Admin_DALDataContext();
            var list = (from org in obj.Organisations
                        where org.IsActive == 1
                        orderby org.Organisation1
                        select new { OrgID = org.OrgID, OrgName = (org.Organisation1 + " (" + org.OrgCode + ")") }) as IQueryable<object>;
            return list;
        }
        #endregion

        #region[Add new Organization]
        public Int16 AddNewOrganization(string Organization1, string Orgcode, string Website, string Address, string Phone, string Fax, string Mail, Int64 AddedBy, Int16 Isactive, string status, Boolean AllowLaneAutomation, Boolean AllowMAA, String Password)
        {
            //Nullable<short> orgId = null;
            //short OrganizationId = 0;

            //objDAL.AddOrganization(
            //    ref orgId,
            //    Organization1,
            //    Orgcode,
            //    Website,
            //    Address,
            //    Phone,
            //    Fax,
            //    Mail,
            //    AddedBy,
            //    Isactive,
            //    status
            //    );

            //if (orgId.HasValue)
            //    OrganizationId = orgId.Value;
            //return OrganizationId;

            Int16 orgSelectID = -1;
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDAL.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("AddOrganisation", Conn);
                SqlParameter parOrgID = new SqlParameter("@OrgID", SqlDbType.SmallInt);
                parOrgID.Direction = ParameterDirection.Output;
                Cmd.Parameters.Add(parOrgID);

                Cmd.Parameters.AddWithValue("@Organisation", Organization1);
                Cmd.Parameters.AddWithValue("@OrgCode", Orgcode);
                Cmd.Parameters.AddWithValue("@Website", Website);
                Cmd.Parameters.AddWithValue("@Address", Address);
                Cmd.Parameters.AddWithValue("@Phone", Phone);
                Cmd.Parameters.AddWithValue("@Fax", Fax);
                Cmd.Parameters.AddWithValue("@Email", Mail);
                Cmd.Parameters.AddWithValue("@Addedby", AddedBy);
                Cmd.Parameters.AddWithValue("@IsActive", Isactive);
                Cmd.Parameters.AddWithValue("@Status", status);
                Cmd.Parameters.AddWithValue("@AllowLaneAutomation", AllowLaneAutomation);
                Cmd.Parameters.AddWithValue("@AllowMAA", AllowMAA);
                Cmd.Parameters.AddWithValue("@Password", Password);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.ExecuteNonQuery();

                if (parOrgID.Value != null)
                {
                    orgSelectID = (Int16)parOrgID.Value;
                }
                objDAL.Dispose();
            }

            return orgSelectID;
        }
        #endregion

        #region[Get OrganizationName and OrganizationCode with respect to OrgID]

        public DataTable getorginfo(Int16 OrganizationID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("orgName");
            dt.Columns.Add("orgCode");
            dt.Columns.Add("orgWebsite");
            dt.Columns.Add("orgAddress");
            dt.Columns.Add("orgPhone");
            dt.Columns.Add("orgFax");
            dt.Columns.Add("orgMail");
            dt.Columns.Add("orgLaneAutomation");
            dt.Columns.Add("orgMAA");
            var orgidgroup = from i in objDAL.Organisations
                             where i.OrgID == OrganizationID
                             select i;

            foreach (var m in orgidgroup)
            {
                dt.Rows.Add(m.Organisation1, m.OrgCode, m.Website, m.Address, m.Phone, m.Fax, m.Email, m.AllowLaneAutomation, m.AllowMAA);
            }

            return dt;
        }
        #endregion

        //#region[search Organization]
        //public DataTable SearchOrg(String Organization, String Code, String Email, String Website, String Phone)
        //{

        //    DataTable dTab = new DataTable("Organization");
        //    Admin_DALDataContext objDal = new Admin_DALDataContext();
        //    using (SqlConnection Conn = new SqlConnection(objDal.Connection.ConnectionString))
        //    {
        //        if (Conn.State == ConnectionState.Closed)
        //            Conn.Open();

        //        SqlCommand Cmd = new SqlCommand("Organisation_Search", Conn);
        //        Cmd.Parameters.AddWithValue("@Organisation", Organisation == null ? "" : Organisation);
        //        Cmd.Parameters.AddWithValue("@Code", Code == null ? "" : Code);
        //        Cmd.Parameters.AddWithValue("@Email", Email == null ? "" : Email);
        //        Cmd.Parameters.AddWithValue("@Website", Website == null ? "" : Website);
        //        Cmd.Parameters.AddWithValue("@Phone", Phone == null ? "" : Phone);

        //        SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        dTab.Load(reader);
        //        reader.Close();
        //        objDal.Dispose();
        //    }
        //    return dTab;
        //}
        //#endregion

        #region[Show Organization Data]
        public DataTable ShowOrganization(string OrgName, string Code, String Phone, String Website, String Email)
        {
            DataTable dTab = new DataTable("OrganizationList");
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDAL.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Organisation_Search", Conn);
                Cmd.Parameters.AddWithValue("@OrgName", OrgName);
                Cmd.Parameters.AddWithValue("@OrgCode", Code);
                Cmd.Parameters.AddWithValue("@Phone", Phone);
                Cmd.Parameters.AddWithValue("@Website", Website);
                Cmd.Parameters.AddWithValue("@Email", Email);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTab.Load(reader);
                reader.Close();
                objDAL.Dispose();
            }
            return dTab;
        }

        #endregion

        #region [Method call to add Dealer/Customer Details ]
        /// <summary>
        /// this is the region for insert
        /// Dealer/Customer details
        /// </summary>
        /// 

        /*
        protected long AddDealer()
        {
            long DealerId = 0;
            // Check if the page is valid
            if (Page.IsValid)
            {
                Dealer objDealer = new Dealer();
                objDealer.DealerName = txtDealerName.Text.Trim();
                objDealer.DealerDIN = txtDealerDIN.Text.Trim();
                objDealer.DealerTypeId = Convert.ToInt32(ddlType.SelectedValue);
                objDealer.DealerCategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
                objDealer.DealerSourceId = Convert.ToInt32(ddlSource.SelectedValue);
                //objDealer.AccountingCode = txtAccountingCode.Text.Trim();
                objDealer.Comment = txtComment.Text.Trim();
                objDealer.PreferenceSettings = Convert.ToBoolean(RblPrefSetting.Items[0].Selected);
                objDealer.ReceiveSms = Convert.ToBoolean(rblSmsSetting.Items[0].Selected);
                objDealer.ReceiveEmail = Convert.ToBoolean(rblEmailSetting.Items[0].Selected);

                //Change Request: June 16 2010(TASK:New Fields in Dealer and Inventory)
                objDealer.AuctionAccessNo = txtAuctionAccessNumber.Text.Trim();

                objDealer.AddedBy = Constant.UserId;
                objDealer.OrgID = Constant.OrgID;

                // Create object of Address master
                Address objAddress = new Address();
                // assign address value to address master properties
                objAddress.Street = txtStreet.Text.Trim();
                objAddress.Suite = txtSuite.Text.Trim();
                objAddress.City = txtCity.Text.Trim();

                //Save selected country & address details
                if (!string.IsNullOrEmpty(ddlState.SelectedValue) && ddlState.SelectedValue != "0")
                    objAddress.StateId = Convert.ToInt32(ddlState.SelectedValue);

                if (!string.IsNullOrEmpty(ddlCountry.SelectedValue) && ddlCountry.SelectedValue != "0")
                    objAddress.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
                objAddress.Zip = txtZip.Text.Trim();
                objAddress.Phone1 = txtPhone.Text.Trim();
                objAddress.Fax = txtFax.Text.Trim();
                objAddress.Phone2 = txtOtherNumber.Text.Trim();
                objAddress.Email1 = txtEmail.Text.Trim();

                DealerId = BAL.Admin_DealerCustomerBAL.AddDealerDetails(objDealer, objAddress);

                // for multiple selection of Franchise

                for (int index = 0; index < lstMake.Items.Count; index++)
                {
                    if (lstMake.Items[index].Selected)
                        BAL.Admin_DealerCustomerBAL.AddFranchise(Convert.ToInt32(lstMake.Items[index].Value), DealerId);
                }
                // call method for insert dealer/customer contact details
                AddDealerContactDetails(DealerId);
                if (RblPrefSetting.Items[0].Selected == true)
                    // Insert dealer preference details
                    AddDealerPreferenceDetails(DealerId);
                if (rblSmsSetting.Items[0].Selected == true)
                    // Insert dealer mobile preference
                    AddDealerMobilePreference(DealerId);
                if (rblEmailSetting.Items[0].Selected == true)
                    // Insert dealer Email preference
                    AddDealerEmailPreference(DealerId);
                // remove the session of contact and preference details

                //sessoin is commented by Prem
                //Session["DContDetail"] = null;
                //Session["Preference"] = null;

            }
            return DealerId;
        }
        */

        #endregion

        #region [Fill Role DropdownList]
        public List<EntityType> GetRealEntityType_List()
        {
            List<EntityType> list = (from en in objDAL.EntityTypes
                                     where en.IsRealEntity == true && en.IsActive == 1
                                     select en
                                     ).OrderBy(en => en.EntityType1).ToList<EntityType>();

            return list;
        }

        public DataTable GetEntityType()
        {
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            DataTable dt = new DataTable("DataTable");
            SqlConnection con = new SqlConnection(objDAL.Connection.ConnectionString);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM EntityType WHERE IsActive = 1 AND IsRealEntity = 1 ORDER BY EntityType", con);
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                con.Dispose();

            }

            return dt;
        }

        #endregion

        #region[populate Organization_DropDownList]
        public DataTable ddl_GetOrganization()
        {
            DataTable dtable = new DataTable("GetOrganisationData");
            SqlConnection con = new SqlConnection(objDAL.Connection.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetOrganisation", con);
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            dtable.Load(reader);
            con.Dispose();
            reader.Dispose();

            return dtable;
        }
        #endregion

        #region[Bind Users]
        public DataTable GetEmployee(int EntityTypeID, Int16 OrgID, Int64 SUID, Int32 IsActive)
        {
            Admin_DALDataContext objdal = new Admin_DALDataContext();
            DataTable dtable = new DataTable("GetEmployees");
            DataTable dt = new DataTable("NewGetEmployees");
            SqlConnection con = new SqlConnection(objdal.Connection.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetAllUserInfo", con);
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
            cmd.Parameters.AddWithValue("@OrgID", OrgID);
            cmd.Parameters.AddWithValue("@IsActive", IsActive); //"1"
            SqlDataReader reader = cmd.ExecuteReader();
            dtable.Load(reader);

            if (SUID != 0)
            {
                IEnumerable<DataRow> datarow = from x in dtable.AsEnumerable()
                                               where x.Field<Int64>("EmployeeID") == SUID
                                               select x;
                dt = datarow.CopyToDataTable<DataRow>();
                con.Dispose();
                reader.Dispose();
                return dt;
            }
            else
            {
                con.Dispose();
                reader.Dispose();
                return dtable;
            }

        }
        public DataTable GetEmployee(int EntityTypeID, Int16 OrgID, Int32 IsActive)
        {
            Admin_DALDataContext objdal = new Admin_DALDataContext();
            DataTable dtable = new DataTable("GetEmployees");
            SqlConnection con = new SqlConnection(objdal.Connection.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetAllUserInfo", con);
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EntityTypeID", EntityTypeID);
            cmd.Parameters.AddWithValue("@OrgID", OrgID);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);// "1"
            SqlDataReader reader = cmd.ExecuteReader();
            dtable.Load(reader);
            con.Dispose();
            reader.Dispose();

            return dtable;
        }


        #endregion

        #region[Update Password]
        public int UpdatePasswd(Int64 securityuserid, string password, Int64 modifiedby)
        {
            Nullable<int> update = null;
            int updatereturn = 0;
            objDAL.Update_Password(
                                ref update,
                                securityuserid,
                                password,
                                modifiedby
                                );

            if (update.HasValue)
                updatereturn = update.Value;
            return updatereturn;
        }
        #endregion

        #region[Change and Update Image at SearchOrganization page]
        public void Manage_Image(Int16 OrgID, Int16 status)
        {
            objDAL.Image_DeleteOrganisation(
                 OrgID,
                status);

        }
        #endregion

        #region[Delete Organization  at SearchOrganization page]
        public void Delete_Organization(Int16 OrgID, Int16 status, Int64 loginID)
        {
            objDAL.Admin_DeleteOrganisation(
                OrgID,
                status,
                loginID);

        }
        #endregion

        #region Get Master Page Left Panel Values
        public Hashtable Admin_GetLeftPanelValue()
        {
            Hashtable ht = new Hashtable();
            ISingleResult<Admin_GetLeftPanelValueResult> result = METAOPTION.Admin_DALDataContext.Factory.DB.Admin_GetLeftPanelValue();
            Admin_GetLeftPanelValueResult res = result.FirstOrDefault<Admin_GetLeftPanelValueResult>();
            ht.Add("ActiveUsers", res.ActiveUsers);
            ht.Add("ActiveOrganizations", res.ActiveOrganisations);
            ht.Add("ActiveSystems", res.ActiveSystems);
            return ht;
        }
        #endregion

        #region[Get Organization Summary DataTable]
        public DataTable OrganizationSummary(Int16 OrgID)
        {
            DataTable dt = new DataTable();
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            using (SqlConnection Conn = new SqlConnection(objDAL.Connection.ConnectionString))
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();

                SqlCommand Cmd = new SqlCommand("Admin_OrganisationSummary", Conn);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(Cmd);
                adap.Fill(dt);
                objDAL.Dispose();
            }
            return dt;
        }

        #endregion

        #region Allow/Deny Lane Automation & MAA Status
        public Int32 AllowDeny_Lane_MAA(Int16 OrgID, Boolean NewStatus, String Type)
        {
            Admin_DALDataContext objDAL = new Admin_DALDataContext();
            Int32 result;
            using (SqlConnection con = new SqlConnection(objDAL.Connection.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlCommand Cmd = new SqlCommand("Admin_AllowDeny_Lane_MAA", con);
                Cmd.Parameters.AddWithValue("@OrgID", OrgID);
                Cmd.Parameters.AddWithValue("@NewStatus", NewStatus);
                Cmd.Parameters.AddWithValue("@Type", Type);
                Cmd.CommandType = CommandType.StoredProcedure;
                result = Cmd.ExecuteNonQuery();
                objDAL.Dispose();
            }
            return result;
        }
        #endregion

        #region[Get the Organization information w r to Entity ID]

        public Int16? GetEntiyInformation(Int64 entityid)
        {
            Admin_DALDataContext obj = new Admin_DALDataContext();
            Int16? orgid = obj.SecurityUsers.Where(r => r.EntityID == entityid).Select(r => r.OrgID).SingleOrDefault();
            return orgid;
        }

        #endregion
    }
}
