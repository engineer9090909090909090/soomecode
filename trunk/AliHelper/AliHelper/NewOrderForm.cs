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
        public NewOrderForm()
        {
            InitializeComponent();
            finOrderManager = new FinOrderManager();
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            order.BeginDate = this.BeginDate.Value.ToString(Constants.DateFormat);
            order.OrderNo = this.OrderNo.Text.Trim();
            order.Description = this.Description.Text.Trim();
            order.SalesMan = ((AppDic)this.SalesMan.SelectedItem).Key;
            order.Status = ((AppDic)this.Status.SelectedItem).Key;
            order.Remark = this.Remark.Text.Trim();
            if (string.IsNullOrEmpty(order.OrderNo))
            {
                return;
            }
            if (string.IsNullOrEmpty(order.Description))
            {
                return;
            }
            finOrderManager.InsertOrder(order);
            this.Close();
        }

        private void Canncel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NewOrderForm_Load(object sender, EventArgs e)
        {
            this.SalesMan.DisplayMember = "Label";
            this.SalesMan.ValueMember = "Key";
            this.Status.DisplayMember = "Label";
            this.Status.ValueMember = "Key";
            this.Status.DataSource = finOrderManager.GetAppDicOptions(Constants.OrderStatusType);
            this.SalesMan.DataSource = finOrderManager.GetAppDicOptions(Constants.Employee);
        }
    }
}
