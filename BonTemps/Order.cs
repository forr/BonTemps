using System;

namespace BonTemps
{
    public struct Order
    {
        private ulong? orderID;
        private ulong? clientID;
        private string menuItemIDs;
        private DateTime? startDateTime;
        private DateTime? endDateTime;

        public static Order Null = new Order(null);
        public ulong? OrderID { get { return this.orderID; } set { this.orderID = value; } }
        public ulong? ClientID { get { return this.clientID; } set { this.clientID = value; } }
        public string MenuItemIDs { get { return this.menuItemIDs; } set { this.menuItemIDs = value; } }
        public DateTime? StartDateTime { get { return this.startDateTime; } set { this.startDateTime = value; } }
        public DateTime? EndDateTime { get { return this.endDateTime; } set { this.endDateTime = value; } }


        public Order(ulong? orderID)
        {
            this.orderID = orderID;
            this.clientID = null;
            this.menuItemIDs = String.Empty;
            this.startDateTime = null;
            this.endDateTime = null;
        }
        public Order(ulong orderID, ulong clientID, string menuItemIDs, DateTime startDateTime, DateTime endDateTime)
            : this(orderID)
        {
            this.clientID = clientID;
            this.menuItemIDs = menuItemIDs;
            this.startDateTime = startDateTime;
            this.endDateTime = endDateTime;
        }
        public override string ToString()
        {
            object[] info = new object[] {
                this.orderID.ToString(), this.clientID.ToString(), this.MenuItemIDs, this.startDateTime.Value.ToLongDateString(), this.endDateTime.Value.ToLongDateString()
            };
            return String.Format("{0};{1};{2};{3};{4}", info);
        }
    }
}
