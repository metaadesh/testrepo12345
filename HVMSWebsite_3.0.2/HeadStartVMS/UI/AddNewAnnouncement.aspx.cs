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
using METAOPTION;
using METAOPTION.BAL;
using System.Text;
namespace HeadStartVMS.UI
{
    public partial class AddNewAnnouncement : System.Web.UI.Page
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
            #region [ Page Load ]
            if (!IsPostBack)
            {
                BindAllEmployeeNameAndEmail();
            }
            #endregion
        }

        #region [ Add new Announcement ]
        protected void btnSaveAnnouncement_Click(object sender, EventArgs e)
        {
            String AnnouncementType = Convert.ToString(Request.QueryString["AnnouncementType"]);

            LaneAssignmentBAL objAdd = new LaneAssignmentBAL();
            Announcement objAnnounce = new Announcement();
            objAnnounce.AnnouncementTypeID = 1;
            if (objAnnounce.AnnouncementTypeID == null)
            {
                objAnnounce.AnnouncementTypeID = 1;
            }
            objAnnounce.AnnouncementTitle = txtTitle.Text;
            objAnnounce.Description = txtDescription.Text;
            objAnnounce.AddedBy = Constant.UserId;
            objAnnounce.OrgID = Constant.OrgID;
            objAdd.AddAnnouncement(objAnnounce);

            //Send Email to selected Users
            SendEmail_ToUsers();

            txtTitle.Text = string.Empty;
            txtDescription.Text = string.Empty;
            chkSendEmail.Checked = false;
            Response.Redirect("ViewAnnouncementList.aspx?Type=ViewAll");

            Cache.Remove(CacheEnum.BuyerAnnouncement);
        }

        #endregion

        #region [ Get Announcement type ]
        private int GetAnnouncementType()
        {
            Int32 ATypeId = 0;

            return ATypeId;

        }
        #endregion

        #region [ Bind Employee email ]
        void BindAllEmployeeNameAndEmail()
        {
            LaneAssignmentBAL objEmail = new LaneAssignmentBAL();
            lstEmployeeList.Items.Clear();
            lstEmployeeList.DataValueField = "Email1";
            lstEmployeeList.DataTextField = "FullName";
            lstEmployeeList.DataSource = objEmail.SelectAllEmployeeEmailLists(Constant.OrgID);
            lstEmployeeList.DataBind();
        }
        #endregion

        //#region [ To Send Email ]
        //protected void chkSendEmail_CheckedChanged(object sender, EventArgs e)
        //{
        //    //StringBuilder To = new StringBuilder();
        //    //string separator = ",";
        //    ////Put here from Email Address
        //    //string From = "sender@emailid.com";
        //    //string EmailBody = txtDescription.Text;
        //    //string subject = txtTitle.Text;
        //    //if (chkSendEmail.Checked == true)
        //    //{
        //    //    //Checking here to collect all employee emails or only selected employee emails
        //    //    if (ddlEmailOption.SelectedItem.Text.Trim().ToLower() == "send to all")
        //    //    {
        //    //        foreach (ListItem item in lstEmployeeList.Items)
        //    //        {
        //    //            if (item.Value.ToString() != "")
        //    //                if (System.Text.RegularExpressions.Regex.IsMatch(item.Value.ToString(), @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
        //    //                {
        //    //                    try
        //    //                    {
        //    //                        METAOPTION.EmailMessage.sendEmail(EmailBody, subject, To.ToString());
        //    //                    }
        //    //                    catch (Exception ex)
        //    //                    {
        //    //                        //TO DO
        //    //                    }
        //    //                }
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        //Get email only selected employee
        //    //        foreach (ListItem item in lstEmployeeList.Items)
        //    //        {
        //    //            if (item.Selected == true)
        //    //            {
        //    //                if (item.Value.ToString() != "")
        //    //                    if (System.Text.RegularExpressions.Regex.IsMatch(item.Value.ToString(), @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
        //    //                    {
        //    //                        try
        //    //                        {
        //    //                            METAOPTION.EmailMessage.sendEmail(EmailBody, subject, To.ToString());
        //    //                        }
        //    //                        catch (Exception ex)
        //    //                        {
        //    //                            //To DO
        //    //                        }
        //    //                    }
        //    //            }
        //    //        }
        //    //    }
        //    }


        //}
        //#endregion


        /// <summary>
        /// This method will send Email to selected users for Current Annoucement to be added.
        /// </summary>
        protected void SendEmail_ToUsers()
        {
            StringBuilder To = new StringBuilder();
            string separator = ",";
            //Put here from Email Address
            string From = "sender@emailid.com";
            string EmailBody = txtDescription.Text;
            string subject = txtTitle.Text;
            if (chkSendEmail.Checked == true)
            {
                //Checking here to collect all employee emails or only selected employee emails
                if (ddlEmailOption.SelectedIndex == 0)
                {
                    foreach (ListItem item in lstEmployeeList.Items)
                    {
                        if (item.Value.ToString() != "")
                            if (System.Text.RegularExpressions.Regex.IsMatch(item.Value.ToString(), @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
                            {
                                try
                                {
                                    //Corrected By Nitin Paliwal,22 March 2010
                                    METAOPTION.EmailMessage.sendEmail(EmailBody, subject, item.Value);
                                    // METAOPTION.EmailMessage.sendEmail(EmailBody, subject, To.ToString());
                                }
                                catch (Exception ex)
                                {
                                    //TO DO
                                }
                            }
                    }
                }
                else
                {
                    //Send Email to selected Employees
                    foreach (ListItem item in lstEmployeeList.Items)
                    {
                        if (item.Selected == true)
                        {
                            if (item.Value.ToString() != "")
                                if (System.Text.RegularExpressions.Regex.IsMatch(item.Value.ToString(), @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
                                {
                                    try
                                    {
                                        //Corrected By Nitin Paliwal,22 March 2010
                                        METAOPTION.EmailMessage.sendEmail(EmailBody, subject, item.Value);
                                        // METAOPTION.EmailMessage.sendEmail(EmailBody, subject, To.ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        //To DO
                                    }
                                }
                        }
                    }//  foreach (ListItem item in lstEmployeeList.Items)
                }
            }//if (chkSendEmail.Checked == true)
        }
    }
}
