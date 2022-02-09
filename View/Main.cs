using Microsoft.Win32;
using NTP_Projekt.Logic;
using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NTPDynamicLibrary;
using RestSharp;
using System.Data.SqlClient;
using System.IO;
using NTP_Projekt.View;

namespace NTP_Projekt
{
    public partial class Main : Form
    {
        IniFile ini = new IniFile("settings.ini");
        Users user = new Users();
        public int editReturnValue = 0;
        public Main()
        {
            InitializeComponent();
        }

        private void cbxGrades_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbxGrades.SelectedIndex)
            {
                case 0:
                    HideAll();
                    ShowGrades();
                    pnlGrades.Visible = true;
                    break;
                case 1:
                    HideAll();
                    ShowCourses();
                    pnlSubjects.Visible = true;
                    break;
                case 2:
                    HideAll();
                    pnlAllGrades.Visible = true;
                    break;
                case 3:
                    HideAll();
                    pnlProfile.Visible = true;
                    ShowProfile();
                    break;
            }
        }

        private void HideAll()
        {
            pnlGrades.Visible = false;
            pnlSubjects.Visible = false;
            pnlAllGrades.Visible = false;
            pnlHome.Visible = false;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            user = Logic.DbHelper.GetUser(Globals.USER_JMBAG);
            label1.Text = "Dobrodošli u NTP Ednevnik\n" + user.FirstName + " " + user.LastName;
            label1.TextAlign = ContentAlignment.TopCenter;
            //Prilikom učitavanja forme, loadaj settingse prozora iz registrya i spremi ih u globalne varijable
            RegistryKey registry = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\\NTPEdnevnik");
            if (registry.GetValue("Left") != null && registry.GetValue("Top") != null &&
                registry.GetValue("Width") != null && registry.GetValue("Height") != null)
            {
                Globals.WINDOW_POS_LEFT = registry.GetValue("Left").ToString();
                Globals.WINDOW_POS_TOP = registry.GetValue("Top").ToString();
                Globals.WINDOW_HEIGHT = registry.GetValue("Height").ToString();
                Globals.WINDOW_WIDTH = registry.GetValue("Width").ToString();
            }

            if (!String.IsNullOrEmpty(Globals.WINDOW_POS_LEFT))
            {
                this.Left = int.Parse(Globals.WINDOW_POS_LEFT);
                this.Top = int.Parse(Globals.WINDOW_POS_TOP);
                this.Width = int.Parse(Globals.WINDOW_HEIGHT);
                this.Height = int.Parse(Globals.WINDOW_WIDTH);
            }
            Logic.Appearance.RefreshForm(this);
            ShowCovidStatistics();
            DbHelper.LoadUserImage(pictureBox1);
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Prilikom zatvaranja forme, saveaj visinu, širinu i poziciju prozora u Windows Registry
            RegistryKey test = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\\NTPEdnevnik");
            test.SetValue("Left", this.Left.ToString());
            test.SetValue("Top", this.Top.ToString());
            test.SetValue("Width", this.Width.ToString());
            test.SetValue("Height", this.Height.ToString());
        }

        private void ShowCourses()
        {
            throw new NotImplementedException();
        }

        private void ShowGrades()
        {
            string connectionString = Properties.Settings.Default.ntp_projektConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    String sql = @" SELECT g.Score, g.Type, c.Name
                                    FROM Grades g 
                                    INNER JOIN Courses c on g.CourseID = c.CourseID 
                                    WHERE g.StudentJMBAG = " + Globals.USER_JMBAG;
                    conn.Open();
                    SqlDataAdapter adapter;
                    adapter = new SqlDataAdapter(sql, connectionString);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].HeaderText = "Score";
                    dataGridView1.Columns[1].HeaderText = "Exam";
                    dataGridView1.Columns[2].HeaderText = "Course";
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
        }




        // ------------------------------ ENCRYPT / DECRYPT --------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "All files|*.*" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                    txtFileName.Text = ofd.FileName;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string password = "+9j?5DvJ2&Qq@Fkh";
            GCHandle gCHandle = GCHandle.Alloc(password, GCHandleType.Pinned);
            Logic.FileEncryption.FileEncrypt(txtFileName.Text, password);
            Logic.FileEncryption.ZeroMemory(gCHandle.AddrOfPinnedObject(), password.Length * 2);
            gCHandle.Free();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string password = "+9j?5DvJ2&Qq@Fkh";
            GCHandle gch = GCHandle.Alloc(password, GCHandleType.Pinned);
            string outputFileName = txtFileName.Text;
            outputFileName = outputFileName.Insert(txtFileName.Text.LastIndexOf('\\')+1, "decrypted_").Replace(".aes", "");
            Logic.FileEncryption.FileDecrypt(txtFileName.Text, outputFileName, password);
            Logic.FileEncryption.ZeroMemory(gch.AddrOfPinnedObject(), password.Length * 2);
            gch.Free();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DigitalSignature digitalSignature = new DigitalSignature();
            digitalSignature.SignXml(txtFileName.Text);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            HideAll();
            DbHelper.LoadUserImage(pictureBox1);
            pnlHome.Visible = true;
        }

        private void ShowCovidStatistics()
        {
            RestClient client = new RestClient(@"https://api.covid19api.com/");
            var request = new RestRequest("summary");
            var response = client.ExecuteAsync(request);

            if (response.Result != null)
            {
                string rawResponse = response.Result.Content;
                dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject(rawResponse);
                lblCovid.Text = "New cases in the last 24h: " + json["Global"]["NewConfirmed"] + "\nNew deaths in the last 24h: " + json["Global"]["NewDeaths"];
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
            Globals.USER_JMBAG = "";
            this.Close();
        }

        private void ShowProfile()
        {
            DateTime dt = (DateTime)user.DoB;
            lblJMBAG.Text = user.JMBAG;
            lblFirstName.Text = user.FirstName;
            lblLastName.Text = user.LastName;
            lblAddress.Text = user.Address;
            lblCity.Text = user.City;
            lblCountry.Text = user.Country;
            lblEmail.Text = user.Email;
            lblDob.Text = dt.ToString("MM/dd/yyyy");
            
        }


        private void btnEditProfile_Click(object sender, EventArgs e)
        {
            EditStudent editForm = new EditStudent();
            editForm.USER_JMBAG = user.JMBAG;
            editForm.form = this;
            editForm.Show();
        }

        public void RefreshProfile()
        {
            if(editReturnValue == 1)
            {
                user = DbHelper.GetUser(user.JMBAG);
                ShowProfile();
                MessageBox.Show("User updated.");
            }
            else
            {
                MessageBox.Show("Error updating user.");
            }
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DownloadTest dForm = new DownloadTest();
            dForm.ShowDialog();
        }
    }
}
