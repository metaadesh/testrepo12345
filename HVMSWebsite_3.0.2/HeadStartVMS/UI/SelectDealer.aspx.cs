using System;
using System.Collections;
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
using METAOPTION.BAL;
using System.Text;

namespace METAOPTION.UI
{
   public partial class SelectDealer : System.Web.UI.Page
   {
      public String IdControlId = string.Empty;
      public string NameControlId = string.Empty;
      protected void Page_Load(object sender, EventArgs e)
      {
         if (Request["IdControl"] != null)
            this.IdControlId = Convert.ToString(Request["IdControl"]);

         if (Request["NameControl"] != null)
            this.NameControlId = Convert.ToString(Request["NameControl"]);
         if (!Page.IsPostBack)
         {
            FillCountry();
            SearchDealer();
         }
      }

      #region[ Search Dealer ]
      /// <summary>
     /// Search Dealer
     /// </summary>
     /// <param name="sender"></param>
     /// <param name="e"></param>
      protected void SearchDealer()
      {
         gvDealerDetails.DataSource = InventoryBAL.SearchDealer(
            this.txtDealerName.Text.Trim()
            , this.txtCity.Text.Trim()
            , Convert.ToInt16(this.ddlDealerState.SelectedValue)
            , this.txtZip.Text.Trim()
            , Convert.ToInt16(this.ddlCountry.SelectedValue) );
         gvDealerDetails.DataBind();
      }
      protected void btnSearchDealers_Click(object sender, EventArgs e)
      {
         SearchDealer();
      }
      #region[ Page Index Change ]
      protected void gvDealerDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
      {
         this.gvDealerDetails.PageIndex = e.NewPageIndex;
         SearchDealer();
      }
      #endregion
      #endregion

      #region [ Fill Country & State ]
      private void FillCountry()
      {
         this.ddlCountry.DataSource = BAL.Common.CountryList();
         this.ddlCountry.DataTextField = "CountryName";
         this.ddlCountry.DataValueField = "CountryId";
         this.ddlCountry.DataBind();
         this.ddlCountry.Items.Insert(0, new ListItem("", "-1"));
         this.ddlDealerState.Items.Insert(0, new ListItem("", "-1"));
      }
      protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
      {
         this.ddlDealerState.Items.Clear();
         this.ddlDealerState.DataSource = BAL.Common.GetStateList(Convert.ToInt16(this.ddlCountry.SelectedValue));
         this.ddlDealerState.DataTextField = "State";
         this.ddlDealerState.DataValueField = "StateId";
         this.ddlDealerState.DataBind();
         this.ddlDealerState.Items.Insert(0, new ListItem("", "-1"));
      }
      #endregion
   }
}
