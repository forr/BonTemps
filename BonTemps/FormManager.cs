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
    public partial class formManager : Form
    {
        public formManager()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
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
