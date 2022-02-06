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

                int userRole = int.Parse(response.Result.Content.Replace("\"", ""));

                if (userRole == 1)
                {
                    Main mainForm = new Main();
                    this.Hide();
                    mainForm.ShowDialog();
                }
                else if (userRole == 2)
                {
                    ProfMain profForm = new ProfMain();
                    this.Hide();
                    profForm.ShowDialog();
                }
                else if(userRole == 3)
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
        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        public void Login_Load(object sender, EventArgs e)
        {

            Logic.Appearance.RefreshForm(this);
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
                txtCity.Text, txtCountry.Text, txtAddress.Text, testDate, txtJmbag.Text);
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            pnlRegister.Visible = false;
            pnlLogin.Visible = true;
        }

        private void btnSoap_Click(object sender, EventArgs e)
        {
            //CountryInfoService.CountryInfoServiceSoapTypeClient client = new CountryInfoService.CountryInfoServiceSoapTypeClient();
            //var response = client.CountryFlag("CRO");

            NTPSoapService.NTPSoapSoapClient client = new NTPSoapService.NTPSoapSoapClient();

            //var response = client.ConvertXmlToJson("<note><to>Tove</to><from>Jani</from><heading>Reminder</heading><body>Don't forget me this weekend!</body></note>");
            string json = @"{
                '@Id': 1,
                'Email': 'james@example.com',
                'Active': true,
                'CreatedDate': '2013-01-20T00:00:00Z',
                'Roles': [
                'User',
                'Admin'
                ],
                'Team': {
                '@Id': 2,
                'Name': 'Software Developers',
                'Description': 'Creators of fine software products and services.'
                }
                }";
            var response = client.ConvertJsonToXml(json);
            MessageBox.Show(response);
        }

        private void btnRest_Click(object sender, EventArgs e)
        {
            RESTGetDateTime();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DownloadTest dForm = new DownloadTest();
            dForm.ShowDialog();
        }


        private string RESTGetDateTime()
        {
            RestClient client = new RestClient("https://localhost:44387/Api");
            var request = new RestRequest("GetLocalDateTime");
            var response = client.ExecuteAsync(request);

            if (response.Result != null)
            {
                string rawResponse = response.Result.Content;
                label1.Text = rawResponse;
                return rawResponse;
            }
            return null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RestClient client = new RestClient(@"https://api.covid19api.com/");
            var request = new RestRequest("summary");
            var response = client.ExecuteAsync(request);

            if(response.Result != null)
            {
                string rawResponse = response.Result.Content;
                dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject(rawResponse);
                label1.Text = "COVID-19 Statistics\nNew cases: " + json["Global"]["NewConfirmed"] + "\nNew deaths: " + json["Global"]["NewDeaths"];
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Main mainForm = new Main();
            this.Hide();
            mainForm.ShowDialog();
        }
    }
}
