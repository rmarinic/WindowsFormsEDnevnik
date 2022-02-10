using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
                        user.JMBAG = reader.GetValue(0).ToString().Trim();
                        user.FirstName = reader.GetValue(1).ToString().Trim();
                        user.LastName = reader.GetValue(2).ToString().Trim();
                        user.Address = reader.GetValue(3).ToString().Trim();
                        user.City = reader.GetValue(4).ToString().Trim();
                        user.Country = reader.GetValue(5).ToString();
                        user.DoB = Convert.ToDateTime(reader.GetValue(6).ToString().Trim()); // TEMP!
                        user.RoleID = int.Parse(reader.GetValue(7).ToString().Trim());
                        user.Email = reader.GetValue(8).ToString().Trim();
                        user.Password = reader.GetValue(9).ToString().Trim();
                        user.Age = Convert.ToInt32(reader.GetValue(10).ToString().Trim());
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

        public static void InsertImageToDb(PictureBox picBox)
        {
            MemoryStream memStream = new MemoryStream();
            picBox.Image.Save(memStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] pic = memStream.ToArray();

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.ntp_projektConnectionString);
            conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UPDATE Students SET Image = @pic WHERE JMBAG=@jmbag";
                cmd.Parameters.AddWithValue("@pic", pic);
                cmd.Parameters.AddWithValue("@jmbag", Globals.USER_JMBAG);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                MessageBox.Show("image inserted!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void LoadUserImage(PictureBox pictureBox1)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.ntp_projektConnectionString);
            conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select Image from Students WHERE JMBAG=@jmbag";
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@jmbag", Globals.USER_JMBAG);
                SqlDataAdapter dp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet("MyImages");

                byte[] MyData = new byte[0];

                dp.Fill(ds, "MyImages");
                DataRow myRow;
                myRow = ds.Tables["MyImages"].Rows[0];

                MyData = (byte[])myRow["Image"];

                MemoryStream stream = new MemoryStream(MyData);
                //With the code below, you are in fact converting the byte array of image
                //to the real image.
                pictureBox1.Image = Image.FromStream(stream);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
