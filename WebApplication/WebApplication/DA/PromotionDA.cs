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
    public class PromotionDA
    {
        PromotionDOM product = new PromotionDOM();

        public int InsertProduct(PromotionDOM product)
        {
            int result = 0;

            try
            {
                String sql = "Insert Into Promotion ";
                sql += " (Brand,Name,Unit_Price,Offer_Price,Offer_Percentage,Offer_Until_Date,Total_Price,Image) OUTPUT INSERTED.ID VALUES";
                sql += " (@Brand,@Name,@Unit_Price,@Offer_Price,@Offer_Percentage,@Offer_Until_Date,@Total_Price,@Image)";
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@Brand", product.Brand);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Unit_Price", product.Unit_Price);
                cmd.Parameters.AddWithValue("@Offer_Price", product.Offer_Price);
                cmd.Parameters.AddWithValue("@Offer_Percentage", product.Offer_Percentage);
                cmd.Parameters.AddWithValue("@Offer_Until_Date", product.Offer_Until_Date);
                cmd.Parameters.AddWithValue("@Total_Price", product.Total_Price);
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
    }
}