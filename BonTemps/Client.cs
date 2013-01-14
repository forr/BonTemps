using System;

namespace BonTemps
{
    public struct Client
    {        
        private ulong? clientID;
        private string firstName;
        private string lastName;
        private string address;
        private string postalCode;
        private string city;
        private string phoneNumber;
        private string email;
        private int visits;

        public static Client Null = new Client(null);
        public ulong? ClientID { get { return this.clientID; } set { this.clientID = value; } }
        public string FirstName { get { return this.firstName; } set { this.firstName = value; } }
        public string LastName { get { return this.lastName; } set { this.lastName = value; } }
        public string Address { get { return this.address; } set { this.address = value; } }
        public string PostalCode { get { return this.postalCode; } set { this.postalCode = value; } }
        public string City { get { return this.city; } set { this.city = value; } }
        public string PhoneNumber { get { return this.phoneNumber; } set { this.phoneNumber = value; } }
        public string Email { get { return this.email; } set { this.email = value; } }
        public int Visits { get { return this.visits; } set { this.visits = value; } }

        public Client(ulong? clientID)
        {
            this.clientID = clientID;
            this.firstName = String.Empty;
            this.lastName = String.Empty;
            this.address = String.Empty;
            this.postalCode = String.Empty;
            this.city = String.Empty;
            this.phoneNumber = String.Empty;
            this.email = String.Empty;
            this.visits = 0;
        }

        public Client(ulong? clientID, string firstName, string lastName, string address, string postalCode, string city, string phoneNumber, string email, int visits)
            : this(clientID)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.postalCode = postalCode;
            this.city = city;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.visits = visits;
        }

        public static void Add(string firstName, string lastName, string address, string postalCode, string city, string phoneNumber, string email)
        {
            new Database().Insert(Database.TableName.Clients, new string[] { firstName, lastName, address, postalCode, city, phoneNumber, email });
        }

        public static void Delete(int id)
        {
            new Database().Delete(Database.TableName.Clients, id);
        }

        public override string ToString()
        {
            object[] info = new object[] { 
                this.clientID.ToString(), this.firstName, this.lastName, this.address, 
                this.postalCode, this.city, this.phoneNumber, this.email 
            };
            return String.Format("{0},{1} {2},{3},{4},{5},{6},{7}", info);
        } 
    }
}
