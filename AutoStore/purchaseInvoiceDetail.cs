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
    public partial class purchaseInvoiceDetail : Form
    {
        Connection pr = new Connection();
        public purchaseInvoiceDetail()
        {
            InitializeComponent();
        }

        private void loadInvoice()
        {
            object monPicker = this.monthPicker.Value.Month;
            object yearPicker = this.monthPicker.Value.Year;


            purchaseDetailManager detailManager = new purchaseDetailManager();
            selectText.DataSource = detailManager.GetPurchaseDetails(monPicker, yearPicker);
            selectText.DisplayMember = "Name";
            selectText.ValueMember = "ID";
            InvoiceDetail.AllowUserToAddRows = false;
        }

        private void purchaseInvoiceDetail_Load(object sender, EventArgs e)
        {
            this.monthPicker.Value = DateTime.Now;
            loadInvoice();
        }

        private void monthPicker_onValueChanged(object sender, EventArgs e)
        {
            loadInvoice();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            purchaseInvoice purchase = new purchaseInvoice();
            this.Close();
            purchase.Show();
        }
        private void showPurchases()
        {
            int invoiceID = (int)selectText.SelectedValue;
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                var purchases = ORCL.Query<pInvoiceDetailView>("SELECT p.name, pu.quantity, pu.total from purchaseinvoice pui LEFT JOIN purchases pu ON pu.PURINVOICE_ID = pui.ID LEFT JOIN products p ON p.id = pu.product_id where pui.ID = "+ invoiceID);
                InvoiceDetail.DataSource = purchases;
            }
            catch { }
            finally { ORCL.Dispose(); }
        }

        private void select_TextChanged(object sender, EventArgs e)
        {
            //showPurchases();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            showPurchases();
            grossLabel.Text = grossSale().ToString();
        }

        private int grossSale()
        {
            int total, grossTot;
            grossTot = 0;
            foreach (DataGridViewRow row in InvoiceDetail.Rows)
            {
                total = Convert.ToInt32(row.Cells["Total"].Value.ToString());
                grossTot = total + grossTot;
            }
            return grossTot;
        }
    }
    public class pInvoiceDetailView
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Total { get; set; }
    }
}
