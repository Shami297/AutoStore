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
    public partial class SaleInvoiceDetail : Form
    {
        Connection pr = new Connection();
        public SaleInvoiceDetail()
        {
            InitializeComponent();
        }
        private void loadSaleInvoice()
        {
            int mon = this.datePick.Value.Month;
            int year = this.datePick.Value.Year;

            SaleDetailManager saleDetailManager = new SaleDetailManager();
            selectSaleInvoice.DataSource = saleDetailManager.GetSaleDetails(mon, year);
            selectSaleInvoice.DisplayMember = "Name";
            selectSaleInvoice.ValueMember = "ID";
            saleGV.AllowUserToAddRows = false;
        }

        private void SaleInvoiceDetail_Load(object sender, EventArgs e)
        {
            loadSaleInvoice();
        }

        private void showSales()
        {
            int saleID = (int)selectSaleInvoice.SelectedValue;
            pr.conn.Open();
            OleDbDataAdapter oda = new OleDbDataAdapter(@"SELECT p.name, s.quantity, s.total from products p, 
            SALEINVOICE si, sales s where si.ID = " + saleID + " and s.SALINVOICE_ID = si.ID and p.id = s.product_id", pr.conn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            saleGV.DataSource = dt;
            pr.conn.Close();
        }

        private void selectSaleInvoice_TextChanged(object sender, EventArgs e)
        {
            //loadSaleInvoice();
          // showSales();
        }



        private void bunifuLabel2_Click(object sender, EventArgs e)
        {

        }

        private void viewBtn_Click(object sender, EventArgs e)
        {
            showSales();
            grossLabel.Text = grossSale().ToString();
        }

        private void datePick_onValueChanged(object sender, EventArgs e)
        {
            loadSaleInvoice();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            saleInvoice sale = new saleInvoice();
            this.Close();
            sale.Show();
        }

        private int grossSale()
        {
            int total, grossTot;
            grossTot = 0;
            foreach(DataGridViewRow row in saleGV.Rows)
            {
                total = Convert.ToInt32(row.Cells["total"].Value.ToString());
                grossTot = total + grossTot;
            }
            return grossTot;
        }
    }
}
