using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace NTP_Projekt
{
    class LoginEncryption
    {

        public string RegisterInsertData(string email, string pass, string firstName, string lastName, string city, string country,
            string address, string dob, string jmbag)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.ntp_projektConnectionString);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO users (FirstName, LastName, Email, Password, Address, City, Country, DoB, JMBAG, RoleID) " +
                    "values (@firstName, @lastName,@email, @password, @address, @city, @country, @dob, @jmbag, 1);" +
                    "INSERT INTO Students (JMBAG, EnrollmentDate) values (@jmbag, @dob)";
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.Parameters.AddWithValue("@lastName", lastName);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@country", country);
                cmd.Parameters.AddWithValue("@dob", dob);
                cmd.Parameters.AddWithValue("@jmbag", jmbag);
                cmd.Parameters.AddWithValue("@password", HashString(pass));
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Account created!");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }

            return null;
        }

        public static string HashString(string passwordStr)
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
