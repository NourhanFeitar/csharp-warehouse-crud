using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Warehouse
{
    public partial class RequestForm : Form
    {
        Model1 Entity;
        public RequestForm()
        {
            InitializeComponent();
            Entity=new Model1();
        }

        //Import/Export 
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem.ToString()=="Import")
            {
                groupBox2.Visible = true;
                groupBox1.Visible = false;
                button1.Visible = true;
            }
            if (comboBox1.SelectedItem.ToString() == "Export")
            {
                groupBox1.Visible = true;
                groupBox2.Visible=false;
                button2.Visible = true;

            }
        }


        //Form on Load
        private void RequestForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'warehouseDataSet8.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.warehouseDataSet8.Products);
            // TODO: This line of code loads data into the 'warehouseDataSet7.Warehouse_Data' table. You can move, or remove it, as needed.
            this.warehouse_DataTableAdapter.Fill(this.warehouseDataSet7.Warehouse_Data);
            // TODO: This line of code loads data into the 'warehouseDataSet6.Client' table. You can move, or remove it, as needed.
            this.clientTableAdapter.Fill(this.warehouseDataSet6.Client);
            // TODO: This line of code loads data into the 'warehouseDataSet5.Suppliers' table. You can move, or remove it, as needed.
            this.suppliersTableAdapter.Fill(this.warehouseDataSet5.Suppliers);

        }

        //Export Request with client
        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox5.Text!= "" & textBox6.Text!="")
            {
                int ClientId = int.Parse(comboBox3.Text.ToString());
                int Req_ID = int.Parse(textBox5.Text.ToString());
                int Warehouse_ID = int.Parse(comboBox5.Text.ToString());

               

                int ProductID = int.Parse(comboBox4.Text.ToString());
                int SupplierID = int.Parse(comboBox2.Text.ToString());
                int P_Qauntity = int.Parse(textBox6.Text.ToString());
                DateTime date = DateTime.Now;


                //Checking If Request Id Exists
                var IDCheck = (from R in Entity.Client_Request where R.Request_ID == Req_ID select R).FirstOrDefault();
                if (IDCheck != null)
                {
                    MessageBox.Show("A Request With The Same ID Already Exists");
                }
                else
                {
                    //Checking iF Product Exists in Warehouse
                    var Product_Check = (from p in Entity.Warehouse_Products
                                         where p.Products_ID == ProductID
                                         where p.Warehouse_ID == Warehouse_ID
                                         select p).FirstOrDefault();
                    if (Product_Check != null)
                    { // Products Existing in "To Warehouse"
                        if (Product_Check.Quantity > P_Qauntity)
                        {
                            //Adding To Client_Request Table
                            Client_Request client_Request = new Client_Request();
                            client_Request.Request_ID = Req_ID;
                            client_Request.Warehouse_ID = Warehouse_ID;
                            client_Request.Date = date;
                            client_Request.Client_ID = ClientId;
                            Entity.Client_Request.Add(client_Request);
                            Entity.SaveChanges();

                            // Adding to Client Req Products
                            Client_Req_Products client_Req_Products = new Client_Req_Products();
                            client_Req_Products.Product_ID = ProductID;
                            client_Req_Products.Request_ID = Req_ID;
                            client_Req_Products.Quantity = P_Qauntity;
                            client_Req_Products.Supplier_ID = SupplierID;
                            client_Req_Products.Client_ID = ClientId;
                            Entity.Client_Req_Products.Add(client_Req_Products);
                            Entity.SaveChanges();

                            Product_Check.Quantity -= P_Qauntity;
                            Entity.SaveChanges();
                            MessageBox.Show("Export Processed Succesfuly!");
                            textBox6.Text = textBox5.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("The Asked Quantity Is Bigger Than The Quantity Available in Warehouse");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Enter Full Set Of Data");
            }
            
        }


        // Submitting Import Request /Suppliers
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox14.Text!="" && textBox12.Text!="")
            {
                int SupplierID = int.Parse(comboBox6.Text.ToString());
                int Req_ID = int.Parse(textBox14.Text);
                int Warehouse_ID = int.Parse(comboBox7.Text.ToString());
                int P_Quantity = int.Parse(textBox12.Text);
                DateTimePicker Manufacture_Date = dateTimePicker1;
                DateTimePicker Expiration_Date = dateTimePicker2;
                DateTime Req_Date = DateTime.Now;
                int Product_ID = int.Parse(comboBox8.Text.ToString());

                //Checking If Request Id Exists
                var IDCheck = (from R in Entity.Supplier_Request where R.Request_ID == Req_ID select R).FirstOrDefault();
                if (IDCheck != null)
                {
                    MessageBox.Show("A Request With The Same ID Already Exists");
                }
                else
                {
                    //Updating Supplier Request Table
                    Supplier_Request supplier_Request = new Supplier_Request();
                    supplier_Request.Request_ID = Req_ID;
                    supplier_Request.Warehouse_ID = Warehouse_ID;
                    supplier_Request.Date = DateTime.Now;
                    supplier_Request.Supplier_ID = SupplierID;
                    Entity.Supplier_Request.Add(supplier_Request);
                    Entity.SaveChanges();

                    //Updating Supplier Request Product Table
                    Sup_Req_Products sup_Req_Products = new Sup_Req_Products();
                    sup_Req_Products.Product_ID = Product_ID;
                    sup_Req_Products.Request_ID = Req_ID;
                    sup_Req_Products.Quantity = P_Quantity;
                    DateTime Exp_date = Convert.ToDateTime(Expiration_Date.Value.ToShortDateString());
                    sup_Req_Products.Exp_Date = Exp_date;
                    DateTime Man_Date = Convert.ToDateTime(Manufacture_Date.Value.ToShortDateString());
                    sup_Req_Products.Manu_Date = Man_Date;
                    Entity.Sup_Req_Products.Add(sup_Req_Products);
                    Entity.SaveChanges();

                    // Updating Quantity in Warehouse
                    var Product_Check = (from p in Entity.Warehouse_Products
                                         where p.Products_ID == Product_ID
                                         where p.Warehouse_ID == Warehouse_ID
                                         select p).FirstOrDefault();
                    if (Product_Check != null)
                    { // Product Exists In Warehouses
                        Product_Check.Quantity += P_Quantity;
                        Entity.SaveChanges();
                    }
                    else// New Product in Warehouse
                    {
                        Warehouse_Products warehouse_Products = new Warehouse_Products();
                        warehouse_Products.Warehouse_ID = Warehouse_ID;
                        warehouse_Products.Products_ID = Product_ID;
                        warehouse_Products.Quantity = P_Quantity;
                        Entity.Warehouse_Products.Add(warehouse_Products);
                        Entity.SaveChanges();
                        MessageBox.Show("Request Processed Successully");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Enter Full Set Of Data");
            }
            
        }

        //Choosing Warehouse
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            int WID= int.Parse(comboBox5.Text.ToString());
            //Adding Products From The Chosen Warehouse in thei combobox
            var products = from P in Entity.Warehouse_Products where P.Warehouse_ID == WID select P;
            foreach (var product in products)
            {
                comboBox4.Items.Add(product.Products_ID.ToString());
            }
        }

        // Adding Quantity of Chosen Product
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int P_Id=int.Parse(comboBox4.Text.ToString());
            int WID = int.Parse(comboBox5.Text.ToString());
            var product = (from p in Entity.Warehouse_Products
                           where p.Products_ID == P_Id
                           where p.Warehouse_ID == WID
                           select p).FirstOrDefault();
            textBox1.Text=product.Quantity.ToString();
        }
    }
}
