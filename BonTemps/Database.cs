﻿using System;
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
        private enum TableName { Tables, Clients, Orders, TableOrders, Menus, Persons };

        private static string GetConnectionString()
        {
            return global::BonTemps.Properties.Settings.Default.DataConnectionString;
        }

        public static bool Insert(string[] tableNames, string[] values)
        {
            string sqlCmd = String.Empty;
            string tables = String.Empty;
            string statement = String.Empty;
            int selectIndex = 0;
            foreach (String str in tableNames)
            {
                foreach (String str2 in values)
                {
                    if (selectIndex == 0) statement += String.Format("INSERT INTO {0} VALUES({1})", str, str2);
                }
            }
        }
        public static bool Update(TableName tableName, string[] argsCol, string[] argsVal, int id)
        {
            string sqlCmd = String.Empty;
            string table = String.Empty;
            string selectColumns = String.Empty;
            int selectIndex = 0;
            switch (tableName)
            {
                case TableName.Tables:
                    table = "Tables";
                    break;
                case TableName.Clients:
                    table = "Clients";
                    break;
                case TableName.Orders:
                    table = "Orders";
                    break;
                case TableName.TableOrders:
                    table = "TableOrders";
                    break;
                case TableName.Menus:
                    table = "Menus";
                    break;
                case TableName.Persons:
                    table = "Persons";
                    break;
            }
            foreach (String str in argsCol)
            {
                foreach (String str2 in argsVal)
                {
                    if (selectIndex == 0) selectColumns += String.Format("{0}='{1}'", str, str2);
                    else selectColumns += String.Format(",{0}='{1}'", str, str2);
                    selectIndex++;
                }
            }

            sqlCmd = "UPDATE @table SET @statement WHERE ID=@id";

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    SqlCommand sqlQuery = new SqlCommand(sqlCmd, sqlConn);
                    sqlQuery.Parameters.AddWithValue("@table", table);
                    sqlQuery.Parameters.AddWithValue("@statement", selectColumns);
                    sqlQuery.Parameters.AddWithValue("@id", id);
                    return sqlQuery.ExecuteNonQuery() == 1;
                }
            }
            catch { return false; }
        }
        public static bool Delete(TableName tableName, int id)
        {
            string sqlCmd = String.Empty;
            string table = String.Empty;
            string selectColumns = String.Empty;
            switch (tableName)
            {
                case TableName.Tables:
                    table = "Tables";
                    break;
                case TableName.Clients:
                    table = "Clients";
                    break;
                case TableName.Orders:
                    table = "Orders";
                    break;
                case TableName.TableOrders:
                    table = "TableOrders";
                    break;
                case TableName.Menus:
                    table = "Menus";
                    break;
                case TableName.Persons:
                    table = "Persons";
                    break;
            }

            sqlCmd = "DELETE FROM @table WHERE ID=@id";

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    SqlCommand sqlQuery = new SqlCommand(sqlCmd, sqlConn);
                    sqlQuery.Parameters.AddWithValue("@table", table);
                    sqlQuery.Parameters.AddWithValue("@id", id);
                    int success = sqlQuery.ExecuteNonQuery();
                    return success == 1;
                }
            }
            catch { return false; }
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
