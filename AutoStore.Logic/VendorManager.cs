using Dapper;
using Oracle.ManagedDataAccess.Client;
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
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                string query = "select v.ID, v.COMPANY as Name from vendors v";
                vendors = ORCL.Query<Vendor>(query).ToList();
            }
            catch { }
            finally { ORCL.Dispose(); }
            return vendors;
        }
    }
}
