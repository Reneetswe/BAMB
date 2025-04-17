using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace OnlineSales
{
    public class DataAccess
    {
        //---

        //global read minimum age allowed to register from app settings in web.config and enforce on registration
        public readonly int MinRegistrationAge = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MinRegistrationAge"]);


        //method to establish connection
        private SqlConnection GetConnection()
        {
            String constring = System.Configuration.ConfigurationManager.ConnectionStrings["devcon"].ConnectionString;
            return new SqlConnection(constring);
        }

        //public method to get data
        public DataSet GetData(String sql, Object[] arrparams = null, String strTableName="data")
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    //create command
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    //add parameters
                    if (arrparams != null)
                    {
                        for (int k = 0; k < arrparams.Length; k++)
                        {
                            String param = $"@{k}";                                    //new param char setting
                            var reg = new System.Text.RegularExpressions.Regex(@"\?"); //param char specification
                            sql = reg.Replace(sql, param, 1);                          //replace param character, 1 char per round
                            cmd.Parameters.AddWithValue(param, arrparams[k]);          //add param and value
                        }
                        //assign updated command text
                        cmd.CommandText = sql;
                    }
                    //execute
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, strTableName);
                    return ds;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExecuteCommand(String sql, Object[] arrparams = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                //create command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //add parameters
                if (arrparams != null)
                {
                    for (int k = 0; k < arrparams.Length; k++)
                    {
                        String param = $"@{k}";                                    //new param char setting
                        var reg = new System.Text.RegularExpressions.Regex(@"\?"); //param char specification
                        sql = reg.Replace(sql, param, 1);                          //replace param character, 1 char per round
                        cmd.Parameters.AddWithValue(param, arrparams[k]);          //add param and value
                    }
                    //assign updated command text
                    cmd.CommandText = sql;
                }
                //execute
                int rows = cmd.ExecuteNonQuery();
                return rows; //return
            }
        }

        //method to load htmlselect from database
        public void LoadHtmlSelect(System.Web.UI.HtmlControls.HtmlSelect ctlselect, DataSet ds, String strTableName = "", String KeyColumnName = "", String ValueColumnName = "")
        {
            try
            {
                strTableName = (strTableName == "") ? ds.Tables[0].TableName : strTableName;
                KeyColumnName = (KeyColumnName == "") ? ds.Tables[strTableName].Columns[0].ColumnName : KeyColumnName;
                ValueColumnName = (ValueColumnName == "") ? ds.Tables[strTableName].Columns[1].ColumnName : ValueColumnName;
                DataTable dt = ds.Tables[0].Copy();
                dt.Rows.Add(0, "Select...");
                dt = dt.AsEnumerable().OrderBy(r => r[0]).ThenBy(x => x[1]).CopyToDataTable(); //orders by first and second column
                //dt.DefaultView.Sort= dt.Columns[0].ColumnName + " ASC"; //order by first column
                ctlselect.DataSource = dt;
                ctlselect.DataValueField = dt.Columns[KeyColumnName].ColumnName;
                ctlselect.DataTextField = dt.Columns[ValueColumnName].ColumnName;
                ctlselect.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //method to load htmlselect from dictionary
        public void LoadHtmlSelect(System.Web.UI.HtmlControls.HtmlSelect ctlselect, Dictionary<String, String> dict)
        {
            try
            {
                ctlselect.DataSource = dict;
                ctlselect.DataValueField = "Key";
                ctlselect.DataTextField = "Value";
                ctlselect.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //load GridView
        public void LoadGridView(System.Web.UI.WebControls.GridView grid, DataSet ds, String strTableName = "")
        {
            try
            {
                strTableName = (strTableName == "") ? ds.Tables[0].TableName : strTableName;
                grid.DataSource = ds.Tables[strTableName];
                grid.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //hide column in gridview || Data wont be available so be careful
        public void HideGridViewColumn(System.Web.UI.WebControls.GridView grid, String colname)
        {
            try
            {
                int colIndex = (grid.DataSource as DataTable).Columns[colname].Ordinal; //get column index
                if (grid.Rows.Count > 0)
                {
                    grid.HeaderRow.Cells[colIndex].Visible = false;
                    for (int i = 0; i <= grid.Rows.Count - 1; i++)
                    {
                        grid.Rows[i].Cells[colIndex].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //MessageBox Showing
        public void ShowMessage(String msg)
        {
            msg = System.Web.HttpUtility.JavaScriptStringEncode(msg, false); //addDoubleQuotes : false|true  [if true msg will have double quotes around]
            HttpContext.Current.Response.Write(@"<script> setTimeout(function(){ alert('" + msg + "') }, 20); </script>"); //setTimeOut: 20 | delay by 2 sec to avoid msg on blank form
        }


        //---
    }

    //-----

}