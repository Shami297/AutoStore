using AutoStore.Logic;
using System;
using System.Activities.Expressions;
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
    public partial class CustomerReport : Form
    {
        Connection pr = new Connection();
        public CustomerReport()
        {
            InitializeComponent();
        }

        private void customerFill()
        {
            CustomerReportManager customer = new CustomerReportManager();

            selectCstmr.DisplayMember = "Name";
            selectCstmr.ValueMember = "ID";

            selectCstmr.DataSource = customer.GetCustomers();
            cstmrReport.AllowUserToAddRows = false;
        }

        private void CustomerReport_Load(object sender, EventArgs e)
        {
            customerFill();
        }

        private void showDetail()
        {
            int cstmrID = (int)selectCstmr.SelectedValue;
            pr.conn.Open();
            OleDbDataAdapter oda = new OleDbDataAdapter(@"SELECT to_char(si.SI_DATE,'DD-MM-YYYY')  AS InvoiceDATE, SUM(s.total) AS Total from saleinvoice si, sales s, customer c where c.cstmr_id = " + cstmrID+ " and si.CUSTOMER_ID = " + cstmrID + " and si.id = s.salinvoice_id GROUP BY si.si_date order by si.si_date", pr.conn);
            
            DataTable dt = new DataTable();
            oda.Fill(dt);
            cstmrReport.DataSource = dt;
            pr.conn.Close();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            showDetail();
            totalLabel.Text = totalSale().ToString();
        }

        private int totalSale()
        {
            int total, Tot;
            Tot = 0;
            foreach (DataGridViewRow row in cstmrReport.Rows)
            {
                total = Convert.ToInt32(row.Cells["Total"].Value.ToString());
                Tot = total + Tot;
            }
            return Tot;
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            Reports reports = new Reports();
            reports.Show();
            this.Close();
        }
    }
}
