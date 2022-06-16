using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace AutoStore.Logic
{
    public class DatabaseConnection
    {
        public OleDbConnection GetConnection()
        {
            OleDbConnection conn = new OleDbConnection("Provider=MSDAORA;Data Source=ORCL;Persist Security Info=True;User ID=HR;Password=HR;Unicode=True");
            return conn;
        }
    }
}
