using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonTemps
{
    public interface IDatabase
    {
        string GetConnectionString();
        bool Insert(Database.TableName tableName, string[] values);
        bool Update(Database.TableName tableName, string[] argsCol, string[] argsVal, int id);
        bool Delete(Database.TableName tableName, int id);

        Client GetClient(ulong clientID);
        Menu GetMenu(ulong menuID);
        Order GetOrder(ulong orderID);
        Person GetPerson(ulong personID);
        TableOrder GetTableOrder(ulong tableOrderID);
        Table GetTable(ulong tableID);
        User GetUser(ulong userID);

        List<Client> GetClientListByName(string name);
        List<UInt64> GetMenuIDs();
        List<AccessDenied> GetAllAccessDenied();
        List<Client> GetAllClients();
        List<Menu> GetAllMenus();
        List<Order> GetAllOrders();
        List<Person> GetAllPersons();
        List<TableOrder> GetAllTableOrders();
        List<Table> GetAllTables();
        List<User> GetAllUsers();

        bool IsPasswordValid(string username, string password, out string employeeType);
        bool IsEmailValid(string email);
        bool IsPhoneNumberValid(string phoneNumber);
    }
}
