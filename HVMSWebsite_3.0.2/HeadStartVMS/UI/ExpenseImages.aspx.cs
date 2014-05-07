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
using System.Collections.Generic;
using System.Text;
using System.Web.Services;
using METAOPTION.UserControls;

namespace METAOPTION.UI
{
    public partial class ExpenseImages : System.Web.UI.Page
    {
        String ServerUrl = System.Configuration.ConfigurationManager.AppSettings["ExpenseImagePath"];

        ExpenseBAL ExpBAL = new ExpenseBAL();
        System.Collections.ArrayList arraylist = new ArrayList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.HasKeys())
                {
                    arraylist = ExpBAL.ExpenseImages(Convert.ToInt64(Request.QueryString["id"]));
                    dlthumb.DataSource = arraylist[0];
                    dlthumb.DataBind();
                }
            }
        }

        public String GetImagePath(object Path)
        {
            return String.Format("{0}{1}", ServerUrl, Path);
        }
        
    }
}