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
    public partial class categories : UserControl
    {
        
        private void savebtn()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                ORCL.Execute("insert into categories values('" + idText.Text + "','" + catNameText.Text + "','" +comboBox1.Text + "')");
                MessageBox.Show("Data inserted successfully");
                clearText();
                Main1.disable(panel1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { ORCL.Dispose(); }

        }

        private void show1(DataGridView categoryGV)
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                var categories = ORCL.Query<catView>("select cat_id as ID,cat_name as Name, cat_isActive as Active from categories").ToList();

                categoryGV.AutoGenerateColumns = true;
                categoryGV.DataSource = null;
                categoryGV.DataSource = categories;
                Main1.disable(panel1);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { ORCL.Dispose(); }

        }

        private void clearText()
        {
            catNameText.Text = "";
            idText.Text = "";
            comboBox1.Text = "Select....";
        }

        //private void auto()
        //{
        //    OracleConnection ORCL = Connection.GetConnection();
        //    try
        //    {

        //    }
        //    catch { }
        //    finally { ORCL.Dispose(); }
        //    pr.conn.Open();
        //    OleDbDataAdapter oda = new OleDbDataAdapter("select isnull(max(cat_id),0) from categories", pr.conn);
        //    DataTable dt = new DataTable();
        //    oda.Fill(dt);
        //    pr.conn.Close();
        //}

        private void updatebtn()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                ORCL.Execute("update categories set cat_name = '" + catNameText.Text + "', cat_isActive = '" + comboBox1.Text + "' where cat_id = '" + idText.Text + "'");

                MessageBox.Show("Data Updated successfully");
                clearText();
                show1(categoryGV);
                Main1.disable(panel1);
            }
            catch { }
            finally { ORCL.Dispose(); }
        }

        private void delbtn()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                ORCL.Execute("DELETE from categories where cat_id = '" + idText.Text + "'");
                MessageBox.Show("Data deleted successfully");
                clearText();
                show1(categoryGV);
                Main1.disable(panel1);
            }
            catch { }
            finally { ORCL.Dispose(); }
        }


        private void search()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                var categories = ORCL.Query<catView>("select cat_id as ID,cat_name as Name, cat_isActive as Active from categories WHERE CAT_NAME LIKE '%" + searchText.Text.ToLower() + "%'").ToList();
                categoryGV.AutoGenerateColumns = true;
                categoryGV.DataSource = null;
                categoryGV.DataSource = categories;
            }
            catch { }
            finally { ORCL.Dispose(); }
        }

        //---------------------------Generate ID----------------


        int categoryID;
        private int getCategoryID()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                categoryID = ORCL.Query<int>("SELECT CAT_ID FROM (SELECT c.CAT_ID FROM Categories c ORDER BY c.CAT_ID DESC) WHERE ROWNUM = 1").FirstOrDefault();
            }
            catch { }
            finally { ORCL.Dispose(); }
            return categoryID;
        }

        private void generateCatID()
        {
            int catId = getCategoryID();
            catId += 1;
            idText.Text = catId.ToString();
        }

        public categories()
        {
            InitializeComponent();
            idText.Enabled = false;
        }

        private void circularButton5_Click(object sender, EventArgs e)
        {
            Main1.enable(panel1);
            generateCatID();
            circularButton2.Enabled = true;
            updateButton.Enabled = false;
            circularButton4.Enabled = false;
            catNameText.Focus();
            catNameText.Text = "";
            comboBox1.Text = "Select....";

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void categories_Load(object sender, EventArgs e)
        {
            Main1.disable(panel1);
        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void circularButton2_Click(object sender, EventArgs e)
        {
            savebtn();
        }

        private void circularButton1_Click(object sender, EventArgs e)
        {
            show1(categoryGV);
            clearText();
            searchText.Focus();
            categoryGV.AllowUserToAddRows = false;
        }

        private void circularButton3_Click(object sender, EventArgs e)
        {
            updatebtn();
        }

        private void circularButton4_Click(object sender, EventArgs e)
        {
            delbtn();
        }

        private void categoryGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (categoryGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                Main1.enable(panel1);
                circularButton2.Enabled = false;
                circularButton4.Enabled = true;
                updateButton.Enabled = true;
                categoryGV.CurrentCell.Selected = true;
                idText.Text = categoryGV.Rows[e.RowIndex].Cells["ID"].FormattedValue.ToString();
                catNameText.Text = categoryGV.Rows[e.RowIndex].Cells["Name"].FormattedValue.ToString();
                comboBox1.Text = categoryGV.Rows[e.RowIndex].Cells["Active"].FormattedValue.ToString();
            }

        }

        private void searchText_TextChange(object sender, EventArgs e)
        {
            search();
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            Main1.disable(panel1);
            clearText();
        }
    }

    public class catView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Active { get; set; }
    }
}
