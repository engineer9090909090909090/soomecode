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
using UtilityLibrary.WinControls;
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
            LoadOutlookBar();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            HttpClient.GetMailList();
            CheckForIllegalCrossThreadCalls = false;
            impProductDetail.InitDataCacheFormOptions();
            productsManager.InitGroupOptions();
            List<AliGroup> groups = productsManager.GetGroupList();
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

        #region OutlookBar 处理
        void LoadOutlookBar()
        {
            outlookBar1.Bands.Add(new OutlookBarBand("产品维护", treeView1));

            ListViewEx listView1 = new ListViewEx();
            listView1.Dock = DockStyle.Fill;
            outlookBar1.Bands.Add(new OutlookBarBand("重发计划", listView1));
            listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            ColumnHeader nameColumnHeader = new System.Windows.Forms.ColumnHeader();
            nameColumnHeader.Text = "名称";
            nameColumnHeader.Width = 80;
            ColumnHeader timeColumnHeader = new System.Windows.Forms.ColumnHeader();
            timeColumnHeader.Text = "重发时间";
            timeColumnHeader.Width = 130;
            listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { nameColumnHeader, timeColumnHeader });
            listView1.Name = "listView1";
            listView1.TabIndex = 0;
            listView1.CheckBoxes = true;
            listView1.Dock = DockStyle.Fill;
            listView1.ContextMenuStrip = this.CfContextMenuStrip;

            ImageList outlookLargeIcons = new ImageList();
            outlookLargeIcons.ImageSize = new Size(32, 32);
            Bitmap icons = (Bitmap)global::AliHelper.Properties.IconImages.OutlookLargeIcons;
            icons.MakeTransparent(Color.FromArgb(255, 0, 255));
            outlookLargeIcons.Images.AddStrip(icons);
            ImageList outlookSmallIcons = new ImageList();
            outlookSmallIcons.ImageSize = new Size(16, 16);
            Bitmap smallIcons = (Bitmap)global::AliHelper.Properties.IconImages.OutlookSmallIcons;
            icons.MakeTransparent(Color.FromArgb(255, 0, 255));
            outlookSmallIcons.Images.AddStrip(smallIcons);
            OutlookBarBand outlookShortcutsBand = new OutlookBarBand("外贸邮");
            outlookShortcutsBand.LargeImageList = outlookLargeIcons;
            outlookShortcutsBand.SmallImageList = outlookSmallIcons;
            outlookShortcutsBand.Items.Add(new OutlookBarItem("收件箱", 1));
            outlookShortcutsBand.Items.Add(new OutlookBarItem("已发送", 5));
            outlookShortcutsBand.Items.Add(new OutlookBarItem("垃圾邮件", 6));
            outlookBar1.Bands.Add(outlookShortcutsBand);
            outlookBar1.PropertyChanged += new OutlookBarPropertyChangedHandler(outlookBar1_PropertyChanged);
            outlookBar1.ItemClicked += new OutlookBarItemClickedHandler(OnOutlookBarItemClicked);
        
        }

        void outlookBar1_PropertyChanged(OutlookBarBand band, OutlookBarProperty property)
        {
            if (property == OutlookBarProperty.CurrentBandChanged)
            {
                int index = outlookBar1.GetCurrentBand();
                if (index == 0)
                {
                    LoadProdutPanel();
                }
                else if (index == 1)
                {
                    UnLoadProdcutPanel();
                }
                else if (index == 2)
                {
                    UnLoadProdcutPanel();
                }
                GC.Collect();
            }
        }

        void OnOutlookBarItemClicked(OutlookBarBand band, OutlookBarItem item)
        {
            string message = "Item : " + item.Text + " was clicked...";
            MessageBox.Show(message);
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
                        ProductDetail detail = impProductDetail.GetEditFormElements(item.Id);
                        productsManager.InsertOrUpdateProdcutDetail(detail);
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
            if (splitContainer1.Panel2.Controls.Count > 0)
            {
                splitContainer1.Panel2.Controls.RemoveAt(0);
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
            this.splitContainer1.Panel2.SuspendLayout();
            this.productView1.Location = new System.Drawing.Point(0, 0);
            this.productView1.Name = "productView1";
            this.productView1.AutoSize = true;
            this.productView1.TabIndex = 0;
            this.productView1.Size = new System.Drawing.Size(this.splitContainer1.Panel2.Width, this.splitContainer1.Panel2.Height);
            this.productView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Panel2.Controls.Add(productView1);
            this.splitContainer1.Panel2.ResumeLayout(false);
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
