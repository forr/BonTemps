using System;

namespace BonTemps
{
    public struct AccessDenied
    {
        private uint? blockedID;
        private string machineID;
        private DateTime? blockedSince;
        private DateTime? blockedUntil;

        public static AccessDenied Null = new AccessDenied(null);
        public uint? BlockedID { get { return this.blockedID; } set { this.blockedID = value; } }
        public string MachineID { get { return this.machineID; } set { this.machineID = value; } }
        public DateTime? BlockedSince { get { return this.blockedSince; } set { this.blockedSince = value; } }
        public DateTime? BlockedUntil { get { return this.blockedUntil; } set { this.blockedUntil = value; } }

        public AccessDenied(uint? blockedID)
        {
            this.blockedID = blockedID;
            this.machineID = null;
            this.blockedSince = null;
            this.blockedUntil = null;
        }
        public AccessDenied(uint? blockedID, string machineID, DateTime? blockedSince, DateTime? blockedUntil)
            : this(blockedID)
        {
            this.machineID = machineID;
            this.blockedSince = blockedSince;
            this.blockedUntil = blockedUntil;
        }

        public override string ToString()
        {
            object[] info = new object[] {
                this.blockedID.ToString(), this.machineID, this.blockedSince.Value.ToLongDateString(), this.blockedUntil.Value.ToLongDateString()
            };
            return String.Format("{0}\n{1}\n{2}\n{3}\n", info);
        }
    }
}
