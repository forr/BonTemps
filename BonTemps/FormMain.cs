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
        private int tableSize = 0;
        private int tableMultiplier = 0;
        private List<Client> clients;
        private List<TableLayout> tables;

        public FormMain(string initialValue)
        {
            this.initialUser = initialValue;
            this.InitializeComponent();
            this.InitializeFormProperties();
            this.InitializeRules();
        }

        private void InitializeFormProperties()
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        private void InitializeRules()
        {
            if (this.initialUser == "Admin")
            {
                //Admin POOL
                //Flushing Data if needed
                //Adding additional value changes to a Database when a critical error arises.
                return; // @Ryan, added return for now... -Alain
            }
            else
            {
                this.IniTabData(); //Removes all Unrelated Tabpages for the current user.
                this.IniOrderView(); //Displays all current orders and is allowed to be seen by everyone.
                
                foreach (Table t in new Database().GetAllTables())
                {
                    this.comboBox1.Items.Add(t.TableNumber);
                    try
                    {
                        this.comboBox1.SelectedIndex = 1;
                    }
                    catch
                    {
                        return;
                    }
                }

                switch (initialUser)
                {
                    // no tables
                    case "Manager":
                        this.IniManager(); //Manager's Initial methods
                        break;
                    case "Chef":
                        this.IniChef(); //Chef's Initial methods
                        break;
                    case "Ober":
                        this.IniWaiter(); //Ober's Initial methods
                        break;
                    case "Receptionist":
                        this.IniReceptionist(); //Receptionist's Initial methods
                        break;
                }                
            }
        }

        #region INI's
        private void IniManager()
        {
            // @Ryan: not sure whether you mean the view initialization or the manager ini... e.g. like you did with IniOrderView & IniChef/Waiter.. 
            // Made IniManagerView() instead... see below
            
        }

        private void IniManagerView()
        {

        }

        private void IniChef()
        {
        }

        private void IniWaiter()
        {
            for (int i = 0; i < this.DisplayMenuItems().Controls.Count; i++)
            {
                this.pnlMenuSelectContainer.Controls.Add(this.DisplayMenuItems().Controls[i]);
            }
        }

        private void IniReceptionist()
        {
            this.tableSize = this.GetTableWidth();
            this.FillLbxClientList(); //Load a Full List of Clients - might get to be called obsolete when integrated.
            this.pnlOverview.AutoScroll = true; //
            this.clients = new Database().GetAllClients().ToList<Client>();
            this.tables = new List<TableLayout>();
            
            for (int i = 0; i <= new Database().GetAllTables().Count(); i++)
            {
                tables.Add(new TableLayout(global::BonTemps.Properties.Resources.table, i, String.Empty, TableStatus.Empty));
            }
            
            this.ShowTables();

            for (int i = 0; i < this.DisplayMenuItems().Controls.Count; i++)
            {
                this.pnlMenuSelectContainer.Controls.Add(this.DisplayMenuItems().Controls[i]);
            }

            //////if (tpNewOrder.Tag.ToString().Contains(initialUser))
            //////{
            //////    this.fillLbxClientList();
            //////}
        }
        #endregion INI's

        private void IniOrderView()
        {
            string[] array = new string[] { "Item 1", "Item 2", "Item 3" };
            var items = this.lvOrders.Items;
            foreach (var val in array)
                items.Add(val);
            this.lvOrders.Bounds = new Rectangle(0, 0, this.lvOrders.ClientSize.Width, this.lvOrders.ClientSize.Height);
            this.lvOrders.View = View.Details;
            this.lvOrders.LabelEdit = false;
            this.lvOrders.CheckBoxes = true;
            this.lvOrders.FullRowSelect = (initialUser == "Chef") ? true : false;
            this.lvOrders.GridLines = true;

            List<Order> orderList = new Database().GetAllOrders();
            List<Client> clientList = new Database().GetAllClients();
            List<ListViewItem> lviList = new List<ListViewItem>();
            foreach (TableOrder tOrder in new Database().GetAllTableOrders())
            {
                ListViewItem itemx = new ListViewItem(string.Format("Table {0}", tOrder.TableID), 0);
                foreach (Order o in orderList)
                {
                    if (o.OrderID == tOrder.OrderID)
                    {
                        List<string> tempMenuSelection = new List<string>();
                        foreach (UInt64 ui in new Database().GetMenuIDs())
                        {
                            tempMenuSelection.Add(Convert.ToString(ui));
                        }
                        string menuSelection = String.Empty;
                        int indexMenuSelection = 0;
                        foreach (string s in tempMenuSelection)
                        {
                            //string result = new Database().GetMenu(Convert.ToUInt64(s)).ToString();
                            menuSelection += ((((indexMenuSelection % 4) == 0) && (indexMenuSelection != 0)) ? "\n" : "") + "Menu nr:";
                            if ((tempMenuSelection.Count - 1) == indexMenuSelection)
                                menuSelection += s;
                            else
                                menuSelection += s + ", ";
                            indexMenuSelection++;
                        }
                        itemx.SubItems.Add(menuSelection);
                    }
                    lviList.Add(itemx);
                }
            }

            lvOrders.Columns.Add("Table Number", this.lvOrders.ClientSize.Width / 3, HorizontalAlignment.Left);
            lvOrders.Columns.Add("Ordered Menus", this.lvOrders.ClientSize.Width / 2, HorizontalAlignment.Left);
            lvOrders.Items.AddRange(lviList.ToArray<ListViewItem>());
        }

        private void IniTabData()
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

        private void FillLbxClientList()
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

            return new Point(Convert.ToInt32((relevantWidth / 100) * currentPanelSize), Convert.ToInt32((relevantHeight / 100) * currentPanelSize));
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

            return ((screen_width / this.tableMultiplier) - 4);
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
                this.FillLbxClientList();
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
            ulong clientid = Convert.ToUInt64(tbxClientID_pnlOrder.Text);
            ulong tableid = Convert.ToUInt64(tbxTableID_pnlOrder.Text);
            ulong? orderid = null;

            string orders = lbxSelectedMenuItems.Text.Replace("\n", "");
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

        private void btnSetOrder_Click(object sender, EventArgs e)
        {
            if (initialUser == "Receptionist")
            {
                this.tctrlInterface.SelectedTab = this.tpNewOrder;
                this.lbxSelectedMenuItems.Items.Clear();
            }

            List<String> OrderSelection = new List<String>();
            List<Control> lblControls = new List<Control>();
            foreach (Control c in this.pnlMenuSelectContainer.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    if (c.Text != "0")
                    {
                        int i;
                        int.TryParse(c.Text, out i);
                        while (i > 0)
                        {
                            lblControls.Add(this.pnlMenuSelectContainer.Controls.Find(String.Format("lblItemID{0}", c.Name.Remove(0, c.Name.Length - 1)), true)[0]);
                            i--;
                        }
                    }
                }
            }
            foreach (Control c in lblControls)
            {
                if (lblControls[lblControls.Count() - 1] == c)
                {
                    this.lbxSelectedMenuItems.Items.Add(c.Text);
                }
                else
                {
                    this.lbxSelectedMenuItems.Items.Add(c.Text + ",");
                }
            }
        }

        private void btnNewClient_Click(object sender, EventArgs e)
        {
            FormNewClient frmNewClient = new FormNewClient();
            frmNewClient.CreateControl();
            frmNewClient.Show();
        }

        private void lblEditMenus_Layout(object sender, LayoutEventArgs e)
        {

        }
        private void CenterLabelsInPanels(object sender)
        {
            if (this.IsHandleCreated == true)
            {
                Control c = sender as Control;

                if (c.Parent != null)
                {
                    int labelsize = c.Width;
                    Control container = c.Parent;
                    int containersize = container.Width;
                    c.Left = (containersize - labelsize) / 2;
                }
            }
        }
        private int dothis()
        {
            return 2++;
        }
        private void splitContainer1_Panel1_SizeChanged(object sender, EventArgs e)
        {
            this.CenterControls(splitContainer1.Panel1);
        }

        private void CenterControls(Control p)
        {
            foreach (Control c in p.Controls)
            {
                if (c.Parent != null)
                {
                    int labelsize = c.Width;
                    Control container = c.Parent;
                    int containersize = container.Width;
                    c.Width = containersize - 10;
                    c.Left = (containersize - labelsize) / 2;
                }

            }
        }
    }
}
