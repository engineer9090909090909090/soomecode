using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soomes;
using AliHelper.Bussness;

namespace AliHelper
{
    public partial class FinView : UserControl
    {
        FinOrderManager finOrderManager;
        public FinView()
        {
            InitializeComponent();
            finOrderManager = new FinOrderManager();
        }

        private void BindDataWithPage(int Page)
        {
            QueryObject<FinDetails> query = new QueryObject<FinDetails>();
            query.Page = Page;
            query.PageSize = FinDetailPager.PageSize;
            query.Condition = new FinDetails();
            query.Condition.BeginTime = this.BeginDateTxt.Value;
            query.Condition.EndTime = this.EndDateTxt.Value;
            query.Condition.EventName = this.EventNameTxt.Text.Trim();
            query.Condition.EventType = (string)this.EventTypeTxt.SelectedValue;
            query.Condition.ItemType = (string)this.ItemTypeTxt.SelectedValue;
            query.Condition.OrderNo = this.OrderNoTxt.Text.Trim();
            query.Condition.Association = this.AssociationTxt.Text.Trim();
            QueryObject<FinDetails> result = finOrderManager.GetFinDetails(query);
            if (result.Result != null)
            {
                FinDetailPager.PageIndex = result.Page;
                FinDetailPager.PageSize = result.PageSize;
                FinDetailPager.RecordCount = result.RecordCount;
                FinDetailDataView.DataBindings.Clear();
                FinDetailDataView.DataSource = result.dt;
            }
            else
            {
                FinDetailPager.PageIndex = Page;
                FinDetailPager.PageSize = 20;
                FinDetailPager.RecordCount = 0;
            }
        }
        private void FinDetailPager_PageIndexChanged(object sender, EventArgs e)
        {
            BindDataWithPage(FinDetailPager.PageIndex);
        }

        private void FinDetailQueryBtn_Click(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }

        private void FinDetailExpBtn_Click(object sender, EventArgs e)
        {
            QueryObject<FinDetails> query = new QueryObject<FinDetails>();
            query.IsExport = true;
            query.Condition = new FinDetails();
            query.Condition.BeginTime = this.BeginDateTxt.Value;
            query.Condition.EndTime = this.EndDateTxt.Value;
            query.Condition.EventName = this.EventNameTxt.Text.Trim();
            query.Condition.EventType = (string)this.EventTypeTxt.SelectedValue;
            query.Condition.ItemType = (string)this.ItemTypeTxt.SelectedValue;
            query.Condition.OrderNo = this.OrderNoTxt.Text.Trim();
            query.Condition.Association = this.AssociationTxt.Text.Trim();
            QueryObject<FinDetails> result = finOrderManager.GetFinDetails(query);

        }

        
    }
}
