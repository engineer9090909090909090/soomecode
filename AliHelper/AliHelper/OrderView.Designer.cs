namespace AliHelper
{
    partial class OrderView
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.FinDetailGroup = new Guifreaks.NavigationBar.NaviGroup(this.components);
            this.Status = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.EndDateTo = new AliHelper.DateTimePickerX();
            this.BeginDateTo = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Remark = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SalesMan = new System.Windows.Forms.ComboBox();
            this.ExpBtn = new System.Windows.Forms.Button();
            this.FinDetailQueryBtn = new System.Windows.Forms.Button();
            this.EndDateForm = new AliHelper.DateTimePickerX();
            this.Description = new System.Windows.Forms.TextBox();
            this.BeginDateForm = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.OrderGrid = new SourceGrid.Grid();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FinDetailGroup)).BeginInit();
            this.FinDetailGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.FinDetailGroup, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.OrderGrid, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(794, 479);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // FinDetailGroup
            // 
            this.FinDetailGroup.Caption = "查询条件";
            this.FinDetailGroup.Controls.Add(this.Status);
            this.FinDetailGroup.Controls.Add(this.label8);
            this.FinDetailGroup.Controls.Add(this.EndDateTo);
            this.FinDetailGroup.Controls.Add(this.BeginDateTo);
            this.FinDetailGroup.Controls.Add(this.label4);
            this.FinDetailGroup.Controls.Add(this.label5);
            this.FinDetailGroup.Controls.Add(this.Remark);
            this.FinDetailGroup.Controls.Add(this.label3);
            this.FinDetailGroup.Controls.Add(this.SalesMan);
            this.FinDetailGroup.Controls.Add(this.ExpBtn);
            this.FinDetailGroup.Controls.Add(this.FinDetailQueryBtn);
            this.FinDetailGroup.Controls.Add(this.EndDateForm);
            this.FinDetailGroup.Controls.Add(this.Description);
            this.FinDetailGroup.Controls.Add(this.BeginDateForm);
            this.FinDetailGroup.Controls.Add(this.label6);
            this.FinDetailGroup.Controls.Add(this.label7);
            this.FinDetailGroup.Controls.Add(this.label2);
            this.FinDetailGroup.Controls.Add(this.label1);
            this.FinDetailGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FinDetailGroup.ExpandedHeight = 100;
            this.FinDetailGroup.HeaderContextMenuStrip = null;
            this.FinDetailGroup.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2003Silver;
            this.FinDetailGroup.Location = new System.Drawing.Point(1, 1);
            this.FinDetailGroup.Margin = new System.Windows.Forms.Padding(0);
            this.FinDetailGroup.Name = "FinDetailGroup";
            this.FinDetailGroup.Padding = new System.Windows.Forms.Padding(1, 22, 1, 1);
            this.FinDetailGroup.Size = new System.Drawing.Size(792, 100);
            this.FinDetailGroup.TabIndex = 2;
            this.FinDetailGroup.Text = "查询条件";
            // 
            // Status
            // 
            this.Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Status.FormattingEnabled = true;
            this.Status.Location = new System.Drawing.Point(585, 67);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(110, 20);
            this.Status.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(531, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "订单状态";
            // 
            // EndDateTo
            // 
            this.EndDateTo.CustomFormat = " ";
            this.EndDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EndDateTo.FormatString = "yyyy-MM-dd";
            this.EndDateTo.Location = new System.Drawing.Point(241, 69);
            this.EndDateTo.Name = "EndDateTo";
            this.EndDateTo.OriginalFormat = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EndDateTo.Size = new System.Drawing.Size(85, 21);
            this.EndDateTo.TabIndex = 16;
            this.EndDateTo.ValueX = null;
            // 
            // BeginDateTo
            // 
            this.BeginDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.BeginDateTo.Location = new System.Drawing.Point(80, 68);
            this.BeginDateTo.Name = "BeginDateTo";
            this.BeginDateTo.Size = new System.Drawing.Size(85, 21);
            this.BeginDateTo.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(176, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "结束时间到";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(15, 72);
            this.label5.Margin = new System.Windows.Forms.Padding(10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "开始时间到";
            // 
            // Remark
            // 
            this.Remark.Location = new System.Drawing.Point(584, 34);
            this.Remark.Name = "Remark";
            this.Remark.Size = new System.Drawing.Size(110, 21);
            this.Remark.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(530, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "备    注";
            // 
            // SalesMan
            // 
            this.SalesMan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SalesMan.FormattingEnabled = true;
            this.SalesMan.Location = new System.Drawing.Point(395, 67);
            this.SalesMan.Name = "SalesMan";
            this.SalesMan.Size = new System.Drawing.Size(121, 20);
            this.SalesMan.TabIndex = 10;
            // 
            // ExpBtn
            // 
            this.ExpBtn.Location = new System.Drawing.Point(710, 67);
            this.ExpBtn.Name = "ExpBtn";
            this.ExpBtn.Size = new System.Drawing.Size(70, 23);
            this.ExpBtn.TabIndex = 9;
            this.ExpBtn.Text = "导出(&E)";
            this.ExpBtn.UseVisualStyleBackColor = true;
            this.ExpBtn.Click += new System.EventHandler(this.ExpBtn_Click);
            // 
            // FinDetailQueryBtn
            // 
            this.FinDetailQueryBtn.Location = new System.Drawing.Point(710, 34);
            this.FinDetailQueryBtn.Name = "FinDetailQueryBtn";
            this.FinDetailQueryBtn.Size = new System.Drawing.Size(70, 23);
            this.FinDetailQueryBtn.TabIndex = 8;
            this.FinDetailQueryBtn.Text = "查询(&Q)";
            this.FinDetailQueryBtn.UseVisualStyleBackColor = true;
            this.FinDetailQueryBtn.Click += new System.EventHandler(this.FinDetailQueryBtn_Click);
            // 
            // EndDateForm
            // 
            this.EndDateForm.CustomFormat = " ";
            this.EndDateForm.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.EndDateForm.FormatString = "yyyy-MM-dd";
            this.EndDateForm.Location = new System.Drawing.Point(241, 35);
            this.EndDateForm.Name = "EndDateForm";
            this.EndDateForm.OriginalFormat = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EndDateForm.Size = new System.Drawing.Size(85, 21);
            this.EndDateForm.TabIndex = 5;
            this.EndDateForm.ValueX = null;
            // 
            // Description
            // 
            this.Description.Location = new System.Drawing.Point(394, 34);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(122, 21);
            this.Description.TabIndex = 0;
            // 
            // BeginDateForm
            // 
            this.BeginDateForm.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.BeginDateForm.Location = new System.Drawing.Point(80, 34);
            this.BeginDateForm.Name = "BeginDateForm";
            this.BeginDateForm.Size = new System.Drawing.Size(85, 21);
            this.BeginDateForm.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(342, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "订单描述";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(342, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "业 务 员";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(176, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "结束时间从";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(15, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "开始时间从";
            // 
            // OrderGrid
            // 
            this.OrderGrid.AutoSize = true;
            this.OrderGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OrderGrid.EnableSort = true;
            this.OrderGrid.Location = new System.Drawing.Point(1, 102);
            this.OrderGrid.Margin = new System.Windows.Forms.Padding(0);
            this.OrderGrid.Name = "OrderGrid";
            this.OrderGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.OrderGrid.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.OrderGrid.Size = new System.Drawing.Size(792, 376);
            this.OrderGrid.TabIndex = 3;
            this.OrderGrid.TabStop = true;
            this.OrderGrid.ToolTipText = "";
            // 
            // OrderView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "OrderView";
            this.Size = new System.Drawing.Size(794, 479);
            this.Load += new System.EventHandler(this.OrderView_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FinDetailGroup)).EndInit();
            this.FinDetailGroup.ResumeLayout(false);
            this.FinDetailGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Guifreaks.NavigationBar.NaviGroup FinDetailGroup;
        private System.Windows.Forms.ComboBox SalesMan;
        private System.Windows.Forms.Button ExpBtn;
        private System.Windows.Forms.Button FinDetailQueryBtn;
        private DateTimePickerX EndDateForm;
        private System.Windows.Forms.TextBox Description;
        private System.Windows.Forms.DateTimePicker BeginDateForm;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private SourceGrid.Grid OrderGrid;
        private DateTimePickerX EndDateTo;
        private System.Windows.Forms.DateTimePicker BeginDateTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Remark;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Status;
        private System.Windows.Forms.Label label8;
    }
}
