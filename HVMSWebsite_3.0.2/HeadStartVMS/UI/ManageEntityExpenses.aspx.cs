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
using MetaOption.Web.UI.WebControls;
using System.Dynamic;

namespace METAOPTION.UI
{
    public partial class ManageEntityExpenses : System.Web.UI.Page
    {
        PreExpenseBAL PreExpBAL = new PreExpenseBAL();
        List<Mobile_ExpensesOfEntityResult> ExpensesList = new List<Mobile_ExpensesOfEntityResult>();

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
            if (!IsPostBack)
            {
                if (Request["EntityId"] != null && Request["type"] != null)
                {
                    BindFilterData();
                }
                
            }
        }

        private void BindFilterData()
        {

            gvExpenseList.DataSource = PreExpBAL.GetAllExpenseTypes(Convert.ToInt32(Request["type"]), Convert.ToInt64(Request["EntityId"]));
            gvExpenseList.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            String EntityTypeID = String.Empty;
            String EntityID = String.Empty;

            if (Request["EntityId"] != null && Request["type"] != null)
            {
                EntityID = Request["EntityId"];
                EntityTypeID = Request["type"];

                DataTable dt = new DataTable();
                dt.Columns.Add("EntityTypeId");
                dt.Columns.Add("EntityId");
                dt.Columns.Add("ExpenseTypeId");
                dt.Columns.Add("MinCount");
                dt.Columns.Add("MaxCount");
                dt.Columns.Add("DefaultPrice");

                foreach (GridViewRow grvrow in gvExpenseList.Rows)
                {
                    CheckBox chkBx = (CheckBox)grvrow.FindControl("chkappr");

                    if (chkBx != null && chkBx.Checked)
                    {
                        String ExpenseTypeID = Convert.ToString(gvExpenseList.DataKeys[grvrow.RowIndex].Value);
                        TextBox txtmincount = (TextBox)grvrow.FindControl("txtmincount");
                        TextBox txtmaxcount = (TextBox)grvrow.FindControl("txtmaxcount");
                        TextBox txtdefaultprice = (TextBox)grvrow.FindControl("txtprice");

                        DataRow row = dt.NewRow();
                        row["EntityTypeId"] = EntityTypeID;
                        row["EntityId"] = EntityID;
                        row["ExpenseTypeId"] = ExpenseTypeID;
                        row["MinCount"] = txtmincount.Text;
                        row["MaxCount"] = txtmaxcount.Text;
                        row["DefaultPrice"] = txtdefaultprice.Text;
                        dt.Rows.Add(row);
                    }
                }
                PreExpBAL.Insert_EntityExpenses(Convert.ToInt64(Session["empId"]), dt);
                Response.Redirect(Request.QueryString["ReturnUrl"]);
            }
            
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.QueryString["ReturnUrl"]);
        }

        protected void gvExpenseList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }
    }
}