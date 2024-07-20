namespace shift_making_man.Views
{
    partial class LoginForm
    {
        private System.Windows.Forms.Button btnCreateAdminAccount;

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnCreateAdminAccount = new System.Windows.Forms.Button();

            this.SuspendLayout();
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(108, 35);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(200, 20);
            this.txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(108, 75);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(200, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(30, 35);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 13);
            this.lblUsername.TabIndex = 2;
            this.lblUsername.Text = "ユーザー名";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(30, 75);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(52, 13);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "パスワード";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(108, 115);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "ログイン";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCreateAdminAccount
            // 
            this.btnCreateAdminAccount.Location = new System.Drawing.Point(200, 115);
            this.btnCreateAdminAccount.Name = "btnCreateAdminAccount";
            this.btnCreateAdminAccount.Size = new System.Drawing.Size(100, 23);
            this.btnCreateAdminAccount.TabIndex = 5;
            this.btnCreateAdminAccount.Text = "アカウント作成";
            this.btnCreateAdminAccount.UseVisualStyleBackColor = true;
            this.btnCreateAdminAccount.Click += new System.EventHandler(this.btnCreateAdminAccount_Click);
            // 
            // LoginForm
            // 
            this.ClientSize = new System.Drawing.Size(354, 161);
            this.Controls.Add(this.btnCreateAdminAccount);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Name = "LoginForm";
            this.Text = "ログイン";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnLogin;
    }
}
