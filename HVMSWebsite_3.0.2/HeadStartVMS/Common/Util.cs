using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

using System.Collections.Generic;
using System.Linq.Expressions;
using System.IO;
using System.Xml;
using System.Reflection;


namespace METAOPTION
{
    public static class Util
    {
        /// <summary>
        /// Set text & values in drop down list by using a datasource.
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="dataSource"></param>
        /// <param name="dataTextField"></param>
        /// <param name="dataValueField"></param>
        public static void setValue(DropDownList ddl, Object dataSource, String dataTextField, String dataValueField)
        {
            ddl.DataSource = dataSource;
            ddl.DataTextField = dataTextField;
            ddl.DataValueField = dataValueField;
            ddl.DataBind();
        }

        /// <summary>
        /// Bind data with grid view using data source.
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="dataSource"></param>
        public static void setValue(GridView gv, Object dataSource)
        {
            gv.DataSource = dataSource;
            gv.DataBind();
        }

        /// <summary>
        /// Set value in a label server control.
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="obj"></param>
        public static void setValue(Label lbl, object obj)
        {
            lbl.Text = (obj == null) ? "" : obj.ToString();
        }

        /// <summary>
        /// Set value in text box server control.
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="obj"></param>
        public static void setValue(TextBox txt, object obj)
        {
            txt.Text = (obj == null) ? "" : obj.ToString();
        }

        /// <summary>
        /// Set value in check box server control.
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="isChecked"></param>
        public static void setValue(CheckBox cb, Boolean isChecked)
        {
            cb.Checked = isChecked;
        }

        /// <summary>
        /// Set value in HiddenField server control.
        /// </summary>
        /// <param name="hdf"></param>
        /// <param name="obj"></param>
        public static void setValue(HiddenField hdf, object obj)
        {
            hdf.Value = obj.ToString();
        }

        /// <summary>
        /// Get selected value fron the dropdoenlist server control.
        /// </summary>
        /// <param name="ddl"></param>
        /// <returns></returns>
        public static string getValue(DropDownList ddl)
        {
            return ddl.SelectedValue.Trim().ToLower();
        }

        /// <summary>
        /// Get selected text from the drop down list.
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string getValue(DropDownList ddl, String type)
        {
            return ddl.SelectedItem.Text.Trim();
        }

        /// <summary>
        /// Get value from textbox server control.
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string getValue(TextBox txt)
        {
            return txt.Text.Trim();
        }

        /// <summary>
        /// Get value from HiddenField server control.
        /// </summary>
        /// <param name="hdf"></param>
        /// <returns></returns>
        public static string getValue(HiddenField hdf)
        {
            return hdf.Value.Trim();
        }

        /// <summary>
        /// Get value from Label server control.
        /// </summary>
        /// <param name="lbl"></param>
        /// <returns></returns>
        public static string getValue(Label lbl)
        {
            return lbl.Text.Trim();
        }

        /// <summary>
        /// Get current date time from the server.
        /// </summary>
        /// <returns></returns>
        public static DateTime GetServerDate()
        {
            METAOPTION.BAL.PaymentBLL paymentBLL = new METAOPTION.BAL.PaymentBLL();
            return paymentBLL.GetServerDate();
        }

        /// <summary>
        /// Sort a List object either asc or in desc order as per the expression passed. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="sortExpression"></param>
        /// <returns></returns>
        public static IEnumerable<T> Sort<T>(this IEnumerable<T> source, string sortExpression)
        {
            string[] sortParts = sortExpression.Split(' ');
            var param = Expression.Parameter(typeof(T), string.Empty);
            try
            {
                var property = Expression.Property(param, sortParts[0]);
                var sortLambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), param);

                if (sortParts.Length > 1 && sortParts[1].Equals("desc", StringComparison.OrdinalIgnoreCase))
                {
                    return source.AsQueryable<T>().OrderByDescending<T, object>(sortLambda);
                }
                return source.AsQueryable<T>().OrderBy<T, object>(sortLambda);
            }
            catch (ArgumentException)
            {
                return source;
            }
        }

        /// <summary>
        /// Convert a list object to DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ConvertToTable<T>(List<T> list)
        {
            DataTable table = new DataTable();

            if (list.Count > 0)
            {
                PropertyInfo[] properties = list[0].GetType().GetProperties();
                List<string> columns = new List<string>();
                foreach (PropertyInfo pi in properties)
                {
                    table.Columns.Add(pi.Name);
                    columns.Add(pi.Name);
                }

                foreach (T item in list)
                {
                    object[] cells = getValues(columns, item);
                    table.Rows.Add(cells);
                }
            }
            return table;
        }

        private static object[] getValues(List<string> columns, object instance)
        {
            object[] ret = new object[columns.Count];

            for (int n = 0; n < ret.Length; n++)
            {
                PropertyInfo pi = instance.GetType().GetProperty(columns[n]);

                object value = pi.GetValue(instance, null);

                ret[n] = value;
            }
            return ret;
        }

        /// <summary>
        /// Get DataSet object from a XML string
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="isValueCheck"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string xmlString, bool isValueCheck)
        {

            DataSet dtSet = new DataSet();
            try
            {
                if (null == xmlString || xmlString.Length <= 0)
                    return null;

                xmlString = xmlString.Replace("&", "&amp;");
                if (isValueCheck)
                    xmlString = xmlString.Replace("\"", "&quot;");

                xmlString = xmlString.Replace("'", "&apos;");

                dtSet.ReadXml(new XmlTextReader(new StringReader(xmlString)));
            }
            catch { }

            return dtSet;
        }

        /// <summary>
        /// Check for valid DataSet
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static Boolean IsValidDataSet(DataSet ds)
        {
            Boolean retVal = false;

            if ((ds != null) && (ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0))
            {
                retVal = true;
            }

            return retVal;
        }

        /// <summary>
        /// Check for valid GUID.
        /// </summary>
        /// <param name="strGUID"></param>
        /// <returns></returns>
        public static Boolean IsGuid(String strGUID)
        {
            Boolean ret = false;
            try
            {
                if (strGUID != String.Empty)
                {
                    Guid abc = new Guid(strGUID);
                    ret = true;
                }
            }
            catch { }

            return ret;
        }

        #region used to logout the session, Coded By Prem
        public static void Logout_Session()
        {
            try
            {
                exprire_cookies();
                Int64 LoginHistoryId = Int64.Parse(System.Web.HttpContext.Current.Session["LoginHistoryId"].ToString());
                METAOPTION.BAL.Common.Logout_Session(LoginHistoryId);
            }
            catch { }
            
            System.Web.HttpContext.Current.Session.Abandon();
            System.Web.HttpContext.Current.Response.Redirect("~/Login.aspx");
        }
        #endregion

        #region Validate Query String Values, Coded by Prem

        public static void Validate_QueryString_Value(Int32 EntityTypeID, String strEntityID, Int16 OrgID)
        {
            try
            {
                //if (Int64.TryParse(Request.QueryString["Code"].ToString(), out EntityID) == false)
                //{
                //    EntityID = 0;
                //}

                Int64 EntityID = Int64.Parse(strEntityID);
                bool isValid = false;
                isValid = BAL.Common.Validate_QueryString_Value(EntityTypeID, EntityID, OrgID);

                //isValid=false, means user has enter wrong query string value by url

                if (isValid == false)
                {
                    System.Web.HttpContext.Current.Response.Redirect("../UI/Default.aspx");
                }
            }
            catch { }
        }

        // This method is overloaded because the below method will handle validation related to some specific page,
        // and above method will handle validation for entity type only
        public static void Validate_QueryString_Value(String PageName, String Code, Int16 OrgID)
        {
            try
            {
                bool isValid = false;
                isValid = BAL.Common.Validate_QueryString_Value(PageName, Code, OrgID);
                //isValid=false, means user has enter wrong query string value by url
                if (isValid == false)
                {
                    System.Web.HttpContext.Current.Response.Redirect("../UI/Default.aspx");
                }
            }
            catch { }
        }

        #endregion

        #region[Set of cookies that need clear at logout]
        private static void exprire_cookies()
        {
            System.Web.HttpContext.Current.Response.Cookies["DealerListHistory"].Expires = DateTime.Now.AddDays(-1d);
            System.Web.HttpContext.Current.Response.Cookies["InvSearchHistory"].Expires = DateTime.Now.AddDays(-1d);
        }
        #endregion
    }
}
