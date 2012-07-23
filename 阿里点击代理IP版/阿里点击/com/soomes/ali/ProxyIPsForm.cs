using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.soomes.model;
using DBUtility.SQLite;

namespace com.soomes.ali
{
    public partial class ProxyIPsForm : Form
    {
        ProxyIpSearcher proxySearcher;
        public ProxyIPsForm()
        {
            InitializeComponent();
            proxySearcher = new ProxyIpSearcher();
            proxySearcher.CheckSuccessEvent += new CheckSuccessEventHandler(CheckSuccessEvent);
            proxySearcher.WriteLogEvent += new WriteLogEventHandler(WriteLogEvent);
            this.FormClosing += new FormClosingEventHandler(ProxyIPsForm_FormClosing);
            toolStripStatusLabel1.Text = "";
            DataTable table = new DataTable();
            table.Columns.Add("Proxy", typeof(string));
            table.Columns.Add("Location", typeof(string));
            proxyGridView.DataSource = table;
            proxyGridView.Columns[0].Width = 200;
            proxyGridView.Columns[1].Width = 270;
            table.Dispose();
        }


        private void searchBtn_Click(object sender, EventArgs e)
        {
            this.stopBtn.Enabled = true;
            this.searchBtn.Enabled = false;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_Completed);
            backgroundWorker1.RunWorkerAsync();           
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker1.DoWork -= new DoWorkEventHandler(BackgroundWorker_DoWork);
            proxySearcher.DoSearch(cnproxyCheck.Checked, minidailiCheck.Checked, proxycnCheck.Checked);
        }

        void WriteLogEvent(object sender, MyEventArgs e)
        {
            this.toolStripStatusLabel1.Text = (string)e.Value;
        }

        void CheckSuccessEvent(object sender, MyEventArgs e)
        {
            ProxyIpModel model = (ProxyIpModel)e.Value;
            if (DAO.HasExistProxy(model.Ip))
            {
                DAO.UpdateProxy(model);
            }else{
                DAO.AddProxy(model);
            }
            DataTable dataTable = (DataTable)proxyGridView.DataSource;
            DataRow newDr = dataTable.NewRow();
            newDr.ItemArray = new object[] { model.Ip, model.IpDesc };
            dataTable.Rows.InsertAt(newDr, 0);
        }

        private void BackgroundWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            backgroundWorker1.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(BackgroundWorker_Completed);
            this.searchBtn.Enabled = true;
            this.stopBtn.Enabled = false;
        }

        private void stop_Click(object sender, EventArgs e)
        {
            this.searchBtn.Enabled = true;
            this.stopBtn.Enabled = false;
            this.proxySearcher.Stop();
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
        }

        void ProxyIPsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.FormClosing -= new FormClosingEventHandler(ProxyIPsForm_FormClosing);
            if (proxySearcher != null)
            {
                proxySearcher.Stop();
            }
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
        }
    }
}
