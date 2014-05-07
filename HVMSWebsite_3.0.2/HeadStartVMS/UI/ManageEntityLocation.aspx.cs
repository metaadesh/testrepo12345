using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using METAOPTION.BAL;
using System.Collections.Generic;
using System.Text;
using System.Web.Services;
using METAOPTION.UserControls;
using MetaOption.Web.UI.WebControls;

namespace METAOPTION.UI
{
    public partial class ManageEntityLocation : System.Web.UI.Page
    {
        PaymentBLL paymentBLL = new PaymentBLL();
        PreInventoryBAL PreInvBAL = new PreInventoryBAL();

        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["MasterPage"])))
                    this.MasterPageFile = Convert.ToString(Session["MasterPage"]);
                else
                    Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(Request.Url.ToString()));
            }
            catch (Exception ex)
            {
                Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + Server.UrlEncode(Request.Url.ToString()));
            }

           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Util.setValue(ddlEntityType, paymentBLL.GetRecipientType(), "EntityType1", "EntityTypeId");
                Util.setValue(ddlLocationType, PreInvBAL.GetLocationType(), "LocationName", "LocationTypeID");
            }
        }

        //protected void ddlEntityType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    txtSearchString.Text = "";
        //    hdnSelectedEntityID.Value = "";
        //    lblSelectedEntityName.Text = "";


        //    String selectedEntityType = Util.getValue(ddlEntityType, "SelectedText");
        //    lblEntityType.Text = (selectedEntityType != String.Empty) ? "Select " + selectedEntityType : "Entity(s)";

        //    BindFilterData();
        //}

        private void BindFilterData()
        {

            List<GetRecipientsResult> receipents = GetFilteredRecipients();

            // Fill Recipients in Grid View
            Util.setValue(grvRecipient, receipents);
        }

        private List<GetRecipientsResult> GetFilteredRecipients()
        {
            String searchField = Util.getValue(ddlSearchField);
            String searchOperator = Util.getValue(ddlSearchOperator);
            String searchString = Util.getValue(txtSearchString).ToLower();

            if (String.IsNullOrEmpty(hfEntityTypeID.Value))
                hfEntityTypeID.Value = "0";
            Int32 receipentTypeId = Convert.ToInt32(hfEntityTypeID.Value);
            if (receipentTypeId == 0)
                return null;

            List<GetRecipientsResult> receipents = paymentBLL.GetRecipients(receipentTypeId, Constant.OrgID);

            var query = from p in receipents
                        where p.OrgID==Constant.OrgID //added By prem
                        select p;

            switch (searchField.ToLower())
            {

                case "recipientname":
                    if (searchOperator == "begins with")
                        query = query.Where(c => c.recipientname.ToLower().StartsWith(searchString));
                    if (searchOperator == "ends with")
                        query = query.Where(c => c.recipientname.ToLower().EndsWith(searchString));
                    if (searchOperator == "contains")
                        query = query.Where(c => c.recipientname.ToLower().Contains(searchString));
                    break;

                case "street":
                    if (searchOperator == "begins with")
                        query = query.Where(c => c.Street.ToLower().StartsWith(searchString));
                    if (searchOperator == "ends with")
                        query = query.Where(c => c.Street.ToLower().EndsWith(searchString));
                    if (searchOperator == "contains")
                        query = query.Where(c => c.Street.ToLower().Contains(searchString));
                    break;

                case "city":
                    if (searchOperator == "begins with")
                        query = query.Where(c => c.City.ToLower().StartsWith(searchString));
                    if (searchOperator == "ends with")
                        query = query.Where(c => c.City.ToLower().EndsWith(searchString));
                    if (searchOperator == "contains")
                        query = query.Where(c => c.City.ToLower().Contains(searchString));
                    break;
            }

            return query.ToList();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            BindFilterData();
            mpeSelectRecipient.Show();
        }

        protected void btnAddLocation_Click(object sender, EventArgs e)
        {
            Mobile_EntityLocation MEL = new Mobile_EntityLocation();
            MEL.LocationCode = txtLocationCode.Text;
            MEL.LocationDesc = txtLocationDesc.Text;
            //MEL.EntityTypeID = Convert.ToInt32(ddlEntityType.SelectedItem.Value);
            MEL.EntityTypeID = Convert.ToInt32(hfEntityTypeID.Value);
            MEL.EntityID = Convert.ToInt64(hdnSelectedEntityID.Value);
            MEL.LocationTypeID = Convert.ToInt32(ddlLocationType.SelectedItem.Value);
            MEL.DateAdded = System.DateTime.Now;
            MEL.AddedBy = Convert.ToInt64(Session["empId"]);
            MEL.DateModified = null;
            MEL.ModifiedBy = 0;
            MEL.DateDeleted = null;
            MEL.DeletedBy = 0;
            MEL.IsActive = 1;
            MEL.IsArchived = false;
            MEL.OrgID = Constant.OrgID;
      

            PreInvBAL.Mobile_EntityLocationInsert(MEL);

            ScriptManager.RegisterClientScriptBlock(upLocation, this.GetType(), "alert", "ShowMsg('Location Saved Successfully');", true);

            txtLocationCode.Text = string.Empty;
            txtLocationDesc.Text = string.Empty;
            //ddlEntityType.SelectedIndex = 0;
            ddlLocationType.SelectedIndex = 0;
            hdnSelectedEntityID.Value = string.Empty;
            lblSelectedEntityName.Text = string.Empty;
        }

        protected void grvRecipient_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvRecipient.PageIndex = e.NewPageIndex;
            //BindData();
            BindFilterData();

            mpeSelectRecipient.Show();
        }

        protected void grvRecipient_OnSorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, ASCENDING);
            }
        }

        private void SortGridView(string sortExpression, string direction)
        {
           // IEnumerable<GetRecipientsResult> results = GetFilteredRecipients().Sort<GetRecipientsResult>(sortExpression + direction);

            // Fill Recipients in Grid View
            //Util.setValue(grvRecipient, results.ToList());
            mpeSelectRecipient.Show();
        }

        protected void btnSelectRecipient_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvRecipient.Rows)
            {
                GroupRadioButton selectedRadioButton = row.FindControl("selectedRadioButton") as GroupRadioButton;

                if (selectedRadioButton != null && selectedRadioButton.Checked)
                {
                    hdnSelectedEntityID.Value = grvRecipient.DataKeys[row.RowIndex].Value.ToString();
                    String recipientName = grvRecipient.Rows[row.RowIndex].Cells[3].Text.Trim();

                    lblSelectedEntityName.Text = recipientName;
                    
                }
            }
        }

        protected void ddlLocationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLocationType.SelectedValue == "4" || ddlLocationType.SelectedValue == "5" || ddlLocationType.SelectedValue == "7")
                trSelectEntity.Visible = true;
            else
                trSelectEntity.Visible = false;

            Int32? EntityTypeID = PreInvBAL.EntityTypeByLocationType(Convert.ToInt64(ddlLocationType.SelectedValue));
            hfEntityTypeID.Value = Convert.ToString(EntityTypeID);
            BindFilterData();
        }

    }
}