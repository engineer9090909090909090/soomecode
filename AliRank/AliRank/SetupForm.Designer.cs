namespace AliRank
{
    partial class SetupForm
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
            this.ImportBtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.errorMsg = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.VPNRadioBtn = new System.Windows.Forms.RadioButton();
            this.NoneRadioBtn = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OnlyClickRadioButton = new System.Windows.Forms.RadioButton();
            this.ClickInquiryRadioButton = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.MaxQueryPage = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.MaxPauseTime = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImportBtn
            // 
            this.ImportBtn.Location = new System.Drawing.Point(105, 223);
            this.ImportBtn.Name = "ImportBtn";
            this.ImportBtn.Size = new System.Drawing.Size(75, 23);
            this.ImportBtn.TabIndex = 0;
            this.ImportBtn.Text = "确认(&S)";
            this.ImportBtn.UseVisualStyleBackColor = true;
            this.ImportBtn.Click += new System.EventHandler(this.ImportBtn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(198, 23);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(102, 21);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "10";
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "循环点击次数：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(306, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "次";
            // 
            // errorMsg
            // 
            this.errorMsg.AutoSize = true;
            this.errorMsg.ForeColor = System.Drawing.Color.Red;
            this.errorMsg.Location = new System.Drawing.Point(103, 199);
            this.errorMsg.Name = "errorMsg";
            this.errorMsg.Size = new System.Drawing.Size(0, 12);
            this.errorMsg.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.MaxQueryPage);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.errorMsg);
            this.groupBox2.Controls.Add(this.MaxPauseTime);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.CancelBtn);
            this.groupBox2.Controls.Add(this.ImportBtn);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(8, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(417, 260);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.VPNRadioBtn);
            this.groupBox3.Controls.Add(this.NoneRadioBtn);
            this.groupBox3.Location = new System.Drawing.Point(188, 127);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(175, 32);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            // 
            // VPNRadioBtn
            // 
            this.VPNRadioBtn.AutoSize = true;
            this.VPNRadioBtn.Checked = true;
            this.VPNRadioBtn.Location = new System.Drawing.Point(6, 11);
            this.VPNRadioBtn.Name = "VPNRadioBtn";
            this.VPNRadioBtn.Size = new System.Drawing.Size(65, 16);
            this.VPNRadioBtn.TabIndex = 7;
            this.VPNRadioBtn.TabStop = true;
            this.VPNRadioBtn.Text = "VPN网络";
            this.VPNRadioBtn.UseVisualStyleBackColor = true;
            // 
            // NoneRadioBtn
            // 
            this.NoneRadioBtn.AutoSize = true;
            this.NoneRadioBtn.Location = new System.Drawing.Point(83, 11);
            this.NoneRadioBtn.Name = "NoneRadioBtn";
            this.NoneRadioBtn.Size = new System.Drawing.Size(47, 16);
            this.NoneRadioBtn.TabIndex = 9;
            this.NoneRadioBtn.Text = "普通";
            this.NoneRadioBtn.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OnlyClickRadioButton);
            this.groupBox1.Controls.Add(this.ClickInquiryRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(188, 163);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(175, 32);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            // 
            // OnlyClickRadioButton
            // 
            this.OnlyClickRadioButton.AutoSize = true;
            this.OnlyClickRadioButton.Checked = true;
            this.OnlyClickRadioButton.Location = new System.Drawing.Point(6, 12);
            this.OnlyClickRadioButton.Name = "OnlyClickRadioButton";
            this.OnlyClickRadioButton.Size = new System.Drawing.Size(59, 16);
            this.OnlyClickRadioButton.TabIndex = 17;
            this.OnlyClickRadioButton.TabStop = true;
            this.OnlyClickRadioButton.Text = "仅点击";
            this.OnlyClickRadioButton.UseVisualStyleBackColor = true;
            // 
            // ClickInquiryRadioButton
            // 
            this.ClickInquiryRadioButton.AutoSize = true;
            this.ClickInquiryRadioButton.Location = new System.Drawing.Point(83, 12);
            this.ClickInquiryRadioButton.Name = "ClickInquiryRadioButton";
            this.ClickInquiryRadioButton.Size = new System.Drawing.Size(83, 16);
            this.ClickInquiryRadioButton.TabIndex = 18;
            this.ClickInquiryRadioButton.Text = "点击且询盘";
            this.ClickInquiryRadioButton.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(79, 176);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "运行模式：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(79, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "最大查询页数：";
            // 
            // MaxQueryPage
            // 
            this.MaxQueryPage.Location = new System.Drawing.Point(198, 99);
            this.MaxQueryPage.Name = "MaxQueryPage";
            this.MaxQueryPage.Size = new System.Drawing.Size(102, 21);
            this.MaxQueryPage.TabIndex = 13;
            this.MaxQueryPage.Text = "10";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(306, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "页";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(79, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "随机暂停最大时间：";
            // 
            // MaxPauseTime
            // 
            this.MaxPauseTime.Location = new System.Drawing.Point(198, 62);
            this.MaxPauseTime.Name = "MaxPauseTime";
            this.MaxPauseTime.Size = new System.Drawing.Size(102, 21);
            this.MaxPauseTime.TabIndex = 10;
            this.MaxPauseTime.Text = "60";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(306, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "秒";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(79, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "网络连接类型：";
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(225, 223);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 5;
            this.CancelBtn.Text = "取消(&C)";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 274);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "点击参数设置";
            this.Load += new System.EventHandler(this.ImpKwForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ImportBtn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label errorMsg;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton VPNRadioBtn;
        private System.Windows.Forms.RadioButton NoneRadioBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox MaxPauseTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox MaxQueryPage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton ClickInquiryRadioButton;
        private System.Windows.Forms.RadioButton OnlyClickRadioButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}