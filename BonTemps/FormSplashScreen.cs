using System;
using System.Windows.Forms;

namespace BonTemps
{
    public partial class FormSplashScreen : Form
    {
        private Database db = new Database();

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
                        FormManager frmManager = new FormManager();
                        frmManager.Show();
                        break;
                    case "Chef":
                        FormChef frmChef = new FormChef();
                        frmChef.Show();
                        break;
                    default:
                        FormMain frmMain = new FormMain();
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
                User[] userList = db.GetAllUsers();
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
            catch { return; }
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
                FormMain frmMain = new FormMain();
                frmMain.CreateControl();
                frmMain.Show();
                this.Hide();
            }
            lblLoginStatus.Text = "Login Failed.";
        }
    }
}
