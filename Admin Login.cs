using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Retailerlitebeta
{
    public partial class Admin_Login : Form
    {
        private string storedPassword = "pub123";
        public string StoredPassword
        {
            get { return storedPassword; }
            set { storedPassword = value; }
        }
        public Admin_Login()
        {
            InitializeComponent();
        }

        private void Admin_Login_Load(object sender, EventArgs e)
        {

        }

        private void btnAdmlogin_Click(object sender, EventArgs e)
        {
            string enteredPassword = textBox1.Text;
            if (ValidatePassword(enteredPassword))
            {
                MessageBox.Show("Login successful.");
                Admin_Dashboard admin_Dashboard = new Admin_Dashboard();
                admin_Dashboard.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect password. Please try again.");
            }
        }
        private bool ValidatePassword(string enteredPassword)
        {
            // Validate the entered password
            return enteredPassword == storedPassword;
        }
        private void OpenResetPasswordForm()
        {
            // Open the Reset Password form
            ResetPassword resetPasswordForm = new ResetPassword(this);
            resetPasswordForm.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
             OpenResetPasswordForm();
        }
    }
}
