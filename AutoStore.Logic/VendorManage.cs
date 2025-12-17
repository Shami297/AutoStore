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
    public class VendorManage
    {
        Connection pr = new Connection();
        public List<Vendor> GetInvoice(int monthPicker, int yearPiker)
        {
            List<Vendor> vendors = new List<Vendor>();
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                string query = @"SELECT p.ID ,v.company||''||p.pI_DATE as Name from vendors v inner join purchaseinvoice p  on p.VENDORS_ID = v.ID where EXTRACT(month FROM p.pI_DATE)= " + monthPicker + " and EXTRACT(year FROM p.pI_DATE) = " + yearPiker + "";
                vendors = ORCL.Query<Vendor>(query).ToList();
            }
            catch { }
            finally { ORCL.Dispose(); }
            return vendors;
        }
    }
}
