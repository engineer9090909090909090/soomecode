namespace AliHelper
{
    partial class CheckCodeForm
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
            this.CheckImage = new System.Windows.Forms.PictureBox();
            this.txtCheckCode = new System.Windows.Forms.TextBox();
            this.BtnConfirm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CheckImage)).BeginInit();
            this.SuspendLayout();
            // 
            // CheckImage
            // 
            this.CheckImage.Location = new System.Drawing.Point(29, 9);
            this.CheckImage.Name = "CheckImage";
            this.CheckImage.Size = new System.Drawing.Size(224, 37);
            this.CheckImage.TabIndex = 0;
            this.CheckImage.TabStop = false;
            this.CheckImage.Click += new System.EventHandler(this.CheckImage_Click);
            // 
            // txtCheckCode
            // 
            this.txtCheckCode.Location = new System.Drawing.Point(97, 57);
            this.txtCheckCode.Name = "txtCheckCode";
            this.txtCheckCode.Size = new System.Drawing.Size(93, 21);
            this.txtCheckCode.TabIndex = 1;
            this.txtCheckCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCheckCode_KeyDown);
            // 
            // BtnConfirm
            // 
            this.BtnConfirm.Location = new System.Drawing.Point(97, 95);
            this.BtnConfirm.Name = "BtnConfirm";
            this.BtnConfirm.Size = new System.Drawing.Size(93, 23);
            this.BtnConfirm.TabIndex = 2;
            this.BtnConfirm.Text = "确认";
            this.BtnConfirm.UseVisualStyleBackColor = true;
            this.BtnConfirm.Click += new System.EventHandler(this.BtnConfirm_Click);
            // 
            // CheckCodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 129);
            this.Controls.Add(this.BtnConfirm);
            this.Controls.Add(this.txtCheckCode);
            this.Controls.Add(this.CheckImage);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CheckCodeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "验证码";
            this.Load += new System.EventHandler(this.CheckCodeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CheckImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox CheckImage;
        private System.Windows.Forms.TextBox txtCheckCode;
        private System.Windows.Forms.Button BtnConfirm;
    }
}