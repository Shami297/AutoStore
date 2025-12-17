using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Text;
using System.Threading.Tasks;

namespace AutoStore.Logic
{
    public class CustomerManage
    {
        Connection pr = new Connection();
        public List<customer> GetInvoice(int monthPicker, int yearPiker)
        {
            OracleConnection ORCL = Connection.GetConnection();
            List<customer> user = new List<customer>();
            try
            {
                
                string query = @"SELECT s.ID ,u.CSTMR_NAME||''||s.SI_DATE AS Name from customer u 
            inner join saleinvoice s  on s.CUSTOMER_ID = u.CSTMR_ID
            where EXTRACT(month FROM s.SI_DATE)= " + monthPicker + " and EXTRACT(year FROM s.SI_DATE) = " + yearPiker + "";
                user = ORCL.Query<customer>(query).ToList();
            }
            catch { }
            finally { ORCL.Dispose(); }
            return user;
        }

    }
}
