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
    public partial class Admin_Dashboard : Form
    {
        public Admin_Dashboard()
        {
            InitializeComponent();
        }

        private void Admin_Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Inventory inventory = new Inventory();
            inventory.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Employee_Management employee_Management = new Employee_Management();
            employee_Management.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            NewBill newBill = new NewBill();
            newBill.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            TodaySummary todaySummary = new TodaySummary();
            todaySummary.Show();
        }
    }
}
