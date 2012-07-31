namespace SooMailer
{
    partial class ExcelImpForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.filePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.selectBtn = new System.Windows.Forms.Button();
            this.importBtn = new System.Windows.Forms.Button();
            this.errorMsg = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(63, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择Excel文件：";
            // 
            // filePath
            // 
            this.filePath.Enabled = false;
            this.filePath.Location = new System.Drawing.Point(62, 57);
            this.filePath.Name = "filePath";
            this.filePath.Size = new System.Drawing.Size(353, 21);
            this.filePath.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(49, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(503, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "提示：Excel文件的列头必需包含Email, Country, Buyer Name, Company, Subject, Date等列";
            // 
            // selectBtn
            // 
            this.selectBtn.Location = new System.Drawing.Point(421, 57);
            this.selectBtn.Name = "selectBtn";
            this.selectBtn.Size = new System.Drawing.Size(89, 23);
            this.selectBtn.TabIndex = 3;
            this.selectBtn.Text = "选择文件(&S)";
            this.selectBtn.UseVisualStyleBackColor = true;
            this.selectBtn.Click += new System.EventHandler(this.selectBtn_Click);
            // 
            // importBtn
            // 
            this.importBtn.Location = new System.Drawing.Point(235, 139);
            this.importBtn.Name = "importBtn";
            this.importBtn.Size = new System.Drawing.Size(78, 29);
            this.importBtn.TabIndex = 4;
            this.importBtn.Text = "导入(&I)";
            this.importBtn.UseVisualStyleBackColor = true;
            this.importBtn.Click += new System.EventHandler(this.importBtn_Click);
            // 
            // errorMsg
            // 
            this.errorMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorMsg.ForeColor = System.Drawing.Color.Red;
            this.errorMsg.Location = new System.Drawing.Point(0, 0);
            this.errorMsg.Name = "errorMsg";
            this.errorMsg.Size = new System.Drawing.Size(501, 54);
            this.errorMsg.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.InitialImage = global::SooMailer.Properties.Resources.progress;
            this.pictureBox1.Location = new System.Drawing.Point(421, 126);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(52, 51);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.errorMsg);
            this.panel1.Location = new System.Drawing.Point(51, 200);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(501, 54);
            this.panel1.TabIndex = 7;
            // 
            // ExcelImpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 266);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.importBtn);
            this.Controls.Add(this.selectBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.filePath);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExcelImpForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "从Excel导入";
            this.Load += new System.EventHandler(this.ExcelImpForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox filePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button selectBtn;
        private System.Windows.Forms.Button importBtn;
        private System.Windows.Forms.Label errorMsg;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
    }
}