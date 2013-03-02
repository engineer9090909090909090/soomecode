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

namespace AliHelper
{
    public partial class NewOrderForm : Form
    {
        FinOrderManager finOrderManager;
        public Order UpdateOrder { set; get; }
        public NewOrderForm()
        {
            InitializeComponent();
            finOrderManager = new FinOrderManager();
        }
        private void NewOrderForm_Load(object sender, EventArgs e)
        {
            this.SalesMan.DisplayMember = "Label";
            this.SalesMan.ValueMember = "Key";
            this.Status.DisplayMember = "Label";
            this.Status.ValueMember = "Key";
            this.Status.DataSource = finOrderManager.GetAppDicOptions(Constants.OrderStatusType);
            this.SalesMan.DataSource = finOrderManager.GetAppDicOptions(Constants.Employee);
            LoadUpdateOrder(UpdateOrder);
        }
        private void LoadUpdateOrder(Order order)
        {
            if (order == null) return;
            this.Tag = order;
            this.BeginDate.Text = order.BeginDate;
            this.OrderNo.Text = order.OrderNo;
            this.Description.Text = order.Description;
            this.Remark.Text = order.Remark;
            AliHelperUtils.LoadAppDicComboBoxValue(this.SalesMan, order.SalesMan);
            AliHelperUtils.LoadAppDicComboBoxValue(this.Status, order.Status);
            this.Status.Enabled = false;
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            Order order;
            if (this.Tag == null)
            {
                order = new Order();
                order.Status = ((AppDic)this.Status.SelectedItem).Key;
            }
            else {
                order = (Order)this.Tag;
            }
            order.BeginDate = this.BeginDate.Value.ToString(Constants.DateFormat);
            order.OrderNo = this.OrderNo.Text.Trim();
            order.Description = this.Description.Text.Trim();
            order.Remark = this.Remark.Text.Trim();
            order.SalesMan = ((AppDic)this.SalesMan.SelectedItem).Key;
            if (string.IsNullOrEmpty(order.OrderNo))
            {
                return;
            }
            if (string.IsNullOrEmpty(order.Description))
            {
                return;
            }
            finOrderManager.InsertOrUpdateOrder(order);
            this.Close();
        }

        private void Canncel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
