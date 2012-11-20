using System;

namespace BonTemps
{
    public struct Order
    {
        private ulong? orderID;
        private ulong? clientID;
        private DateTime startDateTime;
        private DateTime endDateTime;

        public ulong? OrderID { get { return this.orderID; } set { this.orderID = value; } }
        public ulong? ClientID { get { return this.clientID; } set { this.clientID = value; } }
        public DateTime StartDateTime { get { return this.startDateTime; } set { this.startDateTime = value; } }
        public DateTime EndDateTime { get { return this.endDateTime; } set { this.endDateTime = value; } }

        public Order(ulong orderID, ulong clientID, DateTime startDateTime, DateTime endDateTime)
        {
            this.orderID = orderID;
            this.clientID = clientID;
            this.startDateTime = startDateTime;
            this.endDateTime = endDateTime;
        }
        public override string ToString()
        {
            object[] info = new object[] {
                this.orderID.ToString(), this.clientID.ToString(), this.startDateTime.ToLongDateString(), this.endDateTime.ToLongDateString()
            };
            return String.Format("{0}\n{1}\n{2}\n{3}\n", info);
        }
    }
}
