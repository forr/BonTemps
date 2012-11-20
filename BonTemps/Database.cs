using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace BonTemps
{
    public static class Database
    {
        /// <summary>
        /// Enum which contains the database table names
        /// Needs a ToString() call to parse the values of this enum as a string
        /// </summary>
        public enum TableName { Tables, Clients, Orders, TableOrders, Menus, Persons };

        /// <summary>
        /// Retrieves database connection string from app.config and returns it
        /// </summary>
        /// <returns>String</returns>
        private static string GetConnectionString()
        {
            return global::BonTemps.Properties.Settings.Default.DataConnectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="values"></param>
        /// <returns>Boolean</returns>
        public static bool Insert(TableName tableName, string[] values)
        {
            // INSERT INTO <TABLE> VALUES(...)
            string sqlCmd = String.Empty;
            string statement = String.Empty;
            int selectIndex = 0;

            foreach (string str in values)
            {
                if (selectIndex == 0) statement += str;
                else statement += String.Format(",{0}", str);
                selectIndex++;
            }

            sqlCmd = "INSERT INTO @table VALUES(@values)";

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    SqlCommand sqlQuery = new SqlCommand(sqlCmd, sqlConn);
                    sqlQuery.Parameters.AddWithValue("@table", tableName.ToString());
                    sqlQuery.Parameters.AddWithValue("@values", statement);
                    return sqlQuery.ExecuteNonQuery() == 1;
                }
            }
            catch { return false; }
        }
        public static bool Update(TableName tableName, string[] argsCol, string[] argsVal, int id)
        {
            string sqlCmd = String.Empty;
            string table = String.Empty;
            string selectColumns = String.Empty;
            int selectIndex = 0;
            foreach (string str in argsCol)
            {
                foreach (string str2 in argsVal)
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
                    sqlQuery.Parameters.AddWithValue("@table", tableName.ToString());
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

            sqlCmd = "DELETE FROM @table WHERE ID=@id";

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    SqlCommand sqlQuery = new SqlCommand(sqlCmd, sqlConn);
                    sqlQuery.Parameters.AddWithValue("@table", tableName.ToString());
                    sqlQuery.Parameters.AddWithValue("@id", id);
                    return sqlQuery.ExecuteNonQuery() == 1;
                }
            }
            catch { return false; }
        }
        public static Clients[] GetAllClients()
        {
            List<Clients> clnt = new List<Clients>();
            string statement = "SELECT * FROM Clients";
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    sqlConn.Open();
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        SqlCommand sqlQuery = new SqlCommand(statement, sqlConn);
                        SqlDataReader sqlDR = sqlQuery.ExecuteReader();
                        while (sqlDR.Read())
                        {
                            Clients c = new Clients();
                            c.ClientID = Convert.ToUInt64(sqlDR["ClientID"]);
                        }
                        return clnt.ToArray();
                    }
                    return null;
                }
            }
            catch { return null; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Users class array</returns>
        public static Users[] GetAllUsers()
        {
            List<Users> usr = new List<Users>();
            string statement = "SELECT * FROM Users";
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    sqlConn.Open();
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        SqlCommand sqlQuery = new SqlCommand(statement, sqlConn);
                        SqlDataReader sqlDR = sqlQuery.ExecuteReader();
                        while(sqlDR.Read())
                        {
                            Users u = new Users();
                            u.UserID = Convert.ToUInt64(sqlDR["UserID"]);
                            u.Username = sqlDR["EmployeeType"].ToString();
                            u.Password = sqlDR["Password"].ToString();
                            usr.Add(u);
                        }
                        return usr.ToArray();
                    }
                    return null;
                }
            }
            catch { return null; }
        }
        /// <summary>
        /// Password Check – Constraints are that EmployeeType given must equal the 
        /// corresponding Password in order to return bool==true
        /// </summary>
        /// <param name="employeeType"></param>
        /// <param name="password"></param>
        /// <returns>Boolean</returns>
        public static bool IsPasswordValid(string employeeType, string password)
        {
            string statement = "SELECT Password FROM Users WHERE (EmployeeType=@employeeType)";
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    sqlConn.Open();
                    SqlCommand sqlQuery = new SqlCommand(statement, sqlConn);
                    sqlQuery.Parameters.AddWithValue("@employeeType", employeeType);

                    string pwd = (string)sqlQuery.ExecuteScalar();

                    return pwd == password;
                }
            }
            catch { return false; }
        }
    }
}
