using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WebApplication.Domain;

namespace WebApplication.DA
{
    public class UserDA
    {
        UserDOM user = new UserDOM();

        public UserDOM selectUserPassword(string LoginID)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            con.Open();
            String sql = "SELECT * From [User] where LoginID = @LoginID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@LoginID", LoginID);

            using (SqlDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    user.LoginID = dataReader["LoginID"].ToString();
                    user.LoginPassword = dataReader["LoginPassword"].ToString();
                }
            }
            con.Close();
            return user;
        }

    }
}