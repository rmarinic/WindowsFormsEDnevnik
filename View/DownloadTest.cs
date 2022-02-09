using NTP_Projekt.Logic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NTP_Projekt.View
{
    public partial class DownloadTest : Form
    {
        public delegate void ProgressChangedHandler(long? totalFileSize, long totalBytesDownloaded, double? progressPercentage);

        public event ProgressChangedHandler ProgressChanged;
        public DownloadTest()
        {
            InitializeComponent();
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            label1.Text = "Preparing download...";
            int speed = GetSpeed();
            //string response = "https://cdn-129.anonfiles.com/l5c4U7Gax5/e38ebb3f-1644415053/covid_safety_measures.mp4";
            string response = "https://filebin.net/yl2uv4h1c9folmsm/covid_safety_measures.mp4";
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "Resources");

            var destinationFilePath = Path.GetFullPath("covid_safety.jpg");

            using (var client = new HttpClientDownloadWithProgress(response, destinationFilePath, speed))
            {
                client.ProgressChanged += (totalFileSize, totalBytesDownloaded, progressPercentage) => {
                    progressBar1.Maximum = (int)totalFileSize;
                    progressBar1.Value = (int)totalBytesDownloaded;
                    label1.Text = $"{progressPercentage}% ({totalBytesDownloaded/1000}/{totalFileSize/1000})";
                };

                await client.StartDownload();
            }
            MessageBox.Show("Done!");
        }

        private int GetSpeed()
        {
            int speed = 0;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    speed = 10;
                    break;
                case 1:
                    speed = 100;
                    break;
                case 2:
                    speed = 1000;
                    break;
                case 3:
                    speed = 5000;
                    break;
            }
            return speed;
        }

    }
}
