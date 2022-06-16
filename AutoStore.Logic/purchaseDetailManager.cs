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
        Connection pr = new Connection();
        public List<purchaseDetails> GetPurchaseDetails(object monthPicker, object yearPiker)
        {
            List<purchaseDetails> purchaseDetails = new List<purchaseDetails>();
            string query = @"SELECT pu.id ,v.company||' '||pu.pi_date from purchaseinvoice pu 
            inner join vendors v  on v.id = pu.vendors_id 
            where EXTRACT(month FROM pu.pi_date)= " + monthPicker + " and EXTRACT(year FROM pu.pi_date) = " + yearPiker + "";
            pr.conn.Open();
            OleDbCommand command = new OleDbCommand(query, pr.conn);
            OleDbDataReader odr = command.ExecuteReader();

            while (odr.Read())
            {
                purchaseDetails purchase = new purchaseDetails();
                purchase.ID = Convert.ToInt32(odr.GetValue(0).ToString()); //odr.GetInt32(0);
                purchase.Name = odr.GetString(1);
                purchaseDetails.Add(purchase);
            }
            pr.conn.Close();
            return purchaseDetails;
        }
    }
}
