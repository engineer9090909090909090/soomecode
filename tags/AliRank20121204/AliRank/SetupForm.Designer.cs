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
            this.CancelBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.VPNRadioBtn = new System.Windows.Forms.RadioButton();
            this.AgentRadioBtn = new System.Windows.Forms.RadioButton();
            this.NoneRadioBtn = new System.Windows.Forms.RadioButton();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImportBtn
            // 
            this.ImportBtn.Location = new System.Drawing.Point(120, 215);
            this.ImportBtn.Name = "ImportBtn";
            this.ImportBtn.Size = new System.Drawing.Size(75, 23);
            this.ImportBtn.TabIndex = 0;
            this.ImportBtn.Text = "确认(&S)";
            this.ImportBtn.UseVisualStyleBackColor = true;
            this.ImportBtn.Click += new System.EventHandler(this.ImportBtn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(202, 34);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(123, 21);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "50";
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(104, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "循环点击次数：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(331, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "次";
            // 
            // errorMsg
            // 
            this.errorMsg.AutoSize = true;
            this.errorMsg.ForeColor = System.Drawing.Color.Red;
            this.errorMsg.Location = new System.Drawing.Point(86, 172);
            this.errorMsg.Name = "errorMsg";
            this.errorMsg.Size = new System.Drawing.Size(0, 12);
            this.errorMsg.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.NoneRadioBtn);
            this.groupBox2.Controls.Add(this.AgentRadioBtn);
            this.groupBox2.Controls.Add(this.VPNRadioBtn);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.CancelBtn);
            this.groupBox2.Controls.Add(this.ImportBtn);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(515, 269);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(282, 215);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 5;
            this.CancelBtn.Text = "取消(&C)";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(104, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "网络连接类型：";
            // 
            // VPNRadioBtn
            // 
            this.VPNRadioBtn.AutoSize = true;
            this.VPNRadioBtn.Checked = true;
            this.VPNRadioBtn.Location = new System.Drawing.Point(199, 95);
            this.VPNRadioBtn.Name = "VPNRadioBtn";
            this.VPNRadioBtn.Size = new System.Drawing.Size(65, 16);
            this.VPNRadioBtn.TabIndex = 7;
            this.VPNRadioBtn.TabStop = true;
            this.VPNRadioBtn.Text = "VPN网络";
            this.VPNRadioBtn.UseVisualStyleBackColor = true;
            // 
            // AgentRadioBtn
            // 
            this.AgentRadioBtn.AutoSize = true;
            this.AgentRadioBtn.Location = new System.Drawing.Point(270, 97);
            this.AgentRadioBtn.Name = "AgentRadioBtn";
            this.AgentRadioBtn.Size = new System.Drawing.Size(83, 16);
            this.AgentRadioBtn.TabIndex = 8;
            this.AgentRadioBtn.Text = "代理IP网络";
            this.AgentRadioBtn.UseVisualStyleBackColor = true;
            // 
            // NoneRadioBtn
            // 
            this.NoneRadioBtn.AutoSize = true;
            this.NoneRadioBtn.Location = new System.Drawing.Point(363, 97);
            this.NoneRadioBtn.Name = "NoneRadioBtn";
            this.NoneRadioBtn.Size = new System.Drawing.Size(47, 16);
            this.NoneRadioBtn.TabIndex = 9;
            this.NoneRadioBtn.Text = "普通";
            this.NoneRadioBtn.UseVisualStyleBackColor = true;
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 293);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.errorMsg);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "点击设置";
            this.Load += new System.EventHandler(this.ImpKwForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.RadioButton AgentRadioBtn;
        private System.Windows.Forms.RadioButton NoneRadioBtn;
    }
}