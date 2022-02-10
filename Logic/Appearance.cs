using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
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
                bool contrast = bool.Parse(Globals.DARK_MODE);
                if (contrast)
                {
                    form.BackColor = Color.FromArgb(255, 48, 48, 48);
                    form.ForeColor = Color.White;

                    foreach (var button in GetAll(form, typeof(Button)))
                    {
                        (button as Button).BackColor = Color.FromArgb(255, 70, 70, 70);
                        (button as Button).FlatStyle = FlatStyle.Flat;
                        (button as Button).FlatAppearance.BorderColor = Color.FromArgb(255, 70, 70, 70);
                    }

                    foreach (var gridView in GetAll(form, typeof(DataGridView)))
                        gridView.ForeColor = Color.Black;
                }
                else
                {
                    form.BackColor = Color.White;
                    form.ForeColor = Color.Black;

                    foreach (var button in GetAll(form, typeof(Button)))
                    {
                        (button as Button).BackColor = Color.FromArgb(255, 220, 220, 220);
                        (button as Button).FlatStyle = FlatStyle.Flat;
                        (button as Button).FlatAppearance.BorderColor = Color.FromArgb(255, 70, 70, 70);
                    }
                }
            }
        }

        public static IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }
    }
}
