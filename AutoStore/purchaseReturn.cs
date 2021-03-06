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
    public partial class purchaseReturn : Form
    {
        Connection pr = new Connection();
        public purchaseReturn()
        {
            InitializeComponent();
            price1.Hide();
        }

        private void loadInvoice()
        {
            int mon = this.datePicker.Value.Month;
            int year = this.datePicker.Value.Year;

            VendorManage vendorManage = new VendorManage();
            selectInvoice.DataSource = vendorManage.GetInvoice(mon, year);
            selectInvoice.DisplayMember = "Name";
            selectInvoice.ValueMember = "ID";
            invoiceGV.AllowUserToAddRows = false;
        }

        private void purchaseReturn_Load(object sender, EventArgs e)
        {
            loadInvoice();
            proID1.Hide();
            selectInvoice.Text = "Select.......";
        }

        private void datePicker_onValueChanged(object sender, EventArgs e)
        {
            loadInvoice();
        }

        private void viewBtn_Click(object sender, EventArgs e)
        {
            showPurchases();
            invoiceGV.AllowUserToAddRows = false;
        }

        private void showPurchases()
        {
            int purchaseID = (int)selectInvoice.SelectedValue;
            pr.conn.Open();
            OleDbDataAdapter oda = new OleDbDataAdapter(@"SELECT p.id, p.name, pu.quantity, p.PRICE from products p, 
            PURCHASEINVOICE pi, purchases pu where pi.ID = " + purchaseID + " and pu.PURINVOICE_ID = pi.ID and p.id = pu.product_id", pr.conn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            invoiceGV.DataSource = dt;
            pr.conn.Close();
            invoiceGV.Columns["id"].Visible = false;
            invoiceGV.Columns["Price"].Visible = false;
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
            quan -= quantity;
            OleDbCommand cmd = new OleDbCommand("update stock s set s.QUANTITY = " + quan + "where s.PRODUCT_ID = " + proID + "", pr.conn);
            pr.conn.Open();
            cmd.ExecuteNonQuery();
            pr.conn.Close();
        }

        //--------------------- Update Purchase Invoice  ----------

        private void updatePI()
        {
            int proQuantity, Amount, returnAmount, returnQuantity, addAmount, addQuantity;
            int PIID = (int)selectInvoice.SelectedValue;
            int proID = Convert.ToInt32(proID1.Text);
            proQuantity = getProQuantity();
            Amount = getAmount();
            returnAmount = Convert.ToInt32(amountText.Text);
            returnQuantity = Convert.ToInt32(quantityText.Text);
            addAmount = Amount - returnAmount;
            addQuantity = proQuantity - returnQuantity;

            OleDbCommand cmd = new OleDbCommand("update purchases p set p.QUANTITY = " + addQuantity + ", p.total = " + addAmount + " where p.PRODUCT_ID = " + proID + "and p.PURINVOICE_ID = " + PIID + "", pr.conn);
            pr.conn.Open();
            cmd.ExecuteNonQuery();
            pr.conn.Close();
        }



        //---------------------- Get Invoice Values  ----------------

        private int getProQuantity()
        {
            int PIID = (int)selectInvoice.SelectedValue;
            int proID = Convert.ToInt32(proID1.Text);
            string query = "Select quantity from purchases where PURINVOICE_ID = " + PIID + " and product_id = " + proID + " ";
            OleDbCommand cmd = new OleDbCommand(query, pr.conn);
            pr.conn.Open();
            int squantity = Convert.ToInt32(cmd.ExecuteScalar());
            pr.conn.Close();
            return squantity;
        }

        private int getAmount()
        {
            int PIID = (int)selectInvoice.SelectedValue;
            int proID = Convert.ToInt32(proID1.Text);
            string query = "Select total from purchases where PURINVOICE_ID = " + PIID + " and product_id = " + proID + " ";
            OleDbCommand cmd = new OleDbCommand(query, pr.conn);
            pr.conn.Open();
            int sTotal = Convert.ToInt32(cmd.ExecuteScalar());
            pr.conn.Close();
            return sTotal;
        }



        //-----------------------Generate ID---------------

        int returnID;
        private int getSaleReturnID()
        {
            OleDbCommand command = new OleDbCommand("SELECT id FROM (SELECT p.id FROM purchasereturn p ORDER BY p.id DESC) WHERE ROWNUM = 1", pr.conn);
            pr.conn.Open();
            returnID = Convert.ToInt32(command.ExecuteScalar());
            pr.conn.Close();
            return returnID;
        }

        private void generateSRID()
        {
            int siid = getSaleReturnID();
            siid += 1;
            idText.Text = siid.ToString();
        }


        //-------------- Add Purchase Return-----
        private void insert()
        {
            int PIID = (int)selectInvoice.SelectedValue;
            pr.conn.Open();
            OleDbCommand cmd = new OleDbCommand("insert into purchasereturn values('" + idText.Text + "'," + PIID + ",'" +
                proID1.Text + "','" + quantityText.Text + "',sysdate,'" + amountText.Text + "')", pr.conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Purchase Return successfully");
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
                    amountText.Text = "";
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
                        MessageBox.Show("Out of Purchase Quantity");
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
            updatePI();
            int proid = Convert.ToInt32(proID1.Text);
            int quantity = Convert.ToInt32(quantityText.Text);
            int quan = getProductQuantity();
            updateStock(quan, proid, quantity);
            this.InitializeComponent();
            this.Controls.Clear();
            this.Refresh();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            DashBoard board = new DashBoard();
            board.Show();
        }
    }
}
