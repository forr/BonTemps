using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public abstract List<AccessDenied> GetAllAccessDenied();
        public abstract List<Client> GetAllClients();
        public abstract List<Menu> GetAllMenus();
        public abstract List<Order> GetAllOrders();
        public abstract List<Person> GetAllPersons();
        public abstract List<TableOrder> GetAllTableOrders();
        public abstract List<Table> GetAllTables();
        public abstract List<User> GetAllUsers();

        public abstract bool IsPasswordValid(string employeeType, string password);
        public abstract bool IsEmailValid(string email);
        public abstract bool IsPhoneNumberValid(string phoneNumber);
    }
}
