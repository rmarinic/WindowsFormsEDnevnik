﻿
namespace NTP_Projekt
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.lblEmailLogin = new System.Windows.Forms.Label();
            this.lblPasswordLogin = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.btnRegister = new System.Windows.Forms.Button();
            this.pnlRegister = new System.Windows.Forms.Panel();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtEmailReg = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtCountry = new System.Windows.Forms.TextBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtDob = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblSurname = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblCountry = new System.Windows.Forms.Label();
            this.lblDob = new System.Windows.Forms.Label();
            this.lblCity = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.lblPass2 = new System.Windows.Forms.Label();
            this.txtPassReg = new System.Windows.Forms.TextBox();
            this.txtPassReg2 = new System.Windows.Forms.TextBox();
            this.btnReg = new System.Windows.Forms.Button();
            this.JMBAG = new System.Windows.Forms.Label();
            this.txtJmbag = new System.Windows.Forms.TextBox();
            this.pnlLogin.SuspendLayout();
            this.pnlRegister.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblEmailLogin
            // 
            resources.ApplyResources(this.lblEmailLogin, "lblEmailLogin");
            this.lblEmailLogin.Name = "lblEmailLogin";
            // 
            // lblPasswordLogin
            // 
            resources.ApplyResources(this.lblPasswordLogin, "lblPasswordLogin");
            this.lblPasswordLogin.Name = "lblPasswordLogin";
            // 
            // txtEmail
            // 
            resources.ApplyResources(this.txtEmail, "txtEmail");
            this.txtEmail.Name = "txtEmail";
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            // 
            // btnLogin
            // 
            resources.ApplyResources(this.btnLogin, "btnLogin");
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pnlLogin
            // 
            this.pnlLogin.Controls.Add(this.btnRegister);
            this.pnlLogin.Controls.Add(this.lblEmailLogin);
            this.pnlLogin.Controls.Add(this.lblPasswordLogin);
            this.pnlLogin.Controls.Add(this.txtEmail);
            this.pnlLogin.Controls.Add(this.btnLogin);
            this.pnlLogin.Controls.Add(this.txtPassword);
            resources.ApplyResources(this.pnlLogin, "pnlLogin");
            this.pnlLogin.Name = "pnlLogin";
            // 
            // btnRegister
            // 
            resources.ApplyResources(this.btnRegister, "btnRegister");
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // pnlRegister
            // 
            this.pnlRegister.Controls.Add(this.txtJmbag);
            this.pnlRegister.Controls.Add(this.JMBAG);
            this.pnlRegister.Controls.Add(this.btnReg);
            this.pnlRegister.Controls.Add(this.txtPassReg2);
            this.pnlRegister.Controls.Add(this.txtPassReg);
            this.pnlRegister.Controls.Add(this.lblPass2);
            this.pnlRegister.Controls.Add(this.lblPass);
            this.pnlRegister.Controls.Add(this.lblCity);
            this.pnlRegister.Controls.Add(this.lblDob);
            this.pnlRegister.Controls.Add(this.lblCountry);
            this.pnlRegister.Controls.Add(this.lblAddress);
            this.pnlRegister.Controls.Add(this.lblEmail);
            this.pnlRegister.Controls.Add(this.lblSurname);
            this.pnlRegister.Controls.Add(this.lblFirstName);
            this.pnlRegister.Controls.Add(this.txtDob);
            this.pnlRegister.Controls.Add(this.txtCity);
            this.pnlRegister.Controls.Add(this.txtCountry);
            this.pnlRegister.Controls.Add(this.txtAddress);
            this.pnlRegister.Controls.Add(this.txtEmailReg);
            this.pnlRegister.Controls.Add(this.txtLastName);
            this.pnlRegister.Controls.Add(this.txtFirstName);
            resources.ApplyResources(this.pnlRegister, "pnlRegister");
            this.pnlRegister.Name = "pnlRegister";
            // 
            // txtFirstName
            // 
            resources.ApplyResources(this.txtFirstName, "txtFirstName");
            this.txtFirstName.Name = "txtFirstName";
            // 
            // txtLastName
            // 
            resources.ApplyResources(this.txtLastName, "txtLastName");
            this.txtLastName.Name = "txtLastName";
            // 
            // txtEmailReg
            // 
            resources.ApplyResources(this.txtEmailReg, "txtEmailReg");
            this.txtEmailReg.Name = "txtEmailReg";
            // 
            // txtAddress
            // 
            resources.ApplyResources(this.txtAddress, "txtAddress");
            this.txtAddress.Name = "txtAddress";
            // 
            // txtCountry
            // 
            resources.ApplyResources(this.txtCountry, "txtCountry");
            this.txtCountry.Name = "txtCountry";
            // 
            // txtCity
            // 
            resources.ApplyResources(this.txtCity, "txtCity");
            this.txtCity.Name = "txtCity";
            // 
            // txtDob
            // 
            resources.ApplyResources(this.txtDob, "txtDob");
            this.txtDob.Name = "txtDob";
            // 
            // lblFirstName
            // 
            resources.ApplyResources(this.lblFirstName, "lblFirstName");
            this.lblFirstName.Name = "lblFirstName";
            // 
            // lblSurname
            // 
            resources.ApplyResources(this.lblSurname, "lblSurname");
            this.lblSurname.Name = "lblSurname";
            // 
            // lblEmail
            // 
            resources.ApplyResources(this.lblEmail, "lblEmail");
            this.lblEmail.Name = "lblEmail";
            // 
            // lblAddress
            // 
            resources.ApplyResources(this.lblAddress, "lblAddress");
            this.lblAddress.Name = "lblAddress";
            // 
            // lblCountry
            // 
            resources.ApplyResources(this.lblCountry, "lblCountry");
            this.lblCountry.Name = "lblCountry";
            // 
            // lblDob
            // 
            resources.ApplyResources(this.lblDob, "lblDob");
            this.lblDob.Name = "lblDob";
            // 
            // lblCity
            // 
            resources.ApplyResources(this.lblCity, "lblCity");
            this.lblCity.Name = "lblCity";
            // 
            // lblPass
            // 
            resources.ApplyResources(this.lblPass, "lblPass");
            this.lblPass.Name = "lblPass";
            // 
            // lblPass2
            // 
            resources.ApplyResources(this.lblPass2, "lblPass2");
            this.lblPass2.Name = "lblPass2";
            // 
            // txtPassReg
            // 
            resources.ApplyResources(this.txtPassReg, "txtPassReg");
            this.txtPassReg.Name = "txtPassReg";
            // 
            // txtPassReg2
            // 
            resources.ApplyResources(this.txtPassReg2, "txtPassReg2");
            this.txtPassReg2.Name = "txtPassReg2";
            // 
            // btnReg
            // 
            resources.ApplyResources(this.btnReg, "btnReg");
            this.btnReg.Name = "btnReg";
            this.btnReg.UseVisualStyleBackColor = true;
            this.btnReg.Click += new System.EventHandler(this.btnReg_Click);
            // 
            // JMBAG
            // 
            resources.ApplyResources(this.JMBAG, "JMBAG");
            this.JMBAG.Name = "JMBAG";
            // 
            // txtJmbag
            // 
            resources.ApplyResources(this.txtJmbag, "txtJmbag");
            this.txtJmbag.Name = "txtJmbag";
            // 
            // Login
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlRegister);
            this.Controls.Add(this.pnlLogin);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Login";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Login_FormClosed);
            this.Load += new System.EventHandler(this.Login_Load);
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.pnlRegister.ResumeLayout(false);
            this.pnlRegister.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblEmailLogin;
        private System.Windows.Forms.Label lblPasswordLogin;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Panel pnlRegister;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.Label lblDob;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblSurname;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.TextBox txtDob;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.TextBox txtCountry;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtEmailReg;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Button btnReg;
        private System.Windows.Forms.TextBox txtPassReg2;
        private System.Windows.Forms.TextBox txtPassReg;
        private System.Windows.Forms.Label lblPass2;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtJmbag;
        private System.Windows.Forms.Label JMBAG;
    }
}

