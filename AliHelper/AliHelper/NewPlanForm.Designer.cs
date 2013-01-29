namespace AliHelper
{
    partial class NewPlanForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.HourComboBox = new System.Windows.Forms.ComboBox();
            this.MintueComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "重发名称";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(103, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 21);
            this.textBox1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "重发时间";
            // 
            // HourComboBox
            // 
            this.HourComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.HourComboBox.FormattingEnabled = true;
            this.HourComboBox.Location = new System.Drawing.Point(104, 71);
            this.HourComboBox.Name = "HourComboBox";
            this.HourComboBox.Size = new System.Drawing.Size(94, 20);
            this.HourComboBox.TabIndex = 3;
            // 
            // MintueComboBox
            // 
            this.MintueComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MintueComboBox.FormattingEnabled = true;
            this.MintueComboBox.Location = new System.Drawing.Point(202, 71);
            this.MintueComboBox.Name = "MintueComboBox";
            this.MintueComboBox.Size = new System.Drawing.Size(99, 20);
            this.MintueComboBox.TabIndex = 4;
            // 
            // NewPlanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 198);
            this.Controls.Add(this.MintueComboBox);
            this.Controls.Add(this.HourComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "NewPlanForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "重发计划";
            this.Load += new System.EventHandler(this.NewPlanForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox HourComboBox;
        private System.Windows.Forms.ComboBox MintueComboBox;
    }
}