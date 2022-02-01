using Newtonsoft.Json;
using NTP_Projekt.View;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace NTP_Projekt
{
    public partial class AdminMain : Form
    {
        public AdminMain()
        {
            InitializeComponent();
            HideAll();
            pnlStud.Visible = true;
            fill_course_combobox();
        }
        private void HideAll()
        {
            JsonPnl.Visible = false;
            Json2Pnl.Visible = false;
            DBPnl.Visible = false;
            DB2Pnl.Visible = false;
            ProfPnl.Visible = false;
        }

        // ---------------------------------------- Tablica studenata / JSON buttons--------------------------------------
        // JSON panel
        private void button1_Click(object sender, EventArgs e)
        {
            HideAll();
            // Show JSON and hide DB items
            pnlStud.Visible = true;
            JsonPnl.Visible = true;
            Json2Pnl.Visible = true;
            dataGridView1.DataSource = null;
            button8.Enabled = false;

            // DB buttons clear if we switch from DB to JSON 
            button9_Click(sender, e);
            button10.Text = "Add";
        }
        // Studenti iz JSON-a, prikazi panele i gumb pa importaj
        private void button2_Click(object sender, EventArgs e)
        {
            pnlStud.Visible = true;
            button8.Enabled = true;
            import_students_from_json();
        }
        // Spremanje studenata u JSON 
        private void button3_Click(object sender, EventArgs e)
        {
            pnlStud.Visible = true;
            save_students_to_json();
        }
        // Brisanje selektiranog studenta iz viewa
        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(row.Index);
            }
        }
        // Update baze podataka
        // za svaki row provjeri jel student postoji u bazi
        // ako ne, dodaj u bazu
        private void button6_Click(object sender, EventArgs e)
        {
            string connectionString = NTP_Projekt.Properties.Settings.Default.ntp_projektConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                try
                {
                    foreach(DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[0].Value != null)
                        {
                            SqlCommand check_for_user = new SqlCommand("SELECT COUNT(*) FROM Users WHERE JMBAG = @jmbag", conn);
                            check_for_user.Parameters.AddWithValue("@jmbag", row.Cells[0].Value);
                            int UserExist = (int)check_for_user.ExecuteScalar();
                            if (UserExist == 0)
                            {
                                SqlCommand add_user = new SqlCommand();
                                add_user.CommandText = "INSERT INTO users (FirstName, LastName, Email, Password, Address, City, Country, DoB, JMBAG, RoleID) " +
                                    "values (@firstName, @lastName,@email, @password, @address, @city, @country, @dob, @jmbag, 1);" +
                                    "INSERT INTO Students (JMBAG, EnrollmentDate) values (@jmbag, @enrollmentdate)";
                                add_user.Parameters.AddWithValue("@email", row.Cells[3].Value);
                                add_user.Parameters.AddWithValue("@firstName", row.Cells[1].Value);
                                add_user.Parameters.AddWithValue("@lastName", row.Cells[2].Value);
                                add_user.Parameters.AddWithValue("@address", row.Cells[5].Value);
                                add_user.Parameters.AddWithValue("@city", row.Cells[6].Value);
                                add_user.Parameters.AddWithValue("@country", row.Cells[7].Value);
                                add_user.Parameters.AddWithValue("@dob", row.Cells[4].Value);
                                add_user.Parameters.AddWithValue("@jmbag", row.Cells[0].Value);
                                add_user.Parameters.AddWithValue("@enrollmentdate", row.Cells[9].Value);
                                add_user.Parameters.AddWithValue("@password", LoginEncryption.HashString((string)row.Cells[8].Value));
                                add_user.Connection = conn;
                                add_user.ExecuteNonQuery();
                            }
                            else
                            {
                                Console.WriteLine("JMBAG {0} already exists in database. Skipping...", row.Cells[0].Value.ToString());
                                SqlCommand update_user = new SqlCommand();
                                update_user.CommandText = "UPDATE users SET JMBAG = @jmbag, FirstName = @firstName, LastName = @lastName, Email = @email, " +
                                    "Address = @address, City = @city, Country = @country, DoB = @dob, Password = @password WHERE JMBAG = @jmbag;" +
                                    "UPDATE Students SET EnrollmentDate = @enrollmentdate WHERE JMBAG = @jmbag;";
                                update_user.Parameters.AddWithValue("@email", row.Cells[3].Value);
                                update_user.Parameters.AddWithValue("@firstName", row.Cells[1].Value);
                                update_user.Parameters.AddWithValue("@lastName", row.Cells[2].Value);
                                update_user.Parameters.AddWithValue("@address", row.Cells[5].Value);
                                update_user.Parameters.AddWithValue("@city", row.Cells[6].Value);
                                update_user.Parameters.AddWithValue("@country", row.Cells[7].Value);
                                update_user.Parameters.AddWithValue("@dob", row.Cells[4].Value);
                                update_user.Parameters.AddWithValue("@jmbag", row.Cells[0].Value);
                                update_user.Parameters.AddWithValue("@enrollmentdate", row.Cells[9].Value);
                                update_user.Parameters.AddWithValue("@password", LoginEncryption.HashString((string)row.Cells[8].Value));
                                update_user.Connection = conn;
                                update_user.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        // Edit user data transfer to form
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Json2Pnl.Visible == true)
            {
                button8.Text = "Update";
                textBox1.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox2.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox3.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox4.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
                dateTimePicker1.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
                textBox5.Text = this.dataGridView1.CurrentRow.Cells[5].Value.ToString();
                textBox6.Text = this.dataGridView1.CurrentRow.Cells[6].Value.ToString();
                textBox7.Text = this.dataGridView1.CurrentRow.Cells[7].Value.ToString();
                textBox8.Text = this.dataGridView1.CurrentRow.Cells[8].Value.ToString();
                dateTimePicker2.Text = this.dataGridView1.CurrentRow.Cells[9].Value.ToString();
            }
            else if (DB2Pnl.Visible == true)
            {
                textBox9.ReadOnly = true;
                button10.Text = "Update";
                string connectionString = NTP_Projekt.Properties.Settings.Default.ntp_projektConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    try
                    {
                        textBox9.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                        textBox10.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                        textBox11.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                        textBox12.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
                        dateTimePicker3.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
                        textBox13.Text = this.dataGridView1.CurrentRow.Cells[5].Value.ToString();
                        textBox14.Text = this.dataGridView1.CurrentRow.Cells[6].Value.ToString();
                        textBox15.Text = this.dataGridView1.CurrentRow.Cells[7].Value.ToString();
                        dateTimePicker4.Text = this.dataGridView1.CurrentRow.Cells[9].Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            else if (ProfPnl.Visible == true)
            {

            }
        }
        // Cancel button
        private void button7_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            dateTimePicker1.Text = DateTime.Today.ToString();
            textBox5.Text = null;
            textBox6.Text = null;
            textBox7.Text = null;
            textBox8.Text = null;
            dateTimePicker2.Text = DateTime.Today.ToString();
            button8.Text = "Add";
        }
        // Update button
        private void button8_Click(object sender, EventArgs e)
        {
            if(button8.Text == "Add")
            {
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "")
                {
                    string jsonString = File.ReadAllText(get_json_path());
                    BindingList<Model.Student> students = JsonConvert.DeserializeObject<BindingList<Model.Student>>(jsonString);
                    dataGridView1.Columns[4].HeaderText = "Date of birth";
                    dataGridView1.Columns[9].HeaderText = "Enrollment Date";
                    Model.Student student = new Model.Student();
                    student.JMBAG = int.Parse(textBox1.Text);
                    student.Name = textBox2.Text;
                    student.Surname = textBox3.Text;
                    student.Email = textBox4.Text;
                    student.Dob = Convert.ToDateTime(dateTimePicker1.Text);
                    student.Address = textBox5.Text;
                    student.City = textBox6.Text;
                    student.Country = textBox7.Text;
                    student.Password = textBox8.Text;
                    student.Enrollment_Date = Convert.ToDateTime(dateTimePicker2.Text);
                    students.Add(student);
                    dataGridView1.DataSource = students;
                    button7_Click_1(sender, e);
                }
                else
                {
                    MessageBox.Show("All fields must be added.");
                }
                
            }
            else if(button8.Text == "Update")
            {
                try
                {
                    if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "")
                    {
                        dataGridView1.CurrentRow.Cells[0].Value = textBox1.Text.ToString();
                        dataGridView1.CurrentRow.Cells[1].Value = textBox2.Text.ToString();
                        dataGridView1.CurrentRow.Cells[2].Value = textBox3.Text.ToString();
                        dataGridView1.CurrentRow.Cells[3].Value = textBox4.Text.ToString();
                        dataGridView1.CurrentRow.Cells[4].Value = dateTimePicker1.Text;
                        dataGridView1.CurrentRow.Cells[5].Value = textBox5.Text.ToString();
                        dataGridView1.CurrentRow.Cells[6].Value = textBox6.Text.ToString();
                        dataGridView1.CurrentRow.Cells[7].Value = textBox7.Text.ToString();
                        dataGridView1.CurrentRow.Cells[8].Value = textBox8.Text.ToString();
                        dataGridView1.CurrentRow.Cells[9].Value = dateTimePicker2.Text;
                        dataGridView1.NotifyCurrentCellDirty(true);
                    }
                    else
                    {
                        MessageBox.Show("All fields must be added.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    button7_Click_1(sender, e);
                    button8.Text = "Add";
                }
            }
        }

        //----------------------------------------- Tablica studenata / DB buttons ---------------------------------
        private void button5_Click(object sender, EventArgs e)
        {
            textBox9.ReadOnly = false;
            DB2Pnl.Visible = true;

            HideAll();
            // JSON buttons
            button7_Click_1(sender, e);
            button8.Text = "Add";

            pnlStud.Visible = true;
            DBPnl.Visible = true;
            DB2Pnl.Visible = true;
            fetch_students_from_db();
        }
        // CLEAR BUTTON ZA DB
        private void button9_Click(object sender, EventArgs e)
        {
            textBox9.Text = null;
            textBox10.Text = null;
            textBox11.Text = null;
            textBox12.Text = null;
            dateTimePicker3.Text = DateTime.Today.ToString();
            textBox13.Text = null;
            textBox14.Text = null;
            textBox15.Text = null;
            textBox16.Text = null;
            dateTimePicker4.Text = DateTime.Today.ToString();
            button10.Text = "Add";
            textBox9.ReadOnly = false;
        }
        // ADD/UPDATE button
        private void button10_Click(object sender, EventArgs e)
        {
            string connectionString = NTP_Projekt.Properties.Settings.Default.ntp_projektConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                try
                {
                    if (button10.Text == "Add")
                    {
                        if (textBox16.Text != "" && textBox15.Text != "" && textBox14.Text != "" && textBox13.Text != "" && textBox12.Text != "" && textBox11.Text != "" && textBox10.Text != "" && textBox9.Text != "")
                        {
                            SqlCommand add_user = new SqlCommand();
                            add_user.CommandText = "INSERT INTO users (FirstName, LastName, Email, Password, Address, City, Country, DoB, JMBAG, RoleID) " +
                                "values (@firstName, @lastName,@email, @password, @address, @city, @country, @dob, @jmbag, 1);" +
                                "INSERT INTO Students (JMBAG, EnrollmentDate) values (@jmbag, @enrollmentdate)";
                            add_user.Parameters.AddWithValue("@jmbag", textBox9.Text);
                            add_user.Parameters.AddWithValue("@firstName", textBox10.Text);
                            add_user.Parameters.AddWithValue("@lastName", textBox11.Text);
                            add_user.Parameters.AddWithValue("@email", textBox12.Text);
                            add_user.Parameters.AddWithValue("@dob", DateTime.Parse(dateTimePicker3.Text.ToString()));
                            add_user.Parameters.AddWithValue("@address", textBox13.Text);
                            add_user.Parameters.AddWithValue("@city", textBox14.Text);
                            add_user.Parameters.AddWithValue("@country", textBox15.Text);
                            add_user.Parameters.AddWithValue("@enrollmentdate", DateTime.Parse(dateTimePicker4.Text.ToString()));
                            add_user.Parameters.AddWithValue("@password", LoginEncryption.HashString((string)textBox16.Text));
                            add_user.Connection = conn;
                            add_user.ExecuteNonQuery();
                            fetch_students_from_db();
                            button9_Click(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("All fields must be added.");
                        }
                    }
                    else if (button10.Text == "Update")
                    {
                        if (textBox9.Text != "" && textBox15.Text != "" && textBox14.Text != "" && textBox13.Text != "" && textBox12.Text != "" && textBox11.Text != "" && textBox10.Text != "")
                        {
                            SqlCommand update_user = new SqlCommand();
                            if(textBox16.Text == "")
                            {
                                update_user.CommandText = "UPDATE users SET FirstName = @firstName, LastName = @lastName, Email = @email, " +
                                           "Address = @address, City = @city, Country = @country, DoB = @dob WHERE JMBAG = @jmbag;" +
                                           "UPDATE Students SET EnrollmentDate = @enrollmentdate WHERE JMBAG = @jmbag;";
                            }
                            else
                            {
                                update_user.CommandText = "UPDATE users SET FirstName = @firstName, LastName = @lastName, Email = @email, " +
                                           "Address = @address, City = @city, Country = @country, DoB = @dob, Password = @password WHERE JMBAG = @jmbag;" +
                                           "UPDATE Students SET EnrollmentDate = @enrollmentdate WHERE JMBAG = @jmbag;";
                                update_user.Parameters.AddWithValue("@password", LoginEncryption.HashString(textBox16.Text));
                            }
                            update_user.Parameters.AddWithValue("@jmbag", textBox9.Text);
                            update_user.Parameters.AddWithValue("@firstName", textBox10.Text);
                            update_user.Parameters.AddWithValue("@lastName", textBox11.Text);
                            update_user.Parameters.AddWithValue("@email", textBox12.Text);
                            update_user.Parameters.AddWithValue("@dob", DateTime.Parse(dateTimePicker3.Text.ToString()));
                            update_user.Parameters.AddWithValue("@address", textBox13.Text);
                            update_user.Parameters.AddWithValue("@city", textBox14.Text);
                            update_user.Parameters.AddWithValue("@country", textBox15.Text);
                            update_user.Parameters.AddWithValue("@enrollmentdate", DateTime.Parse(dateTimePicker4.Text.ToString()));
                            update_user.Connection = conn;
                            update_user.ExecuteNonQuery();
                            fetch_students_from_db();
                            button9_Click(sender, e);
                            button10.Text = "Add";
                        }
                        else
                        {
                            MessageBox.Show("All fields must be added.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }    
        }

        // DELETE BUTTON
        private void button11_Click(object sender, EventArgs e)
        {
            string connectionString = NTP_Projekt.Properties.Settings.Default.ntp_projektConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    conn.Open();
                    try
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "DELETE FROM Students WHERE JMBAG = @jmbag;" + "DELETE FROM Users WHERE JMBAG = @jmbag;";
                        cmd.Parameters.AddWithValue("@jmbag", row.Cells[0].Value);
                        cmd.Connection = conn;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Account deleted");
                        fetch_students_from_db();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally { conn.Close(); }
                }
            }
        }

        // ---------------------------------------- PROFESORI LOGIKA -------------------------------------------------------
        private void button12_Click(object sender, EventArgs e)
        {
            HideAll();

            pnlStud.Visible = true;
            ProfPnl.Visible = true;
            dataGridView1.DataSource = null;
            button9_Click(sender, e);
            button10.Text = "Add";
            button7_Click_1(sender, e);
            button8.Text = "Add";
            fetch_professors_from_db();
            fill_course_combobox();
        }

        // ---------------------------------------- Tablica studenata pomocne funkcije--------------------------------------
        private void fetch_students_from_db()
        {
            string connectionString = NTP_Projekt.Properties.Settings.Default.ntp_projektConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    String sql = "SELECT Users.JMBAG, FirstName as [Name], LastName as [Surname], Email, Dob as [Date of birth]," +
                        " Address, City, Country, Password, EnrollmentDate as [Enrollment Date] FROM Users" +
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
                    MessageBox.Show(ex.Message.ToString(), "ERROR Loading database.");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void fetch_professors_from_db()
        {
            //string connectionString = NTP_Projekt.Properties.Settings.Default.ntp_projektConnectionString;
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    try
            //    {
            //        String sql = "SELECT Users.JMBAG, FirstName as [Name], LastName as [Surname], Email, Dob as [Date of birth]," +
            //            " Address, City, Country, Password, HireDate as [Hire Date], Name as [Course Name] FROM Users" +
            //            " INNER JOIN Professors on Users.JMBAG = Professors.JMBAG " +
            //            " INNer JOIN Courses on Professors.CourseID = Courses.CourseID WHERE Users.RoleID = 2";
            //        conn.Open();
            //        SqlCommand cmd;
            //        SqlDataAdapter adapter;
            //        cmd = new SqlCommand(sql, conn);
            //        adapter = new SqlDataAdapter(sql, connectionString);
            //        DataTable dt = new DataTable();
            //        adapter.Fill(dt);
            //        dataGridView1.DataSource = dt;
            //        cmd.Dispose();
            //    }
            //    catch (SqlException ex)
            //    {
            //        MessageBox.Show(ex.Message.ToString(), "ERROR Loading database.");
            //    }
            //    finally
            //    {
            //        conn.Close();
            //    }
            //}
        }

        private void import_students_from_json()
        {
            try
            {
                string jsonString = File.ReadAllText(get_json_path());
                BindingList<Model.Student> students = JsonConvert.DeserializeObject<BindingList<Model.Student>>(jsonString);
                dataGridView1.DataSource = students;
                dataGridView1.Columns[4].HeaderText = "Date of birth";
                dataGridView1.Columns[9].HeaderText = "Enrollment Date";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Cannot open file.");
            }
        }

        private void save_students_to_json()
        {
            String matchpattern = @"[ ]{6,}";
            String replacementpattern = @"";
            string output = JsonConvert.SerializeObject(this.dataGridView1.DataSource, Formatting.Indented);
            output = Regex.Replace(output, matchpattern, replacementpattern);
            string filepath = get_json_path();
            if (!File.Exists(filepath))
            {
                File.Create(filepath);
                File.WriteAllText(filepath, output);
            }
            else if (File.Exists(filepath))
            {
                File.WriteAllText(filepath, output);
            }
        }

        private string get_json_path()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            string filepath = Path.Combine(projectDirectory, @"\Resources\", "students.json");
            string filename = Path.GetFullPath(projectDirectory + filepath);
            return filename;
        }

        private void fill_course_combobox()
        {
            try
            {
                using(ntp_projektEntities db = new ntp_projektEntities())
                {
                    courseCategory.DataSource = db.Courses.ToList();
                    courseCategory.ValueMember = "CourseID";
                    courseCategory.DisplayMember = "CourseName";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
