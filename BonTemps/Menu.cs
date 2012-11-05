using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonTemps
{
    public struct Menu
    {
        private int id;
        private string name;

        public int ID { get { return this.id; } }
        public string Name { get { return this.name; } }

        public Menu(string name)
        {
            this.id = -1;
            this.name = name;
        }
    }

    public enum TableStatus
    {
        Empty,
        Ordered,
        OnTime,
        NotOnTime
    }

    public struct Table
    {
        public System.Drawing.Image bmpTableImage;
        public int tableID;
        public string clientID; //Can be empty
        public TableStatus tableStatus;

        /// <summary>
        /// Table Data
        /// </summary>
        /// <param name="bmpTableImage">Use a image.</param>
        /// <param name="TableID">Enter a particular ID</param>
        /// <param name="ClientID">Give used ClientID (Normally obtained by using the standart included ClientInfo Class/Structure).</param>
        /// <param name="tableStatus">Status of the current table (Use enum TableStatus)</param>
        public Table(System.Drawing.Image bmpTableImage, int tableID, string clientID, TableStatus tableStatus)
        {
            this.bmpTableImage = bmpTableImage;
            this.tableID = tableID;
            this.clientID = clientID;
            this.tableStatus = tableStatus;
        }

        /// <summary>
        /// Gets/Sets TableName.
        /// </summary>
        /// <returns>Returns Name + ID from ID</returns>
        /// <param name="name">Best to use with either the type or a name for the object to which this will be applied on.</param>
        /// <param name="id">Obtainable trough a For or Foreach Loop. (Table.clientID)</param>
        public static string GetTableName(string name, int id)
        {
            string objectID = name + id.ToString();
            return objectID;
        }

        /// <summary>
        /// Gets/Sets TableID from a Panel.
        /// </summary>
        /// <returns>Returns ID from a Panels Name</returns>
        /// <param name="name">Best to use with either the type or a name for the object to which this will be applied on.</param>
        /// <param name="id">Obtainable trough a For or Foreach Loop. (Table.clientID)</param>
        public static int GetTableID(string name)
        {
            int TableID = 0;
            string idConverted = String.Empty;

            foreach(char c in name)
            {
                if(Char.IsDigit(c))
                {
                    idConverted += c;
                }
            }
            int.TryParse(idConverted, out TableID);
            return TableID;
        }
    }
}
