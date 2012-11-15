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
        public enum TableName { Tables, Clients, Orders, TableOrders, Menus, Persons };

        private static string GetConnectionString()
        {
            return global::BonTemps.Properties.Settings.Default.DataConnectionString;
        }

        public static bool Insert(string[] tableNames, string[] values)
        {
            // INSERT INTO <TABLE> <VALUES(X,Y)>
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
            return false;
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
                    return sqlQuery.ExecuteNonQuery() == 1;
                }
            }
            catch { return false; }
        }
        /// <summary>
        /// Select method for login (Users Table)
        /// </summary>
        /// <param name="employeeType"></param>
        /// <param name="password"></param>
        /// <returns>DataTable</returns>
        public static DataTable Select(string employeeType, string password)
        {
            DataTable dt = new DataTable();
            string statement = "SELECT * FROM Users WHERE (EmployeeType=@employeeType) AND (Password=@password)";
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    sqlConn.Open();
                    using (SqlDataAdapter sqlDA = new SqlDataAdapter(statement, sqlConn))
                    {
                        sqlDA.SelectCommand.Parameters.AddWithValue("@employeeType", employeeType);
                        sqlDA.SelectCommand.Parameters.AddWithValue("@password", password);
                        sqlDA.Fill(dt);
                        if (dt.Rows.Count == 0) return null;
                        return dt;
                    }
                }
            }
            catch { return null; }
        }
        public static DataTable Select(object[] input)
        {
            DataTable dt = new DataTable();
            string statement = "SELECT @obj1 FROM @obj2 WHERE @obj3=@obj4";
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    sqlConn.Open();
                    using (SqlDataAdapter sqlDA = new SqlDataAdapter(statement, sqlConn))
                    {
                        sqlDA.SelectCommand.Parameters.AddWithValue("@obj1", input[0]);
                        sqlDA.SelectCommand.Parameters.AddWithValue("@obj2", input[1]);
                        sqlDA.SelectCommand.Parameters.AddWithValue("@obj3", input[2]);
                        sqlDA.SelectCommand.Parameters.AddWithValue("@obj4", input[3]);
                        sqlDA.Fill(dt);
                        if (dt.Rows.Count == 0) return null;
                        return dt;
                    }
                }
            }
            catch { return null; }
        }

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
