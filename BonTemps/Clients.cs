﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonTemps
{
    public struct Clients
    {
        private ulong? clientID;
        private string name;
        private string address;
        private string postalCode;
        private string city;
        private string phoneNumber;
        private string email;

        public ulong? ClientID { get { return this.clientID; } }
        public string Name { get { return this.name; } }
        public string Address { get { return this.address; } }
        public string PostalCode { get { return this.postalCode; } }
        public string City { get { return this.city; } }
        public string PhoneNumber { get { return this.phoneNumber; } }
        public string Email { get { return this.email; } }

        public Clients(ulong clientID, string name, string address, string postalCode, string city, string phoneNumber, string email)
        {
            this.clientID = clientID;
            this.name = name;
            this.address = address;
            this.postalCode = postalCode;
            this.city = city;
            this.phoneNumber = phoneNumber;
            this.email = email;
        }
    }
}
