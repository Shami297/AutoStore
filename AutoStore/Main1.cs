using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoStore
{
    class Main1
    {
        public static void disable(Panel p)
        {
            p.Enabled = false;
        }

        public static void enable(Panel p)
        {
            p.Enabled = true;
        }

        public static void date(Label labl)
        {

            labl.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }

        public static void alphabetCheck(TextBox box)
        {
            Regex regex = new Regex(@"^[a-zA-Z\s]+$");
            Match match = regex.Match(box.Text);
            if (!match.Success)
            {
                MessageBox.Show("Enter a valid Name", "Error");
                box.Text = "";
                box.Focus();
            }
            
        }



        public static void IsAlphaNumeric(TextBox box)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9\s]+$");
            Match match = regex.Match(box.Text);
            if (!match.Success)
            {
                MessageBox.Show("Enter a valid UserName", "Error");
                box.Text = "";
                box.Focus();
            }
        }

        public static void authenticateNo(TextBox textBox)
        {
            Regex regex = new Regex(@"^((\+92)?(0092)?(92)?(0)?)(3)([0-9]{9})$");
            Match match = regex.Match(textBox.Text);
            if (!match.Success)
            {
                MessageBox.Show("Enter a valid Mobile Number", "Error");
                textBox.Focus();
            }
        }
    }

}
