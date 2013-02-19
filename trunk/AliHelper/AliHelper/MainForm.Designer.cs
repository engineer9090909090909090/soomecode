namespace AliHelper
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataDicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProductsStrip = new System.Windows.Forms.ToolStrip();
            this.updateGroup = new System.Windows.Forms.ToolStripButton();
            this.updateAllProduct = new System.Windows.Forms.ToolStripButton();
            this.updateAllImages = new System.Windows.Forms.ToolStripButton();
            this.NewProductBtn = new System.Windows.Forms.ToolStripButton();
            this.NavigatorBar = new Guifreaks.NavigationBar.NaviBar(this.components);
            this.ProductBand = new Guifreaks.NavigationBar.NaviBand(this.components);
            this.InquiryBand = new Guifreaks.NavigationBar.NaviBand(this.components);
            this.UpdateBand = new Guifreaks.NavigationBar.NaviBand(this.components);
            this.ClientBand = new Guifreaks.NavigationBar.NaviBand(this.components);
            this.OrderBand = new Guifreaks.NavigationBar.NaviBand(this.components);
            this.FinanceBand = new Guifreaks.NavigationBar.NaviBand(this.components);
            this.CfContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewPlanMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifyPlanMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeletePlanMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Explorer = new System.Windows.Forms.Panel();
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.FinToolStrip = new System.Windows.Forms.ToolStrip();
            this.NewFinance = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.NewWater = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.AppNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.NewOrder = new System.Windows.Forms.ToolStripButton();
            this.NewOrderBtn = new System.Windows.Forms.ToolStripButton();
            this.StatusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.ProductsStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NavigatorBar)).BeginInit();
            this.NavigatorBar.SuspendLayout();
            this.ProductBand.SuspendLayout();
            this.InquiryBand.SuspendLayout();
            this.UpdateBand.SuspendLayout();
            this.ClientBand.SuspendLayout();
            this.OrderBand.SuspendLayout();
            this.FinanceBand.SuspendLayout();
            this.CfContextMenuStrip.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.FinToolStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.StatusStrip.Location = new System.Drawing.Point(0, 496);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(823, 22);
            this.StatusStrip.TabIndex = 0;
            this.StatusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(43, 17);
            this.statusLabel.Text = "状态栏";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuItem,
            this.SettingMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(823, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MainMenuItem
            // 
            this.MainMenuItem.Name = "MainMenuItem";
            this.MainMenuItem.Size = new System.Drawing.Size(55, 20);
            this.MainMenuItem.Text = "主菜单";
            // 
            // SettingMenuItem
            // 
            this.SettingMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DataDicMenuItem});
            this.SettingMenuItem.Name = "SettingMenuItem";
            this.SettingMenuItem.Size = new System.Drawing.Size(43, 20);
            this.SettingMenuItem.Text = "设置";
            // 
            // DataDicMenuItem
            // 
            this.DataDicMenuItem.Name = "DataDicMenuItem";
            this.DataDicMenuItem.Size = new System.Drawing.Size(137, 22);
            this.DataDicMenuItem.Text = "数据字典(&D)";
            this.DataDicMenuItem.Click += new System.EventHandler(this.DataDicMenuItem_Click);
            // 
            // ProductsStrip
            // 
            this.ProductsStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.ProductsStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateGroup,
            this.updateAllProduct,
            this.updateAllImages,
            this.NewProductBtn});
            this.ProductsStrip.Location = new System.Drawing.Point(3, 25);
            this.ProductsStrip.Name = "ProductsStrip";
            this.ProductsStrip.Size = new System.Drawing.Size(336, 25);
            this.ProductsStrip.TabIndex = 2;
            this.ProductsStrip.Text = "v";
            // 
            // updateGroup
            // 
            this.updateGroup.Image = ((System.Drawing.Image)(resources.GetObject("updateGroup.Image")));
            this.updateGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.updateGroup.Name = "updateGroup";
            this.updateGroup.Size = new System.Drawing.Size(75, 22);
            this.updateGroup.Text = "同步分组";
            this.updateGroup.Click += new System.EventHandler(this.updateGroup_Click);
            // 
            // updateAllProduct
            // 
            this.updateAllProduct.Image = ((System.Drawing.Image)(resources.GetObject("updateAllProduct.Image")));
            this.updateAllProduct.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.updateAllProduct.Name = "updateAllProduct";
            this.updateAllProduct.Size = new System.Drawing.Size(99, 22);
            this.updateAllProduct.Text = "同步所有产品";
            this.updateAllProduct.Click += new System.EventHandler(this.updateAllProduct_Click);
            // 
            // updateAllImages
            // 
            this.updateAllImages.Image = ((System.Drawing.Image)(resources.GetObject("updateAllImages.Image")));
            this.updateAllImages.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.updateAllImages.Name = "updateAllImages";
            this.updateAllImages.Size = new System.Drawing.Size(75, 22);
            this.updateAllImages.Text = "同步图片";
            this.updateAllImages.Click += new System.EventHandler(this.updateAllImages_Click);
            // 
            // NewProductBtn
            // 
            this.NewProductBtn.Image = ((System.Drawing.Image)(resources.GetObject("NewProductBtn.Image")));
            this.NewProductBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewProductBtn.Name = "NewProductBtn";
            this.NewProductBtn.Size = new System.Drawing.Size(75, 22);
            this.NewProductBtn.Text = "新增产品";
            this.NewProductBtn.Click += new System.EventHandler(this.newProductBtn_Click);
            // 
            // NavigatorBar
            // 
            this.NavigatorBar.ActiveBand = this.ProductBand;
            this.NavigatorBar.Controls.Add(this.ProductBand);
            this.NavigatorBar.Controls.Add(this.InquiryBand);
            this.NavigatorBar.Controls.Add(this.UpdateBand);
            this.NavigatorBar.Controls.Add(this.ClientBand);
            this.NavigatorBar.Controls.Add(this.OrderBand);
            this.NavigatorBar.Controls.Add(this.FinanceBand);
            this.NavigatorBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NavigatorBar.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Black;
            this.NavigatorBar.Location = new System.Drawing.Point(3, 3);
            this.NavigatorBar.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
            this.NavigatorBar.Name = "NavigatorBar";
            this.NavigatorBar.Size = new System.Drawing.Size(180, 391);
            this.NavigatorBar.TabIndex = 6;
            this.NavigatorBar.Text = "naviBar1";
            this.NavigatorBar.VisibleLargeButtons = 3;
            this.NavigatorBar.ActiveBandChanged += new System.EventHandler(this.NavigatorBar_ActiveBandChanged);
            // 
            // ProductBand
            // 
            // 
            // ProductBand.ClientArea
            // 
            this.ProductBand.ClientArea.Location = new System.Drawing.Point(0, 0);
            this.ProductBand.ClientArea.Name = "ClientArea";
            this.ProductBand.ClientArea.Size = new System.Drawing.Size(178, 228);
            this.ProductBand.ClientArea.TabIndex = 0;
            this.ProductBand.LargeImage = global::AliHelper.Properties.Resources.klipper;
            this.ProductBand.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Black;
            this.ProductBand.Location = new System.Drawing.Point(1, 27);
            this.ProductBand.Name = "ProductBand";
            this.ProductBand.Size = new System.Drawing.Size(178, 228);
            this.ProductBand.SmallImage = global::AliHelper.Properties.Resources.s_klipper_dock;
            this.ProductBand.TabIndex = 0;
            this.ProductBand.Text = "产品列表";
            // 
            // InquiryBand
            // 
            this.InquiryBand.AllowDrop = true;
            // 
            // InquiryBand.ClientArea
            // 
            this.InquiryBand.ClientArea.Location = new System.Drawing.Point(0, 0);
            this.InquiryBand.ClientArea.Name = "ClientArea";
            this.InquiryBand.ClientArea.Size = new System.Drawing.Size(178, 228);
            this.InquiryBand.ClientArea.TabIndex = 0;
            this.InquiryBand.LargeImage = global::AliHelper.Properties.Resources.s_bookmark;
            this.InquiryBand.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Black;
            this.InquiryBand.Location = new System.Drawing.Point(1, 27);
            this.InquiryBand.Name = "InquiryBand";
            this.InquiryBand.Size = new System.Drawing.Size(178, 228);
            this.InquiryBand.SmallImage = global::AliHelper.Properties.Resources.s_bookmark;
            this.InquiryBand.TabIndex = 2;
            this.InquiryBand.Text = "阿里外贸邮";
            // 
            // UpdateBand
            // 
            this.UpdateBand.AllowDrop = true;
            // 
            // UpdateBand.ClientArea
            // 
            this.UpdateBand.ClientArea.Location = new System.Drawing.Point(0, 0);
            this.UpdateBand.ClientArea.Name = "ClientArea";
            this.UpdateBand.ClientArea.Size = new System.Drawing.Size(178, 228);
            this.UpdateBand.ClientArea.TabIndex = 0;
            this.UpdateBand.LargeImage = global::AliHelper.Properties.Resources.history;
            this.UpdateBand.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Black;
            this.UpdateBand.Location = new System.Drawing.Point(1, 27);
            this.UpdateBand.Name = "UpdateBand";
            this.UpdateBand.Size = new System.Drawing.Size(178, 228);
            this.UpdateBand.SmallImage = global::AliHelper.Properties.Resources.s_history;
            this.UpdateBand.TabIndex = 1;
            this.UpdateBand.Text = "重发计划";
            // 
            // ClientBand
            // 
            this.ClientBand.AllowDrop = true;
            // 
            // ClientBand.ClientArea
            // 
            this.ClientBand.ClientArea.Location = new System.Drawing.Point(0, 0);
            this.ClientBand.ClientArea.Name = "ClientArea";
            this.ClientBand.ClientArea.Size = new System.Drawing.Size(178, 228);
            this.ClientBand.ClientArea.TabIndex = 0;
            this.ClientBand.LargeImage = global::AliHelper.Properties.Resources.s_colorize;
            this.ClientBand.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Black;
            this.ClientBand.Location = new System.Drawing.Point(1, 27);
            this.ClientBand.Name = "ClientBand";
            this.ClientBand.Size = new System.Drawing.Size(178, 228);
            this.ClientBand.SmallImage = global::AliHelper.Properties.Resources.s_colorize;
            this.ClientBand.TabIndex = 3;
            this.ClientBand.Text = "客户管理";
            // 
            // OrderBand
            // 
            this.OrderBand.AllowDrop = true;
            // 
            // OrderBand.ClientArea
            // 
            this.OrderBand.ClientArea.Location = new System.Drawing.Point(0, 0);
            this.OrderBand.ClientArea.Name = "ClientArea";
            this.OrderBand.ClientArea.Size = new System.Drawing.Size(178, 228);
            this.OrderBand.ClientArea.TabIndex = 0;
            this.OrderBand.LargeImage = global::AliHelper.Properties.Resources.wizard;
            this.OrderBand.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Black;
            this.OrderBand.Location = new System.Drawing.Point(1, 27);
            this.OrderBand.Name = "OrderBand";
            this.OrderBand.Size = new System.Drawing.Size(178, 228);
            this.OrderBand.SmallImage = global::AliHelper.Properties.Resources.s_wizard;
            this.OrderBand.TabIndex = 4;
            this.OrderBand.Text = "订单管理";
            // 
            // FinanceBand
            // 
            // 
            // FinanceBand.ClientArea
            // 
            this.FinanceBand.ClientArea.Location = new System.Drawing.Point(0, 0);
            this.FinanceBand.ClientArea.Name = "ClientArea";
            this.FinanceBand.ClientArea.Size = new System.Drawing.Size(178, 228);
            this.FinanceBand.ClientArea.TabIndex = 0;
            this.FinanceBand.LargeImage = global::AliHelper.Properties.Resources.Dollars;
            this.FinanceBand.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Black;
            this.FinanceBand.Location = new System.Drawing.Point(1, 27);
            this.FinanceBand.Name = "FinanceBand";
            this.FinanceBand.Size = new System.Drawing.Size(178, 228);
            this.FinanceBand.SmallImage = global::AliHelper.Properties.Resources.s_dollars;
            this.FinanceBand.TabIndex = 10;
            this.FinanceBand.Text = "财务管理";
            // 
            // CfContextMenuStrip
            // 
            this.CfContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewPlanMenuItem,
            this.ModifyPlanMenuItem,
            this.DeletePlanMenuItem});
            this.CfContextMenuStrip.Name = "CfContextMenuStrip";
            this.CfContextMenuStrip.Size = new System.Drawing.Size(147, 70);
            // 
            // NewPlanMenuItem
            // 
            this.NewPlanMenuItem.Name = "NewPlanMenuItem";
            this.NewPlanMenuItem.Size = new System.Drawing.Size(146, 22);
            this.NewPlanMenuItem.Text = "新建重发计划";
            this.NewPlanMenuItem.Click += new System.EventHandler(this.NewPlanMenuItem_Click);
            // 
            // ModifyPlanMenuItem
            // 
            this.ModifyPlanMenuItem.Name = "ModifyPlanMenuItem";
            this.ModifyPlanMenuItem.Size = new System.Drawing.Size(146, 22);
            this.ModifyPlanMenuItem.Text = "编辑重发计划";
            this.ModifyPlanMenuItem.Click += new System.EventHandler(this.ModifyPlanMenuItem_Click);
            // 
            // DeletePlanMenuItem
            // 
            this.DeletePlanMenuItem.Name = "DeletePlanMenuItem";
            this.DeletePlanMenuItem.Size = new System.Drawing.Size(146, 22);
            this.DeletePlanMenuItem.Text = "删除重发计划";
            // 
            // Explorer
            // 
            this.Explorer.BackColor = System.Drawing.SystemColors.Control;
            this.Explorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Explorer.Location = new System.Drawing.Point(187, 3);
            this.Explorer.Name = "Explorer";
            this.Explorer.Size = new System.Drawing.Size(633, 391);
            this.Explorer.TabIndex = 7;
            // 
            // toolStripContainer
            // 
            this.toolStripContainer.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.tableLayoutPanel);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(823, 397);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.LeftToolStripPanelVisible = false;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 24);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.RightToolStripPanelVisible = false;
            this.toolStripContainer.Size = new System.Drawing.Size(823, 472);
            this.toolStripContainer.TabIndex = 7;
            this.toolStripContainer.Text = "toolStripContainer";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.FinToolStrip);
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.ProductsStrip);
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.Explorer, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.NavigatorBar, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(823, 397);
            this.tableLayoutPanel.TabIndex = 5;
            // 
            // FinToolStrip
            // 
            this.FinToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.FinToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewFinance,
            this.toolStripSeparator1,
            this.NewWater,
            this.NewOrderBtn});
            this.FinToolStrip.Location = new System.Drawing.Point(3, 0);
            this.FinToolStrip.Name = "FinToolStrip";
            this.FinToolStrip.Size = new System.Drawing.Size(274, 25);
            this.FinToolStrip.TabIndex = 3;
            // 
            // NewFinance
            // 
            this.NewFinance.Image = ((System.Drawing.Image)(resources.GetObject("NewFinance.Image")));
            this.NewFinance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewFinance.Name = "NewFinance";
            this.NewFinance.Size = new System.Drawing.Size(75, 22);
            this.NewFinance.Text = "新增账目";
            this.NewFinance.Click += new System.EventHandler(this.NewFinance_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // NewWater
            // 
            this.NewWater.Image = ((System.Drawing.Image)(resources.GetObject("NewWater.Image")));
            this.NewWater.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewWater.Name = "NewWater";
            this.NewWater.Size = new System.Drawing.Size(75, 22);
            this.NewWater.Text = "新增流水";
            this.NewWater.Click += new System.EventHandler(this.NewWater_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewOrder,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(3, 50);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(162, 25);
            this.toolStrip1.TabIndex = 4;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(75, 22);
            this.toolStripButton1.Text = "订单分类";
            // 
            // AppNotifyIcon
            // 
            this.AppNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("AppNotifyIcon.Icon")));
            this.AppNotifyIcon.Text = "notifyIcon1";
            this.AppNotifyIcon.Visible = true;
            this.AppNotifyIcon.DoubleClick += new System.EventHandler(this.AppNotifyIcon_DoubleClick);
            // 
            // NewOrder
            // 
            this.NewOrder.Image = ((System.Drawing.Image)(resources.GetObject("NewOrder.Image")));
            this.NewOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewOrder.Name = "NewOrder";
            this.NewOrder.Size = new System.Drawing.Size(75, 22);
            this.NewOrder.Text = "新增订单";
            // 
            // NewOrderBtn
            // 
            this.NewOrderBtn.Image = ((System.Drawing.Image)(resources.GetObject("NewOrderBtn.Image")));
            this.NewOrderBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewOrderBtn.Name = "NewOrderBtn";
            this.NewOrderBtn.Size = new System.Drawing.Size(75, 22);
            this.NewOrderBtn.Text = "新增订单";
            this.NewOrderBtn.Click += new System.EventHandler(this.NewOrderBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 518);
            this.Controls.Add(this.toolStripContainer);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "阿里外贸助手";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ProductsStrip.ResumeLayout(false);
            this.ProductsStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NavigatorBar)).EndInit();
            this.NavigatorBar.ResumeLayout(false);
            this.ProductBand.ResumeLayout(false);
            this.InquiryBand.ResumeLayout(false);
            this.UpdateBand.ResumeLayout(false);
            this.ClientBand.ResumeLayout(false);
            this.OrderBand.ResumeLayout(false);
            this.FinanceBand.ResumeLayout(false);
            this.CfContextMenuStrip.ResumeLayout(false);
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.tableLayoutPanel.ResumeLayout(false);
            this.FinToolStrip.ResumeLayout(false);
            this.FinToolStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingMenuItem;
        private System.Windows.Forms.ToolStrip ProductsStrip;
        private System.Windows.Forms.ToolStripButton updateGroup;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripButton updateAllProduct;
        private System.Windows.Forms.ToolStripButton NewProductBtn;
        private System.Windows.Forms.ToolStripButton updateAllImages;
        private System.Windows.Forms.ContextMenuStrip CfContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem NewPlanMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ModifyPlanMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeletePlanMenuItem;
        public Guifreaks.NavigationBar.NaviBar NavigatorBar;
        private Guifreaks.NavigationBar.NaviBand ProductBand;
        private Guifreaks.NavigationBar.NaviBand UpdateBand;
        private Guifreaks.NavigationBar.NaviBand InquiryBand;
        private Guifreaks.NavigationBar.NaviBand ClientBand;
        private Guifreaks.NavigationBar.NaviBand OrderBand;
        public System.Windows.Forms.Panel Explorer;
        private Guifreaks.NavigationBar.NaviBand FinanceBand;
        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.NotifyIcon AppNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem DataDicMenuItem;
        private System.Windows.Forms.ToolStrip FinToolStrip;
        private System.Windows.Forms.ToolStripButton NewFinance;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton NewWater;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton NewOrderBtn;
        private System.Windows.Forms.ToolStripButton NewOrder;
    }
}