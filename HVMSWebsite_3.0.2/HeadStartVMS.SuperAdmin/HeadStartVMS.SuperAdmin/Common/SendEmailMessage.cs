using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net.Mail;


namespace METAOPTION.Common
{
    public class SendEmailMessage
    {
        #region[ Send Email ]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
        public static bool sendEmail(string mailBody, string mailSubject, String mailTo)
        {
            MailMessage mailMsg = new MailMessage();
            String emailFrom = ConfigurationManager.AppSettings.Get("EmailFromCCed");
            using (mailMsg)
            {
                string domainName = ConfigurationManager.AppSettings.Get("EmailDomainName");

                MailAddress MailFrom = new MailAddress(emailFrom);
                mailMsg.From = MailFrom;

                mailMsg.To.Add(new MailAddress(mailTo.Trim()));

                mailMsg.Bcc.Add(new MailAddress(emailFrom));

                mailMsg.Subject = "HollensheadVMS::" + mailSubject;

                mailMsg.IsBodyHtml = true;
                mailMsg.Body = mailBody;

                mailMsg.Priority = MailPriority.Normal;

                SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings.Get("SmtpServer"));

                //TO DO: Uncomment this before Deployement
                ////For Production
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                //TO DO: Comment this before Deployement.
                //smtpClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;


                bool bSuccess = true;
                try
                {
                    smtpClient.Send(mailMsg);
                }
                catch (SmtpFailedRecipientsException ex)
                {
                    bSuccess = false;
                }
                return bSuccess;
            }
        }
        #endregion
    }
}