namespace AliHelper
{
    partial class FinView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.BaseTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.FinDetailGroup = new Guifreaks.NavigationBar.NaviGroup(this.components);
            this.FinDetailExpBtn = new System.Windows.Forms.Button();
            this.FinDetailQueryBtn = new System.Windows.Forms.Button();
            this.EndDateTxt = new System.Windows.Forms.DateTimePicker();
            this.EventTypeTxt = new System.Windows.Forms.ComboBox();
            this.OrderNoTxt = new System.Windows.Forms.TextBox();
            this.AssociationTxt = new System.Windows.Forms.TextBox();
            this.EventNameTxt = new System.Windows.Forms.TextBox();
            this.ItemTypeTxt = new System.Windows.Forms.ComboBox();
            this.BeginDateTxt = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.FinDetailDataView = new System.Windows.Forms.DataGridView();
            this.DetailId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Association = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FinDetailPager = new AliHelper.Pager();
            this.BizTabPage = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.BaseTabPage.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FinDetailGroup)).BeginInit();
            this.FinDetailGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FinDetailDataView)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.BaseTabPage);
            this.tabControl1.Controls.Add(this.BizTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(938, 480);
            this.tabControl1.TabIndex = 0;
            // 
            // BaseTabPage
            // 
            this.BaseTabPage.Controls.Add(this.tableLayoutPanel1);
            this.BaseTabPage.Location = new System.Drawing.Point(4, 21);
            this.BaseTabPage.Name = "BaseTabPage";
            this.BaseTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.BaseTabPage.Size = new System.Drawing.Size(930, 455);
            this.BaseTabPage.TabIndex = 0;
            this.BaseTabPage.Text = "基本视图";
            this.BaseTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.FinDetailGroup, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.FinDetailDataView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.FinDetailPager, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(924, 449);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // FinDetailGroup
            // 
            this.FinDetailGroup.Caption = null;
            this.FinDetailGroup.Controls.Add(this.FinDetailExpBtn);
            this.FinDetailGroup.Controls.Add(this.FinDetailQueryBtn);
            this.FinDetailGroup.Controls.Add(this.EndDateTxt);
            this.FinDetailGroup.Controls.Add(this.EventTypeTxt);
            this.FinDetailGroup.Controls.Add(this.OrderNoTxt);
            this.FinDetailGroup.Controls.Add(this.AssociationTxt);
            this.FinDetailGroup.Controls.Add(this.EventNameTxt);
            this.FinDetailGroup.Controls.Add(this.ItemTypeTxt);
            this.FinDetailGroup.Controls.Add(this.BeginDateTxt);
            this.FinDetailGroup.Controls.Add(this.label6);
            this.FinDetailGroup.Controls.Add(this.label7);
            this.FinDetailGroup.Controls.Add(this.label8);
            this.FinDetailGroup.Controls.Add(this.label4);
            this.FinDetailGroup.Controls.Add(this.label3);
            this.FinDetailGroup.Controls.Add(this.label2);
            this.FinDetailGroup.Controls.Add(this.label1);
            this.FinDetailGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.FinDetailGroup.ExpandedHeight = 121;
            this.FinDetailGroup.HeaderContextMenuStrip = null;
            this.FinDetailGroup.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Black;
            this.FinDetailGroup.Location = new System.Drawing.Point(4, 4);
            this.FinDetailGroup.Name = "FinDetailGroup";
            this.FinDetailGroup.Padding = new System.Windows.Forms.Padding(1, 22, 1, 1);
            this.FinDetailGroup.Size = new System.Drawing.Size(916, 121);
            this.FinDetailGroup.TabIndex = 0;
            this.FinDetailGroup.Text = "naviGroup1";
            // 
            // FinDetailExpBtn
            // 
            this.FinDetailExpBtn.Location = new System.Drawing.Point(791, 75);
            this.FinDetailExpBtn.Name = "FinDetailExpBtn";
            this.FinDetailExpBtn.Size = new System.Drawing.Size(70, 23);
            this.FinDetailExpBtn.TabIndex = 15;
            this.FinDetailExpBtn.Text = "导出(&E)";
            this.FinDetailExpBtn.UseVisualStyleBackColor = true;
            this.FinDetailExpBtn.Click += new System.EventHandler(this.FinDetailExpBtn_Click);
            // 
            // FinDetailQueryBtn
            // 
            this.FinDetailQueryBtn.Location = new System.Drawing.Point(711, 75);
            this.FinDetailQueryBtn.Name = "FinDetailQueryBtn";
            this.FinDetailQueryBtn.Size = new System.Drawing.Size(70, 23);
            this.FinDetailQueryBtn.TabIndex = 14;
            this.FinDetailQueryBtn.Text = "查询(&Q)";
            this.FinDetailQueryBtn.UseVisualStyleBackColor = true;
            this.FinDetailQueryBtn.Click += new System.EventHandler(this.FinDetailQueryBtn_Click);
            // 
            // EndDateTxt
            // 
            this.EndDateTxt.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.EndDateTxt.Location = new System.Drawing.Point(83, 77);
            this.EndDateTxt.Name = "EndDateTxt";
            this.EndDateTxt.Size = new System.Drawing.Size(105, 21);
            this.EndDateTxt.TabIndex = 13;
            // 
            // EventTypeTxt
            // 
            this.EventTypeTxt.FormattingEnabled = true;
            this.EventTypeTxt.Location = new System.Drawing.Point(490, 77);
            this.EventTypeTxt.Name = "EventTypeTxt";
            this.EventTypeTxt.Size = new System.Drawing.Size(121, 20);
            this.EventTypeTxt.TabIndex = 12;
            // 
            // OrderNoTxt
            // 
            this.OrderNoTxt.Location = new System.Drawing.Point(287, 76);
            this.OrderNoTxt.Name = "OrderNoTxt";
            this.OrderNoTxt.Size = new System.Drawing.Size(124, 21);
            this.OrderNoTxt.TabIndex = 11;
            // 
            // AssociationTxt
            // 
            this.AssociationTxt.Location = new System.Drawing.Point(711, 34);
            this.AssociationTxt.Name = "AssociationTxt";
            this.AssociationTxt.Size = new System.Drawing.Size(150, 21);
            this.AssociationTxt.TabIndex = 10;
            // 
            // EventNameTxt
            // 
            this.EventNameTxt.Location = new System.Drawing.Point(288, 34);
            this.EventNameTxt.Name = "EventNameTxt";
            this.EventNameTxt.Size = new System.Drawing.Size(123, 21);
            this.EventNameTxt.TabIndex = 9;
            // 
            // ItemTypeTxt
            // 
            this.ItemTypeTxt.FormattingEnabled = true;
            this.ItemTypeTxt.Location = new System.Drawing.Point(489, 35);
            this.ItemTypeTxt.Name = "ItemTypeTxt";
            this.ItemTypeTxt.Size = new System.Drawing.Size(121, 20);
            this.ItemTypeTxt.TabIndex = 8;
            // 
            // BeginDateTxt
            // 
            this.BeginDateTxt.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.BeginDateTxt.Location = new System.Drawing.Point(83, 34);
            this.BeginDateTxt.Name = "BeginDateTxt";
            this.BeginDateTxt.Size = new System.Drawing.Size(105, 21);
            this.BeginDateTxt.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(224, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "项目名称";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(652, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "相关人员";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(430, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "收支类型";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(224, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "所属业务";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(430, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "项目类型";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(24, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "结束时间";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(24, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "开始时间";
            // 
            // FinDetailDataView
            // 
            this.FinDetailDataView.AllowUserToAddRows = false;
            this.FinDetailDataView.AllowUserToDeleteRows = false;
            this.FinDetailDataView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.FinDetailDataView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.FinDetailDataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FinDetailDataView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DetailId,
            this.EventTime,
            this.EventName,
            this.Amount,
            this.OrderNo,
            this.ItemType,
            this.Association,
            this.EventType,
            this.Remark});
            this.FinDetailDataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FinDetailDataView.Location = new System.Drawing.Point(1, 129);
            this.FinDetailDataView.Margin = new System.Windows.Forms.Padding(0);
            this.FinDetailDataView.Name = "FinDetailDataView";
            this.FinDetailDataView.ReadOnly = true;
            this.FinDetailDataView.RowHeadersVisible = false;
            this.FinDetailDataView.RowTemplate.Height = 23;
            this.FinDetailDataView.Size = new System.Drawing.Size(922, 290);
            this.FinDetailDataView.TabIndex = 1;
            // 
            // DetailId
            // 
            this.DetailId.HeaderText = "序号";
            this.DetailId.Name = "DetailId";
            this.DetailId.ReadOnly = true;
            // 
            // EventTime
            // 
            this.EventTime.HeaderText = "时间";
            this.EventTime.Name = "EventTime";
            this.EventTime.ReadOnly = true;
            // 
            // EventName
            // 
            this.EventName.HeaderText = "项目名称";
            this.EventName.Name = "EventName";
            this.EventName.ReadOnly = true;
            this.EventName.Width = 250;
            // 
            // Amount
            // 
            this.Amount.HeaderText = "金额";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 80;
            // 
            // OrderNo
            // 
            this.OrderNo.HeaderText = "所属业务";
            this.OrderNo.Name = "OrderNo";
            this.OrderNo.ReadOnly = true;
            // 
            // ItemType
            // 
            this.ItemType.HeaderText = "项目类型";
            this.ItemType.Name = "ItemType";
            this.ItemType.ReadOnly = true;
            // 
            // Association
            // 
            this.Association.HeaderText = "经手人/相关人";
            this.Association.Name = "Association";
            this.Association.ReadOnly = true;
            // 
            // EventType
            // 
            this.EventType.HeaderText = "收支类型";
            this.EventType.Name = "EventType";
            this.EventType.ReadOnly = true;
            // 
            // Remark
            // 
            this.Remark.HeaderText = "备注";
            this.Remark.Name = "Remark";
            this.Remark.ReadOnly = true;
            this.Remark.Width = 200;
            // 
            // FinDetailPager
            // 
            this.FinDetailPager.Dock = System.Windows.Forms.DockStyle.Right;
            this.FinDetailPager.Location = new System.Drawing.Point(390, 423);
            this.FinDetailPager.Name = "FinDetailPager";
            this.FinDetailPager.PageSize = 20;
            this.FinDetailPager.RecordCount = 0;
            this.FinDetailPager.ShowPageSizeDropdown = false;
            this.FinDetailPager.Size = new System.Drawing.Size(530, 22);
            this.FinDetailPager.TabIndex = 2;
            this.FinDetailPager.PageIndexChanged += new System.EventHandler(this.FinDetailPager_PageIndexChanged);
            // 
            // BizTabPage
            // 
            this.BizTabPage.Location = new System.Drawing.Point(4, 21);
            this.BizTabPage.Name = "BizTabPage";
            this.BizTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.BizTabPage.Size = new System.Drawing.Size(930, 455);
            this.BizTabPage.TabIndex = 1;
            this.BizTabPage.Text = "业务视图";
            this.BizTabPage.UseVisualStyleBackColor = true;
            // 
            // FinView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "FinView";
            this.Size = new System.Drawing.Size(938, 480);
            this.tabControl1.ResumeLayout(false);
            this.BaseTabPage.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FinDetailGroup)).EndInit();
            this.FinDetailGroup.ResumeLayout(false);
            this.FinDetailGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FinDetailDataView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage BaseTabPage;
        private System.Windows.Forms.TabPage BizTabPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView FinDetailDataView;
        private Guifreaks.NavigationBar.NaviGroup FinDetailGroup;
        private Pager FinDetailPager;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker BeginDateTxt;
        private System.Windows.Forms.ComboBox EventTypeTxt;
        private System.Windows.Forms.TextBox OrderNoTxt;
        private System.Windows.Forms.TextBox AssociationTxt;
        private System.Windows.Forms.TextBox EventNameTxt;
        private System.Windows.Forms.ComboBox ItemTypeTxt;
        private System.Windows.Forms.DateTimePicker EndDateTxt;
        private System.Windows.Forms.Button FinDetailQueryBtn;
        private System.Windows.Forms.Button FinDetailExpBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailId;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Association;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark;

    }
}
