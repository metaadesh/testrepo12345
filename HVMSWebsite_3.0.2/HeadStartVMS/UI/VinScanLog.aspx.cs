using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using METAOPTION.BAL;
using System.Data;
using System.Web.Security;

namespace METAOPTION.UI
{
    public partial class VinScanLog : System.Web.UI.Page
    {
        CommonBAL bal = new CommonBAL();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["MasterPageNoLeftPanel"])))
                    this.MasterPageFile = Convert.ToString(Session["MasterPageNoLeftPanel"]);
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

            if (!Page.IsPostBack)
            {
                BindAddedByddl();
                BindGrid();

            }
        }

        #region[Bind users]
        protected void BindAddedByddl()
        {
            Int32 ParentBuyerID = -1;
            if (Convert.ToString(Session["BuyerAccessLevel"]) != "1")
                ParentBuyerID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? -1 : Convert.ToInt32(Session["BuyerParent"]);
            ddlAddedBy.DataSource = bal.GetVINScanLogUsers_AddedByForChildBuyer(Convert.ToInt32(Session["empId"]), ParentBuyerID, Convert.ToInt32(Session["LoginEntityTypeID"]), Constant.OrgID);
            ddlAddedBy.DataTextField = "DisplayName";
            ddlAddedBy.DataValueField = "SecurityUserID";
            ddlAddedBy.DataBind();
            ddlAddedBy.Items.Insert(0, new ListItem("ALL", "-1"));
            if (Convert.ToString(Session["LoginEntityTypeID"]) != "5")
            {
                if (ddlAddedBy.Items.FindByValue(Convert.ToString(Session["empId"])) != null && ParentBuyerID == -1 && ddlAddedBy.Items.Count == 2)
                {
                    ddlAddedBy.SelectedValue = Convert.ToString(Session["empId"]);
                    ddlAddedBy.Enabled = false;
                }
                else if (ddlAddedBy.Items.FindByValue(Convert.ToString(Session["empId"])) != null)
                    ddlAddedBy.SelectedValue = Convert.ToString(Session["empId"]);
                else if (ddlAddedBy.Items.Count == 1)
                    ddlAddedBy.Enabled = false;
            }

        }
        #endregion

        #region[Bind Grid]
        protected void BindGrid()
        {
            Int32 ParentBuyerID = -1;

            if (Convert.ToString(Session["BuyerAccessLevel"]) != "1")
                ParentBuyerID = String.IsNullOrEmpty(Convert.ToString(Session["BuyerParent"])) ? -1 : Convert.ToInt32(Session["BuyerParent"]);

            ObjectDataSource odsVinScanLog = new ObjectDataSource();
            odsVinScanLog.TypeName = "METAOPTION.BAL.CommonBAL";
            odsVinScanLog.SelectMethod = "GetVINScanLogForChildBuyerVer211";
            odsVinScanLog.SelectCountMethod = "GetVINScanLogVer211CountForChildBuyer";
            odsVinScanLog.EnablePaging = true;
            odsVinScanLog.SelectParameters.Add("VIN", String.IsNullOrEmpty(txtVIN.Text.Trim()) ? string.Empty : txtVIN.Text.Trim());
            odsVinScanLog.SelectParameters.Add("AddedBy", ddlAddedBy.SelectedValue);
            odsVinScanLog.SelectParameters.Add("EntityTypeID", DbType.Int32, Session["LoginEntityTypeID"].ToString());
            odsVinScanLog.SelectParameters.Add("EntityID", DbType.Int32, Session["UserEntityID"].ToString());
            odsVinScanLog.SelectParameters.Add("ParentEntityID", ParentBuyerID.ToString());
            odsVinScanLog.SelectParameters.Add("StartRowIndex", DbType.Int32, gvVinScanLog.PageIndex.ToString());
            odsVinScanLog.SelectParameters.Add("MaximumRows", DbType.Int32, gvVinScanLog.PageSize.ToString());
            odsVinScanLog.SelectParameters.Add("OrgID", DbType.Int16, Constant.OrgID.ToString());
            gvVinScanLog.DataSource = odsVinScanLog;
            gvVinScanLog.DataBind();

        }
        #endregion

        #region[Paging]
        protected void gvVinScanLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVinScanLog.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvVinScanLog.PageIndex = 0;
            BindGrid();
        }

        public string FormatDeviceID(object oDeviceID)
        {
            String DeviceID = Convert.ToString(oDeviceID);
            string strDeviceID = String.Empty;
            if (!string.IsNullOrEmpty(DeviceID))
            {
                if (DeviceID != "5" && DeviceID.Length > 5)
                    strDeviceID = DeviceID.Substring(0, 5);
                else
                    strDeviceID = String.Empty;
            }
            return strDeviceID;
        }
    }
}