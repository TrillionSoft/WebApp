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

        public ProductCategoryDOM selectProductCategory()
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

        public ProductCategoryDOM selectALLCategory()
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            con.Open();
            String sql = "SELECT PC.Type,ISNULL((SPC.Type),'') as 'Sub-Type' from Product_Category PC left Join Sub_Product_Category SPC on SPC.PC_ID = PC.ID";
            SqlCommand cmd = new SqlCommand(sql, con);

            List<string> Type = new List<string>();
            List<string> SubType = new List<string>();

            using (SqlDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    Type.Add(dataReader["Type"].ToString());
                    SubType.Add(dataReader["Sub-Type"].ToString());
                }
            }

            product.Type = Type;
            product.Description = SubType;

            con.Close();
            return product;
        }

        public int updateProductCategory(ProductCategoryDOM product)
        {
            int result = 0;
            try
            {
                String sql = "UPDATE Product_Category SET ";
                sql += "Type = @Type, Description = @Description, Deleted = @Deleted ";
                sql += "WHERE ID = @ID";
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand(sql, connection);

              
                cmd.Parameters.AddWithValue("@Type", product.Type);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }

            return result;
        }

        public int deleteProductByID(int ID)
        {
            int result = 0;

            try
            {
                String sql = "DELETE Product_Category ";
                sql += "WHERE ID = @ID";

                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@ID", ID);
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }

            return result;
        }
    }
}