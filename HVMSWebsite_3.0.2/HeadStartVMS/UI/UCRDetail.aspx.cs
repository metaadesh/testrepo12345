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
using METAOPTION.UCR;

namespace METAOPTION.UI
{
    public partial class UCRDetail : System.Web.UI.Page
    {
        PreInventoryBAL PreInvBAL = new PreInventoryBAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            //TO DO : Add Authentication
            int CRID = 0;
            String VIN = String.Empty;
            String URL = String.Empty;

            if (!IsPostBack)
            {
                if (Request.QueryString.HasKeys())
                {
                    string strHostName = System.Net.Dns.GetHostName();

                    UCR.UserCredentials user = new UCR.UserCredentials();
                    user.DealerCode = ConfigurationManager.AppSettings["DealerCode"];
                    user.SecurityCode = ConfigurationManager.AppSettings["SecurityCode"];
                    user.LegacySystemCode = ConfigurationManager.AppSettings["LegacySystemCode"];
                    //user.IPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(1).ToString();
                    user.IPAddress = ConfigurationManager.AppSettings["DealerIPAddress"];


                    UCR.LinkCRService service = new LinkCRService();
                    service.UserCredentialsValue = user;

                    UCR.ServiceResponce response = null;

                    if (Request.QueryString["type"] == "1") //By CRID
                    {
                        if (Request.QueryString["value"] != null)
                            CRID = Convert.ToInt32(Request.QueryString["value"]);
                        response = service.GetUCRDetailToLink(CRID, "", 1);
                    }
                    else if (Request.QueryString["type"] == "2") //By CRURL
                    {
                        URL = Request.QueryString["value"];
                        response = service.VerifyURL(URL);
                    }
                    else if (Request.QueryString["type"] == "3") //By VIN
                    {
                        VIN = Request.QueryString["value"];
                        response = service.GetUCRDetailToLink(0, VIN, 4);
                    }
                    if (response.Success)
                    {
                        grducrdetail.DataSource = response.Records;
                        grducrdetail.DataBind();
                    }
                    else
                    {
                        lblCRError.Text = response.Message;
                    }
                }
            }
        }

        protected void btnok_Click(object sender, EventArgs e)
        {
            Int32 CR_ID = 0;
            String CR_URL = String.Empty;
            Int32 CR_Status = 0;
            Int64 PreInvID = 0, InvID = 0;

            for (int i = 0; i < grducrdetail.Rows.Count; i++)
            {
                RadioButton rdbtn = (RadioButton)grducrdetail.Rows[i].FindControl("rdbtn");
                if (rdbtn.Checked == true)
                {
                    GridViewRow row = (GridViewRow)grducrdetail.Rows[i];
                    CR_ID = Convert.ToInt32(row.Cells[1].Text);
                    CR_URL = row.Cells[9].Text;
                    CR_Status = Convert.ToInt32(row.Cells[10].Text);
                    HtmlAnchor aurl = (HtmlAnchor)row.FindControl("aurl");
                    CR_URL = aurl.HRef;

                    break;
                }
            }

            if (Request.QueryString.HasKeys())
            {
                if (Request.QueryString["invtype"].ToLower() == "p")
                {
                    PreInvID = Convert.ToInt64(Request.QueryString["id"]);

                    //Mobile_PreInventory objInv = new Mobile_PreInventory();
                    //objInv = PreInvBAL.GetPreInventoryByID(PreInvID);
                    //InvID = Convert.ToInt64(objInv.InventoryId);
                }
                else if (Request.QueryString["invtype"].ToLower() == "i")
                {
                    InvID = Convert.ToInt64(Request.QueryString["id"]);
                }

            }

            PreInvBAL.LinkCR(PreInvID, InvID, Convert.ToInt64(Session["empId"]), CR_ID, CR_URL, CR_Status);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "closepop", "ClosePopUpWindow();", true);
        }
    }
}