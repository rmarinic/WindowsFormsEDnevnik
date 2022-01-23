using Microsoft.Win32;
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
    public partial class Login : Form
    {
        LoginEncryption loginEncrypt = new LoginEncryption();
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text.Length < 3 || txtPassword.Text.Length < 3)
                MessageBox.Show("Email or password is invalid", "Information",MessageBoxButtons.OK, MessageBoxIcon.Warning);

            loginEncrypt.LoginSelectData(txtEmail.Text, txtPassword.Text);


            Main mainForm = new Main();
            mainForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminMain mn = new AdminMain();
            mn.ShowDialog();
        }




        // --------------------------------------------- SAVEANJE INI I REGISTRY ------------------------------------------------------
        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            var ini = new IniFile("settings.ini");
            //ini.Write("Left", this.Left.ToString());
            ini.Write("Top", this.Top.ToString());
            ini.Write("Width", this.Width.ToString());
            ini.Write("Height", this.Height.ToString());


            RegistryKey test = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\\NTPEdnevnik");
            test.SetValue("Left", this.Left.ToString());
        }

        public void Login_Load(object sender, EventArgs e)
        {
            RegistryKey test = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\\NTPEdnevnik");
            this.Left = int.Parse(test.GetValue("Left").ToString());

            var ini = new IniFile("settings.ini");

            if (ini.Read("FontMultiplier").Length != 0)
            {
                this.Font = new System.Drawing.Font(
                    "Microsoft Sans Serif",
                    8.5F * float.Parse(ini.Read("FontMultiplier")),
                    System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point,
                    ((byte)(0)));
            }



            //this.Left = int.Parse(ini.Read("Left"));
            this.Top = int.Parse(ini.Read("Top"));
            this.Width = int.Parse(ini.Read("Width"));
            this.Height = int.Parse(ini.Read("Height"));
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
    }
}
