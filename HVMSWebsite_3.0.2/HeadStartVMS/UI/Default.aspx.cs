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
using METAOPTION;
using METAOPTION.BAL;
using SSOLib;

namespace HeadStartVMS.UI
{
    public partial class HomePage : System.Web.UI.Page
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
            if (!String.IsNullOrEmpty(Convert.ToString(Session["empID"])))
            {
                try
                {
                    string partialPath = Request.Url.Scheme + "://" + Request.Url.Authority;
                    string LoginSessionID = EncryptMD5.Encrypt(Convert.ToString(Session["empID"]));
                    String LaneNumberingURL = System.Configuration.ConfigurationManager.AppSettings["HVMSWebLaneNumberingURL"].ToString();
                    if (Request.Url.Authority.ToLower().Contains("localhost"))
                    {
                        hlnkLaneAssignmentImg.NavigateUrl = partialPath + LaneNumberingURL + LoginSessionID;
                        hlnkLaneAssignmentHead.NavigateUrl = partialPath + LaneNumberingURL + LoginSessionID;
                        //hlnkLaneAssignmentImg.NavigateUrl = partialPath + "/Home/SessionLogin?SessionID=" + LoginSessionID;
                        //hlnkLaneAssignmentHead.NavigateUrl = partialPath + "/Home/SessionLogin?SessionID=" + LoginSessionID;
                    }
                    else
                    {
                        hlnkLaneAssignmentImg.NavigateUrl = partialPath + LaneNumberingURL + LoginSessionID;
                        hlnkLaneAssignmentHead.NavigateUrl = partialPath + LaneNumberingURL + LoginSessionID;
                        //hlnkLaneAssignmentImg.NavigateUrl = partialPath + "/WebLaneNumbering/Home/SessionLogin?SessionID=" + LoginSessionID;
                        //hlnkLaneAssignmentHead.NavigateUrl = partialPath + "/WebLaneNumbering/Home/SessionLogin?SessionID=" + LoginSessionID;
                    }
                }
                catch { }
            }
            CheckPermission();
        }

        #region[Check Permission]
        private void CheckPermission()
        {
            // Check if the user has right to access Lane Automation Setting
            List<String> Permissions = CommonBAL.GetPagePermission(Constant.UserId, "WEBAPP.LANEAUTOMATION");
            if (!(Permissions.Contains("LANEAUTOMATION.SETTING")))
                trLaneAutomationSetting.Visible = false;

            // Check if the user has right to access Apply Lane Automation
            if (!(Permissions.Contains("LANEAUTOMATION.APPLYRULES")))
                trApplyLaneAutomation.Visible = false;

            try
            {
                //check whether user has LaneNumbering-No-Access Group Assigned or not, if assigned then restrict the user to open this project
                MakeUserBAL objUserBAL = new MakeUserBAL();
                IQueryable result = objUserBAL.GetAssociatedGroups(Constant.UserId);
                IEnumerator enumrtor = result.GetEnumerator();
                //[System.Data.Linq.SqlClient.ObjectReaderCompiler.ObjectReader<System.Data.SqlClient.SqlDataReader,METAOPTION.GetAssociatedGroupsWithUserResult>]	{System.Data.Linq.SqlClient.ObjectReaderCompiler.ObjectReader<System.Data.SqlClient.SqlDataReader,METAOPTION.GetAssociatedGroupsWithUserResult>}	System.Data.Linq.SqlClient.ObjectReaderCompiler.ObjectReader<System.Data.SqlClient.SqlDataReader,METAOPTION.GetAssociatedGroupsWithUserResult>
                METAOPTION.GetAssociatedGroupsWithUserResult curObj;
                hlnkLaneAssignmentImg.Enabled = true;
                hlnkLaneAssignmentHead.Enabled = true;
                while (enumrtor.MoveNext())
                {
                    curObj = (GetAssociatedGroupsWithUserResult)enumrtor.Current;
                    if (curObj.SecurityGroupID == 50)
                    {
                        hlnkLaneAssignmentImg.Enabled = false;
                        hlnkLaneAssignmentHead.Enabled = false;
                    }
                }
            }
            catch { }
        }
        #endregion

    }
}
