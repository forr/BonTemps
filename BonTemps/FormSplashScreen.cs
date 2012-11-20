using System;
using System.Windows.Forms;

namespace BonTemps
{
    public partial class FormSplashScreen : Form
    {
        public FormSplashScreen()
        {
            InitializeComponent();
            lblLoginStatus.ForeColor = System.Drawing.Color.Red;
            FillOccupationCombobox();
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
            lblLoginStatus.Text = "Login Failed.";
        }

        private void FillOccupationCombobox()
        {
            
            try
            {
                Users[] userList = Database.GetAllUsers();
                foreach (Users u in userList)
                {
                    comboBoxOccupation.Items.Add(u.Username);
                    comboBoxOccupation.SelectedIndex = 0;
                }
            }
            catch{}
        }

        private bool CanLogin(string name, string password)
        {
            if (Login.CanLogin(name, password)) return true;
            return false;
        }
    }
}
