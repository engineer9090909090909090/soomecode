using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AliRank
{
    public partial class SetupForm : Form
    {
        private string IniFile;
        public SetupForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(SetupForm_Load);
            IniFile = FileUtils.CreateAppDataFolderEmptyTextFile(Constants.INI_FILE);
        }

        void SetupForm_Load(object sender, EventArgs e)
        {
            string clickNum = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.AUTO_CLICK_NUM, IniFile);
            this.textBox1.Text = clickNum;
            string sMaxPauseTime = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.MAX_PAUSE_TIME, IniFile);
            this.MaxPauseTime.Text = sMaxPauseTime;
            string sMaxQueryPage = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.MAX_QUERY_PAGE, IniFile);
            this.MaxQueryPage.Text = sMaxQueryPage;
            string network = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.NETWORK_CHOICE, IniFile);
            if (network.Equals(Constants.NETWORK_VPN))
            {
                VPNRadioBtn.Checked = true;
            }
            else {
                NoneRadioBtn.Checked = true;
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ImpKwForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        private void ImportBtn_Click(object sender, EventArgs e)
        {
            string str = textBox1.Text;
            int a=0;
            if (string.IsNullOrEmpty(str) || int.TryParse(str, out a) == false)
            {
                errorMsg.Text = "点击次数不能为空且只能为数字！";
                return;
            }
            string maxPauseTime = this.MaxPauseTime.Text;
            if (string.IsNullOrEmpty(maxPauseTime) || int.TryParse(str, out a) == false)
            {
                errorMsg.Text = "随机暂停最大时间不能为空且只能为数字！";
                return;
            }
            string maxQueryPage = this.MaxQueryPage.Text;
            if (string.IsNullOrEmpty(maxQueryPage) || int.TryParse(str, out a) == false)
            {
                errorMsg.Text = "最大查询页数不能为空或且只能为数字！";
                return;
            }
            FileUtils.IniWriteValue(Constants.CLICK_SECTIONS, Constants.AUTO_CLICK_NUM, str, IniFile);
            FileUtils.IniWriteValue(Constants.CLICK_SECTIONS, Constants.MAX_PAUSE_TIME, maxPauseTime, IniFile);
            FileUtils.IniWriteValue(Constants.CLICK_SECTIONS, Constants.MAX_QUERY_PAGE, maxQueryPage, IniFile);
            string Network = Constants.NETWORK_VPN;
            if (VPNRadioBtn.Checked)
            {
                Network = Constants.NETWORK_VPN;
            }
            if (NoneRadioBtn.Checked)
            {
                Network = Constants.NETWORK_NONE;
            }
            FileUtils.IniWriteValue(Constants.CLICK_SECTIONS, Constants.NETWORK_CHOICE, Network, IniFile);
            
            this.Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}