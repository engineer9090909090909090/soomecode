namespace AliHelper
{
    partial class MyItemsListView
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
            MyItemManager.OnEditProductEvent -= new NewEditItemEvent(MyItemManager_OnEditProductEvent);
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
            this.Model = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FinDetailQueryBtn = new System.Windows.Forms.Button();
            this.ProductName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.MyItemGrid = new SourceGrid.Grid();
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
            this.tableLayoutPanel1.Controls.Add(this.MyItemGrid, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pager, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(783, 482);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // FinDetailGroup
            // 
            this.FinDetailGroup.Caption = "查询条件";
            this.FinDetailGroup.Controls.Add(this.Model);
            this.FinDetailGroup.Controls.Add(this.label1);
            this.FinDetailGroup.Controls.Add(this.FinDetailQueryBtn);
            this.FinDetailGroup.Controls.Add(this.ProductName);
            this.FinDetailGroup.Controls.Add(this.label6);
            this.FinDetailGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FinDetailGroup.ExpandedHeight = 100;
            this.FinDetailGroup.HeaderContextMenuStrip = null;
            this.FinDetailGroup.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2003Silver;
            this.FinDetailGroup.Location = new System.Drawing.Point(1, 1);
            this.FinDetailGroup.Margin = new System.Windows.Forms.Padding(0);
            this.FinDetailGroup.Name = "FinDetailGroup";
            this.FinDetailGroup.Padding = new System.Windows.Forms.Padding(1, 22, 1, 1);
            this.FinDetailGroup.Size = new System.Drawing.Size(781, 50);
            this.FinDetailGroup.TabIndex = 2;
            this.FinDetailGroup.Text = "查询条件";
            // 
            // Model
            // 
            this.Model.Location = new System.Drawing.Point(319, 24);
            this.Model.Name = "Model";
            this.Model.Size = new System.Drawing.Size(122, 21);
            this.Model.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(267, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "产品型号";
            // 
            // FinDetailQueryBtn
            // 
            this.FinDetailQueryBtn.Location = new System.Drawing.Point(650, 23);
            this.FinDetailQueryBtn.Name = "FinDetailQueryBtn";
            this.FinDetailQueryBtn.Size = new System.Drawing.Size(70, 23);
            this.FinDetailQueryBtn.TabIndex = 2;
            this.FinDetailQueryBtn.Text = "查询(&Q)";
            this.FinDetailQueryBtn.UseVisualStyleBackColor = true;
            this.FinDetailQueryBtn.Click += new System.EventHandler(this.FinDetailQueryBtn_Click);
            // 
            // ProductName
            // 
            this.ProductName.Location = new System.Drawing.Point(83, 24);
            this.ProductName.Name = "ProductName";
            this.ProductName.Size = new System.Drawing.Size(155, 21);
            this.ProductName.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(30, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "产品标题";
            // 
            // MyItemGrid
            // 
            this.MyItemGrid.AutoSize = true;
            this.MyItemGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MyItemGrid.EnableSort = true;
            this.MyItemGrid.Location = new System.Drawing.Point(1, 52);
            this.MyItemGrid.Margin = new System.Windows.Forms.Padding(0);
            this.MyItemGrid.Name = "MyItemGrid";
            this.MyItemGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.MyItemGrid.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.MyItemGrid.Size = new System.Drawing.Size(781, 398);
            this.MyItemGrid.TabIndex = 3;
            this.MyItemGrid.TabStop = true;
            this.MyItemGrid.ToolTipText = "";
            // 
            // pager
            // 
            this.pager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pager.Location = new System.Drawing.Point(4, 454);
            this.pager.Name = "pager";
            this.pager.RecordCount = 0;
            this.pager.ShowPageSizeDropdown = true;
            this.pager.Size = new System.Drawing.Size(775, 24);
            this.pager.TabIndex = 4;
            this.pager.PageIndexChanged += new System.EventHandler(this.pager_PageIndexChanged);
            // 
            // MyItemsListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MyItemsListView";
            this.Size = new System.Drawing.Size(783, 482);
            this.Load += new System.EventHandler(this.MyItemsListView_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FinDetailGroup)).EndInit();
            this.FinDetailGroup.ResumeLayout(false);
            this.FinDetailGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private SourceGrid.Grid MyItemGrid;
        private Guifreaks.NavigationBar.NaviGroup FinDetailGroup;
        private System.Windows.Forms.Button FinDetailQueryBtn;
        private System.Windows.Forms.TextBox ProductName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Model;
        private System.Windows.Forms.Label label1;
        private Pager pager;
    }
}
