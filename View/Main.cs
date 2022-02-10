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
using System.Linq;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Generic;

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
                    ShowAllGrades();
                    pnlAllGrades.Visible = true;
                    break;
                case 2:
                    HideAll();
                    pnlProfile.Visible = true;
                    ShowProfile();
                    break;
            }
        }

        private void HideAll()
        {
            pnlGrades.Visible = false;
            pnlAllGrades.Visible = false;
            pnlHome.Visible = false;
            pnlProfile.Visible = false;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ChangeLanguage();
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
                this.Width = int.Parse(Globals.WINDOW_WIDTH);
                this.Height = int.Parse(Globals.WINDOW_HEIGHT);
            }
            Logic.Appearance.RefreshForm(this);
            ShowCovidStatistics();
            DbHelper.LoadUserImage(pictureBox1);
            RESTGetDateTime();
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

        private void ShowAllGrades()
        {
            using (ntp_projektEntities1 db = new ntp_projektEntities1())
            {
                create_allgrades_data();
                user = Logic.DbHelper.GetUser(Globals.USER_JMBAG);
                var query = from u in db.Users
                            join s in db.Students
                            on u.JMBAG equals s.JMBAG
                            join e in db.Enrollments
                            on s.JMBAG equals e.StudentJMBAG
                            join c in db.Courses
                            on e.CourseID equals c.CourseID
                            where e.StudentJMBAG == user.JMBAG
                            select new
                            {
                                c.CourseID,
                                c.Name
                            };

                var courses = query.ToList();
                foreach (var c in courses)
                {
                    int rowID = dataGridView2.Rows.Add();
                    string course_id = extract_course_id(c.ToString());
                    string course_name = extract_course_name(c.ToString());
                    int int_course_id = Convert.ToInt32(course_id);
                    int first_exam = extract_exam_score(int_course_id, "first_exam");
                    int second_exam = extract_exam_score(int_course_id, "second_exam");
                    int third_exam = extract_exam_score(int_course_id, "third_exam");
                    int practical_exercise = extract_exam_score(int_course_id, "practical_exercise");
                    int calculated = first_exam + second_exam + third_exam + practical_exercise;
                    int avg = (calculated != 0 ? calculated / 4 : 0);
                    DataGridViewRow row = dataGridView2.Rows[rowID];
                    row.Cells[0].Value = course_name;
                    row.Cells[1].Value = first_exam;
                    row.Cells[2].Value = second_exam;
                    row.Cells[3].Value = third_exam;
                    row.Cells[4].Value = practical_exercise;
                    row.Cells[5].Value = avg;
                }
            }
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

        private void button6_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                dt.Columns.Add(column.HeaderText, System.Type.GetType("System.String"));
            }
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                }
            }

            user = Logic.DbHelper.GetUser(Globals.USER_JMBAG);
            if (radioButton1.Checked)
            {
                Spire.DataExport.PDF.PDFExport PDFExport = new Spire.DataExport.PDF.PDFExport();
                PDFExport.DataSource = Spire.DataExport.Common.ExportSource.DataTable;
                PDFExport.DataTable = dt;
                PDFExport.PDFOptions.PageOptions.Orientation = Spire.DataExport.Common.PageOrientation.Landscape;
                PDFExport.ActionAfterExport = Spire.DataExport.Common.ActionType.OpenView;
                PDFExport.FileName = $"{user.JMBAG}.pdf";
                PDFExport.SaveToFile();
            }
            else if (radioButton2.Checked)
            {
                Spire.DataExport.RTF.RTFExport RTFExport = new Spire.DataExport.RTF.RTFExport();
                RTFExport.DataSource = Spire.DataExport.Common.ExportSource.DataTable;
                RTFExport.DataTable = dt;
                RTFExport.RTFOptions.PageOrientation = Spire.DataExport.Common.PageOrientation.Landscape;
                RTFExport.ActionAfterExport = Spire.DataExport.Common.ActionType.OpenView;
                RTFExport.FileName = $"{user.JMBAG}.rtf";
                RTFExport.SaveToFile();
            }
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
            lblAge2.Text = user.Age.ToString();
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
            if (editReturnValue == 1)
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

        private string RESTGetDateTime()
        {
            RestClient client = new RestClient("https://localhost:44387/Api");
            var request = new RestRequest("GetLocalDateTime");
            var response = client.ExecuteAsync(request);

            if (response.Result != null)
            {
                string rawResponse = response.Result.Content;
                lblTime.Text = "Sign in time:" + rawResponse.Replace('"', ' ');
                return rawResponse;
            }
            return null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DownloadTest dForm = new DownloadTest();
            dForm.ShowDialog();
        }

        private string extract_course_id(string course)
        {
            string[] strings;
            strings = course.Split(' ');
            string id = strings[3].Remove(1, 1);
            return id;
        }

        private string extract_course_name(string course)
        {
            string[] strings;
            strings = course.Split(' ');
            string name = "";
            for(int i = 6; i < strings.Length; i++)
            {
                if (strings[i] == "}")
                    break;
                name += " ";
                name += strings[i];
            }
            return name;
        }

        private int extract_exam_score(int int_course_id, string type)
        {
            using(ntp_projektEntities1 db = new ntp_projektEntities1())
            {
                var exam = from g in db.Grades
                           where g.CourseID == int_course_id && g.StudentJMBAG == user.JMBAG && g.Type == type
                           orderby g.Score
                           select new
                           {
                               g.Score
                           };
                bool has_score = exam.Any(c => c.Score != 0);
                if (has_score)
                {
                    var best_grade = exam.ToList().First();
                    return best_grade.Score;
                }
                else
                {
                    return 0;
                }
            }
        }

        private void create_allgrades_data()
        {
            DataGridViewColumn course_name = new DataGridViewColumn();
            DataGridViewColumn first_exam = new DataGridViewColumn();
            DataGridViewColumn second_exam = new DataGridViewColumn();
            DataGridViewColumn third_exam = new DataGridViewColumn();
            DataGridViewColumn practical_exercise = new DataGridViewColumn();
            DataGridViewColumn average = new DataGridViewColumn();
            course_name.Name = "courseName";
            course_name.HeaderText = "Course name";
            course_name.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView2.Columns.Insert(0, course_name);
            first_exam.Name = "firstExam";
            first_exam.HeaderText = "First exam";
            first_exam.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView2.Columns.Insert(1, first_exam);
            second_exam.Name = "secondExam";
            second_exam.HeaderText = "Second exam";
            second_exam.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView2.Columns.Insert(2, second_exam);
            third_exam.Name = "thirdExam";
            third_exam.HeaderText = "Third exam";
            third_exam.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView2.Columns.Insert(3, third_exam);
            practical_exercise.Name = "pracExercise";
            practical_exercise.HeaderText = "Practical exercises";
            practical_exercise.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView2.Columns.Insert(4, practical_exercise);
            average.Name = "avgGrade";
            average.HeaderText = "Average Grade";
            average.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView2.Columns.Insert(5, average);
        }

        public void ChangeLanguage()
        {
            foreach (Control c in GetAll(this))
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(Main));
                resources.ApplyResources(c, c.Name, new CultureInfo(Globals.LANGUAGE));
            }
        }

        public static IEnumerable<Control> GetAll(Control control)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl))
                                      .Concat(controls);

        }
    }
}
