using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace METAOPTION.UI
{
    public partial class EmailDetails : System.Web.UI.Page
    {
        EmailBAL bal = new EmailBAL();
        Int32 _EmailType = 0;
        long _Id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.HasKeys())
            {
                _EmailType = Convert.ToInt32(Request.QueryString["Type"]);
                _Id = Convert.ToInt64(Request.QueryString["Id"]);
                EmailDetail(_EmailType, _Id);
            }
        }

        #region[Fill email detail]
        private void EmailDetail(Int32 Type, long Id)
        {
            DataTable dtab = bal.GetEmailDetail(Type, Id);
            foreach (DataRow row in dtab.Rows)
            {
                lblMailTo.Text = Convert.ToString(row["MailTo"]);
                lblMailSubject.Text = Convert.ToString(row["Subject"]);
                lblMailFrom.Text = Convert.ToString(row["MailFrom"]);
                lblMailCCed.Text = Convert.ToString(row["MailCCed"]);
                lblMailBCCed.Text = Convert.ToString(row["MailBCCed"]);
                lblMailBody.Text = Convert.ToString(row["Body"]);
                lblAttempt.Text = Convert.ToString(row["AttemptCount"]);
                lblLoggedOn.Text = Convert.ToString(row["LogTime"]);
                lblSentOn.Text = Convert.ToString(row["SentTime"]);
            }
        }
        #endregion
    }
}