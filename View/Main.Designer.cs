
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
            this.lblGradesTitle = new System.Windows.Forms.Label();
            this.pnlSubjects = new System.Windows.Forms.Panel();
            this.lblSubjectsTitle = new System.Windows.Forms.Label();
            this.pnlAllGrades = new System.Windows.Forms.Panel();
            this.lblAllGradesTitle = new System.Windows.Forms.Label();
            this.pnlHome = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
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
            // lblGradesTitle
            // 
            this.lblGradesTitle.AutoSize = true;
            this.lblGradesTitle.Location = new System.Drawing.Point(329, 28);
            this.lblGradesTitle.Name = "lblGradesTitle";
            this.lblGradesTitle.Size = new System.Drawing.Size(85, 13);
            this.lblGradesTitle.TabIndex = 0;
            this.lblGradesTitle.Text = "Trenutne ocjene";
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
            // lblSubjectsTitle
            // 
            this.lblSubjectsTitle.AutoSize = true;
            this.lblSubjectsTitle.Location = new System.Drawing.Point(335, 28);
            this.lblSubjectsTitle.Name = "lblSubjectsTitle";
            this.lblSubjectsTitle.Size = new System.Drawing.Size(80, 13);
            this.lblSubjectsTitle.TabIndex = 1;
            this.lblSubjectsTitle.Text = "Popis predmeta";
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
            // lblAllGradesTitle
            // 
            this.lblAllGradesTitle.AutoSize = true;
            this.lblAllGradesTitle.Location = new System.Drawing.Point(343, 15);
            this.lblAllGradesTitle.Name = "lblAllGradesTitle";
            this.lblAllGradesTitle.Size = new System.Drawing.Size(61, 13);
            this.lblAllGradesTitle.TabIndex = 0;
            this.lblAllGradesTitle.Text = "Sve ocjene";
            // 
            // pnlHome
            // 
            this.pnlHome.Controls.Add(this.button4);
            this.pnlHome.Controls.Add(this.button3);
            this.pnlHome.Controls.Add(this.button2);
            this.pnlHome.Controls.Add(this.txtFileName);
            this.pnlHome.Controls.Add(this.label2);
            this.pnlHome.Controls.Add(this.button1);
            this.pnlHome.Controls.Add(this.pictureBox1);
            this.pnlHome.Controls.Add(this.label1);
            this.pnlHome.Location = new System.Drawing.Point(12, 39);
            this.pnlHome.Name = "pnlHome";
            this.pnlHome.Size = new System.Drawing.Size(776, 399);
            this.pnlHome.TabIndex = 8;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(430, 308);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Decrypt";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(333, 309);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Encrypt";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(233, 261);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(198, 20);
            this.txtFileName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(172, 261);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "File name:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(445, 261);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Browse...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(559, 259);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
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
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
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
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
    }
}