using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonTemps
{
    public struct OrderInfo
    {
        private int clientID;
        private int tableID;
        private int menuID;

        public int ClientID { get { return this.clientID; } }
        public int TableID { get { return this.tableID; } }
        public int MenuID { get { return this.menuID; } }

        public OrderInfo(int cID, int tID, int mID)
        {
            this.clientID = cID;
            this.tableID = tID;
            this.menuID = mID;
        }
    }
}
