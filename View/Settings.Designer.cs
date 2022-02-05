
namespace NTP_Projekt
{
    partial class Settings
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chkFontSize = new System.Windows.Forms.CheckBox();
            this.chkDarkContrast = new System.Windows.Forms.CheckBox();
            this.chkUnderline = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbxFont = new System.Windows.Forms.ComboBox();
            this.lblFont = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Increase font size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(40, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(237, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Contrast (dark background)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(40, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "Underline links";
            // 
            // chkFontSize
            // 
            this.chkFontSize.AutoSize = true;
            this.chkFontSize.Location = new System.Drawing.Point(325, 62);
            this.chkFontSize.Name = "chkFontSize";
            this.chkFontSize.Size = new System.Drawing.Size(15, 14);
            this.chkFontSize.TabIndex = 4;
            this.chkFontSize.UseVisualStyleBackColor = true;
            this.chkFontSize.CheckedChanged += new System.EventHandler(this.chkFontSize_CheckedChanged);
            // 
            // chkDarkContrast
            // 
            this.chkDarkContrast.AutoSize = true;
            this.chkDarkContrast.Location = new System.Drawing.Point(325, 103);
            this.chkDarkContrast.Name = "chkDarkContrast";
            this.chkDarkContrast.Size = new System.Drawing.Size(15, 14);
            this.chkDarkContrast.TabIndex = 5;
            this.chkDarkContrast.UseVisualStyleBackColor = true;
            this.chkDarkContrast.CheckedChanged += new System.EventHandler(this.chkDarkContrast_CheckedChanged);
            // 
            // chkUnderline
            // 
            this.chkUnderline.AutoSize = true;
            this.chkUnderline.Location = new System.Drawing.Point(325, 141);
            this.chkUnderline.Name = "chkUnderline";
            this.chkUnderline.Size = new System.Drawing.Size(15, 14);
            this.chkUnderline.TabIndex = 7;
            this.chkUnderline.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(145, 310);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbxFont
            // 
            this.cbxFont.FormattingEnabled = true;
            this.cbxFont.Location = new System.Drawing.Point(219, 180);
            this.cbxFont.Name = "cbxFont";
            this.cbxFont.Size = new System.Drawing.Size(121, 21);
            this.cbxFont.TabIndex = 9;
            this.cbxFont.SelectedIndexChanged += new System.EventHandler(this.cbxFont_SelectedIndexChanged);
            // 
            // lblFont
            // 
            this.lblFont.AutoSize = true;
            this.lblFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFont.Location = new System.Drawing.Point(40, 175);
            this.lblFont.Name = "lblFont";
            this.lblFont.Size = new System.Drawing.Size(48, 24);
            this.lblFont.TabIndex = 10;
            this.lblFont.Text = "Font";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 372);
            this.Controls.Add(this.lblFont);
            this.Controls.Add(this.cbxFont);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkUnderline);
            this.Controls.Add(this.chkDarkContrast);
            this.Controls.Add(this.chkFontSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkFontSize;
        private System.Windows.Forms.CheckBox chkDarkContrast;
        private System.Windows.Forms.CheckBox chkUnderline;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbxFont;
        private System.Windows.Forms.Label lblFont;
    }
}