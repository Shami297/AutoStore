using AutoStore.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using System.Data.OleDb;

namespace AutoStore
{
    public partial class saleInvoice : Form
    {
        Connection pr = new Connection();
        public saleInvoice()
        {
            InitializeComponent();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            DashBoard dash = new DashBoard();
            this.Close();
            dash.Show();
        }

        private void filProduct()
        {
            ProductSaleManager saleManager = new ProductSaleManager();

            productText.DisplayMember = "Name";
            productText.ValueMember = "ID";

            productText.DataSource = saleManager.GetSaleProducts();

        }

        private void filCustomer()
        {
            customerManager customer = new customerManager();

            customerText.DisplayMember = "Name";
            customerText.ValueMember = "ID";

            customerText.DataSource = customer.GetCustomers();

        }
        private void onLoad()
        {
            filProduct();
            filCustomer();
            Main1.disable(insertInvoice);
            productGV.AllowUserToAddRows = false;
            Main1.enable(addProduct);
            customerText.Text = "Select....";
            productText.Text = "Select....";
            proPrice.Text = "";
            stockQuantity.Text = "00";
            totalLabel.Text = "000";
            saveBtn.Enabled = false;
        }
        private void saleInvoice_Load(object sender, EventArgs e)
        {
            onLoad();
            stockQuantity.Text = "00";
        }

        
        private void productGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (e.ColumnIndex == 5)
                {
                    DataGridViewRow row = productGV.Rows[e.RowIndex];
                    int grostot = 0;
                    grostot = Convert.ToInt32(grossLabel.Text);
                    int tot = 0;
                    tot = Convert.ToInt32(row.Cells["totgv"].Value.ToString());
                    int grostotal = 0;
                    grostotal = grostot - tot;
                    grossLabel.Text = grostotal.ToString();
                    productGV.Rows.Remove(row);
                }
            }
        }

        int saleInvoiceID;
        private int insertSaleInvoice()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                int vendorsID = (int)customerText.SelectedValue;
                ORCL.Execute("insert into SALEINVOICE values('" + idText.Text + "','" +datePick.Text + "'," + vendorsID + ")");
                saleInvoiceID = ORCL.Query<int>("SELECT id FROM (SELECT s.id FROM SALEINVOICE s ORDER BY s.id DESC) WHERE ROWNUM = 1").FirstOrDefault();

            }
            catch { }
            finally { ORCL.Dispose(); }
            return saleInvoiceID;
        }

        int coun = 0;
        private int insertSaleinvoicedetails()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                int purID, proID, quantity, total;
                purID = saleInvoiceID;
                foreach (DataGridViewRow row in productGV.Rows)
                {
                    proID = quantity = total = 0;
                    proID = Convert.ToInt32(row.Cells["idgv"].Value.ToString());
                    quantity = Convert.ToInt32(row.Cells["qugv"].Value.ToString());
                    total = Convert.ToInt32(row.Cells["totgv"].Value.ToString());
                    ORCL.Execute("insert into sales values(" + purID + "," + proID + "," + quantity + "," + total + ")");
                    int num = 0;
                    num = getProductQuantity(proID);
                    if (num > 0)
                    {
                        updateStock(num, proID, quantity);
                    }
                    else
                    {
                        MessageBox.Show("Stock is Not Available", "OutOfStock");
                    }
                    coun++;
                }

            }
            catch { }
            finally { ORCL.Dispose(); }
            
            return coun;
        }

        private void insertInDB()
        {
            if (productGV.Rows.Count > 0)
            {
                insertSaleInvoice();
                insertSaleinvoicedetails();
                if (coun > 0)
                {
                    MessageBox.Show("Saleinvoice Successfully Generated", "Success");
                }
                else
                {
                    MessageBox.Show("Unable to Generated Saleinvoice", "Error");
                }
            }

        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            insertInDB();
            onLoad();
            productGV.Rows.Clear();
            idText.Text = "";
            grossLabel.Text = "000";
            addBtn.Enabled = true;
            saveBtn.Enabled = false;
            productText.Focus();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (productGV.Rows.Count == 0)
            {
                MessageBox.Show("Please Add Products in Cart", "Error");
                saleInvoice invoice = new saleInvoice();
                invoice.Refresh();
            }
            else
            {
                Main1.enable(insertInvoice);
                Main1.disable(addProduct);
                idText.Focus();
                addBtn.Enabled = false;
                saveBtn.Enabled = true;
                generateSIID();
            }
        }

        private void quantityInStock()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                int selectedProductID = (int)productText.SelectedValue;
                string query = "select s.quantity from products p, stock s where p.id  = " + selectedProductID + "and" +" p.id = s.PRODUCT_ID";
                var quan = ORCL.Query<string>(query).FirstOrDefault();
                if (quan != null) { stockQuantity.Text = quan; }
            }
            catch { }
            finally { ORCL.Dispose(); }
        }

        private void productText_TextChanged_1(object sender, EventArgs e)
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                int selectedProductID = (int)productText.SelectedValue;
                string query = "select ((p.Price * 20)/100 + p.Price) from products p where p.id = " + selectedProductID + "";
                var PRI = ORCL.Query<string>(query).FirstOrDefault();
                if (PRI != null) { proPrice.Text = PRI; }
                quantityInStock();
                quantityText.Text = string.Empty;
                quantityText.Focus();
            }
            catch { }
            finally { ORCL.Dispose(); }
            
        }

        //-----------------------Generate ID---------------

        int invoiceID;
        private int getSaleInvoiceID()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                invoiceID = ORCL.Query<int>("SELECT id FROM (SELECT s.id FROM SALEINVOICE s ORDER BY s.id DESC) WHERE ROWNUM = 1").FirstOrDefault();
            }
            catch { }
            finally { ORCL.Dispose(); }
            return invoiceID;
        }

        private void generateSIID()
        {
            int siid = getSaleInvoiceID();
            siid += 1;
            idText.Text = siid.ToString();
            idText.Enabled = false;
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
                    int price, quantity, total;
                    int stock = Convert.ToInt32(stockQuantity.Text);
                    quantity = Convert.ToInt32(quantityText.Text);
                    if(quantity > stock)
                    {
                        MessageBox.Show("Stock is not Enough", "Out of Stock");
                        quantityText.Text = stockQuantity.Text;
                    }
                    else
                    {
                        price = Convert.ToInt32(proPrice.Text);
                        total = price * quantity;
                        totalLabel.Text = total.ToString("######");
                    }
                }
            }
        }

        //------------------------ Add to Cart----------------

        private void afterAddToCart()
        {

            int total, grosstot;
            total = Convert.ToInt32(totalLabel.Text);
            grosstot = Convert.ToInt32(grossLabel.Text);
            grosstot = grosstot + total;
            grossLabel.Text = grosstot.ToString("#####");
            productText.Focus();
            proPrice.Text = "";
            quantityText.Text = "";
            totalLabel.Text = "00";
            productText.Text = "Select....";
        }


        private void addToCart1()
        {
            if (proPrice.Text.Trim() == string.Empty || productText.Text == "Select.....")
            {
                MessageBox.Show("Please Select Product", "Error");
                quantityText.Text = "";
                proPrice.Text = "";
                //productText.Text = "Select.....";

            }
            else if (quantityText.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please Enter Quantity", "Error");
                quantityText.Focus();
            }
            else
            {
                int productID = (int)productText.SelectedValue;
                bool error = false;
                int maxQuan = Convert.ToInt32(stockQuantity.Text);
                foreach (DataGridViewRow row in productGV.Rows)
                {
                    int addedProId = Convert.ToInt32(row.Cells["idgv"].Value.ToString());
                    if (productID == addedProId)
                    {
                        int total = Convert.ToInt32(row.Cells["totgv"].Value.ToString());
                        int quantity = Convert.ToInt32(row.Cells["qugv"].Value.ToString());
                        int quan = Convert.ToInt32(quantityText.Text);
                        if (quantity >= maxQuan)
                        {
                            error = true;
                            break;
                        }
                        else if ((quantity + quan) > maxQuan)
                        {
                            quan = maxQuan - quantity;
                        }
                        quan = quantity + quan;
                        total += Convert.ToInt32(totalLabel.Text);
                        row.Cells["qugv"].Value = quan.ToString();
                        row.Cells["totgv"].Value = total.ToString();
                        grossLabel.Text = (Convert.ToInt32(totalLabel.Text) + Convert.ToInt32(grossLabel.Text)).ToString();
                        goto found;
                    }
                }
                if (error)
                {
                    MessageBox.Show("Maximum Quanity is already added in Cart!", "Error");
                    return;
                }
                int selectedProductID = (int)productText.SelectedValue;
                productGV.Rows.Add(selectedProductID, productText.Text, proPrice.Text, quantityText.Text, totalLabel.Text);
                afterAddToCart();
                found:
                productText.Focus();
                proPrice.Text = "";
                stockQuantity.Text = "00";
                quantityText.Text = "";
                totalLabel.Text = "00";
                productText.Text = "Select....";
            }
        }
        private void addToCart_Click(object sender, EventArgs e)
        {
            addToCart1();
        }

        // -----------  Updation In Stock-------------

        int productstockcount;
        private int getProductQuantity(int proID)
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                string query = "select QUANTITY from stock where PRODUCT_ID = " + proID + "";
                productstockcount = ORCL.Query<int>(query).FirstOrDefault();
            }
            catch { }
            finally { ORCL.Dispose(); }
            
            return productstockcount;
        }

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

        private void salesView_Click(object sender, EventArgs e)
        {
            SaleInvoiceDetail saleInvoiceDetail = new SaleInvoiceDetail();
            this.Close();
            saleInvoiceDetail.Show();
        }

        private void quantityText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                addToCart1();
            }   
        }
     
    }
}
