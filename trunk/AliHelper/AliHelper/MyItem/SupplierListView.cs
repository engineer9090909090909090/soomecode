using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soomes;

namespace AliHelper.MyItem
{
    public partial class SupplierListView : UserControl
    {

        private SupplierManager supplierManager;
        private List<Supplier> list;

        public SupplierListView()
        {
            InitializeComponent();
            supplierManager = new SupplierManager();
            SupplierManager.OnEditSupplierEvent += new NewEditItemEvent(SupplierManager_OnEditSupplierEvent);
            pager.PageSize = 20;
        }

        void SupplierManager_OnEditSupplierEvent(object sender, ItemEventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                BindDataWithPage(pager.PageIndex);
            }));
        }

        private void pager_PageIndexChanged(object sender, EventArgs e)
        {
            BindDataWithPage(pager.PageIndex);
        }

        private void SupplierListView_Load(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }

        private void QueryBtn_Click(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }

        private void BindDataWithPage(int Page)
        {
            QueryObject<Supplier> query = new QueryObject<Supplier>();
            query.Page = Page;
            query.PageSize = pager.PageSize;
            query.Condition = new Supplier();
            query.Condition.Name = this.SupplierName.Text.Trim();
            query.Condition.Remark = this.Remark.Text.Trim();
            QueryObject<Supplier> result = supplierManager.GetSupplierList(query);
            if (result.Result != null && result.Result.Count > 0)
            {
                pager.PageIndex = result.Page;
                pager.PageSize = result.PageSize;
                pager.RecordCount = result.RecordCount;
                list = result.Result;
                DoFill(list);
            }
            else
            {
                pager.PageIndex = Page;
                pager.PageSize = 20;
                pager.RecordCount = 0;
                list = result.Result;
                DoFill(list);
            }
        }

        public void DoFill(List<Supplier> list)
        {
            SupplierGrid.Redim(0, 0);
            SupplierGrid.FixedRows = 1;
            SupplierGrid.FixedColumns = 1;
            SupplierGrid.EnableSort = true;
            SupplierGrid.Redim(list.Count + 1, 5);
            SupplierGrid.Rows[0].Height = 25;
            SupplierGrid[0, 0] = new MyHeader("公司");
            SupplierGrid[0, 0].Column.Width = 250;
            SupplierGrid[0, 0].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            SupplierGrid[0, 1] = new MyHeader("联系人");
            SupplierGrid[0, 1].Column.Width = 200;
            SupplierGrid[0, 1].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            SupplierGrid[0, 2] = new MyHeader("备注");
            SupplierGrid[0, 2].Column.Width = 300;
            SupplierGrid[0, 2].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            SupplierGrid[0, 3] = new MyHeader("地址");
            SupplierGrid[0, 3].Column.Width = 200;
            SupplierGrid[0, 3].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            SupplierGrid[0, 4] = new MyHeader("");
            SupplierGrid[0, 4].Column.Width = 100;
            SourceGrid.Cells.Controllers.CustomEvents clickEvent = new SourceGrid.Cells.Controllers.CustomEvents();
            clickEvent.DoubleClick += new EventHandler(dbClickEvent_Click);
            int r = 1;
            foreach (Supplier item in list)
            {
                SupplierGrid.Rows[r].Tag = item.Id;
                SupplierGrid.Rows[r].Height = 35;
                SupplierGrid[r, 0] = new SourceGrid.Cells.Cell(item.Name);
                SupplierGrid[r, 0].AddController(clickEvent);
                SupplierGrid[r, 1] = new SourceGrid.Cells.Cell(item.Contact);
                SupplierGrid[r, 1].AddController(clickEvent);
                SupplierGrid[r, 2] = new SourceGrid.Cells.Cell(item.Remark);
                SupplierGrid[r, 2].AddController(clickEvent);
                SupplierGrid[r, 3] = new SourceGrid.Cells.Cell(item.Address);
                SupplierGrid[r, 3].AddController(clickEvent);
                SupplierGrid[r, 4] = new SourceGrid.Cells.Button("产品列表");
                SourceGrid.Cells.Controllers.Button buttonClickEvent = new SourceGrid.Cells.Controllers.Button();
                buttonClickEvent.Executed += new EventHandler(CellButton_Click);
                SupplierGrid[r, 4].Controller.AddController(buttonClickEvent);
                r++;
            }
            SupplierGrid.ClipboardMode = SourceGrid.ClipboardMode.All;
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

        private void dbClickEvent_Click(object sender, EventArgs e)
        {
            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            int id = (int)SupplierGrid.Rows[context.Position.Row].Tag;
            Supplier o = supplierManager.GetSupplierById(id);
            SupplierForm f = new SupplierForm();
            f.Text = "编辑供应商信息";
            f.UpdateSupplier = o;
            f.ShowDialog(this);
        }

        private void CellButton_Click(object sender, EventArgs e)
        {
            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            int id = (int)SupplierGrid.Rows[context.Position.Row].Tag;
        }

        
    }
}
