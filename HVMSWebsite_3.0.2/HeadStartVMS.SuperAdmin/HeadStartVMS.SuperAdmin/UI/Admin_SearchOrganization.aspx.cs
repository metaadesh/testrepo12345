using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace METAOPTION.UI
{
    public partial class Admin_SearchOrganization : System.Web.UI.Page
    {
        METAOPTION.BAL.Admin_OrganizationBAL ObjBAL = new BAL.Admin_OrganizationBAL();

        #region[Page Load Event]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(IsPostBack || IsCallback))
            {
                xBindGrid();
            }
        }
        #endregion

        #region[Gridview paging]
        protected void pageindexchanging(object sender, GridViewPageEventArgs e)
        {
            gv_org.PageIndex = e.NewPageIndex;
            BindGrid();

        }
        #endregion

        #region[Sorting]
        protected void gv_org_sort(object sender, GridViewSortEventArgs e)
        {

            string sortExpression = e.SortExpression;
            string direction = string.Empty;

            if (SortDirection == SortDirection.Ascending)
            {
                SortDirection = SortDirection.Descending;
                direction = " DESC";
            }
            else
            {
                SortDirection = SortDirection.Ascending;
                direction = " ASC";
            }


            BindGrid();
            DataTable table = new DataTable();
            table = gv_org.DataSource as DataTable;
            table.DefaultView.Sort = sortExpression + direction;

            gv_org.DataSource = table;
            gv_org.DataBind();
        }

        public SortDirection SortDirection
        {
            get
            {
                if (ViewState["SortDirection"] == null)
                {
                    ViewState["SortDirection"] = SortDirection.Descending;
                }
                return (SortDirection)ViewState["SortDirection"];
            }
            set
            {
                ViewState["SortDirection"] = value;
            }
        }

        #endregion

        #region[Simple GridView Bind ]
        private void BindGrid()
        {
            DataTable dt = new DataTable();
            dt = ObjBAL.ShowOrganization(txtOrganization.Text.Trim(), txtcode.Text.Trim(), txtphone.Text.Trim(), txtwebsite.Text.Trim(), txtemail.Text.Trim());

            gv_org.DataSource = dt;
            gv_org.DataBind();
        }
        #endregion

        #region[Bind GridView on pageLoad event]
        private void xBindGrid()
        {
            DataTable dt = new DataTable();
            dt = ObjBAL.ShowOrganization(txtOrganization.Text.Trim(), txtcode.Text.Trim(), txtphone.Text.Trim(), txtwebsite.Text.Trim(), txtemail.Text.Trim());

            DataView dv = new DataView(dt);
            dv.Sort = "DateAdded DESC";

            gv_org.DataSource = dv;
            gv_org.DataBind();
        }
        #endregion

        #region[Grid View RowDataBound Event]
        protected void gv_org_rowdatabound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField isactive = (HiddenField)e.Row.FindControl("hdnIsActive");
                ImageButton ImageBtn_active = (ImageButton)e.Row.FindControl("ImageBtn_active");
                if (isactive.Value == "0")
                {
                    ImageBtn_active.ImageUrl = "~/Images/Inactive_grey.png";
                    ImageBtn_active.ToolTip = "Click to activate";
                }
                else
                {
                    ImageBtn_active.ImageUrl = "~/Images/H_active.png";
                    ImageBtn_active.ToolTip = "Click to deactivate";
                }

                HiddenField hAllowLaneAutomation = (HiddenField)e.Row.FindControl("hAllowLaneAutomation");
                ImageButton imgbtnLaneAutomation = (ImageButton)e.Row.FindControl("imgbtnLaneAutomation");
                if (hAllowLaneAutomation.Value == "False")
                {
                    imgbtnLaneAutomation.ImageUrl = "~/Images/Inactive_grey.png";
                    imgbtnLaneAutomation.ToolTip = "Click to allow lane automation";
                }
                else
                {
                    imgbtnLaneAutomation.ImageUrl = "~/Images/H_active.png";
                    imgbtnLaneAutomation.ToolTip = "Click to deny lane automation";
                }


                HiddenField hAllowMAA = (HiddenField)e.Row.FindControl("hAllowMAA");
                ImageButton imgbtnLaneMAA = (ImageButton)e.Row.FindControl("imgbtnLaneMAA");
                if (hAllowMAA.Value == "False")
                {
                    imgbtnLaneMAA.ImageUrl = "~/Images/Inactive_grey.png";
                    imgbtnLaneMAA.ToolTip = "Click to allow MAA";
                }
                else
                {
                    imgbtnLaneMAA.ImageUrl = "~/Images/H_active.png";
                    imgbtnLaneMAA.ToolTip = "Click to deny MAA";
                }

            }

        }
        #endregion

        #region[Search Button Action]
        protected void btnSearchOrganization_Click(object sender, EventArgs e)
        {
            BindGrid();
            //if (gv_org.DataSource == null)
            //{
            //    Response.Redirect("No Record Found");
            //}
        }
        #endregion

        #region[Action Image]
        protected void Org_Edit_click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgedit = (ImageButton)sender;
            GridViewRow gvrow = (GridViewRow)imgedit.NamingContainer;
            ////string Orgname = gv_org.Rows[gvrow.RowIndex].Cells[0].Text;
            ////string Orgcode = gv_org.Rows[gvrow.RowIndex].Cells[1].Text;

            Int16 orgid = (Int16)gv_org.DataKeys[gvrow.RowIndex].Value;
            Response.Redirect("Admin_AddOrganization.aspx?mode=edit" + "&org=" + orgid);

        }
        protected void ImageBtn_active_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imagebtn = (ImageButton)sender;
            GridViewRow rowdata = (GridViewRow)imagebtn.NamingContainer;

            HiddenField isactive = (HiddenField)gv_org.Rows[rowdata.RowIndex].FindControl("hdnIsActive");
            Int16 orgid = (Int16)gv_org.DataKeys[rowdata.RowIndex].Value;

            if (isactive.Value == "0")
            {
                Int16 changeStatus = 1;
                ObjBAL.Manage_Image(orgid, changeStatus);
                imagebtn.ImageUrl = "~/Images/H_active.png";
            }

            else
            {
                Int16 changeStatus = 0;
                ObjBAL.Manage_Image(orgid, changeStatus);
                imagebtn.ImageUrl = "~/Images/Inactive_grey.png";

            }
            xBindGrid();
        }
        protected void ImageBtn_delete_click(object sender, ImageClickEventArgs e)
        {

            ImageButton imagebtn = (ImageButton)sender;
            GridViewRow rowdata = (GridViewRow)imagebtn.NamingContainer;
            //string orgname = gv_org.Rows[rowdata.RowIndex].Cells[0].Text;
            //string orgcode = gv_org.Rows[rowdata.RowIndex].Cells[1].Text;
            Int64 loginID = Constant.UserId;
            //Int16 loginid = Convert.ToInt16(Session["empId"].ToString());
            Int16 orgid = (Int16)gv_org.DataKeys[rowdata.RowIndex].Value;

            if (orgid != 0)
            {
                Int16 status = -1;
                ObjBAL.Delete_Organization(orgid, status, loginID);
            }
            xBindGrid();
        }
        #endregion

        #region Summary button click event
        protected void imgbtnSummary_Click(object sender, EventArgs e)
        {
            ImageButton imgSummary = (ImageButton)sender;
            GridViewRow gvrow = (GridViewRow)imgSummary.NamingContainer;
            Int16 OrgID = (Int16)gv_org.DataKeys[gvrow.RowIndex].Value;
            this.Context.Items["SummaryOrgID"] = OrgID.ToString();
            Server.Transfer("Admin_Summary.aspx");
        }
        #endregion


        protected void imgbtnLaneAutomation_Click(object sender, EventArgs e)
        {
            ImageButton imagebtn = (ImageButton)sender;
            GridViewRow rowdata = (GridViewRow)imagebtn.NamingContainer;
            HiddenField hAllowLaneAutomation = (HiddenField)gv_org.Rows[rowdata.RowIndex].FindControl("hAllowLaneAutomation");
            Int16 OrgID = (Int16)gv_org.DataKeys[rowdata.RowIndex].Value;

            if (hAllowLaneAutomation.Value == "False")
            {
                ObjBAL.AllowDeny_Lane_MAA(OrgID, true, "LANE");
            }

            else
            {
                ObjBAL.AllowDeny_Lane_MAA(OrgID, false, "LANE");
            }
            xBindGrid();
        }

        protected void imgbtnLaneMAA_Click(object sender, EventArgs e)
        {
            ImageButton imagebtn = (ImageButton)sender;
            GridViewRow rowdata = (GridViewRow)imagebtn.NamingContainer;
            HiddenField hAllowMAA = (HiddenField)gv_org.Rows[rowdata.RowIndex].FindControl("hAllowMAA");
            Int16 OrgID = (Int16)gv_org.DataKeys[rowdata.RowIndex].Value;

            if (hAllowMAA.Value == "False")
            {
                ObjBAL.AllowDeny_Lane_MAA(OrgID, true, "MAA");
            }

            else
            {
                ObjBAL.AllowDeny_Lane_MAA(OrgID, false, "MAA");
            }
            xBindGrid();
        }

    }
}