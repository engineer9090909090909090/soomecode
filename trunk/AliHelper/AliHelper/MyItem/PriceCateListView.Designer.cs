namespace AliHelper
{
    partial class PriceCateListView
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
            MyItemManager.OnEditPriceCateEvent -= new NewEditItemEvent(MyItemManager_OnEditPriceCateEvent);
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
            this.PriceCateGrid = new SourceGrid.Grid();
            this.pager = new AliHelper.Pager();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.PriceCateGrid, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pager, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(829, 499);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // PriceCateGrid
            // 
            this.PriceCateGrid.AutoSize = true;
            this.PriceCateGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PriceCateGrid.EnableSort = true;
            this.PriceCateGrid.Location = new System.Drawing.Point(1, 1);
            this.PriceCateGrid.Margin = new System.Windows.Forms.Padding(0);
            this.PriceCateGrid.Name = "PriceCateGrid";
            this.PriceCateGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.PriceCateGrid.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.PriceCateGrid.Size = new System.Drawing.Size(827, 466);
            this.PriceCateGrid.TabIndex = 3;
            this.PriceCateGrid.TabStop = true;
            this.PriceCateGrid.ToolTipText = "";
            // 
            // pager
            // 
            this.pager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pager.Location = new System.Drawing.Point(4, 471);
            this.pager.Name = "pager";
            this.pager.RecordCount = 0;
            this.pager.ShowPageSizeDropdown = true;
            this.pager.Size = new System.Drawing.Size(821, 24);
            this.pager.TabIndex = 4;
            // 
            // PriceCateListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PriceCateListView";
            this.Size = new System.Drawing.Size(829, 499);
            this.Load += new System.EventHandler(this.PriceCateListView_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private SourceGrid.Grid PriceCateGrid;
        private Pager pager;
    }
}
