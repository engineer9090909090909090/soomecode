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
            search.DoSearch("http://buyer.waimaoba.com/company/fo/concern-soyuz-group-351448.html");
        }
    }
}
