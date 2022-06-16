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
        Connection pr = new Connection();
        public List<SaleDetails> GetSaleDetails(int monthPicker, int yearPiker)
        {
            List<SaleDetails> saleDetails = new List<SaleDetails>();
            string query = @"SELECT si.id ,c.CSTMR_NAME||' '||si.SI_DATE from saleinvoice si 
            inner join customer c  on c.CSTMR_ID = si.CUSTOMER_ID 
            where EXTRACT(month FROM si.SI_DATE)= " + monthPicker + " and EXTRACT(year FROM si.SI_DATE) = " + yearPiker + "";
            pr.conn.Open();
            OleDbCommand command = new OleDbCommand(query, pr.conn);
            OleDbDataReader odr = command.ExecuteReader();

            while (odr.Read())
            {
                SaleDetails sale = new SaleDetails();
                sale.ID = Convert.ToInt32(odr.GetValue(0).ToString()); //odr.GetInt32(0);
                sale.Name = odr.GetString(1);
                saleDetails.Add(sale);
            }
            pr.conn.Close();
            return saleDetails;
        }
    }
}
