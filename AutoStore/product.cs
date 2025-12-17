using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using AutoStore.Logic;
using Oracle.ManagedDataAccess.Client;
using Dapper;

namespace AutoStore
{
    public partial class product : UserControl
    {
        Connection pr = new Connection();
        public product()
        {
            InitializeComponent();
            category.Text = "Select....";
        }

        private void insert()
        {
            int selectedCatgoryID = (int)category.SelectedValue;
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                ORCL.Execute("insert into products values('" + idText.Text + "','" +nameText.Text.ToLower() + "'," + selectedCatgoryID + ",'" +bunifuTextBox5.Text + "')");
                MessageBox.Show("Product inserted successfully");
                textBox();
                Main1.disable(panel1);
            }
            catch { }
            finally { ORCL.Dispose(); }
        }

        private void update()
        {
            int selectedCatgoryID = (int)category.SelectedValue;
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                ORCL.Execute("UPDATE products p set p.name = '" +nameText.Text.ToLower() + "', p.CATID = " + selectedCatgoryID + ", p.price = '" +bunifuTextBox5.Text + "' where p.id = '" + idText.Text + "'");
                MessageBox.Show("Product Updated successfully");
                textBox();
                Main1.disable(panel1);
            }
            catch { }
            finally { ORCL.Dispose(); }
        }

        private void delete()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                ORCL.Execute("DELETE from products where id = '" + idText.Text + "'");
                MessageBox.Show("Product deleted successfully");
                textBox();
                Main1.disable(panel1);
            }
            catch { }
            finally { ORCL.Dispose(); }
        }

        private void textBox()
        {
            idText.Text = "";
            nameText.Text = "";
            bunifuTextBox5.Text = "";
            category.Text = "Select....";
        }

        private void show()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                var prod = ORCL.Query<ProductView>(
                    @"select 
                        p.id as ID, 
                        p.name as Name, 
                        p.price as Price, 
                        c.cat_name as Category
                      from products p
                      join categories c on p.catid = c.cat_id"
                ).ToList();

                GV.AutoGenerateColumns = true;
                GV.DataSource = null;
                GV.DataSource = prod;

                Main1.disable(panel1);
            }
            catch { }
            finally { ORCL.Dispose(); }
            
        }

        /*private void show1(DataGridView GV, DataGridViewColumn id, DataGridViewColumn name, DataGridViewColumn category, DataGridViewColumn catid, DataGridViewColumn price)
        {
            conn.Open();
            OleDbDataAdapter oda = new OleDbDataAdapter(@"select p.pro_id, p.pro_name, p.pro_price, c.cat_name from
            products p, categories c
            where p.pro_catid = c.cat_id", conn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            *//*    id.DataPropertyName = dt.Columns["p.pro_id"].ToString();
                name.DataPropertyName = dt.Columns["p.pro_name"].ToString();
                //category.DataPropertyName = dt.Columns["pro_catname"].ToString();
                catid.DataPropertyName = dt.Columns["c.cat_name"].ToString();
                price.DataPropertyName = dt.Columns["p.pro_price"].ToString();*//*

            GV.DataSource = dt;
            conn.Close();
            Main1.disable(panel1);
        }*/

        private void filcombo()
        {
            CategoryManager categoryManager = new CategoryManager();
            category.DataSource = categoryManager.GetCategories();
            category.DisplayMember = "Name";
            category.ValueMember = "ID";

        }
        

        private void search()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                var namePro = searchText.Text.ToLower();
                var prod = ORCL.Query<ProductView>(
                    $@"select 
                        p.id as ID, 
                        p.name as Name, 
                        p.price as Price, 
                        c.cat_name as Category
                      from products p
                      join categories c on p.catid = c.cat_id
                      WHERE NAME LIKE '%{namePro}%'"
                ).ToList();

                GV.AutoGenerateColumns = true;
                GV.DataSource = null;
                GV.DataSource = prod;
            }
            catch { }
            finally { ORCL.Dispose(); }
        }

        //---------------------------Generate ID----------------


        int productID;
        private int getProductID()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                productID = ORCL.Query<int>("SELECT ID FROM (SELECT p.ID FROM products p ORDER BY p.ID DESC) WHERE ROWNUM = 1").FirstOrDefault();
            }
            catch { }
            finally { ORCL.Dispose(); }
            return productID;
        }

        private void generateProID()
        {
            int proId = getProductID();
            proId += 1;
            idText.Text = proId.ToString();
        }

        private void BunifuLabel2_Click(object sender, EventArgs e)
        {

        }

        private void product_Load(object sender, EventArgs e)
        {
            Main1.disable(panel1);
            searchText.Enabled = false;
            filcombo();
            category.Text = "SELECT.....";
        }

        private void circularButton5_Click(object sender, EventArgs e)
        {
            updateButton.Enabled = false;
            deleteButton.Enabled = false;
            saveBtn.Enabled = true;
            generateProID();
            Main1.enable(panel1);
            nameText.Focus();
            nameText.Text = "";
            bunifuTextBox5.Text = "";
            category.Text = "Select....";
        }

        private void circularButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void circularButton2_Click(object sender, EventArgs e)
        {
            delete();
            show();
        }

        private void circularButton3_Click(object sender, EventArgs e)
        {
            update();
            show();
        }

        private void GV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                updateButton.Enabled = true;
                deleteButton.Enabled = true;
                Main1.enable(panel1);
                GV.CurrentCell.Selected = true;
                idText.Text = GV.Rows[e.RowIndex].Cells["ID"].FormattedValue.ToString();
                nameText.Text = GV.Rows[e.RowIndex].Cells["NAME"].FormattedValue.ToString();
                bunifuTextBox5.Text = GV.Rows[e.RowIndex].Cells["PRICE"].FormattedValue.ToString();
                category.Text = GV.Rows[e.RowIndex].Cells["category"].FormattedValue.ToString();
                saveBtn.Enabled = false;
            }
        }

        

        private void circularButton4_Click(object sender, EventArgs e)
        {
            show();
            textBox();
            searchText.Enabled = true;
            GV.AllowUserToAddRows = false;
            searchText.Focus();
        }

        

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Main1.disable(panel1);
            filcombo();
            category.Text = "Select....";
            textBox();
        }

       

        private void bunifuTextBox4_TabIndexChanged(object sender, EventArgs e)
        {
            search();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            insert();
            show();
        }

        private void searchText_TextChange(object sender, EventArgs e)
        {
            search();
        }
    }

    public class ProductView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
}
