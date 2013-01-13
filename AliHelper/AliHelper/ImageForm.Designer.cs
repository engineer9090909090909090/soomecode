namespace AliHelper
{
    partial class ImageForm
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
            this.ImageGroupTree = new System.Windows.Forms.TreeView();
            this.ImageListView = new System.Windows.Forms.ListView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearchKey = new System.Windows.Forms.TextBox();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pager1 = new AliHelper.Pager();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImageGroupTree
            // 
            this.ImageGroupTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImageGroupTree.Font = new System.Drawing.Font("SimSun", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ImageGroupTree.ItemHeight = 15;
            this.ImageGroupTree.Location = new System.Drawing.Point(0, 0);
            this.ImageGroupTree.Name = "ImageGroupTree";
            this.ImageGroupTree.Size = new System.Drawing.Size(169, 417);
            this.ImageGroupTree.TabIndex = 0;
            this.ImageGroupTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ImageGroupTree_NodeMouseClick);
            this.ImageGroupTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ImageGroupTree_NodeMouseDoubleClick);
            // 
            // ImageListView
            // 
            this.ImageListView.CheckBoxes = true;
            this.ImageListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImageListView.GridLines = true;
            this.ImageListView.Location = new System.Drawing.Point(0, 0);
            this.ImageListView.Name = "ImageListView";
            this.ImageListView.Size = new System.Drawing.Size(604, 417);
            this.ImageListView.TabIndex = 1;
            this.ImageListView.UseCompatibleStateImageBehavior = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(8, 37);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ImageGroupTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ImageListView);
            this.splitContainer1.Size = new System.Drawing.Size(777, 417);
            this.splitContainer1.SplitterDistance = 169;
            this.splitContainer1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(169, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "按图片名称搜索：";
            // 
            // txtSearchKey
            // 
            this.txtSearchKey.Location = new System.Drawing.Point(273, 7);
            this.txtSearchKey.Name = "txtSearchKey";
            this.txtSearchKey.Size = new System.Drawing.Size(228, 21);
            this.txtSearchKey.TabIndex = 4;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(510, 6);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(75, 23);
            this.BtnSearch.TabIndex = 7;
            this.BtnSearch.Text = "搜索(&Q)";
            this.BtnSearch.UseVisualStyleBackColor = true;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(654, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "引用此图(&S)";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // pager1
            // 
            this.pager1.Location = new System.Drawing.Point(252, 459);
            this.pager1.Name = "pager1";
            this.pager1.PageSize = 20;
            this.pager1.RecordCount = 0;
            this.pager1.ShowPageSizeDropdown = false;
            this.pager1.Size = new System.Drawing.Size(530, 25);
            this.pager1.TabIndex = 9;
            this.pager1.PageIndexChanged += new System.EventHandler(this.pager1_PageIndexChanged);
            // 
            // ImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 490);
            this.Controls.Add(this.pager1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.BtnSearch);
            this.Controls.Add(this.txtSearchKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImageForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "图片银行";
            this.Load += new System.EventHandler(this.ImageForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView ImageGroupTree;
        private System.Windows.Forms.ListView ImageListView;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearchKey;
        private System.Windows.Forms.Button BtnSearch;
        private System.Windows.Forms.Button button2;
        private Pager pager1;
    }
}