namespace AliHelper
{
    partial class LoginForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.loginPanel = new System.Windows.Forms.Panel();
            this.LogPanel = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.loadingImage = new System.Windows.Forms.PictureBox();
            this.labErrorMsg = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbAccount = new System.Windows.Forms.ComboBox();
            this.changeModel = new System.Windows.Forms.Button();
            this.BtnLogin = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.loginPanel.SuspendLayout();
            this.LogPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingImage)).BeginInit();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(325, 163);
            this.webBrowser1.TabIndex = 6;
            this.webBrowser1.Url = new System.Uri("", System.UriKind.Relative);
            this.webBrowser1.Visible = false;
            this.webBrowser1.WebBrowserShortcutsEnabled = false;
            // 
            // loginPanel
            // 
            this.loginPanel.Controls.Add(this.LogPanel);
            this.loginPanel.Controls.Add(this.webBrowser1);
            this.loginPanel.Location = new System.Drawing.Point(0, 0);
            this.loginPanel.Name = "loginPanel";
            this.loginPanel.Size = new System.Drawing.Size(325, 163);
            this.loginPanel.TabIndex = 7;
            // 
            // LogPanel
            // 
            this.LogPanel.Controls.Add(this.groupBox1);
            this.LogPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogPanel.Location = new System.Drawing.Point(0, 0);
            this.LogPanel.Name = "LogPanel";
            this.LogPanel.Size = new System.Drawing.Size(325, 163);
            this.LogPanel.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.loadingImage);
            this.groupBox1.Controls.Add(this.labErrorMsg);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbAccount);
            this.groupBox1.Location = new System.Drawing.Point(8, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 158);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // loadingImage
            // 
            this.loadingImage.Image = global::AliHelper.Properties.Resources.loading;
            this.loadingImage.Location = new System.Drawing.Point(39, 109);
            this.loadingImage.Name = "loadingImage";
            this.loadingImage.Size = new System.Drawing.Size(38, 37);
            this.loadingImage.TabIndex = 5;
            this.loadingImage.TabStop = false;
            this.loadingImage.Visible = false;
            // 
            // labErrorMsg
            // 
            this.labErrorMsg.AutoSize = true;
            this.labErrorMsg.ForeColor = System.Drawing.Color.Red;
            this.labErrorMsg.Location = new System.Drawing.Point(88, 134);
            this.labErrorMsg.Name = "labErrorMsg";
            this.labErrorMsg.Size = new System.Drawing.Size(0, 12);
            this.labErrorMsg.TabIndex = 4;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(123, 108);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(96, 16);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "记住登录信息";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(90, 74);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(200, 21);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.Text = "soomes2008";
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "密　码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "帐　号：";
            // 
            // cmbAccount
            // 
            this.cmbAccount.FormattingEnabled = true;
            this.cmbAccount.Location = new System.Drawing.Point(91, 28);
            this.cmbAccount.Name = "cmbAccount";
            this.cmbAccount.Size = new System.Drawing.Size(200, 20);
            this.cmbAccount.TabIndex = 0;
            this.cmbAccount.Text = "soomes";
            // 
            // changeModel
            // 
            this.changeModel.Location = new System.Drawing.Point(7, 169);
            this.changeModel.Name = "changeModel";
            this.changeModel.Size = new System.Drawing.Size(127, 23);
            this.changeModel.TabIndex = 6;
            this.changeModel.Text = "切换到网页登录模式";
            this.changeModel.UseVisualStyleBackColor = true;
            this.changeModel.Click += new System.EventHandler(this.ChangeModel_Click);
            // 
            // BtnLogin
            // 
            this.BtnLogin.Location = new System.Drawing.Point(160, 169);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(62, 23);
            this.BtnLogin.TabIndex = 4;
            this.BtnLogin.Text = "登录(&L)";
            this.BtnLogin.UseVisualStyleBackColor = true;
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(242, 169);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(57, 23);
            this.BtnCancel.TabIndex = 5;
            this.BtnCancel.Text = "取消(&C)";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 196);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnLogin);
            this.Controls.Add(this.changeModel);
            this.Controls.Add(this.loginPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.loginPanel.ResumeLayout(false);
            this.LogPanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Panel loginPanel;
        private System.Windows.Forms.Button changeModel;
        private System.Windows.Forms.Button BtnLogin;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Panel LogPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbAccount;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label labErrorMsg;
        private System.Windows.Forms.PictureBox loadingImage;
    }
}

