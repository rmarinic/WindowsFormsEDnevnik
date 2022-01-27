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
            string connectionString = NTP_Projekt.Properties.Settings.Default.ntp_projektConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    String sql = "SELECT Users.JMBAG, FirstName as [Name], LastName as [Surname], Email, Dob as [Date of birth]," +
                        " Address, City, EnrollmentDate as [Enrollment Date] FROM Users" +
                        " INNER JOIN Students on Users.JMBAG = Students.JMBAG WHERE Users.RoleID = 1";
                    conn.Open();
                    SqlCommand cmd;
                    SqlDataAdapter adapter;
                    cmd = new SqlCommand(sql, conn);
                    adapter = new SqlDataAdapter(sql, connectionString);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    cmd.Dispose();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "ERROR Loading");
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
