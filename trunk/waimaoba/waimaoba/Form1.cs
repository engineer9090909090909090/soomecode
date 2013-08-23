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
            Searcher search = new Searcher(5);
            search.DoSearch(this.SearchText.Text);
            /*
            search.DoSearch("http://buyer.waimaoba.com/company/jd/shakii-international-ltd-355094.html");
            search.DoSearch("http://104.waimaoba.com/company/bags-wear-llc-4");
            search.DoSearch("http://104.waimaoba.com/company/c2d-5");
            search.DoSearch("http://104.waimaoba.com/company/life-style-limited-10");
            search.DoSearch("http://105.waimaoba.com/company/suria-hwole-sales-1");
            search.DoSearch("http://105.waimaoba.com/company/roda-sa");
            search.DoSearch("http://105.waimaoba.com/company/succes-sa");
            search.DoSearch("http://110.waimaoba.com/company/allouzi-basel");
            search.DoSearch("http://109.waimaoba.com/content/cut-flowers-4");
            search.DoSearch("http://108.waimaoba.com/inquiry/atlas-industrial-door");
            search.DoSearch("http://109.waimaoba.com/content/8620-cold-forged-spline");
            */

        }
    }
}
