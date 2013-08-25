using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using com.soomes;

namespace waimaoba
{
    public partial class MainForm : Form
    {

        private Searcher searcher;
        public MainForm()
        {
            InitializeComponent();
        }

        private void search_Click(object sender, EventArgs e)
        {

            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerAsync();
            bgWorker.Dispose();
            search.Enabled = false;
         }

        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            searcher = new Searcher(20);
            searcher.DoSearchEvent += new SearchEvent(search_DoSearchEvent);
            //search.DoSearch(this.SearchText.Text);
            searcher.DoSearch("http://buyer.waimaoba.com/company/xh/discovery-info-plus-355051.html");
            //search.DoSearch("http://buyer.waimaoba.com/company/lk/getelec-259210.html");

        }
        private delegate void InvokeDelegate(string msg);
        void search_DoSearchEvent(object sender, SearchEventArgs e)
        {
            if (this.SearchText.InvokeRequired)
            {
                this.BeginInvoke(new InvokeDelegate(UpdateSearchText), new object[] { e.Msg });
            }
            else
            {
                UpdateSearchText(e.Msg);
            }
        }
        private void UpdateSearchText(string msg)
        {
            if (this.SearchUrlList.Lines.Length > 1000)
            {
                this.SearchUrlList.Text = msg;
            }
            else
            {
                this.SearchUrlList.Text = msg + "\r\n" + this.SearchUrlList.Text;
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            if (searcher != null)
            {
                searcher.Stop();
            }
            search.Enabled = true;
        }
    }
}
