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
            this.InitializeComponent();
            this.attempts = 0;
            this.llblAdminLogin.Enabled = false;
            this.lblLoginStatus.ForeColor = System.Drawing.Color.Red;
            this.CheckForAdminControl();
            //this.ActiveControl = this.tbxPassword; // @Ryan: I've set it to focus on load, so users can type immediately
            this.tbxPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckEnter);
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
            string employeeType = string.Empty;

            if (!CheckIfBlocked(machineName, out blockedUntil, out deblock))
            {
                this.attempts += 1;
                if (deblock)
                    this.attempts = 0;

                if (this.attempts == 3)
                {
                    new Database().Insert(Database.TableName.AccessDenied, new string[] { "'" + machineName + "'", "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "'" + DateTime.Now.AddMinutes(15).ToString("yyyy-MM-dd HH:mm:ss") + "'" });
                    lblLoginStatus.Text = ("Login Blocked.");
                }
                else
                {
                    if (CanLogin(tbxUsername.Text, tbxPassword.Text, out employeeType) && tbxUsername.Text != "Admin")
                    {
                        if (lblLoginStatus.Text.Contains("Account Locked"))
                        {
                            Application.OpenForms["FormMain"].Show();
                            this.Hide();
                        }
                        else
                        {
                            FormMain frmMain = new FormMain(employeeType, this);
                            frmMain.Show();
                            this.Hide();
                            this.attempts = 0;
                        }
                    }
                    if (attempts > 0)
                        lblLoginStatus.Text = String.Format("Login attempt {0} of 3.", attempts);
                }
            }
            else
            {
                MessageBox.Show(string.Format("Blocked {0}m:{1}s", ((TimeSpan)(blockedUntil - DateTime.Now)).Minutes, ((TimeSpan)(DateTime.Now - blockedUntil)).Seconds.ToString().Remove(0, 1)));
            }
        }


        private void CheckForAdminControl()
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
                }
            }
            catch { return; }
        }

        private bool CanLogin(string name, string password, out string employeeType)
        {
            if (Login.CanLogin(name, password, out employeeType)) return true;
            return false;
        }

        private void llblAdminLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string employeeType = String.Empty;
            if (CanLogin("Admin", tbxPassword.Text, out employeeType))
            {
                FormAdminControls frmAdminControls = new FormAdminControls(this);
                frmAdminControls.CreateControl();
                frmAdminControls.Show();
                this.Hide();
            }
            else
                lblLoginStatus.Text = "Login Failed.";
        }

        private void CheckEnter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.btnLogin_Click(sender, e);
            }
            else
            {
                return;
            }
        }
    }
}
