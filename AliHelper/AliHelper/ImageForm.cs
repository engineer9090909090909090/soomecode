using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soomes;
using System.IO;

namespace AliHelper
{
    public partial class ImageForm : Form
    {
        private ProductsManager productsManager;
        private int CurrentGroupId;
        private string SearchName;
        public ImageForm()
        {
            InitializeComponent();
            productsManager = new ProductsManager();
        }

        private void ImageForm_Load(object sender, EventArgs e)
        {
            List<ImageGroupNode> imageGroupNodeList = HttpClient.GetImageGroup(null);
            UpdateImageGroupUI(imageGroupNodeList);
            CurrentGroupId = -1;
            BindDataWithPage(1);
        }

        public void UpdateListView(List<ImageInfo> ImageInfoList)
        {
            this.ImageListView.Clear();
            this.ImageListView.GridLines = true;
            this.ImageListView.View = View.LargeIcon;
            this.ImageListView.Scrollable = true;
            this.ImageListView.CheckBoxes = true;
            if (ImageInfoList == null)
            {
                return;
            }
            ImageList imageList1 = new ImageList();
            imageList1.ImageSize = new System.Drawing.Size(100, 100);
            this.ImageListView.BeginUpdate();
            for (int i = 0; i < ImageInfoList.Count; i++)
            {
                ImageInfo obj = ImageInfoList[i];
                Image img = global::AliHelper.Properties.Resources.no_image;
                if (!string.IsNullOrEmpty(obj.LocationUrl))
                {
                    img = FileUtils.GetImage(obj.LocationUrl);
                }
                imageList1.Images.Add(img);
                ListViewItem item = new ListViewItem();
                item.ImageIndex = i;
                item.Text = obj.DisplayNameUtf8;
                item.Tag = obj.Id;
                item.Checked = false;
                this.ImageListView.Items.Add(item);
            }
            this.ImageListView.LargeImageList = imageList1;
            this.ImageListView.EndUpdate();
        }

        


        public void UpdateImageGroupUI(List<ImageGroupNode> groups)
        {
            ImageGroupTree.Nodes.Clear();
            TreeNode t = new TreeNode("所有分组");//作为根节点
            ImageGroupTree.Nodes.Add(t);
            foreach (ImageGroupNode p in groups)
            {
                TreeNode t1 = new TreeNode(p.Node.Text);
                t1.Tag = p;
                t.Nodes.Add(t1);
            }
            ImageGroupTree.ExpandAll();
        }

        private void ImageGroupTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode currentNode = e.Node;
            ImageGroupNode node =(ImageGroupNode)currentNode.Tag;
            List<ImageGroupNode> imageGroupNodeList = HttpClient.GetImageGroup(node.Branch);
            if (imageGroupNodeList == null)
            {
                return;
            }
            currentNode.Nodes.Clear();
            foreach (ImageGroupNode p in imageGroupNodeList)
            {
                TreeNode t1 = new TreeNode(p.Node.Text);
                t1.Tag = p;
                currentNode.Nodes.Add(t1);
            }
            currentNode.Expand();
        }

        private void ImageGroupTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode currentNode = e.Node;
            if (currentNode.Tag == null)
            {
                CurrentGroupId = -1;
            }
            else 
            {
                ImageGroupNode node = (ImageGroupNode)currentNode.Tag;
                CurrentGroupId = node.Node.Value;
            }
            BindDataWithPage(1);
        }

        
        private void BindDataWithPage(int Page)
        {
            QueryObject<ImageInfo> query = new QueryObject<ImageInfo>();
            query.Page = Page;
            query.PageSize = pager1.PageSize;
            query.Condition = new ImageInfo();
            query.Condition.GroupId = CurrentGroupId;
            query.Condition.DisplayNameUtf8 = SearchName;
            QueryObject<ImageInfo> result = productsManager.GetImageInfoList(query);
            if (result.Result != null)
            {
                pager1.PageIndex = result.Page;
                pager1.PageSize = result.PageSize;
                pager1.RecordCount = result.RecordCount;
                UpdateListView(result.Result);
            }
            else
            {
                pager1.PageIndex = Page;
                pager1.PageSize = 20;
                pager1.RecordCount = 0;
            }
        }

        private void pager1_PageIndexChanged(object sender, EventArgs e)
        {
            BindDataWithPage(this.pager1.PageIndex);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            this.SearchName = this.txtSearchKey.Text.Trim();
            BindDataWithPage(1);
        }
    }
}
