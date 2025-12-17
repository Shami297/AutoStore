using AutoStore.Logic;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoStore
{
    public partial class SaleReturn : Form
    {
        Connection pr = new Connection();
        public SaleReturn()
        {
            InitializeComponent();
            price1.Hide();
        }

        private void loadInvoice()
        {
            int mon = this.datePicker.Value.Month;
            int year = this.datePicker.Value.Year;

            CustomerManage customer = new CustomerManage();
            selectInvoice.DataSource = customer.GetInvoice(mon, year);
            selectInvoice.DisplayMember = "Name";
            selectInvoice.ValueMember = "ID";
            invoiceGV.AllowUserToAddRows = false;
        }

        private void showSales()
        {
            int saleID = (int)selectInvoice.SelectedValue;
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                var sales = ORCL.Query<sReturnView>("SELECT p.id, p.name, s.quantity,((p.Price * 20)/100 + p.Price) AS Price from products p,SALEINVOICE si, sales s where si.ID = " + saleID + " and s.SALINVOICE_ID = si.ID and p.id = s.product_id");
                
                invoiceGV.AutoGenerateColumns = true;
                invoiceGV.DataSource = null;
                invoiceGV.DataSource = sales;
                invoiceGV.Columns["ID"].Visible = false;
            }
            catch { }
            finally { ORCL.Dispose(); }
            
        }

        private void datePicker_onValueChanged(object sender, EventArgs e)
        {
            loadInvoice();
        }

        private void SaleReturn_Load(object sender, EventArgs e)
        {
            loadInvoice();
            proID1.Hide();
            selectInvoice.Text = "Select.......";
        }

        private void viewBtn_Click(object sender, EventArgs e)
        {
            showSales();
            invoiceGV.AllowUserToAddRows = false;
        }

        private void invoiceGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (invoiceGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                invoiceGV.CurrentCell.Selected = true;
                proID1.Text = invoiceGV.Rows[e.RowIndex].Cells["ID"].FormattedValue.ToString();
                price1.Text = invoiceGV.Rows[e.RowIndex].Cells["Price"].FormattedValue.ToString();
                nameText.Text = invoiceGV.Rows[e.RowIndex].Cells["Name"].FormattedValue.ToString();
                saleQuantity.Text = invoiceGV.Rows[e.RowIndex].Cells["Quantity"].FormattedValue.ToString();
                generateSRID();
            }
            quantityText.Focus();
        }

        //------------------ Get stock---------
        int productstockcount;
        private int getProductQuantity()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                int proID = Convert.ToInt32(proID1.Text);
                string query = "select QUANTITY from stock where PRODUCT_ID = " + proID + "";
                productstockcount = ORCL.Query<int>(query).FirstOrDefault();
            }
            catch { }
            finally { ORCL.Dispose(); }
            return productstockcount;
        }

        //------------------ Update Stock---------
        private void updateStock(int quan, int proID, int quantity)
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                quan += quantity;
                ORCL.Execute("update stock s set s.QUANTITY = " + quan + "where s.PRODUCT_ID = " + proID + "");
            }
            catch { }
            finally { ORCL.Dispose(); }
        }


        //-----------------------Generate ID---------------

        int returnID;
        private int getSaleReturnID()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                returnID = ORCL.Query<int>("SELECT id FROM (SELECT p.id FROM salereturn p ORDER BY p.id DESC) WHERE ROWNUM = 1").FirstOrDefault();
            }
            catch { }
            finally { ORCL.Dispose(); }
            return returnID;
        }

        private void generateSRID()
        {
            int srid = getSaleReturnID();
            srid += 1;
            idText.Text = srid.ToString();
        }

        //------------------- Update Sale Invoice --------

        private void updateSI()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {

                int proQuantity, Amount, returnAmount, returnQuantity, addAmount, addQuantity;
                int SIID = (int)selectInvoice.SelectedValue;
                int proID = Convert.ToInt32(proID1.Text);
                proQuantity = getProQuantity();
                Amount = getAmount();
                returnAmount = Convert.ToInt32(amountText.Text);
                returnQuantity = Convert.ToInt32(quantityText.Text);
                addAmount = Amount - returnAmount;
                addQuantity = proQuantity - returnQuantity;
                ORCL.Execute("update sales s set s.QUANTITY = " + addQuantity + ", s.total = "+addAmount+" where s.PRODUCT_ID = " + proID + "and s.salinvoice_id = "+SIID+"");
            }
            catch { }
            finally { ORCL.Dispose(); }

        }

        //---------------------- Get Sale Invoice Values -------
        private int getProQuantity()
        {
            int squantity = 0;
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                int SIID = (int)selectInvoice.SelectedValue;
                int proID = Convert.ToInt32(proID1.Text);
                string query = "Select quantity from sales where salinvoice_id = " + SIID + " and product_id = " + proID + " ";
                squantity = ORCL.Query<int>(query).FirstOrDefault();
            }
            catch { }
            finally { ORCL.Dispose(); }
            return squantity;
        }


        private int getAmount()
        {
            int sTotal = 0;
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                int SIID = (int)selectInvoice.SelectedValue;
                int proID = Convert.ToInt32(proID1.Text);
                string query = "Select total from sales where salinvoice_id = " + SIID + " and product_id = " + proID + " ";
                sTotal = ORCL.Query<int>(query).FirstOrDefault();
            }
            catch { }
            finally { ORCL.Dispose(); }
            return sTotal;
        }


        //-------------- Add Sale Return -----
        private void insert()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                int SIID = (int)selectInvoice.SelectedValue;
                ORCL.Execute("insert into salereturn values('" + idText.Text + "'," + SIID + ",'" +proID1.Text + "','" + quantityText.Text + "',sysdate,'" + amountText.Text + "')");
                MessageBox.Show("Sale Return successfully");
            }
            catch { }
            finally { ORCL.Dispose(); }
        }


        private void quantityText_TextChange(object sender, EventArgs e)
        {
            if (quantityText.Text != "")
            {
                int parsedValue;
                if (!int.TryParse(quantityText.Text, out parsedValue))
                {
                    MessageBox.Show("This is a number only field");
                    quantityText.Text = "";
                    return;
                }
                else
                {
                    int quantity, saleQuan, amount;
                    saleQuan = Convert.ToInt32(saleQuantity.Text);
                    quantity = Convert.ToInt32(quantityText.Text);
                    amount = Convert.ToInt32(price1.Text);
                    if (quantity > saleQuan)
                    {
                        MessageBox.Show("Out of Sale Quantity");
                        quantityText.Text = "";
                        amountText.Text = "";
                    }
                    else
                    {
                        amountText.Text = (quantity * amount).ToString();
                    }
                }
            }
            else
            {
                amountText.Text = "";
            }
        }

        private void proceed_Click(object sender, EventArgs e)
        {
            insert();
            updateSI();
            int proid = Convert.ToInt32(proID1.Text);
            int quantity = Convert.ToInt32(quantityText.Text);
            int quan = getProductQuantity();
            updateStock(quan, proid, quantity);
            this.Controls.Clear();
            this.InitializeComponent();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            DashBoard board = new DashBoard();
            board.Show();
        }
    }

    public class sReturnView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
    }
}
