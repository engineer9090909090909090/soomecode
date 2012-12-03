namespace AliRank
{
    partial class VpnForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AddressBox = new System.Windows.Forms.TextBox();
            this.ErrorMsg = new System.Windows.Forms.Label();
            this.L2tpBtn = new System.Windows.Forms.RadioButton();
            this.PptpBtn = new System.Windows.Forms.RadioButton();
            this.L2tpKeyTxtBox = new System.Windows.Forms.TextBox();
            this.PasswordBox = new System.Windows.Forms.TextBox();
            this.UsernameBox = new System.Windows.Forms.TextBox();
            this.L2tpKeyLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.InsertBtn = new System.Windows.Forms.Button();
            this.DeleteBtn = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(808, 517);
            this.splitContainer1.SplitterDistance = 150;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AddressBox);
            this.groupBox1.Controls.Add(this.ErrorMsg);
            this.groupBox1.Controls.Add(this.L2tpBtn);
            this.groupBox1.Controls.Add(this.PptpBtn);
            this.groupBox1.Controls.Add(this.L2tpKeyTxtBox);
            this.groupBox1.Controls.Add(this.PasswordBox);
            this.groupBox1.Controls.Add(this.UsernameBox);
            this.groupBox1.Controls.Add(this.L2tpKeyLabel);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.InsertBtn);
            this.groupBox1.Controls.Add(this.DeleteBtn);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(784, 135);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // AddressBox
            // 
            this.AddressBox.Location = new System.Drawing.Point(92, 26);
            this.AddressBox.Name = "AddressBox";
            this.AddressBox.Size = new System.Drawing.Size(128, 21);
            this.AddressBox.TabIndex = 15;
            // 
            // ErrorMsg
            // 
            this.ErrorMsg.AutoSize = true;
            this.ErrorMsg.ForeColor = System.Drawing.Color.Red;
            this.ErrorMsg.Location = new System.Drawing.Point(35, 115);
            this.ErrorMsg.Name = "ErrorMsg";
            this.ErrorMsg.Size = new System.Drawing.Size(0, 12);
            this.ErrorMsg.TabIndex = 14;
            // 
            // L2tpBtn
            // 
            this.L2tpBtn.AutoSize = true;
            this.L2tpBtn.Location = new System.Drawing.Point(163, 78);
            this.L2tpBtn.Name = "L2tpBtn";
            this.L2tpBtn.Size = new System.Drawing.Size(47, 16);
            this.L2tpBtn.TabIndex = 12;
            this.L2tpBtn.TabStop = true;
            this.L2tpBtn.Text = "L2TP";
            this.L2tpBtn.UseVisualStyleBackColor = true;
            this.L2tpBtn.CheckedChanged += new System.EventHandler(this.L2tpBtn_CheckedChanged);
            // 
            // PptpBtn
            // 
            this.PptpBtn.AutoSize = true;
            this.PptpBtn.Checked = true;
            this.PptpBtn.Location = new System.Drawing.Point(92, 78);
            this.PptpBtn.Name = "PptpBtn";
            this.PptpBtn.Size = new System.Drawing.Size(47, 16);
            this.PptpBtn.TabIndex = 11;
            this.PptpBtn.TabStop = true;
            this.PptpBtn.Text = "PPTP";
            this.PptpBtn.UseVisualStyleBackColor = true;
            this.PptpBtn.CheckedChanged += new System.EventHandler(this.L2tpBtn_CheckedChanged);
            // 
            // L2tpKeyTxtBox
            // 
            this.L2tpKeyTxtBox.Location = new System.Drawing.Point(316, 75);
            this.L2tpKeyTxtBox.Name = "L2tpKeyTxtBox";
            this.L2tpKeyTxtBox.Size = new System.Drawing.Size(139, 21);
            this.L2tpKeyTxtBox.TabIndex = 10;
            // 
            // PasswordBox
            // 
            this.PasswordBox.Location = new System.Drawing.Point(529, 26);
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.PasswordChar = '*';
            this.PasswordBox.Size = new System.Drawing.Size(137, 21);
            this.PasswordBox.TabIndex = 9;
            // 
            // UsernameBox
            // 
            this.UsernameBox.Location = new System.Drawing.Point(316, 26);
            this.UsernameBox.Name = "UsernameBox";
            this.UsernameBox.Size = new System.Drawing.Size(139, 21);
            this.UsernameBox.TabIndex = 8;
            // 
            // L2tpKeyLabel
            // 
            this.L2tpKeyLabel.AutoSize = true;
            this.L2tpKeyLabel.Location = new System.Drawing.Point(248, 78);
            this.L2tpKeyLabel.Name = "L2tpKeyLabel";
            this.L2tpKeyLabel.Size = new System.Drawing.Size(65, 12);
            this.L2tpKeyLabel.TabIndex = 6;
            this.L2tpKeyLabel.Text = "L2TP密钥：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "VPN类型：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(494, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "密码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(260, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "用户名：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "VPN地址：";
            // 
            // InsertBtn
            // 
            this.InsertBtn.Location = new System.Drawing.Point(682, 26);
            this.InsertBtn.Name = "InsertBtn";
            this.InsertBtn.Size = new System.Drawing.Size(75, 23);
            this.InsertBtn.TabIndex = 0;
            this.InsertBtn.Text = "新增(&N)";
            this.InsertBtn.UseVisualStyleBackColor = true;
            this.InsertBtn.Click += new System.EventHandler(this.InsertBtn_Click);
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.Location = new System.Drawing.Point(682, 75);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(75, 23);
            this.DeleteBtn.TabIndex = 1;
            this.DeleteBtn.Text = "删除(&D)";
            this.DeleteBtn.UseVisualStyleBackColor = true;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 341);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(808, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(808, 363);
            this.dataGridView.TabIndex = 0;
            // 
            // VpnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 517);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VpnForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "VPN信息维护";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button DeleteBtn;
        private System.Windows.Forms.Button InsertBtn;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label L2tpKeyLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PasswordBox;
        private System.Windows.Forms.TextBox UsernameBox;
        private System.Windows.Forms.TextBox L2tpKeyTxtBox;
        private System.Windows.Forms.RadioButton L2tpBtn;
        private System.Windows.Forms.RadioButton PptpBtn;
        private System.Windows.Forms.Label ErrorMsg;
        private System.Windows.Forms.TextBox AddressBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
    }
}