using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soomes;

namespace AliHelper
{
    public partial class PriceCateForm : Form
    {
        MyItemManager manager;
        public PriceCate UpdatePriceCate { set; get; }
        public PriceCateForm()
        {
            InitializeComponent();
            manager = new MyItemManager();
        }

        private void PriceCateForm_Load(object sender, EventArgs e)
        {
            LoadUpdatePriceCate(UpdatePriceCate);
        }

        private void LoadUpdatePriceCate(PriceCate obj)
        {
            if (obj == null) return;
            this.Tag = obj;
            this.CateName.Text = obj.CateName;
            this.UsePrice1.Checked = obj.UsePrice1;
            this.Price1Name.Text = obj.Price1Name;
            if (obj.Price1Val > 0)
            {
                this.Price1Val.Text = obj.Price1Val.ToString("#,##0.00");
            }
            this.UsePrice2.Checked = obj.UsePrice2;
            this.Price2Name.Text = obj.Price2Name;
            if (obj.Price2Val > 0)
            {
                this.Price2Val.Text = obj.Price2Val.ToString("#,##0.00");
            }
            this.UsePrice3.Checked = obj.UsePrice3;
            this.Price3Name.Text = obj.Price3Name;
            if (obj.Price3Val > 0)
            {
                this.Price3Val.Text = obj.Price3Val.ToString("#,##0.00");
            }
            this.UsePrice4.Checked = obj.UsePrice4;
            this.Price4Name.Text = obj.Price4Name;
            if (obj.Price4Val > 0)
            {
                this.Price4Val.Text = obj.Price4Val.ToString("#,##0.00");
            }
            this.UsePrice5.Checked = obj.UsePrice5;
            this.Price5Name.Text = obj.Price5Name;
            if (obj.Price5Val > 0)
            {
                this.Price5Val.Text = obj.Price5Val.ToString("#,##0.00");
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CateName.Text))
            {
                MessageBox.Show("价格种类名称不能为空。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (UsePrice1.Checked && (string.IsNullOrEmpty(Price1Name.Text) || string.IsNullOrEmpty(Price1Val.Text)))
            {
                MessageBox.Show("价格1已经启用，价格名称与值必须填写。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (UsePrice2.Checked && (string.IsNullOrEmpty(Price2Name.Text) || string.IsNullOrEmpty(Price2Val.Text)))
            {
                MessageBox.Show("价格2已经启用，价格名称与值必须填写。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (UsePrice3.Checked && (string.IsNullOrEmpty(Price3Name.Text) || string.IsNullOrEmpty(Price3Val.Text)))
            {
                MessageBox.Show("价格3已经启用，价格名称与值必须填写。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (UsePrice4.Checked && (string.IsNullOrEmpty(Price4Name.Text) || string.IsNullOrEmpty(Price4Val.Text)))
            {
                MessageBox.Show("价格4已经启用，价格名称与值必须填写。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (UsePrice5.Checked && (string.IsNullOrEmpty(Price5Name.Text) || string.IsNullOrEmpty(Price5Val.Text)))
            {
                MessageBox.Show("价格5已经启用，价格名称与值必须填写。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.Tag == null)
            {
                UpdatePriceCate = new PriceCate();
            }
            UpdatePriceCate.CateName = this.CateName.Text;
            UpdatePriceCate.UsePrice1 = this.UsePrice1.Checked;
            UpdatePriceCate.Price1Name = this.Price1Name.Text;
            UpdatePriceCate.Price1Val = Convert.ToDouble(this.Price1Val.Text);

            UpdatePriceCate.UsePrice2 = this.UsePrice2.Checked;
            UpdatePriceCate.Price2Name = this.Price2Name.Text;
            UpdatePriceCate.Price2Val = Convert.ToDouble(this.Price2Val.Text);

            UpdatePriceCate.UsePrice3 = this.UsePrice3.Checked;
            UpdatePriceCate.Price3Name = this.Price3Name.Text;
            UpdatePriceCate.Price3Val = Convert.ToDouble(this.Price3Val.Text);

            UpdatePriceCate.UsePrice4 = this.UsePrice4.Checked;
            UpdatePriceCate.Price4Name = this.Price4Name.Text;
            UpdatePriceCate.Price4Val = Convert.ToDouble(this.Price4Val.Text);

            UpdatePriceCate.UsePrice5 = this.UsePrice5.Checked;
            UpdatePriceCate.Price5Name = this.Price5Name.Text;
            UpdatePriceCate.Price5Val = Convert.ToDouble(this.Price5Val.Text);
            UpdatePriceCate.Status = "A";
            manager.InsertOrUpdatePriceCate(UpdatePriceCate);
            this.Close();
        }

        
    }
}
