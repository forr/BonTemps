namespace BonTemps
{
    public struct Persons
    {
        private ulong? personID;
        private ulong? menuID;
        private ulong? orderID;

        public ulong? PersonID { get { return this.personID; } }
        public ulong? MenuID { get { return this.menuID; } }
        public ulong? OrderID { get { return this.orderID; } }

        public Persons(ulong personID, ulong menuID, ulong orderID):this()
        {
            this.personID = personID;
            this.menuID = menuID;
            this.orderID = orderID;
        }
    }
}
