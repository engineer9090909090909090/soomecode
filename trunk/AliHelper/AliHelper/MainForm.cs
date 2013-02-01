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
        //alibaba vip manage url
        public static string ManageHtml = "http://hz.productposting.alibaba.com/product/manage_products.htm#tab=approved";
        private ProductsManager productsManager;
        private ImpProductDetail impProductDetail;
        ProductView productView1;
        #region 构造方法
        public MainForm()
        {
            productsManager = new ProductsManager();
            impProductDetail = new ImpProductDetail();
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            HttpClient.GetMailList();
            CheckForIllegalCrossThreadCalls = false;
            impProductDetail.InitDataCacheFormOptions();
            productsManager.InitGroupOptions();
            List<AliGroup> groups = productsManager.GetGroupList();
            LoadNavigatorBar();
            UpdateGroupUI(groups);
            LoadProdutPanel();
        }
        #endregion

        #region TreeView NodeMouse Event
        void treeView1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

        }

        void treeView1_NodeMouseDoubleClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
        }

        void treeView1_NodeMouseClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            TreeNode currentNode = e.Node;
            int GroupId = Convert.ToInt32(currentNode.Tag);
            this.productView1.LoadDataGridView(GroupId);
        }
        #endregion

        #region NavigatorBar 处理

        protected void LoadNavigatorBar()
        {
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.treeView1.LineColor = System.Drawing.Color.Empty;
            this.treeView1.Location = new System.Drawing.Point(0, 23);
            this.treeView1.Name = "treeView1";
            this.treeView1.Dock = DockStyle.Fill;
            this.treeView1.Size = new System.Drawing.Size(130, 423);
            this.treeView1.TabIndex = 4;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            this.treeView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView1_KeyDown);
            ProductBand.ClientArea.Controls.Add(treeView1);

            ListView listView1 = new ListView();
            listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            ColumnHeader nameColumnHeader = new System.Windows.Forms.ColumnHeader();
            nameColumnHeader.Text = "名称";
            nameColumnHeader.Width = 80;
            ColumnHeader timeColumnHeader = new System.Windows.Forms.ColumnHeader();
            timeColumnHeader.Text = "重发时间";
            timeColumnHeader.Width = 130;
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
            ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("收件箱");
            listViewItem1.ImageIndex = 1;
            ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("已发送");
            listViewItem2.ImageIndex = 3;
            ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("垃圾箱");
            listViewItem3.ImageIndex = 5;
            ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("已删除");
            listViewItem4.ImageIndex = 6;
            ListView MailListView = new System.Windows.Forms.ListView();
            MailListView.Name = "MailListView";
            MailListView.BorderStyle = BorderStyle.None;
            MailListView.Location = new System.Drawing.Point(0, 0);
            MailListView.Size = new System.Drawing.Size(130, 500);
            InquiryBand.ClientArea.Margin = new Padding(30);
            MailListView.Dock = DockStyle.Fill;

            /*
            int height = InquiryBand.ClientArea.Height;
            int width = InquiryBand.ClientArea.Width;
            MailListView.Location = new System.Drawing.Point(width/4, 20);
            MailListView.Size = new System.Drawing.Size(width / 2, height - 40);
             */
            MailListView.LargeImageList = outlookLargeIcons;
            MailListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,listViewItem2,listViewItem3,listViewItem4});
            MailListView.UseCompatibleStateImageBehavior = false;
            MailListView.View = System.Windows.Forms.View.LargeIcon;
            InquiryBand.ClientArea.SizeChanged += new EventHandler(InquiryBand_ClientArea_SizeChanged);
            InquiryBand.ClientArea.Controls.Add(MailListView);
        }

        void InquiryBand_ClientArea_SizeChanged(object sender, EventArgs e)
        {
            /*
            int height = InquiryBand.ClientArea.Height;
            int width = InquiryBand.ClientArea.Width;
            if (height < 300) height = 300;
            ListView MailListView = (ListView)InquiryBand.ClientArea.Controls["MailListView"];
            MailListView.Location = new System.Drawing.Point(width <100 ? 0 :width / 4, 20);
            MailListView.Size = new System.Drawing.Size(width <100 ? width: width / 2, height - 40);
             */
        }

        private void NavigatorBar_ActiveBandChanged(object sender, EventArgs e)
        {
            string bandName = NavigatorBar.ActiveBand.Name;
            if (bandName == "ProductBand")
            {
                LoadProdutPanel();
            }
            else if (bandName == "UpdateBand")
            {
                UnLoadProdcutPanel();
            }
            else if (bandName == "ProductBand")
            {
                UnLoadProdcutPanel();
            }
            else if (bandName == "InquiryBand")
            {
                UnLoadProdcutPanel();
            }
            else if (bandName == "ClientBand")
            {
                UnLoadProdcutPanel();
            }
            else if (bandName == "OrderBand")
            {
                UnLoadProdcutPanel();
            }
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

        public void UpdateGroupUI(List<AliGroup> groups)
        {
            treeView1.Nodes.Clear();
            TreeNode t = new TreeNode("产品分组");//作为根节点
            t.Tag = -1;
            treeView1.Nodes.Add(t);
            foreach (AliGroup p in groups)
            {
                if (p.Level == 1)
                {
                    TreeNode t1 = new TreeNode(p.Name);
                    t1.Tag = p.Id;
                    t.Nodes.Add(t1);
                    if (p.HasChildren)
                    {
                        foreach (AliGroup c in groups)
                        {
                            if (c.ParentId == p.Id && c.Level == p.Level + 1)
                            {
                                TreeNode t2 = new TreeNode(c.Name);
                                t2.Tag = c.Id;
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
            foreach (AliGroup group in groups)
            {
                if (group.Level == 1)
                {
                    List<AliProduct> products = HttpClient.GetProducts(groupDic, group.Id, group.Level, csrfToken);
                    productsManager.UpdateGroupProdcuts(group.Id, products);
                    foreach (AliProduct item in products)
                    {
                        if (productsManager.IsNeedUpdateDetail(item.Id))
                        {
                            ProductDetail detail = impProductDetail.GetEditFormElements(item);
                            productsManager.InsertOrUpdateProdcutDetail(detail);
                        }
                    }
                }
            }
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
            //HttpClient.GetCountries();
            EditCategory f = new EditCategory();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        private void UnLoadProdcutPanel()
        {
            if (this.Explorer.Controls.Count > 0)
            {
                this.Explorer.Controls.RemoveAt(0);
                this.productView1.Dispose();
            }
            this.productView1 = null;
            GC.Collect();
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

        private void NewPlanMenuItem_Click(object sender, EventArgs e)
        {
            /*
            NewPlanForm f = new NewPlanForm();
            f.Text = "新建重发计划";
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
            */
        }

        private void ModifyPlanMenuItem_Click(object sender, EventArgs e)
        {
            /*
            NewPlanForm f = new NewPlanForm();
            f.Text = "编辑重发计划";
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
            */
        }

    }
}
