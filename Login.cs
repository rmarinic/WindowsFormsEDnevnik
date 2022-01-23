using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace NTP_Projekt
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            string path = @"Data Source=DESKTOP-I5DQ78V;Initial Catalog=ntp_projekt;Integrated Security=True";
            
            SqlConnection conn;
            conn = new SqlConnection(path);

            conn.Open();
            SqlCommand cmd;
            SqlDataReader reader;
            String sql = "SELECT * FROM Users WHERE RoleID = 3";
            String output = "";
            cmd = new SqlCommand(sql, conn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                output = output + reader.GetString(0) +  " - " + reader.GetString(1);
            }
            MessageBox.Show(output);           

            reader.Close();
            cmd.Dispose();
            conn.Close();   

            Main mn = new Main();
            mn.Show();
        }
    }
}
