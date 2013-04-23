namespace AliHelper
{
    partial class CategoriesForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CateTreeView = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewChildMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveUpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveDownMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewTopButton = new System.Windows.Forms.Button();
            this.CategoryGroup = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CategoryName = new System.Windows.Forms.TextBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.ConfirmButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.CategoryGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CateTreeView);
            this.groupBox1.Location = new System.Drawing.Point(9, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(295, 528);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // CateTreeView
            // 
            this.CateTreeView.ContextMenuStrip = this.contextMenuStrip1;
            this.CateTreeView.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CateTreeView.ItemHeight = 18;
            this.CateTreeView.Location = new System.Drawing.Point(7, 16);
            this.CateTreeView.Name = "CateTreeView";
            this.CateTreeView.Size = new System.Drawing.Size(280, 502);
            this.CateTreeView.TabIndex = 0;
            this.CateTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.CateTreeView_NodeMouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewChildMenuItem,
            this.RenameMenuItem,
            this.DeleMenuItem,
            this.MoveUpMenuItem,
            this.MoveDownMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(150, 114);
            // 
            // NewChildMenuItem
            // 
            this.NewChildMenuItem.Name = "NewChildMenuItem";
            this.NewChildMenuItem.Size = new System.Drawing.Size(152, 22);
            this.NewChildMenuItem.Text = "增加子分类(&N)";
            this.NewChildMenuItem.Click += new System.EventHandler(this.NewChildMenuItem_Click);
            // 
            // RenameMenuItem
            // 
            this.RenameMenuItem.Name = "RenameMenuItem";
            this.RenameMenuItem.Size = new System.Drawing.Size(152, 22);
            this.RenameMenuItem.Text = "重命名分类(&R)";
            this.RenameMenuItem.Click += new System.EventHandler(this.RenameMenuItem_Click);
            // 
            // DeleMenuItem
            // 
            this.DeleMenuItem.Name = "DeleMenuItem";
            this.DeleMenuItem.Size = new System.Drawing.Size(152, 22);
            this.DeleMenuItem.Text = "删除分类(D)";
            this.DeleMenuItem.Click += new System.EventHandler(this.DeleMenuItem_Click);
            // 
            // MoveUpMenuItem
            // 
            this.MoveUpMenuItem.Name = "MoveUpMenuItem";
            this.MoveUpMenuItem.Size = new System.Drawing.Size(152, 22);
            this.MoveUpMenuItem.Text = "上移(&U)";
            this.MoveUpMenuItem.Click += new System.EventHandler(this.MoveUpMenuItem_Click);
            // 
            // MoveDownMenuItem
            // 
            this.MoveDownMenuItem.Name = "MoveDownMenuItem";
            this.MoveDownMenuItem.Size = new System.Drawing.Size(152, 22);
            this.MoveDownMenuItem.Text = "下移(&M)";
            this.MoveDownMenuItem.Click += new System.EventHandler(this.MoveDownMenuItem_Click);
            // 
            // NewTopButton
            // 
            this.NewTopButton.Location = new System.Drawing.Point(320, 18);
            this.NewTopButton.Name = "NewTopButton";
            this.NewTopButton.Size = new System.Drawing.Size(113, 23);
            this.NewTopButton.TabIndex = 1;
            this.NewTopButton.Text = "新增顶级分类(&N)";
            this.NewTopButton.UseVisualStyleBackColor = true;
            this.NewTopButton.Click += new System.EventHandler(this.NewTopButton_Click);
            // 
            // CategoryGroup
            // 
            this.CategoryGroup.Controls.Add(this.label1);
            this.CategoryGroup.Controls.Add(this.CategoryName);
            this.CategoryGroup.Controls.Add(this.CancelButton);
            this.CategoryGroup.Controls.Add(this.ConfirmButton);
            this.CategoryGroup.Location = new System.Drawing.Point(317, 71);
            this.CategoryGroup.Name = "CategoryGroup";
            this.CategoryGroup.Size = new System.Drawing.Size(399, 119);
            this.CategoryGroup.TabIndex = 2;
            this.CategoryGroup.TabStop = false;
            this.CategoryGroup.Text = "新增分类";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "分类名称";
            // 
            // CategoryName
            // 
            this.CategoryName.Location = new System.Drawing.Point(106, 35);
            this.CategoryName.Name = "CategoryName";
            this.CategoryName.Size = new System.Drawing.Size(229, 21);
            this.CategoryName.TabIndex = 2;
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(270, 81);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(68, 23);
            this.CancelButton.TabIndex = 4;
            this.CancelButton.Text = "取消(&C)";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.Location = new System.Drawing.Point(198, 81);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.Size = new System.Drawing.Size(66, 23);
            this.ConfirmButton.TabIndex = 3;
            this.ConfirmButton.Text = "确定(&S)";
            this.ConfirmButton.UseVisualStyleBackColor = true;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // CategoriesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 542);
            this.Controls.Add(this.CategoryGroup);
            this.Controls.Add(this.NewTopButton);
            this.Controls.Add(this.groupBox1);
            this.Name = "CategoriesForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "产品自定义分类管理";
            this.Load += new System.EventHandler(this.CategoriesForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.CategoryGroup.ResumeLayout(false);
            this.CategoryGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView CateTreeView;
        private System.Windows.Forms.Button NewTopButton;
        private System.Windows.Forms.GroupBox CategoryGroup;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button ConfirmButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CategoryName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem RenameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MoveUpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MoveDownMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewChildMenuItem;
    }
}