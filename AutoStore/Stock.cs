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
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                var product = ORCL.Query<stockView>("select p.name, p.price,s.quantity from products p, stock s where p.id = s.product_id").ToList();
                stockGV.DataSource = product;
            }
            catch { }
            finally { ORCL.Dispose(); }
        }

        private void Stock_Load(object sender, EventArgs e)
        {
            showStock();
        }
    }
    public class stockView
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
    }
}
