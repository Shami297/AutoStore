using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class Form1 : Form
    {
        Connection pr = new Connection();
        private void login()
        {

            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                var user = ORCL.Query<dynamic>("Select * from users Where usr_usrname ='" + userbox.Text.Trim() + "' and usr_password = '" + pwdbox.Text.Trim() + "'and USR_ISACTIVE = 'Yes'").FirstOrDefault();

                if (user != null)
                {
                    DashBoard d = new DashBoard();
                    d.Show();
                    userbox.Text = "";
                    pwdbox.Text = "";
                }
                else
                {
                    MessageBox.Show("Please Enter valid UserName or Password");
                    userbox.Text = "";
                    pwdbox.Text = "";
                    userbox.Focus();
                }
            }
            catch (Exception) { }
            finally { ORCL.Dispose(); }

        }
        public Form1()
        {
            InitializeComponent();
        }

        private void CircularButton1_Click(object sender, EventArgs e)
        {
            if (userbox.Text == "")
            {
                if (pwdbox.Text == "")
                {
                    MessageBox.Show("Please Enter Username and Password");
                }
                else
                {
                    MessageBox.Show("Please Enter Username");
                    userbox.Focus();
                }
            }
            else if (pwdbox.Text == "")
            {
                MessageBox.Show("Please Enter Password");
                pwdbox.Focus();
            }

            else
            {
                login();
                userbox.Text = "";
                pwdbox.Text = "";
            }
            /*DashBoard d = new DashBoard();
            d.Show();*/
            
        }

        private void circularButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                pwdbox.UseSystemPasswordChar = false;
            }
            else
            {
                pwdbox.UseSystemPasswordChar = true;
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            Recover recover = new Recover();
            recover.Show();
        }

        private void pwdbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                login();
                userbox.Text = "";
                pwdbox.Text = "";
            }
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        private void userbox_Leave(object sender, EventArgs e)
        {
           Main1.IsAlphaNumeric(userbox);
        }
    }
}
