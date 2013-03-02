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
    public partial class WaterView : UserControl
    {
        private FinOrderManager finOrderManager;
        private List<Finance> list;
        public WaterView()
        {
            InitializeComponent();
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

        void OnNewEditEvent(object sender, ItemEventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                BindDataWithPage();
            }));
            
        }

        private void BindDataWithPage()
        {
            QueryObject<Finance> query = new QueryObject<Finance>();
            query.Condition = new Finance();
            query.Condition.BeginTime = this.BeginDate.Value.ToString(Constants.DateFormat);
            query.Condition.EndTime = this.EndDate.Value.ToString(Constants.DateFormat);
            query.Condition.Description = this.Description.Text.Trim();
            query.Condition.EventType = (string)this.EventType.SelectedValue;
            query.Condition.ItemType = (string)this.ItemType.SelectedValue;
            query.Condition.Association = (string)this.Association.SelectedValue;
            query.Condition.ReceivePaymentor = this.ReceivePaymentor.Text.Trim();
            QueryObject<Finance> result = finOrderManager.GetFinances(query);
            list = result.Result;
            DoFill(list);
        }

        private void FinDetailQueryBtn_Click(object sender, EventArgs e)
        {
            BindDataWithPage();
        }


        public void DoFill(List<Finance> list)
        {
            int rowCount = 3;
            foreach (Finance finance in list)
            {
                rowCount = rowCount + finance.Details.Count;
            }
            FinGrid.Redim(0, 0);
            FinGrid.FixedRows = 2;
            FinGrid.FixedColumns = 3;
            FinGrid.EnableSort = true;
            FinGrid.Redim(rowCount, 15);
            FinGrid[0, 0] = new MyHeader("日期");
            FinGrid[0, 0].RowSpan = 2;
            FinGrid[0, 0].Column.Width = 100;
            FinGrid[0, 1] = new MyHeader("收支类型");
            FinGrid[0, 1].RowSpan = 2;
            FinGrid[0, 1].Column.Width = 70;
            FinGrid[0, 2] = new MyHeader("款项描述"); 
            FinGrid[0, 2].RowSpan = 2;
            FinGrid[0, 2].Column.Width = 250;
            FinGrid[0, 3] = new MyHeader("金额");
            FinGrid[0, 3].RowSpan = 2;
            FinGrid[0, 3].Column.Width = 100;
            FinGrid[0, 4] = new MyHeader("汇率");
            FinGrid[0, 4].RowSpan = 2;
            FinGrid[0, 4].Column.Width = 100;
            FinGrid[0, 5] = new MyHeader("总金额");
            FinGrid[0, 5].RowSpan = 2;
            FinGrid[0, 5].Column.Width = 150;
            FinGrid[0, 6] = new MyHeader("流水号");
            FinGrid[0, 6].RowSpan = 2;
            FinGrid[0, 6].Column.Width = 100;
            FinGrid[0, 7] = new MyHeader("收付款单位");
            FinGrid[0, 7].RowSpan = 2;
            FinGrid[0, 7].Column.Width = 100;
            FinGrid[0, 8] = new MyHeader("经手人/相关人");
            FinGrid[0, 8].RowSpan = 2;
            FinGrid[0, 8].Column.Width = 100;
            FinGrid[0, 9] = new MyHeader("备注");
            FinGrid[0, 9].RowSpan = 2;
            FinGrid[0, 9].Column.Width = 150;
            FinGrid[0, 10] = new MyHeader("明细");
            FinGrid[0, 10].ColumnSpan = 5;
            FinGrid[1, 10] = new MyHeader("描述");
            FinGrid[1, 10].Column.Width = 200;
            FinGrid[1, 11] = new MyHeader("所属业务");
            FinGrid[1, 11].Column.Width = 100;
            FinGrid[1, 12] = new MyHeader("金额");
            FinGrid[1, 12].Column.Width = 150;
            FinGrid[1, 13] = new MyHeader("总金额");
            FinGrid[1, 13].Column.Width = 150;
            FinGrid[1, 14] = new MyHeader("备注");
            FinGrid[1, 14].Column.Width = 300;
            SourceGrid.Cells.Controllers.CustomEvents clickEvent = new SourceGrid.Cells.Controllers.CustomEvents();
            clickEvent.DoubleClick += new EventHandler(clickEvent_Click);
            SourceGrid.Cells.Views.Cell view;
            int r = 2;
            double SumTotalAmount = 0.0;
            double DetailSumTotalAmount = 0.0;
            foreach (Finance finance in list)
            {
                int detailCount = finance.Details.Count();
                FinGrid.Rows[r].Height = 22;
                FinGrid[r, 0] = new SourceGrid.Cells.Cell(finance.FinDate);
                FinGrid[r, 0].AddController(clickEvent);
                FinGrid[r, 1] = new SourceGrid.Cells.Cell(finance.EventType);
                FinGrid[r, 1].AddController(clickEvent);
                FinGrid[r, 2] = new SourceGrid.Cells.Cell(finance.Description);
                FinGrid[r, 2].AddController(clickEvent);
                string amount = "(" + finance.Currency + ")" + finance.Amount.ToString("#,##0.00");
                FinGrid[r, 3] = new SourceGrid.Cells.Cell(amount);
                FinGrid[r, 3].AddController(clickEvent);
                view = new SourceGrid.Cells.Views.Cell();
                view.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                view.ForeColor = (finance.TotalAmount > 0) ? Color.Red : Color.Blue;
                FinGrid[r, 3].View = view;
                string rate = finance.Rate.ToString("#,##0.0000");
                FinGrid[r, 4] = new SourceGrid.Cells.Cell(rate);
                view = new SourceGrid.Cells.Views.Cell();
                view.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                FinGrid[r, 4].View = view;
                string totalAmount= "￥" + finance.TotalAmount.ToString("#,##0.00");
                SumTotalAmount = SumTotalAmount + finance.TotalAmount;
                FinGrid[r, 5] = new SourceGrid.Cells.Cell(totalAmount);
                view = new SourceGrid.Cells.Views.Cell();
                view.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                view.ForeColor = (finance.TotalAmount > 0) ? Color.Red : Color.Blue;
                FinGrid[r, 5].View = view;
                FinGrid[r, 6] = new SourceGrid.Cells.Cell(finance.ReferenceNo);
                FinGrid[r, 7] = new SourceGrid.Cells.Cell(finance.ReceivePaymentor);
                FinGrid[r, 8] = new SourceGrid.Cells.Cell(finance.Association);
                FinGrid[r, 9] = new SourceGrid.Cells.Cell(finance.Remark);
                FinGrid[r, 0].RowSpan = detailCount;
                FinGrid[r, 1].RowSpan = detailCount;
                FinGrid[r, 2].RowSpan = detailCount;
                FinGrid[r, 3].RowSpan = detailCount;
                FinGrid[r, 4].RowSpan = detailCount;
                FinGrid[r, 5].RowSpan = detailCount;
                FinGrid[r, 6].RowSpan = detailCount;
                FinGrid[r, 7].RowSpan = detailCount;
                FinGrid[r, 8].RowSpan = detailCount;
                FinGrid[r, 9].RowSpan = detailCount;
               
                foreach (FinDetails detail in finance.Details)
                {
                    FinGrid.Rows[r].Tag = finance.FinId;
                    FinGrid.Rows[r].Height = 22;
                    FinGrid[r, 10] = new SourceGrid.Cells.Cell(detail.Description);
                    FinGrid[r, 11] = new SourceGrid.Cells.Cell(detail.OrderNo);
                    string detailAmount = "(" + finance.Currency + ")" + detail.Amount.ToString("#,##0.00");
                    FinGrid[r, 12] = new SourceGrid.Cells.Cell(detailAmount);
                    view = new SourceGrid.Cells.Views.Cell();
                    view.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                    view.ForeColor = (finance.TotalAmount > 0) ? Color.Red : Color.Blue;
                    FinGrid[r, 12].View = view;
                    string detailTotalAmount = "￥" + detail.TotalAmount.ToString("#,##0.00");
                    DetailSumTotalAmount = DetailSumTotalAmount + detail.TotalAmount;
                    FinGrid[r, 13] = new SourceGrid.Cells.Cell(detailTotalAmount);
                    view = new SourceGrid.Cells.Views.Cell();
                    view.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                    view.ForeColor = (finance.TotalAmount > 0) ? Color.Red : Color.Blue;
                    FinGrid[r, 13].View = view;
                    FinGrid[r, 14] = new SourceGrid.Cells.Cell(detail.Remark);
                    r++;
                }
            }
            for (int i = 0; i < 15; i++)
            {
                FinGrid[r, i] = new SourceGrid.Cells.Cell("");
            }
            FinGrid.Rows[r].Height = 22;
            FinGrid[r, 2] = new SourceGrid.Cells.Cell("合计");
            FinGrid[r, 5] = new SourceGrid.Cells.Cell("￥" + SumTotalAmount.ToString("#,##0.00"));
            FinGrid[r, 13] = new SourceGrid.Cells.Cell("￥" + DetailSumTotalAmount.ToString("#,##0.00"));
            view = new SourceGrid.Cells.Views.Cell();
            view.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
            view.ForeColor = (SumTotalAmount > 0) ? Color.Red : Color.Blue;
            FinGrid[r, 5].View =  FinGrid[r, 13].View = view;
            FinGrid.ClipboardMode = SourceGrid.ClipboardMode.All;
        }

        private class MyHeader : SourceGrid.Cells.ColumnHeader
        {
            public MyHeader(object value)
                : base(value)
            {
                //1 Header Row
                SourceGrid.Cells.Views.ColumnHeader view = new SourceGrid.Cells.Views.ColumnHeader();
                view.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
                View = view;
                AutomaticSortEnabled = false;
            }
        }

        private void clickEvent_Click(object sender, EventArgs e)
        {
            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            if (FinGrid.Rows[context.Position.Row].Tag == null) return;
            int id = (int)FinGrid.Rows[context.Position.Row].Tag;
            EditFinWater f = new EditFinWater();
            f.UpdateFinance = finOrderManager.GetFinance(id);
            f.ShowDialog(this);
        }

        private void ExpBtn_Click(object sender, EventArgs e)
        {
            QueryObject<Finance> query = new QueryObject<Finance>();
            query.Condition = new Finance();
            query.Condition.BeginTime = this.BeginDate.Value.ToString(Constants.DateFormat);
            query.Condition.EndTime = this.EndDate.Value.ToString(Constants.DateFormat);
            query.Condition.Description = this.Description.Text.Trim();
            query.Condition.EventType = (string)this.EventType.SelectedValue;
            query.Condition.ItemType = (string)this.ItemType.SelectedValue;
            query.Condition.Association = (string)this.Association.SelectedValue;
            query.Condition.ReceivePaymentor = this.ReceivePaymentor.Text.Trim();
            QueryObject<Finance> result = finOrderManager.GetFinances(query);
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel工作簿(*.xls,*.xlsx)| *.xls; *.xlsx";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = saveFileDialog1.FileName.ToString();
                try
                {
                    ExportExcel exporter = new ExportExcel();
                    exporter.AddColumn("FinDate", "日期");
                    exporter.AddColumn("EventType", "收支类型");
                    exporter.AddColumn("Description", "款项说明");
                    exporter.AddColumn("Amount", "金额");
                    exporter.AddColumn("Rate", "汇率");
                    exporter.AddColumn("TotalAmount", "总金额");
                    exporter.AddColumn("ReferenceNo", "流水号");
                    exporter.AddColumn("ReceivePaymentor", "收付款单位");
                    exporter.AddColumn("Association", "经手人/相关人");
                    exporter.AddColumn("Remark", "备注");
                    exporter.ExportToExcel<Finance>(result.Result, localFilePath);
                    exporter.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存文件出错：" + ex.Message);
                }
            }
        }

    }

   
}
