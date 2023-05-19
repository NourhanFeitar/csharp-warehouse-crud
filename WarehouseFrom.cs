using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Warehouse
{
    public partial class WarehouseFrom : Form
    {
        Model1 Entity;
        int warehouse_ID;
        string Selected_product;
        //int ProductID;
        public WarehouseFrom()
        {
            InitializeComponent();
            Entity = new Model1();
        }

        //Onload Of Page
        private void WarehouseFrom_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'warehouseDataSet4.Warehouse_Data' table. You can move, or remove it, as needed.
            this.warehouse_DataTableAdapter1.Fill(this.warehouseDataSet4.Warehouse_Data);
            // TODO: This line of code loads data into the 'warehouseDataSet3.Warehouse_Data' table. You can move, or remove it, as needed.
            this.warehouse_DataTableAdapter.Fill(this.warehouseDataSet3.Warehouse_Data);
            var wareh = from w in Entity.Warehouse_Data select w;
            foreach (var item in wareh)
            {
                listBox2.Items.Add(item.Name);
            }

        }

        public void RefreshList()
        {
            this.warehouse_DataTableAdapter.Fill(this.warehouseDataSet3.Warehouse_Data);
            listBox2.Items.Clear();
            var wareh = from w in Entity.Warehouse_Data select w;
            foreach (var item in wareh)
            {
                listBox2.Items.Add(item.Name);
            }
        }

        // Selecting From List Box
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            string W_Name= listBox2.SelectedItem.ToString();
            Warehouse_Data WD= (from w in Entity.Warehouse_Data where w.Name==W_Name select w).FirstOrDefault();
            if (WD != null)
            {
                textBox2.Text = WD.Warehouse_ID.ToString();
                textBox3.Text = WD.Name;
                textBox4.Text = WD.Manager;
            }
        }

        // Add Button
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                listBox1.Items.Clear();
                Warehouse_Data wareH = new Warehouse_Data();
                int WID = int.Parse(textBox2.Text);
                wareH.Warehouse_ID = int.Parse(textBox2.Text);
                wareH.Name = textBox3.Text;
                wareH.Manager = textBox4.Text;
                var IdCHeck = (from w in Entity.Warehouse_Data where w.Warehouse_ID == WID select w).FirstOrDefault();
                if (IdCHeck == null)
                {
                    Entity.Warehouse_Data.Add(wareH);
                    Entity.SaveChanges();
                    MessageBox.Show("Warehouse Added Succesfully");
                    RefreshList();

                }
                else
                {
                    MessageBox.Show(" A Warehouse With The Same Id Already Exists");
                }
            }
            else
            {
                MessageBox.Show("Please Enter Full Set Of Data");
            }
            // Updating Combo BOXES
            // TODO: This line of code loads data into the 'warehouseDataSet4.Warehouse_Data' table. You can move, or remove it, as needed.
            this.warehouse_DataTableAdapter1.Fill(this.warehouseDataSet4.Warehouse_Data);
            // TODO: This line of code loads data into the 'warehouseDataSet3.Warehouse_Data' table. You can move, or remove it, as needed.
            this.warehouse_DataTableAdapter.Fill(this.warehouseDataSet3.Warehouse_Data);

        }

        //Update Button
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                int WID = int.Parse(textBox2.Text);
                Warehouse_Data WD = (from w in Entity.Warehouse_Data where w.Warehouse_ID == WID select w).FirstOrDefault();
                if (WD != null)
                {
                    WD.Name = textBox3.Text;
                    WD.Manager = textBox4.Text;
                    Entity.SaveChanges();
                    MessageBox.Show("Warehouse Updated Succesfully");
                    RefreshList();

                }
                else
                {
                    MessageBox.Show("This Warehouse Does Not Exist");
                }
            }
            else
            {
                MessageBox.Show("Please Enter Full Set Of Data");

            }

        }


        // Choosing "From" To Warehouse 
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            warehouse_ID = int.Parse(comboBox1.SelectedValue.ToString());
            var Products = from p in Entity.Warehouse_Products where p.Warehouse_ID == warehouse_ID select p;
            foreach (var item in Products)
            {
                listBox1.Items.Add(item.Products_ID);

            }
        }

        //Choosing Product From Warehouse
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            int ProductID= int.Parse(listBox1.SelectedItem.ToString());
            var S_Prod = (from p in Entity.Products where p.Product_ID == ProductID select p).FirstOrDefault();
            textBox5.Text = S_Prod.Name;
            int from_Warehouse = int.Parse(comboBox1.SelectedValue.ToString());
            var Chosen_Product_FromWarehouse = (from p in Entity.Warehouse_Products
                                                where p.Products_ID == ProductID
                                                where p.Warehouse_ID == from_Warehouse
                                                select p).FirstOrDefault();
            int? Product_Quantity = Chosen_Product_FromWarehouse.Quantity;
            textBox6.Text = Product_Quantity.ToString();


        }

        //Transfer Button
        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!="" && textBox7.Text!="")
            {
                if(listBox1.SelectedItem!=null)
                {
                    //Checking For the transfer ID Before Transfer

                    int Trans_ID = int.Parse(textBox7.Text);
                    var CheckTransfer = (from t in Entity.Transfers where t.Transfer_ID == Trans_ID select t).FirstOrDefault();
                    if (CheckTransfer != null)
                    {
                        MessageBox.Show("Transfer Id Already Exists");
                    }
                    else
                    {
                        int from_Warehouse = int.Parse(comboBox1.SelectedValue.ToString());
                        int to_Warehouse = int.Parse(comboBox2.SelectedValue.ToString());
                        int productID = int.Parse(listBox1.SelectedItem.ToString());

                        var Chosen_Product_FromWarehouse = (from p in Entity.Warehouse_Products
                                                            where p.Products_ID == productID
                                                            where p.Warehouse_ID == from_Warehouse
                                                            select p).FirstOrDefault();
                        int? Product_Quantity = Chosen_Product_FromWarehouse.Quantity;
                        int QuantityToTransfer = int.Parse(textBox1.Text);

                        if (Chosen_Product_FromWarehouse.Quantity <= QuantityToTransfer)
                        {
                            MessageBox.Show("Please Select Suitable Quantity");
                        }
                        else
                        {
                            //Checking iF Product Exists in To Warehouse
                            var Chosen_Product_ToWarehouse = (from p in Entity.Warehouse_Products
                                                              where p.Products_ID == productID
                                                              where p.Warehouse_ID == to_Warehouse
                                                              select p).FirstOrDefault();
                            if (Chosen_Product_ToWarehouse != null)
                            { // Products Existing in "To Warehouse"
                                Chosen_Product_ToWarehouse.Quantity += QuantityToTransfer;
                                Chosen_Product_FromWarehouse.Quantity -= QuantityToTransfer;

                                Entity.SaveChanges();
                            }
                            else
                            {// Products Dont Exist in "To Warehouse"
                                Warehouse_Products warehouse_Products = new Warehouse_Products();
                                warehouse_Products.Products_ID = productID;
                                warehouse_Products.Quantity = QuantityToTransfer;
                                warehouse_Products.Warehouse_ID = to_Warehouse;
                                Entity.Warehouse_Products.Add(warehouse_Products);
                                Chosen_Product_FromWarehouse.Quantity -= QuantityToTransfer;
                                Entity.SaveChanges();

                            }
                            textBox6.Text = Chosen_Product_FromWarehouse.Quantity.ToString();
                            MessageBox.Show("Transfer Succesful!");
                            // Update Transfer Table 
                            Transfer transfer = new Transfer();
                            transfer.Transfer_ID = Trans_ID;
                            transfer.From_WH_ID = from_Warehouse;
                            transfer.To_WH_ID = to_Warehouse;
                            Entity.Transfers.Add(transfer);
                            Entity.SaveChanges();

                            //Update Transfer Products Table
                            Transfer_Products TransP = new Transfer_Products();
                            TransP.Product_ID = productID;
                            TransP.Transfer_ID = Trans_ID;
                            TransP.Quantity = QuantityToTransfer;

                            int Req_ID = (from Req in Entity.Sup_Req_Products where Req.Product_ID == productID select Req.Request_ID).FirstOrDefault();
                            int? SUpplierID = (from S in Entity.Supplier_Request where S.Request_ID == Req_ID select S.Supplier_ID).FirstOrDefault();
                            TransP.Supplier_ID = SUpplierID;
                            Entity.Transfer_Products.Add(TransP);
                            Entity.SaveChanges();
                            textBox1.Text = textBox7.Text = "";
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Please Select Product You Want To Transfer");

                }

            }
            else
            {
                MessageBox.Show("Please Enter Quantity And transfer ID");
            }
           
        }

        // Reportts Button
        private void button4_Click(object sender, EventArgs e)
        {
            Reports reports = new Reports();
            reports.ShowDialog();
        }
    }
}
