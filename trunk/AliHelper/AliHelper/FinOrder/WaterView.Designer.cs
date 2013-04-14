
namespace AliHelper
{
    partial class WaterView
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            FinOrderManager.OnEditFinanceEvent -= new NewEditItemEvent(OnNewEditEvent);
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.FinDetailGroup = new Guifreaks.NavigationBar.NaviGroup(this.components);
            this.Association = new System.Windows.Forms.ComboBox();
            this.ExpBtn = new System.Windows.Forms.Button();
            this.FinDetailQueryBtn = new System.Windows.Forms.Button();
            this.EndDate = new System.Windows.Forms.DateTimePicker();
            this.EventType = new System.Windows.Forms.ComboBox();
            this.ReceivePaymentor = new System.Windows.Forms.TextBox();
            this.Description = new System.Windows.Forms.TextBox();
            this.ItemType = new System.Windows.Forms.ComboBox();
            this.BeginDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.FinGrid = new SourceGrid.Grid();
            ((System.ComponentModel.ISupportInitialize)(this.FinDetailGroup)).BeginInit();
            this.FinDetailGroup.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FinDetailGroup
            // 
            this.FinDetailGroup.Caption = "查询条件";
            this.FinDetailGroup.Controls.Add(this.Association);
            this.FinDetailGroup.Controls.Add(this.ExpBtn);
            this.FinDetailGroup.Controls.Add(this.FinDetailQueryBtn);
            this.FinDetailGroup.Controls.Add(this.EndDate);
            this.FinDetailGroup.Controls.Add(this.EventType);
            this.FinDetailGroup.Controls.Add(this.ReceivePaymentor);
            this.FinDetailGroup.Controls.Add(this.Description);
            this.FinDetailGroup.Controls.Add(this.ItemType);
            this.FinDetailGroup.Controls.Add(this.BeginDate);
            this.FinDetailGroup.Controls.Add(this.label6);
            this.FinDetailGroup.Controls.Add(this.label7);
            this.FinDetailGroup.Controls.Add(this.label8);
            this.FinDetailGroup.Controls.Add(this.label4);
            this.FinDetailGroup.Controls.Add(this.label3);
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
            this.FinDetailGroup.Size = new System.Drawing.Size(796, 100);
            this.FinDetailGroup.TabIndex = 2;
            this.FinDetailGroup.Text = "查询条件";
            // 
            // Association
            // 
            this.Association.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Association.FormattingEnabled = true;
            this.Association.Location = new System.Drawing.Point(242, 66);
            this.Association.Name = "Association";
            this.Association.Size = new System.Drawing.Size(121, 20);
            this.Association.TabIndex = 10;
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
            this.FinDetailQueryBtn.Location = new System.Drawing.Point(634, 67);
            this.FinDetailQueryBtn.Name = "FinDetailQueryBtn";
            this.FinDetailQueryBtn.Size = new System.Drawing.Size(70, 23);
            this.FinDetailQueryBtn.TabIndex = 8;
            this.FinDetailQueryBtn.Text = "查询(&Q)";
            this.FinDetailQueryBtn.UseVisualStyleBackColor = true;
            this.FinDetailQueryBtn.Click += new System.EventHandler(this.FinDetailQueryBtn_Click);
            // 
            // EndDate
            // 
            this.EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.EndDate.Location = new System.Drawing.Point(83, 69);
            this.EndDate.Name = "EndDate";
            this.EndDate.Size = new System.Drawing.Size(85, 21);
            this.EndDate.TabIndex = 5;
            // 
            // EventType
            // 
            this.EventType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EventType.FormattingEnabled = true;
            this.EventType.Location = new System.Drawing.Point(442, 69);
            this.EventType.Name = "EventType";
            this.EventType.Size = new System.Drawing.Size(121, 20);
            this.EventType.TabIndex = 7;
            // 
            // ReceivePaymentor
            // 
            this.ReceivePaymentor.Location = new System.Drawing.Point(632, 34);
            this.ReceivePaymentor.Name = "ReceivePaymentor";
            this.ReceivePaymentor.Size = new System.Drawing.Size(124, 21);
            this.ReceivePaymentor.TabIndex = 3;
            // 
            // Description
            // 
            this.Description.Location = new System.Drawing.Point(241, 34);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(123, 21);
            this.Description.TabIndex = 0;
            // 
            // ItemType
            // 
            this.ItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ItemType.FormattingEnabled = true;
            this.ItemType.Location = new System.Drawing.Point(441, 35);
            this.ItemType.Name = "ItemType";
            this.ItemType.Size = new System.Drawing.Size(121, 20);
            this.ItemType.TabIndex = 2;
            // 
            // BeginDate
            // 
            this.BeginDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.BeginDate.Location = new System.Drawing.Point(83, 34);
            this.BeginDate.Name = "BeginDate";
            this.BeginDate.Size = new System.Drawing.Size(85, 21);
            this.BeginDate.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(184, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "款项说明";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(184, 71);
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
            this.label4.Location = new System.Drawing.Point(568, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "收付款单位";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(386, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "款项类型";
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.FinDetailGroup, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.FinGrid, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(798, 425);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // FinGrid
            // 
            this.FinGrid.AutoSize = true;
            this.FinGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FinGrid.EnableSort = true;
            this.FinGrid.Location = new System.Drawing.Point(1, 102);
            this.FinGrid.Margin = new System.Windows.Forms.Padding(0);
            this.FinGrid.Name = "FinGrid";
            this.FinGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.FinGrid.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.FinGrid.Size = new System.Drawing.Size(796, 322);
            this.FinGrid.TabIndex = 3;
            this.FinGrid.TabStop = true;
            this.FinGrid.ToolTipText = "";
            // 
            // WaterView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "WaterView";
            this.Size = new System.Drawing.Size(798, 425);
            this.Load += new System.EventHandler(this.FinanceWaterView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FinDetailGroup)).EndInit();
            this.FinDetailGroup.ResumeLayout(false);
            this.FinDetailGroup.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guifreaks.NavigationBar.NaviGroup FinDetailGroup;
        private System.Windows.Forms.Button ExpBtn;
        private System.Windows.Forms.Button FinDetailQueryBtn;
        private System.Windows.Forms.DateTimePicker EndDate;
        private System.Windows.Forms.ComboBox EventType;
        private System.Windows.Forms.TextBox ReceivePaymentor;
        private System.Windows.Forms.TextBox Description;
        private System.Windows.Forms.DateTimePicker BeginDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Association;
        private System.Windows.Forms.ComboBox ItemType;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private SourceGrid.Grid FinGrid;
    }
}
