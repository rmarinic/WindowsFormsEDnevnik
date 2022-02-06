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
                bool loginFlag = loginEncrypt.LoginSelectData(txtEmail.Text, txtPassword.Text);

                if (loginFlag)
                {
                    Main mainForm = new Main();
                    this.Hide();
                    mainForm.ShowDialog();
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
            ////Prilikom zatvaranja forme, saveaj visinu, širinu i poziciju prozora u Windows Registry
            //RegistryKey test = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\\NTPEdnevnik");
            //test.SetValue("Left", this.Left.ToString());
            //test.SetValue("Top", this.Top.ToString());
            //test.SetValue("Width", this.Width.ToString());
            //test.SetValue("Height", this.Height.ToString());
        }

        public void Login_Load(object sender, EventArgs e)
        {
            ////Prilikom učitavanja forme, loadaj settingse prozora iz registrya i spremi ih u globalne varijable
            //RegistryKey registry = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\\NTPEdnevnik");
            //if(registry.GetValue("Left") != null && registry.GetValue("Top") != null && 
            //    registry.GetValue("Width") != null && registry.GetValue("Height") != null)
            //{
            //    Globals.WINDOW_POS_LEFT = registry.GetValue("Left").ToString();
            //    Globals.WINDOW_POS_TOP = registry.GetValue("Top").ToString();
            //    Globals.WINDOW_HEIGHT = registry.GetValue("Height").ToString();
            //    Globals.WINDOW_WIDTH = registry.GetValue("Width").ToString();
            //}

            //if (!String.IsNullOrEmpty(Globals.WINDOW_POS_LEFT))
            //{
            //    this.Left = int.Parse(Globals.WINDOW_POS_LEFT);
            //    this.Top = int.Parse(Globals.WINDOW_POS_TOP);
            //    this.Width = int.Parse(Globals.WINDOW_HEIGHT);
            //    this.Height = int.Parse(Globals.WINDOW_WIDTH);
            //}

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
    }
}
