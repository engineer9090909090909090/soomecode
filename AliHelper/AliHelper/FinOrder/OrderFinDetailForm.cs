using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soomes;

namespace AliHelper
{
    public partial class OrderFinDetailForm : Form
    {
        public string OrderNo { set; get; }
        FinOrderManager finOrderManager;
        public OrderFinDetailForm()
        {
            InitializeComponent();
            finOrderManager = new FinOrderManager();
        }

        private void OrderFinDetailForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(OrderNo))
            {
                BindDataWithPage(OrderNo);
            }
        }

        private void BindDataWithPage(string orderNo)
        {
            this.BeginInvoke(new Action(() =>
            {
                QueryObject<FinDetails> query = new QueryObject<FinDetails>();
                query.Page = 1;
                query.PageSize = 1000000;
                query.Condition = new FinDetails();
                query.Condition.OrderNo = orderNo.Trim();
                QueryObject<FinDetails> result = finOrderManager.GetFinDetails(query);
                DoFill(result.Result);
            }));
        }

        public void DoFill(List<FinDetails> list)
        {
            FinGrid.Redim(0, 0);
            FinGrid.EnableSort = true;
            FinGrid.Redim(list.Count + 2, 6);
            FinGrid.Rows[0].Height = 25;
            FinGrid[0, 0] = new MyHeader("序号");
            FinGrid[0, 0].Column.Width = 50;
            FinGrid[0, 0].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            FinGrid[0, 1] = new MyHeader("时间");
            FinGrid[0, 1].Column.Width = 80;
            FinGrid[0, 1].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            FinGrid[0, 2] = new MyHeader("描述");
            FinGrid[0, 2].Column.Width = 250;
            FinGrid[0, 2].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            FinGrid[0, 3] = new MyHeader("金额");
            FinGrid[0, 3].Column.Width = 100;
            FinGrid[0, 3].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            FinGrid[0, 4] = new MyHeader("收支类型");
            FinGrid[0, 4].Column.Width = 70;
            FinGrid[0, 4].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            FinGrid[0, 5] = new MyHeader("备注");
            FinGrid[0, 5].Column.Width = 200;
            SourceGrid.Cells.Views.Cell view;
            int r = 1;
            double TotalAmount = 0;
            foreach (FinDetails detail in list)
            {
                TotalAmount = TotalAmount + detail.TotalAmount;
                FinGrid.Rows[r].Tag = detail.FinId;
                FinGrid.Rows[r].Height = 22;
                FinGrid[r, 0] = new SourceGrid.Cells.Cell(r.ToString());
                FinGrid[r, 1] = new SourceGrid.Cells.Cell(detail.FinDate);
                FinGrid[r, 2] = new SourceGrid.Cells.Cell(detail.Description);
                FinGrid[r, 3] = new SourceGrid.Cells.Cell(detail.TotalAmount.ToString("#,##0.00"));
                view = new SourceGrid.Cells.Views.Cell();
                view.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                view.ForeColor = FileUtils.GetColor(detail.TotalAmount);
                FinGrid[r, 3].View = view;
                FinGrid[r, 4] = new SourceGrid.Cells.Cell(detail.EventType);
                FinGrid[r, 5] = new SourceGrid.Cells.Cell(detail.Remark);
                r++;
            }
            for (int i = 0; i < 6; i++)
            {
                FinGrid[r, i] = new SourceGrid.Cells.Cell("");
            }
            FinGrid[r, 2] = new SourceGrid.Cells.Cell("合计");
            FinGrid[r, 3] = new SourceGrid.Cells.Cell(TotalAmount.ToString("#,##0.00"));
            view = new SourceGrid.Cells.Views.Cell();
            view.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
            view.ForeColor = FileUtils.GetColor(TotalAmount);
            FinGrid[r, 3].View = view;
            FinGrid.ClipboardMode = SourceGrid.ClipboardMode.All;
        }

        private class MyHeader : SourceGrid.Cells.ColumnHeader
        {
            public MyHeader(object value)
                : base(value)
            {
                SourceGrid.Cells.Views.ColumnHeader view = new SourceGrid.Cells.Views.ColumnHeader();
                view.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
                View = view;
                AutomaticSortEnabled = false;
            }
        }
            
    }
}
