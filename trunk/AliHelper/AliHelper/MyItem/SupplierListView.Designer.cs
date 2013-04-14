namespace AliHelper.MyItem
{
    partial class SupplierListView
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
            this.Remark = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.QueryBtn = new System.Windows.Forms.Button();
            this.SupplierName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SupplierGrid = new SourceGrid.Grid();
            this.pager = new AliHelper.Pager();
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
            this.tableLayoutPanel1.Controls.Add(this.SupplierGrid, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pager, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(795, 543);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // FinDetailGroup
            // 
            this.FinDetailGroup.Caption = "查询条件";
            this.FinDetailGroup.Controls.Add(this.Remark);
            this.FinDetailGroup.Controls.Add(this.label1);
            this.FinDetailGroup.Controls.Add(this.QueryBtn);
            this.FinDetailGroup.Controls.Add(this.SupplierName);
            this.FinDetailGroup.Controls.Add(this.label6);
            this.FinDetailGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FinDetailGroup.Expanded = false;
            this.FinDetailGroup.ExpandedHeight = 100;
            this.FinDetailGroup.HeaderContextMenuStrip = null;
            this.FinDetailGroup.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2003Silver;
            this.FinDetailGroup.Location = new System.Drawing.Point(1, 1);
            this.FinDetailGroup.Margin = new System.Windows.Forms.Padding(0);
            this.FinDetailGroup.Name = "FinDetailGroup";
            this.FinDetailGroup.Padding = new System.Windows.Forms.Padding(1, 22, 1, 1);
            this.FinDetailGroup.Size = new System.Drawing.Size(793, 50);
            this.FinDetailGroup.TabIndex = 2;
            this.FinDetailGroup.Text = "查询条件";
            // 
            // Remark
            // 
            this.Remark.Location = new System.Drawing.Point(383, 24);
            this.Remark.Name = "Remark";
            this.Remark.Size = new System.Drawing.Size(225, 21);
            this.Remark.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(348, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "备注";
            // 
            // QueryBtn
            // 
            this.QueryBtn.Location = new System.Drawing.Point(650, 23);
            this.QueryBtn.Name = "QueryBtn";
            this.QueryBtn.Size = new System.Drawing.Size(70, 23);
            this.QueryBtn.TabIndex = 8;
            this.QueryBtn.Text = "查询(&Q)";
            this.QueryBtn.UseVisualStyleBackColor = true;
            this.QueryBtn.Click += new System.EventHandler(this.QueryBtn_Click);
            // 
            // SupplierName
            // 
            this.SupplierName.Location = new System.Drawing.Point(123, 24);
            this.SupplierName.Name = "SupplierName";
            this.SupplierName.Size = new System.Drawing.Size(155, 21);
            this.SupplierName.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(54, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "供应商名称";
            // 
            // SupplierGrid
            // 
            this.SupplierGrid.AutoSize = true;
            this.SupplierGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SupplierGrid.EnableSort = true;
            this.SupplierGrid.Location = new System.Drawing.Point(1, 52);
            this.SupplierGrid.Margin = new System.Windows.Forms.Padding(0);
            this.SupplierGrid.Name = "SupplierGrid";
            this.SupplierGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.SupplierGrid.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.SupplierGrid.Size = new System.Drawing.Size(793, 459);
            this.SupplierGrid.TabIndex = 3;
            this.SupplierGrid.TabStop = true;
            this.SupplierGrid.ToolTipText = "";
            // 
            // pager
            // 
            this.pager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pager.Location = new System.Drawing.Point(4, 515);
            this.pager.Name = "pager";
            this.pager.RecordCount = 0;
            this.pager.ShowPageSizeDropdown = true;
            this.pager.Size = new System.Drawing.Size(787, 24);
            this.pager.TabIndex = 4;
            this.pager.PageIndexChanged += new System.EventHandler(this.pager_PageIndexChanged);
            // 
            // SupplierListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SupplierListView";
            this.Size = new System.Drawing.Size(795, 543);
            this.Load += new System.EventHandler(this.SupplierListView_Load);
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
        private System.Windows.Forms.TextBox Remark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button QueryBtn;
        private System.Windows.Forms.TextBox SupplierName;
        private System.Windows.Forms.Label label6;
        private SourceGrid.Grid SupplierGrid;
        private Pager pager;
    }
}
