namespace BonTemps
{
    public struct Tables
    {
        private ulong? tableID;
        private uint tableNumber;
        private uint amountOfChairs;

        public ulong? TableID { get { return this.tableID; } }
        public uint TableNumber { get { return this.tableNumber; } }
        public uint AmountOfChairs { get { return this.amountOfChairs; } }

        public Tables(ulong tableID, uint tableNumber, uint amountOfChairs)
        {
            this.tableID = tableID;
            this.tableNumber = tableNumber;
            this.amountOfChairs = amountOfChairs;
        }
    }
}
