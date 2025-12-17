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
    public partial class VendorReport : Form
    {
        Connection pr = new Connection();
        public VendorReport()
        {
            InitializeComponent();
        }

        private void vendorFill()
        {
            VendorReportManager customer = new VendorReportManager();

            selectVendr.DisplayMember = "Name";
            selectVendr.ValueMember = "ID";

            selectVendr.DataSource = customer.GetVendors();
            vendrReport.AllowUserToAddRows = false;
        }

        private void VendorReport_Load(object sender, EventArgs e)
        {
            vendorFill();
        }

        private void showDetail()
        {
            int vendrID = (int)selectVendr.SelectedValue;
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                var vendr = ORCL.Query<vReportView>("SELECT to_char(pi.PI_DATE,'DD-MM-YYYY')  AS InvoiceDATE, SUM(p.total) AS Total from purchaseinvoice pi, purchases p ,vendors v where v.id = " + vendrID + " and  pi.VENDORS_ID = " + vendrID + "  and pi.ID = p.PURINVOICE_ID GROUP BY pi.PI_DATE order  by pi.PI_DATE").ToList();
                vendrReport.DataSource = vendr;
            }
            catch { }
            finally { ORCL.Dispose(); }


        }

        private int totalPurchase()
        {
            int total, Tot;
            Tot = 0;
            foreach (DataGridViewRow row in vendrReport.Rows)
            {
                total = Convert.ToInt32(row.Cells["Total"].Value.ToString());
                Tot = total + Tot;
            }
            return Tot;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showDetail();
            totalLabel.Text = totalPurchase().ToString();
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            //Reports reports = new Reports();
            //reports.Show();
            this.Close();
        }

    }

    public class vReportView
    {
        public string InvoiceDATE { get; set; }
        public string Total { get; set; }
    }
}
