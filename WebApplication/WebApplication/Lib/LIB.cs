using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace WebApplication.Lib
{
    public class LIB
    {
        //Insert Function no matter what table
        #region Insert Function
        public static int insertFunction(object ClassObject, string TableName)
        {
            try
            {
                string FieldName = "(";
                string FieldValues = "(";
                SqlCommand cmd = new SqlCommand();
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                connection.Open();
                var a = ClassObject.GetType().GetProperties().ToList();

                for (int i = 1; i < a.Count; i++)
                {
                    if (FieldName.Length > 1) FieldName += ",";
                    if (FieldValues.Length > 1) FieldValues += ",";

                    FieldName += a[i].Name;
                    FieldValues += "@" + a[i].Name;

                    cmd.Parameters.AddWithValue("@" + a[i].Name, ClassObject.GetType().GetProperty(a[i].Name).GetValue(ClassObject));
                    System.Diagnostics.Debug.WriteLine(ClassObject.GetType().GetProperty(a[i].Name).GetValue(ClassObject));
                }

                FieldName += ")";
                FieldValues += ")";

                string InsertQuery = "INSERT INTO " + TableName + " " + FieldName + " OUTPUT INSERTED.ID VALUES " + FieldValues;
                cmd.CommandText = InsertQuery;

                cmd.Connection = connection;
                connection.Close();
                return (int)cmd.ExecuteScalar();
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return 0;
            }
        }
        #endregion
        // Update Function no matter what table
        #region Update Function
        public static int updateFunction(object ClassObject, string TableName)
        {
            try
            {
                string text = "";
                SqlCommand cmd = new SqlCommand();
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                connection.Open();
                var a = ClassObject.GetType().GetProperties().ToList();

                for (int i = 0; i < a.Count; i++)
                {
                    if (a[i].Name != "ID" && a[i].Name != "Created_By" && a[i].Name != "Created_Date")
                    {
                        if (text.Length > 1) text += ", ";
                        text += a[i].Name;
                        text += " = @" + a[i].Name;
                    }

                 cmd.Parameters.AddWithValue("@" + a[i].Name, ClassObject.GetType().GetProperty(a[i].Name).GetValue(ClassObject));
                }

                string UpdateQuery = "UPDATE " + TableName + " SET " + text + " WHERE ID = @ID ";
                cmd.CommandText = UpdateQuery;
                cmd.Connection = connection;
                connection.Close();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
        #endregion
        // Selet Function no matter what table
        #region Select Function
        public static void selectFunction(object ClassObject, string TableName)
        {
            try
            {
                var obj = ClassObject.GetType().GetProperties().ToList();
                String sql = "SELECT * FROM " + TableName + " WHERE ID = @ID";
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@ID", ClassObject.GetType().GetProperty("ID").GetValue(ClassObject));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        for (int i = 0; i < obj.Count; i++)
                        {
                            if (obj[i].PropertyType == typeof(String))
                                ClassObject.GetType().GetProperty(obj[i].Name).SetValue(ClassObject, dr[obj[i].Name].ToString());
                            else if (obj[i].PropertyType == typeof(Int32))
                                ClassObject.GetType().GetProperty(obj[i].Name).SetValue(ClassObject, (int)dr[obj[i].Name]);
                            else if (obj[i].PropertyType == typeof(Double))
                                ClassObject.GetType().GetProperty(obj[i].Name).SetValue(ClassObject, Double.Parse(dr[obj[i].Name].ToString()));
                            else if (obj[i].PropertyType == typeof(Boolean))
                                ClassObject.GetType().GetProperty(obj[i].Name).SetValue(ClassObject, Boolean.Parse(dr[obj[i].Name].ToString()));
                            else if (obj[i].PropertyType == typeof(Decimal))
                                ClassObject.GetType().GetProperty(obj[i].Name).SetValue(ClassObject, Decimal.Parse(dr[obj[i].Name].ToString()));
                            else if (obj[i].PropertyType == typeof(DateTime))
                                ClassObject.GetType().GetProperty(obj[i].Name).SetValue(ClassObject, DateTime.Parse(dr[obj[i].Name].ToString()));
                        }
                    }
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

        }
        #endregion
    }
}