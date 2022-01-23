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
    public partial class Settings : Form
    {
        public Login MyParent { get; set; }
        public Settings()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var ini = new IniFile("settings.ini");
            if (checkBox1.Checked)
            {
                ini.Write("FontMultiplier", "1.5");
            }
            else
            {
                ini.Write("FontMultiplier", "1");
            }

            MyParent.Login_Load(sender, e);
        }
    }
}
