namespace AliRank
{
    partial class ModifyWindow
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
            this.oldKeyword = new System.Windows.Forms.TextBox();
            this.NewKeyword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.confirmBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.ErrorMsg = new System.Windows.Forms.Label();
            this.ProductLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "原排名关键词：";
            // 
            // oldKeyword
            // 
            this.oldKeyword.Enabled = false;
            this.oldKeyword.Location = new System.Drawing.Point(159, 99);
            this.oldKeyword.Name = "oldKeyword";
            this.oldKeyword.ReadOnly = true;
            this.oldKeyword.Size = new System.Drawing.Size(207, 21);
            this.oldKeyword.TabIndex = 0;
            // 
            // NewKeyword
            // 
            this.NewKeyword.Location = new System.Drawing.Point(159, 148);
            this.NewKeyword.Name = "NewKeyword";
            this.NewKeyword.Size = new System.Drawing.Size(207, 21);
            this.NewKeyword.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "新排名关键词：";
            // 
            // confirmBtn
            // 
            this.confirmBtn.Location = new System.Drawing.Point(98, 204);
            this.confirmBtn.Name = "confirmBtn";
            this.confirmBtn.Size = new System.Drawing.Size(74, 27);
            this.confirmBtn.TabIndex = 4;
            this.confirmBtn.Text = "确定(&S)";
            this.confirmBtn.UseVisualStyleBackColor = true;
            this.confirmBtn.Click += new System.EventHandler(this.confirmBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(255, 204);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(74, 27);
            this.cancelBtn.TabIndex = 5;
            this.cancelBtn.Text = "取消(&C)";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // ErrorMsg
            // 
            this.ErrorMsg.AutoSize = true;
            this.ErrorMsg.Location = new System.Drawing.Point(97, 191);
            this.ErrorMsg.Name = "ErrorMsg";
            this.ErrorMsg.Size = new System.Drawing.Size(0, 12);
            this.ErrorMsg.TabIndex = 6;
            // 
            // ProductLabel
            // 
            this.ProductLabel.AutoSize = true;
            this.ProductLabel.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ProductLabel.Location = new System.Drawing.Point(111, 26);
            this.ProductLabel.Name = "ProductLabel";
            this.ProductLabel.Size = new System.Drawing.Size(0, 15);
            this.ProductLabel.TabIndex = 7;
            // 
            // ModifyWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 267);
            this.Controls.Add(this.ProductLabel);
            this.Controls.Add(this.ErrorMsg);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.confirmBtn);
            this.Controls.Add(this.NewKeyword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.oldKeyword);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModifyWindow";
            this.Text = "排名关键词修改";
            this.Load += new System.EventHandler(this.ModifyWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox oldKeyword;
        private System.Windows.Forms.TextBox NewKeyword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button confirmBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label ErrorMsg;
        private System.Windows.Forms.Label ProductLabel;
    }
}