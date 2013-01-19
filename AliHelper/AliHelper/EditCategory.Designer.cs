namespace AliHelper
{
    partial class EditCategory
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
            this.KeywordBox = new System.Windows.Forms.TextBox();
            this.CategoryListBox = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.MyCategoryListBox = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // KeywordBox
            // 
            this.KeywordBox.Location = new System.Drawing.Point(137, 11);
            this.KeywordBox.Name = "KeywordBox";
            this.KeywordBox.Size = new System.Drawing.Size(280, 21);
            this.KeywordBox.TabIndex = 0;
            this.KeywordBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeywordBox_KeyDown);
            // 
            // CategoryListBox
            // 
            this.CategoryListBox.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CategoryListBox.FormattingEnabled = true;
            this.CategoryListBox.ItemHeight = 12;
            this.CategoryListBox.Location = new System.Drawing.Point(19, 45);
            this.CategoryListBox.Name = "CategoryListBox";
            this.CategoryListBox.Size = new System.Drawing.Size(481, 304);
            this.CategoryListBox.TabIndex = 3;
            this.CategoryListBox.DoubleClick += new System.EventHandler(this.CategoryListBox_DoubleClick);
            this.CategoryListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CategoryListBox_KeyDown);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(520, 401);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.SearchButton);
            this.tabPage1.Controls.Add(this.CategoryListBox);
            this.tabPage1.Controls.Add(this.KeywordBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(512, 376);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "搜索类目";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "请输入产品关键词: ";
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(425, 9);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 2;
            this.SearchButton.Text = "搜  索(&S)";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.MyCategoryListBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(512, 376);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "您经常使用的类目";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // MyCategoryListBox
            // 
            this.MyCategoryListBox.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MyCategoryListBox.FormattingEnabled = true;
            this.MyCategoryListBox.ItemHeight = 12;
            this.MyCategoryListBox.Location = new System.Drawing.Point(16, 12);
            this.MyCategoryListBox.Name = "MyCategoryListBox";
            this.MyCategoryListBox.Size = new System.Drawing.Size(481, 328);
            this.MyCategoryListBox.TabIndex = 4;
            this.MyCategoryListBox.DoubleClick += new System.EventHandler(this.CategoryListBox_DoubleClick);
            this.MyCategoryListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CategoryListBox_KeyDown);
            // 
            // EditCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 403);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditCategory";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "产品类目选择";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox KeywordBox;
        private System.Windows.Forms.ListBox CategoryListBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.ListBox MyCategoryListBox;
    }
}