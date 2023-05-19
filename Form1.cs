using Bogus;
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
    public partial class Form1 : Form
    {
        Model1 Entity;
        public Form1()
        {
            InitializeComponent();
            Entity = new Model1();
        }

        // Pressing On Products button
        private void button1_Click(object sender, EventArgs e)
        {
            ProductsForm frm = new ProductsForm();
            frm.ShowDialog();
        }

        // Warehouse Button
        private void button3_Click(object sender, EventArgs e)
        {
            WarehouseFrom frm = new WarehouseFrom();
            frm.ShowDialog();
        }

        // Suppliers
        private void button4_Click(object sender, EventArgs e)
        {
            SupplierFormcs frm = new SupplierFormcs();
            frm.ShowDialog();
        }

        //Clients
        private void button5_Click(object sender, EventArgs e)
        {
            ClientFormcs frm = new ClientFormcs();
            frm.ShowDialog();
        }

        // Requests
        private void button2_Click(object sender, EventArgs e)
        {
            RequestForm frm= new RequestForm();
            frm.ShowDialog();
        }

       

       
    }
}
