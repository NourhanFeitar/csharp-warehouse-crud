using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Warehouse
{
    public partial class ProductsForm : Form
    {
        Model1 Entity;
        int warehouseID;
        int Prod_ID;
        public ProductsForm()
        {
            InitializeComponent();
            Entity= new Model1();
        }

        private void ProductsForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'warehouseDataSet2.Warehouse_Data' table. You can move, or remove it, as needed.
            this.warehouse_DataTableAdapter.Fill(this.warehouseDataSet2.Warehouse_Data);

            // Showing A list of products on load
            var prod= from p in Entity.Products select p;
            foreach(var item in prod)
            {
                listBox1.Items.Add(item.Name);
            }
        }


        // Selecting from listbox
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string Prod_Name= listBox1.SelectedItem.ToString();
            
            Product product= (from p in Entity.Products where p.Name== Prod_Name select p).FirstOrDefault();
            if( product != null )
            {
                textBox1.Text = Prod_Name;
                Prod_ID = product.Product_ID;
                textBox5.Text = Prod_ID.ToString();            
                Product_Measurments PM=  product.Product_Measurments.FirstOrDefault();
                textBox2.Text=PM.Length.ToString();
                textBox3.Text=PM.Width.ToString();
                textBox4.Text=PM.Weight.ToString();
                
            }
            else
            {
                MessageBox.Show("Product Was Not Found");
            }

        }

        // Add Button
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
            {
                Product product = new Product();
                product.Product_ID = int.Parse(textBox5.Text);
                product.Name = textBox1.Text;
                Product_Measurments product_Measurments = new Product_Measurments();
                product_Measurments.Length = int.Parse(textBox2.Text);
                product_Measurments.Width = int.Parse(textBox3.Text);
                product_Measurments.Weight = int.Parse(textBox4.Text);
                product.Product_Measurments.Add(product_Measurments);
                Product IDCheck = (from p in Entity.Products where p.Product_ID == product.Product_ID select p).FirstOrDefault();
                if (IDCheck == null)
                {
                    Entity.Products.Add(product);
                    Entity.SaveChanges();
                    MessageBox.Show("Product Added Succesfully");
                    listBox1.Items.Add(product.Name);

                }
            }
            else
            {
                MessageBox.Show("Please Enter Full Set of Data");
            }

            
        }

        // Update Button
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
            {
                int Prod_ID = int.Parse(textBox5.Text);
                Product Select_prod = (from p in Entity.Products where p.Product_ID == Prod_ID select p).FirstOrDefault();
                if (Select_prod != null)
                {
                    Select_prod.Name = textBox1.Text;
                    Product_Measurments PM = Select_prod.Product_Measurments.FirstOrDefault();
                    PM.Length = int.Parse(textBox2.Text);
                    PM.Width = int.Parse(textBox3.Text);
                    PM.Weight = int.Parse(textBox4.Text);
                    Entity.SaveChanges();
                    MessageBox.Show("Product Updates Succesfully");
                    RefreshList();
                }
            }
            else
            {
                MessageBox.Show("Please Enter Full Set of Data");

            }

        }

        public void RefreshList ()
        {
            listBox1.Items.Clear();
            var prod = from p in Entity.Products select p;
            foreach (var item in prod)
            {
                listBox1.Items.Add(item.Name);
            }
        }


        // Report Button
        private void button3_Click(object sender, EventArgs e)
        {
            ProductReport frm= new ProductReport();
            frm.ShowDialog();
        }
    }
}
