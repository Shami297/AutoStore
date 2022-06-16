using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoStore.Logic
{
    public class VendorManage
    {
        Connection pr = new Connection();
        public List<Vendor> GetInvoice(int monthPicker, int yearPiker)
        {
            List<Vendor> vendors = new List<Vendor>();
            string query = @"SELECT p.ID ,v.company||''||p.pI_DATE from vendors v 
            inner join purchaseinvoice p  on p.VENDORS_ID = v.ID
            where EXTRACT(month FROM p.pI_DATE)= " + monthPicker + " and EXTRACT(year FROM p.pI_DATE) = " + yearPiker + "";
            pr.conn.Open();
            OleDbCommand command = new OleDbCommand(query, pr.conn);
            OleDbDataReader odr = command.ExecuteReader();

            while (odr.Read())
            {
                Vendor vendor = new Vendor();
                vendor.ID = Convert.ToInt32(odr.GetValue(0).ToString()); //odr.GetInt32(0);
                vendor.Name = odr.GetString(1);
                vendors.Add(vendor);
            }
            pr.conn.Close();
            return vendors;
        }
    }
}
