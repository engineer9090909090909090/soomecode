using UtilityLibrary.WinControls;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.主菜单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProductsStrip = new System.Windows.Forms.ToolStrip();
            this.updateGroup = new System.Windows.Forms.ToolStripButton();
            this.updateAllProduct = new System.Windows.Forms.ToolStripButton();
            this.updateAllImages = new System.Windows.Forms.ToolStripButton();
            this.newProductBtn = new System.Windows.Forms.ToolStripButton();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.outlookBar1 = new UtilityLibrary.WinControls.OutlookBar();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.ProductsStrip.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.主菜单ToolStripMenuItem,
            this.设置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(823, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 主菜单ToolStripMenuItem
            // 
            this.主菜单ToolStripMenuItem.Name = "主菜单ToolStripMenuItem";
            this.主菜单ToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.主菜单ToolStripMenuItem.Text = "主菜单";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // ProductsStrip
            // 
            this.ProductsStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateGroup,
            this.updateAllProduct,
            this.updateAllImages,
            this.newProductBtn});
            this.ProductsStrip.Location = new System.Drawing.Point(0, 24);
            this.ProductsStrip.Name = "ProductsStrip";
            this.ProductsStrip.Size = new System.Drawing.Size(823, 25);
            this.ProductsStrip.TabIndex = 2;
            this.ProductsStrip.Text = "ProductsStrip";
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
            // newProductBtn
            // 
            this.newProductBtn.Image = ((System.Drawing.Image)(resources.GetObject("newProductBtn.Image")));
            this.newProductBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newProductBtn.Name = "newProductBtn";
            this.newProductBtn.Size = new System.Drawing.Size(75, 22);
            this.newProductBtn.Text = "新增产品";
            this.newProductBtn.Click += new System.EventHandler(this.newProductBtn_Click);
            // 
            // treeView1
            // 
            this.treeView1.LineColor = System.Drawing.Color.Empty;
            this.treeView1.Location = new System.Drawing.Point(0, 23);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(130, 423);
            this.treeView1.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.outlookBar1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.splitContainer1.Size = new System.Drawing.Size(823, 447);
            this.splitContainer1.SplitterDistance = 130;
            this.splitContainer1.TabIndex = 5;
            // 
            // outlookBar1
            // 
            this.outlookBar1.AnimationSpeed = 20;
            this.outlookBar1.BackColor = System.Drawing.SystemColors.Control;
            this.outlookBar1.BackgroundBitmap = null;
            this.outlookBar1.BorderType = UtilityLibrary.WinControls.BorderType.None;
            this.outlookBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outlookBar1.FlatArrowButtons = false;
            this.outlookBar1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outlookBar1.LeftTopColor = System.Drawing.Color.Empty;
            this.outlookBar1.Location = new System.Drawing.Point(0, 0);
            this.outlookBar1.Name = "outlookBar1";
            this.outlookBar1.RightBottomColor = System.Drawing.Color.Empty;
            this.outlookBar1.Size = new System.Drawing.Size(130, 447);
            this.outlookBar1.TabIndex = 5;
            this.outlookBar1.Text = "outlookBar1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 518);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ProductsStrip);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
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
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 主菜单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip ProductsStrip;
        private System.Windows.Forms.ToolStripButton updateGroup;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripButton updateAllProduct;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton newProductBtn;
        private System.Windows.Forms.ToolStripButton updateAllImages;
        private UtilityLibrary.WinControls.OutlookBar outlookBar1;
    }
}