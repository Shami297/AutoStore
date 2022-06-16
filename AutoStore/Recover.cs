using AutoStore.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoStore
{
    public partial class Recover : Form
    {
        Connection pr = new Connection();
        public Recover()
        {
            InitializeComponent();
        }

        private bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
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


        public void findAccount()
        {
            string uname = emailText.Text;
            bool Email = IsValidEmail(uname);
            if(Email)
            {
                string name =emailText.Text.Trim();
               // OracleString or = new OracleString(name); 
                string userName;
                string Password;
                string query = "select USR_usrNAME from users where USR_EMAIL = '" + name+"' ";
                OleDbCommand cmd = new OleDbCommand(query, pr.conn);
                pr.conn.Open();
                userName = cmd.ExecuteScalar().ToString();
                cmd.CommandText = "select USR_PASSWORD from users where USR_EMAIL = '" + name + "' ";
                Password = cmd.ExecuteScalar().ToString();
                pr.conn.Close();
                if (userName != "")
                {
                    nameText.Text = userName;
                    pwdText.Text = Password;
                }
                emailText.Text = "";
            }
            else
            {
                MessageBox.Show("Enter Valid Email Address", "Error");
                emailText.Text = "";
            }
        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Show_Click(object sender, EventArgs e)
        {
            findAccount();
        }

        private void Recover_Load(object sender, EventArgs e)
        {
            emailText.Focus();
        }

        private void emailText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                findAccount();
            }
        }
    }
}
