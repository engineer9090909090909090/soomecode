namespace AliHelper
{
    partial class DicForm
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
            this.EditGroup = new System.Windows.Forms.GroupBox();
            this.DicListView = new System.Windows.Forms.ListView();
            this.KeyColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NewDicType = new System.Windows.Forms.ComboBox();
            this.NewKeyTxt = new System.Windows.Forms.TextBox();
            this.NewValTxt = new System.Windows.Forms.TextBox();
            this.AddBtn = new System.Windows.Forms.Button();
            this.KeyLabel = new System.Windows.Forms.Label();
            this.ValLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.EditBtn = new System.Windows.Forms.Button();
            this.EditValTxt = new System.Windows.Forms.TextBox();
            this.EditKeyTxt = new System.Windows.Forms.TextBox();
            this.EditDicType = new System.Windows.Forms.ComboBox();
            this.DeleteBtn = new System.Windows.Forms.Button();
            this.EditGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // EditGroup
            // 
            this.EditGroup.Controls.Add(this.ValLabel);
            this.EditGroup.Controls.Add(this.KeyLabel);
            this.EditGroup.Controls.Add(this.AddBtn);
            this.EditGroup.Controls.Add(this.NewValTxt);
            this.EditGroup.Controls.Add(this.NewKeyTxt);
            this.EditGroup.Controls.Add(this.NewDicType);
            this.EditGroup.Location = new System.Drawing.Point(12, 8);
            this.EditGroup.Name = "EditGroup";
            this.EditGroup.Size = new System.Drawing.Size(210, 158);
            this.EditGroup.TabIndex = 0;
            this.EditGroup.TabStop = false;
            this.EditGroup.Text = "新增字典";
            // 
            // DicListView
            // 
            this.DicListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.KeyColumn,
            this.ValColumn});
            this.DicListView.FullRowSelect = true;
            this.DicListView.Location = new System.Drawing.Point(230, 11);
            this.DicListView.Name = "DicListView";
            this.DicListView.Size = new System.Drawing.Size(375, 326);
            this.DicListView.TabIndex = 1;
            this.DicListView.UseCompatibleStateImageBehavior = false;
            this.DicListView.View = System.Windows.Forms.View.Details;
            this.DicListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.DicListView_ItemSelectionChanged);
            // 
            // KeyColumn
            // 
            this.KeyColumn.Text = "键";
            this.KeyColumn.Width = 130;
            // 
            // ValColumn
            // 
            this.ValColumn.Text = "值";
            this.ValColumn.Width = 220;
            // 
            // NewDicType
            // 
            this.NewDicType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NewDicType.FormattingEnabled = true;
            this.NewDicType.Location = new System.Drawing.Point(18, 24);
            this.NewDicType.Name = "NewDicType";
            this.NewDicType.Size = new System.Drawing.Size(168, 20);
            this.NewDicType.TabIndex = 0;
            // 
            // NewKeyTxt
            // 
            this.NewKeyTxt.Location = new System.Drawing.Point(60, 59);
            this.NewKeyTxt.Name = "NewKeyTxt";
            this.NewKeyTxt.Size = new System.Drawing.Size(126, 21);
            this.NewKeyTxt.TabIndex = 1;
            // 
            // NewValTxt
            // 
            this.NewValTxt.Location = new System.Drawing.Point(60, 94);
            this.NewValTxt.Name = "NewValTxt";
            this.NewValTxt.Size = new System.Drawing.Size(126, 21);
            this.NewValTxt.TabIndex = 2;
            // 
            // AddBtn
            // 
            this.AddBtn.Location = new System.Drawing.Point(115, 125);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(70, 23);
            this.AddBtn.TabIndex = 3;
            this.AddBtn.Text = "增加";
            this.AddBtn.UseVisualStyleBackColor = true;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // KeyLabel
            // 
            this.KeyLabel.AutoSize = true;
            this.KeyLabel.Location = new System.Drawing.Point(27, 66);
            this.KeyLabel.Name = "KeyLabel";
            this.KeyLabel.Size = new System.Drawing.Size(29, 12);
            this.KeyLabel.TabIndex = 4;
            this.KeyLabel.Text = "键：";
            // 
            // ValLabel
            // 
            this.ValLabel.AutoSize = true;
            this.ValLabel.Location = new System.Drawing.Point(27, 99);
            this.ValLabel.Name = "ValLabel";
            this.ValLabel.Size = new System.Drawing.Size(29, 12);
            this.ValLabel.TabIndex = 5;
            this.ValLabel.Text = "值：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DeleteBtn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.EditBtn);
            this.groupBox1.Controls.Add(this.EditValTxt);
            this.groupBox1.Controls.Add(this.EditKeyTxt);
            this.groupBox1.Controls.Add(this.EditDicType);
            this.groupBox1.Location = new System.Drawing.Point(12, 179);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 158);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "编辑字典";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "值：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "键：";
            // 
            // EditBtn
            // 
            this.EditBtn.Enabled = false;
            this.EditBtn.Location = new System.Drawing.Point(40, 121);
            this.EditBtn.Name = "EditBtn";
            this.EditBtn.Size = new System.Drawing.Size(70, 23);
            this.EditBtn.TabIndex = 3;
            this.EditBtn.Text = "修改";
            this.EditBtn.UseVisualStyleBackColor = true;
            this.EditBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // EditValTxt
            // 
            this.EditValTxt.Location = new System.Drawing.Point(60, 94);
            this.EditValTxt.Name = "EditValTxt";
            this.EditValTxt.Size = new System.Drawing.Size(126, 21);
            this.EditValTxt.TabIndex = 2;
            // 
            // EditKeyTxt
            // 
            this.EditKeyTxt.Location = new System.Drawing.Point(60, 59);
            this.EditKeyTxt.Name = "EditKeyTxt";
            this.EditKeyTxt.ReadOnly = true;
            this.EditKeyTxt.Size = new System.Drawing.Size(126, 21);
            this.EditKeyTxt.TabIndex = 1;
            // 
            // EditDicType
            // 
            this.EditDicType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EditDicType.FormattingEnabled = true;
            this.EditDicType.Location = new System.Drawing.Point(18, 24);
            this.EditDicType.Name = "EditDicType";
            this.EditDicType.Size = new System.Drawing.Size(168, 20);
            this.EditDicType.TabIndex = 0;
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.Enabled = false;
            this.DeleteBtn.Location = new System.Drawing.Point(116, 121);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(70, 23);
            this.DeleteBtn.TabIndex = 6;
            this.DeleteBtn.Text = "删除";
            this.DeleteBtn.UseVisualStyleBackColor = true;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // DicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 348);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.DicListView);
            this.Controls.Add(this.EditGroup);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DicForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "数据字典";
            this.EditGroup.ResumeLayout(false);
            this.EditGroup.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox EditGroup;
        private System.Windows.Forms.ListView DicListView;
        private System.Windows.Forms.ColumnHeader KeyColumn;
        private System.Windows.Forms.ColumnHeader ValColumn;
        private System.Windows.Forms.ComboBox NewDicType;
        private System.Windows.Forms.Label ValLabel;
        private System.Windows.Forms.Label KeyLabel;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.TextBox NewValTxt;
        private System.Windows.Forms.TextBox NewKeyTxt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button EditBtn;
        private System.Windows.Forms.TextBox EditValTxt;
        private System.Windows.Forms.TextBox EditKeyTxt;
        private System.Windows.Forms.ComboBox EditDicType;
        private System.Windows.Forms.Button DeleteBtn;
    }
}