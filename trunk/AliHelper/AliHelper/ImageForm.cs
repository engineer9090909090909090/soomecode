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
        public ImageForm()
        {
            InitializeComponent();
        }

        private void ImageForm_Load(object sender, EventArgs e)
        {
            List<ImageGroupNode> imageGroupNodeList = HttpClient.GetImageGroup(null);
            UpdateImageGroupUI(imageGroupNodeList);
            if (imageGroupNodeList.Count > 0)
            {
                ImageGroupNode firstNode = imageGroupNodeList[0];
                ImageInfoJson imageInfo = HttpClient.GetImages(firstNode.Node.Value, 1);
                UpdateListView(imageInfo.ImageInfos);
            }
            this.pager1.EventPaging += new AliHelper.Controls.EventPagingHandler(pager1_EventPaging);
            this.pager1.PageCurrent = 1;//当前页为第一页  
            this.pager1.PageSize = 20;//页数  
            this.pager1.Bind();//绑定
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

        int pager1_EventPaging(Controls.EventPagingArg e)
        {
            return BindDgv();
        }

        private int CurrentGroupId;
        private int CurrentPage;
        private int BindDgv()
        {
            //传入要取的第一条和最后一条  
            string start = (pager1.PageSize * (pager1.PageCurrent - 1) + 1).ToString();
            string end = (pager1.PageSize * pager1.PageCurrent).ToString();
            if (CurrentGroupId == 0)
            {
                return 0;
            }
            ImageInfoJson imageInfoJson = HttpClient.GetImages(CurrentGroupId, CurrentPage);
            if (imageInfoJson != null)
            {
                int totalCount = imageInfoJson.Query.TotalItem;
                int pageSize = imageInfoJson.Query.PageSize;
                int currPage = imageInfoJson.Query.CurrentPage;
                int pageCount = imageInfoJson.Query.TotalPage;
                pager1.PageSize = pageSize;
                pager1.PageCurrent = currPage;
                pager1.PageCount = pageCount;
                pager1.NMax = totalCount;
                return totalCount;
            }
            else {
                pager1.PageSize = 20;
                pager1.PageCurrent = 1;
                pager1.PageCount = 1;
                pager1.NMax = 0;
                return 0;
            }
            //数据源  
            //dtPage = achieve.GetAll(Keyword, start, end);
            //绑定分页控件  
            //pager1.bindingSource1.DataSource = dtPage;
            //pager1.bindingNavigator1.BindingSource = pager1.bindingSource1;
            ////讲分页控件绑定DataGridView  
            //dgvClients.DataSource = pager1.bindingSource1;
            //返回总记录数  
            //return achieve.GetToalCount(Keyword);
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
                return;
            }
            ImageGroupNode node = (ImageGroupNode)currentNode.Tag;
            ImageInfoJson imageInfo = HttpClient.GetImages(node.Node.Value, 1);
            UpdateListView(imageInfo.ImageInfos);
        }
    }
}
