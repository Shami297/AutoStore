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
    public class SaleDetailManager
    {
        public List<SaleDetails> GetSaleDetails(int monthPicker, int yearPiker)
        {
            List<SaleDetails> saleDetails = new List<SaleDetails>();
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                string query = @"SELECT si.id as ID ,c.CSTMR_NAME||' '||si.SI_DATE as Name from saleinvoice si 
            inner join customer c  on c.CSTMR_ID = si.CUSTOMER_ID 
            where EXTRACT(month FROM si.SI_DATE)= " + monthPicker + " and EXTRACT(year FROM si.SI_DATE) = " + yearPiker + "";
                saleDetails = ORCL.Query<SaleDetails>(query).ToList();
            }
            catch { }
            finally { ORCL.Dispose(); }
            return saleDetails;
        }
    }
}
