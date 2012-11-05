using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonTemps
{
    public struct ClientInfo
    {
        //private int id;
        private string firstname;
        private string lastname;
        private string address;
        private string postalcode;
        private string city;        
        private string phonenumber;
        private string email;

        //public int ID { get { return this.id; } }
        public string FirstName { get { return this.firstname; } }
        public string LastName { get { return this.lastname; } }
        public string Address { get { return this.address; } }
        public string PostalCode { get { return this.postalcode; } }
        public string City { get { return this.city; } }        
        public string PhoneNumber { get { return this.phonenumber; } }
        public string Email { get { return this.email; } }

        public ClientInfo(string pFirstName, string pLastName, string pAddress, string pPostalCode, string pCity, string pPhoneNumber, string pEmail)
        {
            this.firstname = pFirstName;
            this.lastname = pLastName;
            this.address = pAddress;
            this.postalcode = pPostalCode;
            this.city = pCity;
            this.phonenumber = pPhoneNumber;
            this.email = pEmail;
        }
    }
}
