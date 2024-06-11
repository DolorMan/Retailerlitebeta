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
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Login admin_Login = new Admin_Login();
            admin_Login.Show();
        }

        private void btnEmp_Click(object sender, EventArgs e)
        {
            this.Hide();
            Employee_Login employee_Login = new Employee_Login();
            employee_Login.Show();
        }
    }
}
