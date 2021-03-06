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
        Connection pr = new Connection();
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            string query = "select p.ID, p.NAME from products p";


            pr.conn.Open();
            OleDbCommand command = new OleDbCommand(query, pr.conn);
            OleDbDataReader odr = command.ExecuteReader();

            while (odr.Read())
            {
                Product product = new Product();
                product.ID = Convert.ToInt32(odr.GetValue(0).ToString()); //odr.GetInt32(0);
                product.Name = odr.GetString(1);
                products.Add(product);
            }
            pr.conn.Close();
            return products;
        } 
    }
}
