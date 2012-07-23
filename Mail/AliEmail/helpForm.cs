using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AliEmail
{
    public partial class helpForm : Form
    {
        public helpForm()
        {
            InitializeComponent();
            this.webBrowser.Navigate("file:///"+Application.StartupPath + "/help.mht");
        }
    }
}
