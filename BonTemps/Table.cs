using System;

namespace BonTemps
{
    public struct Table
    {
        private ulong? tableID;
        private uint tableNumber;
        private uint amountOfChairs;

        public ulong? TableID { get { return this.tableID; } set { this.tableID = value; } }
        public uint TableNumber { get { return this.tableNumber; } set { this.tableNumber = value; } }
        public uint AmountOfChairs { get { return this.amountOfChairs; } set { this.amountOfChairs = value; } }

        public Table(ulong tableID, uint tableNumber, uint amountOfChairs)
        {
            this.tableID = tableID;
            this.tableNumber = tableNumber;
            this.amountOfChairs = amountOfChairs;
        }
        public override string ToString()
        {
            object[] info = new object[] {
                this.tableID.ToString(), this.tableNumber.ToString(), this.amountOfChairs.ToString()
            };
            return String.Format("{0}\n{1}\n{2}\n", info);
        }
    }
}
