namespace BonTemps
{
    partial class FormSplashScreen
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
            this.btnLogin = new System.Windows.Forms.Button();
            this.tbxPassword = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblLoginStatus = new System.Windows.Forms.Label();
            this.llblAdminLogin = new System.Windows.Forms.LinkLabel();
            this.tbxUsername = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(140, 129);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // tbxPassword
            // 
            this.tbxPassword.Location = new System.Drawing.Point(24, 103);
            this.tbxPassword.Name = "tbxPassword";
            this.tbxPassword.PasswordChar = '*';
            this.tbxPassword.Size = new System.Drawing.Size(306, 20);
            this.tbxPassword.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(113, 158);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(127, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.BackColor = System.Drawing.Color.Transparent;
            this.lblUsername.ForeColor = System.Drawing.Color.Black;
            this.lblUsername.Location = new System.Drawing.Point(21, 40);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 13);
            this.lblUsername.TabIndex = 7;
            this.lblUsername.Text = "Username:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.ForeColor = System.Drawing.Color.Black;
            this.lblPassword.Location = new System.Drawing.Point(21, 87);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "Password:";
            // 
            // lblLoginStatus
            // 
            this.lblLoginStatus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblLoginStatus.AutoSize = true;
            this.lblLoginStatus.Location = new System.Drawing.Point(130, 184);
            this.lblLoginStatus.Name = "lblLoginStatus";
            this.lblLoginStatus.Size = new System.Drawing.Size(0, 13);
            this.lblLoginStatus.TabIndex = 4;
            this.lblLoginStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // llblAdminLogin
            // 
            this.llblAdminLogin.ActiveLinkColor = System.Drawing.Color.DarkGray;
            this.llblAdminLogin.AutoSize = true;
            this.llblAdminLogin.BackColor = System.Drawing.Color.Transparent;
            this.llblAdminLogin.DisabledLinkColor = System.Drawing.Color.White;
            this.llblAdminLogin.LinkColor = System.Drawing.Color.DarkGray;
            this.llblAdminLogin.Location = new System.Drawing.Point(172, 214);
            this.llblAdminLogin.Name = "llblAdminLogin";
            this.llblAdminLogin.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.llblAdminLogin.Size = new System.Drawing.Size(12, 17);
            this.llblAdminLogin.TabIndex = 5;
            this.llblAdminLogin.TabStop = true;
            this.llblAdminLogin.Text = "--";
            this.llblAdminLogin.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.llblAdminLogin.UseCompatibleTextRendering = true;
            this.llblAdminLogin.VisitedLinkColor = System.Drawing.Color.DarkGray;
            this.llblAdminLogin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblAdminLogin_LinkClicked);
            // 
            // tbxUsername
            // 
            this.tbxUsername.Location = new System.Drawing.Point(24, 57);
            this.tbxUsername.Name = "tbxUsername";
            this.tbxUsername.Size = new System.Drawing.Size(306, 20);
            this.tbxUsername.TabIndex = 1;
            // 
            // FormSplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BonTemps.Properties.Resources.splashscreen_background;
            this.ClientSize = new System.Drawing.Size(360, 240);
            this.ControlBox = false;
            this.Controls.Add(this.tbxUsername);
            this.Controls.Add(this.llblAdminLogin);
            this.Controls.Add(this.lblLoginStatus);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.tbxPassword);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(360, 240);
            this.MinimumSize = new System.Drawing.Size(360, 240);
            this.Name = "FormSplashScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormSplashScreen";
            this.TransparencyKey = System.Drawing.Color.Magenta;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox tbxPassword;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblLoginStatus;
        private System.Windows.Forms.LinkLabel llblAdminLogin;
        private System.Windows.Forms.TextBox tbxUsername;
    }
}