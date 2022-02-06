using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace NTPRestProject
{
    public class LoginHelper
    {
        public int LoginSelectData(string email, string pass)
        {
            int ret = 0;
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-9N8RF1B\SQLEXPRESS;Initial Catalog=ntp_projekt;Integrated Security=True");
            conn.Open();
            try
            {
                SqlCommand cmd;
                SqlDataReader reader;
                string sql = ("SELECT RoleID FROM USERS WHERE Email = @email AND Password = @pass");
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@pass", HashString(pass));
                reader = cmd.ExecuteReader();

                if (reader.Read())
                    ret = int.Parse(reader.GetValue(0).ToString());

                reader.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return ret;
        }

        static string HashString(string passwordStr)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(passwordStr))
                sb.Append(b.ToString("X3"));
            return sb.ToString();
        }


        public static byte[] GetHash(string passwordStr)
        {
            using (HashAlgorithm alg = SHA256.Create())
                return alg.ComputeHash(Encoding.UTF8.GetBytes(passwordStr));
        }
    }
}