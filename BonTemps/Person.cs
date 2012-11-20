using System;

namespace BonTemps
{
    public struct Person
    {
        private ulong? personID;
        private ulong? menuID;
        private ulong? orderID;

        public ulong? PersonID { get { return this.personID; } set { this.personID = value; } }
        public ulong? MenuID { get { return this.menuID; } set { this.menuID = value; } }
        public ulong? OrderID { get { return this.orderID; } set { this.orderID = value; } }

        public Person(ulong personID, ulong menuID, ulong orderID):this()
        {
            this.personID = personID;
            this.menuID = menuID;
            this.orderID = orderID;
        }
        public override string ToString()
        {
            object[] info = new object[] {
                this.personID.ToString(), this.menuID.ToString(), this.orderID.ToString()
            };
            return String.Format("{0}\n{1}\n{2}\n", info);
        }
    }
}
