using System;

namespace BonTemps
{
    public class Login
    {
        private static Database db = new Database();
        private string employeeType;
        private string password;
        private bool isActive;
        private string username;

        public string Username { get { return this.username; } set { this.username = value; } }
        public string EmployeeType { get { return this.employeeType; } set { this.employeeType = value; } }
        public string Password { get { return this.password; } set { this.password = value; } }
        public bool IsActive { get { return this.isActive; } set { this.isActive = value; } }

        /// <param name="username">User's name.</param>
        /// <param name="employeeType">To which department/area of expertize does he/she belong.</param>
        /// <param name="password">Password that's also encrypted on creation.</param>
        public Login(string username, string password, string employeeType)
        {
            this.username = username;
            this.employeeType = employeeType;
            this.password = password;
            this.isActive = false;
        }

        public static bool CanLogin(string username, string password, out string employeeType)
        {
            employeeType = "";
            if (String.IsNullOrWhiteSpace(username)) return false;
            if (username.IndexOf(' ') == 0) return false;
            if (String.IsNullOrWhiteSpace(password)) return false;
            if (password.IndexOf(' ') >= 0) return false;

            return db.IsPasswordValid(username, MD5Encryption.MD5HashToString(MD5Encryption.CreateMD5Hash(password)), out employeeType);
        }


    }
}
