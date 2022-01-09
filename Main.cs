using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NTP_Projekt
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void cbxGrades_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(e);
            Grades grade = new Grades();
            grade.Show();
        }
    }
}
