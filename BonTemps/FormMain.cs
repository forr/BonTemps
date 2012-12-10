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
    public partial class FormMain : Form
    {
        private string initialUser;

        private Database db = new Database();
        List<TableLayout> tables;
        int tableSize = 0;
        int tableMultiplier = 0;
        List<Client> clients;

        public FormMain(string initialValue)
        {
            this.initialUser = initialValue;

            InitializeComponent();

            this.InitializeTabData();
            this.InitializeOrders();


            if (tpNewOrder.Tag.ToString().Contains(initialUser))
            {
                this.fillLbxClientList();
            }
            this.tableSize = GetTableWidth();            
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.TopMost = true;
            this.pnlOverview.AutoScroll = true;
            this.clients = db.GetAllClients().ToList<Client>();
            this.tables = new List<TableLayout>();
            for (int i = 0; i <= db.GetAllTables().Count(); i++)
            {
                tables.Add(new TableLayout(Properties.Resources.table, i, String.Empty, TableStatus.Empty));
            }
            this.ShowTables();
            foreach (Table t in new Database().GetAllTables())
            {
                this.comboBox1.Items.Add(t.TableNumber);
                try
                {
                    this.comboBox1.SelectedIndex = 1;
                }
                catch
                {
                    // no tables
                }
            }
            for (int i = 0; i < this.DisplayMenuItems().Controls.Count; i++)
            {
                this.pnlMenuSelectContainer.Controls.Add(this.DisplayMenuItems().Controls[i]);
            }
        }

        private void InitializeOrders()
        {
            ListViewItem item1 = new ListViewItem("Something");
            item1.SubItems.Add("SubItem1a");
            item1.SubItems.Add("SubItem1b");

            ListViewItem item2 = new ListViewItem("Something2");
            item2.SubItems.Add("SubItem2a");
            item2.SubItems.Add("SubItem2a");

            ListViewItem item3 = new ListViewItem("Somethin3");
            item3.SubItems.Add("SubItem3a");
            item3.SubItems.Add("SubItem3a");

            this.lvOrders.Items.AddRange(new ListViewItem[] { item1, item2, item3 });
        }

        private void InitializeTabData()
        {
            foreach(TabPage tp in tctrlInterface.TabPages)
            {
                bool delete = true;
                try
                {
                    string [] allowedUsers = tp.Tag.ToString().Split(",".ToCharArray(),StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < allowedUsers.Count(); i++)
                    {
                        if (initialUser == allowedUsers[i])
                        {
                            delete = false;
                        }
                    }
                    if (allowedUsers.Count() == 0)
                    {
                        delete = false;
                    }
                    if (delete == true)
                    {
                        tctrlInterface.TabPages.Remove(tp);
                    }
                }
                catch
                {
                    // tag is null, it's allowed to be used by all.
                    return;
                }
            }
        }

        private void fillLbxClientList()
        {
            foreach (Client cl in new Database().GetAllClients())
            {
                lbxClientList.Items.Add(cl.ToString());
            }
        }

        private Point GetClientIDLocation()
        {
            //centered on Location (sizes(100%,100%))
            double relevantWidth = 50;
            double relevantHeight = 75;

            double currentPanelSize = tableSize;

            Point p = new Point(Convert.ToInt32((relevantWidth / 100) * currentPanelSize), Convert.ToInt32((relevantHeight / 100) * currentPanelSize));

            return p;
        }

        private int GetTableWidth()
        {
            int screen_width = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - this.pnlOrder.Width) - 11;
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
                pnlTable.Name = TableLayout.GetTableName("pnlTable", (i-1));
                pnlTable.BorderStyle = BorderStyle.FixedSingle;
                pnlTable.Click += new EventHandler(this.pnlTable_Click);

                Label lblTableStatus = new Label();
                lblTableStatus.Text = TableLayout.GetTableName("pnlTable", (i - 1));
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
                    case TableStatus.Empty:
                        lblTableStatus.BackColor = System.Drawing.Color.Blue;
                        break;
                    case TableStatus.NotOnTime:
                        lblTableStatus.BackColor = System.Drawing.Color.FromArgb(255,99,25);
                        lblClientID.ForeColor = System.Drawing.Color.White;
                        lblClientID.BackColor = System.Drawing.Color.Red;
                        break;
                    case TableStatus.OnTime:
                        lblTableStatus.BackColor = System.Drawing.Color.Green;
                        break;
                    case TableStatus.Ordered:
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

                this.pnlOverview.Controls.Add(lblTableStatus);
                this.pnlOverview.Controls.Add(lblClientID);
                this.pnlOverview.Controls.Add(pnlTable);
            }

            pos_y += 4;
        }

        private void pnlTable_Click(object sender, EventArgs e)
        {
            Panel pnlSender = (Panel)sender;
            int currentTableID = 0;
            currentTableID = TableLayout.GetTableID(pnlSender.Name);
            this.tbxTableID_pnlOrder.Text = this.tables[currentTableID].tableID.ToString();
            this.tbxClientID_pnlOrder.Text = this.tables[currentTableID].clientID;
            try
            {
                this.tbxClientName_pnlOrder.Text = this.clients[Convert.ToInt32(this.tables[currentTableID].clientID)].FirstName;
            }
            catch
            {
                this.tbxClientName_pnlOrder.Text = "Unknown";
            }
        }

        #region MenuStrip
        private void fileToolStripMenuItem2_DropDownOpened(object sender, EventArgs e)
        {
            this.fileToolStripMenuItem2.ForeColor = Color.Black;
        }

        private void fileToolStripMenuItem2_DropDownClosed(object sender, EventArgs e)
        {
            this.fileToolStripMenuItem2.ForeColor = Color.White;
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

        private void plnSearchName_MouseClick(object sender, MouseEventArgs e)
        {
            string name = tbxSearchName.Text;
            List<Client> ResultingClients = new List<Client>();
            ResultingClients = new Database().GetClientListByName(name);
            this.lbxClientList.Items.Clear();
            foreach (Client cl in ResultingClients)
            {
                this.lbxClientList.Items.Add(cl.ToString());
            }
            if (this.lbxClientList.Items.Count == 0)
            {
                this.lbxClientList.Items.Add("Failed to Find any results.");
            }
            this.lbxClientList.Items.Add("");
            this.lbxClientList.Items.Add("/.. - Return to Full List");
        }

        private void lbxClientList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lbxClientList.GetItemText(this.lbxClientList.SelectedItem).Contains("/.."))
            {
                this.lbxClientList.Items.Clear();
                this.fillLbxClientList();
            }
            else if (this.lbxClientList.GetItemText(this.lbxClientList.SelectedItem) == String.Empty)
            {
                // Do nothing
                return;
            }
            else
            {
                String[] FillInfo = this.lbxClientList.GetItemText(this.lbxClientList.SelectedItem).Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                this.tbxClientID_pnlOrder.Text = FillInfo[0]; // ID
                this.tbxClientName_pnlOrder.Text = FillInfo[1] + " " + FillInfo[2]; // FirstName + LastName
            }
        }

        private void btnCreateNewOrder_Click(object sender, EventArgs e)
        {

        }

        private void btnSelectMenuItems_Click(object sender, EventArgs e)
        {
            this.tctrlInterface.SelectedTab = this.tpMenuSelection;
        }

        private Panel DisplayMenuItems()
        {
            int defaultSkipHeight = 10; // 10 px;
            int defaultSkipWidth = 4;
            int currentHeight = 4;
            int currentIndexOf = 0;
            List<Menu> MenuItems = new Database().GetAllMenus();

            Panel pnl = new Panel();

            pnl.AutoScroll = false;
            pnl.AutoSize = true;
            pnl.Width = 2000;
            pnl.Height = 1000;

            foreach (Menu m in MenuItems)
            {
                int currentWidth = 4;

                Label lblItemID = new Label();
                Label lblItemName = new Label();
                Label lblItemPrice = new Label();
                TextBox tbxAmmount = new TextBox();
                Button btnRemove = new Button();
                Button btnAdd = new Button();

                lblItemID.Name = String.Format("lblItemID{0}", currentIndexOf.ToString());
                lblItemName.Name = "lblItemName" + currentIndexOf.ToString();
                lblItemPrice.Name = "lblItemPrice" + currentIndexOf.ToString();
                tbxAmmount.Name = "tbxAmmount" + currentIndexOf.ToString();
                btnRemove.Name = "btnRemove" + currentIndexOf.ToString();
                btnAdd.Name = "btnAdd" + currentIndexOf.ToString();

                lblItemID.Enabled = true;
                lblItemName.Enabled = true;
                lblItemPrice.Enabled = true;
                tbxAmmount.Enabled = true;
                btnRemove.Enabled = true;
                btnAdd.Enabled = true;

                lblItemID.AutoSize = true;
                lblItemName.AutoSize = true;
                lblItemPrice.AutoSize = true;
                tbxAmmount.AutoSize = true;
                btnRemove.AutoSize = true;
                btnAdd.AutoSize = true;

                lblItemID.Text = m.MenuID.Value.ToString();
                lblItemName.Text = m.Entree + "\n" +
                              m.MainCourse + "\n" +
                              m.Dessert;
                lblItemPrice.Text = m.Price.ToString();
                tbxAmmount.Text = "0";
                btnRemove.Text = "-";
                btnAdd.Text = "+";

                lblItemID.Location = new Point(currentWidth, currentHeight);
                currentWidth += defaultSkipWidth + lblItemID.Width;
                lblItemName.Location = new Point(currentWidth, currentHeight);
                currentWidth += defaultSkipWidth + lblItemName.Width;
                lblItemPrice.Location = new Point(currentWidth, currentHeight);
                currentWidth += defaultSkipWidth + lblItemPrice.Width;
                tbxAmmount.Location = new Point(currentWidth, currentHeight);
                currentWidth += defaultSkipWidth + tbxAmmount.Width;
                btnRemove.Location = new Point(currentWidth, currentHeight);
                currentWidth += defaultSkipWidth + btnAdd.Width;
                btnAdd.Location = new Point(currentWidth, currentHeight);

                btnRemove.Click += new System.EventHandler(this.btnMenuItemsRemove_Click);
                btnAdd.Click += new System.EventHandler(this.btnMenuItemsAdd_Click);

                pnl.Controls.Add(lblItemID);
                pnl.Controls.Add(lblItemName);
                pnl.Controls.Add(lblItemPrice);
                pnl.Controls.Add(tbxAmmount);
                pnl.Controls.Add(btnRemove);
                pnl.Controls.Add(btnAdd);

                currentIndexOf++;
                currentHeight += lblItemName.Size.Height + defaultSkipHeight;
            }

            pnl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            pnl.Update();
            return pnl;
        }

        private void btnMenuItemsAdd_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            List<Control> a = this.pnlMenuSelectContainer.Controls.Find("tbxAmmount" + btn.Name.Remove(0, btn.Name.Length-1) , true).ToList<Control>();
            foreach (Control tb in a)
            {
                int i = 0;
                int.TryParse(tb.Text, out i);
                tb.Text = (i+=1).ToString();
            }
        }
        private void btnMenuItemsRemove_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            
            List<Control> a = this.pnlMenuSelectContainer.Controls.Find("tbxAmmount" + btn.Name.Remove(0, btn.Name.Length - 1), true).ToList<Control>();
            foreach (Control tb in a)
            {
                int i = 0;
                int.TryParse(tb.Text, out i);
                if(i>0) tb.Text = (i-=1).ToString();
            }
        }
    }
}
