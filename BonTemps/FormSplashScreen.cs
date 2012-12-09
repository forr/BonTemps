using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace BonTemps
{
    public partial class FormSplashScreen : Form
    {
        private uint attempts;
        private enum Occupation { Manager, Chef, Ober, Receptionist };

        public FormSplashScreen()
        {
            InitializeComponent();
            attempts = 0;
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

        public static bool CheckIfBlocked(string uid, out DateTime? dt, out bool resetAttemptCounter)
        {
            List<AccessDenied> adList = new Database().GetAllAccessDenied();
            resetAttemptCounter = false;
            foreach (AccessDenied ad in adList)
            {
                if (ad.MachineID == uid)
                {
                    dt = ad.BlockedUntil;
                    if (dt > DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc))
                    {
                        resetAttemptCounter = false;
                        return true;
                    }
                    new Database().Delete(Database.TableName.AccessDenied, (int)ad.BlockedID);
                    resetAttemptCounter = true;
                }
            }
            dt = new DateTime();
            return false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string machineName = System.Environment.MachineName;
            DateTime? blockedUntil = null;
            bool deblock = false;

            if (!CheckIfBlocked(machineName, out blockedUntil, out deblock))
            {
                this.attempts += 1;
                if (deblock)
                    this.attempts = 0;

                if (this.attempts == 3)
                {
                    new Database().Insert(Database.TableName.AccessDenied, new string[] { "'" + machineName + "'", "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "'" + DateTime.Now.AddMinutes(15).ToString("yyyy-MM-dd HH:mm:ss") + "'" });
                    lblLoginStatus.Text=("Login Blocked.");
                }
                else
                {
                    if (CanLogin(comboBoxOccupation.Text, tbxPassword.Text))
                    {
                        FormMain frmMain = new FormMain(comboBoxOccupation.Text);
                        frmMain.Show();
                        this.Hide();
                    }
                    lblLoginStatus.Text = String.Format("Login attempt {0} of 3.", attempts);
                }
            }
            else
            {

                MessageBox.Show(string.Format("Blocked {0}m:{1}s", ((TimeSpan)(blockedUntil - DateTime.Now)).Minutes, ((TimeSpan)(DateTime.Now - blockedUntil)).Seconds.ToString().Remove(0,1)));
            }
        }

        private void FillOccupationCombobox()
        {
            try
            {
                List<User> userList = new Database().GetAllUsers();
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
                FormMain frmMain = new FormMain("Admin");
                frmMain.CreateControl();
                frmMain.Show();
                this.Hide();
            }
            lblLoginStatus.Text = "Login Failed.";
        }
    }
}
