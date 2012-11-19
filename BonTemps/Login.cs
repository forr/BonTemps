using System;

namespace BonTemps
{
    public class Login
    {
        private string employeeType;
        private string password;
        private bool isActive;

        public string EmployeeType { get { return this.employeeType; } set { this.employeeType = value; } }
        public string Password { get { return this.password; } set { this.password = value; } }
        public bool IsActive { get { return this.isActive; } set { this.isActive = value; } }

        /// <param name="employeeType">To which department/area of expertize does he/she belong.</param>
        /// <param name="password">Password that's also encrypted on creation.</param>
        public Login(string employeeType, string password)
        {
            this.employeeType = employeeType;
            this.password = password;
            this.isActive = false;
        }

        public static bool CanLogin(string employeeType, string password)
        {
            if (String.IsNullOrWhiteSpace(employeeType)) return false;
            if (employeeType.IndexOf(' ') == 0) return false;
            if (String.IsNullOrWhiteSpace(password)) return false;
            if (password.IndexOf(' ') >= 0) return false;

            return Database.IsPasswordValid(employeeType, MD5Encryption.MD5HashToString(MD5Encryption.CreateMD5Hash(password)));
        }
    }
}
