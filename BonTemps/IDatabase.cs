﻿using System;
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

        Client[] GetAllClients();
        Menu[] GetAllMenus();
        Order[] GetAllOrders();
        Person[] GetAllPersons();
        TableOrder[] GetAllTableOrders();
        Table[] GetAllTables();
        User[] GetAllUsers();

        bool IsPasswordValid(string employeeType, string password);
        bool IsEmailValid(string email);
        bool IsPhoneNumberValid(string phoneNumber);
    }
}