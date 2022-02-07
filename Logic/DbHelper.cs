using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NTP_Projekt.Logic
{
    class DbHelper
    {
        public static Users GetUser(string jmbag)
        {
            Users user = new Users();
            string connectionString = NTP_Projekt.Properties.Settings.Default.ntp_projektConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                try
                {
                    SqlCommand cmd;
                    SqlDataReader reader;
                    string sql = ("SELECT * FROM USERS WHERE jmbag = @jmbag");
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@jmbag", jmbag);
                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        user.JMBAG = reader.GetValue(0).ToString();
                        user.FirstName = reader.GetValue(1).ToString();
                        user.LastName = reader.GetValue(2).ToString();
                        user.Address = reader.GetValue(3).ToString();
                        user.City = reader.GetValue(4).ToString();
                        user.Country = reader.GetValue(5).ToString();
                        user.DoB = DateTime.Now; // TEMP!
                        user.RoleID = int.Parse(reader.GetValue(7).ToString());
                        user.Email = reader.GetValue(8).ToString();
                        user.Password = reader.GetValue(9).ToString();
                    }

                    reader.Close();
                    cmd.Dispose();
                    return user;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "ERROR Loading database.");
                }
                finally
                {
                    conn.Close();
                }
            }
            return null;

        }
    }
}
