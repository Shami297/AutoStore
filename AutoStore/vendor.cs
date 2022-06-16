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
            pr.conn.Open();
            OleDbCommand cmd = new OleDbCommand("insert into vendors values('" + idText.Text + "','" + nameText.Text.ToLower() + "','" + address.Text + "','" + noText.Text + "','" + personText.Text + "')", pr.conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("New vendor added successfully");
            textBox();
            pr.conn.Close();
            Main1.disable(panel1);
        }

        private void updatebtn()
        {
            pr.conn.Open();
            OleDbCommand cmd = new OleDbCommand("update vendors set company = '" + nameText.Text + "', address = '" + address.Text + "', contactperson = '" + personText.Text + "', cellno = '" + noText.Text + "' where id = '" + idText.Text + "'", pr.conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Vendor Data Updated successfully");
            textBox();
            pr.conn.Close();
            Main1.disable(panel1);
        }

        private void deletebtn()
        {
            pr.conn.Open();
            OleDbCommand cmd = new OleDbCommand("delete from vendors where id = '" + idText.Text + "'", pr.conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Vendor Deleted successfully");
            textBox();
            pr.conn.Close();
            Main1.disable(panel1);
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
            pr.conn.Open();
            OleDbDataAdapter oda = new OleDbDataAdapter("select * from vendors", pr.conn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            vendorGV.DataSource = dt;
            pr.conn.Close();
            Main1.disable(panel1);
        }

        private void search()
        {
            pr.conn.Open();
            OleDbDataAdapter oda = new OleDbDataAdapter("select * from vendors WHERE COMPANY LIKE '%" + searchText.Text.ToLower() + "%'", pr.conn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            vendorGV.DataSource = dt;
            pr.conn.Close();
        }

        //---------------------------Generate ID----------------


        int vendorID;
        private int getVendorID()
        {
            OleDbCommand command = new OleDbCommand("SELECT ID FROM (SELECT v.ID FROM Vendors v ORDER BY v.ID DESC) WHERE ROWNUM = 1", pr.conn);
            pr.conn.Open();
            vendorID = Convert.ToInt32(command.ExecuteScalar());
            pr.conn.Close();
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
                nameText.Text = vendorGV.Rows[e.RowIndex].Cells["COMPANY"].FormattedValue.ToString();
                address.Text = vendorGV.Rows[e.RowIndex].Cells["ADDRESS"].FormattedValue.ToString();
                personText.Text = vendorGV.Rows[e.RowIndex].Cells["CONTACTPERSON"].FormattedValue.ToString();
                noText.Text = vendorGV.Rows[e.RowIndex].Cells["CELLNO"].FormattedValue.ToString();
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
}
