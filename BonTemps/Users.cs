using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonTemps
{
    public struct Users
    {
        private ulong? userID;
        private string username;
        private string password;

        public ulong? UserID { get { return this.userID; } set { this.userID = value; } }
        public string Username { get { return this.username; } set { this.username = value; } }
        public string Password { get { return this.password; } set { this.password = value; } }
    }
}
