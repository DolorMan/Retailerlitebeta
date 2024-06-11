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
    public partial class Inventory : Form
    {
        public Inventory()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'inventoryDataSet.productsAll' table. You can move, or remove it, as needed.
            this.productsAllTableAdapter1.Fill(this.inventoryDataSet.productsAll);
            // TODO: This line of code loads data into the 'retailerlitebetaDataSet.productsAll' table. You can move, or remove it, as needed.
            this.productsAllTableAdapter.Fill(this.retailerlitebetaDataSet.productsAll);

        }
    }
}
