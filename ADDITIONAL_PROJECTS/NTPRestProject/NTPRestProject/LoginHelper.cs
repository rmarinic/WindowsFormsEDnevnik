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
        public string LoginSelectData(string email, string pass)
        {
            string ret = "";
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-E0LVP3A;Initial Catalog=ntp_projekt;Integrated Security=True");
            conn.Open();
            try
            {
                SqlCommand cmd;
                SqlDataReader reader;
                string sql = ("SELECT JMBAG FROM USERS WHERE Email = @email AND Password = @pass");
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@pass", HashString(pass, email));
                reader = cmd.ExecuteReader();

                if (reader.Read())
                    ret = reader.GetValue(0).ToString();

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

        public static string HashString(string passwordStr, string email)
        {
            string salt = CreateSalt(email);
            string saltAndPwd = String.Concat(passwordStr, salt);
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(saltAndPwd));
            string hashedPwd = String.Concat(byteArrayToString(hashedDataBytes), salt);
            return hashedPwd;
        }

        public static string byteArrayToString(byte[] inputArray)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("X2"));
            }
            return output.ToString();
        }

        public static byte[] GetHash(string passwordStr)
        {
            using (HashAlgorithm alg = SHA256.Create())
                return alg.ComputeHash(Encoding.UTF8.GetBytes(passwordStr));
        }

        private static string CreateSalt(string email)
        {
            string salt;
            byte[] userBytes = ASCIIEncoding.ASCII.GetBytes(email);
            long XORED = 0x00;

            foreach (int x in userBytes)
                XORED = XORED ^ x;

            Random rand = new Random(Convert.ToInt32(XORED));
            salt = rand.Next().ToString();
            salt += rand.Next().ToString();
            salt += rand.Next().ToString();
            salt += rand.Next().ToString();
            return salt;
        }
    }
}