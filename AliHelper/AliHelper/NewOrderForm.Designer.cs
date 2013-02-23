namespace AliHelper
{
    partial class NewOrderForm
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
            this.Status = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Remark = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SalesMan = new System.Windows.Forms.ComboBox();
            this.Description = new System.Windows.Forms.TextBox();
            this.BeginDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.OrderNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Confirm = new System.Windows.Forms.Button();
            this.Canncel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Status
            // 
            this.Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Status.FormattingEnabled = true;
            this.Status.Location = new System.Drawing.Point(91, 216);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(152, 20);
            this.Status.TabIndex = 34;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(34, 220);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 33;
            this.label8.Text = "订单状态";
            // 
            // Remark
            // 
            this.Remark.Location = new System.Drawing.Point(91, 258);
            this.Remark.Multiline = true;
            this.Remark.Name = "Remark";
            this.Remark.Size = new System.Drawing.Size(342, 73);
            this.Remark.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(34, 264);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 28;
            this.label3.Text = "备    注";
            // 
            // SalesMan
            // 
            this.SalesMan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SalesMan.FormattingEnabled = true;
            this.SalesMan.Location = new System.Drawing.Point(90, 170);
            this.SalesMan.Name = "SalesMan";
            this.SalesMan.Size = new System.Drawing.Size(153, 20);
            this.SalesMan.TabIndex = 26;
            // 
            // Description
            // 
            this.Description.Location = new System.Drawing.Point(89, 123);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(344, 21);
            this.Description.TabIndex = 19;
            // 
            // BeginDate
            // 
            this.BeginDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.BeginDate.Location = new System.Drawing.Point(89, 23);
            this.BeginDate.Name = "BeginDate";
            this.BeginDate.Size = new System.Drawing.Size(154, 21);
            this.BeginDate.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(34, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 25;
            this.label6.Text = "订单描述";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(34, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "业 务 员";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(34, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "开始时间";
            // 
            // OrderNo
            // 
            this.OrderNo.Location = new System.Drawing.Point(89, 75);
            this.OrderNo.Name = "OrderNo";
            this.OrderNo.Size = new System.Drawing.Size(154, 21);
            this.OrderNo.TabIndex = 35;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(34, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 36;
            this.label2.Text = "订单编号";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.OrderNo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.Status);
            this.groupBox1.Controls.Add(this.BeginDate);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.Description);
            this.groupBox1.Controls.Add(this.Remark);
            this.groupBox1.Controls.Add(this.SalesMan);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(7, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(468, 360);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            // 
            // Confirm
            // 
            this.Confirm.Location = new System.Drawing.Point(143, 370);
            this.Confirm.Name = "Confirm";
            this.Confirm.Size = new System.Drawing.Size(75, 23);
            this.Confirm.TabIndex = 38;
            this.Confirm.Text = "确认(&S)";
            this.Confirm.UseVisualStyleBackColor = true;
            this.Confirm.Click += new System.EventHandler(this.Confirm_Click);
            // 
            // Canncel
            // 
            this.Canncel.Location = new System.Drawing.Point(262, 370);
            this.Canncel.Name = "Canncel";
            this.Canncel.Size = new System.Drawing.Size(75, 23);
            this.Canncel.TabIndex = 39;
            this.Canncel.Text = "取消(&C)";
            this.Canncel.UseVisualStyleBackColor = true;
            this.Canncel.Click += new System.EventHandler(this.Canncel_Click);
            // 
            // NewOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 408);
            this.Controls.Add(this.Canncel);
            this.Controls.Add(this.Confirm);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewOrderForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "编辑订单";
            this.Load += new System.EventHandler(this.NewOrderForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox Status;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox Remark;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox SalesMan;
        private System.Windows.Forms.TextBox Description;
        private System.Windows.Forms.DateTimePicker BeginDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox OrderNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Confirm;
        private System.Windows.Forms.Button Canncel;
    }
}