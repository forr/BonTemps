using System;

namespace BonTemps
{
    public struct Table
    {
        private ulong? tableID;
        private uint tableNumber;
        private uint amountOfChairs;

        public static Table Null = new Table(null);
        public ulong? TableID { get { return this.tableID; } set { this.tableID = value; } }
        public uint TableNumber { get { return this.tableNumber; } set { this.tableNumber = value; } }
        public uint AmountOfChairs { get { return this.amountOfChairs; } set { this.amountOfChairs = value; } }

        public Table(ulong? tableID)
        {
            this.tableID = tableID;
            this.tableNumber = 0;
            this.amountOfChairs = 0;
        }
        public Table(ulong tableID, uint tableNumber, uint amountOfChairs)
            : this(tableID)
        {
            this.tableNumber = tableNumber;
            this.amountOfChairs = amountOfChairs;
        }

        public static void AddTable(uint tableNumber, uint amountOfChairs)
        {
            string[] sArray = {
                                tableNumber.ToString(),
                                amountOfChairs.ToString()
                              };
            new Database().Insert(Database.TableName.Tables, sArray);
        }

        public static void DeleteTable(int id)
        {
            new Database().Delete(Database.TableName.Tables, id);
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
