using System;

namespace BonTemps
{
    public struct TableOrders
    {
        private ulong? tableOrderID;
        private ulong? tableID;
        private ulong? orderID;

        public ulong? TableOrderID { get { return this.tableOrderID; } set { this.tableOrderID = value; } }
        public ulong? TableID { get { return this.tableID; } set { this.tableID = value; } }
        public ulong? OrderID { get { return this.orderID; } set { this.orderID = value; } }

        public TableOrders(ulong tableOrderID, ulong tableID, ulong orderID)
        {
            this.tableOrderID = tableOrderID;
            this.tableID = tableID;
            this.orderID = orderID;
        }
        public override string ToString()
        {
            object[] info = new object[] {
                this.tableOrderID.ToString(), this.tableID.ToString(), this.orderID.ToString()
            };
            return String.Format("{0}\n{1}\n{2}\n", info);
        }
    }
}
