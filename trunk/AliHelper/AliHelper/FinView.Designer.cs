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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.FinDetailPager = new AliHelper.Pager();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FinDetailGroup)).BeginInit();
            this.FinDetailGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FinDetailDataView)).BeginInit();
            this.SuspendLayout();
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(938, 480);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // FinDetailGroup
            // 
            this.FinDetailGroup.Caption = "查询条件";
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
            this.FinDetailGroup.ExpandedHeight = 100;
            this.FinDetailGroup.HeaderContextMenuStrip = null;
            this.FinDetailGroup.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2003Silver;
            this.FinDetailGroup.Location = new System.Drawing.Point(1, 1);
            this.FinDetailGroup.Margin = new System.Windows.Forms.Padding(0);
            this.FinDetailGroup.Name = "FinDetailGroup";
            this.FinDetailGroup.Padding = new System.Windows.Forms.Padding(1, 22, 1, 1);
            this.FinDetailGroup.Size = new System.Drawing.Size(936, 100);
            this.FinDetailGroup.TabIndex = 0;
            this.FinDetailGroup.Text = "查询条件";
            // 
            // FinDetailExpBtn
            // 
            this.FinDetailExpBtn.Location = new System.Drawing.Point(710, 67);
            this.FinDetailExpBtn.Name = "FinDetailExpBtn";
            this.FinDetailExpBtn.Size = new System.Drawing.Size(70, 23);
            this.FinDetailExpBtn.TabIndex = 15;
            this.FinDetailExpBtn.Text = "导出(&E)";
            this.FinDetailExpBtn.UseVisualStyleBackColor = true;
            this.FinDetailExpBtn.Click += new System.EventHandler(this.FinDetailExpBtn_Click);
            // 
            // FinDetailQueryBtn
            // 
            this.FinDetailQueryBtn.Location = new System.Drawing.Point(634, 67);
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
            this.EndDateTxt.Location = new System.Drawing.Point(83, 69);
            this.EndDateTxt.Name = "EndDateTxt";
            this.EndDateTxt.Size = new System.Drawing.Size(85, 21);
            this.EndDateTxt.TabIndex = 13;
            // 
            // EventTypeTxt
            // 
            this.EventTypeTxt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EventTypeTxt.FormattingEnabled = true;
            this.EventTypeTxt.Location = new System.Drawing.Point(442, 69);
            this.EventTypeTxt.Name = "EventTypeTxt";
            this.EventTypeTxt.Size = new System.Drawing.Size(121, 20);
            this.EventTypeTxt.TabIndex = 12;
            // 
            // OrderNoTxt
            // 
            this.OrderNoTxt.Location = new System.Drawing.Point(240, 68);
            this.OrderNoTxt.Name = "OrderNoTxt";
            this.OrderNoTxt.Size = new System.Drawing.Size(124, 21);
            this.OrderNoTxt.TabIndex = 11;
            // 
            // AssociationTxt
            // 
            this.AssociationTxt.Location = new System.Drawing.Point(630, 34);
            this.AssociationTxt.Name = "AssociationTxt";
            this.AssociationTxt.Size = new System.Drawing.Size(150, 21);
            this.AssociationTxt.TabIndex = 10;
            // 
            // EventNameTxt
            // 
            this.EventNameTxt.Location = new System.Drawing.Point(241, 34);
            this.EventNameTxt.Name = "EventNameTxt";
            this.EventNameTxt.Size = new System.Drawing.Size(123, 21);
            this.EventNameTxt.TabIndex = 9;
            // 
            // ItemTypeTxt
            // 
            this.ItemTypeTxt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ItemTypeTxt.FormattingEnabled = true;
            this.ItemTypeTxt.Location = new System.Drawing.Point(441, 35);
            this.ItemTypeTxt.Name = "ItemTypeTxt";
            this.ItemTypeTxt.Size = new System.Drawing.Size(121, 20);
            this.ItemTypeTxt.TabIndex = 8;
            // 
            // BeginDateTxt
            // 
            this.BeginDateTxt.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.BeginDateTxt.Location = new System.Drawing.Point(83, 34);
            this.BeginDateTxt.Name = "BeginDateTxt";
            this.BeginDateTxt.Size = new System.Drawing.Size(85, 21);
            this.BeginDateTxt.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(184, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "项目名称";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(575, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "相关人员";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(386, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "收支类型";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(184, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "所属业务";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(386, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "项目类型";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(24, 73);
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
            this.label1.Margin = new System.Windows.Forms.Padding(10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "开始时间";
            // 
            // FinDetailDataView
            // 
            this.FinDetailDataView.AllowUserToAddRows = false;
            this.FinDetailDataView.AllowUserToDeleteRows = false;
            this.FinDetailDataView.AllowUserToResizeRows = false;
            this.FinDetailDataView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.FinDetailDataView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.FinDetailDataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FinDetailDataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FinDetailDataView.Location = new System.Drawing.Point(1, 102);
            this.FinDetailDataView.Margin = new System.Windows.Forms.Padding(0);
            this.FinDetailDataView.MultiSelect = false;
            this.FinDetailDataView.Name = "FinDetailDataView";
            this.FinDetailDataView.ReadOnly = true;
            this.FinDetailDataView.RowHeadersVisible = false;
            this.FinDetailDataView.RowTemplate.Height = 23;
            this.FinDetailDataView.Size = new System.Drawing.Size(936, 348);
            this.FinDetailDataView.TabIndex = 1;
            this.FinDetailDataView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FinDetailDataView_CellDoubleClick);
            // 
            // FinDetailPager
            // 
            this.FinDetailPager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FinDetailPager.Location = new System.Drawing.Point(4, 454);
            this.FinDetailPager.Name = "FinDetailPager";
            this.FinDetailPager.PageSize = 20;
            this.FinDetailPager.RecordCount = 0;
            this.FinDetailPager.ShowPageSizeDropdown = false;
            this.FinDetailPager.Size = new System.Drawing.Size(930, 22);
            this.FinDetailPager.TabIndex = 2;
            this.FinDetailPager.PageIndexChanged += new System.EventHandler(this.FinDetailPager_PageIndexChanged);
            // 
            // FinView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FinView";
            this.Size = new System.Drawing.Size(938, 480);
            this.Load += new System.EventHandler(this.FinView_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FinDetailGroup)).EndInit();
            this.FinDetailGroup.ResumeLayout(false);
            this.FinDetailGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FinDetailDataView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

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
    }
}
