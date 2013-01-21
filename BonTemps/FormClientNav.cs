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
    public partial class FormClientNav : Form
    {
        Form fmOrigin = new Form();
        bool update = false;
        int id = -1;
        public FormClientNav(Form fm)
        {
            InitializeComponent();
            this.fmOrigin = fm;
        }

        public FormClientNav(Form fm, bool update, int id)
        {
            InitializeComponent();
            this.fmOrigin = fm;
            this.update = update;
            this.id = id;
            FillData(this.id);
        }

        private void FillData(int id)
        {
            if (id != -1)
            {
                Client cl = new Database().GetClient((ulong)id);
                this.tbxFirstName.Text = cl.FirstName;
                this.tbxFirstName.Enabled = false;
                this.tbxLastName.Text = cl.LastName;
                this.tbxLastName.Enabled = false;
                this.tbxAddress.Text = cl.Address;
                this.tbxPostalCode.Text = cl.PostalCode;
                this.tbxCity.Text = cl.City;
                this.tbxPhoneNumber.Text = cl.PhoneNumber;
                this.tbxEmail.Text = cl.Email;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (update == false)
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
            else
            {
                try
                {
                    if(new Database().Update(Database.TableName.Clients, new String[] { "Address", "PostalCode", "City", "PhoneNumber", "Email" }, new String[] { this.tbxAddress.Text, this.tbxPostalCode.Text, this.tbxCity.Text, this.tbxPhoneNumber.Text, this.tbxEmail.Text }, id))
                    {
                            if (fmOrigin.GetType() == typeof(FormMain))
                            {
                                FormMain fm = (FormMain)fmOrigin;
                                fm.FormNewClient_CloseOnAdd();
                            }
                            this.Close();
                    }
                    else
                    {
                            MessageBox.Show("Failed to edit new user");
                    }
                }
                catch
                {
                    MessageBox.Show("Failed to edit new user");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
