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
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                var returns = ORCL.Query<pReturnView>("SELECT p.id, p.name, pu.quantity, p.PRICE from products p,PURCHASEINVOICE pi, purchases pu where pi.ID = " + purchaseID + " and pu.PURINVOICE_ID = pi.ID and p.id = pu.product_id");
                
                invoiceGV.AutoGenerateColumns = true;
                invoiceGV.DataSource = null;
                invoiceGV.DataSource = returns;
                invoiceGV.Columns["ID"].Visible = false;
            }
            catch { }
            finally { ORCL.Dispose(); }
            
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
                quan -= quantity;
                ORCL.Execute("update stock s set s.QUANTITY = " + quan + "where s.PRODUCT_ID = " + proID + "");
            }
            catch { }
            finally { ORCL.Dispose(); }
        }

        //--------------------- Update Purchase Invoice  ----------

        private void updatePI()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
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
                ORCL.Execute("update purchases p set p.QUANTITY = " + addQuantity + ", p.total = " + addAmount + " where p.PRODUCT_ID = " + proID + "and p.PURINVOICE_ID = " + PIID + "");
            }
            catch { }
            finally { ORCL.Dispose(); }
        }



        //---------------------- Get Invoice Values  ----------------

        private int getProQuantity()
        {
            int squantity = 0;
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                
                int PIID = (int)selectInvoice.SelectedValue;
                int proID = Convert.ToInt32(proID1.Text);
                string query = "Select quantity from purchases where PURINVOICE_ID = " + PIID + " and product_id = " + proID + " ";
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
                int PIID = (int)selectInvoice.SelectedValue;
                int proID = Convert.ToInt32(proID1.Text);
                string query = "Select total from purchases where PURINVOICE_ID = " + PIID + " and product_id = " + proID + " ";
                sTotal = ORCL.Query<int>(query).FirstOrDefault();
            }
            catch { }
            finally { ORCL.Dispose(); }
            return sTotal;
        }



        //-----------------------Generate ID---------------

        int returnID;
        private int getPurchaseReturnID()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                returnID = ORCL.Query<int>("SELECT id FROM (SELECT p.id FROM purchasereturn p ORDER BY p.id DESC) WHERE ROWNUM = 1").FirstOrDefault();
            }
            catch { }
            finally { ORCL.Dispose(); }
            return returnID;
        }

        private void generateSRID()
        {
            int siid = getPurchaseReturnID();
            siid += 1;
            idText.Text = siid.ToString();
        }


        //-------------- Add Purchase Return-----
        private void insert()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                int PIID = (int)selectInvoice.SelectedValue;
                ORCL.Execute("insert into purchasereturn values('" + idText.Text + "'," + PIID + ",'" +proID1.Text + "','" + quantityText.Text + "',sysdate,'" + amountText.Text + "')");
                MessageBox.Show("Purchase Return successfully");
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
            if(quantityText.Text == string.Empty)
            {
                MessageBox.Show("Please Add Product Quantity");
                return ;
            }
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
    public class pReturnView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
    }
}
