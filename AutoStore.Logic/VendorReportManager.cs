using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoStore.Logic
{
    public class VendorReportManager
    {
        Connection pr = new Connection();
        public List<Vendor> GetVendors()
        {
            List<Vendor> vendors = new List<Vendor>();
            string query = "select DISTINCT v.ID, v.COMPANY from vendors v, purchaseinvoice p where v.ID = p.VENDORS_ID";


            pr.conn.Open();
            OleDbCommand command = new OleDbCommand(query, pr.conn);
            OleDbDataReader odr = command.ExecuteReader();

            while (odr.Read())
            {
                Vendor ven = new Vendor();
                ven.ID = Convert.ToInt32(odr.GetValue(0).ToString()); //odr.GetInt32(0);
                ven.Name = odr.GetString(1);
                vendors.Add(ven);
            }
            pr.conn.Close();
            return vendors;
        }
    }
}
