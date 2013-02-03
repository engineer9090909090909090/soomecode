using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AliRank
{
    public partial class ImpKwForm : Form
    {

        List<ShowcaseRankInfo> ProductsList;
        KeywordDAO keywordDAO;
        Dictionary<int, ShowcaseRankInfo> ProductsDic = new Dictionary<int, ShowcaseRankInfo>();
        public ImpKwForm()
        {
            InitializeComponent();
        }

        private void ImpKwForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            keywordDAO = DAOFactory.Instance.GetKeywordDAO();
            UpdateListView(null);
        }
        
        private void ImportBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {               
                this.errorMsg.Visible = true;
                this.errorMsg.Text = "公司出口通网站地址不能为空。";
                return;
            }
            this.pictureBox1.Visible = true;
            this.ImportBtn.Enabled = true;
            ProductsDic.Clear();
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerAsync();
            bgWorker.Dispose();
        }

        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.ImportBtn.Enabled = false;
            this.pictureBox1.Visible = true;
            string url = this.textBox1.Text;
            ShowcaseQueryer searcher = new ShowcaseQueryer();
            url = url.Replace(".en.alibaba.com/", ".en.alibaba.com");
            ProductsList = searcher.Seacher(url);
            UpdateListView(ProductsList);
            searcher.Dispose();
            searcher = null;
            this.pictureBox1.Visible = false;
            this.ImportBtn.Enabled = true;
            
        }

        public void UpdateListView(List<ShowcaseRankInfo> productList)
        {
            this.listView1.Clear();
            this.listView1.GridLines = true; 
            this.listView1.View = View.LargeIcon;
            this.listView1.Scrollable = true;
            this.listView1.CheckBoxes = true;
            ProductsDic.Clear();
            if (productList == null)
            {
                return;
            }
            ImageList imageList1 = new ImageList();
            imageList1.ImageSize = new System.Drawing.Size(100 , 100);
            this.listView1.BeginUpdate();
            for(int i = 0; i < productList.Count; i++)
            {
                ShowcaseRankInfo obj = productList[i];
                ProductsDic.Add(obj.ProductId, obj);
                Image img = global::AliRank.Properties.Resources.no_image; 
                if (!string.IsNullOrEmpty(obj.ProductImg) && File.Exists(obj.ProductImg))
                {
                    img = Image.FromFile(obj.ProductImg, true);
                }
                imageList1.Images.Add(img);
                ListViewItem item = new ListViewItem();
                item.ImageIndex = i;
                item.Text = obj.ProductName;
                item.Tag = obj.ProductId;
                this.listView1.Items.Add(item);
            }
            this.listView1.LargeImageList = imageList1;
            this.listView1.EndUpdate();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            List<ShowcaseRankInfo> selectedList = new List<ShowcaseRankInfo>();
            foreach (ListViewItem item in this.listView1.CheckedItems)
            {
                int productId = Convert.ToInt32(item.Tag);
                ShowcaseRankInfo selectedObj = ProductsDic[productId];
                selectedList.Add(selectedObj);
            }
            if (selectedList.Count > 0)
            {
                keywordDAO.Insert(selectedList);
                selectedList.Clear();
                this.Close();
            }
        }

        private void SelectChk_CheckedChanged(object sender, EventArgs e)
        {
            if (SelectChk.Checked)
            {
                SelectChk.Text = "反选";
            }
            else {
                SelectChk.Text = "全选";
            }
            foreach (ListViewItem item in listView1.Items)
            {
                item.Checked = SelectChk.Checked;
            }
        }
    }
}