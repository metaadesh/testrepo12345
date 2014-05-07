using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using METAOPTION.BAL;
using System.Data;
using System.Data.SqlClient;
using METAOPTION;
using System.Collections;


namespace METAOPTION.UI
{
    public partial class Admin_MasterLeftPanel : System.Web.UI.MasterPage
    {
        Admin_LoginBAL objBAL = new Admin_LoginBAL();
        METAOPTION.BAL.Admin_Common objCommon = new Admin_Common();
        #region[ Page Load Event]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLeftPanelControlsValue();
                BindRepeterControl();
            }

            if (Session["empId"] == null)
                Response.Redirect("~/Admin_Login.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
            else
            {
                lbl_Welcome_Disname.Text = "Welcome " + Constant.UserDisplayName;
                lbl_LastLoginTime.Text = "Last Login was on: " + objBAL.GetLastLogin(Constant.UserId);
            }
        }
        #endregion

        #region[Bind Left panel value]
        private void BindLeftPanelControlsValue()
        {
            Admin_OrganizationBAL BAL = new Admin_OrganizationBAL();
            Hashtable ht = BAL.Admin_GetLeftPanelValue();
            lblNoOfUsers.Text = ht["ActiveUsers"] + " Users";
            lblOrganizations.Text = ht["ActiveOrganizations"] + " Organizations";
            lblSystems.Text = ht["ActiveSystems"] + "  Systems";
        }
        #endregion

        #region[Logout Button Click Event]
        protected void link_logout_click(object sender, EventArgs e)
        {
            try
            {
                Int64 LoginHistoryId = Convert.ToInt64(System.Web.HttpContext.Current.Session["LoginHistoryId"].ToString());
                Admin_LoginBAL.Logout_Session(LoginHistoryId);
            }
            catch { }
            System.Web.HttpContext.Current.Session.Abandon();
            System.Web.Security.FormsAuthentication.SignOut();
            System.Web.HttpContext.Current.Response.Redirect("~/Admin_Login.aspx?ReturnUrl="+HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
            //System.Web.HttpContext.Current.Response.Redirect("~/Admin_Login.aspx", true);

        }
        #endregion

        #region[image Click Event at Leftpanel in repeter control]
        protected void img_org_click(object sender, ImageClickEventArgs e)
        {


            //ImageButton imgbtn = sender as ImageButton;
            //int i = Convert.ToInt32(((ImageButton)sender).CommandArgument);

            //imgbtn.Attributes.Add("onclick", "javascript:HideAllEntity(true)");

            //string imgurl = imgbtn.ImageUrl;
            //if (imgurl == "../Images/expand.png")
            //{
            //    HideAllRows();
            //    imgbtn.ImageUrl = "../Images/collapse.png";

            //HiddenField hdn = (HiddenField)repeter_inentities.Items[i - 1].FindControl("hdnORGID");
            //Int16 ORGID = Convert.ToInt16(hdn.Value);
            //    DataTable dt = objCommon.GetEmpCount(ORGID);

            //    Label dealer = (Label)repeter_inentities.Items[i - 1].FindControl("lbl_dealer");
            //    Label buyer = (Label)repeter_inentities.Items[i - 1].FindControl("lbl_buyer");
            //    Label vendor = (Label)repeter_inentities.Items[i - 1].FindControl("lbl_vendor");
            //    Label uc = (Label)repeter_inentities.Items[i - 1].FindControl("lbl_uc");
            //    Label employee = (Label)repeter_inentities.Items[i - 1].FindControl("lbl_emp");

            //    dealer.Text = "Dealer/Customer " + " (" + dt.Rows[0]["Customer"].ToString() + ")";
            //    buyer.Text = "Buyer " + " (" + dt.Rows[0]["Buyer"].ToString() + ")";
            //    vendor.Text = "Vendor " + " (" + dt.Rows[0]["Vendor"].ToString() + ")";
            //    uc.Text = "Utility Company " + " (" + dt.Rows[0]["Utility Company"].ToString() + ")";
            //    employee.Text = "Employee " + " (" + dt.Rows[0]["Employee"].ToString() + ")";

            //System.Web.UI.HtmlControls.HtmlAnchor Acutomer = (System.Web.UI.HtmlControls.HtmlAnchor)repeter_inentities.Items[i - 1].FindControl("hrefcustomer");
            //Acutomer.HRef = generateUrl(ORGID, 1);
            //System.Web.UI.HtmlControls.HtmlAnchor Abuyer = (System.Web.UI.HtmlControls.HtmlAnchor)repeter_inentities.Items[i - 1].FindControl("hrefbuyer");
            //Abuyer.HRef = generateUrl(ORGID, 2);
            //System.Web.UI.HtmlControls.HtmlAnchor Avendor = (System.Web.UI.HtmlControls.HtmlAnchor)repeter_inentities.Items[i - 1].FindControl("hrefvendor");
            //Avendor.HRef = generateUrl(ORGID, 3);
            //System.Web.UI.HtmlControls.HtmlAnchor Auc = (System.Web.UI.HtmlControls.HtmlAnchor)repeter_inentities.Items[i - 1].FindControl("hrefuc");
            //Auc.HRef = generateUrl(ORGID, 4);
            //System.Web.UI.HtmlControls.HtmlAnchor Aemployee = (System.Web.UI.HtmlControls.HtmlAnchor)repeter_inentities.Items[i - 1].FindControl("hrefemployee");
            //Aemployee.HRef = generateUrl(ORGID, 5);

            //    System.Web.UI.HtmlControls.HtmlTableRow tr = (System.Web.UI.HtmlControls.HtmlTableRow)repeter_inentities.Items[i - 1].FindControl("show_entity");
            //    tr.Visible = true;
            //}

            //else if (imgurl == "../Images/collapse.png")
            //{
            //    imgbtn.ImageUrl = "../Images/expand.png";
            //    System.Web.UI.HtmlControls.HtmlTableRow tr = (System.Web.UI.HtmlControls.HtmlTableRow)repeter_inentities.Items[i - 1].FindControl("show_entity");
            //    tr.Visible = false;
            //}
        }
        #endregion

        #region[Bind Repeter Control at LeftPanel]
        protected void BindRepeterControl()
        {
            METAOPTION.BAL.Admin_Common obj = new Admin_Common();
            string MODE = "DISPLAY";
            DataTable dt = obj.GetOrganizations(MODE);
           // var abc = dt.AsEnumerable().Take(5).ToList();
            //DataRow[] drow =
            DataTable dtnew = new DataTable();
          //dtnew=  dt.Rows.OfType<DataRow>() >.Take(5).CopyToDataTable();

            dtnew = dt.AsEnumerable().Take(5).CopyToDataTable();

            //IEnumerable<DataRow> eRow = dt.AsEnumerable().Take(5);
            repeter_inentities.DataSource = dtnew;
            repeter_inentities.DataBind();
            hNoSystem.Value = dtnew.Rows.Count.ToString();
            int rowcount = dt.Rows.Count;
            //hlk_moremsg.Visible = true;
            if (rowcount > 5)
            {
               
                hlk_moremsg.Visible = true;
            }
            else
            {
                hlk_moremsg.Visible =false;
            }
     
        }
        #endregion

        #region[OnItemDataBound event of Repeter Control]
        protected void repeter_itemdatabound(object sender, RepeaterItemEventArgs e)
        {
            HiddenField hdn = (HiddenField)e.Item.FindControl("hdnORGID");
            HiddenField hdnbuyer = (HiddenField)e.Item.FindControl("hdn_buyer");
            HiddenField hdndealer = (HiddenField)e.Item.FindControl("hdn_dealer");
            HiddenField hdnemployee = (HiddenField)e.Item.FindControl("hdn_employee");
            HiddenField hdnuc = (HiddenField)e.Item.FindControl("hdn_uc");
            HiddenField hdnvendor = (HiddenField)e.Item.FindControl("hdn_vendor");

            Int32 count_buyer = Convert.ToInt32(hdnbuyer.Value);
            Int32 count_dealer = Convert.ToInt32(hdndealer.Value);
            Int32 count_employee = Convert.ToInt32(hdnemployee.Value);
            Int32 count_uc = Convert.ToInt32(hdnuc.Value);
            Int32 count_vendor = Convert.ToInt32(hdnvendor.Value);

            Int16 ORGID = Convert.ToInt16(hdn.Value);
            RepeaterItem item = e.Item;

            int a = repeter_inentities.Items.Count;
            if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
            {
                System.Web.UI.HtmlControls.HtmlAnchor Acutomer = (System.Web.UI.HtmlControls.HtmlAnchor)item.FindControl("hrefcustomer");
                Acutomer.HRef = generateUrl(ORGID, 1, count_dealer);
                System.Web.UI.HtmlControls.HtmlAnchor Abuyer = (System.Web.UI.HtmlControls.HtmlAnchor)item.FindControl("hrefbuyer");
                Abuyer.HRef = generateUrl(ORGID, 2, count_buyer);
                System.Web.UI.HtmlControls.HtmlAnchor Avendor = (System.Web.UI.HtmlControls.HtmlAnchor)item.FindControl("hrefvendor");
                Avendor.HRef = generateUrl(ORGID, 3, count_vendor);
                System.Web.UI.HtmlControls.HtmlAnchor Auc = (System.Web.UI.HtmlControls.HtmlAnchor)item.FindControl("hrefuc");
                Auc.HRef = generateUrl(ORGID, 4, count_uc);
                System.Web.UI.HtmlControls.HtmlAnchor Aemployee = (System.Web.UI.HtmlControls.HtmlAnchor)item.FindControl("hrefemployee");
                Aemployee.HRef = generateUrl(ORGID, 5, count_employee);
                
            }

        }
        #endregion

        #region[Create URL]
        public string generateUrl(Int16 ORGID, int type, Int32 TotalCount)
        {

            if (TotalCount > 0)
            {
                return ("Admin_SearchEntities.aspx?org=" + ORGID + "&type=" + type + "&status=" + "1");
            }
            else
            {
                return "";
            }
        }
        #endregion
    }
}