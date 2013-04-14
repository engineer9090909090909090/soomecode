namespace AliHelper
{
    partial class TestForm
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
            this.myItemsList1 = new AliHelper.MyItem.MyItemsListView();
            this.SuspendLayout();
            // 
            // myItemsList1
            // 
            this.myItemsList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myItemsList1.Location = new System.Drawing.Point(0, 0);
            this.myItemsList1.Name = "myItemsList1";
            this.myItemsList1.Size = new System.Drawing.Size(821, 497);
            this.myItemsList1.TabIndex = 0;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 497);
            this.Controls.Add(this.myItemsList1);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private MyItem.MyItemsListView myItemsList1;



    }
}