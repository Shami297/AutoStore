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

namespace AutoStore
{
    public partial class categories : UserControl
    {
        Connection pr = new Connection();
        private void savebtn()
        {
            try
            {
                pr.conn.Open();
                /*OleDbCommand cmd = new OleDbCommand("insert into categories values('" + bunifuTextBox3.Text + "','" + bunifuTextBox1.Text + "','" +
                    comboBox1.Text + "')", conn);*/
                OleDbCommand cmd = new OleDbCommand("insert into categories values('" + idText.Text + "','" + catNameText.Text + "','" +
                comboBox1.Text + "')", pr.conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data inserted successfully");
                clearText();
                pr.conn.Close();
                Main1.disable(panel1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void show1(DataGridView categoryGV, DataGridViewColumn catid, DataGridViewColumn name, DataGridViewColumn status)
        {
            try
            {
                pr.conn.Open();
                OleDbDataAdapter oda = new OleDbDataAdapter("select * from categories", pr.conn);
                DataTable dt = new DataTable();
                oda.Fill(dt);
                catid.DataPropertyName = dt.Columns["cat_id"].ToString();
                name.DataPropertyName = dt.Columns["cat_name"].ToString();
                status.DataPropertyName = dt.Columns["cat_isActive"].ToString();

                categoryGV.DataSource = dt;
                pr.conn.Close();
                Main1.disable(panel1);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void clearText()
        {
            catNameText.Text = "";
            idText.Text = "";
            comboBox1.Text = "Select....";
        }

        private void auto()
        {
            pr.conn.Open();
            OleDbDataAdapter oda = new OleDbDataAdapter("select isnull(max(cat_id),0) from categories", pr.conn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            pr.conn.Close();
        }

        private void updatebtn()
        {
            pr.conn.Open();
            OleDbCommand cmd = new OleDbCommand("update categories set cat_name = '" + catNameText.Text + "', cat_isActive = '" + comboBox1.Text + "' where cat_id = '" + idText.Text + "'", pr.conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data Updated successfully");
            clearText();
            pr.conn.Close();
            show1(categoryGV, catid, name, status);
            Main1.disable(panel1);
        }

        private void delbtn()
        {
            pr.conn.Open();
            OleDbCommand cmd = new OleDbCommand("delete from categories where cat_id = '" + idText.Text + "'", pr.conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data deleted successfully");
            clearText();
            pr.conn.Close();
            show1(categoryGV, catid, name, status);
            Main1.disable(panel1);
        }


        private void search()
        {
            pr.conn.Open();
            OleDbDataAdapter oda = new OleDbDataAdapter("select * from categories WHERE CAT_NAME LIKE '%" + searchText.Text.ToLower() + "%'", pr.conn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            categoryGV.DataSource = dt;
            pr.conn.Close();
        }

        //---------------------------Generate ID----------------


        int categoryID;
        private int getCategoryID()
        {
            OleDbCommand command = new OleDbCommand("SELECT CAT_ID FROM (SELECT c.CAT_ID FROM Categories c ORDER BY c.CAT_ID DESC) WHERE ROWNUM = 1", pr.conn);
            pr.conn.Open();
            categoryID = Convert.ToInt32(command.ExecuteScalar());
            pr.conn.Close();
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
            show1(categoryGV, catid, name, status);
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
                idText.Text = categoryGV.Rows[e.RowIndex].Cells["catid"].FormattedValue.ToString();
                catNameText.Text = categoryGV.Rows[e.RowIndex].Cells["name"].FormattedValue.ToString();
                comboBox1.Text = categoryGV.Rows[e.RowIndex].Cells["status"].FormattedValue.ToString();
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
}
