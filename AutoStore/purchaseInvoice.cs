using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoStore.Logic;
using System.Transactions;

namespace AutoStore
{
    public partial class purchaseInvoice : Form
    {
        Connection pr = new Connection();

        public purchaseInvoice()
        {
            InitializeComponent();
        }


        private void clearText()
        {
            producttext.Text = "";
            proPrice.Text = "";
            quantitytxt.Text = "";
            vendorText.Text = "";
        }

        private void filProduct()
        {
            ProductManager productManager = new ProductManager();
            
            producttext.DisplayMember = "Name";
            producttext.ValueMember = "ID";

            producttext.DataSource = productManager.GetProducts();

        }

        private void filvendor()
        {
            VendorManager vendorManager = new VendorManager();
            
            vendorText.DisplayMember = "Name";
            vendorText.ValueMember = "ID";
            vendorText.DataSource = vendorManager.GetVendors();

        }

        

        private void producttext_Validated_1(object sender, EventArgs e)
        {
        }

        private void loadForm()
        {
            filProduct();
            filvendor();
            producttext.Text = "Select....";
            vendorText.Text = "Select....";
            proPrice.Text = "";
            productGV.AllowUserToAddRows = false;
            Main1.disable(insertDB);
            circularButton5.Enabled = false;
        }
        private void purchaseInvoice_Load(object sender, EventArgs e)
        {
            loadForm();
        }

        private void afterSave()
        {
            productGV.Rows.Clear();
            productGV.Refresh();
            idText.Text = "";
            dateText.Text = "";
            vendorText.Text = "";
            totalLabel.Text = "000";
        }

        
        private void circularButton3_Click(object sender, EventArgs e)
        {
            if(productGV.Rows.Count == 0)
            {
                MessageBox.Show("Please Add Products in Cart", "Error");
                purchaseInvoice invoice = new purchaseInvoice();
                invoice.Refresh();
            }
            else
            {
                Main1.enable(insertDB);
                Main1.disable(panel3);
                idText.Focus();
                circularButton5.Enabled = true;
                circularButton3.Enabled = false;
                generatePIID();
            }
        }
        int purchaceinvoiceid;
        private int insertpurchaseinvoice()
        {
            int vendorsID = (int)vendorText.SelectedValue;
            OleDbCommand cmd = new OleDbCommand("insert into PURCHASEINVOICE values('" + idText.Text + "','" +
              dateText.Text + "'," + vendorsID + ")", pr.conn);
            pr.conn.Open();
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT id FROM (SELECT p.id FROM purchaseinvoice p ORDER BY p.id DESC) WHERE ROWNUM = 1";
            purchaceinvoiceid = Convert.ToInt32(cmd.ExecuteScalar());
            pr.conn.Close();
            return purchaceinvoiceid;
        }
        
        int coun = 0;
        private int insertpurchaseinvoicedetails()
        {
            int purID, proID, quantity, total;
            purID = purchaceinvoiceid;
            foreach (DataGridViewRow row in productGV.Rows)
            {
                proID = quantity = total = 0;
                proID = Convert.ToInt32(row.Cells["idgv"].Value.ToString());
                quantity = Convert.ToInt32(row.Cells["qugv"].Value.ToString());
                total = Convert.ToInt32(row.Cells["totgv"].Value.ToString());
                OleDbCommand cmd = new OleDbCommand("insert into PURCHASES values(" + purID + "," + proID + "," + quantity + "," + total + ")", pr.conn);
                pr.conn.Open();
                cmd.ExecuteNonQuery();
                pr.conn.Close();
                int q = 0;
                object ob = getProductQuantity(proID);
                if (ob != null)
                {
                    q = Convert.ToInt32(ob);
                    updateStock(q, proID, quantity);
                }
                else
                {
                    insertStock(proID, quantity);
                }
                coun++;
            }
            return coun;
        }
        object productstockcount ;
        private object getProductQuantity(int proID)
        {
            string query = "select QUANTITY from stock where PRODUCT_ID = "+proID+"";
            OleDbCommand cmd = new OleDbCommand(query, pr.conn);
            pr.conn.Open();
            productstockcount =cmd.ExecuteScalar();
            pr.conn.Close();
            return productstockcount;
        }

        //------------------------Add to Stock--------------

        private void insertStock(int proID, int quantity)
        {
            OleDbCommand cmd = new OleDbCommand("insert into stock values(" + proID + "," + quantity + ")", pr.conn);
            pr.conn.Open();
            cmd.ExecuteNonQuery();
            pr.conn.Close();
        }

        private void updateStock(int quan, int proID, int quantity)
        {
            quan += quantity;
            OleDbCommand cmd = new OleDbCommand("update stock s set s.QUANTITY = " + quan + "where s.PRODUCT_ID = " + proID + "", pr.conn);
            pr.conn.Open();
            cmd.ExecuteNonQuery();
            pr.conn.Close();   
        }

        private void saveBtn()
        {
            if (productGV.Rows.Count > 0)
            {
                insertpurchaseinvoice();
                insertpurchaseinvoicedetails();
                afterSave();
                if (coun > 0)
                {
                    MessageBox.Show("Purchaseinvoice Successfully Generated", "Success");
                }
                else
                {
                    MessageBox.Show("Unable to Generated Purchaseinvoice", "Error");
                }
            }

        }

        //------------------------------Add To Cart-------------------------------


        private void aftercart()
        {
            
            int total, grosstot;
            total = Convert.ToInt32(totalLabel.Text);
            grosstot = Convert.ToInt32(grossLabel.Text);
            grosstot = grosstot + total;
            grossLabel.Text = grosstot.ToString("#####");
            producttext.Focus();
            proPrice.Text = "";
            quantitytxt.Text = "";
            totalLabel.Text = "00";
            producttext.Text = "Select....";
        }

        private void addToCart()
        {
            if (quantitytxt.Text == "")
            {
                MessageBox.Show("Please Select Product", "Error");
                proPrice.Text = "";
                quantitytxt.Text = "";
                producttext.Text = "Select.....";
                producttext.Focus();
            }
            else
            {
                int productID = (int)producttext.SelectedValue;
                foreach (DataGridViewRow row in productGV.Rows)
                {
                    int addedProId = Convert.ToInt32(row.Cells["idgv"].Value.ToString());
                    if (productID == addedProId)
                    {
                        int total = Convert.ToInt32(row.Cells["totgv"].Value.ToString());
                        int quantity = Convert.ToInt32(row.Cells["qugv"].Value.ToString());
                        int quan = Convert.ToInt32(quantitytxt.Text);
                        quan = quantity + quan;
                        total += Convert.ToInt32(totalLabel.Text);
                        row.Cells["qugv"].Value = quan.ToString();
                        row.Cells["totgv"].Value = total.ToString();
                        grossLabel.Text = (Convert.ToInt32(totalLabel.Text) + Convert.ToInt32(grossLabel.Text)).ToString();
                        goto found;
                    }
                }

                int selectedProductID = (int)producttext.SelectedValue;
                productGV.Rows.Add(selectedProductID, producttext.Text, proPrice.Text, quantitytxt.Text, totalLabel.Text);
                aftercart();
                found:
                producttext.Focus();
                proPrice.Text = "";
                quantitytxt.Text = "";
                totalLabel.Text = "00";
                producttext.Text = "Select....";
            }
        }

        private void addtocart_Click(object sender, EventArgs e)
        {
            addToCart();
        }

        private void producttext_TextChanged(object sender, EventArgs e)
        {
            int selectedProductID = (int)producttext.SelectedValue;
            pr.conn.Open();
            string query = "select p.Price from products p where p.id = " + selectedProductID + "";
            OleDbCommand command = new OleDbCommand(query, pr.conn);
            OleDbDataReader odr = command.ExecuteReader();
            while (odr.Read())
            {
                proPrice.Text = odr.GetValue(0).ToString();
            }
            pr.conn.Close();
            quantitytxt.Focus();
        }

        private void quantitytxt_TextChange(object sender, EventArgs e)
        {
            if (quantitytxt.Text != "")
            {
                int parsedValue;
                if (!int.TryParse(quantitytxt.Text, out parsedValue))
                {
                    MessageBox.Show("This is a number only field");
                    quantitytxt.Text = "";
                    return;
                }
                else
                {
                    int price, quantity, total;
                    price = Convert.ToInt32(proPrice.Text);
                    quantity = Convert.ToInt32(quantitytxt.Text);
                    total = price * quantity;
                    totalLabel.Text = total.ToString("######");
                }
                
            }
        }

        //---------------------- Generate ID-------------

        int invoiceID;
        private int getPurchaseInvoiceID()
        {
            OleDbCommand command = new OleDbCommand("SELECT id FROM (SELECT p.id FROM purchaseinvoice p ORDER BY p.id DESC) WHERE ROWNUM = 1", pr.conn);
            pr.conn.Open();
            invoiceID = Convert.ToInt32(command.ExecuteScalar());
            pr.conn.Close();
            return invoiceID;
        }

        private void generatePIID()
        {
            int piid = getPurchaseInvoiceID();
            piid += 1;
            idText.Text = piid.ToString();
            idText.Enabled = false;
        }


        private void productGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if(e.ColumnIndex == 5)
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

        private void productGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void circularButton5_Click(object sender, EventArgs e)
        {
            saveBtn();
            loadForm();
            circularButton3.Enabled = true;
            idText.Text = "";
            grossLabel.Text = "000";
            producttext.Focus();
            Main1.enable(panel3);
            //this.Close();
            //purchaseInvoice invoice = new purchaseInvoice();
            //invoice.Show();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            DashBoard dash = new DashBoard();
            this.Close();
            dash.Show();
        }

        private void circularButton4_Click(object sender, EventArgs e)
        {
            purchaseInvoiceDetail purchaseInvoiceDetail = new purchaseInvoiceDetail();
            this.Close();
            purchaseInvoiceDetail.Show();
        }

        private void quantitytxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                addToCart();
            }
        }
    }
}
