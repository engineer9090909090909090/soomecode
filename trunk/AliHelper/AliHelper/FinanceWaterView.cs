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
        private FinOrderManager finOrderManager;
        private List<Finance> list;
        public FinanceWaterView()
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
            BindDataWithPage();
        }

        private void BindDataWithPage()
        {
            QueryObject<Finance> query = new QueryObject<Finance>();
            query.Condition = new Finance();
            query.Condition.BeginTime = this.BeginDate.Value.ToString(Constants.DateFormat);
            query.Condition.EndTime = this.EndDate.Value.ToString(Constants.DateFormat);
            query.Condition.Description = this.Description.Text.Trim();
            query.Condition.EventType = (string)this.EventType.SelectedValue;
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

        /*
        private void FinanceView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(this.FinanceView.Rows[e.RowIndex].Cells[0].Value);
            EditFinWater f = new EditFinWater();
            Finance finance = finOrderManager.GetFinance(id);
            f.LoadEditData(finance);
            f.ShowDialog(this);
        }
        */

        public void DoFill(List<Finance> list)
        {
            int rowCount = 2;
            foreach (Finance finance in list)
            {
                rowCount = rowCount + finance.Details.Count;
            }
            FinGrid.Redim(rowCount, 15);
            FinGrid.FixedRows = 1;
            FinGrid[0, 0] = new MyHeader("日期");
            FinGrid[0, 0].RowSpan = 2;
            FinGrid[0, 0].Column.Width = 100;
            FinGrid[0, 1] = new MyHeader("收支类型");
            FinGrid[0, 1].RowSpan = 2;
            FinGrid[0, 1].Column.Width = 70;
            FinGrid[0, 2] = new MyHeader("款项说明"); 
            FinGrid[0, 2].RowSpan = 2;
            FinGrid[0, 2].Column.Width = 200;
            FinGrid[0, 3] = new MyHeader("金额");
            FinGrid[0, 3].RowSpan = 2;
            FinGrid[0, 3].Column.Width = 150;
            FinGrid[0, 4] = new MyHeader("汇率");
            FinGrid[0, 4].RowSpan = 2;
            FinGrid[0, 4].Column.Width = 100;
            FinGrid[0, 5] = new MyHeader("总金额");
            FinGrid[0, 5].RowSpan = 2;
            FinGrid[0, 5].Column.Width = 150;
            FinGrid[0, 6] = new MyHeader("流水号");
            FinGrid[0, 6].RowSpan = 2;
            FinGrid[0, 6].Column.Width = 150;
            FinGrid[0, 7] = new MyHeader("收付款单位");
            FinGrid[0, 7].RowSpan = 2;
            FinGrid[0, 7].Column.Width = 150;
            FinGrid[0, 8] = new MyHeader("经手人/相关人");
            FinGrid[0, 8].RowSpan = 2;
            FinGrid[0, 8].Column.Width = 100;
            FinGrid[0, 9] = new MyHeader("备注");
            FinGrid[0, 9].RowSpan = 2;
            FinGrid[0, 9].Column.Width = 200;
            FinGrid[0, 10] = new MyHeader("明细");
            FinGrid[0, 10].ColumnSpan = 5;
            FinGrid[1, 10] = new MyHeader("描述");
            FinGrid[1, 10].Column.Width = 200;
            FinGrid[1, 11] = new MyHeader("所属业务");
            FinGrid[1, 11].Column.Width = 150;
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
            foreach (Finance finance in list)
            {
                int detailCount = finance.Details.Count();
                FinGrid[r, 0] = new SourceGrid.Cells.Cell(finance.FinDate, typeof(string));
                FinGrid[r, 0].AddController(clickEvent);
                FinGrid[r, 1] = new SourceGrid.Cells.Cell(finance.EventType, typeof(string));
                FinGrid[r, 1].AddController(clickEvent);
                FinGrid[r, 2] = new SourceGrid.Cells.Cell(finance.Description, typeof(string));
                FinGrid[r, 2].AddController(clickEvent);
                string amount = "(" + finance.Currency + ")" + finance.Amount.ToString("#,##0.00");
                FinGrid[r, 3] = new SourceGrid.Cells.Cell(amount, typeof(string));
                view = new SourceGrid.Cells.Views.Cell();
                view.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                view.ForeColor = (finance.TotalAmount > 0) ? Color.Red : Color.Blue;
                FinGrid[r, 3].View = view;
                string rate = finance.Rate.ToString("#,##0.0000");
                FinGrid[r, 4] = new SourceGrid.Cells.Cell(rate, typeof(string));
                string totalAmount= "￥" + finance.TotalAmount.ToString("#,##0.00");
                FinGrid[r, 5] = new SourceGrid.Cells.Cell(totalAmount, typeof(string));
                view = new SourceGrid.Cells.Views.Cell();
                view.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                view.ForeColor = (finance.TotalAmount > 0) ? Color.Red : Color.Blue;
                FinGrid[r, 5].View = view;
                FinGrid[r, 6] = new SourceGrid.Cells.Cell(finance.ReferenceNo, typeof(string));
                FinGrid[r, 7] = new SourceGrid.Cells.Cell(finance.ReceivePaymentor, typeof(string));
                FinGrid[r, 8] = new SourceGrid.Cells.Cell(finance.Association, typeof(string));
                FinGrid[r, 9] = new SourceGrid.Cells.Cell(finance.Remark, typeof(string));
                if (detailCount > 1)
                {
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
                }
                foreach (FinDetails detail in finance.Details)
                {
                    FinGrid[r, 10] = new SourceGrid.Cells.Cell(detail.Description, typeof(string));
                    FinGrid[r, 11] = new SourceGrid.Cells.Cell(detail.OrderNo, typeof(string));
                    string detailAmount = "(" + finance.Currency + ")" + detail.Amount.ToString("#,##0.00");
                    FinGrid[r, 12] = new SourceGrid.Cells.Cell(detailAmount, typeof(string));
                    view = new SourceGrid.Cells.Views.Cell();
                    view.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                    view.ForeColor = (finance.TotalAmount > 0) ? Color.Red : Color.Blue;
                    FinGrid[r, 12].View = view;
                    string detailTotalAmount = "￥" + detail.TotalAmount.ToString("#,##0.00");
                    FinGrid[r, 13] = new SourceGrid.Cells.Cell(detailTotalAmount, typeof(string));
                    view = new SourceGrid.Cells.Views.Cell();
                    view.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                    view.ForeColor = (finance.TotalAmount > 0) ? Color.Red : Color.Blue;
                    FinGrid[r, 13].View = view;
                    FinGrid[r, 14] = new SourceGrid.Cells.Cell(detail.Remark, typeof(string));
                    r++;
                }
            }
            FinGrid.ClipboardMode = SourceGrid.ClipboardMode.All;
        }

        private class MyHeader : SourceGrid.Cells.ColumnHeader
        {
            public MyHeader(object value)
                : base(value)
            {
                //1 Header Row
                SourceGrid.Cells.Views.ColumnHeader view = new SourceGrid.Cells.Views.ColumnHeader();
                //view.Font = new Font(FontFamily.GenericSansSerif, 10);
                view.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
                View = view;
                AutomaticSortEnabled = false;
            }
        }

        private void clickEvent_Click(object sender, EventArgs e)
        {
            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            int index = context.Position.Row - 2;
            int id = this.list[index].FinId;;
            EditFinWater f = new EditFinWater();
            Finance finance = finOrderManager.GetFinance(id);
            f.LoadEditData(finance);
            f.ShowDialog(this);
        }

    }

   
}
