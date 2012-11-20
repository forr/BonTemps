using System;

namespace BonTemps
{
    public struct User
    {
        private ulong? userID;
        private string username;
        private string password;

        public ulong? UserID { get { return this.userID; } set { this.userID = value; } }
        public string Username { get { return this.username; } set { this.username = value; } }
        public string Password { get { return this.password; } set { this.password = value; } }

        public override string ToString()
        {
            object[] info = new object[] {
                this.userID.ToString(), this.username, this.password
            };
            return String.Format("{0}\n{1}\n{2}\n", info);
        }
    }
}
