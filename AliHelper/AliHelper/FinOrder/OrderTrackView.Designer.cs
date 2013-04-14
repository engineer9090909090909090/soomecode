
namespace AliHelper
{
    partial class OrderTrackView
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
            FinOrderManager.OnEditTrackingEvent -= new NewEditItemEvent(OnEditTrackingEvent);
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.FinDetailGroup = new Guifreaks.NavigationBar.NaviGroup(this.components);
            this.BeginDateTo = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.SalesMan = new System.Windows.Forms.ComboBox();
            this.FinDetailQueryBtn = new System.Windows.Forms.Button();
            this.BeginDateForm = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.OrderGrid = new SourceGrid.Grid();
            this.OrderNo = new System.Windows.Forms.TextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.TrackGrid = new SourceGrid.Grid();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FinDetailGroup)).BeginInit();
            this.FinDetailGroup.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl);
            this.splitContainer1.Size = new System.Drawing.Size(883, 571);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.TabIndex = 0;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(883, 300);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // FinDetailGroup
            // 
            this.FinDetailGroup.Caption = "查询条件";
            this.FinDetailGroup.Controls.Add(this.BeginDateTo);
            this.FinDetailGroup.Controls.Add(this.label5);
            this.FinDetailGroup.Controls.Add(this.SalesMan);
            this.FinDetailGroup.Controls.Add(this.FinDetailQueryBtn);
            this.FinDetailGroup.Controls.Add(this.OrderNo);
            this.FinDetailGroup.Controls.Add(this.BeginDateForm);
            this.FinDetailGroup.Controls.Add(this.label6);
            this.FinDetailGroup.Controls.Add(this.label7);
            this.FinDetailGroup.Controls.Add(this.label1);
            this.FinDetailGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FinDetailGroup.ExpandedHeight = 70;
            this.FinDetailGroup.HeaderContextMenuStrip = null;
            this.FinDetailGroup.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2003Silver;
            this.FinDetailGroup.Location = new System.Drawing.Point(1, 1);
            this.FinDetailGroup.Margin = new System.Windows.Forms.Padding(0);
            this.FinDetailGroup.Name = "FinDetailGroup";
            this.FinDetailGroup.Padding = new System.Windows.Forms.Padding(1, 22, 1, 1);
            this.FinDetailGroup.Size = new System.Drawing.Size(881, 70);
            this.FinDetailGroup.TabIndex = 2;
            this.FinDetailGroup.Text = "查询条件";
            // 
            // BeginDateTo
            // 
            this.BeginDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.BeginDateTo.Location = new System.Drawing.Point(252, 33);
            this.BeginDateTo.Name = "BeginDateTo";
            this.BeginDateTo.Size = new System.Drawing.Size(85, 21);
            this.BeginDateTo.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(187, 37);
            this.label5.Margin = new System.Windows.Forms.Padding(10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "开始时间到";
            // 
            // SalesMan
            // 
            this.SalesMan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SalesMan.FormattingEnabled = true;
            this.SalesMan.Location = new System.Drawing.Point(593, 33);
            this.SalesMan.Name = "SalesMan";
            this.SalesMan.Size = new System.Drawing.Size(121, 20);
            this.SalesMan.TabIndex = 10;
            // 
            // FinDetailQueryBtn
            // 
            this.FinDetailQueryBtn.Location = new System.Drawing.Point(744, 32);
            this.FinDetailQueryBtn.Name = "FinDetailQueryBtn";
            this.FinDetailQueryBtn.Size = new System.Drawing.Size(70, 23);
            this.FinDetailQueryBtn.TabIndex = 8;
            this.FinDetailQueryBtn.Text = "查询(&Q)";
            this.FinDetailQueryBtn.UseVisualStyleBackColor = true;
            this.FinDetailQueryBtn.Click += new System.EventHandler(this.FinDetailQueryBtn_Click);
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
            this.label6.Location = new System.Drawing.Point(357, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "订单号";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(540, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "业 务 员";
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
            this.OrderGrid.Location = new System.Drawing.Point(1, 72);
            this.OrderGrid.Margin = new System.Windows.Forms.Padding(0);
            this.OrderGrid.Name = "OrderGrid";
            this.OrderGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.OrderGrid.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.OrderGrid.Size = new System.Drawing.Size(881, 227);
            this.OrderGrid.TabIndex = 3;
            this.OrderGrid.TabStop = true;
            this.OrderGrid.ToolTipText = "";
            // 
            // OrderNo
            // 
            this.OrderNo.Location = new System.Drawing.Point(402, 34);
            this.OrderNo.Name = "OrderNo";
            this.OrderNo.Size = new System.Drawing.Size(122, 21);
            this.OrderNo.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(883, 267);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.TrackGrid);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(875, 242);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "订单跟踪";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // TrackGrid
            // 
            this.TrackGrid.AutoSize = true;
            this.TrackGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrackGrid.EnableSort = true;
            this.TrackGrid.Location = new System.Drawing.Point(3, 3);
            this.TrackGrid.Margin = new System.Windows.Forms.Padding(0);
            this.TrackGrid.Name = "TrackGrid";
            this.TrackGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.TrackGrid.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.TrackGrid.Size = new System.Drawing.Size(869, 236);
            this.TrackGrid.TabIndex = 4;
            this.TrackGrid.TabStop = true;
            this.TrackGrid.ToolTipText = "";
            // 
            // OrderTrackView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "OrderTrackView";
            this.Size = new System.Drawing.Size(883, 571);
            this.Load += new System.EventHandler(this.OrderTrackView_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FinDetailGroup)).EndInit();
            this.FinDetailGroup.ResumeLayout(false);
            this.FinDetailGroup.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Guifreaks.NavigationBar.NaviGroup FinDetailGroup;
        private System.Windows.Forms.DateTimePicker BeginDateTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox SalesMan;
        private System.Windows.Forms.Button FinDetailQueryBtn;
        private System.Windows.Forms.TextBox OrderNo;
        private System.Windows.Forms.DateTimePicker BeginDateForm;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private SourceGrid.Grid OrderGrid;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private SourceGrid.Grid TrackGrid;
    }
}
