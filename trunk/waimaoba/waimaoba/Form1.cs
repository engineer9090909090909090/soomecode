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
        public MainForm()
        {
            InitializeComponent();
        }

        private void search_Click(object sender, EventArgs e)
        {
            Searcher search = new Searcher();
            search.DoSearch("http://buyer.waimaoba.com/company/jd/shakii-international-ltd-355094.html");
            search.DoSearch("http://104.waimaoba.com/company/bags-wear-llc-4");
        }
    }
}
