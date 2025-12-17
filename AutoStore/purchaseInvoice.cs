using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoStore.Logic;
using System.Transactions;

using Dapper;
using System.Windows.Controls;

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
            OracleConnection ORCL = Connection.GetConnection();
            int vendorsID = (int)vendorText.SelectedValue;
            try
            {
                ORCL.Execute("INSERT INTO PURCHASEINVOICE VALUES ('" + idText.Text + "', '" +dateText.Text + "'," + vendorsID + ")");
                purchaceinvoiceid = ORCL.Query<int>("SELECT id FROM (SELECT p.id FROM purchaseinvoice p ORDER BY p.id DESC) WHERE ROWNUM = 1").FirstOrDefault();
            }
            catch (Exception) { }
            finally { ORCL.Dispose(); }
            return purchaceinvoiceid;
        }
        
        int coun = 0;
        private int insertpurchaseinvoicedetails()
        {
            OracleConnection ORCL = Connection.GetConnection();
            int purID, proID, quantity, total;
            try 
            {
                purID = purchaceinvoiceid;
                foreach (DataGridViewRow row in productGV.Rows)
                {
                    proID = quantity = total = 0;
                    proID = Convert.ToInt32(row.Cells["idgv"].Value.ToString());
                    quantity = Convert.ToInt32(row.Cells["qugv"].Value.ToString());
                    total = Convert.ToInt32(row.Cells["totgv"].Value.ToString());
                    ORCL.Execute("insert into PURCHASES values(" + purID + ", " + proID + ", " + quantity + ", " + total + ")");
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
            } 
            catch (Exception) { } 
            finally { ORCL.Dispose(); }
            
            return coun;
        }
        object productstockcount ;
        private object getProductQuantity(int proID)
        {
            OracleConnection ORCL = Connection.GetConnection();
            try 
            {
                string query = "select QUANTITY from stock where PRODUCT_ID = "+proID+"";
                productstockcount = ORCL.Query<dynamic>(query).FirstOrDefault();
            }
            catch { } 
            finally { ORCL.Dispose(); }
            return productstockcount;
        }

        //------------------------Add to Stock--------------

        private void insertStock(int proID, int quantity)
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                ORCL.Execute("insert into stock values(" + proID + "," + quantity + ")");
            }
            catch { }
            finally { ORCL.Dispose(); }
        }

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
            if (proPrice.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please Select Product!", "Error");
                proPrice.Text = "";
                quantitytxt.Text = "";
                //producttext.Text = "Select.....";
                producttext.Focus();
            }
            else if (quantitytxt.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please Enter Quantity!", "Error");
                quantitytxt.Focus();
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
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                string query = "select p.Price from products p where p.id = " + selectedProductID + "";
                proPrice.Text = ORCL.Query<string>(query).FirstOrDefault();
                quantitytxt.Focus();
            }
            catch { }
            finally { ORCL.Dispose(); }
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
            int invoiceID = 0;
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                invoiceID = ORCL.Query<int>("SELECT id FROM (SELECT p.id FROM purchaseinvoice p ORDER BY p.id DESC) WHERE ROWNUM = 1").FirstOrDefault();

            }
            catch (Exception) { }
            finally { ORCL.Dispose(); }
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
