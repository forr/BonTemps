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
        
        public static Client Null = new Client(null);
        public ulong? ClientID { get { return this.clientID; } set { this.clientID = value; } }
        public string FirstName { get { return this.firstName; } set { this.firstName = value; } }
        public string LastName { get { return this.lastName; } set { this.lastName = value; } }
        public string Address { get { return this.address; } set { this.address = value; } }
        public string PostalCode { get { return this.postalCode; } set { this.postalCode = value; } }
        public string City { get { return this.city; } set { this.city = value; } }
        public string PhoneNumber { get { return this.phoneNumber; } set { this.phoneNumber = value; } }
        public string Email { get { return this.email; } set { this.email = value; } }

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
        }

        public Client(ulong? clientID, string firstName, string lastName, string address, string postalCode, string city, string phoneNumber, string email)
            : this(clientID)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.postalCode = postalCode;
            this.city = city;
            this.phoneNumber = phoneNumber;
            this.email = email;
        }
        public override string ToString()
        {
            object[] info = new object[] { 
                this.clientID.ToString(), this.firstName, this.lastName, this.address, 
                this.postalCode, this.city, this.phoneNumber, this.email 
            };
            return String.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}\n", info);
        } 
    }
}
