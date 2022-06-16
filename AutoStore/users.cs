using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using AutoStore.Logic;

namespace AutoStore
{
    public partial class users : Form
    {
        Connection pr = new Connection();
        OleDbConnection conn = new OleDbConnection("Provider=MSDAORA;Data Source=ORCL;Persist Security Info=True;User ID=HR;Password=HR;Unicode=True");
        
        private void insert()
        {
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("insert into users values('" + idText.Text + "','" + nameText.Text + "','" +
                bunifuTextBox3.Text + "','" + bunifuTextBox4.Text + "','" + bunifuTextBox6.Text + "','" + bunifuTextBox5.Text + "','" + comboBox1.Text + "')", conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data inserted successfully");
            textClear();
            conn.Close();
        }

        private void updateUser()
        {
            int id = Convert.ToInt32(idText.Text);
            if(id == 1)
            {
                MessageBox.Show("You are not allowed to update this User","Error");
                textClear();
                updateBtn.Hide();
                circularButton2.Show();
                Main1.disable(EnteriesUser);
            }
            else
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand(@"update users u set u.USR_NAME = '" +
                    nameText.Text.ToLower() + "', u.USR_USRNAME = '" + bunifuTextBox3.Text.ToLower() + "', u.USR_PASSWORD = '" +
                    bunifuTextBox4.Text.ToLower() + "', u.USR_EMAIL = '" + bunifuTextBox5.Text.ToLower() + "'" +
                    ",u.USR_PHONENO = '" + bunifuTextBox6.Text + "',u.USR_ISACTIVE = '" + comboBox1.Text + "' where u.USR_ID = '" + idText.Text + "'", conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Updated successfully");
                textClear();
                conn.Close();
            }
        }

        private void textClear()
        {
            idText.Text = "";
            nameText.Text = "";
            bunifuTextBox3.Text = "";
            bunifuTextBox4.Text = "";
            bunifuTextBox5.Text = "";
            bunifuTextBox6.Text = "";
            comboBox1.Text = "";
        }

        private void search()
        {
            conn.Open();
            OleDbDataAdapter oda = new OleDbDataAdapter("select * from users WHERE USR_NAME LIKE '%" + userText.Text.ToLower() + "%'", conn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            gv.DataSource = dt;
            conn.Close();
        }

        //Retrieve data in DataGridView
        private void show1(DataGridView gv,DataGridViewColumn Namegv, DataGridViewColumn usernamegv, DataGridViewColumn pwdgv,
            DataGridViewColumn emailgv, DataGridViewColumn nogv, DataGridViewColumn statusgv, DataGridViewColumn idgv)
        {
            conn.Open();
            OleDbDataAdapter oda = new OleDbDataAdapter("select * from users", conn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            Namegv.DataPropertyName = dt.Columns["usr_name"].ToString();
            usernamegv.DataPropertyName = dt.Columns["usr_usrname"].ToString();
            pwdgv.DataPropertyName = dt.Columns["usr_password"].ToString();
            emailgv.DataPropertyName = dt.Columns["usr_email"].ToString();
            nogv.DataPropertyName = dt.Columns["usr_phoneno"].ToString();
            statusgv.DataPropertyName = dt.Columns["usr_isactive"].ToString();
            idgv.DataPropertyName = dt.Columns["usr_ID"].ToString();

            gv.DataSource = dt;
            conn.Close();
        }

        //------------------Authenticate Mobile Number-----------------

        private void authenticateNo()
        {
            Regex regex = new Regex(@"^((\+92)?(0092)?(92)?(0)?)(3)([0-9]{9})$");
            Match match = regex.Match(bunifuTextBox6.Text);
            if (match.Success)
            {
                comboBox1.Focus();
            }
            else
            {
                MessageBox.Show("Enter a valid Mobile Number", "Error");
                bunifuTextBox6.Focus();
            }
        }

        //-----------------Validate Email-------------
        private bool IsValidEmail(string email)
        {
            try
            {
                Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                 @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                 @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)");
                Match match = regex.Match(email);
                if (match.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch
            {
                return false;
            }
        }
        public users()
        {
            InitializeComponent();
            gv.AllowUserToAddRows = false;
        }

        private void users_Load(object sender, EventArgs e)
        {
            Main1.disable(EnteriesUser);
            updateBtn.Hide();
        }

        private void circularButton3_Click(object sender, EventArgs e)
        {
            Main1.enable(EnteriesUser);
            generateUserID();
            nameText.Focus();
        }

        private void circularButton2_Click(object sender, EventArgs e)
        {
            insert();
        }

        private void circularButton4_Click(object sender, EventArgs e)
        {
            show1(gv,Namegv,usernamegv,pwdgv,emailgv,nogv,statusgv,idgv);
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            DashBoard dash = new DashBoard();
            this.Close();
            dash.Show();
        }

        private void gv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                Main1.enable(EnteriesUser);
                updateBtn.Show();
                circularButton2.Hide();
                gv.CurrentCell.Selected = true;
                idText.Text = gv.Rows[e.RowIndex].Cells["idgv"].FormattedValue.ToString();
                nameText.Text = gv.Rows[e.RowIndex].Cells["Namegv"].FormattedValue.ToString();
                bunifuTextBox3.Text = gv.Rows[e.RowIndex].Cells["usernamegv"].FormattedValue.ToString();
                bunifuTextBox5.Text = gv.Rows[e.RowIndex].Cells["emailgv"].FormattedValue.ToString();
                bunifuTextBox6.Text = gv.Rows[e.RowIndex].Cells["nogv"].FormattedValue.ToString();
                comboBox1.Text = gv.Rows[e.RowIndex].Cells["statusgv"].FormattedValue.ToString();
                bunifuTextBox4.Text = gv.Rows[e.RowIndex].Cells["pwdgv"].FormattedValue.ToString();
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            updateUser();
            textClear();
            updateBtn.Hide();
            circularButton2.Show();
            Main1.disable(EnteriesUser);
        }

        private void userText_TextChange(object sender, EventArgs e)
        {
            search();
        }

        private void bunifuTextBox6_Leave(object sender, EventArgs e)
        {
            authenticateNo();
        }

        private void bunifuTextBox5_Leave(object sender, EventArgs e)
        {
            string Email = bunifuTextBox5.Text;
            bool mail = IsValidEmail(Email);

            if (mail)
            {
                bunifuTextBox6.Focus();
            }
            else
            {
                MessageBox.Show("Enter Valid Email Address","Invalid Email");
                bunifuTextBox5.Focus();
                bunifuTextBox5.Text = "";
            }
        }

        //--------------------------- Generate ID ----------

        private void generateUserID()
        {
            int usrId = getUerID();
            usrId += 1;
            idText.Text = usrId.ToString();
        }



        //-------------------------- Get ID -----------

        int userID;
        private int getUerID()
        {
            OleDbCommand command = new OleDbCommand("SELECT USR_ID FROM (SELECT u.USR_ID FROM Users u ORDER BY u.USR_ID DESC) WHERE ROWNUM = 1", pr.conn);
            pr.conn.Open();
            userID = Convert.ToInt32(command.ExecuteScalar());
            pr.conn.Close();
            return userID;
        }


        private void nameText_Leave(object sender, EventArgs e)
        {
            Main1.alphabetCheck(nameText);
        }
    }
}
