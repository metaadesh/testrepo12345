using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace METAOPTION.UI
{
    public partial class SMSDetail : System.Web.UI.Page
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
                SMSDetails(_EmailType, _Id);
            }
        }

        #region[Fill email detail]
        private void SMSDetails(Int32 Type, long Id)
        {
            DataTable dtab = bal.GetSMSDetail(Type, Id);
            dlsmsdetails.DataSource = dtab;
            dlsmsdetails.DataBind();
            //foreach (DataRow row in dtab.Rows)
            //{
            //    lblMailTo.Text = Convert.ToString(row["MailTo"]);
            //    lblMailBody.Text = Convert.ToString(row["Body"]);
            //    lblAttempt.Text = Convert.ToString(row["AttemptCount"]);
            //    lblLoggedOn.Text = Convert.ToString(row["LogTime"]);
            //    lblSentOn.Text = Convert.ToString(row["SentTime"]);
            //}
        }
        #endregion
    }
}