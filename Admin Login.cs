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
        public Admin_Login()
        {
            InitializeComponent();
        }

        private void Admin_Login_Load(object sender, EventArgs e)
        {

        }

        private void btnAdmlogin_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Blaze33")
            {
                this.Hide();
                Admin_Dashboard admin_Dashboard = new Admin_Dashboard();
                admin_Dashboard.Show();
            }
            else
            {
                MessageBox.Show("Password is Incorrect! Try Again.");
            }
        }
    }
}
