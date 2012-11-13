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
            Byte[] hashedPassword = MD5Encryption.CreateMD5Hash(password);
            string passwordResult = String.Empty;

            foreach (byte b in hashedPassword)
            {
                passwordResult += b.ToString();
            }

            //-----------
            Byte[] hashedTempPassword = MD5Encryption.CreateMD5Hash("");
            string tempPassword = String.Empty;
            
            foreach (byte b in hashedTempPassword)
            {
                tempPassword += b.ToString();
            }
            //-----------

            if (passwordResult == tempPassword)
                return true;
            else
                return false;
        }
    }
}
