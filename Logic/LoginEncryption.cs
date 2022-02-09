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
            string address, string dob, string jmbag, int role)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.ntp_projektConnectionString);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO users (FirstName, LastName, Email, Password, Address, City, Country, DoB, JMBAG, RoleID) " +
                    "values (@firstName, @lastName,@email, @password, @address, @city, @country, @dob, @jmbag, @role);" +
                    "INSERT INTO Students (JMBAG, EnrollmentDate) values (@jmbag, @dob)";
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.Parameters.AddWithValue("@lastName", lastName);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@country", country);
                cmd.Parameters.AddWithValue("@dob", dob);
                cmd.Parameters.AddWithValue("@jmbag", jmbag);
                cmd.Parameters.AddWithValue("@password", HashString(pass, email));
                cmd.Parameters.AddWithValue("@role", role);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Account created!");
            }
            catch(Exception e)
            {
                if(role != 3)
                {
                    MessageBox.Show(e.Message);
                    return null;
                }
            }
            finally
            {
                conn.Close();
            }

            return null;
        }

        public static string HashString(string passwordStr,string email)
        {
            string salt = CreateSalt(email);
            string saltAndPwd = String.Concat(passwordStr, salt);
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(saltAndPwd));
            string hashedPwd = String.Concat(byteArrayToString(hashedDataBytes), salt);
            return hashedPwd;


            //StringBuilder sb = new StringBuilder();
            //foreach (byte b in GetHash(passwordStr))
            //    sb.Append(b.ToString("X3"));
            //return sb.ToString();
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
            Console.WriteLine(Encoding.Default.GetString(userBytes) + "<-- user bytes");
            foreach (int x in userBytes)
            {
                Console.WriteLine(XORED.ToString("X2") + "<-- XORED -- " + x.ToString("X2"));
                XORED = XORED ^ x;
            }
                
            
            Random rand = new Random(Convert.ToInt32(XORED));
            salt = rand.Next().ToString();
            salt += rand.Next().ToString();
            salt += rand.Next().ToString();
            salt += rand.Next().ToString();
            return salt;
        }
    }


}
