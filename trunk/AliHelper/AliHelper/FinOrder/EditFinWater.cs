using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soomes;
using AliHelper.Bussness;
using System.Runtime.InteropServices;

namespace AliHelper
{
    public partial class EditFinWater : Form
    {
        FinOrderManager finOrderManager;
        public Finance UpdateFinance { get; set; }
        public EditFinWater()
        {
            InitializeComponent();
            finOrderManager = new FinOrderManager();
        }

        private void EditFinWater_Load(object sender, EventArgs e)
        {
            this.ItemType.DisplayMember = "Label";
            this.ItemType.ValueMember = "Key";
            this.EventType.DisplayMember = "Label";
            this.EventType.ValueMember = "Key";
            this.Curreny.DisplayMember = "Label";
            this.Curreny.ValueMember = "Key";
            this.Association.DisplayMember = "Label";
            this.Association.ValueMember = "Key";
            this.Account.DisplayMember = "Label";
            this.Account.ValueMember = "Key";
            this.DetailAssociation.DisplayMember = "Label";
            this.DetailAssociation.ValueMember = "Key";
            this.ItemType.DataSource = finOrderManager.GetAppDicOptions(Constants.BussnessType);
            this.EventType.DataSource = finOrderManager.GetAppDicOptions(Constants.DebitCredit);
            this.Curreny.DataSource = finOrderManager.GetAppDicOptions(Constants.CurrencyType);
            this.Association.DataSource = finOrderManager.GetAppDicOptions(Constants.Employee);
            this.Account.DataSource = finOrderManager.GetAppDicOptions(Constants.RecivePaymentAccounts);
            
            this.DetailAssociation.DataSource = finOrderManager.GetAppDicOptions(Constants.Employee);
            LoadEditData(UpdateFinance);
        }

        public void LoadEditData(Finance finance)
        {
            if (finance == null) return;
            this.Tag = finance;
            this.Description.Text = finance.Description;
            this.FinDate.Text = finance.FinDate;
            this.Remark.Text = finance.Remark;
            this.Association.Text = finance.Association;
            this.ReceivePaymentor.Text = finance.ReceivePaymentor;
            this.Customer.Text = finance.Customer;
            this.ReferenceNo.Text = finance.ReferenceNo;
            this.TotalAmount.Text = "￥" + finance.TotalAmount.ToString("#,##0.00");
            this.Amount.Text = finance.Amount.ToString("#,##0.00");
            this.Rate.Text = finance.Rate.ToString("#,##0.0000");
            AliHelperUtils.LoadAppDicComboBoxValue(this.ItemType, finance.ItemType);
            AliHelperUtils.LoadAppDicComboBoxValue(this.EventType, finance.EventType);
            AliHelperUtils.LoadAppDicComboBoxValue(this.Curreny, finance.Currency);
            AliHelperUtils.LoadAppDicComboBoxValue(this.Account, finance.Account);
            AliHelperUtils.LoadAppDicComboBoxValue(this.Association, finance.Association);
            foreach (FinDetails detail in finance.Details)
            {
                int index = DetailView.Rows.Add();
                DetailView.Rows[index].Cells["DetailDescription"].Value = detail.Description;
                DetailView.Rows[index].Cells["DetailOrderNo"].Value = detail.OrderNo;
                DetailView.Rows[index].Cells["DetailAssociation"].Value = detail.Association;
                DetailView.Rows[index].Cells["DetailRemark"].Value = detail.Remark;
                DetailView.Rows[index].Cells["DetailAmount"].Value = detail.Amount.ToString("#,##0.00");
                DetailView.Rows[index].Cells["DetailTotalAmount"].Value = "￥"+detail.TotalAmount.ToString("#,##0.00");
            }
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            Finance finance;
            if (this.Tag == null)
            {
                finance = new Finance();
            }
            else
            {
                finance = (Finance)this.Tag;
            }
            finance.Description = this.Description.Text.Trim();
            finance.EventType = ((AppDic)this.EventType.SelectedItem).Key;
            finance.FinDate = this.FinDate.Value.ToString(Constants.DateFormat);
            finance.ItemType = ((AppDic)this.ItemType.SelectedItem).Key; ;
            finance.Remark = this.Remark.Text.Trim();
            finance.Association = ((AppDic)this.Association.SelectedItem).Key;
            finance.Rate = Convert.ToDouble(this.Rate.Text.Trim());
            finance.Currency = ((AppDic)this.Curreny.SelectedItem).Key;
            finance.Amount = Convert.ToDouble(this.Amount.Text);
            finance.ReceivePaymentor = this.ReceivePaymentor.Text.Trim();
            finance.Account = ((AppDic)this.Account.SelectedItem).Key;
            finance.ReferenceNo = this.ReferenceNo.Text.Trim();
            finance.Customer = this.Customer.Text.Trim();
            if (finance.Amount == 0)
            {
                return;
            }
            if (string.IsNullOrEmpty(finance.Description))
            {
                return;
            }
            if (string.IsNullOrEmpty(finance.Association))
            {
                return;
            }
            bool HasError = false;
            List<FinDetails> details = new List<FinDetails>();
            
            foreach (DataGridViewRow row in DetailView.Rows)
            { 
                string desc = (string)row.Cells["DetailDescription"].Value;
                if (string.IsNullOrEmpty(desc))
                {
                    HasError = true;
                    row.Cells["DetailDescription"].ErrorText = "不能为空";
                }
                string orderNo = (string)row.Cells["DetailOrderNo"].Value;
                if (string.IsNullOrEmpty(orderNo))
                {
                    HasError = true;
                    row.Cells["DetailOrderNo"].ErrorText = "不能为空";
                }
                double amount = Convert.ToDouble(row.Cells["DetailAmount"].Value);
                if (amount == 0.00)
                {
                    HasError = true;
                    row.Cells["DetailAmount"].ErrorText = "不能为零";
                }
                string association = (string)row.Cells["DetailAssociation"].Value;
                if (string.IsNullOrEmpty(association))
                {
                    HasError = true;
                    row.Cells["DetailAssociation"].ErrorText = "不能为空";
                }
                string remark = (string)row.Cells["DetailRemark"].Value;
                FinDetails detail = new FinDetails();
                detail.FinId = finance.FinId;
                detail.FinDate = finance.FinDate;
                detail.ItemType = finance.ItemType;
                detail.EventType = finance.EventType;
                detail.Currency = finance.Currency;
                detail.Rate = finance.Rate;
                detail.Description = desc;
                detail.OrderNo = orderNo;
                detail.Amount = amount;
                detail.Association = association;
                detail.Remark = remark;
                details.Add(detail);
            }
            if (HasError)
            {
                return;
            }
            finance.Details = details;
            finOrderManager.InsertOrUpdateFinance(finance);
            this.Close();
        }

        private void Cannel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AmountTxt_Leave(object sender, EventArgs e)
        {
            double amount = Convert.ToDouble(this.Amount.Text);
            double rate = Convert.ToDouble(this.Rate.Text);
            double total = amount * rate;
            this.TotalAmount.Text = "￥" + total.ToString("#,##0.00");
            if (DetailView.Rows.Count == 0)
            {
                int index = this.DetailView.Rows.Add();
                List<AppDic> employees = finOrderManager.GetAppDicOptions(Constants.Employee);
                if (employees != null)
                    DetailView.Rows[index].Cells["DetailAssociation"].Value = employees[0].Key;
                DetailView.Rows[index].Cells["DetailDescription"].Value = this.Description.Text;
                DetailView.Rows[index].Cells["DetailAmount"].Value = this.Amount.Text;
                DetailView.Rows[index].Cells["DetailTotalAmount"].Value = this.TotalAmount.Text;
            }
            else if (DetailView.Rows.Count == 1)
            {
                DetailView.Rows[0].Cells["DetailDescription"].Value = this.Description.Text;
                DetailView.Rows[0].Cells["DetailAmount"].Value = this.Amount.Text;
                DetailView.Rows[0].Cells["DetailTotalAmount"].Value = this.TotalAmount.Text;
            }
        }

        private void CurrenyTxt_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Curreny.SelectedItem != null)
            {
                this.Rate.ReadOnly = false;
                string curreny = ((AppDic)Curreny.SelectedItem).Key;
                if (curreny.ToUpper() == "RMB" || curreny.ToUpper() == "CNY")
                {
                    this.Rate.Text = "1.0000";
                    this.Rate.ReadOnly = true;
                }
                else if (curreny.ToUpper() == "USD")
                {
                    this.Rate.Text = "6.2000";
                }
            }
        }

        private void DeleteLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (DetailView.Rows.Count > 0)
            {
                for (int i = DetailView.Rows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow row = DetailView.Rows[i];
                    bool check = Convert.ToBoolean(row.Cells["Check"].Value);
                    if (check)
                    {
                        this.DetailView.Rows.RemoveAt(i);
                    }
                }
            }
        }

        private void NewAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            double amount = Convert.ToDouble(Amount.Text);
            double rate = Convert.ToDouble(Rate.Text);
            foreach (DataGridViewRow row in DetailView.Rows)
            {
                if (row.Cells["DetailAmount"].Value != null)
                {
                    double deAmount = Convert.ToDouble(row.Cells["DetailAmount"].Value);
                    amount = amount- deAmount;
                }
            }
            double totalAmount = amount * rate;
            int index = this.DetailView.Rows.Add();
            DetailView.Rows[index].Cells["DetailAssociation"].Value = Association.SelectedValue;
            DetailView.Rows[index].Cells["DetailAmount"].Value = amount;
            DetailView.Rows[index].Cells["DetailTotalAmount"].Value = totalAmount;
        }

        private void Association_SelectedIndexChanged(object sender, EventArgs e)
        {
            string val = (string)this.Association.SelectedValue;
            foreach (DataGridViewRow row in DetailView.Rows)
            {
                row.Cells["DetailAssociation"].Value = val;
            }
        }


        private void DetailView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                TextBox textBox1 = e.Control as TextBox;
                textBox1.KeyPress -= new KeyPressEventHandler(Cells_KeyPress);
                textBox1.KeyUp -= new KeyEventHandler(Cells_KeyUp);
                if (((DataGridView)sender).CurrentCell.ColumnIndex == 3) // 第一列
                {
                    textBox1.KeyPress += new KeyPressEventHandler(Cells_KeyPress);
                    textBox1.KeyUp += new KeyEventHandler(Cells_KeyUp);
                }
            }
        }

        void Cells_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox textBox1 = (TextBox)sender;
            if (!string.IsNullOrEmpty(textBox1.Text.Trim()) && textBox1.Text.Trim() != "-")
            {
                try
                {
                    int CurrentRow = DetailView.CurrentRow.Index;
                    double val = Convert.ToDouble(textBox1.Text);
                    double total = Convert.ToDouble(this.Rate.Text) * val;
                    DetailView.Rows[CurrentRow].Cells[4].Value = "￥" + total.ToString("#,##0.00");
                }
                catch { 
                    
                }
            }
        }

        private void Cells_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox1 = (TextBox)sender;
            if ((Convert.ToInt32(e.KeyChar) < 48 || Convert.ToInt32(e.KeyChar) > 57) && Convert.ToInt32(e.KeyChar) != 46 
                && Convert.ToInt32(e.KeyChar) != 8 && Convert.ToInt32(e.KeyChar) != 45 && Convert.ToInt32(e.KeyChar) != 13)
            {
                e.Handled = true;  // 输入非法就屏蔽
            }
            else
            {
                if ((Convert.ToInt32(e.KeyChar) == 46) && (textBox1.Text.IndexOf(".") != -1))
                {
                    e.Handled = true;
                }
                if (Convert.ToInt32(e.KeyChar) == 45 && textBox1.Text.IndexOf("-") > 0)
                {
                    e.Handled = true;
                }
            }
        }
    }
}
