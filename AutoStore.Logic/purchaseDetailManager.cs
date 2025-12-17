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
    public class purchaseDetailManager
    {
        public List<purchaseDetails> GetPurchaseDetails(object monthPicker, object yearPiker)
        {
            List<purchaseDetails> purchaseDetails = new List<purchaseDetails>();
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                string query = @"SELECT pu.id as ID ,v.company||' '||pu.pi_date as Name from purchaseinvoice pu inner join vendors v  on v.id = pu.vendors_id where EXTRACT(month FROM pu.pi_date)= " + monthPicker + " and EXTRACT(year FROM pu.pi_date) = " + yearPiker + "";
                purchaseDetails = ORCL.Query<purchaseDetails>(query).ToList();
            }
            catch { }
            finally { ORCL.Dispose(); }
            return purchaseDetails;
        }
    }
}
