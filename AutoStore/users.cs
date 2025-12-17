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
using Oracle.ManagedDataAccess.Client;
using Dapper;

namespace AutoStore
{
    public partial class users : Form
    {
       
        
        private void insert()
        {
            OracleConnection ORCL = Connection.GetConnection();
            try 
            {
                ORCL.Execute("insert into users values('" + idText.Text + "','" + nameText.Text + "','" +
                bunifuTextBox3.Text + "','" + bunifuTextBox4.Text + "','" + bunifuTextBox6.Text + "','" + bunifuTextBox5.Text + "','" + comboBox1.Text + "')");
                MessageBox.Show("Data inserted successfully");
                textClear();
            }
            catch (Exception) { }
            finally { ORCL.Dispose(); }
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
                OracleConnection ORCL = Connection.GetConnection();
                try
                {
                    ORCL.Execute("update users u set u.USR_NAME = '" +
                    nameText.Text.ToLower() + "', u.USR_USRNAME = '" + bunifuTextBox3.Text.ToLower() + "', u.USR_PASSWORD = '" +
                    bunifuTextBox4.Text.ToLower() + "', u.USR_EMAIL = '" + bunifuTextBox5.Text.ToLower() + "'" +
                    ",u.USR_PHONENO = '" + bunifuTextBox6.Text + "',u.USR_ISACTIVE = '" + comboBox1.Text + "' where u.USR_ID = '" + idText.Text + "'");
                    MessageBox.Show("User Updated successfully");
                    textClear();
                }
                catch (Exception) { }
                finally { ORCL.Dispose(); }
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
            OracleConnection ORCL = Connection.GetConnection();
            try 
            {
                var users = ORCL.Query<usersView>("select usr_ID as ID, usr_name as Name, usr_usrname as userName, usr_password as Password, usr_email as Email, usr_phoneno as Phone,, usr_isactive as Active from users WHERE USR_NAME LIKE '%" + userText.Text.ToLower() + "%'").ToList();
                // Bind to grid
                gv.AutoGenerateColumns = true;
                gv.DataSource = null;
                gv.DataSource = users;
            }
            catch (Exception) { }
            finally { ORCL.Dispose(); }
        }

        //Retrieve data in DataGridView
        private void show1(DataGridView gv)
        {
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                var users = ORCL.Query<usersView>("select usr_ID as ID, usr_name as Name, usr_usrname as userName, usr_password as Password, usr_email as Email, usr_phoneno as Phone, usr_isactive as Active from users").ToList();
                

                // Bind to grid
                gv.AutoGenerateColumns = true;
                gv.DataSource = null;
                gv.DataSource = users;
                gv.Columns["ID"].Visible = false;
                // Force grid refresh if needed
                gv.Refresh();
            }
            catch (Exception) { }
            finally { ORCL.Dispose(); }
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
            show1(gv);
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
                idText.Text = gv.Rows[e.RowIndex].Cells["ID"].FormattedValue.ToString();
                nameText.Text = gv.Rows[e.RowIndex].Cells["Name"].FormattedValue.ToString();
                bunifuTextBox3.Text = gv.Rows[e.RowIndex].Cells["userName"].FormattedValue.ToString();
                bunifuTextBox5.Text = gv.Rows[e.RowIndex].Cells["Email"].FormattedValue.ToString();
                bunifuTextBox6.Text = gv.Rows[e.RowIndex].Cells["Phone"].FormattedValue.ToString();
                comboBox1.Text = gv.Rows[e.RowIndex].Cells["Active"].FormattedValue.ToString();
                bunifuTextBox4.Text = gv.Rows[e.RowIndex].Cells["Password"].FormattedValue.ToString();
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

        
        private int getUerID()
        {
            int userID = 0;
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                userID = ORCL.Query<int>("SELECT USR_ID FROM ( SELECT USR_ID FROM users ORDER BY USR_ID DESC ) WHERE ROWNUM = 1").FirstOrDefault();
                
            }
            catch (Exception) { }
            finally { ORCL.Dispose(); }
            return userID;
        }


        private void nameText_Leave(object sender, EventArgs e)
        {
            if (nameText.Text == string.Empty && bunifuTextBox3.Text == string.Empty)
                return;
            Main1.alphabetCheck(nameText);
        }
    }

    public class usersView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string userName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Active { get; set; }
    }
}
