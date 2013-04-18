    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using Soomes;
using System.Collections;
using System.Resources;
using System.Reflection;
using AliHelper.MyItem;

namespace AliHelper
{
    public partial class MainForm : Form
    {
        private BaseManager configManager;
        private ProductsManager productsManager;
        private MyItemManager myItemsManager;
        private ImpProductDetail impProductDetail;
        private string ExplorerCurrentView;
        private string CurrentToolsStrip;
 
        #region 构造方法
        public MainForm()
        {
            configManager = new BaseManager();
            configManager.InitDbConfig();
            InitializeComponent();
            productsManager = new ProductsManager();
            impProductDetail = new ImpProductDetail();
            myItemsManager = new MyItemManager();
            List<AliGroup> groups = productsManager.GetGroupList();
            if (groups.Count == 0)
            {
                groups = HttpClient.GetGroups(-1, 0, DataCache.Instance.CsrfToken);
                productsManager.UpdateGroups(groups);
            }
            impProductDetail.InitDataCacheFormOptions();
            productsManager.InitGroupOptions();
             
            LoadNavigatorBar();
            UpdateGroupUI(groups);
            LoadProductListView();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //HttpClient.GetMailList();
            CheckForIllegalCrossThreadCalls = false;
            ShowToolsStrip("ProductsStrip");
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            productsManager = null;
            impProductDetail = null;
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                //this.Hide();
            }
        }

        private void AppNotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                //this.WindowState = FormWindowState.Minimized;
                //this.Hide();
            }
        }

        #endregion

        #region NavigatorBar 处理

        protected void LoadNavigatorBar()
        {
            ToolStripMenuItem UpdateGroupItem = new ToolStripMenuItem();
            UpdateGroupItem.Name = "UpdateGroupItem";
            UpdateGroupItem.Size = new System.Drawing.Size(146, 22);
            UpdateGroupItem.Text = "更新本组产品";
            UpdateGroupItem.Click += new EventHandler(UpdateGroupItem_Click);

            ContextMenuStrip TreeNodeContextMenuStrip = new ContextMenuStrip();
            TreeNodeContextMenuStrip.Items.AddRange(
                new System.Windows.Forms.ToolStripItem[] { UpdateGroupItem });
            TreeNodeContextMenuStrip.Name = "TreeNodeContextMenuStrip";
            TreeNodeContextMenuStrip.Size = new System.Drawing.Size(147, 20);

            this.treeView1 = new System.Windows.Forms.TreeView();
            this.treeView1.LineColor = System.Drawing.Color.Empty;
            this.treeView1.Location = new System.Drawing.Point(0, 23);
            this.treeView1.Name = "treeView1";
            this.treeView1.Dock = DockStyle.Fill;
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.ItemHeight = 18;
            //this.treeView1.Font = new System.Drawing.Font("Microsoft YaHei", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.Size = new System.Drawing.Size(130, 423);
            this.treeView1.TabIndex = 4;
            this.treeView1.ContextMenuStrip = TreeNodeContextMenuStrip;
            this.treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            ApTreeNaviGroup.Controls.Add(treeView1);

            ListView listView1 = new ListView();
            listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            ColumnHeader nameColumnHeader = new System.Windows.Forms.ColumnHeader();
            nameColumnHeader.Text = "名称";
            nameColumnHeader.Width = 80;
            ColumnHeader timeColumnHeader = new System.Windows.Forms.ColumnHeader();
            timeColumnHeader.Text = "重发时间";
            timeColumnHeader.Width = 98;
            listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { nameColumnHeader, timeColumnHeader });
            listView1.Name = "listView1";
            listView1.AllowDrop = false;
            listView1.HideSelection = false;
            listView1.Location = new System.Drawing.Point(0, 23);
            listView1.Size = new System.Drawing.Size(130, 423);
            listView1.CheckBoxes = true;
            listView1.Dock = DockStyle.Fill;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = System.Windows.Forms.View.Details;
            listView1.ContextMenuStrip = this.CfContextMenuStrip;
            UpdateBand.ClientArea.Controls.Add(listView1);

            ImageList outlookLargeIcons = new ImageList();
            outlookLargeIcons.ImageSize = new Size(32, 32);
            Bitmap icons = (Bitmap)global::AliHelper.Properties.IconImages.OutlookLargeIcons;
            icons.MakeTransparent(Color.FromArgb(255, 0, 255));
            outlookLargeIcons.Images.AddStrip(icons);
            ListViewItem listViewItem1 = new ListViewItem("收件箱", 1);
            listViewItem1.Name = MailQueryListType.Inbox;
            ListViewItem listViewItem2 = new ListViewItem("已发送", 3);
            listViewItem2.Name = MailQueryListType.Sendbox;
            ListViewItem listViewItem3 = new ListViewItem("垃圾箱", 5);
            listViewItem3.Name = MailQueryListType.Trash;
            ListViewItem listViewItem4 = new ListViewItem("已删除", 6);
            listViewItem4.Name = MailQueryListType.Spam;
            ListView MailListView = new System.Windows.Forms.ListView();
            MailListView.Name = "MailListView";
            MailListView.BorderStyle = BorderStyle.None;
            MailListView.Location = new System.Drawing.Point(40, 20);
            MailListView.Size = new System.Drawing.Size(80, 300);
            MailListView.LargeImageList = outlookLargeIcons;
            MailListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,listViewItem2,listViewItem3,listViewItem4});
            MailListView.UseCompatibleStateImageBehavior = false;
            MailListView.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(MailListView_ItemSelectionChanged);
            MailListView.View = System.Windows.Forms.View.LargeIcon;
            InquiryBand.ClientArea.Controls.Add(MailListView);


            ImageList orderLargeIcons = new ImageList();
            orderLargeIcons.ImageSize = new Size(32, 32);
            Bitmap orderIcons = (Bitmap)global::AliHelper.Properties.IconImages.OutlookLargeIcons;
            icons.MakeTransparent(Color.FromArgb(255, 0, 255));
            orderLargeIcons.Images.AddStrip(orderIcons);
            ListViewItem orderlistViewItem1 = new ListViewItem("订单列表", 1);
            orderlistViewItem1.Name = Constants.OrderBaseView;
            ListViewItem orderlistViewItem2 = new ListViewItem("订单跟踪", 3);
            orderlistViewItem2.Name = Constants.OrderTrackView;
            ListView OrderListView = new System.Windows.Forms.ListView();
            OrderListView.Name = "OrderListView";
            OrderListView.BorderStyle = BorderStyle.None;
            OrderListView.Location = new System.Drawing.Point(40, 20);
            OrderListView.Size = new System.Drawing.Size(80, 300);
            OrderListView.LargeImageList = outlookLargeIcons;
            OrderListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {orderlistViewItem1,orderlistViewItem2});
            OrderListView.UseCompatibleStateImageBehavior = false;
            OrderListView.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(OrderListView_ItemSelectionChanged);
            OrderListView.View = System.Windows.Forms.View.LargeIcon;
            OrderBand.ClientArea.Controls.Add(OrderListView);


            ImageList FinOutlookLargeIcons = new ImageList();
            FinOutlookLargeIcons.ImageSize = new Size(32, 32);
            Bitmap finIcons = (Bitmap)global::AliHelper.Properties.IconImages.OutlookLargeIcons;
            finIcons.MakeTransparent(Color.FromArgb(255, 0, 255));
            FinOutlookLargeIcons.Images.AddStrip(finIcons);
            ListViewItem baseViewItem = new ListViewItem("详细账目", 0);
            baseViewItem.Name = Constants.FinanceBaseView;
            ListViewItem bussViewItem = new ListViewItem("业务账目", 2);
            bussViewItem.Name = Constants.FinanceBizView;
            ListViewItem waterViewItem = new ListViewItem("流水账目", 4);
            waterViewItem.Name = Constants.FinanceWaterView;
            ListView FinListView = new System.Windows.Forms.ListView();
            FinListView.Name = "FinListView";
            FinListView.BorderStyle = BorderStyle.None;
            FinListView.Location = new System.Drawing.Point(40, 20);
            FinListView.Size = new System.Drawing.Size(80, 300);
            FinListView.LargeImageList = outlookLargeIcons;
            FinListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            baseViewItem,bussViewItem,waterViewItem});
            FinListView.UseCompatibleStateImageBehavior = false;
            FinListView.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(FinListView_ItemSelectionChanged);
            FinListView.View = System.Windows.Forms.View.LargeIcon;
            FinanceBand.ClientArea.Controls.Add(FinListView);
        }

        #region TreeView NodeMouse 事件处理

        void treeView1_NodeMouseClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TreeNode currentNode = e.Node;
                LoadProductListView();
                Control view = this.Explorer.Controls[0];
                AliGroup group = (AliGroup)currentNode.Tag;
                if (currentNode.Name == Constants.ProductTypeAll)
                {
                    ((ProductListView)view).LoadDataGridView(-1, false);
                }else if (currentNode.Name == Constants.ProductTypeWin)
                {
                    ((ProductListView)view).LoadDataGridView(-1, true);
                }else if (group != null)
                {
                    ((ProductListView)view).LoadDataGridView(group.Id, false);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                TreeNode currentNode = e.Node;
                AliGroup group = (AliGroup)currentNode.Tag;
                this.treeView1.ContextMenuStrip.Tag = group;
                if (group != null)
                {
                    this.treeView1.ContextMenuStrip.Show();
                }
            }
        }
        #endregion
        

        void OrderListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                UnLoadExplorerSubPanel();
                if (e.Item.Name == Constants.OrderBaseView)
                {
                    LoadOrderFanViewPanel(false);
                }
                else if (e.Item.Name == Constants.OrderTrackView)
                {
                    LoadOrderTrackViewPanel();
                }
            }
        }

        void MailListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                string ItemName = e.Item.Name;
            }
        }

        void FinListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                UnLoadExplorerSubPanel();
                if (e.Item.Name == Constants.FinanceWaterView)
                {
                    LoadFinWaterViewPanel();
                }
                else if (e.Item.Name == Constants.FinanceBaseView)
                {
                    LoadFinViewPanel();
                }
                else if (e.Item.Name == Constants.FinanceBizView)
                {
                    LoadOrderFanViewPanel(true);
                }
            }
        }


        private void NavigatorBar_ActiveBandChanged(object sender, EventArgs e)
        {
            string bandName = NavigatorBar.ActiveBand.Name;
            if (bandName == "ProductBand")
            {
                ShowToolsStrip("ProductsStrip");
                LoadProductListView();
            }
            else if (bandName == "UpdateBand")
            {
                ShowToolsStrip("ProductsStrip");
            }
            else if (bandName == "InquiryBand")
            {
                ShowToolsStrip("ProductsStrip");
                LoadMailViewPanel();
            }
            else if (bandName == "MyItemBand")
            {
                ShowToolsStrip("MyItemStrip");
                LoadMyItemCategoriesTreeView();
                LoadMyItemsListView();
            }
            else if (bandName == "ClientBand")
            {
                ShowToolsStrip("MyItemStrip");
            }
            else if (bandName == "OrderBand")
            {
                ShowToolsStrip("FinToolStrip");
                LoadOrderFanViewPanel(false);
            }
            else if (bandName == "FinanceBand")
            {
                ShowToolsStrip("FinToolStrip");
                LoadFinViewPanel();
            }
            GC.Collect();
        }

        #endregion

        
        #region 更新产品目录
        /*
        private void updateGroup_Click(object sender, EventArgs e)
        {
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_UpdateGroup);
            bgWorker.RunWorkerAsync();
            bgWorker.Dispose();
        }

        void bgWorker_UpdateGroup(object sender, DoWorkEventArgs e)
        {
            if (string.IsNullOrEmpty(DataCache.Instance.CsrfToken))
            {
                return;
            }
            List<AliGroup> groups = HttpClient.GetGroups(-1, 0, DataCache.Instance.CsrfToken);
            productsManager.UpdateGroups(groups);
            this.BeginInvoke(new Action(() =>
            {
                UpdateGroupUI(groups);
            }));
        }
         * */
        #endregion

        #region 更新产品

        private AliGroup NeedUpdGroup;
        void UpdateGroupItem_Click(object sender, EventArgs e)
        {
            NeedUpdGroup = (AliGroup)this.treeView1.ContextMenuStrip.Tag;
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerAsync();
            bgWorker.Dispose();
        }
        
        private void updateAllProduct_Click(object sender, EventArgs e)
        {
            NeedUpdGroup = null;
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerAsync();
            bgWorker.Dispose();
        }

        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (string.IsNullOrEmpty(DataCache.Instance.CsrfToken))
            {
                return;
            }
            if (NeedUpdGroup == null)
            {
                List<AliGroup> groups = HttpClient.GetGroups(-1, 0, DataCache.Instance.CsrfToken);
                productsManager.UpdateGroups(groups);
                GetGroupProduct(groups, DataCache.Instance.CsrfToken);
                this.BeginInvoke(new Action(() =>
                {
                    UpdateGroupUI(groups);

                }));
            }
            else 
            {
                List<AliGroup> groups = new List<AliGroup>();
                groups.Add(NeedUpdGroup);
                GetGroupProduct(groups, DataCache.Instance.CsrfToken);
            }
        }
        #endregion

        #region 更新图片
        private void updateAllImages_Click(object sender, EventArgs e)
        {
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_UpdateImages);
            bgWorker.RunWorkerAsync();
            bgWorker.Dispose();
        }

        void bgWorker_UpdateImages(object sender, DoWorkEventArgs e)
        {
            List<ImageInfo> imageList = HttpClient.GetAllImages();
            productsManager.UpdateImageInfos(imageList);
        }
        #endregion

        #region ToolButton 事件处理
        public void UpdateGroupUI(List<AliGroup> groups)
        {
            treeView1.Nodes.Clear();
            TreeNode t = new TreeNode("所有产品");//作为根节点
            t.Name = Constants.ProductTypeAll;
            treeView1.Nodes.Add(t);
            TreeNode st = new TreeNode("橱窗产品");
            st.Name = Constants.ProductTypeWin;
            treeView1.Nodes.Add(st);
            foreach (AliGroup p in groups)
            {
                if (p.Level == 1)
                {
                    TreeNode t1 = new TreeNode(p.Name);
                    t1.Tag = p;
                    treeView1.Nodes.Add(t1);
                    if (p.HasChildren)
                    {
                        foreach (AliGroup c in groups)
                        {
                            if (c.ParentId == p.Id && c.Level == p.Level + 1)
                            {
                                TreeNode t2 = new TreeNode(c.Name);
                                t2.Tag = c;
                                t1.Nodes.Add(t2);
                            }
                        }
                    }
                }
            }
        }

        public void GetGroupProduct(List<AliGroup> groups, string csrfToken)
        {
            Hashtable groupDic = new Hashtable();
            foreach (AliGroup group in groups)
            {
                groupDic.Add(group.Name, group.Id);
            }
            List<AliProduct> productList = new List<AliProduct>();
            WebClient webClient = new WebClient();
            foreach (AliGroup group in groups)
            {
                List<AliProduct> products = HttpClient.GetProducts(groupDic, group.Id, group.Level, csrfToken);
                productsManager.UpdateGroupProdcuts(group.Id, products);
                foreach (AliProduct item in products)
                {
                    FileUtils.DownloadProductImage(webClient, item.AbsImageUrl, item.Id);
                    /*
                    if (productsManager.IsNeedUpdateDetail(item.Id))
                    {
                        ProductDetail detail = impProductDetail.GetEditFormElements(item);
                        productsManager.InsertOrUpdateProdcutDetail(detail);
                        productList.Add(item);
                    }
                     */
                }
            }
            webClient.Dispose();
            webClient = null;
        }

        private void newProductBtn_Click(object sender, EventArgs e)
        {
            EditCategory f = new EditCategory();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }
        #endregion


        #region ExplorerPanel 处理

        private void UnLoadExplorerSubPanel()
        {
            if (this.Explorer.Controls.Count > 0)
            {
                Control SubPanel = this.Explorer.Controls[0];
                this.Explorer.Controls.RemoveAt(0);
                SubPanel.Dispose();
                SubPanel = null;
            }
            GC.Collect();
        }

        private void ShowToolsStrip(string name)
        {
            if (CurrentToolsStrip == name)
            {
                return;
            }
            if (this.toolStripContainer.TopToolStripPanel.Controls.Count > 0)
            {
                foreach (Control SubPanel in toolStripContainer.TopToolStripPanel.Controls)
                {
                    if (SubPanel.Name == name)
                    {
                        SubPanel.Show();
                    }
                }
                foreach (Control SubPanel in toolStripContainer.TopToolStripPanel.Controls)
                {
                    if (SubPanel.Name != name)
                    {
                        SubPanel.Hide();
                    }
                }
                
            }
            CurrentToolsStrip = name;
        }

        private void LoadFinViewPanel()
        {
            string LoadViewName = "FinBaseView";
            if (this.Explorer.Controls.Count > 0)
            {
                Control SubPanel = this.Explorer.Controls[0];
                if (SubPanel.Name == LoadViewName)
                {
                    return;
                }
            }
            UnLoadExplorerSubPanel();
            FinView FinBaseView = new AliHelper.FinView();
            this.Explorer.SuspendLayout();
            FinBaseView.Location = new System.Drawing.Point(0, 0);
            FinBaseView.Name = LoadViewName;
            Explorer.Name = LoadViewName;
            FinBaseView.AutoSize = true;
            FinBaseView.TabIndex = 1;
            FinBaseView.Size = new System.Drawing.Size(this.Explorer.Width, this.Explorer.Height);
            FinBaseView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Explorer.Controls.Add(FinBaseView);
            this.Explorer.ResumeLayout(false);
        }

        private void LoadOrderFanViewPanel(bool IsFinOrderView)
        {
            string LoadViewName = "FinOrderView";
            if (this.Explorer.Controls.Count > 0)
            {
                Control SubPanel = this.Explorer.Controls[0];
                if (SubPanel.Name == LoadViewName)
                {
                    return;
                }
            }
            UnLoadExplorerSubPanel();
            OrderView FinorderView = new AliHelper.OrderView();
            this.Explorer.SuspendLayout();
            FinorderView.IsFinOrderView = IsFinOrderView;
            FinorderView.Location = new System.Drawing.Point(0, 0);
            FinorderView.Name = LoadViewName;
            Explorer.Name = LoadViewName;
            FinorderView.AutoSize = true;
            FinorderView.TabIndex = 1;
            FinorderView.Size = new System.Drawing.Size(this.Explorer.Width, this.Explorer.Height);
            FinorderView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Explorer.Controls.Add(FinorderView);
            this.Explorer.ResumeLayout(false);
        }

        private void LoadOrderTrackViewPanel()
        {
            string LoadViewName = "OrderTrackView";
            if (this.Explorer.Controls.Count > 0)
            {
                Control SubPanel = this.Explorer.Controls[0];
                if (SubPanel.Name == LoadViewName)
                {
                    return;
                }
            }
            UnLoadExplorerSubPanel();
            OrderTrackView OrderTrackView = new AliHelper.OrderTrackView();
            this.Explorer.SuspendLayout();
            OrderTrackView.Location = new System.Drawing.Point(0, 0);
            OrderTrackView.Name = LoadViewName;
            Explorer.Name = LoadViewName;
            OrderTrackView.AutoSize = true;
            OrderTrackView.Size = new System.Drawing.Size(this.Explorer.Width, this.Explorer.Height);
            OrderTrackView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Explorer.Controls.Add(OrderTrackView);
            this.Explorer.ResumeLayout(false);
        }

        private void LoadFinWaterViewPanel()
        {
            string LoadViewName = "FinanceWaterView";
            if (this.Explorer.Controls.Count > 0)
            {
                Control SubPanel = this.Explorer.Controls[0];
                if (SubPanel.Name == LoadViewName)
                {
                    return;
                }
            }
            UnLoadExplorerSubPanel();
            WaterView FinanceWaterView = new AliHelper.WaterView();
            this.Explorer.SuspendLayout();
            FinanceWaterView.Location = new System.Drawing.Point(0, 0);
            FinanceWaterView.Name = LoadViewName;
            Explorer.Name = LoadViewName;
            FinanceWaterView.AutoSize = true;
            FinanceWaterView.TabIndex = 1;
            FinanceWaterView.Size = new System.Drawing.Size(this.Explorer.Width, this.Explorer.Height);
            FinanceWaterView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Explorer.Controls.Add(FinanceWaterView);
            this.Explorer.ResumeLayout(false);
        }

        private void LoadMailViewPanel()
        {
            string LoadViewName = "MailView";
            if (this.Explorer.Controls.Count > 0)
            {
                Control SubPanel = this.Explorer.Controls[0];
                if (SubPanel.Name == LoadViewName)
                {
                    return;
                }
            }
            UnLoadExplorerSubPanel();
            MailView view = new AliHelper.MailView();
            this.Explorer.SuspendLayout();
            view.Location = new System.Drawing.Point(0, 0);
            view.Name = LoadViewName;
            Explorer.Name = LoadViewName;
            view.AutoSize = true;
            view.TabIndex = 1;
            view.Size = new System.Drawing.Size(this.Explorer.Width, this.Explorer.Height);
            view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Explorer.Controls.Add(view);
            this.Explorer.ResumeLayout(false);
        }

        private void LoadProductListView()
        {
            string LoadViewName = "ProductListView";
            if (this.Explorer.Controls.Count > 0)
            {
                Control SubPanel = this.Explorer.Controls[0];
                if (SubPanel.Name == LoadViewName)
                {
                    return;
                }
            }
            UnLoadExplorerSubPanel();
            ProductListView productView1 = new ProductListView();
            this.Explorer.SuspendLayout();
            productView1.Location = new System.Drawing.Point(0, 0);
            productView1.Name = LoadViewName;
            Explorer.Name = LoadViewName;
            productView1.AutoSize = true;
            productView1.TabIndex = 0;
            productView1.Size = new System.Drawing.Size(this.Explorer.Width, this.Explorer.Height);
            productView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Explorer.Controls.Add(productView1);
            this.Explorer.ResumeLayout(false);
            this.Explorer.Name = productView1.Name;
        }

        private void LoadAliProductView()
        {
            string LoadViewName = "ProductView";
            if (this.Explorer.Controls.Count > 0)
            {
                Control SubPanel = this.Explorer.Controls[0];
                if (SubPanel.Name == LoadViewName)
                {
                    return;
                }
            }
            UnLoadExplorerSubPanel();
            ProductView productView1 = new ProductView();
            this.Explorer.SuspendLayout();
            productView1.Location = new System.Drawing.Point(0, 0);
            productView1.Name = LoadViewName;
            Explorer.Name = LoadViewName;
            productView1.AutoSize = true;
            productView1.TabIndex = 0;
            productView1.Size = new System.Drawing.Size(this.Explorer.Width, this.Explorer.Height);
            productView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Explorer.Controls.Add(productView1);
            this.Explorer.ResumeLayout(false);
        }

        private void LoadPhotoBankViewPanel()
        {
            string LoadViewName = "PhotoBankView";
            if (this.Explorer.Controls.Count > 0)
            {
                Control SubPanel = this.Explorer.Controls[0];
                if (SubPanel.Name == LoadViewName)
                {
                    return;
                }
            }
            UnLoadExplorerSubPanel();
        }

        private void LoadPriceCateView()
        {
            string LoadViewName = "PriceCateListView";
            if (this.Explorer.Controls.Count > 0)
            {
                Control SubPanel = this.Explorer.Controls[0];
                if (SubPanel.Name == LoadViewName)
                {
                    return;
                }
            }
            UnLoadExplorerSubPanel();
            PriceCateListView view = new PriceCateListView();
            this.Explorer.SuspendLayout();
            view.Location = new System.Drawing.Point(0, 0);
            view.Name = LoadViewName;
            Explorer.Name = LoadViewName;
            view.AutoSize = true;
            view.TabIndex = 0;
            view.Size = new System.Drawing.Size(this.Explorer.Width, this.Explorer.Height);
            view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Explorer.Controls.Add(view);
            this.Explorer.ResumeLayout(false);
        }

        private void LoadSupplierListView()
        {
            string LoadViewName = "SupplierListView";
            if (this.Explorer.Controls.Count > 0)
            {
                Control SubPanel = this.Explorer.Controls[0];
                if (SubPanel.Name == LoadViewName)
                {
                    return;
                }
            }
            UnLoadExplorerSubPanel();
            SupplierListView view = new SupplierListView();
            this.Explorer.SuspendLayout();
            view.Location = new System.Drawing.Point(0, 0);
            view.Name = LoadViewName;
            Explorer.Name = LoadViewName;
            view.AutoSize = true;
            view.TabIndex = 0;
            view.Size = new System.Drawing.Size(this.Explorer.Width, this.Explorer.Height);
            view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Explorer.Controls.Add(view);
            this.Explorer.ResumeLayout(false);
        }

        private void LoadMyItemsListView()
        {
            string LoadViewName = "MyItemsListView";
            if (this.Explorer.Controls.Count > 0)
            {
                Control SubPanel = this.Explorer.Controls[0];
                if (SubPanel.Name == LoadViewName)
                {
                    return;
                }
            }
            UnLoadExplorerSubPanel();
            MyItemsListView view = new MyItemsListView();
            this.Explorer.SuspendLayout();
            view.Location = new System.Drawing.Point(0, 0);
            view.Name = LoadViewName;
            Explorer.Name = LoadViewName;
            view.AutoSize = true;
            view.TabIndex = 0;
            view.Size = new System.Drawing.Size(this.Explorer.Width, this.Explorer.Height);
            view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Explorer.Controls.Add(view);
            this.Explorer.ResumeLayout(false);
        }

        #endregion

        #region 重发计划处理
        private void NewPlanMenuItem_Click(object sender, EventArgs e)
        {
            NewPlanForm f = new NewPlanForm();
            f.Text = "新建重发计划";
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        private void ModifyPlanMenuItem_Click(object sender, EventArgs e)
        {
           
            NewPlanForm f = new NewPlanForm();
            f.Text = "编辑重发计划";
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }
        #endregion

        private void DataDicMenuItem_Click(object sender, EventArgs e)
        {
            DicForm f = new DicForm();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        private void NewFinance_Click(object sender, EventArgs e)
        {
            EditFinDetail f = new EditFinDetail();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        private void NewWater_Click(object sender, EventArgs e)
        {
            EditFinWater f = new EditFinWater();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        private void NewOrderBtn_Click(object sender, EventArgs e)
        {
            NewOrderForm f = new NewOrderForm();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        private void DbsetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DbsetForm f = new DbsetForm();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        private void SupplierListButton_Click(object sender, EventArgs e)
        {
            LoadSupplierListView();
        }

        private void PriceCateButton_Click(object sender, EventArgs e)
        {
            LoadPriceCateView();
        }

        private void Top10QueryBtn_Click(object sender, EventArgs e)
        {
            TopFiveQueryForm f = new TopFiveQueryForm();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        private void BatchAddButton_Click(object sender, EventArgs e)
        {
            LoadAliProductView();
        }

        private void ImagesButton_Click(object sender, EventArgs e)
        {
            LoadPhotoBankViewPanel();
        }

        private void NewSupplierButton_Click(object sender, EventArgs e)
        {
            SupplierForm f = new SupplierForm();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        private void NewItemButton_Click(object sender, EventArgs e)
        {
            NewItemForm f = new NewItemForm();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        private void NewPriceCateButton_Click(object sender, EventArgs e)
        {
            PriceCateForm f = new PriceCateForm();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        private void CategoriesButton_Click(object sender, EventArgs e)
        {
            CategoriesForm f = new CategoriesForm();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }


        public void LoadMyItemCategoriesTreeView()
        {
            MyItemManager.OnEditCategoryEvent -= new NewEditItemEvent(MyItemManager_OnEditCategoryEvent);
            MyItemManager.OnEditCategoryEvent += new NewEditItemEvent(MyItemManager_OnEditCategoryEvent);
            TreeView CateTreeView = null;
            if (ItemTreeNaviGroup.Controls.Count > 0)
            {
                CateTreeView = (TreeView)ItemTreeNaviGroup.Controls[0];
            }
            else
            {
                CateTreeView = new System.Windows.Forms.TreeView();
                CateTreeView.LineColor = System.Drawing.Color.Empty;
                CateTreeView.Location = new System.Drawing.Point(0, 23);
                CateTreeView.Name = "CateTreeView";
                CateTreeView.Dock = DockStyle.Fill;
                CateTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
                CateTreeView.ItemHeight = 18;
                //CateTreeView.Font = new System.Drawing.Font("Microsoft YaHei", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                CateTreeView.Size = new System.Drawing.Size(130, 423);
                CateTreeView.TabIndex = 4;
                CateTreeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(cateTreeView_NodeMouseClick);
                ItemTreeNaviGroup.Controls.Add(CateTreeView);
            }
            CateTreeView.Nodes.Clear();
            List<Categories> cates = myItemsManager.GetAllCategories();
            foreach (Categories p in cates)
            {
                if (p.Level == 1)
                {
                    TreeNode t1 = new TreeNode(p.Name);
                    t1.Tag = p;
                    CateTreeView.Nodes.Add(t1);
                    foreach (Categories c in cates)
                    {
                        if (c.ParentId == p.Id && c.Level == p.Level + 1)
                        {
                            TreeNode t2 = new TreeNode(c.Name);
                            t2.Tag = c;
                            t1.Nodes.Add(t2);

                            foreach (Categories f in cates)
                            {
                                if (f.ParentId == c.Id && f.Level == c.Level + 1)
                                {
                                    TreeNode t3 = new TreeNode(f.Name);
                                    t3.Tag = f;
                                    t2.Nodes.Add(t3);
                                }
                            }

                        }
                    }
                }
            }
            CateTreeView.ExpandAll();
        }

        void MyItemManager_OnEditCategoryEvent(object sender, ItemEventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                LoadMyItemCategoriesTreeView();
            }));
        }

        void cateTreeView_NodeMouseClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LoadMyItemsListView();
                TreeNode currentNode = e.Node;
                MyItemsListView view = (MyItemsListView)Explorer.Controls[0];
            }
        }
    }
}
