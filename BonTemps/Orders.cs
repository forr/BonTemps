using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonTemps
{
    public struct Orders
    {
        private ulong? orderID;
        private ulong? clientID;
        private DateTime startDateTime;
        private DateTime endDateTime;

        public ulong? OrderID { get { return this.orderID; } }
        public ulong? ClientID { get { return this.clientID; } }
        public DateTime StartDateTime { get { return this.startDateTime; } }
        public DateTime EndDateTime { get { return this.endDateTime; } }

        public Orders(ulong orderID, ulong clientID, DateTime startDateTime, DateTime endDateTime)
        {
            this.orderID = orderID;
            this.clientID = clientID;
            this.startDateTime = startDateTime;
            this.endDateTime = endDateTime;
        }
    }
}
