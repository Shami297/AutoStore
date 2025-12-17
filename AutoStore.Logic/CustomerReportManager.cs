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
    public class CustomerReportManager
    {
        Connection pr = new Connection();
        public List<customer> GetCustomers()
        {
            List<customer> customers = new List<customer>();
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                string query = "select DISTINCT c.CSTMR_ID as ID, c.CSTMR_NAME as Name from customer c, saleinvoice s where c.cstmr_id = s.customer_id";
                customers = ORCL.Query<customer>(query).ToList();

            }
            catch { }
            finally { ORCL.Dispose(); }
            return customers;
        }
    }
}
