using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NTP_Projekt.Logic
{
    class Appearance
    {
        public static void RefreshForm(Form form)
        {
            //Učitavanje iz .ini datoteke
            IniFile ini = new IniFile("settings.ini");
            Globals.FONT_NAME = (ini.Read("FontFamily"));
            Globals.FONT_MULTIPLIER = (ini.Read("FontMultiplier"));
            Globals.DARK_MODE = (ini.Read("Contrast"));

            if (!String.IsNullOrEmpty(Globals.FONT_NAME) && !String.IsNullOrEmpty(Globals.FONT_MULTIPLIER))
            {
                try
                {
                    form.Font = new System.Drawing.Font(
                    Globals.FONT_NAME,
                    8.5F * float.Parse(Globals.FONT_MULTIPLIER),
                    System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point,
                    ((byte)(0)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error setting font from .ini - " + ex.Message);
                }
            }

            if (!String.IsNullOrEmpty(Globals.DARK_MODE))
            {
                bool contrast = bool.Parse(ini.Read("Contrast"));
                if (contrast)
                {
                    form.BackColor = Color.FromArgb(255, 48, 48, 48);
                    form.ForeColor = Color.White;
                    SetButtonsDark(form);
                }
                else
                {
                    form.BackColor = Color.White;
                    form.ForeColor = Color.Black;
                    SetButtonsLight(form);
                }
            }
        }

        private static void SetButtonsDark(Form form)
        {
            foreach (var button in form.Controls.OfType<Button>())
            {
                button.BackColor = Color.FromArgb(255, 70, 70, 70);
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderColor = Color.FromArgb(255, 70, 70, 70);
            }
            foreach (var panel in form.Controls.OfType<Panel>())
            {
                foreach (var button in panel.Controls.OfType<Button>())
                {
                    button.BackColor = Color.FromArgb(255, 70, 70, 70);
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderColor = Color.FromArgb(255, 70, 70, 70);
                }
            }
        }

        private static void SetButtonsLight(Form form)
        {
            foreach (var button in form.Controls.OfType<Button>())
            {
                button.BackColor = Color.FromArgb(255, 220, 220, 220);
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderColor = Color.FromArgb(255, 70, 70, 70);
            }
            foreach (var panel in form.Controls.OfType<Panel>())
            {
                foreach (var button in panel.Controls.OfType<Button>())
                {
                    button.BackColor = Color.FromArgb(255, 220, 220, 220);
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderColor = Color.FromArgb(255, 70, 70, 70);
                }
            }
        }
    }
}
