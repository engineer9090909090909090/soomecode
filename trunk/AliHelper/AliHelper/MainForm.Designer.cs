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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.CfContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewPlanMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifyPlanMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeletePlanMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Explorer = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.ProductsStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NavigatorBar)).BeginInit();
            this.NavigatorBar.SuspendLayout();
            this.ProductBand.SuspendLayout();
            this.InquiryBand.SuspendLayout();
            this.UpdateBand.SuspendLayout();
            this.ClientBand.SuspendLayout();
            this.OrderBand.SuspendLayout();
            this.CfContextMenuStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 496);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(823, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
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
            this.SettingMenuItem.Name = "SettingMenuItem";
            this.SettingMenuItem.Size = new System.Drawing.Size(43, 20);
            this.SettingMenuItem.Text = "设置";
            // 
            // ProductsStrip
            // 
            this.ProductsStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateGroup,
            this.updateAllProduct,
            this.updateAllImages,
            this.NewProductBtn});
            this.ProductsStrip.Location = new System.Drawing.Point(0, 24);
            this.ProductsStrip.Name = "ProductsStrip";
            this.ProductsStrip.Size = new System.Drawing.Size(823, 25);
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
            this.NavigatorBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NavigatorBar.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Black;
            this.NavigatorBar.Location = new System.Drawing.Point(3, 3);
            this.NavigatorBar.Name = "NavigatorBar";
            this.NavigatorBar.Size = new System.Drawing.Size(180, 441);
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
            this.ProductBand.ClientArea.Size = new System.Drawing.Size(178, 278);
            this.ProductBand.ClientArea.TabIndex = 0;
            this.ProductBand.LargeImage = global::AliHelper.Properties.Resources.klipper;
            this.ProductBand.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Black;
            this.ProductBand.Location = new System.Drawing.Point(1, 27);
            this.ProductBand.Name = "ProductBand";
            this.ProductBand.Size = new System.Drawing.Size(178, 278);
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
            this.InquiryBand.ClientArea.Size = new System.Drawing.Size(178, 278);
            this.InquiryBand.ClientArea.TabIndex = 0;
            this.InquiryBand.LargeImage = global::AliHelper.Properties.Resources.s_bookmark;
            this.InquiryBand.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Black;
            this.InquiryBand.Location = new System.Drawing.Point(1, 27);
            this.InquiryBand.Name = "InquiryBand";
            this.InquiryBand.Size = new System.Drawing.Size(178, 278);
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
            this.UpdateBand.ClientArea.Size = new System.Drawing.Size(178, 278);
            this.UpdateBand.ClientArea.TabIndex = 0;
            this.UpdateBand.LargeImage = global::AliHelper.Properties.Resources.history;
            this.UpdateBand.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Black;
            this.UpdateBand.Location = new System.Drawing.Point(1, 27);
            this.UpdateBand.Name = "UpdateBand";
            this.UpdateBand.Size = new System.Drawing.Size(178, 278);
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
            this.ClientBand.ClientArea.Size = new System.Drawing.Size(178, 278);
            this.ClientBand.ClientArea.TabIndex = 0;
            this.ClientBand.LargeImage = global::AliHelper.Properties.Resources.s_colorize;
            this.ClientBand.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Black;
            this.ClientBand.Location = new System.Drawing.Point(1, 27);
            this.ClientBand.Name = "ClientBand";
            this.ClientBand.Size = new System.Drawing.Size(178, 278);
            this.ClientBand.SmallImage = global::AliHelper.Properties.Resources.s_colorize;
            this.ClientBand.TabIndex = 3;
            this.ClientBand.Text = "客户维护";
            // 
            // OrderBand
            // 
            this.OrderBand.AllowDrop = true;
            // 
            // OrderBand.ClientArea
            // 
            this.OrderBand.ClientArea.Location = new System.Drawing.Point(0, 0);
            this.OrderBand.ClientArea.Name = "ClientArea";
            this.OrderBand.ClientArea.Size = new System.Drawing.Size(178, 278);
            this.OrderBand.ClientArea.TabIndex = 0;
            this.OrderBand.LargeImage = global::AliHelper.Properties.Resources.wizard;
            this.OrderBand.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Black;
            this.OrderBand.Location = new System.Drawing.Point(1, 27);
            this.OrderBand.Name = "OrderBand";
            this.OrderBand.Size = new System.Drawing.Size(178, 278);
            this.OrderBand.SmallImage = global::AliHelper.Properties.Resources.s_wizard;
            this.OrderBand.TabIndex = 4;
            this.OrderBand.Text = "订单跟进";
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
            this.Explorer.Location = new System.Drawing.Point(189, 3);
            this.Explorer.Name = "Explorer";
            this.Explorer.Size = new System.Drawing.Size(631, 441);
            this.Explorer.TabIndex = 7;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.Explorer, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.NavigatorBar, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 49);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(823, 447);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 518);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.ProductsStrip);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "阿里外贸助手";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
            this.CfContextMenuStrip.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
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
        private Guifreaks.NavigationBar.NaviBar NavigatorBar;
        private Guifreaks.NavigationBar.NaviBand ProductBand;
        private Guifreaks.NavigationBar.NaviBand UpdateBand;
        private Guifreaks.NavigationBar.NaviBand InquiryBand;
        private Guifreaks.NavigationBar.NaviBand ClientBand;
        private Guifreaks.NavigationBar.NaviBand OrderBand;
        private System.Windows.Forms.Panel Explorer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}