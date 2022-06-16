using AutoStore.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
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
            pr.conn.Open();
            OleDbDataAdapter oda = new OleDbDataAdapter(@"SELECT p.id, p.name, s.quantity,((p.Price * 20)/100 + p.Price) AS Price from products p, 
            SALEINVOICE si, sales s where si.ID = " + saleID + " and s.SALINVOICE_ID = si.ID and p.id = s.product_id", pr.conn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            invoiceGV.DataSource = dt;
            pr.conn.Close();
            invoiceGV.Columns["id"].Visible = false;
            invoiceGV.Columns["Price"].Visible = false;
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
                nameText.Text = invoiceGV.Rows[e.RowIndex].Cells["NAME"].FormattedValue.ToString();
                saleQuantity.Text = invoiceGV.Rows[e.RowIndex].Cells["quantity"].FormattedValue.ToString();
                generateSRID();
            }
            quantityText.Focus();
        }

        //------------------ Get stock---------
        int productstockcount;
        private int getProductQuantity()
        {
            int proID = Convert.ToInt32(proID1.Text);
            string query = "select QUANTITY from stock where PRODUCT_ID = " + proID + "";
            OleDbCommand cmd = new OleDbCommand(query, pr.conn);
            pr.conn.Open();
            productstockcount = Convert.ToInt32(cmd.ExecuteScalar());
            pr.conn.Close();
            return productstockcount;
        }

        //------------------ Update Stock---------
        private void updateStock(int quan, int proID, int quantity)
        {
            quan += quantity;
            OleDbCommand cmd = new OleDbCommand("update stock s set s.QUANTITY = " + quan + "where s.PRODUCT_ID = " + proID + "", pr.conn);
            pr.conn.Open();
            cmd.ExecuteNonQuery();
            pr.conn.Close();
        }


        //-----------------------Generate ID---------------

        int returnID;
        private int getSaleReturnID()
        {
            OleDbCommand command = new OleDbCommand("SELECT id FROM (SELECT s.id FROM SALEreturn s ORDER BY s.id DESC) WHERE ROWNUM = 1", pr.conn);
            pr.conn.Open();
            returnID = Convert.ToInt32(command.ExecuteScalar());
            pr.conn.Close();
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
            int proQuantity, Amount, returnAmount, returnQuantity, addAmount, addQuantity;
            int SIID = (int)selectInvoice.SelectedValue;
            int proID = Convert.ToInt32(proID1.Text);
            proQuantity = getProQuantity();
            Amount = getAmount();
            returnAmount = Convert.ToInt32(amountText.Text);
            returnQuantity = Convert.ToInt32(quantityText.Text);
            addAmount = Amount - returnAmount;
            addQuantity = proQuantity - returnQuantity;

            OleDbCommand cmd = new OleDbCommand("update sales s set s.QUANTITY = " + addQuantity + ", s.total = "+addAmount+" where s.PRODUCT_ID = " + proID + "and s.salinvoice_id = "+SIID+"", pr.conn);
            pr.conn.Open();
            cmd.ExecuteNonQuery();
            pr.conn.Close();

        }

        //---------------------- Get Sale Invoice Values -------
        private int getProQuantity()
        {
            int SIID = (int)selectInvoice.SelectedValue;
            int proID = Convert.ToInt32(proID1.Text);
            string query = "Select quantity from sales where salinvoice_id = " + SIID + " and product_id = " + proID + " ";
            OleDbCommand cmd = new OleDbCommand(query, pr.conn);
            pr.conn.Open();
            int squantity = Convert.ToInt32(cmd.ExecuteScalar());
            pr.conn.Close();
            return squantity;
        }


        private int getAmount()
        {
            int SIID = (int)selectInvoice.SelectedValue;
            int proID = Convert.ToInt32(proID1.Text);
            string query = "Select total from sales where salinvoice_id = " + SIID + " and product_id = " + proID + " ";
            OleDbCommand cmd = new OleDbCommand(query, pr.conn);
            pr.conn.Open();
            int sTotal = Convert.ToInt32(cmd.ExecuteScalar());
            pr.conn.Close();
            return sTotal;
        }


        //-------------- Add Sale Return -----
        private void insert()
        {
            int SIID = (int)selectInvoice.SelectedValue;
            pr.conn.Open();
            OleDbCommand cmd = new OleDbCommand("insert into salereturn values('" + idText.Text + "'," + SIID + ",'" +
                proID1.Text + "','" + quantityText.Text + "',sysdate,'" + amountText.Text + "')", pr.conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Sale Return successfully");
            pr.conn.Close();
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
}
