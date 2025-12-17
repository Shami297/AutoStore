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
    public class ProductSaleManager
    {
        public List<Product> GetSaleProducts()
        {
            List<Product> products = new List<Product>();
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                string query = "select p.ID, p.NAME from products p, stock s where p.id = s.PRODUCT_ID and s.quantity > 0";
                products = ORCL.Query<Product>(query).ToList();

            }
            catch { }
            finally { ORCL.Dispose(); }
            return products;
        }
    }
}
