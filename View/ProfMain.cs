using NTP_Projekt.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NTP_Projekt.View
{
    public partial class ProfMain : Form
    {
        public ProfMain()
        {
            InitializeComponent();
            GradeAddPnl.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            GradeAddPnl.Visible = true;
            HidePnl.Visible = false;
            fetch_enrolled_students();
            fill_combobox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HidePnl.Visible = false;
            GradeAddPnl.Visible = true;
            fill_combobox();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HidePnl.Visible = true;
            GradeAddPnl.Visible = false;
            fetch_unenrolled_students();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (ntp_projektEntities1 db = new ntp_projektEntities1())
            {
                var jmbag = Globals.USER_JMBAG;
                var professor = db.Professors.SingleOrDefault(p => p.JMBAG == jmbag);
                string stud_jmbag = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox1.Text = stud_jmbag;
                var exam_type = resolve_exam_type(comboBox1.SelectedItem.ToString());
                var grade = db.Grades.SingleOrDefault(g => g.CourseID == professor.CourseID && g.Type == exam_type && g.StudentJMBAG == stud_jmbag);
                if (grade != null)
                {
                    grade.Score = Convert.ToInt32(textBox2.Text);
                    db.SaveChanges();
                    fetch_enrolled_students();
                }
                else
                {
                    Grades new_grade = new Grades();
                    new_grade.ID = Convert.ToInt32(textBox3.Text);
                    new_grade.StudentJMBAG = stud_jmbag;
                    new_grade.Score = Convert.ToInt32(textBox2.Text);
                    new_grade.CourseID = professor.CourseID;
                    new_grade.Type = exam_type;
                    db.Grades.Add(new_grade);
                    db.SaveChanges();
                    fetch_enrolled_students();
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GradeAddPnl.Visible == true)
            {
                textBox1.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                int column_index = this.dataGridView1.CurrentCell.ColumnIndex;
                comboBox1.SelectedItem = dataGridView1.Columns[column_index].HeaderText;
                textBox2.Text = this.dataGridView1.CurrentCell.Value.ToString();
                textBox1.ReadOnly = true;
                button3.Text = "Update";
            }
            else
            {
                ntp_projektEntities1 db = new ntp_projektEntities1();
                var jmbag = Globals.USER_JMBAG;
                var professor = db.Professors.SingleOrDefault(p => p.JMBAG == jmbag);
                Random rand = new Random();
                int id = rand.Next(5, 1000);
                Enrollments enrollment = new Enrollments();
                enrollment.EnrollmentID = id;
                enrollment.CourseID = (int)professor.CourseID;
                enrollment.StudentJMBAG = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                fetch_unenrolled_students();
            }
        }

        private void fetch_enrolled_students()
        {
            using(ntp_projektEntities1 db = new ntp_projektEntities1())
            {
                dataGridView1.Columns.Clear();
                var jmbag = Globals.USER_JMBAG;
                var professor = db.Professors.SingleOrDefault(p => p.JMBAG == jmbag);
                var query = from user in db.Users
                            join students in db.Students
                            on user.JMBAG equals students.JMBAG
                            join enrollments in db.Enrollments
                            on students.JMBAG equals enrollments.StudentJMBAG
                            join courses in db.Courses
                            on enrollments.CourseID equals courses.CourseID
                            where user.RoleID == 1 && enrollments.CourseID == professor.CourseID
                            select new
                            {
                                user.JMBAG,
                                user.FirstName,
                                user.LastName,
                                user.Email
                            };
                dataGridView1.DataSource = query.ToList();
                if (dataGridView1.ColumnCount == 4)
                    add_columns_to_view();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    string stud_jmbag = row.Cells[0].Value.ToString();
                    int first_exam_grade = do_tasks(stud_jmbag, "first_exam");
                    int second_exam_grade = do_tasks(stud_jmbag, "second_exam");
                    int third_exam_grade = do_tasks(stud_jmbag, "third_exam");
                    int practical_exam_grade = do_tasks(stud_jmbag, "practical_exercise");
                    row.Cells[4].Value = first_exam_grade;
                    row.Cells[5].Value = second_exam_grade;
                    row.Cells[6].Value = third_exam_grade;
                    row.Cells[7].Value = practical_exam_grade;
                }

            }
        }

        private void fetch_unenrolled_students()
        {
            using (ntp_projektEntities1 db = new ntp_projektEntities1())
            {
                dataGridView1.Columns.Clear();
                var jmbag = Globals.USER_JMBAG;
                var professor = db.Professors.SingleOrDefault(p => p.JMBAG == jmbag);
                var query = from user in db.Users
                            join students in db.Students
                            on user.JMBAG equals students.JMBAG
                            join enrollments in db.Enrollments
                            on students.JMBAG equals enrollments.StudentJMBAG into enroll
                            from enrollment in enroll.DefaultIfEmpty()
                            where user.RoleID == 1 && enrollment.CourseID != professor.CourseID
                            select new
                            {
                                user.JMBAG,
                                user.FirstName,
                                user.LastName,
                                user.Email
                            };
                dataGridView1.DataSource = query.ToList();
            }

        }

        public int do_tasks(string jmbag, string type)
        {
            using (ntp_projektEntities1 db = new ntp_projektEntities1())
            {
                Task<int> task = Task<int>.Factory.StartNew(() =>
                {
                    var grade = from grades in db.Grades
                                where grades.StudentJMBAG == jmbag && grades.Type == type
                                orderby grades.Score
                                select new
                                {
                                    grades.Score
                                };

                    bool has_score = grade.Any(c => c.Score != 0);
                    if (has_score)
                    {
                        var best_grade = grade.ToList().First();
                        Console.WriteLine("Thread #{0} with the score {1} and type {2}", Thread.CurrentThread.ManagedThreadId, best_grade.Score, type);
                        return best_grade.Score;
                    }
                    else
                    {
                        return 0;
                    }
                });
                return task.Result;
            }
        }

        private void add_columns_to_view()
        {
            DataGridViewColumn first_exam = new DataGridViewColumn();
            DataGridViewColumn second_exam = new DataGridViewColumn();
            DataGridViewColumn third_exam = new DataGridViewColumn();
            DataGridViewColumn practical_exercise = new DataGridViewColumn();
            first_exam.Name = "firstExam";
            first_exam.HeaderText = "First exam";
            first_exam.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView1.Columns.Insert(4, first_exam);
            second_exam.Name = "secondExam";
            second_exam.HeaderText = "Second exam";
            second_exam.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView1.Columns.Insert(5, second_exam);
            third_exam.Name = "thirdExam";
            third_exam.HeaderText = "Third exam";
            third_exam.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView1.Columns.Insert(6, third_exam);
            practical_exercise.Name = "pracExercise";
            practical_exercise.HeaderText = "Practical exercises";
            practical_exercise.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView1.Columns.Insert(7, practical_exercise);
        }

        private void fill_combobox()
        {
            comboBox1.Items.Add("First exam");
            comboBox1.Items.Add("Second exam");
            comboBox1.Items.Add("Third exam");
            comboBox1.Items.Add("Practical exercises");
            comboBox1.SelectedIndex = 0;
        }

        private string resolve_exam_type(string type)
        {
            string exam_type;
            switch (type)
            {
                case "First exam":
                    exam_type = "first_exam";
                    break;
                case "Second exam":
                    exam_type = "second_exam";
                    break;
                case "Third exam":
                    exam_type = "third_exam";
                    break;
                default:
                    exam_type = "practical_exercise";
                    break;
            }
            return exam_type;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.ReadOnly = false;
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            comboBox1.SelectedIndex = 0;
        }
    }
}
