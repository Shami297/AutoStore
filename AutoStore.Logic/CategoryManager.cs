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
            string query = "select CAT_ID, CAT_NAME from categories WHERE CAT_ISACTIVE = 'Yes'";
            OleDbConnection connection = new OleDbConnection("Provider=MSDAORA;Data Source=ORCL;Persist Security Info=True;User ID=HR;Password=HR;Unicode=True");
            connection.Open();
            OleDbCommand command = new OleDbCommand(query, connection); 
            OleDbDataReader odr = command.ExecuteReader();

            while (odr.Read())
            {
                Category category = new Category();
                category.ID = Convert.ToInt32(odr.GetValue(0).ToString()); //odr.GetInt32(0);
                category.Name = odr.GetString(1);
                categories.Add(category);
            }
            connection.Close();
            return categories;
        }
    }
}
