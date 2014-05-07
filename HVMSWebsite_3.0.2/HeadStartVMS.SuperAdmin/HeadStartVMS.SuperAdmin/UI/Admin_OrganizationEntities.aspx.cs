using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace METAOPTION.UI
{
    public partial class Admin_OrganizationEntities : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
        }
        private void BindGrid()
        {
            METAOPTION.BAL.Admin_Common objcommon = new BAL.Admin_Common();
            string MODE = "DISPLAY";
            gv_entity.DataSource = objcommon.GetOrganizations(MODE);
            gv_entity.DataBind();
        }
        protected void gv_entity_pageindexchange(object sender, GridViewPageEventArgs e)
        {
            gv_entity.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}