using NTP_Projekt.Logic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NTP_Projekt.View
{
    public partial class DownloadTest : Form
    {
        public delegate void ProgressChangedHandler(long? totalFileSize, long totalBytesDownloaded, double? progressPercentage);

        public event ProgressChangedHandler ProgressChanged;
        public DownloadTest()
        {
            InitializeComponent();
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            CountryInfoService.CountryInfoServiceSoapTypeClient SOAPclient = new CountryInfoService.CountryInfoServiceSoapTypeClient();
            var response = SOAPclient.CountryFlag("HR");
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "Resources");

            var destinationFilePath = Path.GetFullPath("Country.jpg");

            using (var client = new HttpClientDownloadWithProgress(response, destinationFilePath))
            {
                client.ProgressChanged += (totalFileSize, totalBytesDownloaded, progressPercentage) => {
                    progressBar1.Maximum = (int)totalFileSize;
                    progressBar1.Value = (int)totalBytesDownloaded;
                    label2.Text = $"{progressPercentage}% ({totalBytesDownloaded}/{totalFileSize})";
                };

                await client.StartDownload();
                pictureBox1.Image = Image.FromFile(destinationFilePath);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                InsertImageToDb(pictureBox1);
            }
        }

        public void InsertImageToDb(PictureBox picBox)
        {
            MemoryStream memStream = new MemoryStream();
            picBox.Image.Save(memStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] pic = memStream.ToArray();

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.ntp_projektConnectionString);
            conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UPDATE Students SET Image = @pic WHERE JMBAG=1234567888";
                cmd.Parameters.AddWithValue("@pic", pic);
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


        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.ntp_projektConnectionString);
            conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select Image from Students WHERE JMBAG=1234567888";
                cmd.Connection = conn;
                SqlDataAdapter dp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet("MyImages");
                MessageBox.Show("image inserted!");

                byte[] MyData = new byte[0];

                dp.Fill(ds, "MyImages");
                DataRow myRow;
                myRow = ds.Tables["MyImages"].Rows[0];

                MyData = (byte[])myRow["Image"];

                MemoryStream stream = new MemoryStream(MyData);
                //With the code below, you are in fact converting the byte array of image
                //to the real image.
                pictureBox2.Image = Image.FromStream(stream);
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
