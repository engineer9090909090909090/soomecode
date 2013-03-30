namespace AliHelper
{
    partial class DbsetForm
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
            this.RemoteDbCheck = new System.Windows.Forms.RadioButton();
            this.LocalDbCheck = new System.Windows.Forms.RadioButton();
            this.MySqlSetGroup = new System.Windows.Forms.GroupBox();
            this.ErrorMsg = new System.Windows.Forms.Label();
            this.ConnTestButton = new System.Windows.Forms.Button();
            this.Password = new System.Windows.Forms.TextBox();
            this.Username = new System.Windows.Forms.TextBox();
            this.Database = new System.Windows.Forms.TextBox();
            this.Server = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.MySqlSetGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // RemoteDbCheck
            // 
            this.RemoteDbCheck.AutoSize = true;
            this.RemoteDbCheck.Location = new System.Drawing.Point(192, 18);
            this.RemoteDbCheck.Name = "RemoteDbCheck";
            this.RemoteDbCheck.Size = new System.Drawing.Size(137, 16);
            this.RemoteDbCheck.TabIndex = 1;
            this.RemoteDbCheck.Text = "使用远程MySql数据库";
            this.RemoteDbCheck.UseVisualStyleBackColor = true;
            // 
            // LocalDbCheck
            // 
            this.LocalDbCheck.AutoSize = true;
            this.LocalDbCheck.Checked = true;
            this.LocalDbCheck.Location = new System.Drawing.Point(47, 17);
            this.LocalDbCheck.Name = "LocalDbCheck";
            this.LocalDbCheck.Size = new System.Drawing.Size(107, 16);
            this.LocalDbCheck.TabIndex = 0;
            this.LocalDbCheck.TabStop = true;
            this.LocalDbCheck.Text = "使用本地数据库";
            this.LocalDbCheck.UseVisualStyleBackColor = true;
            this.LocalDbCheck.CheckedChanged += new System.EventHandler(this.DbCheck_CheckedChanged);
            // 
            // MySqlSetGroup
            // 
            this.MySqlSetGroup.Controls.Add(this.ErrorMsg);
            this.MySqlSetGroup.Controls.Add(this.ConnTestButton);
            this.MySqlSetGroup.Controls.Add(this.Password);
            this.MySqlSetGroup.Controls.Add(this.Username);
            this.MySqlSetGroup.Controls.Add(this.Database);
            this.MySqlSetGroup.Controls.Add(this.Server);
            this.MySqlSetGroup.Controls.Add(this.label4);
            this.MySqlSetGroup.Controls.Add(this.label3);
            this.MySqlSetGroup.Controls.Add(this.label2);
            this.MySqlSetGroup.Controls.Add(this.label1);
            this.MySqlSetGroup.Location = new System.Drawing.Point(12, 50);
            this.MySqlSetGroup.Name = "MySqlSetGroup";
            this.MySqlSetGroup.Size = new System.Drawing.Size(471, 245);
            this.MySqlSetGroup.TabIndex = 1;
            this.MySqlSetGroup.TabStop = false;
            this.MySqlSetGroup.Text = "远程MySql数据连接设置";
            // 
            // ErrorMsg
            // 
            this.ErrorMsg.AutoSize = true;
            this.ErrorMsg.ForeColor = System.Drawing.Color.Red;
            this.ErrorMsg.Location = new System.Drawing.Point(82, 179);
            this.ErrorMsg.Name = "ErrorMsg";
            this.ErrorMsg.Size = new System.Drawing.Size(0, 12);
            this.ErrorMsg.TabIndex = 10;
            // 
            // ConnTestButton
            // 
            this.ConnTestButton.Location = new System.Drawing.Point(191, 203);
            this.ConnTestButton.Name = "ConnTestButton";
            this.ConnTestButton.Size = new System.Drawing.Size(224, 23);
            this.ConnTestButton.TabIndex = 8;
            this.ConnTestButton.Text = "测试连接";
            this.ConnTestButton.UseVisualStyleBackColor = true;
            this.ConnTestButton.Click += new System.EventHandler(this.ConnTestButton_Click);
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(189, 147);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(224, 21);
            this.Password.TabIndex = 7;
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(189, 112);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(224, 21);
            this.Username.TabIndex = 6;
            this.Username.Text = "root";
            // 
            // Database
            // 
            this.Database.Location = new System.Drawing.Point(189, 71);
            this.Database.Name = "Database";
            this.Database.Size = new System.Drawing.Size(224, 21);
            this.Database.TabIndex = 5;
            this.Database.Text = "Example";
            // 
            // Server
            // 
            this.Server.Location = new System.Drawing.Point(189, 29);
            this.Server.Name = "Server";
            this.Server.Size = new System.Drawing.Size(224, 21);
            this.Server.TabIndex = 4;
            this.Server.Text = "localhost";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "数据库连接密码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "数据库连接用户：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "MySql数据库名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "MySql服务器地址：";
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(144, 301);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 9;
            this.SaveButton.Text = "保存(&S)";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(270, 301);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 11;
            this.CancelBtn.Text = "取消(&C)";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // DbsetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 333);
            this.Controls.Add(this.RemoteDbCheck);
            this.Controls.Add(this.LocalDbCheck);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.MySqlSetGroup);
            this.Controls.Add(this.SaveButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DbsetForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "数据库设置";
            this.Load += new System.EventHandler(this.DbsetForm_Load);
            this.MySqlSetGroup.ResumeLayout(false);
            this.MySqlSetGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton RemoteDbCheck;
        private System.Windows.Forms.RadioButton LocalDbCheck;
        private System.Windows.Forms.GroupBox MySqlSetGroup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.TextBox Database;
        private System.Windows.Forms.TextBox Server;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Button ConnTestButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Label ErrorMsg;
        private System.Windows.Forms.Button CancelBtn;
    }
}