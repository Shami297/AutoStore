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
    public class ProductManager
    {
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                string query = "select p.ID, p.NAME from products p";
                products = ORCL.Query<Product>(query).ToList();

            }
            catch { }
            finally { ORCL.Dispose(); }
            return products;
        } 
    }
}
