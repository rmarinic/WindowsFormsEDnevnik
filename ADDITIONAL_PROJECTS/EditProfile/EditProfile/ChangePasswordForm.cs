using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditProfile
{
    public partial class ChangePasswordForm : Form
    {
        string connectionString = (@"Data Source=DESKTOP-9N8RF1B\SQLEXPRESS;Initial Catalog=ntp_projekt;Integrated Security=True");
        public string USER_JMBAG = "";
        public string EMAIL = "";
        public ChangePasswordForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pwd = textBox1.Text;
            string pwd2 = textBox2.Text;

            if (pwd.Length < 6 || pwd2.Length < 6)
                MessageBox.Show("Password is too short", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if(pwd.Equals(pwd2) && pwd.Length > 6)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    try
                    {
                        SqlCommand update_user = new SqlCommand();
                        update_user.CommandText = "UPDATE users SET password = @pass WHERE JMBAG = @jmbag;";
                        update_user.Parameters.AddWithValue("@jmbag", USER_JMBAG);
                        update_user.Parameters.AddWithValue("@pass", HashString(pwd, EMAIL));

                        update_user.Connection = conn;
                        update_user.ExecuteNonQuery();
                        MessageBox.Show("Password updated!");
                    }
                    catch
                    {
                        MessageBox.Show("Error");
                    }
                }
            }
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
