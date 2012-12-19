using System;
using System.Globalization;

namespace BonTemps
{
    public struct Order
    {
        private ulong? orderID;
        private ulong? clientID;
        private DateTime? startDateTime;
        private DateTime? endDateTime;

        public static Order Null = new Order(null);
        public ulong? OrderID { get { return this.orderID; } set { this.orderID = value; } }
        public ulong? ClientID { get { return this.clientID; } set { this.clientID = value; } }
        public DateTime? StartDateTime { get { return this.startDateTime; } set { this.startDateTime = value; } }
        public DateTime? EndDateTime { get { return this.endDateTime; } set { this.endDateTime = value; } }

        public Order(ulong? orderID)
        {
            this.orderID = orderID;
            this.clientID = null;
            this.startDateTime = null;
            this.endDateTime = null;
        }
        public Order(ulong orderID, ulong clientID, DateTime startDateTime, DateTime endDateTime)
            : this(orderID)
        {
            this.clientID = clientID;
            this.startDateTime = startDateTime;
            this.endDateTime = endDateTime;
        }

        public static void AddOrder(ulong clientID, DateTime startDateTime, DateTime endDateTime)
        {
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "yyyy-MM-dd HH:mm:ss";

            string[] sArray = {
                                clientID.ToString(),
                                startDateTime.ToString(dtfi),
                                endDateTime.ToString(dtfi)
                              };
            new Database().Insert(Database.TableName.Orders, sArray);
        }

        public static void DeleteOrder(int id)
        {
            new Database().Delete(Database.TableName.Orders, id);
        }

        public override string ToString()
        {
            object[] info = new object[] {
                this.orderID.ToString(), this.clientID.ToString(), this.startDateTime.Value.ToLongDateString(), this.endDateTime.Value.ToLongDateString()
            };
            return String.Format("{0};{1};{2};{3};{4}", info);
        }
    }
}
