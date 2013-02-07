namespace AliHelper
{
    partial class MailView
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
            this.naviGroup1 = new Guifreaks.NavigationBar.NaviGroup(this.components);
            this.CountryTxt = new System.Windows.Forms.TextBox();
            this.SubjectTxt = new System.Windows.Forms.TextBox();
            this.EmailTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.MailerTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.naviGroup1)).BeginInit();
            this.naviGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // naviGroup1
            // 
            this.naviGroup1.Caption = "查询条件";
            this.naviGroup1.Controls.Add(this.CountryTxt);
            this.naviGroup1.Controls.Add(this.SubjectTxt);
            this.naviGroup1.Controls.Add(this.EmailTxt);
            this.naviGroup1.Controls.Add(this.label4);
            this.naviGroup1.Controls.Add(this.MailerTxt);
            this.naviGroup1.Controls.Add(this.label3);
            this.naviGroup1.Controls.Add(this.label2);
            this.naviGroup1.Controls.Add(this.label1);
            this.naviGroup1.Dock = System.Windows.Forms.DockStyle.Top;
            this.naviGroup1.ExpandedHeight = 68;
            this.naviGroup1.HeaderContextMenuStrip = null;
            this.naviGroup1.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2003Silver;
            this.naviGroup1.Location = new System.Drawing.Point(0, 0);
            this.naviGroup1.Margin = new System.Windows.Forms.Padding(0);
            this.naviGroup1.Name = "naviGroup1";
            this.naviGroup1.Padding = new System.Windows.Forms.Padding(1, 22, 1, 1);
            this.naviGroup1.Size = new System.Drawing.Size(739, 68);
            this.naviGroup1.TabIndex = 0;
            this.naviGroup1.Text = "naviGroup1";
            // 
            // CountryTxt
            // 
            this.CountryTxt.Location = new System.Drawing.Point(535, 30);
            this.CountryTxt.Name = "CountryTxt";
            this.CountryTxt.Size = new System.Drawing.Size(134, 21);
            this.CountryTxt.TabIndex = 7;
            this.CountryTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // SubjectTxt
            // 
            this.SubjectTxt.Location = new System.Drawing.Point(380, 29);
            this.SubjectTxt.Name = "SubjectTxt";
            this.SubjectTxt.Size = new System.Drawing.Size(114, 21);
            this.SubjectTxt.TabIndex = 6;
            this.SubjectTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // EmailTxt
            // 
            this.EmailTxt.Location = new System.Drawing.Point(210, 29);
            this.EmailTxt.Name = "EmailTxt";
            this.EmailTxt.Size = new System.Drawing.Size(123, 21);
            this.EmailTxt.TabIndex = 5;
            this.EmailTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(180, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "邮箱";
            // 
            // MailerTxt
            // 
            this.MailerTxt.Location = new System.Drawing.Point(49, 29);
            this.MailerTxt.Name = "MailerTxt";
            this.MailerTxt.Size = new System.Drawing.Size(125, 21);
            this.MailerTxt.TabIndex = 3;
            this.MailerTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(500, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "国家";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(348, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "主题";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(6, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "发件人";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 68);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(739, 356);
            this.dataGridView1.TabIndex = 1;
            // 
            // MailView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.naviGroup1);
            this.Name = "MailView";
            this.Size = new System.Drawing.Size(739, 424);
            ((System.ComponentModel.ISupportInitialize)(this.naviGroup1)).EndInit();
            this.naviGroup1.ResumeLayout(false);
            this.naviGroup1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guifreaks.NavigationBar.NaviGroup naviGroup1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox EmailTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox MailerTxt;
        private System.Windows.Forms.TextBox CountryTxt;
        private System.Windows.Forms.TextBox SubjectTxt;

    }
}
