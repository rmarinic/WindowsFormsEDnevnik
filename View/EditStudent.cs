using NTP_Projekt.Logic;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using EditProfile;
using System.Drawing;

namespace NTP_Projekt.View
{
    public partial class EditStudent : Form
    {
        public string USER_JMBAG = "";
        public Main form = null;
        string connectionString = (@"Data Source=DESKTOP-9N8RF1B\SQLEXPRESS;Initial Catalog=ntp_projekt;Integrated Security=True");
        Users user = new Users();
        public EditStudent()
        {
            InitializeComponent();
        }

        private void EditStudent_Load(object sender, EventArgs e)
        {
            user = DbHelper.GetUser(USER_JMBAG);
            txtJmbag.Text = user.JMBAG;
            txtFirstName.Text = user.FirstName;
            txtLastName.Text = user.LastName;
            txtAddress.Text = user.Address;
            txtCity.Text = user.City;
            txtCountry.Text = user.Country;
            dateTimePicker1.Value = (DateTime)user.DoB;
            txtEmail.Text = user.Email;
            DbHelper.LoadUserImage(pictureBox1);
        }

        public void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                try
                {
                    SqlCommand update_user = new SqlCommand();
                    update_user.CommandText = "UPDATE users SET FirstName = @firstName, LastName = @lastName, Email = @email, " +
                                           "Address = @address, City = @city, Country = @country, DoB = @dob WHERE JMBAG = @jmbag;";
                    update_user.Parameters.AddWithValue("@jmbag", user.JMBAG);
                    update_user.Parameters.AddWithValue("@firstName", txtFirstName.Text);
                    update_user.Parameters.AddWithValue("@lastName", txtLastName.Text);
                    update_user.Parameters.AddWithValue("@email", txtEmail.Text);
                    update_user.Parameters.AddWithValue("@dob", Convert.ToDateTime(dateTimePicker1.Value));
                    update_user.Parameters.AddWithValue("@address", txtAddress.Text);
                    update_user.Parameters.AddWithValue("@city", txtCity.Text);
                    update_user.Parameters.AddWithValue("@country", txtCountry.Text);
                    update_user.Connection = conn;
                    update_user.ExecuteNonQuery();

                    form.editReturnValue = 1;
                    form.RefreshProfile();
                }
                catch
                {
                    form.editReturnValue = 2;
                    form.RefreshProfile();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChangePasswordForm passForm = new ChangePasswordForm();
            passForm.USER_JMBAG = user.JMBAG;
            passForm.EMAIL = user.Email;
            passForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "All files|*.*" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(ofd.FileName);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    DbHelper.InsertImageToDb(pictureBox1);
                }
                    
            }
        }
    }
}
