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

namespace METAOPTION.UI
{
    public partial class ViewBuyers : System.Web.UI.Page
    {
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

        }

        protected void grdViewBuyer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
 
           
        }

        protected void grdViewBuyer_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //// when mouse is over the row, save original color to new attribute, and change it to highlight yellow color

            //e.Row.Attributes.Add("onmouseover",

            // "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#E3F2F9';this.style.cursor='hand'");

            //// when mouse leaves the row, change the bg color to its original value    

            //e.Row.Attributes.Add("onmouseout",

            //"this.style.backgroundColor=this.originalstyle;");

        }

        protected void grdViewBuyer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView _gridView = (GridView)sender;

            // Get the selected index and the command name
            int _selectedIndex = int.Parse(e.CommandArgument.ToString());
            string _commandName = e.CommandName;
            long BuyerId = 0;

            //Current Row Selected Index
            _gridView.SelectedIndex = _selectedIndex;

            //Fetch Selected row values to be passed in querystring;
            //if (!string.IsNullOrEmpty(_gridView.Rows[_selectedIndex].Cells[1].Text))
            //    Year = Convert.ToInt32(_gridView.Rows[_selectedIndex].Cells[1].Text);


            //Redirect user to AddInventory Page with selected Year,Make,Mode,Body
            Response.Redirect("ViewBuyerDetails.aspx?BuyerId="+BuyerId );
        }

        /// <summary>
        /// Override Render event for Invalid Page Postback Event Valdidation error and set unique id
        /// </summary>
        /// <param name="writer"></param>
        //protected override void Render(HtmlTextWriter writer)
        //{
        //    foreach (GridViewRow r in gvVinMatchData.Rows)
        //    {
        //        if (r.RowType == DataControlRowType.DataRow)
        //        {
        //            Page.ClientScript.RegisterForEventValidation
        //                    (r.UniqueID + "$ctl00");

        //            //Page.ClientScript.RegisterForEventValidation
        //            //        (r.UniqueID + "$ctl01");
        //        }
        //    }

        //    base.Render(writer);
        //}

       
    }
}
