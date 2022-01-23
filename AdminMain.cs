using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NTP_Projekt
{
    public partial class AdminMain : Form
    {
        public AdminMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> fonts = new List<string>();

            foreach (FontFamily font in System.Drawing.FontFamily.Families)
            {
                comboBox1.Items.Add(font.Name);
            }

            pictureBox1.ImageLocation = "http://www.oorsprong.org/WebSamples.CountryInfo/Flags/Croatia.jpg";
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

            string connectionString;
            SqlConnection cnn;

            connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ntp_ednevnik;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            cnn = new SqlConnection(connectionString);

            cnn.Open();


            SqlCommand cmd;
            SqlDataAdapter adapter;
            String sql = "SELECT * FROM USERS";

            cmd = new SqlCommand(sql, cnn);
            adapter = new SqlDataAdapter(sql, connectionString);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;

            cmd.Dispose();
            cnn.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Font = new System.Drawing.Font(
                      comboBox1.SelectedItem.ToString(),
                      8.5F,
                      System.Drawing.FontStyle.Regular,
                      System.Drawing.GraphicsUnit.Point,
                      ((byte)(0)));


        }
    }
}
