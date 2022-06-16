﻿using AutoStore.Logic;
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

            int vendorsID = (int)customerText.SelectedValue;
            OleDbCommand cmd = new OleDbCommand("insert into SALEINVOICE values('" + idText.Text + "','" +
              datePick.Text + "'," + vendorsID + ")", pr.conn);
            pr.conn.Open();
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT id FROM (SELECT s.id FROM SALEINVOICE s ORDER BY s.id DESC) WHERE ROWNUM = 1";
            saleInvoiceID = Convert.ToInt32(cmd.ExecuteScalar());
            pr.conn.Close();
            return saleInvoiceID;
        }

        int coun = 0;
        private int insertSaleinvoicedetails()
        {
            int purID, proID, quantity, total;
            purID = saleInvoiceID;
            foreach (DataGridViewRow row in productGV.Rows)
            {
                proID = quantity = total = 0;
                proID = Convert.ToInt32(row.Cells["idgv"].Value.ToString());
                quantity = Convert.ToInt32(row.Cells["qugv"].Value.ToString());
                total = Convert.ToInt32(row.Cells["totgv"].Value.ToString());
                OleDbCommand cmd = new OleDbCommand("insert into sales values(" + purID + "," + proID + "," + quantity + "," + total + ")", pr.conn);
                pr.conn.Open();
                cmd.ExecuteNonQuery();
                pr.conn.Close();
                int num = 0;
                num = getProductQuantity(proID);
                if(num > 0)
                {
                    updateStock(num, proID, quantity);
                }
                else
                {
                    MessageBox.Show("Stock is Not Available", "OutOfStock");
                }
                coun++;
            }
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
            int selectedProductID = (int)productText.SelectedValue;
            pr.conn.Open();
            string query = "select s.quantity from products p, stock s where p.id  = " + selectedProductID + "and" +
                " p.id = s.PRODUCT_ID";
            OleDbCommand command = new OleDbCommand(query, pr.conn);
            OleDbDataReader odr = command.ExecuteReader();
            while (odr.Read())
            {
                stockQuantity.Text = odr.GetValue(0).ToString();
            }
            pr.conn.Close();
        }

        private void productText_TextChanged_1(object sender, EventArgs e)
        {
            int selectedProductID = (int)productText.SelectedValue;
            pr.conn.Open();
            string query = "select ((p.Price * 20)/100 + p.Price) from products p where p.id = " + selectedProductID + "";
            OleDbCommand command = new OleDbCommand(query, pr.conn);
            OleDbDataReader odr = command.ExecuteReader();
            while (odr.Read())
            {
                proPrice.Text = odr.GetValue(0).ToString();
            }
            pr.conn.Close();
            quantityInStock();
            quantityText.Focus();
        }

        //-----------------------Generate ID---------------

        int invoiceID;
        private int getSaleInvoiceID()
        {
            OleDbCommand command = new OleDbCommand("SELECT id FROM (SELECT s.id FROM SALEINVOICE s ORDER BY s.id DESC) WHERE ROWNUM = 1", pr.conn);
            pr.conn.Open();
            invoiceID = Convert.ToInt32(command.ExecuteScalar());
            pr.conn.Close();
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
            if (quantityText.Text == "")
            {
                MessageBox.Show("Please Select Product", "Error");
                quantityText.Text = "";
                proPrice.Text = "";
                productText.Text = "Select.....";

            }
            else
            {
                int productID = (int)productText.SelectedValue;
                foreach (DataGridViewRow row in productGV.Rows)
                {
                    int addedProId = Convert.ToInt32(row.Cells["idgv"].Value.ToString());
                    if (productID == addedProId)
                    {
                        int total = Convert.ToInt32(row.Cells["totgv"].Value.ToString());
                        int quantity = Convert.ToInt32(row.Cells["qugv"].Value.ToString());
                        int quan = Convert.ToInt32(quantityText.Text);
                        quan = quantity + quan;
                        total += Convert.ToInt32(totalLabel.Text);
                        row.Cells["qugv"].Value = quan.ToString();
                        row.Cells["totgv"].Value = total.ToString();
                        grossLabel.Text = (Convert.ToInt32(totalLabel.Text) + Convert.ToInt32(grossLabel.Text)).ToString();
                        goto found;
                    }
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
            string query = "select QUANTITY from stock where PRODUCT_ID = " + proID + "";
            OleDbCommand cmd = new OleDbCommand(query, pr.conn);
            pr.conn.Open();
            productstockcount = Convert.ToInt32(cmd.ExecuteScalar());
            pr.conn.Close();
            return productstockcount;
        }

        private void updateStock(int quan, int proID, int quantity)
        {
            quan -= quantity;
            OleDbCommand cmd = new OleDbCommand("update stock s set s.QUANTITY = " + quan + "where s.PRODUCT_ID = " + proID + "", pr.conn);
            pr.conn.Open();
            cmd.ExecuteNonQuery();
            pr.conn.Close();
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
