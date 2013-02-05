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
            InitDataGridView();
            finOrderManager = new FinOrderManager();
        }

        private void FinView_Load(object sender, EventArgs e)
        {
            this.BeginDateTxt.Value = DateTime.Now;
            this.EndDateTxt.Value = DateTime.Now;
            BindDataWithPage(1);
        }

        private void InitDataGridView() {

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("DetailId", typeof(Int32));
            dataTable.Columns.Add("EventTime", typeof(string));
            dataTable.Columns.Add("EventName", typeof(string));
            dataTable.Columns.Add("Amount", typeof(string));
            dataTable.Columns.Add("OrderNo", typeof(string));
            dataTable.Columns.Add("ItemType", typeof(string));
            dataTable.Columns.Add("Association", typeof(string));
            dataTable.Columns.Add("EventType", typeof(string));
            dataTable.Columns.Add("Remark", typeof(string));
            FinDetailDataView.DataSource = dataTable;
            DataGridViewColumn DetailId = FinDetailDataView.Columns[0];
            DetailId.HeaderText = "序号";
            DetailId.Name = "DetailId";
            DetailId.Width = 50;
            DetailId.ReadOnly = true;
            DataGridViewColumn EventTime = FinDetailDataView.Columns[1];
            EventTime.HeaderText = "时间";
            EventTime.Name = "EventTime";
            EventTime.Width = 80;
            EventTime.ReadOnly = true;
            DataGridViewColumn EventName = FinDetailDataView.Columns[2]; 
            EventName.HeaderText = "项目名称";
            EventName.Name = "EventName";
            EventName.ReadOnly = true;
            EventName.Width = 250;
            DataGridViewColumn Amount = FinDetailDataView.Columns[3]; 
            Amount.HeaderText = "金额";
            Amount.Name = "Amount";
            Amount.ReadOnly = true;
            Amount.Width = 80;
            DataGridViewColumn OrderNo = FinDetailDataView.Columns[4]; 
            OrderNo.HeaderText = "所属业务";
            OrderNo.Name = "OrderNo";
            OrderNo.Width = 100;
            OrderNo.ReadOnly = true;
            DataGridViewColumn ItemType = FinDetailDataView.Columns[5];  
            ItemType.HeaderText = "项目类型";
            ItemType.Name = "ItemType";
            ItemType.Width = 100;
            ItemType.ReadOnly = true;
            DataGridViewColumn Association = FinDetailDataView.Columns[6]; 
            Association.HeaderText = "经手人/相关人";
            Association.Name = "Association";
            Association.Width = 100;
            Association.ReadOnly = true;
            DataGridViewColumn EventType = FinDetailDataView.Columns[7]; 
            EventType.HeaderText = "收支类型";
            EventType.Name = "EventType";
            EventType.Width = 100;
            EventType.ReadOnly = true;
            DataGridViewColumn Remark = FinDetailDataView.Columns[8]; 
            Remark.HeaderText = "备注";
            Remark.Name = "Remark";
            Remark.ReadOnly = true;
            Remark.Width = 200;
            
        }

        private void BindDataWithPage(int Page)
        {
            QueryObject<FinDetails> query = new QueryObject<FinDetails>();
            query.Page = Page;
            query.PageSize = FinDetailPager.PageSize;
            query.Condition = new FinDetails();
            query.Condition.BeginTime = this.BeginDateTxt.Value.ToString(Constants.DateFormat);
            query.Condition.EndTime = this.EndDateTxt.Value.ToString(Constants.DateFormat);
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
            query.Condition.BeginTime = this.BeginDateTxt.Value.ToString(Constants.DateFormat);
            query.Condition.EndTime = this.EndDateTxt.Value.ToString(Constants.DateFormat);
            query.Condition.EventName = this.EventNameTxt.Text.Trim();
            query.Condition.EventType = (string)this.EventTypeTxt.SelectedValue;
            query.Condition.ItemType = (string)this.ItemTypeTxt.SelectedValue;
            query.Condition.OrderNo = this.OrderNoTxt.Text.Trim();
            query.Condition.Association = this.AssociationTxt.Text.Trim();
            QueryObject<FinDetails> result = finOrderManager.GetFinDetails(query);

        }

        

        
    }
}
