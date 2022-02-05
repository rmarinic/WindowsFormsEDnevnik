﻿using System;
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
        string fontMultiplier = "";
        bool contrast = false;
        bool underlineLinks = false;
        IniFile ini = new IniFile("settings.ini");

        public Login MyParent { get; set; }
        public Settings()
        {
            InitializeComponent();
        }

        private void chkFontSize_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFontSize.Checked)
                fontMultiplier = "1.5";
            else
                fontMultiplier = "1";
        }

        private void chkDarkContrast_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDarkContrast.Checked)
                contrast = true;
            else
                contrast = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ini.Write("FontMultiplier", fontMultiplier);
            ini.Write("Contrast", contrast.ToString());
            ini.Write("FontFamily", cbxFont.SelectedItem.ToString());
            Logic.Appearance.RefreshForm(MyParent);
            Logic.Appearance.RefreshForm(this);
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            if (ini.Read("Contrast") == "True")
                chkDarkContrast.Checked = true;
            if (ini.Read("FontMultiplier") == "1.5")
                chkFontSize.Checked = true;

            foreach (FontFamily font in System.Drawing.FontFamily.Families)
            {
                cbxFont.Items.Add(font.Name);
            }
            cbxFont.SelectedItem = ini.Read("FontFamily");

            Logic.Appearance.RefreshForm(this);
        }

        private void cbxFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Font = new System.Drawing.Font(
                cbxFont.SelectedItem.ToString(),
                14F,
                System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point,
                ((byte)(0)));
        }
    }
}
