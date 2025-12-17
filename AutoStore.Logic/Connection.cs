
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoStore.Logic
{
    public class Connection
    {
        private static readonly string _connString =
           "User Id=HR;Password=sys123;Data Source=localhost:1521/XE;";

        public static OracleConnection GetConnection()
        {
            var con = new OracleConnection(_connString);
            con.Open(); 
            return con;
        }
    }
}
