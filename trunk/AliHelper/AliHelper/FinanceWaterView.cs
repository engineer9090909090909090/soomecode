using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AliHelper.Bussness;
using Soomes;

namespace AliHelper
{
    public partial class FinanceWaterView : UserControl
    {
        FinOrderManager finOrderManager;
        private DataTable dataTable;
        public FinanceWaterView()
        {
            InitializeComponent();
            
            InitDataGridView();
            finOrderManager = new FinOrderManager();
            finOrderManager.OnNewEditEvent += new NewEditItemEvent(finOrderManager_OnNewEditEvent);
        }

        private void FinanceWaterView_Load(object sender, EventArgs e)
        {
            this.BeginDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.EndDate.Value = DateTime.Now;
            this.ItemType.DisplayMember = "Label";
            this.ItemType.ValueMember = "Key";
            this.EventType.DisplayMember = "Label";
            this.EventType.ValueMember = "Key";
            this.ItemType.DataSource = finOrderManager.GetQueryAppDicOptions(Constants.BussnessType);
            this.EventType.DataSource = finOrderManager.GetQueryAppDicOptions(Constants.DebitCredit);
            BindDataWithPage();
        }

        private void BindDateTable(List<Finance> list)
        {
            foreach (Finance finance in list)
            {
                foreach (FinDetails detail in finance.Details)
                {
                    DataRow row = this.dataTable.NewRow();
                    row["FinId"] = finance.FinId;
                    row["FinDate"] = finance.FinDate;
                    row["EventType"] = finance.EventType;
                    row["Description"] = finance.Description;
                    row["Amount"] = "(" +finance.Currency + ")"+ finance.Amount;
                    row["Rate"] = finance.Rate;
                    row["TotalAmount"] = "￥" + finance.TotalAmount.ToString("#,##0.00");
                    row["ReferenceNo"] = finance.ReferenceNo;
                    row["ReceivePaymentor"] = finance.ReceivePaymentor;
                    row["Association"] = finance.Association;
                    row["Remark"] = finance.Remark;
                    row["DetailEventName"] = detail.Description;
                    row["DetailOrderNo"] = detail.OrderNo;
                    row["DetailAmount"] = "(" +finance.Currency + ")"+ detail.Amount;
                    row["TotalAmount"] = "￥" + detail.TotalAmount.ToString("#,##0.00");
                    row["DetailRemark"] = detail.Remark;
                    this.dataTable.Rows.Add(row);
                }
            }
        }

        private void InitDataGridView()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add("FinId", typeof(Int32));
            dataTable.Columns.Add("FinDate", typeof(string));
            dataTable.Columns.Add("EventType", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));
            dataTable.Columns.Add("Amount", typeof(string));//Currency
            dataTable.Columns.Add("Rate", typeof(string));
            dataTable.Columns.Add("TotalAmount", typeof(string));
            dataTable.Columns.Add("ReferenceNo", typeof(string));
            dataTable.Columns.Add("ReceivePaymentor", typeof(string));
            dataTable.Columns.Add("Association", typeof(string));
            dataTable.Columns.Add("Remark", typeof(string));

            dataTable.Columns.Add("DetailEventName", typeof(string));
            dataTable.Columns.Add("DetailOrderNo", typeof(string));
            dataTable.Columns.Add("DetailAmount", typeof(string));
            dataTable.Columns.Add("DetailTotalAmount", typeof(string));
            dataTable.Columns.Add("DetailRemark", typeof(string));

            FinanceView.DataSource = dataTable;
            FinanceView.ColumnHeadersHeight = 40;
            FinanceView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            FinanceView.MergeColumnNames.Add("FinId");
            FinanceView.MergeColumnNames.Add("FinDate");
            FinanceView.MergeColumnNames.Add("EventType");
            FinanceView.MergeColumnNames.Add("Description");
            FinanceView.MergeColumnNames.Add("Amount");
            FinanceView.MergeColumnNames.Add("Rate");
            FinanceView.MergeColumnNames.Add("TotalAmount");
            FinanceView.MergeColumnNames.Add("ReferenceNo");
            FinanceView.MergeColumnNames.Add("ReceivePaymentor");
            FinanceView.MergeColumnNames.Add("Association");
            FinanceView.MergeColumnNames.Add("Remark");
            FinanceView.AddSpanHeader(11, 5, "明细");

            DataGridViewCellStyle cellStyle2 = new DataGridViewCellStyle();
            cellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            cellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            DataGridViewColumn DetailId = FinanceView.Columns[0];
            DetailId.HeaderText = "序号";
            DetailId.Name = "FinId";
            DetailId.Width = 50;
            DetailId.ReadOnly = true;
            DetailId.Visible = false;

            DataGridViewColumn FinDate = FinanceView.Columns[1];
            FinDate.HeaderText = "时间";
            FinDate.Name = "FinDate";
            FinDate.Width = 90;
            FinDate.ReadOnly = true;

            DataGridViewColumn EventType = FinanceView.Columns[2];
            EventType.HeaderText = "收支类型";
            EventType.Name = "EventType";
            EventType.Width = 70;
            EventType.ReadOnly = true;

            DataGridViewColumn Desciption = FinanceView.Columns[3];
            Desciption.HeaderText = "款项说明";
            Desciption.Name = "EventType";
            Desciption.Width = 200;
            Desciption.ReadOnly = true;

            DataGridViewColumn Amount = FinanceView.Columns[4];
            Amount.HeaderText = "金额";
            Amount.Name = "Amount";
            Amount.ReadOnly = true;
            Amount.Width = 100;
            Amount.DefaultCellStyle = cellStyle2;

            DataGridViewColumn Rate = FinanceView.Columns[5];
            Rate.HeaderText = "汇率";
            Rate.Name = "Rate";
            Rate.ReadOnly = true;
            Rate.Width = 70;
            Rate.DefaultCellStyle = cellStyle2;

            DataGridViewColumn TotalAmount = FinanceView.Columns[6];
            TotalAmount.HeaderText = "总金额";
            TotalAmount.Name = "TotalAmount";
            TotalAmount.ReadOnly = true;
            TotalAmount.Width = 100;
            TotalAmount.DefaultCellStyle = cellStyle2;


            DataGridViewColumn ReferenceNo = FinanceView.Columns[7];
            ReferenceNo.HeaderText = "流水号";
            ReferenceNo.Name = "ReferenceNo";
            ReferenceNo.Width = 80;
            ReferenceNo.ReadOnly = true;

            DataGridViewColumn ReceivePaymentor = FinanceView.Columns[8];
            ReceivePaymentor.HeaderText = "收付款单位";
            ReceivePaymentor.Name = "ReceivePaymentor";
            ReceivePaymentor.Width = 120;
            ReceivePaymentor.ReadOnly = true;
            ReceivePaymentor.DefaultCellStyle = cellStyle2;

            DataGridViewColumn Association = FinanceView.Columns[9];
            Association.HeaderText = "经手人/相关人";
            Association.Name = "Association";
            Association.Width = 80;
            Association.ReadOnly = true;
            Association.DefaultCellStyle = cellStyle2;
            
            DataGridViewColumn Remark = FinanceView.Columns[10];
            Remark.HeaderText = "备注";
            Remark.Name = "Remark";
            Association.Width = 150;
            Remark.ReadOnly = true;

            DataGridViewColumn DetailEventName = FinanceView.Columns[11];
            DetailEventName.HeaderText = "项目名称";
            DetailEventName.Name = "DetailEventName";
            DetailEventName.ReadOnly = true;
            DetailEventName.Width = 200;

            DataGridViewColumn DetailOrderNo = FinanceView.Columns[12];
            DetailOrderNo.HeaderText = "所属业务";
            DetailOrderNo.Name = "DetailOrderNo";
            DetailOrderNo.Width = 100;
            DetailOrderNo.ReadOnly = true;
            DataGridViewColumn DetailAmount = FinanceView.Columns[13];
            DetailAmount.HeaderText = "金额";
            DetailAmount.Name = "DetailAmount";
            DetailAmount.Width = 100;
            DetailAmount.ReadOnly = true;
            DetailAmount.DefaultCellStyle = cellStyle2;

            DataGridViewColumn DetailTotalAmount = FinanceView.Columns[14];
            DetailTotalAmount.HeaderText = "总金额";
            DetailTotalAmount.Name = "DetailAmount";
            DetailTotalAmount.Width = 100;
            DetailTotalAmount.ReadOnly = true;
            DetailTotalAmount.DefaultCellStyle = cellStyle2;

            DataGridViewColumn DetailRemark = FinanceView.Columns[15];
            DetailRemark.HeaderText = "备注";
            DetailRemark.Name = "Remark";
            EventType.Width = 200;
            DetailRemark.ReadOnly = true;

        }

        void finOrderManager_OnNewEditEvent(object sender, ItemEventArgs e)
        {
            BindDataWithPage();
        }

        private void BindDataWithPage()
        {
            dataTable.Clear();
            QueryObject<Finance> query = new QueryObject<Finance>();
            query.Condition = new Finance();
            query.Condition.BeginTime = this.BeginDate.Value.ToString(Constants.DateFormat);
            query.Condition.EndTime = this.EndDate.Value.ToString(Constants.DateFormat);
            query.Condition.Description = this.Description.Text.Trim();
            query.Condition.EventType = (string)this.EventType.SelectedValue;
            query.Condition.Association = (string)this.Association.SelectedValue;
            QueryObject<Finance> result = finOrderManager.GetFinances(query);
            BindDateTable(result.Result);
        }


    }
}
