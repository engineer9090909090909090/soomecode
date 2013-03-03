namespace AliHelper
{
    partial class OrderTrackForm
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
            this.Canncel = new System.Windows.Forms.Button();
            this.Confirm = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.IsClosed = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Status = new System.Windows.Forms.ComboBox();
            this.TrackingDate = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.Description = new System.Windows.Forms.TextBox();
            this.Tracker = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Canncel
            // 
            this.Canncel.Location = new System.Drawing.Point(369, 300);
            this.Canncel.Name = "Canncel";
            this.Canncel.Size = new System.Drawing.Size(75, 23);
            this.Canncel.TabIndex = 42;
            this.Canncel.Text = "取消(&C)";
            this.Canncel.UseVisualStyleBackColor = true;
            this.Canncel.Click += new System.EventHandler(this.Canncel_Click);
            // 
            // Confirm
            // 
            this.Confirm.Location = new System.Drawing.Point(220, 301);
            this.Confirm.Name = "Confirm";
            this.Confirm.Size = new System.Drawing.Size(75, 23);
            this.Confirm.TabIndex = 41;
            this.Confirm.Text = "确认(&S)";
            this.Confirm.UseVisualStyleBackColor = true;
            this.Confirm.Click += new System.EventHandler(this.Confirm_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.IsClosed);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.Status);
            this.groupBox1.Controls.Add(this.TrackingDate);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.Description);
            this.groupBox1.Controls.Add(this.Tracker);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(632, 282);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            // 
            // IsClosed
            // 
            this.IsClosed.AutoSize = true;
            this.IsClosed.Location = new System.Drawing.Point(514, 246);
            this.IsClosed.Name = "IsClosed";
            this.IsClosed.Size = new System.Drawing.Size(96, 16);
            this.IsClosed.TabIndex = 36;
            this.IsClosed.Text = "是否关闭订单";
            this.IsClosed.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(21, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "时    间";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(21, 201);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "跟 踪 人";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(21, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 25;
            this.label6.Text = "订单描述";
            // 
            // Status
            // 
            this.Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Status.FormattingEnabled = true;
            this.Status.Location = new System.Drawing.Point(78, 242);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(152, 20);
            this.Status.TabIndex = 34;
            // 
            // TrackingDate
            // 
            this.TrackingDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TrackingDate.Location = new System.Drawing.Point(76, 23);
            this.TrackingDate.Name = "TrackingDate";
            this.TrackingDate.Size = new System.Drawing.Size(154, 21);
            this.TrackingDate.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(21, 246);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 33;
            this.label8.Text = "订单状态";
            // 
            // Description
            // 
            this.Description.Location = new System.Drawing.Point(76, 63);
            this.Description.Multiline = true;
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(534, 115);
            this.Description.TabIndex = 19;
            // 
            // Tracker
            // 
            this.Tracker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Tracker.FormattingEnabled = true;
            this.Tracker.Location = new System.Drawing.Point(77, 196);
            this.Tracker.Name = "Tracker";
            this.Tracker.Size = new System.Drawing.Size(153, 20);
            this.Tracker.TabIndex = 26;
            // 
            // OrderTrackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 333);
            this.Controls.Add(this.Canncel);
            this.Controls.Add(this.Confirm);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderTrackForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "跟进订单";
            this.Load += new System.EventHandler(this.OrderTrackForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Canncel;
        private System.Windows.Forms.Button Confirm;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox Status;
        private System.Windows.Forms.DateTimePicker TrackingDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox Description;
        private System.Windows.Forms.ComboBox Tracker;
        private System.Windows.Forms.CheckBox IsClosed;
    }
}