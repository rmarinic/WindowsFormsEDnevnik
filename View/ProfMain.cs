using NTP_Projekt.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NTP_Projekt.View
{
    public partial class ProfMain : Form
    {
        public ProfMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void fetch_enrolled_students()
        {
            using(ntp_projektEntities1 db = new ntp_projektEntities1())
            {
                var jmbag = Globals.USER_JMBAG;
                var professor = db.Professors.SingleOrDefault(p => p.JMBAG == jmbag);
                var query = from user in db.Users
                            join students in db.Students
                            on user.JMBAG equals students.JMBAG
                            join enrollments in db.Enrollments
                            on students.JMBAG equals enrollments.StudentJMBAG
                            join courses in db.Courses
                            on enrollments.CourseID equals courses.CourseID
                            join grades in db.Grades
                            on courses.CourseID equals grades.CourseID
                            where user.RoleID == 1 && enrollments.CourseID == professor.CourseID
                            select new
                            {
                                user.JMBAG,
                                user.FirstName,
                                user.LastName,
                                user.Email
                            };
                dataGridView1.DataSource = query.ToList();
                add_columns_to_view();
                foreach(DataGridViewRow row in dataGridView1.Rows)
                {
                    string stud_jmbag = row.Cells[0].Value.ToString();
                    var first_exam_grade = do_tasks(stud_jmbag, "first_exam");
                    Console.WriteLine(first_exam_grade);
                    //do_tasks(stud_jmbag, "second_exam");
                }
            }
        }

        public Grades do_tasks(string jmbag, string type)
        {
            Task<Grades> task = Task<Grades>.Factory.StartNew(() =>
            {
                using(ntp_projektEntities1 db = new ntp_projektEntities1())
                {
                    var grade = from grades in db.Grades
                                where grades.StudentJMBAG == jmbag && grades.Type == type
                                orderby grades.Score
                                select grades;
                    return (Grades)grade;
                }
            });
            return task.Result;
        }

        private void add_columns_to_view()
        {
            DataGridViewColumn first_exam = new DataGridViewColumn();
            DataGridViewColumn second_exam = new DataGridViewColumn();
            DataGridViewColumn third_exam = new DataGridViewColumn();
            DataGridViewColumn practical_exercise = new DataGridViewColumn();
            first_exam.Name = "firstExam";
            first_exam.HeaderText = "First Exam";
            first_exam.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView1.Columns.Insert(4, first_exam);
            second_exam.Name = "secondExam";
            second_exam.HeaderText = "Second Exam";
            second_exam.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView1.Columns.Insert(5, second_exam);
            third_exam.Name = "thirdExam";
            third_exam.HeaderText = "Third Exam";
            third_exam.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView1.Columns.Insert(6, third_exam);
            practical_exercise.Name = "pracExercise";
            practical_exercise.HeaderText = "Practical Exercise";
            practical_exercise.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView1.Columns.Insert(7, practical_exercise);
        }
    }
}
