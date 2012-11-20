namespace BonTemps
{
    public struct Clients
    {
        private ulong? clientID;
        private string firstName;
        private string lastName;
        private string address;
        private string postalCode;
        private string city;
        private string phoneNumber;
        private string email;

        public ulong? ClientID { get { return this.clientID; } set { this.clientID = value; } }
        public string FirstName { get { return this.firstName; } set { this.firstName = value; } }
        public string LastName { get { return this.lastName; } set { this.lastName = value; } }
        public string Address { get { return this.address; } set { this.address = value; } }
        public string PostalCode { get { return this.postalCode; } set { this.postalCode = value; } }
        public string City { get { return this.city; } set { this.city = value; } }
        public string PhoneNumber { get { return this.phoneNumber; } set { this.phoneNumber = value; } }
        public string Email { get { return this.email; } set { this.email = value; } }

        public Clients(ulong? clientID, string firstName, string lastName, string address, string postalCode, string city, string phoneNumber, string email)
        {
            this.clientID = clientID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.postalCode = postalCode;
            this.city = city;
            this.phoneNumber = phoneNumber;
            this.email = email;
        }
    }
}
