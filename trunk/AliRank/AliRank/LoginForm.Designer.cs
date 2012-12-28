namespace AliRank
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ErrorMsg = new System.Windows.Forms.Label();
            this.loginBtn = new System.Windows.Forms.Button();
            this.cannelBtn = new System.Windows.Forms.Button();
            this.accountBox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.remind = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pictureBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("LiSu", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(75, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "帐 号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("LiSu", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(73, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "密 码：";
            // 
            // ErrorMsg
            // 
            this.ErrorMsg.AutoSize = true;
            this.ErrorMsg.BackColor = System.Drawing.Color.Transparent;
            this.ErrorMsg.ForeColor = System.Drawing.Color.Red;
            this.ErrorMsg.Location = new System.Drawing.Point(130, 142);
            this.ErrorMsg.Name = "ErrorMsg";
            this.ErrorMsg.Size = new System.Drawing.Size(0, 12);
            this.ErrorMsg.TabIndex = 2;
            // 
            // loginBtn
            // 
            this.loginBtn.Location = new System.Drawing.Point(104, 172);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(75, 23);
            this.loginBtn.TabIndex = 3;
            this.loginBtn.Text = "登录";
            this.loginBtn.UseVisualStyleBackColor = true;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // cannelBtn
            // 
            this.cannelBtn.Location = new System.Drawing.Point(216, 172);
            this.cannelBtn.Name = "cannelBtn";
            this.cannelBtn.Size = new System.Drawing.Size(75, 23);
            this.cannelBtn.TabIndex = 4;
            this.cannelBtn.Text = "取消";
            this.cannelBtn.UseVisualStyleBackColor = true;
            this.cannelBtn.Click += new System.EventHandler(this.cannelBtn_Click);
            // 
            // accountBox
            // 
            this.accountBox.Location = new System.Drawing.Point(130, 33);
            this.accountBox.Name = "accountBox";
            this.accountBox.Size = new System.Drawing.Size(189, 21);
            this.accountBox.TabIndex = 1;
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(130, 79);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PasswordChar = '*';
            this.passwordBox.Size = new System.Drawing.Size(189, 21);
            this.passwordBox.TabIndex = 2;
            // 
            // remind
            // 
            this.remind.AutoSize = true;
            this.remind.BackColor = System.Drawing.Color.Transparent;
            this.remind.Location = new System.Drawing.Point(224, 113);
            this.remind.Name = "remind";
            this.remind.Size = new System.Drawing.Size(96, 16);
            this.remind.TabIndex = 5;
            this.remind.Text = "记住登录信息";
            this.remind.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Controls.Add(this.passwordBox);
            this.pictureBox1.Controls.Add(this.accountBox);
            this.pictureBox1.Controls.Add(this.cannelBtn);
            this.pictureBox1.Controls.Add(this.loginBtn);
            this.pictureBox1.Controls.Add(this.ErrorMsg);
            this.pictureBox1.Controls.Add(this.label2);
            this.pictureBox1.Controls.Add(this.label1);
            this.pictureBox1.Controls.Add(this.remind);
            this.pictureBox1.Image = global::AliRank.Properties.Resources.bg;
            this.pictureBox1.Location = new System.Drawing.Point(-2, -2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(406, 231);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 226);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pictureBox1.ResumeLayout(false);
            this.pictureBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ErrorMsg;
        private System.Windows.Forms.Button loginBtn;
        private System.Windows.Forms.Button cannelBtn;
        private System.Windows.Forms.TextBox accountBox;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.CheckBox remind;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}