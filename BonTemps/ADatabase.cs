using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace BonTemps
{
    public abstract class ADatabase : IDatabase
    {
        public virtual string GetConnectionString()
        {
            return global::BonTemps.Properties.Settings.Default.DataConnectionString;
        }
        public abstract bool Insert(Database.TableName tableName, string[] values);
        public abstract bool Update(Database.TableName tableName, string[] argsCol, string[] argsVal, int id);
        public abstract bool Delete(Database.TableName tableName, int id);

        public abstract Client GetClient(ulong clientID);
        public abstract Menu GetMenu(ulong menuID);
        public abstract Order GetOrder(ulong orderID);
        public abstract Person GetPerson(ulong personID);
        public abstract TableOrder GetTableOrder(ulong tableOrderID);
        public abstract Table GetTable(ulong tableID);
        public abstract User GetUser(ulong userID);

        public abstract List<Client> GetClientListByName(string name);
        public abstract List<UInt64> GetMenuIDs();

        public abstract List<AccessDenied> GetAllAccessDenied();
        public abstract List<Client> GetAllClients();
        public abstract List<Menu> GetAllMenus();
        public abstract List<Order> GetAllOrders();
        public abstract List<Person> GetAllPersons();
        public abstract List<TableOrder> GetAllTableOrders();
        public abstract List<Table> GetAllTables();
        public abstract List<User> GetAllUsers();

        public virtual bool IsPasswordValid(string employeeType, string password)
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
        public virtual bool IsEmailValid(string email)
        {
            if (email.Length < 8) return false;
            if (String.IsNullOrWhiteSpace(email)) return false;
            if (email.IndexOf(' ') >= 0) return false;
            return true;
        }
        public virtual bool IsPhoneNumberValid(string phoneNumber)
        {
            if (phoneNumber.Length < 9 || phoneNumber.Length > 17) return false;
            if (String.IsNullOrWhiteSpace(phoneNumber)) return false;
            if (phoneNumber.IndexOf('0') != 0)
                if (phoneNumber.IndexOf('+') != 0) return false;
            return true; 
        }
    }
}
