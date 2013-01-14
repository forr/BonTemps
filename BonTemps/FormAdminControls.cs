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
    public partial class FormAdminControls : Form
    {
        private Form parentForm;
        public FormAdminControls(Form parentForm)
        {
            this.parentForm = parentForm;
            InitializeComponent();
            cbDatabases.Items.AddRange(new string[]{
                                                        "Clients Database",
                                                        "Menus Database",
                                                        "Orders Database",
                                                        "Persons Database",
                                                        "TableOrders Database",
                                                        "Tables Database",
                                                        "Users Database"
                                                   });
            cbDatabases.SelectedItem = "Users Database";
            FillDBResult(cbDatabases.SelectedItem.ToString());
        }

        public void FillDBResult(string sDB)
        {
            DataTable dt = new DataTable();
            switch (sDB)
            {
                //DATABASE HEEFT GEEN DATATABLE DATA!
                case "Clients Database":
                    dgvResult.DataSource = typeof(Client);
                    dgvResult.DataSource = new Database().GetAllClients();
                    dgvResult.ReadOnly = true;
                    break;
                case "Menus Database":
                    dgvResult.DataSource = typeof(Menu);
                    dgvResult.DataSource = new Database().GetAllMenus();
                    dgvResult.ReadOnly = true;
                    break;
                case "Orders Database":
                    dgvResult.DataSource = typeof(Order);
                    dgvResult.DataSource = new Database().GetAllOrders();
                    dgvResult.ReadOnly = true;
                    break;
                case "Persons Database":
                    dgvResult.DataSource = typeof(Person);
                    dgvResult.ReadOnly = true;
                    dgvResult.DataSource = new Database().GetAllPersons();
                    dgvResult.ReadOnly = true;
                    break;
                case "TableOrders Database":
                    dgvResult.DataSource = typeof(TableOrder);
                    dgvResult.DataSource = new Database().GetAllTableOrders();
                    dgvResult.ReadOnly = true;
                    break;
                case "Tables Database":
                    dgvResult.DataSource = typeof(Table);
                    dgvResult.DataSource = new Database().GetAllTables();
                    dgvResult.ReadOnly = true;
                    break;
                case "Users Database": // IS EDITABLE
                    dt.TableName = "Users";
                    dt.Columns.AddRange(new DataColumn[] { new DataColumn("UserID", typeof(ulong)), new DataColumn("EmployeeType", typeof(string)), new DataColumn("Password", typeof(string)) });
                    foreach (User u in new Database().GetAllUsers())
                    {
                        dt.Rows.Add(new object[] { u.UserID, u.Username, u.Password });
                    }
                    dgvResult.DataSource = dt;
                    dgvResult.ReadOnly = false;
                    break;
            }
            dgvResult.Update();
        }

        private void cbDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDBResult(cbDatabases.SelectedItem.ToString());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.parentForm.Show();
            this.Close();
        }

        private void bFlush_Click(object sender, EventArgs e)
        {

        }
    }
}
