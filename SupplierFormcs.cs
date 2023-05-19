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

namespace Warehouse
{
    public partial class SupplierFormcs : Form
    {
        Model1 Entity;
        public SupplierFormcs()
        {
            InitializeComponent();
            Entity = new Model1();
        }


        private void supplierList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Sup_Name= supplierList.SelectedItem.ToString();
            Supplier supplier=(from Sup in Entity.Suppliers where Sup.Name == Sup_Name select Sup).FirstOrDefault();
            textBox2.Text = supplier.Supplier_ID.ToString();
            textBox3.Text = supplier.Name;
            textBox4.Text = supplier.Mobile_Number;
            textBox6.Text = supplier.Work_Number;
            textBox5.Text = supplier.Fax;
            textBox1.Text = supplier.Email;
            textBox7.Text = supplier.Website;

        }

        private void SupplierFormcs_Load(object sender, EventArgs e)
        {
            var Supplier = from Sup in Entity.Suppliers select Sup;
            foreach(var item  in Supplier)
            {
                supplierList.Items.Add(item.Name);
            }
        }

        //Adding Supplier
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "")
            {

                Supplier supplier = new Supplier();
                int SupID = int.Parse(textBox2.Text);
                supplier.Supplier_ID = SupID;
                supplier.Name = textBox3.Text;
                supplier.Mobile_Number = textBox4.Text;
                supplier.Work_Number = textBox6.Text;
                supplier.Fax = textBox5.Text;
                supplier.Email = textBox1.Text;
                supplier.Website = textBox7.Text;

                var IDCheck = (from sup in Entity.Suppliers where sup.Supplier_ID == SupID select sup).FirstOrDefault();
                if (IDCheck != null)
                {
                    MessageBox.Show("Supplier With That ID Already Exists");
                }
                else
                {
                    Entity.Suppliers.Add(supplier);
                    Entity.SaveChanges();
                    MessageBox.Show("Supplier Added Successfully!");
                    RefreshList();

                }
            }
            else
            {
                MessageBox.Show("Please Enter Full Set Of Data");

            }


        }

        //Updating Supplier
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "")
            {
                int SupID = int.Parse(textBox2.Text);
                Supplier supplier = (from sup in Entity.Suppliers where sup.Supplier_ID == SupID select sup).FirstOrDefault();
                if (supplier != null)
                {
                    supplier.Supplier_ID = SupID;
                    supplier.Name = textBox3.Text;
                    supplier.Mobile_Number = textBox4.Text;
                    supplier.Work_Number = textBox6.Text;
                    supplier.Fax = textBox5.Text;
                    supplier.Email = textBox1.Text;
                    supplier.Website = textBox7.Text;
                    Entity.SaveChanges();
                    MessageBox.Show("Supplier Updates Successfully!");
                    RefreshList();
                }
                else// ID Does Not Exist
                {
                    MessageBox.Show("Can't Update As Supplier With This ID Does Not Exist");
                }
            }
            else
            {
                MessageBox.Show("Please Enter Full Set Of Data");
            }
                    
        }

        public void RefreshList()
        {
            supplierList.Items.Clear();
            var Sup = from p in Entity.Suppliers select p;
            foreach (var item in Sup)
            {
                supplierList.Items.Add(item.Name);
            }
        }
    }
}
