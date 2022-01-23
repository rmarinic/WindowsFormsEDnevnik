
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
            this.pnlGrades = new System.Windows.Forms.Panel();
            this.pnlSubjects = new System.Windows.Forms.Panel();
            this.pnlAllGrades = new System.Windows.Forms.Panel();
            this.lblGradesTitle = new System.Windows.Forms.Label();
            this.lblAllGradesTitle = new System.Windows.Forms.Label();
            this.lblSubjectsTitle = new System.Windows.Forms.Label();
            this.pnlHome = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlGrades.SuspendLayout();
            this.pnlSubjects.SuspendLayout();
            this.pnlAllGrades.SuspendLayout();
            this.pnlHome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            // pnlGrades
            // 
            this.pnlGrades.Controls.Add(this.lblGradesTitle);
            this.pnlGrades.Location = new System.Drawing.Point(13, 39);
            this.pnlGrades.Name = "pnlGrades";
            this.pnlGrades.Size = new System.Drawing.Size(775, 398);
            this.pnlGrades.TabIndex = 7;
            this.pnlGrades.Visible = false;
            // 
            // pnlSubjects
            // 
            this.pnlSubjects.Controls.Add(this.lblSubjectsTitle);
            this.pnlSubjects.Location = new System.Drawing.Point(12, 39);
            this.pnlSubjects.Name = "pnlSubjects";
            this.pnlSubjects.Size = new System.Drawing.Size(776, 398);
            this.pnlSubjects.TabIndex = 1;
            this.pnlSubjects.Visible = false;
            // 
            // pnlAllGrades
            // 
            this.pnlAllGrades.Controls.Add(this.lblAllGradesTitle);
            this.pnlAllGrades.Location = new System.Drawing.Point(12, 40);
            this.pnlAllGrades.Name = "pnlAllGrades";
            this.pnlAllGrades.Size = new System.Drawing.Size(776, 398);
            this.pnlAllGrades.TabIndex = 0;
            this.pnlAllGrades.Visible = false;
            // 
            // lblGradesTitle
            // 
            this.lblGradesTitle.AutoSize = true;
            this.lblGradesTitle.Location = new System.Drawing.Point(329, 28);
            this.lblGradesTitle.Name = "lblGradesTitle";
            this.lblGradesTitle.Size = new System.Drawing.Size(85, 13);
            this.lblGradesTitle.TabIndex = 0;
            this.lblGradesTitle.Text = "Trenutne ocjene";
            // 
            // lblAllGradesTitle
            // 
            this.lblAllGradesTitle.AutoSize = true;
            this.lblAllGradesTitle.Location = new System.Drawing.Point(343, 15);
            this.lblAllGradesTitle.Name = "lblAllGradesTitle";
            this.lblAllGradesTitle.Size = new System.Drawing.Size(61, 13);
            this.lblAllGradesTitle.TabIndex = 0;
            this.lblAllGradesTitle.Text = "Sve ocjene";
            // 
            // lblSubjectsTitle
            // 
            this.lblSubjectsTitle.AutoSize = true;
            this.lblSubjectsTitle.Location = new System.Drawing.Point(335, 28);
            this.lblSubjectsTitle.Name = "lblSubjectsTitle";
            this.lblSubjectsTitle.Size = new System.Drawing.Size(80, 13);
            this.lblSubjectsTitle.TabIndex = 1;
            this.lblSubjectsTitle.Text = "Popis predmeta";
            // 
            // pnlHome
            // 
            this.pnlHome.Controls.Add(this.pictureBox1);
            this.pnlHome.Controls.Add(this.label1);
            this.pnlHome.Location = new System.Drawing.Point(12, 39);
            this.pnlHome.Name = "pnlHome";
            this.pnlHome.Size = new System.Drawing.Size(776, 399);
            this.pnlHome.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(239, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dobrodošao/la Ime Prezime!";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::NTP_Projekt.Properties.Resources.aa_pfp;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(333, 88);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(98, 101);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlHome);
            this.Controls.Add(this.pnlGrades);
            this.Controls.Add(this.pnlSubjects);
            this.Controls.Add(this.pnlAllGrades);
            this.Controls.Add(this.cbxGrades);
            this.Name = "Main";
            this.Text = "E-Dnevnik";
            this.pnlGrades.ResumeLayout(false);
            this.pnlGrades.PerformLayout();
            this.pnlSubjects.ResumeLayout(false);
            this.pnlSubjects.PerformLayout();
            this.pnlAllGrades.ResumeLayout(false);
            this.pnlAllGrades.PerformLayout();
            this.pnlHome.ResumeLayout(false);
            this.pnlHome.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxGrades;
        private System.Windows.Forms.Panel pnlGrades;
        private System.Windows.Forms.Label lblGradesTitle;
        private System.Windows.Forms.Panel pnlSubjects;
        private System.Windows.Forms.Panel pnlAllGrades;
        private System.Windows.Forms.Label lblSubjectsTitle;
        private System.Windows.Forms.Label lblAllGradesTitle;
        private System.Windows.Forms.Panel pnlHome;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}