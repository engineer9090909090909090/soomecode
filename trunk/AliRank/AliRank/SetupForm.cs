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
            string network = FileUtils.IniReadValue(Constants.CLICK_SECTIONS, Constants.NETWORK_CHOICE, IniFile);
            if (network.Equals(Constants.NETWORK_VPN))
            {
                VPNRadioBtn.Checked = true;
            }else if (network.Equals(Constants.NETWORK_AGENT))
            {
                AgentRadioBtn.Checked = true;
            }
            else if (network.Equals(Constants.NETWORK_NONE))
            {
                NoneRadioBtn.Checked = true;
            }
            else {
                VPNRadioBtn.Checked = true;
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
            if (string.IsNullOrEmpty(str))
            {
                errorMsg.Text = "点击次数不能为空！";
                return;
            }
            FileUtils.IniWriteValue(Constants.CLICK_SECTIONS, Constants.AUTO_CLICK_NUM, str, IniFile);
            string Network = Constants.NETWORK_VPN;
            if (VPNRadioBtn.Checked)
            {
                Network = Constants.NETWORK_VPN;
            }
            if (AgentRadioBtn.Checked)
            {
                Network = Constants.NETWORK_AGENT;
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