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
            switch (cbxGrades.SelectedIndex)
            {
                case 0:
                    HideAll();
                    pnlGrades.Visible = true;
                    break;
                case 1:
                    HideAll();
                    pnlSubjects.Visible = true;
                    break;
                case 2:
                    HideAll();
                    pnlAllGrades.Visible = true;
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
    }
}
