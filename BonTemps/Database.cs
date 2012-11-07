using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace BonTemps
{
    public static class Database
    {
        private static string GetConnectionString()
        {
            return global::BonTemps.Properties.Settings.Default.DataConnectionString;
        }
        public static bool Insert(Tables t, Clients c, Orders o, TableOrders to, Menus m, Persons p)
        {
            string sqlStatement = "INSERT INTO Tables,Clients,Orders,TableOrders,Menus,Persons";
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    sqlConn.Open();
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        SqlCommand sqlQuery = new SqlCommand(sqlStatement, sqlConn);
                        int m_success = sqlQuery.ExecuteNonQuery();
                        return m_success == 1;
                    }
                }
                return false;
            }
            catch { return false; }
        }
        public static bool Update(Tables t, Clients c, Orders o, TableOrders to, Menus m, Persons p)
        {
            string sqlStatement = "UPDATE Tables,Clients,Orders,TableOrders,Menus,Persons";
            try
            {
                return false;
            }
            catch { return false; }
        }
        public static bool Delete(Tables t, Clients c, Orders o, TableOrders to, Menus m, Persons p)
        {
            return false;
        }
        public static DataTable Select()
        {
            return null;
        }
        public static DataTable Select(Tables t, Clients c, Orders o, TableOrders to, Menus m, Persons p)
        {
            return null;
        }

        //R version
        #region Example1
        /*
        public enum TableSelect { CLIENTS, TABLES, ORDERS }

        public static DataTable SelectGlobal(TableSelect tableSelect, string[] ArgsSelect, string ArgsWhere)
        {
            string sqlcmd = String.Empty;
            string table = String.Empty;
            string selectables = String.Empty;
            DataTable dt = null;

            switch(tableSelect)
            {
                case TableSelect.CLIENTS:
                    table = "'Clients'";
                    break;
                case TableSelect.ORDERS:
                    table = "'Orders'";
                    break;
                case TableSelect.TABLES:
                    table = "'Tables'";
                    break;
            }
            foreach(String s in ArgsSelect)
            {
                selectables = "'" + s + "'";
            }

            sqlcmd = string.Format("SELECT {0} FROM {1} WHERE {2}", selectables, table, ArgsWhere);

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    using(SqlDataAdapter sqlda = new SqlDataAdapter("", sqlConn))
                    {
                        sqlda.Fill(dt);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        return dt;
                    }
                    else
                    {

                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
        }
        */
        #endregion


        #region Example2
        /*
        public static DataTable SelectFromClients()
        {
            DataTable dt = null;
            string sqlcmd = string.Format("SELECT * FROM Clients");

            try
            {

                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    using (SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd, sqlConn))
                    {
                        sqlda.Fill(dt);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        return dt;
                    }
                    else
                    {

                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public static DataTable SelectFromClients(int id)
        {
            DataTable dt = null;
            string sqlcmd = string.Format("SELECT * FROM Clients WHERE ID={0}", id);

            try
            {

                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    using (SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd, sqlConn))
                    {
                        sqlda.Fill(dt);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        return dt;
                    }
                    else
                    {

                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public static DataTable SelectFromClients(string[] ArgsSelect, string[] ArgsWhere)
        {
            string selectables = "";
            DataTable dt = null;

            foreach (String s in ArgsSelect)
            {
                selectables = "'" + s + "'";
            }

            string sqlcmd = string.Format("SELECT {0} FROM Clients WHERE {1}", selectables, ArgsWhere);

            try
            {

                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    using (SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd, sqlConn))
                    {
                        sqlda.Fill(dt);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        return dt;
                    }
                    else
                    {

                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
        }*/
        #endregion
    }
}
