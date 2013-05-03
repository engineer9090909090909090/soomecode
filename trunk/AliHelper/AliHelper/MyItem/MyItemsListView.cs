using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soomes;
using System.IO;
using System.Threading;

namespace AliHelper
{
    public partial class MyItemsListView : UserControl
    {
        private MyItemManager myItemManager;
        private List<Product> list;
        public Categories SelectedCategory
        {
            get 
            {
                return DataCache.Instance.SelectedCategory;
            }
            set
            {
                DataCache.Instance.SelectedCategory = value;
            }
        }

        public MyItemsListView()
        {
            InitializeComponent();
            myItemManager = new MyItemManager();
            MyItemManager.OnEditProductEvent += new NewEditItemEvent(MyItemManager_OnEditProductEvent);
            pager.PageSize = 50;
        }

        void MyItemManager_OnEditProductEvent(object sender, ItemEventArgs e)
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

        private void MyItemsListView_Load(object sender, EventArgs e)
        {
            
        }

        private void FinDetailQueryBtn_Click(object sender, EventArgs e)
        {
            BindDataWithPage(1);
        }

        public void BindDataWithPage(int Page)
        {
            QueryObject<Product> query = new QueryObject<Product>();
            query.Page = Page;
            query.PageSize = pager.PageSize;
            query.Condition = new Product();
            query.Condition.Name = this.ProductName.Text.Trim();
            query.Condition.Model = this.Model.Text.Trim();
            query.Condition.CategoryId = SelectedCategory.Id;
            QueryObject<Product> result = myItemManager.GetProducts(query);
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

        public void DoFill(List<Product> list)
        {
            MyItemGrid.Redim(0, 0);
            MyItemGrid.FixedRows = 1;
            MyItemGrid.FixedColumns = 1;
            MyItemGrid.EnableSort = true;
            MyItemGrid.Redim(list.Count + 1, 6);
            MyItemGrid.Rows[0].Height = 25;
            MyItemGrid[0, 0] = new MyHeader("图片");
            MyItemGrid[0, 0].Column.Width = 80;
            MyItemGrid[0, 1] = new MyHeader("名称标题");
            MyItemGrid[0, 1].Column.Width = MyItemGrid.Width - 460;
            MyItemGrid[0, 1].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            MyItemGrid[0, 2] = new MyHeader("型号");
            MyItemGrid[0, 2].Column.Width = 100;
            MyItemGrid[0, 2].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            MyItemGrid[0, 3] = new MyHeader("价格");
            MyItemGrid[0, 3].Column.Width = 100;
            MyItemGrid[0, 3].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            MyItemGrid[0, 4] = new MyHeader("最小订量");
            MyItemGrid[0, 4].Column.Width = 100;
            MyItemGrid[0, 4].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            MyItemGrid[0, 5] = new MyHeader("状态");
            MyItemGrid[0, 5].Column.Width = 80;
            MyItemGrid[0, 5].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            SourceGrid.Cells.Controllers.CustomEvents clickEvent = new SourceGrid.Cells.Controllers.CustomEvents();
            clickEvent.DoubleClick += new EventHandler(dbClickEvent_Click);
            int r = 1;
            foreach (Product item in list)
            {
                MyItemGrid.Rows[r].Tag = item.Id;
                MyItemGrid.Rows[r].Height = 80;
                Image image = AliHelper.Properties.Resources.no_image;
                if (item.Image != null)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(LoadingProductImage),
                            new object[] { item, r, SelectedCategory.Id });
                }
                MyItemGrid[r, 0] = new SourceGrid.Cells.Image(image);
                MyItemGrid[r, 0].AddController(clickEvent);
                MyItemGrid[r, 1] = new SourceGrid.Cells.Cell(item.Name);
                MyItemGrid[r, 1].AddController(clickEvent);
                MyItemGrid[r, 2] = new SourceGrid.Cells.Cell(item.Model);
                MyItemGrid[r, 2].AddController(clickEvent);
                MyItemGrid[r, 3] = new SourceGrid.Cells.Cell(item.Price.ToString("#,##0.00"));
                MyItemGrid[r, 3].AddController(clickEvent);
                MyItemGrid[r, 4] = new SourceGrid.Cells.Cell(item.Minimum +"PCS");
                MyItemGrid[r, 4].AddController(clickEvent);
                string status = item.Status == "A" ? "有效" : "无效";
                MyItemGrid[r, 5] = new SourceGrid.Cells.Cell(status);
                MyItemGrid[r, 5].AddController(clickEvent);
                r++;
            }
            MyItemGrid.ClipboardMode = SourceGrid.ClipboardMode.All;
        }

        private void LoadingProductImage(object obj)
        {
            object[] arg = (object[])obj;
            Product item = (Product)arg[0];
            int rowIndex = (int)arg[1];
            int categoryId = (int)arg[2];
            if (categoryId != SelectedCategory.Id)
            {
                return;
            }
            string imageFile = myItemManager.GetProductImageFile(item.Id, item.Image);
            this.BeginInvoke(new Action(() =>
            {
                MyItemGrid[rowIndex, 0].Value = ImageUtils.ResizeImage(imageFile, 80, 80);
            }));
        }

        private class MyHeader : SourceGrid.Cells.ColumnHeader
        {
            public MyHeader(object value)
                : base(value)
            {
                SourceGrid.Cells.Views.ColumnHeader view = new SourceGrid.Cells.Views.ColumnHeader();
                view.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
                view.BackColor = Color.Gray;
                View = view;
                AutomaticSortEnabled = false;
            }
        }

        private void dbClickEvent_Click(object sender, EventArgs e)
        {
            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            int id = (int)MyItemGrid.Rows[context.Position.Row].Tag;
            Product o = myItemManager.GetProductById(id);
            NewItemForm f = new NewItemForm();
            f.Text = "编辑产品信息";
            f.UpdatedProduct = o;
            f.ShowDialog(this);
        }

    }
}
