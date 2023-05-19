using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Warehouse
{
    public partial class Reports : Form
    {
        Model1 Entity;
        public Reports()
        {
            InitializeComponent();
            Entity = new Model1();
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'warehouseDataSet9.Warehouse_Data' table. You can move, or remove it, as needed.
            this.warehouse_DataTableAdapter.Fill(this.warehouseDataSet9.Warehouse_Data);

        }


        // selecting Warehouse
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int house_ID= int.Parse(comboBox1.SelectedValue.ToString());
            DataTable dt = new DataTable("Products");

            dt.Columns.Add("Warehouse Products ID");
            dt.Columns.Add("Products Quantity");

            var warehouse = (from ww in Entity.Warehouse_Data where ww.Warehouse_ID == house_ID select ww).FirstOrDefault();
            if (warehouse != null)
            {
                textBox2.Text = warehouse.Warehouse_ID.ToString();
                textBox3.Text = warehouse.Name;
                textBox4.Text = warehouse.Manager;

                var products = from Wp in Entity.Warehouse_Products where Wp.Warehouse_ID==house_ID select Wp;  
                foreach (var p in products)
                {

                    dt.Rows.Add(p.Products_ID, p.Quantity);
                }
                dataGridView1.DataSource = dt;
            }
        }


        // Generating Report relating to Transfers
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
            dataGridView3.DataSource = null;

            DateTimePicker dtp = dateTimePicker1;
            DateTime Chosen_Date = Convert.ToDateTime(dtp.Value.ToShortDateString());

            DataTable Im = new DataTable("Import");
            Im.Columns.Add("Request ID");
            Im.Columns.Add("Supplier ID");
            Im.Columns.Add("Warehouse ID");
            Im.Columns.Add("Product ID");
            Im.Columns.Add("Quantity ID");

            DataTable  Exp  = new DataTable("Export");
            Exp.Columns.Add("Request ID");
            Exp.Columns.Add("Client ID");
            Exp.Columns.Add("Warehouse ID");
            Exp.Columns.Add("Product ID");
            Exp.Columns.Add("Quantity ID");



            //dt.Columns.Add("Warehouse Products ID");
            //dt.Columns.Add("Products Quantity");

            var Sup_Req = (from Sp in Entity.Supplier_Request where Sp.Date == Chosen_Date select Sp);
            var Client_Req = (from Cq in Entity.Client_Request where Cq.Date == Chosen_Date select Cq);

            if(Client_Req == null)
            {
                MessageBox.Show("No Export Transactions On That Date");
            }
            else
            // Transaction With this Date Exists
            {
                foreach(var T in Client_Req)
                {
                    int req_ID = T.Request_ID;
                    var productInfo = (from Pi in Entity.Client_Req_Products where Pi.Request_ID == req_ID select Pi).FirstOrDefault();
                    if (productInfo != null)
                    {
                        Exp.Rows.Add(req_ID, T.Client_ID, T.Warehouse_ID, productInfo.Product_ID, productInfo.Quantity);
                        dataGridView3.DataSource = Exp;
                    }
                }
               
            }
            if (Sup_Req == null)
            {
                MessageBox.Show( "No Import Transactions On That Date");
            }
            else
            {
                foreach (var T in Sup_Req)
                {
                    int req_ID = T.Request_ID;
                    var productInfo = ( from Pi in Entity.Sup_Req_Products where Pi.Request_ID==req_ID select Pi).FirstOrDefault();
                    if (productInfo != null)
                    {
                        Im.Rows.Add(req_ID, T.Supplier_ID, T.Warehouse_ID, productInfo.Product_ID, productInfo.Quantity);
                        dataGridView2.DataSource = Im;
                    }
                }
             
            }
        }
    }
}
