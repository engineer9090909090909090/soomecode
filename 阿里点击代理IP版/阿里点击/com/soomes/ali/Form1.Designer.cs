namespace com.soomes.ali
{
    partial class Form1
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
            if (proxyForm != null)
            {
                proxyForm.Dispose();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.proxyBtn = new System.Windows.Forms.Button();
            this.vpnBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.productidtext = new System.Windows.Forms.TextBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.companyText = new System.Windows.Forms.TextBox();
            this.companyLabel = new System.Windows.Forms.Label();
            this.deleBtn = new System.Windows.Forms.Button();
            this.addBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.keywordText = new System.Windows.Forms.TextBox();
            this.keywordLabel = new System.Windows.Forms.Label();
            this.netGroup = new System.Windows.Forms.GroupBox();
            this.netset_vpn = new System.Windows.Forms.RadioButton();
            this.netset_proxyIp = new System.Windows.Forms.RadioButton();
            this.runTimeText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.startBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.netGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.netGroup);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(712, 566);
            this.splitContainer1.SplitterDistance = 96;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.proxyBtn);
            this.groupBox3.Controls.Add(this.vpnBtn);
            this.groupBox3.Location = new System.Drawing.Point(348, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(140, 90);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Proxy And VPN";
            // 
            // proxyBtn
            // 
            this.proxyBtn.Location = new System.Drawing.Point(18, 60);
            this.proxyBtn.Name = "proxyBtn";
            this.proxyBtn.Size = new System.Drawing.Size(107, 23);
            this.proxyBtn.TabIndex = 1;
            this.proxyBtn.Text = "Set Web Proxy";
            this.proxyBtn.UseVisualStyleBackColor = true;
            this.proxyBtn.Click += new System.EventHandler(this.proxyBtn_Click);
            // 
            // vpnBtn
            // 
            this.vpnBtn.Location = new System.Drawing.Point(18, 37);
            this.vpnBtn.Name = "vpnBtn";
            this.vpnBtn.Size = new System.Drawing.Size(107, 23);
            this.vpnBtn.TabIndex = 0;
            this.vpnBtn.Text = "Set VPN List";
            this.vpnBtn.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.productidtext);
            this.groupBox2.Controls.Add(this.saveBtn);
            this.groupBox2.Controls.Add(this.companyText);
            this.groupBox2.Controls.Add(this.companyLabel);
            this.groupBox2.Controls.Add(this.deleBtn);
            this.groupBox2.Controls.Add(this.addBtn);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.keywordText);
            this.groupBox2.Controls.Add(this.keywordLabel);
            this.groupBox2.Location = new System.Drawing.Point(10, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(323, 90);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Product Setting";
            // 
            // productidtext
            // 
            this.productidtext.Location = new System.Drawing.Point(85, 62);
            this.productidtext.Name = "productidtext";
            this.productidtext.Size = new System.Drawing.Size(158, 21);
            this.productidtext.TabIndex = 2;
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(255, 62);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(60, 23);
            this.saveBtn.TabIndex = 14;
            this.saveBtn.Text = "Save";
            this.toolTip.SetToolTip(this.saveBtn, "Save Alibaba Keyword Data!");
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // companyText
            // 
            this.companyText.Location = new System.Drawing.Point(85, 15);
            this.companyText.Name = "companyText";
            this.companyText.Size = new System.Drawing.Size(159, 21);
            this.companyText.TabIndex = 1;
            // 
            // companyLabel
            // 
            this.companyLabel.AutoSize = true;
            this.companyLabel.Location = new System.Drawing.Point(4, 19);
            this.companyLabel.Name = "companyLabel";
            this.companyLabel.Size = new System.Drawing.Size(77, 12);
            this.companyLabel.TabIndex = 12;
            this.companyLabel.Text = "Company Site";
            // 
            // deleBtn
            // 
            this.deleBtn.Location = new System.Drawing.Point(255, 38);
            this.deleBtn.Name = "deleBtn";
            this.deleBtn.Size = new System.Drawing.Size(60, 23);
            this.deleBtn.TabIndex = 9;
            this.deleBtn.Text = "Delete";
            this.deleBtn.UseVisualStyleBackColor = true;
            this.deleBtn.Click += new System.EventHandler(this.deleBtn_Click);
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(255, 15);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(60, 23);
            this.addBtn.TabIndex = 8;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "Product Id";
            // 
            // keywordText
            // 
            this.keywordText.Location = new System.Drawing.Point(85, 39);
            this.keywordText.Name = "keywordText";
            this.keywordText.Size = new System.Drawing.Size(158, 21);
            this.keywordText.TabIndex = 3;
            // 
            // keywordLabel
            // 
            this.keywordLabel.AutoSize = true;
            this.keywordLabel.Location = new System.Drawing.Point(33, 42);
            this.keywordLabel.Name = "keywordLabel";
            this.keywordLabel.Size = new System.Drawing.Size(47, 12);
            this.keywordLabel.TabIndex = 13;
            this.keywordLabel.Text = "Keyword";
            // 
            // netGroup
            // 
            this.netGroup.Controls.Add(this.netset_vpn);
            this.netGroup.Controls.Add(this.netset_proxyIp);
            this.netGroup.Controls.Add(this.runTimeText);
            this.netGroup.Controls.Add(this.label2);
            this.netGroup.Controls.Add(this.startBtn);
            this.netGroup.Controls.Add(this.stopBtn);
            this.netGroup.Location = new System.Drawing.Point(509, 6);
            this.netGroup.Name = "netGroup";
            this.netGroup.Size = new System.Drawing.Size(196, 90);
            this.netGroup.TabIndex = 5;
            this.netGroup.TabStop = false;
            this.netGroup.Text = "Running Setting";
            // 
            // netset_vpn
            // 
            this.netset_vpn.AutoSize = true;
            this.netset_vpn.Location = new System.Drawing.Point(17, 40);
            this.netset_vpn.Name = "netset_vpn";
            this.netset_vpn.Size = new System.Drawing.Size(71, 16);
            this.netset_vpn.TabIndex = 1;
            this.netset_vpn.Text = "Auto VPN";
            this.netset_vpn.UseVisualStyleBackColor = true;
            // 
            // netset_proxyIp
            // 
            this.netset_proxyIp.AutoSize = true;
            this.netset_proxyIp.Checked = true;
            this.netset_proxyIp.Location = new System.Drawing.Point(17, 17);
            this.netset_proxyIp.Name = "netset_proxyIp";
            this.netset_proxyIp.Size = new System.Drawing.Size(83, 16);
            this.netset_proxyIp.TabIndex = 2;
            this.netset_proxyIp.TabStop = true;
            this.netset_proxyIp.Text = "Auto Proxy";
            this.netset_proxyIp.UseVisualStyleBackColor = true;
            // 
            // runTimeText
            // 
            this.runTimeText.Location = new System.Drawing.Point(80, 61);
            this.runTimeText.Name = "runTimeText";
            this.runTimeText.Size = new System.Drawing.Size(31, 21);
            this.runTimeText.TabIndex = 4;
            this.runTimeText.Text = "50";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Run Number";
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(127, 35);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(61, 23);
            this.startBtn.TabIndex = 11;
            this.startBtn.Text = "Start";
            this.toolTip.SetToolTip(this.startBtn, "Start Run BackgroundWorker");
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Enabled = false;
            this.stopBtn.Location = new System.Drawing.Point(128, 59);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(61, 23);
            this.stopBtn.TabIndex = 10;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(712, 466);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 17);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(706, 446);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(698, 421);
            this.tabPage1.TabIndex = 12;
            this.tabPage1.Text = "Data Window";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.SizeChanged += new System.EventHandler(this.tabPage_SizeChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(240, 150);
            this.dataGridView1.TabIndex = 13;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.webBrowser1);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(698, 421);
            this.tabPage2.TabIndex = 14;
            this.tabPage2.Text = "IE Window";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.SizeChanged += new System.EventHandler(this.tabPage_SizeChanged);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(3, 3);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(250, 250);
            this.webBrowser1.TabIndex = 15;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 544);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(712, 22);
            this.statusStrip1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 566);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "阿里点击(代理IP版) http://rank.soomes.com";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.netGroup.ResumeLayout(false);
            this.netGroup.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox runTimeText;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox productidtext;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button deleBtn;
        public System.Windows.Forms.Button stopBtn;
        public System.Windows.Forms.TextBox keywordText;
        public System.Windows.Forms.Button startBtn;
        public System.Windows.Forms.Label companyLabel;
        public System.Windows.Forms.Button addBtn;
        public System.Windows.Forms.TextBox companyText;
        private System.Windows.Forms.Label keywordLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        public  System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.GroupBox netGroup;
        public System.Windows.Forms.RadioButton netset_vpn;
        public System.Windows.Forms.RadioButton netset_proxyIp;
        public System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.TabPage tabPage2;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button proxyBtn;
        private System.Windows.Forms.Button vpnBtn;
        private ProxyIPsForm proxyForm;
    }
}

