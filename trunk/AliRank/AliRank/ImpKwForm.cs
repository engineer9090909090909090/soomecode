using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AliRank
{
    public partial class ImpKwForm : Form
    {

        List<ShowcaseRankInfo> keywordList;
        public ImpKwForm()
        {
            InitializeComponent();
        }

        private void ImpKwForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            UpdateListView();
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
            keywordList = searcher.Seacher(url);
            searcher.Dispose();
            searcher = null;
            this.pictureBox1.Visible = false;
            this.ImportBtn.Enabled = true;
            
        }

        public void UpdateListView()
        {
            this.listView1.GridLines = true; //显示表格线
            this.listView1.View = View.Details;//显示表格细节
            this.listView1.LabelEdit = false; //是否可编辑,ListView只可编辑第一列。
            this.listView1.Scrollable = true;//有滚动条
            this.listView1.FullRowSelect = false;//是否可以选择行

            //添加表头
            this.listView1.Columns.Add("", 0);
            this.listView1.Columns.Add("列1", 80);
            this.listView1.Columns.Add("列2", 160);
            this.listView1.Columns.Add("列3", 160);
            this.listView1.Columns.Add("列4", 160);
            //添加各项
            ListViewItem[] p = new ListViewItem[2];
            p[0] = new ListViewItem(new string[] { "", "aaaa", "bbbb" });
            p[1] = new ListViewItem(new string[] { "", "cccc", "ggggg" });
            p[1] = new ListViewItem(new string[] { "", "eeee", "ffff" });
            //p[0].SubItems[0].BackColor = Color.Red; //用于设置某行的背景颜色

            this.listView1.Items.AddRange(p);
            //也可以用this.listView1.Items.Add();不过需要在使用的前后添加Begin... 和End...防止界面自动刷新
            // 添加分组
            /*
            this.listView1.Groups.Add(new ListViewGroup("tou"));
            this.listView1.Groups.Add(new ListViewGroup("wei"));

            this.listView1.Items[0].Group = this.listView1.Groups[0];
            this.listView1.Items[1].Group = this.listView1.Groups[1];
            */
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            KeywordDAO keywordDAO = DAOFactory.Instance.GetKeywordDAO();
            keywordDAO.Insert(keywordList);
            this.Close();
        }
    }
}