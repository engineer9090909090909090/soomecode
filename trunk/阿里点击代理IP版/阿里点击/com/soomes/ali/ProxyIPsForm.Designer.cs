namespace com.soomes.ali
{
    partial class ProxyIPsForm
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
            if (proxySearcher != null)
            {
                proxySearcher = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProxyIPsForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.stopBtn = new System.Windows.Forms.Button();
            this.searchBtn = new System.Windows.Forms.Button();
            this.cnproxyCheck = new System.Windows.Forms.CheckBox();
            this.minidailiCheck = new System.Windows.Forms.CheckBox();
            this.proxycnCheck = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.proxyGridView = new System.Windows.Forms.DataGridView();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.proxyGridView)).BeginInit();
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
            this.splitContainer1.Panel2.Controls.Add(this.proxyGridView);
            this.splitContainer1.Size = new System.Drawing.Size(533, 370);
            this.splitContainer1.SplitterDistance = 68;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.stopBtn);
            this.groupBox1.Controls.Add(this.searchBtn);
            this.groupBox1.Controls.Add(this.cnproxyCheck);
            this.groupBox1.Controls.Add(this.minidailiCheck);
            this.groupBox1.Controls.Add(this.proxycnCheck);
            this.groupBox1.Location = new System.Drawing.Point(8, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(518, 53);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Proxy Sites";
            // 
            // stopBtn
            // 
            this.stopBtn.Enabled = false;
            this.stopBtn.Location = new System.Drawing.Point(463, 19);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(46, 23);
            this.stopBtn.TabIndex = 4;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stop_Click);
            // 
            // searchBtn
            // 
            this.searchBtn.Location = new System.Drawing.Point(402, 19);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(56, 23);
            this.searchBtn.TabIndex = 3;
            this.searchBtn.Text = "Search";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // cnproxyCheck
            // 
            this.cnproxyCheck.AutoSize = true;
            this.cnproxyCheck.Checked = true;
            this.cnproxyCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cnproxyCheck.Location = new System.Drawing.Point(281, 23);
            this.cnproxyCheck.Name = "cnproxyCheck";
            this.cnproxyCheck.Size = new System.Drawing.Size(114, 16);
            this.cnproxyCheck.TabIndex = 2;
            this.cnproxyCheck.Text = "www.cnproxy.com";
            this.cnproxyCheck.UseVisualStyleBackColor = true;
            // 
            // minidailiCheck
            // 
            this.minidailiCheck.AutoSize = true;
            this.minidailiCheck.Location = new System.Drawing.Point(149, 23);
            this.minidailiCheck.Name = "minidailiCheck";
            this.minidailiCheck.Size = new System.Drawing.Size(126, 16);
            this.minidailiCheck.TabIndex = 1;
            this.minidailiCheck.Text = "www.minidaili.com";
            this.minidailiCheck.UseVisualStyleBackColor = true;
            // 
            // proxycnCheck
            // 
            this.proxycnCheck.AutoSize = true;
            this.proxycnCheck.Location = new System.Drawing.Point(16, 23);
            this.proxycnCheck.Name = "proxycnCheck";
            this.proxycnCheck.Size = new System.Drawing.Size(114, 16);
            this.proxycnCheck.TabIndex = 0;
            this.proxycnCheck.Text = "www.proxycn.com";
            this.proxycnCheck.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 276);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(533, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // proxyGridView
            // 
            this.proxyGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.proxyGridView.Location = new System.Drawing.Point(0, 2);
            this.proxyGridView.Name = "proxyGridView";
            this.proxyGridView.RowTemplate.Height = 23;
            this.proxyGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.proxyGridView.Size = new System.Drawing.Size(530, 271);
            this.proxyGridView.TabIndex = 0;
            // 
            // ProxyIPsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 370);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ProxyIPsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fetch Proxy IPs";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.proxyGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.DataGridView proxyGridView;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.CheckBox cnproxyCheck;
        public System.Windows.Forms.CheckBox minidailiCheck;
        public System.Windows.Forms.CheckBox proxycnCheck;
        private System.Windows.Forms.Button searchBtn;
        public System.Windows.Forms.StatusStrip statusStrip1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button stopBtn;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}