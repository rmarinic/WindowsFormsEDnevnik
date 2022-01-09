
namespace NTP_Projekt
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbxGrades = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbxGrades
            // 
            this.cbxGrades.FormattingEnabled = true;
            this.cbxGrades.Items.AddRange(new object[] {
            "Trenutne ocjene",
            "Popis predmeta",
            "Sve ocjene"});
            this.cbxGrades.Location = new System.Drawing.Point(12, 12);
            this.cbxGrades.Name = "cbxGrades";
            this.cbxGrades.Size = new System.Drawing.Size(121, 21);
            this.cbxGrades.TabIndex = 6;
            this.cbxGrades.Text = "Ocjene";
            this.cbxGrades.SelectedIndexChanged += new System.EventHandler(this.cbxGrades_SelectedIndexChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cbxGrades);
            this.Name = "Main";
            this.Text = "E-Dnevnik";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxGrades;
    }
}