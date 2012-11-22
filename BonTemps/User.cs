using System;

namespace BonTemps
{
    public struct User
    {
        private ulong? userID;
        private string username;
        private string password;

        public static User Null = new User(null);
        public ulong? UserID { get { return this.userID; } set { this.userID = value; } }
        public string Username { get { return this.username; } set { this.username = value; } }
        public string Password { get { return this.password; } set { this.password = value; } }

        public User(ulong? userID)
        {
            this.userID = userID;
            this.username = null;
            this.password = null;
        }
        public User(ulong userID, string username, string password)
            : this(userID)
        {
            this.username = username;
            this.password = password;
        }
        public override string ToString()
        {
            object[] info = new object[] {
                this.userID.ToString(), this.username, this.password
            };
            return String.Format("{0}\n{1}\n{2}\n", info);
        }
    }
}
