using System;
using System.Windows.Forms;

namespace BonTemps
{
    public partial class FormSplashScreen : Form
    {
        public FormSplashScreen()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (CanLogin(comboBoxOccupation.Text, tbxPassword.Text))
            {
                formMain frmMain = new formMain();
                frmMain.CreateControl();
                frmMain.Show();
                this.Hide();
            }
        }

        public bool CanLogin(string name, string password)
        {
            if (Login.CanLogin(name, password)) return true;
            return false;
        }
    }
}
