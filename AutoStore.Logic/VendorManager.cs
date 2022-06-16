using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoStore.Logic
{
    public class VendorManager
    {
        public List<Vendor> GetVendors()
        {
            List<Vendor> vendors = new List<Vendor>();
            string query = "select v.ID, v.COMPANY from vendors v";
            OleDbConnection connection = new OleDbConnection("Provider=MSDAORA;Data Source=ORCL;Persist Security Info=True;User ID=HR;Password=HR;Unicode=True");
            connection.Open();
            OleDbCommand command = new OleDbCommand(query, connection);
            OleDbDataReader odr = command.ExecuteReader();

            while (odr.Read())
            {
                Vendor vendor = new Vendor();
                vendor.ID = Convert.ToInt32(odr.GetValue(0).ToString()); //odr.GetInt32(0);
                vendor.Name = odr.GetString(1);
                vendors.Add(vendor);
            }
            connection.Close();
            return vendors;
        }
    }
}
