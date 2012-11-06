using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonTemps
{
    public struct TableOrders
    {
        private ulong? tableOrderID;
        private ulong? tableID;
        private ulong? orderID;

        public ulong? TableOrderID { get { return this.tableOrderID; } }
        public ulong? TableID { get { return this.tableID; } }
        public ulong? OrderID { get { return this.orderID; } }

        public TableOrders(ulong tableOrderID, ulong tableID, ulong orderID)
        {
            this.tableOrderID = tableOrderID;
            this.tableID = tableID;
            this.orderID = orderID;
        }
    }
}
