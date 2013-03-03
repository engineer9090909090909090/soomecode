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
    public partial class OrderTrackForm : Form
    {
        FinOrderManager finOrderManager;
        public Order UpdateOrder { set; get; }
        public OrderTracking UpdateOrderTracking { set; get; }

        public OrderTrackForm()
        {
            InitializeComponent();
            finOrderManager = new FinOrderManager();
        }

        private void OrderTrackForm_Load(object sender, EventArgs e)
        {
            this.Tracker.DisplayMember = "Label";
            this.Tracker.ValueMember = "Key";
            this.Status.DisplayMember = "Label";
            this.Status.ValueMember = "Key";
            this.Status.DataSource = finOrderManager.GetAppDicOptions(Constants.OrderStatusType);
            this.Tracker.DataSource = finOrderManager.GetAppDicOptions(Constants.Employee);
            LoadUpdateOrder(this.UpdateOrderTracking);
        }

        private void LoadUpdateOrder(OrderTracking tracking)
        {
            if (tracking == null) return;
            this.Tag = tracking;
            this.TrackingDate.Text = tracking.TrackingDate;
            this.Description.Text = tracking.Description;
            AliHelperUtils.LoadAppDicComboBoxValue(this.Tracker, tracking.Tracker);
            AliHelperUtils.LoadAppDicComboBoxValue(this.Status, tracking.Status);
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            OrderTracking tracking;
            if (this.Tag == null)
            {
                tracking = new OrderTracking();
                tracking.Status = ((AppDic)this.Status.SelectedItem).Key;
                tracking.OrderId = UpdateOrder.Id;
            }
            else
            {
                tracking = (OrderTracking)this.Tag;
            }
            tracking.TrackingDate = this.TrackingDate.Value.ToString(Constants.DateFormat);
            tracking.Description = this.Description.Text.Trim();
            tracking.Tracker = ((AppDic)this.Tracker.SelectedItem).Key;
            if (string.IsNullOrEmpty(tracking.Description))
            {
                return;
            }
            finOrderManager.InsertOrUpdateTracking(tracking);
            this.Close();
        }

        private void Canncel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
