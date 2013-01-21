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

        public virtual bool IsPasswordValid(string username, string password, out string employeeType)
        {
            string statement = "SELECT EmployeeType,Password FROM Users WHERE (UserName=@username)";
            employeeType = "";
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    sqlConn.Open();
                    SqlCommand sqlQuery = new SqlCommand(statement, sqlConn);
                    sqlQuery.Parameters.AddWithValue("@username", username);
                    string pwd = String.Empty;

                    SqlDataReader sqlDR = sqlQuery.ExecuteReader();
                    while (sqlDR.Read())
                    {
                        pwd = sqlDR["Password"].ToString();
                        employeeType = sqlDR["EmployeeType"].ToString();
                    }

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
        public virtual bool IsPostalCodeValid(string postalCode)
        {
            if (String.IsNullOrWhiteSpace(postalCode)) return false;
            if (postalCode.IndexOf(' ').Equals(4)) postalCode = postalCode.Remove(4, 1);
            if (postalCode.Length != 6) return false;
            for (int i = 0; i < 4; i++)
                if (!char.IsNumber(postalCode[i])) return false;
            for (int i = 4; i < 6; i++)
                if (!char.IsLetter(postalCode[i])) return false;
            return true;
        }
    }
}
