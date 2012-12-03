using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonTemps
{
    public abstract class ADatabase : IDatabase
    {
        public abstract string GetConnectionString();
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

        public abstract Client[] GetAllClients();
        public abstract Menu[] GetAllMenus();
        public abstract Order[] GetAllOrders();
        public abstract Person[] GetAllPersons();
        public abstract TableOrder[] GetAllTableOrders();
        public abstract Table[] GetAllTables();
        public abstract User[] GetAllUsers();

        public abstract bool IsPasswordValid(string employeeType, string password);
        public abstract bool IsEmailValid(string email);
        public abstract bool IsPhoneNumberValid(string phoneNumber);
    }
}
