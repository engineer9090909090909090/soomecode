namespace AliHelper.MyItem
{
    partial class PriceCateForm
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
            this.CateName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.UsePrice1 = new System.Windows.Forms.CheckBox();
            this.Price1Name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.UsePrice2 = new System.Windows.Forms.CheckBox();
            this.UsePrice3 = new System.Windows.Forms.CheckBox();
            this.UsePrice4 = new System.Windows.Forms.CheckBox();
            this.UsePrice5 = new System.Windows.Forms.CheckBox();
            this.Price1Val = new AliHelper.NumericTextbox();
            this.Price2Name = new System.Windows.Forms.TextBox();
            this.Price3Name = new System.Windows.Forms.TextBox();
            this.Price4Name = new System.Windows.Forms.TextBox();
            this.Price5Name = new System.Windows.Forms.TextBox();
            this.Price2Val = new AliHelper.NumericTextbox();
            this.Price3Val = new AliHelper.NumericTextbox();
            this.Price4Val = new AliHelper.NumericTextbox();
            this.Price5Val = new AliHelper.NumericTextbox();
            this.ConfirmButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "价格种类名称";
            // 
            // CateName
            // 
            this.CateName.Location = new System.Drawing.Point(111, 28);
            this.CateName.Name = "CateName";
            this.CateName.Size = new System.Drawing.Size(244, 21);
            this.CateName.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.CateName);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(692, 353);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.Controls.Add(this.label4, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.UsePrice1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.Price1Name, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.UsePrice2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.UsePrice3, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.UsePrice4, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.UsePrice5, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.Price1Val, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.Price2Name, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.Price3Name, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.Price4Name, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.Price5Name, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.Price2Val, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.Price3Val, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.Price4Val, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.Price5Val, 3, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(25, 66);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(658, 264);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(429, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "价格值";
            // 
            // UsePrice1
            // 
            this.UsePrice1.AutoSize = true;
            this.UsePrice1.Location = new System.Drawing.Point(68, 23);
            this.UsePrice1.Name = "UsePrice1";
            this.UsePrice1.Size = new System.Drawing.Size(48, 16);
            this.UsePrice1.TabIndex = 2;
            this.UsePrice1.Text = "启用";
            this.UsePrice1.UseVisualStyleBackColor = true;
            // 
            // Price1Name
            // 
            this.Price1Name.Location = new System.Drawing.Point(199, 23);
            this.Price1Name.Name = "Price1Name";
            this.Price1Name.Size = new System.Drawing.Size(197, 21);
            this.Price1Name.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "是否启用";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(199, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "价格名称";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "价格2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "价格3";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 170);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "价格4";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 220);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "价格5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "价格1";
            // 
            // UsePrice2
            // 
            this.UsePrice2.AutoSize = true;
            this.UsePrice2.Location = new System.Drawing.Point(68, 73);
            this.UsePrice2.Name = "UsePrice2";
            this.UsePrice2.Size = new System.Drawing.Size(48, 16);
            this.UsePrice2.TabIndex = 12;
            this.UsePrice2.Text = "启用";
            this.UsePrice2.UseVisualStyleBackColor = true;
            // 
            // UsePrice3
            // 
            this.UsePrice3.AutoSize = true;
            this.UsePrice3.Location = new System.Drawing.Point(68, 123);
            this.UsePrice3.Name = "UsePrice3";
            this.UsePrice3.Size = new System.Drawing.Size(48, 16);
            this.UsePrice3.TabIndex = 13;
            this.UsePrice3.Text = "启用";
            this.UsePrice3.UseVisualStyleBackColor = true;
            // 
            // UsePrice4
            // 
            this.UsePrice4.AutoSize = true;
            this.UsePrice4.Location = new System.Drawing.Point(68, 173);
            this.UsePrice4.Name = "UsePrice4";
            this.UsePrice4.Size = new System.Drawing.Size(48, 16);
            this.UsePrice4.TabIndex = 14;
            this.UsePrice4.Text = "启用";
            this.UsePrice4.UseVisualStyleBackColor = true;
            // 
            // UsePrice5
            // 
            this.UsePrice5.AutoSize = true;
            this.UsePrice5.Location = new System.Drawing.Point(68, 223);
            this.UsePrice5.Name = "UsePrice5";
            this.UsePrice5.Size = new System.Drawing.Size(48, 16);
            this.UsePrice5.TabIndex = 15;
            this.UsePrice5.Text = "启用";
            this.UsePrice5.UseVisualStyleBackColor = true;
            // 
            // Price1Val
            // 
            this.Price1Val.CommaFormat = false;
            this.Price1Val.CurrencyFormat = false;
            this.Price1Val.DecimalPrecision = 3;
            this.Price1Val.Location = new System.Drawing.Point(429, 23);
            this.Price1Val.Name = "Price1Val";
            this.Price1Val.PercentFormat = false;
            this.Price1Val.ReadOnly = false;
            this.Price1Val.Size = new System.Drawing.Size(153, 24);
            this.Price1Val.TabIndex = 16;
            this.Price1Val.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // Price2Name
            // 
            this.Price2Name.Location = new System.Drawing.Point(199, 73);
            this.Price2Name.Name = "Price2Name";
            this.Price2Name.Size = new System.Drawing.Size(197, 21);
            this.Price2Name.TabIndex = 17;
            // 
            // Price3Name
            // 
            this.Price3Name.Location = new System.Drawing.Point(199, 123);
            this.Price3Name.Name = "Price3Name";
            this.Price3Name.Size = new System.Drawing.Size(197, 21);
            this.Price3Name.TabIndex = 18;
            // 
            // Price4Name
            // 
            this.Price4Name.Location = new System.Drawing.Point(199, 173);
            this.Price4Name.Name = "Price4Name";
            this.Price4Name.Size = new System.Drawing.Size(197, 21);
            this.Price4Name.TabIndex = 19;
            // 
            // Price5Name
            // 
            this.Price5Name.Location = new System.Drawing.Point(199, 223);
            this.Price5Name.Name = "Price5Name";
            this.Price5Name.Size = new System.Drawing.Size(197, 21);
            this.Price5Name.TabIndex = 20;
            // 
            // Price2Val
            // 
            this.Price2Val.CommaFormat = false;
            this.Price2Val.CurrencyFormat = false;
            this.Price2Val.DecimalPrecision = 3;
            this.Price2Val.Location = new System.Drawing.Point(429, 73);
            this.Price2Val.Name = "Price2Val";
            this.Price2Val.PercentFormat = false;
            this.Price2Val.ReadOnly = false;
            this.Price2Val.Size = new System.Drawing.Size(153, 24);
            this.Price2Val.TabIndex = 21;
            this.Price2Val.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // Price3Val
            // 
            this.Price3Val.CommaFormat = false;
            this.Price3Val.CurrencyFormat = false;
            this.Price3Val.DecimalPrecision = 3;
            this.Price3Val.Location = new System.Drawing.Point(429, 123);
            this.Price3Val.Name = "Price3Val";
            this.Price3Val.PercentFormat = false;
            this.Price3Val.ReadOnly = false;
            this.Price3Val.Size = new System.Drawing.Size(153, 24);
            this.Price3Val.TabIndex = 22;
            this.Price3Val.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // Price4Val
            // 
            this.Price4Val.CommaFormat = false;
            this.Price4Val.CurrencyFormat = false;
            this.Price4Val.DecimalPrecision = 3;
            this.Price4Val.Location = new System.Drawing.Point(429, 173);
            this.Price4Val.Name = "Price4Val";
            this.Price4Val.PercentFormat = false;
            this.Price4Val.ReadOnly = false;
            this.Price4Val.Size = new System.Drawing.Size(153, 24);
            this.Price4Val.TabIndex = 23;
            this.Price4Val.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // Price5Val
            // 
            this.Price5Val.CommaFormat = false;
            this.Price5Val.CurrencyFormat = false;
            this.Price5Val.DecimalPrecision = 3;
            this.Price5Val.Location = new System.Drawing.Point(429, 223);
            this.Price5Val.Name = "Price5Val";
            this.Price5Val.PercentFormat = false;
            this.Price5Val.ReadOnly = false;
            this.Price5Val.Size = new System.Drawing.Size(153, 24);
            this.Price5Val.TabIndex = 24;
            this.Price5Val.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.Location = new System.Drawing.Point(462, 372);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.Size = new System.Drawing.Size(75, 23);
            this.ConfirmButton.TabIndex = 3;
            this.ConfirmButton.Text = "确定(&S)";
            this.ConfirmButton.UseVisualStyleBackColor = true;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(560, 372);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 4;
            this.CancelButton.Text = "取消(&C)";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // PriceCateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 407);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ConfirmButton);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PriceCateForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "编辑价格种类";
            this.Load += new System.EventHandler(this.PriceCateForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CateName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox UsePrice1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox Price1Name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox UsePrice2;
        private System.Windows.Forms.CheckBox UsePrice3;
        private System.Windows.Forms.CheckBox UsePrice4;
        private System.Windows.Forms.CheckBox UsePrice5;
        private NumericTextbox Price1Val;
        private System.Windows.Forms.TextBox Price2Name;
        private System.Windows.Forms.TextBox Price3Name;
        private System.Windows.Forms.TextBox Price4Name;
        private System.Windows.Forms.TextBox Price5Name;
        private NumericTextbox Price2Val;
        private NumericTextbox Price3Val;
        private NumericTextbox Price4Val;
        private NumericTextbox Price5Val;
        private System.Windows.Forms.Button ConfirmButton;
        private System.Windows.Forms.Button CancelButton;
    }
}