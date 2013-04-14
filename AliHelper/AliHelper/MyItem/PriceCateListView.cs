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
    public partial class PriceCateListView : UserControl
    {
        private MyItemManager myItemManager;
        private List<PriceCate> list;

        public PriceCateListView()
        {
            InitializeComponent();
            myItemManager = new MyItemManager();
            pager.PageSize = 20;
        }

        private void PriceCateListView_Load(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }

        private void pager_PageIndexChanged(object sender, EventArgs e)
        {
            BindDataWithPage(pager.PageIndex);
        }

        private void BindDataWithPage(int Page)
        {
            QueryObject<PriceCate> query = new QueryObject<PriceCate>();
            query.Page = Page;
            query.PageSize = pager.PageSize;
            query.Condition = new PriceCate();
            QueryObject<PriceCate> result = myItemManager.GetPriceCates(query);
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

        public void DoFill(List<PriceCate> list)
        {
            PriceCateGrid.Redim(0, 0);
            PriceCateGrid.FixedRows = 1;
            PriceCateGrid.FixedColumns = 1;
            PriceCateGrid.EnableSort = true;
            PriceCateGrid.Redim(list.Count + 1, 7);
            PriceCateGrid.Rows[0].Height = 25;
            PriceCateGrid[0, 0] = new MyHeader("描述");
            PriceCateGrid[0, 0].Column.Width = 150;
            PriceCateGrid[0, 0].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            PriceCateGrid[0, 1] = new MyHeader("价格1");
            PriceCateGrid[0, 1].Column.Width = 100;
            PriceCateGrid[0, 1].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            PriceCateGrid[0, 2] = new MyHeader("价格2");
            PriceCateGrid[0, 2].Column.Width = 100;
            PriceCateGrid[0, 2].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            PriceCateGrid[0, 3] = new MyHeader("价格3");
            PriceCateGrid[0, 3].Column.Width = 100;
            PriceCateGrid[0, 3].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            PriceCateGrid[0, 4] = new MyHeader("价格4");
            PriceCateGrid[0, 4].Column.Width = 100;
            PriceCateGrid[0, 4].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            PriceCateGrid[0, 5] = new MyHeader("价格5");
            PriceCateGrid[0, 5].Column.Width = 100;
            PriceCateGrid[0, 5].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            PriceCateGrid[0, 6] = new MyHeader("状态");
            PriceCateGrid[0, 6].Column.Width = 80;
            PriceCateGrid[0, 6].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            SourceGrid.Cells.Controllers.CustomEvents clickEvent = new SourceGrid.Cells.Controllers.CustomEvents();
            clickEvent.DoubleClick += new EventHandler(dbClickEvent_Click);
            int r = 1;
            foreach (PriceCate item in list)
            {
                PriceCateGrid.Rows[r].Tag = item.Id;
                PriceCateGrid.Rows[r].Height = 25;
                PriceCateGrid[r, 0] = new SourceGrid.Cells.Cell(item.CateName);
                PriceCateGrid[r, 0].AddController(clickEvent);
                string p1 = item.UsePrice1 ? item.Price1Name + "\r\n" + item.Price1Val : "";
                PriceCateGrid[r, 1] = new SourceGrid.Cells.Cell(p1);
                PriceCateGrid[r, 1].AddController(clickEvent);
                string p2 = item.UsePrice2 ? item.Price2Name + "\r\n" + item.Price2Val : "";
                PriceCateGrid[r, 2] = new SourceGrid.Cells.Cell(p2);
                PriceCateGrid[r, 2].AddController(clickEvent);
                string p3 = item.UsePrice3 ? item.Price3Name + "\r\n" + item.Price3Val : "";
                PriceCateGrid[r, 3] = new SourceGrid.Cells.Cell(p3);
                PriceCateGrid[r, 3].AddController(clickEvent);
                string p4 = item.UsePrice4 ? item.Price4Name + "\r\n" + item.Price4Val : "";
                PriceCateGrid[r, 4] = new SourceGrid.Cells.Cell(p4);
                PriceCateGrid[r, 4].AddController(clickEvent);
                string p5 = item.UsePrice5 ? item.Price5Name + "\r\n" + item.Price5Val : "";
                PriceCateGrid[r, 5] = new SourceGrid.Cells.Button(p5);
                PriceCateGrid[r, 5].AddController(clickEvent);
                string status = item.Status == "A" ? "启用" : "未启用";
                PriceCateGrid[r, 6] = new SourceGrid.Cells.Button(status);
                PriceCateGrid[r, 6].AddController(clickEvent);
                r++;
            }
            PriceCateGrid.ClipboardMode = SourceGrid.ClipboardMode.All;
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
            int id = (int)PriceCateGrid.Rows[context.Position.Row].Tag;
            PriceCate o = myItemManager.GetPriceCateById(id);
        }

        private void CellButton_Click(object sender, EventArgs e)
        {
            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            int id = (int)PriceCateGrid.Rows[context.Position.Row].Tag;
        }
    }
}
