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
            string query = "select DISTINCT c.CSTMR_ID, c.CSTMR_NAME from customer c, saleinvoice s where c.cstmr_id = s.customer_id";


            pr.conn.Open();
            OleDbCommand command = new OleDbCommand(query, pr.conn);
            OleDbDataReader odr = command.ExecuteReader();

            while (odr.Read())
            {
                customer cus = new customer();
                cus.ID = Convert.ToInt32(odr.GetValue(0).ToString()); //odr.GetInt32(0);
                cus.Name = odr.GetString(1);
                customers.Add(cus);
            }
            pr.conn.Close();
            return customers;
        }
    }
}
