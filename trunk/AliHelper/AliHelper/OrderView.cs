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
    public partial class OrderView : UserControl
    {
        [DefaultValue(false), Category("自定义属性"), Description("是否为业务帐目视图")]
        public bool IsFinOrderView { set; get; }
        private FinOrderManager finOrderManager;
        private List<Order> list;

        
        public OrderView()
        {
            InitializeComponent();
            finOrderManager = new FinOrderManager();
            FinOrderManager.OnEditOrderEvent += new NewEditItemEvent(OnNewEditEvent);
        }

        private void OrderView_Load(object sender, EventArgs e)
        {
            DateTime beginTime = DateTime.Now.AddMonths(-3);
            this.BeginDateForm.Value = new DateTime(beginTime.Year, beginTime.Month, 1);
            this.BeginDateTo.Value = DateTime.Now;
            this.EndDateForm.Format = DateTimePickerFormat.Custom;
            this.EndDateForm.ValueX = null;
            this.EndDateTo.Format = DateTimePickerFormat.Custom;
            this.EndDateTo.ValueX = null;
            this.SalesMan.DisplayMember = "Label";
            this.SalesMan.ValueMember = "Key";
            this.SalesMan.DataSource = finOrderManager.GetQueryAppDicOptions(Constants.Employee);
            this.Status.DisplayMember = "Label";
            this.Status.ValueMember = "Key";
            this.Status.DataSource = finOrderManager.GetQueryAppDicOptions(Constants.OrderStatusType);
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
            QueryObject<Order> query = GetQueryObject();
            QueryObject<Order> result = finOrderManager.GetOrders(query);
            list = result.Result;
            DoFill(list);
        }

        public QueryObject<Order> GetQueryObject()
        {
            QueryObject<Order> query = new QueryObject<Order>();
            query.Condition = new Order();
            query.Condition.IsFinOrderView = IsFinOrderView;
            query.Condition.BeginDateForm = this.BeginDateForm.Value.ToString(Constants.DateFormat);
            query.Condition.BeginDateTo = this.BeginDateTo.Value.ToString(Constants.DateFormat);
            if (this.EndDateForm.ValueX != null)
            {
                query.Condition.EndDateForm = this.EndDateForm.ValueX.Value.ToString(Constants.DateFormat);
            }
            if (this.EndDateTo.ValueX != null)
            {
                DateTime endDateTo = this.EndDateTo.ValueX.Value;
                query.Condition.EndDateTo = endDateTo.ToString(Constants.DateFormat);
            }
            query.Condition.Description = this.Description.Text.Trim();
            query.Condition.Status = (string)this.Status.SelectedValue;
            query.Condition.SalesMan = (string)this.SalesMan.SelectedValue;
            query.Condition.Remark = this.Remark.Text.Trim();
            return query;
        }

        public void DoFill(List<Order> list)
        {
            OrderGrid.Redim(0, 0);
            OrderGrid.FixedRows = 1;
            OrderGrid.EnableSort = true;
            OrderGrid.Redim(list.Count + 1, 15);
            OrderGrid.Rows[0].Height = 25;
            OrderGrid[0, 0] = new MyHeader("开始日期");
            OrderGrid[0, 0].Column.Width = 100;
            OrderGrid[0, 0].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            OrderGrid[0, 1] = new MyHeader("结束日期");
            OrderGrid[0, 1].Column.Width = 100;
            OrderGrid[0, 1].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            OrderGrid[0, 2] = new MyHeader("订单编号");
            OrderGrid[0, 2].Column.Width = 150;
            OrderGrid[0, 2].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            OrderGrid[0, 3] = new MyHeader("订单描述");
            OrderGrid[0, 3].Column.Width = 300;
            OrderGrid[0, 3].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            OrderGrid[0, 4] = new MyHeader("业务员");
            OrderGrid[0, 4].Column.Width = 100;
            OrderGrid[0, 4].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            OrderGrid[0, 5] = new MyHeader("订单状态");
            OrderGrid[0, 5].Column.Width = 150;
            OrderGrid[0, 5].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            if (!IsFinOrderView)
            {
                OrderGrid[0, 6] = new MyHeader("备注");
                OrderGrid[0, 6].Column.Width = 300;
            }
            else
            {
                OrderGrid[0, 6] = new MyHeader("业务总金额");
                OrderGrid[0, 6].Column.Width = 150;
                OrderGrid[0, 6].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            }
            SourceGrid.Cells.Controllers.CustomEvents clickEvent = new SourceGrid.Cells.Controllers.CustomEvents();
            clickEvent.DoubleClick += new EventHandler(clickEvent_Click);
            int r = 1;
            foreach (Order order in list)
            {
                OrderGrid.Rows[r].Tag = order.Id;
                OrderGrid.Rows[r].Height = 22;
                OrderGrid[r, 0] = new SourceGrid.Cells.Cell(order.BeginDate);
                OrderGrid[r, 0].AddController(clickEvent);
                OrderGrid[r, 1] = new SourceGrid.Cells.Cell(order.EndDate);
                OrderGrid[r, 1].AddController(clickEvent);
                OrderGrid[r, 2] = new SourceGrid.Cells.Cell(order.OrderNo);
                OrderGrid[r, 2].AddController(clickEvent);
                OrderGrid[r, 3] = new SourceGrid.Cells.Cell(order.Description);
                OrderGrid[r, 3].AddController(clickEvent);
                OrderGrid[r, 4] = new SourceGrid.Cells.Cell(order.SalesMan);
                OrderGrid[r, 5] = new SourceGrid.Cells.Cell(order.Status);
                if (!IsFinOrderView)
                {
                    OrderGrid[r, 6] = new SourceGrid.Cells.Cell(order.Remark);
                }
                else
                {
                    string totalAmount = "￥" + order.TotalAmount.ToString("#,##0.00");
                    OrderGrid[r, 6] = new SourceGrid.Cells.Cell(totalAmount);
                    SourceGrid.Cells.Views.Cell view = new SourceGrid.Cells.Views.Cell();
                    view.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                    view.ForeColor = (order.TotalAmount > 0) ? Color.Red : Color.Blue;
                    OrderGrid[r, 6].View = view;
                }
                r++;
            }
            OrderGrid.ClipboardMode = SourceGrid.ClipboardMode.All;
        }

        private class MyHeader : SourceGrid.Cells.ColumnHeader
        {
            public MyHeader(object value) : base(value)
            {
                SourceGrid.Cells.Views.ColumnHeader view = new SourceGrid.Cells.Views.ColumnHeader();
                view.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
                View = view;
                AutomaticSortEnabled = false;
            }
        }

        private void FinDetailQueryBtn_Click(object sender, EventArgs e)
        {
            BindDataWithPage();
        }

        private void clickEvent_Click(object sender, EventArgs e)
        {
            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            int id = (int)OrderGrid.Rows[context.Position.Row].Tag;
            Order o = finOrderManager.GetOrderById(id);
            
            if (this.IsFinOrderView)
            {
                OrderFinDetailForm f = new OrderFinDetailForm();
                f.OrderNo = o.OrderNo;
                f.ShowDialog(this);
            }
            else {
                NewOrderForm f = new NewOrderForm();
                f.UpdateOrder = finOrderManager.GetOrderById(id);
                f.ShowDialog(this);
            
            }
        }

        private void ExpBtn_Click(object sender, EventArgs e)
        {
            QueryObject<Order> query = GetQueryObject();
            QueryObject<Order> result = finOrderManager.GetOrders(query);

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
                    exporter.AddColumn("BeginDate", "开始日期");
                    exporter.AddColumn("EndDate", "结束日期");
                    exporter.AddColumn("OrderNo", "订单编号");
                    exporter.AddColumn("Description", "订单描述");
                    exporter.AddColumn("SalesMan", "业务员");
                    exporter.AddColumn("Status", "订单状态");
                    if (!IsFinOrderView) {
                        exporter.AddColumn("Remark", "备注");
                    } else {
                        exporter.AddColumn("TotalAmount", "业务总金额");
                    }
                    exporter.ExportToExcel<Order>(result.Result, localFilePath);
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
