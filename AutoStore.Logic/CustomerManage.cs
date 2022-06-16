using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoStore.Logic
{
    public class CustomerManage
    {
        Connection pr = new Connection();
        public List<customer> GetInvoice(int monthPicker, int yearPiker)
        {
            List<customer> user = new List<customer>();
            string query = @"SELECT s.ID ,u.CSTMR_NAME||''||s.SI_DATE from customer u 
            inner join saleinvoice s  on s.CUSTOMER_ID = u.CSTMR_ID
            where EXTRACT(month FROM s.SI_DATE)= " + monthPicker + " and EXTRACT(year FROM s.SI_DATE) = " + yearPiker + "";
            pr.conn.Open();
            OleDbCommand command = new OleDbCommand(query, pr.conn);
            OleDbDataReader odr = command.ExecuteReader();

            while (odr.Read())
            {
                customer cust = new customer();
                cust.ID = Convert.ToInt32(odr.GetValue(0).ToString()); //odr.GetInt32(0);
                cust.Name = odr.GetString(1);
                user.Add(cust);
            }
            pr.conn.Close();
            return user;
        }
    }
}
