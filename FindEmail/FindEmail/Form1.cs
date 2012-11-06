using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FindEmail
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "http://www.google.de/search?q=usb+flash+drive";

            //url = "http://search.yahoo.com/search?p=usb+flash+drive&fr=sfp&fr2=&iscqry=";
            Searcher searcher = new Searcher();
            searcher.DoSearch(url);
        }
    }
}
