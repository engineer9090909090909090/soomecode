namespace AliHelper
{
    partial class Pager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pager));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnToPageIndex = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cmbCurrentPage = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lab2PageSize = new System.Windows.Forms.ToolStripLabel();
            this.cmbPageSize = new System.Windows.Forms.ToolStripComboBox();
            this.lab1PageSize = new System.Windows.Forms.ToolStripLabel();
            this.lab3PageSize = new System.Windows.Forms.ToolStripSeparator();
            this.btnFirst = new System.Windows.Forms.ToolStripButton();
            this.btnPrev = new System.Windows.Forms.ToolStripButton();
            this.txtCurrentPage = new System.Windows.Forms.ToolStripTextBox();
            this.btnNext = new System.Windows.Forms.ToolStripButton();
            this.btnLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblPager = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnToPageIndex,
            this.bindingNavigatorSeparator1,
            this.toolStripLabel1,
            this.cmbCurrentPage,
            this.toolStripLabel3,
            this.bindingNavigatorSeparator2,
            this.lab2PageSize,
            this.cmbPageSize,
            this.lab1PageSize,
            this.lab3PageSize,
            this.btnFirst,
            this.btnPrev,
            this.txtCurrentPage,
            this.btnNext,
            this.btnLast,
            this.toolStripSeparator1,
            this.lblPager});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(639, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnToPageIndex
            // 
            this.btnToPageIndex.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnToPageIndex.Image = ((System.Drawing.Image)(resources.GetObject("btnToPageIndex.Image")));
            this.btnToPageIndex.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnToPageIndex.Name = "btnToPageIndex";
            this.btnToPageIndex.Size = new System.Drawing.Size(23, 22);
            this.btnToPageIndex.Text = "Moved";
            this.btnToPageIndex.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.AutoSize = false;
            this.toolStripLabel1.Font = new System.Drawing.Font("宋体", 9F);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(20, 20);
            this.toolStripLabel1.Text = "页";
            // 
            // cmbCurrentPage
            // 
            this.cmbCurrentPage.AutoSize = false;
            this.cmbCurrentPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrentPage.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cmbCurrentPage.Name = "cmbCurrentPage";
            this.cmbCurrentPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbCurrentPage.Size = new System.Drawing.Size(45, 20);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.AutoSize = false;
            this.toolStripLabel3.Font = new System.Drawing.Font("宋体", 9F);
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(20, 20);
            this.toolStripLabel3.Text = "第";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // lab2PageSize
            // 
            this.lab2PageSize.AutoSize = false;
            this.lab2PageSize.Font = new System.Drawing.Font("宋体", 9F);
            this.lab2PageSize.Name = "lab2PageSize";
            this.lab2PageSize.Size = new System.Drawing.Size(20, 20);
            this.lab2PageSize.Text = "条";
            // 
            // cmbPageSize
            // 
            this.cmbPageSize.AutoSize = false;
            this.cmbPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPageSize.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cmbPageSize.Items.AddRange(new object[] {
            "20",
            "30",
            "50",
            "100",
            "200"});
            this.cmbPageSize.Name = "cmbPageSize";
            this.cmbPageSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbPageSize.Size = new System.Drawing.Size(45, 20);
            this.cmbPageSize.Visible = false;
            this.cmbPageSize.SelectedIndexChanged += new System.EventHandler(this.cmbPageSize_SelectedIndexChanged);
            // 
            // lab1PageSize
            // 
            this.lab1PageSize.AutoSize = false;
            this.lab1PageSize.Font = new System.Drawing.Font("宋体", 9F);
            this.lab1PageSize.Name = "lab1PageSize";
            this.lab1PageSize.Size = new System.Drawing.Size(31, 20);
            this.lab1PageSize.Text = "每页";
            // 
            // lab3PageSize
            // 
            this.lab3PageSize.Name = "lab3PageSize";
            this.lab3PageSize.Size = new System.Drawing.Size(6, 25);
            // 
            // btnFirst
            // 
            this.btnFirst.AutoSize = false;
            this.btnFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFirst.Image = ((System.Drawing.Image)(resources.GetObject("btnFirst.Image")));
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.RightToLeftAutoMirrorImage = true;
            this.btnFirst.Size = new System.Drawing.Size(25, 24);
            this.btnFirst.Text = "Moved to the first page";
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.AutoSize = false;
            this.btnPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrev.Image = ((System.Drawing.Image)(resources.GetObject("btnPrev.Image")));
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.RightToLeftAutoMirrorImage = true;
            this.btnPrev.Size = new System.Drawing.Size(25, 24);
            this.btnPrev.Text = "Move to previous page";
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // txtCurrentPage
            // 
            this.txtCurrentPage.AccessibleName = "位置";
            this.txtCurrentPage.AutoSize = false;
            this.txtCurrentPage.Margin = new System.Windows.Forms.Padding(1, 1, 1, 0);
            this.txtCurrentPage.Name = "txtCurrentPage";
            this.txtCurrentPage.Size = new System.Drawing.Size(30, 20);
            this.txtCurrentPage.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCurrentPage.ToolTipText = "Location";
            // 
            // btnNext
            // 
            this.btnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.Name = "btnNext";
            this.btnNext.RightToLeftAutoMirrorImage = true;
            this.btnNext.Size = new System.Drawing.Size(23, 22);
            this.btnNext.Text = "Move to next page";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.AutoSize = false;
            this.btnLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLast.Image = ((System.Drawing.Image)(resources.GetObject("btnLast.Image")));
            this.btnLast.Name = "btnLast";
            this.btnLast.RightToLeftAutoMirrorImage = true;
            this.btnLast.Size = new System.Drawing.Size(25, 24);
            this.btnLast.Text = "Moved to the last page";
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // lblPager
            // 
            this.lblPager.Name = "lblPager";
            this.lblPager.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblPager.Size = new System.Drawing.Size(227, 22);
            this.lblPager.Text = "当前{1}/{2}页,每页{3}条,总共{0}条记录";
            this.lblPager.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Pager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Name = "Pager";
            this.Size = new System.Drawing.Size(639, 26);
            this.Load += new System.EventHandler(this.WinFormPager_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel lblPager;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnFirst;
        private System.Windows.Forms.ToolStripButton btnPrev;
        private System.Windows.Forms.ToolStripTextBox txtCurrentPage;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton btnNext;
        private System.Windows.Forms.ToolStripButton btnLast;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox cmbCurrentPage;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton btnToPageIndex;
        private System.Windows.Forms.ToolStripLabel lab1PageSize;
        private System.Windows.Forms.ToolStripComboBox cmbPageSize;
        private System.Windows.Forms.ToolStripLabel lab2PageSize;
        private System.Windows.Forms.ToolStripSeparator lab3PageSize;

    }
}
