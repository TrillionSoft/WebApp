using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WebApplication.Domain;

namespace WebApplication.DA
{
    public class ProductDA
    {
        ProductDOM product = new ProductDOM();

        public int InsertProduct(ProductDOM product)
        {
            int result = 0;
           
            try
            {
                String sql = "Insert Into Product ";
                sql += " (PC_ID,SPC_ID,Brand,Description,Production_Country,Material,Deleted,Image) OUTPUT INSERTED.ID VALUES";
                sql += " (@PC_ID,@SPC_ID,@Brand,@Description,@Production_Country,@Material,@Deleted,@Image)";
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@PC_ID", product.PC_ID);
                cmd.Parameters.AddWithValue("@SPC_ID", product.SPC_ID);
                cmd.Parameters.AddWithValue("@Brand", product.Brand);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@Production_Country", product.Production_Country);
                cmd.Parameters.AddWithValue("@Material", product.Material);
                cmd.Parameters.AddWithValue("@Deleted", product.Deleted);
                cmd.Parameters.AddWithValue("@Image", SqlDbType.Image).Value = product.Image;

                result = (int)cmd.ExecuteScalar();
                connection.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return 0;
            }

            return result;
        }

        

        public ProductDOM selectProduct(int Product_ID)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            String sql = "SELECT * From Product";
            SqlCommand cmd = new SqlCommand(sql, con);

            using (SqlDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    product.ID = (int)dataReader["ID"];
                    product.PC_ID = (int)dataReader["PC_ID"];
                    product.SPC_ID = (int)dataReader["SPC_ID"];
                    product.Brand = dataReader["Brand"].ToString();
                    product.Description = dataReader["Description"].ToString();
                    product.Production_Country = dataReader["Production_Country"].ToString();
                    product.Material = dataReader["Material"].ToString();
                    product.Deleted = (bool)dataReader["Deleted"];
                    product.Image = (byte[])dataReader["Image"];
                }
            }
            con.Close();
            return product;
        }
    }
}