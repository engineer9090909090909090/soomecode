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
using AliRank.IpSearch;
using AliRank.Bussness;

namespace AliRank
{
    public partial class MainForm : Form
    {
        private KeywordDAO keywordDAO;
        private VpnDAO vpnDao;
        private InquiryDAO inquiryDao;
        private static bool IsStop;
        private bool AutoShutdown;
        private IpAddressSearchWebServiceSoapClient soapClient;

        #region Form 事件
        public MainForm()
        {
            InitializeComponent();
            string sAutoClickNum = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.AUTO_CLICK_NUM);
            if (string.IsNullOrEmpty(sAutoClickNum))
            {
                DAOFactory.Instance.GetProfileDAO().SetValue(Constants.AUTO_CLICK_NUM, 50 + "");
            }
            string sMaxPauseTime = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.MAX_PAUSE_TIME);
            if (string.IsNullOrEmpty(sMaxPauseTime))
            {
                DAOFactory.Instance.GetProfileDAO().SetValue(Constants.MAX_PAUSE_TIME, 5 + "");
            }
            string sMaxIntervalTime = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.MAX_INTERVAL_TIME);
            if (string.IsNullOrEmpty(sMaxIntervalTime))
            {
                DAOFactory.Instance.GetProfileDAO().SetValue(Constants.MAX_INTERVAL_TIME, 50 + "");
            }

            string sMinIntervalTime = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.MIN_INTERVAL_TIME);
            if (string.IsNullOrEmpty(sMinIntervalTime))
            {
                DAOFactory.Instance.GetProfileDAO().SetValue(Constants.MIN_INTERVAL_TIME, 10 + "");
            }
            string sMaxQueryPage = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.MAX_QUERY_PAGE);
            if (string.IsNullOrEmpty(sMaxQueryPage))
            {
                DAOFactory.Instance.GetProfileDAO().SetValue(Constants.MAX_QUERY_PAGE, 20 + "");
            }
            this.WindowState = FormWindowState.Maximized;
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            vpnDao = DAOFactory.Instance.GetVpnDAO();
            inquiryDao = DAOFactory.Instance.GetInquiryDAO();
            keywordDAO = DAOFactory.Instance.GetKeywordDAO();
            soapClient = new IpAddressSearchWebServiceSoapClient();
            LoadDataview();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CurrVpnEntity != null)
            {
                vpnDao.UpdateAllVPNStatus(Constants.EFFECTIVE);
                CurrVpnEntity.Disconnect();
                CurrVpnEntity.Dispose();
                CurrVpnEntity = null;
            }
        }
        #endregion

        #region 导入关键词菜单，点击设置菜单
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

        #region 清空菜单，VPN管理菜单, 关机菜单

        /// <summary>
        /// 清空菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CleanKeyMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("\r\n        您确认要清空所有数据吗？          \r\n\r\n", "清空关键词", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                keywordDAO.DeleteAll();
                LoadDataview();
            }
        }

        /// <summary>
        /// VPN管理菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VPNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VpnForm f = new VpnForm();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }


        private void MtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopFiveQueryForm f = new TopFiveQueryForm();
            f.Show();
        }

        private void AccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountForm f = new AccountForm();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        private void MessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageForm f = new MessageForm();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }
        /// <summary>
        /// 关机菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shutdownStripMenuItem_Click(object sender, EventArgs e)
        {
            if (shutdownToolStripMenuItem.Checked)
            {
                shutdownToolStripMenuItem.Checked = false;
                AutoShutdown = false;
            }
            else
            {
                shutdownToolStripMenuItem.Checked = true;
                AutoShutdown = true;
            }
        }

        

        #endregion

        #region DataGridView 初始化处理
        void LoadDataview()
        {
            this.dataGridView1.DataBindings.Clear();
            
            this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            DataTable dt = new DataTable();
            dt.Columns.Add("Image", typeof(Image));
            dt.Columns.Add("productName", typeof(string));
            dt.Columns.Add("productId", typeof(string));
            dt.Columns.Add("mainKey", typeof(string));
            dt.Columns.Add("rankKey", typeof(string));
            dt.Columns.Add("rank", typeof(string));
            dt.Columns.Add("clicked", typeof(string));
            dt.Columns.Add("factInquiryQty", typeof(string));
            dt.Columns.Add("maxInquiryQty", typeof(string));
            dt.Columns.Add("status", typeof(string));
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
            column3.Width = 150;
            column3.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DataGridViewColumn columnRankKey = this.dataGridView1.Columns[4];
            columnRankKey.HeaderText = "排名关键词";
            columnRankKey.Width = 150;
            columnRankKey.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DataGridViewColumn column4 = this.dataGridView1.Columns[5];
            column4.HeaderText = "排名状态";
            column4.Width = 150;
            column4.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DataGridViewColumn column5 = this.dataGridView1.Columns[6];
            column5.HeaderText = "点击";
            column5.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column5.Width = 70;
            DataGridViewColumn column6 = this.dataGridView1.Columns[7];
            column6.HeaderText = "今日询盘";
            column6.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column6.Width = 80;
            DataGridViewColumn column7 = this.dataGridView1.Columns[8];
            column7.HeaderText = "最大询盘";
            column7.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column7.Width = 80;
            DataGridViewColumn column8 = this.dataGridView1.Columns[9];
            column8.HeaderText = "询盘状态";
            column8.Width = 80;
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
                    row["factInquiryQty"] = item.FactInquiryQty;
                    row["maxInquiryQty"] = item.MaxInquiryQty;
                    row["status"] = item.Status == 2 ? "询盘":"不询盘";
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
            string sMaxQueryPage = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.MAX_QUERY_PAGE);
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
        private ProductClicker clicker;
        private VPN CurrVpnEntity;
        private VpnModel CurrVpnModel;
        private AliAccounts InquiryUser;

        private void clickRunBtn_Click(object sender, EventArgs e)
        {
            string network = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.NETWORK_CHOICE);
            if (network.Equals(Constants.NETWORK_VPN))
            {
                List<VpnModel> VpnModelList = vpnDao.GetVpnModelList();
                if (VpnModelList == null || VpnModelList.Count == 0)
                {
                    MessageBox.Show("\r\n您选择了VPN网络点击，但您没有设置任何VPN数据信息.请到[VPN 管理]项添加.\r\n","提示");
                    return;
                }
            }
            IsStop = false;
            
            tabControl.SelectedIndex = 1;
            if (bgClickWorker != null)
            {
                bgClickWorker.Dispose();
                bgClickWorker = null;
            }
            bgClickWorker = new BackgroundWorker();
            bgClickWorker.WorkerSupportsCancellation = true;
            bgClickWorker.DoWork += new DoWorkEventHandler(bgWorker_Click);
            bgClickWorker.RunWorkerAsync();
            vpnDao.UpdateAllVPNStatus(Constants.EFFECTIVE);
            if (clickTimer != null)
            {
                clickTimer.Dispose();
                clickTimer = null;
            }
            bgClickWorker.Dispose();
            clickTimer = new System.Timers.Timer(1000);
            beginTime = DateTime.Now;
            clickTimer.Elapsed += new System.Timers.ElapsedEventHandler(theout);
            clickTimer.AutoReset = true;
            clickTimer.Enabled = true;
            clickRunBtn.Enabled = false;
            clickStopBtn.Enabled = true;
        }

        private void clickStopBtn_Click(object sender, EventArgs e)
        {
            clickStopBtn.Enabled = false;
            clickTimer.Enabled = false;
            IsStop = true;
            bgClickWorker.CancelAsync();
            if (clicker != null)
            {
                clicker.Stop();
            }
        }

        public void theout(object source, System.Timers.ElapsedEventArgs e) 
        {
            TimeSpan ts = DateTime.Now - beginTime;
            this.RunTime.Text = ts.Hours.ToString("00") + " : " + ts.Minutes.ToString("00") + " : " + ts.Seconds.ToString("00"); 
        }

        private bool ConnectNextVpn(string vpnAddress)
        {
            if (IsStop)
            {
                return true;
            }
            if (CurrVpnEntity != null)
            {
                CurrVpnEntity.Disconnect();
                CurrVpnEntity.Dispose();
            }
            if (!string.IsNullOrEmpty(vpnAddress))
            {
                CurrVpnModel = vpnDao.GetVpnModelByIpAddress(vpnAddress);
            }
            else
            {
                CurrVpnModel = vpnDao.GetEffctiveVPN();
            }
            if (CurrVpnModel == null)
            {
                return false;
            }
            this.MessageLabel.Text = "正在连接到VPN地址" + CurrVpnModel.Address;
            this.toolStripStatusLabel1.Text = "正在连接到VPN地址" + CurrVpnModel.Address;
            CurrVpnEntity = new VPN("MyVPN", CurrVpnModel);
            bool Connected = CurrVpnEntity.Connect();
            if (!Connected)
            {
                vpnDao.UpdateVPNStatus(CurrVpnModel.Address, Constants.INVALID);
                return ConnectNextVpn(null);
            } 
            else 
            {
                vpnDao.AddVPNConnQty(CurrVpnModel.Address, Constants.EFFECTIVE);
            }
            return true;
        }
        
        private string GetIpInfo(string address) 
        {
            string ipInfo = string.Empty;
            try
            {
                string[] ips = soapClient.getCountryCityByIp(address);
                if (ips != null && ips.Length > 1)
                {
                    ipInfo = ips[0] + "[" + ips[1].Trim() + "]";
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e.Message);
                ipInfo = address + "[获取地址失败]";
            }
            return ipInfo;
        }

        void bgWorker_Click(object sender, DoWorkEventArgs e)
        {
            List<ShowcaseRankInfo> productList = keywordDAO.GetClickProducts();
            string ConfigClickNum = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.AUTO_CLICK_NUM);
            string sNetwork = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.NETWORK_CHOICE);
            string sMaxPauseTime = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.MAX_PAUSE_TIME);
            string sMaxQueryPage = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.MAX_QUERY_PAGE);
            int iMaxQueryPage = Convert.ToInt32(sMaxQueryPage);
            int iRandomMaxTime = Convert.ToInt32(sMaxPauseTime) * 1000;
            if (iRandomMaxTime < 10000) iRandomMaxTime = 2000;

            IEHandleUtils.ClearIECookie();
            string ipAddress = string.Empty;

            int clickNum = Convert.ToInt32(ConfigClickNum);
            for (int n = 0; n < clickNum; n++)
            {
                if (sNetwork == Constants.NETWORK_VPN)
                {
                    if (!ConnectNextVpn(null))//连接到一个新的VPN地址
                    {
                        toolStripStatusLabel1.Text = "没有正确可用的VPN可以连接.";
                        IsStop = true;
                        break;
                    }
                    ipAddress = CurrVpnModel.Address;
                }
                for (int i = 0; i < productList.Count; i++)
                {
                    if (i > 0)
                    {
                        int randomNumber = new Random().Next(2000, iRandomMaxTime);
                        toolStripStatusLabel1.Text = "轮循到下一个产品，暂停" + randomNumber / 1000 + "秒.";
                        if (!IsStop) Thread.Sleep(randomNumber);
                    }
                    
                    if (sNetwork == Constants.NETWORK_NONE)
                    {
                        ipAddress = HttpHelper.Ip138GetIp();
                    }

                    //显示IP信息
                    toolStripStatusLabel1.Text = "读取IP信息数据.";
                    this.MessageLabel.Text = GetIpInfo(ipAddress);
                    if (IsStop) { break; }
                    ShowcaseRankInfo productObj = productList[i];
                    if (IsStop) { break; }
                    IEHandleUtils.ClearIECookie();
                    clicker = new ProductClicker(webBrowser);
                    clicker.OnRankClickingEvent += new RankClickingEvent(clicker_OnRankClickingEvent);
                    clicker.OnRankClickEndEvent += new RankClickEndEvent(clicker_OnRankClickEndEvent);
                    toolStripStatusLabel1.Text = "开始点击操作。";
                    clicker.Click(productObj, iMaxQueryPage, null, false, null);
                    clicker.OnRankClickingEvent -= new RankClickingEvent(clicker_OnRankClickingEvent);
                    clicker.OnRankClickEndEvent -= new RankClickEndEvent(clicker_OnRankClickEndEvent);
                    clicker = null;
                    toolStripStatusLabel1.Text = "点击操作结束。";
                    if (IsStop) { break; }
                }
                if (IsStop) { break; }
                GC.Collect();
            }
            
            if (CurrVpnEntity != null)
            {
                toolStripStatusLabel1.Text = "断开VPN连接。";
                CurrVpnEntity.Disconnect();
                CurrVpnEntity.Dispose();
                CurrVpnModel = null;
                CurrVpnEntity = null;
            }
            clickRunBtn.Enabled = true;
            clickTimer.Enabled = false;

            if (IsStop == false && this.AutoShutdown)
            {
                SoomesUtils.Shutdown();
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

        #region GridView右键菜单

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
                    SelectRowObject = keywordDAO.GetShowcaseRankInfo(RowSelectedProductId);
                    ChangeStatusTsmItem.Text = SelectRowObject.Status == 2 ? "取消询盘状态" : "改变到询盘状态";
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private int RowSelectedProductId = 0;
        private int SelectedRowIndex = 0;
        private ShowcaseRankInfo SelectRowObject;
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", SelectRowObject.CompanyUrl + SelectRowObject.ProductUrl);
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

        private void ChangeStatusTsmItem_Click(object sender, EventArgs e)
        {
            int status = SelectRowObject.Status == 2 ? 1 : 2;
            keywordDAO.UpadateProductStatus(RowSelectedProductId, status);
            dataGridView1.Rows[SelectedRowIndex].Cells[9].Value = (status == 2) ? "询盘" : "不询盘";
        }

        void bgWorker_DoWorkQueryRank(object sender, DoWorkEventArgs e)
        {
            toolStripButton4.Enabled = false;
            clickRunBtn.Enabled = false;
            string sMaxQueryPage = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.MAX_QUERY_PAGE);
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

        private void MaxInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MaxInWindow f = new MaxInWindow();
            f.iModifyProductId = RowSelectedProductId;
            f.FormClosed +=new FormClosedEventHandler(f_FormClosed1);
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }
        #endregion

        #region 询盘
        public bool DoLoginAliWebSite(WebBrowser webBrowser, AliAccounts account, string loginedIp)
        {
            Passporter passporter = new AliRank.Passporter(webBrowser);
            bool loginSuccess = passporter.DoLogin(account);
            passporter = null;
            if (!loginSuccess)
            {
                inquiryDao.DisableAccount(account.Account);
                return false;
            }
            else
            {
                account.LoginIp = loginedIp;
                inquiryDao.UpdateAccountLoginIp(account.Account, loginedIp);
            }
            return true;
        }

        void clicker_OnInquiryEndEvent(object sender, InquiryEventArgs e)
        {
            InquiryInfos item = e.Item;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCell productIdCell = row.Cells[2];
                string id = (string)productIdCell.Value;
                if (Convert.ToInt32(id) == item.ProductId)
                {
                    DataGridViewCell cell = row.Cells[7];
                    cell.Value = (Convert.ToInt32(cell.Value) + 1).ToString();
                    toolStripStatusLabel1.Text = e.Msg;
                    inquiryDao.InsertInquiryInfos(item);
                    inquiryDao.AddInqMessageSendNum(item.MsgId);
                }
            }
        }

        private void InquiryRunBtn_Click(object sender, EventArgs e)
        {
            List<InquiryMessages> msgList = inquiryDao.GetInquiryMessages();
            if (msgList == null || msgList.Count == 0)
            {
                MessageBox.Show("\r\n您没有设置任何询盘信息数据信息.请到[询盘信息管理]项添加.\r\n", "提示");
                return;
            }
            List<AliAccounts> accoutList = inquiryDao.GetAccounts();
            if (accoutList == null || accoutList.Count == 0)
            {
                MessageBox.Show("\r\n您没有设置任何阿里帐号信息.请到[阿里帐号管理]项添加.\r\n", "提示");
                return;
            }
            
            IsStop = false;
            tabControl.SelectedIndex = 1;
            if (bgClickWorker != null)
            {
                bgClickWorker.Dispose();
                bgClickWorker = null;
            }
            bgClickWorker = new BackgroundWorker();
            bgClickWorker.WorkerSupportsCancellation = true;
            bgClickWorker.DoWork += new DoWorkEventHandler(bgWorker_Inquiry);
            bgClickWorker.RunWorkerAsync();
            vpnDao.UpdateAllVPNStatus(Constants.EFFECTIVE);
            if (clickTimer != null)
            {
                clickTimer.Dispose();
                clickTimer = null;
            }
            bgClickWorker.Dispose();
            clickTimer = new System.Timers.Timer(1000);
            beginTime = DateTime.Now;
            clickTimer.Elapsed += new System.Timers.ElapsedEventHandler(theout);
            clickTimer.AutoReset = true;
            InquiryRunBtn.Enabled = false;
            InquiryStopBtn.Enabled = true;
            clickTimer.Enabled = true;
        }

        private void InquiryStopBtn_Click(object sender, EventArgs e)
        {
            InquiryStopBtn.Enabled = false;
            clickTimer.Enabled = false;
            IsStop = true;
            bgClickWorker.CancelAsync();
            if (clicker != null)
            {
                clicker.Stop();
            }
        }

        void bgWorker_Inquiry(object sender, DoWorkEventArgs e)
        {
            List<ShowcaseRankInfo> productList = keywordDAO.GetClickProducts();
            string sNetwork = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.NETWORK_CHOICE);
            string sMaxQueryPage = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.MAX_QUERY_PAGE);
            string sMinInterval = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.MIN_INTERVAL_TIME);
            string sMaxInterval = DAOFactory.Instance.GetProfileDAO().GetValue(Constants.MAX_INTERVAL_TIME);
            int iMaxQueryPage = Convert.ToInt32(sMaxQueryPage);
            int iMinInterval = Convert.ToInt32(sMinInterval) * 1000 * 60;
            int iMaxInterval = Convert.ToInt32(sMaxInterval) * 1000 * 60;
            InquiryMessages inquiryMessages = null;
            string ipAddress = string.Empty;
            while (!IsStop)
            {
                //查找一个未询盘的阿里帐号
                int today = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
                InquiryUser = inquiryDao.GetCanInquiryAccount(today);
                if (sNetwork == Constants.NETWORK_VPN)
                {
                    if (!ConnectNextVpn(InquiryUser.LoginIp))//连接到一个新的VPN地址
                    {
                        toolStripStatusLabel1.Text = "没有正确可用的VPN可以连接.";
                        IsStop = true;
                        break;
                    }
                    ipAddress = CurrVpnModel.Address;
                }
                if (sNetwork == Constants.NETWORK_NONE)
                {
                    ipAddress = HttpHelper.Ip138GetIp();
                }

                //显示IP信息
                toolStripStatusLabel1.Text = "读取IP信息数据.";
                this.MessageLabel.Text = GetIpInfo(ipAddress);
                if (IsStop) { break; }

                toolStripStatusLabel1.Text = "进行用户[" + InquiryUser.Account + "]自动登录操作。";
                bool LoginSuccess = DoLoginAliWebSite(this.webBrowser,InquiryUser, ipAddress);
                if (!LoginSuccess)
                {
                    toolStripStatusLabel1.Text = "用户[" + InquiryUser.Account + "]自动登录失败, 转到下一个用户。";
                    break;
                }

                //查找一个要询盘的产品
                ShowcaseRankInfo productItem = keywordDAO.GetEffctiveProduct();
                if (productItem == null)
                {
                    toolStripStatusLabel1.Text = "经查询没有需要询盘的产品.";
                    IsStop = true;
                    break;
                }
                inquiryMessages = inquiryDao.GetInquiryMinMessage();

                if (IsStop) { break; }

                IEHandleUtils.ClearIECookie();
                clicker = new ProductClicker(webBrowser);
                clicker.OnRankClickingEvent += new RankClickingEvent(clicker_OnRankClickingEvent);
                clicker.OnRankClickEndEvent += new RankClickEndEvent(clicker_OnRankClickEndEvent);
                clicker.OnInquiryEndEvent += new RankInquiryEndEvent(clicker_OnInquiryEndEvent);
                toolStripStatusLabel1.Text = "开始自动询盘操作。";
                clicker.Click(productItem, iMaxQueryPage, InquiryUser, true, inquiryMessages);
                clicker.OnRankClickingEvent -= new RankClickingEvent(clicker_OnRankClickingEvent);
                clicker.OnRankClickEndEvent -= new RankClickEndEvent(clicker_OnRankClickEndEvent);
                clicker.OnInquiryEndEvent -= new RankInquiryEndEvent(clicker_OnInquiryEndEvent);
                clicker = null;
                toolStripStatusLabel1.Text = "询盘操作结束。";
                if (IsStop) { break; }
                int puaseTime = new Random().Next(iMinInterval, iMaxInterval);
                toolStripStatusLabel1.Text = "询盘操作替停" + (puaseTime / 1000 / 60) + "分钟。";
                Thread.Sleep(puaseTime);
                GC.Collect();
            }

            if (CurrVpnEntity != null)
            {
                toolStripStatusLabel1.Text = "断开VPN连接。";
                CurrVpnEntity.Disconnect();
                CurrVpnEntity.Dispose();
                CurrVpnModel = null;
                CurrVpnEntity = null;
            }
            toolStripStatusLabel1.Text = "";
            MessageLabel.Text = "";
            InquiryRunBtn.Enabled = true;
            InquiryStopBtn.Enabled = false;

        }
        #endregion

        

    }
}
