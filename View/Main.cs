using Microsoft.Win32;
using NTP_Projekt.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NTPDynamicLibrary;

namespace NTP_Projekt
{
    public partial class Main : Form
    {
        IniFile ini = new IniFile("settings.ini");
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

        private void Main_Load(object sender, EventArgs e)
        {
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
    }
}
