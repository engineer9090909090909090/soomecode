using AliHelper.Controls;
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
            this.DbsetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProductsStrip = new System.Windows.Forms.ToolStrip();
            this.UpdateAllProduct = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.UpdateAllImages = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.NewProductBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.RankProductBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.Top10QueryBtn = new System.Windows.Forms.ToolStripButton();
            this.NavigatorBar = new Guifreaks.NavigationBar.NaviBar(this.components);
            this.ProductBand = new Guifreaks.NavigationBar.NaviBand(this.components);
            this.ApTreeNaviGroup = new Guifreaks.NavigationBar.NaviGroup(this.components);
            this.ApListNaviGroup = new Guifreaks.NavigationBar.NaviGroup(this.components);
            this.ImagesButton = new AliHelper.Controls.ListButton();
            this.BatchAddButton = new AliHelper.Controls.ListButton();
            this.InquiryBand = new Guifreaks.NavigationBar.NaviBand(this.components);
            this.UpdateBand = new Guifreaks.NavigationBar.NaviBand(this.components);
            this.MyItemBand = new Guifreaks.NavigationBar.NaviBand(this.components);
            this.ItemTreeNaviGroup = new Guifreaks.NavigationBar.NaviGroup(this.components);
            this.ItemNaviGroup = new Guifreaks.NavigationBar.NaviGroup(this.components);
            this.PriceCateButton = new AliHelper.Controls.ListButton();
            this.SupplierListButton = new AliHelper.Controls.ListButton();
            this.ClientBand = new Guifreaks.NavigationBar.NaviBand(this.components);
            this.FinanceBand = new Guifreaks.NavigationBar.NaviBand(this.components);
            this.OrderBand = new Guifreaks.NavigationBar.NaviBand(this.components);
            this.CfContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewPlanMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifyPlanMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeletePlanMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Explorer = new System.Windows.Forms.Panel();
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.FinToolStrip = new System.Windows.Forms.ToolStrip();
            this.NewWater = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.NewOrderBtn = new System.Windows.Forms.ToolStripButton();
            this.MyItemStrip = new System.Windows.Forms.ToolStrip();
            this.NewSuplier = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.NewItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.AppNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.StatusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.ProductsStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NavigatorBar)).BeginInit();
            this.NavigatorBar.SuspendLayout();
            this.ProductBand.ClientArea.SuspendLayout();
            this.ProductBand.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ApTreeNaviGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ApListNaviGroup)).BeginInit();
            this.ApListNaviGroup.SuspendLayout();
            this.InquiryBand.SuspendLayout();
            this.UpdateBand.SuspendLayout();
            this.MyItemBand.ClientArea.SuspendLayout();
            this.MyItemBand.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTreeNaviGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemNaviGroup)).BeginInit();
            this.ItemNaviGroup.SuspendLayout();
            this.ClientBand.SuspendLayout();
            this.FinanceBand.SuspendLayout();
            this.OrderBand.SuspendLayout();
            this.CfContextMenuStrip.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.FinToolStrip.SuspendLayout();
            this.MyItemStrip.SuspendLayout();
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
            this.DataDicMenuItem,
            this.DbsetToolStripMenuItem});
            this.SettingMenuItem.Name = "SettingMenuItem";
            this.SettingMenuItem.Size = new System.Drawing.Size(43, 20);
            this.SettingMenuItem.Text = "设置";
            // 
            // DataDicMenuItem
            // 
            this.DataDicMenuItem.Image = global::AliHelper.Properties.Resources.dirctionary_b;
            this.DataDicMenuItem.Name = "DataDicMenuItem";
            this.DataDicMenuItem.Size = new System.Drawing.Size(148, 22);
            this.DataDicMenuItem.Text = "数据字典(&D)";
            this.DataDicMenuItem.Click += new System.EventHandler(this.DataDicMenuItem_Click);
            // 
            // DbsetToolStripMenuItem
            // 
            this.DbsetToolStripMenuItem.Image = global::AliHelper.Properties.Resources.setting_b;
            this.DbsetToolStripMenuItem.Name = "DbsetToolStripMenuItem";
            this.DbsetToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.DbsetToolStripMenuItem.Text = "数据库设置(&S)";
            this.DbsetToolStripMenuItem.Click += new System.EventHandler(this.DbsetToolStripMenuItem_Click);
            // 
            // ProductsStrip
            // 
            this.ProductsStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.ProductsStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UpdateAllProduct,
            this.toolStripSeparator4,
            this.UpdateAllImages,
            this.toolStripSeparator5,
            this.NewProductBtn,
            this.toolStripSeparator6,
            this.RankProductBtn,
            this.toolStripSeparator7,
            this.Top10QueryBtn});
            this.ProductsStrip.Location = new System.Drawing.Point(3, 50);
            this.ProductsStrip.Name = "ProductsStrip";
            this.ProductsStrip.Size = new System.Drawing.Size(496, 25);
            this.ProductsStrip.TabIndex = 2;
            this.ProductsStrip.Text = "v";
            // 
            // UpdateAllProduct
            // 
            this.UpdateAllProduct.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UpdateAllProduct.Image = global::AliHelper.Properties.Resources.reflash_b;
            this.UpdateAllProduct.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UpdateAllProduct.Name = "UpdateAllProduct";
            this.UpdateAllProduct.Size = new System.Drawing.Size(76, 22);
            this.UpdateAllProduct.Text = "同步产品";
            this.UpdateAllProduct.Click += new System.EventHandler(this.updateAllProduct_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // UpdateAllImages
            // 
            this.UpdateAllImages.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UpdateAllImages.Image = global::AliHelper.Properties.Resources.reflash1_b;
            this.UpdateAllImages.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UpdateAllImages.Name = "UpdateAllImages";
            this.UpdateAllImages.Size = new System.Drawing.Size(76, 22);
            this.UpdateAllImages.Text = "同步图片";
            this.UpdateAllImages.Click += new System.EventHandler(this.updateAllImages_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // NewProductBtn
            // 
            this.NewProductBtn.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NewProductBtn.Image = global::AliHelper.Properties.Resources.product1_b;
            this.NewProductBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewProductBtn.Name = "NewProductBtn";
            this.NewProductBtn.Size = new System.Drawing.Size(76, 22);
            this.NewProductBtn.Text = "新增产品";
            this.NewProductBtn.Click += new System.EventHandler(this.newProductBtn_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // RankProductBtn
            // 
            this.RankProductBtn.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RankProductBtn.Image = global::AliHelper.Properties.Resources.search;
            this.RankProductBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RankProductBtn.Name = "RankProductBtn";
            this.RankProductBtn.Size = new System.Drawing.Size(76, 22);
            this.RankProductBtn.Text = "排名查询";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // Top10QueryBtn
            // 
            this.Top10QueryBtn.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Top10QueryBtn.Image = ((System.Drawing.Image)(resources.GetObject("Top10QueryBtn.Image")));
            this.Top10QueryBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Top10QueryBtn.Name = "Top10QueryBtn";
            this.Top10QueryBtn.Size = new System.Drawing.Size(125, 22);
            this.Top10QueryBtn.Text = "关键词Top10分析";
            this.Top10QueryBtn.Click += new System.EventHandler(this.Top10QueryBtn_Click);
            // 
            // NavigatorBar
            // 
            this.NavigatorBar.ActiveBand = this.ProductBand;
            this.NavigatorBar.Controls.Add(this.ProductBand);
            this.NavigatorBar.Controls.Add(this.InquiryBand);
            this.NavigatorBar.Controls.Add(this.UpdateBand);
            this.NavigatorBar.Controls.Add(this.MyItemBand);
            this.NavigatorBar.Controls.Add(this.ClientBand);
            this.NavigatorBar.Controls.Add(this.FinanceBand);
            this.NavigatorBar.Controls.Add(this.OrderBand);
            this.NavigatorBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NavigatorBar.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Silver;
            this.NavigatorBar.Location = new System.Drawing.Point(3, 3);
            this.NavigatorBar.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
            this.NavigatorBar.Name = "NavigatorBar";
            this.NavigatorBar.Size = new System.Drawing.Size(180, 391);
            this.NavigatorBar.TabIndex = 6;
            this.NavigatorBar.Text = "naviBar1";
            this.NavigatorBar.VisibleLargeButtons = 2;
            this.NavigatorBar.ActiveBandChanged += new System.EventHandler(this.NavigatorBar_ActiveBandChanged);
            // 
            // ProductBand
            // 
            // 
            // ProductBand.ClientArea
            // 
            this.ProductBand.ClientArea.Controls.Add(this.ApTreeNaviGroup);
            this.ProductBand.ClientArea.Controls.Add(this.ApListNaviGroup);
            this.ProductBand.ClientArea.Location = new System.Drawing.Point(0, 0);
            this.ProductBand.ClientArea.Name = "ClientArea";
            this.ProductBand.ClientArea.Size = new System.Drawing.Size(178, 260);
            this.ProductBand.ClientArea.TabIndex = 0;
            this.ProductBand.LargeImage = global::AliHelper.Properties.Resources.product1_b;
            this.ProductBand.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Silver;
            this.ProductBand.Location = new System.Drawing.Point(1, 27);
            this.ProductBand.Name = "ProductBand";
            this.ProductBand.Size = new System.Drawing.Size(178, 260);
            this.ProductBand.SmallImage = global::AliHelper.Properties.Resources.product1_s;
            this.ProductBand.TabIndex = 0;
            this.ProductBand.Text = "国际站产品";
            // 
            // ApTreeNaviGroup
            // 
            this.ApTreeNaviGroup.Caption = null;
            this.ApTreeNaviGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ApTreeNaviGroup.HeaderContextMenuStrip = null;
            this.ApTreeNaviGroup.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Silver;
            this.ApTreeNaviGroup.Location = new System.Drawing.Point(0, 70);
            this.ApTreeNaviGroup.Name = "ApTreeNaviGroup";
            this.ApTreeNaviGroup.Padding = new System.Windows.Forms.Padding(1, 22, 1, 1);
            this.ApTreeNaviGroup.Size = new System.Drawing.Size(178, 190);
            this.ApTreeNaviGroup.TabIndex = 1;
            this.ApTreeNaviGroup.Text = "naviGroup2";
            // 
            // ApListNaviGroup
            // 
            this.ApListNaviGroup.Caption = null;
            this.ApListNaviGroup.Controls.Add(this.ImagesButton);
            this.ApListNaviGroup.Controls.Add(this.BatchAddButton);
            this.ApListNaviGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.ApListNaviGroup.HeaderContextMenuStrip = null;
            this.ApListNaviGroup.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Silver;
            this.ApListNaviGroup.Location = new System.Drawing.Point(0, 0);
            this.ApListNaviGroup.Name = "ApListNaviGroup";
            this.ApListNaviGroup.Padding = new System.Windows.Forms.Padding(1, 22, 1, 1);
            this.ApListNaviGroup.Size = new System.Drawing.Size(178, 70);
            this.ApListNaviGroup.TabIndex = 0;
            this.ApListNaviGroup.Text = "naviGroup1";
            // 
            // ImagesButton
            // 
            this.ImagesButton.BackColor = System.Drawing.Color.Transparent;
            this.ImagesButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.ImagesButton.FlatAppearance.BorderSize = 0;
            this.ImagesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImagesButton.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ImagesButton.Image = global::AliHelper.Properties.Resources.order_s;
            this.ImagesButton.Location = new System.Drawing.Point(1, 45);
            this.ImagesButton.Margin = new System.Windows.Forms.Padding(0);
            this.ImagesButton.Name = "ImagesButton";
            this.ImagesButton.Size = new System.Drawing.Size(176, 23);
            this.ImagesButton.TabIndex = 1;
            this.ImagesButton.Text = " 图片银行          ";
            this.ImagesButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ImagesButton.UseVisualStyleBackColor = false;
            this.ImagesButton.Click += new System.EventHandler(this.ImagesButton_Click);
            // 
            // BatchAddButton
            // 
            this.BatchAddButton.BackColor = System.Drawing.Color.Transparent;
            this.BatchAddButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.BatchAddButton.FlatAppearance.BorderSize = 0;
            this.BatchAddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BatchAddButton.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BatchAddButton.Image = global::AliHelper.Properties.Resources.product_s;
            this.BatchAddButton.Location = new System.Drawing.Point(1, 22);
            this.BatchAddButton.Margin = new System.Windows.Forms.Padding(0);
            this.BatchAddButton.Name = "BatchAddButton";
            this.BatchAddButton.Size = new System.Drawing.Size(176, 23);
            this.BatchAddButton.TabIndex = 0;
            this.BatchAddButton.Text = " 批量新增与修改";
            this.BatchAddButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BatchAddButton.UseVisualStyleBackColor = false;
            this.BatchAddButton.Click += new System.EventHandler(this.BatchAddButton_Click);
            // 
            // InquiryBand
            // 
            this.InquiryBand.AllowDrop = true;
            // 
            // InquiryBand.ClientArea
            // 
            this.InquiryBand.ClientArea.Location = new System.Drawing.Point(0, 0);
            this.InquiryBand.ClientArea.Name = "ClientArea";
            this.InquiryBand.ClientArea.Size = new System.Drawing.Size(178, 260);
            this.InquiryBand.ClientArea.TabIndex = 0;
            this.InquiryBand.LargeImage = global::AliHelper.Properties.Resources.mail_b;
            this.InquiryBand.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Silver;
            this.InquiryBand.Location = new System.Drawing.Point(1, 27);
            this.InquiryBand.Name = "InquiryBand";
            this.InquiryBand.Size = new System.Drawing.Size(178, 260);
            this.InquiryBand.SmallImage = global::AliHelper.Properties.Resources.mail_s;
            this.InquiryBand.TabIndex = 2;
            this.InquiryBand.Text = "国际站询盘";
            // 
            // UpdateBand
            // 
            this.UpdateBand.AllowDrop = true;
            // 
            // UpdateBand.ClientArea
            // 
            this.UpdateBand.ClientArea.Location = new System.Drawing.Point(0, 0);
            this.UpdateBand.ClientArea.Name = "ClientArea";
            this.UpdateBand.ClientArea.Size = new System.Drawing.Size(178, 260);
            this.UpdateBand.ClientArea.TabIndex = 0;
            this.UpdateBand.LargeImage = global::AliHelper.Properties.Resources.history;
            this.UpdateBand.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Silver;
            this.UpdateBand.Location = new System.Drawing.Point(1, 27);
            this.UpdateBand.Name = "UpdateBand";
            this.UpdateBand.Size = new System.Drawing.Size(178, 260);
            this.UpdateBand.SmallImage = global::AliHelper.Properties.Resources.s_history;
            this.UpdateBand.TabIndex = 1;
            this.UpdateBand.Text = "重发计划";
            // 
            // MyItemBand
            // 
            // 
            // MyItemBand.ClientArea
            // 
            this.MyItemBand.ClientArea.Controls.Add(this.ItemTreeNaviGroup);
            this.MyItemBand.ClientArea.Controls.Add(this.ItemNaviGroup);
            this.MyItemBand.ClientArea.Location = new System.Drawing.Point(0, 0);
            this.MyItemBand.ClientArea.Name = "ClientArea";
            this.MyItemBand.ClientArea.Size = new System.Drawing.Size(178, 260);
            this.MyItemBand.ClientArea.TabIndex = 0;
            this.MyItemBand.LargeImage = global::AliHelper.Properties.Resources.product_b;
            this.MyItemBand.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Silver;
            this.MyItemBand.Location = new System.Drawing.Point(1, 27);
            this.MyItemBand.Name = "MyItemBand";
            this.MyItemBand.Size = new System.Drawing.Size(178, 260);
            this.MyItemBand.SmallImage = global::AliHelper.Properties.Resources.product_s;
            this.MyItemBand.TabIndex = 3;
            this.MyItemBand.Text = "公司产品列表";
            // 
            // ItemTreeNaviGroup
            // 
            this.ItemTreeNaviGroup.Caption = null;
            this.ItemTreeNaviGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ItemTreeNaviGroup.Expanded = false;
            this.ItemTreeNaviGroup.HeaderContextMenuStrip = null;
            this.ItemTreeNaviGroup.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Silver;
            this.ItemTreeNaviGroup.Location = new System.Drawing.Point(0, 70);
            this.ItemTreeNaviGroup.Name = "ItemTreeNaviGroup";
            this.ItemTreeNaviGroup.Padding = new System.Windows.Forms.Padding(1, 22, 1, 1);
            this.ItemTreeNaviGroup.Size = new System.Drawing.Size(178, 190);
            this.ItemTreeNaviGroup.TabIndex = 1;
            this.ItemTreeNaviGroup.Text = "naviGroup2";
            // 
            // ItemNaviGroup
            // 
            this.ItemNaviGroup.Caption = null;
            this.ItemNaviGroup.Controls.Add(this.PriceCateButton);
            this.ItemNaviGroup.Controls.Add(this.SupplierListButton);
            this.ItemNaviGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.ItemNaviGroup.ExpandedHeight = 70;
            this.ItemNaviGroup.HeaderContextMenuStrip = null;
            this.ItemNaviGroup.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Silver;
            this.ItemNaviGroup.Location = new System.Drawing.Point(0, 0);
            this.ItemNaviGroup.Name = "ItemNaviGroup";
            this.ItemNaviGroup.Padding = new System.Windows.Forms.Padding(1, 22, 1, 1);
            this.ItemNaviGroup.Size = new System.Drawing.Size(178, 70);
            this.ItemNaviGroup.TabIndex = 0;
            this.ItemNaviGroup.Text = "naviGroup1";
            // 
            // PriceCateButton
            // 
            this.PriceCateButton.BackColor = System.Drawing.Color.White;
            this.PriceCateButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PriceCateButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.PriceCateButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.PriceCateButton.FlatAppearance.BorderSize = 0;
            this.PriceCateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PriceCateButton.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PriceCateButton.Image = global::AliHelper.Properties.Resources.s_dollars;
            this.PriceCateButton.Location = new System.Drawing.Point(1, 44);
            this.PriceCateButton.Margin = new System.Windows.Forms.Padding(0);
            this.PriceCateButton.Name = "PriceCateButton";
            this.PriceCateButton.Size = new System.Drawing.Size(176, 22);
            this.PriceCateButton.TabIndex = 1;
            this.PriceCateButton.Text = " 价格种类列表";
            this.PriceCateButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.PriceCateButton.UseMnemonic = false;
            this.PriceCateButton.UseVisualStyleBackColor = false;
            this.PriceCateButton.Click += new System.EventHandler(this.PriceCateButton_Click);
            // 
            // SupplierListButton
            // 
            this.SupplierListButton.BackColor = System.Drawing.Color.White;
            this.SupplierListButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.SupplierListButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.SupplierListButton.FlatAppearance.BorderSize = 0;
            this.SupplierListButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ActiveBorder;
            this.SupplierListButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.SupplierListButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SupplierListButton.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SupplierListButton.Image = global::AliHelper.Properties.Resources.supplier_s;
            this.SupplierListButton.Location = new System.Drawing.Point(1, 22);
            this.SupplierListButton.Margin = new System.Windows.Forms.Padding(0);
            this.SupplierListButton.Name = "SupplierListButton";
            this.SupplierListButton.Size = new System.Drawing.Size(176, 22);
            this.SupplierListButton.TabIndex = 0;
            this.SupplierListButton.Text = " 供应商列表    ";
            this.SupplierListButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SupplierListButton.UseMnemonic = false;
            this.SupplierListButton.UseVisualStyleBackColor = false;
            this.SupplierListButton.Click += new System.EventHandler(this.SupplierListButton_Click);
            // 
            // ClientBand
            // 
            this.ClientBand.AllowDrop = true;
            // 
            // ClientBand.ClientArea
            // 
            this.ClientBand.ClientArea.Location = new System.Drawing.Point(0, 0);
            this.ClientBand.ClientArea.Name = "ClientArea";
            this.ClientBand.ClientArea.Size = new System.Drawing.Size(178, 260);
            this.ClientBand.ClientArea.TabIndex = 0;
            this.ClientBand.LargeImage = global::AliHelper.Properties.Resources.customer_b;
            this.ClientBand.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Silver;
            this.ClientBand.Location = new System.Drawing.Point(1, 27);
            this.ClientBand.Name = "ClientBand";
            this.ClientBand.Size = new System.Drawing.Size(178, 260);
            this.ClientBand.SmallImage = global::AliHelper.Properties.Resources.customer_s;
            this.ClientBand.TabIndex = 4;
            this.ClientBand.Text = "客户管理";
            // 
            // FinanceBand
            // 
            // 
            // FinanceBand.ClientArea
            // 
            this.FinanceBand.ClientArea.Location = new System.Drawing.Point(0, 0);
            this.FinanceBand.ClientArea.Name = "ClientArea";
            this.FinanceBand.ClientArea.Size = new System.Drawing.Size(178, 260);
            this.FinanceBand.ClientArea.TabIndex = 0;
            this.FinanceBand.LargeImage = global::AliHelper.Properties.Resources.fan1_b;
            this.FinanceBand.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Silver;
            this.FinanceBand.Location = new System.Drawing.Point(1, 27);
            this.FinanceBand.Name = "FinanceBand";
            this.FinanceBand.Size = new System.Drawing.Size(178, 260);
            this.FinanceBand.SmallImage = global::AliHelper.Properties.Resources.fan1_s;
            this.FinanceBand.TabIndex = 6;
            this.FinanceBand.Text = "财务管理";
            // 
            // OrderBand
            // 
            this.OrderBand.AllowDrop = true;
            // 
            // OrderBand.ClientArea
            // 
            this.OrderBand.ClientArea.Location = new System.Drawing.Point(0, 0);
            this.OrderBand.ClientArea.Name = "ClientArea";
            this.OrderBand.ClientArea.Size = new System.Drawing.Size(178, 260);
            this.OrderBand.ClientArea.TabIndex = 0;
            this.OrderBand.LargeImage = global::AliHelper.Properties.Resources.order_b;
            this.OrderBand.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Silver;
            this.OrderBand.Location = new System.Drawing.Point(1, 27);
            this.OrderBand.Name = "OrderBand";
            this.OrderBand.Size = new System.Drawing.Size(178, 260);
            this.OrderBand.SmallImage = global::AliHelper.Properties.Resources.order_s;
            this.OrderBand.TabIndex = 5;
            this.OrderBand.Text = "订单管理";
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
            this.toolStripContainer.TopToolStripPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.FinToolStrip);
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.MyItemStrip);
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.ProductsStrip);
            this.toolStripContainer.TopToolStripPanel.RightToLeft = System.Windows.Forms.RightToLeft.No;
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
            this.FinToolStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.FinToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewWater,
            this.toolStripSeparator8,
            this.NewOrderBtn});
            this.FinToolStrip.Location = new System.Drawing.Point(3, 0);
            this.FinToolStrip.Name = "FinToolStrip";
            this.FinToolStrip.Size = new System.Drawing.Size(170, 25);
            this.FinToolStrip.TabIndex = 3;
            // 
            // NewWater
            // 
            this.NewWater.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NewWater.Image = global::AliHelper.Properties.Resources.fan_b;
            this.NewWater.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewWater.Name = "NewWater";
            this.NewWater.Size = new System.Drawing.Size(76, 22);
            this.NewWater.Text = "新增流水";
            this.NewWater.Click += new System.EventHandler(this.NewWater_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // NewOrderBtn
            // 
            this.NewOrderBtn.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewOrderBtn.Image = global::AliHelper.Properties.Resources.order_b;
            this.NewOrderBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewOrderBtn.Name = "NewOrderBtn";
            this.NewOrderBtn.Size = new System.Drawing.Size(76, 22);
            this.NewOrderBtn.Text = "新增订单";
            this.NewOrderBtn.Click += new System.EventHandler(this.NewOrderBtn_Click);
            // 
            // MyItemStrip
            // 
            this.MyItemStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.MyItemStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewSuplier,
            this.toolStripSeparator1,
            this.toolStripButton2,
            this.toolStripSeparator2,
            this.NewItem,
            this.toolStripSeparator3,
            this.toolStripButton1});
            this.MyItemStrip.Location = new System.Drawing.Point(3, 25);
            this.MyItemStrip.Name = "MyItemStrip";
            this.MyItemStrip.Size = new System.Drawing.Size(405, 25);
            this.MyItemStrip.TabIndex = 4;
            // 
            // NewSuplier
            // 
            this.NewSuplier.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewSuplier.Image = global::AliHelper.Properties.Resources.category_b;
            this.NewSuplier.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewSuplier.Name = "NewSuplier";
            this.NewSuplier.Size = new System.Drawing.Size(112, 22);
            this.NewSuplier.Text = "自定义类别管理";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::AliHelper.Properties.Resources.price_b;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(99, 22);
            this.toolStripButton2.Text = "价格种类管理";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // NewItem
            // 
            this.NewItem.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewItem.Image = global::AliHelper.Properties.Resources.product_b;
            this.NewItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewItem.Name = "NewItem";
            this.NewItem.Size = new System.Drawing.Size(76, 22);
            this.NewItem.Text = "新增产品";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = global::AliHelper.Properties.Resources.supplier_b;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(88, 22);
            this.toolStripButton1.Text = "新增供应商";
            // 
            // AppNotifyIcon
            // 
            this.AppNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("AppNotifyIcon.Icon")));
            this.AppNotifyIcon.Text = "外贸助手";
            this.AppNotifyIcon.Visible = true;
            this.AppNotifyIcon.DoubleClick += new System.EventHandler(this.AppNotifyIcon_DoubleClick);
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
            this.ProductBand.ClientArea.ResumeLayout(false);
            this.ProductBand.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ApTreeNaviGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ApListNaviGroup)).EndInit();
            this.ApListNaviGroup.ResumeLayout(false);
            this.InquiryBand.ResumeLayout(false);
            this.UpdateBand.ResumeLayout(false);
            this.MyItemBand.ClientArea.ResumeLayout(false);
            this.MyItemBand.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ItemTreeNaviGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemNaviGroup)).EndInit();
            this.ItemNaviGroup.ResumeLayout(false);
            this.ClientBand.ResumeLayout(false);
            this.FinanceBand.ResumeLayout(false);
            this.OrderBand.ResumeLayout(false);
            this.CfContextMenuStrip.ResumeLayout(false);
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.tableLayoutPanel.ResumeLayout(false);
            this.FinToolStrip.ResumeLayout(false);
            this.FinToolStrip.PerformLayout();
            this.MyItemStrip.ResumeLayout(false);
            this.MyItemStrip.PerformLayout();
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
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripButton UpdateAllProduct;
        private System.Windows.Forms.ToolStripButton NewProductBtn;
        private System.Windows.Forms.ToolStripButton UpdateAllImages;
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
        private System.Windows.Forms.ToolStripButton NewWater;
        private System.Windows.Forms.ToolStrip MyItemStrip;
        private System.Windows.Forms.ToolStripButton NewSuplier;
        private System.Windows.Forms.ToolStripButton NewOrderBtn;
        private System.Windows.Forms.ToolStripButton NewItem;
        private System.Windows.Forms.ToolStripMenuItem DbsetToolStripMenuItem;
        private Guifreaks.NavigationBar.NaviBand MyItemBand;
        private Guifreaks.NavigationBar.NaviGroup ItemTreeNaviGroup;
        private Guifreaks.NavigationBar.NaviGroup ItemNaviGroup;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private ListButton SupplierListButton;
        private ListButton PriceCateButton;
        private System.Windows.Forms.ToolStripButton RankProductBtn;
        private System.Windows.Forms.ToolStripButton Top10QueryBtn;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private Guifreaks.NavigationBar.NaviGroup ApTreeNaviGroup;
        private Guifreaks.NavigationBar.NaviGroup ApListNaviGroup;
        private ListButton BatchAddButton;
        private ListButton ImagesButton;
    }
}