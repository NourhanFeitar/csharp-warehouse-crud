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
    public partial class ClientFormcs : Form
    {
        Model1 Entity;
        public ClientFormcs()
        {
            InitializeComponent();
            Entity = new Model1();
        }

        //Selecting From Client List
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string client_Name = listBox1.SelectedItem.ToString();
            Client client = (from cli in Entity.Clients where cli.Name == client_Name select cli).FirstOrDefault();
            textBox2.Text = client.Client_ID.ToString();
            textBox3.Text = client.Name;
            textBox4.Text = client.Mobile_Number;
            textBox6.Text = client.Work_Number;
            textBox5.Text = client.Fax;
            textBox1.Text = client.Email;
            textBox7.Text = client.Website;
        }

        //On load of page
        private void ClientFormcs_Load(object sender, EventArgs e)
        {

            var client = from cl in Entity.Clients select cl;
            foreach (var item in client)
            {
                listBox1.Items.Add(item.Name);
            }
        }

        public void RefreshList()
        {
            listBox1.Items.Clear();
            var cli = from cl in Entity.Clients select cl;
            foreach (var item in cli)
            {
                listBox1.Items.Add(item.Name);
            }
        }

        //Add Button
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!="" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" )
            {
                Client client = new Client();
                int ClID = int.Parse(textBox2.Text);
                client.Client_ID = ClID;
                client.Name = textBox3.Text;
                client.Mobile_Number = textBox4.Text;
                client.Work_Number = textBox6.Text;
                client.Fax = textBox5.Text;
                client.Email = textBox1.Text;
                client.Website = textBox7.Text;

                var IDCheck = (from cli in Entity.Clients where cli.Client_ID == ClID select cli).FirstOrDefault();
                if (IDCheck != null)
                {
                    MessageBox.Show("A Client With That ID Already Exists");
                }
                else
                {
                    Entity.Clients.Add(client);
                    Entity.SaveChanges();
                    MessageBox.Show("Client Added Successfully!");
                    RefreshList();

                }
            }
            else
            {
                MessageBox.Show("Please Enter Full Set Of Data");
            }
            
        }

        //Updating Client
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "")
            {
                int CliID = int.Parse(textBox2.Text);
                Client client = (from cli in Entity.Clients where cli.Client_ID == CliID select cli).FirstOrDefault();
                if (client != null)
                {
                    client.Client_ID = CliID;
                    client.Name = textBox3.Text;
                    client.Mobile_Number = textBox4.Text;
                    client.Work_Number = textBox6.Text;
                    client.Fax = textBox5.Text;
                    client.Email = textBox1.Text;
                    client.Website = textBox7.Text;
                    Entity.SaveChanges();
                    MessageBox.Show("Client Updated Successfully!");
                    RefreshList();
                }
                else// ID Does Not Exist
                {
                    MessageBox.Show("Can't Update As Client With This ID Does Not Exist");
                }
            }
            else
            {
                MessageBox.Show("Please Enter Full Set Of Data");
            }

        }
    }
}
