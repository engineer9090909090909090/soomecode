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
        DataTable dataTable;
        public FinView()
        {
            InitializeComponent();
            dataTable = new DataTable(); 
            FinDetailPager.PageIndex = 1;
            FinDetailPager.PageSize = 100;
            InitDataGridView();
            finOrderManager = new FinOrderManager();
            FinOrderManager.OnEditFinDetailEvent += new NewEditItemEvent(OnNewEditEvent);
        }

        void OnNewEditEvent(object sender, ItemEventArgs e)
        {
            BindDataWithPage(FinDetailPager.PageIndex);
        }

        private void FinView_Load(object sender, EventArgs e)
        {
            this.BeginDateTxt.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.EndDateTxt.Value = DateTime.Now;
            this.ItemTypeTxt.DisplayMember = "Label";
            this.ItemTypeTxt.ValueMember = "Key";
            this.EventTypeTxt.DisplayMember = "Label";
            this.EventTypeTxt.ValueMember = "Key";
            this.AssociationTxt.DisplayMember = "Label";
            this.AssociationTxt.ValueMember = "Key";
            this.ItemTypeTxt.DataSource = finOrderManager.GetQueryAppDicOptions(Constants.BussnessType);
            this.EventTypeTxt.DataSource = finOrderManager.GetQueryAppDicOptions(Constants.DebitCredit);
            this.AssociationTxt.DataSource = finOrderManager.GetQueryAppDicOptions(Constants.Employee);
            BindDataWithPage(1);
        }

        private void BindDateTable(List<FinDetails> list)
        {
            int i = 0;
            foreach (FinDetails item in list)
            {
                DataRow row = this.dataTable.NewRow();
                row["DetailId"] = item.DetailId;
                row["Id"] = 1 + i;
                row["FinDate"] = item.FinDate;
                row["Description"] = item.Description;
                row["TotalAmount"] = "￥" + item.TotalAmount.ToString("#,##0.00");
                row["OrderNo"] = item.OrderNo;
                row["ItemType"] = item.ItemType;
                row["Association"] = item.Association;
                row["EventType"] = item.EventType;
                row["Remark"] = item.Remark;
                this.dataTable.Rows.Add(row);
                FinDetailDataView.Rows[i].Cells[4].Style.ForeColor = 
                (item.TotalAmount > 0)? Color.Red : Color.Blue;
                i++;
            }
        }
        private void InitDataGridView() 
        {
            dataTable.Columns.Add("DetailId", typeof(Int32));
            dataTable.Columns.Add("Id", typeof(Int32));
            dataTable.Columns.Add("FinDate", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));
            dataTable.Columns.Add("TotalAmount", typeof(string));
            dataTable.Columns.Add("OrderNo", typeof(string));
            dataTable.Columns.Add("ItemType", typeof(string));
            dataTable.Columns.Add("Association", typeof(string));
            dataTable.Columns.Add("EventType", typeof(string));
            dataTable.Columns.Add("Remark", typeof(string));
            FinDetailDataView.DataSource = dataTable;

            DataGridViewCellStyle cellStyle2 = new DataGridViewCellStyle();
            cellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            cellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            DataGridViewColumn DetailId = FinDetailDataView.Columns[0];
            DetailId.HeaderText = "Id";
            DetailId.Name = "DetailId";
            DetailId.Width = 50;
            DetailId.ReadOnly = true;
            DetailId.Visible = false;

            DataGridViewColumn Id = FinDetailDataView.Columns[1];
            Id.HeaderText = "序号";
            Id.Name = "Id";
            Id.Width = 50;
            Id.ReadOnly = true;
            DataGridViewColumn FinDate = FinDetailDataView.Columns[2];
            FinDate.HeaderText = "时间";
            FinDate.Name = "FinDate";
            FinDate.Width = 100;
            FinDate.ReadOnly = true;
            DataGridViewColumn Description = FinDetailDataView.Columns[3];
            Description.HeaderText = "描述";
            Description.Name = "Description";
            Description.ReadOnly = true;
            Description.Width = 250;
            DataGridViewColumn Amount = FinDetailDataView.Columns[4]; 
            Amount.HeaderText = "金额";
            Amount.Name = "TotalAmount";
            Amount.ReadOnly = true;
            Amount.Width = 100;
            Amount.DefaultCellStyle = cellStyle2;
            

            DataGridViewColumn OrderNo = FinDetailDataView.Columns[5]; 
            OrderNo.HeaderText = "所属业务";
            OrderNo.Name = "OrderNo";
            OrderNo.Width = 100;
            OrderNo.ReadOnly = true;
            DataGridViewColumn ItemType = FinDetailDataView.Columns[6];  
            ItemType.HeaderText = "项目类型";
            ItemType.Name = "ItemType";
            ItemType.Width = 100;
            ItemType.ReadOnly = true;
            ItemType.DefaultCellStyle = cellStyle2;
            DataGridViewColumn Association = FinDetailDataView.Columns[7]; 
            Association.HeaderText = "经手人/相关人";
            Association.Name = "Association";
            Association.Width = 100;
            Association.ReadOnly = true;
            Association.DefaultCellStyle = cellStyle2;
            DataGridViewColumn EventType = FinDetailDataView.Columns[8]; 
            EventType.HeaderText = "收支类型";
            EventType.Name = "EventType";
            EventType.Width = 70;
            EventType.ReadOnly = true;
            EventType.DefaultCellStyle = cellStyle2;
            DataGridViewColumn Remark = FinDetailDataView.Columns[9]; 
            Remark.HeaderText = "备注";
            Remark.Name = "Remark";
            Remark.Width = 200;
            Remark.ReadOnly = true;
            
        }

        private void BindDataWithPage(int Page)
        {
            dataTable.Clear();
            QueryObject<FinDetails> query = new QueryObject<FinDetails>();
            query.Page = Page;
            query.PageSize = FinDetailPager.PageSize;
            query.Condition = new FinDetails();
            query.Condition.BeginTime = this.BeginDateTxt.Value.ToString(Constants.DateFormat);
            query.Condition.EndTime = this.EndDateTxt.Value.ToString(Constants.DateFormat);
            query.Condition.Description = this.EventNameTxt.Text.Trim();
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
                BindDateTable(result.Result);
            }
            else
            {
                FinDetailPager.PageIndex = Page;
                FinDetailPager.PageSize = 100;
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
            query.Condition.Description = this.EventNameTxt.Text.Trim();
            query.Condition.EventType = (string)this.EventTypeTxt.SelectedValue;
            query.Condition.ItemType = (string)this.ItemTypeTxt.SelectedValue;
            query.Condition.OrderNo = this.OrderNoTxt.Text.Trim();
            query.Condition.Association = this.AssociationTxt.Text.Trim();
            QueryObject<FinDetails> result = finOrderManager.GetFinDetails(query);

        }

        private void FinDetailDataView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(this.FinDetailDataView.Rows[e.RowIndex].Cells[0].Value);
            EditFinDetail f = new EditFinDetail();
            f.UpdateDetail = finOrderManager.GetFinDetail(id);
            f.ShowDialog(this);
        }

        

        
    }
}
