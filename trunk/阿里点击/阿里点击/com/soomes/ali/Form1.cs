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

namespace com.soomes.ali
{
    public partial class Form1 : Form
    {

        static string xmlFile = @"./data.xml";

        public Form1()
        {
            InitializeComponent();
            this.dataGridView1.Height = (this.tabPage1.Height - 20);
            this.dataGridView1.Width = this.tabPage1.Width - 10;
            this.webBrowser1.Height = (this.tabPage2.Height - 20);
            this.webBrowser1.Width = this.tabPage2.Width - 10;
            this.webBrowser1.ScriptErrorsSuppressed = true;
            ReadXmlFile(dataGridView1);
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
            string site = companyText.Text;
            string keyword = keywordText.Text;
            string productid = productidtext.Text;
            if ("".Equals(site.Trim()) || "".Equals(keyword.Trim()) || "".Equals(productid.Trim()))
            {
                MessageBox.Show("Company Site, KeyWord and Product Id is required.");
                return;
            }
            DataTable dt = (DataTable)dataGridView1.DataSource;
            int cid = this.GetId(dt);
            object[] arr = { false, site, keyword, productid, true, 0, "" };
            dt.Rows.Add(arr);            
            //companyText.Text = "";
            keywordText.Text = "";
            productidtext.Text = "";
            WriteXMLFile(dataGridView1);
        }

        private int GetId(DataTable dt)
        {
            int id = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int tid = Convert.ToInt32(dt.Rows[i].ItemArray[0]);
                    if (tid > id)
                    {
                        id = tid;
                    }
                }
            }
            return (id + 1);
        }


        void dataGridView1_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
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
                Boolean chk = System.Boolean.Parse(dr[0].ToString());
                if (chk)
                {
                    dt.Rows.Remove(dr);
                    i++;
                }
            }
            if (i > 0)
            {
                WriteXMLFile(dataGridView1);
                MessageBox.Show(i + " row(s) records be deleted.");
            }
        }

        public static void ReadXmlFile(DataGridView dataGridView1)
        {
            DataTable dataTable = DefineTable();
            DataSet xmlDS = new DataSet();
            if (File.Exists(xmlFile))
            {
               
                xmlDS.ReadXml(xmlFile);
                if (xmlDS.Tables.Count > 0)
                {
                    CopyDataToTable(dataTable, xmlDS.Tables[0]);
                }   
            }
            dataGridView1.DataSource = dataTable;
            xmlDS.Dispose();
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
                    newDr[0] = System.Boolean.Parse(dr[0].ToString());
                    newDr[4] = System.Boolean.Parse(dr[4].ToString());
                    newTable.Rows.Add(newDr);
                }
            }

        }

        public static void WriteXMLFile(DataGridView dataGridView1)
        {
            DataSet xmlDS = new DataSet();
            DataTable dt = (DataTable)dataGridView1.DataSource;
            
            DataTable dt1 = dt.Copy();
            xmlDS.Tables.Add(dt1);
            xmlDS.WriteXml(xmlFile);
            xmlDS.Dispose();
        }

        public static DataTable ReadVpnDataTable()
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
            return dataTable;
        }

        public static DataTable DefineTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Operate", System.Type.GetType("System.Boolean"));
            table.Columns.Add("Company Site Url", typeof(string));
            table.Columns.Add("Keyword", typeof(string));
            table.Columns.Add("Product ID", typeof(string));
            table.Columns.Add("Is Run", System.Type.GetType("System.Boolean"));
            table.Columns.Add("Clicked Number", typeof(Int64));
            table.Columns.Add("Page Ranking", typeof(string));
            return table;
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {

            stopBtn.Enabled = false;
            addBtn.Enabled = true;
            deleBtn.Enabled = true;
            startBtn.Enabled = true;
            runTimeText.Enabled = true;
            netset_adsl.Enabled = true;
            netset_vpn.Enabled = true;
            netset_none.Enabled = true;
            saveBtn.Enabled = true;
            tabControl1.SelectedIndex = 0;
            RequestStop();
            backgroundWorker.CancelAsync();
            backgroundWorker.DoWork -= new DoWorkEventHandler(BackgroundWorker_DoWork);
            backgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(BackgroundWorker_Completed);
            WriteXMLFile(this.dataGridView1);
            backgroundWorker.Dispose();
            
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.CancellationPending && backgroundWorker.IsBusy)
            {
                MessageBox.Show("Background Worker Processing Still Running, Please Waiting....");
                return;
            }
            int runTime = Convert.ToInt16(this.runTimeText.Text);
            startBtn.Enabled = false;
            addBtn.Enabled = false;
            deleBtn.Enabled = false;
            stopBtn.Enabled = true;
            runTimeText.Enabled = false;
            netset_adsl.Enabled = false;
            netset_vpn.Enabled = false;
            netset_none.Enabled = false;
            saveBtn.Enabled = false;
            tabControl1.SelectedIndex = 1;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(BackgroundWorker_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_Completed);
            backgroundWorker.RunWorkerAsync();
        }

        private Searcher searcher;
        private volatile bool _shouldStop;
        private string currVpnName;
        private void BackgroundWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            URLSecurityZoneAPI.InternetSetFeatureEnabled(URLSecurityZoneAPI.InternetFeaturelist.DISABLE_NAVIGATION_SOUNDS, URLSecurityZoneAPI.SetFeatureOn.PROCESS, true);
            this.stopBtn.Enabled = false;
            this.addBtn.Enabled = true;
            this.deleBtn.Enabled = true;
            this.startBtn.Enabled = true;
            runTimeText.Enabled = true;
            netset_adsl.Enabled = true;
            netset_vpn.Enabled = true;
            netset_none.Enabled = true;
            saveBtn.Enabled = true;
            this.tabControl1.SelectedIndex = 0;
            DisConnectionVPN();
        }


        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable kwDataTable = (DataTable)dataGridView1.DataSource;
            DataTable vpnDataTable = ReadVpnDataTable();
            int totalRunTime = Int32.Parse(this.runTimeText.Text);
            int nextVpnId = 0;
            int beRunTime = 0;
            this._shouldStop = false;

            while (!_shouldStop)
            {
                if (beRunTime >= totalRunTime)
                {
                    this._shouldStop = true;
                    vpnDataTable.Dispose();
                    return;
                }
                if (this.netset_adsl.Checked)
                {
                    RASDisplay ras = new RASDisplay();
                    ras.Disconnect();//断开连接
                    ras.Connect("ADSL");//重新拨号
                    Thread.Sleep(20000);
                }
                else if (this.netset_vpn.Checked)
                {
                    DataRow dr = vpnDataTable.Rows[nextVpnId];
                    string IPServer = (string)dr["Ip"];
                    string username = (string)dr["UserName"];
                    string password = (string)dr["Password"];

                    DisConnectionVPN();
                    this.currVpnName = "VPN_123";
                    VPN.CreateVPN(this.currVpnName, IPServer, username, password);
                    VPN.ConnectToVPN(this.currVpnName, username, password);
                    Thread.Sleep(15000);

   /*       
                    DataRow dr = vpnDataTable.Rows[nextVpnId];
                    string IPServer = (string)dr["Ip"];
                    string username = (string)dr["UserName"];
                    string password = (string)dr["Password"];
                    string type = (string)dr["Type"];

                    DisConnectionVPN();

                    this.currVpnName = "VPN_" + type;

                    if ("US".Equals(type))
                    {
                        VPN.CreateVPN_US(this.currVpnName, IPServer, username, password);
                    }
                    else
                    {
                        VPN.CreateVPN(this.currVpnName, IPServer, username, password);
                    }
                    VPN.ConnectToVPN(this.currVpnName, username, password);
                    Thread.Sleep(15000);

                    nextVpnId = nextVpnId + 1;
                    if (nextVpnId >= (vpnDataTable.Rows.Count))
                    {
                        nextVpnId = 0;
                    }
    */
                }
                this.DoSearch(kwDataTable);
                beRunTime = beRunTime + 1;
                
                DisConnectionVPN();
               
            }

            kwDataTable.Dispose();
            vpnDataTable.Dispose();
        }

        public void DisConnectionVPN()
        {
            if (this.netset_vpn.Checked)
            {
                if (this.currVpnName != null)
                {
                    VPN.DisconnectFromVPN(this.currVpnName);
                    Thread.Sleep(2000);
                }
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

        private void DoSearch(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                bool isRun = System.Boolean.Parse(dr[4].ToString());
                if (!isRun)
                {
                    continue;
                }
                string companySite = (string)dr[1];
                string keyword = (string)dr[2];
                string productid = (string)dr[3];
                Int64 hitStr = (Int64)dr[5];
                searcher = new Searcher(this, keyword, productid, companySite);
                int[] result = searcher.DoSearchClick();
                if (result[0] == 1)
                {
                    dr[5] = hitStr + 1;
                    dr[6] = result[1] + "";
                }
                
                searcher = null;
                if (this._shouldStop)
                {
                    return;
                }
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            WriteXMLFile(this.dataGridView1);
        }

    }
}
