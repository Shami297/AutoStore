using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoStore
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            DashBoard db = new DashBoard();
            db.Show();
            this.Close();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            CustomerReport customerReport = new CustomerReport();
            this.Close();
            customerReport.Show();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            VendorReport vendorReport = new VendorReport();
            this.Close();
            vendorReport.Show();
        }
    }
}
