using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace BonTemps
{
    public sealed class Database : ADatabase
    {
        public enum TableName { AccessDenied, Clients, Menus, Orders, Persons, TableOrders, Tables, Users };

        #region Usual Insert/Delete/Update methods
        public override bool Insert(TableName tableName, string[] values)
        {
            string sqlCmd = String.Empty;
            string statement = String.Empty;
            int selectIndex = 0;

            foreach (string str in values)
            {
                bool hasletters = false;
                foreach (Char c in str) 
                    if (Char.IsLetter(c)) hasletters = true;

                if (str.Contains(" ") || hasletters)
                {
                    if (selectIndex == 0) statement += String.Format("'{0}'",str);
                    else statement += String.Format(",'{0}'", str);
                }
                else
                {
                    if (selectIndex == 0) statement += str;
                    else statement += String.Format(",{0}", str);
                }
                selectIndex++;
            }           

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    switch (tableName)
                    {
                        case TableName.AccessDenied:
                            sqlCmd = String.Format("INSERT INTO AccessDenied (MachineID,BlockedSince,BlockedUntil) VALUES ({0})", statement);
                            break;
                        case TableName.Clients:
                            sqlCmd = String.Format("INSERT INTO Clients (Name,Address,PostalCode,City,PhoneNumber,Email) VALUES ({0})", statement);
                            break;
                        case TableName.Menus:
                            sqlCmd = String.Format("INSERT INTO Menus (Entree,MainCourse,Dessert,Price) VALUES ({0})", statement);
                            break;
                        case TableName.Orders:
                            sqlCmd = String.Format("INSERT INTO Orders (ClientID,StartDateTime,EndDateTime) VALUES ({0})", statement);
                            break;
                        case TableName.Persons:
                            sqlCmd = String.Format("INSERT INTO Persons (MenuID,OrderID,TableID) VALUES ({0})", statement);
                            break;
                        case TableName.TableOrders:
                            sqlCmd = String.Format("INSERT INTO TableOrders (TableID,OrderID,OrderReady) VALUES ({0})", statement);
                            break;
                        case TableName.Tables:
                            sqlCmd = String.Format("INSERT INTO Tables (TableNumber,AmountOfChairs) VALUES ({0})", statement);
                            break;
                        case TableName.Users:
                            sqlCmd = String.Format("INSERT INTO Users (EmployeeType,Password) VALUES ({0})", statement);
                            break;
                        default: 
                            sqlCmd = String.Format("INSERT INTO {0} VALUES({1})", tableName.ToString(), statement); 
                            break;
                    }

                    sqlConn.Open();
                    SqlCommand sqlQuery = new SqlCommand(sqlCmd, sqlConn);
                    return sqlQuery.ExecuteNonQuery() == 1;
                }
            }
            catch( Exception ex ) { return false; }
        }
        public override bool Update(TableName tableName, string[] argsCol, string[] argsVal, int id)
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
        public override bool Delete(TableName tableName, int id)
        {
            string sqlCmd = String.Empty;
            string table = String.Empty;
            string selectColumns = String.Empty;


            if (tableName == TableName.AccessDenied) sqlCmd = "DELETE FROM AccessDenied WHERE BlockedID=@id";
            else sqlCmd = String.Format("DELETE FROM {0} WHERE ID=@id", tableName.ToString());

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    sqlConn.Open();
                    SqlCommand sqlQuery = new SqlCommand(sqlCmd, sqlConn);
                    sqlQuery.Parameters.AddWithValue("@id", id);
                    return sqlQuery.ExecuteNonQuery() == 1;
                }
            }
            catch(Exception ex) { return false; }
        }
        #endregion

        public bool UpdateClientVisit(ulong id)
        {
            string sqlCmd = string.Format("UPDATE Clients " +
                                          "SET           Visits = " +
                                          "(SELECT         COUNT(*) AS Visits " +
                                          "FROM            Clients AS c INNER JOIN " +
                                          "Orders AS o ON c.ClientID = o.ClientID " +
                                          "WHERE           (c.ClientID={0}) " +
                                          "GROUP BY c.ClientID) " +
                                          "WHERE        (ClientID={1})", id, id);

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    sqlConn.Open();
                    SqlCommand sqlQuery = new SqlCommand(sqlCmd, sqlConn);
                    //sqlQuery.Parameters.AddWithValue("@cClientID", (int)id);
                    //sqlQuery.Parameters.AddWithValue("@ClientID", (int)id);
                    return sqlQuery.ExecuteNonQuery() == 1;
                }
            }
            catch { return false; }
        }

        public override List<UInt64> GetMenuIDs()
        {
            List<UInt64> menuIDs = new List<UInt64>();
            string statement = "SELECT p.MenuID FROM TableOrders AS t INNER JOIN Orders AS o ON t.OrderID = o.OrderID INNER JOIN Persons AS p ON o.OrderID = p.OrderID";
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
                            menuIDs.Add(Convert.ToUInt64(sqlDR["MenuID"]));
                        }
                        return menuIDs;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #region GetListOf_X methods
        public override List<Client> GetClientListByName(string name)
        {
            List<Client> result = new List<Client>();
            string statement = String.Format("SELECT * FROM Clients WHERE Name LIKE @Name");
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    sqlConn.Open();
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        SqlCommand sqlQuery = new SqlCommand(statement, sqlConn);
                        sqlQuery.Parameters.AddWithValue("@Name", "%" + name + "%");
                        SqlDataReader sqlDR = sqlQuery.ExecuteReader();
                        while (sqlDR.Read())
                        {
                            Client c = new Client();
                            c.ClientID = Convert.ToUInt64(sqlDR["ClientID"]);
                            c.FirstName = sqlDR["Name"].ToString().Split(' ')[0];
                            c.LastName = (sqlDR["Name"].ToString().Replace(c.FirstName, "")).Remove(0, 1);
                            c.Address = sqlDR["Address"].ToString();
                            c.PostalCode = sqlDR["PostalCode"].ToString();
                            c.City = sqlDR["City"].ToString();
                            c.PhoneNumber = sqlDR["PhoneNumber"].ToString();
                            c.Email = sqlDR["Email"].ToString();
                            c.Visits = (int)sqlDR["Visits"];
                            result.Add(c);
                        }
                    }
                    return result;
                }
            }
            catch(Exception ex)
            {
                return result;
            }
        }
        #endregion GetListOf_X methods

        #region GetA_X methods
        public override Client GetClient(ulong clientID)
        {
            Client result = Client.Null;
            string statement = "SELECT * FROM Clients WHERE ClientID=@ID";
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    sqlConn.Open();
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        SqlCommand sqlQuery = new SqlCommand(statement, sqlConn);
                        sqlQuery.Parameters.AddWithValue("@ID", clientID);
                        SqlDataReader sqlDR = sqlQuery.ExecuteReader();
                        if (sqlDR.Read())
                        {
                            Client c = new Client();
                            c.ClientID = Convert.ToUInt64(sqlDR["ClientID"]);
                            c.FirstName = sqlDR["Name"].ToString().Split(' ')[0];
                            c.LastName = (sqlDR["Name"].ToString().Replace(c.FirstName, "")).Remove(0, 1);
                            c.Address = sqlDR["Address"].ToString();
                            c.PostalCode = sqlDR["PostalCode"].ToString();
                            c.City = sqlDR["City"].ToString();
                            c.PhoneNumber = sqlDR["PhoneNumber"].ToString();
                            c.Email = sqlDR["Email"].ToString();
                            c.Visits = (int)sqlDR["Visits"];
                            return c;
                        }
                        result = Client.Null;
                    }
                }
            }
            catch { result = Client.Null; }
            return result;
        }
        public override Menu GetMenu(ulong menuID)
        {
            Menu result = Menu.Null;
            string statement = "SELECT * FROM Menus WHERE MenuID=@ID";
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    sqlConn.Open();
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        SqlCommand sqlQuery = new SqlCommand(statement, sqlConn);
                        sqlQuery.Parameters.AddWithValue("@ID", menuID);
                        SqlDataReader sqlDR = sqlQuery.ExecuteReader();
                        if (sqlDR.Read())
                        {
                            Menu m = new Menu();
                            m.MenuID = Convert.ToUInt64(sqlDR["MenuID"]);
                            m.Entree = sqlDR["Entree"].ToString();
                            m.MainCourse = sqlDR["MainCourse"].ToString();
                            m.Dessert = sqlDR["Dessert"].ToString();
                            m.Price = (int)sqlDR["Price"];
                            return m;
                        }
                        result = Menu.Null;
                    }
                }
            }
            catch { result = Menu.Null; }
            return result;
        }
        public int GetOrderID(ulong ClientID, string StartDateTime, string EndDateTime)
        {
            int result = -1;
            string statement = "SELECT OrderID FROM Orders WHERE ClientID=@clientID AND StartDateTime=@startDateTime AND EndDateTime=@endDateTimeOrder";
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    sqlConn.Open();
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        SqlCommand sqlQuery = new SqlCommand(statement, sqlConn);
                        sqlQuery.Parameters.AddWithValue("@clientID", (int)ClientID);
                        sqlQuery.Parameters.AddWithValue("@startDateTime", StartDateTime);
                        sqlQuery.Parameters.AddWithValue("@endDateTimeOrder", EndDateTime);
                        SqlDataReader sqlDR = sqlQuery.ExecuteReader();
                        if (sqlDR.Read())
                        {
                            result = int.Parse(sqlDR["OrderID"].ToString());
                        }
                    }
                }
            }
            catch (Exception Exception) { }
            return result;
        }

        public override Order GetOrder(ulong orderID)
        {
            Order result = Order.Null;
            string statement = "SELECT * FROM Orders WHERE OrderID=@ID";
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    sqlConn.Open();
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        SqlCommand sqlQuery = new SqlCommand(statement, sqlConn);
                        sqlQuery.Parameters.AddWithValue("@ID", orderID);
                        SqlDataReader sqlDR = sqlQuery.ExecuteReader();
                        if (sqlDR.Read())
                        {
                            Order o = new Order();
                            o.OrderID = Convert.ToUInt64(sqlDR["OrderID"]);
                            o.ClientID = Convert.ToUInt64(sqlDR["ClientID"]);
                            o.StartDateTime = (DateTime)sqlDR["StartDateTime"];
                            o.EndDateTime = (DateTime)sqlDR["EndDateTime"];
                            return o;
                        }
                        result = Order.Null;
                    }
                }
            }
            catch { result = Order.Null; }
            return result;
        }
        public override Person GetPerson(ulong personID)
        {
            Person result = Person.Null;
            string statement = "SELECT * FROM Persons WHERE PersonID=@ID";
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    sqlConn.Open();
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        SqlCommand sqlQuery = new SqlCommand(statement, sqlConn);
                        sqlQuery.Parameters.AddWithValue("@ID", personID);
                        SqlDataReader sqlDR = sqlQuery.ExecuteReader();
                        if (sqlDR.Read())
                        {
                            Person p = new Person();
                            p.PersonID = (ulong)sqlDR["PersonID"];
                            p.MenuID = (ulong)sqlDR["MenuID"];
                            p.OrderID = (ulong)sqlDR["OrderID"];
                            return p;
                        }
                        result = Person.Null;
                    }
                }
            }
            catch { result = Person.Null; }
            return result;
        }
        public override TableOrder GetTableOrder(ulong tableOrderID)
        {
            TableOrder result = TableOrder.Null;
            string statement = "SELECT * FROM TableOrders WHERE TableOrderID=@ID";
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    sqlConn.Open();
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        SqlCommand sqlQuery = new SqlCommand(statement, sqlConn);
                        sqlQuery.Parameters.AddWithValue("@ID", tableOrderID);
                        SqlDataReader sqlDR = sqlQuery.ExecuteReader();
                        if (sqlDR.Read())
                        {
                            TableOrder to = new TableOrder();
                            to.TableOrderID = (ulong)sqlDR["TableOrderID"];
                            to.TableID = (ulong)sqlDR["TableID"];
                            to.OrderID = (ulong)sqlDR["OrderID"];
                            return to;
                        }
                        result = TableOrder.Null;
                    }
                }
            }
            catch { result = TableOrder.Null; }
            return result;
        }
        public override Table GetTable(ulong tableID)
        {
            Table result = Table.Null;
            string statement = "SELECT * FROM Tables WHERE TableID=@ID";
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    sqlConn.Open();
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        SqlCommand sqlQuery = new SqlCommand(statement, sqlConn);
                        sqlQuery.Parameters.AddWithValue("@ID", tableID);
                        SqlDataReader sqlDR = sqlQuery.ExecuteReader();
                        if (sqlDR.Read())
                        {
                            Table t = new Table();
                            t.TableID = (ulong)sqlDR["TableID"];
                            t.TableNumber = (uint)sqlDR["TableNumber"];
                            t.AmountOfChairs = (uint)sqlDR["AmountOfChairs"];
                            return t;
                        }
                        result = Table.Null;
                    }
                }
            }
            catch { result = Table.Null; }
            return result;
        }
        public override User GetUser(ulong userID)
        {
            User result = User.Null;
            string statement = "SELECT * FROM Users WHERE UserID=@ID";
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    sqlConn.Open();
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        SqlCommand sqlQuery = new SqlCommand(statement, sqlConn);
                        sqlQuery.Parameters.AddWithValue("@ID", userID);
                        SqlDataReader sqlDR = sqlQuery.ExecuteReader();
                        if (sqlDR.Read())
                        {
                            User u = new User();
                            u.UserID = (ulong)sqlDR["UserID"];
                            u.Username = sqlDR["EmployeeType"].ToString();
                            u.Password = sqlDR["Password"].ToString();
                            return u;
                        }
                        result = User.Null;
                    }
                }
            }
            catch { result = User.Null; }
            return result;
        }
        #endregion

        #region GetAllX methods
        public override List<AccessDenied> GetAllAccessDenied()
        {
            List<AccessDenied> ad = new List<AccessDenied>();

            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "yyyy-MM-dd HH:mm:ss";

            string statement = "SELECT * FROM AccessDenied";
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
                            AccessDenied a = new AccessDenied();
                            a.BlockedID = Convert.ToUInt32(sqlDR["BlockedID"]);
                            a.MachineID = sqlDR["MachineID"].ToString();
                            a.BlockedSince = System.DateTime.Parse(((DateTime)sqlDR["BlockedSince"]).ToString("yyyy-MM-dd hh:mm:ss tt"));
                            a.BlockedUntil = Convert.ToDateTime(sqlDR["BlockedUntil"], dtfi);
                            ad.Add(a);
                        }
                        return ad;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public override List<Client> GetAllClients()
        {
            List<Client> clnt = new List<Client>();
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
                            Client c = new Client();
                            c.ClientID = Convert.ToUInt64(sqlDR["ClientID"]);
                            c.FirstName = sqlDR["Name"].ToString().Split(' ')[0];
                            c.LastName = (sqlDR["Name"].ToString().Replace(c.FirstName, "")).Remove(0,1);
                            c.Address = sqlDR["Address"].ToString();
                            c.PostalCode = sqlDR["PostalCode"].ToString();
                            c.City = sqlDR["City"].ToString();
                            c.PhoneNumber = sqlDR["PhoneNumber"].ToString();
                            c.Email = sqlDR["Email"].ToString();
                            c.Visits = (int)sqlDR["Visits"];
                            clnt.Add(c);
                        }
                        return clnt;
                    }
                    return null;
                }
            }
            catch { return null; }
        }

        public override List<Menu> GetAllMenus()
        {
            List<Menu> menus = new List<Menu>();
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
                            Menu m = new Menu();
                            m.MenuID = Convert.ToUInt64(sqlDR["MenuID"]);
                            m.Entree = sqlDR["Entree"].ToString();
                            m.MainCourse = sqlDR["MainCourse"].ToString();
                            m.Dessert = sqlDR["Dessert"].ToString();
                            double priceOut = 0.00;
                            double.TryParse(sqlDR["Price"].ToString(), out priceOut);
                            m.Price = priceOut;
                            menus.Add(m);
                        }
                        return menus;
                    }
                    return null;
                }
            }
            catch { return null; }
        }
        
        public override List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
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
                            Order o = new Order();
                            o.OrderID = Convert.ToUInt64(sqlDR["OrderID"]);
                            o.ClientID = Convert.ToUInt64(sqlDR["ClientID"]);
                            o.StartDateTime = (DateTime)sqlDR["StartDateTime"];
                            o.EndDateTime = (DateTime)sqlDR["EndDateTime"];
                            orders.Add(o);
                        }
                        return orders;
                    }
                    return null;
                }
            }
            catch { return null; }
        }

        public double GetTotalPrice()
        {
            double result = -1;
            string statement = "SELECT SUM(m.Price) AS TotalPrice" +
                               "FROM Clients AS c CROSS JOIN" +
                               "Persons AS p INNER JOIN" +
                               "Orders AS o ON p.OrderID = o.OrderID INNER JOIN" +
                               "Menus AS m ON p.MenuID = m.MenuID" +
                               "WHERE (c.ClientID = 2)";
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    sqlConn.Open();
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        SqlCommand sqlQuery = new SqlCommand(statement, sqlConn);
                        SqlDataReader sqlDR = sqlQuery.ExecuteReader();
                        result = (double)sqlDR[0];
                        return result;
                    }
                    return result;
                }
            }
            catch { return result; }
        }

        public int GetVisitorCount()
        {
            int result = -1;
            string statement = "SELECT COUNT(*) AS Visitors FROM Persons";
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    sqlConn.Open();
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        SqlCommand sqlQuery = new SqlCommand(statement, sqlConn);
                        SqlDataReader sqlDR = sqlQuery.ExecuteReader();
                        result = (int)sqlDR[0];
                        return result;
                    }
                    return result;
                }
            }
            catch { return result; }
        }

        public DataTable GetAllCurrentOrders()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("OrderID", typeof(Int32)),
                                                   new DataColumn("MenuID",  typeof(Int32)),
                                                   new DataColumn("TableID",  typeof(Int32)),
                                                   new DataColumn("OrderReady",  typeof(Boolean))});
            string statement = "SELECT p.OrderID, p.MenuID, p.TableID, t.OrderReady " +
                               "FROM TableOrders AS t INNER JOIN " +
                               "Persons AS p ON t.TableID = p.TableID AND t.OrderID = p.OrderID CROSS JOIN " +
                               "Orders AS o " +
                               "WHERE (o.Seated = 1)";
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
                            dt.Rows.Add(sqlDR[0], sqlDR[1], sqlDR[2], sqlDR[3]);
                        }
                        return dt;
                    }
                    return null;
                }
            }
            catch { return null; }
        }

        public override List<Person> GetAllPersons()
        {
            List<Person> persons = new List<Person>();
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
                            Person p = new Person();
                            p.PersonID = Convert.ToUInt64(sqlDR["PersonID"]);
                            p.MenuID = Convert.ToUInt64(sqlDR["MenuID"]);
                            p.OrderID = Convert.ToUInt64(sqlDR["OrderID"]);
                            persons.Add(p);
                        }
                        return persons;
                    }
                    return null;
                }
            }
            catch { return null; }
        }
        public override List<TableOrder> GetAllTableOrders()
        {
            List<TableOrder> tableOrders = new List<TableOrder>();
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
                            TableOrder to = new TableOrder();
                            to.TableOrderID = Convert.ToUInt64(sqlDR["TableOrderID"]);
                            to.TableID = Convert.ToUInt64(sqlDR["TableID"]);
                            to.OrderID = Convert.ToUInt64(sqlDR["OrderID"]);
                            tableOrders.Add(to);
                        }
                        return tableOrders;
                    }
                    return null;
                }
            }
            catch { return null; }
        }
        public override List<Table> GetAllTables()
        {
            List<Table> tables = new List<Table>();
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
                            Table t = new Table();
                            t.TableID = Convert.ToUInt64(sqlDR["TableID"]);
                            t.TableNumber = Convert.ToUInt32(sqlDR["TableNumber"]);
                            t.AmountOfChairs = Convert.ToUInt32(sqlDR["AmountOfChairs"]);
                            tables.Add(t);
                        }
                        return tables;
                    }
                    return null;
                }
            }
            catch { return null; }
        }
        public override List<User> GetAllUsers()
        {
            List<User> usr = new List<User>();
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
                            User u = new User();
                            u.UserID = Convert.ToUInt64(sqlDR["UserID"]);
                            u.Username = sqlDR["EmployeeType"].ToString();
                            u.Password = sqlDR["Password"].ToString();
                            usr.Add(u);
                        }
                        return usr;
                    }
                    return null;
                }
            }
            catch { return null; }
        }
        #endregion

        #region Select methods
        
        #endregion

        public override bool IsPasswordValid(string employeeType, string password)
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