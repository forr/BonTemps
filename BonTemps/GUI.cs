using System;
using System.Windows;
using System.Windows.Forms;

namespace BonTemps
{
    public enum TableStatus { Empty, Ordered, OnTime, NotOnTime }

    public sealed class TableLayout
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
        public TableLayout(System.Drawing.Image bmpTableImage, int tableID, string clientID, TableStatus tableStatus)
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

            foreach (char c in name)
            {
                if (Char.IsDigit(c))
                {
                    idConverted += c;
                }
            }
            int.TryParse(idConverted, out TableID);
            return TableID;
        }
    }

    // There may be a few mistakes here, Ryan... check it :))
    public sealed class ChefView
    {
        public static Panel MenuPanel()
        {
            Panel pnl = new Panel();
            pnl.Location = new System.Drawing.Point(0, 0);
            pnl.AutoSize = true;
            pnl.BorderStyle = BorderStyle.Fixed3D;

            ListBox lbxCurrentMenu = new ListBox();
            foreach (Menu m in Database.GetAllMenus())
            {
                lbxCurrentMenu.Items.Add(m.ToString());
            }
            lbxCurrentMenu.Location = new System.Drawing.Point(0, 0);
            lbxCurrentMenu.AutoSize = true;
            return pnl;
        }
    }

    public sealed class ManagerView
    {
        public static Panel MenuPanel()
        {
            int iHeightPos = 0;
            int iSkipPos = 12;

            //Top Layer
            Panel pnlMain = new Panel();
            pnlMain.Location = new System.Drawing.Point(0, 0);
            pnlMain.AutoSize = true;
            pnlMain.BorderStyle = BorderStyle.Fixed3D;

            //Sub Layers
            Label lblCurrentMenu = new Label();
            lblCurrentMenu.Text = "Current Menu:";
            lblCurrentMenu.AutoSize = true;
            lblCurrentMenu.Location = new System.Drawing.Point((int)0, (int)iHeightPos);
            iHeightPos += lblCurrentMenu.Height;

            ListBox lbxCurrentMenu = new ListBox();
            foreach (Menu m in Database.GetAllMenus())
            {
                lbxCurrentMenu.Items.Add(m.ToString());
            }
            lbxCurrentMenu.Location = new System.Drawing.Point(0, iHeightPos);
            lbxCurrentMenu.AutoSize = true;
            iHeightPos += lbxCurrentMenu.Height;

            iHeightPos += iSkipPos;
             
            pnlMain.Controls.Add(lblCurrentMenu);
            pnlMain.Controls.Add(lbxCurrentMenu);
            pnlMain.Show();

            return pnlMain;
        }

        public enum ObjectType { Form, Panel };

        public static Control[] ObjectControlArray(object sender, ObjectType objType)
        {
            Control[] ctrl;
            switch (objType)
            {
                case ObjectType.Panel:
                    Panel pnlObj = (Panel)sender;
                    ctrl = new Control[pnlObj.Controls.Count];
                    pnlObj.Controls.CopyTo(ctrl, 0);
                    break;
                case ObjectType.Form:
                    Form frmObj = (Form)sender;
                    ctrl = new Control[frmObj.Controls.Count];
                    frmObj.Controls.CopyTo(ctrl, 0);
                    break;
                default:
                    ctrl = (ctrl = new Control[0]);
                    break;
            }
            return ctrl;
        }
    }
}