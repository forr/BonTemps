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
        public enum TableName { Tables, Clients, Orders, TableOrders, Menus, Persons };

        private static string GetConnectionString()
        {            
            return global::BonTemps.Properties.Settings.Default.DataConnectionString;
        }

        #region Usual Insert/Delete/Update methods
        public static bool Insert(TableName tableName, string[] values)
        {
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
        #endregion

        #region GetA_X methods
        public static Clients GetAClient(ulong clientID)
        {
            Clients result = Clients.Null;

            string statement = "SELECT * FROM Clients WHERE ClientID=@ID";
            try
            {

            }
            catch { result = Clients.Null; }
            return result;

        }
        #endregion

        #region GetAllX methods
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
                            c.FirstName = sqlDR["Name"].ToString().Split(' ')[0];
                            c.LastName = (sqlDR["Name"].ToString().Replace(c.FirstName, "")).Remove(0,1);
                            c.Address = sqlDR["Address"].ToString();
                            c.PostalCode = sqlDR["PostalCode"].ToString();
                            c.City = sqlDR["City"].ToString();
                            c.PhoneNumber = sqlDR["PhoneNumber"].ToString();
                            c.Email = sqlDR["Email"].ToString();
                            clnt.Add(c);
                        }
                        return clnt.ToArray();
                    }
                    return null;
                }
            }
            catch { return null; }
        }
        public static Menus[] GetAllMenus()
        {
            List<Menus> menus = new List<Menus>();
            string statement = "SELECT * FROM Menus";
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
                            Menus m = new Menus();
                            m.MenuID = Convert.ToUInt64(sqlDR["MenuID"]);
                            m.Entree = sqlDR["Entree"].ToString();
                            m.MainCourse = sqlDR["MainCourse"].ToString();
                            m.Dessert = sqlDR["Dessert"].ToString();
                            m.Price = (int)sqlDR["Price"];
                            menus.Add(m);
                        }
                        return menus.ToArray();
                    }
                    return null;
                }
            }
            catch { return null; }
        }
        public static Orders[] GetAllOrders()
        {
            List<Orders> orders = new List<Orders>();
            string statement = "SELECT * FROM Orders";
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
                            Orders o = new Orders();
                            o.OrderID = Convert.ToUInt64(sqlDR["OrderID"]);
                            o.ClientID = Convert.ToUInt64(sqlDR["ClientID"]);
                            o.StartDateTime = (DateTime)sqlDR["StartDateTime"];
                            o.EndDateTime = (DateTime)sqlDR["EndDateTime"];
                            orders.Add(o);
                        }
                        return orders.ToArray();
                    }
                    return null;
                }
            }
            catch { return null; }
        }
        public static Persons[] GetAllPersons()
        {
            List<Persons> persons = new List<Persons>();
            string statement = "SELECT * FROM Persons";
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
                            Persons p = new Persons();
                            p.PersonID = Convert.ToUInt64(sqlDR["PersonID"]);
                            p.MenuID = Convert.ToUInt64(sqlDR["MenuID"]);
                            p.OrderID = Convert.ToUInt64(sqlDR["OrderID"]);
                            persons.Add(p);
                        }
                        return persons.ToArray();
                    }
                    return null;
                }
            }
            catch { return null; }
        }
        public static TableOrders[] GetAllTableOrders()
        {
            List<TableOrders> tableOrders = new List<TableOrders>();
            string statement = "SELECT * FROM TableOrders";
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
                            TableOrders to = new TableOrders();
                            to.TableOrderID = Convert.ToUInt64(sqlDR["TableOrderID"]);
                            to.TableID = Convert.ToUInt64(sqlDR["TableID"]);
                            to.OrderID = Convert.ToUInt64(sqlDR["OrderID"]);
                            tableOrders.Add(to);
                        }
                        return tableOrders.ToArray();
                    }
                    return null;
                }
            }
            catch { return null; }
        }
        public static Tables[] GetAllTables()
        {
            List<Tables> tables = new List<Tables>();
            string statement = "SELECT * FROM Tables";
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
                            Tables t = new Tables();
                            t.TableID = Convert.ToUInt64(sqlDR["TableID"]);
                            t.TableNumber = (uint)sqlDR["TableNumber"];
                            t.AmountOfChairs = (uint)sqlDR["AmountOfChairs"];
                            tables.Add(t);
                        }
                        return tables.ToArray();
                    }
                    return null;
                }
            }
            catch { return null; }
        }
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
        #endregion

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
