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
    public partial class Employee_Login : Form
    {
        public Employee_Login()
        {
            InitializeComponent();
        }

        private void Employee_Login_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'retailerlitebetaDataSet1.UserDetails' table. You can move, or remove it, as needed.
            this.userDetailsTableAdapter.Fill(this.retailerlitebetaDataSet1.UserDetails);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            NewBill newBill = new NewBill();
            newBill.Show();
        }
    }
}
