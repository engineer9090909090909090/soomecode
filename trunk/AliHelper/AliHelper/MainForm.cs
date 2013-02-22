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

namespace AliHelper
{
    public partial class MainForm : Form
    {
        private ProductsManager productsManager;
        private ImpProductDetail impProductDetail;
        private string ExplorerCurrentSubName;
        ProductView productView1;
 
        #region 构造方法
        public MainForm()
        {
            productsManager = new ProductsManager();
            impProductDetail = new ImpProductDetail();
            InitializeComponent();
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
            LoadProdutPanel();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //HttpClient.GetMailList();
            CheckForIllegalCrossThreadCalls = false;
            HideAllToolsStrip();
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
                this.Hide();
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
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
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
            this.treeView1.Size = new System.Drawing.Size(130, 423);
            this.treeView1.TabIndex = 4;
            this.treeView1.ContextMenuStrip = TreeNodeContextMenuStrip;
            this.treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            ProductBand.ClientArea.Controls.Add(treeView1);

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
            listViewItem2.Name = Constants.OrderTrackView;
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
                AliGroup group = (AliGroup)currentNode.Tag;
                if (group != null)
                {
                    this.productView1.LoadDataGridView(group.Id);
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
        void UpdateGroupItem_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                AliGroup group = (AliGroup)this.treeView1.ContextMenuStrip.Tag;
                if (group != null)
                {
                    List<AliGroup> groups = new List<AliGroup>();
                    groups.Add(group);
                    GetGroupProduct(groups, DataCache.Instance.CsrfToken);
                }
            }));
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
            if (bandName == ExplorerCurrentSubName)
            {
                return;
            }
            UnLoadExplorerSubPanel();
            HideAllToolsStrip();
            if (bandName == "ProductBand")
            {
                ShowToolsStrip("ProductsStrip");
                LoadProdutPanel();
            }
            else if (bandName == "UpdateBand")
            {
            }
            else if (bandName == "ProductBand")
            {
            }
            else if (bandName == "InquiryBand")
            {
                LoadMailViewPanel();
            }
            else if (bandName == "ClientBand")
            {
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
            ExplorerCurrentSubName = bandName;
            GC.Collect();
        }

        #endregion

        #region 更新产品目录
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
        #endregion

        #region 更新产品
        private void updateAllProduct_Click(object sender, EventArgs e)
        {
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
            List<AliGroup> groups = HttpClient.GetGroups(-1, 0, DataCache.Instance.CsrfToken);
            productsManager.UpdateGroups(groups);
            GetGroupProduct(groups, DataCache.Instance.CsrfToken);
            this.BeginInvoke(new Action(() =>
            {
                UpdateGroupUI(groups);
            }));
            
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
            TreeNode t = new TreeNode("产品分组");//作为根节点
            treeView1.Nodes.Add(t);
            foreach (AliGroup p in groups)
            {
                if (p.Level == 1)
                {
                    TreeNode t1 = new TreeNode(p.Name);
                    t1.Tag = p;
                    t.Nodes.Add(t1);
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
            treeView1.ExpandAll();
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
                if (group.Level == 1)
                {
                    List<AliProduct> products = HttpClient.GetProducts(groupDic, group.Id, group.Level, csrfToken);
                    productsManager.UpdateGroupProdcuts(group.Id, products);
                    foreach (AliProduct item in products)
                    {
                        FileUtils.DownloadProductImage(webClient, item.AbsImageUrl, item.Id);
                        if (productsManager.IsNeedUpdateDetail(item.Id))
                        {
                            ProductDetail detail = impProductDetail.GetEditFormElements(item);
                            productsManager.InsertOrUpdateProdcutDetail(detail);
                            productList.Add(item);
                        }
                    }
                }
            }
            webClient.Dispose();
            webClient = null;
        }
        
        public void GetGroupProduct(List<AliGroup> groups, AliGroup currGroup, string csrfToken)
        {
            Hashtable groupDic = new Hashtable();
            foreach (AliGroup group in groups)
            {
                groupDic.Add(group.Name, group.Id);
            }
            List<AliProduct> products = HttpClient.GetProducts(groupDic, currGroup.Id, currGroup.Level, csrfToken);
            productsManager.UpdateGroupProdcuts(currGroup.Id, products);
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
            this.productView1 = null;
            GC.Collect();
        }
        private void HideAllToolsStrip()
        {
            if (this.toolStripContainer.TopToolStripPanel.Controls.Count > 0)
            {
                foreach (Control SubPanel in toolStripContainer.TopToolStripPanel.Controls)
                {
                    SubPanel.Hide();
                }
            }
        }

        private void ShowToolsStrip(string name)
        {
            if (this.toolStripContainer.TopToolStripPanel.Controls.Count > 0)
            {
                foreach (Control SubPanel in toolStripContainer.TopToolStripPanel.Controls)
                {
                    if (SubPanel.Name == name)
                    {
                        SubPanel.Show();
                    }
                }
            }
        }


        private void LoadFinViewPanel()
        {
            FinView FinBaseView = new AliHelper.FinView();
            this.Explorer.SuspendLayout();
            FinBaseView.Location = new System.Drawing.Point(0, 0);
            FinBaseView.Name = "FinBaseView";
            FinBaseView.AutoSize = true;
            FinBaseView.TabIndex = 1;
            FinBaseView.Size = new System.Drawing.Size(this.Explorer.Width, this.Explorer.Height);
            FinBaseView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Explorer.Controls.Add(FinBaseView);
            this.Explorer.ResumeLayout(false);
        }

        private void LoadOrderFanViewPanel(bool IsFinOrderView)
        {
            OrderView FinorderView = new AliHelper.OrderView();
            this.Explorer.SuspendLayout();
            FinorderView.IsFinOrderView = IsFinOrderView;
            FinorderView.Location = new System.Drawing.Point(0, 0);
            FinorderView.Name = "FinorderView";
            FinorderView.AutoSize = true;
            FinorderView.TabIndex = 1;
            FinorderView.Size = new System.Drawing.Size(this.Explorer.Width, this.Explorer.Height);
            FinorderView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Explorer.Controls.Add(FinorderView);
            this.Explorer.ResumeLayout(false);
        }

        private void LoadFinWaterViewPanel()
        {
            WaterView FinanceWaterView = new AliHelper.WaterView();
            this.Explorer.SuspendLayout();
            FinanceWaterView.Location = new System.Drawing.Point(0, 0);
            FinanceWaterView.Name = "FinanceWaterView";
            FinanceWaterView.AutoSize = true;
            FinanceWaterView.TabIndex = 1;
            FinanceWaterView.Size = new System.Drawing.Size(this.Explorer.Width, this.Explorer.Height);
            FinanceWaterView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Explorer.Controls.Add(FinanceWaterView);
            this.Explorer.ResumeLayout(false);
        }


        private void LoadMailViewPanel()
        {
            MailView view = new AliHelper.MailView();
            this.Explorer.SuspendLayout();
            view.Location = new System.Drawing.Point(0, 0);
            view.Name = "mailView";
            view.AutoSize = true;
            view.TabIndex = 1;
            view.Size = new System.Drawing.Size(this.Explorer.Width, this.Explorer.Height);
            view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Explorer.Controls.Add(view);
            this.Explorer.ResumeLayout(false);
        }

        private void LoadProdutPanel()
        {
            if (this.productView1 != null)
            {
                return;
            }
            this.productView1 = new AliHelper.ProductView();
            this.Explorer.SuspendLayout();
            this.productView1.Location = new System.Drawing.Point(0, 0);
            this.productView1.Name = "productView1";
            this.productView1.AutoSize = true;
            this.productView1.TabIndex = 0;
            this.productView1.Size = new System.Drawing.Size(this.Explorer.Width, this.Explorer.Height);
            this.productView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Explorer.Controls.Add(productView1);
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


    }
}
