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
            FinOrderManager.OnEditFinanceEvent += new NewEditItemEvent(OnNewEditEvent);
        }

        private void FinanceWaterView_Load(object sender, EventArgs e)
        {
            this.BeginDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.EndDate.Value = DateTime.Now;
            this.ItemType.DisplayMember = "Label";
            this.ItemType.ValueMember = "Key";
            this.EventType.DisplayMember = "Label";
            this.EventType.ValueMember = "Key";
            this.Association.DisplayMember = "Label";
            this.Association.ValueMember = "Key";
            this.ItemType.DataSource = finOrderManager.GetQueryAppDicOptions(Constants.BussnessType);
            this.EventType.DataSource = finOrderManager.GetQueryAppDicOptions(Constants.DebitCredit);
            this.Association.DataSource = finOrderManager.GetQueryAppDicOptions(Constants.Employee);
            BindDataWithPage();
        }

        private void BindDateTable(List<Finance> list)
        {
            int rowIndex = 0;
            int dateIndex = 0;
            foreach (Finance finance in list)
            {
                dateIndex++;
                foreach (FinDetails detail in finance.Details)
                {
                    DataRow row = this.dataTable.NewRow();
                    row["FinId"] = finance.FinId;
                    row["FinDate"] = finance.FinDate + "(" + dateIndex + ")";
                    row["EventType"] = finance.EventType;
                    row["Description"] = finance.Description;
                    row["Amount"] = "(" + finance.Currency + ")" + finance.Amount.ToString("#,##0.00");
                    row["Rate"] = finance.Rate.ToString("#,##0.0000");
                    row["TotalAmount"] = "￥" + finance.TotalAmount.ToString("#,##0.00");
                    row["ReferenceNo"] = finance.ReferenceNo;
                    row["ReceivePaymentor"] = finance.ReceivePaymentor;
                    //row["Association"] = finance.Association;
                    row["Remark"] = finance.Remark;
                    row["DetailEventName"] = detail.Description;
                    row["DetailOrderNo"] = detail.OrderNo;
                    row["DetailAmount"] = "(" + finance.Currency + ")" + detail.Amount.ToString("#,##0.00");
                    row["DetailTotalAmount"] = "￥" + detail.TotalAmount.ToString("#,##0.00");
                    row["DetailRemark"] = detail.Remark;
                    this.dataTable.Rows.Add(row);
                    FinanceView.Rows[rowIndex].Cells["Amount"].Style.ForeColor =
                        (finance.Amount > 0) ? Color.Red : Color.Blue;
                    FinanceView.Rows[rowIndex].Cells["DetailAmount"].Style.ForeColor =
                        (detail.Amount > 0) ? Color.Red : Color.Blue;
                    rowIndex++;
                    
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
            //dataTable.Columns.Add("Association", typeof(string));
            dataTable.Columns.Add("Remark", typeof(string));
            dataTable.Columns.Add("DetailEventName", typeof(string));
            dataTable.Columns.Add("DetailOrderNo", typeof(string));
            dataTable.Columns.Add("DetailAmount", typeof(string));
            dataTable.Columns.Add("DetailTotalAmount", typeof(string));
            dataTable.Columns.Add("DetailRemark", typeof(string));
            FinanceView.DataSource = dataTable;
            FinanceView.ColumnHeadersHeight = 25;
            FinanceView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //FinanceView.MergeColumnNames.Add("FinId");
            FinanceView.MergeColumnNames.Add("FinDate");
            FinanceView.MergeColumnNames.Add("EventType");
            FinanceView.MergeColumnNames.Add("Description");
            FinanceView.MergeColumnNames.Add("Amount");
            FinanceView.MergeColumnNames.Add("Rate");
            FinanceView.MergeColumnNames.Add("TotalAmount");
            FinanceView.MergeColumnNames.Add("ReferenceNo");
            FinanceView.MergeColumnNames.Add("ReceivePaymentor");
            FinanceView.MergeColumnNames.Add("Remark");

            int colIndex = 0;
            DataGridViewCellStyle cellStyle2 = new DataGridViewCellStyle();
            cellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            cellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            DataGridViewColumn FinId = FinanceView.Columns[colIndex++];
            FinId.HeaderText = "序号";
            FinId.Name = "FinId";
            FinId.Width = 50;
            FinId.ReadOnly = true;
            FinId.Visible = false;

            DataGridViewColumn FinDate = FinanceView.Columns[colIndex++];
            FinDate.HeaderText = "时间";
            FinDate.Name = "FinDate";
            FinDate.Width = 90;
            FinDate.ReadOnly = true;

            DataGridViewColumn EventType = FinanceView.Columns[colIndex++];
            EventType.HeaderText = "收支类型";
            EventType.Name = "EventType";
            EventType.Width = 70;
            EventType.ReadOnly = true;

            DataGridViewColumn Desciption = FinanceView.Columns[colIndex++];
            Desciption.HeaderText = "款项说明";
            Desciption.Name = "EventType";
            Desciption.Width = 200;
            Desciption.ReadOnly = true;

            DataGridViewColumn Amount = FinanceView.Columns[colIndex++];
            Amount.HeaderText = "金额";
            Amount.Name = "Amount";
            Amount.ReadOnly = true;
            Amount.Width = 100;
            Amount.DefaultCellStyle = cellStyle2;

            DataGridViewColumn Rate = FinanceView.Columns[colIndex++];
            Rate.HeaderText = "汇率";
            Rate.Name = "Rate";
            Rate.ReadOnly = true;
            Rate.Width = 65;
            Rate.DefaultCellStyle = cellStyle2;

            DataGridViewColumn TotalAmount = FinanceView.Columns[colIndex++];
            TotalAmount.HeaderText = "总金额";
            TotalAmount.Name = "TotalAmount";
            TotalAmount.ReadOnly = true;
            TotalAmount.Width = 100;
            TotalAmount.DefaultCellStyle = cellStyle2;


            DataGridViewColumn ReferenceNo = FinanceView.Columns[colIndex++];
            ReferenceNo.HeaderText = "流水号";
            ReferenceNo.Name = "ReferenceNo";
            ReferenceNo.Width = 80;
            ReferenceNo.ReadOnly = true;

            DataGridViewColumn ReceivePaymentor = FinanceView.Columns[colIndex++];
            ReceivePaymentor.HeaderText = "收付款单位";
            ReceivePaymentor.Name = "ReceivePaymentor";
            ReceivePaymentor.Width = 120;
            ReceivePaymentor.ReadOnly = true;

            //DataGridViewColumn Association = FinanceView.Columns[colIndex++];
            //Association.HeaderText = "经手人/相关人";
            //Association.Name = "Association";
            //Association.Width = 100;
            //Association.ReadOnly = true;

            DataGridViewColumn Remark = FinanceView.Columns[colIndex++];
            Remark.HeaderText = "备注";
            Remark.Name = "Remark";
            Remark.Width = 250;
            Remark.ReadOnly = true;

            DataGridViewColumn DetailEventName = FinanceView.Columns[colIndex++];
            DetailEventName.HeaderText = "描述(明细)";
            DetailEventName.Name = "DetailEventName";
            DetailEventName.ReadOnly = true;
            DetailEventName.Width = 200;

            DataGridViewColumn DetailOrderNo = FinanceView.Columns[colIndex++];
            DetailOrderNo.HeaderText = "所属业务(明细)";
            DetailOrderNo.Name = "DetailOrderNo";
            DetailOrderNo.Width = 100;
            DetailOrderNo.ReadOnly = true;
            DataGridViewColumn DetailAmount = FinanceView.Columns[colIndex++];
            DetailAmount.HeaderText = "金额(明细)";
            DetailAmount.Name = "DetailAmount";
            DetailAmount.Width = 100;
            DetailAmount.ReadOnly = true;
            DetailAmount.DefaultCellStyle = cellStyle2;

            DataGridViewColumn DetailTotalAmount = FinanceView.Columns[colIndex++];
            DetailTotalAmount.HeaderText = "总金额(明细)";
            DetailTotalAmount.Name = "DetailAmount";
            DetailTotalAmount.Width = 100;
            DetailTotalAmount.ReadOnly = true;
            DetailTotalAmount.DefaultCellStyle = cellStyle2;

            DataGridViewColumn DetailRemark = FinanceView.Columns[colIndex++];
            DetailRemark.HeaderText = "备注(明细)";
            DetailRemark.Name = "Remark";
            DetailRemark.Width = 300;
            DetailRemark.ReadOnly = true;

        }

        void OnNewEditEvent(object sender, ItemEventArgs e)
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
            query.Condition.ReceivePaymentor = this.ReceivePaymentor.Text.Trim();
            QueryObject<Finance> result = finOrderManager.GetFinances(query);
            BindDateTable(result.Result);
        }

        private void FinDetailQueryBtn_Click(object sender, EventArgs e)
        {
            BindDataWithPage();
        }

        private void FinanceView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(this.FinanceView.Rows[e.RowIndex].Cells[0].Value);
            EditFinWater f = new EditFinWater();
            Finance finance = finOrderManager.GetFinance(id);
            f.LoadEditData(finance);
            f.ShowDialog(this);
        }


    }
}
