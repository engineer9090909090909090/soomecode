namespace AliHelper
{
    partial class OrderFinDetailForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.FinDetailDataView = new System.Windows.Forms.DataGridView();
            this.DetailId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FinDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FinGrid = new SourceGrid.Grid();
            ((System.ComponentModel.ISupportInitialize)(this.FinDetailDataView)).BeginInit();
            this.SuspendLayout();
            // 
            // FinDetailDataView
            // 
            this.FinDetailDataView.AllowUserToAddRows = false;
            this.FinDetailDataView.AllowUserToDeleteRows = false;
            this.FinDetailDataView.AllowUserToResizeRows = false;
            this.FinDetailDataView.BackgroundColor = System.Drawing.Color.White;
            this.FinDetailDataView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FinDetailDataView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.FinDetailDataView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.FinDetailDataView.ColumnHeadersHeight = 25;
            this.FinDetailDataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.FinDetailDataView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DetailId,
            this.Id,
            this.FinDate,
            this.Description,
            this.TotalAmount,
            this.EventType,
            this.Remark});
            this.FinDetailDataView.Location = new System.Drawing.Point(261, 306);
            this.FinDetailDataView.Margin = new System.Windows.Forms.Padding(0);
            this.FinDetailDataView.MultiSelect = false;
            this.FinDetailDataView.Name = "FinDetailDataView";
            this.FinDetailDataView.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.FinDetailDataView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.FinDetailDataView.RowHeadersVisible = false;
            this.FinDetailDataView.RowTemplate.Height = 23;
            this.FinDetailDataView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.FinDetailDataView.Size = new System.Drawing.Size(292, 100);
            this.FinDetailDataView.TabIndex = 2;
            // 
            // DetailId
            // 
            this.DetailId.HeaderText = "Id";
            this.DetailId.Name = "DetailId";
            this.DetailId.ReadOnly = true;
            this.DetailId.Visible = false;
            this.DetailId.Width = 50;
            // 
            // Id
            // 
            this.Id.HeaderText = "序号";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 50;
            // 
            // FinDate
            // 
            this.FinDate.HeaderText = "时间";
            this.FinDate.Name = "FinDate";
            this.FinDate.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.HeaderText = "描述";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 250;
            // 
            // TotalAmount
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TotalAmount.DefaultCellStyle = dataGridViewCellStyle5;
            this.TotalAmount.HeaderText = "金额";
            this.TotalAmount.Name = "TotalAmount";
            this.TotalAmount.ReadOnly = true;
            this.TotalAmount.Width = 120;
            // 
            // EventType
            // 
            this.EventType.HeaderText = "收支类型";
            this.EventType.Name = "EventType";
            this.EventType.ReadOnly = true;
            this.EventType.Width = 70;
            // 
            // Remark
            // 
            this.Remark.HeaderText = "备注";
            this.Remark.Name = "Remark";
            this.Remark.ReadOnly = true;
            this.Remark.Width = 200;
            // 
            // FinGrid
            // 
            this.FinGrid.AutoSize = true;
            this.FinGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FinGrid.EnableSort = true;
            this.FinGrid.Location = new System.Drawing.Point(0, 0);
            this.FinGrid.Margin = new System.Windows.Forms.Padding(0);
            this.FinGrid.Name = "FinGrid";
            this.FinGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.FinGrid.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.FinGrid.Size = new System.Drawing.Size(792, 466);
            this.FinGrid.TabIndex = 4;
            this.FinGrid.TabStop = true;
            this.FinGrid.ToolTipText = "";
            // 
            // OrderFinDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 466);
            this.Controls.Add(this.FinGrid);
            this.Controls.Add(this.FinDetailDataView);
            this.MinimizeBox = false;
            this.Name = "OrderFinDetailForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "订单财务明细";
            this.Load += new System.EventHandler(this.OrderFinDetailForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FinDetailDataView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView FinDetailDataView;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn FinDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark;
        private SourceGrid.Grid FinGrid;
    }
}