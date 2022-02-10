using Microsoft.Win32;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RestSharp;
using NTP_Projekt.View;
using NTP_Projekt.Logic;

namespace NTP_Projekt
{
    public partial class Login : Form
    {
        LoginEncryption loginEncrypt = new LoginEncryption();
        IniFile ini = new IniFile("settings.ini");
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text.Length < 3 || txtPassword.Text.Length < 3)
                MessageBox.Show("Email or password is invalid", "Information",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                RestClient client = new RestClient("https://localhost:44387/Api");
                var request = new RestRequest("Authentication");
                request.AddParameter("email", txtEmail.Text);
                request.AddParameter("pass", txtPassword.Text);
                var response = client.ExecuteAsync(request);

                Globals.USER_JMBAG = response.Result.Content.Replace("\"", "");

                Users user = new Users();
                user = DbHelper.GetUser(Globals.USER_JMBAG);

                if (user.RoleID == 1)
                {
                    Main mainForm = new Main();
                    mainForm.Show();
                    this.Hide();
                }
                else if (user.RoleID == 2)
                {
                    ProfMain profForm = new ProfMain();
                    this.Hide();
                    profForm.ShowDialog();
                }
                else if(user.RoleID == 3)
                {
                    AdminMain adminForm = new AdminMain();
                    this.Hide();
                    adminForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Invalid email/password, please try again.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminMain mn = new AdminMain();
            mn.ShowDialog();
        }

        // --------------------------------------------- SAVEANJE INI I REGISTRY ------------------------------------------------------

        public void Login_Load(object sender, EventArgs e)
        {
            Logic.Appearance.RefreshForm(this);
            loginEncrypt.RegisterInsertData("admin@gmail.com", "admin", "Admin", "Admin",
               "Area 51", "HR", "Area 51", DateTime.Now.ToString(), "555566660", 3);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Settings sForm = new Settings();
            sForm.MyParent = this;
            sForm.ShowDialog();

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            pnlLogin.Visible = false;
            pnlRegister.Visible = true;
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            string testDate = DateTime.Now.ToString();
            loginEncrypt.RegisterInsertData(txtEmailReg.Text, txtPassReg.Text, txtFirstName.Text, txtLastName.Text, 
                txtCity.Text, txtCountry.Text, txtAddress.Text, testDate, RandomDigits(10), 1);
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            pnlRegister.Visible = false;
            pnlLogin.Visible = true;
        }


        public string RandomDigits(int length)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
    }
}
