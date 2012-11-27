using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BonTemps
{
    public partial class formManager : Form
    {
        private enum TypeOfExport { Clients }

        public formManager()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.TopMost = true;

            this.pnlOrder.Controls.AddRange(ManagerView.ObjectControlArray(ManagerView.MenuPanel(), ManagerView.ObjectType.Panel));
            this.pnlOrder.Update();
            this.pnlOrder.BorderStyle = BorderStyle.Fixed3D;
            this.pnlOrder.Width = ((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 10) * 7);
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
            List<String> csvClients = new List<String>();
            foreach (Client c in Database.GetAllClients())
            {
                csvClients.Add(c.ToString());
            }
            string[] csvData = CSVFile.CreateCsvString(csvClients);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Comma-separated Values (*.csv)|*.csv";
            sfd.DefaultExt = "csv";
            sfd.AddExtension = true;
            sfd.FileName = "ClientListExport_" + DateTime.Now.ToShortDateString().ToString().Replace("/", "-") + "_" + DateTime.Now.TimeOfDay.ToString().Replace(".", ",").Replace(":", ".");
            if(sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    File.WriteAllLines(sfd.FileName, csvData);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        

        #endregion MenuStrip
    }
}
