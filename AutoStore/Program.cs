using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoStore
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ProgressBar());
            //Application.Run(new Form1());
            //Application.Run(new purchaseInvoice());
            //Application.Run(new purchaseInvoiceDetail());
            //Application.Run(new DashBoard());
            //Application.Run(new saleInvoice());
            //Application.Run(new SaleInvoiceDetail());
            //Application.Run(new Stock());
            //Application.Run(new users());
            //Application.Run(new Recover());
            //Application.Run(new SaleReturn());
            //Application.Run(new purchaseReturn());
            //Application.Run(new CustomerReport());
            //Application.Run(new VendorReport());
        }
    }
}
