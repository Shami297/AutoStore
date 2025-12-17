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
            this.datePick.Value = DateTime.Now;
            loadSaleInvoice();
        }

        private void showSales()
        {
            int saleID = (int)selectSaleInvoice.SelectedValue;
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                var sales = ORCL.Query<sInvoiceDetailView>("SELECT p.name, s.quantity, s.total from SALEINVOICE si LEFT JOIN sales s ON s.SALINVOICE_ID = si.ID LEFT JOIN products p ON p.id = s.product_id where si.ID = "+ saleID);
                saleGV.DataSource = sales;
            }
            catch { }
            finally { ORCL.Dispose(); }
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
                total = Convert.ToInt32(row.Cells["Total"].Value.ToString());
                grossTot = total + grossTot;
            }
            return grossTot;
        }
    }

    public class sInvoiceDetailView
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Total { get; set; }
    }
}
