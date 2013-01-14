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
    public partial class FormNewClient : Form
    {
        Form fmOrigin = new Form();
        public FormNewClient(Form fm)
        {
            InitializeComponent();
            fmOrigin = fm;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Client c = new Client(null,
                                      this.tbxFirstName.Text,
                                      this.tbxLastName.Text,
                                      this.tbxAddress.Text,
                                      this.tbxPostalCode.Text,
                                      this.tbxCity.Text,
                                      this.tbxPhoneNumber.Text,
                                      this.tbxEmail.Text,
                                      0);

                if (new Database().Insert(Database.TableName.Clients, c.ToString().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)))
                {
                    if (fmOrigin.GetType() == typeof(FormMain))
                    {
                        FormMain fm = (FormMain)fmOrigin;
                        fm.FormNewClient_CloseOnAdd();
                    }
                    this.Close();
                }
                else
                    MessageBox.Show("Failed to add new user");
            }
            catch
            {
                MessageBox.Show("Failed to add new user");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
