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
        private Form parentForm;
        private string initialUser;
        private int tableSize = 0;
        private int tableMultiplier;
        private List<Client> clients;
        private List<TableLayout> tables;

        public FormMain(string initialValue, Form parentForm)
        {
            this.initialUser = initialValue;
            this.parentForm = parentForm;
            this.InitializeComponent();
            this.InitializeFormProperties();
            this.InitializeRules();
        }

        private void InitializeTabManagement()
        {
            this.cbxType.SelectedIndex = 0;
            this.cbxType.DropDownStyle = ComboBoxStyle.DropDownList;
            //this.dgvEditTables.DataSource = new Database().GetAllMenus();
            //int x = this.dgvEditTables.Location.X;
            //int y = this.dgvEditTables.Location.Y + this.dgvEditTables.ClientSize.Height + 20;
            //string[] lblText = new string[] { "Menu ID", "Entree", "Main Course", "Dessert", "Price" };
            //for (int i = 0; i < 6; i++)
            //{
            //    TextBox txtbx = new TextBox();
            //    Label lbl = new Label();
            //    txtbx.Name = String.Format("txtbxEditItem{0}", i);
            //    lbl.Name = String.Format("lblEditItem{0}", i);

            //    if (i != 0)
            //    {
            //        Control[] ctrlLbl = this.Controls.Find("lblEditItem" + (i - 1).ToString(), true);
            //        MessageBox.Show(ctrlLbl.Length.ToString());
            //        ctrlLbl[0].Text = lblText[i - 1];

            //        Control[] ctrlTbx = this.Controls.Find("txtbxEditItem" + (i - 1).ToString(), true);

            //        if ((i + 1) % 2 == 0)
            //        {
            //            x += (this.dgvEditTables.ClientSize.Width / 2) + 4;
            //            y = ctrlTbx[0].Location.Y;
            //        }
            //        else
            //            y = (ctrlTbx[0].Location.Y + ctrlTbx[0].ClientSize.Height + 8);
            //    }
            //    lbl.Location = new Point(x, y);
            //    lbl.AutoSize = true;
            //    txtbx.Location = new Point(x + 2 + lbl.ClientSize.Width, y);

            //    if (i != 5)
            //    {
            //        this.tpManagement.Controls.Add(txtbx);
            //        this.tpManagement.Controls.Add(lbl);
            //        x = this.dgvEditTables.Location.X;
            //    }
            //}
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
            this.InitializeTabManagement();
            
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
            this.IniTimeData();
            this.FillLbxClientList(); //Load a Full List of Clients - might get to be called obsolete when integrated.
            this.clients = new Database().GetAllClients().ToList<Client>();
            this.tables = new List<TableLayout>();
            this.CreateTableLayoutData();

            for (int i = 0; i < this.DisplayMenuItems().Controls.Count; i++)
            {
                this.pnlMenuSelectContainer.Controls.Add(this.DisplayMenuItems().Controls[i]);
            }

            //////if (tpNewOrder.Tag.ToString().Contains(initialUser))
            //////{
            //////    this.fillLbxClientList();
            //////}
        }

        private void CreateTableLayoutData()
        {
            tables.Clear();
            List<TableOrder> tbo = new Database().GetAllTableOrders();
            List<Table> tbs = new Database().GetAllTables();

            for (int i = 0; i < tbs.Count; i++)
            {
                TableStatus tableStatus = TableStatus.Empty;
                string clientID = String.Empty;

                for (int j = 0; j < tbo.Count; j++)
                {
                    if (tbo[j].TableID == tbs[i].TableID)
                    {
                        Order o = new Database().GetOrder((ulong)tbo[j].OrderID);
                        if (o.Payed == true)
                            tableStatus = TableStatus.Empty;
                        else
                        {
                            if (o.StartDateTime > DateTime.Now)
                                tableStatus = TableStatus.NotOnTime;
                            else
                            {
                                if (o.Seated == true)
                                    tableStatus = TableStatus.OnTime;
                                else
                                    tableStatus = TableStatus.Ordered;
                            }
                        }

                        clientID = new Database().GetOrder((ulong)tbo[j].OrderID).ClientID.ToString();
                    }
                }

                tables.Add(new TableLayout((int)tbs[i].TableID, clientID, tableStatus));
            }

            this.ShowTables();
        }

        #endregion INI's

        private void IniTimeData()
        {
            int minuteIndex = 0;
            int currentMinute = DateTime.Now.Minute;
            cbxOrderHour.SelectedIndex = DateTime.Now.Hour;

            if (currentMinute < 55) // if its higher then 55 minutes it resets to index 0 which it is by default
            {
                for (int i = 0; i < currentMinute; i++)
                {
                    if (i % 5 == 0)
                    {
                        minuteIndex++;
                        currentMinute--;
                    }
                }
                if (currentMinute > 0)
                    if (currentMinute > 2 && currentMinute != 5)
                        minuteIndex += 2;
                    else
                        minuteIndex++;
            }

            cbxOrderMinute.SelectedIndex = minuteIndex;
        }

        private void IniOrderView()
        {
            tbxClientID_tpClientSelect.Text = "";
            tbxClientName_tpClientSelect.Text = "";

            btnEditClient_ClientSelect.Enabled = false;

            //Test Example for filling the lvOrders Listview:
            //
            //string[] array = new string[] { "Item 1", "Item 2", "Item 3" };
            //var items = this.lvOrders.Items;
            //foreach (var val in array)
            //    items.Add(val);

            this.lvOrders.Bounds = new Rectangle(0, 0, this.lvOrders.ClientSize.Width, this.lvOrders.ClientSize.Height);
            this.lvOrders.View = View.Details;
            this.lvOrders.LabelEdit = false;
            this.lvOrders.CheckBoxes = true;
            this.lvOrders.FullRowSelect = (initialUser == "Chef") ? true : false;
            this.lvOrders.GridLines = true;


            //DataTable orderList = new Database().GetAllCurrentOrders();
            List<ListViewItem> lviList = new List<ListViewItem>();
            ListViewItem itemx = null;
            foreach (DataRow dr in new Database().GetAllCurrentOrders().Rows)
            {
                itemx = new ListViewItem(string.Format("Order nr.{0}", dr["OrderID"] ), 0);
                itemx.Checked = (bool)dr["OrderReady"];

                itemx.SubItems.Add(dr["TableID"].ToString());
                List<string> tempMenuSelection = new List<string>();
                foreach (UInt64 ui in new Database().GetMenuIDs())
                {
                    tempMenuSelection.Add(Convert.ToString(ui));
                }
                string menuSelection = String.Empty;
                int indexMenuSelection = 0;
                foreach (string s in tempMenuSelection)
                {
                    menuSelection += ((((indexMenuSelection % 4) == 0) && (indexMenuSelection != 0)) ? ";" : "") + "Menu nr:";
                    if ((tempMenuSelection.Count - 1) == indexMenuSelection)
                        menuSelection += s;
                    else
                        menuSelection += s + ", ";
                    indexMenuSelection++;
                }
                itemx.SubItems.Add(menuSelection);
            }
            lviList.Add(itemx);

            lvOrders.Columns.Add("Order Number", this.lvOrders.ClientSize.Width / 3, HorizontalAlignment.Left);
            lvOrders.Columns.Add("Table Number", this.lvOrders.ClientSize.Width / 2, HorizontalAlignment.Left);
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
            int screen_width = (this.pnlOverview.Width) - 11;

            this.tableMultiplier = 10;

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
                pnlTable.BackColor = System.Drawing.Color.Blue;
                pnlTable.Name = TableLayout.GetTableName("pnlTable", this.tables[i - 1].tableID);
                pnlTable.BorderStyle = BorderStyle.FixedSingle;
                pnlTable.Click += new EventHandler(this.pnlTable_Click);

                Label lblTableStatus = new Label();
                lblTableStatus.Text = TableLayout.GetTableName("pnlTable", (i - 1));
                lblTableStatus.AutoSize = true;
                lblTableStatus.Location = new Point(pos_x + (this.tableSize / 2) - 24, pos_y);
                lblTableStatus.TextAlign = ContentAlignment.MiddleRight;
                lblTableStatus.ForeColor = System.Drawing.Color.White;
                lblTableStatus.Font = new Font(SystemFonts.CaptionFont.FontFamily, SystemFonts.CaptionFont.Size, FontStyle.Bold);
                //lblTableStatus.BackColor = Color.White;

                Label lblClientID = new Label();
                Point pLblClientID = this.GetClientIDLocation();
                lblClientID.Location = new Point(((pos_x + pLblClientID.X) - (lblClientID.Width / 2)), (pos_y + pLblClientID.Y));
                lblClientID.TextAlign = ContentAlignment.MiddleCenter;
                lblClientID.ForeColor = System.Drawing.Color.Black;
                lblClientID.BackColor = System.Drawing.Color.White;
                lblClientID.Text = this.tables[(i - 1)].clientID;
                lblClientID.AutoSize = true;

                switch (this.tables[(i - 1)].tableStatus)
                {
                    case TableStatus.Empty:
                        pnlTable.BackColor = System.Drawing.Color.Blue;
                        lblTableStatus.BackColor = System.Drawing.Color.Blue;
                        break;
                    case TableStatus.NotOnTime:
                        pnlTable.BackColor = System.Drawing.Color.FromArgb(255, 99, 25);
                        lblTableStatus.BackColor = System.Drawing.Color.FromArgb(255, 99, 25);
                        lblClientID.ForeColor = System.Drawing.Color.White;
                        lblClientID.BackColor = System.Drawing.Color.Red;
                        break;
                    case TableStatus.OnTime:
                        pnlTable.BackColor = System.Drawing.Color.Green;
                        lblTableStatus.BackColor = System.Drawing.Color.Green;
                        break;
                    case TableStatus.Ordered:
                        pnlTable.BackColor = System.Drawing.Color.DarkRed;
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
            if (tbxAmountOfPersons_tpOrderCreation.Text != String.Empty)
            {
                Panel pnlSender = (Panel)sender;
                int currentTableID = 0;
                currentTableID = TableLayout.GetTableID(pnlSender.Name);

                Table tb = new Database().GetTable(Convert.ToUInt64(currentTableID));
                try
                {
                    if (this.clients[Convert.ToInt32(this.tables[currentTableID].clientID)].FirstName == String.Empty)
                    {
                        MessageBox.Show("Table Already in use", "In use", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        return;
                    }
                }
                catch (Exception ex) { }

                if (tbxTableID_tpOrderCreation.Text.Contains(" " + currentTableID.ToString() + ",") ||
                    tbxTableID_tpOrderCreation.Text.Contains(", " + currentTableID.ToString()) ||
                    (tbxTableID_tpOrderCreation.Text.Contains(currentTableID.ToString()) && (tbxTableID_tpOrderCreation.Text.Length == currentTableID.ToString().Length)) && !tbxTableID_tpOrderCreation.Text.Contains(","))
                {
                    return;
                }

                this.tbxTableID_tpOrderCreation.Text += (this.tbxTableID_tpOrderCreation.Text == String.Empty) ?
                    currentTableID.ToString() : ", " + currentTableID.ToString().ToString();
            }
            else
            {
                MessageBox.Show("Add the amount of accompanies first.");
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

                this.tbxClientID_tpClientSelect.Text = FillInfo[0]; // ID
                this.tbxClientName_tpClientSelect.Text = FillInfo[1]; // FirstName + LastName
                this.tbxClientVisits_ClientSelect.Text = FillInfo[7];
            }
        }

        private void btnCreateNewOrder_Click(object sender, EventArgs e)
        {
            try
            {
                ulong clientid = Convert.ToUInt64(tbxClientID_tpOrderCreation.Text);
                int personsCount = 0;
                List<Table> tableCount = new List<Table>();
                int.TryParse(tbxAmountOfPersons_tpOrderCreation.Text, out personsCount);
                List<ulong> tableidList = new List<ulong>();
                string[] TableIds = this.tbxTableID_tpOrderCreation.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (String s in TableIds)
                {
                    tableidList.Add(Convert.ToUInt64(s));
                }

                foreach (ulong ul in tableidList)
                {
                    //personsCount -= new Database().GetAllTables().Find(new Predicate<Table>(new Table() { TableID = ul })).AmountOfChairs;
                    Table tb = new Table();
                    foreach(Table t in new Database().GetAllTables())
                    {
                        if (t.TableID == ul)
                        {
                            tb = t;
                            tableCount.Add(t);
                        }
                    }
                    personsCount -= (int)tb.AmountOfChairs; 
                }

                if (personsCount > 0)
                {
                    MessageBox.Show("Please select the table(s) first");
                    return;
                }

                ulong? orderid = null;

                //string orders = lbxSelectedMenuItems.Text.Replace("\n", "");
                List<String> orders = new List<String>();
                foreach (Object o in this.lbxSelectedMenuItems.Items)
                {
                    orders.Add(o.ToString().Replace(",", ""));
                }
                //lbxSelectedMenuItems.Items.CopyTo(orders.ToArray<String>(), 0);

                DateTime TimeToInject = dtpOrderDate.Value;
                TimeToInject = TimeToInject.AddHours(Convert.ToInt32(cbxOrderHour.SelectedItem) - TimeToInject.Hour);
                TimeToInject = TimeToInject.AddMinutes(Convert.ToInt32(cbxOrderMinute.SelectedItem) - TimeToInject.Minute);

                new Database().Insert(Database.TableName.Orders, new String[] { this.tbxClientID_tpOrderCreation.Text, TimeToInject.ToString("yyyy-MM-dd HH:mm:ss"), TimeToInject.AddMinutes(30).ToString("yyyy-MM-dd HH:mm:ss") });  //Injects Succesfull
                orderid = (ulong)new Database().GetOrderID((ulong)int.Parse(this.tbxClientID_tpOrderCreation.Text), TimeToInject.ToString("yyyy-MM-dd HH:mm:ss"), TimeToInject.AddMinutes(30).ToString("yyyy-MM-dd HH:mm:ss"));
                for(int i = 0; i < int.Parse(tbxAmountOfPersons_tpOrderCreation.Text); i++)
                {
                    int tableindex;
                    int tablenr = GetTableNumber(tableCount, out tableindex);
                    if (orders.Count > i)
                        new Database().Insert(Database.TableName.Persons, new String[] { orders[i].ToString(), orderid.ToString(), tablenr.ToString() });
                    else
                        new Database().Insert(Database.TableName.Persons, new String[] { "NULL", orderid.ToString(), tablenr.ToString() });
                    int decreasedAmountOfChairs = (Convert.ToInt32(tableCount[tableindex].AmountOfChairs) - 1);
                    int decreasedID = (int)tableCount[tableindex].TableID;
                    int decreasedTableNR = (int)tableCount[tableindex].TableNumber;
                    List<Table> TempTableList = new List<Table>();
                    foreach (Table t in tableCount)
                    {
                        if ((int)t.TableID == tablenr)
                            TempTableList.Add(new Table((ulong)decreasedID, (uint)decreasedTableNR, (uint)decreasedAmountOfChairs));
                        else
                            TempTableList.Add(t);
                    }
                    tableCount.Clear();
                    tableCount.AddRange(TempTableList);
                }
                foreach (Table tb in tableCount)
                {
                    new Database().Insert(Database.TableName.TableOrders, new String[] { tb.TableID.ToString(), orderid.ToString(), false.ToString() });
                }
                new Database().UpdateClientVisit(ulong.Parse(this.tbxClientID_tpOrderCreation.Text));
                this.pnlOverview.Controls.Clear();
                this.pnlOverview.Update();
                this.CreateTableLayoutData();


                this.tpClientSelect.Enabled = true;
                this.tpOrderCreation.Enabled = false;
                this.tpMenuSelection.Enabled = false;
                this.tpTableSelection.Enabled = false;

                this.tctrlCreateOrder.SelectedTab = this.tpClientSelect;

                MessageBox.Show("Order Succesful,\n"+
                                "Mr./Ms. " + new Database().GetClient(clientid).LastName + "\n" +
                                "Can seat around " + new Database().GetOrder((ulong)orderid).StartDateTime.Value.TimeOfDay.ToString(), "Order Succesfull", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1 );
            }
            catch ( Exception ex )
            {
                MessageBox.Show("Please fill the records with valid data.");
                return;
            }
        }

        private int GetTableNumber(List<Table> tb, out int CurrentIndex)
        {
            for (int i = 0; i < tb.Count; i++)
            {
                if (tb[i].AmountOfChairs > 0)
                {
                    CurrentIndex = i;
                    return (int)tb[i].TableID;
                }
            }
            CurrentIndex = -1;
            return -1;
        }

        private void btnSelectMenuItems_Click(object sender, EventArgs e)
        {
            this.tpMenuSelection.Enabled = true;
            this.tctrlCreateOrder.SelectedTab = this.tpMenuSelection;
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
                this.tctrlCreateOrder.SelectedTab = this.tpTableSelection;
                this.tpTableSelection.Enabled = true;
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
            FormClientNav frmNewClient = new FormClientNav(this);
            frmNewClient.CreateControl();
            frmNewClient.Show();
        }

        public void FormNewClient_CloseOnAdd()
        {
            this.lbxClientList.Items.Clear();
            this.FillLbxClientList();
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
            return 0;
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

        private void btnClearTableIDs_pnlOrder_Click(object sender, EventArgs e)
        {
            this.tbxTableID_tpOrderCreation.Text = String.Empty;
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.parentForm.Show();

            Control[] cs = this.parentForm.Controls.Find("tbxPassword", true);
            cs[0].Text = String.Empty;

            this.Close();
        }

        private void lockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.parentForm.Show();

            Control[] cs = this.parentForm.Controls.Find("lblLoginStatus", true);
            cs[0].Text = "Account Locked";

            Control[] cs2 = this.parentForm.Controls.Find("tbxUsername", true);
            cs2[0].Enabled = false;

            Control[] cs3 = this.parentForm.Controls.Find("tbxPassword", true);
            cs3[0].Text = String.Empty;

            this.Hide();
        }

        private void btnEditClient_Click(object sender, EventArgs e)
        {
            if (lbxClientList.SelectedItem != null)
            {
                FormClientNav frmClientNav = new FormClientNav(this, true, int.Parse(this.tbxClientID_tpClientSelect.Text));
                frmClientNav.Show();
            }
        }

        private void btnClientSelect_OK_Click(object sender, EventArgs e)
        {
            this.tbxClientID_tpOrderCreation.Text = tbxClientID_tpClientSelect.Text;
            this.tbxClientName_tpOrderCreation.Text = tbxClientName_tpClientSelect.Text;

            this.tbxClientID_tpClientSelect.Text = "";
            this.tbxClientName_tpClientSelect.Text = "";

            this.btnEditClient_ClientSelect.Enabled = false;

            this.tpOrderCreation.Enabled = true;
            this.tctrlCreateOrder.SelectedTab = this.tpOrderCreation;
        }

        private void btnClientSelect_Cancel_Click(object sender, EventArgs e)
        {
            this.tbxClientID_tpClientSelect.Text = "";
            this.tbxClientName_tpClientSelect.Text = "";

            this.btnEditClient_ClientSelect.Enabled = false;
        }

        private void tbxClientID_tpClientSelect_TextChanged(object sender, EventArgs e)
        {
            if (tbxClientID_tpClientSelect.Text != String.Empty)
            {
                this.btnEditClient_ClientSelect.Enabled = true;
            }
            else
            {
                this.btnEditClient_ClientSelect.Enabled = false;
            }
        }

        private void tpNewOrder_Enter(object sender, EventArgs e)
        {
            this.tpClientSelect.Enabled = true;
            this.tpOrderCreation.Enabled = false;
            this.tpMenuSelection.Enabled = false;
            this.tpTableSelection.Enabled = false;
        }

        private void btnDeleteClient_ClientSelect_Click(object sender, EventArgs e)
        {
            if (int.Parse(this.tbxClientVisits_ClientSelect.Text) == 0)
            {
                if (new Database().Delete(Database.TableName.Clients, int.Parse(this.tbxClientID_tpClientSelect.Text)))
                {
                    MessageBox.Show("Succesfully deleted the current user.");
                    this.FillLbxClientList();
                    this.btnClientSelect_Cancel_Click(sender, e);
                }
                else
                    MessageBox.Show("Failed to Delete current client.");
            }
            else
                MessageBox.Show("You may only delete those that aren't listed (A client with 0 visits).", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //if (this.rbEditMenus.Checked)
            //    if (this.ProcessInfoMenu())
            //        MessageBox.Show("Update of menu information successful.");
            //    else
            //        MessageBox.Show("Update of menu information failed.");
            //else
            //    if (this.ProcessInfoUser())
            //        MessageBox.Show("Update of user information successful.");
            //    else
            //        MessageBox.Show("Update of user information failed.");
        }

        private void cbxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxType.SelectedText == "Menus")
            {
                this.DisableUserControls();
                this.EnableMenuControls();
            }
            else
            {
                this.DisableMenuControls();
                this.EnableUserControls();
            }
        }

        private void EnableMenuControls()
        {

        }

        private void DisableMenuControls()
        {

        }

        private void EnableUserControls()
        {

        }

        private void DisableUserControls()
        {

        }

        //private bool ProcessInfoMenu()
        //{
        //    return true;
        //}

        //private bool ProcessInfoUser()
        //{
        //    return true;
        //    //string[] lblText = new string[] { "User ID", "Username", "Employee Type", "Password" };

        //    //for (int i = 0; i < 6; i++)
        //    //{
        //    //    if (i != 0)
        //    //    {
        //    //        Control[] ctrlLblOld = this.Controls.Find("lblEditItem" + (i - 1).ToString(), true);
        //    //        Control[] ctrlTbxOld = this.Controls.Find("txtbxEditItem" + (i - 1).ToString(), true);
        //    //        ctrlLblOld[0].Text = null;
        //    //        //this.Controls.Remove(ctrlTbxOld[4]);         
        //    //    }
        //    //}
        //    //for (int j = 0; j < 4; j++)
        //    //{
        //    //    if (j != 0)
        //    //    {
        //    //        Control[] ctrlLbl = this.Controls.Find("lblEditItem" + (j - 1).ToString(), true);
        //    //        ctrlLbl[0].Text = lblText[j - 1];
        //    //    }
        //    //}

        //    //return true;
        //}
    }
}
