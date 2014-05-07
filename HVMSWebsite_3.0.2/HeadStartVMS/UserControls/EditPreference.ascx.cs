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

namespace METAOPTION.UserControls
{
    public partial class EditPreference : System.Web.UI.UserControl
    {
        public string EntityId = string.Empty;
             
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["EntityId"] != null)
            {
                EntityId = Request["EntityId"];
                // type = Request["type"];
            }
            if (Convert.ToString(Session["LoginEntityTypeID"]) == "1")
            {
                editpreprence.Visible = false;
            }
        }
    }
}