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

namespace METAOPTION.UI
{
    public partial class AfterSalesInventoryHistory : System.Web.UI.Page
    {
        Int32 _code;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region [Set InventoryId by reading from QueryString]
            //Set InventoryId by reading from QueryString
            if (Request["Code"] != null)
                try
                {
                    this._code = Convert.ToInt32(Request["Code"]);

                }
                catch
                { }
            #endregion

            if (!IsPostBack)
            {
                //Show Inventory Header Information 
                lblInventoryHeader.Text = "List of Previous Changes :: For Inventory " + InventoryBAL.GetCurrentInventoryHeader(_code);

                //Load Chrome History Data
                LoadAfterSalesHistory();
            }
        }
        /// <summary>
        /// This method LoadChromeHistory in GridView Control
        /// </summary>
        private void LoadAfterSalesHistory()
        {
            gvAfterSalesInventoryHistory.DataSource =  AfterSalesManagementBAL.GetAfterSalesHistory(_code); ;
            gvAfterSalesInventoryHistory.DataBind();
        }
        /// <summary>
        /// Handle PageIndex Change event of Gridview control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvAfterSalesInventoryHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAfterSalesInventoryHistory.PageIndex = e.NewPageIndex;
            LoadAfterSalesHistory();
        }

        protected void lnkbtnBack_Click(object sender, EventArgs e)
        {

           // Response.Redirect("AfterSalesInventories.aspx");
        }
    }
}
