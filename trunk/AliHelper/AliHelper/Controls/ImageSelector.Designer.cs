namespace AliHelper
{
    partial class ImageSelector
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.FileSelectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScreenshotMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CutBroadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CleanMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileSelectMenuItem,
            this.ScreenshotMenuItem,
            this.CutBroadMenuItem,
            this.CleanMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 114);
            // 
            // FileSelectMenuItem
            // 
            this.FileSelectMenuItem.Image = global::AliHelper.Properties.Resources.open;
            this.FileSelectMenuItem.Name = "FileSelectMenuItem";
            this.FileSelectMenuItem.Size = new System.Drawing.Size(152, 22);
            this.FileSelectMenuItem.Text = "选择图片";
            this.FileSelectMenuItem.Click += new System.EventHandler(this.FileSelectMenuItem_Click);
            // 
            // ScreenshotMenuItem
            // 
            this.ScreenshotMenuItem.Image = global::AliHelper.Properties.Resources.Screenshot;
            this.ScreenshotMenuItem.Name = "ScreenshotMenuItem";
            this.ScreenshotMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ScreenshotMenuItem.Text = "截　图";
            this.ScreenshotMenuItem.Click += new System.EventHandler(this.ScreenshotMenuItem_Click);
            // 
            // CutBroadMenuItem
            // 
            this.CutBroadMenuItem.Image = global::AliHelper.Properties.Resources.paste;
            this.CutBroadMenuItem.Name = "CutBroadMenuItem";
            this.CutBroadMenuItem.Size = new System.Drawing.Size(152, 22);
            this.CutBroadMenuItem.Text = "剪贴板获取";
            this.CutBroadMenuItem.Click += new System.EventHandler(this.CutBroadMenuItem_Click);
            // 
            // CleanMenuItem
            // 
            this.CleanMenuItem.Image = global::AliHelper.Properties.Resources.delete1;
            this.CleanMenuItem.Name = "CleanMenuItem";
            this.CleanMenuItem.Size = new System.Drawing.Size(152, 22);
            this.CleanMenuItem.Text = "清除图片";
            this.CleanMenuItem.Click += new System.EventHandler(this.CleanMenuItem_Click);
            // 
            // ImageSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Name = "ImageSelector";
            this.Size = new System.Drawing.Size(125, 125);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ImageSelector_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImageSelector_MouseDown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileSelectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ScreenshotMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CutBroadMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CleanMenuItem;
    }
}
