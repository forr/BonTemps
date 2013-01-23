using System;

namespace BonTemps
{
    public struct TableOrder
    {
        private ulong? tableOrderID;
        private ulong? tableID;
        private ulong? orderID;
        private bool orderReady;

        public static TableOrder Null = new TableOrder(null);
        public ulong? TableOrderID { get { return this.tableOrderID; } set { this.tableOrderID = value; } }
        public ulong? TableID { get { return this.tableID; } set { this.tableID = value; } }
        public ulong? OrderID { get { return this.orderID; } set { this.orderID = value; } }
        public bool OrderReady { get { return this.orderReady; } set { this.orderReady = value; } }

        public TableOrder(ulong? tableOrderID)
        {
            this.tableOrderID = tableOrderID;
            this.tableID = null;
            this.orderID = null;
            this.orderReady = false;
        }
        public TableOrder(ulong tableOrderID, ulong tableID, ulong orderID, bool orderReady)
            : this(tableOrderID)
        {
            this.tableID = tableID;
            this.orderID = orderID;
            this.orderReady = orderReady;
        }

        public static void AddTableOrders(ulong menuID, ulong orderID)
        {
            string[] sArray = {
                                menuID.ToString(),
                                orderID.ToString()
                              };
            new Database().Insert(Database.TableName.TableOrders, sArray);
        }

        public static void DeleteTableOrders(int id)
        {
            new Database().Delete(Database.TableName.TableOrders, id);
        }

        public override string ToString()
        {
            object[] info = new object[] {
                this.tableOrderID.ToString(), this.tableID.ToString(), this.orderID.ToString(), this.orderReady.ToString()
            };
            return String.Format("{0}\n{1}\n{2}\n{3}\n", info);
        }
    }
}
