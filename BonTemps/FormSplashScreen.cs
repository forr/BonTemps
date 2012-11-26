using System;
using System.Windows.Forms;

namespace BonTemps
{
    public partial class FormSplashScreen : Form
    {
        public FormSplashScreen()
        {
            InitializeComponent();
            llblAdminLogin.Enabled = false;
            lblLoginStatus.ForeColor = System.Drawing.Color.Red;
            FillOccupationCombobox();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormShape()
        {
            this.Region = new System.Drawing.Region();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (CanLogin(comboBoxOccupation.Text, tbxPassword.Text))
            {
                switch (comboBoxOccupation.Text)
                {
                    case "Manager":
                        formManager frmManager = new formManager();
                        frmManager.CreateControl();
                        frmManager.Show();
                        break;
                    default:
                        formMain frmMain = new formMain();
                        frmMain.CreateControl();
                        frmMain.Show();
                        break;
                }
                this.Hide();
            }
            lblLoginStatus.Text = "Login Failed.";
        }

        private void FillOccupationCombobox()
        {
            
            try
            {
                User[] userList = Database.GetAllUsers();
                foreach (User u in userList)
                {
                    if (u.Username == "Admin")
                    {
                        llblAdminLogin.Enabled = true;
                        llblAdminLogin.Text = "Admin Login(enter password).";
                    }
                    else
                    {
                        comboBoxOccupation.Items.Add(u.Username);
                        comboBoxOccupation.SelectedIndex = 0;
                    }
                }
            }
            catch{}
        }

        private bool CanLogin(string name, string password)
        {
            if (Login.CanLogin(name, password)) return true;
            return false;
        }

        private void llblAdminLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (CanLogin("Admin", tbxPassword.Text))
            {
                formMain frmMain = new formMain();
                frmMain.CreateControl();
                frmMain.Show();
                this.Hide();
            }
            lblLoginStatus.Text = "Login Failed.";
        }
    }
}
