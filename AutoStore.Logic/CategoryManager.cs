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
    public class CategoryManager
    {
        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            OracleConnection ORCL = Connection.GetConnection();
            try
            {
                string query = "select CAT_ID as ID, CAT_NAME as NAME from categories WHERE CAT_ISACTIVE = 'Yes'";
                categories = ORCL.Query<Category>(query).ToList();
            }
            catch { }
            finally { ORCL.Dispose(); }
            return categories;
        }
    }
}
