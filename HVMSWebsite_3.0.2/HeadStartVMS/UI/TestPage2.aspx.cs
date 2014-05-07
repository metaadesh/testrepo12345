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

namespace METAOPTION.UI
{
    public partial class TestPage2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ViewState["testValue"] != null)
                {
                    ddlTest.SelectedValue = ViewState["testValue"].ToString();
                
                }
            }
        }

        protected void test_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ViewState["testValue"] = ddlTest.SelectedValue;
            Response.Redirect("TestPage1.aspx");
        }
    }
}
