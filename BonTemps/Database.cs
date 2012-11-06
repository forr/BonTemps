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
        private static string GetConnectionString()
        {
            return global::BonTemps.Properties.Settings.Default.DataConnectionString;
        }
        public static bool Insert(Tables t, Clients c, Orders o, TableOrders to, Menus m, Persons p)
        {
            var sql_statement = "INSERT INTO Tables,Clients,Orders,TableOrders,Menus,Persons WHERE ";
            try
            {
                using(SqlConnection sql_conn = new SqlConnection(GetConnectionString()))
                {
                    sql_conn.Open();
                    if (sql_conn.State == ConnectionState.Open)
                    {
                        
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
        public static bool Update(Tables t, Clients c, Orders o, TableOrders to, Menus m, Persons p)
        {
            return false;
        }
        public static bool Delete(Tables t, Clients c, Orders o, TableOrders to, Menus m, Persons p)
        {
            return false;
        }
        public static DataTable Select()
        {
            return null;
        }
        public static DataTable Select(Tables t, Clients c, Orders o, TableOrders to, Menus m, Persons p)
        {
            return null;
        }
    }
}
