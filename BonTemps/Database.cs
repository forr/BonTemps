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
            string sqlStatement = "INSERT INTO Tables,Clients,Orders,TableOrders,Menus,Persons";
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                {
                    sqlConn.Open();
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        SqlCommand sqlQuery = new SqlCommand(sqlStatement, sqlConn);
                        int m_success = sqlQuery.ExecuteNonQuery();
                        return m_success == 1;
                    }
                }
                return false;
            }
            catch { return false; }
        }
        public static bool Update(Tables t, Clients c, Orders o, TableOrders to, Menus m, Persons p)
        {
            string sqlStatement = "UPDATE Tables,Clients,Orders,TableOrders,Menus,Persons";
            try
            {

            }
            catch { return false; }
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
