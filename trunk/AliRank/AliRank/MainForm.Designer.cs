namespace AliRank
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.KwToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VPNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shutdownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CleanKeyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.clickRunBtn = new System.Windows.Forms.ToolStripButton();
            this.clickStopBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.RunTime = new System.Windows.Forms.ToolStripLabel();
            this.MessageLabel = new System.Windows.Forms.ToolStripLabel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.dataTab = new System.Windows.Forms.TabPage();
            this.IETab = new System.Windows.Forms.TabPage();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl.SuspendLayout();
            this.dataTab.SuspendLayout();
            this.IETab.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 544);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(792, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(55, 17);
            this.toolStripStatusLabel1.Text = "运行状态";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.KwToolStripMenuItem,
            this.SetToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(792, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // KwToolStripMenuItem
            // 
            this.KwToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImpToolStripMenuItem,
            this.AsToolStripMenuItem,
            this.MtToolStripMenuItem});
            this.KwToolStripMenuItem.Name = "KwToolStripMenuItem";
            this.KwToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.KwToolStripMenuItem.Text = "关键词(&K)";
            // 
            // ImpToolStripMenuItem
            // 
            this.ImpToolStripMenuItem.Name = "ImpToolStripMenuItem";
            this.ImpToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.ImpToolStripMenuItem.Text = "导入橱窗产品(&I)";
            this.ImpToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem1_Click);
            // 
            // AsToolStripMenuItem
            // 
            this.AsToolStripMenuItem.Name = "AsToolStripMenuItem";
            this.AsToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.AsToolStripMenuItem.Text = "关键词排名查询(&Q)";
            this.AsToolStripMenuItem.Click += new System.EventHandler(this.AsToolStripMenuItem_Click);
            // 
            // MtToolStripMenuItem
            // 
            this.MtToolStripMenuItem.Name = "MtToolStripMenuItem";
            this.MtToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.MtToolStripMenuItem.Text = "关键词前5名分析(&A)";
            this.MtToolStripMenuItem.Click += new System.EventHandler(this.MtToolStripMenuItem_Click);
            // 
            // SetToolStripMenuItem
            // 
            this.SetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClickToolStripMenuItem,
            this.VPNToolStripMenuItem,
            this.shutdownToolStripMenuItem,
            this.CleanKeyMenuItem});
            this.SetToolStripMenuItem.Name = "SetToolStripMenuItem";
            this.SetToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.SetToolStripMenuItem.Text = "设置(E)";
            // 
            // ClickToolStripMenuItem
            // 
            this.ClickToolStripMenuItem.Name = "ClickToolStripMenuItem";
            this.ClickToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.ClickToolStripMenuItem.Text = "点击设置(&C)";
            this.ClickToolStripMenuItem.Click += new System.EventHandler(this.ClickToolStripMenuItem_Click);
            // 
            // VPNToolStripMenuItem
            // 
            this.VPNToolStripMenuItem.Name = "VPNToolStripMenuItem";
            this.VPNToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.VPNToolStripMenuItem.Text = "VPN数据管理(&V)";
            this.VPNToolStripMenuItem.Click += new System.EventHandler(this.VPNToolStripMenuItem_Click);
            // 
            // shutdownToolStripMenuItem
            // 
            this.shutdownToolStripMenuItem.Name = "shutdownToolStripMenuItem";
            this.shutdownToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.shutdownToolStripMenuItem.Text = "完成后关机(&F)";
            this.shutdownToolStripMenuItem.Click += new System.EventHandler(this.shutdownStripMenuItem_Click);
            // 
            // CleanKeyMenuItem
            // 
            this.CleanKeyMenuItem.Name = "CleanKeyMenuItem";
            this.CleanKeyMenuItem.Size = new System.Drawing.Size(155, 22);
            this.CleanKeyMenuItem.Text = "清空关键词(&C)";
            this.CleanKeyMenuItem.Click += new System.EventHandler(this.CleanKeyMenuItem_Click);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.HelpToolStripMenuItem.Text = "帮助(&H)";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clickRunBtn,
            this.clickStopBtn,
            this.toolStripSeparator1,
            this.toolStripButton4,
            this.toolStripSeparator3,
            this.toolStripLabel1,
            this.RunTime,
            this.MessageLabel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(792, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // clickRunBtn
            // 
            this.clickRunBtn.Image = global::AliRank.Properties.Resources.play;
            this.clickRunBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clickRunBtn.Name = "clickRunBtn";
            this.clickRunBtn.Size = new System.Drawing.Size(75, 22);
            this.clickRunBtn.Text = "开始点击";
            this.clickRunBtn.ToolTipText = "运行";
            this.clickRunBtn.Click += new System.EventHandler(this.clickRunBtn_Click);
            // 
            // clickStopBtn
            // 
            this.clickStopBtn.Enabled = false;
            this.clickStopBtn.Image = global::AliRank.Properties.Resources.stop;
            this.clickStopBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.clickStopBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clickStopBtn.Name = "clickStopBtn";
            this.clickStopBtn.RightToLeftAutoMirrorImage = true;
            this.clickStopBtn.Size = new System.Drawing.Size(75, 22);
            this.clickStopBtn.Text = "停止点击";
            this.clickStopBtn.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.clickStopBtn.ToolTipText = "停止";
            this.clickStopBtn.Click += new System.EventHandler(this.clickStopBtn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = global::AliRank.Properties.Resources.query;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(75, 22);
            this.toolStripButton4.Text = "排名查询";
            this.toolStripButton4.Click += new System.EventHandler(this.AsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(55, 22);
            this.toolStripLabel1.Text = "运行时间";
            // 
            // RunTime
            // 
            this.RunTime.Name = "RunTime";
            this.RunTime.Size = new System.Drawing.Size(63, 22);
            this.RunTime.Text = "00 : 00 : 00";
            // 
            // MessageLabel
            // 
            this.MessageLabel.ForeColor = System.Drawing.Color.Red;
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(0, 22);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(778, 464);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyUp);
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser.Location = new System.Drawing.Point(3, 3);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.Size = new System.Drawing.Size(778, 464);
            this.webBrowser.TabIndex = 5;
            this.webBrowser.WebBrowserShortcutsEnabled = false;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.dataTab);
            this.tabControl.Controls.Add(this.IETab);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 49);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(792, 495);
            this.tabControl.TabIndex = 6;
            // 
            // dataTab
            // 
            this.dataTab.Controls.Add(this.dataGridView1);
            this.dataTab.Location = new System.Drawing.Point(4, 21);
            this.dataTab.Name = "dataTab";
            this.dataTab.Padding = new System.Windows.Forms.Padding(3);
            this.dataTab.Size = new System.Drawing.Size(784, 470);
            this.dataTab.TabIndex = 0;
            this.dataTab.Text = "橱窗产品列表";
            this.dataTab.UseVisualStyleBackColor = true;
            // 
            // IETab
            // 
            this.IETab.Controls.Add(this.webBrowser);
            this.IETab.Location = new System.Drawing.Point(4, 21);
            this.IETab.Name = "IETab";
            this.IETab.Padding = new System.Windows.Forms.Padding(3);
            this.IETab.Size = new System.Drawing.Size(784, 470);
            this.IETab.TabIndex = 1;
            this.IETab.Text = "浏览器点击窗口";
            this.IETab.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AliRank";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.dataTab.ResumeLayout(false);
            this.IETab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem KwToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton clickRunBtn;
        private System.Windows.Forms.ToolStripButton clickStopBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem SetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem VPNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem ImpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MtToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel RunTime;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage dataTab;
        private System.Windows.Forms.TabPage IETab;
        private System.Windows.Forms.ToolStripMenuItem shutdownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CleanKeyMenuItem;
        private System.Windows.Forms.ToolStripLabel MessageLabel;
    }
}

