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
using System.Text.RegularExpressions;

namespace AutoStore
{
    public partial class customer : UserControl
    {
        //OleDbConnection conn = new OleDbConnection("Provider=MSDAORA;Data Source=ORCL;Persist Security Info=True;User ID=HR;Password=HR;Unicode=True");
        Connection pr = new Connection();

        public customer()
        {
            InitializeComponent();
            idText.Enabled = false;
        }

        private void clearText()
        {
            idText.Text = "";
            cstmrText.Text = "";
            cityText.Text = "";
            addressText.Text = "";
            noText.Text = "";
        }

        private void savebtn()
        {
            pr.conn.Open();
            OleDbCommand cmd = new OleDbCommand("insert into customer values('" + idText.Text + "','" +
                cstmrText.Text + "','" + cityText.Text + "','" + noText.Text + "','" +
                addressText.Text + "')", pr.conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("New Customer Added successfully");
            clearText();
            pr.conn.Close();
            Main1.disable(panel1);
        }

        private void updatebtn()
        {
            pr.conn.Open();
            OleDbCommand cmd = new OleDbCommand("update customer set cstmr_name = '" + cstmrText.Text + "', cstmr_city = '" + cityText.Text + "', cstmr_address = '" + addressText.Text + "', cstmr_no = '" + noText.Text + "' where cstmr_id = '" + idText.Text + "'", pr.conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Customer Updated successfully");
            clearText();
            pr.conn.Close();
            Main1.disable(panel1);
        }

        private void deletebtn()
        {
            pr.conn.Open();
            OleDbCommand cmd = new OleDbCommand("delete from customer where cstmr_id = '" + idText.Text + "'", pr.conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Customer Deleted Successfully");
            clearText();
            pr.conn.Close();
            Main1.disable(panel1);
        }

        private void showdata(DataGridView customergv, DataGridViewColumn cid, DataGridViewColumn cname, DataGridViewColumn caddress, DataGridViewColumn cno,DataGridViewColumn ccity)
        {
            pr.conn.Open();
            OleDbDataAdapter oda = new OleDbDataAdapter("select * from customer", pr.conn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            cid.DataPropertyName = dt.Columns["cstmr_id"].ToString();
            cname.DataPropertyName = dt.Columns["cstmr_name"].ToString();
            caddress.DataPropertyName = dt.Columns["cstmr_address"].ToString();
            ccity.DataPropertyName = dt.Columns["cstmr_city"].ToString();
            cno.DataPropertyName = dt.Columns["cstmr_no"].ToString();

            customergv.DataSource = dt;
            pr.conn.Close();
            Main1.disable(panel1);
        }

        //---------------------------Generate ID----------------


        int customerID;
        private int getCustomerID()
        {
            OleDbCommand command = new OleDbCommand("SELECT CSTMR_ID FROM (SELECT c.CSTMR_ID FROM Customer c ORDER BY c.CSTMR_ID DESC) WHERE ROWNUM = 1", pr.conn);
            pr.conn.Open();
            customerID = Convert.ToInt32(command.ExecuteScalar());
            pr.conn.Close();
            return customerID;
        }

        private void generateCustomerID()
        {
            int custId = getCustomerID();
            custId += 1;
            idText.Text = custId.ToString();
        }


        private void circularButton4_Click(object sender, EventArgs e)
        {
            circularButton2.Enabled = false;
            circularButton3.Enabled = false;
            circularButton1.Enabled = true;
            Main1.enable(panel1);
            generateCustomerID();
            cstmrText.Focus();
            cstmrText.Text = "";
            cityText.Text = "";
            addressText.Text = "";
            noText.Text = "";
            
        }

        private void customer_Load(object sender, EventArgs e)
        {
            Main1.disable(panel1);
        }

        private void search()
        {
            pr.conn.Open();
            OleDbDataAdapter oda = new OleDbDataAdapter("select * from customer WHERE CSTMR_NAME LIKE '%" + searchText.Text.ToLower() + "%'", pr.conn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            customergv.DataSource = dt;
            pr.conn.Close();
        }

        private void customergv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (customergv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                Main1.enable(panel1);
                circularButton1.Enabled = false;
                circularButton2.Enabled = true;
                circularButton3.Enabled = true;
                customergv.CurrentCell.Selected = true;
                idText.Text = customergv.Rows[e.RowIndex].Cells["cid"].FormattedValue.ToString();
                cstmrText.Text = customergv.Rows[e.RowIndex].Cells["cname"].FormattedValue.ToString();
                cityText.Text = customergv.Rows[e.RowIndex].Cells["ccity"].FormattedValue.ToString();
                addressText.Text = customergv.Rows[e.RowIndex].Cells["caddress"].FormattedValue.ToString();
                noText.Text = customergv.Rows[e.RowIndex].Cells["cno"].FormattedValue.ToString();
            }
        }

        //--------------------Validate Number---------------

        private void authenticateNo()
        {
            Regex regex = new Regex(@"^((\+92)?(0092)?(92)?(0)?)(3)([0-9]{9})$");
            Match match = regex.Match(noText.Text);
            if (match.Success)
            {
                addressText.Focus();
            }
            else
            {
                MessageBox.Show("Enter a valid Mobile Number", "Error");
                noText.Focus();
            }
        }

        private void circularButton3_Click(object sender, EventArgs e)
        {
            updatebtn();
            showdata(customergv, cid, cname, caddress, cno, ccity);
        }

        private void circularButton1_Click(object sender, EventArgs e)
        {
            savebtn();
            showdata(customergv, cid, cname, caddress, cno, ccity);
        }

        private void circularButton2_Click(object sender, EventArgs e)
        {
            deletebtn();
            showdata(customergv, cid, cname, caddress, cno, ccity);
        }

        private void circularButton5_Click(object sender, EventArgs e)
        {
            showdata(customergv,cid,cname,caddress,cno,ccity);
            searchText.Focus();
            clearText();
            customergv.AllowUserToAddRows = false;
        }

        private void searchText_TextChange(object sender, EventArgs e)
        {
            search();
        }

        private void noText_Leave(object sender, EventArgs e)
        {
            authenticateNo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main1.disable(panel1);
            clearText();
        }

        private void cstmrText_Leave(object sender, EventArgs e)
        {
            Main1.alphabetCheck(cstmrText);
        }
    }
}
