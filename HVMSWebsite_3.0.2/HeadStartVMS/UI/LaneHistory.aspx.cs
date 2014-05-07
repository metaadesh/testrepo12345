using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;  
using System.Xml.Linq;
using METAOPTION.DAL;
using METAOPTION.BAL;
namespace HeadStartVMS
{
    public partial class LaneHistory : System.Web.UI.Page
    {
       
        LaneAssignmentBAL ObjHist = new LaneAssignmentBAL();
        //GetMakeModelByInventoryIdBAL obkMake = new GetMakeModelByInventoryIdBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Int64 invetoryid = Convert.ToInt64(Request.QueryString["inventoryid"]);
                ViewState["invetoryid"] = invetoryid;
                BindGrid(invetoryid);
            }
        }
       
        void BindGrid(Int64 invetoryid)
        {
            List<string> lstMake = null;
            lstMake = ObjHist.GetMake(invetoryid);
            if (lstMake.Count > 0)
            {
                if (lstMake[0].ToString() == "0")
                {
                    lblYear.Text = "N/A";
                }
                else
                    lblYear.Text = lstMake[0].ToString();
                lblMake.Text = lstMake[1].ToString();
                lblModel.Text = lstMake[2].ToString();

            }


            gvLaneHistory.DataSource = ObjHist.SelectLaneAssignmentHistory(invetoryid);
            gvLaneHistory.DataBind();



        }
        protected void gvLaneHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gvLaneHistory.PageIndex = e.NewPageIndex;
            BindGrid(Convert.ToInt64(ViewState["invetoryid"]));
        }


    }
}
