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
    public partial class ProductReport : Form
    {
        Model1 Entity;
        public ProductReport()
        {
            InitializeComponent();
            Entity = new Model1();
        }

        private void ProductReport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'warehouseDataSet10.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.warehouseDataSet10.Products);

        }


        // Choosing Product ID
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;

            DataTable dt = new DataTable();
            dt.Columns.Add("Product Name");
            dt.Columns.Add("Warehouse ID");
            dt.Columns.Add("Produt Quantity");

            int Prod_ID = int.Parse(comboBox1.SelectedValue.ToString());
            var product =(from pp in Entity.Products where pp.Product_ID==Prod_ID select pp).FirstOrDefault();
            var pm=(from pro in Entity.Product_Measurments where pro.Product_ID == Prod_ID select pro).FirstOrDefault();
            textBox6.Text=pm.Length.ToString();
            textBox5.Text=pm.Width.ToString();
            textBox1.Text=pm.Weight.ToString();
            var WP= from wp in Entity.Warehouse_Products where wp.Products_ID==Prod_ID select wp;
            if(WP.Count()!=0)
            {
                foreach (var wp in WP)
                {
                    dt.Rows.Add(product.Name, wp.Warehouse_ID, wp.Quantity);
                    dataGridView1.DataSource = dt;
                }
            }
            else
            {
                MessageBox.Show("This Products Is Not Available In Any Warehouse");
            }
          
        }


        //Generating Report
        private void button3_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            DataTable table= new DataTable();
            table.Columns.Add("Products Id");
            table.Columns.Add("Warehouse Id");
            table.Columns.Add("Products Quantity");
            table.Columns.Add("Manufacture Date");
            table.Columns.Add("Expiration Date");


            if (textBox2.Text!="")
            {
                int NumOfDays= int.Parse(textBox2.Text);
                DateTime Date = dt.AddDays(-NumOfDays).Date;

                var supReq= (from sp in Entity.Supplier_Request where sp.Date==Date select sp);
                if(supReq!=null)
                {
                    foreach( var sup in supReq)
                    {

                        int Req_Id = sup.Request_ID;
                        int? Warhouse_Id = sup.Warehouse_ID;
                        var product=(from pro in Entity.Sup_Req_Products where pro.Request_ID == Req_Id select pro).FirstOrDefault();   
                        if(product!=null)
                        {
                            int productID = product.Product_ID;
                            string ExpDate=product.Exp_Date.ToString();
                            string ManDate=product.Manu_Date.ToString();
                            var wp = (from p in Entity.Warehouse_Products
                                            where p.Products_ID == productID
                                            where p.Warehouse_ID == Warhouse_Id
                                            select p).FirstOrDefault();
                            int? Quantity = wp.Quantity;
                            table.Rows.Add(productID,Warhouse_Id, Quantity,ManDate,ExpDate);
                           dataGridView2.DataSource = table;
                        }

                   }

                }
                else
                {
                    MessageBox.Show($"No Products imported {NumOfDays} Days Ago ");
                }

            }
            else
            {
                MessageBox.Show("Please Insert Number Of Days");
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Product Id");
            table.Columns.Add("Product Name");
            table.Columns.Add("Product Quantity");
            table.Columns.Add("Manufacture Date");
            table.Columns.Add("Warehouse ID");
            table.Columns.Add("Warehouse Name");


            if (textBox3.Text!="")
            {
                int NumOfDays = int.Parse(textBox3.Text);
                DateTime dt = DateTime.Now;
                DateTime Exp_Date = dt.AddDays(NumOfDays).Date;
                var product= from p in Entity.Sup_Req_Products where p.Exp_Date == Exp_Date select p;
                if(product.Count()!=0 )
                {
                    foreach( var item in product )
                    {
                        int prodId = item.Product_ID;
                        string Man_Date= item.Manu_Date.ToString();
                        int ReqID = item.Request_ID;
                        int? Warehouse_Id = (from R in Entity.Supplier_Request where R.Request_ID == ReqID select R.Warehouse_ID).FirstOrDefault();
                        var wp = (from p in Entity.Warehouse_Products
                                  where p.Products_ID == prodId
                                  where p.Warehouse_ID == Warehouse_Id
                                  select p).FirstOrDefault();
                        int? Quantity = wp.Quantity;
                        string warehouse_Name=(from w in Entity.Warehouse_Data where w.Warehouse_ID==Warehouse_Id select w.Name).FirstOrDefault();
                        string ProductName=(from p in Entity.Products where p.Product_ID==prodId select p.Name).FirstOrDefault();
                        table.Rows.Add(prodId,ProductName,Quantity, Man_Date, Warehouse_Id, warehouse_Name);
                        dataGridView3.DataSource = table;
                    }
                  
                }
                else
                {
                    MessageBox.Show($"No Products Will Expire In {NumOfDays} Days ");
                }
            }
            else
            {
                MessageBox.Show("Please Enter Number Of Days");
            }
           
            
        }
    }
}
