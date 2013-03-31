using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soomes;
using System.Net;
using System.Threading;
using System.IO;

namespace AliHelper
{
    public partial class ProductListView : UserControl
    {
        private ProductsManager productsManager;
        private int CurrentGroupId = 0;
        private bool IsWindowProduct = false;
        private int PrevSelectedId = 0;

        public ProductListView()
        {
            InitializeComponent();
            productsManager = new ProductsManager();
            pager1.PageSize = 20;
        }

        #region Load ProductGridView

        public void LoadDataGridView(int GroupId, bool isWindowProduct)
        {
            BindDataWithPage(1, GroupId, isWindowProduct);
        }

        private void pager1_PageIndexChanged(object sender, EventArgs e)
        {
            BindDataWithPage(pager1.PageIndex, CurrentGroupId, IsWindowProduct);
        }

        private void BindDataWithPage(int Page, int GroupId, bool isWindowProduct)
        {
            this.BeginInvoke(new Action(() =>
            {
                CurrentGroupId = GroupId;
                IsWindowProduct = isWindowProduct;
                ThreadPool.SetMinThreads(6, 40);
                ThreadPool.SetMaxThreads(10, 200);

                QueryObject<AliProduct> query = new QueryObject<AliProduct>();
                query.Page = Page;
                query.PageSize = pager1.PageSize;
                query.Condition = new AliProduct();
                query.Condition.GroupId = GroupId.ToString();
                query.Condition.IsWindowProduct = isWindowProduct;
                QueryObject<AliProduct> result = productsManager.GetProductList(query);
                if (result.Result != null && result.Result.Count > 0)
                {
                    pager1.PageIndex = result.Page;
                    pager1.PageSize = result.PageSize;
                    pager1.RecordCount = result.RecordCount;
                    DoFill(GroupId, result.Result);
                }
                else
                {
                    pager1.PageIndex = Page;
                    pager1.PageSize = 20;
                    pager1.RecordCount = 0;
                    DoFill(GroupId, result.Result);
                }
            }));
        }

        public void DoFill(int GroupId, List<AliProduct> list)
        {
            ProductGrid.Redim(0, 0);
            ProductGrid.FixedRows = 1;
            ProductGrid.EnableSort = true;
            ProductGrid.Redim(list.Count + 1, 8);
            ProductGrid.Rows[0].Height = 25;
            ProductGrid[0, 0] = new MyHeader("图片");
            ProductGrid[0, 0].Column.Width = 50;
            ProductGrid[0, 0].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            ProductGrid[0, 1] = new MyHeader("产品名称");
            ProductGrid[0, 1].Column.Width = 350;
            ProductGrid[0, 1].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            ProductGrid[0, 2] = new MyHeader("型号");
            ProductGrid[0, 2].Column.Width = 120;
            ProductGrid[0, 2].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            ProductGrid[0, 3] = new MyHeader("橱窗产品");
            ProductGrid[0, 3].Column.Width = 80;
            ProductGrid[0, 3].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            ProductGrid[0, 4] = new MyHeader("关键词");
            ProductGrid[0, 4].Column.Width = 150;
            ProductGrid[0, 4].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            ProductGrid[0, 5] = new MyHeader("产品状态");
            ProductGrid[0, 5].Column.Width = 70;
            ProductGrid[0, 5].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            ProductGrid[0, 6] = new MyHeader("所属成员");
            ProductGrid[0, 6].Column.Width = 100;
            ProductGrid[0, 6].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            ProductGrid[0, 7] = new MyHeader("更新时间");
            ProductGrid[0, 7].Column.Width = 100;
            ProductGrid[0, 7].AddController(new SourceGrid.Cells.Controllers.SortableHeader());
            SourceGrid.Cells.Controllers.CustomEvents clickEvent = new SourceGrid.Cells.Controllers.CustomEvents();
            clickEvent.Click += new EventHandler(clickEvent_Click);
            int r = 1;
            foreach (AliProduct item in list)
            {
                string imageFile = FileUtils.GetProductImagesFolder()
                                 + Path.DirectorySeparatorChar + item.Id + ".jpg";
                Image image = ImageUtils.ResizeImage(imageFile, 50, 50);
                ProductGrid.Rows[r].Tag = item.Id;
                ProductGrid.Rows[r].Height = 50;
                ProductGrid[r, 0] = new SourceGrid.Cells.Image(image);
                ProductGrid[r, 0].AddController(clickEvent);
                ProductGrid[r, 1] = new SourceGrid.Cells.Cell(item.Subject);
                ProductGrid[r, 1].AddController(clickEvent);
                ProductGrid[r, 2] = new SourceGrid.Cells.Cell(item.RedModel);
                ProductGrid[r, 2].AddController(clickEvent);
                ProductGrid[r, 3] = new SourceGrid.Cells.Cell(item.IsWindowProduct ? "是" : "");
                ProductGrid[r, 3].AddController(clickEvent);
                ProductGrid[r, 4] = new SourceGrid.Cells.Cell(item.Keywords.Replace(",", "\r\n"));
                ProductGrid[r, 4].AddController(clickEvent);
                ProductGrid[r, 5] = new SourceGrid.Cells.Cell(item.Status);
                ProductGrid[r, 5].AddController(clickEvent);
                ProductGrid[r, 6] = new SourceGrid.Cells.Cell(item.OwnerMemberName);
                ProductGrid[r, 6].AddController(clickEvent);
                ProductGrid[r, 7] = new SourceGrid.Cells.Cell(item.GmtModified);
                ProductGrid[r, 7].AddController(clickEvent);
                if (!File.Exists(imageFile))
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(LoadingProductImage),
                        new object[] { item, r, GroupId });
                }
                r++;
            }
            ProductGrid.ClipboardMode = SourceGrid.ClipboardMode.All;
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

        private void LoadingProductImage(object obj)
        {
            object[] arg = (object[])obj;
            AliProduct item = (AliProduct)arg[0];
            int rowIndex = (int)arg[1];
            int groupId = (int)arg[2];
            WebClient webClient = new WebClient();
            FileUtils.DownloadProductImage(webClient, item.AbsImageUrl, item.Id);
            webClient.Dispose();
            webClient = null;
            if (groupId != CurrentGroupId)
            {
                return;
            }
            this.BeginInvoke(new Action(() =>
            {
                string imageFile = FileUtils.GetProductImagesFolder()
                        + Path.DirectorySeparatorChar + item.Id + ".jpg";
                ProductGrid[rowIndex, 0].Value =
                        ImageUtils.ResizeImage(imageFile, 50, 50);

            }));
        }

        private void clickEvent_Click(object sender, EventArgs e)
        {
            SourceGrid.CellContext context = (SourceGrid.CellContext)sender;
            int id = (int)ProductGrid.Rows[context.Position.Row].Tag;
            
        }
        #endregion

        
    }
}
