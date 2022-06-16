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
    public partial class Stock : Form
    {
        Connection pr = new Connection();
        public Stock()
        {
            InitializeComponent();
        }

        private void backButn_Click(object sender, EventArgs e)
        {
            this.Close();
            DashBoard dash = new DashBoard();
            dash.Show();
        }

        private void showStock()
        {
            pr.conn.Open();
            OleDbDataAdapter oda = new OleDbDataAdapter(@"select p.name, p.price,s.quantity from products p
            , stock s where p.id = s.product_id", pr.conn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            stockGV.DataSource = dt;
            pr.conn.Close();
        }

        private void Stock_Load(object sender, EventArgs e)
        {
            showStock();
        }
    }
}
