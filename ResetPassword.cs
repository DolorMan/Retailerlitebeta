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
    public partial class ResetPassword : Form
    {
        private Admin_Login admin_Login;
        public ResetPassword(Admin_Login adminLogin)
        {
            InitializeComponent();
            admin_Login = adminLogin;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string oldPassword = textBox1.Text;
            string newPassword = textBox2.Text;
            string confirmPassword = textBox3.Text;

            if (ValidateOldPassword(oldPassword))
            {
                if (newPassword == confirmPassword)
                {
                    admin_Login.StoredPassword = newPassword;
                    MessageBox.Show("Password has been reset successfully.");
                    ClearAllFields();
                    this.Close(); // Close the reset password form after success
                }
                else
                {
                    MessageBox.Show("New passwords do not match. Please try again.");
                }
            }
            else
            {
                MessageBox.Show("Old password is incorrect. Please try again.");
            }
        }
        private bool ValidateOldPassword(string oldPassword)
        {
            return oldPassword == admin_Login.StoredPassword;
        }
        private void ClearAllFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }


    }
}
