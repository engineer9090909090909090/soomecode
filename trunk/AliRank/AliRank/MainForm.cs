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
            string sAutoClickNum = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.AUTO_CLICK_NUM, IniFile);
            if (string.IsNullOrEmpty(sAutoClickNum))
            {
                FileUtils.IniWriteValue(Constants.CLICK_SECTIONS, Constants.AUTO_CLICK_NUM, 50 + "", IniFile);
            }
            string sMaxPauseTime = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.MAX_PAUSE_TIME, IniFile);
            if (string.IsNullOrEmpty(sMaxPauseTime))
            {
                FileUtils.IniWriteValue(Constants.CLICK_SECTIONS, Constants.MAX_PAUSE_TIME, 60 + "", IniFile);
            }
            string sMaxQueryPage = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.MAX_QUERY_PAGE, IniFile);
            if (string.IsNullOrEmpty(sMaxQueryPage))
            {
                FileUtils.IniWriteValue(Constants.CLICK_SECTIONS, Constants.MAX_QUERY_PAGE, 20 + "", IniFile);
            }
            this.WindowState = FormWindowState.Maximized;
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            vpnDao = DAOFactory.Instance.GetVpnDAO();
            LoadDataview();
        }

        #region 导入关键词菜单，点击设计菜单
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
        #endregion

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

        #region 清空菜单，VPN管理菜单
        private void CleanKeyMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("\r\n        您确认要清空所有数据吗？          \r\n\r\n", "清空关键词", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                keywordDAO.DeleteAll();
                LoadDataview();
            }
        }

        private void VPNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VpnForm f = new VpnForm();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }
        #endregion

        #region DataGridView 初始化处理
        void LoadDataview()
        {
            this.dataGridView1.DataBindings.Clear();
            keywordDAO = DAOFactory.Instance.GetKeywordDAO();
            this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            DataTable dt = new DataTable();
            dt.Columns.Add("Image", typeof(Image));
            dt.Columns.Add("productName", typeof(string));
            dt.Columns.Add("productId", typeof(string));
            dt.Columns.Add("mainKey", typeof(string));
            dt.Columns.Add("rankKey", typeof(string));
            dt.Columns.Add("rank", typeof(string));
            dt.Columns.Add("clicked", typeof(string));
            dt.Columns.Add("updateTime", typeof(DateTime));
            this.dataGridView1.DataSource = dt;

            DataGridViewColumn column0 = this.dataGridView1.Columns[0];
            column0.HeaderText = "产品图片";
            column0.Width = 120;
            DataGridViewColumn column = this.dataGridView1.Columns[1];
            column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column.HeaderText = "产品名称";
            column.Width = 250;
            DataGridViewColumn column2 = this.dataGridView1.Columns[2];
            column2.HeaderText = "产品ID";
            column2.Width = 100;
            DataGridViewColumn column3 = this.dataGridView1.Columns[3];
            column3.HeaderText = "产品关键词";
            column3.Width = 200;
            column3.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DataGridViewColumn columnRankKey = this.dataGridView1.Columns[4];
            columnRankKey.HeaderText = "排名关键词";
            columnRankKey.Width = 150;
            columnRankKey.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DataGridViewColumn column4 = this.dataGridView1.Columns[5];
            column4.HeaderText = "排名状态";
            column4.Width = 200;
            column4.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DataGridViewColumn column5 = this.dataGridView1.Columns[6];
            column5.HeaderText = "点击";
            column5.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column5.Width = 100;
            DataGridViewColumn column6 = this.dataGridView1.Columns[7];
            column6.HeaderText = "更新时间";
            column6.Width = 120;
            List<ShowcaseRankInfo> productList = keywordDAO.GetKeywordList();
            if (productList.Count > 0)
            {
                foreach (ShowcaseRankInfo item in productList)
                {
                    DataRow row = dt.NewRow();
                    if (string.IsNullOrEmpty(item.ProductImg) || !File.Exists(item.ProductImg))
                    {
                        row["Image"] = global::AliRank.Properties.Resources.no_image; 
                    }else {
                        row["Image"] = new Bitmap(Image.FromFile(item.ProductImg));
                    }
                    row["productName"] = item.ProductName;
                    row["productId"] = item.ProductId;
                    row["mainKey"] = item.MainKey.Replace(",", "\r\n\r\n");
                    row["rankKey"] = item.RankKeyword;
                    row["rank"] = ShowcaseRankInfo.GetRankInfo(item);
                    row["clicked"] = Convert.ToString(item.Clicked);
                    row["updateTime"] = item.UpdateTime;
                    dt.Rows.Add(row);                
                }
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
            List<ShowcaseRankInfo> kwList = keywordDAO.GetKeywordList();
            if (kwList == null || kwList.Count == 0)
            {
                return;
            }
            MessageLabel.Text = "开始进行关键词排名查询...";
            AsToolStripMenuItem.Enabled = false;
            toolStripButton4.Enabled = false;
            clickRunBtn.Enabled = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCell cell = row.Cells[5];
                cell.Value = "";
            }
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerAsync();
            bgWorker.Dispose();
        }



        private int iCount = 0;
        private int MaxCount = 10;
        private int iMaxQueryPage = 10;
        ManualResetEvent eventX;
        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string sMaxQueryPage = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.MAX_QUERY_PAGE, IniFile);
            iMaxQueryPage = Convert.ToInt32(sMaxQueryPage);
            List<ShowcaseRankInfo> queryList = keywordDAO.GetKeywordList();
            keywordDAO.UpdateAllQueryStatus();
            MaxCount = queryList.Count;
            if (MaxCount > 0)
            {
                iCount = 0;
                eventX = new ManualResetEvent(false);
                ThreadPool.SetMinThreads(2, 40);
                ThreadPool.SetMaxThreads(5, 200);
                for (int i = 0; i < MaxCount; i++)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(DoRankSearch), (object)queryList[i]);
                }
                eventX.WaitOne(Timeout.Infinite, true);
            }
            toolStripButton4.Enabled = true;
            AsToolStripMenuItem.Enabled = true;
            clickRunBtn.Enabled = true;
            MessageLabel.Text = "";
            Console.WriteLine("线程池结束！");
        }
        
        private void DoRankSearch(object obj)
        {
            ShowcaseRankInfo item = (ShowcaseRankInfo)obj;
            RankQueryer queryer = new RankQueryer();
            queryer.OnRankSearchingEvent += new RankSearchingEvent(queryer_OnRankSearchingEvent);
            queryer.OnRankSearchEndEvent += new RankSearchEndEvent(queryer_OnRankSearchEndEvent);
            queryer.Seacher(item, iMaxQueryPage);
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
            ShowcaseRankInfo item = e.Item;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCell productIdCell = row.Cells[2];
                string id = (string)productIdCell.Value;
                if (Convert.ToInt32(id) == item.ProductId)
                {
                    row.Cells[5].Value = e.Msg;
                    break;
                }
            }
        }

        void queryer_OnRankSearchEndEvent(object sender, RankEventArgs e)
        {
            ShowcaseRankInfo item = e.Item;
            if (item.Rank > 0)
            {
                keywordDAO.UpdateRank(item);
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCell productIdCell = row.Cells[2];
                string id = (string)productIdCell.Value;
                if (Convert.ToInt32(id) ==item.ProductId)
                {
                    row.Cells[5].Value = ShowcaseRankInfo.GetRankInfo(item);
                    break;
                }
            }
        }

        #endregion

        #region 点击处理事件

        System.Timers.Timer clickTimer;
        DateTime beginTime;
        private BackgroundWorker bgClickWorker;
        private List<VpnModel> VpnModelList;
        private VPN vpnEntity;
        private int CurrentActiveVpnIndex;

        private void clickRunBtn_Click(object sender, EventArgs e)
        {
            string network = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.NETWORK_CHOICE, IniFile);
            if (network.Equals(Constants.NETWORK_VPN))
            {
                VpnModelList = null;
                VpnModelList = vpnDao.GetVpnModelList();
                CurrentActiveVpnIndex = 0;
                if (VpnModelList == null || VpnModelList.Count == 0)
                {
                    MessageBox.Show("\r\n您选择了VPN网络点击，但您没有设置任何VPN数据信息.请到[VPN 管理]项设置.\r\n","提示");
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
        
        private bool ConnectNextVpn()
        {
            if (IsStopClicking)
            {
                return true;
            }
            if (vpnEntity != null)
            {
                vpnEntity.Disconnect();
                vpnEntity.Dispose();
            }
            if (CurrentActiveVpnIndex >= VpnModelList.Count)
            {
                CurrentActiveVpnIndex = 0;
            }
            VpnModel model = VpnModelList[CurrentActiveVpnIndex];
            vpnEntity = new VPN("MyVPN", model);
            CurrentActiveVpnIndex++;
            bool Connected = vpnEntity.Connect();
            if (!Connected)
            {
                return ConnectNextVpn();
            }
            return true;
        }

        void bgWorker_DoWork2(object sender, DoWorkEventArgs e)
        {
            List<ShowcaseRankInfo> productList = keywordDAO.GetKeywordList();
            string ConfigClickNum = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.AUTO_CLICK_NUM, IniFile);
            string sNetwork = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.NETWORK_CHOICE, IniFile);
            string sMaxPauseTime = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.MAX_PAUSE_TIME, IniFile);
            string sMaxQueryPage = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.MAX_QUERY_PAGE, IniFile);
            int iMaxQueryPage = Convert.ToInt32(sMaxQueryPage);
            int iRandomMaxTime = Convert.ToInt32(sMaxPauseTime) * 1000;
            //IEHandleUtils.ClearIECache();
            IEHandleUtils.ClearIECookie();
            if (sNetwork.Equals(Constants.NETWORK_VPN))
            {
                int clickNum = Convert.ToInt32(ConfigClickNum);
                for (int n = 0; n < clickNum; n++)
                {
                    bool Connected = ConnectNextVpn();
                    if (!Connected)
                    {
                        toolStripStatusLabel1.Text = "没有正确的VPN可以连接.";
                        break;
                    }
                    if (IsStopClicking) { break; }
                    
                    for (int i = 0; i < productList.Count; i++)
                    {
                        //IEHandleUtils.ClearIECache();
                        IEHandleUtils.ClearIECookie();
                        Random r = new Random();
                        int randomNumber = r.Next(1000, iRandomMaxTime);
                        if (i > 0) Thread.Sleep(randomNumber);
                        ProductClicker clicker = new ProductClicker(webBrowser);
                        clicker.OnRankClickingEvent += new RankClickingEvent(clicker_OnRankClickingEvent);
                        clicker.OnRankClickEndEvent += new RankClickEndEvent(clicker_OnRankClickEndEvent);
                        clicker.DoClick(productList[i], iMaxQueryPage);
                        clicker.OnRankClickingEvent -= new RankClickingEvent(clicker_OnRankClickingEvent);
                        clicker.OnRankClickEndEvent -= new RankClickEndEvent(clicker_OnRankClickEndEvent);
                        if (IsStopClicking) { break; }
                    }
                    if (IsStopClicking) { break; }
                }
            }
            else 
            {
                int clickNum = Convert.ToInt32(ConfigClickNum);
                for (int n = 0; n < clickNum; n++)
                {
                    for (int i = 0; i < productList.Count; i++)
                    {
                        if (i % 3 == 0)
                        {
                            IEHandleUtils.ClearIECookie();
                            Passporter passporter = new AliRank.Passporter(webBrowser);
                            bool loginSuccess = passporter.DoLogin("sales01@soomes.com", "soomes2008");
                        }
                        Random r = new Random();
                        int randomNumber = r.Next(1000, iRandomMaxTime);
                        if (i > 0) Thread.Sleep(randomNumber);
                        
                        ProductClicker clicker = new ProductClicker(webBrowser);
                        clicker.OnRankClickingEvent += new RankClickingEvent(clicker_OnRankClickingEvent);
                        clicker.OnRankClickEndEvent += new RankClickEndEvent(clicker_OnRankClickEndEvent);
                        clicker.OnInquiryEndEvent += new RankInquiryEndEvent(clicker_OnInquiryEndEvent);
                        clicker.DoClick(productList[i], iMaxQueryPage);
                        clicker.OnRankClickingEvent -= new RankClickingEvent(clicker_OnRankClickingEvent);
                        clicker.OnRankClickEndEvent -= new RankClickEndEvent(clicker_OnRankClickEndEvent);
                        clicker.OnInquiryEndEvent -= new RankInquiryEndEvent(clicker_OnInquiryEndEvent);
                        if (IsStopClicking) { break; }
                    }
                    if (IsStopClicking) { break; }
                }
            }
            if (vpnEntity != null)
            {
                vpnEntity.Disconnect();
                vpnEntity.Dispose();
                vpnEntity = null;
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

        void clicker_OnInquiryEndEvent(object sender, RankEventArgs e)
        {
            ShowcaseRankInfo item = e.Item;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCell productIdCell = row.Cells[2];
                string id = (string)productIdCell.Value;
                if (Convert.ToInt32(id) == item.ProductId)
                {
                    DataGridViewCell cell = row.Cells[6];
                    //cell.Value = item.InquiryNum;
                    toolStripStatusLabel1.Text = e.Msg;
                    //keywordDAO.UpdateClicked(item);
                }
            }
        }

        void clicker_OnRankClickEndEvent(object sender, RankEventArgs e)
        {
            ShowcaseRankInfo item = e.Item;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCell productIdCell = row.Cells[2];
                string id = (string)productIdCell.Value;
                if (Convert.ToInt32(id) == item.ProductId)
                {
                    DataGridViewCell cell = row.Cells[6];
                    cell.Value = item.Clicked;
                    toolStripStatusLabel1.Text = e.Msg;
                    keywordDAO.UpdateClicked(item);
                }
            }
        }

        void clicker_OnRankClickingEvent(object sender, RankEventArgs e)
        {
            ShowcaseRankInfo item = e.Item;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCell productIdCell = row.Cells[2];
                string id = (string)productIdCell.Value;
                if (Convert.ToInt32(id) == item.ProductId)
                {
                    DataGridViewCell cell = row.Cells[6];
                    cell.Value = "Clicking...";
                    toolStripStatusLabel1.Text = e.Msg;
                }
            }
        }
        #endregion 

        private void MtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopFiveQueryForm f = new TopFiveQueryForm();
            f.Show();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (vpnEntity != null)
            {
                vpnEntity.Disconnect();
                vpnEntity.Dispose();
                vpnEntity = null;
            }
        }


        #region 右键菜单

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        row.Selected = false;
                    }
                    dataGridView1.Rows[e.RowIndex].Selected = true;
                    SelectedRowIndex = e.RowIndex;
                    RowSelectedProductId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private int RowSelectedProductId = 0;
        private int SelectedRowIndex = 0;
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowcaseRankInfo obj = keywordDAO.GetShowcaseRankInfo(RowSelectedProductId);
            System.Diagnostics.Process.Start("iexplore.exe", obj.CompanyUrl + obj.ProductUrl);
        }

        private void DeleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            keywordDAO.Delete(RowSelectedProductId);
            dataGridView1.Rows.RemoveAt(SelectedRowIndex);
            dataGridView1.Update();
        }

        private void QueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWorkQueryRank);
            bgWorker.RunWorkerAsync();
            bgWorker.Dispose();
        }

        void bgWorker_DoWorkQueryRank(object sender, DoWorkEventArgs e)
        {
            toolStripButton4.Enabled = false;
            clickRunBtn.Enabled = false;
            string sMaxQueryPage = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.MAX_QUERY_PAGE, IniFile);
            iMaxQueryPage = Convert.ToInt32(sMaxQueryPage);
            ShowcaseRankInfo item = keywordDAO.GetShowcaseRankInfo(RowSelectedProductId);
            MessageLabel.Text = "开始查询["+item.RankKeyword+"]关键词排名...";
            RankQueryer queryer = new RankQueryer();
            queryer.OnRankSearchingEvent += new RankSearchingEvent(queryer_OnRankSearchingEvent);
            queryer.OnRankSearchEndEvent += new RankSearchEndEvent(queryer_OnRankSearchEndEvent);
            queryer.Seacher(item, iMaxQueryPage);
            queryer.OnRankSearchingEvent -= new RankSearchingEvent(queryer_OnRankSearchingEvent);
            queryer.OnRankSearchEndEvent -= new RankSearchEndEvent(queryer_OnRankSearchEndEvent);
            toolStripButton4.Enabled = true;
            clickRunBtn.Enabled = true;
            MessageLabel.Text = "";
        }

        private void ModifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModifyWindow f = new ModifyWindow();
            f.iModifyProductId = RowSelectedProductId;
            f.FormClosed +=new FormClosedEventHandler(f_FormClosed1);
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        void f_FormClosed1(object sender, FormClosedEventArgs e)
        {
            if (ModifyWindow.updatedSuccess)
            {
                ShowcaseRankInfo obj = keywordDAO.GetShowcaseRankInfo(RowSelectedProductId);
                dataGridView1.Rows[SelectedRowIndex].Cells[4].Value = obj.RankKeyword;
            }
        }
        #endregion
    }
}
