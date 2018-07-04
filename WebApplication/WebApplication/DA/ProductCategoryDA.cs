using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WebApplication.Domain;

namespace WebApplication.DA
{
    public class ProductCategoryDA
    {
        ProductCategoryDOM product = new ProductCategoryDOM();

        public ProductCategoryDOM selectALLProductCategory()
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            con.Open();
            String sql = "SELECT * From Product_Category";
            SqlCommand cmd = new SqlCommand(sql, con);

            List<int> ID = new List<int>();
            List<string> Type = new List<string>();
            List<string> Description = new List<string>();
            List<bool> Deleted = new List<bool>();

            using (SqlDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    ID.Add((int)dataReader["ID"]);
                    Type.Add(dataReader["Type"].ToString());
                    Description.Add(dataReader["Description"].ToString());
                    Deleted.Add((bool)dataReader["Deleted"]);
                }
            }
            product.ID = ID;
            product.Type = Type;
            product.Description = Description;
            product.Deleted = Deleted;

            con.Close();
            return product;
        }
    }
}