using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

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
            string passwordResult = MD5Encryption.MD5HashToString(MD5Encryption.CreateMD5Hash(password));

            if (Login.CanLogin(passwordResult))
                return true;
            else
                return false;
        }
    }
}
