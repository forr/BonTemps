using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonTemps
{
    public class Login
    {
        public string EmployeeType { get { return EmployeeType; } set { EmployeeType = value; } }
        public string Password { get { return Password; } set { Password = MD5Encryption.MD5HashToString(MD5Encryption.CreateMD5Hash(value)); } }
        public bool Active { get { return Active; } set { Active = value; } }

        /// <param name="employeeType">To which department/area of expertize does he/she belong.</param>
        /// <param name="password">Password that's also encrypted on creation.</param>
        public Login(string employeeType, string password)
        {
            this.EmployeeType = employeeType;
            this.Password = password;
            Active = false;
        }

        public static bool CanLogin(string password)
        {
            return true;
        }

    }
}
