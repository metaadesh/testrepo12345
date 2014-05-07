using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using METAOPTION.BAL;
using METAOPTION.DAL;
namespace METAOPTION.UI
{
   public partial class NotesPopUp : System.Web.UI.Page
   {
      Int32 invID = 0;
      protected void Page_Load(object sender, EventArgs e)
      {
         
         if (Request.QueryString["Inventoryid"] != null)
            invID = Convert.ToInt32(Request.QueryString["Inventoryid"]);

         GetNotes(invID);
      }

      private void GetNotes(Int32 invID)
      {
         gvNotes.DataSource = InventoryBAL.Notes_ByInventoryId(invID, 6);
         gvNotes.DataBind();
      }

      protected void gvNotes_PageIndexChanging(object sender, GridViewPageEventArgs e)
      {
         this.gvNotes.PageIndex = e.NewPageIndex;
         gvNotes.DataSource = InventoryBAL.Notes_ByInventoryId(this.invID, 6);
         gvNotes.DataBind();
      }
   }
}
