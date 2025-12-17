using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoStore
{
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
            customizeDesing();
            
        }

        private void customizeDesing()
        {
            panel3.Visible = false;
            panel4.Visible = false;
            product1.Hide();
            vendor1.Hide();
            customer1.Hide();
            categories1.Hide();
            pricing1.Hide();
            panel5.Visible = true;
        }

        private void hideSubMenu()
        {
            if (panel3.Visible == true) panel3.Visible = false;
            if (panel4.Visible == true) panel4.Visible = false;
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void  productBtn()
        {
            panel5.Visible = true;
            product1.Show();
            vendor1.Hide();
            vendor1.Refresh();
            customer1.Hide();
            customer1.Refresh();
            categories1.Hide();
            categories1.Refresh();
            pricing1.Hide();
            pricing1.Refresh();
            hideSubMenu();
        }

        private void vendorBtn()
        {
            panel5.Visible = true;
            product1.Hide();
            product1.Refresh();
            customer1.Hide();
            customer1.Refresh();
            categories1.Hide();
            categories1.Refresh();
            pricing1.Hide();
            vendor1.Show();
            hideSubMenu();
        }

        private void customerBtn()
        {
            panel5.Visible = true;
            product1.Hide();
            product1.Refresh();
            vendor1.Hide();
            vendor1.Refresh();
            categories1.Hide();
            categories1.Refresh();
            pricing1.Hide();
            pricing1.Refresh();
            customer1.Show();
            hideSubMenu();
        }

        private void categoryBtn()
        {
            panel5.Visible = true;
            product1.Hide();
            product1.Refresh();
            vendor1.Hide();
            vendor1.Refresh();
            customer1.Hide();
            customer1.Refresh();
            pricing1.Hide();
            pricing1.Refresh();
            categories1.Show();
            hideSubMenu();
        }

        private void pricingBtn()
        {
            panel5.Visible = true;
            product1.Hide();
            product1.Refresh();
            vendor1.Hide();
            vendor1.Refresh();
            customer1.Hide();
            customer1.Refresh();
            categories1.Hide();
            categories1.Refresh();
            pricing1.Show();
            hideSubMenu();
        }

        private void IconButton1_Click(object sender, EventArgs e)
        {
            showSubMenu(panel3);
        }

        private void IconButton5_Click(object sender, EventArgs e)
        {
            showSubMenu(panel4);
        }

        private void IconButton2_Click(object sender, EventArgs e)
        {
            productBtn();
        }

        private void IconButton3_Click(object sender, EventArgs e)
        {
            vendorBtn();
        }

        private void IconButton4_Click(object sender, EventArgs e)
        {
            customerBtn();
        }

        private void purchaseIn()
        {
            customizeDesing();
            purchaseInvoice Pur = new purchaseInvoice();
            Pur.Show();
        }

        private void saleIn()
        {
            customizeDesing();
            saleInvoice sal = new saleInvoice();
            sal.Show();
        }

        private void users()
        {
            customizeDesing();
            users usr = new users();
            usr.Show();
        }


        private void IconButton6_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            purchaseIn();
            this.Close();
        }

        private void IconButton7_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            purchaseReturn purchase = new purchaseReturn();
            this.Close();
            purchase.Show();
        }

        private void IconButton8_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            saleIn();
            this.Close();
        }

        private void IconButton9_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            SaleReturn saleReturn = new SaleReturn();
            this.Close();
            saleReturn.Show();
        }

        private void IconButton10_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void IconButton11_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void BunifuLabel2_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            productBtn();
        }

        private void CustomersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            customerBtn();
        }

        private void VendorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vendorBtn();
        }

        private void PurchaseInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            purchaseIn();
            this.Close();
        }

        private void Customer1_Load(object sender, EventArgs e)
        {

        }

        private void SaleInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saleIn();
            this.Close();
        }

        private void iconButton10_Click_1(object sender, EventArgs e)
        {
            categoryBtn();
        }

        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            categoryBtn();
        }

        private void iconButton11_Click_1(object sender, EventArgs e)
        {
            pricingBtn();
        }

        private void productPricingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports reports = new Reports();
            reports.Show();
            this.Close();
        }

        private void iconButton13_Click(object sender, EventArgs e)
        {
            users();
            this.Close();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            users();
            this.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DashBoard_Load(object sender, EventArgs e)
        {
            Main1.date(date);
        }

        private void iconButton12_Click(object sender, EventArgs e)
        {
            Stock stock = new Stock();
            this.Close();
            stock.Show();
        }

        private void iconButton11_Click_2(object sender, EventArgs e)
        {
            Reports reports = new Reports();
            reports.Show();
            this.Close();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
