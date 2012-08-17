using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AliRank
{
    public partial class QueryKwForm : Form
    {

        private string IniFile;
        public static bool IsQuery = false;
        private bool IsCancel = false;
        public QueryKwForm()
        {
            InitializeComponent();
            IniFile = FileUtils.CreateAppDataFolderEmptyTextFile(Constants.INI_FILE);
        }

        private void QueryKwForm_Load(object sender, EventArgs e)
        {
            IsQuery = false;
            IsCancel = false;
            string keywords = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.KEY_WORDS, IniFile);
            if (!string.IsNullOrEmpty(keywords))
            {
                KwTextBox.Text = keywords.Replace(",", "\r\n");
            }
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            IsQuery = true;
            IsCancel = false;
            this.Close();
        }

        private void QueryKwForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!IsCancel)
            {
                string keywords = KwTextBox.Text = KwTextBox.Text.Replace("\r\n", ",");
                FileUtils.IniWriteValue(Constants.CLICK_SECTIONS, Constants.KEY_WORDS, keywords, IniFile);
            }
        }

        private void CancenBtn_Click(object sender, EventArgs e)
        {
            IsCancel = true;
            IsQuery = false;
            this.Close();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            IsCancel = false;
            IsQuery = false;
            this.Close();
        }
    }
}
