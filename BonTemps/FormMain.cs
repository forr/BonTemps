using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace BonTemps
{
    public partial class formMain : Form
    {
        TableLayout[] tables = new TableLayout[22];
        int tableSize = 0;
        int tableMultiplier = 0;

        #region Temp
        //======================================================================================
        //======================================================================================
        //======================================================================================

        List<Client> clients = new List<Client>();

        private void CreateTempClientList()
        {
            
            for (int iClient = 1; (iClient - 1) < 2; iClient++)
            {
                clients.Add(new Client(null, "Name" + iClient.ToString(),
                                           "LastName",
                                           "Address",
                                           "Postcode",
                                           "Shitty",
                                           "0492000000",
                                           "Emairu"));
            }
        }

        private void AddClient()
        {
            clients.Add(new Client(null, "tbxFirstName.Text",
                                       "tbxLastName.Text",
                                       "tbxAddress.Text",
                                       "tbxPostcode.Text",
                                       "tbxCity.Text",
                                       "tbxPhoneNumber.Text",
                                       "tbxEmail.Text"));
        }

        private void CreateTable()
        {
            for (int iTable = 0; iTable < tables.Count(); iTable++)
            {
                if (iTable == 4 | iTable == 5)
                {
                    this.tables[iTable] = new TableLayout(Properties.Resources.table.GetThumbnailImage(tableSize, tableSize, null, IntPtr.Zero), iTable, clients[1].FirstName, TableStatus.NOTONTIME);
                    continue;
                }
                else if (iTable == 2)
                {
                    this.tables[iTable] = new TableLayout(Properties.Resources.table.GetThumbnailImage(tableSize, tableSize, null, IntPtr.Zero), iTable, clients[0].FirstName, TableStatus.ORDERED);
                    continue;
                }
                this.tables[iTable] = new TableLayout(Properties.Resources.table.GetThumbnailImage(tableSize, tableSize, null, IntPtr.Zero), iTable, "", TableStatus.EMPTY);
            }
        }

        /*  Database Connect
         *  private void GetClients()
         *  {
         *      ClientInfo[] NewClientList = Database.GetClients();
         *      Foreach(ClientInfo ci in NewClientList)
         *      {
         *          clients.Add(ci); // clients <- List<typeof(ClientInfo)>
         *      }
         *  }
         */

        //======================================================================================
        //======================================================================================
        //======================================================================================
        #endregion Temp

        public formMain()
        {
            InitializeComponent();

            this.tableSize = GetTableWidth();

            #region Temp
            //======================================================================================
            //======================================================================================
            //======================================================================================
            this.CreateTempClientList();
            this.CreateTable();

            //======================================================================================
            //======================================================================================
            //======================================================================================
            #endregion Temp
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.TopMost = true;
            this.pnlOverview.AutoScroll = true;
            this.ShowTables();
        }

        private Point GetClientIDLocation()
        {
            //Centered on Location (sizes(100%,100%))
            double relevantWidth = 50;
            double relevantHeight = 75;
            double currentPanelSize = tableSize;

            Point p = new Point(Convert.ToInt32((relevantWidth / 100) * currentPanelSize), Convert.ToInt32((relevantHeight / 100) * currentPanelSize));

            return p;
        }

        private int GetTableWidth()
        {
            int screen_width = (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - this.pnlOrder.Width - this.lbxOverview.Width) - 11;
            if(screen_width < 240)
            {
                this.tableMultiplier = 2;
            }
            else if (screen_width >=240 && screen_width < 480)
            {
                this.tableMultiplier = 4;
            }
            else if (screen_width >= 480 && screen_width < 1024)
            {
                this.tableMultiplier = 6;
            }
            else if (screen_width >= 1024 && screen_width < 1440)
            {
                this.tableMultiplier = 8;
            }
            else
            {
                this.tableMultiplier = 10;
            }

            int table_width = ((screen_width / this.tableMultiplier) - 4);

            return table_width;
        }

        private void ShowTables()
        {
            int pos_x = 0;
            int pos_y = 2;

            int table_width = tableSize;
            int table_height = tableSize;

            for (int i = 1; i <= this.tables.Count(); i++)
            {
                pos_x += 2;

                Panel pnlTable = new Panel();
                pnlTable.Width = table_width;
                pnlTable.Height = table_height;
                pnlTable.Location = new Point(pos_x, pos_y);
                pnlTable.BackgroundImage = this.tables[(i - 1)].bmpTableImage;
                pnlTable.BackgroundImageLayout = ImageLayout.Stretch;
                pnlTable.Name = TableLayout.GetTableName("pnlTable", (i - 1));
                pnlTable.BorderStyle = BorderStyle.FixedSingle;
                pnlTable.Click += new EventHandler(this.pnlTable_Click);

                Label lblTableStatus = new Label();
                lblTableStatus.Text = TableLayout.GetTableName("Table ", (i)); // Removed - 1 from "(i - 1)"; still need to fix the array element part
                lblTableStatus.AutoSize = true;
                lblTableStatus.Location = new Point(((pos_x + table_width) - lblTableStatus.Width) - 1, (pos_y) + 1);
                lblTableStatus.TextAlign = ContentAlignment.MiddleRight;
                lblTableStatus.ForeColor = System.Drawing.Color.White;
                lblTableStatus.Font = new Font(SystemFonts.CaptionFont.FontFamily, SystemFonts.CaptionFont.Size, FontStyle.Bold);

                Label lblClientID = new Label();
                Point pLblClientID = this.GetClientIDLocation();
                lblClientID.Location = new Point(((pos_x + pLblClientID.X) - (lblClientID.Width/2)), (pos_y + pLblClientID.Y));
                lblClientID.TextAlign = ContentAlignment.MiddleCenter;
                lblClientID.ForeColor = System.Drawing.Color.Black;
                lblClientID.BackColor = System.Drawing.Color.White;
                lblClientID.Text = this.tables[(i - 1)].clientID;

                switch (this.tables[(i - 1)].tableStatus)
                {
                    case TableStatus.EMPTY:
                        lblTableStatus.BackColor = System.Drawing.Color.Blue;
                        break;
                    case TableStatus.NOTONTIME:
                        lblTableStatus.BackColor = System.Drawing.Color.FromArgb(255,99,25);
                        lblClientID.ForeColor = System.Drawing.Color.White;
                        lblClientID.BackColor = System.Drawing.Color.Red;
                        break;
                    case TableStatus.ONTIME:
                        lblTableStatus.BackColor = System.Drawing.Color.Green;
                        break;
                    case TableStatus.ORDERED:
                        lblTableStatus.BackColor = System.Drawing.Color.DarkRed;
                        break;
                }

                pos_x += table_width + 2;

                if (i % this.tableMultiplier == 0 && i != 0)
                {
                    pos_y += 2;
                    pos_y += table_height;
                    pos_x = 0;
                }

                pnlOverview.Controls.Add(lblTableStatus);
                pnlOverview.Controls.Add(lblClientID);
                pnlOverview.Controls.Add(pnlTable);
            }

            pos_y += 4;
        }

        private void pnlTable_Click(object sender, EventArgs e)
        {
            Panel pnlSender = (Panel)sender;
            int currentTableID = 0;
            currentTableID = TableLayout.GetTableID(pnlSender.Name);
            tbxTableID_pnlOrder.Text = this.tables[currentTableID].tableID.ToString();
            tbxClientID_pnlOrder.Text = this.tables[currentTableID].clientID;
            try
            {
                tbxClientName_pnlOrder.Text = this.clients[Convert.ToInt32(this.tables[currentTableID].clientID)].FirstName;
            }
            catch
            {
                tbxClientName_pnlOrder.Text = "unknown";
            }
        }

        #region MenuStrip
        private void fileToolStripMenuItem2_DropDownOpened(object sender, EventArgs e)
        {
            fileToolStripMenuItem2.ForeColor = Color.Black;
        }

        private void fileToolStripMenuItem2_DropDownClosed(object sender, EventArgs e)
        {
            fileToolStripMenuItem2.ForeColor = Color.White;
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure that you want to exit?", "Exit Program", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void exitToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void minimizeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void exportClientsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Richtext file (*.rtf)|*.rtf";
            sfd.DefaultExt = "rtf";
            sfd.AddExtension = true;
            sfd.FileName = "ClientListExport_" + DateTime.Now.ToShortDateString().ToString();
            sfd.ShowDialog();
        }
        #endregion MenuStrip
    }
}
