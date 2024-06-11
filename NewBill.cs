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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing.Printing;
using System.Windows.Forms.VisualStyles;

namespace Retailerlitebeta
{
    public partial class NewBill : Form
    {
        private List<string> originalItems;
        public NewBill()
        {
            InitializeComponent();
            // Initialize the ComboBox
            // Load data from the database
            originalItems = LoadDataFromDatabase();

            // Bind the ComboBox to the data
            UpdateComboBoxItems(originalItems);

            // Handle the ComboBox TextUpdate event
            comboBox1.TextUpdate += ComboBox_TextUpdate;
            //comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

            comboBox1.KeyDown += ComboBox1_KeyDown;
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
        }

        private List<string> LoadDataFromDatabase()
        {
            List<string> items = new List<string>();

            // Connection string to your SQL database
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Retailerlitebeta\\Retailerlitebeta.mdf;Integrated Security=True;Connect Timeout=30;";
            string query = "SELECT Name FROM productsAll";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(reader["Name"].ToString());
                        }
                    }
                }
            }

            return items;
        }

        private void ComboBox_TextUpdate(object sender, EventArgs e)
        {
            string filter = comboBox1.Text.ToLower();
            var filteredItems = originalItems.Where(item => item.ToLower().Contains(filter)).ToList();

            UpdateComboBoxItems(filteredItems);

            // Set cursor to the end of the text
            comboBox1.SelectionStart = comboBox1.Text.Length;
            comboBox1.SelectionLength = 0;

            // Ensure the drop-down list is open
            comboBox1.DroppedDown = true;
            Cursor.Current = Cursors.Default;
        }

        private void UpdateComboBoxItems(List<string> items)
        {
            string currentText = comboBox1.Text;

            comboBox1.DataSource = null;
            comboBox1.DataSource = items;

            comboBox1.Text = currentText;
            comboBox1.SelectionStart = currentText.Length;
            comboBox1.SelectionLength = 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void Reset()
        {
            comboBox1.SelectedIndex = -1;
            textBox1.Text = string.Empty;
            dataGridView1.Rows.Clear();
            ind = 1;
            label7.Text = "Total: 0";
        }

        private void ComboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Check if Enter key is pressed
            {
                AddComboBoxValueToDataGridView();
                e.Handled = true; // Mark the event as handled
                e.SuppressKeyPress = true; // Prevent the beep sound on Enter key press
            }
        }

        private int ind = 1;
        private void AddComboBoxValueToDataGridView()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Retailerlitebeta\\Retailerlitebeta.mdf;Integrated Security=True;Connect Timeout=30;";
            string selectedItem = comboBox1.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedItem))
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM productsAll WHERE Name = @Name";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", selectedItem);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int price = Convert.ToInt32(reader["Price"]);
                        int quantity = 1; // Default quantity
                        int subtotal = price * quantity;

                        dataGridView1.Rows.Add(
                            ind.ToString(),
                            reader["Name"].ToString(),
                            reader["Price"].ToString()
                        );
                        ind++;

                        UpdateTotalSubtotal();
                    }
                    else
                    {
                        MessageBox.Show("No data found for the selected item.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an item from the ComboBox.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void NewBill_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'retailerlitebetaDataSet.productsAll' table. You can move, or remove it, as needed.
            this.KeyPreview = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reset();
            UpdateTotalSubtotal();
        }

        private void NewBill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 123)
            {
                button2.PerformClick();
            }
        }

        private object[] GetSelectedRowData(string selectedItem)
        {
            // Connection string to your SQL database
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Retailerlitebeta\\Retailerlitebeta.mdf;Integrated Security=True;Connect Timeout=30;";
            string query = "SELECT * FROM productsAll WHERE Name = @SelectedItem";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SelectedItem", selectedItem);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Assuming the table has columns: Id, Name, Description
                            return new object[]
                            {
                            reader["SKU"],
                            reader["Name"],
                            reader["Price"]
                            };
                        }
                    }
                }
            }

            return null;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Quantity"].Index)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                int quantity, price;
                // Parse the quantity and price values from the DataGridView
                if (int.TryParse(row.Cells["Quantity"].Value?.ToString(), out quantity) &&
                    int.TryParse(row.Cells["Price"].Value?.ToString(), out price))
                {
                    // Calculate subtotal
                    int subtotal = quantity * price;
                    // Update subtotal cell in DataGridView
                    row.Cells["Subtotal"].Value = subtotal;

                    UpdateTotalSubtotal();
                }
            }
        }

        private void UpdateTotalSubtotal()
        {
            int total = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Subtotal"].Value != null)
                {
                    total += Convert.ToInt32(row.Cells["Subtotal"].Value);
                }
            }

            label7.Text = $"Total: Rs: {total}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string orderType = radioButton1.Checked ? "Dine-In" : "Take-Away";
            ReceiptPrinter receiptPrinter = new ReceiptPrinter(dataGridView1, orderType);
            receiptPrinter.PrintReceipt();
        }

        public class ReceiptPrinter
        {
            private DataGridView dataGridView;
            private PrintDocument printDocument;
            private Font regularFont = new Font("Arial", 10);
            private Font boldFont = new Font("Arial", 10, FontStyle.Bold);
            private string orderType;
            private Font ubFont = new Font("Arial", 15, FontStyle.Bold);

            public ReceiptPrinter(DataGridView dgv, string orderType)
            {
                dataGridView = dgv;
                this.orderType = orderType;
                printDocument = new PrintDocument();
                printDocument.PrintPage += new PrintPageEventHandler(PrintReceiptPage);
                this.orderType = orderType;
            }

            public void PrintReceipt()
            {
                PrintDialog printDialog = new PrintDialog
                {
                    Document = printDocument
                };

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument.Print();
                }
            }

            private void PrintReceiptPage(object sender, PrintPageEventArgs e)
            {

                Graphics graphics = e.Graphics;
                float yPos = 10;
                float leftMargin = e.MarginBounds.Left;
                float rightMargin = e.MarginBounds.Right;
                float centerMargin = (rightMargin - leftMargin) / 2 + leftMargin;

                // Print header
                graphics.DrawString("Kirimatiyana PUB & BAR", boldFont, Brushes.Black, centerMargin, yPos, new StringFormat { Alignment = StringAlignment.Center });
                yPos += 20;
                graphics.DrawString("Tel:000000000", regularFont, Brushes.Black, leftMargin, yPos);
                yPos += 20;
                graphics.DrawString("info@yourcompany.com", regularFont, Brushes.Black, leftMargin, yPos);
                yPos += 20;
                graphics.DrawString("http://www.example.com", regularFont, Brushes.Black, leftMargin, yPos);
                yPos += 20;
                graphics.DrawString("Served by Mitchell Admin", regularFont, Brushes.Black, leftMargin, yPos);
                yPos += 30;

                // Print column headers
                graphics.DrawString("Product", boldFont, Brushes.Black, leftMargin, yPos);
                graphics.DrawString("Quantity", boldFont, Brushes.Black, centerMargin - 50, yPos, new StringFormat { Alignment = StringAlignment.Center });
                graphics.DrawString("Price", boldFont, Brushes.Black, centerMargin + 50, yPos, new StringFormat { Alignment = StringAlignment.Center });
                graphics.DrawString("Sub Total", boldFont, Brushes.Black, rightMargin - 100, yPos);
                yPos += 20;

                // Print items
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells["Product"].Value != null && row.Cells["Price"].Value != null && row.Cells["Quantity"].Value != null)
                    {
                        string product = row.Cells["Product"].Value.ToString();
                        string price = row.Cells["Price"].Value.ToString();
                        string quantity = row.Cells["Quantity"].Value.ToString();
                        float subTotal = float.Parse(price) * float.Parse(quantity);

                        graphics.DrawString($"{product}", regularFont, Brushes.Black, leftMargin, yPos);
                        graphics.DrawString($"{quantity}", regularFont, Brushes.Black, centerMargin - 50, yPos, new StringFormat { Alignment = StringAlignment.Center });
                        graphics.DrawString($"Rs: {price}", regularFont, Brushes.Black, centerMargin + 50, yPos, new StringFormat { Alignment = StringAlignment.Center });
                        graphics.DrawString($"Rs: {subTotal:F2}", regularFont, Brushes.Black, rightMargin - 100, yPos);
                        yPos += 20;
                    }
                }

                yPos += 30;
                float total = CalculateTotal();
                graphics.DrawString("TOTAL", boldFont, Brushes.Black, centerMargin, yPos, new StringFormat { Alignment = StringAlignment.Center });
                graphics.DrawString($"Rs:{total:F2}", regularFont, Brushes.Black, rightMargin - 100, yPos);

                // Print payment details
                /*yPos += 20;
                graphics.DrawString("Bank", regularFont, Brushes.Black, leftMargin, yPos);
                graphics.DrawString($"${total:F2}", regularFont, Brushes.Black, rightMargin - 100, yPos);
                yPos += 20;
                graphics.DrawString("CHANGE", regularFont, Brushes.Black, leftMargin, yPos);
                graphics.DrawString("$0.00", regularFont, Brushes.Black, rightMargin - 100, yPos);
                */
                // Print taxes and footer
                /*  yPos += 20;
                 graphics.DrawString("Total Taxes", regularFont, Brushes.Black, leftMargin, yPos);
                 graphics.DrawString("$0.00", regularFont, Brushes.Black, rightMargin - 100, yPos);
                 yPos += 20;
                 graphics.DrawString("Thanks for shopping with us!", regularFont, Brushes.Black, centerMargin, yPos, new StringFormat { Alignment = StringAlignment.Center });
                 yPos += 30;
                */
                yPos += 35;
                graphics.DrawString($"{orderType}", ubFont, Brushes.Black, centerMargin, yPos, new StringFormat { Alignment = StringAlignment.Center });
                yPos += 20;

                // Print order details
                yPos += 30;
                string currentDateTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                graphics.DrawString("Order 00001-001-0001", regularFont, Brushes.Black, centerMargin, yPos, new StringFormat { Alignment = StringAlignment.Center });
                yPos += 20;
                graphics.DrawString(currentDateTime, regularFont, Brushes.Black, centerMargin, yPos, new StringFormat { Alignment = StringAlignment.Center });
            }

            private float CalculateTotal()
            {
                float total = 0;
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells["Price"].Value != null && row.Cells["Quantity"].Value != null)
                    {
                        float price = float.Parse(row.Cells["Price"].Value.ToString());
                        float quantity = float.Parse(row.Cells["Quantity"].Value.ToString());
                        total += price * quantity;
                    }
                }
                return total;
            }
            }

    }
}

