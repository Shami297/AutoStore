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
using Oracle.ManagedDataAccess.Client;
using Dapper;

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
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                ORCL.Execute("insert into customer values('" + idText.Text + "','" +cstmrText.Text + "','" + cityText.Text + "','" + noText.Text + "','" +addressText.Text + "')");
                MessageBox.Show("New Customer Added successfully");
                clearText();
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
                ORCL.Execute("update customer set cstmr_name = '" + cstmrText.Text + "', cstmr_city = '" + cityText.Text + "', cstmr_address = '" + addressText.Text + "', cstmr_no = '" + noText.Text + "' where cstmr_id = '" + idText.Text + "'");
                MessageBox.Show("Customer Updated successfully");
                clearText();
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
                ORCL.Execute("delete from customer where cstmr_id = '" + idText.Text + "'");
                MessageBox.Show("Customer Deleted Successfully");
                clearText();
                Main1.disable(panel1);
            }
            catch { }
            finally { ORCL.Dispose(); }
        }

        private void showdata(DataGridView customergv)
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                var custmr = ORCL.Query<custView>("select cstmr_id as ID, cstmr_name as Name, cstmr_address as Address,cstmr_city as City, cstmr_no as Phone  from customer").ToList();

                customergv.AutoGenerateColumns = true;
                customergv.DataSource = null;
                customergv.DataSource = custmr;

                Main1.disable(panel1);
            }
            catch { }
            finally { ORCL.Dispose(); }
        }

        //---------------------------Generate ID----------------


        int customerID;
        private int getCustomerID()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                customerID = ORCL.Query<int>("SELECT CSTMR_ID FROM (SELECT c.CSTMR_ID FROM Customer c ORDER BY c.CSTMR_ID DESC) WHERE ROWNUM = 1").FirstOrDefault();
            }
            catch { }
            finally { ORCL.Dispose(); }
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
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                var custmr = ORCL.Query<custView>("select cstmr_id as ID, cstmr_name as Name, cstmr_address as Address,cstmr_city as City, cstmr_no as Phone from customer WHERE CSTMR_NAME LIKE '%" + searchText.Text.ToLower() + "%'").ToList();
                customergv.AutoGenerateColumns = true;
                customergv.DataSource = null;
                customergv.DataSource = custmr;
            }
            catch { }
            finally { ORCL.Dispose(); }
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
                idText.Text = customergv.Rows[e.RowIndex].Cells["ID"].FormattedValue.ToString();
                cstmrText.Text = customergv.Rows[e.RowIndex].Cells["Name"].FormattedValue.ToString();
                cityText.Text = customergv.Rows[e.RowIndex].Cells["City"].FormattedValue.ToString();
                addressText.Text = customergv.Rows[e.RowIndex].Cells["Address"].FormattedValue.ToString();
                noText.Text = customergv.Rows[e.RowIndex].Cells["Phone"].FormattedValue.ToString();
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
            showdata(customergv);
        }

        private void circularButton1_Click(object sender, EventArgs e)
        {
            savebtn();
            showdata(customergv);
        }

        private void circularButton2_Click(object sender, EventArgs e)
        {
            deletebtn();
            showdata(customergv);
        }

        private void circularButton5_Click(object sender, EventArgs e)
        {
            showdata(customergv);
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

    public class custView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
