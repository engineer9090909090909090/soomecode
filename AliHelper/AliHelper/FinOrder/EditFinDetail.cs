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
    public partial class EditFinDetail : Form
    {
        FinOrderManager finOrderManager;
        public FinDetails UpdateDetail { get; set; }
        public EditFinDetail()
        {
            InitializeComponent();
            finOrderManager = new FinOrderManager();
        }

        private void EditFin_Load(object sender, EventArgs e)
        {
            this.ItemTypeTxt.DisplayMember = "Label";
            this.ItemTypeTxt.ValueMember = "Key";
            this.EventTypeTxt.DisplayMember = "Label";
            this.EventTypeTxt.ValueMember = "Key";
            this.CurrenyTxt.DisplayMember = "Label";
            this.CurrenyTxt.ValueMember = "Key";
            this.AssociationTxt.DisplayMember = "Label";
            this.AssociationTxt.ValueMember = "Key";
            this.ItemTypeTxt.DataSource = finOrderManager.GetAppDicOptions(Constants.BussnessType);
            this.EventTypeTxt.DataSource = finOrderManager.GetAppDicOptions(Constants.DebitCredit);
            this.CurrenyTxt.DataSource = finOrderManager.GetAppDicOptions(Constants.CurrencyType);
            this.AssociationTxt.DataSource = finOrderManager.GetAppDicOptions(Constants.Employee);
            LoadEditData(UpdateDetail);
        }

        public void LoadEditData(FinDetails detail)
        {
            if (detail == null) return;
            this.Tag = detail;
            this.EventNameTxt.Text = detail.Description;
            this.EventTimeTxt.Text = detail.FinDate;
            this.RemarkTxt.Text = detail.Remark;
            this.OrderNoTxt.Text = detail.OrderNo;
            this.TotalAmount.Text = detail.TotalAmount.ToString("#,##0.00");
            this.AmountTxt.Text = detail.Amount.ToString("#,##0.00");
            AliHelperUtils.LoadAppDicComboBoxValue(this.EventTypeTxt, detail.EventType);
            AliHelperUtils.LoadAppDicComboBoxValue(this.ItemTypeTxt, detail.ItemType);
            AliHelperUtils.LoadAppDicComboBoxValue(this.CurrenyTxt, detail.Currency);
            AliHelperUtils.LoadAppDicComboBoxValue(this.AssociationTxt, detail.Association);
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            FinDetails detail = new FinDetails();
            if (this.Tag == null)
            {
                detail = new FinDetails();
            }
            else
            {
                detail = (FinDetails)this.Tag;
            }
            detail.Description = this.EventNameTxt.Text.Trim();
            detail.EventType = ((AppDic)this.EventTypeTxt.SelectedItem).Key;
            detail.FinDate = this.EventTimeTxt.Value.ToString(Constants.DateFormat);
            detail.ItemType = ((AppDic)this.ItemTypeTxt.SelectedItem).Key; ;
            detail.Remark = this.RemarkTxt.Text.Trim();
            detail.OrderNo = this.OrderNoTxt.Text.Trim();
            detail.Association = ((AppDic)this.AssociationTxt.SelectedItem).Key;
            detail.Rate = Convert.ToDouble(this.RateTxt.Text.Trim());
            detail.Currency = ((AppDic)this.CurrenyTxt.SelectedItem).Key;
            detail.Amount = Convert.ToDouble(this.AmountTxt.Text);
            if (detail.Amount == 0)
            {
                return;
            }
            if (string.IsNullOrEmpty(detail.Description))
            {
                return;
            }
            if (string.IsNullOrEmpty(detail.Association))
            {
                return;
            }
            List<FinDetails> list = new List<FinDetails>();
            list.Add(detail);
            finOrderManager.InsertOrUpdateDetails(list);
            list.Clear();
            list = null;
            this.Close();
        }

        private void Cannel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AmountTxt_Leave(object sender, EventArgs e)
        {
            try
            {
                double amount = Convert.ToDouble(this.AmountTxt.Text);
                double rate = Convert.ToDouble(this.RateTxt.Text);
                double total = amount * rate;
                this.TotalAmount.Text = "￥" + total.ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        private void CurrenyTxt_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CurrenyTxt.SelectedItem != null)
            {
                this.RateTxt.ReadOnly = false;
                string curreny = ((AppDic)CurrenyTxt.SelectedItem).Key;
                if (curreny.ToUpper() == "RMB" || curreny.ToUpper() == "CNY")
                {
                    this.RateTxt.Text = "1.0";
                    this.RateTxt.ReadOnly = true;
                }
                else if (curreny.ToUpper() == "USD")
                {
                    this.RateTxt.Text = "6.2000";
                }
            }
        }

    }
}
