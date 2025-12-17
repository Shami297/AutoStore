using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using AutoStore.Logic;
using Oracle.ManagedDataAccess.Client;
using Dapper;

namespace AutoStore
{
    public partial class vendor : UserControl
    {
        //OleDbConnection conn = new OleDbConnection("Provider=MSDAORA;Data Source=ORCL;Persist Security Info=True;User ID=HR;Password=HR;Unicode=True");
        Connection pr = new Connection();
        public vendor()
        {
            InitializeComponent();
            idText.Enabled = false;
        }

        private void savebtn()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                ORCL.Execute("insert into vendors values('" + idText.Text + "','" + nameText.Text.ToLower() + "','" + address.Text + "','" + noText.Text + "','" + personText.Text + "')");
                MessageBox.Show("New vendor added successfully");
                textBox();
                Main1.disable(panel1);
            }
            catch { }
            finally { ORCL.Dispose(); }
            
        }

        private void updatebtn()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                ORCL.Execute("update vendors set company = '" + nameText.Text + "', address = '" + address.Text + "', contactperson = '" + personText.Text + "', cellno = '" + noText.Text + "' where id = '" + idText.Text + "'");
                MessageBox.Show("Vendor Data Updated successfully");
                textBox();
                Main1.disable(panel1);
            }
            catch { }
            finally { ORCL.Dispose(); }
        }

        private void deletebtn()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                ORCL.Execute("DELETE from vendors where id = '" + idText.Text + "'");
                MessageBox.Show("Vendor Deleted successfully");
                textBox();
                Main1.disable(panel1);
            }
            catch { }
            finally { ORCL.Dispose(); }
        }

        private void textBox()
        {
            idText.Text = "";
            nameText.Text = "";
            address.Text = "";
            personText.Text = "";
            noText.Text = "";
        }

        private void show()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                var vendr = ORCL.Query<vendorView>("select ID, Company, Address, CONTACTPERSON as Name, CELLNO as Phone from vendors").ToList();
                vendorGV.AutoGenerateColumns = true;
                vendorGV.DataSource = null;
                vendorGV.DataSource = vendr;
                Main1.disable(panel1);
            }
            catch { }
            finally { ORCL.Dispose(); }
        }

        private void search()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                var vendr = ORCL.Query<vendorView>("select ID, Company, Address, CONTACTPERSON as Name, CELLNO as Phone from vendors WHERE COMPANY LIKE '%" + searchText.Text.ToLower() + "%'").ToList();
                vendorGV.AutoGenerateColumns = true;
                vendorGV.DataSource = null;
                vendorGV.DataSource = vendr;
            }
            catch { }
            finally { ORCL.Dispose(); }
        }

        //---------------------------Generate ID----------------


        int vendorID;
        private int getVendorID()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                vendorID = ORCL.Query<int>("SELECT ID FROM (SELECT v.ID FROM Vendors v ORDER BY v.ID DESC) WHERE ROWNUM = 1").FirstOrDefault();
            }
            catch { }
            finally { ORCL.Dispose(); }
            return vendorID;
        }

        private void generateVendorID()
        {
            int venId = getVendorID();
            venId += 1;
            idText.Text = venId.ToString();
        }

        private void vendor_Load(object sender, EventArgs e)
        {
            Main1.disable(panel1);
        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void circularButton5_Click(object sender, EventArgs e)
        {
            deleteButton.Enabled = false;
            updateButton.Enabled = false;
            saveButton.Enabled = true;
            Main1.enable(panel1);
            generateVendorID();
            nameText.Text = "";
            address.Text = "";
            personText.Text = "";
            noText.Text = "";
            nameText.Focus();
        }

        private void circularButton3_Click(object sender, EventArgs e)
        {
            updatebtn();
            show();
        }

        private void circularButton2_Click(object sender, EventArgs e)
        {
            deletebtn();
            show();
        }

        private void circularButton1_Click(object sender, EventArgs e)
        {
            savebtn();
            show();
        }

        private void circularButton4_Click(object sender, EventArgs e)
        {
            show();
            textBox();
            searchText.Focus();
            vendorGV.AllowUserToAddRows = false;
        }

        private void vendorGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (vendorGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                deleteButton.Enabled = true;
                updateButton.Enabled = true;
                Main1.enable(panel1);
                saveButton.Enabled = false;
                vendorGV.CurrentCell.Selected = true;
                idText.Text = vendorGV.Rows[e.RowIndex].Cells["ID"].FormattedValue.ToString();
                nameText.Text = vendorGV.Rows[e.RowIndex].Cells["Company"].FormattedValue.ToString();
                address.Text = vendorGV.Rows[e.RowIndex].Cells["Address"].FormattedValue.ToString();
                personText.Text = vendorGV.Rows[e.RowIndex].Cells["Name"].FormattedValue.ToString();
                noText.Text = vendorGV.Rows[e.RowIndex].Cells["Phone"].FormattedValue.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main1.disable(panel1);
            textBox();
        }

        private void searchText_TextChange(object sender, EventArgs e)
        {
            search();
        }

        private void nameText_Leave(object sender, EventArgs e)
        {
            Main1.alphabetCheck(nameText);
        }

        private void noText_Leave(object sender, EventArgs e)
        {
            Main1.authenticateNo(noText);
        }
    }

    public class vendorView
    {
        public int ID { get; set; }
        public string Company { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
