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
    public partial class OrderTrackView : UserControl
    {
        private FinOrderManager finOrderManager;
        private List<Order> list;

        public OrderTrackView()
        {
            InitializeComponent();
            finOrderManager = new FinOrderManager();
            FinOrderManager.OnEditTrackingEvent -= new NewEditItemEvent(OnEditTrackingEvent);
            FinOrderManager.OnEditTrackingEvent += new NewEditItemEvent(OnEditTrackingEvent);
        }

        void OnEditTrackingEvent(object sender, ItemEventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                OrderTracking o = (OrderTracking)e.Item;
                BindDataWithPage();
                for (int i = 0; i < list.Count; i++)
                {
                    Order order = (Order)list[i];
                    if (order.Id == o.OrderId)
                    {
                        OrderGrid.Selection.FocusRow(i + 1);
                        break;
                    }
                }
                List<OrderTracking> trackingList = finOrderManager.GetOrderTrackingList(o.OrderId);
                DoFillTracking(trackingList);
            }));

        }

        private void OrderTrackView_Load(object sender, EventArgs e)
        {
            DateTime beginTime = DateTime.Now.AddMonths(-2);
            this.BeginDateForm.Value = new DateTime(beginTime.Year, beginTime.Month, 1);
            this.BeginDateTo.Value = DateTime.Now;
            this.SalesMan.DisplayMember = "Label";
            this.SalesMan.ValueMember = "Key";
            this.SalesMan.DataSource = finOrderManager.GetQueryAppDicOptions(Constants.Employee);
            BindDataWithPage();
        }

        private void FinDetailQueryBtn_Click(object sender, EventArgs e)
        {
            BindDataWithPage();
        }

        private void BindDataWithPage()
        {
            QueryObject<Order> query = new QueryObject<Order>();
            query.Condition = new Order();
            query.Condition.BeginDateForm = this.BeginDateForm.Value.ToString(Constants.DateFormat);
            query.Condition.BeginDateTo = this.BeginDateTo.Value.ToString(Constants.DateFormat);
            query.Condition.OrderNo = this.OrderNo.Text.Trim();
            query.Condition.SalesMan = (string)this.SalesMan.SelectedValue;
            QueryObject<Order> result = finOrderManager.GetOrders(query);
            list = result.Result;
            DoFill(list);
        }

        public void DoFill(List<Order> list)
        {
            OrderGrid.Redim(0, 0);
            OrderGrid.FixedRows = 1;
            OrderGrid.FixedColumns = 1;
            OrderGrid.EnableSort = true;
            OrderGrid.Redim(list.Count + 1, 8);
            OrderGrid.Rows[0].Height = 25;
            OrderGrid[0, 0] = new MyHeader("");
            OrderGrid[0, 0].Column.Width = 50;
            OrderGrid[0, 1] = new MyHeader("开始日期");
            OrderGrid[0, 1].Column.Width = 100;
            OrderGrid[0, 1].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            OrderGrid[0, 2] = new MyHeader("结束日期");
            OrderGrid[0, 2].Column.Width = 100;
            OrderGrid[0, 2].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            OrderGrid[0, 3] = new MyHeader("订单编号");
            OrderGrid[0, 3].Column.Width = 100;
            OrderGrid[0, 3].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            OrderGrid[0, 4] = new MyHeader("订单描述");
            OrderGrid[0, 4].Column.Width = 300;
            OrderGrid[0, 4].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            OrderGrid[0, 5] = new MyHeader("业务员");
            OrderGrid[0, 5].Column.Width = 100;
            OrderGrid[0, 5].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            OrderGrid[0, 6] = new MyHeader("订单状态");
            OrderGrid[0, 6].Column.Width = 150;
            OrderGrid[0, 6].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            OrderGrid[0, 7] = new MyHeader("备注");
            OrderGrid[0, 7].Column.Width = 200;
            SourceGrid.Cells.Controllers.CustomEvents clickEvent = new SourceGrid.Cells.Controllers.CustomEvents();
            clickEvent.DoubleClick += new EventHandler(dbClickEvent_Click);
            clickEvent.Click += new EventHandler(clickEvent_Click);
            int r = 1;
            foreach (Order order in list)
            {
                OrderGrid.Rows[r].Tag = order.Id;
                OrderGrid.Rows[r].Height = 25;
                if (order.EndDate != null)
                {
                    OrderGrid[r, 0] = new SourceGrid.Cells.Button("跟进");
                    SourceGrid.Cells.Controllers.Button buttonClickEvent = new SourceGrid.Cells.Controllers.Button();
                    buttonClickEvent.Executed += new EventHandler(CellButton_Click);
                    OrderGrid[r, 0].Controller.AddController(buttonClickEvent);
                }
                else 
                {
                    OrderGrid[r, 0] = null;
                }
                OrderGrid[r, 1] = new SourceGrid.Cells.Cell(order.BeginDate);
                OrderGrid[r, 1].AddController(clickEvent);
                OrderGrid[r, 2] = new SourceGrid.Cells.Cell(order.EndDate);
                OrderGrid[r, 2].AddController(clickEvent);
                OrderGrid[r, 3] = new SourceGrid.Cells.Cell(order.OrderNo);
                OrderGrid[r, 3].AddController(clickEvent);
                OrderGrid[r, 4] = new SourceGrid.Cells.Cell(order.Description);
                OrderGrid[r, 4].AddController(clickEvent);
                OrderGrid[r, 5] = new SourceGrid.Cells.Cell(order.SalesMan);
                OrderGrid[r, 6] = new SourceGrid.Cells.Cell(order.Status);
                OrderGrid[r, 7] = new SourceGrid.Cells.Cell(order.Remark);
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

        private void dbClickEvent_Click(object sender, EventArgs e)
        {
            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            int id = (int)OrderGrid.Rows[context.Position.Row].Tag;
            Order o = finOrderManager.GetOrderById(id);
            NewOrderForm f = new NewOrderForm();
            f.UpdateOrder = finOrderManager.GetOrderById(id);
            f.ShowDialog(this);
        }

        private void clickEvent_Click(object sender, EventArgs e)
        {
            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            int id = (int)OrderGrid.Rows[context.Position.Row].Tag;
            Order o = finOrderManager.GetOrderById(id);
            List<OrderTracking> trackingList = finOrderManager.GetOrderTrackingList(o.Id);
            DoFillTracking(trackingList);
        }

        private void CellButton_Click(object sender, EventArgs e)
        {
            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            int id = (int)OrderGrid.Rows[context.Position.Row].Tag;
            OrderTrackForm f = new OrderTrackForm();
            f.UpdateOrder = finOrderManager.GetOrderById(id);
            f.ShowDialog(this);
        }

        public void DoFillTracking(List<OrderTracking> list)
        {
            TrackGrid.Redim(0, 0);
            TrackGrid.FixedRows = 1;
            TrackGrid.EnableSort = true;
            TrackGrid.Redim(list.Count + 1, 4);
            TrackGrid.Rows[0].Height = 25;
            TrackGrid[0, 0] = new MyHeader("跟踪日期");
            TrackGrid[0, 0].Column.Width = 100;
            TrackGrid[0, 0].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            TrackGrid[0, 1] = new MyHeader("描述");
            TrackGrid[0, 1].Column.Width = 650;
            TrackGrid[0, 1].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            TrackGrid[0, 2] = new MyHeader("状态");
            TrackGrid[0, 2].Column.Width = 150;
            TrackGrid[0, 2].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            TrackGrid[0, 3] = new MyHeader("跟踪人");
            TrackGrid[0, 3].Column.Width = 100;
            SourceGrid.Cells.Controllers.CustomEvents trackEvents = new SourceGrid.Cells.Controllers.CustomEvents();
            trackEvents.DoubleClick += new EventHandler(trackEvent_Click);
            int r = 1;
            foreach (OrderTracking tracking in list)
            {
                TrackGrid.Rows[r].Tag = tracking.Id;
                TrackGrid.Rows[r].Height = 22;
                TrackGrid[r, 0] = new SourceGrid.Cells.Cell(tracking.TrackingDate);
                TrackGrid[r, 0].AddController(trackEvents);
                TrackGrid[r, 1] = new SourceGrid.Cells.Cell(tracking.Description);
                TrackGrid[r, 1].AddController(trackEvents);
                TrackGrid[r, 2] = new SourceGrid.Cells.Cell(tracking.Status);
                TrackGrid[r, 2].AddController(trackEvents);
                TrackGrid[r, 3] = new SourceGrid.Cells.Cell(tracking.Tracker);
                TrackGrid[r, 3].AddController(trackEvents);
                r++;
            }
            TrackGrid.ClipboardMode = SourceGrid.ClipboardMode.All;
        }

        private void trackEvent_Click(object sender, EventArgs e)
        {
            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            int id = (int)TrackGrid.Rows[context.Position.Row].Tag;
            OrderTrackForm f = new OrderTrackForm();
            f.UpdateOrderTracking = finOrderManager.GetOrderTrackingById(id);
            f.ShowDialog(this);
        }

    }
}
