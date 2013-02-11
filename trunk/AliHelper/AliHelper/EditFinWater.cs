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
    public partial class NewAddDetail : Form
    {
        FinOrderManager finOrderManager;
        public NewAddDetail()
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
            this.ItemType.DataSource = finOrderManager.GetAppDicOptions(Constants.BussnessType);
            this.EventType.DataSource = finOrderManager.GetAppDicOptions(Constants.DebitCredit);
            this.Curreny.DataSource = finOrderManager.GetAppDicOptions(Constants.CurrencyType);
            this.Association.DataSource = finOrderManager.GetAppDicOptions(Constants.Employee);
            this.DetailAssociation.DisplayMember = "Label";
            this.DetailAssociation.ValueMember = "Key";
            this.DetailAssociation.DataSource = finOrderManager.GetAppDicOptions(Constants.Employee);
            int index = this.DetailView.Rows.Add();
            DetailView.Rows[index].Cells[1].Value = this.Description.Text;
            DetailView.Rows[index].Cells[3].Value = this.Amount.Text;
            DetailView.Rows[index].Cells[4].Value = this.TotalAmount.Text;
        }

        public void LoadEditData(FinDetails detail)
        {
            this.Tag = detail;
            this.Description.Text = detail.Description;
            this.EventType.SelectedText = detail.EventType;
            this.FinDate.Text = detail.FinDate;
            this.ItemType.SelectedText = detail.ItemType;
            this.Remark.Text = detail.Remark;
            this.Association.Text = detail.Association;
            this.Curreny.SelectedText = detail.Currency;
            this.TotalAmount.Text = detail.TotalAmount.ToString("#,##0.00");
            this.Amount.Text = detail.Amount.ToString("#,##0.00");
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
            detail.Description = this.Description.Text.Trim();
            detail.EventType = ((AppDic)this.EventType.SelectedItem).Key;
            detail.FinDate = this.FinDate.Value.ToString(Constants.DateFormat);
            detail.ItemType = ((AppDic)this.ItemType.SelectedItem).Key; ;
            detail.Remark = this.Remark.Text.Trim();
            detail.Association = this.Association.Text.Trim();
            detail.Rate = Convert.ToDouble(this.Rate.Text.Trim());
            detail.Currency = ((AppDic)this.Curreny.SelectedItem).Key;
            detail.Amount = Convert.ToDouble(this.Amount.Text);
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
                double amount = Convert.ToDouble(this.Amount.Text);
                double rate = Convert.ToDouble(this.Rate.Text);
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
            this.DetailView.Rows.Add();
        }

        private void DetailView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            double total = 0.0;
            if (e.ColumnIndex == 3)
            {
                try
                {
                    double val = Convert.ToDouble(DetailView.Rows[row].Cells[3].Value);
                    total = Convert.ToDouble(this.Rate.Text) * val;
                }
                catch
                {
                    DetailView.Rows[row].Cells[3].Value = "";
                }
                DetailView.Rows[row].Cells[4].Value = "￥" + total.ToString("#,##0.00");
            }
        }

        private void DetailView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                TextBox textBox1 = e.Control as TextBox; 
                if (((DataGridView)sender).CurrentCell.ColumnIndex == 3) // 第一列
                {
                    textBox1.KeyPress += new KeyPressEventHandler(Cells_KeyPress);
                }
            }
        }

        private void Cells_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox1 = (TextBox)sender;
            if ((Convert.ToInt32(e.KeyChar) < 48 || 
                Convert.ToInt32(e.KeyChar) > 57) 
                && Convert.ToInt32(e.KeyChar) != 46 
                && Convert.ToInt32(e.KeyChar) != 8
                && Convert.ToInt32(e.KeyChar) != 45
                && Convert.ToInt32(e.KeyChar) != 13)
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
