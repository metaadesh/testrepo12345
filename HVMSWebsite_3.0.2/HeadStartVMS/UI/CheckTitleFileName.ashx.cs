using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace METAOPTION.UI
{
    /// <summary>
    /// Summary description for CheckTitleFileName
    /// </summary>
    public class CheckTitleFileName : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string Type = string.Empty;
            int type = 0;
            int EntityId = -1;
            string DocumentTitle = string.Empty;
            if (context.Request.QueryString["EntityId"] != null)
                EntityId = Convert.ToInt32(context.Request.QueryString["EntityId"]);
            if (!string.IsNullOrEmpty(context.Request.QueryString["Type"]))
            {
                Type = context.Request.QueryString["Type"].ToString();

                if (Type == "FileName")
                {
                    type = 1;
                    DocumentTitle = context.Request.QueryString["FileName"].ToString();
                }
                else
                {
                    type = 2;
                    DocumentTitle = context.Request.QueryString["Title"].ToString();
                }

                int i = 0;
                DocumentBAL objBAL = new DocumentBAL();
                DataTable dt = new DataTable();
                dt = objBAL.CheckFileTitleName(type, DocumentTitle, EntityId, Constant.OrgID);
                if (dt.Rows.Count > 0)
                    i = 1;
                else
                    i = 0;
                context.Response.Write(i);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}