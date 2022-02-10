using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using NTPDynamicLibrary;
using Newtonsoft.Json.Linq;

namespace NTP_Projekt
{
    public partial class AdminMain : Form
    {
        DigitalSignature _digitalSignature = new DigitalSignature();
        public AdminMain()
        {
            InitializeComponent();
            HideAll();
            pnlStud.Visible = true;
        }
        private void HideAll()
        {
            JsonPnl.Visible = false;
            Json2Pnl.Visible = false;
            DBPnl.Visible = false;
            DB2Pnl.Visible = false;
            ProfPnl.Visible = false;
            CoursePnl.Visible = false;
            pnlConversion.Visible = false;
            check_for_extra_columns();
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

            // DB buttons clear if we switch to other scenario 
            button15_Click(sender, e);
            button9_Click(sender, e);
            button20_Click(sender, e);
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
                                add_user.Parameters.AddWithValue("@password", LoginEncryption.HashString((string)row.Cells[8].Value, row.Cells[3].Value.ToString()));
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
                                update_user.Parameters.AddWithValue("@password", LoginEncryption.HashString((string)row.Cells[8].Value, row.Cells[3].Value.ToString()));
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
                    string jsonString = Logic.FileEncryption.DecryptJson(get_json_path());
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
                }
            }
        }

        //----------------------------------------- Tablica studenata / DB buttons ---------------------------------
        private void button5_Click(object sender, EventArgs e)
        {
            textBox9.ReadOnly = false;
            DB2Pnl.Visible = true;
            dataGridView1.DataSource = null;

            HideAll();
            // JSON buttons
            button7_Click_1(sender, e);
            button15_Click(sender, e);
            button20_Click(sender, e);
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
                            add_user.Parameters.AddWithValue("@password", LoginEncryption.HashString((string)textBox16.Text, textBox12.Text));
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
                                update_user.Parameters.AddWithValue("@password", LoginEncryption.HashString(textBox16.Text, textBox12.Text));
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
        // FILTER
        private void button22_Click(object sender, EventArgs e)
        {
            int idx = dataGridView1.CurrentCell.ColumnIndex;
            string filter = textBox28.Text;
            if (idx < 3)
            {
                string clmnName = dataGridView1.Columns[idx].Name;
                string column;
                switch (clmnName)
                {
                    case "Name":
                        column = "FirstName";
                        break;
                    case "Surname":
                        column = "LastName";
                        break;
                    default:
                        column = clmnName;
                        break;
                }
                filter_students(column, filter);

            }
            else if (idx > 4 && idx < 8)
            {
                string clmnName = dataGridView1.Columns[idx].Name;
                filter_students(clmnName, filter);
            }
        }

        // CLEAR FILTER
        private void button23_Click(object sender, EventArgs e)
        {
            fetch_students_from_db();
            textBox28.Text = null;
        }

        // ---------------------------------------- PROFESORI LOGIKA -------------------------------------------------------
        private void button12_Click(object sender, EventArgs e)
        {
            HideAll();

            textBox17.ReadOnly = false;
            pnlStud.Visible = true;
            ProfPnl.Visible = true;
            dataGridView1.DataSource = null;
            button9_Click(sender, e);
            button7_Click_1(sender, e);
            button20_Click(sender, e);
            fetch_professors_from_db();
            fill_course_combobox();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox17.Text = null;
            textBox18.Text = null;
            textBox19.Text = null;
            textBox20.Text = null;
            dateTimePicker5.Text = DateTime.Today.ToString();
            textBox21.Text = null;
            textBox22.Text = null;
            textBox23.Text = null;
            textBox24.Text = null;
            dateTimePicker4.Text = DateTime.Today.ToString();
            if(ProfPnl.Visible)
            {
                courseCategory.SelectedIndex = 1;
            } 
            button16.Text = "Add";
            textBox17.ReadOnly = false;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                using (ntp_projektEntities1 db = new ntp_projektEntities1())
                {
                    if (textBox18.Text != "" && textBox19.Text != "" && textBox20.Text != "" && textBox21.Text != "" && textBox22.Text != "" && textBox23.Text != "")
                    {
                        if (button16.Text == "Add" && textBox17.Text != "" && textBox24.Text != "")
                        {
                            Users user = new Users();                            
                            user.JMBAG = textBox17.Text;
                            user.FirstName = textBox18.Text;
                            user.LastName = textBox19.Text;
                            user.Email = textBox20.Text;
                            user.DoB = DateTime.Parse(dateTimePicker5.Text);
                            user.Address = textBox21.Text;
                            user.City = textBox22.Text;
                            user.Country = textBox23.Text;
                            user.Password = LoginEncryption.HashString(textBox24.Text, textBox20.Text);
                            user.RoleID = 2;
                            Professors professor = new Professors();
                            professor.JMBAG = user.JMBAG;
                            professor.RoleID = 2;
                            professor.CourseID = (int)courseCategory.SelectedValue;
                            professor.HireDate = DateTime.Parse(dateTimePicker6.Text);                  
                            db.Users.Add(user);
                            db.SaveChanges();
                            db.Professors.Add(professor);
                            db.SaveChanges();
                            fetch_professors_from_db();

                        }
                        else if (button16.Text == "Update")
                        {
                            var user = db.Users.SingleOrDefault(u => u.JMBAG == textBox17.Text);
                            var professor = db.Professors.SingleOrDefault(p => p.JMBAG == textBox17.Text);
                            if(user != null)
                            {
                                user.FirstName = textBox18.Text;
                                user.LastName = textBox19.Text;
                                user.Email = textBox20.Text;
                                user.DoB = DateTime.Parse(dateTimePicker5.Text);
                                user.Address = textBox21.Text;
                                user.City = textBox22.Text;
                                user.Country = textBox23.Text;
                                if(textBox24.Text != "")
                                {
                                    user.Password = textBox24.Text;
                                }
                                professor.HireDate = DateTime.Parse(dateTimePicker6.Text);
                                professor.CourseID = (int)courseCategory.SelectedValue;
                                db.SaveChanges();
                                fetch_professors_from_db();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("All fields have to be filled.");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            using (ntp_projektEntities1 db = new ntp_projektEntities1())
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    string id = row.Cells[0].Value.ToString();
                    var prof_to_delete = db.Professors.SingleOrDefault(p => p.JMBAG == id);
                    if (prof_to_delete != null)
                    {
                        db.Professors.Remove(prof_to_delete);
                        db.SaveChanges();
                    }
                }
                fetch_professors_from_db();
            }
        }

        // ---------------------------------------- Tablica Courses / XML Fje ----------------------------------------------
        private void button13_Click(object sender, EventArgs e)
        {
            CoursePnl.Visible = true;
            HideAll();
            pnlStud.Visible = true;
            dataGridView1.DataSource = null;
            textBox25.ReadOnly = false;
            CoursePnl.Visible = true;
            fetch_courses_from_db();
            dataGridView1.AllowUserToAddRows = false;

            button7_Click_1(sender, e);
            button9_Click(sender, e);
            button15_Click(sender, e);
        }
        //EXPORT/SAVE
        private void button17_Click(object sender, EventArgs e)
        {
            export_courses_to_xml();
        }
        // IMPORT
        private void button18_Click(object sender, EventArgs e)
        {
            import_courses_from_xml();
        }
        // CLEAR BUTTON
        private void button20_Click(object sender, EventArgs e)
        {
            textBox25.Text = null;
            textBox26.Text = null;
            textBox27.Text = null;
            button19.Text = "Add";
            textBox25.ReadOnly = false;
        }
        // ADD
        private void button19_Click(object sender, EventArgs e)
        {
            using(ntp_projektEntities1 db = new ntp_projektEntities1())
            {
                if(textBox26.Text != "" && textBox27.Text != "")
                {
                    if(button19.Text == "Add" && textBox25.Text != "")
                    {
                        Courses course = new Courses();                    
                        course.CourseID = Convert.ToInt32(textBox25.Text.ToString().Trim());
                        course.Name = textBox26.Text.ToString().Trim();
                        course.Description = textBox27.Text.Trim();
                        db.Courses.Add(course);
                        db.SaveChanges();
                        fetch_courses_from_db();
                    }
                    else if (button19.Text == "Update")
                    {
                        int id = Convert.ToInt32(textBox25.Text.ToString());
                        var course = db.Courses.SingleOrDefault(c => c.CourseID == id);
                        if(course != null)
                        {
                            course.Name = textBox26.Text.ToString().Trim();
                            course.Description = textBox27.Text.ToString().Trim();
                            db.SaveChanges();
                        }
                        fetch_courses_from_db();                       
                    }
                }
                else
                {
                    MessageBox.Show("All fields have to be filled.");
                }

            }
        }
        //DELETE
        private void button21_Click(object sender, EventArgs e)
        {
            using(ntp_projektEntities1 db = new ntp_projektEntities1())
            {
                foreach(DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int id = (int)row.Cells[0].Value;                    
                    var course_to_delete = db.Courses.SingleOrDefault(c => c.CourseID==id);
                    if(course_to_delete != null)
                    {
                        db.Courses.Remove(course_to_delete);
                        db.SaveChanges();
                    }
                    fetch_courses_from_db();
                }
            }
        }

        // ---------------------------------------- Tablica studenata pomocne funkcije--------------------------------------
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
                textBox17.ReadOnly = true;
                button16.Text = "Update";
                ILookup<int, Courses> looksup = fill_course_combobox();
                textBox17.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox18.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox19.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox20.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
                dateTimePicker5.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
                textBox21.Text = this.dataGridView1.CurrentRow.Cells[5].Value.ToString();
                textBox22.Text = this.dataGridView1.CurrentRow.Cells[6].Value.ToString();
                textBox23.Text = this.dataGridView1.CurrentRow.Cells[7].Value.ToString();
                dateTimePicker4.Text = this.dataGridView1.CurrentRow.Cells[9].Value.ToString();
                foreach (Courses course in looksup[(int)this.dataGridView1.CurrentRow.Cells[10].Value])
                {
                    courseCategory.SelectedValue = course.CourseID;
                }
            }
            else if (CoursePnl.Visible == true)
            {
                textBox25.ReadOnly = true;
                button19.Text = "Update";
                textBox25.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim();
                textBox26.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim();
                textBox27.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString().Trim();
            }
        }

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
            ILookup<int, Courses> looksup = fill_course_combobox();
            ntp_projektEntities1 db = new ntp_projektEntities1();
            var query = from user in db.Users
                        where user.RoleID == 2
                        join professor in db.Professors
                        on user.JMBAG equals professor.JMBAG
                        select new
                        {
                            user.JMBAG,
                            user.FirstName,
                            user.LastName,
                            user.Email,
                            user.DoB,
                            user.Address,
                            user.City,
                            user.Country,
                            user.Password,
                            professor.HireDate,
                            professor.CourseID
                        };
            dataGridView1.DataSource = query.ToList();
            check_for_extra_columns();
            DataGridViewColumn course_name_column = new DataGridViewColumn();
            course_name_column.Name = "courseName";
            course_name_column.HeaderText = "Course Name";
            course_name_column.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView1.Columns.Insert(11, course_name_column);
            dataGridView1.Columns[1].HeaderText = "Name";
            dataGridView1.Columns[2].HeaderText = "Surname";
            dataGridView1.Columns[4].HeaderText = "Date of birth";
            dataGridView1.Columns[9].HeaderText = "Hire date";
            dataGridView1.Columns[10].HeaderText = "Course ID";
            courseCategory.DataBindings.Clear();
            courseCategory.DataBindings.Add("SelectedValue", db.Professors.Local.ToList(), "CourseID");
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int nbr = (int)row.Cells[10].Value;
                foreach (Courses course in looksup[nbr])
                {
                    row.Cells[11].Value = course.Name;
                }
            }
        }

        private void fetch_courses_from_db()
        {
            string connectionString = NTP_Projekt.Properties.Settings.Default.ntp_projektConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    String sql = "SELECT CourseID, Name, Description FROM Courses";
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

        private void import_students_from_json()
        {
            try
            {
                string jsonString = Logic.FileEncryption.DecryptJson(get_json_path());
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
            string output = JsonConvert.SerializeObject(this.dataGridView1.DataSource, Newtonsoft.Json.Formatting.Indented);
            output = Regex.Replace(output, matchpattern, replacementpattern);
            string filepath = get_json_path();
            File.Delete(filepath + ".aes");

            if (File.Exists(filepath))
                File.Delete(filepath);

            using (StreamWriter stream = new StreamWriter(filepath))
                stream.Write(output);

            Logic.FileEncryption.EncryptJson(filepath);
        }

        private string get_json_path()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            string filepath = Path.Combine(projectDirectory, @"\Resources\", "students.json");
            string filename = Path.GetFullPath(projectDirectory + filepath);
            return filename;
        }

        private string get_xml_path()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            string filepath = Path.Combine(projectDirectory, @"\Resources\", "courses.xml");
            string filename2 = Path.GetFullPath(projectDirectory + filepath);
            return filename2;
        }

        private ILookup<int, Courses> fill_course_combobox()
        {
            try
            {
                using(ntp_projektEntities1 db = new ntp_projektEntities1())
                {
                    var courses = db.Courses.ToList();
                    foreach (Courses course in courses)
                    {
                        course.Name = course.Name.Trim();
                    }
                    courseCategory.DataSource = courses;
                    courseCategory.ValueMember = "CourseID";
                    courseCategory.DisplayMember = "Name";
                    ILookup<int, Courses> lookups = courses.ToLookup(id => id.CourseID);
                    return lookups;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        private void export_courses_to_xml()
        {
            DataSet ds = new DataSet();
            var dt = new DataTable();
            DataColumn col1 = new DataColumn("CourseID");
            DataColumn col2 = new DataColumn("Name");
            DataColumn col3 = new DataColumn("Description");
            dt.Columns.Add(col1);
            dt.Columns.Add(col2);
            dt.Columns.Add(col3);

            object[] cells = new object[dataGridView1.Columns.Count];
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                cells[0] = row.Cells[0].Value;
                cells[1] = row.Cells[1].Value.ToString().Trim();
                cells[2] = row.Cells[2].Value.ToString().Trim();
                dt.Rows.Add(cells);
            }
            dt.TableName = "course";
            ds.DataSetName = "courses";
            ds.Tables.Add(dt);
            ds.WriteXml(get_xml_path());
            _digitalSignature.SignXml(get_xml_path());
        }

        private void import_courses_from_xml()
        {
            using(ntp_projektEntities1 db = new ntp_projektEntities1())
            {
                if (_digitalSignature.VerifyXml(get_xml_path()))
                {
                    XElement xml = XElement.Load(get_xml_path());
                    IEnumerable<XElement> courses = from el in xml.Elements() where el.ToString().StartsWith("<course>") select el;
                    foreach (XElement e in courses)
                    {
                        var course_from_xml = e.Elements().ToList();
                        var course_id = Convert.ToInt32(course_from_xml[0].Value);
                        var dbchecker = db.Courses.SingleOrDefault(c => c.CourseID == course_id);
                        if (dbchecker == null)
                        {
                            Courses course = new Courses();
                            course.CourseID = Convert.ToInt32(course_from_xml[0].Value);
                            course.Name = course_from_xml[1].Value;
                            course.Description = course_from_xml[2].Value;
                            db.Courses.Add(course);
                            Console.WriteLine($"Course {course_from_xml[1].Value} successfully imported!");
                        }
                        else
                        {
                            Console.WriteLine($"Course {course_from_xml[1].Value} already exists, skipping....");
                        }
                    }

                    db.SaveChanges();
                    fetch_courses_from_db();
                }
                else
                {
                    Console.WriteLine("digital signature verification failed.");
                }
            }
        }

        private void check_for_extra_columns()
        {
            if (dataGridView1.Columns.Contains("courseName"))
            {
                dataGridView1.Columns.Remove("courseName");
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn = dataGridView1.Columns[e.ColumnIndex];
            DataGridViewColumn oldColumn = dataGridView1.SortedColumn;
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn &&
                    dataGridView1.SortOrder == System.Windows.Forms.SortOrder.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = ListSortDirection.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
            }

            // Sort the selected column.
            dataGridView1.Sort(newColumn, direction);
            newColumn.HeaderCell.SortGlyphDirection =
                direction == ListSortDirection.Ascending ? System.Windows.Forms.SortOrder.Ascending : System.Windows.Forms.SortOrder.Descending;
        }

        private void dataGridView1_DataBindingComplete(object sender,
            DataGridViewBindingCompleteEventArgs e)
        {
            // Put each of the columns into programmatic sort mode.
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }

        private void filter_students(string field, string filter)
        {
            string connectionString = NTP_Projekt.Properties.Settings.Default.ntp_projektConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    String sql = "SELECT Users.JMBAG, FirstName as [Name], LastName as [Surname], Email, Dob as [Date of birth]," +
                        " Address, City, Country, Password, EnrollmentDate as [Enrollment Date] FROM Users" +
                        $" INNER JOIN Students on Users.JMBAG = Students.JMBAG WHERE Users.RoleID = 1 AND Users.{field} LIKE '{filter}%'";
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

        private void AdminMain_Load(object sender, EventArgs e)
        {
            Logic.Appearance.RefreshForm(this);
            btnConvertToXml.Enabled = false;
            btnConvertToJson.Enabled = false;
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "XML Files|*.xml|JSON Files|*.json" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtFile.Text = ofd.FileName;
                    if (ofd.FileName.Contains(".xml")){
                        btnConvertToJson.Enabled = true;
                        btnConvertToXml.Enabled = false;
                    }   
                    else if (ofd.FileName.Contains(".json")){
                        btnConvertToXml.Enabled = true;
                        btnConvertToJson.Enabled = false;
                    }
                        
                }

            }
        }

        private void btnConvertToJson_Click(object sender, EventArgs e)
        {
            if (txtFile.Text.Contains(".xml"))
            {
                NTPSoapService.NTPSoapSoapClient client = new NTPSoapService.NTPSoapSoapClient();
                string xmlString = File.ReadAllText(txtFile.Text);
                var response = client.ConvertXmlToJson(xmlString);

                string workingDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
                string filepath = Path.Combine(projectDirectory, @"\Resources\", "courses_conv.json");
                string filename = Path.GetFullPath(projectDirectory + filepath);

                File.WriteAllText(filename, response);
            }
            else
                MessageBox.Show("Invalid file type.");
            

        }

        private void btnConvertToXml_Click(object sender, EventArgs e)
        {
            if (txtFile.Text.Contains(".json"))
            {
                NTPSoapService.NTPSoapSoapClient client = new NTPSoapService.NTPSoapSoapClient();
                string jsonString = File.ReadAllText(txtFile.Text);
                Console.WriteLine(jsonString);
                var response = client.ConvertJsonToXml(jsonString);

                string workingDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
                string filepath = Path.Combine(projectDirectory, @"\Resources\", "courses_conv.xml");
                string filename = Path.GetFullPath(projectDirectory + filepath);

                File.WriteAllText(filename, response);
            }
            else
                MessageBox.Show("Invalid file type.");
        }

        private void btnConversion_Click(object sender, EventArgs e)
        {
            HideAll();
            pnlConversion.BringToFront();
            pnlConversion.Visible = true;
        }

        private void btnImportJson_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JSON Files|*.json" })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        txtFile.Text = ofd.FileName;
                    }
                    string jsonString = File.ReadAllText(ofd.FileName);
                    dynamic json = JsonConvert.DeserializeObject(jsonString);
                    string completeJson = "[";
                    foreach(var element in json["courses"]["course"])
                        completeJson += element.ToString() + ",";
                    completeJson.Remove(completeJson.Length - 1);
                    completeJson += "]";
                    BindingList<Model.Courses> courses = JsonConvert.DeserializeObject<BindingList<Model.Courses>>(completeJson);
                    dataGridView1.DataSource = courses;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading JSON Courses" + ex.Message);
            }
        }

        private void btnExportJson_Click(object sender, EventArgs e)
        {
            string output = JsonConvert.SerializeObject(this.dataGridView1.DataSource, Newtonsoft.Json.Formatting.Indented);
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            string filepath = Path.Combine(projectDirectory, @"\Resources\", "courses_test.json");
            string filename = Path.GetFullPath(projectDirectory + filepath);

            using (StreamWriter stream = new StreamWriter(filename))
                stream.Write(output);
        }
    }
}
