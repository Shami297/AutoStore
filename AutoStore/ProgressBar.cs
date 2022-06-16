using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AutoStore
{
    public partial class ProgressBar : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
            (
            int nLeftRect,
            int nTopRect,
            int RightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nheightEllipse
            );
        public ProgressBar()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 40, 40));
            prBar.Value = 0;
        }

        private void ProgressBar_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            prBar.Value += 1;
            prBar.Text = prBar.Value.ToString() + "%";

            if(prBar.Value == 100)
            {
                timer1.Enabled = false;
                Form1 form1 = new Form1();
                this.Hide();
                form1.Show();
            }
        }
    }
}
