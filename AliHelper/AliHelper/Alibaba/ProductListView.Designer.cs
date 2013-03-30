namespace AliHelper
{
    partial class ProductListView
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pager1 = new AliHelper.Pager();
            this.ProductGrid = new SourceGrid.Grid();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.ProductGrid, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pager1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(771, 446);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pager1
            // 
            this.pager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pager1.Location = new System.Drawing.Point(3, 421);
            this.pager1.Name = "pager1";
            this.pager1.RecordCount = 0;
            this.pager1.ShowPageSizeDropdown = true;
            this.pager1.Size = new System.Drawing.Size(765, 22);
            this.pager1.TabIndex = 0;
            // 
            // ProductGrid
            // 
            this.ProductGrid.AutoSize = true;
            this.ProductGrid.BackColor = System.Drawing.Color.White;
            this.ProductGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProductGrid.EnableSort = true;
            this.ProductGrid.Location = new System.Drawing.Point(0, 0);
            this.ProductGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ProductGrid.Name = "ProductGrid";
            this.ProductGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.ProductGrid.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.ProductGrid.Size = new System.Drawing.Size(771, 418);
            this.ProductGrid.TabIndex = 1;
            this.ProductGrid.TabStop = true;
            this.ProductGrid.ToolTipText = "";
            // 
            // ProductListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ProductListView";
            this.Size = new System.Drawing.Size(771, 446);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Pager pager1;
        private SourceGrid.Grid ProductGrid;
    }
}
