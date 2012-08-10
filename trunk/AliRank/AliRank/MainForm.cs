using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Resources;

namespace AliRank
{
    public partial class MainForm : Form
    {
        private KeywordDAO keywordDAO;
        private VpnDAO vpnDao;
        private static bool IsStopClicking;

        private string IniFile;
        public MainForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(MainForm_Load);
            IniFile = FileUtils.CreateAppDataFolderEmptyTextFile(Constants.INI_FILE);
            FileUtils.IniWriteValue(Constants.CLICK_SECTIONS, Constants.AUTO_SHUTDOWN, Constants.NO, IniFile);
            string clickNum = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.AUTO_CLICK_NUM, IniFile);
            if (string.IsNullOrEmpty(clickNum))
            {
                FileUtils.IniWriteValue(Constants.CLICK_SECTIONS, Constants.AUTO_CLICK_NUM, 50 + "", IniFile);
            }
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            vpnDao = DAOFactory.Instance.GetVpnDAO();
            LoadDataview();
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImpKwForm f = new ImpKwForm();
            f.FormClosed += new FormClosedEventHandler(f_FormClosed);
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }
        private void ClickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetupForm f = new SetupForm();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        #region 关机菜单
        private void shutdownStripMenuItem_Click(object sender, EventArgs e)
        {
            if (shutdownToolStripMenuItem.Checked)
            {
                shutdownToolStripMenuItem.Checked = false;
                FileUtils.IniWriteValue(Constants.CLICK_SECTIONS, Constants.AUTO_SHUTDOWN, Constants.NO, IniFile);
            }
            else {
                shutdownToolStripMenuItem.Checked = true;
                FileUtils.IniWriteValue(Constants.CLICK_SECTIONS, Constants.AUTO_SHUTDOWN, Constants.YES, IniFile);
            }
        }
        #endregion

        #region DataGridView 初始化处理
        void LoadDataview()
        {
            keywordDAO = DAOFactory.Instance.GetKeywordDAO();
            this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            DataTable dt = new DataTable();
            dt.Columns.Add("Image", typeof(Image));
            dt.Columns.Add("productName", typeof(string));
            dt.Columns.Add("productId", typeof(string));
            dt.Columns.Add("mainKey", typeof(string));
            dt.Columns.Add("rank", typeof(string));
            dt.Columns.Add("clicked", typeof(string));
            dt.Columns.Add("productUrl", typeof(string));
            dt.Columns.Add("updateTime", typeof(DateTime));
            this.dataGridView1.DataSource = dt;

            DataGridViewColumn column0 = this.dataGridView1.Columns[0];
            column0.HeaderText = "Product Image";
            column0.Width = 120;
            DataGridViewColumn column = this.dataGridView1.Columns[1];
            column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column.HeaderText = "Product Name";
            column.Width = 150;
            DataGridViewColumn column2 = this.dataGridView1.Columns[2];
            column2.HeaderText = "Product Id";
            column2.Width = 100;
            DataGridViewColumn column3 = this.dataGridView1.Columns[3];
            column3.HeaderText = "Main Keyword";
            column3.Width = 150;
            DataGridViewColumn column4 = this.dataGridView1.Columns[4];
            column4.HeaderText = "Ranking Status";
            column4.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column4.Width = 180;
            DataGridViewColumn column5 = this.dataGridView1.Columns[5];
            column5.HeaderText = "Clicked";
            column5.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column5.Width = 150;
            DataGridViewColumn column6 = this.dataGridView1.Columns[6];
            column6.HeaderText = "Pruduct Url";
            column6.Width = 400;
            DataGridViewColumn column7 = this.dataGridView1.Columns[7];
            column7.HeaderText = "Update Time";
            column7.Width = 120;
            List<Keywords> productList = keywordDAO.GetKeywordList();
            if (productList.Count > 0)
            {
                foreach (Keywords item in productList)
                {
                    DataRow row = dt.NewRow();
                    if (string.IsNullOrEmpty(item.ProductImg))
                    {
                        //ResourceManager rm = new ResourceManager("AliRank.Properties.Resources", typeof(MainForm).Assembly);
                        // rm.GetObject("no_image");
                        row["Image"] = global::AliRank.Properties.Resources.no_image; 
                    }
                    else {
                        row["Image"] = Image.FromFile(item.ProductImg);
                    }
                    row["productName"] = item.ProductName;
                    row["productId"] = item.ProductId;
                    row["mainKey"] = item.MainKey;
                    row["rank"] = Keywords.GetRankInfo(item);
                    row["clicked"] = Convert.ToString(item.Clicked);
                    row["productUrl"] = item.CompanyUrl + item.ProductUrl;
                    row["updateTime"] = item.UpdateTime;
                    dt.Rows.Add(row);                
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                DataGridViewCell cell = dataGridView1[e.ColumnIndex, e.RowIndex];
                System.Diagnostics.Process.Start("iexplore.exe", Convert.ToString(cell.Value));
            }
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                Clipboard.SetText(this.dataGridView1.GetClipboardContent().GetData(DataFormats.Text).ToString());
            }
        }

        void f_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadDataview();
        }

        #endregion

        #region 排名查询事件处理

        private void AsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AsToolStripMenuItem.Enabled = false;
            toolStripButton4.Enabled = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCell cell = row.Cells[4];
                cell.Value = "Waiting...";
            }
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerAsync();
            bgWorker.Dispose();
        }

        private static int iCount = 0;
        private static int MaxCount = 10;
        ManualResetEvent eventX;
        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<Keywords> productList = keywordDAO.GetKeywordList();
            MaxCount = productList.Count;
            if (MaxCount > 0)
            {
                eventX = new ManualResetEvent(false);
                ThreadPool.SetMinThreads(4, 40);
                ThreadPool.SetMaxThreads(10, 200);
                for (int i = 0; i < MaxCount; i++)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(DoRankSearch), (object)productList[i]);
                }
                eventX.WaitOne(Timeout.Infinite, true);
            }
            toolStripButton4.Enabled = true;
            AsToolStripMenuItem.Enabled = true;
            Console.WriteLine("线程池结束！");
        }
        
        private void DoRankSearch(object obj)
        {
            Keywords item = (Keywords)obj;
            RankQueryer queryer = new RankQueryer();
            queryer.OnRankSearchingEvent += new RankSearchingEvent(queryer_OnRankSearchingEvent);
            queryer.OnRankSearchEndEvent += new RankSearchEndEvent(queryer_OnRankSearchEndEvent);
            queryer.Seacher(item);
            queryer.OnRankSearchingEvent -= new RankSearchingEvent(queryer_OnRankSearchingEvent);
            queryer.OnRankSearchEndEvent -= new RankSearchEndEvent(queryer_OnRankSearchEndEvent);
            Interlocked.Increment(ref iCount);
            if (iCount == MaxCount)
            {
                eventX.Set();
            }
        }

        void queryer_OnRankSearchingEvent(object sender, RankEventArgs e)
        {
            Keywords item = e.Item;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCell productIdCell = row.Cells[2];
                string id = (string)productIdCell.Value;
                if (id == item.ProductId)
                {
                    DataGridViewCell cell = row.Cells[4];
                    cell.Value = e.Msg;
                }
            }
        }

        void queryer_OnRankSearchEndEvent(object sender, RankEventArgs e)
        {
            Keywords item = e.Item;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCell productIdCell = row.Cells[2];
                string id = (string)productIdCell.Value;
                if (id == item.ProductId)
                {
                    DataGridViewCell cell = row.Cells[4];
                    keywordDAO.UpdateRank(item);
                    cell.Value = Keywords.GetRankInfo(item);
                }
            }
        }

        #endregion

        #region 点击处理事件

        BackgroundWorker bgClickWorker;
        private void clickRunBtn_Click(object sender, EventArgs e)
        {
            string network = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.NETWORK_CHOICE, IniFile);
            if (network.Equals(Constants.NETWORK_VPN))
            {
                List<VpnModel> vpnList = vpnDao.GetVpnModelList();
                if (vpnList == null || vpnList.Count == 0)
                {
                    MessageBox.Show("您选择了VPN网络点击，但您没有设置任何VPN数据信息.\r\n请到[VPN 管理]项设置.");
                    return;
                }
            }
            clickRunBtn.Enabled = false;
            clickStopBtn.Enabled = true;
            IsStopClicking = false;
            tabControl.SelectedIndex = 1;
            if (bgClickWorker != null)
            {
                bgClickWorker.Dispose();
                bgClickWorker = null;
            }
            bgClickWorker = new BackgroundWorker();
            bgClickWorker.WorkerSupportsCancellation = true;
            bgClickWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork2);
            bgClickWorker.RunWorkerAsync();

            if (clickTimer != null)
            {
                clickTimer.Dispose();
                clickTimer = null;
            }
            clickTimer = new System.Timers.Timer(1000);
            beginTime = DateTime.Now;
            clickTimer.Elapsed += new System.Timers.ElapsedEventHandler(theout);
            clickTimer.AutoReset = true;
            clickTimer.Enabled = true; 
        }

        System.Timers.Timer clickTimer;
        DateTime beginTime;
        public void theout(object source, System.Timers.ElapsedEventArgs e) 
        {
            TimeSpan ts = DateTime.Now - beginTime;
            this.RunTime.Text = ts.Hours.ToString("00") + " : " + ts.Minutes.ToString("00") + " : " + ts.Seconds.ToString("00"); 
        } 

        private void clickStopBtn_Click(object sender, EventArgs e)
        {
            IsStopClicking = true;
            clickTimer.Enabled = false; 
            bgClickWorker.CancelAsync();
            clickStopBtn.Enabled = false;
        }

        void bgWorker_DoWork2(object sender, DoWorkEventArgs e)
        {
            List<Keywords> productList = keywordDAO.GetKeywordList();
            string ConfigClickNum = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.AUTO_CLICK_NUM, IniFile);
            string Network = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.NETWORK_CHOICE, IniFile);
            List<VpnModel> VpnModelList = null;
            string vpnName = "VPN123";
            VPN.DisConnect(vpnName);
            if (Network.Equals(Constants.NETWORK_VPN))
            {
                VpnModelList = vpnDao.GetVpnModelList();
            }
            int clickNum = Convert.ToInt32(ConfigClickNum);
            for (int n = 0; n < clickNum; n++)
            {
                if (Network.Equals(Constants.NETWORK_VPN))
                {
                    VpnModel model = VpnModelList[n];
                    VPN.Create(vpnName, model);
                    VPN.Connect(vpnName, model);
                }
                IEHandleUtils.ClearIECookie();
                for (int i = 0; i < productList.Count; i++)
                {
                    ProductClicker clicker = new ProductClicker(webBrowser);
                    clicker.OnRankClickingEvent += new RankClickingEvent(clicker_OnRankClickingEvent);
                    clicker.OnRankClickEndEvent += new RankClickEndEvent(clicker_OnRankClickEndEvent);
                    clicker.DoClick(productList[i]);
                    clicker.OnRankClickingEvent -= new RankClickingEvent(clicker_OnRankClickingEvent);
                    clicker.OnRankClickEndEvent -= new RankClickEndEvent(clicker_OnRankClickEndEvent);
                    if (IsStopClicking) { break; }
                }
                if (IsStopClicking) { break; }
            }
            clickRunBtn.Enabled = true;
            clickStopBtn.Enabled = false;
            clickTimer.Enabled = false; 
            string sdflag = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.AUTO_SHUTDOWN, IniFile);
            if (IsStopClicking == false && sdflag.Equals(Constants.YES))
            {
                SoomesUtils.Shutdown();
            }
        }


        void clicker_OnRankClickEndEvent(object sender, RankEventArgs e)
        {
            Keywords item = e.Item;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCell productIdCell = row.Cells[2];
                string id = (string)productIdCell.Value;
                if (id == item.ProductId)
                {
                    DataGridViewCell cell = row.Cells[5];
                    cell.Value = item.Clicked;
                    toolStripStatusLabel1.Text = e.Msg;
                    keywordDAO.UpdateClicked(item);
                }
            }
        }

        void clicker_OnRankClickingEvent(object sender, RankEventArgs e)
        {
            Keywords item = e.Item;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCell productIdCell = row.Cells[2];
                string id = (string)productIdCell.Value;
                if (id == item.ProductId)
                {
                    DataGridViewCell cell = row.Cells[5];
                    cell.Value = "Clicking...";
                    toolStripStatusLabel1.Text = e.Msg;
                }
            }
        }
        #endregion 

        private void CleanKeyMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("\r\n        您确认要清空所有数据吗？          \r\n\r\n", "清空关键词", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                string DataBasePath = FileUtils.GetAppDataFolder() + Path.DirectorySeparatorChar + Constants.DB_FILE;
                File.Delete(DataBasePath);
                LoadDataview();
            }
        }

        private void VPNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VpnForm f = new VpnForm();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }


        

    
        

        


        
    }
}
