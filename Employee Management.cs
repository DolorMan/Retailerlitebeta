using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Retailerlitebeta
{
    public partial class Employee_Management : Form
    {
        public Employee_Management()
        {
            InitializeComponent();
        }

        private void Employee_Management_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'retailerlitebetaDataSet1.UserDetails' table. You can move, or remove it, as needed.
            this.userDetailsTableAdapter.Fill(this.retailerlitebetaDataSet1.UserDetails);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=G:\\repos\\Retailerlitebeta\\Retailerlitebeta\\Retailerlitebeta.mdf;Integrated Security=True;");
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into UserDetails(No,UserName) values ('"+textBox1.Text+"','"+textBox2.Text+"')",con);
            int i = cmd.ExecuteNonQuery();
            if (i == 0) {
                MessageBox.Show("Couldn't Add. Please Try Restarting the App");
            }
            else
            {
                MessageBox.Show("Employer Added Successfully!");
            }
            con.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=G:\\repos\\Retailerlitebeta\\Retailerlitebeta\\Retailerlitebeta.mdf;Integrated Security=True;");
            string c = comboBox1.SelectedItem.ToString();
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from UserDetails where UserName='" + c + "'", con);
            int d = cmd.ExecuteNonQuery();
            if (d == 0)
            {
                MessageBox.Show("Couldn't remove the user. Please try Restarting the App");
            }
            else
            {
                MessageBox.Show("User is Removed Successfully!");
            }*/
        }
    }
}
