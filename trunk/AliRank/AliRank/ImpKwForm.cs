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
        public ImpKwForm()
        {
            InitializeComponent();
        }

        private void ImpKwForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
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
            List<ShowcaseRankInfo> keywordList = searcher.Seacher(url);
            KeywordDAO keywordDAO = DAOFactory.Instance.GetKeywordDAO();
            keywordDAO.Insert(keywordList);
            searcher.Dispose();
            searcher = null;
            string IniFile = FileUtils.CreateAppDataFolderEmptyTextFile(Constants.INI_FILE);
            FileUtils.IniWriteValue(Constants.CLICK_SECTIONS, Constants.COMPANY_URL, url, IniFile);
            this.pictureBox1.Visible = false;
            this.ImportBtn.Enabled = true;
            this.Close();
        }
    }
}