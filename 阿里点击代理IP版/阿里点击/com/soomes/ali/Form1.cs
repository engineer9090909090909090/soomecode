using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Threading;
using log4net;
using DBUtility.SQLite;
using com.soomes.model;
using System.Collections;

namespace com.soomes.ali
{
    public partial class Form1 : Form
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Form1));
        private string currVpnName = "VPN_123";


        public Form1()
        {
            Log.Debug("Application Begin ...");
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
            this.dataGridView1.Height = (this.tabPage1.Height - 20);
            this.dataGridView1.Width = this.tabPage1.Width - 10;
            this.webBrowser1.Height = (this.tabPage2.Height - 20);
            this.webBrowser1.Width = this.tabPage2.Width - 10;
            this.webBrowser1.ScriptErrorsSuppressed = true;
            ReadData(dataGridView1);
        }

        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.FormClosed -= new FormClosedEventHandler(Form1_FormClosed);
            HttpUtils.SetupProxy(null);
        }

        public static void MagrationVpnData()
        {
            string xmlFile = @"./VPN.xml";
            DataTable dataTable = null;
            if (File.Exists(xmlFile))
            {
                DataSet xmlDS = new DataSet();
                xmlDS.ReadXml(xmlFile);
                dataTable = (DataTable)xmlDS.Tables[0];
                xmlDS.Dispose();
            }
            for (int j = dataTable.Rows.Count - 1; j >= 0; j--)
            {
                DataRow dr = dataTable.Rows[j];
                VpnModel model = new VpnModel();
                model.Ip = (string)dr[0];
                model.UserId = (string)dr[1];
                model.Password = (string)dr[2];
                model.Type = (string)dr[3];
                model.Enabled = true;
                if (!DAO.HasExistIpAddress(model.Ip))
                {
                    DAO.AddVpn(model);
                }
            }
        }

        private void tabPage_SizeChanged(object sender, EventArgs e)
        {
            this.webBrowser1.Height = (this.tabPage2.Height - 20);
            this.webBrowser1.Width = this.tabPage2.Width - 10;
            this.dataGridView1.Height = (this.tabPage1.Height - 20);
            this.dataGridView1.Width = this.tabPage1.Width -10;
        }
        private void addBtn_Click(object sender, EventArgs e)
        {
            ClickerModel model = new ClickerModel();
            model.CompanyUrl = companyText.Text;
            model.KeyWord = keywordText.Text;
            model.ProductId = productidtext.Text;
            if ("".Equals(model.CompanyUrl.Trim()) || "".Equals(model.KeyWord.Trim()) || "".Equals(model.ProductId.Trim()))
            {
                MessageBox.Show("Company Site, KeyWord and Product Id is required.");
                return;
            }
            model.Operate = false;
            model.Enabled = true;
            DAO.AddClicker(model);
            ReadData(dataGridView1);
            Log.Debug("add new row to table.");
            model = null;
        }


        void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i <= this.dataGridView1.RowCount; i++)
            {
                object cell0value = dataGridView1.Rows[i].Cells[0].Value;
                if (cell0value != null && cell0value.ToString() == "True")
                {
                    this.dataGridView1.Rows[i].Selected = true;
                }
            }
        }
        private void saveBtn_Click(object sender, EventArgs e)
        {
            UpdateClickerModelList(this.dataGridView1);
        }

        private void UpdateClickerModelList(DataGridView dataGridView)
        {
            DataTable dt = (DataTable)dataGridView.DataSource;
            for (int j = dt.Rows.Count - 1; j >= 0; j--)
            {
                DataRow dr = dt.Rows[j];
                ClickerModel model = new ClickerModel();
                model.Id = (Int64)dr[0];
                model.Operate = (bool)dr[1];
                model.CompanyUrl = (string)dr[2];
                model.KeyWord = (string)dr[3];
                model.ProductId = (string)dr[4];
                model.Enabled = (bool)dr[5];
                model.ClickedNum = (Int64)dr[6];
                model.PageRank = (Int64)dr[7];
                DAO.UpdateClicker(model);
            }
        }
        private void deleBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1 == null || dataGridView1.Rows.Count == 0)
            {
                return;
            }
            int i = 0;
            DataTable dt = (DataTable)dataGridView1.DataSource;
            for (int j = dt.Rows.Count -1; j >= 0 ; j--)
            { 
                DataRow dr = dt.Rows[j];
                Boolean chk = System.Boolean.Parse(dr[1].ToString());
                if (chk)
                {
                    long id = (Int64)dr[0];
                    DAO.DeleteClicker(id);
                    dt.Rows.Remove(dr);
                    i++;
                }
            }
            if (i > 0)
            {
                MessageBox.Show(i + " row(s) records be deleted.");
            }
        }

        public static void ReadData(DataGridView dataGridView1)
        {
            DataTable dataTable = DefineTable();
            DataSet dataSet = DAO.GetAllClickerList();
            if (dataSet.Tables.Count > 0)
            {
                CopyDataToTable(dataTable, dataSet.Tables[0]);
            } 
            dataGridView1.DataSource = dataTable;
            dataSet.Dispose();
            dataTable.Dispose();
        }

        public static void CopyDataToTable(DataTable newTable, DataTable oldTable)
        {
            if (oldTable != null && oldTable.Rows.Count > 0)
            {
                foreach (DataRow dr in oldTable.Rows)
                {
                    DataRow newDr = newTable.NewRow();
                    newDr.ItemArray = dr.ItemArray;
                    newTable.Rows.Add(newDr);
                }
            }
        }

        public static DataTable DefineTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(Int64));
            table.Columns.Add("Operate", System.Type.GetType("System.Boolean"));
            table.Columns.Add("Company Site Url", typeof(string));
            table.Columns.Add("Keyword", typeof(string));
            table.Columns.Add("Product ID", typeof(string));
            table.Columns.Add("Is Run", System.Type.GetType("System.Boolean"));
            table.Columns.Add("Clicked Number", typeof(Int64));
            table.Columns.Add("Page Ranking", typeof(Int64));
            table.Columns[0].ColumnMapping = MappingType.Hidden;
            return table;
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            stopBtn.Enabled = false;
            RequestStop();
            backgroundWorker.CancelAsync();
            statusLabel.Text = "Stopping background clicker processing, Please waiting....";
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            Form.CheckForIllegalCrossThreadCalls = false;
            if (backgroundWorker.CancellationPending && backgroundWorker.IsBusy)
            {
                MessageBox.Show("Background clicker processing is running, Please waiting....");
                return;
            }
            int runTime = Convert.ToInt16(this.runTimeText.Text);
            startBtn.Enabled = false;
            addBtn.Enabled = false;
            deleBtn.Enabled = false;
            stopBtn.Enabled = true;
            runTimeText.Enabled = false;
            netset_proxyIp.Enabled = false;
            netset_vpn.Enabled = false;
            saveBtn.Enabled = false;
            tabControl1.SelectedIndex = 1;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(BackgroundWorker_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_Completed);
            backgroundWorker.RunWorkerAsync();
        }

        private Searcher searcher;
        private volatile bool _shouldStop;
        private void BackgroundWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            backgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(BackgroundWorker_Completed);
            DisConnection();
            stopBtn.Enabled = false;
            addBtn.Enabled = true;
            deleBtn.Enabled = true;
            startBtn.Enabled = true;
            runTimeText.Enabled = true;
            netset_proxyIp.Enabled = true;
            netset_vpn.Enabled = true;
            saveBtn.Enabled = true;
            tabControl1.SelectedIndex = 0;
            UpdateClickerModelList(this.dataGridView1);
            HttpUtils.SetupProxy(null);
            statusLabel.Text = "background clicker processing be stopped.";
        }


        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            IList<VpnModel> vpnModelList = DAO.GetAllVpnAddress();
            IList<ProxyIpModel> proxyIpModelList = DAO.GetEnableProxyList();
            if (netset_proxyIp.Checked && proxyIpModelList.Count == 0)
            {
                backgroundWorker.DoWork -= new DoWorkEventHandler(BackgroundWorker_DoWork);
                MessageBox.Show("Have not proxy IPs in this application. Please click [Set Web Proxy] button setting it.");
                proxyIpModelList = null;
                vpnModelList = null;
                return;                
            }
            else if (netset_vpn.Checked && vpnModelList.Count == 0)
            {
                backgroundWorker.DoWork -= new DoWorkEventHandler(BackgroundWorker_DoWork);
                MessageBox.Show("Have not VPNs in this application. Please click [Set VPN List] button setting it.");
                proxyIpModelList = null;
                vpnModelList = null;
                return;
            }

            int totalRunTime = Int32.Parse(this.runTimeText.Text);
            int nextVpnId = 0;
            int runTime = 0;

            while (!_shouldStop && runTime <= totalRunTime)
            {
                if (this.netset_proxyIp.Checked && proxyIpModelList.Count > 0)
                {
                    ProxyIpModel proxyIpModel = proxyIpModelList[nextVpnId];
                    HttpUtils.SetupProxy(proxyIpModel.Ip);
                    HttpUtils.RefreshIESettings(proxyIpModel.Ip);
                    Thread.Sleep(2000);
                    nextVpnId = (nextVpnId + 1 == proxyIpModelList.Count) ? 0 : nextVpnId + 1;
                }
                if (this.netset_vpn.Checked && vpnModelList.Count > 0)
                {
                    VpnModel vpnModel = vpnModelList[nextVpnId];
                    currVpnName = "VPN_" + vpnModel.Type;
                    VPN.CreateVPN(currVpnName, vpnModel.Ip, vpnModel.UserId, vpnModel.Password);
                    Thread.Sleep(15000);
                    nextVpnId = (nextVpnId + 1 == vpnModelList.Count) ? 0 : nextVpnId + 1;
                }
                BgWorkerSearch((DataTable)dataGridView1.DataSource);
                runTime = runTime + 1;
                DisConnection();
            }
            proxyIpModelList = null;
            vpnModelList = null;
        }

        public void DisConnection()
        {
            if (this.netset_proxyIp.Checked)
            {
                HttpUtils.SetupProxy(null);
                HttpUtils.RefreshIESettings(null);
            }
            else if (this.netset_vpn.Checked && this.currVpnName != null)
            {
                VPN.DisconnectFromVPN(this.currVpnName);
                Thread.Sleep(3000);
            }
        }

        public void RequestStop()
        {
            if (searcher != null)
            {
                searcher.Stop();
            }
            _shouldStop = true;
        }

        private void BgWorkerSearch(DataTable kwDataTable)
        {
            IList<ClickerModel> clickerModellist = DAO.GetClickerModelList();
            foreach (ClickerModel model in clickerModellist)
            {
                if (!model.Enabled)
                {
                    continue;
                }
                IEHandleUtils.ClearIECookie();
                IEHandleUtils.ClearIECache();
                searcher = new Searcher(this, model);
                int[] result = searcher.DoSearchClick();
                if (result[0] == 1)
                {
                    model.ClickedNum = model.ClickedNum + 1;
                    model.PageRank = result[1];
                    DAO.UpdateClicker(model);
                    UpdateDataView(kwDataTable, model);
                }
                searcher = null;
                if (this._shouldStop)
                {
                    break;
                }
            }
        }

        public void UpdateDataView(DataTable kwDataTable, ClickerModel model) 
        {
            foreach (DataRow dr in kwDataTable.Rows)
            {
                long id = (Int64)dr[0];
                if (id == model.Id)
                {
                    dr[6] = model.ClickedNum;
                    dr[7] = model.PageRank;
                }
            }
        }


        private void proxyBtn_Click(object sender, EventArgs e)
        {
            if (proxyForm == null)
            {
                proxyForm = new ProxyIPsForm();
                proxyForm.FormClosed +=new FormClosedEventHandler(proxyForm_FormClosed);
                proxyForm.ShowInTaskbar = false;
                proxyForm.Show();
            }
            else 
            {
                if (proxyForm.MinimizeBox) 
                {
                    proxyForm.WindowState = FormWindowState.Normal;
                    proxyForm.StartPosition = FormStartPosition.CenterScreen;
                }
                proxyForm.Focus();
            }
            
        }
        private void proxyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            proxyForm.FormClosed -= new FormClosedEventHandler(proxyForm_FormClosed);
            proxyForm = null;
        }
    }
}
